<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_LoggedAlertCol
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
    Me.dgvLoggedAlert = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlLoggedAlert = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTimeOccurred = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTimeOccurred = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFaultNumber = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFaultNumber = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSystemName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSystemName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCallingApplication = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCallingApplication = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsAffectedUser = New System.Windows.Forms.BindingSource(Me.components)
    Me.colAffectedUser = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colAffectedUserText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAffectedUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCallingApplicationVersion = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCallingApplicationVersion = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCallingFunctionWithinApplication = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCallingFunctionWithinApplication = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFreeText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFreeText = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFaultingAssembly = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFaultingAssembly = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAssemblyEntryPoint = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAssemblyEntryPoint = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFaultingClass = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFaultingClass = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFaultingFunction = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFaultingFunction = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFaultingFunctionParameters = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFaultingFunctionParameters = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFaultIdent = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFaultIdent = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFaultDescription = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFaultDescription = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colMessageSentToUser = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleMessageSentToUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colActionSentToUser = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleActionSentToUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsFaultType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colFaultType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleFaultType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsFaultSeverity = New System.Windows.Forms.BindingSource(Me.components)
    Me.colFaultSeverity = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleFaultSeverity = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsLoggedLogin = New System.Windows.Forms.BindingSource(Me.components)
    Me.colLoggedLogin = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colLoggedLoginText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLoggedLogin = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colThread = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleThread = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsUserIdentityType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colUserIdentityType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleUserIdentityType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsUserIdentityTypeName = New System.Windows.Forms.BindingSource(Me.components)
    Me.colUserIdentityTypeName = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleUserIdentityTypeName = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colDateOccurred = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDateOccurred = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colMonthOccurred = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleMonthOccurred = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvLoggedAlert, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsAffectedUser, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsFaultType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsFaultSeverity, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsLoggedLogin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsUserIdentityType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsUserIdentityTypeName, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlLoggedAlert, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvLoggedAlert
    '
    Me.dgvLoggedAlert.AllowUserToAddRows = False
    Me.dgvLoggedAlert.AllowUserToDeleteRows = False
    Me.dgvLoggedAlert.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvLoggedAlert.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvLoggedAlert.AutoGenerateColumns = False
    Me.dgvLoggedAlert.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvLoggedAlert.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvLoggedAlert.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvLoggedAlert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvLoggedAlert.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colTimeOccurred, Me.colFaultNumber, Me.colSystemName, Me.colCallingApplication, Me.colAffectedUser, Me.colAffectedUserText, Me.colCallingApplicationVersion, Me.colCallingFunctionWithinApplication, Me.colFreeText, Me.colFaultingAssembly, Me.colAssemblyEntryPoint, Me.colFaultingClass, Me.colFaultingFunction, Me.colFaultingFunctionParameters, Me.colFaultIdent, Me.colFaultDescription, Me.colMessageSentToUser, Me.colActionSentToUser, Me.colFaultType, Me.colFaultSeverity, Me.colLoggedLogin, Me.colLoggedLoginText, Me.colThread, Me.colUserIdentityType, Me.colUserIdentityTypeName, Me.colDateOccurred, Me.colMonthOccurred})
    Me.dgvLoggedAlert.DataSource = Me.bsCtlLoggedAlert
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvLoggedAlert.DefaultCellStyle = styleDefaultCell
    Me.dgvLoggedAlert.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvLoggedAlert.EnableHeadersVisualStyles = False
    Me.dgvLoggedAlert.Location = New System.Drawing.Point(0, 25)
    Me.dgvLoggedAlert.MultiSelect = False 
    Me.dgvLoggedAlert.ContextMenuStrip = Me.cmsGrid 
    Me.dgvLoggedAlert.Name = "dgvLoggedAlert"
    Me.dgvLoggedAlert.RowHeadersVisible = False
    Me.dgvLoggedAlert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvLoggedAlert.Size = New System.Drawing.Size(712, 347)
    Me.dgvLoggedAlert.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlLoggedAlert
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleTimeOccurred, Me.mnuColVisibleFaultNumber, Me.mnuColVisibleSystemName, Me.mnuColVisibleCallingApplication, Me.mnuColVisibleAffectedUser, Me.mnuColVisibleCallingApplicationVersion, Me.mnuColVisibleCallingFunctionWithinApplication, Me.mnuColVisibleFreeText, Me.mnuColVisibleFaultingAssembly, Me.mnuColVisibleAssemblyEntryPoint, Me.mnuColVisibleFaultingClass, Me.mnuColVisibleFaultingFunction, Me.mnuColVisibleFaultingFunctionParameters, Me.mnuColVisibleFaultIdent, Me.mnuColVisibleFaultDescription, Me.mnuColVisibleMessageSentToUser, Me.mnuColVisibleActionSentToUser, Me.mnuColVisibleFaultType, Me.mnuColVisibleFaultSeverity, Me.mnuColVisibleLoggedLogin, Me.mnuColVisibleThread, Me.mnuColVisibleUserIdentityType, Me.mnuColVisibleUserIdentityTypeName, Me.mnuColVisibleDateOccurred, Me.mnuColVisibleMonthOccurred, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsAffectedUser
    Me.bsAffectedUser.DataSource = GetType(clsComboList)
    'bsFaultType
    Me.bsFaultType.DataSource = GetType(clsComboList)
    'bsFaultSeverity
    Me.bsFaultSeverity.DataSource = GetType(clsComboList)
    'bsLoggedLogin
    Me.bsLoggedLogin.DataSource = GetType(clsComboList)
    'bsUserIdentityType
    Me.bsUserIdentityType.DataSource = GetType(clsComboList)
    'bsUserIdentityTypeName
    Me.bsUserIdentityTypeName.DataSource = GetType(clsComboList)
    '
    'bsCtlLoggedAlert
    '
    Me.bsCtlLoggedAlert.DataSource = GetType(csLoggedAlert)
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
    'colTimeOccurred
    '
    Me.colTimeOccurred.DataPropertyName = "TimeOccurred"
    Me.colTimeOccurred.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colTimeOccurred.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colTimeOccurred.HeaderText = "Time Occurred"
    Me.colTimeOccurred.Name = "colTimeOccurred"
    Me.colTimeOccurred.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTimeOccurred.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTimeOccurred.Width = 60
    Me.colTimeOccurred.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTimeOccurred 
    ' 
    Me.mnuColVisibleTimeOccurred.Checked = True 
    Me.mnuColVisibleTimeOccurred.CheckOnClick = True 
    Me.mnuColVisibleTimeOccurred.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTimeOccurred.Name = "mnuColVisibleTimeOccurred" 
    Me.mnuColVisibleTimeOccurred.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTimeOccurred.Text = "Time Occurred" 
    ' 
    'colFaultNumber
    '
    Me.colFaultNumber.DataPropertyName = "FaultNumber"
    Me.colFaultNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colFaultNumber.HeaderText = "Fault Number"
    Me.colFaultNumber.Name = "colFaultNumber"
    Me.colFaultNumber.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultNumber.Width = 60
    Me.colFaultNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultNumber 
    ' 
    Me.mnuColVisibleFaultNumber.Checked = True 
    Me.mnuColVisibleFaultNumber.CheckOnClick = True 
    Me.mnuColVisibleFaultNumber.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultNumber.Name = "mnuColVisibleFaultNumber" 
    Me.mnuColVisibleFaultNumber.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultNumber.Text = "Fault Number" 
    ' 
    'colSystemName
    '
    Me.colSystemName.DataPropertyName = "SystemName"
    Me.colSystemName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSystemName.HeaderText = "System Name"
    Me.colSystemName.Name = "colSystemName"
    Me.colSystemName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSystemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSystemName.Width = 60
    Me.colSystemName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSystemName 
    ' 
    Me.mnuColVisibleSystemName.Checked = True 
    Me.mnuColVisibleSystemName.CheckOnClick = True 
    Me.mnuColVisibleSystemName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSystemName.Name = "mnuColVisibleSystemName" 
    Me.mnuColVisibleSystemName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSystemName.Text = "System Name" 
    ' 
    'colCallingApplication
    '
    Me.colCallingApplication.DataPropertyName = "CallingApplication"
    Me.colCallingApplication.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCallingApplication.HeaderText = "Calling Application"
    Me.colCallingApplication.Name = "colCallingApplication"
    Me.colCallingApplication.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCallingApplication.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCallingApplication.Width = 60
    Me.colCallingApplication.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCallingApplication 
    ' 
    Me.mnuColVisibleCallingApplication.Checked = True 
    Me.mnuColVisibleCallingApplication.CheckOnClick = True 
    Me.mnuColVisibleCallingApplication.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCallingApplication.Name = "mnuColVisibleCallingApplication" 
    Me.mnuColVisibleCallingApplication.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCallingApplication.Text = "Calling Application" 
    ' 
    'colAffectedUser
    '
    Me.colAffectedUser.DataPropertyName = "AffectedUserID"
    Me.colAffectedUser.DataSource = Me.bsAffectedUser
    Me.colAffectedUser.DisplayMember = "Text"
    Me.colAffectedUser.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colAffectedUser.HeaderText = "Affected User"
    Me.colAffectedUser.Name = "colAffectedUser"
    Me.colAffectedUser.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAffectedUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAffectedUser.ValueMember = "KeyLong"
    Me.colAffectedUser.Width = 60
    Me.colAffectedUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colAffectedUserText 
    ' 
    Me.colAffectedUserText.DataPropertyName = "AffectedUserText" 
    Me.colAffectedUserText.HeaderText = "AffectedUser" 
    Me.colAffectedUserText.Name = "colAffectedUserText" 
    Me.colAffectedUserText.ReadOnly = True 
    Me.colAffectedUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAffectedUser 
    ' 
    Me.mnuColVisibleAffectedUser.Checked = True 
    Me.mnuColVisibleAffectedUser.CheckOnClick = True 
    Me.mnuColVisibleAffectedUser.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAffectedUser.Name = "mnuColVisibleAffectedUser" 
    Me.mnuColVisibleAffectedUser.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAffectedUser.Text = "Affected User" 
    ' 
    'colCallingApplicationVersion
    '
    Me.colCallingApplicationVersion.DataPropertyName = "CallingApplicationVersion"
    Me.colCallingApplicationVersion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCallingApplicationVersion.HeaderText = "Calling Application Version"
    Me.colCallingApplicationVersion.Name = "colCallingApplicationVersion"
    Me.colCallingApplicationVersion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCallingApplicationVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCallingApplicationVersion.Width = 60
    Me.colCallingApplicationVersion.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCallingApplicationVersion 
    ' 
    Me.mnuColVisibleCallingApplicationVersion.Checked = True 
    Me.mnuColVisibleCallingApplicationVersion.CheckOnClick = True 
    Me.mnuColVisibleCallingApplicationVersion.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCallingApplicationVersion.Name = "mnuColVisibleCallingApplicationVersion" 
    Me.mnuColVisibleCallingApplicationVersion.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCallingApplicationVersion.Text = "Calling Application Version" 
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
    'colFreeText
    '
    Me.colFreeText.DataPropertyName = "FreeText"
    Me.colFreeText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFreeText.HeaderText = "Free Text"
    Me.colFreeText.Name = "colFreeText"
    Me.colFreeText.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFreeText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFreeText.Width = 60
    Me.colFreeText.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFreeText 
    ' 
    Me.mnuColVisibleFreeText.Checked = True 
    Me.mnuColVisibleFreeText.CheckOnClick = True 
    Me.mnuColVisibleFreeText.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFreeText.Name = "mnuColVisibleFreeText" 
    Me.mnuColVisibleFreeText.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFreeText.Text = "Free Text" 
    ' 
    'colFaultingAssembly
    '
    Me.colFaultingAssembly.DataPropertyName = "FaultingAssembly"
    Me.colFaultingAssembly.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFaultingAssembly.HeaderText = "Faulting Assembly"
    Me.colFaultingAssembly.Name = "colFaultingAssembly"
    Me.colFaultingAssembly.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultingAssembly.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultingAssembly.Width = 60
    Me.colFaultingAssembly.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultingAssembly 
    ' 
    Me.mnuColVisibleFaultingAssembly.Checked = True 
    Me.mnuColVisibleFaultingAssembly.CheckOnClick = True 
    Me.mnuColVisibleFaultingAssembly.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultingAssembly.Name = "mnuColVisibleFaultingAssembly" 
    Me.mnuColVisibleFaultingAssembly.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultingAssembly.Text = "Faulting Assembly" 
    ' 
    'colAssemblyEntryPoint
    '
    Me.colAssemblyEntryPoint.DataPropertyName = "AssemblyEntryPoint"
    Me.colAssemblyEntryPoint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colAssemblyEntryPoint.HeaderText = "Assembly Entry Point"
    Me.colAssemblyEntryPoint.Name = "colAssemblyEntryPoint"
    Me.colAssemblyEntryPoint.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAssemblyEntryPoint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAssemblyEntryPoint.Width = 60
    Me.colAssemblyEntryPoint.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAssemblyEntryPoint 
    ' 
    Me.mnuColVisibleAssemblyEntryPoint.Checked = True 
    Me.mnuColVisibleAssemblyEntryPoint.CheckOnClick = True 
    Me.mnuColVisibleAssemblyEntryPoint.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAssemblyEntryPoint.Name = "mnuColVisibleAssemblyEntryPoint" 
    Me.mnuColVisibleAssemblyEntryPoint.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAssemblyEntryPoint.Text = "Assembly Entry Point" 
    ' 
    'colFaultingClass
    '
    Me.colFaultingClass.DataPropertyName = "FaultingClass"
    Me.colFaultingClass.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFaultingClass.HeaderText = "Faulting Class"
    Me.colFaultingClass.Name = "colFaultingClass"
    Me.colFaultingClass.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultingClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultingClass.Width = 60
    Me.colFaultingClass.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultingClass 
    ' 
    Me.mnuColVisibleFaultingClass.Checked = True 
    Me.mnuColVisibleFaultingClass.CheckOnClick = True 
    Me.mnuColVisibleFaultingClass.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultingClass.Name = "mnuColVisibleFaultingClass" 
    Me.mnuColVisibleFaultingClass.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultingClass.Text = "Faulting Class" 
    ' 
    'colFaultingFunction
    '
    Me.colFaultingFunction.DataPropertyName = "FaultingFunction"
    Me.colFaultingFunction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFaultingFunction.HeaderText = "Faulting Function"
    Me.colFaultingFunction.Name = "colFaultingFunction"
    Me.colFaultingFunction.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultingFunction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultingFunction.Width = 60
    Me.colFaultingFunction.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultingFunction 
    ' 
    Me.mnuColVisibleFaultingFunction.Checked = True 
    Me.mnuColVisibleFaultingFunction.CheckOnClick = True 
    Me.mnuColVisibleFaultingFunction.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultingFunction.Name = "mnuColVisibleFaultingFunction" 
    Me.mnuColVisibleFaultingFunction.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultingFunction.Text = "Faulting Function" 
    ' 
    'colFaultingFunctionParameters
    '
    Me.colFaultingFunctionParameters.DataPropertyName = "FaultingFunctionParameters"
    Me.colFaultingFunctionParameters.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFaultingFunctionParameters.HeaderText = "Faulting Function Parameters"
    Me.colFaultingFunctionParameters.Name = "colFaultingFunctionParameters"
    Me.colFaultingFunctionParameters.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultingFunctionParameters.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultingFunctionParameters.Width = 60
    Me.colFaultingFunctionParameters.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultingFunctionParameters 
    ' 
    Me.mnuColVisibleFaultingFunctionParameters.Checked = True 
    Me.mnuColVisibleFaultingFunctionParameters.CheckOnClick = True 
    Me.mnuColVisibleFaultingFunctionParameters.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultingFunctionParameters.Name = "mnuColVisibleFaultingFunctionParameters" 
    Me.mnuColVisibleFaultingFunctionParameters.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultingFunctionParameters.Text = "Faulting Function Parameters" 
    ' 
    'colFaultIdent
    '
    Me.colFaultIdent.DataPropertyName = "FaultIdent"
    Me.colFaultIdent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFaultIdent.HeaderText = "Fault Ident"
    Me.colFaultIdent.Name = "colFaultIdent"
    Me.colFaultIdent.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultIdent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultIdent.Width = 60
    Me.colFaultIdent.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultIdent 
    ' 
    Me.mnuColVisibleFaultIdent.Checked = True 
    Me.mnuColVisibleFaultIdent.CheckOnClick = True 
    Me.mnuColVisibleFaultIdent.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultIdent.Name = "mnuColVisibleFaultIdent" 
    Me.mnuColVisibleFaultIdent.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultIdent.Text = "Fault Ident" 
    ' 
    'colFaultDescription
    '
    Me.colFaultDescription.DataPropertyName = "FaultDescription"
    Me.colFaultDescription.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFaultDescription.HeaderText = "Fault Description"
    Me.colFaultDescription.Name = "colFaultDescription"
    Me.colFaultDescription.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultDescription.Width = 60
    Me.colFaultDescription.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultDescription 
    ' 
    Me.mnuColVisibleFaultDescription.Checked = True 
    Me.mnuColVisibleFaultDescription.CheckOnClick = True 
    Me.mnuColVisibleFaultDescription.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultDescription.Name = "mnuColVisibleFaultDescription" 
    Me.mnuColVisibleFaultDescription.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultDescription.Text = "Fault Description" 
    ' 
    'colMessageSentToUser
    '
    Me.colMessageSentToUser.DataPropertyName = "MessageSentToUser"
    Me.colMessageSentToUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colMessageSentToUser.HeaderText = "Message Sent To User"
    Me.colMessageSentToUser.Name = "colMessageSentToUser"
    Me.colMessageSentToUser.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colMessageSentToUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colMessageSentToUser.Width = 60
    Me.colMessageSentToUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleMessageSentToUser 
    ' 
    Me.mnuColVisibleMessageSentToUser.Checked = True 
    Me.mnuColVisibleMessageSentToUser.CheckOnClick = True 
    Me.mnuColVisibleMessageSentToUser.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleMessageSentToUser.Name = "mnuColVisibleMessageSentToUser" 
    Me.mnuColVisibleMessageSentToUser.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleMessageSentToUser.Text = "Message Sent To User" 
    ' 
    'colActionSentToUser
    '
    Me.colActionSentToUser.DataPropertyName = "ActionSentToUser"
    Me.colActionSentToUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colActionSentToUser.HeaderText = "Action Sent To User"
    Me.colActionSentToUser.Name = "colActionSentToUser"
    Me.colActionSentToUser.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colActionSentToUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colActionSentToUser.Width = 60
    Me.colActionSentToUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleActionSentToUser 
    ' 
    Me.mnuColVisibleActionSentToUser.Checked = True 
    Me.mnuColVisibleActionSentToUser.CheckOnClick = True 
    Me.mnuColVisibleActionSentToUser.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleActionSentToUser.Name = "mnuColVisibleActionSentToUser" 
    Me.mnuColVisibleActionSentToUser.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleActionSentToUser.Text = "Action Sent To User" 
    ' 
    'colFaultType
    '
    Me.colFaultType.DataPropertyName = "FaultType"
    Me.colFaultType.DataSource = Me.bsFaultType
    Me.colFaultType.DisplayMember = "Text"
    Me.colFaultType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colFaultType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colFaultType.HeaderText = "Fault Type"
    Me.colFaultType.Name = "colFaultType"
    Me.colFaultType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultType.ValueMember = "KeyEnum"
    Me.colFaultType.Width = 60
    Me.colFaultType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultType 
    ' 
    Me.mnuColVisibleFaultType.Checked = True 
    Me.mnuColVisibleFaultType.CheckOnClick = True 
    Me.mnuColVisibleFaultType.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultType.Name = "mnuColVisibleFaultType" 
    Me.mnuColVisibleFaultType.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultType.Text = "Fault Type" 
    ' 
    'colFaultSeverity
    '
    Me.colFaultSeverity.DataPropertyName = "FaultSeverity"
    Me.colFaultSeverity.DataSource = Me.bsFaultSeverity
    Me.colFaultSeverity.DisplayMember = "Text"
    Me.colFaultSeverity.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colFaultSeverity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colFaultSeverity.HeaderText = "Fault Severity"
    Me.colFaultSeverity.Name = "colFaultSeverity"
    Me.colFaultSeverity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFaultSeverity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFaultSeverity.ValueMember = "KeyEnum"
    Me.colFaultSeverity.Width = 60
    Me.colFaultSeverity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFaultSeverity 
    ' 
    Me.mnuColVisibleFaultSeverity.Checked = True 
    Me.mnuColVisibleFaultSeverity.CheckOnClick = True 
    Me.mnuColVisibleFaultSeverity.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFaultSeverity.Name = "mnuColVisibleFaultSeverity" 
    Me.mnuColVisibleFaultSeverity.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFaultSeverity.Text = "Fault Severity" 
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
    'colUserIdentityType
    '
    Me.colUserIdentityType.DataPropertyName = "UserIdentityTypeCode"
    Me.colUserIdentityType.DataSource = Me.bsUserIdentityType
    Me.colUserIdentityType.DisplayMember = "Text"
    Me.colUserIdentityType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colUserIdentityType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colUserIdentityType.HeaderText = "User Identity Type"
    Me.colUserIdentityType.Name = "colUserIdentityType"
    Me.colUserIdentityType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUserIdentityType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUserIdentityType.ValueMember = "KeyString"
    Me.colUserIdentityType.Width = 60
    Me.colUserIdentityType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUserIdentityType 
    ' 
    Me.mnuColVisibleUserIdentityType.Checked = True 
    Me.mnuColVisibleUserIdentityType.CheckOnClick = True 
    Me.mnuColVisibleUserIdentityType.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUserIdentityType.Name = "mnuColVisibleUserIdentityType" 
    Me.mnuColVisibleUserIdentityType.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUserIdentityType.Text = "User Identity Type" 
    ' 
    'colUserIdentityTypeName
    '
    Me.colUserIdentityTypeName.DataPropertyName = "UserIdentityTypeNameCode"
    Me.colUserIdentityTypeName.DataSource = Me.bsUserIdentityTypeName
    Me.colUserIdentityTypeName.DisplayMember = "Text"
    Me.colUserIdentityTypeName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colUserIdentityTypeName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colUserIdentityTypeName.HeaderText = "User Identity Type Name"
    Me.colUserIdentityTypeName.Name = "colUserIdentityTypeName"
    Me.colUserIdentityTypeName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUserIdentityTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUserIdentityTypeName.ValueMember = "KeyInteger"
    Me.colUserIdentityTypeName.Width = 60
    Me.colUserIdentityTypeName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUserIdentityTypeName 
    ' 
    Me.mnuColVisibleUserIdentityTypeName.Checked = True 
    Me.mnuColVisibleUserIdentityTypeName.CheckOnClick = True 
    Me.mnuColVisibleUserIdentityTypeName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUserIdentityTypeName.Name = "mnuColVisibleUserIdentityTypeName" 
    Me.mnuColVisibleUserIdentityTypeName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUserIdentityTypeName.Text = "User Identity Type Name" 
    ' 
    'colDateOccurred
    '
    Me.colDateOccurred.DataPropertyName = "DateOccurred"
    Me.colDateOccurred.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colDateOccurred.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDateOccurred.HeaderText = "Date Occurred"
    Me.colDateOccurred.Name = "colDateOccurred"
    Me.colDateOccurred.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDateOccurred.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDateOccurred.Width = 60
    Me.colDateOccurred.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDateOccurred 
    ' 
    Me.mnuColVisibleDateOccurred.Checked = True 
    Me.mnuColVisibleDateOccurred.CheckOnClick = True 
    Me.mnuColVisibleDateOccurred.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDateOccurred.Name = "mnuColVisibleDateOccurred" 
    Me.mnuColVisibleDateOccurred.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDateOccurred.Text = "Date Occurred" 
    ' 
    'colMonthOccurred
    '
    Me.colMonthOccurred.DataPropertyName = "MonthOccurred"
    Me.colMonthOccurred.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colMonthOccurred.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colMonthOccurred.HeaderText = "Month Occurred"
    Me.colMonthOccurred.Name = "colMonthOccurred"
    Me.colMonthOccurred.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colMonthOccurred.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colMonthOccurred.Width = 60
    Me.colMonthOccurred.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleMonthOccurred 
    ' 
    Me.mnuColVisibleMonthOccurred.Checked = True 
    Me.mnuColVisibleMonthOccurred.CheckOnClick = True 
    Me.mnuColVisibleMonthOccurred.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleMonthOccurred.Name = "mnuColVisibleMonthOccurred" 
    Me.mnuColVisibleMonthOccurred.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleMonthOccurred.Text = "Month Occurred" 
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
    'ctlLoggedAlertCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvLoggedAlert)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_LoggedAlertCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvLoggedAlert, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsAffectedUser, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsFaultType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsFaultSeverity, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsLoggedLogin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsUserIdentityType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsUserIdentityTypeName, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlLoggedAlert, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvLoggedAlert As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlLoggedAlert As System.Windows.Forms.BindingSource
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
  Friend WithEvents colTimeOccurred As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTimeOccurred As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFaultNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFaultNumber As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSystemName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSystemName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCallingApplication As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCallingApplication As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsAffectedUser As System.Windows.Forms.BindingSource
  Friend WithEvents colAffectedUser As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colAffectedUserText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleAffectedUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCallingApplicationVersion As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCallingApplicationVersion As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCallingFunctionWithinApplication As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCallingFunctionWithinApplication As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFreeText As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFreeText As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFaultingAssembly As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFaultingAssembly As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAssemblyEntryPoint As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAssemblyEntryPoint As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFaultingClass As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFaultingClass As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFaultingFunction As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFaultingFunction As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFaultingFunctionParameters As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFaultingFunctionParameters As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFaultIdent As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFaultIdent As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFaultDescription As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFaultDescription As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colMessageSentToUser As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleMessageSentToUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colActionSentToUser As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleActionSentToUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsFaultType As System.Windows.Forms.BindingSource
  Friend WithEvents colFaultType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleFaultType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsFaultSeverity As System.Windows.Forms.BindingSource
  Friend WithEvents colFaultSeverity As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleFaultSeverity As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsLoggedLogin As System.Windows.Forms.BindingSource
  Friend WithEvents colLoggedLogin As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colLoggedLoginText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleLoggedLogin As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colThread As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleThread As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsUserIdentityType As System.Windows.Forms.BindingSource
  Friend WithEvents colUserIdentityType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleUserIdentityType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsUserIdentityTypeName As System.Windows.Forms.BindingSource
  Friend WithEvents colUserIdentityTypeName As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleUserIdentityTypeName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDateOccurred As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDateOccurred As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colMonthOccurred As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleMonthOccurred As System.Windows.Forms.ToolStripMenuItem 

End Class
