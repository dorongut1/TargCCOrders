Public Class ctlPnlc_ObjectToTranslate 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlObjectToTranslateCol As ctlc_ObjectToTranslateCol 
  Private WithEvents _ctlObjectToTranslate As ctlc_ObjectToTranslate 
  Private WithEvents _ctlObjectTranslationCol As ctlc_ObjectTranslationCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _ObjectToTranslateID As Long 
 
  'The data holders 
  Private _ObjectToTranslateCol As csObjectToTranslateCol 
  Private _ObjectToTranslate As csObjectToTranslate 
  Private _ObjectTranslationCol As csObjectTranslationCol 
 
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
  Public Event evtOverrideLoadCboObjectToTranslate(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetObjectToTranslateIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillObjectToTranslateCol(ByRef rObjectToTranslateCol As csObjectToTranslateCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillObjectTranslationCol(ByRef rObjectTranslationCol As csObjectTranslationCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlObjectToTranslateCol(ByRef rLoadParameters As ctlc_ObjectToTranslateCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlObjectToTranslate(ByRef rLoadParameters As ctlc_ObjectToTranslate.clsLoadParameters) 
  Private Event evtOverrideLoadCtlObjectTranslationCol(ByRef rLoadParameters As ctlc_ObjectTranslationCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreObjectToTranslateCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtObjectToTranslateTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtObjectTranslationChosen As Boolean = False 
  Private _ShowPopForEvtObjectTranslationChosen As Boolean = False 
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
 
    lnkObjectToTranslateCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkObjectToTranslate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkObjectTranslationCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vObjectToTranslateID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _ObjectToTranslateID = CType(vObjectToTranslateID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlObjectToTranslate.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkObjectToTranslateCol.Visible = False 
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
      pFault = LoadCboObjectToTranslates(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _ObjectToTranslateID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_ObjectToTranslateID) 
      End If 
      ChooseObjectToTranslate() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_ObjectToTranslate") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _ObjectToTranslateID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlc_ObjectToTranslate" OrElse pControlName = "ctlObjectToTranslate" Then 
      lnkObjectToTranslate.ForeColor = Color.Black : lnkObjectToTranslate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkObjectToTranslate.BackColor = Color.Wheat 
      If _ctlObjectToTranslate Is Nothing Then 
        _ctlObjectToTranslate = New ctlc_ObjectToTranslate() 
        _ctlObjectToTranslate.Dock = DockStyle.Fill 
        _ctlObjectToTranslate.Controls.RemoveByKey("btnAdd") 
        pnlObjectToTranslate.Controls.Add(_ctlObjectToTranslate) 
        _ctlObjectToTranslate.Visible = False 
      End If 
      If _ObjectToTranslateID = 0 Then 
        pnlObjectToTranslate.Visible = False 
      End If 
      'If _ObjectToTranslate Is Nothing Then 
      pFault = RefreshCtlObjectToTranslate() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlObjectToTranslate.ObjectToTranslate.IsEmpty AndAlso _ObjectToTranslateID <> -2 Then 
        pnlObjectToTranslate.Visible = False 
      End If 
      _ctlObjectToTranslate.Name = "ctlc_ObjectToTranslate" 
      _ActiveControl = _ctlObjectToTranslate 
      _ctlObjectToTranslate.BringToFront() 
      _ctlObjectToTranslate.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_ObjectToTranslateCol" Then 
      lnkObjectToTranslateCol.ForeColor = Color.Black : lnkObjectToTranslateCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkObjectToTranslateCol.BackColor = Color.Wheat 
      If _ctlObjectToTranslateCol Is Nothing Then 
        _ctlObjectToTranslateCol = New ctlc_ObjectToTranslateCol() 
        _ctlObjectToTranslateCol.Dock = DockStyle.Fill 
        pnlObjectToTranslate.Controls.Add(_ctlObjectToTranslateCol) 
        _ctlObjectToTranslateCol.Visible = False 
      End If  
      pnlObjectToTranslate.Visible = True 
      If _ObjectToTranslateCol Is Nothing Then 
        pFault = RefreshCtlObjectToTranslateCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlObjectToTranslateCol.Name = "ctlc_ObjectToTranslateCol" 
      _ActiveControl = _ctlObjectToTranslateCol 
      _ctlObjectToTranslateCol.BringToFront() 
      _ctlObjectToTranslateCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlc_ObjectTranslationCol" Then 
      lnkObjectTranslationCol.ForeColor = Color.Black : lnkObjectTranslationCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkObjectTranslationCol.BackColor = Color.Wheat 
      If _ctlObjectTranslationCol Is Nothing Then 
      _ctlObjectTranslationCol = New ctlc_ObjectTranslationCol() 
      _ctlObjectTranslationCol.Dock = DockStyle.Fill 
      pnlObjectToTranslate.Controls.Add(_ctlObjectTranslationCol) 
      _ctlObjectTranslationCol.Visible = False 
      End If  
      If _ObjectTranslationCol Is Nothing Then 
        pFault = RefreshCtlObjectTranslationCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlObjectTranslationCol.Name = "ctlc_ObjectTranslationCol" 
      _ActiveControl = _ctlObjectTranslationCol 
      _ctlObjectTranslationCol.BringToFront() 
      _ctlObjectTranslationCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-ObjectToTranslate-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("ObjectToTranslate", _Requester) 
 
    lnkObjectToTranslateCol.Text = CCTextTranslate("List", _Requester) 
    lnkObjectToTranslate.Text = CCTextTranslate("Details", _Requester) 
 
    lnkObjectTranslationCol.Text = TableNameTranslate("ObjectTranslation", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlObjectToTranslate.Controls(0) Is _ctlObjectToTranslate Then 
      If _ObjectToTranslateID = 0 Then 
        pnlObjectToTranslate.Visible = False 
      End If 
    ElseIf pnlObjectToTranslate.Controls(0) Is _ctlObjectToTranslateCol Then 
    ElseIf pnlObjectToTranslate.Controls(0) Is _ctlObjectTranslationCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pObjectToTranslateID As Long = _ObjectToTranslateID 
      If ccHelper.IsNumeric(pText) Then _ObjectToTranslateID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetObjectToTranslateIDFromIntelliComboText(pText) 
      If pObjectToTranslateID <> _ObjectToTranslateID Then 
        _ObjectToTranslate = Nothing 
        pFault = ActivateControl("ctlc_ObjectToTranslate") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlObjectToTranslate.Controls(0) Is _ctlObjectToTranslate Then 
      pFault = RefreshCtlObjectToTranslate() 
    ElseIf pnlObjectToTranslate.Controls(0) Is _ctlObjectToTranslateCol Then 
      pFault = RefreshCtlObjectToTranslateCol() 
    ElseIf pnlObjectToTranslate.Controls(0) Is _ctlObjectTranslationCol Then 
      pFault = RefreshCtlObjectTranslationCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlObjectToTranslate.Controls(0).Name, "", "TRGT-ObjectToTranslate-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboObjectToTranslates(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlObjectToTranslateCol_evtRowClicked(ByVal vObjectToTranslate As Object) Handles _ctlObjectToTranslateCol.evtRowClicked 
    
    If vObjectToTranslate Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pObjectToTranslate As csObjectToTranslate = CType(vObjectToTranslate, csObjectToTranslate) 
    _ObjectToTranslateID = pObjectToTranslate.ID 
    
    If _ActiveControl Is _ctlObjectToTranslateCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByObjectType.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByObject.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByItem.ToString() Then 
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
    
    ChooseObjectToTranslate() 
    
    Try 
      MyIntelliCombo.ValueSelect(_ObjectToTranslateID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pObjectToTranslate.ObjectType.ToString & " " & pObjectToTranslate.Object & " " & pObjectToTranslate.Item
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseObjectToTranslate() 
    _ObjectToTranslate = Nothing 
    lnkObjectToTranslate.Visible = True 
    _ObjectTranslationCol = Nothing 
    lnkObjectTranslationCol.Visible = True 
  End Sub 
  Private Sub _ctlObjectToTranslateCol_evtRowDoubleClicked(ByVal vObjectToTranslate As csObjectToTranslate, ByRef rHandled As Boolean) Handles _ctlObjectToTranslateCol.evtRowDoubleClicked 
    If lnkObjectToTranslate.Parent IsNot flpMenu Then Exit Sub 
    If vObjectToTranslate Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByObjectType.ToString() Then 
        If pSearchFilters.ContainsKey(csObjectToTranslateCol.enmFillOnTheFlyParameters.ObjectType) Then pSearchFilters.Remove(csObjectToTranslateCol.enmFillOnTheFlyParameters.ObjectType) 
        pSearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.ObjectType, vObjectToTranslate.ObjectType) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByObject.ToString() Then 
        If pSearchFilters.ContainsKey(csObjectToTranslateCol.enmFillOnTheFlyParameters.Object) Then pSearchFilters.Remove(csObjectToTranslateCol.enmFillOnTheFlyParameters.Object) 
        pSearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.Object, vObjectToTranslate.Object) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByItem.ToString() Then 
        If pSearchFilters.ContainsKey(csObjectToTranslateCol.enmFillOnTheFlyParameters.Item) Then pSearchFilters.Remove(csObjectToTranslateCol.enmFillOnTheFlyParameters.Item) 
        pSearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.Item, vObjectToTranslate.Item) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreObjectToTranslateCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vObjectToTranslate.ID, vObjectToTranslate.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _ObjectToTranslateID = vObjectToTranslate.ID 
      'MyIntelliCombo.ValueSelect(_ObjectToTranslateID) 
      pFault = ActivateControl("ctlc_ObjectToTranslate") 
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
      pFault = _ObjectToTranslateCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ObjectToTranslateCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ObjectToTranslateCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ObjectToTranslateCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_ObjectToTranslateCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csObjectToTranslate.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ObjectToTranslate" 
      pFault = _ctlObjectToTranslateCol.LoadControl(_ObjectToTranslateCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlObjectToTranslateCol_evtUnChosen() Handles _ctlObjectToTranslateCol.evtUnChosen 
 
    _ObjectToTranslateID = 0 
    _ObjectToTranslate = Nothing 
    _ObjectTranslationCol = Nothing 
    lnkObjectTranslationCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkObjectToTranslate.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkObjectTranslationCol.Click, 
      lnkObjectToTranslateCol.Click, 
      lnkObjectToTranslate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkObjectToTranslate OrElse (lnk Is lnkObjectToTranslateCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlObjectToTranslateCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_ObjectToTranslateCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csObjectToTranslate.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csObjectToTranslateCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillObjectToTranslateCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _ObjectToTranslateCol = New csObjectToTranslateCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ObjectToTranslateCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlObjectToTranslateCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlObjectToTranslateCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _ObjectToTranslateCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlObjectToTranslateCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _ObjectToTranslateCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _ObjectToTranslateCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ObjectToTranslateCol.Count) 
      End If 
    Else 
      _ObjectToTranslateCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _ObjectToTranslateCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlObjectToTranslateCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ObjectToTranslate" 
    
    Dim pObjectToTranslateID As Long = _ObjectToTranslateID 
    
    pFault = _ctlObjectToTranslateCol.LoadControl(_ObjectToTranslateCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlObjectToTranslateCol.Visible = True 
    
    _ctlObjectToTranslateCol.Refresh() 
    If pObjectToTranslateID <> 0 Then 
      Dim pObjectToTranslateCol As csObjectToTranslateCol = CType(_ctlObjectToTranslateCol.bsCtlObjectToTranslate.DataSource, csObjectToTranslateCol) 
      Dim pObjectToTranslate As csObjectToTranslate = pObjectToTranslateCol.FindByID(pObjectToTranslateID) 
      If pObjectToTranslate.ID > 0 Then 
        _ctlObjectToTranslateCol.bsCtlObjectToTranslate.CurrencyManager.Position = pObjectToTranslateCol.IndexOf(pObjectToTranslate) 
        _ctlObjectToTranslateCol.dgvObjectToTranslate.Rows(pObjectToTranslateCol.IndexOf(pObjectToTranslate)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlObjectToTranslate() As clsFault 
    Dim pFault As New clsFault 
    
    If _ObjectToTranslateID > 0 Then 
      ChooseObjectToTranslate() 
      _ObjectToTranslate = New csObjectToTranslate() 
      pFault = _ObjectToTranslate.GetByID(_ObjectToTranslateID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _ObjectToTranslate = New csObjectToTranslate() 
    End If 
    'lblSecondaryTitle.Text = _ObjectToTranslate.ObjectType.ToString & " " & _ObjectToTranslate.Object & " " & _ObjectToTranslate.Item    
     
    Dim pLoadParameters As New ctlc_ObjectToTranslate.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlObjectToTranslate(pLoadParameters)
    pFault = _ctlObjectToTranslate.LoadControl(_ObjectToTranslate, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlObjectToTranslate.Visible = True 
    If _ObjectToTranslateID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlObjectToTranslate.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlObjectToTranslate.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlObjectTranslationCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlObjectTranslationCol.dgvObjectTranslation.SelectedRows.Count > 0 Then 
      Dim pObjectTranslation As csObjectTranslation = CType(_ctlObjectTranslationCol.bsCtlObjectTranslation.Current, csObjectTranslation) 
      pID = pObjectTranslation.ID 
    End If 
 
    Dim pTestCol As csObjectTranslationCol = Nothing 
    RaiseEvent evtOverrideFillObjectTranslationCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _ObjectTranslationCol = New csObjectTranslationCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _ObjectTranslationCol.FillByObjectToTranslateID(_ObjectToTranslateID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _ObjectTranslationCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _ObjectTranslationCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ObjectTranslationCol.Count) 
      End If 
    Else 
      _ObjectTranslationCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _ObjectTranslationCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_ObjectTranslationCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _ObjectToTranslate IsNot Nothing AndAlso Not String.IsNullOrEmpty(_ObjectToTranslate.DefaultDesignation) Then 
        .ReportTitle = "List of ObjectTranslations for " & _ObjectToTranslate.DefaultDesignation 
      Else 
        .ReportTitle = "List of ObjectTranslations for ObjectToTranslate" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csObjectTranslation.enmProperty.ObjectToTranslate) 
    End With 
    RaiseEvent evtOverrideLoadCtlObjectTranslationCol(pLoadParameters)
    
    pFault = _ctlObjectTranslationCol.LoadControl(_ObjectTranslationCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlObjectTranslationCol.Visible = True 
 
    If pID > 0 Then 
      Dim pObjectTranslations As csObjectTranslationCol = CType(_ctlObjectTranslationCol.bsCtlObjectTranslation.DataSource, csObjectTranslationCol) 
      Dim pObjectTranslation As csObjectTranslation = pObjectTranslations.FindByID((pID)) 
      If pObjectTranslation.ID > 0 Then 
        _ctlObjectTranslationCol.bsCtlObjectTranslation.CurrencyManager.Position = pObjectTranslations.IndexOf(pObjectTranslation) 
        _ctlObjectTranslationCol.dgvObjectTranslation.Rows(pObjectTranslations.IndexOf(pObjectTranslation)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlObjectTranslationCol_evtBeforeUpdate(ByVal vObjectTranslation As csObjectTranslation, ByRef rCancel As Boolean) Handles _ctlObjectTranslationCol.evtBeforeUpdate 
    vObjectTranslation.ObjectToTranslateID = _ObjectToTranslate.ID 
  End Sub 
  Private Sub _ctlObjectToTranslate_evtDeleted(ByVal vObjectToTranslateID As Long) Handles _ctlObjectToTranslate.evtDeleted 
    _ObjectToTranslateCol = Nothing 
    Dim pFault As clsFault 
    _ObjectToTranslateID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboObjectToTranslates(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlObjectToTranslate() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlObjectToTranslate.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkObjectToTranslateCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlObjectToTranslate_evtCancelledEdit(ByVal vObjectToTranslate As csObjectToTranslate) Handles _ctlObjectToTranslate.evtCancelledEdit 
    RefreshCtlObjectToTranslate() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboObjectToTranslates(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlObjectToTranslate.btnAdd.Visible = False 
      If _ObjectToTranslateID = 0 OrElse _ObjectToTranslateID = -2 Then 
        pnlObjectToTranslate.Visible = False 
      Else 
        pnlObjectToTranslate.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlObjectToTranslate.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_ObjectToTranslateCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlObjectToTranslate_evtUpdated(ByVal vWhichProperty As csObjectToTranslate.enmUpdateType, ByVal vObjectToTranslate As csObjectToTranslate) Handles _ctlObjectToTranslate.evtUpdated 
    _ObjectToTranslateCol = Nothing 
    Dim pFault As clsFault 
    _ObjectToTranslateID = CType(vObjectToTranslate, csObjectToTranslate).ID 
    If _ActiveControl.Name = "ctlc_ObjectToTranslate" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboObjectToTranslates(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlObjectToTranslate() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlObjectToTranslate.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboObjectToTranslates(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_ObjectToTranslateDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboObjectToTranslate(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _ObjectToTranslateID >= 0 Then 
      MyIntelliCombo.ValueSelect(_ObjectToTranslateID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_ObjectToTranslateUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ObjectToTranslateID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ObjectToTranslateID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetObjectToTranslateIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _ObjectToTranslateID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _ObjectToTranslateID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _ObjectToTranslateID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _ObjectToTranslateID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseObjectToTranslate() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_ObjectToTranslate", StringComparison.OrdinalIgnoreCase) AndAlso _ObjectToTranslateID > 0 Then 
        'to avoid getting ObjectNotFound 
        _ObjectToTranslate = New csObjectToTranslate() 
        pFault = _ObjectToTranslate.GetByID(_ObjectToTranslateID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_ObjectToTranslate") 
    End If 
    pnlObjectToTranslate.Visible = True 
    pnlCover.SendToBack() 
    sLoading = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  
  'Choose Children
  Private Sub _ctlObjectTranslationCol_evtRowDoubleClicked(ByVal vObjectTranslation As csObjectTranslation, ByRef rHandled As Boolean) Handles _ctlObjectTranslationCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtObjectTranslationChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtObjectTranslationChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vObjectTranslation.ID 
      .Object = New csObjectTranslation 
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
    pnlObjectToTranslate.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkObjectToTranslateCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _ObjectToTranslateID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_ObjectToTranslateCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkObjectToTranslateCol.Visible = False 
      _ActiveControl = _ctlObjectToTranslate 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboObjectToTranslates(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _ObjectToTranslateID <> 0 Then 
        pFault = ActivateControl("ctlc_ObjectToTranslate") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlObjectToTranslate.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlObjectToTranslate.Visible = False 
        _ObjectToTranslateID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _ObjectToTranslateID > 0 Then pnlObjectToTranslate.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkObjectTranslationCol.MouseEnter, 
                  lnkObjectToTranslateCol.MouseEnter, 
                  lnkObjectToTranslate.MouseEnter, 
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
                  lnkObjectToTranslateCol.MouseLeave, 
                  lnkObjectToTranslate.MouseLeave, 
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
  Private Sub _ctlObjectToTranslate_evtAdd(ByVal vObjectToTranslate As csObjectToTranslate) Handles _ctlObjectToTranslate.evtAdd 
    lnkObjectTranslationCol.Visible = False 
    lnkObjectToTranslateCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pObjectType As clsEnums.enmObjectType = Nothing 
    Dim pObject As String = Nothing 
    Dim pObjectWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pItem As String = Nothing 
    Dim pItemWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByObjectType As Boolean = False 
    Dim pGroupByObject As Boolean = False 
    Dim pGroupByItem As Boolean = False 
    
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Object To Translates"  
  
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
        .Combo01Label.Text = If(_ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectToTranslate.enmProperty.ObjectType), _ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText(csObjectToTranslate.enmProperty.ObjectType), "Object Type") 
        Dim pObjectTypes As New clsComboList 
        pFault = pObjectTypes.FillEnums(clsEnums.enmEnum.ObjectType, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pObjectTypes.Remove(pObjectTypes.FindByKey(clsEnums.enmObjectType.UD)) 
        pObjectTypes.SortByText() 
        If pObjectTypes IsNot Nothing AndAlso pObjectTypes.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo01Label) 
          .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo01 
          .MakeSmart() 
          .LoadControl(pObjectTypes, GetChoose(_Requester)) 
          .TabIndex = 3 
        End With 
 
        .String01Label.Text = If(_ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectToTranslate.enmProperty.Object), _ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText(csObjectToTranslate.enmProperty.Object), "Object") 
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
 
        .String02Label.Text = If(_ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectToTranslate.enmProperty.Item), _ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText(csObjectToTranslate.enmProperty.Item), "Item") 
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
 
        .Text01Label.Text = If(_ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectToTranslate.enmProperty.ID), _ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText(csObjectToTranslate.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 8 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 9 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectToTranslate.enmProperty.ObjectType), _ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText(csObjectToTranslate.enmProperty.ObjectType), "Object Type") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 10 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectToTranslate.enmProperty.Object), _ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText(csObjectToTranslate.enmProperty.Object), "Object") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 11 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText.ContainsKey(csObjectToTranslate.enmProperty.Item), _ctlObjectToTranslateCol.LoadParameters.ColumnsHeaderText(csObjectToTranslate.enmProperty.Item), "Item") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 12 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
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
      If .Combo01.SelectedItem IsNot Nothing Then 
        pObjectType = CType(CType(.Combo01.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmObjectType) 
        _SearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.ObjectType, pObjectType) 
      End If 
      If .String01Text.Text <> "" Then 
        pObject = .String01Text.Text 
        pObjectWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.Object, pObject) 
        _SearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.ObjectWildcardType, pObjectWildcardType) 
      End If 
      If .String02Text.Text <> "" Then 
        pItem = .String02Text.Text 
        pItemWildcardType = CType(CType(.String02WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.Item, pItem) 
        _SearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.ItemWildcardType, pItemWildcardType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csObjectToTranslateCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csObjectToTranslateCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csObjectToTranslateCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByObjectType = True 
        pDoSum = True 
        _SearchFilters.Add(csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByObjectType, pGroupByObjectType) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByObject = True 
        pDoSum = True 
        _SearchFilters.Add(csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByObject, pGroupByObject) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByItem = True 
        pDoSum = True 
        _SearchFilters.Add(csObjectToTranslateCol.enmFillSumOnTheFlyParameters.GroupByItem, pGroupByItem) 
      End If 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csObjectToTranslateCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csObjectToTranslateCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csObjectToTranslateCol.enmListDefinition.Dir) Then _SearchFilters.Add(csObjectToTranslateCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_ObjectToTranslateCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_ObjectToTranslateCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csObjectToTranslate.enmProperty.ID, "ID") 
      End With 
      _ObjectToTranslateCol = New csObjectToTranslateCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ObjectToTranslateCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _ObjectToTranslateCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ObjectToTranslateCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ObjectToTranslateCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ObjectToTranslateCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ObjectToTranslate" 
      RaiseEvent evtOverrideLoadCtlObjectToTranslateCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _ObjectToTranslateCol = New csObjectToTranslateCol() 
      pFault = _ObjectToTranslateCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_ObjectToTranslateCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _ObjectToTranslateCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csObjectToTranslate.enmProperty.ID, "Count") 
        If pGroupByObjectType = False Then .ColumnsHide.Add(csObjectToTranslate.enmProperty.ObjectType) 
        If pGroupByObject = False Then .ColumnsHide.Add(csObjectToTranslate.enmProperty.Object) 
        If pGroupByItem = False Then .ColumnsHide.Add(csObjectToTranslate.enmProperty.Item) 
        .ColumnsHide.Add(csObjectToTranslate.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlObjectToTranslateCol.Visible = True 
    pFault = _ctlObjectToTranslateCol.LoadControl(_ObjectToTranslateCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csObjectToTranslateCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csObjectToTranslateCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlObjectToTranslate.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlObjectToTranslate.Controls(0).Name) 
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
    _ObjectToTranslateID = -2 
    pFault = ActivateControl("ctlc_ObjectToTranslate") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlObjectToTranslate() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlObjectToTranslate.Visible = True 'new 
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
 
  Private Sub _ctlObjectToTranslateCol_evtTimerTripped() Handles _ctlObjectToTranslateCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtObjectToTranslateTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlObjectToTranslateCol.ObjectToTranslateCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlObjectToTranslateCol.ObjectToTranslateCol(0).ID 
 
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
    If _ObjectToTranslateCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csObjectToTranslate() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csObjectToTranslateCol = CType(CallByName(_ObjectToTranslateCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csObjectToTranslateCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csObjectToTranslateCol = CType(CallByName(_ObjectToTranslateCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csObjectToTranslateCol) 
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
                  ccHelper.GetPropertyTypeName(New csObjectToTranslateCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csObjectToTranslateCol = CType(CallByName(_ObjectToTranslateCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csObjectToTranslateCol) 
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
 
  Private Sub cc_ctlPnlObjectToTranslate_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
  Private Sub _ctlPnlc_ObjectToTranslate_ccevtOverrideFillObjectTranslationCol(ByRef rObjectTranslationCol As csObjectTranslationCol, ByRef rGridTitle As String) Handles Me.evtOverrideFillObjectTranslationCol 
    Dim pFault As clsFault 
    pFault = ccDatabaseMaintenance.FillAllTranslationPossibilities(_ObjectToTranslateID, clsEnums.enmLanguage.UD, _Requester, rObjectTranslationCol) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
  End Sub 
  Private Sub cc_ctlPnlc_ObjectToTranslate_evtOverrideLoadCtlObjectToTranslateCol(ByRef rLoadParameters As ctlc_ObjectToTranslateCol.clsLoadParameters) Handles Me.evtOverrideLoadCtlObjectToTranslateCol 
    rLoadParameters.ReadOnly = False 
    rLoadParameters.TruncateStrings = False 
  End Sub 
 
  Private Sub cc_ctlPnlc_ObjectToTranslate_evtOverrideLoadCtlObjectTranslationCol(ByRef rLoadParameters As ctlc_ObjectTranslationCol.clsLoadParameters) Handles Me.evtOverrideLoadCtlObjectTranslationCol 
    rLoadParameters.ReadOnly = False 
    rLoadParameters.TruncateStrings = False 
  End Sub 
  
End Class 
