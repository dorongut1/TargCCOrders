Public Class ctlccDelivery
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsDelivery.enmUpdateType) 
  Public Event evtAdd(ByVal vDelivery As clsDelivery) 
  Public Event evtBeforeUpdate(ByVal vDelivery As clsDelivery, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsDelivery.enmUpdateType, ByVal vDelivery As clsDelivery) 
  Public Event evtBeforeDelete(ByVal vDelivery As clsDelivery, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vDeliveryID As Long) 
  Public Event evtCancelledEdit(ByVal vDelivery As clsDelivery) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vDelivery As clsDelivery) 
  
  Public Event evtParentChosen(ByVal vParentName As clsDelivery.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsDelivery.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsDelivery.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsDelivery.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsDelivery.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsDelivery.enmParentProperty) 
      _EnableParentLinks.Add(clsDelivery.enmParentProperty.OrderHeader) 
 
    End Sub 
  End Class 
 
  Private WithEvents _Delivery As clsDelivery

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlDelivery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboOrderHeader.Size = txtOrderHeader.Size
    cboOrderHeader.Location = txtOrderHeader.Location
    cboDeliveryMethod.Size = txtDeliveryMethod.Size
    cboDeliveryMethod.Location = txtDeliveryMethod.Location
    dtpOrderedDate.Size = txtOrderedDate.Size
    dtpOrderedDate.Location = txtOrderedDate.Location
    dtpReceivedDate.Size = txtReceivedDate.Size
    dtpReceivedDate.Location = txtReceivedDate.Location
    dtpArrivalToHubDate.Size = txtArrivalToHubDate.Size
    dtpArrivalToHubDate.Location = txtArrivalToHubDate.Location
    dtpArrivalToCustomerDate.Size = txtArrivalToCustomerDate.Size
    dtpArrivalToCustomerDate.Location = txtArrivalToCustomerDate.Location
    cboDeliveryStatus.Size = txtDeliveryStatus.Size
    cboDeliveryStatus.Location = txtDeliveryStatus.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vDeliveryID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pDelivery As New clsDelivery(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vDeliveryID <> 0 Then 
      pFault = pDelivery.GetByID(vDeliveryID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pDelivery) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rDelivery As clsDelivery, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rDelivery)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rDelivery As clsDelivery) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _Delivery = rDelivery 

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
 
    If cboDeliveryStatus.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.ccOrderHeaderDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboDeliveryMethod() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboDeliveryStatus() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboOrderHeader() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rDelivery"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rDelivery As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rDelivery.GetType.Name = "clsDelivery" Then 
      ctlDelivery_Load(Nothing, Nothing) 
      Dim pDelivery As clsDelivery = CType(rDelivery, clsDelivery) 
      Return LoadControl(pDelivery) 
    Else 
      Dim pDeliveryID As Long = CType(rDelivery, Long) 
      Return LoadControl(pDeliveryID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "OrderHeader", _Requester) 
    If pStrg <> "" Then lblOrderHeader.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "DeliveryAddress", _Requester) 
    If pStrg <> "" Then lblDeliveryAddress.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "ContactPhone", _Requester) 
    If pStrg <> "" Then lblContactPhone.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "ContactName", _Requester) 
    If pStrg <> "" Then lblContactName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "DeliveryMethod", _Requester) 
    If pStrg <> "" Then lblDeliveryMethod.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "OrderedDate", _Requester) 
    If pStrg <> "" Then lblOrderedDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "ReceivedDate", _Requester) 
    If pStrg <> "" Then lblReceivedDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "ArrivalToHubDate", _Requester) 
    If pStrg <> "" Then lblArrivalToHubDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "ArrivalToCustomerDate", _Requester) 
    If pStrg <> "" Then lblArrivalToCustomerDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "DeliveryStatus", _Requester) 
    If pStrg <> "" Then lblDeliveryStatus.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "Location", _Requester) 
    If pStrg <> "" Then lblLocation.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "ProductsSummary", _Requester) 
    If pStrg <> "" Then lblProductsSummary.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Delivery", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [Delivery]() As clsDelivery
    Get 
      Return _Delivery 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboOrderHeader() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccOrderHeaderDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(clsDelivery.enmParentProperty.OrderHeader, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboOrderHeader.MakeSmart() Else cboOrderHeader.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboOrderHeader.LoadControl(pComboList, pPrompt) 
    Else 
      cboOrderHeader.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _Delivery.OrderHeaderID > 0 Then cboOrderHeader.ValueSelect(_Delivery.OrderHeaderID) Else cboOrderHeader.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboDeliveryMethod() As clsFault
    Dim pFault As New clsFault
 
    'If cboDeliveryMethod.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pDeliveryMethodes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsDelivery.enmParentProperty.DeliveryMethod, pTestCol, pPrompt) 
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

    cboDeliveryMethod.SelectedValue = _Delivery.DeliveryMethod 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboDeliveryStatus() As clsFault
    Dim pFault As New clsFault
 
    'If cboDeliveryStatus.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pDeliveryStatuses As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsDelivery.enmParentProperty.DeliveryStatus, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pDeliveryStatuses.FillEnums(clsEnums.enmEnum.DeliveryStatus, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pDeliveryStatuses = pTestCol
    End If
    
    pDeliveryStatuses.Remove(pDeliveryStatuses.FindByKey(clsEnums.enmDeliveryStatus.UD))
    pDeliveryStatuses.SortByText()
    pDeliveryStatuses.AddToTop(clsEnums.enmDeliveryStatus.UD, GetChoose(_Requester))

    With cboDeliveryStatus
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pDeliveryStatuses
    End With

    cboDeliveryStatus.SelectedValue = _Delivery.DeliveryStatus 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboOrderHeader_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboOrderHeader.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsDelivery.enmParentProperty.OrderHeader, pUniqueCode) 
  End Sub 
  Private Sub cboDeliveryMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDeliveryMethod.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmDeliveryMethod = CType(cboDeliveryMethod.SelectedValue, clsEnums.enmDeliveryMethod) 
    RaiseEvent evtCboSelectedIndexChanged(clsDelivery.enmParentProperty.DeliveryMethod, pEnum.ToString) 
  End Sub 
  Private Sub cboDeliveryStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDeliveryStatus.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmDeliveryStatus = CType(cboDeliveryStatus.SelectedValue, clsEnums.enmDeliveryStatus) 
    RaiseEvent evtCboSelectedIndexChanged(clsDelivery.enmParentProperty.DeliveryStatus, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsDelivery.enmParentProperty = clsDelivery.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsDelivery.enmParentProperty.OrderHeader) = clsDelivery.enmParentProperty.OrderHeader Then 
      lblOrderHeader.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtOrderHeader.ReadOnly = True
      txtOrderHeader.Visible = True
      txtOrderHeader.BackColor = pReadonlyColour
      txtOrderHeader.ForeColor = SetForeColor(vInEdit) 
      cboOrderHeader.Visible = False 
    Else 
      txtOrderHeader.ReadOnly = True
      txtOrderHeader.Visible = Not (vInEdit)
      txtOrderHeader.BackColor = pReadonlyColour 
      txtOrderHeader.ForeColor = SetForeColor(vInEdit) 
      cboOrderHeader.Visible = vInEdit
    End If  
    txtDeliveryAddress.ReadOnly = Not (vInEdit)
    txtDeliveryAddress.BackColor = pDefaultColour 
    txtContactPhone.ReadOnly = Not (vInEdit)
    txtContactPhone.BackColor = pDefaultColour 
    txtContactName.ReadOnly = Not (vInEdit)
    txtContactName.BackColor = pDefaultColour 
    txtDeliveryMethod.ReadOnly = True
    txtDeliveryMethod.Visible = Not (vInEdit)
    txtDeliveryMethod.BackColor = pReadonlyColour 
    txtDeliveryMethod.ForeColor = SetForeColor(vInEdit) 
    cboDeliveryMethod.Visible = vInEdit
    dtpOrderedDate.Visible = vInEdit
    txtOrderedDate.Visible = Not (vInEdit)
    txtOrderedDate.BackColor = pReadonlyColour 
    txtOrderedDate.ForeColor = SetForeColor(vInEdit) 
    txtOrderedDate.ReadOnly = True
    dtpReceivedDate.Visible = vInEdit
    txtReceivedDate.Visible = Not (vInEdit)
    txtReceivedDate.BackColor = pReadonlyColour 
    txtReceivedDate.ForeColor = SetForeColor(vInEdit) 
    txtReceivedDate.ReadOnly = True
    dtpArrivalToHubDate.Visible = vInEdit
    txtArrivalToHubDate.Visible = Not (vInEdit)
    txtArrivalToHubDate.BackColor = pReadonlyColour 
    txtArrivalToHubDate.ForeColor = SetForeColor(vInEdit) 
    txtArrivalToHubDate.ReadOnly = True
    dtpArrivalToCustomerDate.Visible = vInEdit
    txtArrivalToCustomerDate.Visible = Not (vInEdit)
    txtArrivalToCustomerDate.BackColor = pReadonlyColour 
    txtArrivalToCustomerDate.ForeColor = SetForeColor(vInEdit) 
    txtArrivalToCustomerDate.ReadOnly = True
    txtDeliveryStatus.ReadOnly = True
    txtDeliveryStatus.Visible = Not (vInEdit)
    txtDeliveryStatus.BackColor = pReadonlyColour 
    txtDeliveryStatus.ForeColor = SetForeColor(vInEdit) 
    cboDeliveryStatus.Visible = vInEdit
    txtLocation.ReadOnly = Not (vInEdit)
    txtLocation.BackColor = pDefaultColour 
    txtProductsSummary.ReadOnly = True 
    txtProductsSummary.BackColor = pReadonlyColour 
    txtProductsSummary.ForeColor = SetForeColor(vInEdit) 
    txtNotes.ReadOnly = Not (vInEdit)
    txtNotes.BackColor = pDefaultColour 

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
      If _Delivery.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_DeliveryUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_DeliveryDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_DeliveryUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _Delivery) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _Delivery
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtOrderHeader.Text = .OrderHeaderText 
      txtDeliveryAddress.Text = .DeliveryAddress.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDeliveryAddress.MaxLength = 500 
      txtContactPhone.Text = .ContactPhone.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtContactPhone.MaxLength = 50 
      txtContactName.Text = .ContactName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtContactName.MaxLength = 255 
      cboDeliveryMethod.SelectedValue = .DeliveryMethod
      txtDeliveryMethod.Text = cboDeliveryMethod.Text : If cboDeliveryMethod.SelectedValue Is Nothing OrElse cboDeliveryMethod.SelectedValue.ToString() = "UD" Then txtDeliveryMethod.Text = ""    
      If .OrderedDate < dtpOrderedDate.MinDate Then dtpOrderedDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpOrderedDate.Value = .OrderedDate.Date
      dtpOrderedDate.CustomFormat = FormatFromTag(txtOrderedDate, "dd-MM-yyyy") 
      dtpOrderedDate.Value = DateTime.ParseExact(dtpOrderedDate.Value.ToString(dtpOrderedDate.CustomFormat), dtpOrderedDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .OrderedDate < dtpOrderedDate.MinDate Then dtpOrderedDate.Checked = False Else dtpOrderedDate.Checked = True 
      If Math.Abs(.OrderedDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.OrderedDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtOrderedDate.Text = "" Else txtOrderedDate.Text = .OrderedDate.ToString(FormatFromTag(txtOrderedDate, "dd-MM-yyyy"))
      If .ReceivedDate < dtpReceivedDate.MinDate Then dtpReceivedDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpReceivedDate.Value = .ReceivedDate.Date
      dtpReceivedDate.CustomFormat = FormatFromTag(txtReceivedDate, "dd-MM-yyyy") 
      dtpReceivedDate.Value = DateTime.ParseExact(dtpReceivedDate.Value.ToString(dtpReceivedDate.CustomFormat), dtpReceivedDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .ReceivedDate < dtpReceivedDate.MinDate Then dtpReceivedDate.Checked = False Else dtpReceivedDate.Checked = True 
      If Math.Abs(.ReceivedDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.ReceivedDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtReceivedDate.Text = "" Else txtReceivedDate.Text = .ReceivedDate.ToString(FormatFromTag(txtReceivedDate, "dd-MM-yyyy"))
      If .ArrivalToHubDate < dtpArrivalToHubDate.MinDate Then dtpArrivalToHubDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpArrivalToHubDate.Value = .ArrivalToHubDate.Date
      dtpArrivalToHubDate.CustomFormat = FormatFromTag(txtArrivalToHubDate, "dd-MM-yyyy") 
      dtpArrivalToHubDate.Value = DateTime.ParseExact(dtpArrivalToHubDate.Value.ToString(dtpArrivalToHubDate.CustomFormat), dtpArrivalToHubDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .ArrivalToHubDate < dtpArrivalToHubDate.MinDate Then dtpArrivalToHubDate.Checked = False Else dtpArrivalToHubDate.Checked = True 
      If Math.Abs(.ArrivalToHubDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.ArrivalToHubDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtArrivalToHubDate.Text = "" Else txtArrivalToHubDate.Text = .ArrivalToHubDate.ToString(FormatFromTag(txtArrivalToHubDate, "dd-MM-yyyy"))
      If .ArrivalToCustomerDate < dtpArrivalToCustomerDate.MinDate Then dtpArrivalToCustomerDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpArrivalToCustomerDate.Value = .ArrivalToCustomerDate.Date
      dtpArrivalToCustomerDate.CustomFormat = FormatFromTag(txtArrivalToCustomerDate, "dd-MM-yyyy") 
      dtpArrivalToCustomerDate.Value = DateTime.ParseExact(dtpArrivalToCustomerDate.Value.ToString(dtpArrivalToCustomerDate.CustomFormat), dtpArrivalToCustomerDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .ArrivalToCustomerDate < dtpArrivalToCustomerDate.MinDate Then dtpArrivalToCustomerDate.Checked = False Else dtpArrivalToCustomerDate.Checked = True 
      If Math.Abs(.ArrivalToCustomerDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.ArrivalToCustomerDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtArrivalToCustomerDate.Text = "" Else txtArrivalToCustomerDate.Text = .ArrivalToCustomerDate.ToString(FormatFromTag(txtArrivalToCustomerDate, "dd-MM-yyyy"))
      cboDeliveryStatus.SelectedValue = .DeliveryStatus
      txtDeliveryStatus.Text = cboDeliveryStatus.Text : If cboDeliveryStatus.SelectedValue Is Nothing OrElse cboDeliveryStatus.SelectedValue.ToString() = "UD" Then txtDeliveryStatus.Text = ""    
      txtLocation.Text = .Location.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLocation.MaxLength = 500 
      txtProductsSummary.Text = .ProductsSummary.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _Delivery
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-Delivery-ID-090417-0012", _Requester) : Return pFault 
      If cboOrderHeader.SelectedItem Is Nothing OrElse cboOrderHeader.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .OrderHeaderID = 0 
      Else 
        Dim pOrderHeaderID As Long = CType(cboOrderHeader.SelectedItem, clsComboListMember).KeyLong 
        If pOrderHeaderID = -1 Then .OrderHeaderID = 0 Else .OrderHeaderID = pOrderHeaderID 
      End If 
      .DeliveryAddress = txtDeliveryAddress.Text 
      .ContactPhone = txtContactPhone.Text 
      .ContactName = txtContactName.Text 
      .DeliveryMethod = CType(cboDeliveryMethod.SelectedValue, clsEnums.enmDeliveryMethod)
      If (dtpOrderedDate.ShowCheckBox AndAlso dtpOrderedDate.Checked = False) OrElse dtpOrderedDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .OrderedDate = Nothing Else .OrderedDate = dtpOrderedDate.Value.Date
      If (dtpReceivedDate.ShowCheckBox AndAlso dtpReceivedDate.Checked = False) OrElse dtpReceivedDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .ReceivedDate = Nothing Else .ReceivedDate = dtpReceivedDate.Value.Date
      If (dtpArrivalToHubDate.ShowCheckBox AndAlso dtpArrivalToHubDate.Checked = False) OrElse dtpArrivalToHubDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .ArrivalToHubDate = Nothing Else .ArrivalToHubDate = dtpArrivalToHubDate.Value.Date
      If (dtpArrivalToCustomerDate.ShowCheckBox AndAlso dtpArrivalToCustomerDate.Checked = False) OrElse dtpArrivalToCustomerDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .ArrivalToCustomerDate = Nothing Else .ArrivalToCustomerDate = dtpArrivalToCustomerDate.Value.Date
      .DeliveryStatus = CType(cboDeliveryStatus.SelectedValue, clsEnums.enmDeliveryStatus)
      .Location = txtLocation.Text 
      .Notes = txtNotes.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-Delivery-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsDelivery.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-Delivery-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_Delivery, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _Delivery.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the Delivery collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccDeliveryDefaultByID) 
      RaiseEvent evtUpdated(clsDelivery.enmUpdateType.Standard, _Delivery) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_Delivery_evtAfterUpdate 
  Private Sub _Delivery_evtAfterUpdate() Handles _Delivery.evtAfterUpdate, _Delivery.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_Delivery) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _Delivery = New clsDelivery(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_Delivery) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_Delivery, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _Delivery.ID.ToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _Delivery.ID 
    pFault = _Delivery.Delete(_Requester) 
    If pFault.isOK = True Then 
      _Delivery = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblOrderHeader_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.DoubleClick 
    If _Delivery.OrderHeaderID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsDelivery.enmParentProperty.OrderHeader) = clsDelivery.enmParentProperty.OrderHeader Then 
      If _Delivery.OrderHeaderID <> 0 Then RaiseEvent evtParentChosen(clsDelivery.enmParentProperty.OrderHeader, _Delivery.OrderHeaderID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "OrderHeader Detail" 
      fPopup.LoadControl("ctlccOrderHeader", _Delivery.OrderHeaderID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblOrderHeader_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsDelivery.enmParentProperty.OrderHeader) <> clsDelivery.enmParentProperty.OrderHeader Then Exit Sub 
    lblOrderHeader.ForeColor = Color.Brown 
    'lblOrderHeader.Font = New Font(lblOrderHeader.Font.Name, lblOrderHeader.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblOrderHeader.BackColor = ccHelper.InvertColour(lblOrderHeader.ForeColor) 'did this instead 
    lblOrderHeader.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblOrderHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsDelivery.enmParentProperty.OrderHeader) <> clsDelivery.enmParentProperty.OrderHeader Then Exit Sub 
    lblOrderHeader.ForeColor = Color.Brown 
    'lblOrderHeader.Font = New Font(lblOrderHeader.Font.Name, lblOrderHeader.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblOrderHeader.BackColor = Me.BackColor 'did this instead 
    lblOrderHeader.Cursor = Cursors.Default 
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("Delivery", _Delivery.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _Delivery.DateAdded 
    pAuditIndexed.TableName = "Delivery" 
    pAuditIndexed.RowID = _Delivery.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Delivery'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlccDelivery_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the Delivery to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pDelivery As clsDelivery = _Delivery 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pDelivery.ToCSV) 
        Else 
          Clipboard.SetText(pDelivery.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The Delivery is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccDelivery_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlDelivery_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
