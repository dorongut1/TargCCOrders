Public Class ctlc_MFA
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csMFA.enmUpdateType) 
  Public Event evtAdd(ByVal vMFA As csMFA) 
  Public Event evtBeforeUpdate(ByVal vMFA As csMFA, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csMFA.enmUpdateType, ByVal vMFA As csMFA) 
  Public Event evtCancelledEdit(ByVal vMFA As csMFA) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vMFA As csMFA) 
  
  Public Event evtParentChosen(ByVal vParentName As csMFA.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csMFA.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csMFA.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csMFA.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csMFA.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csMFA.enmParentProperty) 
      _EnableParentLinks.Add(csMFA.enmParentProperty.User) 
 
    End Sub 
  End Class 
 
  Private WithEvents _MFA As csMFA

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlMFA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboUILang.Size = txtUILang.Size
    cboUILang.Location = txtUILang.Location
    dtpWhenCreated.Size = txtWhenCreated.Size
    dtpWhenCreated.Location = txtWhenCreated.Location
    dtpWhenAccessed.Size = txtWhenAccessed.Size
    dtpWhenAccessed.Location = txtWhenAccessed.Location
    dtpWhenExpires.Size = txtWhenExpires.Size
    dtpWhenExpires.Location = txtWhenExpires.Location
    cboUser.Size = txtUser.Size
    cboUser.Location = txtUser.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vMFAID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pMFA As New csMFA(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vMFAID <> 0 Then 
      pFault = pMFA.GetByID(vMFAID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pMFA) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rMFA As csMFA, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rMFA)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rMFA As csMFA) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _MFA = rMFA 

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
 
    If cboUILang.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboUILang() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboUser() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rMFA"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rMFA As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rMFA.GetType.Name = "csMFA" Then 
      ctlMFA_Load(Nothing, Nothing) 
      Dim pMFA As csMFA = CType(rMFA, csMFA) 
      Return LoadControl(pMFA) 
    Else 
      Dim pMFAID As Long = CType(rMFA, Long) 
      Return LoadControl(pMFAID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "CellOrEmail", _Requester) 
    If pStrg <> "" Then lblCellOrEmail.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "ProtectedFunction", _Requester) 
    If pStrg <> "" Then lblProtectedFunction.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "CodeHashed", _Requester) 
    If pStrg <> "" Then lblCodeHashed.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "AttemptNo", _Requester) 
    If pStrg <> "" Then lblAttemptNo.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "IsSuccessful", _Requester) 
    If pStrg <> "" Then lblIsSuccessful.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "LastAccessingIP", _Requester) 
    If pStrg <> "" Then lblLastAccessingIP.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "LastAccessingCountry", _Requester) 
    If pStrg <> "" Then lblLastAccessingCountry.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "UILang", _Requester) 
    If pStrg <> "" Then lblUILang.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "WhenCreated", _Requester) 
    If pStrg <> "" Then lblWhenCreated.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "WhenAccessed", _Requester) 
    If pStrg <> "" Then lblWhenAccessed.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "WhenExpires", _Requester) 
    If pStrg <> "" Then lblWhenExpires.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "Details", _Requester) 
    If pStrg <> "" Then lblDetails.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "User", _Requester) 
    If pStrg <> "" Then lblUser.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [MFA]() As csMFA
    Get 
      Return _MFA 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboUILang() As clsFault
    Dim pFault As New clsFault
 
    'If cboUILang.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pUILanges As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csMFA.enmParentProperty.UILang, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pUILanges.FillEnums(clsEnums.enmEnum.Language, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pUILanges = pTestCol
    End If
    
    pUILanges.Remove(pUILanges.FindByKey(clsEnums.enmLanguage.UD))
    pUILanges.SortByText()
    pUILanges.AddToTop(clsEnums.enmLanguage.UD, GetChoose(_Requester))

    With cboUILang
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pUILanges
    End With

    cboUILang.SelectedValue = _MFA.UILang 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboUser() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_UserDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csMFA.enmParentProperty.User, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
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
    
    If _MFA.UserID > 0 Then cboUser.ValueSelect(_MFA.UserID) Else cboUser.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboUILang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUILang.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmLanguage = CType(cboUILang.SelectedValue, clsEnums.enmLanguage) 
    RaiseEvent evtCboSelectedIndexChanged(csMFA.enmParentProperty.UILang, pEnum.ToString) 
  End Sub 
  Private Sub cboUser_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboUser.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csMFA.enmParentProperty.User, pUniqueCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csMFA.enmParentProperty = csMFA.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csMFA.enmParentProperty.User) = csMFA.enmParentProperty.User Then 
      lblUser.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    txtCellOrEmail.ReadOnly = Not (vInEdit)
    txtCellOrEmail.BackColor = pDefaultColour 
    txtProtectedFunction.ReadOnly = Not (vInEdit)
    txtProtectedFunction.BackColor = pDefaultColour 
    txtCodeHashed.ReadOnly = Not (vInEdit)
    txtCodeHashed.BackColor = pDefaultColour 
    txtAttemptNo.ReadOnly = Not (vInEdit)
    txtAttemptNo.BackColor = pDefaultColour 
    chkIsSuccessful.Enabled = True
    txtLastAccessingIP.ReadOnly = Not (vInEdit)
    txtLastAccessingIP.BackColor = pDefaultColour 
    txtLastAccessingCountry.ReadOnly = Not (vInEdit)
    txtLastAccessingCountry.BackColor = pDefaultColour 
    txtUILang.ReadOnly = True
    txtUILang.Visible = Not (vInEdit)
    txtUILang.BackColor = pReadonlyColour 
    txtUILang.ForeColor = SetForeColor(vInEdit) 
    cboUILang.Visible = vInEdit
    dtpWhenCreated.Visible = vInEdit
    txtWhenCreated.Visible = Not (vInEdit)
    txtWhenCreated.BackColor = pReadonlyColour 
    txtWhenCreated.ForeColor = SetForeColor(vInEdit) 
    txtWhenCreated.ReadOnly = True
    dtpWhenAccessed.Visible = vInEdit
    txtWhenAccessed.Visible = Not (vInEdit)
    txtWhenAccessed.BackColor = pReadonlyColour 
    txtWhenAccessed.ForeColor = SetForeColor(vInEdit) 
    txtWhenAccessed.ReadOnly = True
    dtpWhenExpires.Visible = vInEdit
    txtWhenExpires.Visible = Not (vInEdit)
    txtWhenExpires.BackColor = pReadonlyColour 
    txtWhenExpires.ForeColor = SetForeColor(vInEdit) 
    txtWhenExpires.ReadOnly = True
    txtDetails.ReadOnly = Not (vInEdit)
    txtDetails.BackColor = pDefaultColour 
    If vInEdit = False Then 
      txtUser.ReadOnly = True
      txtUser.Visible = True
      txtUser.BackColor = pReadonlyColour
      txtUser.ForeColor = SetForeColor(vInEdit) 
      cboUser.Visible = False 
    Else 
      txtUser.ReadOnly = True
      If txtUser.Parent Is cboUser.Parent Then txtUser.Visible = Not (vInEdit)
      txtUser.BackColor = pReadonlyColour 
      txtUser.ForeColor = SetForeColor(vInEdit) 
      cboUser.Visible = vInEdit
    End If  

    If _LoadParameters.ReadOnly = False Then 
      If _ButtonsMoved = False Then 
        btnUpdate.Visible = True 
        btnCancel.Visible = True 
        btnEdit.Visible = True 
        btnAdd.Visible = True 
        _ButtonsMoved = True 
      End If 
      btnUpdate.Visible = vInEdit 
      btnCancel.Visible = vInEdit 
      btnUpdate.Top = btnEdit.Top 
      btnCancel.Top = btnEdit.Top 
      If _MFA.ID = 0 Then 
        btnEdit.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_MFAUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_MFAUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
    Else 
      btnUpdate.Visible = False 
      btnCancel.Visible = False 
      btnEdit.Visible = False 
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
 
    If Not (_MFA.IsEmpty) Then 
      btnAdd.Visible = False 
    End If 
 
    RaiseEvent evtControlsRefreshed(vInEdit, _MFA) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _MFA
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtCellOrEmail.Text = .CellOrEmail.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCellOrEmail.MaxLength = 50 
      txtProtectedFunction.Text = .ProtectedFunction.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtProtectedFunction.MaxLength = 50 
      txtCodeHashed.PasswordChar = "*"c 
      txtCodeHashed.UseSystemPasswordChar = True 
      txtCodeHashed.Text = "xxxxxxxx"
      txtAttemptNo.Text = .AttemptNo.ToString(FormatFromTag(txtAttemptNo, "#,##0"))
      chkIsSuccessful.Checked = .IsSuccessful
      txtLastAccessingIP.Text = .LastAccessingIP.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLastAccessingIP.MaxLength = 50 
      txtLastAccessingCountry.Text = .LastAccessingCountry.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLastAccessingCountry.MaxLength = 5 
      cboUILang.SelectedValue = .UILang
      txtUILang.Text = cboUILang.Text : If cboUILang.SelectedValue Is Nothing OrElse cboUILang.SelectedValue.ToString() = "UD" Then txtUILang.Text = ""    
      If .WhenCreated < dtpWhenCreated.MinDate Then dtpWhenCreated.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpWhenCreated.Value = .WhenCreated.LocalDateTime
      dtpWhenCreated.CustomFormat = FormatFromTag(txtWhenCreated, "dd-MM-yyyy HH:mm:ss") 
      dtpWhenCreated.Value = DateTime.ParseExact(dtpWhenCreated.Value.ToString(dtpWhenCreated.CustomFormat), dtpWhenCreated.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .WhenCreated < dtpWhenCreated.MinDate Then dtpWhenCreated.Checked = False Else dtpWhenCreated.Checked = True 
      txtWhenCreated.Text = FormattedDateTimeOffsetFromTag(txtWhenCreated, .WhenCreated) 
      If .WhenAccessed < dtpWhenAccessed.MinDate Then dtpWhenAccessed.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpWhenAccessed.Value = .WhenAccessed.LocalDateTime
      dtpWhenAccessed.CustomFormat = FormatFromTag(txtWhenAccessed, "dd-MM-yyyy HH:mm:ss") 
      dtpWhenAccessed.Value = DateTime.ParseExact(dtpWhenAccessed.Value.ToString(dtpWhenAccessed.CustomFormat), dtpWhenAccessed.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .WhenAccessed < dtpWhenAccessed.MinDate Then dtpWhenAccessed.Checked = False Else dtpWhenAccessed.Checked = True 
      txtWhenAccessed.Text = FormattedDateTimeOffsetFromTag(txtWhenAccessed, .WhenAccessed) 
      If .WhenExpires < dtpWhenExpires.MinDate Then dtpWhenExpires.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpWhenExpires.Value = .WhenExpires.LocalDateTime
      dtpWhenExpires.CustomFormat = FormatFromTag(txtWhenExpires, "dd-MM-yyyy HH:mm:ss") 
      dtpWhenExpires.Value = DateTime.ParseExact(dtpWhenExpires.Value.ToString(dtpWhenExpires.CustomFormat), dtpWhenExpires.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .WhenExpires < dtpWhenExpires.MinDate Then dtpWhenExpires.Checked = False Else dtpWhenExpires.Checked = True 
      txtWhenExpires.Text = FormattedDateTimeOffsetFromTag(txtWhenExpires, .WhenExpires) 
      txtDetails.Text = .Details.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDetails.MaxLength = 500 
      txtUser.Text = .UserText 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _MFA
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-MFA-ID-090417-0012", _Requester) : Return pFault 
      .CellOrEmail = txtCellOrEmail.Text 
      .ProtectedFunction = txtProtectedFunction.Text 
      If txtCodeHashed.Text <> "xxxxxxxx" Then .CodeHashed = "PleaseHash" & txtCodeHashed.Text 
      If Integer.TryParse(txtAttemptNo.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .AttemptNo) = False Then pFault.LogFreeTextFault(208, ".AttemptNo", txtAttemptNo.Text, "TRGT-MFA-AttemptNo-090417-0013", _Requester) : Return pFault 
      .IsSuccessful = chkIsSuccessful.Checked
      .LastAccessingIP = txtLastAccessingIP.Text 
      .LastAccessingCountry = txtLastAccessingCountry.Text 
      .UILang = CType(cboUILang.SelectedValue, clsEnums.enmLanguage)
      If (dtpWhenCreated.ShowCheckBox AndAlso dtpWhenCreated.Checked = False) OrElse dtpWhenCreated.Value = New Date(1900, 1, 1, 0, 0, 0) Then .WhenCreated = Nothing Else .WhenCreated = dtpWhenCreated.Value
      If (dtpWhenAccessed.ShowCheckBox AndAlso dtpWhenAccessed.Checked = False) OrElse dtpWhenAccessed.Value = New Date(1900, 1, 1, 0, 0, 0) Then .WhenAccessed = Nothing Else .WhenAccessed = dtpWhenAccessed.Value
      If (dtpWhenExpires.ShowCheckBox AndAlso dtpWhenExpires.Checked = False) OrElse dtpWhenExpires.Value = New Date(1900, 1, 1, 0, 0, 0) Then .WhenExpires = Nothing Else .WhenExpires = dtpWhenExpires.Value
      .Details = txtDetails.Text 
      If cboUser.SelectedItem Is Nothing OrElse cboUser.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .UserID = 0 
      Else 
        Dim pUserID As Long = CType(cboUser.SelectedItem, clsComboListMember).KeyLong 
        If pUserID = -1 Then .UserID = 0 Else .UserID = pUserID 
      End If 
    End With
    pFault.SetOK() 
    Return pFault 
  End Function
  
  'Handle one way encrypted textboxes
  Private Sub txtCodeHashed_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodeHashed.KeyDown 
    If txtCodeHashed.Text = "xxxxxxxx" And btnUpdate.Visible = True Then 
      txtCodeHashed.PasswordChar = Nothing 
      txtCodeHashed.UseSystemPasswordChar = False 
      txtCodeHashed.Text = "" 
      txtCodeHashed.Text = ChrW(e.KeyValue) 
      txtCodeHashed.SelectionStart = 1 
    End If 
  End Sub 
  
  'check control data validity 
  Private Sub txtID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtID.Text 
    Dim pTest As Long 
 
    If txtID.Text = "" Then Exit Sub 
    If txtID.Text = txtID.Name Then Exit Sub 
 
    If Long.TryParse(txtID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-MFA-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtAttemptNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAttemptNo.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtAttemptNo.Text 
    Dim pTest As Integer 
 
    If txtAttemptNo.Text = "" Then Exit Sub 
    If txtAttemptNo.Text = txtAttemptNo.Name Then Exit Sub 
 
    If Integer.TryParse(txtAttemptNo.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-MFA-AttemptNo-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csMFA.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-MFA-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_MFA, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _MFA.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the MFA collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.c_MFADefaultByID) 
      RaiseEvent evtUpdated(csMFA.enmUpdateType.Standard, _MFA) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_MFA_evtAfterUpdate 
  Private Sub _MFA_evtAfterUpdate() Handles _MFA.evtAfterUpdate, _MFA.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_MFA) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _MFA = New csMFA(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_MFA) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub

  'Ensure Read-Only
  Private Sub chkIsSuccessful_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsSuccessful.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkIsSuccessful.Checked = _MFA.IsSuccessful
    End If
  End Sub

  'Now the Parents
  Private Sub lblUser_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.DoubleClick 
    If _MFA.UserID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csMFA.enmParentProperty.User) = csMFA.enmParentProperty.User Then 
      If _MFA.UserID <> 0 Then RaiseEvent evtParentChosen(csMFA.enmParentProperty.User, _MFA.UserID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "User Detail" 
      fPopup.LoadControl("ctlc_User", _MFA.UserID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblUser_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csMFA.enmParentProperty.User) <> csMFA.enmParentProperty.User Then Exit Sub 
    lblUser.ForeColor = Color.Brown 
    'lblUser.Font = New Font(lblUser.Font.Name, lblUser.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblUser.BackColor = ccHelper.InvertColour(lblUser.ForeColor) 'did this instead 
    lblUser.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblUser_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csMFA.enmParentProperty.User) <> csMFA.enmParentProperty.User Then Exit Sub 
    lblUser.ForeColor = Color.Brown 
    'lblUser.Font = New Font(lblUser.Font.Name, lblUser.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblUser.BackColor = Me.BackColor 'did this instead 
    lblUser.Cursor = Cursors.Default 
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("c_MFA", _MFA.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _MFA.DateAdded 
    pAuditIndexed.TableName = "MFA" 
    pAuditIndexed.RowID = _MFA.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'MFA'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlc_MFA_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the MFA to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pMFA As csMFA = _MFA 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pMFA.ToCSV) 
        Else 
          Clipboard.SetText(pMFA.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The MFA is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_MFA_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlMFA_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
