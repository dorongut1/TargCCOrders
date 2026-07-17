<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _ 
Partial Class ctlPnlc_Role 
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
    Me.gpbHeader = New System.Windows.Forms.GroupBox() 
    Me.MyIntelliCombo = New IntelliCombo 
    Me.btnNew = New System.Windows.Forms.Button() 
    Me.chkGrid = New System.Windows.Forms.CheckBox() 
    Me.lblSecondaryTitle = New System.Windows.Forms.Label() 
    Me.dpnlRight = New System.Windows.Forms.Panel() 
    Me.dpnlCentre = New System.Windows.Forms.Panel() 
    Me.dpnlLeft = New System.Windows.Forms.Panel() 
    Me.btnFilter = New System.Windows.Forms.Button() 
    Me.btnRefresh = New System.Windows.Forms.Button() 
    Me.lblBack = New System.Windows.Forms.Label() 
    Me.lblTitle = New System.Windows.Forms.Label() 
    Me.pnlRole = New System.Windows.Forms.Panel() 
    Me.pnlButtons = New System.Windows.Forms.Panel() 
    Me.flpMenu = New System.Windows.Forms.FlowLayoutPanel()  
    Me.lnkRoleCol = New System.Windows.Forms.Label() 
    Me.lnkRole = New System.Windows.Forms.Label() 
    Me.lnkPermissionCol = New System.Windows.Forms.Label() 
    Me.lnkUserCol = New System.Windows.Forms.Label() 
    Me.tlpHeader = New System.Windows.Forms.TableLayoutPanel() 
    Me.pnlCover = New System.Windows.Forms.Panel() 
    Me.gpbHeader.SuspendLayout() 
    Me.pnlRole.SuspendLayout() 
    Me.pnlButtons.SuspendLayout() 
    Me.flpMenu.SuspendLayout() 
    Me.tlpHeader.SuspendLayout() 
    Me.SuspendLayout() 
    ' 
    'gpbHeader 
    ' 
    Me.gpbHeader.Controls.Add(Me.lblSecondaryTitle) 
    Me.gpbHeader.Controls.Add(Me.tlpHeader) 
    Me.gpbHeader.Controls.Add(Me.btnFilter) 
    Me.gpbHeader.Controls.Add(Me.dpnlCentre) 
    Me.gpbHeader.Controls.Add(Me.dpnlLeft) 
    Me.gpbHeader.Controls.Add(Me.btnRefresh) 
    Me.gpbHeader.Controls.Add(Me.lblTitle) 
    Me.gpbHeader.Controls.Add(Me.lblBack) 
    Me.gpbHeader.Controls.Add(Me.dpnlRight) 
    Me.gpbHeader.Controls.Add(Me.chkGrid) 
    Me.gpbHeader.Dock = System.Windows.Forms.DockStyle.Top 
    Me.gpbHeader.Location = New System.Drawing.Point(5, 5) 
    Me.gpbHeader.Name = "gpbHeader" 
    Me.gpbHeader.Padding = New System.Windows.Forms.Padding(3, 3, 3, 7) 
    Me.gpbHeader.Size = New System.Drawing.Size(640, 56) 
    Me.gpbHeader.TabIndex = 0 
    Me.gpbHeader.TabStop = False 
    ' 
    'MyIntelliCombo 
    ' 
    Me.MyIntelliCombo.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.MyIntelliCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown 
    Me.MyIntelliCombo.Location = New System.Drawing.Point(1, 1) 
    Me.MyIntelliCombo.Margin = New System.Windows.Forms.Padding(1) 
    Me.MyIntelliCombo.Name = "MyIntelliCombo" 
    Me.MyIntelliCombo.Padding = New System.Windows.Forms.Padding(0, 1, 0, 0) 
    Me.MyIntelliCombo.Size = New System.Drawing.Size(216, 22) 
    Me.MyIntelliCombo.TabIndex = 38 
    ' 
    'btnNew 
    ' 
    Me.btnNew.AutoSize = True 
    Me.btnNew.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
    Me.btnNew.Location = New System.Drawing.Point(236, 0) 
    Me.btnNew.Margin = New System.Windows.Forms.Padding(0) 
    Me.btnNew.Name = "btnNew" 
    Me.btnNew.Size = New System.Drawing.Size(59, 24) 
    Me.btnNew.TabIndex = 39 
    Me.btnNew.Text = "New" 
    Me.btnNew.UseVisualStyleBackColor = True 
    ' 
    'chkGrid 
    ' 
    Me.chkGrid.AutoSize = True 
    Me.chkGrid.Dock = System.Windows.Forms.DockStyle.Right 
    Me.chkGrid.Location = New System.Drawing.Point(592, 16) 
    Me.chkGrid.Name = "chkGrid" 
    Me.chkGrid.Size = New System.Drawing.Size(45, 24) 
    Me.chkGrid.TabIndex = 40 
    Me.chkGrid.Text = "Show List" 
    Me.chkGrid.UseVisualStyleBackColor = True 
    ' 
    'lblSecondaryTitle 
    ' 
    Me.lblSecondaryTitle.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.lblSecondaryTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte)) 
    Me.lblSecondaryTitle.Location = New System.Drawing.Point(123, 16) 
    Me.lblSecondaryTitle.Name = "lblSecondaryTitle" 
    Me.lblSecondaryTitle.Size = New System.Drawing.Size(369, 24) 
    Me.lblSecondaryTitle.TabIndex = 2 
    Me.lblSecondaryTitle.Text = "lblSecondaryTitle" 
    Me.lblSecondaryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter 
    ' 
    'dpnlRight 
    ' 
    Me.dpnlRight.AutoSize = True 
    Me.dpnlRight.MinimumSize = New System.Drawing.Size(10, 24) 
    Me.dpnlRight.Dock = System.Windows.Forms.DockStyle.Right 
    Me.dpnlRight.Location = New System.Drawing.Point(582, 16) 
    Me.dpnlRight.Name = "dpnlRight" 
    Me.dpnlRight.Size = New System.Drawing.Size(10, 24) 
    Me.dpnlRight.TabIndex = 4 
    ' 
    'dpnlCentre 
    ' 
    Me.dpnlCentre.AutoSize = True 
    Me.dpnlCentre.MinimumSize = New System.Drawing.Size(10, 24) 
    Me.dpnlCentre.Dock = System.Windows.Forms.DockStyle.Right 
    Me.dpnlCentre.Location = New System.Drawing.Point(492, 16) 
    Me.dpnlCentre.Name = "dpnlCentre" 
    Me.dpnlCentre.Size = New System.Drawing.Size(10, 24) 
    Me.dpnlCentre.TabIndex = 4 
    ' 
    'dpnlLeft 
    ' 
    Me.dpnlLeft.AutoSize = True 
    Me.dpnlLeft.Dock = System.Windows.Forms.DockStyle.Left 
    Me.dpnlLeft.Location = New System.Drawing.Point(113, 16) 
    Me.dpnlLeft.MinimumSize = New System.Drawing.Size(10, 24) 
    Me.dpnlLeft.Name = "dpnlLeft" 
    Me.dpnlLeft.Size = New System.Drawing.Size(10, 24) 
    Me.dpnlLeft.TabIndex = 4 
    ' 
    'btnRefresh 
    ' 
    Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right 
    Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
    Me.btnRefresh.Location = New System.Drawing.Point(502, 16) 
    Me.btnRefresh.Name = "btnRefresh" 
    Me.btnRefresh.Size = New System.Drawing.Size(80, 24) 
    Me.btnRefresh.TabIndex = 37 
    Me.btnRefresh.Text = "Refresh" 
    Me.btnRefresh.UseVisualStyleBackColor = True 
    ' 
    'lblBack 
    ' 
    Me.lblBack.Dock = System.Windows.Forms.DockStyle.Left 
    Me.lblBack.Font = New System.Drawing.Font("Wingdings", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte)) 
    Me.lblBack.ForeColor = System.Drawing.Color.Black 
    Me.lblBack.Location = New System.Drawing.Point(3, 16) 
    Me.lblBack.Margin = New System.Windows.Forms.Padding(0) 
    Me.lblBack.Name = "lblBack" 
    Me.lblBack.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0) 
    Me.lblBack.Size = New System.Drawing.Size(26, 24) 
    Me.lblBack.TabIndex = 40 
    Me.lblBack.TabStop = True 
    Me.lblBack.Text = "E" 
    ' 
    'lblTitle 
    ' 
    Me.lblTitle.AutoSize = True 
    Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Left 
    Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(1, Byte)) 
    Me.lblTitle.Location = New System.Drawing.Point(3, 16) 
    Me.lblTitle.Name = "lblTitle" 
    Me.lblTitle.Size = New System.Drawing.Size(80, 25) 
    Me.lblTitle.TabIndex = 0 
    Me.lblTitle.Text = "Role" 
    ' 
    'pnlRole 
    ' 
    Me.pnlRole.Controls.Add(Me.pnlCover) 
    Me.pnlRole.Controls.Add(Me.pnlButtons) 
    Me.pnlRole.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.pnlRole.Location = New System.Drawing.Point(5, 48) 
    Me.pnlRole.Name = "pnlRole" 
    Me.pnlRole.Size = New System.Drawing.Size(640, 545) 
    Me.pnlRole.TabIndex = 1 
    ' 
    'pnlButtons 
    ' 
    Me.pnlButtons.Controls.Add(Me.flpMenu) 
    Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top 
    Me.pnlButtons.Location = New System.Drawing.Point(0, 0) 
    Me.pnlButtons.Name = "pnlButtons" 
    Me.pnlButtons.BackColor = System.Drawing.Color.White 
    Me.pnlButtons.Size = New System.Drawing.Size(623, 25) 
    Me.pnlButtons.TabIndex = 3 
    ' 
    'flpMenu 
    ' 
    Me.flpMenu.AutoSize = True 
    Me.flpMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink 
    Me.flpMenu.Controls.Add(Me.lnkRoleCol) 
    Me.flpMenu.Controls.Add(Me.lnkRole) 
    Me.flpMenu.Controls.Add(Me.lnkPermissionCol) 
    Me.flpMenu.Controls.Add(Me.lnkUserCol) 
    Me.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.flpMenu.Location = New System.Drawing.Point(3, 3) 
    Me.flpMenu.Name = "flpMenu" 
    Me.flpMenu.Size = New System.Drawing.Size(224, 18) 
    Me.flpMenu.TabIndex = 0 
    ' 
    'lnkRoleCol 
    ' 
    Me.lnkRoleCol.AutoSize = True 
    Me.lnkRoleCol.ForeColor = System.Drawing.Color.Black 
    Me.lnkRoleCol.Location = New System.Drawing.Point(5, 5) 
    Me.lnkRoleCol.Margin = New System.Windows.Forms.Padding(5, 5, 3, 0) 
    Me.lnkRoleCol.Name = "lnkRoleCol" 
    Me.lnkRoleCol.Size = New System.Drawing.Size(45, 13) 
    Me.lnkRoleCol.TabIndex = 0 
    Me.lnkRoleCol.TabStop = True 
    Me.lnkRoleCol.Tag = "ctlc_RoleCol" 
    Me.lnkRoleCol.Text = "List" 
    ' 
    'btnFilter 
    ' 
    Me.btnFilter.AutoSize = True 
    Me.btnFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink 
    Me.btnFilter.Dock = System.Windows.Forms.DockStyle.Left 
    Me.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
    Me.btnFilter.Location = New System.Drawing.Point(172, 16) 
    Me.btnFilter.Name = "btnFilter" 
    Me.btnFilter.Size = New System.Drawing.Size(39, 24) 
    Me.btnFilter.TabIndex = 0 
    Me.btnFilter.Text = "Filter" 
    Me.btnFilter.UseVisualStyleBackColor = True 
    ' 
    'lnkRole 
    ' 
    Me.lnkRole.AutoSize = True 
    Me.lnkRole.ForeColor = System.Drawing.Color.Black 
    Me.lnkRole.Location = New System.Drawing.Point(58, 5) 
    Me.lnkRole.Margin = New System.Windows.Forms.Padding(5, 5, 3, 0) 
    Me.lnkRole.Name = "lnkRole" 
    Me.lnkRole.Size = New System.Drawing.Size(40, 13) 
    Me.lnkRole.TabIndex = 1 
    Me.lnkRole.TabStop = True 
    Me.lnkRole.Tag = "ctlc_Role" 
    Me.lnkRole.Text = "Details" 
    ' 
    'lnkPermissionCol 
    ' 
    Me.lnkPermissionCol.AutoSize = True 
    Me.lnkPermissionCol.ForeColor = System.Drawing.Color.Black 
    Me.lnkPermissionCol.Location = New System.Drawing.Point(193, 5) 
    Me.lnkPermissionCol.Margin = New System.Windows.Forms.Padding(5, 5, 3, 0) 
    Me.lnkPermissionCol.Name = "lnkPermissionCol" 
    Me.lnkPermissionCol.Size = New System.Drawing.Size(28, 13) 
    Me.lnkPermissionCol.TabIndex = 1 
    Me.lnkPermissionCol.TabStop = True 
    Me.lnkPermissionCol.Tag = "ctlc_PermissionCol" 
    Me.lnkPermissionCol.Text = "Permissions"
    ' 
    'lnkUserCol 
    ' 
    Me.lnkUserCol.AutoSize = True 
    Me.lnkUserCol.ForeColor = System.Drawing.Color.Black 
    Me.lnkUserCol.Location = New System.Drawing.Point(193, 5) 
    Me.lnkUserCol.Margin = New System.Windows.Forms.Padding(5, 5, 3, 0) 
    Me.lnkUserCol.Name = "lnkUserCol" 
    Me.lnkUserCol.Size = New System.Drawing.Size(28, 13) 
    Me.lnkUserCol.TabIndex = 1 
    Me.lnkUserCol.TabStop = True 
    Me.lnkUserCol.Tag = "ctlc_UserCol" 
    Me.lnkUserCol.Text = "Users"
    ' 
    'tlpHeader 
    ' 
    Me.tlpHeader.ColumnCount = 3 
    Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle()) 
    Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!)) 
    Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle()) 
    Me.tlpHeader.Controls.Add(Me.MyIntelliCombo, 1, 0) 
    Me.tlpHeader.Controls.Add(Me.btnNew, 2, 0) 
    Me.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.tlpHeader.Location = New System.Drawing.Point(131, 16) 
    Me.tlpHeader.Margin = New System.Windows.Forms.Padding(0) 
    Me.tlpHeader.Name = "tlpHeader" 
    Me.tlpHeader.RowCount = 1 
    Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!)) 
    Me.tlpHeader.Size = New System.Drawing.Size(334, 31) 
    Me.tlpHeader.TabIndex = 4 
    ' 
        'pnlCover 
        ' 
        Me.pnlCover.Dock = System.Windows.Forms.DockStyle.Fill 
        Me.pnlCover.Location = New System.Drawing.Point(0, 25) 
        Me.pnlCover.Name = "pnlCover" 
        Me.pnlCover.Size = New System.Drawing.Size(640, 507) 
        Me.pnlCover.TabIndex = 4 
        ' 
    'ctlPnlRole 
    ' 
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.pnlRole) 
    Me.Controls.Add(Me.gpbHeader) 
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlPnlc_Role" 
    Me.Size = New System.Drawing.Size(650, 598) 
    Me.Padding = New System.Windows.Forms.Padding(5) 
    Me.gpbHeader.ResumeLayout(False) 
    Me.gpbHeader.PerformLayout() 
    Me.pnlRole.ResumeLayout(False) 
    Me.pnlButtons.ResumeLayout(False) 
    Me.pnlButtons.PerformLayout() 
    Me.flpMenu.ResumeLayout(False) 
    Me.flpMenu.PerformLayout() 
    Me.tlpHeader.ResumeLayout(False) 
    Me.ResumeLayout(False) 
 
  End Sub 
  Friend WithEvents gpbHeader As System.Windows.Forms.GroupBox 
  Friend WithEvents lblBack As System.Windows.Forms.Label 
  Friend WithEvents lblTitle As System.Windows.Forms.Label 
  Friend WithEvents btnRefresh As System.Windows.Forms.Button 
  Friend WithEvents pnlRole As System.Windows.Forms.Panel 
  Friend WithEvents flpMenu As System.Windows.Forms.FlowLayoutPanel 
  Friend WithEvents lnkRoleCol As System.Windows.Forms.Label 
  Friend WithEvents lblSecondaryTitle As System.Windows.Forms.Label 
  Friend WithEvents lnkRole As System.Windows.Forms.Label 
  Friend WithEvents lnkPermissionCol As System.Windows.Forms.Label 
  Friend WithEvents lnkUserCol As System.Windows.Forms.Label 
  Friend WithEvents pnlButtons As System.Windows.Forms.Panel 
  Friend WithEvents MyIntelliCombo As IntelliCombo 
  Friend WithEvents btnNew As System.Windows.Forms.Button 
  Friend WithEvents chkGrid As System.Windows.Forms.CheckBox 
  Friend WithEvents dpnlRight As System.Windows.Forms.Panel 
  Friend WithEvents dpnlCentre As System.Windows.Forms.Panel 
  Friend WithEvents dpnlLeft As System.Windows.Forms.Panel 
  Friend WithEvents btnFilter As System.Windows.Forms.Button 
  Friend WithEvents tlpHeader As System.Windows.Forms.TableLayoutPanel 
  Friend WithEvents pnlCover As Panel 
 
End Class 
