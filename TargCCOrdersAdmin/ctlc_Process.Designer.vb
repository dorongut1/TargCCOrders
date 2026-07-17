<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_Process
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
    Me.txtDateChecked = New System.Windows.Forms.TextBox()
    Me.lblDateChecked = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(158, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(412, 25)
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
    Me.txtName.Location = New System.Drawing.Point(158, 57)
    Me.txtName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtName.Name = "txtName"
    Me.txtName.Size = New System.Drawing.Size(412, 105)
    Me.txtName.Multiline = True
    Me.txtName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtName.WordWrap = False 
    Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtName.TabIndex = 2
    Me.txtName.Text = "txtName"
    '
    'lblName
    '
    Me.lblName.AutoSize = True
    Me.lblName.Location = New System.Drawing.Point(42, 55)
    Me.lblName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblName.Name = "lblName"
    Me.lblName.Size = New System.Drawing.Size(18, 13)
    Me.lblName.TabIndex = 3
    Me.lblName.Text = "Name"
    '
    'CtxtDateChecked
    '
    Me.txtDateChecked.Location = New System.Drawing.Point(158, 177)
    Me.txtDateChecked.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDateChecked.Name = "txtDateChecked"
    Me.txtDateChecked.Size = New System.Drawing.Size(412, 20)
    Me.txtDateChecked.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDateChecked.TabIndex = 4
    Me.txtDateChecked.Text = "txtDateChecked"
    '
    'lblDateChecked
    '
    Me.lblDateChecked.AutoSize = True
    Me.lblDateChecked.Location = New System.Drawing.Point(42, 180)
    Me.lblDateChecked.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDateChecked.Name = "lblDateChecked"
    Me.lblDateChecked.Size = New System.Drawing.Size(18, 13)
    Me.lblDateChecked.TabIndex = 5
    Me.lblDateChecked.Text = "Date Checked"
    '
    'ctlProcess 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtName)
    Me.Controls.Add(Me.lblName)
    Me.Controls.Add(Me.txtDateChecked)
    Me.Controls.Add(Me.lblDateChecked)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_Process"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtName As System.Windows.Forms.TextBox
  Friend WithEvents lblName As System.Windows.Forms.Label
  Friend WithEvents txtDateChecked As System.Windows.Forms.TextBox
  Friend WithEvents lblDateChecked As System.Windows.Forms.Label

End Class
