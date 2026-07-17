<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_Table
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
    Me.txtID = New System.Windows.Forms.TextBox()
    Me.lblID = New System.Windows.Forms.Label()
    Me.txtName = New System.Windows.Forms.TextBox()
    Me.lblName = New System.Windows.Forms.Label()
    Me.txtDefaultTextFields = New System.Windows.Forms.TextBox()
    Me.lblDefaultTextFields = New System.Windows.Forms.Label()
    Me.chkUsedForIdentity = New System.Windows.Forms.CheckBox()
    Me.lblUsedForIdentity = New System.Windows.Forms.Label()
    Me.chkIsSingleRow = New System.Windows.Forms.CheckBox()
    Me.lblIsSingleRow = New System.Windows.Forms.Label()
    Me.txtCanAdd = New System.Windows.Forms.TextBox()
    Me.lblCanAdd = New System.Windows.Forms.Label()
    Me.txtCanEdit = New System.Windows.Forms.TextBox()
    Me.lblCanEdit = New System.Windows.Forms.Label()
    Me.txtCanDelete = New System.Windows.Forms.TextBox()
    Me.lblCanDelete = New System.Windows.Forms.Label()
    Me.chkAuditAdd = New System.Windows.Forms.CheckBox()
    Me.lblAuditAdd = New System.Windows.Forms.Label()
    Me.chkAuditEdit = New System.Windows.Forms.CheckBox()
    Me.lblAuditEdit = New System.Windows.Forms.Label()
    Me.chkAuditDelete = New System.Windows.Forms.CheckBox()
    Me.lblAuditDelete = New System.Windows.Forms.Label()
    Me.chkTrackRowChangers = New System.Windows.Forms.CheckBox()
    Me.lblTrackRowChangers = New System.Windows.Forms.Label()
    Me.chkCreateUIMenu = New System.Windows.Forms.CheckBox()
    Me.lblCreateUIMenu = New System.Windows.Forms.Label()
    Me.chkCreateUICollection = New System.Windows.Forms.CheckBox()
    Me.lblCreateUICollection = New System.Windows.Forms.Label()
    Me.chkCreateUIEntity = New System.Windows.Forms.CheckBox()
    Me.lblCreateUIEntity = New System.Windows.Forms.Label()
    Me.txtSortOrder = New System.Windows.Forms.TextBox()
    Me.lblSortOrder = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(192, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(378, 25)
    Me.txtID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtID.TabIndex = 0
    Me.txtID.Text = "txtID"
    '
    'lblID
    '
    Me.lblID.AutoSize = True
    Me.lblID.Location = New System.Drawing.Point(42, 20)
    Me.lblID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblID.Name = "lblID"
    Me.lblID.Size = New System.Drawing.Size(18, 13)
    Me.lblID.TabIndex = 1
    Me.lblID.Text = "ID"
    '
    'DtxtName
    '
    Me.txtName.Location = New System.Drawing.Point(192, 57)
    Me.txtName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtName.Name = "txtName"
    Me.txtName.Size = New System.Drawing.Size(378, 25)
    Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtName.TabIndex = 2
    Me.txtName.Text = "txtName"
    '
    'lblName
    '
    Me.lblName.AutoSize = True
    Me.lblName.Location = New System.Drawing.Point(42, 60)
    Me.lblName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblName.Name = "lblName"
    Me.lblName.Size = New System.Drawing.Size(18, 13)
    Me.lblName.TabIndex = 3
    Me.lblName.Text = "Name"
    '
    'DtxtDefaultTextFields
    '
    Me.txtDefaultTextFields.Location = New System.Drawing.Point(192, 97)
    Me.txtDefaultTextFields.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDefaultTextFields.Name = "txtDefaultTextFields"
    Me.txtDefaultTextFields.Size = New System.Drawing.Size(378, 105)
    Me.txtDefaultTextFields.Multiline = True
    Me.txtDefaultTextFields.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtDefaultTextFields.WordWrap = False 
    Me.txtDefaultTextFields.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDefaultTextFields.TabIndex = 4
    Me.txtDefaultTextFields.Text = "txtDefaultTextFields"
    '
    'lblDefaultTextFields
    '
    Me.lblDefaultTextFields.AutoSize = True
    Me.lblDefaultTextFields.Location = New System.Drawing.Point(42, 95)
    Me.lblDefaultTextFields.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDefaultTextFields.Name = "lblDefaultTextFields"
    Me.lblDefaultTextFields.Size = New System.Drawing.Size(18, 13)
    Me.lblDefaultTextFields.TabIndex = 5
    Me.lblDefaultTextFields.Text = "Default Text Fields"
    '
    'chkUsedForIdentity
    '
    Me.chkUsedForIdentity.AutoSize = True
    Me.chkUsedForIdentity.Location = New System.Drawing.Point(192, 223)
    Me.chkUsedForIdentity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkUsedForIdentity.Name = "chkUsedForIdentity"
    Me.chkUsedForIdentity.Size = New System.Drawing.Size(15, 14)
    Me.chkUsedForIdentity.TabIndex = 6
    Me.chkUsedForIdentity.UseVisualStyleBackColor = True
    '
    'lblUsedForIdentity
    '
    Me.lblUsedForIdentity.AutoSize = True
    Me.lblUsedForIdentity.Location = New System.Drawing.Point(42, 218)
    Me.lblUsedForIdentity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUsedForIdentity.Name = "lblUsedForIdentity"
    Me.lblUsedForIdentity.Size = New System.Drawing.Size(18, 13)
    Me.lblUsedForIdentity.TabIndex = 7
    Me.lblUsedForIdentity.Text = "Used For Identity"
    '
    'chkIsSingleRow
    '
    Me.chkIsSingleRow.AutoSize = True
    Me.chkIsSingleRow.Location = New System.Drawing.Point(192, 263)
    Me.chkIsSingleRow.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkIsSingleRow.Name = "chkIsSingleRow"
    Me.chkIsSingleRow.Size = New System.Drawing.Size(15, 14)
    Me.chkIsSingleRow.TabIndex = 8
    Me.chkIsSingleRow.UseVisualStyleBackColor = True
    '
    'lblIsSingleRow
    '
    Me.lblIsSingleRow.AutoSize = True
    Me.lblIsSingleRow.Location = New System.Drawing.Point(42, 258)
    Me.lblIsSingleRow.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIsSingleRow.Name = "lblIsSingleRow"
    Me.lblIsSingleRow.Size = New System.Drawing.Size(18, 13)
    Me.lblIsSingleRow.TabIndex = 9
    Me.lblIsSingleRow.Text = "Is Single Row"
    '
    'DtxtCanAdd
    '
    Me.txtCanAdd.Location = New System.Drawing.Point(192, 297)
    Me.txtCanAdd.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCanAdd.Name = "txtCanAdd"
    Me.txtCanAdd.Size = New System.Drawing.Size(378, 25)
    Me.txtCanAdd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCanAdd.TabIndex = 10
    Me.txtCanAdd.Text = "txtCanAdd"
    '
    'lblCanAdd
    '
    Me.lblCanAdd.AutoSize = True
    Me.lblCanAdd.Location = New System.Drawing.Point(42, 300)
    Me.lblCanAdd.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCanAdd.Name = "lblCanAdd"
    Me.lblCanAdd.Size = New System.Drawing.Size(18, 13)
    Me.lblCanAdd.TabIndex = 11
    Me.lblCanAdd.Text = "Can Add"
    '
    'DtxtCanEdit
    '
    Me.txtCanEdit.Location = New System.Drawing.Point(192, 337)
    Me.txtCanEdit.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCanEdit.Name = "txtCanEdit"
    Me.txtCanEdit.Size = New System.Drawing.Size(378, 25)
    Me.txtCanEdit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCanEdit.TabIndex = 12
    Me.txtCanEdit.Text = "txtCanEdit"
    '
    'lblCanEdit
    '
    Me.lblCanEdit.AutoSize = True
    Me.lblCanEdit.Location = New System.Drawing.Point(42, 340)
    Me.lblCanEdit.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCanEdit.Name = "lblCanEdit"
    Me.lblCanEdit.Size = New System.Drawing.Size(18, 13)
    Me.lblCanEdit.TabIndex = 13
    Me.lblCanEdit.Text = "Can Edit"
    '
    'DtxtCanDelete
    '
    Me.txtCanDelete.Location = New System.Drawing.Point(192, 377)
    Me.txtCanDelete.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCanDelete.Name = "txtCanDelete"
    Me.txtCanDelete.Size = New System.Drawing.Size(378, 25)
    Me.txtCanDelete.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCanDelete.TabIndex = 14
    Me.txtCanDelete.Text = "txtCanDelete"
    '
    'lblCanDelete
    '
    Me.lblCanDelete.AutoSize = True
    Me.lblCanDelete.Location = New System.Drawing.Point(42, 380)
    Me.lblCanDelete.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCanDelete.Name = "lblCanDelete"
    Me.lblCanDelete.Size = New System.Drawing.Size(18, 13)
    Me.lblCanDelete.TabIndex = 15
    Me.lblCanDelete.Text = "Can Delete"
    '
    'chkAuditAdd
    '
    Me.chkAuditAdd.AutoSize = True
    Me.chkAuditAdd.Location = New System.Drawing.Point(192, 423)
    Me.chkAuditAdd.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkAuditAdd.Name = "chkAuditAdd"
    Me.chkAuditAdd.Size = New System.Drawing.Size(15, 14)
    Me.chkAuditAdd.TabIndex = 16
    Me.chkAuditAdd.UseVisualStyleBackColor = True
    '
    'lblAuditAdd
    '
    Me.lblAuditAdd.AutoSize = True
    Me.lblAuditAdd.Location = New System.Drawing.Point(42, 418)
    Me.lblAuditAdd.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAuditAdd.Name = "lblAuditAdd"
    Me.lblAuditAdd.Size = New System.Drawing.Size(18, 13)
    Me.lblAuditAdd.TabIndex = 17
    Me.lblAuditAdd.Text = "Audit Add"
    '
    'chkAuditEdit
    '
    Me.chkAuditEdit.AutoSize = True
    Me.chkAuditEdit.Location = New System.Drawing.Point(192, 463)
    Me.chkAuditEdit.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkAuditEdit.Name = "chkAuditEdit"
    Me.chkAuditEdit.Size = New System.Drawing.Size(15, 14)
    Me.chkAuditEdit.TabIndex = 18
    Me.chkAuditEdit.UseVisualStyleBackColor = True
    '
    'lblAuditEdit
    '
    Me.lblAuditEdit.AutoSize = True
    Me.lblAuditEdit.Location = New System.Drawing.Point(42, 458)
    Me.lblAuditEdit.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAuditEdit.Name = "lblAuditEdit"
    Me.lblAuditEdit.Size = New System.Drawing.Size(18, 13)
    Me.lblAuditEdit.TabIndex = 19
    Me.lblAuditEdit.Text = "Audit Edit"
    '
    'chkAuditDelete
    '
    Me.chkAuditDelete.AutoSize = True
    Me.chkAuditDelete.Location = New System.Drawing.Point(192, 503)
    Me.chkAuditDelete.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkAuditDelete.Name = "chkAuditDelete"
    Me.chkAuditDelete.Size = New System.Drawing.Size(15, 14)
    Me.chkAuditDelete.TabIndex = 20
    Me.chkAuditDelete.UseVisualStyleBackColor = True
    '
    'lblAuditDelete
    '
    Me.lblAuditDelete.AutoSize = True
    Me.lblAuditDelete.Location = New System.Drawing.Point(42, 498)
    Me.lblAuditDelete.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAuditDelete.Name = "lblAuditDelete"
    Me.lblAuditDelete.Size = New System.Drawing.Size(18, 13)
    Me.lblAuditDelete.TabIndex = 21
    Me.lblAuditDelete.Text = "Audit Delete"
    '
    'chkTrackRowChangers
    '
    Me.chkTrackRowChangers.AutoSize = True
    Me.chkTrackRowChangers.Location = New System.Drawing.Point(192, 543)
    Me.chkTrackRowChangers.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkTrackRowChangers.Name = "chkTrackRowChangers"
    Me.chkTrackRowChangers.Size = New System.Drawing.Size(15, 14)
    Me.chkTrackRowChangers.TabIndex = 22
    Me.chkTrackRowChangers.UseVisualStyleBackColor = True
    '
    'lblTrackRowChangers
    '
    Me.lblTrackRowChangers.AutoSize = True
    Me.lblTrackRowChangers.Location = New System.Drawing.Point(42, 538)
    Me.lblTrackRowChangers.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTrackRowChangers.Name = "lblTrackRowChangers"
    Me.lblTrackRowChangers.Size = New System.Drawing.Size(18, 13)
    Me.lblTrackRowChangers.TabIndex = 23
    Me.lblTrackRowChangers.Text = "Track Row Changers"
    '
    'chkCreateUIMenu
    '
    Me.chkCreateUIMenu.AutoSize = True
    Me.chkCreateUIMenu.Location = New System.Drawing.Point(192, 583)
    Me.chkCreateUIMenu.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkCreateUIMenu.Name = "chkCreateUIMenu"
    Me.chkCreateUIMenu.Size = New System.Drawing.Size(15, 14)
    Me.chkCreateUIMenu.TabIndex = 24
    Me.chkCreateUIMenu.UseVisualStyleBackColor = True
    '
    'lblCreateUIMenu
    '
    Me.lblCreateUIMenu.AutoSize = True
    Me.lblCreateUIMenu.Location = New System.Drawing.Point(42, 578)
    Me.lblCreateUIMenu.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCreateUIMenu.Name = "lblCreateUIMenu"
    Me.lblCreateUIMenu.Size = New System.Drawing.Size(18, 13)
    Me.lblCreateUIMenu.TabIndex = 25
    Me.lblCreateUIMenu.Text = "Create UI Menu"
    '
    'chkCreateUICollection
    '
    Me.chkCreateUICollection.AutoSize = True
    Me.chkCreateUICollection.Location = New System.Drawing.Point(192, 623)
    Me.chkCreateUICollection.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkCreateUICollection.Name = "chkCreateUICollection"
    Me.chkCreateUICollection.Size = New System.Drawing.Size(15, 14)
    Me.chkCreateUICollection.TabIndex = 26
    Me.chkCreateUICollection.UseVisualStyleBackColor = True
    '
    'lblCreateUICollection
    '
    Me.lblCreateUICollection.AutoSize = True
    Me.lblCreateUICollection.Location = New System.Drawing.Point(42, 618)
    Me.lblCreateUICollection.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCreateUICollection.Name = "lblCreateUICollection"
    Me.lblCreateUICollection.Size = New System.Drawing.Size(18, 13)
    Me.lblCreateUICollection.TabIndex = 27
    Me.lblCreateUICollection.Text = "Create UI Collection"
    '
    'chkCreateUIEntity
    '
    Me.chkCreateUIEntity.AutoSize = True
    Me.chkCreateUIEntity.Location = New System.Drawing.Point(192, 663)
    Me.chkCreateUIEntity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkCreateUIEntity.Name = "chkCreateUIEntity"
    Me.chkCreateUIEntity.Size = New System.Drawing.Size(15, 14)
    Me.chkCreateUIEntity.TabIndex = 28
    Me.chkCreateUIEntity.UseVisualStyleBackColor = True
    '
    'lblCreateUIEntity
    '
    Me.lblCreateUIEntity.AutoSize = True
    Me.lblCreateUIEntity.Location = New System.Drawing.Point(42, 658)
    Me.lblCreateUIEntity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCreateUIEntity.Name = "lblCreateUIEntity"
    Me.lblCreateUIEntity.Size = New System.Drawing.Size(18, 13)
    Me.lblCreateUIEntity.TabIndex = 29
    Me.lblCreateUIEntity.Text = "Create UI Entity"
    '
    'DtxtSortOrder
    '
    Me.txtSortOrder.Location = New System.Drawing.Point(192, 697)
    Me.txtSortOrder.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSortOrder.Name = "txtSortOrder"
    Me.txtSortOrder.Size = New System.Drawing.Size(378, 25)
    Me.txtSortOrder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSortOrder.TabIndex = 30
    Me.txtSortOrder.Text = "txtSortOrder"
    '
    'lblSortOrder
    '
    Me.lblSortOrder.AutoSize = True
    Me.lblSortOrder.Location = New System.Drawing.Point(42, 700)
    Me.lblSortOrder.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSortOrder.Name = "lblSortOrder"
    Me.lblSortOrder.Size = New System.Drawing.Size(18, 13)
    Me.lblSortOrder.TabIndex = 31
    Me.lblSortOrder.Text = "Sort Order"
    '
    'ctlTable 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtName)
    Me.Controls.Add(Me.lblName)
    Me.Controls.Add(Me.txtDefaultTextFields)
    Me.Controls.Add(Me.lblDefaultTextFields)
    Me.Controls.Add(Me.chkUsedForIdentity)
    Me.Controls.Add(Me.lblUsedForIdentity)
    Me.Controls.Add(Me.chkIsSingleRow)
    Me.Controls.Add(Me.lblIsSingleRow)
    Me.Controls.Add(Me.txtCanAdd)
    Me.Controls.Add(Me.lblCanAdd)
    Me.Controls.Add(Me.txtCanEdit)
    Me.Controls.Add(Me.lblCanEdit)
    Me.Controls.Add(Me.txtCanDelete)
    Me.Controls.Add(Me.lblCanDelete)
    Me.Controls.Add(Me.chkAuditAdd)
    Me.Controls.Add(Me.lblAuditAdd)
    Me.Controls.Add(Me.chkAuditEdit)
    Me.Controls.Add(Me.lblAuditEdit)
    Me.Controls.Add(Me.chkAuditDelete)
    Me.Controls.Add(Me.lblAuditDelete)
    Me.Controls.Add(Me.chkTrackRowChangers)
    Me.Controls.Add(Me.lblTrackRowChangers)
    Me.Controls.Add(Me.chkCreateUIMenu)
    Me.Controls.Add(Me.lblCreateUIMenu)
    Me.Controls.Add(Me.chkCreateUICollection)
    Me.Controls.Add(Me.lblCreateUICollection)
    Me.Controls.Add(Me.chkCreateUIEntity)
    Me.Controls.Add(Me.lblCreateUIEntity)
    Me.Controls.Add(Me.txtSortOrder)
    Me.Controls.Add(Me.lblSortOrder)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_Table"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtName As System.Windows.Forms.TextBox
  Friend WithEvents lblName As System.Windows.Forms.Label
  Friend WithEvents txtDefaultTextFields As System.Windows.Forms.TextBox
  Friend WithEvents lblDefaultTextFields As System.Windows.Forms.Label
  Friend WithEvents chkUsedForIdentity As System.Windows.Forms.CheckBox
  Friend WithEvents lblUsedForIdentity As System.Windows.Forms.Label
  Friend WithEvents chkIsSingleRow As System.Windows.Forms.CheckBox
  Friend WithEvents lblIsSingleRow As System.Windows.Forms.Label
  Friend WithEvents txtCanAdd As System.Windows.Forms.TextBox
  Friend WithEvents lblCanAdd As System.Windows.Forms.Label
  Friend WithEvents txtCanEdit As System.Windows.Forms.TextBox
  Friend WithEvents lblCanEdit As System.Windows.Forms.Label
  Friend WithEvents txtCanDelete As System.Windows.Forms.TextBox
  Friend WithEvents lblCanDelete As System.Windows.Forms.Label
  Friend WithEvents chkAuditAdd As System.Windows.Forms.CheckBox
  Friend WithEvents lblAuditAdd As System.Windows.Forms.Label
  Friend WithEvents chkAuditEdit As System.Windows.Forms.CheckBox
  Friend WithEvents lblAuditEdit As System.Windows.Forms.Label
  Friend WithEvents chkAuditDelete As System.Windows.Forms.CheckBox
  Friend WithEvents lblAuditDelete As System.Windows.Forms.Label
  Friend WithEvents chkTrackRowChangers As System.Windows.Forms.CheckBox
  Friend WithEvents lblTrackRowChangers As System.Windows.Forms.Label
  Friend WithEvents chkCreateUIMenu As System.Windows.Forms.CheckBox
  Friend WithEvents lblCreateUIMenu As System.Windows.Forms.Label
  Friend WithEvents chkCreateUICollection As System.Windows.Forms.CheckBox
  Friend WithEvents lblCreateUICollection As System.Windows.Forms.Label
  Friend WithEvents chkCreateUIEntity As System.Windows.Forms.CheckBox
  Friend WithEvents lblCreateUIEntity As System.Windows.Forms.Label
  Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
  Friend WithEvents lblSortOrder As System.Windows.Forms.Label

End Class
