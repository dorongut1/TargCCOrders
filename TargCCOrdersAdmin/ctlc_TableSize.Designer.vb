<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_TableSize
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
    Me.txtNumberOfRows = New System.Windows.Forms.TextBox()
    Me.lblNumberOfRows = New System.Windows.Forms.Label()
    Me.txtReservedSizeKb = New System.Windows.Forms.TextBox()
    Me.lblReservedSizeKb = New System.Windows.Forms.Label()
    Me.txtDataSizeKb = New System.Windows.Forms.TextBox()
    Me.lblDataSizeKb = New System.Windows.Forms.Label()
    Me.txtIndexSizeKb = New System.Windows.Forms.TextBox()
    Me.lblIndexSizeKb = New System.Windows.Forms.Label()
    Me.txtUnusedSizeKb = New System.Windows.Forms.TextBox()
    Me.lblUnusedSizeKb = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(174, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(396, 25)
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
    Me.txtTableName.Location = New System.Drawing.Point(174, 57)
    Me.txtTableName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTableName.Name = "txtTableName"
    Me.txtTableName.Size = New System.Drawing.Size(396, 105)
    Me.txtTableName.Multiline = True
    Me.txtTableName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtTableName.WordWrap = False 
    Me.txtTableName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTableName.TabIndex = 2
    Me.txtTableName.Text = "txtTableName"
    '
    'lblTableName
    '
    Me.lblTableName.AutoSize = True
    Me.lblTableName.Location = New System.Drawing.Point(42, 55)
    Me.lblTableName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTableName.Name = "lblTableName"
    Me.lblTableName.Size = New System.Drawing.Size(18, 13)
    Me.lblTableName.TabIndex = 3
    Me.lblTableName.Text = "Table Name"
    '
    'DtxtNumberOfRows
    '
    Me.txtNumberOfRows.Location = New System.Drawing.Point(174, 177)
    Me.txtNumberOfRows.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNumberOfRows.Name = "txtNumberOfRows"
    Me.txtNumberOfRows.Size = New System.Drawing.Size(396, 25)
    Me.txtNumberOfRows.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNumberOfRows.TabIndex = 4
    Me.txtNumberOfRows.Text = "txtNumberOfRows"
    '
    'lblNumberOfRows
    '
    Me.lblNumberOfRows.AutoSize = True
    Me.lblNumberOfRows.Location = New System.Drawing.Point(42, 180)
    Me.lblNumberOfRows.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNumberOfRows.Name = "lblNumberOfRows"
    Me.lblNumberOfRows.Size = New System.Drawing.Size(18, 13)
    Me.lblNumberOfRows.TabIndex = 5
    Me.lblNumberOfRows.Text = "Number Of Rows"
    '
    'DtxtReservedSizeKb
    '
    Me.txtReservedSizeKb.Location = New System.Drawing.Point(174, 217)
    Me.txtReservedSizeKb.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtReservedSizeKb.Name = "txtReservedSizeKb"
    Me.txtReservedSizeKb.Size = New System.Drawing.Size(396, 25)
    Me.txtReservedSizeKb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtReservedSizeKb.TabIndex = 6
    Me.txtReservedSizeKb.Text = "txtReservedSizeKb"
    '
    'lblReservedSizeKb
    '
    Me.lblReservedSizeKb.AutoSize = True
    Me.lblReservedSizeKb.Location = New System.Drawing.Point(42, 220)
    Me.lblReservedSizeKb.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblReservedSizeKb.Name = "lblReservedSizeKb"
    Me.lblReservedSizeKb.Size = New System.Drawing.Size(18, 13)
    Me.lblReservedSizeKb.TabIndex = 7
    Me.lblReservedSizeKb.Text = "Reserved Size Kb"
    '
    'DtxtDataSizeKb
    '
    Me.txtDataSizeKb.Location = New System.Drawing.Point(174, 257)
    Me.txtDataSizeKb.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDataSizeKb.Name = "txtDataSizeKb"
    Me.txtDataSizeKb.Size = New System.Drawing.Size(396, 25)
    Me.txtDataSizeKb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDataSizeKb.TabIndex = 8
    Me.txtDataSizeKb.Text = "txtDataSizeKb"
    '
    'lblDataSizeKb
    '
    Me.lblDataSizeKb.AutoSize = True
    Me.lblDataSizeKb.Location = New System.Drawing.Point(42, 260)
    Me.lblDataSizeKb.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDataSizeKb.Name = "lblDataSizeKb"
    Me.lblDataSizeKb.Size = New System.Drawing.Size(18, 13)
    Me.lblDataSizeKb.TabIndex = 9
    Me.lblDataSizeKb.Text = "Data Size Kb"
    '
    'DtxtIndexSizeKb
    '
    Me.txtIndexSizeKb.Location = New System.Drawing.Point(174, 297)
    Me.txtIndexSizeKb.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtIndexSizeKb.Name = "txtIndexSizeKb"
    Me.txtIndexSizeKb.Size = New System.Drawing.Size(396, 25)
    Me.txtIndexSizeKb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtIndexSizeKb.TabIndex = 10
    Me.txtIndexSizeKb.Text = "txtIndexSizeKb"
    '
    'lblIndexSizeKb
    '
    Me.lblIndexSizeKb.AutoSize = True
    Me.lblIndexSizeKb.Location = New System.Drawing.Point(42, 300)
    Me.lblIndexSizeKb.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIndexSizeKb.Name = "lblIndexSizeKb"
    Me.lblIndexSizeKb.Size = New System.Drawing.Size(18, 13)
    Me.lblIndexSizeKb.TabIndex = 11
    Me.lblIndexSizeKb.Text = "Index Size Kb"
    '
    'DtxtUnusedSizeKb
    '
    Me.txtUnusedSizeKb.Location = New System.Drawing.Point(174, 337)
    Me.txtUnusedSizeKb.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUnusedSizeKb.Name = "txtUnusedSizeKb"
    Me.txtUnusedSizeKb.Size = New System.Drawing.Size(396, 25)
    Me.txtUnusedSizeKb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUnusedSizeKb.TabIndex = 12
    Me.txtUnusedSizeKb.Text = "txtUnusedSizeKb"
    '
    'lblUnusedSizeKb
    '
    Me.lblUnusedSizeKb.AutoSize = True
    Me.lblUnusedSizeKb.Location = New System.Drawing.Point(42, 340)
    Me.lblUnusedSizeKb.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUnusedSizeKb.Name = "lblUnusedSizeKb"
    Me.lblUnusedSizeKb.Size = New System.Drawing.Size(18, 13)
    Me.lblUnusedSizeKb.TabIndex = 13
    Me.lblUnusedSizeKb.Text = "Unused Size Kb"
    '
    'ctlTableSize 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtTableName)
    Me.Controls.Add(Me.lblTableName)
    Me.Controls.Add(Me.txtNumberOfRows)
    Me.Controls.Add(Me.lblNumberOfRows)
    Me.Controls.Add(Me.txtReservedSizeKb)
    Me.Controls.Add(Me.lblReservedSizeKb)
    Me.Controls.Add(Me.txtDataSizeKb)
    Me.Controls.Add(Me.lblDataSizeKb)
    Me.Controls.Add(Me.txtIndexSizeKb)
    Me.Controls.Add(Me.lblIndexSizeKb)
    Me.Controls.Add(Me.txtUnusedSizeKb)
    Me.Controls.Add(Me.lblUnusedSizeKb)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_TableSize"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtTableName As System.Windows.Forms.TextBox
  Friend WithEvents lblTableName As System.Windows.Forms.Label
  Friend WithEvents txtNumberOfRows As System.Windows.Forms.TextBox
  Friend WithEvents lblNumberOfRows As System.Windows.Forms.Label
  Friend WithEvents txtReservedSizeKb As System.Windows.Forms.TextBox
  Friend WithEvents lblReservedSizeKb As System.Windows.Forms.Label
  Friend WithEvents txtDataSizeKb As System.Windows.Forms.TextBox
  Friend WithEvents lblDataSizeKb As System.Windows.Forms.Label
  Friend WithEvents txtIndexSizeKb As System.Windows.Forms.TextBox
  Friend WithEvents lblIndexSizeKb As System.Windows.Forms.Label
  Friend WithEvents txtUnusedSizeKb As System.Windows.Forms.TextBox
  Friend WithEvents lblUnusedSizeKb As System.Windows.Forms.Label

End Class
