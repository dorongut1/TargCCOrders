Public Class ctlccCustomerDebt
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsCustomerDebt.enmUpdateType) 
  Public Event evtAdd(ByVal vCustomerDebt As clsCustomerDebt) 
  Public Event evtBeforeUpdate(ByVal vCustomerDebt As clsCustomerDebt, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsCustomerDebt.enmUpdateType, ByVal vCustomerDebt As clsCustomerDebt) 
  Public Event evtBeforeDelete(ByVal vCustomerDebt As clsCustomerDebt, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vCustomerDebtID As Long) 
  Public Event evtCancelledEdit(ByVal vCustomerDebt As clsCustomerDebt) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vCustomerDebt As clsCustomerDebt) 
  
  Public Event evtParentChosen(ByVal vParentName As clsCustomerDebt.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsCustomerDebt.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsCustomerDebt.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsCustomerDebt.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsCustomerDebt.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsCustomerDebt.enmParentProperty) 
      _EnableParentLinks.Add(clsCustomerDebt.enmParentProperty.Customer) 
      _EnableParentLinks.Add(clsCustomerDebt.enmParentProperty.OrderHeader) 
 
    End Sub 
  End Class 
 
  Private WithEvents _CustomerDebt As clsCustomerDebt

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlCustomerDebt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboOrderHeader.Size = txtOrderHeader.Size
    cboOrderHeader.Location = txtOrderHeader.Location
    dtpDebtDate.Size = txtDebtDate.Size
    dtpDebtDate.Location = txtDebtDate.Location
    dtpDueDate.Size = txtDueDate.Size
    dtpDueDate.Location = txtDueDate.Location
    cboDebtStatus.Size = txtDebtStatus.Size
    cboDebtStatus.Location = txtDebtStatus.Location
    dtpDeliveryDate.Size = txtDeliveryDate.Size
    dtpDeliveryDate.Location = txtDeliveryDate.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vCustomerDebtID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCustomerDebt As New clsCustomerDebt(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vCustomerDebtID <> 0 Then 
      pFault = pCustomerDebt.GetByID(vCustomerDebtID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pCustomerDebt) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rCustomerDebt As clsCustomerDebt, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rCustomerDebt)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rCustomerDebt As clsCustomerDebt) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _CustomerDebt = rCustomerDebt 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboDebtStatus.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.ccCustomerDefaultByID, Cache.enmLevel.Previous) 
      MyCache.SetLevel(clsEnums.enmComboListType.ccOrderHeaderDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboDebtStatus() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboOrderHeader() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rCustomerDebt"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rCustomerDebt As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rCustomerDebt.GetType.Name = "clsCustomerDebt" Then 
      ctlCustomerDebt_Load(Nothing, Nothing) 
      Dim pCustomerDebt As clsCustomerDebt = CType(rCustomerDebt, clsCustomerDebt) 
      Return LoadControl(pCustomerDebt) 
    Else 
      Dim pCustomerDebtID As Long = CType(rCustomerDebt, Long) 
      Return LoadControl(pCustomerDebtID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "Customer", _Requester) 
    If pStrg <> "" Then lblCustomer.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "OrderHeader", _Requester) 
    If pStrg <> "" Then lblOrderHeader.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "DebtAmount", _Requester) 
    If pStrg <> "" Then lblDebtAmount.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "PaidAmount", _Requester) 
    If pStrg <> "" Then lblPaidAmount.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "RemainingAmount", _Requester) 
    If pStrg <> "" Then lblRemainingAmount.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "DebtDate", _Requester) 
    If pStrg <> "" Then lblDebtDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "DueDate", _Requester) 
    If pStrg <> "" Then lblDueDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "DebtStatus", _Requester) 
    If pStrg <> "" Then lblDebtStatus.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "NeedsAttention", _Requester) 
    If pStrg <> "" Then lblNeedsAttention.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "ProductTypes", _Requester) 
    If pStrg <> "" Then lblProductTypes.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("CustomerDebt", "DeliveryDate", _Requester) 
    If pStrg <> "" Then lblDeliveryDate.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [CustomerDebt]() As clsCustomerDebt
    Get 
      Return _CustomerDebt 
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
    RaiseEvent evtOverrideLoadIntelliCombo(clsCustomerDebt.enmParentProperty.Customer, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
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
    
    If _CustomerDebt.CustomerID > 0 Then cboCustomer.ValueSelect(_CustomerDebt.CustomerID) Else cboCustomer.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboOrderHeader() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.ccOrderHeaderDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(clsCustomerDebt.enmParentProperty.OrderHeader, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
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
    
    If _CustomerDebt.OrderHeaderID > 0 Then cboOrderHeader.ValueSelect(_CustomerDebt.OrderHeaderID) Else cboOrderHeader.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboDebtStatus() As clsFault
    Dim pFault As New clsFault
 
    'If cboDebtStatus.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pDebtStatuses As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsCustomerDebt.enmParentProperty.DebtStatus, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pDebtStatuses.FillEnums(clsEnums.enmEnum.DebtStatus, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pDebtStatuses = pTestCol
    End If
    
    pDebtStatuses.Remove(pDebtStatuses.FindByKey(clsEnums.enmDebtStatus.UD))
    pDebtStatuses.SortByText()
    pDebtStatuses.AddToTop(clsEnums.enmDebtStatus.UD, GetChoose(_Requester))

    With cboDebtStatus
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pDebtStatuses
    End With

    cboDebtStatus.SelectedValue = _CustomerDebt.DebtStatus 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboCustomer_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboCustomer.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsCustomerDebt.enmParentProperty.Customer, pUniqueCode) 
  End Sub 
  Private Sub cboOrderHeader_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboOrderHeader.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsCustomerDebt.enmParentProperty.OrderHeader, pUniqueCode) 
  End Sub 
  Private Sub cboDebtStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDebtStatus.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmDebtStatus = CType(cboDebtStatus.SelectedValue, clsEnums.enmDebtStatus) 
    RaiseEvent evtCboSelectedIndexChanged(clsCustomerDebt.enmParentProperty.DebtStatus, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsCustomerDebt.enmParentProperty = clsCustomerDebt.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsCustomerDebt.enmParentProperty.Customer) = clsCustomerDebt.enmParentProperty.Customer Then 
      lblCustomer.ForeColor = Color.Brown 
    End If 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsCustomerDebt.enmParentProperty.OrderHeader) = clsCustomerDebt.enmParentProperty.OrderHeader Then 
      lblOrderHeader.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
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
    txtDebtAmount.ReadOnly = Not (vInEdit)
    txtDebtAmount.BackColor = pDefaultColour 
    txtPaidAmount.ReadOnly = Not (vInEdit)
    txtPaidAmount.BackColor = pDefaultColour 
    If vInEdit AndAlso _CustomerDebt.PaidAmount = 0D Then txtPaidAmount.Text = _CustomerDebt.PaidAmount.ToString() 
    txtRemainingAmount.ReadOnly = True 
    txtRemainingAmount.BackColor = pReadonlyColour 
    txtRemainingAmount.ForeColor = SetForeColor(vInEdit) 
    dtpDebtDate.Visible = vInEdit
    txtDebtDate.Visible = Not (vInEdit)
    txtDebtDate.BackColor = pReadonlyColour 
    txtDebtDate.ForeColor = SetForeColor(vInEdit) 
    txtDebtDate.ReadOnly = True
    dtpDueDate.Visible = vInEdit
    txtDueDate.Visible = Not (vInEdit)
    txtDueDate.BackColor = pReadonlyColour 
    txtDueDate.ForeColor = SetForeColor(vInEdit) 
    txtDueDate.ReadOnly = True
    txtDebtStatus.ReadOnly = True
    txtDebtStatus.Visible = Not (vInEdit)
    txtDebtStatus.BackColor = pReadonlyColour 
    txtDebtStatus.ForeColor = SetForeColor(vInEdit) 
    cboDebtStatus.Visible = vInEdit
    txtNotes.ReadOnly = Not (vInEdit)
    txtNotes.BackColor = pDefaultColour 
    chkNeedsAttention.Enabled = True 
    txtProductTypes.ReadOnly = Not (vInEdit)
    txtProductTypes.BackColor = pDefaultColour 
    dtpDeliveryDate.Visible = vInEdit
    txtDeliveryDate.Visible = Not (vInEdit)
    txtDeliveryDate.BackColor = pReadonlyColour 
    txtDeliveryDate.ForeColor = SetForeColor(vInEdit) 
    txtDeliveryDate.ReadOnly = True

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
      If _CustomerDebt.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_CustomerDebtUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_CustomerDebtDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_CustomerDebtUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _CustomerDebt) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _CustomerDebt
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtCustomer.Text = .CustomerText 
      txtOrderHeader.Text = .OrderHeaderText 
      txtDebtAmount.Text = .DebtAmount.ToString(FormatFromTag(txtDebtAmount, "#,##0.00"))
      txtPaidAmount.Text = .PaidAmount.ToString(FormatFromTag(txtPaidAmount, "#,##0.00"))
      txtPaidAmount.Text = If(.PaidAmount = 0D, "", .PaidAmount.ToString(FormatFromTag(txtPaidAmount, "#,##0.00"))) 
      txtRemainingAmount.Text = .RemainingAmount.ToString(FormatFromTag(txtRemainingAmount, "#,##0.00"))
      If .DebtDate < dtpDebtDate.MinDate Then dtpDebtDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpDebtDate.Value = .DebtDate.Date
      dtpDebtDate.CustomFormat = FormatFromTag(txtDebtDate, "dd-MM-yyyy") 
      dtpDebtDate.Value = DateTime.ParseExact(dtpDebtDate.Value.ToString(dtpDebtDate.CustomFormat), dtpDebtDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .DebtDate < dtpDebtDate.MinDate Then dtpDebtDate.Checked = False Else dtpDebtDate.Checked = True 
      If Math.Abs(.DebtDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.DebtDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtDebtDate.Text = "" Else txtDebtDate.Text = .DebtDate.ToString(FormatFromTag(txtDebtDate, "dd-MM-yyyy"))
      If .DueDate < dtpDueDate.MinDate Then dtpDueDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpDueDate.Value = .DueDate.Date
      dtpDueDate.CustomFormat = FormatFromTag(txtDueDate, "dd-MM-yyyy") 
      dtpDueDate.Value = DateTime.ParseExact(dtpDueDate.Value.ToString(dtpDueDate.CustomFormat), dtpDueDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .DueDate < dtpDueDate.MinDate Then dtpDueDate.Checked = False Else dtpDueDate.Checked = True 
      If Math.Abs(.DueDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.DueDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtDueDate.Text = "" Else txtDueDate.Text = .DueDate.ToString(FormatFromTag(txtDueDate, "dd-MM-yyyy"))
      cboDebtStatus.SelectedValue = .DebtStatus
      txtDebtStatus.Text = cboDebtStatus.Text : If cboDebtStatus.SelectedValue Is Nothing OrElse cboDebtStatus.SelectedValue.ToString() = "UD" Then txtDebtStatus.Text = ""    
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      chkNeedsAttention.Checked = .NeedsAttention
      txtProductTypes.Text = .ProductTypes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtProductTypes.MaxLength = 500 
      If .DeliveryDate < dtpDeliveryDate.MinDate Then dtpDeliveryDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpDeliveryDate.Value = .DeliveryDate.Date
      dtpDeliveryDate.CustomFormat = FormatFromTag(txtDeliveryDate, "dd-MM-yyyy") 
      dtpDeliveryDate.Value = DateTime.ParseExact(dtpDeliveryDate.Value.ToString(dtpDeliveryDate.CustomFormat), dtpDeliveryDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .DeliveryDate < dtpDeliveryDate.MinDate Then dtpDeliveryDate.Checked = False Else dtpDeliveryDate.Checked = True 
      If Math.Abs(.DeliveryDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.DeliveryDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtDeliveryDate.Text = "" Else txtDeliveryDate.Text = .DeliveryDate.ToString(FormatFromTag(txtDeliveryDate, "dd-MM-yyyy"))
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _CustomerDebt
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-CustomerDebt-ID-090417-0012", _Requester) : Return pFault 
      If cboCustomer.SelectedItem Is Nothing OrElse cboCustomer.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .CustomerID = 0 
      Else 
        Dim pCustomerID As Long = CType(cboCustomer.SelectedItem, clsComboListMember).KeyLong 
        If pCustomerID = -1 Then .CustomerID = 0 Else .CustomerID = pCustomerID 
      End If 
      If cboOrderHeader.SelectedItem Is Nothing OrElse cboOrderHeader.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .OrderHeaderID = 0 
      Else 
        Dim pOrderHeaderID As Long = CType(cboOrderHeader.SelectedItem, clsComboListMember).KeyLong 
        If pOrderHeaderID = -1 Then .OrderHeaderID = 0 Else .OrderHeaderID = pOrderHeaderID 
      End If 
      If Decimal.TryParse(txtDebtAmount.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .DebtAmount) = False Then pFault.LogFreeTextFault(208, ".DebtAmount", txtDebtAmount.Text, "TRGT-CustomerDebt-DebtAmount-090417-0016", _Requester) : Return pFault 
      If Decimal.TryParse(txtPaidAmount.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .PaidAmount) = False Then pFault.LogFreeTextFault(208, ".PaidAmount", txtPaidAmount.Text, "TRGT-CustomerDebt-PaidAmount-090417-0016", _Requester) : Return pFault 
      If (dtpDebtDate.ShowCheckBox AndAlso dtpDebtDate.Checked = False) OrElse dtpDebtDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .DebtDate = Nothing Else .DebtDate = dtpDebtDate.Value.Date
      If (dtpDueDate.ShowCheckBox AndAlso dtpDueDate.Checked = False) OrElse dtpDueDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .DueDate = Nothing Else .DueDate = dtpDueDate.Value.Date
      .DebtStatus = CType(cboDebtStatus.SelectedValue, clsEnums.enmDebtStatus)
      .Notes = txtNotes.Text 
      .ProductTypes = txtProductTypes.Text 
      If (dtpDeliveryDate.ShowCheckBox AndAlso dtpDeliveryDate.Checked = False) OrElse dtpDeliveryDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .DeliveryDate = Nothing Else .DeliveryDate = dtpDeliveryDate.Value.Date
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-CustomerDebt-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtDebtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDebtAmount.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtDebtAmount.Text 
    Dim pTest As Decimal 
 
    If txtDebtAmount.Text = "" Then Exit Sub 
    If txtDebtAmount.Text = txtDebtAmount.Name Then Exit Sub 
 
    If Decimal.TryParse(txtDebtAmount.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-CustomerDebt-DebtAmount-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtPaidAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaidAmount.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtPaidAmount.Text 
    Dim pTest As Decimal 
 
    If txtPaidAmount.Text = "" Then Exit Sub 
    If txtPaidAmount.Text = txtPaidAmount.Name Then Exit Sub 
 
    If Decimal.TryParse(txtPaidAmount.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-CustomerDebt-PaidAmount-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsCustomerDebt.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-CustomerDebt-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_CustomerDebt, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _CustomerDebt.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the CustomerDebt collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccCustomerDebtDefaultByID) 
      RaiseEvent evtUpdated(clsCustomerDebt.enmUpdateType.Standard, _CustomerDebt) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_CustomerDebt_evtAfterUpdate 
  Private Sub _CustomerDebt_evtAfterUpdate() Handles _CustomerDebt.evtAfterUpdate, _CustomerDebt.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_CustomerDebt) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _CustomerDebt = New clsCustomerDebt(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_CustomerDebt) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_CustomerDebt, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _CustomerDebt.CustomerID.ToString() & "bt of " & _CustomerDebt.DebtAmount.ToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _CustomerDebt.ID 
    pFault = _CustomerDebt.Delete(_Requester) 
    If pFault.isOK = True Then 
      _CustomerDebt = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only
  Private Sub chkNeedsAttention_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNeedsAttention.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkNeedsAttention.Checked = _CustomerDebt.NeedsAttention
    End If
  End Sub

  'Now the Parents
  Private Sub lblCustomer_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.DoubleClick 
    If _CustomerDebt.CustomerID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsCustomerDebt.enmParentProperty.Customer) = clsCustomerDebt.enmParentProperty.Customer Then 
      If _CustomerDebt.CustomerID <> 0 Then RaiseEvent evtParentChosen(clsCustomerDebt.enmParentProperty.Customer, _CustomerDebt.CustomerID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "Customer Detail" 
      fPopup.LoadControl("ctlccCustomer", _CustomerDebt.CustomerID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblCustomer_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsCustomerDebt.enmParentProperty.Customer) <> clsCustomerDebt.enmParentProperty.Customer Then Exit Sub 
    lblCustomer.ForeColor = Color.Brown 
    'lblCustomer.Font = New Font(lblCustomer.Font.Name, lblCustomer.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblCustomer.BackColor = ccHelper.InvertColour(lblCustomer.ForeColor) 'did this instead 
    lblCustomer.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblCustomer_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsCustomerDebt.enmParentProperty.Customer) <> clsCustomerDebt.enmParentProperty.Customer Then Exit Sub 
    lblCustomer.ForeColor = Color.Brown 
    'lblCustomer.Font = New Font(lblCustomer.Font.Name, lblCustomer.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblCustomer.BackColor = Me.BackColor 'did this instead 
    lblCustomer.Cursor = Cursors.Default 
  End Sub 
 
  Private Sub lblOrderHeader_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.DoubleClick 
    If _CustomerDebt.OrderHeaderID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsCustomerDebt.enmParentProperty.OrderHeader) = clsCustomerDebt.enmParentProperty.OrderHeader Then 
      If _CustomerDebt.OrderHeaderID <> 0 Then RaiseEvent evtParentChosen(clsCustomerDebt.enmParentProperty.OrderHeader, _CustomerDebt.OrderHeaderID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "OrderHeader Detail" 
      fPopup.LoadControl("ctlccOrderHeader", _CustomerDebt.OrderHeaderID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblOrderHeader_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsCustomerDebt.enmParentProperty.OrderHeader) <> clsCustomerDebt.enmParentProperty.OrderHeader Then Exit Sub 
    lblOrderHeader.ForeColor = Color.Brown 
    'lblOrderHeader.Font = New Font(lblOrderHeader.Font.Name, lblOrderHeader.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblOrderHeader.BackColor = ccHelper.InvertColour(lblOrderHeader.ForeColor) 'did this instead 
    lblOrderHeader.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblOrderHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrderHeader.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsCustomerDebt.enmParentProperty.OrderHeader) <> clsCustomerDebt.enmParentProperty.OrderHeader Then Exit Sub 
    lblOrderHeader.ForeColor = Color.Brown 
    'lblOrderHeader.Font = New Font(lblOrderHeader.Font.Name, lblOrderHeader.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblOrderHeader.BackColor = Me.BackColor 'did this instead 
    lblOrderHeader.Cursor = Cursors.Default 
  End Sub 
 
  'SeparateUpdates 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  Private Sub ctlccCustomerDebt_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the CustomerDebt to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pCustomerDebt As clsCustomerDebt = _CustomerDebt 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pCustomerDebt.ToCSV) 
        Else 
          Clipboard.SetText(pCustomerDebt.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The CustomerDebt is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccCustomerDebt_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlCustomerDebt_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
