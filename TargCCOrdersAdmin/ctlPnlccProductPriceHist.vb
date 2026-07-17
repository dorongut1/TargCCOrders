Public Class ctlPnlccProductPriceHist 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlProductPriceHistCol As ctlccProductPriceHistCol 
  Private WithEvents _ctlProductPriceHist As ctlccProductPriceHist 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _ProductPriceHistID As Long 
 
  'The data holders 
  Private _ProductPriceHistCol As clsProductPriceHistCol 
  Private _ProductPriceHist As clsProductPriceHist 
 
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
  Public Event evtOverrideLoadCboProductPriceHist(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetProductPriceHistIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillProductPriceHistCol(ByRef rProductPriceHistCol As clsProductPriceHistCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlProductPriceHistCol(ByRef rLoadParameters As ctlccProductPriceHistCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlProductPriceHist(ByRef rLoadParameters As ctlccProductPriceHist.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreProductPriceHistCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtProductPriceHistTimerTripped(ByRef rCancel As Boolean) 
  
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
 
    lnkProductPriceHistCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkProductPriceHist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vProductPriceHistID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _ProductPriceHistID = CType(vProductPriceHistID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlProductPriceHist.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkProductPriceHistCol.Visible = False 
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
      pFault = LoadCboProductPriceHists(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _ProductPriceHistID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_ProductPriceHistID) 
      End If 
      ChooseProductPriceHist() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlccProductPriceHist") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _ProductPriceHistID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlccProductPriceHist" OrElse pControlName = "ctlProductPriceHist" Then 
      lnkProductPriceHist.ForeColor = Color.Black : lnkProductPriceHist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkProductPriceHist.BackColor = Color.Wheat 
      If _ctlProductPriceHist Is Nothing Then 
        _ctlProductPriceHist = New ctlccProductPriceHist() 
        _ctlProductPriceHist.Dock = DockStyle.Fill 
        _ctlProductPriceHist.Controls.RemoveByKey("btnAdd") 
        pnlProductPriceHist.Controls.Add(_ctlProductPriceHist) 
        _ctlProductPriceHist.Visible = False 
      End If 
      If _ProductPriceHistID = 0 Then 
        pnlProductPriceHist.Visible = False 
      End If 
      'If _ProductPriceHist Is Nothing Then 
      pFault = RefreshCtlProductPriceHist() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlProductPriceHist.ProductPriceHist.IsEmpty AndAlso _ProductPriceHistID <> -2 Then 
        pnlProductPriceHist.Visible = False 
      End If 
      _ctlProductPriceHist.Name = "ctlccProductPriceHist" 
      _ActiveControl = _ctlProductPriceHist 
      _ctlProductPriceHist.BringToFront() 
      _ctlProductPriceHist.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlccProductPriceHistCol" Then 
      lnkProductPriceHistCol.ForeColor = Color.Black : lnkProductPriceHistCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkProductPriceHistCol.BackColor = Color.Wheat 
      If _ctlProductPriceHistCol Is Nothing Then 
        _ctlProductPriceHistCol = New ctlccProductPriceHistCol() 
        _ctlProductPriceHistCol.Dock = DockStyle.Fill 
        pnlProductPriceHist.Controls.Add(_ctlProductPriceHistCol) 
        _ctlProductPriceHistCol.Visible = False 
      End If  
      pnlProductPriceHist.Visible = True 
      If _ProductPriceHistCol Is Nothing Then 
        pFault = RefreshCtlProductPriceHistCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlProductPriceHistCol.Name = "ctlccProductPriceHistCol" 
      _ActiveControl = _ctlProductPriceHistCol 
      _ctlProductPriceHistCol.BringToFront() 
      _ctlProductPriceHistCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-ProductPriceHist-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("ProductPriceHist", _Requester) 
 
    lnkProductPriceHistCol.Text = CCTextTranslate("List", _Requester) 
    lnkProductPriceHist.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlProductPriceHist.Controls(0) Is _ctlProductPriceHist Then 
      If _ProductPriceHistID = 0 Then 
        pnlProductPriceHist.Visible = False 
      End If 
    ElseIf pnlProductPriceHist.Controls(0) Is _ctlProductPriceHistCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pProductPriceHistID As Long = _ProductPriceHistID 
      If ccHelper.IsNumeric(pText) Then _ProductPriceHistID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetProductPriceHistIDFromIntelliComboText(pText) 
      If pProductPriceHistID <> _ProductPriceHistID Then 
        _ProductPriceHist = Nothing 
        pFault = ActivateControl("ctlccProductPriceHist") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlProductPriceHist.Controls(0) Is _ctlProductPriceHist Then 
      pFault = RefreshCtlProductPriceHist() 
    ElseIf pnlProductPriceHist.Controls(0) Is _ctlProductPriceHistCol Then 
      pFault = RefreshCtlProductPriceHistCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlProductPriceHist.Controls(0).Name, "", "TRGT-ProductPriceHist-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboProductPriceHists(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlProductPriceHistCol_evtRowClicked(ByVal vProductPriceHist As Object) Handles _ctlProductPriceHistCol.evtRowClicked 
    
    If vProductPriceHist Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pProductPriceHist As clsProductPriceHist = CType(vProductPriceHist, clsProductPriceHist) 
    _ProductPriceHistID = pProductPriceHist.ID 
    
    If _ActiveControl Is _ctlProductPriceHistCol Then 
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
    
    ChooseProductPriceHist() 
    
    Try 
      MyIntelliCombo.ValueSelect(_ProductPriceHistID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pProductPriceHist.ProductID.ToString("#,##0") & " " & pProductPriceHist.CustomerType.ToString
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseProductPriceHist() 
    _ProductPriceHist = Nothing 
    lnkProductPriceHist.Visible = True 
  End Sub 
  Private Sub _ctlProductPriceHistCol_evtRowDoubleClicked(ByVal vProductPriceHist As clsProductPriceHist, ByRef rHandled As Boolean) Handles _ctlProductPriceHistCol.evtRowDoubleClicked 
    If lnkProductPriceHist.Parent IsNot flpMenu Then Exit Sub 
    If vProductPriceHist Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreProductPriceHistCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vProductPriceHist.ID, vProductPriceHist.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _ProductPriceHistID = vProductPriceHist.ID 
      'MyIntelliCombo.ValueSelect(_ProductPriceHistID) 
      pFault = ActivateControl("ctlccProductPriceHist") 
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
      pFault = _ProductPriceHistCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ProductPriceHistCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ProductPriceHistCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProductPriceHistCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlccProductPriceHistCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsProductPriceHist.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ProductPriceHist" 
      pFault = _ctlProductPriceHistCol.LoadControl(_ProductPriceHistCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlProductPriceHistCol_evtUnChosen() Handles _ctlProductPriceHistCol.evtUnChosen 
 
    _ProductPriceHistID = 0 
    _ProductPriceHist = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkProductPriceHist.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkProductPriceHistCol.Click, 
      lnkProductPriceHist.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkProductPriceHist OrElse (lnk Is lnkProductPriceHistCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlProductPriceHistCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlccProductPriceHistCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(clsProductPriceHist.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As clsProductPriceHistCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillProductPriceHistCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _ProductPriceHistCol = New clsProductPriceHistCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ProductPriceHistCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlProductPriceHistCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlProductPriceHistCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _ProductPriceHistCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlProductPriceHistCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _ProductPriceHistCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _ProductPriceHistCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProductPriceHistCol.Count) 
      End If 
    Else 
      _ProductPriceHistCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _ProductPriceHistCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlProductPriceHistCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ProductPriceHist" 
    
    Dim pProductPriceHistID As Long = _ProductPriceHistID 
    
    pFault = _ctlProductPriceHistCol.LoadControl(_ProductPriceHistCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlProductPriceHistCol.Visible = True 
    
    _ctlProductPriceHistCol.Refresh() 
    If pProductPriceHistID <> 0 Then 
      Dim pProductPriceHistCol As clsProductPriceHistCol = CType(_ctlProductPriceHistCol.bsCtlProductPriceHist.DataSource, clsProductPriceHistCol) 
      Dim pProductPriceHist As clsProductPriceHist = pProductPriceHistCol.FindByID(pProductPriceHistID) 
      If pProductPriceHist.ID > 0 Then 
        _ctlProductPriceHistCol.bsCtlProductPriceHist.CurrencyManager.Position = pProductPriceHistCol.IndexOf(pProductPriceHist) 
        _ctlProductPriceHistCol.dgvProductPriceHist.Rows(pProductPriceHistCol.IndexOf(pProductPriceHist)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlProductPriceHist() As clsFault 
    Dim pFault As New clsFault 
    
    If _ProductPriceHistID > 0 Then 
      ChooseProductPriceHist() 
      _ProductPriceHist = New clsProductPriceHist() 
      pFault = _ProductPriceHist.GetByID(_ProductPriceHistID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _ProductPriceHist = New clsProductPriceHist() 
    End If 
    'lblSecondaryTitle.Text = _ProductPriceHist.ProductID.ToString("#,##0") & " " & _ProductPriceHist.CustomerType.ToString    
     
    Dim pLoadParameters As New ctlccProductPriceHist.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlProductPriceHist(pLoadParameters)
    pFault = _ctlProductPriceHist.LoadControl(_ProductPriceHist, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlProductPriceHist.Visible = True 
    If _ProductPriceHistID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlProductPriceHist.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlProductPriceHist.btnAdd.Visible = False 
      End If 
    End If 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlProductPriceHist_evtDeleted(ByVal vProductPriceHistID As Long) Handles _ctlProductPriceHist.evtDeleted 
    _ProductPriceHistCol = Nothing 
    Dim pFault As clsFault 
    _ProductPriceHistID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboProductPriceHists(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlProductPriceHist() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlProductPriceHist.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkProductPriceHistCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlProductPriceHist_evtCancelledEdit(ByVal vProductPriceHist As clsProductPriceHist) Handles _ctlProductPriceHist.evtCancelledEdit 
    RefreshCtlProductPriceHist() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboProductPriceHists(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlProductPriceHist.btnAdd.Visible = False 
      If _ProductPriceHistID = 0 OrElse _ProductPriceHistID = -2 Then 
        pnlProductPriceHist.Visible = False 
      Else 
        pnlProductPriceHist.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlProductPriceHist.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlccProductPriceHistCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlProductPriceHist_evtUpdated(ByVal vWhichProperty As clsProductPriceHist.enmUpdateType, ByVal vProductPriceHist As clsProductPriceHist) Handles _ctlProductPriceHist.evtUpdated 
    _ProductPriceHistCol = Nothing 
    Dim pFault As clsFault 
    _ProductPriceHistID = CType(vProductPriceHist, clsProductPriceHist).ID 
    If _ActiveControl.Name = "ctlccProductPriceHist" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboProductPriceHists(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlProductPriceHist() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlProductPriceHist.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboProductPriceHists(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccProductPriceHistDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboProductPriceHist(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _ProductPriceHistID >= 0 Then 
      MyIntelliCombo.ValueSelect(_ProductPriceHistID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ProductPriceHistID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _ProductPriceHistID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetProductPriceHistIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _ProductPriceHistID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _ProductPriceHistID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _ProductPriceHistID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _ProductPriceHistID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseProductPriceHist() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlccProductPriceHist", StringComparison.OrdinalIgnoreCase) AndAlso _ProductPriceHistID > 0 Then 
        'to avoid getting ObjectNotFound 
        _ProductPriceHist = New clsProductPriceHist() 
        pFault = _ProductPriceHist.GetByID(_ProductPriceHistID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlccProductPriceHist") 
    End If 
    pnlProductPriceHist.Visible = True 
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
    pnlProductPriceHist.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkProductPriceHistCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _ProductPriceHistID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlccProductPriceHistCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkProductPriceHistCol.Visible = False 
      _ActiveControl = _ctlProductPriceHist 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboProductPriceHists(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _ProductPriceHistID <> 0 Then 
        pFault = ActivateControl("ctlccProductPriceHist") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlProductPriceHist.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlProductPriceHist.Visible = False 
        _ProductPriceHistID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _ProductPriceHistID > 0 Then pnlProductPriceHist.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkProductPriceHistCol.MouseEnter, 
                  lnkProductPriceHist.MouseEnter, 
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
                  lnkProductPriceHistCol.MouseLeave, 
                  lnkProductPriceHist.MouseLeave, 
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
  Private Sub _ctlProductPriceHist_evtAdd(ByVal vProductPriceHist As clsProductPriceHist) Handles _ctlProductPriceHist.evtAdd 
    lnkProductPriceHistCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    
    Dim pSumProductID As Boolean = False 
    Dim pSumBaseCost As Boolean = False 
    Dim pSumSellingPrice As Boolean = False 
    Dim pSumMinQuantity As Boolean = False 
    Dim pSumDiscountPercent As Boolean = False 
    Dim pSumOriginalPriceID As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Product Price Hists"  
  
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
        .Text01Label.Text = If(_ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProductPriceHist.enmProperty.ID), _ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText(clsProductPriceHist.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 3 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 4 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProductPriceHist.enmProperty.ProductID), _ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText(clsProductPriceHist.enmProperty.ProductID), "Product ID") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 5 
        .flpSumColumns.Controls.Add(.lblSumField01) 
        .flpSumColumns.Controls.Add(.chkSumField01) 
 
        .lblSumField02.Text = If(_ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProductPriceHist.enmProperty.BaseCost), _ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText(clsProductPriceHist.enmProperty.BaseCost), "Base Cost") 
        .chkSumField02.Checked = False 
        .chkSumField02.TabIndex = 6 
        .flpSumColumns.Controls.Add(.lblSumField02) 
        .flpSumColumns.Controls.Add(.chkSumField02) 
 
        .lblSumField03.Text = If(_ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProductPriceHist.enmProperty.SellingPrice), _ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText(clsProductPriceHist.enmProperty.SellingPrice), "Selling Price") 
        .chkSumField03.Checked = False 
        .chkSumField03.TabIndex = 7 
        .flpSumColumns.Controls.Add(.lblSumField03) 
        .flpSumColumns.Controls.Add(.chkSumField03) 
 
        .lblSumField04.Text = If(_ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProductPriceHist.enmProperty.MinQuantity), _ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText(clsProductPriceHist.enmProperty.MinQuantity), "Min Quantity") 
        .chkSumField04.Checked = False 
        .chkSumField04.TabIndex = 8 
        .flpSumColumns.Controls.Add(.lblSumField04) 
        .flpSumColumns.Controls.Add(.chkSumField04) 
 
        .lblSumField05.Text = If(_ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProductPriceHist.enmProperty.DiscountPercent), _ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText(clsProductPriceHist.enmProperty.DiscountPercent), "Discount Percent") 
        .chkSumField05.Checked = False 
        .chkSumField05.TabIndex = 9 
        .flpSumColumns.Controls.Add(.lblSumField05) 
        .flpSumColumns.Controls.Add(.chkSumField05) 
 
        .lblSumField06.Text = If(_ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText.ContainsKey(clsProductPriceHist.enmProperty.OriginalPriceID), _ctlProductPriceHistCol.LoadParameters.ColumnsHeaderText(clsProductPriceHist.enmProperty.OriginalPriceID), "Original Price ID") 
        .chkSumField06.Checked = False 
        .chkSumField06.TabIndex = 10 
        .flpSumColumns.Controls.Add(.lblSumField06) 
        .flpSumColumns.Controls.Add(.chkSumField06) 
 
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
          _SearchFilters.Add(clsProductPriceHistCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(clsProductPriceHistCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(clsProductPriceHistCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(clsProductPriceHistCol.enmListDefinition.Dir, pDir) 
      End If 
 
    
      If .chkSumField01.Checked = True Then 
        pSumProductID = True 
        pDoSum = True 
      End If 
      
      If .chkSumField02.Checked = True Then 
        pSumBaseCost = True 
        pDoSum = True 
      End If 
      
      If .chkSumField03.Checked = True Then 
        pSumSellingPrice = True 
        pDoSum = True 
      End If 
      
      If .chkSumField04.Checked = True Then 
        pSumMinQuantity = True 
        pDoSum = True 
      End If 
      
      If .chkSumField05.Checked = True Then 
        pSumDiscountPercent = True 
        pDoSum = True 
      End If 
      
      If .chkSumField06.Checked = True Then 
        pSumOriginalPriceID = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(clsProductPriceHistCol.enmListDefinition.HowMany) Then _SearchFilters.Add(clsProductPriceHistCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(clsProductPriceHistCol.enmListDefinition.Dir) Then _SearchFilters.Add(clsProductPriceHistCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlccProductPriceHistCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlccProductPriceHistCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(clsProductPriceHist.enmProperty.ID, "ID") 
      End With 
      _ProductPriceHistCol = New clsProductPriceHistCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _ProductPriceHistCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _ProductPriceHistCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _ProductPriceHistCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _ProductPriceHistCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _ProductPriceHistCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see ProductPriceHist" 
      RaiseEvent evtOverrideLoadCtlProductPriceHistCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _ProductPriceHistCol = New clsProductPriceHistCol() 
      pFault = _ProductPriceHistCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlccProductPriceHistCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _ProductPriceHistCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(clsProductPriceHist.enmProperty.ID, "Count") 
        If pSumProductID = False Then .ColumnsHide.Add(clsProductPriceHist.enmProperty.ProductID) 
        If pSumBaseCost = False Then .ColumnsHide.Add(clsProductPriceHist.enmProperty.BaseCost) 
        If pSumSellingPrice = False Then .ColumnsHide.Add(clsProductPriceHist.enmProperty.SellingPrice) 
        If pSumMinQuantity = False Then .ColumnsHide.Add(clsProductPriceHist.enmProperty.MinQuantity) 
        If pSumDiscountPercent = False Then .ColumnsHide.Add(clsProductPriceHist.enmProperty.DiscountPercent) 
        If pSumOriginalPriceID = False Then .ColumnsHide.Add(clsProductPriceHist.enmProperty.OriginalPriceID) 
        .ColumnsHide.Add(clsProductPriceHist.enmProperty.CustomerType) 
        .ColumnsHide.Add(clsProductPriceHist.enmProperty.ValidFrom) 
        .ColumnsHide.Add(clsProductPriceHist.enmProperty.ValidTo) 
        .ColumnsHide.Add(clsProductPriceHist.enmProperty.ArchivedDate) 
        .ColumnsHide.Add(clsProductPriceHist.enmProperty.ArchivedReason) 
        .ColumnsHide.Add(clsProductPriceHist.enmProperty.Notes) 
        .ColumnsHide.Add(clsProductPriceHist.enmProperty.AddFieldsHere) 
        .ColumnsHide.Add(clsProductPriceHist.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlProductPriceHistCol.Visible = True 
    pFault = _ctlProductPriceHistCol.LoadControl(_ProductPriceHistCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(clsProductPriceHistCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(clsProductPriceHistCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlProductPriceHist.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlProductPriceHist.Controls(0).Name) 
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
    _ProductPriceHistID = -2 
    pFault = ActivateControl("ctlccProductPriceHist") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlProductPriceHist() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlProductPriceHist.Visible = True 'new 
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
 
  Private Sub _ctlProductPriceHistCol_evtTimerTripped() Handles _ctlProductPriceHistCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtProductPriceHistTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlProductPriceHistCol.ProductPriceHistCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlProductPriceHistCol.ProductPriceHistCol(0).ID 
 
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
    If _ProductPriceHistCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New clsProductPriceHist() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As clsProductPriceHistCol = CType(CallByName(_ProductPriceHistCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsProductPriceHistCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As clsProductPriceHistCol = CType(CallByName(_ProductPriceHistCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsProductPriceHistCol) 
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
                  ccHelper.GetPropertyTypeName(New clsProductPriceHistCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As clsProductPriceHistCol = CType(CallByName(_ProductPriceHistCol, "CloneBy" & pFieldName, CallType.Method, l.Value), clsProductPriceHistCol) 
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
 
  Private Sub cc_ctlPnlProductPriceHist_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
End Class 
