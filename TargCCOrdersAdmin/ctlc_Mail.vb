Public Class ctlc_Mail
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csMail.enmUpdateType) 
  Public Event evtBeforeUpdate(ByVal vMail As csMail, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csMail.enmUpdateType, ByVal vMail As csMail) 
  Public Event evtBeforeDelete(ByVal vMail As csMail, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vMailID As Long) 
  Public Event evtCancelledEdit(ByVal vMail As csMail) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vMail As csMail) 
  
  Public Event evtParentChosen(ByVal vParentName As csMail.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csMail.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csMail.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csMail.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csMail.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csMail.enmParentProperty) 
 
    End Sub 
  End Class 
 
  Private WithEvents _Mail As csMail

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlMail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.DoubleBuffered = True 
    If Me.DesignMode = True Then Exit Sub
    
    'buttons
    'btnUpdate.Location = btnEdit.Location
    'btnCancel.Location = New Point(btnCancel.Location.X, btnEdit.Location.Y)
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
    cboMessagingMode.Size = txtMessagingMode.Size
    cboMessagingMode.Location = txtMessagingMode.Location
    dtpWhenSent.Size = txtWhenSent.Size
    dtpWhenSent.Location = txtWhenSent.Location
    dtpWhenSeen.Size = txtWhenSeen.Size
    dtpWhenSeen.Location = txtWhenSeen.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vMailID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pMail As New csMail() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vMailID <> 0 Then 
      pFault = pMail.GetByID(vMailID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pMail) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rMail As csMail, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rMail)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rMail As csMail) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _Mail = rMail 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboMessagingMode.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      'EnumCombos
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
 
    'Parents
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rMail"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rMail As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rMail.GetType.Name = "csMail" Then 
      ctlMail_Load(Nothing, Nothing) 
      Dim pMail As csMail = CType(rMail, csMail) 
      Return LoadControl(pMail) 
    Else 
      Dim pMailID As Long = CType(rMail, Long) 
      Return LoadControl(pMailID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Mail", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Mail", "MessagingMode", _Requester) 
    If pStrg <> "" Then lblMessagingMode.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Mail", "RecipientEmail", _Requester) 
    If pStrg <> "" Then lblRecipientEmail.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Mail", "WhenSent", _Requester) 
    If pStrg <> "" Then lblWhenSent.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Mail", "Subject", _Requester) 
    If pStrg <> "" Then lblSubject.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Mail", "Body", _Requester) 
    If pStrg <> "" Then lblBody.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Mail", "WhenSeen", _Requester) 
    If pStrg <> "" Then lblWhenSeen.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Mail", "WasSeen", _Requester) 
    If pStrg <> "" Then lblWasSeen.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [Mail]() As csMail
    Get 
      Return _Mail 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboMessagingMode() As clsFault
    Dim pFault As New clsFault
 
    'If cboMessagingMode.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pMessagingModees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csMail.enmParentProperty.MessagingMode, pTestCol, pPrompt) 
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

    cboMessagingMode.SelectedValue = _Mail.MessagingMode 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboMessagingMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMessagingMode.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmMessagingMode = CType(cboMessagingMode.SelectedValue, clsEnums.enmMessagingMode) 
    RaiseEvent evtCboSelectedIndexChanged(csMail.enmParentProperty.MessagingMode, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csMail.enmParentProperty = csMail.enmParentProperty.UD 
    
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
    txtMessagingMode.ReadOnly = True
    txtMessagingMode.Visible = Not (vInEdit)
    txtMessagingMode.BackColor = pReadonlyColour 
    txtMessagingMode.ForeColor = SetForeColor(vInEdit) 
    cboMessagingMode.Visible = vInEdit
    txtRecipientEmail.ReadOnly = Not (vInEdit)
    txtRecipientEmail.BackColor = pDefaultColour 
    dtpWhenSent.Visible = vInEdit
    txtWhenSent.Visible = Not (vInEdit)
    txtWhenSent.BackColor = pReadonlyColour 
    txtWhenSent.ForeColor = SetForeColor(vInEdit) 
    txtWhenSent.ReadOnly = True
    txtSubject.ReadOnly = Not (vInEdit)
    txtSubject.BackColor = pDefaultColour 
    txtBody.ReadOnly = Not (vInEdit)
    txtBody.BackColor = pDefaultColour 
    dtpWhenSeen.Visible = vInEdit
    txtWhenSeen.Visible = Not (vInEdit)
    txtWhenSeen.BackColor = pReadonlyColour 
    txtWhenSeen.ForeColor = SetForeColor(vInEdit) 
    txtWhenSeen.ReadOnly = True
    chkWasSeen.Enabled = True

    If _LoadParameters.ReadOnly = False Then 
      If _ButtonsMoved = False Then 
        btnUpdate.Visible = True 
        btnCancel.Visible = True 
        btnEdit.Visible = True 
        btnDelete.Visible = True 
        btnDelete.Top = btnEdit.Top 
        _ButtonsMoved = True 
      End If 
      btnUpdate.Visible = vInEdit 
      btnCancel.Visible = vInEdit 
      btnUpdate.Top = btnEdit.Top 
      btnCancel.Top = btnEdit.Top 
      If _Mail.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_MailUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_MailDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
    Else 
      btnUpdate.Visible = False 
      btnCancel.Visible = False 
      btnEdit.Visible = False 
      btnDelete.Visible = False 
    End If 
    
    'disable or enable any child grids 
    Dim ctrl As Control = Me.GetNextControl(Me, True) 
    Do Until ctrl Is Nothing 
      If ctrl.GetType.Name.StartsWith("ctl") AndAlso ctrl.GetType.Name.EndsWith("Col") Then 
        ctrl.Enabled = Not vInEdit 
      End If 
      ctrl = Me.GetNextControl(ctrl, True) 
    Loop 
 
    RaiseEvent evtControlsRefreshed(vInEdit, _Mail) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _Mail
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      cboMessagingMode.SelectedValue = .MessagingMode
      txtMessagingMode.Text = cboMessagingMode.Text : If cboMessagingMode.SelectedValue Is Nothing OrElse cboMessagingMode.SelectedValue.ToString() = "UD" Then txtMessagingMode.Text = ""    
      txtRecipientEmail.Text = .RecipientEmail.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtRecipientEmail.MaxLength = 50 
      If .WhenSent < dtpWhenSent.MinDate Then dtpWhenSent.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpWhenSent.Value = .WhenSent.LocalDateTime
      dtpWhenSent.CustomFormat = FormatFromTag(txtWhenSent, "dd-MM-yyyy HH:mm:ss") 
      dtpWhenSent.Value = DateTime.ParseExact(dtpWhenSent.Value.ToString(dtpWhenSent.CustomFormat), dtpWhenSent.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .WhenSent < dtpWhenSent.MinDate Then dtpWhenSent.Checked = False Else dtpWhenSent.Checked = True 
      txtWhenSent.Text = FormattedDateTimeOffsetFromTag(txtWhenSent, .WhenSent) 
      txtSubject.Text = .Subject.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSubject.MaxLength = 50 
      txtBody.Text = .Body.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      If .WhenSeen < dtpWhenSeen.MinDate Then dtpWhenSeen.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpWhenSeen.Value = .WhenSeen.LocalDateTime
      dtpWhenSeen.CustomFormat = FormatFromTag(txtWhenSeen, "dd-MM-yyyy HH:mm:ss") 
      dtpWhenSeen.Value = DateTime.ParseExact(dtpWhenSeen.Value.ToString(dtpWhenSeen.CustomFormat), dtpWhenSeen.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .WhenSeen < dtpWhenSeen.MinDate Then dtpWhenSeen.Checked = False Else dtpWhenSeen.Checked = True 
      txtWhenSeen.Text = FormattedDateTimeOffsetFromTag(txtWhenSeen, .WhenSeen) 
      chkWasSeen.Checked = .WasSeen
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _Mail
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-Mail-ID-090417-0012", _Requester) : Return pFault 
      .MessagingMode = CType(cboMessagingMode.SelectedValue, clsEnums.enmMessagingMode)
      .RecipientEmail = txtRecipientEmail.Text 
      If (dtpWhenSent.ShowCheckBox AndAlso dtpWhenSent.Checked = False) OrElse dtpWhenSent.Value = New Date(1900, 1, 1, 0, 0, 0) Then .WhenSent = Nothing Else .WhenSent = dtpWhenSent.Value
      .Subject = txtSubject.Text 
      .Body = txtBody.Text 
      If (dtpWhenSeen.ShowCheckBox AndAlso dtpWhenSeen.Checked = False) OrElse dtpWhenSeen.Value = New Date(1900, 1, 1, 0, 0, 0) Then .WhenSeen = Nothing Else .WhenSeen = dtpWhenSeen.Value
      .WasSeen = chkWasSeen.Checked
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-Mail-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csMail.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-Mail-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_Mail, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _Mail.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      RaiseEvent evtUpdated(csMail.enmUpdateType.Standard, _Mail) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_Mail_evtAfterUpdate 
  Private Sub _Mail_evtAfterUpdate() Handles _Mail.evtAfterUpdate, _Mail.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_Mail) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_Mail, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete the row with an ID of '" & _Mail.ID.ToString & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _Mail.ID 
    pFault = _Mail.Delete(_Requester) 
    If pFault.isOK = True Then 
      _Mail = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only
  Private Sub chkWasSeen_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkWasSeen.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkWasSeen.Checked = _Mail.WasSeen
    End If
  End Sub

  'Now the Parents
  'SeparateUpdates 
  
  'Uploads
  
  'PictureBox MouseHandlers 
  
 
  Private Sub ctlc_Mail_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the Mail to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pMail As csMail = _Mail 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pMail.ToCSV) 
        Else 
          Clipboard.SetText(pMail.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The Mail is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_Mail_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
 
  'Mail Specific 
  Private _LoggedAlertID As Long 
 
  Public Event evtLoggedAlertChosen(ByVal vLoggedAlertID As Long) 
 
  Private Sub ctlccMail_evtLoaded() Handles Me.evtLoaded 
    'see if we have to load the view button 
    _LoggedAlertID = 0 
    btnView.Visible = False 
 
    Dim pLocation As Integer = _Mail.Body.IndexOf("Alert ID:") 
    If pLocation > 0 Then 
      Dim pString As String = _Mail.Body.Substring(pLocation) 
      Dim psID As String = pString.Split(ChrW(13))(0) 
      pLocation = psID.IndexOf(":") 
      If pLocation > 0 Then 
        psID = psID.Split(":"c)(1) 
      End If 
      Dim pID As Long = 0 
      Long.TryParse(psID, pID) 
      If pID > 0 Then 
        _LoggedAlertID = pID 
        btnView.Text = $"View Alert ID {_LoggedAlertID}" 
        btnView.Visible = True 
      End If 
    End If 
 
  End Sub 
 
  Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click 
    RaiseEvent evtLoggedAlertChosen(_LoggedAlertID) 
  End Sub 
 
  Private Sub ctlccMail_evtBeforeLoad() Handles Me.evtBeforeLoad 
    'sample   
    _LoadParameters.ReadOnly = False 
  End Sub 
 

  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlMail_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
