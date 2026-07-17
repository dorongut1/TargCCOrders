Public Class ctlccOrderHeader
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsOrderHeader.enmUpdateType) 
  Public Event evtAdd(ByVal vOrderHeader As clsOrderHeader) 
  Public Event evtBeforeUpdate(ByVal vOrderHeader As clsOrderHeader, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsOrderHeader.enmUpdateType, ByVal vOrderHeader As clsOrderHeader) 
  Public Event evtBeforeDelete(ByVal vOrderHeader As clsOrderHeader, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vOrderHeaderID As Long) 
  Public Event evtCancelledEdit(ByVal vOrderHeader As clsOrderHeader) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vOrderHeader As clsOrderHeader) 
  
  Public Event evtParentChosen(ByVal vParentName As clsOrderHeader.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsOrderHeader.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsOrderHeader.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsOrderHeader.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsOrderHeader.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsOrderHeader.enmParentProperty) 
      _EnableParentLinks.Add(clsOrderHeader.enmParentProperty.Customer) 
 
    End Sub 
  End Class 
 
  Private WithEvents _OrderHeader As clsOrderHeader

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlOrderHeader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboCustomer.Size = txtCustomer.Size
    cboCustomer.Location = txtCustomer.Location
    dtpOrderDate.Size = txtOrderDate.Size
    dtpOrderDate.Location = txtOrderDate.Location
    cboPaymentMethod.Size = txtPaymentMethod.Size
    cboPaymentMethod.Location = txtPaymentMethod.Location
    cboPaymentStatus.Size = txtPaymentStatus.Size
    cboPaymentStatus.Location = txtPaymentStatus.Location
    dtpPaymentDate.Size = txtPaymentDate.Size
    dtpPaymentDate.Location = txtPaymentDate.Location
    cboDeliveryMethod.Size = txtDeliveryMethod.Size
    cboDeliveryMethod.Location = txtDeliveryMethod.Location
    dtpDeliveryDate.Size = txtDeliveryDate.Size
    dtpDeliveryDate.Location = txtDeliveryDate.Location
    cboDeliveryDay.Size = txtDeliveryDay.Size
    cboDeliveryDay.Location = txtDeliveryDay.Location
    cboOrderStatus.Size = txtOrderStatus.Size
    cboOrderStatus.Location = txtOrderStatus.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vOrderHeaderID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pOrderHeader As New clsOrderHeader(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vOrderHeaderID <> 0 Then 
      pFault = pOrderHeader.GetByID(vOrderHeaderID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pOrderHeader) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rOrderHeader As clsOrderHeader, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rOrderHeader)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rOrderHeader As clsOrderHeader) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _OrderHeader = rOrderHeader 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboOrderStatus.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.ccCustomerDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboPaymentMethod() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboPaymentStatus() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboDeliveryMethod() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboDeliveryDay() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboOrderStatus() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboCustomer() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rOrderHeader"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rOrderHeader As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rOrderHeader.GetType.Name = "clsOrderHeader" Then 
      ctlOrderHeader_Load(Nothing, Nothing) 
      Dim pOrderHeader As clsOrderHeader = CType(rOrderHeader, clsOrderHeader) 
      Return LoadControl(pOrderHeader) 
    Else 
      Dim pOrderHeaderID As Long = CType(rOrderHeader, Long) 
      Return LoadControl(pOrderHeaderID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "OrderNumber", _Requester) 
    If pStrg <> "" Then lblOrderNumber.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "Customer", _Requester) 
    If pStrg <> "" Then lblCustomer.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "OrderDate", _Requester) 
    If pStrg <> "" Then lblOrderDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "TotalAmount", _Requester) 
    If pStrg <> "" Then lblTotalAmount.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "VATAmount", _Requester) 
    If pStrg <> "" Then lblVATAmount.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "TotalWithVAT", _Requester) 
    If pStrg <> "" Then lblTotalWithVAT.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "PaymentMethod", _Requester) 
    If pStrg <> "" Then lblPaymentMethod.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "PaymentStatus", _Requester) 
    If pStrg <> "" Then lblPaymentStatus.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "PaymentDate", _Requester) 
    If pStrg <> "" Then lblPaymentDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "InvoiceNumber", _Requester) 
    If pStrg <> "" Then lblInvoiceNumber.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "DeliveryMethod", _Requester) 
    If pStrg <> "" Then lblDeliveryMethod.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "DeliveryDate", _Requester) 
    If pStrg <> "" Then lblDeliveryDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "DeliveryDay", _Requester) 
    If pStrg <> "" Then lblDeliveryDay.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "OrderStatus", _Requester) 
    If pStrg <> "" Then lblOrderStatus.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "Notes2", _Requester) 
    If pStrg <> "" Then lblNotes2.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "OrderMonth", _Requester) 
    If pStrg <> "" Then lblOrderMonth.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("OrderHeader", "Quarter", _Requester) 
    If pStrg <> "" Then lblQuarter.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [OrderHeader]() As clsOrderHeader
    Get 
      Return _OrderHeader 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboCustomer() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccCustomerDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(clsOrderHeader.enmParentProperty.Customer, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboCustomer.MakeSmart() Else cboCustomer.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboCustomer.LoadControl(pComboList, pPrompt) 
    Else 
      cboCustomer.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _OrderHeader.CustomerID > 0 Then cboCustomer.ValueSelect(_OrderHeader.CustomerID) Else cboCustomer.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboPaymentMethod() As clsFault
    Dim pFault As New clsFault
 
    'If cboPaymentMethod.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pPaymentMethodes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsOrderHeader.enmParentProperty.PaymentMethod, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pPaymentMethodes.FillEnums(clsEnums.enmEnum.PaymentMethod, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pPaymentMethodes = pTestCol
    End If
    
    pPaymentMethodes.Remove(pPaymentMethodes.FindByKey(clsEnums.enmPaymentMethod.UD))
    pPaymentMethodes.SortByText()
    pPaymentMethodes.AddToTop(clsEnums.enmPaymentMethod.UD, GetChoose(_Requester))

    With cboPaymentMethod
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pPaymentMethodes
    End With

    cboPaymentMethod.SelectedValue = _OrderHeader.PaymentMethod 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboPaymentStatus() As clsFault
    Dim pFault As New clsFault
 
    'If cboPaymentStatus.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pPaymentStatuses As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsOrderHeader.enmParentProperty.PaymentStatus, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pPaymentStatuses.FillEnums(clsEnums.enmEnum.PaymentStatus, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pPaymentStatuses = pTestCol
    End If
    
    pPaymentStatuses.Remove(pPaymentStatuses.FindByKey(clsEnums.enmPaymentStatus.UD))
    pPaymentStatuses.SortByText()
    pPaymentStatuses.AddToTop(clsEnums.enmPaymentStatus.UD, GetChoose(_Requester))

    With cboPaymentStatus
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pPaymentStatuses
    End With

    cboPaymentStatus.SelectedValue = _OrderHeader.PaymentStatus 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboDeliveryMethod() As clsFault
    Dim pFault As New clsFault
 
    'If cboDeliveryMethod.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pDeliveryMethodes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsOrderHeader.enmParentProperty.DeliveryMethod, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pDeliveryMethodes.FillEnums(clsEnums.enmEnum.DeliveryMethod, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pDeliveryMethodes = pTestCol
    End If
    
    pDeliveryMethodes.Remove(pDeliveryMethodes.FindByKey(clsEnums.enmDeliveryMethod.UD))
    pDeliveryMethodes.SortByText()
    pDeliveryMethodes.AddToTop(clsEnums.enmDeliveryMethod.UD, GetChoose(_Requester))

    With cboDeliveryMethod
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pDeliveryMethodes
    End With

    cboDeliveryMethod.SelectedValue = _OrderHeader.DeliveryMethod 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboDeliveryDay() As clsFault
    Dim pFault As New clsFault
 
    'If cboDeliveryDay.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pDeliveryDayes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsOrderHeader.enmParentProperty.DeliveryDay, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pDeliveryDayes.FillEnums(clsEnums.enmEnum.DeliveryDay, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pDeliveryDayes = pTestCol
    End If
    
    pDeliveryDayes.Remove(pDeliveryDayes.FindByKey(clsEnums.enmDeliveryDay.UD))
    pDeliveryDayes.SortByText()
    pDeliveryDayes.AddToTop(clsEnums.enmDeliveryDay.UD, GetChoose(_Requester))

    With cboDeliveryDay
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pDeliveryDayes
    End With

    cboDeliveryDay.SelectedValue = _OrderHeader.DeliveryDay 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboOrderStatus() As clsFault
    Dim pFault As New clsFault
 
    'If cboOrderStatus.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pOrderStatuses As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsOrderHeader.enmParentProperty.OrderStatus, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pOrderStatuses.FillEnums(clsEnums.enmEnum.OrderStatus, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pOrderStatuses = pTestCol
    End If
    
    pOrderStatuses.Remove(pOrderStatuses.FindByKey(clsEnums.enmOrderStatus.UD))
    pOrderStatuses.SortByText()
    pOrderStatuses.AddToTop(clsEnums.enmOrderStatus.UD, GetChoose(_Requester))

    With cboOrderStatus
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pOrderStatuses
    End With

    cboOrderStatus.SelectedValue = _OrderHeader.OrderStatus 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboCustomer_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboCustomer.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsOrderHeader.enmParentProperty.Customer, pUniqueCode) 
  End Sub 
  Private Sub cboPaymentMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPaymentMethod.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmPaymentMethod = CType(cboPaymentMethod.SelectedValue, clsEnums.enmPaymentMethod) 
    RaiseEvent evtCboSelectedIndexChanged(clsOrderHeader.enmParentProperty.PaymentMethod, pEnum.ToString) 
  End Sub 
  Private Sub cboPaymentStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPaymentStatus.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmPaymentStatus = CType(cboPaymentStatus.SelectedValue, clsEnums.enmPaymentStatus) 
    RaiseEvent evtCboSelectedIndexChanged(clsOrderHeader.enmParentProperty.PaymentStatus, pEnum.ToString) 
  End Sub 
  Private Sub cboDeliveryMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDeliveryMethod.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmDeliveryMethod = CType(cboDeliveryMethod.SelectedValue, clsEnums.enmDeliveryMethod) 
    RaiseEvent evtCboSelectedIndexChanged(clsOrderHeader.enmParentProperty.DeliveryMethod, pEnum.ToString) 
  End Sub 
  Private Sub cboDeliveryDay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDeliveryDay.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmDeliveryDay = CType(cboDeliveryDay.SelectedValue, clsEnums.enmDeliveryDay) 
    RaiseEvent evtCboSelectedIndexChanged(clsOrderHeader.enmParentProperty.DeliveryDay, pEnum.ToString) 
  End Sub 
  Private Sub cboOrderStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOrderStatus.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmOrderStatus = CType(cboOrderStatus.SelectedValue, clsEnums.enmOrderStatus) 
    RaiseEvent evtCboSelectedIndexChanged(clsOrderHeader.enmParentProperty.OrderStatus, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsOrderHeader.enmParentProperty = clsOrderHeader.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderHeader.enmParentProperty.Customer) = clsOrderHeader.enmParentProperty.Customer Then 
      lblCustomer.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    txtOrderNumber.ReadOnly = Not (vInEdit)
    txtOrderNumber.BackColor = pDefaultColour 
    If vInEdit = False Then 
      txtCustomer.ReadOnly = True
      txtCustomer.Visible = True
      txtCustomer.BackColor = pReadonlyColour
      txtCustomer.ForeColor = SetForeColor(vInEdit) 
      cboCustomer.Visible = False 
    Else 
      txtCustomer.ReadOnly = True
      txtCustomer.Visible = Not (vInEdit)
      txtCustomer.BackColor = pReadonlyColour 
      txtCustomer.ForeColor = SetForeColor(vInEdit) 
      cboCustomer.Visible = vInEdit
    End If  
    dtpOrderDate.Visible = vInEdit
    txtOrderDate.Visible = Not (vInEdit)
    txtOrderDate.BackColor = pReadonlyColour 
    txtOrderDate.ForeColor = SetForeColor(vInEdit) 
    txtOrderDate.ReadOnly = True
    txtTotalAmount.ReadOnly = True 
    txtTotalAmount.BackColor = pReadonlyColour 
    txtTotalAmount.ForeColor = SetForeColor(vInEdit) 
    txtVATAmount.ReadOnly = True 
    txtVATAmount.BackColor = pReadonlyColour 
    txtVATAmount.ForeColor = SetForeColor(vInEdit) 
    txtTotalWithVAT.ReadOnly = True 
    txtTotalWithVAT.BackColor = pReadonlyColour 
    txtTotalWithVAT.ForeColor = SetForeColor(vInEdit) 
    txtPaymentMethod.ReadOnly = True
    txtPaymentMethod.Visible = Not (vInEdit)
    txtPaymentMethod.BackColor = pReadonlyColour 
    txtPaymentMethod.ForeColor = SetForeColor(vInEdit) 
    cboPaymentMethod.Visible = vInEdit
    txtPaymentStatus.ReadOnly = True
    txtPaymentStatus.Visible = Not (vInEdit)
    txtPaymentStatus.BackColor = pReadonlyColour 
    txtPaymentStatus.ForeColor = SetForeColor(vInEdit) 
    cboPaymentStatus.Visible = vInEdit
    dtpPaymentDate.Visible = vInEdit
    txtPaymentDate.Visible = Not (vInEdit)
    txtPaymentDate.BackColor = pReadonlyColour 
    txtPaymentDate.ForeColor = SetForeColor(vInEdit) 
    txtPaymentDate.ReadOnly = True
    txtInvoiceNumber.ReadOnly = Not (vInEdit)
    txtInvoiceNumber.BackColor = pDefaultColour 
    txtDeliveryMethod.ReadOnly = True
    txtDeliveryMethod.Visible = Not (vInEdit)
    txtDeliveryMethod.BackColor = pReadonlyColour 
    txtDeliveryMethod.ForeColor = SetForeColor(vInEdit) 
    cboDeliveryMethod.Visible = vInEdit
    dtpDeliveryDate.Visible = vInEdit
    txtDeliveryDate.Visible = Not (vInEdit)
    txtDeliveryDate.BackColor = pReadonlyColour 
    txtDeliveryDate.ForeColor = SetForeColor(vInEdit) 
    txtDeliveryDate.ReadOnly = True
    txtDeliveryDay.ReadOnly = True
    txtDeliveryDay.Visible = Not (vInEdit)
    txtDeliveryDay.BackColor = pReadonlyColour 
    txtDeliveryDay.ForeColor = SetForeColor(vInEdit) 
    cboDeliveryDay.Visible = vInEdit
    txtOrderStatus.ReadOnly = True
    txtOrderStatus.Visible = Not (vInEdit)
    txtOrderStatus.BackColor = pReadonlyColour 
    txtOrderStatus.ForeColor = SetForeColor(vInEdit) 
    cboOrderStatus.Visible = vInEdit
    txtNotes.ReadOnly = Not (vInEdit)
    txtNotes.BackColor = pDefaultColour 
    txtNotes2.ReadOnly = Not (vInEdit)
    txtNotes2.BackColor = pDefaultColour 
    txtOrderMonth.ReadOnly = True 
    txtOrderMonth.BackColor = pReadonlyColour 
    txtOrderMonth.ForeColor = SetForeColor(vInEdit) 
    txtQuarter.ReadOnly = True 
    txtQuarter.BackColor = pReadonlyColour 
    txtQuarter.ForeColor = SetForeColor(vInEdit) 

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
      If _OrderHeader.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_OrderHeaderUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_OrderHeaderDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_OrderHeaderUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _OrderHeader) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _OrderHeader
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtOrderNumber.Text = .OrderNumber.ToString(FormatFromTag(txtOrderNumber, "#,##0"))
      txtCustomer.Text = .CustomerText 
      If .OrderDate < dtpOrderDate.MinDate Then dtpOrderDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpOrderDate.Value = .OrderDate
      dtpOrderDate.CustomFormat = FormatFromTag(txtOrderDate, "dd-MM-yyyy HH:mm:ss") 
      dtpOrderDate.Value = DateTime.ParseExact(dtpOrderDate.Value.ToString(dtpOrderDate.CustomFormat), dtpOrderDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .OrderDate < dtpOrderDate.MinDate Then dtpOrderDate.Checked = False Else dtpOrderDate.Checked = True 
      If Math.Abs(.OrderDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.OrderDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtOrderDate.Text = "" Else txtOrderDate.Text = .OrderDate.ToString(FormatFromTag(txtOrderDate, "dd-MM-yyyy HH:mm:ss"))
      txtTotalAmount.Text = .TotalAmount.ToString(FormatFromTag(txtTotalAmount, "#,##0.00"))
      txtTotalAmount.Text = If(.TotalAmount = 0D, "", .TotalAmount.ToString(FormatFromTag(txtTotalAmount, "#,##0.00"))) 
      txtVATAmount.Text = .VATAmount.ToString(FormatFromTag(txtVATAmount, "#,##0.00"))
      txtVATAmount.Text = If(.VATAmount = 0D, "", .VATAmount.ToString(FormatFromTag(txtVATAmount, "#,##0.00"))) 
      txtTotalWithVAT.Text = .TotalWithVAT.ToString(FormatFromTag(txtTotalWithVAT, "#,##0.00"))
      txtTotalWithVAT.Text = If(.TotalWithVAT = 0D, "", .TotalWithVAT.ToString(FormatFromTag(txtTotalWithVAT, "#,##0.00"))) 
      cboPaymentMethod.SelectedValue = .PaymentMethod
      txtPaymentMethod.Text = cboPaymentMethod.Text : If cboPaymentMethod.SelectedValue Is Nothing OrElse cboPaymentMethod.SelectedValue.ToString() = "UD" Then txtPaymentMethod.Text = ""    
      cboPaymentStatus.SelectedValue = .PaymentStatus
      txtPaymentStatus.Text = cboPaymentStatus.Text : If cboPaymentStatus.SelectedValue Is Nothing OrElse cboPaymentStatus.SelectedValue.ToString() = "UD" Then txtPaymentStatus.Text = ""    
      If .PaymentDate < dtpPaymentDate.MinDate Then dtpPaymentDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpPaymentDate.Value = .PaymentDate.Date
      dtpPaymentDate.CustomFormat = FormatFromTag(txtPaymentDate, "dd-MM-yyyy") 
      dtpPaymentDate.Value = DateTime.ParseExact(dtpPaymentDate.Value.ToString(dtpPaymentDate.CustomFormat), dtpPaymentDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .PaymentDate < dtpPaymentDate.MinDate Then dtpPaymentDate.Checked = False Else dtpPaymentDate.Checked = True 
      If Math.Abs(.PaymentDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.PaymentDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtPaymentDate.Text = "" Else txtPaymentDate.Text = .PaymentDate.ToString(FormatFromTag(txtPaymentDate, "dd-MM-yyyy"))
      txtInvoiceNumber.Text = .InvoiceNumber.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtInvoiceNumber.MaxLength = 50 
      cboDeliveryMethod.SelectedValue = .DeliveryMethod
      txtDeliveryMethod.Text = cboDeliveryMethod.Text : If cboDeliveryMethod.SelectedValue Is Nothing OrElse cboDeliveryMethod.SelectedValue.ToString() = "UD" Then txtDeliveryMethod.Text = ""    
      If .DeliveryDate < dtpDeliveryDate.MinDate Then dtpDeliveryDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpDeliveryDate.Value = .DeliveryDate.Date
      dtpDeliveryDate.CustomFormat = FormatFromTag(txtDeliveryDate, "dd-MM-yyyy") 
      dtpDeliveryDate.Value = DateTime.ParseExact(dtpDeliveryDate.Value.ToString(dtpDeliveryDate.CustomFormat), dtpDeliveryDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .DeliveryDate < dtpDeliveryDate.MinDate Then dtpDeliveryDate.Checked = False Else dtpDeliveryDate.Checked = True 
      If Math.Abs(.DeliveryDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.DeliveryDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtDeliveryDate.Text = "" Else txtDeliveryDate.Text = .DeliveryDate.ToString(FormatFromTag(txtDeliveryDate, "dd-MM-yyyy"))
      cboDeliveryDay.SelectedValue = .DeliveryDay
      txtDeliveryDay.Text = cboDeliveryDay.Text : If cboDeliveryDay.SelectedValue Is Nothing OrElse cboDeliveryDay.SelectedValue.ToString() = "UD" Then txtDeliveryDay.Text = ""    
      cboOrderStatus.SelectedValue = .OrderStatus
      txtOrderStatus.Text = cboOrderStatus.Text : If cboOrderStatus.SelectedValue Is Nothing OrElse cboOrderStatus.SelectedValue.ToString() = "UD" Then txtOrderStatus.Text = ""    
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtNotes2.Text = .Notes2.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtOrderMonth.Text = .OrderMonth.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtOrderMonth.MaxLength = 10 
      txtQuarter.Text = .Quarter.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtQuarter.MaxLength = 2 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _OrderHeader
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-OrderHeader-ID-090417-0012", _Requester) : Return pFault 
      If Integer.TryParse(txtOrderNumber.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .OrderNumber) = False Then pFault.LogFreeTextFault(208, ".OrderNumber", txtOrderNumber.Text, "TRGT-OrderHeader-OrderNumber-090417-0013", _Requester) : Return pFault 
      If cboCustomer.SelectedItem Is Nothing OrElse cboCustomer.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .CustomerID = 0 
      Else 
        Dim pCustomerID As Long = CType(cboCustomer.SelectedItem, clsComboListMember).KeyLong 
        If pCustomerID = -1 Then .CustomerID = 0 Else .CustomerID = pCustomerID 
      End If 
      If (dtpOrderDate.ShowCheckBox AndAlso dtpOrderDate.Checked = False) OrElse dtpOrderDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .OrderDate = Nothing Else .OrderDate = dtpOrderDate.Value
      .PaymentMethod = CType(cboPaymentMethod.SelectedValue, clsEnums.enmPaymentMethod)
      .PaymentStatus = CType(cboPaymentStatus.SelectedValue, clsEnums.enmPaymentStatus)
      If (dtpPaymentDate.ShowCheckBox AndAlso dtpPaymentDate.Checked = False) OrElse dtpPaymentDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .PaymentDate = Nothing Else .PaymentDate = dtpPaymentDate.Value.Date
      .InvoiceNumber = txtInvoiceNumber.Text 
      .DeliveryMethod = CType(cboDeliveryMethod.SelectedValue, clsEnums.enmDeliveryMethod)
      If (dtpDeliveryDate.ShowCheckBox AndAlso dtpDeliveryDate.Checked = False) OrElse dtpDeliveryDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .DeliveryDate = Nothing Else .DeliveryDate = dtpDeliveryDate.Value.Date
      .DeliveryDay = CType(cboDeliveryDay.SelectedValue, clsEnums.enmDeliveryDay)
      .OrderStatus = CType(cboOrderStatus.SelectedValue, clsEnums.enmOrderStatus)
      .Notes = txtNotes.Text 
      .Notes2 = txtNotes2.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-OrderHeader-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtOrderNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrderNumber.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtOrderNumber.Text 
    Dim pTest As Integer 
 
    If txtOrderNumber.Text = "" Then Exit Sub 
    If txtOrderNumber.Text = txtOrderNumber.Name Then Exit Sub 
 
    If Integer.TryParse(txtOrderNumber.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-OrderHeader-OrderNumber-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsOrderHeader.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-OrderHeader-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_OrderHeader, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _OrderHeader.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the OrderHeader collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccOrderHeaderDefaultByID) 
      RaiseEvent evtUpdated(clsOrderHeader.enmUpdateType.Standard, _OrderHeader) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_OrderHeader_evtAfterUpdate 
  Private Sub _OrderHeader_evtAfterUpdate() Handles _OrderHeader.evtAfterUpdate, _OrderHeader.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_OrderHeader) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _OrderHeader = New clsOrderHeader(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_OrderHeader) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_OrderHeader, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _OrderHeader.OrderNumber.ToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _OrderHeader.ID 
    pFault = _OrderHeader.Delete(_Requester) 
    If pFault.isOK = True Then 
      _OrderHeader = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblCustomer_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.DoubleClick 
    If _OrderHeader.CustomerID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderHeader.enmParentProperty.Customer) = clsOrderHeader.enmParentProperty.Customer Then 
      If _OrderHeader.CustomerID <> 0 Then RaiseEvent evtParentChosen(clsOrderHeader.enmParentProperty.Customer, _OrderHeader.CustomerID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "Customer Detail" 
      fPopup.LoadControl("ctlccCustomer", _OrderHeader.CustomerID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblCustomer_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderHeader.enmParentProperty.Customer) <> clsOrderHeader.enmParentProperty.Customer Then Exit Sub 
    lblCustomer.ForeColor = Color.Brown 
    'lblCustomer.Font = New Font(lblCustomer.Font.Name, lblCustomer.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblCustomer.BackColor = ccHelper.InvertColour(lblCustomer.ForeColor) 'did this instead 
    lblCustomer.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblCustomer_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsOrderHeader.enmParentProperty.Customer) <> clsOrderHeader.enmParentProperty.Customer Then Exit Sub 
    lblCustomer.ForeColor = Color.Brown 
    'lblCustomer.Font = New Font(lblCustomer.Font.Name, lblCustomer.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblCustomer.BackColor = Me.BackColor 'did this instead 
    lblCustomer.Cursor = Cursors.Default 
  End Sub 
 
  'SeparateUpdates 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  Private Sub ctlccOrderHeader_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the OrderHeader to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pOrderHeader As clsOrderHeader = _OrderHeader 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pOrderHeader.ToCSV) 
        Else 
          Clipboard.SetText(pOrderHeader.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The OrderHeader is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccOrderHeader_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlOrderHeader_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
