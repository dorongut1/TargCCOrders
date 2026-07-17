Public Class ctlccProductPrice
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsProductPrice.enmUpdateType) 
  Public Event evtAdd(ByVal vProductPrice As clsProductPrice) 
  Public Event evtBeforeUpdate(ByVal vProductPrice As clsProductPrice, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsProductPrice.enmUpdateType, ByVal vProductPrice As clsProductPrice) 
  Public Event evtBeforeDelete(ByVal vProductPrice As clsProductPrice, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vProductPriceID As Long) 
  Public Event evtCancelledEdit(ByVal vProductPrice As clsProductPrice) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vProductPrice As clsProductPrice) 
  
  Public Event evtParentChosen(ByVal vParentName As clsProductPrice.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsProductPrice.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsProductPrice.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsProductPrice.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsProductPrice.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsProductPrice.enmParentProperty) 
      _EnableParentLinks.Add(clsProductPrice.enmParentProperty.Product) 
 
    End Sub 
  End Class 
 
  Private WithEvents _ProductPrice As clsProductPrice

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlProductPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboProduct.Size = txtProduct.Size
    cboProduct.Location = txtProduct.Location
    cboCustomerType.Size = txtCustomerType.Size
    cboCustomerType.Location = txtCustomerType.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vProductPriceID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pProductPrice As New clsProductPrice(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vProductPriceID <> 0 Then 
      pFault = pProductPrice.GetByID(vProductPriceID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pProductPrice) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rProductPrice As clsProductPrice, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rProductPrice)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rProductPrice As clsProductPrice) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _ProductPrice = rProductPrice 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboCustomerType.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.ccProductDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboCustomerType() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboProduct() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rProductPrice"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rProductPrice As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rProductPrice.GetType.Name = "clsProductPrice" Then 
      ctlProductPrice_Load(Nothing, Nothing) 
      Dim pProductPrice As clsProductPrice = CType(rProductPrice, clsProductPrice) 
      Return LoadControl(pProductPrice) 
    Else 
      Dim pProductPriceID As Long = CType(rProductPrice, Long) 
      Return LoadControl(pProductPriceID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPrice", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPrice", "Product", _Requester) 
    If pStrg <> "" Then lblProduct.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPrice", "CustomerType", _Requester) 
    If pStrg <> "" Then lblCustomerType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPrice", "SellingPrice", _Requester) 
    If pStrg <> "" Then lblSellingPrice.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPrice", "MinQuantity", _Requester) 
    If pStrg <> "" Then lblMinQuantity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPrice", "DiscountPercent", _Requester) 
    If pStrg <> "" Then lblDiscountPercent.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPrice", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [ProductPrice]() As clsProductPrice
    Get 
      Return _ProductPrice 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboProduct() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccProductDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(clsProductPrice.enmParentProperty.Product, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboProduct.MakeSmart() Else cboProduct.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboProduct.LoadControl(pComboList, pPrompt) 
    Else 
      cboProduct.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _ProductPrice.ProductID > 0 Then cboProduct.ValueSelect(_ProductPrice.ProductID) Else cboProduct.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboCustomerType() As clsFault
    Dim pFault As New clsFault
 
    'If cboCustomerType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pCustomerTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsProductPrice.enmParentProperty.CustomerType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pCustomerTypees.FillEnums(clsEnums.enmEnum.CustomerType, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pCustomerTypees = pTestCol
    End If
    
    pCustomerTypees.Remove(pCustomerTypees.FindByKey(clsEnums.enmCustomerType.UD))
    pCustomerTypees.SortByText()
    pCustomerTypees.AddToTop(clsEnums.enmCustomerType.UD, GetChoose(_Requester))

    With cboCustomerType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pCustomerTypees
    End With

    cboCustomerType.SelectedValue = _ProductPrice.CustomerType 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboProduct_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboProduct.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsProductPrice.enmParentProperty.Product, pUniqueCode) 
  End Sub 
  Private Sub cboCustomerType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustomerType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmCustomerType = CType(cboCustomerType.SelectedValue, clsEnums.enmCustomerType) 
    RaiseEvent evtCboSelectedIndexChanged(clsProductPrice.enmParentProperty.CustomerType, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsProductPrice.enmParentProperty = clsProductPrice.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsProductPrice.enmParentProperty.Product) = clsProductPrice.enmParentProperty.Product Then 
      lblProduct.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtProduct.ReadOnly = True
      txtProduct.Visible = True
      txtProduct.BackColor = pReadonlyColour
      txtProduct.ForeColor = SetForeColor(vInEdit) 
      cboProduct.Visible = False 
    Else 
      txtProduct.ReadOnly = True
      txtProduct.Visible = Not (vInEdit)
      txtProduct.BackColor = pReadonlyColour 
      txtProduct.ForeColor = SetForeColor(vInEdit) 
      cboProduct.Visible = vInEdit
    End If  
    txtCustomerType.ReadOnly = True
    txtCustomerType.Visible = Not (vInEdit)
    txtCustomerType.BackColor = pReadonlyColour 
    txtCustomerType.ForeColor = SetForeColor(vInEdit) 
    cboCustomerType.Visible = vInEdit
    txtSellingPrice.ReadOnly = Not (vInEdit)
    txtSellingPrice.BackColor = pDefaultColour 
    txtMinQuantity.ReadOnly = Not (vInEdit)
    txtMinQuantity.BackColor = pDefaultColour 
    If vInEdit AndAlso _ProductPrice.MinQuantity = 1 Then txtMinQuantity.Text = _ProductPrice.MinQuantity.ToString() 
    txtDiscountPercent.ReadOnly = Not (vInEdit)
    txtDiscountPercent.BackColor = pDefaultColour 
    If vInEdit AndAlso _ProductPrice.DiscountPercent = 0D Then txtDiscountPercent.Text = _ProductPrice.DiscountPercent.ToString() 
    txtNotes.ReadOnly = Not (vInEdit)
    txtNotes.BackColor = pDefaultColour 

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
      If _ProductPrice.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _ProductPrice) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _ProductPrice
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtProduct.Text = .ProductText 
      cboCustomerType.SelectedValue = .CustomerType
      txtCustomerType.Text = cboCustomerType.Text : If cboCustomerType.SelectedValue Is Nothing OrElse cboCustomerType.SelectedValue.ToString() = "UD" Then txtCustomerType.Text = ""    
      txtSellingPrice.Text = .SellingPrice.ToString(FormatFromTag(txtSellingPrice, "#,##0.00"))
      txtMinQuantity.Text = .MinQuantity.ToString(FormatFromTag(txtMinQuantity, "#,##0"))
      txtMinQuantity.Text = If(.MinQuantity = 1, "", .MinQuantity.ToString(FormatFromTag(txtMinQuantity, "#,##0"))) 
      txtDiscountPercent.Text = .DiscountPercent.ToString(FormatFromTag(txtDiscountPercent, "#,##0.00"))
      txtDiscountPercent.Text = If(.DiscountPercent = 0D, "", .DiscountPercent.ToString(FormatFromTag(txtDiscountPercent, "#,##0.00"))) 
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _ProductPrice
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-ProductPrice-ID-090417-0012", _Requester) : Return pFault 
      If cboProduct.SelectedItem Is Nothing OrElse cboProduct.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .ProductID = 0 
      Else 
        Dim pProductID As Long = CType(cboProduct.SelectedItem, clsComboListMember).KeyLong 
        If pProductID = -1 Then .ProductID = 0 Else .ProductID = pProductID 
      End If 
      .CustomerType = CType(cboCustomerType.SelectedValue, clsEnums.enmCustomerType)
      If Decimal.TryParse(txtSellingPrice.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .SellingPrice) = False Then pFault.LogFreeTextFault(208, ".SellingPrice", txtSellingPrice.Text, "TRGT-ProductPrice-SellingPrice-090417-0016", _Requester) : Return pFault 
      If Integer.TryParse(txtMinQuantity.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .MinQuantity) = False Then pFault.LogFreeTextFault(208, ".MinQuantity", txtMinQuantity.Text, "TRGT-ProductPrice-MinQuantity-090417-0013", _Requester) : Return pFault 
      If Decimal.TryParse(txtDiscountPercent.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .DiscountPercent) = False Then pFault.LogFreeTextFault(208, ".DiscountPercent", txtDiscountPercent.Text, "TRGT-ProductPrice-DiscountPercent-090417-0016", _Requester) : Return pFault 
      .Notes = txtNotes.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-ProductPrice-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtSellingPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSellingPrice.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtSellingPrice.Text 
    Dim pTest As Decimal 
 
    If txtSellingPrice.Text = "" Then Exit Sub 
    If txtSellingPrice.Text = txtSellingPrice.Name Then Exit Sub 
 
    If Decimal.TryParse(txtSellingPrice.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-ProductPrice-SellingPrice-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtMinQuantity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMinQuantity.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtMinQuantity.Text 
    Dim pTest As Integer 
 
    If txtMinQuantity.Text = "" Then Exit Sub 
    If txtMinQuantity.Text = txtMinQuantity.Name Then Exit Sub 
 
    If Integer.TryParse(txtMinQuantity.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-ProductPrice-MinQuantity-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtDiscountPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscountPercent.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtDiscountPercent.Text 
    Dim pTest As Decimal 
 
    If txtDiscountPercent.Text = "" Then Exit Sub 
    If txtDiscountPercent.Text = txtDiscountPercent.Name Then Exit Sub 
 
    If Decimal.TryParse(txtDiscountPercent.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-ProductPrice-DiscountPercent-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsProductPrice.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-ProductPrice-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_ProductPrice, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _ProductPrice.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the ProductPrice collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccProductPriceDefaultByID) 
      RaiseEvent evtUpdated(clsProductPrice.enmUpdateType.Standard, _ProductPrice) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_ProductPrice_evtAfterUpdate 
  Private Sub _ProductPrice_evtAfterUpdate() Handles _ProductPrice.evtAfterUpdate, _ProductPrice.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_ProductPrice) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _ProductPrice = New clsProductPrice(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_ProductPrice) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_ProductPrice, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _ProductPrice.ProductID.ToString() & " - " & _ProductPrice.CustomerType.FastToString() & ": ?" & _ProductPrice.SellingPrice.ToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _ProductPrice.ID 
    pFault = _ProductPrice.Delete(_Requester) 
    If pFault.isOK = True Then 
      _ProductPrice = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblProduct_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProduct.DoubleClick 
    If _ProductPrice.ProductID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsProductPrice.enmParentProperty.Product) = clsProductPrice.enmParentProperty.Product Then 
      If _ProductPrice.ProductID <> 0 Then RaiseEvent evtParentChosen(clsProductPrice.enmParentProperty.Product, _ProductPrice.ProductID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "Product Detail" 
      fPopup.LoadControl("ctlccProduct", _ProductPrice.ProductID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblProduct_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProduct.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsProductPrice.enmParentProperty.Product) <> clsProductPrice.enmParentProperty.Product Then Exit Sub 
    lblProduct.ForeColor = Color.Brown 
    'lblProduct.Font = New Font(lblProduct.Font.Name, lblProduct.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblProduct.BackColor = ccHelper.InvertColour(lblProduct.ForeColor) 'did this instead 
    lblProduct.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblProduct_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProduct.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsProductPrice.enmParentProperty.Product) <> clsProductPrice.enmParentProperty.Product Then Exit Sub 
    lblProduct.ForeColor = Color.Brown 
    'lblProduct.Font = New Font(lblProduct.Font.Name, lblProduct.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblProduct.BackColor = Me.BackColor 'did this instead 
    lblProduct.Cursor = Cursors.Default 
  End Sub 
 
  'SeparateUpdates 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  Private Sub ctlccProductPrice_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the ProductPrice to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pProductPrice As clsProductPrice = _ProductPrice 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pProductPrice.ToCSV) 
        Else 
          Clipboard.SetText(pProductPrice.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The ProductPrice is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccProductPrice_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlProductPrice_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
