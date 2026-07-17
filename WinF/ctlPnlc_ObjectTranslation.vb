Public Class ctlPnlc_ObjectTranslation 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlObjectTranslationCol As ctlc_ObjectTranslationCol 
  Private WithEvents _ctlObjectTranslation As ctlc_ObjectTranslation 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _ObjectTranslationID As Long 
 
  'The data holders 
  Private _ObjectTranslationCol As csObjectTranslationCol 
  Private _ObjectTranslation As csObjectTranslation 
 
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
  Public Event evtOverrideLoadCboObjectTranslation(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetObjectTranslationIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillObjectTranslationCol(ByRef rObjectTranslationCol As csObjectTranslationCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlObjectTranslationCol(ByRef rLoadParameters As ctlc_ObjectTranslationCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlObjectTranslation(ByRef rLoadParameters As ctlc_ObjectTranslation.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreObjectTranslationCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtObjectTranslationTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  'Parents
  Private _CancelEvtObjectToTranslateChosen As Boolean = False 
  Private _ShowPopForEvtObjectToTranslateChosen As Boolean = False 
  
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
 
    lnkObjectTranslationCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkObjectTranslation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vObjectTranslationID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _ObjectTranslationID = CType(vObjectTranslationID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlObjectTranslation.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkObjectTranslationCol.Visible = False 
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
      pFault = LoadCboObjectTranslations(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _ObjectTranslationID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_ObjectTranslationID) 
      End If 
      ChooseObjectTranslation() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_ObjectTranslation") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _ObjectTranslationID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _ObjectTranslationID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_ObjectTranslation" OrElse pControlName = "ctlObjectTranslation" Then 
      lnkObjectTranslation.ForeColor = Color.Black : lnkObjectTranslation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkObjectTranslation.BackColor = Color.Wheat 
      If _ctlObjectTranslation Is Nothing Then 
        _ctlObjectTranslation = New ctlc_ObjectTranslation() 
        _ctlObjectTranslation.Dock = DockStyle.Fill 
        _ctlObjectTranslation.Controls.RemoveByKey("btnAdd") 
        pnlObjectTranslation.Controls.Add(_ctlObjectTranslation) 
        _ctlObjectTranslation.Visible = False 
      End If 
      If _ObjectTranslationID = 0 Then 
        pnlObjectTranslation.Visible = False 
      End If 
      'If _ObjectTranslation Is Nothing Then 
      pFault = RefreshCtlObjectTranslation() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlObjectTranslation.ObjectTranslation.IsEmpty AndAlso _ObjectTranslationID <> -2 Then 
        pnlObjectTranslation.Visible = False 
      End If 
      _ctlObjectTranslation.Name = "ctlc_ObjectTranslation" 
      _ActiveControl = _ctlObjectTranslation 
      _ctlObjectTranslation.BringToFront() 
      _ctlObjectTranslation.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_ObjectTranslationCol" Then 
      lnkObjectTranslationCol.ForeColor = Color.Black : lnkObjectTranslationCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkObjectTranslationCol.BackColor = Color.Wheat 
      If _ctlObjectTranslationCol Is Nothing Then 
        _ctlObjectTranslationCol = New ctlc_ObjectTranslationCol() 
        _ctlObjectTranslationCol.Dock = DockStyle.Fill 
        pnlObjectTranslation.Controls.Add(_ctlObjectTranslationCol) 
        _ctlObjectTranslationCol.Visible = False 
      End If  
      pnlObjectTranslation.Visible = True 
      If _ObjectTranslationCol Is Nothing Then 
        pFault = RefreshCtlObjectTranslationCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlObjectTranslationCol.Name = "ctlc_ObjectTranslationCol" 
      _ActiveControl = _ctlObjectTranslationCol 
      _ctlObjectTranslationCol.BringToFront() 
      _ctlObjectTranslationCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-ObjectTranslation-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("ObjectTranslation", _Requester) 
 
    lnkObjectTranslationCol.Text = CCTextTranslate("List", _Requester) 
    lnkObjectTranslation.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlObjectTranslation.Controls(0) Is _ctlObjectTranslation Then 
      If _ObjectTranslationID = 0 Then 
        pnlObjectTranslation.Visible = False 
      End If 
    ElseIf pnlObjectTranslation.Controls(0) Is _ctlObjectTranslationCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pObjectTranslationID As Long = _ObjectTranslationID 
      If ccHelper.IsNumeric(pText) Then _ObjectTranslationID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetObjectTranslationIDFromIntelliComboText(pText) 
      If pObjectTranslationID <> _ObjectTranslationID Then 
        _ObjectTranslation = Nothing 
        pFault = ActivateControl("ctlc_ObjectTranslation") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlObjectTranslation.Controls(0) Is _ctlObjectTranslation Then 
      pFault = RefreshCtlObjectTranslation() 
    ElseIf pnlObjectTranslation.Controls(0) Is _ctlObjectTranslationCol Then 
      pFault = RefreshCtlObjectTranslationCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlObjectTranslation.Controls(0).Name, "", "TRGT-ObjectTranslation-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub _ctlObjectTranslationCol_evtRowClicked(ByVal vObjectTranslation As Object) Handles _ctlObjectTranslationCol.evtRowClicked 
    
    If vObjectTranslation Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pObjectTranslation As csObjectTranslation = CType(vObjectTranslation, csObjectTranslation) 
    _ObjectTranslationID = pObjectTranslation.ID 
    
    If _ActiveControl Is _ctlObjectTranslationCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByObjectToTranslateID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByInstance.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByLanguage.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByInstanceUniqueText.ToString() Then 
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
    
    ChooseObjectTranslation() 
    
    Try 
      MyIntelliCombo.ValueSelect(_ObjectTranslationID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pObjectTranslation.ID.ToString("#,##0")

 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseObjectTranslation() 
    _ObjectTranslation = Nothing 
    lnkObjectTranslation.Visible = True 
  End Sub 
  Private Sub _ctlObjectTranslationCol_evtRowDoubleClicked(ByVal vObjectTranslation As csObjectTranslation, ByRef rHandled As Boolean) Handles _ctlObjectTranslationCol.evtRowDoubleClicked 
    If lnkObjectTranslation.Parent IsNot flpMenu Then Exit Sub 
    If vObjectTranslation Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByObjectToTranslateID.ToString() Then 
        If pSearchFilters.ContainsKey(csObjectTranslationCol.enmFillOnTheFlyParameters.ObjectToTranslateID) Then pSearchFilters.Remove(csObjectTranslationCol.enmFillOnTheFlyParameters.ObjectToTranslateID) 
        pSearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.ObjectToTranslateID, vObjectTranslation.ObjectToTranslateID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByInstance.ToString() Then 
        If pSearchFilters.ContainsKey(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceFrom) Then pSearchFilters.Remove(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceFrom) 
        If pSearchFilters.ContainsKey(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceTo) Then pSearchFilters.Remove(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceTo) 
        pSearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceFrom, vObjectTranslation.Instance) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByLanguage.ToString() Then 
        If pSearchFilters.ContainsKey(csObjectTranslationCol.enmFillOnTheFlyParameters.Language) Then pSearchFilters.Remove(csObjectTranslationCol.enmFillOnTheFlyParameters.Language) 
        pSearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.Language, vObjectTranslation.Language) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByInstanceUniqueText.ToString() Then 
        If pSearchFilters.ContainsKey(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceUniqueText) Then pSearchFilters.Remove(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceUniqueText) 
        pSearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceUniqueText, vObjectTranslation.InstanceUniqueText) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreObjectTranslationCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vObjectTranslation.ID, vObjectTranslation.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _ObjectTranslationID = vObjectTranslation.ID 
      'MyIntelliCombo.ValueSelect(_ObjectTranslationID) 
      pFault = ActivateControl("ctlc_ObjectTranslation") 
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
      pFault = _ObjectTranslationCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ObjectTranslationCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ObjectTranslationCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ObjectTranslationCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_ObjectTranslationCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csObjectTranslation.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ObjectTranslation" 
      pFault = _ctlObjectTranslationCol.LoadControl(_ObjectTranslationCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlObjectTranslationCol_evtUnChosen() Handles _ctlObjectTranslationCol.evtUnChosen 
 
    _ObjectTranslationID = 0 
    _ObjectTranslation = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkObjectTranslation.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkObjectTranslationCol.Click, 
      lnkObjectTranslation.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkObjectTranslation OrElse (lnk Is lnkObjectTranslationCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlObjectTranslationCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_ObjectTranslationCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csObjectTranslation.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csObjectTranslationCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillObjectTranslationCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _ObjectTranslationCol = New csObjectTranslationCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ObjectTranslationCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlObjectTranslationCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlObjectTranslationCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _ObjectTranslationCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlObjectTranslationCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _ObjectTranslationCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _ObjectTranslationCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ObjectTranslationCol.Count) 
      End If 
    Else 
      _ObjectTranslationCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _ObjectTranslationCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlObjectTranslationCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ObjectTranslation" 
    
    Dim pObjectTranslationID As Long = _ObjectTranslationID 
    
    pFault = _ctlObjectTranslationCol.LoadControl(_ObjectTranslationCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlObjectTranslationCol.Visible = True 
    
    _ctlObjectTranslationCol.Refresh() 
    If pObjectTranslationID <> 0 Then 
      Dim pObjectTranslationCol As csObjectTranslationCol = CType(_ctlObjectTranslationCol.bsCtlObjectTranslation.DataSource, csObjectTranslationCol) 
      Dim pObjectTranslation As csObjectTranslation = pObjectTranslationCol.FindByID(pObjectTranslationID) 
      If pObjectTranslation.ID > 0 Then 
        _ctlObjectTranslationCol.bsCtlObjectTranslation.CurrencyManager.Position = pObjectTranslationCol.IndexOf(pObjectTranslation) 
        _ctlObjectTranslationCol.dgvObjectTranslation.Rows(pObjectTranslationCol.IndexOf(pObjectTranslation)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlObjectTranslation() As clsFault 
    Dim pFault As New clsFault 
    
    If _ObjectTranslationID > 0 Then 
      ChooseObjectTranslation() 
      _ObjectTranslation = New csObjectTranslation(clsEnums.enmLoadParent.TextOnly) 
      pFault = _ObjectTranslation.GetByID(_ObjectTranslationID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _ObjectTranslation = New csObjectTranslation(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _ObjectTranslation.ID.ToString("#,##0")
    
     
    Dim pLoadParameters As New ctlc_ObjectTranslation.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlObjectTranslation(pLoadParameters)
    pFault = _ctlObjectTranslation.LoadControl(_ObjectTranslation, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlObjectTranslation.Visible = True 
    If _ObjectTranslationID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlObjectTranslation.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlObjectTranslation.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlObjectTranslation_evtDeleted(ByVal vObjectTranslationID As Long) Handles _ctlObjectTranslation.evtDeleted 
    _ObjectTranslationCol = Nothing 
    Dim pFault As clsFault 
    _ObjectTranslationID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboObjectTranslations(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlObjectTranslation() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlObjectTranslation.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkObjectTranslationCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlObjectTranslation_evtCancelledEdit(ByVal vObjectTranslation As csObjectTranslation) Handles _ctlObjectTranslation.evtCancelledEdit 
    RefreshCtlObjectTranslation() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboObjectTranslations(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlObjectTranslation.btnAdd.Visible = False 
      If _ObjectTranslationID = 0 OrElse _ObjectTranslationID = -2 Then 
        pnlObjectTranslation.Visible = False 
      Else 
        pnlObjectTranslation.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlObjectTranslation.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_ObjectTranslationCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlObjectTranslation_evtUpdated(ByVal vWhichProperty As csObjectTranslation.enmUpdateType, ByVal vObjectTranslation As csObjectTranslation) Handles _ctlObjectTranslation.evtUpdated 
    _ObjectTranslationCol = Nothing 
    Dim pFault As clsFault 
    _ObjectTranslationID = CType(vObjectTranslation, csObjectTranslation).ID 
    If _ActiveControl.Name = "ctlc_ObjectTranslation" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboObjectTranslations(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlObjectTranslation() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlObjectTranslation.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboObjectTranslations(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.UD 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboObjectTranslation(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _ObjectTranslationID >= 0 Then 
      MyIntelliCombo.ValueSelect(_ObjectTranslationID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_ObjectTranslationUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ObjectTranslationID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ObjectTranslationID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetObjectTranslationIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _ObjectTranslationID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _ObjectTranslationID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _ObjectTranslationID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _ObjectTranslationID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseObjectTranslation() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_ObjectTranslation", StringComparison.OrdinalIgnoreCase) AndAlso _ObjectTranslationID > 0 Then 
        'to avoid getting ObjectNotFound 
        _ObjectTranslation = New csObjectTranslation(clsEnums.enmLoadParent.TextOnly) 
        pFault = _ObjectTranslation.GetByID(_ObjectTranslationID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_ObjectTranslation") 
    End If 
    pnlObjectTranslation.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csObjectTranslation.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlObjectTranslation.evtParentChosen 
    If vParentName = csObjectTranslation.enmParentProperty.ObjectToTranslate Then 
      rHandled = True 
      If _CancelEvtObjectToTranslateChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New csObjectToTranslate 
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
    pnlObjectTranslation.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkObjectTranslationCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _ObjectTranslationID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_ObjectTranslationCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkObjectTranslationCol.Visible = False 
      _ActiveControl = _ctlObjectTranslation 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboObjectTranslations(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _ObjectTranslationID <> 0 Then 
        MyIntelliCombo.cbo.Text = _ObjectTranslationID.ToString() 
        pFault = ActivateControl("ctlc_ObjectTranslation") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlObjectTranslation.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlObjectTranslation.Visible = False 
        _ObjectTranslationID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _ObjectTranslationID > 0 Then pnlObjectTranslation.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkObjectTranslationCol.MouseEnter, 
                  lnkObjectTranslation.MouseEnter, 
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
                  lnkObjectTranslationCol.MouseLeave, 
                  lnkObjectTranslation.MouseLeave, 
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
  Private Sub _ctlObjectTranslation_evtAdd(ByVal vObjectTranslation As csObjectTranslation) Handles _ctlObjectTranslation.evtAdd 
    lnkObjectTranslationCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pObjectToTranslateID As Nullable(Of Long) = Nothing 
    Dim pInstanceFrom As Nullable(Of Long) = Nothing 
    Dim pInstanceTo As Nullable(Of Long) = Nothing 
    Dim pLanguage As clsEnums.enmLanguage = Nothing 
    Dim pInstanceUniqueText As String = Nothing 
    Dim pInstanceUniqueTextWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByObjectToTranslateID As Boolean = False 
    Dim pGroupByInstance As Boolean = False 
    Dim pGroupByLanguage As Boolean = False 
    Dim pGroupByInstanceUniqueText As Boolean = False 
    
    Dim pSumInstance As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Object Translations"  
  
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
        .Combo01Label.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.ObjectToTranslate), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.ObjectToTranslate), "Object To Translate") 
        Dim pObjectToTranslates As New clsComboList 
        pFault = MyCache.GetComboList(clsEnums.enmComboListType.c_ObjectToTranslateDefaultByID, pObjectToTranslates) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
        'If pObjectToTranslates IsNot Nothing AndAlso pObjectToTranslates.Count > 0 Then 
        .flpFilter.Controls.Add(.Combo01Label) 
        .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        'End If 
        With .Combo01 
          .MakeSmart() 
          If pObjectToTranslates IsNot Nothing Then 
            .LoadControl(pObjectToTranslates, GetChoose(_Requester)) 
          Else 
            .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.c_ObjectToTranslateDefaultByID, 0, _Requester) 
          End If 
          .TabIndex = 3 
        End With 
 
        .Text01Label.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.Instance), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.Instance), "Instance") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 4 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 5 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .Combo02Label.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.Language), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.Language), "Language") 
        Dim pLanguages As New clsComboList 
        pFault = pLanguages.FillEnums(clsEnums.enmEnum.Language, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pLanguages.Remove(pLanguages.FindByKey(clsEnums.enmLanguage.UD)) 
        pLanguages.SortByText() 
        If pLanguages IsNot Nothing AndAlso pLanguages.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo02Label) 
          .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo02 
          .MakeSmart() 
          .LoadControl(pLanguages, GetChoose(_Requester)) 
          .TabIndex = 6 
        End With 
 
        .String01Label.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.InstanceUniqueText), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.InstanceUniqueText), "Instance Unique Text") 
        .String01Text.Text = "" 
        .String01Text.TabIndex = 7 
        With .String01WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 8 
        End With 
        .flpFilter.Controls.Add(.String01Label) 
        .flpFilter.Controls.Add(.String01Text) 
        .flpFilter.Controls.Add(.String01LblWCType) 
        .flpFilter.Controls.Add(.String01WCType) 
 
        .Text02Label.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.ID), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 9 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 10 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.ObjectToTranslate), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.ObjectToTranslate), "Object To Translate") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 11 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.Instance), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.Instance), "Instance") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 12 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.Language), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.Language), "Language") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 13 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .lblGroupBy04.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.InstanceUniqueText), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.InstanceUniqueText), "Instance Unique Text") 
        .chkGroupBy04.Checked = False 
        .chkGroupBy04.TabIndex = 14 
        .flpGroupBy.Controls.Add(.lblGroupBy04) 
        .flpGroupBy.Controls.Add(.chkGroupBy04) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectTranslation.enmProperty.Instance), _ctlObjectTranslationCol.LoadParameters.ColumnsHeaderText(csObjectTranslation.enmProperty.Instance), "Instance") 
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
      If .Combo01.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo01.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
        pObjectToTranslateID = CType(.Combo01.SelectedItem, clsComboListMember).KeyLong 
        _SearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.ObjectToTranslateID, pObjectToTranslateID) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pInstanceFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pInstanceTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pInstanceTo = pInstanceFrom 
          End If 
          _SearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceFrom, pInstanceFrom) 
          _SearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceTo, pInstanceTo) 
        End If 
      End If 
      If .Combo02.SelectedItem IsNot Nothing Then 
        pLanguage = CType(CType(.Combo02.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmLanguage) 
        _SearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.Language, pLanguage) 
      End If 
      If .String01Text.Text <> "" Then 
        pInstanceUniqueText = .String01Text.Text 
        pInstanceUniqueTextWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceUniqueText, pInstanceUniqueText) 
        _SearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.InstanceUniqueTextWildcardType, pInstanceUniqueTextWildcardType) 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csObjectTranslationCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csObjectTranslationCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csObjectTranslationCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByObjectToTranslateID = True 
        pDoSum = True 
        _SearchFilters.Add(csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByObjectToTranslateID, pGroupByObjectToTranslateID) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByInstance = True 
        pDoSum = True 
        _SearchFilters.Add(csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByInstance, pGroupByInstance) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByLanguage = True 
        pDoSum = True 
        _SearchFilters.Add(csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByLanguage, pGroupByLanguage) 
      End If 
      If .chkGroupBy04.Checked = True Then 
        pGroupByInstanceUniqueText = True 
        pDoSum = True 
        _SearchFilters.Add(csObjectTranslationCol.enmFillSumOnTheFlyParameters.GroupByInstanceUniqueText, pGroupByInstanceUniqueText) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumInstance = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csObjectTranslationCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csObjectTranslationCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csObjectTranslationCol.enmListDefinition.Dir) Then _SearchFilters.Add(csObjectTranslationCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_ObjectTranslationCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_ObjectTranslationCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csObjectTranslation.enmProperty.ID, "ID") 
      End With 
      _ObjectTranslationCol = New csObjectTranslationCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ObjectTranslationCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _ObjectTranslationCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ObjectTranslationCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ObjectTranslationCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ObjectTranslationCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ObjectTranslation" 
      RaiseEvent evtOverrideLoadCtlObjectTranslationCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _ObjectTranslationCol = New csObjectTranslationCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _ObjectTranslationCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_ObjectTranslationCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _ObjectTranslationCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csObjectTranslation.enmProperty.ID, "Count") 
        If pGroupByObjectToTranslateID = False Then .ColumnsHide.Add(csObjectTranslation.enmProperty.ObjectToTranslate) 
        If pGroupByInstance = False Then .ColumnsHide.Add(csObjectTranslation.enmProperty.Instance) 
        If pGroupByLanguage = False Then .ColumnsHide.Add(csObjectTranslation.enmProperty.Language) 
        If pGroupByInstanceUniqueText = False Then .ColumnsHide.Add(csObjectTranslation.enmProperty.InstanceUniqueText) 
        If pSumInstance = False Then .ColumnsHide.Add(csObjectTranslation.enmProperty.Instance) 
        If pGroupByInstance = True OrElse pSumInstance = True Then If .ColumnsHide.Contains(csObjectTranslation.enmProperty.Instance) Then .ColumnsHide.Remove(csObjectTranslation.enmProperty.Instance) 
        .ColumnsHide.Add(csObjectTranslation.enmProperty.DefaultText) 
        .ColumnsHide.Add(csObjectTranslation.enmProperty.Text) 
        .ColumnsHide.Add(csObjectTranslation.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlObjectTranslationCol.Visible = True 
    pFault = _ctlObjectTranslationCol.LoadControl(_ObjectTranslationCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csObjectTranslationCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csObjectTranslationCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlObjectTranslation.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlObjectTranslation.Controls(0).Name) 
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
    _ObjectTranslationID = -2 
    pFault = ActivateControl("ctlc_ObjectTranslation") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlObjectTranslation() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlObjectTranslation.Visible = True 'new 
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
 
  Private Sub _ctlObjectTranslationCol_evtTimerTripped() Handles _ctlObjectTranslationCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtObjectTranslationTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlObjectTranslationCol.ObjectTranslationCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlObjectTranslationCol.ObjectTranslationCol(0).ID 
 
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
    If _ObjectTranslationCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csObjectTranslation() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csObjectTranslationCol = CType(CallByName(_ObjectTranslationCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csObjectTranslationCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csObjectTranslationCol = CType(CallByName(_ObjectTranslationCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csObjectTranslationCol) 
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
                  ccHelper.GetPropertyTypeName(New csObjectTranslationCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csObjectTranslationCol = CType(CallByName(_ObjectTranslationCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csObjectTranslationCol) 
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
 
  Private Sub cc_ctlPnlObjectTranslation_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
 
  '==================================================================================================== 
 
  Private _Lang As Nullable(Of clsEnums.enmLanguage) = Nothing 
 
  Private Sub cc_ctlPnlc_ObjectTranslation_ccevtOverrideFillObjectTranslationCol(ByRef rObjectTranslationCol As csObjectTranslationCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) Handles Me.evtOverrideFillObjectTranslationCol 
    Dim pFault As clsFault 
    If _Lang Is Nothing Then 
      rObjectTranslationCol = New csObjectTranslationCol(clsEnums.enmLoadParent.TextOnly) 
    Else 
      pFault = ccDatabaseMaintenance.FillAllTranslationPossibilities(0, clsEnums.enmLanguage.UD, _Requester, rObjectTranslationCol) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
    End If 
    If rObjectTranslationCol.Count > 0 AndAlso _Lang <> clsEnums.enmLanguage.UD Then 
      'filter the languages 
      Dim pObjectTranslations = rObjectTranslationCol.Clone() 
      rObjectTranslationCol = New csObjectTranslationCol(clsEnums.enmLoadParent.TextOnly) 
      For Each l In pObjectTranslations 
        If l.Language = _Lang Then 
          rObjectTranslationCol.Add(l) 
        End If 
      Next 
    End If 
  End Sub 
 
  Private Sub btnAddAllObjectsToTranslate_Click(sender As Object, e As EventArgs) Handles btnAddAllObjectsToTranslate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    Cursor = Cursors.WaitCursor 
    pFault = ccDatabaseMaintenance.TranslationAddAllPossibilitiesToObjectToTranslate(_Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
 
    _Lang = CType(CType(cboLanguages.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmLanguage) 
    btnRefresh.PerformClick() 
  End Sub 
 
  Private Sub btnRemoveUnusedObjectsToTranslate_Click(sender As Object, e As EventArgs) Handles btnRemoveUnusedObjectsToTranslate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    Cursor = Cursors.WaitCursor 
    pFault = ccDatabaseMaintenance.TranslationRemoveUnusedPossibilitiesFromObjectToTranslate(_Requester) 
    Cursor = Cursors.Default 
    ShowFault(pFault, _Requester) 
  End Sub 
 
  'Design changes 
  Friend WithEvents btnRemoveUnusedObjectsToTranslate As Button 
  Friend WithEvents btnAddAllObjectsToTranslate As Button 
  Friend WithEvents cboLanguages As ComboBox 
 
  Private Sub cc_ctlPnlc_ObjectTranslation_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    chkGrid.Parent.Controls.Remove(chkGrid) 
    btnFilter.Parent.Controls.Remove(btnFilter) 
    btnRefresh.Parent.Controls.Remove(btnRefresh) 
 
 
    Me.btnAddAllObjectsToTranslate = New System.Windows.Forms.Button() 
    Me.btnRemoveUnusedObjectsToTranslate = New System.Windows.Forms.Button() 
    Me.cboLanguages = New System.Windows.Forms.ComboBox() 
 
 
    Me.gpbHeader.Controls.Add(Me.btnRemoveUnusedObjectsToTranslate) 
    Me.gpbHeader.Controls.Add(Me.btnAddAllObjectsToTranslate) 
    Me.gpbHeader.Controls.Add(Me.cboLanguages) 
 
 
    'btnAddAllObjectsToTranslate 
    ' 
    Me.btnAddAllObjectsToTranslate.AutoSize = True 
    Me.btnAddAllObjectsToTranslate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink 
    Me.btnAddAllObjectsToTranslate.Dock = System.Windows.Forms.DockStyle.Left 
    Me.btnAddAllObjectsToTranslate.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
    Me.btnAddAllObjectsToTranslate.Location = New System.Drawing.Point(402, 21) 
    Me.btnAddAllObjectsToTranslate.Name = "btnAddAllObjectsToTranslate" 
    Me.btnAddAllObjectsToTranslate.Size = New System.Drawing.Size(188, 28) 
    Me.btnAddAllObjectsToTranslate.TabIndex = 41 
    Me.btnAddAllObjectsToTranslate.Text = "Add All Objects to Translate" 
    Me.btnAddAllObjectsToTranslate.UseVisualStyleBackColor = True 
    ' 
    'btnRemoveUnusedObjectsToTranslate 
    ' 
    Me.btnRemoveUnusedObjectsToTranslate.AutoSize = True 
    Me.btnRemoveUnusedObjectsToTranslate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink 
    Me.btnRemoveUnusedObjectsToTranslate.Dock = System.Windows.Forms.DockStyle.Left 
    Me.btnRemoveUnusedObjectsToTranslate.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
    Me.btnRemoveUnusedObjectsToTranslate.Location = New System.Drawing.Point(590, 21) 
    Me.btnRemoveUnusedObjectsToTranslate.Name = "btnRemoveUnusedObjectsToTranslate" 
    Me.btnRemoveUnusedObjectsToTranslate.Size = New System.Drawing.Size(169, 28) 
    Me.btnRemoveUnusedObjectsToTranslate.TabIndex = 42 
    Me.btnRemoveUnusedObjectsToTranslate.Text = "Remove Unused Objects" 
    Me.btnRemoveUnusedObjectsToTranslate.UseVisualStyleBackColor = True 
    ' 
    'cboLanguages 
    ' 
    Me.cboLanguages.Dock = System.Windows.Forms.DockStyle.Left 
    Me.cboLanguages.FormattingEnabled = True 
    Me.cboLanguages.Location = New System.Drawing.Point(281, 21) 
    Me.cboLanguages.Name = "cboLanguages" 
    Me.cboLanguages.Size = New System.Drawing.Size(121, 25) 
    Me.cboLanguages.TabIndex = 43 
    ' 
 
    Me.cboLanguages.BringToFront() 
    Me.btnAddAllObjectsToTranslate.BringToFront() 
    Me.btnRemoveUnusedObjectsToTranslate.BringToFront() 
 
  End Sub 
 
  Private Sub cc_ctlPnlc_ObjectTranslation_evtLoaded() Handles Me.evtLoaded 
 
    'LoadLanguage cbo 
    Dim pCombolist As New clsComboList() 
    'Dim pFault As clsFault = pCombolist.Fill(clsEnums.enmComboListType.c_LanguageDefaultByCode, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    Dim pFault As clsFault = pCombolist.FillEnums(clsEnums.enmEnum.Language, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
 
    pFault = LoadCbo(cboLanguages, pCombolist, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
 
    pCombolist.RemoveAt(0) 
    pCombolist.SortByText() 
    pCombolist.AddToTop(clsEnums.enmLanguage.UD, "All") 
 
    With cboLanguages 
      '.ValueMember = "KeyString" 
      .ValueMember = "KeyEnum" 
      .DisplayMember = "Text" 
      .DataSource = pCombolist 
    End With 
  End Sub 
 
  Private Sub cc_ctlPnlc_ObjectTranslation_evtOverrideLoadCtlObjectTranslationCol(ByRef rLoadParameters As ctlc_ObjectTranslationCol.clsLoadParameters) Handles Me.evtOverrideLoadCtlObjectTranslationCol 
    rLoadParameters.ReadOnly = False 
    rLoadParameters.TruncateStrings = False 
    rLoadParameters.ColumnsReadOnly.Add(csObjectTranslation.enmProperty.ObjectToTranslate) 
    rLoadParameters.ColumnsReadOnly.Add(csObjectTranslation.enmProperty.InstanceUniqueText) 
  End Sub 
  
End Class 
