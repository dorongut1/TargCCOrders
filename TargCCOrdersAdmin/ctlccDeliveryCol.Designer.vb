<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccDeliveryCol
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
    Me.dgvDelivery = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlDelivery = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsOrderHeader = New System.Windows.Forms.BindingSource(Me.components)
    Me.colOrderHeader = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colOrderHeaderText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOrderHeader = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDeliveryAddress = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDeliveryAddress = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colContactPhone = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleContactPhone = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colContactName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleContactName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsDeliveryMethod = New System.Windows.Forms.BindingSource(Me.components)
    Me.colDeliveryMethod = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleDeliveryMethod = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colOrderedDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOrderedDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colReceivedDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleReceivedDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colArrivalToHubDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleArrivalToHubDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colArrivalToCustomerDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleArrivalToCustomerDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsDeliveryStatus = New System.Windows.Forms.BindingSource(Me.components)
    Me.colDeliveryStatus = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleDeliveryStatus = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colLocation = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLocation = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colProductsSummary = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProductsSummary = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNotes = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNotes = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsDeliveryMethod, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsDeliveryStatus, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvDelivery
    '
    Me.dgvDelivery.AllowUserToAddRows = False
    Me.dgvDelivery.AllowUserToDeleteRows = False
    Me.dgvDelivery.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvDelivery.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvDelivery.AutoGenerateColumns = False
    Me.dgvDelivery.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvDelivery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvDelivery.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvDelivery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvDelivery.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colOrderHeader, Me.colOrderHeaderText, Me.colDeliveryAddress, Me.colContactPhone, Me.colContactName, Me.colDeliveryMethod, Me.colOrderedDate, Me.colReceivedDate, Me.colArrivalToHubDate, Me.colArrivalToCustomerDate, Me.colDeliveryStatus, Me.colLocation, Me.colProductsSummary, Me.colNotes})
    Me.dgvDelivery.DataSource = Me.bsCtlDelivery
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvDelivery.DefaultCellStyle = styleDefaultCell
    Me.dgvDelivery.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvDelivery.EnableHeadersVisualStyles = False
    Me.dgvDelivery.Location = New System.Drawing.Point(0, 25)
    Me.dgvDelivery.MultiSelect = False 
    Me.dgvDelivery.ContextMenuStrip = Me.cmsGrid 
    Me.dgvDelivery.Name = "dgvDelivery"
    Me.dgvDelivery.RowHeadersVisible = False
    Me.dgvDelivery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvDelivery.Size = New System.Drawing.Size(712, 347)
    Me.dgvDelivery.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlDelivery
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleOrderHeader, Me.mnuColVisibleDeliveryAddress, Me.mnuColVisibleContactPhone, Me.mnuColVisibleContactName, Me.mnuColVisibleDeliveryMethod, Me.mnuColVisibleOrderedDate, Me.mnuColVisibleReceivedDate, Me.mnuColVisibleArrivalToHubDate, Me.mnuColVisibleArrivalToCustomerDate, Me.mnuColVisibleDeliveryStatus, Me.mnuColVisibleLocation, Me.mnuColVisibleProductsSummary, Me.mnuColVisibleNotes, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsOrderHeader
    Me.bsOrderHeader.DataSource = GetType(clsComboList)
    'bsDeliveryMethod
    Me.bsDeliveryMethod.DataSource = GetType(clsComboList)
    'bsDeliveryStatus
    Me.bsDeliveryStatus.DataSource = GetType(clsComboList)
    '
    'bsCtlDelivery
    '
    Me.bsCtlDelivery.DataSource = GetType(clsDelivery)
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
    'colDeliveryAddress
    '
    Me.colDeliveryAddress.DataPropertyName = "DeliveryAddress"
    Me.colDeliveryAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colDeliveryAddress.HeaderText = "Delivery Address"
    Me.colDeliveryAddress.Name = "colDeliveryAddress"
    Me.colDeliveryAddress.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDeliveryAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDeliveryAddress.Width = 60
    Me.colDeliveryAddress.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDeliveryAddress 
    ' 
    Me.mnuColVisibleDeliveryAddress.Checked = True 
    Me.mnuColVisibleDeliveryAddress.CheckOnClick = True 
    Me.mnuColVisibleDeliveryAddress.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDeliveryAddress.Name = "mnuColVisibleDeliveryAddress" 
    Me.mnuColVisibleDeliveryAddress.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDeliveryAddress.Text = "Delivery Address" 
    ' 
    'colContactPhone
    '
    Me.colContactPhone.DataPropertyName = "ContactPhone"
    Me.colContactPhone.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colContactPhone.HeaderText = "Contact Phone"
    Me.colContactPhone.Name = "colContactPhone"
    Me.colContactPhone.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colContactPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colContactPhone.Width = 60
    Me.colContactPhone.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleContactPhone 
    ' 
    Me.mnuColVisibleContactPhone.Checked = True 
    Me.mnuColVisibleContactPhone.CheckOnClick = True 
    Me.mnuColVisibleContactPhone.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleContactPhone.Name = "mnuColVisibleContactPhone" 
    Me.mnuColVisibleContactPhone.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleContactPhone.Text = "Contact Phone" 
    ' 
    'colContactName
    '
    Me.colContactName.DataPropertyName = "ContactName"
    Me.colContactName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colContactName.HeaderText = "Contact Name"
    Me.colContactName.Name = "colContactName"
    Me.colContactName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colContactName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colContactName.Width = 60
    Me.colContactName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleContactName 
    ' 
    Me.mnuColVisibleContactName.Checked = True 
    Me.mnuColVisibleContactName.CheckOnClick = True 
    Me.mnuColVisibleContactName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleContactName.Name = "mnuColVisibleContactName" 
    Me.mnuColVisibleContactName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleContactName.Text = "Contact Name" 
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
    'colOrderedDate
    '
    Me.colOrderedDate.DataPropertyName = "OrderedDate"
    Me.colOrderedDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colOrderedDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colOrderedDate.HeaderText = "Ordered Date"
    Me.colOrderedDate.Name = "colOrderedDate"
    Me.colOrderedDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOrderedDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOrderedDate.Width = 60
    Me.colOrderedDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOrderedDate 
    ' 
    Me.mnuColVisibleOrderedDate.Checked = True 
    Me.mnuColVisibleOrderedDate.CheckOnClick = True 
    Me.mnuColVisibleOrderedDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOrderedDate.Name = "mnuColVisibleOrderedDate" 
    Me.mnuColVisibleOrderedDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOrderedDate.Text = "Ordered Date" 
    ' 
    'colReceivedDate
    '
    Me.colReceivedDate.DataPropertyName = "ReceivedDate"
    Me.colReceivedDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colReceivedDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colReceivedDate.HeaderText = "Received Date"
    Me.colReceivedDate.Name = "colReceivedDate"
    Me.colReceivedDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colReceivedDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colReceivedDate.Width = 60
    Me.colReceivedDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleReceivedDate 
    ' 
    Me.mnuColVisibleReceivedDate.Checked = True 
    Me.mnuColVisibleReceivedDate.CheckOnClick = True 
    Me.mnuColVisibleReceivedDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleReceivedDate.Name = "mnuColVisibleReceivedDate" 
    Me.mnuColVisibleReceivedDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleReceivedDate.Text = "Received Date" 
    ' 
    'colArrivalToHubDate
    '
    Me.colArrivalToHubDate.DataPropertyName = "ArrivalToHubDate"
    Me.colArrivalToHubDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colArrivalToHubDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colArrivalToHubDate.HeaderText = "Arrival To Hub Date"
    Me.colArrivalToHubDate.Name = "colArrivalToHubDate"
    Me.colArrivalToHubDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colArrivalToHubDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colArrivalToHubDate.Width = 60
    Me.colArrivalToHubDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleArrivalToHubDate 
    ' 
    Me.mnuColVisibleArrivalToHubDate.Checked = True 
    Me.mnuColVisibleArrivalToHubDate.CheckOnClick = True 
    Me.mnuColVisibleArrivalToHubDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleArrivalToHubDate.Name = "mnuColVisibleArrivalToHubDate" 
    Me.mnuColVisibleArrivalToHubDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleArrivalToHubDate.Text = "Arrival To Hub Date" 
    ' 
    'colArrivalToCustomerDate
    '
    Me.colArrivalToCustomerDate.DataPropertyName = "ArrivalToCustomerDate"
    Me.colArrivalToCustomerDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colArrivalToCustomerDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colArrivalToCustomerDate.HeaderText = "Arrival To Customer Date"
    Me.colArrivalToCustomerDate.Name = "colArrivalToCustomerDate"
    Me.colArrivalToCustomerDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colArrivalToCustomerDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colArrivalToCustomerDate.Width = 60
    Me.colArrivalToCustomerDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleArrivalToCustomerDate 
    ' 
    Me.mnuColVisibleArrivalToCustomerDate.Checked = True 
    Me.mnuColVisibleArrivalToCustomerDate.CheckOnClick = True 
    Me.mnuColVisibleArrivalToCustomerDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleArrivalToCustomerDate.Name = "mnuColVisibleArrivalToCustomerDate" 
    Me.mnuColVisibleArrivalToCustomerDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleArrivalToCustomerDate.Text = "Arrival To Customer Date" 
    ' 
    'colDeliveryStatus
    '
    Me.colDeliveryStatus.DataPropertyName = "DeliveryStatus"
    Me.colDeliveryStatus.DataSource = Me.bsDeliveryStatus
    Me.colDeliveryStatus.DisplayMember = "Text"
    Me.colDeliveryStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colDeliveryStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDeliveryStatus.HeaderText = "Delivery Status"
    Me.colDeliveryStatus.Name = "colDeliveryStatus"
    Me.colDeliveryStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDeliveryStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDeliveryStatus.ValueMember = "KeyEnum"
    Me.colDeliveryStatus.Width = 60
    Me.colDeliveryStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDeliveryStatus 
    ' 
    Me.mnuColVisibleDeliveryStatus.Checked = True 
    Me.mnuColVisibleDeliveryStatus.CheckOnClick = True 
    Me.mnuColVisibleDeliveryStatus.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDeliveryStatus.Name = "mnuColVisibleDeliveryStatus" 
    Me.mnuColVisibleDeliveryStatus.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDeliveryStatus.Text = "Delivery Status" 
    ' 
    'colLocation
    '
    Me.colLocation.DataPropertyName = "Location"
    Me.colLocation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colLocation.HeaderText = "Location"
    Me.colLocation.Name = "colLocation"
    Me.colLocation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLocation.Width = 60
    Me.colLocation.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLocation 
    ' 
    Me.mnuColVisibleLocation.Checked = True 
    Me.mnuColVisibleLocation.CheckOnClick = True 
    Me.mnuColVisibleLocation.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLocation.Name = "mnuColVisibleLocation" 
    Me.mnuColVisibleLocation.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLocation.Text = "Location" 
    ' 
    'colProductsSummary
    '
    Me.colProductsSummary.DataPropertyName = "ProductsSummary"
    Me.colProductsSummary.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colProductsSummary.HeaderText = "Products Summary"
    Me.colProductsSummary.Name = "colProductsSummary"
    Me.colProductsSummary.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colProductsSummary.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colProductsSummary.Width = 60
    Me.colProductsSummary.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleProductsSummary 
    ' 
    Me.mnuColVisibleProductsSummary.Checked = True 
    Me.mnuColVisibleProductsSummary.CheckOnClick = True 
    Me.mnuColVisibleProductsSummary.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleProductsSummary.Name = "mnuColVisibleProductsSummary" 
    Me.mnuColVisibleProductsSummary.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleProductsSummary.Text = "Products Summary" 
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
    'ctlDeliveryCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvDelivery)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccDeliveryCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvDelivery, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsDeliveryMethod, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsDeliveryStatus, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlDelivery, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvDelivery As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlDelivery As System.Windows.Forms.BindingSource
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
  Friend WithEvents colDeliveryAddress As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDeliveryAddress As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colContactPhone As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleContactPhone As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colContactName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleContactName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsDeliveryMethod As System.Windows.Forms.BindingSource
  Friend WithEvents colDeliveryMethod As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleDeliveryMethod As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colOrderedDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleOrderedDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colReceivedDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleReceivedDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colArrivalToHubDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleArrivalToHubDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colArrivalToCustomerDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleArrivalToCustomerDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsDeliveryStatus As System.Windows.Forms.BindingSource
  Friend WithEvents colDeliveryStatus As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleDeliveryStatus As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLocation As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLocation As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colProductsSummary As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleProductsSummary As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNotes As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNotes As System.Windows.Forms.ToolStripMenuItem 

End Class
