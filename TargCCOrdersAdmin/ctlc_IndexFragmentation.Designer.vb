<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_IndexFragmentation
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
    Me.txtIndexName = New System.Windows.Forms.TextBox()
    Me.lblIndexName = New System.Windows.Forms.Label()
    Me.txtIndexType = New System.Windows.Forms.TextBox()
    Me.lblIndexType = New System.Windows.Forms.Label()
    Me.txtFragmentationPct = New System.Windows.Forms.TextBox()
    Me.lblFragmentationPct = New System.Windows.Forms.Label()
    Me.txtPageCount = New System.Windows.Forms.TextBox()
    Me.lblPageCount = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(185, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(385, 25)
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
    Me.txtTableName.Location = New System.Drawing.Point(185, 57)
    Me.txtTableName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTableName.Name = "txtTableName"
    Me.txtTableName.Size = New System.Drawing.Size(385, 105)
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
    'DtxtIndexName
    '
    Me.txtIndexName.Location = New System.Drawing.Point(185, 177)
    Me.txtIndexName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtIndexName.Name = "txtIndexName"
    Me.txtIndexName.Size = New System.Drawing.Size(385, 105)
    Me.txtIndexName.Multiline = True
    Me.txtIndexName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtIndexName.WordWrap = False 
    Me.txtIndexName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtIndexName.TabIndex = 4
    Me.txtIndexName.Text = "txtIndexName"
    '
    'lblIndexName
    '
    Me.lblIndexName.AutoSize = True
    Me.lblIndexName.Location = New System.Drawing.Point(42, 175)
    Me.lblIndexName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIndexName.Name = "lblIndexName"
    Me.lblIndexName.Size = New System.Drawing.Size(18, 13)
    Me.lblIndexName.TabIndex = 5
    Me.lblIndexName.Text = "Index Name"
    '
    'DtxtIndexType
    '
    Me.txtIndexType.Location = New System.Drawing.Point(185, 297)
    Me.txtIndexType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtIndexType.Name = "txtIndexType"
    Me.txtIndexType.Size = New System.Drawing.Size(385, 25)
    Me.txtIndexType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtIndexType.TabIndex = 6
    Me.txtIndexType.Text = "txtIndexType"
    '
    'lblIndexType
    '
    Me.lblIndexType.AutoSize = True
    Me.lblIndexType.Location = New System.Drawing.Point(42, 300)
    Me.lblIndexType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIndexType.Name = "lblIndexType"
    Me.lblIndexType.Size = New System.Drawing.Size(18, 13)
    Me.lblIndexType.TabIndex = 7
    Me.lblIndexType.Text = "Index Type"
    '
    'DtxtFragmentationPct
    '
    Me.txtFragmentationPct.Location = New System.Drawing.Point(185, 337)
    Me.txtFragmentationPct.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtFragmentationPct.Name = "txtFragmentationPct"
    Me.txtFragmentationPct.Size = New System.Drawing.Size(385, 25)
    Me.txtFragmentationPct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFragmentationPct.TabIndex = 8
    Me.txtFragmentationPct.Text = "txtFragmentationPct"
    '
    'lblFragmentationPct
    '
    Me.lblFragmentationPct.AutoSize = True
    Me.lblFragmentationPct.Location = New System.Drawing.Point(42, 340)
    Me.lblFragmentationPct.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblFragmentationPct.Name = "lblFragmentationPct"
    Me.lblFragmentationPct.Size = New System.Drawing.Size(18, 13)
    Me.lblFragmentationPct.TabIndex = 9
    Me.lblFragmentationPct.Text = "Fragmentation Pct"
    '
    'DtxtPageCount
    '
    Me.txtPageCount.Location = New System.Drawing.Point(185, 377)
    Me.txtPageCount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPageCount.Name = "txtPageCount"
    Me.txtPageCount.Size = New System.Drawing.Size(385, 25)
    Me.txtPageCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPageCount.TabIndex = 10
    Me.txtPageCount.Text = "txtPageCount"
    '
    'lblPageCount
    '
    Me.lblPageCount.AutoSize = True
    Me.lblPageCount.Location = New System.Drawing.Point(42, 380)
    Me.lblPageCount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblPageCount.Name = "lblPageCount"
    Me.lblPageCount.Size = New System.Drawing.Size(18, 13)
    Me.lblPageCount.TabIndex = 11
    Me.lblPageCount.Text = "Page Count"
    '
    'ctlIndexFragmentation 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtTableName)
    Me.Controls.Add(Me.lblTableName)
    Me.Controls.Add(Me.txtIndexName)
    Me.Controls.Add(Me.lblIndexName)
    Me.Controls.Add(Me.txtIndexType)
    Me.Controls.Add(Me.lblIndexType)
    Me.Controls.Add(Me.txtFragmentationPct)
    Me.Controls.Add(Me.lblFragmentationPct)
    Me.Controls.Add(Me.txtPageCount)
    Me.Controls.Add(Me.lblPageCount)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_IndexFragmentation"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtTableName As System.Windows.Forms.TextBox
  Friend WithEvents lblTableName As System.Windows.Forms.Label
  Friend WithEvents txtIndexName As System.Windows.Forms.TextBox
  Friend WithEvents lblIndexName As System.Windows.Forms.Label
  Friend WithEvents txtIndexType As System.Windows.Forms.TextBox
  Friend WithEvents lblIndexType As System.Windows.Forms.Label
  Friend WithEvents txtFragmentationPct As System.Windows.Forms.TextBox
  Friend WithEvents lblFragmentationPct As System.Windows.Forms.Label
  Friend WithEvents txtPageCount As System.Windows.Forms.TextBox
  Friend WithEvents lblPageCount As System.Windows.Forms.Label

End Class
