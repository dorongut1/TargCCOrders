Public Class ctlc_ObjectTranslation
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csObjectTranslation.enmUpdateType) 
  Public Event evtAdd(ByVal vObjectTranslation As csObjectTranslation) 
  Public Event evtBeforeUpdate(ByVal vObjectTranslation As csObjectTranslation, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csObjectTranslation.enmUpdateType, ByVal vObjectTranslation As csObjectTranslation) 
  Public Event evtBeforeDelete(ByVal vObjectTranslation As csObjectTranslation, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vObjectTranslationID As Long) 
  Public Event evtCancelledEdit(ByVal vObjectTranslation As csObjectTranslation) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vObjectTranslation As csObjectTranslation) 
  
  Public Event evtParentChosen(ByVal vParentName As csObjectTranslation.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csObjectTranslation.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csObjectTranslation.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csObjectTranslation.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csObjectTranslation.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csObjectTranslation.enmParentProperty) 
      _EnableParentLinks.Add(csObjectTranslation.enmParentProperty.ObjectToTranslate) 
 
    End Sub 
  End Class 
 
  Private WithEvents _ObjectTranslation As csObjectTranslation

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlObjectTranslation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboObjectToTranslate.Size = txtObjectToTranslate.Size
    cboObjectToTranslate.Location = txtObjectToTranslate.Location
    cboLanguage.Size = txtLanguage.Size
    cboLanguage.Location = txtLanguage.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vObjectTranslationID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pObjectTranslation As New csObjectTranslation(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vObjectTranslationID <> 0 Then 
      pFault = pObjectTranslation.GetByID(vObjectTranslationID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pObjectTranslation) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rObjectTranslation As csObjectTranslation, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rObjectTranslation)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rObjectTranslation As csObjectTranslation) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _ObjectTranslation = rObjectTranslation 

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
 
    If cboLanguage.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.c_ObjectToTranslateDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboLanguage() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboObjectToTranslate() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rObjectTranslation"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rObjectTranslation As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rObjectTranslation.GetType.Name = "csObjectTranslation" Then 
      ctlObjectTranslation_Load(Nothing, Nothing) 
      Dim pObjectTranslation As csObjectTranslation = CType(rObjectTranslation, csObjectTranslation) 
      Return LoadControl(pObjectTranslation) 
    Else 
      Dim pObjectTranslationID As Long = CType(rObjectTranslation, Long) 
      Return LoadControl(pObjectTranslationID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_ObjectTranslation", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_ObjectTranslation", "ObjectToTranslate", _Requester) 
    If pStrg <> "" Then lblObjectToTranslate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_ObjectTranslation", "Instance", _Requester) 
    If pStrg <> "" Then lblInstance.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_ObjectTranslation", "DefaultText", _Requester) 
    If pStrg <> "" Then lblDefaultText.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_ObjectTranslation", "Language", _Requester) 
    If pStrg <> "" Then lblLanguage.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_ObjectTranslation", "Text", _Requester) 
    If pStrg <> "" Then lblText.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_ObjectTranslation", "InstanceUniqueText", _Requester) 
    If pStrg <> "" Then lblInstanceUniqueText.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [ObjectTranslation]() As csObjectTranslation
    Get 
      Return _ObjectTranslation 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboObjectToTranslate() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_ObjectToTranslateDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csObjectTranslation.enmParentProperty.ObjectToTranslate, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboObjectToTranslate.MakeSmart() Else cboObjectToTranslate.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboObjectToTranslate.LoadControl(pComboList, pPrompt) 
    Else 
      cboObjectToTranslate.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _ObjectTranslation.ObjectToTranslateID > 0 Then cboObjectToTranslate.ValueSelect(_ObjectTranslation.ObjectToTranslateID) Else cboObjectToTranslate.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboLanguage() As clsFault
    Dim pFault As New clsFault
 
    'If cboLanguage.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pLanguagees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csObjectTranslation.enmParentProperty.Language, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pLanguagees.FillEnums(clsEnums.enmEnum.Language, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pLanguagees = pTestCol
    End If
    
    pLanguagees.Remove(pLanguagees.FindByKey(clsEnums.enmLanguage.UD))
    pLanguagees.SortByText()
    pLanguagees.AddToTop(clsEnums.enmLanguage.UD, GetChoose(_Requester))

    With cboLanguage
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pLanguagees
    End With

    cboLanguage.SelectedValue = _ObjectTranslation.Language 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboObjectToTranslate_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboObjectToTranslate.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csObjectTranslation.enmParentProperty.ObjectToTranslate, pUniqueCode) 
  End Sub 
  Private Sub cboLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLanguage.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmLanguage = CType(cboLanguage.SelectedValue, clsEnums.enmLanguage) 
    RaiseEvent evtCboSelectedIndexChanged(csObjectTranslation.enmParentProperty.Language, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csObjectTranslation.enmParentProperty = csObjectTranslation.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csObjectTranslation.enmParentProperty.ObjectToTranslate) = csObjectTranslation.enmParentProperty.ObjectToTranslate Then 
      lblObjectToTranslate.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtObjectToTranslate.ReadOnly = True
      txtObjectToTranslate.Visible = True
      txtObjectToTranslate.BackColor = pReadonlyColour
      txtObjectToTranslate.ForeColor = SetForeColor(vInEdit) 
      cboObjectToTranslate.Visible = False 
    Else 
      txtObjectToTranslate.ReadOnly = True
      txtObjectToTranslate.Visible = Not (vInEdit)
      txtObjectToTranslate.BackColor = pReadonlyColour 
      txtObjectToTranslate.ForeColor = SetForeColor(vInEdit) 
      cboObjectToTranslate.Visible = vInEdit
    End If  
    txtInstance.ReadOnly = Not (vInEdit)
    txtInstance.BackColor = pDefaultColour 
    txtDefaultText.ReadOnly = True 
    txtDefaultText.BackColor = pReadonlyColour 
    txtDefaultText.ForeColor = SetForeColor(vInEdit) 
    txtLanguage.ReadOnly = True
    txtLanguage.Visible = Not (vInEdit)
    txtLanguage.BackColor = pReadonlyColour 
    txtLanguage.ForeColor = SetForeColor(vInEdit) 
    cboLanguage.Visible = vInEdit
    txtText.ReadOnly = Not (vInEdit)
    txtText.BackColor = pDefaultColour 
    txtInstanceUniqueText.ReadOnly = Not (vInEdit)
    txtInstanceUniqueText.BackColor = pDefaultColour 

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
      If _ObjectTranslation.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_ObjectTranslationUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_ObjectTranslationDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_ObjectTranslationUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _ObjectTranslation) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _ObjectTranslation
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtObjectToTranslate.Text = .ObjectToTranslateText 
      txtInstance.Text = .Instance.ToString(FormatFromTag(txtInstance, "#,##0"))
      txtDefaultText.Text = .DefaultText.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      cboLanguage.SelectedValue = .Language
      txtLanguage.Text = cboLanguage.Text : If cboLanguage.SelectedValue Is Nothing OrElse cboLanguage.SelectedValue.ToString() = "UD" Then txtLanguage.Text = ""    
      txtText.Text = .Text.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtInstanceUniqueText.Text = .InstanceUniqueText.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtInstanceUniqueText.MaxLength = 500 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _ObjectTranslation
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-ObjectTranslation-ID-090417-0012", _Requester) : Return pFault 
      If cboObjectToTranslate.SelectedItem Is Nothing OrElse cboObjectToTranslate.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .ObjectToTranslateID = 0 
      Else 
        Dim pObjectToTranslateID As Long = CType(cboObjectToTranslate.SelectedItem, clsComboListMember).KeyLong 
        If pObjectToTranslateID = -1 Then .ObjectToTranslateID = 0 Else .ObjectToTranslateID = pObjectToTranslateID 
      End If 
      If Long.TryParse(txtInstance.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .Instance) = False Then pFault.LogFreeTextFault(208, ".Instance", txtInstance.Text, "TRGT-ObjectTranslation-Instance-090417-0012", _Requester) : Return pFault 
      .Language = CType(cboLanguage.SelectedValue, clsEnums.enmLanguage)
      .Text = txtText.Text 
      .InstanceUniqueText = txtInstanceUniqueText.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-ObjectTranslation-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtInstance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInstance.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtInstance.Text 
    Dim pTest As Long 
 
    If txtInstance.Text = "" Then Exit Sub 
    If txtInstance.Text = txtInstance.Name Then Exit Sub 
 
    If Long.TryParse(txtInstance.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-ObjectTranslation-Instance-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csObjectTranslation.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-ObjectTranslation-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_ObjectTranslation, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _ObjectTranslation.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      RaiseEvent evtUpdated(csObjectTranslation.enmUpdateType.Standard, _ObjectTranslation) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_ObjectTranslation_evtAfterUpdate 
  Private Sub _ObjectTranslation_evtAfterUpdate() Handles _ObjectTranslation.evtAfterUpdate, _ObjectTranslation.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_ObjectTranslation) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _ObjectTranslation = New csObjectTranslation(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_ObjectTranslation) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_ObjectTranslation, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete the row with an ID of '" & _ObjectTranslation.ID.ToString & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _ObjectTranslation.ID 
    pFault = _ObjectTranslation.Delete(_Requester) 
    If pFault.isOK = True Then 
      _ObjectTranslation = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblObjectToTranslate_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblObjectToTranslate.DoubleClick 
    If _ObjectTranslation.ObjectToTranslateID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csObjectTranslation.enmParentProperty.ObjectToTranslate) = csObjectTranslation.enmParentProperty.ObjectToTranslate Then 
      If _ObjectTranslation.ObjectToTranslateID <> 0 Then RaiseEvent evtParentChosen(csObjectTranslation.enmParentProperty.ObjectToTranslate, _ObjectTranslation.ObjectToTranslateID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "ObjectToTranslate Detail" 
      fPopup.LoadControl("ctlc_ObjectToTranslate", _ObjectTranslation.ObjectToTranslateID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblObjectToTranslate_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblObjectToTranslate.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csObjectTranslation.enmParentProperty.ObjectToTranslate) <> csObjectTranslation.enmParentProperty.ObjectToTranslate Then Exit Sub 
    lblObjectToTranslate.ForeColor = Color.Brown 
    'lblObjectToTranslate.Font = New Font(lblObjectToTranslate.Font.Name, lblObjectToTranslate.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblObjectToTranslate.BackColor = ccHelper.InvertColour(lblObjectToTranslate.ForeColor) 'did this instead 
    lblObjectToTranslate.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblObjectToTranslate_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblObjectToTranslate.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csObjectTranslation.enmParentProperty.ObjectToTranslate) <> csObjectTranslation.enmParentProperty.ObjectToTranslate Then Exit Sub 
    lblObjectToTranslate.ForeColor = Color.Brown 
    'lblObjectToTranslate.Font = New Font(lblObjectToTranslate.Font.Name, lblObjectToTranslate.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblObjectToTranslate.BackColor = Me.BackColor 'did this instead 
    lblObjectToTranslate.Cursor = Cursors.Default 
  End Sub 
 
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("c_ObjectTranslation", _ObjectTranslation.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _ObjectTranslation.DateAdded 
    pAuditIndexed.TableName = "ObjectTranslation" 
    pAuditIndexed.RowID = _ObjectTranslation.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Object Translation'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlc_ObjectTranslation_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the ObjectTranslation to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pObjectTranslation As csObjectTranslation = _ObjectTranslation 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pObjectTranslation.ToCSV) 
        Else 
          Clipboard.SetText(pObjectTranslation.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The ObjectTranslation is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_ObjectTranslation_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlObjectTranslation_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
