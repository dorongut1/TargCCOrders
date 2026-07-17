Public Class ctlccCustomer
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsCustomer.enmUpdateType) 
  Public Event evtAdd(ByVal vCustomer As clsCustomer) 
  Public Event evtBeforeUpdate(ByVal vCustomer As clsCustomer, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsCustomer.enmUpdateType, ByVal vCustomer As clsCustomer) 
  Public Event evtBeforeDelete(ByVal vCustomer As clsCustomer, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vCustomerID As Long) 
  Public Event evtCancelledEdit(ByVal vCustomer As clsCustomer) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vCustomer As clsCustomer) 
  
  Public Event evtParentChosen(ByVal vParentName As clsCustomer.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsCustomer.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsCustomer.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsCustomer.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsCustomer.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsCustomer.enmParentProperty) 
 
    End Sub 
  End Class 
 
  Private WithEvents _Customer As clsCustomer

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboCustomerType.Size = txtCustomerType.Size
    cboCustomerType.Location = txtCustomerType.Location
    cboAccountantMethod.Size = txtAccountantMethod.Size
    cboAccountantMethod.Location = txtAccountantMethod.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vCustomerID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCustomer As New clsCustomer() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vCustomerID <> 0 Then 
      pFault = pCustomer.GetByID(vCustomerID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pCustomer) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rCustomer As clsCustomer, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rCustomer)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rCustomer As clsCustomer) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _Customer = rCustomer 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboAccountantMethod.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboCustomerType() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboAccountantMethod() : If pFault.isOK = False Then Return pFault 
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
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rCustomer"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rCustomer As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rCustomer.GetType.Name = "clsCustomer" Then 
      ctlCustomer_Load(Nothing, Nothing) 
      Dim pCustomer As clsCustomer = CType(rCustomer, clsCustomer) 
      Return LoadControl(pCustomer) 
    Else 
      Dim pCustomerID As Long = CType(rCustomer, Long) 
      Return LoadControl(pCustomerID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "CustomerCode", _Requester) 
    If pStrg <> "" Then lblCustomerCode.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "CustomerName", _Requester) 
    If pStrg <> "" Then lblCustomerName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "Phone", _Requester) 
    If pStrg <> "" Then lblPhone.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "Email", _Requester) 
    If pStrg <> "" Then lblEmail.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "Address", _Requester) 
    If pStrg <> "" Then lblAddress.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "City", _Requester) 
    If pStrg <> "" Then lblCity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "TaxID", _Requester) 
    If pStrg <> "" Then lblTaxID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "CustomerType", _Requester) 
    If pStrg <> "" Then lblCustomerType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "PaymentTermsDays", _Requester) 
    If pStrg <> "" Then lblPaymentTermsDays.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "IsActive", _Requester) 
    If pStrg <> "" Then lblIsActive.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "Location", _Requester) 
    If pStrg <> "" Then lblLocation.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "AccountantEmail", _Requester) 
    If pStrg <> "" Then lblAccountantEmail.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "AccountantMethod", _Requester) 
    If pStrg <> "" Then lblAccountantMethod.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "InvoiceName", _Requester) 
    If pStrg <> "" Then lblInvoiceName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "ProfitabilityCode", _Requester) 
    If pStrg <> "" Then lblProfitabilityCode.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("Customer", "CustomerIdentifier", _Requester) 
    If pStrg <> "" Then lblCustomerIdentifier.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [Customer]() As clsCustomer
    Get 
      Return _Customer 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboCustomerType() As clsFault
    Dim pFault As New clsFault
 
    'If cboCustomerType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pCustomerTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsCustomer.enmParentProperty.CustomerType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pCustomerTypees.FillEnums(clsEnums.enmEnum.CustomerType, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pCustomerTypees = pTestCol
    End If
    
    pCustomerTypees.Remove(pCustomerTypees.FindByKey(clsEnums.enmCustomerType.UD))
    pCustomerTypees.SortByText()
    pCustomerTypees.AddToTop(clsEnums.enmCustomerType.UD, GetChoose(_Requester))

    With cboCustomerType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pCustomerTypees
    End With

    cboCustomerType.SelectedValue = _Customer.CustomerType 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboAccountantMethod() As clsFault
    Dim pFault As New clsFault
 
    'If cboAccountantMethod.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pAccountantMethodes As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(clsCustomer.enmParentProperty.AccountantMethod, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pAccountantMethodes.FillEnums(clsEnums.enmEnum.AccountantMethod, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pAccountantMethodes = pTestCol
    End If
    
    pAccountantMethodes.Remove(pAccountantMethodes.FindByKey(clsEnums.enmAccountantMethod.UD))
    pAccountantMethodes.SortByText()
    pAccountantMethodes.AddToTop(clsEnums.enmAccountantMethod.UD, GetChoose(_Requester))

    With cboAccountantMethod
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pAccountantMethodes
    End With

    cboAccountantMethod.SelectedValue = _Customer.AccountantMethod 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboCustomerType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustomerType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmCustomerType = CType(cboCustomerType.SelectedValue, clsEnums.enmCustomerType) 
    RaiseEvent evtCboSelectedIndexChanged(clsCustomer.enmParentProperty.CustomerType, pEnum.ToString) 
  End Sub 
  Private Sub cboAccountantMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAccountantMethod.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmAccountantMethod = CType(cboAccountantMethod.SelectedValue, clsEnums.enmAccountantMethod) 
    RaiseEvent evtCboSelectedIndexChanged(clsCustomer.enmParentProperty.AccountantMethod, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsCustomer.enmParentProperty = clsCustomer.enmParentProperty.UD 
    
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
    txtCustomerCode.ReadOnly = Not (vInEdit)
    txtCustomerCode.BackColor = pDefaultColour 
    txtCustomerName.ReadOnly = Not (vInEdit)
    txtCustomerName.BackColor = pDefaultColour 
    txtPhone.ReadOnly = Not (vInEdit)
    txtPhone.BackColor = pDefaultColour 
    txtEmail.ReadOnly = Not (vInEdit)
    txtEmail.BackColor = pDefaultColour 
    txtAddress.ReadOnly = Not (vInEdit)
    txtAddress.BackColor = pDefaultColour 
    txtCity.ReadOnly = Not (vInEdit)
    txtCity.BackColor = pDefaultColour 
    txtTaxID.ReadOnly = Not (vInEdit)
    txtTaxID.BackColor = pDefaultColour 
    txtCustomerType.ReadOnly = True
    txtCustomerType.Visible = Not (vInEdit)
    txtCustomerType.BackColor = pReadonlyColour 
    txtCustomerType.ForeColor = SetForeColor(vInEdit) 
    cboCustomerType.Visible = vInEdit
    txtPaymentTermsDays.ReadOnly = Not (vInEdit)
    txtPaymentTermsDays.BackColor = pDefaultColour 
    If vInEdit AndAlso _Customer.PaymentTermsDays = 0 Then txtPaymentTermsDays.Text = _Customer.PaymentTermsDays.ToString() 
    txtNotes.ReadOnly = Not (vInEdit)
    txtNotes.BackColor = pDefaultColour 
    chkIsActive.Enabled = True 
    txtLocation.ReadOnly = Not (vInEdit)
    txtLocation.BackColor = pDefaultColour 
    txtAccountantEmail.ReadOnly = Not (vInEdit)
    txtAccountantEmail.BackColor = pDefaultColour 
    txtAccountantMethod.ReadOnly = True
    txtAccountantMethod.Visible = Not (vInEdit)
    txtAccountantMethod.BackColor = pReadonlyColour 
    txtAccountantMethod.ForeColor = SetForeColor(vInEdit) 
    cboAccountantMethod.Visible = vInEdit
    txtInvoiceName.ReadOnly = Not (vInEdit)
    txtInvoiceName.BackColor = pDefaultColour 
    txtProfitabilityCode.ReadOnly = Not (vInEdit)
    txtProfitabilityCode.BackColor = pDefaultColour 
    txtCustomerIdentifier.ReadOnly = True 
    txtCustomerIdentifier.BackColor = pReadonlyColour 
    txtCustomerIdentifier.ForeColor = SetForeColor(vInEdit) 

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
      If _Customer.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_CustomerUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_CustomerDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_CustomerUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _Customer) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _Customer
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtCustomerCode.Text = .CustomerCode.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCustomerCode.MaxLength = 50 
      txtCustomerName.Text = .CustomerName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCustomerName.MaxLength = 255 
      txtPhone.Text = .Phone.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtPhone.MaxLength = 20 
      txtEmail.Text = .Email.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtEmail.MaxLength = 255 
      txtAddress.Text = .Address.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCity.Text = .City.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCity.MaxLength = 100 
      txtTaxID.Text = .TaxID.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtTaxID.MaxLength = 20 
      cboCustomerType.SelectedValue = .CustomerType
      txtCustomerType.Text = cboCustomerType.Text : If cboCustomerType.SelectedValue Is Nothing OrElse cboCustomerType.SelectedValue.ToString() = "UD" Then txtCustomerType.Text = ""    
      txtPaymentTermsDays.Text = .PaymentTermsDays.ToString(FormatFromTag(txtPaymentTermsDays, "#,##0"))
      txtPaymentTermsDays.Text = If(.PaymentTermsDays = 0, "", .PaymentTermsDays.ToString(FormatFromTag(txtPaymentTermsDays, "#,##0"))) 
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      chkIsActive.Checked = .IsActive
      txtLocation.Text = .Location.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLocation.MaxLength = 100 
      txtAccountantEmail.Text = .AccountantEmail.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtAccountantEmail.MaxLength = 255 
      cboAccountantMethod.SelectedValue = .AccountantMethod
      txtAccountantMethod.Text = cboAccountantMethod.Text : If cboAccountantMethod.SelectedValue Is Nothing OrElse cboAccountantMethod.SelectedValue.ToString() = "UD" Then txtAccountantMethod.Text = ""    
      txtInvoiceName.Text = .InvoiceName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtInvoiceName.MaxLength = 255 
      txtProfitabilityCode.Text = .ProfitabilityCode.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtProfitabilityCode.MaxLength = 50 
      txtCustomerIdentifier.Text = .CustomerIdentifier.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCustomerIdentifier.MaxLength = 306 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _Customer
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-Customer-ID-090417-0012", _Requester) : Return pFault 
      .CustomerCode = txtCustomerCode.Text 
      .CustomerName = txtCustomerName.Text 
      .Phone = txtPhone.Text 
      .Email = txtEmail.Text 
      .Address = txtAddress.Text 
      .City = txtCity.Text 
      .TaxID = txtTaxID.Text 
      .CustomerType = CType(cboCustomerType.SelectedValue, clsEnums.enmCustomerType)
      If Integer.TryParse(txtPaymentTermsDays.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .PaymentTermsDays) = False Then pFault.LogFreeTextFault(208, ".PaymentTermsDays", txtPaymentTermsDays.Text, "TRGT-Customer-PaymentTermsDays-090417-0013", _Requester) : Return pFault 
      .Notes = txtNotes.Text 
      .Location = txtLocation.Text 
      .AccountantEmail = txtAccountantEmail.Text 
      .AccountantMethod = CType(cboAccountantMethod.SelectedValue, clsEnums.enmAccountantMethod)
      .InvoiceName = txtInvoiceName.Text 
      .ProfitabilityCode = txtProfitabilityCode.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-Customer-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtPaymentTermsDays_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaymentTermsDays.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtPaymentTermsDays.Text 
    Dim pTest As Integer 
 
    If txtPaymentTermsDays.Text = "" Then Exit Sub 
    If txtPaymentTermsDays.Text = txtPaymentTermsDays.Name Then Exit Sub 
 
    If Integer.TryParse(txtPaymentTermsDays.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-Customer-PaymentTermsDays-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsCustomer.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-Customer-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_Customer, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _Customer.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the Customer collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccCustomerDefaultByID) 
      RaiseEvent evtUpdated(clsCustomer.enmUpdateType.Standard, _Customer) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_Customer_evtAfterUpdate 
  Private Sub _Customer_evtAfterUpdate() Handles _Customer.evtAfterUpdate, _Customer.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_Customer) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _Customer = New clsCustomer() 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_Customer) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_Customer, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _Customer.CustomerName & " " & _Customer.CustomerCode & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _Customer.ID 
    pFault = _Customer.Delete(_Requester) 
    If pFault.isOK = True Then 
      _Customer = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only
  Private Sub chkIsActive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsActive.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkIsActive.Checked = _Customer.IsActive
    End If
  End Sub

  'Now the Parents
  'SeparateUpdates 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  Private Sub ctlccCustomer_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the Customer to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pCustomer As clsCustomer = _Customer 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pCustomer.ToCSV) 
        Else 
          Clipboard.SetText(pCustomer.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The Customer is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccCustomer_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlCustomer_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
