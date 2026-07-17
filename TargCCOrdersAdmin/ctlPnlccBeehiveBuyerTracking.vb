Public Class ctlPnlccBeehiveBuyerTracking 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlBeehiveBuyerTrackingCol As ctlccBeehiveBuyerTrackingCol 
  Private WithEvents _ctlBeehiveBuyerTracking As ctlccBeehiveBuyerTracking 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _BeehiveBuyerTrackingID As Long 
 
  'The data holders 
  Private _BeehiveBuyerTrackingCol As clsBeehiveBuyerTrackingCol 
  Private _BeehiveBuyerTracking As clsBeehiveBuyerTracking 
 
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
  Public Event evtOverrideLoadCboBeehiveBuyerTracking(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetBeehiveBuyerTrackingIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillBeehiveBuyerTrackingCol(ByRef rBeehiveBuyerTrackingCol As clsBeehiveBuyerTrackingCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlBeehiveBuyerTrackingCol(ByRef rLoadParameters As ctlccBeehiveBuyerTrackingCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlBeehiveBuyerTracking(ByRef rLoadParameters As ctlccBeehiveBuyerTracking.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreBeehiveBuyerTrackingCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtBeehiveBuyerTrackingTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
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
 
    lnkBeehiveBuyerTrackingCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkBeehiveBuyerTracking.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vBeehiveBuyerTrackingID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _BeehiveBuyerTrackingID = CType(vBeehiveBuyerTrackingID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlBeehiveBuyerTracking.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkBeehiveBuyerTrackingCol.Visible = False 
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
      pFault = LoadCboBeehiveBuyerTrackings(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _BeehiveBuyerTrackingID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_BeehiveBuyerTrackingID) 
      End If 
      ChooseBeehiveBuyerTracking() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccBeehiveBuyerTracking") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _BeehiveBuyerTrackingID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _BeehiveBuyerTrackingID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlccBeehiveBuyerTracking" OrElse pControlName = "ctlBeehiveBuyerTracking" Then 
      lnkBeehiveBuyerTracking.ForeColor = Color.Black : lnkBeehiveBuyerTracking.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkBeehiveBuyerTracking.BackColor = Color.Wheat 
      If _ctlBeehiveBuyerTracking Is Nothing Then 
        _ctlBeehiveBuyerTracking = New ctlccBeehiveBuyerTracking() 
        _ctlBeehiveBuyerTracking.Dock = DockStyle.Fill 
        _ctlBeehiveBuyerTracking.Controls.RemoveByKey("btnAdd") 
        pnlBeehiveBuyerTracking.Controls.Add(_ctlBeehiveBuyerTracking) 
        _ctlBeehiveBuyerTracking.Visible = False 
      End If 
      If _BeehiveBuyerTrackingID = 0 Then 
        pnlBeehiveBuyerTracking.Visible = False 
      End If 
      'If _BeehiveBuyerTracking Is Nothing Then 
      pFault = RefreshCtlBeehiveBuyerTracking() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlBeehiveBuyerTracking.BeehiveBuyerTracking.IsEmpty AndAlso _BeehiveBuyerTrackingID <> -2 Then 
        pnlBeehiveBuyerTracking.Visible = False 
      End If 
      _ctlBeehiveBuyerTracking.Name = "ctlccBeehiveBuyerTracking" 
      _ActiveControl = _ctlBeehiveBuyerTracking 
      _ctlBeehiveBuyerTracking.BringToFront() 
      _ctlBeehiveBuyerTracking.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccBeehiveBuyerTrackingCol" Then 
      lnkBeehiveBuyerTrackingCol.ForeColor = Color.Black : lnkBeehiveBuyerTrackingCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkBeehiveBuyerTrackingCol.BackColor = Color.Wheat 
      If _ctlBeehiveBuyerTrackingCol Is Nothing Then 
        _ctlBeehiveBuyerTrackingCol = New ctlccBeehiveBuyerTrackingCol() 
        _ctlBeehiveBuyerTrackingCol.Dock = DockStyle.Fill 
        pnlBeehiveBuyerTracking.Controls.Add(_ctlBeehiveBuyerTrackingCol) 
        _ctlBeehiveBuyerTrackingCol.Visible = False 
      End If  
      pnlBeehiveBuyerTracking.Visible = True 
      If _BeehiveBuyerTrackingCol Is Nothing Then 
        pFault = RefreshCtlBeehiveBuyerTrackingCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlBeehiveBuyerTrackingCol.Name = "ctlccBeehiveBuyerTrackingCol" 
      _ActiveControl = _ctlBeehiveBuyerTrackingCol 
      _ctlBeehiveBuyerTrackingCol.BringToFront() 
      _ctlBeehiveBuyerTrackingCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-BeehiveBuyerTracking-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("BeehiveBuyerTracking", _Requester) 
 
    lnkBeehiveBuyerTrackingCol.Text = CCTextTranslate("List", _Requester) 
    lnkBeehiveBuyerTracking.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlBeehiveBuyerTracking.Controls(0) Is _ctlBeehiveBuyerTracking Then 
      If _BeehiveBuyerTrackingID = 0 Then 
        pnlBeehiveBuyerTracking.Visible = False 
      End If 
    ElseIf pnlBeehiveBuyerTracking.Controls(0) Is _ctlBeehiveBuyerTrackingCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pBeehiveBuyerTrackingID As Long = _BeehiveBuyerTrackingID 
      If ccHelper.IsNumeric(pText) Then _BeehiveBuyerTrackingID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetBeehiveBuyerTrackingIDFromIntelliComboText(pText) 
      If pBeehiveBuyerTrackingID <> _BeehiveBuyerTrackingID Then 
        _BeehiveBuyerTracking = Nothing 
        pFault = ActivateControl("ctlccBeehiveBuyerTracking") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlBeehiveBuyerTracking.Controls(0) Is _ctlBeehiveBuyerTracking Then 
      pFault = RefreshCtlBeehiveBuyerTracking() 
    ElseIf pnlBeehiveBuyerTracking.Controls(0) Is _ctlBeehiveBuyerTrackingCol Then 
      pFault = RefreshCtlBeehiveBuyerTrackingCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlBeehiveBuyerTracking.Controls(0).Name, "", "TRGT-BeehiveBuyerTracking-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboBeehiveBuyerTrackings(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlBeehiveBuyerTrackingCol_evtRowClicked(ByVal vBeehiveBuyerTracking As Object) Handles _ctlBeehiveBuyerTrackingCol.evtRowClicked 
    
    If vBeehiveBuyerTracking Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pBeehiveBuyerTracking As clsBeehiveBuyerTracking = CType(vBeehiveBuyerTracking, clsBeehiveBuyerTracking) 
    _BeehiveBuyerTrackingID = pBeehiveBuyerTracking.ID 
    
    If _ActiveControl Is _ctlBeehiveBuyerTrackingCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = clsBeehiveBuyerTrackingCol.enmFillSumOnTheFlyParameters.GroupByCustomerID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsBeehiveBuyerTrackingCol.enmFillSumOnTheFlyParameters.GroupByReminderMonth.ToString() Then 
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
    
    ChooseBeehiveBuyerTracking() 
    
    Try 
      MyIntelliCombo.ValueSelect(_BeehiveBuyerTrackingID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pBeehiveBuyerTracking.ID.ToString("#,##0")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseBeehiveBuyerTracking() 
    _BeehiveBuyerTracking = Nothing 
    lnkBeehiveBuyerTracking.Visible = True 
  End Sub 
  Private Sub _ctlBeehiveBuyerTrackingCol_evtRowDoubleClicked(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking, ByRef rHandled As Boolean) Handles _ctlBeehiveBuyerTrackingCol.evtRowDoubleClicked 
    If lnkBeehiveBuyerTracking.Parent IsNot flpMenu Then Exit Sub 
    If vBeehiveBuyerTracking Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = clsBeehiveBuyerTrackingCol.enmFillSumOnTheFlyParameters.GroupByCustomerID.ToString() Then 
        If pSearchFilters.ContainsKey(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.CustomerID) Then pSearchFilters.Remove(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.CustomerID) 
        pSearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.CustomerID, vBeehiveBuyerTracking.CustomerID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsBeehiveBuyerTrackingCol.enmFillSumOnTheFlyParameters.GroupByReminderMonth.ToString() Then 
        If pSearchFilters.ContainsKey(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.ReminderMonthFrom) Then pSearchFilters.Remove(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.ReminderMonthFrom) 
        If pSearchFilters.ContainsKey(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.ReminderMonthTo) Then pSearchFilters.Remove(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.ReminderMonthTo) 
        pSearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.ReminderMonthFrom, vBeehiveBuyerTracking.ReminderMonth) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreBeehiveBuyerTrackingCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vBeehiveBuyerTracking.ID, vBeehiveBuyerTracking.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _BeehiveBuyerTrackingID = vBeehiveBuyerTracking.ID 
      'MyIntelliCombo.ValueSelect(_BeehiveBuyerTrackingID) 
      pFault = ActivateControl("ctlccBeehiveBuyerTracking") 
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
      pFault = _BeehiveBuyerTrackingCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _BeehiveBuyerTrackingCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _BeehiveBuyerTrackingCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _BeehiveBuyerTrackingCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccBeehiveBuyerTrackingCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsBeehiveBuyerTracking.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see BeehiveBuyerTracking" 
      pFault = _ctlBeehiveBuyerTrackingCol.LoadControl(_BeehiveBuyerTrackingCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlBeehiveBuyerTrackingCol_evtUnChosen() Handles _ctlBeehiveBuyerTrackingCol.evtUnChosen 
 
    _BeehiveBuyerTrackingID = 0 
    _BeehiveBuyerTracking = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkBeehiveBuyerTracking.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkBeehiveBuyerTrackingCol.Click, 
      lnkBeehiveBuyerTracking.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkBeehiveBuyerTracking OrElse (lnk Is lnkBeehiveBuyerTrackingCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlBeehiveBuyerTrackingCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccBeehiveBuyerTrackingCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsBeehiveBuyerTracking.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsBeehiveBuyerTrackingCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillBeehiveBuyerTrackingCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _BeehiveBuyerTrackingCol = New clsBeehiveBuyerTrackingCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _BeehiveBuyerTrackingCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlBeehiveBuyerTrackingCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.Customer 
            pFault = _BeehiveBuyerTrackingCol.FillByCustomerID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=100, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then  
              _ctlBeehiveBuyerTrackingCol.Timer?.Stop()  
              Return pFault  
            End If  
          Case Else 
            If _ctlBeehiveBuyerTrackingCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _BeehiveBuyerTrackingCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlBeehiveBuyerTrackingCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _BeehiveBuyerTrackingCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _BeehiveBuyerTrackingCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _BeehiveBuyerTrackingCol.Count) 
      End If 
    Else 
      _BeehiveBuyerTrackingCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _BeehiveBuyerTrackingCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlBeehiveBuyerTrackingCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see BeehiveBuyerTracking" 
    
    Dim pBeehiveBuyerTrackingID As Long = _BeehiveBuyerTrackingID 
    
    pFault = _ctlBeehiveBuyerTrackingCol.LoadControl(_BeehiveBuyerTrackingCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlBeehiveBuyerTrackingCol.Visible = True 
    
    _ctlBeehiveBuyerTrackingCol.Refresh() 
    If pBeehiveBuyerTrackingID <> 0 Then 
      Dim pBeehiveBuyerTrackingCol As clsBeehiveBuyerTrackingCol = CType(_ctlBeehiveBuyerTrackingCol.bsCtlBeehiveBuyerTracking.DataSource, clsBeehiveBuyerTrackingCol) 
      Dim pBeehiveBuyerTracking As clsBeehiveBuyerTracking = pBeehiveBuyerTrackingCol.FindByID(pBeehiveBuyerTrackingID) 
      If pBeehiveBuyerTracking.ID > 0 Then 
        _ctlBeehiveBuyerTrackingCol.bsCtlBeehiveBuyerTracking.CurrencyManager.Position = pBeehiveBuyerTrackingCol.IndexOf(pBeehiveBuyerTracking) 
        _ctlBeehiveBuyerTrackingCol.dgvBeehiveBuyerTracking.Rows(pBeehiveBuyerTrackingCol.IndexOf(pBeehiveBuyerTracking)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlBeehiveBuyerTracking() As clsFault 
    Dim pFault As New clsFault 
    
    If _BeehiveBuyerTrackingID > 0 Then 
      ChooseBeehiveBuyerTracking() 
      _BeehiveBuyerTracking = New clsBeehiveBuyerTracking(clsEnums.enmLoadParent.TextOnly) 
      pFault = _BeehiveBuyerTracking.GetByID(_BeehiveBuyerTrackingID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _BeehiveBuyerTracking = New clsBeehiveBuyerTracking(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _BeehiveBuyerTracking.ID.ToString("#,##0")    
     
    Dim pLoadParameters As New ctlccBeehiveBuyerTracking.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlBeehiveBuyerTracking(pLoadParameters)
    pFault = _ctlBeehiveBuyerTracking.LoadControl(_BeehiveBuyerTracking, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlBeehiveBuyerTracking.Visible = True 
    If _BeehiveBuyerTrackingID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlBeehiveBuyerTracking.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlBeehiveBuyerTracking.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlBeehiveBuyerTracking_evtDeleted(ByVal vBeehiveBuyerTrackingID As Long) Handles _ctlBeehiveBuyerTracking.evtDeleted 
    _BeehiveBuyerTrackingCol = Nothing 
    Dim pFault As clsFault 
    _BeehiveBuyerTrackingID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboBeehiveBuyerTrackings(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlBeehiveBuyerTracking() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlBeehiveBuyerTracking.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkBeehiveBuyerTrackingCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlBeehiveBuyerTracking_evtCancelledEdit(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking) Handles _ctlBeehiveBuyerTracking.evtCancelledEdit 
    RefreshCtlBeehiveBuyerTracking() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboBeehiveBuyerTrackings(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlBeehiveBuyerTracking.btnAdd.Visible = False 
      If _BeehiveBuyerTrackingID = 0 OrElse _BeehiveBuyerTrackingID = -2 Then 
        pnlBeehiveBuyerTracking.Visible = False 
      Else 
        pnlBeehiveBuyerTracking.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlBeehiveBuyerTracking.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccBeehiveBuyerTrackingCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlBeehiveBuyerTracking_evtUpdated(ByVal vWhichProperty As clsBeehiveBuyerTracking.enmUpdateType, ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking) Handles _ctlBeehiveBuyerTracking.evtUpdated 
    _BeehiveBuyerTrackingCol = Nothing 
    Dim pFault As clsFault 
    _BeehiveBuyerTrackingID = CType(vBeehiveBuyerTracking, clsBeehiveBuyerTracking).ID 
    If _ActiveControl.Name = "ctlccBeehiveBuyerTracking" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboBeehiveBuyerTrackings(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlBeehiveBuyerTracking() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlBeehiveBuyerTracking.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboBeehiveBuyerTrackings(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccBeehiveBuyerTrackingDefaultByID 
    Dim pParentID As Long = 0 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.Customer Then 
      pComboListTypeToLoad = clsEnums.enmComboListType.ccBeehiveBuyerTrackingForCustomerDefaultByID 
      pParentID = _Requester.UserIdentityInstanceID 
    End If 
    
    RaiseEvent evtOverrideLoadCboBeehiveBuyerTracking(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _BeehiveBuyerTrackingID >= 0 Then 
      MyIntelliCombo.ValueSelect(_BeehiveBuyerTrackingID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_BeehiveBuyerTrackingUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _BeehiveBuyerTrackingID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _BeehiveBuyerTrackingID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetBeehiveBuyerTrackingIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _BeehiveBuyerTrackingID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _BeehiveBuyerTrackingID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _BeehiveBuyerTrackingID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _BeehiveBuyerTrackingID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseBeehiveBuyerTracking() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccBeehiveBuyerTracking", StringComparison.OrdinalIgnoreCase) AndAlso _BeehiveBuyerTrackingID > 0 Then 
        'to avoid getting ObjectNotFound 
        _BeehiveBuyerTracking = New clsBeehiveBuyerTracking(clsEnums.enmLoadParent.TextOnly) 
        pFault = _BeehiveBuyerTracking.GetByID(_BeehiveBuyerTrackingID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccBeehiveBuyerTracking") 
    End If 
    pnlBeehiveBuyerTracking.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As clsBeehiveBuyerTracking.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlBeehiveBuyerTracking.evtParentChosen 
    If vParentName = clsBeehiveBuyerTracking.enmParentProperty.Customer Then 
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
    pnlBeehiveBuyerTracking.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkBeehiveBuyerTrackingCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _BeehiveBuyerTrackingID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccBeehiveBuyerTrackingCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkBeehiveBuyerTrackingCol.Visible = False 
      _ActiveControl = _ctlBeehiveBuyerTracking 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboBeehiveBuyerTrackings(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _BeehiveBuyerTrackingID <> 0 Then 
        MyIntelliCombo.cbo.Text = _BeehiveBuyerTrackingID.ToString() 
        pFault = ActivateControl("ctlccBeehiveBuyerTracking") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlBeehiveBuyerTracking.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlBeehiveBuyerTracking.Visible = False 
        _BeehiveBuyerTrackingID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _BeehiveBuyerTrackingID > 0 Then pnlBeehiveBuyerTracking.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkBeehiveBuyerTrackingCol.MouseEnter, 
                  lnkBeehiveBuyerTracking.MouseEnter, 
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
                  lnkBeehiveBuyerTracking.MouseLeave, 
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
  Private Sub _ctlBeehiveBuyerTracking_evtAdd(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking) Handles _ctlBeehiveBuyerTracking.evtAdd 
    lnkBeehiveBuyerTrackingCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pCustomerID As Nullable(Of Long) = Nothing 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.Customer Then 
      pCustomerID = _Requester.UserIdentityInstanceID 
    End If 
    Dim pReminderMonthFrom As Nullable(Of Integer) = Nothing 
    Dim pReminderMonthTo As Nullable(Of Integer) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByCustomerID As Boolean = False 
    Dim pGroupByReminderMonth As Boolean = False 
    
    Dim pSumBeehiveQuantity As Boolean = False 
    Dim pSumReminderMonth As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Beehive Buyer Trackings"  
  
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
        If pCustomerID Is Nothing Then 
         .Combo01Label.Text = If(_ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsBeehiveBuyerTracking.enmProperty.Customer), _ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText(clsBeehiveBuyerTracking.enmProperty.Customer), "Customer") 
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
           .TabIndex = 3 
         End With 
        End If 
 
        .Text01Label.Text = If(_ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsBeehiveBuyerTracking.enmProperty.ReminderMonth), _ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText(clsBeehiveBuyerTracking.enmProperty.ReminderMonth), "Reminder Month") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 4 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 5 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .Text02Label.Text = If(_ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsBeehiveBuyerTracking.enmProperty.ID), _ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText(clsBeehiveBuyerTracking.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 6 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsBeehiveBuyerTracking.enmProperty.Customer), _ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText(clsBeehiveBuyerTracking.enmProperty.Customer), "Customer") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsBeehiveBuyerTracking.enmProperty.ReminderMonth), _ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText(clsBeehiveBuyerTracking.enmProperty.ReminderMonth), "Reminder Month") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsBeehiveBuyerTracking.enmProperty.BeehiveQuantity), _ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText(clsBeehiveBuyerTracking.enmProperty.BeehiveQuantity), "Beehive Quantity") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 10 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsBeehiveBuyerTracking.enmProperty.ReminderMonth), _ctlBeehiveBuyerTrackingCol.LoadParameters.ColumnsHeaderText(clsBeehiveBuyerTracking.enmProperty.ReminderMonth), "Reminder Month") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 11 
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
      If pCustomerID Is Nothing Then 
       If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
         pCustomerID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
         _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.CustomerID, pCustomerID) 
       End If 
      Else 
        _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.CustomerID, pCustomerID) 
      End If  
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pReminderMonthFrom = ccHelper.ToInteger(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pReminderMonthTo = ccHelper.ToInteger(.Text01To.Text) 
          Else 
            pReminderMonthTo = pReminderMonthFrom 
          End If 
          _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.ReminderMonthFrom, pReminderMonthFrom) 
          _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.ReminderMonthTo, pReminderMonthTo) 
        End If 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByCustomerID = True 
        pDoSum = True 
        _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillSumOnTheFlyParameters.GroupByCustomerID, pGroupByCustomerID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByReminderMonth = True 
        pDoSum = True 
        _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmFillSumOnTheFlyParameters.GroupByReminderMonth, pGroupByReminderMonth) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumBeehiveQuantity = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumReminderMonth = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsBeehiveBuyerTrackingCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsBeehiveBuyerTrackingCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsBeehiveBuyerTrackingCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccBeehiveBuyerTrackingCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccBeehiveBuyerTrackingCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsBeehiveBuyerTracking.enmProperty.ID, "ID") 
      End With 
      _BeehiveBuyerTrackingCol = New clsBeehiveBuyerTrackingCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _BeehiveBuyerTrackingCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.Customer 
            pFault = _BeehiveBuyerTrackingCol.FillByCustomerID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
          Case Else 
            pFault = _BeehiveBuyerTrackingCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _BeehiveBuyerTrackingCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _BeehiveBuyerTrackingCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _BeehiveBuyerTrackingCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see BeehiveBuyerTracking" 
      RaiseEvent evtOverrideLoadCtlBeehiveBuyerTrackingCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _BeehiveBuyerTrackingCol = New clsBeehiveBuyerTrackingCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _BeehiveBuyerTrackingCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccBeehiveBuyerTrackingCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _BeehiveBuyerTrackingCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsBeehiveBuyerTracking.enmProperty.ID, "Count") 
        If pGroupByCustomerID = False Then .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.Customer) 
        If pGroupByReminderMonth = False Then .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.ReminderMonth) 
        If pSumBeehiveQuantity = False Then .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.BeehiveQuantity) 
        If pSumReminderMonth = False Then .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.ReminderMonth) 
        If pGroupByReminderMonth = True OrElse pSumReminderMonth = True Then If .ColumnsHide.Contains(clsBeehiveBuyerTracking.enmProperty.ReminderMonth) Then .ColumnsHide.Remove(clsBeehiveBuyerTracking.enmProperty.ReminderMonth) 
        .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.LastOrderDate) 
        .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.IsRelevant) 
        .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.Notes) 
        .ColumnsHide.Add(clsBeehiveBuyerTracking.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlBeehiveBuyerTrackingCol.Visible = True 
    pFault = _ctlBeehiveBuyerTrackingCol.LoadControl(_BeehiveBuyerTrackingCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsBeehiveBuyerTrackingCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsBeehiveBuyerTrackingCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlBeehiveBuyerTracking.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlBeehiveBuyerTracking.Controls(0).Name) 
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
    _BeehiveBuyerTrackingID = -2 
    pFault = ActivateControl("ctlccBeehiveBuyerTracking") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlBeehiveBuyerTracking() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlBeehiveBuyerTracking.Visible = True 'new 
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
 
  Private Sub _ctlBeehiveBuyerTrackingCol_evtTimerTripped() Handles _ctlBeehiveBuyerTrackingCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeehiveBuyerTrackingTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlBeehiveBuyerTrackingCol.BeehiveBuyerTrackingCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlBeehiveBuyerTrackingCol.BeehiveBuyerTrackingCol(0).ID 
 
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
    If _BeehiveBuyerTrackingCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsBeehiveBuyerTracking() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsBeehiveBuyerTrackingCol = CType(CallByName(_BeehiveBuyerTrackingCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsBeehiveBuyerTrackingCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsBeehiveBuyerTrackingCol = CType(CallByName(_BeehiveBuyerTrackingCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsBeehiveBuyerTrackingCol) 
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
                  ccHelper.GetPropertyTypeName(New clsBeehiveBuyerTrackingCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsBeehiveBuyerTrackingCol = CType(CallByName(_BeehiveBuyerTrackingCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsBeehiveBuyerTrackingCol) 
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
 
  Private Sub cc_ctlPnlBeehiveBuyerTracking_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
