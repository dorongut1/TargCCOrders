<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_UserCol
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
    Me.dgvUser = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlUser = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colUserName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUserName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLastName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFirstName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFullName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFullName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNationalIDNo = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNationalIDNo = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAddress = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAddress = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCity = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCity = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colProvinceState = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProvinceState = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colPostalCode = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisiblePostalCode = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCountry = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCountry = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colPhoneNumber = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisiblePhoneNumber = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colEmail = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleEmail = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colPasswordHashed = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisiblePasswordHashed = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDatePasswordChanged = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDatePasswordChanged = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colIDinType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleIDinType = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colRequiresComputerIdentification = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleRequiresComputerIdentification = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colEnableSimultaneousLogins = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleEnableSimultaneousLogins = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colDateActivated = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDateActivated = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colIsDisabled = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleIsDisabled = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colExpiryDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleExpiryDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colComments = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleComments = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLastPasswords = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastPasswords = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colApplications = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleApplications = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsLanguage = New System.Windows.Forms.BindingSource(Me.components)
    Me.colLanguage = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleLanguage = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colIsLockedOut = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleIsLockedOut = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsRole = New System.Windows.Forms.BindingSource(Me.components)
    Me.colRole = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colRoleText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleRole = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsAuthenticationMethod = New System.Windows.Forms.BindingSource(Me.components)
    Me.colAuthenticationMethod = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleAuthenticationMethod = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colRequiresFixedIP = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleRequiresFixedIP = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsMessagingMode = New System.Windows.Forms.BindingSource(Me.components)
    Me.colMessagingMode = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleMessagingMode = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colLoggedInIP = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLoggedInIP = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colApprovalCodeHashed = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleApprovalCodeHashed = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colApprovalFunctionName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleApprovalFunctionName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colApprovalTime = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleApprovalTime = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLastSuccessfulLogin = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastSuccessfulLogin = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colPasswordNeverExpires = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisiblePasswordNeverExpires = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsSecurityQuestion1 = New System.Windows.Forms.BindingSource(Me.components)
    Me.colSecurityQuestion1 = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleSecurityQuestion1 = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colSecurityQuestion1Response = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSecurityQuestion1Response = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsSecurityQuestion2 = New System.Windows.Forms.BindingSource(Me.components)
    Me.colSecurityQuestion2 = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleSecurityQuestion2 = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colSecurityQuestion2Response = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSecurityQuestion2Response = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsSecurityQuestion3 = New System.Windows.Forms.BindingSource(Me.components)
    Me.colSecurityQuestion3 = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleSecurityQuestion3 = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colSecurityQuestion3Response = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSecurityQuestion3Response = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colPIN = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisiblePIN = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvUser, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsLanguage, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsRole, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsAuthenticationMethod, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsMessagingMode, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsSecurityQuestion1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsSecurityQuestion2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsSecurityQuestion3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlUser, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvUser
    '
    Me.dgvUser.AllowUserToAddRows = False
    Me.dgvUser.AllowUserToDeleteRows = False
    Me.dgvUser.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvUser.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvUser.AutoGenerateColumns = False
    Me.dgvUser.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvUser.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvUser.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colUserName, Me.colLastName, Me.colFirstName, Me.colFullName, Me.colNationalIDNo, Me.colAddress, Me.colCity, Me.colProvinceState, Me.colPostalCode, Me.colCountry, Me.colPhoneNumber, Me.colEmail, Me.colPasswordHashed, Me.colDatePasswordChanged, Me.colType, Me.colIDinType, Me.colRequiresComputerIdentification, Me.colEnableSimultaneousLogins, Me.colDateActivated, Me.colIsDisabled, Me.colExpiryDate, Me.colComments, Me.colLastPasswords, Me.colApplications, Me.colLanguage, Me.colIsLockedOut, Me.colRole, Me.colRoleText, Me.colAuthenticationMethod, Me.colRequiresFixedIP, Me.colMessagingMode, Me.colLoggedInIP, Me.colApprovalCodeHashed, Me.colApprovalFunctionName, Me.colApprovalTime, Me.colLastSuccessfulLogin, Me.colPasswordNeverExpires, Me.colSecurityQuestion1, Me.colSecurityQuestion1Response, Me.colSecurityQuestion2, Me.colSecurityQuestion2Response, Me.colSecurityQuestion3, Me.colSecurityQuestion3Response, Me.colPIN})
    Me.dgvUser.DataSource = Me.bsCtlUser
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvUser.DefaultCellStyle = styleDefaultCell
    Me.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvUser.EnableHeadersVisualStyles = False
    Me.dgvUser.Location = New System.Drawing.Point(0, 25)
    Me.dgvUser.MultiSelect = False 
    Me.dgvUser.ContextMenuStrip = Me.cmsGrid 
    Me.dgvUser.Name = "dgvUser"
    Me.dgvUser.RowHeadersVisible = False
    Me.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvUser.Size = New System.Drawing.Size(712, 347)
    Me.dgvUser.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlUser
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleUserName, Me.mnuColVisibleLastName, Me.mnuColVisibleFirstName, Me.mnuColVisibleFullName, Me.mnuColVisibleNationalIDNo, Me.mnuColVisibleAddress, Me.mnuColVisibleCity, Me.mnuColVisibleProvinceState, Me.mnuColVisiblePostalCode, Me.mnuColVisibleCountry, Me.mnuColVisiblePhoneNumber, Me.mnuColVisibleEmail, Me.mnuColVisiblePasswordHashed, Me.mnuColVisibleDatePasswordChanged, Me.mnuColVisibleType, Me.mnuColVisibleIDinType, Me.mnuColVisibleRequiresComputerIdentification, Me.mnuColVisibleEnableSimultaneousLogins, Me.mnuColVisibleDateActivated, Me.mnuColVisibleIsDisabled, Me.mnuColVisibleExpiryDate, Me.mnuColVisibleComments, Me.mnuColVisibleLastPasswords, Me.mnuColVisibleApplications, Me.mnuColVisibleLanguage, Me.mnuColVisibleIsLockedOut, Me.mnuColVisibleRole, Me.mnuColVisibleAuthenticationMethod, Me.mnuColVisibleRequiresFixedIP, Me.mnuColVisibleMessagingMode, Me.mnuColVisibleLoggedInIP, Me.mnuColVisibleApprovalCodeHashed, Me.mnuColVisibleApprovalFunctionName, Me.mnuColVisibleApprovalTime, Me.mnuColVisibleLastSuccessfulLogin, Me.mnuColVisiblePasswordNeverExpires, Me.mnuColVisibleSecurityQuestion1, Me.mnuColVisibleSecurityQuestion1Response, Me.mnuColVisibleSecurityQuestion2, Me.mnuColVisibleSecurityQuestion2Response, Me.mnuColVisibleSecurityQuestion3, Me.mnuColVisibleSecurityQuestion3Response, Me.mnuColVisiblePIN, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsType
    Me.bsType.DataSource = GetType(clsComboList)
    'bsLanguage
    Me.bsLanguage.DataSource = GetType(clsComboList)
    'bsRole
    Me.bsRole.DataSource = GetType(clsComboList)
    'bsAuthenticationMethod
    Me.bsAuthenticationMethod.DataSource = GetType(clsComboList)
    'bsMessagingMode
    Me.bsMessagingMode.DataSource = GetType(clsComboList)
    'bsSecurityQuestion1
    Me.bsSecurityQuestion1.DataSource = GetType(clsComboList)
    'bsSecurityQuestion2
    Me.bsSecurityQuestion2.DataSource = GetType(clsComboList)
    'bsSecurityQuestion3
    Me.bsSecurityQuestion3.DataSource = GetType(clsComboList)
    '
    'bsCtlUser
    '
    Me.bsCtlUser.DataSource = GetType(csUser)
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
    'colLastName
    '
    Me.colLastName.DataPropertyName = "LastName"
    Me.colLastName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colLastName.HeaderText = "Last Name"
    Me.colLastName.Name = "colLastName"
    Me.colLastName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLastName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLastName.Width = 60
    Me.colLastName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLastName 
    ' 
    Me.mnuColVisibleLastName.Checked = True 
    Me.mnuColVisibleLastName.CheckOnClick = True 
    Me.mnuColVisibleLastName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLastName.Name = "mnuColVisibleLastName" 
    Me.mnuColVisibleLastName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLastName.Text = "Last Name" 
    ' 
    'colFirstName
    '
    Me.colFirstName.DataPropertyName = "FirstName"
    Me.colFirstName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFirstName.HeaderText = "First Name"
    Me.colFirstName.Name = "colFirstName"
    Me.colFirstName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFirstName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFirstName.Width = 60
    Me.colFirstName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFirstName 
    ' 
    Me.mnuColVisibleFirstName.Checked = True 
    Me.mnuColVisibleFirstName.CheckOnClick = True 
    Me.mnuColVisibleFirstName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFirstName.Name = "mnuColVisibleFirstName" 
    Me.mnuColVisibleFirstName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFirstName.Text = "First Name" 
    ' 
    'colFullName
    '
    Me.colFullName.DataPropertyName = "FullName"
    Me.colFullName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colFullName.HeaderText = "Full Name"
    Me.colFullName.Name = "colFullName"
    Me.colFullName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFullName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFullName.Width = 60
    Me.colFullName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFullName 
    ' 
    Me.mnuColVisibleFullName.Checked = True 
    Me.mnuColVisibleFullName.CheckOnClick = True 
    Me.mnuColVisibleFullName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFullName.Name = "mnuColVisibleFullName" 
    Me.mnuColVisibleFullName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFullName.Text = "Full Name" 
    ' 
    'colNationalIDNo
    '
    Me.colNationalIDNo.DataPropertyName = "NationalIDNo"
    Me.colNationalIDNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colNationalIDNo.HeaderText = "National ID No"
    Me.colNationalIDNo.Name = "colNationalIDNo"
    Me.colNationalIDNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colNationalIDNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colNationalIDNo.Width = 60
    Me.colNationalIDNo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleNationalIDNo 
    ' 
    Me.mnuColVisibleNationalIDNo.Checked = True 
    Me.mnuColVisibleNationalIDNo.CheckOnClick = True 
    Me.mnuColVisibleNationalIDNo.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleNationalIDNo.Name = "mnuColVisibleNationalIDNo" 
    Me.mnuColVisibleNationalIDNo.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleNationalIDNo.Text = "National ID No" 
    ' 
    'colAddress
    '
    Me.colAddress.DataPropertyName = "Address"
    Me.colAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colAddress.HeaderText = "Address"
    Me.colAddress.Name = "colAddress"
    Me.colAddress.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAddress.Width = 60
    Me.colAddress.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAddress 
    ' 
    Me.mnuColVisibleAddress.Checked = True 
    Me.mnuColVisibleAddress.CheckOnClick = True 
    Me.mnuColVisibleAddress.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAddress.Name = "mnuColVisibleAddress" 
    Me.mnuColVisibleAddress.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAddress.Text = "Address" 
    ' 
    'colCity
    '
    Me.colCity.DataPropertyName = "City"
    Me.colCity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCity.HeaderText = "City"
    Me.colCity.Name = "colCity"
    Me.colCity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCity.Width = 60
    Me.colCity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCity 
    ' 
    Me.mnuColVisibleCity.Checked = True 
    Me.mnuColVisibleCity.CheckOnClick = True 
    Me.mnuColVisibleCity.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCity.Name = "mnuColVisibleCity" 
    Me.mnuColVisibleCity.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCity.Text = "City" 
    ' 
    'colProvinceState
    '
    Me.colProvinceState.DataPropertyName = "ProvinceState"
    Me.colProvinceState.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colProvinceState.HeaderText = "Province State"
    Me.colProvinceState.Name = "colProvinceState"
    Me.colProvinceState.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colProvinceState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colProvinceState.Width = 60
    Me.colProvinceState.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleProvinceState 
    ' 
    Me.mnuColVisibleProvinceState.Checked = True 
    Me.mnuColVisibleProvinceState.CheckOnClick = True 
    Me.mnuColVisibleProvinceState.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleProvinceState.Name = "mnuColVisibleProvinceState" 
    Me.mnuColVisibleProvinceState.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleProvinceState.Text = "Province State" 
    ' 
    'colPostalCode
    '
    Me.colPostalCode.DataPropertyName = "PostalCode"
    Me.colPostalCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colPostalCode.HeaderText = "Postal Code"
    Me.colPostalCode.Name = "colPostalCode"
    Me.colPostalCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPostalCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPostalCode.Width = 60
    Me.colPostalCode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePostalCode 
    ' 
    Me.mnuColVisiblePostalCode.Checked = True 
    Me.mnuColVisiblePostalCode.CheckOnClick = True 
    Me.mnuColVisiblePostalCode.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePostalCode.Name = "mnuColVisiblePostalCode" 
    Me.mnuColVisiblePostalCode.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePostalCode.Text = "Postal Code" 
    ' 
    'colCountry
    '
    Me.colCountry.DataPropertyName = "Country"
    Me.colCountry.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCountry.HeaderText = "Country"
    Me.colCountry.Name = "colCountry"
    Me.colCountry.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCountry.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCountry.Width = 60
    Me.colCountry.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCountry 
    ' 
    Me.mnuColVisibleCountry.Checked = True 
    Me.mnuColVisibleCountry.CheckOnClick = True 
    Me.mnuColVisibleCountry.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCountry.Name = "mnuColVisibleCountry" 
    Me.mnuColVisibleCountry.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCountry.Text = "Country" 
    ' 
    'colPhoneNumber
    '
    Me.colPhoneNumber.DataPropertyName = "PhoneNumber"
    Me.colPhoneNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colPhoneNumber.HeaderText = "Phone Number"
    Me.colPhoneNumber.Name = "colPhoneNumber"
    Me.colPhoneNumber.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPhoneNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPhoneNumber.Width = 60
    Me.colPhoneNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePhoneNumber 
    ' 
    Me.mnuColVisiblePhoneNumber.Checked = True 
    Me.mnuColVisiblePhoneNumber.CheckOnClick = True 
    Me.mnuColVisiblePhoneNumber.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePhoneNumber.Name = "mnuColVisiblePhoneNumber" 
    Me.mnuColVisiblePhoneNumber.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePhoneNumber.Text = "Phone Number" 
    ' 
    'colEmail
    '
    Me.colEmail.DataPropertyName = "Email"
    Me.colEmail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colEmail.HeaderText = "Email"
    Me.colEmail.Name = "colEmail"
    Me.colEmail.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEmail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEmail.Width = 60
    Me.colEmail.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEmail 
    ' 
    Me.mnuColVisibleEmail.Checked = True 
    Me.mnuColVisibleEmail.CheckOnClick = True 
    Me.mnuColVisibleEmail.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEmail.Name = "mnuColVisibleEmail" 
    Me.mnuColVisibleEmail.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEmail.Text = "Email" 
    ' 
    'colPasswordHashed
    '
    Me.colPasswordHashed.DataPropertyName = "PasswordHashed"
    Me.colPasswordHashed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colPasswordHashed.HeaderText = "Password Hashed"
    Me.colPasswordHashed.Name = "colPasswordHashed"
    Me.colPasswordHashed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPasswordHashed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPasswordHashed.Width = 60
    Me.colPasswordHashed.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePasswordHashed 
    ' 
    Me.mnuColVisiblePasswordHashed.Checked = True 
    Me.mnuColVisiblePasswordHashed.CheckOnClick = True 
    Me.mnuColVisiblePasswordHashed.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePasswordHashed.Name = "mnuColVisiblePasswordHashed" 
    Me.mnuColVisiblePasswordHashed.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePasswordHashed.Text = "Password Hashed" 
    ' 
    'colDatePasswordChanged
    '
    Me.colDatePasswordChanged.DataPropertyName = "DatePasswordChanged"
    Me.colDatePasswordChanged.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colDatePasswordChanged.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDatePasswordChanged.HeaderText = "Date Password Changed"
    Me.colDatePasswordChanged.Name = "colDatePasswordChanged"
    Me.colDatePasswordChanged.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDatePasswordChanged.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDatePasswordChanged.Width = 60
    Me.colDatePasswordChanged.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDatePasswordChanged 
    ' 
    Me.mnuColVisibleDatePasswordChanged.Checked = True 
    Me.mnuColVisibleDatePasswordChanged.CheckOnClick = True 
    Me.mnuColVisibleDatePasswordChanged.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDatePasswordChanged.Name = "mnuColVisibleDatePasswordChanged" 
    Me.mnuColVisibleDatePasswordChanged.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDatePasswordChanged.Text = "Date Password Changed" 
    ' 
    'colType
    '
    Me.colType.DataPropertyName = "Type"
    Me.colType.DataSource = Me.bsType
    Me.colType.DisplayMember = "Text"
    Me.colType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colType.HeaderText = "Type"
    Me.colType.Name = "colType"
    Me.colType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colType.ValueMember = "KeyEnum"
    Me.colType.Width = 60
    Me.colType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleType 
    ' 
    Me.mnuColVisibleType.Checked = True 
    Me.mnuColVisibleType.CheckOnClick = True 
    Me.mnuColVisibleType.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleType.Name = "mnuColVisibleType" 
    Me.mnuColVisibleType.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleType.Text = "Type" 
    ' 
    'colIDinType
    '
    Me.colIDinType.DataPropertyName = "IDinType"
    Me.colIDinType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colIDinType.HeaderText = "TypeName"
    Me.colIDinType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing] 
    Me.colIDinType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic 
    Me.colIDinType.Name = "colIDinType"
    Me.colIDinType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIDinType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIDinType.Width = 60
    Me.colIDinType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIDinType 
    ' 
    Me.mnuColVisibleIDinType.Checked = True 
    Me.mnuColVisibleIDinType.CheckOnClick = True 
    Me.mnuColVisibleIDinType.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIDinType.Name = "mnuColVisibleIDinType" 
    Me.mnuColVisibleIDinType.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIDinType.Text = "I Din Type" 
    ' 
    'colRequiresComputerIdentification
    '
    Me.colRequiresComputerIdentification.DataPropertyName = "RequiresComputerIdentification"
    Me.colRequiresComputerIdentification.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colRequiresComputerIdentification.HeaderText = "Requires Computer Identification"
    Me.colRequiresComputerIdentification.Name = "colRequiresComputerIdentification"
    Me.colRequiresComputerIdentification.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRequiresComputerIdentification.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRequiresComputerIdentification.Width = 60
    Me.colRequiresComputerIdentification.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRequiresComputerIdentification 
    ' 
    Me.mnuColVisibleRequiresComputerIdentification.Checked = True 
    Me.mnuColVisibleRequiresComputerIdentification.CheckOnClick = True 
    Me.mnuColVisibleRequiresComputerIdentification.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRequiresComputerIdentification.Name = "mnuColVisibleRequiresComputerIdentification" 
    Me.mnuColVisibleRequiresComputerIdentification.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRequiresComputerIdentification.Text = "Requires Computer Identification" 
    ' 
    'colEnableSimultaneousLogins
    '
    Me.colEnableSimultaneousLogins.DataPropertyName = "EnableSimultaneousLogins"
    Me.colEnableSimultaneousLogins.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colEnableSimultaneousLogins.HeaderText = "Enable Simultaneous Logins"
    Me.colEnableSimultaneousLogins.Name = "colEnableSimultaneousLogins"
    Me.colEnableSimultaneousLogins.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEnableSimultaneousLogins.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEnableSimultaneousLogins.Width = 60
    Me.colEnableSimultaneousLogins.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEnableSimultaneousLogins 
    ' 
    Me.mnuColVisibleEnableSimultaneousLogins.Checked = True 
    Me.mnuColVisibleEnableSimultaneousLogins.CheckOnClick = True 
    Me.mnuColVisibleEnableSimultaneousLogins.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEnableSimultaneousLogins.Name = "mnuColVisibleEnableSimultaneousLogins" 
    Me.mnuColVisibleEnableSimultaneousLogins.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEnableSimultaneousLogins.Text = "Enable Simultaneous Logins" 
    ' 
    'colDateActivated
    '
    Me.colDateActivated.DataPropertyName = "DateActivated"
    Me.colDateActivated.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colDateActivated.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDateActivated.HeaderText = "Date Activated"
    Me.colDateActivated.Name = "colDateActivated"
    Me.colDateActivated.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDateActivated.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDateActivated.Width = 60
    Me.colDateActivated.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDateActivated 
    ' 
    Me.mnuColVisibleDateActivated.Checked = True 
    Me.mnuColVisibleDateActivated.CheckOnClick = True 
    Me.mnuColVisibleDateActivated.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDateActivated.Name = "mnuColVisibleDateActivated" 
    Me.mnuColVisibleDateActivated.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDateActivated.Text = "Date Activated" 
    ' 
    'colIsDisabled
    '
    Me.colIsDisabled.DataPropertyName = "IsDisabled"
    Me.colIsDisabled.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colIsDisabled.HeaderText = "Is Disabled"
    Me.colIsDisabled.Name = "colIsDisabled"
    Me.colIsDisabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIsDisabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIsDisabled.Width = 60
    Me.colIsDisabled.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIsDisabled 
    ' 
    Me.mnuColVisibleIsDisabled.Checked = True 
    Me.mnuColVisibleIsDisabled.CheckOnClick = True 
    Me.mnuColVisibleIsDisabled.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIsDisabled.Name = "mnuColVisibleIsDisabled" 
    Me.mnuColVisibleIsDisabled.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIsDisabled.Text = "Is Disabled" 
    ' 
    'colExpiryDate
    '
    Me.colExpiryDate.DataPropertyName = "ExpiryDate"
    Me.colExpiryDate.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colExpiryDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colExpiryDate.HeaderText = "Expiry Date"
    Me.colExpiryDate.Name = "colExpiryDate"
    Me.colExpiryDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colExpiryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colExpiryDate.Width = 60
    Me.colExpiryDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleExpiryDate 
    ' 
    Me.mnuColVisibleExpiryDate.Checked = True 
    Me.mnuColVisibleExpiryDate.CheckOnClick = True 
    Me.mnuColVisibleExpiryDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleExpiryDate.Name = "mnuColVisibleExpiryDate" 
    Me.mnuColVisibleExpiryDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleExpiryDate.Text = "Expiry Date" 
    ' 
    'colComments
    '
    Me.colComments.DataPropertyName = "Comments"
    Me.colComments.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colComments.HeaderText = "Comments"
    Me.colComments.Name = "colComments"
    Me.colComments.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colComments.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colComments.Width = 60
    Me.colComments.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleComments 
    ' 
    Me.mnuColVisibleComments.Checked = True 
    Me.mnuColVisibleComments.CheckOnClick = True 
    Me.mnuColVisibleComments.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleComments.Name = "mnuColVisibleComments" 
    Me.mnuColVisibleComments.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleComments.Text = "Comments" 
    ' 
    'colLastPasswords
    '
    Me.colLastPasswords.DataPropertyName = "LastPasswords"
    Me.colLastPasswords.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colLastPasswords.HeaderText = "Last Passwords"
    Me.colLastPasswords.Name = "colLastPasswords"
    Me.colLastPasswords.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLastPasswords.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLastPasswords.Width = 60
    Me.colLastPasswords.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLastPasswords 
    ' 
    Me.mnuColVisibleLastPasswords.Checked = True 
    Me.mnuColVisibleLastPasswords.CheckOnClick = True 
    Me.mnuColVisibleLastPasswords.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLastPasswords.Name = "mnuColVisibleLastPasswords" 
    Me.mnuColVisibleLastPasswords.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLastPasswords.Text = "Last Passwords" 
    ' 
    'colApplications
    '
    Me.colApplications.DataPropertyName = "Applications"
    Me.colApplications.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colApplications.HeaderText = "Applications"
    Me.colApplications.Name = "colApplications"
    Me.colApplications.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colApplications.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colApplications.Width = 60
    Me.colApplications.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleApplications 
    ' 
    Me.mnuColVisibleApplications.Checked = True 
    Me.mnuColVisibleApplications.CheckOnClick = True 
    Me.mnuColVisibleApplications.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleApplications.Name = "mnuColVisibleApplications" 
    Me.mnuColVisibleApplications.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleApplications.Text = "Applications" 
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
    'colIsLockedOut
    '
    Me.colIsLockedOut.DataPropertyName = "IsLockedOut"
    Me.colIsLockedOut.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colIsLockedOut.HeaderText = "Is Locked Out"
    Me.colIsLockedOut.Name = "colIsLockedOut"
    Me.colIsLockedOut.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIsLockedOut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIsLockedOut.Width = 60
    Me.colIsLockedOut.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIsLockedOut 
    ' 
    Me.mnuColVisibleIsLockedOut.Checked = True 
    Me.mnuColVisibleIsLockedOut.CheckOnClick = True 
    Me.mnuColVisibleIsLockedOut.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIsLockedOut.Name = "mnuColVisibleIsLockedOut" 
    Me.mnuColVisibleIsLockedOut.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIsLockedOut.Text = "Is Locked Out" 
    ' 
    'colRole
    '
    Me.colRole.DataPropertyName = "RoleID"
    Me.colRole.DataSource = Me.bsRole
    Me.colRole.DisplayMember = "Text"
    Me.colRole.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colRole.HeaderText = "Role"
    Me.colRole.Name = "colRole"
    Me.colRole.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRole.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRole.ValueMember = "KeyLong"
    Me.colRole.Width = 60
    Me.colRole.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colRoleText 
    ' 
    Me.colRoleText.DataPropertyName = "RoleText" 
    Me.colRoleText.HeaderText = "Role" 
    Me.colRoleText.Name = "colRoleText" 
    Me.colRoleText.ReadOnly = True 
    Me.colRole.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRole 
    ' 
    Me.mnuColVisibleRole.Checked = True 
    Me.mnuColVisibleRole.CheckOnClick = True 
    Me.mnuColVisibleRole.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRole.Name = "mnuColVisibleRole" 
    Me.mnuColVisibleRole.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRole.Text = "Role" 
    ' 
    'colAuthenticationMethod
    '
    Me.colAuthenticationMethod.DataPropertyName = "AuthenticationMethod"
    Me.colAuthenticationMethod.DataSource = Me.bsAuthenticationMethod
    Me.colAuthenticationMethod.DisplayMember = "Text"
    Me.colAuthenticationMethod.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colAuthenticationMethod.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colAuthenticationMethod.HeaderText = "Authentication Method"
    Me.colAuthenticationMethod.Name = "colAuthenticationMethod"
    Me.colAuthenticationMethod.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAuthenticationMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAuthenticationMethod.ValueMember = "KeyEnum"
    Me.colAuthenticationMethod.Width = 60
    Me.colAuthenticationMethod.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAuthenticationMethod 
    ' 
    Me.mnuColVisibleAuthenticationMethod.Checked = True 
    Me.mnuColVisibleAuthenticationMethod.CheckOnClick = True 
    Me.mnuColVisibleAuthenticationMethod.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAuthenticationMethod.Name = "mnuColVisibleAuthenticationMethod" 
    Me.mnuColVisibleAuthenticationMethod.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAuthenticationMethod.Text = "Authentication Method" 
    ' 
    'colRequiresFixedIP
    '
    Me.colRequiresFixedIP.DataPropertyName = "RequiresFixedIP"
    Me.colRequiresFixedIP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colRequiresFixedIP.HeaderText = "Requires Fixed IP"
    Me.colRequiresFixedIP.Name = "colRequiresFixedIP"
    Me.colRequiresFixedIP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRequiresFixedIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRequiresFixedIP.Width = 60
    Me.colRequiresFixedIP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRequiresFixedIP 
    ' 
    Me.mnuColVisibleRequiresFixedIP.Checked = True 
    Me.mnuColVisibleRequiresFixedIP.CheckOnClick = True 
    Me.mnuColVisibleRequiresFixedIP.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRequiresFixedIP.Name = "mnuColVisibleRequiresFixedIP" 
    Me.mnuColVisibleRequiresFixedIP.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRequiresFixedIP.Text = "Requires Fixed IP" 
    ' 
    'colMessagingMode
    '
    Me.colMessagingMode.DataPropertyName = "MessagingMode"
    Me.colMessagingMode.DataSource = Me.bsMessagingMode
    Me.colMessagingMode.DisplayMember = "Text"
    Me.colMessagingMode.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colMessagingMode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colMessagingMode.HeaderText = "Messaging Mode"
    Me.colMessagingMode.Name = "colMessagingMode"
    Me.colMessagingMode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colMessagingMode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colMessagingMode.ValueMember = "KeyEnum"
    Me.colMessagingMode.Width = 60
    Me.colMessagingMode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleMessagingMode 
    ' 
    Me.mnuColVisibleMessagingMode.Checked = True 
    Me.mnuColVisibleMessagingMode.CheckOnClick = True 
    Me.mnuColVisibleMessagingMode.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleMessagingMode.Name = "mnuColVisibleMessagingMode" 
    Me.mnuColVisibleMessagingMode.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleMessagingMode.Text = "Messaging Mode" 
    ' 
    'colLoggedInIP
    '
    Me.colLoggedInIP.DataPropertyName = "LoggedInIP"
    Me.colLoggedInIP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colLoggedInIP.HeaderText = "Logged In IP"
    Me.colLoggedInIP.Name = "colLoggedInIP"
    Me.colLoggedInIP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLoggedInIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLoggedInIP.Width = 60
    Me.colLoggedInIP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLoggedInIP 
    ' 
    Me.mnuColVisibleLoggedInIP.Checked = True 
    Me.mnuColVisibleLoggedInIP.CheckOnClick = True 
    Me.mnuColVisibleLoggedInIP.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLoggedInIP.Name = "mnuColVisibleLoggedInIP" 
    Me.mnuColVisibleLoggedInIP.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLoggedInIP.Text = "Logged In IP" 
    ' 
    'colApprovalCodeHashed
    '
    Me.colApprovalCodeHashed.DataPropertyName = "ApprovalCodeHashed"
    Me.colApprovalCodeHashed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colApprovalCodeHashed.HeaderText = "Approval Code Hashed"
    Me.colApprovalCodeHashed.Name = "colApprovalCodeHashed"
    Me.colApprovalCodeHashed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colApprovalCodeHashed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colApprovalCodeHashed.Width = 60
    Me.colApprovalCodeHashed.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleApprovalCodeHashed 
    ' 
    Me.mnuColVisibleApprovalCodeHashed.Checked = True 
    Me.mnuColVisibleApprovalCodeHashed.CheckOnClick = True 
    Me.mnuColVisibleApprovalCodeHashed.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleApprovalCodeHashed.Name = "mnuColVisibleApprovalCodeHashed" 
    Me.mnuColVisibleApprovalCodeHashed.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleApprovalCodeHashed.Text = "Approval Code Hashed" 
    ' 
    'colApprovalFunctionName
    '
    Me.colApprovalFunctionName.DataPropertyName = "ApprovalFunctionName"
    Me.colApprovalFunctionName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colApprovalFunctionName.HeaderText = "Approval Function Name"
    Me.colApprovalFunctionName.Name = "colApprovalFunctionName"
    Me.colApprovalFunctionName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colApprovalFunctionName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colApprovalFunctionName.Width = 60
    Me.colApprovalFunctionName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleApprovalFunctionName 
    ' 
    Me.mnuColVisibleApprovalFunctionName.Checked = True 
    Me.mnuColVisibleApprovalFunctionName.CheckOnClick = True 
    Me.mnuColVisibleApprovalFunctionName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleApprovalFunctionName.Name = "mnuColVisibleApprovalFunctionName" 
    Me.mnuColVisibleApprovalFunctionName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleApprovalFunctionName.Text = "Approval Function Name" 
    ' 
    'colApprovalTime
    '
    Me.colApprovalTime.DataPropertyName = "ApprovalTime"
    Me.colApprovalTime.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss zzz"
    Me.colApprovalTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colApprovalTime.HeaderText = "Approval Time"
    Me.colApprovalTime.Name = "colApprovalTime"
    Me.colApprovalTime.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colApprovalTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colApprovalTime.Width = 60
    Me.colApprovalTime.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleApprovalTime 
    ' 
    Me.mnuColVisibleApprovalTime.Checked = True 
    Me.mnuColVisibleApprovalTime.CheckOnClick = True 
    Me.mnuColVisibleApprovalTime.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleApprovalTime.Name = "mnuColVisibleApprovalTime" 
    Me.mnuColVisibleApprovalTime.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleApprovalTime.Text = "Approval Time" 
    ' 
    'colLastSuccessfulLogin
    '
    Me.colLastSuccessfulLogin.DataPropertyName = "LastSuccessfulLogin"
    Me.colLastSuccessfulLogin.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss zzz"
    Me.colLastSuccessfulLogin.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colLastSuccessfulLogin.HeaderText = "Last Successful Login"
    Me.colLastSuccessfulLogin.Name = "colLastSuccessfulLogin"
    Me.colLastSuccessfulLogin.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLastSuccessfulLogin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLastSuccessfulLogin.Width = 60
    Me.colLastSuccessfulLogin.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLastSuccessfulLogin 
    ' 
    Me.mnuColVisibleLastSuccessfulLogin.Checked = True 
    Me.mnuColVisibleLastSuccessfulLogin.CheckOnClick = True 
    Me.mnuColVisibleLastSuccessfulLogin.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLastSuccessfulLogin.Name = "mnuColVisibleLastSuccessfulLogin" 
    Me.mnuColVisibleLastSuccessfulLogin.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLastSuccessfulLogin.Text = "Last Successful Login" 
    ' 
    'colPasswordNeverExpires
    '
    Me.colPasswordNeverExpires.DataPropertyName = "PasswordNeverExpires"
    Me.colPasswordNeverExpires.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colPasswordNeverExpires.HeaderText = "Password Never Expires"
    Me.colPasswordNeverExpires.Name = "colPasswordNeverExpires"
    Me.colPasswordNeverExpires.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPasswordNeverExpires.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPasswordNeverExpires.Width = 60
    Me.colPasswordNeverExpires.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePasswordNeverExpires 
    ' 
    Me.mnuColVisiblePasswordNeverExpires.Checked = True 
    Me.mnuColVisiblePasswordNeverExpires.CheckOnClick = True 
    Me.mnuColVisiblePasswordNeverExpires.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePasswordNeverExpires.Name = "mnuColVisiblePasswordNeverExpires" 
    Me.mnuColVisiblePasswordNeverExpires.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePasswordNeverExpires.Text = "Password Never Expires" 
    ' 
    'colSecurityQuestion1
    '
    Me.colSecurityQuestion1.DataPropertyName = "SecurityQuestion1Code"
    Me.colSecurityQuestion1.DataSource = Me.bsSecurityQuestion1
    Me.colSecurityQuestion1.DisplayMember = "Text"
    Me.colSecurityQuestion1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colSecurityQuestion1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colSecurityQuestion1.HeaderText = "Security Question 1"
    Me.colSecurityQuestion1.Name = "colSecurityQuestion1"
    Me.colSecurityQuestion1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSecurityQuestion1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSecurityQuestion1.ValueMember = "KeyString"
    Me.colSecurityQuestion1.Width = 60
    Me.colSecurityQuestion1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSecurityQuestion1 
    ' 
    Me.mnuColVisibleSecurityQuestion1.Checked = True 
    Me.mnuColVisibleSecurityQuestion1.CheckOnClick = True 
    Me.mnuColVisibleSecurityQuestion1.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSecurityQuestion1.Name = "mnuColVisibleSecurityQuestion1" 
    Me.mnuColVisibleSecurityQuestion1.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSecurityQuestion1.Text = "Security Question 1" 
    ' 
    'colSecurityQuestion1Response
    '
    Me.colSecurityQuestion1Response.DataPropertyName = "SecurityQuestion1Response"
    Me.colSecurityQuestion1Response.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSecurityQuestion1Response.HeaderText = "Security Question 1 Response"
    Me.colSecurityQuestion1Response.Name = "colSecurityQuestion1Response"
    Me.colSecurityQuestion1Response.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSecurityQuestion1Response.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSecurityQuestion1Response.Width = 60
    Me.colSecurityQuestion1Response.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSecurityQuestion1Response 
    ' 
    Me.mnuColVisibleSecurityQuestion1Response.Checked = True 
    Me.mnuColVisibleSecurityQuestion1Response.CheckOnClick = True 
    Me.mnuColVisibleSecurityQuestion1Response.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSecurityQuestion1Response.Name = "mnuColVisibleSecurityQuestion1Response" 
    Me.mnuColVisibleSecurityQuestion1Response.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSecurityQuestion1Response.Text = "Security Question 1 Response" 
    ' 
    'colSecurityQuestion2
    '
    Me.colSecurityQuestion2.DataPropertyName = "SecurityQuestion2Code"
    Me.colSecurityQuestion2.DataSource = Me.bsSecurityQuestion2
    Me.colSecurityQuestion2.DisplayMember = "Text"
    Me.colSecurityQuestion2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colSecurityQuestion2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colSecurityQuestion2.HeaderText = "Security Question 2"
    Me.colSecurityQuestion2.Name = "colSecurityQuestion2"
    Me.colSecurityQuestion2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSecurityQuestion2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSecurityQuestion2.ValueMember = "KeyString"
    Me.colSecurityQuestion2.Width = 60
    Me.colSecurityQuestion2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSecurityQuestion2 
    ' 
    Me.mnuColVisibleSecurityQuestion2.Checked = True 
    Me.mnuColVisibleSecurityQuestion2.CheckOnClick = True 
    Me.mnuColVisibleSecurityQuestion2.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSecurityQuestion2.Name = "mnuColVisibleSecurityQuestion2" 
    Me.mnuColVisibleSecurityQuestion2.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSecurityQuestion2.Text = "Security Question 2" 
    ' 
    'colSecurityQuestion2Response
    '
    Me.colSecurityQuestion2Response.DataPropertyName = "SecurityQuestion2Response"
    Me.colSecurityQuestion2Response.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSecurityQuestion2Response.HeaderText = "Security Question 2 Response"
    Me.colSecurityQuestion2Response.Name = "colSecurityQuestion2Response"
    Me.colSecurityQuestion2Response.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSecurityQuestion2Response.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSecurityQuestion2Response.Width = 60
    Me.colSecurityQuestion2Response.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSecurityQuestion2Response 
    ' 
    Me.mnuColVisibleSecurityQuestion2Response.Checked = True 
    Me.mnuColVisibleSecurityQuestion2Response.CheckOnClick = True 
    Me.mnuColVisibleSecurityQuestion2Response.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSecurityQuestion2Response.Name = "mnuColVisibleSecurityQuestion2Response" 
    Me.mnuColVisibleSecurityQuestion2Response.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSecurityQuestion2Response.Text = "Security Question 2 Response" 
    ' 
    'colSecurityQuestion3
    '
    Me.colSecurityQuestion3.DataPropertyName = "SecurityQuestion3Code"
    Me.colSecurityQuestion3.DataSource = Me.bsSecurityQuestion3
    Me.colSecurityQuestion3.DisplayMember = "Text"
    Me.colSecurityQuestion3.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colSecurityQuestion3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colSecurityQuestion3.HeaderText = "Security Question 3"
    Me.colSecurityQuestion3.Name = "colSecurityQuestion3"
    Me.colSecurityQuestion3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSecurityQuestion3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSecurityQuestion3.ValueMember = "KeyString"
    Me.colSecurityQuestion3.Width = 60
    Me.colSecurityQuestion3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSecurityQuestion3 
    ' 
    Me.mnuColVisibleSecurityQuestion3.Checked = True 
    Me.mnuColVisibleSecurityQuestion3.CheckOnClick = True 
    Me.mnuColVisibleSecurityQuestion3.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSecurityQuestion3.Name = "mnuColVisibleSecurityQuestion3" 
    Me.mnuColVisibleSecurityQuestion3.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSecurityQuestion3.Text = "Security Question 3" 
    ' 
    'colSecurityQuestion3Response
    '
    Me.colSecurityQuestion3Response.DataPropertyName = "SecurityQuestion3Response"
    Me.colSecurityQuestion3Response.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSecurityQuestion3Response.HeaderText = "Security Question 3 Response"
    Me.colSecurityQuestion3Response.Name = "colSecurityQuestion3Response"
    Me.colSecurityQuestion3Response.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSecurityQuestion3Response.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSecurityQuestion3Response.Width = 60
    Me.colSecurityQuestion3Response.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSecurityQuestion3Response 
    ' 
    Me.mnuColVisibleSecurityQuestion3Response.Checked = True 
    Me.mnuColVisibleSecurityQuestion3Response.CheckOnClick = True 
    Me.mnuColVisibleSecurityQuestion3Response.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSecurityQuestion3Response.Name = "mnuColVisibleSecurityQuestion3Response" 
    Me.mnuColVisibleSecurityQuestion3Response.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSecurityQuestion3Response.Text = "Security Question 3 Response" 
    ' 
    'colPIN
    '
    Me.colPIN.DataPropertyName = "PIN"
    Me.colPIN.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colPIN.HeaderText = "PIN"
    Me.colPIN.Name = "colPIN"
    Me.colPIN.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPIN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPIN.Width = 60
    Me.colPIN.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePIN 
    ' 
    Me.mnuColVisiblePIN.Checked = True 
    Me.mnuColVisiblePIN.CheckOnClick = True 
    Me.mnuColVisiblePIN.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePIN.Name = "mnuColVisiblePIN" 
    Me.mnuColVisiblePIN.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePIN.Text = "PIN" 
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
    'ctlUserCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvUser)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_UserCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvUser, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsLanguage, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsRole, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsAuthenticationMethod, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsMessagingMode, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsSecurityQuestion1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsSecurityQuestion2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsSecurityQuestion3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlUser, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvUser As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlUser As System.Windows.Forms.BindingSource
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
  Friend WithEvents colUserName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleUserName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFirstName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFullName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFullName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNationalIDNo As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNationalIDNo As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAddress As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAddress As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCity As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCity As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colProvinceState As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleProvinceState As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPostalCode As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisiblePostalCode As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCountry As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCountry As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPhoneNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisiblePhoneNumber As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEmail As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleEmail As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPasswordHashed As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisiblePasswordHashed As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDatePasswordChanged As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDatePasswordChanged As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsType As System.Windows.Forms.BindingSource
  Friend WithEvents colType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIDinType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleIDinType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colRequiresComputerIdentification As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleRequiresComputerIdentification As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEnableSimultaneousLogins As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleEnableSimultaneousLogins As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDateActivated As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDateActivated As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIsDisabled As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleIsDisabled As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colExpiryDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleExpiryDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colComments As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleComments As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastPasswords As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastPasswords As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colApplications As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleApplications As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsLanguage As System.Windows.Forms.BindingSource
  Friend WithEvents colLanguage As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleLanguage As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIsLockedOut As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleIsLockedOut As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsRole As System.Windows.Forms.BindingSource
  Friend WithEvents colRole As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colRoleText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleRole As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsAuthenticationMethod As System.Windows.Forms.BindingSource
  Friend WithEvents colAuthenticationMethod As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleAuthenticationMethod As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colRequiresFixedIP As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleRequiresFixedIP As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsMessagingMode As System.Windows.Forms.BindingSource
  Friend WithEvents colMessagingMode As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleMessagingMode As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLoggedInIP As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLoggedInIP As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colApprovalCodeHashed As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleApprovalCodeHashed As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colApprovalFunctionName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleApprovalFunctionName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colApprovalTime As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleApprovalTime As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastSuccessfulLogin As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastSuccessfulLogin As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPasswordNeverExpires As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisiblePasswordNeverExpires As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsSecurityQuestion1 As System.Windows.Forms.BindingSource
  Friend WithEvents colSecurityQuestion1 As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleSecurityQuestion1 As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSecurityQuestion1Response As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSecurityQuestion1Response As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsSecurityQuestion2 As System.Windows.Forms.BindingSource
  Friend WithEvents colSecurityQuestion2 As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleSecurityQuestion2 As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSecurityQuestion2Response As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSecurityQuestion2Response As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsSecurityQuestion3 As System.Windows.Forms.BindingSource
  Friend WithEvents colSecurityQuestion3 As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleSecurityQuestion3 As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSecurityQuestion3Response As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSecurityQuestion3Response As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPIN As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisiblePIN As System.Windows.Forms.ToolStripMenuItem 

End Class
