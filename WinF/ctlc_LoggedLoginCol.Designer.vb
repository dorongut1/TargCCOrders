<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_LoggedLoginCol
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
    Me.dgvLoggedLogin = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlLoggedLogin = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colUserName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUserName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colUserFullName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUserFullName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTimeLoggedIn = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTimeLoggedIn = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colApplicationName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleApplicationName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsUserIdentityType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colUserIdentityType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleUserIdentityType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsUserIdentityTypeName = New System.Windows.Forms.BindingSource(Me.components)
    Me.colUserIdentityTypeName = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleUserIdentityTypeName = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colRoles = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleRoles = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTimeLoggedOut = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTimeLoggedOut = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLoginFaultNumber = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLoginFaultNumber = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colEnvironmentUserName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleEnvironmentUserName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colEnvironmentMachineName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleEnvironmentMachineName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colEnvironmentUserDomainName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleEnvironmentUserDomainName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDnsGetHostName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDnsGetHostName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAddressList = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAddressList = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colComputerMACAddress = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleComputerMACAddress = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSystemDiskVolumeSerialNo = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSystemDiskVolumeSerialNo = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLocalTime = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLocalTime = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colGmtTime = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleGmtTime = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAccessingComputerDetails = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAccessingComputerDetails = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colUICulture = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUICulture = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTotalPhysicalMemoryKb = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTotalPhysicalMemoryKb = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAvailablePhysicalMemoryKb = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAvailablePhysicalMemoryKb = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colApplicationVersion = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleApplicationVersion = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colOriginatingIP = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOriginatingIP = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsLanguage = New System.Windows.Forms.BindingSource(Me.components)
    Me.colLanguage = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleLanguage = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colHostingAssembly = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleHostingAssembly = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colOriginatingCountry = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOriginatingCountry = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDateLoggedIn = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDateLoggedIn = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colMonthLoggedIn = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleMonthLoggedIn = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colClientReportedIP = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleClientReportedIP = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colClientReportedCountry = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleClientReportedCountry = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colIPAdditionalDetails = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleIPAdditionalDetails = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvLoggedLogin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsUserIdentityType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsUserIdentityTypeName, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsLanguage, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlLoggedLogin, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvLoggedLogin
    '
    Me.dgvLoggedLogin.AllowUserToAddRows = False
    Me.dgvLoggedLogin.AllowUserToDeleteRows = False
    Me.dgvLoggedLogin.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvLoggedLogin.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvLoggedLogin.AutoGenerateColumns = False
    Me.dgvLoggedLogin.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvLoggedLogin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvLoggedLogin.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvLoggedLogin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvLoggedLogin.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colUserName, Me.colUserFullName, Me.colTimeLoggedIn, Me.colApplicationName, Me.colUserIdentityType, Me.colUserIdentityTypeName, Me.colRoles, Me.colTimeLoggedOut, Me.colLoginFaultNumber, Me.colEnvironmentUserName, Me.colEnvironmentMachineName, Me.colEnvironmentUserDomainName, Me.colDnsGetHostName, Me.colAddressList, Me.colComputerMACAddress, Me.colSystemDiskVolumeSerialNo, Me.colLocalTime, Me.colGmtTime, Me.colAccessingComputerDetails, Me.colUICulture, Me.colTotalPhysicalMemoryKb, Me.colAvailablePhysicalMemoryKb, Me.colApplicationVersion, Me.colOriginatingIP, Me.colLanguage, Me.colHostingAssembly, Me.colOriginatingCountry, Me.colDateLoggedIn, Me.colMonthLoggedIn, Me.colClientReportedIP, Me.colClientReportedCountry, Me.colIPAdditionalDetails})
    Me.dgvLoggedLogin.DataSource = Me.bsCtlLoggedLogin
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvLoggedLogin.DefaultCellStyle = styleDefaultCell
    Me.dgvLoggedLogin.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvLoggedLogin.EnableHeadersVisualStyles = False
    Me.dgvLoggedLogin.Location = New System.Drawing.Point(0, 25)
    Me.dgvLoggedLogin.MultiSelect = False 
    Me.dgvLoggedLogin.ContextMenuStrip = Me.cmsGrid 
    Me.dgvLoggedLogin.Name = "dgvLoggedLogin"
    Me.dgvLoggedLogin.RowHeadersVisible = False
    Me.dgvLoggedLogin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvLoggedLogin.Size = New System.Drawing.Size(712, 347)
    Me.dgvLoggedLogin.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlLoggedLogin
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleUserName, Me.mnuColVisibleUserFullName, Me.mnuColVisibleTimeLoggedIn, Me.mnuColVisibleApplicationName, Me.mnuColVisibleUserIdentityType, Me.mnuColVisibleUserIdentityTypeName, Me.mnuColVisibleRoles, Me.mnuColVisibleTimeLoggedOut, Me.mnuColVisibleLoginFaultNumber, Me.mnuColVisibleEnvironmentUserName, Me.mnuColVisibleEnvironmentMachineName, Me.mnuColVisibleEnvironmentUserDomainName, Me.mnuColVisibleDnsGetHostName, Me.mnuColVisibleAddressList, Me.mnuColVisibleComputerMACAddress, Me.mnuColVisibleSystemDiskVolumeSerialNo, Me.mnuColVisibleLocalTime, Me.mnuColVisibleGmtTime, Me.mnuColVisibleAccessingComputerDetails, Me.mnuColVisibleUICulture, Me.mnuColVisibleTotalPhysicalMemoryKb, Me.mnuColVisibleAvailablePhysicalMemoryKb, Me.mnuColVisibleApplicationVersion, Me.mnuColVisibleOriginatingIP, Me.mnuColVisibleLanguage, Me.mnuColVisibleHostingAssembly, Me.mnuColVisibleOriginatingCountry, Me.mnuColVisibleDateLoggedIn, Me.mnuColVisibleMonthLoggedIn, Me.mnuColVisibleClientReportedIP, Me.mnuColVisibleClientReportedCountry, Me.mnuColVisibleIPAdditionalDetails, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsUserIdentityType
    Me.bsUserIdentityType.DataSource = GetType(clsComboList)
    'bsUserIdentityTypeName
    Me.bsUserIdentityTypeName.DataSource = GetType(clsComboList)
    'bsLanguage
    Me.bsLanguage.DataSource = GetType(clsComboList)
    '
    'bsCtlLoggedLogin
    '
    Me.bsCtlLoggedLogin.DataSource = GetType(csLoggedLogin)
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
    'colUserName
    '
    Me.colUserName.DataPropertyName = "UserName"
    Me.colUserName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colUserName.HeaderText = "User Name"
    Me.colUserName.Name = "colUserName"
    Me.colUserName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUserName.Width = 60
    Me.colUserName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUserName 
    ' 
    Me.mnuColVisibleUserName.Checked = True 
    Me.mnuColVisibleUserName.CheckOnClick = True 
    Me.mnuColVisibleUserName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUserName.Name = "mnuColVisibleUserName" 
    Me.mnuColVisibleUserName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUserName.Text = "User Name" 
    ' 
    'colUserFullName
    '
    Me.colUserFullName.DataPropertyName = "UserFullName"
    Me.colUserFullName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colUserFullName.HeaderText = "User Full Name"
    Me.colUserFullName.Name = "colUserFullName"
    Me.colUserFullName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUserFullName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUserFullName.Width = 60
    Me.colUserFullName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUserFullName 
    ' 
    Me.mnuColVisibleUserFullName.Checked = True 
    Me.mnuColVisibleUserFullName.CheckOnClick = True 
    Me.mnuColVisibleUserFullName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUserFullName.Name = "mnuColVisibleUserFullName" 
    Me.mnuColVisibleUserFullName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUserFullName.Text = "User Full Name" 
    ' 
    'colTimeLoggedIn
    '
    Me.colTimeLoggedIn.DataPropertyName = "TimeLoggedIn"
    Me.colTimeLoggedIn.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colTimeLoggedIn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colTimeLoggedIn.HeaderText = "Time Logged In"
    Me.colTimeLoggedIn.Name = "colTimeLoggedIn"
    Me.colTimeLoggedIn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTimeLoggedIn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTimeLoggedIn.Width = 60
    Me.colTimeLoggedIn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTimeLoggedIn 
    ' 
    Me.mnuColVisibleTimeLoggedIn.Checked = True 
    Me.mnuColVisibleTimeLoggedIn.CheckOnClick = True 
    Me.mnuColVisibleTimeLoggedIn.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTimeLoggedIn.Name = "mnuColVisibleTimeLoggedIn" 
    Me.mnuColVisibleTimeLoggedIn.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTimeLoggedIn.Text = "Time Logged In" 
    ' 
    'colApplicationName
    '
    Me.colApplicationName.DataPropertyName = "ApplicationName"
    Me.colApplicationName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colApplicationName.HeaderText = "Application Name"
    Me.colApplicationName.Name = "colApplicationName"
    Me.colApplicationName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colApplicationName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colApplicationName.Width = 60
    Me.colApplicationName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleApplicationName 
    ' 
    Me.mnuColVisibleApplicationName.Checked = True 
    Me.mnuColVisibleApplicationName.CheckOnClick = True 
    Me.mnuColVisibleApplicationName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleApplicationName.Name = "mnuColVisibleApplicationName" 
    Me.mnuColVisibleApplicationName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleApplicationName.Text = "Application Name" 
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
    'colRoles
    '
    Me.colRoles.DataPropertyName = "Roles"
    Me.colRoles.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colRoles.HeaderText = "Roles"
    Me.colRoles.Name = "colRoles"
    Me.colRoles.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRoles.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRoles.Width = 60
    Me.colRoles.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRoles 
    ' 
    Me.mnuColVisibleRoles.Checked = True 
    Me.mnuColVisibleRoles.CheckOnClick = True 
    Me.mnuColVisibleRoles.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRoles.Name = "mnuColVisibleRoles" 
    Me.mnuColVisibleRoles.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRoles.Text = "Roles" 
    ' 
    'colTimeLoggedOut
    '
    Me.colTimeLoggedOut.DataPropertyName = "TimeLoggedOut"
    Me.colTimeLoggedOut.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colTimeLoggedOut.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colTimeLoggedOut.HeaderText = "Time Logged Out"
    Me.colTimeLoggedOut.Name = "colTimeLoggedOut"
    Me.colTimeLoggedOut.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTimeLoggedOut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTimeLoggedOut.Width = 60
    Me.colTimeLoggedOut.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTimeLoggedOut 
    ' 
    Me.mnuColVisibleTimeLoggedOut.Checked = True 
    Me.mnuColVisibleTimeLoggedOut.CheckOnClick = True 
    Me.mnuColVisibleTimeLoggedOut.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTimeLoggedOut.Name = "mnuColVisibleTimeLoggedOut" 
    Me.mnuColVisibleTimeLoggedOut.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTimeLoggedOut.Text = "Time Logged Out" 
    ' 
    'colLoginFaultNumber
    '
    Me.colLoginFaultNumber.DataPropertyName = "LoginFaultNumber"
    Me.colLoginFaultNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colLoginFaultNumber.HeaderText = "Login Fault Number"
    Me.colLoginFaultNumber.Name = "colLoginFaultNumber"
    Me.colLoginFaultNumber.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLoginFaultNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLoginFaultNumber.Width = 60
    Me.colLoginFaultNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLoginFaultNumber 
    ' 
    Me.mnuColVisibleLoginFaultNumber.Checked = True 
    Me.mnuColVisibleLoginFaultNumber.CheckOnClick = True 
    Me.mnuColVisibleLoginFaultNumber.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLoginFaultNumber.Name = "mnuColVisibleLoginFaultNumber" 
    Me.mnuColVisibleLoginFaultNumber.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLoginFaultNumber.Text = "Login Fault Number" 
    ' 
    'colEnvironmentUserName
    '
    Me.colEnvironmentUserName.DataPropertyName = "EnvironmentUserName"
    Me.colEnvironmentUserName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colEnvironmentUserName.HeaderText = "Environment User Name"
    Me.colEnvironmentUserName.Name = "colEnvironmentUserName"
    Me.colEnvironmentUserName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEnvironmentUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEnvironmentUserName.Width = 60
    Me.colEnvironmentUserName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEnvironmentUserName 
    ' 
    Me.mnuColVisibleEnvironmentUserName.Checked = True 
    Me.mnuColVisibleEnvironmentUserName.CheckOnClick = True 
    Me.mnuColVisibleEnvironmentUserName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEnvironmentUserName.Name = "mnuColVisibleEnvironmentUserName" 
    Me.mnuColVisibleEnvironmentUserName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEnvironmentUserName.Text = "Environment User Name" 
    ' 
    'colEnvironmentMachineName
    '
    Me.colEnvironmentMachineName.DataPropertyName = "EnvironmentMachineName"
    Me.colEnvironmentMachineName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colEnvironmentMachineName.HeaderText = "Environment Machine Name"
    Me.colEnvironmentMachineName.Name = "colEnvironmentMachineName"
    Me.colEnvironmentMachineName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEnvironmentMachineName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEnvironmentMachineName.Width = 60
    Me.colEnvironmentMachineName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEnvironmentMachineName 
    ' 
    Me.mnuColVisibleEnvironmentMachineName.Checked = True 
    Me.mnuColVisibleEnvironmentMachineName.CheckOnClick = True 
    Me.mnuColVisibleEnvironmentMachineName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEnvironmentMachineName.Name = "mnuColVisibleEnvironmentMachineName" 
    Me.mnuColVisibleEnvironmentMachineName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEnvironmentMachineName.Text = "Environment Machine Name" 
    ' 
    'colEnvironmentUserDomainName
    '
    Me.colEnvironmentUserDomainName.DataPropertyName = "EnvironmentUserDomainName"
    Me.colEnvironmentUserDomainName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colEnvironmentUserDomainName.HeaderText = "Environment User Domain Name"
    Me.colEnvironmentUserDomainName.Name = "colEnvironmentUserDomainName"
    Me.colEnvironmentUserDomainName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEnvironmentUserDomainName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEnvironmentUserDomainName.Width = 60
    Me.colEnvironmentUserDomainName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEnvironmentUserDomainName 
    ' 
    Me.mnuColVisibleEnvironmentUserDomainName.Checked = True 
    Me.mnuColVisibleEnvironmentUserDomainName.CheckOnClick = True 
    Me.mnuColVisibleEnvironmentUserDomainName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEnvironmentUserDomainName.Name = "mnuColVisibleEnvironmentUserDomainName" 
    Me.mnuColVisibleEnvironmentUserDomainName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEnvironmentUserDomainName.Text = "Environment User Domain Name" 
    ' 
    'colDnsGetHostName
    '
    Me.colDnsGetHostName.DataPropertyName = "DnsGetHostName"
    Me.colDnsGetHostName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colDnsGetHostName.HeaderText = "Dns Get Host Name"
    Me.colDnsGetHostName.Name = "colDnsGetHostName"
    Me.colDnsGetHostName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDnsGetHostName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDnsGetHostName.Width = 60
    Me.colDnsGetHostName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDnsGetHostName 
    ' 
    Me.mnuColVisibleDnsGetHostName.Checked = True 
    Me.mnuColVisibleDnsGetHostName.CheckOnClick = True 
    Me.mnuColVisibleDnsGetHostName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDnsGetHostName.Name = "mnuColVisibleDnsGetHostName" 
    Me.mnuColVisibleDnsGetHostName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDnsGetHostName.Text = "Dns Get Host Name" 
    ' 
    'colAddressList
    '
    Me.colAddressList.DataPropertyName = "AddressList"
    Me.colAddressList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colAddressList.HeaderText = "Address List"
    Me.colAddressList.Name = "colAddressList"
    Me.colAddressList.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAddressList.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAddressList.Width = 60
    Me.colAddressList.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAddressList 
    ' 
    Me.mnuColVisibleAddressList.Checked = True 
    Me.mnuColVisibleAddressList.CheckOnClick = True 
    Me.mnuColVisibleAddressList.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAddressList.Name = "mnuColVisibleAddressList" 
    Me.mnuColVisibleAddressList.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAddressList.Text = "Address List" 
    ' 
    'colComputerMACAddress
    '
    Me.colComputerMACAddress.DataPropertyName = "ComputerMACAddress"
    Me.colComputerMACAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colComputerMACAddress.HeaderText = "Computer MAC Address"
    Me.colComputerMACAddress.Name = "colComputerMACAddress"
    Me.colComputerMACAddress.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colComputerMACAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colComputerMACAddress.Width = 60
    Me.colComputerMACAddress.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleComputerMACAddress 
    ' 
    Me.mnuColVisibleComputerMACAddress.Checked = True 
    Me.mnuColVisibleComputerMACAddress.CheckOnClick = True 
    Me.mnuColVisibleComputerMACAddress.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleComputerMACAddress.Name = "mnuColVisibleComputerMACAddress" 
    Me.mnuColVisibleComputerMACAddress.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleComputerMACAddress.Text = "Computer MAC Address" 
    ' 
    'colSystemDiskVolumeSerialNo
    '
    Me.colSystemDiskVolumeSerialNo.DataPropertyName = "SystemDiskVolumeSerialNo"
    Me.colSystemDiskVolumeSerialNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSystemDiskVolumeSerialNo.HeaderText = "System Disk Volume Serial No"
    Me.colSystemDiskVolumeSerialNo.Name = "colSystemDiskVolumeSerialNo"
    Me.colSystemDiskVolumeSerialNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSystemDiskVolumeSerialNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSystemDiskVolumeSerialNo.Width = 60
    Me.colSystemDiskVolumeSerialNo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSystemDiskVolumeSerialNo 
    ' 
    Me.mnuColVisibleSystemDiskVolumeSerialNo.Checked = True 
    Me.mnuColVisibleSystemDiskVolumeSerialNo.CheckOnClick = True 
    Me.mnuColVisibleSystemDiskVolumeSerialNo.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSystemDiskVolumeSerialNo.Name = "mnuColVisibleSystemDiskVolumeSerialNo" 
    Me.mnuColVisibleSystemDiskVolumeSerialNo.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSystemDiskVolumeSerialNo.Text = "System Disk Volume Serial No" 
    ' 
    'colLocalTime
    '
    Me.colLocalTime.DataPropertyName = "LocalTime"
    Me.colLocalTime.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colLocalTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colLocalTime.HeaderText = "Local Time"
    Me.colLocalTime.Name = "colLocalTime"
    Me.colLocalTime.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLocalTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLocalTime.Width = 60
    Me.colLocalTime.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLocalTime 
    ' 
    Me.mnuColVisibleLocalTime.Checked = True 
    Me.mnuColVisibleLocalTime.CheckOnClick = True 
    Me.mnuColVisibleLocalTime.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLocalTime.Name = "mnuColVisibleLocalTime" 
    Me.mnuColVisibleLocalTime.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLocalTime.Text = "Local Time" 
    ' 
    'colGmtTime
    '
    Me.colGmtTime.DataPropertyName = "GmtTime"
    Me.colGmtTime.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colGmtTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colGmtTime.HeaderText = "Gmt Time"
    Me.colGmtTime.Name = "colGmtTime"
    Me.colGmtTime.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colGmtTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colGmtTime.Width = 60
    Me.colGmtTime.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleGmtTime 
    ' 
    Me.mnuColVisibleGmtTime.Checked = True 
    Me.mnuColVisibleGmtTime.CheckOnClick = True 
    Me.mnuColVisibleGmtTime.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleGmtTime.Name = "mnuColVisibleGmtTime" 
    Me.mnuColVisibleGmtTime.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleGmtTime.Text = "Gmt Time" 
    ' 
    'colAccessingComputerDetails
    '
    Me.colAccessingComputerDetails.DataPropertyName = "AccessingComputerDetails"
    Me.colAccessingComputerDetails.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colAccessingComputerDetails.HeaderText = "Accessing Computer Details"
    Me.colAccessingComputerDetails.Name = "colAccessingComputerDetails"
    Me.colAccessingComputerDetails.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAccessingComputerDetails.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAccessingComputerDetails.Width = 60
    Me.colAccessingComputerDetails.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAccessingComputerDetails 
    ' 
    Me.mnuColVisibleAccessingComputerDetails.Checked = True 
    Me.mnuColVisibleAccessingComputerDetails.CheckOnClick = True 
    Me.mnuColVisibleAccessingComputerDetails.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAccessingComputerDetails.Name = "mnuColVisibleAccessingComputerDetails" 
    Me.mnuColVisibleAccessingComputerDetails.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAccessingComputerDetails.Text = "Accessing Computer Details" 
    ' 
    'colUICulture
    '
    Me.colUICulture.DataPropertyName = "UICulture"
    Me.colUICulture.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colUICulture.HeaderText = "UI Culture"
    Me.colUICulture.Name = "colUICulture"
    Me.colUICulture.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUICulture.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUICulture.Width = 60
    Me.colUICulture.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUICulture 
    ' 
    Me.mnuColVisibleUICulture.Checked = True 
    Me.mnuColVisibleUICulture.CheckOnClick = True 
    Me.mnuColVisibleUICulture.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUICulture.Name = "mnuColVisibleUICulture" 
    Me.mnuColVisibleUICulture.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUICulture.Text = "UI Culture" 
    ' 
    'colTotalPhysicalMemoryKb
    '
    Me.colTotalPhysicalMemoryKb.DataPropertyName = "TotalPhysicalMemoryKb"
    Me.colTotalPhysicalMemoryKb.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colTotalPhysicalMemoryKb.HeaderText = "Total Physical Memory Kb"
    Me.colTotalPhysicalMemoryKb.Name = "colTotalPhysicalMemoryKb"
    Me.colTotalPhysicalMemoryKb.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTotalPhysicalMemoryKb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTotalPhysicalMemoryKb.Width = 60
    Me.colTotalPhysicalMemoryKb.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTotalPhysicalMemoryKb 
    ' 
    Me.mnuColVisibleTotalPhysicalMemoryKb.Checked = True 
    Me.mnuColVisibleTotalPhysicalMemoryKb.CheckOnClick = True 
    Me.mnuColVisibleTotalPhysicalMemoryKb.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTotalPhysicalMemoryKb.Name = "mnuColVisibleTotalPhysicalMemoryKb" 
    Me.mnuColVisibleTotalPhysicalMemoryKb.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTotalPhysicalMemoryKb.Text = "Total Physical Memory Kb" 
    ' 
    'colAvailablePhysicalMemoryKb
    '
    Me.colAvailablePhysicalMemoryKb.DataPropertyName = "AvailablePhysicalMemoryKb"
    Me.colAvailablePhysicalMemoryKb.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colAvailablePhysicalMemoryKb.HeaderText = "Available Physical Memory Kb"
    Me.colAvailablePhysicalMemoryKb.Name = "colAvailablePhysicalMemoryKb"
    Me.colAvailablePhysicalMemoryKb.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAvailablePhysicalMemoryKb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAvailablePhysicalMemoryKb.Width = 60
    Me.colAvailablePhysicalMemoryKb.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAvailablePhysicalMemoryKb 
    ' 
    Me.mnuColVisibleAvailablePhysicalMemoryKb.Checked = True 
    Me.mnuColVisibleAvailablePhysicalMemoryKb.CheckOnClick = True 
    Me.mnuColVisibleAvailablePhysicalMemoryKb.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAvailablePhysicalMemoryKb.Name = "mnuColVisibleAvailablePhysicalMemoryKb" 
    Me.mnuColVisibleAvailablePhysicalMemoryKb.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAvailablePhysicalMemoryKb.Text = "Available Physical Memory Kb" 
    ' 
    'colApplicationVersion
    '
    Me.colApplicationVersion.DataPropertyName = "ApplicationVersion"
    Me.colApplicationVersion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colApplicationVersion.HeaderText = "Application Version"
    Me.colApplicationVersion.Name = "colApplicationVersion"
    Me.colApplicationVersion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colApplicationVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colApplicationVersion.Width = 60
    Me.colApplicationVersion.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleApplicationVersion 
    ' 
    Me.mnuColVisibleApplicationVersion.Checked = True 
    Me.mnuColVisibleApplicationVersion.CheckOnClick = True 
    Me.mnuColVisibleApplicationVersion.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleApplicationVersion.Name = "mnuColVisibleApplicationVersion" 
    Me.mnuColVisibleApplicationVersion.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleApplicationVersion.Text = "Application Version" 
    ' 
    'colOriginatingIP
    '
    Me.colOriginatingIP.DataPropertyName = "OriginatingIP"
    Me.colOriginatingIP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colOriginatingIP.HeaderText = "Originating IP"
    Me.colOriginatingIP.Name = "colOriginatingIP"
    Me.colOriginatingIP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOriginatingIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOriginatingIP.Width = 60
    Me.colOriginatingIP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOriginatingIP 
    ' 
    Me.mnuColVisibleOriginatingIP.Checked = True 
    Me.mnuColVisibleOriginatingIP.CheckOnClick = True 
    Me.mnuColVisibleOriginatingIP.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOriginatingIP.Name = "mnuColVisibleOriginatingIP" 
    Me.mnuColVisibleOriginatingIP.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOriginatingIP.Text = "Originating IP" 
    ' 
    'colLanguage
    '
    Me.colLanguage.DataPropertyName = "Language"
    Me.colLanguage.DataSource = Me.bsLanguage
    Me.colLanguage.DisplayMember = "Text"
    Me.colLanguage.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colLanguage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colLanguage.HeaderText = "Language"
    Me.colLanguage.Name = "colLanguage"
    Me.colLanguage.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLanguage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLanguage.ValueMember = "KeyEnum"
    Me.colLanguage.Width = 60
    Me.colLanguage.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLanguage 
    ' 
    Me.mnuColVisibleLanguage.Checked = True 
    Me.mnuColVisibleLanguage.CheckOnClick = True 
    Me.mnuColVisibleLanguage.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLanguage.Name = "mnuColVisibleLanguage" 
    Me.mnuColVisibleLanguage.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLanguage.Text = "Language" 
    ' 
    'colHostingAssembly
    '
    Me.colHostingAssembly.DataPropertyName = "HostingAssembly"
    Me.colHostingAssembly.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colHostingAssembly.HeaderText = "Hosting Assembly"
    Me.colHostingAssembly.Name = "colHostingAssembly"
    Me.colHostingAssembly.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colHostingAssembly.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colHostingAssembly.Width = 60
    Me.colHostingAssembly.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleHostingAssembly 
    ' 
    Me.mnuColVisibleHostingAssembly.Checked = True 
    Me.mnuColVisibleHostingAssembly.CheckOnClick = True 
    Me.mnuColVisibleHostingAssembly.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleHostingAssembly.Name = "mnuColVisibleHostingAssembly" 
    Me.mnuColVisibleHostingAssembly.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleHostingAssembly.Text = "Hosting Assembly" 
    ' 
    'colOriginatingCountry
    '
    Me.colOriginatingCountry.DataPropertyName = "OriginatingCountry"
    Me.colOriginatingCountry.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colOriginatingCountry.HeaderText = "Originating Country"
    Me.colOriginatingCountry.Name = "colOriginatingCountry"
    Me.colOriginatingCountry.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOriginatingCountry.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOriginatingCountry.Width = 60
    Me.colOriginatingCountry.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOriginatingCountry 
    ' 
    Me.mnuColVisibleOriginatingCountry.Checked = True 
    Me.mnuColVisibleOriginatingCountry.CheckOnClick = True 
    Me.mnuColVisibleOriginatingCountry.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOriginatingCountry.Name = "mnuColVisibleOriginatingCountry" 
    Me.mnuColVisibleOriginatingCountry.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOriginatingCountry.Text = "Originating Country" 
    ' 
    'colDateLoggedIn
    '
    Me.colDateLoggedIn.DataPropertyName = "DateLoggedIn"
    Me.colDateLoggedIn.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colDateLoggedIn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDateLoggedIn.HeaderText = "Date Logged In"
    Me.colDateLoggedIn.Name = "colDateLoggedIn"
    Me.colDateLoggedIn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDateLoggedIn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDateLoggedIn.Width = 60
    Me.colDateLoggedIn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDateLoggedIn 
    ' 
    Me.mnuColVisibleDateLoggedIn.Checked = True 
    Me.mnuColVisibleDateLoggedIn.CheckOnClick = True 
    Me.mnuColVisibleDateLoggedIn.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDateLoggedIn.Name = "mnuColVisibleDateLoggedIn" 
    Me.mnuColVisibleDateLoggedIn.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDateLoggedIn.Text = "Date Logged In" 
    ' 
    'colMonthLoggedIn
    '
    Me.colMonthLoggedIn.DataPropertyName = "MonthLoggedIn"
    Me.colMonthLoggedIn.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colMonthLoggedIn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colMonthLoggedIn.HeaderText = "Month Logged In"
    Me.colMonthLoggedIn.Name = "colMonthLoggedIn"
    Me.colMonthLoggedIn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colMonthLoggedIn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colMonthLoggedIn.Width = 60
    Me.colMonthLoggedIn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleMonthLoggedIn 
    ' 
    Me.mnuColVisibleMonthLoggedIn.Checked = True 
    Me.mnuColVisibleMonthLoggedIn.CheckOnClick = True 
    Me.mnuColVisibleMonthLoggedIn.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleMonthLoggedIn.Name = "mnuColVisibleMonthLoggedIn" 
    Me.mnuColVisibleMonthLoggedIn.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleMonthLoggedIn.Text = "Month Logged In" 
    ' 
    'colClientReportedIP
    '
    Me.colClientReportedIP.DataPropertyName = "ClientReportedIP"
    Me.colClientReportedIP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colClientReportedIP.HeaderText = "Client Reported IP"
    Me.colClientReportedIP.Name = "colClientReportedIP"
    Me.colClientReportedIP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colClientReportedIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colClientReportedIP.Width = 60
    Me.colClientReportedIP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleClientReportedIP 
    ' 
    Me.mnuColVisibleClientReportedIP.Checked = True 
    Me.mnuColVisibleClientReportedIP.CheckOnClick = True 
    Me.mnuColVisibleClientReportedIP.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleClientReportedIP.Name = "mnuColVisibleClientReportedIP" 
    Me.mnuColVisibleClientReportedIP.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleClientReportedIP.Text = "Client Reported IP" 
    ' 
    'colClientReportedCountry
    '
    Me.colClientReportedCountry.DataPropertyName = "ClientReportedCountry"
    Me.colClientReportedCountry.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colClientReportedCountry.HeaderText = "Client Reported Country"
    Me.colClientReportedCountry.Name = "colClientReportedCountry"
    Me.colClientReportedCountry.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colClientReportedCountry.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colClientReportedCountry.Width = 60
    Me.colClientReportedCountry.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleClientReportedCountry 
    ' 
    Me.mnuColVisibleClientReportedCountry.Checked = True 
    Me.mnuColVisibleClientReportedCountry.CheckOnClick = True 
    Me.mnuColVisibleClientReportedCountry.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleClientReportedCountry.Name = "mnuColVisibleClientReportedCountry" 
    Me.mnuColVisibleClientReportedCountry.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleClientReportedCountry.Text = "Client Reported Country" 
    ' 
    'colIPAdditionalDetails
    '
    Me.colIPAdditionalDetails.DataPropertyName = "IPAdditionalDetails"
    Me.colIPAdditionalDetails.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colIPAdditionalDetails.HeaderText = "IP Additional Details"
    Me.colIPAdditionalDetails.Name = "colIPAdditionalDetails"
    Me.colIPAdditionalDetails.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIPAdditionalDetails.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIPAdditionalDetails.Width = 60
    Me.colIPAdditionalDetails.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIPAdditionalDetails 
    ' 
    Me.mnuColVisibleIPAdditionalDetails.Checked = True 
    Me.mnuColVisibleIPAdditionalDetails.CheckOnClick = True 
    Me.mnuColVisibleIPAdditionalDetails.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIPAdditionalDetails.Name = "mnuColVisibleIPAdditionalDetails" 
    Me.mnuColVisibleIPAdditionalDetails.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIPAdditionalDetails.Text = "IP Additional Details" 
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
    'ctlLoggedLoginCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvLoggedLogin)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_LoggedLoginCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvLoggedLogin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsUserIdentityType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsUserIdentityTypeName, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsLanguage, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlLoggedLogin, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvLoggedLogin As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlLoggedLogin As System.Windows.Forms.BindingSource
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
  Friend WithEvents colUserName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleUserName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colUserFullName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleUserFullName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTimeLoggedIn As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTimeLoggedIn As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colApplicationName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleApplicationName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsUserIdentityType As System.Windows.Forms.BindingSource
  Friend WithEvents colUserIdentityType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleUserIdentityType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsUserIdentityTypeName As System.Windows.Forms.BindingSource
  Friend WithEvents colUserIdentityTypeName As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleUserIdentityTypeName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colRoles As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleRoles As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTimeLoggedOut As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTimeLoggedOut As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLoginFaultNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLoginFaultNumber As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEnvironmentUserName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleEnvironmentUserName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEnvironmentMachineName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleEnvironmentMachineName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEnvironmentUserDomainName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleEnvironmentUserDomainName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDnsGetHostName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDnsGetHostName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAddressList As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAddressList As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colComputerMACAddress As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleComputerMACAddress As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSystemDiskVolumeSerialNo As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSystemDiskVolumeSerialNo As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLocalTime As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLocalTime As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colGmtTime As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleGmtTime As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAccessingComputerDetails As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAccessingComputerDetails As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colUICulture As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleUICulture As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTotalPhysicalMemoryKb As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTotalPhysicalMemoryKb As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAvailablePhysicalMemoryKb As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAvailablePhysicalMemoryKb As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colApplicationVersion As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleApplicationVersion As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colOriginatingIP As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleOriginatingIP As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsLanguage As System.Windows.Forms.BindingSource
  Friend WithEvents colLanguage As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleLanguage As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colHostingAssembly As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleHostingAssembly As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colOriginatingCountry As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleOriginatingCountry As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDateLoggedIn As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDateLoggedIn As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colMonthLoggedIn As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleMonthLoggedIn As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colClientReportedIP As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleClientReportedIP As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colClientReportedCountry As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleClientReportedCountry As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIPAdditionalDetails As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleIPAdditionalDetails As System.Windows.Forms.ToolStripMenuItem 

End Class
