Public Class ctlPnlc_LoggedAlert 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlLoggedAlertCol As ctlc_LoggedAlertCol 
  Private WithEvents _ctlLoggedAlert As ctlc_LoggedAlert 
  Private WithEvents _ctlLoggedJobCol As ctlc_LoggedJobCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _LoggedAlertID As Long 
 
  'The data holders 
  Private _LoggedAlertCol As csLoggedAlertCol 
  Private _LoggedAlert As csLoggedAlert 
  Private _LoggedJobCol As csLoggedJobCol 
 
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
  Public Event evtOverrideLoadCboLoggedAlert(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetLoggedAlertIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillLoggedAlertCol(ByRef rLoggedAlertCol As csLoggedAlertCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillLoggedJobCol(ByRef rLoggedJobCol As csLoggedJobCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlLoggedAlertCol(ByRef rLoadParameters As ctlc_LoggedAlertCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedAlert(ByRef rLoadParameters As ctlc_LoggedAlert.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedJobCol(ByRef rLoadParameters As ctlc_LoggedJobCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreLoggedAlertCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtLoggedAlertTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtLoggedJobChosen As Boolean = False 
  Private _ShowPopForEvtLoggedJobChosen As Boolean = False 
  'Parents
  Private _CancelEvtAffectedUserChosen As Boolean = False 
  Private _ShowPopForEvtAffectedUserChosen As Boolean = False 
  Private _CancelEvtLoggedLoginChosen As Boolean = False 
  Private _ShowPopForEvtLoggedLoginChosen As Boolean = False 
  
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
 
    lnkLoggedAlertCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedAlert.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedJobCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    lnkLoggedJobCol.Parent.Controls.Remove(lnkLoggedJobCol) 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vLoggedAlertID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _LoggedAlertID = CType(vLoggedAlertID, Long) 
 
    If _Requester.UserIdentityType <> clsEnums.enmUserIdentityType.Global Then 
      btnFilter.Parent.Controls.Remove(btnFilter) 
    End If 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlLoggedAlert.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkLoggedAlertCol.Visible = False 
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
      pFault = LoadCboLoggedAlerts(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _LoggedAlertID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_LoggedAlertID) 
      End If 
      ChooseLoggedAlert() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_LoggedAlert") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _LoggedAlertID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _LoggedAlertID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_LoggedAlert" OrElse pControlName = "ctlLoggedAlert" Then 
      lnkLoggedAlert.ForeColor = Color.Black : lnkLoggedAlert.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedAlert.BackColor = Color.Wheat 
      If _ctlLoggedAlert Is Nothing Then 
        _ctlLoggedAlert = New ctlc_LoggedAlert() 
        _ctlLoggedAlert.Dock = DockStyle.Fill 
        pnlLoggedAlert.Controls.Add(_ctlLoggedAlert) 
        _ctlLoggedAlert.Visible = False 
      End If 
      If _LoggedAlertID = 0 Then 
        pnlLoggedAlert.Visible = False 
      End If 
      'If _LoggedAlert Is Nothing Then 
      pFault = RefreshCtlLoggedAlert() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlLoggedAlert.LoggedAlert.IsEmpty AndAlso _LoggedAlertID <> -2 Then 
        pnlLoggedAlert.Visible = False 
      End If 
      _ctlLoggedAlert.Name = "ctlc_LoggedAlert" 
      _ActiveControl = _ctlLoggedAlert 
      _ctlLoggedAlert.BringToFront() 
      _ctlLoggedAlert.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_LoggedAlertCol" Then 
      lnkLoggedAlertCol.ForeColor = Color.Black : lnkLoggedAlertCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedAlertCol.BackColor = Color.Wheat 
      If _ctlLoggedAlertCol Is Nothing Then 
        _ctlLoggedAlertCol = New ctlc_LoggedAlertCol() 
        _ctlLoggedAlertCol.Dock = DockStyle.Fill 
        pnlLoggedAlert.Controls.Add(_ctlLoggedAlertCol) 
        _ctlLoggedAlertCol.Visible = False 
      End If  
      pnlLoggedAlert.Visible = True 
      If _LoggedAlertCol Is Nothing Then 
        pFault = RefreshCtlLoggedAlertCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedAlertCol.Name = "ctlc_LoggedAlertCol" 
      _ActiveControl = _ctlLoggedAlertCol 
      _ctlLoggedAlertCol.BringToFront() 
      _ctlLoggedAlertCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlc_LoggedJobCol" Then 
      lnkLoggedJobCol.ForeColor = Color.Black : lnkLoggedJobCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedJobCol.BackColor = Color.Wheat 
      If _ctlLoggedJobCol Is Nothing Then 
      _ctlLoggedJobCol = New ctlc_LoggedJobCol() 
      _ctlLoggedJobCol.Dock = DockStyle.Fill 
      pnlLoggedAlert.Controls.Add(_ctlLoggedJobCol) 
      _ctlLoggedJobCol.Visible = False 
      End If  
      If _LoggedJobCol Is Nothing Then 
        pFault = RefreshCtlLoggedJobCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedJobCol.Name = "ctlc_LoggedJobCol" 
      _ActiveControl = _ctlLoggedJobCol 
      _ctlLoggedJobCol.BringToFront() 
      _ctlLoggedJobCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-LoggedAlert-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("LoggedAlert", _Requester) 
 
    lnkLoggedAlertCol.Text = CCTextTranslate("List", _Requester) 
    lnkLoggedAlert.Text = CCTextTranslate("Details", _Requester) 
 
    lnkLoggedJobCol.Text = TableNameTranslate("LoggedJob", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlLoggedAlert.Controls(0) Is _ctlLoggedAlert Then 
      If _LoggedAlertID = 0 Then 
        pnlLoggedAlert.Visible = False 
      End If 
    ElseIf pnlLoggedAlert.Controls(0) Is _ctlLoggedAlertCol Then 
    ElseIf pnlLoggedAlert.Controls(0) Is _ctlLoggedJobCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pLoggedAlertID As Long = _LoggedAlertID 
      If ccHelper.IsNumeric(pText) Then _LoggedAlertID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetLoggedAlertIDFromIntelliComboText(pText) 
      If pLoggedAlertID <> _LoggedAlertID Then 
        _LoggedAlert = Nothing 
        pFault = ActivateControl("ctlc_LoggedAlert") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlLoggedAlert.Controls(0) Is _ctlLoggedAlert Then 
      pFault = RefreshCtlLoggedAlert() 
    ElseIf pnlLoggedAlert.Controls(0) Is _ctlLoggedAlertCol Then 
      pFault = RefreshCtlLoggedAlertCol() 
    ElseIf pnlLoggedAlert.Controls(0) Is _ctlLoggedJobCol Then 
      pFault = RefreshCtlLoggedJobCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlLoggedAlert.Controls(0).Name, "", "TRGT-LoggedAlert-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboLoggedAlerts(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlLoggedAlertCol_evtRowClicked(ByVal vLoggedAlert As Object) Handles _ctlLoggedAlertCol.evtRowClicked 
    
    If vLoggedAlert Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pLoggedAlert As csLoggedAlert = CType(vLoggedAlert, csLoggedAlert) 
    _LoggedAlertID = pLoggedAlert.ID 
    
    If _ActiveControl Is _ctlLoggedAlertCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByTimeOccurred.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultNumber.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupBySystemName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByCallingApplication.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByAffectedUserID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultType.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultSeverity.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByLoggedLoginID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByDateOccurred.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByMonthOccurred.ToString() Then 
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
    
    ChooseLoggedAlert() 
    
    Try 
      MyIntelliCombo.ValueSelect(_LoggedAlertID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pLoggedAlert.ID.ToString("#,##0")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseLoggedAlert() 
    _LoggedAlert = Nothing 
    lnkLoggedAlert.Visible = True 
    _LoggedJobCol = Nothing 
    lnkLoggedJobCol.Visible = True 
  End Sub 
  Private Sub _ctlLoggedAlertCol_evtRowDoubleClicked(ByVal vLoggedAlert As csLoggedAlert, ByRef rHandled As Boolean) Handles _ctlLoggedAlertCol.evtRowDoubleClicked 
    If lnkLoggedAlert.Parent IsNot flpMenu Then Exit Sub 
    If vLoggedAlert Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByTimeOccurred.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.TimeOccurredStart) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.TimeOccurredStart) 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.TimeOccurredEnd) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.TimeOccurredEnd) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.TimeOccurredStart, vLoggedAlert.TimeOccurred) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultNumber.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultNumberFrom) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultNumberFrom) 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultNumberTo) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultNumberTo) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultNumberFrom, vLoggedAlert.FaultNumber) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupBySystemName.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.SystemName) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.SystemName) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.SystemName, vLoggedAlert.SystemName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByCallingApplication.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.CallingApplication) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.CallingApplication) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.CallingApplication, vLoggedAlert.CallingApplication) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByAffectedUserID.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.AffectedUserID) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.AffectedUserID) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.AffectedUserID, vLoggedAlert.AffectedUserID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultType.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultType) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultType) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultType, vLoggedAlert.FaultType) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultSeverity.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultSeverity) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultSeverity) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultSeverity, vLoggedAlert.FaultSeverity) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByLoggedLoginID.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.LoggedLoginID) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.LoggedLoginID) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.LoggedLoginID, vLoggedAlert.LoggedLoginID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByDateOccurred.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.DateOccurredStart) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.DateOccurredStart) 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.DateOccurredEnd) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.DateOccurredEnd) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.DateOccurredStart, vLoggedAlert.DateOccurred) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByMonthOccurred.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.MonthOccurredStart) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.MonthOccurredStart) 
        If pSearchFilters.ContainsKey(csLoggedAlertCol.enmFillOnTheFlyParameters.MonthOccurredEnd) Then pSearchFilters.Remove(csLoggedAlertCol.enmFillOnTheFlyParameters.MonthOccurredEnd) 
        pSearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.MonthOccurredStart, vLoggedAlert.MonthOccurred) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreLoggedAlertCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vLoggedAlert.ID, vLoggedAlert.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _LoggedAlertID = vLoggedAlert.ID 
      'MyIntelliCombo.ValueSelect(_LoggedAlertID) 
      pFault = ActivateControl("ctlc_LoggedAlert") 
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
      pFault = _LoggedAlertCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _LoggedAlertCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedAlertCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedAlertCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_LoggedAlertCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csLoggedAlert.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedAlert" 
      pFault = _ctlLoggedAlertCol.LoadControl(_LoggedAlertCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlLoggedAlertCol_evtUnChosen() Handles _ctlLoggedAlertCol.evtUnChosen 
 
    _LoggedAlertID = 0 
    _LoggedAlert = Nothing 
    _LoggedJobCol = Nothing 
    lnkLoggedJobCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkLoggedAlert.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkLoggedJobCol.Click, 
      lnkLoggedAlertCol.Click, 
      lnkLoggedAlert.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkLoggedAlert OrElse (lnk Is lnkLoggedAlertCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlLoggedAlertCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_LoggedAlertCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csLoggedAlert.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csLoggedAlertCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillLoggedAlertCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _LoggedAlertCol = New csLoggedAlertCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _LoggedAlertCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlLoggedAlertCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _LoggedAlertCol.FillByAffectedUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=100, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then  
              _ctlLoggedAlertCol.Timer?.Stop()  
              Return pFault  
            End If  
          Case Else 
            If _ctlLoggedAlertCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _LoggedAlertCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlLoggedAlertCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _LoggedAlertCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _LoggedAlertCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedAlertCol.Count) 
      End If 
    Else 
      _LoggedAlertCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedAlertCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlLoggedAlertCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedAlert" 
    
    Dim pLoggedAlertID As Long = _LoggedAlertID 
    
    pFault = _ctlLoggedAlertCol.LoadControl(_LoggedAlertCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedAlertCol.Visible = True 
    
    _ctlLoggedAlertCol.Refresh() 
    If pLoggedAlertID <> 0 Then 
      Dim pLoggedAlertCol As csLoggedAlertCol = CType(_ctlLoggedAlertCol.bsCtlLoggedAlert.DataSource, csLoggedAlertCol) 
      Dim pLoggedAlert As csLoggedAlert = pLoggedAlertCol.FindByID(pLoggedAlertID) 
      If pLoggedAlert.ID > 0 Then 
        _ctlLoggedAlertCol.bsCtlLoggedAlert.CurrencyManager.Position = pLoggedAlertCol.IndexOf(pLoggedAlert) 
        _ctlLoggedAlertCol.dgvLoggedAlert.Rows(pLoggedAlertCol.IndexOf(pLoggedAlert)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlLoggedAlert() As clsFault 
    Dim pFault As New clsFault 
    
    If _LoggedAlertID > 0 Then 
      ChooseLoggedAlert() 
      _LoggedAlert = New csLoggedAlert(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedAlert.GetByID(_LoggedAlertID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _LoggedAlert = New csLoggedAlert(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _LoggedAlert.ID.ToString("#,##0")    
     
    Dim pLoadParameters As New ctlc_LoggedAlert.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = True 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedAlert(pLoadParameters)
    pFault = _ctlLoggedAlert.LoadControl(_LoggedAlert, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlLoggedAlert.Visible = True 
    Return pFault 
  End Function 
  Private Function RefreshCtlLoggedJobCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlLoggedJobCol.dgvLoggedJob.SelectedRows.Count > 0 Then 
      Dim pLoggedJob As csLoggedJob = CType(_ctlLoggedJobCol.bsCtlLoggedJob.Current, csLoggedJob) 
      pID = pLoggedJob.ID 
    End If 
 
    Dim pTestCol As csLoggedJobCol = Nothing 
    RaiseEvent evtOverrideFillLoggedJobCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _LoggedJobCol = New csLoggedJobCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedJobCol.FillByLoggedAlertID(_LoggedAlertID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _LoggedJobCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedJobCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedJobCol.Count) 
      End If 
    Else 
      _LoggedJobCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedJobCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_LoggedJobCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _LoggedAlert IsNot Nothing AndAlso Not String.IsNullOrEmpty(_LoggedAlert.DefaultDesignation) Then 
        .ReportTitle = "List of LoggedJobs for " & _LoggedAlert.DefaultDesignation 
      Else 
        .ReportTitle = "List of LoggedJobs for LoggedAlert" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csLoggedJob.enmProperty.LoggedAlert) 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedJobCol(pLoadParameters)
    
    pFault = _ctlLoggedJobCol.LoadControl(_LoggedJobCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedJobCol.Visible = True 
 
    If pID > 0 Then 
      Dim pLoggedJobs As csLoggedJobCol = CType(_ctlLoggedJobCol.bsCtlLoggedJob.DataSource, csLoggedJobCol) 
      Dim pLoggedJob As csLoggedJob = pLoggedJobs.FindByID((pID)) 
      If pLoggedJob.ID > 0 Then 
        _ctlLoggedJobCol.bsCtlLoggedJob.CurrencyManager.Position = pLoggedJobs.IndexOf(pLoggedJob) 
        _ctlLoggedJobCol.dgvLoggedJob.Rows(pLoggedJobs.IndexOf(pLoggedJob)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Function LoadCboLoggedAlerts(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_LoggedAlertDefaultByID 
    Dim pParentID As Long = 0 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.c_User Then 
      pComboListTypeToLoad = clsEnums.enmComboListType.c_LoggedAlertForAffectedUserDefaultByID 
      pParentID = _Requester.UserIdentityInstanceID 
    End If 
    
    RaiseEvent evtOverrideLoadCboLoggedAlert(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _LoggedAlertID >= 0 Then 
      MyIntelliCombo.ValueSelect(_LoggedAlertID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _LoggedAlertID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _LoggedAlertID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetLoggedAlertIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _LoggedAlertID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _LoggedAlertID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _LoggedAlertID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _LoggedAlertID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseLoggedAlert() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_LoggedAlert", StringComparison.OrdinalIgnoreCase) AndAlso _LoggedAlertID > 0 Then 
        'to avoid getting ObjectNotFound 
        _LoggedAlert = New csLoggedAlert(clsEnums.enmLoadParent.TextOnly) 
        pFault = _LoggedAlert.GetByID(_LoggedAlertID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_LoggedAlert") 
    End If 
    pnlLoggedAlert.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  Private Sub _ctlLoggedJobCol_evtRowDoubleClicked(ByVal vLoggedJob As csLoggedJob, ByRef rHandled As Boolean) Handles _ctlLoggedJobCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtLoggedJobChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtLoggedJobChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vLoggedJob.ID 
      .Object = New csLoggedJob 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csLoggedAlert.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlLoggedAlert.evtParentChosen 
    If vParentName = csLoggedAlert.enmParentProperty.AffectedUser Then 
      rHandled = True 
      If _CancelEvtAffectedUserChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New csUser 
      End With 
      Try 
        RaiseEvent evtEntityChosen(Me, pEventArgs) 
      Catch ex As Exception 
        rHandled = False 
      End Try 
    End If 
    If vParentName = csLoggedAlert.enmParentProperty.LoggedLogin Then 
      rHandled = True 
      If _CancelEvtLoggedLoginChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New csLoggedLogin 
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
    pnlLoggedAlert.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkLoggedAlertCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _LoggedAlertID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_LoggedAlertCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkLoggedAlertCol.Visible = False 
      _ActiveControl = _ctlLoggedAlert 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboLoggedAlerts(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _LoggedAlertID <> 0 Then 
        MyIntelliCombo.cbo.Text = _LoggedAlertID.ToString() 
        pFault = ActivateControl("ctlc_LoggedAlert") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlLoggedAlert.Visible = False 
        _LoggedAlertID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _LoggedAlertID > 0 Then pnlLoggedAlert.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkLoggedJobCol.MouseEnter, 
                  lnkLoggedAlertCol.MouseEnter, 
                  lnkLoggedAlert.MouseEnter, 
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
                  lnkLoggedJobCol.MouseLeave, 
                  lnkLoggedAlertCol.MouseLeave, 
                  lnkLoggedAlert.MouseLeave, 
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
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pTimeOccurredStart As Nullable(Of Date) = Nothing 
    Dim pTimeOccurredEnd As Nullable(Of Date) = Nothing 
    Dim pFaultNumberFrom As Nullable(Of Integer) = Nothing 
    Dim pFaultNumberTo As Nullable(Of Integer) = Nothing 
    Dim pSystemName As String = Nothing 
    Dim pSystemNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pCallingApplication As String = Nothing 
    Dim pCallingApplicationWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pAffectedUserID As Nullable(Of Long) = Nothing 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.c_User Then 
      pAffectedUserID = _Requester.UserIdentityInstanceID 
    End If 
    Dim pFaultType As clsEnums.enmFaultType = Nothing 
    Dim pFaultSeverity As clsEnums.enmFaultSeverity = Nothing 
    Dim pLoggedLoginID As Nullable(Of Long) = Nothing 
    Dim pDateOccurredStart As Nullable(Of Date) = Nothing 
    Dim pDateOccurredEnd As Nullable(Of Date) = Nothing 
    Dim pMonthOccurredStart As Nullable(Of Date) = Nothing 
    Dim pMonthOccurredEnd As Nullable(Of Date) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByTimeOccurred As Boolean = False 
    Dim pGroupByFaultNumber As Boolean = False 
    Dim pGroupBySystemName As Boolean = False 
    Dim pGroupByCallingApplication As Boolean = False 
    Dim pGroupByAffectedUserID As Boolean = False 
    Dim pGroupByFaultType As Boolean = False 
    Dim pGroupByFaultSeverity As Boolean = False 
    Dim pGroupByLoggedLoginID As Boolean = False 
    Dim pGroupByDateOccurred As Boolean = False 
    Dim pGroupByMonthOccurred As Boolean = False 
    
    Dim pSumFaultNumber As Boolean = False 
    Dim pSumUserIdentityTypeNameCode As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Logged Alerts"  
  
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
        .Date01Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.TimeOccurred), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.TimeOccurred), "Time Occurred") 
        .Date01From.TabIndex = 3 
        .Date01To.TabIndex = 4 
        .Date01From.ShowCheckBox = True 
        .Date01To.ShowCheckBox = True 
        .Date01From.Checked = False 
        .Date01To.Checked = False 
        If _ctlLoggedAlertCol.LoadParameters.ColumnsFormat.ContainsKey(csLoggedAlert.enmProperty.TimeOccurred) Then 
          .Date01From.CustomFormat = _ctlLoggedAlertCol.LoadParameters.ColumnsFormat(csLoggedAlert.enmProperty.TimeOccurred) 
          .Date01To.CustomFormat = _ctlLoggedAlertCol.LoadParameters.ColumnsFormat(csLoggedAlert.enmProperty.TimeOccurred) 
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
 
        .Text01Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.FaultNumber), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.FaultNumber), "Fault Number") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 5 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 6 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .String01Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.SystemName), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.SystemName), "System Name") 
        .String01Text.Text = "" 
        .String01Text.TabIndex = 7 
        With .String01WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 8 
        End With 
        .flpFilter.Controls.Add(.String01Label) 
        .flpFilter.Controls.Add(.String01Text) 
        .flpFilter.Controls.Add(.String01LblWCType) 
        .flpFilter.Controls.Add(.String01WCType) 
 
        .String02Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.CallingApplication), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.CallingApplication), "Calling Application") 
        .String02Text.Text = "" 
        .String02Text.TabIndex = 9 
        With .String02WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 10 
        End With 
        .flpFilter.Controls.Add(.String02Label) 
        .flpFilter.Controls.Add(.String02Text) 
        .flpFilter.Controls.Add(.String02LblWCType) 
        .flpFilter.Controls.Add(.String02WCType) 
 
        If pAffectedUserID Is Nothing Then 
         .Combo01Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.AffectedUser), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.AffectedUser), "Affected User") 
         Dim pAffectedUsers As New clsComboList 
         pFault = MyCache.GetComboList(clsEnums.enmComboListType.c_UserDefaultByID, pAffectedUsers) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
         'If pAffectedUsers IsNot Nothing AndAlso pAffectedUsers.Count > 0 Then 
         .flpFilter.Controls.Add(.Combo01Label) 
         .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
         'End If 
         With .Combo01 
           .MakeSmart() 
           If pAffectedUsers IsNot Nothing Then 
             .LoadControl(pAffectedUsers, GetChoose(_Requester)) 
           Else 
             .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.c_UserDefaultByID, 0, _Requester) 
           End If 
           .TabIndex = 11 
         End With 
        End If 
 
        .Combo02Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.FaultType), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.FaultType), "Fault Type") 
        Dim pFaultTypes As New clsComboList 
        pFault = pFaultTypes.FillEnums(clsEnums.enmEnum.FaultType, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pFaultTypes.Remove(pFaultTypes.FindByKey(clsEnums.enmFaultType.UD)) 
        pFaultTypes.SortByText() 
        If pFaultTypes IsNot Nothing AndAlso pFaultTypes.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo02Label) 
          .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo02 
          .MakeSmart() 
          .LoadControl(pFaultTypes, GetChoose(_Requester)) 
          .TabIndex = 12 
        End With 
 
        .Combo03Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.FaultSeverity), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.FaultSeverity), "Fault Severity") 
        Dim pFaultSeveritys As New clsComboList 
        pFault = pFaultSeveritys.FillEnums(clsEnums.enmEnum.FaultSeverity, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pFaultSeveritys.Remove(pFaultSeveritys.FindByKey(clsEnums.enmFaultSeverity.UD)) 
        pFaultSeveritys.SortByText() 
        If pFaultSeveritys IsNot Nothing AndAlso pFaultSeveritys.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo03Label) 
          .flpFilter.Controls.Add(.Combo03)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo03 
          .MakeSmart() 
          .LoadControl(pFaultSeveritys, GetChoose(_Requester)) 
          .TabIndex = 13 
        End With 
 
        .Text02Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.LoggedLogin), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.LoggedLogin), "Logged Login") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 14 
        .Text02From.Width = .String01Text.Width 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 15 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.SetFlowBreak(.Text02From, True) 
 
        .Date02Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.DateOccurred), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.DateOccurred), "Date Occurred") 
        .Date02From.TabIndex = 16 
        .Date02To.TabIndex = 17 
        .Date02From.ShowCheckBox = True 
        .Date02To.ShowCheckBox = True 
        .Date02From.Checked = False 
        .Date02To.Checked = False 
        If _ctlLoggedAlertCol.LoadParameters.ColumnsFormat.ContainsKey(csLoggedAlert.enmProperty.DateOccurred) Then 
          .Date02From.CustomFormat = _ctlLoggedAlertCol.LoadParameters.ColumnsFormat(csLoggedAlert.enmProperty.DateOccurred) 
          .Date02To.CustomFormat = _ctlLoggedAlertCol.LoadParameters.ColumnsFormat(csLoggedAlert.enmProperty.DateOccurred) 
        Else 
          .Date02From.CustomFormat = "dd-MM-yyyy" 
          .Date02To.CustomFormat = "dd-MM-yyyy" 
        End If 
        If .Date02From.CustomFormat.IndexOf("dd") >= 0 Then 
          .Date02From.Value = pNowStart 
          .Date02To.Value = pNowEnd 
        Else 
          .Date02From.Value = pNowMonthStart 
          .Date02To.Value = pNowMonthEnd 
        End If 
        .flpFilter.Controls.Add(.Date02Label) 
        .flpFilter.Controls.Add(.Date02From) 
        .flpFilter.Controls.Add(.Date02lblTo) 
        .flpFilter.Controls.Add(.Date02To) 
 
        .Date03Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.MonthOccurred), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.MonthOccurred), "Month Occurred") 
        .Date03From.TabIndex = 18 
        .Date03To.TabIndex = 19 
        .Date03From.ShowCheckBox = True 
        .Date03To.ShowCheckBox = True 
        .Date03From.Checked = False 
        .Date03To.Checked = False 
        If _ctlLoggedAlertCol.LoadParameters.ColumnsFormat.ContainsKey(csLoggedAlert.enmProperty.MonthOccurred) Then 
          .Date03From.CustomFormat = _ctlLoggedAlertCol.LoadParameters.ColumnsFormat(csLoggedAlert.enmProperty.MonthOccurred) 
          .Date03To.CustomFormat = _ctlLoggedAlertCol.LoadParameters.ColumnsFormat(csLoggedAlert.enmProperty.MonthOccurred) 
        Else 
          .Date03From.CustomFormat = "dd-MM-yyyy" 
          .Date03To.CustomFormat = "dd-MM-yyyy" 
        End If 
        If .Date03From.CustomFormat.IndexOf("dd") >= 0 Then 
          .Date03From.Value = pNowStart 
          .Date03To.Value = pNowEnd 
        Else 
          .Date03From.Value = pNowMonthStart 
          .Date03To.Value = pNowMonthEnd 
        End If 
        .flpFilter.Controls.Add(.Date03Label) 
        .flpFilter.Controls.Add(.Date03From) 
        .flpFilter.Controls.Add(.Date03lblTo) 
        .flpFilter.Controls.Add(.Date03To) 
 
        .Text03Label.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.ID), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.ID), "ID") 
        .Text03From.Text = "" 
        .Text03From.TabIndex = 20 
        .Text03To.Text = "" 
        .Text03To.TabIndex = 21 
        .flpFilter.Controls.Add(.Text03Label) 
        .flpFilter.Controls.Add(.Text03From) 
        .flpFilter.Controls.Add(.Text03LblTo) 
        .flpFilter.Controls.Add(.Text03To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.TimeOccurred), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.TimeOccurred), "Time Occurred") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 22 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.FaultNumber), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.FaultNumber), "Fault Number") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 23 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.SystemName), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.SystemName), "System Name") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 24 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .lblGroupBy04.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.CallingApplication), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.CallingApplication), "Calling Application") 
        .chkGroupBy04.Checked = False 
        .chkGroupBy04.TabIndex = 25 
        .flpGroupBy.Controls.Add(.lblGroupBy04) 
        .flpGroupBy.Controls.Add(.chkGroupBy04) 
 
        .lblGroupBy05.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.AffectedUser), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.AffectedUser), "Affected User") 
        .chkGroupBy05.Checked = False 
        .chkGroupBy05.TabIndex = 26 
        .flpGroupBy.Controls.Add(.lblGroupBy05) 
        .flpGroupBy.Controls.Add(.chkGroupBy05) 
 
        .lblGroupBy06.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.FaultType), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.FaultType), "Fault Type") 
        .chkGroupBy06.Checked = False 
        .chkGroupBy06.TabIndex = 27 
        .flpGroupBy.Controls.Add(.lblGroupBy06) 
        .flpGroupBy.Controls.Add(.chkGroupBy06) 
 
        .lblGroupBy07.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.FaultSeverity), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.FaultSeverity), "Fault Severity") 
        .chkGroupBy07.Checked = False 
        .chkGroupBy07.TabIndex = 28 
        .flpGroupBy.Controls.Add(.lblGroupBy07) 
        .flpGroupBy.Controls.Add(.chkGroupBy07) 
 
        .lblGroupBy08.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.LoggedLogin), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.LoggedLogin), "Logged Login") 
        .chkGroupBy08.Checked = False 
        .chkGroupBy08.TabIndex = 29 
        .flpGroupBy.Controls.Add(.lblGroupBy08) 
        .flpGroupBy.Controls.Add(.chkGroupBy08) 
 
        .lblGroupBy09.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.DateOccurred), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.DateOccurred), "Date Occurred") 
        .chkGroupBy09.Checked = False 
        .chkGroupBy09.TabIndex = 30 
        .flpGroupBy.Controls.Add(.lblGroupBy09) 
        .flpGroupBy.Controls.Add(.chkGroupBy09) 
 
        .lblGroupBy10.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.MonthOccurred), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.MonthOccurred), "Month Occurred") 
        .chkGroupBy10.Checked = False 
        .chkGroupBy10.TabIndex = 31 
        .flpGroupBy.Controls.Add(.lblGroupBy10) 
        .flpGroupBy.Controls.Add(.chkGroupBy10) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.FaultNumber), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.FaultNumber), "Fault Number") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 32 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedAlert.enmProperty.UserIdentityTypeName), _ctlLoggedAlertCol.LoadParameters.ColumnsHeaderText(csLoggedAlert.enmProperty.UserIdentityTypeName), "User Identity Type Name") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 33 
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
      If .Date01From.Checked OrElse .Date01To.Checked Then 
        pTimeOccurredStart = .Date01From.Value 
        pTimeOccurredEnd = .Date01To.Value 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.TimeOccurredStart, pTimeOccurredStart) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.TimeOccurredEnd, pTimeOccurredEnd) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pFaultNumberFrom = ccHelper.ToInteger(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pFaultNumberTo = ccHelper.ToInteger(.Text01To.Text) 
          Else 
            pFaultNumberTo = pFaultNumberFrom 
          End If 
          _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultNumberFrom, pFaultNumberFrom) 
          _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultNumberTo, pFaultNumberTo) 
        End If 
      End If 
      If .String01Text.Text <> "" Then 
        pSystemName = .String01Text.Text 
        pSystemNameWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.SystemName, pSystemName) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.SystemNameWildcardType, pSystemNameWildcardType) 
      End If 
      If .String02Text.Text <> "" Then 
        pCallingApplication = .String02Text.Text 
        pCallingApplicationWildcardType = CType(CType(.String02WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.CallingApplication, pCallingApplication) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.CallingApplicationWildcardType, pCallingApplicationWildcardType) 
      End If 
      If pAffectedUserID Is Nothing Then 
       If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
         pAffectedUserID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
         _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.AffectedUserID, pAffectedUserID) 
       End If 
      Else 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.AffectedUserID, pAffectedUserID) 
      End If  
      If .Combo02.SelectedItem IsNot Nothing Then 
        pFaultType = CType(CType(.Combo02.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmFaultType) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultType, pFaultType) 
      End If 
      If .Combo03.SelectedItem IsNot Nothing Then 
        pFaultSeverity = CType(CType(.Combo03.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmFaultSeverity) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.FaultSeverity, pFaultSeverity) 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pLoggedLoginID = ccHelper.ToLong(.Text02From.Text) 
          _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.LoggedLoginID, pLoggedLoginID) 
        End If 
      End If 
      If .Date02From.Checked OrElse .Date02To.Checked Then 
        pDateOccurredStart = .Date02From.Value 
        pDateOccurredEnd = .Date02To.Value 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.DateOccurredStart, pDateOccurredStart) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.DateOccurredEnd, pDateOccurredEnd) 
      End If 
      If .Date03From.Checked OrElse .Date03To.Checked Then 
        pMonthOccurredStart = .Date03From.Value 
        pMonthOccurredEnd = .Date03To.Value 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.MonthOccurredStart, pMonthOccurredStart) 
        _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.MonthOccurredEnd, pMonthOccurredEnd) 
      End If 
      If .Text03From.Text <> "" Then 
        If IsNumeric(.Text03From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text03From.Text) 
          If .Text03To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text03To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csLoggedAlertCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csLoggedAlertCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csLoggedAlertCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByTimeOccurred = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByTimeOccurred, pGroupByTimeOccurred) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByFaultNumber = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultNumber, pGroupByFaultNumber) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupBySystemName = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupBySystemName, pGroupBySystemName) 
      End If 
      If .chkGroupBy04.Checked = True Then 
        pGroupByCallingApplication = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByCallingApplication, pGroupByCallingApplication) 
      End If 
      If .chkGroupBy05.Checked = True Then 
        pGroupByAffectedUserID = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByAffectedUserID, pGroupByAffectedUserID) 
      End If 
      If .chkGroupBy06.Checked = True Then 
        pGroupByFaultType = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultType, pGroupByFaultType) 
      End If 
      If .chkGroupBy07.Checked = True Then 
        pGroupByFaultSeverity = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByFaultSeverity, pGroupByFaultSeverity) 
      End If 
      If .chkGroupBy08.Checked = True Then 
        pGroupByLoggedLoginID = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByLoggedLoginID, pGroupByLoggedLoginID) 
      End If 
      If .chkGroupBy09.Checked = True Then 
        pGroupByDateOccurred = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByDateOccurred, pGroupByDateOccurred) 
      End If 
      If .chkGroupBy10.Checked = True Then 
        pGroupByMonthOccurred = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedAlertCol.enmFillSumOnTheFlyParameters.GroupByMonthOccurred, pGroupByMonthOccurred) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumFaultNumber = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumUserIdentityTypeNameCode = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csLoggedAlertCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csLoggedAlertCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csLoggedAlertCol.enmListDefinition.Dir) Then _SearchFilters.Add(csLoggedAlertCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_LoggedAlertCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_LoggedAlertCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csLoggedAlert.enmProperty.ID, "ID") 
      End With 
      _LoggedAlertCol = New csLoggedAlertCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _LoggedAlertCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _LoggedAlertCol.FillByAffectedUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
          Case Else 
            pFault = _LoggedAlertCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _LoggedAlertCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedAlertCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedAlertCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedAlert" 
      RaiseEvent evtOverrideLoadCtlLoggedAlertCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _LoggedAlertCol = New csLoggedAlertCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedAlertCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_LoggedAlertCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _LoggedAlertCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csLoggedAlert.enmProperty.ID, "Count") 
        If pGroupByTimeOccurred = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.TimeOccurred) 
        If pGroupByFaultNumber = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultNumber) 
        If pGroupBySystemName = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.SystemName) 
        If pGroupByCallingApplication = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.CallingApplication) 
        If pGroupByAffectedUserID = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.AffectedUser) 
        If pGroupByFaultType = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultType) 
        If pGroupByFaultSeverity = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultSeverity) 
        If pGroupByLoggedLoginID = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.LoggedLogin) 
        If pGroupByDateOccurred = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.DateOccurred) 
        If pGroupByMonthOccurred = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.MonthOccurred) 
        If pSumFaultNumber = False Then .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultNumber) 
        If pGroupByFaultNumber = True OrElse pSumFaultNumber = True Then If .ColumnsHide.Contains(csLoggedAlert.enmProperty.FaultNumber) Then .ColumnsHide.Remove(csLoggedAlert.enmProperty.FaultNumber) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.CallingApplicationVersion) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.CallingFunctionWithinApplication) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.FreeText) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultingAssembly) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.AssemblyEntryPoint) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultingClass) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultingFunction) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultingFunctionParameters) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultIdent) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.FaultDescription) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.MessageSentToUser) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.ActionSentToUser) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.Thread) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.UserIdentityType) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.UserIdentityTypeName) 
        .ColumnsHide.Add(csLoggedAlert.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlLoggedAlertCol.Visible = True 
    pFault = _ctlLoggedAlertCol.LoadControl(_LoggedAlertCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csLoggedAlertCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csLoggedAlertCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlLoggedAlert.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlLoggedAlert.Controls(0).Name) 
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
 
  Private Sub _ctlLoggedAlertCol_evtTimerTripped() Handles _ctlLoggedAlertCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtLoggedAlertTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlLoggedAlertCol.LoggedAlertCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlLoggedAlertCol.LoggedAlertCol(0).ID 
 
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
    If _LoggedAlertCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csLoggedAlert() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csLoggedAlertCol = CType(CallByName(_LoggedAlertCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedAlertCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csLoggedAlertCol = CType(CallByName(_LoggedAlertCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedAlertCol) 
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
                  ccHelper.GetPropertyTypeName(New csLoggedAlertCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csLoggedAlertCol = CType(CallByName(_LoggedAlertCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedAlertCol) 
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
 
  Private Sub cc_ctlPnlLoggedAlert_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
