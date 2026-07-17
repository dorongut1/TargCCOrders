<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_MailCol
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
    Me.dgvMail = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlMail = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsMessagingMode = New System.Windows.Forms.BindingSource(Me.components)
    Me.colMessagingMode = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleMessagingMode = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colRecipientEmail = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleRecipientEmail = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colWhenSent = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleWhenSent = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSubject = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSubject = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colBody = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleBody = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colWhenSeen = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleWhenSeen = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colWasSeen = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleWasSeen = New System.Windows.Forms.ToolStripMenuItem()  
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvMail, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsMessagingMode, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlMail, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvMail
    '
    Me.dgvMail.AllowUserToAddRows = False
    Me.dgvMail.AllowUserToDeleteRows = False
    Me.dgvMail.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvMail.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvMail.AutoGenerateColumns = False
    Me.dgvMail.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvMail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvMail.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvMail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colMessagingMode, Me.colRecipientEmail, Me.colWhenSent, Me.colSubject, Me.colBody, Me.colWhenSeen, Me.colWasSeen})
    Me.dgvMail.DataSource = Me.bsCtlMail
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvMail.DefaultCellStyle = styleDefaultCell
    Me.dgvMail.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvMail.EnableHeadersVisualStyles = False
    Me.dgvMail.Location = New System.Drawing.Point(0, 25)
    Me.dgvMail.MultiSelect = False 
    Me.dgvMail.ContextMenuStrip = Me.cmsGrid 
    Me.dgvMail.Name = "dgvMail"
    Me.dgvMail.RowHeadersVisible = False
    Me.dgvMail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvMail.Size = New System.Drawing.Size(712, 347)
    Me.dgvMail.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlMail
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleMessagingMode, Me.mnuColVisibleRecipientEmail, Me.mnuColVisibleWhenSent, Me.mnuColVisibleSubject, Me.mnuColVisibleBody, Me.mnuColVisibleWhenSeen, Me.mnuColVisibleWasSeen, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsMessagingMode
    Me.bsMessagingMode.DataSource = GetType(clsComboList)
    '
    'bsCtlMail
    '
    Me.bsCtlMail.DataSource = GetType(csMail)
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
    'colRecipientEmail
    '
    Me.colRecipientEmail.DataPropertyName = "RecipientEmail"
    Me.colRecipientEmail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colRecipientEmail.HeaderText = "Recipient Email"
    Me.colRecipientEmail.Name = "colRecipientEmail"
    Me.colRecipientEmail.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRecipientEmail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRecipientEmail.Width = 60
    Me.colRecipientEmail.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRecipientEmail 
    ' 
    Me.mnuColVisibleRecipientEmail.Checked = True 
    Me.mnuColVisibleRecipientEmail.CheckOnClick = True 
    Me.mnuColVisibleRecipientEmail.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRecipientEmail.Name = "mnuColVisibleRecipientEmail" 
    Me.mnuColVisibleRecipientEmail.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRecipientEmail.Text = "Recipient Email" 
    ' 
    'colWhenSent
    '
    Me.colWhenSent.DataPropertyName = "WhenSent"
    Me.colWhenSent.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss zzz"
    Me.colWhenSent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colWhenSent.HeaderText = "When Sent"
    Me.colWhenSent.Name = "colWhenSent"
    Me.colWhenSent.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWhenSent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWhenSent.Width = 60
    Me.colWhenSent.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWhenSent 
    ' 
    Me.mnuColVisibleWhenSent.Checked = True 
    Me.mnuColVisibleWhenSent.CheckOnClick = True 
    Me.mnuColVisibleWhenSent.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWhenSent.Name = "mnuColVisibleWhenSent" 
    Me.mnuColVisibleWhenSent.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWhenSent.Text = "When Sent" 
    ' 
    'colSubject
    '
    Me.colSubject.DataPropertyName = "Subject"
    Me.colSubject.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSubject.HeaderText = "Subject"
    Me.colSubject.Name = "colSubject"
    Me.colSubject.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSubject.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSubject.Width = 60
    Me.colSubject.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSubject 
    ' 
    Me.mnuColVisibleSubject.Checked = True 
    Me.mnuColVisibleSubject.CheckOnClick = True 
    Me.mnuColVisibleSubject.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSubject.Name = "mnuColVisibleSubject" 
    Me.mnuColVisibleSubject.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSubject.Text = "Subject" 
    ' 
    'colBody
    '
    Me.colBody.DataPropertyName = "Body"
    Me.colBody.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colBody.HeaderText = "Body"
    Me.colBody.Name = "colBody"
    Me.colBody.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colBody.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colBody.Width = 60
    Me.colBody.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleBody 
    ' 
    Me.mnuColVisibleBody.Checked = True 
    Me.mnuColVisibleBody.CheckOnClick = True 
    Me.mnuColVisibleBody.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleBody.Name = "mnuColVisibleBody" 
    Me.mnuColVisibleBody.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleBody.Text = "Body" 
    ' 
    'colWhenSeen
    '
    Me.colWhenSeen.DataPropertyName = "WhenSeen"
    Me.colWhenSeen.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss zzz"
    Me.colWhenSeen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colWhenSeen.HeaderText = "When Seen"
    Me.colWhenSeen.Name = "colWhenSeen"
    Me.colWhenSeen.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWhenSeen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWhenSeen.Width = 60
    Me.colWhenSeen.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWhenSeen 
    ' 
    Me.mnuColVisibleWhenSeen.Checked = True 
    Me.mnuColVisibleWhenSeen.CheckOnClick = True 
    Me.mnuColVisibleWhenSeen.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWhenSeen.Name = "mnuColVisibleWhenSeen" 
    Me.mnuColVisibleWhenSeen.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWhenSeen.Text = "When Seen" 
    ' 
    'colWasSeen
    '
    Me.colWasSeen.DataPropertyName = "WasSeen"
    Me.colWasSeen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colWasSeen.HeaderText = "Was Seen"
    Me.colWasSeen.Name = "colWasSeen"
    Me.colWasSeen.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWasSeen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWasSeen.Width = 60
    Me.colWasSeen.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWasSeen 
    ' 
    Me.mnuColVisibleWasSeen.Checked = True 
    Me.mnuColVisibleWasSeen.CheckOnClick = True 
    Me.mnuColVisibleWasSeen.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWasSeen.Name = "mnuColVisibleWasSeen" 
    Me.mnuColVisibleWasSeen.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWasSeen.Text = "Was Seen" 
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
    'ctlMailCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvMail)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_MailCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvMail, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsMessagingMode, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlMail, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvMail As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlMail As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsMessagingMode As System.Windows.Forms.BindingSource
  Friend WithEvents colMessagingMode As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleMessagingMode As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colRecipientEmail As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleRecipientEmail As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWhenSent As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleWhenSent As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSubject As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSubject As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colBody As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleBody As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWhenSeen As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleWhenSeen As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWasSeen As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleWasSeen As System.Windows.Forms.ToolStripMenuItem 

End Class
