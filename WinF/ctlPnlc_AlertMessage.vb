Public Class ctlPnlc_AlertMessage 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlAlertMessageCol As ctlc_AlertMessageCol 
  Private WithEvents _ctlAlertMessage As ctlc_AlertMessage 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _AlertMessageID As Long 
 
  'The data holders 
  Private _AlertMessageCol As csAlertMessageCol 
  Private _AlertMessage As csAlertMessage 
 
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
  Public Event evtOverrideLoadCboAlertMessage(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetAlertMessageIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillAlertMessageCol(ByRef rAlertMessageCol As csAlertMessageCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlAlertMessageCol(ByRef rLoadParameters As ctlc_AlertMessageCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlAlertMessage(ByRef rLoadParameters As ctlc_AlertMessage.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreAlertMessageCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtAlertMessageTimerTripped(ByRef rCancel As Boolean) 
  
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
 
    lnkAlertMessageCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkAlertMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vAlertMessageID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _AlertMessageID = CType(vAlertMessageID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlAlertMessage.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkAlertMessageCol.Visible = False 
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
      pFault = LoadCboAlertMessages(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _AlertMessageID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_AlertMessageID) 
      End If 
      ChooseAlertMessage() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_AlertMessage") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _AlertMessageID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _AlertMessageID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_AlertMessage" OrElse pControlName = "ctlAlertMessage" Then 
      lnkAlertMessage.ForeColor = Color.Black : lnkAlertMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkAlertMessage.BackColor = Color.Wheat 
      If _ctlAlertMessage Is Nothing Then 
        _ctlAlertMessage = New ctlc_AlertMessage() 
        _ctlAlertMessage.Dock = DockStyle.Fill 
        _ctlAlertMessage.Controls.RemoveByKey("btnAdd") 
        pnlAlertMessage.Controls.Add(_ctlAlertMessage) 
        _ctlAlertMessage.Visible = False 
      End If 
      If _AlertMessageID = 0 Then 
        pnlAlertMessage.Visible = False 
      End If 
      'If _AlertMessage Is Nothing Then 
      pFault = RefreshCtlAlertMessage() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlAlertMessage.AlertMessage.IsEmpty AndAlso _AlertMessageID <> -2 Then 
        pnlAlertMessage.Visible = False 
      End If 
      _ctlAlertMessage.Name = "ctlc_AlertMessage" 
      _ActiveControl = _ctlAlertMessage 
      _ctlAlertMessage.BringToFront() 
      _ctlAlertMessage.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_AlertMessageCol" Then 
      lnkAlertMessageCol.ForeColor = Color.Black : lnkAlertMessageCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkAlertMessageCol.BackColor = Color.Wheat 
      If _ctlAlertMessageCol Is Nothing Then 
        _ctlAlertMessageCol = New ctlc_AlertMessageCol() 
        _ctlAlertMessageCol.Dock = DockStyle.Fill 
        pnlAlertMessage.Controls.Add(_ctlAlertMessageCol) 
        _ctlAlertMessageCol.Visible = False 
      End If  
      pnlAlertMessage.Visible = True 
      If _AlertMessageCol Is Nothing Then 
        pFault = RefreshCtlAlertMessageCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlAlertMessageCol.Name = "ctlc_AlertMessageCol" 
      _ActiveControl = _ctlAlertMessageCol 
      _ctlAlertMessageCol.BringToFront() 
      _ctlAlertMessageCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-AlertMessage-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("AlertMessage", _Requester) 
 
    lnkAlertMessageCol.Text = CCTextTranslate("List", _Requester) 
    lnkAlertMessage.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlAlertMessage.Controls(0) Is _ctlAlertMessage Then 
      If _AlertMessageID = 0 Then 
        pnlAlertMessage.Visible = False 
      End If 
    ElseIf pnlAlertMessage.Controls(0) Is _ctlAlertMessageCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pAlertMessageID As Long = _AlertMessageID 
      If ccHelper.IsNumeric(pText) Then _AlertMessageID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetAlertMessageIDFromIntelliComboText(pText) 
      If pAlertMessageID <> _AlertMessageID Then 
        _AlertMessage = Nothing 
        pFault = ActivateControl("ctlc_AlertMessage") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlAlertMessage.Controls(0) Is _ctlAlertMessage Then 
      pFault = RefreshCtlAlertMessage() 
    ElseIf pnlAlertMessage.Controls(0) Is _ctlAlertMessageCol Then 
      pFault = RefreshCtlAlertMessageCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlAlertMessage.Controls(0).Name, "", "TRGT-AlertMessage-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboAlertMessages(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlAlertMessageCol_evtRowClicked(ByVal vAlertMessage As Object) Handles _ctlAlertMessageCol.evtRowClicked 
    
    If vAlertMessage Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pAlertMessage As csAlertMessage = CType(vAlertMessage, csAlertMessage) 
    _AlertMessageID = pAlertMessage.ID 
    
    If _ActiveControl Is _ctlAlertMessageCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupByDescription.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupByType.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupBySeverity.ToString() Then 
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
    
    ChooseAlertMessage() 
    
    Try 
      MyIntelliCombo.ValueSelect(_AlertMessageID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pAlertMessage.Number.ToString("#,##0")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseAlertMessage() 
    _AlertMessage = Nothing 
    lnkAlertMessage.Visible = True 
  End Sub 
  Private Sub _ctlAlertMessageCol_evtRowDoubleClicked(ByVal vAlertMessage As csAlertMessage, ByRef rHandled As Boolean) Handles _ctlAlertMessageCol.evtRowDoubleClicked 
    If lnkAlertMessage.Parent IsNot flpMenu Then Exit Sub 
    If vAlertMessage Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupByDescription.ToString() Then 
        If pSearchFilters.ContainsKey(csAlertMessageCol.enmFillOnTheFlyParameters.Description) Then pSearchFilters.Remove(csAlertMessageCol.enmFillOnTheFlyParameters.Description) 
        pSearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.Description, vAlertMessage.Description) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupByType.ToString() Then 
        If pSearchFilters.ContainsKey(csAlertMessageCol.enmFillOnTheFlyParameters.Type) Then pSearchFilters.Remove(csAlertMessageCol.enmFillOnTheFlyParameters.Type) 
        pSearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.Type, vAlertMessage.Type) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupBySeverity.ToString() Then 
        If pSearchFilters.ContainsKey(csAlertMessageCol.enmFillOnTheFlyParameters.Severity) Then pSearchFilters.Remove(csAlertMessageCol.enmFillOnTheFlyParameters.Severity) 
        pSearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.Severity, vAlertMessage.Severity) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreAlertMessageCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vAlertMessage.ID, vAlertMessage.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _AlertMessageID = vAlertMessage.ID 
      'MyIntelliCombo.ValueSelect(_AlertMessageID) 
      pFault = ActivateControl("ctlc_AlertMessage") 
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
      pFault = _AlertMessageCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _AlertMessageCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _AlertMessageCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _AlertMessageCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_AlertMessageCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csAlertMessage.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see AlertMessage" 
      pFault = _ctlAlertMessageCol.LoadControl(_AlertMessageCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlAlertMessageCol_evtUnChosen() Handles _ctlAlertMessageCol.evtUnChosen 
 
    _AlertMessageID = 0 
    _AlertMessage = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkAlertMessage.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkAlertMessageCol.Click, 
      lnkAlertMessage.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkAlertMessage OrElse (lnk Is lnkAlertMessageCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlAlertMessageCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_AlertMessageCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = False 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csAlertMessage.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csAlertMessageCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillAlertMessageCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _AlertMessageCol = New csAlertMessageCol(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _AlertMessageCol.OverrideDefaultLanguage(LocalizedTextLanguage) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _AlertMessageCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlAlertMessageCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlAlertMessageCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _AlertMessageCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlAlertMessageCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _AlertMessageCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _AlertMessageCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _AlertMessageCol.Count) 
      End If 
    Else 
      _AlertMessageCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _AlertMessageCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlAlertMessageCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see AlertMessage" 
    
    Dim pAlertMessageID As Long = _AlertMessageID 
    
    pFault = _ctlAlertMessageCol.LoadControl(_AlertMessageCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlAlertMessageCol.Visible = True 
    
    _ctlAlertMessageCol.Refresh() 
    If pAlertMessageID <> 0 Then 
      Dim pAlertMessageCol As csAlertMessageCol = CType(_ctlAlertMessageCol.bsCtlAlertMessage.DataSource, csAlertMessageCol) 
      Dim pAlertMessage As csAlertMessage = pAlertMessageCol.FindByID(pAlertMessageID) 
      If pAlertMessage.ID > 0 Then 
        _ctlAlertMessageCol.bsCtlAlertMessage.CurrencyManager.Position = pAlertMessageCol.IndexOf(pAlertMessage) 
        _ctlAlertMessageCol.dgvAlertMessage.Rows(pAlertMessageCol.IndexOf(pAlertMessage)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlAlertMessage() As clsFault 
    Dim pFault As New clsFault 
    
    If _AlertMessageID > 0 Then 
      ChooseAlertMessage() 
      _AlertMessage = New csAlertMessage(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _AlertMessage.OverrideDefaultLanguage(LocalizedTextLanguage) 
      pFault = _AlertMessage.GetByID(_AlertMessageID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _AlertMessage = New csAlertMessage(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _AlertMessage.OverrideDefaultLanguage(LocalizedTextLanguage) 
    End If 
    'lblSecondaryTitle.Text = _AlertMessage.Number.ToString("#,##0")    
     
    Dim pLoadParameters As New ctlc_AlertMessage.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlAlertMessage(pLoadParameters)
    pFault = _ctlAlertMessage.LoadControl(_AlertMessage, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlAlertMessage.Visible = True 
    If _AlertMessageID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlAlertMessage.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlAlertMessage.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlAlertMessage_evtDeleted(ByVal vAlertMessageID As Long) Handles _ctlAlertMessage.evtDeleted 
    _AlertMessageCol = Nothing 
    Dim pFault As clsFault 
    _AlertMessageID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboAlertMessages(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlAlertMessage() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlAlertMessage.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkAlertMessageCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlAlertMessage_evtCancelledEdit(ByVal vAlertMessage As csAlertMessage) Handles _ctlAlertMessage.evtCancelledEdit 
    RefreshCtlAlertMessage() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboAlertMessages(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlAlertMessage.btnAdd.Visible = False 
      If _AlertMessageID = 0 OrElse _AlertMessageID = -2 Then 
        pnlAlertMessage.Visible = False 
      Else 
        pnlAlertMessage.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlAlertMessage.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_AlertMessageCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlAlertMessage_evtUpdated(ByVal vWhichProperty As csAlertMessage.enmUpdateType, ByVal vAlertMessage As csAlertMessage) Handles _ctlAlertMessage.evtUpdated 
    _AlertMessageCol = Nothing 
    Dim pFault As clsFault 
    _AlertMessageID = CType(vAlertMessage, csAlertMessage).ID 
    If _ActiveControl.Name = "ctlc_AlertMessage" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboAlertMessages(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlAlertMessage() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlAlertMessage.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboAlertMessages(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_AlertMessageDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboAlertMessage(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _AlertMessageID >= 0 Then 
      MyIntelliCombo.ValueSelect(_AlertMessageID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_AlertMessageUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _AlertMessageID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _AlertMessageID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetAlertMessageIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _AlertMessageID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _AlertMessageID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _AlertMessageID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _AlertMessageID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseAlertMessage() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_AlertMessage", StringComparison.OrdinalIgnoreCase) AndAlso _AlertMessageID > 0 Then 
        'to avoid getting ObjectNotFound 
        _AlertMessage = New csAlertMessage(vIsLocalized:=True) 
        If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _AlertMessage.OverrideDefaultLanguage(LocalizedTextLanguage) 
        pFault = _AlertMessage.GetByID(_AlertMessageID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_AlertMessage") 
    End If 
    pnlAlertMessage.Visible = True 
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
    pnlAlertMessage.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkAlertMessageCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _AlertMessageID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_AlertMessageCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkAlertMessageCol.Visible = False 
      _ActiveControl = _ctlAlertMessage 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboAlertMessages(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _AlertMessageID <> 0 Then 
        pFault = ActivateControl("ctlc_AlertMessage") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlAlertMessage.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlAlertMessage.Visible = False 
        _AlertMessageID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _AlertMessageID > 0 Then pnlAlertMessage.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkAlertMessageCol.MouseEnter, 
                  lnkAlertMessage.MouseEnter, 
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
                  lnkAlertMessageCol.MouseLeave, 
                  lnkAlertMessage.MouseLeave, 
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
  Private Sub _ctlAlertMessage_evtAdd(ByVal vAlertMessage As csAlertMessage) Handles _ctlAlertMessage.evtAdd 
    lnkAlertMessageCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pNumberFrom As Nullable(Of Integer) = Nothing 
    Dim pNumberTo As Nullable(Of Integer) = Nothing 
    Dim pDescription As String = Nothing 
    Dim pDescriptionWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pType As clsEnums.enmFaultType = Nothing 
    Dim pSeverity As clsEnums.enmFaultSeverity = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByDescription As Boolean = False 
    Dim pGroupByType As Boolean = False 
    Dim pGroupBySeverity As Boolean = False 
    
    Dim pSumNumber As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Alert Messages"  
  
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
        .Text01Label.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.Number), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.Number), "Number") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 3 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 4 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .String01Label.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.Description), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.Description), "Description") 
        .String01Text.Text = "" 
        .String01Text.TabIndex = 5 
        With .String01WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 6 
        End With 
        .flpFilter.Controls.Add(.String01Label) 
        .flpFilter.Controls.Add(.String01Text) 
        .flpFilter.Controls.Add(.String01LblWCType) 
        .flpFilter.Controls.Add(.String01WCType) 
 
        .Combo01Label.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.Type), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.Type), "Type") 
        Dim pTypes As New clsComboList 
        pFault = pTypes.FillEnums(clsEnums.enmEnum.FaultType, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pTypes.Remove(pTypes.FindByKey(clsEnums.enmFaultType.UD)) 
        pTypes.SortByText() 
        If pTypes IsNot Nothing AndAlso pTypes.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo01Label) 
          .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo01 
          .MakeSmart() 
          .LoadControl(pTypes, GetChoose(_Requester)) 
          .TabIndex = 7 
        End With 
 
        .Combo02Label.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.Severity), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.Severity), "Severity") 
        Dim pSeveritys As New clsComboList 
        pFault = pSeveritys.FillEnums(clsEnums.enmEnum.FaultSeverity, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pSeveritys.Remove(pSeveritys.FindByKey(clsEnums.enmFaultSeverity.UD)) 
        pSeveritys.SortByText() 
        If pSeveritys IsNot Nothing AndAlso pSeveritys.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo02Label) 
          .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo02 
          .MakeSmart() 
          .LoadControl(pSeveritys, GetChoose(_Requester)) 
          .TabIndex = 8 
        End With 
 
        .Text02Label.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.ID), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 9 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 10 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.Description), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.Description), "Description") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 11 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.Type), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.Type), "Type") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 12 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.Severity), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.Severity), "Severity") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 13 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlAlertMessageCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAlertMessage.enmProperty.Number), _ctlAlertMessageCol.LoadParameters.ColumnsHeaderText(csAlertMessage.enmProperty.Number), "Number") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 14 
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
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pNumberFrom = ccHelper.ToInteger(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pNumberTo = ccHelper.ToInteger(.Text01To.Text) 
          Else 
            pNumberTo = pNumberFrom 
          End If 
          _SearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.NumberFrom, pNumberFrom) 
          _SearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.NumberTo, pNumberTo) 
        End If 
      End If 
      If .String01Text.Text <> "" Then 
        pDescription = .String01Text.Text 
        pDescriptionWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.Description, pDescription) 
        _SearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.DescriptionWildcardType, pDescriptionWildcardType) 
      End If 
      If .Combo01.SelectedItem IsNot Nothing Then 
        pType = CType(CType(.Combo01.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmFaultType) 
        _SearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.Type, pType) 
      End If 
      If .Combo02.SelectedItem IsNot Nothing Then 
        pSeverity = CType(CType(.Combo02.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmFaultSeverity) 
        _SearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.Severity, pSeverity) 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csAlertMessageCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csAlertMessageCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csAlertMessageCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByDescription = True 
        pDoSum = True 
        _SearchFilters.Add(csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupByDescription, pGroupByDescription) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByType = True 
        pDoSum = True 
        _SearchFilters.Add(csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupByType, pGroupByType) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupBySeverity = True 
        pDoSum = True 
        _SearchFilters.Add(csAlertMessageCol.enmFillSumOnTheFlyParameters.GroupBySeverity, pGroupBySeverity) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumNumber = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csAlertMessageCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csAlertMessageCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csAlertMessageCol.enmListDefinition.Dir) Then _SearchFilters.Add(csAlertMessageCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_AlertMessageCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_AlertMessageCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csAlertMessage.enmProperty.ID, "ID") 
      End With 
      _AlertMessageCol = New csAlertMessageCol(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _AlertMessageCol.OverrideDefaultLanguage(LocalizedTextLanguage) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _AlertMessageCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _AlertMessageCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _AlertMessageCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _AlertMessageCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _AlertMessageCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see AlertMessage" 
      RaiseEvent evtOverrideLoadCtlAlertMessageCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _AlertMessageCol = New csAlertMessageCol(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _AlertMessageCol.OverrideDefaultLanguage(LocalizedTextLanguage) 
      pFault = _AlertMessageCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_AlertMessageCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _AlertMessageCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csAlertMessage.enmProperty.ID, "Count") 
        If pGroupByDescription = False Then .ColumnsHide.Add(csAlertMessage.enmProperty.Description) 
        If pGroupByType = False Then .ColumnsHide.Add(csAlertMessage.enmProperty.Type) 
        If pGroupBySeverity = False Then .ColumnsHide.Add(csAlertMessage.enmProperty.Severity) 
        If pSumNumber = False Then .ColumnsHide.Add(csAlertMessage.enmProperty.Number) 
        .ColumnsHide.Add(csAlertMessage.enmProperty.Message) 
        .ColumnsHide.Add(csAlertMessage.enmProperty.Action) 
        .ColumnsHide.Add(csAlertMessage.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlAlertMessageCol.Visible = True 
    pFault = _ctlAlertMessageCol.LoadControl(_AlertMessageCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csAlertMessageCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csAlertMessageCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlAlertMessage.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlAlertMessage.Controls(0).Name) 
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
    _AlertMessageID = -2 
    pFault = ActivateControl("ctlc_AlertMessage") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlAlertMessage() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlAlertMessage.Visible = True 'new 
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
 
  Private Sub _ctlAlertMessageCol_evtTimerTripped() Handles _ctlAlertMessageCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtAlertMessageTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlAlertMessageCol.AlertMessageCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlAlertMessageCol.AlertMessageCol(0).ID 
 
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
    If _AlertMessageCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csAlertMessage() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csAlertMessageCol = CType(CallByName(_AlertMessageCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csAlertMessageCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csAlertMessageCol = CType(CallByName(_AlertMessageCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csAlertMessageCol) 
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
                  ccHelper.GetPropertyTypeName(New csAlertMessageCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csAlertMessageCol = CType(CallByName(_AlertMessageCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csAlertMessageCol) 
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
 
  Private Sub cc_ctlPnlAlertMessage_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
  Private Sub ctlPnlc_AlertMessage_evtOverrideFillAlertMessageCol(ByRef rAlertMessageCol As csAlertMessageCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) Handles Me.evtOverrideFillAlertMessageCol 
    Dim pFault As clsFault 
 
    If IsFiltered() Then Return 
    If _ctlAlertMessageCol.chkAutoRefresh.Checked Then Return 
 
    rAlertMessageCol = New csAlertMessageCol(vIsLocalized:=True) 
    If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then rAlertMessageCol.OverrideDefaultLanguage(LocalizedTextLanguage) 
    btnFilter.BackColor = Me.BackColor 
    lblTitle.ForeColor = Color.Black 
    _Tooltip.SetToolTip(lblTitle, "") 
    _Tooltip.SetToolTip(btnFilter, "") 
 
    pFault = rAlertMessageCol.Fill(vRequester:=_Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    rAlertMessageCol.SortByNumber() 
 
  End Sub 
  Private Sub ctlPnlc_AlertMessage_evtOverrideLoadCtlAlertMessageCol(ByRef rLoadParameters As ctlc_AlertMessageCol.clsLoadParameters) Handles Me.evtOverrideLoadCtlAlertMessageCol 
    rLoadParameters.SummarizeGrid = False 
  End Sub 
  
End Class 
