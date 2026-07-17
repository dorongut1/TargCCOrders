Public Class ctlPnlccOrderHeader 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlOrderHeaderCol As ctlccOrderHeaderCol 
  Private WithEvents _ctlOrderHeader As ctlccOrderHeader 
  Private WithEvents _ctlCustomerDebtCol As ctlccCustomerDebtCol 
  Private WithEvents _ctlDeliveryCol As ctlccDeliveryCol 
  Private WithEvents _ctlOrderLineCol As ctlccOrderLineCol 
  Private WithEvents _ctlSupplierOrderCol As ctlccSupplierOrderCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _OrderHeaderID As Long 
 
  'The data holders 
  Private _OrderHeaderCol As clsOrderHeaderCol 
  Private _OrderHeader As clsOrderHeader 
  Private _CustomerDebtCol As clsCustomerDebtCol 
  Private _DeliveryCol As clsDeliveryCol 
  Private _OrderLineCol As clsOrderLineCol 
  Private _SupplierOrderCol As clsSupplierOrderCol 
 
  Private _ShowIntelligentCombo As Boolean 
  
  'Events 
  Public Event evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As System.Windows.Forms.ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) 
  ''' <summary> 
  ''' If you want to 'PageFromServer', i.e. you don't want to load all the items to the combolist, and want to query the database to get the handful of items at a time, 
  '''   then set the level in MyCache to PageFromServer in evtBeforeLoad. 
  ''' This is useful if the population of the combolist is huge.... 
  ''' If you want to force it to load all items, 
  '''   then set the level in MyCache to AlwaysCache. 
  ''' Otherwise, it will default to Auto (PageFromServer if more than 100 items) 
  ''' </summary> 
  ''' <param name="rComboListTypeToLoad"></param> 
  ''' <param name="rParentID"></param> 
  ''' <param name="rComboList"></param> 
  ''' <param name="rPrompt"></param> 
  ''' <param name="rMakeSmart"></param> 
  ''' <param name="rAddNewPrompt"></param> 
  Public Event evtOverrideLoadCboOrderHeader(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetOrderHeaderIDFromIntelliComboText(ByVal vIntelliComboText As String) 
  ''' <summary> 
  ''' If you want to 'PageFromServer', i.e. you don't want to load all the items to the combolist, and want to query the database to get the handful of items at a time,  
  '''   then set the level in MyCache to PageFromServer in evtBeforeLoad 
  ''' This is useful if the population of the combolist is huge....  
  ''' If you want to force it to load all items,  
  '''   then set the level in MyCache to AlwaysCache.  
  ''' Otherwise, it will default to Auto (PageFromServer if more than 100 items) 
  ''' </summary> 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  'Override Collection Fills 
  Public Event evtOverrideFillOrderHeaderCol(ByRef rOrderHeaderCol As clsOrderHeaderCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillCustomerDebtCol(ByRef rCustomerDebtCol As clsCustomerDebtCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillDeliveryCol(ByRef rDeliveryCol As clsDeliveryCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillOrderLineCol(ByRef rOrderLineCol As clsOrderLineCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillSupplierOrderCol(ByRef rSupplierOrderCol As clsSupplierOrderCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlOrderHeaderCol(ByRef rLoadParameters As ctlccOrderHeaderCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlOrderHeader(ByRef rLoadParameters As ctlccOrderHeader.clsLoadParameters) 
  Private Event evtOverrideLoadCtlCustomerDebtCol(ByRef rLoadParameters As ctlccCustomerDebtCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlDeliveryCol(ByRef rLoadParameters As ctlccDeliveryCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlOrderLineCol(ByRef rLoadParameters As ctlccOrderLineCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlSupplierOrderCol(ByRef rLoadParameters As ctlccSupplierOrderCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreOrderHeaderCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtOrderHeaderTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtCustomerDebtChosen As Boolean = False 
  Private _ShowPopForEvtCustomerDebtChosen As Boolean = False 
  Private _CancelEvtDeliveryChosen As Boolean = False 
  Private _ShowPopForEvtDeliveryChosen As Boolean = False 
  Private _CancelEvtOrderLineChosen As Boolean = False 
  Private _ShowPopForEvtOrderLineChosen As Boolean = False 
  Private _CancelEvtSupplierOrderChosen As Boolean = False 
  Private _ShowPopForEvtSupplierOrderChosen As Boolean = False 
  'Parents
  Private _CancelEvtCustomerChosen As Boolean = False 
  Private _ShowPopForEvtCustomerChosen As Boolean = False 
  
  Private WithEvents _Tooltip As ToolTip
  
  Private Sub ctl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 
    If Me.DesignMode = True Then Exit Sub 
    MakeControlRTL(Me) 
  
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
    Me.Visible = False 
 
    If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
    lblSecondaryTitle.Visible = False 
    btnFilter.Visible = False 
 
    lnkOrderHeaderCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkOrderHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkCustomerDebtCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkDeliveryCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkOrderLineCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkSupplierOrderCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vOrderHeaderID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _OrderHeaderID = CType(vOrderHeaderID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlOrderHeader.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkOrderHeaderCol.Visible = False 
    _ShowIntelligentCombo = True 
    chkGrid.Checked = False 
 
    Dim pIntelliComboMakeDumb As Boolean = False 
    Dim pIntelliComboDropDownStyle As System.Windows.Forms.ComboBoxStyle = ComboBoxStyle.DropDown 
    Dim pExitSubAfterEvent As Boolean = False 
    RaiseEvent evtOverrideLoadControl(pIntelliComboMakeDumb, pIntelliComboDropDownStyle, pExitSubAfterEvent) 
 
    If pIntelliComboMakeDumb = True Then MyIntelliCombo.MakeDumb() Else MyIntelliCombo.MakeSmart() 
    MyIntelliCombo.DropDownStyle = pIntelliComboDropDownStyle 
    If pExitSubAfterEvent = True Then 
      Me.Visible = True 
      pFault.SetOK() 
      Return pFault 
    End If 
  
    If MyIntelliCombo.IsLoaded = False Then 
      pFault = LoadCboOrderHeaders(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _OrderHeaderID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_OrderHeaderID) 
      End If 
      ChooseOrderHeader() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccOrderHeader") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _OrderHeaderID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _OrderHeaderID = -1 Then chkGrid.Checked = True 
 
    Me.Visible = True 
 
    RaiseEvent evtLoaded() 
 
    pFault.SetOK() 
    Return pFault 
  End Function 
 
  Private Function ActivateControl(ByVal pControlName As String) As clsFault 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    'Check if it's already open 
    For Each pL As Control In flpMenu.Controls 
      If (TypeOf (pL) Is Label) = False Then Continue For 
      Dim plbl As Label = CType(pL, Label) 
      plbl.ForeColor = Color.Black 
      plbl.BackColor = Color.White 
    Next 
 
    Dim pShowGrid As Boolean = chkGrid.Checked 
    btnFilter.Visible = False 
    
    Dim pPreviousControlName As String = _ActiveControl?.Name 
    
    If pControlName = "ctlccOrderHeader" OrElse pControlName = "ctlOrderHeader" Then 
      lnkOrderHeader.ForeColor = Color.Black : lnkOrderHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkOrderHeader.BackColor = Color.Wheat 
      If _ctlOrderHeader Is Nothing Then 
        _ctlOrderHeader = New ctlccOrderHeader() 
        _ctlOrderHeader.Dock = DockStyle.Fill 
        _ctlOrderHeader.Controls.RemoveByKey("btnAdd") 
        pnlOrderHeader.Controls.Add(_ctlOrderHeader) 
        _ctlOrderHeader.Visible = False 
      End If 
      If _OrderHeaderID = 0 Then 
        pnlOrderHeader.Visible = False 
      End If 
      'If _OrderHeader Is Nothing Then 
      pFault = RefreshCtlOrderHeader() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlOrderHeader.OrderHeader.IsEmpty AndAlso _OrderHeaderID <> -2 Then 
        pnlOrderHeader.Visible = False 
      End If 
      _ctlOrderHeader.Name = "ctlccOrderHeader" 
      _ActiveControl = _ctlOrderHeader 
      _ctlOrderHeader.BringToFront() 
      _ctlOrderHeader.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccOrderHeaderCol" Then 
      lnkOrderHeaderCol.ForeColor = Color.Black : lnkOrderHeaderCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkOrderHeaderCol.BackColor = Color.Wheat 
      If _ctlOrderHeaderCol Is Nothing Then 
        _ctlOrderHeaderCol = New ctlccOrderHeaderCol() 
        _ctlOrderHeaderCol.Dock = DockStyle.Fill 
        pnlOrderHeader.Controls.Add(_ctlOrderHeaderCol) 
        _ctlOrderHeaderCol.Visible = False 
      End If  
      pnlOrderHeader.Visible = True 
      If _OrderHeaderCol Is Nothing Then 
        pFault = RefreshCtlOrderHeaderCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlOrderHeaderCol.Name = "ctlccOrderHeaderCol" 
      _ActiveControl = _ctlOrderHeaderCol 
      _ctlOrderHeaderCol.BringToFront() 
      _ctlOrderHeaderCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlccCustomerDebtCol" Then 
      lnkCustomerDebtCol.ForeColor = Color.Black : lnkCustomerDebtCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkCustomerDebtCol.BackColor = Color.Wheat 
      If _ctlCustomerDebtCol Is Nothing Then 
      _ctlCustomerDebtCol = New ctlccCustomerDebtCol() 
      _ctlCustomerDebtCol.Dock = DockStyle.Fill 
      pnlOrderHeader.Controls.Add(_ctlCustomerDebtCol) 
      _ctlCustomerDebtCol.Visible = False 
      End If  
      If _CustomerDebtCol Is Nothing Then 
        pFault = RefreshCtlCustomerDebtCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlCustomerDebtCol.Name = "ctlccCustomerDebtCol" 
      _ActiveControl = _ctlCustomerDebtCol 
      _ctlCustomerDebtCol.BringToFront() 
      _ctlCustomerDebtCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccDeliveryCol" Then 
      lnkDeliveryCol.ForeColor = Color.Black : lnkDeliveryCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkDeliveryCol.BackColor = Color.Wheat 
      If _ctlDeliveryCol Is Nothing Then 
      _ctlDeliveryCol = New ctlccDeliveryCol() 
      _ctlDeliveryCol.Dock = DockStyle.Fill 
      pnlOrderHeader.Controls.Add(_ctlDeliveryCol) 
      _ctlDeliveryCol.Visible = False 
      End If  
      If _DeliveryCol Is Nothing Then 
        pFault = RefreshCtlDeliveryCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlDeliveryCol.Name = "ctlccDeliveryCol" 
      _ActiveControl = _ctlDeliveryCol 
      _ctlDeliveryCol.BringToFront() 
      _ctlDeliveryCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccOrderLineCol" Then 
      lnkOrderLineCol.ForeColor = Color.Black : lnkOrderLineCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkOrderLineCol.BackColor = Color.Wheat 
      If _ctlOrderLineCol Is Nothing Then 
      _ctlOrderLineCol = New ctlccOrderLineCol() 
      _ctlOrderLineCol.Dock = DockStyle.Fill 
      pnlOrderHeader.Controls.Add(_ctlOrderLineCol) 
      _ctlOrderLineCol.Visible = False 
      End If  
      If _OrderLineCol Is Nothing Then 
        pFault = RefreshCtlOrderLineCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlOrderLineCol.Name = "ctlccOrderLineCol" 
      _ActiveControl = _ctlOrderLineCol 
      _ctlOrderLineCol.BringToFront() 
      _ctlOrderLineCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccSupplierOrderCol" Then 
      lnkSupplierOrderCol.ForeColor = Color.Black : lnkSupplierOrderCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkSupplierOrderCol.BackColor = Color.Wheat 
      If _ctlSupplierOrderCol Is Nothing Then 
      _ctlSupplierOrderCol = New ctlccSupplierOrderCol() 
      _ctlSupplierOrderCol.Dock = DockStyle.Fill 
      pnlOrderHeader.Controls.Add(_ctlSupplierOrderCol) 
      _ctlSupplierOrderCol.Visible = False 
      End If  
      If _SupplierOrderCol Is Nothing Then 
        pFault = RefreshCtlSupplierOrderCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlSupplierOrderCol.Name = "ctlccSupplierOrderCol" 
      _ActiveControl = _ctlSupplierOrderCol 
      _ctlSupplierOrderCol.BringToFront() 
      _ctlSupplierOrderCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-OrderHeader-091229-1815", _Requester) 
    End If 
 
    If pPreviousControlName <> pControlName Then 
      pnlCover.BringToFront() 
      Application.DoEvents() 
    End If 
 
    Return pFault 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    btnFilter.Text = CCTextTranslate(btnFilter.Text, _Requester) 
    chkGrid.Text = CCTextTranslate(chkGrid.Text, _Requester) 
 
    btnRefresh.Text = CCTextTranslate(btnRefresh.Text, _Requester) 
 
    lblTitle.Text = TableNameTranslate("OrderHeader", _Requester) 
 
    lnkOrderHeaderCol.Text = CCTextTranslate("List", _Requester) 
    lnkOrderHeader.Text = CCTextTranslate("Details", _Requester) 
 
    lnkCustomerDebtCol.Text = TableNameTranslate("CustomerDebt", _Requester, vMakePlural:=True) 
    lnkDeliveryCol.Text = TableNameTranslate("Delivery", _Requester, vMakePlural:=True) 
    lnkOrderLineCol.Text = TableNameTranslate("OrderLine", _Requester, vMakePlural:=True) 
    lnkSupplierOrderCol.Text = TableNameTranslate("SupplierOrder", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlOrderHeader.Controls(0) Is _ctlOrderHeader Then 
      If _OrderHeaderID = 0 Then 
        pnlOrderHeader.Visible = False 
      End If 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlOrderHeaderCol Then 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlCustomerDebtCol Then 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlDeliveryCol Then 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlOrderLineCol Then 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlSupplierOrderCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pOrderHeaderID As Long = _OrderHeaderID 
      If ccHelper.IsNumeric(pText) Then _OrderHeaderID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetOrderHeaderIDFromIntelliComboText(pText) 
      If pOrderHeaderID <> _OrderHeaderID Then 
        _OrderHeader = Nothing 
        pFault = ActivateControl("ctlccOrderHeader") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlOrderHeader.Controls(0) Is _ctlOrderHeader Then 
      pFault = RefreshCtlOrderHeader() 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlOrderHeaderCol Then 
      pFault = RefreshCtlOrderHeaderCol() 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlCustomerDebtCol Then 
      pFault = RefreshCtlCustomerDebtCol() 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlDeliveryCol Then 
      pFault = RefreshCtlDeliveryCol() 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlOrderLineCol Then 
      pFault = RefreshCtlOrderLineCol() 
    ElseIf pnlOrderHeader.Controls(0) Is _ctlSupplierOrderCol Then 
      pFault = RefreshCtlSupplierOrderCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlOrderHeader.Controls(0).Name, "", "TRGT-OrderHeader-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboOrderHeaders(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlOrderHeaderCol_evtRowClicked(ByVal vOrderHeader As Object) Handles _ctlOrderHeaderCol.evtRowClicked 
    
    If vOrderHeader Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pOrderHeader As clsOrderHeader = CType(vOrderHeader, clsOrderHeader) 
    _OrderHeaderID = pOrderHeader.ID 
    
    If _ActiveControl Is _ctlOrderHeaderCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByCustomerID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByOrderDate.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByPaymentStatus.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByOrderStatus.ToString() Then 
          pInGroupBy = True 
          Exit For 
        End If 
      Next 
      If pInGroupBy = True Then Cursor = Cursors.Default : Return 
      
      btnFilter.Visible = True 
      lblSecondaryTitle.Visible = False 
    Else 
      btnFilter.Visible = False 
      lblSecondaryTitle.Visible = True 
    End If 
    
    ChooseOrderHeader() 
    
    Try 
      MyIntelliCombo.ValueSelect(_OrderHeaderID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pOrderHeader.OrderNumber.ToString("#,##0")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseOrderHeader() 
    _OrderHeader = Nothing 
    lnkOrderHeader.Visible = True 
    _CustomerDebtCol = Nothing 
    lnkCustomerDebtCol.Visible = True 
    _DeliveryCol = Nothing 
    lnkDeliveryCol.Visible = True 
    _OrderLineCol = Nothing 
    lnkOrderLineCol.Visible = True 
    _SupplierOrderCol = Nothing 
    lnkSupplierOrderCol.Visible = True 
  End Sub 
  Private Sub _ctlOrderHeaderCol_evtRowDoubleClicked(ByVal vOrderHeader As clsOrderHeader, ByRef rHandled As Boolean) Handles _ctlOrderHeaderCol.evtRowDoubleClicked 
    If lnkOrderHeader.Parent IsNot flpMenu Then Exit Sub 
    If vOrderHeader Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByCustomerID.ToString() Then 
        If pSearchFilters.ContainsKey(clsOrderHeaderCol.enmFillOnTheFlyParameters.CustomerID) Then pSearchFilters.Remove(clsOrderHeaderCol.enmFillOnTheFlyParameters.CustomerID) 
        pSearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.CustomerID, vOrderHeader.CustomerID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByOrderDate.ToString() Then 
        If pSearchFilters.ContainsKey(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderDateStart) Then pSearchFilters.Remove(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderDateStart) 
        If pSearchFilters.ContainsKey(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderDateEnd) Then pSearchFilters.Remove(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderDateEnd) 
        pSearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderDateStart, vOrderHeader.OrderDate) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByPaymentStatus.ToString() Then 
        If pSearchFilters.ContainsKey(clsOrderHeaderCol.enmFillOnTheFlyParameters.PaymentStatus) Then pSearchFilters.Remove(clsOrderHeaderCol.enmFillOnTheFlyParameters.PaymentStatus) 
        pSearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.PaymentStatus, vOrderHeader.PaymentStatus) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByOrderStatus.ToString() Then 
        If pSearchFilters.ContainsKey(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderStatus) Then pSearchFilters.Remove(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderStatus) 
        pSearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderStatus, vOrderHeader.OrderStatus) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreOrderHeaderCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vOrderHeader.ID, vOrderHeader.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _OrderHeaderID = vOrderHeader.ID 
      'MyIntelliCombo.ValueSelect(_OrderHeaderID) 
      pFault = ActivateControl("ctlccOrderHeader") 
      If pFault.isOK = False Then 
        Cursor = Cursors.Default 
        ShowFault(pFault, _Requester) 
      End If 
 
      _NestedFormsCount += 1 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      pnlCover.SendToBack() 
    Else 
      _SearchFilters = pSearchFilters 
      pFault = _OrderHeaderCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _OrderHeaderCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _OrderHeaderCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _OrderHeaderCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccOrderHeaderCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsOrderHeader.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see OrderHeader" 
      pFault = _ctlOrderHeaderCol.LoadControl(_OrderHeaderCol, pLoadParameters, _Requester) 
      If pFault.isOK = False Then 
        Cursor = Cursors.Default 
        ShowFault(pFault, _Requester) 
      Else 
        btnRefresh.Visible = True 
        Cursor = Cursors.Default 
        'frmMessageOrInputBox.ShowMsg(pFilters.ToString, frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub _ctlOrderHeaderCol_evtUnChosen() Handles _ctlOrderHeaderCol.evtUnChosen 
 
    _OrderHeaderID = 0 
    _OrderHeader = Nothing 
    _CustomerDebtCol = Nothing 
    lnkCustomerDebtCol.Visible = False 
    _DeliveryCol = Nothing 
    lnkDeliveryCol.Visible = False 
    _OrderLineCol = Nothing 
    lnkOrderLineCol.Visible = False 
    _SupplierOrderCol = Nothing 
    lnkSupplierOrderCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkOrderHeader.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkCustomerDebtCol.Click, 
      lnkDeliveryCol.Click, 
      lnkOrderLineCol.Click, 
      lnkSupplierOrderCol.Click, 
      lnkOrderHeaderCol.Click, 
      lnkOrderHeader.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkOrderHeader OrElse (lnk Is lnkOrderHeaderCol AndAlso chkGrid.Checked = True) Then 
      _NestedFormsCount = 0 
      If _NestedInMain = False Then 
        lblBack.Visible = False 
        chkGrid.Enabled = True 
      End If 
    Else 
      _NestedFormsCount += 1 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
    End If 
 
    pnlCover.SendToBack() 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
 
 
  'Refreshes 
  Private Function RefreshCtlOrderHeaderCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccOrderHeaderCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsOrderHeader.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsOrderHeaderCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillOrderHeaderCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _OrderHeaderCol = New clsOrderHeaderCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _OrderHeaderCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlOrderHeaderCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.Customer 
            pFault = _OrderHeaderCol.FillByCustomerID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=100, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then  
              _ctlOrderHeaderCol.Timer?.Stop()  
              Return pFault  
            End If  
          Case Else 
            If _ctlOrderHeaderCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _OrderHeaderCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlOrderHeaderCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _OrderHeaderCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _OrderHeaderCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _OrderHeaderCol.Count) 
      End If 
    Else 
      _OrderHeaderCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _OrderHeaderCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlOrderHeaderCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see OrderHeader" 
    
    Dim pOrderHeaderID As Long = _OrderHeaderID 
    
    pFault = _ctlOrderHeaderCol.LoadControl(_OrderHeaderCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlOrderHeaderCol.Visible = True 
    
    _ctlOrderHeaderCol.Refresh() 
    If pOrderHeaderID <> 0 Then 
      Dim pOrderHeaderCol As clsOrderHeaderCol = CType(_ctlOrderHeaderCol.bsCtlOrderHeader.DataSource, clsOrderHeaderCol) 
      Dim pOrderHeader As clsOrderHeader = pOrderHeaderCol.FindByID(pOrderHeaderID) 
      If pOrderHeader.ID > 0 Then 
        _ctlOrderHeaderCol.bsCtlOrderHeader.CurrencyManager.Position = pOrderHeaderCol.IndexOf(pOrderHeader) 
        _ctlOrderHeaderCol.dgvOrderHeader.Rows(pOrderHeaderCol.IndexOf(pOrderHeader)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlOrderHeader() As clsFault 
    Dim pFault As New clsFault 
    
    If _OrderHeaderID > 0 Then 
      ChooseOrderHeader() 
      _OrderHeader = New clsOrderHeader(clsEnums.enmLoadParent.TextOnly) 
      pFault = _OrderHeader.GetByID(_OrderHeaderID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _OrderHeader = New clsOrderHeader(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _OrderHeader.OrderNumber.ToString("#,##0")    
     
    Dim pLoadParameters As New ctlccOrderHeader.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlOrderHeader(pLoadParameters)
    pFault = _ctlOrderHeader.LoadControl(_OrderHeader, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlOrderHeader.Visible = True 
    If _OrderHeaderID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlOrderHeader.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlOrderHeader.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlCustomerDebtCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlCustomerDebtCol.dgvCustomerDebt.SelectedRows.Count > 0 Then 
      Dim pCustomerDebt As clsCustomerDebt = CType(_ctlCustomerDebtCol.bsCtlCustomerDebt.Current, clsCustomerDebt) 
      pID = pCustomerDebt.ID 
    End If 
 
    Dim pTestCol As clsCustomerDebtCol = Nothing 
    RaiseEvent evtOverrideFillCustomerDebtCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _CustomerDebtCol = New clsCustomerDebtCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _CustomerDebtCol.FillByOrderHeaderID(_OrderHeaderID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _CustomerDebtCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _CustomerDebtCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _CustomerDebtCol.Count) 
      End If 
    Else 
      _CustomerDebtCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _CustomerDebtCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlccCustomerDebtCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _OrderHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(_OrderHeader.DefaultDesignation) Then 
        .ReportTitle = "List of CustomerDebts for " & _OrderHeader.DefaultDesignation 
      Else 
        .ReportTitle = "List of CustomerDebts for OrderHeader" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsCustomerDebt.enmProperty.OrderHeader) 
    End With 
    RaiseEvent evtOverrideLoadCtlCustomerDebtCol(pLoadParameters)
    
    pFault = _ctlCustomerDebtCol.LoadControl(_CustomerDebtCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlCustomerDebtCol.Visible = True 
 
    If pID > 0 Then 
      Dim pCustomerDebts As clsCustomerDebtCol = CType(_ctlCustomerDebtCol.bsCtlCustomerDebt.DataSource, clsCustomerDebtCol) 
      Dim pCustomerDebt As clsCustomerDebt = pCustomerDebts.FindByID((pID)) 
      If pCustomerDebt.ID > 0 Then 
        _ctlCustomerDebtCol.bsCtlCustomerDebt.CurrencyManager.Position = pCustomerDebts.IndexOf(pCustomerDebt) 
        _ctlCustomerDebtCol.dgvCustomerDebt.Rows(pCustomerDebts.IndexOf(pCustomerDebt)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlDeliveryCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlDeliveryCol.dgvDelivery.SelectedRows.Count > 0 Then 
      Dim pDelivery As clsDelivery = CType(_ctlDeliveryCol.bsCtlDelivery.Current, clsDelivery) 
      pID = pDelivery.ID 
    End If 
 
    Dim pTestCol As clsDeliveryCol = Nothing 
    RaiseEvent evtOverrideFillDeliveryCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _DeliveryCol = New clsDeliveryCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _DeliveryCol.FillByOrderHeaderID(_OrderHeaderID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _DeliveryCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _DeliveryCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _DeliveryCol.Count) 
      End If 
    Else 
      _DeliveryCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _DeliveryCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlccDeliveryCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _OrderHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(_OrderHeader.DefaultDesignation) Then 
        .ReportTitle = "List of Deliverys for " & _OrderHeader.DefaultDesignation 
      Else 
        .ReportTitle = "List of Deliverys for OrderHeader" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsDelivery.enmProperty.OrderHeader) 
    End With 
    RaiseEvent evtOverrideLoadCtlDeliveryCol(pLoadParameters)
    
    pFault = _ctlDeliveryCol.LoadControl(_DeliveryCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlDeliveryCol.Visible = True 
 
    If pID > 0 Then 
      Dim pDeliverys As clsDeliveryCol = CType(_ctlDeliveryCol.bsCtlDelivery.DataSource, clsDeliveryCol) 
      Dim pDelivery As clsDelivery = pDeliverys.FindByID((pID)) 
      If pDelivery.ID > 0 Then 
        _ctlDeliveryCol.bsCtlDelivery.CurrencyManager.Position = pDeliverys.IndexOf(pDelivery) 
        _ctlDeliveryCol.dgvDelivery.Rows(pDeliverys.IndexOf(pDelivery)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlOrderLineCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlOrderLineCol.dgvOrderLine.SelectedRows.Count > 0 Then 
      Dim pOrderLine As clsOrderLine = CType(_ctlOrderLineCol.bsCtlOrderLine.Current, clsOrderLine) 
      pID = pOrderLine.ID 
    End If 
 
    Dim pTestCol As clsOrderLineCol = Nothing 
    RaiseEvent evtOverrideFillOrderLineCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _OrderLineCol = New clsOrderLineCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _OrderLineCol.FillByOrderHeaderID(_OrderHeaderID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _OrderLineCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _OrderLineCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _OrderLineCol.Count) 
      End If 
    Else 
      _OrderLineCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _OrderLineCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlccOrderLineCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _OrderHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(_OrderHeader.DefaultDesignation) Then 
        .ReportTitle = "List of OrderLines for " & _OrderHeader.DefaultDesignation 
      Else 
        .ReportTitle = "List of OrderLines for OrderHeader" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsOrderLine.enmProperty.OrderHeader) 
    End With 
    RaiseEvent evtOverrideLoadCtlOrderLineCol(pLoadParameters)
    
    pFault = _ctlOrderLineCol.LoadControl(_OrderLineCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlOrderLineCol.Visible = True 
 
    If pID > 0 Then 
      Dim pOrderLines As clsOrderLineCol = CType(_ctlOrderLineCol.bsCtlOrderLine.DataSource, clsOrderLineCol) 
      Dim pOrderLine As clsOrderLine = pOrderLines.FindByID((pID)) 
      If pOrderLine.ID > 0 Then 
        _ctlOrderLineCol.bsCtlOrderLine.CurrencyManager.Position = pOrderLines.IndexOf(pOrderLine) 
        _ctlOrderLineCol.dgvOrderLine.Rows(pOrderLines.IndexOf(pOrderLine)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlSupplierOrderCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlSupplierOrderCol.dgvSupplierOrder.SelectedRows.Count > 0 Then 
      Dim pSupplierOrder As clsSupplierOrder = CType(_ctlSupplierOrderCol.bsCtlSupplierOrder.Current, clsSupplierOrder) 
      pID = pSupplierOrder.ID 
    End If 
 
    Dim pTestCol As clsSupplierOrderCol = Nothing 
    RaiseEvent evtOverrideFillSupplierOrderCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _SupplierOrderCol = New clsSupplierOrderCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _SupplierOrderCol.FillByOrderHeaderID(_OrderHeaderID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _SupplierOrderCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _SupplierOrderCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _SupplierOrderCol.Count) 
      End If 
    Else 
      _SupplierOrderCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _SupplierOrderCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlccSupplierOrderCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _OrderHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(_OrderHeader.DefaultDesignation) Then 
        .ReportTitle = "List of SupplierOrders for " & _OrderHeader.DefaultDesignation 
      Else 
        .ReportTitle = "List of SupplierOrders for OrderHeader" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsSupplierOrder.enmProperty.OrderHeader) 
    End With 
    RaiseEvent evtOverrideLoadCtlSupplierOrderCol(pLoadParameters)
    
    pFault = _ctlSupplierOrderCol.LoadControl(_SupplierOrderCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlSupplierOrderCol.Visible = True 
 
    If pID > 0 Then 
      Dim pSupplierOrders As clsSupplierOrderCol = CType(_ctlSupplierOrderCol.bsCtlSupplierOrder.DataSource, clsSupplierOrderCol) 
      Dim pSupplierOrder As clsSupplierOrder = pSupplierOrders.FindByID((pID)) 
      If pSupplierOrder.ID > 0 Then 
        _ctlSupplierOrderCol.bsCtlSupplierOrder.CurrencyManager.Position = pSupplierOrders.IndexOf(pSupplierOrder) 
        _ctlSupplierOrderCol.dgvSupplierOrder.Rows(pSupplierOrders.IndexOf(pSupplierOrder)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlCustomerDebtCol_evtBeforeUpdate(ByVal vCustomerDebt As clsCustomerDebt, ByRef rCancel As Boolean) Handles _ctlCustomerDebtCol.evtBeforeUpdate 
    vCustomerDebt.OrderHeaderID = _OrderHeader.ID 
  End Sub 
  Private Sub _ctlDeliveryCol_evtBeforeUpdate(ByVal vDelivery As clsDelivery, ByRef rCancel As Boolean) Handles _ctlDeliveryCol.evtBeforeUpdate 
    vDelivery.OrderHeaderID = _OrderHeader.ID 
  End Sub 
  Private Sub _ctlOrderLineCol_evtBeforeUpdate(ByVal vOrderLine As clsOrderLine, ByRef rCancel As Boolean) Handles _ctlOrderLineCol.evtBeforeUpdate 
    vOrderLine.OrderHeaderID = _OrderHeader.ID 
  End Sub 
  Private Sub _ctlSupplierOrderCol_evtBeforeUpdate(ByVal vSupplierOrder As clsSupplierOrder, ByRef rCancel As Boolean) Handles _ctlSupplierOrderCol.evtBeforeUpdate 
    vSupplierOrder.OrderHeaderID = _OrderHeader.ID 
  End Sub 
  Private Sub _ctlOrderHeader_evtDeleted(ByVal vOrderHeaderID As Long) Handles _ctlOrderHeader.evtDeleted 
    _OrderHeaderCol = Nothing 
    Dim pFault As clsFault 
    _OrderHeaderID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboOrderHeaders(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlOrderHeader() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlOrderHeader.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkOrderHeaderCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlOrderHeader_evtCancelledEdit(ByVal vOrderHeader As clsOrderHeader) Handles _ctlOrderHeader.evtCancelledEdit 
    RefreshCtlOrderHeader() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboOrderHeaders(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlOrderHeader.btnAdd.Visible = False 
      If _OrderHeaderID = 0 OrElse _OrderHeaderID = -2 Then 
        pnlOrderHeader.Visible = False 
      Else 
        pnlOrderHeader.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlOrderHeader.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccOrderHeaderCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlOrderHeader_evtUpdated(ByVal vWhichProperty As clsOrderHeader.enmUpdateType, ByVal vOrderHeader As clsOrderHeader) Handles _ctlOrderHeader.evtUpdated 
    _OrderHeaderCol = Nothing 
    Dim pFault As clsFault 
    _OrderHeaderID = CType(vOrderHeader, clsOrderHeader).ID 
    If _ActiveControl.Name = "ctlccOrderHeader" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboOrderHeaders(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlOrderHeader() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlOrderHeader.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboOrderHeaders(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccOrderHeaderDefaultByID 
    Dim pParentID As Long = 0 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.Customer Then 
      pComboListTypeToLoad = clsEnums.enmComboListType.ccOrderHeaderForCustomerDefaultByID 
      pParentID = _Requester.UserIdentityInstanceID 
    End If 
    
    RaiseEvent evtOverrideLoadCboOrderHeader(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
    If pComboList Is Nothing Then 
      If vRenewCache = True Then MyCache.ClearComboList(pComboListTypeToLoad) 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK Then Return pFault 
    Else 
      pFault = New clsFault 
      pFault.SetOK() 
    End If 
    
    If pPrompt = "" Then pPrompt = pChoose 
 
    If pComboList?.Count = 0 Then 
      MyIntelliCombo.Clear() 
      MyIntelliCombo.SetKeyType(clsEnums.enmComboListKeyType.Long) 
    End If 
 
    If pComboList IsNot Nothing Then 
      MyIntelliCombo.LoadControl(pComboList, pPrompt, vShowOptionsOn1stLoad:=True) 
    Else 
      MyIntelliCombo.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester, vShowOptionsOn1stLoad:=True) 
    End If 
 
    If _OrderHeaderID >= 0 Then 
      MyIntelliCombo.ValueSelect(_OrderHeaderID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_OrderHeaderUpdate, _Requester) = True Then 
      If pAddNewPrompt = "" Then pAddNewPrompt = ccHelper.GetNew(_Requester) 
      btnNew.Text = pAddNewPrompt 
    End If 
 
    Return pFault 
  End Function 
 
  Private Sub MyIntelliCombo_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles MyIntelliCombo.evtComboListMemberChosen  
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    Cursor = Cursors.WaitCursor 
    Static sLoading As Boolean 
    If vComboListMember Is Nothing OrElse vComboListMember.KeyType = clsEnums.enmComboListKeyType.UD Then 
      If sLoading = True Then Exit Sub 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _OrderHeaderID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _OrderHeaderID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetOrderHeaderIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _OrderHeaderID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _OrderHeaderID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _OrderHeaderID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _OrderHeaderID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseOrderHeader() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccOrderHeader", StringComparison.OrdinalIgnoreCase) AndAlso _OrderHeaderID > 0 Then 
        'to avoid getting ObjectNotFound 
        _OrderHeader = New clsOrderHeader(clsEnums.enmLoadParent.TextOnly) 
        pFault = _OrderHeader.GetByID(_OrderHeaderID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccOrderHeader") 
    End If 
    pnlOrderHeader.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  Private Sub _ctlCustomerDebtCol_evtRowDoubleClicked(ByVal vCustomerDebt As clsCustomerDebt, ByRef rHandled As Boolean) Handles _ctlCustomerDebtCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtCustomerDebtChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtCustomerDebtChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vCustomerDebt.ID 
      .Object = New clsCustomerDebt 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlDeliveryCol_evtRowDoubleClicked(ByVal vDelivery As clsDelivery, ByRef rHandled As Boolean) Handles _ctlDeliveryCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtDeliveryChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtDeliveryChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vDelivery.ID 
      .Object = New clsDelivery 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlOrderLineCol_evtRowDoubleClicked(ByVal vOrderLine As clsOrderLine, ByRef rHandled As Boolean) Handles _ctlOrderLineCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtOrderLineChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtOrderLineChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vOrderLine.ID 
      .Object = New clsOrderLine 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlSupplierOrderCol_evtRowDoubleClicked(ByVal vSupplierOrder As clsSupplierOrder, ByRef rHandled As Boolean) Handles _ctlSupplierOrderCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtSupplierOrderChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtSupplierOrderChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vSupplierOrder.ID 
      .Object = New clsSupplierOrder 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As clsOrderHeader.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlOrderHeader.evtParentChosen 
    If vParentName = clsOrderHeader.enmParentProperty.Customer Then 
      rHandled = True 
      If _CancelEvtCustomerChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New clsCustomer 
      End With 
      Try 
        RaiseEvent evtEntityChosen(Me, pEventArgs) 
      Catch ex As Exception 
        rHandled = False 
      End Try 
    End If 
  End Sub 
   
  Private Sub chkGrid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrid.CheckedChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    
    Cursor = Cursors.WaitCursor 
    chkGrid.Enabled = False 
    pnlButtons.Visible = False 
    pnlOrderHeader.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkOrderHeaderCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _OrderHeaderID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccOrderHeaderCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkOrderHeaderCol.Visible = False 
      _ActiveControl = _ctlOrderHeader 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboOrderHeaders(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _OrderHeaderID <> 0 Then 
        pFault = ActivateControl("ctlccOrderHeader") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlOrderHeader.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlOrderHeader.Visible = False 
        _OrderHeaderID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _OrderHeaderID > 0 Then pnlOrderHeader.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkCustomerDebtCol.MouseEnter, 
                  lnkDeliveryCol.MouseEnter, 
                  lnkOrderLineCol.MouseEnter, 
                  lnkSupplierOrderCol.MouseEnter, 
                  lnkOrderHeaderCol.MouseEnter, 
                  lnkOrderHeader.MouseEnter, 
                  lblBack.MouseEnter 
    Dim lnk As Label = CType(sender, Label) 
    If lnk.BackColor = Color.Wheat Then Exit Sub 
    lnk.BackColor = Color.FromArgb(252, 227, 138) 'Color.LightGray 
    If lnk IsNot lblBack Then 
      lnk.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    Else 
      lnk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle 
    End If 
    lnk.Cursor = Cursors.Hand 
  End Sub 
 
  Private Sub lnk_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkCustomerDebtCol.MouseLeave, 
                  lnkDeliveryCol.MouseLeave, 
                  lnkOrderLineCol.MouseLeave, 
                  lnkSupplierOrderCol.MouseLeave, 
                  lnkOrderHeaderCol.MouseLeave, 
                  lnkOrderHeader.MouseLeave, 
                  lblBack.MouseLeave 
    Dim lnk As Label = CType(sender, Label) 
    If lnk.BackColor = Color.Wheat Then Exit Sub 
    lnk.BackColor = Color.White 
    If lnk IsNot lblBack Then 
      lnk.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    Else 
      lnk.BorderStyle = System.Windows.Forms.BorderStyle.None 
      lnk.BackColor = gpbHeader.BackColor 
    End If 
    lnk.Cursor = Cursors.Default 
  End Sub 
 
  'Hide links if in add 
  Private Sub _ctlOrderHeader_evtAdd(ByVal vOrderHeader As clsOrderHeader) Handles _ctlOrderHeader.evtAdd 
    lnkCustomerDebtCol.Visible = False 
    lnkDeliveryCol.Visible = False 
    lnkOrderLineCol.Visible = False 
    lnkSupplierOrderCol.Visible = False 
    lnkOrderHeaderCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pOrderNumberFrom As Nullable(Of Integer) = Nothing 
    Dim pOrderNumberTo As Nullable(Of Integer) = Nothing 
    Dim pCustomerID As Nullable(Of Long) = Nothing 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.Customer Then 
      pCustomerID = _Requester.UserIdentityInstanceID 
    End If 
    Dim pOrderDateStart As Nullable(Of Date) = Nothing 
    Dim pOrderDateEnd As Nullable(Of Date) = Nothing 
    Dim pPaymentStatus As clsEnums.enmPaymentStatus = Nothing 
    Dim pOrderStatus As clsEnums.enmOrderStatus = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByCustomerID As Boolean = False 
    Dim pGroupByOrderDate As Boolean = False 
    Dim pGroupByPaymentStatus As Boolean = False 
    Dim pGroupByOrderStatus As Boolean = False 
    
    Dim pSumOrderNumber As Boolean = False 
    Dim pSumTotalAmount As Boolean = False 
    Dim pSumVATAmount As Boolean = False 
    Dim pSumTotalWithVAT As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Order Headers"  
  
      _frmSearch.flpFilter.Controls.Clear() 
      _frmSearch.flpGroupBy.Controls.Clear() 
      _frmSearch.flpSumColumns.Controls.Clear() 
  
      'Get wild-card type combolist  
      Dim pWildCardTypes As New clsComboList  
      pFault = pWildCardTypes.FillEnums(clsEnums.enmEnum.WildCardType, _Requester)  
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      pWildCardTypes.Remove(pWildCardTypes.FindByKey(clsEnums.enmWildCardType.UD)) 
      pWildCardTypes.SortByText()  
      pWildCardTypes.AddToTop(clsEnums.enmWildCardType.UD, GetChoose(_Requester)) 
 
      'Prepare for dates 
      Dim pNowStart As Date = Now.Date 
      Dim pNowEnd As Date = pNowStart.AddDays(1).AddSeconds(-1) 
      Dim pNowMonthStart As Date = pNowStart.Date.AddDays(-(Now.Day - 1)) 
      Dim pNowMonthEnd As Date = pNowMonthStart 
 
      With _frmSearch 
        .pnlRows.TabIndex = 0 
        .txtMaxRowsToReturn.TabIndex = 1 
        .flpFilter.TabIndex = 2 
        .flpFilter.Controls.Add(.lblFilterBy) 
        .Text01Label.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.OrderNumber), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.OrderNumber), "Order Number") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 3 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 4 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        If pCustomerID Is Nothing Then 
         .Combo01Label.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.Customer), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.Customer), "Customer") 
         Dim pCustomers As New clsComboList 
         pFault = MyCache.GetComboList(clsEnums.enmComboListType.ccCustomerDefaultByID, pCustomers) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
         'If pCustomers IsNot Nothing AndAlso pCustomers.Count > 0 Then 
         .flpFilter.Controls.Add(.Combo01Label) 
         .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
         'End If 
         With .Combo01 
           .MakeSmart() 
           If pCustomers IsNot Nothing Then 
             .LoadControl(pCustomers, GetChoose(_Requester)) 
           Else 
             .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.ccCustomerDefaultByID, 0, _Requester) 
           End If 
           .TabIndex = 5 
         End With 
        End If 
 
        .Date01Label.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.OrderDate), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.OrderDate), "Order Date") 
        .Date01From.TabIndex = 6 
        .Date01To.TabIndex = 7 
        .Date01From.ShowCheckBox = True 
        .Date01To.ShowCheckBox = True 
        .Date01From.Checked = False 
        .Date01To.Checked = False 
        If _ctlOrderHeaderCol.LoadParameters.ColumnsFormat.ContainsKey(clsOrderHeader.enmProperty.OrderDate) Then 
          .Date01From.CustomFormat = _ctlOrderHeaderCol.LoadParameters.ColumnsFormat(clsOrderHeader.enmProperty.OrderDate) 
          .Date01To.CustomFormat = _ctlOrderHeaderCol.LoadParameters.ColumnsFormat(clsOrderHeader.enmProperty.OrderDate) 
        Else 
          .Date01From.CustomFormat = "dd-MM-yyyy HH:mm:ss" 
          .Date01To.CustomFormat = "dd-MM-yyyy HH:mm:ss" 
        End If 
        If .Date01From.CustomFormat.IndexOf("dd") >= 0 Then 
          .Date01From.Value = pNowStart 
          .Date01To.Value = pNowEnd 
        Else 
          .Date01From.Value = pNowMonthStart 
          .Date01To.Value = pNowMonthEnd 
        End If 
        .flpFilter.Controls.Add(.Date01Label) 
        .flpFilter.Controls.Add(.Date01From) 
        .flpFilter.Controls.Add(.Date01lblTo) 
        .flpFilter.Controls.Add(.Date01To) 
 
        .Combo02Label.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.PaymentStatus), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.PaymentStatus), "Payment Status") 
        Dim pPaymentStatuss As New clsComboList 
        pFault = pPaymentStatuss.FillEnums(clsEnums.enmEnum.PaymentStatus, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pPaymentStatuss.Remove(pPaymentStatuss.FindByKey(clsEnums.enmPaymentStatus.UD)) 
        pPaymentStatuss.SortByText() 
        If pPaymentStatuss IsNot Nothing AndAlso pPaymentStatuss.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo02Label) 
          .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo02 
          .MakeSmart() 
          .LoadControl(pPaymentStatuss, GetChoose(_Requester)) 
          .TabIndex = 8 
        End With 
 
        .Combo03Label.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.OrderStatus), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.OrderStatus), "Order Status") 
        Dim pOrderStatuss As New clsComboList 
        pFault = pOrderStatuss.FillEnums(clsEnums.enmEnum.OrderStatus, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pOrderStatuss.Remove(pOrderStatuss.FindByKey(clsEnums.enmOrderStatus.UD)) 
        pOrderStatuss.SortByText() 
        If pOrderStatuss IsNot Nothing AndAlso pOrderStatuss.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo03Label) 
          .flpFilter.Controls.Add(.Combo03)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo03 
          .MakeSmart() 
          .LoadControl(pOrderStatuss, GetChoose(_Requester)) 
          .TabIndex = 9 
        End With 
 
        .Text02Label.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.ID), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 10 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 11 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.Customer), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.Customer), "Customer") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 12 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.OrderDate), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.OrderDate), "Order Date") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 13 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.PaymentStatus), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.PaymentStatus), "Payment Status") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 14 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .lblGroupBy04.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.OrderStatus), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.OrderStatus), "Order Status") 
        .chkGroupBy04.Checked = False 
        .chkGroupBy04.TabIndex = 15 
        .flpGroupBy.Controls.Add(.lblGroupBy04) 
        .flpGroupBy.Controls.Add(.chkGroupBy04) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.OrderNumber), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.OrderNumber), "Order Number") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 16 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.TotalAmount), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.TotalAmount), "Total Amount") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 17 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
        .lblSumField03.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.VATAmount), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.VATAmount), "VAT Amount") 
        .chkSumField03.Checked = False 
        .chkSumField03.TabIndex = 18 
        .flpSumColumns.Controls.Add(.lblSumField03) 
        .flpSumColumns.Controls.Add(.chkSumField03) 
 
        .lblSumField04.Text = If(_ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderHeader.enmProperty.TotalWithVAT), _ctlOrderHeaderCol.LoadParameters.ColumnsHeaderText(clsOrderHeader.enmProperty.TotalWithVAT), "Total With VAT") 
        .chkSumField04.Checked = False 
        .chkSumField04.TabIndex = 19 
        .flpSumColumns.Controls.Add(.lblSumField04) 
        .flpSumColumns.Controls.Add(.chkSumField04) 
 
      End With 
    
      RaiseEvent evtOverrideSearchForm() 
    End If 
    
    Cursor = Cursors.Default 
    
    Dim pResult As System.Windows.Forms.DialogResult 
    pResult = _frmSearch.ShowDialog() 
 
    If _frmSearch.DialogResult <> DialogResult.OK Then Exit Sub 
 
    Cursor = Cursors.WaitCursor 
    
    Dim pDoSum As Boolean = False 
 
    Dim pRows As Integer = 100 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.DESC 
    _SearchFilters = New Dictionary(Of System.Enum, Object) 
    
    With _frmSearch 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pOrderNumberFrom = ccHelper.ToInteger(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pOrderNumberTo = ccHelper.ToInteger(.Text01To.Text) 
          Else 
            pOrderNumberTo = pOrderNumberFrom 
          End If 
          _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderNumberFrom, pOrderNumberFrom) 
          _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderNumberTo, pOrderNumberTo) 
        End If 
      End If 
      If pCustomerID Is Nothing Then 
       If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
         pCustomerID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
         _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.CustomerID, pCustomerID) 
       End If 
      Else 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.CustomerID, pCustomerID) 
      End If  
      If .Date01From.Checked OrElse .Date01To.Checked Then 
        pOrderDateStart = .Date01From.Value 
        pOrderDateEnd = .Date01To.Value 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderDateStart, pOrderDateStart) 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderDateEnd, pOrderDateEnd) 
      End If 
      If .Combo02.SelectedItem IsNot Nothing Then 
        pPaymentStatus = CType(CType(.Combo02.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmPaymentStatus) 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.PaymentStatus, pPaymentStatus) 
      End If 
      If .Combo03.SelectedItem IsNot Nothing Then 
        pOrderStatus = CType(CType(.Combo03.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmOrderStatus) 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.OrderStatus, pOrderStatus) 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsOrderHeaderCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsOrderHeaderCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsOrderHeaderCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByCustomerID = True 
        pDoSum = True 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByCustomerID, pGroupByCustomerID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByOrderDate = True 
        pDoSum = True 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByOrderDate, pGroupByOrderDate) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByPaymentStatus = True 
        pDoSum = True 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByPaymentStatus, pGroupByPaymentStatus) 
      End If 
      If .chkGroupBy04.Checked = True Then 
        pGroupByOrderStatus = True 
        pDoSum = True 
        _SearchFilters.Add(clsOrderHeaderCol.enmFillSumOnTheFlyParameters.GroupByOrderStatus, pGroupByOrderStatus) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumOrderNumber = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumTotalAmount = True 
        pDoSum = True 
      End If 
      
      If .chkSumField03.Checked = True Then 
        pSumVATAmount = True 
        pDoSum = True 
      End If 
      
      If .chkSumField04.Checked = True Then 
        pSumTotalWithVAT = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsOrderHeaderCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsOrderHeaderCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsOrderHeaderCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsOrderHeaderCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccOrderHeaderCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccOrderHeaderCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsOrderHeader.enmProperty.ID, "ID") 
      End With 
      _OrderHeaderCol = New clsOrderHeaderCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _OrderHeaderCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.Customer 
            pFault = _OrderHeaderCol.FillByCustomerID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
          Case Else 
            pFault = _OrderHeaderCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _OrderHeaderCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _OrderHeaderCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _OrderHeaderCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see OrderHeader" 
      RaiseEvent evtOverrideLoadCtlOrderHeaderCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _OrderHeaderCol = New clsOrderHeaderCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _OrderHeaderCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccOrderHeaderCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _OrderHeaderCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsOrderHeader.enmProperty.ID, "Count") 
        If pGroupByCustomerID = False Then .ColumnsHide.Add(clsOrderHeader.enmProperty.Customer) 
        If pGroupByOrderDate = False Then .ColumnsHide.Add(clsOrderHeader.enmProperty.OrderDate) 
        If pGroupByPaymentStatus = False Then .ColumnsHide.Add(clsOrderHeader.enmProperty.PaymentStatus) 
        If pGroupByOrderStatus = False Then .ColumnsHide.Add(clsOrderHeader.enmProperty.OrderStatus) 
        If pSumOrderNumber = False Then .ColumnsHide.Add(clsOrderHeader.enmProperty.OrderNumber) 
        If pSumTotalAmount = False Then .ColumnsHide.Add(clsOrderHeader.enmProperty.TotalAmount) 
        If pSumVATAmount = False Then .ColumnsHide.Add(clsOrderHeader.enmProperty.VATAmount) 
        If pSumTotalWithVAT = False Then .ColumnsHide.Add(clsOrderHeader.enmProperty.TotalWithVAT) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.PaymentMethod) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.PaymentDate) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.InvoiceNumber) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.DeliveryMethod) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.DeliveryDate) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.DeliveryDay) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.Notes) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.Notes2) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.OrderMonth) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.Quarter) 
        .ColumnsHide.Add(clsOrderHeader.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlOrderHeaderCol.Visible = True 
    pFault = _ctlOrderHeaderCol.LoadControl(_OrderHeaderCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsOrderHeaderCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsOrderHeaderCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlOrderHeader.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlOrderHeader.Controls(0).Name) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pnlCover.SendToBack() 
      _NestedFormsCount -= 1 
    End If 
    If _NestedFormsCount = 0 Then 
      If _NestedInMain = False Then 
        lblBack.Visible = False 
        chkGrid.Enabled = True 
      Else 
        Dim pEvent As New PanelEventArgs 
        RaiseEvent evtBackClicked(Me, pEvent) 
      End If 
    End If 
  End Sub 
 
  'btnNew 
  Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
 
    Cursor = Cursors.WaitCursor 
    _OrderHeaderID = -2 
    pFault = ActivateControl("ctlccOrderHeader") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlOrderHeader() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlOrderHeader.Visible = True 'new 
    Cursor = Cursors.Default 
 
    pnlCover.SendToBack() 
 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
 
  End Sub 
 
  ' Load btnNew 
  ' add this to the bottom of the prt file, or integrate it into the designer.vb file 
  ' If you don't want the btnNew, make it "nothing" in the prt file 
  'Friend WithEvents btnNew As System.Windows.Forms.Button = New System.Windows.Forms.Button() 
 
  'Private Sub ctlPnlXXX_evtOverrideLoadControlforBtnNew(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
  '  _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
 
  '  'If in designer.vb 
  '  If btnNew Is Nothing Then 
  '    btnNew = New System.Windows.Forms.Button() 
  '    Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.9!)) 
  '    Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.1!)) 
  '    Exit Sub 
  '  End If 
 
  '  'Me.tlpHeader.ColumnStyles(0).Width = 80.0! 
  '  'Me.tlpHeader.ColumnStyles(1).Width = 20.0! 
 
  '  Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!)) 
  '  Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!)) 
 
  '  Me.tlpHeader.Controls.Add(Me.btnNew, 1, 0) 
 
  '  'btnNew 
  '  ' 
  '  Me.btnNew.AutoSize = True 
  '  Me.btnNew.Dock = System.Windows.Forms.DockStyle.Fill 
  '  Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
  '  Me.btnNew.Location = New System.Drawing.Point(236, 0) 
  '  Me.btnNew.Margin = New System.Windows.Forms.Padding(0) 
  '  Me.btnNew.Name = "btnNew" 
  '  Me.btnNew.Size = New System.Drawing.Size(59, 24) 
  '  Me.btnNew.TabIndex = Me.MyIntelliCombo.TabIndex + 1 
  '  Me.btnNew.Text = "New" 
  '  Me.btnNew.UseVisualStyleBackColor = True 
  '  ' 
 
  'End Sub 
 
  Private Sub _ctlOrderHeaderCol_evtTimerTripped() Handles _ctlOrderHeaderCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtOrderHeaderTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlOrderHeaderCol.OrderHeaderCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlOrderHeaderCol.OrderHeaderCol(0).ID 
 
    If pNewLastID <> pLastID Then 
      Console.Beep(500, 100) 
      Console.Beep(1000, 100) 
      Console.Beep(2000, 300) 
      Console.Beep(1000, 100) 
      System.Media.SystemSounds.Hand.Play() 
    End If 
 
  End Sub 
 
  Private Function CreateGridTitle(ByVal vShowingRowsText As String) As String 
    Dim pFilters As New Text.StringBuilder 
 
    pFilters.Append(vShowingRowsText & " - ") 
    If _OrderHeaderCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsOrderHeader() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsOrderHeaderCol = CType(CallByName(_OrderHeaderCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsOrderHeaderCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsOrderHeaderCol = CType(CallByName(_OrderHeaderCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsOrderHeaderCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          Dim pOriginalFieldName As String = pFieldName 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 4) 
          Dim pText As String = "" 
          Try 
            pText = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
            If pText = "" Then 
              pCol(0).LoadLookupAndEnumText(_Requester) 
              pText = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
            End If 
            pFilters.Append($"{pFieldName}: {pText}; ") 
          Catch ex As Exception 
            'This means it wasn't a lookup 
            pFieldName = pOriginalFieldName 
            pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
          End Try 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf Not (pFieldName.Equals("HowMany") OrElse pFieldName.Equals("Dir") OrElse 
                  pFieldName.EndsWith("WildcardType") OrElse 
                  pFieldName.EndsWith("From") OrElse pFieldName.EndsWith("To") OrElse 
                  pFieldName.EndsWith("Start") OrElse pFieldName.EndsWith("End") OrElse 
                  ccHelper.GetPropertyTypeName(New clsOrderHeaderCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsOrderHeaderCol = CType(CallByName(_OrderHeaderCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsOrderHeaderCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          'Check that the field exists 
          Dim pTest = ccHelper.GetPropertyTypeName(pCol(0), $"{pFieldName}Text") 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          If pText = "" Then 
            pCol(0).LoadLookupAndEnumText(_Requester) 
            pText = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          End If 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      Else 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      End If 
      pFoundFilters = True 
    Next 
 
    If pFoundFilters = True Then 
      Return pFilters.ToString().Substring(0, pFilters.ToString().Length - 2) & ControlChars.NewLine 
    Else 
      Return pFilters.ToString().Substring(0, pFilters.ToString().Length - 3) & ". " 
    End If 
 
  End Function 
 
  Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.DoubleClick 
    If lblTitle.ForeColor = Color.Black Then Exit Sub 
    'reset the filter 
    _SearchFilters = New Dictionary(Of System.Enum, Object) 
    btnFilter.BackColor = Me.BackColor 
    lblTitle.ForeColor = Color.Black 
    btnRefresh.Visible = True 
    btnRefresh.PerformClick() 
  End Sub 
 
  Private Sub cc_ctlPnlOrderHeader_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
