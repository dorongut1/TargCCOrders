Public Class ctlPnlc_AuditIndexed 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlAuditIndexedCol As ctlc_AuditIndexedCol 
  Private WithEvents _ctlAuditIndexed As ctlc_AuditIndexed 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _AuditIndexedID As Long 
 
  'The data holders 
  Private _AuditIndexedCol As csAuditIndexedCol 
  Private _AuditIndexed As csAuditIndexed 
 
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
  Public Event evtOverrideLoadCboAuditIndexed(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetAuditIndexedIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillAuditIndexedCol(ByRef rAuditIndexedCol As csAuditIndexedCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlAuditIndexedCol(ByRef rLoadParameters As ctlc_AuditIndexedCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlAuditIndexed(ByRef rLoadParameters As ctlc_AuditIndexed.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreAuditIndexedCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtAuditIndexedTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
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
 
    lnkAuditIndexedCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkAuditIndexed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vAuditIndexedID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _AuditIndexedID = CType(vAuditIndexedID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlAuditIndexed.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkAuditIndexedCol.Visible = False 
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
      pFault = LoadCboAuditIndexeds(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _AuditIndexedID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_AuditIndexedID) 
      End If 
      ChooseAuditIndexed() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_AuditIndexed") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _AuditIndexedID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _AuditIndexedID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_AuditIndexed" OrElse pControlName = "ctlAuditIndexed" Then 
      lnkAuditIndexed.ForeColor = Color.Black : lnkAuditIndexed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkAuditIndexed.BackColor = Color.Wheat 
      If _ctlAuditIndexed Is Nothing Then 
        _ctlAuditIndexed = New ctlc_AuditIndexed() 
        _ctlAuditIndexed.Dock = DockStyle.Fill 
        pnlAuditIndexed.Controls.Add(_ctlAuditIndexed) 
        _ctlAuditIndexed.Visible = False 
      End If 
      If _AuditIndexedID = 0 Then 
        pnlAuditIndexed.Visible = False 
      End If 
      'If _AuditIndexed Is Nothing Then 
      pFault = RefreshCtlAuditIndexed() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlAuditIndexed.AuditIndexed.IsEmpty AndAlso _AuditIndexedID <> -2 Then 
        pnlAuditIndexed.Visible = False 
      End If 
      _ctlAuditIndexed.Name = "ctlc_AuditIndexed" 
      _ActiveControl = _ctlAuditIndexed 
      _ctlAuditIndexed.BringToFront() 
      _ctlAuditIndexed.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_AuditIndexedCol" Then 
      lnkAuditIndexedCol.ForeColor = Color.Black : lnkAuditIndexedCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkAuditIndexedCol.BackColor = Color.Wheat 
      If _ctlAuditIndexedCol Is Nothing Then 
        _ctlAuditIndexedCol = New ctlc_AuditIndexedCol() 
        _ctlAuditIndexedCol.Dock = DockStyle.Fill 
        pnlAuditIndexed.Controls.Add(_ctlAuditIndexedCol) 
        _ctlAuditIndexedCol.Visible = False 
      End If  
      pnlAuditIndexed.Visible = True 
      If _AuditIndexedCol Is Nothing Then 
        pFault = RefreshCtlAuditIndexedCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlAuditIndexedCol.Name = "ctlc_AuditIndexedCol" 
      _ActiveControl = _ctlAuditIndexedCol 
      _ctlAuditIndexedCol.BringToFront() 
      _ctlAuditIndexedCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-AuditIndexed-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("AuditIndexed", _Requester) 
 
    lnkAuditIndexedCol.Text = CCTextTranslate("List", _Requester) 
    lnkAuditIndexed.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlAuditIndexed.Controls(0) Is _ctlAuditIndexed Then 
      If _AuditIndexedID = 0 Then 
        pnlAuditIndexed.Visible = False 
      End If 
    ElseIf pnlAuditIndexed.Controls(0) Is _ctlAuditIndexedCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pAuditIndexedID As Long = _AuditIndexedID 
      If ccHelper.IsNumeric(pText) Then _AuditIndexedID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetAuditIndexedIDFromIntelliComboText(pText) 
      If pAuditIndexedID <> _AuditIndexedID Then 
        _AuditIndexed = Nothing 
        pFault = ActivateControl("ctlc_AuditIndexed") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlAuditIndexed.Controls(0) Is _ctlAuditIndexed Then 
      pFault = RefreshCtlAuditIndexed() 
    ElseIf pnlAuditIndexed.Controls(0) Is _ctlAuditIndexedCol Then 
      pFault = RefreshCtlAuditIndexedCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlAuditIndexed.Controls(0).Name, "", "TRGT-AuditIndexed-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub _ctlAuditIndexedCol_evtRowClicked(ByVal vAuditIndexed As Object) Handles _ctlAuditIndexedCol.evtRowClicked 
    
    If vAuditIndexed Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pAuditIndexed As csAuditIndexed = CType(vAuditIndexed, csAuditIndexed) 
    _AuditIndexedID = pAuditIndexed.ID 
    
    If _ActiveControl Is _ctlAuditIndexedCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByOriginalID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByTableName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByRowID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByOccurredAt.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByFieldName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByChangedByUser.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByActiveLoginID.ToString() Then 
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
    
    ChooseAuditIndexed() 
    
    Try 
      MyIntelliCombo.ValueSelect(_AuditIndexedID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pAuditIndexed.ID.ToString("#,##0")

 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseAuditIndexed() 
    _AuditIndexed = Nothing 
    lnkAuditIndexed.Visible = True 
  End Sub 
  Private Sub _ctlAuditIndexedCol_evtRowDoubleClicked(ByVal vAuditIndexed As csAuditIndexed, ByRef rHandled As Boolean) Handles _ctlAuditIndexedCol.evtRowDoubleClicked 
    If lnkAuditIndexed.Parent IsNot flpMenu Then Exit Sub 
    If vAuditIndexed Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByOriginalID.ToString() Then 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.OriginalIDFrom) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.OriginalIDFrom) 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.OriginalIDTo) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.OriginalIDTo) 
        pSearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.OriginalIDFrom, vAuditIndexed.OriginalID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByTableName.ToString() Then 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.TableName) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.TableName) 
        pSearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.TableName, vAuditIndexed.TableName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByRowID.ToString() Then 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.RowIDFrom) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.RowIDFrom) 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.RowIDTo) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.RowIDTo) 
        pSearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.RowIDFrom, vAuditIndexed.RowID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByOccurredAt.ToString() Then 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.OccurredAtStart) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.OccurredAtStart) 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.OccurredAtEnd) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.OccurredAtEnd) 
        pSearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.OccurredAtStart, vAuditIndexed.OccurredAt) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByFieldName.ToString() Then 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.FieldName) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.FieldName) 
        pSearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.FieldName, vAuditIndexed.FieldName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByChangedByUser.ToString() Then 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.ChangedByUser) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.ChangedByUser) 
        pSearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.ChangedByUser, vAuditIndexed.ChangedByUser) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByActiveLoginID.ToString() Then 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.ActiveLoginIDFrom) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.ActiveLoginIDFrom) 
        If pSearchFilters.ContainsKey(csAuditIndexedCol.enmFillOnTheFlyParameters.ActiveLoginIDTo) Then pSearchFilters.Remove(csAuditIndexedCol.enmFillOnTheFlyParameters.ActiveLoginIDTo) 
        pSearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.ActiveLoginIDFrom, vAuditIndexed.ActiveLoginID) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreAuditIndexedCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vAuditIndexed.ID, vAuditIndexed.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _AuditIndexedID = vAuditIndexed.ID 
      'MyIntelliCombo.ValueSelect(_AuditIndexedID) 
      pFault = ActivateControl("ctlc_AuditIndexed") 
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
      pFault = _AuditIndexedCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _AuditIndexedCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _AuditIndexedCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _AuditIndexedCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_AuditIndexedCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csAuditIndexed.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see AuditIndexed" 
      pFault = _ctlAuditIndexedCol.LoadControl(_AuditIndexedCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlAuditIndexedCol_evtUnChosen() Handles _ctlAuditIndexedCol.evtUnChosen 
 
    _AuditIndexedID = 0 
    _AuditIndexed = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkAuditIndexed.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkAuditIndexedCol.Click, 
      lnkAuditIndexed.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkAuditIndexed OrElse (lnk Is lnkAuditIndexedCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlAuditIndexedCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_AuditIndexedCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csAuditIndexed.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csAuditIndexedCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillAuditIndexedCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _AuditIndexedCol = New csAuditIndexedCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _AuditIndexedCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlAuditIndexedCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlAuditIndexedCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _AuditIndexedCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlAuditIndexedCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _AuditIndexedCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _AuditIndexedCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _AuditIndexedCol.Count) 
      End If 
    Else 
      _AuditIndexedCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _AuditIndexedCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlAuditIndexedCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see AuditIndexed" 
    
    Dim pAuditIndexedID As Long = _AuditIndexedID 
    
    pFault = _ctlAuditIndexedCol.LoadControl(_AuditIndexedCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlAuditIndexedCol.Visible = True 
    
    _ctlAuditIndexedCol.Refresh() 
    If pAuditIndexedID <> 0 Then 
      Dim pAuditIndexedCol As csAuditIndexedCol = CType(_ctlAuditIndexedCol.bsCtlAuditIndexed.DataSource, csAuditIndexedCol) 
      Dim pAuditIndexed As csAuditIndexed = pAuditIndexedCol.FindByID(pAuditIndexedID) 
      If pAuditIndexed.ID > 0 Then 
        _ctlAuditIndexedCol.bsCtlAuditIndexed.CurrencyManager.Position = pAuditIndexedCol.IndexOf(pAuditIndexed) 
        _ctlAuditIndexedCol.dgvAuditIndexed.Rows(pAuditIndexedCol.IndexOf(pAuditIndexed)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlAuditIndexed() As clsFault 
    Dim pFault As New clsFault 
    
    If _AuditIndexedID > 0 Then 
      ChooseAuditIndexed() 
      _AuditIndexed = New csAuditIndexed() 
      pFault = _AuditIndexed.GetByID(_AuditIndexedID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _AuditIndexed = New csAuditIndexed() 
    End If 
    'lblSecondaryTitle.Text = _AuditIndexed.ID.ToString("#,##0")
    
     
    Dim pLoadParameters As New ctlc_AuditIndexed.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = True 
    End With 
    RaiseEvent evtOverrideLoadCtlAuditIndexed(pLoadParameters)
    pFault = _ctlAuditIndexed.LoadControl(_AuditIndexed, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlAuditIndexed.Visible = True 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Function LoadCboAuditIndexeds(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.UD 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboAuditIndexed(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _AuditIndexedID >= 0 Then 
      MyIntelliCombo.ValueSelect(_AuditIndexedID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _AuditIndexedID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _AuditIndexedID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetAuditIndexedIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _AuditIndexedID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _AuditIndexedID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _AuditIndexedID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _AuditIndexedID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseAuditIndexed() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_AuditIndexed", StringComparison.OrdinalIgnoreCase) AndAlso _AuditIndexedID > 0 Then 
        'to avoid getting ObjectNotFound 
        _AuditIndexed = New csAuditIndexed() 
        pFault = _AuditIndexed.GetByID(_AuditIndexedID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_AuditIndexed") 
    End If 
    pnlAuditIndexed.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
   
  Private Sub chkGrid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrid.CheckedChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    
    Cursor = Cursors.WaitCursor 
    chkGrid.Enabled = False 
    pnlButtons.Visible = False 
    pnlAuditIndexed.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkAuditIndexedCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _AuditIndexedID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_AuditIndexedCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkAuditIndexedCol.Visible = False 
      _ActiveControl = _ctlAuditIndexed 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboAuditIndexeds(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _AuditIndexedID <> 0 Then 
        MyIntelliCombo.cbo.Text = _AuditIndexedID.ToString() 
        pFault = ActivateControl("ctlc_AuditIndexed") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlAuditIndexed.Visible = False 
        _AuditIndexedID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _AuditIndexedID > 0 Then pnlAuditIndexed.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkAuditIndexedCol.MouseEnter, 
                  lnkAuditIndexed.MouseEnter, 
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
                  lnkAuditIndexedCol.MouseLeave, 
                  lnkAuditIndexed.MouseLeave, 
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
    Dim pOriginalIDFrom As Nullable(Of Long) = Nothing 
    Dim pOriginalIDTo As Nullable(Of Long) = Nothing 
    Dim pTableName As String = Nothing 
    Dim pTableNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pRowIDFrom As Nullable(Of Long) = Nothing 
    Dim pRowIDTo As Nullable(Of Long) = Nothing 
    Dim pOccurredAtStart As Nullable(Of Date) = Nothing 
    Dim pOccurredAtEnd As Nullable(Of Date) = Nothing 
    Dim pFieldName As String = Nothing 
    Dim pFieldNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pChangedByUser As String = Nothing 
    Dim pChangedByUserWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pActiveLoginIDFrom As Nullable(Of Long) = Nothing 
    Dim pActiveLoginIDTo As Nullable(Of Long) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByOriginalID As Boolean = False 
    Dim pGroupByTableName As Boolean = False 
    Dim pGroupByRowID As Boolean = False 
    Dim pGroupByOccurredAt As Boolean = False 
    Dim pGroupByFieldName As Boolean = False 
    Dim pGroupByChangedByUser As Boolean = False 
    Dim pGroupByActiveLoginID As Boolean = False 
    
    Dim pSumOriginalID As Boolean = False 
    Dim pSumRowID As Boolean = False 
    Dim pSumActiveLoginID As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Audit Indexeds"  
  
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
        .Text01Label.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.OriginalID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.OriginalID), "Original ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 3 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 4 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .Combo01Label.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.TableName), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.TableName), "Table Name") 
        Dim pTableNames As New clsComboList 
        pTableNames.Add(New clsComboListMember(1, "BeehiveBuyerTracking")) 
        pTableNames.Add(New clsComboListMember(2, "Customer")) 
        pTableNames.Add(New clsComboListMember(3, "CustomerDebt")) 
        pTableNames.Add(New clsComboListMember(4, "Delivery")) 
        pTableNames.Add(New clsComboListMember(5, "OrderHeader")) 
        pTableNames.Add(New clsComboListMember(6, "OrderLine")) 
        pTableNames.Add(New clsComboListMember(7, "Product")) 
        pTableNames.Add(New clsComboListMember(8, "ProductPrice")) 
        pTableNames.Add(New clsComboListMember(9, "ProductPriceHist")) 
        pTableNames.Add(New clsComboListMember(10, "SupplierOrder")) 
        pTableNames.Add(New clsComboListMember(11, "c_AlertMessage")) 
        pTableNames.Add(New clsComboListMember(12, "c_AuditIndexed")) 
        pTableNames.Add(New clsComboListMember(13, "c_Enumeration")) 
        pTableNames.Add(New clsComboListMember(14, "c_IndexFragmentation")) 
        pTableNames.Add(New clsComboListMember(15, "c_Job")) 
        pTableNames.Add(New clsComboListMember(16, "c_JobAlertRecipient")) 
        pTableNames.Add(New clsComboListMember(17, "c_Language")) 
        pTableNames.Add(New clsComboListMember(18, "c_LoggedAlert")) 
        pTableNames.Add(New clsComboListMember(19, "c_LoggedJob")) 
        pTableNames.Add(New clsComboListMember(20, "c_LoggedLogin")) 
        pTableNames.Add(New clsComboListMember(21, "c_LoggedRequest")) 
        pTableNames.Add(New clsComboListMember(22, "c_Lookup")) 
        pTableNames.Add(New clsComboListMember(23, "c_Mail")) 
        pTableNames.Add(New clsComboListMember(24, "c_MFA")) 
        pTableNames.Add(New clsComboListMember(25, "c_ObjectToTranslate")) 
        pTableNames.Add(New clsComboListMember(26, "c_ObjectTranslation")) 
        pTableNames.Add(New clsComboListMember(27, "c_Permission")) 
        pTableNames.Add(New clsComboListMember(28, "c_Process")) 
        pTableNames.Add(New clsComboListMember(29, "c_Role")) 
        pTableNames.Add(New clsComboListMember(30, "c_SystemAudit")) 
        pTableNames.Add(New clsComboListMember(31, "c_SystemDefault")) 
        pTableNames.Add(New clsComboListMember(32, "c_Table")) 
        pTableNames.Add(New clsComboListMember(33, "c_TableSize")) 
        pTableNames.Add(New clsComboListMember(34, "c_User")) 
        pTableNames.Add(New clsComboListMember(35, "c_UserLoginKey")) 
        pTableNames.Add(New clsComboListMember(36, "c_UserPermission")) 
        pTableNames.Add(New clsComboListMember(37, "c_UserStatus")) 
        pTableNames.SortByText() 
        If pTableNames IsNot Nothing AndAlso pTableNames.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo01Label) 
          .flpFilter.Controls.Add(.Combo01) 'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo01 
          .MakeSmart() 
          .LoadControl(pTableNames, GetChoose(_Requester)) 
          .TabIndex = 5 
        End With 
 
        .Text02Label.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.RowID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.RowID), "Row ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 6 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 7 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .Date01Label.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.OccurredAt), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.OccurredAt), "Occurred At") 
        .Date01From.TabIndex = 8 
        .Date01To.TabIndex = 9 
        .Date01From.ShowCheckBox = True 
        .Date01To.ShowCheckBox = True 
        .Date01From.Checked = False 
        .Date01To.Checked = False 
        If _ctlAuditIndexedCol.LoadParameters.ColumnsFormat.ContainsKey(csAuditIndexed.enmProperty.OccurredAt) Then 
          .Date01From.CustomFormat = _ctlAuditIndexedCol.LoadParameters.ColumnsFormat(csAuditIndexed.enmProperty.OccurredAt) 
          .Date01To.CustomFormat = _ctlAuditIndexedCol.LoadParameters.ColumnsFormat(csAuditIndexed.enmProperty.OccurredAt) 
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
 
        .Combo02Label.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.FieldName), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.FieldName), "Field Name") 
        Dim pFieldNames As New clsComboList 
        If pFieldNames.FindByText("CustomerID").Text = "" Then pFieldNames.Add(New clsComboListMember(1, "CustomerID")) 
        If pFieldNames.FindByText("LastOrderDate").Text = "" Then pFieldNames.Add(New clsComboListMember(2, "LastOrderDate")) 
        If pFieldNames.FindByText("BeehiveQuantity").Text = "" Then pFieldNames.Add(New clsComboListMember(3, "BeehiveQuantity")) 
        If pFieldNames.FindByText("ReminderMonth").Text = "" Then pFieldNames.Add(New clsComboListMember(4, "ReminderMonth")) 
        If pFieldNames.FindByText("blg_IsRelevant").Text = "" Then pFieldNames.Add(New clsComboListMember(5, "blg_IsRelevant")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(6, "Notes")) 
        If pFieldNames.FindByText("CustomerCode").Text = "" Then pFieldNames.Add(New clsComboListMember(7, "CustomerCode")) 
        If pFieldNames.FindByText("CustomerName").Text = "" Then pFieldNames.Add(New clsComboListMember(8, "CustomerName")) 
        If pFieldNames.FindByText("Phone").Text = "" Then pFieldNames.Add(New clsComboListMember(9, "Phone")) 
        If pFieldNames.FindByText("Email").Text = "" Then pFieldNames.Add(New clsComboListMember(10, "Email")) 
        If pFieldNames.FindByText("Address").Text = "" Then pFieldNames.Add(New clsComboListMember(11, "Address")) 
        If pFieldNames.FindByText("City").Text = "" Then pFieldNames.Add(New clsComboListMember(12, "City")) 
        If pFieldNames.FindByText("TaxID").Text = "" Then pFieldNames.Add(New clsComboListMember(13, "TaxID")) 
        If pFieldNames.FindByText("enmCustomerType").Text = "" Then pFieldNames.Add(New clsComboListMember(14, "enmCustomerType")) 
        If pFieldNames.FindByText("PaymentTermsDays").Text = "" Then pFieldNames.Add(New clsComboListMember(15, "PaymentTermsDays")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(16, "Notes")) 
        If pFieldNames.FindByText("blg_IsActive").Text = "" Then pFieldNames.Add(New clsComboListMember(17, "blg_IsActive")) 
        If pFieldNames.FindByText("Location").Text = "" Then pFieldNames.Add(New clsComboListMember(18, "Location")) 
        If pFieldNames.FindByText("AccountantEmail").Text = "" Then pFieldNames.Add(New clsComboListMember(19, "AccountantEmail")) 
        If pFieldNames.FindByText("enmAccountantMethod").Text = "" Then pFieldNames.Add(New clsComboListMember(20, "enmAccountantMethod")) 
        If pFieldNames.FindByText("InvoiceName").Text = "" Then pFieldNames.Add(New clsComboListMember(21, "InvoiceName")) 
        If pFieldNames.FindByText("ProfitabilityCode").Text = "" Then pFieldNames.Add(New clsComboListMember(22, "ProfitabilityCode")) 
        If pFieldNames.FindByText("clc_CustomerIdentifier").Text = "" Then pFieldNames.Add(New clsComboListMember(23, "clc_CustomerIdentifier")) 
        If pFieldNames.FindByText("CustomerID").Text = "" Then pFieldNames.Add(New clsComboListMember(24, "CustomerID")) 
        If pFieldNames.FindByText("OrderHeaderID").Text = "" Then pFieldNames.Add(New clsComboListMember(25, "OrderHeaderID")) 
        If pFieldNames.FindByText("DebtAmount").Text = "" Then pFieldNames.Add(New clsComboListMember(26, "DebtAmount")) 
        If pFieldNames.FindByText("PaidAmount").Text = "" Then pFieldNames.Add(New clsComboListMember(27, "PaidAmount")) 
        If pFieldNames.FindByText("clc_RemainingAmount").Text = "" Then pFieldNames.Add(New clsComboListMember(28, "clc_RemainingAmount")) 
        If pFieldNames.FindByText("DebtDate").Text = "" Then pFieldNames.Add(New clsComboListMember(29, "DebtDate")) 
        If pFieldNames.FindByText("DueDate").Text = "" Then pFieldNames.Add(New clsComboListMember(30, "DueDate")) 
        If pFieldNames.FindByText("enmDebtStatus").Text = "" Then pFieldNames.Add(New clsComboListMember(31, "enmDebtStatus")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(32, "Notes")) 
        If pFieldNames.FindByText("blg_NeedsAttention").Text = "" Then pFieldNames.Add(New clsComboListMember(33, "blg_NeedsAttention")) 
        If pFieldNames.FindByText("ProductTypes").Text = "" Then pFieldNames.Add(New clsComboListMember(34, "ProductTypes")) 
        If pFieldNames.FindByText("DeliveryDate").Text = "" Then pFieldNames.Add(New clsComboListMember(35, "DeliveryDate")) 
        If pFieldNames.FindByText("OrderHeaderID").Text = "" Then pFieldNames.Add(New clsComboListMember(36, "OrderHeaderID")) 
        If pFieldNames.FindByText("DeliveryAddress").Text = "" Then pFieldNames.Add(New clsComboListMember(37, "DeliveryAddress")) 
        If pFieldNames.FindByText("ContactPhone").Text = "" Then pFieldNames.Add(New clsComboListMember(38, "ContactPhone")) 
        If pFieldNames.FindByText("ContactName").Text = "" Then pFieldNames.Add(New clsComboListMember(39, "ContactName")) 
        If pFieldNames.FindByText("enmDeliveryMethod").Text = "" Then pFieldNames.Add(New clsComboListMember(40, "enmDeliveryMethod")) 
        If pFieldNames.FindByText("OrderedDate").Text = "" Then pFieldNames.Add(New clsComboListMember(41, "OrderedDate")) 
        If pFieldNames.FindByText("ReceivedDate").Text = "" Then pFieldNames.Add(New clsComboListMember(42, "ReceivedDate")) 
        If pFieldNames.FindByText("ArrivalToHubDate").Text = "" Then pFieldNames.Add(New clsComboListMember(43, "ArrivalToHubDate")) 
        If pFieldNames.FindByText("ArrivalToCustomerDate").Text = "" Then pFieldNames.Add(New clsComboListMember(44, "ArrivalToCustomerDate")) 
        If pFieldNames.FindByText("enmDeliveryStatus").Text = "" Then pFieldNames.Add(New clsComboListMember(45, "enmDeliveryStatus")) 
        If pFieldNames.FindByText("Location").Text = "" Then pFieldNames.Add(New clsComboListMember(46, "Location")) 
        If pFieldNames.FindByText("blg_ProductsSummary").Text = "" Then pFieldNames.Add(New clsComboListMember(47, "blg_ProductsSummary")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(48, "Notes")) 
        If pFieldNames.FindByText("OrderNumber").Text = "" Then pFieldNames.Add(New clsComboListMember(49, "OrderNumber")) 
        If pFieldNames.FindByText("CustomerID").Text = "" Then pFieldNames.Add(New clsComboListMember(50, "CustomerID")) 
        If pFieldNames.FindByText("OrderDate").Text = "" Then pFieldNames.Add(New clsComboListMember(51, "OrderDate")) 
        If pFieldNames.FindByText("clc_TotalAmount").Text = "" Then pFieldNames.Add(New clsComboListMember(52, "clc_TotalAmount")) 
        If pFieldNames.FindByText("clc_VATAmount").Text = "" Then pFieldNames.Add(New clsComboListMember(53, "clc_VATAmount")) 
        If pFieldNames.FindByText("clc_TotalWithVAT").Text = "" Then pFieldNames.Add(New clsComboListMember(54, "clc_TotalWithVAT")) 
        If pFieldNames.FindByText("enmPaymentMethod").Text = "" Then pFieldNames.Add(New clsComboListMember(55, "enmPaymentMethod")) 
        If pFieldNames.FindByText("enmPaymentStatus").Text = "" Then pFieldNames.Add(New clsComboListMember(56, "enmPaymentStatus")) 
        If pFieldNames.FindByText("PaymentDate").Text = "" Then pFieldNames.Add(New clsComboListMember(57, "PaymentDate")) 
        If pFieldNames.FindByText("InvoiceNumber").Text = "" Then pFieldNames.Add(New clsComboListMember(58, "InvoiceNumber")) 
        If pFieldNames.FindByText("enmDeliveryMethod").Text = "" Then pFieldNames.Add(New clsComboListMember(59, "enmDeliveryMethod")) 
        If pFieldNames.FindByText("DeliveryDate").Text = "" Then pFieldNames.Add(New clsComboListMember(60, "DeliveryDate")) 
        If pFieldNames.FindByText("enmDeliveryDay").Text = "" Then pFieldNames.Add(New clsComboListMember(61, "enmDeliveryDay")) 
        If pFieldNames.FindByText("enmOrderStatus").Text = "" Then pFieldNames.Add(New clsComboListMember(62, "enmOrderStatus")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(63, "Notes")) 
        If pFieldNames.FindByText("Notes2").Text = "" Then pFieldNames.Add(New clsComboListMember(64, "Notes2")) 
        If pFieldNames.FindByText("clc_OrderMonth").Text = "" Then pFieldNames.Add(New clsComboListMember(65, "clc_OrderMonth")) 
        If pFieldNames.FindByText("clc_Quarter").Text = "" Then pFieldNames.Add(New clsComboListMember(66, "clc_Quarter")) 
        If pFieldNames.FindByText("OrderHeaderID").Text = "" Then pFieldNames.Add(New clsComboListMember(67, "OrderHeaderID")) 
        If pFieldNames.FindByText("ProductID").Text = "" Then pFieldNames.Add(New clsComboListMember(68, "ProductID")) 
        If pFieldNames.FindByText("Quantity").Text = "" Then pFieldNames.Add(New clsComboListMember(69, "Quantity")) 
        If pFieldNames.FindByText("UnitPrice").Text = "" Then pFieldNames.Add(New clsComboListMember(70, "UnitPrice")) 
        If pFieldNames.FindByText("DiscountPercent").Text = "" Then pFieldNames.Add(New clsComboListMember(71, "DiscountPercent")) 
        If pFieldNames.FindByText("blg_UnitCost").Text = "" Then pFieldNames.Add(New clsComboListMember(72, "blg_UnitCost")) 
        If pFieldNames.FindByText("LineNumber").Text = "" Then pFieldNames.Add(New clsComboListMember(73, "LineNumber")) 
        If pFieldNames.FindByText("clc_LineTotal").Text = "" Then pFieldNames.Add(New clsComboListMember(74, "clc_LineTotal")) 
        If pFieldNames.FindByText("clc_TotalCost").Text = "" Then pFieldNames.Add(New clsComboListMember(75, "clc_TotalCost")) 
        If pFieldNames.FindByText("clc_Profit").Text = "" Then pFieldNames.Add(New clsComboListMember(76, "clc_Profit")) 
        If pFieldNames.FindByText("ProductCode").Text = "" Then pFieldNames.Add(New clsComboListMember(77, "ProductCode")) 
        If pFieldNames.FindByText("ProductName").Text = "" Then pFieldNames.Add(New clsComboListMember(78, "ProductName")) 
        If pFieldNames.FindByText("enmCategory").Text = "" Then pFieldNames.Add(New clsComboListMember(79, "enmCategory")) 
        If pFieldNames.FindByText("UnitOfMeasure").Text = "" Then pFieldNames.Add(New clsComboListMember(80, "UnitOfMeasure")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(81, "Notes")) 
        If pFieldNames.FindByText("blg_IsActive").Text = "" Then pFieldNames.Add(New clsComboListMember(82, "blg_IsActive")) 
        If pFieldNames.FindByText("clc_CurrentStock").Text = "" Then pFieldNames.Add(New clsComboListMember(83, "clc_CurrentStock")) 
        If pFieldNames.FindByText("BaseCost").Text = "" Then pFieldNames.Add(New clsComboListMember(84, "BaseCost")) 
        If pFieldNames.FindByText("ProductID").Text = "" Then pFieldNames.Add(New clsComboListMember(85, "ProductID")) 
        If pFieldNames.FindByText("enmCustomerType").Text = "" Then pFieldNames.Add(New clsComboListMember(86, "enmCustomerType")) 
        If pFieldNames.FindByText("SellingPrice").Text = "" Then pFieldNames.Add(New clsComboListMember(87, "SellingPrice")) 
        If pFieldNames.FindByText("MinQuantity").Text = "" Then pFieldNames.Add(New clsComboListMember(88, "MinQuantity")) 
        If pFieldNames.FindByText("DiscountPercent").Text = "" Then pFieldNames.Add(New clsComboListMember(89, "DiscountPercent")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(90, "Notes")) 
        If pFieldNames.FindByText("ProductID").Text = "" Then pFieldNames.Add(New clsComboListMember(91, "ProductID")) 
        If pFieldNames.FindByText("enmCustomerType").Text = "" Then pFieldNames.Add(New clsComboListMember(92, "enmCustomerType")) 
        If pFieldNames.FindByText("BaseCost").Text = "" Then pFieldNames.Add(New clsComboListMember(93, "BaseCost")) 
        If pFieldNames.FindByText("SellingPrice").Text = "" Then pFieldNames.Add(New clsComboListMember(94, "SellingPrice")) 
        If pFieldNames.FindByText("MinQuantity").Text = "" Then pFieldNames.Add(New clsComboListMember(95, "MinQuantity")) 
        If pFieldNames.FindByText("DiscountPercent").Text = "" Then pFieldNames.Add(New clsComboListMember(96, "DiscountPercent")) 
        If pFieldNames.FindByText("ValidFrom").Text = "" Then pFieldNames.Add(New clsComboListMember(97, "ValidFrom")) 
        If pFieldNames.FindByText("ValidTo").Text = "" Then pFieldNames.Add(New clsComboListMember(98, "ValidTo")) 
        If pFieldNames.FindByText("ArchivedDate").Text = "" Then pFieldNames.Add(New clsComboListMember(99, "ArchivedDate")) 
        If pFieldNames.FindByText("ArchivedReason").Text = "" Then pFieldNames.Add(New clsComboListMember(100, "ArchivedReason")) 
        If pFieldNames.FindByText("OriginalPriceID").Text = "" Then pFieldNames.Add(New clsComboListMember(101, "OriginalPriceID")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(102, "Notes")) 
        If pFieldNames.FindByText("AddFieldsHere").Text = "" Then pFieldNames.Add(New clsComboListMember(103, "AddFieldsHere")) 
        If pFieldNames.FindByText("OrderHeaderID").Text = "" Then pFieldNames.Add(New clsComboListMember(104, "OrderHeaderID")) 
        If pFieldNames.FindByText("SupplierEmail").Text = "" Then pFieldNames.Add(New clsComboListMember(105, "SupplierEmail")) 
        If pFieldNames.FindByText("EmailSubject").Text = "" Then pFieldNames.Add(New clsComboListMember(106, "EmailSubject")) 
        If pFieldNames.FindByText("EmailBody").Text = "" Then pFieldNames.Add(New clsComboListMember(107, "EmailBody")) 
        If pFieldNames.FindByText("enmEmailStatus").Text = "" Then pFieldNames.Add(New clsComboListMember(108, "enmEmailStatus")) 
        If pFieldNames.FindByText("SentDate").Text = "" Then pFieldNames.Add(New clsComboListMember(109, "SentDate")) 
        If pFieldNames.FindByText("blg_TotalCost").Text = "" Then pFieldNames.Add(New clsComboListMember(110, "blg_TotalCost")) 
        If pFieldNames.FindByText("enmDeliveryMethod").Text = "" Then pFieldNames.Add(New clsComboListMember(111, "enmDeliveryMethod")) 
        If pFieldNames.FindByText("RequestedDeliveryDate").Text = "" Then pFieldNames.Add(New clsComboListMember(112, "RequestedDeliveryDate")) 
        If pFieldNames.FindByText("RequestedDeliveryDay").Text = "" Then pFieldNames.Add(New clsComboListMember(113, "RequestedDeliveryDay")) 
        If pFieldNames.FindByText("Notes").Text = "" Then pFieldNames.Add(New clsComboListMember(114, "Notes")) 
        If pFieldNames.FindByText("Number").Text = "" Then pFieldNames.Add(New clsComboListMember(115, "Number")) 
        If pFieldNames.FindByText("Description").Text = "" Then pFieldNames.Add(New clsComboListMember(116, "Description")) 
        If pFieldNames.FindByText("enmType_FaultType").Text = "" Then pFieldNames.Add(New clsComboListMember(117, "enmType_FaultType")) 
        If pFieldNames.FindByText("enmSeverity_FaultSeverity").Text = "" Then pFieldNames.Add(New clsComboListMember(118, "enmSeverity_FaultSeverity")) 
        If pFieldNames.FindByText("locMessage").Text = "" Then pFieldNames.Add(New clsComboListMember(119, "locMessage")) 
        If pFieldNames.FindByText("locAction").Text = "" Then pFieldNames.Add(New clsComboListMember(120, "locAction")) 
        If pFieldNames.FindByText("OriginalID").Text = "" Then pFieldNames.Add(New clsComboListMember(121, "OriginalID")) 
        If pFieldNames.FindByText("TableName").Text = "" Then pFieldNames.Add(New clsComboListMember(122, "TableName")) 
        If pFieldNames.FindByText("RowID").Text = "" Then pFieldNames.Add(New clsComboListMember(123, "RowID")) 
        If pFieldNames.FindByText("Operation").Text = "" Then pFieldNames.Add(New clsComboListMember(124, "Operation")) 
        If pFieldNames.FindByText("OccurredAt").Text = "" Then pFieldNames.Add(New clsComboListMember(125, "OccurredAt")) 
        If pFieldNames.FindByText("SqlCurrentUser").Text = "" Then pFieldNames.Add(New clsComboListMember(126, "SqlCurrentUser")) 
        If pFieldNames.FindByText("FieldName").Text = "" Then pFieldNames.Add(New clsComboListMember(127, "FieldName")) 
        If pFieldNames.FindByText("OldValue").Text = "" Then pFieldNames.Add(New clsComboListMember(128, "OldValue")) 
        If pFieldNames.FindByText("NewValue").Text = "" Then pFieldNames.Add(New clsComboListMember(129, "NewValue")) 
        If pFieldNames.FindByText("ChangedByUser").Text = "" Then pFieldNames.Add(New clsComboListMember(130, "ChangedByUser")) 
        If pFieldNames.FindByText("ActiveLoginID").Text = "" Then pFieldNames.Add(New clsComboListMember(131, "ActiveLoginID")) 
        If pFieldNames.FindByText("SqlSystemUser").Text = "" Then pFieldNames.Add(New clsComboListMember(132, "SqlSystemUser")) 
        If pFieldNames.FindByText("SqlAppName").Text = "" Then pFieldNames.Add(New clsComboListMember(133, "SqlAppName")) 
        If pFieldNames.FindByText("SqlHostName").Text = "" Then pFieldNames.Add(New clsComboListMember(134, "SqlHostName")) 
        If pFieldNames.FindByText("IsSystem").Text = "" Then pFieldNames.Add(New clsComboListMember(135, "IsSystem")) 
        If pFieldNames.FindByText("EnumType").Text = "" Then pFieldNames.Add(New clsComboListMember(136, "EnumType")) 
        If pFieldNames.FindByText("EnumValue").Text = "" Then pFieldNames.Add(New clsComboListMember(137, "EnumValue")) 
        If pFieldNames.FindByText("locText").Text = "" Then pFieldNames.Add(New clsComboListMember(138, "locText")) 
        If pFieldNames.FindByText("TableName").Text = "" Then pFieldNames.Add(New clsComboListMember(139, "TableName")) 
        If pFieldNames.FindByText("IndexName").Text = "" Then pFieldNames.Add(New clsComboListMember(140, "IndexName")) 
        If pFieldNames.FindByText("IndexType").Text = "" Then pFieldNames.Add(New clsComboListMember(141, "IndexType")) 
        If pFieldNames.FindByText("FragmentationPct").Text = "" Then pFieldNames.Add(New clsComboListMember(142, "FragmentationPct")) 
        If pFieldNames.FindByText("PageCount").Text = "" Then pFieldNames.Add(New clsComboListMember(143, "PageCount")) 
        If pFieldNames.FindByText("lkpJob").Text = "" Then pFieldNames.Add(New clsComboListMember(144, "lkpJob")) 
        If pFieldNames.FindByText("lkpJobRunner").Text = "" Then pFieldNames.Add(New clsComboListMember(145, "lkpJobRunner")) 
        If pFieldNames.FindByText("Description").Text = "" Then pFieldNames.Add(New clsComboListMember(146, "Description")) 
        If pFieldNames.FindByText("Instructions").Text = "" Then pFieldNames.Add(New clsComboListMember(147, "Instructions")) 
        If pFieldNames.FindByText("enmJobType").Text = "" Then pFieldNames.Add(New clsComboListMember(148, "enmJobType")) 
        If pFieldNames.FindByText("WhenToRun").Text = "" Then pFieldNames.Add(New clsComboListMember(149, "WhenToRun")) 
        If pFieldNames.FindByText("CyclicCount").Text = "" Then pFieldNames.Add(New clsComboListMember(150, "CyclicCount")) 
        If pFieldNames.FindByText("SendNotificationOnSuccess").Text = "" Then pFieldNames.Add(New clsComboListMember(151, "SendNotificationOnSuccess")) 
        If pFieldNames.FindByText("SendAlarmOnMissed").Text = "" Then pFieldNames.Add(New clsComboListMember(152, "SendAlarmOnMissed")) 
        If pFieldNames.FindByText("TimeOutSec").Text = "" Then pFieldNames.Add(New clsComboListMember(153, "TimeOutSec")) 
        If pFieldNames.FindByText("Active").Text = "" Then pFieldNames.Add(New clsComboListMember(154, "Active")) 
        If pFieldNames.FindByText("ActivatingUser").Text = "" Then pFieldNames.Add(New clsComboListMember(155, "ActivatingUser")) 
        If pFieldNames.FindByText("NextRunTime").Text = "" Then pFieldNames.Add(New clsComboListMember(156, "NextRunTime")) 
        If pFieldNames.FindByText("LastRunTime").Text = "" Then pFieldNames.Add(New clsComboListMember(157, "LastRunTime")) 
        If pFieldNames.FindByText("enmJobStatus").Text = "" Then pFieldNames.Add(New clsComboListMember(158, "enmJobStatus")) 
        If pFieldNames.FindByText("WarningMailSent").Text = "" Then pFieldNames.Add(New clsComboListMember(159, "WarningMailSent")) 
        If pFieldNames.FindByText("IsManaged").Text = "" Then pFieldNames.Add(New clsComboListMember(160, "IsManaged")) 
        If pFieldNames.FindByText("LastRunBy").Text = "" Then pFieldNames.Add(New clsComboListMember(161, "LastRunBy")) 
        If pFieldNames.FindByText("c_JobID").Text = "" Then pFieldNames.Add(New clsComboListMember(162, "c_JobID")) 
        If pFieldNames.FindByText("c_UserID").Text = "" Then pFieldNames.Add(New clsComboListMember(163, "c_UserID")) 
        If pFieldNames.FindByText("enmJobAlertType").Text = "" Then pFieldNames.Add(New clsComboListMember(164, "enmJobAlertType")) 
        If pFieldNames.FindByText("OverrideName").Text = "" Then pFieldNames.Add(New clsComboListMember(165, "OverrideName")) 
        If pFieldNames.FindByText("OverrideEmailOrPhone").Text = "" Then pFieldNames.Add(New clsComboListMember(166, "OverrideEmailOrPhone")) 
        If pFieldNames.FindByText("Code").Text = "" Then pFieldNames.Add(New clsComboListMember(167, "Code")) 
        If pFieldNames.FindByText("Name").Text = "" Then pFieldNames.Add(New clsComboListMember(168, "Name")) 
        If pFieldNames.FindByText("NameLoc").Text = "" Then pFieldNames.Add(New clsComboListMember(169, "NameLoc")) 
        If pFieldNames.FindByText("Culture").Text = "" Then pFieldNames.Add(New clsComboListMember(170, "Culture")) 
        If pFieldNames.FindByText("TimeOccurred").Text = "" Then pFieldNames.Add(New clsComboListMember(171, "TimeOccurred")) 
        If pFieldNames.FindByText("FaultNumber").Text = "" Then pFieldNames.Add(New clsComboListMember(172, "FaultNumber")) 
        If pFieldNames.FindByText("SystemName").Text = "" Then pFieldNames.Add(New clsComboListMember(173, "SystemName")) 
        If pFieldNames.FindByText("CallingApplication").Text = "" Then pFieldNames.Add(New clsComboListMember(174, "CallingApplication")) 
        If pFieldNames.FindByText("AffectedUserID").Text = "" Then pFieldNames.Add(New clsComboListMember(175, "AffectedUserID")) 
        If pFieldNames.FindByText("CallingApplicationVersion").Text = "" Then pFieldNames.Add(New clsComboListMember(176, "CallingApplicationVersion")) 
        If pFieldNames.FindByText("CallingFunctionWithinApplication").Text = "" Then pFieldNames.Add(New clsComboListMember(177, "CallingFunctionWithinApplication")) 
        If pFieldNames.FindByText("FreeText").Text = "" Then pFieldNames.Add(New clsComboListMember(178, "FreeText")) 
        If pFieldNames.FindByText("FaultingAssembly").Text = "" Then pFieldNames.Add(New clsComboListMember(179, "FaultingAssembly")) 
        If pFieldNames.FindByText("AssemblyEntryPoint").Text = "" Then pFieldNames.Add(New clsComboListMember(180, "AssemblyEntryPoint")) 
        If pFieldNames.FindByText("FaultingClass").Text = "" Then pFieldNames.Add(New clsComboListMember(181, "FaultingClass")) 
        If pFieldNames.FindByText("FaultingFunction").Text = "" Then pFieldNames.Add(New clsComboListMember(182, "FaultingFunction")) 
        If pFieldNames.FindByText("FaultingFunctionParameters").Text = "" Then pFieldNames.Add(New clsComboListMember(183, "FaultingFunctionParameters")) 
        If pFieldNames.FindByText("FaultIdent").Text = "" Then pFieldNames.Add(New clsComboListMember(184, "FaultIdent")) 
        If pFieldNames.FindByText("FaultDescription").Text = "" Then pFieldNames.Add(New clsComboListMember(185, "FaultDescription")) 
        If pFieldNames.FindByText("MessageSentToUser").Text = "" Then pFieldNames.Add(New clsComboListMember(186, "MessageSentToUser")) 
        If pFieldNames.FindByText("ActionSentToUser").Text = "" Then pFieldNames.Add(New clsComboListMember(187, "ActionSentToUser")) 
        If pFieldNames.FindByText("enmFaultType_FaultType").Text = "" Then pFieldNames.Add(New clsComboListMember(188, "enmFaultType_FaultType")) 
        If pFieldNames.FindByText("enmFaultSeverity_FaultSeverity").Text = "" Then pFieldNames.Add(New clsComboListMember(189, "enmFaultSeverity_FaultSeverity")) 
        If pFieldNames.FindByText("c_LoggedLoginID").Text = "" Then pFieldNames.Add(New clsComboListMember(190, "c_LoggedLoginID")) 
        If pFieldNames.FindByText("Thread").Text = "" Then pFieldNames.Add(New clsComboListMember(191, "Thread")) 
        If pFieldNames.FindByText("lkpUserIdentityType").Text = "" Then pFieldNames.Add(New clsComboListMember(192, "lkpUserIdentityType")) 
        If pFieldNames.FindByText("lkpUserIdentityTypeName").Text = "" Then pFieldNames.Add(New clsComboListMember(193, "lkpUserIdentityTypeName")) 
        If pFieldNames.FindByText("clc_DateOccurred").Text = "" Then pFieldNames.Add(New clsComboListMember(194, "clc_DateOccurred")) 
        If pFieldNames.FindByText("clc_MonthOccurred").Text = "" Then pFieldNames.Add(New clsComboListMember(195, "clc_MonthOccurred")) 
        If pFieldNames.FindByText("c_JobID").Text = "" Then pFieldNames.Add(New clsComboListMember(196, "c_JobID")) 
        If pFieldNames.FindByText("WhenStarted").Text = "" Then pFieldNames.Add(New clsComboListMember(197, "WhenStarted")) 
        If pFieldNames.FindByText("ActivatingUser").Text = "" Then pFieldNames.Add(New clsComboListMember(198, "ActivatingUser")) 
        If pFieldNames.FindByText("LastRunBy").Text = "" Then pFieldNames.Add(New clsComboListMember(199, "LastRunBy")) 
        If pFieldNames.FindByText("ExecutionTimeSec").Text = "" Then pFieldNames.Add(New clsComboListMember(200, "ExecutionTimeSec")) 
        If pFieldNames.FindByText("enmRunStatus_JobStatus").Text = "" Then pFieldNames.Add(New clsComboListMember(201, "enmRunStatus_JobStatus")) 
        If pFieldNames.FindByText("Remarks").Text = "" Then pFieldNames.Add(New clsComboListMember(202, "Remarks")) 
        If pFieldNames.FindByText("c_LoggedAlertID").Text = "" Then pFieldNames.Add(New clsComboListMember(203, "c_LoggedAlertID")) 
        If pFieldNames.FindByText("SuccessCount").Text = "" Then pFieldNames.Add(New clsComboListMember(204, "SuccessCount")) 
        If pFieldNames.FindByText("FailureCount").Text = "" Then pFieldNames.Add(New clsComboListMember(205, "FailureCount")) 
        If pFieldNames.FindByText("UserName").Text = "" Then pFieldNames.Add(New clsComboListMember(206, "UserName")) 
        If pFieldNames.FindByText("UserFullName").Text = "" Then pFieldNames.Add(New clsComboListMember(207, "UserFullName")) 
        If pFieldNames.FindByText("TimeLoggedIn").Text = "" Then pFieldNames.Add(New clsComboListMember(208, "TimeLoggedIn")) 
        If pFieldNames.FindByText("ApplicationName").Text = "" Then pFieldNames.Add(New clsComboListMember(209, "ApplicationName")) 
        If pFieldNames.FindByText("lkpUserIdentityType").Text = "" Then pFieldNames.Add(New clsComboListMember(210, "lkpUserIdentityType")) 
        If pFieldNames.FindByText("lkpUserIdentityTypeName").Text = "" Then pFieldNames.Add(New clsComboListMember(211, "lkpUserIdentityTypeName")) 
        If pFieldNames.FindByText("Roles").Text = "" Then pFieldNames.Add(New clsComboListMember(212, "Roles")) 
        If pFieldNames.FindByText("TimeLoggedOut").Text = "" Then pFieldNames.Add(New clsComboListMember(213, "TimeLoggedOut")) 
        If pFieldNames.FindByText("LoginFaultNumber").Text = "" Then pFieldNames.Add(New clsComboListMember(214, "LoginFaultNumber")) 
        If pFieldNames.FindByText("EnvironmentUserName").Text = "" Then pFieldNames.Add(New clsComboListMember(215, "EnvironmentUserName")) 
        If pFieldNames.FindByText("EnvironmentMachineName").Text = "" Then pFieldNames.Add(New clsComboListMember(216, "EnvironmentMachineName")) 
        If pFieldNames.FindByText("EnvironmentUserDomainName").Text = "" Then pFieldNames.Add(New clsComboListMember(217, "EnvironmentUserDomainName")) 
        If pFieldNames.FindByText("DnsGetHostName").Text = "" Then pFieldNames.Add(New clsComboListMember(218, "DnsGetHostName")) 
        If pFieldNames.FindByText("AddressList").Text = "" Then pFieldNames.Add(New clsComboListMember(219, "AddressList")) 
        If pFieldNames.FindByText("ComputerMACAddress").Text = "" Then pFieldNames.Add(New clsComboListMember(220, "ComputerMACAddress")) 
        If pFieldNames.FindByText("SystemDiskVolumeSerialNo").Text = "" Then pFieldNames.Add(New clsComboListMember(221, "SystemDiskVolumeSerialNo")) 
        If pFieldNames.FindByText("LocalTime").Text = "" Then pFieldNames.Add(New clsComboListMember(222, "LocalTime")) 
        If pFieldNames.FindByText("GmtTime").Text = "" Then pFieldNames.Add(New clsComboListMember(223, "GmtTime")) 
        If pFieldNames.FindByText("AccessingComputerDetails").Text = "" Then pFieldNames.Add(New clsComboListMember(224, "AccessingComputerDetails")) 
        If pFieldNames.FindByText("UICulture").Text = "" Then pFieldNames.Add(New clsComboListMember(225, "UICulture")) 
        If pFieldNames.FindByText("TotalPhysicalMemoryKb").Text = "" Then pFieldNames.Add(New clsComboListMember(226, "TotalPhysicalMemoryKb")) 
        If pFieldNames.FindByText("AvailablePhysicalMemoryKb").Text = "" Then pFieldNames.Add(New clsComboListMember(227, "AvailablePhysicalMemoryKb")) 
        If pFieldNames.FindByText("ApplicationVersion").Text = "" Then pFieldNames.Add(New clsComboListMember(228, "ApplicationVersion")) 
        If pFieldNames.FindByText("OriginatingIP").Text = "" Then pFieldNames.Add(New clsComboListMember(229, "OriginatingIP")) 
        If pFieldNames.FindByText("enmLanguage").Text = "" Then pFieldNames.Add(New clsComboListMember(230, "enmLanguage")) 
        If pFieldNames.FindByText("HostingAssembly").Text = "" Then pFieldNames.Add(New clsComboListMember(231, "HostingAssembly")) 
        If pFieldNames.FindByText("OriginatingCountry").Text = "" Then pFieldNames.Add(New clsComboListMember(232, "OriginatingCountry")) 
        If pFieldNames.FindByText("clc_DateLoggedIn").Text = "" Then pFieldNames.Add(New clsComboListMember(233, "clc_DateLoggedIn")) 
        If pFieldNames.FindByText("clc_MonthLoggedIn").Text = "" Then pFieldNames.Add(New clsComboListMember(234, "clc_MonthLoggedIn")) 
        If pFieldNames.FindByText("ClientReportedIP").Text = "" Then pFieldNames.Add(New clsComboListMember(235, "ClientReportedIP")) 
        If pFieldNames.FindByText("ClientReportedCountry").Text = "" Then pFieldNames.Add(New clsComboListMember(236, "ClientReportedCountry")) 
        If pFieldNames.FindByText("IPAdditionalDetails").Text = "" Then pFieldNames.Add(New clsComboListMember(237, "IPAdditionalDetails")) 
        If pFieldNames.FindByText("c_LoggedLoginID").Text = "" Then pFieldNames.Add(New clsComboListMember(238, "c_LoggedLoginID")) 
        If pFieldNames.FindByText("TimeAccessed").Text = "" Then pFieldNames.Add(New clsComboListMember(239, "TimeAccessed")) 
        If pFieldNames.FindByText("c_UserID").Text = "" Then pFieldNames.Add(New clsComboListMember(240, "c_UserID")) 
        If pFieldNames.FindByText("CallingFunctionWithinApplication").Text = "" Then pFieldNames.Add(New clsComboListMember(241, "CallingFunctionWithinApplication")) 
        If pFieldNames.FindByText("EntryPoint").Text = "" Then pFieldNames.Add(New clsComboListMember(242, "EntryPoint")) 
        If pFieldNames.FindByText("Process").Text = "" Then pFieldNames.Add(New clsComboListMember(243, "Process")) 
        If pFieldNames.FindByText("Thread").Text = "" Then pFieldNames.Add(New clsComboListMember(244, "Thread")) 
        If pFieldNames.FindByText("enmParentLookupType_Lookup").Text = "" Then pFieldNames.Add(New clsComboListMember(245, "enmParentLookupType_Lookup")) 
        If pFieldNames.FindByText("ParentCode").Text = "" Then pFieldNames.Add(New clsComboListMember(246, "ParentCode")) 
        If pFieldNames.FindByText("enmLookupType_Lookup").Text = "" Then pFieldNames.Add(New clsComboListMember(247, "enmLookupType_Lookup")) 
        If pFieldNames.FindByText("Code").Text = "" Then pFieldNames.Add(New clsComboListMember(248, "Code")) 
        If pFieldNames.FindByText("locText").Text = "" Then pFieldNames.Add(New clsComboListMember(249, "locText")) 
        If pFieldNames.FindByText("locDescription").Text = "" Then pFieldNames.Add(New clsComboListMember(250, "locDescription")) 
        If pFieldNames.FindByText("enmMessagingMode").Text = "" Then pFieldNames.Add(New clsComboListMember(251, "enmMessagingMode")) 
        If pFieldNames.FindByText("RecipientEmail").Text = "" Then pFieldNames.Add(New clsComboListMember(252, "RecipientEmail")) 
        If pFieldNames.FindByText("WhenSent").Text = "" Then pFieldNames.Add(New clsComboListMember(253, "WhenSent")) 
        If pFieldNames.FindByText("Subject").Text = "" Then pFieldNames.Add(New clsComboListMember(254, "Subject")) 
        If pFieldNames.FindByText("Body").Text = "" Then pFieldNames.Add(New clsComboListMember(255, "Body")) 
        If pFieldNames.FindByText("WhenSeen").Text = "" Then pFieldNames.Add(New clsComboListMember(256, "WhenSeen")) 
        If pFieldNames.FindByText("WasSeen").Text = "" Then pFieldNames.Add(New clsComboListMember(257, "WasSeen")) 
        If pFieldNames.FindByText("CellOrEmail").Text = "" Then pFieldNames.Add(New clsComboListMember(258, "CellOrEmail")) 
        If pFieldNames.FindByText("ProtectedFunction").Text = "" Then pFieldNames.Add(New clsComboListMember(259, "ProtectedFunction")) 
        If pFieldNames.FindByText("enoCode").Text = "" Then pFieldNames.Add(New clsComboListMember(260, "enoCode")) 
        If pFieldNames.FindByText("AttemptNo").Text = "" Then pFieldNames.Add(New clsComboListMember(261, "AttemptNo")) 
        If pFieldNames.FindByText("IsSuccessful").Text = "" Then pFieldNames.Add(New clsComboListMember(262, "IsSuccessful")) 
        If pFieldNames.FindByText("LastAccessingIP").Text = "" Then pFieldNames.Add(New clsComboListMember(263, "LastAccessingIP")) 
        If pFieldNames.FindByText("LastAccessingCountry").Text = "" Then pFieldNames.Add(New clsComboListMember(264, "LastAccessingCountry")) 
        If pFieldNames.FindByText("enmUILang_Language").Text = "" Then pFieldNames.Add(New clsComboListMember(265, "enmUILang_Language")) 
        If pFieldNames.FindByText("WhenCreated").Text = "" Then pFieldNames.Add(New clsComboListMember(266, "WhenCreated")) 
        If pFieldNames.FindByText("WhenAccessed").Text = "" Then pFieldNames.Add(New clsComboListMember(267, "WhenAccessed")) 
        If pFieldNames.FindByText("WhenExpires").Text = "" Then pFieldNames.Add(New clsComboListMember(268, "WhenExpires")) 
        If pFieldNames.FindByText("Details").Text = "" Then pFieldNames.Add(New clsComboListMember(269, "Details")) 
        If pFieldNames.FindByText("c_UserID").Text = "" Then pFieldNames.Add(New clsComboListMember(270, "c_UserID")) 
        If pFieldNames.FindByText("enmObjectType").Text = "" Then pFieldNames.Add(New clsComboListMember(271, "enmObjectType")) 
        If pFieldNames.FindByText("Object").Text = "" Then pFieldNames.Add(New clsComboListMember(272, "Object")) 
        If pFieldNames.FindByText("Item").Text = "" Then pFieldNames.Add(New clsComboListMember(273, "Item")) 
        If pFieldNames.FindByText("c_ObjectToTranslateID").Text = "" Then pFieldNames.Add(New clsComboListMember(274, "c_ObjectToTranslateID")) 
        If pFieldNames.FindByText("Instance").Text = "" Then pFieldNames.Add(New clsComboListMember(275, "Instance")) 
        If pFieldNames.FindByText("DefaultText").Text = "" Then pFieldNames.Add(New clsComboListMember(276, "DefaultText")) 
        If pFieldNames.FindByText("enmLanguage").Text = "" Then pFieldNames.Add(New clsComboListMember(277, "enmLanguage")) 
        If pFieldNames.FindByText("Text").Text = "" Then pFieldNames.Add(New clsComboListMember(278, "Text")) 
        If pFieldNames.FindByText("InstanceUniqueText").Text = "" Then pFieldNames.Add(New clsComboListMember(279, "InstanceUniqueText")) 
        If pFieldNames.FindByText("c_ProcessID").Text = "" Then pFieldNames.Add(New clsComboListMember(280, "c_ProcessID")) 
        If pFieldNames.FindByText("c_RoleID").Text = "" Then pFieldNames.Add(New clsComboListMember(281, "c_RoleID")) 
        If pFieldNames.FindByText("CanDo").Text = "" Then pFieldNames.Add(New clsComboListMember(282, "CanDo")) 
        If pFieldNames.FindByText("Name").Text = "" Then pFieldNames.Add(New clsComboListMember(283, "Name")) 
        If pFieldNames.FindByText("DateChecked").Text = "" Then pFieldNames.Add(New clsComboListMember(284, "DateChecked")) 
        If pFieldNames.FindByText("Name").Text = "" Then pFieldNames.Add(New clsComboListMember(285, "Name")) 
        If pFieldNames.FindByText("BaseRoleID").Text = "" Then pFieldNames.Add(New clsComboListMember(286, "BaseRoleID")) 
        If pFieldNames.FindByText("TableName").Text = "" Then pFieldNames.Add(New clsComboListMember(287, "TableName")) 
        If pFieldNames.FindByText("RowId").Text = "" Then pFieldNames.Add(New clsComboListMember(288, "RowId")) 
        If pFieldNames.FindByText("Operation").Text = "" Then pFieldNames.Add(New clsComboListMember(289, "Operation")) 
        If pFieldNames.FindByText("OccurredAt").Text = "" Then pFieldNames.Add(New clsComboListMember(290, "OccurredAt")) 
        If pFieldNames.FindByText("SqlCurrentUser").Text = "" Then pFieldNames.Add(New clsComboListMember(291, "SqlCurrentUser")) 
        If pFieldNames.FindByText("ChangedByUser").Text = "" Then pFieldNames.Add(New clsComboListMember(292, "ChangedByUser")) 
        If pFieldNames.FindByText("ActiveLoginID").Text = "" Then pFieldNames.Add(New clsComboListMember(293, "ActiveLoginID")) 
        If pFieldNames.FindByText("SqlSystemUser").Text = "" Then pFieldNames.Add(New clsComboListMember(294, "SqlSystemUser")) 
        If pFieldNames.FindByText("SqlAppName").Text = "" Then pFieldNames.Add(New clsComboListMember(295, "SqlAppName")) 
        If pFieldNames.FindByText("SqlHostName").Text = "" Then pFieldNames.Add(New clsComboListMember(296, "SqlHostName")) 
        If pFieldNames.FindByText("Changes").Text = "" Then pFieldNames.Add(New clsComboListMember(297, "Changes")) 
        If pFieldNames.FindByText("Group").Text = "" Then pFieldNames.Add(New clsComboListMember(298, "Group")) 
        If pFieldNames.FindByText("SettingName").Text = "" Then pFieldNames.Add(New clsComboListMember(299, "SettingName")) 
        If pFieldNames.FindByText("spt_SettingValue").Text = "" Then pFieldNames.Add(New clsComboListMember(300, "spt_SettingValue")) 
        If pFieldNames.FindByText("enmSystemDefaultType").Text = "" Then pFieldNames.Add(New clsComboListMember(301, "enmSystemDefaultType")) 
        If pFieldNames.FindByText("Description").Text = "" Then pFieldNames.Add(New clsComboListMember(302, "Description")) 
        If pFieldNames.FindByText("Name").Text = "" Then pFieldNames.Add(New clsComboListMember(303, "Name")) 
        If pFieldNames.FindByText("DefaultTextFields").Text = "" Then pFieldNames.Add(New clsComboListMember(304, "DefaultTextFields")) 
        If pFieldNames.FindByText("UsedForIdentity").Text = "" Then pFieldNames.Add(New clsComboListMember(305, "UsedForIdentity")) 
        If pFieldNames.FindByText("IsSingleRow").Text = "" Then pFieldNames.Add(New clsComboListMember(306, "IsSingleRow")) 
        If pFieldNames.FindByText("CanAdd").Text = "" Then pFieldNames.Add(New clsComboListMember(307, "CanAdd")) 
        If pFieldNames.FindByText("CanEdit").Text = "" Then pFieldNames.Add(New clsComboListMember(308, "CanEdit")) 
        If pFieldNames.FindByText("CanDelete").Text = "" Then pFieldNames.Add(New clsComboListMember(309, "CanDelete")) 
        If pFieldNames.FindByText("AuditAdd").Text = "" Then pFieldNames.Add(New clsComboListMember(310, "AuditAdd")) 
        If pFieldNames.FindByText("AuditEdit").Text = "" Then pFieldNames.Add(New clsComboListMember(311, "AuditEdit")) 
        If pFieldNames.FindByText("AuditDelete").Text = "" Then pFieldNames.Add(New clsComboListMember(312, "AuditDelete")) 
        If pFieldNames.FindByText("TrackRowChangers").Text = "" Then pFieldNames.Add(New clsComboListMember(313, "TrackRowChangers")) 
        If pFieldNames.FindByText("CreateUIMenu").Text = "" Then pFieldNames.Add(New clsComboListMember(314, "CreateUIMenu")) 
        If pFieldNames.FindByText("CreateUICollection").Text = "" Then pFieldNames.Add(New clsComboListMember(315, "CreateUICollection")) 
        If pFieldNames.FindByText("CreateUIEntity").Text = "" Then pFieldNames.Add(New clsComboListMember(316, "CreateUIEntity")) 
        If pFieldNames.FindByText("SortOrder").Text = "" Then pFieldNames.Add(New clsComboListMember(317, "SortOrder")) 
        If pFieldNames.FindByText("TableName").Text = "" Then pFieldNames.Add(New clsComboListMember(318, "TableName")) 
        If pFieldNames.FindByText("NumberOfRows").Text = "" Then pFieldNames.Add(New clsComboListMember(319, "NumberOfRows")) 
        If pFieldNames.FindByText("ReservedSizeKb").Text = "" Then pFieldNames.Add(New clsComboListMember(320, "ReservedSizeKb")) 
        If pFieldNames.FindByText("DataSizeKb").Text = "" Then pFieldNames.Add(New clsComboListMember(321, "DataSizeKb")) 
        If pFieldNames.FindByText("IndexSizeKb").Text = "" Then pFieldNames.Add(New clsComboListMember(322, "IndexSizeKb")) 
        If pFieldNames.FindByText("UnusedSizeKb").Text = "" Then pFieldNames.Add(New clsComboListMember(323, "UnusedSizeKb")) 
        If pFieldNames.FindByText("UserName").Text = "" Then pFieldNames.Add(New clsComboListMember(324, "UserName")) 
        If pFieldNames.FindByText("LastName").Text = "" Then pFieldNames.Add(New clsComboListMember(325, "LastName")) 
        If pFieldNames.FindByText("FirstName").Text = "" Then pFieldNames.Add(New clsComboListMember(326, "FirstName")) 
        If pFieldNames.FindByText("clc_FullName").Text = "" Then pFieldNames.Add(New clsComboListMember(327, "clc_FullName")) 
        If pFieldNames.FindByText("NationalIDNo").Text = "" Then pFieldNames.Add(New clsComboListMember(328, "NationalIDNo")) 
        If pFieldNames.FindByText("Address").Text = "" Then pFieldNames.Add(New clsComboListMember(329, "Address")) 
        If pFieldNames.FindByText("City").Text = "" Then pFieldNames.Add(New clsComboListMember(330, "City")) 
        If pFieldNames.FindByText("ProvinceState").Text = "" Then pFieldNames.Add(New clsComboListMember(331, "ProvinceState")) 
        If pFieldNames.FindByText("PostalCode").Text = "" Then pFieldNames.Add(New clsComboListMember(332, "PostalCode")) 
        If pFieldNames.FindByText("Country").Text = "" Then pFieldNames.Add(New clsComboListMember(333, "Country")) 
        If pFieldNames.FindByText("PhoneNumber").Text = "" Then pFieldNames.Add(New clsComboListMember(334, "PhoneNumber")) 
        If pFieldNames.FindByText("Email").Text = "" Then pFieldNames.Add(New clsComboListMember(335, "Email")) 
        If pFieldNames.FindByText("enoPassword").Text = "" Then pFieldNames.Add(New clsComboListMember(336, "enoPassword")) 
        If pFieldNames.FindByText("DatePasswordChanged").Text = "" Then pFieldNames.Add(New clsComboListMember(337, "DatePasswordChanged")) 
        If pFieldNames.FindByText("enmType_UserIdentityType").Text = "" Then pFieldNames.Add(New clsComboListMember(338, "enmType_UserIdentityType")) 
        If pFieldNames.FindByText("IDinType").Text = "" Then pFieldNames.Add(New clsComboListMember(339, "IDinType")) 
        If pFieldNames.FindByText("RequiresComputerIdentification").Text = "" Then pFieldNames.Add(New clsComboListMember(340, "RequiresComputerIdentification")) 
        If pFieldNames.FindByText("EnableSimultaneousLogins").Text = "" Then pFieldNames.Add(New clsComboListMember(341, "EnableSimultaneousLogins")) 
        If pFieldNames.FindByText("clc_DateActivated").Text = "" Then pFieldNames.Add(New clsComboListMember(342, "clc_DateActivated")) 
        If pFieldNames.FindByText("IsDisabled").Text = "" Then pFieldNames.Add(New clsComboListMember(343, "IsDisabled")) 
        If pFieldNames.FindByText("ExpiryDate").Text = "" Then pFieldNames.Add(New clsComboListMember(344, "ExpiryDate")) 
        If pFieldNames.FindByText("Comments").Text = "" Then pFieldNames.Add(New clsComboListMember(345, "Comments")) 
        If pFieldNames.FindByText("LastPasswords").Text = "" Then pFieldNames.Add(New clsComboListMember(346, "LastPasswords")) 
        If pFieldNames.FindByText("spl_Applications").Text = "" Then pFieldNames.Add(New clsComboListMember(347, "spl_Applications")) 
        If pFieldNames.FindByText("enmLanguage").Text = "" Then pFieldNames.Add(New clsComboListMember(348, "enmLanguage")) 
        If pFieldNames.FindByText("IsLockedOut").Text = "" Then pFieldNames.Add(New clsComboListMember(349, "IsLockedOut")) 
        If pFieldNames.FindByText("RoleID").Text = "" Then pFieldNames.Add(New clsComboListMember(350, "RoleID")) 
        If pFieldNames.FindByText("enmAuthenticationMethod").Text = "" Then pFieldNames.Add(New clsComboListMember(351, "enmAuthenticationMethod")) 
        If pFieldNames.FindByText("RequiresFixedIP").Text = "" Then pFieldNames.Add(New clsComboListMember(352, "RequiresFixedIP")) 
        If pFieldNames.FindByText("enmMessagingMode").Text = "" Then pFieldNames.Add(New clsComboListMember(353, "enmMessagingMode")) 
        If pFieldNames.FindByText("spt_LoggedInIP").Text = "" Then pFieldNames.Add(New clsComboListMember(354, "spt_LoggedInIP")) 
        If pFieldNames.FindByText("enoApprovalCode").Text = "" Then pFieldNames.Add(New clsComboListMember(355, "enoApprovalCode")) 
        If pFieldNames.FindByText("ApprovalFunctionName").Text = "" Then pFieldNames.Add(New clsComboListMember(356, "ApprovalFunctionName")) 
        If pFieldNames.FindByText("ApprovalTime").Text = "" Then pFieldNames.Add(New clsComboListMember(357, "ApprovalTime")) 
        If pFieldNames.FindByText("spt_LastSuccessfulLogin").Text = "" Then pFieldNames.Add(New clsComboListMember(358, "spt_LastSuccessfulLogin")) 
        If pFieldNames.FindByText("PasswordNeverExpires").Text = "" Then pFieldNames.Add(New clsComboListMember(359, "PasswordNeverExpires")) 
        If pFieldNames.FindByText("lkpSecurityQuestion1_SecurityQuestion").Text = "" Then pFieldNames.Add(New clsComboListMember(360, "lkpSecurityQuestion1_SecurityQuestion")) 
        If pFieldNames.FindByText("entSecurityQuestion1Response").Text = "" Then pFieldNames.Add(New clsComboListMember(361, "entSecurityQuestion1Response")) 
        If pFieldNames.FindByText("lkpSecurityQuestion2_SecurityQuestion").Text = "" Then pFieldNames.Add(New clsComboListMember(362, "lkpSecurityQuestion2_SecurityQuestion")) 
        If pFieldNames.FindByText("entSecurityQuestion2Response").Text = "" Then pFieldNames.Add(New clsComboListMember(363, "entSecurityQuestion2Response")) 
        If pFieldNames.FindByText("lkpSecurityQuestion3_SecurityQuestion").Text = "" Then pFieldNames.Add(New clsComboListMember(364, "lkpSecurityQuestion3_SecurityQuestion")) 
        If pFieldNames.FindByText("entSecurityQuestion3Response").Text = "" Then pFieldNames.Add(New clsComboListMember(365, "entSecurityQuestion3Response")) 
        If pFieldNames.FindByText("entPIN").Text = "" Then pFieldNames.Add(New clsComboListMember(366, "entPIN")) 
        If pFieldNames.FindByText("c_UserID").Text = "" Then pFieldNames.Add(New clsComboListMember(367, "c_UserID")) 
        If pFieldNames.FindByText("ApplicationName").Text = "" Then pFieldNames.Add(New clsComboListMember(368, "ApplicationName")) 
        If pFieldNames.FindByText("ApplicationIdentifier").Text = "" Then pFieldNames.Add(New clsComboListMember(369, "ApplicationIdentifier")) 
        If pFieldNames.FindByText("enoKey").Text = "" Then pFieldNames.Add(New clsComboListMember(370, "enoKey")) 
        If pFieldNames.FindByText("ExternalIPAtCreation").Text = "" Then pFieldNames.Add(New clsComboListMember(371, "ExternalIPAtCreation")) 
        If pFieldNames.FindByText("CountryAtCreation").Text = "" Then pFieldNames.Add(New clsComboListMember(372, "CountryAtCreation")) 
        If pFieldNames.FindByText("LastAccessTime").Text = "" Then pFieldNames.Add(New clsComboListMember(373, "LastAccessTime")) 
        If pFieldNames.FindByText("LoggedLoginID").Text = "" Then pFieldNames.Add(New clsComboListMember(374, "LoggedLoginID")) 
        If pFieldNames.FindByText("c_UserID").Text = "" Then pFieldNames.Add(New clsComboListMember(375, "c_UserID")) 
        If pFieldNames.FindByText("ApplicationName").Text = "" Then pFieldNames.Add(New clsComboListMember(376, "ApplicationName")) 
        If pFieldNames.FindByText("ComputerIdentifier").Text = "" Then pFieldNames.Add(New clsComboListMember(377, "ComputerIdentifier")) 
        If pFieldNames.FindByText("ComputerName").Text = "" Then pFieldNames.Add(New clsComboListMember(378, "ComputerName")) 
        If pFieldNames.FindByText("ExternalIP").Text = "" Then pFieldNames.Add(New clsComboListMember(379, "ExternalIP")) 
        If pFieldNames.FindByText("HasPermission").Text = "" Then pFieldNames.Add(New clsComboListMember(380, "HasPermission")) 
        If pFieldNames.FindByText("Comments").Text = "" Then pFieldNames.Add(New clsComboListMember(381, "Comments")) 
        If pFieldNames.FindByText("LastAccessTime").Text = "" Then pFieldNames.Add(New clsComboListMember(382, "LastAccessTime")) 
        If pFieldNames.FindByText("LoggedLoginID").Text = "" Then pFieldNames.Add(New clsComboListMember(383, "LoggedLoginID")) 
        If pFieldNames.FindByText("c_UserID").Text = "" Then pFieldNames.Add(New clsComboListMember(384, "c_UserID")) 
        If pFieldNames.FindByText("ApplicationName").Text = "" Then pFieldNames.Add(New clsComboListMember(385, "ApplicationName")) 
        If pFieldNames.FindByText("LastLoggedLoginID").Text = "" Then pFieldNames.Add(New clsComboListMember(386, "LastLoggedLoginID")) 
        If pFieldNames.FindByText("LoginTime").Text = "" Then pFieldNames.Add(New clsComboListMember(387, "LoginTime")) 
        If pFieldNames.FindByText("LogoutTime").Text = "" Then pFieldNames.Add(New clsComboListMember(388, "LogoutTime")) 
        pFieldNames.SortByText() 
        If pFieldNames IsNot Nothing AndAlso pFieldNames.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo02Label) 
          .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo02 
          .MakeSmart() 
          .LoadControl(pFieldNames, GetChoose(_Requester)) 
          .TabIndex = 10 
        End With 
 
        .String01Label.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.ChangedByUser), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.ChangedByUser), "Changed By User") 
        .String01Text.Text = "" 
        .String01Text.TabIndex = 11 
        With .String01WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 12 
        End With 
        .flpFilter.Controls.Add(.String01Label) 
        .flpFilter.Controls.Add(.String01Text) 
        .flpFilter.Controls.Add(.String01LblWCType) 
        .flpFilter.Controls.Add(.String01WCType) 
 
        .Text03Label.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.ActiveLoginID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.ActiveLoginID), "Active Login ID") 
        .Text03From.Text = "" 
        .Text03From.TabIndex = 13 
        .Text03To.Text = "" 
        .Text03To.TabIndex = 14 
        .flpFilter.Controls.Add(.Text03Label) 
        .flpFilter.Controls.Add(.Text03From) 
        .flpFilter.Controls.Add(.Text03LblTo) 
        .flpFilter.Controls.Add(.Text03To) 
 
        .Text04Label.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.ID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.ID), "ID") 
        .Text04From.Text = "" 
        .Text04From.TabIndex = 15 
        .Text04To.Text = "" 
        .Text04To.TabIndex = 16 
        .flpFilter.Controls.Add(.Text04Label) 
        .flpFilter.Controls.Add(.Text04From) 
        .flpFilter.Controls.Add(.Text04LblTo) 
        .flpFilter.Controls.Add(.Text04To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.OriginalID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.OriginalID), "Original ID") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 17 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.TableName), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.TableName), "Table Name") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 18 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.RowID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.RowID), "Row ID") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 19 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .lblGroupBy04.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.OccurredAt), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.OccurredAt), "Occurred At") 
        .chkGroupBy04.Checked = False 
        .chkGroupBy04.TabIndex = 20 
        .flpGroupBy.Controls.Add(.lblGroupBy04) 
        .flpGroupBy.Controls.Add(.chkGroupBy04) 
 
        .lblGroupBy05.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.FieldName), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.FieldName), "Field Name") 
        .chkGroupBy05.Checked = False 
        .chkGroupBy05.TabIndex = 21 
        .flpGroupBy.Controls.Add(.lblGroupBy05) 
        .flpGroupBy.Controls.Add(.chkGroupBy05) 
 
        .lblGroupBy06.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.ChangedByUser), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.ChangedByUser), "Changed By User") 
        .chkGroupBy06.Checked = False 
        .chkGroupBy06.TabIndex = 22 
        .flpGroupBy.Controls.Add(.lblGroupBy06) 
        .flpGroupBy.Controls.Add(.chkGroupBy06) 
 
        .lblGroupBy07.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.ActiveLoginID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.ActiveLoginID), "Active Login ID") 
        .chkGroupBy07.Checked = False 
        .chkGroupBy07.TabIndex = 23 
        .flpGroupBy.Controls.Add(.lblGroupBy07) 
        .flpGroupBy.Controls.Add(.chkGroupBy07) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.OriginalID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.OriginalID), "Original ID") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 24 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.RowID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.RowID), "Row ID") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 25 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
        .lblSumField03.Text = If(_ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText.ContainsKey(csAuditIndexed.enmProperty.ActiveLoginID), _ctlAuditIndexedCol.LoadParameters.ColumnsHeaderText(csAuditIndexed.enmProperty.ActiveLoginID), "Active Login ID") 
        .chkSumField03.Checked = False 
        .chkSumField03.TabIndex = 26 
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
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pOriginalIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pOriginalIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pOriginalIDTo = pOriginalIDFrom 
          End If 
          _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.OriginalIDFrom, pOriginalIDFrom) 
          _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.OriginalIDTo, pOriginalIDTo) 
        End If 
      End If 
      If .Combo01.SelectedItem IsNot Nothing Then 
        pTableName = CType(.Combo01.SelectedItem, clsComboListMember).Text 
        pTableNameWildcardType = clsEnums.enmWildCardType.None 
        _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.TableName, pTableName) 
        _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.TableNameWildcardType, pTableNameWildcardType) 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pRowIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pRowIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pRowIDTo = pRowIDFrom 
          End If 
          _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.RowIDFrom, pRowIDFrom) 
          _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.RowIDTo, pRowIDTo) 
        End If 
      End If 
      If .Date01From.Checked OrElse .Date01To.Checked Then 
        pOccurredAtStart = .Date01From.Value 
        pOccurredAtEnd = .Date01To.Value 
        _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.OccurredAtStart, pOccurredAtStart) 
        _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.OccurredAtEnd, pOccurredAtEnd) 
      End If 
      If .Combo02.SelectedItem IsNot Nothing Then 
        pFieldName = CType(.Combo02.SelectedItem, clsComboListMember).Text 
        pFieldNameWildcardType = clsEnums.enmWildCardType.None 
        _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.FieldName, pFieldName) 
        _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.FieldNameWildcardType, pFieldNameWildcardType) 
      End If 
      If .String01Text.Text <> "" Then 
        pChangedByUser = .String01Text.Text 
        pChangedByUserWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.ChangedByUser, pChangedByUser) 
        _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.ChangedByUserWildcardType, pChangedByUserWildcardType) 
      End If 
      If .Text03From.Text <> "" Then 
        If IsNumeric(.Text03From.Text) Then 
          pActiveLoginIDFrom = ccHelper.ToLong(.Text03From.Text) 
          If .Text03To.Text <> "" Then 
            pActiveLoginIDTo = ccHelper.ToLong(.Text03To.Text) 
          Else 
            pActiveLoginIDTo = pActiveLoginIDFrom 
          End If 
          _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.ActiveLoginIDFrom, pActiveLoginIDFrom) 
          _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.ActiveLoginIDTo, pActiveLoginIDTo) 
        End If 
      End If 
      If .Text04From.Text <> "" Then 
        If IsNumeric(.Text04From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text04From.Text) 
          If .Text04To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text04To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csAuditIndexedCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csAuditIndexedCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csAuditIndexedCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByOriginalID = True 
        pDoSum = True 
        _SearchFilters.Add(csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByOriginalID, pGroupByOriginalID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByTableName = True 
        pDoSum = True 
        _SearchFilters.Add(csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByTableName, pGroupByTableName) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByRowID = True 
        pDoSum = True 
        _SearchFilters.Add(csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByRowID, pGroupByRowID) 
      End If 
      If .chkGroupBy04.Checked = True Then 
        pGroupByOccurredAt = True 
        pDoSum = True 
        _SearchFilters.Add(csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByOccurredAt, pGroupByOccurredAt) 
      End If 
      If .chkGroupBy05.Checked = True Then 
        pGroupByFieldName = True 
        pDoSum = True 
        _SearchFilters.Add(csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByFieldName, pGroupByFieldName) 
      End If 
      If .chkGroupBy06.Checked = True Then 
        pGroupByChangedByUser = True 
        pDoSum = True 
        _SearchFilters.Add(csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByChangedByUser, pGroupByChangedByUser) 
      End If 
      If .chkGroupBy07.Checked = True Then 
        pGroupByActiveLoginID = True 
        pDoSum = True 
        _SearchFilters.Add(csAuditIndexedCol.enmFillSumOnTheFlyParameters.GroupByActiveLoginID, pGroupByActiveLoginID) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumOriginalID = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumRowID = True 
        pDoSum = True 
      End If 
      
      If .chkSumField03.Checked = True Then 
        pSumActiveLoginID = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csAuditIndexedCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csAuditIndexedCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csAuditIndexedCol.enmListDefinition.Dir) Then _SearchFilters.Add(csAuditIndexedCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_AuditIndexedCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_AuditIndexedCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csAuditIndexed.enmProperty.ID, "ID") 
      End With 
      _AuditIndexedCol = New csAuditIndexedCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _AuditIndexedCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _AuditIndexedCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _AuditIndexedCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _AuditIndexedCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _AuditIndexedCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see AuditIndexed" 
      RaiseEvent evtOverrideLoadCtlAuditIndexedCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _AuditIndexedCol = New csAuditIndexedCol() 
      pFault = _AuditIndexedCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_AuditIndexedCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _AuditIndexedCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csAuditIndexed.enmProperty.ID, "Count") 
        If pGroupByOriginalID = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.OriginalID) 
        If pGroupByTableName = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.TableName) 
        If pGroupByRowID = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.RowID) 
        If pGroupByOccurredAt = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.OccurredAt) 
        If pGroupByFieldName = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.FieldName) 
        If pGroupByChangedByUser = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.ChangedByUser) 
        If pGroupByActiveLoginID = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.ActiveLoginID) 
        If pSumOriginalID = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.OriginalID) 
        If pSumRowID = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.RowID) 
        If pSumActiveLoginID = False Then .ColumnsHide.Add(csAuditIndexed.enmProperty.ActiveLoginID) 
        If pGroupByOriginalID = True OrElse pSumOriginalID = True Then If .ColumnsHide.Contains(csAuditIndexed.enmProperty.OriginalID) Then .ColumnsHide.Remove(csAuditIndexed.enmProperty.OriginalID) 
        If pGroupByRowID = True OrElse pSumRowID = True Then If .ColumnsHide.Contains(csAuditIndexed.enmProperty.RowID) Then .ColumnsHide.Remove(csAuditIndexed.enmProperty.RowID) 
        If pGroupByActiveLoginID = True OrElse pSumActiveLoginID = True Then If .ColumnsHide.Contains(csAuditIndexed.enmProperty.ActiveLoginID) Then .ColumnsHide.Remove(csAuditIndexed.enmProperty.ActiveLoginID) 
        .ColumnsHide.Add(csAuditIndexed.enmProperty.Operation) 
        .ColumnsHide.Add(csAuditIndexed.enmProperty.SqlCurrentUser) 
        .ColumnsHide.Add(csAuditIndexed.enmProperty.OldValue) 
        .ColumnsHide.Add(csAuditIndexed.enmProperty.NewValue) 
        .ColumnsHide.Add(csAuditIndexed.enmProperty.SqlSystemUser) 
        .ColumnsHide.Add(csAuditIndexed.enmProperty.SqlAppName) 
        .ColumnsHide.Add(csAuditIndexed.enmProperty.SqlHostName) 
        .ColumnsHide.Add(csAuditIndexed.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlAuditIndexedCol.Visible = True 
    pFault = _ctlAuditIndexedCol.LoadControl(_AuditIndexedCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csAuditIndexedCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csAuditIndexedCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlAuditIndexed.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlAuditIndexed.Controls(0).Name) 
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
 
  Private Sub _ctlAuditIndexedCol_evtTimerTripped() Handles _ctlAuditIndexedCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtAuditIndexedTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlAuditIndexedCol.AuditIndexedCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlAuditIndexedCol.AuditIndexedCol(0).ID 
 
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
    If _AuditIndexedCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csAuditIndexed() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csAuditIndexedCol = CType(CallByName(_AuditIndexedCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csAuditIndexedCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csAuditIndexedCol = CType(CallByName(_AuditIndexedCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csAuditIndexedCol) 
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
                  ccHelper.GetPropertyTypeName(New csAuditIndexedCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csAuditIndexedCol = CType(CallByName(_AuditIndexedCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csAuditIndexedCol) 
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
 
  Private Sub cc_ctlPnlAuditIndexed_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
