<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccProduct
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
    Me.txtProductCode = New System.Windows.Forms.TextBox()
    Me.lblProductCode = New System.Windows.Forms.Label()
    Me.txtProductName = New System.Windows.Forms.TextBox()
    Me.lblProductName = New System.Windows.Forms.Label()
    Me.cboCategory = New System.Windows.Forms.ComboBox()
    Me.txtCategory = New System.Windows.Forms.TextBox()
    Me.lblCategory = New System.Windows.Forms.Label()
    Me.txtUnitOfMeasure = New System.Windows.Forms.TextBox()
    Me.lblUnitOfMeasure = New System.Windows.Forms.Label()
    Me.txtNotes = New System.Windows.Forms.TextBox()
    Me.lblNotes = New System.Windows.Forms.Label()
    Me.chkIsActive = New System.Windows.Forms.CheckBox()
    Me.lblIsActive = New System.Windows.Forms.Label()
    Me.txtCurrentStock = New System.Windows.Forms.TextBox()
    Me.lblCurrentStock = New System.Windows.Forms.Label()
    Me.txtBaseCost = New System.Windows.Forms.TextBox()
    Me.lblBaseCost = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(171, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(399, 25)
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
    'DtxtProductCode
    '
    Me.txtProductCode.Location = New System.Drawing.Point(171, 57)
    Me.txtProductCode.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProductCode.Name = "txtProductCode"
    Me.txtProductCode.Size = New System.Drawing.Size(399, 25)
    Me.txtProductCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProductCode.TabIndex = 2
    Me.txtProductCode.Text = "txtProductCode"
    '
    'lblProductCode
    '
    Me.lblProductCode.AutoSize = True
    Me.lblProductCode.Location = New System.Drawing.Point(42, 60)
    Me.lblProductCode.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProductCode.Name = "lblProductCode"
    Me.lblProductCode.Size = New System.Drawing.Size(18, 13)
    Me.lblProductCode.TabIndex = 3
    Me.lblProductCode.Text = "Product Code"
    '
    'DtxtProductName
    '
    Me.txtProductName.Location = New System.Drawing.Point(171, 97)
    Me.txtProductName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProductName.Name = "txtProductName"
    Me.txtProductName.Size = New System.Drawing.Size(399, 105)
    Me.txtProductName.Multiline = True
    Me.txtProductName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtProductName.WordWrap = False 
    Me.txtProductName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProductName.TabIndex = 4
    Me.txtProductName.Text = "txtProductName"
    '
    'lblProductName
    '
    Me.lblProductName.AutoSize = True
    Me.lblProductName.Location = New System.Drawing.Point(42, 95)
    Me.lblProductName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProductName.Name = "lblProductName"
    Me.lblProductName.Size = New System.Drawing.Size(18, 13)
    Me.lblProductName.TabIndex = 5
    Me.lblProductName.Text = "Product Name"
    '
    'cboCategory
    '
    Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboCategory.FormattingEnabled = True
    Me.cboCategory.Location = New System.Drawing.Point(164, 211)
    Me.cboCategory.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboCategory.Name = "cboCategory"
    Me.cboCategory.Size = New System.Drawing.Size(349, 21)
    Me.cboCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboCategory.TabIndex = 6
    '
    'BtxtCategory
    '
    Me.txtCategory.Location = New System.Drawing.Point(171, 217)
    Me.txtCategory.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCategory.Name = "txtCategory"
    Me.txtCategory.Size = New System.Drawing.Size(399, 20)
    Me.txtCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCategory.TabIndex = 7
    Me.txtCategory.Text = "txtCategory"
    '
    'DtxtCategory
    '
    Me.txtCategory.Location = New System.Drawing.Point(171, 217)
    Me.txtCategory.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCategory.Name = "txtCategory"
    Me.txtCategory.Size = New System.Drawing.Size(399, 25)
    Me.txtCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCategory.TabIndex = 8
    Me.txtCategory.Text = "txtCategory"
    '
    'lblCategory
    '
    Me.lblCategory.AutoSize = True
    Me.lblCategory.Location = New System.Drawing.Point(42, 220)
    Me.lblCategory.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCategory.Name = "lblCategory"
    Me.lblCategory.Size = New System.Drawing.Size(18, 13)
    Me.lblCategory.TabIndex = 9
    Me.lblCategory.Text = "Category"
    '
    'DtxtUnitOfMeasure
    '
    Me.txtUnitOfMeasure.Location = New System.Drawing.Point(171, 257)
    Me.txtUnitOfMeasure.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUnitOfMeasure.Name = "txtUnitOfMeasure"
    Me.txtUnitOfMeasure.Size = New System.Drawing.Size(399, 25)
    Me.txtUnitOfMeasure.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUnitOfMeasure.TabIndex = 10
    Me.txtUnitOfMeasure.Text = "txtUnitOfMeasure"
    '
    'lblUnitOfMeasure
    '
    Me.lblUnitOfMeasure.AutoSize = True
    Me.lblUnitOfMeasure.Location = New System.Drawing.Point(42, 260)
    Me.lblUnitOfMeasure.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUnitOfMeasure.Name = "lblUnitOfMeasure"
    Me.lblUnitOfMeasure.Size = New System.Drawing.Size(18, 13)
    Me.lblUnitOfMeasure.TabIndex = 11
    Me.lblUnitOfMeasure.Text = "Unit Of Measure"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(171, 297)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(399, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 12
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 295)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 13
    Me.lblNotes.Text = "Notes"
    '
    'chkIsActive
    '
    Me.chkIsActive.AutoSize = True
    Me.chkIsActive.Location = New System.Drawing.Point(171, 423)
    Me.chkIsActive.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkIsActive.Name = "chkIsActive"
    Me.chkIsActive.Size = New System.Drawing.Size(15, 14)
    Me.chkIsActive.TabIndex = 14
    Me.chkIsActive.UseVisualStyleBackColor = True
    '
    'lblIsActive
    '
    Me.lblIsActive.AutoSize = True
    Me.lblIsActive.Location = New System.Drawing.Point(42, 418)
    Me.lblIsActive.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIsActive.Name = "lblIsActive"
    Me.lblIsActive.Size = New System.Drawing.Size(18, 13)
    Me.lblIsActive.TabIndex = 15
    Me.lblIsActive.Text = "Is Active"
    '
    'DtxtCurrentStock
    '
    Me.txtCurrentStock.Location = New System.Drawing.Point(171, 457)
    Me.txtCurrentStock.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCurrentStock.Name = "txtCurrentStock"
    Me.txtCurrentStock.Size = New System.Drawing.Size(399, 25)
    Me.txtCurrentStock.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCurrentStock.TabIndex = 16
    Me.txtCurrentStock.Text = "txtCurrentStock"
    '
    'lblCurrentStock
    '
    Me.lblCurrentStock.AutoSize = True
    Me.lblCurrentStock.Location = New System.Drawing.Point(42, 460)
    Me.lblCurrentStock.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCurrentStock.Name = "lblCurrentStock"
    Me.lblCurrentStock.Size = New System.Drawing.Size(18, 13)
    Me.lblCurrentStock.TabIndex = 17
    Me.lblCurrentStock.Text = "Current Stock"
    '
    'DtxtBaseCost
    '
    Me.txtBaseCost.Location = New System.Drawing.Point(171, 497)
    Me.txtBaseCost.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtBaseCost.Name = "txtBaseCost"
    Me.txtBaseCost.Size = New System.Drawing.Size(399, 25)
    Me.txtBaseCost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtBaseCost.TabIndex = 18
    Me.txtBaseCost.Text = "txtBaseCost"
    '
    'lblBaseCost
    '
    Me.lblBaseCost.AutoSize = True
    Me.lblBaseCost.Location = New System.Drawing.Point(42, 500)
    Me.lblBaseCost.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblBaseCost.Name = "lblBaseCost"
    Me.lblBaseCost.Size = New System.Drawing.Size(18, 13)
    Me.lblBaseCost.TabIndex = 19
    Me.lblBaseCost.Text = "Base Cost"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 577)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 20
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 577)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 21
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 577)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 22
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 565)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 23
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 565)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 24
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlProduct 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtProductCode)
    Me.Controls.Add(Me.lblProductCode)
    Me.Controls.Add(Me.txtProductName)
    Me.Controls.Add(Me.lblProductName)
    Me.Controls.Add(Me.cboCategory)
    Me.Controls.Add(Me.txtCategory)
    Me.Controls.Add(Me.txtCategory)
    Me.Controls.Add(Me.lblCategory)
    Me.Controls.Add(Me.txtUnitOfMeasure)
    Me.Controls.Add(Me.lblUnitOfMeasure)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.chkIsActive)
    Me.Controls.Add(Me.lblIsActive)
    Me.Controls.Add(Me.txtCurrentStock)
    Me.Controls.Add(Me.lblCurrentStock)
    Me.Controls.Add(Me.txtBaseCost)
    Me.Controls.Add(Me.lblBaseCost)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccProduct"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtProductCode As System.Windows.Forms.TextBox
  Friend WithEvents lblProductCode As System.Windows.Forms.Label
  Friend WithEvents txtProductName As System.Windows.Forms.TextBox
  Friend WithEvents lblProductName As System.Windows.Forms.Label
  Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
  Friend WithEvents txtCategory As System.Windows.Forms.TextBox
  Friend WithEvents lblCategory As System.Windows.Forms.Label
  Friend WithEvents txtUnitOfMeasure As System.Windows.Forms.TextBox
  Friend WithEvents lblUnitOfMeasure As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents chkIsActive As System.Windows.Forms.CheckBox
  Friend WithEvents lblIsActive As System.Windows.Forms.Label
  Friend WithEvents txtCurrentStock As System.Windows.Forms.TextBox
  Friend WithEvents lblCurrentStock As System.Windows.Forms.Label
  Friend WithEvents txtBaseCost As System.Windows.Forms.TextBox
  Friend WithEvents lblBaseCost As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
