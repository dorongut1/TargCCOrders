<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMessageOrInputBox
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
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
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnYes = New System.Windows.Forms.Button()
    Me.txtInput = New System.Windows.Forms.TextBox()
    Me.pbIcon = New System.Windows.Forms.PictureBox()
    Me.btnNo = New System.Windows.Forms.Button()
    Me.lblMessage = New System.Windows.Forms.Label()
    Me.pnlButtons = New System.Windows.Forms.Panel()
    Me.pnlMessage = New System.Windows.Forms.Panel()
    Me.pnlInput = New System.Windows.Forms.Panel()
    Me.btnCopy = New System.Windows.Forms.Button()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.pnlCopy = New System.Windows.Forms.Panel()
        CType(Me.pbIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButtons.SuspendLayout()
        Me.pnlMessage.SuspendLayout()
        Me.pnlInput.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCopy.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(10, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 26)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnYes
        '
        Me.btnYes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnYes.Location = New System.Drawing.Point(342, 3)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(75, 26)
        Me.btnYes.TabIndex = 1
        Me.btnYes.Text = "Yes"
        Me.btnYes.UseVisualStyleBackColor = True
        '
        'txtInput
        '
        Me.txtInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInput.Location = New System.Drawing.Point(0, 0)
        Me.txtInput.Multiline = True
        Me.txtInput.Name = "txtInput"
        Me.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInput.Size = New System.Drawing.Size(321, 36)
        Me.txtInput.TabIndex = 2
        Me.txtInput.Text = "txtInput"
        '
        'pbIcon
        '
        Me.pbIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pbIcon.Dock = System.Windows.Forms.DockStyle.Left
        Me.pbIcon.Location = New System.Drawing.Point(10, 10)
        Me.pbIcon.MaximumSize = New System.Drawing.Size(32, 32)
        Me.pbIcon.MinimumSize = New System.Drawing.Size(32, 32)
        Me.pbIcon.Name = "pbIcon"
        Me.pbIcon.Size = New System.Drawing.Size(32, 32)
        Me.pbIcon.TabIndex = 3
        Me.pbIcon.TabStop = False
        '
        'btnNo
        '
        Me.btnNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNo.Location = New System.Drawing.Point(261, 3)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(75, 26)
        Me.btnNo.TabIndex = 4
        Me.btnNo.Text = "No"
        Me.btnNo.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(5, 8)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(82, 19)
        Me.lblMessage.TabIndex = 5
        Me.lblMessage.Text = "pnlMessage"
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlButtons.Controls.Add(Me.btnCancel)
        Me.pnlButtons.Controls.Add(Me.btnNo)
        Me.pnlButtons.Controls.Add(Me.btnYes)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Location = New System.Drawing.Point(0, 207)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(429, 30)
        Me.pnlButtons.TabIndex = 6
        '
        'pnlMessage
        '
        Me.pnlMessage.AutoScroll = True
        Me.pnlMessage.AutoSize = True
        Me.pnlMessage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMessage.Controls.Add(Me.lblMessage)
        Me.pnlMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMessage.Location = New System.Drawing.Point(52, 0)
        Me.pnlMessage.MinimumSize = New System.Drawing.Size(0, 45)
        Me.pnlMessage.Name = "pnlMessage"
        Me.pnlMessage.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlMessage.Size = New System.Drawing.Size(321, 171)
        Me.pnlMessage.TabIndex = 7
        '
        'pnlInput
        '
        Me.pnlInput.Controls.Add(Me.txtInput)
        Me.pnlInput.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlInput.Location = New System.Drawing.Point(52, 171)
        Me.pnlInput.Name = "pnlInput"
        Me.pnlInput.Size = New System.Drawing.Size(321, 36)
        Me.pnlInput.TabIndex = 8
        '
        'btnCopy
        '
        Me.btnCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCopy.AutoSize = True
        Me.btnCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCopy.Location = New System.Drawing.Point(15, 174)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(28, 29)
        Me.btnCopy.TabIndex = 5
        Me.btnCopy.Text = "C"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.pbIcon)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel2.Size = New System.Drawing.Size(52, 207)
        Me.Panel2.TabIndex = 9
        '
        'pnlCopy
        '
        Me.pnlCopy.AutoSize = True
        Me.pnlCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlCopy.Controls.Add(Me.btnCopy)
        Me.pnlCopy.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlCopy.Location = New System.Drawing.Point(373, 0)
        Me.pnlCopy.Name = "pnlCopy"
        Me.pnlCopy.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlCopy.Size = New System.Drawing.Size(56, 207)
        Me.pnlCopy.TabIndex = 10
        '
        'frmMessageOrInputBox
        '
        Me.AcceptButton = Me.btnYes
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(429, 237)
        Me.Controls.Add(Me.pnlMessage)
        Me.Controls.Add(Me.pnlInput)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlCopy)
        Me.Controls.Add(Me.pnlButtons)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMessageOrInputBox"
        Me.ShowIcon = False
        Me.Text = "frmMessageOrInputBox"
        CType(Me.pbIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlMessage.ResumeLayout(False)
        Me.pnlMessage.PerformLayout()
        Me.pnlInput.ResumeLayout(False)
        Me.pnlInput.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.pnlCopy.ResumeLayout(False)
        Me.pnlCopy.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCancel As Button
  Friend WithEvents btnYes As Button
  Friend WithEvents txtInput As TextBox
  Friend WithEvents pbIcon As PictureBox
  Friend WithEvents btnNo As Button
  Friend WithEvents lblMessage As Label
  Friend WithEvents pnlButtons As Panel
  Friend WithEvents pnlMessage As Panel
  Friend WithEvents pnlInput As Panel
  Friend WithEvents btnCopy As Button
  Friend WithEvents Panel2 As Panel
  Friend WithEvents pnlCopy As Panel
End Class
