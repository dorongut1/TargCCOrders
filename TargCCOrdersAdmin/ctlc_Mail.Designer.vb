'Look for Drawing.Color.ColourObjectBackground

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlc_Mail
  Inherits System.Windows.Forms.UserControl

  'UserControl overrides dispose to clean up the component list.
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
    Me.txtID = New System.Windows.Forms.TextBox()
    Me.lblID = New System.Windows.Forms.Label()
    Me.cboMessagingMode = New System.Windows.Forms.ComboBox()
    Me.txtMessagingMode = New System.Windows.Forms.TextBox()
    Me.lblMessagingMode = New System.Windows.Forms.Label()
    Me.txtRecipientEmail = New System.Windows.Forms.TextBox()
    Me.lblRecipientEmail = New System.Windows.Forms.Label()
    Me.dtpWhenSent = New System.Windows.Forms.DateTimePicker()
    Me.txtWhenSent = New System.Windows.Forms.TextBox()
    Me.lblWhenSent = New System.Windows.Forms.Label()
    Me.txtSubject = New System.Windows.Forms.TextBox()
    Me.lblSubject = New System.Windows.Forms.Label()
    Me.txtBody = New System.Windows.Forms.TextBox()
    Me.lblBody = New System.Windows.Forms.Label()
    Me.dtpWhenSeen = New System.Windows.Forms.DateTimePicker()
    Me.txtWhenSeen = New System.Windows.Forms.TextBox()
    Me.lblWhenSeen = New System.Windows.Forms.Label()
    Me.chkWasSeen = New System.Windows.Forms.CheckBox()
    Me.lblWasSeen = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.tlp1 = New System.Windows.Forms.TableLayoutPanel()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.pnlGarbage = New System.Windows.Forms.Panel()
    Me.btnView = New System.Windows.Forms.Button()
        Me.tlp1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlGarbage.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtID
        '
        Me.txtID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtID.Location = New System.Drawing.Point(618, 543)
        Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(90, 25)
        Me.txtID.TabIndex = 0
        Me.txtID.Text = "txtID"
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(570, 546)
        Me.lblID.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(23, 19)
        Me.lblID.TabIndex = 1
        Me.lblID.Text = "ID"
        '
        'cboMessagingMode
        '
        Me.cboMessagingMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboMessagingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMessagingMode.FormattingEnabled = True
        Me.cboMessagingMode.Location = New System.Drawing.Point(27, 32)
        Me.cboMessagingMode.Name = "cboMessagingMode"
        Me.cboMessagingMode.Size = New System.Drawing.Size(77, 25)
        Me.cboMessagingMode.TabIndex = 2
        '
        'txtMessagingMode
        '
        Me.txtMessagingMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessagingMode.Location = New System.Drawing.Point(153, 115)
        Me.txtMessagingMode.Margin = New System.Windows.Forms.Padding(15, 30, 15, 0)
        Me.txtMessagingMode.Name = "txtMessagingMode"
        Me.txtMessagingMode.Size = New System.Drawing.Size(200, 25)
        Me.txtMessagingMode.TabIndex = 4
        Me.txtMessagingMode.Text = "txtMessagingMode"
        '
        'lblMessagingMode
        '
        Me.lblMessagingMode.AutoSize = True
        Me.lblMessagingMode.Location = New System.Drawing.Point(13, 118)
        Me.lblMessagingMode.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblMessagingMode.Name = "lblMessagingMode"
        Me.lblMessagingMode.Size = New System.Drawing.Size(115, 19)
        Me.lblMessagingMode.TabIndex = 5
        Me.lblMessagingMode.Text = "Messaging Mode"
        '
        'txtRecipientEmail
        '
        Me.txtRecipientEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRecipientEmail.Location = New System.Drawing.Point(153, 155)
        Me.txtRecipientEmail.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtRecipientEmail.Name = "txtRecipientEmail"
        Me.txtRecipientEmail.Size = New System.Drawing.Size(200, 25)
        Me.txtRecipientEmail.TabIndex = 6
        Me.txtRecipientEmail.Text = "txtRecipientEmail"
        '
        'lblRecipientEmail
        '
        Me.lblRecipientEmail.AutoSize = True
        Me.lblRecipientEmail.Location = New System.Drawing.Point(13, 158)
        Me.lblRecipientEmail.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblRecipientEmail.Name = "lblRecipientEmail"
        Me.lblRecipientEmail.Size = New System.Drawing.Size(100, 19)
        Me.lblRecipientEmail.TabIndex = 7
        Me.lblRecipientEmail.Text = "Recipient Email"
        '
        'dtpWhenSent
        '
        Me.dtpWhenSent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpWhenSent.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpWhenSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWhenSent.Location = New System.Drawing.Point(39, 56)
        Me.dtpWhenSent.Name = "dtpWhenSent"
        Me.dtpWhenSent.ShowCheckBox = True
        Me.dtpWhenSent.ShowUpDown = True
        Me.dtpWhenSent.Size = New System.Drawing.Size(77, 25)
        Me.dtpWhenSent.TabIndex = 8
        '
        'txtWhenSent
        '
        Me.txtWhenSent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWhenSent.Location = New System.Drawing.Point(153, 195)
        Me.txtWhenSent.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtWhenSent.Name = "txtWhenSent"
        Me.txtWhenSent.Size = New System.Drawing.Size(200, 25)
        Me.txtWhenSent.TabIndex = 9
        Me.txtWhenSent.Text = "txtWhenSent"
        '
        'lblWhenSent
        '
        Me.lblWhenSent.AutoSize = True
        Me.lblWhenSent.Location = New System.Drawing.Point(13, 198)
        Me.lblWhenSent.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblWhenSent.Name = "lblWhenSent"
        Me.lblWhenSent.Size = New System.Drawing.Size(76, 19)
        Me.lblWhenSent.TabIndex = 10
        Me.lblWhenSent.Text = "When Sent"
        '
        'txtSubject
        '
        Me.txtSubject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSubject.Location = New System.Drawing.Point(31, 60)
        Me.txtSubject.Margin = New System.Windows.Forms.Padding(15, 10, 15, 0)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(322, 25)
        Me.txtSubject.TabIndex = 11
        Me.txtSubject.Text = "txtSubject"
        '
        'lblSubject
        '
        Me.lblSubject.AutoSize = True
        Me.lblSubject.Location = New System.Drawing.Point(13, 31)
        Me.lblSubject.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(53, 19)
        Me.lblSubject.TabIndex = 12
        Me.lblSubject.Text = "Subject"
        '
        'txtBody
        '
        Me.txtBody.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBody.Location = New System.Drawing.Point(18, 36)
        Me.txtBody.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtBody.Multiline = True
        Me.txtBody.Name = "txtBody"
        Me.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBody.Size = New System.Drawing.Size(335, 457)
        Me.txtBody.TabIndex = 13
        Me.txtBody.Text = "txtBody"
        '
        'lblBody
        '
        Me.lblBody.AutoSize = True
        Me.lblBody.Location = New System.Drawing.Point(146, 63)
        Me.lblBody.Name = "lblBody"
        Me.lblBody.Size = New System.Drawing.Size(40, 19)
        Me.lblBody.TabIndex = 14
        Me.lblBody.Text = "Body"
        '
        'dtpWhenSeen
        '
        Me.dtpWhenSeen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpWhenSeen.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpWhenSeen.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWhenSeen.Location = New System.Drawing.Point(38, 9)
        Me.dtpWhenSeen.Name = "dtpWhenSeen"
        Me.dtpWhenSeen.ShowCheckBox = True
        Me.dtpWhenSeen.ShowUpDown = True
        Me.dtpWhenSeen.Size = New System.Drawing.Size(78, 25)
        Me.dtpWhenSeen.TabIndex = 15
        '
        'txtWhenSeen
        '
        Me.txtWhenSeen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWhenSeen.Location = New System.Drawing.Point(153, 235)
        Me.txtWhenSeen.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtWhenSeen.Name = "txtWhenSeen"
        Me.txtWhenSeen.Size = New System.Drawing.Size(200, 25)
        Me.txtWhenSeen.TabIndex = 16
        Me.txtWhenSeen.Text = "txtWhenSeen"
        '
        'lblWhenSeen
        '
        Me.lblWhenSeen.AutoSize = True
        Me.lblWhenSeen.Location = New System.Drawing.Point(13, 238)
        Me.lblWhenSeen.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblWhenSeen.Name = "lblWhenSeen"
        Me.lblWhenSeen.Size = New System.Drawing.Size(78, 19)
        Me.lblWhenSeen.TabIndex = 17
        Me.lblWhenSeen.Text = "When Seen"
        '
        'chkWasSeen
        '
        Me.chkWasSeen.AutoSize = True
        Me.chkWasSeen.Location = New System.Drawing.Point(189, 275)
        Me.chkWasSeen.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.chkWasSeen.Name = "chkWasSeen"
        Me.chkWasSeen.Size = New System.Drawing.Size(15, 14)
        Me.chkWasSeen.TabIndex = 18
        Me.chkWasSeen.UseVisualStyleBackColor = True
        '
        'lblWasSeen
        '
        Me.lblWasSeen.AutoSize = True
        Me.lblWasSeen.Location = New System.Drawing.Point(13, 273)
        Me.lblWasSeen.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblWasSeen.Name = "lblWasSeen"
        Me.lblWasSeen.Size = New System.Drawing.Size(67, 19)
        Me.lblWasSeen.TabIndex = 19
        Me.lblWasSeen.Text = "Was Seen"
        '
        'btnEdit
        '
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnEdit.Location = New System.Drawing.Point(110, 27)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 26)
        Me.btnEdit.TabIndex = 21
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(29, 15)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 26)
        Me.btnCancel.TabIndex = 22
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnUpdate.Location = New System.Drawing.Point(110, 15)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
        Me.btnUpdate.TabIndex = 23
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDelete.Location = New System.Drawing.Point(61, 541)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 27)
        Me.btnDelete.TabIndex = 20
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'tlp1
        '
        Me.tlp1.ColumnCount = 2
        Me.tlp1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlp1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlp1.Controls.Add(Me.Panel1, 0, 0)
        Me.tlp1.Controls.Add(Me.Panel2, 1, 0)
        Me.tlp1.Dock = System.Windows.Forms.DockStyle.Top
        Me.tlp1.Location = New System.Drawing.Point(0, 0)
        Me.tlp1.Name = "tlp1"
        Me.tlp1.RowCount = 1
        Me.tlp1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 388.0!))
        Me.tlp1.Size = New System.Drawing.Size(774, 520)
        Me.tlp1.TabIndex = 24
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel1.Size = New System.Drawing.Size(381, 514)
        Me.Panel1.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblWhenSent)
        Me.GroupBox1.Controls.Add(Me.txtWhenSent)
        Me.GroupBox1.Controls.Add(Me.txtMessagingMode)
        Me.GroupBox1.Controls.Add(Me.lblRecipientEmail)
        Me.GroupBox1.Controls.Add(Me.lblSubject)
        Me.GroupBox1.Controls.Add(Me.txtSubject)
        Me.GroupBox1.Controls.Add(Me.txtWhenSeen)
        Me.GroupBox1.Controls.Add(Me.lblWhenSeen)
        Me.GroupBox1.Controls.Add(Me.lblMessagingMode)
        Me.GroupBox1.Controls.Add(Me.chkWasSeen)
        Me.GroupBox1.Controls.Add(Me.txtRecipientEmail)
        Me.GroupBox1.Controls.Add(Me.lblWasSeen)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(371, 504)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "About the Mail"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(390, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel2.Size = New System.Drawing.Size(381, 514)
        Me.Panel2.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtBody)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(371, 504)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "The Message"
        '
        'pnlGarbage
        '
        Me.pnlGarbage.Controls.Add(Me.cboMessagingMode)
        Me.pnlGarbage.Controls.Add(Me.dtpWhenSeen)
        Me.pnlGarbage.Controls.Add(Me.dtpWhenSent)
        Me.pnlGarbage.Controls.Add(Me.lblBody)
        Me.pnlGarbage.Controls.Add(Me.btnEdit)
        Me.pnlGarbage.Controls.Add(Me.btnUpdate)
        Me.pnlGarbage.Controls.Add(Me.btnCancel)
        Me.pnlGarbage.Location = New System.Drawing.Point(12, 448)
        Me.pnlGarbage.Name = "pnlGarbage"
        Me.pnlGarbage.Size = New System.Drawing.Size(200, 100)
        Me.pnlGarbage.TabIndex = 25
        Me.pnlGarbage.Visible = False
        '
        'btnView
        '
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnView.Location = New System.Drawing.Point(314, 541)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(143, 27)
        Me.btnView.TabIndex = 20
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'ctlc_Mail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Wheat
        Me.Controls.Add(Me.tlp1)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.pnlGarbage)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Name = "ctlc_Mail"
        Me.Size = New System.Drawing.Size(774, 591)
        Me.tlp1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pnlGarbage.ResumeLayout(False)
        Me.pnlGarbage.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboMessagingMode As System.Windows.Forms.ComboBox
  Friend WithEvents txtMessagingMode As System.Windows.Forms.TextBox
  Friend WithEvents lblMessagingMode As System.Windows.Forms.Label
  Friend WithEvents txtRecipientEmail As System.Windows.Forms.TextBox
  Friend WithEvents lblRecipientEmail As System.Windows.Forms.Label
  Friend WithEvents dtpWhenSent As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtWhenSent As System.Windows.Forms.TextBox
  Friend WithEvents lblWhenSent As System.Windows.Forms.Label
  Friend WithEvents txtSubject As System.Windows.Forms.TextBox
  Friend WithEvents lblSubject As System.Windows.Forms.Label
  Friend WithEvents txtBody As System.Windows.Forms.TextBox
  Friend WithEvents lblBody As System.Windows.Forms.Label
  Friend WithEvents dtpWhenSeen As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtWhenSeen As System.Windows.Forms.TextBox
  Friend WithEvents lblWhenSeen As System.Windows.Forms.Label
  Friend WithEvents chkWasSeen As System.Windows.Forms.CheckBox
  Friend WithEvents lblWasSeen As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button
  Friend WithEvents tlp1 As TableLayoutPanel
  Friend WithEvents Panel1 As Panel
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Panel2 As Panel
  Friend WithEvents GroupBox2 As GroupBox
  Friend WithEvents pnlGarbage As Panel
  Friend WithEvents btnView As Button
End Class
