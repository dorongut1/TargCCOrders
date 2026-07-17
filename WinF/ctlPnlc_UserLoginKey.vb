Public Class ctlPnlc_UserLoginKey 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlUserLoginKeyCol As ctlc_UserLoginKeyCol 
  Private WithEvents _ctlUserLoginKey As ctlc_UserLoginKey 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _UserLoginKeyID As Long 
 
  'The data holders 
  Private _UserLoginKeyCol As csUserLoginKeyCol 
  Private _UserLoginKey As csUserLoginKey 
 
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
  Public Event evtOverrideLoadCboUserLoginKey(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetUserLoginKeyIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillUserLoginKeyCol(ByRef rUserLoginKeyCol As csUserLoginKeyCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlUserLoginKeyCol(ByRef rLoadParameters As ctlc_UserLoginKeyCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlUserLoginKey(ByRef rLoadParameters As ctlc_UserLoginKey.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreUserLoginKeyCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtUserLoginKeyTimerTripped(ByRef rCancel As Boolean) 
  
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
 
    lnkUserLoginKeyCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkUserLoginKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vUserLoginKeyID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _UserLoginKeyID = CType(vUserLoginKeyID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlUserLoginKey.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkUserLoginKeyCol.Visible = False 
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
      pFault = LoadCboUserLoginKeys(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _UserLoginKeyID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_UserLoginKeyID) 
      End If 
      ChooseUserLoginKey() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_UserLoginKey") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _UserLoginKeyID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _UserLoginKeyID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_UserLoginKey" OrElse pControlName = "ctlUserLoginKey" Then 
      lnkUserLoginKey.ForeColor = Color.Black : lnkUserLoginKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserLoginKey.BackColor = Color.Wheat 
      If _ctlUserLoginKey Is Nothing Then 
        _ctlUserLoginKey = New ctlc_UserLoginKey() 
        _ctlUserLoginKey.Dock = DockStyle.Fill 
        pnlUserLoginKey.Controls.Add(_ctlUserLoginKey) 
        _ctlUserLoginKey.Visible = False 
      End If 
      If _UserLoginKeyID = 0 Then 
        pnlUserLoginKey.Visible = False 
      End If 
      'If _UserLoginKey Is Nothing Then 
      pFault = RefreshCtlUserLoginKey() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlUserLoginKey.UserLoginKey.IsEmpty AndAlso _UserLoginKeyID <> -2 Then 
        pnlUserLoginKey.Visible = False 
      End If 
      _ctlUserLoginKey.Name = "ctlc_UserLoginKey" 
      _ActiveControl = _ctlUserLoginKey 
      _ctlUserLoginKey.BringToFront() 
      _ctlUserLoginKey.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_UserLoginKeyCol" Then 
      lnkUserLoginKeyCol.ForeColor = Color.Black : lnkUserLoginKeyCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserLoginKeyCol.BackColor = Color.Wheat 
      If _ctlUserLoginKeyCol Is Nothing Then 
        _ctlUserLoginKeyCol = New ctlc_UserLoginKeyCol() 
        _ctlUserLoginKeyCol.Dock = DockStyle.Fill 
        pnlUserLoginKey.Controls.Add(_ctlUserLoginKeyCol) 
        _ctlUserLoginKeyCol.Visible = False 
      End If  
      pnlUserLoginKey.Visible = True 
      If _UserLoginKeyCol Is Nothing Then 
        pFault = RefreshCtlUserLoginKeyCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlUserLoginKeyCol.Name = "ctlc_UserLoginKeyCol" 
      _ActiveControl = _ctlUserLoginKeyCol 
      _ctlUserLoginKeyCol.BringToFront() 
      _ctlUserLoginKeyCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-UserLoginKey-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("UserLoginKey", _Requester) 
 
    lnkUserLoginKeyCol.Text = CCTextTranslate("List", _Requester) 
    lnkUserLoginKey.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlUserLoginKey.Controls(0) Is _ctlUserLoginKey Then 
      If _UserLoginKeyID = 0 Then 
        pnlUserLoginKey.Visible = False 
      End If 
    ElseIf pnlUserLoginKey.Controls(0) Is _ctlUserLoginKeyCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pUserLoginKeyID As Long = _UserLoginKeyID 
      If ccHelper.IsNumeric(pText) Then _UserLoginKeyID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetUserLoginKeyIDFromIntelliComboText(pText) 
      If pUserLoginKeyID <> _UserLoginKeyID Then 
        _UserLoginKey = Nothing 
        pFault = ActivateControl("ctlc_UserLoginKey") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlUserLoginKey.Controls(0) Is _ctlUserLoginKey Then 
      pFault = RefreshCtlUserLoginKey() 
    ElseIf pnlUserLoginKey.Controls(0) Is _ctlUserLoginKeyCol Then 
      pFault = RefreshCtlUserLoginKeyCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlUserLoginKey.Controls(0).Name, "", "TRGT-UserLoginKey-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub _ctlUserLoginKeyCol_evtRowClicked(ByVal vUserLoginKey As Object) Handles _ctlUserLoginKeyCol.evtRowClicked 
    
    If vUserLoginKey Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pUserLoginKey As csUserLoginKey = CType(vUserLoginKey, csUserLoginKey) 
    _UserLoginKeyID = pUserLoginKey.ID 
    
    If _ActiveControl Is _ctlUserLoginKeyCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByUserID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByApplicationName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByApplicationIdentifier.ToString() Then 
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
    
    ChooseUserLoginKey() 
    
    Try 
      MyIntelliCombo.ValueSelect(_UserLoginKeyID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pUserLoginKey.ID.ToString("#,##0")

 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseUserLoginKey() 
    _UserLoginKey = Nothing 
    lnkUserLoginKey.Visible = True 
  End Sub 
  Private Sub _ctlUserLoginKeyCol_evtRowDoubleClicked(ByVal vUserLoginKey As csUserLoginKey, ByRef rHandled As Boolean) Handles _ctlUserLoginKeyCol.evtRowDoubleClicked 
    If lnkUserLoginKey.Parent IsNot flpMenu Then Exit Sub 
    If vUserLoginKey Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByUserID.ToString() Then 
        If pSearchFilters.ContainsKey(csUserLoginKeyCol.enmFillOnTheFlyParameters.UserID) Then pSearchFilters.Remove(csUserLoginKeyCol.enmFillOnTheFlyParameters.UserID) 
        pSearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.UserID, vUserLoginKey.UserID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByApplicationName.ToString() Then 
        If pSearchFilters.ContainsKey(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationName) Then pSearchFilters.Remove(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationName) 
        pSearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationName, vUserLoginKey.ApplicationName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByApplicationIdentifier.ToString() Then 
        If pSearchFilters.ContainsKey(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationIdentifier) Then pSearchFilters.Remove(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationIdentifier) 
        pSearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationIdentifier, vUserLoginKey.ApplicationIdentifier) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreUserLoginKeyCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vUserLoginKey.ID, vUserLoginKey.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _UserLoginKeyID = vUserLoginKey.ID 
      'MyIntelliCombo.ValueSelect(_UserLoginKeyID) 
      pFault = ActivateControl("ctlc_UserLoginKey") 
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
      pFault = _UserLoginKeyCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _UserLoginKeyCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserLoginKeyCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserLoginKeyCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_UserLoginKeyCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csUserLoginKey.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserLoginKey" 
      pFault = _ctlUserLoginKeyCol.LoadControl(_UserLoginKeyCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlUserLoginKeyCol_evtUnChosen() Handles _ctlUserLoginKeyCol.evtUnChosen 
 
    _UserLoginKeyID = 0 
    _UserLoginKey = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkUserLoginKey.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkUserLoginKeyCol.Click, 
      lnkUserLoginKey.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkUserLoginKey OrElse (lnk Is lnkUserLoginKeyCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlUserLoginKeyCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_UserLoginKeyCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csUserLoginKey.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csUserLoginKeyCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillUserLoginKeyCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _UserLoginKeyCol = New csUserLoginKeyCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _UserLoginKeyCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlUserLoginKeyCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _UserLoginKeyCol.FillByUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=100, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then  
              _ctlUserLoginKeyCol.Timer?.Stop()  
              Return pFault  
            End If  
          Case Else 
            If _ctlUserLoginKeyCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _UserLoginKeyCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlUserLoginKeyCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _UserLoginKeyCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _UserLoginKeyCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserLoginKeyCol.Count) 
      End If 
    Else 
      _UserLoginKeyCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _UserLoginKeyCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlUserLoginKeyCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserLoginKey" 
    
    Dim pUserLoginKeyID As Long = _UserLoginKeyID 
    
    pFault = _ctlUserLoginKeyCol.LoadControl(_UserLoginKeyCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlUserLoginKeyCol.Visible = True 
    
    _ctlUserLoginKeyCol.Refresh() 
    If pUserLoginKeyID <> 0 Then 
      Dim pUserLoginKeyCol As csUserLoginKeyCol = CType(_ctlUserLoginKeyCol.bsCtlUserLoginKey.DataSource, csUserLoginKeyCol) 
      Dim pUserLoginKey As csUserLoginKey = pUserLoginKeyCol.FindByID(pUserLoginKeyID) 
      If pUserLoginKey.ID > 0 Then 
        _ctlUserLoginKeyCol.bsCtlUserLoginKey.CurrencyManager.Position = pUserLoginKeyCol.IndexOf(pUserLoginKey) 
        _ctlUserLoginKeyCol.dgvUserLoginKey.Rows(pUserLoginKeyCol.IndexOf(pUserLoginKey)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlUserLoginKey() As clsFault 
    Dim pFault As New clsFault 
    
    If _UserLoginKeyID > 0 Then 
      ChooseUserLoginKey() 
      _UserLoginKey = New csUserLoginKey(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserLoginKey.GetByID(_UserLoginKeyID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _UserLoginKey = New csUserLoginKey(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _UserLoginKey.ID.ToString("#,##0")
    
     
    Dim pLoadParameters As New ctlc_UserLoginKey.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlUserLoginKey(pLoadParameters)
    pFault = _ctlUserLoginKey.LoadControl(_UserLoginKey, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlUserLoginKey.Visible = True 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlUserLoginKey_evtDeleted(ByVal vUserLoginKeyID As Long) Handles _ctlUserLoginKey.evtDeleted 
    _UserLoginKeyCol = Nothing 
    Dim pFault As clsFault 
    _UserLoginKeyID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboUserLoginKeys(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlUserLoginKey() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    Else  
      lnk_Click(lnkUserLoginKeyCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlUserLoginKey_evtCancelledEdit(ByVal vUserLoginKey As csUserLoginKey) Handles _ctlUserLoginKey.evtCancelledEdit 
    RefreshCtlUserLoginKey() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboUserLoginKeys(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      If _UserLoginKeyID = 0 OrElse _UserLoginKeyID = -2 Then 
        pnlUserLoginKey.Visible = False 
      Else 
        pnlUserLoginKey.Visible = True 
      End If 
    End If 
  End Sub 
  Private Sub _ctlUserLoginKey_evtUpdated(ByVal vWhichProperty As csUserLoginKey.enmUpdateType, ByVal vUserLoginKey As csUserLoginKey) Handles _ctlUserLoginKey.evtUpdated 
    _UserLoginKeyCol = Nothing 
    Dim pFault As clsFault 
    _UserLoginKeyID = CType(vUserLoginKey, csUserLoginKey).ID 
    If _ActiveControl.Name = "ctlc_UserLoginKey" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboUserLoginKeys(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlUserLoginKey() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
    End If  
  End Sub 
  Private Function LoadCboUserLoginKeys(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.UD 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboUserLoginKey(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _UserLoginKeyID >= 0 Then 
      MyIntelliCombo.ValueSelect(_UserLoginKeyID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _UserLoginKeyID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _UserLoginKeyID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetUserLoginKeyIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _UserLoginKeyID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _UserLoginKeyID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _UserLoginKeyID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _UserLoginKeyID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseUserLoginKey() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_UserLoginKey", StringComparison.OrdinalIgnoreCase) AndAlso _UserLoginKeyID > 0 Then 
        'to avoid getting ObjectNotFound 
        _UserLoginKey = New csUserLoginKey(clsEnums.enmLoadParent.TextOnly) 
        pFault = _UserLoginKey.GetByID(_UserLoginKeyID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_UserLoginKey") 
    End If 
    pnlUserLoginKey.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csUserLoginKey.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlUserLoginKey.evtParentChosen 
    If vParentName = csUserLoginKey.enmParentProperty.User Then 
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
    pnlUserLoginKey.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkUserLoginKeyCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _UserLoginKeyID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_UserLoginKeyCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkUserLoginKeyCol.Visible = False 
      _ActiveControl = _ctlUserLoginKey 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboUserLoginKeys(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _UserLoginKeyID <> 0 Then 
        MyIntelliCombo.cbo.Text = _UserLoginKeyID.ToString() 
        pFault = ActivateControl("ctlc_UserLoginKey") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlUserLoginKey.Visible = False 
        _UserLoginKeyID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _UserLoginKeyID > 0 Then pnlUserLoginKey.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkUserLoginKeyCol.MouseEnter, 
                  lnkUserLoginKey.MouseEnter, 
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
                  lnkUserLoginKeyCol.MouseLeave, 
                  lnkUserLoginKey.MouseLeave, 
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
    Dim pUserID As Nullable(Of Long) = Nothing 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.c_User Then 
      pUserID = _Requester.UserIdentityInstanceID 
    End If 
    Dim pApplicationName As String = Nothing 
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pApplicationIdentifier As String = Nothing 
    Dim pApplicationIdentifierWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByUserID As Boolean = False 
    Dim pGroupByApplicationName As Boolean = False 
    Dim pGroupByApplicationIdentifier As Boolean = False 
    
    Dim pSumLoggedLoginID As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the User Login Keies"  
  
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
         .Combo01Label.Text = If(_ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserLoginKey.enmProperty.User), _ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText(csUserLoginKey.enmProperty.User), "User") 
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
 
        .String01Label.Text = If(_ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserLoginKey.enmProperty.ApplicationName), _ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText(csUserLoginKey.enmProperty.ApplicationName), "Application Name") 
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
 
        .String02Label.Text = If(_ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserLoginKey.enmProperty.ApplicationIdentifier), _ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText(csUserLoginKey.enmProperty.ApplicationIdentifier), "Application Identifier") 
        .String02Text.Text = "" 
        .String02Text.TabIndex = 6 
        With .String02WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 7 
        End With 
        .flpFilter.Controls.Add(.String02Label) 
        .flpFilter.Controls.Add(.String02Text) 
        .flpFilter.Controls.Add(.String02LblWCType) 
        .flpFilter.Controls.Add(.String02WCType) 
 
        .Text01Label.Text = If(_ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserLoginKey.enmProperty.ID), _ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText(csUserLoginKey.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 8 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 9 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserLoginKey.enmProperty.User), _ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText(csUserLoginKey.enmProperty.User), "User") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 10 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserLoginKey.enmProperty.ApplicationName), _ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText(csUserLoginKey.enmProperty.ApplicationName), "Application Name") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 11 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserLoginKey.enmProperty.ApplicationIdentifier), _ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText(csUserLoginKey.enmProperty.ApplicationIdentifier), "Application Identifier") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 12 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUserLoginKey.enmProperty.LoggedLoginID), _ctlUserLoginKeyCol.LoadParameters.ColumnsHeaderText(csUserLoginKey.enmProperty.LoggedLoginID), "Logged Login ID") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 13 
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
         _SearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.UserID, pUserID) 
       End If 
      Else 
        _SearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.UserID, pUserID) 
      End If  
      If .String01Text.Text <> "" Then 
        pApplicationName = .String01Text.Text 
        pApplicationNameWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationName, pApplicationName) 
        _SearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationNameWildcardType, pApplicationNameWildcardType) 
      End If 
      If .String02Text.Text <> "" Then 
        pApplicationIdentifier = .String02Text.Text 
        pApplicationIdentifierWildcardType = CType(CType(.String02WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationIdentifier, pApplicationIdentifier) 
        _SearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.ApplicationIdentifierWildcardType, pApplicationIdentifierWildcardType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csUserLoginKeyCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csUserLoginKeyCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csUserLoginKeyCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByUserID = True 
        pDoSum = True 
        _SearchFilters.Add(csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByUserID, pGroupByUserID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByApplicationName = True 
        pDoSum = True 
        _SearchFilters.Add(csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByApplicationName, pGroupByApplicationName) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByApplicationIdentifier = True 
        pDoSum = True 
        _SearchFilters.Add(csUserLoginKeyCol.enmFillSumOnTheFlyParameters.GroupByApplicationIdentifier, pGroupByApplicationIdentifier) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumLoggedLoginID = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csUserLoginKeyCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csUserLoginKeyCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csUserLoginKeyCol.enmListDefinition.Dir) Then _SearchFilters.Add(csUserLoginKeyCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_UserLoginKeyCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_UserLoginKeyCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csUserLoginKey.enmProperty.ID, "ID") 
      End With 
      _UserLoginKeyCol = New csUserLoginKeyCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _UserLoginKeyCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _UserLoginKeyCol.FillByUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
          Case Else 
            pFault = _UserLoginKeyCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _UserLoginKeyCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserLoginKeyCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserLoginKeyCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see UserLoginKey" 
      RaiseEvent evtOverrideLoadCtlUserLoginKeyCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _UserLoginKeyCol = New csUserLoginKeyCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserLoginKeyCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_UserLoginKeyCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _UserLoginKeyCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csUserLoginKey.enmProperty.ID, "Count") 
        If pGroupByUserID = False Then .ColumnsHide.Add(csUserLoginKey.enmProperty.User) 
        If pGroupByApplicationName = False Then .ColumnsHide.Add(csUserLoginKey.enmProperty.ApplicationName) 
        If pGroupByApplicationIdentifier = False Then .ColumnsHide.Add(csUserLoginKey.enmProperty.ApplicationIdentifier) 
        If pSumLoggedLoginID = False Then .ColumnsHide.Add(csUserLoginKey.enmProperty.LoggedLoginID) 
        .ColumnsHide.Add(csUserLoginKey.enmProperty.KeyHashed) 
        .ColumnsHide.Add(csUserLoginKey.enmProperty.ExternalIPAtCreation) 
        .ColumnsHide.Add(csUserLoginKey.enmProperty.CountryAtCreation) 
        .ColumnsHide.Add(csUserLoginKey.enmProperty.LastAccessTime) 
        .ColumnsHide.Add(csUserLoginKey.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlUserLoginKeyCol.Visible = True 
    pFault = _ctlUserLoginKeyCol.LoadControl(_UserLoginKeyCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csUserLoginKeyCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csUserLoginKeyCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlUserLoginKey.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlUserLoginKey.Controls(0).Name) 
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
 
  Private Sub _ctlUserLoginKeyCol_evtTimerTripped() Handles _ctlUserLoginKeyCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtUserLoginKeyTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlUserLoginKeyCol.UserLoginKeyCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlUserLoginKeyCol.UserLoginKeyCol(0).ID 
 
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
    If _UserLoginKeyCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csUserLoginKey() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csUserLoginKeyCol = CType(CallByName(_UserLoginKeyCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserLoginKeyCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csUserLoginKeyCol = CType(CallByName(_UserLoginKeyCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserLoginKeyCol) 
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
                  ccHelper.GetPropertyTypeName(New csUserLoginKeyCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csUserLoginKeyCol = CType(CallByName(_UserLoginKeyCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserLoginKeyCol) 
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
 
  Private Sub cc_ctlPnlUserLoginKey_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
