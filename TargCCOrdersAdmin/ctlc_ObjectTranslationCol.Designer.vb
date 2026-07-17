<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_ObjectTranslationCol
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
    Me.dgvObjectTranslation = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlObjectTranslation = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsObjectToTranslate = New System.Windows.Forms.BindingSource(Me.components)
    Me.colObjectToTranslate = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colObjectToTranslateText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleObjectToTranslate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colInstance = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleInstance = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDefaultText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDefaultText = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsLanguage = New System.Windows.Forms.BindingSource(Me.components)
    Me.colLanguage = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleLanguage = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleText = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colInstanceUniqueText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleInstanceUniqueText = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvObjectTranslation, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsObjectToTranslate, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsLanguage, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlObjectTranslation, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvObjectTranslation
    '
    Me.dgvObjectTranslation.AllowUserToAddRows = False
    Me.dgvObjectTranslation.AllowUserToDeleteRows = False
    Me.dgvObjectTranslation.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvObjectTranslation.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvObjectTranslation.AutoGenerateColumns = False
    Me.dgvObjectTranslation.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvObjectTranslation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvObjectTranslation.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvObjectTranslation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvObjectTranslation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colObjectToTranslate, Me.colObjectToTranslateText, Me.colInstance, Me.colDefaultText, Me.colLanguage, Me.colText, Me.colInstanceUniqueText})
    Me.dgvObjectTranslation.DataSource = Me.bsCtlObjectTranslation
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvObjectTranslation.DefaultCellStyle = styleDefaultCell
    Me.dgvObjectTranslation.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvObjectTranslation.EnableHeadersVisualStyles = False
    Me.dgvObjectTranslation.Location = New System.Drawing.Point(0, 25)
    Me.dgvObjectTranslation.MultiSelect = False 
    Me.dgvObjectTranslation.ContextMenuStrip = Me.cmsGrid 
    Me.dgvObjectTranslation.Name = "dgvObjectTranslation"
    Me.dgvObjectTranslation.RowHeadersVisible = False
    Me.dgvObjectTranslation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvObjectTranslation.Size = New System.Drawing.Size(712, 347)
    Me.dgvObjectTranslation.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlObjectTranslation
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleObjectToTranslate, Me.mnuColVisibleInstance, Me.mnuColVisibleDefaultText, Me.mnuColVisibleLanguage, Me.mnuColVisibleText, Me.mnuColVisibleInstanceUniqueText, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsObjectToTranslate
    Me.bsObjectToTranslate.DataSource = GetType(clsComboList)
    'bsLanguage
    Me.bsLanguage.DataSource = GetType(clsComboList)
    '
    'bsCtlObjectTranslation
    '
    Me.bsCtlObjectTranslation.DataSource = GetType(csObjectTranslation)
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
    'colObjectToTranslate
    '
    Me.colObjectToTranslate.DataPropertyName = "ObjectToTranslateID"
    Me.colObjectToTranslate.DataSource = Me.bsObjectToTranslate
    Me.colObjectToTranslate.DisplayMember = "Text"
    Me.colObjectToTranslate.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colObjectToTranslate.HeaderText = "Object To Translate"
    Me.colObjectToTranslate.Name = "colObjectToTranslate"
    Me.colObjectToTranslate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colObjectToTranslate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colObjectToTranslate.ValueMember = "KeyLong"
    Me.colObjectToTranslate.Width = 60
    Me.colObjectToTranslate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colObjectToTranslateText 
    ' 
    Me.colObjectToTranslateText.DataPropertyName = "ObjectToTranslateText" 
    Me.colObjectToTranslateText.HeaderText = "ObjectToTranslate" 
    Me.colObjectToTranslateText.Name = "colObjectToTranslateText" 
    Me.colObjectToTranslateText.ReadOnly = True 
    Me.colObjectToTranslate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleObjectToTranslate 
    ' 
    Me.mnuColVisibleObjectToTranslate.Checked = True 
    Me.mnuColVisibleObjectToTranslate.CheckOnClick = True 
    Me.mnuColVisibleObjectToTranslate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleObjectToTranslate.Name = "mnuColVisibleObjectToTranslate" 
    Me.mnuColVisibleObjectToTranslate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleObjectToTranslate.Text = "Object To Translate" 
    ' 
    'colInstance
    '
    Me.colInstance.DataPropertyName = "Instance"
    Me.colInstance.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colInstance.HeaderText = "Instance"
    Me.colInstance.Name = "colInstance"
    Me.colInstance.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colInstance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colInstance.Width = 60
    Me.colInstance.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleInstance 
    ' 
    Me.mnuColVisibleInstance.Checked = True 
    Me.mnuColVisibleInstance.CheckOnClick = True 
    Me.mnuColVisibleInstance.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleInstance.Name = "mnuColVisibleInstance" 
    Me.mnuColVisibleInstance.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleInstance.Text = "Instance" 
    ' 
    'colDefaultText
    '
    Me.colDefaultText.DataPropertyName = "DefaultText"
    Me.colDefaultText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colDefaultText.HeaderText = "Default Text"
    Me.colDefaultText.Name = "colDefaultText"
    Me.colDefaultText.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDefaultText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDefaultText.Width = 60
    Me.colDefaultText.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDefaultText 
    ' 
    Me.mnuColVisibleDefaultText.Checked = True 
    Me.mnuColVisibleDefaultText.CheckOnClick = True 
    Me.mnuColVisibleDefaultText.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDefaultText.Name = "mnuColVisibleDefaultText" 
    Me.mnuColVisibleDefaultText.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDefaultText.Text = "Default Text" 
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
    'colInstanceUniqueText
    '
    Me.colInstanceUniqueText.DataPropertyName = "InstanceUniqueText"
    Me.colInstanceUniqueText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colInstanceUniqueText.HeaderText = "Instance Unique Text"
    Me.colInstanceUniqueText.Name = "colInstanceUniqueText"
    Me.colInstanceUniqueText.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colInstanceUniqueText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colInstanceUniqueText.Width = 60
    Me.colInstanceUniqueText.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleInstanceUniqueText 
    ' 
    Me.mnuColVisibleInstanceUniqueText.Checked = True 
    Me.mnuColVisibleInstanceUniqueText.CheckOnClick = True 
    Me.mnuColVisibleInstanceUniqueText.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleInstanceUniqueText.Name = "mnuColVisibleInstanceUniqueText" 
    Me.mnuColVisibleInstanceUniqueText.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleInstanceUniqueText.Text = "Instance Unique Text" 
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
    'ctlObjectTranslationCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvObjectTranslation)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_ObjectTranslationCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvObjectTranslation, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsObjectToTranslate, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsLanguage, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlObjectTranslation, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvObjectTranslation As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlObjectTranslation As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsObjectToTranslate As System.Windows.Forms.BindingSource
  Friend WithEvents colObjectToTranslate As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colObjectToTranslateText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleObjectToTranslate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colInstance As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleInstance As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDefaultText As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDefaultText As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsLanguage As System.Windows.Forms.BindingSource
  Friend WithEvents colLanguage As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleLanguage As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colText As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleText As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colInstanceUniqueText As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleInstanceUniqueText As System.Windows.Forms.ToolStripMenuItem 

End Class
