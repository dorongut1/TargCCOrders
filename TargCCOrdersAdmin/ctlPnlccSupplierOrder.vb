Public Class ctlPnlccSupplierOrder 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlSupplierOrderCol As ctlccSupplierOrderCol 
  Private WithEvents _ctlSupplierOrder As ctlccSupplierOrder 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _SupplierOrderID As Long 
 
  'The data holders 
  Private _SupplierOrderCol As clsSupplierOrderCol 
  Private _SupplierOrder As clsSupplierOrder 
 
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
  Public Event evtOverrideLoadCboSupplierOrder(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetSupplierOrderIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillSupplierOrderCol(ByRef rSupplierOrderCol As clsSupplierOrderCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlSupplierOrderCol(ByRef rLoadParameters As ctlccSupplierOrderCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlSupplierOrder(ByRef rLoadParameters As ctlccSupplierOrder.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreSupplierOrderCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtSupplierOrderTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  'Parents
  Private _CancelEvtOrderHeaderChosen As Boolean = False 
  Private _ShowPopForEvtOrderHeaderChosen As Boolean = False 
  
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
 
    lnkSupplierOrderCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkSupplierOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vSupplierOrderID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _SupplierOrderID = CType(vSupplierOrderID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlSupplierOrder.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkSupplierOrderCol.Visible = False 
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
      pFault = LoadCboSupplierOrders(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _SupplierOrderID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_SupplierOrderID) 
      End If 
      ChooseSupplierOrder() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccSupplierOrder") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _SupplierOrderID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _SupplierOrderID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlccSupplierOrder" OrElse pControlName = "ctlSupplierOrder" Then 
      lnkSupplierOrder.ForeColor = Color.Black : lnkSupplierOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkSupplierOrder.BackColor = Color.Wheat 
      If _ctlSupplierOrder Is Nothing Then 
        _ctlSupplierOrder = New ctlccSupplierOrder() 
        _ctlSupplierOrder.Dock = DockStyle.Fill 
        _ctlSupplierOrder.Controls.RemoveByKey("btnAdd") 
        pnlSupplierOrder.Controls.Add(_ctlSupplierOrder) 
        _ctlSupplierOrder.Visible = False 
      End If 
      If _SupplierOrderID = 0 Then 
        pnlSupplierOrder.Visible = False 
      End If 
      'If _SupplierOrder Is Nothing Then 
      pFault = RefreshCtlSupplierOrder() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlSupplierOrder.SupplierOrder.IsEmpty AndAlso _SupplierOrderID <> -2 Then 
        pnlSupplierOrder.Visible = False 
      End If 
      _ctlSupplierOrder.Name = "ctlccSupplierOrder" 
      _ActiveControl = _ctlSupplierOrder 
      _ctlSupplierOrder.BringToFront() 
      _ctlSupplierOrder.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccSupplierOrderCol" Then 
      lnkSupplierOrderCol.ForeColor = Color.Black : lnkSupplierOrderCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkSupplierOrderCol.BackColor = Color.Wheat 
      If _ctlSupplierOrderCol Is Nothing Then 
        _ctlSupplierOrderCol = New ctlccSupplierOrderCol() 
        _ctlSupplierOrderCol.Dock = DockStyle.Fill 
        pnlSupplierOrder.Controls.Add(_ctlSupplierOrderCol) 
        _ctlSupplierOrderCol.Visible = False 
      End If  
      pnlSupplierOrder.Visible = True 
      If _SupplierOrderCol Is Nothing Then 
        pFault = RefreshCtlSupplierOrderCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlSupplierOrderCol.Name = "ctlccSupplierOrderCol" 
      _ActiveControl = _ctlSupplierOrderCol 
      _ctlSupplierOrderCol.BringToFront() 
      _ctlSupplierOrderCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-SupplierOrder-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("SupplierOrder", _Requester) 
 
    lnkSupplierOrderCol.Text = CCTextTranslate("List", _Requester) 
    lnkSupplierOrder.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlSupplierOrder.Controls(0) Is _ctlSupplierOrder Then 
      If _SupplierOrderID = 0 Then 
        pnlSupplierOrder.Visible = False 
      End If 
    ElseIf pnlSupplierOrder.Controls(0) Is _ctlSupplierOrderCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pSupplierOrderID As Long = _SupplierOrderID 
      If ccHelper.IsNumeric(pText) Then _SupplierOrderID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetSupplierOrderIDFromIntelliComboText(pText) 
      If pSupplierOrderID <> _SupplierOrderID Then 
        _SupplierOrder = Nothing 
        pFault = ActivateControl("ctlccSupplierOrder") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlSupplierOrder.Controls(0) Is _ctlSupplierOrder Then 
      pFault = RefreshCtlSupplierOrder() 
    ElseIf pnlSupplierOrder.Controls(0) Is _ctlSupplierOrderCol Then 
      pFault = RefreshCtlSupplierOrderCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlSupplierOrder.Controls(0).Name, "", "TRGT-SupplierOrder-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboSupplierOrders(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlSupplierOrderCol_evtRowClicked(ByVal vSupplierOrder As Object) Handles _ctlSupplierOrderCol.evtRowClicked 
    
    If vSupplierOrder Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pSupplierOrder As clsSupplierOrder = CType(vSupplierOrder, clsSupplierOrder) 
    _SupplierOrderID = pSupplierOrder.ID 
    
    If _ActiveControl Is _ctlSupplierOrderCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = clsSupplierOrderCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsSupplierOrderCol.enmFillSumOnTheFlyParameters.GroupBySentDate.ToString() Then 
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
    
    ChooseSupplierOrder() 
    
    Try 
      MyIntelliCombo.ValueSelect(_SupplierOrderID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pSupplierOrder.ID.ToString("#,##0")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseSupplierOrder() 
    _SupplierOrder = Nothing 
    lnkSupplierOrder.Visible = True 
  End Sub 
  Private Sub _ctlSupplierOrderCol_evtRowDoubleClicked(ByVal vSupplierOrder As clsSupplierOrder, ByRef rHandled As Boolean) Handles _ctlSupplierOrderCol.evtRowDoubleClicked 
    If lnkSupplierOrder.Parent IsNot flpMenu Then Exit Sub 
    If vSupplierOrder Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = clsSupplierOrderCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID.ToString() Then 
        If pSearchFilters.ContainsKey(clsSupplierOrderCol.enmFillOnTheFlyParameters.OrderHeaderID) Then pSearchFilters.Remove(clsSupplierOrderCol.enmFillOnTheFlyParameters.OrderHeaderID) 
        pSearchFilters.Add(clsSupplierOrderCol.enmFillOnTheFlyParameters.OrderHeaderID, vSupplierOrder.OrderHeaderID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsSupplierOrderCol.enmFillSumOnTheFlyParameters.GroupBySentDate.ToString() Then 
        If pSearchFilters.ContainsKey(clsSupplierOrderCol.enmFillOnTheFlyParameters.SentDateStart) Then pSearchFilters.Remove(clsSupplierOrderCol.enmFillOnTheFlyParameters.SentDateStart) 
        If pSearchFilters.ContainsKey(clsSupplierOrderCol.enmFillOnTheFlyParameters.SentDateEnd) Then pSearchFilters.Remove(clsSupplierOrderCol.enmFillOnTheFlyParameters.SentDateEnd) 
        pSearchFilters.Add(clsSupplierOrderCol.enmFillOnTheFlyParameters.SentDateStart, vSupplierOrder.SentDate) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreSupplierOrderCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vSupplierOrder.ID, vSupplierOrder.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _SupplierOrderID = vSupplierOrder.ID 
      'MyIntelliCombo.ValueSelect(_SupplierOrderID) 
      pFault = ActivateControl("ctlccSupplierOrder") 
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
      pFault = _SupplierOrderCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _SupplierOrderCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _SupplierOrderCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _SupplierOrderCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccSupplierOrderCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsSupplierOrder.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see SupplierOrder" 
      pFault = _ctlSupplierOrderCol.LoadControl(_SupplierOrderCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlSupplierOrderCol_evtUnChosen() Handles _ctlSupplierOrderCol.evtUnChosen 
 
    _SupplierOrderID = 0 
    _SupplierOrder = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkSupplierOrder.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkSupplierOrderCol.Click, 
      lnkSupplierOrder.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkSupplierOrder OrElse (lnk Is lnkSupplierOrderCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlSupplierOrderCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccSupplierOrderCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsSupplierOrder.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsSupplierOrderCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillSupplierOrderCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _SupplierOrderCol = New clsSupplierOrderCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _SupplierOrderCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlSupplierOrderCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlSupplierOrderCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _SupplierOrderCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlSupplierOrderCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _SupplierOrderCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _SupplierOrderCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _SupplierOrderCol.Count) 
      End If 
    Else 
      _SupplierOrderCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _SupplierOrderCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlSupplierOrderCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see SupplierOrder" 
    
    Dim pSupplierOrderID As Long = _SupplierOrderID 
    
    pFault = _ctlSupplierOrderCol.LoadControl(_SupplierOrderCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlSupplierOrderCol.Visible = True 
    
    _ctlSupplierOrderCol.Refresh() 
    If pSupplierOrderID <> 0 Then 
      Dim pSupplierOrderCol As clsSupplierOrderCol = CType(_ctlSupplierOrderCol.bsCtlSupplierOrder.DataSource, clsSupplierOrderCol) 
      Dim pSupplierOrder As clsSupplierOrder = pSupplierOrderCol.FindByID(pSupplierOrderID) 
      If pSupplierOrder.ID > 0 Then 
        _ctlSupplierOrderCol.bsCtlSupplierOrder.CurrencyManager.Position = pSupplierOrderCol.IndexOf(pSupplierOrder) 
        _ctlSupplierOrderCol.dgvSupplierOrder.Rows(pSupplierOrderCol.IndexOf(pSupplierOrder)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlSupplierOrder() As clsFault 
    Dim pFault As New clsFault 
    
    If _SupplierOrderID > 0 Then 
      ChooseSupplierOrder() 
      _SupplierOrder = New clsSupplierOrder(clsEnums.enmLoadParent.TextOnly) 
      pFault = _SupplierOrder.GetByID(_SupplierOrderID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _SupplierOrder = New clsSupplierOrder(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _SupplierOrder.ID.ToString("#,##0")    
     
    Dim pLoadParameters As New ctlccSupplierOrder.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlSupplierOrder(pLoadParameters)
    pFault = _ctlSupplierOrder.LoadControl(_SupplierOrder, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlSupplierOrder.Visible = True 
    If _SupplierOrderID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlSupplierOrder.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlSupplierOrder.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlSupplierOrder_evtDeleted(ByVal vSupplierOrderID As Long) Handles _ctlSupplierOrder.evtDeleted 
    _SupplierOrderCol = Nothing 
    Dim pFault As clsFault 
    _SupplierOrderID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboSupplierOrders(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlSupplierOrder() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlSupplierOrder.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkSupplierOrderCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlSupplierOrder_evtCancelledEdit(ByVal vSupplierOrder As clsSupplierOrder) Handles _ctlSupplierOrder.evtCancelledEdit 
    RefreshCtlSupplierOrder() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboSupplierOrders(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlSupplierOrder.btnAdd.Visible = False 
      If _SupplierOrderID = 0 OrElse _SupplierOrderID = -2 Then 
        pnlSupplierOrder.Visible = False 
      Else 
        pnlSupplierOrder.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlSupplierOrder.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccSupplierOrderCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlSupplierOrder_evtUpdated(ByVal vWhichProperty As clsSupplierOrder.enmUpdateType, ByVal vSupplierOrder As clsSupplierOrder) Handles _ctlSupplierOrder.evtUpdated 
    _SupplierOrderCol = Nothing 
    Dim pFault As clsFault 
    _SupplierOrderID = CType(vSupplierOrder, clsSupplierOrder).ID 
    If _ActiveControl.Name = "ctlccSupplierOrder" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboSupplierOrders(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlSupplierOrder() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlSupplierOrder.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboSupplierOrders(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccSupplierOrderDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboSupplierOrder(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _SupplierOrderID >= 0 Then 
      MyIntelliCombo.ValueSelect(_SupplierOrderID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_SupplierOrderUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _SupplierOrderID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _SupplierOrderID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetSupplierOrderIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _SupplierOrderID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _SupplierOrderID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _SupplierOrderID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _SupplierOrderID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseSupplierOrder() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccSupplierOrder", StringComparison.OrdinalIgnoreCase) AndAlso _SupplierOrderID > 0 Then 
        'to avoid getting ObjectNotFound 
        _SupplierOrder = New clsSupplierOrder(clsEnums.enmLoadParent.TextOnly) 
        pFault = _SupplierOrder.GetByID(_SupplierOrderID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccSupplierOrder") 
    End If 
    pnlSupplierOrder.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As clsSupplierOrder.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlSupplierOrder.evtParentChosen 
    If vParentName = clsSupplierOrder.enmParentProperty.OrderHeader Then 
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
  End Sub 
   
  Private Sub chkGrid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrid.CheckedChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    
    Cursor = Cursors.WaitCursor 
    chkGrid.Enabled = False 
    pnlButtons.Visible = False 
    pnlSupplierOrder.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkSupplierOrderCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _SupplierOrderID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccSupplierOrderCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkSupplierOrderCol.Visible = False 
      _ActiveControl = _ctlSupplierOrder 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboSupplierOrders(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _SupplierOrderID <> 0 Then 
        MyIntelliCombo.cbo.Text = _SupplierOrderID.ToString() 
        pFault = ActivateControl("ctlccSupplierOrder") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlSupplierOrder.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlSupplierOrder.Visible = False 
        _SupplierOrderID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _SupplierOrderID > 0 Then pnlSupplierOrder.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkSupplierOrderCol.MouseEnter, 
                  lnkSupplierOrder.MouseEnter, 
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
                  lnkSupplierOrderCol.MouseLeave, 
                  lnkSupplierOrder.MouseLeave, 
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
  Private Sub _ctlSupplierOrder_evtAdd(ByVal vSupplierOrder As clsSupplierOrder) Handles _ctlSupplierOrder.evtAdd 
    lnkSupplierOrderCol.Visible = False 
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
    Dim pSentDateStart As Nullable(Of Date) = Nothing 
    Dim pSentDateEnd As Nullable(Of Date) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByOrderHeaderID As Boolean = False 
    Dim pGroupBySentDate As Boolean = False 
    
    Dim pSumTotalCost As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Supplier Orders"  
  
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
        .Combo01Label.Text = If(_ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsSupplierOrder.enmProperty.OrderHeader), _ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText(clsSupplierOrder.enmProperty.OrderHeader), "Order Header") 
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
 
        .Date01Label.Text = If(_ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsSupplierOrder.enmProperty.SentDate), _ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText(clsSupplierOrder.enmProperty.SentDate), "Sent Date") 
        .Date01From.TabIndex = 4 
        .Date01To.TabIndex = 5 
        .Date01From.ShowCheckBox = True 
        .Date01To.ShowCheckBox = True 
        .Date01From.Checked = False 
        .Date01To.Checked = False 
        If _ctlSupplierOrderCol.LoadParameters.ColumnsFormat.ContainsKey(clsSupplierOrder.enmProperty.SentDate) Then 
          .Date01From.CustomFormat = _ctlSupplierOrderCol.LoadParameters.ColumnsFormat(clsSupplierOrder.enmProperty.SentDate) 
          .Date01To.CustomFormat = _ctlSupplierOrderCol.LoadParameters.ColumnsFormat(clsSupplierOrder.enmProperty.SentDate) 
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
 
        .Text01Label.Text = If(_ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsSupplierOrder.enmProperty.ID), _ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText(clsSupplierOrder.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 6 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsSupplierOrder.enmProperty.OrderHeader), _ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText(clsSupplierOrder.enmProperty.OrderHeader), "Order Header") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsSupplierOrder.enmProperty.SentDate), _ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText(clsSupplierOrder.enmProperty.SentDate), "Sent Date") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsSupplierOrder.enmProperty.TotalCost), _ctlSupplierOrderCol.LoadParameters.ColumnsHeaderText(clsSupplierOrder.enmProperty.TotalCost), "Total Cost") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 10 
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
      If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
        pOrderHeaderID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
        _SearchFilters.Add(clsSupplierOrderCol.enmFillOnTheFlyParameters.OrderHeaderID, pOrderHeaderID) 
      End If 
      If .Date01From.Checked OrElse .Date01To.Checked Then 
        pSentDateStart = .Date01From.Value 
        pSentDateEnd = .Date01To.Value 
        _SearchFilters.Add(clsSupplierOrderCol.enmFillOnTheFlyParameters.SentDateStart, pSentDateStart) 
        _SearchFilters.Add(clsSupplierOrderCol.enmFillOnTheFlyParameters.SentDateEnd, pSentDateEnd) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(clsSupplierOrderCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsSupplierOrderCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsSupplierOrderCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsSupplierOrderCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByOrderHeaderID = True 
        pDoSum = True 
        _SearchFilters.Add(clsSupplierOrderCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID, pGroupByOrderHeaderID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupBySentDate = True 
        pDoSum = True 
        _SearchFilters.Add(clsSupplierOrderCol.enmFillSumOnTheFlyParameters.GroupBySentDate, pGroupBySentDate) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumTotalCost = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsSupplierOrderCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsSupplierOrderCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsSupplierOrderCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsSupplierOrderCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccSupplierOrderCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccSupplierOrderCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsSupplierOrder.enmProperty.ID, "ID") 
      End With 
      _SupplierOrderCol = New clsSupplierOrderCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _SupplierOrderCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _SupplierOrderCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _SupplierOrderCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _SupplierOrderCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _SupplierOrderCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see SupplierOrder" 
      RaiseEvent evtOverrideLoadCtlSupplierOrderCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _SupplierOrderCol = New clsSupplierOrderCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _SupplierOrderCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccSupplierOrderCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _SupplierOrderCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsSupplierOrder.enmProperty.ID, "Count") 
        If pGroupByOrderHeaderID = False Then .ColumnsHide.Add(clsSupplierOrder.enmProperty.OrderHeader) 
        If pGroupBySentDate = False Then .ColumnsHide.Add(clsSupplierOrder.enmProperty.SentDate) 
        If pSumTotalCost = False Then .ColumnsHide.Add(clsSupplierOrder.enmProperty.TotalCost) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.SupplierEmail) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.EmailSubject) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.EmailBody) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.EmailStatus) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.DeliveryMethod) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.RequestedDeliveryDate) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.RequestedDeliveryDay) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.Notes) 
        .ColumnsHide.Add(clsSupplierOrder.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlSupplierOrderCol.Visible = True 
    pFault = _ctlSupplierOrderCol.LoadControl(_SupplierOrderCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsSupplierOrderCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsSupplierOrderCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlSupplierOrder.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlSupplierOrder.Controls(0).Name) 
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
    _SupplierOrderID = -2 
    pFault = ActivateControl("ctlccSupplierOrder") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlSupplierOrder() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlSupplierOrder.Visible = True 'new 
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
 
  Private Sub _ctlSupplierOrderCol_evtTimerTripped() Handles _ctlSupplierOrderCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtSupplierOrderTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlSupplierOrderCol.SupplierOrderCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlSupplierOrderCol.SupplierOrderCol(0).ID 
 
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
    If _SupplierOrderCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsSupplierOrder() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsSupplierOrderCol = CType(CallByName(_SupplierOrderCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsSupplierOrderCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsSupplierOrderCol = CType(CallByName(_SupplierOrderCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsSupplierOrderCol) 
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
                  ccHelper.GetPropertyTypeName(New clsSupplierOrderCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsSupplierOrderCol = CType(CallByName(_SupplierOrderCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsSupplierOrderCol) 
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
 
  Private Sub cc_ctlPnlSupplierOrder_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
