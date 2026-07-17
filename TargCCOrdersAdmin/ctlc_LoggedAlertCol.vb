Public Class ctlc_LoggedAlertCol
 
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
 
  
  Public Event evtRowClicked(ByVal vLoggedAlert As csLoggedAlert) 
  Public Event evtRowDoubleClicked(ByVal vLoggedAlert As csLoggedAlert, ByRef rHandled As Boolean) 
  Public Event evtUnChosen() 
 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csLoggedAlert.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  
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
    Public Property DoNotSummarizeProperties As List(Of csLoggedAlert.enmSummarizeableProperty) 
    Public Property SpreadsheetShowAllFields As Nullable(Of Boolean) 
    Public Property GridTitle As String 
    Public Property ReportTitle As String 
    Public Property [ReadOnly] As Boolean 
    Public Property CbosDoNotLoad As List(Of csLoggedAlert.enmParentProperty) 
    Public Property ColumnsReadOnly As List(Of csLoggedAlert.enmProperty) 
    Public Property ColumnsHide As List(Of csLoggedAlert.enmProperty) 
    Public Property ColumnsFormat As Dictionary(Of csLoggedAlert.enmProperty, String) 
    Public Property ColumnsOrdinalPosition As Dictionary(Of csLoggedAlert.enmProperty, Integer) 
    Public Property ColumnsAlignment As Dictionary(Of csLoggedAlert.enmProperty, DataGridViewContentAlignment) 
    Public Property ColumnsHeaderText As Dictionary(Of csLoggedAlert.enmProperty, String) 
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
      _DoNotSummarizeProperties = New List(Of csLoggedAlert.enmSummarizeableProperty) 
      _SpreadsheetShowAllFields = Nothing 
      _GridTitle = "" 
      _ReportTitle = "" 
      _ReadOnly = False 
      _CbosDoNotLoad = New List(Of csLoggedAlert.enmParentProperty) 
      _ColumnsReadOnly = New List(Of csLoggedAlert.enmProperty) 
      _ColumnsHide = New List(Of csLoggedAlert.enmProperty) 
      _ColumnsFormat = New Dictionary(Of csLoggedAlert.enmProperty, String) 
      _ColumnsOrdinalPosition = New Dictionary(Of csLoggedAlert.enmProperty, Integer) 
      _ColumnsAlignment = New Dictionary(Of csLoggedAlert.enmProperty, DataGridViewContentAlignment) 
      _ColumnsHeaderText = New Dictionary(Of csLoggedAlert.enmProperty, String) 
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
 
  Private WithEvents _LoggedAlertCol As csLoggedAlertCol
  Private WithEvents _LoggedAlertColFullLength As csLoggedAlertCol
 
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
 
  Private _UserIdentityTypeNames As Dictionary(Of String, clsComboList) 
  
  'ctl_Load 
  Private Sub ctl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 
    If Me.DesignMode = True Then Exit Sub 
 
  End Sub 
 
  'Properties 
  Public ReadOnly Property [SelectedLoggedAlert]() As csLoggedAlert 
    Get 
      If dgvLoggedAlert.SelectedRows.Count = 0 OrElse _Loading = True Then Return Nothing 
      Dim RowIndex As Integer = dgvLoggedAlert.SelectedRows(0).Cells(0).RowIndex 
      If RowIndex < 0 Then Return Nothing 
      If _Summarized = True AndAlso RowIndex = dgvLoggedAlert.Rows.Count - 1 Then dgvLoggedAlert.ClearSelection() : RaiseEvent evtUnChosen() : Return Nothing 
      Return _LoggedAlertCol(RowIndex) 
    End Get 
  End Property 
  
  Public ReadOnly Property [LoggedAlertCol]() As csLoggedAlertCol 
    Get 
      Return _LoggedAlertCol 
    End Get 
  End Property 
 
  Public Function LoadControl(ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    Dim pLoggedAlertCol As New csLoggedAlertCol(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    pFault = pLoggedAlertCol.Fill(_Requester) 
    If pFault.isOK = False Then Return pFault 
 
    pFault = LoadControl(pLoggedAlertCol)
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByVal vUniqueCode As Object, ByVal vParentObjectType As String, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pLoggedAlertCol As New csLoggedAlertCol(clsEnums.enmLoadParent.EntireObject) 
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
        Case "User" 
          pFault = pLoggedAlertCol.FillByAffectedUserID(ccHelper.ToLong(vUniqueCode), _Requester) 
        Case "LoggedLogin" 
          pFault = pLoggedAlertCol.FillByLoggedLoginID(ccHelper.ToLong(vUniqueCode), _Requester) 
        Case Else 
          Throw New Exception("Invalid vParentObjectType '" & vParentObjectType & "' received ") 
      End Select 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pLoggedAlertCol) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(vLoggedAlertCol As csLoggedAlertCol, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    Return LoadControl(vLoggedAlertCol) 
  End Function
  
  Private Function LoadControl(vLoggedAlertCol As csLoggedAlertCol) As clsFault
    Dim pFault As New clsFault
 
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
    'Use evtBeforeLoad to set or remove the list type, if you don't want the default 
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList  
    RaiseEvent evtBeforeLoad() 
 
    LoadLocalizedText() 
 
    'keep safe in case 
    Dim pColumnsHides As List(Of csLoggedAlert.enmProperty) = Nothing 
    If _LoadParameters.IsSumFillOnTheFly Then 
      pColumnsHides = New List(Of csLoggedAlert.enmProperty) 
      pColumnsHides.AddRange(_LoadParameters.ColumnsHide) 
    End If 
 
    'Force blg and clc fields to read-only 
    
    'Check for ReadOnly columns 
    For Each l In _LoadParameters.ColumnsReadOnly 
      If l = csLoggedAlert.enmProperty.ID Then colID.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.TimeOccurred Then colTimeOccurred.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultNumber Then colFaultNumber.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.SystemName Then colSystemName.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.CallingApplication Then colCallingApplication.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.AffectedUser Then colAffectedUser.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.CallingApplicationVersion Then colCallingApplicationVersion.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.CallingFunctionWithinApplication Then colCallingFunctionWithinApplication.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FreeText Then colFreeText.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultingAssembly Then colFaultingAssembly.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.AssemblyEntryPoint Then colAssemblyEntryPoint.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultingClass Then colFaultingClass.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultingFunction Then colFaultingFunction.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultingFunctionParameters Then colFaultingFunctionParameters.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultIdent Then colFaultIdent.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultDescription Then colFaultDescription.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.MessageSentToUser Then colMessageSentToUser.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.ActionSentToUser Then colActionSentToUser.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultType Then colFaultType.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.FaultSeverity Then colFaultSeverity.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.LoggedLogin Then colLoggedLogin.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.Thread Then colThread.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.UserIdentityType Then colUserIdentityType.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.UserIdentityTypeName Then colUserIdentityTypeName.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.DateOccurred Then colDateOccurred.ReadOnly = True 
      If l = csLoggedAlert.enmProperty.MonthOccurred Then colMonthOccurred.ReadOnly = True 
    Next 
 
    For Each l In _LoadParameters.ColumnsHide 
      'Parents only 
      Dim pParentProperty As csLoggedAlert.enmParentProperty = csLoggedAlert.enmParentProperty.UD 
      Dim pSuccess As Boolean = [Enum].TryParse(Of csLoggedAlert.enmParentProperty)(l.ToString(), ignoreCase:=False, pParentProperty) 
      If pSuccess = False Then Continue For 
      If Not _LoadParameters.CbosDoNotLoad.Contains(pParentProperty) Then 
        _LoadParameters.CbosDoNotLoad.Add(pParentProperty) 
      End If 
    Next 
 
    If _LoadParameters.IsSumFillOnTheFly Then 
      'Use what we just save instead 
      _LoadParameters.ColumnsHide = pColumnsHides 
    End If 
 
    dgvLoggedAlert.DoubleBuffered(True) 
 
    pFault = vLoggedAlertCol.LoadLookupAndEnumText(_Requester) : If Not pFault.isOK Then Return pFault 
    
    'Now transfer to local collection 
    _LoggedAlertColFullLength = vLoggedAlertCol.Clone() 
 
    'Truncate the strings 
    _LoggedAlertCol = vLoggedAlertCol 
    If _LoadParameters.TruncateStrings Then 
      _LoggedAlertCol.TruncateStrings() 
    Else 
      dgvLoggedAlert.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
      dgvLoggedAlert.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders 
    End If 
 
    ' If you switch between ReadOnly and not Readonly, it causes problems
    Static sReadOnlyHandled As Boolean = False 
    If sReadOnlyHandled = False Then 
      If _LoadParameters.ReadOnly = True Then 
        colAffectedUser.Name = colAffectedUser.Name & "zzzz" 
        colAffectedUserText.Name = colAffectedUser.Name.Replace("zzzz", "") 
        If colAffectedUser.DataGridView IsNot Nothing Then dgvLoggedAlert.Columns.Remove(colAffectedUser) 
        colLoggedLogin.Name = colLoggedLogin.Name & "zzzz" 
        colLoggedLoginText.Name = colLoggedLogin.Name.Replace("zzzz", "") 
        If colLoggedLogin.DataGridView IsNot Nothing Then dgvLoggedAlert.Columns.Remove(colLoggedLogin) 
      Else 
        If colAffectedUser.ReadOnly = False Then 
          If colAffectedUserText.DataGridView IsNot Nothing Then dgvLoggedAlert.Columns.Remove(colAffectedUserText) 
        Else 
          colAffectedUser.Name = colAffectedUser.Name & "zzzz" 
          colAffectedUserText.Name = colAffectedUser.Name.Replace("zzzz", "") 
          If colAffectedUser.DataGridView IsNot Nothing Then dgvLoggedAlert.Columns.Remove(colAffectedUser) 
          If Not _LoadParameters.CbosDoNotLoad.Contains(csLoggedAlert.enmParentProperty.AffectedUser) Then 
            _LoadParameters.CbosDoNotLoad.Add(csLoggedAlert.enmParentProperty.AffectedUser) 
          End If 
        End If 
        If colLoggedLogin.ReadOnly = False Then 
          If colLoggedLoginText.DataGridView IsNot Nothing Then dgvLoggedAlert.Columns.Remove(colLoggedLoginText) 
        Else 
          colLoggedLogin.Name = colLoggedLogin.Name & "zzzz" 
          colLoggedLoginText.Name = colLoggedLogin.Name.Replace("zzzz", "") 
          If colLoggedLogin.DataGridView IsNot Nothing Then dgvLoggedAlert.Columns.Remove(colLoggedLogin) 
          If Not _LoadParameters.CbosDoNotLoad.Contains(csLoggedAlert.enmParentProperty.LoggedLogin) Then 
            _LoadParameters.CbosDoNotLoad.Add(csLoggedAlert.enmParentProperty.LoggedLogin) 
          End If 
        End If 
      End If 
      sReadOnlyHandled = True 
    End If 
    If _LoadParameters.ReadOnly = False Then 
      'Load ComboListCache 
      If Not _LoadParameters.CbosDoNotLoad.Contains(csLoggedAlert.enmParentProperty.AffectedUser) Then 
        MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.Previous) 
      End If 
      If Not _LoadParameters.CbosDoNotLoad.Contains(csLoggedAlert.enmParentProperty.LoggedLogin) Then 
        MyCache.SetLevel(clsEnums.enmComboListType.c_LoggedLoginDefaultByID, Cache.enmLevel.Previous) 
      End If 
    End If 
 
    _SummaryOverFlow = "#" 
 
    Dim pHiddenColumnNames As New List(Of String) 
    For Each l In _LoadParameters.ColumnsHide 
      pHiddenColumnNames.Add("col" & l.ToString()) 
    Next 
    For Each lCol As DataGridViewColumn In dgvLoggedAlert.Columns 
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
    For Each p As csLoggedAlert.enmProperty In _LoadParameters.ColumnsHide 
      Dim pGridSetting As clsGridSetting = _GridSettings.FindByColumnName("col" & p.ToString()) 
      'HideColumn(p.ToString) 
      pGridSetting.ColumnRemoved = True 
    Next 
     
    'Set Header Text 
    For Each pD In _LoadParameters.ColumnsHeaderText 
      dgvLoggedAlert.Columns("col" & pD.Key.ToString).HeaderText = pD.Value 
    Next 
 
    'Format Columns 
    For Each pD In _LoadParameters.ColumnsFormat 
      dgvLoggedAlert.Columns("col" & pD.Key.ToString).DefaultCellStyle.Format = pD.Value 
    Next 
 
    'ordinal position 
    For Each pD In _LoadParameters.ColumnsOrdinalPosition 
      dgvLoggedAlert.Columns("col" & pD.Key.ToString).DisplayIndex = pD.Value 
    Next 
    _GridSettings.Update(Me, _Requester)
    
    'Align Columns 
    For Each pD In _LoadParameters.ColumnsAlignment 
      dgvLoggedAlert.Columns("col" & pD.Key.ToString).DefaultCellStyle.Alignment = pD.Value 
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
 
    dgvLoggedAlert.ClearSelection()
    bsCtlLoggedAlert.DataSource = Nothing 
    
    pFault = LoadSupportingCombos() : If pFault.isOK = False Then Return pFault 
 
    lblGrid.Text = _LoadParameters.GridTitle 
    If lblGrid.Text = "" Then 
      'Assume chkAutoRefresh is not used either. (may have to add it to LoadParameters) 
      pnlHeader.Visible = False 
    End If 
    Try
      LoadGrid()
    Catch ex As Exception
      Return pFault.LogException(ex, "LoadGrid", "TRGT-LoggedAlert-090124-2345", _Requester) 
    End Try
    
    RaiseEvent evtLoaded() 
    
    'Show row count in status label 
    lblStatus.ForeColor = Color.DarkGreen 
    lblStatus.Text = dgvLoggedAlert.RowCount & " rows" 
    
    'now do the default sorts 
    If _SortList IsNot Nothing Then 
      _AutoSorting = True 
      _PrevSortColumn = Nothing 
      For Each i In _SortList 
        Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(i, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0)) 
        dgvLoggedAlert_ColumnHeaderMouseClick(Me, pE) 
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
    If dgvLoggedAlert.SelectedRows.Count > 0 Then 
      pRowIndex = dgvLoggedAlert.SelectedRows(0).Cells(0).RowIndex 
    Else 
      If dgvLoggedAlert.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
        pRowIndex = dgvLoggedAlert.CurrentCellAddress.Y 
      End If 
    End If 
 
    If _LoadParameters.SummarizeGrid = True Then Summarize() 
 
    _Loading = True 
 
    bsCtlLoggedAlert.DataSource = Nothing 
    bsCtlLoggedAlert.DataSource = _LoggedAlertCol
    
    dgvLoggedAlert.ClearSelection() 
    
    RaiseEvent evtUnChosen()
    
    SetUpBNButtons(False)
    'set columns 
    LoadColumns() 
 
    'Load buttons 
    For Each p As ToolStripMenuItem In btnColumns.DropDownItems 
      If p Is mnuColsReset OrElse p Is mnuColsHideMost Then Continue For 
      Dim pMenuItemProprty As String = p.Name.Substring(13) 
      p.Checked = dgvLoggedAlert.Columns("col" & pMenuItemProprty).Visible 
    Next 
 
    If pRowIndex >= 0 Then 
      If _Summarized = True Then 
        If pRowIndex <= _LoggedAlertCol.Count - 2 Then 
          dgvLoggedAlert.Rows(pRowIndex).Selected = True 
        End If 
      Else 
        If pRowIndex <= _LoggedAlertCol.Count - 1 Then 
          dgvLoggedAlert.Rows(pRowIndex).Selected = True 
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
    Dim pTestLookupCol As clsComboList = Nothing 
    Dim pEnumCol As clsComboList = Nothing 
    'Load comboLists 
    'AffectedUser
    If _LoadParameters.ReadOnly = False AndAlso _LoadParameters.CbosDoNotLoad.Find(Function(p) p = csLoggedAlert.enmParentProperty.AffectedUser) = csLoggedAlert.enmParentProperty.UD Then 
      'enable using an external list if needed 
      pComboList = Nothing 
      pPrompt = "" 
      Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_UserDefaultByID 
      Dim pParentID As Long = 0 
      RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.AffectedUser, pComboListTypeToLoad, pParentID, pComboList, pPrompt) 
      If pComboList Is Nothing Then 
        pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList) : If Not pFault.isOK() Then Return pFault 
        If MyCache.GetLevel(pComboListTypeToLoad) = Cache.enmLevel.AlwaysPageFromServer Then 
          Return pFault.LogFreeTextFault($"In {Me.Name}, {pComboListTypeToLoad.FastToString()} is defined as AlwaysPageFromServer. Either change it to AlwaysCache in evtBeforeLoad, make the column read-only, or make this grid read-only", "", "TRGT-LoggedAlertCol-200806-1015", _Requester) 
        End If 
      End If 
      pComboList = pComboList.Clone() 
      If pPrompt = "" Then pPrompt = pChoose 
        pComboList.AddToTop(ccHelper.ToLong(0), pPrompt) 
      bsAffectedUser.DataSource = pComboList 
      colAffectedUser.Tag = pPrompt 
    End If 

    'EnumFaultType
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.FaultType, Nothing, Nothing, pEnumCol, pPrompt) 
    If pEnumCol Is Nothing Then 
      pEnumCol = New clsComboList 
      pFault = pEnumCol.FillEnums(clsEnums.enmEnum.FaultType, _Requester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      pFault.SetOK() 
    End If 
    pEnumCol.Remove(pEnumCol.FindByKey(clsEnums.enmFaultType.UD)) 
    pEnumCol.SortByText() 
    If pPrompt = "" Then 
      pPrompt = pChoose 
    End If 
    pEnumCol.AddToTop(clsEnums.enmFaultType.UD, pPrompt) 
    bsFaultType.DataSource = pEnumCol 
    colFaultType.Tag = pPrompt 

    'EnumFaultSeverity
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.FaultSeverity, Nothing, Nothing, pEnumCol, pPrompt) 
    If pEnumCol Is Nothing Then 
      pEnumCol = New clsComboList 
      pFault = pEnumCol.FillEnums(clsEnums.enmEnum.FaultSeverity, _Requester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      pFault.SetOK() 
    End If 
    pEnumCol.Remove(pEnumCol.FindByKey(clsEnums.enmFaultSeverity.UD)) 
    pEnumCol.SortByText() 
    If pPrompt = "" Then 
      pPrompt = pChoose 
    End If 
    pEnumCol.AddToTop(clsEnums.enmFaultSeverity.UD, pPrompt) 
    bsFaultSeverity.DataSource = pEnumCol 
    colFaultSeverity.Tag = pPrompt 

    'LoggedLogin
    If _LoadParameters.ReadOnly = False AndAlso _LoadParameters.CbosDoNotLoad.Find(Function(p) p = csLoggedAlert.enmParentProperty.LoggedLogin) = csLoggedAlert.enmParentProperty.UD Then 
      'enable using an external list if needed 
      pComboList = Nothing 
      pPrompt = "" 
      Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_LoggedLoginDefaultByID 
      Dim pParentID As Long = 0 
      RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.LoggedLogin, pComboListTypeToLoad, pParentID, pComboList, pPrompt) 
      If pComboList Is Nothing Then 
        pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList) : If Not pFault.isOK() Then Return pFault 
        If MyCache.GetLevel(pComboListTypeToLoad) = Cache.enmLevel.AlwaysPageFromServer Then 
          Return pFault.LogFreeTextFault($"In {Me.Name}, {pComboListTypeToLoad.FastToString()} is defined as AlwaysPageFromServer. Either change it to AlwaysCache in evtBeforeLoad, make the column read-only, or make this grid read-only", "", "TRGT-LoggedAlertCol-200806-1015", _Requester) 
        End If 
      End If 
      pComboList = pComboList.Clone() 
      If pPrompt = "" Then pPrompt = pChoose 
        pComboList.AddToTop(ccHelper.ToLong(0), pPrompt) 
      bsLoggedLogin.DataSource = pComboList 
      colLoggedLogin.Tag = pPrompt 
    End If 

    'UserIdentityType
    'enable using an external list if needed 
    pTestLookupCol = Nothing 
    pPrompt = pChoose 
    RaiseEvent evtOverrideLoadCbo(csLoggedAlert.enmParentProperty.UserIdentityType, Nothing, Nothing, pTestLookupCol, pPrompt) 
    If pTestLookupCol Is Nothing Then 
      pComboList = New clsComboList() 
      pFault = pComboList.FillLookup(clsEnums.enmLookup.UserIdentityType, _Requester) : If pFault.isOK = False Then Return pFault 
      pPrompt = pChoose 
      pComboList.AddToTop("", pPrompt) 
    Else 
      pComboList = pTestLookupCol 
    End If 
    bsUserIdentityType.DataSource = pComboList 
    colUserIdentityType.Tag = pPrompt 

    'UserIdentityTypeName
    _UserIdentityTypeNames = New Dictionary(Of String, clsComboList) 
    pComboList = New clsComboList 
    pComboList.Add(New clsComboListMember(0, "Global")) 
    pComboList.AddToTop(ccHelper.ToInteger(-1), pPrompt) 
    _UserIdentityTypeNames.Add("Global", pComboList) 
    'now do the list 
    Dim pUserIdentityType As New clsComboList() 
    pFault = pUserIdentityType.FillLookup(clsEnums.enmLookup.UserIdentityType, _Requester) : If Not pFault.isOK Then Return pFault 
    For Each l In pUserIdentityType 
      If l.KeyString.Equals("UD", StringComparison.OrdinalIgnoreCase) OrElse l.KeyString.Equals("Global", StringComparison.OrdinalIgnoreCase) Then Continue For 
      pComboList = New clsComboList 
      Dim pComboListToAdd As New clsComboList() 
      Dim pPrefix As String = "cc" 
      If l.KeyString.Equals("c_User", StringComparison.OrdinalIgnoreCase) Then pPrefix = "" 
      Dim pComboListType As clsEnums.enmComboListType = clsEnums.TranslateEnmComboListType($"{pPrefix}{l.KeyString}DefaultByID") 
      pFault = pComboListToAdd.Fill(pComboListType, _Requester) : If Not pFault.isOK Then Return pFault 
      For Each ll In pComboListToAdd 
        pComboList.Add(New clsComboListMember(ccHelper.ToInteger(ll.Key), ll.Text)) 
      Next 
      pComboList.AddToTop(ccHelper.ToInteger(-1), pPrompt) 
      _UserIdentityTypeNames.Add(l.KeyString, pComboList) 
    Next 
    pComboList = New clsComboList 
    pComboList.AddToTop(ccHelper.ToInteger(-1), pPrompt) 
    _UserIdentityTypeNames.Add("", pComboList) 
    colUserIdentityTypeName.Tag = pPrompt 

    _LoadedCombos = True 
     
    If pFault.Number = 0 Then pFault.SetOK() 'Haven't loaded any parameters 
    Return pFault
  End Function


  'Buttons
  Private Sub SetUpBNButtons(ByVal vInEdit As Boolean)
    btnEdit.Visible = False 
    btnImport.Visible = False 
    btnCeaseEdit.Visible = False 
    dgvLoggedAlert.EditMode = DataGridViewEditMode.EditProgrammatically 
    dgvLoggedAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
    dgvLoggedAlert.AllowUserToDeleteRows = False 
    dgvLoggedAlert.AllowUserToAddRows = False 
    If _LoggedAlertCol.Count = 0 Then 
      btnSpreadsheet.Enabled = False 
      btnReport.Enabled = False 
    Else 
      btnSpreadsheet.Enabled = True 
      btnReport.Enabled = True 
    End If 
    lblEditMode.Text = "" 
    tssReports.Visible = False 
    lblStatus.Text = "" 
    dgvLoggedAlert.Refresh() 
  End Sub
  'ExternalButtons 
  'CellFormatting  
  Private Sub dgvLoggedAlert_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvLoggedAlert.CellFormatting 
    '_Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    If dgvLoggedAlert.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      If dgvLoggedAlert.Columns(e.ColumnIndex).ReadOnly = False Then 
        Exit Sub 
      End If 
    End If 
 
    RaiseEvent evtCellFormatting(sender, e) 
 
    _IgnoreGridFault = True 
    If e.ColumnIndex = colUserIdentityType.Index Then 
      Dim pParentCell As DataGridViewComboBoxCell = CType(dgvLoggedAlert(colUserIdentityType.Index, e.RowIndex), DataGridViewComboBoxCell) 
 
      'get the cell with the cbo 
      Dim pCell As DataGridViewComboBoxCell = CType(dgvLoggedAlert(colUserIdentityTypeName.Index, e.RowIndex), DataGridViewComboBoxCell) 
      If Not pCell.DisplayMember.Equals("Text", StringComparison.OrdinalIgnoreCase) Then pCell.DisplayMember = "Text" 
      If Not pCell.ValueMember.Equals("KeyInteger", StringComparison.OrdinalIgnoreCase) Then pCell.ValueMember = "KeyInteger" 
      Try 
        If Not (pCell.DataSource Is _UserIdentityTypeNames(pParentCell.Value.ToString())) Then pCell.DataSource = _UserIdentityTypeNames(pParentCell.Value.ToString()) 
      Catch ex As Exception 
        pCell.DataSource = _UserIdentityTypeNames("") 
      End Try 
    End If 
    _IgnoreGridFault = False 
    ' Sample code evtCellFormatting - evtCellFormatting 
    ' You can use this to colour the fonts or your cell background or anything else that requires complete control of your cell 
    'Dim pLoggedAlert As csLoggedAlert = Nothing 
    'If dgvLoggedAlert.Columns(e.ColumnIndex).Name = colRecommendedQuantityToOrder.Name Then 
    '  If pLoggedAlert Is Nothing Then pLoggedAlert = CType(dgvLoggedAlert.Rows(e.RowIndex).DataBoundItem, csLoggedAlert) ' Only assign it if needed 
    '  If pLoggedAlert.CustomerOrders > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pLoggedAlert.CustomerOrders > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
    'If dgvLoggedAlert.Columns(e.ColumnIndex).Name = colRAV.Name Then 
    '  If pLoggedAlert Is Nothing Then pLoggedAlert = CType(dgvLoggedAlert.Rows(e.RowIndex).DataBoundItem, csLoggedAlert) ' Only assign it if needed
    '  If pLoggedAlert.RAV > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pLoggedAlert.RAV - pLoggedAlert.MaximumStock > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
 
    'Debug.Print("loc x,y:" & e.RowIndex & ", " & e.ColumnIndex & ": GetType" & dgvLoggedAlert.Columns(e.ColumnIndex).GetType.ToString & ": zValue" & e.Value.ToString) 
    If dgvLoggedAlert.Columns(e.ColumnIndex).GetType.ToString = "System.Windows.Forms.DataGridViewComboBoxColumn" Then 
      Dim pCol As System.Windows.Forms.DataGridViewComboBoxColumn = CType(dgvLoggedAlert.Columns(e.ColumnIndex), System.Windows.Forms.DataGridViewComboBoxColumn) 
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
          e.Value = "* BadCode '" & dgvLoggedAlert.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & "' *" 
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
 
    If dgvLoggedAlert.Columns(e.ColumnIndex).GetType.Name.Equals("DataGridViewImageColumn", StringComparison.OrdinalIgnoreCase) Then 
      If e.Value Is Nothing Then 
        e.Value = New Bitmap(1, 1) 
      End If 
    End If 
 
    If _Summarized = True Then 
      If e.RowIndex = dgvLoggedAlert.Rows.Count - 1 Then 
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
            If _SummaryOverFlow.IndexOf(dgvLoggedAlert.Columns(e.ColumnIndex).Name.Substring(3)) >= 0 Then 
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
  Private Sub dgvLoggedAlert_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLoggedAlert.CellValueChanged 
    If e.RowIndex < 0 Then Exit Sub 
 
    _IgnoreGridFault = True 
    If e.ColumnIndex = colUserIdentityType.Index Then 
      Dim pParentCell As DataGridViewComboBoxCell = CType(dgvLoggedAlert(colUserIdentityType.Index, e.RowIndex), DataGridViewComboBoxCell) 
 
      'get the cell with the cbo 
      Dim pCell As DataGridViewComboBoxCell = CType(dgvLoggedAlert(colUserIdentityTypeName.Index, e.RowIndex), DataGridViewComboBoxCell) 
      pCell.DisplayMember = "Text" 
      pCell.ValueMember = "KeyInteger" 
      Try 
        pCell.DataSource = _UserIdentityTypeNames(pParentCell.Value.ToString()) 
      Catch ex As Exception 
        pCell.DataSource = _UserIdentityTypeNames("") 
      End Try 
    End If 
    _IgnoreGridFault = False 
 
  End Sub 
 
  'Grid Sort
  Private Sub dgvLoggedAlert_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLoggedAlert.ColumnHeaderMouseClick
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewColumn As DataGridViewColumn = dgvLoggedAlert.Columns(e.ColumnIndex)
    If bsCtlLoggedAlert.Current Is Nothing Then Exit Sub

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
    dgvLoggedAlert.SuspendLayout()

    Dim pLoggedAlert As csLoggedAlert
    Dim pID As Long = 0 
    If dgvLoggedAlert.SelectedRows.Count > 0 Then 
    pLoggedAlert = CType(bsCtlLoggedAlert.Current, csLoggedAlert)
      pID = pLoggedAlert.ID 
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
    Dim pLoggedAlertCol As csLoggedAlertCol
    pLoggedAlertCol = CType(bsCtlLoggedAlert.DataSource, csLoggedAlertCol)

    Dim pSummaryRow As csLoggedAlert = Nothing 
    If _Summarized = True Then 
      pSummaryRow = pLoggedAlertCol(pLoggedAlertCol.Count - 1) 
      pLoggedAlertCol.RemoveAt(pLoggedAlertCol.Count - 1) 
    End If 
 
    If pNewSortOrder = SortOrder.Ascending Then
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
        'save the present sort 
        For iCntr As Integer = 0 To pLoggedAlertCol.Count - 1 
          pLoggedAlertCol(iCntr).Tag = iCntr.ToString("0000000000") 
        Next 
      End If 
      If pNewColumn Is colID Then
        pLoggedAlertCol.SortByID()
      ElseIf pNewColumn Is colTimeOccurred Then
        pLoggedAlertCol.SortByTimeOccurred()
      ElseIf pNewColumn Is colFaultNumber Then
        pLoggedAlertCol.SortByFaultNumber()
      ElseIf pNewColumn Is colSystemName Then
        pLoggedAlertCol.SortBySystemName()
      ElseIf pNewColumn Is colCallingApplication Then
        pLoggedAlertCol.SortByCallingApplication()
      ElseIf pNewColumn Is colAffectedUser OrElse pNewColumn Is colAffectedUserText Then
        pLoggedAlertCol.SortByAffectedUserText()
      ElseIf pNewColumn Is colCallingApplicationVersion Then
        pLoggedAlertCol.SortByCallingApplicationVersion()
      ElseIf pNewColumn Is colCallingFunctionWithinApplication Then
        pLoggedAlertCol.SortByCallingFunctionWithinApplication()
      ElseIf pNewColumn Is colFreeText Then
        pLoggedAlertCol.SortByFreeText()
      ElseIf pNewColumn Is colFaultingAssembly Then
        pLoggedAlertCol.SortByFaultingAssembly()
      ElseIf pNewColumn Is colAssemblyEntryPoint Then
        pLoggedAlertCol.SortByAssemblyEntryPoint()
      ElseIf pNewColumn Is colFaultingClass Then
        pLoggedAlertCol.SortByFaultingClass()
      ElseIf pNewColumn Is colFaultingFunction Then
        pLoggedAlertCol.SortByFaultingFunction()
      ElseIf pNewColumn Is colFaultingFunctionParameters Then
        pLoggedAlertCol.SortByFaultingFunctionParameters()
      ElseIf pNewColumn Is colFaultIdent Then
        pLoggedAlertCol.SortByFaultIdent()
      ElseIf pNewColumn Is colFaultDescription Then
        pLoggedAlertCol.SortByFaultDescription()
      ElseIf pNewColumn Is colMessageSentToUser Then
        pLoggedAlertCol.SortByMessageSentToUser()
      ElseIf pNewColumn Is colActionSentToUser Then
        pLoggedAlertCol.SortByActionSentToUser()
      ElseIf pNewColumn Is colFaultType Then
        pLoggedAlertCol.SortByFaultType()
      ElseIf pNewColumn Is colFaultSeverity Then
        pLoggedAlertCol.SortByFaultSeverity()
      ElseIf pNewColumn Is colLoggedLogin OrElse pNewColumn Is colLoggedLoginText Then
        pLoggedAlertCol.SortByLoggedLoginText()
      ElseIf pNewColumn Is colThread Then
        pLoggedAlertCol.SortByThread()
      ElseIf pNewColumn Is colUserIdentityType Then
        pLoggedAlertCol.SortByUserIdentityTypeText()
      ElseIf pNewColumn Is colUserIdentityTypeName Then
        pLoggedAlertCol.SortByUserIdentityTypeNameText()
      ElseIf pNewColumn Is colDateOccurred Then
        pLoggedAlertCol.SortByDateOccurred()
      ElseIf pNewColumn Is colMonthOccurred Then
        pLoggedAlertCol.SortByMonthOccurred()
      End If
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
      Dim iCntr As Integer = 0 
        If pNewColumn Is colID Then
          Dim pTest As Long = 0 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.ID <> pTest Then iCntr += 1 : pTest = p.ID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colTimeOccurred Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.TimeOccurred <> pTest Then iCntr += 1 : pTest = p.TimeOccurred 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultNumber Then
          Dim pTest As Integer = 0 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultNumber <> pTest Then iCntr += 1 : pTest = p.FaultNumber 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colSystemName Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.SystemName <> pTest Then iCntr += 1 : pTest = p.SystemName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colCallingApplication Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.CallingApplication <> pTest Then iCntr += 1 : pTest = p.CallingApplication 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAffectedUser OrElse pNewColumn Is colAffectedUserText Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.AffectedUserText <> pTest Then iCntr += 1 : pTest = p.AffectedUserText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colCallingApplicationVersion Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.CallingApplicationVersion <> pTest Then iCntr += 1 : pTest = p.CallingApplicationVersion 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colCallingFunctionWithinApplication Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.CallingFunctionWithinApplication <> pTest Then iCntr += 1 : pTest = p.CallingFunctionWithinApplication 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFreeText Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FreeText <> pTest Then iCntr += 1 : pTest = p.FreeText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultingAssembly Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultingAssembly <> pTest Then iCntr += 1 : pTest = p.FaultingAssembly 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAssemblyEntryPoint Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.AssemblyEntryPoint <> pTest Then iCntr += 1 : pTest = p.AssemblyEntryPoint 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultingClass Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultingClass <> pTest Then iCntr += 1 : pTest = p.FaultingClass 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultingFunction Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultingFunction <> pTest Then iCntr += 1 : pTest = p.FaultingFunction 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultingFunctionParameters Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultingFunctionParameters <> pTest Then iCntr += 1 : pTest = p.FaultingFunctionParameters 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultIdent Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultIdent <> pTest Then iCntr += 1 : pTest = p.FaultIdent 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultDescription Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultDescription <> pTest Then iCntr += 1 : pTest = p.FaultDescription 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colMessageSentToUser Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.MessageSentToUser <> pTest Then iCntr += 1 : pTest = p.MessageSentToUser 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colActionSentToUser Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.ActionSentToUser <> pTest Then iCntr += 1 : pTest = p.ActionSentToUser 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultType Then
          Dim pTest As clsEnums.enmFaultType = clsEnums.enmFaultType.UD 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultType <> pTest Then iCntr += 1 : pTest = p.FaultType 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFaultSeverity Then
          Dim pTest As clsEnums.enmFaultSeverity = clsEnums.enmFaultSeverity.UD 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.FaultSeverity <> pTest Then iCntr += 1 : pTest = p.FaultSeverity 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLoggedLogin OrElse pNewColumn Is colLoggedLoginText Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.LoggedLoginText <> pTest Then iCntr += 1 : pTest = p.LoggedLoginText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colThread Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.Thread <> pTest Then iCntr += 1 : pTest = p.Thread 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUserIdentityType Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.UserIdentityTypeText <> pTest Then iCntr += 1 : pTest = p.UserIdentityTypeText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUserIdentityTypeName Then
          Dim pTest As String = "" 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.UserIdentityTypeNameText <> pTest Then iCntr += 1 : pTest = p.UserIdentityTypeNameText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colDateOccurred Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.DateOccurred <> pTest Then iCntr += 1 : pTest = p.DateOccurred 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colMonthOccurred Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedAlert In pLoggedAlertCol 
            If p.MonthOccurred <> pTest Then iCntr += 1 : pTest = p.MonthOccurred 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        End If 
        pLoggedAlertCol.SortByTag() 
      End If 
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
      _PrevSortColumn = pNewColumn
      pPrevSortOrder = SortOrder.Ascending
    Else
      pLoggedAlertCol.Reverse()
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
      pPrevSortOrder = SortOrder.Descending
    End If

    If _Summarized = True Then 
      pLoggedAlertCol.Add(pSummaryRow) 
    End If 
 
    If pID > 0 Then
      bsCtlLoggedAlert.Position = bsCtlLoggedAlert.IndexOf(pLoggedAlertCol.FindByID(pID))
    End If

    'dgvLoggedAlert.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
    dgvLoggedAlert.ResumeLayout()

    Cursor = Cursors.Default
    dgvLoggedAlert.Refresh()

  End Sub
  'Select Row 
  Public Sub SelectRowByObjectID(ByVal pID As Long) 
    If pID > 0 Then 
      Dim pLoggedAlertCol As csLoggedAlertCol 
      pLoggedAlertCol = CType(bsCtlLoggedAlert.DataSource, csLoggedAlertCol) 
      Dim pLoggedAlert As csLoggedAlert = pLoggedAlertCol.FindByID(pID) 
      If Not pLoggedAlert.IsEmpty Then 
        bsCtlLoggedAlert.Position = bsCtlLoggedAlert.IndexOf(pLoggedAlertCol.FindByID(pID)) 
        dgvLoggedAlert.Rows(bsCtlLoggedAlert.Position).Selected = True 
      Else 
        dgvLoggedAlert.ClearSelection() 
      End If 
    ElseIf pID = 0 Then 
      dgvLoggedAlert.ClearSelection() 
    End If 
  End Sub 
  
  'Grid Resize
  Private Sub dgvLoggedAlert_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLoggedAlert.ColumnHeaderMouseDoubleClick
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    dgvLoggedAlert.AutoResizeColumn(e.ColumnIndex)
    Cursor = Cursors.Default
  End Sub
  'Other Grid Events
  Private Sub dgvLoggedAlert_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLoggedAlert.CurrentCellDirtyStateChanged
   _DVGDirty = True 
  End Sub
  Private Sub dgvLoggedAlert_Scroll(sender As Object, e As ScrollEventArgs) Handles dgvLoggedAlert.Scroll
    dgvLoggedAlert.Invalidate() 
  End Sub
 
  Private Sub dgvLoggedAlert_DataFault(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvLoggedAlert.DataError
    'Dim pFault As New clsFault
    '
    'If e.RowIndex = dgvLoggedAlert.Rows.Count - 1 Then Exit Sub
 
    'If dgvLoggedAlert.Columns(e.ColumnIndex).Name.StartsWith("colIDin", StringComparison.OrdinalIgnoreCase) Then Exit Sub 
 
    'If dgvLoggedAlert.Columns(e.ColumnIndex) Is colUserIdentityTypeName Then Exit Sub 
 
    'If _IgnoreGridFault = True Then Exit Sub
    '_DVGDirty = False 
    'Static pShown As Boolean 
    '
    'Dim pSubStrg As New System.Text.StringBuilder 
    ''Other Error 
    'Try 
    '  Try 
    '    pSubStrg.AppendLine("In table 'LoggedAlert', the row with an ID of " & dgvLoggedAlert.Rows(e.RowIndex).Cells(0).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine("In grid 'LoggedAlert', row index " & e.RowIndex) 
    '  End Try 
    '  Try 
    '    pSubStrg.AppendLine(" has an invalid value of " & dgvLoggedAlert.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine(" has an invalid value of Nothing.") 
    '  End Try 
    '  pSubStrg.AppendLine(" in column " & dgvLoggedAlert.Columns(e.ColumnIndex).DataPropertyName) 
    'Catch ex As Exception 
    '  pSubStrg.AppendLine("; Failed trying to fill DataFault as well!") 
    'End Try 
    'pFault.LogException(209, e.Exception, pSubStrg.ToString, "TRGT-LoggedAlert-100409-2248", _Requester) 
    'If pShown = False Then 
    '  Dim pCell As DataGridViewCell 
    '  Try 
    '    pCell = dgvLoggedAlert(e.ColumnIndex, e.RowIndex)
    '  Catch ex As Exception 
    '    pCell = dgvLoggedAlert(0, 0)
    '  End Try 
    '  ShowFault(pFault, _Requester) 
    '  pShown = True 
    'End If 
  End Sub
  Private Sub dgvLoggedAlert_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvLoggedAlert.ColumnWidthChanged
    If Me.DesignMode = True Then Exit Sub 
    If _Loading = False Then SaveSizes()
  End Sub
  Private Sub dgvLoggedAlert_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvLoggedAlert.ColumnDisplayIndexChanged
    Cursor = Cursors.WaitCursor
    If _Loading = False Then SaveSizes()
    Cursor = Cursors.Default
  End Sub
  Private Sub dgvLoggedAlert_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLoggedAlert.CellDoubleClick 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso e.RowIndex = dgvLoggedAlert.Rows.Count - 1 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
    Dim pCell As DataGridViewCell = dgvLoggedAlert(e.ColumnIndex, e.RowIndex) 
 
    Dim pHandled As Boolean = False 
    Dim pLoggedAlert As csLoggedAlert = _LoggedAlertCol(e.RowIndex)
    RaiseEvent evtRowDoubleClicked(pLoggedAlert, pHandled) 
    Cursor = Cursors.Default 
 
    If pHandled = False Then 
      If Me.ParentForm.Name.Equals("frmPopup", StringComparison.OrdinalIgnoreCase) Then Return 
      frmPopup.Text = "LoggedAlert Detail" 
      Dim pFault As clsFault = frmPopup.LoadControl("ctlc_LoggedAlert", pLoggedAlert, _Requester) 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
      frmPopup.ShowDialog() 
    End If 
 
  End Sub 
  Private Sub dgvLoggedAlert_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLoggedAlert.SelectionChanged 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If _Loading = True Then Exit Sub 
    If DateTime.Now < _IgnoreSelectionUntil Then Exit Sub
    If _ProcessingSelection Then Exit Sub
    If dgvLoggedAlert.SelectedRows.Count = 0 Then 
      RaiseEvent evtUnChosen() 
      Exit Sub 
    End If 
    Dim RowIndex As Integer = dgvLoggedAlert.SelectedRows(0).Cells(0).RowIndex 
    If RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso RowIndex = dgvLoggedAlert.Rows.Count - 1 Then dgvLoggedAlert.ClearSelection() : RaiseEvent evtUnChosen() : Exit Sub 
    Dim pLoggedAlert As csLoggedAlert = _LoggedAlertCol(RowIndex)
    _ProcessingSelection = True
    Try
      RaiseEvent evtRowClicked(pLoggedAlert) 
    Finally
      _IgnoreSelectionUntil = DateTime.Now.AddMilliseconds(500)
      _ProcessingSelection = False
    End Try
  End Sub 
  Private Sub dgvLoggedAlert_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLoggedAlert.RowLeave 
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
    For Each pCol As DataGridViewColumn In dgvLoggedAlert.Columns 
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
    If _LoggedAlertCol.Count <= 1 Then 
      _Summarized = False 
      Exit Sub 
    End If 
 
    Dim pFaultNumber As Integer 
    For Each pExistingRow As csLoggedAlert In _LoggedAlertCol 
      If _SummaryOverFlow.IndexOf("#FaultNumber#") < 0 Then 
        Try 
          pFaultNumber += pExistingRow.FaultNumber 
        Catch ex As System.OverflowException 
          pFaultNumber = -99999999 
          _SummaryOverFlow &= "FaultNumber#" 
        End Try 
      End If 
    Next 
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csLoggedAlert.enmSummarizeableProperty.FaultNumber) = csLoggedAlert.enmSummarizeableProperty.FaultNumber Then pFaultNumber = 0
    Dim pSummaryRow As New csLoggedAlert( _ 
        vID:=0 _ 
      , vTimeOccurred:=Nothing _ 
      , vFaultNumber:=pFaultNumber _ 
      , vSystemName:="" _ 
      , vCallingApplication:="" _ 
      , vAffectedUserID:=0 _ 
      , vAffectedUserText:="" _ 
      , vCallingApplicationVersion:="" _ 
      , vCallingFunctionWithinApplication:="" _ 
      , vFreeText:="" _ 
      , vFaultingAssembly:="" _ 
      , vAssemblyEntryPoint:="" _ 
      , vFaultingClass:="" _ 
      , vFaultingFunction:="" _ 
      , vFaultingFunctionParameters:="" _ 
      , vFaultIdent:="" _ 
      , vFaultDescription:="" _ 
      , vMessageSentToUser:="" _ 
      , vActionSentToUser:="" _ 
      , vFaultType:=clsEnums.enmFaultType.UD _ 
      , vFaultTypeText:="" _ 
      , vFaultSeverity:=clsEnums.enmFaultSeverity.UD _ 
      , vFaultSeverityText:="" _ 
      , vLoggedLoginID:=0 _ 
      , vLoggedLoginText:="" _ 
      , vThread:="" _ 
      , vUserIdentityTypeCode:="" _ 
      , vUserIdentityTypeText:="" _ 
      , vUserIdentityTypeNameCode:=-1 _ 
      , vUserIdentityTypeNameText:="" _ 
      , vDateOccurred:=Nothing _ 
      , vMonthOccurred:=Nothing _ 
      , vTag:="" _ 
      , vDateAdded:=Nothing _ 
      , vWithParents:=clsEnums.enmLoadParent.TextOnly _ 
      )
    _LoggedAlertCol.Add(pSummaryRow) 
    _Summarized = True 
  End Sub 
  
  'Reports and Excel 
  Friend Function CreateSpreadSheet() As clsFault  
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name  
    Dim pFault As New clsFault  
    'Dim pExcel As New Tools.ExcelSheet  
    Dim pDateToShow As String = DateTime.Now.ToString("yyMMdd_HHmmss")  
    Dim pRoot As String = $"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles" 
 
    Dim pFileNameAllFields As String = $"{pRoot}\LoggedAlertCol_{pDateToShow}AllFields.csv" 
    Dim pFileNameFieldsOnGrid As String = $"{pRoot}\LoggedAlertCol_{pDateToShow}FieldsOnGrid.csv" 
    Dim pFileNameAllFieldsWithIDs As String = $"{pRoot}\LoggedAlertCol_{pDateToShow}AllFieldsWithIDs.csv" 
    Dim pFileNameAllFieldsXML As String = $"{pRoot}\LoggedAlertCol_{pDateToShow}AllFields.xml" 
    Dim pFileNameAllFieldsJson As String = $"{pRoot}\LoggedAlertCol_{pDateToShow}AllFields.json" 
 
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
    For Each pCol As DataGridViewColumn In dgvLoggedAlert.Columns 
      If pCol.Visible = True Then 
        pTmpStrg.Append(",""" & pCol.HeaderText & """") 
      End If 
    Next 
    pCSV.AppendLine(pTmpStrg.ToString.Substring(1)) 
 
    'Now the data  
    Dim i As Integer 
    Dim pStart As Date = Now 
 
    Dim pTruncatedFieldNames As String = "" 
    For Each Row As DataGridViewRow In dgvLoggedAlert.Rows 
      i += 1 
      If _LoadParameters.SummarizeGrid = True Then 
        If Row.Index = dgvLoggedAlert.Rows.Count - 1 Then Exit For 
      End If 
      If i Mod 500 = 0 Then 
        lblStatus.Text = " Writing Row " & i & ". Time Elapsed: " & DateTime.Now.Subtract(pStart).TotalSeconds().ToString("###0") & " sec" : Application.DoEvents() 
      End If 
      pTmpStrg = New System.Text.StringBuilder 
       
      For Each pCol As DataGridViewColumn In dgvLoggedAlert.Columns 
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
        pFault = _LoggedAlertColFullLength.CreateXML(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsXML, pStrg, False) 
        'json 
        pFault = _LoggedAlertColFullLength.CreateJSON(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsJson, pStrg, False) 
        'default  
        My.Computer.FileSystem.WriteAllText(pFileNameAllFields, _LoggedAlertColFullLength.ToCSV, False)  
        'WithIDs  
        'pFault = _LoggedAlertColFullLength.LoadLookupAndEnumText(_Requester) : If pFault.isOK = False Then Return pFault (already done) 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsWithIDs, _LoggedAlertColFullLength.ToCSV(True), False) 
      End If  
      'default  
      My.Computer.FileSystem.WriteAllText(pFileNameFieldsOnGrid, pCSV.ToString, False)  
      pFault.SetOK()  
    Catch ex As Exception  
      pFault.LogException(ex, "", "TRGT-LoggedAlert-090210-1618", _Requester)  
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
      pFault.LogException(ex, "", "TRGT-LoggedAlert-090210-1618", _Requester)  
    End Try  
  
    If pFault.isOK = False Then Return pFault  
    
    Return pFault  
  End Function  
 
  Private Sub ReportDesign() 
 
    _Report = New vbReport.ReportDocument 
    _Report.AutoDiscover = False 
    Try 
      For Each pCol As DataGridViewColumn In dgvLoggedAlert.Columns 
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
        .SubTitleLeft = "LoggedAlerts" 
        .SubTitleRight = "Rows: " & _LoggedAlertCol.Count.ToString 
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
        .DataSource = _LoggedAlertCol 
        .HasSummaryLine = _Summarized 
      End With 
    Catch ex As Exception 
      Dim pFault As New clsFault 
      pFault.LogException(ex, "", "TRGT-LoggedAlert-090210-2119", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
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
      pFault.LogException(ex, "", "TRGT-LoggedAlert-090211-0746", _Requester) 
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
      For Each pRow As DataGridViewRow In dgvLoggedAlert.Rows 
        Try : pRow.Visible = True : Catch : End Try 
      Next 
      lblStatus.ForeColor = Color.DarkGreen 
      lblStatus.Text = dgvLoggedAlert.RowCount & " rows" 
      Exit Sub 
    End If 
    ' Hide rows that don't match search text 
    For Each row As DataGridViewRow In dgvLoggedAlert.Rows 
      Dim pVisible As Boolean = False 
      For Each cell As DataGridViewCell In row.Cells 
        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(pSearchText) Then 
          pVisible = True : Exit For 
        End If 
      Next 
      Try 
        Dim pBS As CurrencyManager = CType(Me.BindingContext(bsCtlLoggedAlert), CurrencyManager) 
        row.Visible = pVisible 
      Catch : End Try 
    Next 
    Dim pVisibleCount As Integer = 0 
    For Each row As DataGridViewRow In dgvLoggedAlert.Rows 
      If row.Visible Then pVisibleCount += 1 
    Next 
    lblStatus.ForeColor = Color.DarkBlue 
    lblStatus.Text = pVisibleCount & " of " & dgvLoggedAlert.RowCount & " rows" 
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
      pFault.LogException(ex, "GetOrInitializeGridSettings", "TRGT-LoggedAlert-120225-1310", _Requester) 
    End Try 
    If pFault.isOK = False Then Return pFault 
 
    Dim pSaveInitial As Boolean = False 
    
    '_GridSettings.Clear() Use for testing 
    If _GridSettings.Count = 0 Then 
      For Each pCol As DataGridViewColumn In dgvLoggedAlert.Columns 
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
      For Each pCol As DataGridViewColumn In dgvLoggedAlert.Columns 
        Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
        If pG.ColumnName = "" Then 
          pG.ColumnDisplayIndex = pCol.DisplayIndex 
          pG.ColumnWidth = ccHelper.ToInteger((dgvLoggedAlert.Width - 30) / dgvLoggedAlert.Columns.Count) 
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
    'colCallingFunctionWithinApplication.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colFreeText.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colFaultingAssembly.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colAssemblyEntryPoint.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colFaultingFunction.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colFaultingFunctionParameters.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colFaultIdent.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colFaultDescription.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colMessageSentToUser.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colActionSentToUser.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    
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
           
          With dgvLoggedAlert.Columns(lGridSetting.ColumnName) 
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
      pFault.LogException(204, ex, "", "TRGT-LoggedAlert-090120-1502", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
    End Try 
  End Sub 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "ID", _Requester) 
    If pStrg <> "" Then colID.HeaderText = pStrg : mnuColVisibleID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "TimeOccurred", _Requester) 
    If pStrg <> "" Then colTimeOccurred.HeaderText = pStrg : mnuColVisibleTimeOccurred.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultNumber", _Requester) 
    If pStrg <> "" Then colFaultNumber.HeaderText = pStrg : mnuColVisibleFaultNumber.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "SystemName", _Requester) 
    If pStrg <> "" Then colSystemName.HeaderText = pStrg : mnuColVisibleSystemName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "CallingApplication", _Requester) 
    If pStrg <> "" Then colCallingApplication.HeaderText = pStrg : mnuColVisibleCallingApplication.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "AffectedUser", _Requester) 
    If pStrg <> "" Then colAffectedUser.HeaderText = pStrg : mnuColVisibleAffectedUser.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "CallingApplicationVersion", _Requester) 
    If pStrg <> "" Then colCallingApplicationVersion.HeaderText = pStrg : mnuColVisibleCallingApplicationVersion.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "CallingFunctionWithinApplication", _Requester) 
    If pStrg <> "" Then colCallingFunctionWithinApplication.HeaderText = pStrg : mnuColVisibleCallingFunctionWithinApplication.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FreeText", _Requester) 
    If pStrg <> "" Then colFreeText.HeaderText = pStrg : mnuColVisibleFreeText.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultingAssembly", _Requester) 
    If pStrg <> "" Then colFaultingAssembly.HeaderText = pStrg : mnuColVisibleFaultingAssembly.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "AssemblyEntryPoint", _Requester) 
    If pStrg <> "" Then colAssemblyEntryPoint.HeaderText = pStrg : mnuColVisibleAssemblyEntryPoint.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultingClass", _Requester) 
    If pStrg <> "" Then colFaultingClass.HeaderText = pStrg : mnuColVisibleFaultingClass.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultingFunction", _Requester) 
    If pStrg <> "" Then colFaultingFunction.HeaderText = pStrg : mnuColVisibleFaultingFunction.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultingFunctionParameters", _Requester) 
    If pStrg <> "" Then colFaultingFunctionParameters.HeaderText = pStrg : mnuColVisibleFaultingFunctionParameters.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultIdent", _Requester) 
    If pStrg <> "" Then colFaultIdent.HeaderText = pStrg : mnuColVisibleFaultIdent.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultDescription", _Requester) 
    If pStrg <> "" Then colFaultDescription.HeaderText = pStrg : mnuColVisibleFaultDescription.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "MessageSentToUser", _Requester) 
    If pStrg <> "" Then colMessageSentToUser.HeaderText = pStrg : mnuColVisibleMessageSentToUser.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "ActionSentToUser", _Requester) 
    If pStrg <> "" Then colActionSentToUser.HeaderText = pStrg : mnuColVisibleActionSentToUser.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultType", _Requester) 
    If pStrg <> "" Then colFaultType.HeaderText = pStrg : mnuColVisibleFaultType.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "FaultSeverity", _Requester) 
    If pStrg <> "" Then colFaultSeverity.HeaderText = pStrg : mnuColVisibleFaultSeverity.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "LoggedLogin", _Requester) 
    If pStrg <> "" Then colLoggedLogin.HeaderText = pStrg : mnuColVisibleLoggedLogin.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "Thread", _Requester) 
    If pStrg <> "" Then colThread.HeaderText = pStrg : mnuColVisibleThread.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "UserIdentityType", _Requester) 
    If pStrg <> "" Then colUserIdentityType.HeaderText = pStrg : mnuColVisibleUserIdentityType.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "UserIdentityTypeName", _Requester) 
    If pStrg <> "" Then colUserIdentityTypeName.HeaderText = pStrg : mnuColVisibleUserIdentityTypeName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "DateOccurred", _Requester) 
    If pStrg <> "" Then colDateOccurred.HeaderText = pStrg : mnuColVisibleDateOccurred.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedAlert", "MonthOccurred", _Requester) 
    If pStrg <> "" Then colMonthOccurred.HeaderText = pStrg : mnuColVisibleMonthOccurred.Text = pStrg
 
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
 
  Private Sub mnuColVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColVisibleID.Click, mnuColVisibleTimeOccurred.Click, mnuColVisibleFaultNumber.Click, mnuColVisibleSystemName.Click, mnuColVisibleCallingApplication.Click, mnuColVisibleAffectedUser.Click, mnuColVisibleCallingApplicationVersion.Click, mnuColVisibleCallingFunctionWithinApplication.Click, mnuColVisibleFreeText.Click, mnuColVisibleFaultingAssembly.Click, mnuColVisibleAssemblyEntryPoint.Click, mnuColVisibleFaultingClass.Click, mnuColVisibleFaultingFunction.Click, mnuColVisibleFaultingFunctionParameters.Click, mnuColVisibleFaultIdent.Click, mnuColVisibleFaultDescription.Click, mnuColVisibleMessageSentToUser.Click, mnuColVisibleActionSentToUser.Click, mnuColVisibleFaultType.Click, mnuColVisibleFaultSeverity.Click, mnuColVisibleLoggedLogin.Click, mnuColVisibleThread.Click, mnuColVisibleUserIdentityType.Click, mnuColVisibleUserIdentityTypeName.Click, mnuColVisibleDateOccurred.Click, mnuColVisibleMonthOccurred.Click
    Cursor = Cursors.WaitCursor 
    Dim pToolStripItem As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem) 
    dgvLoggedAlert.Columns("col" & pToolStripItem.Name.Substring(13)).Visible = pToolStripItem.Checked 
    If _Loading = False Then SaveSizes() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsReset.Click 
    Cursor = Cursors.WaitCursor 
    dgvLoggedAlert.SuspendLayout() 
 
    For Each pCol As DataGridViewColumn In dgvLoggedAlert.Columns 
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
    pNewWidth = ccHelper.ToInteger((dgvLoggedAlert.Width - 30) / pVisibleColumns) 
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
     
    dgvLoggedAlert.ResumeLayout() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsHideMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsHideMost.Click 
 
    _Loading = True 
    'Hide All 
    If mnuColVisibleID.Checked = True Then mnuColVisibleID.PerformClick() 
    If mnuColVisibleTimeOccurred.Checked = True Then mnuColVisibleTimeOccurred.PerformClick() 
    If mnuColVisibleFaultNumber.Checked = True Then mnuColVisibleFaultNumber.PerformClick() 
    If mnuColVisibleSystemName.Checked = True Then mnuColVisibleSystemName.PerformClick() 
    If mnuColVisibleCallingApplication.Checked = True Then mnuColVisibleCallingApplication.PerformClick() 
    If mnuColVisibleAffectedUser.Checked = True Then mnuColVisibleAffectedUser.PerformClick() 
    If mnuColVisibleCallingApplicationVersion.Checked = True Then mnuColVisibleCallingApplicationVersion.PerformClick() 
    If mnuColVisibleCallingFunctionWithinApplication.Checked = True Then mnuColVisibleCallingFunctionWithinApplication.PerformClick() 
    If mnuColVisibleFreeText.Checked = True Then mnuColVisibleFreeText.PerformClick() 
    If mnuColVisibleFaultingAssembly.Checked = True Then mnuColVisibleFaultingAssembly.PerformClick() 
    If mnuColVisibleAssemblyEntryPoint.Checked = True Then mnuColVisibleAssemblyEntryPoint.PerformClick() 
    If mnuColVisibleFaultingClass.Checked = True Then mnuColVisibleFaultingClass.PerformClick() 
    If mnuColVisibleFaultingFunction.Checked = True Then mnuColVisibleFaultingFunction.PerformClick() 
    If mnuColVisibleFaultingFunctionParameters.Checked = True Then mnuColVisibleFaultingFunctionParameters.PerformClick() 
    If mnuColVisibleFaultIdent.Checked = True Then mnuColVisibleFaultIdent.PerformClick() 
    If mnuColVisibleFaultDescription.Checked = True Then mnuColVisibleFaultDescription.PerformClick() 
    If mnuColVisibleMessageSentToUser.Checked = True Then mnuColVisibleMessageSentToUser.PerformClick() 
    If mnuColVisibleActionSentToUser.Checked = True Then mnuColVisibleActionSentToUser.PerformClick() 
    If mnuColVisibleFaultType.Checked = True Then mnuColVisibleFaultType.PerformClick() 
    If mnuColVisibleFaultSeverity.Checked = True Then mnuColVisibleFaultSeverity.PerformClick() 
    If mnuColVisibleLoggedLogin.Checked = True Then mnuColVisibleLoggedLogin.PerformClick() 
    If mnuColVisibleThread.Checked = True Then mnuColVisibleThread.PerformClick() 
    If mnuColVisibleUserIdentityType.Checked = True Then mnuColVisibleUserIdentityType.PerformClick() 
    If mnuColVisibleUserIdentityTypeName.Checked = True Then mnuColVisibleUserIdentityTypeName.PerformClick() 
    If mnuColVisibleDateOccurred.Checked = True Then mnuColVisibleDateOccurred.PerformClick() 
    If mnuColVisibleMonthOccurred.Checked = True Then mnuColVisibleMonthOccurred.PerformClick() 
    'Show Defaults 
    If mnuColVisibleID.Checked = False Then mnuColVisibleID.PerformClick() 
    
    _Loading = False 
    'dgvLoggedAlert.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
  End Sub 
  
  Private Sub dgvLoggedAlert_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvLoggedAlert.CellMouseClick 
    If e.Button = MouseButtons.Right Then 
      Dim pMessageBox As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the LoggedAlert to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pMessageBox <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pLoggedAlert As csLoggedAlert = _LoggedAlertCol(e.RowIndex) 
        If pMessageBox = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pLoggedAlert.ToCSV) 
        Else 
          Clipboard.SetText(pLoggedAlert.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The LoggedAlert is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvLoggedAlert_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvLoggedAlert.MouseDown 
    '--- Save anchor on normal click (no modifiers) ---
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And (Keys.Shift Or Keys.Control)) = 0 AndAlso dgvLoggedAlert.CurrentRow IsNot Nothing Then 
      _SelectionAnchor = dgvLoggedAlert.CurrentRow.Index 
    End If 
    'This removes on click from the update 
    If dgvLoggedAlert.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
 
    Dim pCell As DataGridView.HitTestInfo = dgvLoggedAlert.HitTest(e.X, e.Y) 
 
    If pCell.Type = DataGridViewHitTestType.Cell Then 
      'Enable edit force it to be current 
      Dim pCurrentCell As DataGridViewCell = Nothing 
      Try 
        pCurrentCell = dgvLoggedAlert(pCell.ColumnIndex, pCell.RowIndex) 
        If pCurrentCell.ReadOnly Then Exit Sub 
        dgvLoggedAlert.CurrentCell = pCurrentCell 
      Catch ex As Exception 
        Exit Sub  
      End Try 
      'make the combobox drop down if it's active 
      If pCurrentCell.GetType().Name.Equals("DataGridViewComboBoxCell", StringComparison.OrdinalIgnoreCase) Then 
        dgvLoggedAlert.BeginEdit(True) 
        DirectCast(dgvLoggedAlert.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvLoggedAlert_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvLoggedAlert.MouseUp 
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And Keys.Shift) = Keys.Shift AndAlso _SelectionAnchor >= 0 Then 
      Dim hit = dgvLoggedAlert.HitTest(e.X, e.Y) 
      If hit.RowIndex >= 0 Then 
        dgvLoggedAlert.MultiSelect = True 
        dgvLoggedAlert.ClearSelection() 
        Dim pFrom As Integer = Math.Min(_SelectionAnchor, hit.RowIndex) 
        Dim pTo As Integer = Math.Max(_SelectionAnchor, hit.RowIndex) 
        For i As Integer = pFrom To pTo 
          dgvLoggedAlert.Rows(i).Selected = True 
        Next 
      End If 
    ElseIf (pModifiers And Keys.Control) = Keys.Control Then 
      dgvLoggedAlert.MultiSelect = True 
    Else 
      dgvLoggedAlert.MultiSelect = False 
    End If 
  End Sub 
 
  Private Sub chkAutoRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoRefresh.CheckedChanged 
    If chkAutoRefresh.Checked Then 
      _PrevSortColumn = Nothing 
 
      Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(colID.Index, -1, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton2, 0, 0, 0, 0)) 
      dgvLoggedAlert_ColumnHeaderMouseClick(Me, pE) 
      Application.DoEvents() 
      dgvLoggedAlert_ColumnHeaderMouseClick(Me, pE) 
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
 
  Private Sub ctlc_LoggedAlertCol_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
    'Set the font for the BN 
        If MyFont Is Nothing Then Return 
    BN.Font = New Font(MyFont.Name, MyFont.Size) 
    dgvLoggedAlert.RowTemplate.Height = ccHelper.ToInteger(23 * MyFont.Size / 9) 
  End Sub 
 
  Private Sub ctlc_LoggedAlertCol_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    If Me.ParentForm Is Nothing Then Exit Sub 
    Dim pParent As String = Me.ParentForm.Name 
    Dim pResponse As Boolean = Me.Visible 
    Dim pSize As Integer = dgvLoggedAlert.Width 
 
    'now set sizes if needed 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_LoggedAlertCol_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True AndAlso Not Me.Parent.Name.StartsWith("pnl", StringComparison.OrdinalIgnoreCase) Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub cc_evtBeforeLoad() Handles Me.evtBeforeLoad 
    _LoadParameters.SummarizeGrid = False 
    _LoadParameters.ReadOnly = True 
 
    If _LoadParameters.IsSumFillOnTheFly Then Exit Sub 
 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.SystemName) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.CallingFunctionWithinApplication) 
    '_LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.FreeText) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.AssemblyEntryPoint) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.FaultIdent) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.MessageSentToUser) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.ActionSentToUser) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.LoggedLogin) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.Thread) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.CallingApplicationVersion) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.FaultingAssembly) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.FaultingClass) 
    _LoadParameters.ColumnsHide.Add(csLoggedAlert.enmProperty.FaultingFunctionParameters) 
 
  End Sub 
 
  'Track open detail windows to prevent duplicates 
  Private Shared _openDetailWindows As New Dictionary(Of String, Form)() 
 
  'Context menu - right-click: add to selection if not already selected, otherwise keep multi-selection 
  Private Sub dgvLoggedAlert_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvLoggedAlert.CellMouseDown 
    ReleaseStuckModifierKeys() 'Fix sticky SHIFT/CTRL before selection changes 
    If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then 
      If Not dgvLoggedAlert.Rows(e.RowIndex).Selected Then 
        dgvLoggedAlert.ClearSelection() 
        dgvLoggedAlert.Rows(e.RowIndex).Selected = True 
      End If 
    End If 
  End Sub 
 
  'Context menu - Opening: adjust items based on single/multi selection 
  Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening 
    Dim pCount As Integer = dgvLoggedAlert.SelectedRows.Count 
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
    If dgvLoggedAlert.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvLoggedAlert.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _LoggedAlertCol.Count Then Exit Sub 
    Dim pLoggedAlert As csLoggedAlert = _LoggedAlertCol(pRowIndex) 
    Dim pTitle As String = "LoggedAlert #" & pLoggedAlert.ID.ToString() 
    Dim pKey As String = "LoggedAlert_" & pLoggedAlert.ID.ToString() 
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
    Dim pCtlName As String = "ctlc_LoggedAlert" 
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
    Dim pFault As clsFault = CType(pLoad.Invoke(pControl, New Object() {pLoggedAlert, _Requester}), clsFault) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    'Return to Tab click handler - sends entity to a new tab in frmMain 
    Dim pEntityRef As Object = pLoggedAlert 
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
    If dgvLoggedAlert.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvLoggedAlert.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _LoggedAlertCol.Count Then Exit Sub 
    Dim pLoggedAlert As csLoggedAlert = _LoggedAlertCol(pRowIndex) 
    Dim pFrmMain As frmMain = Nothing 
    For Each pForm As Form In Application.OpenForms 
      If TypeOf pForm Is frmMain Then 
        pFrmMain = CType(pForm, frmMain) 
        Exit For 
      End If 
    Next 
    If pFrmMain Is Nothing Then Exit Sub 
    'Check if already open in a window - if so, bring to front instead 
    Dim pWinKey As String = "LoggedAlert_" & pLoggedAlert.ID.ToString() 
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
    Dim pTabTitle As String = "LoggedAlert #" & pLoggedAlert.ID.ToString() 
    Dim pFault As clsFault = pFrmMain.OpenEntityInNewTab("ctlc_LoggedAlert", pLoggedAlert, _Requester, pTabTitle) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Context menu - Copy ID (supports multi-select) 
  Private Sub tsmiCopyID_Click(sender As Object, e As EventArgs) Handles tsmiCopyID.Click 
    If dgvLoggedAlert.SelectedRows.Count = 0 Then Exit Sub 
    Dim pIDs As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedAlert.SelectedRows 
      If pSelectedRow.Index >= 0 AndAlso pSelectedRow.Index < _LoggedAlertCol.Count Then 
        Dim pLoggedAlert As csLoggedAlert = _LoggedAlertCol(pSelectedRow.Index) 
        If pIDs.Length > 0 Then pIDs.Append(", ") 
        pIDs.Append(pLoggedAlert.ID.ToString()) 
      End If 
    Next 
    If pIDs.Length > 0 Then 
      Clipboard.SetText(pIDs.ToString()) 
      Dim pCount As Integer = dgvLoggedAlert.SelectedRows.Count 
      ShowToast(If(pCount = 1, "ID copied: " & pIDs.ToString(), pCount.ToString() & " IDs copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows (supports multi-select, values only) 
  Private Sub tsmiCopyRow_Click(sender As Object, e As EventArgs) Handles tsmiCopyRow.Click 
    If dgvLoggedAlert.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedAlert.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvLoggedAlert.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row copied", pCount.ToString() & " rows copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows with Headers (supports multi-select) 
  Private Sub tsmiCopyRowHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopyRowHeaders.Click 
    If dgvLoggedAlert.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers from first row 
    Dim pFirstRow As DataGridViewRow = dgvLoggedAlert.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add all selected rows 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedAlert.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvLoggedAlert.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row with headers copied", pCount.ToString() & " rows with headers copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy for Excel (with headers, VARCHAR fields wrapped in ="value" to preserve leading zeros) 
  Private Sub tsmiCopyExcel_Click(sender As Object, e As EventArgs) Handles tsmiCopyExcel.Click 
    If dgvLoggedAlert.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers 
    Dim pFirstRow As DataGridViewRow = dgvLoggedAlert.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add rows with Excel-safe formatting for text columns 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedAlert.SelectedRows 
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
      Dim pCount As Integer = dgvLoggedAlert.SelectedRows.Count 
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
