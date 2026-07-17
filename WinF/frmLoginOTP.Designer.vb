<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> 
Partial Class frmLoginOTP 
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
        Me.lblUserName = New System.Windows.Forms.Label() 
        Me.lblEmail = New System.Windows.Forms.Label() 
        Me.pctLogo = New System.Windows.Forms.PictureBox() 
        Me.txtUserName = New System.Windows.Forms.TextBox() 
        Me.txtEmail = New System.Windows.Forms.MaskedTextBox() 
        Me.btnCancel = New System.Windows.Forms.Button() 
        Me.btnOK = New System.Windows.Forms.Button() 
        Me.lblLogo = New System.Windows.Forms.Label() 
        Me.lblVersion = New System.Windows.Forms.Label() 
        CType(Me.pctLogo, System.ComponentModel.ISupportInitialize).BeginInit() 
        Me.SuspendLayout() 
        ' 
        'lblUserName 
        ' 
        Me.lblUserName.AutoSize = True 
        Me.lblUserName.Location = New System.Drawing.Point(17, 101) 
        Me.lblUserName.Name = "lblUserName" 
        Me.lblUserName.Size = New System.Drawing.Size(77, 19) 
        Me.lblUserName.TabIndex = 0 
        Me.lblUserName.Text = "User Name" 
        ' 
        'lblEmail 
        ' 
        Me.lblEmail.AutoSize = True 
        Me.lblEmail.Location = New System.Drawing.Point(18, 135) 
        Me.lblEmail.Name = "lblEmail" 
        Me.lblEmail.Size = New System.Drawing.Size(41, 19) 
        Me.lblEmail.TabIndex = 1 
        Me.lblEmail.Text = "Email" 
        ' 
        'pctLogo 
        ' 
        Me.pctLogo.BackColor = System.Drawing.Color.Teal 
        Me.pctLogo.Location = New System.Drawing.Point(12, 12) 
        Me.pctLogo.Name = "pctLogo" 
        Me.pctLogo.Size = New System.Drawing.Size(170, 47) 
        Me.pctLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize 
        Me.pctLogo.TabIndex = 2 
        Me.pctLogo.TabStop = False 
        ' 
        'txtUserName 
        ' 
        Me.txtUserName.Location = New System.Drawing.Point(126, 98) 
        Me.txtUserName.Name = "txtUserName" 
        Me.txtUserName.Size = New System.Drawing.Size(169, 25) 
        Me.txtUserName.TabIndex = 0 
        Me.txtUserName.Text = "txtUserName" 
        ' 
        'txtEmail 
        ' 
        Me.txtEmail.Location = New System.Drawing.Point(126, 133) 
        Me.txtEmail.Name = "txtEmail" 
        Me.txtEmail.Size = New System.Drawing.Size(169, 25) 
        Me.txtEmail.TabIndex = 1 
        Me.txtEmail.Text = "txtEmail" 
        ' 
        'btnCancel 
        ' 
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel 
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
        Me.btnCancel.Location = New System.Drawing.Point(399, 132) 
        Me.btnCancel.Name = "btnCancel" 
        Me.btnCancel.Size = New System.Drawing.Size(75, 26) 
        Me.btnCancel.TabIndex = 3 
        Me.btnCancel.Text = "Cancel" 
        Me.btnCancel.UseVisualStyleBackColor = True 
        ' 
        'btnOK 
        ' 
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
        Me.btnOK.Location = New System.Drawing.Point(399, 100) 
        Me.btnOK.Name = "btnOK" 
        Me.btnOK.Size = New System.Drawing.Size(75, 26) 
        Me.btnOK.TabIndex = 2 
        Me.btnOK.Text = "OK" 
        Me.btnOK.UseVisualStyleBackColor = True 
        ' 
        'lblLogo 
        ' 
        Me.lblLogo.Font = New System.Drawing.Font("Segoe UI", 21.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(177, Byte)) 
        Me.lblLogo.Location = New System.Drawing.Point(188, 12) 
        Me.lblLogo.Name = "lblLogo" 
        Me.lblLogo.Size = New System.Drawing.Size(286, 79) 
        Me.lblLogo.TabIndex = 7 
        Me.lblLogo.Text = "TargCCOrders" 
        Me.lblLogo.TextAlign = System.Drawing.ContentAlignment.TopCenter 
        ' 
        'lblVersion 
        ' 
        Me.lblVersion.AutoSize = True 
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent 
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 7.0!) 
        Me.lblVersion.ForeColor = System.Drawing.Color.Black 
        Me.lblVersion.Location = New System.Drawing.Point(3, 166) 
        Me.lblVersion.Name = "lblVersion" 
        Me.lblVersion.Size = New System.Drawing.Size(46, 12) 
        Me.lblVersion.TabIndex = 8 
        Me.lblVersion.Text = "lblVersion" 
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight 
        ' 
        'frmLoginOTP 
        ' 
        Me.AcceptButton = Me.btnOK 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!) 
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
        Me.BackColor = System.Drawing.Color.Wheat 
        Me.CancelButton = Me.btnCancel 
        Me.ClientSize = New System.Drawing.Size(497, 179) 
        Me.ControlBox = False 
        Me.Controls.Add(Me.lblVersion) 
        Me.Controls.Add(Me.lblLogo) 
        Me.Controls.Add(Me.btnOK) 
        Me.Controls.Add(Me.btnCancel) 
        Me.Controls.Add(Me.txtEmail) 
        Me.Controls.Add(Me.txtUserName) 
        Me.Controls.Add(Me.pctLogo) 
        Me.Controls.Add(Me.lblEmail) 
        Me.Controls.Add(Me.lblUserName) 
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!) 
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog 
        Me.MaximizeBox = False 
        Me.Name = "frmLoginOTP" 
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent 
        Me.Text = "Login via OTP" 
        CType(Me.pctLogo, System.ComponentModel.ISupportInitialize).EndInit() 
        Me.ResumeLayout(False) 
        Me.PerformLayout() 
 
    End Sub 
    Friend WithEvents lblUserName As System.Windows.Forms.Label 
    Friend WithEvents lblEmail As System.Windows.Forms.Label 
    Friend WithEvents pctLogo As System.Windows.Forms.PictureBox 
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox 
    Friend WithEvents txtEmail As System.Windows.Forms.MaskedTextBox 
    Friend WithEvents btnCancel As System.Windows.Forms.Button 
    Friend WithEvents btnOK As System.Windows.Forms.Button 
    Friend WithEvents lblLogo As System.Windows.Forms.Label 
    Friend WithEvents lblVersion As Label 
End Class 
