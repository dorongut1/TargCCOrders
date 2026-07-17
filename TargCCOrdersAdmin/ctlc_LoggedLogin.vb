Public Class ctlc_LoggedLogin
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vLoggedLogin As csLoggedLogin) 
  
  Public Event evtParentChosen(ByVal vParentName As csLoggedLogin.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csLoggedLogin.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csLoggedLogin.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csLoggedLogin.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csLoggedLogin.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csLoggedLogin.enmParentProperty) 
 
    End Sub 
  End Class 
 
  Private WithEvents _LoggedLogin As csLoggedLogin

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlLoggedLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.DoubleBuffered = True 
    If Me.DesignMode = True Then Exit Sub
    
    'buttons
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
  End Sub

  Public Function LoadControl(ByVal vLoggedLoginID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pLoggedLogin As New csLoggedLogin() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vLoggedLoginID <> 0 Then 
      pFault = pLoggedLogin.GetByID(vLoggedLoginID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pLoggedLogin) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rLoggedLogin As csLoggedLogin, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rLoggedLogin)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rLoggedLogin As csLoggedLogin) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _LoggedLogin = rLoggedLogin 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboUserIdentityType.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      pFault = LoadCboUserIdentityType() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboUserIdentityType() : If pFault.isOK = False Then Return pFault 
 
    'Parents
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rLoggedLogin"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rLoggedLogin As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rLoggedLogin.GetType.Name = "csLoggedLogin" Then 
      ctlLoggedLogin_Load(Nothing, Nothing) 
      Dim pLoggedLogin As csLoggedLogin = CType(rLoggedLogin, csLoggedLogin) 
      Return LoadControl(pLoggedLogin) 
    Else 
      Dim pLoggedLoginID As Long = CType(rLoggedLogin, Long) 
      Return LoadControl(pLoggedLoginID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UserName", _Requester) 
    If pStrg <> "" Then lblUserName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UserFullName", _Requester) 
    If pStrg <> "" Then lblUserFullName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "TimeLoggedIn", _Requester) 
    If pStrg <> "" Then lblTimeLoggedIn.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ApplicationName", _Requester) 
    If pStrg <> "" Then lblApplicationName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UserIdentityType", _Requester) 
    If pStrg <> "" Then lblUserIdentityType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UserIdentityTypeName", _Requester) 
    If pStrg <> "" Then lblUserIdentityTypeName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "Roles", _Requester) 
    If pStrg <> "" Then lblRoles.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "TimeLoggedOut", _Requester) 
    If pStrg <> "" Then lblTimeLoggedOut.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "LoginFaultNumber", _Requester) 
    If pStrg <> "" Then lblLoginFaultNumber.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "EnvironmentUserName", _Requester) 
    If pStrg <> "" Then lblEnvironmentUserName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "EnvironmentMachineName", _Requester) 
    If pStrg <> "" Then lblEnvironmentMachineName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "EnvironmentUserDomainName", _Requester) 
    If pStrg <> "" Then lblEnvironmentUserDomainName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "DnsGetHostName", _Requester) 
    If pStrg <> "" Then lblDnsGetHostName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "AddressList", _Requester) 
    If pStrg <> "" Then lblAddressList.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ComputerMACAddress", _Requester) 
    If pStrg <> "" Then lblComputerMACAddress.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "SystemDiskVolumeSerialNo", _Requester) 
    If pStrg <> "" Then lblSystemDiskVolumeSerialNo.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "LocalTime", _Requester) 
    If pStrg <> "" Then lblLocalTime.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "GmtTime", _Requester) 
    If pStrg <> "" Then lblGmtTime.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "AccessingComputerDetails", _Requester) 
    If pStrg <> "" Then lblAccessingComputerDetails.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UICulture", _Requester) 
    If pStrg <> "" Then lblUICulture.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "TotalPhysicalMemoryKb", _Requester) 
    If pStrg <> "" Then lblTotalPhysicalMemoryKb.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "AvailablePhysicalMemoryKb", _Requester) 
    If pStrg <> "" Then lblAvailablePhysicalMemoryKb.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ApplicationVersion", _Requester) 
    If pStrg <> "" Then lblApplicationVersion.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "OriginatingIP", _Requester) 
    If pStrg <> "" Then lblOriginatingIP.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "Language", _Requester) 
    If pStrg <> "" Then lblLanguage.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "HostingAssembly", _Requester) 
    If pStrg <> "" Then lblHostingAssembly.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "OriginatingCountry", _Requester) 
    If pStrg <> "" Then lblOriginatingCountry.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "DateLoggedIn", _Requester) 
    If pStrg <> "" Then lblDateLoggedIn.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "MonthLoggedIn", _Requester) 
    If pStrg <> "" Then lblMonthLoggedIn.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ClientReportedIP", _Requester) 
    If pStrg <> "" Then lblClientReportedIP.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ClientReportedCountry", _Requester) 
    If pStrg <> "" Then lblClientReportedCountry.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "IPAdditionalDetails", _Requester) 
    If pStrg <> "" Then lblIPAdditionalDetails.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [LoggedLogin]() As csLoggedLogin
    Get 
      Return _LoggedLogin 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboUserIdentityType() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboUserIdentityType.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLoggedLogin.enmParentProperty.UserIdentityType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.UserIdentityType, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText() 
    Else
      pComboList = pTestCol
    End If

    cboUserIdentityType.Tag = "" 
    pFault = LoadCbo(cboUserIdentityType, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _LoggedLogin.UserIdentityTypeCode <> "" Then cboUserIdentityType.SelectedValue = _LoggedLogin.UserIdentityTypeCode

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboUserIdentityTypeName(ByVal vUserIdentityTypeCode As String) As clsFault 
    Dim pFault As clsFault 
 
    'If cboUserIdentityTypeName.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded  
 
    Dim pComboList As clsComboList 
 
    'enable using an external list if needed   
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLoggedLogin.enmParentProperty.UserIdentityTypeName, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList() 
      If String.IsNullOrEmpty(vUserIdentityTypeCode) = False AndAlso Not vUserIdentityTypeCode.Equals("Global", StringComparison.OrdinalIgnoreCase) Then 
        Dim pComboListType As clsEnums.enmComboListType = clsEnums.TranslateEnmComboListType($"cc{vUserIdentityTypeCode}DefaultByID") 
        Dim pNonLookupCombolist = New clsComboList() 
        pFault = pNonLookupCombolist.Fill(pComboListType, _Requester) : If Not pFault.isOK Then Return pFault 
        pComboList = New clsComboList() 
        For Each l In pNonLookupCombolist 
          pComboList.Add(New clsComboListMember(ccHelper.ToInteger(l.KeyLong), l.Text)) 
        Next 
      Else 
        pFault = New clsFault 
        pFault.SetOK() 
      End If 
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText() 
    Else
      pComboList = pTestCol
    End If

    cboUserIdentityTypeName.Tag = "Numeric" 
    pFault = LoadCbo(cboUserIdentityTypeName, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _LoggedLogin.UserIdentityTypeNameCode <> -1 Then cboUserIdentityTypeName.SelectedValue = _LoggedLogin.UserIdentityTypeNameCode

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboLanguage() As clsFault
    Dim pFault As New clsFault
 
    'If cboLanguage.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pLanguagees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLoggedLogin.enmParentProperty.Language, pTestCol, pPrompt) 
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

    cboLanguage.SelectedValue = _LoggedLogin.Language 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboUserIdentityType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserIdentityType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboUserIdentityType.SelectedValue, String) 
    Dim pFault As clsFault = LoadCboUserIdentityTypeName(pCode) 
    'RaiseEvent evtCboSelectedIndexChanged(csLoggedLogin.enmParentProperty.UserIdentityType, pCode) 
  End Sub 
  Private Sub cboUserIdentityTypeName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserIdentityTypeName.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboUserIdentityTypeName.SelectedValue, String) 
    RaiseEvent evtCboSelectedIndexChanged(csLoggedLogin.enmParentProperty.UserIdentityTypeName, pCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csLoggedLogin.enmParentProperty = csLoggedLogin.enmParentProperty.UD 
    
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
    txtUserName.ReadOnly = Not (vInEdit)
    txtUserName.BackColor = pDefaultColour 
    txtUserFullName.ReadOnly = Not (vInEdit)
    txtUserFullName.BackColor = pDefaultColour 
    txtTimeLoggedIn.Visible = True 
    txtTimeLoggedIn.BackColor = pReadonlyColour 
    txtTimeLoggedIn.ReadOnly = True
    txtTimeLoggedIn.ForeColor = SetForeColor(vInEdit) 
    txtApplicationName.ReadOnly = Not (vInEdit)
    txtApplicationName.BackColor = pDefaultColour 
    txtUserIdentityType.ReadOnly = True
    txtUserIdentityType.Visible = Not (vInEdit)
    txtUserIdentityType.BackColor = pReadonlyColour 
    txtUserIdentityType.ForeColor = SetForeColor(vInEdit) 
    cboUserIdentityType.Visible = vInEdit
    txtUserIdentityTypeName.ReadOnly = True
    txtUserIdentityTypeName.Visible = Not (vInEdit)
    txtUserIdentityTypeName.BackColor = pReadonlyColour 
    txtUserIdentityTypeName.ForeColor = SetForeColor(vInEdit) 
    cboUserIdentityTypeName.Visible = vInEdit
    txtRoles.ReadOnly = Not (vInEdit)
    txtRoles.BackColor = pDefaultColour 
    txtTimeLoggedOut.Visible = True 
    txtTimeLoggedOut.BackColor = pReadonlyColour 
    txtTimeLoggedOut.ReadOnly = True
    txtTimeLoggedOut.ForeColor = SetForeColor(vInEdit) 
    txtLoginFaultNumber.ReadOnly = Not (vInEdit)
    txtLoginFaultNumber.BackColor = pDefaultColour 
    txtEnvironmentUserName.ReadOnly = Not (vInEdit)
    txtEnvironmentUserName.BackColor = pDefaultColour 
    txtEnvironmentMachineName.ReadOnly = Not (vInEdit)
    txtEnvironmentMachineName.BackColor = pDefaultColour 
    txtEnvironmentUserDomainName.ReadOnly = Not (vInEdit)
    txtEnvironmentUserDomainName.BackColor = pDefaultColour 
    txtDnsGetHostName.ReadOnly = Not (vInEdit)
    txtDnsGetHostName.BackColor = pDefaultColour 
    txtAddressList.ReadOnly = Not (vInEdit)
    txtAddressList.BackColor = pDefaultColour 
    txtComputerMACAddress.ReadOnly = Not (vInEdit)
    txtComputerMACAddress.BackColor = pDefaultColour 
    txtSystemDiskVolumeSerialNo.ReadOnly = Not (vInEdit)
    txtSystemDiskVolumeSerialNo.BackColor = pDefaultColour 
    txtLocalTime.Visible = True 
    txtLocalTime.BackColor = pReadonlyColour 
    txtLocalTime.ReadOnly = True
    txtLocalTime.ForeColor = SetForeColor(vInEdit) 
    txtGmtTime.Visible = True 
    txtGmtTime.BackColor = pReadonlyColour 
    txtGmtTime.ReadOnly = True
    txtGmtTime.ForeColor = SetForeColor(vInEdit) 
    txtAccessingComputerDetails.ReadOnly = Not (vInEdit)
    txtAccessingComputerDetails.BackColor = pDefaultColour 
    txtUICulture.ReadOnly = Not (vInEdit)
    txtUICulture.BackColor = pDefaultColour 
    txtTotalPhysicalMemoryKb.ReadOnly = Not (vInEdit)
    txtTotalPhysicalMemoryKb.BackColor = pDefaultColour 
    txtAvailablePhysicalMemoryKb.ReadOnly = Not (vInEdit)
    txtAvailablePhysicalMemoryKb.BackColor = pDefaultColour 
    txtApplicationVersion.ReadOnly = Not (vInEdit)
    txtApplicationVersion.BackColor = pDefaultColour 
    txtOriginatingIP.ReadOnly = Not (vInEdit)
    txtOriginatingIP.BackColor = pDefaultColour 
    txtLanguage.ReadOnly = True
    txtLanguage.Visible = Not (vInEdit)
    txtLanguage.BackColor = pReadonlyColour 
    txtLanguage.ForeColor = SetForeColor(vInEdit) 
    cboLanguage.Visible = vInEdit
    txtHostingAssembly.ReadOnly = Not (vInEdit)
    txtHostingAssembly.BackColor = pDefaultColour 
    txtOriginatingCountry.ReadOnly = Not (vInEdit)
    txtOriginatingCountry.BackColor = pDefaultColour 
    txtDateLoggedIn.ReadOnly = True 
    txtDateLoggedIn.BackColor = pReadonlyColour 
    txtDateLoggedIn.ForeColor = SetForeColor(vInEdit) 
    txtMonthLoggedIn.ReadOnly = True 
    txtMonthLoggedIn.BackColor = pReadonlyColour 
    txtMonthLoggedIn.ForeColor = SetForeColor(vInEdit) 
    txtClientReportedIP.ReadOnly = Not (vInEdit)
    txtClientReportedIP.BackColor = pDefaultColour 
    txtClientReportedCountry.ReadOnly = Not (vInEdit)
    txtClientReportedCountry.BackColor = pDefaultColour 
    txtIPAdditionalDetails.ReadOnly = Not (vInEdit)
    txtIPAdditionalDetails.BackColor = pDefaultColour 

    RaiseEvent evtControlsRefreshed(vInEdit, _LoggedLogin) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _LoggedLogin
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtUserName.Text = .UserName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtUserName.MaxLength = 50 
      txtUserFullName.Text = .UserFullName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtUserFullName.MaxLength = 50 
      If Math.Abs(.TimeLoggedIn.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.TimeLoggedIn.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtTimeLoggedIn.Text = "" Else txtTimeLoggedIn.Text = .TimeLoggedIn.ToString(FormatFromTag(txtTimeLoggedIn, "dd-MM-yyyy HH:mm:ss"))
      txtApplicationName.Text = .ApplicationName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtApplicationName.MaxLength = 50 
      cboUserIdentityType.SelectedValue = .UserIdentityTypeCode
      txtUserIdentityType.Text = cboUserIdentityType.Text : If cboUserIdentityType.SelectedValue Is Nothing OrElse cboUserIdentityType.SelectedValue.ToString() = "" Then txtUserIdentityType.Text = ""    
      'Have to load the parent before the child, even if we load the parent again later on
      cboUserIdentityType.SelectedValue = .UserIdentityTypeCode
      cboUserIdentityTypeName.SelectedValue = .UserIdentityTypeNameCode
      txtUserIdentityTypeName.Text = cboUserIdentityTypeName.Text : If cboUserIdentityTypeName.SelectedValue Is Nothing OrElse ccHelper.ToInteger(cboUserIdentityTypeName.SelectedValue) = -1 Then txtUserIdentityTypeName.Text = ""    
      txtRoles.Text = .Roles.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtRoles.MaxLength = 250 
      If Math.Abs(.TimeLoggedOut.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.TimeLoggedOut.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtTimeLoggedOut.Text = "" Else txtTimeLoggedOut.Text = .TimeLoggedOut.ToString(FormatFromTag(txtTimeLoggedOut, "dd-MM-yyyy HH:mm:ss"))
      txtLoginFaultNumber.Text = .LoginFaultNumber.ToString(FormatFromTag(txtLoginFaultNumber, "#,##0"))
      txtEnvironmentUserName.Text = .EnvironmentUserName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtEnvironmentUserName.MaxLength = 100 
      txtEnvironmentMachineName.Text = .EnvironmentMachineName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtEnvironmentMachineName.MaxLength = 50 
      txtEnvironmentUserDomainName.Text = .EnvironmentUserDomainName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtEnvironmentUserDomainName.MaxLength = 10 
      txtDnsGetHostName.Text = .DnsGetHostName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDnsGetHostName.MaxLength = 50 
      txtAddressList.Text = .AddressList.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtAddressList.MaxLength = 100 
      txtComputerMACAddress.Text = .ComputerMACAddress.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtComputerMACAddress.MaxLength = 100 
      txtSystemDiskVolumeSerialNo.Text = .SystemDiskVolumeSerialNo.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSystemDiskVolumeSerialNo.MaxLength = 100 
      If Math.Abs(.LocalTime.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.LocalTime.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtLocalTime.Text = "" Else txtLocalTime.Text = .LocalTime.ToString(FormatFromTag(txtLocalTime, "dd-MM-yyyy HH:mm:ss"))
      If Math.Abs(.GmtTime.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.GmtTime.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtGmtTime.Text = "" Else txtGmtTime.Text = .GmtTime.ToString(FormatFromTag(txtGmtTime, "dd-MM-yyyy HH:mm:ss"))
      txtAccessingComputerDetails.Text = .AccessingComputerDetails.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtAccessingComputerDetails.MaxLength = 250 
      txtUICulture.Text = .UICulture.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtUICulture.MaxLength = 50 
      txtTotalPhysicalMemoryKb.Text = .TotalPhysicalMemoryKb.ToString(FormatFromTag(txtTotalPhysicalMemoryKb, "#,##0"))
      txtAvailablePhysicalMemoryKb.Text = .AvailablePhysicalMemoryKb.ToString(FormatFromTag(txtAvailablePhysicalMemoryKb, "#,##0"))
      txtApplicationVersion.Text = .ApplicationVersion.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtApplicationVersion.MaxLength = 250 
      txtOriginatingIP.Text = .OriginatingIP.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtOriginatingIP.MaxLength = 100 
      cboLanguage.SelectedValue = .Language
      txtLanguage.Text = cboLanguage.Text : If cboLanguage.SelectedValue Is Nothing OrElse cboLanguage.SelectedValue.ToString() = "UD" Then txtLanguage.Text = ""    
      txtHostingAssembly.Text = .HostingAssembly.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtHostingAssembly.MaxLength = 50 
      txtOriginatingCountry.Text = .OriginatingCountry.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtOriginatingCountry.MaxLength = 10 
      If Math.Abs(.DateLoggedIn.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.DateLoggedIn.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtDateLoggedIn.Text = "" Else txtDateLoggedIn.Text = .DateLoggedIn.ToString(FormatFromTag(txtDateLoggedIn, "dd-MM-yyyy"))
      If Math.Abs(.MonthLoggedIn.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.MonthLoggedIn.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtMonthLoggedIn.Text = "" Else txtMonthLoggedIn.Text = .MonthLoggedIn.ToString(FormatFromTag(txtMonthLoggedIn, "dd-MM-yyyy"))
      txtClientReportedIP.Text = .ClientReportedIP.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtClientReportedIP.MaxLength = 100 
      txtClientReportedCountry.Text = .ClientReportedCountry.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtClientReportedCountry.MaxLength = 10 
      txtIPAdditionalDetails.Text = .IPAdditionalDetails.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtIPAdditionalDetails.MaxLength = 250 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-LoggedLogin-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtLoginFaultNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLoginFaultNumber.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtLoginFaultNumber.Text 
    Dim pTest As Integer 
 
    If txtLoginFaultNumber.Text = "" Then Exit Sub 
    If txtLoginFaultNumber.Text = txtLoginFaultNumber.Name Then Exit Sub 
 
    If Integer.TryParse(txtLoginFaultNumber.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-LoggedLogin-LoginFaultNumber-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtTotalPhysicalMemoryKb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalPhysicalMemoryKb.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtTotalPhysicalMemoryKb.Text 
    Dim pTest As Long 
 
    If txtTotalPhysicalMemoryKb.Text = "" Then Exit Sub 
    If txtTotalPhysicalMemoryKb.Text = txtTotalPhysicalMemoryKb.Name Then Exit Sub 
 
    If Long.TryParse(txtTotalPhysicalMemoryKb.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-LoggedLogin-TotalPhysicalMemoryKb-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtAvailablePhysicalMemoryKb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAvailablePhysicalMemoryKb.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtAvailablePhysicalMemoryKb.Text 
    Dim pTest As Long 
 
    If txtAvailablePhysicalMemoryKb.Text = "" Then Exit Sub 
    If txtAvailablePhysicalMemoryKb.Text = txtAvailablePhysicalMemoryKb.Name Then Exit Sub 
 
    If Long.TryParse(txtAvailablePhysicalMemoryKb.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-LoggedLogin-AvailablePhysicalMemoryKb-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Ensure Read-Only

  'Now the Parents
  
 
  Private Sub ctlc_LoggedLogin_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the LoggedLogin to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pLoggedLogin As csLoggedLogin = _LoggedLogin 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pLoggedLogin.ToCSV) 
        Else 
          Clipboard.SetText(pLoggedLogin.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The LoggedLogin is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_LoggedLogin_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlLoggedLogin_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
