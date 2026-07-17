Public Class ctlPnlc_Process 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlProcessCol As ctlc_ProcessCol 
  Private WithEvents _ctlProcess As ctlc_Process 
  Private WithEvents _ctlPermissionCol As ctlc_PermissionCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _ProcessID As Long 
 
  'The data holders 
  Private _ProcessCol As csProcessCol 
  Private _Process As csProcess 
  Private _PermissionCol As csPermissionCol 
 
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
  Public Event evtOverrideLoadCboProcess(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetProcessIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillProcessCol(ByRef rProcessCol As csProcessCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillPermissionCol(ByRef rPermissionCol As csPermissionCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlProcessCol(ByRef rLoadParameters As ctlc_ProcessCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlProcess(ByRef rLoadParameters As ctlc_Process.clsLoadParameters) 
  Private Event evtOverrideLoadCtlPermissionCol(ByRef rLoadParameters As ctlc_PermissionCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreProcessCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtProcessTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtPermissionChosen As Boolean = False 
  Private _ShowPopForEvtPermissionChosen As Boolean = False 
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
 
    lnkProcessCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkPermissionCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vProcessID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _ProcessID = CType(vProcessID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlProcess.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkProcessCol.Visible = False 
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
      pFault = LoadCboProcesss(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _ProcessID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_ProcessID) 
      End If 
      ChooseProcess() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_Process") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _ProcessID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlc_Process" OrElse pControlName = "ctlProcess" Then 
      lnkProcess.ForeColor = Color.Black : lnkProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkProcess.BackColor = Color.Wheat 
      If _ctlProcess Is Nothing Then 
        _ctlProcess = New ctlc_Process() 
        _ctlProcess.Dock = DockStyle.Fill 
        pnlProcess.Controls.Add(_ctlProcess) 
        _ctlProcess.Visible = False 
      End If 
      If _ProcessID = 0 Then 
        pnlProcess.Visible = False 
      End If 
      'If _Process Is Nothing Then 
      pFault = RefreshCtlProcess() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlProcess.Process.IsEmpty AndAlso _ProcessID <> -2 Then 
        pnlProcess.Visible = False 
      End If 
      _ctlProcess.Name = "ctlc_Process" 
      _ActiveControl = _ctlProcess 
      _ctlProcess.BringToFront() 
      _ctlProcess.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_ProcessCol" Then 
      lnkProcessCol.ForeColor = Color.Black : lnkProcessCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkProcessCol.BackColor = Color.Wheat 
      If _ctlProcessCol Is Nothing Then 
        _ctlProcessCol = New ctlc_ProcessCol() 
        _ctlProcessCol.Dock = DockStyle.Fill 
        pnlProcess.Controls.Add(_ctlProcessCol) 
        _ctlProcessCol.Visible = False 
      End If  
      pnlProcess.Visible = True 
      If _ProcessCol Is Nothing Then 
        pFault = RefreshCtlProcessCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlProcessCol.Name = "ctlc_ProcessCol" 
      _ActiveControl = _ctlProcessCol 
      _ctlProcessCol.BringToFront() 
      _ctlProcessCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlc_PermissionCol" Then 
      lnkPermissionCol.ForeColor = Color.Black : lnkPermissionCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkPermissionCol.BackColor = Color.Wheat 
      If _ctlPermissionCol Is Nothing Then 
      _ctlPermissionCol = New ctlc_PermissionCol() 
      _ctlPermissionCol.Dock = DockStyle.Fill 
      pnlProcess.Controls.Add(_ctlPermissionCol) 
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
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-Process-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("Process", _Requester) 
 
    lnkProcessCol.Text = CCTextTranslate("List", _Requester) 
    lnkProcess.Text = CCTextTranslate("Details", _Requester) 
 
    lnkPermissionCol.Text = TableNameTranslate("Permission", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlProcess.Controls(0) Is _ctlProcess Then 
      If _ProcessID = 0 Then 
        pnlProcess.Visible = False 
      End If 
    ElseIf pnlProcess.Controls(0) Is _ctlProcessCol Then 
    ElseIf pnlProcess.Controls(0) Is _ctlPermissionCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pProcessID As Long = _ProcessID 
      If ccHelper.IsNumeric(pText) Then _ProcessID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetProcessIDFromIntelliComboText(pText) 
      If pProcessID <> _ProcessID Then 
        _Process = Nothing 
        pFault = ActivateControl("ctlc_Process") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlProcess.Controls(0) Is _ctlProcess Then 
      pFault = RefreshCtlProcess() 
    ElseIf pnlProcess.Controls(0) Is _ctlProcessCol Then 
      pFault = RefreshCtlProcessCol() 
    ElseIf pnlProcess.Controls(0) Is _ctlPermissionCol Then 
      pFault = RefreshCtlPermissionCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlProcess.Controls(0).Name, "", "TRGT-Process-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboProcesss(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlProcessCol_evtRowClicked(ByVal vProcess As Object) Handles _ctlProcessCol.evtRowClicked 
    
    If vProcess Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pProcess As csProcess = CType(vProcess, csProcess) 
    _ProcessID = pProcess.ID 
    
    If _ActiveControl Is _ctlProcessCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
      Next 
      If pInGroupBy = True Then Cursor = Cursors.Default : Return 
      
      btnFilter.Visible = True 
      lblSecondaryTitle.Visible = False 
    Else 
      btnFilter.Visible = False 
      lblSecondaryTitle.Visible = True 
    End If 
    
    ChooseProcess() 
    
    Try 
      MyIntelliCombo.ValueSelect(_ProcessID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pProcess.Name
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseProcess() 
    _Process = Nothing 
    lnkProcess.Visible = True 
    _PermissionCol = Nothing 
    lnkPermissionCol.Visible = True 
  End Sub 
  Private Sub _ctlProcessCol_evtRowDoubleClicked(ByVal vProcess As csProcess, ByRef rHandled As Boolean) Handles _ctlProcessCol.evtRowDoubleClicked 
    If lnkProcess.Parent IsNot flpMenu Then Exit Sub 
    If vProcess Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreProcessCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vProcess.ID, vProcess.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _ProcessID = vProcess.ID 
      'MyIntelliCombo.ValueSelect(_ProcessID) 
      pFault = ActivateControl("ctlc_Process") 
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
      pFault = _ProcessCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ProcessCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ProcessCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProcessCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_ProcessCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csProcess.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Process" 
      pFault = _ctlProcessCol.LoadControl(_ProcessCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlProcessCol_evtUnChosen() Handles _ctlProcessCol.evtUnChosen 
 
    _ProcessID = 0 
    _Process = Nothing 
    _PermissionCol = Nothing 
    lnkPermissionCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkProcess.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkPermissionCol.Click, 
      lnkProcessCol.Click, 
      lnkProcess.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkProcess OrElse (lnk Is lnkProcessCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlProcessCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_ProcessCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csProcess.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csProcessCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillProcessCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _ProcessCol = New csProcessCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ProcessCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlProcessCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlProcessCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _ProcessCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlProcessCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _ProcessCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _ProcessCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProcessCol.Count) 
      End If 
    Else 
      _ProcessCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _ProcessCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlProcessCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Process" 
    
    Dim pProcessID As Long = _ProcessID 
    
    pFault = _ctlProcessCol.LoadControl(_ProcessCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlProcessCol.Visible = True 
    
    _ctlProcessCol.Refresh() 
    If pProcessID <> 0 Then 
      Dim pProcessCol As csProcessCol = CType(_ctlProcessCol.bsCtlProcess.DataSource, csProcessCol) 
      Dim pProcess As csProcess = pProcessCol.FindByID(pProcessID) 
      If pProcess.ID > 0 Then 
        _ctlProcessCol.bsCtlProcess.CurrencyManager.Position = pProcessCol.IndexOf(pProcess) 
        _ctlProcessCol.dgvProcess.Rows(pProcessCol.IndexOf(pProcess)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlProcess() As clsFault 
    Dim pFault As New clsFault 
    
    If _ProcessID > 0 Then 
      ChooseProcess() 
      _Process = New csProcess() 
      pFault = _Process.GetByID(_ProcessID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _Process = New csProcess() 
    End If 
    'lblSecondaryTitle.Text = _Process.Name    
     
    Dim pLoadParameters As New ctlc_Process.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = True 
    End With 
    RaiseEvent evtOverrideLoadCtlProcess(pLoadParameters)
    pFault = _ctlProcess.LoadControl(_Process, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlProcess.Visible = True 
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
      pFault = _PermissionCol.FillByProcessID(_ProcessID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
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
      If _Process IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Process.DefaultDesignation) Then 
        .ReportTitle = "List of Permissions for " & _Process.DefaultDesignation 
      Else 
        .ReportTitle = "List of Permissions for Process" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csPermission.enmProperty.Process) 
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
 
 
  'Handle events 
  Private Sub _ctlPermissionCol_evtBeforeUpdate(ByVal vPermission As csPermission, ByRef rCancel As Boolean) Handles _ctlPermissionCol.evtBeforeUpdate 
    vPermission.ProcessID = _Process.ID 
  End Sub 
  Private Function LoadCboProcesss(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_ProcessDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboProcess(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _ProcessID >= 0 Then 
      MyIntelliCombo.ValueSelect(_ProcessID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ProcessID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ProcessID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetProcessIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _ProcessID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _ProcessID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _ProcessID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _ProcessID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseProcess() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_Process", StringComparison.OrdinalIgnoreCase) AndAlso _ProcessID > 0 Then 
        'to avoid getting ObjectNotFound 
        _Process = New csProcess() 
        pFault = _Process.GetByID(_ProcessID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_Process") 
    End If 
    pnlProcess.Visible = True 
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
  
   
  Private Sub chkGrid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrid.CheckedChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    
    Cursor = Cursors.WaitCursor 
    chkGrid.Enabled = False 
    pnlButtons.Visible = False 
    pnlProcess.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkProcessCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _ProcessID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_ProcessCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkProcessCol.Visible = False 
      _ActiveControl = _ctlProcess 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboProcesss(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _ProcessID <> 0 Then 
        pFault = ActivateControl("ctlc_Process") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlProcess.Visible = False 
        _ProcessID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _ProcessID > 0 Then pnlProcess.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkPermissionCol.MouseEnter, 
                  lnkProcessCol.MouseEnter, 
                  lnkProcess.MouseEnter, 
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
                  lnkProcessCol.MouseLeave, 
                  lnkProcess.MouseLeave, 
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
    Dim pName As String = Nothing 
    Dim pNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Processes"  
  
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
        .String01Label.Text = If(_ctlProcessCol.LoadParameters.ColumnsHeaderText.ContainsKey(csProcess.enmProperty.Name), _ctlProcessCol.LoadParameters.ColumnsHeaderText(csProcess.enmProperty.Name), "Name") 
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
 
        .Text01Label.Text = If(_ctlProcessCol.LoadParameters.ColumnsHeaderText.ContainsKey(csProcess.enmProperty.ID), _ctlProcessCol.LoadParameters.ColumnsHeaderText(csProcess.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 5 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 6 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
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
        _SearchFilters.Add(csProcessCol.enmFillOnTheFlyParameters.Name, pName) 
        _SearchFilters.Add(csProcessCol.enmFillOnTheFlyParameters.NameWildcardType, pNameWildcardType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csProcessCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csProcessCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csProcessCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csProcessCol.enmListDefinition.Dir, pDir) 
      End If 
 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csProcessCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csProcessCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csProcessCol.enmListDefinition.Dir) Then _SearchFilters.Add(csProcessCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_ProcessCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_ProcessCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csProcess.enmProperty.ID, "ID") 
      End With 
      _ProcessCol = New csProcessCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ProcessCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _ProcessCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ProcessCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ProcessCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProcessCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Process" 
      RaiseEvent evtOverrideLoadCtlProcessCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _ProcessCol = New csProcessCol() 
      pFault = _ProcessCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_ProcessCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _ProcessCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csProcess.enmProperty.ID, "Count") 
        .ColumnsHide.Add(csProcess.enmProperty.Name) 
        .ColumnsHide.Add(csProcess.enmProperty.DateChecked) 
        .ColumnsHide.Add(csProcess.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlProcessCol.Visible = True 
    pFault = _ctlProcessCol.LoadControl(_ProcessCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csProcessCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csProcessCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlProcess.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlProcess.Controls(0).Name) 
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
 
  Private Sub _ctlProcessCol_evtTimerTripped() Handles _ctlProcessCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtProcessTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlProcessCol.ProcessCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlProcessCol.ProcessCol(0).ID 
 
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
    If _ProcessCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csProcess() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csProcessCol = CType(CallByName(_ProcessCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csProcessCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csProcessCol = CType(CallByName(_ProcessCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csProcessCol) 
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
                  ccHelper.GetPropertyTypeName(New csProcessCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csProcessCol = CType(CallByName(_ProcessCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csProcessCol) 
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
 
  Private Sub cc_ctlPnlProcess_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
