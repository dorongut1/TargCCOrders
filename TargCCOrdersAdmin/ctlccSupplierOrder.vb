Public Class ctlccSupplierOrder
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsSupplierOrder.enmUpdateType) 
  Public Event evtAdd(ByVal vSupplierOrder As clsSupplierOrder) 
  Public Event evtBeforeUpdate(ByVal vSupplierOrder As clsSupplierOrder, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsSupplierOrder.enmUpdateType, ByVal vSupplierOrder As clsSupplierOrder) 
  Public Event evtBeforeDelete(ByVal vSupplierOrder As clsSupplierOrder, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vSupplierOrderID As Long) 
  Public Event evtCancelledEdit(ByVal vSupplierOrder As clsSupplierOrder) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vSupplierOrder As clsSupplierOrder) 
  
  Public Event evtParentChosen(ByVal vParentName As clsSupplierOrder.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsSupplierOrder.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsSupplierOrder.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsSupplierOrder.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsSupplierOrder.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsSupplierOrder.enmParentProperty) 
      _EnableParentLinks.Add(clsSupplierOrder.enmParentProperty.OrderHeader) 
 
    End Sub 
  End Class 
 
  Private WithEvents _SupplierOrder As clsSupplierOrder

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlSupplierOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboEmailStatus.Size = txtEmailStatus.Size
    cboEmailStatus.Location = txtEmailStatus.Location
    dtpSentDate.Size = txtSentDate.Size
    dtpSentDate.Location = txtSentDate.Location
    cboDeliveryMethod.Size = txtDeliveryMethod.Size
    cboDeliveryMethod.Location = txtDeliveryMethod.Location
    dtpRequestedDeliveryDate.Size = txtRequestedDeliveryDate.Size
    dtpRequestedDeliveryDate.Location = txtRequestedDeliveryDate.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vSupplierOrderID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pSupplierOrder As New clsSupplierOrder(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vSupplierOrderID <> 0 Then 
      pFault = pSupplierOrder.GetByID(vSupplierOrderID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pSupplierOrder) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rSupplierOrder As clsSupplierOrder, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rSupplierOrder)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rSupplierOrder As clsSupplierOrder) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _SupplierOrder = rSupplierOrder 

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
 
    If cboDeliveryMethod.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.ccOrderHeaderDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboEmailStatus() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboDeliveryMethod() : If pFault.isOK = False Then Return pFault 
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
  ''' <param name="rSupplierOrder"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rSupplierOrder As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rSupplierOrder.GetType.Name = "clsSupplierOrder" Then 
      ctlSupplierOrder_Load(Nothing, Nothing) 
      Dim pSupplierOrder As clsSupplierOrder = CType(rSupplierOrder, clsSupplierOrder) 
      Return LoadControl(pSupplierOrder) 
    Else 
      Dim pSupplierOrderID As Long = CType(rSupplierOrder, Long) 
      Return LoadControl(pSupplierOrderID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "OrderHeader", _Requester) 
    If pStrg <> "" Then lblOrderHeader.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "SupplierEmail", _Requester) 
    If pStrg <> "" Then lblSupplierEmail.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "EmailSubject", _Requester) 
    If pStrg <> "" Then lblEmailSubject.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "EmailBody", _Requester) 
    If pStrg <> "" Then lblEmailBody.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "EmailStatus", _Requester) 
    If pStrg <> "" Then lblEmailStatus.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "SentDate", _Requester) 
    If pStrg <> "" Then lblSentDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "TotalCost", _Requester) 
    If pStrg <> "" Then lblTotalCost.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "DeliveryMethod", _Requester) 
    If pStrg <> "" Then lblDeliveryMethod.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "RequestedDeliveryDate", _Requester) 
    If pStrg <> "" Then lblRequestedDeliveryDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "RequestedDeliveryDay", _Requester) 
    If pStrg <> "" Then lblRequestedDeliveryDay.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("SupplierOrder", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [SupplierOrder]() As clsSupplierOrder
    Get 
      Return _SupplierOrder 
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
    RaiseEvent evtOverrideLoadIntelliCombo(clsSupplierOrder.enmParentProperty.OrderHeader, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
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
    
    If _SupplierOrder.OrderHeaderID > 0 Then cboOrderHeader.ValueSelect(_SupplierOrder.OrderHeaderID) Else cboOrderHeader.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboEmailStatus() As clsFault
    Dim pFault As New clsFault
 
    'If cboEmailStatus.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pEmailStatuses As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsSupplierOrder.enmParentProperty.EmailStatus, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pEmailStatuses.FillEnums(clsEnums.enmEnum.EmailStatus, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pEmailStatuses = pTestCol
    End If
    
    pEmailStatuses.Remove(pEmailStatuses.FindByKey(clsEnums.enmEmailStatus.UD))
    pEmailStatuses.SortByText()
    pEmailStatuses.AddToTop(clsEnums.enmEmailStatus.UD, GetChoose(_Requester))

    With cboEmailStatus
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pEmailStatuses
    End With

    cboEmailStatus.SelectedValue = _SupplierOrder.EmailStatus 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboDeliveryMethod() As clsFault
    Dim pFault As New clsFault
 
    'If cboDeliveryMethod.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pDeliveryMethodes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsSupplierOrder.enmParentProperty.DeliveryMethod, pTestCol, pPrompt) 
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

    cboDeliveryMethod.SelectedValue = _SupplierOrder.DeliveryMethod 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboOrderHeader_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboOrderHeader.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsSupplierOrder.enmParentProperty.OrderHeader, pUniqueCode) 
  End Sub 
  Private Sub cboEmailStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEmailStatus.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmEmailStatus = CType(cboEmailStatus.SelectedValue, clsEnums.enmEmailStatus) 
    RaiseEvent evtCboSelectedIndexChanged(clsSupplierOrder.enmParentProperty.EmailStatus, pEnum.ToString) 
  End Sub 
  Private Sub cboDeliveryMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDeliveryMethod.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmDeliveryMethod = CType(cboDeliveryMethod.SelectedValue, clsEnums.enmDeliveryMethod) 
    RaiseEvent evtCboSelectedIndexChanged(clsSupplierOrder.enmParentProperty.DeliveryMethod, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsSupplierOrder.enmParentProperty = clsSupplierOrder.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsSupplierOrder.enmParentProperty.OrderHeader) = clsSupplierOrder.enmParentProperty.OrderHeader Then 
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
    txtSupplierEmail.ReadOnly = Not (vInEdit)
    txtSupplierEmail.BackColor = pDefaultColour 
    txtEmailSubject.ReadOnly = Not (vInEdit)
    txtEmailSubject.BackColor = pDefaultColour 
    txtEmailBody.ReadOnly = Not (vInEdit)
    txtEmailBody.BackColor = pDefaultColour 
    txtEmailStatus.ReadOnly = True
    txtEmailStatus.Visible = Not (vInEdit)
    txtEmailStatus.BackColor = pReadonlyColour 
    txtEmailStatus.ForeColor = SetForeColor(vInEdit) 
    cboEmailStatus.Visible = vInEdit
    dtpSentDate.Visible = vInEdit
    txtSentDate.Visible = Not (vInEdit)
    txtSentDate.BackColor = pReadonlyColour 
    txtSentDate.ForeColor = SetForeColor(vInEdit) 
    txtSentDate.ReadOnly = True
    txtTotalCost.ReadOnly = True 
    txtTotalCost.BackColor = pReadonlyColour 
    txtTotalCost.ForeColor = SetForeColor(vInEdit) 
    txtDeliveryMethod.ReadOnly = True
    txtDeliveryMethod.Visible = Not (vInEdit)
    txtDeliveryMethod.BackColor = pReadonlyColour 
    txtDeliveryMethod.ForeColor = SetForeColor(vInEdit) 
    cboDeliveryMethod.Visible = vInEdit
    dtpRequestedDeliveryDate.Visible = vInEdit
    txtRequestedDeliveryDate.Visible = Not (vInEdit)
    txtRequestedDeliveryDate.BackColor = pReadonlyColour 
    txtRequestedDeliveryDate.ForeColor = SetForeColor(vInEdit) 
    txtRequestedDeliveryDate.ReadOnly = True
    txtRequestedDeliveryDay.ReadOnly = Not (vInEdit)
    txtRequestedDeliveryDay.BackColor = pDefaultColour 
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
      If _SupplierOrder.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_SupplierOrderUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_SupplierOrderDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_SupplierOrderUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _SupplierOrder) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _SupplierOrder
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtOrderHeader.Text = .OrderHeaderText 
      txtSupplierEmail.Text = .SupplierEmail.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSupplierEmail.MaxLength = 255 
      txtEmailSubject.Text = .EmailSubject.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtEmailSubject.MaxLength = 500 
      txtEmailBody.Text = .EmailBody.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      cboEmailStatus.SelectedValue = .EmailStatus
      txtEmailStatus.Text = cboEmailStatus.Text : If cboEmailStatus.SelectedValue Is Nothing OrElse cboEmailStatus.SelectedValue.ToString() = "UD" Then txtEmailStatus.Text = ""    
      If .SentDate < dtpSentDate.MinDate Then dtpSentDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpSentDate.Value = .SentDate
      dtpSentDate.CustomFormat = FormatFromTag(txtSentDate, "dd-MM-yyyy HH:mm:ss") 
      dtpSentDate.Value = DateTime.ParseExact(dtpSentDate.Value.ToString(dtpSentDate.CustomFormat), dtpSentDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .SentDate < dtpSentDate.MinDate Then dtpSentDate.Checked = False Else dtpSentDate.Checked = True 
      If Math.Abs(.SentDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.SentDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtSentDate.Text = "" Else txtSentDate.Text = .SentDate.ToString(FormatFromTag(txtSentDate, "dd-MM-yyyy HH:mm:ss"))
      txtTotalCost.Text = .TotalCost.ToString(FormatFromTag(txtTotalCost, "#,##0.00"))
      cboDeliveryMethod.SelectedValue = .DeliveryMethod
      txtDeliveryMethod.Text = cboDeliveryMethod.Text : If cboDeliveryMethod.SelectedValue Is Nothing OrElse cboDeliveryMethod.SelectedValue.ToString() = "UD" Then txtDeliveryMethod.Text = ""    
      If .RequestedDeliveryDate < dtpRequestedDeliveryDate.MinDate Then dtpRequestedDeliveryDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpRequestedDeliveryDate.Value = .RequestedDeliveryDate.Date
      dtpRequestedDeliveryDate.CustomFormat = FormatFromTag(txtRequestedDeliveryDate, "dd-MM-yyyy") 
      dtpRequestedDeliveryDate.Value = DateTime.ParseExact(dtpRequestedDeliveryDate.Value.ToString(dtpRequestedDeliveryDate.CustomFormat), dtpRequestedDeliveryDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .RequestedDeliveryDate < dtpRequestedDeliveryDate.MinDate Then dtpRequestedDeliveryDate.Checked = False Else dtpRequestedDeliveryDate.Checked = True 
      If Math.Abs(.RequestedDeliveryDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.RequestedDeliveryDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtRequestedDeliveryDate.Text = "" Else txtRequestedDeliveryDate.Text = .RequestedDeliveryDate.ToString(FormatFromTag(txtRequestedDeliveryDate, "dd-MM-yyyy"))
      txtRequestedDeliveryDay.Text = .RequestedDeliveryDay.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtRequestedDeliveryDay.MaxLength = 10 
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _SupplierOrder
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-SupplierOrder-ID-090417-0012", _Requester) : Return pFault 
      If cboOrderHeader.SelectedItem Is Nothing OrElse cboOrderHeader.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .OrderHeaderID = 0 
      Else 
        Dim pOrderHeaderID As Long = CType(cboOrderHeader.SelectedItem, clsComboListMember).KeyLong 
        If pOrderHeaderID = -1 Then .OrderHeaderID = 0 Else .OrderHeaderID = pOrderHeaderID 
      End If 
      .SupplierEmail = txtSupplierEmail.Text 
      .EmailSubject = txtEmailSubject.Text 
      .EmailBody = txtEmailBody.Text 
      .EmailStatus = CType(cboEmailStatus.SelectedValue, clsEnums.enmEmailStatus)
      If (dtpSentDate.ShowCheckBox AndAlso dtpSentDate.Checked = False) OrElse dtpSentDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .SentDate = Nothing Else .SentDate = dtpSentDate.Value
      .DeliveryMethod = CType(cboDeliveryMethod.SelectedValue, clsEnums.enmDeliveryMethod)
      If (dtpRequestedDeliveryDate.ShowCheckBox AndAlso dtpRequestedDeliveryDate.Checked = False) OrElse dtpRequestedDeliveryDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .RequestedDeliveryDate = Nothing Else .RequestedDeliveryDate = dtpRequestedDeliveryDate.Value.Date
      .RequestedDeliveryDay = txtRequestedDeliveryDay.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-SupplierOrder-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsSupplierOrder.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-SupplierOrder-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_SupplierOrder, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _SupplierOrder.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the SupplierOrder collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccSupplierOrderDefaultByID) 
      RaiseEvent evtUpdated(clsSupplierOrder.enmUpdateType.Standard, _SupplierOrder) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_SupplierOrder_evtAfterUpdate 
  Private Sub _SupplierOrder_evtAfterUpdate() Handles _SupplierOrder.evtAfterUpdate, _SupplierOrder.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_SupplierOrder) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _SupplierOrder = New clsSupplierOrder(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_SupplierOrder) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_SupplierOrder, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _SupplierOrder.ID.ToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _SupplierOrder.ID 
    pFault = _SupplierOrder.Delete(_Requester) 
    If pFault.isOK = True Then 
      _SupplierOrder = Nothing 
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
    If _SupplierOrder.OrderHeaderID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsSupplierOrder.enmParentProperty.OrderHeader) = clsSupplierOrder.enmParentProperty.OrderHeader Then 
      If _SupplierOrder.OrderHeaderID <> 0 Then RaiseEvent evtParentChosen(clsSupplierOrder.enmParentProperty.OrderHeader, _SupplierOrder.OrderHeaderID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "OrderHeader Detail" 
      fPopup.LoadControl("ctlccOrderHeader", _SupplierOrder.OrderHeaderID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblOrderHeader_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsSupplierOrder.enmParentProperty.OrderHeader) <> clsSupplierOrder.enmParentProperty.OrderHeader Then Exit Sub 
    lblOrderHeader.ForeColor = Color.Brown 
    'lblOrderHeader.Font = New Font(lblOrderHeader.Font.Name, lblOrderHeader.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblOrderHeader.BackColor = ccHelper.InvertColour(lblOrderHeader.ForeColor) 'did this instead 
    lblOrderHeader.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblOrderHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsSupplierOrder.enmParentProperty.OrderHeader) <> clsSupplierOrder.enmParentProperty.OrderHeader Then Exit Sub 
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("SupplierOrder", _SupplierOrder.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _SupplierOrder.DateAdded 
    pAuditIndexed.TableName = "SupplierOrder" 
    pAuditIndexed.RowID = _SupplierOrder.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Supplier Order'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlccSupplierOrder_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the SupplierOrder to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pSupplierOrder As clsSupplierOrder = _SupplierOrder 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pSupplierOrder.ToCSV) 
        Else 
          Clipboard.SetText(pSupplierOrder.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The SupplierOrder is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccSupplierOrder_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlSupplierOrder_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
