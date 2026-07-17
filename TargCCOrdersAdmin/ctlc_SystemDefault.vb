Public Class ctlc_SystemDefault
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csSystemDefault.enmUpdateType) 
  Public Event evtAdd(ByVal vSystemDefault As csSystemDefault) 
  Public Event evtBeforeUpdate(ByVal vSystemDefault As csSystemDefault, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csSystemDefault.enmUpdateType, ByVal vSystemDefault As csSystemDefault) 
  Public Event evtBeforeDelete(ByVal vSystemDefault As csSystemDefault, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vSystemDefaultID As Long) 
  Public Event evtCancelledEdit(ByVal vSystemDefault As csSystemDefault) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vSystemDefault As csSystemDefault) 
  
  Public Event evtSeparateEdit(ByVal vPropertyName As csSystemDefault.enmUpdateType, ByRef rNewValue As String, ByRef rUseNewValue As Boolean, ByRef rCancelUpdate As Boolean, ByRef rNewPrompt As String, ByRef rAppendText As Boolean) 
  Public Event evtParentChosen(ByVal vParentName As csSystemDefault.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csSystemDefault.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csSystemDefault.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csSystemDefault.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csSystemDefault.enmParentProperty) 
    Public Property EnableBtnSettingValueUpdate As Boolean 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csSystemDefault.enmParentProperty) 
 
      _EnableBtnSettingValueUpdate = True 
    End Sub 
  End Class 
 
  Private WithEvents _SystemDefault As csSystemDefault

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlSystemDefault_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboSystemDefaultType.Size = txtSystemDefaultType.Size
    cboSystemDefaultType.Location = txtSystemDefaultType.Location
    'Separate buttons 
    btnSettingValueUpdate.Visible = _LoadParameters.EnableBtnSettingValueUpdate 
  End Sub

  Public Function LoadControl(ByVal vSystemDefaultID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pSystemDefault As New csSystemDefault() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vSystemDefaultID <> 0 Then 
      pFault = pSystemDefault.GetByID(vSystemDefaultID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pSystemDefault) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rSystemDefault As csSystemDefault, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rSystemDefault)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rSystemDefault As csSystemDefault) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _SystemDefault = rSystemDefault 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    'this will be done once only. 
    If Not Controls.Contains(btnHistory) Then 
     'btnHistory 
      'btnHistory.AutoSize = True 
      btnHistory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink 
      btnHistory.Anchor = AnchorStyles.Right Or AnchorStyles.Top 
      btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Popup 
      btnHistory.Name = "btnHistory" 
      btnHistory.Size = New System.Drawing.Size(ccHelper.ToInteger(txtID.Height * 1.25), txtID.Height) 
      'btnHistory.Text = "&&" 
      btnHistory.Text = "½" 
      btnHistory.Font = New Font("Wingdings", CSng(My.Settings.FontSize * 1.1), FontStyle.Bold) 
      btnHistory.UseVisualStyleBackColor = True 
      btnHistory.Location = New System.Drawing.Point(txtID.Left + txtID.Width + 25, txtID.Top) 
      txtID.Parent.Controls.Add(btnHistory) 
      btnHistory.BringToFront() 
    End If 
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboSystemDefaultType.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboSystemDefaultType() : If pFault.isOK = False Then Return pFault 
    End If 
    
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
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rSystemDefault"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rSystemDefault As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rSystemDefault.GetType.Name = "csSystemDefault" Then 
      ctlSystemDefault_Load(Nothing, Nothing) 
      Dim pSystemDefault As csSystemDefault = CType(rSystemDefault, csSystemDefault) 
      Return LoadControl(pSystemDefault) 
    Else 
      Dim pSystemDefaultID As Long = CType(rSystemDefault, Long) 
      Return LoadControl(pSystemDefaultID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_SystemDefault", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_SystemDefault", "Group", _Requester) 
    If pStrg <> "" Then lblGroup.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_SystemDefault", "SettingName", _Requester) 
    If pStrg <> "" Then lblSettingName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_SystemDefault", "SettingValue", _Requester) 
    If pStrg <> "" Then lblSettingValue.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_SystemDefault", "SystemDefaultType", _Requester) 
    If pStrg <> "" Then lblSystemDefaultType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_SystemDefault", "Description", _Requester) 
    If pStrg <> "" Then lblDescription.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [SystemDefault]() As csSystemDefault
    Get 
      Return _SystemDefault 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboSystemDefaultType() As clsFault
    Dim pFault As New clsFault
 
    'If cboSystemDefaultType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pSystemDefaultTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csSystemDefault.enmParentProperty.SystemDefaultType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pSystemDefaultTypees.FillEnums(clsEnums.enmEnum.SystemDefaultType, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pSystemDefaultTypees = pTestCol
    End If
    
    pSystemDefaultTypees.Remove(pSystemDefaultTypees.FindByKey(clsEnums.enmSystemDefaultType.UD))
    pSystemDefaultTypees.SortByText()
    pSystemDefaultTypees.AddToTop(clsEnums.enmSystemDefaultType.UD, GetChoose(_Requester))

    With cboSystemDefaultType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pSystemDefaultTypees
    End With

    cboSystemDefaultType.SelectedValue = _SystemDefault.SystemDefaultType 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboSystemDefaultType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSystemDefaultType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmSystemDefaultType = CType(cboSystemDefaultType.SelectedValue, clsEnums.enmSystemDefaultType) 
    RaiseEvent evtCboSelectedIndexChanged(csSystemDefault.enmParentProperty.SystemDefaultType, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csSystemDefault.enmParentProperty = csSystemDefault.enmParentProperty.UD 
    
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
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    txtGroup.ReadOnly = Not (vInEdit)
    txtGroup.BackColor = pDefaultColour 
    txtSettingName.ReadOnly = Not (vInEdit)
    txtSettingName.BackColor = pDefaultColour 
    txtSettingValue.ReadOnly = True 
    txtSettingValue.BackColor = pReadonlyColour 
    txtSettingValue.ForeColor = SetForeColor(vInEdit) 
    txtSystemDefaultType.ReadOnly = True
    txtSystemDefaultType.Visible = Not (vInEdit)
    txtSystemDefaultType.BackColor = pReadonlyColour 
    txtSystemDefaultType.ForeColor = SetForeColor(vInEdit) 
    cboSystemDefaultType.Visible = vInEdit
    txtDescription.ReadOnly = Not (vInEdit)
    txtDescription.BackColor = pDefaultColour 

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
      If _SystemDefault.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_SystemDefaultUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_SystemDefaultUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
    Else 
      btnUpdate.Visible = False 
      btnCancel.Visible = False 
      btnEdit.Visible = False 
      btnDelete.Visible = False 
      btnAdd.Visible = False 
    End If 
    
    'set up 'UpdatedSeparately' controls
    'SettingValue 
    If _SystemDefault.SettingValue.ToString() = "" Then btnSettingValueUpdate.Text = CCTextTranslate("Create", _Requester) Else btnSettingValueUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtSettingValue.Enabled = Not (vInEdit) 
    btnSettingValueUpdate.Enabled = Not (vInEdit) 
    btnSettingValueUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_SystemDefaultUpdate, _Requester) 

    'disable or enable any child grids 
    Dim ctrl As Control = Me.GetNextControl(Me, True) 
    Do Until ctrl Is Nothing 
      If ctrl.GetType.Name.StartsWith("ctl") AndAlso ctrl.GetType.Name.EndsWith("Col") Then 
        ctrl.Enabled = Not vInEdit 
      End If 
      ctrl = Me.GetNextControl(ctrl, True) 
    Loop 
 
    RaiseEvent evtControlsRefreshed(vInEdit, _SystemDefault) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _SystemDefault
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtGroup.Text = .Group.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtGroup.MaxLength = 50 
      txtSettingName.Text = .SettingName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSettingName.MaxLength = 50 
      txtSettingValue.Text = .SettingValue.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSettingValue.MaxLength = 4000 
      cboSystemDefaultType.SelectedValue = .SystemDefaultType
      txtSystemDefaultType.Text = cboSystemDefaultType.Text : If cboSystemDefaultType.SelectedValue Is Nothing OrElse cboSystemDefaultType.SelectedValue.ToString() = "UD" Then txtSystemDefaultType.Text = ""    
      txtDescription.Text = .Description.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDescription.MaxLength = 500 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _SystemDefault
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-SystemDefault-ID-090417-0012", _Requester) : Return pFault 
      .Group = txtGroup.Text 
      .SettingName = txtSettingName.Text 
      .SystemDefaultType = CType(cboSystemDefaultType.SelectedValue, clsEnums.enmSystemDefaultType)
      .Description = txtDescription.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-SystemDefault-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csSystemDefault.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-SystemDefault-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_SystemDefault, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _SystemDefault.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the SystemDefault collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.c_SystemDefaultDefaultByID) 
      RaiseEvent evtUpdated(csSystemDefault.enmUpdateType.Standard, _SystemDefault) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_SystemDefault_evtAfterUpdate 
  Private Sub _SystemDefault_evtAfterUpdate() Handles _SystemDefault.evtAfterUpdate, _SystemDefault.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_SystemDefault) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _SystemDefault = New csSystemDefault() 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_SystemDefault) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_SystemDefault, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _SystemDefault.Group & "_" & _SystemDefault.SettingName & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _SystemDefault.ID 
    pFault = _SystemDefault.Delete(_Requester) 
    If pFault.isOK = True Then 
      _SystemDefault = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
  'SeparateUpdates 
  Private Sub btnSettingValueUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingValueUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToSettingValue As Boolean = False 
    RaiseEvent evtSeparateEdit(csSystemDefault.enmUpdateType.SettingValue, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToSettingValue) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then 
        If pAppendTextToSettingValue Then 
          pPrompt = "Add a new Setting Value" 
        Else 
          pPrompt = "Write a Setting Value" 
        End If 
      End If 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.MultiLineTextBox 
      If pAppendTextToSettingValue Then 
        frmUpdateField.DialogueInitialValue = "" 
      Else 
        frmUpdateField.DialogueInitialValue = _SystemDefault.SettingValue 
      End If 
      frmUpdateField.DialoguePrompt = pPrompt 
      Do 
        frmUpdateField.ShowDialog() 
        If frmUpdateField.DialogResult = DialogResult.OK Then 
          Try 
            pNewValue = frmUpdateField.DialogueReturnValue.ToString() 
            pSucceeded = True 
          Catch ex As Exception 
            pSucceeded = False 
            frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
          End Try 
        Else 
          Exit Sub 
        End If 
      Loop Until pSucceeded = True 
    End If 
 
    Cursor = Cursors.WaitCursor 
    If pAppendTextToSettingValue Then pNewValue = ccHelper.PrefixToComment(pNewValue, _SystemDefault.SettingValue, _Requester) 
    pFault = New clsFault 
    Try 
      If pNewValue = _SystemDefault.SettingValue Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _SystemDefault.UpdateSettingValue(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the SystemDefault 
      pFault = _SystemDefault.GetByID(_SystemDefault.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csSystemDefault.enmUpdateType.SettingValue, _SystemDefault) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  'History 
  Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    'Create the AuditIndexed object 
    Dim pAuditIndexedCol As New csAuditIndexedCol 
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("c_SystemDefault", _SystemDefault.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _SystemDefault.DateAdded 
    pAuditIndexed.TableName = "SystemDefault" 
    pAuditIndexed.RowID = _SystemDefault.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'System Default'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlc_SystemDefault_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the SystemDefault to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pSystemDefault As csSystemDefault = _SystemDefault 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pSystemDefault.ToCSV) 
        Else 
          Clipboard.SetText(pSystemDefault.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The SystemDefault is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_SystemDefault_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Private Sub ctlc_SystemDefault_evtSeparateEdit(vPropertyName As csSystemDefault.enmUpdateType, ByRef rNewValue As String, ByRef rUseNewValue As Boolean, ByRef rCancelUpdate As Boolean, ByRef rNewPrompt As String, ByRef rAppendText As Boolean) Handles Me.evtSeparateEdit 
    If vPropertyName = csSystemDefault.enmUpdateType.SettingValue Then 
      Dim pFault As New clsFault 
 
 
      Dim pSucceeded As Boolean = False 
      Dim pPrompt As String = $"Assign a new {_SystemDefault.SettingName}" 
      If _SystemDefault.SystemDefaultType = clsEnums.enmSystemDefaultType.Bit Then 
        frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.CheckBox 
        If _SystemDefault.SettingValue = "1" Then 
          frmUpdateField.DialogueInitialValue = True 
        Else 
          frmUpdateField.DialogueInitialValue = False 
        End If 
      ElseIf _SystemDefault.SystemDefaultType = clsEnums.enmSystemDefaultType.Enum Then  
        Dim pAvailableOptions As New clsComboList  
        pFault = pAvailableOptions.FillEnums(clsEnums.TranslateEnmEnum(_SystemDefault.SettingName), _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : rCancelUpdate = True : Exit Sub  
 
        Dim pSelectedOption As New clsComboListMember 
        pSelectedOption = pAvailableOptions.FindByText(_SystemDefault.SettingValue) 
 
        frmUpdateField.ListOptions = pAvailableOptions  
        frmUpdateField.Requester = _Requester  
        frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.ComboBox 
        frmUpdateField.DialogueInitialValue = pSelectedOption 
      ElseIf _SystemDefault.SystemDefaultType = clsEnums.enmSystemDefaultType.Encrypted Then 
        frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.MultiLineTextBox 
        frmUpdateField.DialogueInitialValue = "" 
      Else  
        frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.MultiLineTextBox  
        frmUpdateField.DialogueInitialValue = _SystemDefault.SettingValue  
      End If  
      frmUpdateField.DialoguePrompt = pPrompt  
      Do  
        frmUpdateField.ShowDialog()  
        If frmUpdateField.DialogResult = DialogResult.OK Then  
          Try 
            If _SystemDefault.SystemDefaultType = clsEnums.enmSystemDefaultType.Enum Then 
              rNewValue = frmUpdateField.DialogueReturnValue.ToString 
            ElseIf _SystemDefault.SystemDefaultType = clsEnums.enmSystemDefaultType.Bit Then 
              If CBool(frmUpdateField.DialogueReturnValue) = True Then 
                rNewValue = "1" 
              Else 
                rNewValue = "0" 
              End If 
            Else 
              rNewValue = frmUpdateField.DialogueReturnValue.ToString 
            End If 
            pSucceeded = True  
          Catch ex As Exception  
            pSucceeded = False  
          End Try  
        Else  
          rCancelUpdate = True  
          Exit Sub  
        End If  
      Loop Until pSucceeded = True  
  
      rUseNewValue = True  
  
    End If  
  End Sub  
  Private Sub ccctlc_SystemDefault_evtLoaded() Handles Me.evtLoaded 
    lblTitle.Text = "Setting: " & _SystemDefault.Group & " - " & _SystemDefault.SettingName 
  End Sub 
  Private Sub ccctlc_SystemDefault_evtControlsRefreshed(vInEdit As Boolean, vSystemDefault As csSystemDefault) Handles Me.evtControlsRefreshed 
    If _SystemDefault.SystemDefaultType = clsEnums.enmSystemDefaultType.Encrypted Then 
      txtSettingValue.Text = "*************" 
    End If 
  End Sub 
 

  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlSystemDefault_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
