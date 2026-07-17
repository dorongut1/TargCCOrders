Public Class ctlPnlc_UserStatus 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlUserStatusCol As ctlc_UserStatusCol 
  Private WithEvents _ctlUserStatus As ctlc_UserStatus 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _UserStatusID As Long 
 
  'The data holders 
  Private _UserStatusCol As csUserStatusCol 
  Private _UserStatus As csUserStatus 
 
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
  Public Event evtOverrideLoadCboUserStatus(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetUserStatusIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillUserStatusCol(ByRef rUserStatusCol As csUserStatusCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlUserStatusCol(ByRef rLoadParameters As ctlc_UserStatusCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlUserStatus(ByRef rLoadParameters As ctlc_UserStatus.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreUserStatusCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtUserStatusTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  'Parents
  Private _CancelEvtUserChosen As Boolean = False 
  Private _ShowPopForEvtUserChosen As Boolean = False 
  
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
 
    lnkUserStatusCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkUserStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vUserStatusID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _UserStatusID = CType(vUserStatusID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlUserStatus.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkUserStatusCol.Visible = False 
    _ShowIntelligentCombo = True 
    chkGrid.Checked = False 
 
    'Since there is no default text field  
    'Dim pIntelliComboMakeDumb As Boolean = False 
    'Dim pIntelliComboDropDownStyle As System.Windows.Forms.ComboBoxStyle = ComboBoxStyle.DropDown 
    Dim pIntelliComboMakeDumb As Boolean = True 
    Dim pIntelliComboDropDownStyle As System.Windows.Forms.ComboBoxStyle = ComboBoxStyle.Simple 
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
      pFault = LoadCboUserStatuss(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _UserStatusID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_UserStatusID) 
      End If 
      ChooseUserStatus() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_UserStatus") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _UserStatusID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _UserStatusID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_UserStatus" OrElse pControlName = "ctlUserStatus" Then 
      lnkUserStatus.ForeColor = Color.Black : lnkUserStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserStatus.BackColor = Color.Wheat 
      If _ctlUserStatus Is Nothing Then 
        _ctlUserStatus = New ctlc_UserStatus() 
        _ctlUserStatus.Dock = DockStyle.Fill 
        _ctlUserStatus.Controls.RemoveByKey("btnAdd") 
        pnlUserStatus.Controls.Add(_ctlUserStatus) 
        _ctlUserStatus.Visible = False 
      End If 
      If _UserStatusID = 0 Then 
        pnlUserStatus.Visible = False 
      End If 
      'If _UserStatus Is Nothing Then 
      pFault = RefreshCtlUserStatus() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlUserStatus.UserStatus.IsEmpty AndAlso _UserStatusID <> -2 Then 
        pnlUserStatus.Visible = False 
      End If 
      _ctlUserStatus.Name = "ctlc_UserStatus" 
      _ActiveControl = _ctlUserStatus 
      _ctlUserStatus.BringToFront() 
      _ctlUserStatus.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_UserStatusCol" Then 
      lnkUserStatusCol.ForeColor = Color.Black : lnkUserStatusCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserStatusCol.BackColor = Color.Wheat 
      If _ctlUserStatusCol Is Nothing Then 
        _ctlUserStatusCol = New ctlc_UserStatusCol() 
        _ctlUserStatusCol.Dock = DockStyle.Fill 
        pnlUserStatus.Controls.Add(_ctlUserStatusCol) 
        _ctlUserStatusCol.Visible = False 
      End If  
      pnlUserStatus.Visible = True 
      If _UserStatusCol Is Nothing Then 
        pFault = RefreshCtlUserStatusCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlUserStatusCol.Name = "ctlc_UserStatusCol" 
      _ActiveControl = _ctlUserStatusCol 
      _ctlUserStatusCol.BringToFront() 
      _ctlUserStatusCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-UserStatus-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("UserStatus", _Requester) 
 
    lnkUserStatusCol.Text = CCTextTranslate("List", _Requester) 
    lnkUserStatus.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlUserStatus.Controls(0) Is _ctlUserStatus Then 
      If _UserStatusID = 0 Then 
        pnlUserStatus.Visible = False 
      End If 
    ElseIf pnlUserStatus.Controls(0) Is _ctlUserStatusCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pUserStatusID As Long = _UserStatusID 
      If ccHelper.IsNumeric(pText) Then _UserStatusID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetUserStatusIDFromIntelliComboText(pText) 
      If pUserStatusID <> _UserStatusID Then 
        _UserStatus = Nothing 
        pFault = ActivateControl("ctlc_UserStatus") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlUserStatus.Controls(0) Is _ctlUserStatus Then 
      pFault = RefreshCtlUserStatus() 
    ElseIf pnlUserStatus.Controls(0) Is _ctlUserStatusCol Then 
      pFault = RefreshCtlUserStatusCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlUserStatus.Controls(0).Name, "", "TRGT-UserStatus-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub _ctlUserStatusCol_evtRowClicked(ByVal vUserStatus As Object) Handles _ctlUserStatusCol.evtRowClicked 
    
    If vUserStatus Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pUserStatus As csUserStatus = CType(vUserStatus, csUserStatus) 
    _UserStatusID = pUserStatus.ID 
    
    If _ActiveControl Is _ctlUserStatusCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByUserID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByApplicationName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByLoginTime.ToString() Then 
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
    
    ChooseUserStatus() 
    
    Try 
      MyIntelliCombo.ValueSelect(_UserStatusID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pUserStatus.ID.ToString("#,##0")

 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseUserStatus() 
    _UserStatus = Nothing 
    lnkUserStatus.Visible = True 
  End Sub 
  Private Sub _ctlUserStatusCol_evtRowDoubleClicked(ByVal vUserStatus As csUserStatus, ByRef rHandled As Boolean) Handles _ctlUserStatusCol.evtRowDoubleClicked 
    If lnkUserStatus.Parent IsNot flpMenu Then Exit Sub 
    If vUserStatus Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByUserID.ToString() Then 
        If pSearchFilters.ContainsKey(csUserStatusCol.enmFillOnTheFlyParameters.UserID) Then pSearchFilters.Remove(csUserStatusCol.enmFillOnTheFlyParameters.UserID) 
        pSearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.UserID, vUserStatus.UserID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByApplicationName.ToString() Then 
        If pSearchFilters.ContainsKey(csUserStatusCol.enmFillOnTheFlyParameters.ApplicationName) Then pSearchFilters.Remove(csUserStatusCol.enmFillOnTheFlyParameters.ApplicationName) 
        pSearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.ApplicationName, vUserStatus.ApplicationName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByLoginTime.ToString() Then 
        If pSearchFilters.ContainsKey(csUserStatusCol.enmFillOnTheFlyParameters.LoginTimeStart) Then pSearchFilters.Remove(csUserStatusCol.enmFillOnTheFlyParameters.LoginTimeStart) 
        If pSearchFilters.ContainsKey(csUserStatusCol.enmFillOnTheFlyParameters.LoginTimeEnd) Then pSearchFilters.Remove(csUserStatusCol.enmFillOnTheFlyParameters.LoginTimeEnd) 
        pSearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.LoginTimeStart, vUserStatus.LoginTime) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreUserStatusCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vUserStatus.ID, vUserStatus.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _UserStatusID = vUserStatus.ID 
      'MyIntelliCombo.ValueSelect(_UserStatusID) 
      pFault = ActivateControl("ctlc_UserStatus") 
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
      pFault = _UserStatusCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _UserStatusCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserStatusCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserStatusCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_UserStatusCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csUserStatus.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserStatus" 
      pFault = _ctlUserStatusCol.LoadControl(_UserStatusCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlUserStatusCol_evtUnChosen() Handles _ctlUserStatusCol.evtUnChosen 
 
    _UserStatusID = 0 
    _UserStatus = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkUserStatus.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkUserStatusCol.Click, 
      lnkUserStatus.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkUserStatus OrElse (lnk Is lnkUserStatusCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlUserStatusCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_UserStatusCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csUserStatus.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csUserStatusCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillUserStatusCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _UserStatusCol = New csUserStatusCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _UserStatusCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlUserStatusCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _UserStatusCol.FillByUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=100, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then  
              _ctlUserStatusCol.Timer?.Stop()  
              Return pFault  
            End If  
          Case Else 
            If _ctlUserStatusCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _UserStatusCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlUserStatusCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _UserStatusCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _UserStatusCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserStatusCol.Count) 
      End If 
    Else 
      _UserStatusCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _UserStatusCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlUserStatusCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserStatus" 
    
    Dim pUserStatusID As Long = _UserStatusID 
    
    pFault = _ctlUserStatusCol.LoadControl(_UserStatusCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlUserStatusCol.Visible = True 
    
    _ctlUserStatusCol.Refresh() 
    If pUserStatusID <> 0 Then 
      Dim pUserStatusCol As csUserStatusCol = CType(_ctlUserStatusCol.bsCtlUserStatus.DataSource, csUserStatusCol) 
      Dim pUserStatus As csUserStatus = pUserStatusCol.FindByID(pUserStatusID) 
      If pUserStatus.ID > 0 Then 
        _ctlUserStatusCol.bsCtlUserStatus.CurrencyManager.Position = pUserStatusCol.IndexOf(pUserStatus) 
        _ctlUserStatusCol.dgvUserStatus.Rows(pUserStatusCol.IndexOf(pUserStatus)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlUserStatus() As clsFault 
    Dim pFault As New clsFault 
    
    If _UserStatusID > 0 Then 
      ChooseUserStatus() 
      _UserStatus = New csUserStatus(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserStatus.GetByID(_UserStatusID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _UserStatus = New csUserStatus(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _UserStatus.ID.ToString("#,##0")
    
     
    Dim pLoadParameters As New ctlc_UserStatus.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlUserStatus(pLoadParameters)
    pFault = _ctlUserStatus.LoadControl(_UserStatus, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlUserStatus.Visible = True 
    If _UserStatusID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlUserStatus.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlUserStatus.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlUserStatus_evtDeleted(ByVal vUserStatusID As Long) Handles _ctlUserStatus.evtDeleted 
    _UserStatusCol = Nothing 
    Dim pFault As clsFault 
    _UserStatusID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboUserStatuss(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlUserStatus() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlUserStatus.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkUserStatusCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlUserStatus_evtCancelledEdit(ByVal vUserStatus As csUserStatus) Handles _ctlUserStatus.evtCancelledEdit 
    RefreshCtlUserStatus() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboUserStatuss(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlUserStatus.btnAdd.Visible = False 
      If _UserStatusID = 0 OrElse _UserStatusID = -2 Then 
        pnlUserStatus.Visible = False 
      Else 
        pnlUserStatus.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlUserStatus.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_UserStatusCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlUserStatus_evtUpdated(ByVal vWhichProperty As csUserStatus.enmUpdateType, ByVal vUserStatus As csUserStatus) Handles _ctlUserStatus.evtUpdated 
    _UserStatusCol = Nothing 
    Dim pFault As clsFault 
    _UserStatusID = CType(vUserStatus, csUserStatus).ID 
    If _ActiveControl.Name = "ctlc_UserStatus" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboUserStatuss(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlUserStatus() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlUserStatus.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboUserStatuss(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.UD 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboUserStatus(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
    If pComboList Is Nothing Then 
      pComboList = New clsComboList() 
      'Since there is no default text field  
      pFault = New clsFault 
      pFault.SetOK() 
      pPrompt = "Type an ID"
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
 
    If _UserStatusID >= 0 Then 
      MyIntelliCombo.ValueSelect(_UserStatusID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserStatusUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _UserStatusID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _UserStatusID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetUserStatusIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _UserStatusID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _UserStatusID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _UserStatusID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _UserStatusID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseUserStatus() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_UserStatus", StringComparison.OrdinalIgnoreCase) AndAlso _UserStatusID > 0 Then 
        'to avoid getting ObjectNotFound 
        _UserStatus = New csUserStatus(clsEnums.enmLoadParent.TextOnly) 
        pFault = _UserStatus.GetByID(_UserStatusID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_UserStatus") 
    End If 
    pnlUserStatus.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csUserStatus.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlUserStatus.evtParentChosen 
    If vParentName = csUserStatus.enmParentProperty.User Then 
      rHandled = True 
      If _CancelEvtUserChosen = True Then Exit Sub 
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
  End Sub 
   
  Private Sub chkGrid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrid.CheckedChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    
    Cursor = Cursors.WaitCursor 
    chkGrid.Enabled = False 
    pnlButtons.Visible = False 
    pnlUserStatus.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkUserStatusCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _UserStatusID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_UserStatusCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkUserStatusCol.Visible = False 
      _ActiveControl = _ctlUserStatus 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboUserStatuss(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _UserStatusID <> 0 Then 
        MyIntelliCombo.cbo.Text = _UserStatusID.ToString() 
        pFault = ActivateControl("ctlc_UserStatus") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlUserStatus.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlUserStatus.Visible = False 
        _UserStatusID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _UserStatusID > 0 Then pnlUserStatus.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkUserStatusCol.MouseEnter, 
                  lnkUserStatus.MouseEnter, 
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
                  lnkUserStatusCol.MouseLeave, 
                  lnkUserStatus.MouseLeave, 
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
  Private Sub _ctlUserStatus_evtAdd(ByVal vUserStatus As csUserStatus) Handles _ctlUserStatus.evtAdd 
    lnkUserStatusCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pUserID As Nullable(Of Long) = Nothing 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.c_User Then 
      pUserID = _Requester.UserIdentityInstanceID 
    End If 
    Dim pApplicationName As String = Nothing 
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pLastLoggedLoginIDFrom As Nullable(Of Long) = Nothing 
    Dim pLastLoggedLoginIDTo As Nullable(Of Long) = Nothing 
    Dim pLoginTimeStart As Nullable(Of Date) = Nothing 
    Dim pLoginTimeEnd As Nullable(Of Date) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByUserID As Boolean = False 
    Dim pGroupByApplicationName As Boolean = False 
    Dim pGroupByLoginTime As Boolean = False 
    
    Dim pSumLastLoggedLoginID As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the User Statuses"  
  
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
        If pUserID Is Nothing Then 
         .Combo01Label.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.User), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.User), "User") 
         Dim pUsers As New clsComboList 
         pFault = MyCache.GetComboList(clsEnums.enmComboListType.c_UserDefaultByID, pUsers) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
         'If pUsers IsNot Nothing AndAlso pUsers.Count > 0 Then 
         .flpFilter.Controls.Add(.Combo01Label) 
         .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
         'End If 
         With .Combo01 
           .MakeSmart() 
           If pUsers IsNot Nothing Then 
             .LoadControl(pUsers, GetChoose(_Requester)) 
           Else 
             .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.c_UserDefaultByID, 0, _Requester) 
           End If 
           .TabIndex = 3 
         End With 
        End If 
 
        .String01Label.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.ApplicationName), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.ApplicationName), "Application Name") 
        .String01Text.Text = "" 
        .String01Text.TabIndex = 4 
        With .String01WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 5 
        End With 
        .flpFilter.Controls.Add(.String01Label) 
        .flpFilter.Controls.Add(.String01Text) 
        .flpFilter.Controls.Add(.String01LblWCType) 
        .flpFilter.Controls.Add(.String01WCType) 
 
        .Text01Label.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.LastLoggedLoginID), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.LastLoggedLoginID), "Last Logged Login ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 6 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .Date01Label.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.LoginTime), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.LoginTime), "Login Time") 
        .Date01From.TabIndex = 8 
        .Date01To.TabIndex = 9 
        .Date01From.ShowCheckBox = True 
        .Date01To.ShowCheckBox = True 
        .Date01From.Checked = False 
        .Date01To.Checked = False 
        If _ctlUserStatusCol.LoadParameters.ColumnsFormat.ContainsKey(csUserStatus.enmProperty.LoginTime) Then 
          .Date01From.CustomFormat = _ctlUserStatusCol.LoadParameters.ColumnsFormat(csUserStatus.enmProperty.LoginTime) 
          .Date01To.CustomFormat = _ctlUserStatusCol.LoadParameters.ColumnsFormat(csUserStatus.enmProperty.LoginTime) 
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
 
        .Text02Label.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.ID), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 10 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 11 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.User), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.User), "User") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 12 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.ApplicationName), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.ApplicationName), "Application Name") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 13 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.LoginTime), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.LoginTime), "Login Time") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 14 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlUserStatusCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserStatus.enmProperty.LastLoggedLoginID), _ctlUserStatusCol.LoadParameters.ColumnsHeaderText(csUserStatus.enmProperty.LastLoggedLoginID), "Last Logged Login ID") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 15 
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
      If pUserID Is Nothing Then 
       If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
         pUserID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
         _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.UserID, pUserID) 
       End If 
      Else 
        _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.UserID, pUserID) 
      End If  
      If .String01Text.Text <> "" Then 
        pApplicationName = .String01Text.Text 
        pApplicationNameWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.ApplicationName, pApplicationName) 
        _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.ApplicationNameWildcardType, pApplicationNameWildcardType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pLastLoggedLoginIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pLastLoggedLoginIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pLastLoggedLoginIDTo = pLastLoggedLoginIDFrom 
          End If 
          _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.LastLoggedLoginIDFrom, pLastLoggedLoginIDFrom) 
          _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.LastLoggedLoginIDTo, pLastLoggedLoginIDTo) 
        End If 
      End If 
      If .Date01From.Checked OrElse .Date01To.Checked Then 
        pLoginTimeStart = .Date01From.Value 
        pLoginTimeEnd = .Date01To.Value 
        _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.LoginTimeStart, pLoginTimeStart) 
        _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.LoginTimeEnd, pLoginTimeEnd) 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csUserStatusCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csUserStatusCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csUserStatusCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByUserID = True 
        pDoSum = True 
        _SearchFilters.Add(csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByUserID, pGroupByUserID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByApplicationName = True 
        pDoSum = True 
        _SearchFilters.Add(csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByApplicationName, pGroupByApplicationName) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByLoginTime = True 
        pDoSum = True 
        _SearchFilters.Add(csUserStatusCol.enmFillSumOnTheFlyParameters.GroupByLoginTime, pGroupByLoginTime) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumLastLoggedLoginID = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csUserStatusCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csUserStatusCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csUserStatusCol.enmListDefinition.Dir) Then _SearchFilters.Add(csUserStatusCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_UserStatusCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_UserStatusCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csUserStatus.enmProperty.ID, "ID") 
      End With 
      _UserStatusCol = New csUserStatusCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _UserStatusCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _UserStatusCol.FillByUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
          Case Else 
            pFault = _UserStatusCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _UserStatusCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserStatusCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserStatusCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserStatus" 
      RaiseEvent evtOverrideLoadCtlUserStatusCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _UserStatusCol = New csUserStatusCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserStatusCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_UserStatusCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _UserStatusCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csUserStatus.enmProperty.ID, "Count") 
        If pGroupByUserID = False Then .ColumnsHide.Add(csUserStatus.enmProperty.User) 
        If pGroupByApplicationName = False Then .ColumnsHide.Add(csUserStatus.enmProperty.ApplicationName) 
        If pGroupByLoginTime = False Then .ColumnsHide.Add(csUserStatus.enmProperty.LoginTime) 
        If pSumLastLoggedLoginID = False Then .ColumnsHide.Add(csUserStatus.enmProperty.LastLoggedLoginID) 
        .ColumnsHide.Add(csUserStatus.enmProperty.LogoutTime) 
        .ColumnsHide.Add(csUserStatus.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlUserStatusCol.Visible = True 
    pFault = _ctlUserStatusCol.LoadControl(_UserStatusCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csUserStatusCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csUserStatusCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlUserStatus.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlUserStatus.Controls(0).Name) 
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
    _UserStatusID = -2 
    pFault = ActivateControl("ctlc_UserStatus") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlUserStatus() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlUserStatus.Visible = True 'new 
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
 
  Private Sub _ctlUserStatusCol_evtTimerTripped() Handles _ctlUserStatusCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtUserStatusTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlUserStatusCol.UserStatusCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlUserStatusCol.UserStatusCol(0).ID 
 
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
    If _UserStatusCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csUserStatus() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csUserStatusCol = CType(CallByName(_UserStatusCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserStatusCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csUserStatusCol = CType(CallByName(_UserStatusCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserStatusCol) 
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
                  ccHelper.GetPropertyTypeName(New csUserStatusCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csUserStatusCol = CType(CallByName(_UserStatusCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserStatusCol) 
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
 
  Private Sub cc_ctlPnlUserStatus_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
