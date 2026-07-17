<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _  
Partial Class frmMain  
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
    Me.components = New System.ComponentModel.Container() 
    Me.pnlControl = New System.Windows.Forms.Panel() 
    Me.pnlMessage = New System.Windows.Forms.Panel() 
    Me.lblMessage = New System.Windows.Forms.Label() 
    Me.btnMails = New System.Windows.Forms.Button() 
    Me.tabMain = New System.Windows.Forms.TabControl() 
    Me.cmsTabMenu = New System.Windows.Forms.ContextMenuStrip(Me.components) 
    Me.tsmiPopOut = New System.Windows.Forms.ToolStripMenuItem() 
    Me.tsmiReturnToTab = New System.Windows.Forms.ToolStripMenuItem() 
    Me.tsmiCloseTab = New System.Windows.Forms.ToolStripMenuItem() 
    Me.spcMain = New System.Windows.Forms.SplitContainer() 
    Me.pnlTop = New System.Windows.Forms.Panel() 
    Me.pnlBottom = New System.Windows.Forms.Panel() 
    Me.lblDev = New System.Windows.Forms.Label() 
    Me.pnlBottom.SuspendLayout() 
    CType(Me.spcMain, System.ComponentModel.ISupportInitialize).BeginInit() 
    Me.spcMain.Panel1.SuspendLayout() 
    Me.spcMain.Panel2.SuspendLayout() 
    Me.spcMain.SuspendLayout() 
    Me.pnlCover = New System.Windows.Forms.Panel() 
    Me.pnlMessage.SuspendLayout() 
    Me.pnlControl.SuspendLayout() 
    Me.SuspendLayout() 
    ' 
    'pnlControl 
    ' 
    Me.pnlControl.BackColor = System.Drawing.Color.SeaShell 
    Me.pnlControl.Controls.Add(Me.tabMain) 
    Me.pnlControl.Controls.Add(Me.pnlCover) 
    Me.pnlControl.Controls.Add(Me.pnlMessage) 
    Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.pnlControl.Location = New System.Drawing.Point(0, 0) 
    Me.pnlControl.Name = "pnlControl" 
    Me.pnlControl.Size = New System.Drawing.Size(774, 621) 
    Me.pnlControl.TabIndex = 11 
    ' 
    'pnlMessage 
    ' 
    Me.pnlMessage.AutoScroll = True 
    Me.pnlMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    Me.pnlMessage.Controls.Add(Me.lblMessage) 
    Me.pnlMessage.Dock = System.Windows.Forms.DockStyle.Bottom 
    Me.pnlMessage.Location = New System.Drawing.Point(0, 599) 
    Me.pnlMessage.Name = "pnlMessage" 
    Me.pnlMessage.Size = New System.Drawing.Size(774, 22) 
    Me.pnlMessage.TabIndex = 12 
    ' 
    'lblMessage 
    ' 
    Me.lblMessage.BackColor = System.Drawing.Color.Wheat 
    Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.lblMessage.Location = New System.Drawing.Point(0, 0) 
    Me.lblMessage.Name = "lblMessage" 
    Me.lblMessage.Size = New System.Drawing.Size(770, 18) 
    Me.lblMessage.TabIndex = 0 
    Me.lblMessage.Text = "Put text here or make invisible, both in prt" 
    ' 
    'spcMain 
    ' 
    Me.spcMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle 
    Me.spcMain.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.spcMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1 
    Me.spcMain.Location = New System.Drawing.Point(0, 0) 
    Me.spcMain.BackColor = System.Drawing.Color.White 
    Me.spcMain.SplitterWidth = 4 
    Me.spcMain.Name = "spcMain" 
    ' 
    'spcMain.Panel1 
    ' 
    Me.spcMain.Panel1.Controls.Add(Me.btnMails) 
    Me.spcMain.Panel1.Controls.Add(Me.pnlTop) 
    Me.spcMain.Panel1.Controls.Add(Me.pnlBottom) 
    Me.spcMain.Panel1MinSize = 0 
    Me.spcMain.Panel1.BackColor = System.Drawing.Color.White 
    ' 
    'spcMain.Panel2 
    ' 
    Me.spcMain.Panel2.Controls.Add(Me.pnlControl) 
    Me.spcMain.Size = New System.Drawing.Size(917, 623) 
    Me.spcMain.SplitterDistance = 137 
    Me.spcMain.TabIndex = 12 
    ' 
    'btnMails 
    ' 
    Me.btnMails.BackColor = System.Drawing.Color.White 
    Me.btnMails.Cursor = System.Windows.Forms.Cursors.Hand 
    Me.btnMails.Dock = System.Windows.Forms.DockStyle.Top 
    Me.btnMails.FlatAppearance.BorderColor = System.Drawing.Color.White 
    Me.btnMails.FlatStyle = System.Windows.Forms.FlatStyle.Flat 
    Me.btnMails.Location = New System.Drawing.Point(0, 20) 
    Me.btnMails.Name = "btnMails" 
    Me.btnMails.Size = New System.Drawing.Size(135, 27) 
    Me.btnMails.TabIndex = 13 
    Me.btnMails.Text = "Mails" 
    Me.btnMails.UseVisualStyleBackColor = False 
    ' 
    ' 
    'pnlTop 
    ' 
    Me.pnlTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink 
    Me.pnlTop.BackColor = System.Drawing.Color.White 
    Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top 
    Me.pnlTop.Location = New System.Drawing.Point(0, 0) 
    Me.pnlTop.Name = "pnlTop" 
    Me.pnlTop.Size = New System.Drawing.Size(135, 20) 
    Me.pnlTop.TabIndex = 12 
    Me.pnlTop.Visible = False 
    ' 
    'pnlBottom 
    ' 
    Me.pnlBottom.AutoSize = False 
    Me.pnlBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink 
    Me.pnlBottom.Controls.Add(Me.lblDev) 
    Me.pnlBottom.BackColor = System.Drawing.Color.White 
    Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom 
    Me.pnlBottom.Location = New System.Drawing.Point(0, 599) 
    Me.pnlBottom.Name = "pnlBottom" 
    Me.pnlBottom.Size = New System.Drawing.Size(135, 22) 
    Me.pnlBottom.TabIndex = 11 
    ' 
    'lblDev 
    ' 
    Me.lblDev.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.lblDev.Location = New System.Drawing.Point(0, 0) 
    Me.lblDev.Name = "lblDev" 
    Me.lblDev.Size = New System.Drawing.Size(131, 18) 
    Me.lblDev.TabIndex = 0 
    Me.lblDev.Text = "Development" 
    Me.lblDev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter 
    ' 
    'tabMain 
    ' 
    Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.tabMain.Location = New System.Drawing.Point(0, 0) 
    Me.tabMain.Name = "tabMain" 
    Me.tabMain.SelectedIndex = 0 
    Me.tabMain.Size = New System.Drawing.Size(774, 599) 
    Me.tabMain.TabIndex = 14 
    ' 
    'cmsTabMenu 
    ' 
    Me.cmsTabMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiPopOut, Me.tsmiReturnToTab, Me.tsmiCloseTab}) 
    Me.cmsTabMenu.Name = "cmsTabMenu" 
    Me.cmsTabMenu.Size = New System.Drawing.Size(145, 70) 
    ' 
    'tsmiPopOut 
    ' 
    Me.tsmiPopOut.Name = "tsmiPopOut" 
    Me.tsmiPopOut.Size = New System.Drawing.Size(144, 22) 
    Me.tsmiPopOut.Text = "Pop Out" 
    ' 
    'tsmiReturnToTab 
    ' 
    Me.tsmiReturnToTab.Name = "tsmiReturnToTab" 
    Me.tsmiReturnToTab.Size = New System.Drawing.Size(144, 22) 
    Me.tsmiReturnToTab.Text = "Return to Tab" 
    ' 
    'tsmiCloseTab 
    ' 
    Me.tsmiCloseTab.Name = "tsmiCloseTab" 
    Me.tsmiCloseTab.Size = New System.Drawing.Size(144, 22) 
    Me.tsmiCloseTab.Text = "Close Tab" 
    ' 
    'pnlCover 
    ' 
    Me.pnlCover.Dock = System.Windows.Forms.DockStyle.Fill 
    Me.pnlCover.Location = New System.Drawing.Point(0, 0) 
    Me.pnlCover.Name = "pnlCover" 
    Me.pnlCover.Size = New System.Drawing.Size(774, 599) 
    Me.pnlCover.TabIndex = 13 
    ' 
    'frmMain 
    ' 
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!) 
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True 
    Me.AutoSize = True 
    Me.BackColor = System.Drawing.Color.SeaShell 
    Me.ClientSize = New System.Drawing.Size(917, 623) 
    Me.Controls.Add(Me.spcMain) 
    Me.DoubleBuffered = True   
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "frmMain" 
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen 
    Me.Text = "Main" 
    Me.pnlControl.ResumeLayout(False) 
    Me.pnlMessage.ResumeLayout(False) 
    Me.spcMain.Panel1.ResumeLayout(False) 
    Me.spcMain.Panel1.PerformLayout() 
    Me.spcMain.Panel2.ResumeLayout(False) 
    CType(Me.spcMain, System.ComponentModel.ISupportInitialize).EndInit() 
    Me.spcMain.ResumeLayout(False) 
    Me.pnlBottom.ResumeLayout(False) 
    Me.ResumeLayout(False) 
 
  End Sub 
 
  Friend WithEvents pnlControl As System.Windows.Forms.Panel 
  Friend WithEvents pnlMessage As Panel 
  Friend WithEvents lblMessage As Label 
  Friend WithEvents pnlTop As Panel 
  Friend WithEvents lblDev As Label 
  Friend WithEvents spcMain As System.Windows.Forms.SplitContainer 
  Friend WithEvents pnlCover As System.Windows.Forms.Panel  
  Friend WithEvents pnlBottom As System.Windows.Forms.Panel  
  Friend WithEvents btnMails As Button  
  Friend WithEvents tabMain As System.Windows.Forms.TabControl 
  Friend WithEvents cmsTabMenu As System.Windows.Forms.ContextMenuStrip 
  Friend WithEvents tsmiPopOut As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents tsmiReturnToTab As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents tsmiCloseTab As System.Windows.Forms.ToolStripMenuItem 
End Class 
