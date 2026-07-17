<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccProductPriceHist
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
    Me.txtProductID = New System.Windows.Forms.TextBox()
    Me.lblProductID = New System.Windows.Forms.Label()
    Me.cboCustomerType = New System.Windows.Forms.ComboBox()
    Me.txtCustomerType = New System.Windows.Forms.TextBox()
    Me.lblCustomerType = New System.Windows.Forms.Label()
    Me.txtBaseCost = New System.Windows.Forms.TextBox()
    Me.lblBaseCost = New System.Windows.Forms.Label()
    Me.txtSellingPrice = New System.Windows.Forms.TextBox()
    Me.lblSellingPrice = New System.Windows.Forms.Label()
    Me.txtMinQuantity = New System.Windows.Forms.TextBox()
    Me.lblMinQuantity = New System.Windows.Forms.Label()
    Me.txtDiscountPercent = New System.Windows.Forms.TextBox()
    Me.lblDiscountPercent = New System.Windows.Forms.Label()
    Me.dtpValidFrom = New System.Windows.Forms.DateTimePicker()
    Me.txtValidFrom = New System.Windows.Forms.TextBox()
    Me.lblValidFrom = New System.Windows.Forms.Label()
    Me.dtpValidTo = New System.Windows.Forms.DateTimePicker()
    Me.txtValidTo = New System.Windows.Forms.TextBox()
    Me.lblValidTo = New System.Windows.Forms.Label()
    Me.dtpArchivedDate = New System.Windows.Forms.DateTimePicker()
    Me.txtArchivedDate = New System.Windows.Forms.TextBox()
    Me.lblArchivedDate = New System.Windows.Forms.Label()
    Me.txtArchivedReason = New System.Windows.Forms.TextBox()
    Me.lblArchivedReason = New System.Windows.Forms.Label()
    Me.txtOriginalPriceID = New System.Windows.Forms.TextBox()
    Me.lblOriginalPriceID = New System.Windows.Forms.Label()
    Me.txtNotes = New System.Windows.Forms.TextBox()
    Me.lblNotes = New System.Windows.Forms.Label()
    Me.txtAddFieldsHere = New System.Windows.Forms.TextBox()
    Me.lblAddFieldsHere = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(176, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(394, 25)
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
    'DtxtProductID
    '
    Me.txtProductID.Location = New System.Drawing.Point(176, 57)
    Me.txtProductID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProductID.Name = "txtProductID"
    Me.txtProductID.Size = New System.Drawing.Size(394, 25)
    Me.txtProductID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProductID.TabIndex = 2
    Me.txtProductID.Text = "txtProductID"
    '
    'lblProductID
    '
    Me.lblProductID.AutoSize = True
    Me.lblProductID.Location = New System.Drawing.Point(42, 60)
    Me.lblProductID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProductID.Name = "lblProductID"
    Me.lblProductID.Size = New System.Drawing.Size(18, 13)
    Me.lblProductID.TabIndex = 3
    Me.lblProductID.Text = "Product ID"
    '
    'cboCustomerType
    '
    Me.cboCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboCustomerType.FormattingEnabled = True
    Me.cboCustomerType.Location = New System.Drawing.Point(169, 91)
    Me.cboCustomerType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboCustomerType.Name = "cboCustomerType"
    Me.cboCustomerType.Size = New System.Drawing.Size(344, 21)
    Me.cboCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboCustomerType.TabIndex = 4
    '
    'BtxtCustomerType
    '
    Me.txtCustomerType.Location = New System.Drawing.Point(176, 97)
    Me.txtCustomerType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerType.Name = "txtCustomerType"
    Me.txtCustomerType.Size = New System.Drawing.Size(394, 20)
    Me.txtCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerType.TabIndex = 5
    Me.txtCustomerType.Text = "txtCustomerType"
    '
    'DtxtCustomerType
    '
    Me.txtCustomerType.Location = New System.Drawing.Point(176, 97)
    Me.txtCustomerType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerType.Name = "txtCustomerType"
    Me.txtCustomerType.Size = New System.Drawing.Size(394, 25)
    Me.txtCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerType.TabIndex = 6
    Me.txtCustomerType.Text = "txtCustomerType"
    '
    'lblCustomerType
    '
    Me.lblCustomerType.AutoSize = True
    Me.lblCustomerType.Location = New System.Drawing.Point(42, 100)
    Me.lblCustomerType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCustomerType.Name = "lblCustomerType"
    Me.lblCustomerType.Size = New System.Drawing.Size(18, 13)
    Me.lblCustomerType.TabIndex = 7
    Me.lblCustomerType.Text = "Customer Type"
    '
    'DtxtBaseCost
    '
    Me.txtBaseCost.Location = New System.Drawing.Point(176, 137)
    Me.txtBaseCost.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtBaseCost.Name = "txtBaseCost"
    Me.txtBaseCost.Size = New System.Drawing.Size(394, 25)
    Me.txtBaseCost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtBaseCost.TabIndex = 8
    Me.txtBaseCost.Text = "txtBaseCost"
    '
    'lblBaseCost
    '
    Me.lblBaseCost.AutoSize = True
    Me.lblBaseCost.Location = New System.Drawing.Point(42, 140)
    Me.lblBaseCost.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblBaseCost.Name = "lblBaseCost"
    Me.lblBaseCost.Size = New System.Drawing.Size(18, 13)
    Me.lblBaseCost.TabIndex = 9
    Me.lblBaseCost.Text = "Base Cost"
    '
    'DtxtSellingPrice
    '
    Me.txtSellingPrice.Location = New System.Drawing.Point(176, 177)
    Me.txtSellingPrice.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSellingPrice.Name = "txtSellingPrice"
    Me.txtSellingPrice.Size = New System.Drawing.Size(394, 25)
    Me.txtSellingPrice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSellingPrice.TabIndex = 10
    Me.txtSellingPrice.Text = "txtSellingPrice"
    '
    'lblSellingPrice
    '
    Me.lblSellingPrice.AutoSize = True
    Me.lblSellingPrice.Location = New System.Drawing.Point(42, 180)
    Me.lblSellingPrice.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSellingPrice.Name = "lblSellingPrice"
    Me.lblSellingPrice.Size = New System.Drawing.Size(18, 13)
    Me.lblSellingPrice.TabIndex = 11
    Me.lblSellingPrice.Text = "Selling Price"
    '
    'DtxtMinQuantity
    '
    Me.txtMinQuantity.Location = New System.Drawing.Point(176, 217)
    Me.txtMinQuantity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtMinQuantity.Name = "txtMinQuantity"
    Me.txtMinQuantity.Size = New System.Drawing.Size(394, 25)
    Me.txtMinQuantity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtMinQuantity.TabIndex = 12
    Me.txtMinQuantity.Text = "txtMinQuantity"
    '
    'lblMinQuantity
    '
    Me.lblMinQuantity.AutoSize = True
    Me.lblMinQuantity.Location = New System.Drawing.Point(42, 220)
    Me.lblMinQuantity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblMinQuantity.Name = "lblMinQuantity"
    Me.lblMinQuantity.Size = New System.Drawing.Size(18, 13)
    Me.lblMinQuantity.TabIndex = 13
    Me.lblMinQuantity.Text = "Min Quantity"
    '
    'DtxtDiscountPercent
    '
    Me.txtDiscountPercent.Location = New System.Drawing.Point(176, 257)
    Me.txtDiscountPercent.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDiscountPercent.Name = "txtDiscountPercent"
    Me.txtDiscountPercent.Size = New System.Drawing.Size(394, 25)
    Me.txtDiscountPercent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDiscountPercent.TabIndex = 14
    Me.txtDiscountPercent.Text = "txtDiscountPercent"
    '
    'lblDiscountPercent
    '
    Me.lblDiscountPercent.AutoSize = True
    Me.lblDiscountPercent.Location = New System.Drawing.Point(42, 260)
    Me.lblDiscountPercent.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDiscountPercent.Name = "lblDiscountPercent"
    Me.lblDiscountPercent.Size = New System.Drawing.Size(18, 13)
    Me.lblDiscountPercent.TabIndex = 15
    Me.lblDiscountPercent.Text = "Discount Percent"
    '
    'dtpValidFrom
    '
    Me.dtpValidFrom.CustomFormat = "dd-MM-yyyy"
    Me.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpValidFrom.Location = New System.Drawing.Point(169, 291)
    Me.dtpValidFrom.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpValidFrom.Name = "dtpValidFrom"
    Me.dtpValidFrom.ShowCheckBox = True
    Me.dtpValidFrom.ShowUpDown = True
    Me.dtpValidFrom.Size = New System.Drawing.Size(344, 20)
    Me.dtpValidFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpValidFrom.TabIndex = 16
    '
    'CtxtValidFrom
    '
    Me.txtValidFrom.Location = New System.Drawing.Point(176, 297)
    Me.txtValidFrom.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtValidFrom.Name = "txtValidFrom"
    Me.txtValidFrom.Size = New System.Drawing.Size(394, 20)
    Me.txtValidFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtValidFrom.TabIndex = 17
    Me.txtValidFrom.Text = "txtValidFrom"
    '
    'lblValidFrom
    '
    Me.lblValidFrom.AutoSize = True
    Me.lblValidFrom.Location = New System.Drawing.Point(42, 300)
    Me.lblValidFrom.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblValidFrom.Name = "lblValidFrom"
    Me.lblValidFrom.Size = New System.Drawing.Size(18, 13)
    Me.lblValidFrom.TabIndex = 18
    Me.lblValidFrom.Text = "Valid From"
    '
    'dtpValidTo
    '
    Me.dtpValidTo.CustomFormat = "dd-MM-yyyy"
    Me.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpValidTo.Location = New System.Drawing.Point(169, 331)
    Me.dtpValidTo.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpValidTo.Name = "dtpValidTo"
    Me.dtpValidTo.ShowCheckBox = True
    Me.dtpValidTo.ShowUpDown = True
    Me.dtpValidTo.Size = New System.Drawing.Size(344, 20)
    Me.dtpValidTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpValidTo.TabIndex = 19
    '
    'CtxtValidTo
    '
    Me.txtValidTo.Location = New System.Drawing.Point(176, 337)
    Me.txtValidTo.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtValidTo.Name = "txtValidTo"
    Me.txtValidTo.Size = New System.Drawing.Size(394, 20)
    Me.txtValidTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtValidTo.TabIndex = 20
    Me.txtValidTo.Text = "txtValidTo"
    '
    'lblValidTo
    '
    Me.lblValidTo.AutoSize = True
    Me.lblValidTo.Location = New System.Drawing.Point(42, 340)
    Me.lblValidTo.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblValidTo.Name = "lblValidTo"
    Me.lblValidTo.Size = New System.Drawing.Size(18, 13)
    Me.lblValidTo.TabIndex = 21
    Me.lblValidTo.Text = "Valid To"
    '
    'dtpArchivedDate
    '
    Me.dtpArchivedDate.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpArchivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpArchivedDate.Location = New System.Drawing.Point(169, 371)
    Me.dtpArchivedDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpArchivedDate.Name = "dtpArchivedDate"
    Me.dtpArchivedDate.ShowCheckBox = True
    Me.dtpArchivedDate.ShowUpDown = True
    Me.dtpArchivedDate.Size = New System.Drawing.Size(344, 20)
    Me.dtpArchivedDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpArchivedDate.TabIndex = 22
    '
    'CtxtArchivedDate
    '
    Me.txtArchivedDate.Location = New System.Drawing.Point(176, 377)
    Me.txtArchivedDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtArchivedDate.Name = "txtArchivedDate"
    Me.txtArchivedDate.Size = New System.Drawing.Size(394, 20)
    Me.txtArchivedDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtArchivedDate.TabIndex = 23
    Me.txtArchivedDate.Text = "txtArchivedDate"
    '
    'lblArchivedDate
    '
    Me.lblArchivedDate.AutoSize = True
    Me.lblArchivedDate.Location = New System.Drawing.Point(42, 380)
    Me.lblArchivedDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblArchivedDate.Name = "lblArchivedDate"
    Me.lblArchivedDate.Size = New System.Drawing.Size(18, 13)
    Me.lblArchivedDate.TabIndex = 24
    Me.lblArchivedDate.Text = "Archived Date"
    '
    'DtxtArchivedReason
    '
    Me.txtArchivedReason.Location = New System.Drawing.Point(176, 417)
    Me.txtArchivedReason.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtArchivedReason.Name = "txtArchivedReason"
    Me.txtArchivedReason.Size = New System.Drawing.Size(394, 105)
    Me.txtArchivedReason.Multiline = True
    Me.txtArchivedReason.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtArchivedReason.WordWrap = False 
    Me.txtArchivedReason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtArchivedReason.TabIndex = 25
    Me.txtArchivedReason.Text = "txtArchivedReason"
    '
    'lblArchivedReason
    '
    Me.lblArchivedReason.AutoSize = True
    Me.lblArchivedReason.Location = New System.Drawing.Point(42, 415)
    Me.lblArchivedReason.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblArchivedReason.Name = "lblArchivedReason"
    Me.lblArchivedReason.Size = New System.Drawing.Size(18, 13)
    Me.lblArchivedReason.TabIndex = 26
    Me.lblArchivedReason.Text = "Archived Reason"
    '
    'DtxtOriginalPriceID
    '
    Me.txtOriginalPriceID.Location = New System.Drawing.Point(176, 537)
    Me.txtOriginalPriceID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOriginalPriceID.Name = "txtOriginalPriceID"
    Me.txtOriginalPriceID.Size = New System.Drawing.Size(394, 25)
    Me.txtOriginalPriceID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOriginalPriceID.TabIndex = 27
    Me.txtOriginalPriceID.Text = "txtOriginalPriceID"
    '
    'lblOriginalPriceID
    '
    Me.lblOriginalPriceID.AutoSize = True
    Me.lblOriginalPriceID.Location = New System.Drawing.Point(42, 540)
    Me.lblOriginalPriceID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOriginalPriceID.Name = "lblOriginalPriceID"
    Me.lblOriginalPriceID.Size = New System.Drawing.Size(18, 13)
    Me.lblOriginalPriceID.TabIndex = 28
    Me.lblOriginalPriceID.Text = "Original Price ID"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(176, 577)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(394, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 29
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 575)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 30
    Me.lblNotes.Text = "Notes"
    '
    'DtxtAddFieldsHere
    '
    Me.txtAddFieldsHere.Location = New System.Drawing.Point(176, 697)
    Me.txtAddFieldsHere.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtAddFieldsHere.Name = "txtAddFieldsHere"
    Me.txtAddFieldsHere.Size = New System.Drawing.Size(394, 25)
    Me.txtAddFieldsHere.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAddFieldsHere.TabIndex = 31
    Me.txtAddFieldsHere.Text = "txtAddFieldsHere"
    '
    'lblAddFieldsHere
    '
    Me.lblAddFieldsHere.AutoSize = True
    Me.lblAddFieldsHere.Location = New System.Drawing.Point(42, 700)
    Me.lblAddFieldsHere.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAddFieldsHere.Name = "lblAddFieldsHere"
    Me.lblAddFieldsHere.Size = New System.Drawing.Size(18, 13)
    Me.lblAddFieldsHere.TabIndex = 32
    Me.lblAddFieldsHere.Text = "Add Fields Here"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 777)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 33
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 777)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 34
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 777)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 35
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 765)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 36
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 765)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 37
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlProductPriceHist 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtProductID)
    Me.Controls.Add(Me.lblProductID)
    Me.Controls.Add(Me.cboCustomerType)
    Me.Controls.Add(Me.txtCustomerType)
    Me.Controls.Add(Me.txtCustomerType)
    Me.Controls.Add(Me.lblCustomerType)
    Me.Controls.Add(Me.txtBaseCost)
    Me.Controls.Add(Me.lblBaseCost)
    Me.Controls.Add(Me.txtSellingPrice)
    Me.Controls.Add(Me.lblSellingPrice)
    Me.Controls.Add(Me.txtMinQuantity)
    Me.Controls.Add(Me.lblMinQuantity)
    Me.Controls.Add(Me.txtDiscountPercent)
    Me.Controls.Add(Me.lblDiscountPercent)
    Me.Controls.Add(Me.dtpValidFrom)
    Me.Controls.Add(Me.txtValidFrom)
    Me.Controls.Add(Me.lblValidFrom)
    Me.Controls.Add(Me.dtpValidTo)
    Me.Controls.Add(Me.txtValidTo)
    Me.Controls.Add(Me.lblValidTo)
    Me.Controls.Add(Me.dtpArchivedDate)
    Me.Controls.Add(Me.txtArchivedDate)
    Me.Controls.Add(Me.lblArchivedDate)
    Me.Controls.Add(Me.txtArchivedReason)
    Me.Controls.Add(Me.lblArchivedReason)
    Me.Controls.Add(Me.txtOriginalPriceID)
    Me.Controls.Add(Me.lblOriginalPriceID)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.txtAddFieldsHere)
    Me.Controls.Add(Me.lblAddFieldsHere)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccProductPriceHist"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtProductID As System.Windows.Forms.TextBox
  Friend WithEvents lblProductID As System.Windows.Forms.Label
  Friend WithEvents cboCustomerType As System.Windows.Forms.ComboBox
  Friend WithEvents txtCustomerType As System.Windows.Forms.TextBox
  Friend WithEvents lblCustomerType As System.Windows.Forms.Label
  Friend WithEvents txtBaseCost As System.Windows.Forms.TextBox
  Friend WithEvents lblBaseCost As System.Windows.Forms.Label
  Friend WithEvents txtSellingPrice As System.Windows.Forms.TextBox
  Friend WithEvents lblSellingPrice As System.Windows.Forms.Label
  Friend WithEvents txtMinQuantity As System.Windows.Forms.TextBox
  Friend WithEvents lblMinQuantity As System.Windows.Forms.Label
  Friend WithEvents txtDiscountPercent As System.Windows.Forms.TextBox
  Friend WithEvents lblDiscountPercent As System.Windows.Forms.Label
  Friend WithEvents dtpValidFrom As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtValidFrom As System.Windows.Forms.TextBox
  Friend WithEvents lblValidFrom As System.Windows.Forms.Label
  Friend WithEvents dtpValidTo As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtValidTo As System.Windows.Forms.TextBox
  Friend WithEvents lblValidTo As System.Windows.Forms.Label
  Friend WithEvents dtpArchivedDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtArchivedDate As System.Windows.Forms.TextBox
  Friend WithEvents lblArchivedDate As System.Windows.Forms.Label
  Friend WithEvents txtArchivedReason As System.Windows.Forms.TextBox
  Friend WithEvents lblArchivedReason As System.Windows.Forms.Label
  Friend WithEvents txtOriginalPriceID As System.Windows.Forms.TextBox
  Friend WithEvents lblOriginalPriceID As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents txtAddFieldsHere As System.Windows.Forms.TextBox
  Friend WithEvents lblAddFieldsHere As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
