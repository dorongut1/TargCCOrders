<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccSupplierOrderCol
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
    Me.dgvSupplierOrder = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlSupplierOrder = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsOrderHeader = New System.Windows.Forms.BindingSource(Me.components)
    Me.colOrderHeader = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colOrderHeaderText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOrderHeader = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSupplierEmail = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSupplierEmail = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colEmailSubject = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleEmailSubject = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colEmailBody = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleEmailBody = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsEmailStatus = New System.Windows.Forms.BindingSource(Me.components)
    Me.colEmailStatus = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleEmailStatus = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colSentDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSentDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTotalCost = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsDeliveryMethod = New System.Windows.Forms.BindingSource(Me.components)
    Me.colDeliveryMethod = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleDeliveryMethod = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colRequestedDeliveryDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleRequestedDeliveryDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colRequestedDeliveryDay = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleRequestedDeliveryDay = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNotes = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNotes = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvSupplierOrder, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsEmailStatus, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsDeliveryMethod, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlSupplierOrder, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvSupplierOrder
    '
    Me.dgvSupplierOrder.AllowUserToAddRows = False
    Me.dgvSupplierOrder.AllowUserToDeleteRows = False
    Me.dgvSupplierOrder.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvSupplierOrder.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvSupplierOrder.AutoGenerateColumns = False
    Me.dgvSupplierOrder.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvSupplierOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvSupplierOrder.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvSupplierOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvSupplierOrder.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colOrderHeader, Me.colOrderHeaderText, Me.colSupplierEmail, Me.colEmailSubject, Me.colEmailBody, Me.colEmailStatus, Me.colSentDate, Me.colTotalCost, Me.colDeliveryMethod, Me.colRequestedDeliveryDate, Me.colRequestedDeliveryDay, Me.colNotes})
    Me.dgvSupplierOrder.DataSource = Me.bsCtlSupplierOrder
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvSupplierOrder.DefaultCellStyle = styleDefaultCell
    Me.dgvSupplierOrder.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvSupplierOrder.EnableHeadersVisualStyles = False
    Me.dgvSupplierOrder.Location = New System.Drawing.Point(0, 25)
    Me.dgvSupplierOrder.MultiSelect = False 
    Me.dgvSupplierOrder.ContextMenuStrip = Me.cmsGrid 
    Me.dgvSupplierOrder.Name = "dgvSupplierOrder"
    Me.dgvSupplierOrder.RowHeadersVisible = False
    Me.dgvSupplierOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvSupplierOrder.Size = New System.Drawing.Size(712, 347)
    Me.dgvSupplierOrder.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlSupplierOrder
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleOrderHeader, Me.mnuColVisibleSupplierEmail, Me.mnuColVisibleEmailSubject, Me.mnuColVisibleEmailBody, Me.mnuColVisibleEmailStatus, Me.mnuColVisibleSentDate, Me.mnuColVisibleTotalCost, Me.mnuColVisibleDeliveryMethod, Me.mnuColVisibleRequestedDeliveryDate, Me.mnuColVisibleRequestedDeliveryDay, Me.mnuColVisibleNotes, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsOrderHeader
    Me.bsOrderHeader.DataSource = GetType(clsComboList)
    'bsEmailStatus
    Me.bsEmailStatus.DataSource = GetType(clsComboList)
    'bsDeliveryMethod
    Me.bsDeliveryMethod.DataSource = GetType(clsComboList)
    '
    'bsCtlSupplierOrder
    '
    Me.bsCtlSupplierOrder.DataSource = GetType(clsSupplierOrder)
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
    'colOrderHeader
    '
    Me.colOrderHeader.DataPropertyName = "OrderHeaderID"
    Me.colOrderHeader.DataSource = Me.bsOrderHeader
    Me.colOrderHeader.DisplayMember = "Text"
    Me.colOrderHeader.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colOrderHeader.HeaderText = "Order Header"
    Me.colOrderHeader.Name = "colOrderHeader"
    Me.colOrderHeader.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOrderHeader.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOrderHeader.ValueMember = "KeyLong"
    Me.colOrderHeader.Width = 60
    Me.colOrderHeader.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colOrderHeaderText 
    ' 
    Me.colOrderHeaderText.DataPropertyName = "OrderHeaderText" 
    Me.colOrderHeaderText.HeaderText = "OrderHeader" 
    Me.colOrderHeaderText.Name = "colOrderHeaderText" 
    Me.colOrderHeaderText.ReadOnly = True 
    Me.colOrderHeader.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOrderHeader 
    ' 
    Me.mnuColVisibleOrderHeader.Checked = True 
    Me.mnuColVisibleOrderHeader.CheckOnClick = True 
    Me.mnuColVisibleOrderHeader.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOrderHeader.Name = "mnuColVisibleOrderHeader" 
    Me.mnuColVisibleOrderHeader.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOrderHeader.Text = "Order Header" 
    ' 
    'colSupplierEmail
    '
    Me.colSupplierEmail.DataPropertyName = "SupplierEmail"
    Me.colSupplierEmail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSupplierEmail.HeaderText = "Supplier Email"
    Me.colSupplierEmail.Name = "colSupplierEmail"
    Me.colSupplierEmail.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSupplierEmail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSupplierEmail.Width = 60
    Me.colSupplierEmail.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSupplierEmail 
    ' 
    Me.mnuColVisibleSupplierEmail.Checked = True 
    Me.mnuColVisibleSupplierEmail.CheckOnClick = True 
    Me.mnuColVisibleSupplierEmail.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSupplierEmail.Name = "mnuColVisibleSupplierEmail" 
    Me.mnuColVisibleSupplierEmail.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSupplierEmail.Text = "Supplier Email" 
    ' 
    'colEmailSubject
    '
    Me.colEmailSubject.DataPropertyName = "EmailSubject"
    Me.colEmailSubject.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colEmailSubject.HeaderText = "Email Subject"
    Me.colEmailSubject.Name = "colEmailSubject"
    Me.colEmailSubject.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEmailSubject.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEmailSubject.Width = 60
    Me.colEmailSubject.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEmailSubject 
    ' 
    Me.mnuColVisibleEmailSubject.Checked = True 
    Me.mnuColVisibleEmailSubject.CheckOnClick = True 
    Me.mnuColVisibleEmailSubject.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEmailSubject.Name = "mnuColVisibleEmailSubject" 
    Me.mnuColVisibleEmailSubject.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEmailSubject.Text = "Email Subject" 
    ' 
    'colEmailBody
    '
    Me.colEmailBody.DataPropertyName = "EmailBody"
    Me.colEmailBody.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colEmailBody.HeaderText = "Email Body"
    Me.colEmailBody.Name = "colEmailBody"
    Me.colEmailBody.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEmailBody.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEmailBody.Width = 60
    Me.colEmailBody.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEmailBody 
    ' 
    Me.mnuColVisibleEmailBody.Checked = True 
    Me.mnuColVisibleEmailBody.CheckOnClick = True 
    Me.mnuColVisibleEmailBody.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEmailBody.Name = "mnuColVisibleEmailBody" 
    Me.mnuColVisibleEmailBody.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEmailBody.Text = "Email Body" 
    ' 
    'colEmailStatus
    '
    Me.colEmailStatus.DataPropertyName = "EmailStatus"
    Me.colEmailStatus.DataSource = Me.bsEmailStatus
    Me.colEmailStatus.DisplayMember = "Text"
    Me.colEmailStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colEmailStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colEmailStatus.HeaderText = "Email Status"
    Me.colEmailStatus.Name = "colEmailStatus"
    Me.colEmailStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEmailStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEmailStatus.ValueMember = "KeyEnum"
    Me.colEmailStatus.Width = 60
    Me.colEmailStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEmailStatus 
    ' 
    Me.mnuColVisibleEmailStatus.Checked = True 
    Me.mnuColVisibleEmailStatus.CheckOnClick = True 
    Me.mnuColVisibleEmailStatus.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEmailStatus.Name = "mnuColVisibleEmailStatus" 
    Me.mnuColVisibleEmailStatus.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEmailStatus.Text = "Email Status" 
    ' 
    'colSentDate
    '
    Me.colSentDate.DataPropertyName = "SentDate"
    Me.colSentDate.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colSentDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colSentDate.HeaderText = "Sent Date"
    Me.colSentDate.Name = "colSentDate"
    Me.colSentDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSentDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSentDate.Width = 60
    Me.colSentDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSentDate 
    ' 
    Me.mnuColVisibleSentDate.Checked = True 
    Me.mnuColVisibleSentDate.CheckOnClick = True 
    Me.mnuColVisibleSentDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSentDate.Name = "mnuColVisibleSentDate" 
    Me.mnuColVisibleSentDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSentDate.Text = "Sent Date" 
    ' 
    'colTotalCost
    '
    Me.colTotalCost.DataPropertyName = "TotalCost"
    Me.colTotalCost.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colTotalCost.HeaderText = "Total Cost"
    Me.colTotalCost.Name = "colTotalCost"
    Me.colTotalCost.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTotalCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTotalCost.Width = 60
    Me.colTotalCost.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTotalCost 
    ' 
    Me.mnuColVisibleTotalCost.Checked = True 
    Me.mnuColVisibleTotalCost.CheckOnClick = True 
    Me.mnuColVisibleTotalCost.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTotalCost.Name = "mnuColVisibleTotalCost" 
    Me.mnuColVisibleTotalCost.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTotalCost.Text = "Total Cost" 
    ' 
    'colDeliveryMethod
    '
    Me.colDeliveryMethod.DataPropertyName = "DeliveryMethod"
    Me.colDeliveryMethod.DataSource = Me.bsDeliveryMethod
    Me.colDeliveryMethod.DisplayMember = "Text"
    Me.colDeliveryMethod.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colDeliveryMethod.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDeliveryMethod.HeaderText = "Delivery Method"
    Me.colDeliveryMethod.Name = "colDeliveryMethod"
    Me.colDeliveryMethod.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDeliveryMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDeliveryMethod.ValueMember = "KeyEnum"
    Me.colDeliveryMethod.Width = 60
    Me.colDeliveryMethod.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDeliveryMethod 
    ' 
    Me.mnuColVisibleDeliveryMethod.Checked = True 
    Me.mnuColVisibleDeliveryMethod.CheckOnClick = True 
    Me.mnuColVisibleDeliveryMethod.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDeliveryMethod.Name = "mnuColVisibleDeliveryMethod" 
    Me.mnuColVisibleDeliveryMethod.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDeliveryMethod.Text = "Delivery Method" 
    ' 
    'colRequestedDeliveryDate
    '
    Me.colRequestedDeliveryDate.DataPropertyName = "RequestedDeliveryDate"
    Me.colRequestedDeliveryDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colRequestedDeliveryDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colRequestedDeliveryDate.HeaderText = "Requested Delivery Date"
    Me.colRequestedDeliveryDate.Name = "colRequestedDeliveryDate"
    Me.colRequestedDeliveryDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRequestedDeliveryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRequestedDeliveryDate.Width = 60
    Me.colRequestedDeliveryDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRequestedDeliveryDate 
    ' 
    Me.mnuColVisibleRequestedDeliveryDate.Checked = True 
    Me.mnuColVisibleRequestedDeliveryDate.CheckOnClick = True 
    Me.mnuColVisibleRequestedDeliveryDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRequestedDeliveryDate.Name = "mnuColVisibleRequestedDeliveryDate" 
    Me.mnuColVisibleRequestedDeliveryDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRequestedDeliveryDate.Text = "Requested Delivery Date" 
    ' 
    'colRequestedDeliveryDay
    '
    Me.colRequestedDeliveryDay.DataPropertyName = "RequestedDeliveryDay"
    Me.colRequestedDeliveryDay.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colRequestedDeliveryDay.HeaderText = "Requested Delivery Day"
    Me.colRequestedDeliveryDay.Name = "colRequestedDeliveryDay"
    Me.colRequestedDeliveryDay.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRequestedDeliveryDay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRequestedDeliveryDay.Width = 60
    Me.colRequestedDeliveryDay.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRequestedDeliveryDay 
    ' 
    Me.mnuColVisibleRequestedDeliveryDay.Checked = True 
    Me.mnuColVisibleRequestedDeliveryDay.CheckOnClick = True 
    Me.mnuColVisibleRequestedDeliveryDay.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRequestedDeliveryDay.Name = "mnuColVisibleRequestedDeliveryDay" 
    Me.mnuColVisibleRequestedDeliveryDay.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRequestedDeliveryDay.Text = "Requested Delivery Day" 
    ' 
    'colNotes
    '
    Me.colNotes.DataPropertyName = "Notes"
    Me.colNotes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colNotes.HeaderText = "Notes"
    Me.colNotes.Name = "colNotes"
    Me.colNotes.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colNotes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colNotes.Width = 60
    Me.colNotes.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleNotes 
    ' 
    Me.mnuColVisibleNotes.Checked = True 
    Me.mnuColVisibleNotes.CheckOnClick = True 
    Me.mnuColVisibleNotes.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleNotes.Name = "mnuColVisibleNotes" 
    Me.mnuColVisibleNotes.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleNotes.Text = "Notes" 
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
    'ctlSupplierOrderCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvSupplierOrder)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccSupplierOrderCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvSupplierOrder, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsEmailStatus, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsDeliveryMethod, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlSupplierOrder, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvSupplierOrder As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlSupplierOrder As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsOrderHeader As System.Windows.Forms.BindingSource
  Friend WithEvents colOrderHeader As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colOrderHeaderText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleOrderHeader As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSupplierEmail As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSupplierEmail As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEmailSubject As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleEmailSubject As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEmailBody As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleEmailBody As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsEmailStatus As System.Windows.Forms.BindingSource
  Friend WithEvents colEmailStatus As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleEmailStatus As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSentDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSentDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTotalCost As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsDeliveryMethod As System.Windows.Forms.BindingSource
  Friend WithEvents colDeliveryMethod As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleDeliveryMethod As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colRequestedDeliveryDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleRequestedDeliveryDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colRequestedDeliveryDay As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleRequestedDeliveryDay As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNotes As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNotes As System.Windows.Forms.ToolStripMenuItem 

End Class
