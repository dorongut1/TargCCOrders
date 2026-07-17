Public Class ctlc_LoggedJob
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vLoggedJob As csLoggedJob) 
  
  Public Event evtParentChosen(ByVal vParentName As csLoggedJob.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csLoggedJob.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csLoggedJob.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csLoggedJob.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csLoggedJob.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csLoggedJob.enmParentProperty) 
      _EnableParentLinks.Add(csLoggedJob.enmParentProperty.Job) 
      _EnableParentLinks.Add(csLoggedJob.enmParentProperty.LoggedAlert) 
 
    End Sub 
  End Class 
 
  Private WithEvents _LoggedJob As csLoggedJob

  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlLoggedJob_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Function LoadControl(ByVal vLoggedJobID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pLoggedJob As New csLoggedJob(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vLoggedJobID <> 0 Then 
      pFault = pLoggedJob.GetByID(vLoggedJobID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pLoggedJob) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rLoggedJob As csLoggedJob, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rLoggedJob)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rLoggedJob As csLoggedJob) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _LoggedJob = rLoggedJob 

    If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then MsgBox(Me.Name.ToString() & " CallingFunctionWithinApplication is empty!!") 
    
    LoadLocalizedText()
 
    'Use evtBeforeLoad to set or remove the list type (if any), if you don't want the default
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList 
    'also use to set final load parameters 
    RaiseEvent evtBeforeLoad() 
    
    SetUpControls()
 
    If cboRunStatus.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      MyCache.SetLevel(clsEnums.enmComboListType.c_JobDefaultByID, Cache.enmLevel.Previous) 
      MyCache.SetLevel(clsEnums.enmComboListType.c_LoggedAlertDefaultByID, Cache.enmLevel.Previous) 
      
      'Lookup Combos
      'EnumCombos
      pFault = LoadCboRunStatus() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboJob() : If pFault.isOK = False Then Return pFault 
    pFault = LoadCboLoggedAlert() : If pFault.isOK = False Then Return pFault 
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rLoggedJob"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rLoggedJob As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rLoggedJob.GetType.Name = "csLoggedJob" Then 
      ctlLoggedJob_Load(Nothing, Nothing) 
      Dim pLoggedJob As csLoggedJob = CType(rLoggedJob, csLoggedJob) 
      Return LoadControl(pLoggedJob) 
    Else 
      Dim pLoggedJobID As Long = CType(rLoggedJob, Long) 
      Return LoadControl(pLoggedJobID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "Job", _Requester) 
    If pStrg <> "" Then lblJob.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "WhenStarted", _Requester) 
    If pStrg <> "" Then lblWhenStarted.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "ActivatingUser", _Requester) 
    If pStrg <> "" Then lblActivatingUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "LastRunBy", _Requester) 
    If pStrg <> "" Then lblLastRunBy.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "ExecutionTimeSec", _Requester) 
    If pStrg <> "" Then lblExecutionTimeSec.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "RunStatus", _Requester) 
    If pStrg <> "" Then lblRunStatus.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "Remarks", _Requester) 
    If pStrg <> "" Then lblRemarks.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "LoggedAlert", _Requester) 
    If pStrg <> "" Then lblLoggedAlert.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "SuccessCount", _Requester) 
    If pStrg <> "" Then lblSuccessCount.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "FailureCount", _Requester) 
    If pStrg <> "" Then lblFailureCount.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [LoggedJob]() As csLoggedJob
    Get 
      Return _LoggedJob 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboJob() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_JobDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csLoggedJob.enmParentProperty.Job, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboJob.MakeSmart() Else cboJob.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboJob.LoadControl(pComboList, pPrompt) 
    Else 
      cboJob.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _LoggedJob.JobID > 0 Then cboJob.ValueSelect(_LoggedJob.JobID) Else cboJob.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboRunStatus() As clsFault
    Dim pFault As New clsFault
 
    'If cboRunStatus.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pRunStatuses As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csLoggedJob.enmParentProperty.RunStatus, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pRunStatuses.FillEnums(clsEnums.enmEnum.JobStatus, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pRunStatuses = pTestCol
    End If
    
    pRunStatuses.Remove(pRunStatuses.FindByKey(clsEnums.enmJobStatus.UD))
    pRunStatuses.SortByText()
    pRunStatuses.AddToTop(clsEnums.enmJobStatus.UD, GetChoose(_Requester))

    With cboRunStatus
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pRunStatuses
    End With

    cboRunStatus.SelectedValue = _LoggedJob.RunStatus 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboLoggedAlert() As clsFault
    Dim pFault As clsFault

    'enable using an external list if needed 
    Dim pComboList As clsComboList = Nothing 
    Dim pPrompt As String = "" 
    Dim pMakeSmart As Boolean = True 
    Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_LoggedAlertDefaultByID 
    Dim pParentID As Long = 0 
    RaiseEvent evtOverrideLoadIntelliCombo(csLoggedJob.enmParentProperty.LoggedAlert, pComboListTypeToLoad, pParentID, pComboList, pPrompt, pMakeSmart) 
    If pComboList Is Nothing Then 
      pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList, pParentID) : If Not pFault.isOK() Then Return pFault 
    Else
      pFault = New clsFault() 
      pFault.SetOK() 
    End If
    
    If pMakeSmart Then cboLoggedAlert.MakeSmart() Else cboLoggedAlert.MakeDumb() 
     
    If pPrompt = "" Then pPrompt = ccHelper.GetChoose(_Requester) 
    If pComboList IsNot Nothing Then 
      cboLoggedAlert.LoadControl(pComboList, pPrompt) 
    Else 
      cboLoggedAlert.LoadControlAndPageFromServer(pPrompt, pComboListTypeToLoad, pParentID, _Requester) 
    End If 
    
    If _LoggedJob.LoggedAlertID > 0 Then cboLoggedAlert.ValueSelect(_LoggedJob.LoggedAlertID) Else cboLoggedAlert.ValueSelect(ccHelper.ToLong(-1)) 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboJob_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboJob.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csLoggedJob.enmParentProperty.Job, pUniqueCode) 
  End Sub 
  Private Sub cboLoggedAlert_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles cboLoggedAlert.evtComboListMemberChosen 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUniqueCode As Long = vComboListMember.KeyLong 
    RaiseEvent evtCboSelectedIndexChanged(csLoggedJob.enmParentProperty.LoggedAlert, pUniqueCode) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csLoggedJob.enmParentProperty = csLoggedJob.enmParentProperty.UD 
    
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
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedJob.enmParentProperty.Job) = csLoggedJob.enmParentProperty.Job Then 
      lblJob.ForeColor = Color.Brown 
    End If 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedJob.enmParentProperty.LoggedAlert) = csLoggedJob.enmParentProperty.LoggedAlert Then 
      lblLoggedAlert.ForeColor = Color.Brown 
    End If 
    txtID.ReadOnly = True 
    txtID.BackColor = pReadonlyColour 
    txtID.ForeColor = SetForeColor(vInEdit) 
    If vInEdit = False Then 
      txtJob.ReadOnly = True
      txtJob.Visible = True
      txtJob.BackColor = pReadonlyColour
      txtJob.ForeColor = SetForeColor(vInEdit) 
      cboJob.Visible = False 
    Else 
      txtJob.ReadOnly = True
      txtJob.Visible = Not (vInEdit)
      txtJob.BackColor = pReadonlyColour 
      txtJob.ForeColor = SetForeColor(vInEdit) 
      cboJob.Visible = vInEdit
    End If  
    txtWhenStarted.Visible = True 
    txtWhenStarted.BackColor = pReadonlyColour 
    txtWhenStarted.ReadOnly = True
    txtWhenStarted.ForeColor = SetForeColor(vInEdit) 
    txtActivatingUser.ReadOnly = Not (vInEdit)
    txtActivatingUser.BackColor = pDefaultColour 
    txtLastRunBy.ReadOnly = Not (vInEdit)
    txtLastRunBy.BackColor = pDefaultColour 
    txtExecutionTimeSec.ReadOnly = Not (vInEdit)
    txtExecutionTimeSec.BackColor = pDefaultColour 
    txtRunStatus.ReadOnly = True
    txtRunStatus.Visible = Not (vInEdit)
    txtRunStatus.BackColor = pReadonlyColour 
    txtRunStatus.ForeColor = SetForeColor(vInEdit) 
    cboRunStatus.Visible = vInEdit
    txtRemarks.ReadOnly = Not (vInEdit)
    txtRemarks.BackColor = pDefaultColour 
    If vInEdit = False Then 
      txtLoggedAlert.ReadOnly = True
      txtLoggedAlert.Visible = True
      txtLoggedAlert.BackColor = pReadonlyColour
      txtLoggedAlert.ForeColor = SetForeColor(vInEdit) 
      cboLoggedAlert.Visible = False 
    Else 
      txtLoggedAlert.ReadOnly = True
      txtLoggedAlert.Visible = Not (vInEdit)
      txtLoggedAlert.BackColor = pReadonlyColour 
      txtLoggedAlert.ForeColor = SetForeColor(vInEdit) 
      cboLoggedAlert.Visible = vInEdit
    End If  
    txtSuccessCount.ReadOnly = Not (vInEdit)
    txtSuccessCount.BackColor = pDefaultColour 
    txtFailureCount.ReadOnly = Not (vInEdit)
    txtFailureCount.BackColor = pDefaultColour 

    RaiseEvent evtControlsRefreshed(vInEdit, _LoggedJob) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _LoggedJob
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      txtJob.Text = .JobText 
      If Math.Abs(.WhenStarted.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.WhenStarted.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtWhenStarted.Text = "" Else txtWhenStarted.Text = .WhenStarted.ToString(FormatFromTag(txtWhenStarted, "dd-MM-yyyy HH:mm:ss"))
      txtActivatingUser.Text = .ActivatingUser.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtActivatingUser.MaxLength = 50 
      txtLastRunBy.Text = .LastRunBy.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLastRunBy.MaxLength = 50 
      txtExecutionTimeSec.Text = .ExecutionTimeSec.ToString(FormatFromTag(txtExecutionTimeSec, "#,##0"))
      cboRunStatus.SelectedValue = .RunStatus
      txtRunStatus.Text = cboRunStatus.Text : If cboRunStatus.SelectedValue Is Nothing OrElse cboRunStatus.SelectedValue.ToString() = "UD" Then txtRunStatus.Text = ""    
      txtRemarks.Text = .Remarks.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLoggedAlert.Text = .LoggedAlertText 
      txtSuccessCount.Text = .SuccessCount.ToString(FormatFromTag(txtSuccessCount, "#,##0"))
      txtFailureCount.Text = .FailureCount.ToString(FormatFromTag(txtFailureCount, "#,##0"))
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-LoggedJob-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtExecutionTimeSec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExecutionTimeSec.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtExecutionTimeSec.Text 
    Dim pTest As Integer 
 
    If txtExecutionTimeSec.Text = "" Then Exit Sub 
    If txtExecutionTimeSec.Text = txtExecutionTimeSec.Name Then Exit Sub 
 
    If Integer.TryParse(txtExecutionTimeSec.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-LoggedJob-ExecutionTimeSec-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtSuccessCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSuccessCount.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtSuccessCount.Text 
    Dim pTest As Integer 
 
    If txtSuccessCount.Text = "" Then Exit Sub 
    If txtSuccessCount.Text = txtSuccessCount.Name Then Exit Sub 
 
    If Integer.TryParse(txtSuccessCount.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-LoggedJob-SuccessCount-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtFailureCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFailureCount.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtFailureCount.Text 
    Dim pTest As Integer 
 
    If txtFailureCount.Text = "" Then Exit Sub 
    If txtFailureCount.Text = txtFailureCount.Name Then Exit Sub 
 
    If Integer.TryParse(txtFailureCount.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-LoggedJob-FailureCount-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Ensure Read-Only

  'Now the Parents
  Private Sub lblJob_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblJob.DoubleClick 
    If _LoggedJob.JobID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedJob.enmParentProperty.Job) = csLoggedJob.enmParentProperty.Job Then 
      If _LoggedJob.JobID <> 0 Then RaiseEvent evtParentChosen(csLoggedJob.enmParentProperty.Job, _LoggedJob.JobID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "Job Detail" 
      fPopup.LoadControl("ctlc_Job", _LoggedJob.JobID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblJob_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblJob.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedJob.enmParentProperty.Job) <> csLoggedJob.enmParentProperty.Job Then Exit Sub 
    lblJob.ForeColor = Color.Brown 
    'lblJob.Font = New Font(lblJob.Font.Name, lblJob.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblJob.BackColor = ccHelper.InvertColour(lblJob.ForeColor) 'did this instead 
    lblJob.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblJob_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblJob.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedJob.enmParentProperty.Job) <> csLoggedJob.enmParentProperty.Job Then Exit Sub 
    lblJob.ForeColor = Color.Brown 
    'lblJob.Font = New Font(lblJob.Font.Name, lblJob.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblJob.BackColor = Me.BackColor 'did this instead 
    lblJob.Cursor = Cursors.Default 
  End Sub 
 
  Private Sub lblLoggedAlert_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedAlert.DoubleClick 
    If _LoggedJob.LoggedAlertID = 0 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
 
    Dim pHandled As Boolean = False 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedJob.enmParentProperty.LoggedAlert) = csLoggedJob.enmParentProperty.LoggedAlert Then 
      If _LoggedJob.LoggedAlertID <> 0 Then RaiseEvent evtParentChosen(csLoggedJob.enmParentProperty.LoggedAlert, _LoggedJob.LoggedAlertID, pHandled) 
    End If 
 
    If pHandled = False Then 
      Dim fPopup As New frmPopup() 
      fPopup.Text = "LoggedAlert Detail" 
      fPopup.LoadControl("ctlc_LoggedAlert", _LoggedJob.LoggedAlertID, _Requester) 
      Cursor = Cursors.Default 
      fPopup.ShowDialog() 
    Else 
      Cursor = Cursors.Default 
    End If 
  End Sub 
  Private Sub lblLoggedAlert_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedAlert.MouseEnter 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedJob.enmParentProperty.LoggedAlert) <> csLoggedJob.enmParentProperty.LoggedAlert Then Exit Sub 
    lblLoggedAlert.ForeColor = Color.Brown 
    'lblLoggedAlert.Font = New Font(lblLoggedAlert.Font.Name, lblLoggedAlert.Font.SizeInPoints, FontStyle.Underline) 'makes the page jump to the top!! 
    lblLoggedAlert.BackColor = ccHelper.InvertColour(lblLoggedAlert.ForeColor) 'did this instead 
    lblLoggedAlert.Cursor = Cursors.Hand 
  End Sub 
  Private Sub lblLoggedAlert_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLoggedAlert.MouseLeave 
    If _LoadParameters.EnableParentLinks.Find(Function(p) p = csLoggedJob.enmParentProperty.LoggedAlert) <> csLoggedJob.enmParentProperty.LoggedAlert Then Exit Sub 
    lblLoggedAlert.ForeColor = Color.Brown 
    'lblLoggedAlert.Font = New Font(lblLoggedAlert.Font.Name, lblLoggedAlert.Font.SizeInPoints) 'makes the page jump to the top!! 
    lblLoggedAlert.BackColor = Me.BackColor 'did this instead 
    lblLoggedAlert.Cursor = Cursors.Default 
  End Sub 
 
  
 
  Private Sub ctlc_LoggedJob_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the LoggedJob to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pLoggedJob As csLoggedJob = _LoggedJob 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pLoggedJob.ToCSV) 
        Else 
          Clipboard.SetText(pLoggedJob.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The LoggedJob is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_LoggedJob_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlLoggedJob_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
