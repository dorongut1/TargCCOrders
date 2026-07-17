Public Class ctlc_Job
 
  Private _Requester As clsRequester  
  
  Private _InEdit As Boolean  
  
  Private _Loading As Boolean = False  
  
  'events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
  
  Public Event evtEdit(ByVal vWhichType As csJob.enmUpdateType) 
  Public Event evtAdd(ByVal vJob As csJob) 
  Public Event evtBeforeUpdate(ByVal vJob As csJob, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vWhichType As csJob.enmUpdateType, ByVal vJob As csJob) 
  Public Event evtBeforeDelete(ByVal vJob As csJob, ByRef rCancel As Nullable(Of Boolean)) 
  Public Event evtDeleted(ByVal vJobID As Long) 
  Public Event evtCancelledEdit(ByVal vJob As csJob) 
  Public Event evtControlsRefreshed(ByVal vInEdit As Boolean, ByVal vJob As csJob) 
  
  Public Event evtParentChosen(ByVal vParentName As csJob.enmParentProperty, ByVal vParentUniqueCode As Object, ByRef rHandled As Boolean) 
  Public Event evtOverrideLoadIntelliCombo(ByVal vParentName As csJob.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String, ByRef rMakeSmart As Boolean) 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csJob.enmParentProperty, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  Public Event evtCboSelectedIndexChanged(ByVal vParentName As csJob.enmParentProperty, ByVal vSelectedValue As Object) 
   
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
    Public Property EnableParentLinks As List(Of csJob.enmParentProperty) 
    ''' <summary> 
    ''' Initializes with ReadOnly = False, all Parent Combos loaded OnFirstControlLoadOnly, and all Parent links enabled 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _ReadOnly = False 
      _EnableParentLinks = New List(Of csJob.enmParentProperty) 
 
    End Sub 
  End Class 
 
  Private WithEvents _Job As csJob

  'History Button 
  Friend WithEvents btnHistory As New System.Windows.Forms.Button 
 
  Friend WithEvents txtGrabFocus As TextBox 
 
  Private Sub ctlJob_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    cboJob.Size = txtJob.Size
    cboJob.Location = txtJob.Location
    cboJobRunner.Size = txtJobRunner.Size
    cboJobRunner.Location = txtJobRunner.Location
    cboJobType.Size = txtJobType.Size
    cboJobType.Location = txtJobType.Location
    dtpWhenToRun.Size = txtWhenToRun.Size
    dtpWhenToRun.Location = txtWhenToRun.Location
    'Separate buttons 
  End Sub

  Public Function LoadControl(ByVal vJobID As Long, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault 
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pJob As New csJob() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    If vJobID <> 0 Then 
      pFault = pJob.GetByID(vJobID, _Requester, False) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pJob) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByRef rJob As csJob, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
    Return LoadControl(rJob)
  End Function 
 
  Private _Scaled As Boolean = False 
 
  Private Function LoadControl(ByRef rJob As csJob) As clsFault
    Dim pFault As New clsFault
    
    If _Scaled = False Then 
      MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
      Me.Font = MyFont 
      Me.PerformAutoScale() 
      _Scaled = True 
    End If 
    
    _InEdit = False 
    
    _Job = rJob 

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
 
    If cboJob.Items.Count = 0 Then
      'Combos
      'Set comboListsCache 
      
      'Lookup Combos
      pFault = LoadCboJob() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboJobRunner() : If pFault.isOK = False Then Return pFault 
      'EnumCombos
      pFault = LoadCboJobType() : If pFault.isOK = False Then Return pFault 
      pFault = LoadCboJobStatus() : If pFault.isOK = False Then Return pFault 
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
    pFault = LoadCboJob() : If pFault.isOK = False Then Return pFault 
    pFault = LoadCboJobRunner() : If pFault.isOK = False Then Return pFault 
 
    'Parents
 
    _Loading = False 
 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' For Popups 
  ''' </summary> 
  ''' <param name="rJob"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function LoadControlForPopup(ByRef rJob As Object, ByVal vRequester As clsRequester) As clsFault 
    _Requester = vRequester 
    _LoadParameters = New clsLoadParameters 
    With _LoadParameters 
      .ReadOnly = True 
      '.EnableParentLinks.Clear() 
    End With 
    
    If rJob.GetType.Name = "csJob" Then 
      ctlJob_Load(Nothing, Nothing) 
      Dim pJob As csJob = CType(rJob, csJob) 
      Return LoadControl(pJob) 
    Else 
      Dim pJobID As Long = CType(rJob, Long) 
      Return LoadControl(pJobID, _LoadParameters, vRequester) 
    End If 
 
  End Function 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "ID", _Requester) 
    If pStrg <> "" Then lblID.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "Job", _Requester) 
    If pStrg <> "" Then lblJob.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "JobRunner", _Requester) 
    If pStrg <> "" Then lblJobRunner.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "Description", _Requester) 
    If pStrg <> "" Then lblDescription.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "Instructions", _Requester) 
    If pStrg <> "" Then lblInstructions.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "JobType", _Requester) 
    If pStrg <> "" Then lblJobType.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "WhenToRun", _Requester) 
    If pStrg <> "" Then lblWhenToRun.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "CyclicCount", _Requester) 
    If pStrg <> "" Then lblCyclicCount.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "SendNotificationOnSuccess", _Requester) 
    If pStrg <> "" Then lblSendNotificationOnSuccess.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "SendAlarmOnMissed", _Requester) 
    If pStrg <> "" Then lblSendAlarmOnMissed.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "TimeOutSec", _Requester) 
    If pStrg <> "" Then lblTimeOutSec.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "Active", _Requester) 
    If pStrg <> "" Then lblActive.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "ActivatingUser", _Requester) 
    If pStrg <> "" Then lblActivatingUser.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "NextRunTime", _Requester) 
    If pStrg <> "" Then lblNextRunTime.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "LastRunTime", _Requester) 
    If pStrg <> "" Then lblLastRunTime.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "JobStatus", _Requester) 
    If pStrg <> "" Then lblJobStatus.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "WarningMailSent", _Requester) 
    If pStrg <> "" Then lblWarningMailSent.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "IsManaged", _Requester) 
    If pStrg <> "" Then lblIsManaged.Text = pStrg 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_Job", "LastRunBy", _Requester) 
    If pStrg <> "" Then lblLastRunBy.Text = pStrg 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pbtn As Button = CType(p, Button) 
        pStrg = CCTextTranslate(pbtn.Text, _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Public ReadOnly Property [Job]() As csJob
    Get 
      Return _Job 
    End Get 
  End Property 
 
  'Load comboboxes
  Private Function LoadCboJob() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboJob.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csJob.enmParentProperty.Job, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.Job, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText() 
    Else
      pComboList = pTestCol
    End If

    cboJob.Tag = "" 
    pFault = LoadCbo(cboJob, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _Job.JobCode <> "" Then cboJob.SelectedValue = _Job.JobCode

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboJobRunner() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboJobRunner.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csJob.enmParentProperty.JobRunner, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.JobRunner, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText() 
    Else
      pComboList = pTestCol
    End If

    cboJobRunner.Tag = "" 
    pFault = LoadCbo(cboJobRunner, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _Job.JobRunnerCode <> "" Then cboJobRunner.SelectedValue = _Job.JobRunnerCode

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboJobType() As clsFault
    Dim pFault As New clsFault
 
    'If cboJobType.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pJobTypees As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csJob.enmParentProperty.JobType, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pJobTypees.FillEnums(clsEnums.enmEnum.JobType, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pJobTypees = pTestCol
    End If
    
    pJobTypees.Remove(pJobTypees.FindByKey(clsEnums.enmJobType.UD))
    pJobTypees.SortByText()
    pJobTypees.AddToTop(clsEnums.enmJobType.UD, GetChoose(_Requester))

    With cboJobType
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pJobTypees
    End With

    cboJobType.SelectedValue = _Job.JobType 

    Return pFault.SetOK() 
  End Function
  Private Function LoadCboJobStatus() As clsFault
    Dim pFault As New clsFault
 
    'If cboJobStatus.Items.Count > 0 Then Return pFault.SetOK() 'Already loaded 
 
    Dim pJobStatuses As New clsComboList
     
    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing 
    Dim pPrompt As String = ccHelper.GetChoose(_Requester) 
    RaiseEvent evtOverrideLoadCbo(csJob.enmParentProperty.JobStatus, pTestCol, pPrompt) 
    If pTestCol Is Nothing Then 
      pFault = pJobStatuses.FillEnums(clsEnums.enmEnum.JobStatus, _Requester)
      If pFault.isOK = False Then Return pFault
    Else
      pJobStatuses = pTestCol
    End If
    
    pJobStatuses.Remove(pJobStatuses.FindByKey(clsEnums.enmJobStatus.UD))
    pJobStatuses.SortByText()
    pJobStatuses.AddToTop(clsEnums.enmJobStatus.UD, GetChoose(_Requester))

    With cboJobStatus
      .ValueMember = "KeyEnum"
      .DisplayMember = "Text"
      .DataSource = pJobStatuses
    End With

    cboJobStatus.SelectedValue = _Job.JobStatus 

    Return pFault.SetOK() 
  End Function
  
  'Handle Comboboxes
  Private Sub cboJob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboJob.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboJob.SelectedValue, String) 
    RaiseEvent evtCboSelectedIndexChanged(csJob.enmParentProperty.Job, pCode) 
  End Sub 
  Private Sub cboJobRunner_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboJobRunner.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCode As String = CType(cboJobRunner.SelectedValue, String) 
    RaiseEvent evtCboSelectedIndexChanged(csJob.enmParentProperty.JobRunner, pCode) 
  End Sub 
  Private Sub cboJobType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboJobType.SelectedIndexChanged 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pEnum As clsEnums.enmJobType = CType(cboJobType.SelectedValue, clsEnums.enmJobType) 
    RaiseEvent evtCboSelectedIndexChanged(csJob.enmParentProperty.JobType, pEnum.ToString) 
  End Sub 
  'Handle Controls
  Private _ButtonsMoved As Boolean = False 
  
  Private Sub SetUpButtons(ByVal vInEdit As Boolean)
    Dim pParentLinkName As csJob.enmParentProperty = csJob.enmParentProperty.UD 
    
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
    txtJob.ReadOnly = True
    txtJob.Visible = Not (vInEdit)
    txtJob.BackColor = pReadonlyColour 
    txtJob.ForeColor = SetForeColor(vInEdit) 
    cboJob.Visible = vInEdit
    txtJobRunner.ReadOnly = True
    txtJobRunner.Visible = Not (vInEdit)
    txtJobRunner.BackColor = pReadonlyColour 
    txtJobRunner.ForeColor = SetForeColor(vInEdit) 
    cboJobRunner.Visible = vInEdit
    txtDescription.ReadOnly = Not (vInEdit)
    txtDescription.BackColor = pDefaultColour 
    txtInstructions.ReadOnly = Not (vInEdit)
    txtInstructions.BackColor = pDefaultColour 
    txtJobType.ReadOnly = True
    txtJobType.Visible = Not (vInEdit)
    txtJobType.BackColor = pReadonlyColour 
    txtJobType.ForeColor = SetForeColor(vInEdit) 
    cboJobType.Visible = vInEdit
    dtpWhenToRun.Visible = vInEdit
    txtWhenToRun.Visible = Not (vInEdit)
    txtWhenToRun.BackColor = pReadonlyColour 
    txtWhenToRun.ForeColor = SetForeColor(vInEdit) 
    txtWhenToRun.ReadOnly = True
    txtCyclicCount.ReadOnly = Not (vInEdit)
    txtCyclicCount.BackColor = pDefaultColour 
    chkSendNotificationOnSuccess.Enabled = True
    chkSendAlarmOnMissed.Enabled = True
    txtTimeOutSec.ReadOnly = Not (vInEdit)
    txtTimeOutSec.BackColor = pDefaultColour 
    chkActive.Enabled = True
    txtActivatingUser.ReadOnly = True 
    txtActivatingUser.BackColor = pReadonlyColour 
    txtActivatingUser.ForeColor = SetForeColor(vInEdit) 
    txtNextRunTime.ReadOnly = True 
    txtNextRunTime.BackColor = pReadonlyColour 
    txtNextRunTime.ForeColor = SetForeColor(vInEdit) 
    txtLastRunTime.ReadOnly = True 
    txtLastRunTime.BackColor = pReadonlyColour 
    txtLastRunTime.ForeColor = SetForeColor(vInEdit) 
    cboJobStatus.Visible = False 
    txtJobStatus.ReadOnly = True 
    txtJobStatus.BackColor = pReadonlyColour 
    txtJobStatus.ForeColor = SetForeColor(vInEdit) 
    chkWarningMailSent.Enabled = True 
    chkIsManaged.Enabled = True
    txtLastRunBy.ReadOnly = Not (vInEdit)
    txtLastRunBy.BackColor = pDefaultColour 

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
      If _Job.ID = 0 Then 
        btnEdit.Visible = False 
        btnDelete.Visible = False 
      Else 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_JobUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_JobDelete, _Requester) = True Then btnDelete.Visible = Not (vInEdit) Else btnDelete.Visible = False 
      End If 
        If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_JobUpdate, _Requester) = True Then btnAdd.Visible = Not (vInEdit) Else btnAdd.Visible = False 
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
 
    RaiseEvent evtControlsRefreshed(vInEdit, _Job) 
  End Sub
  Private Function SetForeColor(ByVal vInEdit As Boolean) As System.Drawing.Color 
    If vInEdit = True Then 
      Return Color.Gray 
    Else 
      Return Color.Black 
    End If 
  End Function 
  Private Sub ControlsLoad()
    With _Job
      txtID.Text = .ID.ToString(FormatFromTag(txtID, "###0"))
      cboJob.SelectedValue = .JobCode
      txtJob.Text = cboJob.Text : If cboJob.SelectedValue Is Nothing OrElse cboJob.SelectedValue.ToString() = "" Then txtJob.Text = ""    
      cboJobRunner.SelectedValue = .JobRunnerCode
      txtJobRunner.Text = cboJobRunner.Text : If cboJobRunner.SelectedValue Is Nothing OrElse cboJobRunner.SelectedValue.ToString() = "" Then txtJobRunner.Text = ""    
      txtDescription.Text = .Description.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtDescription.MaxLength = 500 
      txtInstructions.Text = .Instructions.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtInstructions.MaxLength = 1000 
      cboJobType.SelectedValue = .JobType
      txtJobType.Text = cboJobType.Text : If cboJobType.SelectedValue Is Nothing OrElse cboJobType.SelectedValue.ToString() = "UD" Then txtJobType.Text = ""    
      If .WhenToRun < dtpWhenToRun.MinDate Then dtpWhenToRun.Value = New Date(1900, 1, 1, 0, 0, 0) Else dtpWhenToRun.Value = .WhenToRun
      dtpWhenToRun.CustomFormat = FormatFromTag(txtWhenToRun, "dd-MM-yyyy HH:mm:ss") 
      dtpWhenToRun.Value = DateTime.ParseExact(dtpWhenToRun.Value.ToString(dtpWhenToRun.CustomFormat), dtpWhenToRun.CustomFormat, System.Globalization.CultureInfo.CurrentCulture) 
      If .WhenToRun < dtpWhenToRun.MinDate Then dtpWhenToRun.Checked = False Else dtpWhenToRun.Checked = True 
      If Math.Abs(.WhenToRun.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.WhenToRun.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtWhenToRun.Text = "" Else txtWhenToRun.Text = .WhenToRun.ToString(FormatFromTag(txtWhenToRun, "dd-MM-yyyy HH:mm:ss"))
      txtCyclicCount.Text = .CyclicCount.ToString(FormatFromTag(txtCyclicCount, "#,##0"))
      chkSendNotificationOnSuccess.Checked = .SendNotificationOnSuccess
      chkSendAlarmOnMissed.Checked = .SendAlarmOnMissed
      txtTimeOutSec.Text = .TimeOutSec.ToString(FormatFromTag(txtTimeOutSec, "#,##0"))
      chkActive.Checked = .Active
      txtActivatingUser.Text = .ActivatingUser.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtActivatingUser.MaxLength = 50 
      If Math.Abs(.NextRunTime.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.NextRunTime.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtNextRunTime.Text = "" Else txtNextRunTime.Text = .NextRunTime.ToString(FormatFromTag(txtNextRunTime, "dd-MM-yyyy HH:mm:ss"))
      If Math.Abs(.LastRunTime.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(.LastRunTime.Subtract(DateTime.MaxValue).TotalDays) < 5 Then txtLastRunTime.Text = "" Else txtLastRunTime.Text = .LastRunTime.ToString(FormatFromTag(txtLastRunTime, "dd-MM-yyyy HH:mm:ss"))
      cboJobStatus.SelectedValue = .JobStatus
      txtJobStatus.Text = cboJobStatus.Text : If cboJobStatus.SelectedValue Is Nothing OrElse cboJobStatus.SelectedValue.ToString() = "UD" Then txtJobStatus.Text = ""    
      chkWarningMailSent.Checked = .WarningMailSent
      chkIsManaged.Checked = .IsManaged
      txtLastRunBy.Text = .LastRunBy.Replace(ControlChars.Lf, Environment.NewLine).Replace(ControlChars.Cr & ControlChars.Cr, ControlChars.Cr).Replace(" ‡ ", Environment.NewLine)
      txtLastRunBy.MaxLength = 50 
    End With
  End Sub
  Private Function ControlsRead() As clsFault 
    Dim pFault As New clsFault
    With _Job
      If Long.TryParse(txtID.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .ID) = False Then pFault.LogFreeTextFault(208, ".ID", txtID.Text, "TRGT-Job-ID-090417-0012", _Requester) : Return pFault 
      If cboJob.SelectedItem IsNot Nothing Then .JobCode = CType(cboJob.SelectedItem, clsComboListMember).KeyString Else .JobCode = "" 
      If cboJobRunner.SelectedItem IsNot Nothing Then .JobRunnerCode = CType(cboJobRunner.SelectedItem, clsComboListMember).KeyString Else .JobRunnerCode = "" 
      .Description = txtDescription.Text 
      .Instructions = txtInstructions.Text 
      .JobType = CType(cboJobType.SelectedValue, clsEnums.enmJobType)
      If (dtpWhenToRun.ShowCheckBox AndAlso dtpWhenToRun.Checked = False) OrElse dtpWhenToRun.Value = New Date(1900, 1, 1, 0, 0, 0) Then .WhenToRun = Nothing Else .WhenToRun = dtpWhenToRun.Value
      If Integer.TryParse(txtCyclicCount.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .CyclicCount) = False Then pFault.LogFreeTextFault(208, ".CyclicCount", txtCyclicCount.Text, "TRGT-Job-CyclicCount-090417-0013", _Requester) : Return pFault 
      .SendNotificationOnSuccess = chkSendNotificationOnSuccess.Checked
      .SendAlarmOnMissed = chkSendAlarmOnMissed.Checked
      If Integer.TryParse(txtTimeOutSec.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, .TimeOutSec) = False Then pFault.LogFreeTextFault(208, ".TimeOutSec", txtTimeOutSec.Text, "TRGT-Job-TimeOutSec-090417-0013", _Requester) : Return pFault 
      .Active = chkActive.Checked
      .IsManaged = chkIsManaged.Checked
      .LastRunBy = txtLastRunBy.Text 
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
      pFault.LogFreeTextFault(211, "", pFunctionParameters, "TRGT-Job-ID-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtCyclicCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCyclicCount.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtCyclicCount.Text 
    Dim pTest As Integer 
 
    If txtCyclicCount.Text = "" Then Exit Sub 
    If txtCyclicCount.Text = txtCyclicCount.Name Then Exit Sub 
 
    If Integer.TryParse(txtCyclicCount.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-Job-CyclicCount-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 
  Private Sub txtTimeOutSec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTimeOutSec.TextChanged 
    Dim pFunctionParameters As String = "Text Input=" & txtTimeOutSec.Text 
    Dim pTest As Integer 
 
    If txtTimeOutSec.Text = "" Then Exit Sub 
    If txtTimeOutSec.Text = txtTimeOutSec.Name Then Exit Sub 
 
    If Integer.TryParse(txtTimeOutSec.Text & "0", Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pTest) = False Then 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(210, "", pFunctionParameters, "TRGT-Job-TimeOutSec-100907-1302", _Requester) 
      ShowFault(pFault, _Requester) 
    End If 
  End Sub 

  'Buttons
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    SetUpButtons(True) 
    RaiseEvent evtEdit(csJob.enmUpdateType.Standard) 
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
      pFault.LogException(ex, "", "TRGT-Job-100711-1722", _Requester) 
    End Try 
    If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdate(_Job, pCancel) 
    If pCancel = True Then pFault.SetOK() : Cursor = Cursors.Default : Exit Sub 
    pFault = _Job.Update(_Requester, True) 
    If pFault.isOK = True OrElse pFault.Severity = clsEnums.enmFaultSeverity.Info Then 
      'Reset the Job collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.c_JobDefaultByID) 
      RaiseEvent evtUpdated(csJob.enmUpdateType.Standard, _Job) 
      ShowToast("Saved successfully") 
    End If 
    _InEdit = False 
    Me.Refresh() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub
  '_Job_evtAfterUpdate 
  Private Sub _Job_evtAfterUpdate() Handles _Job.evtAfterUpdate, _Job.evtAfterGet 
    ControlsLoad() 
    SetUpButtons(False) 
  End Sub 
  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = False 
    ControlsLoad() 
    SetUpButtons(False) 
    RaiseEvent evtCancelledEdit(_Job) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    _InEdit = True 
    _Job = New csJob() 
    ControlsLoad() 
    SetUpButtons(True) 
    RaiseEvent evtAdd(_Job) 
    Me.Refresh() 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault
    Dim pCancel As Nullable(Of Boolean) = Nothing 
    RaiseEvent evtBeforeDelete(_Job, pCancel) 
    If pCancel = True Then 
      Exit Sub 
    ElseIf pCancel Is Nothing Then 
      Dim pRequest As String = "Are you sure you want to delete '" & _Job.JobCode & " on " & _Job.JobRunnerCode & "'?" 
      Cursor = Cursors.Default 
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
      pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then Exit Sub 
    End If 
    Cursor = Cursors.WaitCursor 
    Dim pID As Long = _Job.ID 
    pFault = _Job.Delete(_Requester) 
    If pFault.isOK = True Then 
      _Job = Nothing 
      RaiseEvent evtDeleted(pID) 
      ShowToast("Deleted successfully") 
    End If 
    _InEdit = False 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub

  'Ensure Read-Only
  Private Sub chkSendNotificationOnSuccess_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSendNotificationOnSuccess.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkSendNotificationOnSuccess.Checked = _Job.SendNotificationOnSuccess
    End If
  End Sub
  Private Sub chkSendAlarmOnMissed_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSendAlarmOnMissed.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkSendAlarmOnMissed.Checked = _Job.SendAlarmOnMissed
    End If
  End Sub
  Private Sub chkActive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkActive.Checked = _Job.Active
    End If
  End Sub
  Private Sub chkWarningMailSent_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkWarningMailSent.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkWarningMailSent.Checked = _Job.WarningMailSent
    End If
  End Sub
  Private Sub chkIsManaged_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsManaged.CheckedChanged
    If Not _InEdit OrElse _LoadParameters.ReadOnly = True Then
      chkIsManaged.Checked = _Job.IsManaged
    End If
  End Sub

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
    pFault = pAuditIndexedCol.FillByTableNameAndRowID("c_Job", _Job.ID, _Requester, 500, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pAuditIndexed As New csAuditIndexed 
    pAuditIndexed.ID = -1 
    pAuditIndexed.Operation = "Added" 
    pAuditIndexed.OccurredAt = _Job.DateAdded 
    pAuditIndexed.TableName = "Job" 
    pAuditIndexed.RowID = _Job.ID 
    pAuditIndexed.FieldName = "** Row Added **" 
    pAuditIndexed.OldValue = "- - -" 
    pAuditIndexed.NewValue = "- - -" 
    pAuditIndexed.ChangedByUser = "- - -" 
    pAuditIndexed.ActiveLoginID = 0 
    pAuditIndexed.SqlAppName = "- - -" 
 
    pAuditIndexedCol.Add(pAuditIndexed) 
 
    Dim fPopup As New frmPopup 
    fPopup.Text = "History Detail for 'Job'" 
    pFault = fPopup.LoadControl("ctlc_AuditIndexedCol", pAuditIndexedCol, _Requester) 
    Cursor = Cursors.Default 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    fPopup.Show(Me.ParentForm) 
 
  End Sub 
 
  Private Sub ctlc_Job_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.Button = MouseButtons.Right Then 
      Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the Job to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pResult <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pJob As csJob = _Job 
        If pResult = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pJob.ToCSV) 
        Else 
          Clipboard.SetText(pJob.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The Job is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
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
 
  Private Sub ctlc_Job_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    Me.Refresh() 
  End Sub 
  
  Private Sub cc_Job_evtCboSelectedIndexChanged(vParentName As csJob.enmParentProperty, vSelectedValue As Object) Handles Me.evtCboSelectedIndexChanged 
    If vParentName = csJob.enmParentProperty.JobType Then 
 
      txtCyclicCount.Enabled = False 
      dtpWhenToRun.Enabled = False 
      dtpWhenToRun.Checked = False 
      Dim pFormat As String = "" 
 
      Select Case clsEnums.TranslateEnmJobType(CStr(vSelectedValue)) 
        Case clsEnums.enmJobType.Annually 
          dtpWhenToRun.Checked = True 
          dtpWhenToRun.Enabled = True 
          pFormat = "dd-MMM HH:mm" 
        Case clsEnums.enmJobType.Monthly 
          dtpWhenToRun.Checked = True 
          dtpWhenToRun.Enabled = True 
          pFormat = "dd/@ HH:mm" 
        Case clsEnums.enmJobType.Weekly 
          dtpWhenToRun.Checked = True 
          dtpWhenToRun.Enabled = True 
          pFormat = "dddd (dd) HH:mm" 
        Case clsEnums.enmJobType.Daily 
          dtpWhenToRun.Checked = True 
          dtpWhenToRun.Enabled = True 
          pFormat = "HH:mm" 
        Case clsEnums.enmJobType.CyclicDay 
          dtpWhenToRun.Checked = True 
          dtpWhenToRun.Enabled = True 
          pFormat = "HH:mm" 
          txtCyclicCount.Enabled = True 
        Case clsEnums.enmJobType.CyclicHour 
          dtpWhenToRun.Checked = True 
          dtpWhenToRun.Enabled = True 
          pFormat = "@:mm" 
          txtCyclicCount.Enabled = True 
        Case clsEnums.enmJobType.CyclicMin 
          dtpWhenToRun.Checked = False 
          dtpWhenToRun.Enabled = False 
          txtCyclicCount.Enabled = True 
        Case clsEnums.enmJobType.CyclicSec 
          dtpWhenToRun.Checked = False 
          dtpWhenToRun.Enabled = False 
          txtCyclicCount.Enabled = True 
        Case clsEnums.enmJobType.OneOff 
          dtpWhenToRun.Checked = True 
          dtpWhenToRun.Enabled = True 
          pFormat = "dd-MMM-yyyy HH:mm" 
        Case clsEnums.enmJobType.UD 
      End Select 
      txtWhenToRun.Tag = pFormat 
      dtpWhenToRun.CustomFormat = pFormat 
 
    End If 
  End Sub 
 
  Private Sub btnRunNow_Click(sender As Object, e As EventArgs) Handles btnRunNow.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
 
    Dim pSure As Boolean = AreYouSure("run " & _Job.JobCode & " now") 
    If pSure = False Then Exit Sub 
 
    Cursor = Cursors.WaitCursor 
 
    pFault = ccTaskManager.SetJobToNow(_Job.ID, _Requester) 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
      Exit Sub 
    End If 
 
    pFault = LoadControl(_Job.ID, _LoadParameters, _Requester) 
    If pFault.isOK = False Then 
      Cursor = Cursors.Default 
      ShowFault(pFault, _Requester) 
      Exit Sub 
    End If 
 
    Cursor = Cursors.Default 
  End Sub 
 
  Private Sub ctlc_Job_evtEditCC(vWhichType As csJob.enmUpdateType) Handles Me.evtEdit 
    btnRunNow.Visible = False 
  End Sub 
 
  Private Sub ctlc_Job_evtAddCC(vJob As csJob) Handles Me.evtAdd 
    btnRunNow.Visible = False 
  End Sub 
 
  Private Sub ctlc_Job_evtLoadedCC() Handles Me.evtLoaded 
    btnRunNow.Visible = True 
  End Sub 
 

  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    Select Case keyData 
      Case Keys.Control Or Keys.S 
        Dim pBtn = Me.Controls.Find("btnUpdate", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
      Case Keys.F5 
        ctlJob_Load(Nothing, Nothing) 
        Return True 
      Case Keys.Escape 
        Dim pBtn = Me.Controls.Find("btnCancel", True).FirstOrDefault() 
        If pBtn IsNot Nothing AndAlso pBtn.Visible AndAlso pBtn.Enabled Then DirectCast(pBtn, Button).PerformClick() 
        Return True 
    End Select 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
