'BackColor = System.Drawing.Color.SeaShell

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateField
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
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
    Me.lblPrompt = New System.Windows.Forms.Label()
    Me.txtField = New System.Windows.Forms.TextBox()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnOK = New System.Windows.Forms.Button()
    Me.dtpField = New System.Windows.Forms.DateTimePicker()
    Me.chkField = New System.Windows.Forms.CheckBox()
    Me.cboField = New System.Windows.Forms.ComboBox()
    Me.lstField = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lblPrompt
        '
        Me.lblPrompt.AutoSize = True
        Me.lblPrompt.Location = New System.Drawing.Point(12, 9)
        Me.lblPrompt.Name = "lblPrompt"
        Me.lblPrompt.Size = New System.Drawing.Size(55, 19)
        Me.lblPrompt.TabIndex = 0
        Me.lblPrompt.Text = "Prompt"
        '
        'txtField
        '
        Me.txtField.Location = New System.Drawing.Point(15, 29)
        Me.txtField.MaxLength = 0
        Me.txtField.Name = "txtField"
        Me.txtField.Size = New System.Drawing.Size(297, 25)
        Me.txtField.TabIndex = 0
        Me.txtField.Text = "txtField"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(205, 82)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 26)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOK.Location = New System.Drawing.Point(286, 82)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 26)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'dtpField
        '
        Me.dtpField.CustomFormat = "ddd, dd MMM yyyy"
        Me.dtpField.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpField.Location = New System.Drawing.Point(88, 9)
        Me.dtpField.Name = "dtpField"
        Me.dtpField.Size = New System.Drawing.Size(200, 25)
        Me.dtpField.TabIndex = 4
        '
        'chkField
        '
        Me.chkField.AutoSize = True
        Me.chkField.Location = New System.Drawing.Point(15, 59)
        Me.chkField.Name = "chkField"
        Me.chkField.Size = New System.Drawing.Size(15, 14)
        Me.chkField.TabIndex = 5
        Me.chkField.UseVisualStyleBackColor = True
        '
        'cboField
        '
        Me.cboField.FormattingEnabled = True
        Me.cboField.Items.AddRange(New Object() {"y", "u", "i"})
        Me.cboField.Location = New System.Drawing.Point(15, 28)
        Me.cboField.Name = "cboField"
        Me.cboField.Size = New System.Drawing.Size(330, 25)
        Me.cboField.TabIndex = 6
        '
        'lstField
        '
        Me.lstField.FormattingEnabled = True
        Me.lstField.ItemHeight = 17
        Me.lstField.Location = New System.Drawing.Point(15, 28)
        Me.lstField.Name = "lstField"
        Me.lstField.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstField.Size = New System.Drawing.Size(251, 55)
        Me.lstField.TabIndex = 7
        '
        'frmUpdateField
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SeaShell
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(373, 114)
        Me.ControlBox = False
        Me.Controls.Add(Me.cboField)
        Me.Controls.Add(Me.chkField)
        Me.Controls.Add(Me.dtpField)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtField)
        Me.Controls.Add(Me.lblPrompt)
        Me.Controls.Add(Me.lstField)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdateField"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "UpdateField"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPrompt As System.Windows.Forms.Label
  Friend WithEvents txtField As System.Windows.Forms.TextBox
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnOK As System.Windows.Forms.Button
  Friend WithEvents dtpField As System.Windows.Forms.DateTimePicker
  Friend WithEvents chkField As System.Windows.Forms.CheckBox
  Friend WithEvents cboField As System.Windows.Forms.ComboBox
  Friend WithEvents lstField As System.Windows.Forms.ListBox
End Class
