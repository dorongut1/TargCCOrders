Public Class ctlc_JobAlertRecipient
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csJobAlertRecipient.enmUpdateType) 
  Public Event evtAdd(ByVal vJobAlertRecipient As csJobAlertRecipient) 
  Public Event evtBeforeUpdate(ByVal vJobAlertRecipient As csJobAlertRecipient, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csJobAlertRecipient.enmUpdateType, ByVal vJobAlertRecipient As csJobAlertRecipient) 
  Public Event evtBeforeDelete(ByVal vJobAlertRecipient As csJobAlertRecipient, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vJobAlertRecipientID As Long) 
  Public Event evtCancelledEdit(ByVal vJobAlertRecipient As csJobAlertRecipient) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vJobAlertRecipient As csJobAlertRecipient) 
  
  Public Event evtParentChosen(ByVal vParentName As csJobAlertRecipient.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csJobAlertRecipient.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csJobAlertRecipient.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csJobAlertRecipient.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csJobAlertRecipient.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csJobAlertRecipient.enmParentProperty) 
      _EnableParentLinks.Add(csJobAlertRecipient.enmParentProperty.Job) 
      _EnableParentLinks.Add(csJobAlertRecipient.enmParentProperty.User) 
 
    End Sub 
  End Class 
 
  Private WithEvents _JobAlertRecipient As csJobAlertRecipient

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlJobAlertRecipient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboJob.Size = txtJob.Size
    cboJob.Location = txtJob.Location
    cboUser.Size = txtUser.Size
    cboUser.Location = txtUser.Location
    cboJobAlertType.Size = txtJobAlertType.Size
    cboJobAlertType.Location = txtJobAlertType.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vJobAlertRecipientID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pJobAlertRecipient As New csJobAlertRecipient(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vJobAlertRecipientID <> 0 Then 
      pFault = pJobAlertRecipient.GetByID(vJobAlertRecipientID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pJobAlertRecipient) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rJobAlertRecipient As csJobAlertRecipient, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rJobAlertRecipient)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rJobAlertRecipient As csJobAlertRecipient) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _JobAlertRecipient = rJobAlertRecipient 

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
 
    If cboJobAlertType.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.c_JobDefaultByID, Cache.enmLevel.Previous) 
      MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboJobAlertType() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboJob() : If pFault.isOK = False Then Return pFault 
    pFault = LoadCboUser() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rJobAlertRecipient"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rJobAlertRecipient As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rJobAlertRecipient.GetType.Name = "csJobAlertRecipient" Then 
      ctlJobAlertRecipient_Load(Nothing, Nothing) 
      Dim pJobAlertRecipient As csJobAlertRecipient = CType(rJobAlertRecipient, csJobAlertRecipient) 
      Return LoadControl(pJobAlertRecipient) 
    Else 
      Dim pJobAlertRecipientID As Long = CType(rJobAlertRecipient, Long) 
      Return LoadControl(pJobAlertRecipientID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_JobAlertRecipient", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_JobAlertRecipient", "Job", _Requester) 
    If pStrg <> "" Then lblJob.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_JobAlertRecipient", "User", _Requester) 
    If pStrg <> "" Then lblUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_JobAlertRecipient", "JobAlertType", _Requester) 
    If pStrg <> "" Then lblJobAlertType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_JobAlertRecipient", "OverrideName", _Requester) 
    If pStrg <> "" Then lblOverrideName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_JobAlertRecipient", "OverrideEmailOrPhone", _Requester) 
    If pStrg <> "" Then lblOverrideEmailOrPhone.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [JobAlertRecipient]() As csJobAlertRecipient
    Get 
      Return _JobAlertRecipient 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboJob() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_JobDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csJobAlertRecipient.enmParentProperty.Job, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboJob.MakeSmart() Else cboJob.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboJob.LoadControl(pComboList, pPrompt) 
    Else 
      cboJob.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _JobAlertRecipient.JobID > 0 Then cboJob.ValueSelect(_JobAlertRecipient.JobID) Else cboJob.ValueSelect(ccHelper.ToLong(-1)) 

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
    RaiseEvent evtOverrideLoadIntelliCombo(csJobAlertRecipient.enmParentProperty.User, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
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
    
    If _JobAlertRecipient.UserID > 0 Then cboUser.ValueSelect(_JobAlertRecipient.UserID) Else cboUser.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboJobAlertType() As clsFault
    Dim pFault As New clsFault
 
    'If cboJobAlertType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pJobAlertTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csJobAlertRecipient.enmParentProperty.JobAlertType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pJobAlertTypees.FillEnums(clsEnums.enmEnum.JobAlertType, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pJobAlertTypees = pTestCol
    End If
    
    pJobAlertTypees.Remove(pJobAlertTypees.FindByKey(clsEnums.enmJobAlertType.UD))
    pJobAlertTypees.SortByText()
    pJobAlertTypees.AddToTop(clsEnums.enmJobAlertType.UD, GetChoose(_Requester))

    With cboJobAlertType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pJobAlertTypees
    End With

    cboJobAlertType.SelectedValue = _JobAlertRecipient.JobAlertType 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboJob_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboJob.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csJobAlertRecipient.enmParentProperty.Job, pUniqueCode) 
  End Sub 
  Private Sub cboUser_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboUser.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csJobAlertRecipient.enmParentProperty.User, pUniqueCode) 
  End Sub 
  Private Sub cboJobAlertType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboJobAlertType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmJobAlertType = CType(cboJobAlertType.SelectedValue, clsEnums.enmJobAlertType) 
    RaiseEvent evtCboSelectedIndexChanged(csJobAlertRecipient.enmParentProperty.JobAlertType, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csJobAlertRecipient.enmParentProperty = csJobAlertRecipient.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csJobAlertRecipient.enmParentProperty.Job) = csJobAlertRecipient.enmParentProperty.Job Then 
      lblJob.ForeColor = Color.Brown 
    End If 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csJobAlertRecipient.enmParentProperty.User) = csJobAlertRecipient.enmParentProperty.User Then 
      lblUser.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtJob.ReadOnly = True
      txtJob.Visible = True
      txtJob.BackColor = pReadonlyColour
      txtJob.ForeColor = SetForeColor(vInEdit) 
      cboJob.Visible = False 
    Else 
      txtJob.ReadOnly = True
      txtJob.Visible = Not (vInEdit)
      txtJob.BackColor = pReadonlyColour 
      txtJob.ForeColor = SetForeColor(vInEdit) 
      cboJob.Visible = vInEdit
    End If  
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
    txtJobAlertType.ReadOnly = True
    txtJobAlertType.Visible = Not (vInEdit)
    txtJobAlertType.BackColor = pReadonlyColour 
    txtJobAlertType.ForeColor = SetForeColor(vInEdit) 
    cboJobAlertType.Visible = vInEdit
    txtOverrideName.ReadOnly = Not (vInEdit)
    txtOverrideName.BackColor = pDefaultColour 
    txtOverrideEmailOrPhone.ReadOnly = Not (vInEdit)
    txtOverrideEmailOrPhone.BackColor = pDefaultColour 

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
      If _JobAlertRecipient.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_JobAlertRecipientUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_JobAlertRecipientDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_JobAlertRecipientUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _JobAlertRecipient) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _JobAlertRecipient
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtJob.Text = .JobText 
      txtUser.Text = .UserText 
      cboJobAlertType.SelectedValue = .JobAlertType
      txtJobAlertType.Text = cboJobAlertType.Text : If cboJobAlertType.SelectedValue Is Nothing OrElse cboJobAlertType.SelectedValue.ToString() = "UD" Then txtJobAlertType.Text = ""    
      txtOverrideName.Text = .OverrideName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtOverrideName.MaxLength = 50 
      txtOverrideEmailOrPhone.Text = .OverrideEmailOrPhone.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtOverrideEmailOrPhone.MaxLength = 50 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _JobAlertRecipient
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-JobAlertRecipient-ID-090417-0012", _Requester) : Return pFault 
      If cboJob.SelectedItem Is Nothing OrElse cboJob.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .JobID = 0 
      Else 
        Dim pJobID As Long = CType(cboJob.SelectedItem, clsComboListMember).KeyLong 
        If pJobID = -1 Then .JobID = 0 Else .JobID = pJobID 
      End If 
      If cboUser.SelectedItem Is Nothing OrElse cboUser.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .UserID = 0 
      Else 
        Dim pUserID As Long = CType(cboUser.SelectedItem, clsComboListMember).KeyLong 
        If pUserID = -1 Then .UserID = 0 Else .UserID = pUserID 
      End If 
      .JobAlertType = CType(cboJobAlertType.SelectedValue, clsEnums.enmJobAlertType)
      .OverrideName = txtOverrideName.Text 
      .OverrideEmailOrPhone = txtOverrideEmailOrPhone.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-JobAlertRecipient-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csJobAlertRecipient.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-JobAlertRecipient-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_JobAlertRecipient, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _JobAlertRecipient.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      RaiseEvent evtUpdated(csJobAlertRecipient.enmUpdateType.Standard, _JobAlertRecipient) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_JobAlertRecipient_evtAfterUpdate 
  Private Sub _JobAlertRecipient_evtAfterUpdate() Handles _JobAlertRecipient.evtAfterUpdate, _JobAlertRecipient.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_JobAlertRecipient) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _JobAlertRecipient = New csJobAlertRecipient(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_JobAlertRecipient) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_JobAlertRecipient, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete the row with an ID of '" & _JobAlertRecipient.ID.ToString & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _JobAlertRecipient.ID 
    pFault = _JobAlertRecipient.Delete(_Requester) 
    If pFault.isOK = True Then 
      _JobAlertRecipient = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblJob_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblJob.DoubleClick 
    If _JobAlertRecipient.JobID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csJobAlertRecipient.enmParentProperty.Job) = csJobAlertRecipient.enmParentProperty.Job Then 
      If _JobAlertRecipient.JobID <> 0 Then RaiseEvent evtParentChosen(csJobAlertRecipient.enmParentProperty.Job, _JobAlertRecipient.JobID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "Job Detail" 
      fPopup.LoadControl("ctlc_Job", _JobAlertRecipient.JobID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblJob_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblJob.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csJobAlertRecipient.enmParentProperty.Job) <> csJobAlertRecipient.enmParentProperty.Job Then Exit Sub 
    lblJob.ForeColor = Color.Brown 
    'lblJob.Font = New Font(lblJob.Font.Name, lblJob.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblJob.BackColor = ccHelper.InvertColour(lblJob.ForeColor) 'did this instead 
    lblJob.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblJob_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblJob.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csJobAlertRecipient.enmParentProperty.Job) <> csJobAlertRecipient.enmParentProperty.Job Then Exit Sub 
    lblJob.ForeColor = Color.Brown 
    'lblJob.Font = New Font(lblJob.Font.Name, lblJob.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblJob.BackColor = Me.BackColor 'did this instead 
    lblJob.Cursor = Cursors.Default 
  End Sub 
 
  Private Sub lblUser_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.DoubleClick 
    If _JobAlertRecipient.UserID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csJobAlertRecipient.enmParentProperty.User) = csJobAlertRecipient.enmParentProperty.User Then 
      If _JobAlertRecipient.UserID <> 0 Then RaiseEvent evtParentChosen(csJobAlertRecipient.enmParentProperty.User, _JobAlertRecipient.UserID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "User Detail" 
      fPopup.LoadControl("ctlc_User", _JobAlertRecipient.UserID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblUser_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csJobAlertRecipient.enmParentProperty.User) <> csJobAlertRecipient.enmParentProperty.User Then Exit Sub 
    lblUser.ForeColor = Color.Brown 
    'lblUser.Font = New Font(lblUser.Font.Name, lblUser.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblUser.BackColor = ccHelper.InvertColour(lblUser.ForeColor) 'did this instead 
    lblUser.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblUser_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csJobAlertRecipient.enmParentProperty.User) <> csJobAlertRecipient.enmParentProperty.User Then Exit Sub 
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("c_JobAlertRecipient", _JobAlertRecipient.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _JobAlertRecipient.DateAdded 
    pAuditIndexed.TableName = "JobAlertRecipient" 
    pAuditIndexed.RowID = _JobAlertRecipient.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Job Alert Recipient'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlc_JobAlertRecipient_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the JobAlertRecipient to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pJobAlertRecipient As csJobAlertRecipient = _JobAlertRecipient 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pJobAlertRecipient.ToCSV) 
        Else 
          Clipboard.SetText(pJobAlertRecipient.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The JobAlertRecipient is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_JobAlertRecipient_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlJobAlertRecipient_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
