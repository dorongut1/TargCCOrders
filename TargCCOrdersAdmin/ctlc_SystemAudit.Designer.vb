<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_SystemAudit
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
    Me.txtTableName = New System.Windows.Forms.TextBox()
    Me.lblTableName = New System.Windows.Forms.Label()
    Me.txtRowId = New System.Windows.Forms.TextBox()
    Me.lblRowId = New System.Windows.Forms.Label()
    Me.txtOperation = New System.Windows.Forms.TextBox()
    Me.lblOperation = New System.Windows.Forms.Label()
    Me.txtOccurredAt = New System.Windows.Forms.TextBox()
    Me.lblOccurredAt = New System.Windows.Forms.Label()
    Me.txtSqlCurrentUser = New System.Windows.Forms.TextBox()
    Me.lblSqlCurrentUser = New System.Windows.Forms.Label()
    Me.txtChangedByUser = New System.Windows.Forms.TextBox()
    Me.lblChangedByUser = New System.Windows.Forms.Label()
    Me.txtActiveLoginID = New System.Windows.Forms.TextBox()
    Me.lblActiveLoginID = New System.Windows.Forms.Label()
    Me.txtSqlSystemUser = New System.Windows.Forms.TextBox()
    Me.lblSqlSystemUser = New System.Windows.Forms.Label()
    Me.txtSqlAppName = New System.Windows.Forms.TextBox()
    Me.lblSqlAppName = New System.Windows.Forms.Label()
    Me.txtSqlHostName = New System.Windows.Forms.TextBox()
    Me.lblSqlHostName = New System.Windows.Forms.Label()
    Me.txtChanges = New System.Windows.Forms.TextBox()
    Me.lblChanges = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(175, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(395, 25)
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
    'DtxtTableName
    '
    Me.txtTableName.Location = New System.Drawing.Point(175, 57)
    Me.txtTableName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTableName.Name = "txtTableName"
    Me.txtTableName.Size = New System.Drawing.Size(395, 25)
    Me.txtTableName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTableName.TabIndex = 2
    Me.txtTableName.Text = "txtTableName"
    '
    'lblTableName
    '
    Me.lblTableName.AutoSize = True
    Me.lblTableName.Location = New System.Drawing.Point(42, 60)
    Me.lblTableName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTableName.Name = "lblTableName"
    Me.lblTableName.Size = New System.Drawing.Size(18, 13)
    Me.lblTableName.TabIndex = 3
    Me.lblTableName.Text = "Table Name"
    '
    'DtxtRowId
    '
    Me.txtRowId.Location = New System.Drawing.Point(175, 97)
    Me.txtRowId.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRowId.Name = "txtRowId"
    Me.txtRowId.Size = New System.Drawing.Size(395, 25)
    Me.txtRowId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRowId.TabIndex = 4
    Me.txtRowId.Text = "txtRowId"
    '
    'lblRowId
    '
    Me.lblRowId.AutoSize = True
    Me.lblRowId.Location = New System.Drawing.Point(42, 100)
    Me.lblRowId.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblRowId.Name = "lblRowId"
    Me.lblRowId.Size = New System.Drawing.Size(18, 13)
    Me.lblRowId.TabIndex = 5
    Me.lblRowId.Text = "Row Id"
    '
    'DtxtOperation
    '
    Me.txtOperation.Location = New System.Drawing.Point(175, 137)
    Me.txtOperation.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOperation.Name = "txtOperation"
    Me.txtOperation.Size = New System.Drawing.Size(395, 25)
    Me.txtOperation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOperation.TabIndex = 6
    Me.txtOperation.Text = "txtOperation"
    '
    'lblOperation
    '
    Me.lblOperation.AutoSize = True
    Me.lblOperation.Location = New System.Drawing.Point(42, 140)
    Me.lblOperation.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOperation.Name = "lblOperation"
    Me.lblOperation.Size = New System.Drawing.Size(18, 13)
    Me.lblOperation.TabIndex = 7
    Me.lblOperation.Text = "Operation"
    '
    'CtxtOccurredAt
    '
    Me.txtOccurredAt.Location = New System.Drawing.Point(175, 177)
    Me.txtOccurredAt.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOccurredAt.Name = "txtOccurredAt"
    Me.txtOccurredAt.Size = New System.Drawing.Size(395, 20)
    Me.txtOccurredAt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOccurredAt.TabIndex = 8
    Me.txtOccurredAt.Text = "txtOccurredAt"
    '
    'lblOccurredAt
    '
    Me.lblOccurredAt.AutoSize = True
    Me.lblOccurredAt.Location = New System.Drawing.Point(42, 180)
    Me.lblOccurredAt.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOccurredAt.Name = "lblOccurredAt"
    Me.lblOccurredAt.Size = New System.Drawing.Size(18, 13)
    Me.lblOccurredAt.TabIndex = 9
    Me.lblOccurredAt.Text = "Occurred At"
    '
    'DtxtSqlCurrentUser
    '
    Me.txtSqlCurrentUser.Location = New System.Drawing.Point(175, 217)
    Me.txtSqlCurrentUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSqlCurrentUser.Name = "txtSqlCurrentUser"
    Me.txtSqlCurrentUser.Size = New System.Drawing.Size(395, 25)
    Me.txtSqlCurrentUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSqlCurrentUser.TabIndex = 10
    Me.txtSqlCurrentUser.Text = "txtSqlCurrentUser"
    '
    'lblSqlCurrentUser
    '
    Me.lblSqlCurrentUser.AutoSize = True
    Me.lblSqlCurrentUser.Location = New System.Drawing.Point(42, 220)
    Me.lblSqlCurrentUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSqlCurrentUser.Name = "lblSqlCurrentUser"
    Me.lblSqlCurrentUser.Size = New System.Drawing.Size(18, 13)
    Me.lblSqlCurrentUser.TabIndex = 11
    Me.lblSqlCurrentUser.Text = "Sql Current User"
    '
    'DtxtChangedByUser
    '
    Me.txtChangedByUser.Location = New System.Drawing.Point(175, 257)
    Me.txtChangedByUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtChangedByUser.Name = "txtChangedByUser"
    Me.txtChangedByUser.Size = New System.Drawing.Size(395, 25)
    Me.txtChangedByUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtChangedByUser.TabIndex = 12
    Me.txtChangedByUser.Text = "txtChangedByUser"
    '
    'lblChangedByUser
    '
    Me.lblChangedByUser.AutoSize = True
    Me.lblChangedByUser.Location = New System.Drawing.Point(42, 260)
    Me.lblChangedByUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblChangedByUser.Name = "lblChangedByUser"
    Me.lblChangedByUser.Size = New System.Drawing.Size(18, 13)
    Me.lblChangedByUser.TabIndex = 13
    Me.lblChangedByUser.Text = "Changed By User"
    '
    'DtxtActiveLoginID
    '
    Me.txtActiveLoginID.Location = New System.Drawing.Point(175, 297)
    Me.txtActiveLoginID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtActiveLoginID.Name = "txtActiveLoginID"
    Me.txtActiveLoginID.Size = New System.Drawing.Size(395, 25)
    Me.txtActiveLoginID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtActiveLoginID.TabIndex = 14
    Me.txtActiveLoginID.Text = "txtActiveLoginID"
    '
    'lblActiveLoginID
    '
    Me.lblActiveLoginID.AutoSize = True
    Me.lblActiveLoginID.Location = New System.Drawing.Point(42, 300)
    Me.lblActiveLoginID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblActiveLoginID.Name = "lblActiveLoginID"
    Me.lblActiveLoginID.Size = New System.Drawing.Size(18, 13)
    Me.lblActiveLoginID.TabIndex = 15
    Me.lblActiveLoginID.Text = "Active Login ID"
    '
    'DtxtSqlSystemUser
    '
    Me.txtSqlSystemUser.Location = New System.Drawing.Point(175, 337)
    Me.txtSqlSystemUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSqlSystemUser.Name = "txtSqlSystemUser"
    Me.txtSqlSystemUser.Size = New System.Drawing.Size(395, 25)
    Me.txtSqlSystemUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSqlSystemUser.TabIndex = 16
    Me.txtSqlSystemUser.Text = "txtSqlSystemUser"
    '
    'lblSqlSystemUser
    '
    Me.lblSqlSystemUser.AutoSize = True
    Me.lblSqlSystemUser.Location = New System.Drawing.Point(42, 340)
    Me.lblSqlSystemUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSqlSystemUser.Name = "lblSqlSystemUser"
    Me.lblSqlSystemUser.Size = New System.Drawing.Size(18, 13)
    Me.lblSqlSystemUser.TabIndex = 17
    Me.lblSqlSystemUser.Text = "Sql System User"
    '
    'DtxtSqlAppName
    '
    Me.txtSqlAppName.Location = New System.Drawing.Point(175, 377)
    Me.txtSqlAppName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSqlAppName.Name = "txtSqlAppName"
    Me.txtSqlAppName.Size = New System.Drawing.Size(395, 105)
    Me.txtSqlAppName.Multiline = True
    Me.txtSqlAppName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtSqlAppName.WordWrap = False 
    Me.txtSqlAppName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSqlAppName.TabIndex = 18
    Me.txtSqlAppName.Text = "txtSqlAppName"
    '
    'lblSqlAppName
    '
    Me.lblSqlAppName.AutoSize = True
    Me.lblSqlAppName.Location = New System.Drawing.Point(42, 375)
    Me.lblSqlAppName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSqlAppName.Name = "lblSqlAppName"
    Me.lblSqlAppName.Size = New System.Drawing.Size(18, 13)
    Me.lblSqlAppName.TabIndex = 19
    Me.lblSqlAppName.Text = "Sql App Name"
    '
    'DtxtSqlHostName
    '
    Me.txtSqlHostName.Location = New System.Drawing.Point(175, 497)
    Me.txtSqlHostName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSqlHostName.Name = "txtSqlHostName"
    Me.txtSqlHostName.Size = New System.Drawing.Size(395, 25)
    Me.txtSqlHostName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSqlHostName.TabIndex = 20
    Me.txtSqlHostName.Text = "txtSqlHostName"
    '
    'lblSqlHostName
    '
    Me.lblSqlHostName.AutoSize = True
    Me.lblSqlHostName.Location = New System.Drawing.Point(42, 500)
    Me.lblSqlHostName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSqlHostName.Name = "lblSqlHostName"
    Me.lblSqlHostName.Size = New System.Drawing.Size(18, 13)
    Me.lblSqlHostName.TabIndex = 21
    Me.lblSqlHostName.Text = "Sql Host Name"
    '
    'DtxtChanges
    '
    Me.txtChanges.Location = New System.Drawing.Point(175, 537)
    Me.txtChanges.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtChanges.Name = "txtChanges"
    Me.txtChanges.Size = New System.Drawing.Size(395, 105)
    Me.txtChanges.Multiline = True
    Me.txtChanges.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtChanges.WordWrap = False 
    Me.txtChanges.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtChanges.TabIndex = 22
    Me.txtChanges.Text = "txtChanges"
    '
    'lblChanges
    '
    Me.lblChanges.AutoSize = True
    Me.lblChanges.Location = New System.Drawing.Point(42, 535)
    Me.lblChanges.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblChanges.Name = "lblChanges"
    Me.lblChanges.Size = New System.Drawing.Size(18, 13)
    Me.lblChanges.TabIndex = 23
    Me.lblChanges.Text = "Changes"
    '
    'ctlSystemAudit 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtTableName)
    Me.Controls.Add(Me.lblTableName)
    Me.Controls.Add(Me.txtRowId)
    Me.Controls.Add(Me.lblRowId)
    Me.Controls.Add(Me.txtOperation)
    Me.Controls.Add(Me.lblOperation)
    Me.Controls.Add(Me.txtOccurredAt)
    Me.Controls.Add(Me.lblOccurredAt)
    Me.Controls.Add(Me.txtSqlCurrentUser)
    Me.Controls.Add(Me.lblSqlCurrentUser)
    Me.Controls.Add(Me.txtChangedByUser)
    Me.Controls.Add(Me.lblChangedByUser)
    Me.Controls.Add(Me.txtActiveLoginID)
    Me.Controls.Add(Me.lblActiveLoginID)
    Me.Controls.Add(Me.txtSqlSystemUser)
    Me.Controls.Add(Me.lblSqlSystemUser)
    Me.Controls.Add(Me.txtSqlAppName)
    Me.Controls.Add(Me.lblSqlAppName)
    Me.Controls.Add(Me.txtSqlHostName)
    Me.Controls.Add(Me.lblSqlHostName)
    Me.Controls.Add(Me.txtChanges)
    Me.Controls.Add(Me.lblChanges)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_SystemAudit"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtTableName As System.Windows.Forms.TextBox
  Friend WithEvents lblTableName As System.Windows.Forms.Label
  Friend WithEvents txtRowId As System.Windows.Forms.TextBox
  Friend WithEvents lblRowId As System.Windows.Forms.Label
  Friend WithEvents txtOperation As System.Windows.Forms.TextBox
  Friend WithEvents lblOperation As System.Windows.Forms.Label
  Friend WithEvents txtOccurredAt As System.Windows.Forms.TextBox
  Friend WithEvents lblOccurredAt As System.Windows.Forms.Label
  Friend WithEvents txtSqlCurrentUser As System.Windows.Forms.TextBox
  Friend WithEvents lblSqlCurrentUser As System.Windows.Forms.Label
  Friend WithEvents txtChangedByUser As System.Windows.Forms.TextBox
  Friend WithEvents lblChangedByUser As System.Windows.Forms.Label
  Friend WithEvents txtActiveLoginID As System.Windows.Forms.TextBox
  Friend WithEvents lblActiveLoginID As System.Windows.Forms.Label
  Friend WithEvents txtSqlSystemUser As System.Windows.Forms.TextBox
  Friend WithEvents lblSqlSystemUser As System.Windows.Forms.Label
  Friend WithEvents txtSqlAppName As System.Windows.Forms.TextBox
  Friend WithEvents lblSqlAppName As System.Windows.Forms.Label
  Friend WithEvents txtSqlHostName As System.Windows.Forms.TextBox
  Friend WithEvents lblSqlHostName As System.Windows.Forms.Label
  Friend WithEvents txtChanges As System.Windows.Forms.TextBox
  Friend WithEvents lblChanges As System.Windows.Forms.Label

End Class
