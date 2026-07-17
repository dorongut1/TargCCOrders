Public Class ctlPnlc_UserPermission 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlUserPermissionCol As ctlc_UserPermissionCol 
  Private WithEvents _ctlUserPermission As ctlc_UserPermission 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _UserPermissionID As Long 
 
  'The data holders 
  Private _UserPermissionCol As csUserPermissionCol 
  Private _UserPermission As csUserPermission 
 
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
  Public Event evtOverrideLoadCboUserPermission(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetUserPermissionIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillUserPermissionCol(ByRef rUserPermissionCol As csUserPermissionCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlUserPermissionCol(ByRef rLoadParameters As ctlc_UserPermissionCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlUserPermission(ByRef rLoadParameters As ctlc_UserPermission.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreUserPermissionCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtUserPermissionTimerTripped(ByRef rCancel As Boolean) 
  
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
 
    lnkUserPermissionCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkUserPermission.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vUserPermissionID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _UserPermissionID = CType(vUserPermissionID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlUserPermission.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkUserPermissionCol.Visible = False 
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
      pFault = LoadCboUserPermissions(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _UserPermissionID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_UserPermissionID) 
      End If 
      ChooseUserPermission() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_UserPermission") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _UserPermissionID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _UserPermissionID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_UserPermission" OrElse pControlName = "ctlUserPermission" Then 
      lnkUserPermission.ForeColor = Color.Black : lnkUserPermission.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserPermission.BackColor = Color.Wheat 
      If _ctlUserPermission Is Nothing Then 
        _ctlUserPermission = New ctlc_UserPermission() 
        _ctlUserPermission.Dock = DockStyle.Fill 
        _ctlUserPermission.Controls.RemoveByKey("btnAdd") 
        pnlUserPermission.Controls.Add(_ctlUserPermission) 
        _ctlUserPermission.Visible = False 
      End If 
      If _UserPermissionID = 0 Then 
        pnlUserPermission.Visible = False 
      End If 
      'If _UserPermission Is Nothing Then 
      pFault = RefreshCtlUserPermission() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlUserPermission.UserPermission.IsEmpty AndAlso _UserPermissionID <> -2 Then 
        pnlUserPermission.Visible = False 
      End If 
      _ctlUserPermission.Name = "ctlc_UserPermission" 
      _ActiveControl = _ctlUserPermission 
      _ctlUserPermission.BringToFront() 
      _ctlUserPermission.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_UserPermissionCol" Then 
      lnkUserPermissionCol.ForeColor = Color.Black : lnkUserPermissionCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserPermissionCol.BackColor = Color.Wheat 
      If _ctlUserPermissionCol Is Nothing Then 
        _ctlUserPermissionCol = New ctlc_UserPermissionCol() 
        _ctlUserPermissionCol.Dock = DockStyle.Fill 
        pnlUserPermission.Controls.Add(_ctlUserPermissionCol) 
        _ctlUserPermissionCol.Visible = False 
      End If  
      pnlUserPermission.Visible = True 
      If _UserPermissionCol Is Nothing Then 
        pFault = RefreshCtlUserPermissionCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlUserPermissionCol.Name = "ctlc_UserPermissionCol" 
      _ActiveControl = _ctlUserPermissionCol 
      _ctlUserPermissionCol.BringToFront() 
      _ctlUserPermissionCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-UserPermission-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("UserPermission", _Requester) 
 
    lnkUserPermissionCol.Text = CCTextTranslate("List", _Requester) 
    lnkUserPermission.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlUserPermission.Controls(0) Is _ctlUserPermission Then 
      If _UserPermissionID = 0 Then 
        pnlUserPermission.Visible = False 
      End If 
    ElseIf pnlUserPermission.Controls(0) Is _ctlUserPermissionCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pUserPermissionID As Long = _UserPermissionID 
      If ccHelper.IsNumeric(pText) Then _UserPermissionID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetUserPermissionIDFromIntelliComboText(pText) 
      If pUserPermissionID <> _UserPermissionID Then 
        _UserPermission = Nothing 
        pFault = ActivateControl("ctlc_UserPermission") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlUserPermission.Controls(0) Is _ctlUserPermission Then 
      pFault = RefreshCtlUserPermission() 
    ElseIf pnlUserPermission.Controls(0) Is _ctlUserPermissionCol Then 
      pFault = RefreshCtlUserPermissionCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlUserPermission.Controls(0).Name, "", "TRGT-UserPermission-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub _ctlUserPermissionCol_evtRowClicked(ByVal vUserPermission As Object) Handles _ctlUserPermissionCol.evtRowClicked 
    
    If vUserPermission Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pUserPermission As csUserPermission = CType(vUserPermission, csUserPermission) 
    _UserPermissionID = pUserPermission.ID 
    
    If _ActiveControl Is _ctlUserPermissionCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csUserPermissionCol.enmFillSumOnTheFlyParameters.GroupByUserID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserPermissionCol.enmFillSumOnTheFlyParameters.GroupByApplicationName.ToString() Then 
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
    
    ChooseUserPermission() 
    
    Try 
      MyIntelliCombo.ValueSelect(_UserPermissionID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pUserPermission.ID.ToString("#,##0")

 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseUserPermission() 
    _UserPermission = Nothing 
    lnkUserPermission.Visible = True 
  End Sub 
  Private Sub _ctlUserPermissionCol_evtRowDoubleClicked(ByVal vUserPermission As csUserPermission, ByRef rHandled As Boolean) Handles _ctlUserPermissionCol.evtRowDoubleClicked 
    If lnkUserPermission.Parent IsNot flpMenu Then Exit Sub 
    If vUserPermission Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csUserPermissionCol.enmFillSumOnTheFlyParameters.GroupByUserID.ToString() Then 
        If pSearchFilters.ContainsKey(csUserPermissionCol.enmFillOnTheFlyParameters.UserID) Then pSearchFilters.Remove(csUserPermissionCol.enmFillOnTheFlyParameters.UserID) 
        pSearchFilters.Add(csUserPermissionCol.enmFillOnTheFlyParameters.UserID, vUserPermission.UserID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserPermissionCol.enmFillSumOnTheFlyParameters.GroupByApplicationName.ToString() Then 
        If pSearchFilters.ContainsKey(csUserPermissionCol.enmFillOnTheFlyParameters.ApplicationName) Then pSearchFilters.Remove(csUserPermissionCol.enmFillOnTheFlyParameters.ApplicationName) 
        pSearchFilters.Add(csUserPermissionCol.enmFillOnTheFlyParameters.ApplicationName, vUserPermission.ApplicationName) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreUserPermissionCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vUserPermission.ID, vUserPermission.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _UserPermissionID = vUserPermission.ID 
      'MyIntelliCombo.ValueSelect(_UserPermissionID) 
      pFault = ActivateControl("ctlc_UserPermission") 
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
      pFault = _UserPermissionCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _UserPermissionCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserPermissionCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserPermissionCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_UserPermissionCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csUserPermission.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserPermission" 
      pFault = _ctlUserPermissionCol.LoadControl(_UserPermissionCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlUserPermissionCol_evtUnChosen() Handles _ctlUserPermissionCol.evtUnChosen 
 
    _UserPermissionID = 0 
    _UserPermission = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkUserPermission.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkUserPermissionCol.Click, 
      lnkUserPermission.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkUserPermission OrElse (lnk Is lnkUserPermissionCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlUserPermissionCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_UserPermissionCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csUserPermission.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csUserPermissionCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillUserPermissionCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _UserPermissionCol = New csUserPermissionCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _UserPermissionCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlUserPermissionCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _UserPermissionCol.FillByUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=100, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then  
              _ctlUserPermissionCol.Timer?.Stop()  
              Return pFault  
            End If  
          Case Else 
            If _ctlUserPermissionCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _UserPermissionCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlUserPermissionCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _UserPermissionCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _UserPermissionCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserPermissionCol.Count) 
      End If 
    Else 
      _UserPermissionCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _UserPermissionCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlUserPermissionCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserPermission" 
    
    Dim pUserPermissionID As Long = _UserPermissionID 
    
    pFault = _ctlUserPermissionCol.LoadControl(_UserPermissionCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlUserPermissionCol.Visible = True 
    
    _ctlUserPermissionCol.Refresh() 
    If pUserPermissionID <> 0 Then 
      Dim pUserPermissionCol As csUserPermissionCol = CType(_ctlUserPermissionCol.bsCtlUserPermission.DataSource, csUserPermissionCol) 
      Dim pUserPermission As csUserPermission = pUserPermissionCol.FindByID(pUserPermissionID) 
      If pUserPermission.ID > 0 Then 
        _ctlUserPermissionCol.bsCtlUserPermission.CurrencyManager.Position = pUserPermissionCol.IndexOf(pUserPermission) 
        _ctlUserPermissionCol.dgvUserPermission.Rows(pUserPermissionCol.IndexOf(pUserPermission)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlUserPermission() As clsFault 
    Dim pFault As New clsFault 
    
    If _UserPermissionID > 0 Then 
      ChooseUserPermission() 
      _UserPermission = New csUserPermission(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserPermission.GetByID(_UserPermissionID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _UserPermission = New csUserPermission(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _UserPermission.ID.ToString("#,##0")
    
     
    Dim pLoadParameters As New ctlc_UserPermission.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlUserPermission(pLoadParameters)
    pFault = _ctlUserPermission.LoadControl(_UserPermission, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlUserPermission.Visible = True 
    If _UserPermissionID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlUserPermission.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlUserPermission.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlUserPermission_evtDeleted(ByVal vUserPermissionID As Long) Handles _ctlUserPermission.evtDeleted 
    _UserPermissionCol = Nothing 
    Dim pFault As clsFault 
    _UserPermissionID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboUserPermissions(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlUserPermission() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlUserPermission.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkUserPermissionCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlUserPermission_evtCancelledEdit(ByVal vUserPermission As csUserPermission) Handles _ctlUserPermission.evtCancelledEdit 
    RefreshCtlUserPermission() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboUserPermissions(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlUserPermission.btnAdd.Visible = False 
      If _UserPermissionID = 0 OrElse _UserPermissionID = -2 Then 
        pnlUserPermission.Visible = False 
      Else 
        pnlUserPermission.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlUserPermission.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_UserPermissionCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlUserPermission_evtUpdated(ByVal vWhichProperty As csUserPermission.enmUpdateType, ByVal vUserPermission As csUserPermission) Handles _ctlUserPermission.evtUpdated 
    _UserPermissionCol = Nothing 
    Dim pFault As clsFault 
    _UserPermissionID = CType(vUserPermission, csUserPermission).ID 
    If _ActiveControl.Name = "ctlc_UserPermission" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboUserPermissions(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlUserPermission() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlUserPermission.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboUserPermissions(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.UD 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboUserPermission(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _UserPermissionID >= 0 Then 
      MyIntelliCombo.ValueSelect(_UserPermissionID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserPermissionUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _UserPermissionID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _UserPermissionID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetUserPermissionIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _UserPermissionID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _UserPermissionID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _UserPermissionID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _UserPermissionID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseUserPermission() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_UserPermission", StringComparison.OrdinalIgnoreCase) AndAlso _UserPermissionID > 0 Then 
        'to avoid getting ObjectNotFound 
        _UserPermission = New csUserPermission(clsEnums.enmLoadParent.TextOnly) 
        pFault = _UserPermission.GetByID(_UserPermissionID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_UserPermission") 
    End If 
    pnlUserPermission.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csUserPermission.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlUserPermission.evtParentChosen 
    If vParentName = csUserPermission.enmParentProperty.User Then 
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
    pnlUserPermission.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkUserPermissionCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _UserPermissionID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_UserPermissionCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkUserPermissionCol.Visible = False 
      _ActiveControl = _ctlUserPermission 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboUserPermissions(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _UserPermissionID <> 0 Then 
        MyIntelliCombo.cbo.Text = _UserPermissionID.ToString() 
        pFault = ActivateControl("ctlc_UserPermission") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlUserPermission.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlUserPermission.Visible = False 
        _UserPermissionID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _UserPermissionID > 0 Then pnlUserPermission.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkUserPermissionCol.MouseEnter, 
                  lnkUserPermission.MouseEnter, 
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
                  lnkUserPermissionCol.MouseLeave, 
                  lnkUserPermission.MouseLeave, 
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
  Private Sub _ctlUserPermission_evtAdd(ByVal vUserPermission As csUserPermission) Handles _ctlUserPermission.evtAdd 
    lnkUserPermissionCol.Visible = False 
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
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByUserID As Boolean = False 
    Dim pGroupByApplicationName As Boolean = False 
    
    Dim pSumLoggedLoginID As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the User Permissions"  
  
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
         .Combo01Label.Text = If(_ctlUserPermissionCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserPermission.enmProperty.User), _ctlUserPermissionCol.LoadParameters.ColumnsHeaderText(csUserPermission.enmProperty.User), "User") 
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
 
        .String01Label.Text = If(_ctlUserPermissionCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserPermission.enmProperty.ApplicationName), _ctlUserPermissionCol.LoadParameters.ColumnsHeaderText(csUserPermission.enmProperty.ApplicationName), "Application Name") 
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
 
        .Text01Label.Text = If(_ctlUserPermissionCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserPermission.enmProperty.ID), _ctlUserPermissionCol.LoadParameters.ColumnsHeaderText(csUserPermission.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 6 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlUserPermissionCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserPermission.enmProperty.User), _ctlUserPermissionCol.LoadParameters.ColumnsHeaderText(csUserPermission.enmProperty.User), "User") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlUserPermissionCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserPermission.enmProperty.ApplicationName), _ctlUserPermissionCol.LoadParameters.ColumnsHeaderText(csUserPermission.enmProperty.ApplicationName), "Application Name") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlUserPermissionCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserPermission.enmProperty.LoggedLoginID), _ctlUserPermissionCol.LoadParameters.ColumnsHeaderText(csUserPermission.enmProperty.LoggedLoginID), "Logged Login ID") 
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
      If pUserID Is Nothing Then 
       If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
         pUserID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
         _SearchFilters.Add(csUserPermissionCol.enmFillOnTheFlyParameters.UserID, pUserID) 
       End If 
      Else 
        _SearchFilters.Add(csUserPermissionCol.enmFillOnTheFlyParameters.UserID, pUserID) 
      End If  
      If .String01Text.Text <> "" Then 
        pApplicationName = .String01Text.Text 
        pApplicationNameWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csUserPermissionCol.enmFillOnTheFlyParameters.ApplicationName, pApplicationName) 
        _SearchFilters.Add(csUserPermissionCol.enmFillOnTheFlyParameters.ApplicationNameWildcardType, pApplicationNameWildcardType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csUserPermissionCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csUserPermissionCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csUserPermissionCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csUserPermissionCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByUserID = True 
        pDoSum = True 
        _SearchFilters.Add(csUserPermissionCol.enmFillSumOnTheFlyParameters.GroupByUserID, pGroupByUserID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByApplicationName = True 
        pDoSum = True 
        _SearchFilters.Add(csUserPermissionCol.enmFillSumOnTheFlyParameters.GroupByApplicationName, pGroupByApplicationName) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumLoggedLoginID = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csUserPermissionCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csUserPermissionCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csUserPermissionCol.enmListDefinition.Dir) Then _SearchFilters.Add(csUserPermissionCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_UserPermissionCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_UserPermissionCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csUserPermission.enmProperty.ID, "ID") 
      End With 
      _UserPermissionCol = New csUserPermissionCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _UserPermissionCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _UserPermissionCol.FillByUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
          Case Else 
            pFault = _UserPermissionCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _UserPermissionCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserPermissionCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserPermissionCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserPermission" 
      RaiseEvent evtOverrideLoadCtlUserPermissionCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _UserPermissionCol = New csUserPermissionCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserPermissionCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_UserPermissionCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _UserPermissionCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csUserPermission.enmProperty.ID, "Count") 
        If pGroupByUserID = False Then .ColumnsHide.Add(csUserPermission.enmProperty.User) 
        If pGroupByApplicationName = False Then .ColumnsHide.Add(csUserPermission.enmProperty.ApplicationName) 
        If pSumLoggedLoginID = False Then .ColumnsHide.Add(csUserPermission.enmProperty.LoggedLoginID) 
        .ColumnsHide.Add(csUserPermission.enmProperty.ComputerIdentifier) 
        .ColumnsHide.Add(csUserPermission.enmProperty.ComputerName) 
        .ColumnsHide.Add(csUserPermission.enmProperty.ExternalIP) 
        .ColumnsHide.Add(csUserPermission.enmProperty.HasPermission) 
        .ColumnsHide.Add(csUserPermission.enmProperty.Comments) 
        .ColumnsHide.Add(csUserPermission.enmProperty.LastAccessTime) 
        .ColumnsHide.Add(csUserPermission.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlUserPermissionCol.Visible = True 
    pFault = _ctlUserPermissionCol.LoadControl(_UserPermissionCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csUserPermissionCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csUserPermissionCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlUserPermission.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlUserPermission.Controls(0).Name) 
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
    _UserPermissionID = -2 
    pFault = ActivateControl("ctlc_UserPermission") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlUserPermission() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlUserPermission.Visible = True 'new 
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
 
  Private Sub _ctlUserPermissionCol_evtTimerTripped() Handles _ctlUserPermissionCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtUserPermissionTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlUserPermissionCol.UserPermissionCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlUserPermissionCol.UserPermissionCol(0).ID 
 
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
    If _UserPermissionCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csUserPermission() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csUserPermissionCol = CType(CallByName(_UserPermissionCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserPermissionCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csUserPermissionCol = CType(CallByName(_UserPermissionCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserPermissionCol) 
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
                  ccHelper.GetPropertyTypeName(New csUserPermissionCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csUserPermissionCol = CType(CallByName(_UserPermissionCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserPermissionCol) 
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
 
  Private Sub cc_ctlPnlUserPermission_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
