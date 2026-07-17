Public Class ctlPnlc_LoggedJob 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlLoggedJobCol As ctlc_LoggedJobCol 
  Private WithEvents _ctlLoggedJob As ctlc_LoggedJob 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _LoggedJobID As Long 
 
  'The data holders 
  Private _LoggedJobCol As csLoggedJobCol 
  Private _LoggedJob As csLoggedJob 
 
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
  Public Event evtOverrideLoadCboLoggedJob(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetLoggedJobIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillLoggedJobCol(ByRef rLoggedJobCol As csLoggedJobCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlLoggedJobCol(ByRef rLoadParameters As ctlc_LoggedJobCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedJob(ByRef rLoadParameters As ctlc_LoggedJob.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreLoggedJobCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtLoggedJobTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  'Parents
  Private _CancelEvtJobChosen As Boolean = False 
  Private _ShowPopForEvtJobChosen As Boolean = False 
  Private _CancelEvtLoggedAlertChosen As Boolean = False 
  Private _ShowPopForEvtLoggedAlertChosen As Boolean = False 
  
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
 
    lnkLoggedJobCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedJob.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vLoggedJobID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _LoggedJobID = CType(vLoggedJobID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlLoggedJob.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkLoggedJobCol.Visible = False 
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
      pFault = LoadCboLoggedJobs(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _LoggedJobID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_LoggedJobID) 
      End If 
      ChooseLoggedJob() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_LoggedJob") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _LoggedJobID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _LoggedJobID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_LoggedJob" OrElse pControlName = "ctlLoggedJob" Then 
      lnkLoggedJob.ForeColor = Color.Black : lnkLoggedJob.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedJob.BackColor = Color.Wheat 
      If _ctlLoggedJob Is Nothing Then 
        _ctlLoggedJob = New ctlc_LoggedJob() 
        _ctlLoggedJob.Dock = DockStyle.Fill 
        pnlLoggedJob.Controls.Add(_ctlLoggedJob) 
        _ctlLoggedJob.Visible = False 
      End If 
      If _LoggedJobID = 0 Then 
        pnlLoggedJob.Visible = False 
      End If 
      'If _LoggedJob Is Nothing Then 
      pFault = RefreshCtlLoggedJob() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlLoggedJob.LoggedJob.IsEmpty AndAlso _LoggedJobID <> -2 Then 
        pnlLoggedJob.Visible = False 
      End If 
      _ctlLoggedJob.Name = "ctlc_LoggedJob" 
      _ActiveControl = _ctlLoggedJob 
      _ctlLoggedJob.BringToFront() 
      _ctlLoggedJob.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_LoggedJobCol" Then 
      lnkLoggedJobCol.ForeColor = Color.Black : lnkLoggedJobCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedJobCol.BackColor = Color.Wheat 
      If _ctlLoggedJobCol Is Nothing Then 
        _ctlLoggedJobCol = New ctlc_LoggedJobCol() 
        _ctlLoggedJobCol.Dock = DockStyle.Fill 
        pnlLoggedJob.Controls.Add(_ctlLoggedJobCol) 
        _ctlLoggedJobCol.Visible = False 
      End If  
      pnlLoggedJob.Visible = True 
      If _LoggedJobCol Is Nothing Then 
        pFault = RefreshCtlLoggedJobCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedJobCol.Name = "ctlc_LoggedJobCol" 
      _ActiveControl = _ctlLoggedJobCol 
      _ctlLoggedJobCol.BringToFront() 
      _ctlLoggedJobCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-LoggedJob-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("LoggedJob", _Requester) 
 
    lnkLoggedJobCol.Text = CCTextTranslate("List", _Requester) 
    lnkLoggedJob.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlLoggedJob.Controls(0) Is _ctlLoggedJob Then 
      If _LoggedJobID = 0 Then 
        pnlLoggedJob.Visible = False 
      End If 
    ElseIf pnlLoggedJob.Controls(0) Is _ctlLoggedJobCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pLoggedJobID As Long = _LoggedJobID 
      If ccHelper.IsNumeric(pText) Then _LoggedJobID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetLoggedJobIDFromIntelliComboText(pText) 
      If pLoggedJobID <> _LoggedJobID Then 
        _LoggedJob = Nothing 
        pFault = ActivateControl("ctlc_LoggedJob") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlLoggedJob.Controls(0) Is _ctlLoggedJob Then 
      pFault = RefreshCtlLoggedJob() 
    ElseIf pnlLoggedJob.Controls(0) Is _ctlLoggedJobCol Then 
      pFault = RefreshCtlLoggedJobCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlLoggedJob.Controls(0).Name, "", "TRGT-LoggedJob-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub _ctlLoggedJobCol_evtRowClicked(ByVal vLoggedJob As Object) Handles _ctlLoggedJobCol.evtRowClicked 
    
    If vLoggedJob Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pLoggedJob As csLoggedJob = CType(vLoggedJob, csLoggedJob) 
    _LoggedJobID = pLoggedJob.ID 
    
    If _ActiveControl Is _ctlLoggedJobCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csLoggedJobCol.enmFillSumOnTheFlyParameters.GroupByJobID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csLoggedJobCol.enmFillSumOnTheFlyParameters.GroupByLoggedAlertID.ToString() Then 
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
    
    ChooseLoggedJob() 
    
    Try 
      MyIntelliCombo.ValueSelect(_LoggedJobID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pLoggedJob.ID.ToString("#,##0")

 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseLoggedJob() 
    _LoggedJob = Nothing 
    lnkLoggedJob.Visible = True 
  End Sub 
  Private Sub _ctlLoggedJobCol_evtRowDoubleClicked(ByVal vLoggedJob As csLoggedJob, ByRef rHandled As Boolean) Handles _ctlLoggedJobCol.evtRowDoubleClicked 
    If lnkLoggedJob.Parent IsNot flpMenu Then Exit Sub 
    If vLoggedJob Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csLoggedJobCol.enmFillSumOnTheFlyParameters.GroupByJobID.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedJobCol.enmFillOnTheFlyParameters.JobID) Then pSearchFilters.Remove(csLoggedJobCol.enmFillOnTheFlyParameters.JobID) 
        pSearchFilters.Add(csLoggedJobCol.enmFillOnTheFlyParameters.JobID, vLoggedJob.JobID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csLoggedJobCol.enmFillSumOnTheFlyParameters.GroupByLoggedAlertID.ToString() Then 
        If pSearchFilters.ContainsKey(csLoggedJobCol.enmFillOnTheFlyParameters.LoggedAlertID) Then pSearchFilters.Remove(csLoggedJobCol.enmFillOnTheFlyParameters.LoggedAlertID) 
        pSearchFilters.Add(csLoggedJobCol.enmFillOnTheFlyParameters.LoggedAlertID, vLoggedJob.LoggedAlertID) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreLoggedJobCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vLoggedJob.ID, vLoggedJob.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _LoggedJobID = vLoggedJob.ID 
      'MyIntelliCombo.ValueSelect(_LoggedJobID) 
      pFault = ActivateControl("ctlc_LoggedJob") 
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
      pFault = _LoggedJobCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _LoggedJobCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedJobCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedJobCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_LoggedJobCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csLoggedJob.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedJob" 
      pFault = _ctlLoggedJobCol.LoadControl(_LoggedJobCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlLoggedJobCol_evtUnChosen() Handles _ctlLoggedJobCol.evtUnChosen 
 
    _LoggedJobID = 0 
    _LoggedJob = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkLoggedJob.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkLoggedJobCol.Click, 
      lnkLoggedJob.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkLoggedJob OrElse (lnk Is lnkLoggedJobCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlLoggedJobCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_LoggedJobCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csLoggedJob.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csLoggedJobCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillLoggedJobCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _LoggedJobCol = New csLoggedJobCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _LoggedJobCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlLoggedJobCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlLoggedJobCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _LoggedJobCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlLoggedJobCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _LoggedJobCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _LoggedJobCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedJobCol.Count) 
      End If 
    Else 
      _LoggedJobCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedJobCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlLoggedJobCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedJob" 
    
    Dim pLoggedJobID As Long = _LoggedJobID 
    
    pFault = _ctlLoggedJobCol.LoadControl(_LoggedJobCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedJobCol.Visible = True 
    
    _ctlLoggedJobCol.Refresh() 
    If pLoggedJobID <> 0 Then 
      Dim pLoggedJobCol As csLoggedJobCol = CType(_ctlLoggedJobCol.bsCtlLoggedJob.DataSource, csLoggedJobCol) 
      Dim pLoggedJob As csLoggedJob = pLoggedJobCol.FindByID(pLoggedJobID) 
      If pLoggedJob.ID > 0 Then 
        _ctlLoggedJobCol.bsCtlLoggedJob.CurrencyManager.Position = pLoggedJobCol.IndexOf(pLoggedJob) 
        _ctlLoggedJobCol.dgvLoggedJob.Rows(pLoggedJobCol.IndexOf(pLoggedJob)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlLoggedJob() As clsFault 
    Dim pFault As New clsFault 
    
    If _LoggedJobID > 0 Then 
      ChooseLoggedJob() 
      _LoggedJob = New csLoggedJob(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedJob.GetByID(_LoggedJobID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _LoggedJob = New csLoggedJob(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _LoggedJob.ID.ToString("#,##0")
    
     
    Dim pLoadParameters As New ctlc_LoggedJob.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = True 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedJob(pLoadParameters)
    pFault = _ctlLoggedJob.LoadControl(_LoggedJob, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlLoggedJob.Visible = True 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Function LoadCboLoggedJobs(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.UD 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboLoggedJob(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _LoggedJobID >= 0 Then 
      MyIntelliCombo.ValueSelect(_LoggedJobID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _LoggedJobID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _LoggedJobID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetLoggedJobIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _LoggedJobID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _LoggedJobID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _LoggedJobID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _LoggedJobID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseLoggedJob() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_LoggedJob", StringComparison.OrdinalIgnoreCase) AndAlso _LoggedJobID > 0 Then 
        'to avoid getting ObjectNotFound 
        _LoggedJob = New csLoggedJob(clsEnums.enmLoadParent.TextOnly) 
        pFault = _LoggedJob.GetByID(_LoggedJobID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_LoggedJob") 
    End If 
    pnlLoggedJob.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csLoggedJob.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlLoggedJob.evtParentChosen 
    If vParentName = csLoggedJob.enmParentProperty.Job Then 
      rHandled = True 
      If _CancelEvtJobChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New csJob 
      End With 
      Try 
        RaiseEvent evtEntityChosen(Me, pEventArgs) 
      Catch ex As Exception 
        rHandled = False 
      End Try 
    End If 
    If vParentName = csLoggedJob.enmParentProperty.LoggedAlert Then 
      rHandled = True 
      If _CancelEvtLoggedAlertChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New csLoggedAlert 
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
    pnlLoggedJob.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkLoggedJobCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _LoggedJobID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_LoggedJobCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkLoggedJobCol.Visible = False 
      _ActiveControl = _ctlLoggedJob 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboLoggedJobs(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _LoggedJobID <> 0 Then 
        MyIntelliCombo.cbo.Text = _LoggedJobID.ToString() 
        pFault = ActivateControl("ctlc_LoggedJob") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlLoggedJob.Visible = False 
        _LoggedJobID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _LoggedJobID > 0 Then pnlLoggedJob.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkLoggedJobCol.MouseEnter, 
                  lnkLoggedJob.MouseEnter, 
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
                  lnkLoggedJob.MouseLeave, 
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
    Dim pJobID As Nullable(Of Long) = Nothing 
    Dim pLoggedAlertID As Nullable(Of Long) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByJobID As Boolean = False 
    Dim pGroupByLoggedAlertID As Boolean = False 
    
    Dim pSumExecutionTimeSec As Boolean = False 
    Dim pSumSuccessCount As Boolean = False 
    Dim pSumFailureCount As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Logged Jobs"  
  
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
        .Combo01Label.Text = If(_ctlLoggedJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedJob.enmProperty.Job), _ctlLoggedJobCol.LoadParameters.ColumnsHeaderText(csLoggedJob.enmProperty.Job), "Job") 
        Dim pJobs As New clsComboList 
        pFault = MyCache.GetComboList(clsEnums.enmComboListType.c_JobDefaultByID, pJobs) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
        'If pJobs IsNot Nothing AndAlso pJobs.Count > 0 Then 
        .flpFilter.Controls.Add(.Combo01Label) 
        .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        'End If 
        With .Combo01 
          .MakeSmart() 
          If pJobs IsNot Nothing Then 
            .LoadControl(pJobs, GetChoose(_Requester)) 
          Else 
            .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.c_JobDefaultByID, 0, _Requester) 
          End If 
          .TabIndex = 3 
        End With 
 
        .Text01Label.Text = If(_ctlLoggedJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedJob.enmProperty.LoggedAlert), _ctlLoggedJobCol.LoadParameters.ColumnsHeaderText(csLoggedJob.enmProperty.LoggedAlert), "Logged Alert") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 4 
        .Text01From.Width = .String01Text.Width 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 5 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.SetFlowBreak(.Text01From, True) 
 
        .Text02Label.Text = If(_ctlLoggedJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedJob.enmProperty.ID), _ctlLoggedJobCol.LoadParameters.ColumnsHeaderText(csLoggedJob.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 6 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlLoggedJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedJob.enmProperty.Job), _ctlLoggedJobCol.LoadParameters.ColumnsHeaderText(csLoggedJob.enmProperty.Job), "Job") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlLoggedJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedJob.enmProperty.LoggedAlert), _ctlLoggedJobCol.LoadParameters.ColumnsHeaderText(csLoggedJob.enmProperty.LoggedAlert), "Logged Alert") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlLoggedJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedJob.enmProperty.ExecutionTimeSec), _ctlLoggedJobCol.LoadParameters.ColumnsHeaderText(csLoggedJob.enmProperty.ExecutionTimeSec), "Execution Time Sec") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 10 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlLoggedJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedJob.enmProperty.SuccessCount), _ctlLoggedJobCol.LoadParameters.ColumnsHeaderText(csLoggedJob.enmProperty.SuccessCount), "Success Count") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 11 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
        .lblSumField03.Text = If(_ctlLoggedJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csLoggedJob.enmProperty.FailureCount), _ctlLoggedJobCol.LoadParameters.ColumnsHeaderText(csLoggedJob.enmProperty.FailureCount), "Failure Count") 
        .chkSumField03.Checked = False 
        .chkSumField03.TabIndex = 12 
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
      If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
        pJobID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
        _SearchFilters.Add(csLoggedJobCol.enmFillOnTheFlyParameters.JobID, pJobID) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pLoggedAlertID = ccHelper.ToLong(.Text01From.Text) 
          _SearchFilters.Add(csLoggedJobCol.enmFillOnTheFlyParameters.LoggedAlertID, pLoggedAlertID) 
        End If 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csLoggedJobCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csLoggedJobCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csLoggedJobCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csLoggedJobCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByJobID = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedJobCol.enmFillSumOnTheFlyParameters.GroupByJobID, pGroupByJobID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByLoggedAlertID = True 
        pDoSum = True 
        _SearchFilters.Add(csLoggedJobCol.enmFillSumOnTheFlyParameters.GroupByLoggedAlertID, pGroupByLoggedAlertID) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumExecutionTimeSec = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumSuccessCount = True 
        pDoSum = True 
      End If 
      
      If .chkSumField03.Checked = True Then 
        pSumFailureCount = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csLoggedJobCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csLoggedJobCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csLoggedJobCol.enmListDefinition.Dir) Then _SearchFilters.Add(csLoggedJobCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_LoggedJobCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_LoggedJobCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csLoggedJob.enmProperty.ID, "ID") 
      End With 
      _LoggedJobCol = New csLoggedJobCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _LoggedJobCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _LoggedJobCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _LoggedJobCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedJobCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedJobCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see LoggedJob" 
      RaiseEvent evtOverrideLoadCtlLoggedJobCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _LoggedJobCol = New csLoggedJobCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedJobCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_LoggedJobCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _LoggedJobCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csLoggedJob.enmProperty.ID, "Count") 
        If pGroupByJobID = False Then .ColumnsHide.Add(csLoggedJob.enmProperty.Job) 
        If pGroupByLoggedAlertID = False Then .ColumnsHide.Add(csLoggedJob.enmProperty.LoggedAlert) 
        If pSumExecutionTimeSec = False Then .ColumnsHide.Add(csLoggedJob.enmProperty.ExecutionTimeSec) 
        If pSumSuccessCount = False Then .ColumnsHide.Add(csLoggedJob.enmProperty.SuccessCount) 
        If pSumFailureCount = False Then .ColumnsHide.Add(csLoggedJob.enmProperty.FailureCount) 
        .ColumnsHide.Add(csLoggedJob.enmProperty.WhenStarted) 
        .ColumnsHide.Add(csLoggedJob.enmProperty.ActivatingUser) 
        .ColumnsHide.Add(csLoggedJob.enmProperty.LastRunBy) 
        .ColumnsHide.Add(csLoggedJob.enmProperty.RunStatus) 
        .ColumnsHide.Add(csLoggedJob.enmProperty.Remarks) 
        .ColumnsHide.Add(csLoggedJob.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlLoggedJobCol.Visible = True 
    pFault = _ctlLoggedJobCol.LoadControl(_LoggedJobCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csLoggedJobCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csLoggedJobCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlLoggedJob.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlLoggedJob.Controls(0).Name) 
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
 
  Private Sub _ctlLoggedJobCol_evtTimerTripped() Handles _ctlLoggedJobCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtLoggedJobTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlLoggedJobCol.LoggedJobCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlLoggedJobCol.LoggedJobCol(0).ID 
 
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
    If _LoggedJobCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csLoggedJob() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csLoggedJobCol = CType(CallByName(_LoggedJobCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedJobCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csLoggedJobCol = CType(CallByName(_LoggedJobCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedJobCol) 
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
                  ccHelper.GetPropertyTypeName(New csLoggedJobCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csLoggedJobCol = CType(CallByName(_LoggedJobCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csLoggedJobCol) 
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
 
  Private Sub cc_ctlPnlLoggedJob_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
