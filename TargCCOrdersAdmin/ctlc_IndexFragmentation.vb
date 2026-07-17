Public Class ctlc_IndexFragmentation
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vIndexFragmentation As csIndexFragmentation) 
  
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
 
  Private WithEvents _IndexFragmentation As csIndexFragmentation

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlIndexFragmentation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Function LoadControl(ByVal vIndexFragmentationID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pIndexFragmentation As New csIndexFragmentation() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vIndexFragmentationID <> -1 Then 
      pFault = pIndexFragmentation.GetByID(vIndexFragmentationID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pIndexFragmentation) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rIndexFragmentation As csIndexFragmentation, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rIndexFragmentation)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rIndexFragmentation As csIndexFragmentation) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _IndexFragmentation = rIndexFragmentation 

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
  ''' <param name="rIndexFragmentation"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rIndexFragmentation As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
    End With 
    
    If rIndexFragmentation.GetType.Name = "csIndexFragmentation" Then 
      ctlIndexFragmentation_Load(Nothing, Nothing) 
      Dim pIndexFragmentation As csIndexFragmentation = CType(rIndexFragmentation, csIndexFragmentation) 
      Return LoadControl(pIndexFragmentation) 
    Else 
      Dim pIndexFragmentationID As Long = CType(rIndexFragmentation, Long) 
      Return LoadControl(pIndexFragmentationID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_IndexFragmentation", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_IndexFragmentation", "TableName", _Requester) 
    If pStrg <> "" Then lblTableName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_IndexFragmentation", "IndexName", _Requester) 
    If pStrg <> "" Then lblIndexName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_IndexFragmentation", "IndexType", _Requester) 
    If pStrg <> "" Then lblIndexType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_IndexFragmentation", "FragmentationPct", _Requester) 
    If pStrg <> "" Then lblFragmentationPct.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_IndexFragmentation", "PageCount", _Requester) 
    If pStrg <> "" Then lblPageCount.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [IndexFragmentation]() As csIndexFragmentation
    Get 
      Return _IndexFragmentation 
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
    If _IndexFragmentation.ID = -1 Then 
      txtID.ReadOnly = Not (vInEdit) 
      txtID.BackColor = pDefaultColour 
    Else 
      txtID.ReadOnly = True 
      txtID.BackColor = pReadonlyColour 
      txtID.ForeColor = SetForeColor(vInEdit) 
    End If 
    txtTableName.ReadOnly = Not (vInEdit)
    txtTableName.BackColor = pDefaultColour 
    txtIndexName.ReadOnly = Not (vInEdit)
    txtIndexName.BackColor = pDefaultColour 
    txtIndexType.ReadOnly = Not (vInEdit)
    txtIndexType.BackColor = pDefaultColour 
    txtFragmentationPct.ReadOnly = Not (vInEdit)
    txtFragmentationPct.BackColor = pDefaultColour 
    txtPageCount.ReadOnly = Not (vInEdit)
    txtPageCount.BackColor = pDefaultColour 

    RaiseEvent evtControlsRefreshed(vInEdit, _IndexFragmentation) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _IndexFragmentation
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtTableName.Text = .TableName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtTableName.MaxLength = 200 
      txtIndexName.Text = .IndexName.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtIndexName.MaxLength = 255 
      txtIndexType.Text = .IndexType.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtIndexType.MaxLength = 50 
      txtFragmentationPct.Text = .FragmentationPct.ToString(FormatFromTag(txtFragmentationPct, "#,##0.00"))
      txtPageCount.Text = .PageCount.ToString(FormatFromTag(txtPageCount, "#,##0"))
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-IndexFragmentation-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtFragmentationPct_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFragmentationPct.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtFragmentationPct.Text 
    Dim pTest As Decimal 
 
    If txtFragmentationPct.Text = "" Then Exit Sub 
    If txtFragmentationPct.Text = txtFragmentationPct.Name Then Exit Sub 
 
    If Decimal.TryParse(txtFragmentationPct.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(214, "", pFunctionParameters, "TRGT-IndexFragmentation-FragmentationPct-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtPageCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPageCount.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtPageCount.Text 
    Dim pTest As Integer 
 
    If txtPageCount.Text = "" Then Exit Sub 
    If txtPageCount.Text = txtPageCount.Name Then Exit Sub 
 
    If Integer.TryParse(txtPageCount.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-IndexFragmentation-PageCount-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Ensure Read-Only

  'Now the Parents
  
 
  Private Sub ctlc_IndexFragmentation_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the IndexFragmentation to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pIndexFragmentation As csIndexFragmentation = _IndexFragmentation 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pIndexFragmentation.ToCSV) 
        Else 
          Clipboard.SetText(pIndexFragmentation.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The IndexFragmentation is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_IndexFragmentation_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlIndexFragmentation_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
