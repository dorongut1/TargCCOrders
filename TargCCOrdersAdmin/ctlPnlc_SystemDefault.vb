Public Class ctlPnlc_SystemDefault 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlSystemDefaultCol As ctlc_SystemDefaultCol 
  Private WithEvents _ctlSystemDefault As ctlc_SystemDefault 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _SystemDefaultID As Long 
 
  'The data holders 
  Private _SystemDefaultCol As csSystemDefaultCol 
  Private _SystemDefault As csSystemDefault 
 
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
  Public Event evtOverrideLoadCboSystemDefault(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetSystemDefaultIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillSystemDefaultCol(ByRef rSystemDefaultCol As csSystemDefaultCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlSystemDefaultCol(ByRef rLoadParameters As ctlc_SystemDefaultCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlSystemDefault(ByRef rLoadParameters As ctlc_SystemDefault.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreSystemDefaultCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtSystemDefaultTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
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
 
    lnkSystemDefaultCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkSystemDefault.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vSystemDefaultID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _SystemDefaultID = CType(vSystemDefaultID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlSystemDefault.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkSystemDefaultCol.Visible = False 
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
      pFault = LoadCboSystemDefaults(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _SystemDefaultID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_SystemDefaultID) 
      End If 
      ChooseSystemDefault() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_SystemDefault") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _SystemDefaultID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlc_SystemDefault" OrElse pControlName = "ctlSystemDefault" Then 
      lnkSystemDefault.ForeColor = Color.Black : lnkSystemDefault.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkSystemDefault.BackColor = Color.Wheat 
      If _ctlSystemDefault Is Nothing Then 
        _ctlSystemDefault = New ctlc_SystemDefault() 
        _ctlSystemDefault.Dock = DockStyle.Fill 
        _ctlSystemDefault.Controls.RemoveByKey("btnAdd") 
        pnlSystemDefault.Controls.Add(_ctlSystemDefault) 
        _ctlSystemDefault.Visible = False 
      End If 
      If _SystemDefaultID = 0 Then 
        pnlSystemDefault.Visible = False 
      End If 
      'If _SystemDefault Is Nothing Then 
      pFault = RefreshCtlSystemDefault() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlSystemDefault.SystemDefault.IsEmpty AndAlso _SystemDefaultID <> -2 Then 
        pnlSystemDefault.Visible = False 
      End If 
      _ctlSystemDefault.Name = "ctlc_SystemDefault" 
      _ActiveControl = _ctlSystemDefault 
      _ctlSystemDefault.BringToFront() 
      _ctlSystemDefault.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_SystemDefaultCol" Then 
      lnkSystemDefaultCol.ForeColor = Color.Black : lnkSystemDefaultCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkSystemDefaultCol.BackColor = Color.Wheat 
      If _ctlSystemDefaultCol Is Nothing Then 
        _ctlSystemDefaultCol = New ctlc_SystemDefaultCol() 
        _ctlSystemDefaultCol.Dock = DockStyle.Fill 
        pnlSystemDefault.Controls.Add(_ctlSystemDefaultCol) 
        _ctlSystemDefaultCol.Visible = False 
      End If  
      pnlSystemDefault.Visible = True 
      If _SystemDefaultCol Is Nothing Then 
        pFault = RefreshCtlSystemDefaultCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlSystemDefaultCol.Name = "ctlc_SystemDefaultCol" 
      _ActiveControl = _ctlSystemDefaultCol 
      _ctlSystemDefaultCol.BringToFront() 
      _ctlSystemDefaultCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-SystemDefault-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("SystemDefault", _Requester) 
 
    lnkSystemDefaultCol.Text = CCTextTranslate("List", _Requester) 
    lnkSystemDefault.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlSystemDefault.Controls(0) Is _ctlSystemDefault Then 
      If _SystemDefaultID = 0 Then 
        pnlSystemDefault.Visible = False 
      End If 
    ElseIf pnlSystemDefault.Controls(0) Is _ctlSystemDefaultCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pSystemDefaultID As Long = _SystemDefaultID 
      If ccHelper.IsNumeric(pText) Then _SystemDefaultID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetSystemDefaultIDFromIntelliComboText(pText) 
      If pSystemDefaultID <> _SystemDefaultID Then 
        _SystemDefault = Nothing 
        pFault = ActivateControl("ctlc_SystemDefault") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlSystemDefault.Controls(0) Is _ctlSystemDefault Then 
      pFault = RefreshCtlSystemDefault() 
    ElseIf pnlSystemDefault.Controls(0) Is _ctlSystemDefaultCol Then 
      pFault = RefreshCtlSystemDefaultCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlSystemDefault.Controls(0).Name, "", "TRGT-SystemDefault-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboSystemDefaults(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlSystemDefaultCol_evtRowClicked(ByVal vSystemDefault As Object) Handles _ctlSystemDefaultCol.evtRowClicked 
    
    If vSystemDefault Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pSystemDefault As csSystemDefault = CType(vSystemDefault, csSystemDefault) 
    _SystemDefaultID = pSystemDefault.ID 
    
    If _ActiveControl Is _ctlSystemDefaultCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csSystemDefaultCol.enmFillSumOnTheFlyParameters.GroupByGroup.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csSystemDefaultCol.enmFillSumOnTheFlyParameters.GroupBySettingName.ToString() Then 
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
    
    ChooseSystemDefault() 
    
    Try 
      MyIntelliCombo.ValueSelect(_SystemDefaultID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pSystemDefault.Group & " " & pSystemDefault.SettingName
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseSystemDefault() 
    _SystemDefault = Nothing 
    lnkSystemDefault.Visible = True 
  End Sub 
  Private Sub _ctlSystemDefaultCol_evtRowDoubleClicked(ByVal vSystemDefault As csSystemDefault, ByRef rHandled As Boolean) Handles _ctlSystemDefaultCol.evtRowDoubleClicked 
    If lnkSystemDefault.Parent IsNot flpMenu Then Exit Sub 
    If vSystemDefault Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csSystemDefaultCol.enmFillSumOnTheFlyParameters.GroupByGroup.ToString() Then 
        If pSearchFilters.ContainsKey(csSystemDefaultCol.enmFillOnTheFlyParameters.Group) Then pSearchFilters.Remove(csSystemDefaultCol.enmFillOnTheFlyParameters.Group) 
        pSearchFilters.Add(csSystemDefaultCol.enmFillOnTheFlyParameters.Group, vSystemDefault.Group) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csSystemDefaultCol.enmFillSumOnTheFlyParameters.GroupBySettingName.ToString() Then 
        If pSearchFilters.ContainsKey(csSystemDefaultCol.enmFillOnTheFlyParameters.SettingName) Then pSearchFilters.Remove(csSystemDefaultCol.enmFillOnTheFlyParameters.SettingName) 
        pSearchFilters.Add(csSystemDefaultCol.enmFillOnTheFlyParameters.SettingName, vSystemDefault.SettingName) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreSystemDefaultCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vSystemDefault.ID, vSystemDefault.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _SystemDefaultID = vSystemDefault.ID 
      'MyIntelliCombo.ValueSelect(_SystemDefaultID) 
      pFault = ActivateControl("ctlc_SystemDefault") 
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
      pFault = _SystemDefaultCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _SystemDefaultCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _SystemDefaultCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _SystemDefaultCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_SystemDefaultCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csSystemDefault.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see SystemDefault" 
      pFault = _ctlSystemDefaultCol.LoadControl(_SystemDefaultCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlSystemDefaultCol_evtUnChosen() Handles _ctlSystemDefaultCol.evtUnChosen 
 
    _SystemDefaultID = 0 
    _SystemDefault = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkSystemDefault.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkSystemDefaultCol.Click, 
      lnkSystemDefault.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkSystemDefault OrElse (lnk Is lnkSystemDefaultCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlSystemDefaultCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_SystemDefaultCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csSystemDefault.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csSystemDefaultCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillSystemDefaultCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _SystemDefaultCol = New csSystemDefaultCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _SystemDefaultCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlSystemDefaultCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlSystemDefaultCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _SystemDefaultCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlSystemDefaultCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _SystemDefaultCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _SystemDefaultCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _SystemDefaultCol.Count) 
      End If 
    Else 
      _SystemDefaultCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _SystemDefaultCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlSystemDefaultCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see SystemDefault" 
    
    Dim pSystemDefaultID As Long = _SystemDefaultID 
    
    pFault = _ctlSystemDefaultCol.LoadControl(_SystemDefaultCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlSystemDefaultCol.Visible = True 
    
    _ctlSystemDefaultCol.Refresh() 
    If pSystemDefaultID <> 0 Then 
      Dim pSystemDefaultCol As csSystemDefaultCol = CType(_ctlSystemDefaultCol.bsCtlSystemDefault.DataSource, csSystemDefaultCol) 
      Dim pSystemDefault As csSystemDefault = pSystemDefaultCol.FindByID(pSystemDefaultID) 
      If pSystemDefault.ID > 0 Then 
        _ctlSystemDefaultCol.bsCtlSystemDefault.CurrencyManager.Position = pSystemDefaultCol.IndexOf(pSystemDefault) 
        _ctlSystemDefaultCol.dgvSystemDefault.Rows(pSystemDefaultCol.IndexOf(pSystemDefault)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlSystemDefault() As clsFault 
    Dim pFault As New clsFault 
    
    If _SystemDefaultID > 0 Then 
      ChooseSystemDefault() 
      _SystemDefault = New csSystemDefault() 
      pFault = _SystemDefault.GetByID(_SystemDefaultID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _SystemDefault = New csSystemDefault() 
    End If 
    'lblSecondaryTitle.Text = _SystemDefault.Group & " " & _SystemDefault.SettingName    
     
    Dim pLoadParameters As New ctlc_SystemDefault.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlSystemDefault(pLoadParameters)
    pFault = _ctlSystemDefault.LoadControl(_SystemDefault, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlSystemDefault.Visible = True 
    If _SystemDefaultID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlSystemDefault.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlSystemDefault.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlSystemDefault_evtDeleted(ByVal vSystemDefaultID As Long) Handles _ctlSystemDefault.evtDeleted 
    _SystemDefaultCol = Nothing 
    Dim pFault As clsFault 
    _SystemDefaultID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboSystemDefaults(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlSystemDefault() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlSystemDefault.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkSystemDefaultCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlSystemDefault_evtCancelledEdit(ByVal vSystemDefault As csSystemDefault) Handles _ctlSystemDefault.evtCancelledEdit 
    RefreshCtlSystemDefault() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboSystemDefaults(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlSystemDefault.btnAdd.Visible = False 
      If _SystemDefaultID = 0 OrElse _SystemDefaultID = -2 Then 
        pnlSystemDefault.Visible = False 
      Else 
        pnlSystemDefault.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlSystemDefault.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_SystemDefaultCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlSystemDefault_evtUpdated(ByVal vWhichProperty As csSystemDefault.enmUpdateType, ByVal vSystemDefault As csSystemDefault) Handles _ctlSystemDefault.evtUpdated 
    _SystemDefaultCol = Nothing 
    Dim pFault As clsFault 
    _SystemDefaultID = CType(vSystemDefault, csSystemDefault).ID 
    If _ActiveControl.Name = "ctlc_SystemDefault" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboSystemDefaults(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlSystemDefault() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlSystemDefault.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Sub _ctlSystemDefault_evtUpdated() Handles _ctlSystemDefault.evtUpdated 
    'frmMessageOrInputBox.ShowMsg("Please restart any services that depend on the values you changed.", frmMessageOrInputBox.enmIconType.Warning) 
    'Environment.Exit(0) 
  End Sub 
  Private Function LoadCboSystemDefaults(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_SystemDefaultDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboSystemDefault(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _SystemDefaultID >= 0 Then 
      MyIntelliCombo.ValueSelect(_SystemDefaultID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_SystemDefaultUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _SystemDefaultID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _SystemDefaultID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetSystemDefaultIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _SystemDefaultID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _SystemDefaultID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _SystemDefaultID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _SystemDefaultID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseSystemDefault() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_SystemDefault", StringComparison.OrdinalIgnoreCase) AndAlso _SystemDefaultID > 0 Then 
        'to avoid getting ObjectNotFound 
        _SystemDefault = New csSystemDefault() 
        pFault = _SystemDefault.GetByID(_SystemDefaultID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_SystemDefault") 
    End If 
    pnlSystemDefault.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
   
  Private Sub chkGrid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrid.CheckedChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    
    Cursor = Cursors.WaitCursor 
    chkGrid.Enabled = False 
    pnlButtons.Visible = False 
    pnlSystemDefault.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkSystemDefaultCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _SystemDefaultID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_SystemDefaultCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkSystemDefaultCol.Visible = False 
      _ActiveControl = _ctlSystemDefault 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboSystemDefaults(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _SystemDefaultID <> 0 Then 
        pFault = ActivateControl("ctlc_SystemDefault") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlSystemDefault.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlSystemDefault.Visible = False 
        _SystemDefaultID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _SystemDefaultID > 0 Then pnlSystemDefault.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkSystemDefaultCol.MouseEnter, 
                  lnkSystemDefault.MouseEnter, 
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
                  lnkSystemDefaultCol.MouseLeave, 
                  lnkSystemDefault.MouseLeave, 
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
  Private Sub _ctlSystemDefault_evtAdd(ByVal vSystemDefault As csSystemDefault) Handles _ctlSystemDefault.evtAdd 
    lnkSystemDefaultCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pGroup As String = Nothing 
    Dim pGroupWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pSettingName As String = Nothing 
    Dim pSettingNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByGroup As Boolean = False 
    Dim pGroupBySettingName As Boolean = False 
    
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the System Defaults"  
  
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
        .String01Label.Text = If(_ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText.ContainsKey(csSystemDefault.enmProperty.Group), _ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText(csSystemDefault.enmProperty.Group), "Group") 
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
 
        .String02Label.Text = If(_ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText.ContainsKey(csSystemDefault.enmProperty.SettingName), _ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText(csSystemDefault.enmProperty.SettingName), "Setting Name") 
        .String02Text.Text = "" 
        .String02Text.TabIndex = 5 
        With .String02WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 6 
        End With 
        .flpFilter.Controls.Add(.String02Label) 
        .flpFilter.Controls.Add(.String02Text) 
        .flpFilter.Controls.Add(.String02LblWCType) 
        .flpFilter.Controls.Add(.String02WCType) 
 
        .Text01Label.Text = If(_ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText.ContainsKey(csSystemDefault.enmProperty.ID), _ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText(csSystemDefault.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 7 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 8 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText.ContainsKey(csSystemDefault.enmProperty.Group), _ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText(csSystemDefault.enmProperty.Group), "Group") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText.ContainsKey(csSystemDefault.enmProperty.SettingName), _ctlSystemDefaultCol.LoadParameters.ColumnsHeaderText(csSystemDefault.enmProperty.SettingName), "Setting Name") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 10 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
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
      If .String01Text.Text <> "" Then 
        pGroup = .String01Text.Text 
        pGroupWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csSystemDefaultCol.enmFillOnTheFlyParameters.Group, pGroup) 
        _SearchFilters.Add(csSystemDefaultCol.enmFillOnTheFlyParameters.GroupWildcardType, pGroupWildcardType) 
      End If 
      If .String02Text.Text <> "" Then 
        pSettingName = .String02Text.Text 
        pSettingNameWildcardType = CType(CType(.String02WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csSystemDefaultCol.enmFillOnTheFlyParameters.SettingName, pSettingName) 
        _SearchFilters.Add(csSystemDefaultCol.enmFillOnTheFlyParameters.SettingNameWildcardType, pSettingNameWildcardType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csSystemDefaultCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csSystemDefaultCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csSystemDefaultCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csSystemDefaultCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByGroup = True 
        pDoSum = True 
        _SearchFilters.Add(csSystemDefaultCol.enmFillSumOnTheFlyParameters.GroupByGroup, pGroupByGroup) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupBySettingName = True 
        pDoSum = True 
        _SearchFilters.Add(csSystemDefaultCol.enmFillSumOnTheFlyParameters.GroupBySettingName, pGroupBySettingName) 
      End If 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csSystemDefaultCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csSystemDefaultCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csSystemDefaultCol.enmListDefinition.Dir) Then _SearchFilters.Add(csSystemDefaultCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_SystemDefaultCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_SystemDefaultCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csSystemDefault.enmProperty.ID, "ID") 
      End With 
      _SystemDefaultCol = New csSystemDefaultCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _SystemDefaultCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _SystemDefaultCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _SystemDefaultCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _SystemDefaultCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _SystemDefaultCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see SystemDefault" 
      RaiseEvent evtOverrideLoadCtlSystemDefaultCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _SystemDefaultCol = New csSystemDefaultCol() 
      pFault = _SystemDefaultCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_SystemDefaultCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _SystemDefaultCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csSystemDefault.enmProperty.ID, "Count") 
        If pGroupByGroup = False Then .ColumnsHide.Add(csSystemDefault.enmProperty.Group) 
        If pGroupBySettingName = False Then .ColumnsHide.Add(csSystemDefault.enmProperty.SettingName) 
        .ColumnsHide.Add(csSystemDefault.enmProperty.SettingValue) 
        .ColumnsHide.Add(csSystemDefault.enmProperty.SystemDefaultType) 
        .ColumnsHide.Add(csSystemDefault.enmProperty.Description) 
        .ColumnsHide.Add(csSystemDefault.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlSystemDefaultCol.Visible = True 
    pFault = _ctlSystemDefaultCol.LoadControl(_SystemDefaultCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csSystemDefaultCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csSystemDefaultCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlSystemDefault.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlSystemDefault.Controls(0).Name) 
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
    _SystemDefaultID = -2 
    pFault = ActivateControl("ctlc_SystemDefault") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlSystemDefault() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlSystemDefault.Visible = True 'new 
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
 
  Private Sub _ctlSystemDefaultCol_evtTimerTripped() Handles _ctlSystemDefaultCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtSystemDefaultTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlSystemDefaultCol.SystemDefaultCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlSystemDefaultCol.SystemDefaultCol(0).ID 
 
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
    If _SystemDefaultCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csSystemDefault() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csSystemDefaultCol = CType(CallByName(_SystemDefaultCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csSystemDefaultCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csSystemDefaultCol = CType(CallByName(_SystemDefaultCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csSystemDefaultCol) 
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
                  ccHelper.GetPropertyTypeName(New csSystemDefaultCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csSystemDefaultCol = CType(CallByName(_SystemDefaultCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csSystemDefaultCol) 
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
 
  Private Sub cc_ctlPnlSystemDefault_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
