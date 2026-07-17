Public Class ctlc_TableSize
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vTableSize As csTableSize) 
  
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
 
  Private WithEvents _TableSize As csTableSize

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlTableSize_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Function LoadControl(ByVal vTableSizeID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pTableSize As New csTableSize() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vTableSizeID <> -1 Then 
      pFault = pTableSize.GetByID(vTableSizeID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pTableSize) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rTableSize As csTableSize, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rTableSize)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rTableSize As csTableSize) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _TableSize = rTableSize 

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
  ''' <param name="rTableSize"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rTableSize As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
    End With 
    
    If rTableSize.GetType.Name = "csTableSize" Then 
      ctlTableSize_Load(Nothing, Nothing) 
      Dim pTableSize As csTableSize = CType(rTableSize, csTableSize) 
      Return LoadControl(pTableSize) 
    Else 
      Dim pTableSizeID As Long = CType(rTableSize, Long) 
      Return LoadControl(pTableSizeID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_TableSize", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_TableSize", "TableName", _Requester) 
    If pStrg <> "" Then lblTableName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_TableSize", "NumberOfRows", _Requester) 
    If pStrg <> "" Then lblNumberOfRows.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_TableSize", "ReservedSizeKb", _Requester) 
    If pStrg <> "" Then lblReservedSizeKb.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_TableSize", "DataSizeKb", _Requester) 
    If pStrg <> "" Then lblDataSizeKb.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_TableSize", "IndexSizeKb", _Requester) 
    If pStrg <> "" Then lblIndexSizeKb.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_TableSize", "UnusedSizeKb", _Requester) 
    If pStrg <> "" Then lblUnusedSizeKb.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [TableSize]() As csTableSize
    Get 
      Return _TableSize 
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
    If _TableSize.ID = -1 Then 
      txtID.ReadOnly = Not (vInEdit) 
      txtID.BackColor = pDefaultColour 
    Else 
      txtID.ReadOnly = True 
      txtID.BackColor = pReadonlyColour 
      txtID.ForeColor = SetForeColor(vInEdit) 
    End If 
    txtTableName.ReadOnly = Not (vInEdit)
    txtTableName.BackColor = pDefaultColour 
    txtNumberOfRows.ReadOnly = Not (vInEdit)
    txtNumberOfRows.BackColor = pDefaultColour 
    txtReservedSizeKb.ReadOnly = Not (vInEdit)
    txtReservedSizeKb.BackColor = pDefaultColour 
    txtDataSizeKb.ReadOnly = Not (vInEdit)
    txtDataSizeKb.BackColor = pDefaultColour 
    txtIndexSizeKb.ReadOnly = Not (vInEdit)
    txtIndexSizeKb.BackColor = pDefaultColour 
    txtUnusedSizeKb.ReadOnly = Not (vInEdit)
    txtUnusedSizeKb.BackColor = pDefaultColour 

    RaiseEvent evtControlsRefreshed(vInEdit, _TableSize) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _TableSize
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtTableName.Text = .TableName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtTableName.MaxLength = 200 
      txtNumberOfRows.Text = .NumberOfRows.ToString(FormatFromTag(txtNumberOfRows, "#,##0"))
      txtReservedSizeKb.Text = .ReservedSizeKb.ToString(FormatFromTag(txtReservedSizeKb, "#,##0"))
      txtDataSizeKb.Text = .DataSizeKb.ToString(FormatFromTag(txtDataSizeKb, "#,##0"))
      txtIndexSizeKb.Text = .IndexSizeKb.ToString(FormatFromTag(txtIndexSizeKb, "#,##0"))
      txtUnusedSizeKb.Text = .UnusedSizeKb.ToString(FormatFromTag(txtUnusedSizeKb, "#,##0"))
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-TableSize-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtNumberOfRows_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumberOfRows.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtNumberOfRows.Text 
    Dim pTest As Integer 
 
    If txtNumberOfRows.Text = "" Then Exit Sub 
    If txtNumberOfRows.Text = txtNumberOfRows.Name Then Exit Sub 
 
    If Integer.TryParse(txtNumberOfRows.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-TableSize-NumberOfRows-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtReservedSizeKb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReservedSizeKb.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtReservedSizeKb.Text 
    Dim pTest As Integer 
 
    If txtReservedSizeKb.Text = "" Then Exit Sub 
    If txtReservedSizeKb.Text = txtReservedSizeKb.Name Then Exit Sub 
 
    If Integer.TryParse(txtReservedSizeKb.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-TableSize-ReservedSizeKb-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtDataSizeKb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDataSizeKb.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtDataSizeKb.Text 
    Dim pTest As Integer 
 
    If txtDataSizeKb.Text = "" Then Exit Sub 
    If txtDataSizeKb.Text = txtDataSizeKb.Name Then Exit Sub 
 
    If Integer.TryParse(txtDataSizeKb.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-TableSize-DataSizeKb-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtIndexSizeKb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIndexSizeKb.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtIndexSizeKb.Text 
    Dim pTest As Integer 
 
    If txtIndexSizeKb.Text = "" Then Exit Sub 
    If txtIndexSizeKb.Text = txtIndexSizeKb.Name Then Exit Sub 
 
    If Integer.TryParse(txtIndexSizeKb.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-TableSize-IndexSizeKb-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtUnusedSizeKb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnusedSizeKb.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtUnusedSizeKb.Text 
    Dim pTest As Integer 
 
    If txtUnusedSizeKb.Text = "" Then Exit Sub 
    If txtUnusedSizeKb.Text = txtUnusedSizeKb.Name Then Exit Sub 
 
    If Integer.TryParse(txtUnusedSizeKb.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-TableSize-UnusedSizeKb-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Ensure Read-Only

  'Now the Parents
  
 
  Private Sub ctlc_TableSize_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the TableSize to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pTableSize As csTableSize = _TableSize 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pTableSize.ToCSV) 
        Else 
          Clipboard.SetText(pTableSize.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The TableSize is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_TableSize_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlTableSize_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
