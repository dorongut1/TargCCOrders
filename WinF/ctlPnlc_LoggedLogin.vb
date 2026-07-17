Public Class ctlPnlc_LoggedLogin 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlLoggedLoginCol As ctlc_LoggedLoginCol 
  Private WithEvents _ctlLoggedLogin As ctlc_LoggedLogin 
  Private WithEvents _ctlLoggedAlertCol As ctlc_LoggedAlertCol 
  Private WithEvents _ctlLoggedRequestCol As ctlc_LoggedRequestCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _LoggedLoginID As Long 
 
  'The data holders 
  Private _LoggedLoginCol As csLoggedLoginCol 
  Private _LoggedLogin As csLoggedLogin 
  Private _LoggedAlertCol As csLoggedAlertCol 
  Private _LoggedRequestCol As csLoggedRequestCol 
 
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
  Public Event evtOverrideLoadCboLoggedLogin(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetLoggedLoginIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillLoggedLoginCol(ByRef rLoggedLoginCol As csLoggedLoginCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillLoggedAlertCol(ByRef rLoggedAlertCol As csLoggedAlertCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillLoggedRequestCol(ByRef rLoggedRequestCol As csLoggedRequestCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlLoggedLoginCol(ByRef rLoadParameters As ctlc_LoggedLoginCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedLogin(ByRef rLoadParameters As ctlc_LoggedLogin.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedAlertCol(ByRef rLoadParameters As ctlc_LoggedAlertCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedRequestCol(ByRef rLoadParameters As ctlc_LoggedRequestCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreLoggedLoginCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtLoggedLoginTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtLoggedAlertChosen As Boolean = False 
  Private _ShowPopForEvtLoggedAlertChosen As Boolean = False 
  Private _CancelEvtLoggedRequestChosen As Boolean = False 
  Private _ShowPopForEvtLoggedRequestChosen As Boolean = False 
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
 
    lnkLoggedLoginCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedLogin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedAlertCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedRequestCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vLoggedLoginID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _LoggedLoginID = CType(vLoggedLoginID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlLoggedLogin.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkLoggedLoginCol.Visible = False 
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
      pFault = LoadCboLoggedLogins(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _LoggedLoginID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_LoggedLoginID) 
      End If 
      ChooseLoggedLogin() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_LoggedLogin") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _LoggedLoginID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _LoggedLoginID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_LoggedLogin" OrElse pControlName = "ctlLoggedLogin" Then 
      lnkLoggedLogin.ForeColor = Color.Black : lnkLoggedLogin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedLogin.BackColor = Color.Wheat 
      If _ctlLoggedLogin Is Nothing Then 
        _ctlLoggedLogin = New ctlc_LoggedLogin() 
        _ctlLoggedLogin.Dock = DockStyle.Fill 
        pnlLoggedLogin.Controls.Add(_ctlLoggedLogin) 
        _ctlLoggedLogin.Visible = False 
      End If 
      If _LoggedLoginID = 0 Then 
        pnlLoggedLogin.Visible = False 
      End If 
      'If _LoggedLogin Is Nothing Then 
      pFault = RefreshCtlLoggedLogin() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlLoggedLogin.LoggedLogin.IsEmpty AndAlso _LoggedLoginID <> -2 Then 
        pnlLoggedLogin.Visible = False 
      End If 
      _ctlLoggedLogin.Name = "ctlc_LoggedLogin" 
      _ActiveControl = _ctlLoggedLogin 
      _ctlLoggedLogin.BringToFront() 
      _ctlLoggedLogin.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_LoggedLoginCol" Then 
      lnkLoggedLoginCol.ForeColor = Color.Black : lnkLoggedLoginCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedLoginCol.BackColor = Color.Wheat 
      If _ctlLoggedLoginCol Is Nothing Then 
        _ctlLoggedLoginCol = New ctlc_LoggedLoginCol() 
        _ctlLoggedLoginCol.Dock = DockStyle.Fill 
        pnlLoggedLogin.Controls.Add(_ctlLoggedLoginCol) 
        _ctlLoggedLoginCol.Visible = False 
      End If  
      pnlLoggedLogin.Visible = True 
      If _LoggedLoginCol Is Nothing Then 
        pFault = RefreshCtlLoggedLoginCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedLoginCol.Name = "ctlc_LoggedLoginCol" 
      _ActiveControl = _ctlLoggedLoginCol 
      _ctlLoggedLoginCol.BringToFront() 
      _ctlLoggedLoginCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlc_LoggedAlertCol" Then 
      lnkLoggedAlertCol.ForeColor = Color.Black : lnkLoggedAlertCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedAlertCol.BackColor = Color.Wheat 
      If _ctlLoggedAlertCol Is Nothing Then 
      _ctlLoggedAlertCol = New ctlc_LoggedAlertCol() 
      _ctlLoggedAlertCol.Dock = DockStyle.Fill 
      pnlLoggedLogin.Controls.Add(_ctlLoggedAlertCol) 
      _ctlLoggedAlertCol.Visible = False 
      End If  
      If _LoggedAlertCol Is Nothing Then 
        pFault = RefreshCtlLoggedAlertCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedAlertCol.Name = "ctlc_LoggedAlertCol" 
      _ActiveControl = _ctlLoggedAlertCol 
      _ctlLoggedAlertCol.BringToFront() 
      _ctlLoggedAlertCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_LoggedRequestCol" Then 
      lnkLoggedRequestCol.ForeColor = Color.Black : lnkLoggedRequestCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedRequestCol.BackColor = Color.Wheat 
      If _ctlLoggedRequestCol Is Nothing Then 
      _ctlLoggedRequestCol = New ctlc_LoggedRequestCol() 
      _ctlLoggedRequestCol.Dock = DockStyle.Fill 
      pnlLoggedLogin.Controls.Add(_ctlLoggedRequestCol) 
      _ctlLoggedRequestCol.Visible = False 
      End If  
      If _LoggedRequestCol Is Nothing Then 
        pFault = RefreshCtlLoggedRequestCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedRequestCol.Name = "ctlc_LoggedRequestCol" 
      _ActiveControl = _ctlLoggedRequestCol 
      _ctlLoggedRequestCol.BringToFront() 
      _ctlLoggedRequestCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-LoggedLogin-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("LoggedLogin", _Requester) 
 
    lnkLoggedLoginCol.Text = CCTextTranslate("List", _Requester) 
    lnkLoggedLogin.Text = CCTextTranslate("Details", _Requester) 
 
    lnkLoggedAlertCol.Text = TableNameTranslate("LoggedAlert", _Requester, vMakePlural:=True) 
    lnkLoggedRequestCol.Text = TableNameTranslate("LoggedRequest", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlLoggedLogin.Controls(0) Is _ctlLoggedLogin Then 
      If _LoggedLoginID = 0 Then 
        pnlLoggedLogin.Visible = False 
      End If 
    ElseIf pnlLoggedLogin.Controls(0) Is _ctlLoggedLoginCol Then 
    ElseIf pnlLoggedLogin.Controls(0) Is _ctlLoggedAlertCol Then 
    ElseIf pnlLoggedLogin.Controls(0) Is _ctlLoggedRequestCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pLoggedLoginID As Long = _LoggedLoginID 
      If ccHelper.IsNumeric(pText) Then _LoggedLoginID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetLoggedLoginIDFromIntelliComboText(pText) 
      If pLoggedLoginID <> _LoggedLoginID Then 
        _LoggedLogin = Nothing 
        pFault = ActivateControl("ctlc_LoggedLogin") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlLoggedLogin.Controls(0) Is _ctlLoggedLogin Then 
      pFault = RefreshCtlLoggedLogin() 
    ElseIf pnlLoggedLogin.Controls(0) Is _ctlLoggedLoginCol Then 
      pFault = RefreshCtlLoggedLoginCol() 
    ElseIf pnlLoggedLogin.Controls(0) Is _ctlLoggedAlertCol Then 
      pFault = RefreshCtlLoggedAlertCol() 
    ElseIf pnlLoggedLogin.Controls(0) Is _ctlLoggedRequestCol Then 
      pFault = RefreshCtlLoggedRequestCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlLoggedLogin.Controls(0).Name, "", "TRGT-LoggedLogin-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboLoggedLogins(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlLoggedLoginCol_evtRowClicked(ByVal vLoggedLogin As Object) Handles _ctlLoggedLoginCol.evtRowClicked 
    
    If vLoggedLogin Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pLoggedLogin As csLoggedLogin = CType(vLoggedLogin, csLoggedLogin) 
    _LoggedLoginID = pLoggedLogin.ID 
    
    If _ActiveControl Is _ctlLoggedLoginCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByUserName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByTimeLoggedIn.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByApplicationName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByLoginFaultNumber.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByOriginatingCountry.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByDateLoggedIn.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByMonthLoggedIn.ToString() Then 
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
    
    ChooseLoggedLogin() 
    
    Try 
      MyIntelliCombo.ValueSelect(_LoggedLoginID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pLoggedLogin.ID.ToString("#,##0")
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseLoggedLogin() 
    _LoggedLogin = Nothing 
    lnkLoggedLogin.Visible = True 
    _LoggedAlertCol = Nothing 
    lnkLoggedAlertCol.Visible = True 
    _LoggedRequestCol = Nothing 
    lnkLoggedRequestCol.Visible = True 
  End Sub 
  Private Sub _ctlLoggedLoginCol_evtRowDoubleClicked(ByVal vLoggedLogin As csLoggedLogin, ByRef rHandled As Boolean) Handles _ctlLoggedLoginCol.evtRowDoubleClicked 
    If lnkLoggedLogin.Parent IsNot flpMenu Then Exit Sub 
    If vLoggedLogin Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByUserName.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.UserName) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.UserName) 
        pSearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.UserName, vLoggedLogin.UserName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByTimeLoggedIn.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.TimeLoggedInStart) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.TimeLoggedInStart) 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.TimeLoggedInEnd) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.TimeLoggedInEnd) 
        pSearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.TimeLoggedInStart, vLoggedLogin.TimeLoggedIn) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByApplicationName.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.ApplicationName) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.ApplicationName) 
        pSearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.ApplicationName, vLoggedLogin.ApplicationName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByLoginFaultNumber.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.LoginFaultNumberFrom) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.LoginFaultNumberFrom) 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.LoginFaultNumberTo) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.LoginFaultNumberTo) 
        pSearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.LoginFaultNumberFrom, vLoggedLogin.LoginFaultNumber) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByOriginatingCountry.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.OriginatingCountry) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.OriginatingCountry) 
        pSearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.OriginatingCountry, vLoggedLogin.OriginatingCountry) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByDateLoggedIn.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.DateLoggedInStart) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.DateLoggedInStart) 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.DateLoggedInEnd) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.DateLoggedInEnd) 
        pSearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.DateLoggedInStart, vLoggedLogin.DateLoggedIn) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByMonthLoggedIn.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.MonthLoggedInStart) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.MonthLoggedInStart) 
        If pSearchFilters.ContainsKey(csLoggedLoginCol.enmFillOnTheFlyParameters.MonthLoggedInEnd) Then pSearchFilters.Remove(csLoggedLoginCol.enmFillOnTheFlyParameters.MonthLoggedInEnd) 
        pSearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.MonthLoggedInStart, vLoggedLogin.MonthLoggedIn) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreLoggedLoginCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vLoggedLogin.ID, vLoggedLogin.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _LoggedLoginID = vLoggedLogin.ID 
      'MyIntelliCombo.ValueSelect(_LoggedLoginID) 
      pFault = ActivateControl("ctlc_LoggedLogin") 
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
      pFault = _LoggedLoginCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _LoggedLoginCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedLoginCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedLoginCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_LoggedLoginCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csLoggedLogin.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedLogin" 
      pFault = _ctlLoggedLoginCol.LoadControl(_LoggedLoginCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlLoggedLoginCol_evtUnChosen() Handles _ctlLoggedLoginCol.evtUnChosen 
 
    _LoggedLoginID = 0 
    _LoggedLogin = Nothing 
    _LoggedAlertCol = Nothing 
    lnkLoggedAlertCol.Visible = False 
    _LoggedRequestCol = Nothing 
    lnkLoggedRequestCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkLoggedLogin.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkLoggedAlertCol.Click, 
      lnkLoggedRequestCol.Click, 
      lnkLoggedLoginCol.Click, 
      lnkLoggedLogin.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkLoggedLogin OrElse (lnk Is lnkLoggedLoginCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlLoggedLoginCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_LoggedLoginCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csLoggedLogin.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csLoggedLoginCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillLoggedLoginCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _LoggedLoginCol = New csLoggedLoginCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _LoggedLoginCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlLoggedLoginCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlLoggedLoginCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _LoggedLoginCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlLoggedLoginCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _LoggedLoginCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _LoggedLoginCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedLoginCol.Count) 
      End If 
    Else 
      _LoggedLoginCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedLoginCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlLoggedLoginCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedLogin" 
    
    Dim pLoggedLoginID As Long = _LoggedLoginID 
    
    pFault = _ctlLoggedLoginCol.LoadControl(_LoggedLoginCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedLoginCol.Visible = True 
    
    _ctlLoggedLoginCol.Refresh() 
    If pLoggedLoginID <> 0 Then 
      Dim pLoggedLoginCol As csLoggedLoginCol = CType(_ctlLoggedLoginCol.bsCtlLoggedLogin.DataSource, csLoggedLoginCol) 
      Dim pLoggedLogin As csLoggedLogin = pLoggedLoginCol.FindByID(pLoggedLoginID) 
      If pLoggedLogin.ID > 0 Then 
        _ctlLoggedLoginCol.bsCtlLoggedLogin.CurrencyManager.Position = pLoggedLoginCol.IndexOf(pLoggedLogin) 
        _ctlLoggedLoginCol.dgvLoggedLogin.Rows(pLoggedLoginCol.IndexOf(pLoggedLogin)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlLoggedLogin() As clsFault 
    Dim pFault As New clsFault 
    
    If _LoggedLoginID > 0 Then 
      ChooseLoggedLogin() 
      _LoggedLogin = New csLoggedLogin() 
      pFault = _LoggedLogin.GetByID(_LoggedLoginID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _LoggedLogin = New csLoggedLogin() 
    End If 
    'lblSecondaryTitle.Text = _LoggedLogin.ID.ToString("#,##0")    
     
    Dim pLoadParameters As New ctlc_LoggedLogin.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = True 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedLogin(pLoadParameters)
    pFault = _ctlLoggedLogin.LoadControl(_LoggedLogin, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlLoggedLogin.Visible = True 
    Return pFault 
  End Function 
  Private Function RefreshCtlLoggedAlertCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlLoggedAlertCol.dgvLoggedAlert.SelectedRows.Count > 0 Then 
      Dim pLoggedAlert As csLoggedAlert = CType(_ctlLoggedAlertCol.bsCtlLoggedAlert.Current, csLoggedAlert) 
      pID = pLoggedAlert.ID 
    End If 
 
    Dim pTestCol As csLoggedAlertCol = Nothing 
    RaiseEvent evtOverrideFillLoggedAlertCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _LoggedAlertCol = New csLoggedAlertCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedAlertCol.FillByLoggedLoginID(_LoggedLoginID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _LoggedAlertCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedAlertCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedAlertCol.Count) 
      End If 
    Else 
      _LoggedAlertCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedAlertCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_LoggedAlertCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _LoggedLogin IsNot Nothing AndAlso Not String.IsNullOrEmpty(_LoggedLogin.DefaultDesignation) Then 
        .ReportTitle = "List of LoggedAlerts for " & _LoggedLogin.DefaultDesignation 
      Else 
        .ReportTitle = "List of LoggedAlerts for LoggedLogin" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csLoggedAlert.enmProperty.LoggedLogin) 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedAlertCol(pLoadParameters)
    
    pFault = _ctlLoggedAlertCol.LoadControl(_LoggedAlertCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedAlertCol.Visible = True 
 
    If pID > 0 Then 
      Dim pLoggedAlerts As csLoggedAlertCol = CType(_ctlLoggedAlertCol.bsCtlLoggedAlert.DataSource, csLoggedAlertCol) 
      Dim pLoggedAlert As csLoggedAlert = pLoggedAlerts.FindByID((pID)) 
      If pLoggedAlert.ID > 0 Then 
        _ctlLoggedAlertCol.bsCtlLoggedAlert.CurrencyManager.Position = pLoggedAlerts.IndexOf(pLoggedAlert) 
        _ctlLoggedAlertCol.dgvLoggedAlert.Rows(pLoggedAlerts.IndexOf(pLoggedAlert)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlLoggedRequestCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlLoggedRequestCol.dgvLoggedRequest.SelectedRows.Count > 0 Then 
      Dim pLoggedRequest As csLoggedRequest = CType(_ctlLoggedRequestCol.bsCtlLoggedRequest.Current, csLoggedRequest) 
      pID = pLoggedRequest.ID 
    End If 
 
    Dim pTestCol As csLoggedRequestCol = Nothing 
    RaiseEvent evtOverrideFillLoggedRequestCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _LoggedRequestCol = New csLoggedRequestCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedRequestCol.FillByLoggedLoginID(_LoggedLoginID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _LoggedRequestCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedRequestCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedRequestCol.Count) 
      End If 
    Else 
      _LoggedRequestCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedRequestCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_LoggedRequestCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _LoggedLogin IsNot Nothing AndAlso Not String.IsNullOrEmpty(_LoggedLogin.DefaultDesignation) Then 
        .ReportTitle = "List of LoggedRequests for " & _LoggedLogin.DefaultDesignation 
      Else 
        .ReportTitle = "List of LoggedRequests for LoggedLogin" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csLoggedRequest.enmProperty.LoggedLogin) 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedRequestCol(pLoadParameters)
    
    pFault = _ctlLoggedRequestCol.LoadControl(_LoggedRequestCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedRequestCol.Visible = True 
 
    If pID > 0 Then 
      Dim pLoggedRequests As csLoggedRequestCol = CType(_ctlLoggedRequestCol.bsCtlLoggedRequest.DataSource, csLoggedRequestCol) 
      Dim pLoggedRequest As csLoggedRequest = pLoggedRequests.FindByID((pID)) 
      If pLoggedRequest.ID > 0 Then 
        _ctlLoggedRequestCol.bsCtlLoggedRequest.CurrencyManager.Position = pLoggedRequests.IndexOf(pLoggedRequest) 
        _ctlLoggedRequestCol.dgvLoggedRequest.Rows(pLoggedRequests.IndexOf(pLoggedRequest)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Function LoadCboLoggedLogins(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_LoggedLoginDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboLoggedLogin(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _LoggedLoginID >= 0 Then 
      MyIntelliCombo.ValueSelect(_LoggedLoginID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _LoggedLoginID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _LoggedLoginID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetLoggedLoginIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _LoggedLoginID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _LoggedLoginID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _LoggedLoginID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _LoggedLoginID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseLoggedLogin() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_LoggedLogin", StringComparison.OrdinalIgnoreCase) AndAlso _LoggedLoginID > 0 Then 
        'to avoid getting ObjectNotFound 
        _LoggedLogin = New csLoggedLogin() 
        pFault = _LoggedLogin.GetByID(_LoggedLoginID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_LoggedLogin") 
    End If 
    pnlLoggedLogin.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  Private Sub _ctlLoggedAlertCol_evtRowDoubleClicked(ByVal vLoggedAlert As csLoggedAlert, ByRef rHandled As Boolean) Handles _ctlLoggedAlertCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtLoggedAlertChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtLoggedAlertChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vLoggedAlert.ID 
      .Object = New csLoggedAlert 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlLoggedRequestCol_evtRowDoubleClicked(ByVal vLoggedRequest As csLoggedRequest, ByRef rHandled As Boolean) Handles _ctlLoggedRequestCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtLoggedRequestChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtLoggedRequestChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vLoggedRequest.ID 
      .Object = New csLoggedRequest 
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
    pnlLoggedLogin.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkLoggedLoginCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _LoggedLoginID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_LoggedLoginCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkLoggedLoginCol.Visible = False 
      _ActiveControl = _ctlLoggedLogin 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboLoggedLogins(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _LoggedLoginID <> 0 Then 
        MyIntelliCombo.cbo.Text = _LoggedLoginID.ToString() 
        pFault = ActivateControl("ctlc_LoggedLogin") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlLoggedLogin.Visible = False 
        _LoggedLoginID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _LoggedLoginID > 0 Then pnlLoggedLogin.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkLoggedAlertCol.MouseEnter, 
                  lnkLoggedRequestCol.MouseEnter, 
                  lnkLoggedLoginCol.MouseEnter, 
                  lnkLoggedLogin.MouseEnter, 
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
                  lnkLoggedAlertCol.MouseLeave, 
                  lnkLoggedRequestCol.MouseLeave, 
                  lnkLoggedLoginCol.MouseLeave, 
                  lnkLoggedLogin.MouseLeave, 
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
    Dim pUserName As String = Nothing 
    Dim pUserNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pTimeLoggedInStart As Nullable(Of Date) = Nothing 
    Dim pTimeLoggedInEnd As Nullable(Of Date) = Nothing 
    Dim pApplicationName As String = Nothing 
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pLoginFaultNumberFrom As Nullable(Of Integer) = Nothing 
    Dim pLoginFaultNumberTo As Nullable(Of Integer) = Nothing 
    Dim pOriginatingCountry As String = Nothing 
    Dim pOriginatingCountryWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pDateLoggedInStart As Nullable(Of Date) = Nothing 
    Dim pDateLoggedInEnd As Nullable(Of Date) = Nothing 
    Dim pMonthLoggedInStart As Nullable(Of Date) = Nothing 
    Dim pMonthLoggedInEnd As Nullable(Of Date) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByUserName As Boolean = False 
    Dim pGroupByTimeLoggedIn As Boolean = False 
    Dim pGroupByApplicationName As Boolean = False 
    Dim pGroupByLoginFaultNumber As Boolean = False 
    Dim pGroupByOriginatingCountry As Boolean = False 
    Dim pGroupByDateLoggedIn As Boolean = False 
    Dim pGroupByMonthLoggedIn As Boolean = False 
    
    Dim pSumUserIdentityTypeNameCode As Boolean = False 
    Dim pSumLoginFaultNumber As Boolean = False 
    Dim pSumTotalPhysicalMemoryKb As Boolean = False 
    Dim pSumAvailablePhysicalMemoryKb As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Logged Logins"  
  
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
        .String01Label.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.UserName), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.UserName), "User Name") 
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
 
        .Date01Label.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.TimeLoggedIn), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.TimeLoggedIn), "Time Logged In") 
        .Date01From.TabIndex = 5 
        .Date01To.TabIndex = 6 
        .Date01From.ShowCheckBox = True 
        .Date01To.ShowCheckBox = True 
        .Date01From.Checked = False 
        .Date01To.Checked = False 
        If _ctlLoggedLoginCol.LoadParameters.ColumnsFormat.ContainsKey(csLoggedLogin.enmProperty.TimeLoggedIn) Then 
          .Date01From.CustomFormat = _ctlLoggedLoginCol.LoadParameters.ColumnsFormat(csLoggedLogin.enmProperty.TimeLoggedIn) 
          .Date01To.CustomFormat = _ctlLoggedLoginCol.LoadParameters.ColumnsFormat(csLoggedLogin.enmProperty.TimeLoggedIn) 
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
 
        .String02Label.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.ApplicationName), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.ApplicationName), "Application Name") 
        .String02Text.Text = "" 
        .String02Text.TabIndex = 7 
        With .String02WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 8 
        End With 
        .flpFilter.Controls.Add(.String02Label) 
        .flpFilter.Controls.Add(.String02Text) 
        .flpFilter.Controls.Add(.String02LblWCType) 
        .flpFilter.Controls.Add(.String02WCType) 
 
        .Text01Label.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.LoginFaultNumber), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.LoginFaultNumber), "Login Fault Number") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 9 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 10 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .String03Label.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.OriginatingCountry), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.OriginatingCountry), "Originating Country") 
        .String03Text.Text = "" 
        .String03Text.TabIndex = 11 
        With .String03WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 12 
        End With 
        .flpFilter.Controls.Add(.String03Label) 
        .flpFilter.Controls.Add(.String03Text) 
        .flpFilter.Controls.Add(.String03LblWCType) 
        .flpFilter.Controls.Add(.String03WCType) 
 
        .Date02Label.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.DateLoggedIn), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.DateLoggedIn), "Date Logged In") 
        .Date02From.TabIndex = 13 
        .Date02To.TabIndex = 14 
        .Date02From.ShowCheckBox = True 
        .Date02To.ShowCheckBox = True 
        .Date02From.Checked = False 
        .Date02To.Checked = False 
        If _ctlLoggedLoginCol.LoadParameters.ColumnsFormat.ContainsKey(csLoggedLogin.enmProperty.DateLoggedIn) Then 
          .Date02From.CustomFormat = _ctlLoggedLoginCol.LoadParameters.ColumnsFormat(csLoggedLogin.enmProperty.DateLoggedIn) 
          .Date02To.CustomFormat = _ctlLoggedLoginCol.LoadParameters.ColumnsFormat(csLoggedLogin.enmProperty.DateLoggedIn) 
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
 
        .Date03Label.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.MonthLoggedIn), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.MonthLoggedIn), "Month Logged In") 
        .Date03From.TabIndex = 15 
        .Date03To.TabIndex = 16 
        .Date03From.ShowCheckBox = True 
        .Date03To.ShowCheckBox = True 
        .Date03From.Checked = False 
        .Date03To.Checked = False 
        If _ctlLoggedLoginCol.LoadParameters.ColumnsFormat.ContainsKey(csLoggedLogin.enmProperty.MonthLoggedIn) Then 
          .Date03From.CustomFormat = _ctlLoggedLoginCol.LoadParameters.ColumnsFormat(csLoggedLogin.enmProperty.MonthLoggedIn) 
          .Date03To.CustomFormat = _ctlLoggedLoginCol.LoadParameters.ColumnsFormat(csLoggedLogin.enmProperty.MonthLoggedIn) 
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
 
        .Text02Label.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.ID), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 17 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 18 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.UserName), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.UserName), "User Name") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 19 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.TimeLoggedIn), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.TimeLoggedIn), "Time Logged In") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 20 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.ApplicationName), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.ApplicationName), "Application Name") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 21 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .lblGroupBy04.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.LoginFaultNumber), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.LoginFaultNumber), "Login Fault Number") 
        .chkGroupBy04.Checked = False 
        .chkGroupBy04.TabIndex = 22 
        .flpGroupBy.Controls.Add(.lblGroupBy04) 
        .flpGroupBy.Controls.Add(.chkGroupBy04) 
 
        .lblGroupBy05.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.OriginatingCountry), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.OriginatingCountry), "Originating Country") 
        .chkGroupBy05.Checked = False 
        .chkGroupBy05.TabIndex = 23 
        .flpGroupBy.Controls.Add(.lblGroupBy05) 
        .flpGroupBy.Controls.Add(.chkGroupBy05) 
 
        .lblGroupBy06.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.DateLoggedIn), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.DateLoggedIn), "Date Logged In") 
        .chkGroupBy06.Checked = False 
        .chkGroupBy06.TabIndex = 24 
        .flpGroupBy.Controls.Add(.lblGroupBy06) 
        .flpGroupBy.Controls.Add(.chkGroupBy06) 
 
        .lblGroupBy07.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.MonthLoggedIn), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.MonthLoggedIn), "Month Logged In") 
        .chkGroupBy07.Checked = False 
        .chkGroupBy07.TabIndex = 25 
        .flpGroupBy.Controls.Add(.lblGroupBy07) 
        .flpGroupBy.Controls.Add(.chkGroupBy07) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.UserIdentityTypeName), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.UserIdentityTypeName), "User Identity Type Name") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 26 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.LoginFaultNumber), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.LoginFaultNumber), "Login Fault Number") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 27 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
        .lblSumField03.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.TotalPhysicalMemoryKb), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.TotalPhysicalMemoryKb), "Total Physical Memory Kb") 
        .chkSumField03.Checked = False 
        .chkSumField03.TabIndex = 28 
        .flpSumColumns.Controls.Add(.lblSumField03) 
        .flpSumColumns.Controls.Add(.chkSumField03) 
 
        .lblSumField04.Text = If(_ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedLogin.enmProperty.AvailablePhysicalMemoryKb), _ctlLoggedLoginCol.LoadParameters.ColumnsHeaderText(csLoggedLogin.enmProperty.AvailablePhysicalMemoryKb), "Available Physical Memory Kb") 
        .chkSumField04.Checked = False 
        .chkSumField04.TabIndex = 29 
        .flpSumColumns.Controls.Add(.lblSumField04) 
        .flpSumColumns.Controls.Add(.chkSumField04) 
 
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
        pUserName = .String01Text.Text 
        pUserNameWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.UserName, pUserName) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.UserNameWildcardType, pUserNameWildcardType) 
      End If 
      If .Date01From.Checked OrElse .Date01To.Checked Then 
        pTimeLoggedInStart = .Date01From.Value 
        pTimeLoggedInEnd = .Date01To.Value 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.TimeLoggedInStart, pTimeLoggedInStart) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.TimeLoggedInEnd, pTimeLoggedInEnd) 
      End If 
      If .String02Text.Text <> "" Then 
        pApplicationName = .String02Text.Text 
        pApplicationNameWildcardType = CType(CType(.String02WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.ApplicationName, pApplicationName) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.ApplicationNameWildcardType, pApplicationNameWildcardType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pLoginFaultNumberFrom = ccHelper.ToInteger(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pLoginFaultNumberTo = ccHelper.ToInteger(.Text01To.Text) 
          Else 
            pLoginFaultNumberTo = pLoginFaultNumberFrom 
          End If 
          _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.LoginFaultNumberFrom, pLoginFaultNumberFrom) 
          _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.LoginFaultNumberTo, pLoginFaultNumberTo) 
        End If 
      End If 
      If .String03Text.Text <> "" Then 
        pOriginatingCountry = .String03Text.Text 
        pOriginatingCountryWildcardType = CType(CType(.String03WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.OriginatingCountry, pOriginatingCountry) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.OriginatingCountryWildcardType, pOriginatingCountryWildcardType) 
      End If 
      If .Date02From.Checked OrElse .Date02To.Checked Then 
        pDateLoggedInStart = .Date02From.Value 
        pDateLoggedInEnd = .Date02To.Value 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.DateLoggedInStart, pDateLoggedInStart) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.DateLoggedInEnd, pDateLoggedInEnd) 
      End If 
      If .Date03From.Checked OrElse .Date03To.Checked Then 
        pMonthLoggedInStart = .Date03From.Value 
        pMonthLoggedInEnd = .Date03To.Value 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.MonthLoggedInStart, pMonthLoggedInStart) 
        _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.MonthLoggedInEnd, pMonthLoggedInEnd) 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csLoggedLoginCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csLoggedLoginCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csLoggedLoginCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByUserName = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByUserName, pGroupByUserName) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByTimeLoggedIn = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByTimeLoggedIn, pGroupByTimeLoggedIn) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByApplicationName = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByApplicationName, pGroupByApplicationName) 
      End If 
      If .chkGroupBy04.Checked = True Then 
        pGroupByLoginFaultNumber = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByLoginFaultNumber, pGroupByLoginFaultNumber) 
      End If 
      If .chkGroupBy05.Checked = True Then 
        pGroupByOriginatingCountry = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByOriginatingCountry, pGroupByOriginatingCountry) 
      End If 
      If .chkGroupBy06.Checked = True Then 
        pGroupByDateLoggedIn = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByDateLoggedIn, pGroupByDateLoggedIn) 
      End If 
      If .chkGroupBy07.Checked = True Then 
        pGroupByMonthLoggedIn = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedLoginCol.enmFillSumOnTheFlyParameters.GroupByMonthLoggedIn, pGroupByMonthLoggedIn) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumUserIdentityTypeNameCode = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumLoginFaultNumber = True 
        pDoSum = True 
      End If 
      
      If .chkSumField03.Checked = True Then 
        pSumTotalPhysicalMemoryKb = True 
        pDoSum = True 
      End If 
      
      If .chkSumField04.Checked = True Then 
        pSumAvailablePhysicalMemoryKb = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csLoggedLoginCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csLoggedLoginCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csLoggedLoginCol.enmListDefinition.Dir) Then _SearchFilters.Add(csLoggedLoginCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_LoggedLoginCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_LoggedLoginCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csLoggedLogin.enmProperty.ID, "ID") 
      End With 
      _LoggedLoginCol = New csLoggedLoginCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _LoggedLoginCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _LoggedLoginCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _LoggedLoginCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedLoginCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedLoginCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedLogin" 
      RaiseEvent evtOverrideLoadCtlLoggedLoginCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _LoggedLoginCol = New csLoggedLoginCol() 
      pFault = _LoggedLoginCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_LoggedLoginCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _LoggedLoginCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csLoggedLogin.enmProperty.ID, "Count") 
        If pGroupByUserName = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.UserName) 
        If pGroupByTimeLoggedIn = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.TimeLoggedIn) 
        If pGroupByApplicationName = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.ApplicationName) 
        If pGroupByLoginFaultNumber = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.LoginFaultNumber) 
        If pGroupByOriginatingCountry = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.OriginatingCountry) 
        If pGroupByDateLoggedIn = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.DateLoggedIn) 
        If pGroupByMonthLoggedIn = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.MonthLoggedIn) 
        If pSumLoginFaultNumber = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.LoginFaultNumber) 
        If pSumTotalPhysicalMemoryKb = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.TotalPhysicalMemoryKb) 
        If pSumAvailablePhysicalMemoryKb = False Then .ColumnsHide.Add(csLoggedLogin.enmProperty.AvailablePhysicalMemoryKb) 
        If pGroupByLoginFaultNumber = True OrElse pSumLoginFaultNumber = True Then If .ColumnsHide.Contains(csLoggedLogin.enmProperty.LoginFaultNumber) Then .ColumnsHide.Remove(csLoggedLogin.enmProperty.LoginFaultNumber) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.UserFullName) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.UserIdentityType) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.UserIdentityTypeName) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.Roles) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.TimeLoggedOut) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.EnvironmentUserName) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.EnvironmentMachineName) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.EnvironmentUserDomainName) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.DnsGetHostName) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.AddressList) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.ComputerMACAddress) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.SystemDiskVolumeSerialNo) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.LocalTime) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.GmtTime) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.AccessingComputerDetails) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.UICulture) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.ApplicationVersion) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.OriginatingIP) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.Language) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.HostingAssembly) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.ClientReportedIP) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.ClientReportedCountry) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.IPAdditionalDetails) 
        .ColumnsHide.Add(csLoggedLogin.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlLoggedLoginCol.Visible = True 
    pFault = _ctlLoggedLoginCol.LoadControl(_LoggedLoginCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csLoggedLoginCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csLoggedLoginCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlLoggedLogin.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlLoggedLogin.Controls(0).Name) 
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
 
  Private Sub _ctlLoggedLoginCol_evtTimerTripped() Handles _ctlLoggedLoginCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtLoggedLoginTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlLoggedLoginCol.LoggedLoginCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlLoggedLoginCol.LoggedLoginCol(0).ID 
 
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
    If _LoggedLoginCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csLoggedLogin() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csLoggedLoginCol = CType(CallByName(_LoggedLoginCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedLoginCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csLoggedLoginCol = CType(CallByName(_LoggedLoginCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedLoginCol) 
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
                  ccHelper.GetPropertyTypeName(New csLoggedLoginCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csLoggedLoginCol = CType(CallByName(_LoggedLoginCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedLoginCol) 
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
 
  Private Sub cc_ctlPnlLoggedLogin_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
  Private Sub ctlPnlc_LoggedLogin_ccevtOverrideLoadCtlLoggedLoginCol(ByRef rLoadParameters As ctlc_LoggedLoginCol.clsLoadParameters) Handles Me.evtOverrideLoadCtlLoggedLoginCol 
    rLoadParameters.TruncateStrings = False 
 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.UserFullName) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.UserIdentityType) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.UserIdentityTypeName) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.Roles) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.EnvironmentUserName) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.EnvironmentMachineName) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.EnvironmentUserDomainName) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.DnsGetHostName) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.AddressList) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.ComputerMACAddress) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.SystemDiskVolumeSerialNo) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.GmtTime) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.AccessingComputerDetails) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.UICulture) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.TotalPhysicalMemoryKb) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.HostingAssembly) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.DateLoggedIn) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.MonthLoggedIn) 
    rLoadParameters.ColumnsHide.Add(csLoggedLogin.enmProperty.IPAdditionalDetails) 
 
  End Sub 
End Class 
