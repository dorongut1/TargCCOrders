<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IntelliCombo
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
    Me.cbo = New System.Windows.Forms.ComboBox()
    Me.SuspendLayout()
    '
    'cbo
    '
    Me.cbo.Dock = System.Windows.Forms.DockStyle.Top
    Me.cbo.FormattingEnabled = True
    Me.cbo.Location = New System.Drawing.Point(0, 0)
    Me.cbo.Margin = New System.Windows.Forms.Padding(0)
    Me.cbo.Name = "cbo"
    Me.cbo.Size = New System.Drawing.Size(348, 21)
    Me.cbo.TabIndex = 0
    '
    'IntelliCombo
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.Controls.Add(Me.cbo)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    Me.Name = "IntelliCombo"
    Me.Size = New System.Drawing.Size(348, 25)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents cbo As System.Windows.Forms.ComboBox

End Class
