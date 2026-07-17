Public Class ctlPnlccCustomerDebt 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlCustomerDebtCol As ctlccCustomerDebtCol 
  Private WithEvents _ctlCustomerDebt As ctlccCustomerDebt 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _CustomerDebtID As Long 
 
  'The data holders 
  Private _CustomerDebtCol As clsCustomerDebtCol 
  Private _CustomerDebt As clsCustomerDebt 
 
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
  Public Event evtOverrideLoadCboCustomerDebt(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetCustomerDebtIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillCustomerDebtCol(ByRef rCustomerDebtCol As clsCustomerDebtCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlCustomerDebtCol(ByRef rLoadParameters As ctlccCustomerDebtCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlCustomerDebt(ByRef rLoadParameters As ctlccCustomerDebt.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreCustomerDebtCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtCustomerDebtTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  'Parents
  Private _CancelEvtCustomerChosen As Boolean = False 
  Private _ShowPopForEvtCustomerChosen As Boolean = False 
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
 
    lnkCustomerDebtCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkCustomerDebt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vCustomerDebtID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _CustomerDebtID = CType(vCustomerDebtID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlCustomerDebt.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkCustomerDebtCol.Visible = False 
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
      pFault = LoadCboCustomerDebts(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _CustomerDebtID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_CustomerDebtID) 
      End If 
      ChooseCustomerDebt() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccCustomerDebt") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _CustomerDebtID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlccCustomerDebt" OrElse pControlName = "ctlCustomerDebt" Then 
      lnkCustomerDebt.ForeColor = Color.Black : lnkCustomerDebt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkCustomerDebt.BackColor = Color.Wheat 
      If _ctlCustomerDebt Is Nothing Then 
        _ctlCustomerDebt = New ctlccCustomerDebt() 
        _ctlCustomerDebt.Dock = DockStyle.Fill 
        _ctlCustomerDebt.Controls.RemoveByKey("btnAdd") 
        pnlCustomerDebt.Controls.Add(_ctlCustomerDebt) 
        _ctlCustomerDebt.Visible = False 
      End If 
      If _CustomerDebtID = 0 Then 
        pnlCustomerDebt.Visible = False 
      End If 
      'If _CustomerDebt Is Nothing Then 
      pFault = RefreshCtlCustomerDebt() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlCustomerDebt.CustomerDebt.IsEmpty AndAlso _CustomerDebtID <> -2 Then 
        pnlCustomerDebt.Visible = False 
      End If 
      _ctlCustomerDebt.Name = "ctlccCustomerDebt" 
      _ActiveControl = _ctlCustomerDebt 
      _ctlCustomerDebt.BringToFront() 
      _ctlCustomerDebt.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccCustomerDebtCol" Then 
      lnkCustomerDebtCol.ForeColor = Color.Black : lnkCustomerDebtCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkCustomerDebtCol.BackColor = Color.Wheat 
      If _ctlCustomerDebtCol Is Nothing Then 
        _ctlCustomerDebtCol = New ctlccCustomerDebtCol() 
        _ctlCustomerDebtCol.Dock = DockStyle.Fill 
        pnlCustomerDebt.Controls.Add(_ctlCustomerDebtCol) 
        _ctlCustomerDebtCol.Visible = False 
      End If  
      pnlCustomerDebt.Visible = True 
      If _CustomerDebtCol Is Nothing Then 
        pFault = RefreshCtlCustomerDebtCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlCustomerDebtCol.Name = "ctlccCustomerDebtCol" 
      _ActiveControl = _ctlCustomerDebtCol 
      _ctlCustomerDebtCol.BringToFront() 
      _ctlCustomerDebtCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-CustomerDebt-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("CustomerDebt", _Requester) 
 
    lnkCustomerDebtCol.Text = CCTextTranslate("List", _Requester) 
    lnkCustomerDebt.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlCustomerDebt.Controls(0) Is _ctlCustomerDebt Then 
      If _CustomerDebtID = 0 Then 
        pnlCustomerDebt.Visible = False 
      End If 
    ElseIf pnlCustomerDebt.Controls(0) Is _ctlCustomerDebtCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pCustomerDebtID As Long = _CustomerDebtID 
      If ccHelper.IsNumeric(pText) Then _CustomerDebtID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetCustomerDebtIDFromIntelliComboText(pText) 
      If pCustomerDebtID <> _CustomerDebtID Then 
        _CustomerDebt = Nothing 
        pFault = ActivateControl("ctlccCustomerDebt") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlCustomerDebt.Controls(0) Is _ctlCustomerDebt Then 
      pFault = RefreshCtlCustomerDebt() 
    ElseIf pnlCustomerDebt.Controls(0) Is _ctlCustomerDebtCol Then 
      pFault = RefreshCtlCustomerDebtCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlCustomerDebt.Controls(0).Name, "", "TRGT-CustomerDebt-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboCustomerDebts(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlCustomerDebtCol_evtRowClicked(ByVal vCustomerDebt As Object) Handles _ctlCustomerDebtCol.evtRowClicked 
    
    If vCustomerDebt Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pCustomerDebt As clsCustomerDebt = CType(vCustomerDebt, clsCustomerDebt) 
    _CustomerDebtID = pCustomerDebt.ID 
    
    If _ActiveControl Is _ctlCustomerDebtCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = clsCustomerDebtCol.enmFillSumOnTheFlyParameters.GroupByCustomerID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = clsCustomerDebtCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID.ToString() Then 
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
    
    ChooseCustomerDebt() 
    
    Try 
      MyIntelliCombo.ValueSelect(_CustomerDebtID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pCustomerDebt.CustomerID.ToString("#,##0") & " " & pCustomerDebt.DebtAmount.ToString("#,##0.00")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseCustomerDebt() 
    _CustomerDebt = Nothing 
    lnkCustomerDebt.Visible = True 
  End Sub 
  Private Sub _ctlCustomerDebtCol_evtRowDoubleClicked(ByVal vCustomerDebt As clsCustomerDebt, ByRef rHandled As Boolean) Handles _ctlCustomerDebtCol.evtRowDoubleClicked 
    If lnkCustomerDebt.Parent IsNot flpMenu Then Exit Sub 
    If vCustomerDebt Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = clsCustomerDebtCol.enmFillSumOnTheFlyParameters.GroupByCustomerID.ToString() Then 
        If pSearchFilters.ContainsKey(clsCustomerDebtCol.enmFillOnTheFlyParameters.CustomerID) Then pSearchFilters.Remove(clsCustomerDebtCol.enmFillOnTheFlyParameters.CustomerID) 
        pSearchFilters.Add(clsCustomerDebtCol.enmFillOnTheFlyParameters.CustomerID, vCustomerDebt.CustomerID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = clsCustomerDebtCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID.ToString() Then 
        If pSearchFilters.ContainsKey(clsCustomerDebtCol.enmFillOnTheFlyParameters.OrderHeaderID) Then pSearchFilters.Remove(clsCustomerDebtCol.enmFillOnTheFlyParameters.OrderHeaderID) 
        pSearchFilters.Add(clsCustomerDebtCol.enmFillOnTheFlyParameters.OrderHeaderID, vCustomerDebt.OrderHeaderID) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreCustomerDebtCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vCustomerDebt.ID, vCustomerDebt.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _CustomerDebtID = vCustomerDebt.ID 
      'MyIntelliCombo.ValueSelect(_CustomerDebtID) 
      pFault = ActivateControl("ctlccCustomerDebt") 
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
      pFault = _CustomerDebtCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _CustomerDebtCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _CustomerDebtCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _CustomerDebtCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccCustomerDebtCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsCustomerDebt.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see CustomerDebt" 
      pFault = _ctlCustomerDebtCol.LoadControl(_CustomerDebtCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlCustomerDebtCol_evtUnChosen() Handles _ctlCustomerDebtCol.evtUnChosen 
 
    _CustomerDebtID = 0 
    _CustomerDebt = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkCustomerDebt.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkCustomerDebtCol.Click, 
      lnkCustomerDebt.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkCustomerDebt OrElse (lnk Is lnkCustomerDebtCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlCustomerDebtCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccCustomerDebtCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsCustomerDebt.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsCustomerDebtCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillCustomerDebtCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _CustomerDebtCol = New clsCustomerDebtCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _CustomerDebtCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlCustomerDebtCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.Customer 
            pFault = _CustomerDebtCol.FillByCustomerID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=100, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then  
              _ctlCustomerDebtCol.Timer?.Stop()  
              Return pFault  
            End If  
          Case Else 
            If _ctlCustomerDebtCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _CustomerDebtCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlCustomerDebtCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _CustomerDebtCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _CustomerDebtCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _CustomerDebtCol.Count) 
      End If 
    Else 
      _CustomerDebtCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _CustomerDebtCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlCustomerDebtCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see CustomerDebt" 
    
    Dim pCustomerDebtID As Long = _CustomerDebtID 
    
    pFault = _ctlCustomerDebtCol.LoadControl(_CustomerDebtCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlCustomerDebtCol.Visible = True 
    
    _ctlCustomerDebtCol.Refresh() 
    If pCustomerDebtID <> 0 Then 
      Dim pCustomerDebtCol As clsCustomerDebtCol = CType(_ctlCustomerDebtCol.bsCtlCustomerDebt.DataSource, clsCustomerDebtCol) 
      Dim pCustomerDebt As clsCustomerDebt = pCustomerDebtCol.FindByID(pCustomerDebtID) 
      If pCustomerDebt.ID > 0 Then 
        _ctlCustomerDebtCol.bsCtlCustomerDebt.CurrencyManager.Position = pCustomerDebtCol.IndexOf(pCustomerDebt) 
        _ctlCustomerDebtCol.dgvCustomerDebt.Rows(pCustomerDebtCol.IndexOf(pCustomerDebt)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlCustomerDebt() As clsFault 
    Dim pFault As New clsFault 
    
    If _CustomerDebtID > 0 Then 
      ChooseCustomerDebt() 
      _CustomerDebt = New clsCustomerDebt(clsEnums.enmLoadParent.TextOnly) 
      pFault = _CustomerDebt.GetByID(_CustomerDebtID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _CustomerDebt = New clsCustomerDebt(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _CustomerDebt.CustomerID.ToString("#,##0") & " " & _CustomerDebt.DebtAmount.ToString("#,##0.00")    
     
    Dim pLoadParameters As New ctlccCustomerDebt.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlCustomerDebt(pLoadParameters)
    pFault = _ctlCustomerDebt.LoadControl(_CustomerDebt, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlCustomerDebt.Visible = True 
    If _CustomerDebtID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlCustomerDebt.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlCustomerDebt.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlCustomerDebt_evtDeleted(ByVal vCustomerDebtID As Long) Handles _ctlCustomerDebt.evtDeleted 
    _CustomerDebtCol = Nothing 
    Dim pFault As clsFault 
    _CustomerDebtID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboCustomerDebts(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlCustomerDebt() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlCustomerDebt.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkCustomerDebtCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlCustomerDebt_evtCancelledEdit(ByVal vCustomerDebt As clsCustomerDebt) Handles _ctlCustomerDebt.evtCancelledEdit 
    RefreshCtlCustomerDebt() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboCustomerDebts(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlCustomerDebt.btnAdd.Visible = False 
      If _CustomerDebtID = 0 OrElse _CustomerDebtID = -2 Then 
        pnlCustomerDebt.Visible = False 
      Else 
        pnlCustomerDebt.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlCustomerDebt.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccCustomerDebtCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlCustomerDebt_evtUpdated(ByVal vWhichProperty As clsCustomerDebt.enmUpdateType, ByVal vCustomerDebt As clsCustomerDebt) Handles _ctlCustomerDebt.evtUpdated 
    _CustomerDebtCol = Nothing 
    Dim pFault As clsFault 
    _CustomerDebtID = CType(vCustomerDebt, clsCustomerDebt).ID 
    If _ActiveControl.Name = "ctlccCustomerDebt" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboCustomerDebts(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlCustomerDebt() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlCustomerDebt.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboCustomerDebts(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccCustomerDebtDefaultByID 
    Dim pParentID As Long = 0 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.Customer Then 
      pComboListTypeToLoad = clsEnums.enmComboListType.ccCustomerDebtForCustomerDefaultByID 
      pParentID = _Requester.UserIdentityInstanceID 
    End If 
    
    RaiseEvent evtOverrideLoadCboCustomerDebt(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _CustomerDebtID >= 0 Then 
      MyIntelliCombo.ValueSelect(_CustomerDebtID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_CustomerDebtUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _CustomerDebtID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _CustomerDebtID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetCustomerDebtIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _CustomerDebtID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _CustomerDebtID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _CustomerDebtID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _CustomerDebtID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseCustomerDebt() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccCustomerDebt", StringComparison.OrdinalIgnoreCase) AndAlso _CustomerDebtID > 0 Then 
        'to avoid getting ObjectNotFound 
        _CustomerDebt = New clsCustomerDebt(clsEnums.enmLoadParent.TextOnly) 
        pFault = _CustomerDebt.GetByID(_CustomerDebtID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccCustomerDebt") 
    End If 
    pnlCustomerDebt.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As clsCustomerDebt.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlCustomerDebt.evtParentChosen 
    If vParentName = clsCustomerDebt.enmParentProperty.Customer Then 
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
    If vParentName = clsCustomerDebt.enmParentProperty.OrderHeader Then 
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
    pnlCustomerDebt.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkCustomerDebtCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _CustomerDebtID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccCustomerDebtCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkCustomerDebtCol.Visible = False 
      _ActiveControl = _ctlCustomerDebt 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboCustomerDebts(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _CustomerDebtID <> 0 Then 
        pFault = ActivateControl("ctlccCustomerDebt") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlCustomerDebt.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlCustomerDebt.Visible = False 
        _CustomerDebtID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _CustomerDebtID > 0 Then pnlCustomerDebt.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkCustomerDebtCol.MouseEnter, 
                  lnkCustomerDebt.MouseEnter, 
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
                  lnkCustomerDebt.MouseLeave, 
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
  Private Sub _ctlCustomerDebt_evtAdd(ByVal vCustomerDebt As clsCustomerDebt) Handles _ctlCustomerDebt.evtAdd 
    lnkCustomerDebtCol.Visible = False 
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
    Dim pOrderHeaderID As Nullable(Of Long) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByCustomerID As Boolean = False 
    Dim pGroupByOrderHeaderID As Boolean = False 
    
    Dim pSumDebtAmount As Boolean = False 
    Dim pSumPaidAmount As Boolean = False 
    Dim pSumRemainingAmount As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Customer Debts"  
  
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
         .Combo01Label.Text = If(_ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomerDebt.enmProperty.Customer), _ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText(clsCustomerDebt.enmProperty.Customer), "Customer") 
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
 
        .Combo02Label.Text = If(_ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomerDebt.enmProperty.OrderHeader), _ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText(clsCustomerDebt.enmProperty.OrderHeader), "Order Header") 
        Dim pOrderHeaders As New clsComboList 
        pFault = MyCache.GetComboList(clsEnums.enmComboListType.ccOrderHeaderDefaultByID, pOrderHeaders) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
        'If pOrderHeaders IsNot Nothing AndAlso pOrderHeaders.Count > 0 Then 
        .flpFilter.Controls.Add(.Combo02Label) 
        .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        'End If 
        With .Combo02 
          .MakeSmart() 
          If pOrderHeaders IsNot Nothing Then 
            .LoadControl(pOrderHeaders, GetChoose(_Requester)) 
          Else 
            .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.ccOrderHeaderDefaultByID, 0, _Requester) 
          End If 
          .TabIndex = 4 
        End With 
 
        .Text01Label.Text = If(_ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomerDebt.enmProperty.ID), _ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText(clsCustomerDebt.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 5 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 6 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomerDebt.enmProperty.Customer), _ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText(clsCustomerDebt.enmProperty.Customer), "Customer") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 7 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomerDebt.enmProperty.OrderHeader), _ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText(clsCustomerDebt.enmProperty.OrderHeader), "Order Header") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomerDebt.enmProperty.DebtAmount), _ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText(clsCustomerDebt.enmProperty.DebtAmount), "Debt Amount") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 9 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomerDebt.enmProperty.PaidAmount), _ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText(clsCustomerDebt.enmProperty.PaidAmount), "Paid Amount") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 10 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
        .lblSumField03.Text = If(_ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsCustomerDebt.enmProperty.RemainingAmount), _ctlCustomerDebtCol.LoadParameters.ColumnsHeaderText(clsCustomerDebt.enmProperty.RemainingAmount), "Remaining Amount") 
        .chkSumField03.Checked = False 
        .chkSumField03.TabIndex = 11 
        .flpSumColumns.Controls.Add(.lblSumField03) 
        .flpSumColumns.Controls.Add(.chkSumField03) 
 
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
         _SearchFilters.Add(clsCustomerDebtCol.enmFillOnTheFlyParameters.CustomerID, pCustomerID) 
       End If 
      Else 
        _SearchFilters.Add(clsCustomerDebtCol.enmFillOnTheFlyParameters.CustomerID, pCustomerID) 
      End If  
      If .Combo02.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo02.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
        pOrderHeaderID = CType(.Combo02.SelectedItem, clsComboListMember).KeyLong 
        _SearchFilters.Add(clsCustomerDebtCol.enmFillOnTheFlyParameters.OrderHeaderID, pOrderHeaderID) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(clsCustomerDebtCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsCustomerDebtCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsCustomerDebtCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsCustomerDebtCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByCustomerID = True 
        pDoSum = True 
        _SearchFilters.Add(clsCustomerDebtCol.enmFillSumOnTheFlyParameters.GroupByCustomerID, pGroupByCustomerID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByOrderHeaderID = True 
        pDoSum = True 
        _SearchFilters.Add(clsCustomerDebtCol.enmFillSumOnTheFlyParameters.GroupByOrderHeaderID, pGroupByOrderHeaderID) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumDebtAmount = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumPaidAmount = True 
        pDoSum = True 
      End If 
      
      If .chkSumField03.Checked = True Then 
        pSumRemainingAmount = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsCustomerDebtCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsCustomerDebtCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsCustomerDebtCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsCustomerDebtCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccCustomerDebtCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccCustomerDebtCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsCustomerDebt.enmProperty.ID, "ID") 
      End With 
      _CustomerDebtCol = New clsCustomerDebtCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _CustomerDebtCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.Customer 
            pFault = _CustomerDebtCol.FillByCustomerID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
          Case Else 
            pFault = _CustomerDebtCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _CustomerDebtCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _CustomerDebtCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _CustomerDebtCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see CustomerDebt" 
      RaiseEvent evtOverrideLoadCtlCustomerDebtCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _CustomerDebtCol = New clsCustomerDebtCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _CustomerDebtCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccCustomerDebtCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _CustomerDebtCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsCustomerDebt.enmProperty.ID, "Count") 
        If pGroupByCustomerID = False Then .ColumnsHide.Add(clsCustomerDebt.enmProperty.Customer) 
        If pGroupByOrderHeaderID = False Then .ColumnsHide.Add(clsCustomerDebt.enmProperty.OrderHeader) 
        If pSumDebtAmount = False Then .ColumnsHide.Add(clsCustomerDebt.enmProperty.DebtAmount) 
        If pSumPaidAmount = False Then .ColumnsHide.Add(clsCustomerDebt.enmProperty.PaidAmount) 
        If pSumRemainingAmount = False Then .ColumnsHide.Add(clsCustomerDebt.enmProperty.RemainingAmount) 
        .ColumnsHide.Add(clsCustomerDebt.enmProperty.DebtDate) 
        .ColumnsHide.Add(clsCustomerDebt.enmProperty.DueDate) 
        .ColumnsHide.Add(clsCustomerDebt.enmProperty.DebtStatus) 
        .ColumnsHide.Add(clsCustomerDebt.enmProperty.Notes) 
        .ColumnsHide.Add(clsCustomerDebt.enmProperty.NeedsAttention) 
        .ColumnsHide.Add(clsCustomerDebt.enmProperty.ProductTypes) 
        .ColumnsHide.Add(clsCustomerDebt.enmProperty.DeliveryDate) 
        .ColumnsHide.Add(clsCustomerDebt.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlCustomerDebtCol.Visible = True 
    pFault = _ctlCustomerDebtCol.LoadControl(_CustomerDebtCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsCustomerDebtCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsCustomerDebtCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlCustomerDebt.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlCustomerDebt.Controls(0).Name) 
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
    _CustomerDebtID = -2 
    pFault = ActivateControl("ctlccCustomerDebt") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlCustomerDebt() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlCustomerDebt.Visible = True 'new 
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
 
  Private Sub _ctlCustomerDebtCol_evtTimerTripped() Handles _ctlCustomerDebtCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtCustomerDebtTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlCustomerDebtCol.CustomerDebtCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlCustomerDebtCol.CustomerDebtCol(0).ID 
 
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
    If _CustomerDebtCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsCustomerDebt() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsCustomerDebtCol = CType(CallByName(_CustomerDebtCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsCustomerDebtCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsCustomerDebtCol = CType(CallByName(_CustomerDebtCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsCustomerDebtCol) 
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
                  ccHelper.GetPropertyTypeName(New clsCustomerDebtCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsCustomerDebtCol = CType(CallByName(_CustomerDebtCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsCustomerDebtCol) 
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
 
  Private Sub cc_ctlPnlCustomerDebt_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
