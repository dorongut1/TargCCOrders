Public Class ctlPnlc_LoggedRequest 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlLoggedRequestCol As ctlc_LoggedRequestCol 
  Private WithEvents _ctlLoggedRequest As ctlc_LoggedRequest 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _LoggedRequestID As Long 
 
  'The data holders 
  Private _LoggedRequestCol As csLoggedRequestCol 
  Private _LoggedRequest As csLoggedRequest 
 
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
  Public Event evtOverrideLoadCboLoggedRequest(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetLoggedRequestIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillLoggedRequestCol(ByRef rLoggedRequestCol As csLoggedRequestCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlLoggedRequestCol(ByRef rLoadParameters As ctlc_LoggedRequestCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedRequest(ByRef rLoadParameters As ctlc_LoggedRequest.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreLoggedRequestCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtLoggedRequestTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  'Parents
  Private _CancelEvtLoggedLoginChosen As Boolean = False 
  Private _ShowPopForEvtLoggedLoginChosen As Boolean = False 
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
 
    lnkLoggedRequestCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedRequest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vLoggedRequestID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _LoggedRequestID = CType(vLoggedRequestID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlLoggedRequest.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkLoggedRequestCol.Visible = False 
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
      pFault = LoadCboLoggedRequests(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _LoggedRequestID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_LoggedRequestID) 
      End If 
      ChooseLoggedRequest() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_LoggedRequest") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _LoggedRequestID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _LoggedRequestID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_LoggedRequest" OrElse pControlName = "ctlLoggedRequest" Then 
      lnkLoggedRequest.ForeColor = Color.Black : lnkLoggedRequest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedRequest.BackColor = Color.Wheat 
      If _ctlLoggedRequest Is Nothing Then 
        _ctlLoggedRequest = New ctlc_LoggedRequest() 
        _ctlLoggedRequest.Dock = DockStyle.Fill 
        pnlLoggedRequest.Controls.Add(_ctlLoggedRequest) 
        _ctlLoggedRequest.Visible = False 
      End If 
      If _LoggedRequestID = 0 Then 
        pnlLoggedRequest.Visible = False 
      End If 
      'If _LoggedRequest Is Nothing Then 
      pFault = RefreshCtlLoggedRequest() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlLoggedRequest.LoggedRequest.IsEmpty AndAlso _LoggedRequestID <> -2 Then 
        pnlLoggedRequest.Visible = False 
      End If 
      _ctlLoggedRequest.Name = "ctlc_LoggedRequest" 
      _ActiveControl = _ctlLoggedRequest 
      _ctlLoggedRequest.BringToFront() 
      _ctlLoggedRequest.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_LoggedRequestCol" Then 
      lnkLoggedRequestCol.ForeColor = Color.Black : lnkLoggedRequestCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedRequestCol.BackColor = Color.Wheat 
      If _ctlLoggedRequestCol Is Nothing Then 
        _ctlLoggedRequestCol = New ctlc_LoggedRequestCol() 
        _ctlLoggedRequestCol.Dock = DockStyle.Fill 
        pnlLoggedRequest.Controls.Add(_ctlLoggedRequestCol) 
        _ctlLoggedRequestCol.Visible = False 
      End If  
      pnlLoggedRequest.Visible = True 
      If _LoggedRequestCol Is Nothing Then 
        pFault = RefreshCtlLoggedRequestCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedRequestCol.Name = "ctlc_LoggedRequestCol" 
      _ActiveControl = _ctlLoggedRequestCol 
      _ctlLoggedRequestCol.BringToFront() 
      _ctlLoggedRequestCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-LoggedRequest-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("LoggedRequest", _Requester) 
 
    lnkLoggedRequestCol.Text = CCTextTranslate("List", _Requester) 
    lnkLoggedRequest.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlLoggedRequest.Controls(0) Is _ctlLoggedRequest Then 
      If _LoggedRequestID = 0 Then 
        pnlLoggedRequest.Visible = False 
      End If 
    ElseIf pnlLoggedRequest.Controls(0) Is _ctlLoggedRequestCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pLoggedRequestID As Long = _LoggedRequestID 
      If ccHelper.IsNumeric(pText) Then _LoggedRequestID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetLoggedRequestIDFromIntelliComboText(pText) 
      If pLoggedRequestID <> _LoggedRequestID Then 
        _LoggedRequest = Nothing 
        pFault = ActivateControl("ctlc_LoggedRequest") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlLoggedRequest.Controls(0) Is _ctlLoggedRequest Then 
      pFault = RefreshCtlLoggedRequest() 
    ElseIf pnlLoggedRequest.Controls(0) Is _ctlLoggedRequestCol Then 
      pFault = RefreshCtlLoggedRequestCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlLoggedRequest.Controls(0).Name, "", "TRGT-LoggedRequest-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub _ctlLoggedRequestCol_evtRowClicked(ByVal vLoggedRequest As Object) Handles _ctlLoggedRequestCol.evtRowClicked 
    
    If vLoggedRequest Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pLoggedRequest As csLoggedRequest = CType(vLoggedRequest, csLoggedRequest) 
    _LoggedRequestID = pLoggedRequest.ID 
    
    If _ActiveControl Is _ctlLoggedRequestCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csLoggedRequestCol.enmFillSumOnTheFlyParameters.GroupByLoggedLoginID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedRequestCol.enmFillSumOnTheFlyParameters.GroupByUserID.ToString() Then 
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
    
    ChooseLoggedRequest() 
    
    Try 
      MyIntelliCombo.ValueSelect(_LoggedRequestID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pLoggedRequest.ID.ToString("#,##0")

 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseLoggedRequest() 
    _LoggedRequest = Nothing 
    lnkLoggedRequest.Visible = True 
  End Sub 
  Private Sub _ctlLoggedRequestCol_evtRowDoubleClicked(ByVal vLoggedRequest As csLoggedRequest, ByRef rHandled As Boolean) Handles _ctlLoggedRequestCol.evtRowDoubleClicked 
    If lnkLoggedRequest.Parent IsNot flpMenu Then Exit Sub 
    If vLoggedRequest Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csLoggedRequestCol.enmFillSumOnTheFlyParameters.GroupByLoggedLoginID.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedRequestCol.enmFillOnTheFlyParameters.LoggedLoginID) Then pSearchFilters.Remove(csLoggedRequestCol.enmFillOnTheFlyParameters.LoggedLoginID) 
        pSearchFilters.Add(csLoggedRequestCol.enmFillOnTheFlyParameters.LoggedLoginID, vLoggedRequest.LoggedLoginID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedRequestCol.enmFillSumOnTheFlyParameters.GroupByUserID.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedRequestCol.enmFillOnTheFlyParameters.UserID) Then pSearchFilters.Remove(csLoggedRequestCol.enmFillOnTheFlyParameters.UserID) 
        pSearchFilters.Add(csLoggedRequestCol.enmFillOnTheFlyParameters.UserID, vLoggedRequest.UserID) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreLoggedRequestCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vLoggedRequest.ID, vLoggedRequest.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _LoggedRequestID = vLoggedRequest.ID 
      'MyIntelliCombo.ValueSelect(_LoggedRequestID) 
      pFault = ActivateControl("ctlc_LoggedRequest") 
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
      pFault = _LoggedRequestCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _LoggedRequestCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedRequestCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedRequestCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_LoggedRequestCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csLoggedRequest.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedRequest" 
      pFault = _ctlLoggedRequestCol.LoadControl(_LoggedRequestCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlLoggedRequestCol_evtUnChosen() Handles _ctlLoggedRequestCol.evtUnChosen 
 
    _LoggedRequestID = 0 
    _LoggedRequest = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkLoggedRequest.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkLoggedRequestCol.Click, 
      lnkLoggedRequest.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkLoggedRequest OrElse (lnk Is lnkLoggedRequestCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlLoggedRequestCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_LoggedRequestCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csLoggedRequest.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csLoggedRequestCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillLoggedRequestCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _LoggedRequestCol = New csLoggedRequestCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _LoggedRequestCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlLoggedRequestCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _LoggedRequestCol.FillByUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=100, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then  
              _ctlLoggedRequestCol.Timer?.Stop()  
              Return pFault  
            End If  
          Case Else 
            If _ctlLoggedRequestCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _LoggedRequestCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlLoggedRequestCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _LoggedRequestCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _LoggedRequestCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedRequestCol.Count) 
      End If 
    Else 
      _LoggedRequestCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedRequestCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlLoggedRequestCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedRequest" 
    
    Dim pLoggedRequestID As Long = _LoggedRequestID 
    
    pFault = _ctlLoggedRequestCol.LoadControl(_LoggedRequestCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedRequestCol.Visible = True 
    
    _ctlLoggedRequestCol.Refresh() 
    If pLoggedRequestID <> 0 Then 
      Dim pLoggedRequestCol As csLoggedRequestCol = CType(_ctlLoggedRequestCol.bsCtlLoggedRequest.DataSource, csLoggedRequestCol) 
      Dim pLoggedRequest As csLoggedRequest = pLoggedRequestCol.FindByID(pLoggedRequestID) 
      If pLoggedRequest.ID > 0 Then 
        _ctlLoggedRequestCol.bsCtlLoggedRequest.CurrencyManager.Position = pLoggedRequestCol.IndexOf(pLoggedRequest) 
        _ctlLoggedRequestCol.dgvLoggedRequest.Rows(pLoggedRequestCol.IndexOf(pLoggedRequest)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlLoggedRequest() As clsFault 
    Dim pFault As New clsFault 
    
    If _LoggedRequestID > 0 Then 
      ChooseLoggedRequest() 
      _LoggedRequest = New csLoggedRequest(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedRequest.GetByID(_LoggedRequestID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _LoggedRequest = New csLoggedRequest(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _LoggedRequest.ID.ToString("#,##0")
    
     
    Dim pLoadParameters As New ctlc_LoggedRequest.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = True 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedRequest(pLoadParameters)
    pFault = _ctlLoggedRequest.LoadControl(_LoggedRequest, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlLoggedRequest.Visible = True 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Function LoadCboLoggedRequests(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.UD 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboLoggedRequest(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _LoggedRequestID >= 0 Then 
      MyIntelliCombo.ValueSelect(_LoggedRequestID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _LoggedRequestID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _LoggedRequestID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetLoggedRequestIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _LoggedRequestID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _LoggedRequestID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _LoggedRequestID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _LoggedRequestID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseLoggedRequest() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_LoggedRequest", StringComparison.OrdinalIgnoreCase) AndAlso _LoggedRequestID > 0 Then 
        'to avoid getting ObjectNotFound 
        _LoggedRequest = New csLoggedRequest(clsEnums.enmLoadParent.TextOnly) 
        pFault = _LoggedRequest.GetByID(_LoggedRequestID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_LoggedRequest") 
    End If 
    pnlLoggedRequest.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csLoggedRequest.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlLoggedRequest.evtParentChosen 
    If vParentName = csLoggedRequest.enmParentProperty.LoggedLogin Then 
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
    If vParentName = csLoggedRequest.enmParentProperty.User Then 
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
    pnlLoggedRequest.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkLoggedRequestCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _LoggedRequestID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_LoggedRequestCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkLoggedRequestCol.Visible = False 
      _ActiveControl = _ctlLoggedRequest 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboLoggedRequests(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _LoggedRequestID <> 0 Then 
        MyIntelliCombo.cbo.Text = _LoggedRequestID.ToString() 
        pFault = ActivateControl("ctlc_LoggedRequest") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlLoggedRequest.Visible = False 
        _LoggedRequestID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _LoggedRequestID > 0 Then pnlLoggedRequest.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkLoggedRequestCol.MouseEnter, 
                  lnkLoggedRequest.MouseEnter, 
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
                  lnkLoggedRequestCol.MouseLeave, 
                  lnkLoggedRequest.MouseLeave, 
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
    Dim pLoggedLoginID As Nullable(Of Long) = Nothing 
    Dim pUserID As Nullable(Of Long) = Nothing 
    If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.c_User Then 
      pUserID = _Requester.UserIdentityInstanceID 
    End If 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByLoggedLoginID As Boolean = False 
    Dim pGroupByUserID As Boolean = False 
    
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Logged Requests"  
  
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
        .Text01Label.Text = If(_ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedRequest.enmProperty.LoggedLogin), _ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText(csLoggedRequest.enmProperty.LoggedLogin), "Logged Login") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 3 
        .Text01From.Width = .String01Text.Width 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 4 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.SetFlowBreak(.Text01From, True) 
 
        If pUserID Is Nothing Then 
         .Combo01Label.Text = If(_ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedRequest.enmProperty.User), _ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText(csLoggedRequest.enmProperty.User), "User") 
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
           .TabIndex = 5 
         End With 
        End If 
 
        .Text02Label.Text = If(_ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedRequest.enmProperty.ID), _ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText(csLoggedRequest.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 6 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedRequest.enmProperty.LoggedLogin), _ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText(csLoggedRequest.enmProperty.LoggedLogin), "Logged Login") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedRequest.enmProperty.User), _ctlLoggedRequestCol.LoadParameters.ColumnsHeaderText(csLoggedRequest.enmProperty.User), "User") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 9 
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
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pLoggedLoginID = ccHelper.ToLong(.Text01From.Text) 
          _SearchFilters.Add(csLoggedRequestCol.enmFillOnTheFlyParameters.LoggedLoginID, pLoggedLoginID) 
        End If 
      End If 
      If pUserID Is Nothing Then 
       If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
         pUserID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
         _SearchFilters.Add(csLoggedRequestCol.enmFillOnTheFlyParameters.UserID, pUserID) 
       End If 
      Else 
        _SearchFilters.Add(csLoggedRequestCol.enmFillOnTheFlyParameters.UserID, pUserID) 
      End If  
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csLoggedRequestCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csLoggedRequestCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csLoggedRequestCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csLoggedRequestCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByLoggedLoginID = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedRequestCol.enmFillSumOnTheFlyParameters.GroupByLoggedLoginID, pGroupByLoggedLoginID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByUserID = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedRequestCol.enmFillSumOnTheFlyParameters.GroupByUserID, pGroupByUserID) 
      End If 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csLoggedRequestCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csLoggedRequestCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csLoggedRequestCol.enmListDefinition.Dir) Then _SearchFilters.Add(csLoggedRequestCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_LoggedRequestCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_LoggedRequestCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csLoggedRequest.enmProperty.ID, "ID") 
      End With 
      _LoggedRequestCol = New csLoggedRequestCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _LoggedRequestCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case clsEnums.enmUserIdentityType.c_User 
            pFault = _LoggedRequestCol.FillByUserID(_Requester.UserIdentityInstanceID, vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
          Case Else 
            pFault = _LoggedRequestCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _LoggedRequestCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedRequestCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedRequestCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedRequest" 
      RaiseEvent evtOverrideLoadCtlLoggedRequestCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _LoggedRequestCol = New csLoggedRequestCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedRequestCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_LoggedRequestCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _LoggedRequestCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csLoggedRequest.enmProperty.ID, "Count") 
        If pGroupByLoggedLoginID = False Then .ColumnsHide.Add(csLoggedRequest.enmProperty.LoggedLogin) 
        If pGroupByUserID = False Then .ColumnsHide.Add(csLoggedRequest.enmProperty.User) 
        .ColumnsHide.Add(csLoggedRequest.enmProperty.TimeAccessed) 
        .ColumnsHide.Add(csLoggedRequest.enmProperty.CallingFunctionWithinApplication) 
        .ColumnsHide.Add(csLoggedRequest.enmProperty.EntryPoint) 
        .ColumnsHide.Add(csLoggedRequest.enmProperty.Process) 
        .ColumnsHide.Add(csLoggedRequest.enmProperty.Thread) 
        .ColumnsHide.Add(csLoggedRequest.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlLoggedRequestCol.Visible = True 
    pFault = _ctlLoggedRequestCol.LoadControl(_LoggedRequestCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csLoggedRequestCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csLoggedRequestCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlLoggedRequest.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlLoggedRequest.Controls(0).Name) 
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
 
  Private Sub _ctlLoggedRequestCol_evtTimerTripped() Handles _ctlLoggedRequestCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtLoggedRequestTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlLoggedRequestCol.LoggedRequestCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlLoggedRequestCol.LoggedRequestCol(0).ID 
 
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
    If _LoggedRequestCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csLoggedRequest() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csLoggedRequestCol = CType(CallByName(_LoggedRequestCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedRequestCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csLoggedRequestCol = CType(CallByName(_LoggedRequestCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedRequestCol) 
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
                  ccHelper.GetPropertyTypeName(New csLoggedRequestCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csLoggedRequestCol = CType(CallByName(_LoggedRequestCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedRequestCol) 
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
 
  Private Sub cc_ctlPnlLoggedRequest_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
