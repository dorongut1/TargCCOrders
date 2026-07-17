Public Class ctlccBeehiveBuyerTracking
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As clsBeehiveBuyerTracking.enmUpdateType) 
  Public Event evtAdd(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking) 
  Public Event evtBeforeUpdate(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As clsBeehiveBuyerTracking.enmUpdateType, ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking) 
  Public Event evtBeforeDelete(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vBeehiveBuyerTrackingID As Long) 
  Public Event evtCancelledEdit(ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vBeehiveBuyerTracking As clsBeehiveBuyerTracking) 
  
  Public Event evtParentChosen(ByVal vParentName As clsBeehiveBuyerTracking.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As clsBeehiveBuyerTracking.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsBeehiveBuyerTracking.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As clsBeehiveBuyerTracking.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of clsBeehiveBuyerTracking.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of clsBeehiveBuyerTracking.enmParentProperty) 
      _EnableParentLinks.Add(clsBeehiveBuyerTracking.enmParentProperty.Customer) 
 
    End Sub 
  End Class 
 
  Private WithEvents _BeehiveBuyerTracking As clsBeehiveBuyerTracking

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlBeehiveBuyerTracking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    dtpLastOrderDate.Size = txtLastOrderDate.Size
    dtpLastOrderDate.Location = txtLastOrderDate.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vBeehiveBuyerTrackingID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pBeehiveBuyerTracking As New clsBeehiveBuyerTracking(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vBeehiveBuyerTrackingID <> 0 Then 
      pFault = pBeehiveBuyerTracking.GetByID(vBeehiveBuyerTrackingID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pBeehiveBuyerTracking) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rBeehiveBuyerTracking As clsBeehiveBuyerTracking, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rBeehiveBuyerTracking)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rBeehiveBuyerTracking As clsBeehiveBuyerTracking) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _BeehiveBuyerTracking = rBeehiveBuyerTracking 

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
 
    'Combos
    'Set comboListsCache 
    MyCache.SetLevel(clsEnums.enmComboListType.ccCustomerDefaultByID, Cache.enmLevel.Previous) 
    
    'Lookup Combos
    'EnumCombos
    
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
  ''' <param name="rBeehiveBuyerTracking"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rBeehiveBuyerTracking As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rBeehiveBuyerTracking.GetType.Name = "clsBeehiveBuyerTracking" Then 
      ctlBeehiveBuyerTracking_Load(Nothing, Nothing) 
      Dim pBeehiveBuyerTracking As clsBeehiveBuyerTracking = CType(rBeehiveBuyerTracking, clsBeehiveBuyerTracking) 
      Return LoadControl(pBeehiveBuyerTracking) 
    Else 
      Dim pBeehiveBuyerTrackingID As Long = CType(rBeehiveBuyerTracking, Long) 
      Return LoadControl(pBeehiveBuyerTrackingID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("BeehiveBuyerTracking", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("BeehiveBuyerTracking", "Customer", _Requester) 
    If pStrg <> "" Then lblCustomer.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("BeehiveBuyerTracking", "LastOrderDate", _Requester) 
    If pStrg <> "" Then lblLastOrderDate.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("BeehiveBuyerTracking", "BeehiveQuantity", _Requester) 
    If pStrg <> "" Then lblBeehiveQuantity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("BeehiveBuyerTracking", "ReminderMonth", _Requester) 
    If pStrg <> "" Then lblReminderMonth.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("BeehiveBuyerTracking", "IsRelevant", _Requester) 
    If pStrg <> "" Then lblIsRelevant.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("BeehiveBuyerTracking", "Notes", _Requester) 
    If pStrg <> "" Then lblNotes.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [BeehiveBuyerTracking]() As clsBeehiveBuyerTracking
    Get 
      Return _BeehiveBuyerTracking 
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
    RaiseEvent evtOverrideLoadIntelliCombo(clsBeehiveBuyerTracking.enmParentProperty.Customer, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
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
    
    If _BeehiveBuyerTracking.CustomerID > 0 Then cboCustomer.ValueSelect(_BeehiveBuyerTracking.CustomerID) Else cboCustomer.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboCustomer_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboCustomer.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(clsBeehiveBuyerTracking.enmParentProperty.Customer, pUniqueCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As clsBeehiveBuyerTracking.enmParentProperty = clsBeehiveBuyerTracking.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsBeehiveBuyerTracking.enmParentProperty.Customer) = clsBeehiveBuyerTracking.enmParentProperty.Customer Then 
      lblCustomer.ForeColor = Color.Brown 
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
    dtpLastOrderDate.Visible = vInEdit
    txtLastOrderDate.Visible = Not (vInEdit)
    txtLastOrderDate.BackColor = pReadonlyColour 
    txtLastOrderDate.ForeColor = SetForeColor(vInEdit) 
    txtLastOrderDate.ReadOnly = True
    txtBeehiveQuantity.ReadOnly = Not (vInEdit)
    txtBeehiveQuantity.BackColor = pDefaultColour 
    txtReminderMonth.ReadOnly = Not (vInEdit)
    txtReminderMonth.BackColor = pDefaultColour 
    chkIsRelevant.Enabled = True 
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
      If _BeehiveBuyerTracking.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_BeehiveBuyerTrackingUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_BeehiveBuyerTrackingDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_BeehiveBuyerTrackingUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _BeehiveBuyerTracking) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _BeehiveBuyerTracking
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtCustomer.Text = .CustomerText 
      If .LastOrderDate < dtpLastOrderDate.MinDate Then dtpLastOrderDate.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpLastOrderDate.Value = .LastOrderDate.Date
      dtpLastOrderDate.CustomFormat = FormatFromTag(txtLastOrderDate, "dd-MM-yyyy") 
      dtpLastOrderDate.Value = DateTime.ParseExact(dtpLastOrderDate.Value.ToString(dtpLastOrderDate.CustomFormat), dtpLastOrderDate.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .LastOrderDate < dtpLastOrderDate.MinDate Then dtpLastOrderDate.Checked = False Else dtpLastOrderDate.Checked = True 
      If Math.Abs(.LastOrderDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.LastOrderDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtLastOrderDate.Text = "" Else txtLastOrderDate.Text = .LastOrderDate.ToString(FormatFromTag(txtLastOrderDate, "dd-MM-yyyy"))
      txtBeehiveQuantity.Text = .BeehiveQuantity.ToString(FormatFromTag(txtBeehiveQuantity, "#,##0"))
      txtReminderMonth.Text = .ReminderMonth.ToString(FormatFromTag(txtReminderMonth, "#,##0"))
      chkIsRelevant.Checked = .IsRelevant
      txtNotes.Text = .Notes.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _BeehiveBuyerTracking
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-BeehiveBuyerTracking-ID-090417-0012", _Requester) : Return pFault 
      If cboCustomer.SelectedItem Is Nothing OrElse cboCustomer.SelectedItem.KeyType = clsEnums.enmComboListKeyType.UD Then 
        .CustomerID = 0 
      Else 
        Dim pCustomerID As Long = CType(cboCustomer.SelectedItem, clsComboListMember).KeyLong 
        If pCustomerID = -1 Then .CustomerID = 0 Else .CustomerID = pCustomerID 
      End If 
      If (dtpLastOrderDate.ShowCheckBox AndAlso dtpLastOrderDate.Checked = False) OrElse dtpLastOrderDate.Value = New Date(1900, 1, 1, 0, 0, 0) Then .LastOrderDate = Nothing Else .LastOrderDate = dtpLastOrderDate.Value.Date
      If Integer.TryParse(txtBeehiveQuantity.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .BeehiveQuantity) = False Then pFault.LogFreeTextFault(208, ".BeehiveQuantity", txtBeehiveQuantity.Text, "TRGT-BeehiveBuyerTracking-BeehiveQuantity-090417-0013", _Requester) : Return pFault 
      If Integer.TryParse(txtReminderMonth.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ReminderMonth) = False Then pFault.LogFreeTextFault(208, ".ReminderMonth", txtReminderMonth.Text, "TRGT-BeehiveBuyerTracking-ReminderMonth-090417-0013", _Requester) : Return pFault 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-BeehiveBuyerTracking-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtBeehiveQuantity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBeehiveQuantity.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtBeehiveQuantity.Text 
    Dim pTest As Integer 
 
    If txtBeehiveQuantity.Text = "" Then Exit Sub 
    If txtBeehiveQuantity.Text = txtBeehiveQuantity.Name Then Exit Sub 
 
    If Integer.TryParse(txtBeehiveQuantity.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-BeehiveBuyerTracking-BeehiveQuantity-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtReminderMonth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReminderMonth.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtReminderMonth.Text 
    Dim pTest As Integer 
 
    If txtReminderMonth.Text = "" Then Exit Sub 
    If txtReminderMonth.Text = txtReminderMonth.Name Then Exit Sub 
 
    If Integer.TryParse(txtReminderMonth.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-BeehiveBuyerTracking-ReminderMonth-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(clsBeehiveBuyerTracking.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-BeehiveBuyerTracking-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_BeehiveBuyerTracking, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _BeehiveBuyerTracking.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the BeehiveBuyerTracking collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccBeehiveBuyerTrackingDefaultByID) 
      RaiseEvent evtUpdated(clsBeehiveBuyerTracking.enmUpdateType.Standard, _BeehiveBuyerTracking) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_BeehiveBuyerTracking_evtAfterUpdate 
  Private Sub _BeehiveBuyerTracking_evtAfterUpdate() Handles _BeehiveBuyerTracking.evtAfterUpdate, _BeehiveBuyerTracking.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_BeehiveBuyerTracking) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _BeehiveBuyerTracking = New clsBeehiveBuyerTracking(clsEnums.enmLoadParent.TextOnly) 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_BeehiveBuyerTracking) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_BeehiveBuyerTracking, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _BeehiveBuyerTracking.ID.ToString() & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _BeehiveBuyerTracking.ID 
    pFault = _BeehiveBuyerTracking.Delete(_Requester) 
    If pFault.isOK = True Then 
      _BeehiveBuyerTracking = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only
  Private Sub chkIsRelevant_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsRelevant.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkIsRelevant.Checked = _BeehiveBuyerTracking.IsRelevant
    End If
  End Sub

  'Now the Parents
  Private Sub lblCustomer_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.DoubleClick 
    If _BeehiveBuyerTracking.CustomerID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsBeehiveBuyerTracking.enmParentProperty.Customer) = clsBeehiveBuyerTracking.enmParentProperty.Customer Then 
      If _BeehiveBuyerTracking.CustomerID <> 0 Then RaiseEvent evtParentChosen(clsBeehiveBuyerTracking.enmParentProperty.Customer, _BeehiveBuyerTracking.CustomerID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "Customer Detail" 
      fPopup.LoadControl("ctlccCustomer", _BeehiveBuyerTracking.CustomerID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblCustomer_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsBeehiveBuyerTracking.enmParentProperty.Customer) <> clsBeehiveBuyerTracking.enmParentProperty.Customer Then Exit Sub 
    lblCustomer.ForeColor = Color.Brown 
    'lblCustomer.Font = New Font(lblCustomer.Font.Name, lblCustomer.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblCustomer.BackColor = ccHelper.InvertColour(lblCustomer.ForeColor) 'did this instead 
    lblCustomer.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblCustomer_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCustomer.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = clsBeehiveBuyerTracking.enmParentProperty.Customer) <> clsBeehiveBuyerTracking.enmParentProperty.Customer Then Exit Sub 
    lblCustomer.ForeColor = Color.Brown 
    'lblCustomer.Font = New Font(lblCustomer.Font.Name, lblCustomer.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblCustomer.BackColor = Me.BackColor 'did this instead 
    lblCustomer.Cursor = Cursors.Default 
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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("BeehiveBuyerTracking", _BeehiveBuyerTracking.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _BeehiveBuyerTracking.DateAdded 
    pAuditIndexed.TableName = "BeehiveBuyerTracking" 
    pAuditIndexed.RowID = _BeehiveBuyerTracking.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Beehive Buyer Tracking'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlccBeehiveBuyerTracking_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the BeehiveBuyerTracking to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pBeehiveBuyerTracking As clsBeehiveBuyerTracking = _BeehiveBuyerTracking 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pBeehiveBuyerTracking.ToCSV) 
        Else 
          Clipboard.SetText(pBeehiveBuyerTracking.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The BeehiveBuyerTracking is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlccBeehiveBuyerTracking_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlBeehiveBuyerTracking_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
