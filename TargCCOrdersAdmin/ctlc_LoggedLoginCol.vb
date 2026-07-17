Public Class ctlc_LoggedLoginCol
 
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
 
  
  Public Event evtRowClicked(ByVal vLoggedLogin As csLoggedLogin) 
  Public Event evtRowDoubleClicked(ByVal vLoggedLogin As csLoggedLogin, ByRef rHandled As Boolean) 
  Public Event evtUnChosen() 
 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csLoggedLogin.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  
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
    Public Property DoNotSummarizeProperties As List(Of csLoggedLogin.enmSummarizeableProperty) 
    Public Property SpreadsheetShowAllFields As Nullable(Of Boolean) 
    Public Property GridTitle As String 
    Public Property ReportTitle As String 
    Public Property [ReadOnly] As Boolean 
    Public Property CbosDoNotLoad As List(Of csLoggedLogin.enmParentProperty) 
    Public Property ColumnsReadOnly As List(Of csLoggedLogin.enmProperty) 
    Public Property ColumnsHide As List(Of csLoggedLogin.enmProperty) 
    Public Property ColumnsFormat As Dictionary(Of csLoggedLogin.enmProperty, String) 
    Public Property ColumnsOrdinalPosition As Dictionary(Of csLoggedLogin.enmProperty, Integer) 
    Public Property ColumnsAlignment As Dictionary(Of csLoggedLogin.enmProperty, DataGridViewContentAlignment) 
    Public Property ColumnsHeaderText As Dictionary(Of csLoggedLogin.enmProperty, String) 
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
      _DoNotSummarizeProperties = New List(Of csLoggedLogin.enmSummarizeableProperty) 
      _SpreadsheetShowAllFields = Nothing 
      _GridTitle = "" 
      _ReportTitle = "" 
      _ReadOnly = False 
      _CbosDoNotLoad = New List(Of csLoggedLogin.enmParentProperty) 
      _ColumnsReadOnly = New List(Of csLoggedLogin.enmProperty) 
      _ColumnsHide = New List(Of csLoggedLogin.enmProperty) 
      _ColumnsFormat = New Dictionary(Of csLoggedLogin.enmProperty, String) 
      _ColumnsOrdinalPosition = New Dictionary(Of csLoggedLogin.enmProperty, Integer) 
      _ColumnsAlignment = New Dictionary(Of csLoggedLogin.enmProperty, DataGridViewContentAlignment) 
      _ColumnsHeaderText = New Dictionary(Of csLoggedLogin.enmProperty, String) 
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
 
  Private WithEvents _LoggedLoginCol As csLoggedLoginCol
  Private WithEvents _LoggedLoginColFullLength As csLoggedLoginCol
 
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
  Public ReadOnly Property [SelectedLoggedLogin]() As csLoggedLogin 
    Get 
      If dgvLoggedLogin.SelectedRows.Count = 0 OrElse _Loading = True Then Return Nothing 
      Dim RowIndex As Integer = dgvLoggedLogin.SelectedRows(0).Cells(0).RowIndex 
      If RowIndex < 0 Then Return Nothing 
      If _Summarized = True AndAlso RowIndex = dgvLoggedLogin.Rows.Count - 1 Then dgvLoggedLogin.ClearSelection() : RaiseEvent evtUnChosen() : Return Nothing 
      Return _LoggedLoginCol(RowIndex) 
    End Get 
  End Property 
  
  Public ReadOnly Property [LoggedLoginCol]() As csLoggedLoginCol 
    Get 
      Return _LoggedLoginCol 
    End Get 
  End Property 
 
  Public Function LoadControl(ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    Dim pLoggedLoginCol As New csLoggedLoginCol() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    pFault = pLoggedLoginCol.Fill(_Requester) 
    If pFault.isOK = False Then Return pFault 
 
    pFault = LoadControl(pLoggedLoginCol)
    Return pFault 
  End Function 
 
  Public Function LoadControl(vLoggedLoginCol As csLoggedLoginCol, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    Return LoadControl(vLoggedLoginCol) 
  End Function
  
  Private Function LoadControl(vLoggedLoginCol As csLoggedLoginCol) As clsFault
    Dim pFault As New clsFault
 
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
    'Use evtBeforeLoad to set or remove the list type, if you don't want the default 
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList  
    RaiseEvent evtBeforeLoad() 
 
    LoadLocalizedText() 
 
    'keep safe in case 
    Dim pColumnsHides As List(Of csLoggedLogin.enmProperty) = Nothing 
    If _LoadParameters.IsSumFillOnTheFly Then 
      pColumnsHides = New List(Of csLoggedLogin.enmProperty) 
      pColumnsHides.AddRange(_LoadParameters.ColumnsHide) 
    End If 
 
    'Force blg and clc fields to read-only 
    
    'Check for ReadOnly columns 
    For Each l In _LoadParameters.ColumnsReadOnly 
      If l = csLoggedLogin.enmProperty.ID Then colID.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.UserName Then colUserName.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.UserFullName Then colUserFullName.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.TimeLoggedIn Then colTimeLoggedIn.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.ApplicationName Then colApplicationName.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.UserIdentityType Then colUserIdentityType.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.UserIdentityTypeName Then colUserIdentityTypeName.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.Roles Then colRoles.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.TimeLoggedOut Then colTimeLoggedOut.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.LoginFaultNumber Then colLoginFaultNumber.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.EnvironmentUserName Then colEnvironmentUserName.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.EnvironmentMachineName Then colEnvironmentMachineName.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.EnvironmentUserDomainName Then colEnvironmentUserDomainName.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.DnsGetHostName Then colDnsGetHostName.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.AddressList Then colAddressList.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.ComputerMACAddress Then colComputerMACAddress.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.SystemDiskVolumeSerialNo Then colSystemDiskVolumeSerialNo.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.LocalTime Then colLocalTime.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.GmtTime Then colGmtTime.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.AccessingComputerDetails Then colAccessingComputerDetails.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.UICulture Then colUICulture.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.TotalPhysicalMemoryKb Then colTotalPhysicalMemoryKb.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.AvailablePhysicalMemoryKb Then colAvailablePhysicalMemoryKb.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.ApplicationVersion Then colApplicationVersion.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.OriginatingIP Then colOriginatingIP.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.Language Then colLanguage.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.HostingAssembly Then colHostingAssembly.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.OriginatingCountry Then colOriginatingCountry.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.DateLoggedIn Then colDateLoggedIn.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.MonthLoggedIn Then colMonthLoggedIn.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.ClientReportedIP Then colClientReportedIP.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.ClientReportedCountry Then colClientReportedCountry.ReadOnly = True 
      If l = csLoggedLogin.enmProperty.IPAdditionalDetails Then colIPAdditionalDetails.ReadOnly = True 
    Next 
 
    For Each l In _LoadParameters.ColumnsHide 
      'Parents only 
    Next 
 
    If _LoadParameters.IsSumFillOnTheFly Then 
      'Use what we just save instead 
      _LoadParameters.ColumnsHide = pColumnsHides 
    End If 
 
    dgvLoggedLogin.DoubleBuffered(True) 
 
    pFault = vLoggedLoginCol.LoadLookupAndEnumText(_Requester) : If Not pFault.isOK Then Return pFault 
    
    'Now transfer to local collection 
    _LoggedLoginColFullLength = vLoggedLoginCol.Clone() 
 
    'Truncate the strings 
    _LoggedLoginCol = vLoggedLoginCol 
    If _LoadParameters.TruncateStrings Then 
      _LoggedLoginCol.TruncateStrings() 
    Else 
      dgvLoggedLogin.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
      dgvLoggedLogin.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders 
    End If 
 
    ' If you switch between ReadOnly and not Readonly, it causes problems
    Static sReadOnlyHandled As Boolean = False 
    If sReadOnlyHandled = False Then 
      If _LoadParameters.ReadOnly = True Then 
      Else 
      End If 
      sReadOnlyHandled = True 
    End If 
    If _LoadParameters.ReadOnly = False Then 
      'Load ComboListCache 
    End If 
 
    _SummaryOverFlow = "#" 
 
    Dim pHiddenColumnNames As New List(Of String) 
    For Each l In _LoadParameters.ColumnsHide 
      pHiddenColumnNames.Add("col" & l.ToString()) 
    Next 
    For Each lCol As DataGridViewColumn In dgvLoggedLogin.Columns 
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
    For Each p As csLoggedLogin.enmProperty In _LoadParameters.ColumnsHide 
      Dim pGridSetting As clsGridSetting = _GridSettings.FindByColumnName("col" & p.ToString()) 
      'HideColumn(p.ToString) 
      pGridSetting.ColumnRemoved = True 
    Next 
     
    'Set Header Text 
    For Each pD In _LoadParameters.ColumnsHeaderText 
      dgvLoggedLogin.Columns("col" & pD.Key.ToString).HeaderText = pD.Value 
    Next 
 
    'Format Columns 
    For Each pD In _LoadParameters.ColumnsFormat 
      dgvLoggedLogin.Columns("col" & pD.Key.ToString).DefaultCellStyle.Format = pD.Value 
    Next 
 
    'ordinal position 
    For Each pD In _LoadParameters.ColumnsOrdinalPosition 
      dgvLoggedLogin.Columns("col" & pD.Key.ToString).DisplayIndex = pD.Value 
    Next 
    _GridSettings.Update(Me, _Requester)
    
    'Align Columns 
    For Each pD In _LoadParameters.ColumnsAlignment 
      dgvLoggedLogin.Columns("col" & pD.Key.ToString).DefaultCellStyle.Alignment = pD.Value 
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
 
    dgvLoggedLogin.ClearSelection()
    bsCtlLoggedLogin.DataSource = Nothing 
    
    pFault = LoadSupportingCombos() : If pFault.isOK = False Then Return pFault 
 
    lblGrid.Text = _LoadParameters.GridTitle 
    If lblGrid.Text = "" Then 
      'Assume chkAutoRefresh is not used either. (may have to add it to LoadParameters) 
      pnlHeader.Visible = False 
    End If 
    Try
      LoadGrid()
    Catch ex As Exception
      Return pFault.LogException(ex, "LoadGrid", "TRGT-LoggedLogin-090124-2345", _Requester) 
    End Try
    
    RaiseEvent evtLoaded() 
    
    'Show row count in status label 
    lblStatus.ForeColor = Color.DarkGreen 
    lblStatus.Text = dgvLoggedLogin.RowCount & " rows" 
    
    'now do the default sorts 
    If _SortList IsNot Nothing Then 
      _AutoSorting = True 
      _PrevSortColumn = Nothing 
      For Each i In _SortList 
        Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(i, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0)) 
        dgvLoggedLogin_ColumnHeaderMouseClick(Me, pE) 
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
    If dgvLoggedLogin.SelectedRows.Count > 0 Then 
      pRowIndex = dgvLoggedLogin.SelectedRows(0).Cells(0).RowIndex 
    Else 
      If dgvLoggedLogin.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
        pRowIndex = dgvLoggedLogin.CurrentCellAddress.Y 
      End If 
    End If 
 
    If _LoadParameters.SummarizeGrid = True Then Summarize() 
 
    _Loading = True 
 
    bsCtlLoggedLogin.DataSource = Nothing 
    bsCtlLoggedLogin.DataSource = _LoggedLoginCol
    
    dgvLoggedLogin.ClearSelection() 
    
    RaiseEvent evtUnChosen()
    
    SetUpBNButtons(False)
    'set columns 
    LoadColumns() 
 
    'Load buttons 
    For Each p As ToolStripMenuItem In btnColumns.DropDownItems 
      If p Is mnuColsReset OrElse p Is mnuColsHideMost Then Continue For 
      Dim pMenuItemProprty As String = p.Name.Substring(13) 
      p.Checked = dgvLoggedLogin.Columns("col" & pMenuItemProprty).Visible 
    Next 
 
    If pRowIndex >= 0 Then 
      If _Summarized = True Then 
        If pRowIndex <= _LoggedLoginCol.Count - 2 Then 
          dgvLoggedLogin.Rows(pRowIndex).Selected = True 
        End If 
      Else 
        If pRowIndex <= _LoggedLoginCol.Count - 1 Then 
          dgvLoggedLogin.Rows(pRowIndex).Selected = True 
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
    'UserIdentityType
    'enable using an external list if needed 
    pTestLookupCol = Nothing 
    pPrompt = pChoose 
    RaiseEvent evtOverrideLoadCbo(csLoggedLogin.enmParentProperty.UserIdentityType, Nothing, Nothing, pTestLookupCol, pPrompt) 
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

    'EnumLanguage
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csLoggedLogin.enmParentProperty.Language, Nothing, Nothing, pEnumCol, pPrompt) 
    If pEnumCol Is Nothing Then 
      pEnumCol = New clsComboList 
      pFault = pEnumCol.FillEnums(clsEnums.enmEnum.Language, _Requester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      pFault.SetOK() 
    End If 
    pEnumCol.Remove(pEnumCol.FindByKey(clsEnums.enmLanguage.UD)) 
    pEnumCol.SortByText() 
    If pPrompt = "" Then 
      pPrompt = pChoose 
    End If 
    pEnumCol.AddToTop(clsEnums.enmLanguage.UD, pPrompt) 
    bsLanguage.DataSource = pEnumCol 
    colLanguage.Tag = pPrompt 

    _LoadedCombos = True 
     
    If pFault.Number = 0 Then pFault.SetOK() 'Haven't loaded any parameters 
    Return pFault
  End Function


  'Buttons
  Private Sub SetUpBNButtons(ByVal vInEdit As Boolean)
    btnEdit.Visible = False 
    btnImport.Visible = False 
    btnCeaseEdit.Visible = False 
    dgvLoggedLogin.EditMode = DataGridViewEditMode.EditProgrammatically 
    dgvLoggedLogin.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
    dgvLoggedLogin.AllowUserToDeleteRows = False 
    dgvLoggedLogin.AllowUserToAddRows = False 
    If _LoggedLoginCol.Count = 0 Then 
      btnSpreadsheet.Enabled = False 
      btnReport.Enabled = False 
    Else 
      btnSpreadsheet.Enabled = True 
      btnReport.Enabled = True 
    End If 
    lblEditMode.Text = "" 
    tssReports.Visible = False 
    lblStatus.Text = "" 
    dgvLoggedLogin.Refresh() 
  End Sub
  'ExternalButtons 
  'CellFormatting  
  Private Sub dgvLoggedLogin_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvLoggedLogin.CellFormatting 
    '_Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    If dgvLoggedLogin.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      If dgvLoggedLogin.Columns(e.ColumnIndex).ReadOnly = False Then 
        Exit Sub 
      End If 
    End If 
 
    RaiseEvent evtCellFormatting(sender, e) 
 
    _IgnoreGridFault = True 
    If e.ColumnIndex = colUserIdentityType.Index Then 
      Dim pParentCell As DataGridViewComboBoxCell = CType(dgvLoggedLogin(colUserIdentityType.Index, e.RowIndex), DataGridViewComboBoxCell) 
 
      'get the cell with the cbo 
      Dim pCell As DataGridViewComboBoxCell = CType(dgvLoggedLogin(colUserIdentityTypeName.Index, e.RowIndex), DataGridViewComboBoxCell) 
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
    'Dim pLoggedLogin As csLoggedLogin = Nothing 
    'If dgvLoggedLogin.Columns(e.ColumnIndex).Name = colRecommendedQuantityToOrder.Name Then 
    '  If pLoggedLogin Is Nothing Then pLoggedLogin = CType(dgvLoggedLogin.Rows(e.RowIndex).DataBoundItem, csLoggedLogin) ' Only assign it if needed 
    '  If pLoggedLogin.CustomerOrders > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pLoggedLogin.CustomerOrders > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
    'If dgvLoggedLogin.Columns(e.ColumnIndex).Name = colRAV.Name Then 
    '  If pLoggedLogin Is Nothing Then pLoggedLogin = CType(dgvLoggedLogin.Rows(e.RowIndex).DataBoundItem, csLoggedLogin) ' Only assign it if needed
    '  If pLoggedLogin.RAV > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pLoggedLogin.RAV - pLoggedLogin.MaximumStock > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
 
    'Debug.Print("loc x,y:" & e.RowIndex & ", " & e.ColumnIndex & ": GetType" & dgvLoggedLogin.Columns(e.ColumnIndex).GetType.ToString & ": zValue" & e.Value.ToString) 
    If dgvLoggedLogin.Columns(e.ColumnIndex).GetType.ToString = "System.Windows.Forms.DataGridViewComboBoxColumn" Then 
      Dim pCol As System.Windows.Forms.DataGridViewComboBoxColumn = CType(dgvLoggedLogin.Columns(e.ColumnIndex), System.Windows.Forms.DataGridViewComboBoxColumn) 
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
          e.Value = "* BadCode '" & dgvLoggedLogin.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & "' *" 
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
 
    If dgvLoggedLogin.Columns(e.ColumnIndex).GetType.Name.Equals("DataGridViewImageColumn", StringComparison.OrdinalIgnoreCase) Then 
      If e.Value Is Nothing Then 
        e.Value = New Bitmap(1, 1) 
      End If 
    End If 
 
    If _Summarized = True Then 
      If e.RowIndex = dgvLoggedLogin.Rows.Count - 1 Then 
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
            If _SummaryOverFlow.IndexOf(dgvLoggedLogin.Columns(e.ColumnIndex).Name.Substring(3)) >= 0 Then 
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
  Private Sub dgvLoggedLogin_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLoggedLogin.CellValueChanged 
    If e.RowIndex < 0 Then Exit Sub 
 
    _IgnoreGridFault = True 
    If e.ColumnIndex = colUserIdentityType.Index Then 
      Dim pParentCell As DataGridViewComboBoxCell = CType(dgvLoggedLogin(colUserIdentityType.Index, e.RowIndex), DataGridViewComboBoxCell) 
 
      'get the cell with the cbo 
      Dim pCell As DataGridViewComboBoxCell = CType(dgvLoggedLogin(colUserIdentityTypeName.Index, e.RowIndex), DataGridViewComboBoxCell) 
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
  Private Sub dgvLoggedLogin_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLoggedLogin.ColumnHeaderMouseClick
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewColumn As DataGridViewColumn = dgvLoggedLogin.Columns(e.ColumnIndex)
    If bsCtlLoggedLogin.Current Is Nothing Then Exit Sub

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
    dgvLoggedLogin.SuspendLayout()

    Dim pLoggedLogin As csLoggedLogin
    Dim pID As Long = 0 
    If dgvLoggedLogin.SelectedRows.Count > 0 Then 
    pLoggedLogin = CType(bsCtlLoggedLogin.Current, csLoggedLogin)
      pID = pLoggedLogin.ID 
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
    Dim pLoggedLoginCol As csLoggedLoginCol
    pLoggedLoginCol = CType(bsCtlLoggedLogin.DataSource, csLoggedLoginCol)

    Dim pSummaryRow As csLoggedLogin = Nothing 
    If _Summarized = True Then 
      pSummaryRow = pLoggedLoginCol(pLoggedLoginCol.Count - 1) 
      pLoggedLoginCol.RemoveAt(pLoggedLoginCol.Count - 1) 
    End If 
 
    If pNewSortOrder = SortOrder.Ascending Then
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
        'save the present sort 
        For iCntr As Integer = 0 To pLoggedLoginCol.Count - 1 
          pLoggedLoginCol(iCntr).Tag = iCntr.ToString("0000000000") 
        Next 
      End If 
      If pNewColumn Is colID Then
        pLoggedLoginCol.SortByID()
      ElseIf pNewColumn Is colUserName Then
        pLoggedLoginCol.SortByUserName()
      ElseIf pNewColumn Is colUserFullName Then
        pLoggedLoginCol.SortByUserFullName()
      ElseIf pNewColumn Is colTimeLoggedIn Then
        pLoggedLoginCol.SortByTimeLoggedIn()
      ElseIf pNewColumn Is colApplicationName Then
        pLoggedLoginCol.SortByApplicationName()
      ElseIf pNewColumn Is colUserIdentityType Then
        pLoggedLoginCol.SortByUserIdentityTypeText()
      ElseIf pNewColumn Is colUserIdentityTypeName Then
        pLoggedLoginCol.SortByUserIdentityTypeNameText()
      ElseIf pNewColumn Is colRoles Then
        pLoggedLoginCol.SortByRoles()
      ElseIf pNewColumn Is colTimeLoggedOut Then
        pLoggedLoginCol.SortByTimeLoggedOut()
      ElseIf pNewColumn Is colLoginFaultNumber Then
        pLoggedLoginCol.SortByLoginFaultNumber()
      ElseIf pNewColumn Is colEnvironmentUserName Then
        pLoggedLoginCol.SortByEnvironmentUserName()
      ElseIf pNewColumn Is colEnvironmentMachineName Then
        pLoggedLoginCol.SortByEnvironmentMachineName()
      ElseIf pNewColumn Is colEnvironmentUserDomainName Then
        pLoggedLoginCol.SortByEnvironmentUserDomainName()
      ElseIf pNewColumn Is colDnsGetHostName Then
        pLoggedLoginCol.SortByDnsGetHostName()
      ElseIf pNewColumn Is colAddressList Then
        pLoggedLoginCol.SortByAddressList()
      ElseIf pNewColumn Is colComputerMACAddress Then
        pLoggedLoginCol.SortByComputerMACAddress()
      ElseIf pNewColumn Is colSystemDiskVolumeSerialNo Then
        pLoggedLoginCol.SortBySystemDiskVolumeSerialNo()
      ElseIf pNewColumn Is colLocalTime Then
        pLoggedLoginCol.SortByLocalTime()
      ElseIf pNewColumn Is colGmtTime Then
        pLoggedLoginCol.SortByGmtTime()
      ElseIf pNewColumn Is colAccessingComputerDetails Then
        pLoggedLoginCol.SortByAccessingComputerDetails()
      ElseIf pNewColumn Is colUICulture Then
        pLoggedLoginCol.SortByUICulture()
      ElseIf pNewColumn Is colTotalPhysicalMemoryKb Then
        pLoggedLoginCol.SortByTotalPhysicalMemoryKb()
      ElseIf pNewColumn Is colAvailablePhysicalMemoryKb Then
        pLoggedLoginCol.SortByAvailablePhysicalMemoryKb()
      ElseIf pNewColumn Is colApplicationVersion Then
        pLoggedLoginCol.SortByApplicationVersion()
      ElseIf pNewColumn Is colOriginatingIP Then
        pLoggedLoginCol.SortByOriginatingIP()
      ElseIf pNewColumn Is colLanguage Then
        pLoggedLoginCol.SortByLanguage()
      ElseIf pNewColumn Is colHostingAssembly Then
        pLoggedLoginCol.SortByHostingAssembly()
      ElseIf pNewColumn Is colOriginatingCountry Then
        pLoggedLoginCol.SortByOriginatingCountry()
      ElseIf pNewColumn Is colDateLoggedIn Then
        pLoggedLoginCol.SortByDateLoggedIn()
      ElseIf pNewColumn Is colMonthLoggedIn Then
        pLoggedLoginCol.SortByMonthLoggedIn()
      ElseIf pNewColumn Is colClientReportedIP Then
        pLoggedLoginCol.SortByClientReportedIP()
      ElseIf pNewColumn Is colClientReportedCountry Then
        pLoggedLoginCol.SortByClientReportedCountry()
      ElseIf pNewColumn Is colIPAdditionalDetails Then
        pLoggedLoginCol.SortByIPAdditionalDetails()
      End If
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
      Dim iCntr As Integer = 0 
        If pNewColumn Is colID Then
          Dim pTest As Long = 0 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.ID <> pTest Then iCntr += 1 : pTest = p.ID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUserName Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.UserName <> pTest Then iCntr += 1 : pTest = p.UserName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUserFullName Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.UserFullName <> pTest Then iCntr += 1 : pTest = p.UserFullName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colTimeLoggedIn Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.TimeLoggedIn <> pTest Then iCntr += 1 : pTest = p.TimeLoggedIn 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colApplicationName Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.ApplicationName <> pTest Then iCntr += 1 : pTest = p.ApplicationName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUserIdentityType Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.UserIdentityTypeText <> pTest Then iCntr += 1 : pTest = p.UserIdentityTypeText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUserIdentityTypeName Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.UserIdentityTypeNameText <> pTest Then iCntr += 1 : pTest = p.UserIdentityTypeNameText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colRoles Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.Roles <> pTest Then iCntr += 1 : pTest = p.Roles 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colTimeLoggedOut Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.TimeLoggedOut <> pTest Then iCntr += 1 : pTest = p.TimeLoggedOut 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLoginFaultNumber Then
          Dim pTest As Integer = 0 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.LoginFaultNumber <> pTest Then iCntr += 1 : pTest = p.LoginFaultNumber 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colEnvironmentUserName Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.EnvironmentUserName <> pTest Then iCntr += 1 : pTest = p.EnvironmentUserName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colEnvironmentMachineName Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.EnvironmentMachineName <> pTest Then iCntr += 1 : pTest = p.EnvironmentMachineName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colEnvironmentUserDomainName Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.EnvironmentUserDomainName <> pTest Then iCntr += 1 : pTest = p.EnvironmentUserDomainName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colDnsGetHostName Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.DnsGetHostName <> pTest Then iCntr += 1 : pTest = p.DnsGetHostName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAddressList Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.AddressList <> pTest Then iCntr += 1 : pTest = p.AddressList 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colComputerMACAddress Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.ComputerMACAddress <> pTest Then iCntr += 1 : pTest = p.ComputerMACAddress 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colSystemDiskVolumeSerialNo Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.SystemDiskVolumeSerialNo <> pTest Then iCntr += 1 : pTest = p.SystemDiskVolumeSerialNo 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLocalTime Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.LocalTime <> pTest Then iCntr += 1 : pTest = p.LocalTime 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colGmtTime Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.GmtTime <> pTest Then iCntr += 1 : pTest = p.GmtTime 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAccessingComputerDetails Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.AccessingComputerDetails <> pTest Then iCntr += 1 : pTest = p.AccessingComputerDetails 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUICulture Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.UICulture <> pTest Then iCntr += 1 : pTest = p.UICulture 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colTotalPhysicalMemoryKb Then
          Dim pTest As Long = 0 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.TotalPhysicalMemoryKb <> pTest Then iCntr += 1 : pTest = p.TotalPhysicalMemoryKb 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAvailablePhysicalMemoryKb Then
          Dim pTest As Long = 0 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.AvailablePhysicalMemoryKb <> pTest Then iCntr += 1 : pTest = p.AvailablePhysicalMemoryKb 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colApplicationVersion Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.ApplicationVersion <> pTest Then iCntr += 1 : pTest = p.ApplicationVersion 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colOriginatingIP Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.OriginatingIP <> pTest Then iCntr += 1 : pTest = p.OriginatingIP 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLanguage Then
          Dim pTest As clsEnums.enmLanguage = clsEnums.enmLanguage.UD 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.Language <> pTest Then iCntr += 1 : pTest = p.Language 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colHostingAssembly Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.HostingAssembly <> pTest Then iCntr += 1 : pTest = p.HostingAssembly 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colOriginatingCountry Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.OriginatingCountry <> pTest Then iCntr += 1 : pTest = p.OriginatingCountry 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colDateLoggedIn Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.DateLoggedIn <> pTest Then iCntr += 1 : pTest = p.DateLoggedIn 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colMonthLoggedIn Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.MonthLoggedIn <> pTest Then iCntr += 1 : pTest = p.MonthLoggedIn 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colClientReportedIP Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.ClientReportedIP <> pTest Then iCntr += 1 : pTest = p.ClientReportedIP 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colClientReportedCountry Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.ClientReportedCountry <> pTest Then iCntr += 1 : pTest = p.ClientReportedCountry 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colIPAdditionalDetails Then
          Dim pTest As String = "" 
          For Each p As csLoggedLogin In pLoggedLoginCol 
            If p.IPAdditionalDetails <> pTest Then iCntr += 1 : pTest = p.IPAdditionalDetails 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        End If 
        pLoggedLoginCol.SortByTag() 
      End If 
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
      _PrevSortColumn = pNewColumn
      pPrevSortOrder = SortOrder.Ascending
    Else
      pLoggedLoginCol.Reverse()
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
      pPrevSortOrder = SortOrder.Descending
    End If

    If _Summarized = True Then 
      pLoggedLoginCol.Add(pSummaryRow) 
    End If 
 
    If pID > 0 Then
      bsCtlLoggedLogin.Position = bsCtlLoggedLogin.IndexOf(pLoggedLoginCol.FindByID(pID))
    End If

    'dgvLoggedLogin.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
    dgvLoggedLogin.ResumeLayout()

    Cursor = Cursors.Default
    dgvLoggedLogin.Refresh()

  End Sub
  'Select Row 
  Public Sub SelectRowByObjectID(ByVal pID As Long) 
    If pID > 0 Then 
      Dim pLoggedLoginCol As csLoggedLoginCol 
      pLoggedLoginCol = CType(bsCtlLoggedLogin.DataSource, csLoggedLoginCol) 
      Dim pLoggedLogin As csLoggedLogin = pLoggedLoginCol.FindByID(pID) 
      If Not pLoggedLogin.IsEmpty Then 
        bsCtlLoggedLogin.Position = bsCtlLoggedLogin.IndexOf(pLoggedLoginCol.FindByID(pID)) 
        dgvLoggedLogin.Rows(bsCtlLoggedLogin.Position).Selected = True 
      Else 
        dgvLoggedLogin.ClearSelection() 
      End If 
    ElseIf pID = 0 Then 
      dgvLoggedLogin.ClearSelection() 
    End If 
  End Sub 
  
  'Grid Resize
  Private Sub dgvLoggedLogin_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLoggedLogin.ColumnHeaderMouseDoubleClick
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    dgvLoggedLogin.AutoResizeColumn(e.ColumnIndex)
    Cursor = Cursors.Default
  End Sub
  'Other Grid Events
  Private Sub dgvLoggedLogin_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLoggedLogin.CurrentCellDirtyStateChanged
   _DVGDirty = True 
  End Sub
  Private Sub dgvLoggedLogin_Scroll(sender As Object, e As ScrollEventArgs) Handles dgvLoggedLogin.Scroll
    dgvLoggedLogin.Invalidate() 
  End Sub
 
  Private Sub dgvLoggedLogin_DataFault(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvLoggedLogin.DataError
    'Dim pFault As New clsFault
    '
    'If e.RowIndex = dgvLoggedLogin.Rows.Count - 1 Then Exit Sub
 
    'If dgvLoggedLogin.Columns(e.ColumnIndex).Name.StartsWith("colIDin", StringComparison.OrdinalIgnoreCase) Then Exit Sub 
 
    'If dgvLoggedLogin.Columns(e.ColumnIndex) Is colUserIdentityTypeName Then Exit Sub 
 
    'If _IgnoreGridFault = True Then Exit Sub
    '_DVGDirty = False 
    'Static pShown As Boolean 
    '
    'Dim pSubStrg As New System.Text.StringBuilder 
    ''Other Error 
    'Try 
    '  Try 
    '    pSubStrg.AppendLine("In table 'LoggedLogin', the row with an ID of " & dgvLoggedLogin.Rows(e.RowIndex).Cells(0).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine("In grid 'LoggedLogin', row index " & e.RowIndex) 
    '  End Try 
    '  Try 
    '    pSubStrg.AppendLine(" has an invalid value of " & dgvLoggedLogin.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine(" has an invalid value of Nothing.") 
    '  End Try 
    '  pSubStrg.AppendLine(" in column " & dgvLoggedLogin.Columns(e.ColumnIndex).DataPropertyName) 
    'Catch ex As Exception 
    '  pSubStrg.AppendLine("; Failed trying to fill DataFault as well!") 
    'End Try 
    'pFault.LogException(209, e.Exception, pSubStrg.ToString, "TRGT-LoggedLogin-100409-2248", _Requester) 
    'If pShown = False Then 
    '  Dim pCell As DataGridViewCell 
    '  Try 
    '    pCell = dgvLoggedLogin(e.ColumnIndex, e.RowIndex)
    '  Catch ex As Exception 
    '    pCell = dgvLoggedLogin(0, 0)
    '  End Try 
    '  ShowFault(pFault, _Requester) 
    '  pShown = True 
    'End If 
  End Sub
  Private Sub dgvLoggedLogin_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvLoggedLogin.ColumnWidthChanged
    If Me.DesignMode = True Then Exit Sub 
    If _Loading = False Then SaveSizes()
  End Sub
  Private Sub dgvLoggedLogin_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvLoggedLogin.ColumnDisplayIndexChanged
    Cursor = Cursors.WaitCursor
    If _Loading = False Then SaveSizes()
    Cursor = Cursors.Default
  End Sub
  Private Sub dgvLoggedLogin_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLoggedLogin.CellDoubleClick 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso e.RowIndex = dgvLoggedLogin.Rows.Count - 1 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
    Dim pCell As DataGridViewCell = dgvLoggedLogin(e.ColumnIndex, e.RowIndex) 
 
    Dim pHandled As Boolean = False 
    Dim pLoggedLogin As csLoggedLogin = _LoggedLoginCol(e.RowIndex)
    RaiseEvent evtRowDoubleClicked(pLoggedLogin, pHandled) 
    Cursor = Cursors.Default 
 
    If pHandled = False Then 
      If Me.ParentForm.Name.Equals("frmPopup", StringComparison.OrdinalIgnoreCase) Then Return 
      frmPopup.Text = "LoggedLogin Detail" 
      Dim pFault As clsFault = frmPopup.LoadControl("ctlc_LoggedLogin", pLoggedLogin, _Requester) 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
      frmPopup.ShowDialog() 
    End If 
 
  End Sub 
  Private Sub dgvLoggedLogin_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLoggedLogin.SelectionChanged 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If _Loading = True Then Exit Sub 
    If DateTime.Now < _IgnoreSelectionUntil Then Exit Sub
    If _ProcessingSelection Then Exit Sub
    If dgvLoggedLogin.SelectedRows.Count = 0 Then 
      RaiseEvent evtUnChosen() 
      Exit Sub 
    End If 
    Dim RowIndex As Integer = dgvLoggedLogin.SelectedRows(0).Cells(0).RowIndex 
    If RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso RowIndex = dgvLoggedLogin.Rows.Count - 1 Then dgvLoggedLogin.ClearSelection() : RaiseEvent evtUnChosen() : Exit Sub 
    Dim pLoggedLogin As csLoggedLogin = _LoggedLoginCol(RowIndex)
    _ProcessingSelection = True
    Try
      RaiseEvent evtRowClicked(pLoggedLogin) 
    Finally
      _IgnoreSelectionUntil = DateTime.Now.AddMilliseconds(500)
      _ProcessingSelection = False
    End Try
  End Sub 
  Private Sub dgvLoggedLogin_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLoggedLogin.RowLeave 
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
    For Each pCol As DataGridViewColumn In dgvLoggedLogin.Columns 
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
    If _LoggedLoginCol.Count <= 1 Then 
      _Summarized = False 
      Exit Sub 
    End If 
 
    Dim pLoginFaultNumber As Integer 
    Dim pTotalPhysicalMemoryKb As Long 
    Dim pAvailablePhysicalMemoryKb As Long 
    For Each pExistingRow As csLoggedLogin In _LoggedLoginCol 
      If _SummaryOverFlow.IndexOf("#LoginFaultNumber#") < 0 Then 
        Try 
          pLoginFaultNumber += pExistingRow.LoginFaultNumber 
        Catch ex As System.OverflowException 
          pLoginFaultNumber = -99999999 
          _SummaryOverFlow &= "LoginFaultNumber#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#TotalPhysicalMemoryKb#") < 0 Then 
        Try 
          pTotalPhysicalMemoryKb += pExistingRow.TotalPhysicalMemoryKb 
        Catch ex As System.OverflowException 
          pTotalPhysicalMemoryKb = -99999999 
          _SummaryOverFlow &= "TotalPhysicalMemoryKb#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#AvailablePhysicalMemoryKb#") < 0 Then 
        Try 
          pAvailablePhysicalMemoryKb += pExistingRow.AvailablePhysicalMemoryKb 
        Catch ex As System.OverflowException 
          pAvailablePhysicalMemoryKb = -99999999 
          _SummaryOverFlow &= "AvailablePhysicalMemoryKb#" 
        End Try 
      End If 
    Next 
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csLoggedLogin.enmSummarizeableProperty.LoginFaultNumber) = csLoggedLogin.enmSummarizeableProperty.LoginFaultNumber Then pLoginFaultNumber = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csLoggedLogin.enmSummarizeableProperty.TotalPhysicalMemoryKb) = csLoggedLogin.enmSummarizeableProperty.TotalPhysicalMemoryKb Then pTotalPhysicalMemoryKb = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csLoggedLogin.enmSummarizeableProperty.AvailablePhysicalMemoryKb) = csLoggedLogin.enmSummarizeableProperty.AvailablePhysicalMemoryKb Then pAvailablePhysicalMemoryKb = 0
    Dim pSummaryRow As New csLoggedLogin( _ 
        vID:=0 _ 
      , vUserName:="" _ 
      , vUserFullName:="" _ 
      , vTimeLoggedIn:=Nothing _ 
      , vApplicationName:="" _ 
      , vUserIdentityTypeCode:="" _ 
      , vUserIdentityTypeText:="" _ 
      , vUserIdentityTypeNameCode:=-1 _ 
      , vUserIdentityTypeNameText:="" _ 
      , vRoles:="" _ 
      , vTimeLoggedOut:=Nothing _ 
      , vLoginFaultNumber:=pLoginFaultNumber _ 
      , vEnvironmentUserName:="" _ 
      , vEnvironmentMachineName:="" _ 
      , vEnvironmentUserDomainName:="" _ 
      , vDnsGetHostName:="" _ 
      , vAddressList:="" _ 
      , vComputerMACAddress:="" _ 
      , vSystemDiskVolumeSerialNo:="" _ 
      , vLocalTime:=Nothing _ 
      , vGmtTime:=Nothing _ 
      , vAccessingComputerDetails:="" _ 
      , vUICulture:="" _ 
      , vTotalPhysicalMemoryKb:=pTotalPhysicalMemoryKb _ 
      , vAvailablePhysicalMemoryKb:=pAvailablePhysicalMemoryKb _ 
      , vApplicationVersion:="" _ 
      , vOriginatingIP:="" _ 
      , vLanguage:=clsEnums.enmLanguage.UD _ 
      , vLanguageText:="" _ 
      , vHostingAssembly:="" _ 
      , vOriginatingCountry:="" _ 
      , vDateLoggedIn:=Nothing _ 
      , vMonthLoggedIn:=Nothing _ 
      , vClientReportedIP:="" _ 
      , vClientReportedCountry:="" _ 
      , vIPAdditionalDetails:="" _ 
      , vTag:="" _ 
      , vDateAdded:=Nothing _ 
      )
    _LoggedLoginCol.Add(pSummaryRow) 
    _Summarized = True 
  End Sub 
  
  'Reports and Excel 
  Friend Function CreateSpreadSheet() As clsFault  
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name  
    Dim pFault As New clsFault  
    'Dim pExcel As New Tools.ExcelSheet  
    Dim pDateToShow As String = DateTime.Now.ToString("yyMMdd_HHmmss")  
    Dim pRoot As String = $"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles" 
 
    Dim pFileNameAllFields As String = $"{pRoot}\LoggedLoginCol_{pDateToShow}AllFields.csv" 
    Dim pFileNameFieldsOnGrid As String = $"{pRoot}\LoggedLoginCol_{pDateToShow}FieldsOnGrid.csv" 
    Dim pFileNameAllFieldsWithIDs As String = $"{pRoot}\LoggedLoginCol_{pDateToShow}AllFieldsWithIDs.csv" 
    Dim pFileNameAllFieldsXML As String = $"{pRoot}\LoggedLoginCol_{pDateToShow}AllFields.xml" 
    Dim pFileNameAllFieldsJson As String = $"{pRoot}\LoggedLoginCol_{pDateToShow}AllFields.json" 
 
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
    For Each pCol As DataGridViewColumn In dgvLoggedLogin.Columns 
      If pCol.Visible = True Then 
        pTmpStrg.Append(",""" & pCol.HeaderText & """") 
      End If 
    Next 
    pCSV.AppendLine(pTmpStrg.ToString.Substring(1)) 
 
    'Now the data  
    Dim i As Integer 
    Dim pStart As Date = Now 
 
    Dim pTruncatedFieldNames As String = "" 
    For Each Row As DataGridViewRow In dgvLoggedLogin.Rows 
      i += 1 
      If _LoadParameters.SummarizeGrid = True Then 
        If Row.Index = dgvLoggedLogin.Rows.Count - 1 Then Exit For 
      End If 
      If i Mod 500 = 0 Then 
        lblStatus.Text = " Writing Row " & i & ". Time Elapsed: " & DateTime.Now.Subtract(pStart).TotalSeconds().ToString("###0") & " sec" : Application.DoEvents() 
      End If 
      pTmpStrg = New System.Text.StringBuilder 
       
      For Each pCol As DataGridViewColumn In dgvLoggedLogin.Columns 
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
        pFault = _LoggedLoginColFullLength.CreateXML(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsXML, pStrg, False) 
        'json 
        pFault = _LoggedLoginColFullLength.CreateJSON(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsJson, pStrg, False) 
        'default  
        My.Computer.FileSystem.WriteAllText(pFileNameAllFields, _LoggedLoginColFullLength.ToCSV, False)  
        'WithIDs  
        'pFault = _LoggedLoginColFullLength.LoadLookupAndEnumText(_Requester) : If pFault.isOK = False Then Return pFault (already done) 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsWithIDs, _LoggedLoginColFullLength.ToCSV(True), False) 
      End If  
      'default  
      My.Computer.FileSystem.WriteAllText(pFileNameFieldsOnGrid, pCSV.ToString, False)  
      pFault.SetOK()  
    Catch ex As Exception  
      pFault.LogException(ex, "", "TRGT-LoggedLogin-090210-1618", _Requester)  
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
      pFault.LogException(ex, "", "TRGT-LoggedLogin-090210-1618", _Requester)  
    End Try  
  
    If pFault.isOK = False Then Return pFault  
    
    Return pFault  
  End Function  
 
  Private Sub ReportDesign() 
 
    _Report = New vbReport.ReportDocument 
    _Report.AutoDiscover = False 
    Try 
      For Each pCol As DataGridViewColumn In dgvLoggedLogin.Columns 
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
        .SubTitleLeft = "LoggedLogins" 
        .SubTitleRight = "Rows: " & _LoggedLoginCol.Count.ToString 
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
        .DataSource = _LoggedLoginCol 
        .HasSummaryLine = _Summarized 
      End With 
    Catch ex As Exception 
      Dim pFault As New clsFault 
      pFault.LogException(ex, "", "TRGT-LoggedLogin-090210-2119", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
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
      pFault.LogException(ex, "", "TRGT-LoggedLogin-090211-0746", _Requester) 
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
      For Each pRow As DataGridViewRow In dgvLoggedLogin.Rows 
        Try : pRow.Visible = True : Catch : End Try 
      Next 
      lblStatus.ForeColor = Color.DarkGreen 
      lblStatus.Text = dgvLoggedLogin.RowCount & " rows" 
      Exit Sub 
    End If 
    ' Hide rows that don't match search text 
    For Each row As DataGridViewRow In dgvLoggedLogin.Rows 
      Dim pVisible As Boolean = False 
      For Each cell As DataGridViewCell In row.Cells 
        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(pSearchText) Then 
          pVisible = True : Exit For 
        End If 
      Next 
      Try 
        Dim pBS As CurrencyManager = CType(Me.BindingContext(bsCtlLoggedLogin), CurrencyManager) 
        row.Visible = pVisible 
      Catch : End Try 
    Next 
    Dim pVisibleCount As Integer = 0 
    For Each row As DataGridViewRow In dgvLoggedLogin.Rows 
      If row.Visible Then pVisibleCount += 1 
    Next 
    lblStatus.ForeColor = Color.DarkBlue 
    lblStatus.Text = pVisibleCount & " of " & dgvLoggedLogin.RowCount & " rows" 
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
      pFault.LogException(ex, "GetOrInitializeGridSettings", "TRGT-LoggedLogin-120225-1310", _Requester) 
    End Try 
    If pFault.isOK = False Then Return pFault 
 
    Dim pSaveInitial As Boolean = False 
    
    '_GridSettings.Clear() Use for testing 
    If _GridSettings.Count = 0 Then 
      For Each pCol As DataGridViewColumn In dgvLoggedLogin.Columns 
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
      For Each pCol As DataGridViewColumn In dgvLoggedLogin.Columns 
        Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
        If pG.ColumnName = "" Then 
          pG.ColumnDisplayIndex = pCol.DisplayIndex 
          pG.ColumnWidth = ccHelper.ToInteger((dgvLoggedLogin.Width - 30) / dgvLoggedLogin.Columns.Count) 
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
    'colRoles.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colEnvironmentUserName.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colAddressList.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colComputerMACAddress.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colSystemDiskVolumeSerialNo.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colAccessingComputerDetails.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colApplicationVersion.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colOriginatingIP.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colClientReportedIP.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colIPAdditionalDetails.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    
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
           
          With dgvLoggedLogin.Columns(lGridSetting.ColumnName) 
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
      pFault.LogException(204, ex, "", "TRGT-LoggedLogin-090120-1502", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
    End Try 
  End Sub 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ID", _Requester) 
    If pStrg <> "" Then colID.HeaderText = pStrg : mnuColVisibleID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UserName", _Requester) 
    If pStrg <> "" Then colUserName.HeaderText = pStrg : mnuColVisibleUserName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UserFullName", _Requester) 
    If pStrg <> "" Then colUserFullName.HeaderText = pStrg : mnuColVisibleUserFullName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "TimeLoggedIn", _Requester) 
    If pStrg <> "" Then colTimeLoggedIn.HeaderText = pStrg : mnuColVisibleTimeLoggedIn.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ApplicationName", _Requester) 
    If pStrg <> "" Then colApplicationName.HeaderText = pStrg : mnuColVisibleApplicationName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UserIdentityType", _Requester) 
    If pStrg <> "" Then colUserIdentityType.HeaderText = pStrg : mnuColVisibleUserIdentityType.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UserIdentityTypeName", _Requester) 
    If pStrg <> "" Then colUserIdentityTypeName.HeaderText = pStrg : mnuColVisibleUserIdentityTypeName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "Roles", _Requester) 
    If pStrg <> "" Then colRoles.HeaderText = pStrg : mnuColVisibleRoles.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "TimeLoggedOut", _Requester) 
    If pStrg <> "" Then colTimeLoggedOut.HeaderText = pStrg : mnuColVisibleTimeLoggedOut.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "LoginFaultNumber", _Requester) 
    If pStrg <> "" Then colLoginFaultNumber.HeaderText = pStrg : mnuColVisibleLoginFaultNumber.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "EnvironmentUserName", _Requester) 
    If pStrg <> "" Then colEnvironmentUserName.HeaderText = pStrg : mnuColVisibleEnvironmentUserName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "EnvironmentMachineName", _Requester) 
    If pStrg <> "" Then colEnvironmentMachineName.HeaderText = pStrg : mnuColVisibleEnvironmentMachineName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "EnvironmentUserDomainName", _Requester) 
    If pStrg <> "" Then colEnvironmentUserDomainName.HeaderText = pStrg : mnuColVisibleEnvironmentUserDomainName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "DnsGetHostName", _Requester) 
    If pStrg <> "" Then colDnsGetHostName.HeaderText = pStrg : mnuColVisibleDnsGetHostName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "AddressList", _Requester) 
    If pStrg <> "" Then colAddressList.HeaderText = pStrg : mnuColVisibleAddressList.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ComputerMACAddress", _Requester) 
    If pStrg <> "" Then colComputerMACAddress.HeaderText = pStrg : mnuColVisibleComputerMACAddress.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "SystemDiskVolumeSerialNo", _Requester) 
    If pStrg <> "" Then colSystemDiskVolumeSerialNo.HeaderText = pStrg : mnuColVisibleSystemDiskVolumeSerialNo.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "LocalTime", _Requester) 
    If pStrg <> "" Then colLocalTime.HeaderText = pStrg : mnuColVisibleLocalTime.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "GmtTime", _Requester) 
    If pStrg <> "" Then colGmtTime.HeaderText = pStrg : mnuColVisibleGmtTime.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "AccessingComputerDetails", _Requester) 
    If pStrg <> "" Then colAccessingComputerDetails.HeaderText = pStrg : mnuColVisibleAccessingComputerDetails.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "UICulture", _Requester) 
    If pStrg <> "" Then colUICulture.HeaderText = pStrg : mnuColVisibleUICulture.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "TotalPhysicalMemoryKb", _Requester) 
    If pStrg <> "" Then colTotalPhysicalMemoryKb.HeaderText = pStrg : mnuColVisibleTotalPhysicalMemoryKb.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "AvailablePhysicalMemoryKb", _Requester) 
    If pStrg <> "" Then colAvailablePhysicalMemoryKb.HeaderText = pStrg : mnuColVisibleAvailablePhysicalMemoryKb.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ApplicationVersion", _Requester) 
    If pStrg <> "" Then colApplicationVersion.HeaderText = pStrg : mnuColVisibleApplicationVersion.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "OriginatingIP", _Requester) 
    If pStrg <> "" Then colOriginatingIP.HeaderText = pStrg : mnuColVisibleOriginatingIP.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "Language", _Requester) 
    If pStrg <> "" Then colLanguage.HeaderText = pStrg : mnuColVisibleLanguage.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "HostingAssembly", _Requester) 
    If pStrg <> "" Then colHostingAssembly.HeaderText = pStrg : mnuColVisibleHostingAssembly.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "OriginatingCountry", _Requester) 
    If pStrg <> "" Then colOriginatingCountry.HeaderText = pStrg : mnuColVisibleOriginatingCountry.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "DateLoggedIn", _Requester) 
    If pStrg <> "" Then colDateLoggedIn.HeaderText = pStrg : mnuColVisibleDateLoggedIn.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "MonthLoggedIn", _Requester) 
    If pStrg <> "" Then colMonthLoggedIn.HeaderText = pStrg : mnuColVisibleMonthLoggedIn.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ClientReportedIP", _Requester) 
    If pStrg <> "" Then colClientReportedIP.HeaderText = pStrg : mnuColVisibleClientReportedIP.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "ClientReportedCountry", _Requester) 
    If pStrg <> "" Then colClientReportedCountry.HeaderText = pStrg : mnuColVisibleClientReportedCountry.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_LoggedLogin", "IPAdditionalDetails", _Requester) 
    If pStrg <> "" Then colIPAdditionalDetails.HeaderText = pStrg : mnuColVisibleIPAdditionalDetails.Text = pStrg
 
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
 
  Private Sub mnuColVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColVisibleID.Click, mnuColVisibleUserName.Click, mnuColVisibleUserFullName.Click, mnuColVisibleTimeLoggedIn.Click, mnuColVisibleApplicationName.Click, mnuColVisibleUserIdentityType.Click, mnuColVisibleUserIdentityTypeName.Click, mnuColVisibleRoles.Click, mnuColVisibleTimeLoggedOut.Click, mnuColVisibleLoginFaultNumber.Click, mnuColVisibleEnvironmentUserName.Click, mnuColVisibleEnvironmentMachineName.Click, mnuColVisibleEnvironmentUserDomainName.Click, mnuColVisibleDnsGetHostName.Click, mnuColVisibleAddressList.Click, mnuColVisibleComputerMACAddress.Click, mnuColVisibleSystemDiskVolumeSerialNo.Click, mnuColVisibleLocalTime.Click, mnuColVisibleGmtTime.Click, mnuColVisibleAccessingComputerDetails.Click, mnuColVisibleUICulture.Click, mnuColVisibleTotalPhysicalMemoryKb.Click, mnuColVisibleAvailablePhysicalMemoryKb.Click, mnuColVisibleApplicationVersion.Click, mnuColVisibleOriginatingIP.Click, mnuColVisibleLanguage.Click, mnuColVisibleHostingAssembly.Click, mnuColVisibleOriginatingCountry.Click, mnuColVisibleDateLoggedIn.Click, mnuColVisibleMonthLoggedIn.Click, mnuColVisibleClientReportedIP.Click, mnuColVisibleClientReportedCountry.Click, mnuColVisibleIPAdditionalDetails.Click
    Cursor = Cursors.WaitCursor 
    Dim pToolStripItem As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem) 
    dgvLoggedLogin.Columns("col" & pToolStripItem.Name.Substring(13)).Visible = pToolStripItem.Checked 
    If _Loading = False Then SaveSizes() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsReset.Click 
    Cursor = Cursors.WaitCursor 
    dgvLoggedLogin.SuspendLayout() 
 
    For Each pCol As DataGridViewColumn In dgvLoggedLogin.Columns 
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
    pNewWidth = ccHelper.ToInteger((dgvLoggedLogin.Width - 30) / pVisibleColumns) 
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
     
    dgvLoggedLogin.ResumeLayout() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsHideMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsHideMost.Click 
 
    _Loading = True 
    'Hide All 
    If mnuColVisibleID.Checked = True Then mnuColVisibleID.PerformClick() 
    If mnuColVisibleUserName.Checked = True Then mnuColVisibleUserName.PerformClick() 
    If mnuColVisibleUserFullName.Checked = True Then mnuColVisibleUserFullName.PerformClick() 
    If mnuColVisibleTimeLoggedIn.Checked = True Then mnuColVisibleTimeLoggedIn.PerformClick() 
    If mnuColVisibleApplicationName.Checked = True Then mnuColVisibleApplicationName.PerformClick() 
    If mnuColVisibleUserIdentityType.Checked = True Then mnuColVisibleUserIdentityType.PerformClick() 
    If mnuColVisibleUserIdentityTypeName.Checked = True Then mnuColVisibleUserIdentityTypeName.PerformClick() 
    If mnuColVisibleRoles.Checked = True Then mnuColVisibleRoles.PerformClick() 
    If mnuColVisibleTimeLoggedOut.Checked = True Then mnuColVisibleTimeLoggedOut.PerformClick() 
    If mnuColVisibleLoginFaultNumber.Checked = True Then mnuColVisibleLoginFaultNumber.PerformClick() 
    If mnuColVisibleEnvironmentUserName.Checked = True Then mnuColVisibleEnvironmentUserName.PerformClick() 
    If mnuColVisibleEnvironmentMachineName.Checked = True Then mnuColVisibleEnvironmentMachineName.PerformClick() 
    If mnuColVisibleEnvironmentUserDomainName.Checked = True Then mnuColVisibleEnvironmentUserDomainName.PerformClick() 
    If mnuColVisibleDnsGetHostName.Checked = True Then mnuColVisibleDnsGetHostName.PerformClick() 
    If mnuColVisibleAddressList.Checked = True Then mnuColVisibleAddressList.PerformClick() 
    If mnuColVisibleComputerMACAddress.Checked = True Then mnuColVisibleComputerMACAddress.PerformClick() 
    If mnuColVisibleSystemDiskVolumeSerialNo.Checked = True Then mnuColVisibleSystemDiskVolumeSerialNo.PerformClick() 
    If mnuColVisibleLocalTime.Checked = True Then mnuColVisibleLocalTime.PerformClick() 
    If mnuColVisibleGmtTime.Checked = True Then mnuColVisibleGmtTime.PerformClick() 
    If mnuColVisibleAccessingComputerDetails.Checked = True Then mnuColVisibleAccessingComputerDetails.PerformClick() 
    If mnuColVisibleUICulture.Checked = True Then mnuColVisibleUICulture.PerformClick() 
    If mnuColVisibleTotalPhysicalMemoryKb.Checked = True Then mnuColVisibleTotalPhysicalMemoryKb.PerformClick() 
    If mnuColVisibleAvailablePhysicalMemoryKb.Checked = True Then mnuColVisibleAvailablePhysicalMemoryKb.PerformClick() 
    If mnuColVisibleApplicationVersion.Checked = True Then mnuColVisibleApplicationVersion.PerformClick() 
    If mnuColVisibleOriginatingIP.Checked = True Then mnuColVisibleOriginatingIP.PerformClick() 
    If mnuColVisibleLanguage.Checked = True Then mnuColVisibleLanguage.PerformClick() 
    If mnuColVisibleHostingAssembly.Checked = True Then mnuColVisibleHostingAssembly.PerformClick() 
    If mnuColVisibleOriginatingCountry.Checked = True Then mnuColVisibleOriginatingCountry.PerformClick() 
    If mnuColVisibleDateLoggedIn.Checked = True Then mnuColVisibleDateLoggedIn.PerformClick() 
    If mnuColVisibleMonthLoggedIn.Checked = True Then mnuColVisibleMonthLoggedIn.PerformClick() 
    If mnuColVisibleClientReportedIP.Checked = True Then mnuColVisibleClientReportedIP.PerformClick() 
    If mnuColVisibleClientReportedCountry.Checked = True Then mnuColVisibleClientReportedCountry.PerformClick() 
    If mnuColVisibleIPAdditionalDetails.Checked = True Then mnuColVisibleIPAdditionalDetails.PerformClick() 
    'Show Defaults 
    If mnuColVisibleID.Checked = False Then mnuColVisibleID.PerformClick() 
    
    _Loading = False 
    'dgvLoggedLogin.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
  End Sub 
  
  Private Sub dgvLoggedLogin_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvLoggedLogin.CellMouseClick 
    If e.Button = MouseButtons.Right Then 
      Dim pMessageBox As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the LoggedLogin to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pMessageBox <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pLoggedLogin As csLoggedLogin = _LoggedLoginCol(e.RowIndex) 
        If pMessageBox = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pLoggedLogin.ToCSV) 
        Else 
          Clipboard.SetText(pLoggedLogin.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The LoggedLogin is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvLoggedLogin_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvLoggedLogin.MouseDown 
    '--- Save anchor on normal click (no modifiers) ---
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And (Keys.Shift Or Keys.Control)) = 0 AndAlso dgvLoggedLogin.CurrentRow IsNot Nothing Then 
      _SelectionAnchor = dgvLoggedLogin.CurrentRow.Index 
    End If 
    'This removes on click from the update 
    If dgvLoggedLogin.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
 
    Dim pCell As DataGridView.HitTestInfo = dgvLoggedLogin.HitTest(e.X, e.Y) 
 
    If pCell.Type = DataGridViewHitTestType.Cell Then 
      'Enable edit force it to be current 
      Dim pCurrentCell As DataGridViewCell = Nothing 
      Try 
        pCurrentCell = dgvLoggedLogin(pCell.ColumnIndex, pCell.RowIndex) 
        If pCurrentCell.ReadOnly Then Exit Sub 
        dgvLoggedLogin.CurrentCell = pCurrentCell 
      Catch ex As Exception 
        Exit Sub  
      End Try 
      'make the combobox drop down if it's active 
      If pCurrentCell.GetType().Name.Equals("DataGridViewComboBoxCell", StringComparison.OrdinalIgnoreCase) Then 
        dgvLoggedLogin.BeginEdit(True) 
        DirectCast(dgvLoggedLogin.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvLoggedLogin_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvLoggedLogin.MouseUp 
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And Keys.Shift) = Keys.Shift AndAlso _SelectionAnchor >= 0 Then 
      Dim hit = dgvLoggedLogin.HitTest(e.X, e.Y) 
      If hit.RowIndex >= 0 Then 
        dgvLoggedLogin.MultiSelect = True 
        dgvLoggedLogin.ClearSelection() 
        Dim pFrom As Integer = Math.Min(_SelectionAnchor, hit.RowIndex) 
        Dim pTo As Integer = Math.Max(_SelectionAnchor, hit.RowIndex) 
        For i As Integer = pFrom To pTo 
          dgvLoggedLogin.Rows(i).Selected = True 
        Next 
      End If 
    ElseIf (pModifiers And Keys.Control) = Keys.Control Then 
      dgvLoggedLogin.MultiSelect = True 
    Else 
      dgvLoggedLogin.MultiSelect = False 
    End If 
  End Sub 
 
  Private Sub chkAutoRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoRefresh.CheckedChanged 
    If chkAutoRefresh.Checked Then 
      _PrevSortColumn = Nothing 
 
      Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(colID.Index, -1, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton2, 0, 0, 0, 0)) 
      dgvLoggedLogin_ColumnHeaderMouseClick(Me, pE) 
      Application.DoEvents() 
      dgvLoggedLogin_ColumnHeaderMouseClick(Me, pE) 
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
 
  Private Sub ctlc_LoggedLoginCol_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
    'Set the font for the BN 
        If MyFont Is Nothing Then Return 
    BN.Font = New Font(MyFont.Name, MyFont.Size) 
    dgvLoggedLogin.RowTemplate.Height = ccHelper.ToInteger(23 * MyFont.Size / 9) 
  End Sub 
 
  Private Sub ctlc_LoggedLoginCol_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    If Me.ParentForm Is Nothing Then Exit Sub 
    Dim pParent As String = Me.ParentForm.Name 
    Dim pResponse As Boolean = Me.Visible 
    Dim pSize As Integer = dgvLoggedLogin.Width 
 
    'now set sizes if needed 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_LoggedLoginCol_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True AndAlso Not Me.Parent.Name.StartsWith("pnl", StringComparison.OrdinalIgnoreCase) Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_LoggedLoginCol_ccevtCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.evtCellFormatting 
    Dim pLoggedLogin As csLoggedLogin = Nothing 
    If dgvLoggedLogin.Columns(e.ColumnIndex).Name = colOriginatingIP.Name Then 
      If pLoggedLogin Is Nothing Then pLoggedLogin = CType(dgvLoggedLogin.Rows(e.RowIndex).DataBoundItem, csLoggedLogin) ' Only assign it if needed 
      If pLoggedLogin.IPAdditionalDetails.IndexOf("IsTor: True", StringComparison.OrdinalIgnoreCase) > 0 Then 
        e.CellStyle.BackColor = Color.Red 
      ElseIf pLoggedLogin.IPAdditionalDetails.IndexOf("IsVPN: True", StringComparison.OrdinalIgnoreCase) > 0 Then 
        e.CellStyle.BackColor = Color.Yellow 
      End If 
    End If 
  End Sub 
 
  'Track open detail windows to prevent duplicates 
  Private Shared _openDetailWindows As New Dictionary(Of String, Form)() 
 
  'Context menu - right-click: add to selection if not already selected, otherwise keep multi-selection 
  Private Sub dgvLoggedLogin_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvLoggedLogin.CellMouseDown 
    ReleaseStuckModifierKeys() 'Fix sticky SHIFT/CTRL before selection changes 
    If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then 
      If Not dgvLoggedLogin.Rows(e.RowIndex).Selected Then 
        dgvLoggedLogin.ClearSelection() 
        dgvLoggedLogin.Rows(e.RowIndex).Selected = True 
      End If 
    End If 
  End Sub 
 
  'Context menu - Opening: adjust items based on single/multi selection 
  Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening 
    Dim pCount As Integer = dgvLoggedLogin.SelectedRows.Count 
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
    If dgvLoggedLogin.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvLoggedLogin.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _LoggedLoginCol.Count Then Exit Sub 
    Dim pLoggedLogin As csLoggedLogin = _LoggedLoginCol(pRowIndex) 
    Dim pTitle As String = "LoggedLogin #" & pLoggedLogin.ID.ToString() 
    Dim pKey As String = "LoggedLogin_" & pLoggedLogin.ID.ToString() 
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
    Dim pCtlName As String = "ctlc_LoggedLogin" 
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
    Dim pFault As clsFault = CType(pLoad.Invoke(pControl, New Object() {pLoggedLogin, _Requester}), clsFault) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    'Return to Tab click handler - sends entity to a new tab in frmMain 
    Dim pEntityRef As Object = pLoggedLogin 
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
    If dgvLoggedLogin.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvLoggedLogin.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _LoggedLoginCol.Count Then Exit Sub 
    Dim pLoggedLogin As csLoggedLogin = _LoggedLoginCol(pRowIndex) 
    Dim pFrmMain As frmMain = Nothing 
    For Each pForm As Form In Application.OpenForms 
      If TypeOf pForm Is frmMain Then 
        pFrmMain = CType(pForm, frmMain) 
        Exit For 
      End If 
    Next 
    If pFrmMain Is Nothing Then Exit Sub 
    'Check if already open in a window - if so, bring to front instead 
    Dim pWinKey As String = "LoggedLogin_" & pLoggedLogin.ID.ToString() 
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
    Dim pTabTitle As String = "LoggedLogin #" & pLoggedLogin.ID.ToString() 
    Dim pFault As clsFault = pFrmMain.OpenEntityInNewTab("ctlc_LoggedLogin", pLoggedLogin, _Requester, pTabTitle) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Context menu - Copy ID (supports multi-select) 
  Private Sub tsmiCopyID_Click(sender As Object, e As EventArgs) Handles tsmiCopyID.Click 
    If dgvLoggedLogin.SelectedRows.Count = 0 Then Exit Sub 
    Dim pIDs As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedLogin.SelectedRows 
      If pSelectedRow.Index >= 0 AndAlso pSelectedRow.Index < _LoggedLoginCol.Count Then 
        Dim pLoggedLogin As csLoggedLogin = _LoggedLoginCol(pSelectedRow.Index) 
        If pIDs.Length > 0 Then pIDs.Append(", ") 
        pIDs.Append(pLoggedLogin.ID.ToString()) 
      End If 
    Next 
    If pIDs.Length > 0 Then 
      Clipboard.SetText(pIDs.ToString()) 
      Dim pCount As Integer = dgvLoggedLogin.SelectedRows.Count 
      ShowToast(If(pCount = 1, "ID copied: " & pIDs.ToString(), pCount.ToString() & " IDs copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows (supports multi-select, values only) 
  Private Sub tsmiCopyRow_Click(sender As Object, e As EventArgs) Handles tsmiCopyRow.Click 
    If dgvLoggedLogin.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedLogin.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvLoggedLogin.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row copied", pCount.ToString() & " rows copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows with Headers (supports multi-select) 
  Private Sub tsmiCopyRowHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopyRowHeaders.Click 
    If dgvLoggedLogin.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers from first row 
    Dim pFirstRow As DataGridViewRow = dgvLoggedLogin.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add all selected rows 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedLogin.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvLoggedLogin.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row with headers copied", pCount.ToString() & " rows with headers copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy for Excel (with headers, VARCHAR fields wrapped in ="value" to preserve leading zeros) 
  Private Sub tsmiCopyExcel_Click(sender As Object, e As EventArgs) Handles tsmiCopyExcel.Click 
    If dgvLoggedLogin.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers 
    Dim pFirstRow As DataGridViewRow = dgvLoggedLogin.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add rows with Excel-safe formatting for text columns 
    For Each pSelectedRow As DataGridViewRow In dgvLoggedLogin.SelectedRows 
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
      Dim pCount As Integer = dgvLoggedLogin.SelectedRows.Count 
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
