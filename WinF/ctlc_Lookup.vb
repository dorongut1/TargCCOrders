Public Class ctlc_Lookup
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csLookup.enmUpdateType) 
  Public Event evtAdd(ByVal vLookup As csLookup) 
  Public Event evtBeforeUpdate(ByVal vLookup As csLookup, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csLookup.enmUpdateType, ByVal vLookup As csLookup) 
  Public Event evtBeforeDelete(ByVal vLookup As csLookup, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vLookupID As Long) 
  Public Event evtCancelledEdit(ByVal vLookup As csLookup) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vLookup As csLookup) 
  
  Public Event evtParentChosen(ByVal vParentName As csLookup.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csLookup.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csLookup.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csLookup.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csLookup.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csLookup.enmParentProperty) 
 
    End Sub 
  End Class 
 
  Private WithEvents _Lookup As csLookup

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboParentLookupType.Size = txtParentLookupType.Size
    cboParentLookupType.Location = txtParentLookupType.Location
    cboLookupType.Size = txtLookupType.Size
    cboLookupType.Location = txtLookupType.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vLookupID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pLookup As New csLookup(vIsLocalized:=True) 
    If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then pLookup.OverrideDefaultLanguage(LocalizedTextLanguage) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vLookupID <> 0 Then 
      pFault = pLookup.GetByID(vLookupID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pLookup) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rLookup As csLookup, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rLookup)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rLookup As csLookup) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _Lookup = rLookup 

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
 
    If cboLookupType.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboParentLookupType() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboLookupType() : If pFault.isOK = False Then Return pFault 
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
  ''' <param name="rLookup"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rLookup As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rLookup.GetType.Name = "csLookup" Then 
      ctlLookup_Load(Nothing, Nothing) 
      Dim pLookup As csLookup = CType(rLookup, csLookup) 
      Return LoadControl(pLookup) 
    Else 
      Dim pLookupID As Long = CType(rLookup, Long) 
      Return LoadControl(pLookupID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Lookup", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Lookup", "ParentLookupType", _Requester) 
    If pStrg <> "" Then lblParentLookupType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Lookup", "ParentCode", _Requester) 
    If pStrg <> "" Then lblParentCode.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Lookup", "LookupType", _Requester) 
    If pStrg <> "" Then lblLookupType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Lookup", "Code", _Requester) 
    If pStrg <> "" Then lblCode.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Lookup", "Text", _Requester) 
    If pStrg <> "" Then lblTextLocalized.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Lookup", "Description", _Requester) 
    If pStrg <> "" Then lblDescriptionLocalized.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [Lookup]() As csLookup
    Get 
      Return _Lookup 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboParentLookupType() As clsFault
    Dim pFault As New clsFault
 
    'If cboParentLookupType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pParentLookupTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLookup.enmParentProperty.ParentLookupType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pParentLookupTypees.FillEnums(clsEnums.enmEnum.Lookup, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pParentLookupTypees = pTestCol
    End If
    
    pParentLookupTypees.Remove(pParentLookupTypees.FindByKey(clsEnums.enmLookup.UD))
    pParentLookupTypees.SortByText()
    pParentLookupTypees.AddToTop(clsEnums.enmLookup.UD, GetChoose(_Requester))

    With cboParentLookupType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pParentLookupTypees
    End With

    cboParentLookupType.SelectedValue = _Lookup.ParentLookupType 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboLookupType() As clsFault
    Dim pFault As New clsFault
 
    'If cboLookupType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pLookupTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLookup.enmParentProperty.LookupType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pLookupTypees.FillEnums(clsEnums.enmEnum.Lookup, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pLookupTypees = pTestCol
    End If
    
    pLookupTypees.Remove(pLookupTypees.FindByKey(clsEnums.enmLookup.UD))
    pLookupTypees.SortByText()
    pLookupTypees.AddToTop(clsEnums.enmLookup.UD, GetChoose(_Requester))

    With cboLookupType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pLookupTypees
    End With

    cboLookupType.SelectedValue = _Lookup.LookupType 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboParentLookupType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboParentLookupType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmLookup = CType(cboParentLookupType.SelectedValue, clsEnums.enmLookup) 
    RaiseEvent evtCboSelectedIndexChanged(csLookup.enmParentProperty.ParentLookupType, pEnum.ToString) 
  End Sub 
  Private Sub cboLookupType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLookupType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmLookup = CType(cboLookupType.SelectedValue, clsEnums.enmLookup) 
    RaiseEvent evtCboSelectedIndexChanged(csLookup.enmParentProperty.LookupType, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csLookup.enmParentProperty = csLookup.enmParentProperty.UD 
    
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
    txtParentLookupType.ReadOnly = True
    txtParentLookupType.Visible = Not (vInEdit)
    txtParentLookupType.BackColor = pReadonlyColour 
    txtParentLookupType.ForeColor = SetForeColor(vInEdit) 
    cboParentLookupType.Visible = vInEdit
    txtParentCode.ReadOnly = Not (vInEdit)
    txtParentCode.BackColor = pDefaultColour 
    txtLookupType.ReadOnly = True
    txtLookupType.Visible = Not (vInEdit)
    txtLookupType.BackColor = pReadonlyColour 
    txtLookupType.ForeColor = SetForeColor(vInEdit) 
    cboLookupType.Visible = vInEdit
    txtCode.ReadOnly = Not (vInEdit)
    txtCode.BackColor = pDefaultColour 
    txtText.ReadOnly = Not (vInEdit)
    txtText.BackColor = pDefaultColour 
    txtTextLocalized.ReadOnly = Not (vInEdit)
    txtTextLocalized.BackColor = pDefaultColour 
    'Set label
    If lblTextLocalized.Text.EndsWith(" Loc") Then lblTextLocalized.Text = lblTextLocalized.Text.Substring(0, lblTextLocalized.Text.Length - 4) & $" ({_Lookup.LocalizedLanguage})" 
    If lblTextLocalized.Text.EndsWith(")") Then lblTextLocalized.Text = lblTextLocalized.Text.Substring(0, lblTextLocalized.Text.Length - 5) & $" ({_Lookup.LocalizedLanguage})" 
    If _Lookup.LocalizedLanguage = clsEnums.enmLanguage.he Then txtTextLocalized.RightToLeft = RightToLeft.Yes Else txtTextLocalized.RightToLeft = RightToLeft.No 
    txtDescription.ReadOnly = Not (vInEdit)
    txtDescription.BackColor = pDefaultColour 
    txtDescriptionLocalized.ReadOnly = Not (vInEdit)
    txtDescriptionLocalized.BackColor = pDefaultColour 
    'Set label
    If lblDescriptionLocalized.Text.EndsWith(" Loc") Then lblDescriptionLocalized.Text = lblDescriptionLocalized.Text.Substring(0, lblDescriptionLocalized.Text.Length - 4) & $" ({_Lookup.LocalizedLanguage})" 
    If lblDescriptionLocalized.Text.EndsWith(")") Then lblDescriptionLocalized.Text = lblDescriptionLocalized.Text.Substring(0, lblDescriptionLocalized.Text.Length - 5) & $" ({_Lookup.LocalizedLanguage})" 
    If _Lookup.LocalizedLanguage = clsEnums.enmLanguage.he Then txtDescriptionLocalized.RightToLeft = RightToLeft.Yes Else txtDescriptionLocalized.RightToLeft = RightToLeft.No 

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
      If _Lookup.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_LookupUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_LookupDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_LookupUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _Lookup) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _Lookup
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      cboParentLookupType.SelectedValue = .ParentLookupType
      txtParentLookupType.Text = cboParentLookupType.Text : If cboParentLookupType.SelectedValue Is Nothing OrElse cboParentLookupType.SelectedValue.ToString() = "UD" Then txtParentLookupType.Text = ""    
      txtParentCode.Text = .ParentCode.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtParentCode.MaxLength = 50 
      cboLookupType.SelectedValue = .LookupType
      txtLookupType.Text = cboLookupType.Text : If cboLookupType.SelectedValue Is Nothing OrElse cboLookupType.SelectedValue.ToString() = "UD" Then txtLookupType.Text = ""    
      txtCode.Text = .Code.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCode.MaxLength = 50 
      txtText.Text = .Text.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtText.MaxLength = 100 
      txtTextLocalized.Text = .TextLocalized.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtTextLocalized.MaxLength = 100 
      txtDescription.Text = .Description.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDescription.MaxLength = 50 
      txtDescriptionLocalized.Text = .DescriptionLocalized.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDescriptionLocalized.MaxLength = 50 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _Lookup
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-Lookup-ID-090417-0012", _Requester) : Return pFault 
      .ParentLookupType = CType(cboParentLookupType.SelectedValue, clsEnums.enmLookup)
      .ParentCode = txtParentCode.Text 
      .LookupType = CType(cboLookupType.SelectedValue, clsEnums.enmLookup)
      .Code = txtCode.Text 
      .Text = txtText.Text 
      .TextLocalized = txtTextLocalized.Text 
      .Description = txtDescription.Text 
      .DescriptionLocalized = txtDescriptionLocalized.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-Lookup-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csLookup.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-Lookup-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_Lookup, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _Lookup.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the Lookup collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.c_LookupDefaultByID) 
      RaiseEvent evtUpdated(csLookup.enmUpdateType.Standard, _Lookup) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_Lookup_evtAfterUpdate 
  Private Sub _Lookup_evtAfterUpdate() Handles _Lookup.evtAfterUpdate, _Lookup.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_Lookup) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _Lookup = New csLookup(vIsLocalized:=True) 
    If LocalizedTextLanguage <> clsEnums.enmLanguage.UD Then _Lookup.OverrideDefaultLanguage(LocalizedTextLanguage) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_Lookup) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_Lookup, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _Lookup.LookupType.FastToString() & " --> " & _Lookup.Code & " (" & _Lookup.Text & ")" & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _Lookup.ID 
    pFault = _Lookup.Delete(_Requester) 
    If pFault.isOK = True Then 
      _Lookup = Nothing 
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("c_Lookup", _Lookup.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _Lookup.DateAdded 
    pAuditIndexed.TableName = "Lookup" 
    pAuditIndexed.RowID = _Lookup.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Lookup'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlc_Lookup_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the Lookup to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pLookup As csLookup = _Lookup 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pLookup.ToCSV) 
        Else 
          Clipboard.SetText(pLookup.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The Lookup is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_Lookup_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlLookup_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
