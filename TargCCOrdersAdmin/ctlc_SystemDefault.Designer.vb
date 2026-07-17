'Me.BackColor = System.Drawing.XXX --> System.Drawing.Color.Wheat

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlc_SystemDefault
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
        Me.txtGroup = New System.Windows.Forms.TextBox()
        Me.lblGroup = New System.Windows.Forms.Label()
        Me.txtSettingName = New System.Windows.Forms.TextBox()
        Me.lblSettingName = New System.Windows.Forms.Label()
        Me.txtSettingValue = New System.Windows.Forms.TextBox()
        Me.btnSettingValueUpdate = New System.Windows.Forms.Button()
        Me.lblSettingValue = New System.Windows.Forms.Label()
        Me.cboSystemDefaultType = New System.Windows.Forms.ComboBox()
        Me.txtSystemDefaultType = New System.Windows.Forms.TextBox()
        Me.lblSystemDefaultType = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.gpbWhichSetting = New System.Windows.Forms.GroupBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.gpbWhichSetting.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtID
        '
        Me.txtID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtID.Location = New System.Drawing.Point(618, 543)
        Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(90, 25)
        Me.txtID.TabIndex = 0
        Me.txtID.Text = "txtID"
        '
        'lblID
        '
        Me.lblID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(577, 546)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(23, 19)
        Me.lblID.TabIndex = 1
        Me.lblID.Text = "ID"
        '
        'txtGroup
        '
        Me.txtGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGroup.Location = New System.Drawing.Point(214, 36)
        Me.txtGroup.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtGroup.Name = "txtGroup"
        Me.txtGroup.Size = New System.Drawing.Size(506, 25)
        Me.txtGroup.TabIndex = 2
        Me.txtGroup.Text = "txtGroup"
        '
        'lblGroup
        '
        Me.lblGroup.AutoSize = True
        Me.lblGroup.Location = New System.Drawing.Point(19, 36)
        Me.lblGroup.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(48, 19)
        Me.lblGroup.TabIndex = 3
        Me.lblGroup.Text = "Group"
        '
        'txtSettingName
        '
        Me.txtSettingName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSettingName.Location = New System.Drawing.Point(214, 76)
        Me.txtSettingName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtSettingName.Name = "txtSettingName"
        Me.txtSettingName.Size = New System.Drawing.Size(506, 25)
        Me.txtSettingName.TabIndex = 4
        Me.txtSettingName.Text = "txtSettingName"
        '
        'lblSettingName
        '
        Me.lblSettingName.AutoSize = True
        Me.lblSettingName.Location = New System.Drawing.Point(19, 79)
        Me.lblSettingName.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblSettingName.Name = "lblSettingName"
        Me.lblSettingName.Size = New System.Drawing.Size(92, 19)
        Me.lblSettingName.TabIndex = 5
        Me.lblSettingName.Text = "Setting Name"
        '
        'txtSettingValue
        '
        Me.txtSettingValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSettingValue.Location = New System.Drawing.Point(149, 53)
        Me.txtSettingValue.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtSettingValue.Multiline = True
        Me.txtSettingValue.Name = "txtSettingValue"
        Me.txtSettingValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSettingValue.Size = New System.Drawing.Size(469, 90)
        Me.txtSettingValue.TabIndex = 6
        Me.txtSettingValue.Text = "txtSettingValue"
        '
        'btnSettingValueUpdate
        '
        Me.btnSettingValueUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSettingValueUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSettingValueUpdate.Location = New System.Drawing.Point(648, 53)
        Me.btnSettingValueUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.btnSettingValueUpdate.Name = "btnSettingValueUpdate"
        Me.btnSettingValueUpdate.Size = New System.Drawing.Size(75, 26)
        Me.btnSettingValueUpdate.TabIndex = 7
        Me.btnSettingValueUpdate.Text = "Update"
        Me.btnSettingValueUpdate.UseVisualStyleBackColor = True
        '
        'lblSettingValue
        '
        Me.lblSettingValue.AutoSize = True
        Me.lblSettingValue.Location = New System.Drawing.Point(35, 53)
        Me.lblSettingValue.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblSettingValue.Name = "lblSettingValue"
        Me.lblSettingValue.Size = New System.Drawing.Size(89, 19)
        Me.lblSettingValue.TabIndex = 8
        Me.lblSettingValue.Text = "Setting Value"
        '
        'cboSystemDefaultType
        '
        Me.cboSystemDefaultType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSystemDefaultType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSystemDefaultType.FormattingEnabled = True
        Me.cboSystemDefaultType.Location = New System.Drawing.Point(246, 106)
        Me.cboSystemDefaultType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.cboSystemDefaultType.Name = "cboSystemDefaultType"
        Me.cboSystemDefaultType.Size = New System.Drawing.Size(456, 25)
        Me.cboSystemDefaultType.TabIndex = 9
        '
        'txtSystemDefaultType
        '
        Me.txtSystemDefaultType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSystemDefaultType.Location = New System.Drawing.Point(214, 116)
        Me.txtSystemDefaultType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtSystemDefaultType.Name = "txtSystemDefaultType"
        Me.txtSystemDefaultType.Size = New System.Drawing.Size(506, 25)
        Me.txtSystemDefaultType.TabIndex = 11
        Me.txtSystemDefaultType.Text = "txtSystemDefaultType"
        '
        'lblSystemDefaultType
        '
        Me.lblSystemDefaultType.AutoSize = True
        Me.lblSystemDefaultType.Location = New System.Drawing.Point(19, 119)
        Me.lblSystemDefaultType.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblSystemDefaultType.Name = "lblSystemDefaultType"
        Me.lblSystemDefaultType.Size = New System.Drawing.Size(133, 19)
        Me.lblSystemDefaultType.TabIndex = 12
        Me.lblSystemDefaultType.Text = "System Default Type"
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(214, 156)
        Me.txtDescription.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(506, 129)
        Me.txtDescription.TabIndex = 13
        Me.txtDescription.Text = "txtDescription"
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(19, 156)
        Me.lblDescription.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(78, 19)
        Me.lblDescription.TabIndex = 14
        Me.lblDescription.Text = "Description"
        '
        'btnEdit
        '
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnEdit.Location = New System.Drawing.Point(414, 323)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 26)
        Me.btnEdit.TabIndex = 16
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAdd.Location = New System.Drawing.Point(309, 323)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 26)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(309, 311)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 26)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnUpdate.Location = New System.Drawing.Point(414, 311)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
        Me.btnUpdate.TabIndex = 19
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDelete.Location = New System.Drawing.Point(128, 323)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 26)
        Me.btnDelete.TabIndex = 15
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'gpbWhichSetting
        '
        Me.gpbWhichSetting.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gpbWhichSetting.Controls.Add(Me.txtGroup)
        Me.gpbWhichSetting.Controls.Add(Me.btnUpdate)
        Me.gpbWhichSetting.Controls.Add(Me.lblGroup)
        Me.gpbWhichSetting.Controls.Add(Me.btnCancel)
        Me.gpbWhichSetting.Controls.Add(Me.txtSettingName)
        Me.gpbWhichSetting.Controls.Add(Me.lblSettingName)
        Me.gpbWhichSetting.Controls.Add(Me.btnDelete)
        Me.gpbWhichSetting.Controls.Add(Me.lblDescription)
        Me.gpbWhichSetting.Controls.Add(Me.txtDescription)
        Me.gpbWhichSetting.Controls.Add(Me.lblSystemDefaultType)
        Me.gpbWhichSetting.Controls.Add(Me.btnAdd)
        Me.gpbWhichSetting.Controls.Add(Me.txtSystemDefaultType)
        Me.gpbWhichSetting.Controls.Add(Me.cboSystemDefaultType)
        Me.gpbWhichSetting.Controls.Add(Me.btnEdit)
        Me.gpbWhichSetting.Location = New System.Drawing.Point(19, 159)
        Me.gpbWhichSetting.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.gpbWhichSetting.Name = "gpbWhichSetting"
        Me.gpbWhichSetting.Size = New System.Drawing.Size(738, 366)
        Me.gpbWhichSetting.TabIndex = 0
        Me.gpbWhichSetting.TabStop = False
        Me.gpbWhichSetting.Text = "Setting Definition"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 15.0!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.lblTitle.Location = New System.Drawing.Point(16, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(70, 28)
        Me.lblTitle.TabIndex = 20
        Me.lblTitle.Text = "lblTitle"
        '
        'ctlc_SystemDefault
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Wheat
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.btnSettingValueUpdate)
        Me.Controls.Add(Me.lblSettingValue)
        Me.Controls.Add(Me.txtSettingValue)
        Me.Controls.Add(Me.gpbWhichSetting)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Name = "ctlc_SystemDefault"
        Me.Size = New System.Drawing.Size(774, 591)
        Me.gpbWhichSetting.ResumeLayout(False)
        Me.gpbWhichSetting.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtGroup As System.Windows.Forms.TextBox
    Friend WithEvents lblGroup As System.Windows.Forms.Label
    Friend WithEvents txtSettingName As System.Windows.Forms.TextBox
    Friend WithEvents lblSettingName As System.Windows.Forms.Label
    Friend WithEvents txtSettingValue As System.Windows.Forms.TextBox
    Friend WithEvents btnSettingValueUpdate As System.Windows.Forms.Button
    Friend WithEvents lblSettingValue As System.Windows.Forms.Label
    Friend WithEvents cboSystemDefaultType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSystemDefaultType As System.Windows.Forms.TextBox
    Friend WithEvents lblSystemDefaultType As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents gpbWhichSetting As GroupBox
    Friend WithEvents lblTitle As Label
End Class
