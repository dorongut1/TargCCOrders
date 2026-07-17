<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_MFA
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
    Me.txtCellOrEmail = New System.Windows.Forms.TextBox()
    Me.lblCellOrEmail = New System.Windows.Forms.Label()
    Me.txtProtectedFunction = New System.Windows.Forms.TextBox()
    Me.lblProtectedFunction = New System.Windows.Forms.Label()
    Me.txtCodeHashed = New System.Windows.Forms.TextBox()
    Me.lblCodeHashed = New System.Windows.Forms.Label()
    Me.txtAttemptNo = New System.Windows.Forms.TextBox()
    Me.lblAttemptNo = New System.Windows.Forms.Label()
    Me.chkIsSuccessful = New System.Windows.Forms.CheckBox()
    Me.lblIsSuccessful = New System.Windows.Forms.Label()
    Me.txtLastAccessingIP = New System.Windows.Forms.TextBox()
    Me.lblLastAccessingIP = New System.Windows.Forms.Label()
    Me.txtLastAccessingCountry = New System.Windows.Forms.TextBox()
    Me.lblLastAccessingCountry = New System.Windows.Forms.Label()
    Me.cboUILang = New System.Windows.Forms.ComboBox()
    Me.txtUILang = New System.Windows.Forms.TextBox()
    Me.lblUILang = New System.Windows.Forms.Label()
    Me.dtpWhenCreated = New System.Windows.Forms.DateTimePicker()
    Me.txtWhenCreated = New System.Windows.Forms.TextBox()
    Me.lblWhenCreated = New System.Windows.Forms.Label()
    Me.dtpWhenAccessed = New System.Windows.Forms.DateTimePicker()
    Me.txtWhenAccessed = New System.Windows.Forms.TextBox()
    Me.lblWhenAccessed = New System.Windows.Forms.Label()
    Me.dtpWhenExpires = New System.Windows.Forms.DateTimePicker()
    Me.txtWhenExpires = New System.Windows.Forms.TextBox()
    Me.lblWhenExpires = New System.Windows.Forms.Label()
    Me.txtDetails = New System.Windows.Forms.TextBox()
    Me.lblDetails = New System.Windows.Forms.Label()
    Me.cboUser = New IntelliCombo()
    Me.txtUser = New System.Windows.Forms.TextBox()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(211, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(359, 25)
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
    'DtxtCellOrEmail
    '
    Me.txtCellOrEmail.Location = New System.Drawing.Point(211, 57)
    Me.txtCellOrEmail.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCellOrEmail.Name = "txtCellOrEmail"
    Me.txtCellOrEmail.Size = New System.Drawing.Size(359, 25)
    Me.txtCellOrEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCellOrEmail.TabIndex = 2
    Me.txtCellOrEmail.Text = "txtCellOrEmail"
    '
    'lblCellOrEmail
    '
    Me.lblCellOrEmail.AutoSize = True
    Me.lblCellOrEmail.Location = New System.Drawing.Point(42, 60)
    Me.lblCellOrEmail.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCellOrEmail.Name = "lblCellOrEmail"
    Me.lblCellOrEmail.Size = New System.Drawing.Size(18, 13)
    Me.lblCellOrEmail.TabIndex = 3
    Me.lblCellOrEmail.Text = "Cell Or Email"
    '
    'DtxtProtectedFunction
    '
    Me.txtProtectedFunction.Location = New System.Drawing.Point(211, 97)
    Me.txtProtectedFunction.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProtectedFunction.Name = "txtProtectedFunction"
    Me.txtProtectedFunction.Size = New System.Drawing.Size(359, 25)
    Me.txtProtectedFunction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProtectedFunction.TabIndex = 4
    Me.txtProtectedFunction.Text = "txtProtectedFunction"
    '
    'lblProtectedFunction
    '
    Me.lblProtectedFunction.AutoSize = True
    Me.lblProtectedFunction.Location = New System.Drawing.Point(42, 100)
    Me.lblProtectedFunction.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProtectedFunction.Name = "lblProtectedFunction"
    Me.lblProtectedFunction.Size = New System.Drawing.Size(18, 13)
    Me.lblProtectedFunction.TabIndex = 5
    Me.lblProtectedFunction.Text = "Protected Function"
    '
    'DtxtCodeHashed
    '
    Me.txtCodeHashed.Location = New System.Drawing.Point(211, 137)
    Me.txtCodeHashed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCodeHashed.Name = "txtCodeHashed"
    Me.txtCodeHashed.Size = New System.Drawing.Size(359, 25)
    Me.txtCodeHashed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCodeHashed.TabIndex = 6
    Me.txtCodeHashed.Text = "txtCodeHashed"
    Me.txtCodeHashed.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtCodeHashed.UseSystemPasswordChar = True
    '
    'lblCodeHashed
    '
    Me.lblCodeHashed.AutoSize = True
    Me.lblCodeHashed.Location = New System.Drawing.Point(42, 140)
    Me.lblCodeHashed.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCodeHashed.Name = "lblCodeHashed"
    Me.lblCodeHashed.Size = New System.Drawing.Size(18, 13)
    Me.lblCodeHashed.TabIndex = 7
    Me.lblCodeHashed.Text = "Code"
    '
    'DtxtAttemptNo
    '
    Me.txtAttemptNo.Location = New System.Drawing.Point(211, 177)
    Me.txtAttemptNo.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtAttemptNo.Name = "txtAttemptNo"
    Me.txtAttemptNo.Size = New System.Drawing.Size(359, 25)
    Me.txtAttemptNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAttemptNo.TabIndex = 8
    Me.txtAttemptNo.Text = "txtAttemptNo"
    '
    'lblAttemptNo
    '
    Me.lblAttemptNo.AutoSize = True
    Me.lblAttemptNo.Location = New System.Drawing.Point(42, 180)
    Me.lblAttemptNo.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAttemptNo.Name = "lblAttemptNo"
    Me.lblAttemptNo.Size = New System.Drawing.Size(18, 13)
    Me.lblAttemptNo.TabIndex = 9
    Me.lblAttemptNo.Text = "Attempt No"
    '
    'chkIsSuccessful
    '
    Me.chkIsSuccessful.AutoSize = True
    Me.chkIsSuccessful.Location = New System.Drawing.Point(211, 223)
    Me.chkIsSuccessful.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkIsSuccessful.Name = "chkIsSuccessful"
    Me.chkIsSuccessful.Size = New System.Drawing.Size(15, 14)
    Me.chkIsSuccessful.TabIndex = 10
    Me.chkIsSuccessful.UseVisualStyleBackColor = True
    '
    'lblIsSuccessful
    '
    Me.lblIsSuccessful.AutoSize = True
    Me.lblIsSuccessful.Location = New System.Drawing.Point(42, 218)
    Me.lblIsSuccessful.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIsSuccessful.Name = "lblIsSuccessful"
    Me.lblIsSuccessful.Size = New System.Drawing.Size(18, 13)
    Me.lblIsSuccessful.TabIndex = 11
    Me.lblIsSuccessful.Text = "Is Successful"
    '
    'DtxtLastAccessingIP
    '
    Me.txtLastAccessingIP.Location = New System.Drawing.Point(211, 257)
    Me.txtLastAccessingIP.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLastAccessingIP.Name = "txtLastAccessingIP"
    Me.txtLastAccessingIP.Size = New System.Drawing.Size(359, 25)
    Me.txtLastAccessingIP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastAccessingIP.TabIndex = 12
    Me.txtLastAccessingIP.Text = "txtLastAccessingIP"
    '
    'lblLastAccessingIP
    '
    Me.lblLastAccessingIP.AutoSize = True
    Me.lblLastAccessingIP.Location = New System.Drawing.Point(42, 260)
    Me.lblLastAccessingIP.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLastAccessingIP.Name = "lblLastAccessingIP"
    Me.lblLastAccessingIP.Size = New System.Drawing.Size(18, 13)
    Me.lblLastAccessingIP.TabIndex = 13
    Me.lblLastAccessingIP.Text = "Last Accessing IP"
    '
    'DtxtLastAccessingCountry
    '
    Me.txtLastAccessingCountry.Location = New System.Drawing.Point(211, 297)
    Me.txtLastAccessingCountry.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLastAccessingCountry.Name = "txtLastAccessingCountry"
    Me.txtLastAccessingCountry.Size = New System.Drawing.Size(359, 25)
    Me.txtLastAccessingCountry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastAccessingCountry.TabIndex = 14
    Me.txtLastAccessingCountry.Text = "txtLastAccessingCountry"
    '
    'lblLastAccessingCountry
    '
    Me.lblLastAccessingCountry.AutoSize = True
    Me.lblLastAccessingCountry.Location = New System.Drawing.Point(42, 300)
    Me.lblLastAccessingCountry.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLastAccessingCountry.Name = "lblLastAccessingCountry"
    Me.lblLastAccessingCountry.Size = New System.Drawing.Size(18, 13)
    Me.lblLastAccessingCountry.TabIndex = 15
    Me.lblLastAccessingCountry.Text = "Last Accessing Country"
    '
    'cboUILang
    '
    Me.cboUILang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboUILang.FormattingEnabled = True
    Me.cboUILang.Location = New System.Drawing.Point(204, 331)
    Me.cboUILang.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboUILang.Name = "cboUILang"
    Me.cboUILang.Size = New System.Drawing.Size(309, 21)
    Me.cboUILang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUILang.TabIndex = 16
    '
    'BtxtUILang
    '
    Me.txtUILang.Location = New System.Drawing.Point(211, 337)
    Me.txtUILang.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUILang.Name = "txtUILang"
    Me.txtUILang.Size = New System.Drawing.Size(359, 20)
    Me.txtUILang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUILang.TabIndex = 17
    Me.txtUILang.Text = "txtUILang"
    '
    'DtxtUILang
    '
    Me.txtUILang.Location = New System.Drawing.Point(211, 337)
    Me.txtUILang.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUILang.Name = "txtUILang"
    Me.txtUILang.Size = New System.Drawing.Size(359, 25)
    Me.txtUILang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUILang.TabIndex = 18
    Me.txtUILang.Text = "txtUILang"
    '
    'lblUILang
    '
    Me.lblUILang.AutoSize = True
    Me.lblUILang.Location = New System.Drawing.Point(42, 340)
    Me.lblUILang.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUILang.Name = "lblUILang"
    Me.lblUILang.Size = New System.Drawing.Size(18, 13)
    Me.lblUILang.TabIndex = 19
    Me.lblUILang.Text = "UI Lang"
    '
    'dtpWhenCreated
    '
    Me.dtpWhenCreated.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpWhenCreated.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpWhenCreated.Location = New System.Drawing.Point(204, 371)
    Me.dtpWhenCreated.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpWhenCreated.Name = "dtpWhenCreated"
    Me.dtpWhenCreated.ShowCheckBox = True
    Me.dtpWhenCreated.ShowUpDown = True
    Me.dtpWhenCreated.Size = New System.Drawing.Size(309, 20)
    Me.dtpWhenCreated.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpWhenCreated.TabIndex = 20
    '
    'CtxtWhenCreated
    '
    Me.txtWhenCreated.Location = New System.Drawing.Point(211, 377)
    Me.txtWhenCreated.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtWhenCreated.Name = "txtWhenCreated"
    Me.txtWhenCreated.Size = New System.Drawing.Size(359, 20)
    Me.txtWhenCreated.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtWhenCreated.TabIndex = 21
    Me.txtWhenCreated.Text = "txtWhenCreated"
    '
    'lblWhenCreated
    '
    Me.lblWhenCreated.AutoSize = True
    Me.lblWhenCreated.Location = New System.Drawing.Point(42, 380)
    Me.lblWhenCreated.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblWhenCreated.Name = "lblWhenCreated"
    Me.lblWhenCreated.Size = New System.Drawing.Size(18, 13)
    Me.lblWhenCreated.TabIndex = 22
    Me.lblWhenCreated.Text = "When Created"
    '
    'dtpWhenAccessed
    '
    Me.dtpWhenAccessed.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpWhenAccessed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpWhenAccessed.Location = New System.Drawing.Point(204, 411)
    Me.dtpWhenAccessed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpWhenAccessed.Name = "dtpWhenAccessed"
    Me.dtpWhenAccessed.ShowCheckBox = True
    Me.dtpWhenAccessed.ShowUpDown = True
    Me.dtpWhenAccessed.Size = New System.Drawing.Size(309, 20)
    Me.dtpWhenAccessed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpWhenAccessed.TabIndex = 23
    '
    'CtxtWhenAccessed
    '
    Me.txtWhenAccessed.Location = New System.Drawing.Point(211, 417)
    Me.txtWhenAccessed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtWhenAccessed.Name = "txtWhenAccessed"
    Me.txtWhenAccessed.Size = New System.Drawing.Size(359, 20)
    Me.txtWhenAccessed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtWhenAccessed.TabIndex = 24
    Me.txtWhenAccessed.Text = "txtWhenAccessed"
    '
    'lblWhenAccessed
    '
    Me.lblWhenAccessed.AutoSize = True
    Me.lblWhenAccessed.Location = New System.Drawing.Point(42, 420)
    Me.lblWhenAccessed.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblWhenAccessed.Name = "lblWhenAccessed"
    Me.lblWhenAccessed.Size = New System.Drawing.Size(18, 13)
    Me.lblWhenAccessed.TabIndex = 25
    Me.lblWhenAccessed.Text = "When Accessed"
    '
    'dtpWhenExpires
    '
    Me.dtpWhenExpires.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpWhenExpires.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpWhenExpires.Location = New System.Drawing.Point(204, 451)
    Me.dtpWhenExpires.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpWhenExpires.Name = "dtpWhenExpires"
    Me.dtpWhenExpires.ShowCheckBox = True
    Me.dtpWhenExpires.ShowUpDown = True
    Me.dtpWhenExpires.Size = New System.Drawing.Size(309, 20)
    Me.dtpWhenExpires.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpWhenExpires.TabIndex = 26
    '
    'CtxtWhenExpires
    '
    Me.txtWhenExpires.Location = New System.Drawing.Point(211, 457)
    Me.txtWhenExpires.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtWhenExpires.Name = "txtWhenExpires"
    Me.txtWhenExpires.Size = New System.Drawing.Size(359, 20)
    Me.txtWhenExpires.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtWhenExpires.TabIndex = 27
    Me.txtWhenExpires.Text = "txtWhenExpires"
    '
    'lblWhenExpires
    '
    Me.lblWhenExpires.AutoSize = True
    Me.lblWhenExpires.Location = New System.Drawing.Point(42, 460)
    Me.lblWhenExpires.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblWhenExpires.Name = "lblWhenExpires"
    Me.lblWhenExpires.Size = New System.Drawing.Size(18, 13)
    Me.lblWhenExpires.TabIndex = 28
    Me.lblWhenExpires.Text = "When Expires"
    '
    'DtxtDetails
    '
    Me.txtDetails.Location = New System.Drawing.Point(211, 497)
    Me.txtDetails.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDetails.Name = "txtDetails"
    Me.txtDetails.Size = New System.Drawing.Size(359, 105)
    Me.txtDetails.Multiline = True
    Me.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtDetails.WordWrap = False 
    Me.txtDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDetails.TabIndex = 29
    Me.txtDetails.Text = "txtDetails"
    '
    'lblDetails
    '
    Me.lblDetails.AutoSize = True
    Me.lblDetails.Location = New System.Drawing.Point(42, 495)
    Me.lblDetails.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDetails.Name = "lblDetails"
    Me.lblDetails.Size = New System.Drawing.Size(18, 13)
    Me.lblDetails.TabIndex = 30
    Me.lblDetails.Text = "Details"
    '
    'cboUser
    '
    Me.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboUser.Location = New System.Drawing.Point(204, 611)
    Me.cboUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboUser.Name = "cboUser"
    Me.cboUser.Size = New System.Drawing.Size(309, 21)
    Me.cboUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUser.TabIndex = 31
    '
    'AtxtUser
    '
    Me.txtUser.Location = New System.Drawing.Point(211, 617)
    Me.txtUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUser.Name = "txtUser"
    Me.txtUser.Size = New System.Drawing.Size(359, 20)
    Me.txtUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUser.TabIndex = 32
    Me.txtUser.Text = "txtUser"
    '
    'lblUser
    '
    Me.lblUser.AutoSize = True
    Me.lblUser.Location = New System.Drawing.Point(42, 620)
    Me.lblUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(18, 13)
    Me.lblUser.TabIndex = 33
    Me.lblUser.Text = "User"
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 697)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 34
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 697)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 35
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 685)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 36
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 685)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 37
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlMFA 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtCellOrEmail)
    Me.Controls.Add(Me.lblCellOrEmail)
    Me.Controls.Add(Me.txtProtectedFunction)
    Me.Controls.Add(Me.lblProtectedFunction)
    Me.Controls.Add(Me.txtCodeHashed)
    Me.Controls.Add(Me.lblCodeHashed)
    Me.Controls.Add(Me.txtAttemptNo)
    Me.Controls.Add(Me.lblAttemptNo)
    Me.Controls.Add(Me.chkIsSuccessful)
    Me.Controls.Add(Me.lblIsSuccessful)
    Me.Controls.Add(Me.txtLastAccessingIP)
    Me.Controls.Add(Me.lblLastAccessingIP)
    Me.Controls.Add(Me.txtLastAccessingCountry)
    Me.Controls.Add(Me.lblLastAccessingCountry)
    Me.Controls.Add(Me.cboUILang)
    Me.Controls.Add(Me.txtUILang)
    Me.Controls.Add(Me.txtUILang)
    Me.Controls.Add(Me.lblUILang)
    Me.Controls.Add(Me.dtpWhenCreated)
    Me.Controls.Add(Me.txtWhenCreated)
    Me.Controls.Add(Me.lblWhenCreated)
    Me.Controls.Add(Me.dtpWhenAccessed)
    Me.Controls.Add(Me.txtWhenAccessed)
    Me.Controls.Add(Me.lblWhenAccessed)
    Me.Controls.Add(Me.dtpWhenExpires)
    Me.Controls.Add(Me.txtWhenExpires)
    Me.Controls.Add(Me.lblWhenExpires)
    Me.Controls.Add(Me.txtDetails)
    Me.Controls.Add(Me.lblDetails)
    Me.Controls.Add(Me.cboUser)
    Me.Controls.Add(Me.txtUser)
    Me.Controls.Add(Me.txtUser)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_MFA"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtCellOrEmail As System.Windows.Forms.TextBox
  Friend WithEvents lblCellOrEmail As System.Windows.Forms.Label
  Friend WithEvents txtProtectedFunction As System.Windows.Forms.TextBox
  Friend WithEvents lblProtectedFunction As System.Windows.Forms.Label
  Friend WithEvents txtCodeHashed As System.Windows.Forms.TextBox
  Friend WithEvents lblCodeHashed As System.Windows.Forms.Label
  Friend WithEvents txtAttemptNo As System.Windows.Forms.TextBox
  Friend WithEvents lblAttemptNo As System.Windows.Forms.Label
  Friend WithEvents chkIsSuccessful As System.Windows.Forms.CheckBox
  Friend WithEvents lblIsSuccessful As System.Windows.Forms.Label
  Friend WithEvents txtLastAccessingIP As System.Windows.Forms.TextBox
  Friend WithEvents lblLastAccessingIP As System.Windows.Forms.Label
  Friend WithEvents txtLastAccessingCountry As System.Windows.Forms.TextBox
  Friend WithEvents lblLastAccessingCountry As System.Windows.Forms.Label
  Friend WithEvents cboUILang As System.Windows.Forms.ComboBox
  Friend WithEvents txtUILang As System.Windows.Forms.TextBox
  Friend WithEvents lblUILang As System.Windows.Forms.Label
  Friend WithEvents dtpWhenCreated As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtWhenCreated As System.Windows.Forms.TextBox
  Friend WithEvents lblWhenCreated As System.Windows.Forms.Label
  Friend WithEvents dtpWhenAccessed As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtWhenAccessed As System.Windows.Forms.TextBox
  Friend WithEvents lblWhenAccessed As System.Windows.Forms.Label
  Friend WithEvents dtpWhenExpires As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtWhenExpires As System.Windows.Forms.TextBox
  Friend WithEvents lblWhenExpires As System.Windows.Forms.Label
  Friend WithEvents txtDetails As System.Windows.Forms.TextBox
  Friend WithEvents lblDetails As System.Windows.Forms.Label
  Friend WithEvents cboUser As IntelliCombo
  Friend WithEvents txtUser As System.Windows.Forms.TextBox
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
