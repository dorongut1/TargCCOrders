'ColourObjectBackground
'ColourObjectReadOnlyTextBackground

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlPnlcsDBMaintenance
  Inherits System.Windows.Forms.UserControl


  'UserControl overrides dispose to clean up the component list. 
  <System.Diagnostics.DebuggerNonUserCode()>
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
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Me.gpbHeader = New System.Windows.Forms.GroupBox()
    Me.lblTitle = New System.Windows.Forms.Label()
    Me.tbc = New System.Windows.Forms.TabControl()
    Me.tbpDatabase = New System.Windows.Forms.TabPage()
    Me.gpbDatabaseStatus = New System.Windows.Forms.GroupBox()
    Me.txtDatabaseFileSizes = New System.Windows.Forms.TextBox()
    Me.grdIndexFragmentation = New ctlc_IndexFragmentationCol()
    Me.grdTableSizes = New ctlc_TableSizeCol()
    Me.pnlDatabaseStatus = New System.Windows.Forms.Panel()
    Me.btnDatabaseFileSizes = New System.Windows.Forms.Button()
    Me.btnTableSizes = New System.Windows.Forms.Button()
    Me.btnIndexFragmentation = New System.Windows.Forms.Button()
    Me.gbpDBMaintenance = New System.Windows.Forms.GroupBox()
    Me.tblDBMaintenance = New System.Windows.Forms.TableLayoutPanel()
    Me.pnlDBMaintenanceL = New System.Windows.Forms.Panel()
    Me.btnResetPermissionsForDefaultRoles = New System.Windows.Forms.Button()
    Me.btnEjectNonMasterUsersOnly = New System.Windows.Forms.Button()
    Me.btnBackupDatabase = New System.Windows.Forms.Button()
    Me.btnEjectAllUsers = New System.Windows.Forms.Button()
    Me.btnRequestIndexReorganization = New System.Windows.Forms.Button()
    Me.pnlDBMaintenanceR = New System.Windows.Forms.Panel()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate = New System.Windows.Forms.Button()
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate = New System.Windows.Forms.Button()
    Me.gpbSysAdmin = New System.Windows.Forms.GroupBox()
    Me.btnRunScriptOnServer = New System.Windows.Forms.Button()
    Me.btnEnableCLR = New System.Windows.Forms.Button()
    Me.gpbCreateBinaryFilesOnServer = New System.Windows.Forms.GroupBox()
    Me.rbtnOneFilePerTable = New System.Windows.Forms.RadioButton()
    Me.rbtnOneFileForDatabase = New System.Windows.Forms.RadioButton()
    Me.btnCreateBinaryFilesOnServer = New System.Windows.Forms.Button()
    Me.gpbHeader.SuspendLayout()
    Me.tbc.SuspendLayout()
    Me.tbpDatabase.SuspendLayout()
    Me.gpbDatabaseStatus.SuspendLayout()
    Me.pnlDatabaseStatus.SuspendLayout()
    Me.gbpDBMaintenance.SuspendLayout()
    Me.tblDBMaintenance.SuspendLayout()
    Me.pnlDBMaintenanceL.SuspendLayout()
    Me.pnlDBMaintenanceR.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.gpbSysAdmin.SuspendLayout()
    Me.gpbCreateBinaryFilesOnServer.SuspendLayout()
    Me.SuspendLayout()
    '
    'gpbHeader
    '
    Me.gpbHeader.Controls.Add(Me.lblTitle)
    Me.gpbHeader.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbHeader.Location = New System.Drawing.Point(5, 5)
    Me.gpbHeader.Name = "gpbHeader"
    Me.gpbHeader.Padding = New System.Windows.Forms.Padding(3, 0, 3, 6)
    Me.gpbHeader.Size = New System.Drawing.Size(811, 56)
    Me.gpbHeader.TabIndex = 0
    Me.gpbHeader.TabStop = False
    '
    'lblTitle
    '
    Me.lblTitle.AutoSize = True
    Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Left
    Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Italic)
    Me.lblTitle.Location = New System.Drawing.Point(3, 18)
    Me.lblTitle.Name = "lblTitle"
    Me.lblTitle.Size = New System.Drawing.Size(238, 31)
    Me.lblTitle.TabIndex = 0
    Me.lblTitle.Text = "Database Maintenance"
    '
    'tbc
    '
    Me.tbc.Controls.Add(Me.tbpDatabase)
    Me.tbc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tbc.Location = New System.Drawing.Point(5, 61)
    Me.tbc.Name = "tbc"
    Me.tbc.SelectedIndex = 0
    Me.tbc.Size = New System.Drawing.Size(811, 605)
    Me.tbc.TabIndex = 1
    '
    'tbpDatabase
    '
    Me.tbpDatabase.BackColor = System.Drawing.Color.Wheat
    Me.tbpDatabase.Controls.Add(Me.gpbDatabaseStatus)
    Me.tbpDatabase.Controls.Add(Me.gbpDBMaintenance)
    Me.tbpDatabase.Controls.Add(Me.gpbCreateBinaryFilesOnServer)
    Me.tbpDatabase.Location = New System.Drawing.Point(4, 26)
    Me.tbpDatabase.Name = "tbpDatabase"
    Me.tbpDatabase.Padding = New System.Windows.Forms.Padding(5)
    Me.tbpDatabase.Size = New System.Drawing.Size(803, 575)
    Me.tbpDatabase.TabIndex = 1
    Me.tbpDatabase.Text = "Database"
    '
    'gpbDatabaseStatus
    '
    Me.gpbDatabaseStatus.BackColor = System.Drawing.Color.Wheat
    Me.gpbDatabaseStatus.Controls.Add(Me.txtDatabaseFileSizes)
    Me.gpbDatabaseStatus.Controls.Add(Me.grdIndexFragmentation)
    Me.gpbDatabaseStatus.Controls.Add(Me.grdTableSizes)
    Me.gpbDatabaseStatus.Controls.Add(Me.pnlDatabaseStatus)
    Me.gpbDatabaseStatus.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbDatabaseStatus.Location = New System.Drawing.Point(5, 65)
    Me.gpbDatabaseStatus.Name = "gpbDatabaseStatus"
    Me.gpbDatabaseStatus.Size = New System.Drawing.Size(793, 289)
    Me.gpbDatabaseStatus.TabIndex = 3
    Me.gpbDatabaseStatus.TabStop = False
    Me.gpbDatabaseStatus.Text = "Database Status"
    '
    'txtDatabaseFileSizes
    '
    Me.txtDatabaseFileSizes.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtDatabaseFileSizes.Location = New System.Drawing.Point(362, 20)
    Me.txtDatabaseFileSizes.Multiline = True
    Me.txtDatabaseFileSizes.Name = "txtDatabaseFileSizes"
    Me.txtDatabaseFileSizes.ReadOnly = True
    Me.txtDatabaseFileSizes.Size = New System.Drawing.Size(200, 117)
    Me.txtDatabaseFileSizes.TabIndex = 5
    '
    'grdIndexFragmentation
    '
    Me.grdIndexFragmentation.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.grdIndexFragmentation.Location = New System.Drawing.Point(282, 19)
    Me.grdIndexFragmentation.Name = "grdIndexFragmentation"
    Me.grdIndexFragmentation.Size = New System.Drawing.Size(155, 95)
    Me.grdIndexFragmentation.TabIndex = 4
    '
    'grdTableSizes
    '
    Me.grdTableSizes.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.grdTableSizes.Location = New System.Drawing.Point(3, 16)
    Me.grdTableSizes.Name = "grdTableSizes"
    Me.grdTableSizes.Size = New System.Drawing.Size(329, 115)
    Me.grdTableSizes.TabIndex = 2
    '
    'pnlDatabaseStatus
    '
    Me.pnlDatabaseStatus.Controls.Add(Me.btnDatabaseFileSizes)
    Me.pnlDatabaseStatus.Controls.Add(Me.btnTableSizes)
    Me.pnlDatabaseStatus.Controls.Add(Me.btnIndexFragmentation)
    Me.pnlDatabaseStatus.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnlDatabaseStatus.Location = New System.Drawing.Point(3, 255)
    Me.pnlDatabaseStatus.Name = "pnlDatabaseStatus"
    Me.pnlDatabaseStatus.Size = New System.Drawing.Size(787, 31)
    Me.pnlDatabaseStatus.TabIndex = 3
    '
    'btnDatabaseFileSizes
    '
    Me.btnDatabaseFileSizes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnDatabaseFileSizes.Location = New System.Drawing.Point(265, 5)
    Me.btnDatabaseFileSizes.Name = "btnDatabaseFileSizes"
    Me.btnDatabaseFileSizes.Size = New System.Drawing.Size(125, 27)
    Me.btnDatabaseFileSizes.TabIndex = 2
    Me.btnDatabaseFileSizes.Text = "Database File Sizes"
    Me.btnDatabaseFileSizes.UseVisualStyleBackColor = True
    '
    'btnTableSizes
    '
    Me.btnTableSizes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnTableSizes.Location = New System.Drawing.Point(3, 5)
    Me.btnTableSizes.Name = "btnTableSizes"
    Me.btnTableSizes.Size = New System.Drawing.Size(125, 27)
    Me.btnTableSizes.TabIndex = 0
    Me.btnTableSizes.Text = "Table Sizes"
    Me.btnTableSizes.UseVisualStyleBackColor = True
    '
    'btnIndexFragmentation
    '
    Me.btnIndexFragmentation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnIndexFragmentation.Location = New System.Drawing.Point(134, 5)
    Me.btnIndexFragmentation.Name = "btnIndexFragmentation"
    Me.btnIndexFragmentation.Size = New System.Drawing.Size(125, 27)
    Me.btnIndexFragmentation.TabIndex = 1
    Me.btnIndexFragmentation.Text = "Index Fragmentation"
    Me.btnIndexFragmentation.UseVisualStyleBackColor = True
    '
    'gbpDBMaintenance
    '
    Me.gbpDBMaintenance.Controls.Add(Me.tblDBMaintenance)
    Me.gbpDBMaintenance.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.gbpDBMaintenance.Location = New System.Drawing.Point(5, 354)
    Me.gbpDBMaintenance.Name = "gbpDBMaintenance"
    Me.gbpDBMaintenance.Size = New System.Drawing.Size(793, 216)
    Me.gbpDBMaintenance.TabIndex = 2
    Me.gbpDBMaintenance.TabStop = False
    Me.gbpDBMaintenance.Text = "Database Maintenance"
    Me.gbpDBMaintenance.Visible = False
    '
    'tblDBMaintenance
    '
    Me.tblDBMaintenance.ColumnCount = 2
    Me.tblDBMaintenance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tblDBMaintenance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tblDBMaintenance.Controls.Add(Me.pnlDBMaintenanceL, 0, 0)
    Me.tblDBMaintenance.Controls.Add(Me.pnlDBMaintenanceR, 1, 0)
    Me.tblDBMaintenance.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tblDBMaintenance.Location = New System.Drawing.Point(3, 21)
    Me.tblDBMaintenance.Name = "tblDBMaintenance"
    Me.tblDBMaintenance.RowCount = 1
    Me.tblDBMaintenance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tblDBMaintenance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 197.0!))
    Me.tblDBMaintenance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 197.0!))
    Me.tblDBMaintenance.Size = New System.Drawing.Size(787, 192)
    Me.tblDBMaintenance.TabIndex = 5
    '
    'pnlDBMaintenanceL
    '
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnResetPermissionsForDefaultRoles)
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnEjectNonMasterUsersOnly)
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnBackupDatabase)
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnEjectAllUsers)
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnRequestIndexReorganization)
    Me.pnlDBMaintenanceL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlDBMaintenanceL.Location = New System.Drawing.Point(3, 3)
    Me.pnlDBMaintenanceL.Name = "pnlDBMaintenanceL"
    Me.pnlDBMaintenanceL.Size = New System.Drawing.Size(387, 186)
    Me.pnlDBMaintenanceL.TabIndex = 0
    '
    'btnResetPermissionsForDefaultRoles
    '
    Me.btnResetPermissionsForDefaultRoles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnResetPermissionsForDefaultRoles.Location = New System.Drawing.Point(15, 150)
    Me.btnResetPermissionsForDefaultRoles.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnResetPermissionsForDefaultRoles.Name = "btnResetPermissionsForDefaultRoles"
    Me.btnResetPermissionsForDefaultRoles.Size = New System.Drawing.Size(357, 25)
    Me.btnResetPermissionsForDefaultRoles.TabIndex = 9
    Me.btnResetPermissionsForDefaultRoles.Text = "Reset Permissions for Default Roles"
    Me.btnResetPermissionsForDefaultRoles.UseVisualStyleBackColor = True
    '
    'btnEjectNonMasterUsersOnly
    '
    Me.btnEjectNonMasterUsersOnly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnEjectNonMasterUsersOnly.Location = New System.Drawing.Point(15, 43)
    Me.btnEjectNonMasterUsersOnly.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnEjectNonMasterUsersOnly.Name = "btnEjectNonMasterUsersOnly"
    Me.btnEjectNonMasterUsersOnly.Size = New System.Drawing.Size(357, 25)
    Me.btnEjectNonMasterUsersOnly.TabIndex = 2
    Me.btnEjectNonMasterUsersOnly.Text = "Eject Non Master Users Only"
    Me.btnEjectNonMasterUsersOnly.UseVisualStyleBackColor = True
    '
    'btnBackupDatabase
    '
    Me.btnBackupDatabase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnBackupDatabase.Location = New System.Drawing.Point(15, 115)
    Me.btnBackupDatabase.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnBackupDatabase.Name = "btnBackupDatabase"
    Me.btnBackupDatabase.Size = New System.Drawing.Size(357, 25)
    Me.btnBackupDatabase.TabIndex = 4
    Me.btnBackupDatabase.Text = "Backup Database"
    Me.btnBackupDatabase.UseVisualStyleBackColor = True
    '
    'btnEjectAllUsers
    '
    Me.btnEjectAllUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnEjectAllUsers.Location = New System.Drawing.Point(15, 10)
    Me.btnEjectAllUsers.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnEjectAllUsers.Name = "btnEjectAllUsers"
    Me.btnEjectAllUsers.Size = New System.Drawing.Size(357, 25)
    Me.btnEjectAllUsers.TabIndex = 1
    Me.btnEjectAllUsers.Text = "Eject All Users"
    Me.btnEjectAllUsers.UseVisualStyleBackColor = True
    '
    'btnRequestIndexReorganization
    '
    Me.btnRequestIndexReorganization.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnRequestIndexReorganization.Location = New System.Drawing.Point(15, 80)
    Me.btnRequestIndexReorganization.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnRequestIndexReorganization.Name = "btnRequestIndexReorganization"
    Me.btnRequestIndexReorganization.Size = New System.Drawing.Size(357, 25)
    Me.btnRequestIndexReorganization.TabIndex = 3
    Me.btnRequestIndexReorganization.Text = "Request Index Reorganization"
    Me.btnRequestIndexReorganization.UseVisualStyleBackColor = True
    '
    'pnlDBMaintenanceR
    '
    Me.pnlDBMaintenanceR.Controls.Add(Me.GroupBox2)
    Me.pnlDBMaintenanceR.Controls.Add(Me.gpbSysAdmin)
    Me.pnlDBMaintenanceR.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlDBMaintenanceR.Location = New System.Drawing.Point(396, 3)
    Me.pnlDBMaintenanceR.Name = "pnlDBMaintenanceR"
    Me.pnlDBMaintenanceR.Size = New System.Drawing.Size(388, 186)
    Me.pnlDBMaintenanceR.TabIndex = 1
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.btnTranslationAddAllPossibilitiesToObjectToTranslate)
    Me.GroupBox2.Controls.Add(Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate)
    Me.GroupBox2.Location = New System.Drawing.Point(15, 10)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(358, 83)
    Me.GroupBox2.TabIndex = 5
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Translation"
    '
    'btnTranslationAddAllPossibilitiesToObjectToTranslate
    '
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Location = New System.Drawing.Point(0, 18)
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Name = "btnTranslationAddAllPossibilitiesToObjectToTranslate"
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Size = New System.Drawing.Size(358, 25)
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.TabIndex = 3
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Text = "Add All Possibilities To 'ObjectToTranslate'"
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.UseVisualStyleBackColor = True
    '
    'btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate
    '
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Location = New System.Drawing.Point(0, 51)
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Name = "btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate"
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Size = New System.Drawing.Size(358, 25)
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.TabIndex = 3
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Text = "Remove Unused Possibilities From 'ObjectToTranslate'"
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.UseVisualStyleBackColor = True
    '
    'gpbSysAdmin
    '
    Me.gpbSysAdmin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gpbSysAdmin.Controls.Add(Me.btnRunScriptOnServer)
    Me.gpbSysAdmin.Controls.Add(Me.btnEnableCLR)
    Me.gpbSysAdmin.Location = New System.Drawing.Point(15, 99)
    Me.gpbSysAdmin.Name = "gpbSysAdmin"
    Me.gpbSysAdmin.Size = New System.Drawing.Size(358, 83)
    Me.gpbSysAdmin.TabIndex = 4
    Me.gpbSysAdmin.TabStop = False
    Me.gpbSysAdmin.Text = "SQL SysAdmin or dbo Only"
    '
    'btnRunScriptOnServer
    '
    Me.btnRunScriptOnServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnRunScriptOnServer.Location = New System.Drawing.Point(6, 51)
    Me.btnRunScriptOnServer.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnRunScriptOnServer.Name = "btnRunScriptOnServer"
    Me.btnRunScriptOnServer.Size = New System.Drawing.Size(346, 25)
    Me.btnRunScriptOnServer.TabIndex = 4
    Me.btnRunScriptOnServer.Text = "Run Script On Server"
    Me.btnRunScriptOnServer.UseVisualStyleBackColor = True
    '
    'btnEnableCLR
    '
    Me.btnEnableCLR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnEnableCLR.Location = New System.Drawing.Point(6, 22)
    Me.btnEnableCLR.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnEnableCLR.Name = "btnEnableCLR"
    Me.btnEnableCLR.Size = New System.Drawing.Size(346, 25)
    Me.btnEnableCLR.TabIndex = 4
    Me.btnEnableCLR.Text = "Enable CLR"
    Me.btnEnableCLR.UseVisualStyleBackColor = True
    '
    'gpbCreateBinaryFilesOnServer
    '
    Me.gpbCreateBinaryFilesOnServer.Controls.Add(Me.rbtnOneFilePerTable)
    Me.gpbCreateBinaryFilesOnServer.Controls.Add(Me.rbtnOneFileForDatabase)
    Me.gpbCreateBinaryFilesOnServer.Controls.Add(Me.btnCreateBinaryFilesOnServer)
    Me.gpbCreateBinaryFilesOnServer.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbCreateBinaryFilesOnServer.Location = New System.Drawing.Point(5, 5)
    Me.gpbCreateBinaryFilesOnServer.Name = "gpbCreateBinaryFilesOnServer"
    Me.gpbCreateBinaryFilesOnServer.Size = New System.Drawing.Size(793, 60)
    Me.gpbCreateBinaryFilesOnServer.TabIndex = 1
    Me.gpbCreateBinaryFilesOnServer.TabStop = False
    Me.gpbCreateBinaryFilesOnServer.Text = "Create Binary Files on Server"
    Me.gpbCreateBinaryFilesOnServer.Visible = False
    '
    'rbtnOneFilePerTable
    '
    Me.rbtnOneFilePerTable.AutoSize = True
    Me.rbtnOneFilePerTable.Location = New System.Drawing.Point(185, 24)
    Me.rbtnOneFilePerTable.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.rbtnOneFilePerTable.Name = "rbtnOneFilePerTable"
    Me.rbtnOneFilePerTable.Size = New System.Drawing.Size(134, 23)
    Me.rbtnOneFilePerTable.TabIndex = 2
    Me.rbtnOneFilePerTable.TabStop = True
    Me.rbtnOneFilePerTable.Text = "One File Per Table"
    Me.rbtnOneFilePerTable.UseVisualStyleBackColor = True
    '
    'rbtnOneFileForDatabase
    '
    Me.rbtnOneFileForDatabase.AutoSize = True
    Me.rbtnOneFileForDatabase.Location = New System.Drawing.Point(18, 24)
    Me.rbtnOneFileForDatabase.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.rbtnOneFileForDatabase.Name = "rbtnOneFileForDatabase"
    Me.rbtnOneFileForDatabase.Size = New System.Drawing.Size(154, 23)
    Me.rbtnOneFileForDatabase.TabIndex = 1
    Me.rbtnOneFileForDatabase.TabStop = True
    Me.rbtnOneFileForDatabase.Text = "One file for database"
    Me.rbtnOneFileForDatabase.UseVisualStyleBackColor = True
    '
    'btnCreateBinaryFilesOnServer
    '
    Me.btnCreateBinaryFilesOnServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCreateBinaryFilesOnServer.Location = New System.Drawing.Point(337, 22)
    Me.btnCreateBinaryFilesOnServer.Name = "btnCreateBinaryFilesOnServer"
    Me.btnCreateBinaryFilesOnServer.Size = New System.Drawing.Size(208, 27)
    Me.btnCreateBinaryFilesOnServer.TabIndex = 0
    Me.btnCreateBinaryFilesOnServer.Text = "Create Binary Files on Server"
    Me.btnCreateBinaryFilesOnServer.UseVisualStyleBackColor = True
    '
    'ctlPnlcsDBMaintenance
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.tbc)
    Me.Controls.Add(Me.gpbHeader)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.Name = "ctlPnlcsDBMaintenance"
    Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 3)
    Me.Size = New System.Drawing.Size(821, 669)
    Me.gpbHeader.ResumeLayout(False)
    Me.gpbHeader.PerformLayout()
    Me.tbc.ResumeLayout(False)
    Me.tbpDatabase.ResumeLayout(False)
    Me.gpbDatabaseStatus.ResumeLayout(False)
    Me.gpbDatabaseStatus.PerformLayout()
    Me.pnlDatabaseStatus.ResumeLayout(False)
    Me.gbpDBMaintenance.ResumeLayout(False)
    Me.tblDBMaintenance.ResumeLayout(False)
    Me.pnlDBMaintenanceL.ResumeLayout(False)
    Me.pnlDBMaintenanceR.ResumeLayout(False)
    Me.GroupBox2.ResumeLayout(False)
    Me.gpbSysAdmin.ResumeLayout(False)
    Me.gpbCreateBinaryFilesOnServer.ResumeLayout(False)
    Me.gpbCreateBinaryFilesOnServer.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents gpbHeader As System.Windows.Forms.GroupBox
  Friend WithEvents lblTitle As System.Windows.Forms.Label
  Friend WithEvents tbc As System.Windows.Forms.TabControl
  Friend WithEvents tbpDatabase As System.Windows.Forms.TabPage
  Friend WithEvents gbpDBMaintenance As System.Windows.Forms.GroupBox
  Friend WithEvents gpbCreateBinaryFilesOnServer As System.Windows.Forms.GroupBox
  Friend WithEvents rbtnOneFilePerTable As System.Windows.Forms.RadioButton
  Friend WithEvents rbtnOneFileForDatabase As System.Windows.Forms.RadioButton
  Friend WithEvents btnCreateBinaryFilesOnServer As System.Windows.Forms.Button
  Friend WithEvents btnEjectAllUsers As System.Windows.Forms.Button
  Friend WithEvents btnEjectNonMasterUsersOnly As System.Windows.Forms.Button
  Friend WithEvents gpbDatabaseStatus As System.Windows.Forms.GroupBox
  Friend WithEvents btnIndexFragmentation As System.Windows.Forms.Button
  Friend WithEvents btnTableSizes As System.Windows.Forms.Button
  Friend WithEvents btnRequestIndexReorganization As System.Windows.Forms.Button
  Friend WithEvents btnResetPermissionsForDefaultRoles As System.Windows.Forms.Button
  Friend WithEvents btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate As System.Windows.Forms.Button
  Friend WithEvents btnTranslationAddAllPossibilitiesToObjectToTranslate As System.Windows.Forms.Button
  Friend WithEvents pnlDatabaseStatus As System.Windows.Forms.Panel
  Friend WithEvents grdTableSizes As ctlc_TableSizeCol
  Friend WithEvents grdIndexFragmentation As ctlc_IndexFragmentationCol
  Friend WithEvents btnBackupDatabase As System.Windows.Forms.Button
  Friend WithEvents tblDBMaintenance As TableLayoutPanel
  Friend WithEvents pnlDBMaintenanceL As Panel
  Friend WithEvents pnlDBMaintenanceR As Panel
  Friend WithEvents btnEnableCLR As Button
  Friend WithEvents gpbSysAdmin As GroupBox
  Friend WithEvents btnRunScriptOnServer As Button
  Friend WithEvents txtDatabaseFileSizes As TextBox
  Friend WithEvents btnDatabaseFileSizes As Button
  Friend WithEvents GroupBox2 As GroupBox
End Class
