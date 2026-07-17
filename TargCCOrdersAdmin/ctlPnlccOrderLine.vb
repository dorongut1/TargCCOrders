Public Class ctlPnlccOrderLine 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlOrderLineCol As ctlccOrderLineCol 
  Private WithEvents _ctlOrderLine As ctlccOrderLine 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _OrderLineID As Long 
 
  'The data holders 
  Private _OrderLineCol As clsOrderLineCol 
  Private _OrderLine As clsOrderLine 
 
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
  Public Event evtOverrideLoadCboOrderLine(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetOrderLineIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillOrderLineCol(ByRef rOrderLineCol As clsOrderLineCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlOrderLineCol(ByRef rLoadParameters As ctlccOrderLineCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlOrderLine(ByRef rLoadParameters As ctlccOrderLine.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreOrderLineCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtOrderLineTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  'Parents
  Private _CancelEvtOrderHeaderChosen As Boolean = False 
  Private _ShowPopForEvtOrderHeaderChosen As Boolean = False 
  Private _CancelEvtProductChosen As Boolean = False 
  Private _ShowPopForEvtProductChosen As Boolean = False 
  
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
 
    lnkOrderLineCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkOrderLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vOrderLineID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _OrderLineID = CType(vOrderLineID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlOrderLine.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkOrderLineCol.Visible = False 
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
      pFault = LoadCboOrderLines(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _OrderLineID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_OrderLineID) 
      End If 
      ChooseOrderLine() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccOrderLine") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _OrderLineID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlccOrderLine" OrElse pControlName = "ctlOrderLine" Then 
      lnkOrderLine.ForeColor = Color.Black : lnkOrderLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkOrderLine.BackColor = Color.Wheat 
      If _ctlOrderLine Is Nothing Then 
        _ctlOrderLine = New ctlccOrderLine() 
        _ctlOrderLine.Dock = DockStyle.Fill 
        _ctlOrderLine.Controls.RemoveByKey("btnAdd") 
        pnlOrderLine.Controls.Add(_ctlOrderLine) 
        _ctlOrderLine.Visible = False 
      End If 
      If _OrderLineID = 0 Then 
        pnlOrderLine.Visible = False 
      End If 
      'If _OrderLine Is Nothing Then 
      pFault = RefreshCtlOrderLine() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlOrderLine.OrderLine.IsEmpty AndAlso _OrderLineID <> -2 Then 
        pnlOrderLine.Visible = False 
      End If 
      _ctlOrderLine.Name = "ctlccOrderLine" 
      _ActiveControl = _ctlOrderLine 
      _ctlOrderLine.BringToFront() 
      _ctlOrderLine.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccOrderLineCol" Then 
      lnkOrderLineCol.ForeColor = Color.Black : lnkOrderLineCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkOrderLineCol.BackColor = Color.Wheat 
      If _ctlOrderLineCol Is Nothing Then 
        _ctlOrderLineCol = New ctlccOrderLineCol() 
        _ctlOrderLineCol.Dock = DockStyle.Fill 
        pnlOrderLine.Controls.Add(_ctlOrderLineCol) 
        _ctlOrderLineCol.Visible = False 
      End If  
      pnlOrderLine.Visible = True 
      If _OrderLineCol Is Nothing Then 
        pFault = RefreshCtlOrderLineCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlOrderLineCol.Name = "ctlccOrderLineCol" 
      _ActiveControl = _ctlOrderLineCol 
      _ctlOrderLineCol.BringToFront() 
      _ctlOrderLineCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-OrderLine-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("OrderLine", _Requester) 
 
    lnkOrderLineCol.Text = CCTextTranslate("List", _Requester) 
    lnkOrderLine.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlOrderLine.Controls(0) Is _ctlOrderLine Then 
      If _OrderLineID = 0 Then 
        pnlOrderLine.Visible = False 
      End If 
    ElseIf pnlOrderLine.Controls(0) Is _ctlOrderLineCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pOrderLineID As Long = _OrderLineID 
      If ccHelper.IsNumeric(pText) Then _OrderLineID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetOrderLineIDFromIntelliComboText(pText) 
      If pOrderLineID <> _OrderLineID Then 
        _OrderLine = Nothing 
        pFault = ActivateControl("ctlccOrderLine") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlOrderLine.Controls(0) Is _ctlOrderLine Then 
      pFault = RefreshCtlOrderLine() 
    ElseIf pnlOrderLine.Controls(0) Is _ctlOrderLineCol Then 
      pFault = RefreshCtlOrderLineCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlOrderLine.Controls(0).Name, "", "TRGT-OrderLine-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboOrderLines(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlOrderLineCol_evtRowClicked(ByVal vOrderLine As Object) Handles _ctlOrderLineCol.evtRowClicked 
    
    If vOrderLine Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pOrderLine As clsOrderLine = CType(vOrderLine, clsOrderLine) 
    _OrderLineID = pOrderLine.ID 
    
    If _ActiveControl Is _ctlOrderLineCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = clsOrderLineCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsOrderLineCol.enmFillSumOnTheFlyParameters.GroupByProductID.ToString() Then 
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
    
    ChooseOrderLine() 
    
    Try 
      MyIntelliCombo.ValueSelect(_OrderLineID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pOrderLine.ProductID.ToString("#,##0") & " " & pOrderLine.Quantity.ToString("#,##0")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseOrderLine() 
    _OrderLine = Nothing 
    lnkOrderLine.Visible = True 
  End Sub 
  Private Sub _ctlOrderLineCol_evtRowDoubleClicked(ByVal vOrderLine As clsOrderLine, ByRef rHandled As Boolean) Handles _ctlOrderLineCol.evtRowDoubleClicked 
    If lnkOrderLine.Parent IsNot flpMenu Then Exit Sub 
    If vOrderLine Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = clsOrderLineCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID.ToString() Then 
        If pSearchFilters.ContainsKey(clsOrderLineCol.enmFillOnTheFlyParameters.OrderHeaderID) Then pSearchFilters.Remove(clsOrderLineCol.enmFillOnTheFlyParameters.OrderHeaderID) 
        pSearchFilters.Add(clsOrderLineCol.enmFillOnTheFlyParameters.OrderHeaderID, vOrderLine.OrderHeaderID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsOrderLineCol.enmFillSumOnTheFlyParameters.GroupByProductID.ToString() Then 
        If pSearchFilters.ContainsKey(clsOrderLineCol.enmFillOnTheFlyParameters.ProductID) Then pSearchFilters.Remove(clsOrderLineCol.enmFillOnTheFlyParameters.ProductID) 
        pSearchFilters.Add(clsOrderLineCol.enmFillOnTheFlyParameters.ProductID, vOrderLine.ProductID) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreOrderLineCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vOrderLine.ID, vOrderLine.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _OrderLineID = vOrderLine.ID 
      'MyIntelliCombo.ValueSelect(_OrderLineID) 
      pFault = ActivateControl("ctlccOrderLine") 
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
      pFault = _OrderLineCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _OrderLineCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _OrderLineCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _OrderLineCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccOrderLineCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsOrderLine.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see OrderLine" 
      pFault = _ctlOrderLineCol.LoadControl(_OrderLineCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlOrderLineCol_evtUnChosen() Handles _ctlOrderLineCol.evtUnChosen 
 
    _OrderLineID = 0 
    _OrderLine = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkOrderLine.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkOrderLineCol.Click, 
      lnkOrderLine.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkOrderLine OrElse (lnk Is lnkOrderLineCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlOrderLineCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccOrderLineCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsOrderLine.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsOrderLineCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillOrderLineCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _OrderLineCol = New clsOrderLineCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _OrderLineCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlOrderLineCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlOrderLineCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _OrderLineCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlOrderLineCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _OrderLineCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _OrderLineCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _OrderLineCol.Count) 
      End If 
    Else 
      _OrderLineCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _OrderLineCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlOrderLineCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see OrderLine" 
    
    Dim pOrderLineID As Long = _OrderLineID 
    
    pFault = _ctlOrderLineCol.LoadControl(_OrderLineCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlOrderLineCol.Visible = True 
    
    _ctlOrderLineCol.Refresh() 
    If pOrderLineID <> 0 Then 
      Dim pOrderLineCol As clsOrderLineCol = CType(_ctlOrderLineCol.bsCtlOrderLine.DataSource, clsOrderLineCol) 
      Dim pOrderLine As clsOrderLine = pOrderLineCol.FindByID(pOrderLineID) 
      If pOrderLine.ID > 0 Then 
        _ctlOrderLineCol.bsCtlOrderLine.CurrencyManager.Position = pOrderLineCol.IndexOf(pOrderLine) 
        _ctlOrderLineCol.dgvOrderLine.Rows(pOrderLineCol.IndexOf(pOrderLine)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlOrderLine() As clsFault 
    Dim pFault As New clsFault 
    
    If _OrderLineID > 0 Then 
      ChooseOrderLine() 
      _OrderLine = New clsOrderLine(clsEnums.enmLoadParent.TextOnly) 
      pFault = _OrderLine.GetByID(_OrderLineID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _OrderLine = New clsOrderLine(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _OrderLine.ProductID.ToString("#,##0") & " " & _OrderLine.Quantity.ToString("#,##0")    
     
    Dim pLoadParameters As New ctlccOrderLine.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlOrderLine(pLoadParameters)
    pFault = _ctlOrderLine.LoadControl(_OrderLine, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlOrderLine.Visible = True 
    If _OrderLineID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlOrderLine.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlOrderLine.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlOrderLine_evtDeleted(ByVal vOrderLineID As Long) Handles _ctlOrderLine.evtDeleted 
    _OrderLineCol = Nothing 
    Dim pFault As clsFault 
    _OrderLineID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboOrderLines(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlOrderLine() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlOrderLine.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkOrderLineCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlOrderLine_evtCancelledEdit(ByVal vOrderLine As clsOrderLine) Handles _ctlOrderLine.evtCancelledEdit 
    RefreshCtlOrderLine() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboOrderLines(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlOrderLine.btnAdd.Visible = False 
      If _OrderLineID = 0 OrElse _OrderLineID = -2 Then 
        pnlOrderLine.Visible = False 
      Else 
        pnlOrderLine.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlOrderLine.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccOrderLineCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlOrderLine_evtUpdated(ByVal vWhichProperty As clsOrderLine.enmUpdateType, ByVal vOrderLine As clsOrderLine) Handles _ctlOrderLine.evtUpdated 
    _OrderLineCol = Nothing 
    Dim pFault As clsFault 
    _OrderLineID = CType(vOrderLine, clsOrderLine).ID 
    If _ActiveControl.Name = "ctlccOrderLine" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboOrderLines(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlOrderLine() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlOrderLine.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboOrderLines(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccOrderLineDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboOrderLine(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _OrderLineID >= 0 Then 
      MyIntelliCombo.ValueSelect(_OrderLineID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_OrderLineUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _OrderLineID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _OrderLineID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetOrderLineIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _OrderLineID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _OrderLineID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _OrderLineID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _OrderLineID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseOrderLine() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccOrderLine", StringComparison.OrdinalIgnoreCase) AndAlso _OrderLineID > 0 Then 
        'to avoid getting ObjectNotFound 
        _OrderLine = New clsOrderLine(clsEnums.enmLoadParent.TextOnly) 
        pFault = _OrderLine.GetByID(_OrderLineID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccOrderLine") 
    End If 
    pnlOrderLine.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As clsOrderLine.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlOrderLine.evtParentChosen 
    If vParentName = clsOrderLine.enmParentProperty.OrderHeader Then 
      rHandled = True 
      If _CancelEvtOrderHeaderChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New clsOrderHeader 
      End With 
      Try 
        RaiseEvent evtEntityChosen(Me, pEventArgs) 
      Catch ex As Exception 
        rHandled = False 
      End Try 
    End If 
    If vParentName = clsOrderLine.enmParentProperty.Product Then 
      rHandled = True 
      If _CancelEvtProductChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New clsProduct 
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
    pnlOrderLine.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkOrderLineCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _OrderLineID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccOrderLineCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkOrderLineCol.Visible = False 
      _ActiveControl = _ctlOrderLine 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboOrderLines(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _OrderLineID <> 0 Then 
        pFault = ActivateControl("ctlccOrderLine") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlOrderLine.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlOrderLine.Visible = False 
        _OrderLineID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _OrderLineID > 0 Then pnlOrderLine.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkOrderLineCol.MouseEnter, 
                  lnkOrderLine.MouseEnter, 
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
                  lnkOrderLine.MouseLeave, 
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
  Private Sub _ctlOrderLine_evtAdd(ByVal vOrderLine As clsOrderLine) Handles _ctlOrderLine.evtAdd 
    lnkOrderLineCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pOrderHeaderID As Nullable(Of Long) = Nothing 
    Dim pProductID As Nullable(Of Long) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByOrderHeaderID As Boolean = False 
    Dim pGroupByProductID As Boolean = False 
    
    Dim pSumQuantity As Boolean = False 
    Dim pSumUnitPrice As Boolean = False 
    Dim pSumDiscountPercent As Boolean = False 
    Dim pSumUnitCost As Boolean = False 
    Dim pSumLineNumber As Boolean = False 
    Dim pSumLineTotal As Boolean = False 
    Dim pSumTotalCost As Boolean = False 
    Dim pSumProfit As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Order Lines"  
  
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
        .Combo01Label.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.OrderHeader), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.OrderHeader), "Order Header") 
        Dim pOrderHeaders As New clsComboList 
        pFault = MyCache.GetComboList(clsEnums.enmComboListType.ccOrderHeaderDefaultByID, pOrderHeaders) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
        'If pOrderHeaders IsNot Nothing AndAlso pOrderHeaders.Count > 0 Then 
        .flpFilter.Controls.Add(.Combo01Label) 
        .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        'End If 
        With .Combo01 
          .MakeSmart() 
          If pOrderHeaders IsNot Nothing Then 
            .LoadControl(pOrderHeaders, GetChoose(_Requester)) 
          Else 
            .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.ccOrderHeaderDefaultByID, 0, _Requester) 
          End If 
          .TabIndex = 3 
        End With 
 
        .Combo02Label.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.Product), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.Product), "Product") 
        Dim pProducts As New clsComboList 
        pFault = MyCache.GetComboList(clsEnums.enmComboListType.ccProductDefaultByID, pProducts) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
        'If pProducts IsNot Nothing AndAlso pProducts.Count > 0 Then 
        .flpFilter.Controls.Add(.Combo02Label) 
        .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        'End If 
        With .Combo02 
          .MakeSmart() 
          If pProducts IsNot Nothing Then 
            .LoadControl(pProducts, GetChoose(_Requester)) 
          Else 
            .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.ccProductDefaultByID, 0, _Requester) 
          End If 
          .TabIndex = 4 
        End With 
 
        .Text01Label.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.ID), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 5 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 6 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.OrderHeader), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.OrderHeader), "Order Header") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 7 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.Product), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.Product), "Product") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.Quantity), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.Quantity), "Quantity") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 9 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.UnitPrice), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.UnitPrice), "Unit Price") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 10 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
        .lblSumField03.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.DiscountPercent), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.DiscountPercent), "Discount Percent") 
        .chkSumField03.Checked = False 
        .chkSumField03.TabIndex = 11 
        .flpSumColumns.Controls.Add(.lblSumField03) 
        .flpSumColumns.Controls.Add(.chkSumField03) 
 
        .lblSumField04.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.UnitCost), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.UnitCost), "Unit Cost") 
        .chkSumField04.Checked = False 
        .chkSumField04.TabIndex = 12 
        .flpSumColumns.Controls.Add(.lblSumField04) 
        .flpSumColumns.Controls.Add(.chkSumField04) 
 
        .lblSumField05.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.LineNumber), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.LineNumber), "Line Number") 
        .chkSumField05.Checked = False 
        .chkSumField05.TabIndex = 13 
        .flpSumColumns.Controls.Add(.lblSumField05) 
        .flpSumColumns.Controls.Add(.chkSumField05) 
 
        .lblSumField06.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.LineTotal), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.LineTotal), "Line Total") 
        .chkSumField06.Checked = False 
        .chkSumField06.TabIndex = 14 
        .flpSumColumns.Controls.Add(.lblSumField06) 
        .flpSumColumns.Controls.Add(.chkSumField06) 
 
        .lblSumField07.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.TotalCost), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.TotalCost), "Total Cost") 
        .chkSumField07.Checked = False 
        .chkSumField07.TabIndex = 15 
        .flpSumColumns.Controls.Add(.lblSumField07) 
        .flpSumColumns.Controls.Add(.chkSumField07) 
 
        .lblSumField08.Text = If(_ctlOrderLineCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsOrderLine.enmProperty.Profit), _ctlOrderLineCol.LoadParameters.ColumnsHeaderText(clsOrderLine.enmProperty.Profit), "Profit") 
        .chkSumField08.Checked = False 
        .chkSumField08.TabIndex = 16 
        .flpSumColumns.Controls.Add(.lblSumField08) 
        .flpSumColumns.Controls.Add(.chkSumField08) 
 
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
      If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
        pOrderHeaderID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
        _SearchFilters.Add(clsOrderLineCol.enmFillOnTheFlyParameters.OrderHeaderID, pOrderHeaderID) 
      End If 
      If .Combo02.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo02.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
        pProductID = CType(.Combo02.SelectedItem, clsComboListMember).KeyLong 
        _SearchFilters.Add(clsOrderLineCol.enmFillOnTheFlyParameters.ProductID, pProductID) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(clsOrderLineCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsOrderLineCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsOrderLineCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsOrderLineCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByOrderHeaderID = True 
        pDoSum = True 
        _SearchFilters.Add(clsOrderLineCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID, pGroupByOrderHeaderID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByProductID = True 
        pDoSum = True 
        _SearchFilters.Add(clsOrderLineCol.enmFillSumOnTheFlyParameters.GroupByProductID, pGroupByProductID) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumQuantity = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumUnitPrice = True 
        pDoSum = True 
      End If 
      
      If .chkSumField03.Checked = True Then 
        pSumDiscountPercent = True 
        pDoSum = True 
      End If 
      
      If .chkSumField04.Checked = True Then 
        pSumUnitCost = True 
        pDoSum = True 
      End If 
      
      If .chkSumField05.Checked = True Then 
        pSumLineNumber = True 
        pDoSum = True 
      End If 
      
      If .chkSumField06.Checked = True Then 
        pSumLineTotal = True 
        pDoSum = True 
      End If 
      
      If .chkSumField07.Checked = True Then 
        pSumTotalCost = True 
        pDoSum = True 
      End If 
      
      If .chkSumField08.Checked = True Then 
        pSumProfit = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsOrderLineCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsOrderLineCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsOrderLineCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsOrderLineCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccOrderLineCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccOrderLineCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsOrderLine.enmProperty.ID, "ID") 
      End With 
      _OrderLineCol = New clsOrderLineCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _OrderLineCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _OrderLineCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _OrderLineCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _OrderLineCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _OrderLineCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see OrderLine" 
      RaiseEvent evtOverrideLoadCtlOrderLineCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _OrderLineCol = New clsOrderLineCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _OrderLineCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccOrderLineCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _OrderLineCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsOrderLine.enmProperty.ID, "Count") 
        If pGroupByOrderHeaderID = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.OrderHeader) 
        If pGroupByProductID = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.Product) 
        If pSumQuantity = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.Quantity) 
        If pSumUnitPrice = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.UnitPrice) 
        If pSumDiscountPercent = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.DiscountPercent) 
        If pSumUnitCost = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.UnitCost) 
        If pSumLineNumber = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.LineNumber) 
        If pSumLineTotal = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.LineTotal) 
        If pSumTotalCost = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.TotalCost) 
        If pSumProfit = False Then .ColumnsHide.Add(clsOrderLine.enmProperty.Profit) 
        .ColumnsHide.Add(clsOrderLine.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlOrderLineCol.Visible = True 
    pFault = _ctlOrderLineCol.LoadControl(_OrderLineCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsOrderLineCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsOrderLineCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlOrderLine.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlOrderLine.Controls(0).Name) 
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
    _OrderLineID = -2 
    pFault = ActivateControl("ctlccOrderLine") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlOrderLine() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlOrderLine.Visible = True 'new 
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
 
  Private Sub _ctlOrderLineCol_evtTimerTripped() Handles _ctlOrderLineCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtOrderLineTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlOrderLineCol.OrderLineCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlOrderLineCol.OrderLineCol(0).ID 
 
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
    If _OrderLineCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsOrderLine() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsOrderLineCol = CType(CallByName(_OrderLineCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsOrderLineCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsOrderLineCol = CType(CallByName(_OrderLineCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsOrderLineCol) 
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
                  ccHelper.GetPropertyTypeName(New clsOrderLineCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsOrderLineCol = CType(CallByName(_OrderLineCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsOrderLineCol) 
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
 
  Private Sub cc_ctlPnlOrderLine_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
