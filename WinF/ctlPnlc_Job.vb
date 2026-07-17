Public Class ctlPnlc_Job 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlJobCol As ctlc_JobCol 
  Private WithEvents _ctlJob As ctlc_Job 
  Private WithEvents _ctlJobAlertRecipientCol As ctlc_JobAlertRecipientCol 
  Private WithEvents _ctlLoggedJobCol As ctlc_LoggedJobCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _JobID As Long 
 
  'The data holders 
  Private _JobCol As csJobCol 
  Private _Job As csJob 
  Private _JobAlertRecipientCol As csJobAlertRecipientCol 
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
  Public Event evtOverrideLoadCboJob(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetJobIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillJobCol(ByRef rJobCol As csJobCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillJobAlertRecipientCol(ByRef rJobAlertRecipientCol As csJobAlertRecipientCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillLoggedJobCol(ByRef rLoggedJobCol As csLoggedJobCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlJobCol(ByRef rLoadParameters As ctlc_JobCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlJob(ByRef rLoadParameters As ctlc_Job.clsLoadParameters) 
  Private Event evtOverrideLoadCtlJobAlertRecipientCol(ByRef rLoadParameters As ctlc_JobAlertRecipientCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedJobCol(ByRef rLoadParameters As ctlc_LoggedJobCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreJobCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtJobTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtJobAlertRecipientChosen As Boolean = False 
  Private _ShowPopForEvtJobAlertRecipientChosen As Boolean = False 
  Private _CancelEvtLoggedJobChosen As Boolean = False 
  Private _ShowPopForEvtLoggedJobChosen As Boolean = False 
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
 
    lnkJobCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkJob.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkJobAlertRecipientCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedJobCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _JobID = CType(vJobID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlJob.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkJobCol.Visible = False 
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
      pFault = LoadCboJobs(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _JobID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_JobID) 
      End If 
      ChooseJob() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_Job") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _JobID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlc_Job" OrElse pControlName = "ctlJob" Then 
      lnkJob.ForeColor = Color.Black : lnkJob.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkJob.BackColor = Color.Wheat 
      If _ctlJob Is Nothing Then 
        _ctlJob = New ctlc_Job() 
        _ctlJob.Dock = DockStyle.Fill 
        _ctlJob.Controls.RemoveByKey("btnAdd") 
        pnlJob.Controls.Add(_ctlJob) 
        _ctlJob.Visible = False 
      End If 
      If _JobID = 0 Then 
        pnlJob.Visible = False 
      End If 
      'If _Job Is Nothing Then 
      pFault = RefreshCtlJob() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlJob.Job.IsEmpty AndAlso _JobID <> -2 Then 
        pnlJob.Visible = False 
      End If 
      _ctlJob.Name = "ctlc_Job" 
      _ActiveControl = _ctlJob 
      _ctlJob.BringToFront() 
      _ctlJob.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_JobCol" Then 
      lnkJobCol.ForeColor = Color.Black : lnkJobCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkJobCol.BackColor = Color.Wheat 
      If _ctlJobCol Is Nothing Then 
        _ctlJobCol = New ctlc_JobCol() 
        _ctlJobCol.Dock = DockStyle.Fill 
        pnlJob.Controls.Add(_ctlJobCol) 
        _ctlJobCol.Visible = False 
      End If  
      pnlJob.Visible = True 
      If _JobCol Is Nothing Then 
        pFault = RefreshCtlJobCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlJobCol.Name = "ctlc_JobCol" 
      _ActiveControl = _ctlJobCol 
      _ctlJobCol.BringToFront() 
      _ctlJobCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlc_JobAlertRecipientCol" Then 
      lnkJobAlertRecipientCol.ForeColor = Color.Black : lnkJobAlertRecipientCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkJobAlertRecipientCol.BackColor = Color.Wheat 
      If _ctlJobAlertRecipientCol Is Nothing Then 
      _ctlJobAlertRecipientCol = New ctlc_JobAlertRecipientCol() 
      _ctlJobAlertRecipientCol.Dock = DockStyle.Fill 
      pnlJob.Controls.Add(_ctlJobAlertRecipientCol) 
      _ctlJobAlertRecipientCol.Visible = False 
      End If  
      If _JobAlertRecipientCol Is Nothing Then 
        pFault = RefreshCtlJobAlertRecipientCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlJobAlertRecipientCol.Name = "ctlc_JobAlertRecipientCol" 
      _ActiveControl = _ctlJobAlertRecipientCol 
      _ctlJobAlertRecipientCol.BringToFront() 
      _ctlJobAlertRecipientCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_LoggedJobCol" Then 
      lnkLoggedJobCol.ForeColor = Color.Black : lnkLoggedJobCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedJobCol.BackColor = Color.Wheat 
      If _ctlLoggedJobCol Is Nothing Then 
      _ctlLoggedJobCol = New ctlc_LoggedJobCol() 
      _ctlLoggedJobCol.Dock = DockStyle.Fill 
      pnlJob.Controls.Add(_ctlLoggedJobCol) 
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
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-Job-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("Job", _Requester) 
 
    lnkJobCol.Text = CCTextTranslate("List", _Requester) 
    lnkJob.Text = CCTextTranslate("Details", _Requester) 
 
    lnkJobAlertRecipientCol.Text = TableNameTranslate("JobAlertRecipient", _Requester, vMakePlural:=True) 
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
    If pnlJob.Controls(0) Is _ctlJob Then 
      If _JobID = 0 Then 
        pnlJob.Visible = False 
      End If 
    ElseIf pnlJob.Controls(0) Is _ctlJobCol Then 
    ElseIf pnlJob.Controls(0) Is _ctlJobAlertRecipientCol Then 
    ElseIf pnlJob.Controls(0) Is _ctlLoggedJobCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pJobID As Long = _JobID 
      If ccHelper.IsNumeric(pText) Then _JobID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetJobIDFromIntelliComboText(pText) 
      If pJobID <> _JobID Then 
        _Job = Nothing 
        pFault = ActivateControl("ctlc_Job") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlJob.Controls(0) Is _ctlJob Then 
      pFault = RefreshCtlJob() 
    ElseIf pnlJob.Controls(0) Is _ctlJobCol Then 
      pFault = RefreshCtlJobCol() 
    ElseIf pnlJob.Controls(0) Is _ctlJobAlertRecipientCol Then 
      pFault = RefreshCtlJobAlertRecipientCol() 
    ElseIf pnlJob.Controls(0) Is _ctlLoggedJobCol Then 
      pFault = RefreshCtlLoggedJobCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlJob.Controls(0).Name, "", "TRGT-Job-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboJobs(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlJobCol_evtRowClicked(ByVal vJob As Object) Handles _ctlJobCol.evtRowClicked 
    
    If vJob Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pJob As csJob = CType(vJob, csJob) 
    _JobID = pJob.ID 
    
    If _ActiveControl Is _ctlJobCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csJobCol.enmFillSumOnTheFlyParameters.GroupByJobCode.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csJobCol.enmFillSumOnTheFlyParameters.GroupByJobRunnerCode.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csJobCol.enmFillSumOnTheFlyParameters.GroupByActive.ToString() Then 
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
    
    ChooseJob() 
    
    Try 
      MyIntelliCombo.ValueSelect(_JobID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pJob.JobCode & " " & pJob.JobRunnerCode
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseJob() 
    _Job = Nothing 
    lnkJob.Visible = True 
    _JobAlertRecipientCol = Nothing 
    lnkJobAlertRecipientCol.Visible = True 
    _LoggedJobCol = Nothing 
    lnkLoggedJobCol.Visible = True 
  End Sub 
  Private Sub _ctlJobCol_evtRowDoubleClicked(ByVal vJob As csJob, ByRef rHandled As Boolean) Handles _ctlJobCol.evtRowDoubleClicked 
    If lnkJob.Parent IsNot flpMenu Then Exit Sub 
    If vJob Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csJobCol.enmFillSumOnTheFlyParameters.GroupByJobCode.ToString() Then 
        If pSearchFilters.ContainsKey(csJobCol.enmFillOnTheFlyParameters.JobCode) Then pSearchFilters.Remove(csJobCol.enmFillOnTheFlyParameters.JobCode) 
        pSearchFilters.Add(csJobCol.enmFillOnTheFlyParameters.JobCode, vJob.JobCode) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csJobCol.enmFillSumOnTheFlyParameters.GroupByJobRunnerCode.ToString() Then 
        If pSearchFilters.ContainsKey(csJobCol.enmFillOnTheFlyParameters.JobRunnerCode) Then pSearchFilters.Remove(csJobCol.enmFillOnTheFlyParameters.JobRunnerCode) 
        pSearchFilters.Add(csJobCol.enmFillOnTheFlyParameters.JobRunnerCode, vJob.JobRunnerCode) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csJobCol.enmFillSumOnTheFlyParameters.GroupByActive.ToString() Then 
        If pSearchFilters.ContainsKey(csJobCol.enmFillOnTheFlyParameters.Active) Then pSearchFilters.Remove(csJobCol.enmFillOnTheFlyParameters.Active) 
        pSearchFilters.Add(csJobCol.enmFillOnTheFlyParameters.Active, vJob.Active) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreJobCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vJob.ID, vJob.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _JobID = vJob.ID 
      'MyIntelliCombo.ValueSelect(_JobID) 
      pFault = ActivateControl("ctlc_Job") 
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
      pFault = _JobCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _JobCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _JobCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _JobCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_JobCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csJob.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Job" 
      pFault = _ctlJobCol.LoadControl(_JobCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlJobCol_evtUnChosen() Handles _ctlJobCol.evtUnChosen 
 
    _JobID = 0 
    _Job = Nothing 
    _JobAlertRecipientCol = Nothing 
    lnkJobAlertRecipientCol.Visible = False 
    _LoggedJobCol = Nothing 
    lnkLoggedJobCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkJob.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkJobAlertRecipientCol.Click, 
      lnkLoggedJobCol.Click, 
      lnkJobCol.Click, 
      lnkJob.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkJob OrElse (lnk Is lnkJobCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlJobCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_JobCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csJob.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csJobCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillJobCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _JobCol = New csJobCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _JobCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlJobCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlJobCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _JobCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlJobCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _JobCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _JobCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _JobCol.Count) 
      End If 
    Else 
      _JobCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _JobCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlJobCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Job" 
    
    Dim pJobID As Long = _JobID 
    
    pFault = _ctlJobCol.LoadControl(_JobCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlJobCol.Visible = True 
    
    _ctlJobCol.Refresh() 
    If pJobID <> 0 Then 
      Dim pJobCol As csJobCol = CType(_ctlJobCol.bsCtlJob.DataSource, csJobCol) 
      Dim pJob As csJob = pJobCol.FindByID(pJobID) 
      If pJob.ID > 0 Then 
        _ctlJobCol.bsCtlJob.CurrencyManager.Position = pJobCol.IndexOf(pJob) 
        _ctlJobCol.dgvJob.Rows(pJobCol.IndexOf(pJob)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlJob() As clsFault 
    Dim pFault As New clsFault 
    
    If _JobID > 0 Then 
      ChooseJob() 
      _Job = New csJob() 
      pFault = _Job.GetByID(_JobID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _Job = New csJob() 
    End If 
    'lblSecondaryTitle.Text = _Job.JobCode & " " & _Job.JobRunnerCode    
     
    Dim pLoadParameters As New ctlc_Job.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlJob(pLoadParameters)
    pFault = _ctlJob.LoadControl(_Job, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlJob.Visible = True 
    If _JobID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlJob.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlJob.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlJobAlertRecipientCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlJobAlertRecipientCol.dgvJobAlertRecipient.SelectedRows.Count > 0 Then 
      Dim pJobAlertRecipient As csJobAlertRecipient = CType(_ctlJobAlertRecipientCol.bsCtlJobAlertRecipient.Current, csJobAlertRecipient) 
      pID = pJobAlertRecipient.ID 
    End If 
 
    Dim pTestCol As csJobAlertRecipientCol = Nothing 
    RaiseEvent evtOverrideFillJobAlertRecipientCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _JobAlertRecipientCol = New csJobAlertRecipientCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _JobAlertRecipientCol.FillByJobID(_JobID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _JobAlertRecipientCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _JobAlertRecipientCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _JobAlertRecipientCol.Count) 
      End If 
    Else 
      _JobAlertRecipientCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _JobAlertRecipientCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_JobAlertRecipientCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _Job IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Job.DefaultDesignation) Then 
        .ReportTitle = "List of JobAlertRecipients for " & _Job.DefaultDesignation 
      Else 
        .ReportTitle = "List of JobAlertRecipients for Job" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csJobAlertRecipient.enmProperty.Job) 
    End With 
    RaiseEvent evtOverrideLoadCtlJobAlertRecipientCol(pLoadParameters)
    
    pFault = _ctlJobAlertRecipientCol.LoadControl(_JobAlertRecipientCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlJobAlertRecipientCol.Visible = True 
 
    If pID > 0 Then 
      Dim pJobAlertRecipients As csJobAlertRecipientCol = CType(_ctlJobAlertRecipientCol.bsCtlJobAlertRecipient.DataSource, csJobAlertRecipientCol) 
      Dim pJobAlertRecipient As csJobAlertRecipient = pJobAlertRecipients.FindByID((pID)) 
      If pJobAlertRecipient.ID > 0 Then 
        _ctlJobAlertRecipientCol.bsCtlJobAlertRecipient.CurrencyManager.Position = pJobAlertRecipients.IndexOf(pJobAlertRecipient) 
        _ctlJobAlertRecipientCol.dgvJobAlertRecipient.Rows(pJobAlertRecipients.IndexOf(pJobAlertRecipient)).Selected = True 
      End If 
    End If 
 
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
      pFault = _LoggedJobCol.FillByJobID(_JobID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
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
      If _Job IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Job.DefaultDesignation) Then 
        .ReportTitle = "List of LoggedJobs for " & _Job.DefaultDesignation 
      Else 
        .ReportTitle = "List of LoggedJobs for Job" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csLoggedJob.enmProperty.Job) 
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
  Private Sub _ctlJobAlertRecipientCol_evtBeforeUpdate(ByVal vJobAlertRecipient As csJobAlertRecipient, ByRef rCancel As Boolean) Handles _ctlJobAlertRecipientCol.evtBeforeUpdate 
    vJobAlertRecipient.JobID = _Job.ID 
  End Sub 
  Private Sub _ctlJob_evtDeleted(ByVal vJobID As Long) Handles _ctlJob.evtDeleted 
    _JobCol = Nothing 
    Dim pFault As clsFault 
    _JobID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboJobs(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlJob() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlJob.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkJobCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlJob_evtCancelledEdit(ByVal vJob As csJob) Handles _ctlJob.evtCancelledEdit 
    RefreshCtlJob() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboJobs(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlJob.btnAdd.Visible = False 
      If _JobID = 0 OrElse _JobID = -2 Then 
        pnlJob.Visible = False 
      Else 
        pnlJob.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlJob.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_JobCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlJob_evtUpdated(ByVal vWhichProperty As csJob.enmUpdateType, ByVal vJob As csJob) Handles _ctlJob.evtUpdated 
    _JobCol = Nothing 
    Dim pFault As clsFault 
    _JobID = CType(vJob, csJob).ID 
    If _ActiveControl.Name = "ctlc_Job" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboJobs(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlJob() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlJob.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboJobs(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_JobDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboJob(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _JobID >= 0 Then 
      MyIntelliCombo.ValueSelect(_JobID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_JobUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _JobID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _JobID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetJobIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _JobID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _JobID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _JobID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _JobID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseJob() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_Job", StringComparison.OrdinalIgnoreCase) AndAlso _JobID > 0 Then 
        'to avoid getting ObjectNotFound 
        _Job = New csJob() 
        pFault = _Job.GetByID(_JobID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_Job") 
    End If 
    pnlJob.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  Private Sub _ctlJobAlertRecipientCol_evtRowDoubleClicked(ByVal vJobAlertRecipient As csJobAlertRecipient, ByRef rHandled As Boolean) Handles _ctlJobAlertRecipientCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtJobAlertRecipientChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtJobAlertRecipientChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vJobAlertRecipient.ID 
      .Object = New csJobAlertRecipient 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
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
  
   
  Private Sub chkGrid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrid.CheckedChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    
    Cursor = Cursors.WaitCursor 
    chkGrid.Enabled = False 
    pnlButtons.Visible = False 
    pnlJob.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkJobCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _JobID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_JobCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkJobCol.Visible = False 
      _ActiveControl = _ctlJob 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboJobs(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _JobID <> 0 Then 
        pFault = ActivateControl("ctlc_Job") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlJob.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlJob.Visible = False 
        _JobID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _JobID > 0 Then pnlJob.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkJobAlertRecipientCol.MouseEnter, 
                  lnkLoggedJobCol.MouseEnter, 
                  lnkJobCol.MouseEnter, 
                  lnkJob.MouseEnter, 
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
                  lnkJobAlertRecipientCol.MouseLeave, 
                  lnkLoggedJobCol.MouseLeave, 
                  lnkJobCol.MouseLeave, 
                  lnkJob.MouseLeave, 
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
  Private Sub _ctlJob_evtAdd(ByVal vJob As csJob) Handles _ctlJob.evtAdd 
    lnkJobAlertRecipientCol.Visible = False 
    lnkLoggedJobCol.Visible = False 
    lnkJobCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pJobCode As String = Nothing 
    Dim pJobRunnerCode As String = Nothing 
    Dim pActive As Nullable(Of Boolean) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByJobCode As Boolean = False 
    Dim pGroupByJobRunnerCode As Boolean = False 
    Dim pGroupByActive As Boolean = False 
    
    Dim pSumCyclicCount As Boolean = False 
    Dim pSumTimeOutSec As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Jobs"  
  
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
        .Combo01Label.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.Job), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.Job), "Job") 
        Dim pJobCodes As New clsComboList 
        pFault = pJobCodes.FillLookup(clsEnums.enmLookup.Job, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pJobCodes.SortByText() 
        If pJobCodes IsNot Nothing AndAlso pJobCodes.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo01Label) 
          .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo01 
          .MakeSmart() 
          .LoadControl(pJobCodes, GetChoose(_Requester)) 
          .TabIndex = 3 
        End With 
 
        .Combo02Label.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.JobRunner), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.JobRunner), "Job Runner") 
        Dim pJobRunnerCodes As New clsComboList 
        pFault = pJobRunnerCodes.FillLookup(clsEnums.enmLookup.JobRunner, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pJobRunnerCodes.SortByText() 
        If pJobRunnerCodes IsNot Nothing AndAlso pJobRunnerCodes.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo02Label) 
          .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo02 
          .MakeSmart() 
          .LoadControl(pJobRunnerCodes, GetChoose(_Requester)) 
          .TabIndex = 4 
        End With 
 
        .Check01Label.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.Active), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.Active), "Active") 
        .Check01.CheckState = CheckState.Indeterminate 
        .Check01.TabIndex = 5 
        .flpFilter.Controls.Add(.Check01Label) 
        .flpFilter.Controls.Add(.Check01) 
 
        .Text01Label.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.ID), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 6 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.Job), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.Job), "Job") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 8 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.JobRunner), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.JobRunner), "Job Runner") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.Active), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.Active), "Active") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 10 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.CyclicCount), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.CyclicCount), "Cyclic Count") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 11 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlJobCol.LoadParameters.ColumnsHeaderText.ContainsKey(csJob.enmProperty.TimeOutSec), _ctlJobCol.LoadParameters.ColumnsHeaderText(csJob.enmProperty.TimeOutSec), "Time Out Sec") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 12 
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
      If .Combo01.SelectedItem IsNot Nothing Then 
        pJobCode = CType(.Combo01.SelectedItem, clsComboListMember).KeyString 
        _SearchFilters.Add(csJobCol.enmFillOnTheFlyParameters.JobCode, pJobCode) 
      End If 
      If .Combo02.SelectedItem IsNot Nothing Then 
        pJobRunnerCode = CType(.Combo02.SelectedItem, clsComboListMember).KeyString 
        _SearchFilters.Add(csJobCol.enmFillOnTheFlyParameters.JobRunnerCode, pJobRunnerCode) 
      End If 
      If .Check01.CheckState <> CheckState.Indeterminate Then 
        pActive = .Check01.Checked 
        _SearchFilters.Add(csJobCol.enmFillOnTheFlyParameters.Active, pActive) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csJobCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csJobCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csJobCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csJobCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByJobCode = True 
        pDoSum = True 
        _SearchFilters.Add(csJobCol.enmFillSumOnTheFlyParameters.GroupByJobCode, pGroupByJobCode) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByJobRunnerCode = True 
        pDoSum = True 
        _SearchFilters.Add(csJobCol.enmFillSumOnTheFlyParameters.GroupByJobRunnerCode, pGroupByJobRunnerCode) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByActive = True 
        pDoSum = True 
        _SearchFilters.Add(csJobCol.enmFillSumOnTheFlyParameters.GroupByActive, pGroupByActive) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumCyclicCount = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumTimeOutSec = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csJobCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csJobCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csJobCol.enmListDefinition.Dir) Then _SearchFilters.Add(csJobCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_JobCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_JobCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csJob.enmProperty.ID, "ID") 
      End With 
      _JobCol = New csJobCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _JobCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _JobCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _JobCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _JobCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _JobCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Job" 
      RaiseEvent evtOverrideLoadCtlJobCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _JobCol = New csJobCol() 
      pFault = _JobCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_JobCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _JobCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csJob.enmProperty.ID, "Count") 
        If pGroupByJobCode = False Then .ColumnsHide.Add(csJob.enmProperty.Job) 
        If pGroupByJobRunnerCode = False Then .ColumnsHide.Add(csJob.enmProperty.JobRunner) 
        If pGroupByActive = False Then .ColumnsHide.Add(csJob.enmProperty.Active) 
        If pSumCyclicCount = False Then .ColumnsHide.Add(csJob.enmProperty.CyclicCount) 
        If pSumTimeOutSec = False Then .ColumnsHide.Add(csJob.enmProperty.TimeOutSec) 
        .ColumnsHide.Add(csJob.enmProperty.Description) 
        .ColumnsHide.Add(csJob.enmProperty.Instructions) 
        .ColumnsHide.Add(csJob.enmProperty.JobType) 
        .ColumnsHide.Add(csJob.enmProperty.WhenToRun) 
        .ColumnsHide.Add(csJob.enmProperty.SendNotificationOnSuccess) 
        .ColumnsHide.Add(csJob.enmProperty.SendAlarmOnMissed) 
        .ColumnsHide.Add(csJob.enmProperty.ActivatingUser) 
        .ColumnsHide.Add(csJob.enmProperty.NextRunTime) 
        .ColumnsHide.Add(csJob.enmProperty.LastRunTime) 
        .ColumnsHide.Add(csJob.enmProperty.JobStatus) 
        .ColumnsHide.Add(csJob.enmProperty.WarningMailSent) 
        .ColumnsHide.Add(csJob.enmProperty.IsManaged) 
        .ColumnsHide.Add(csJob.enmProperty.LastRunBy) 
        .ColumnsHide.Add(csJob.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlJobCol.Visible = True 
    pFault = _ctlJobCol.LoadControl(_JobCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csJobCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csJobCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlJob.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlJob.Controls(0).Name) 
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
    _JobID = -2 
    pFault = ActivateControl("ctlc_Job") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlJob() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlJob.Visible = True 'new 
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
 
  Private Sub _ctlJobCol_evtTimerTripped() Handles _ctlJobCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtJobTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlJobCol.JobCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlJobCol.JobCol(0).ID 
 
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
    If _JobCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csJob() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csJobCol = CType(CallByName(_JobCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csJobCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csJobCol = CType(CallByName(_JobCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csJobCol) 
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
                  ccHelper.GetPropertyTypeName(New csJobCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csJobCol = CType(CallByName(_JobCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csJobCol) 
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
 
  Private Sub cc_ctlPnlJob_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
  Private Sub ctlPnlc_Job_ccevtLoaded() Handles Me.evtLoaded 
    chkGrid.Checked = True 
  End Sub 
  
End Class 
