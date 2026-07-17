Public Class ctlccProduct
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsProduct.enmUpdateType) 
  Public Event evtAdd(ByVal vProduct As clsProduct) 
  Public Event evtBeforeUpdate(ByVal vProduct As clsProduct, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsProduct.enmUpdateType, ByVal vProduct As clsProduct) 
  Public Event evtBeforeDelete(ByVal vProduct As clsProduct, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vProductID As Long) 
  Public Event evtCancelledEdit(ByVal vProduct As clsProduct) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vProduct As clsProduct) 
  
  Public Event evtParentChosen(ByVal vParentName As clsProduct.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsProduct.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsProduct.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsProduct.enmParentProperty, ByVal vSelectedValue As Object) 
   
  Private WithEvents _Tooltip As New ToolTip 
  
  Private _LoadParameters As clsLoadParameters 
  Friend Property LoadParameters() As clsLoadParameters 
    Get 
      Return _LoadParameters 
    End Get 
    Set(value As clsLoadParameters) 
      _LoadParameters = value 
    End Set 
  End Property 
  
  Public Class clsLoadParameters 
    Public Property [ReadOnly]() As Boolean 
    Public Property EnableParentLinks As List(Of clsProduct.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsProduct.enmParentProperty) 
 
    End Sub 
  End Class 
 
  Private WithEvents _Product As clsProduct

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlProduct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.DoubleBuffered = True 
    If Me.DesignMode = True Then Exit Sub
    
    'buttons
    'btnUpdate.Location = btnEdit.Location
    'btnCancel.Location = btnAdd.Location
    'txtGrabFocus - to make sure topmost item has focus 
    If txtGrabFocus IsNot Nothing Then Return 
    Me.txtGrabFocus = New System.Windows.Forms.TextBox() 
    Me.txtGrabFocus.BorderStyle = System.Windows.Forms.BorderStyle.None 
    Me.txtGrabFocus.Location = New System.Drawing.Point(0, 0) 
    Me.txtGrabFocus.Name = "txtGrabFocus" 
    Me.txtGrabFocus.Size = New System.Drawing.Size(0, 13) 
    Me.txtGrabFocus.TabIndex = 0 
    Me.Controls.Add(Me.txtGrabFocus) 
 
    MakeControlRTL(Me) 
 
  End Sub

  Private Sub SetUpControls()
    'multiple control location
    cboCategory.Size = txtCategory.Size
    cboCategory.Location = txtCategory.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vProductID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pProduct As New clsProduct() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vProductID <> 0 Then 
      pFault = pProduct.GetByID(vProductID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pProduct) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rProduct As clsProduct, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rProduct)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rProduct As clsProduct) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _Product = rProduct 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboCategory.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboCategory() : If pFault.isOK = False Then Return pFault 
    End If 
    
    ControlsLoad()

    SetUpButtons(False)

    If txtGrabFocus IsNot Nothing Then txtGrabFocus.Focus() 

    RaiseEvent evtLoaded() 

    Return pFault.SetOK() 
  End Function

  Private Function LoadCbos() As clsFault 
    Dim pFault As New clsFault() 
 
    _Loading = True 
 
    'Lookups (in case of change)
 
    'Parents
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rProduct"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rProduct As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rProduct.GetType.Name = "clsProduct" Then 
      ctlProduct_Load(Nothing, Nothing) 
      Dim pProduct As clsProduct = CType(rProduct, clsProduct) 
      Return LoadControl(pProduct) 
    Else 
      Dim pProductID As Long = CType(rProduct, Long) 
      Return LoadControl(pProductID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "ProductCode", _Requester) 
    If pStrg <> "" Then lblProductCode.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "ProductName", _Requester) 
    If pStrg <> "" Then lblProductName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "Category", _Requester) 
    If pStrg <> "" Then lblCategory.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "UnitOfMeasure", _Requester) 
    If pStrg <> "" Then lblUnitOfMeasure.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "IsActive", _Requester) 
    If pStrg <> "" Then lblIsActive.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "CurrentStock", _Requester) 
    If pStrg <> "" Then lblCurrentStock.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Product", "BaseCost", _Requester) 
    If pStrg <> "" Then lblBaseCost.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [Product]() As clsProduct
    Get 
      Return _Product 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboCategory() As clsFault
    Dim pFault As New clsFault
 
    'If cboCategory.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pCategoryes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsProduct.enmParentProperty.Category, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pCategoryes.FillEnums(clsEnums.enmEnum.Category, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pCategoryes = pTestCol
    End If
    
    pCategoryes.Remove(pCategoryes.FindByKey(clsEnums.enmCategory.UD))
    pCategoryes.SortByText()
    pCategoryes.AddToTop(clsEnums.enmCategory.UD, GetChoose(_Requester))

    With cboCategory
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pCategoryes
    End With

    cboCategory.SelectedValue = _Product.Category 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmCategory = CType(cboCategory.SelectedValue, clsEnums.enmCategory) 
    RaiseEvent evtCboSelectedIndexChanged(clsProduct.enmParentProperty.Category, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsProduct.enmParentProperty = clsProduct.enmParentProperty.UD 
    
    'Load comboboxes 
    If vInEdit = True Then 
      Dim pFault As clsFault 
      pFault = LoadCbos() 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    End If 
 
    Dim pDefaultColour As System.Drawing.Color 
    Dim pReadonlyColour As System.Drawing.Color 
    pDefaultColour = System.Drawing.Color.White 
    If vInEdit = True Then 
      pReadonlyColour = System.Drawing.Color.PapayaWhip 
    Else 
      pReadonlyColour = pDefaultColour 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    txtProductCode.ReadOnly = Not (vInEdit)
    txtProductCode.BackColor = pDefaultColour 
    txtProductName.ReadOnly = Not (vInEdit)
    txtProductName.BackColor = pDefaultColour 
    txtCategory.ReadOnly = True
    txtCategory.Visible = Not (vInEdit)
    txtCategory.BackColor = pReadonlyColour 
    txtCategory.ForeColor = SetForeColor(vInEdit) 
    cboCategory.Visible = vInEdit
    txtUnitOfMeasure.ReadOnly = Not (vInEdit)
    txtUnitOfMeasure.BackColor = pDefaultColour 
    If vInEdit AndAlso _Product.UnitOfMeasure = "יחידה" Then txtUnitOfMeasure.Text = _Product.UnitOfMeasure.ToString() 
    txtNotes.ReadOnly = Not (vInEdit)
    txtNotes.BackColor = pDefaultColour 
    chkIsActive.Enabled = True 
    txtCurrentStock.ReadOnly = True 
    txtCurrentStock.BackColor = pReadonlyColour 
    txtCurrentStock.ForeColor = SetForeColor(vInEdit) 
    txtBaseCost.ReadOnly = Not (vInEdit)
    txtBaseCost.BackColor = pDefaultColour 
    If vInEdit AndAlso _Product.BaseCost = 0D Then txtBaseCost.Text = _Product.BaseCost.ToString() 

    If _LoadParameters.ReadOnly = False Then 
      If _ButtonsMoved = False Then 
        btnUpdate.Visible = True 
        btnCancel.Visible = True 
        btnEdit.Visible = True 
        btnAdd.Visible = True 
        btnDelete.Visible = True 
        btnDelete.Top = btnEdit.Top 
        _ButtonsMoved = True 
      End If 
      btnUpdate.Visible = vInEdit 
      btnCancel.Visible = vInEdit 
      btnUpdate.Top = btnEdit.Top 
      btnCancel.Top = btnEdit.Top 
      If _Product.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
    Else 
      btnUpdate.Visible = False 
      btnCancel.Visible = False 
      btnEdit.Visible = False 
      btnDelete.Visible = False 
      btnAdd.Visible = False 
    End If 
    
    'disable or enable any child grids 
    Dim ctrl As Control = Me.GetNextControl(Me, True) 
    Do Until ctrl Is Nothing 
      If ctrl.GetType.Name.StartsWith("ctl") AndAlso ctrl.GetType.Name.EndsWith("Col") Then 
        ctrl.Enabled = Not vInEdit 
      End If 
      ctrl = Me.GetNextControl(ctrl, True) 
    Loop 
 
    RaiseEvent evtControlsRefreshed(vInEdit, _Product) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _Product
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtProductCode.Text = .ProductCode.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtProductCode.MaxLength = 50 
      txtProductName.Text = .ProductName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtProductName.MaxLength = 255 
      cboCategory.SelectedValue = .Category
      txtCategory.Text = cboCategory.Text : If cboCategory.SelectedValue Is Nothing OrElse cboCategory.SelectedValue.ToString() = "UD" Then txtCategory.Text = ""    
      txtUnitOfMeasure.Text = .UnitOfMeasure.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtUnitOfMeasure.MaxLength = 20 
      txtUnitOfMeasure.Text = If(.UnitOfMeasure = "יחידה", "", .UnitOfMeasure.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)) 
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      chkIsActive.Checked = .IsActive
      txtCurrentStock.Text = .CurrentStock.ToString(FormatFromTag(txtCurrentStock, "#,##0"))
      txtCurrentStock.Text = If(.CurrentStock = 0, "", .CurrentStock.ToString(FormatFromTag(txtCurrentStock, "#,##0"))) 
      txtBaseCost.Text = .BaseCost.ToString(FormatFromTag(txtBaseCost, "#,##0.00"))
      txtBaseCost.Text = If(.BaseCost = 0D, "", .BaseCost.ToString(FormatFromTag(txtBaseCost, "#,##0.00"))) 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _Product
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-Product-ID-090417-0012", _Requester) : Return pFault 
      .ProductCode = txtProductCode.Text 
      .ProductName = txtProductName.Text 
      .Category = CType(cboCategory.SelectedValue, clsEnums.enmCategory)
      .UnitOfMeasure = txtUnitOfMeasure.Text 
      .Notes = txtNotes.Text 
      If Decimal.TryParse(txtBaseCost.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .BaseCost) = False Then pFault.LogFreeTextFault(208, ".BaseCost", txtBaseCost.Text, "TRGT-Product-BaseCost-090417-0016", _Requester) : Return pFault 
    End With
    pFault.SetOK() 
    Return pFault 
  End Function
  
  'Handle one way encrypted textboxes
  
  'check control data validity 
  Private Sub txtID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtID.Text 
    Dim pTest As Long 
 
    If txtID.Text = "" Then Exit Sub 
    If txtID.Text = txtID.Name Then Exit Sub 
 
    If Long.TryParse(txtID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-Product-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtBaseCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBaseCost.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtBaseCost.Text 
    Dim pTest As Decimal 
 
    If txtBaseCost.Text = "" Then Exit Sub 
    If txtBaseCost.Text = txtBaseCost.Name Then Exit Sub 
 
    If Decimal.TryParse(txtBaseCost.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-Product-BaseCost-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsProduct.enmUpdateType.Standard) 
    Me.Refresh() 
    txtGrabFocus.Focus() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    Dim pFault As New clsFault 
    Try 
      pFault = ControlsRead() 
    Catch ex As Exception 
      pFault.LogException(ex, "", "TRGT-Product-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_Product, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _Product.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the Product collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccProductDefaultByID) 
      RaiseEvent evtUpdated(clsProduct.enmUpdateType.Standard, _Product) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_Product_evtAfterUpdate 
  Private Sub _Product_evtAfterUpdate() Handles _Product.evtAfterUpdate, _Product.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_Product) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _Product = New clsProduct() 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_Product) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_Product, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _Product.ProductCode & " - " & _Product.ProductName & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _Product.ID 
    pFault = _Product.Delete(_Requester) 
    If pFault.isOK = True Then 
      _Product = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only
  Private Sub chkIsActive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsActive.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkIsActive.Checked = _Product.IsActive
    End If
  End Sub

  'Now the Parents
  'SeparateUpdates 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  Private Sub ctlccProduct_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the Product to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pProduct As clsProduct = _Product 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pProduct.ToCSV) 
        Else 
          Clipboard.SetText(pProduct.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The Product is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub txtID_GotFocus(sender As Object, e As EventArgs) Handles txtID.GotFocus 
    'this is done so that the form *always* loads on with (0,0) visible. txtGrabFocus can be focused during the 1st load, since it wasn't created yet.... 
    Static sDone As Boolean = False 
    If sDone = False Then 
      txtGrabFocus.Focus() 
      sDone = True 
    End If 
  End Sub 
   
  'Handle screen 
  Private Sub HandleUplViewText(vFieldText As String, vUplButton As Button, vEnableUpload As Boolean, Optional vButtonTextHint As String = "") 
 
    If Not String.IsNullOrEmpty(vFieldText) Then 
      vUplButton.Text = CCTextTranslate("View", _Requester) 
      _Tooltip.SetToolTip(vUplButton, CCTextTranslate($"Click to view - right click To delete", _Requester)) 
      vUplButton.Enabled = True 
    Else 
      If vEnableUpload Then 
        vUplButton.Text = CCTextTranslate("Upload", _Requester) 
        _Tooltip.SetToolTip(vUplButton, "") 
      Else 
        vUplButton.Text = "" 
        _Tooltip.SetToolTip(vUplButton, "") 
      End If 
      vUplButton.Enabled = vEnableUpload 
    End If 
 
    If Not String.IsNullOrEmpty(vButtonTextHint) Then 
      vUplButton.Text = vButtonTextHint & " " & vUplButton.Text 
    End If 
 
  End Sub 
 
  Private Sub ctlccProduct_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlProduct_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
