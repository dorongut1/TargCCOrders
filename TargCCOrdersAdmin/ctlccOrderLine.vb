Public Class ctlccOrderLine
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsOrderLine.enmUpdateType) 
  Public Event evtAdd(ByVal vOrderLine As clsOrderLine) 
  Public Event evtBeforeUpdate(ByVal vOrderLine As clsOrderLine, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsOrderLine.enmUpdateType, ByVal vOrderLine As clsOrderLine) 
  Public Event evtBeforeDelete(ByVal vOrderLine As clsOrderLine, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vOrderLineID As Long) 
  Public Event evtCancelledEdit(ByVal vOrderLine As clsOrderLine) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vOrderLine As clsOrderLine) 
  
  Public Event evtParentChosen(ByVal vParentName As clsOrderLine.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsOrderLine.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsOrderLine.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsOrderLine.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsOrderLine.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsOrderLine.enmParentProperty) 
      _EnableParentLinks.Add(clsOrderLine.enmParentProperty.OrderHeader) 
      _EnableParentLinks.Add(clsOrderLine.enmParentProperty.Product) 
 
    End Sub 
  End Class 
 
  Private WithEvents _OrderLine As clsOrderLine

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlOrderLine_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboOrderHeader.Size = txtOrderHeader.Size
    cboOrderHeader.Location = txtOrderHeader.Location
    cboProduct.Size = txtProduct.Size
    cboProduct.Location = txtProduct.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vOrderLineID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pOrderLine As New clsOrderLine(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vOrderLineID <> 0 Then 
      pFault = pOrderLine.GetByID(vOrderLineID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pOrderLine) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rOrderLine As clsOrderLine, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rOrderLine)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rOrderLine As clsOrderLine) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _OrderLine = rOrderLine 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    'Combos
    'Set comboListsCache 
    MyCache.SetLevel(clsEnums.enmComboListType.ccOrderHeaderDefaultByID, Cache.enmLevel.Previous) 
    MyCache.SetLevel(clsEnums.enmComboListType.ccProductDefaultByID, Cache.enmLevel.Previous) 
    
    'Lookup Combos
    'EnumCombos
    
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
    pFault = LoadCboOrderHeader() : If pFault.isOK = False Then Return pFault 
    pFault = LoadCboProduct() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rOrderLine"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rOrderLine As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rOrderLine.GetType.Name = "clsOrderLine" Then 
      ctlOrderLine_Load(Nothing, Nothing) 
      Dim pOrderLine As clsOrderLine = CType(rOrderLine, clsOrderLine) 
      Return LoadControl(pOrderLine) 
    Else 
      Dim pOrderLineID As Long = CType(rOrderLine, Long) 
      Return LoadControl(pOrderLineID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "OrderHeader", _Requester) 
    If pStrg <> "" Then lblOrderHeader.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "Product", _Requester) 
    If pStrg <> "" Then lblProduct.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "Quantity", _Requester) 
    If pStrg <> "" Then lblQuantity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "UnitPrice", _Requester) 
    If pStrg <> "" Then lblUnitPrice.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "DiscountPercent", _Requester) 
    If pStrg <> "" Then lblDiscountPercent.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "UnitCost", _Requester) 
    If pStrg <> "" Then lblUnitCost.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "LineNumber", _Requester) 
    If pStrg <> "" Then lblLineNumber.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "LineTotal", _Requester) 
    If pStrg <> "" Then lblLineTotal.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "TotalCost", _Requester) 
    If pStrg <> "" Then lblTotalCost.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderLine", "Profit", _Requester) 
    If pStrg <> "" Then lblProfit.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [OrderLine]() As clsOrderLine
    Get 
      Return _OrderLine 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboOrderHeader() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccOrderHeaderDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(clsOrderLine.enmParentProperty.OrderHeader, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboOrderHeader.MakeSmart() Else cboOrderHeader.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboOrderHeader.LoadControl(pComboList, pPrompt) 
    Else 
      cboOrderHeader.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _OrderLine.OrderHeaderID > 0 Then cboOrderHeader.ValueSelect(_OrderLine.OrderHeaderID) Else cboOrderHeader.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboProduct() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccProductDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(clsOrderLine.enmParentProperty.Product, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
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
    
    If _OrderLine.ProductID > 0 Then cboProduct.ValueSelect(_OrderLine.ProductID) Else cboProduct.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboOrderHeader_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboOrderHeader.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsOrderLine.enmParentProperty.OrderHeader, pUniqueCode) 
  End Sub 
  Private Sub cboProduct_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboProduct.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsOrderLine.enmParentProperty.Product, pUniqueCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsOrderLine.enmParentProperty = clsOrderLine.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderLine.enmParentProperty.OrderHeader) = clsOrderLine.enmParentProperty.OrderHeader Then 
      lblOrderHeader.ForeColor = Color.Brown 
    End If 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderLine.enmParentProperty.Product) = clsOrderLine.enmParentProperty.Product Then 
      lblProduct.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtOrderHeader.ReadOnly = True
      txtOrderHeader.Visible = True
      txtOrderHeader.BackColor = pReadonlyColour
      txtOrderHeader.ForeColor = SetForeColor(vInEdit) 
      cboOrderHeader.Visible = False 
    Else 
      txtOrderHeader.ReadOnly = True
      txtOrderHeader.Visible = Not (vInEdit)
      txtOrderHeader.BackColor = pReadonlyColour 
      txtOrderHeader.ForeColor = SetForeColor(vInEdit) 
      cboOrderHeader.Visible = vInEdit
    End If  
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
    txtQuantity.ReadOnly = Not (vInEdit)
    txtQuantity.BackColor = pDefaultColour 
    txtUnitPrice.ReadOnly = Not (vInEdit)
    txtUnitPrice.BackColor = pDefaultColour 
    txtDiscountPercent.ReadOnly = Not (vInEdit)
    txtDiscountPercent.BackColor = pDefaultColour 
    If vInEdit AndAlso _OrderLine.DiscountPercent = 0D Then txtDiscountPercent.Text = _OrderLine.DiscountPercent.ToString() 
    txtUnitCost.ReadOnly = True 
    txtUnitCost.BackColor = pReadonlyColour 
    txtUnitCost.ForeColor = SetForeColor(vInEdit) 
    txtLineNumber.ReadOnly = Not (vInEdit)
    txtLineNumber.BackColor = pDefaultColour 
    txtLineTotal.ReadOnly = True 
    txtLineTotal.BackColor = pReadonlyColour 
    txtLineTotal.ForeColor = SetForeColor(vInEdit) 
    txtTotalCost.ReadOnly = True 
    txtTotalCost.BackColor = pReadonlyColour 
    txtTotalCost.ForeColor = SetForeColor(vInEdit) 
    txtProfit.ReadOnly = True 
    txtProfit.BackColor = pReadonlyColour 
    txtProfit.ForeColor = SetForeColor(vInEdit) 

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
      If _OrderLine.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_OrderLineUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_OrderLineDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_OrderLineUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _OrderLine) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _OrderLine
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtOrderHeader.Text = .OrderHeaderText 
      txtProduct.Text = .ProductText 
      txtQuantity.Text = .Quantity.ToString(FormatFromTag(txtQuantity, "#,##0"))
      txtUnitPrice.Text = .UnitPrice.ToString(FormatFromTag(txtUnitPrice, "#,##0.00"))
      txtDiscountPercent.Text = .DiscountPercent.ToString(FormatFromTag(txtDiscountPercent, "#,##0.00"))
      txtDiscountPercent.Text = If(.DiscountPercent = 0D, "", .DiscountPercent.ToString(FormatFromTag(txtDiscountPercent, "#,##0.00"))) 
      txtUnitCost.Text = .UnitCost.ToString(FormatFromTag(txtUnitCost, "#,##0.00"))
      txtUnitCost.Text = If(.UnitCost = 0D, "", .UnitCost.ToString(FormatFromTag(txtUnitCost, "#,##0.00"))) 
      txtLineNumber.Text = .LineNumber.ToString(FormatFromTag(txtLineNumber, "#,##0"))
      txtLineTotal.Text = .LineTotal.ToString(FormatFromTag(txtLineTotal, "#,##0.00"))
      txtTotalCost.Text = .TotalCost.ToString(FormatFromTag(txtTotalCost, "#,##0.00"))
      txtProfit.Text = .Profit.ToString(FormatFromTag(txtProfit, "#,##0.00"))
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _OrderLine
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-OrderLine-ID-090417-0012", _Requester) : Return pFault 
      If cboOrderHeader.SelectedItem Is Nothing OrElse cboOrderHeader.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .OrderHeaderID = 0 
      Else 
        Dim pOrderHeaderID As Long = CType(cboOrderHeader.SelectedItem, clsComboListMember).KeyLong 
        If pOrderHeaderID = -1 Then .OrderHeaderID = 0 Else .OrderHeaderID = pOrderHeaderID 
      End If 
      If cboProduct.SelectedItem Is Nothing OrElse cboProduct.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .ProductID = 0 
      Else 
        Dim pProductID As Long = CType(cboProduct.SelectedItem, clsComboListMember).KeyLong 
        If pProductID = -1 Then .ProductID = 0 Else .ProductID = pProductID 
      End If 
      If Integer.TryParse(txtQuantity.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .Quantity) = False Then pFault.LogFreeTextFault(208, ".Quantity", txtQuantity.Text, "TRGT-OrderLine-Quantity-090417-0013", _Requester) : Return pFault 
      If Decimal.TryParse(txtUnitPrice.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .UnitPrice) = False Then pFault.LogFreeTextFault(208, ".UnitPrice", txtUnitPrice.Text, "TRGT-OrderLine-UnitPrice-090417-0016", _Requester) : Return pFault 
      If Decimal.TryParse(txtDiscountPercent.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .DiscountPercent) = False Then pFault.LogFreeTextFault(208, ".DiscountPercent", txtDiscountPercent.Text, "TRGT-OrderLine-DiscountPercent-090417-0016", _Requester) : Return pFault 
      If Integer.TryParse(txtLineNumber.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .LineNumber) = False Then pFault.LogFreeTextFault(208, ".LineNumber", txtLineNumber.Text, "TRGT-OrderLine-LineNumber-090417-0013", _Requester) : Return pFault 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-OrderLine-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtQuantity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQuantity.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtQuantity.Text 
    Dim pTest As Integer 
 
    If txtQuantity.Text = "" Then Exit Sub 
    If txtQuantity.Text = txtQuantity.Name Then Exit Sub 
 
    If Integer.TryParse(txtQuantity.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-OrderLine-Quantity-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtUnitPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnitPrice.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtUnitPrice.Text 
    Dim pTest As Decimal 
 
    If txtUnitPrice.Text = "" Then Exit Sub 
    If txtUnitPrice.Text = txtUnitPrice.Name Then Exit Sub 
 
    If Decimal.TryParse(txtUnitPrice.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-OrderLine-UnitPrice-100907-1302", _Requester) 
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
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-OrderLine-DiscountPercent-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtLineNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLineNumber.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtLineNumber.Text 
    Dim pTest As Integer 
 
    If txtLineNumber.Text = "" Then Exit Sub 
    If txtLineNumber.Text = txtLineNumber.Name Then Exit Sub 
 
    If Integer.TryParse(txtLineNumber.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-OrderLine-LineNumber-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsOrderLine.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-OrderLine-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_OrderLine, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _OrderLine.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the OrderLine collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccOrderLineDefaultByID) 
      RaiseEvent evtUpdated(clsOrderLine.enmUpdateType.Standard, _OrderLine) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_OrderLine_evtAfterUpdate 
  Private Sub _OrderLine_evtAfterUpdate() Handles _OrderLine.evtAfterUpdate, _OrderLine.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_OrderLine) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _OrderLine = New clsOrderLine(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_OrderLine) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_OrderLine, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _OrderLine.ProductID.ToString() & " - Qty: " & _OrderLine.Quantity.ToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _OrderLine.ID 
    pFault = _OrderLine.Delete(_Requester) 
    If pFault.isOK = True Then 
      _OrderLine = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblOrderHeader_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.DoubleClick 
    If _OrderLine.OrderHeaderID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderLine.enmParentProperty.OrderHeader) = clsOrderLine.enmParentProperty.OrderHeader Then 
      If _OrderLine.OrderHeaderID <> 0 Then RaiseEvent evtParentChosen(clsOrderLine.enmParentProperty.OrderHeader, _OrderLine.OrderHeaderID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "OrderHeader Detail" 
      fPopup.LoadControl("ctlccOrderHeader", _OrderLine.OrderHeaderID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblOrderHeader_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderLine.enmParentProperty.OrderHeader) <> clsOrderLine.enmParentProperty.OrderHeader Then Exit Sub 
    lblOrderHeader.ForeColor = Color.Brown 
    'lblOrderHeader.Font = New Font(lblOrderHeader.Font.Name, lblOrderHeader.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblOrderHeader.BackColor = ccHelper.InvertColour(lblOrderHeader.ForeColor) 'did this instead 
    lblOrderHeader.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblOrderHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderLine.enmParentProperty.OrderHeader) <> clsOrderLine.enmParentProperty.OrderHeader Then Exit Sub 
    lblOrderHeader.ForeColor = Color.Brown 
    'lblOrderHeader.Font = New Font(lblOrderHeader.Font.Name, lblOrderHeader.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblOrderHeader.BackColor = Me.BackColor 'did this instead 
    lblOrderHeader.Cursor = Cursors.Default 
  End Sub 
 
  Private Sub lblProduct_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProduct.DoubleClick 
    If _OrderLine.ProductID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderLine.enmParentProperty.Product) = clsOrderLine.enmParentProperty.Product Then 
      If _OrderLine.ProductID <> 0 Then RaiseEvent evtParentChosen(clsOrderLine.enmParentProperty.Product, _OrderLine.ProductID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "Product Detail" 
      fPopup.LoadControl("ctlccProduct", _OrderLine.ProductID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblProduct_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProduct.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderLine.enmParentProperty.Product) <> clsOrderLine.enmParentProperty.Product Then Exit Sub 
    lblProduct.ForeColor = Color.Brown 
    'lblProduct.Font = New Font(lblProduct.Font.Name, lblProduct.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblProduct.BackColor = ccHelper.InvertColour(lblProduct.ForeColor) 'did this instead 
    lblProduct.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblProduct_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProduct.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderLine.enmParentProperty.Product) <> clsOrderLine.enmParentProperty.Product Then Exit Sub 
    lblProduct.ForeColor = Color.Brown 
    'lblProduct.Font = New Font(lblProduct.Font.Name, lblProduct.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblProduct.BackColor = Me.BackColor 'did this instead 
    lblProduct.Cursor = Cursors.Default 
  End Sub 
 
  'SeparateUpdates 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  Private Sub ctlccOrderLine_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the OrderLine to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pOrderLine As clsOrderLine = _OrderLine 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pOrderLine.ToCSV) 
        Else 
          Clipboard.SetText(pOrderLine.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The OrderLine is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccOrderLine_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlOrderLine_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
