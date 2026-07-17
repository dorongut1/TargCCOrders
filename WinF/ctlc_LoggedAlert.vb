Public Class ctlc_LoggedAlert
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vLoggedAlert As csLoggedAlert) 
  
  Public Event evtParentChosen(ByVal vParentName As csLoggedAlert.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csLoggedAlert.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csLoggedAlert.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csLoggedAlert.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csLoggedAlert.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csLoggedAlert.enmParentProperty) 
      _EnableParentLinks.Add(csLoggedAlert.enmParentProperty.AffectedUser) 
      _EnableParentLinks.Add(csLoggedAlert.enmParentProperty.LoggedLogin) 
 
    End Sub 
  End Class 
 
  Private WithEvents _LoggedAlert As csLoggedAlert

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlLoggedAlert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Function LoadControl(ByVal vLoggedAlertID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pLoggedAlert As New csLoggedAlert(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vLoggedAlertID <> 0 Then 
      pFault = pLoggedAlert.GetByID(vLoggedAlertID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pLoggedAlert) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rLoggedAlert As csLoggedAlert, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rLoggedAlert)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rLoggedAlert As csLoggedAlert) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _LoggedAlert = rLoggedAlert 

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
      MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.Previous) 
      MyCache.SetLevel(clsEnums.enmComboListType.c_LoggedLoginDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      pFault = LoadCboUserIdentityType() : If pFault.isOK = False Then Return pFault 
      'EnumCombos
      pFault = LoadCboFaultType() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboFaultSeverity() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboAffectedUser() : If pFault.isOK = False Then Return pFault 
    pFault = LoadCboLoggedLogin() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rLoggedAlert"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rLoggedAlert As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rLoggedAlert.GetType.Name = "csLoggedAlert" Then 
      ctlLoggedAlert_Load(Nothing, Nothing) 
      Dim pLoggedAlert As csLoggedAlert = CType(rLoggedAlert, csLoggedAlert) 
      Return LoadControl(pLoggedAlert) 
    Else 
      Dim pLoggedAlertID As Long = CType(rLoggedAlert, Long) 
      Return LoadControl(pLoggedAlertID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "TimeOccurred", _Requester) 
    If pStrg <> "" Then lblTimeOccurred.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultNumber", _Requester) 
    If pStrg <> "" Then lblFaultNumber.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "SystemName", _Requester) 
    If pStrg <> "" Then lblSystemName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "CallingApplication", _Requester) 
    If pStrg <> "" Then lblCallingApplication.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "AffectedUser", _Requester) 
    If pStrg <> "" Then lblAffectedUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "CallingApplicationVersion", _Requester) 
    If pStrg <> "" Then lblCallingApplicationVersion.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "CallingFunctionWithinApplication", _Requester) 
    If pStrg <> "" Then lblCallingFunctionWithinApplication.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FreeText", _Requester) 
    If pStrg <> "" Then lblFreeText.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultingAssembly", _Requester) 
    If pStrg <> "" Then lblFaultingAssembly.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "AssemblyEntryPoint", _Requester) 
    If pStrg <> "" Then lblAssemblyEntryPoint.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultingClass", _Requester) 
    If pStrg <> "" Then lblFaultingClass.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultingFunction", _Requester) 
    If pStrg <> "" Then lblFaultingFunction.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultingFunctionParameters", _Requester) 
    If pStrg <> "" Then lblFaultingFunctionParameters.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultIdent", _Requester) 
    If pStrg <> "" Then lblFaultIdent.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultDescription", _Requester) 
    If pStrg <> "" Then lblFaultDescription.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "MessageSentToUser", _Requester) 
    If pStrg <> "" Then lblMessageSentToUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "ActionSentToUser", _Requester) 
    If pStrg <> "" Then lblActionSentToUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultType", _Requester) 
    If pStrg <> "" Then lblFaultType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultSeverity", _Requester) 
    If pStrg <> "" Then lblFaultSeverity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "LoggedLogin", _Requester) 
    If pStrg <> "" Then lblLoggedLogin.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "Thread", _Requester) 
    If pStrg <> "" Then lblThread.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "UserIdentityType", _Requester) 
    If pStrg <> "" Then lblUserIdentityType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "UserIdentityTypeName", _Requester) 
    If pStrg <> "" Then lblUserIdentityTypeName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "DateOccurred", _Requester) 
    If pStrg <> "" Then lblDateOccurred.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "MonthOccurred", _Requester) 
    If pStrg <> "" Then lblMonthOccurred.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [LoggedAlert]() As csLoggedAlert
    Get 
      Return _LoggedAlert 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboAffectedUser() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_UserDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csLoggedAlert.enmParentProperty.AffectedUser, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboAffectedUser.MakeSmart() Else cboAffectedUser.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboAffectedUser.LoadControl(pComboList, pPrompt) 
    Else 
      cboAffectedUser.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _LoggedAlert.AffectedUserID > 0 Then cboAffectedUser.ValueSelect(_LoggedAlert.AffectedUserID) Else cboAffectedUser.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboFaultType() As clsFault
    Dim pFault As New clsFault
 
    'If cboFaultType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pFaultTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.FaultType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pFaultTypees.FillEnums(clsEnums.enmEnum.FaultType, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pFaultTypees = pTestCol
    End If
    
    pFaultTypees.Remove(pFaultTypees.FindByKey(clsEnums.enmFaultType.UD))
    pFaultTypees.SortByText()
    pFaultTypees.AddToTop(clsEnums.enmFaultType.UD, GetChoose(_Requester))

    With cboFaultType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pFaultTypees
    End With

    cboFaultType.SelectedValue = _LoggedAlert.FaultType 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboFaultSeverity() As clsFault
    Dim pFault As New clsFault
 
    'If cboFaultSeverity.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pFaultSeverityes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.FaultSeverity, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pFaultSeverityes.FillEnums(clsEnums.enmEnum.FaultSeverity, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pFaultSeverityes = pTestCol
    End If
    
    pFaultSeverityes.Remove(pFaultSeverityes.FindByKey(clsEnums.enmFaultSeverity.UD))
    pFaultSeverityes.SortByText()
    pFaultSeverityes.AddToTop(clsEnums.enmFaultSeverity.UD, GetChoose(_Requester))

    With cboFaultSeverity
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pFaultSeverityes
    End With

    cboFaultSeverity.SelectedValue = _LoggedAlert.FaultSeverity 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboLoggedLogin() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_LoggedLoginDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csLoggedAlert.enmParentProperty.LoggedLogin, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboLoggedLogin.MakeSmart() Else cboLoggedLogin.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboLoggedLogin.LoadControl(pComboList, pPrompt) 
    Else 
      cboLoggedLogin.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _LoggedAlert.LoggedLoginID > 0 Then cboLoggedLogin.ValueSelect(_LoggedAlert.LoggedLoginID) Else cboLoggedLogin.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboUserIdentityType() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboUserIdentityType.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.UserIdentityType, pTestCol, pPrompt) 
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

    If _LoggedAlert.UserIdentityTypeCode <> "" Then cboUserIdentityType.SelectedValue = _LoggedAlert.UserIdentityTypeCode

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboUserIdentityTypeName(ByVal vUserIdentityTypeCode As String) As clsFault 
    Dim pFault As clsFault 
 
    'If cboUserIdentityTypeName.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded  
 
    Dim pComboList As clsComboList 
 
    'enable using an external list if needed   
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.UserIdentityTypeName, pTestCol, pPrompt) 
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

    If _LoggedAlert.UserIdentityTypeNameCode <> -1 Then cboUserIdentityTypeName.SelectedValue = _LoggedAlert.UserIdentityTypeNameCode

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboAffectedUser_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboAffectedUser.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csLoggedAlert.enmParentProperty.AffectedUser, pUniqueCode) 
  End Sub 
  Private Sub cboLoggedLogin_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboLoggedLogin.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csLoggedAlert.enmParentProperty.LoggedLogin, pUniqueCode) 
  End Sub 
  Private Sub cboUserIdentityType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserIdentityType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboUserIdentityType.SelectedValue, String) 
    Dim pFault As clsFault = LoadCboUserIdentityTypeName(pCode) 
    'RaiseEvent evtCboSelectedIndexChanged(csLoggedAlert.enmParentProperty.UserIdentityType, pCode) 
  End Sub 
  Private Sub cboUserIdentityTypeName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserIdentityTypeName.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboUserIdentityTypeName.SelectedValue, String) 
    RaiseEvent evtCboSelectedIndexChanged(csLoggedAlert.enmParentProperty.UserIdentityTypeName, pCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csLoggedAlert.enmParentProperty = csLoggedAlert.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.AffectedUser) = csLoggedAlert.enmParentProperty.AffectedUser Then 
      lblAffectedUser.ForeColor = Color.Brown 
    End If 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.LoggedLogin) = csLoggedAlert.enmParentProperty.LoggedLogin Then 
      lblLoggedLogin.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    txtTimeOccurred.Visible = True 
    txtTimeOccurred.BackColor = pReadonlyColour 
    txtTimeOccurred.ReadOnly = True
    txtTimeOccurred.ForeColor = SetForeColor(vInEdit) 
    txtFaultNumber.ReadOnly = Not (vInEdit)
    txtFaultNumber.BackColor = pDefaultColour 
    txtSystemName.ReadOnly = Not (vInEdit)
    txtSystemName.BackColor = pDefaultColour 
    txtCallingApplication.ReadOnly = Not (vInEdit)
    txtCallingApplication.BackColor = pDefaultColour 
    If vInEdit = False Then 
      txtAffectedUser.ReadOnly = True
      txtAffectedUser.Visible = True
      txtAffectedUser.BackColor = pReadonlyColour
      txtAffectedUser.ForeColor = SetForeColor(vInEdit) 
      cboAffectedUser.Visible = False 
    Else 
      txtAffectedUser.ReadOnly = True
      txtAffectedUser.Visible = Not (vInEdit)
      txtAffectedUser.BackColor = pReadonlyColour 
      txtAffectedUser.ForeColor = SetForeColor(vInEdit) 
      cboAffectedUser.Visible = vInEdit
    End If  
    txtCallingApplicationVersion.ReadOnly = Not (vInEdit)
    txtCallingApplicationVersion.BackColor = pDefaultColour 
    txtCallingFunctionWithinApplication.ReadOnly = Not (vInEdit)
    txtCallingFunctionWithinApplication.BackColor = pDefaultColour 
    txtFreeText.ReadOnly = Not (vInEdit)
    txtFreeText.BackColor = pDefaultColour 
    txtFaultingAssembly.ReadOnly = Not (vInEdit)
    txtFaultingAssembly.BackColor = pDefaultColour 
    txtAssemblyEntryPoint.ReadOnly = Not (vInEdit)
    txtAssemblyEntryPoint.BackColor = pDefaultColour 
    txtFaultingClass.ReadOnly = Not (vInEdit)
    txtFaultingClass.BackColor = pDefaultColour 
    txtFaultingFunction.ReadOnly = Not (vInEdit)
    txtFaultingFunction.BackColor = pDefaultColour 
    txtFaultingFunctionParameters.ReadOnly = Not (vInEdit)
    txtFaultingFunctionParameters.BackColor = pDefaultColour 
    txtFaultIdent.ReadOnly = Not (vInEdit)
    txtFaultIdent.BackColor = pDefaultColour 
    txtFaultDescription.ReadOnly = Not (vInEdit)
    txtFaultDescription.BackColor = pDefaultColour 
    txtMessageSentToUser.ReadOnly = Not (vInEdit)
    txtMessageSentToUser.BackColor = pDefaultColour 
    txtActionSentToUser.ReadOnly = Not (vInEdit)
    txtActionSentToUser.BackColor = pDefaultColour 
    txtFaultType.ReadOnly = True
    txtFaultType.Visible = Not (vInEdit)
    txtFaultType.BackColor = pReadonlyColour 
    txtFaultType.ForeColor = SetForeColor(vInEdit) 
    cboFaultType.Visible = vInEdit
    txtFaultSeverity.ReadOnly = True
    txtFaultSeverity.Visible = Not (vInEdit)
    txtFaultSeverity.BackColor = pReadonlyColour 
    txtFaultSeverity.ForeColor = SetForeColor(vInEdit) 
    cboFaultSeverity.Visible = vInEdit
    If vInEdit = False Then 
      txtLoggedLogin.ReadOnly = True
      txtLoggedLogin.Visible = True
      txtLoggedLogin.BackColor = pReadonlyColour
      txtLoggedLogin.ForeColor = SetForeColor(vInEdit) 
      cboLoggedLogin.Visible = False 
    Else 
      txtLoggedLogin.ReadOnly = True
      txtLoggedLogin.Visible = Not (vInEdit)
      txtLoggedLogin.BackColor = pReadonlyColour 
      txtLoggedLogin.ForeColor = SetForeColor(vInEdit) 
      cboLoggedLogin.Visible = vInEdit
    End If  
    txtThread.ReadOnly = Not (vInEdit)
    txtThread.BackColor = pDefaultColour 
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
    txtDateOccurred.ReadOnly = True 
    txtDateOccurred.BackColor = pReadonlyColour 
    txtDateOccurred.ForeColor = SetForeColor(vInEdit) 
    txtMonthOccurred.ReadOnly = True 
    txtMonthOccurred.BackColor = pReadonlyColour 
    txtMonthOccurred.ForeColor = SetForeColor(vInEdit) 

    RaiseEvent evtControlsRefreshed(vInEdit, _LoggedAlert) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _LoggedAlert
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      If Math.Abs(.TimeOccurred.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.TimeOccurred.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtTimeOccurred.Text = "" Else txtTimeOccurred.Text = .TimeOccurred.ToString(FormatFromTag(txtTimeOccurred, "dd-MM-yyyy HH:mm:ss"))
      txtFaultNumber.Text = .FaultNumber.ToString(FormatFromTag(txtFaultNumber, "#,##0"))
      txtSystemName.Text = .SystemName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSystemName.MaxLength = 50 
      txtCallingApplication.Text = .CallingApplication.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCallingApplication.MaxLength = 50 
      txtAffectedUser.Text = .AffectedUserText 
      txtCallingApplicationVersion.Text = .CallingApplicationVersion.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCallingApplicationVersion.MaxLength = 50 
      txtCallingFunctionWithinApplication.Text = .CallingFunctionWithinApplication.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCallingFunctionWithinApplication.MaxLength = 100 
      txtFreeText.Text = .FreeText.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFaultingAssembly.Text = .FaultingAssembly.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFaultingAssembly.MaxLength = 100 
      txtAssemblyEntryPoint.Text = .AssemblyEntryPoint.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtAssemblyEntryPoint.MaxLength = 100 
      txtFaultingClass.Text = .FaultingClass.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFaultingClass.MaxLength = 50 
      txtFaultingFunction.Text = .FaultingFunction.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFaultingFunction.MaxLength = 100 
      txtFaultingFunctionParameters.Text = .FaultingFunctionParameters.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFaultIdent.Text = .FaultIdent.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFaultIdent.MaxLength = 100 
      txtFaultDescription.Text = .FaultDescription.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFaultDescription.MaxLength = 100 
      txtMessageSentToUser.Text = .MessageSentToUser.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtMessageSentToUser.MaxLength = 100 
      txtActionSentToUser.Text = .ActionSentToUser.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtActionSentToUser.MaxLength = 200 
      cboFaultType.SelectedValue = .FaultType
      txtFaultType.Text = cboFaultType.Text : If cboFaultType.SelectedValue Is Nothing OrElse cboFaultType.SelectedValue.ToString() = "UD" Then txtFaultType.Text = ""    
      cboFaultSeverity.SelectedValue = .FaultSeverity
      txtFaultSeverity.Text = cboFaultSeverity.Text : If cboFaultSeverity.SelectedValue Is Nothing OrElse cboFaultSeverity.SelectedValue.ToString() = "UD" Then txtFaultSeverity.Text = ""    
      txtLoggedLogin.Text = .LoggedLoginText 
      txtThread.Text = .Thread.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtThread.MaxLength = 50 
      cboUserIdentityType.SelectedValue = .UserIdentityTypeCode
      txtUserIdentityType.Text = cboUserIdentityType.Text : If cboUserIdentityType.SelectedValue Is Nothing OrElse cboUserIdentityType.SelectedValue.ToString() = "" Then txtUserIdentityType.Text = ""    
      'Have to load the parent before the child, even if we load the parent again later on
      cboUserIdentityType.SelectedValue = .UserIdentityTypeCode
      cboUserIdentityTypeName.SelectedValue = .UserIdentityTypeNameCode
      txtUserIdentityTypeName.Text = cboUserIdentityTypeName.Text : If cboUserIdentityTypeName.SelectedValue Is Nothing OrElse ccHelper.ToInteger(cboUserIdentityTypeName.SelectedValue) = -1 Then txtUserIdentityTypeName.Text = ""    
      If Math.Abs(.DateOccurred.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.DateOccurred.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtDateOccurred.Text = "" Else txtDateOccurred.Text = .DateOccurred.ToString(FormatFromTag(txtDateOccurred, "dd-MM-yyyy"))
      If Math.Abs(.MonthOccurred.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.MonthOccurred.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtMonthOccurred.Text = "" Else txtMonthOccurred.Text = .MonthOccurred.ToString(FormatFromTag(txtMonthOccurred, "dd-MM-yyyy"))
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-LoggedAlert-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtFaultNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFaultNumber.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtFaultNumber.Text 
    Dim pTest As Integer 
 
    If txtFaultNumber.Text = "" Then Exit Sub 
    If txtFaultNumber.Text = txtFaultNumber.Name Then Exit Sub 
 
    If Integer.TryParse(txtFaultNumber.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-LoggedAlert-FaultNumber-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblAffectedUser_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblAffectedUser.DoubleClick 
    If _LoggedAlert.AffectedUserID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.AffectedUser) = csLoggedAlert.enmParentProperty.AffectedUser Then 
      If _LoggedAlert.AffectedUserID <> 0 Then RaiseEvent evtParentChosen(csLoggedAlert.enmParentProperty.AffectedUser, _LoggedAlert.AffectedUserID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "AffectedUser Detail" 
      fPopup.LoadControl("ctlc_User", _LoggedAlert.AffectedUserID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblAffectedUser_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblAffectedUser.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.AffectedUser) <> csLoggedAlert.enmParentProperty.AffectedUser Then Exit Sub 
    lblAffectedUser.ForeColor = Color.Brown 
    'lblAffectedUser.Font = New Font(lblAffectedUser.Font.Name, lblAffectedUser.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblAffectedUser.BackColor = ccHelper.InvertColour(lblAffectedUser.ForeColor) 'did this instead 
    lblAffectedUser.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblAffectedUser_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblAffectedUser.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.AffectedUser) <> csLoggedAlert.enmParentProperty.AffectedUser Then Exit Sub 
    lblAffectedUser.ForeColor = Color.Brown 
    'lblAffectedUser.Font = New Font(lblAffectedUser.Font.Name, lblAffectedUser.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblAffectedUser.BackColor = Me.BackColor 'did this instead 
    lblAffectedUser.Cursor = Cursors.Default 
  End Sub 
 
  Private Sub lblLoggedLogin_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedLogin.DoubleClick 
    If _LoggedAlert.LoggedLoginID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.LoggedLogin) = csLoggedAlert.enmParentProperty.LoggedLogin Then 
      If _LoggedAlert.LoggedLoginID <> 0 Then RaiseEvent evtParentChosen(csLoggedAlert.enmParentProperty.LoggedLogin, _LoggedAlert.LoggedLoginID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "LoggedLogin Detail" 
      fPopup.LoadControl("ctlc_LoggedLogin", _LoggedAlert.LoggedLoginID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblLoggedLogin_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedLogin.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.LoggedLogin) <> csLoggedAlert.enmParentProperty.LoggedLogin Then Exit Sub 
    lblLoggedLogin.ForeColor = Color.Brown 
    'lblLoggedLogin.Font = New Font(lblLoggedLogin.Font.Name, lblLoggedLogin.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblLoggedLogin.BackColor = ccHelper.InvertColour(lblLoggedLogin.ForeColor) 'did this instead 
    lblLoggedLogin.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblLoggedLogin_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedLogin.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.LoggedLogin) <> csLoggedAlert.enmParentProperty.LoggedLogin Then Exit Sub 
    lblLoggedLogin.ForeColor = Color.Brown 
    'lblLoggedLogin.Font = New Font(lblLoggedLogin.Font.Name, lblLoggedLogin.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblLoggedLogin.BackColor = Me.BackColor 'did this instead 
    lblLoggedLogin.Cursor = Cursors.Default 
  End Sub 
 
  
 
  Private Sub ctlc_LoggedAlert_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the LoggedAlert to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pLoggedAlert As csLoggedAlert = _LoggedAlert 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pLoggedAlert.ToCSV) 
        Else 
          Clipboard.SetText(pLoggedAlert.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The LoggedAlert is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_LoggedAlert_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  'Specific Code for LoggedAlert START 
  Private Sub lblUserIdentityTypeName_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUserIdentityTypeName.DoubleClick 
    Cursor = Cursors.WaitCursor 
 
    frmPopup.Text = _LoggedAlert.UserIdentityTypeCode & " Detail" 
    If _LoggedAlert.UserIdentityTypeCode.Equals("Customer") Then 
      frmPopup.LoadControl("ctlccCustomer", _LoggedAlert.UserIdentityTypeNameCode, _Requester, True) 
    ElseIf _LoggedAlert.UserIdentityTypeCode.Equals("User") Then 
      frmPopup.LoadControl("ctlccUser", _LoggedAlert.UserIdentityTypeNameCode, _Requester, True) 
    End If 
    Cursor = Cursors.Default 
    frmPopup.ShowDialog() 
 
  End Sub 
  Private Sub lblUserIdentityTypeName_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUserIdentityTypeName.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.AffectedUser) <> csLoggedAlert.enmParentProperty.AffectedUser Then Exit Sub 
    lblUserIdentityTypeName.ForeColor = Color.Brown  
    'lblUserIdentityTypeName.Font = New Font(lblUserIdentityTypeName.Font.Name, lblUserIdentityTypeName.Font.SizeInPoints, FontStyle.Underline) 
    lblUserIdentityTypeName.BackColor = ccHelper.InvertColour(lblUserIdentityTypeName.ForeColor)  
    lblUserIdentityTypeName.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblUserIdentityTypeName_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUserIdentityTypeName.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedAlert.enmParentProperty.AffectedUser) <> csLoggedAlert.enmParentProperty.AffectedUser Then Exit Sub 
    lblUserIdentityTypeName.ForeColor = Color.Brown 
    'lblUserIdentityTypeName.Font = New Font(lblUserIdentityTypeName.Font.Name, lblUserIdentityTypeName.Font.SizeInPoints) 
    lblUserIdentityTypeName.BackColor = Me.BackColor  
    lblUserIdentityTypeName.Cursor = Cursors.Default 
  End Sub 
 
  Private Sub ctlc_LoggedAlert_ccevtLoaded() Handles Me.evtLoaded 
    lblUserIdentityTypeName.ForeColor = Color.Brown 
 
    If txtUserIdentityType.Text = txtUserIdentityTypeName.Text OrElse txtUserIdentityTypeName.Text = "0"  OrElse txtUserIdentityTypeName.Text = "" Then 
      lblUserIdentityType.Visible = True 
      txtUserIdentityType.Visible = True 
      lblUserIdentityType.Location = lblUserIdentityTypeName.Location 
      txtUserIdentityType.Location = txtUserIdentityTypeName.Location 
      lblUserIdentityType.Size = lblUserIdentityTypeName.Size 
      txtUserIdentityType.Size = txtUserIdentityTypeName.Size 
 
      lblUserIdentityTypeName.Visible = False 
      txtUserIdentityTypeName.Visible = False 
    Else 
      lblUserIdentityType.Visible = False 
      txtUserIdentityType.Visible = False 
      lblUserIdentityTypeName.Visible = True 
      txtUserIdentityTypeName.Visible = True 
      lblUserIdentityTypeName.Text = txtUserIdentityType.Text 
    End If 
 
  End Sub 
  'Specific Code for LoggedAlert END 
 

  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlLoggedAlert_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
