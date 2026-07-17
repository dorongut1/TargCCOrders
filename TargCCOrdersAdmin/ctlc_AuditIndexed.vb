Public Class ctlc_AuditIndexed
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vAuditIndexed As csAuditIndexed) 
  
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
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
 
    End Sub 
  End Class 
 
  Private WithEvents _AuditIndexed As csAuditIndexed

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlAuditIndexed_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Function LoadControl(ByVal vAuditIndexedID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pAuditIndexed As New csAuditIndexed() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vAuditIndexedID <> 0 Then 
      pFault = pAuditIndexed.GetByID(vAuditIndexedID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pAuditIndexed) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rAuditIndexed As csAuditIndexed, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rAuditIndexed)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rAuditIndexed As csAuditIndexed) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _AuditIndexed = rAuditIndexed 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    'Combos
    'Set comboListsCache 
    
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
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rAuditIndexed"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rAuditIndexed As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
    End With 
    
    If rAuditIndexed.GetType.Name = "csAuditIndexed" Then 
      ctlAuditIndexed_Load(Nothing, Nothing) 
      Dim pAuditIndexed As csAuditIndexed = CType(rAuditIndexed, csAuditIndexed) 
      Return LoadControl(pAuditIndexed) 
    Else 
      Dim pAuditIndexedID As Long = CType(rAuditIndexed, Long) 
      Return LoadControl(pAuditIndexedID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "OriginalID", _Requester) 
    If pStrg <> "" Then lblOriginalID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "TableName", _Requester) 
    If pStrg <> "" Then lblTableName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "RowID", _Requester) 
    If pStrg <> "" Then lblRowID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "Operation", _Requester) 
    If pStrg <> "" Then lblOperation.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "OccurredAt", _Requester) 
    If pStrg <> "" Then lblOccurredAt.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "SqlCurrentUser", _Requester) 
    If pStrg <> "" Then lblSqlCurrentUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "FieldName", _Requester) 
    If pStrg <> "" Then lblFieldName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "OldValue", _Requester) 
    If pStrg <> "" Then lblOldValue.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "NewValue", _Requester) 
    If pStrg <> "" Then lblNewValue.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "ChangedByUser", _Requester) 
    If pStrg <> "" Then lblChangedByUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "ActiveLoginID", _Requester) 
    If pStrg <> "" Then lblActiveLoginID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "SqlSystemUser", _Requester) 
    If pStrg <> "" Then lblSqlSystemUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "SqlAppName", _Requester) 
    If pStrg <> "" Then lblSqlAppName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_AuditIndexed", "SqlHostName", _Requester) 
    If pStrg <> "" Then lblSqlHostName.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [AuditIndexed]() As csAuditIndexed
    Get 
      Return _AuditIndexed 
    End Get 
  End Property 
 
  'Load comboboxes
  
  'Handle Comboboxes
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    
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
    txtOriginalID.ReadOnly = Not (vInEdit)
    txtOriginalID.BackColor = pDefaultColour 
    txtTableName.ReadOnly = Not (vInEdit)
    txtTableName.BackColor = pDefaultColour 
    txtRowID.ReadOnly = Not (vInEdit)
    txtRowID.BackColor = pDefaultColour 
    txtOperation.ReadOnly = Not (vInEdit)
    txtOperation.BackColor = pDefaultColour 
    txtOccurredAt.Visible = True 
    txtOccurredAt.BackColor = pReadonlyColour 
    txtOccurredAt.ReadOnly = True
    txtOccurredAt.ForeColor = SetForeColor(vInEdit) 
    txtSqlCurrentUser.ReadOnly = Not (vInEdit)
    txtSqlCurrentUser.BackColor = pDefaultColour 
    txtFieldName.ReadOnly = Not (vInEdit)
    txtFieldName.BackColor = pDefaultColour 
    txtOldValue.ReadOnly = Not (vInEdit)
    txtOldValue.BackColor = pDefaultColour 
    txtNewValue.ReadOnly = Not (vInEdit)
    txtNewValue.BackColor = pDefaultColour 
    txtChangedByUser.ReadOnly = Not (vInEdit)
    txtChangedByUser.BackColor = pDefaultColour 
    txtActiveLoginID.ReadOnly = Not (vInEdit)
    txtActiveLoginID.BackColor = pDefaultColour 
    txtSqlSystemUser.ReadOnly = Not (vInEdit)
    txtSqlSystemUser.BackColor = pDefaultColour 
    txtSqlAppName.ReadOnly = Not (vInEdit)
    txtSqlAppName.BackColor = pDefaultColour 
    txtSqlHostName.ReadOnly = Not (vInEdit)
    txtSqlHostName.BackColor = pDefaultColour 

    RaiseEvent evtControlsRefreshed(vInEdit, _AuditIndexed) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _AuditIndexed
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtOriginalID.Text = .OriginalID.ToString(FormatFromTag(txtOriginalID, "#,##0"))
      txtTableName.Text = .TableName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtTableName.MaxLength = 50 
      txtRowID.Text = .RowID.ToString(FormatFromTag(txtRowID, "#,##0"))
      txtOperation.Text = .Operation.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtOperation.MaxLength = 10 
      If Math.Abs(.OccurredAt.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.OccurredAt.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtOccurredAt.Text = "" Else txtOccurredAt.Text = .OccurredAt.ToString(FormatFromTag(txtOccurredAt, "dd-MM-yyyy HH:mm:ss"))
      txtSqlCurrentUser.Text = .SqlCurrentUser.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSqlCurrentUser.MaxLength = 50 
      txtFieldName.Text = .FieldName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtFieldName.MaxLength = 100 
      txtOldValue.Text = .OldValue.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtOldValue.MaxLength = 1000 
      txtNewValue.Text = .NewValue.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtNewValue.MaxLength = 1000 
      txtChangedByUser.Text = .ChangedByUser.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtChangedByUser.MaxLength = 50 
      txtActiveLoginID.Text = .ActiveLoginID.ToString(FormatFromTag(txtActiveLoginID, "#,##0"))
      txtSqlSystemUser.Text = .SqlSystemUser.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSqlSystemUser.MaxLength = 50 
      txtSqlAppName.Text = .SqlAppName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSqlAppName.MaxLength = 250 
      txtSqlHostName.Text = .SqlHostName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtSqlHostName.MaxLength = 50 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-AuditIndexed-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtOriginalID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOriginalID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtOriginalID.Text 
    Dim pTest As Long 
 
    If txtOriginalID.Text = "" Then Exit Sub 
    If txtOriginalID.Text = txtOriginalID.Name Then Exit Sub 
 
    If Long.TryParse(txtOriginalID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-AuditIndexed-OriginalID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtRowID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRowID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtRowID.Text 
    Dim pTest As Long 
 
    If txtRowID.Text = "" Then Exit Sub 
    If txtRowID.Text = txtRowID.Name Then Exit Sub 
 
    If Long.TryParse(txtRowID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-AuditIndexed-RowID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtActiveLoginID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtActiveLoginID.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtActiveLoginID.Text 
    Dim pTest As Long 
 
    If txtActiveLoginID.Text = "" Then Exit Sub 
    If txtActiveLoginID.Text = txtActiveLoginID.Name Then Exit Sub 
 
    If Long.TryParse(txtActiveLoginID.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-AuditIndexed-ActiveLoginID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Ensure Read-Only

  'Now the Parents
  
 
  Private Sub ctlc_AuditIndexed_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the AuditIndexed to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pAuditIndexed As csAuditIndexed = _AuditIndexed 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pAuditIndexed.ToCSV) 
        Else 
          Clipboard.SetText(pAuditIndexed.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The AuditIndexed is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_AuditIndexed_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlAuditIndexed_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
