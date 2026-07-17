Public Class ctlccProductPriceHist
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsProductPriceHist.enmUpdateType) 
  Public Event evtAdd(ByVal vProductPriceHist As clsProductPriceHist) 
  Public Event evtBeforeUpdate(ByVal vProductPriceHist As clsProductPriceHist, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsProductPriceHist.enmUpdateType, ByVal vProductPriceHist As clsProductPriceHist) 
  Public Event evtBeforeDelete(ByVal vProductPriceHist As clsProductPriceHist, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vProductPriceHistID As Long) 
  Public Event evtCancelledEdit(ByVal vProductPriceHist As clsProductPriceHist) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vProductPriceHist As clsProductPriceHist) 
  
  Public Event evtParentChosen(ByVal vParentName As clsProductPriceHist.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsProductPriceHist.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsProductPriceHist.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsProductPriceHist.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsProductPriceHist.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsProductPriceHist.enmParentProperty) 
 
    End Sub 
  End Class 
 
  Private WithEvents _ProductPriceHist As clsProductPriceHist

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlProductPriceHist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    dtpValidFrom.Size = txtValidFrom.Size
    dtpValidFrom.Location = txtValidFrom.Location
    dtpValidTo.Size = txtValidTo.Size
    dtpValidTo.Location = txtValidTo.Location
    dtpArchivedDate.Size = txtArchivedDate.Size
    dtpArchivedDate.Location = txtArchivedDate.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vProductPriceHistID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pProductPriceHist As New clsProductPriceHist() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vProductPriceHistID <> 0 Then 
      pFault = pProductPriceHist.GetByID(vProductPriceHistID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pProductPriceHist) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rProductPriceHist As clsProductPriceHist, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rProductPriceHist)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rProductPriceHist As clsProductPriceHist) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _ProductPriceHist = rProductPriceHist 

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
 
    If cboCustomerType.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboCustomerType() : If pFault.isOK = False Then Return pFault 
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
  ''' <param name="rProductPriceHist"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rProductPriceHist As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rProductPriceHist.GetType.Name = "clsProductPriceHist" Then 
      ctlProductPriceHist_Load(Nothing, Nothing) 
      Dim pProductPriceHist As clsProductPriceHist = CType(rProductPriceHist, clsProductPriceHist) 
      Return LoadControl(pProductPriceHist) 
    Else 
      Dim pProductPriceHistID As Long = CType(rProductPriceHist, Long) 
      Return LoadControl(pProductPriceHistID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ProductID", _Requester) 
    If pStrg <> "" Then lblProductID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "CustomerType", _Requester) 
    If pStrg <> "" Then lblCustomerType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "BaseCost", _Requester) 
    If pStrg <> "" Then lblBaseCost.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "SellingPrice", _Requester) 
    If pStrg <> "" Then lblSellingPrice.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "MinQuantity", _Requester) 
    If pStrg <> "" Then lblMinQuantity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "DiscountPercent", _Requester) 
    If pStrg <> "" Then lblDiscountPercent.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ValidFrom", _Requester) 
    If pStrg <> "" Then lblValidFrom.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ValidTo", _Requester) 
    If pStrg <> "" Then lblValidTo.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ArchivedDate", _Requester) 
    If pStrg <> "" Then lblArchivedDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ArchivedReason", _Requester) 
    If pStrg <> "" Then lblArchivedReason.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "OriginalPriceID", _Requester) 
    If pStrg <> "" Then lblOriginalPriceID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "AddFieldsHere", _Requester) 
    If pStrg <> "" Then lblAddFieldsHere.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [ProductPriceHist]() As clsProductPriceHist
    Get 
      Return _ProductPriceHist 
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
    RaiseEvent evtOverrideLoadCbo(clsProductPriceHist.enmParentProperty.CustomerType, pTestCol, pPrompt) 
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

    cboCustomerType.SelectedValue = _ProductPriceHist.CustomerType 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboCustomerType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustomerType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmCustomerType = CType(cboCustomerType.SelectedValue, clsEnums.enmCustomerType) 
    RaiseEvent evtCboSelectedIndexChanged(clsProductPriceHist.enmParentProperty.CustomerType, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsProductPriceHist.enmParentProperty = clsProductPriceHist.enmParentProperty.UD 
    
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
    txtProductID.ReadOnly = Not (vInEdit)
    txtProductID.BackColor = pDefaultColour 
    txtCustomerType.ReadOnly = True
    txtCustomerType.Visible = Not (vInEdit)
    txtCustomerType.BackColor = pReadonlyColour 
    txtCustomerType.ForeColor = SetForeColor(vInEdit) 
    cboCustomerType.Visible = vInEdit
    txtBaseCost.ReadOnly = Not (vInEdit)
    txtBaseCost.BackColor = pDefaultColour 
    If vInEdit AndAlso _ProductPriceHist.BaseCost = 0D Then txtBaseCost.Text = _ProductPriceHist.BaseCost.ToString() 
    txtSellingPrice.ReadOnly = Not (vInEdit)
    txtSellingPrice.BackColor = pDefaultColour 
    txtMinQuantity.ReadOnly = Not (vInEdit)
    txtMinQuantity.BackColor = pDefaultColour 
    If vInEdit AndAlso _ProductPriceHist.MinQuantity = 1 Then txtMinQuantity.Text = _ProductPriceHist.MinQuantity.ToString() 
    txtDiscountPercent.ReadOnly = Not (vInEdit)
    txtDiscountPercent.BackColor = pDefaultColour 
    If vInEdit AndAlso _ProductPriceHist.DiscountPercent = 0D Then txtDiscountPercent.Text = _ProductPriceHist.DiscountPercent.ToString() 
    dtpValidFrom.Visible = vInEdit
    txtValidFrom.Visible = Not (vInEdit)
    txtValidFrom.BackColor = pReadonlyColour 
    txtValidFrom.ForeColor = SetForeColor(vInEdit) 
    txtValidFrom.ReadOnly = True
    dtpValidTo.Visible = vInEdit
    txtValidTo.Visible = Not (vInEdit)
    txtValidTo.BackColor = pReadonlyColour 
    txtValidTo.ForeColor = SetForeColor(vInEdit) 
    txtValidTo.ReadOnly = True
    dtpArchivedDate.Visible = vInEdit
    txtArchivedDate.Visible = Not (vInEdit)
    txtArchivedDate.BackColor = pReadonlyColour 
    txtArchivedDate.ForeColor = SetForeColor(vInEdit) 
    txtArchivedDate.ReadOnly = True
    txtArchivedReason.ReadOnly = Not (vInEdit)
    txtArchivedReason.BackColor = pDefaultColour 
    txtOriginalPriceID.ReadOnly = Not (vInEdit)
    txtOriginalPriceID.BackColor = pDefaultColour 
    txtNotes.ReadOnly = Not (vInEdit)
    txtNotes.BackColor = pDefaultColour 
    txtAddFieldsHere.ReadOnly = Not (vInEdit)
    txtAddFieldsHere.BackColor = pDefaultColour 

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
      If _ProductPriceHist.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceHistDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _ProductPriceHist) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _ProductPriceHist
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtProductID.Text = .ProductID.ToString(FormatFromTag(txtProductID, "#,##0"))
      cboCustomerType.SelectedValue = .CustomerType
      txtCustomerType.Text = cboCustomerType.Text : If cboCustomerType.SelectedValue Is Nothing OrElse cboCustomerType.SelectedValue.ToString() = "UD" Then txtCustomerType.Text = ""    
      txtBaseCost.Text = .BaseCost.ToString(FormatFromTag(txtBaseCost, "#,##0.00"))
      txtBaseCost.Text = If(.BaseCost = 0D, "", .BaseCost.ToString(FormatFromTag(txtBaseCost, "#,##0.00"))) 
      txtSellingPrice.Text = .SellingPrice.ToString(FormatFromTag(txtSellingPrice, "#,##0.00"))
      txtMinQuantity.Text = .MinQuantity.ToString(FormatFromTag(txtMinQuantity, "#,##0"))
      txtMinQuantity.Text = If(.MinQuantity = 1, "", .MinQuantity.ToString(FormatFromTag(txtMinQuantity, "#,##0"))) 
      txtDiscountPercent.Text = .DiscountPercent.ToString(FormatFromTag(txtDiscountPercent, "#,##0.00"))
      txtDiscountPercent.Text = If(.DiscountPercent = 0D, "", .DiscountPercent.ToString(FormatFromTag(txtDiscountPercent, "#,##0.00"))) 
      If .ValidFrom < dtpValidFrom.MinDate Then dtpValidFrom.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpValidFrom.Value = .ValidFrom.Date
      dtpValidFrom.CustomFormat = FormatFromTag(txtValidFrom, "dd-MM-yyyy") 
      dtpValidFrom.Value = DateTime.ParseExact(dtpValidFrom.Value.ToString(dtpValidFrom.CustomFormat), dtpValidFrom.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .ValidFrom < dtpValidFrom.MinDate Then dtpValidFrom.Checked = False Else dtpValidFrom.Checked = True 
      If Math.Abs(.ValidFrom.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.ValidFrom.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtValidFrom.Text = "" Else txtValidFrom.Text = .ValidFrom.ToString(FormatFromTag(txtValidFrom, "dd-MM-yyyy"))
      If .ValidTo < dtpValidTo.MinDate Then dtpValidTo.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpValidTo.Value = .ValidTo.Date
      dtpValidTo.CustomFormat = FormatFromTag(txtValidTo, "dd-MM-yyyy") 
      dtpValidTo.Value = DateTime.ParseExact(dtpValidTo.Value.ToString(dtpValidTo.CustomFormat), dtpValidTo.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .ValidTo < dtpValidTo.MinDate Then dtpValidTo.Checked = False Else dtpValidTo.Checked = True 
      If Math.Abs(.ValidTo.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.ValidTo.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtValidTo.Text = "" Else txtValidTo.Text = .ValidTo.ToString(FormatFromTag(txtValidTo, "dd-MM-yyyy"))
      If .ArchivedDate < dtpArchivedDate.MinDate Then dtpArchivedDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpArchivedDate.Value = .ArchivedDate
      dtpArchivedDate.CustomFormat = FormatFromTag(txtArchivedDate, "dd-MM-yyyy HH:mm:ss") 
      dtpArchivedDate.Value = DateTime.ParseExact(dtpArchivedDate.Value.ToString(dtpArchivedDate.CustomFormat), dtpArchivedDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .ArchivedDate < dtpArchivedDate.MinDate Then dtpArchivedDate.Checked = False Else dtpArchivedDate.Checked = True 
      If Math.Abs(.ArchivedDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.ArchivedDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtArchivedDate.Text = "" Else txtArchivedDate.Text = .ArchivedDate.ToString(FormatFromTag(txtArchivedDate, "dd-MM-yyyy HH:mm:ss"))
      txtArchivedReason.Text = .ArchivedReason.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtArchivedReason.MaxLength = 255 
      txtOriginalPriceID.Text = .OriginalPriceID.ToString(FormatFromTag(txtOriginalPriceID, "#,##0"))
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtAddFieldsHere.Text = .AddFieldsHere.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtAddFieldsHere.MaxLength = 50 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _ProductPriceHist
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-ProductPriceHist-ID-090417-0012", _Requester) : Return pFault 
      If Long.TryParse(txtProductID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ProductID) = False Then pFault.LogFreeTextFault(208, ".ProductID", txtProductID.Text, "TRGT-ProductPriceHist-ProductID-090417-0012", _Requester) : Return pFault 
      .CustomerType = CType(cboCustomerType.SelectedValue, clsEnums.enmCustomerType)
      If Decimal.TryParse(txtBaseCost.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .BaseCost) = False Then pFault.LogFreeTextFault(208, ".BaseCost", txtBaseCost.Text, "TRGT-ProductPriceHist-BaseCost-090417-0016", _Requester) : Return pFault 
      If Decimal.TryParse(txtSellingPrice.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .SellingPrice) = False Then pFault.LogFreeTextFault(208, ".SellingPrice", txtSellingPrice.Text, "TRGT-ProductPriceHist-SellingPrice-090417-0016", _Requester) : Return pFault 
      If Integer.TryParse(txtMinQuantity.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .MinQuantity) = False Then pFault.LogFreeTextFault(208, ".MinQuantity", txtMinQuantity.Text, "TRGT-ProductPriceHist-MinQuantity-090417-0013", _Requester) : Return pFault 
      If Decimal.TryParse(txtDiscountPercent.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .DiscountPercent) = False Then pFault.LogFreeTextFault(208, ".DiscountPercent", txtDiscountPercent.Text, "TRGT-ProductPriceHist-DiscountPercent-090417-0016", _Requester) : Return pFault 
      If (dtpValidFrom.ShowCheckBox AndAlso dtpValidFrom.Checked = False) OrElse dtpValidFrom.Value = New Date(1900, 1, 1, 0, 0, 0) Then .ValidFrom = Nothing Else .ValidFrom = dtpValidFrom.Value.Date
      If (dtpValidTo.ShowCheckBox AndAlso dtpValidTo.Checked = False) OrElse dtpValidTo.Value = New Date(1900, 1, 1, 0, 0, 0) Then .ValidTo = Nothing Else .ValidTo = dtpValidTo.Value.Date
      If (dtpArchivedDate.ShowCheckBox AndAlso dtpArchivedDate.Checked = False) OrElse dtpArchivedDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .ArchivedDate = Nothing Else .ArchivedDate = dtpArchivedDate.Value
      .ArchivedReason = txtArchivedReason.Text 
      If Long.TryParse(txtOriginalPriceID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .OriginalPriceID) = False Then pFault.LogFreeTextFault(208, ".OriginalPriceID", txtOriginalPriceID.Text, "TRGT-ProductPriceHist-OriginalPriceID-090417-0012", _Requester) : Return pFault 
      .Notes = txtNotes.Text 
      .AddFieldsHere = txtAddFieldsHere.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-ProductPriceHist-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtProductID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProductID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtProductID.Text 
    Dim pTest As Long 
 
    If txtProductID.Text = "" Then Exit Sub 
    If txtProductID.Text = txtProductID.Name Then Exit Sub 
 
    If Long.TryParse(txtProductID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-ProductPriceHist-ProductID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtBaseCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBaseCost.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtBaseCost.Text 
    Dim pTest As Decimal 
 
    If txtBaseCost.Text = "" Then Exit Sub 
    If txtBaseCost.Text = txtBaseCost.Name Then Exit Sub 
 
    If Decimal.TryParse(txtBaseCost.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-ProductPriceHist-BaseCost-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtSellingPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSellingPrice.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtSellingPrice.Text 
    Dim pTest As Decimal 
 
    If txtSellingPrice.Text = "" Then Exit Sub 
    If txtSellingPrice.Text = txtSellingPrice.Name Then Exit Sub 
 
    If Decimal.TryParse(txtSellingPrice.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-ProductPriceHist-SellingPrice-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtMinQuantity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMinQuantity.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtMinQuantity.Text 
    Dim pTest As Integer 
 
    If txtMinQuantity.Text = "" Then Exit Sub 
    If txtMinQuantity.Text = txtMinQuantity.Name Then Exit Sub 
 
    If Integer.TryParse(txtMinQuantity.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-ProductPriceHist-MinQuantity-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtDiscountPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscountPercent.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtDiscountPercent.Text 
    Dim pTest As Decimal 
 
    If txtDiscountPercent.Text = "" Then Exit Sub 
    If txtDiscountPercent.Text = txtDiscountPercent.Name Then Exit Sub 
 
    If Decimal.TryParse(txtDiscountPercent.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-ProductPriceHist-DiscountPercent-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtOriginalPriceID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOriginalPriceID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtOriginalPriceID.Text 
    Dim pTest As Long 
 
    If txtOriginalPriceID.Text = "" Then Exit Sub 
    If txtOriginalPriceID.Text = txtOriginalPriceID.Name Then Exit Sub 
 
    If Long.TryParse(txtOriginalPriceID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-ProductPriceHist-OriginalPriceID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsProductPriceHist.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-ProductPriceHist-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_ProductPriceHist, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _ProductPriceHist.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the ProductPriceHist collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccProductPriceHistDefaultByID) 
      RaiseEvent evtUpdated(clsProductPriceHist.enmUpdateType.Standard, _ProductPriceHist) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_ProductPriceHist_evtAfterUpdate 
  Private Sub _ProductPriceHist_evtAfterUpdate() Handles _ProductPriceHist.evtAfterUpdate, _ProductPriceHist.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_ProductPriceHist) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _ProductPriceHist = New clsProductPriceHist() 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_ProductPriceHist) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_ProductPriceHist, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _ProductPriceHist.ProductID.ToString() & " " & _ProductPriceHist.CustomerType.FastToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _ProductPriceHist.ID 
    pFault = _ProductPriceHist.Delete(_Requester) 
    If pFault.isOK = True Then 
      _ProductPriceHist = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only

  'Now the Parents
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("ProductPriceHist", _ProductPriceHist.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _ProductPriceHist.DateAdded 
    pAuditIndexed.TableName = "ProductPriceHist" 
    pAuditIndexed.RowID = _ProductPriceHist.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Product Price Hist'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlccProductPriceHist_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the ProductPriceHist to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pProductPriceHist As clsProductPriceHist = _ProductPriceHist 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pProductPriceHist.ToCSV) 
        Else 
          Clipboard.SetText(pProductPriceHist.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The ProductPriceHist is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccProductPriceHist_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlProductPriceHist_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
