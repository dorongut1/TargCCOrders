Public Class ctlc_LoggedJobCol
 
  '--- Win32 API for Sticky Modifier Keys fix (RDP / DoEvents / Accessibility) ---
  <System.Runtime.InteropServices.DllImport("user32.dll")>
  Private Shared Sub keybd_event(bVk As Byte, bScan As Byte, dwFlags As UInteger, dwExtraInfo As UInteger)
  End Sub
  <System.Runtime.InteropServices.DllImport("user32.dll")>
  Private Shared Function GetAsyncKeyState(vKey As Integer) As Short
  End Function
  Private Const KEYEVENTF_KEYUP As UInteger = &H2
  Private Const VK_SHIFT As Byte = &H10
  Private Const VK_CONTROL As Byte = &H11
 
  'Re-entrancy guard for SelectionChanged (prevents "row jump" race condition from DoEvents)
  Private _ProcessingSelection As Boolean = False
  Private _IgnoreSelectionUntil As DateTime = DateTime.MinValue
  Private _SelectionAnchor As Integer = -1
 
  Private _Requester As clsRequester 
  
  'Events 
  Public Event evtBeforeLoad() 
  Public Event evtLoaded() 
 
  
  Public Event evtRowClicked(ByVal vLoggedJob As csLoggedJob) 
  Public Event evtRowDoubleClicked(ByVal vLoggedJob As csLoggedJob, ByRef rHandled As Boolean) 
  Public Event evtUnChosen() 
 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csLoggedJob.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  
  'Handle import and export 
  Private Event evtOverrideSpreadsheet(ByRef rOverridden As Boolean) 
  Private Event evtOverrideImport(ByRef rOverridden As Boolean) 
  
  Friend Event evtCellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) 
  
  'For Timer 
  Friend WithEvents Timer As Timer 
  Friend Event evtTimerTripped() 
  Private _TimerIntervalMs As Integer = 30000 
 
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
    Public Property SummarizeGrid As Boolean 
    Public Property DoNotSummarizeProperties As List(Of csLoggedJob.enmSummarizeableProperty) 
    Public Property SpreadsheetShowAllFields As Nullable(Of Boolean) 
    Public Property GridTitle As String 
    Public Property ReportTitle As String 
    Public Property [ReadOnly] As Boolean 
    Public Property CbosDoNotLoad As List(Of csLoggedJob.enmParentProperty) 
    Public Property ColumnsReadOnly As List(Of csLoggedJob.enmProperty) 
    Public Property ColumnsHide As List(Of csLoggedJob.enmProperty) 
    Public Property ColumnsFormat As Dictionary(Of csLoggedJob.enmProperty, String) 
    Public Property ColumnsOrdinalPosition As Dictionary(Of csLoggedJob.enmProperty, Integer) 
    Public Property ColumnsAlignment As Dictionary(Of csLoggedJob.enmProperty, DataGridViewContentAlignment) 
    Public Property ColumnsHeaderText As Dictionary(Of csLoggedJob.enmProperty, String) 
    Public Property ColumnsListHide As Boolean 
    Public Property SpreadsheetButtonHide As Boolean 
    Public Property ReportButtonHide As Boolean 
    Public Property ImportButtonHide As Boolean 
    Public Property AddEditDeleteButtonsHide As Boolean 
    Public Property NavigationBarHide As Boolean 
    Public Property IsSumFillOnTheFly As Boolean 
    Public Property TruncateStrings As Boolean 
    Public Property SearchFilters As Dictionary(Of System.Enum, Object) 
    ''' <summary> 
    ''' Initializes with Summarize = True; 
    ''' DoNotSummarizeProperties  Cleared;  
    ''' SpreadsheetShowAllFields = Nothing((Master Or (AdministratorAndGlobal)=True) Else False); 
    ''' GridTitle = ""; 
    ''' ReportTitle = ""; 
    ''' ReadOnly = False; 
    ''' CbosDoNotLoad Cleared; 
    ''' ColumnsReadOnly Cleared;  
    ''' ColumnsHide Cleared;  
    ''' ColumnsFormat Cleared;  
    ''' ColumnsOrdinalPosition Cleared;  
    ''' ColumnsAlignment Cleared;  
    ''' ColumnsHeaderText Cleared;  
    ''' ColumnsListHide = False 
    ''' SpreadsheetButtonHide = False 
    ''' ReportHide = False 
    ''' NavigationBarHide = False 
    ''' IsSumFillOnTheFly = False 
    ''' </summary> 
    ''' <remarks></remarks> 
    Public Sub New() 
      _SummarizeGrid = True 
      _DoNotSummarizeProperties = New List(Of csLoggedJob.enmSummarizeableProperty) 
      _SpreadsheetShowAllFields = Nothing 
      _GridTitle = "" 
      _ReportTitle = "" 
      _ReadOnly = False 
      _CbosDoNotLoad = New List(Of csLoggedJob.enmParentProperty) 
      _ColumnsReadOnly = New List(Of csLoggedJob.enmProperty) 
      _ColumnsHide = New List(Of csLoggedJob.enmProperty) 
      _ColumnsFormat = New Dictionary(Of csLoggedJob.enmProperty, String) 
      _ColumnsOrdinalPosition = New Dictionary(Of csLoggedJob.enmProperty, Integer) 
      _ColumnsAlignment = New Dictionary(Of csLoggedJob.enmProperty, DataGridViewContentAlignment) 
      _ColumnsHeaderText = New Dictionary(Of csLoggedJob.enmProperty, String) 
      _ColumnsListHide = False 
      _SpreadsheetButtonHide = False 
      _ReportButtonHide = False 
      _ImportButtonHide = False 
      _AddEditDeleteButtonsHide = False 
      _NavigationBarHide = False 
      _IsSumFillOnTheFly = False 
      _TruncateStrings = True 
      _SearchFilters = New Dictionary(Of System.Enum, Object) 
    End Sub 
  End Class 
 
  Private WithEvents _LoggedJobCol As csLoggedJobCol
  Private WithEvents _LoggedJobColFullLength As csLoggedJobCol
 
  Private _DVGDirty As Boolean
 
  Private _Loading As Boolean = True
  Private _Report As vbReport.ReportDocument
  Private _Summarized As Boolean 

  Private _SummaryOverFlow As String 

  Private _IgnoreGridFault As Boolean 

  Private _GridSettings As clsGridSettingCol 

  Private _SortList As List(Of Integer) 
  Private _AutoSorting As Boolean = False 
  Private _PrevSortColumn As DataGridViewColumn = Nothing 
 
  'ctl_Load 
  Private Sub ctl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 
    If Me.DesignMode = True Then Exit Sub 
 
  End Sub 
 
  'Properties 
  Public ReadOnly Property [SelectedLoggedJob]() As csLoggedJob 
    Get 
      If dgvLoggedJob.SelectedRows.Count = 0 OrElse _Loading = True Then Return Nothing 
      Dim RowIndex As Integer = dgvLoggedJob.SelectedRows(0).Cells(0).RowIndex 
      If RowIndex < 0 Then Return Nothing 
      If _Summarized = True AndAlso RowIndex = dgvLoggedJob.Rows.Count - 1 Then dgvLoggedJob.ClearSelection() : RaiseEvent evtUnChosen() : Return Nothing 
      Return _LoggedJobCol(RowIndex) 
    End Get 
  End Property 
  
  Public ReadOnly Property [LoggedJobCol]() As csLoggedJobCol 
    Get 
      Return _LoggedJobCol 
    End Get 
  End Property 
 
  Public Function LoadControl(ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    Dim pLoggedJobCol As New csLoggedJobCol(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    pFault = pLoggedJobCol.Fill(_Requester) 
    If pFault.isOK = False Then Return pFault 
 
    pFault = LoadControl(pLoggedJobCol)
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByVal vUniqueCode As Object, ByVal vParentObjectType As String, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pLoggedJobCol As New csLoggedJobCol(clsEnums.enmLoadParent.EntireObject) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    Dim pDoIt As Boolean = False 
    If vUniqueCode.GetType().Name.Equals("string", StringComparison.OrdinalIgnoreCase) Then 
      If Not String.IsNullOrEmpty(vUniqueCode.ToString()) Then 
        pDoIt = True 
      End If 
    Else 
      If ccHelper.ToLong(vUniqueCode) <> 0 Then pDoIt = True 
    End If 
 
    If pDoIt Then 
      Select Case vParentObjectType 
        Case "Job" 
          pFault = pLoggedJobCol.FillByJobID(ccHelper.ToLong(vUniqueCode), _Requester) 
        Case "LoggedAlert" 
          pFault = pLoggedJobCol.FillByLoggedAlertID(ccHelper.ToLong(vUniqueCode), _Requester) 
        Case Else 
          Throw New Exception("Invalid vParentObjectType '" & vParentObjectType & "' received ") 
      End Select 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pLoggedJobCol) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(vLoggedJobCol As csLoggedJobCol, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    Return LoadControl(vLoggedJobCol) 
  End Function
  
  Private Function LoadControl(vLoggedJobCol As csLoggedJobCol) As clsFault
    Dim pFault As New clsFault
 
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
    'Use evtBeforeLoad to set or remove the list type, if you don't want the default 
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList  
    RaiseEvent evtBeforeLoad() 
 
    LoadLocalizedText() 
 
    'keep safe in case 
    Dim pColumnsHides As List(Of csLoggedJob.enmProperty) = Nothing 
    If _LoadParameters.IsSumFillOnTheFly Then 
      pColumnsHides = New List(Of csLoggedJob.enmProperty) 
      pColumnsHides.AddRange(_LoadParameters.ColumnsHide) 
    End If 
 
    'Force blg and clc fields to read-only 
    
    'Check for ReadOnly columns 
    For Each l In _LoadParameters.ColumnsReadOnly 
      If l = csLoggedJob.enmProperty.ID Then colID.ReadOnly = True 
      If l = csLoggedJob.enmProperty.Job Then colJob.ReadOnly = True 
      If l = csLoggedJob.enmProperty.WhenStarted Then colWhenStarted.ReadOnly = True 
      If l = csLoggedJob.enmProperty.ActivatingUser Then colActivatingUser.ReadOnly = True 
      If l = csLoggedJob.enmProperty.LastRunBy Then colLastRunBy.ReadOnly = True 
      If l = csLoggedJob.enmProperty.ExecutionTimeSec Then colExecutionTimeSec.ReadOnly = True 
      If l = csLoggedJob.enmProperty.RunStatus Then colRunStatus.ReadOnly = True 
      If l = csLoggedJob.enmProperty.Remarks Then colRemarks.ReadOnly = True 
      If l = csLoggedJob.enmProperty.LoggedAlert Then colLoggedAlert.ReadOnly = True 
      If l = csLoggedJob.enmProperty.SuccessCount Then colSuccessCount.ReadOnly = True 
      If l = csLoggedJob.enmProperty.FailureCount Then colFailureCount.ReadOnly = True 
    Next 
 
    For Each l In _LoadParameters.ColumnsHide 
      'Parents only 
      Dim pParentProperty As csLoggedJob.enmParentProperty = csLoggedJob.enmParentProperty.UD 
      Dim pSuccess As Boolean = [Enum].TryParse(Of csLoggedJob.enmParentProperty)(l.ToString(), ignoreCase:=False, pParentProperty) 
      If pSuccess = False Then Continue For 
      If Not _LoadParameters.CbosDoNotLoad.Contains(pParentProperty) Then 
        _LoadParameters.CbosDoNotLoad.Add(pParentProperty) 
      End If 
    Next 
 
    If _LoadParameters.IsSumFillOnTheFly Then 
      'Use what we just save instead 
      _LoadParameters.ColumnsHide = pColumnsHides 
    End If 
 
    dgvLoggedJob.DoubleBuffered(True) 
 
    pFault = vLoggedJobCol.LoadLookupAndEnumText(_Requester) : If Not pFault.isOK Then Return pFault 
    
    'Now transfer to local collection 
    _LoggedJobColFullLength = vLoggedJobCol.Clone() 
 
    'Truncate the strings 
    _LoggedJobCol = vLoggedJobCol 
    If _LoadParameters.TruncateStrings Then 
      _LoggedJobCol.TruncateStrings() 
    Else 
      dgvLoggedJob.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
      dgvLoggedJob.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders 
    End If 
 
    ' If you switch between ReadOnly and not Readonly, it causes problems
    Static sReadOnlyHandled As Boolean = False 
    If sReadOnlyHandled = False Then 
      If _LoadParameters.ReadOnly = True Then 
        colJob.Name = colJob.Name & "zzzz" 
        colJobText.Name = colJob.Name.Replace("zzzz", "") 
        If colJob.DataGridView IsNot Nothing Then dgvLoggedJob.Columns.Remove(colJob) 
        colLoggedAlert.Name = colLoggedAlert.Name & "zzzz" 
        colLoggedAlertText.Name = colLoggedAlert.Name.Replace("zzzz", "") 
        If colLoggedAlert.DataGridView IsNot Nothing Then dgvLoggedJob.Columns.Remove(colLoggedAlert) 
      Else 
        If colJob.ReadOnly = False Then 
          If colJobText.DataGridView IsNot Nothing Then dgvLoggedJob.Columns.Remove(colJobText) 
        Else 
          colJob.Name = colJob.Name & "zzzz" 
          colJobText.Name = colJob.Name.Replace("zzzz", "") 
          If colJob.DataGridView IsNot Nothing Then dgvLoggedJob.Columns.Remove(colJob) 
          If Not _LoadParameters.CbosDoNotLoad.Contains(csLoggedJob.enmParentProperty.Job) Then 
            _LoadParameters.CbosDoNotLoad.Add(csLoggedJob.enmParentProperty.Job) 
          End If 
        End If 
        If colLoggedAlert.ReadOnly = False Then 
          If colLoggedAlertText.DataGridView IsNot Nothing Then dgvLoggedJob.Columns.Remove(colLoggedAlertText) 
        Else 
          colLoggedAlert.Name = colLoggedAlert.Name & "zzzz" 
          colLoggedAlertText.Name = colLoggedAlert.Name.Replace("zzzz", "") 
          If colLoggedAlert.DataGridView IsNot Nothing Then dgvLoggedJob.Columns.Remove(colLoggedAlert) 
          If Not _LoadParameters.CbosDoNotLoad.Contains(csLoggedJob.enmParentProperty.LoggedAlert) Then 
            _LoadParameters.CbosDoNotLoad.Add(csLoggedJob.enmParentProperty.LoggedAlert) 
          End If 
        End If 
      End If 
      sReadOnlyHandled = True 
    End If 
    If _LoadParameters.ReadOnly = False Then 
      'Load ComboListCache 
      If Not _LoadParameters.CbosDoNotLoad.Contains(csLoggedJob.enmParentProperty.Job) Then 
        MyCache.SetLevel(clsEnums.enmComboListType.c_JobDefaultByID, Cache.enmLevel.Previous) 
      End If 
      If Not _LoadParameters.CbosDoNotLoad.Contains(csLoggedJob.enmParentProperty.LoggedAlert) Then 
        MyCache.SetLevel(clsEnums.enmComboListType.c_LoggedAlertDefaultByID, Cache.enmLevel.Previous) 
      End If 
    End If 
 
    _SummaryOverFlow = "#" 
 
    Dim pHiddenColumnNames As New List(Of String) 
    For Each l In _LoadParameters.ColumnsHide 
      pHiddenColumnNames.Add("col" & l.ToString()) 
    Next 
    For Each lCol As DataGridViewColumn In dgvLoggedJob.Columns 
      If lCol.Visible = False AndAlso Not pHiddenColumnNames.Contains(lCol.Name) Then lCol.Visible = True 
    Next 
    For Each p As ToolStripMenuItem In btnColumns.DropDownItems 
      If p Is mnuColsReset OrElse p Is mnuColsHideMost Then Continue For 
      If p.Checked = False Then p.Checked = True 
      If p.Visible = False Then p.Visible = True 
    Next 
 
    'Load GridSettings 
    pFault = GetOrInitializeGridSettings() : If pFault.isOK = False Then Return pFault 
 
    For Each l In _GridSettings 
      l.ColumnRemoved = False 
    Next 
 
    'Hide columns  
    For Each p As csLoggedJob.enmProperty In _LoadParameters.ColumnsHide 
      Dim pGridSetting As clsGridSetting = _GridSettings.FindByColumnName("col" & p.ToString()) 
      'HideColumn(p.ToString) 
      pGridSetting.ColumnRemoved = True 
    Next 
     
    'Set Header Text 
    For Each pD In _LoadParameters.ColumnsHeaderText 
      dgvLoggedJob.Columns("col" & pD.Key.ToString).HeaderText = pD.Value 
    Next 
 
    'Format Columns 
    For Each pD In _LoadParameters.ColumnsFormat 
      dgvLoggedJob.Columns("col" & pD.Key.ToString).DefaultCellStyle.Format = pD.Value 
    Next 
 
    'ordinal position 
    For Each pD In _LoadParameters.ColumnsOrdinalPosition 
      dgvLoggedJob.Columns("col" & pD.Key.ToString).DisplayIndex = pD.Value 
    Next 
    _GridSettings.Update(Me, _Requester)
    
    'Align Columns 
    For Each pD In _LoadParameters.ColumnsAlignment 
      dgvLoggedJob.Columns("col" & pD.Key.ToString).DefaultCellStyle.Alignment = pD.Value 
    Next 
 
    'Hide ColumnList 
    If _LoadParameters.ColumnsListHide = True Then 
      btnColumns.Visible = False 
    End If 
 
    'SpreadsheetButtonHide  
    If _LoadParameters.SpreadsheetButtonHide = True Then 
      btnSpreadsheet.Visible = False 
    End If 
 
    'ReportButtonHide  
    If _LoadParameters.ReportButtonHide = True Then 
      btnReport.Visible = False 
    End If 
 
    'ImportButtonHide  
    If _LoadParameters.ImportButtonHide = True Then 
      'btnImport.Visible = False 
      'handled in SetUpBNButtons 
    End If 
 
    'NavigationBarHide  
    If _LoadParameters.NavigationBarHide = True Then 
      BN.Visible = False 
    End If 
 
    dgvLoggedJob.ClearSelection()
    bsCtlLoggedJob.DataSource = Nothing 
    
    pFault = LoadSupportingCombos() : If pFault.isOK = False Then Return pFault 
 
    lblGrid.Text = _LoadParameters.GridTitle 
    If lblGrid.Text = "" Then 
      'Assume chkAutoRefresh is not used either. (may have to add it to LoadParameters) 
      pnlHeader.Visible = False 
    End If 
    Try
      LoadGrid()
    Catch ex As Exception
      Return pFault.LogException(ex, "LoadGrid", "TRGT-LoggedJob-090124-2345", _Requester) 
    End Try
    
    RaiseEvent evtLoaded() 
    
    'Show row count in status label 
    lblStatus.ForeColor = Color.DarkGreen 
    lblStatus.Text = dgvLoggedJob.RowCount & " rows" 
    
    'now do the default sorts 
    If _SortList IsNot Nothing Then 
      _AutoSorting = True 
      _PrevSortColumn = Nothing 
      For Each i In _SortList 
        Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(i, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0)) 
        dgvLoggedJob_ColumnHeaderMouseClick(Me, pE) 
      Next 
      _AutoSorting = False 
    End If 
 
    If _LoadParameters.IsSumFillOnTheFly Then 
      chkAutoRefresh.Visible = False 
      chkAutoRefresh.Checked = False 
      btnEdit.Visible = False 
    End If 
 
    pFault = _GridSettings.Update(Me, _Requester) : If pFault.isOK = False Then Return pFault  
 
    chkAutoRefresh.BackColor = pnlHeader.BackColor 
 
    Return pFault 
  End Function

  Private Sub LoadGrid()
    Dim pFault As New clsFault

    Dim pRowIndex As Integer = -1 
    If dgvLoggedJob.SelectedRows.Count > 0 Then 
      pRowIndex = dgvLoggedJob.SelectedRows(0).Cells(0).RowIndex 
    Else 
      If dgvLoggedJob.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
        pRowIndex = dgvLoggedJob.CurrentCellAddress.Y 
      End If 
    End If 
 
    If _LoadParameters.SummarizeGrid = True Then Summarize() 
 
    _Loading = True 
 
    bsCtlLoggedJob.DataSource = Nothing 
    bsCtlLoggedJob.DataSource = _LoggedJobCol
    
    dgvLoggedJob.ClearSelection() 
    
    RaiseEvent evtUnChosen()
    
    SetUpBNButtons(False)
    'set columns 
    LoadColumns() 
 
    'Load buttons 
    For Each p As ToolStripMenuItem In btnColumns.DropDownItems 
      If p Is mnuColsReset OrElse p Is mnuColsHideMost Then Continue For 
      Dim pMenuItemProprty As String = p.Name.Substring(13) 
      p.Checked = dgvLoggedJob.Columns("col" & pMenuItemProprty).Visible 
    Next 
 
    If pRowIndex >= 0 Then 
      If _Summarized = True Then 
        If pRowIndex <= _LoggedJobCol.Count - 2 Then 
          dgvLoggedJob.Rows(pRowIndex).Selected = True 
        End If 
      Else 
        If pRowIndex <= _LoggedJobCol.Count - 1 Then 
          dgvLoggedJob.Rows(pRowIndex).Selected = True 
        End If 
      End If 
    End If 
 
    _Loading = False
  End Sub

  'In the future, rearrange this to only be called once.  
  Private _LoadedCombos As Boolean = False 
   
  'Load the comboboxes to be displayed
  Private Function LoadSupportingCombos() As clsFault
    Dim pFault As New clsFault 
    If _LoadedCombos = True Then Return pFault.SetOK() 
    
    Dim pComboList As clsComboList
    Dim pPrompt As String 
    Dim pChoose As String = GetChoose(_Requester) 
    Dim pEnumCol As clsComboList = Nothing 
    'Load comboLists 
    'Job
    If _LoadParameters.ReadOnly = False AndAlso _LoadParameters.CbosDoNotLoad.Find(Function(p) p = csLoggedJob.enmParentProperty.Job) = csLoggedJob.enmParentProperty.UD Then 
      'enable using an external list if needed 
      pComboList = Nothing 
      pPrompt = "" 
      Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_JobDefaultByID 
      Dim pParentID As Long = 0 
      RaiseEvent evtOverrideLoadCbo(csLoggedJob.enmParentProperty.Job, pComboListTypeToLoad, pParentID, pComboList, pPrompt) 
      If pComboList Is Nothing Then 
        pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList) : If Not pFault.isOK() Then Return pFault 
        If MyCache.GetLevel(pComboListTypeToLoad) = Cache.enmLevel.AlwaysPageFromServer Then 
          Return pFault.LogFreeTextFault($"In {Me.Name}, {pComboListTypeToLoad.FastToString()} is defined as AlwaysPageFromServer. Either change it to AlwaysCache in evtBeforeLoad, make the column read-only, or make this grid read-only", "", "TRGT-LoggedJobCol-200806-1015", _Requester) 
        End If 
      End If 
      pComboList = pComboList.Clone() 
      If pPrompt = "" Then pPrompt = pChoose 
        pComboList.AddToTop(ccHelper.ToLong(0), pPrompt) 
      bsJob.DataSource = pComboList 
      colJob.Tag = pPrompt 
    End If 

    'EnumRunStatus
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csLoggedJob.enmParentProperty.RunStatus, Nothing, Nothing, pEnumCol, pPrompt) 
    If pEnumCol Is Nothing Then 
      pEnumCol = New clsComboList 
      pFault = pEnumCol.FillEnums(clsEnums.enmEnum.JobStatus, _Requester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      pFault.SetOK() 
    End If 
    pEnumCol.Remove(pEnumCol.FindByKey(clsEnums.enmJobStatus.UD)) 
    pEnumCol.SortByText() 
    If pPrompt = "" Then 
      pPrompt = pChoose 
    End If 
    pEnumCol.AddToTop(clsEnums.enmJobStatus.UD, pPrompt) 
    bsRunStatus.DataSource = pEnumCol 
    colRunStatus.Tag = pPrompt 

    'LoggedAlert
    If _LoadParameters.ReadOnly = False AndAlso _LoadParameters.CbosDoNotLoad.Find(Function(p) p = csLoggedJob.enmParentProperty.LoggedAlert) = csLoggedJob.enmParentProperty.UD Then 
      'enable using an external list if needed 
      pComboList = Nothing 
      pPrompt = "" 
      Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_LoggedAlertDefaultByID 
      Dim pParentID As Long = 0 
      RaiseEvent evtOverrideLoadCbo(csLoggedJob.enmParentProperty.LoggedAlert, pComboListTypeToLoad, pParentID, pComboList, pPrompt) 
      If pComboList Is Nothing Then 
        pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList) : If Not pFault.isOK() Then Return pFault 
        If MyCache.GetLevel(pComboListTypeToLoad) = Cache.enmLevel.AlwaysPageFromServer Then 
          Return pFault.LogFreeTextFault($"In {Me.Name}, {pComboListTypeToLoad.FastToString()} is defined as AlwaysPageFromServer. Either change it to AlwaysCache in evtBeforeLoad, make the column read-only, or make this grid read-only", "", "TRGT-LoggedJobCol-200806-1015", _Requester) 
        End If 
      End If 
      pComboList = pComboList.Clone() 
      If pPrompt = "" Then pPrompt = pChoose 
        pComboList.AddToTop(ccHelper.ToLong(0), pPrompt) 
      bsLoggedAlert.DataSource = pComboList 
      colLoggedAlert.Tag = pPrompt 
    End If 

    _LoadedCombos = True 
     
    If pFault.Number = 0 Then pFault.SetOK() 'Haven't loaded any parameters 
    Return pFault
  End Function


  'Buttons
  Private Sub SetUpBNButtons(ByVal vInEdit As Boolean)
    btnEdit.Visible = False 
    btnImport.Visible = False 
    btnCeaseEdit.Visible = False 
    dgvLoggedJob.EditMode = DataGridViewEditMode.EditProgrammatically 
    dgvLoggedJob.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
    dgvLoggedJob.AllowUserToDeleteRows = False 
    dgvLoggedJob.AllowUserToAddRows = False 
    If _LoggedJobCol.Count = 0 Then 
      btnSpreadsheet.Enabled = False 
      btnReport.Enabled = False 
    Else 
      btnSpreadsheet.Enabled = True 
      btnReport.Enabled = True 
    End If 
    lblEditMode.Text = "" 
    tssReports.Visible = False 
    lblStatus.Text = "" 
    dgvLoggedJob.Refresh() 
  End Sub
  'ExternalButtons 
  'CellFormatting  
  Private Sub dgvLoggedJob_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvLoggedJob.CellFormatting 
    '_Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    If dgvLoggedJob.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      If dgvLoggedJob.Columns(e.ColumnIndex).ReadOnly = False Then 
        Exit Sub 
      End If 
    End If 
 
    RaiseEvent evtCellFormatting(sender, e) 
 
    ' Sample code evtCellFormatting - evtCellFormatting 
    ' You can use this to colour the fonts or your cell background or anything else that requires complete control of your cell 
    'Dim pLoggedJob As csLoggedJob = Nothing 
    'If dgvLoggedJob.Columns(e.ColumnIndex).Name = colRecommendedQuantityToOrder.Name Then 
    '  If pLoggedJob Is Nothing Then pLoggedJob = CType(dgvLoggedJob.Rows(e.RowIndex).DataBoundItem, csLoggedJob) ' Only assign it if needed 
    '  If pLoggedJob.CustomerOrders > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pLoggedJob.CustomerOrders > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
    'If dgvLoggedJob.Columns(e.ColumnIndex).Name = colRAV.Name Then 
    '  If pLoggedJob Is Nothing Then pLoggedJob = CType(dgvLoggedJob.Rows(e.RowIndex).DataBoundItem, csLoggedJob) ' Only assign it if needed
    '  If pLoggedJob.RAV > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pLoggedJob.RAV - pLoggedJob.MaximumStock > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
 
    'Debug.Print("loc x,y:" & e.RowIndex & ", " & e.ColumnIndex & ": GetType" & dgvLoggedJob.Columns(e.ColumnIndex).GetType.ToString & ": zValue" & e.Value.ToString) 
    If dgvLoggedJob.Columns(e.ColumnIndex).GetType.ToString = "System.Windows.Forms.DataGridViewComboBoxColumn" Then 
      Dim pCol As System.Windows.Forms.DataGridViewComboBoxColumn = CType(dgvLoggedJob.Columns(e.ColumnIndex), System.Windows.Forms.DataGridViewComboBoxColumn) 
      Dim pTag As String = "" 
      If pCol.Tag Is Nothing Then 
        e.Value = "* NoCombo *" 
        e.CellStyle.ForeColor = Color.Gray 
      Else 
        pTag = pCol.Tag.ToString 
      End If 
      If pTag <> "" Then 
        If e.Value Is Nothing Then 
          'e.Value = "## INVALID ##" 
          e.Value = "* BadCode '" & dgvLoggedJob.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & "' *" 
          e.CellStyle.ForeColor = Color.Tomato 
          e.CellStyle.BackColor = Color.LightYellow 
        Else 
          If e.Value.ToString = pTag Then 
            e.Value = "" 
          End If 
        End If 
      End If 
    End If 
 
    'If e.ColumnIndex = 0 Then 
    '  Dim pID As Long = ccHelper.ToLong(e.Value) 
    '  If pID Mod 2 = 0 Then 
    '    e.CellStyle.BackColor = Color.Yellow 
    '  End If 
    'End If 
 
    If e.Value IsNot Nothing Then 
      If e.Value.ToString = "." Then 
        e.Value = "" 
      End If 
    End If 
 
    If e.Value?.GetType.Name.Equals("datetime", StringComparison.OrdinalIgnoreCase) Then 
      Dim pDate As Date = CDate(e.Value) 
      If Math.Abs(pDate.Subtract(DateTime.MinValue).TotalDays) < 5 OrElse Math.Abs(pDate.Subtract(DateTime.MaxValue).TotalDays) < 5 Then 
        e.Value = "" 
      End If 
    End If 
 
    If e.Value?.GetType.Name.Equals("datetimeoffset", StringComparison.OrdinalIgnoreCase) Then 
      Dim pDate As DateTimeOffset = CType(e.Value, DateTimeOffset) 
      e.Value = FormattedDateTimeOffsetFromGridCellStyle(e.CellStyle.Format, pDate) 
    End If 
 
    If dgvLoggedJob.Columns(e.ColumnIndex).GetType.Name.Equals("DataGridViewImageColumn", StringComparison.OrdinalIgnoreCase) Then 
      If e.Value Is Nothing Then 
        e.Value = New Bitmap(1, 1) 
      End If 
    End If 
 
    If _Summarized = True Then 
      If e.RowIndex = dgvLoggedJob.Rows.Count - 1 Then 
        If e.Value IsNot Nothing Then 
          If e.Value.GetType.Name = "DateTime" Then 
            If CDate(e.Value) = #12:00:00 AM# Then 
              e.Value = "" 
            End If 
          ElseIf e.Value.GetType.Name = "String" Then 
            e.Value = "" 
          ElseIf e.Value.ToString = "-1" Then 
            e.Value = "" 
          ElseIf e.Value.ToString = "UD" Then 
            e.Value = "" 
          ElseIf e.Value.ToString = "0" Then 
            e.Value = "" 
          Else 
            If _SummaryOverFlow.IndexOf(dgvLoggedJob.Columns(e.ColumnIndex).Name.Substring(3)) >= 0 Then 
              e.Value = "* OVERFLOW *" 
            End If 
          End If 
        Else 
          e.Value = "" 
        End If 
        e.CellStyle.ForeColor = Color.White 
        e.CellStyle.BackColor = Color.Gray 
      End If 
    End If 
  End Sub 
  Private Sub dgvLoggedJob_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLoggedJob.CellValueChanged 
    If e.RowIndex < 0 Then Exit Sub 
 
  End Sub 
 
  'Grid Sort
  Private Sub dgvLoggedJob_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLoggedJob.ColumnHeaderMouseClick
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewColumn As DataGridViewColumn = dgvLoggedJob.Columns(e.ColumnIndex)
    If bsCtlLoggedJob.Current Is Nothing Then Exit Sub

    Static pPrevSortOrder As System.Windows.Forms.SortOrder = SortOrder.None
    Dim pNewSortOrder As System.Windows.Forms.SortOrder

    If chkAutoRefresh.Checked = True AndAlso e.Button = MouseButtons.XButton1 Then 
      Exit Sub 
    End If 
 
    If chkAutoRefresh.Checked = True AndAlso e.Button <> MouseButtons.XButton2 AndAlso e.RowIndex = -1 Then 
      frmMessageOrInputBox.ShowMsg("Cancel Auto-Fresh", frmMessageOrInputBox.enmIconType.Exclamation) 
      Exit Sub 
    End If 
 
    Cursor = Cursors.WaitCursor
    dgvLoggedJob.SuspendLayout()

    Dim pLoggedJob As csLoggedJob
    Dim pID As Long = 0 
    If dgvLoggedJob.SelectedRows.Count > 0 Then 
    pLoggedJob = CType(bsCtlLoggedJob.Current, csLoggedJob)
      pID = pLoggedJob.ID 
    End If 

    If _AutoSorting = False Then 
      If _SortList Is Nothing Then _SortList = New List(Of Integer) 
      _SortList.Add(e.ColumnIndex) 
    End If 
 
    ' If _PrevSortColumn is null, then the DataGridView is not currently sorted.
    If _PrevSortColumn Is Nothing Then 
      pPrevSortOrder = SortOrder.None 
    End If 
 
    If _PrevSortColumn IsNot Nothing Then
      ' Sort the same column again, reversing the SortOrder.
      If _PrevSortColumn Is pNewColumn AndAlso pPrevSortOrder = SortOrder.Ascending Then
        pNewSortOrder = SortOrder.Descending
      Else
        ' Sort a new column and remove the old SortGlyph.
        pNewSortOrder = SortOrder.Ascending
        _PrevSortColumn.HeaderCell.SortGlyphDirection = SortOrder.None
      End If
    Else
      pNewSortOrder = SortOrder.Ascending
    End If

    ' Sort the selected column.
    Dim pLoggedJobCol As csLoggedJobCol
    pLoggedJobCol = CType(bsCtlLoggedJob.DataSource, csLoggedJobCol)

    Dim pSummaryRow As csLoggedJob = Nothing 
    If _Summarized = True Then 
      pSummaryRow = pLoggedJobCol(pLoggedJobCol.Count - 1) 
      pLoggedJobCol.RemoveAt(pLoggedJobCol.Count - 1) 
    End If 
 
    If pNewSortOrder = SortOrder.Ascending Then
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
        'save the present sort 
        For iCntr As Integer = 0 To pLoggedJobCol.Count - 1 
          pLoggedJobCol(iCntr).Tag = iCntr.ToString("0000000000") 
        Next 
      End If 
      If pNewColumn Is colID Then
        pLoggedJobCol.SortByID()
      ElseIf pNewColumn Is colJob OrElse pNewColumn Is colJobText Then
        pLoggedJobCol.SortByJobText()
      ElseIf pNewColumn Is colWhenStarted Then
        pLoggedJobCol.SortByWhenStarted()
      ElseIf pNewColumn Is colActivatingUser Then
        pLoggedJobCol.SortByActivatingUser()
      ElseIf pNewColumn Is colLastRunBy Then
        pLoggedJobCol.SortByLastRunBy()
      ElseIf pNewColumn Is colExecutionTimeSec Then
        pLoggedJobCol.SortByExecutionTimeSec()
      ElseIf pNewColumn Is colRunStatus Then
        pLoggedJobCol.SortByRunStatus()
      ElseIf pNewColumn Is colRemarks Then
        pLoggedJobCol.SortByRemarks()
      ElseIf pNewColumn Is colLoggedAlert OrElse pNewColumn Is colLoggedAlertText Then
        pLoggedJobCol.SortByLoggedAlertText()
      ElseIf pNewColumn Is colSuccessCount Then
        pLoggedJobCol.SortBySuccessCount()
      ElseIf pNewColumn Is colFailureCount Then
        pLoggedJobCol.SortByFailureCount()
      End If
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
      Dim iCntr As Integer = 0 
        If pNewColumn Is colID Then
          Dim pTest As Long = 0 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.ID <> pTest Then iCntr += 1 : pTest = p.ID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colJob OrElse pNewColumn Is colJobText Then
          Dim pTest As String = "" 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.JobText <> pTest Then iCntr += 1 : pTest = p.JobText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colWhenStarted Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.WhenStarted <> pTest Then iCntr += 1 : pTest = p.WhenStarted 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colActivatingUser Then
          Dim pTest As String = "" 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.ActivatingUser <> pTest Then iCntr += 1 : pTest = p.ActivatingUser 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLastRunBy Then
          Dim pTest As String = "" 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.LastRunBy <> pTest Then iCntr += 1 : pTest = p.LastRunBy 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colExecutionTimeSec Then
          Dim pTest As Integer = 0 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.ExecutionTimeSec <> pTest Then iCntr += 1 : pTest = p.ExecutionTimeSec 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colRunStatus Then
          Dim pTest As clsEnums.enmJobStatus = clsEnums.enmJobStatus.UD 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.RunStatus <> pTest Then iCntr += 1 : pTest = p.RunStatus 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colRemarks Then
          Dim pTest As String = "" 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.Remarks <> pTest Then iCntr += 1 : pTest = p.Remarks 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLoggedAlert OrElse pNewColumn Is colLoggedAlertText Then
          Dim pTest As String = "" 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.LoggedAlertText <> pTest Then iCntr += 1 : pTest = p.LoggedAlertText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colSuccessCount Then
          Dim pTest As Integer = 0 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.SuccessCount <> pTest Then iCntr += 1 : pTest = p.SuccessCount 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFailureCount Then
          Dim pTest As Integer = 0 
          For Each p As csLoggedJob In pLoggedJobCol 
            If p.FailureCount <> pTest Then iCntr += 1 : pTest = p.FailureCount 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        End If 
        pLoggedJobCol.SortByTag() 
      End If 
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
      _PrevSortColumn = pNewColumn
      pPrevSortOrder = SortOrder.Ascending
    Else
      pLoggedJobCol.Reverse()
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
      pPrevSortOrder = SortOrder.Descending
    End If

    If _Summarized = True Then 
      pLoggedJobCol.Add(pSummaryRow) 
    End If 
 
    If pID > 0 Then
      bsCtlLoggedJob.Position = bsCtlLoggedJob.IndexOf(pLoggedJobCol.FindByID(pID))
    End If

    'dgvLoggedJob.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
    dgvLoggedJob.ResumeLayout()

    Cursor = Cursors.Default
    dgvLoggedJob.Refresh()

  End Sub
  'Select Row 
  Public Sub SelectRowByObjectID(ByVal pID As Long) 
    If pID > 0 Then 
      Dim pLoggedJobCol As csLoggedJobCol 
      pLoggedJobCol = CType(bsCtlLoggedJob.DataSource, csLoggedJobCol) 
      Dim pLoggedJob As csLoggedJob = pLoggedJobCol.FindByID(pID) 
      If Not pLoggedJob.IsEmpty Then 
        bsCtlLoggedJob.Position = bsCtlLoggedJob.IndexOf(pLoggedJobCol.FindByID(pID)) 
        dgvLoggedJob.Rows(bsCtlLoggedJob.Position).Selected = True 
      Else 
        dgvLoggedJob.ClearSelection() 
      End If 
    ElseIf pID = 0 Then 
      dgvLoggedJob.ClearSelection() 
    End If 
  End Sub 
  
  'Grid Resize
  Private Sub dgvLoggedJob_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLoggedJob.ColumnHeaderMouseDoubleClick
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    dgvLoggedJob.AutoResizeColumn(e.ColumnIndex)
    Cursor = Cursors.Default
  End Sub
  'Other Grid Events
  Private Sub dgvLoggedJob_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLoggedJob.CurrentCellDirtyStateChanged
   _DVGDirty = True 
  End Sub
  Private Sub dgvLoggedJob_Scroll(sender As Object, e As ScrollEventArgs) Handles dgvLoggedJob.Scroll
    dgvLoggedJob.Invalidate() 
  End Sub
 
  Private Sub dgvLoggedJob_DataFault(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvLoggedJob.DataError
    'Dim pFault As New clsFault
    '
    'If e.RowIndex = dgvLoggedJob.Rows.Count - 1 Then Exit Sub
 
    'If dgvLoggedJob.Columns(e.ColumnIndex).Name.StartsWith("colIDin", StringComparison.OrdinalIgnoreCase) Then Exit Sub 
 
    'If _IgnoreGridFault = True Then Exit Sub
    '_DVGDirty = False 
    'Static pShown As Boolean 
    '
    'Dim pSubStrg As New System.Text.StringBuilder 
    ''Other Error 
    'Try 
    '  Try 
    '    pSubStrg.AppendLine("In table 'LoggedJob', the row with an ID of " & dgvLoggedJob.Rows(e.RowIndex).Cells(0).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine("In grid 'LoggedJob', row index " & e.RowIndex) 
    '  End Try 
    '  Try 
    '    pSubStrg.AppendLine(" has an invalid value of " & dgvLoggedJob.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine(" has an invalid value of Nothing.") 
    '  End Try 
    '  pSubStrg.AppendLine(" in column " & dgvLoggedJob.Columns(e.ColumnIndex).DataPropertyName) 
    'Catch ex As Exception 
    '  pSubStrg.AppendLine("; Failed trying to fill DataFault as well!") 
    'End Try 
    'pFault.LogException(209, e.Exception, pSubStrg.ToString, "TRGT-LoggedJob-100409-2248", _Requester) 
    'If pShown = False Then 
    '  Dim pCell As DataGridViewCell 
    '  Try 
    '    pCell = dgvLoggedJob(e.ColumnIndex, e.RowIndex)
    '  Catch ex As Exception 
    '    pCell = dgvLoggedJob(0, 0)
    '  End Try 
    '  ShowFault(pFault, _Requester) 
    '  pShown = True 
    'End If 
  End Sub
  Private Sub dgvLoggedJob_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvLoggedJob.ColumnWidthChanged
    If Me.DesignMode = True Then Exit Sub 
    If _Loading = False Then SaveSizes()
  End Sub
  Private Sub dgvLoggedJob_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvLoggedJob.ColumnDisplayIndexChanged
    Cursor = Cursors.WaitCursor
    If _Loading = False Then SaveSizes()
    Cursor = Cursors.Default
  End Sub
  Private Sub dgvLoggedJob_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLoggedJob.CellDoubleClick 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso e.RowIndex = dgvLoggedJob.Rows.Count - 1 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
    Dim pCell As DataGridViewCell = dgvLoggedJob(e.ColumnIndex, e.RowIndex) 
 
    Dim pHandled As Boolean = False 
    Dim pLoggedJob As csLoggedJob = _LoggedJobCol(e.RowIndex)
    RaiseEvent evtRowDoubleClicked(pLoggedJob, pHandled) 
    Cursor = Cursors.Default 
 
    If pHandled = False Then 
      If Me.ParentForm.Name.Equals("frmPopup", StringComparison.OrdinalIgnoreCase) Then Return 
      frmPopup.Text = "LoggedJob Detail" 
      Dim pFault As clsFault = frmPopup.LoadControl("ctlc_LoggedJob", pLoggedJob, _Requester) 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
      frmPopup.ShowDialog() 
    End If 
 
  End Sub 
  Private Sub dgvLoggedJob_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLoggedJob.SelectionChanged 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If _Loading = True Then Exit Sub 
    If DateTime.Now < _IgnoreSelectionUntil Then Exit Sub
    If _ProcessingSelection Then Exit Sub
    If dgvLoggedJob.SelectedRows.Count = 0 Then 
      RaiseEvent evtUnChosen() 
      Exit Sub 
    End If 
    Dim RowIndex As Integer = dgvLoggedJob.SelectedRows(0).Cells(0).RowIndex 
    If RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso RowIndex = dgvLoggedJob.Rows.Count - 1 Then dgvLoggedJob.ClearSelection() : RaiseEvent evtUnChosen() : Exit Sub 
    Dim pLoggedJob As csLoggedJob = _LoggedJobCol(RowIndex)
    _ProcessingSelection = True
    Try
      RaiseEvent evtRowClicked(pLoggedJob) 
    Finally
      _IgnoreSelectionUntil = DateTime.Now.AddMilliseconds(500)
      _ProcessingSelection = False
    End Try
  End Sub 
  Private Sub dgvLoggedJob_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLoggedJob.RowLeave 
    colID.ReadOnly = True 
  End Sub 
  Private Sub ReleaseStuckModifierKeys()
    If (Control.ModifierKeys And Keys.Shift) = Keys.Shift Then
      If (GetAsyncKeyState(VK_SHIFT) And &H8000) = 0 Then
        keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0)
      End If
    End If
    If (Control.ModifierKeys And Keys.Control) = Keys.Control Then
      If (GetAsyncKeyState(VK_CONTROL) And &H8000) = 0 Then
        keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0)
      End If
    End If
  End Sub


  'Calculations
  Private Sub SaveSizes() 
    ' Save column state data  
    ' including order, column width and whether or not the column is visible  
    For Each pCol As DataGridViewColumn In dgvLoggedJob.Columns 
      Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
      pG.ColumnDisplayIndex = pCol.DisplayIndex 
      pG.ColumnWidth = pCol.Width 
      If pG.ColumnRemoved = False Then 
        pG.ColumnVisible = pCol.Visible 
      Else 
        pG.ColumnVisible = True 
      End If 
      If pG.ColumnName = "" Then 
        pG.ColumnName = pCol.Name 
        _GridSettings.Add(pG) 
      End If 
    Next 
 
    Dim pFault As clsFault 
    pFault = _GridSettings.Update(Me, _Requester) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
 
  End Sub 
 
  Private Sub Summarize() 
    If _LoggedJobCol.Count <= 1 Then 
      _Summarized = False 
      Exit Sub 
    End If 
 
    Dim pExecutionTimeSec As Integer 
    Dim pSuccessCount As Integer 
    Dim pFailureCount As Integer 
    For Each pExistingRow As csLoggedJob In _LoggedJobCol 
      If _SummaryOverFlow.IndexOf("#ExecutionTimeSec#") < 0 Then 
        Try 
          pExecutionTimeSec += pExistingRow.ExecutionTimeSec 
        Catch ex As System.OverflowException 
          pExecutionTimeSec = -99999999 
          _SummaryOverFlow &= "ExecutionTimeSec#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#SuccessCount#") < 0 Then 
        Try 
          pSuccessCount += pExistingRow.SuccessCount 
        Catch ex As System.OverflowException 
          pSuccessCount = -99999999 
          _SummaryOverFlow &= "SuccessCount#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#FailureCount#") < 0 Then 
        Try 
          pFailureCount += pExistingRow.FailureCount 
        Catch ex As System.OverflowException 
          pFailureCount = -99999999 
          _SummaryOverFlow &= "FailureCount#" 
        End Try 
      End If 
    Next 
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csLoggedJob.enmSummarizeableProperty.ExecutionTimeSec) = csLoggedJob.enmSummarizeableProperty.ExecutionTimeSec Then pExecutionTimeSec = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csLoggedJob.enmSummarizeableProperty.SuccessCount) = csLoggedJob.enmSummarizeableProperty.SuccessCount Then pSuccessCount = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csLoggedJob.enmSummarizeableProperty.FailureCount) = csLoggedJob.enmSummarizeableProperty.FailureCount Then pFailureCount = 0
    Dim pSummaryRow As New csLoggedJob( _ 
        vID:=0 _ 
      , vJobID:=0 _ 
      , vJobText:="" _ 
      , vWhenStarted:=Nothing _ 
      , vActivatingUser:="" _ 
      , vLastRunBy:="" _ 
      , vExecutionTimeSec:=pExecutionTimeSec _ 
      , vRunStatus:=clsEnums.enmJobStatus.UD _ 
      , vRunStatusText:="" _ 
      , vRemarks:="" _ 
      , vLoggedAlertID:=0 _ 
      , vLoggedAlertText:="" _ 
      , vSuccessCount:=pSuccessCount _ 
      , vFailureCount:=pFailureCount _ 
      , vTag:="" _ 
      , vDateAdded:=Nothing _ 
      , vWithParents:=clsEnums.enmLoadParent.TextOnly _ 
      )
    _LoggedJobCol.Add(pSummaryRow) 
    _Summarized = True 
  End Sub 
  
  'Reports and Excel 
  Friend Function CreateSpreadSheet() As clsFault  
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name  
    Dim pFault As New clsFault  
    'Dim pExcel As New Tools.ExcelSheet  
    Dim pDateToShow As String = DateTime.Now.ToString("yyMMdd_HHmmss")  
    Dim pRoot As String = $"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles" 
 
    Dim pFileNameAllFields As String = $"{pRoot}\LoggedJobCol_{pDateToShow}AllFields.csv" 
    Dim pFileNameFieldsOnGrid As String = $"{pRoot}\LoggedJobCol_{pDateToShow}FieldsOnGrid.csv" 
    Dim pFileNameAllFieldsWithIDs As String = $"{pRoot}\LoggedJobCol_{pDateToShow}AllFieldsWithIDs.csv" 
    Dim pFileNameAllFieldsXML As String = $"{pRoot}\LoggedJobCol_{pDateToShow}AllFields.xml" 
    Dim pFileNameAllFieldsJson As String = $"{pRoot}\LoggedJobCol_{pDateToShow}AllFields.json" 
 
    'clear out the folder  
    If Not System.IO.Directory.Exists($"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles") Then 
      System.IO.Directory.CreateDirectory($"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles") 
    End If 
 
    Dim pFilePaths As String() = System.IO.Directory.GetFiles($"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles") 
    For Each l In pFilePaths 
      Try 
        System.IO.File.Delete(l) 
      Catch ex As Exception 
      End Try 
    Next 
 
    Dim pCSV As New System.Text.StringBuilder 
    Dim pTmpStrg As New System.Text.StringBuilder 
 
    Dim pShowAllFields As Boolean = False  
    If _Requester.IsInRole("Master") OrElse _Requester.IsInRole("ApplicationMaster") Then  
      pShowAllFields = True  
    ElseIf (_Requester.IsInRole("Administrator") OrElse _Requester.IsInRole("SysAdmin")) AndAlso _Requester.UserIdentityType = clsEnums.enmUserIdentityType.Global Then  
      pShowAllFields = True  
    Else  
      If _LoadParameters.SpreadsheetShowAllFields IsNot Nothing Then  
        pShowAllFields = CBool(_LoadParameters.SpreadsheetShowAllFields)  
      End If  
    End If  
  
    'Get the titles  
    pTmpStrg = New System.Text.StringBuilder 
    For Each pCol As DataGridViewColumn In dgvLoggedJob.Columns 
      If pCol.Visible = True Then 
        pTmpStrg.Append(",""" & pCol.HeaderText & """") 
      End If 
    Next 
    pCSV.AppendLine(pTmpStrg.ToString.Substring(1)) 
 
    'Now the data  
    Dim i As Integer 
    Dim pStart As Date = Now 
 
    Dim pTruncatedFieldNames As String = "" 
    For Each Row As DataGridViewRow In dgvLoggedJob.Rows 
      i += 1 
      If _LoadParameters.SummarizeGrid = True Then 
        If Row.Index = dgvLoggedJob.Rows.Count - 1 Then Exit For 
      End If 
      If i Mod 500 = 0 Then 
        lblStatus.Text = " Writing Row " & i & ". Time Elapsed: " & DateTime.Now.Subtract(pStart).TotalSeconds().ToString("###0") & " sec" : Application.DoEvents() 
      End If 
      pTmpStrg = New System.Text.StringBuilder 
       
      For Each pCol As DataGridViewColumn In dgvLoggedJob.Columns 
        Dim pCell As DataGridViewCell = Row.Cells(pCol.Name) 
        Dim pCellValueType As String = pCell.ValueType.Name 
        If pCol.Visible = True Then 
          Dim pFormattedValue As String = pCell.FormattedValue.ToString 
          If pFormattedValue.EndsWith(" ~~~") Then 
            If Not pTruncatedFieldNames.Contains($" {pCol.Name}, ") Then pTruncatedFieldNames &= $" {pCol.Name}, " 
          End If 
          Dim pStrg As String = "" 
          If pCellValueType.Equals("string", StringComparison.OrdinalIgnoreCase) Then 
            pStrg = $",""{ccHelper.StringForCSV(pFormattedValue)}""" 
          Else 
            If ccHelper.IsNumeric(pFormattedValue) Then 
              pFormattedValue = ccHelper.ToDecimal(pFormattedValue).ToString() 
              pStrg = $",""{pFormattedValue}""" 
            Else 
              pStrg = $",""{ccHelper.StringForCSV(pFormattedValue)}""" 
            End If 
          End If 
          pTmpStrg.Append(pStrg) 
        End If 
      Next 
      pCSV.AppendLine(pTmpStrg.ToString.Substring(1)) 
    Next 
 
    If Not String.IsNullOrWhiteSpace(pTruncatedFieldNames) Then 
      Dim pCount As Integer = pTruncatedFieldNames.Count(Function(c As Char) c = ","c) 
 
      Dim pFields As String = pTruncatedFieldNames.Replace(" col", "").Replace(" ", "").Replace(",", ", ") 
      pFields = pFields.Substring(0, pFields.Length - 2) 
 
      Dim pVerb As String = "has" 
      If pCount > 1 Then 
        pVerb = "have" 
 
        pFields = pFields.Substring(0, pFields.LastIndexOf(",")) + " &&" + pFields.Substring(pFields.LastIndexOf(",") + 1) 
      End If 
 
      frmMessageOrInputBox.ShowMsg($"{pFields} {pVerb} truncated values.{Environment.NewLine}If you need complete strings, then choose a report other than 'FieldsOnGrid'", frmMessageOrInputBox.enmIconType.Exclamation) 
    End If 
 
    Try  
      If pShowAllFields = True Then  
        Dim pStrg As String = "" 
        'xml 
        pFault = _LoggedJobColFullLength.CreateXML(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsXML, pStrg, False) 
        'json 
        pFault = _LoggedJobColFullLength.CreateJSON(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsJson, pStrg, False) 
        'default  
        My.Computer.FileSystem.WriteAllText(pFileNameAllFields, _LoggedJobColFullLength.ToCSV, False)  
        'WithIDs  
        'pFault = _LoggedJobColFullLength.LoadLookupAndEnumText(_Requester) : If pFault.isOK = False Then Return pFault (already done) 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsWithIDs, _LoggedJobColFullLength.ToCSV(True), False) 
      End If  
      'default  
      My.Computer.FileSystem.WriteAllText(pFileNameFieldsOnGrid, pCSV.ToString, False)  
      pFault.SetOK()  
    Catch ex As Exception  
      pFault.LogException(ex, "", "TRGT-LoggedJob-090210-1618", _Requester)  
    End Try  
    If pFault.isOK = False Then Return pFault  
  
    frmMessageOrInputBox.ShowMsg("Succeeded! Seconds elapsed: " & DateTime.Now.Subtract(pStart).TotalSeconds().ToString("#,##0"), frmMessageOrInputBox.enmIconType.Information) 
 
    lblStatus.Text = "" 
 
    Try  
      If pShowAllFields = True Then  
        Shell("explorer.exe /n,/select," & pFileNameAllFieldsWithIDs, AppWinStyle.NormalFocus, False)  
      Else  
        Shell("explorer.exe /n,/select," & pFileNameFieldsOnGrid, AppWinStyle.NormalFocus, False)  
      End If  
    Catch ex As Exception  
      pFault.LogException(ex, "", "TRGT-LoggedJob-090210-1618", _Requester)  
    End Try  
  
    If pFault.isOK = False Then Return pFault  
    
    Return pFault  
  End Function  
 
  Private Sub ReportDesign() 
 
    _Report = New vbReport.ReportDocument 
    _Report.AutoDiscover = False 
    Try 
      For Each pCol As DataGridViewColumn In dgvLoggedJob.Columns 
        If pCol.Visible = True Then 
          'Handle DataGridViewComboBoxColumn in vbReport DLL 
          If pCol.ValueType.Name = "String" Then 
            _Report.Columns.Add(pCol.DataPropertyName, pCol.HeaderText, pCol.DefaultCellStyle.Format, vbReport.CellTextJustification.Near) 
          ElseIf pCol.ValueType.Name = "Boolean" Then 
            _Report.Columns.Add(pCol.DataPropertyName, pCol.HeaderText, pCol.DefaultCellStyle.Format, vbReport.CellTextJustification.Centered) 
          Else 
            If pCol.CellType.Name = "DataGridViewComboBoxCell" Then 
              'this means it's a foreign key 
              Dim pFieldName As String = pCol.DataPropertyName 
              If pFieldName.EndsWith("ID") Then 
                pFieldName = pFieldName.Substring(0, pFieldName.Length - 2) & "Text" 
              End If  
              _Report.Columns.Add(pFieldName, pCol.HeaderText, pCol.DefaultCellStyle.Format, vbReport.CellTextJustification.Near) 
            Else 
              _Report.Columns.Add(pCol.DataPropertyName, pCol.HeaderText, pCol.DefaultCellStyle.Format, vbReport.CellTextJustification.Far) 
            End If 
          End If 
        End If 
      Next 
      With _Report 
        If _Requester.UILang = clsEnums.enmLanguage.he Then 
          .RTL = True 
        End If 
        If _LoadParameters.ReportTitle = "" Then .Title = "Report" Else .Title = _LoadParameters.ReportTitle 
        .SubTitleLeft = "LoggedJobs" 
        .SubTitleRight = "Rows: " & _LoggedJobCol.Count.ToString 
        .FooterLeft = "Printed on" 
        .FooterLeft &= " " & FormatDateTime(Now, DateFormat.LongDate) & " " & FormatDateTime(Now, DateFormat.LongTime) 
        .FooterRight = "" 
        .Font = New Font("Arial", 10) 
        .DefaultPageSettings = New System.Drawing.Printing.PageSettings 
        .DefaultPageSettings.Landscape = True 
        '.DefaultPageSettings.Landscape = False 
        If .DefaultPageSettings.Landscape = True Then 
          .Columns.SetEvenSpacing(.DefaultPageSettings.PaperSize.Height - .DefaultPageSettings.Margins.Top - .DefaultPageSettings.Margins.Bottom) 
        Else 
          .Columns.SetEvenSpacing(.DefaultPageSettings.PaperSize.Width - .DefaultPageSettings.Margins.Left - .DefaultPageSettings.Margins.Right) 
        End If 
        .DataSource = _LoggedJobCol 
        .HasSummaryLine = _Summarized 
      End With 
    Catch ex As Exception 
      Dim pFault As New clsFault 
      pFault.LogException(ex, "", "TRGT-LoggedJob-090210-2119", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
    End Try 
  End Sub 
  Friend Function CreateReport() As clsFault 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
 
    ReportDesign()
    Try 
      Dim dlg As New PrintPreviewDialog 
      dlg.Document = _Report 
      dlg.WindowState = FormWindowState.Maximized 
      dlg.ShowDialog() 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(ex, "", "TRGT-LoggedJob-090211-0746", _Requester) 
    End Try 
    Return pFault 
  End Function 
 
  Private Sub btnSpreadsheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpreadsheet.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    Dim pOverriden As Boolean = False 
    RaiseEvent evtOverrideSpreadsheet(pOverriden) 
    If pOverriden = True Then Exit Sub 
 
    Cursor = Cursors.WaitCursor 
    pFault = CreateSpreadSheet() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
  Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    Cursor = Cursors.WaitCursor 
    pFault = CreateReport() 
    Cursor = Cursors.Default 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Search filter 
  Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged 
    If _Loading Then Exit Sub 
    If txtSearch.ForeColor = Color.Gray Then Exit Sub 'watermark 
    Dim pSearchText As String = txtSearch.Text.Trim().ToLower() 
    If String.IsNullOrEmpty(pSearchText) Then 
      For Each pRow As DataGridViewRow In dgvLoggedJob.Rows 
        Try : pRow.Visible = True : Catch : End Try 
      Next 
      lblStatus.ForeColor = Color.DarkGreen 
      lblStatus.Text = dgvLoggedJob.RowCount & " rows" 
      Exit Sub 
    End If 
    ' Hide rows that don't match search text 
    For Each row As DataGridViewRow In dgvLoggedJob.Rows 
      Dim pVisible As Boolean = False 
      For Each cell As DataGridViewCell In row.Cells 
        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(pSearchText) Then 
          pVisible = True : Exit For 
        End If 
      Next 
      Try 
        Dim pBS As CurrencyManager = CType(Me.BindingContext(bsCtlLoggedJob), CurrencyManager) 
        row.Visible = pVisible 
      Catch : End Try 
    Next 
    Dim pVisibleCount As Integer = 0 
    For Each row As DataGridViewRow In dgvLoggedJob.Rows 
      If row.Visible Then pVisibleCount += 1 
    Next 
    lblStatus.ForeColor = Color.DarkBlue 
    lblStatus.Text = pVisibleCount & " of " & dgvLoggedJob.RowCount & " rows" 
  End Sub 
 
  'Search watermark (PlaceholderText not available in .NET Framework) 
  Private Sub txtSearch_GotFocus(sender As Object, e As EventArgs) Handles txtSearch.GotFocus 
    If txtSearch.Text = "Search..." AndAlso txtSearch.ForeColor = Color.Gray Then 
      txtSearch.Text = "" 
      txtSearch.ForeColor = SystemColors.WindowText 
    End If 
  End Sub 
 
  Private Sub txtSearch_LostFocus(sender As Object, e As EventArgs) Handles txtSearch.LostFocus 
    If String.IsNullOrEmpty(txtSearch.Text) Then 
      txtSearch.ForeColor = Color.Gray 
      txtSearch.Text = "Search..." 
    End If 
  End Sub 
 
  'CSV Export 
  'Design 
  Private Function GetOrInitializeGridSettings() As clsFault 
    Dim pFault As New clsFault 
 
    Try 
      _GridSettings = clsGridSettingCol.GetGridSettings(Me, _Requester, pFault) 
    Catch ex As Exception 
      pFault.LogException(ex, "GetOrInitializeGridSettings", "TRGT-LoggedJob-120225-1310", _Requester) 
    End Try 
    If pFault.isOK = False Then Return pFault 
 
    Dim pSaveInitial As Boolean = False 
    
    '_GridSettings.Clear() Use for testing 
    If _GridSettings.Count = 0 Then 
      For Each pCol As DataGridViewColumn In dgvLoggedJob.Columns 
        Dim pG As New clsGridSetting 
        pG.ColumnDisplayIndex = pCol.DisplayIndex 
        pG.ColumnWidth = 5 
        pG.ColumnRemoved = False 
        pG.ColumnVisible = True 
        pG.ColumnName = pCol.Name 
        _GridSettings.Add(pG) 
      Next 
      pSaveInitial = True 
    Else 
      'Remove non-existent columns 
      For Each pG As clsGridSetting In _GridSettings 
        pG.Tag = "" 
      Next 
      For Each pCol As DataGridViewColumn In dgvLoggedJob.Columns 
        Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
        If pG.ColumnName = "" Then 
          pG.ColumnDisplayIndex = pCol.DisplayIndex 
          pG.ColumnWidth = ccHelper.ToInteger((dgvLoggedJob.Width - 30) / dgvLoggedJob.Columns.Count) 
          If pG.ColumnWidth < 60 Then pG.ColumnWidth = 60 
          pG.ColumnRemoved = False 
          pG.ColumnVisible = True 
          pG.ColumnName = pCol.Name 
          _GridSettings.Add(pG) 
          pSaveInitial = True 
        End If 
        pG.Tag = "Found" 
      Next 
      For Each pG As clsGridSetting In _GridSettings.Clone() 
        If pG.Tag = "" Then 
          _GridSettings.Remove(_GridSettings.FindByColumnName(pG.ColumnName)) 
          pSaveInitial = True 
        End If 
      Next 
    End If 
 
    If pSaveInitial = True Then 
      pFault = _GridSettings.Update(Me, _Requester) : If pFault.isOK = False Then Return pFault 
      'Refresh the GridSettings (need ID) 
      Try 
        _GridSettings = clsGridSettingCol.GetGridSettings(Me, _Requester, pFault) 
      Catch ex As Exception 
        pFault.LogException(ex, "GetOrInitializeGridSettings", "TRGT-Permission-120225-1310", _Requester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  Private Sub LoadColumns() 
    Dim pFault As New clsFault 
    
    'Set Wrap 
    'colRemarks.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    
    Try 
      For Each lGridSetting In _GridSettings 
        Try 
          Dim pToolStripMenuItem As ToolStripMenuItem = Nothing 
          For Each lToolStripMenuItem As ToolStripMenuItem In btnColumns.DropDownItems 
            If lToolStripMenuItem Is mnuColsReset OrElse lToolStripMenuItem Is mnuColsHideMost Then Continue For 
            If lToolStripMenuItem.Name.Substring(13) = lGridSetting.ColumnName.Substring(3) Then 
              pToolStripMenuItem = lToolStripMenuItem 
              Exit For 
            End If 
          Next 
          If pToolStripMenuItem Is Nothing Then Continue For 
           
          With dgvLoggedJob.Columns(lGridSetting.ColumnName) 
            '.DisplayIndex = lGridSetting.ColumnDisplayIndex 
            If lGridSetting.ColumnRemoved = True Then 
              .Visible = False 
              pToolStripMenuItem.Visible = False 
            Else 
              pToolStripMenuItem.Visible = True 
              If lGridSetting.ColumnVisible = False Then 
                .Visible = False 
                pToolStripMenuItem.Checked = False 
              Else 
                .Visible = True 
                pToolStripMenuItem.Checked = True 
              End If 
            End If 
            .Width = lGridSetting.ColumnWidth 
          End With 
        Catch ex As Exception 
        End Try 
      Next 
    Catch ex As Exception 'Fault in getting GridSettings  
      pFault.LogException(204, ex, "", "TRGT-LoggedJob-090120-1502", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
    End Try 
  End Sub 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "ID", _Requester) 
    If pStrg <> "" Then colID.HeaderText = pStrg : mnuColVisibleID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "Job", _Requester) 
    If pStrg <> "" Then colJob.HeaderText = pStrg : mnuColVisibleJob.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "WhenStarted", _Requester) 
    If pStrg <> "" Then colWhenStarted.HeaderText = pStrg : mnuColVisibleWhenStarted.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "ActivatingUser", _Requester) 
    If pStrg <> "" Then colActivatingUser.HeaderText = pStrg : mnuColVisibleActivatingUser.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "LastRunBy", _Requester) 
    If pStrg <> "" Then colLastRunBy.HeaderText = pStrg : mnuColVisibleLastRunBy.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "ExecutionTimeSec", _Requester) 
    If pStrg <> "" Then colExecutionTimeSec.HeaderText = pStrg : mnuColVisibleExecutionTimeSec.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "RunStatus", _Requester) 
    If pStrg <> "" Then colRunStatus.HeaderText = pStrg : mnuColVisibleRunStatus.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "Remarks", _Requester) 
    If pStrg <> "" Then colRemarks.HeaderText = pStrg : mnuColVisibleRemarks.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "LoggedAlert", _Requester) 
    If pStrg <> "" Then colLoggedAlert.HeaderText = pStrg : mnuColVisibleLoggedAlert.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "SuccessCount", _Requester) 
    If pStrg <> "" Then colSuccessCount.HeaderText = pStrg : mnuColVisibleSuccessCount.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedJob", "FailureCount", _Requester) 
    If pStrg <> "" Then colFailureCount.HeaderText = pStrg : mnuColVisibleFailureCount.Text = pStrg
 
    For Each p As ToolStripItem In BN.Items 
      If p.GetType().Name = "ToolStripButton" Then 
        Dim pbtn As ToolStripButton = CType(p, ToolStripButton) 
        pStrg = CCTextTranslate(pbtn.Text.Replace("&", ""), _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      ElseIf p.GetType().Name = "ToolStripDropDownButton" Then 
        Dim pbtn As ToolStripDropDownButton = CType(p, ToolStripDropDownButton) 
        pStrg = CCTextTranslate(pbtn.Text.Replace("&", ""), _Requester) 
        If pStrg <> "" Then pbtn.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Private Sub mnuColVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColVisibleID.Click, mnuColVisibleJob.Click, mnuColVisibleWhenStarted.Click, mnuColVisibleActivatingUser.Click, mnuColVisibleLastRunBy.Click, mnuColVisibleExecutionTimeSec.Click, mnuColVisibleRunStatus.Click, mnuColVisibleRemarks.Click, mnuColVisibleLoggedAlert.Click, mnuColVisibleSuccessCount.Click, mnuColVisibleFailureCount.Click
    Cursor = Cursors.WaitCursor 
    Dim pToolStripItem As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem) 
    dgvLoggedJob.Columns("col" & pToolStripItem.Name.Substring(13)).Visible = pToolStripItem.Checked 
    If _Loading = False Then SaveSizes() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsReset.Click 
    Cursor = Cursors.WaitCursor 
    dgvLoggedJob.SuspendLayout() 
 
    For Each pCol As DataGridViewColumn In dgvLoggedJob.Columns 
      Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
      pG.ColumnDisplayIndex = pCol.Index 
    Next 
 
    Dim pVisibleColumns As Integer = 0 
    For Each p In _GridSettings 
      p.ColumnVisible = True 
      If p.ColumnRemoved = False Then 
        pVisibleColumns += 1 
      End If 
    Next 
    Dim pNewWidth As Integer = 0 
    pNewWidth = ccHelper.ToInteger((dgvLoggedJob.Width - 30) / pVisibleColumns) 
    If pNewWidth < 60 Then pNewWidth = 60 
    For Each p In _GridSettings 
      p.ColumnWidth = pNewWidth 
    Next 
 
    Dim pFault As clsFault 
    pFault = _GridSettings.Update(Me, _Requester)  
    If pFault.isOK = False Then ShowFault(pFault, _Requester)  
 
    _Loading = True 
    LoadColumns() 
    _Loading = False 
     
    dgvLoggedJob.ResumeLayout() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsHideMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsHideMost.Click 
 
    _Loading = True 
    'Hide All 
    If mnuColVisibleID.Checked = True Then mnuColVisibleID.PerformClick() 
    If mnuColVisibleJob.Checked = True Then mnuColVisibleJob.PerformClick() 
    If mnuColVisibleWhenStarted.Checked = True Then mnuColVisibleWhenStarted.PerformClick() 
    If mnuColVisibleActivatingUser.Checked = True Then mnuColVisibleActivatingUser.PerformClick() 
    If mnuColVisibleLastRunBy.Checked = True Then mnuColVisibleLastRunBy.PerformClick() 
    If mnuColVisibleExecutionTimeSec.Checked = True Then mnuColVisibleExecutionTimeSec.PerformClick() 
    If mnuColVisibleRunStatus.Checked = True Then mnuColVisibleRunStatus.PerformClick() 
    If mnuColVisibleRemarks.Checked = True Then mnuColVisibleRemarks.PerformClick() 
    If mnuColVisibleLoggedAlert.Checked = True Then mnuColVisibleLoggedAlert.PerformClick() 
    If mnuColVisibleSuccessCount.Checked = True Then mnuColVisibleSuccessCount.PerformClick() 
    If mnuColVisibleFailureCount.Checked = True Then mnuColVisibleFailureCount.PerformClick() 
    
    _Loading = False 
    'dgvLoggedJob.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
  End Sub 
  
  Private Sub dgvLoggedJob_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvLoggedJob.CellMouseClick 
    If e.Button = MouseButtons.Right Then 
      Dim pMessageBox As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the LoggedJob to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pMessageBox <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pLoggedJob As csLoggedJob = _LoggedJobCol(e.RowIndex) 
        If pMessageBox = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pLoggedJob.ToCSV) 
        Else 
          Clipboard.SetText(pLoggedJob.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The LoggedJob is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvLoggedJob_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvLoggedJob.MouseDown 
    '--- Save anchor on normal click (no modifiers) ---
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And (Keys.Shift Or Keys.Control)) = 0 AndAlso dgvLoggedJob.CurrentRow IsNot Nothing Then 
      _SelectionAnchor = dgvLoggedJob.CurrentRow.Index 
    End If 
    'This removes on click from the update 
    If dgvLoggedJob.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
 
    Dim pCell As DataGridView.HitTestInfo = dgvLoggedJob.HitTest(e.X, e.Y) 
 
    If pCell.Type = DataGridViewHitTestType.Cell Then 
      'Enable edit force it to be current 
      Dim pCurrentCell As DataGridViewCell = Nothing 
      Try 
        pCurrentCell = dgvLoggedJob(pCell.ColumnIndex, pCell.RowIndex) 
        If pCurrentCell.ReadOnly Then Exit Sub 
        dgvLoggedJob.CurrentCell = pCurrentCell 
      Catch ex As Exception 
        Exit Sub  
      End Try 
      'make the combobox drop down if it's active 
      If pCurrentCell.GetType().Name.Equals("DataGridViewComboBoxCell", StringComparison.OrdinalIgnoreCase) Then 
        dgvLoggedJob.BeginEdit(True) 
        DirectCast(dgvLoggedJob.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvLoggedJob_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvLoggedJob.MouseUp 
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And Keys.Shift) = Keys.Shift AndAlso _SelectionAnchor >= 0 Then 
      Dim hit = dgvLoggedJob.HitTest(e.X, e.Y) 
      If hit.RowIndex >= 0 Then 
        dgvLoggedJob.MultiSelect = True 
        dgvLoggedJob.ClearSelection() 
        Dim pFrom As Integer = Math.Min(_SelectionAnchor, hit.RowIndex) 
        Dim pTo As Integer = Math.Max(_SelectionAnchor, hit.RowIndex) 
        For i As Integer = pFrom To pTo 
          dgvLoggedJob.Rows(i).Selected = True 
        Next 
      End If 
    ElseIf (pModifiers And Keys.Control) = Keys.Control Then 
      dgvLoggedJob.MultiSelect = True 
    Else 
      dgvLoggedJob.MultiSelect = False 
    End If 
  End Sub 
 
  Private Sub chkAutoRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoRefresh.CheckedChanged 
    If chkAutoRefresh.Checked Then 
      _PrevSortColumn = Nothing 
 
      Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(colID.Index, -1, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton2, 0, 0, 0, 0)) 
      dgvLoggedJob_ColumnHeaderMouseClick(Me, pE) 
      Application.DoEvents() 
      dgvLoggedJob_ColumnHeaderMouseClick(Me, pE) 
      Application.DoEvents() 
 
      Timer = New Timer 
      Timer.Interval = _TimerIntervalMs 
      Timer.Start() 
    Else 
      If Timer IsNot Nothing Then 
        Timer.Stop() 
      End If 
    End If 
  End Sub 
 
  Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick 
    RaiseEvent evtTimerTripped() 
  End Sub 
 
  Private Sub ctlc_LoggedJobCol_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
    'Set the font for the BN 
        If MyFont Is Nothing Then Return 
    BN.Font = New Font(MyFont.Name, MyFont.Size) 
    dgvLoggedJob.RowTemplate.Height = ccHelper.ToInteger(23 * MyFont.Size / 9) 
  End Sub 
 
  Private Sub ctlc_LoggedJobCol_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    If Me.ParentForm Is Nothing Then Exit Sub 
    Dim pParent As String = Me.ParentForm.Name 
    Dim pResponse As Boolean = Me.Visible 
    Dim pSize As Integer = dgvLoggedJob.Width 
 
    'now set sizes if needed 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_LoggedJobCol_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True AndAlso Not Me.Parent.Name.StartsWith("pnl", StringComparison.OrdinalIgnoreCase) Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  'Track open detail windows to prevent duplicates 
  Private Shared _openDetailWindows As New Dictionary(Of String, Form)() 
 
  'Context menu - right-click: add to selection if not already selected, otherwise keep multi-selection 
  Private Sub dgvLoggedJob_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvLoggedJob.CellMouseDown 
    ReleaseStuckModifierKeys() 'Fix sticky SHIFT/CTRL before selection changes 
    If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then 
      If Not dgvLoggedJob.Rows(e.RowIndex).Selected Then 
        dgvLoggedJob.ClearSelection() 
        dgvLoggedJob.Rows(e.RowIndex).Selected = True 
      End If 
    End If 
  End Sub 
 
  'Context menu - Opening: adjust items based on single/multi selection 
  Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening 
    Dim pCount As Integer = dgvLoggedJob.SelectedRows.Count 
    Dim pMulti As Boolean = (pCount > 1) 
    'Disable Open options when multiple rows selected 
    tsmiOpenDetail.Visible = Not pMulti 
    tsmiOpenInTab.Visible = Not pMulti 
    'Update Row/Rows text dynamically 
    tsmiCopyRow.Text = If(pMulti, "Copy Rows", "Copy Row") 
    tsmiCopyRowHeaders.Text = If(pMulti, "Copy Rows with Headers", "Copy Row with Headers") 
    tsmiCopyExcel.Text = If(pMulti, "Copy Rows for Excel", "Copy for Excel") 
  End Sub 
 
  'Context menu - Open in New Window (regular Form with Return to Tab bar) 
  Private Sub tsmiOpenDetail_Click(sender As Object, e As EventArgs) Handles tsmiOpenDetail.Click 
    If dgvLoggedJob.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvLoggedJob.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _LoggedJobCol.Count Then Exit Sub 
    Dim pLoggedJob As csLoggedJob = _LoggedJobCol(pRowIndex) 
    Dim pTitle As String = "LoggedJob #" & pLoggedJob.ID.ToString() 
    Dim pKey As String = "LoggedJob_" & pLoggedJob.ID.ToString() 
    'Check if already open in a window - if so, bring to front 
    If _openDetailWindows.ContainsKey(pKey) Then 
      Dim pExisting As Form = _openDetailWindows(pKey) 
      If pExisting IsNot Nothing AndAlso Not pExisting.IsDisposed Then 
        pExisting.BringToFront() 
        pExisting.Focus() 
        Return 
      Else 
        _openDetailWindows.Remove(pKey) 
      End If 
    End If 
    'Check if already open in a tab - if so, switch to it 
    Dim pFrmMainCheck As frmMain = Nothing 
    For Each pF As Form In Application.OpenForms 
      If TypeOf pF Is frmMain Then pFrmMainCheck = CType(pF, frmMain) : Exit For 
    Next 
    If pFrmMainCheck IsNot Nothing AndAlso pFrmMainCheck.IsEntityOpenInTab(pTitle) Then Return 
    'Create regular form (same style as Pop Out) 
    Dim pForm As New Form() 
    pForm.Text = pTitle 
    pForm.Size = New Size(800, 600) 
    pForm.StartPosition = FormStartPosition.CenterScreen 
    pForm.Font = New Font("Segoe UI", 10, FontStyle.Regular) 
    'Return to Tab bar 
    Dim pTopPanel As New Panel() 
    pTopPanel.Dock = DockStyle.Top 
    pTopPanel.Height = 28 
    pTopPanel.BackColor = Color.FromArgb(70, 130, 180) 
    pTopPanel.Cursor = Cursors.Hand 
    Dim pReturnLabel As New Label() 
    pReturnLabel.Text = " ↩ Return to Tab" 
    pReturnLabel.ForeColor = Color.White 
    pReturnLabel.Font = New Font("Segoe UI", 9, FontStyle.Bold) 
    pReturnLabel.Dock = DockStyle.Fill 
    pReturnLabel.TextAlign = ContentAlignment.MiddleLeft 
    pReturnLabel.Cursor = Cursors.Hand 
    pTopPanel.Controls.Add(pReturnLabel) 
    'Load entity control via reflection 
    Dim pCtlName As String = "ctlc_LoggedJob" 
    Dim pAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly() 
    Dim pClassType As Type = pAssembly.GetType(Me.GetType().Namespace & "." & pCtlName) 
    If pClassType Is Nothing Then Exit Sub 
    Dim pControl As Control = CType(Activator.CreateInstance(pClassType), Control) 
    pControl.Dock = DockStyle.Fill 
    'Build form layout BEFORE loading - GridSetting needs Parent chain to reach a Form 
    Dim pContentPanel As New Panel() 
    pContentPanel.Dock = DockStyle.Fill 
    pContentPanel.Controls.Add(pControl) 
    pForm.Controls.Add(pContentPanel) 
    pForm.Controls.Add(pTopPanel) 
    'Load entity via reflection - control is now parented to pForm 
    Dim pLoad As System.Reflection.MethodInfo = pClassType.GetMethod("LoadControlForPopup") 
    If pLoad Is Nothing Then Exit Sub 
    Dim pFault As clsFault = CType(pLoad.Invoke(pControl, New Object() {pLoggedJob, _Requester}), clsFault) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    'Return to Tab click handler - sends entity to a new tab in frmMain 
    Dim pEntityRef As Object = pLoggedJob 
    Dim pSendToTab As Action = Sub() 
      Dim pFrmMain As frmMain = Nothing 
      For Each pF As Form In Application.OpenForms 
        If TypeOf pF Is frmMain Then pFrmMain = CType(pF, frmMain) : Exit For 
      Next 
      If pFrmMain IsNot Nothing Then 
        pFrmMain.OpenEntityInNewTab(pCtlName, pEntityRef, _Requester, pTitle) 
        pForm.Close() 
      End If 
    End Sub 
    AddHandler pTopPanel.Click, Sub(s, ev) pSendToTab() 
    AddHandler pReturnLabel.Click, Sub(s, ev) pSendToTab() 
    'Track window and show 
    _openDetailWindows(pKey) = pForm 
    AddHandler pForm.FormClosed, Sub(s, ev) _openDetailWindows.Remove(pKey) 
    pForm.Show() 
  End Sub 
 
  'Context menu - Open in New Tab (opens entity detail in a new tab in frmMain) 
  Private Sub tsmiOpenInTab_Click(sender As Object, e As EventArgs) Handles tsmiOpenInTab.Click 
    If dgvLoggedJob.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvLoggedJob.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _LoggedJobCol.Count Then Exit Sub 
    Dim pLoggedJob As csLoggedJob = _LoggedJobCol(pRowIndex) 
    Dim pFrmMain As frmMain = Nothing 
    For Each pForm As Form In Application.OpenForms 
      If TypeOf pForm Is frmMain Then 
        pFrmMain = CType(pForm, frmMain) 
        Exit For 
      End If 
    Next 
    If pFrmMain Is Nothing Then Exit Sub 
    'Check if already open in a window - if so, bring to front instead 
    Dim pWinKey As String = "LoggedJob_" & pLoggedJob.ID.ToString() 
    If _openDetailWindows.ContainsKey(pWinKey) Then 
      Dim pExisting As Form = _openDetailWindows(pWinKey) 
      If pExisting IsNot Nothing AndAlso Not pExisting.IsDisposed Then 
        pExisting.BringToFront() 
        pExisting.Focus() 
        Exit Sub 
      Else 
        _openDetailWindows.Remove(pWinKey) 
      End If 
    End If 
    Dim pTabTitle As String = "LoggedJob #" & pLoggedJob.ID.ToString() 
    Dim pFault As clsFault = pFrmMain.OpenEntityInNewTab("ctlc_LoggedJob", pLoggedJob, _Requester, pTabTitle) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Context menu - Copy ID (supports multi-select) 
  Private Sub tsmiCopyID_Click(sender As Object, e As EventArgs) Handles tsmiCopyID.Click 
    If dgvLoggedJob.SelectedRows.Count = 0 Then Exit Sub 
    Dim pIDs As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedJob.SelectedRows 
      If pSelectedRow.Index >= 0 AndAlso pSelectedRow.Index < _LoggedJobCol.Count Then 
        Dim pLoggedJob As csLoggedJob = _LoggedJobCol(pSelectedRow.Index) 
        If pIDs.Length > 0 Then pIDs.Append(", ") 
        pIDs.Append(pLoggedJob.ID.ToString()) 
      End If 
    Next 
    If pIDs.Length > 0 Then 
      Clipboard.SetText(pIDs.ToString()) 
      Dim pCount As Integer = dgvLoggedJob.SelectedRows.Count 
      ShowToast(If(pCount = 1, "ID copied: " & pIDs.ToString(), pCount.ToString() & " IDs copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows (supports multi-select, values only) 
  Private Sub tsmiCopyRow_Click(sender As Object, e As EventArgs) Handles tsmiCopyRow.Click 
    If dgvLoggedJob.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedJob.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvLoggedJob.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row copied", pCount.ToString() & " rows copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows with Headers (supports multi-select) 
  Private Sub tsmiCopyRowHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopyRowHeaders.Click 
    If dgvLoggedJob.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers from first row 
    Dim pFirstRow As DataGridViewRow = dgvLoggedJob.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add all selected rows 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedJob.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvLoggedJob.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row with headers copied", pCount.ToString() & " rows with headers copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy for Excel (with headers, VARCHAR fields wrapped in ="value" to preserve leading zeros) 
  Private Sub tsmiCopyExcel_Click(sender As Object, e As EventArgs) Handles tsmiCopyExcel.Click 
    If dgvLoggedJob.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers 
    Dim pFirstRow As DataGridViewRow = dgvLoggedJob.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add rows with Excel-safe formatting for text columns 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedJob.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then 
          Dim pVal As String = If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "") 
          If pCell.OwningColumn.ValueType Is GetType(String) AndAlso pVal.Length > 0 Then 
            pSB.Append("=""" & pVal.Replace("""", "'") & """") 
          Else 
            pSB.Append(pVal) 
          End If 
          pSB.Append(vbTab) 
        End If 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvLoggedJob.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Copied for Excel", pCount.ToString() & " rows copied for Excel")) 
    End If 
  End Sub 
 
  Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean 
    If keyData = Keys.F5 Then 
      Dim pBtn = Me.Controls.Find("btnRefresh", True).FirstOrDefault() 
      If pBtn IsNot Nothing Then DirectCast(pBtn, Button).PerformClick() 
      Return True 
    End If 
    Return MyBase.ProcessCmdKey(msg, keyData) 
  End Function 
  
End Class
