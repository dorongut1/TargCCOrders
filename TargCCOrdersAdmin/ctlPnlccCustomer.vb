Public Class ctlPnlccCustomer 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlCustomerCol As ctlccCustomerCol 
  Private WithEvents _ctlCustomer As ctlccCustomer 
  Private WithEvents _ctlBeehiveBuyerTrackingCol As ctlccBeehiveBuyerTrackingCol 
  Private WithEvents _ctlCustomerDebtCol As ctlccCustomerDebtCol 
  Private WithEvents _ctlOrderHeaderCol As ctlccOrderHeaderCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _CustomerID As Long 
 
  'The data holders 
  Private _CustomerCol As clsCustomerCol 
  Private _Customer As clsCustomer 
  Private _BeehiveBuyerTrackingCol As clsBeehiveBuyerTrackingCol 
  Private _CustomerDebtCol As clsCustomerDebtCol 
  Private _OrderHeaderCol As clsOrderHeaderCol 
 
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
  Public Event evtOverrideLoadCboCustomer(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetCustomerIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillCustomerCol(ByRef rCustomerCol As clsCustomerCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillBeehiveBuyerTrackingCol(ByRef rBeehiveBuyerTrackingCol As clsBeehiveBuyerTrackingCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillCustomerDebtCol(ByRef rCustomerDebtCol As clsCustomerDebtCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillOrderHeaderCol(ByRef rOrderHeaderCol As clsOrderHeaderCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlCustomerCol(ByRef rLoadParameters As ctlccCustomerCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlCustomer(ByRef rLoadParameters As ctlccCustomer.clsLoadParameters) 
  Private Event evtOverrideLoadCtlBeehiveBuyerTrackingCol(ByRef rLoadParameters As ctlccBeehiveBuyerTrackingCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlCustomerDebtCol(ByRef rLoadParameters As ctlccCustomerDebtCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlOrderHeaderCol(ByRef rLoadParameters As ctlccOrderHeaderCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreCustomerCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtCustomerTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtBeehiveBuyerTrackingChosen As Boolean = False 
  Private _ShowPopForEvtBeehiveBuyerTrackingChosen As Boolean = False 
  Private _CancelEvtCustomerDebtChosen As Boolean = False 
  Private _ShowPopForEvtCustomerDebtChosen As Boolean = False 
  Private _CancelEvtOrderHeaderChosen As Boolean = False 
  Private _ShowPopForEvtOrderHeaderChosen As Boolean = False 
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
 
    lnkCustomerCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkBeehiveBuyerTrackingCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkCustomerDebtCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkOrderHeaderCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vCustomerID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _CustomerID = CType(vCustomerID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlCustomer.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkCustomerCol.Visible = False 
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
      pFault = LoadCboCustomers(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _CustomerID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_CustomerID) 
      End If 
      ChooseCustomer() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccCustomer") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _CustomerID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlccCustomer" OrElse pControlName = "ctlCustomer" Then 
      lnkCustomer.ForeColor = Color.Black : lnkCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkCustomer.BackColor = Color.Wheat 
      If _ctlCustomer Is Nothing Then 
        _ctlCustomer = New ctlccCustomer() 
        _ctlCustomer.Dock = DockStyle.Fill 
        _ctlCustomer.Controls.RemoveByKey("btnAdd") 
        pnlCustomer.Controls.Add(_ctlCustomer) 
        _ctlCustomer.Visible = False 
      End If 
      If _CustomerID = 0 Then 
        pnlCustomer.Visible = False 
      End If 
      'If _Customer Is Nothing Then 
      pFault = RefreshCtlCustomer() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlCustomer.Customer.IsEmpty AndAlso _CustomerID <> -2 Then 
        pnlCustomer.Visible = False 
      End If 
      _ctlCustomer.Name = "ctlccCustomer" 
      _ActiveControl = _ctlCustomer 
      _ctlCustomer.BringToFront() 
      _ctlCustomer.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccCustomerCol" Then 
      lnkCustomerCol.ForeColor = Color.Black : lnkCustomerCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkCustomerCol.BackColor = Color.Wheat 
      If _ctlCustomerCol Is Nothing Then 
        _ctlCustomerCol = New ctlccCustomerCol() 
        _ctlCustomerCol.Dock = DockStyle.Fill 
        pnlCustomer.Controls.Add(_ctlCustomerCol) 
        _ctlCustomerCol.Visible = False 
      End If  
      pnlCustomer.Visible = True 
      If _CustomerCol Is Nothing Then 
        pFault = RefreshCtlCustomerCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlCustomerCol.Name = "ctlccCustomerCol" 
      _ActiveControl = _ctlCustomerCol 
      _ctlCustomerCol.BringToFront() 
      _ctlCustomerCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlccBeehiveBuyerTrackingCol" Then 
      lnkBeehiveBuyerTrackingCol.ForeColor = Color.Black : lnkBeehiveBuyerTrackingCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkBeehiveBuyerTrackingCol.BackColor = Color.Wheat 
      If _ctlBeehiveBuyerTrackingCol Is Nothing Then 
      _ctlBeehiveBuyerTrackingCol = New ctlccBeehiveBuyerTrackingCol() 
      _ctlBeehiveBuyerTrackingCol.Dock = DockStyle.Fill 
      pnlCustomer.Controls.Add(_ctlBeehiveBuyerTrackingCol) 
      _ctlBeehiveBuyerTrackingCol.Visible = False 
      End If  
      If _BeehiveBuyerTrackingCol Is Nothing Then 
        pFault = RefreshCtlBeehiveBuyerTrackingCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlBeehiveBuyerTrackingCol.Name = "ctlccBeehiveBuyerTrackingCol" 
      _ActiveControl = _ctlBeehiveBuyerTrackingCol 
      _ctlBeehiveBuyerTrackingCol.BringToFront() 
      _ctlBeehiveBuyerTrackingCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccCustomerDebtCol" Then 
      lnkCustomerDebtCol.ForeColor = Color.Black : lnkCustomerDebtCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkCustomerDebtCol.BackColor = Color.Wheat 
      If _ctlCustomerDebtCol Is Nothing Then 
      _ctlCustomerDebtCol = New ctlccCustomerDebtCol() 
      _ctlCustomerDebtCol.Dock = DockStyle.Fill 
      pnlCustomer.Controls.Add(_ctlCustomerDebtCol) 
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
    ElseIf pControlName = "ctlccOrderHeaderCol" Then 
      lnkOrderHeaderCol.ForeColor = Color.Black : lnkOrderHeaderCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkOrderHeaderCol.BackColor = Color.Wheat 
      If _ctlOrderHeaderCol Is Nothing Then 
      _ctlOrderHeaderCol = New ctlccOrderHeaderCol() 
      _ctlOrderHeaderCol.Dock = DockStyle.Fill 
      pnlCustomer.Controls.Add(_ctlOrderHeaderCol) 
      _ctlOrderHeaderCol.Visible = False 
      End If  
      If _OrderHeaderCol Is Nothing Then 
        pFault = RefreshCtlOrderHeaderCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlOrderHeaderCol.Name = "ctlccOrderHeaderCol" 
      _ActiveControl = _ctlOrderHeaderCol 
      _ctlOrderHeaderCol.BringToFront() 
      _ctlOrderHeaderCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-Customer-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("Customer", _Requester) 
 
    lnkCustomerCol.Text = CCTextTranslate("List", _Requester) 
    lnkCustomer.Text = CCTextTranslate("Details", _Requester) 
 
    lnkBeehiveBuyerTrackingCol.Text = TableNameTranslate("BeehiveBuyerTracking", _Requester, vMakePlural:=True) 
    lnkCustomerDebtCol.Text = TableNameTranslate("CustomerDebt", _Requester, vMakePlural:=True) 
    lnkOrderHeaderCol.Text = TableNameTranslate("OrderHeader", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlCustomer.Controls(0) Is _ctlCustomer Then 
      If _CustomerID = 0 Then 
        pnlCustomer.Visible = False 
      End If 
    ElseIf pnlCustomer.Controls(0) Is _ctlCustomerCol Then 
    ElseIf pnlCustomer.Controls(0) Is _ctlBeehiveBuyerTrackingCol Then 
    ElseIf pnlCustomer.Controls(0) Is _ctlCustomerDebtCol Then 
    ElseIf pnlCustomer.Controls(0) Is _ctlOrderHeaderCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pCustomerID As Long = _CustomerID 
      If ccHelper.IsNumeric(pText) Then _CustomerID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetCustomerIDFromIntelliComboText(pText) 
      If pCustomerID <> _CustomerID Then 
        _Customer = Nothing 
        pFault = ActivateControl("ctlccCustomer") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlCustomer.Controls(0) Is _ctlCustomer Then 
      pFault = RefreshCtlCustomer() 
    ElseIf pnlCustomer.Controls(0) Is _ctlCustomerCol Then 
      pFault = RefreshCtlCustomerCol() 
    ElseIf pnlCustomer.Controls(0) Is _ctlBeehiveBuyerTrackingCol Then 
      pFault = RefreshCtlBeehiveBuyerTrackingCol() 
    ElseIf pnlCustomer.Controls(0) Is _ctlCustomerDebtCol Then 
      pFault = RefreshCtlCustomerDebtCol() 
    ElseIf pnlCustomer.Controls(0) Is _ctlOrderHeaderCol Then 
      pFault = RefreshCtlOrderHeaderCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlCustomer.Controls(0).Name, "", "TRGT-Customer-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboCustomers(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlCustomerCol_evtRowClicked(ByVal vCustomer As Object) Handles _ctlCustomerCol.evtRowClicked 
    
    If vCustomer Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pCustomer As clsCustomer = CType(vCustomer, clsCustomer) 
    _CustomerID = pCustomer.ID 
    
    If _ActiveControl Is _ctlCustomerCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = clsCustomerCol.enmFillSumOnTheFlyParameters.GroupByCustomerType.ToString() Then 
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
    
    ChooseCustomer() 
    
    Try 
      MyIntelliCombo.ValueSelect(_CustomerID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pCustomer.CustomerName & " " & pCustomer.CustomerCode
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseCustomer() 
    _Customer = Nothing 
    lnkCustomer.Visible = True 
    _BeehiveBuyerTrackingCol = Nothing 
    lnkBeehiveBuyerTrackingCol.Visible = True 
    _CustomerDebtCol = Nothing 
    lnkCustomerDebtCol.Visible = True 
    _OrderHeaderCol = Nothing 
    lnkOrderHeaderCol.Visible = True 
  End Sub 
  Private Sub _ctlCustomerCol_evtRowDoubleClicked(ByVal vCustomer As clsCustomer, ByRef rHandled As Boolean) Handles _ctlCustomerCol.evtRowDoubleClicked 
    If lnkCustomer.Parent IsNot flpMenu Then Exit Sub 
    If vCustomer Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = clsCustomerCol.enmFillSumOnTheFlyParameters.GroupByCustomerType.ToString() Then 
        If pSearchFilters.ContainsKey(clsCustomerCol.enmFillOnTheFlyParameters.CustomerType) Then pSearchFilters.Remove(clsCustomerCol.enmFillOnTheFlyParameters.CustomerType) 
        pSearchFilters.Add(clsCustomerCol.enmFillOnTheFlyParameters.CustomerType, vCustomer.CustomerType) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreCustomerCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vCustomer.ID, vCustomer.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _CustomerID = vCustomer.ID 
      'MyIntelliCombo.ValueSelect(_CustomerID) 
      pFault = ActivateControl("ctlccCustomer") 
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
      pFault = _CustomerCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _CustomerCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _CustomerCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _CustomerCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccCustomerCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsCustomer.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Customer" 
      pFault = _ctlCustomerCol.LoadControl(_CustomerCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlCustomerCol_evtUnChosen() Handles _ctlCustomerCol.evtUnChosen 
 
    _CustomerID = 0 
    _Customer = Nothing 
    _BeehiveBuyerTrackingCol = Nothing 
    lnkBeehiveBuyerTrackingCol.Visible = False 
    _CustomerDebtCol = Nothing 
    lnkCustomerDebtCol.Visible = False 
    _OrderHeaderCol = Nothing 
    lnkOrderHeaderCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkCustomer.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkBeehiveBuyerTrackingCol.Click, 
      lnkCustomerDebtCol.Click, 
      lnkOrderHeaderCol.Click, 
      lnkCustomerCol.Click, 
      lnkCustomer.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkCustomer OrElse (lnk Is lnkCustomerCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlCustomerCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccCustomerCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsCustomer.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsCustomerCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillCustomerCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _CustomerCol = New clsCustomerCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _CustomerCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlCustomerCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case Else 
            If _ctlCustomerCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _CustomerCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlCustomerCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _CustomerCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _CustomerCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _CustomerCol.Count) 
      End If 
    Else 
      _CustomerCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _CustomerCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlCustomerCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Customer" 
    
    Dim pCustomerID As Long = _CustomerID 
    
    pFault = _ctlCustomerCol.LoadControl(_CustomerCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlCustomerCol.Visible = True 
    
    _ctlCustomerCol.Refresh() 
    If pCustomerID <> 0 Then 
      Dim pCustomerCol As clsCustomerCol = CType(_ctlCustomerCol.bsCtlCustomer.DataSource, clsCustomerCol) 
      Dim pCustomer As clsCustomer = pCustomerCol.FindByID(pCustomerID) 
      If pCustomer.ID > 0 Then 
        _ctlCustomerCol.bsCtlCustomer.CurrencyManager.Position = pCustomerCol.IndexOf(pCustomer) 
        _ctlCustomerCol.dgvCustomer.Rows(pCustomerCol.IndexOf(pCustomer)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlCustomer() As clsFault 
    Dim pFault As New clsFault 
    
    If _CustomerID > 0 Then 
      ChooseCustomer() 
      _Customer = New clsCustomer() 
      pFault = _Customer.GetByID(_CustomerID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _Customer = New clsCustomer() 
    End If 
    'lblSecondaryTitle.Text = _Customer.CustomerName & " " & _Customer.CustomerCode    
     
    Dim pLoadParameters As New ctlccCustomer.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlCustomer(pLoadParameters)
    pFault = _ctlCustomer.LoadControl(_Customer, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlCustomer.Visible = True 
    If _CustomerID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlCustomer.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlCustomer.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlBeehiveBuyerTrackingCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlBeehiveBuyerTrackingCol.dgvBeehiveBuyerTracking.SelectedRows.Count > 0 Then 
      Dim pBeehiveBuyerTracking As clsBeehiveBuyerTracking = CType(_ctlBeehiveBuyerTrackingCol.bsCtlBeehiveBuyerTracking.Current, clsBeehiveBuyerTracking) 
      pID = pBeehiveBuyerTracking.ID 
    End If 
 
    Dim pTestCol As clsBeehiveBuyerTrackingCol = Nothing 
    RaiseEvent evtOverrideFillBeehiveBuyerTrackingCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _BeehiveBuyerTrackingCol = New clsBeehiveBuyerTrackingCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _BeehiveBuyerTrackingCol.FillByCustomerID(_CustomerID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _BeehiveBuyerTrackingCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _BeehiveBuyerTrackingCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _BeehiveBuyerTrackingCol.Count) 
      End If 
    Else 
      _BeehiveBuyerTrackingCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _BeehiveBuyerTrackingCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlccBeehiveBuyerTrackingCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _Customer IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Customer.DefaultDesignation) Then 
        .ReportTitle = "List of BeehiveBuyerTrackings for " & _Customer.DefaultDesignation 
      Else 
        .ReportTitle = "List of BeehiveBuyerTrackings for Customer" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.Customer) 
    End With 
    RaiseEvent evtOverrideLoadCtlBeehiveBuyerTrackingCol(pLoadParameters)
    
    pFault = _ctlBeehiveBuyerTrackingCol.LoadControl(_BeehiveBuyerTrackingCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlBeehiveBuyerTrackingCol.Visible = True 
 
    If pID > 0 Then 
      Dim pBeehiveBuyerTrackings As clsBeehiveBuyerTrackingCol = CType(_ctlBeehiveBuyerTrackingCol.bsCtlBeehiveBuyerTracking.DataSource, clsBeehiveBuyerTrackingCol) 
      Dim pBeehiveBuyerTracking As clsBeehiveBuyerTracking = pBeehiveBuyerTrackings.FindByID((pID)) 
      If pBeehiveBuyerTracking.ID > 0 Then 
        _ctlBeehiveBuyerTrackingCol.bsCtlBeehiveBuyerTracking.CurrencyManager.Position = pBeehiveBuyerTrackings.IndexOf(pBeehiveBuyerTracking) 
        _ctlBeehiveBuyerTrackingCol.dgvBeehiveBuyerTracking.Rows(pBeehiveBuyerTrackings.IndexOf(pBeehiveBuyerTracking)).Selected = True 
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
      pFault = _CustomerDebtCol.FillByCustomerID(_CustomerID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
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
      If _Customer IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Customer.DefaultDesignation) Then 
        .ReportTitle = "List of CustomerDebts for " & _Customer.DefaultDesignation 
      Else 
        .ReportTitle = "List of CustomerDebts for Customer" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsCustomerDebt.enmProperty.Customer) 
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
  Private Function RefreshCtlOrderHeaderCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlOrderHeaderCol.dgvOrderHeader.SelectedRows.Count > 0 Then 
      Dim pOrderHeader As clsOrderHeader = CType(_ctlOrderHeaderCol.bsCtlOrderHeader.Current, clsOrderHeader) 
      pID = pOrderHeader.ID 
    End If 
 
    Dim pTestCol As clsOrderHeaderCol = Nothing 
    RaiseEvent evtOverrideFillOrderHeaderCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _OrderHeaderCol = New clsOrderHeaderCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _OrderHeaderCol.FillByCustomerID(_CustomerID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _OrderHeaderCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _OrderHeaderCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _OrderHeaderCol.Count) 
      End If 
    Else 
      _OrderHeaderCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _OrderHeaderCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlccOrderHeaderCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _Customer IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Customer.DefaultDesignation) Then 
        .ReportTitle = "List of OrderHeaders for " & _Customer.DefaultDesignation 
      Else 
        .ReportTitle = "List of OrderHeaders for Customer" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(clsOrderHeader.enmProperty.Customer) 
    End With 
    RaiseEvent evtOverrideLoadCtlOrderHeaderCol(pLoadParameters)
    
    pFault = _ctlOrderHeaderCol.LoadControl(_OrderHeaderCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlOrderHeaderCol.Visible = True 
 
    If pID > 0 Then 
      Dim pOrderHeaders As clsOrderHeaderCol = CType(_ctlOrderHeaderCol.bsCtlOrderHeader.DataSource, clsOrderHeaderCol) 
      Dim pOrderHeader As clsOrderHeader = pOrderHeaders.FindByID((pID)) 
      If pOrderHeader.ID > 0 Then 
        _ctlOrderHeaderCol.bsCtlOrderHeader.CurrencyManager.Position = pOrderHeaders.IndexOf(pOrderHeader) 
        _ctlOrderHeaderCol.dgvOrderHeader.Rows(pOrderHeaders.IndexOf(pOrderHeader)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlBeehiveBuyerTrackingCol_evtBeforeUpdate(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking, ByRef rCancel As Boolean) Handles _ctlBeehiveBuyerTrackingCol.evtBeforeUpdate 
    vBeehiveBuyerTracking.CustomerID = _Customer.ID 
  End Sub 
  Private Sub _ctlCustomerDebtCol_evtBeforeUpdate(ByVal vCustomerDebt As clsCustomerDebt, ByRef rCancel As Boolean) Handles _ctlCustomerDebtCol.evtBeforeUpdate 
    vCustomerDebt.CustomerID = _Customer.ID 
  End Sub 
  Private Sub _ctlOrderHeaderCol_evtBeforeUpdate(ByVal vOrderHeader As clsOrderHeader, ByRef rCancel As Boolean) Handles _ctlOrderHeaderCol.evtBeforeUpdate 
    vOrderHeader.CustomerID = _Customer.ID 
  End Sub 
  Private Sub _ctlCustomer_evtDeleted(ByVal vCustomerID As Long) Handles _ctlCustomer.evtDeleted 
    _CustomerCol = Nothing 
    Dim pFault As clsFault 
    _CustomerID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboCustomers(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlCustomer() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlCustomer.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkCustomerCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlCustomer_evtCancelledEdit(ByVal vCustomer As clsCustomer) Handles _ctlCustomer.evtCancelledEdit 
    RefreshCtlCustomer() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboCustomers(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlCustomer.btnAdd.Visible = False 
      If _CustomerID = 0 OrElse _CustomerID = -2 Then 
        pnlCustomer.Visible = False 
      Else 
        pnlCustomer.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlCustomer.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccCustomerCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlCustomer_evtUpdated(ByVal vWhichProperty As clsCustomer.enmUpdateType, ByVal vCustomer As clsCustomer) Handles _ctlCustomer.evtUpdated 
    _CustomerCol = Nothing 
    Dim pFault As clsFault 
    _CustomerID = CType(vCustomer, clsCustomer).ID 
    If _ActiveControl.Name = "ctlccCustomer" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboCustomers(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlCustomer() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlCustomer.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboCustomers(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccCustomerDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboCustomer(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
    If pComboList Is Nothing Then 
      If vRenewCache = True Then MyCache.ClearComboList(pComboListTypeToLoad) 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK Then Return pFault 
      If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.Customer Then 
        Dim pCombolistMember As clsComboListMember = pComboList.FindByKey(_Requester.UserIdentityInstanceID) 
        pComboList.Clear() 
        pComboList.Add(pCombolistMember) 
      End If 
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
 
    If _CustomerID >= 0 Then 
      MyIntelliCombo.ValueSelect(_CustomerID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_CustomerUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _CustomerID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _CustomerID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetCustomerIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _CustomerID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _CustomerID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _CustomerID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _CustomerID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseCustomer() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccCustomer", StringComparison.OrdinalIgnoreCase) AndAlso _CustomerID > 0 Then 
        'to avoid getting ObjectNotFound 
        _Customer = New clsCustomer() 
        pFault = _Customer.GetByID(_CustomerID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccCustomer") 
    End If 
    pnlCustomer.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  Private Sub _ctlBeehiveBuyerTrackingCol_evtRowDoubleClicked(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking, ByRef rHandled As Boolean) Handles _ctlBeehiveBuyerTrackingCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtBeehiveBuyerTrackingChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtBeehiveBuyerTrackingChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vBeehiveBuyerTracking.ID 
      .Object = New clsBeehiveBuyerTracking 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
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
  Private Sub _ctlOrderHeaderCol_evtRowDoubleClicked(ByVal vOrderHeader As clsOrderHeader, ByRef rHandled As Boolean) Handles _ctlOrderHeaderCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtOrderHeaderChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtOrderHeaderChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vOrderHeader.ID 
      .Object = New clsOrderHeader 
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
    pnlCustomer.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkCustomerCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _CustomerID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccCustomerCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkCustomerCol.Visible = False 
      _ActiveControl = _ctlCustomer 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboCustomers(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _CustomerID <> 0 Then 
        pFault = ActivateControl("ctlccCustomer") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlCustomer.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlCustomer.Visible = False 
        _CustomerID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _CustomerID > 0 Then pnlCustomer.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkBeehiveBuyerTrackingCol.MouseEnter, 
                  lnkCustomerDebtCol.MouseEnter, 
                  lnkOrderHeaderCol.MouseEnter, 
                  lnkCustomerCol.MouseEnter, 
                  lnkCustomer.MouseEnter, 
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
                  lnkBeehiveBuyerTrackingCol.MouseLeave, 
                  lnkCustomerDebtCol.MouseLeave, 
                  lnkOrderHeaderCol.MouseLeave, 
                  lnkCustomerCol.MouseLeave, 
                  lnkCustomer.MouseLeave, 
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
  Private Sub _ctlCustomer_evtAdd(ByVal vCustomer As clsCustomer) Handles _ctlCustomer.evtAdd 
    lnkBeehiveBuyerTrackingCol.Visible = False 
    lnkCustomerDebtCol.Visible = False 
    lnkOrderHeaderCol.Visible = False 
    lnkCustomerCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pCustomerCode As String = Nothing 
    Dim pCustomerCodeWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pCustomerType As clsEnums.enmCustomerType = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByCustomerType As Boolean = False 
    
    Dim pSumPaymentTermsDays As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Customers"  
  
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
        .String01Label.Text = If(_ctlCustomerCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomer.enmProperty.CustomerCode), _ctlCustomerCol.LoadParameters.ColumnsHeaderText(clsCustomer.enmProperty.CustomerCode), "Customer Code") 
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
 
        .Combo01Label.Text = If(_ctlCustomerCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomer.enmProperty.CustomerType), _ctlCustomerCol.LoadParameters.ColumnsHeaderText(clsCustomer.enmProperty.CustomerType), "Customer Type") 
        Dim pCustomerTypes As New clsComboList 
        pFault = pCustomerTypes.FillEnums(clsEnums.enmEnum.CustomerType, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pCustomerTypes.Remove(pCustomerTypes.FindByKey(clsEnums.enmCustomerType.UD)) 
        pCustomerTypes.SortByText() 
        If pCustomerTypes IsNot Nothing AndAlso pCustomerTypes.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo01Label) 
          .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo01 
          .MakeSmart() 
          .LoadControl(pCustomerTypes, GetChoose(_Requester)) 
          .TabIndex = 5 
        End With 
 
        .Text01Label.Text = If(_ctlCustomerCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomer.enmProperty.ID), _ctlCustomerCol.LoadParameters.ColumnsHeaderText(clsCustomer.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 6 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlCustomerCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomer.enmProperty.CustomerType), _ctlCustomerCol.LoadParameters.ColumnsHeaderText(clsCustomer.enmProperty.CustomerType), "Customer Type") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlCustomerCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomer.enmProperty.PaymentTermsDays), _ctlCustomerCol.LoadParameters.ColumnsHeaderText(clsCustomer.enmProperty.PaymentTermsDays), "Payment Terms Days") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 9 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
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
        pCustomerCode = .String01Text.Text 
        pCustomerCodeWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(clsCustomerCol.enmFillOnTheFlyParameters.CustomerCode, pCustomerCode) 
        _SearchFilters.Add(clsCustomerCol.enmFillOnTheFlyParameters.CustomerCodeWildcardType, pCustomerCodeWildcardType) 
      End If 
      If .Combo01.SelectedItem IsNot Nothing Then 
        pCustomerType = CType(CType(.Combo01.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmCustomerType) 
        _SearchFilters.Add(clsCustomerCol.enmFillOnTheFlyParameters.CustomerType, pCustomerType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(clsCustomerCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsCustomerCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsCustomerCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsCustomerCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByCustomerType = True 
        pDoSum = True 
        _SearchFilters.Add(clsCustomerCol.enmFillSumOnTheFlyParameters.GroupByCustomerType, pGroupByCustomerType) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumPaymentTermsDays = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsCustomerCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsCustomerCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsCustomerCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsCustomerCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccCustomerCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccCustomerCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsCustomer.enmProperty.ID, "ID") 
      End With 
      _CustomerCol = New clsCustomerCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _CustomerCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case Else 
            pFault = _CustomerCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _CustomerCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _CustomerCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _CustomerCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Customer" 
      RaiseEvent evtOverrideLoadCtlCustomerCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _CustomerCol = New clsCustomerCol() 
      pFault = _CustomerCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccCustomerCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _CustomerCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsCustomer.enmProperty.ID, "Count") 
        If pGroupByCustomerType = False Then .ColumnsHide.Add(clsCustomer.enmProperty.CustomerType) 
        If pSumPaymentTermsDays = False Then .ColumnsHide.Add(clsCustomer.enmProperty.PaymentTermsDays) 
        .ColumnsHide.Add(clsCustomer.enmProperty.CustomerCode) 
        .ColumnsHide.Add(clsCustomer.enmProperty.CustomerName) 
        .ColumnsHide.Add(clsCustomer.enmProperty.Phone) 
        .ColumnsHide.Add(clsCustomer.enmProperty.Email) 
        .ColumnsHide.Add(clsCustomer.enmProperty.Address) 
        .ColumnsHide.Add(clsCustomer.enmProperty.City) 
        .ColumnsHide.Add(clsCustomer.enmProperty.TaxID) 
        .ColumnsHide.Add(clsCustomer.enmProperty.Notes) 
        .ColumnsHide.Add(clsCustomer.enmProperty.IsActive) 
        .ColumnsHide.Add(clsCustomer.enmProperty.Location) 
        .ColumnsHide.Add(clsCustomer.enmProperty.AccountantEmail) 
        .ColumnsHide.Add(clsCustomer.enmProperty.AccountantMethod) 
        .ColumnsHide.Add(clsCustomer.enmProperty.InvoiceName) 
        .ColumnsHide.Add(clsCustomer.enmProperty.ProfitabilityCode) 
        .ColumnsHide.Add(clsCustomer.enmProperty.CustomerIdentifier) 
        .ColumnsHide.Add(clsCustomer.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlCustomerCol.Visible = True 
    pFault = _ctlCustomerCol.LoadControl(_CustomerCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsCustomerCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsCustomerCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlCustomer.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlCustomer.Controls(0).Name) 
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
    _CustomerID = -2 
    pFault = ActivateControl("ctlccCustomer") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlCustomer() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlCustomer.Visible = True 'new 
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
 
  Private Sub _ctlCustomerCol_evtTimerTripped() Handles _ctlCustomerCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtCustomerTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlCustomerCol.CustomerCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlCustomerCol.CustomerCol(0).ID 
 
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
    If _CustomerCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsCustomer() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsCustomerCol = CType(CallByName(_CustomerCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsCustomerCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsCustomerCol = CType(CallByName(_CustomerCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsCustomerCol) 
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
                  ccHelper.GetPropertyTypeName(New clsCustomerCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsCustomerCol = CType(CallByName(_CustomerCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsCustomerCol) 
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
 
  Private Sub cc_ctlPnlCustomer_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
