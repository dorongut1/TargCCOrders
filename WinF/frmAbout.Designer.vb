'BackColor = System.Drawing.Color.SeaShell


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
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
    Me.lblCompanyName = New System.Windows.Forms.Label()
    Me.lblCopyright = New System.Windows.Forms.Label()
    Me.lblDescription = New System.Windows.Forms.Label()
    Me.lblProductName = New System.Windows.Forms.Label()
    Me.lblVersion = New System.Windows.Forms.Label()
    Me.pnlMain = New System.Windows.Forms.Panel()
    Me.btnReadMe = New System.Windows.Forms.Button()
    Me.lblImage = New System.Windows.Forms.Label()
    Me.btnOK = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCompanyName
        '
        Me.lblCompanyName.AutoSize = True
        Me.lblCompanyName.BackColor = System.Drawing.Color.Transparent
        Me.lblCompanyName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCompanyName.ForeColor = System.Drawing.Color.White
        Me.lblCompanyName.Location = New System.Drawing.Point(26, 298)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(130, 19)
        Me.lblCompanyName.TabIndex = 1
        Me.lblCompanyName.Text = "lblCompanyName"
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyright.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCopyright.ForeColor = System.Drawing.Color.White
        Me.lblCopyright.Location = New System.Drawing.Point(26, 258)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(93, 19)
        Me.lblCopyright.TabIndex = 1
        Me.lblCopyright.Text = "lblCopyright"
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 17.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.lblDescription.ForeColor = System.Drawing.Color.White
        Me.lblDescription.Location = New System.Drawing.Point(24, 9)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(137, 31)
        Me.lblDescription.TabIndex = 1
        Me.lblDescription.Text = "Description"
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.BackColor = System.Drawing.Color.Transparent
        Me.lblProductName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblProductName.ForeColor = System.Drawing.Color.White
        Me.lblProductName.Location = New System.Drawing.Point(26, 238)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(119, 19)
        Me.lblProductName.TabIndex = 1
        Me.lblProductName.Text = "lblProductName"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblVersion.ForeColor = System.Drawing.Color.White
        Me.lblVersion.Location = New System.Drawing.Point(26, 278)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(75, 19)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "lblVersion"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.SeaShell
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlMain.Controls.Add(Me.btnReadMe)
        Me.pnlMain.Controls.Add(Me.lblImage)
        Me.pnlMain.Controls.Add(Me.btnOK)
        Me.pnlMain.Controls.Add(Me.lblDescription)
        Me.pnlMain.Controls.Add(Me.lblVersion)
        Me.pnlMain.Controls.Add(Me.lblProductName)
        Me.pnlMain.Controls.Add(Me.lblCompanyName)
        Me.pnlMain.Controls.Add(Me.lblCopyright)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(515, 331)
        Me.pnlMain.TabIndex = 2
        '
        'btnReadMe
        '
        Me.btnReadMe.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReadMe.Location = New System.Drawing.Point(413, 253)
        Me.btnReadMe.Name = "btnReadMe"
        Me.btnReadMe.Size = New System.Drawing.Size(75, 27)
        Me.btnReadMe.TabIndex = 4
        Me.btnReadMe.Text = "Read Me"
        Me.btnReadMe.UseVisualStyleBackColor = True
        Me.btnReadMe.Visible = False
        '
        'lblImage
        '
        Me.lblImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblImage.Font = New System.Drawing.Font("Segoe UI", 23.0!)
        Me.lblImage.ForeColor = System.Drawing.Color.White
        Me.lblImage.Location = New System.Drawing.Point(30, 76)
        Me.lblImage.Name = "lblImage"
        Me.lblImage.Size = New System.Drawing.Size(447, 119)
        Me.lblImage.TabIndex = 3
        Me.lblImage.Text = "Put an image  or logo here or as background for whole panel"
        Me.lblImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnOK
        '
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Location = New System.Drawing.Point(413, 288)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 27)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        Me.btnOK.Visible = False
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(515, 331)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlMain)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAbout"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
  Friend WithEvents lblCopyright As System.Windows.Forms.Label
  Friend WithEvents lblDescription As System.Windows.Forms.Label
  Friend WithEvents lblProductName As System.Windows.Forms.Label
  Friend WithEvents lblVersion As System.Windows.Forms.Label
  Friend WithEvents pnlMain As System.Windows.Forms.Panel
  Friend WithEvents btnOK As System.Windows.Forms.Button
  Friend WithEvents lblImage As System.Windows.Forms.Label
  Friend WithEvents btnReadMe As System.Windows.Forms.Button
End Class
