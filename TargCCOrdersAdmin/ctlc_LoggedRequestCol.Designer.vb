<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_LoggedRequestCol
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
    Me.dgvLoggedRequest = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlLoggedRequest = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsLoggedLogin = New System.Windows.Forms.BindingSource(Me.components)
    Me.colLoggedLogin = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colLoggedLoginText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLoggedLogin = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTimeAccessed = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTimeAccessed = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsUser = New System.Windows.Forms.BindingSource(Me.components)
    Me.colUser = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colUserText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCallingFunctionWithinApplication = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCallingFunctionWithinApplication = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colEntryPoint = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleEntryPoint = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colProcess = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProcess = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colThread = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleThread = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvLoggedRequest, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsLoggedLogin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsUser, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlLoggedRequest, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvLoggedRequest
    '
    Me.dgvLoggedRequest.AllowUserToAddRows = False
    Me.dgvLoggedRequest.AllowUserToDeleteRows = False
    Me.dgvLoggedRequest.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvLoggedRequest.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvLoggedRequest.AutoGenerateColumns = False
    Me.dgvLoggedRequest.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvLoggedRequest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvLoggedRequest.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvLoggedRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvLoggedRequest.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colLoggedLogin, Me.colLoggedLoginText, Me.colTimeAccessed, Me.colUser, Me.colUserText, Me.colCallingFunctionWithinApplication, Me.colEntryPoint, Me.colProcess, Me.colThread})
    Me.dgvLoggedRequest.DataSource = Me.bsCtlLoggedRequest
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvLoggedRequest.DefaultCellStyle = styleDefaultCell
    Me.dgvLoggedRequest.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvLoggedRequest.EnableHeadersVisualStyles = False
    Me.dgvLoggedRequest.Location = New System.Drawing.Point(0, 25)
    Me.dgvLoggedRequest.MultiSelect = False 
    Me.dgvLoggedRequest.ContextMenuStrip = Me.cmsGrid 
    Me.dgvLoggedRequest.Name = "dgvLoggedRequest"
    Me.dgvLoggedRequest.RowHeadersVisible = False
    Me.dgvLoggedRequest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvLoggedRequest.Size = New System.Drawing.Size(712, 347)
    Me.dgvLoggedRequest.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlLoggedRequest
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleLoggedLogin, Me.mnuColVisibleTimeAccessed, Me.mnuColVisibleUser, Me.mnuColVisibleCallingFunctionWithinApplication, Me.mnuColVisibleEntryPoint, Me.mnuColVisibleProcess, Me.mnuColVisibleThread, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsLoggedLogin
    Me.bsLoggedLogin.DataSource = GetType(clsComboList)
    'bsUser
    Me.bsUser.DataSource = GetType(clsComboList)
    '
    'bsCtlLoggedRequest
    '
    Me.bsCtlLoggedRequest.DataSource = GetType(csLoggedRequest)
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
    'colLoggedLogin
    '
    Me.colLoggedLogin.DataPropertyName = "LoggedLoginID"
    Me.colLoggedLogin.DataSource = Me.bsLoggedLogin
    Me.colLoggedLogin.DisplayMember = "Text"
    Me.colLoggedLogin.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colLoggedLogin.HeaderText = "Logged Login"
    Me.colLoggedLogin.Name = "colLoggedLogin"
    Me.colLoggedLogin.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLoggedLogin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLoggedLogin.ValueMember = "KeyLong"
    Me.colLoggedLogin.Width = 60
    Me.colLoggedLogin.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colLoggedLoginText 
    ' 
    Me.colLoggedLoginText.DataPropertyName = "LoggedLoginText" 
    Me.colLoggedLoginText.HeaderText = "LoggedLogin" 
    Me.colLoggedLoginText.Name = "colLoggedLoginText" 
    Me.colLoggedLoginText.ReadOnly = True 
    Me.colLoggedLogin.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLoggedLogin 
    ' 
    Me.mnuColVisibleLoggedLogin.Checked = True 
    Me.mnuColVisibleLoggedLogin.CheckOnClick = True 
    Me.mnuColVisibleLoggedLogin.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLoggedLogin.Name = "mnuColVisibleLoggedLogin" 
    Me.mnuColVisibleLoggedLogin.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLoggedLogin.Text = "Logged Login" 
    ' 
    'colTimeAccessed
    '
    Me.colTimeAccessed.DataPropertyName = "TimeAccessed"
    Me.colTimeAccessed.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colTimeAccessed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colTimeAccessed.HeaderText = "Time Accessed"
    Me.colTimeAccessed.Name = "colTimeAccessed"
    Me.colTimeAccessed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTimeAccessed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTimeAccessed.Width = 60
    Me.colTimeAccessed.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTimeAccessed 
    ' 
    Me.mnuColVisibleTimeAccessed.Checked = True 
    Me.mnuColVisibleTimeAccessed.CheckOnClick = True 
    Me.mnuColVisibleTimeAccessed.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTimeAccessed.Name = "mnuColVisibleTimeAccessed" 
    Me.mnuColVisibleTimeAccessed.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTimeAccessed.Text = "Time Accessed" 
    ' 
    'colUser
    '
    Me.colUser.DataPropertyName = "UserID"
    Me.colUser.DataSource = Me.bsUser
    Me.colUser.DisplayMember = "Text"
    Me.colUser.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colUser.HeaderText = "User"
    Me.colUser.Name = "colUser"
    Me.colUser.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUser.ValueMember = "KeyLong"
    Me.colUser.Width = 60
    Me.colUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colUserText 
    ' 
    Me.colUserText.DataPropertyName = "UserText" 
    Me.colUserText.HeaderText = "User" 
    Me.colUserText.Name = "colUserText" 
    Me.colUserText.ReadOnly = True 
    Me.colUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUser 
    ' 
    Me.mnuColVisibleUser.Checked = True 
    Me.mnuColVisibleUser.CheckOnClick = True 
    Me.mnuColVisibleUser.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUser.Name = "mnuColVisibleUser" 
    Me.mnuColVisibleUser.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUser.Text = "User" 
    ' 
    'colCallingFunctionWithinApplication
    '
    Me.colCallingFunctionWithinApplication.DataPropertyName = "CallingFunctionWithinApplication"
    Me.colCallingFunctionWithinApplication.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCallingFunctionWithinApplication.HeaderText = "Calling Function Within Application"
    Me.colCallingFunctionWithinApplication.Name = "colCallingFunctionWithinApplication"
    Me.colCallingFunctionWithinApplication.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCallingFunctionWithinApplication.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCallingFunctionWithinApplication.Width = 60
    Me.colCallingFunctionWithinApplication.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCallingFunctionWithinApplication 
    ' 
    Me.mnuColVisibleCallingFunctionWithinApplication.Checked = True 
    Me.mnuColVisibleCallingFunctionWithinApplication.CheckOnClick = True 
    Me.mnuColVisibleCallingFunctionWithinApplication.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCallingFunctionWithinApplication.Name = "mnuColVisibleCallingFunctionWithinApplication" 
    Me.mnuColVisibleCallingFunctionWithinApplication.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCallingFunctionWithinApplication.Text = "Calling Function Within Application" 
    ' 
    'colEntryPoint
    '
    Me.colEntryPoint.DataPropertyName = "EntryPoint"
    Me.colEntryPoint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colEntryPoint.HeaderText = "Entry Point"
    Me.colEntryPoint.Name = "colEntryPoint"
    Me.colEntryPoint.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEntryPoint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEntryPoint.Width = 60
    Me.colEntryPoint.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEntryPoint 
    ' 
    Me.mnuColVisibleEntryPoint.Checked = True 
    Me.mnuColVisibleEntryPoint.CheckOnClick = True 
    Me.mnuColVisibleEntryPoint.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEntryPoint.Name = "mnuColVisibleEntryPoint" 
    Me.mnuColVisibleEntryPoint.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEntryPoint.Text = "Entry Point" 
    ' 
    'colProcess
    '
    Me.colProcess.DataPropertyName = "Process"
    Me.colProcess.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colProcess.HeaderText = "Process"
    Me.colProcess.Name = "colProcess"
    Me.colProcess.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colProcess.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colProcess.Width = 60
    Me.colProcess.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleProcess 
    ' 
    Me.mnuColVisibleProcess.Checked = True 
    Me.mnuColVisibleProcess.CheckOnClick = True 
    Me.mnuColVisibleProcess.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleProcess.Name = "mnuColVisibleProcess" 
    Me.mnuColVisibleProcess.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleProcess.Text = "Process" 
    ' 
    'colThread
    '
    Me.colThread.DataPropertyName = "Thread"
    Me.colThread.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colThread.HeaderText = "Thread"
    Me.colThread.Name = "colThread"
    Me.colThread.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colThread.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colThread.Width = 60
    Me.colThread.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleThread 
    ' 
    Me.mnuColVisibleThread.Checked = True 
    Me.mnuColVisibleThread.CheckOnClick = True 
    Me.mnuColVisibleThread.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleThread.Name = "mnuColVisibleThread" 
    Me.mnuColVisibleThread.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleThread.Text = "Thread" 
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
    'ctlLoggedRequestCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvLoggedRequest)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_LoggedRequestCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvLoggedRequest, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsLoggedLogin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsUser, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlLoggedRequest, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvLoggedRequest As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlLoggedRequest As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsLoggedLogin As System.Windows.Forms.BindingSource
  Friend WithEvents colLoggedLogin As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colLoggedLoginText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleLoggedLogin As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTimeAccessed As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTimeAccessed As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsUser As System.Windows.Forms.BindingSource
  Friend WithEvents colUser As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colUserText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCallingFunctionWithinApplication As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCallingFunctionWithinApplication As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEntryPoint As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleEntryPoint As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colProcess As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleProcess As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colThread As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleThread As System.Windows.Forms.ToolStripMenuItem 

End Class
