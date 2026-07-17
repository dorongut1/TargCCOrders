Public Class ctlc_User
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csUser.enmUpdateType) 
  Public Event evtAdd(ByVal vUser As csUser) 
  Public Event evtBeforeUpdate(ByVal vUser As csUser, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csUser.enmUpdateType, ByVal vUser As csUser) 
  Public Event evtBeforeDelete(ByVal vUser As csUser, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vUserID As Long) 
  Public Event evtCancelledEdit(ByVal vUser As csUser) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vUser As csUser) 
  
  Public Event evtSeparateEdit(ByVal vPropertyName As csUser.enmUpdateType, ByRef rNewValue As String, ByRef rUseNewValue As Boolean, ByRef rCancelUpdate As Boolean, ByRef rNewPrompt As String, ByRef rAppendText As Boolean) 
  Private Event evtLoadApplicationsOptions(ByRef rApplicationsOptions As clsComboList, ByRef rPrompt As String, ByRef rFault As clsFault) 
  Public Event evtParentChosen(ByVal vParentName As csUser.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csUser.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csUser.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csUser.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csUser.enmParentProperty) 
    Public Property EnableBtnPasswordHashedUpdate As Boolean 
    Public Property EnableBtnCommentsUpdate As Boolean 
    Public Property EnableBtnApplicationsUpdate As Boolean 
    Public Property EnableBtnLoggedInIPUpdate As Boolean 
    Public Property EnableBtnLastSuccessfulLoginUpdate As Boolean 
    Public Property EnableBtnSecurityQuestion1ResponseUpdate As Boolean 
    Public Property EnableBtnSecurityQuestion2ResponseUpdate As Boolean 
    Public Property EnableBtnSecurityQuestion3ResponseUpdate As Boolean 
    Public Property EnableBtnPINUpdate As Boolean 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csUser.enmParentProperty) 
      _EnableParentLinks.Add(csUser.enmParentProperty.Role) 
 
      _EnableBtnPasswordHashedUpdate = True 
      _EnableBtnCommentsUpdate = True 
      _EnableBtnApplicationsUpdate = True 
      _EnableBtnLoggedInIPUpdate = True 
      _EnableBtnLastSuccessfulLoginUpdate = True 
      _EnableBtnSecurityQuestion1ResponseUpdate = True 
      _EnableBtnSecurityQuestion2ResponseUpdate = True 
      _EnableBtnSecurityQuestion3ResponseUpdate = True 
      _EnableBtnPINUpdate = True 
    End Sub 
  End Class 
 
  Private WithEvents _User As csUser

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    dtpExpiryDate.Size = txtExpiryDate.Size
    dtpExpiryDate.Location = txtExpiryDate.Location
    cboLanguage.Size = txtLanguage.Size
    cboLanguage.Location = txtLanguage.Location
    cboRole.Size = txtRole.Size
    cboRole.Location = txtRole.Location
    cboAuthenticationMethod.Size = txtAuthenticationMethod.Size
    cboAuthenticationMethod.Location = txtAuthenticationMethod.Location
    cboMessagingMode.Size = txtMessagingMode.Size
    cboMessagingMode.Location = txtMessagingMode.Location
    dtpApprovalTime.Size = txtApprovalTime.Size
    dtpApprovalTime.Location = txtApprovalTime.Location
    cboSecurityQuestion1.Size = txtSecurityQuestion1.Size
    cboSecurityQuestion1.Location = txtSecurityQuestion1.Location
    cboSecurityQuestion2.Size = txtSecurityQuestion2.Size
    cboSecurityQuestion2.Location = txtSecurityQuestion2.Location
    cboSecurityQuestion3.Size = txtSecurityQuestion3.Size
    cboSecurityQuestion3.Location = txtSecurityQuestion3.Location
    'Separate buttons 
    btnPasswordHashedUpdate.Visible = _LoadParameters.EnableBtnPasswordHashedUpdate 
    btnCommentsUpdate.Visible = _LoadParameters.EnableBtnCommentsUpdate 
    btnApplicationsUpdate.Visible = _LoadParameters.EnableBtnApplicationsUpdate 
    btnLoggedInIPUpdate.Visible = _LoadParameters.EnableBtnLoggedInIPUpdate 
    btnLastSuccessfulLoginUpdate.Visible = _LoadParameters.EnableBtnLastSuccessfulLoginUpdate 
    btnSecurityQuestion1ResponseUpdate.Visible = _LoadParameters.EnableBtnSecurityQuestion1ResponseUpdate 
    btnSecurityQuestion2ResponseUpdate.Visible = _LoadParameters.EnableBtnSecurityQuestion2ResponseUpdate 
    btnSecurityQuestion3ResponseUpdate.Visible = _LoadParameters.EnableBtnSecurityQuestion3ResponseUpdate 
    btnPINUpdate.Visible = _LoadParameters.EnableBtnPINUpdate 
  End Sub

  Public Function LoadControl(ByVal vUserID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUser As New csUser(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vUserID <> 0 Then 
      pFault = pUser.GetByID(vUserID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pUser) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rUser As csUser, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rUser)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rUser As csUser) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _User = rUser 

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
 
    If cboSecurityQuestion1.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.c_RoleDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      pFault = LoadCboSecurityQuestion1() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboSecurityQuestion2() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboSecurityQuestion3() : If pFault.isOK = False Then Return pFault 
      'EnumCombos
      pFault = LoadCboType() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboLanguage() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboAuthenticationMethod() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboMessagingMode() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboSecurityQuestion1() : If pFault.isOK = False Then Return pFault 
    pFault = LoadCboSecurityQuestion2() : If pFault.isOK = False Then Return pFault 
    pFault = LoadCboSecurityQuestion3() : If pFault.isOK = False Then Return pFault 
 
    'Parents
    pFault = LoadCboRole() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rUser"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rUser As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rUser.GetType.Name = "csUser" Then 
      ctlUser_Load(Nothing, Nothing) 
      Dim pUser As csUser = CType(rUser, csUser) 
      Return LoadControl(pUser) 
    Else 
      Dim pUserID As Long = CType(rUser, Long) 
      Return LoadControl(pUserID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "UserName", _Requester) 
    If pStrg <> "" Then lblUserName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "LastName", _Requester) 
    If pStrg <> "" Then lblLastName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "FirstName", _Requester) 
    If pStrg <> "" Then lblFirstName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "FullName", _Requester) 
    If pStrg <> "" Then lblFullName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "NationalIDNo", _Requester) 
    If pStrg <> "" Then lblNationalIDNo.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Address", _Requester) 
    If pStrg <> "" Then lblAddress.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "City", _Requester) 
    If pStrg <> "" Then lblCity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ProvinceState", _Requester) 
    If pStrg <> "" Then lblProvinceState.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PostalCode", _Requester) 
    If pStrg <> "" Then lblPostalCode.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Country", _Requester) 
    If pStrg <> "" Then lblCountry.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PhoneNumber", _Requester) 
    If pStrg <> "" Then lblPhoneNumber.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Email", _Requester) 
    If pStrg <> "" Then lblEmail.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PasswordHashed", _Requester) 
    If pStrg <> "" Then lblPasswordHashed.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "DatePasswordChanged", _Requester) 
    If pStrg <> "" Then lblDatePasswordChanged.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Type", _Requester) 
    If pStrg <> "" Then lblType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "IDinType", _Requester) 
    If pStrg <> "" Then lblIDinType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "RequiresComputerIdentification", _Requester) 
    If pStrg <> "" Then lblRequiresComputerIdentification.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "EnableSimultaneousLogins", _Requester) 
    If pStrg <> "" Then lblEnableSimultaneousLogins.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "DateActivated", _Requester) 
    If pStrg <> "" Then lblDateActivated.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "IsDisabled", _Requester) 
    If pStrg <> "" Then lblIsDisabled.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ExpiryDate", _Requester) 
    If pStrg <> "" Then lblExpiryDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Comments", _Requester) 
    If pStrg <> "" Then lblComments.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "LastPasswords", _Requester) 
    If pStrg <> "" Then lblLastPasswords.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Applications", _Requester) 
    If pStrg <> "" Then lblApplications.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Language", _Requester) 
    If pStrg <> "" Then lblLanguage.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "IsLockedOut", _Requester) 
    If pStrg <> "" Then lblIsLockedOut.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Role", _Requester) 
    If pStrg <> "" Then lblRole.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "AuthenticationMethod", _Requester) 
    If pStrg <> "" Then lblAuthenticationMethod.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "RequiresFixedIP", _Requester) 
    If pStrg <> "" Then lblRequiresFixedIP.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "MessagingMode", _Requester) 
    If pStrg <> "" Then lblMessagingMode.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "LoggedInIP", _Requester) 
    If pStrg <> "" Then lblLoggedInIP.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ApprovalCodeHashed", _Requester) 
    If pStrg <> "" Then lblApprovalCodeHashed.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ApprovalFunctionName", _Requester) 
    If pStrg <> "" Then lblApprovalFunctionName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ApprovalTime", _Requester) 
    If pStrg <> "" Then lblApprovalTime.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "LastSuccessfulLogin", _Requester) 
    If pStrg <> "" Then lblLastSuccessfulLogin.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PasswordNeverExpires", _Requester) 
    If pStrg <> "" Then lblPasswordNeverExpires.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion1", _Requester) 
    If pStrg <> "" Then lblSecurityQuestion1.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion1Response", _Requester) 
    If pStrg <> "" Then lblSecurityQuestion1Response.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion2", _Requester) 
    If pStrg <> "" Then lblSecurityQuestion2.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion2Response", _Requester) 
    If pStrg <> "" Then lblSecurityQuestion2Response.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion3", _Requester) 
    If pStrg <> "" Then lblSecurityQuestion3.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion3Response", _Requester) 
    If pStrg <> "" Then lblSecurityQuestion3Response.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PIN", _Requester) 
    If pStrg <> "" Then lblPIN.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [User]() As csUser
    Get 
      Return _User 
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
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.Type, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pTypees.FillEnums(clsEnums.enmEnum.UserIdentityType, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pTypees = pTestCol
    End If
    
    pTypees.Remove(pTypees.FindByKey(clsEnums.enmUserIdentityType.UD))
    pTypees.SortByText()
    pTypees.AddToTop(clsEnums.enmUserIdentityType.UD, GetChoose(_Requester))

    With cboType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pTypees
    End With

    cboType.SelectedValue = _User.Type 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboLanguage() As clsFault
    Dim pFault As New clsFault
 
    'If cboLanguage.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pLanguagees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.Language, pTestCol, pPrompt) 
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

    cboLanguage.SelectedValue = _User.Language 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboRole() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_RoleDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csUser.enmParentProperty.Role, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboRole.MakeSmart() Else cboRole.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboRole.LoadControl(pComboList, pPrompt) 
    Else 
      cboRole.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _User.RoleID > 0 Then cboRole.ValueSelect(_User.RoleID) Else cboRole.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboAuthenticationMethod() As clsFault
    Dim pFault As New clsFault
 
    'If cboAuthenticationMethod.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pAuthenticationMethodes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.AuthenticationMethod, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pAuthenticationMethodes.FillEnums(clsEnums.enmEnum.AuthenticationMethod, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pAuthenticationMethodes = pTestCol
    End If
    
    pAuthenticationMethodes.Remove(pAuthenticationMethodes.FindByKey(clsEnums.enmAuthenticationMethod.UD))
    pAuthenticationMethodes.SortByText()
    pAuthenticationMethodes.AddToTop(clsEnums.enmAuthenticationMethod.UD, GetChoose(_Requester))

    With cboAuthenticationMethod
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pAuthenticationMethodes
    End With

    cboAuthenticationMethod.SelectedValue = _User.AuthenticationMethod 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboMessagingMode() As clsFault
    Dim pFault As New clsFault
 
    'If cboMessagingMode.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pMessagingModees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.MessagingMode, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pMessagingModees.FillEnums(clsEnums.enmEnum.MessagingMode, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pMessagingModees = pTestCol
    End If
    
    pMessagingModees.Remove(pMessagingModees.FindByKey(clsEnums.enmMessagingMode.UD))
    pMessagingModees.SortByText()
    pMessagingModees.AddToTop(clsEnums.enmMessagingMode.UD, GetChoose(_Requester))

    With cboMessagingMode
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pMessagingModees
    End With

    cboMessagingMode.SelectedValue = _User.MessagingMode 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboSecurityQuestion1() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboSecurityQuestion1.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.SecurityQuestion1, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText() 
    Else
      pComboList = pTestCol
    End If

    cboSecurityQuestion1.Tag = "" 
    pFault = LoadCbo(cboSecurityQuestion1, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _User.SecurityQuestion1Code <> "" Then cboSecurityQuestion1.SelectedValue = _User.SecurityQuestion1Code

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboSecurityQuestion2() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboSecurityQuestion2.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.SecurityQuestion2, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText() 
    Else
      pComboList = pTestCol
    End If

    cboSecurityQuestion2.Tag = "" 
    pFault = LoadCbo(cboSecurityQuestion2, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _User.SecurityQuestion2Code <> "" Then cboSecurityQuestion2.SelectedValue = _User.SecurityQuestion2Code

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboSecurityQuestion3() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboSecurityQuestion3.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.SecurityQuestion3, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText() 
    Else
      pComboList = pTestCol
    End If

    cboSecurityQuestion3.Tag = "" 
    pFault = LoadCbo(cboSecurityQuestion3, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _User.SecurityQuestion3Code <> "" Then cboSecurityQuestion3.SelectedValue = _User.SecurityQuestion3Code

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmUserIdentityType = CType(cboType.SelectedValue, clsEnums.enmUserIdentityType) 
    RaiseEvent evtCboSelectedIndexChanged(csUser.enmParentProperty.Type, pEnum.ToString) 
  End Sub 
  Private Sub cboLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLanguage.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmLanguage = CType(cboLanguage.SelectedValue, clsEnums.enmLanguage) 
    RaiseEvent evtCboSelectedIndexChanged(csUser.enmParentProperty.Language, pEnum.ToString) 
  End Sub 
  Private Sub cboRole_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboRole.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csUser.enmParentProperty.Role, pUniqueCode) 
  End Sub 
  Private Sub cboAuthenticationMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAuthenticationMethod.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmAuthenticationMethod = CType(cboAuthenticationMethod.SelectedValue, clsEnums.enmAuthenticationMethod) 
    RaiseEvent evtCboSelectedIndexChanged(csUser.enmParentProperty.AuthenticationMethod, pEnum.ToString) 
  End Sub 
  Private Sub cboMessagingMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMessagingMode.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmMessagingMode = CType(cboMessagingMode.SelectedValue, clsEnums.enmMessagingMode) 
    RaiseEvent evtCboSelectedIndexChanged(csUser.enmParentProperty.MessagingMode, pEnum.ToString) 
  End Sub 
  Private Sub cboSecurityQuestion1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSecurityQuestion1.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboSecurityQuestion1.SelectedValue, String) 
    RaiseEvent evtCboSelectedIndexChanged(csUser.enmParentProperty.SecurityQuestion1, pCode) 
  End Sub 
  Private Sub cboSecurityQuestion2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSecurityQuestion2.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboSecurityQuestion2.SelectedValue, String) 
    RaiseEvent evtCboSelectedIndexChanged(csUser.enmParentProperty.SecurityQuestion2, pCode) 
  End Sub 
  Private Sub cboSecurityQuestion3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSecurityQuestion3.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboSecurityQuestion3.SelectedValue, String) 
    RaiseEvent evtCboSelectedIndexChanged(csUser.enmParentProperty.SecurityQuestion3, pCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csUser.enmParentProperty = csUser.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csUser.enmParentProperty.Role) = csUser.enmParentProperty.Role Then 
      lblRole.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    txtUserName.ReadOnly = Not (vInEdit)
    txtUserName.BackColor = pDefaultColour 
    txtLastName.ReadOnly = Not (vInEdit)
    txtLastName.BackColor = pDefaultColour 
    txtFirstName.ReadOnly = Not (vInEdit)
    txtFirstName.BackColor = pDefaultColour 
    txtFullName.ReadOnly = True 
    txtFullName.BackColor = pReadonlyColour 
    txtFullName.ForeColor = SetForeColor(vInEdit) 
    txtNationalIDNo.ReadOnly = Not (vInEdit)
    txtNationalIDNo.BackColor = pDefaultColour 
    txtAddress.ReadOnly = Not (vInEdit)
    txtAddress.BackColor = pDefaultColour 
    txtCity.ReadOnly = Not (vInEdit)
    txtCity.BackColor = pDefaultColour 
    txtProvinceState.ReadOnly = Not (vInEdit)
    txtProvinceState.BackColor = pDefaultColour 
    txtPostalCode.ReadOnly = Not (vInEdit)
    txtPostalCode.BackColor = pDefaultColour 
    txtCountry.ReadOnly = Not (vInEdit)
    txtCountry.BackColor = pDefaultColour 
    txtPhoneNumber.ReadOnly = Not (vInEdit)
    txtPhoneNumber.BackColor = pDefaultColour 
    txtEmail.ReadOnly = Not (vInEdit)
    txtEmail.BackColor = pDefaultColour 
    txtPasswordHashed.ReadOnly = True 
    txtPasswordHashed.BackColor = pReadonlyColour 
    txtPasswordHashed.ForeColor = SetForeColor(vInEdit) 
    txtDatePasswordChanged.ReadOnly = True 
    txtDatePasswordChanged.BackColor = pReadonlyColour 
    txtDatePasswordChanged.ForeColor = SetForeColor(vInEdit) 
    txtType.ReadOnly = True
    txtType.Visible = Not (vInEdit)
    txtType.BackColor = pReadonlyColour 
    txtType.ForeColor = SetForeColor(vInEdit) 
    cboType.Visible = vInEdit
    txtIDinType.ReadOnly = Not (vInEdit)
    txtIDinType.BackColor = pDefaultColour 
    chkRequiresComputerIdentification.Enabled = True
    chkEnableSimultaneousLogins.Enabled = True
    txtDateActivated.ReadOnly = True 
    txtDateActivated.BackColor = pReadonlyColour 
    txtDateActivated.ForeColor = SetForeColor(vInEdit) 
    chkIsDisabled.Enabled = True
    dtpExpiryDate.Visible = vInEdit
    txtExpiryDate.Visible = Not (vInEdit)
    txtExpiryDate.BackColor = pReadonlyColour 
    txtExpiryDate.ForeColor = SetForeColor(vInEdit) 
    txtExpiryDate.ReadOnly = True
    txtComments.ReadOnly = True 
    txtComments.BackColor = pReadonlyColour 
    txtComments.ForeColor = SetForeColor(vInEdit) 
    txtLastPasswords.ReadOnly = True 
    txtLastPasswords.BackColor = pReadonlyColour 
    txtLastPasswords.ForeColor = SetForeColor(vInEdit) 
    txtApplications.ReadOnly = True 
    txtApplications.BackColor = pReadonlyColour 
    txtApplications.ForeColor = SetForeColor(vInEdit) 
    txtLanguage.ReadOnly = True
    txtLanguage.Visible = Not (vInEdit)
    txtLanguage.BackColor = pReadonlyColour 
    txtLanguage.ForeColor = SetForeColor(vInEdit) 
    cboLanguage.Visible = vInEdit
    chkIsLockedOut.Enabled = True
    If vInEdit = False Then 
      txtRole.ReadOnly = True
      txtRole.Visible = True
      txtRole.BackColor = pReadonlyColour
      txtRole.ForeColor = SetForeColor(vInEdit) 
      cboRole.Visible = False 
    Else 
      txtRole.ReadOnly = True
      txtRole.Visible = Not (vInEdit)
      txtRole.BackColor = pReadonlyColour 
      txtRole.ForeColor = SetForeColor(vInEdit) 
      cboRole.Visible = vInEdit
    End If  
    txtAuthenticationMethod.ReadOnly = True
    txtAuthenticationMethod.Visible = Not (vInEdit)
    txtAuthenticationMethod.BackColor = pReadonlyColour 
    txtAuthenticationMethod.ForeColor = SetForeColor(vInEdit) 
    cboAuthenticationMethod.Visible = vInEdit
    chkRequiresFixedIP.Enabled = True
    txtMessagingMode.ReadOnly = True
    txtMessagingMode.Visible = Not (vInEdit)
    txtMessagingMode.BackColor = pReadonlyColour 
    txtMessagingMode.ForeColor = SetForeColor(vInEdit) 
    cboMessagingMode.Visible = vInEdit
    txtLoggedInIP.ReadOnly = True 
    txtLoggedInIP.BackColor = pReadonlyColour 
    txtLoggedInIP.ForeColor = SetForeColor(vInEdit) 
    txtApprovalCodeHashed.ReadOnly = Not (vInEdit)
    txtApprovalCodeHashed.BackColor = pDefaultColour 
    txtApprovalFunctionName.ReadOnly = Not (vInEdit)
    txtApprovalFunctionName.BackColor = pDefaultColour 
    dtpApprovalTime.Visible = vInEdit
    txtApprovalTime.Visible = Not (vInEdit)
    txtApprovalTime.BackColor = pReadonlyColour 
    txtApprovalTime.ForeColor = SetForeColor(vInEdit) 
    txtApprovalTime.ReadOnly = True
    txtLastSuccessfulLogin.ReadOnly = True 
    txtLastSuccessfulLogin.BackColor = pReadonlyColour 
    txtLastSuccessfulLogin.ForeColor = SetForeColor(vInEdit) 
    chkPasswordNeverExpires.Enabled = True
    txtSecurityQuestion1.ReadOnly = True
    txtSecurityQuestion1.Visible = Not (vInEdit)
    txtSecurityQuestion1.BackColor = pReadonlyColour 
    txtSecurityQuestion1.ForeColor = SetForeColor(vInEdit) 
    cboSecurityQuestion1.Visible = vInEdit
    txtSecurityQuestion1Response.ReadOnly = True 
    txtSecurityQuestion1Response.BackColor = pReadonlyColour 
    txtSecurityQuestion1Response.ForeColor = SetForeColor(vInEdit) 
    txtSecurityQuestion2.ReadOnly = True
    txtSecurityQuestion2.Visible = Not (vInEdit)
    txtSecurityQuestion2.BackColor = pReadonlyColour 
    txtSecurityQuestion2.ForeColor = SetForeColor(vInEdit) 
    cboSecurityQuestion2.Visible = vInEdit
    txtSecurityQuestion2Response.ReadOnly = True 
    txtSecurityQuestion2Response.BackColor = pReadonlyColour 
    txtSecurityQuestion2Response.ForeColor = SetForeColor(vInEdit) 
    txtSecurityQuestion3.ReadOnly = True
    txtSecurityQuestion3.Visible = Not (vInEdit)
    txtSecurityQuestion3.BackColor = pReadonlyColour 
    txtSecurityQuestion3.ForeColor = SetForeColor(vInEdit) 
    cboSecurityQuestion3.Visible = vInEdit
    txtSecurityQuestion3Response.ReadOnly = True 
    txtSecurityQuestion3Response.BackColor = pReadonlyColour 
    txtSecurityQuestion3Response.ForeColor = SetForeColor(vInEdit) 
    txtPIN.ReadOnly = True 
    txtPIN.BackColor = pReadonlyColour 
    txtPIN.ForeColor = SetForeColor(vInEdit) 

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
      If _User.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
    Else 
      btnUpdate.Visible = False 
      btnCancel.Visible = False 
      btnEdit.Visible = False 
      btnDelete.Visible = False 
      btnAdd.Visible = False 
    End If 
    
    'set up 'UpdatedSeparately' controls
    'PasswordHashed 
    If _User.PasswordHashed.ToString() = "" Then btnPasswordHashedUpdate.Text = CCTextTranslate("Create", _Requester) Else btnPasswordHashedUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtPasswordHashed.Enabled = Not (vInEdit) 
    btnPasswordHashedUpdate.Enabled = Not (vInEdit) 
    btnPasswordHashedUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 
    'Comments 
    If _User.Comments.ToString() = "" Then btnCommentsUpdate.Text = CCTextTranslate("Create", _Requester) Else btnCommentsUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtComments.Enabled = Not (vInEdit) 
    btnCommentsUpdate.Enabled = Not (vInEdit) 
    btnCommentsUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 
    'Applications 
    If _User.Applications.ToString() = "" Then btnApplicationsUpdate.Text = CCTextTranslate("Create", _Requester) Else btnApplicationsUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtApplications.Enabled = Not (vInEdit) 
    btnApplicationsUpdate.Enabled = Not (vInEdit) 
    btnApplicationsUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 
    'LoggedInIP 
    If _User.LoggedInIP.ToString() = "" Then btnLoggedInIPUpdate.Text = CCTextTranslate("Create", _Requester) Else btnLoggedInIPUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtLoggedInIP.Enabled = Not (vInEdit) 
    btnLoggedInIPUpdate.Enabled = Not (vInEdit) 
    btnLoggedInIPUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 
    'LastSuccessfulLogin 
    If _User.LastSuccessfulLogin.ToString() = "" Then btnLastSuccessfulLoginUpdate.Text = CCTextTranslate("Create", _Requester) Else btnLastSuccessfulLoginUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtLastSuccessfulLogin.Enabled = Not (vInEdit) 
    btnLastSuccessfulLoginUpdate.Enabled = Not (vInEdit) 
    btnLastSuccessfulLoginUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 
    'SecurityQuestion1Response 
    If _User.SecurityQuestion1Response(vDecrypt:=False).ToString() = "" Then btnSecurityQuestion1ResponseUpdate.Text = CCTextTranslate("Create", _Requester) Else btnSecurityQuestion1ResponseUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtSecurityQuestion1Response.Enabled = Not (vInEdit) 
    btnSecurityQuestion1ResponseUpdate.Enabled = Not (vInEdit) 
    btnSecurityQuestion1ResponseUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 
    'SecurityQuestion2Response 
    If _User.SecurityQuestion2Response(vDecrypt:=False).ToString() = "" Then btnSecurityQuestion2ResponseUpdate.Text = CCTextTranslate("Create", _Requester) Else btnSecurityQuestion2ResponseUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtSecurityQuestion2Response.Enabled = Not (vInEdit) 
    btnSecurityQuestion2ResponseUpdate.Enabled = Not (vInEdit) 
    btnSecurityQuestion2ResponseUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 
    'SecurityQuestion3Response 
    If _User.SecurityQuestion3Response(vDecrypt:=False).ToString() = "" Then btnSecurityQuestion3ResponseUpdate.Text = CCTextTranslate("Create", _Requester) Else btnSecurityQuestion3ResponseUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtSecurityQuestion3Response.Enabled = Not (vInEdit) 
    btnSecurityQuestion3ResponseUpdate.Enabled = Not (vInEdit) 
    btnSecurityQuestion3ResponseUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 
    'PIN 
    If _User.PIN(vDecrypt:=False).ToString() = "" Then btnPINUpdate.Text = CCTextTranslate("Create", _Requester) Else btnPINUpdate.Text = CCTextTranslate("Change", _Requester) 
    txtPIN.Enabled = Not (vInEdit) 
    btnPINUpdate.Enabled = Not (vInEdit) 
    btnPINUpdate.Visible = ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) 

    'disable or enable any child grids 
    Dim ctrl As Control = Me.GetNextControl(Me, True) 
    Do Until ctrl Is Nothing 
      If ctrl.GetType.Name.StartsWith("ctl") AndAlso ctrl.GetType.Name.EndsWith("Col") Then 
        ctrl.Enabled = Not vInEdit 
      End If 
      ctrl = Me.GetNextControl(ctrl, True) 
    Loop 
 
    RaiseEvent evtControlsRefreshed(vInEdit, _User) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _User
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtUserName.Text = .UserName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtUserName.MaxLength = 50 
      txtLastName.Text = .LastName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLastName.MaxLength = 50 
      txtFirstName.Text = .FirstName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFirstName.MaxLength = 50 
      txtFullName.Text = .FullName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFullName.MaxLength = 101 
      txtNationalIDNo.Text = .NationalIDNo.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtNationalIDNo.MaxLength = 50 
      txtAddress.Text = .Address.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtAddress.MaxLength = 250 
      txtCity.Text = .City.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCity.MaxLength = 50 
      txtProvinceState.Text = .ProvinceState.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtProvinceState.MaxLength = 50 
      txtPostalCode.Text = .PostalCode.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtPostalCode.MaxLength = 50 
      txtCountry.Text = .Country.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCountry.MaxLength = 50 
      txtPhoneNumber.Text = .PhoneNumber.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtPhoneNumber.MaxLength = 50 
      txtEmail.Text = .Email.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtEmail.MaxLength = 50 
      txtPasswordHashed.PasswordChar = "*"c 
      txtPasswordHashed.UseSystemPasswordChar = True 
      txtPasswordHashed.Text = "xxxxxxxx"
      If Math.Abs(.DatePasswordChanged.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.DatePasswordChanged.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtDatePasswordChanged.Text = "" Else txtDatePasswordChanged.Text = .DatePasswordChanged.ToString(FormatFromTag(txtDatePasswordChanged, "dd-MM-yyyy HH:mm:ss"))
      cboType.SelectedValue = .Type
      txtType.Text = cboType.Text : If cboType.SelectedValue Is Nothing OrElse cboType.SelectedValue.ToString() = "UD" Then txtType.Text = ""    
      txtIDinType.Text = .IDinType.ToString(FormatFromTag(txtIDinType, "#,##0"))
      chkRequiresComputerIdentification.Checked = .RequiresComputerIdentification
      chkEnableSimultaneousLogins.Checked = .EnableSimultaneousLogins
      If Math.Abs(.DateActivated.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.DateActivated.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtDateActivated.Text = "" Else txtDateActivated.Text = .DateActivated.ToString(FormatFromTag(txtDateActivated, "dd-MM-yyyy HH:mm:ss"))
      chkIsDisabled.Checked = .IsDisabled
      If .ExpiryDate < dtpExpiryDate.MinDate Then dtpExpiryDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpExpiryDate.Value = .ExpiryDate
      dtpExpiryDate.CustomFormat = FormatFromTag(txtExpiryDate, "dd-MM-yyyy HH:mm:ss") 
      dtpExpiryDate.Value = DateTime.ParseExact(dtpExpiryDate.Value.ToString(dtpExpiryDate.CustomFormat), dtpExpiryDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .ExpiryDate < dtpExpiryDate.MinDate Then dtpExpiryDate.Checked = False Else dtpExpiryDate.Checked = True 
      If Math.Abs(.ExpiryDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.ExpiryDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtExpiryDate.Text = "" Else txtExpiryDate.Text = .ExpiryDate.ToString(FormatFromTag(txtExpiryDate, "dd-MM-yyyy HH:mm:ss"))
      txtComments.Text = .Comments.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtComments.MaxLength = 250 
      txtLastPasswords.Text = .LastPasswords.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLastPasswords.MaxLength = 350 
      txtApplications.Text = .Applications.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtApplications.MaxLength = 1000 
      cboLanguage.SelectedValue = .Language
      txtLanguage.Text = cboLanguage.Text : If cboLanguage.SelectedValue Is Nothing OrElse cboLanguage.SelectedValue.ToString() = "UD" Then txtLanguage.Text = ""    
      chkIsLockedOut.Checked = .IsLockedOut
      txtRole.Text = .RoleText 
      cboAuthenticationMethod.SelectedValue = .AuthenticationMethod
      txtAuthenticationMethod.Text = cboAuthenticationMethod.Text : If cboAuthenticationMethod.SelectedValue Is Nothing OrElse cboAuthenticationMethod.SelectedValue.ToString() = "UD" Then txtAuthenticationMethod.Text = ""    
      chkRequiresFixedIP.Checked = .RequiresFixedIP
      cboMessagingMode.SelectedValue = .MessagingMode
      txtMessagingMode.Text = cboMessagingMode.Text : If cboMessagingMode.SelectedValue Is Nothing OrElse cboMessagingMode.SelectedValue.ToString() = "UD" Then txtMessagingMode.Text = ""    
      txtLoggedInIP.Text = .LoggedInIP.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLoggedInIP.MaxLength = 100 
      txtApprovalCodeHashed.PasswordChar = "*"c 
      txtApprovalCodeHashed.UseSystemPasswordChar = True 
      txtApprovalCodeHashed.Text = "xxxxxxxx"
      txtApprovalFunctionName.Text = .ApprovalFunctionName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtApprovalFunctionName.MaxLength = 100 
      If .ApprovalTime < dtpApprovalTime.MinDate Then dtpApprovalTime.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpApprovalTime.Value = .ApprovalTime.LocalDateTime
      dtpApprovalTime.CustomFormat = FormatFromTag(txtApprovalTime, "dd-MM-yyyy HH:mm:ss") 
      dtpApprovalTime.Value = DateTime.ParseExact(dtpApprovalTime.Value.ToString(dtpApprovalTime.CustomFormat), dtpApprovalTime.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .ApprovalTime < dtpApprovalTime.MinDate Then dtpApprovalTime.Checked = False Else dtpApprovalTime.Checked = True 
      txtApprovalTime.Text = FormattedDateTimeOffsetFromTag(txtApprovalTime, .ApprovalTime) 
      txtLastSuccessfulLogin.Text = FormattedDateTimeOffsetFromTag(txtLastSuccessfulLogin, .LastSuccessfulLogin) 
      chkPasswordNeverExpires.Checked = .PasswordNeverExpires
      cboSecurityQuestion1.SelectedValue = .SecurityQuestion1Code
      txtSecurityQuestion1.Text = cboSecurityQuestion1.Text : If cboSecurityQuestion1.SelectedValue Is Nothing OrElse cboSecurityQuestion1.SelectedValue.ToString() = "" Then txtSecurityQuestion1.Text = ""    
      txtSecurityQuestion1Response.Text = .SecurityQuestion1Response(vDecrypt:=True)
      cboSecurityQuestion2.SelectedValue = .SecurityQuestion2Code
      txtSecurityQuestion2.Text = cboSecurityQuestion2.Text : If cboSecurityQuestion2.SelectedValue Is Nothing OrElse cboSecurityQuestion2.SelectedValue.ToString() = "" Then txtSecurityQuestion2.Text = ""    
      txtSecurityQuestion2Response.Text = .SecurityQuestion2Response(vDecrypt:=True)
      cboSecurityQuestion3.SelectedValue = .SecurityQuestion3Code
      txtSecurityQuestion3.Text = cboSecurityQuestion3.Text : If cboSecurityQuestion3.SelectedValue Is Nothing OrElse cboSecurityQuestion3.SelectedValue.ToString() = "" Then txtSecurityQuestion3.Text = ""    
      txtSecurityQuestion3Response.Text = .SecurityQuestion3Response(vDecrypt:=True)
      txtPIN.Text = .PIN(vDecrypt:=True)
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _User
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-User-ID-090417-0012", _Requester) : Return pFault 
      .UserName = txtUserName.Text 
      .LastName = txtLastName.Text 
      .FirstName = txtFirstName.Text 
      .NationalIDNo = txtNationalIDNo.Text 
      .Address = txtAddress.Text 
      .City = txtCity.Text 
      .ProvinceState = txtProvinceState.Text 
      .PostalCode = txtPostalCode.Text 
      .Country = txtCountry.Text 
      .PhoneNumber = txtPhoneNumber.Text 
      .Email = txtEmail.Text 
      .Type = CType(cboType.SelectedValue, clsEnums.enmUserIdentityType)
      If Long.TryParse(txtIDinType.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .IDinType) = False Then pFault.LogFreeTextFault(208, ".IDinType", txtIDinType.Text, "TRGT-User-IDinType-090417-0012", _Requester) : Return pFault 
      .RequiresComputerIdentification = chkRequiresComputerIdentification.Checked
      .EnableSimultaneousLogins = chkEnableSimultaneousLogins.Checked
      .IsDisabled = chkIsDisabled.Checked
      If (dtpExpiryDate.ShowCheckBox AndAlso dtpExpiryDate.Checked = False) OrElse dtpExpiryDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .ExpiryDate = Nothing Else .ExpiryDate = dtpExpiryDate.Value
      .Language = CType(cboLanguage.SelectedValue, clsEnums.enmLanguage)
      .IsLockedOut = chkIsLockedOut.Checked
      If cboRole.SelectedItem Is Nothing OrElse cboRole.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .RoleID = 0 
      Else 
        Dim pRoleID As Long = CType(cboRole.SelectedItem, clsComboListMember).KeyLong 
        If pRoleID = -1 Then .RoleID = 0 Else .RoleID = pRoleID 
      End If 
      .AuthenticationMethod = CType(cboAuthenticationMethod.SelectedValue, clsEnums.enmAuthenticationMethod)
      .RequiresFixedIP = chkRequiresFixedIP.Checked
      .MessagingMode = CType(cboMessagingMode.SelectedValue, clsEnums.enmMessagingMode)
      If txtApprovalCodeHashed.Text <> "xxxxxxxx" Then .ApprovalCodeHashed = "PleaseHash" & txtApprovalCodeHashed.Text 
      .ApprovalFunctionName = txtApprovalFunctionName.Text 
      If (dtpApprovalTime.ShowCheckBox AndAlso dtpApprovalTime.Checked = False) OrElse dtpApprovalTime.Value = New Date(1900, 1, 1, 0, 0, 0) Then .ApprovalTime = Nothing Else .ApprovalTime = dtpApprovalTime.Value
      .PasswordNeverExpires = chkPasswordNeverExpires.Checked
      If cboSecurityQuestion1.SelectedItem IsNot Nothing Then .SecurityQuestion1Code = CType(cboSecurityQuestion1.SelectedItem, clsComboListMember).KeyString Else .SecurityQuestion1Code = "" 
      If cboSecurityQuestion2.SelectedItem IsNot Nothing Then .SecurityQuestion2Code = CType(cboSecurityQuestion2.SelectedItem, clsComboListMember).KeyString Else .SecurityQuestion2Code = "" 
      If cboSecurityQuestion3.SelectedItem IsNot Nothing Then .SecurityQuestion3Code = CType(cboSecurityQuestion3.SelectedItem, clsComboListMember).KeyString Else .SecurityQuestion3Code = "" 
    End With
    pFault.SetOK() 
    Return pFault 
  End Function
  
  'Handle one way encrypted textboxes
  Private Sub txtApprovalCodeHashed_KeyDown(sender As Object, e As KeyEventArgs) Handles txtApprovalCodeHashed.KeyDown 
    If txtApprovalCodeHashed.Text = "xxxxxxxx" And btnUpdate.Visible = True Then 
      txtApprovalCodeHashed.PasswordChar = Nothing 
      txtApprovalCodeHashed.UseSystemPasswordChar = False 
      txtApprovalCodeHashed.Text = "" 
      txtApprovalCodeHashed.Text = ChrW(e.KeyValue) 
      txtApprovalCodeHashed.SelectionStart = 1 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-User-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtIDinType_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIDinType.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtIDinType.Text 
    Dim pTest As Long 
 
    If txtIDinType.Text = "" Then Exit Sub 
    If txtIDinType.Text = txtIDinType.Name Then Exit Sub 
 
    If Long.TryParse(txtIDinType.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-User-IDinType-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csUser.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-User-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_User, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _User.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the User collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.c_UserDefaultByID) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.Standard, _User) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_User_evtAfterUpdate 
  Private Sub _User_evtAfterUpdate() Handles _User.evtAfterUpdate, _User.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_User) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _User = New csUser(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_User) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_User, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _User.FirstName & " " & _User.LastName & " (" & _User.UserName & ")" & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _User.ID 
    pFault = _User.Delete(_Requester) 
    If pFault.isOK = True Then 
      _User = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only
  Private Sub chkRequiresComputerIdentification_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRequiresComputerIdentification.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkRequiresComputerIdentification.Checked = _User.RequiresComputerIdentification
    End If
  End Sub
  Private Sub chkEnableSimultaneousLogins_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEnableSimultaneousLogins.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkEnableSimultaneousLogins.Checked = _User.EnableSimultaneousLogins
    End If
  End Sub
  Private Sub chkIsDisabled_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsDisabled.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkIsDisabled.Checked = _User.IsDisabled
    End If
  End Sub
  Private Sub chkIsLockedOut_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsLockedOut.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkIsLockedOut.Checked = _User.IsLockedOut
    End If
  End Sub
  Private Sub chkRequiresFixedIP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRequiresFixedIP.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkRequiresFixedIP.Checked = _User.RequiresFixedIP
    End If
  End Sub
  Private Sub chkPasswordNeverExpires_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPasswordNeverExpires.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkPasswordNeverExpires.Checked = _User.PasswordNeverExpires
    End If
  End Sub

  'Now the Parents
  Private Sub lblRole_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblRole.DoubleClick 
    If _User.RoleID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csUser.enmParentProperty.Role) = csUser.enmParentProperty.Role Then 
      If _User.RoleID <> 0 Then RaiseEvent evtParentChosen(csUser.enmParentProperty.Role, _User.RoleID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "Role Detail" 
      fPopup.LoadControl("ctlc_Role", _User.RoleID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblRole_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblRole.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csUser.enmParentProperty.Role) <> csUser.enmParentProperty.Role Then Exit Sub 
    lblRole.ForeColor = Color.Brown 
    'lblRole.Font = New Font(lblRole.Font.Name, lblRole.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblRole.BackColor = ccHelper.InvertColour(lblRole.ForeColor) 'did this instead 
    lblRole.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblRole_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblRole.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csUser.enmParentProperty.Role) <> csUser.enmParentProperty.Role Then Exit Sub 
    lblRole.ForeColor = Color.Brown 
    'lblRole.Font = New Font(lblRole.Font.Name, lblRole.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblRole.BackColor = Me.BackColor 'did this instead 
    lblRole.Cursor = Cursors.Default 
  End Sub 
 
  'SeparateUpdates 
  Private Sub btnPasswordHashedUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPasswordHashedUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToPasswordHashed As Boolean = False 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.PasswordHashed, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToPasswordHashed) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then pPrompt = "Write a new Password " 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.PasswordTextBox 
      frmUpdateField.DialoguePrompt = pPrompt 
      Do 
        frmUpdateField.DialogueInitialValue = "" 
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
    pFault = New clsFault 
    Try 
      If pNewValue = _User.PasswordHashed Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdatePasswordHashed(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.PasswordHashed, _User) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub btnCommentsUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCommentsUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToComments As Boolean = True 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.Comments, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToComments) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then 
        If pAppendTextToComments Then 
          pPrompt = "Add a new Comments" 
        Else 
          pPrompt = "Write a Comments" 
        End If 
      End If 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.MultiLineTextBox 
      If pAppendTextToComments Then 
        frmUpdateField.DialogueInitialValue = "" 
      Else 
        frmUpdateField.DialogueInitialValue = _User.Comments 
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
    If pAppendTextToComments Then pNewValue = ccHelper.PrefixToComment(pNewValue, _User.Comments, _Requester) 
    pFault = New clsFault 
    Try 
      If pNewValue = _User.Comments Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdateComments(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.Comments, _User) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub btnApplicationsUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplicationsUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToApplications As Boolean = False 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.Applications, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToApplications) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then pPrompt = "Choose a Applications" 
 
      Dim pAvailableOptions As New clsComboList 
      pPrompt = "" 
      RaiseEvent evtLoadApplicationsOptions(pAvailableOptions, pPrompt, pFault) 
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
      Dim pSelectedOptions As New clsComboList 
      Dim pApplications As String() = _User.Applications.Replace(ChrW(13), "").Split(ChrW(10)) 
      For Each l In pApplications 
        If l.Trim <> "" Then 
          pSelectedOptions.AddToEnd(pAvailableOptions.FindByText(l)) 
        End If 
      Next 
 
      frmUpdateField.ListOptions = pAvailableOptions 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.ListBox 
      frmUpdateField.DialogueInitialValue = pSelectedOptions 
      If pPrompt = "" Then pPrompt = "Define new Applications" 
      frmUpdateField.DialoguePrompt = pPrompt 
      Do 
        frmUpdateField.ShowDialog() 
        If frmUpdateField.DialogResult = DialogResult.OK Then 
          Try 
            Dim pComboList As clsComboList = CType(frmUpdateField.DialogueReturnValue, clsComboList) 
            For Each l In pComboList 
              pNewValue &= l.Text & Environment.NewLine 
            Next 
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
    pFault = New clsFault 
    Try 
      If pNewValue = _User.Applications Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdateApplications(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.Applications, _User) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub btnLoggedInIPUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoggedInIPUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToLoggedInIP As Boolean = False 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.LoggedInIP, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToLoggedInIP) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then 
        If pAppendTextToLoggedInIP Then 
          pPrompt = "Add a new Logged In IP" 
        Else 
          pPrompt = "Write a Logged In IP" 
        End If 
      End If 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.MultiLineTextBox 
      If pAppendTextToLoggedInIP Then 
        frmUpdateField.DialogueInitialValue = "" 
      Else 
        frmUpdateField.DialogueInitialValue = _User.LoggedInIP 
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
    If pAppendTextToLoggedInIP Then pNewValue = ccHelper.PrefixToComment(pNewValue, _User.LoggedInIP, _Requester) 
    pFault = New clsFault 
    Try 
      If pNewValue = _User.LoggedInIP Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdateLoggedInIP(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.LoggedInIP, _User) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub btnLastSuccessfulLoginUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLastSuccessfulLoginUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToLastSuccessfulLogin As Boolean = False 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.LastSuccessfulLogin, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToLastSuccessfulLogin) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then pPrompt = "Write a new Last Successful Login" 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.DateTimePicker 
      frmUpdateField.DateFormat = "dd MMM yyyy HH:mm:ss" 
      frmUpdateField.DialogueInitialValue = _User.LastSuccessfulLogin 
      frmUpdateField.DialoguePrompt = pPrompt 
      Do 
        frmUpdateField.ShowDialog() 
        If frmUpdateField.DialogResult = DialogResult.OK Then 
          Try 
            pNewValue = frmUpdateField.DialogueReturnValue.ToString() 
            pNewValue = CType(pNewValue, DateTimeOffset).ToString()  'Test to see if data can be converted 
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
    pFault = New clsFault 
    Try 
      If CType(pNewValue, DateTimeOffset) = _User.LastSuccessfulLogin Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdateLastSuccessfulLogin(CType(pNewValue, DateTimeOffset), _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.LastSuccessfulLogin, _User) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub btnSecurityQuestion1ResponseUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSecurityQuestion1ResponseUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToSecurityQuestion1Response As Boolean = False 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.SecurityQuestion1Response, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToSecurityQuestion1Response) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then pPrompt = "Write a new Security Question 1 Response" 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.SingleLineTextBox 
      frmUpdateField.DialoguePrompt = pPrompt 
      Do 
        frmUpdateField.DialogueInitialValue = "" 
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
    pFault = New clsFault 
    Try 
      If pNewValue = _User.SecurityQuestion1Response(True) Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdateSecurityQuestion1Response(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.SecurityQuestion1Response, _User) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub btnSecurityQuestion2ResponseUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSecurityQuestion2ResponseUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToSecurityQuestion2Response As Boolean = False 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.SecurityQuestion2Response, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToSecurityQuestion2Response) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then pPrompt = "Write a new Security Question 2 Response" 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.SingleLineTextBox 
      frmUpdateField.DialoguePrompt = pPrompt 
      Do 
        frmUpdateField.DialogueInitialValue = "" 
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
    pFault = New clsFault 
    Try 
      If pNewValue = _User.SecurityQuestion2Response(True) Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdateSecurityQuestion2Response(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.SecurityQuestion2Response, _User) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub btnSecurityQuestion3ResponseUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSecurityQuestion3ResponseUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToSecurityQuestion3Response As Boolean = False 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.SecurityQuestion3Response, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToSecurityQuestion3Response) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then pPrompt = "Write a new Security Question 3 Response" 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.SingleLineTextBox 
      frmUpdateField.DialoguePrompt = pPrompt 
      Do 
        frmUpdateField.DialogueInitialValue = "" 
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
    pFault = New clsFault 
    Try 
      If pNewValue = _User.SecurityQuestion3Response(True) Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdateSecurityQuestion3Response(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.SecurityQuestion3Response, _User) 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub btnPINUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPINUpdate.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewValue As String = "" 
    Dim pUseNewValue As Boolean = False 
    Dim pCancelUpdate As Boolean = False 
    Dim pPrompt As String = "" 
    Dim pAppendTextToPIN As Boolean = False 
    RaiseEvent evtSeparateEdit(csUser.enmUpdateType.PIN, pNewValue, pUseNewValue, pCancelUpdate, pPrompt, pAppendTextToPIN) 
    If pCancelUpdate = True Then Exit Sub 
 
    Dim pFault As New clsFault 
 
    If pUseNewValue = False Then 
      Dim pSucceeded As Boolean = False 
      If String.IsNullOrEmpty(pPrompt) Then pPrompt = "Write a new PIN" 
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.SingleLineTextBox 
      frmUpdateField.DialoguePrompt = pPrompt 
      Do 
        frmUpdateField.DialogueInitialValue = "" 
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
    pFault = New clsFault 
    Try 
      If pNewValue = _User.PIN(True) Then Cursor = Cursors.Default : Return 'only update if needed  
      pFault = _User.UpdatePIN(pNewValue, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
      'since it's updated separately, then refresh the User 
      pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
    Catch ex As Exception 
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-111207-162001", _Requester) 
    End Try 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
    Else 
      ControlsLoad() 
      SetUpButtons(False) 
      RaiseEvent evtUpdated(csUser.enmUpdateType.PIN, _User) 
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("c_User", _User.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _User.DateAdded 
    pAuditIndexed.TableName = "User" 
    pAuditIndexed.RowID = _User.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'User'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlc_User_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the User to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pUser As csUser = _User 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pUser.ToCSV) 
        Else 
          Clipboard.SetText(pUser.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The User is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_User_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  'Addition for c_User only. This handles the comboboxes for the  IdentityInType
 
  Public Event evtOverrideLoadCboNameInType(ByRef rComboList As clsComboList) 
 
  Public Event evtCboNameInTypeSelectedIndexChanged(ByVal vID As Long) 
 
  'Create controls in code 
  Protected WithEvents txtNameInType As New TextBox 
  Protected WithEvents cboNameInType As New IntelliCombo 
 
  Private _Roles As New csRoleCol 
 
  'Trap the change in cboType 
  Private Sub ctlc_User_evtCboTypeSelectedIndexChanged(ByVal vParentName As csUser.enmParentProperty, ByVal vSelectedValue As Object) Handles Me.evtCboSelectedIndexChanged 
    If vParentName = csUser.enmParentProperty.Type Then 
      Dim pSelectedEnum = clsEnums.TranslateEnmUserIdentityType(CStr(vSelectedValue)) 
      If pSelectedEnum = clsEnums.enmUserIdentityType.Global _ 
          OrElse pSelectedEnum = clsEnums.enmUserIdentityType.UD Then 
        lblIDinType.Text = "Sub-Type" 
        txtNameInType.Text = "Not Applicable" 
        cboNameInType.Visible = False 
        txtNameInType.Visible = True 
        txtIDinType.Text = "0" 
      ElseIf pSelectedEnum = clsEnums.enmUserIdentityType.c_User Then 
        Dim pFault As clsFault 
        pFault = LoadCboUser() : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        cboNameInType.Visible = True 
        txtNameInType.Visible = False 
        lblIDinType.Text = pSelectedEnum.FastToString() 
      ElseIf pSelectedEnum = clsEnums.enmUserIdentityType.Customer Then 
        Dim pFault As clsFault 
        pFault = LoadCboCustomer() : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
        cboNameInType.Visible = True 
        txtNameInType.Visible = False 
        lblIDinType.Text = pSelectedEnum.FastToString() 
      End If 
    End If 
  End Sub 
 
  'Load the comboboxes 
  Public Function LoadCboUser() As clsFault 
    Dim pFault As clsFault 
    Dim pComboList As clsComboList 
 
    'enable using an external list if needed  
    Dim pTestCol As clsComboList = Nothing 
    RaiseEvent evtOverrideLoadCboNameInType(pTestCol) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList() 
      pFault = pComboList.Fill(clsEnums.enmComboListType.c_UserDefaultByID, _Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return pFault 
    Else 
      pFault = New clsFault 
      pFault.SetOK() 
      pComboList = pTestCol 
    End If 
 
    LoadPrtControls() ' otherwise the 'SelectedValue' doesn't work 
 
    If pComboList.Count > 0 Then cboNameInType.LoadControl(pComboList, "Choose") Else cboNameInType.SetKeyType(clsEnums.enmComboListKeyType.Long) 
 
    If _User.IDinType > 0 Then 
      cboNameInType.ValueSelect(_User.IDinType) 
      txtIDinType.Text = _User.IDinType.ToString 
      If cboNameInType.cbo.SelectedItem IsNot Nothing Then 
        txtNameInType.Text = CType(cboNameInType.cbo.SelectedItem, clsComboListMember).Text 
      Else 
        txtNameInType.Text = "" 
      End If 
    End If 
 
    Return pFault.SetOK() 
  End Function 
  Public Function LoadCboCustomer() As clsFault 
    Dim pFault As clsFault 
    Dim pComboList As clsComboList 
 
    'enable using an external list if needed  
    Dim pTestCol As clsComboList = Nothing 
    RaiseEvent evtOverrideLoadCboNameInType(pTestCol) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList() 
      pFault = pComboList.Fill(clsEnums.enmComboListType.ccCustomerDefaultByID, _Requester) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return pFault 
    Else 
      pFault = New clsFault 
      pFault.SetOK() 
      pComboList = pTestCol 
    End If 
 
    LoadPrtControls() ' otherwise the 'SelectedValue' doesn't work 
 
    If pComboList.Count > 0 Then cboNameInType.LoadControl(pComboList, "Choose") Else cboNameInType.SetKeyType(clsEnums.enmComboListKeyType.Long) 
 
    If _User.IDinType > 0 Then 
      cboNameInType.ValueSelect(_User.IDinType) 
      txtIDinType.Text = _User.IDinType.ToString 
      If cboNameInType.cbo.SelectedItem IsNot Nothing Then 
        txtNameInType.Text = CType(cboNameInType.cbo.SelectedItem, clsComboListMember).Text 
      Else 
        txtNameInType.Text = "" 
      End If 
    End If 
 
    Return pFault.SetOK() 
  End Function 
 
  'Trap the change so we can assign to the textbox 
  Private Sub cboNameInType_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboNameInType.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If vComboListMember Is Nothing Then Exit Sub 
    Dim pID As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboNameInTypeSelectedIndexChanged(pID) 
    txtIDinType.Text = pID.ToString 
    txtNameInType.Text = vComboListMember.Text 
  End Sub 
 
  'user handling of the controls 
  Private Sub ctlc_User_evtInEdit(ByVal vWhichProperty As csUser.enmUpdateType) Handles Me.evtEdit 
    SetUpButtonsInPrt(True) 
  End Sub 
  Private Sub ctlc_User_evtUpdated(ByVal vWhichProperty As csUser.enmUpdateType, ByVal vUser As csUser) Handles Me.evtUpdated 
    SetUpButtonsInPrt(False) 
  End Sub 
  Private Sub ctlc_User_evtCancelledEdit(ByVal vUserIdentity As Object) Handles Me.evtCancelledEdit  
    SetUpButtonsInPrt(False)  
  End Sub  
  
  'Trapped the start of the control reload  
  Private Sub ctlc_User_evtBeforeLoad() Handles Me.evtBeforeLoad 
    If cboType.Items.Count > 0 Then cboType.SelectedIndex = 0 
    
    txtNameInType.Text = "Not Applicable" 
    lblIDinType.Text = "Sub-Type" 
    cboNameInType.Visible = False 
    txtIDinType.Text = "0" 
 
    cboNameInType.MakeSmart() 
 
    _Roles = New csRoleCol 
    Dim pFault As clsFault 
    pFault = _Roles.Fill(_Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
  End Sub 
  'Trapped the end of the control reload  
  Private Sub ctlc_User_evtControlLoaded() Handles Me.evtLoaded 
    LoadPrtControls() 
    SetUpButtonsInPrt(False) 
  
    If Not (_Requester.IsInRole("SysAdmin") OrElse _Requester.IsInRole("Master") OrElse _Requester.IsInRole("ApplicationMaster")) Then 
      Dim pRole As csRole = _Roles.FindByID(_User.RoleID) 
      Dim pIsSenior As Boolean = False 
      If pRole.Name.Equals("Master", StringComparison.OrdinalIgnoreCase) OrElse 
        pRole.Name.Equals("ApplicationMaster", StringComparison.OrdinalIgnoreCase) Then 
        pIsSenior = True 
      Else 
        If pRole.BaseRoleID > 0 Then 
          Dim pBaseRole As csRole = _Roles.FindByID(pRole.BaseRoleID) 
          If pBaseRole.Name.Equals("sysadmin", StringComparison.OrdinalIgnoreCase) Then 
            pIsSenior = True 
          End If 
        End If 
      End If 
      If pIsSenior = True Then 
        btnEdit.Visible = False 
        txtRole.Text = pRole.Name 
        btnHistory.Visible = False 
        btnApplicationsUpdate.Visible = False 
        btnPasswordHashedUpdate.Visible = False 
        If tbcUser.TabPages.Contains(tbpComments) Then tbcUser.TabPages.Remove(tbpComments) 
      Else 
        If Not tbcUser.TabPages.Contains(tbpComments) Then tbcUser.TabPages.Add(tbpComments) 
        btnHistory.Visible = True 
        btnApplicationsUpdate.Visible = True 
      End If 
    End If 
  End Sub 
 
  'Create the textboxes form code if needed  
  Private Sub LoadPrtControls()  
    If txtNameInType.Parent Is Nothing Then  
      txtIDinType.Parent.Controls.Add(txtNameInType)  
      txtIDinType.Parent.Controls.Add(cboNameInType)  
      txtNameInType.Location = txtIDinType.Location  
      txtNameInType.Size = txtIDinType.Size  
      txtNameInType.Anchor = txtIDinType.Anchor  
      cboNameInType.Location = txtIDinType.Location  
      cboNameInType.Size = txtIDinType.Size  
      cboNameInType.Anchor = txtIDinType.Anchor  
      txtNameInType.Visible = True  
      cboNameInType.Visible = True  
      txtNameInType.BringToFront()  
      cboNameInType.BringToFront()  
    End If  
     
    Dim pShowPassword As Boolean = False  
    If ccSecurity.UserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser Then 
      If MyController.IsAuthenticationDoneOnExternalSystem AndAlso Not (_Requester.IsInRole("Master") OrElse _Requester.IsInRole("ApplicationMaster")) Then 
      Else 
        pShowPassword = True 
      End If 
    End If 
    txtPasswordHashed.Visible = pShowPassword  
    lblPasswordHashed.Visible = pShowPassword  
    btnPasswordHashedUpdate.Visible = pShowPassword  
    txtDatePasswordChanged.Visible = pShowPassword  
    If Not pShowPassword Then lblDatePasswordChanged.Text = $"Password handled by external system." 
    txtExpiryDate.Visible = pShowPassword 
    lblExpiryDate.Visible = pShowPassword 
  End Sub  
  'Shadow the SetUpButtons in the main control, with the additional controls 
  Private Sub SetUpButtonsInPrt(ByVal vInEdit As Boolean) 
    txtIDinType.Visible = False 
 
    Dim pColour As System.Drawing.Color 
    If vInEdit = True Then 
      pColour = System.Drawing.Color.White 
    Else 
      pColour = System.Drawing.Color.PapayaWhip 
    End If 
    txtNameInType.ReadOnly = True 
    txtNameInType.BackColor = System.Drawing.Color.PapayaWhip 
    If vInEdit = True Then 
      If CType(cboType.SelectedValue, clsEnums.enmUserIdentityType) = clsEnums.enmUserIdentityType.c_User Then 
        txtNameInType.Visible = False 
        cboNameInType.Visible = True 
      ElseIf CType(cboType.SelectedValue, clsEnums.enmUserIdentityType) = clsEnums.enmUserIdentityType.Customer Then 
        txtNameInType.Visible = False 
        cboNameInType.Visible = True 
      Else 
        txtNameInType.Visible = True 
        cboNameInType.Visible = False 
      End If 
    Else 
      txtNameInType.Visible = True 
      cboNameInType.Visible = False 
    End If 
 
    Application.DoEvents() 
  End Sub 
 
  Private Sub ctlc_User_evtControlsRefreshed(vInEdit As Boolean, vUser As csUser) Handles Me.evtControlsRefreshed 
    If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdatePasswordHashed, _Requester) = True Then 
      If ForceUserToChangePassword() = True Then 
        btnPasswordHashedUpdate.Text = "Set or Reset" 
      Else 
        btnPasswordHashedUpdate.Text = "Set" 
      End If 
    Else 
      If ForceUserToChangePassword() = True Then 
        btnPasswordHashedUpdate.Text = "Reset" 
      Else 
        btnPasswordHashedUpdate.Visible = False 
      End If 
    End If 
    If MyController.IsAuthenticationDoneOnExternalSystem AndAlso Not (_Requester.IsInRole("Master") OrElse _Requester.IsInRole("ApplicationMaster")) Then 
      txtLastName.ReadOnly = True 
      txtFirstName.ReadOnly = True 
      txtNationalIDNo.ReadOnly = True 
      txtAddress.ReadOnly = True 
      txtCity.ReadOnly = True 
      txtProvinceState.ReadOnly = True 
      txtPostalCode.ReadOnly = True 
      txtCountry.ReadOnly = True 
      txtPhoneNumber.ReadOnly = True 
      txtEmail.ReadOnly = True 
      txtLanguage.ReadOnly = True 
      txtMessagingMode.ReadOnly = True 
 
      txtDatePasswordChanged.ReadOnly = True 
      dtpExpiryDate.Visible = False 
      btnPasswordHashedUpdate.Visible = False 
    End If 
 
    txtSecurityQuestion1Response.Hide() 
    txtSecurityQuestion2Response.Hide() 
    txtSecurityQuestion3Response.Hide() 
 
    btnViewHideSecurityQuestionResponse.Text = "View" 
 
  End Sub 
 
  Private Sub btnViewHideSecurityQuestionResponse_Click(sender As Object, e As EventArgs) Handles btnViewHideSecurityQuestionResponse.Click 
 
    If btnViewHideSecurityQuestionResponse.Text = "View" Then 
      txtSecurityQuestion1Response.Show() 
      txtSecurityQuestion2Response.Show() 
      txtSecurityQuestion3Response.Show() 
      btnViewHideSecurityQuestionResponse.Text = "Hide" 
    Else 
      txtSecurityQuestion1Response.Hide() 
      txtSecurityQuestion2Response.Hide() 
      txtSecurityQuestion3Response.Hide() 
      btnViewHideSecurityQuestionResponse.Text = "View" 
    End If 
 
  End Sub 
 
  Private Sub ctlc_User_evtAdd(vUser As csUser) Handles Me.evtAdd 
    'Set to Global automatically, if that is the only option 
    If cboType.Items.Count = 2 Then 
      If _User.Type = clsEnums.enmUserIdentityType.UD Then 
        cboType.SelectedIndex = 1 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_User_evtLoadApplicationsOptions(ByRef rApplicationsOptions As clsComboList, ByRef rPrompt As String, ByRef rFault As clsFault) Handles Me.evtLoadApplicationsOptions 
 
    Dim pSystemDefault As New csSystemDefault 
    rFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Controller_Applications, _Requester, True) 
 
    rApplicationsOptions = New clsComboList 
    Dim pApps As String() = pSystemDefault.SettingValue.Replace(ChrW(13), "").Split(ChrW(10)) 
    Dim pCntr As Integer = 0 
    For Each l In pApps 
      If l = "" Then Continue For 
      pCntr += 1 
      rApplicationsOptions.AddToEnd(pCntr, l) 
    Next 
    rApplicationsOptions.SortByText() 
 
  End Sub 
 
  'Private Sub ctlc_User_evtBeforeLoad() Handles Me.evtBeforeLoad  
  '  'sample   
  '  _LoadParameters.ReadOnly = False  
  'End Sub  
 
 
  'ctlUserPermissionColForUser  
  Private WithEvents _ctlUserPermissionColForUser As ctlc_UserPermissionCol 
  Private Sub ctlccUser_evtControlLoadedLoadUserPermissionColForUser() Handles Me.evtLoaded 
    If _ctlUserPermissionColForUser Is Nothing Then 
      _ctlUserPermissionColForUser = New ctlc_UserPermissionCol 
      _ctlUserPermissionColForUser.Dock = DockStyle.Fill 
      _ctlUserPermissionColForUser.Location = New System.Drawing.Point(0, 33) 
      _ctlUserPermissionColForUser.Size = New System.Drawing.Size(207, 340) 
      Me.gpbUserPermissionColForUser.Controls.Add(Me._ctlUserPermissionColForUser) 'Add it manually of needed
      _ctlUserPermissionColForUser.Name = "ctlc_UserPermissionColForUser" 
      '_ctlUserPermissionColForUser.lblGrid.Visible = False  
    End If 
    If _User.ID = 0 Then 
      _ctlUserPermissionColForUser.Visible = False 
    Else 
      Dim pFault As New clsFault 
      pFault = RefreshCtlUserPermissionColForUser() 
      If pFault.isOK = False Then 
        ShowFault(pFault, _Requester) 
      End If 
      _ctlUserPermissionColForUser.Visible = True 
    End If 
  End Sub 
  Private Function RefreshCtlUserPermissionColForUser() As clsFault 
    Dim pFault As New clsFault 
 
    'get selected row   
    Dim pID As Long = 0 
    If _ctlUserPermissionColForUser.dgvUserPermission.SelectedRows.Count > 0 Then 
      Dim pUserPermissionForUser As csUserPermission = CType(_ctlUserPermissionColForUser.bsCtlUserPermission.Current, csUserPermission) 
      pID = pUserPermissionForUser.ID 
    End If 
 
    _ctlUserPermissionColForUser.SuspendLayout() 
    Dim pUserPermissionColForUser As New csUserPermissionCol(clsEnums.enmLoadParent.TextOnly) 
    pFault = pUserPermissionColForUser.FillByUserID(_User.ID, _Requester, 251, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pTitle As String 
    If pUserPermissionColForUser.Count > 250 Then 
      pTitle = "Showing 1st 250 rows" 
      pUserPermissionColForUser.RemoveAt(250) 
    Else 
      pTitle = String.Format(pShowing0Rows, pUserPermissionColForUser.Count) 
    End If 
 
    Dim pGridTitle As String = "" 
    Dim pReportTitle As String = "" 
    Dim pReadOnly As Boolean = False 
    Dim pLoadCboCustomer As Boolean = True 
    Dim pLoadCboUser As Boolean = False 
 
    If pGridTitle = "" Then pGridTitle = pTitle 
 
    Dim pLoadParameters As New ctlc_UserPermissionCol.clsLoadParameters() 
    With pLoadParameters 
      .ColumnsHide.Add(csUserPermission.enmProperty.ID) 
      .ColumnsHide.Add(csUserPermission.enmProperty.User) 
      .ReportButtonHide = True 
      .SpreadsheetButtonHide = True 
      '.ColumnsListHide = True 
    End With 
    _ctlUserPermissionColForUser.Visible = True 
    pFault = _ctlUserPermissionColForUser.LoadControl(pUserPermissionColForUser, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
 
    If pID > 0 Then 
      Dim pUserPermissionColForUsers As csUserPermissionCol = CType(_ctlUserPermissionColForUser.bsCtlUserPermission.DataSource, csUserPermissionCol) 
      Dim pUserPermissionForUser As csUserPermission = pUserPermissionColForUsers.FindByID((pID)) 
      If Not (pUserPermissionForUser.IsEmpty) Then 
        _ctlUserPermissionColForUser.bsCtlUserPermission.CurrencyManager.Position = pUserPermissionColForUsers.IndexOf(pUserPermissionForUser) 
        _ctlUserPermissionColForUser.dgvUserPermission.Rows(pUserPermissionColForUsers.IndexOf(pUserPermissionForUser)).Selected = True 
      End If 
    End If 
 
    _ctlUserPermissionColForUser.ResumeLayout() 
    Application.DoEvents() 
    If pFault.isOK = False Then Return pFault 
    Return pFault 
  End Function 
  Private Sub _ctlUserPermissionColForUser_evtBeforeUpdate(ByVal vUserPermissionColForUser As csUserPermission, ByRef rCancel As Boolean) Handles _ctlUserPermissionColForUser.evtBeforeUpdate 
    vUserPermissionColForUser.UserID = _User.ID 
  End Sub 
 
  'ctlUserLoginKeyColForUser   
  'Private WithEvents MyCtlUserLoginKeyColForUser As ctlc_UserLoginKeyCol 
  Private Sub ctlccUser_evtControlLoadedLoadUserLoginKeyColForUser() Handles Me.evtLoaded 
    If MyCtlUserLoginKeyColForUser Is Nothing Then 
      MyCtlUserLoginKeyColForUser = New ctlc_UserLoginKeyCol 
      MyCtlUserLoginKeyColForUser.Dock = DockStyle.Fill 
      MyCtlUserLoginKeyColForUser.Location = New System.Drawing.Point(0, 33) 
      MyCtlUserLoginKeyColForUser.Size = New System.Drawing.Size(207, 340) 
      Me.gpbApplicationLoginKeys.Controls.Add(Me.MyCtlUserLoginKeyColForUser) 'Add it manually of needed 
      MyCtlUserLoginKeyColForUser.Name = "ctlc_UserLoginKeyColForUser" 
      'MyCtlUserLoginKeyColForUser.lblGrid.Visible = False   
    End If 
    If _User.ID = 0 Then 
      MyCtlUserLoginKeyColForUser.Visible = False 
    Else 
      Dim pFault As New clsFault 
      pFault = RefreshCtlUserLoginKeyColForUser() 
      If pFault.isOK = False Then 
        ShowFault(pFault, _Requester) 
      End If 
      MyCtlUserLoginKeyColForUser.Visible = True 
    End If 
  End Sub 
  Private Function RefreshCtlUserLoginKeyColForUser() As clsFault 
    Dim pFault As New clsFault 
 
    'get selected row    
    Dim pID As Long = 0 
    If MyCtlUserLoginKeyColForUser.dgvUserLoginKey.SelectedRows.Count > 0 Then 
      Dim pUserLoginKeyForUser As csUserLoginKey = CType(MyCtlUserLoginKeyColForUser.bsCtlUserLoginKey.Current, csUserLoginKey) 
      pID = pUserLoginKeyForUser.ID 
    End If 
 
    MyCtlUserLoginKeyColForUser.SuspendLayout() 
    Dim pUserLoginKeyColForUser As New csUserLoginKeyCol(clsEnums.enmLoadParent.TextOnly) 
    pFault = pUserLoginKeyColForUser.FillByUserID(_User.ID, _Requester, 251, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pShowing0Rows As String = CCTextTranslate("Showing {0} rows", _Requester) 
    If pShowing0Rows = "" Then 
      pShowing0Rows = "Showing {0} rows" 
    End If 
 
    Dim pTitle As String 
    If pUserLoginKeyColForUser.Count > 250 Then 
      pTitle = "Showing 1st 250 rows" 
      pUserLoginKeyColForUser.RemoveAt(250) 
    Else 
      pTitle = String.Format(pShowing0Rows, pUserLoginKeyColForUser.Count) 
    End If 
 
    Dim pGridTitle As String = "" 
    Dim pReportTitle As String = "" 
    Dim pReadOnly As Boolean = False 
    Dim pLoadCboCustomer As Boolean = True 
    Dim pLoadCboUser As Boolean = False 
 
    If pGridTitle = "" Then pGridTitle = pTitle 
 
    Dim pLoadParameters As New ctlc_UserLoginKeyCol.clsLoadParameters() 
    With pLoadParameters 
      .ColumnsHide.Add(csUserLoginKey.enmProperty.ID) 
      .ColumnsHide.Add(csUserLoginKey.enmProperty.User) 
      .ColumnsHide.Add(csUserLoginKey.enmProperty.KeyHashed) 
      .ColumnsHide.Add(csUserLoginKey.enmProperty.LoggedLoginID) 
      .ReportButtonHide = True 
      .SpreadsheetButtonHide = True 
      .ImportButtonHide = True 
      '.ColumnsListHide = True  
    End With 
    MyCtlUserLoginKeyColForUser.Visible = True 
    pFault = MyCtlUserLoginKeyColForUser.LoadControl(pUserLoginKeyColForUser, pLoadParameters, _Requester) 
    If pFault.isOK = False Then Return pFault 
 
    If pID > 0 Then 
      Dim pUserLoginKeyColForUsers As csUserLoginKeyCol = CType(MyCtlUserLoginKeyColForUser.bsCtlUserLoginKey.DataSource, csUserLoginKeyCol) 
      Dim pUserLoginKeyForUser As csUserLoginKey = pUserLoginKeyColForUsers.FindByID((pID)) 
      If Not (pUserLoginKeyForUser.IsEmpty) Then 
        MyCtlUserLoginKeyColForUser.bsCtlUserLoginKey.CurrencyManager.Position = pUserLoginKeyColForUsers.IndexOf(pUserLoginKeyForUser) 
        MyCtlUserLoginKeyColForUser.dgvUserLoginKey.Rows(pUserLoginKeyColForUsers.IndexOf(pUserLoginKeyForUser)).Selected = True 
      End If 
    End If 
 
    MyCtlUserLoginKeyColForUser.ResumeLayout() 
    Application.DoEvents() 
    If pFault.isOK = False Then Return pFault 
    Return pFault 
  End Function 
  Private Sub MyCtlUserLoginKeyColForUser_evtBeforeUpdate(ByVal vUserLoginKeyColForUser As csUserLoginKey, ByRef rCancel As Boolean) Handles MyCtlUserLoginKeyColForUser.evtBeforeUpdate 
    vUserLoginKeyColForUser.UserID = _User.ID 
  End Sub 
 
  Private Shared sForceUserToChangePassword As Nullable(Of Boolean) = Nothing 
 
  Private Function ForceUserToChangePassword() As Boolean 
 
    If sForceUserToChangePassword.HasValue Then Return sForceUserToChangePassword.Value 
 
    Dim pSystemDefault As New csSystemDefault() 
    Dim pFault As clsFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Security_ForceUserToChangePasswordOnInitialLogin, _Requester, True) 
    If Not pFault.isOK Then 
      ShowFault(pFault, _Requester) 
      Environment.Exit(0) 
    End If 
    sForceUserToChangePassword = CBool(pSystemDefault.SettingValue) 
    Return sForceUserToChangePassword.Value 
  End Function 
 
  Private Sub ccctlc_User_evtSeparateEdit(vPropertyName As csUser.enmUpdateType, ByRef rNewValue As String, ByRef rUseNewValue As Boolean, ByRef rCancelUpdate As Boolean, ByRef rNewPrompt As String, ByRef rAppendText As Boolean) Handles Me.evtSeparateEdit  
    If vPropertyName = csUser.enmUpdateType.PasswordHashed Then 
      'check if we can do it, or it has be done by mail  
 
      If ForceUserToChangePassword() = False Then Return 
 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdatePasswordHashed, _Requester) = True Then 
        Dim pButtonReturned As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("Do you want to set or reset password the password?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Reset", vNoText:="Set") 
        If pButtonReturned = frmMessageOrInputBox.enmButtonReturned.No Then 
          Return 
        ElseIf pButtonReturned = frmMessageOrInputBox.enmButtonReturned.Cancel Then 
          rCancelUpdate = True 
          Return 
        End If 
      End If 
 
      rCancelUpdate = True 'Don't allow the password to be changed by the default code 
 
      Dim pDoIt As Boolean = AreYouSure($"reset {_User.UserName}'s password") 
      If pDoIt = False Then Exit Sub 
 
      'Create a new password 
      Cursor = Cursors.WaitCursor 
      Dim pFault As New clsFault() 
      Try 
        pFault = _User.ChangePassword(_User.ID.ToString() & "AutoCreate", _Requester) 
      Catch ex As Exception 
        pFault.LogException(60, ex, "Value=" & "AutoCreate", "TRGT-200323-1612", _Requester) 
      End Try 
      If pFault.isOK = False Then 
        Cursor = Cursors.Default 
        ShowFault(pFault, _Requester) 
      Else 
        'since it's updated separately, then refresh the User  
        pFault = _User.GetByID(_User.ID, _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return 
        ControlsLoad() 
        SetUpButtons(False) 
        RaiseEvent evtUpdated(csUser.enmUpdateType.PasswordHashed, _User) 
        Cursor = Cursors.Default 
        ShowFault(pFault, _Requester) 
      End If 
 
    End If 
  End Sub 
 
  Private _IncludeDefaultRoles As Nullable(Of Boolean) = Nothing 
 
  Private Sub ctlc_User_evtOverrideLoadIntelliCombo(ByVal vParentName As csUser.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) Handles Me.evtOverrideLoadIntelliCombo 
    If vParentName = csUser.enmParentProperty.Role Then 
      Dim pFault As clsFault 
 
      If _IncludeDefaultRoles Is Nothing Then 
        Dim pSystemDefault As New csSystemDefault() 
        pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Security_IncludeDefaultRoles, _Requester, True) 
        If Not pFault.isOK() Then 
          ShowFault(pFault, _Requester) 
          rComboList = New clsComboList() 
          rPrompt = "Failed" 
          Return 
        End If 
        Dim pIncludeDefaultRoles As Boolean = False 
        Try 
          pIncludeDefaultRoles = CBool(pSystemDefault.SettingValue) 
        Catch ex As Exception 
          pIncludeDefaultRoles = False 
        End Try 
        _IncludeDefaultRoles = pIncludeDefaultRoles 
      End If 
 
      If _IncludeDefaultRoles.Value = True Then Return 
 
      If _Requester.IsInRole("Administrator") OrElse _Requester.IsInRole("UserManager") Then 
        rComboListTypeToLoad = clsEnums.enmComboListType.c_RoleWithBaseNoSysAdminDefaultByID 
      ElseIf _Requester.IsInRole("SysAdmin") OrElse _Requester.IsInRole("Master") OrElse _Requester.IsInRole("ApplicationMaster") Then 
        rComboListTypeToLoad = clsEnums.enmComboListType.c_RoleWithBaseAndMasterDefaultByID 
      Else 
        rComboListTypeToLoad = clsEnums.enmComboListType.UD 
      End If 
      Return 
    End If 
  End Sub 
 
  Private Sub tbcUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbcUser.SelectedIndexChanged 
    If _User.ID = 0 Then tbcUser.SelectedIndex = 0 
  End Sub 
 
  Private Sub btnDeleteAllKeys_Click(sender As Object, e As EventArgs) Handles btnDeleteAllKeys.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor 
    Dim pFault As New clsFault 
 
    pFault = csUserLoginKeyCol.DeleteByUserID(_User.ID, _Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    pFault = Me.LoadControl(_User.ID, _LoadParameters, _Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
    ShowFault(pFault, _Requester) 
  End Sub 
 
  Private Sub btnDeletePIN_Click(sender As Object, e As EventArgs) Handles btnDeletePIN.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor 
    Dim pFault As New clsFault 
 
    pFault = _User.UpdatePIN("", _Requester) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Cursor = Cursors.Default 
    ShowFault(pFault, _Requester) 
  End Sub 
 
  Private Sub txtRole_TextChanged(sender As Object, e As EventArgs) Handles txtRole.TextChanged 
    If _User IsNot Nothing AndAlso _User.RoleID > 0 AndAlso String.IsNullOrEmpty(txtRole.Text) Then 
      txtRole.Text = "You must choose an allowed role" 
    End If 
  End Sub 

  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlUser_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
