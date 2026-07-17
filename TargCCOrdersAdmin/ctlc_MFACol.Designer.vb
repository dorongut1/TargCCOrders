<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_MFACol
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
    Me.dgvMFA = New System.Windows.Forms.DataGridView() 
    Me.BN = New System.Windows.Forms.BindingNavigator(Me.components)
    Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator() 
    Me.btnEdit = New System.Windows.Forms.ToolStripButton() 
    Me.btnImport = New System.Windows.Forms.ToolStripButton() 
    Me.btnAdd = New System.Windows.Forms.ToolStripButton() 
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
    Me.bsCtlMFA = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCellOrEmail = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCellOrEmail = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colProtectedFunction = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProtectedFunction = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCodeHashed = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCodeHashed = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAttemptNo = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAttemptNo = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colIsSuccessful = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleIsSuccessful = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colLastAccessingIP = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastAccessingIP = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLastAccessingCountry = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastAccessingCountry = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsUILang = New System.Windows.Forms.BindingSource(Me.components)
    Me.colUILang = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleUILang = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colWhenCreated = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleWhenCreated = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colWhenAccessed = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleWhenAccessed = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colWhenExpires = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleWhenExpires = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDetails = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDetails = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsUser = New System.Windows.Forms.BindingSource(Me.components)
    Me.colUser = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colUserText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvMFA, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsUILang, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsUser, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlMFA, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvMFA
    '
    Me.dgvMFA.AllowUserToAddRows = False
    Me.dgvMFA.AllowUserToDeleteRows = False
    Me.dgvMFA.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvMFA.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvMFA.AutoGenerateColumns = False
    Me.dgvMFA.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvMFA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvMFA.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvMFA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvMFA.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colCellOrEmail, Me.colProtectedFunction, Me.colCodeHashed, Me.colAttemptNo, Me.colIsSuccessful, Me.colLastAccessingIP, Me.colLastAccessingCountry, Me.colUILang, Me.colWhenCreated, Me.colWhenAccessed, Me.colWhenExpires, Me.colDetails, Me.colUser, Me.colUserText})
    Me.dgvMFA.DataSource = Me.bsCtlMFA
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvMFA.DefaultCellStyle = styleDefaultCell
    Me.dgvMFA.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvMFA.EnableHeadersVisualStyles = False
    Me.dgvMFA.Location = New System.Drawing.Point(0, 25)
    Me.dgvMFA.MultiSelect = False 
    Me.dgvMFA.ContextMenuStrip = Me.cmsGrid 
    Me.dgvMFA.Name = "dgvMFA"
    Me.dgvMFA.RowHeadersVisible = False
    Me.dgvMFA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvMFA.Size = New System.Drawing.Size(712, 347)
    Me.dgvMFA.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlMFA
    Me.BN.CountItem = Nothing
    Me.BN.DeleteItem = Nothing
    Me.BN.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.BN.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorSeparator, Me.btnEdit, Me.btnAdd, Me.btnImport, Me.btnCeaseEdit, Me.tssEditMode, Me.lblEditMode, Me.tssReports, Me.btnSpreadsheet, Me.lblStatus, Me.btnReport, Me.tssColumns, Me.btnColumns})
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleCellOrEmail, Me.mnuColVisibleProtectedFunction, Me.mnuColVisibleCodeHashed, Me.mnuColVisibleAttemptNo, Me.mnuColVisibleIsSuccessful, Me.mnuColVisibleLastAccessingIP, Me.mnuColVisibleLastAccessingCountry, Me.mnuColVisibleUILang, Me.mnuColVisibleWhenCreated, Me.mnuColVisibleWhenAccessed, Me.mnuColVisibleWhenExpires, Me.mnuColVisibleDetails, Me.mnuColVisibleUser, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsUILang
    Me.bsUILang.DataSource = GetType(clsComboList)
    'bsUser
    Me.bsUser.DataSource = GetType(clsComboList)
    '
    'bsCtlMFA
    '
    Me.bsCtlMFA.DataSource = GetType(csMFA)
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
    'colCellOrEmail
    '
    Me.colCellOrEmail.DataPropertyName = "CellOrEmail"
    Me.colCellOrEmail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCellOrEmail.HeaderText = "Cell Or Email"
    Me.colCellOrEmail.Name = "colCellOrEmail"
    Me.colCellOrEmail.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCellOrEmail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCellOrEmail.Width = 60
    Me.colCellOrEmail.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCellOrEmail 
    ' 
    Me.mnuColVisibleCellOrEmail.Checked = True 
    Me.mnuColVisibleCellOrEmail.CheckOnClick = True 
    Me.mnuColVisibleCellOrEmail.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCellOrEmail.Name = "mnuColVisibleCellOrEmail" 
    Me.mnuColVisibleCellOrEmail.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCellOrEmail.Text = "Cell Or Email" 
    ' 
    'colProtectedFunction
    '
    Me.colProtectedFunction.DataPropertyName = "ProtectedFunction"
    Me.colProtectedFunction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colProtectedFunction.HeaderText = "Protected Function"
    Me.colProtectedFunction.Name = "colProtectedFunction"
    Me.colProtectedFunction.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colProtectedFunction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colProtectedFunction.Width = 60
    Me.colProtectedFunction.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleProtectedFunction 
    ' 
    Me.mnuColVisibleProtectedFunction.Checked = True 
    Me.mnuColVisibleProtectedFunction.CheckOnClick = True 
    Me.mnuColVisibleProtectedFunction.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleProtectedFunction.Name = "mnuColVisibleProtectedFunction" 
    Me.mnuColVisibleProtectedFunction.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleProtectedFunction.Text = "Protected Function" 
    ' 
    'colCodeHashed
    '
    Me.colCodeHashed.DataPropertyName = "CodeHashed"
    Me.colCodeHashed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCodeHashed.HeaderText = "Code Hashed"
    Me.colCodeHashed.Name = "colCodeHashed"
    Me.colCodeHashed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCodeHashed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCodeHashed.Width = 60
    Me.colCodeHashed.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCodeHashed 
    ' 
    Me.mnuColVisibleCodeHashed.Checked = True 
    Me.mnuColVisibleCodeHashed.CheckOnClick = True 
    Me.mnuColVisibleCodeHashed.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCodeHashed.Name = "mnuColVisibleCodeHashed" 
    Me.mnuColVisibleCodeHashed.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCodeHashed.Text = "Code Hashed" 
    ' 
    'colAttemptNo
    '
    Me.colAttemptNo.DataPropertyName = "AttemptNo"
    Me.colAttemptNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colAttemptNo.HeaderText = "Attempt No"
    Me.colAttemptNo.Name = "colAttemptNo"
    Me.colAttemptNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAttemptNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAttemptNo.Width = 60
    Me.colAttemptNo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAttemptNo 
    ' 
    Me.mnuColVisibleAttemptNo.Checked = True 
    Me.mnuColVisibleAttemptNo.CheckOnClick = True 
    Me.mnuColVisibleAttemptNo.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAttemptNo.Name = "mnuColVisibleAttemptNo" 
    Me.mnuColVisibleAttemptNo.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAttemptNo.Text = "Attempt No" 
    ' 
    'colIsSuccessful
    '
    Me.colIsSuccessful.DataPropertyName = "IsSuccessful"
    Me.colIsSuccessful.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colIsSuccessful.HeaderText = "Is Successful"
    Me.colIsSuccessful.Name = "colIsSuccessful"
    Me.colIsSuccessful.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIsSuccessful.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIsSuccessful.Width = 60
    Me.colIsSuccessful.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIsSuccessful 
    ' 
    Me.mnuColVisibleIsSuccessful.Checked = True 
    Me.mnuColVisibleIsSuccessful.CheckOnClick = True 
    Me.mnuColVisibleIsSuccessful.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIsSuccessful.Name = "mnuColVisibleIsSuccessful" 
    Me.mnuColVisibleIsSuccessful.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIsSuccessful.Text = "Is Successful" 
    ' 
    'colLastAccessingIP
    '
    Me.colLastAccessingIP.DataPropertyName = "LastAccessingIP"
    Me.colLastAccessingIP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colLastAccessingIP.HeaderText = "Last Accessing IP"
    Me.colLastAccessingIP.Name = "colLastAccessingIP"
    Me.colLastAccessingIP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLastAccessingIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLastAccessingIP.Width = 60
    Me.colLastAccessingIP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLastAccessingIP 
    ' 
    Me.mnuColVisibleLastAccessingIP.Checked = True 
    Me.mnuColVisibleLastAccessingIP.CheckOnClick = True 
    Me.mnuColVisibleLastAccessingIP.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLastAccessingIP.Name = "mnuColVisibleLastAccessingIP" 
    Me.mnuColVisibleLastAccessingIP.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLastAccessingIP.Text = "Last Accessing IP" 
    ' 
    'colLastAccessingCountry
    '
    Me.colLastAccessingCountry.DataPropertyName = "LastAccessingCountry"
    Me.colLastAccessingCountry.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colLastAccessingCountry.HeaderText = "Last Accessing Country"
    Me.colLastAccessingCountry.Name = "colLastAccessingCountry"
    Me.colLastAccessingCountry.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLastAccessingCountry.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLastAccessingCountry.Width = 60
    Me.colLastAccessingCountry.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLastAccessingCountry 
    ' 
    Me.mnuColVisibleLastAccessingCountry.Checked = True 
    Me.mnuColVisibleLastAccessingCountry.CheckOnClick = True 
    Me.mnuColVisibleLastAccessingCountry.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLastAccessingCountry.Name = "mnuColVisibleLastAccessingCountry" 
    Me.mnuColVisibleLastAccessingCountry.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLastAccessingCountry.Text = "Last Accessing Country" 
    ' 
    'colUILang
    '
    Me.colUILang.DataPropertyName = "UILang"
    Me.colUILang.DataSource = Me.bsUILang
    Me.colUILang.DisplayMember = "Text"
    Me.colUILang.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colUILang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colUILang.HeaderText = "UI Lang"
    Me.colUILang.Name = "colUILang"
    Me.colUILang.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUILang.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUILang.ValueMember = "KeyEnum"
    Me.colUILang.Width = 60
    Me.colUILang.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUILang 
    ' 
    Me.mnuColVisibleUILang.Checked = True 
    Me.mnuColVisibleUILang.CheckOnClick = True 
    Me.mnuColVisibleUILang.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUILang.Name = "mnuColVisibleUILang" 
    Me.mnuColVisibleUILang.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUILang.Text = "UI Lang" 
    ' 
    'colWhenCreated
    '
    Me.colWhenCreated.DataPropertyName = "WhenCreated"
    Me.colWhenCreated.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss zzz"
    Me.colWhenCreated.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colWhenCreated.HeaderText = "When Created"
    Me.colWhenCreated.Name = "colWhenCreated"
    Me.colWhenCreated.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWhenCreated.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWhenCreated.Width = 60
    Me.colWhenCreated.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWhenCreated 
    ' 
    Me.mnuColVisibleWhenCreated.Checked = True 
    Me.mnuColVisibleWhenCreated.CheckOnClick = True 
    Me.mnuColVisibleWhenCreated.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWhenCreated.Name = "mnuColVisibleWhenCreated" 
    Me.mnuColVisibleWhenCreated.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWhenCreated.Text = "When Created" 
    ' 
    'colWhenAccessed
    '
    Me.colWhenAccessed.DataPropertyName = "WhenAccessed"
    Me.colWhenAccessed.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss zzz"
    Me.colWhenAccessed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colWhenAccessed.HeaderText = "When Accessed"
    Me.colWhenAccessed.Name = "colWhenAccessed"
    Me.colWhenAccessed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWhenAccessed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWhenAccessed.Width = 60
    Me.colWhenAccessed.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWhenAccessed 
    ' 
    Me.mnuColVisibleWhenAccessed.Checked = True 
    Me.mnuColVisibleWhenAccessed.CheckOnClick = True 
    Me.mnuColVisibleWhenAccessed.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWhenAccessed.Name = "mnuColVisibleWhenAccessed" 
    Me.mnuColVisibleWhenAccessed.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWhenAccessed.Text = "When Accessed" 
    ' 
    'colWhenExpires
    '
    Me.colWhenExpires.DataPropertyName = "WhenExpires"
    Me.colWhenExpires.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss zzz"
    Me.colWhenExpires.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colWhenExpires.HeaderText = "When Expires"
    Me.colWhenExpires.Name = "colWhenExpires"
    Me.colWhenExpires.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWhenExpires.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWhenExpires.Width = 60
    Me.colWhenExpires.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWhenExpires 
    ' 
    Me.mnuColVisibleWhenExpires.Checked = True 
    Me.mnuColVisibleWhenExpires.CheckOnClick = True 
    Me.mnuColVisibleWhenExpires.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWhenExpires.Name = "mnuColVisibleWhenExpires" 
    Me.mnuColVisibleWhenExpires.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWhenExpires.Text = "When Expires" 
    ' 
    'colDetails
    '
    Me.colDetails.DataPropertyName = "Details"
    Me.colDetails.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colDetails.HeaderText = "Details"
    Me.colDetails.Name = "colDetails"
    Me.colDetails.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDetails.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDetails.Width = 60
    Me.colDetails.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDetails 
    ' 
    Me.mnuColVisibleDetails.Checked = True 
    Me.mnuColVisibleDetails.CheckOnClick = True 
    Me.mnuColVisibleDetails.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDetails.Name = "mnuColVisibleDetails" 
    Me.mnuColVisibleDetails.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDetails.Text = "Details" 
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
    'ctlMFACol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvMFA)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_MFACol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvMFA, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsUILang, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsUser, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlMFA, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvMFA As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlMFA As System.Windows.Forms.BindingSource
  Friend WithEvents BN As System.Windows.Forms.BindingNavigator
  Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnImport As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnAdd As System.Windows.Forms.ToolStripButton
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
  Friend WithEvents colCellOrEmail As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCellOrEmail As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colProtectedFunction As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleProtectedFunction As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCodeHashed As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCodeHashed As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAttemptNo As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAttemptNo As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIsSuccessful As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleIsSuccessful As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastAccessingIP As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastAccessingIP As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastAccessingCountry As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastAccessingCountry As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsUILang As System.Windows.Forms.BindingSource
  Friend WithEvents colUILang As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleUILang As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWhenCreated As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleWhenCreated As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWhenAccessed As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleWhenAccessed As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWhenExpires As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleWhenExpires As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDetails As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDetails As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsUser As System.Windows.Forms.BindingSource
  Friend WithEvents colUser As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colUserText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleUser As System.Windows.Forms.ToolStripMenuItem 

End Class
