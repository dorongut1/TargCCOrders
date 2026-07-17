<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccProductPrice
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
    Me.cboProduct = New IntelliCombo()
    Me.txtProduct = New System.Windows.Forms.TextBox()
    Me.lblProduct = New System.Windows.Forms.Label()
    Me.cboCustomerType = New System.Windows.Forms.ComboBox()
    Me.txtCustomerType = New System.Windows.Forms.TextBox()
    Me.lblCustomerType = New System.Windows.Forms.Label()
    Me.txtSellingPrice = New System.Windows.Forms.TextBox()
    Me.lblSellingPrice = New System.Windows.Forms.Label()
    Me.txtMinQuantity = New System.Windows.Forms.TextBox()
    Me.lblMinQuantity = New System.Windows.Forms.Label()
    Me.txtDiscountPercent = New System.Windows.Forms.TextBox()
    Me.lblDiscountPercent = New System.Windows.Forms.Label()
    Me.txtNotes = New System.Windows.Forms.TextBox()
    Me.lblNotes = New System.Windows.Forms.Label()
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
    'cboProduct
    '
    Me.cboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboProduct.Location = New System.Drawing.Point(169, 51)
    Me.cboProduct.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboProduct.Name = "cboProduct"
    Me.cboProduct.Size = New System.Drawing.Size(344, 21)
    Me.cboProduct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboProduct.TabIndex = 2
    '
    'AtxtProduct
    '
    Me.txtProduct.Location = New System.Drawing.Point(176, 57)
    Me.txtProduct.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProduct.Name = "txtProduct"
    Me.txtProduct.Size = New System.Drawing.Size(394, 20)
    Me.txtProduct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProduct.TabIndex = 3
    Me.txtProduct.Text = "txtProduct"
    '
    'lblProduct
    '
    Me.lblProduct.AutoSize = True
    Me.lblProduct.Location = New System.Drawing.Point(42, 60)
    Me.lblProduct.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProduct.Name = "lblProduct"
    Me.lblProduct.Size = New System.Drawing.Size(18, 13)
    Me.lblProduct.TabIndex = 4
    Me.lblProduct.Text = "Product"
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
    Me.cboCustomerType.TabIndex = 5
    '
    'BtxtCustomerType
    '
    Me.txtCustomerType.Location = New System.Drawing.Point(176, 97)
    Me.txtCustomerType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerType.Name = "txtCustomerType"
    Me.txtCustomerType.Size = New System.Drawing.Size(394, 20)
    Me.txtCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerType.TabIndex = 6
    Me.txtCustomerType.Text = "txtCustomerType"
    '
    'DtxtCustomerType
    '
    Me.txtCustomerType.Location = New System.Drawing.Point(176, 97)
    Me.txtCustomerType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerType.Name = "txtCustomerType"
    Me.txtCustomerType.Size = New System.Drawing.Size(394, 25)
    Me.txtCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerType.TabIndex = 7
    Me.txtCustomerType.Text = "txtCustomerType"
    '
    'lblCustomerType
    '
    Me.lblCustomerType.AutoSize = True
    Me.lblCustomerType.Location = New System.Drawing.Point(42, 100)
    Me.lblCustomerType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCustomerType.Name = "lblCustomerType"
    Me.lblCustomerType.Size = New System.Drawing.Size(18, 13)
    Me.lblCustomerType.TabIndex = 8
    Me.lblCustomerType.Text = "Customer Type"
    '
    'DtxtSellingPrice
    '
    Me.txtSellingPrice.Location = New System.Drawing.Point(176, 137)
    Me.txtSellingPrice.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSellingPrice.Name = "txtSellingPrice"
    Me.txtSellingPrice.Size = New System.Drawing.Size(394, 25)
    Me.txtSellingPrice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSellingPrice.TabIndex = 9
    Me.txtSellingPrice.Text = "txtSellingPrice"
    '
    'lblSellingPrice
    '
    Me.lblSellingPrice.AutoSize = True
    Me.lblSellingPrice.Location = New System.Drawing.Point(42, 140)
    Me.lblSellingPrice.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSellingPrice.Name = "lblSellingPrice"
    Me.lblSellingPrice.Size = New System.Drawing.Size(18, 13)
    Me.lblSellingPrice.TabIndex = 10
    Me.lblSellingPrice.Text = "Selling Price"
    '
    'DtxtMinQuantity
    '
    Me.txtMinQuantity.Location = New System.Drawing.Point(176, 177)
    Me.txtMinQuantity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtMinQuantity.Name = "txtMinQuantity"
    Me.txtMinQuantity.Size = New System.Drawing.Size(394, 25)
    Me.txtMinQuantity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtMinQuantity.TabIndex = 11
    Me.txtMinQuantity.Text = "txtMinQuantity"
    '
    'lblMinQuantity
    '
    Me.lblMinQuantity.AutoSize = True
    Me.lblMinQuantity.Location = New System.Drawing.Point(42, 180)
    Me.lblMinQuantity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblMinQuantity.Name = "lblMinQuantity"
    Me.lblMinQuantity.Size = New System.Drawing.Size(18, 13)
    Me.lblMinQuantity.TabIndex = 12
    Me.lblMinQuantity.Text = "Min Quantity"
    '
    'DtxtDiscountPercent
    '
    Me.txtDiscountPercent.Location = New System.Drawing.Point(176, 217)
    Me.txtDiscountPercent.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDiscountPercent.Name = "txtDiscountPercent"
    Me.txtDiscountPercent.Size = New System.Drawing.Size(394, 25)
    Me.txtDiscountPercent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDiscountPercent.TabIndex = 13
    Me.txtDiscountPercent.Text = "txtDiscountPercent"
    '
    'lblDiscountPercent
    '
    Me.lblDiscountPercent.AutoSize = True
    Me.lblDiscountPercent.Location = New System.Drawing.Point(42, 220)
    Me.lblDiscountPercent.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDiscountPercent.Name = "lblDiscountPercent"
    Me.lblDiscountPercent.Size = New System.Drawing.Size(18, 13)
    Me.lblDiscountPercent.TabIndex = 14
    Me.lblDiscountPercent.Text = "Discount Percent"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(176, 257)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(394, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 15
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 255)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 16
    Me.lblNotes.Text = "Notes"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 417)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 17
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 417)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 18
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 417)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 19
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 405)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 20
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 405)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 21
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlProductPrice 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboProduct)
    Me.Controls.Add(Me.txtProduct)
    Me.Controls.Add(Me.txtProduct)
    Me.Controls.Add(Me.lblProduct)
    Me.Controls.Add(Me.cboCustomerType)
    Me.Controls.Add(Me.txtCustomerType)
    Me.Controls.Add(Me.txtCustomerType)
    Me.Controls.Add(Me.lblCustomerType)
    Me.Controls.Add(Me.txtSellingPrice)
    Me.Controls.Add(Me.lblSellingPrice)
    Me.Controls.Add(Me.txtMinQuantity)
    Me.Controls.Add(Me.lblMinQuantity)
    Me.Controls.Add(Me.txtDiscountPercent)
    Me.Controls.Add(Me.lblDiscountPercent)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccProductPrice"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboProduct As IntelliCombo
  Friend WithEvents txtProduct As System.Windows.Forms.TextBox
  Friend WithEvents lblProduct As System.Windows.Forms.Label
  Friend WithEvents cboCustomerType As System.Windows.Forms.ComboBox
  Friend WithEvents txtCustomerType As System.Windows.Forms.TextBox
  Friend WithEvents lblCustomerType As System.Windows.Forms.Label
  Friend WithEvents txtSellingPrice As System.Windows.Forms.TextBox
  Friend WithEvents lblSellingPrice As System.Windows.Forms.Label
  Friend WithEvents txtMinQuantity As System.Windows.Forms.TextBox
  Friend WithEvents lblMinQuantity As System.Windows.Forms.Label
  Friend WithEvents txtDiscountPercent As System.Windows.Forms.TextBox
  Friend WithEvents lblDiscountPercent As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
