Public Class ctlPnlc_TableSize 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlTableSizeCol As ctlc_TableSizeCol 
  Private WithEvents _ctlTableSize As ctlc_TableSize 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _TableSizeID As Long 
 
  'The data holders 
  Private _TableSizeCol As csTableSizeCol 
  Private _TableSize As csTableSize 
 
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
  Public Event evtOverrideLoadCboTableSize(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetTableSizeIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillTableSizeCol(ByRef rTableSizeCol As csTableSizeCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlTableSizeCol(ByRef rLoadParameters As ctlc_TableSizeCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlTableSize(ByRef rLoadParameters As ctlc_TableSize.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreTableSizeCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtTableSizeTimerTripped(ByRef rCancel As Boolean) 
  
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
 
    lnkTableSizeCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkTableSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vTableSizeID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _TableSizeID = CType(vTableSizeID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlTableSize.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkTableSizeCol.Visible = False 
    _ShowIntelligentCombo = True 
    chkGrid.Checked = False 
 
    'since we're in a view with no data 
    btnRefresh.Visible = False 
    chkGrid.Visible = False 
 
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
      pFault = LoadCboTableSizes(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _TableSizeID > -1 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_TableSizeID) 
      End If 
      ChooseTableSize() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_TableSize") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _TableSizeID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlc_TableSize" OrElse pControlName = "ctlTableSize" Then 
      lnkTableSize.ForeColor = Color.Black : lnkTableSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkTableSize.BackColor = Color.Wheat 
      If _ctlTableSize Is Nothing Then 
        _ctlTableSize = New ctlc_TableSize() 
        _ctlTableSize.Dock = DockStyle.Fill 
        pnlTableSize.Controls.Add(_ctlTableSize) 
        _ctlTableSize.Visible = False 
      End If 
      If _TableSizeID = -1 Then 
        pnlTableSize.Visible = False 
      End If 
      'If _TableSize Is Nothing Then 
      pFault = RefreshCtlTableSize() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlTableSize.TableSize.IsEmpty AndAlso _TableSizeID <> -2 Then 
        pnlTableSize.Visible = False 
      End If 
      _ctlTableSize.Name = "ctlc_TableSize" 
      _ActiveControl = _ctlTableSize 
      _ctlTableSize.BringToFront() 
      _ctlTableSize.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_TableSizeCol" Then 
      lnkTableSizeCol.ForeColor = Color.Black : lnkTableSizeCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkTableSizeCol.BackColor = Color.Wheat 
      If _ctlTableSizeCol Is Nothing Then 
        _ctlTableSizeCol = New ctlc_TableSizeCol() 
        _ctlTableSizeCol.Dock = DockStyle.Fill 
        pnlTableSize.Controls.Add(_ctlTableSizeCol) 
        _ctlTableSizeCol.Visible = False 
      End If  
      pnlTableSize.Visible = True 
      If _TableSizeCol Is Nothing Then 
        pFault = RefreshCtlTableSizeCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlTableSizeCol.Name = "ctlc_TableSizeCol" 
      _ActiveControl = _ctlTableSizeCol 
      _ctlTableSizeCol.BringToFront() 
      _ctlTableSizeCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-TableSize-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("TableSize", _Requester) 
 
    lnkTableSizeCol.Text = CCTextTranslate("List", _Requester) 
    lnkTableSize.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlTableSize.Controls(0) Is _ctlTableSize Then 
      If _TableSizeID = -1 Then 
        pnlTableSize.Visible = False 
      End If 
    ElseIf pnlTableSize.Controls(0) Is _ctlTableSizeCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pTableSizeID As Long = _TableSizeID 
      If ccHelper.IsNumeric(pText) Then _TableSizeID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetTableSizeIDFromIntelliComboText(pText) 
      If pTableSizeID <> _TableSizeID Then 
        _TableSize = Nothing 
        pFault = ActivateControl("ctlc_TableSize") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlTableSize.Controls(0) Is _ctlTableSize Then 
      pFault = RefreshCtlTableSize() 
    ElseIf pnlTableSize.Controls(0) Is _ctlTableSizeCol Then 
      pFault = RefreshCtlTableSizeCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlTableSize.Controls(0).Name, "", "TRGT-TableSize-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboTableSizes(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlTableSizeCol_evtRowClicked(ByVal vTableSize As Object) Handles _ctlTableSizeCol.evtRowClicked 
    
    If vTableSize Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pTableSize As csTableSize = CType(vTableSize, csTableSize) 
    _TableSizeID = pTableSize.ID 
    
    If _ActiveControl Is _ctlTableSizeCol Then 
      'Group by's not done since this is a view with no data 
      
      btnFilter.Visible = True 
      lblSecondaryTitle.Visible = False 
    Else 
      btnFilter.Visible = False 
      lblSecondaryTitle.Visible = True 
    End If 
    
    ChooseTableSize() 
    
    Try 
      MyIntelliCombo.ValueSelect(_TableSizeID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pTableSize.TableName
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseTableSize() 
    _TableSize = Nothing 
    lnkTableSize.Visible = True 
  End Sub 
  Private Sub _ctlTableSizeCol_evtRowDoubleClicked(ByVal vTableSize As csTableSize, ByRef rHandled As Boolean) Handles _ctlTableSizeCol.evtRowDoubleClicked 
    If lnkTableSize.Parent IsNot flpMenu Then Exit Sub 
    If vTableSize Is Nothing Then Exit Sub 
 
    'Group by's not done since this is a view with no data 
    If lnkTableSize.Parent IsNot flpMenu Then Exit Sub 
    If vTableSize Is Nothing Then Exit Sub 
 
    Dim pIgnore As Boolean = False 
    RaiseEvent evtIgnoreTableSizeCol_evtRowDoubleClicked(pIgnore) 
    If pIgnore = True Then Exit Sub 
 
    _TableSizeID = CType(vTableSize, csTableSize).ID 
 
    rHandled = True 
 
    Dim pFault As clsFault 
    Cursor = Cursors.WaitCursor 
 
    pFault = ActivateControl("ctlc_TableSize") 
 
    _NestedFormsCount += 1 
    lblBack.Visible = True 
    chkGrid.Enabled = False 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  Private Sub _ctlTableSizeCol_evtUnChosen() Handles _ctlTableSizeCol.evtUnChosen 
 
    _TableSizeID = -1 
    _TableSize = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkTableSize.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkTableSizeCol.Click, 
      lnkTableSize.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkTableSize OrElse (lnk Is lnkTableSizeCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlTableSizeCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_TableSizeCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csTableSize.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csTableSizeCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillTableSizeCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _TableSizeCol = New csTableSizeCol() 
      'Fill not done since this is a view with no data 
      pFault.SetOK() 
    Else 
      _TableSizeCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _TableSizeCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlTableSizeCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see TableSize" 
    
    Dim pTableSizeID As Long = _TableSizeID 
    
    pFault = _ctlTableSizeCol.LoadControl(_TableSizeCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlTableSizeCol.Visible = True 
    
    _ctlTableSizeCol.Refresh() 
    If pTableSizeID <> -1 Then 
      Dim pTableSizeCol As csTableSizeCol = CType(_ctlTableSizeCol.bsCtlTableSize.DataSource, csTableSizeCol) 
      Dim pTableSize As csTableSize = pTableSizeCol.FindByID(pTableSizeID) 
      If pTableSize.ID > -1 Then 
        _ctlTableSizeCol.bsCtlTableSize.CurrencyManager.Position = pTableSizeCol.IndexOf(pTableSize) 
        _ctlTableSizeCol.dgvTableSize.Rows(pTableSizeCol.IndexOf(pTableSize)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlTableSize() As clsFault 
    Dim pFault As New clsFault 
    
    If _TableSizeID > -1 Then 
      ChooseTableSize() 
      _TableSize = New csTableSize() 
      pFault = _TableSize.GetByID(_TableSizeID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _TableSize = New csTableSize() 
    End If 
    'lblSecondaryTitle.Text = _TableSize.TableName    
     
    Dim pLoadParameters As New ctlc_TableSize.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = True 
    End With 
    RaiseEvent evtOverrideLoadCtlTableSize(pLoadParameters)
    pFault = _ctlTableSize.LoadControl(_TableSize, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlTableSize.Visible = True 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Function LoadCboTableSizes(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_TableSizeDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboTableSize(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _TableSizeID >= 0 Then 
      MyIntelliCombo.ValueSelect(_TableSizeID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _TableSizeID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _TableSizeID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetTableSizeIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _TableSizeID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _TableSizeID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _TableSizeID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _TableSizeID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseTableSize() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_TableSize", StringComparison.OrdinalIgnoreCase) AndAlso _TableSizeID > 0 Then 
        'to avoid getting ObjectNotFound 
        _TableSize = New csTableSize() 
        pFault = _TableSize.GetByID(_TableSizeID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_TableSize") 
    End If 
    pnlTableSize.Visible = True 
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
    pnlTableSize.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkTableSizeCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _TableSizeID <> -1 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_TableSizeCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkTableSizeCol.Visible = False 
      _ActiveControl = _ctlTableSize 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboTableSizes(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _TableSizeID <> -1 Then 
        pFault = ActivateControl("ctlc_TableSize") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlTableSize.Visible = False 
        _TableSizeID = -1 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _TableSizeID > 0 Then pnlTableSize.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkTableSizeCol.MouseEnter, 
                  lnkTableSize.MouseEnter, 
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
                  lnkTableSizeCol.MouseLeave, 
                  lnkTableSize.MouseLeave, 
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
    Dim pTableName As String = Nothing 
    Dim pTableNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    
    Dim pSumNumberOfRows As Boolean = False 
    Dim pSumReservedSizeKb As Boolean = False 
    Dim pSumDataSizeKb As Boolean = False 
    Dim pSumIndexSizeKb As Boolean = False 
    Dim pSumUnusedSizeKb As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Table Sizes"  
  
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
        .String01Label.Text = If(_ctlTableSizeCol.LoadParameters.ColumnsHeaderText.ContainsKey(csTableSize.enmProperty.TableName), _ctlTableSizeCol.LoadParameters.ColumnsHeaderText(csTableSize.enmProperty.TableName), "Table Name") 
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
 
        .Text01Label.Text = If(_ctlTableSizeCol.LoadParameters.ColumnsHeaderText.ContainsKey(csTableSize.enmProperty.ID), _ctlTableSizeCol.LoadParameters.ColumnsHeaderText(csTableSize.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 5 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 6 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlTableSizeCol.LoadParameters.ColumnsHeaderText.ContainsKey(csTableSize.enmProperty.NumberOfRows), _ctlTableSizeCol.LoadParameters.ColumnsHeaderText(csTableSize.enmProperty.NumberOfRows), "Number Of Rows") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 7 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlTableSizeCol.LoadParameters.ColumnsHeaderText.ContainsKey(csTableSize.enmProperty.ReservedSizeKb), _ctlTableSizeCol.LoadParameters.ColumnsHeaderText(csTableSize.enmProperty.ReservedSizeKb), "Reserved Size Kb") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 8 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
        .lblSumField03.Text = If(_ctlTableSizeCol.LoadParameters.ColumnsHeaderText.ContainsKey(csTableSize.enmProperty.DataSizeKb), _ctlTableSizeCol.LoadParameters.ColumnsHeaderText(csTableSize.enmProperty.DataSizeKb), "Data Size Kb") 
        .chkSumField03.Checked = False 
        .chkSumField03.TabIndex = 9 
        .flpSumColumns.Controls.Add(.lblSumField03) 
        .flpSumColumns.Controls.Add(.chkSumField03) 
 
        .lblSumField04.Text = If(_ctlTableSizeCol.LoadParameters.ColumnsHeaderText.ContainsKey(csTableSize.enmProperty.IndexSizeKb), _ctlTableSizeCol.LoadParameters.ColumnsHeaderText(csTableSize.enmProperty.IndexSizeKb), "Index Size Kb") 
        .chkSumField04.Checked = False 
        .chkSumField04.TabIndex = 10 
        .flpSumColumns.Controls.Add(.lblSumField04) 
        .flpSumColumns.Controls.Add(.chkSumField04) 
 
        .lblSumField05.Text = If(_ctlTableSizeCol.LoadParameters.ColumnsHeaderText.ContainsKey(csTableSize.enmProperty.UnusedSizeKb), _ctlTableSizeCol.LoadParameters.ColumnsHeaderText(csTableSize.enmProperty.UnusedSizeKb), "Unused Size Kb") 
        .chkSumField05.Checked = False 
        .chkSumField05.TabIndex = 11 
        .flpSumColumns.Controls.Add(.lblSumField05) 
        .flpSumColumns.Controls.Add(.chkSumField05) 
 
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
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csTableSizeCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csTableSizeCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csTableSizeCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csTableSizeCol.enmListDefinition.Dir, pDir) 
      End If 
 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csTableSizeCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csTableSizeCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csTableSizeCol.enmListDefinition.Dir) Then _SearchFilters.Add(csTableSizeCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_TableSizeCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_TableSizeCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csTableSize.enmProperty.ID, "ID") 
      End With 
      'Commented out since this is a view. Replace with your own query in the prt file 
      _TableSizeCol = New csTableSizeCol() 
 
      'If IsFiltered() Then 
      '  btnFilter.BackColor = Color.Pink 
      '  lblTitle.ForeColor = Color.Red 
      '  _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
      '  _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      '  pFault = _TableSizeCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      'Else 
      '  btnFilter.BackColor = Me.BackColor 
      '  lblTitle.ForeColor = Color.Black 
      '  _Tooltip.SetToolTip(lblTitle, "") 
      '  _Tooltip.SetToolTip(btnFilter, "") 
      '  pFault = _TableSizeCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      'End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _TableSizeCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _TableSizeCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _TableSizeCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see TableSize" 
      RaiseEvent evtOverrideLoadCtlTableSizeCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
      '  btnFilter.BackColor = Color.Pink 
      '  lblTitle.ForeColor = Color.Red 
      '  _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
      '  _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _TableSizeCol = New csTableSizeCol() 
      'pFault = _TableSizeCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_TableSizeCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _TableSizeCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csTableSize.enmProperty.ID, "Count") 
        If pSumNumberOfRows = False Then .ColumnsHide.Add(csTableSize.enmProperty.NumberOfRows) 
        If pSumReservedSizeKb = False Then .ColumnsHide.Add(csTableSize.enmProperty.ReservedSizeKb) 
        If pSumDataSizeKb = False Then .ColumnsHide.Add(csTableSize.enmProperty.DataSizeKb) 
        If pSumIndexSizeKb = False Then .ColumnsHide.Add(csTableSize.enmProperty.IndexSizeKb) 
        If pSumUnusedSizeKb = False Then .ColumnsHide.Add(csTableSize.enmProperty.UnusedSizeKb) 
        .ColumnsHide.Add(csTableSize.enmProperty.TableName) 
        .ColumnsHide.Add(csTableSize.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlTableSizeCol.Visible = True 
    pFault = _ctlTableSizeCol.LoadControl(_TableSizeCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csTableSizeCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csTableSizeCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlTableSize.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlTableSize.Controls(0).Name) 
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
 
  Private Sub _ctlTableSizeCol_evtTimerTripped() Handles _ctlTableSizeCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtTableSizeTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlTableSizeCol.TableSizeCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlTableSizeCol.TableSizeCol(0).ID 
 
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
    If _TableSizeCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csTableSize() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csTableSizeCol = CType(CallByName(_TableSizeCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csTableSizeCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csTableSizeCol = CType(CallByName(_TableSizeCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csTableSizeCol) 
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
                  ccHelper.GetPropertyTypeName(New csTableSizeCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csTableSizeCol = CType(CallByName(_TableSizeCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csTableSizeCol) 
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
 
  Private Sub cc_ctlPnlTableSize_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
