Public Class ctlPnlc_Mail 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlMailCol As ctlc_MailCol 
  Private WithEvents _ctlMail As ctlc_Mail 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _MailID As Long 
 
  'The data holders 
  Private _MailCol As csMailCol 
  Private _Mail As csMail 
 
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
  Public Event evtOverrideLoadCboMail(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetMailIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillMailCol(ByRef rMailCol As csMailCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlMailCol(ByRef rLoadParameters As ctlc_MailCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlMail(ByRef rLoadParameters As ctlc_Mail.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreMailCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtMailTimerTripped(ByRef rCancel As Boolean) 
  
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
 
    lnkMailCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkMail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vMailID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _MailID = CType(vMailID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlMail.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkMailCol.Visible = False 
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
      pFault = LoadCboMails(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _MailID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_MailID) 
      End If 
      ChooseMail() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_Mail") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _MailID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
      'load the item automatically, since there is only one 
      Dim pItem As clsComboListMember = CType(MyIntelliCombo.cbo.Items(0), clsComboListMember) 
      If pItem.KeyType <> clsEnums.enmComboListKeyType.UD Then 'It has to have a real member in the combolist 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(pItem.KeyLong) 
        MyIntelliCombo_evtComboListMemberChosen(pItem) 
      End If 
    End If 
 
    If _MailID = -1 Then chkGrid.Checked = True 
 
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
    
    If pControlName = "ctlc_Mail" OrElse pControlName = "ctlMail" Then 
      lnkMail.ForeColor = Color.Black : lnkMail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkMail.BackColor = Color.Wheat 
      If _ctlMail Is Nothing Then 
        _ctlMail = New ctlc_Mail() 
        _ctlMail.Dock = DockStyle.Fill 
        pnlMail.Controls.Add(_ctlMail) 
        _ctlMail.Visible = False 
      End If 
      If _MailID = 0 Then 
        pnlMail.Visible = False 
      End If 
      'If _Mail Is Nothing Then 
      pFault = RefreshCtlMail() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlMail.Mail.IsEmpty AndAlso _MailID <> -2 Then 
        pnlMail.Visible = False 
      End If 
      _ctlMail.Name = "ctlc_Mail" 
      _ActiveControl = _ctlMail 
      _ctlMail.BringToFront() 
      _ctlMail.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_MailCol" Then 
      lnkMailCol.ForeColor = Color.Black : lnkMailCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkMailCol.BackColor = Color.Wheat 
      If _ctlMailCol Is Nothing Then 
        _ctlMailCol = New ctlc_MailCol() 
        _ctlMailCol.Dock = DockStyle.Fill 
        pnlMail.Controls.Add(_ctlMailCol) 
        _ctlMailCol.Visible = False 
      End If  
      pnlMail.Visible = True 
      If _MailCol Is Nothing Then 
        pFault = RefreshCtlMailCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlMailCol.Name = "ctlc_MailCol" 
      _ActiveControl = _ctlMailCol 
      _ctlMailCol.BringToFront() 
      _ctlMailCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-Mail-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("Mail", _Requester) 
 
    lnkMailCol.Text = CCTextTranslate("List", _Requester) 
    lnkMail.Text = CCTextTranslate("Details", _Requester) 
 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlMail.Controls(0) Is _ctlMail Then 
      If _MailID = 0 Then 
        pnlMail.Visible = False 
      End If 
    ElseIf pnlMail.Controls(0) Is _ctlMailCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pMailID As Long = _MailID 
      If ccHelper.IsNumeric(pText) Then _MailID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetMailIDFromIntelliComboText(pText) 
      If pMailID <> _MailID Then 
        _Mail = Nothing 
        pFault = ActivateControl("ctlc_Mail") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlMail.Controls(0) Is _ctlMail Then 
      pFault = RefreshCtlMail() 
    ElseIf pnlMail.Controls(0) Is _ctlMailCol Then 
      pFault = RefreshCtlMailCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlMail.Controls(0).Name, "", "TRGT-Mail-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub _ctlMailCol_evtRowClicked(ByVal vMail As Object) Handles _ctlMailCol.evtRowClicked 
    
    If vMail Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pMail As csMail = CType(vMail, csMail) 
    _MailID = pMail.ID 
    
    If _ActiveControl Is _ctlMailCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csMailCol.enmFillSumOnTheFlyParameters.GroupByMessagingMode.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csMailCol.enmFillSumOnTheFlyParameters.GroupByRecipientEmail.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csMailCol.enmFillSumOnTheFlyParameters.GroupByWasSeen.ToString() Then 
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
    
    ChooseMail() 
    
    Try 
      MyIntelliCombo.ValueSelect(_MailID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pMail.ID.ToString("#,##0")

 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseMail() 
    _Mail = Nothing 
    lnkMail.Visible = True 
  End Sub 
  Private Sub _ctlMailCol_evtRowDoubleClicked(ByVal vMail As csMail, ByRef rHandled As Boolean) Handles _ctlMailCol.evtRowDoubleClicked 
    If lnkMail.Parent IsNot flpMenu Then Exit Sub 
    If vMail Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csMailCol.enmFillSumOnTheFlyParameters.GroupByMessagingMode.ToString() Then 
        If pSearchFilters.ContainsKey(csMailCol.enmFillOnTheFlyParameters.MessagingMode) Then pSearchFilters.Remove(csMailCol.enmFillOnTheFlyParameters.MessagingMode) 
        pSearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.MessagingMode, vMail.MessagingMode) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csMailCol.enmFillSumOnTheFlyParameters.GroupByRecipientEmail.ToString() Then 
        If pSearchFilters.ContainsKey(csMailCol.enmFillOnTheFlyParameters.RecipientEmail) Then pSearchFilters.Remove(csMailCol.enmFillOnTheFlyParameters.RecipientEmail) 
        pSearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.RecipientEmail, vMail.RecipientEmail) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csMailCol.enmFillSumOnTheFlyParameters.GroupByWasSeen.ToString() Then 
        If pSearchFilters.ContainsKey(csMailCol.enmFillOnTheFlyParameters.WasSeen) Then pSearchFilters.Remove(csMailCol.enmFillOnTheFlyParameters.WasSeen) 
        pSearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.WasSeen, vMail.WasSeen) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreMailCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vMail.ID, vMail.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _MailID = vMail.ID 
      'MyIntelliCombo.ValueSelect(_MailID) 
      pFault = ActivateControl("ctlc_Mail") 
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
      pFault = _MailCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _MailCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _MailCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _MailCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_MailCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csMail.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Mail" 
      pFault = _ctlMailCol.LoadControl(_MailCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlMailCol_evtUnChosen() Handles _ctlMailCol.evtUnChosen 
 
    _MailID = 0 
    _Mail = Nothing 
    lblSecondaryTitle.Text = "" 
    lnkMail.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkMailCol.Click, 
      lnkMail.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkMail OrElse (lnk Is lnkMailCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlMailCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_MailCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csMail.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csMailCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillMailCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _MailCol = New csMailCol() 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _MailCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlMailCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        If _ctlMailCol.chkAutoRefresh.Checked Then pHowmany = 15 
        pFault = _MailCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
        If pFault.isOK = False Then 
          _ctlMailCol.Timer?.Stop() 
          Return pFault 
        End If 
      End If 
 
      If _MailCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _MailCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _MailCol.Count) 
      End If 
    Else 
      _MailCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _MailCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlMailCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Mail" 
    
    Dim pMailID As Long = _MailID 
    
    pFault = _ctlMailCol.LoadControl(_MailCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlMailCol.Visible = True 
    
    _ctlMailCol.Refresh() 
    If pMailID <> 0 Then 
      Dim pMailCol As csMailCol = CType(_ctlMailCol.bsCtlMail.DataSource, csMailCol) 
      Dim pMail As csMail = pMailCol.FindByID(pMailID) 
      If pMail.ID > 0 Then 
        _ctlMailCol.bsCtlMail.CurrencyManager.Position = pMailCol.IndexOf(pMail) 
        _ctlMailCol.dgvMail.Rows(pMailCol.IndexOf(pMail)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlMail() As clsFault 
    Dim pFault As New clsFault 
    
    If _MailID > 0 Then 
      ChooseMail() 
      _Mail = New csMail() 
      pFault = _Mail.GetByID(_MailID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _Mail = New csMail() 
    End If 
    'lblSecondaryTitle.Text = _Mail.ID.ToString("#,##0")
    
     
    Dim pLoadParameters As New ctlc_Mail.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlMail(pLoadParameters)
    pFault = _ctlMail.LoadControl(_Mail, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlMail.Visible = True 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlMail_evtDeleted(ByVal vMailID As Long) Handles _ctlMail.evtDeleted 
    _MailCol = Nothing 
    Dim pFault As clsFault 
    _MailID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboMails(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlMail() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    Else  
      lnk_Click(lnkMailCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlMail_evtCancelledEdit(ByVal vMail As csMail) Handles _ctlMail.evtCancelledEdit 
    RefreshCtlMail() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboMails(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      If _MailID = 0 OrElse _MailID = -2 Then 
        pnlMail.Visible = False 
      Else 
        pnlMail.Visible = True 
      End If 
    End If 
  End Sub 
  Private Sub _ctlMail_evtUpdated(ByVal vWhichProperty As csMail.enmUpdateType, ByVal vMail As csMail) Handles _ctlMail.evtUpdated 
    _MailCol = Nothing 
    Dim pFault As clsFault 
    _MailID = CType(vMail, csMail).ID 
    If _ActiveControl.Name = "ctlc_Mail" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboMails(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlMail() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
    End If  
  End Sub 
  Private Function LoadCboMails(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.UD 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboMail(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
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
 
    If _MailID >= 0 Then 
      MyIntelliCombo.ValueSelect(_MailID) 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _MailID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _MailID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetMailIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _MailID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _MailID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _MailID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _MailID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseMail() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_Mail", StringComparison.OrdinalIgnoreCase) AndAlso _MailID > 0 Then 
        'to avoid getting ObjectNotFound 
        _Mail = New csMail() 
        pFault = _Mail.GetByID(_MailID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_Mail") 
    End If 
    pnlMail.Visible = True 
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
    pnlMail.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkMailCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _MailID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_MailCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkMailCol.Visible = False 
      _ActiveControl = _ctlMail 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboMails(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _MailID <> 0 Then 
        MyIntelliCombo.cbo.Text = _MailID.ToString() 
        pFault = ActivateControl("ctlc_Mail") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      Else 
        MyIntelliCombo.ValueClear() 
        pnlMail.Visible = False 
        _MailID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _MailID > 0 Then pnlMail.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkMailCol.MouseEnter, 
                  lnkMail.MouseEnter, 
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
                  lnkMailCol.MouseLeave, 
                  lnkMail.MouseLeave, 
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
    Dim pMessagingMode As clsEnums.enmMessagingMode = Nothing 
    Dim pRecipientEmail As String = Nothing 
    Dim pRecipientEmailWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pWasSeen As Nullable(Of Boolean) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByMessagingMode As Boolean = False 
    Dim pGroupByRecipientEmail As Boolean = False 
    Dim pGroupByWasSeen As Boolean = False 
    
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Mails"  
  
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
        .Combo01Label.Text = If(_ctlMailCol.LoadParameters.ColumnsHeaderText.ContainsKey(csMail.enmProperty.MessagingMode), _ctlMailCol.LoadParameters.ColumnsHeaderText(csMail.enmProperty.MessagingMode), "Messaging Mode") 
        Dim pMessagingModes As New clsComboList 
        pFault = pMessagingModes.FillEnums(clsEnums.enmEnum.MessagingMode, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pMessagingModes.Remove(pMessagingModes.FindByKey(clsEnums.enmMessagingMode.UD)) 
        pMessagingModes.SortByText() 
        If pMessagingModes IsNot Nothing AndAlso pMessagingModes.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo01Label) 
          .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo01 
          .MakeSmart() 
          .LoadControl(pMessagingModes, GetChoose(_Requester)) 
          .TabIndex = 3 
        End With 
 
        .String01Label.Text = If(_ctlMailCol.LoadParameters.ColumnsHeaderText.ContainsKey(csMail.enmProperty.RecipientEmail), _ctlMailCol.LoadParameters.ColumnsHeaderText(csMail.enmProperty.RecipientEmail), "Recipient Email") 
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
 
        .Check01Label.Text = If(_ctlMailCol.LoadParameters.ColumnsHeaderText.ContainsKey(csMail.enmProperty.WasSeen), _ctlMailCol.LoadParameters.ColumnsHeaderText(csMail.enmProperty.WasSeen), "Was Seen") 
        .Check01.CheckState = CheckState.Indeterminate 
        .Check01.TabIndex = 6 
        .flpFilter.Controls.Add(.Check01Label) 
        .flpFilter.Controls.Add(.Check01) 
 
        .Text01Label.Text = If(_ctlMailCol.LoadParameters.ColumnsHeaderText.ContainsKey(csMail.enmProperty.ID), _ctlMailCol.LoadParameters.ColumnsHeaderText(csMail.enmProperty.ID), "ID") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 7 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 8 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlMailCol.LoadParameters.ColumnsHeaderText.ContainsKey(csMail.enmProperty.MessagingMode), _ctlMailCol.LoadParameters.ColumnsHeaderText(csMail.enmProperty.MessagingMode), "Messaging Mode") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 9 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlMailCol.LoadParameters.ColumnsHeaderText.ContainsKey(csMail.enmProperty.RecipientEmail), _ctlMailCol.LoadParameters.ColumnsHeaderText(csMail.enmProperty.RecipientEmail), "Recipient Email") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 10 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlMailCol.LoadParameters.ColumnsHeaderText.ContainsKey(csMail.enmProperty.WasSeen), _ctlMailCol.LoadParameters.ColumnsHeaderText(csMail.enmProperty.WasSeen), "Was Seen") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 11 
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
        pMessagingMode = CType(CType(.Combo01.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmMessagingMode) 
        _SearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.MessagingMode, pMessagingMode) 
      End If 
      If .String01Text.Text <> "" Then 
        pRecipientEmail = .String01Text.Text 
        pRecipientEmailWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.RecipientEmail, pRecipientEmail) 
        _SearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.RecipientEmailWildcardType, pRecipientEmailWildcardType) 
      End If 
      If .Check01.CheckState <> CheckState.Indeterminate Then 
        pWasSeen = .Check01.Checked 
        _SearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.WasSeen, pWasSeen) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csMailCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csMailCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csMailCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByMessagingMode = True 
        pDoSum = True 
        _SearchFilters.Add(csMailCol.enmFillSumOnTheFlyParameters.GroupByMessagingMode, pGroupByMessagingMode) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByRecipientEmail = True 
        pDoSum = True 
        _SearchFilters.Add(csMailCol.enmFillSumOnTheFlyParameters.GroupByRecipientEmail, pGroupByRecipientEmail) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByWasSeen = True 
        pDoSum = True 
        _SearchFilters.Add(csMailCol.enmFillSumOnTheFlyParameters.GroupByWasSeen, pGroupByWasSeen) 
      End If 
    
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csMailCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csMailCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csMailCol.enmListDefinition.Dir) Then _SearchFilters.Add(csMailCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_MailCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_MailCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csMail.enmProperty.ID, "ID") 
      End With 
      _MailCol = New csMailCol() 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _MailCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        pFault = _MailCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _MailCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _MailCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _MailCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see Mail" 
      RaiseEvent evtOverrideLoadCtlMailCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _MailCol = New csMailCol() 
      pFault = _MailCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_MailCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _MailCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csMail.enmProperty.ID, "Count") 
        If pGroupByMessagingMode = False Then .ColumnsHide.Add(csMail.enmProperty.MessagingMode) 
        If pGroupByRecipientEmail = False Then .ColumnsHide.Add(csMail.enmProperty.RecipientEmail) 
        If pGroupByWasSeen = False Then .ColumnsHide.Add(csMail.enmProperty.WasSeen) 
        .ColumnsHide.Add(csMail.enmProperty.WhenSent) 
        .ColumnsHide.Add(csMail.enmProperty.Subject) 
        .ColumnsHide.Add(csMail.enmProperty.Body) 
        .ColumnsHide.Add(csMail.enmProperty.WhenSeen) 
        .ColumnsHide.Add(csMail.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlMailCol.Visible = True 
    pFault = _ctlMailCol.LoadControl(_MailCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csMailCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csMailCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlMail.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlMail.Controls(0).Name) 
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
 
  Private Sub _ctlMailCol_evtTimerTripped() Handles _ctlMailCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtMailTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlMailCol.MailCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlMailCol.MailCol(0).ID 
 
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
    If _MailCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csMail() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csMailCol = CType(CallByName(_MailCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csMailCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csMailCol = CType(CallByName(_MailCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csMailCol) 
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
                  ccHelper.GetPropertyTypeName(New csMailCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csMailCol = CType(CallByName(_MailCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csMailCol) 
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
 
  Private Sub cc_ctlPnlMail_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
  'Mail Specific 
  Private Sub ctlPnlc_Mail_ccevtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    chkGrid.Checked = True 
    chkGrid.Visible = False 
    'btnFilter.Parent.Controls.Remove(btnFilter) 
    btnFilter.Text = "Delete All" 
  End Sub 
 
  Private _RecipentEmail As String 
  Private _RecipentSMS As String 
 
  Private Sub ctlPnlc_Mail_evtOverrideFillMailCol(ByRef rMailCol As csMailCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) Handles Me.evtOverrideFillMailCol 
    Dim pFault As clsFault = Nothing 
 
    rMailCol = New csMailCol() 
 
    If String.IsNullOrEmpty(_RecipentEmail) Then 
      'get the user email & SMS 
      Dim pUser As New csUser(_Requester.UserID, clsEnums.enmLoadParent.DoNotLoad, _Requester, pFault, True) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
      _RecipentEmail = pUser.Email 
      pFault = ccHelper.CreateInternationalPhoneNumber(pUser.PhoneNumber, _RecipentSMS, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    End If 
 
    Dim pHowmany As Integer = 50 
    Dim pTmpCol As New csMailCol() 
    pFault = rMailCol.FillByMessagingModeAndRecipientEmailAndWasSeen(clsEnums.enmMessagingMode.Email, _RecipentEmail, vWasSeen:=False, vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    If rMailCol.Count < 50 Then 
      pFault = pTmpCol.FillByMessagingModeAndRecipientEmailAndWasSeen(clsEnums.enmMessagingMode.Email, _RecipentEmail, vWasSeen:=True, vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
      rMailCol.AddRange(pTmpCol) 
    End If 
    pTmpCol = New csMailCol() 
    pFault = pTmpCol.FillByMessagingModeAndRecipientEmailAndWasSeen(clsEnums.enmMessagingMode.SMS, _RecipentSMS, vWasSeen:=False, vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    rMailCol.AddRange(pTmpCol) 
    If pTmpCol.Count < 50 Then 
      pFault = pTmpCol.FillByMessagingModeAndRecipientEmailAndWasSeen(clsEnums.enmMessagingMode.SMS, _RecipentSMS, vWasSeen:=True, vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
      rMailCol.AddRange(pTmpCol) 
    End If 
    rMailCol.SortByID() 
    rMailCol.Reverse() 
 
    'mark them as seen (use a clone)  
    Dim pMailCol As csMailCol = rMailCol.CloneByWasSeen(False) 
    For Each l In pMailCol 
      l.WasSeen = True 
    Next 
    pFault = pMailCol.UpdateFromCollection(_Requester, False) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
 
  End Sub 
 
  'Mail specific 
  Private Sub ctlPnlc_Mail_evtOverrideLoadCtlMailCol(ByRef rLoadParameters As ctlc_MailCol.clsLoadParameters) Handles Me.evtOverrideLoadCtlMailCol 
    rLoadParameters.ReadOnly = True 
 
    'rLoadParameters.ColumnsHide.Add(csMail.enmProperty.WhenSeen) 
    rLoadParameters.ColumnsHeaderText.Add(csMail.enmProperty.WhenSeen, "When Received or Seen") 
    rLoadParameters.ColumnsHide.Add(csMail.enmProperty.ID) 
    rLoadParameters.ColumnsHide.Add(csMail.enmProperty.RecipientEmail) 
    rLoadParameters.ColumnsHide.Add(csMail.enmProperty.Body) 
  End Sub 
 
  Private Sub ctlPnlc_Mail_evtOverrideLoadCtlMail(ByRef rLoadParameters As ctlc_Mail.clsLoadParameters) Handles Me.evtOverrideLoadCtlMail 
    rLoadParameters.ReadOnly = False 
  End Sub 
 
  Private Sub ctlPnlc_Mail_evtOverrideFilterButton(ByRef rOverridden As Boolean) Handles Me.evtOverrideFilterButton 
    rOverridden = True 
 
    Dim pFault As clsFault = Nothing 
 
    Cursor = Cursors.WaitCursor  
 
    pFault = csMailCol.DeleteByMessagingModeAndRecipientEmail(clsEnums.enmMessagingMode.Email, _RecipentEmail, vRequester:=_Requester) 
    If Not pFault.isOK() Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
      Return 
    End If 
    pFault = csMailCol.DeleteByMessagingModeAndRecipientEmail(clsEnums.enmMessagingMode.SMS, _RecipentSMS, vRequester:=_Requester) 
    If Not pFault.isOK() Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
      Return 
    End If 
 
    btnRefresh_Click(New Object, New EventArgs) 
 
    Cursor = Cursors.Default 
  End Sub 
 
  Private Sub _ctlMail_evtLoggedAlertChosen(vLoggedAlertID As Long) Handles _ctlMail.evtLoggedAlertChosen 
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vLoggedAlertID 
      .Object = New csLoggedAlert 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
    Catch ex As Exception 
      MsgBox(ex.Message) 
    End Try 
  End Sub 
  
End Class 
