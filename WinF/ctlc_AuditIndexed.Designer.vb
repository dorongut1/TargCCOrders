<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_AuditIndexed
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
    Me.txtOriginalID = New System.Windows.Forms.TextBox()
    Me.lblOriginalID = New System.Windows.Forms.Label()
    Me.txtTableName = New System.Windows.Forms.TextBox()
    Me.lblTableName = New System.Windows.Forms.Label()
    Me.txtRowID = New System.Windows.Forms.TextBox()
    Me.lblRowID = New System.Windows.Forms.Label()
    Me.txtOperation = New System.Windows.Forms.TextBox()
    Me.lblOperation = New System.Windows.Forms.Label()
    Me.txtOccurredAt = New System.Windows.Forms.TextBox()
    Me.lblOccurredAt = New System.Windows.Forms.Label()
    Me.txtSqlCurrentUser = New System.Windows.Forms.TextBox()
    Me.lblSqlCurrentUser = New System.Windows.Forms.Label()
    Me.txtFieldName = New System.Windows.Forms.TextBox()
    Me.lblFieldName = New System.Windows.Forms.Label()
    Me.txtOldValue = New System.Windows.Forms.TextBox()
    Me.lblOldValue = New System.Windows.Forms.Label()
    Me.txtNewValue = New System.Windows.Forms.TextBox()
    Me.lblNewValue = New System.Windows.Forms.Label()
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
    'DtxtOriginalID
    '
    Me.txtOriginalID.Location = New System.Drawing.Point(175, 57)
    Me.txtOriginalID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOriginalID.Name = "txtOriginalID"
    Me.txtOriginalID.Size = New System.Drawing.Size(395, 25)
    Me.txtOriginalID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOriginalID.TabIndex = 2
    Me.txtOriginalID.Text = "txtOriginalID"
    '
    'lblOriginalID
    '
    Me.lblOriginalID.AutoSize = True
    Me.lblOriginalID.Location = New System.Drawing.Point(42, 60)
    Me.lblOriginalID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOriginalID.Name = "lblOriginalID"
    Me.lblOriginalID.Size = New System.Drawing.Size(18, 13)
    Me.lblOriginalID.TabIndex = 3
    Me.lblOriginalID.Text = "Original ID"
    '
    'DtxtTableName
    '
    Me.txtTableName.Location = New System.Drawing.Point(175, 97)
    Me.txtTableName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTableName.Name = "txtTableName"
    Me.txtTableName.Size = New System.Drawing.Size(395, 25)
    Me.txtTableName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTableName.TabIndex = 4
    Me.txtTableName.Text = "txtTableName"
    '
    'lblTableName
    '
    Me.lblTableName.AutoSize = True
    Me.lblTableName.Location = New System.Drawing.Point(42, 100)
    Me.lblTableName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTableName.Name = "lblTableName"
    Me.lblTableName.Size = New System.Drawing.Size(18, 13)
    Me.lblTableName.TabIndex = 5
    Me.lblTableName.Text = "Table Name"
    '
    'DtxtRowID
    '
    Me.txtRowID.Location = New System.Drawing.Point(175, 137)
    Me.txtRowID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRowID.Name = "txtRowID"
    Me.txtRowID.Size = New System.Drawing.Size(395, 25)
    Me.txtRowID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRowID.TabIndex = 6
    Me.txtRowID.Text = "txtRowID"
    '
    'lblRowID
    '
    Me.lblRowID.AutoSize = True
    Me.lblRowID.Location = New System.Drawing.Point(42, 140)
    Me.lblRowID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblRowID.Name = "lblRowID"
    Me.lblRowID.Size = New System.Drawing.Size(18, 13)
    Me.lblRowID.TabIndex = 7
    Me.lblRowID.Text = "Row ID"
    '
    'DtxtOperation
    '
    Me.txtOperation.Location = New System.Drawing.Point(175, 177)
    Me.txtOperation.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOperation.Name = "txtOperation"
    Me.txtOperation.Size = New System.Drawing.Size(395, 25)
    Me.txtOperation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOperation.TabIndex = 8
    Me.txtOperation.Text = "txtOperation"
    '
    'lblOperation
    '
    Me.lblOperation.AutoSize = True
    Me.lblOperation.Location = New System.Drawing.Point(42, 180)
    Me.lblOperation.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOperation.Name = "lblOperation"
    Me.lblOperation.Size = New System.Drawing.Size(18, 13)
    Me.lblOperation.TabIndex = 9
    Me.lblOperation.Text = "Operation"
    '
    'CtxtOccurredAt
    '
    Me.txtOccurredAt.Location = New System.Drawing.Point(175, 217)
    Me.txtOccurredAt.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOccurredAt.Name = "txtOccurredAt"
    Me.txtOccurredAt.Size = New System.Drawing.Size(395, 20)
    Me.txtOccurredAt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOccurredAt.TabIndex = 10
    Me.txtOccurredAt.Text = "txtOccurredAt"
    '
    'lblOccurredAt
    '
    Me.lblOccurredAt.AutoSize = True
    Me.lblOccurredAt.Location = New System.Drawing.Point(42, 220)
    Me.lblOccurredAt.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOccurredAt.Name = "lblOccurredAt"
    Me.lblOccurredAt.Size = New System.Drawing.Size(18, 13)
    Me.lblOccurredAt.TabIndex = 11
    Me.lblOccurredAt.Text = "Occurred At"
    '
    'DtxtSqlCurrentUser
    '
    Me.txtSqlCurrentUser.Location = New System.Drawing.Point(175, 257)
    Me.txtSqlCurrentUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSqlCurrentUser.Name = "txtSqlCurrentUser"
    Me.txtSqlCurrentUser.Size = New System.Drawing.Size(395, 25)
    Me.txtSqlCurrentUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSqlCurrentUser.TabIndex = 12
    Me.txtSqlCurrentUser.Text = "txtSqlCurrentUser"
    '
    'lblSqlCurrentUser
    '
    Me.lblSqlCurrentUser.AutoSize = True
    Me.lblSqlCurrentUser.Location = New System.Drawing.Point(42, 260)
    Me.lblSqlCurrentUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSqlCurrentUser.Name = "lblSqlCurrentUser"
    Me.lblSqlCurrentUser.Size = New System.Drawing.Size(18, 13)
    Me.lblSqlCurrentUser.TabIndex = 13
    Me.lblSqlCurrentUser.Text = "Sql Current User"
    '
    'DtxtFieldName
    '
    Me.txtFieldName.Location = New System.Drawing.Point(175, 297)
    Me.txtFieldName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtFieldName.Name = "txtFieldName"
    Me.txtFieldName.Size = New System.Drawing.Size(395, 105)
    Me.txtFieldName.Multiline = True
    Me.txtFieldName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtFieldName.WordWrap = False 
    Me.txtFieldName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFieldName.TabIndex = 14
    Me.txtFieldName.Text = "txtFieldName"
    '
    'lblFieldName
    '
    Me.lblFieldName.AutoSize = True
    Me.lblFieldName.Location = New System.Drawing.Point(42, 295)
    Me.lblFieldName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblFieldName.Name = "lblFieldName"
    Me.lblFieldName.Size = New System.Drawing.Size(18, 13)
    Me.lblFieldName.TabIndex = 15
    Me.lblFieldName.Text = "Field Name"
    '
    'DtxtOldValue
    '
    Me.txtOldValue.Location = New System.Drawing.Point(175, 417)
    Me.txtOldValue.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOldValue.Name = "txtOldValue"
    Me.txtOldValue.Size = New System.Drawing.Size(395, 105)
    Me.txtOldValue.Multiline = True
    Me.txtOldValue.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtOldValue.WordWrap = False 
    Me.txtOldValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOldValue.TabIndex = 16
    Me.txtOldValue.Text = "txtOldValue"
    '
    'lblOldValue
    '
    Me.lblOldValue.AutoSize = True
    Me.lblOldValue.Location = New System.Drawing.Point(42, 415)
    Me.lblOldValue.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOldValue.Name = "lblOldValue"
    Me.lblOldValue.Size = New System.Drawing.Size(18, 13)
    Me.lblOldValue.TabIndex = 17
    Me.lblOldValue.Text = "Old Value"
    '
    'DtxtNewValue
    '
    Me.txtNewValue.Location = New System.Drawing.Point(175, 537)
    Me.txtNewValue.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNewValue.Name = "txtNewValue"
    Me.txtNewValue.Size = New System.Drawing.Size(395, 105)
    Me.txtNewValue.Multiline = True
    Me.txtNewValue.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNewValue.WordWrap = False 
    Me.txtNewValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNewValue.TabIndex = 18
    Me.txtNewValue.Text = "txtNewValue"
    '
    'lblNewValue
    '
    Me.lblNewValue.AutoSize = True
    Me.lblNewValue.Location = New System.Drawing.Point(42, 535)
    Me.lblNewValue.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNewValue.Name = "lblNewValue"
    Me.lblNewValue.Size = New System.Drawing.Size(18, 13)
    Me.lblNewValue.TabIndex = 19
    Me.lblNewValue.Text = "New Value"
    '
    'DtxtChangedByUser
    '
    Me.txtChangedByUser.Location = New System.Drawing.Point(175, 657)
    Me.txtChangedByUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtChangedByUser.Name = "txtChangedByUser"
    Me.txtChangedByUser.Size = New System.Drawing.Size(395, 25)
    Me.txtChangedByUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtChangedByUser.TabIndex = 20
    Me.txtChangedByUser.Text = "txtChangedByUser"
    '
    'lblChangedByUser
    '
    Me.lblChangedByUser.AutoSize = True
    Me.lblChangedByUser.Location = New System.Drawing.Point(42, 660)
    Me.lblChangedByUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblChangedByUser.Name = "lblChangedByUser"
    Me.lblChangedByUser.Size = New System.Drawing.Size(18, 13)
    Me.lblChangedByUser.TabIndex = 21
    Me.lblChangedByUser.Text = "Changed By User"
    '
    'DtxtActiveLoginID
    '
    Me.txtActiveLoginID.Location = New System.Drawing.Point(175, 697)
    Me.txtActiveLoginID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtActiveLoginID.Name = "txtActiveLoginID"
    Me.txtActiveLoginID.Size = New System.Drawing.Size(395, 25)
    Me.txtActiveLoginID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtActiveLoginID.TabIndex = 22
    Me.txtActiveLoginID.Text = "txtActiveLoginID"
    '
    'lblActiveLoginID
    '
    Me.lblActiveLoginID.AutoSize = True
    Me.lblActiveLoginID.Location = New System.Drawing.Point(42, 700)
    Me.lblActiveLoginID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblActiveLoginID.Name = "lblActiveLoginID"
    Me.lblActiveLoginID.Size = New System.Drawing.Size(18, 13)
    Me.lblActiveLoginID.TabIndex = 23
    Me.lblActiveLoginID.Text = "Active Login ID"
    '
    'DtxtSqlSystemUser
    '
    Me.txtSqlSystemUser.Location = New System.Drawing.Point(175, 737)
    Me.txtSqlSystemUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSqlSystemUser.Name = "txtSqlSystemUser"
    Me.txtSqlSystemUser.Size = New System.Drawing.Size(395, 25)
    Me.txtSqlSystemUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSqlSystemUser.TabIndex = 24
    Me.txtSqlSystemUser.Text = "txtSqlSystemUser"
    '
    'lblSqlSystemUser
    '
    Me.lblSqlSystemUser.AutoSize = True
    Me.lblSqlSystemUser.Location = New System.Drawing.Point(42, 740)
    Me.lblSqlSystemUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSqlSystemUser.Name = "lblSqlSystemUser"
    Me.lblSqlSystemUser.Size = New System.Drawing.Size(18, 13)
    Me.lblSqlSystemUser.TabIndex = 25
    Me.lblSqlSystemUser.Text = "Sql System User"
    '
    'DtxtSqlAppName
    '
    Me.txtSqlAppName.Location = New System.Drawing.Point(175, 777)
    Me.txtSqlAppName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSqlAppName.Name = "txtSqlAppName"
    Me.txtSqlAppName.Size = New System.Drawing.Size(395, 105)
    Me.txtSqlAppName.Multiline = True
    Me.txtSqlAppName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtSqlAppName.WordWrap = False 
    Me.txtSqlAppName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSqlAppName.TabIndex = 26
    Me.txtSqlAppName.Text = "txtSqlAppName"
    '
    'lblSqlAppName
    '
    Me.lblSqlAppName.AutoSize = True
    Me.lblSqlAppName.Location = New System.Drawing.Point(42, 775)
    Me.lblSqlAppName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSqlAppName.Name = "lblSqlAppName"
    Me.lblSqlAppName.Size = New System.Drawing.Size(18, 13)
    Me.lblSqlAppName.TabIndex = 27
    Me.lblSqlAppName.Text = "Sql App Name"
    '
    'DtxtSqlHostName
    '
    Me.txtSqlHostName.Location = New System.Drawing.Point(175, 897)
    Me.txtSqlHostName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSqlHostName.Name = "txtSqlHostName"
    Me.txtSqlHostName.Size = New System.Drawing.Size(395, 25)
    Me.txtSqlHostName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSqlHostName.TabIndex = 28
    Me.txtSqlHostName.Text = "txtSqlHostName"
    '
    'lblSqlHostName
    '
    Me.lblSqlHostName.AutoSize = True
    Me.lblSqlHostName.Location = New System.Drawing.Point(42, 900)
    Me.lblSqlHostName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSqlHostName.Name = "lblSqlHostName"
    Me.lblSqlHostName.Size = New System.Drawing.Size(18, 13)
    Me.lblSqlHostName.TabIndex = 29
    Me.lblSqlHostName.Text = "Sql Host Name"
    '
    'ctlAuditIndexed 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtOriginalID)
    Me.Controls.Add(Me.lblOriginalID)
    Me.Controls.Add(Me.txtTableName)
    Me.Controls.Add(Me.lblTableName)
    Me.Controls.Add(Me.txtRowID)
    Me.Controls.Add(Me.lblRowID)
    Me.Controls.Add(Me.txtOperation)
    Me.Controls.Add(Me.lblOperation)
    Me.Controls.Add(Me.txtOccurredAt)
    Me.Controls.Add(Me.lblOccurredAt)
    Me.Controls.Add(Me.txtSqlCurrentUser)
    Me.Controls.Add(Me.lblSqlCurrentUser)
    Me.Controls.Add(Me.txtFieldName)
    Me.Controls.Add(Me.lblFieldName)
    Me.Controls.Add(Me.txtOldValue)
    Me.Controls.Add(Me.lblOldValue)
    Me.Controls.Add(Me.txtNewValue)
    Me.Controls.Add(Me.lblNewValue)
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
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_AuditIndexed"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtOriginalID As System.Windows.Forms.TextBox
  Friend WithEvents lblOriginalID As System.Windows.Forms.Label
  Friend WithEvents txtTableName As System.Windows.Forms.TextBox
  Friend WithEvents lblTableName As System.Windows.Forms.Label
  Friend WithEvents txtRowID As System.Windows.Forms.TextBox
  Friend WithEvents lblRowID As System.Windows.Forms.Label
  Friend WithEvents txtOperation As System.Windows.Forms.TextBox
  Friend WithEvents lblOperation As System.Windows.Forms.Label
  Friend WithEvents txtOccurredAt As System.Windows.Forms.TextBox
  Friend WithEvents lblOccurredAt As System.Windows.Forms.Label
  Friend WithEvents txtSqlCurrentUser As System.Windows.Forms.TextBox
  Friend WithEvents lblSqlCurrentUser As System.Windows.Forms.Label
  Friend WithEvents txtFieldName As System.Windows.Forms.TextBox
  Friend WithEvents lblFieldName As System.Windows.Forms.Label
  Friend WithEvents txtOldValue As System.Windows.Forms.TextBox
  Friend WithEvents lblOldValue As System.Windows.Forms.Label
  Friend WithEvents txtNewValue As System.Windows.Forms.TextBox
  Friend WithEvents lblNewValue As System.Windows.Forms.Label
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

End Class
