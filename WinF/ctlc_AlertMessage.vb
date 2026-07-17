Public Class ctlc_AlertMessage
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csAlertMessage.enmUpdateType) 
  Public Event evtAdd(ByVal vAlertMessage As csAlertMessage) 
  Public Event evtBeforeUpdate(ByVal vAlertMessage As csAlertMessage, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csAlertMessage.enmUpdateType, ByVal vAlertMessage As csAlertMessage) 
  Public Event evtBeforeDelete(ByVal vAlertMessage As csAlertMessage, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vAlertMessageID As Long) 
  Public Event evtCancelledEdit(ByVal vAlertMessage As csAlertMessage) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vAlertMessage As csAlertMessage) 
  
  Public Event evtParentChosen(ByVal vParentName As csAlertMessage.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csAlertMessage.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csAlertMessage.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csAlertMessage.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csAlertMessage.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csAlertMessage.enmParentProperty) 
 
    End Sub 
  End Class 
 
  Private WithEvents _AlertMessage As csAlertMessage

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlAlertMessage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboType.Size = txtType.Size
    cboType.Location = txtType.Location
    cboSeverity.Size = txtSeverity.Size
    cboSeverity.Location = txtSeverity.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vAlertMessageID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pAlertMessage As New csAlertMessage(vIsLocalized:=True) 
    If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then pAlertMessage.OverrideDefaultLanguage(LocalizedTextLanguage) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vAlertMessageID <> 0 Then 
      pFault = pAlertMessage.GetByID(vAlertMessageID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pAlertMessage) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rAlertMessage As csAlertMessage, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rAlertMessage)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rAlertMessage As csAlertMessage) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _AlertMessage = rAlertMessage 

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
 
    If cboSeverity.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboType() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboSeverity() : If pFault.isOK = False Then Return pFault 
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
  ''' <param name="rAlertMessage"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rAlertMessage As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rAlertMessage.GetType.Name = "csAlertMessage" Then 
      ctlAlertMessage_Load(Nothing, Nothing) 
      Dim pAlertMessage As csAlertMessage = CType(rAlertMessage, csAlertMessage) 
      Return LoadControl(pAlertMessage) 
    Else 
      Dim pAlertMessageID As Long = CType(rAlertMessage, Long) 
      Return LoadControl(pAlertMessageID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AlertMessage", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AlertMessage", "Number", _Requester) 
    If pStrg <> "" Then lblNumber.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AlertMessage", "Description", _Requester) 
    If pStrg <> "" Then lblDescription.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AlertMessage", "Type", _Requester) 
    If pStrg <> "" Then lblType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AlertMessage", "Severity", _Requester) 
    If pStrg <> "" Then lblSeverity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AlertMessage", "Message", _Requester) 
    If pStrg <> "" Then lblMessageLocalized.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AlertMessage", "Action", _Requester) 
    If pStrg <> "" Then lblActionLocalized.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [AlertMessage]() As csAlertMessage
    Get 
      Return _AlertMessage 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboType() As clsFault
    Dim pFault As New clsFault
 
    'If cboType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csAlertMessage.enmParentProperty.Type, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pTypees.FillEnums(clsEnums.enmEnum.FaultType, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pTypees = pTestCol
    End If
    
    pTypees.Remove(pTypees.FindByKey(clsEnums.enmFaultType.UD))
    pTypees.SortByText()
    pTypees.AddToTop(clsEnums.enmFaultType.UD, GetChoose(_Requester))

    With cboType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pTypees
    End With

    cboType.SelectedValue = _AlertMessage.Type 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboSeverity() As clsFault
    Dim pFault As New clsFault
 
    'If cboSeverity.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pSeverityes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csAlertMessage.enmParentProperty.Severity, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pSeverityes.FillEnums(clsEnums.enmEnum.FaultSeverity, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pSeverityes = pTestCol
    End If
    
    pSeverityes.Remove(pSeverityes.FindByKey(clsEnums.enmFaultSeverity.UD))
    pSeverityes.SortByText()
    pSeverityes.AddToTop(clsEnums.enmFaultSeverity.UD, GetChoose(_Requester))

    With cboSeverity
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pSeverityes
    End With

    cboSeverity.SelectedValue = _AlertMessage.Severity 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmFaultType = CType(cboType.SelectedValue, clsEnums.enmFaultType) 
    RaiseEvent evtCboSelectedIndexChanged(csAlertMessage.enmParentProperty.Type, pEnum.ToString) 
  End Sub 
  Private Sub cboSeverity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeverity.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmFaultSeverity = CType(cboSeverity.SelectedValue, clsEnums.enmFaultSeverity) 
    RaiseEvent evtCboSelectedIndexChanged(csAlertMessage.enmParentProperty.Severity, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csAlertMessage.enmParentProperty = csAlertMessage.enmParentProperty.UD 
    
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
    txtNumber.ReadOnly = Not (vInEdit)
    txtNumber.BackColor = pDefaultColour 
    txtDescription.ReadOnly = Not (vInEdit)
    txtDescription.BackColor = pDefaultColour 
    txtType.ReadOnly = True
    txtType.Visible = Not (vInEdit)
    txtType.BackColor = pReadonlyColour 
    txtType.ForeColor = SetForeColor(vInEdit) 
    cboType.Visible = vInEdit
    txtSeverity.ReadOnly = True
    txtSeverity.Visible = Not (vInEdit)
    txtSeverity.BackColor = pReadonlyColour 
    txtSeverity.ForeColor = SetForeColor(vInEdit) 
    cboSeverity.Visible = vInEdit
    txtMessage.ReadOnly = Not (vInEdit)
    txtMessage.BackColor = pDefaultColour 
    txtMessageLocalized.ReadOnly = Not (vInEdit)
    txtMessageLocalized.BackColor = pDefaultColour 
    'Set label
    If lblMessageLocalized.Text.EndsWith(" Loc") Then lblMessageLocalized.Text = lblMessageLocalized.Text.Substring(0, lblMessageLocalized.Text.Length - 4) & $" ({_AlertMessage.LocalizedLanguage})" 
    If lblMessageLocalized.Text.EndsWith(")") Then lblMessageLocalized.Text = lblMessageLocalized.Text.Substring(0, lblMessageLocalized.Text.Length - 5) & $" ({_AlertMessage.LocalizedLanguage})" 
    If _AlertMessage.LocalizedLanguage = clsEnums.enmLanguage.he Then txtMessageLocalized.RightToLeft = RightToLeft.Yes Else txtMessageLocalized.RightToLeft = RightToLeft.No 
    txtAction.ReadOnly = Not (vInEdit)
    txtAction.BackColor = pDefaultColour 
    txtActionLocalized.ReadOnly = Not (vInEdit)
    txtActionLocalized.BackColor = pDefaultColour 
    'Set label
    If lblActionLocalized.Text.EndsWith(" Loc") Then lblActionLocalized.Text = lblActionLocalized.Text.Substring(0, lblActionLocalized.Text.Length - 4) & $" ({_AlertMessage.LocalizedLanguage})" 
    If lblActionLocalized.Text.EndsWith(")") Then lblActionLocalized.Text = lblActionLocalized.Text.Substring(0, lblActionLocalized.Text.Length - 5) & $" ({_AlertMessage.LocalizedLanguage})" 
    If _AlertMessage.LocalizedLanguage = clsEnums.enmLanguage.he Then txtActionLocalized.RightToLeft = RightToLeft.Yes Else txtActionLocalized.RightToLeft = RightToLeft.No 

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
      If _AlertMessage.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_AlertMessageUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_AlertMessageDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_AlertMessageUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _AlertMessage) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _AlertMessage
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtNumber.Text = .Number.ToString(FormatFromTag(txtNumber, "#,##0"))
      txtDescription.Text = .Description.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDescription.MaxLength = 100 
      cboType.SelectedValue = .Type
      txtType.Text = cboType.Text : If cboType.SelectedValue Is Nothing OrElse cboType.SelectedValue.ToString() = "UD" Then txtType.Text = ""    
      cboSeverity.SelectedValue = .Severity
      txtSeverity.Text = cboSeverity.Text : If cboSeverity.SelectedValue Is Nothing OrElse cboSeverity.SelectedValue.ToString() = "UD" Then txtSeverity.Text = ""    
      txtMessage.Text = .Message.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtMessage.MaxLength = 100 
      txtMessageLocalized.Text = .MessageLocalized.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtMessageLocalized.MaxLength = 100 
      txtAction.Text = .Action.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtAction.MaxLength = 100 
      txtActionLocalized.Text = .ActionLocalized.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtActionLocalized.MaxLength = 100 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _AlertMessage
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-AlertMessage-ID-090417-0012", _Requester) : Return pFault 
      If Integer.TryParse(txtNumber.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .Number) = False Then pFault.LogFreeTextFault(208, ".Number", txtNumber.Text, "TRGT-AlertMessage-Number-090417-0013", _Requester) : Return pFault 
      .Description = txtDescription.Text 
      .Type = CType(cboType.SelectedValue, clsEnums.enmFaultType)
      .Severity = CType(cboSeverity.SelectedValue, clsEnums.enmFaultSeverity)
      .Message = txtMessage.Text 
      .MessageLocalized = txtMessageLocalized.Text 
      .Action = txtAction.Text 
      .ActionLocalized = txtActionLocalized.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-AlertMessage-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumber.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtNumber.Text 
    Dim pTest As Integer 
 
    If txtNumber.Text = "" Then Exit Sub 
    If txtNumber.Text = txtNumber.Name Then Exit Sub 
 
    If Integer.TryParse(txtNumber.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-AlertMessage-Number-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csAlertMessage.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-AlertMessage-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_AlertMessage, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _AlertMessage.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the AlertMessage collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.c_AlertMessageDefaultByID) 
      RaiseEvent evtUpdated(csAlertMessage.enmUpdateType.Standard, _AlertMessage) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_AlertMessage_evtAfterUpdate 
  Private Sub _AlertMessage_evtAfterUpdate() Handles _AlertMessage.evtAfterUpdate, _AlertMessage.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_AlertMessage) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _AlertMessage = New csAlertMessage(vIsLocalized:=True) 
    If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _AlertMessage.OverrideDefaultLanguage(LocalizedTextLanguage) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_AlertMessage) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_AlertMessage, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _AlertMessage.Number.ToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _AlertMessage.ID 
    pFault = _AlertMessage.Delete(_Requester) 
    If pFault.isOK = True Then 
      _AlertMessage = Nothing 
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
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  'History 
  Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
 
    Cursor = Cursors.WaitCursor 
 
    'Create the AuditIndexed object 
    Dim pAuditIndexedCol As New csAuditIndexedCol 
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("c_AlertMessage", _AlertMessage.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _AlertMessage.DateAdded 
    pAuditIndexed.TableName = "AlertMessage" 
    pAuditIndexed.RowID = _AlertMessage.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Alert Message'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlc_AlertMessage_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the AlertMessage to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pAlertMessage As csAlertMessage = _AlertMessage 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pAlertMessage.ToCSV) 
        Else 
          Clipboard.SetText(pAlertMessage.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The AlertMessage is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_AlertMessage_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlAlertMessage_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
