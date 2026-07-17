Public Class ctlPnlccProduct 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlProductCol As ctlccProductCol 
  Private WithEvents _ctlProduct As ctlccProduct 
  Private WithEvents _ctlOrderLineCol As ctlccOrderLineCol 
  Private WithEvents _ctlProductPriceCol As ctlccProductPriceCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _ProductID As Long 
 
  'The data holders 
  Private _ProductCol As clsProductCol 
  Private _Product As clsProduct 
  Private _OrderLineCol As clsOrderLineCol 
  Private _ProductPriceCol As clsProductPriceCol 
 
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
  Public Event evtOverrideLoadCboProduct(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetProductIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillProductCol(ByRef rProductCol As clsProductCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillOrderLineCol(ByRef rOrderLineCol As clsOrderLineCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillProductPriceCol(ByRef rProductPriceCol As clsProductPriceCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlProductCol(ByRef rLoadParameters As ctlccProductCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlProduct(ByRef rLoadParameters As ctlccProduct.clsLoadParameters) 
  Private Event evtOverrideLoadCtlOrderLineCol(ByRef rLoadParameters As ctlccOrderLineCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlProductPriceCol(ByRef rLoadParameters As ctlccProductPriceCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreProductCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtProductTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtOrderLineChosen As Boolean = False 
  Private _ShowPopForEvtOrderLineChosen As Boolean = False 
  Private _CancelEvtProductPriceChosen As Boolean = False 
  Private _ShowPopForEvtProductPriceChosen As Boolean = False 
  'Parents
  
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
 
    lnkProductCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkProduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkOrderLineCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkProductPriceCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vProductID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _ProductID = CType(vProductID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlProduct.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkProductCol.Visible = False 
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
      pFault = LoadCboProducts(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _ProductID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_ProductID) 
      End If 
      ChooseProduct() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccProduct") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _ProductID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
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
    
    If pControlName = "ctlccProduct" OrElse pControlName = "ctlProduct" Then 
      lnkProduct.ForeColor = Color.Black : lnkProduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkProduct.BackColor = Color.Wheat 
      If _ctlProduct Is Nothing Then 
        _ctlProduct = New ctlccProduct() 
        _ctlProduct.Dock = DockStyle.Fill 
        _ctlProduct.Controls.RemoveByKey("btnAdd") 
        pnlProduct.Controls.Add(_ctlProduct) 
        _ctlProduct.Visible = False 
      End If 
      If _ProductID = 0 Then 
        pnlProduct.Visible = False 
      End If 
      'If _Product Is Nothing Then 
      pFault = RefreshCtlProduct() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlProduct.Product.IsEmpty AndAlso _ProductID <> -2 Then 
        pnlProduct.Visible = False 
      End If 
      _ctlProduct.Name = "ctlccProduct" 
      _ActiveControl = _ctlProduct 
      _ctlProduct.BringToFront() 
      _ctlProduct.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccProductCol" Then 
      lnkProductCol.ForeColor = Color.Black : lnkProductCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkProductCol.BackColor = Color.Wheat 
      If _ctlProductCol Is Nothing Then 
        _ctlProductCol = New ctlccProductCol() 
        _ctlProductCol.Dock = DockStyle.Fill 
        pnlProduct.Controls.Add(_ctlProductCol) 
        _ctlProductCol.Visible = False 
      End If  
      pnlProduct.Visible = True 
      If _ProductCol Is Nothing Then 
        pFault = RefreshCtlProductCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlProductCol.Name = "ctlccProductCol" 
      _ActiveControl = _ctlProductCol 
      _ctlProductCol.BringToFront() 
      _ctlProductCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlccOrderLineCol" Then 
      lnkOrderLineCol.ForeColor = Color.Black : lnkOrderLineCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkOrderLineCol.BackColor = Color.Wheat 
      If _ctlOrderLineCol Is Nothing Then 
      _ctlOrderLineCol = New ctlccOrderLineCol() 
      _ctlOrderLineCol.Dock = DockStyle.Fill 
      pnlProduct.Controls.Add(_ctlOrderLineCol) 
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
    ElseIf pControlName = "ctlccProductPriceCol" Then 
      lnkProductPriceCol.ForeColor = Color.Black : lnkProductPriceCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkProductPriceCol.BackColor = Color.Wheat 
      If _ctlProductPriceCol Is Nothing Then 
      _ctlProductPriceCol = New ctlccProductPriceCol() 
      _ctlProductPriceCol.Dock = DockStyle.Fill 
      pnlProduct.Controls.Add(_ctlProductPriceCol) 
      _ctlProductPriceCol.Visible = False 
      End If  
      If _ProductPriceCol Is Nothing Then 
        pFault = RefreshCtlProductPriceCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlProductPriceCol.Name = "ctlccProductPriceCol" 
      _ActiveControl = _ctlProductPriceCol 
      _ctlProductPriceCol.BringToFront() 
      _ctlProductPriceCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-Product-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("Product", _Requester) 
 
    lnkProductCol.Text = CCTextTranslate("List", _Requester) 
    lnkProduct.Text = CCTextTranslate("Details", _Requester) 
 
    lnkOrderLineCol.Text = TableNameTranslate("OrderLine", _Requester, vMakePlural:=True) 
    lnkProductPriceCol.Text = TableNameTranslate("ProductPrice", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlProduct.Controls(0) Is _ctlProduct Then 
      If _ProductID = 0 Then 
        pnlProduct.Visible = False 
      End If 
    ElseIf pnlProduct.Controls(0) Is _ctlProductCol Then 
    ElseIf pnlProduct.Controls(0) Is _ctlOrderLineCol Then 
    ElseIf pnlProduct.Controls(0) Is _ctlProductPriceCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pProductID As Long = _ProductID 
      If ccHelper.IsNumeric(pText) Then _ProductID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetProductIDFromIntelliComboText(pText) 
      If pProductID <> _ProductID Then 
        _Product = Nothing 
        pFault = ActivateControl("ctlccProduct") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlProduct.Controls(0) Is _ctlProduct Then 
      pFault = RefreshCtlProduct() 
    ElseIf pnlProduct.Controls(0) Is _ctlProductCol Then 
      pFault = RefreshCtlProductCol() 
    ElseIf pnlProduct.Controls(0) Is _ctlOrderLineCol Then 
      pFault = RefreshCtlOrderLineCol() 
    ElseIf pnlProduct.Controls(0) Is _ctlProductPriceCol Then 
      pFault = RefreshCtlProductPriceCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlProduct.Controls(0).Name, "", "TRGT-Product-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboProducts(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlProductCol_evtRowClicked(ByVal vProduct As Object) Handles _ctlProductCol.evtRowClicked 
    
    If vProduct Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pProduct As clsProduct = CType(vProduct, clsProduct) 
    _ProductID = pProduct.ID 
    
    If _ActiveControl Is _ctlProductCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = clsProductCol.enmFillSumOnTheFlyParameters.GroupByCategory.ToString() Then 
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
    
    ChooseProduct() 
    
    Try 
      MyIntelliCombo.ValueSelect(_ProductID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pProduct.ProductCode & " " & pProduct.ProductName
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseProduct() 
    _Product = Nothing 
    lnkProduct.Visible = True 
    _OrderLineCol = Nothing 
    lnkOrderLineCol.Visible = True 
    _ProductPriceCol = Nothing 
    lnkProductPriceCol.Visible = True 
  End Sub 
  Private Sub _ctlProductCol_evtRowDoubleClicked(ByVal vProduct As clsProduct, ByRef rHandled As Boolean) Handles _ctlProductCol.evtRowDoubleClicked 
    If lnkProduct.Parent IsNot flpMenu Then Exit Sub 
    If vProduct Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = clsProductCol.enmFillSumOnTheFlyParameters.GroupByCategory.ToString() Then 
        If pSearchFilters.ContainsKey(clsProductCol.enmFillOnTheFlyParameters.Category) Then pSearchFilters.Remove(clsProductCol.enmFillOnTheFlyParameters.Category) 
        pSearchFilters.Add(clsProductCol.enmFillOnTheFlyParameters.Category, vProduct.Category) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreProductCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vProduct.ID, vProduct.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _ProductID = vProduct.ID 
      'MyIntelliCombo.ValueSelect(_ProductID) 
      pFault = ActivateControl("ctlccProduct") 
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
      pFault = _ProductCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ProductCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ProductCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProductCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccProductCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsProduct.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Product" 
      pFault = _ctlProductCol.LoadControl(_ProductCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlProductCol_evtUnChosen() Handles _ctlProductCol.evtUnChosen 
 
    _ProductID = 0 
    _Product = Nothing 
    _OrderLineCol = Nothing 
    lnkOrderLineCol.Visible = False 
    _ProductPriceCol = Nothing 
    lnkProductPriceCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkProduct.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkOrderLineCol.Click, 
      lnkProductPriceCol.Click, 
      lnkProductCol.Click, 
      lnkProduct.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkProduct OrElse (lnk Is lnkProductCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlProductCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccProductCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsProduct.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsProductCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillProductCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _ProductCol = New clsProductCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ProductCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlProductCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlProductCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _ProductCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlProductCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _ProductCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _ProductCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProductCol.Count) 
      End If 
    Else 
      _ProductCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _ProductCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlProductCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Product" 
    
    Dim pProductID As Long = _ProductID 
    
    pFault = _ctlProductCol.LoadControl(_ProductCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlProductCol.Visible = True 
    
    _ctlProductCol.Refresh() 
    If pProductID <> 0 Then 
      Dim pProductCol As clsProductCol = CType(_ctlProductCol.bsCtlProduct.DataSource, clsProductCol) 
      Dim pProduct As clsProduct = pProductCol.FindByID(pProductID) 
      If pProduct.ID > 0 Then 
        _ctlProductCol.bsCtlProduct.CurrencyManager.Position = pProductCol.IndexOf(pProduct) 
        _ctlProductCol.dgvProduct.Rows(pProductCol.IndexOf(pProduct)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlProduct() As clsFault 
    Dim pFault As New clsFault 
    
    If _ProductID > 0 Then 
      ChooseProduct() 
      _Product = New clsProduct() 
      pFault = _Product.GetByID(_ProductID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _Product = New clsProduct() 
    End If 
    'lblSecondaryTitle.Text = _Product.ProductCode & " " & _Product.ProductName    
     
    Dim pLoadParameters As New ctlccProduct.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlProduct(pLoadParameters)
    pFault = _ctlProduct.LoadControl(_Product, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlProduct.Visible = True 
    If _ProductID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlProduct.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlProduct.btnAdd.Visible = False 
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
      pFault = _OrderLineCol.FillByProductID(_ProductID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
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
      If _Product IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Product.DefaultDesignation) Then 
        .ReportTitle = "List of OrderLines for " & _Product.DefaultDesignation 
      Else 
        .ReportTitle = "List of OrderLines for Product" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsOrderLine.enmProperty.Product) 
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
  Private Function RefreshCtlProductPriceCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlProductPriceCol.dgvProductPrice.SelectedRows.Count > 0 Then 
      Dim pProductPrice As clsProductPrice = CType(_ctlProductPriceCol.bsCtlProductPrice.Current, clsProductPrice) 
      pID = pProductPrice.ID 
    End If 
 
    Dim pTestCol As clsProductPriceCol = Nothing 
    RaiseEvent evtOverrideFillProductPriceCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _ProductPriceCol = New clsProductPriceCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _ProductPriceCol.FillByProductID(_ProductID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _ProductPriceCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _ProductPriceCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProductPriceCol.Count) 
      End If 
    Else 
      _ProductPriceCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _ProductPriceCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlccProductPriceCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _Product IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Product.DefaultDesignation) Then 
        .ReportTitle = "List of ProductPrices for " & _Product.DefaultDesignation 
      Else 
        .ReportTitle = "List of ProductPrices for Product" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsProductPrice.enmProperty.Product) 
    End With 
    RaiseEvent evtOverrideLoadCtlProductPriceCol(pLoadParameters)
    
    pFault = _ctlProductPriceCol.LoadControl(_ProductPriceCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlProductPriceCol.Visible = True 
 
    If pID > 0 Then 
      Dim pProductPrices As clsProductPriceCol = CType(_ctlProductPriceCol.bsCtlProductPrice.DataSource, clsProductPriceCol) 
      Dim pProductPrice As clsProductPrice = pProductPrices.FindByID((pID)) 
      If pProductPrice.ID > 0 Then 
        _ctlProductPriceCol.bsCtlProductPrice.CurrencyManager.Position = pProductPrices.IndexOf(pProductPrice) 
        _ctlProductPriceCol.dgvProductPrice.Rows(pProductPrices.IndexOf(pProductPrice)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlOrderLineCol_evtBeforeUpdate(ByVal vOrderLine As clsOrderLine, ByRef rCancel As Boolean) Handles _ctlOrderLineCol.evtBeforeUpdate 
    vOrderLine.ProductID = _Product.ID 
  End Sub 
  Private Sub _ctlProductPriceCol_evtBeforeUpdate(ByVal vProductPrice As clsProductPrice, ByRef rCancel As Boolean) Handles _ctlProductPriceCol.evtBeforeUpdate 
    vProductPrice.ProductID = _Product.ID 
  End Sub 
  Private Sub _ctlProduct_evtDeleted(ByVal vProductID As Long) Handles _ctlProduct.evtDeleted 
    _ProductCol = Nothing 
    Dim pFault As clsFault 
    _ProductID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboProducts(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlProduct() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlProduct.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkProductCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlProduct_evtCancelledEdit(ByVal vProduct As clsProduct) Handles _ctlProduct.evtCancelledEdit 
    RefreshCtlProduct() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboProducts(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlProduct.btnAdd.Visible = False 
      If _ProductID = 0 OrElse _ProductID = -2 Then 
        pnlProduct.Visible = False 
      Else 
        pnlProduct.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlProduct.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccProductCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlProduct_evtUpdated(ByVal vWhichProperty As clsProduct.enmUpdateType, ByVal vProduct As clsProduct) Handles _ctlProduct.evtUpdated 
    _ProductCol = Nothing 
    Dim pFault As clsFault 
    _ProductID = CType(vProduct, clsProduct).ID 
    If _ActiveControl.Name = "ctlccProduct" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboProducts(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlProduct() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlProduct.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboProducts(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccProductDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboProduct(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _ProductID >= 0 Then 
      MyIntelliCombo.ValueSelect(_ProductID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ProductID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ProductID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetProductIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _ProductID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _ProductID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _ProductID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _ProductID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseProduct() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccProduct", StringComparison.OrdinalIgnoreCase) AndAlso _ProductID > 0 Then 
        'to avoid getting ObjectNotFound 
        _Product = New clsProduct() 
        pFault = _Product.GetByID(_ProductID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccProduct") 
    End If 
    pnlProduct.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
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
  Private Sub _ctlProductPriceCol_evtRowDoubleClicked(ByVal vProductPrice As clsProductPrice, ByRef rHandled As Boolean) Handles _ctlProductPriceCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtProductPriceChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtProductPriceChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vProductPrice.ID 
      .Object = New clsProductPrice 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  
   
  Private Sub chkGrid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrid.CheckedChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    
    Cursor = Cursors.WaitCursor 
    chkGrid.Enabled = False 
    pnlButtons.Visible = False 
    pnlProduct.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkProductCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _ProductID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccProductCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkProductCol.Visible = False 
      _ActiveControl = _ctlProduct 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboProducts(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _ProductID <> 0 Then 
        pFault = ActivateControl("ctlccProduct") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlProduct.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlProduct.Visible = False 
        _ProductID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _ProductID > 0 Then pnlProduct.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkOrderLineCol.MouseEnter, 
                  lnkProductPriceCol.MouseEnter, 
                  lnkProductCol.MouseEnter, 
                  lnkProduct.MouseEnter, 
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
                  lnkOrderLineCol.MouseLeave, 
                  lnkProductPriceCol.MouseLeave, 
                  lnkProductCol.MouseLeave, 
                  lnkProduct.MouseLeave, 
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
  Private Sub _ctlProduct_evtAdd(ByVal vProduct As clsProduct) Handles _ctlProduct.evtAdd 
    lnkOrderLineCol.Visible = False 
    lnkProductPriceCol.Visible = False 
    lnkProductCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pProductCode As String = Nothing 
    Dim pProductCodeWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pCategory As clsEnums.enmCategory = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByCategory As Boolean = False 
    
    Dim pSumCurrentStock As Boolean = False 
    Dim pSumBaseCost As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Products"  
  
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
        .String01Label.Text = If(_ctlProductCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProduct.enmProperty.ProductCode), _ctlProductCol.LoadParameters.ColumnsHeaderText(clsProduct.enmProperty.ProductCode), "Product Code") 
        .String01Text.Text = "" 
        .String01Text.TabIndex = 3 
        With .String01WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 4 
        End With 
        .flpFilter.Controls.Add(.String01Label) 
        .flpFilter.Controls.Add(.String01Text) 
        .flpFilter.Controls.Add(.String01LblWCType) 
        .flpFilter.Controls.Add(.String01WCType) 
 
        .Combo01Label.Text = If(_ctlProductCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProduct.enmProperty.Category), _ctlProductCol.LoadParameters.ColumnsHeaderText(clsProduct.enmProperty.Category), "Category") 
        Dim pCategorys As New clsComboList 
        pFault = pCategorys.FillEnums(clsEnums.enmEnum.Category, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pCategorys.Remove(pCategorys.FindByKey(clsEnums.enmCategory.UD)) 
        pCategorys.SortByText() 
        If pCategorys IsNot Nothing AndAlso pCategorys.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo01Label) 
          .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo01 
          .MakeSmart() 
          .LoadControl(pCategorys, GetChoose(_Requester)) 
          .TabIndex = 5 
        End With 
 
        .Text01Label.Text = If(_ctlProductCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProduct.enmProperty.ID), _ctlProductCol.LoadParameters.ColumnsHeaderText(clsProduct.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 6 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlProductCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProduct.enmProperty.Category), _ctlProductCol.LoadParameters.ColumnsHeaderText(clsProduct.enmProperty.Category), "Category") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlProductCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProduct.enmProperty.CurrentStock), _ctlProductCol.LoadParameters.ColumnsHeaderText(clsProduct.enmProperty.CurrentStock), "Current Stock") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 9 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlProductCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProduct.enmProperty.BaseCost), _ctlProductCol.LoadParameters.ColumnsHeaderText(clsProduct.enmProperty.BaseCost), "Base Cost") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 10 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
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
      If .String01Text.Text <> "" Then 
        pProductCode = .String01Text.Text 
        pProductCodeWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(clsProductCol.enmFillOnTheFlyParameters.ProductCode, pProductCode) 
        _SearchFilters.Add(clsProductCol.enmFillOnTheFlyParameters.ProductCodeWildcardType, pProductCodeWildcardType) 
      End If 
      If .Combo01.SelectedItem IsNot Nothing Then 
        pCategory = CType(CType(.Combo01.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmCategory) 
        _SearchFilters.Add(clsProductCol.enmFillOnTheFlyParameters.Category, pCategory) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(clsProductCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsProductCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsProductCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsProductCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByCategory = True 
        pDoSum = True 
        _SearchFilters.Add(clsProductCol.enmFillSumOnTheFlyParameters.GroupByCategory, pGroupByCategory) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumCurrentStock = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumBaseCost = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsProductCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsProductCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsProductCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsProductCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccProductCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccProductCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsProduct.enmProperty.ID, "ID") 
      End With 
      _ProductCol = New clsProductCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ProductCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _ProductCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ProductCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ProductCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProductCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Product" 
      RaiseEvent evtOverrideLoadCtlProductCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _ProductCol = New clsProductCol() 
      pFault = _ProductCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccProductCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _ProductCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsProduct.enmProperty.ID, "Count") 
        If pGroupByCategory = False Then .ColumnsHide.Add(clsProduct.enmProperty.Category) 
        If pSumCurrentStock = False Then .ColumnsHide.Add(clsProduct.enmProperty.CurrentStock) 
        If pSumBaseCost = False Then .ColumnsHide.Add(clsProduct.enmProperty.BaseCost) 
        .ColumnsHide.Add(clsProduct.enmProperty.ProductCode) 
        .ColumnsHide.Add(clsProduct.enmProperty.ProductName) 
        .ColumnsHide.Add(clsProduct.enmProperty.UnitOfMeasure) 
        .ColumnsHide.Add(clsProduct.enmProperty.Notes) 
        .ColumnsHide.Add(clsProduct.enmProperty.IsActive) 
        .ColumnsHide.Add(clsProduct.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlProductCol.Visible = True 
    pFault = _ctlProductCol.LoadControl(_ProductCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsProductCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsProductCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlProduct.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlProduct.Controls(0).Name) 
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
    _ProductID = -2 
    pFault = ActivateControl("ctlccProduct") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlProduct() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlProduct.Visible = True 'new 
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
 
  Private Sub _ctlProductCol_evtTimerTripped() Handles _ctlProductCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtProductTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlProductCol.ProductCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlProductCol.ProductCol(0).ID 
 
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
    If _ProductCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsProduct() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsProductCol = CType(CallByName(_ProductCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsProductCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsProductCol = CType(CallByName(_ProductCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsProductCol) 
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
                  ccHelper.GetPropertyTypeName(New clsProductCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsProductCol = CType(CallByName(_ProductCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsProductCol) 
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
 
  Private Sub cc_ctlPnlProduct_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
