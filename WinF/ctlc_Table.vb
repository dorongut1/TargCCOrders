Public Class ctlc_Table
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vTable As csTable) 
  
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
 
  Private WithEvents _Table As csTable

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlTable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Function LoadControl(ByVal vTableID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pTable As New csTable() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vTableID <> 0 Then 
      pFault = pTable.GetByID(vTableID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pTable) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rTable As csTable, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rTable)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rTable As csTable) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _Table = rTable 

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
  ''' <param name="rTable"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rTable As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
    End With 
    
    If rTable.GetType.Name = "csTable" Then 
      ctlTable_Load(Nothing, Nothing) 
      Dim pTable As csTable = CType(rTable, csTable) 
      Return LoadControl(pTable) 
    Else 
      Dim pTableID As Long = CType(rTable, Long) 
      Return LoadControl(pTableID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "Name", _Requester) 
    If pStrg <> "" Then lblName.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "DefaultTextFields", _Requester) 
    If pStrg <> "" Then lblDefaultTextFields.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "UsedForIdentity", _Requester) 
    If pStrg <> "" Then lblUsedForIdentity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "IsSingleRow", _Requester) 
    If pStrg <> "" Then lblIsSingleRow.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "CanAdd", _Requester) 
    If pStrg <> "" Then lblCanAdd.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "CanEdit", _Requester) 
    If pStrg <> "" Then lblCanEdit.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "CanDelete", _Requester) 
    If pStrg <> "" Then lblCanDelete.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "AuditAdd", _Requester) 
    If pStrg <> "" Then lblAuditAdd.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "AuditEdit", _Requester) 
    If pStrg <> "" Then lblAuditEdit.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "AuditDelete", _Requester) 
    If pStrg <> "" Then lblAuditDelete.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "TrackRowChangers", _Requester) 
    If pStrg <> "" Then lblTrackRowChangers.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "CreateUIMenu", _Requester) 
    If pStrg <> "" Then lblCreateUIMenu.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "CreateUICollection", _Requester) 
    If pStrg <> "" Then lblCreateUICollection.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "CreateUIEntity", _Requester) 
    If pStrg <> "" Then lblCreateUIEntity.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Table", "SortOrder", _Requester) 
    If pStrg <> "" Then lblSortOrder.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [Table]() As csTable
    Get 
      Return _Table 
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
    txtName.ReadOnly = Not (vInEdit)
    txtName.BackColor = pDefaultColour 
    txtDefaultTextFields.ReadOnly = Not (vInEdit)
    txtDefaultTextFields.BackColor = pDefaultColour 
    chkUsedForIdentity.Enabled = True
    chkIsSingleRow.Enabled = True
    txtCanAdd.ReadOnly = Not (vInEdit)
    txtCanAdd.BackColor = pDefaultColour 
    txtCanEdit.ReadOnly = Not (vInEdit)
    txtCanEdit.BackColor = pDefaultColour 
    txtCanDelete.ReadOnly = Not (vInEdit)
    txtCanDelete.BackColor = pDefaultColour 
    chkAuditAdd.Enabled = True
    chkAuditEdit.Enabled = True
    chkAuditDelete.Enabled = True
    chkTrackRowChangers.Enabled = True
    chkCreateUIMenu.Enabled = True
    chkCreateUICollection.Enabled = True
    chkCreateUIEntity.Enabled = True
    txtSortOrder.ReadOnly = Not (vInEdit)
    txtSortOrder.BackColor = pDefaultColour 

    RaiseEvent evtControlsRefreshed(vInEdit, _Table) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _Table
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtName.Text = .Name.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtName.MaxLength = 50 
      txtDefaultTextFields.Text = .DefaultTextFields.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDefaultTextFields.MaxLength = 100 
      chkUsedForIdentity.Checked = .UsedForIdentity
      chkIsSingleRow.Checked = .IsSingleRow
      txtCanAdd.Text = .CanAdd.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCanAdd.MaxLength = 1 
      txtCanEdit.Text = .CanEdit.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCanEdit.MaxLength = 1 
      txtCanDelete.Text = .CanDelete.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCanDelete.MaxLength = 1 
      chkAuditAdd.Checked = .AuditAdd
      chkAuditEdit.Checked = .AuditEdit
      chkAuditDelete.Checked = .AuditDelete
      chkTrackRowChangers.Checked = .TrackRowChangers
      chkCreateUIMenu.Checked = .CreateUIMenu
      chkCreateUICollection.Checked = .CreateUICollection
      chkCreateUIEntity.Checked = .CreateUIEntity
      txtSortOrder.Text = .SortOrder.ToString(FormatFromTag(txtSortOrder, "#,##0"))
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-Table-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtSortOrder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSortOrder.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtSortOrder.Text 
    Dim pTest As Integer 
 
    If txtSortOrder.Text = "" Then Exit Sub 
    If txtSortOrder.Text = txtSortOrder.Name Then Exit Sub 
 
    If Integer.TryParse(txtSortOrder.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-Table-SortOrder-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Ensure Read-Only
  Private Sub chkUsedForIdentity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUsedForIdentity.CheckedChanged
    chkUsedForIdentity.Checked = _Table.UsedForIdentity
  End Sub
  Private Sub chkIsSingleRow_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsSingleRow.CheckedChanged
    chkIsSingleRow.Checked = _Table.IsSingleRow
  End Sub
  Private Sub chkAuditAdd_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAuditAdd.CheckedChanged
    chkAuditAdd.Checked = _Table.AuditAdd
  End Sub
  Private Sub chkAuditEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAuditEdit.CheckedChanged
    chkAuditEdit.Checked = _Table.AuditEdit
  End Sub
  Private Sub chkAuditDelete_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAuditDelete.CheckedChanged
    chkAuditDelete.Checked = _Table.AuditDelete
  End Sub
  Private Sub chkTrackRowChangers_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTrackRowChangers.CheckedChanged
    chkTrackRowChangers.Checked = _Table.TrackRowChangers
  End Sub
  Private Sub chkCreateUIMenu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCreateUIMenu.CheckedChanged
    chkCreateUIMenu.Checked = _Table.CreateUIMenu
  End Sub
  Private Sub chkCreateUICollection_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCreateUICollection.CheckedChanged
    chkCreateUICollection.Checked = _Table.CreateUICollection
  End Sub
  Private Sub chkCreateUIEntity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCreateUIEntity.CheckedChanged
    chkCreateUIEntity.Checked = _Table.CreateUIEntity
  End Sub

  'Now the Parents
  
 
  Private Sub ctlc_Table_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the Table to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pTable As csTable = _Table 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pTable.ToCSV) 
        Else 
          Clipboard.SetText(pTable.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The Table is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_Table_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlTable_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
