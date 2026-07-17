Public Class ctlPnlc_User 
 
  Private _Requester As clsRequester  
  
  'The Controls 
  Private WithEvents _ctlUserCol As ctlc_UserCol 
  Private WithEvents _ctlUser As ctlc_User 
  Private WithEvents _ctlJobAlertRecipientCol As ctlc_JobAlertRecipientCol 
  Private WithEvents _ctlLoggedAlertsForAffectedUserCol As ctlc_LoggedAlertCol 
  Private WithEvents _ctlLoggedRequestCol As ctlc_LoggedRequestCol 
  Private WithEvents _ctlMFA As ctlc_MFA 
  Private WithEvents _ctlUserLoginKeyCol As ctlc_UserLoginKeyCol 
  Private WithEvents _ctlUserPermissionCol As ctlc_UserPermissionCol 
  Private WithEvents _ctlUserStatusCol As ctlc_UserStatusCol 
  
  'Search Form 
  Private _frmSearch As frmFilter 
  Private _SearchFilters As New Dictionary(Of System.Enum, Object) 
  
  Private _ActiveControl As Control 
  
  'The master 
  Private _UserID As Long 
 
  'The data holders 
  Private _UserCol As csUserCol 
  Private _User As csUser 
  Private _JobAlertRecipientCol As csJobAlertRecipientCol 
  Private _LoggedAlertsForAffectedUserCol As csLoggedAlertCol 
  Private _LoggedRequestCol As csLoggedRequestCol 
  Private _MFA As csMFA 
  Private _UserLoginKeyCol As csUserLoginKeyCol 
  Private _UserPermissionCol As csUserPermissionCol 
  Private _UserStatusCol As csUserStatusCol 
 
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
  Public Event evtOverrideLoadCboUser(ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean, ByRef rAddNewPrompt As String) 
  Private Event evtGetUserIDFromIntelliComboText(ByVal vIntelliComboText As String) 
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
  Public Event evtOverrideFillUserCol(ByRef rUserCol As csUserCol, ByRef rGridTitle As String, ByRef rHowMany As Integer) 
  Public Event evtOverrideFillJobAlertRecipientCol(ByRef rJobAlertRecipientCol As csJobAlertRecipientCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillLoggedAlertsForAffectedUserCol(ByRef rLoggedAlertsForAffectedUserCol As csLoggedAlertCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillLoggedRequestCol(ByRef rLoggedRequestCol As csLoggedRequestCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillUserLoginKeyCol(ByRef rUserLoginKeyCol As csUserLoginKeyCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillUserPermissionCol(ByRef rUserPermissionCol As csUserPermissionCol, ByRef rGridTitle As String) 
  Public Event evtOverrideFillUserStatusCol(ByRef rUserStatusCol As csUserStatusCol, ByRef rGridTitle As String) 
  
  'Override LoadControls
  Private Event evtOverrideLoadCtlUserCol(ByRef rLoadParameters As ctlc_UserCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlUser(ByRef rLoadParameters As ctlc_User.clsLoadParameters) 
  Private Event evtOverrideLoadCtlJobAlertRecipientCol(ByRef rLoadParameters As ctlc_JobAlertRecipientCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedAlertsForAffectedUserCol(ByRef rLoadParameters As ctlc_LoggedAlertCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlLoggedRequestCol(ByRef rLoadParameters As ctlc_LoggedRequestCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlMFA(ByRef rLoadParameters As ctlc_MFA.clsLoadParameters) 
  Private Event evtOverrideLoadCtlUserLoginKeyCol(ByRef rLoadParameters As ctlc_UserLoginKeyCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlUserPermissionCol(ByRef rLoadParameters As ctlc_UserPermissionCol.clsLoadParameters) 
  Private Event evtOverrideLoadCtlUserStatusCol(ByRef rLoadParameters As ctlc_UserStatusCol.clsLoadParameters) 
  
  'Set IgnoreDoubleclicks 
  Private Event evtIgnoreUserCol_evtRowDoubleClicked(ByRef rIgnore As Boolean) 
  
  'Override Filter Form 
  Public Event evtOverrideSearchForm() 
  Public Event evtOverrideFilterButton(ByRef rOverridden As Boolean) 
  
  'PassChosenEntity
  Public Event evtEntityChosen(ByVal sender As Object, ByVal e As EntityEventArgs)
  
  'Primary Timer
  Private Event evtUserTimerTripped(ByRef rCancel As Boolean) 
  
  'Back button 
  Public Event evtBackClicked(ByVal sender As Object, ByVal e As PanelEventArgs) 
  Private _NestedInMain As Boolean 
  Private _NestedFormsCount As Integer 
 
  'Children
  Private _CancelEvtJobAlertRecipientChosen As Boolean = False 
  Private _ShowPopForEvtJobAlertRecipientChosen As Boolean = False 
  Private _CancelEvtLoggedAlertsForAffectedUserChosen As Boolean = False 
  Private _ShowPopForEvtLoggedAlertsForAffectedUserChosen As Boolean = False 
  Private _CancelEvtLoggedRequestChosen As Boolean = False 
  Private _ShowPopForEvtLoggedRequestChosen As Boolean = False 
  Private _CancelEvtUserLoginKeyChosen As Boolean = False 
  Private _ShowPopForEvtUserLoginKeyChosen As Boolean = False 
  Private _CancelEvtUserPermissionChosen As Boolean = False 
  Private _ShowPopForEvtUserPermissionChosen As Boolean = False 
  Private _CancelEvtUserStatusChosen As Boolean = False 
  Private _ShowPopForEvtUserStatusChosen As Boolean = False 
  'Parents
  Private _CancelEvtRoleChosen As Boolean = False 
  Private _ShowPopForEvtRoleChosen As Boolean = False 
  
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
 
    lnkUserCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkJobAlertRecipientCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedAlertsForAffectedUserCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkLoggedRequestCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkMFA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkUserLoginKeyCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkUserPermissionCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
    lnkUserStatusCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D 
 
    _Tooltip = New ToolTip 
    _Tooltip.SetToolTip(btnRefresh, "Click to refresh window. Double-Click to refresh the list in the combobox") 
 
    lblBack.Visible = False 
    chkGrid.Enabled = True 
 
  End Sub 
 
  Public Function LoadControl(ByVal vUserID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    _Requester = vRequester 
    _UserID = CType(vUserID, Long) 
 
    LoadLocalizedText() 
 
    RaiseEvent evtBeforeLoad() 
 
    _NestedFormsCount = 0 
 
    pnlUser.Visible = False 
 
    ' "Create and load the controls" moved to "activate controls", so that it loads them only as needed
    pnlCover.BringToFront() 
    
    lnkUserCol.Visible = False 
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
      pFault = LoadCboUsers(False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _UserID > 0 Then 
      If Not MyIntelliCombo.IsDumb Then 
        MyIntelliCombo.ValueClear() 
        MyIntelliCombo.ValueSelect(_UserID) 
      End If 
      ChooseUser() 
      MyIntelliCombo.Visible = False 
      lblSecondaryTitle.Visible = True 
      chkGrid.Visible = False 
      pFault = ActivateControl("ctlc_User") 
      If pFault.isOK = False Then Return pFault 
 
      lblBack.Visible = True 
      chkGrid.Enabled = False 
      _NestedInMain = True 
      pnlCover.SendToBack() 
    ElseIf _UserID = -1 AndAlso MyIntelliCombo.cbo.Items.Count = 1 Then 
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
    
    If pControlName = "ctlc_User" OrElse pControlName = "ctlUser" Then 
      lnkUser.ForeColor = Color.Black : lnkUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUser.BackColor = Color.Wheat 
      If _ctlUser Is Nothing Then 
        _ctlUser = New ctlc_User() 
        _ctlUser.Dock = DockStyle.Fill 
        _ctlUser.Controls.RemoveByKey("btnAdd") 
        pnlUser.Controls.Add(_ctlUser) 
        _ctlUser.Visible = False 
      End If 
      If _UserID = 0 Then 
        pnlUser.Visible = False 
      End If 
      'If _User Is Nothing Then 
      pFault = RefreshCtlUser() 
      If pFault.isOK = False Then Return pFault 
      'End If 
      If _ctlUser.User.IsEmpty AndAlso _UserID <> -2 Then 
        pnlUser.Visible = False 
      End If 
      _ctlUser.Name = "ctlc_User" 
      _ActiveControl = _ctlUser 
      _ctlUser.BringToFront() 
      _ctlUser.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_UserCol" Then 
      lnkUserCol.ForeColor = Color.Black : lnkUserCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserCol.BackColor = Color.Wheat 
      If _ctlUserCol Is Nothing Then 
        _ctlUserCol = New ctlc_UserCol() 
        _ctlUserCol.Dock = DockStyle.Fill 
        pnlUser.Controls.Add(_ctlUserCol) 
        _ctlUserCol.Visible = False 
      End If  
      pnlUser.Visible = True 
      If _UserCol Is Nothing Then 
        pFault = RefreshCtlUserCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlUserCol.Name = "ctlc_UserCol" 
      _ActiveControl = _ctlUserCol 
      _ctlUserCol.BringToFront() 
      _ctlUserCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = False 
        btnFilter.Visible = True 
      End If 
    ElseIf pControlName = "ctlc_JobAlertRecipientCol" Then 
      lnkJobAlertRecipientCol.ForeColor = Color.Black : lnkJobAlertRecipientCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkJobAlertRecipientCol.BackColor = Color.Wheat 
      If _ctlJobAlertRecipientCol Is Nothing Then 
      _ctlJobAlertRecipientCol = New ctlc_JobAlertRecipientCol() 
      _ctlJobAlertRecipientCol.Dock = DockStyle.Fill 
      pnlUser.Controls.Add(_ctlJobAlertRecipientCol) 
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
    ElseIf pControlName = "ctlc_LoggedAlertsForAffectedUserCol" Then 
      lnkLoggedAlertsForAffectedUserCol.ForeColor = Color.Black : lnkLoggedAlertsForAffectedUserCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedAlertsForAffectedUserCol.BackColor = Color.Wheat 
      If _ctlLoggedAlertsForAffectedUserCol Is Nothing Then 
      _ctlLoggedAlertsForAffectedUserCol = New ctlc_LoggedAlertCol() 
      _ctlLoggedAlertsForAffectedUserCol.Dock = DockStyle.Fill 
      pnlUser.Controls.Add(_ctlLoggedAlertsForAffectedUserCol) 
      _ctlLoggedAlertsForAffectedUserCol.Visible = False 
      End If  
      If _LoggedAlertsForAffectedUserCol Is Nothing Then 
        pFault = RefreshCtlLoggedAlertsForAffectedUserCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedAlertsForAffectedUserCol.Name = "ctlc_LoggedAlertsForAffectedUserCol" 
      _ActiveControl = _ctlLoggedAlertsForAffectedUserCol 
      _ctlLoggedAlertsForAffectedUserCol.BringToFront() 
      _ctlLoggedAlertsForAffectedUserCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_LoggedRequestCol" Then 
      lnkLoggedRequestCol.ForeColor = Color.Black : lnkLoggedRequestCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkLoggedRequestCol.BackColor = Color.Wheat 
      If _ctlLoggedRequestCol Is Nothing Then 
      _ctlLoggedRequestCol = New ctlc_LoggedRequestCol() 
      _ctlLoggedRequestCol.Dock = DockStyle.Fill 
      pnlUser.Controls.Add(_ctlLoggedRequestCol) 
      _ctlLoggedRequestCol.Visible = False 
      End If  
      If _LoggedRequestCol Is Nothing Then 
        pFault = RefreshCtlLoggedRequestCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlLoggedRequestCol.Name = "ctlc_LoggedRequestCol" 
      _ActiveControl = _ctlLoggedRequestCol 
      _ctlLoggedRequestCol.BringToFront() 
      _ctlLoggedRequestCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_MFA" Then 
      lnkMFA.ForeColor = Color.Black : lnkMFA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkMFA.BackColor = Color.Wheat 
      If _ctlMFA Is Nothing Then 
      pnlUser.Visible = False 
        _ctlMFA = New ctlc_MFA() 
        _ctlMFA.Dock = DockStyle.Fill 
        pnlUser.Controls.Add(_ctlMFA) 
        _ctlMFA.Visible = False 
      pnlUser.Visible = True 
      End If 
      If _MFA Is Nothing Then 
        pFault = RefreshCtlMFA() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlMFA.Name = "ctlc_MFA" 
      _ActiveControl = _ctlMFA 
      _ctlMFA.BringToFront() 
      _ctlMFA.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_UserLoginKeyCol" Then 
      lnkUserLoginKeyCol.ForeColor = Color.Black : lnkUserLoginKeyCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserLoginKeyCol.BackColor = Color.Wheat 
      If _ctlUserLoginKeyCol Is Nothing Then 
      _ctlUserLoginKeyCol = New ctlc_UserLoginKeyCol() 
      _ctlUserLoginKeyCol.Dock = DockStyle.Fill 
      pnlUser.Controls.Add(_ctlUserLoginKeyCol) 
      _ctlUserLoginKeyCol.Visible = False 
      End If  
      If _UserLoginKeyCol Is Nothing Then 
        pFault = RefreshCtlUserLoginKeyCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlUserLoginKeyCol.Name = "ctlc_UserLoginKeyCol" 
      _ActiveControl = _ctlUserLoginKeyCol 
      _ctlUserLoginKeyCol.BringToFront() 
      _ctlUserLoginKeyCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_UserPermissionCol" Then 
      lnkUserPermissionCol.ForeColor = Color.Black : lnkUserPermissionCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserPermissionCol.BackColor = Color.Wheat 
      If _ctlUserPermissionCol Is Nothing Then 
      _ctlUserPermissionCol = New ctlc_UserPermissionCol() 
      _ctlUserPermissionCol.Dock = DockStyle.Fill 
      pnlUser.Controls.Add(_ctlUserPermissionCol) 
      _ctlUserPermissionCol.Visible = False 
      End If  
      If _UserPermissionCol Is Nothing Then 
        pFault = RefreshCtlUserPermissionCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlUserPermissionCol.Name = "ctlc_UserPermissionCol" 
      _ActiveControl = _ctlUserPermissionCol 
      _ctlUserPermissionCol.BringToFront() 
      _ctlUserPermissionCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    ElseIf pControlName = "ctlc_UserStatusCol" Then 
      lnkUserStatusCol.ForeColor = Color.Black : lnkUserStatusCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D : lnkUserStatusCol.BackColor = Color.Wheat 
      If _ctlUserStatusCol Is Nothing Then 
      _ctlUserStatusCol = New ctlc_UserStatusCol() 
      _ctlUserStatusCol.Dock = DockStyle.Fill 
      pnlUser.Controls.Add(_ctlUserStatusCol) 
      _ctlUserStatusCol.Visible = False 
      End If  
      If _UserStatusCol Is Nothing Then 
        pFault = RefreshCtlUserStatusCol() 
        If pFault.isOK = False Then Return pFault 
      End If 
      _ctlUserStatusCol.Name = "ctlc_UserStatusCol" 
      _ActiveControl = _ctlUserStatusCol 
      _ctlUserStatusCol.BringToFront() 
      _ctlUserStatusCol.Focus() 
      If pShowGrid = True Then 
        lblSecondaryTitle.Visible = True 
        btnFilter.Visible = False 
      End If 
    Else 
      Return pFault.LogFreeTextFault(1, "Invalid object:" & pControlName, "", "TRGT-User-091229-1815", _Requester) 
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
 
    lblTitle.Text = TableNameTranslate("User", _Requester) 
 
    lnkUserCol.Text = CCTextTranslate("List", _Requester) 
    lnkUser.Text = CCTextTranslate("Details", _Requester) 
 
    lnkJobAlertRecipientCol.Text = TableNameTranslate("JobAlertRecipient", _Requester, vMakePlural:=True) 
    lnkLoggedAlertsForAffectedUserCol.Text = TableNameTranslate("LoggedAlertForAffectedUser", _Requester, vMakePlural:=True) 
    lnkLoggedRequestCol.Text = TableNameTranslate("LoggedRequest", _Requester, vMakePlural:=True) 
    lnkMFA.Text = TableNameTranslate("MFA", _Requester) 
    lnkUserLoginKeyCol.Text = TableNameTranslate("UserLoginKey", _Requester, vMakePlural:=True) 
    lnkUserPermissionCol.Text = TableNameTranslate("UserPermission", _Requester, vMakePlural:=True) 
    lnkUserStatusCol.Text = TableNameTranslate("UserStatus", _Requester, vMakePlural:=True) 
 
  End Sub 
 
  'Handle Controls 
  Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
    If MyIntelliCombo.Visible = False Then btnRefresh.Enabled = False 
 
    'Don't do anything if nothing to refresh!
    If pnlUser.Controls(0) Is _ctlUser Then 
      If _UserID = 0 Then 
        pnlUser.Visible = False 
      End If 
    ElseIf pnlUser.Controls(0) Is _ctlUserCol Then 
    ElseIf pnlUser.Controls(0) Is _ctlJobAlertRecipientCol Then 
    ElseIf pnlUser.Controls(0) Is _ctlLoggedAlertsForAffectedUserCol Then 
    ElseIf pnlUser.Controls(0) Is _ctlLoggedRequestCol Then 
    ElseIf pnlUser.Controls(0) Is _ctlMFA Then 
    ElseIf pnlUser.Controls(0) Is _ctlUserLoginKeyCol Then 
    ElseIf pnlUser.Controls(0) Is _ctlUserPermissionCol Then 
    ElseIf pnlUser.Controls(0) Is _ctlUserStatusCol Then 
    Else 
      Cursor = Cursors.Default 
      btnRefresh.Enabled = True 
      Exit Sub 
    End If 
 
 
    If MyIntelliCombo.IsDumb = True AndAlso MyIntelliCombo.DropDownStyle <> ComboBoxStyle.DropDownList Then 
      Dim pText As String = MyIntelliCombo.Text 
      Dim pUserID As Long = _UserID 
      If ccHelper.IsNumeric(pText) Then _UserID = ccHelper.ToLong(pText) 
      RaiseEvent evtGetUserIDFromIntelliComboText(pText) 
      If pUserID <> _UserID Then 
        _User = Nothing 
        pFault = ActivateControl("ctlc_User") 
        Cursor = Cursors.Default 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        btnRefresh.Enabled = True 
        Exit Sub 
      End If 
    End If 
 
    'refresh the active screen 
    If pnlUser.Controls(0) Is _ctlUser Then 
      pFault = RefreshCtlUser() 
    ElseIf pnlUser.Controls(0) Is _ctlUserCol Then 
      pFault = RefreshCtlUserCol() 
    ElseIf pnlUser.Controls(0) Is _ctlJobAlertRecipientCol Then 
      pFault = RefreshCtlJobAlertRecipientCol() 
    ElseIf pnlUser.Controls(0) Is _ctlLoggedAlertsForAffectedUserCol Then 
      pFault = RefreshCtlLoggedAlertsForAffectedUserCol() 
    ElseIf pnlUser.Controls(0) Is _ctlLoggedRequestCol Then 
      pFault = RefreshCtlLoggedRequestCol() 
    ElseIf pnlUser.Controls(0) Is _ctlMFA Then 
      pFault = RefreshCtlMFA() 
    ElseIf pnlUser.Controls(0) Is _ctlUserLoginKeyCol Then 
      pFault = RefreshCtlUserLoginKeyCol() 
    ElseIf pnlUser.Controls(0) Is _ctlUserPermissionCol Then 
      pFault = RefreshCtlUserPermissionCol() 
    ElseIf pnlUser.Controls(0) Is _ctlUserStatusCol Then 
      pFault = RefreshCtlUserStatusCol() 
    Else 
      Cursor = Cursors.Default 
      pFault.LogFreeTextFault(1, "Can't refresh - no control found:" & pnlUser.Controls(0).Name, "", "TRGT-User-091229-1810", _Requester) 
    End If 
 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    btnRefresh.Enabled = True 
  End Sub 
  Private Sub btnRefresh_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRefresh.MouseDown 
    If MyIntelliCombo.Visible = False Then Exit Sub 
    If e.Clicks = 2 Then 
      Dim pFault As clsFault = LoadCboUsers(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    End If 
  End Sub 
 
  Private Sub _ctlUserCol_evtRowClicked(ByVal vUser As Object) Handles _ctlUserCol.evtRowClicked 
    
    If vUser Is Nothing Then Exit Sub 
    
    Cursor = Cursors.WaitCursor 
    Dim pUser As csUser = CType(vUser, csUser) 
    _UserID = pUser.ID 
    
    If _ActiveControl Is _ctlUserCol Then 
      Dim pInGroupBy As Boolean = False 
      For Each l In _SearchFilters 
        If l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByLastName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByFirstName.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByCity.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByType.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByIDinType.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByRoleID.ToString() Then 
          pInGroupBy = True 
          Exit For 
        ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByLastSuccessfulLogin.ToString() Then 
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
    
    ChooseUser() 
    
    Try 
      MyIntelliCombo.ValueSelect(_UserID) 
    Catch ex As Exception 
      'none found 
    End Try 
    
    'lblSecondaryTitle.Text = pUser.FirstName & " " & pUser.LastName & " " & pUser.UserName
 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub ChooseUser() 
    _User = Nothing 
    lnkUser.Visible = True 
    _JobAlertRecipientCol = Nothing 
    lnkJobAlertRecipientCol.Visible = True 
    _LoggedAlertsForAffectedUserCol = Nothing 
    lnkLoggedAlertsForAffectedUserCol.Visible = True 
    _LoggedRequestCol = Nothing 
    lnkLoggedRequestCol.Visible = True 
    _MFA = Nothing 
    lnkMFA.Visible = True 
    _UserLoginKeyCol = Nothing 
    lnkUserLoginKeyCol.Visible = True 
    _UserPermissionCol = Nothing 
    lnkUserPermissionCol.Visible = True 
    _UserStatusCol = Nothing 
    lnkUserStatusCol.Visible = True 
  End Sub 
  Private Sub _ctlUserCol_evtRowDoubleClicked(ByVal vUser As csUser, ByRef rHandled As Boolean) Handles _ctlUserCol.evtRowDoubleClicked 
    If lnkUser.Parent IsNot flpMenu Then Exit Sub 
    If vUser Is Nothing Then Exit Sub 
 
    Dim pFault As clsFault 
    Dim pSearchFilters As New Dictionary(Of System.Enum, Object) 
    
    Dim pInGroupBy As Boolean = False 
    For Each l In _SearchFilters 
      If l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByLastName.ToString() Then 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.LastName) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.LastName) 
        pSearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.LastName, vUser.LastName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByFirstName.ToString() Then 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.FirstName) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.FirstName) 
        pSearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.FirstName, vUser.FirstName) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByCity.ToString() Then 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.City) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.City) 
        pSearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.City, vUser.City) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByType.ToString() Then 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.Type) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.Type) 
        pSearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.Type, vUser.Type) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByIDinType.ToString() Then 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.IDinTypeFrom) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.IDinTypeFrom) 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.IDinTypeTo) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.IDinTypeTo) 
        pSearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.IDinTypeFrom, vUser.IDinType) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByRoleID.ToString() Then 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.RoleID) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.RoleID) 
        pSearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.RoleID, vUser.RoleID) 
        pInGroupBy = True 
      ElseIf l.Key.ToString() = csUserCol.enmFillSumOnTheFlyParameters.GroupByLastSuccessfulLogin.ToString() Then 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.LastSuccessfulLoginStart) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.LastSuccessfulLoginStart) 
        If pSearchFilters.ContainsKey(csUserCol.enmFillOnTheFlyParameters.LastSuccessfulLoginEnd) Then pSearchFilters.Remove(csUserCol.enmFillOnTheFlyParameters.LastSuccessfulLoginEnd) 
        pSearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.LastSuccessfulLoginStart, vUser.LastSuccessfulLogin) 
        pInGroupBy = True 
      Else 
        pSearchFilters.Add(l.Key, l.Value) 
      End If 
    Next 
    
    rHandled = True 
 
    Cursor = Cursors.WaitCursor 
 
    If pInGroupBy = False Then 
      Dim pIgnore As Boolean = False 
      RaiseEvent evtIgnoreUserCol_evtRowDoubleClicked(pIgnore) 
      If pIgnore = True Then Exit Sub 
 
      'Check if entity is already open in another tab BEFORE loading 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, vUser.ID, vUser.DefaultDesignation) = False Then 
          'Entity already open in another tab - abort 
          rHandled = True 
          Exit Sub 
        End If 
      End If 
 
      _UserID = vUser.ID 
      'MyIntelliCombo.ValueSelect(_UserID) 
      pFault = ActivateControl("ctlc_User") 
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
      pFault = _UserCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _UserCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserCol.Count) 
      End If 
 
      Dim pLoadParameters As New ctlc_UserCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csUser.enmProperty.ID, "ID") 
      End With 
 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see User" 
      pFault = _ctlUserCol.LoadControl(_UserCol, pLoadParameters, _Requester) 
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
  Private Sub _ctlUserCol_evtUnChosen() Handles _ctlUserCol.evtUnChosen 
 
    _UserID = 0 
    _User = Nothing 
    _JobAlertRecipientCol = Nothing 
    lnkJobAlertRecipientCol.Visible = False 
    _LoggedAlertsForAffectedUserCol = Nothing 
    lnkLoggedAlertsForAffectedUserCol.Visible = False 
    _LoggedRequestCol = Nothing 
    lnkLoggedRequestCol.Visible = False 
    _MFA = Nothing 
    lnkMFA.Visible = False 
    _UserLoginKeyCol = Nothing 
    lnkUserLoginKeyCol.Visible = False 
    _UserPermissionCol = Nothing 
    lnkUserPermissionCol.Visible = False 
    _UserStatusCol = Nothing 
    lnkUserStatusCol.Visible = False 
    lblSecondaryTitle.Text = "" 
    lnkUser.Visible = False 
 
  End Sub 
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _ 
      lnkJobAlertRecipientCol.Click, 
      lnkLoggedAlertsForAffectedUserCol.Click, 
      lnkLoggedRequestCol.Click, 
      lnkMFA.Click, 
      lnkUserLoginKeyCol.Click, 
      lnkUserPermissionCol.Click, 
      lnkUserStatusCol.Click, 
      lnkUserCol.Click, 
      lnkUser.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    Dim lnk As Label = CType(sender, Label) 
 
    pFault = ActivateControl(lnk.Tag.ToString) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    If lnk Is lnkUser OrElse (lnk Is lnkUserCol AndAlso chkGrid.Checked = True) Then 
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
  Private Function RefreshCtlUserCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pLoadParameters As New ctlc_UserCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .ReportTitle = "Summary List" 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHeaderText.Clear() 
      .SearchFilters = _SearchFilters 
      '.ColumnsHeaderText.Add(csUser.enmProperty.ID, "ID") 
    End With 
    
    Dim pTestCol As csUserCol = Nothing 
    Dim pHowmany As Integer = 100 
    RaiseEvent evtOverrideFillUserCol(pTestCol, pTitle, pHowmany) 
    If pTestCol Is Nothing Then 
      _UserCol = New csUserCol(clsEnums.enmLoadParent.TextOnly) 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _UserCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) 
        If pFault.isOK = False Then 
          _ctlUserCol.Timer?.Stop() 
          Return pFault 
        End If 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case Else 
            If _ctlUserCol.chkAutoRefresh.Checked Then pHowmany = 15 
            pFault = _UserCol.Fill(vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) 
            If pFault.isOK = False Then 
              _ctlUserCol.Timer?.Stop() 
              Return pFault 
            End If 
        End Select 
      End If 
 
      If _UserCol.Count = pHowmany Then 
        pTitle = $"Showing 1st {pHowmany - 1} rows" 
        _UserCol.RemoveAt(pHowmany - 1) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserCol.Count) 
      End If 
    Else 
      _UserCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _UserCol.Count) 
      End If 
    End If 
    RaiseEvent evtOverrideLoadCtlUserCol(pLoadParameters) 
    pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see User" 
    
    Dim pUserID As Long = _UserID 
    
    pFault = _ctlUserCol.LoadControl(_UserCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlUserCol.Visible = True 
    
    _ctlUserCol.Refresh() 
    If pUserID <> 0 Then 
      Dim pUserCol As csUserCol = CType(_ctlUserCol.bsCtlUser.DataSource, csUserCol) 
      Dim pUser As csUser = pUserCol.FindByID(pUserID) 
      If pUser.ID > 0 Then 
        _ctlUserCol.bsCtlUser.CurrencyManager.Position = pUserCol.IndexOf(pUser) 
        _ctlUserCol.dgvUser.Rows(pUserCol.IndexOf(pUser)).Selected = True 
      End If 
    End If 
    Return pFault 
  End Function 
  Private Function RefreshCtlUser() As clsFault 
    Dim pFault As New clsFault 
    
    If _UserID > 0 Then 
      ChooseUser() 
      _User = New csUser(clsEnums.enmLoadParent.TextOnly) 
      pFault = _User.GetByID(_UserID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _User = New csUser(clsEnums.enmLoadParent.TextOnly) 
    End If 
    'lblSecondaryTitle.Text = _User.FirstName & " " & _User.LastName & " " & _User.UserName    
     
    Dim pLoadParameters As New ctlc_User.clsLoadParameters() 
    With pLoadParameters
      .ReadOnly = False 
    End With 
    RaiseEvent evtOverrideLoadCtlUser(pLoadParameters)
    pFault = _ctlUser.LoadControl(_User, pLoadParameters, _Requester) : If pFault.isOK = False Then Return pFault 
    _ctlUser.Visible = True 
    If _UserID = -2 Then 
      lblSecondaryTitle.Text = "New" 
      _ctlUser.btnAdd.PerformClick() 
    Else 
      If _ShowIntelligentCombo = True Then 
        MyIntelliCombo.Visible = True 
        _ctlUser.btnAdd.Visible = False 
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
      pFault = _JobAlertRecipientCol.FillByUserID(_UserID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
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
      If _User IsNot Nothing AndAlso Not String.IsNullOrEmpty(_User.DefaultDesignation) Then 
        .ReportTitle = "List of JobAlertRecipients for " & _User.DefaultDesignation 
      Else 
        .ReportTitle = "List of JobAlertRecipients for User" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csJobAlertRecipient.enmProperty.User) 
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
  Private Function RefreshCtlLoggedAlertsForAffectedUserCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlLoggedAlertsForAffectedUserCol.dgvLoggedAlert.SelectedRows.Count > 0 Then 
      Dim pLoggedAlert As csLoggedAlert = CType(_ctlLoggedAlertsForAffectedUserCol.bsCtlLoggedAlert.Current, csLoggedAlert) 
      pID = pLoggedAlert.ID 
    End If 
 
    Dim pTestCol As csLoggedAlertCol = Nothing 
    RaiseEvent evtOverrideFillLoggedAlertsForAffectedUserCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _LoggedAlertsForAffectedUserCol = New csLoggedAlertCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedAlertsForAffectedUserCol.FillByAffectedUserID(_UserID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _LoggedAlertsForAffectedUserCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedAlertsForAffectedUserCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedAlertsForAffectedUserCol.Count) 
      End If 
    Else 
      _LoggedAlertsForAffectedUserCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedAlertsForAffectedUserCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_LoggedAlertCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _User IsNot Nothing AndAlso Not String.IsNullOrEmpty(_User.DefaultDesignation) Then 
        .ReportTitle = "List of LoggedAlerts for " & _User.DefaultDesignation 
      Else 
        .ReportTitle = "List of LoggedAlerts for User" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csLoggedAlert.enmProperty.AffectedUser) 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedAlertsForAffectedUserCol(pLoadParameters)
    
    pFault = _ctlLoggedAlertsForAffectedUserCol.LoadControl(_LoggedAlertsForAffectedUserCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedAlertsForAffectedUserCol.Visible = True 
 
    If pID > 0 Then 
      Dim pLoggedAlerts As csLoggedAlertCol = CType(_ctlLoggedAlertsForAffectedUserCol.bsCtlLoggedAlert.DataSource, csLoggedAlertCol) 
      Dim pLoggedAlert As csLoggedAlert = pLoggedAlerts.FindByID((pID)) 
      If pLoggedAlert.ID > 0 Then 
        _ctlLoggedAlertsForAffectedUserCol.bsCtlLoggedAlert.CurrencyManager.Position = pLoggedAlerts.IndexOf(pLoggedAlert) 
        _ctlLoggedAlertsForAffectedUserCol.dgvLoggedAlert.Rows(pLoggedAlerts.IndexOf(pLoggedAlert)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlLoggedRequestCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlLoggedRequestCol.dgvLoggedRequest.SelectedRows.Count > 0 Then 
      Dim pLoggedRequest As csLoggedRequest = CType(_ctlLoggedRequestCol.bsCtlLoggedRequest.Current, csLoggedRequest) 
      pID = pLoggedRequest.ID 
    End If 
 
    Dim pTestCol As csLoggedRequestCol = Nothing 
    RaiseEvent evtOverrideFillLoggedRequestCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _LoggedRequestCol = New csLoggedRequestCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _LoggedRequestCol.FillByUserID(_UserID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _LoggedRequestCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _LoggedRequestCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _LoggedRequestCol.Count) 
      End If 
    Else 
      _LoggedRequestCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _LoggedRequestCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_LoggedRequestCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _User IsNot Nothing AndAlso Not String.IsNullOrEmpty(_User.DefaultDesignation) Then 
        .ReportTitle = "List of LoggedRequests for " & _User.DefaultDesignation 
      Else 
        .ReportTitle = "List of LoggedRequests for User" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csLoggedRequest.enmProperty.User) 
    End With 
    RaiseEvent evtOverrideLoadCtlLoggedRequestCol(pLoadParameters)
    
    pFault = _ctlLoggedRequestCol.LoadControl(_LoggedRequestCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlLoggedRequestCol.Visible = True 
 
    If pID > 0 Then 
      Dim pLoggedRequests As csLoggedRequestCol = CType(_ctlLoggedRequestCol.bsCtlLoggedRequest.DataSource, csLoggedRequestCol) 
      Dim pLoggedRequest As csLoggedRequest = pLoggedRequests.FindByID((pID)) 
      If pLoggedRequest.ID > 0 Then 
        _ctlLoggedRequestCol.bsCtlLoggedRequest.CurrencyManager.Position = pLoggedRequests.IndexOf(pLoggedRequest) 
        _ctlLoggedRequestCol.dgvLoggedRequest.Rows(pLoggedRequests.IndexOf(pLoggedRequest)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlMFA() As clsFault 
    Dim pFault As New clsFault 
 
    If _UserID > 0 Then 
      _MFA = New csMFA(clsEnums.enmLoadParent.TextOnly) 
      pFault = _MFA.GetByUserID(_UserID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    Else 
      _MFA = New csMFA 
    End If 
    
    Dim pLoadParameters As New ctlc_MFA.clsLoadParameters() 
    pLoadParameters.ReadOnly = False 
    pFault = _ctlMFA.LoadControl(_MFA, pLoadParameters, _Requester) 
    _ctlMFA.Visible = True 
    Return pFault 
  End Function 
  Private Function RefreshCtlUserLoginKeyCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlUserLoginKeyCol.dgvUserLoginKey.SelectedRows.Count > 0 Then 
      Dim pUserLoginKey As csUserLoginKey = CType(_ctlUserLoginKeyCol.bsCtlUserLoginKey.Current, csUserLoginKey) 
      pID = pUserLoginKey.ID 
    End If 
 
    Dim pTestCol As csUserLoginKeyCol = Nothing 
    RaiseEvent evtOverrideFillUserLoginKeyCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _UserLoginKeyCol = New csUserLoginKeyCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserLoginKeyCol.FillByUserID(_UserID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _UserLoginKeyCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserLoginKeyCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserLoginKeyCol.Count) 
      End If 
    Else 
      _UserLoginKeyCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _UserLoginKeyCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_UserLoginKeyCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _User IsNot Nothing AndAlso Not String.IsNullOrEmpty(_User.DefaultDesignation) Then 
        .ReportTitle = "List of UserLoginKeys for " & _User.DefaultDesignation 
      Else 
        .ReportTitle = "List of UserLoginKeys for User" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csUserLoginKey.enmProperty.User) 
    End With 
    RaiseEvent evtOverrideLoadCtlUserLoginKeyCol(pLoadParameters)
    
    pFault = _ctlUserLoginKeyCol.LoadControl(_UserLoginKeyCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlUserLoginKeyCol.Visible = True 
 
    If pID > 0 Then 
      Dim pUserLoginKeys As csUserLoginKeyCol = CType(_ctlUserLoginKeyCol.bsCtlUserLoginKey.DataSource, csUserLoginKeyCol) 
      Dim pUserLoginKey As csUserLoginKey = pUserLoginKeys.FindByID((pID)) 
      If pUserLoginKey.ID > 0 Then 
        _ctlUserLoginKeyCol.bsCtlUserLoginKey.CurrencyManager.Position = pUserLoginKeys.IndexOf(pUserLoginKey) 
        _ctlUserLoginKeyCol.dgvUserLoginKey.Rows(pUserLoginKeys.IndexOf(pUserLoginKey)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlUserPermissionCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlUserPermissionCol.dgvUserPermission.SelectedRows.Count > 0 Then 
      Dim pUserPermission As csUserPermission = CType(_ctlUserPermissionCol.bsCtlUserPermission.Current, csUserPermission) 
      pID = pUserPermission.ID 
    End If 
 
    Dim pTestCol As csUserPermissionCol = Nothing 
    RaiseEvent evtOverrideFillUserPermissionCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _UserPermissionCol = New csUserPermissionCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserPermissionCol.FillByUserID(_UserID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _UserPermissionCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserPermissionCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserPermissionCol.Count) 
      End If 
    Else 
      _UserPermissionCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _UserPermissionCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_UserPermissionCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _User IsNot Nothing AndAlso Not String.IsNullOrEmpty(_User.DefaultDesignation) Then 
        .ReportTitle = "List of UserPermissions for " & _User.DefaultDesignation 
      Else 
        .ReportTitle = "List of UserPermissions for User" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csUserPermission.enmProperty.User) 
    End With 
    RaiseEvent evtOverrideLoadCtlUserPermissionCol(pLoadParameters)
    
    pFault = _ctlUserPermissionCol.LoadControl(_UserPermissionCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlUserPermissionCol.Visible = True 
 
    If pID > 0 Then 
      Dim pUserPermissions As csUserPermissionCol = CType(_ctlUserPermissionCol.bsCtlUserPermission.DataSource, csUserPermissionCol) 
      Dim pUserPermission As csUserPermission = pUserPermissions.FindByID((pID)) 
      If pUserPermission.ID > 0 Then 
        _ctlUserPermissionCol.bsCtlUserPermission.CurrencyManager.Position = pUserPermissions.IndexOf(pUserPermission) 
        _ctlUserPermissionCol.dgvUserPermission.Rows(pUserPermissions.IndexOf(pUserPermission)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
  Private Function RefreshCtlUserStatusCol() As clsFault 
    Dim pFault As New clsFault 
    Dim pTitle As String = Nothing 
 
    'get selected row 
    Dim pID As Long = 0 
    If _ctlUserStatusCol.dgvUserStatus.SelectedRows.Count > 0 Then 
      Dim pUserStatus As csUserStatus = CType(_ctlUserStatusCol.bsCtlUserStatus.Current, csUserStatus) 
      pID = pUserStatus.ID 
    End If 
 
    Dim pTestCol As csUserStatusCol = Nothing 
    RaiseEvent evtOverrideFillUserStatusCol(pTestCol, pTitle) 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    If pTestCol Is Nothing Then 
      _UserStatusCol = New csUserStatusCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserStatusCol.FillByUserID(_UserID, _Requester, 100, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Return pFault 
 
      If _UserStatusCol.Count > 99 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserStatusCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserStatusCol.Count) 
      End If 
    Else 
      _UserStatusCol = pTestCol 
      If pTitle Is Nothing Then 
        pTitle = String.Format(pShowing0Rows, _UserStatusCol.Count) 
      End If 
    End If 
    
    Dim pLoadParameters As New ctlc_UserStatusCol.clsLoadParameters() 
    With pLoadParameters 
      .SpreadsheetShowAllFields = False 
      .GridTitle = pTitle 
      If _User IsNot Nothing AndAlso Not String.IsNullOrEmpty(_User.DefaultDesignation) Then 
        .ReportTitle = "List of UserStatuss for " & _User.DefaultDesignation 
      Else 
        .ReportTitle = "List of UserStatuss for User" 
      End If 
      .ReadOnly = True 
      .SummarizeGrid = True 
      .ColumnsHide.Add(csUserStatus.enmProperty.User) 
    End With 
    RaiseEvent evtOverrideLoadCtlUserStatusCol(pLoadParameters)
    
    pFault = _ctlUserStatusCol.LoadControl(_UserStatusCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
    _ctlUserStatusCol.Visible = True 
 
    If pID > 0 Then 
      Dim pUserStatuss As csUserStatusCol = CType(_ctlUserStatusCol.bsCtlUserStatus.DataSource, csUserStatusCol) 
      Dim pUserStatus As csUserStatus = pUserStatuss.FindByID((pID)) 
      If pUserStatus.ID > 0 Then 
        _ctlUserStatusCol.bsCtlUserStatus.CurrencyManager.Position = pUserStatuss.IndexOf(pUserStatus) 
        _ctlUserStatusCol.dgvUserStatus.Rows(pUserStatuss.IndexOf(pUserStatus)).Selected = True 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
 
  'Handle events 
  Private Sub _ctlJobAlertRecipientCol_evtBeforeUpdate(ByVal vJobAlertRecipient As csJobAlertRecipient, ByRef rCancel As Boolean) Handles _ctlJobAlertRecipientCol.evtBeforeUpdate 
    vJobAlertRecipient.UserID = _User.ID 
  End Sub 
  Private Sub _ctlMFA_evtBeforeUpdate(ByVal vMFA As csMFA, ByRef rCancel As Boolean) Handles _ctlMFA.evtBeforeUpdate 
    vMFA.UserID = _UserID 
  End Sub 
  Private Sub _ctlUserLoginKeyCol_evtBeforeUpdate(ByVal vUserLoginKey As csUserLoginKey, ByRef rCancel As Boolean) Handles _ctlUserLoginKeyCol.evtBeforeUpdate 
    vUserLoginKey.UserID = _User.ID 
  End Sub 
  Private Sub _ctlUserPermissionCol_evtBeforeUpdate(ByVal vUserPermission As csUserPermission, ByRef rCancel As Boolean) Handles _ctlUserPermissionCol.evtBeforeUpdate 
    vUserPermission.UserID = _User.ID 
  End Sub 
  Private Sub _ctlUserStatusCol_evtBeforeUpdate(ByVal vUserStatus As csUserStatus, ByRef rCancel As Boolean) Handles _ctlUserStatusCol.evtBeforeUpdate 
    vUserStatus.UserID = _User.ID 
  End Sub 
  Private Sub _ctlUser_evtDeleted(ByVal vUserID As Long) Handles _ctlUser.evtDeleted 
    _UserCol = Nothing 
    Dim pFault As clsFault 
    _UserID = -1 
    If _ShowIntelligentCombo = True Then 
      pFault = LoadCboUsers(True) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      pFault = RefreshCtlUser() 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlUser.btnAdd.Visible = False 
    Else  
      lnk_Click(lnkUserCol, New System.EventArgs) 
    End If  
  End Sub 
  Private Sub _ctlUser_evtCancelledEdit(ByVal vUser As csUser) Handles _ctlUser.evtCancelledEdit 
    RefreshCtlUser() 
    If _ShowIntelligentCombo = True Then 
      Dim pFault As clsFault = LoadCboUsers(False) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      _ctlUser.btnAdd.Visible = False 
      If _UserID = 0 OrElse _UserID = -2 Then 
        pnlUser.Visible = False 
      Else 
        pnlUser.Visible = True 
      End If 
      MyIntelliCombo.Visible = True 
    Else 
      If _ctlUser.txtID.Text = "-1" Then 
        Dim pFault As clsFault = ActivateControl("ctlc_UserCol") 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
        pnlCover.SendToBack() 
      End If 
    End If 
  End Sub 
  Private Sub _ctlUser_evtUpdated(ByVal vWhichProperty As csUser.enmUpdateType, ByVal vUser As csUser) Handles _ctlUser.evtUpdated 
    _UserCol = Nothing 
    Dim pFault As clsFault 
    _UserID = CType(vUser, csUser).ID 
    If _ActiveControl.Name = "ctlc_User" Then 
      If _ShowIntelligentCombo = True Then 
        pFault = LoadCboUsers(True) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      Else 
        pFault = RefreshCtlUser() 'no need if _ShowIntelligentCombo = True Called via LoadCboCustomers 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) 
      End If 
      _ctlUser.btnAdd.Visible = False 
    End If  
  End Sub 
  Private Function LoadCboUsers(ByVal vRenewCache As Boolean) As clsFault 
    Dim pFault As clsFault 
    
    Dim pChoose As String = ccHelper.GetChoose(_Requester) 
    
    'enable using an external list if needed  
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pAddNewPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_UserDefaultByID 
    Dim pParentID As Long = 0 
    
    RaiseEvent evtOverrideLoadCboUser(pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart, pAddNewPrompt) 
    If pComboList Is Nothing Then 
      If vRenewCache = True Then MyCache.ClearComboList(pComboListTypeToLoad) 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK Then Return pFault 
      If _Requester.UserIdentityType = clsEnums.enmUserIdentityType.c_User Then 
        Dim pCombolistMember As clsComboListMember = pComboList.FindByKey(_Requester.UserIdentityInstanceID) 
        pComboList.Clear() 
        pComboList.Add(pCombolistMember) 
      End If 
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
 
    If _UserID >= 0 Then 
      MyIntelliCombo.ValueSelect(_UserID) 
    End If 
 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) = True Then 
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
      If ccHelper.IsNumeric(vComboListMember.Text) Then _UserID = ccHelper.ToLong(vComboListMember.Text) 
      lblSecondaryTitle.Text = "ID: " & vComboListMember.Text 
    ElseIf vComboListMember.KeyLong = -3 Then 
      sLoading = True 
      If ccHelper.IsNumeric(vComboListMember.Text) Then _UserID = ccHelper.ToLong(vComboListMember.Text) 
      RaiseEvent evtGetUserIDFromIntelliComboText(vComboListMember.Text) 
      lblSecondaryTitle.Text = vComboListMember.Text 
    Else 
      sLoading = True 
      _UserID = vComboListMember.KeyLong 
      lblSecondaryTitle.Text = vComboListMember.Text 
    End If 
    'Check if entity is already open in another tab BEFORE loading 
    If _UserID > 0 Then 
      Dim pMainForm As frmMain = TryCast(Me.FindForm(), frmMain) 
      If pMainForm IsNot Nothing Then 
        If pMainForm.UpdateActiveMenuCodeWithID(Me.Name, _UserID, vComboListMember.Text) = False Then 
          'Entity already open in another tab - abort loading and reset 
          _UserID = 0 
          MyIntelliCombo.ValueClear() 
          lblSecondaryTitle.Text = "" 
          sLoading = False 
          Cursor = Cursors.Default 
          Return 
        End If 
      End If 
    End If 
    ChooseUser() 
    If _ActiveControl IsNot Nothing Then 
      If Not _ActiveControl.Name.Equals("ctlc_User", StringComparison.OrdinalIgnoreCase) AndAlso _UserID > 0 Then 
        'to avoid getting ObjectNotFound 
        _User = New csUser(clsEnums.enmLoadParent.TextOnly) 
        pFault = _User.GetByID(_UserID, _Requester, False) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return 
      End If 
      pFault = ActivateControl(_ActiveControl.Name) 
    Else 
      pFault = ActivateControl("ctlc_User") 
    End If 
    pnlUser.Visible = True 
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
  Private Sub _ctlLoggedAlertsForAffectedUserCol_evtRowDoubleClicked(ByVal vLoggedAlert As csLoggedAlert, ByRef rHandled As Boolean) Handles _ctlLoggedAlertsForAffectedUserCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtLoggedAlertsForAffectedUserChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtLoggedAlertsForAffectedUserChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vLoggedAlert.ID 
      .Object = New csLoggedAlert 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlLoggedRequestCol_evtRowDoubleClicked(ByVal vLoggedRequest As csLoggedRequest, ByRef rHandled As Boolean) Handles _ctlLoggedRequestCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtLoggedRequestChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtLoggedRequestChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vLoggedRequest.ID 
      .Object = New csLoggedRequest 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlUserLoginKeyCol_evtRowDoubleClicked(ByVal vUserLoginKey As csUserLoginKey, ByRef rHandled As Boolean) Handles _ctlUserLoginKeyCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtUserLoginKeyChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtUserLoginKeyChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vUserLoginKey.ID 
      .Object = New csUserLoginKey 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlUserPermissionCol_evtRowDoubleClicked(ByVal vUserPermission As csUserPermission, ByRef rHandled As Boolean) Handles _ctlUserPermissionCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtUserPermissionChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtUserPermissionChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vUserPermission.ID 
      .Object = New csUserPermission 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  Private Sub _ctlUserStatusCol_evtRowDoubleClicked(ByVal vUserStatus As csUserStatus, ByRef rHandled As Boolean) Handles _ctlUserStatusCol.evtRowDoubleClicked 
    rHandled = False 
    If _CancelEvtUserStatusChosen = True Then rHandled = True : Return 
    If _ShowPopForEvtUserStatusChosen = True Then rHandled = False : Return 
     
    Dim pEventArgs As New EntityEventArgs 
    With pEventArgs 
      .UniqueCode = vUserStatus.ID 
      .Object = New csUserStatus 
    End With 
    Try 
      RaiseEvent evtEntityChosen(Me, pEventArgs) 
      rHandled = True 
    Catch ex As Exception 
      rHandled = False 
    End Try 
  End Sub 
  
  'Choose Parents
  Private Sub _ctl_evtParentChosen(ByVal vParentName As csUser.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) Handles _ctlUser.evtParentChosen 
    If vParentName = csUser.enmParentProperty.Role Then 
      rHandled = True 
      If _CancelEvtRoleChosen = True Then Exit Sub 
      Dim pEventArgs As New EntityEventArgs 
      With pEventArgs 
        .UniqueCode = vParentUniqueCode 
        .Object = New csRole 
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
    pnlUser.Visible = False 
    
    Dim pShowGrid As Boolean = chkGrid.Checked 
    If pShowGrid = True Then 
      lnkUserCol.Visible = True 
      _ShowIntelligentCombo = False 
      MyIntelliCombo.Visible = False 
      btnNew.Visible = False 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = MyIntelliCombo.Width 
      If _UserID <> 0 Then btnFilter.Visible = True 
      pFault = ActivateControl("ctlc_UserCol") 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
      pnlCover.SendToBack()  
    Else 
      btnFilter.Visible = False 
      MyIntelliCombo.Visible = True 
      btnNew.Visible = True 
      If lblSecondaryTitle.Parent Is gpbHeader Then lblSecondaryTitle.Width = 0 
      lnkUserCol.Visible = False 
      _ActiveControl = _ctlUser 
      lblSecondaryTitle.Visible = False 
      pFault = LoadCboUsers(False) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
      If _UserID <> 0 Then 
        pFault = ActivateControl("ctlc_User") 
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub  
        _ctlUser.btnAdd.Visible = False 
      Else 
        MyIntelliCombo.ValueClear() 
        pnlUser.Visible = False 
        _UserID = 0 
      End If 
      _ShowIntelligentCombo = True 
    End If 
    
    chkGrid.Enabled = True 
    If _UserID > 0 Then pnlUser.Visible = True 
    pnlButtons.Visible = True 
    Cursor = Cursors.Default 
  End Sub 
  'Now handle the links 
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ 
                  lnkJobAlertRecipientCol.MouseEnter, 
                  lnkLoggedAlertsForAffectedUserCol.MouseEnter, 
                  lnkLoggedRequestCol.MouseEnter, 
                  lnkMFA.MouseEnter, 
                  lnkUserLoginKeyCol.MouseEnter, 
                  lnkUserPermissionCol.MouseEnter, 
                  lnkUserStatusCol.MouseEnter, 
                  lnkUserCol.MouseEnter, 
                  lnkUser.MouseEnter, 
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
                  lnkLoggedAlertsForAffectedUserCol.MouseLeave, 
                  lnkLoggedRequestCol.MouseLeave, 
                  lnkMFA.MouseLeave, 
                  lnkUserLoginKeyCol.MouseLeave, 
                  lnkUserPermissionCol.MouseLeave, 
                  lnkUserStatusCol.MouseLeave, 
                  lnkUserCol.MouseLeave, 
                  lnkUser.MouseLeave, 
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
  Private Sub _ctlUser_evtAdd(ByVal vUser As csUser) Handles _ctlUser.evtAdd 
    lnkJobAlertRecipientCol.Visible = False 
    lnkLoggedAlertsForAffectedUserCol.Visible = False 
    lnkLoggedRequestCol.Visible = False 
    lnkMFA.Visible = False 
    lnkUserLoginKeyCol.Visible = False 
    lnkUserPermissionCol.Visible = False 
    lnkUserStatusCol.Visible = False 
    lnkUserCol.Visible = False 
  End Sub 
 
  Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault() : pFault.SetOK() 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideFilterButton(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    'Now set the items 
    Dim pUserName As String = Nothing 
    Dim pUserNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pLastName As String = Nothing 
    Dim pLastNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pFirstName As String = Nothing 
    Dim pFirstNameWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pCity As String = Nothing 
    Dim pCityWildcardType As clsEnums.enmWildCardType = Nothing 
    Dim pType As clsEnums.enmUserIdentityType = Nothing 
    Dim pIDinTypeFrom As Nullable(Of Long) = Nothing 
    Dim pIDinTypeTo As Nullable(Of Long) = Nothing 
    Dim pRoleID As Nullable(Of Long) = Nothing 
    Dim pLastSuccessfulLoginStart As Nullable(Of DateTimeOffset) = Nothing 
    Dim pLastSuccessfulLoginEnd As Nullable(Of DateTimeOffset) = Nothing 
    Dim pIDFrom As Nullable(Of Long) = Nothing 
    Dim pIDTo As Nullable(Of Long) = Nothing 
 
    Dim pGroupByLastName As Boolean = False 
    Dim pGroupByFirstName As Boolean = False 
    Dim pGroupByCity As Boolean = False 
    Dim pGroupByType As Boolean = False 
    Dim pGroupByIDinType As Boolean = False 
    Dim pGroupByRoleID As Boolean = False 
    Dim pGroupByLastSuccessfulLogin As Boolean = False 
    
    Dim pSumIDinType As Boolean = False 
    
    If _frmSearch Is Nothing Then RaiseEvent evtOverrideSearchForm() 
    
    If _frmSearch Is Nothing Then 
      _frmSearch = New frmFilter  
  
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        _frmSearch.RightToLeftLayout = True 
        _frmSearch.RightToLeft = RightToLeft.Yes 
      End If 
 
      _frmSearch.Text = "Filter the Users"  
  
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
        .String01Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.UserName), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.UserName), "User Name") 
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
 
        .String02Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.LastName), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.LastName), "Last Name") 
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
 
        .String03Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.FirstName), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.FirstName), "First Name") 
        .String03Text.Text = "" 
        .String03Text.TabIndex = 7 
        With .String03WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 8 
        End With 
        .flpFilter.Controls.Add(.String03Label) 
        .flpFilter.Controls.Add(.String03Text) 
        .flpFilter.Controls.Add(.String03LblWCType) 
        .flpFilter.Controls.Add(.String03WCType) 
 
        .String04Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.City), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.City), "City") 
        .String04Text.Text = "" 
        .String04Text.TabIndex = 9 
        With .String04WCType 
          .ValueMember = "EnumValue" 
          .DisplayMember = "Text" 
          .DataSource = pWildCardTypes.Clone() 
          .TabIndex = 10 
        End With 
        .flpFilter.Controls.Add(.String04Label) 
        .flpFilter.Controls.Add(.String04Text) 
        .flpFilter.Controls.Add(.String04LblWCType) 
        .flpFilter.Controls.Add(.String04WCType) 
 
        .Combo01Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.Type), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.Type), "Type") 
        Dim pTypes As New clsComboList 
        pFault = pTypes.FillEnums(clsEnums.enmEnum.UserIdentityType, _Requester) 
        If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        pTypes.Remove(pTypes.FindByKey(clsEnums.enmUserIdentityType.UD)) 
        pTypes.SortByText() 
        If pTypes IsNot Nothing AndAlso pTypes.Count > 0 Then 
          .flpFilter.Controls.Add(.Combo01Label) 
          .flpFilter.Controls.Add(.Combo01)  'Add 1st in case of IntelliCombo Logging
        End If 
        With .Combo01 
          .MakeSmart() 
          .LoadControl(pTypes, GetChoose(_Requester)) 
          .TabIndex = 11 
        End With 
 
        .Text01Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.IDinType), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.IDinType), "I Din Type") 
        .Text01From.Text = "" 
        .Text01From.TabIndex = 12 
        .Text01To.Text = "" 
        .Text01To.TabIndex = 13 
        .flpFilter.Controls.Add(.Text01Label) 
        .flpFilter.Controls.Add(.Text01From) 
        .flpFilter.Controls.Add(.Text01LblTo) 
        .flpFilter.Controls.Add(.Text01To) 
 
        .Combo02Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.Role), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.Role), "Role") 
        Dim pRoles As New clsComboList 
        pFault = MyCache.GetComboList(clsEnums.enmComboListType.c_RoleDefaultByID, pRoles) : If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
        'If pRoles IsNot Nothing AndAlso pRoles.Count > 0 Then 
        .flpFilter.Controls.Add(.Combo02Label) 
        .flpFilter.Controls.Add(.Combo02)  'Add 1st in case of IntelliCombo Logging
        'End If 
        With .Combo02 
          .MakeSmart() 
          If pRoles IsNot Nothing Then 
            .LoadControl(pRoles, GetChoose(_Requester)) 
          Else 
            .LoadControlAndPageFromServer(GetChoose(_Requester), clsEnums.enmComboListType.c_RoleDefaultByID, 0, _Requester) 
          End If 
          .TabIndex = 14 
        End With 
 
        .Date01Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.LastSuccessfulLogin), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.LastSuccessfulLogin), "Last Successful Login") 
        .Date01From.TabIndex = 15 
        .Date01To.TabIndex = 16 
        .Date01From.ShowCheckBox = True 
        .Date01To.ShowCheckBox = True 
        .Date01From.Checked = False 
        .Date01To.Checked = False 
        If _ctlUserCol.LoadParameters.ColumnsFormat.ContainsKey(csUser.enmProperty.LastSuccessfulLogin) Then 
          .Date01From.CustomFormat = _ctlUserCol.LoadParameters.ColumnsFormat(csUser.enmProperty.LastSuccessfulLogin) 
          .Date01To.CustomFormat = _ctlUserCol.LoadParameters.ColumnsFormat(csUser.enmProperty.LastSuccessfulLogin) 
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
 
        .Text02Label.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.ID), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.ID), "ID") 
        .Text02From.Text = "" 
        .Text02From.TabIndex = 17 
        .Text02To.Text = "" 
        .Text02To.TabIndex = 18 
        .flpFilter.Controls.Add(.Text02Label) 
        .flpFilter.Controls.Add(.Text02From) 
        .flpFilter.Controls.Add(.Text02LblTo) 
        .flpFilter.Controls.Add(.Text02To) 
 
        .flpGroupBy.Controls.Add(.lblGroupBy) 
 
        .lblGroupBy01.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.LastName), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.LastName), "Last Name") 
        .chkGroupBy01.Checked = False 
        .chkGroupBy01.TabIndex = 19 
        .flpGroupBy.Controls.Add(.lblGroupBy01) 
        .flpGroupBy.Controls.Add(.chkGroupBy01) 
 
        .lblGroupBy02.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.FirstName), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.FirstName), "First Name") 
        .chkGroupBy02.Checked = False 
        .chkGroupBy02.TabIndex = 20 
        .flpGroupBy.Controls.Add(.lblGroupBy02) 
        .flpGroupBy.Controls.Add(.chkGroupBy02) 
 
        .lblGroupBy03.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.City), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.City), "City") 
        .chkGroupBy03.Checked = False 
        .chkGroupBy03.TabIndex = 21 
        .flpGroupBy.Controls.Add(.lblGroupBy03) 
        .flpGroupBy.Controls.Add(.chkGroupBy03) 
 
        .lblGroupBy04.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.Type), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.Type), "Type") 
        .chkGroupBy04.Checked = False 
        .chkGroupBy04.TabIndex = 22 
        .flpGroupBy.Controls.Add(.lblGroupBy04) 
        .flpGroupBy.Controls.Add(.chkGroupBy04) 
 
        .lblGroupBy05.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.IDinType), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.IDinType), "I Din Type") 
        .chkGroupBy05.Checked = False 
        .chkGroupBy05.TabIndex = 23 
        .flpGroupBy.Controls.Add(.lblGroupBy05) 
        .flpGroupBy.Controls.Add(.chkGroupBy05) 
 
        .lblGroupBy06.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.Role), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.Role), "Role") 
        .chkGroupBy06.Checked = False 
        .chkGroupBy06.TabIndex = 24 
        .flpGroupBy.Controls.Add(.lblGroupBy06) 
        .flpGroupBy.Controls.Add(.chkGroupBy06) 
 
        .lblGroupBy07.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.LastSuccessfulLogin), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.LastSuccessfulLogin), "Last Successful Login") 
        .chkGroupBy07.Checked = False 
        .chkGroupBy07.TabIndex = 25 
        .flpGroupBy.Controls.Add(.lblGroupBy07) 
        .flpGroupBy.Controls.Add(.chkGroupBy07) 
 
        .flpSumColumns.Controls.Add(.lblSumColumns) 
 
        .lblSumField01.Text = If(_ctlUserCol.LoadParameters.ColumnsHeaderText.ContainsKey(csUser.enmProperty.IDinType), _ctlUserCol.LoadParameters.ColumnsHeaderText(csUser.enmProperty.IDinType), "I Din Type") 
        .chkSumField01.Checked = False 
        .chkSumField01.TabIndex = 26 
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
      If .String01Text.Text <> "" Then 
        pUserName = .String01Text.Text 
        pUserNameWildcardType = CType(CType(.String01WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.UserName, pUserName) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.UserNameWildcardType, pUserNameWildcardType) 
      End If 
      If .String02Text.Text <> "" Then 
        pLastName = .String02Text.Text 
        pLastNameWildcardType = CType(CType(.String02WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.LastName, pLastName) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.LastNameWildcardType, pLastNameWildcardType) 
      End If 
      If .String03Text.Text <> "" Then 
        pFirstName = .String03Text.Text 
        pFirstNameWildcardType = CType(CType(.String03WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.FirstName, pFirstName) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.FirstNameWildcardType, pFirstNameWildcardType) 
      End If 
      If .String04Text.Text <> "" Then 
        pCity = .String04Text.Text 
        pCityWildcardType = CType(CType(.String04WCType.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmWildCardType) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.City, pCity) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.CityWildcardType, pCityWildcardType) 
      End If 
      If .Combo01.SelectedItem IsNot Nothing Then 
        pType = CType(CType(.Combo01.SelectedItem, clsComboListMember).KeyEnum, clsEnums.enmUserIdentityType) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.Type, pType) 
      End If 
      If .Text01From.Text <> "" Then 
        If IsNumeric(.Text01From.Text) Then 
          pIDinTypeFrom = ccHelper.ToLong(.Text01From.Text) 
          If .Text01To.Text <> "" Then 
            pIDinTypeTo = ccHelper.ToLong(.Text01To.Text) 
          Else 
            pIDinTypeTo = pIDinTypeFrom 
          End If 
          _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.IDinTypeFrom, pIDinTypeFrom) 
          _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.IDinTypeTo, pIDinTypeTo) 
        End If 
      End If 
      If .Combo02.SelectedItem IsNot Nothing AndAlso CType(_frmSearch.Combo02.SelectedItem, clsComboListMember).Key.ToString() <> "" Then 
        pRoleID = CType(.Combo02.SelectedItem, clsComboListMember).KeyLong 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.RoleID, pRoleID) 
      End If 
      If .Date01From.Checked OrElse .Date01To.Checked Then 
        pLastSuccessfulLoginStart = .Date01From.Value 
        pLastSuccessfulLoginEnd = .Date01To.Value 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.LastSuccessfulLoginStart, pLastSuccessfulLoginStart) 
        _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.LastSuccessfulLoginEnd, pLastSuccessfulLoginEnd) 
      End If 
      If .Text02From.Text <> "" Then 
        If IsNumeric(.Text02From.Text) Then 
          pIDFrom = ccHelper.ToLong(.Text02From.Text) 
          If .Text02To.Text <> "" Then 
            pIDTo = ccHelper.ToLong(.Text02To.Text) 
          Else 
            pIDTo = pIDFrom 
          End If 
          _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.IDFrom, pIDFrom) 
          _SearchFilters.Add(csUserCol.enmFillOnTheFlyParameters.IDTo, pIDTo) 
        End If 
      End If 
      If .txtMaxRowsToReturn.Text <> "" AndAlso IsNumeric(.txtMaxRowsToReturn.Text) Then 
        Try 
          pRows = ccHelper.ToInteger(.txtMaxRowsToReturn.Text) 
          _SearchFilters.Add(csUserCol.enmListDefinition.HowMany, pRows) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
      End If 
      If .rbtnOldestFirst.Checked = True Then 
        pDir = clsEnums.enmFillDirection.ASC 
        _SearchFilters.Add(csUserCol.enmListDefinition.Dir, pDir) 
      End If 
 
      If .chkGroupBy01.Checked = True Then 
        pGroupByLastName = True 
        pDoSum = True 
        _SearchFilters.Add(csUserCol.enmFillSumOnTheFlyParameters.GroupByLastName, pGroupByLastName) 
      End If 
      If .chkGroupBy02.Checked = True Then 
        pGroupByFirstName = True 
        pDoSum = True 
        _SearchFilters.Add(csUserCol.enmFillSumOnTheFlyParameters.GroupByFirstName, pGroupByFirstName) 
      End If 
      If .chkGroupBy03.Checked = True Then 
        pGroupByCity = True 
        pDoSum = True 
        _SearchFilters.Add(csUserCol.enmFillSumOnTheFlyParameters.GroupByCity, pGroupByCity) 
      End If 
      If .chkGroupBy04.Checked = True Then 
        pGroupByType = True 
        pDoSum = True 
        _SearchFilters.Add(csUserCol.enmFillSumOnTheFlyParameters.GroupByType, pGroupByType) 
      End If 
      If .chkGroupBy05.Checked = True Then 
        pGroupByIDinType = True 
        pDoSum = True 
        _SearchFilters.Add(csUserCol.enmFillSumOnTheFlyParameters.GroupByIDinType, pGroupByIDinType) 
      End If 
      If .chkGroupBy06.Checked = True Then 
        pGroupByRoleID = True 
        pDoSum = True 
        _SearchFilters.Add(csUserCol.enmFillSumOnTheFlyParameters.GroupByRoleID, pGroupByRoleID) 
      End If 
      If .chkGroupBy07.Checked = True Then 
        pGroupByLastSuccessfulLogin = True 
        pDoSum = True 
        _SearchFilters.Add(csUserCol.enmFillSumOnTheFlyParameters.GroupByLastSuccessfulLogin, pGroupByLastSuccessfulLogin) 
      End If 
    
      If .chkSumField01.Checked = True Then 
        pSumIDinType = True 
        pDoSum = True 
      End If 
      
    End With 
 
    If _SearchFilters.Count > 0 Then 
      If Not _SearchFilters.ContainsKey(csUserCol.enmListDefinition.HowMany) Then _SearchFilters.Add(csUserCol.enmListDefinition.HowMany, pRows) 
      If Not _SearchFilters.ContainsKey(csUserCol.enmListDefinition.Dir) Then _SearchFilters.Add(csUserCol.enmListDefinition.Dir, pDir) 
    End If  
 
    Dim pLoadParameters As New ctlc_UserCol.clsLoadParameters()
    If pDoSum = False Then
      pLoadParameters = New ctlc_UserCol.clsLoadParameters() 
      With pLoadParameters 
        .SpreadsheetShowAllFields = False 
        .ReportTitle = "Summary List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .SearchFilters = _SearchFilters 
        .ColumnsHeaderText.Add(csUser.enmProperty.ID, "ID") 
      End With 
      _UserCol = New csUserCol(clsEnums.enmLoadParent.TextOnly) 
 
      If IsFiltered() Then 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
        pFault = _UserCol.FillOnTheFly(_SearchFilters, vRequester:=_Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
      Else 
        btnFilter.BackColor = Me.BackColor 
        lblTitle.ForeColor = Color.Black 
        _Tooltip.SetToolTip(lblTitle, "") 
        _Tooltip.SetToolTip(btnFilter, "") 
        Select Case _Requester.UserIdentityType 
          Case Else 
            pFault = _UserCol.Fill(vRequester:=_Requester, vHowMany:=pRows, vDir:=pDir) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        End Select 
      End If 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      Dim pTitle As String 
      If _UserCol.Count = 100 Then 
        pTitle = "Showing 1st 99 rows" 
        _UserCol.RemoveAt(99) 
      Else 
        pTitle = String.Format(pShowing0Rows, _UserCol.Count) 
      End If 
      pLoadParameters.GridTitle = CreateGridTitle(pTitle) & "Double click row to see User" 
      RaiseEvent evtOverrideLoadCtlUserCol(pLoadParameters) 
      btnRefresh.Visible = True 
    Else 
        btnFilter.BackColor = Color.Pink 
        lblTitle.ForeColor = Color.Red 
        _Tooltip.SetToolTip(lblTitle, "Double click this label to reset the filter") 
        _Tooltip.SetToolTip(btnFilter, "Double click the text to the left to reset the filter") 
      
      _UserCol = New csUserCol(clsEnums.enmLoadParent.TextOnly) 
      pFault = _UserCol.FillSumOnTheFly(_SearchFilters, vRequester:=_Requester) 
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
      If pShowing0Rows = "" Then 
        pShowing0Rows = "Showing {0} rows" 
      End If 
 
      pLoadParameters = New ctlc_UserCol.clsLoadParameters() 
      With pLoadParameters 
        .IsSumFillOnTheFly = True 
        .SpreadsheetShowAllFields = False 
        .GridTitle = CreateGridTitle(String.Format(pShowing0Rows, _UserCol.Count)) & "Double click row to see listing" 
        .ReportTitle = "Summary Grouped By List" 
        .ReadOnly = True 
        .SummarizeGrid = True 
        .ColumnsHide.Clear() 
        .ColumnsHeaderText.Clear() 
        .ColumnsHeaderText.Add(csUser.enmProperty.ID, "Count") 
        If pGroupByLastName = False Then .ColumnsHide.Add(csUser.enmProperty.LastName) 
        If pGroupByFirstName = False Then .ColumnsHide.Add(csUser.enmProperty.FirstName) 
        If pGroupByCity = False Then .ColumnsHide.Add(csUser.enmProperty.City) 
        If pGroupByType = False Then .ColumnsHide.Add(csUser.enmProperty.Type) 
        If pGroupByIDinType = False Then .ColumnsHide.Add(csUser.enmProperty.IDinType) 
        If pGroupByRoleID = False Then .ColumnsHide.Add(csUser.enmProperty.Role) 
        If pGroupByLastSuccessfulLogin = False Then .ColumnsHide.Add(csUser.enmProperty.LastSuccessfulLogin) 
        If pSumIDinType = False Then .ColumnsHide.Add(csUser.enmProperty.IDinType) 
        If pGroupByIDinType = True OrElse pSumIDinType = True Then If .ColumnsHide.Contains(csUser.enmProperty.IDinType) Then .ColumnsHide.Remove(csUser.enmProperty.IDinType) 
        .ColumnsHide.Add(csUser.enmProperty.UserName) 
        .ColumnsHide.Add(csUser.enmProperty.FullName) 
        .ColumnsHide.Add(csUser.enmProperty.NationalIDNo) 
        .ColumnsHide.Add(csUser.enmProperty.Address) 
        .ColumnsHide.Add(csUser.enmProperty.ProvinceState) 
        .ColumnsHide.Add(csUser.enmProperty.PostalCode) 
        .ColumnsHide.Add(csUser.enmProperty.Country) 
        .ColumnsHide.Add(csUser.enmProperty.PhoneNumber) 
        .ColumnsHide.Add(csUser.enmProperty.Email) 
        .ColumnsHide.Add(csUser.enmProperty.PasswordHashed) 
        .ColumnsHide.Add(csUser.enmProperty.DatePasswordChanged) 
        .ColumnsHide.Add(csUser.enmProperty.RequiresComputerIdentification) 
        .ColumnsHide.Add(csUser.enmProperty.EnableSimultaneousLogins) 
        .ColumnsHide.Add(csUser.enmProperty.DateActivated) 
        .ColumnsHide.Add(csUser.enmProperty.IsDisabled) 
        .ColumnsHide.Add(csUser.enmProperty.ExpiryDate) 
        .ColumnsHide.Add(csUser.enmProperty.Comments) 
        .ColumnsHide.Add(csUser.enmProperty.LastPasswords) 
        .ColumnsHide.Add(csUser.enmProperty.Applications) 
        .ColumnsHide.Add(csUser.enmProperty.Language) 
        .ColumnsHide.Add(csUser.enmProperty.IsLockedOut) 
        .ColumnsHide.Add(csUser.enmProperty.AuthenticationMethod) 
        .ColumnsHide.Add(csUser.enmProperty.RequiresFixedIP) 
        .ColumnsHide.Add(csUser.enmProperty.MessagingMode) 
        .ColumnsHide.Add(csUser.enmProperty.LoggedInIP) 
        .ColumnsHide.Add(csUser.enmProperty.ApprovalCodeHashed) 
        .ColumnsHide.Add(csUser.enmProperty.ApprovalFunctionName) 
        .ColumnsHide.Add(csUser.enmProperty.ApprovalTime) 
        .ColumnsHide.Add(csUser.enmProperty.PasswordNeverExpires) 
        .ColumnsHide.Add(csUser.enmProperty.SecurityQuestion1) 
        .ColumnsHide.Add(csUser.enmProperty.SecurityQuestion1Response) 
        .ColumnsHide.Add(csUser.enmProperty.SecurityQuestion2) 
        .ColumnsHide.Add(csUser.enmProperty.SecurityQuestion2Response) 
        .ColumnsHide.Add(csUser.enmProperty.SecurityQuestion3) 
        .ColumnsHide.Add(csUser.enmProperty.SecurityQuestion3Response) 
        .ColumnsHide.Add(csUser.enmProperty.PIN) 
        .ColumnsHide.Add(csUser.enmProperty.Tag) 
        .SearchFilters = New Dictionary(Of System.Enum, Object) 
      End With 
      btnRefresh.Visible = False 
    End If 
    _ctlUserCol.Visible = True 
    pFault = _ctlUserCol.LoadControl(_UserCol, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
 
  End Sub 
 
  Private Function IsFiltered() As Boolean 
    If _SearchFilters.Count = 0 Then Return False 
 
    Dim pIsFiltered As Boolean = False 
 
    If _SearchFilters.Count > 2 Then 
      pIsFiltered = True 
    Else 
      If ccHelper.ToInteger(_SearchFilters(csUserCol.enmListDefinition.HowMany)) <> 100 Then 
        pIsFiltered = True 
      End If 
      If CType(_SearchFilters(csUserCol.enmListDefinition.Dir), clsEnums.enmFillDirection) <> clsEnums.enmFillDirection.DESC Then 
        pIsFiltered = True 
      End If 
    End If 
 
    Return pIsFiltered 
  End Function 
 
  Private Sub lblBack_Click(sender As System.Object, e As System.EventArgs) Handles lblBack.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    If _NestedFormsCount > 0 Then 
      pnlUser.Controls(0).SendToBack() 
      pFault = ActivateControl(pnlUser.Controls(0).Name) 
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
    _UserID = -2 
    pFault = ActivateControl("ctlc_User") : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    pFault = RefreshCtlUser() : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    MyIntelliCombo.Visible = False 
    pnlUser.Visible = True 'new 
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
 
  Private Sub _ctlUserCol_evtTimerTripped() Handles _ctlUserCol.evtTimerTripped 
    Dim pCancel As Boolean = False 
    RaiseEvent evtUserTimerTripped(pCancel) 
    If pCancel = True Then Exit Sub 
 
    'get the last one 
    Dim pLastID As Long = _ctlUserCol.UserCol(0).ID 
    btnRefresh_Click(Me, New EventArgs) 
    Dim pNewLastID As Long = _ctlUserCol.UserCol(0).ID 
 
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
    If _UserCol.Count = 0 Then Return vShowingRowsText & ". " 
    Dim pFoundFilters As Boolean = False 
    For Each l In _SearchFilters 
      Dim pFieldName As String = l.Key.ToString() 
      Dim pTestRow As New csUser() 
      If pFieldName.StartsWith("GroupBy") Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf ccHelper.GetPropertyTypeName(pTestRow, pFieldName) = "Boolean" Then 
        pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
      ElseIf pFieldName.EndsWith("ID") Then 
        Dim pCol As csUserCol = CType(CallByName(_UserCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserCol) 
        If Not (pCol Is Nothing OrElse pCol.Count = 0) Then 
          pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) 
          Dim pText As String = CallByName(pCol(0), $"{pFieldName}Text", CallType.Get).ToString() 
          pFilters.Append($"{pFieldName}: {pText}; ") 
        Else 
          pFilters.Append(pFieldName & ": " & l.Value.ToString & "; ") 
        End If 
      ElseIf pFieldName.EndsWith("Code") Then 
        Dim pCol As csUserCol = CType(CallByName(_UserCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserCol) 
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
                  ccHelper.GetPropertyTypeName(New csUserCol(), $"{pFieldName}Text") = "") Then 
        Dim pCol As csUserCol = CType(CallByName(_UserCol, "CloneBy" & pFieldName, CallType.Method, l.Value), csUserCol) 
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
 
  Private Sub cc_ctlPnlUser_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
 
    Dim pSize As Single = CSng(14 * MyFont.Size / 9) 
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 
 
    pSize = CSng(12 * MyFont.Size / 9) 
    lblSecondaryTitle.Font = New Font(MyFont.Name, pSize) ', FontStyle.Bold 
  End Sub 
 
  Private Sub ctlPnlc_User_ccevtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True   
    If lnkUserPermissionCol.Parent IsNot Nothing Then lnkUserPermissionCol.Parent.Controls.Remove(lnkUserPermissionCol) 
    If lnkUserLoginKeyCol.Parent IsNot Nothing Then lnkUserLoginKeyCol.Parent.Controls.Remove(lnkUserLoginKeyCol) 
    If lnkLoggedRequestCol.Parent IsNot Nothing Then lnkLoggedRequestCol.Parent.Controls.Remove(lnkLoggedRequestCol) 
    Dim pParent = lnkJobAlertRecipientCol.Parent 
    If lnkJobAlertRecipientCol.Parent IsNot Nothing Then lnkJobAlertRecipientCol.Parent.Controls.Remove(lnkJobAlertRecipientCol) 
    If lnkMFA.Parent IsNot Nothing Then lnkMFA.Parent.Controls.Remove(lnkMFA) 
    pParent.Controls.Add(lnkJobAlertRecipientCol) 
 
    lnkUserStatusCol.Text = "Last Logins" 
    lnkLoggedAlertsForAffectedUserCol.Text = "Logged Alerts" 
  End Sub 
End Class 
