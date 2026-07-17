Public Class ctlPnlc_Role 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlRoleCol As ctlc_RoleCol 
  Private WithEvents _ctlRole As ctlc_Role 
  Private WithEvents _ctlPermissionCol As ctlc_PermissionCol 
  Private WithEvents _ctlUserCol As ctlc_UserCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _RoleID As Long 
 
  'The data holders 
  Private _RoleCol As csRoleCol 
  Private _Role As csRole 
  Private _PermissionCol As csPermissionCol 
  Private _UserCol As csUserCol 
 
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
  Public Event evtOverrideLoadCboRole(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetRoleIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillRoleCol(ByRef rRoleCol As csRoleCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillPermissionCol(ByRef rPermissionCol As csPermissionCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillUserCol(ByRef rUserCol As csUserCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlRoleCol(ByRef rLoadParameters As ctlc_RoleCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlRole(ByRef rLoadParameters As ctlc_Role.clsLoadParameters) 
  Private Event evtOverrideLoadCtlPermissionCol(ByRef rLoadParameters As ctlc_PermissionCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlUserCol(ByRef rLoadParameters As ctlc_UserCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreRoleCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtRoleTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtPermissionChosen As Boolean = False 
  Private _ShowPopForEvtPermissionChosen As Boolean = False 
  Private _CancelEvtUserChosen As Boolean = False 
  Private _ShowPopForEvtUserChosen As Boolean = False 
  'Parents
  Private _CancelEvtBaseRoleChosen As Boolean = False 
  Private _ShowPopForEvtBaseRoleChosen As Boolean = False 
  
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
 
    lnkRoleCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkRole.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkPermissionCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkUserCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vRoleID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _RoleID = CType(vRoleID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlRole.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkRoleCol.Visible = False 
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
      pFault = LoadCboRoles(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _RoleID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_RoleID) 
      End If 
      ChooseRole() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_Role") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _RoleID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlc_Role" OrElse pControlName = "ctlRole" Then 
      lnkRole.ForeColor = Color.Black : lnkRole.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkRole.BackColor = Color.Wheat 
      If _ctlRole Is Nothing Then 
        _ctlRole = New ctlc_Role() 
        _ctlRole.Dock = DockStyle.Fill 
        _ctlRole.Controls.RemoveByKey("btnAdd") 
        pnlRole.Controls.Add(_ctlRole) 
        _ctlRole.Visible = False 
      End If 
      If _RoleID = 0 Then 
        pnlRole.Visible = False 
      End If 
      'If _Role Is Nothing Then 
      pFault = RefreshCtlRole() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlRole.Role.IsEmpty AndAlso _RoleID <> -2 Then 
        pnlRole.Visible = False 
      End If 
      _ctlRole.Name = "ctlc_Role" 
      _ActiveControl = _ctlRole 
      _ctlRole.BringToFront() 
      _ctlRole.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_RoleCol" Then 
      lnkRoleCol.ForeColor = Color.Black : lnkRoleCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkRoleCol.BackColor = Color.Wheat 
      If _ctlRoleCol Is Nothing Then 
        _ctlRoleCol = New ctlc_RoleCol() 
        _ctlRoleCol.Dock = DockStyle.Fill 
        pnlRole.Controls.Add(_ctlRoleCol) 
        _ctlRoleCol.Visible = False 
      End If  
      pnlRole.Visible = True 
      If _RoleCol Is Nothing Then 
        pFault = RefreshCtlRoleCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlRoleCol.Name = "ctlc_RoleCol" 
      _ActiveControl = _ctlRoleCol 
      _ctlRoleCol.BringToFront() 
      _ctlRoleCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlc_PermissionCol" Then 
      lnkPermissionCol.ForeColor = Color.Black : lnkPermissionCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkPermissionCol.BackColor = Color.Wheat 
      If _ctlPermissionCol Is Nothing Then 
      _ctlPermissionCol = New ctlc_PermissionCol() 
      _ctlPermissionCol.Dock = DockStyle.Fill 
      pnlRole.Controls.Add(_ctlPermissionCol) 
      _ctlPermissionCol.Visible = False 
      End If  
      If _PermissionCol Is Nothing Then 
        pFault = RefreshCtlPermissionCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlPermissionCol.Name = "ctlc_PermissionCol" 
      _ActiveControl = _ctlPermissionCol 
      _ctlPermissionCol.BringToFront() 
      _ctlPermissionCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_UserCol" Then 
      lnkUserCol.ForeColor = Color.Black : lnkUserCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserCol.BackColor = Color.Wheat 
      If _ctlUserCol Is Nothing Then 
      _ctlUserCol = New ctlc_UserCol() 
      _ctlUserCol.Dock = DockStyle.Fill 
      pnlRole.Controls.Add(_ctlUserCol) 
      _ctlUserCol.Visible = False 
      End If  
      If _UserCol Is Nothing Then 
        pFault = RefreshCtlUserCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlUserCol.Name = "ctlc_UserCol" 
      _ActiveControl = _ctlUserCol 
      _ctlUserCol.BringToFront() 
      _ctlUserCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-Role-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("Role", _Requester) 
 
    lnkRoleCol.Text = CCTextTranslate("List", _Requester) 
    lnkRole.Text = CCTextTranslate("Details", _Requester) 
 
    lnkPermissionCol.Text = TableNameTranslate("Permission", _Requester, vMakePlural:=True) 
    lnkUserCol.Text = TableNameTranslate("User", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlRole.Controls(0) Is _ctlRole Then 
      If _RoleID = 0 Then 
        pnlRole.Visible = False 
      End If 
    ElseIf pnlRole.Controls(0) Is _ctlRoleCol Then 
    ElseIf pnlRole.Controls(0) Is _ctlPermissionCol Then 
    ElseIf pnlRole.Controls(0) Is _ctlUserCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pRoleID As Long = _RoleID 
      If ccHelper.IsNumeric(pText) Then _RoleID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetRoleIDFromIntelliComboText(pText) 
      If pRoleID <> _RoleID Then 
        _Role = Nothing 
        pFault = ActivateControl("ctlc_Role") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlRole.Controls(0) Is _ctlRole Then 
      pFault = RefreshCtlRole() 
    ElseIf pnlRole.Controls(0) Is _ctlRoleCol Then 
      pFault = RefreshCtlRoleCol() 
    ElseIf pnlRole.Controls(0) Is _ctlPermissionCol Then 
      pFault = RefreshCtlPermissionCol() 
    ElseIf pnlRole.Controls(0) Is _ctlUserCol Then 
      pFault = RefreshCtlUserCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlRole.Controls(0).Name, "", "TRGT-Role-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboRoles(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlRoleCol_evtRowClicked(ByVal vRole As Object) Handles _ctlRoleCol.evtRowClicked 
    
    If vRole Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pRole As csRole = CType(vRole, csRole) 
    _RoleID = pRole.ID 
    
    If _ActiveControl Is _ctlRoleCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csRoleCol.enmFillSumOnTheFlyParameters.GroupByBaseRoleID.ToString() Then 
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
    
    ChooseRole() 
    
    Try 
      MyIntelliCombo.ValueSelect(_RoleID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pRole.Name
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseRole() 
    _Role = Nothing 
    lnkRole.Visible = True 
    _PermissionCol = Nothing 
    lnkPermissionCol.Visible = True 
    _UserCol = Nothing 
    lnkUserCol.Visible = True 
  End Sub 
  Private Sub _ctlRoleCol_evtRowDoubleClicked(ByVal vRole As csRole, ByRef rHandled As Boolean) Handles _ctlRoleCol.evtRowDoubleClicked 
    If lnkRole.Parent IsNot flpMenu Then Exit Sub 
    If vRole Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csRoleCol.enmFillSumOnTheFlyParameters.GroupByBaseRoleID.ToString() Then 
        If pSearchFilters.ContainsKey(csRoleCol.enmFillOnTheFlyParameters.BaseRoleID) Then pSearchFilters.Remove(csRoleCol.enmFillOnTheFlyParameters.BaseRoleID) 
        pSearchFilters.Add(csRoleCol.enmFillOnTheFlyParameters.BaseRoleID, vRole.BaseRoleID) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreRoleCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vRole.ID, vRole.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _RoleID = vRole.ID 
      'MyIntelliCombo.ValueSelect(_RoleID) 
      pFault = ActivateControl("ctlc_Role") 
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
      pFault = _RoleCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _RoleCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _RoleCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _RoleCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_RoleCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csRole.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Role" 
      pFault = _ctlRoleCol.LoadControl(_RoleCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlRoleCol_evtUnChosen() Handles _ctlRoleCol.evtUnChosen 
 
    _RoleID = 0 
    _Role = Nothing 
    _PermissionCol = Nothing 
    lnkPermissionCol.Visible = False 
    _UserCol = Nothing 
    lnkUserCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkRole.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkPermissionCol.Click, 
      lnkUserCol.Click, 
      lnkRoleCol.Click, 
      lnkRole.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkRole OrElse (lnk Is lnkRoleCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlRoleCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_RoleCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csRole.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csRoleCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillRoleCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _RoleCol = New csRoleCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _RoleCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlRoleCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlRoleCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _RoleCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlRoleCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _RoleCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _RoleCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _RoleCol.Count) 
      End If 
    Else 
      _RoleCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _RoleCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlRoleCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Role" 
    
    Dim pRoleID As Long = _RoleID 
    
    pFault = _ctlRoleCol.LoadControl(_RoleCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlRoleCol.Visible = True 
    
    _ctlRoleCol.Refresh() 
    If pRoleID <> 0 Then 
      Dim pRoleCol As csRoleCol = CType(_ctlRoleCol.bsCtlRole.DataSource, csRoleCol) 
      Dim pRole As csRole = pRoleCol.FindByID(pRoleID) 
      If pRole.ID > 0 Then 
        _ctlRoleCol.bsCtlRole.CurrencyManager.Position = pRoleCol.IndexOf(pRole) 
        _ctlRoleCol.dgvRole.Rows(pRoleCol.IndexOf(pRole)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlRole() As clsFault 
    Dim pFault As New clsFault 
    
    If _RoleID > 0 Then 
      ChooseRole() 
      _Role = New csRole(clsEnums.enmLoadParent.TextOnly) 
      pFault = _Role.GetByID(_RoleID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _Role = New csRole(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _Role.Name    
     
    Dim pLoadParameters As New ctlc_Role.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlRole(pLoadParameters)
    pFault = _ctlRole.LoadControl(_Role, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlRole.Visible = True 
    If _RoleID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlRole.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlRole.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlPermissionCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlPermissionCol.dgvPermission.SelectedRows.Count > 0 Then 
      Dim pPermission As csPermission = CType(_ctlPermissionCol.bsCtlPermission.Current, csPermission) 
      pID = pPermission.ID 
    End If 
 
    Dim pTestCol As csPermissionCol = Nothing 
    RaiseEvent evtOverrideFillPermissionCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _PermissionCol = New csPermissionCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _PermissionCol.FillByRoleID(_RoleID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _PermissionCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _PermissionCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _PermissionCol.Count) 
      End If 
    Else 
      _PermissionCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _PermissionCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_PermissionCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _Role IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Role.DefaultDesignation) Then 
        .ReportTitle = "List of Permissions for " & _Role.DefaultDesignation 
      Else 
        .ReportTitle = "List of Permissions for Role" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csPermission.enmProperty.Role) 
    End With 
    RaiseEvent evtOverrideLoadCtlPermissionCol(pLoadParameters)
    
    pFault = _ctlPermissionCol.LoadControl(_PermissionCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlPermissionCol.Visible = True 
 
    If pID > 0 Then 
      Dim pPermissions As csPermissionCol = CType(_ctlPermissionCol.bsCtlPermission.DataSource, csPermissionCol) 
      Dim pPermission As csPermission = pPermissions.FindByID((pID)) 
      If pPermission.ID > 0 Then 
        _ctlPermissionCol.bsCtlPermission.CurrencyManager.Position = pPermissions.IndexOf(pPermission) 
        _ctlPermissionCol.dgvPermission.Rows(pPermissions.IndexOf(pPermission)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlUserCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlUserCol.dgvUser.SelectedRows.Count > 0 Then 
      Dim pUser As csUser = CType(_ctlUserCol.bsCtlUser.Current, csUser) 
      pID = pUser.ID 
    End If 
 
    Dim pTestCol As csUserCol = Nothing 
    RaiseEvent evtOverrideFillUserCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _UserCol = New csUserCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserCol.FillByRoleID(_RoleID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _UserCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserCol.Count) 
      End If 
    Else 
      _UserCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _UserCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_UserCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _Role IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Role.DefaultDesignation) Then 
        .ReportTitle = "List of Users for " & _Role.DefaultDesignation 
      Else 
        .ReportTitle = "List of Users for Role" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csUser.enmProperty.Role) 
    End With 
    RaiseEvent evtOverrideLoadCtlUserCol(pLoadParameters)
    
    pFault = _ctlUserCol.LoadControl(_UserCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlUserCol.Visible = True 
 
    If pID > 0 Then 
      Dim pUsers As csUserCol = CType(_ctlUserCol.bsCtlUser.DataSource, csUserCol) 
      Dim pUser As csUser = pUsers.FindByID((pID)) 
      If pUser.ID > 0 Then 
        _ctlUserCol.bsCtlUser.CurrencyManager.Position = pUsers.IndexOf(pUser) 
        _ctlUserCol.dgvUser.Rows(pUsers.IndexOf(pUser)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlPermissionCol_evtBeforeUpdate(ByVal vPermission As csPermission, ByRef rCancel As Boolean) Handles _ctlPermissionCol.evtBeforeUpdate 
    vPermission.RoleID = _Role.ID 
  End Sub 
  Private Sub _ctlUserCol_evtBeforeUpdate(ByVal vUser As csUser, ByRef rCancel As Boolean) Handles _ctlUserCol.evtBeforeUpdate 
    vUser.RoleID = _Role.ID 
  End Sub 
  Private Sub _ctlRole_evtDeleted(ByVal vRoleID As Long) Handles _ctlRole.evtDeleted 
    _RoleCol = Nothing 
    Dim pFault As clsFault 
    _RoleID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboRoles(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlRole() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlRole.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkRoleCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlRole_evtCancelledEdit(ByVal vRole As csRole) Handles _ctlRole.evtCancelledEdit 
    RefreshCtlRole() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboRoles(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlRole.btnAdd.Visible = False 
      If _RoleID = 0 OrElse _RoleID = -2 Then 
        pnlRole.Visible = False 
      Else 
        pnlRole.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlRole.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_RoleCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlRole_evtUpdated(ByVal vWhichProperty As csRole.enmUpdateType, ByVal vRole As csRole) Handles _ctlRole.evtUpdated 
    _RoleCol = Nothing 
    Dim pFault As clsFault 
    _RoleID = CType(vRole, csRole).ID 
    If _ActiveControl.Name = "ctlc_Role" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboRoles(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlRole() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlRole.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboRoles(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_RoleDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboRole(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _RoleID >= 0 Then 
      MyIntelliCombo.ValueSelect(_RoleID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_RoleUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _RoleID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _RoleID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetRoleIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _RoleID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _RoleID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _RoleID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _RoleID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseRole() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_Role", StringComparison.OrdinalIgnoreCase) AndAlso _RoleID > 0 Then 
        'to avoid getting ObjectNotFound 
        _Role = New csRole(clsEnums.enmLoadParent.TextOnly) 
        pFault = _Role.GetByID(_RoleID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_Role") 
    End If 
    pnlRole.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  Private Sub _ctlPermissionCol_evtRowDoubleClicked(ByVal vPermission As csPermission, ByRef rHandled As Boolean) Handles _ctlPermissionCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtPermissionChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtPermissionChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vPermission.ID 
      .Object = New csPermission 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlUserCol_evtRowDoubleClicked(ByVal vUser As csUser, ByRef rHandled As Boolean) Handles _ctlUserCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtUserChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtUserChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vUser.ID 
      .Object = New csUser 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csRole.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlRole.evtParentChosen 
    If vParentName = csRole.enmParentProperty.BaseRole Then 
      rHandled = True 
      If _CancelEvtBaseRoleChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New csRole 
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
    pnlRole.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkRoleCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _RoleID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_RoleCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkRoleCol.Visible = False 
      _ActiveControl = _ctlRole 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboRoles(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _RoleID <> 0 Then 
        pFault = ActivateControl("ctlc_Role") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlRole.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlRole.Visible = False 
        _RoleID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _RoleID > 0 Then pnlRole.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkPermissionCol.MouseEnter, 
                  lnkUserCol.MouseEnter, 
                  lnkRoleCol.MouseEnter, 
                  lnkRole.MouseEnter, 
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
                  lnkPermissionCol.MouseLeave, 
                  lnkUserCol.MouseLeave, 
                  lnkRoleCol.MouseLeave, 
                  lnkRole.MouseLeave, 
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
  Private Sub _ctlRole_evtAdd(ByVal vRole As csRole) Handles _ctlRole.evtAdd 
    lnkPermissionCol.Visible = False 
    lnkUserCol.Visible = False 
    lnkRoleCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pName As String = Nothing 
    Dim pNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pBaseRoleID As Nullable(Of Long) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByBaseRoleID As Boolean = False 
    
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Roles"  
  
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
        .String01Label.Text = If(_ctlRoleCol.LoadParameters.ColumnsHeaderText.ContainsKey(csRole.enmProperty.Name), _ctlRoleCol.LoadParameters.ColumnsHeaderText(csRole.enmProperty.Name), "Name") 
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
 
        .Combo01Label.Text = If(_ctlRoleCol.LoadParameters.ColumnsHeaderText.ContainsKey(csRole.enmProperty.BaseRole), _ctlRoleCol.LoadParameters.ColumnsHeaderText(csRole.enmProperty.BaseRole), "Base Role") 
        Dim pBaseRoles As New clsComboList 
        pFault = MyCache.GetComboList(clsEnums.enmComboListType.c_RoleDefaultByID, pBaseRoles) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
        'If pBaseRoles IsNot Nothing AndAlso pBaseRoles.Count > 0 Then 
        .flpFilter.Controls.Add(.Combo01Label) 
        .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        'End If 
        With .Combo01 
          .MakeSmart() 
          If pBaseRoles IsNot Nothing Then 
            .LoadControl(pBaseRoles, GetChoose(_Requester)) 
          Else 
            .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.c_RoleDefaultByID, 0, _Requester) 
          End If 
          .TabIndex = 5 
        End With 
 
        .Text01Label.Text = If(_ctlRoleCol.LoadParameters.ColumnsHeaderText.ContainsKey(csRole.enmProperty.ID), _ctlRoleCol.LoadParameters.ColumnsHeaderText(csRole.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 6 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlRoleCol.LoadParameters.ColumnsHeaderText.ContainsKey(csRole.enmProperty.BaseRole), _ctlRoleCol.LoadParameters.ColumnsHeaderText(csRole.enmProperty.BaseRole), "Base Role") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
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
        pName = .String01Text.Text 
        pNameWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csRoleCol.enmFillOnTheFlyParameters.Name, pName) 
        _SearchFilters.Add(csRoleCol.enmFillOnTheFlyParameters.NameWildcardType, pNameWildcardType) 
      End If 
      If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
        pBaseRoleID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
        _SearchFilters.Add(csRoleCol.enmFillOnTheFlyParameters.BaseRoleID, pBaseRoleID) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csRoleCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csRoleCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csRoleCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csRoleCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByBaseRoleID = True 
        pDoSum = True 
        _SearchFilters.Add(csRoleCol.enmFillSumOnTheFlyParameters.GroupByBaseRoleID, pGroupByBaseRoleID) 
      End If 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csRoleCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csRoleCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csRoleCol.enmListDefinition.Dir) Then _SearchFilters.Add(csRoleCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_RoleCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_RoleCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csRole.enmProperty.ID, "ID") 
      End With 
      _RoleCol = New csRoleCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _RoleCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _RoleCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _RoleCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _RoleCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _RoleCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Role" 
      RaiseEvent evtOverrideLoadCtlRoleCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _RoleCol = New csRoleCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _RoleCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_RoleCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _RoleCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csRole.enmProperty.ID, "Count") 
        If pGroupByBaseRoleID = False Then .ColumnsHide.Add(csRole.enmProperty.BaseRole) 
        .ColumnsHide.Add(csRole.enmProperty.Name) 
        .ColumnsHide.Add(csRole.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlRoleCol.Visible = True 
    pFault = _ctlRoleCol.LoadControl(_RoleCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csRoleCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csRoleCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlRole.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlRole.Controls(0).Name) 
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
    _RoleID = -2 
    pFault = ActivateControl("ctlc_Role") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlRole() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlRole.Visible = True 'new 
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
 
  Private Sub _ctlRoleCol_evtTimerTripped() Handles _ctlRoleCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtRoleTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlRoleCol.RoleCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlRoleCol.RoleCol(0).ID 
 
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
    If _RoleCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csRole() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csRoleCol = CType(CallByName(_RoleCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csRoleCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csRoleCol = CType(CallByName(_RoleCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csRoleCol) 
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
                  ccHelper.GetPropertyTypeName(New csRoleCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csRoleCol = CType(CallByName(_RoleCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csRoleCol) 
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
 
  Private Sub cc_ctlPnlRole_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
  Private Sub ctlPnlc_Role_ccevtOverrideLoadCboRole(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) Handles Me.evtOverrideLoadCboRole 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserPermissionUpdate, _Requester) = False Then 'allow to see all roles  
      rComboListTypeToLoad = clsEnums.enmComboListType.c_RoleWithBaseDefaultByID 
    End If 
  End Sub 
  
End Class 
