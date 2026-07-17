<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_UserLoginKeyCol
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
    Me.dgvUserLoginKey = New System.Windows.Forms.DataGridView() 
    Me.BN = New System.Windows.Forms.BindingNavigator(Me.components)
    Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator() 
    Me.btnEdit = New System.Windows.Forms.ToolStripButton() 
    Me.btnImport = New System.Windows.Forms.ToolStripButton() 
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
    Me.bsCtlUserLoginKey = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsUser = New System.Windows.Forms.BindingSource(Me.components)
    Me.colUser = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colUserText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colApplicationName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleApplicationName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colApplicationIdentifier = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleApplicationIdentifier = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colKeyHashed = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleKeyHashed = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colExternalIPAtCreation = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleExternalIPAtCreation = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCountryAtCreation = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCountryAtCreation = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLastAccessTime = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastAccessTime = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLoggedLoginID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLoggedLoginID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvUserLoginKey, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsUser, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlUserLoginKey, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvUserLoginKey
    '
    Me.dgvUserLoginKey.AllowUserToAddRows = False
    Me.dgvUserLoginKey.AllowUserToDeleteRows = False
    Me.dgvUserLoginKey.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvUserLoginKey.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvUserLoginKey.AutoGenerateColumns = False
    Me.dgvUserLoginKey.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvUserLoginKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvUserLoginKey.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvUserLoginKey.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvUserLoginKey.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colUser, Me.colUserText, Me.colApplicationName, Me.colApplicationIdentifier, Me.colKeyHashed, Me.colExternalIPAtCreation, Me.colCountryAtCreation, Me.colLastAccessTime, Me.colLoggedLoginID})
    Me.dgvUserLoginKey.DataSource = Me.bsCtlUserLoginKey
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvUserLoginKey.DefaultCellStyle = styleDefaultCell
    Me.dgvUserLoginKey.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvUserLoginKey.EnableHeadersVisualStyles = False
    Me.dgvUserLoginKey.Location = New System.Drawing.Point(0, 25)
    Me.dgvUserLoginKey.MultiSelect = False 
    Me.dgvUserLoginKey.ContextMenuStrip = Me.cmsGrid 
    Me.dgvUserLoginKey.Name = "dgvUserLoginKey"
    Me.dgvUserLoginKey.RowHeadersVisible = False
    Me.dgvUserLoginKey.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvUserLoginKey.Size = New System.Drawing.Size(712, 347)
    Me.dgvUserLoginKey.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlUserLoginKey
    Me.BN.CountItem = Nothing
    Me.BN.DeleteItem = Nothing
    Me.BN.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.BN.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorSeparator, Me.btnEdit, Me.btnDelete, Me.btnImport, Me.btnCeaseEdit, Me.tssEditMode, Me.lblEditMode, Me.tssReports, Me.btnSpreadsheet, Me.lblStatus, Me.btnReport, Me.tssColumns, Me.btnColumns})
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleUser, Me.mnuColVisibleApplicationName, Me.mnuColVisibleApplicationIdentifier, Me.mnuColVisibleKeyHashed, Me.mnuColVisibleExternalIPAtCreation, Me.mnuColVisibleCountryAtCreation, Me.mnuColVisibleLastAccessTime, Me.mnuColVisibleLoggedLoginID, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsUser
    Me.bsUser.DataSource = GetType(clsComboList)
    '
    'bsCtlUserLoginKey
    '
    Me.bsCtlUserLoginKey.DataSource = GetType(csUserLoginKey)
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
    'colApplicationIdentifier
    '
    Me.colApplicationIdentifier.DataPropertyName = "ApplicationIdentifier"
    Me.colApplicationIdentifier.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colApplicationIdentifier.HeaderText = "Application Identifier"
    Me.colApplicationIdentifier.Name = "colApplicationIdentifier"
    Me.colApplicationIdentifier.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colApplicationIdentifier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colApplicationIdentifier.Width = 60
    Me.colApplicationIdentifier.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleApplicationIdentifier 
    ' 
    Me.mnuColVisibleApplicationIdentifier.Checked = True 
    Me.mnuColVisibleApplicationIdentifier.CheckOnClick = True 
    Me.mnuColVisibleApplicationIdentifier.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleApplicationIdentifier.Name = "mnuColVisibleApplicationIdentifier" 
    Me.mnuColVisibleApplicationIdentifier.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleApplicationIdentifier.Text = "Application Identifier" 
    ' 
    'colKeyHashed
    '
    Me.colKeyHashed.DataPropertyName = "KeyHashed"
    Me.colKeyHashed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colKeyHashed.HeaderText = "Key Hashed"
    Me.colKeyHashed.Name = "colKeyHashed"
    Me.colKeyHashed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colKeyHashed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colKeyHashed.Width = 60
    Me.colKeyHashed.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleKeyHashed 
    ' 
    Me.mnuColVisibleKeyHashed.Checked = True 
    Me.mnuColVisibleKeyHashed.CheckOnClick = True 
    Me.mnuColVisibleKeyHashed.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleKeyHashed.Name = "mnuColVisibleKeyHashed" 
    Me.mnuColVisibleKeyHashed.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleKeyHashed.Text = "Key Hashed" 
    ' 
    'colExternalIPAtCreation
    '
    Me.colExternalIPAtCreation.DataPropertyName = "ExternalIPAtCreation"
    Me.colExternalIPAtCreation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colExternalIPAtCreation.HeaderText = "External IP At Creation"
    Me.colExternalIPAtCreation.Name = "colExternalIPAtCreation"
    Me.colExternalIPAtCreation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colExternalIPAtCreation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colExternalIPAtCreation.Width = 60
    Me.colExternalIPAtCreation.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleExternalIPAtCreation 
    ' 
    Me.mnuColVisibleExternalIPAtCreation.Checked = True 
    Me.mnuColVisibleExternalIPAtCreation.CheckOnClick = True 
    Me.mnuColVisibleExternalIPAtCreation.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleExternalIPAtCreation.Name = "mnuColVisibleExternalIPAtCreation" 
    Me.mnuColVisibleExternalIPAtCreation.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleExternalIPAtCreation.Text = "External IP At Creation" 
    ' 
    'colCountryAtCreation
    '
    Me.colCountryAtCreation.DataPropertyName = "CountryAtCreation"
    Me.colCountryAtCreation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCountryAtCreation.HeaderText = "Country At Creation"
    Me.colCountryAtCreation.Name = "colCountryAtCreation"
    Me.colCountryAtCreation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCountryAtCreation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCountryAtCreation.Width = 60
    Me.colCountryAtCreation.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCountryAtCreation 
    ' 
    Me.mnuColVisibleCountryAtCreation.Checked = True 
    Me.mnuColVisibleCountryAtCreation.CheckOnClick = True 
    Me.mnuColVisibleCountryAtCreation.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCountryAtCreation.Name = "mnuColVisibleCountryAtCreation" 
    Me.mnuColVisibleCountryAtCreation.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCountryAtCreation.Text = "Country At Creation" 
    ' 
    'colLastAccessTime
    '
    Me.colLastAccessTime.DataPropertyName = "LastAccessTime"
    Me.colLastAccessTime.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colLastAccessTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colLastAccessTime.HeaderText = "Last Access Time"
    Me.colLastAccessTime.Name = "colLastAccessTime"
    Me.colLastAccessTime.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLastAccessTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLastAccessTime.Width = 60
    Me.colLastAccessTime.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLastAccessTime 
    ' 
    Me.mnuColVisibleLastAccessTime.Checked = True 
    Me.mnuColVisibleLastAccessTime.CheckOnClick = True 
    Me.mnuColVisibleLastAccessTime.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLastAccessTime.Name = "mnuColVisibleLastAccessTime" 
    Me.mnuColVisibleLastAccessTime.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLastAccessTime.Text = "Last Access Time" 
    ' 
    'colLoggedLoginID
    '
    Me.colLoggedLoginID.DataPropertyName = "LoggedLoginID"
    Me.colLoggedLoginID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colLoggedLoginID.HeaderText = "Logged Login ID"
    Me.colLoggedLoginID.Name = "colLoggedLoginID"
    Me.colLoggedLoginID.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLoggedLoginID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLoggedLoginID.Width = 60
    Me.colLoggedLoginID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLoggedLoginID 
    ' 
    Me.mnuColVisibleLoggedLoginID.Checked = True 
    Me.mnuColVisibleLoggedLoginID.CheckOnClick = True 
    Me.mnuColVisibleLoggedLoginID.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLoggedLoginID.Name = "mnuColVisibleLoggedLoginID" 
    Me.mnuColVisibleLoggedLoginID.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLoggedLoginID.Text = "Logged Login ID" 
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
    'ctlUserLoginKeyCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvUserLoginKey)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_UserLoginKeyCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvUserLoginKey, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsUser, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlUserLoginKey, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvUserLoginKey As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlUserLoginKey As System.Windows.Forms.BindingSource
  Friend WithEvents BN As System.Windows.Forms.BindingNavigator
  Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnImport As System.Windows.Forms.ToolStripButton
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
  Friend WithEvents bsUser As System.Windows.Forms.BindingSource
  Friend WithEvents colUser As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colUserText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colApplicationName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleApplicationName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colApplicationIdentifier As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleApplicationIdentifier As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colKeyHashed As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleKeyHashed As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colExternalIPAtCreation As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleExternalIPAtCreation As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCountryAtCreation As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCountryAtCreation As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastAccessTime As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastAccessTime As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLoggedLoginID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLoggedLoginID As System.Windows.Forms.ToolStripMenuItem 

End Class
