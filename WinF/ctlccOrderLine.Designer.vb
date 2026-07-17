<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccOrderLine
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
    Me.cboOrderHeader = New IntelliCombo()
    Me.txtOrderHeader = New System.Windows.Forms.TextBox()
    Me.lblOrderHeader = New System.Windows.Forms.Label()
    Me.cboProduct = New IntelliCombo()
    Me.txtProduct = New System.Windows.Forms.TextBox()
    Me.lblProduct = New System.Windows.Forms.Label()
    Me.txtQuantity = New System.Windows.Forms.TextBox()
    Me.lblQuantity = New System.Windows.Forms.Label()
    Me.txtUnitPrice = New System.Windows.Forms.TextBox()
    Me.lblUnitPrice = New System.Windows.Forms.Label()
    Me.txtDiscountPercent = New System.Windows.Forms.TextBox()
    Me.lblDiscountPercent = New System.Windows.Forms.Label()
    Me.txtUnitCost = New System.Windows.Forms.TextBox()
    Me.lblUnitCost = New System.Windows.Forms.Label()
    Me.txtLineNumber = New System.Windows.Forms.TextBox()
    Me.lblLineNumber = New System.Windows.Forms.Label()
    Me.txtLineTotal = New System.Windows.Forms.TextBox()
    Me.lblLineTotal = New System.Windows.Forms.Label()
    Me.txtTotalCost = New System.Windows.Forms.TextBox()
    Me.lblTotalCost = New System.Windows.Forms.Label()
    Me.txtProfit = New System.Windows.Forms.TextBox()
    Me.lblProfit = New System.Windows.Forms.Label()
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
    'cboOrderHeader
    '
    Me.cboOrderHeader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboOrderHeader.Location = New System.Drawing.Point(169, 51)
    Me.cboOrderHeader.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboOrderHeader.Name = "cboOrderHeader"
    Me.cboOrderHeader.Size = New System.Drawing.Size(344, 21)
    Me.cboOrderHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboOrderHeader.TabIndex = 2
    '
    'AtxtOrderHeader
    '
    Me.txtOrderHeader.Location = New System.Drawing.Point(176, 57)
    Me.txtOrderHeader.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderHeader.Name = "txtOrderHeader"
    Me.txtOrderHeader.Size = New System.Drawing.Size(394, 20)
    Me.txtOrderHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderHeader.TabIndex = 3
    Me.txtOrderHeader.Text = "txtOrderHeader"
    '
    'lblOrderHeader
    '
    Me.lblOrderHeader.AutoSize = True
    Me.lblOrderHeader.Location = New System.Drawing.Point(42, 60)
    Me.lblOrderHeader.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOrderHeader.Name = "lblOrderHeader"
    Me.lblOrderHeader.Size = New System.Drawing.Size(18, 13)
    Me.lblOrderHeader.TabIndex = 4
    Me.lblOrderHeader.Text = "Order Header"
    '
    'cboProduct
    '
    Me.cboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboProduct.Location = New System.Drawing.Point(169, 91)
    Me.cboProduct.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboProduct.Name = "cboProduct"
    Me.cboProduct.Size = New System.Drawing.Size(344, 21)
    Me.cboProduct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboProduct.TabIndex = 5
    '
    'AtxtProduct
    '
    Me.txtProduct.Location = New System.Drawing.Point(176, 97)
    Me.txtProduct.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProduct.Name = "txtProduct"
    Me.txtProduct.Size = New System.Drawing.Size(394, 20)
    Me.txtProduct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProduct.TabIndex = 6
    Me.txtProduct.Text = "txtProduct"
    '
    'lblProduct
    '
    Me.lblProduct.AutoSize = True
    Me.lblProduct.Location = New System.Drawing.Point(42, 100)
    Me.lblProduct.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProduct.Name = "lblProduct"
    Me.lblProduct.Size = New System.Drawing.Size(18, 13)
    Me.lblProduct.TabIndex = 7
    Me.lblProduct.Text = "Product"
    '
    'DtxtQuantity
    '
    Me.txtQuantity.Location = New System.Drawing.Point(176, 137)
    Me.txtQuantity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtQuantity.Name = "txtQuantity"
    Me.txtQuantity.Size = New System.Drawing.Size(394, 25)
    Me.txtQuantity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtQuantity.TabIndex = 8
    Me.txtQuantity.Text = "txtQuantity"
    '
    'lblQuantity
    '
    Me.lblQuantity.AutoSize = True
    Me.lblQuantity.Location = New System.Drawing.Point(42, 140)
    Me.lblQuantity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblQuantity.Name = "lblQuantity"
    Me.lblQuantity.Size = New System.Drawing.Size(18, 13)
    Me.lblQuantity.TabIndex = 9
    Me.lblQuantity.Text = "Quantity"
    '
    'DtxtUnitPrice
    '
    Me.txtUnitPrice.Location = New System.Drawing.Point(176, 177)
    Me.txtUnitPrice.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUnitPrice.Name = "txtUnitPrice"
    Me.txtUnitPrice.Size = New System.Drawing.Size(394, 25)
    Me.txtUnitPrice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUnitPrice.TabIndex = 10
    Me.txtUnitPrice.Text = "txtUnitPrice"
    '
    'lblUnitPrice
    '
    Me.lblUnitPrice.AutoSize = True
    Me.lblUnitPrice.Location = New System.Drawing.Point(42, 180)
    Me.lblUnitPrice.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUnitPrice.Name = "lblUnitPrice"
    Me.lblUnitPrice.Size = New System.Drawing.Size(18, 13)
    Me.lblUnitPrice.TabIndex = 11
    Me.lblUnitPrice.Text = "Unit Price"
    '
    'DtxtDiscountPercent
    '
    Me.txtDiscountPercent.Location = New System.Drawing.Point(176, 217)
    Me.txtDiscountPercent.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDiscountPercent.Name = "txtDiscountPercent"
    Me.txtDiscountPercent.Size = New System.Drawing.Size(394, 25)
    Me.txtDiscountPercent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDiscountPercent.TabIndex = 12
    Me.txtDiscountPercent.Text = "txtDiscountPercent"
    '
    'lblDiscountPercent
    '
    Me.lblDiscountPercent.AutoSize = True
    Me.lblDiscountPercent.Location = New System.Drawing.Point(42, 220)
    Me.lblDiscountPercent.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDiscountPercent.Name = "lblDiscountPercent"
    Me.lblDiscountPercent.Size = New System.Drawing.Size(18, 13)
    Me.lblDiscountPercent.TabIndex = 13
    Me.lblDiscountPercent.Text = "Discount Percent"
    '
    'DtxtUnitCost
    '
    Me.txtUnitCost.Location = New System.Drawing.Point(176, 257)
    Me.txtUnitCost.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUnitCost.Name = "txtUnitCost"
    Me.txtUnitCost.Size = New System.Drawing.Size(394, 25)
    Me.txtUnitCost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUnitCost.TabIndex = 14
    Me.txtUnitCost.Text = "txtUnitCost"
    '
    'lblUnitCost
    '
    Me.lblUnitCost.AutoSize = True
    Me.lblUnitCost.Location = New System.Drawing.Point(42, 260)
    Me.lblUnitCost.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUnitCost.Name = "lblUnitCost"
    Me.lblUnitCost.Size = New System.Drawing.Size(18, 13)
    Me.lblUnitCost.TabIndex = 15
    Me.lblUnitCost.Text = "Unit Cost"
    '
    'DtxtLineNumber
    '
    Me.txtLineNumber.Location = New System.Drawing.Point(176, 297)
    Me.txtLineNumber.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLineNumber.Name = "txtLineNumber"
    Me.txtLineNumber.Size = New System.Drawing.Size(394, 25)
    Me.txtLineNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLineNumber.TabIndex = 16
    Me.txtLineNumber.Text = "txtLineNumber"
    '
    'lblLineNumber
    '
    Me.lblLineNumber.AutoSize = True
    Me.lblLineNumber.Location = New System.Drawing.Point(42, 300)
    Me.lblLineNumber.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLineNumber.Name = "lblLineNumber"
    Me.lblLineNumber.Size = New System.Drawing.Size(18, 13)
    Me.lblLineNumber.TabIndex = 17
    Me.lblLineNumber.Text = "Line Number"
    '
    'DtxtLineTotal
    '
    Me.txtLineTotal.Location = New System.Drawing.Point(176, 337)
    Me.txtLineTotal.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLineTotal.Name = "txtLineTotal"
    Me.txtLineTotal.Size = New System.Drawing.Size(394, 25)
    Me.txtLineTotal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLineTotal.TabIndex = 18
    Me.txtLineTotal.Text = "txtLineTotal"
    '
    'lblLineTotal
    '
    Me.lblLineTotal.AutoSize = True
    Me.lblLineTotal.Location = New System.Drawing.Point(42, 340)
    Me.lblLineTotal.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLineTotal.Name = "lblLineTotal"
    Me.lblLineTotal.Size = New System.Drawing.Size(18, 13)
    Me.lblLineTotal.TabIndex = 19
    Me.lblLineTotal.Text = "Line Total"
    '
    'DtxtTotalCost
    '
    Me.txtTotalCost.Location = New System.Drawing.Point(176, 377)
    Me.txtTotalCost.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTotalCost.Name = "txtTotalCost"
    Me.txtTotalCost.Size = New System.Drawing.Size(394, 25)
    Me.txtTotalCost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTotalCost.TabIndex = 20
    Me.txtTotalCost.Text = "txtTotalCost"
    '
    'lblTotalCost
    '
    Me.lblTotalCost.AutoSize = True
    Me.lblTotalCost.Location = New System.Drawing.Point(42, 380)
    Me.lblTotalCost.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTotalCost.Name = "lblTotalCost"
    Me.lblTotalCost.Size = New System.Drawing.Size(18, 13)
    Me.lblTotalCost.TabIndex = 21
    Me.lblTotalCost.Text = "Total Cost"
    '
    'DtxtProfit
    '
    Me.txtProfit.Location = New System.Drawing.Point(176, 417)
    Me.txtProfit.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProfit.Name = "txtProfit"
    Me.txtProfit.Size = New System.Drawing.Size(394, 25)
    Me.txtProfit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProfit.TabIndex = 22
    Me.txtProfit.Text = "txtProfit"
    '
    'lblProfit
    '
    Me.lblProfit.AutoSize = True
    Me.lblProfit.Location = New System.Drawing.Point(42, 420)
    Me.lblProfit.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProfit.Name = "lblProfit"
    Me.lblProfit.Size = New System.Drawing.Size(18, 13)
    Me.lblProfit.TabIndex = 23
    Me.lblProfit.Text = "Profit"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 497)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 24
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 497)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 25
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 497)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 26
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 485)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 27
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 485)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 28
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlOrderLine 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboOrderHeader)
    Me.Controls.Add(Me.txtOrderHeader)
    Me.Controls.Add(Me.txtOrderHeader)
    Me.Controls.Add(Me.lblOrderHeader)
    Me.Controls.Add(Me.cboProduct)
    Me.Controls.Add(Me.txtProduct)
    Me.Controls.Add(Me.txtProduct)
    Me.Controls.Add(Me.lblProduct)
    Me.Controls.Add(Me.txtQuantity)
    Me.Controls.Add(Me.lblQuantity)
    Me.Controls.Add(Me.txtUnitPrice)
    Me.Controls.Add(Me.lblUnitPrice)
    Me.Controls.Add(Me.txtDiscountPercent)
    Me.Controls.Add(Me.lblDiscountPercent)
    Me.Controls.Add(Me.txtUnitCost)
    Me.Controls.Add(Me.lblUnitCost)
    Me.Controls.Add(Me.txtLineNumber)
    Me.Controls.Add(Me.lblLineNumber)
    Me.Controls.Add(Me.txtLineTotal)
    Me.Controls.Add(Me.lblLineTotal)
    Me.Controls.Add(Me.txtTotalCost)
    Me.Controls.Add(Me.lblTotalCost)
    Me.Controls.Add(Me.txtProfit)
    Me.Controls.Add(Me.lblProfit)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccOrderLine"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboOrderHeader As IntelliCombo
  Friend WithEvents txtOrderHeader As System.Windows.Forms.TextBox
  Friend WithEvents lblOrderHeader As System.Windows.Forms.Label
  Friend WithEvents cboProduct As IntelliCombo
  Friend WithEvents txtProduct As System.Windows.Forms.TextBox
  Friend WithEvents lblProduct As System.Windows.Forms.Label
  Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
  Friend WithEvents lblQuantity As System.Windows.Forms.Label
  Friend WithEvents txtUnitPrice As System.Windows.Forms.TextBox
  Friend WithEvents lblUnitPrice As System.Windows.Forms.Label
  Friend WithEvents txtDiscountPercent As System.Windows.Forms.TextBox
  Friend WithEvents lblDiscountPercent As System.Windows.Forms.Label
  Friend WithEvents txtUnitCost As System.Windows.Forms.TextBox
  Friend WithEvents lblUnitCost As System.Windows.Forms.Label
  Friend WithEvents txtLineNumber As System.Windows.Forms.TextBox
  Friend WithEvents lblLineNumber As System.Windows.Forms.Label
  Friend WithEvents txtLineTotal As System.Windows.Forms.TextBox
  Friend WithEvents lblLineTotal As System.Windows.Forms.Label
  Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
  Friend WithEvents lblTotalCost As System.Windows.Forms.Label
  Friend WithEvents txtProfit As System.Windows.Forms.TextBox
  Friend WithEvents lblProfit As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
