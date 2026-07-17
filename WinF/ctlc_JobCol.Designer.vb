<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_JobCol
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
    Me.dgvJob = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlJob = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsJob = New System.Windows.Forms.BindingSource(Me.components)
    Me.colJob = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleJob = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsJobRunner = New System.Windows.Forms.BindingSource(Me.components)
    Me.colJobRunner = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleJobRunner = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDescription = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colInstructions = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleInstructions = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsJobType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colJobType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleJobType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colWhenToRun = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleWhenToRun = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCyclicCount = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCyclicCount = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSendNotificationOnSuccess = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleSendNotificationOnSuccess = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colSendAlarmOnMissed = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleSendAlarmOnMissed = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colTimeOutSec = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTimeOutSec = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colActive = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleActive = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colActivatingUser = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleActivatingUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNextRunTime = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNextRunTime = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLastRunTime = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastRunTime = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsJobStatus = New System.Windows.Forms.BindingSource(Me.components)
    Me.colJobStatus = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleJobStatus = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colWarningMailSent = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleWarningMailSent = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colIsManaged = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleIsManaged = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colLastRunBy = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastRunBy = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvJob, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsJob, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsJobRunner, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsJobType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsJobStatus, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlJob, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvJob
    '
    Me.dgvJob.AllowUserToAddRows = False
    Me.dgvJob.AllowUserToDeleteRows = False
    Me.dgvJob.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvJob.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvJob.AutoGenerateColumns = False
    Me.dgvJob.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvJob.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvJob.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvJob.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvJob.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colJob, Me.colJobRunner, Me.colDescription, Me.colInstructions, Me.colJobType, Me.colWhenToRun, Me.colCyclicCount, Me.colSendNotificationOnSuccess, Me.colSendAlarmOnMissed, Me.colTimeOutSec, Me.colActive, Me.colActivatingUser, Me.colNextRunTime, Me.colLastRunTime, Me.colJobStatus, Me.colWarningMailSent, Me.colIsManaged, Me.colLastRunBy})
    Me.dgvJob.DataSource = Me.bsCtlJob
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvJob.DefaultCellStyle = styleDefaultCell
    Me.dgvJob.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvJob.EnableHeadersVisualStyles = False
    Me.dgvJob.Location = New System.Drawing.Point(0, 25)
    Me.dgvJob.MultiSelect = False 
    Me.dgvJob.ContextMenuStrip = Me.cmsGrid 
    Me.dgvJob.Name = "dgvJob"
    Me.dgvJob.RowHeadersVisible = False
    Me.dgvJob.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvJob.Size = New System.Drawing.Size(712, 347)
    Me.dgvJob.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlJob
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleJob, Me.mnuColVisibleJobRunner, Me.mnuColVisibleDescription, Me.mnuColVisibleInstructions, Me.mnuColVisibleJobType, Me.mnuColVisibleWhenToRun, Me.mnuColVisibleCyclicCount, Me.mnuColVisibleSendNotificationOnSuccess, Me.mnuColVisibleSendAlarmOnMissed, Me.mnuColVisibleTimeOutSec, Me.mnuColVisibleActive, Me.mnuColVisibleActivatingUser, Me.mnuColVisibleNextRunTime, Me.mnuColVisibleLastRunTime, Me.mnuColVisibleJobStatus, Me.mnuColVisibleWarningMailSent, Me.mnuColVisibleIsManaged, Me.mnuColVisibleLastRunBy, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsJob
    Me.bsJob.DataSource = GetType(clsComboList)
    'bsJobRunner
    Me.bsJobRunner.DataSource = GetType(clsComboList)
    'bsJobType
    Me.bsJobType.DataSource = GetType(clsComboList)
    'bsJobStatus
    Me.bsJobStatus.DataSource = GetType(clsComboList)
    '
    'bsCtlJob
    '
    Me.bsCtlJob.DataSource = GetType(csJob)
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
    Me.colJob.DataPropertyName = "JobCode"
    Me.colJob.DataSource = Me.bsJob
    Me.colJob.DisplayMember = "Text"
    Me.colJob.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colJob.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colJob.HeaderText = "Job"
    Me.colJob.Name = "colJob"
    Me.colJob.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colJob.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colJob.ValueMember = "KeyString"
    Me.colJob.Width = 60
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
    'colJobRunner
    '
    Me.colJobRunner.DataPropertyName = "JobRunnerCode"
    Me.colJobRunner.DataSource = Me.bsJobRunner
    Me.colJobRunner.DisplayMember = "Text"
    Me.colJobRunner.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colJobRunner.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colJobRunner.HeaderText = "Job Runner"
    Me.colJobRunner.Name = "colJobRunner"
    Me.colJobRunner.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colJobRunner.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colJobRunner.ValueMember = "KeyString"
    Me.colJobRunner.Width = 60
    Me.colJobRunner.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleJobRunner 
    ' 
    Me.mnuColVisibleJobRunner.Checked = True 
    Me.mnuColVisibleJobRunner.CheckOnClick = True 
    Me.mnuColVisibleJobRunner.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleJobRunner.Name = "mnuColVisibleJobRunner" 
    Me.mnuColVisibleJobRunner.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleJobRunner.Text = "Job Runner" 
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
    'colInstructions
    '
    Me.colInstructions.DataPropertyName = "Instructions"
    Me.colInstructions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colInstructions.HeaderText = "Instructions"
    Me.colInstructions.Name = "colInstructions"
    Me.colInstructions.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colInstructions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colInstructions.Width = 60
    Me.colInstructions.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleInstructions 
    ' 
    Me.mnuColVisibleInstructions.Checked = True 
    Me.mnuColVisibleInstructions.CheckOnClick = True 
    Me.mnuColVisibleInstructions.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleInstructions.Name = "mnuColVisibleInstructions" 
    Me.mnuColVisibleInstructions.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleInstructions.Text = "Instructions" 
    ' 
    'colJobType
    '
    Me.colJobType.DataPropertyName = "JobType"
    Me.colJobType.DataSource = Me.bsJobType
    Me.colJobType.DisplayMember = "Text"
    Me.colJobType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colJobType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colJobType.HeaderText = "Job Type"
    Me.colJobType.Name = "colJobType"
    Me.colJobType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colJobType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colJobType.ValueMember = "KeyEnum"
    Me.colJobType.Width = 60
    Me.colJobType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleJobType 
    ' 
    Me.mnuColVisibleJobType.Checked = True 
    Me.mnuColVisibleJobType.CheckOnClick = True 
    Me.mnuColVisibleJobType.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleJobType.Name = "mnuColVisibleJobType" 
    Me.mnuColVisibleJobType.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleJobType.Text = "Job Type" 
    ' 
    'colWhenToRun
    '
    Me.colWhenToRun.DataPropertyName = "WhenToRun"
    Me.colWhenToRun.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colWhenToRun.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colWhenToRun.HeaderText = "When To Run"
    Me.colWhenToRun.Name = "colWhenToRun"
    Me.colWhenToRun.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWhenToRun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWhenToRun.Width = 60
    Me.colWhenToRun.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWhenToRun 
    ' 
    Me.mnuColVisibleWhenToRun.Checked = True 
    Me.mnuColVisibleWhenToRun.CheckOnClick = True 
    Me.mnuColVisibleWhenToRun.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWhenToRun.Name = "mnuColVisibleWhenToRun" 
    Me.mnuColVisibleWhenToRun.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWhenToRun.Text = "When To Run" 
    ' 
    'colCyclicCount
    '
    Me.colCyclicCount.DataPropertyName = "CyclicCount"
    Me.colCyclicCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colCyclicCount.HeaderText = "Cyclic Count"
    Me.colCyclicCount.Name = "colCyclicCount"
    Me.colCyclicCount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCyclicCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCyclicCount.Width = 60
    Me.colCyclicCount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCyclicCount 
    ' 
    Me.mnuColVisibleCyclicCount.Checked = True 
    Me.mnuColVisibleCyclicCount.CheckOnClick = True 
    Me.mnuColVisibleCyclicCount.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCyclicCount.Name = "mnuColVisibleCyclicCount" 
    Me.mnuColVisibleCyclicCount.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCyclicCount.Text = "Cyclic Count" 
    ' 
    'colSendNotificationOnSuccess
    '
    Me.colSendNotificationOnSuccess.DataPropertyName = "SendNotificationOnSuccess"
    Me.colSendNotificationOnSuccess.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colSendNotificationOnSuccess.HeaderText = "Send Notification On Success"
    Me.colSendNotificationOnSuccess.Name = "colSendNotificationOnSuccess"
    Me.colSendNotificationOnSuccess.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSendNotificationOnSuccess.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSendNotificationOnSuccess.Width = 60
    Me.colSendNotificationOnSuccess.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSendNotificationOnSuccess 
    ' 
    Me.mnuColVisibleSendNotificationOnSuccess.Checked = True 
    Me.mnuColVisibleSendNotificationOnSuccess.CheckOnClick = True 
    Me.mnuColVisibleSendNotificationOnSuccess.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSendNotificationOnSuccess.Name = "mnuColVisibleSendNotificationOnSuccess" 
    Me.mnuColVisibleSendNotificationOnSuccess.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSendNotificationOnSuccess.Text = "Send Notification On Success" 
    ' 
    'colSendAlarmOnMissed
    '
    Me.colSendAlarmOnMissed.DataPropertyName = "SendAlarmOnMissed"
    Me.colSendAlarmOnMissed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colSendAlarmOnMissed.HeaderText = "Send Alarm On Missed"
    Me.colSendAlarmOnMissed.Name = "colSendAlarmOnMissed"
    Me.colSendAlarmOnMissed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSendAlarmOnMissed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSendAlarmOnMissed.Width = 60
    Me.colSendAlarmOnMissed.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSendAlarmOnMissed 
    ' 
    Me.mnuColVisibleSendAlarmOnMissed.Checked = True 
    Me.mnuColVisibleSendAlarmOnMissed.CheckOnClick = True 
    Me.mnuColVisibleSendAlarmOnMissed.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSendAlarmOnMissed.Name = "mnuColVisibleSendAlarmOnMissed" 
    Me.mnuColVisibleSendAlarmOnMissed.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSendAlarmOnMissed.Text = "Send Alarm On Missed" 
    ' 
    'colTimeOutSec
    '
    Me.colTimeOutSec.DataPropertyName = "TimeOutSec"
    Me.colTimeOutSec.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colTimeOutSec.HeaderText = "Time Out Sec"
    Me.colTimeOutSec.Name = "colTimeOutSec"
    Me.colTimeOutSec.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTimeOutSec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTimeOutSec.Width = 60
    Me.colTimeOutSec.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTimeOutSec 
    ' 
    Me.mnuColVisibleTimeOutSec.Checked = True 
    Me.mnuColVisibleTimeOutSec.CheckOnClick = True 
    Me.mnuColVisibleTimeOutSec.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTimeOutSec.Name = "mnuColVisibleTimeOutSec" 
    Me.mnuColVisibleTimeOutSec.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTimeOutSec.Text = "Time Out Sec" 
    ' 
    'colActive
    '
    Me.colActive.DataPropertyName = "Active"
    Me.colActive.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colActive.HeaderText = "Active"
    Me.colActive.Name = "colActive"
    Me.colActive.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colActive.Width = 60
    Me.colActive.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleActive 
    ' 
    Me.mnuColVisibleActive.Checked = True 
    Me.mnuColVisibleActive.CheckOnClick = True 
    Me.mnuColVisibleActive.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleActive.Name = "mnuColVisibleActive" 
    Me.mnuColVisibleActive.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleActive.Text = "Active" 
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
    'colNextRunTime
    '
    Me.colNextRunTime.DataPropertyName = "NextRunTime"
    Me.colNextRunTime.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colNextRunTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colNextRunTime.HeaderText = "Next Run Time"
    Me.colNextRunTime.Name = "colNextRunTime"
    Me.colNextRunTime.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colNextRunTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colNextRunTime.Width = 60
    Me.colNextRunTime.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleNextRunTime 
    ' 
    Me.mnuColVisibleNextRunTime.Checked = True 
    Me.mnuColVisibleNextRunTime.CheckOnClick = True 
    Me.mnuColVisibleNextRunTime.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleNextRunTime.Name = "mnuColVisibleNextRunTime" 
    Me.mnuColVisibleNextRunTime.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleNextRunTime.Text = "Next Run Time" 
    ' 
    'colLastRunTime
    '
    Me.colLastRunTime.DataPropertyName = "LastRunTime"
    Me.colLastRunTime.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colLastRunTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colLastRunTime.HeaderText = "Last Run Time"
    Me.colLastRunTime.Name = "colLastRunTime"
    Me.colLastRunTime.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLastRunTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLastRunTime.Width = 60
    Me.colLastRunTime.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLastRunTime 
    ' 
    Me.mnuColVisibleLastRunTime.Checked = True 
    Me.mnuColVisibleLastRunTime.CheckOnClick = True 
    Me.mnuColVisibleLastRunTime.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLastRunTime.Name = "mnuColVisibleLastRunTime" 
    Me.mnuColVisibleLastRunTime.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLastRunTime.Text = "Last Run Time" 
    ' 
    'colJobStatus
    '
    Me.colJobStatus.DataPropertyName = "JobStatus"
    Me.colJobStatus.DataSource = Me.bsJobStatus
    Me.colJobStatus.DisplayMember = "Text"
    Me.colJobStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colJobStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colJobStatus.HeaderText = "Job Status"
    Me.colJobStatus.Name = "colJobStatus"
    Me.colJobStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colJobStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colJobStatus.ValueMember = "KeyEnum"
    Me.colJobStatus.Width = 60
    Me.colJobStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleJobStatus 
    ' 
    Me.mnuColVisibleJobStatus.Checked = True 
    Me.mnuColVisibleJobStatus.CheckOnClick = True 
    Me.mnuColVisibleJobStatus.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleJobStatus.Name = "mnuColVisibleJobStatus" 
    Me.mnuColVisibleJobStatus.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleJobStatus.Text = "Job Status" 
    ' 
    'colWarningMailSent
    '
    Me.colWarningMailSent.DataPropertyName = "WarningMailSent"
    Me.colWarningMailSent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colWarningMailSent.HeaderText = "Warning Mail Sent"
    Me.colWarningMailSent.Name = "colWarningMailSent"
    Me.colWarningMailSent.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWarningMailSent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWarningMailSent.Width = 60
    Me.colWarningMailSent.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWarningMailSent 
    ' 
    Me.mnuColVisibleWarningMailSent.Checked = True 
    Me.mnuColVisibleWarningMailSent.CheckOnClick = True 
    Me.mnuColVisibleWarningMailSent.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWarningMailSent.Name = "mnuColVisibleWarningMailSent" 
    Me.mnuColVisibleWarningMailSent.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWarningMailSent.Text = "Warning Mail Sent" 
    ' 
    'colIsManaged
    '
    Me.colIsManaged.DataPropertyName = "IsManaged"
    Me.colIsManaged.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colIsManaged.HeaderText = "Is Managed"
    Me.colIsManaged.Name = "colIsManaged"
    Me.colIsManaged.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIsManaged.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIsManaged.Width = 60
    Me.colIsManaged.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIsManaged 
    ' 
    Me.mnuColVisibleIsManaged.Checked = True 
    Me.mnuColVisibleIsManaged.CheckOnClick = True 
    Me.mnuColVisibleIsManaged.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIsManaged.Name = "mnuColVisibleIsManaged" 
    Me.mnuColVisibleIsManaged.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIsManaged.Text = "Is Managed" 
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
    'ctlJobCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvJob)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_JobCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvJob, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsJob, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsJobRunner, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsJobType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsJobStatus, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlJob, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvJob As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlJob As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsJob As System.Windows.Forms.BindingSource
  Friend WithEvents colJob As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleJob As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsJobRunner As System.Windows.Forms.BindingSource
  Friend WithEvents colJobRunner As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleJobRunner As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDescription As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colInstructions As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleInstructions As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsJobType As System.Windows.Forms.BindingSource
  Friend WithEvents colJobType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleJobType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWhenToRun As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleWhenToRun As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCyclicCount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCyclicCount As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSendNotificationOnSuccess As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleSendNotificationOnSuccess As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSendAlarmOnMissed As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleSendAlarmOnMissed As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTimeOutSec As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTimeOutSec As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colActive As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleActive As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colActivatingUser As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleActivatingUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNextRunTime As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNextRunTime As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastRunTime As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastRunTime As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsJobStatus As System.Windows.Forms.BindingSource
  Friend WithEvents colJobStatus As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleJobStatus As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWarningMailSent As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleWarningMailSent As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIsManaged As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleIsManaged As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastRunBy As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastRunBy As System.Windows.Forms.ToolStripMenuItem 

End Class
