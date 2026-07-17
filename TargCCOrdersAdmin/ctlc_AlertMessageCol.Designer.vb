<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_AlertMessageCol
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
    Me.dgvAlertMessage = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlAlertMessage = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNumber = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNumber = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDescription = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsSeverity = New System.Windows.Forms.BindingSource(Me.components)
    Me.colSeverity = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleSeverity = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colMessage = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleMessage = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colMessageLocalized = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleMessageLocalized = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colAction = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAction = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colActionLocalized = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleActionLocalized = New System.Windows.Forms.ToolStripMenuItem()  
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvAlertMessage, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsSeverity, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlAlertMessage, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvAlertMessage
    '
    Me.dgvAlertMessage.AllowUserToAddRows = False
    Me.dgvAlertMessage.AllowUserToDeleteRows = False
    Me.dgvAlertMessage.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvAlertMessage.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvAlertMessage.AutoGenerateColumns = False
    Me.dgvAlertMessage.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvAlertMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvAlertMessage.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvAlertMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvAlertMessage.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colNumber, Me.colDescription, Me.colType, Me.colSeverity, Me.colMessage, Me.colMessageLocalized, Me.colAction, Me.colActionLocalized})
    Me.dgvAlertMessage.DataSource = Me.bsCtlAlertMessage
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvAlertMessage.DefaultCellStyle = styleDefaultCell
    Me.dgvAlertMessage.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvAlertMessage.EnableHeadersVisualStyles = False
    Me.dgvAlertMessage.Location = New System.Drawing.Point(0, 25)
    Me.dgvAlertMessage.MultiSelect = False 
    Me.dgvAlertMessage.ContextMenuStrip = Me.cmsGrid 
    Me.dgvAlertMessage.Name = "dgvAlertMessage"
    Me.dgvAlertMessage.RowHeadersVisible = False
    Me.dgvAlertMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvAlertMessage.Size = New System.Drawing.Size(712, 347)
    Me.dgvAlertMessage.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlAlertMessage
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleNumber, Me.mnuColVisibleDescription, Me.mnuColVisibleType, Me.mnuColVisibleSeverity, Me.mnuColVisibleMessage, Me.mnuColVisibleMessageLocalized, Me.mnuColVisibleAction, Me.mnuColVisibleActionLocalized, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsType
    Me.bsType.DataSource = GetType(clsComboList)
    'bsSeverity
    Me.bsSeverity.DataSource = GetType(clsComboList)
    '
    'bsCtlAlertMessage
    '
    Me.bsCtlAlertMessage.DataSource = GetType(csAlertMessage)
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
    'colNumber
    '
    Me.colNumber.DataPropertyName = "Number"
    Me.colNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colNumber.HeaderText = "Number"
    Me.colNumber.Name = "colNumber"
    Me.colNumber.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colNumber.Width = 60
    Me.colNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleNumber 
    ' 
    Me.mnuColVisibleNumber.Checked = True 
    Me.mnuColVisibleNumber.CheckOnClick = True 
    Me.mnuColVisibleNumber.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleNumber.Name = "mnuColVisibleNumber" 
    Me.mnuColVisibleNumber.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleNumber.Text = "Number" 
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
    'colSeverity
    '
    Me.colSeverity.DataPropertyName = "Severity"
    Me.colSeverity.DataSource = Me.bsSeverity
    Me.colSeverity.DisplayMember = "Text"
    Me.colSeverity.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colSeverity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colSeverity.HeaderText = "Severity"
    Me.colSeverity.Name = "colSeverity"
    Me.colSeverity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSeverity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSeverity.ValueMember = "KeyEnum"
    Me.colSeverity.Width = 60
    Me.colSeverity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSeverity 
    ' 
    Me.mnuColVisibleSeverity.Checked = True 
    Me.mnuColVisibleSeverity.CheckOnClick = True 
    Me.mnuColVisibleSeverity.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSeverity.Name = "mnuColVisibleSeverity" 
    Me.mnuColVisibleSeverity.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSeverity.Text = "Severity" 
    ' 
    'colMessage
    '
    Me.colMessage.DataPropertyName = "Message"
    Me.colMessage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colMessage.HeaderText = "Message"
    Me.colMessage.Name = "colMessage"
    Me.colMessage.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colMessage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colMessage.Width = 60
    Me.colMessage.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleMessage 
    ' 
    Me.mnuColVisibleMessage.Checked = True 
    Me.mnuColVisibleMessage.CheckOnClick = True 
    Me.mnuColVisibleMessage.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleMessage.Name = "mnuColVisibleMessage" 
    Me.mnuColVisibleMessage.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleMessage.Text = "Message" 
    ' 
    'colMessageLocalized
    '
    Me.colMessageLocalized.DataPropertyName = "MessageLocalized"
    Me.colMessageLocalized.HeaderText = "MessageLocalized"
    Me.colMessageLocalized.Name = "colMessageLocalized"
    Me.colMessageLocalized.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colMessageLocalized.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colMessageLocalized.Width = 60
    Me.colMessage.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'mnuColVisibleMessageLocalized 
    ' 
    Me.mnuColVisibleMessageLocalized.Checked = True 
    Me.mnuColVisibleMessageLocalized.CheckOnClick = True 
    Me.mnuColVisibleMessageLocalized.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleMessageLocalized.Name = "mnuColVisibleMessageLocalized" 
    Me.mnuColVisibleMessage.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleMessageLocalized.Text = "MessageLocalized" 
    ' 
    'colAction
    '
    Me.colAction.DataPropertyName = "Action"
    Me.colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colAction.HeaderText = "Action"
    Me.colAction.Name = "colAction"
    Me.colAction.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAction.Width = 60
    Me.colAction.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAction 
    ' 
    Me.mnuColVisibleAction.Checked = True 
    Me.mnuColVisibleAction.CheckOnClick = True 
    Me.mnuColVisibleAction.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAction.Name = "mnuColVisibleAction" 
    Me.mnuColVisibleAction.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAction.Text = "Action" 
    ' 
    'colActionLocalized
    '
    Me.colActionLocalized.DataPropertyName = "ActionLocalized"
    Me.colActionLocalized.HeaderText = "ActionLocalized"
    Me.colActionLocalized.Name = "colActionLocalized"
    Me.colActionLocalized.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colActionLocalized.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colActionLocalized.Width = 60
    Me.colAction.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'mnuColVisibleActionLocalized 
    ' 
    Me.mnuColVisibleActionLocalized.Checked = True 
    Me.mnuColVisibleActionLocalized.CheckOnClick = True 
    Me.mnuColVisibleActionLocalized.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleActionLocalized.Name = "mnuColVisibleActionLocalized" 
    Me.mnuColVisibleAction.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleActionLocalized.Text = "ActionLocalized" 
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
    'ctlAlertMessageCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvAlertMessage)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_AlertMessageCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvAlertMessage, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsSeverity, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlAlertMessage, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvAlertMessage As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlAlertMessage As System.Windows.Forms.BindingSource
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
  Friend WithEvents colNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNumber As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDescription As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsType As System.Windows.Forms.BindingSource
  Friend WithEvents colType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsSeverity As System.Windows.Forms.BindingSource
  Friend WithEvents colSeverity As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleSeverity As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colMessage As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleMessage As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colMessageLocalized As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleMessageLocalized As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAction As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAction As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colActionLocalized As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleActionLocalized As System.Windows.Forms.ToolStripMenuItem 

End Class
