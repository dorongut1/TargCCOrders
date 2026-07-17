Public Class ctlPnlccDelivery 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlDeliveryCol As ctlccDeliveryCol 
  Private WithEvents _ctlDelivery As ctlccDelivery 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _DeliveryID As Long 
 
  'The data holders 
  Private _DeliveryCol As clsDeliveryCol 
  Private _Delivery As clsDelivery 
 
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
  Public Event evtOverrideLoadCboDelivery(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetDeliveryIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillDeliveryCol(ByRef rDeliveryCol As clsDeliveryCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlDeliveryCol(ByRef rLoadParameters As ctlccDeliveryCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlDelivery(ByRef rLoadParameters As ctlccDelivery.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreDeliveryCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtDeliveryTimerTripped(ByRef rCancel As Boolean) 
  
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
 
    lnkDeliveryCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkDelivery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vDeliveryID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _DeliveryID = CType(vDeliveryID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlDelivery.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkDeliveryCol.Visible = False 
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
      pFault = LoadCboDeliverys(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _DeliveryID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_DeliveryID) 
      End If 
      ChooseDelivery() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccDelivery") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _DeliveryID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _DeliveryID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlccDelivery" OrElse pControlName = "ctlDelivery" Then 
      lnkDelivery.ForeColor = Color.Black : lnkDelivery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkDelivery.BackColor = Color.Wheat 
      If _ctlDelivery Is Nothing Then 
        _ctlDelivery = New ctlccDelivery() 
        _ctlDelivery.Dock = DockStyle.Fill 
        _ctlDelivery.Controls.RemoveByKey("btnAdd") 
        pnlDelivery.Controls.Add(_ctlDelivery) 
        _ctlDelivery.Visible = False 
      End If 
      If _DeliveryID = 0 Then 
        pnlDelivery.Visible = False 
      End If 
      'If _Delivery Is Nothing Then 
      pFault = RefreshCtlDelivery() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlDelivery.Delivery.IsEmpty AndAlso _DeliveryID <> -2 Then 
        pnlDelivery.Visible = False 
      End If 
      _ctlDelivery.Name = "ctlccDelivery" 
      _ActiveControl = _ctlDelivery 
      _ctlDelivery.BringToFront() 
      _ctlDelivery.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccDeliveryCol" Then 
      lnkDeliveryCol.ForeColor = Color.Black : lnkDeliveryCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkDeliveryCol.BackColor = Color.Wheat 
      If _ctlDeliveryCol Is Nothing Then 
        _ctlDeliveryCol = New ctlccDeliveryCol() 
        _ctlDeliveryCol.Dock = DockStyle.Fill 
        pnlDelivery.Controls.Add(_ctlDeliveryCol) 
        _ctlDeliveryCol.Visible = False 
      End If  
      pnlDelivery.Visible = True 
      If _DeliveryCol Is Nothing Then 
        pFault = RefreshCtlDeliveryCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlDeliveryCol.Name = "ctlccDeliveryCol" 
      _ActiveControl = _ctlDeliveryCol 
      _ctlDeliveryCol.BringToFront() 
      _ctlDeliveryCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-Delivery-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("Delivery", _Requester) 
 
    lnkDeliveryCol.Text = CCTextTranslate("List", _Requester) 
    lnkDelivery.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlDelivery.Controls(0) Is _ctlDelivery Then 
      If _DeliveryID = 0 Then 
        pnlDelivery.Visible = False 
      End If 
    ElseIf pnlDelivery.Controls(0) Is _ctlDeliveryCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pDeliveryID As Long = _DeliveryID 
      If ccHelper.IsNumeric(pText) Then _DeliveryID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetDeliveryIDFromIntelliComboText(pText) 
      If pDeliveryID <> _DeliveryID Then 
        _Delivery = Nothing 
        pFault = ActivateControl("ctlccDelivery") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlDelivery.Controls(0) Is _ctlDelivery Then 
      pFault = RefreshCtlDelivery() 
    ElseIf pnlDelivery.Controls(0) Is _ctlDeliveryCol Then 
      pFault = RefreshCtlDeliveryCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlDelivery.Controls(0).Name, "", "TRGT-Delivery-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboDeliverys(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlDeliveryCol_evtRowClicked(ByVal vDelivery As Object) Handles _ctlDeliveryCol.evtRowClicked 
    
    If vDelivery Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pDelivery As clsDelivery = CType(vDelivery, clsDelivery) 
    _DeliveryID = pDelivery.ID 
    
    If _ActiveControl Is _ctlDeliveryCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByArrivalToCustomerDate.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByDeliveryStatus.ToString() Then 
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
    
    ChooseDelivery() 
    
    Try 
      MyIntelliCombo.ValueSelect(_DeliveryID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pDelivery.ID.ToString("#,##0")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseDelivery() 
    _Delivery = Nothing 
    lnkDelivery.Visible = True 
  End Sub 
  Private Sub _ctlDeliveryCol_evtRowDoubleClicked(ByVal vDelivery As clsDelivery, ByRef rHandled As Boolean) Handles _ctlDeliveryCol.evtRowDoubleClicked 
    If lnkDelivery.Parent IsNot flpMenu Then Exit Sub 
    If vDelivery Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID.ToString() Then 
        If pSearchFilters.ContainsKey(clsDeliveryCol.enmFillOnTheFlyParameters.OrderHeaderID) Then pSearchFilters.Remove(clsDeliveryCol.enmFillOnTheFlyParameters.OrderHeaderID) 
        pSearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.OrderHeaderID, vDelivery.OrderHeaderID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByArrivalToCustomerDate.ToString() Then 
        If pSearchFilters.ContainsKey(clsDeliveryCol.enmFillOnTheFlyParameters.ArrivalToCustomerDateStart) Then pSearchFilters.Remove(clsDeliveryCol.enmFillOnTheFlyParameters.ArrivalToCustomerDateStart) 
        If pSearchFilters.ContainsKey(clsDeliveryCol.enmFillOnTheFlyParameters.ArrivalToCustomerDateEnd) Then pSearchFilters.Remove(clsDeliveryCol.enmFillOnTheFlyParameters.ArrivalToCustomerDateEnd) 
        pSearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.ArrivalToCustomerDateStart, vDelivery.ArrivalToCustomerDate) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByDeliveryStatus.ToString() Then 
        If pSearchFilters.ContainsKey(clsDeliveryCol.enmFillOnTheFlyParameters.DeliveryStatus) Then pSearchFilters.Remove(clsDeliveryCol.enmFillOnTheFlyParameters.DeliveryStatus) 
        pSearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.DeliveryStatus, vDelivery.DeliveryStatus) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreDeliveryCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vDelivery.ID, vDelivery.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _DeliveryID = vDelivery.ID 
      'MyIntelliCombo.ValueSelect(_DeliveryID) 
      pFault = ActivateControl("ctlccDelivery") 
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
      pFault = _DeliveryCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _DeliveryCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _DeliveryCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _DeliveryCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccDeliveryCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsDelivery.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Delivery" 
      pFault = _ctlDeliveryCol.LoadControl(_DeliveryCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlDeliveryCol_evtUnChosen() Handles _ctlDeliveryCol.evtUnChosen 
 
    _DeliveryID = 0 
    _Delivery = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkDelivery.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkDeliveryCol.Click, 
      lnkDelivery.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkDelivery OrElse (lnk Is lnkDeliveryCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlDeliveryCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccDeliveryCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsDelivery.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsDeliveryCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillDeliveryCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _DeliveryCol = New clsDeliveryCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _DeliveryCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlDeliveryCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlDeliveryCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _DeliveryCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlDeliveryCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _DeliveryCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _DeliveryCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _DeliveryCol.Count) 
      End If 
    Else 
      _DeliveryCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _DeliveryCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlDeliveryCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Delivery" 
    
    Dim pDeliveryID As Long = _DeliveryID 
    
    pFault = _ctlDeliveryCol.LoadControl(_DeliveryCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlDeliveryCol.Visible = True 
    
    _ctlDeliveryCol.Refresh() 
    If pDeliveryID <> 0 Then 
      Dim pDeliveryCol As clsDeliveryCol = CType(_ctlDeliveryCol.bsCtlDelivery.DataSource, clsDeliveryCol) 
      Dim pDelivery As clsDelivery = pDeliveryCol.FindByID(pDeliveryID) 
      If pDelivery.ID > 0 Then 
        _ctlDeliveryCol.bsCtlDelivery.CurrencyManager.Position = pDeliveryCol.IndexOf(pDelivery) 
        _ctlDeliveryCol.dgvDelivery.Rows(pDeliveryCol.IndexOf(pDelivery)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlDelivery() As clsFault 
    Dim pFault As New clsFault 
    
    If _DeliveryID > 0 Then 
      ChooseDelivery() 
      _Delivery = New clsDelivery(clsEnums.enmLoadParent.TextOnly) 
      pFault = _Delivery.GetByID(_DeliveryID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _Delivery = New clsDelivery(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _Delivery.ID.ToString("#,##0")    
     
    Dim pLoadParameters As New ctlccDelivery.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlDelivery(pLoadParameters)
    pFault = _ctlDelivery.LoadControl(_Delivery, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlDelivery.Visible = True 
    If _DeliveryID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlDelivery.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlDelivery.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlDelivery_evtDeleted(ByVal vDeliveryID As Long) Handles _ctlDelivery.evtDeleted 
    _DeliveryCol = Nothing 
    Dim pFault As clsFault 
    _DeliveryID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboDeliverys(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlDelivery() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlDelivery.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkDeliveryCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlDelivery_evtCancelledEdit(ByVal vDelivery As clsDelivery) Handles _ctlDelivery.evtCancelledEdit 
    RefreshCtlDelivery() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboDeliverys(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlDelivery.btnAdd.Visible = False 
      If _DeliveryID = 0 OrElse _DeliveryID = -2 Then 
        pnlDelivery.Visible = False 
      Else 
        pnlDelivery.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlDelivery.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccDeliveryCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlDelivery_evtUpdated(ByVal vWhichProperty As clsDelivery.enmUpdateType, ByVal vDelivery As clsDelivery) Handles _ctlDelivery.evtUpdated 
    _DeliveryCol = Nothing 
    Dim pFault As clsFault 
    _DeliveryID = CType(vDelivery, clsDelivery).ID 
    If _ActiveControl.Name = "ctlccDelivery" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboDeliverys(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlDelivery() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlDelivery.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboDeliverys(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccDeliveryDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboDelivery(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _DeliveryID >= 0 Then 
      MyIntelliCombo.ValueSelect(_DeliveryID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_DeliveryUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _DeliveryID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _DeliveryID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetDeliveryIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _DeliveryID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _DeliveryID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _DeliveryID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _DeliveryID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseDelivery() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccDelivery", StringComparison.OrdinalIgnoreCase) AndAlso _DeliveryID > 0 Then 
        'to avoid getting ObjectNotFound 
        _Delivery = New clsDelivery(clsEnums.enmLoadParent.TextOnly) 
        pFault = _Delivery.GetByID(_DeliveryID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccDelivery") 
    End If 
    pnlDelivery.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As clsDelivery.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlDelivery.evtParentChosen 
    If vParentName = clsDelivery.enmParentProperty.OrderHeader Then 
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
    pnlDelivery.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkDeliveryCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _DeliveryID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccDeliveryCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkDeliveryCol.Visible = False 
      _ActiveControl = _ctlDelivery 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboDeliverys(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _DeliveryID <> 0 Then 
        MyIntelliCombo.cbo.Text = _DeliveryID.ToString() 
        pFault = ActivateControl("ctlccDelivery") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlDelivery.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlDelivery.Visible = False 
        _DeliveryID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _DeliveryID > 0 Then pnlDelivery.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkDeliveryCol.MouseEnter, 
                  lnkDelivery.MouseEnter, 
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
                  lnkDeliveryCol.MouseLeave, 
                  lnkDelivery.MouseLeave, 
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
  Private Sub _ctlDelivery_evtAdd(ByVal vDelivery As clsDelivery) Handles _ctlDelivery.evtAdd 
    lnkDeliveryCol.Visible = False 
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
    Dim pArrivalToCustomerDateStart As Nullable(Of Date) = Nothing 
    Dim pArrivalToCustomerDateEnd As Nullable(Of Date) = Nothing 
    Dim pDeliveryStatus As clsEnums.enmDeliveryStatus = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByOrderHeaderID As Boolean = False 
    Dim pGroupByArrivalToCustomerDate As Boolean = False 
    Dim pGroupByDeliveryStatus As Boolean = False 
    
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Deliveries"  
  
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
        .Combo01Label.Text = If(_ctlDeliveryCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsDelivery.enmProperty.OrderHeader), _ctlDeliveryCol.LoadParameters.ColumnsHeaderText(clsDelivery.enmProperty.OrderHeader), "Order Header") 
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
 
        .Date01Label.Text = If(_ctlDeliveryCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsDelivery.enmProperty.ArrivalToCustomerDate), _ctlDeliveryCol.LoadParameters.ColumnsHeaderText(clsDelivery.enmProperty.ArrivalToCustomerDate), "Arrival To Customer Date") 
        .Date01From.TabIndex = 4 
        .Date01To.TabIndex = 5 
        .Date01From.ShowCheckBox = True 
        .Date01To.ShowCheckBox = True 
        .Date01From.Checked = False 
        .Date01To.Checked = False 
        If _ctlDeliveryCol.LoadParameters.ColumnsFormat.ContainsKey(clsDelivery.enmProperty.ArrivalToCustomerDate) Then 
          .Date01From.CustomFormat = _ctlDeliveryCol.LoadParameters.ColumnsFormat(clsDelivery.enmProperty.ArrivalToCustomerDate) 
          .Date01To.CustomFormat = _ctlDeliveryCol.LoadParameters.ColumnsFormat(clsDelivery.enmProperty.ArrivalToCustomerDate) 
        Else 
          .Date01From.CustomFormat = "dd-MM-yyyy" 
          .Date01To.CustomFormat = "dd-MM-yyyy" 
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
 
        .Combo02Label.Text = If(_ctlDeliveryCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsDelivery.enmProperty.DeliveryStatus), _ctlDeliveryCol.LoadParameters.ColumnsHeaderText(clsDelivery.enmProperty.DeliveryStatus), "Delivery Status") 
        Dim pDeliveryStatuss As New clsComboList 
        pFault = pDeliveryStatuss.FillEnums(clsEnums.enmEnum.DeliveryStatus, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pDeliveryStatuss.Remove(pDeliveryStatuss.FindByKey(clsEnums.enmDeliveryStatus.UD)) 
        pDeliveryStatuss.SortByText() 
        If pDeliveryStatuss IsNot Nothing AndAlso pDeliveryStatuss.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo02Label) 
          .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo02 
          .MakeSmart() 
          .LoadControl(pDeliveryStatuss, GetChoose(_Requester)) 
          .TabIndex = 6 
        End With 
 
        .Text01Label.Text = If(_ctlDeliveryCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsDelivery.enmProperty.ID), _ctlDeliveryCol.LoadParameters.ColumnsHeaderText(clsDelivery.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 7 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 8 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlDeliveryCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsDelivery.enmProperty.OrderHeader), _ctlDeliveryCol.LoadParameters.ColumnsHeaderText(clsDelivery.enmProperty.OrderHeader), "Order Header") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlDeliveryCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsDelivery.enmProperty.ArrivalToCustomerDate), _ctlDeliveryCol.LoadParameters.ColumnsHeaderText(clsDelivery.enmProperty.ArrivalToCustomerDate), "Arrival To Customer Date") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 10 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlDeliveryCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsDelivery.enmProperty.DeliveryStatus), _ctlDeliveryCol.LoadParameters.ColumnsHeaderText(clsDelivery.enmProperty.DeliveryStatus), "Delivery Status") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 11 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .flpSumColumns.Hide() 
 
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
        _SearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.OrderHeaderID, pOrderHeaderID) 
      End If 
      If .Date01From.Checked OrElse .Date01To.Checked Then 
        pArrivalToCustomerDateStart = .Date01From.Value 
        pArrivalToCustomerDateEnd = .Date01To.Value 
        _SearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.ArrivalToCustomerDateStart, pArrivalToCustomerDateStart) 
        _SearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.ArrivalToCustomerDateEnd, pArrivalToCustomerDateEnd) 
      End If 
      If .Combo02.SelectedItem IsNot Nothing Then 
        pDeliveryStatus = CType(CType(.Combo02.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmDeliveryStatus) 
        _SearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.DeliveryStatus, pDeliveryStatus) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsDeliveryCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsDeliveryCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsDeliveryCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByOrderHeaderID = True 
        pDoSum = True 
        _SearchFilters.Add(clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID, pGroupByOrderHeaderID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByArrivalToCustomerDate = True 
        pDoSum = True 
        _SearchFilters.Add(clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByArrivalToCustomerDate, pGroupByArrivalToCustomerDate) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByDeliveryStatus = True 
        pDoSum = True 
        _SearchFilters.Add(clsDeliveryCol.enmFillSumOnTheFlyParameters.GroupByDeliveryStatus, pGroupByDeliveryStatus) 
      End If 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsDeliveryCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsDeliveryCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsDeliveryCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsDeliveryCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccDeliveryCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccDeliveryCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsDelivery.enmProperty.ID, "ID") 
      End With 
      _DeliveryCol = New clsDeliveryCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _DeliveryCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _DeliveryCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _DeliveryCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _DeliveryCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _DeliveryCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Delivery" 
      RaiseEvent evtOverrideLoadCtlDeliveryCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _DeliveryCol = New clsDeliveryCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _DeliveryCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccDeliveryCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _DeliveryCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsDelivery.enmProperty.ID, "Count") 
        If pGroupByOrderHeaderID = False Then .ColumnsHide.Add(clsDelivery.enmProperty.OrderHeader) 
        If pGroupByArrivalToCustomerDate = False Then .ColumnsHide.Add(clsDelivery.enmProperty.ArrivalToCustomerDate) 
        If pGroupByDeliveryStatus = False Then .ColumnsHide.Add(clsDelivery.enmProperty.DeliveryStatus) 
        .ColumnsHide.Add(clsDelivery.enmProperty.DeliveryAddress) 
        .ColumnsHide.Add(clsDelivery.enmProperty.ContactPhone) 
        .ColumnsHide.Add(clsDelivery.enmProperty.ContactName) 
        .ColumnsHide.Add(clsDelivery.enmProperty.DeliveryMethod) 
        .ColumnsHide.Add(clsDelivery.enmProperty.OrderedDate) 
        .ColumnsHide.Add(clsDelivery.enmProperty.ReceivedDate) 
        .ColumnsHide.Add(clsDelivery.enmProperty.ArrivalToHubDate) 
        .ColumnsHide.Add(clsDelivery.enmProperty.Location) 
        .ColumnsHide.Add(clsDelivery.enmProperty.ProductsSummary) 
        .ColumnsHide.Add(clsDelivery.enmProperty.Notes) 
        .ColumnsHide.Add(clsDelivery.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlDeliveryCol.Visible = True 
    pFault = _ctlDeliveryCol.LoadControl(_DeliveryCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsDeliveryCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsDeliveryCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlDelivery.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlDelivery.Controls(0).Name) 
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
    _DeliveryID = -2 
    pFault = ActivateControl("ctlccDelivery") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlDelivery() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlDelivery.Visible = True 'new 
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
 
  Private Sub _ctlDeliveryCol_evtTimerTripped() Handles _ctlDeliveryCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtDeliveryTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlDeliveryCol.DeliveryCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlDeliveryCol.DeliveryCol(0).ID 
 
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
    If _DeliveryCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsDelivery() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsDeliveryCol = CType(CallByName(_DeliveryCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsDeliveryCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsDeliveryCol = CType(CallByName(_DeliveryCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsDeliveryCol) 
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
                  ccHelper.GetPropertyTypeName(New clsDeliveryCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsDeliveryCol = CType(CallByName(_DeliveryCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsDeliveryCol) 
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
 
  Private Sub cc_ctlPnlDelivery_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
