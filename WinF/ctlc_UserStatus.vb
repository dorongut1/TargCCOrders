Public Class ctlc_UserStatus
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csUserStatus.enmUpdateType) 
  Public Event evtAdd(ByVal vUserStatus As csUserStatus) 
  Public Event evtBeforeUpdate(ByVal vUserStatus As csUserStatus, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csUserStatus.enmUpdateType, ByVal vUserStatus As csUserStatus) 
  Public Event evtBeforeDelete(ByVal vUserStatus As csUserStatus, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vUserStatusID As Long) 
  Public Event evtCancelledEdit(ByVal vUserStatus As csUserStatus) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vUserStatus As csUserStatus) 
  
  Public Event evtParentChosen(ByVal vParentName As csUserStatus.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csUserStatus.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csUserStatus.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csUserStatus.enmParentProperty, ByVal vSelectedValue As Object) 
   
  Private WithEvents _Tooltip As New ToolTip 
  
  Private _LoadParameters As clsLoadParameters 
  Friend Property LoadParameters() As clsLoadParameters 
    Get 
      Return _LoadParameters 
    End Get 
    Set(value As clsLoadParameters) 
      _LoadParameters = value 
    End Set 
  End Property 
  
  Public Class clsLoadParameters 
    Public Property [ReadOnly]() As Boolean 
    Public Property EnableParentLinks As List(Of csUserStatus.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csUserStatus.enmParentProperty) 
      _EnableParentLinks.Add(csUserStatus.enmParentProperty.User) 
 
    End Sub 
  End Class 
 
  Private WithEvents _UserStatus As csUserStatus

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlUserStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.DoubleBuffered = True 
    If Me.DesignMode = True Then Exit Sub
    
    'buttons
    'btnUpdate.Location = btnEdit.Location
    'btnCancel.Location = btnAdd.Location
    'txtGrabFocus - to make sure topmost item has focus 
    If txtGrabFocus IsNot Nothing Then Return 
    Me.txtGrabFocus = New System.Windows.Forms.TextBox() 
    Me.txtGrabFocus.BorderStyle = System.Windows.Forms.BorderStyle.None 
    Me.txtGrabFocus.Location = New System.Drawing.Point(0, 0) 
    Me.txtGrabFocus.Name = "txtGrabFocus" 
    Me.txtGrabFocus.Size = New System.Drawing.Size(0, 13) 
    Me.txtGrabFocus.TabIndex = 0 
    Me.Controls.Add(Me.txtGrabFocus) 
 
    MakeControlRTL(Me) 
 
  End Sub

  Private Sub SetUpControls()
    'multiple control location
    cboUser.Size = txtUser.Size
    cboUser.Location = txtUser.Location
    dtpLoginTime.Size = txtLoginTime.Size
    dtpLoginTime.Location = txtLoginTime.Location
    dtpLogoutTime.Size = txtLogoutTime.Size
    dtpLogoutTime.Location = txtLogoutTime.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vUserStatusID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUserStatus As New csUserStatus(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vUserStatusID <> 0 Then 
      pFault = pUserStatus.GetByID(vUserStatusID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pUserStatus) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rUserStatus As csUserStatus, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rUserStatus)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rUserStatus As csUserStatus) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _UserStatus = rUserStatus 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    'Combos
    'Set comboListsCache 
    MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.Previous) 
    
    'Lookup Combos
    'EnumCombos
    
    ControlsLoad()

    SetUpButtons(False)

    If txtGrabFocus IsNot Nothing Then txtGrabFocus.Focus() 

    RaiseEvent evtLoaded() 

    Return pFault.SetOK() 
  End Function

  Private Function LoadCbos() As clsFault 
    Dim pFault As New clsFault() 
 
    _Loading = True 
 
    'Lookups (in case of change)
 
    'Parents
    pFault = LoadCboUser() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rUserStatus"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rUserStatus As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rUserStatus.GetType.Name = "csUserStatus" Then 
      ctlUserStatus_Load(Nothing, Nothing) 
      Dim pUserStatus As csUserStatus = CType(rUserStatus, csUserStatus) 
      Return LoadControl(pUserStatus) 
    Else 
      Dim pUserStatusID As Long = CType(rUserStatus, Long) 
      Return LoadControl(pUserStatusID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserStatus", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserStatus", "User", _Requester) 
    If pStrg <> "" Then lblUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserStatus", "ApplicationName", _Requester) 
    If pStrg <> "" Then lblApplicationName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserStatus", "LastLoggedLoginID", _Requester) 
    If pStrg <> "" Then lblLastLoggedLoginID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserStatus", "LoginTime", _Requester) 
    If pStrg <> "" Then lblLoginTime.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserStatus", "LogoutTime", _Requester) 
    If pStrg <> "" Then lblLogoutTime.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [UserStatus]() As csUserStatus
    Get 
      Return _UserStatus 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboUser() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_UserDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csUserStatus.enmParentProperty.User, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboUser.MakeSmart() Else cboUser.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboUser.LoadControl(pComboList, pPrompt) 
    Else 
      cboUser.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _UserStatus.UserID > 0 Then cboUser.ValueSelect(_UserStatus.UserID) Else cboUser.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboUser_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboUser.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csUserStatus.enmParentProperty.User, pUniqueCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csUserStatus.enmParentProperty = csUserStatus.enmParentProperty.UD 
    
    'Load comboboxes 
    If vInEdit = True Then 
      Dim pFault As clsFault 
      pFault = LoadCbos() 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    End If 
 
    Dim pDefaultColour As System.Drawing.Color 
    Dim pReadonlyColour As System.Drawing.Color 
    pDefaultColour = System.Drawing.Color.White 
    If vInEdit = True Then 
      pReadonlyColour = System.Drawing.Color.PapayaWhip 
    Else 
      pReadonlyColour = pDefaultColour 
    End If 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csUserStatus.enmParentProperty.User) = csUserStatus.enmParentProperty.User Then 
      lblUser.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtUser.ReadOnly = True
      txtUser.Visible = True
      txtUser.BackColor = pReadonlyColour
      txtUser.ForeColor = SetForeColor(vInEdit) 
      cboUser.Visible = False 
    Else 
      txtUser.ReadOnly = True
      txtUser.Visible = Not (vInEdit)
      txtUser.BackColor = pReadonlyColour 
      txtUser.ForeColor = SetForeColor(vInEdit) 
      cboUser.Visible = vInEdit
    End If  
    txtApplicationName.ReadOnly = Not (vInEdit)
    txtApplicationName.BackColor = pDefaultColour 
    txtLastLoggedLoginID.ReadOnly = Not (vInEdit)
    txtLastLoggedLoginID.BackColor = pDefaultColour 
    dtpLoginTime.Visible = vInEdit
    txtLoginTime.Visible = Not (vInEdit)
    txtLoginTime.BackColor = pReadonlyColour 
    txtLoginTime.ForeColor = SetForeColor(vInEdit) 
    txtLoginTime.ReadOnly = True
    dtpLogoutTime.Visible = vInEdit
    txtLogoutTime.Visible = Not (vInEdit)
    txtLogoutTime.BackColor = pReadonlyColour 
    txtLogoutTime.ForeColor = SetForeColor(vInEdit) 
    txtLogoutTime.ReadOnly = True

    If _LoadParameters.ReadOnly = False Then 
      If _ButtonsMoved = False Then 
        btnUpdate.Visible = True 
        btnCancel.Visible = True 
        btnEdit.Visible = True 
        btnAdd.Visible = True 
        btnDelete.Visible = True 
        btnDelete.Top = btnEdit.Top 
        _ButtonsMoved = True 
      End If 
      btnUpdate.Visible = vInEdit 
      btnCancel.Visible = vInEdit 
      btnUpdate.Top = btnEdit.Top 
      btnCancel.Top = btnEdit.Top 
      If _UserStatus.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserStatusUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserStatusDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserStatusUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
    Else 
      btnUpdate.Visible = False 
      btnCancel.Visible = False 
      btnEdit.Visible = False 
      btnDelete.Visible = False 
      btnAdd.Visible = False 
    End If 
    
    'disable or enable any child grids 
    Dim ctrl As Control = Me.GetNextControl(Me, True) 
    Do Until ctrl Is Nothing 
      If ctrl.GetType.Name.StartsWith("ctl") AndAlso ctrl.GetType.Name.EndsWith("Col") Then 
        ctrl.Enabled = Not vInEdit 
      End If 
      ctrl = Me.GetNextControl(ctrl, True) 
    Loop 
 
    RaiseEvent evtControlsRefreshed(vInEdit, _UserStatus) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _UserStatus
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtUser.Text = .UserText 
      txtApplicationName.Text = .ApplicationName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtApplicationName.MaxLength = 50 
      txtLastLoggedLoginID.Text = .LastLoggedLoginID.ToString(FormatFromTag(txtLastLoggedLoginID, "#,##0"))
      If .LoginTime < dtpLoginTime.MinDate Then dtpLoginTime.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpLoginTime.Value = .LoginTime
      dtpLoginTime.CustomFormat = FormatFromTag(txtLoginTime, "dd-MM-yyyy HH:mm:ss") 
      dtpLoginTime.Value = DateTime.ParseExact(dtpLoginTime.Value.ToString(dtpLoginTime.CustomFormat), dtpLoginTime.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .LoginTime < dtpLoginTime.MinDate Then dtpLoginTime.Checked = False Else dtpLoginTime.Checked = True 
      If Math.Abs(.LoginTime.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.LoginTime.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtLoginTime.Text = "" Else txtLoginTime.Text = .LoginTime.ToString(FormatFromTag(txtLoginTime, "dd-MM-yyyy HH:mm:ss"))
      If .LogoutTime < dtpLogoutTime.MinDate Then dtpLogoutTime.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpLogoutTime.Value = .LogoutTime
      dtpLogoutTime.CustomFormat = FormatFromTag(txtLogoutTime, "dd-MM-yyyy HH:mm:ss") 
      dtpLogoutTime.Value = DateTime.ParseExact(dtpLogoutTime.Value.ToString(dtpLogoutTime.CustomFormat), dtpLogoutTime.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .LogoutTime < dtpLogoutTime.MinDate Then dtpLogoutTime.Checked = False Else dtpLogoutTime.Checked = True 
      If Math.Abs(.LogoutTime.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.LogoutTime.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtLogoutTime.Text = "" Else txtLogoutTime.Text = .LogoutTime.ToString(FormatFromTag(txtLogoutTime, "dd-MM-yyyy HH:mm:ss"))
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _UserStatus
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-UserStatus-ID-090417-0012", _Requester) : Return pFault 
      If cboUser.SelectedItem Is Nothing OrElse cboUser.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .UserID = 0 
      Else 
        Dim pUserID As Long = CType(cboUser.SelectedItem, clsComboListMember).KeyLong 
        If pUserID = -1 Then .UserID = 0 Else .UserID = pUserID 
      End If 
      .ApplicationName = txtApplicationName.Text 
      If Long.TryParse(txtLastLoggedLoginID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .LastLoggedLoginID) = False Then pFault.LogFreeTextFault(208, ".LastLoggedLoginID", txtLastLoggedLoginID.Text, "TRGT-UserStatus-LastLoggedLoginID-090417-0012", _Requester) : Return pFault 
      If (dtpLoginTime.ShowCheckBox AndAlso dtpLoginTime.Checked = False) OrElse dtpLoginTime.Value = New Date(1900, 1, 1, 0, 0, 0) Then .LoginTime = Nothing Else .LoginTime = dtpLoginTime.Value
      If (dtpLogoutTime.ShowCheckBox AndAlso dtpLogoutTime.Checked = False) OrElse dtpLogoutTime.Value = New Date(1900, 1, 1, 0, 0, 0) Then .LogoutTime = Nothing Else .LogoutTime = dtpLogoutTime.Value
    End With
    pFault.SetOK() 
    Return pFault 
  End Function
  
  'Handle one way encrypted textboxes
  
  'check control data validity 
  Private Sub txtID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtID.Text 
    Dim pTest As Long 
 
    If txtID.Text = "" Then Exit Sub 
    If txtID.Text = txtID.Name Then Exit Sub 
 
    If Long.TryParse(txtID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-UserStatus-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtLastLoggedLoginID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLastLoggedLoginID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtLastLoggedLoginID.Text 
    Dim pTest As Long 
 
    If txtLastLoggedLoginID.Text = "" Then Exit Sub 
    If txtLastLoggedLoginID.Text = txtLastLoggedLoginID.Name Then Exit Sub 
 
    If Long.TryParse(txtLastLoggedLoginID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-UserStatus-LastLoggedLoginID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csUserStatus.enmUpdateType.Standard) 
    Me.Refresh() 
    txtGrabFocus.Focus() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    Dim pFault As New clsFault 
    Try 
      pFault = ControlsRead() 
    Catch ex As Exception 
      pFault.LogException(ex, "", "TRGT-UserStatus-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_UserStatus, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _UserStatus.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      RaiseEvent evtUpdated(csUserStatus.enmUpdateType.Standard, _UserStatus) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_UserStatus_evtAfterUpdate 
  Private Sub _UserStatus_evtAfterUpdate() Handles _UserStatus.evtAfterUpdate, _UserStatus.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_UserStatus) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _UserStatus = New csUserStatus(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_UserStatus) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_UserStatus, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete the row with an ID of '" & _UserStatus.ID.ToString & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _UserStatus.ID 
    pFault = _UserStatus.Delete(_Requester) 
    If pFault.isOK = True Then 
      _UserStatus = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblUser_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.DoubleClick 
    If _UserStatus.UserID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csUserStatus.enmParentProperty.User) = csUserStatus.enmParentProperty.User Then 
      If _UserStatus.UserID <> 0 Then RaiseEvent evtParentChosen(csUserStatus.enmParentProperty.User, _UserStatus.UserID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "User Detail" 
      fPopup.LoadControl("ctlc_User", _UserStatus.UserID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblUser_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csUserStatus.enmParentProperty.User) <> csUserStatus.enmParentProperty.User Then Exit Sub 
    lblUser.ForeColor = Color.Brown 
    'lblUser.Font = New Font(lblUser.Font.Name, lblUser.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblUser.BackColor = ccHelper.InvertColour(lblUser.ForeColor) 'did this instead 
    lblUser.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblUser_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csUserStatus.enmParentProperty.User) <> csUserStatus.enmParentProperty.User Then Exit Sub 
    lblUser.ForeColor = Color.Brown 
    'lblUser.Font = New Font(lblUser.Font.Name, lblUser.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblUser.BackColor = Me.BackColor 'did this instead 
    lblUser.Cursor = Cursors.Default 
  End Sub 
 
  'SeparateUpdates 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  Private Sub ctlc_UserStatus_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the UserStatus to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pUserStatus As csUserStatus = _UserStatus 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pUserStatus.ToCSV) 
        Else 
          Clipboard.SetText(pUserStatus.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The UserStatus is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub txtID_GotFocus(sender As Object, e As EventArgs) Handles txtID.GotFocus 
    'this is done so that the form *always* loads on with (0,0) visible. txtGrabFocus can be focused during the 1st load, since it wasn't created yet.... 
    Static sDone As Boolean = False 
    If sDone = False Then 
      txtGrabFocus.Focus() 
      sDone = True 
    End If 
  End Sub 
   
  'Handle screen 
  Private Sub HandleUplViewText(vFieldText As String, vUplButton As Button, vEnableUpload As Boolean, Optional vButtonTextHint As String = "") 
 
    If Not String.IsNullOrEmpty(vFieldText) Then 
      vUplButton.Text = CCTextTranslate("View", _Requester) 
      _Tooltip.SetToolTip(vUplButton, CCTextTranslate($"Click to view - right click To delete", _Requester)) 
      vUplButton.Enabled = True 
    Else 
      If vEnableUpload Then 
        vUplButton.Text = CCTextTranslate("Upload", _Requester) 
        _Tooltip.SetToolTip(vUplButton, "") 
      Else 
        vUplButton.Text = "" 
        _Tooltip.SetToolTip(vUplButton, "") 
      End If 
      vUplButton.Enabled = vEnableUpload 
    End If 
 
    If Not String.IsNullOrEmpty(vButtonTextHint) Then 
      vUplButton.Text = vButtonTextHint & " " & vUplButton.Text 
    End If 
 
  End Sub 
 
  Private Sub ctlc_UserStatus_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlUserStatus_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
