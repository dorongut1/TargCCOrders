Public Class ctlPnlc_Enumeration 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlEnumerationCol As ctlc_EnumerationCol 
  Private WithEvents _ctlEnumeration As ctlc_Enumeration 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _EnumerationID As Integer 
 
  'The data holders 
  Private _EnumerationCol As csEnumerationCol 
  Private _Enumeration As csEnumeration 
 
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
  Public Event evtOverrideLoadCboEnumeration(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetEnumerationIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillEnumerationCol(ByRef rEnumerationCol As csEnumerationCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlEnumerationCol(ByRef rLoadParameters As ctlc_EnumerationCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlEnumeration(ByRef rLoadParameters As ctlc_Enumeration.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreEnumerationCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtEnumerationTimerTripped(ByRef rCancel As Boolean) 
  
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
 
    lnkEnumerationCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkEnumeration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vEnumerationID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _EnumerationID = CType(vEnumerationID, Integer) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlEnumeration.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkEnumerationCol.Visible = False 
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
      pFault = LoadCboEnumerations(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _EnumerationID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_EnumerationID) 
      End If 
      ChooseEnumeration() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_Enumeration") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _EnumerationID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyInteger) 
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
    
    If pControlName = "ctlc_Enumeration" OrElse pControlName = "ctlEnumeration" Then 
      lnkEnumeration.ForeColor = Color.Black : lnkEnumeration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkEnumeration.BackColor = Color.Wheat 
      If _ctlEnumeration Is Nothing Then 
        _ctlEnumeration = New ctlc_Enumeration() 
        _ctlEnumeration.Dock = DockStyle.Fill 
        _ctlEnumeration.Controls.RemoveByKey("btnAdd") 
        pnlEnumeration.Controls.Add(_ctlEnumeration) 
        _ctlEnumeration.Visible = False 
      End If 
      If _EnumerationID = 0 Then 
        pnlEnumeration.Visible = False 
      End If 
      'If _Enumeration Is Nothing Then 
      pFault = RefreshCtlEnumeration() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlEnumeration.Enumeration.IsEmpty AndAlso _EnumerationID <> -2 Then 
        pnlEnumeration.Visible = False 
      End If 
      _ctlEnumeration.Name = "ctlc_Enumeration" 
      _ActiveControl = _ctlEnumeration 
      _ctlEnumeration.BringToFront() 
      _ctlEnumeration.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_EnumerationCol" Then 
      lnkEnumerationCol.ForeColor = Color.Black : lnkEnumerationCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkEnumerationCol.BackColor = Color.Wheat 
      If _ctlEnumerationCol Is Nothing Then 
        _ctlEnumerationCol = New ctlc_EnumerationCol() 
        _ctlEnumerationCol.Dock = DockStyle.Fill 
        pnlEnumeration.Controls.Add(_ctlEnumerationCol) 
        _ctlEnumerationCol.Visible = False 
      End If  
      pnlEnumeration.Visible = True 
      If _EnumerationCol Is Nothing Then 
        pFault = RefreshCtlEnumerationCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlEnumerationCol.Name = "ctlc_EnumerationCol" 
      _ActiveControl = _ctlEnumerationCol 
      _ctlEnumerationCol.BringToFront() 
      _ctlEnumerationCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-Enumeration-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("Enumeration", _Requester) 
 
    lnkEnumerationCol.Text = CCTextTranslate("List", _Requester) 
    lnkEnumeration.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlEnumeration.Controls(0) Is _ctlEnumeration Then 
      If _EnumerationID = 0 Then 
        pnlEnumeration.Visible = False 
      End If 
    ElseIf pnlEnumeration.Controls(0) Is _ctlEnumerationCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pEnumerationID As Long = _EnumerationID 
      If ccHelper.IsNumeric(pText) Then _EnumerationID = ccHelper.ToInteger(pText) 
      RaiseEvent evtGetEnumerationIDFromIntelliComboText(pText) 
      If pEnumerationID <> _EnumerationID Then 
        _Enumeration = Nothing 
        pFault = ActivateControl("ctlc_Enumeration") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlEnumeration.Controls(0) Is _ctlEnumeration Then 
      pFault = RefreshCtlEnumeration() 
    ElseIf pnlEnumeration.Controls(0) Is _ctlEnumerationCol Then 
      pFault = RefreshCtlEnumerationCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlEnumeration.Controls(0).Name, "", "TRGT-Enumeration-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboEnumerations(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlEnumerationCol_evtRowClicked(ByVal vEnumeration As Object) Handles _ctlEnumerationCol.evtRowClicked 
    
    If vEnumeration Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pEnumeration As csEnumeration = CType(vEnumeration, csEnumeration) 
    _EnumerationID = pEnumeration.ID 
    
    If _ActiveControl Is _ctlEnumerationCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csEnumerationCol.enmFillSumOnTheFlyParameters.GroupByEnumType.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csEnumerationCol.enmFillSumOnTheFlyParameters.GroupByEnumValue.ToString() Then 
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
    
    ChooseEnumeration() 
    
    Try 
      MyIntelliCombo.ValueSelect(_EnumerationID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pEnumeration.EnumType & " " & pEnumeration.EnumValue
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseEnumeration() 
    _Enumeration = Nothing 
    lnkEnumeration.Visible = True 
  End Sub 
  Private Sub _ctlEnumerationCol_evtRowDoubleClicked(ByVal vEnumeration As csEnumeration, ByRef rHandled As Boolean) Handles _ctlEnumerationCol.evtRowDoubleClicked 
    If lnkEnumeration.Parent IsNot flpMenu Then Exit Sub 
    If vEnumeration Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csEnumerationCol.enmFillSumOnTheFlyParameters.GroupByEnumType.ToString() Then 
        If pSearchFilters.ContainsKey(csEnumerationCol.enmFillOnTheFlyParameters.EnumType) Then pSearchFilters.Remove(csEnumerationCol.enmFillOnTheFlyParameters.EnumType) 
        pSearchFilters.Add(csEnumerationCol.enmFillOnTheFlyParameters.EnumType, vEnumeration.EnumType) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csEnumerationCol.enmFillSumOnTheFlyParameters.GroupByEnumValue.ToString() Then 
        If pSearchFilters.ContainsKey(csEnumerationCol.enmFillOnTheFlyParameters.EnumValue) Then pSearchFilters.Remove(csEnumerationCol.enmFillOnTheFlyParameters.EnumValue) 
        pSearchFilters.Add(csEnumerationCol.enmFillOnTheFlyParameters.EnumValue, vEnumeration.EnumValue) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreEnumerationCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vEnumeration.ID, vEnumeration.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _EnumerationID = vEnumeration.ID 
      'MyIntelliCombo.ValueSelect(_EnumerationID) 
      pFault = ActivateControl("ctlc_Enumeration") 
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
      pFault = _EnumerationCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _EnumerationCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _EnumerationCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _EnumerationCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_EnumerationCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csEnumeration.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Enumeration" 
      pFault = _ctlEnumerationCol.LoadControl(_EnumerationCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlEnumerationCol_evtUnChosen() Handles _ctlEnumerationCol.evtUnChosen 
 
    _EnumerationID = 0 
    _Enumeration = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkEnumeration.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkEnumerationCol.Click, 
      lnkEnumeration.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkEnumeration OrElse (lnk Is lnkEnumerationCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlEnumerationCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_EnumerationCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = False 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csEnumeration.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csEnumerationCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillEnumerationCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _EnumerationCol = New csEnumerationCol(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _EnumerationCol.OverrideDefaultLanguage(LocalizedTextLanguage) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _EnumerationCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlEnumerationCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlEnumerationCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _EnumerationCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlEnumerationCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _EnumerationCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _EnumerationCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _EnumerationCol.Count) 
      End If 
    Else 
      _EnumerationCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _EnumerationCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlEnumerationCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Enumeration" 
    
    Dim pEnumerationID As Integer = _EnumerationID 
    
    pFault = _ctlEnumerationCol.LoadControl(_EnumerationCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlEnumerationCol.Visible = True 
    
    _ctlEnumerationCol.Refresh() 
    If pEnumerationID <> 0 Then 
      Dim pEnumerationCol As csEnumerationCol = CType(_ctlEnumerationCol.bsCtlEnumeration.DataSource, csEnumerationCol) 
      Dim pEnumeration As csEnumeration = pEnumerationCol.FindByID(pEnumerationID) 
      If pEnumeration.ID > 0 Then 
        _ctlEnumerationCol.bsCtlEnumeration.CurrencyManager.Position = pEnumerationCol.IndexOf(pEnumeration) 
        _ctlEnumerationCol.dgvEnumeration.Rows(pEnumerationCol.IndexOf(pEnumeration)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlEnumeration() As clsFault 
    Dim pFault As New clsFault 
    
    If _EnumerationID > 0 Then 
      ChooseEnumeration() 
      _Enumeration = New csEnumeration(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _Enumeration.OverrideDefaultLanguage(LocalizedTextLanguage) 
      pFault = _Enumeration.GetByID(_EnumerationID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _Enumeration = New csEnumeration(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _Enumeration.OverrideDefaultLanguage(LocalizedTextLanguage) 
    End If 
    'lblSecondaryTitle.Text = _Enumeration.EnumType & " " & _Enumeration.EnumValue    
     
    Dim pLoadParameters As New ctlc_Enumeration.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlEnumeration(pLoadParameters)
    pFault = _ctlEnumeration.LoadControl(_Enumeration, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlEnumeration.Visible = True 
    If _EnumerationID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlEnumeration.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlEnumeration.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlEnumeration_evtDeleted(ByVal vEnumerationID As Long) Handles _ctlEnumeration.evtDeleted 
    _EnumerationCol = Nothing 
    Dim pFault As clsFault 
    _EnumerationID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboEnumerations(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlEnumeration() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlEnumeration.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkEnumerationCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlEnumeration_evtCancelledEdit(ByVal vEnumeration As csEnumeration) Handles _ctlEnumeration.evtCancelledEdit 
    RefreshCtlEnumeration() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboEnumerations(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlEnumeration.btnAdd.Visible = False 
      If _EnumerationID = 0 OrElse _EnumerationID = -2 Then 
        pnlEnumeration.Visible = False 
      Else 
        pnlEnumeration.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlEnumeration.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_EnumerationCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlEnumeration_evtUpdated(ByVal vWhichProperty As csEnumeration.enmUpdateType, ByVal vEnumeration As csEnumeration) Handles _ctlEnumeration.evtUpdated 
    _EnumerationCol = Nothing 
    Dim pFault As clsFault 
    _EnumerationID = ccHelper.ToInteger(CType(vEnumeration, csEnumeration).ID) 
    If _ActiveControl.Name = "ctlc_Enumeration" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboEnumerations(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlEnumeration() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlEnumeration.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboEnumerations(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_EnumerationDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboEnumeration(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
      MyIntelliCombo.SetKeyType(clsEnums.enmComboListKeyType.Integer) 
    End If 
 
    If pComboList IsNot Nothing Then 
      MyIntelliCombo.LoadControl(pComboList, pPrompt, vShowOptionsOn1stLoad:=True) 
    Else 
      MyIntelliCombo.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester, vShowOptionsOn1stLoad:=True) 
    End If 
 
    If _EnumerationID >= 0 Then 
      MyIntelliCombo.ValueSelect(_EnumerationID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_EnumerationUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _EnumerationID = ccHelper.ToInteger(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyInteger = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _EnumerationID = ccHelper.ToInteger(vComboListMember.Text) 
      RaiseEvent evtGetEnumerationIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _EnumerationID = vComboListMember.KeyInteger 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _EnumerationID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _EnumerationID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _EnumerationID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseEnumeration() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_Enumeration", StringComparison.OrdinalIgnoreCase) AndAlso _EnumerationID > 0 Then 
        'to avoid getting ObjectNotFound 
        _Enumeration = New csEnumeration(vIsLocalized:=True) 
        If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _Enumeration.OverrideDefaultLanguage(LocalizedTextLanguage) 
        pFault = _Enumeration.GetByID(_EnumerationID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_Enumeration") 
    End If 
    pnlEnumeration.Visible = True 
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
    pnlEnumeration.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkEnumerationCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _EnumerationID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_EnumerationCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkEnumerationCol.Visible = False 
      _ActiveControl = _ctlEnumeration 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboEnumerations(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _EnumerationID <> 0 Then 
        pFault = ActivateControl("ctlc_Enumeration") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlEnumeration.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlEnumeration.Visible = False 
        _EnumerationID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _EnumerationID > 0 Then pnlEnumeration.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkEnumerationCol.MouseEnter, 
                  lnkEnumeration.MouseEnter, 
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
                  lnkEnumerationCol.MouseLeave, 
                  lnkEnumeration.MouseLeave, 
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
  Private Sub _ctlEnumeration_evtAdd(ByVal vEnumeration As csEnumeration) Handles _ctlEnumeration.evtAdd 
    lnkEnumerationCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pEnumType As String = Nothing 
    Dim pEnumTypeWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pEnumValue As String = Nothing 
    Dim pEnumValueWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pIDFrom As Nullable(Of Integer) = Nothing 
    Dim pIDTo As Nullable(Of Integer) = Nothing 
 
    Dim pGroupByEnumType As Boolean = False 
    Dim pGroupByEnumValue As Boolean = False 
    
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Enumerations"  
  
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
        .String01Label.Text = If(_ctlEnumerationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csEnumeration.enmProperty.EnumType), _ctlEnumerationCol.LoadParameters.ColumnsHeaderText(csEnumeration.enmProperty.EnumType), "Enum Type") 
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
 
        .String02Label.Text = If(_ctlEnumerationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csEnumeration.enmProperty.EnumValue), _ctlEnumerationCol.LoadParameters.ColumnsHeaderText(csEnumeration.enmProperty.EnumValue), "Enum Value") 
        .String02Text.Text = "" 
        .String02Text.TabIndex = 5 
        With .String02WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 6 
        End With 
        .flpFilter.Controls.Add(.String02Label) 
        .flpFilter.Controls.Add(.String02Text) 
        .flpFilter.Controls.Add(.String02LblWCType) 
        .flpFilter.Controls.Add(.String02WCType) 
 
        .Text01Label.Text = If(_ctlEnumerationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csEnumeration.enmProperty.ID), _ctlEnumerationCol.LoadParameters.ColumnsHeaderText(csEnumeration.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 7 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 8 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlEnumerationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csEnumeration.enmProperty.EnumType), _ctlEnumerationCol.LoadParameters.ColumnsHeaderText(csEnumeration.enmProperty.EnumType), "Enum Type") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlEnumerationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csEnumeration.enmProperty.EnumValue), _ctlEnumerationCol.LoadParameters.ColumnsHeaderText(csEnumeration.enmProperty.EnumValue), "Enum Value") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 10 
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
      If .String01Text.Text <> "" Then 
        pEnumType = .String01Text.Text 
        pEnumTypeWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csEnumerationCol.enmFillOnTheFlyParameters.EnumType, pEnumType) 
        _SearchFilters.Add(csEnumerationCol.enmFillOnTheFlyParameters.EnumTypeWildcardType, pEnumTypeWildcardType) 
      End If 
      If .String02Text.Text <> "" Then 
        pEnumValue = .String02Text.Text 
        pEnumValueWildcardType = CType(CType(.String02WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csEnumerationCol.enmFillOnTheFlyParameters.EnumValue, pEnumValue) 
        _SearchFilters.Add(csEnumerationCol.enmFillOnTheFlyParameters.EnumValueWildcardType, pEnumValueWildcardType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToInteger(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToInteger(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csEnumerationCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csEnumerationCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csEnumerationCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csEnumerationCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByEnumType = True 
        pDoSum = True 
        _SearchFilters.Add(csEnumerationCol.enmFillSumOnTheFlyParameters.GroupByEnumType, pGroupByEnumType) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByEnumValue = True 
        pDoSum = True 
        _SearchFilters.Add(csEnumerationCol.enmFillSumOnTheFlyParameters.GroupByEnumValue, pGroupByEnumValue) 
      End If 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csEnumerationCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csEnumerationCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csEnumerationCol.enmListDefinition.Dir) Then _SearchFilters.Add(csEnumerationCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_EnumerationCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_EnumerationCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csEnumeration.enmProperty.ID, "ID") 
      End With 
      _EnumerationCol = New csEnumerationCol(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _EnumerationCol.OverrideDefaultLanguage(LocalizedTextLanguage) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _EnumerationCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _EnumerationCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _EnumerationCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _EnumerationCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _EnumerationCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Enumeration" 
      RaiseEvent evtOverrideLoadCtlEnumerationCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _EnumerationCol = New csEnumerationCol(vIsLocalized:=True) 
      If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _EnumerationCol.OverrideDefaultLanguage(LocalizedTextLanguage) 
      pFault = _EnumerationCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_EnumerationCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _EnumerationCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csEnumeration.enmProperty.ID, "Count") 
        If pGroupByEnumType = False Then .ColumnsHide.Add(csEnumeration.enmProperty.EnumType) 
        If pGroupByEnumValue = False Then .ColumnsHide.Add(csEnumeration.enmProperty.EnumValue) 
        .ColumnsHide.Add(csEnumeration.enmProperty.IsSystem) 
        .ColumnsHide.Add(csEnumeration.enmProperty.Text) 
        .ColumnsHide.Add(csEnumeration.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlEnumerationCol.Visible = True 
    pFault = _ctlEnumerationCol.LoadControl(_EnumerationCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csEnumerationCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csEnumerationCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlEnumeration.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlEnumeration.Controls(0).Name) 
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
    _EnumerationID = -2 
    pFault = ActivateControl("ctlc_Enumeration") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlEnumeration() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlEnumeration.Visible = True 'new 
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
 
  Private Sub _ctlEnumerationCol_evtTimerTripped() Handles _ctlEnumerationCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtEnumerationTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlEnumerationCol.EnumerationCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlEnumerationCol.EnumerationCol(0).ID 
 
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
    If _EnumerationCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csEnumeration() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csEnumerationCol = CType(CallByName(_EnumerationCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csEnumerationCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csEnumerationCol = CType(CallByName(_EnumerationCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csEnumerationCol) 
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
                  ccHelper.GetPropertyTypeName(New csEnumerationCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csEnumerationCol = CType(CallByName(_EnumerationCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csEnumerationCol) 
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
 
  Private Sub cc_ctlPnlEnumeration_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
