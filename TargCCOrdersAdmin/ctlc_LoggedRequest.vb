Public Class ctlc_LoggedRequest
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vLoggedRequest As csLoggedRequest) 
  
  Public Event evtParentChosen(ByVal vParentName As csLoggedRequest.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csLoggedRequest.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csLoggedRequest.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csLoggedRequest.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csLoggedRequest.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csLoggedRequest.enmParentProperty) 
      _EnableParentLinks.Add(csLoggedRequest.enmParentProperty.LoggedLogin) 
      _EnableParentLinks.Add(csLoggedRequest.enmParentProperty.User) 
 
    End Sub 
  End Class 
 
  Private WithEvents _LoggedRequest As csLoggedRequest

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlLoggedRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Function LoadControl(ByVal vLoggedRequestID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pLoggedRequest As New csLoggedRequest(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vLoggedRequestID <> 0 Then 
      pFault = pLoggedRequest.GetByID(vLoggedRequestID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pLoggedRequest) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rLoggedRequest As csLoggedRequest, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rLoggedRequest)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rLoggedRequest As csLoggedRequest) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _LoggedRequest = rLoggedRequest 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    'Combos
    'Set comboListsCache 
    MyCache.SetLevel(clsEnums.enmComboListType.c_LoggedLoginDefaultByID, Cache.enmLevel.Previous) 
    MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.Previous) 
    
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
    pFault = LoadCboLoggedLogin() : If pFault.isOK = False Then Return pFault 
    pFault = LoadCboUser() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rLoggedRequest"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rLoggedRequest As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rLoggedRequest.GetType.Name = "csLoggedRequest" Then 
      ctlLoggedRequest_Load(Nothing, Nothing) 
      Dim pLoggedRequest As csLoggedRequest = CType(rLoggedRequest, csLoggedRequest) 
      Return LoadControl(pLoggedRequest) 
    Else 
      Dim pLoggedRequestID As Long = CType(rLoggedRequest, Long) 
      Return LoadControl(pLoggedRequestID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedRequest", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedRequest", "LoggedLogin", _Requester) 
    If pStrg <> "" Then lblLoggedLogin.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedRequest", "TimeAccessed", _Requester) 
    If pStrg <> "" Then lblTimeAccessed.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedRequest", "User", _Requester) 
    If pStrg <> "" Then lblUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedRequest", "CallingFunctionWithinApplication", _Requester) 
    If pStrg <> "" Then lblCallingFunctionWithinApplication.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedRequest", "EntryPoint", _Requester) 
    If pStrg <> "" Then lblEntryPoint.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedRequest", "Process", _Requester) 
    If pStrg <> "" Then lblProcess.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedRequest", "Thread", _Requester) 
    If pStrg <> "" Then lblThread.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [LoggedRequest]() As csLoggedRequest
    Get 
      Return _LoggedRequest 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboLoggedLogin() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_LoggedLoginDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csLoggedRequest.enmParentProperty.LoggedLogin, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboLoggedLogin.MakeSmart() Else cboLoggedLogin.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboLoggedLogin.LoadControl(pComboList, pPrompt) 
    Else 
      cboLoggedLogin.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _LoggedRequest.LoggedLoginID > 0 Then cboLoggedLogin.ValueSelect(_LoggedRequest.LoggedLoginID) Else cboLoggedLogin.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboUser() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_UserDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csLoggedRequest.enmParentProperty.User, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboUser.MakeSmart() Else cboUser.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboUser.LoadControl(pComboList, pPrompt) 
    Else 
      cboUser.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _LoggedRequest.UserID > 0 Then cboUser.ValueSelect(_LoggedRequest.UserID) Else cboUser.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboLoggedLogin_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboLoggedLogin.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csLoggedRequest.enmParentProperty.LoggedLogin, pUniqueCode) 
  End Sub 
  Private Sub cboUser_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboUser.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csLoggedRequest.enmParentProperty.User, pUniqueCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csLoggedRequest.enmParentProperty = csLoggedRequest.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedRequest.enmParentProperty.LoggedLogin) = csLoggedRequest.enmParentProperty.LoggedLogin Then 
      lblLoggedLogin.ForeColor = Color.Brown 
    End If 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedRequest.enmParentProperty.User) = csLoggedRequest.enmParentProperty.User Then 
      lblUser.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtLoggedLogin.ReadOnly = True
      txtLoggedLogin.Visible = True
      txtLoggedLogin.BackColor = pReadonlyColour
      txtLoggedLogin.ForeColor = SetForeColor(vInEdit) 
      cboLoggedLogin.Visible = False 
    Else 
      txtLoggedLogin.ReadOnly = True
      txtLoggedLogin.Visible = Not (vInEdit)
      txtLoggedLogin.BackColor = pReadonlyColour 
      txtLoggedLogin.ForeColor = SetForeColor(vInEdit) 
      cboLoggedLogin.Visible = vInEdit
    End If  
    txtTimeAccessed.Visible = True 
    txtTimeAccessed.BackColor = pReadonlyColour 
    txtTimeAccessed.ReadOnly = True
    txtTimeAccessed.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtUser.ReadOnly = True
      txtUser.Visible = True
      txtUser.BackColor = pReadonlyColour
      txtUser.ForeColor = SetForeColor(vInEdit) 
      cboUser.Visible = False 
    Else 
      txtUser.ReadOnly = True
      txtUser.Visible = Not (vInEdit)
      txtUser.BackColor = pReadonlyColour 
      txtUser.ForeColor = SetForeColor(vInEdit) 
      cboUser.Visible = vInEdit
    End If  
    txtCallingFunctionWithinApplication.ReadOnly = Not (vInEdit)
    txtCallingFunctionWithinApplication.BackColor = pDefaultColour 
    txtEntryPoint.ReadOnly = Not (vInEdit)
    txtEntryPoint.BackColor = pDefaultColour 
    txtProcess.ReadOnly = Not (vInEdit)
    txtProcess.BackColor = pDefaultColour 
    txtThread.ReadOnly = Not (vInEdit)
    txtThread.BackColor = pDefaultColour 

    RaiseEvent evtControlsRefreshed(vInEdit, _LoggedRequest) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _LoggedRequest
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtLoggedLogin.Text = .LoggedLoginText 
      If Math.Abs(.TimeAccessed.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.TimeAccessed.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtTimeAccessed.Text = "" Else txtTimeAccessed.Text = .TimeAccessed.ToString(FormatFromTag(txtTimeAccessed, "dd-MM-yyyy HH:mm:ss"))
      txtUser.Text = .UserText 
      txtCallingFunctionWithinApplication.Text = .CallingFunctionWithinApplication.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtCallingFunctionWithinApplication.MaxLength = 100 
      txtEntryPoint.Text = .EntryPoint.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtEntryPoint.MaxLength = 255 
      txtProcess.Text = .Process.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtProcess.MaxLength = 75 
      txtThread.Text = .Thread.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtThread.MaxLength = 50 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-LoggedRequest-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblLoggedLogin_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedLogin.DoubleClick 
    If _LoggedRequest.LoggedLoginID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedRequest.enmParentProperty.LoggedLogin) = csLoggedRequest.enmParentProperty.LoggedLogin Then 
      If _LoggedRequest.LoggedLoginID <> 0 Then RaiseEvent evtParentChosen(csLoggedRequest.enmParentProperty.LoggedLogin, _LoggedRequest.LoggedLoginID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "LoggedLogin Detail" 
      fPopup.LoadControl("ctlc_LoggedLogin", _LoggedRequest.LoggedLoginID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblLoggedLogin_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedLogin.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedRequest.enmParentProperty.LoggedLogin) <> csLoggedRequest.enmParentProperty.LoggedLogin Then Exit Sub 
    lblLoggedLogin.ForeColor = Color.Brown 
    'lblLoggedLogin.Font = New Font(lblLoggedLogin.Font.Name, lblLoggedLogin.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblLoggedLogin.BackColor = ccHelper.InvertColour(lblLoggedLogin.ForeColor) 'did this instead 
    lblLoggedLogin.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblLoggedLogin_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedLogin.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedRequest.enmParentProperty.LoggedLogin) <> csLoggedRequest.enmParentProperty.LoggedLogin Then Exit Sub 
    lblLoggedLogin.ForeColor = Color.Brown 
    'lblLoggedLogin.Font = New Font(lblLoggedLogin.Font.Name, lblLoggedLogin.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblLoggedLogin.BackColor = Me.BackColor 'did this instead 
    lblLoggedLogin.Cursor = Cursors.Default 
  End Sub 
 
  Private Sub lblUser_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.DoubleClick 
    If _LoggedRequest.UserID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedRequest.enmParentProperty.User) = csLoggedRequest.enmParentProperty.User Then 
      If _LoggedRequest.UserID <> 0 Then RaiseEvent evtParentChosen(csLoggedRequest.enmParentProperty.User, _LoggedRequest.UserID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "User Detail" 
      fPopup.LoadControl("ctlc_User", _LoggedRequest.UserID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblUser_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedRequest.enmParentProperty.User) <> csLoggedRequest.enmParentProperty.User Then Exit Sub 
    lblUser.ForeColor = Color.Brown 
    'lblUser.Font = New Font(lblUser.Font.Name, lblUser.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblUser.BackColor = ccHelper.InvertColour(lblUser.ForeColor) 'did this instead 
    lblUser.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblUser_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedRequest.enmParentProperty.User) <> csLoggedRequest.enmParentProperty.User Then Exit Sub 
    lblUser.ForeColor = Color.Brown 
    'lblUser.Font = New Font(lblUser.Font.Name, lblUser.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblUser.BackColor = Me.BackColor 'did this instead 
    lblUser.Cursor = Cursors.Default 
  End Sub 
 
  
 
  Private Sub ctlc_LoggedRequest_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the LoggedRequest to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pLoggedRequest As csLoggedRequest = _LoggedRequest 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pLoggedRequest.ToCSV) 
        Else 
          Clipboard.SetText(pLoggedRequest.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The LoggedRequest is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_LoggedRequest_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlLoggedRequest_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
