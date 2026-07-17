<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IntelliMultiCombo
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
    Me.flp = New System.Windows.Forms.FlowLayoutPanel
    Me.icbo01 = New IntelliCombo
    Me.icbo02 = New IntelliCombo
    Me.icbo03 = New IntelliCombo
    Me.flp.SuspendLayout()
    Me.SuspendLayout()
    '
    'flp
    '
    Me.flp.Controls.Add(Me.icbo01)
    Me.flp.Controls.Add(Me.icbo02)
    Me.flp.Controls.Add(Me.icbo03)
    Me.flp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.flp.Location = New System.Drawing.Point(0, 0)
    Me.flp.Margin = New System.Windows.Forms.Padding(0)
    Me.flp.Name = "flp"
    Me.flp.Size = New System.Drawing.Size(494, 95)
    Me.flp.TabIndex = 0
    '
    'icbo01
    '
    Me.icbo01.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
    Me.icbo01.Location = New System.Drawing.Point(0, 0)
    Me.icbo01.Margin = New System.Windows.Forms.Padding(0)
    Me.icbo01.Name = "icbo01"
    Me.icbo01.Size = New System.Drawing.Size(158, 25)
    Me.icbo01.TabIndex = 5
    '
    'icbo02
    '
    Me.icbo02.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
    Me.icbo02.Location = New System.Drawing.Point(158, 0)
    Me.icbo02.Margin = New System.Windows.Forms.Padding(0)
    Me.icbo02.Name = "icbo02"
    Me.icbo02.Size = New System.Drawing.Size(158, 25)
    Me.icbo02.TabIndex = 6
    '
    'icbo03
    '
    Me.icbo03.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
    Me.icbo03.Location = New System.Drawing.Point(316, 0)
    Me.icbo03.Margin = New System.Windows.Forms.Padding(0)
    Me.icbo03.Name = "icbo03"
    Me.icbo03.Size = New System.Drawing.Size(158, 25)
    Me.icbo03.TabIndex = 7
    '
    'IntelliMultiCombo
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.flp)
    Me.Margin = New System.Windows.Forms.Padding(0)
    Me.Name = "IntelliMultiCombo"
    Me.Size = New System.Drawing.Size(494, 95)
    Me.flp.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents flp As System.Windows.Forms.FlowLayoutPanel
  Friend WithEvents icbo01 As IntelliCombo
  Friend WithEvents icbo02 As IntelliCombo
  Friend WithEvents icbo03 As IntelliCombo

End Class
