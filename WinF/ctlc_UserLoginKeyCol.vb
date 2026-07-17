Public Class ctlc_UserLoginKeyCol
 
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
 
  Public Event evtBeforeUpdate(ByVal vUserLoginKey As csUserLoginKey, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vUserLoginKey As csUserLoginKey) 
  Private Event evtBeforeDelete(ByVal vUserLoginKey As csUserLoginKey, ByRef rCancel As Nullable(Of Boolean)) 
  
  Public Event evtRowClicked(ByVal vUserLoginKey As csUserLoginKey) 
  Public Event evtRowDoubleClicked(ByVal vUserLoginKey As csUserLoginKey, ByRef rHandled As Boolean) 
  Public Event evtUnChosen() 
 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csUserLoginKey.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  
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
    Public Property DoNotSummarizeProperties As List(Of csUserLoginKey.enmSummarizeableProperty) 
    Public Property SpreadsheetShowAllFields As Nullable(Of Boolean) 
    Public Property GridTitle As String 
    Public Property ReportTitle As String 
    Public Property [ReadOnly] As Boolean 
    Public Property CbosDoNotLoad As List(Of csUserLoginKey.enmParentProperty) 
    Public Property ColumnsReadOnly As List(Of csUserLoginKey.enmProperty) 
    Public Property ColumnsHide As List(Of csUserLoginKey.enmProperty) 
    Public Property ColumnsFormat As Dictionary(Of csUserLoginKey.enmProperty, String) 
    Public Property ColumnsOrdinalPosition As Dictionary(Of csUserLoginKey.enmProperty, Integer) 
    Public Property ColumnsAlignment As Dictionary(Of csUserLoginKey.enmProperty, DataGridViewContentAlignment) 
    Public Property ColumnsHeaderText As Dictionary(Of csUserLoginKey.enmProperty, String) 
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
      _DoNotSummarizeProperties = New List(Of csUserLoginKey.enmSummarizeableProperty) 
      _SpreadsheetShowAllFields = Nothing 
      _GridTitle = "" 
      _ReportTitle = "" 
      _ReadOnly = False 
      _CbosDoNotLoad = New List(Of csUserLoginKey.enmParentProperty) 
      _ColumnsReadOnly = New List(Of csUserLoginKey.enmProperty) 
      _ColumnsHide = New List(Of csUserLoginKey.enmProperty) 
      _ColumnsFormat = New Dictionary(Of csUserLoginKey.enmProperty, String) 
      _ColumnsOrdinalPosition = New Dictionary(Of csUserLoginKey.enmProperty, Integer) 
      _ColumnsAlignment = New Dictionary(Of csUserLoginKey.enmProperty, DataGridViewContentAlignment) 
      _ColumnsHeaderText = New Dictionary(Of csUserLoginKey.enmProperty, String) 
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
 
  Private WithEvents _UserLoginKeyCol As csUserLoginKeyCol
  Private WithEvents _UserLoginKeyColFullLength As csUserLoginKeyCol
 
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
  Public ReadOnly Property [SelectedUserLoginKey]() As csUserLoginKey 
    Get 
      If dgvUserLoginKey.SelectedRows.Count = 0 OrElse _Loading = True Then Return Nothing 
      Dim RowIndex As Integer = dgvUserLoginKey.SelectedRows(0).Cells(0).RowIndex 
      If RowIndex < 0 Then Return Nothing 
      If _Summarized = True AndAlso RowIndex = dgvUserLoginKey.Rows.Count - 1 Then dgvUserLoginKey.ClearSelection() : RaiseEvent evtUnChosen() : Return Nothing 
      Return _UserLoginKeyCol(RowIndex) 
    End Get 
  End Property 
  
  Public ReadOnly Property [UserLoginKeyCol]() As csUserLoginKeyCol 
    Get 
      Return _UserLoginKeyCol 
    End Get 
  End Property 
 
  Public Function LoadControl(ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    Dim pUserLoginKeyCol As New csUserLoginKeyCol(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    pFault = pUserLoginKeyCol.Fill(_Requester) 
    If pFault.isOK = False Then Return pFault 
 
    pFault = LoadControl(pUserLoginKeyCol)
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByVal vUniqueCode As Object, ByVal vParentObjectType As String, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUserLoginKeyCol As New csUserLoginKeyCol(clsEnums.enmLoadParent.EntireObject) 
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
          pFault = pUserLoginKeyCol.FillByUserID(ccHelper.ToLong(vUniqueCode), _Requester) 
        Case Else 
          Throw New Exception("Invalid vParentObjectType '" & vParentObjectType & "' received ") 
      End Select 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pUserLoginKeyCol) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(vUserLoginKeyCol As csUserLoginKeyCol, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    Return LoadControl(vUserLoginKeyCol) 
  End Function
  
  Private Function LoadControl(vUserLoginKeyCol As csUserLoginKeyCol) As clsFault
    Dim pFault As New clsFault
 
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
    'Use evtBeforeLoad to set or remove the list type, if you don't want the default 
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList  
    RaiseEvent evtBeforeLoad() 
 
    LoadLocalizedText() 
 
    'keep safe in case 
    Dim pColumnsHides As List(Of csUserLoginKey.enmProperty) = Nothing 
    If _LoadParameters.IsSumFillOnTheFly Then 
      pColumnsHides = New List(Of csUserLoginKey.enmProperty) 
      pColumnsHides.AddRange(_LoadParameters.ColumnsHide) 
    End If 
 
    'Force blg and clc fields to read-only 
    
    'Check for ReadOnly columns 
    For Each l In _LoadParameters.ColumnsReadOnly 
      If l = csUserLoginKey.enmProperty.ID Then colID.ReadOnly = True 
      If l = csUserLoginKey.enmProperty.User Then colUser.ReadOnly = True 
      If l = csUserLoginKey.enmProperty.ApplicationName Then colApplicationName.ReadOnly = True 
      If l = csUserLoginKey.enmProperty.ApplicationIdentifier Then colApplicationIdentifier.ReadOnly = True 
      If l = csUserLoginKey.enmProperty.KeyHashed Then colKeyHashed.ReadOnly = True 
      If l = csUserLoginKey.enmProperty.ExternalIPAtCreation Then colExternalIPAtCreation.ReadOnly = True 
      If l = csUserLoginKey.enmProperty.CountryAtCreation Then colCountryAtCreation.ReadOnly = True 
      If l = csUserLoginKey.enmProperty.LastAccessTime Then colLastAccessTime.ReadOnly = True 
      If l = csUserLoginKey.enmProperty.LoggedLoginID Then colLoggedLoginID.ReadOnly = True 
    Next 
 
    For Each l In _LoadParameters.ColumnsHide 
      'Parents only 
      Dim pParentProperty As csUserLoginKey.enmParentProperty = csUserLoginKey.enmParentProperty.UD 
      Dim pSuccess As Boolean = [Enum].TryParse(Of csUserLoginKey.enmParentProperty)(l.ToString(), ignoreCase:=False, pParentProperty) 
      If pSuccess = False Then Continue For 
      If Not _LoadParameters.CbosDoNotLoad.Contains(pParentProperty) Then 
        _LoadParameters.CbosDoNotLoad.Add(pParentProperty) 
      End If 
    Next 
 
    If _LoadParameters.IsSumFillOnTheFly Then 
      'Use what we just save instead 
      _LoadParameters.ColumnsHide = pColumnsHides 
    End If 
 
    dgvUserLoginKey.DoubleBuffered(True) 
 
    pFault = vUserLoginKeyCol.LoadLookupAndEnumText(_Requester) : If Not pFault.isOK Then Return pFault 
    
    'Now transfer to local collection 
    _UserLoginKeyColFullLength = vUserLoginKeyCol.Clone() 
 
    'Truncate the strings 
    _UserLoginKeyCol = vUserLoginKeyCol 
    If _LoadParameters.TruncateStrings Then 
      _UserLoginKeyCol.TruncateStrings() 
    Else 
      dgvUserLoginKey.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
      dgvUserLoginKey.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders 
    End If 
 
    ' If you switch between ReadOnly and not Readonly, it causes problems
    Static sReadOnlyHandled As Boolean = False 
    If sReadOnlyHandled = False Then 
      If _LoadParameters.ReadOnly = True Then 
        colUser.Name = colUser.Name & "zzzz" 
        colUserText.Name = colUser.Name.Replace("zzzz", "") 
        If colUser.DataGridView IsNot Nothing Then dgvUserLoginKey.Columns.Remove(colUser) 
      Else 
        If colUser.ReadOnly = False Then 
          If colUserText.DataGridView IsNot Nothing Then dgvUserLoginKey.Columns.Remove(colUserText) 
        Else 
          colUser.Name = colUser.Name & "zzzz" 
          colUserText.Name = colUser.Name.Replace("zzzz", "") 
          If colUser.DataGridView IsNot Nothing Then dgvUserLoginKey.Columns.Remove(colUser) 
          If Not _LoadParameters.CbosDoNotLoad.Contains(csUserLoginKey.enmParentProperty.User) Then 
            _LoadParameters.CbosDoNotLoad.Add(csUserLoginKey.enmParentProperty.User) 
          End If 
        End If 
      End If 
      sReadOnlyHandled = True 
    End If 
    If _LoadParameters.ReadOnly = False Then 
      'Load ComboListCache 
      If Not _LoadParameters.CbosDoNotLoad.Contains(csUserLoginKey.enmParentProperty.User) Then 
        MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.Previous) 
      End If 
    End If 
 
    _SummaryOverFlow = "#" 
 
    Dim pHiddenColumnNames As New List(Of String) 
    For Each l In _LoadParameters.ColumnsHide 
      pHiddenColumnNames.Add("col" & l.ToString()) 
    Next 
    For Each lCol As DataGridViewColumn In dgvUserLoginKey.Columns 
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
    For Each p As csUserLoginKey.enmProperty In _LoadParameters.ColumnsHide 
      Dim pGridSetting As clsGridSetting = _GridSettings.FindByColumnName("col" & p.ToString()) 
      'HideColumn(p.ToString) 
      pGridSetting.ColumnRemoved = True 
    Next 
     
    'Set Header Text 
    For Each pD In _LoadParameters.ColumnsHeaderText 
      dgvUserLoginKey.Columns("col" & pD.Key.ToString).HeaderText = pD.Value 
    Next 
 
    'Format Columns 
    For Each pD In _LoadParameters.ColumnsFormat 
      dgvUserLoginKey.Columns("col" & pD.Key.ToString).DefaultCellStyle.Format = pD.Value 
    Next 
 
    'ordinal position 
    For Each pD In _LoadParameters.ColumnsOrdinalPosition 
      dgvUserLoginKey.Columns("col" & pD.Key.ToString).DisplayIndex = pD.Value 
    Next 
    _GridSettings.Update(Me, _Requester)
    
    'Align Columns 
    For Each pD In _LoadParameters.ColumnsAlignment 
      dgvUserLoginKey.Columns("col" & pD.Key.ToString).DefaultCellStyle.Alignment = pD.Value 
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
 
    dgvUserLoginKey.ClearSelection()
    bsCtlUserLoginKey.DataSource = Nothing 
    
    pFault = LoadSupportingCombos() : If pFault.isOK = False Then Return pFault 
 
    lblGrid.Text = _LoadParameters.GridTitle 
    If lblGrid.Text = "" Then 
      'Assume chkAutoRefresh is not used either. (may have to add it to LoadParameters) 
      pnlHeader.Visible = False 
    End If 
    Try
      LoadGrid()
    Catch ex As Exception
      Return pFault.LogException(ex, "LoadGrid", "TRGT-UserLoginKey-090124-2345", _Requester) 
    End Try
    
    RaiseEvent evtLoaded() 
    
    'Show row count in status label 
    lblStatus.ForeColor = Color.DarkGreen 
    lblStatus.Text = dgvUserLoginKey.RowCount & " rows" 
    
    'now do the default sorts 
    If _SortList IsNot Nothing Then 
      _AutoSorting = True 
      _PrevSortColumn = Nothing 
      For Each i In _SortList 
        Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(i, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0)) 
        dgvUserLoginKey_ColumnHeaderMouseClick(Me, pE) 
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
    If dgvUserLoginKey.SelectedRows.Count > 0 Then 
      pRowIndex = dgvUserLoginKey.SelectedRows(0).Cells(0).RowIndex 
    Else 
      If dgvUserLoginKey.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
        pRowIndex = dgvUserLoginKey.CurrentCellAddress.Y 
      End If 
    End If 
 
    If _LoadParameters.SummarizeGrid = True Then Summarize() 
 
    _Loading = True 
 
    bsCtlUserLoginKey.DataSource = Nothing 
    bsCtlUserLoginKey.DataSource = _UserLoginKeyCol
    
    dgvUserLoginKey.ClearSelection() 
    
    RaiseEvent evtUnChosen()
    
    SetUpBNButtons(False)
    'set columns 
    LoadColumns() 
 
    'Load buttons 
    For Each p As ToolStripMenuItem In btnColumns.DropDownItems 
      If p Is mnuColsReset OrElse p Is mnuColsHideMost Then Continue For 
      Dim pMenuItemProprty As String = p.Name.Substring(13) 
      p.Checked = dgvUserLoginKey.Columns("col" & pMenuItemProprty).Visible 
    Next 
 
    If pRowIndex >= 0 Then 
      If _Summarized = True Then 
        If pRowIndex <= _UserLoginKeyCol.Count - 2 Then 
          dgvUserLoginKey.Rows(pRowIndex).Selected = True 
        End If 
      Else 
        If pRowIndex <= _UserLoginKeyCol.Count - 1 Then 
          dgvUserLoginKey.Rows(pRowIndex).Selected = True 
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
    'Load comboLists 
    'User
    If _LoadParameters.ReadOnly = False AndAlso _LoadParameters.CbosDoNotLoad.Find(Function(p) p = csUserLoginKey.enmParentProperty.User) = csUserLoginKey.enmParentProperty.UD Then 
      'enable using an external list if needed 
      pComboList = Nothing 
      pPrompt = "" 
      Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_UserDefaultByID 
      Dim pParentID As Long = 0 
      RaiseEvent evtOverrideLoadCbo(csUserLoginKey.enmParentProperty.User, pComboListTypeToLoad, pParentID, pComboList, pPrompt) 
      If pComboList Is Nothing Then 
        pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList) : If Not pFault.isOK() Then Return pFault 
        If MyCache.GetLevel(pComboListTypeToLoad) = Cache.enmLevel.AlwaysPageFromServer Then 
          Return pFault.LogFreeTextFault($"In {Me.Name}, {pComboListTypeToLoad.FastToString()} is defined as AlwaysPageFromServer. Either change it to AlwaysCache in evtBeforeLoad, make the column read-only, or make this grid read-only", "", "TRGT-UserLoginKeyCol-200806-1015", _Requester) 
        End If 
      End If 
      pComboList = pComboList.Clone() 
      If pPrompt = "" Then pPrompt = pChoose 
        pComboList.AddToTop(ccHelper.ToLong(0), pPrompt) 
      bsUser.DataSource = pComboList 
      colUser.Tag = pPrompt 
    End If 

    _LoadedCombos = True 
     
    If pFault.Number = 0 Then pFault.SetOK() 'Haven't loaded any parameters 
    Return pFault
  End Function


  'Buttons
  Private Sub SetUpBNButtons(ByVal vInEdit As Boolean)
    If _LoadParameters.ReadOnly = True Then 
      btnEdit.Visible = False 
      btnImport.Visible = False 
      btnDelete.Visible = False 
      btnCeaseEdit.Visible = False 
    Else 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserLoginKeyUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserLoginKeyUpdate, _Requester) = True AndAlso _LoadParameters.ImportButtonHide = False Then btnImport.Visible = vInEdit Else btnImport.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserLoginKeyDelete, _Requester) = True Then btnDelete.Visible = vInEdit Else btnDelete.Visible = False 
      btnCeaseEdit.Visible = vInEdit 
      If _LoadParameters.AddEditDeleteButtonsHide = True Then 
        btnDelete.Visible = False 
      End If 
    End If 
    If vInEdit = True AndAlso _LoadParameters.AddEditDeleteButtonsHide = False Then 
      colID.ReadOnly = True 
      dgvUserLoginKey.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 
      dgvUserLoginKey.SelectionMode = DataGridViewSelectionMode.CellSelect 
      _DVGDirty = False 
    Else 
      dgvUserLoginKey.EditMode = DataGridViewEditMode.EditProgrammatically 
      dgvUserLoginKey.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
      dgvUserLoginKey.AllowUserToDeleteRows = False 
      dgvUserLoginKey.AllowUserToAddRows = False 
      'Don't automatically set the 1st one If dgvUserLoginKey.Rows.Count > 0 Then 
      '  Dim pCurrentRow As Integer 
      '  pCurrentRow = dgvUserLoginKey.CurrentRow.Index 
      '  dgvUserLoginKey.CurrentCell = dgvUserLoginKey.Rows(pCurrentRow).Cells(0) 
      '  dgvUserLoginKey.Rows(pCurrentRow).Selected = True 
      'End If 
    End If 
    If vInEdit = True Then 
      lblEditMode.Text = "Edit Mode" 
      tssReports.Visible = True 
      btnSpreadsheet.Enabled = False 
      btnReport.Enabled = False 
    Else 
      If _UserLoginKeyCol.Count = 0 Then 
        btnSpreadsheet.Enabled = False 
        btnReport.Enabled = False 
      Else 
        btnSpreadsheet.Enabled = True 
        btnReport.Enabled = True 
      End If 
      lblEditMode.Text = "" 
      tssReports.Visible = False 
    End If 
    lblStatus.Text = "" 
    dgvUserLoginKey.Refresh() 
  End Sub
  Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    If _LoadParameters.AddEditDeleteButtonsHide = False Then 
      DoEdit() 
    Else 
      SetUpBNButtons(True) 
    End If 
    Cursor = Cursors.Default
  End Sub
  Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor 
    Dim pFault As New clsFault 
    pFault = DoDelete() 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
    Cursor = Cursors.Default 
  End Sub
  Private Sub btnCeaseEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCeaseEdit.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name  
    Cursor = Cursors.WaitCursor 
    DoCeaseEdit() 
    Cursor = Cursors.Default 
  End Sub 

  Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
 
    Dim pOverridden As Boolean = False 
    RaiseEvent evtOverrideImport(pOverridden) 
    If pOverridden = True Then Exit Sub 
 
    Dim pFieldList As New System.Text.StringBuilder 
    pFieldList.Append("ID, ") 
    pFieldList.Append("UserID (DB Code), ") 
    pFieldList.Append("ApplicationName, ") 
    pFieldList.Append("ApplicationIdentifier, ") 
    pFieldList.Append("KeyHashed, ") 
    pFieldList.Append("ExternalIPAtCreation, ") 
    pFieldList.Append("CountryAtCreation, ") 
    pFieldList.Append("LastAccessTime, ") 
    pFieldList.Append("LoggedLoginID, ") 
    
    Dim pNumberOfFields As Integer = 9 
    
    Dim pMessage As String = "This will import your spreadsheet data. It will update existing rows, and add non-existing rows." & Environment.NewLine 
    pMessage &= "Please save your file to a Unicode Text format, or CSV (comma delimited)." & Environment.NewLine 
    pMessage &= Environment.NewLine 
    pMessage &= "The first row should be column headers." & Environment.NewLine 
    pMessage &= Environment.NewLine 
    pMessage &= "The file should have the following " & pNumberOfFields & " fields:" & Environment.NewLine & " - " 
    pMessage &= pFieldList.ToString.Substring(0, pFieldList.Length - 2).Replace(", ", $"{Environment.NewLine} - ") & Environment.NewLine 
    pMessage &= "(I put the fields list in the clipboard for your convenience!)" 
    pMessage &= Environment.NewLine 
    pMessage &= Environment.NewLine 
    pMessage &= "If there is no ID field (the 1st field is UserID), then I will delete the table and recreate it with the data in this spreadsheet" 
    pMessage &= Environment.NewLine 
    pMessage &= Environment.NewLine 
    pMessage &= "Do you wish to continue?" & Environment.NewLine 
 
    Clipboard.SetText(pFieldList.ToString.Substring(0, pFieldList.Length - 2).Replace(", ", ControlChars.Tab)) 
 
    Dim pResponse As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg(pMessage, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
    If pResponse = frmMessageOrInputBox.enmButtonReturned.No Then Exit Sub 
 
 
    Dim ofd As New System.Windows.Forms.OpenFileDialog 
 
    Dim pImportFileLocation As String = My.Computer.FileSystem.SpecialDirectories.Desktop 
 
    'Get the file 
    With ofd 
      .AutoUpgradeEnabled = True 
      .CheckFileExists = True 
      .CheckPathExists = True 
      .DefaultExt = "" 
      .Filter = "TXT files (*.txt)|*.txt|CSV files (*.csv)|*.csv|All files (*.*)|*.*" 
      .InitialDirectory = pImportFileLocation 
      .Multiselect = False 
      .Title = "Choose the file to import" 
      .FileName = "" 
    End With 
 
    Dim pResult As System.Windows.Forms.DialogResult 
    pResult = ofd.ShowDialog(Me.Parent) 
 
    If pResult <> DialogResult.OK Then Exit Sub 
 
    Cursor = Cursors.WaitCursor 
 
    Dim pFilenameIn As String = ofd.FileName 
    Dim pEnding As String = pFilenameIn.Substring(pFilenameIn.Length - 4) 
    Dim pFilenameOut As String = pFilenameIn.Replace(pEnding, "Out.csv") 
 
    'Now Load the data 
    Dim pIncomingUserLoginKeys As New csUserLoginKeyCol(vWithParents:=clsEnums.enmLoadParent.DoNotLoad) 
 
    Dim pErrorFound As Boolean = False 
    Using pReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(ofd.FileName, System.Text.Encoding.Unicode) 
      pReader.TextFieldType = FileIO.FieldType.Delimited 
      If pEnding.Equals(".csv", StringComparison.OrdinalIgnoreCase) Then 
        pReader.SetDelimiters(",") 
      ElseIf pEnding.Equals(".txt", StringComparison.OrdinalIgnoreCase) Then 
        pReader.SetDelimiters(ControlChars.Tab) 
      Else 
        frmMessageOrInputBox.ShowMsg("txt or csv ONLY!!", frmMessageOrInputBox.enmIconType.Exclamation) 
        Exit Sub 
      End If 
      Dim pCurrentRow As String() 
      Dim pRow As Integer = -1 
 
      Dim pNoPrimaryKey As Boolean = False 
      While Not pReader.EndOfData 
        pRow += 1 
 
        Dim pFieldName As String = "" 
 
        Try 
          Dim pIncomingUserLoginKey As New csUserLoginKey(vWithParents:=clsEnums.enmLoadParent.DoNotLoad) 
          pIncomingUserLoginKey.Tag = "Row " & pRow.ToString 
          pCurrentRow = pReader.ReadFields() 
          If pRow = 0 Then 
            If pCurrentRow.Length = pNumberOfFields - 1 Then 
              pNoPrimaryKey = True 
              pFault = csUserLoginKeyCol.Delete(_Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
              pNumberOfFields = pNumberOfFields - 1 
            End If 
            Continue While 'Header line  
          End If 
 
          If pCurrentRow.Length <> pNumberOfFields Then 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ": There should be " & pNumberOfFields & " fields, but there are actually " & pCurrentRow.Length & " fields." 
            pIncomingUserLoginKeys.Add(pIncomingUserLoginKey) 
            Continue While 
          End If 
 
          Dim pFieldNo As Integer = -1 
 
          If pNoPrimaryKey = False Then 
            Try 
              pFieldNo += 1 
              pFieldName = "ID" 
              pIncomingUserLoginKey.ID = CType(pCurrentRow(pFieldNo), Long) 
            Catch ex As Exception 
              pErrorFound = True 
              pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
            End Try 
          End If 
 
          Try 
            pFieldNo += 1 
            pFieldName = "UserID" 
            pIncomingUserLoginKey.UserID = CType(pCurrentRow(pFieldNo), Long) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ApplicationName" 
            pIncomingUserLoginKey.ApplicationName = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ApplicationIdentifier" 
            pIncomingUserLoginKey.ApplicationIdentifier = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "KeyHashed" 
            pIncomingUserLoginKey.KeyHashed = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ExternalIPAtCreation" 
            pIncomingUserLoginKey.ExternalIPAtCreation = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "CountryAtCreation" 
            pIncomingUserLoginKey.CountryAtCreation = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "LastAccessTime" 
            pIncomingUserLoginKey.LastAccessTime = CType(pCurrentRow(pFieldNo), Date) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "LoggedLoginID" 
            pIncomingUserLoginKey.LoggedLoginID = CType(pCurrentRow(pFieldNo), Long) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUserLoginKey.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          If pIncomingUserLoginKey.Tag = "Row " & pRow.ToString Then 
            pIncomingUserLoginKey.Tag &= ": OK" 
          End If 
 
          pIncomingUserLoginKeys.Add(pIncomingUserLoginKey) 
        Catch ex As Exception 
          Cursor = Cursors.Default 
          Dim pFullMessage As String = "Thrown at Row: " & pRow & "; Field: " & pFieldName & "; Problem: " & ex.Message 
          frmMessageOrInputBox.ShowMsg("Critical Error!!" & Environment.NewLine & pFullMessage & Environment.NewLine & ex.ToString, frmMessageOrInputBox.enmIconType.CriticalError) 
          Exit Sub 
        End Try 
      End While 
 
      If pErrorFound = True Then 
        Cursor = Cursors.Default 
        Try 
          My.Computer.FileSystem.WriteAllText(pFilenameOut, pIncomingUserLoginKeys.ToCSV, False) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg("Errors were found during the update, but I couldn't write them to the output file." & Environment.NewLine & $"Please check that the output file {pFilenameOut} is closed.", frmMessageOrInputBox.enmIconType.Exclamation) 
          Return 
        End Try 
        frmMessageOrInputBox.ShowMsg("Errors were found before the update. The update was NOT done." & Environment.NewLine & "Please check the Tag column of the output file.", frmMessageOrInputBox.enmIconType.Exclamation) 
        Try 
          Shell("explorer.exe /n,/select," & pFilenameOut, AppWinStyle.NormalFocus, False) 
        Catch ex As Exception 
          frmMessageOrInputBox.ShowMsg("Could not show file!" & Environment.NewLine & ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
        End Try 
        Exit Sub 
      End If 
    End Using 
 
    'Now try to update it 
    pFault = pIncomingUserLoginKeys.UpdateFromCollection(_Requester) 
    If pFault.isOK = False Then 
      ShowFault(pFault, _Requester) 
      Exit Sub 
    End If 
    Cursor = Cursors.Default 
 
    'Check that there were no problems 
    pErrorFound = False 
    For Each p In pIncomingUserLoginKeys 
      If p.Tag <> "OK" Then 
        pErrorFound = True 
      End If 
    Next 
    If pErrorFound = True Then 
      Try 
        My.Computer.FileSystem.WriteAllText(pFilenameOut, pIncomingUserLoginKeys.ToCSV, False) 
      Catch ex As Exception 
        frmMessageOrInputBox.ShowMsg("Errors were found during the update, but I couldn't write them to the output file." & Environment.NewLine & $"Please check that the output file {pFilenameOut} is closed.", frmMessageOrInputBox.enmIconType.Exclamation) 
        Return 
      End Try 
      frmMessageOrInputBox.ShowMsg("Errors were found during the update." & Environment.NewLine & "Please check the Tag column of the output file." & Environment.NewLine & "Some items may have been updated.", frmMessageOrInputBox.enmIconType.Exclamation) 
      Try 
        Shell("explorer.exe /n,/select," & pFilenameOut, AppWinStyle.NormalFocus, False) 
      Catch ex As Exception 
        frmMessageOrInputBox.ShowMsg("Could not show file!" & Environment.NewLine & ex.Message, frmMessageOrInputBox.enmIconType.Exclamation) 
      End Try 
      Exit Sub 
    Else 
      _UserLoginKeyCol = pIncomingUserLoginKeys 
      LoadGrid() 
      frmMessageOrInputBox.ShowMsg("Update Successful! Please click on Refresh to see all the data", frmMessageOrInputBox.enmIconType.Information) 
    End If 
 
  End Sub 
 
  'ExternalButtons 
  Private Sub DoEdit() 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    Dim pCellRow As Integer = -1 
    Dim pCellCol As Integer = -1 
 
 
    If dgvUserLoginKey.Focused = True AndAlso dgvUserLoginKey.SelectedRows.Count > 0 Then 
      pCellRow = dgvUserLoginKey.CurrentCell.RowIndex 
      pCellCol = dgvUserLoginKey.CurrentCell.ColumnIndex 
    End If 
 
    Try 'in case it's empty 
      dgvUserLoginKey.CurrentCell = dgvUserLoginKey.Rows(0).Cells(0) 
      dgvUserLoginKey.CurrentCell.Selected = True 
    Catch ex As Exception 
    End Try 
 
 
    'remove summary row 
    If _LoadParameters.SummarizeGrid = True AndAlso _UserLoginKeyCol.Count > 0 AndAlso _UserLoginKeyCol(_UserLoginKeyCol.Count - 1).ID = 0 Then 
      _UserLoginKeyCol.RemoveAt(_UserLoginKeyCol.Count - 1) 
      bsCtlUserLoginKey.DataSource = Nothing 
      bsCtlUserLoginKey.DataSource = _UserLoginKeyCol 
      _Summarized = False 
    End If 
 
    SetUpBNButtons(True) 
    If pCellRow >= 0 AndAlso pCellCol >= 0 Then 
      dgvUserLoginKey.Focus() 
      dgvUserLoginKey.CurrentCell = dgvUserLoginKey.Rows(pCellRow).Cells(pCellCol) 
      dgvUserLoginKey.CurrentCell.Selected = True 
    ElseIf _UserLoginKeyCol.Count = 0 Then 
    Else 
      Try 'in case the cell is hidden.... 
        dgvUserLoginKey.CurrentCell = dgvUserLoginKey.Rows(0).Cells(0) 
        dgvUserLoginKey.CurrentCell.Selected = True 
      Catch ex As Exception 
      End Try 
    End If 
  End Sub 
  Private Function DoDelete() As clsFault 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
    
    If dgvUserLoginKey.CurrentCell Is Nothing Then Return pFault 
    
    If dgvUserLoginKey.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      Dim pUserLoginKey As csUserLoginKey 
      pUserLoginKey = CType(bsCtlUserLoginKey.Current, csUserLoginKey) 
      If pUserLoginKey Is Nothing Then 
        pFault.LogFreeTextFault("There is no UserLoginKey to delete", "", "TRGT-110303-165408", _Requester) 
        Return pFault 
      End If 
      Dim pOriginalCol As Integer = dgvUserLoginKey.CurrentCell.ColumnIndex 
      Dim pOriginalRow As Integer = dgvUserLoginKey.CurrentCell.RowIndex 
      'show row as selected  
      dgvUserLoginKey.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
      dgvUserLoginKey.EditMode = DataGridViewEditMode.EditProgrammatically 
      dgvUserLoginKey.CurrentRow.Selected = True 
      If pUserLoginKey.ID > 0 Then 
        Dim pRequest As String = "Are you sure you want to delete the row with a ID of '" & pUserLoginKey.ID.ToString & "'?" 
        Dim pCancel As Nullable(Of Boolean) = Nothing 
        RaiseEvent evtBeforeDelete(pUserLoginKey, pCancel) 
        If pCancel = True Then 
          Return pFault 
        ElseIf pCancel Is Nothing Then 
          Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
          pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
          If pResponse = frmMessageOrInputBox.enmButtonReturned.No Then 
            dgvUserLoginKey.SelectionMode = DataGridViewSelectionMode.CellSelect 
            dgvUserLoginKey.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 
            dgvUserLoginKey.Rows(pOriginalRow).Cells(pOriginalCol).Selected = True 
            Return pFault 
          End If 
        End If 
        pFault = pUserLoginKey.Delete(_Requester) : If pFault.isOK = False Then Return pFault 
      End If 
      bsCtlUserLoginKey.Remove(bsCtlUserLoginKey.Current) 
      LoadGrid() 
    End If 
    Return pFault 
  End Function 
  Private Sub DoCeaseEdit() 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True And _DVGDirty = False Then 
      bsCtlUserLoginKey.DataSource = _UserLoginKeyCol 
    End If 
    If _DVGDirty = True Then 
      RaiseEvent evtTimerTripped() 
      Exit Sub 
    End If 
    Dim pUserLoginKey As csUserLoginKey = CType(bsCtlUserLoginKey.Current, csUserLoginKey) 
    If pUserLoginKey IsNot Nothing Then 
      If pUserLoginKey.ID = 0 Then 
        _IgnoreGridFault = True 
        bsCtlUserLoginKey.Remove(bsCtlUserLoginKey.Current) 
        _IgnoreGridFault = False 
      End If 
    End If 
    SetUpBNButtons(False) 
    If _UserLoginKeyCol.Count > 0 AndAlso dgvUserLoginKey.CurrentCell IsNot Nothing Then 
      For i As Integer = 0 To dgvUserLoginKey.Columns.Count - 1 
        If dgvUserLoginKey.Columns(i).Visible Then 
          dgvUserLoginKey.CurrentCell = dgvUserLoginKey.Rows(dgvUserLoginKey.CurrentCell.RowIndex).Cells(i) 
          Exit For 
        End If 
      Next 
      dgvUserLoginKey.Refresh() 
      dgvUserLoginKey.Rows(dgvUserLoginKey.CurrentCell.RowIndex).Selected = True 
    Else 
      dgvUserLoginKey.Refresh() 
    End If 
  End Sub 
  'Grid RowValidating 
  Private Sub dgvUserLoginKey_RowValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvUserLoginKey.RowValidating 
    If _Loading = True OrElse dgvUserLoginKey.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True Then 
      e.Cancel = True 
      dgvUserLoginKey.CurrentCell = dgvUserLoginKey.Rows(e.RowIndex).Cells(e.ColumnIndex) 
    End If 
  End Sub 
  'CellFormatting  
  Private Sub dgvUserLoginKey_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvUserLoginKey.CellFormatting 
    '_Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    If dgvUserLoginKey.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      If dgvUserLoginKey.Columns(e.ColumnIndex).ReadOnly = False Then 
        Exit Sub 
      End If 
    End If 
 
    RaiseEvent evtCellFormatting(sender, e) 
 
    ' Sample code evtCellFormatting - evtCellFormatting 
    ' You can use this to colour the fonts or your cell background or anything else that requires complete control of your cell 
    'Dim pUserLoginKey As csUserLoginKey = Nothing 
    'If dgvUserLoginKey.Columns(e.ColumnIndex).Name = colRecommendedQuantityToOrder.Name Then 
    '  If pUserLoginKey Is Nothing Then pUserLoginKey = CType(dgvUserLoginKey.Rows(e.RowIndex).DataBoundItem, csUserLoginKey) ' Only assign it if needed 
    '  If pUserLoginKey.CustomerOrders > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pUserLoginKey.CustomerOrders > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
    'If dgvUserLoginKey.Columns(e.ColumnIndex).Name = colRAV.Name Then 
    '  If pUserLoginKey Is Nothing Then pUserLoginKey = CType(dgvUserLoginKey.Rows(e.RowIndex).DataBoundItem, csUserLoginKey) ' Only assign it if needed
    '  If pUserLoginKey.RAV > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pUserLoginKey.RAV - pUserLoginKey.MaximumStock > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
 
    'Debug.Print("loc x,y:" & e.RowIndex & ", " & e.ColumnIndex & ": GetType" & dgvUserLoginKey.Columns(e.ColumnIndex).GetType.ToString & ": zValue" & e.Value.ToString) 
    If dgvUserLoginKey.Columns(e.ColumnIndex).GetType.ToString = "System.Windows.Forms.DataGridViewComboBoxColumn" Then 
      Dim pCol As System.Windows.Forms.DataGridViewComboBoxColumn = CType(dgvUserLoginKey.Columns(e.ColumnIndex), System.Windows.Forms.DataGridViewComboBoxColumn) 
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
          e.Value = "* BadCode '" & dgvUserLoginKey.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & "' *" 
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
 
    If dgvUserLoginKey.Columns(e.ColumnIndex).GetType.Name.Equals("DataGridViewImageColumn", StringComparison.OrdinalIgnoreCase) Then 
      If e.Value Is Nothing Then 
        e.Value = New Bitmap(1, 1) 
      End If 
    End If 
 
    If _Summarized = True Then 
      If e.RowIndex = dgvUserLoginKey.Rows.Count - 1 Then 
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
            If _SummaryOverFlow.IndexOf(dgvUserLoginKey.Columns(e.ColumnIndex).Name.Substring(3)) >= 0 Then 
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
  Private Sub dgvUserLoginKey_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUserLoginKey.CellValueChanged 
    If e.RowIndex < 0 Then Exit Sub 
 
  End Sub 
 
  'Grid Sort
  Private Sub dgvUserLoginKey_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvUserLoginKey.ColumnHeaderMouseClick
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewColumn As DataGridViewColumn = dgvUserLoginKey.Columns(e.ColumnIndex)
    If bsCtlUserLoginKey.Current Is Nothing Then Exit Sub

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
    dgvUserLoginKey.SuspendLayout()

    Dim pUserLoginKey As csUserLoginKey
    Dim pID As Long = 0 
    If dgvUserLoginKey.SelectedRows.Count > 0 Then 
    pUserLoginKey = CType(bsCtlUserLoginKey.Current, csUserLoginKey)
      pID = pUserLoginKey.ID 
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
    Dim pUserLoginKeyCol As csUserLoginKeyCol
    pUserLoginKeyCol = CType(bsCtlUserLoginKey.DataSource, csUserLoginKeyCol)

    Dim pSummaryRow As csUserLoginKey = Nothing 
    If _Summarized = True Then 
      pSummaryRow = pUserLoginKeyCol(pUserLoginKeyCol.Count - 1) 
      pUserLoginKeyCol.RemoveAt(pUserLoginKeyCol.Count - 1) 
    End If 
 
    If pNewSortOrder = SortOrder.Ascending Then
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
        'save the present sort 
        For iCntr As Integer = 0 To pUserLoginKeyCol.Count - 1 
          pUserLoginKeyCol(iCntr).Tag = iCntr.ToString("0000000000") 
        Next 
      End If 
      If pNewColumn Is colID Then
        pUserLoginKeyCol.SortByID()
      ElseIf pNewColumn Is colUser OrElse pNewColumn Is colUserText Then
        pUserLoginKeyCol.SortByUserText()
      ElseIf pNewColumn Is colApplicationName Then
        pUserLoginKeyCol.SortByApplicationName()
      ElseIf pNewColumn Is colApplicationIdentifier Then
        pUserLoginKeyCol.SortByApplicationIdentifier()
      ElseIf pNewColumn Is colExternalIPAtCreation Then
        pUserLoginKeyCol.SortByExternalIPAtCreation()
      ElseIf pNewColumn Is colCountryAtCreation Then
        pUserLoginKeyCol.SortByCountryAtCreation()
      ElseIf pNewColumn Is colLastAccessTime Then
        pUserLoginKeyCol.SortByLastAccessTime()
      ElseIf pNewColumn Is colLoggedLoginID Then
        pUserLoginKeyCol.SortByLoggedLoginID()
      End If
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
      Dim iCntr As Integer = 0 
        If pNewColumn Is colID Then
          Dim pTest As Long = 0 
          For Each p As csUserLoginKey In pUserLoginKeyCol 
            If p.ID <> pTest Then iCntr += 1 : pTest = p.ID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUser OrElse pNewColumn Is colUserText Then
          Dim pTest As String = "" 
          For Each p As csUserLoginKey In pUserLoginKeyCol 
            If p.UserText <> pTest Then iCntr += 1 : pTest = p.UserText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colApplicationName Then
          Dim pTest As String = "" 
          For Each p As csUserLoginKey In pUserLoginKeyCol 
            If p.ApplicationName <> pTest Then iCntr += 1 : pTest = p.ApplicationName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colApplicationIdentifier Then
          Dim pTest As String = "" 
          For Each p As csUserLoginKey In pUserLoginKeyCol 
            If p.ApplicationIdentifier <> pTest Then iCntr += 1 : pTest = p.ApplicationIdentifier 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colExternalIPAtCreation Then
          Dim pTest As String = "" 
          For Each p As csUserLoginKey In pUserLoginKeyCol 
            If p.ExternalIPAtCreation <> pTest Then iCntr += 1 : pTest = p.ExternalIPAtCreation 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colCountryAtCreation Then
          Dim pTest As String = "" 
          For Each p As csUserLoginKey In pUserLoginKeyCol 
            If p.CountryAtCreation <> pTest Then iCntr += 1 : pTest = p.CountryAtCreation 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLastAccessTime Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csUserLoginKey In pUserLoginKeyCol 
            If p.LastAccessTime <> pTest Then iCntr += 1 : pTest = p.LastAccessTime 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLoggedLoginID Then
          Dim pTest As Long = 0 
          For Each p As csUserLoginKey In pUserLoginKeyCol 
            If p.LoggedLoginID <> pTest Then iCntr += 1 : pTest = p.LoggedLoginID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        End If 
        pUserLoginKeyCol.SortByTag() 
      End If 
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
      _PrevSortColumn = pNewColumn
      pPrevSortOrder = SortOrder.Ascending
    Else
      pUserLoginKeyCol.Reverse()
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
      pPrevSortOrder = SortOrder.Descending
    End If

    If _Summarized = True Then 
      pUserLoginKeyCol.Add(pSummaryRow) 
    End If 
 
    If pID > 0 Then
      bsCtlUserLoginKey.Position = bsCtlUserLoginKey.IndexOf(pUserLoginKeyCol.FindByID(pID))
    End If

    'dgvUserLoginKey.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
    dgvUserLoginKey.ResumeLayout()

    Cursor = Cursors.Default
    dgvUserLoginKey.Refresh()

  End Sub
  'Select Row 
  Public Sub SelectRowByObjectID(ByVal pID As Long) 
    If pID > 0 Then 
      Dim pUserLoginKeyCol As csUserLoginKeyCol 
      pUserLoginKeyCol = CType(bsCtlUserLoginKey.DataSource, csUserLoginKeyCol) 
      Dim pUserLoginKey As csUserLoginKey = pUserLoginKeyCol.FindByID(pID) 
      If Not pUserLoginKey.IsEmpty Then 
        bsCtlUserLoginKey.Position = bsCtlUserLoginKey.IndexOf(pUserLoginKeyCol.FindByID(pID)) 
        dgvUserLoginKey.Rows(bsCtlUserLoginKey.Position).Selected = True 
      Else 
        dgvUserLoginKey.ClearSelection() 
      End If 
    ElseIf pID = 0 Then 
      dgvUserLoginKey.ClearSelection() 
    End If 
  End Sub 
  
  'Grid Resize
  Private Sub dgvUserLoginKey_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvUserLoginKey.ColumnHeaderMouseDoubleClick
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    dgvUserLoginKey.AutoResizeColumn(e.ColumnIndex)
    Cursor = Cursors.Default
  End Sub
  'Other Grid Events
  Private Sub dgvUserLoginKey_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvUserLoginKey.CurrentCellDirtyStateChanged
   _DVGDirty = True 
  End Sub
  Private Sub dgvUserLoginKey_Scroll(sender As Object, e As ScrollEventArgs) Handles dgvUserLoginKey.Scroll
    dgvUserLoginKey.Invalidate() 
  End Sub
 
  Private Sub dgvUserLoginKey_DataFault(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvUserLoginKey.DataError
    'Dim pFault As New clsFault
    '
    'If e.RowIndex = dgvUserLoginKey.Rows.Count - 1 Then Exit Sub
 
    'If dgvUserLoginKey.Columns(e.ColumnIndex).Name.StartsWith("colIDin", StringComparison.OrdinalIgnoreCase) Then Exit Sub 
 
    'If _IgnoreGridFault = True Then Exit Sub
    '_DVGDirty = False 
    'Static pShown As Boolean 
    '
    'Dim pSubStrg As New System.Text.StringBuilder 
    ''Other Error 
    'Try 
    '  Try 
    '    pSubStrg.AppendLine("In table 'UserLoginKey', the row with an ID of " & dgvUserLoginKey.Rows(e.RowIndex).Cells(0).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine("In grid 'UserLoginKey', row index " & e.RowIndex) 
    '  End Try 
    '  Try 
    '    pSubStrg.AppendLine(" has an invalid value of " & dgvUserLoginKey.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine(" has an invalid value of Nothing.") 
    '  End Try 
    '  pSubStrg.AppendLine(" in column " & dgvUserLoginKey.Columns(e.ColumnIndex).DataPropertyName) 
    'Catch ex As Exception 
    '  pSubStrg.AppendLine("; Failed trying to fill DataFault as well!") 
    'End Try 
    'pFault.LogException(209, e.Exception, pSubStrg.ToString, "TRGT-UserLoginKey-100409-2248", _Requester) 
    'If pShown = False Then 
    '  Dim pCell As DataGridViewCell 
    '  Try 
    '    pCell = dgvUserLoginKey(e.ColumnIndex, e.RowIndex)
    '  Catch ex As Exception 
    '    pCell = dgvUserLoginKey(0, 0)
    '  End Try 
    '  ShowFault(pFault, _Requester) 
    '  pShown = True 
    'End If 
  End Sub
  Private Sub dgvUserLoginKey_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvUserLoginKey.KeyDown 
    If e.KeyCode = Keys.Escape Then 
      'DoCeaseEdit() 
      Dim pUserLoginKey As csUserLoginKey = CType(bsCtlUserLoginKey.Current, csUserLoginKey) 
      If pUserLoginKey IsNot Nothing Then 
        If pUserLoginKey.ID = 0 Then 
          _IgnoreGridFault = True 
          bsCtlUserLoginKey.Remove(bsCtlUserLoginKey.Current) 
          _IgnoreGridFault = False 
        End If 
      End If 
      SetUpBNButtons(False) 
      'dgvUserLoginKey.CurrentCell = dgvUserLoginKey.Rows(dgvUserLoginKey.CurrentCell.RowIndex).Cells(0) 
      dgvUserLoginKey.Refresh() 
    End If 
  End Sub 
  Private Sub dgvUserLoginKey_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvUserLoginKey.ColumnWidthChanged
    If Me.DesignMode = True Then Exit Sub 
    If _Loading = False Then SaveSizes()
  End Sub
  Private Sub dgvUserLoginKey_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvUserLoginKey.ColumnDisplayIndexChanged
    Cursor = Cursors.WaitCursor
    If _Loading = False Then SaveSizes()
    Cursor = Cursors.Default
  End Sub
  Private Sub dgvUserLoginKey_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUserLoginKey.CellDoubleClick 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso e.RowIndex = dgvUserLoginKey.Rows.Count - 1 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
    Dim pCell As DataGridViewCell = dgvUserLoginKey(e.ColumnIndex, e.RowIndex) 
 
    Dim pHandled As Boolean = False 
    Dim pUserLoginKey As csUserLoginKey = _UserLoginKeyCol(e.RowIndex)
    RaiseEvent evtRowDoubleClicked(pUserLoginKey, pHandled) 
    Cursor = Cursors.Default 
 
    If pHandled = False Then 
      If Me.ParentForm.Name.Equals("frmPopup", StringComparison.OrdinalIgnoreCase) Then Return 
      frmPopup.Text = "UserLoginKey Detail" 
      Dim pFault As clsFault = frmPopup.LoadControl("ctlc_UserLoginKey", pUserLoginKey, _Requester) 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
      frmPopup.ShowDialog() 
    End If 
 
  End Sub 
  Private Sub dgvUserLoginKey_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvUserLoginKey.SelectionChanged 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If _Loading = True Then Exit Sub 
    If DateTime.Now < _IgnoreSelectionUntil Then Exit Sub
    If _ProcessingSelection Then Exit Sub
    If dgvUserLoginKey.SelectedRows.Count = 0 Then 
      RaiseEvent evtUnChosen() 
      Exit Sub 
    End If 
    Dim RowIndex As Integer = dgvUserLoginKey.SelectedRows(0).Cells(0).RowIndex 
    If RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso RowIndex = dgvUserLoginKey.Rows.Count - 1 Then dgvUserLoginKey.ClearSelection() : RaiseEvent evtUnChosen() : Exit Sub 
    Dim pUserLoginKey As csUserLoginKey = _UserLoginKeyCol(RowIndex)
    _ProcessingSelection = True
    Try
      RaiseEvent evtRowClicked(pUserLoginKey) 
    Finally
      _IgnoreSelectionUntil = DateTime.Now.AddMilliseconds(500)
      _ProcessingSelection = False
    End Try
  End Sub 
  Private Sub dgvUserLoginKey_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUserLoginKey.RowLeave 
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
  Private Function UpdateRow() As Boolean 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    Dim pCancel As Boolean = False 
    If _DVGDirty = False Then Return False 
 
    Dim pOriginalCol As Integer = dgvUserLoginKey.CurrentCell.ColumnIndex 
     
    'If user clicked on CeaseEdit without changing cells, the data will not be received 
    ' therefore we have to fake exiting the cell 
    Dim pNewCol As Integer 
    'We can only go to a visible cell! 
    If pOriginalCol > 0 Then 
      pNewCol = pOriginalCol - 1 
      Do Until dgvUserLoginKey.Columns(pNewCol).Visible = True OrElse pNewCol = 0 
        pNewCol = pNewCol - 1 
      Loop 
    Else 
      pNewCol = 1 
    End If 
    If dgvUserLoginKey.Columns(pNewCol).Visible = False Then 
      dgvUserLoginKey.Columns(pNewCol).Visible = True 
      dgvUserLoginKey.CurrentCell = dgvUserLoginKey.CurrentRow.Cells(pNewCol) 
      dgvUserLoginKey.CurrentCell = dgvUserLoginKey.CurrentRow.Cells(pOriginalCol) 
      dgvUserLoginKey.Columns(pNewCol).Visible = False 
    Else 
      dgvUserLoginKey.CurrentCell = dgvUserLoginKey.CurrentRow.Cells(pNewCol) 
      dgvUserLoginKey.CurrentCell = dgvUserLoginKey.CurrentRow.Cells(pOriginalCol) 
    End If 
    dgvUserLoginKey.Rows(dgvUserLoginKey.CurrentCell.RowIndex).Selected = True 
    Dim pUserLoginKey As csUserLoginKey 
    pUserLoginKey = CType(bsCtlUserLoginKey.Current, csUserLoginKey) 
 
    'Add required data (primary keys) from parent objects  
    RaiseEvent evtBeforeUpdate(CType(pUserLoginKey, csUserLoginKey), pCancel) 
    If pCancel = True Then 
      _DVGDirty = False 
      RaiseEvent evtTimerTripped() 
      Return True 
    End If 
    pFault = pUserLoginKey.Update(_Requester) 
    If pFault.isOK = False AndAlso pFault.Severity <> clsEnums.enmFaultSeverity.LogOnly Then 
      ShowFault(pFault, _Requester) 
      frmMessageOrInputBox.ShowMsg("Fix the problem, or click on 'Esc' to remove the row.", frmMessageOrInputBox.enmIconType.Information, frmMessageOrInputBox.enmButtons.Yes) 
      Return True 
    Else 
      If pFault.isOK = False Then 'AndAlso pFault.Severity = clsEnums.enmFaultSeverity.LogOnly  
        ShowFault(pFault, _Requester) 
      End If 
      dgvUserLoginKey.EndEdit() 
      _DVGDirty = False 
      RaiseEvent evtUpdated(pUserLoginKey) 
      Return False 
    End If 
  End Function 
  Private Sub SaveSizes() 
    ' Save column state data  
    ' including order, column width and whether or not the column is visible  
    For Each pCol As DataGridViewColumn In dgvUserLoginKey.Columns 
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
    If _UserLoginKeyCol.Count <= 1 Then 
      _Summarized = False 
      Exit Sub 
    End If 
 
    Dim pLoggedLoginID As Long 
    For Each pExistingRow As csUserLoginKey In _UserLoginKeyCol 
      If _SummaryOverFlow.IndexOf("#LoggedLoginID#") < 0 Then 
        Try 
          pLoggedLoginID += pExistingRow.LoggedLoginID 
        Catch ex As System.OverflowException 
          pLoggedLoginID = -99999999 
          _SummaryOverFlow &= "LoggedLoginID#" 
        End Try 
      End If 
    Next 
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csUserLoginKey.enmSummarizeableProperty.LoggedLoginID) = csUserLoginKey.enmSummarizeableProperty.LoggedLoginID Then pLoggedLoginID = 0
    Dim pSummaryRow As New csUserLoginKey( _ 
        vID:=0 _ 
      , vUserID:=0 _ 
      , vUserText:="" _ 
      , vApplicationName:="" _ 
      , vApplicationIdentifier:="" _ 
      , vKeyHashed:="" _ 
      , vExternalIPAtCreation:="" _ 
      , vCountryAtCreation:="" _ 
      , vLastAccessTime:=Nothing _ 
      , vLoggedLoginID:=pLoggedLoginID _ 
      , vTag:="" _ 
      , vDateAdded:=Nothing _ 
      , vWithParents:=clsEnums.enmLoadParent.TextOnly _ 
      )
    _UserLoginKeyCol.Add(pSummaryRow) 
    _Summarized = True 
  End Sub 
  
  'Reports and Excel 
  Friend Function CreateSpreadSheet() As clsFault  
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name  
    Dim pFault As New clsFault  
    'Dim pExcel As New Tools.ExcelSheet  
    Dim pDateToShow As String = DateTime.Now.ToString("yyMMdd_HHmmss")  
    Dim pRoot As String = $"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles" 
 
    Dim pFileNameAllFields As String = $"{pRoot}\UserLoginKeyCol_{pDateToShow}AllFields.csv" 
    Dim pFileNameFieldsOnGrid As String = $"{pRoot}\UserLoginKeyCol_{pDateToShow}FieldsOnGrid.csv" 
    Dim pFileNameAllFieldsWithIDs As String = $"{pRoot}\UserLoginKeyCol_{pDateToShow}AllFieldsWithIDs.csv" 
    Dim pFileNameAllFieldsXML As String = $"{pRoot}\UserLoginKeyCol_{pDateToShow}AllFields.xml" 
    Dim pFileNameAllFieldsJson As String = $"{pRoot}\UserLoginKeyCol_{pDateToShow}AllFields.json" 
 
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
    For Each pCol As DataGridViewColumn In dgvUserLoginKey.Columns 
      If pCol.Visible = True Then 
        pTmpStrg.Append(",""" & pCol.HeaderText & """") 
      End If 
    Next 
    pCSV.AppendLine(pTmpStrg.ToString.Substring(1)) 
 
    'Now the data  
    Dim i As Integer 
    Dim pStart As Date = Now 
 
    Dim pTruncatedFieldNames As String = "" 
    For Each Row As DataGridViewRow In dgvUserLoginKey.Rows 
      i += 1 
      If _LoadParameters.SummarizeGrid = True Then 
        If Row.Index = dgvUserLoginKey.Rows.Count - 1 Then Exit For 
      End If 
      If i Mod 500 = 0 Then 
        lblStatus.Text = " Writing Row " & i & ". Time Elapsed: " & DateTime.Now.Subtract(pStart).TotalSeconds().ToString("###0") & " sec" : Application.DoEvents() 
      End If 
      pTmpStrg = New System.Text.StringBuilder 
       
      For Each pCol As DataGridViewColumn In dgvUserLoginKey.Columns 
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
        pFault = _UserLoginKeyColFullLength.CreateXML(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsXML, pStrg, False) 
        'json 
        pFault = _UserLoginKeyColFullLength.CreateJSON(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsJson, pStrg, False) 
        'default  
        My.Computer.FileSystem.WriteAllText(pFileNameAllFields, _UserLoginKeyColFullLength.ToCSV, False)  
        'WithIDs  
        'pFault = _UserLoginKeyColFullLength.LoadLookupAndEnumText(_Requester) : If pFault.isOK = False Then Return pFault (already done) 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsWithIDs, _UserLoginKeyColFullLength.ToCSV(True), False) 
      End If  
      'default  
      My.Computer.FileSystem.WriteAllText(pFileNameFieldsOnGrid, pCSV.ToString, False)  
      pFault.SetOK()  
    Catch ex As Exception  
      pFault.LogException(ex, "", "TRGT-UserLoginKey-090210-1618", _Requester)  
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
      pFault.LogException(ex, "", "TRGT-UserLoginKey-090210-1618", _Requester)  
    End Try  
  
    If pFault.isOK = False Then Return pFault  
    
    Return pFault  
  End Function  
 
  Private Sub ReportDesign() 
 
    _Report = New vbReport.ReportDocument 
    _Report.AutoDiscover = False 
    Try 
      For Each pCol As DataGridViewColumn In dgvUserLoginKey.Columns 
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
        .SubTitleLeft = "UserLoginKeies" 
        .SubTitleRight = "Rows: " & _UserLoginKeyCol.Count.ToString 
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
        .DataSource = _UserLoginKeyCol 
        .HasSummaryLine = _Summarized 
      End With 
    Catch ex As Exception 
      Dim pFault As New clsFault 
      pFault.LogException(ex, "", "TRGT-UserLoginKey-090210-2119", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
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
      pFault.LogException(ex, "", "TRGT-UserLoginKey-090211-0746", _Requester) 
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
      For Each pRow As DataGridViewRow In dgvUserLoginKey.Rows 
        Try : pRow.Visible = True : Catch : End Try 
      Next 
      lblStatus.ForeColor = Color.DarkGreen 
      lblStatus.Text = dgvUserLoginKey.RowCount & " rows" 
      Exit Sub 
    End If 
    ' Hide rows that don't match search text 
    For Each row As DataGridViewRow In dgvUserLoginKey.Rows 
      Dim pVisible As Boolean = False 
      For Each cell As DataGridViewCell In row.Cells 
        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(pSearchText) Then 
          pVisible = True : Exit For 
        End If 
      Next 
      Try 
        Dim pBS As CurrencyManager = CType(Me.BindingContext(bsCtlUserLoginKey), CurrencyManager) 
        row.Visible = pVisible 
      Catch : End Try 
    Next 
    Dim pVisibleCount As Integer = 0 
    For Each row As DataGridViewRow In dgvUserLoginKey.Rows 
      If row.Visible Then pVisibleCount += 1 
    Next 
    lblStatus.ForeColor = Color.DarkBlue 
    lblStatus.Text = pVisibleCount & " of " & dgvUserLoginKey.RowCount & " rows" 
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
      pFault.LogException(ex, "GetOrInitializeGridSettings", "TRGT-UserLoginKey-120225-1310", _Requester) 
    End Try 
    If pFault.isOK = False Then Return pFault 
 
    Dim pSaveInitial As Boolean = False 
    
    '_GridSettings.Clear() Use for testing 
    If _GridSettings.Count = 0 Then 
      For Each pCol As DataGridViewColumn In dgvUserLoginKey.Columns 
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
      For Each pCol As DataGridViewColumn In dgvUserLoginKey.Columns 
        Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
        If pG.ColumnName = "" Then 
          pG.ColumnDisplayIndex = pCol.DisplayIndex 
          pG.ColumnWidth = ccHelper.ToInteger((dgvUserLoginKey.Width - 30) / dgvUserLoginKey.Columns.Count) 
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
    'colApplicationIdentifier.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colExternalIPAtCreation.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    
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
           
          With dgvUserLoginKey.Columns(lGridSetting.ColumnName) 
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
      pFault.LogException(204, ex, "", "TRGT-UserLoginKey-090120-1502", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
    End Try 
  End Sub 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "ID", _Requester) 
    If pStrg <> "" Then colID.HeaderText = pStrg : mnuColVisibleID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "User", _Requester) 
    If pStrg <> "" Then colUser.HeaderText = pStrg : mnuColVisibleUser.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "ApplicationName", _Requester) 
    If pStrg <> "" Then colApplicationName.HeaderText = pStrg : mnuColVisibleApplicationName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "ApplicationIdentifier", _Requester) 
    If pStrg <> "" Then colApplicationIdentifier.HeaderText = pStrg : mnuColVisibleApplicationIdentifier.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "KeyHashed", _Requester) 
    If pStrg <> "" Then colKeyHashed.HeaderText = pStrg : mnuColVisibleKeyHashed.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "ExternalIPAtCreation", _Requester) 
    If pStrg <> "" Then colExternalIPAtCreation.HeaderText = pStrg : mnuColVisibleExternalIPAtCreation.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "CountryAtCreation", _Requester) 
    If pStrg <> "" Then colCountryAtCreation.HeaderText = pStrg : mnuColVisibleCountryAtCreation.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "LastAccessTime", _Requester) 
    If pStrg <> "" Then colLastAccessTime.HeaderText = pStrg : mnuColVisibleLastAccessTime.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_UserLoginKey", "LoggedLoginID", _Requester) 
    If pStrg <> "" Then colLoggedLoginID.HeaderText = pStrg : mnuColVisibleLoggedLoginID.Text = pStrg
 
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
 
  Private Sub mnuColVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColVisibleID.Click, mnuColVisibleUser.Click, mnuColVisibleApplicationName.Click, mnuColVisibleApplicationIdentifier.Click, mnuColVisibleKeyHashed.Click, mnuColVisibleExternalIPAtCreation.Click, mnuColVisibleCountryAtCreation.Click, mnuColVisibleLastAccessTime.Click, mnuColVisibleLoggedLoginID.Click
    Cursor = Cursors.WaitCursor 
    Dim pToolStripItem As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem) 
    dgvUserLoginKey.Columns("col" & pToolStripItem.Name.Substring(13)).Visible = pToolStripItem.Checked 
    If _Loading = False Then SaveSizes() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsReset.Click 
    Cursor = Cursors.WaitCursor 
    dgvUserLoginKey.SuspendLayout() 
 
    For Each pCol As DataGridViewColumn In dgvUserLoginKey.Columns 
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
    pNewWidth = ccHelper.ToInteger((dgvUserLoginKey.Width - 30) / pVisibleColumns) 
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
     
    dgvUserLoginKey.ResumeLayout() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsHideMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsHideMost.Click 
 
    _Loading = True 
    'Hide All 
    If mnuColVisibleID.Checked = True Then mnuColVisibleID.PerformClick() 
    If mnuColVisibleUser.Checked = True Then mnuColVisibleUser.PerformClick() 
    If mnuColVisibleApplicationName.Checked = True Then mnuColVisibleApplicationName.PerformClick() 
    If mnuColVisibleApplicationIdentifier.Checked = True Then mnuColVisibleApplicationIdentifier.PerformClick() 
    If mnuColVisibleKeyHashed.Checked = True Then mnuColVisibleKeyHashed.PerformClick() 
    If mnuColVisibleExternalIPAtCreation.Checked = True Then mnuColVisibleExternalIPAtCreation.PerformClick() 
    If mnuColVisibleCountryAtCreation.Checked = True Then mnuColVisibleCountryAtCreation.PerformClick() 
    If mnuColVisibleLastAccessTime.Checked = True Then mnuColVisibleLastAccessTime.PerformClick() 
    If mnuColVisibleLoggedLoginID.Checked = True Then mnuColVisibleLoggedLoginID.PerformClick() 
    
    _Loading = False 
    'dgvUserLoginKey.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
  End Sub 
  
  Private Sub dgvUserLoginKey_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvUserLoginKey.CellMouseClick 
    If e.Button = MouseButtons.Right Then 
      Dim pMessageBox As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the UserLoginKey to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pMessageBox <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pUserLoginKey As csUserLoginKey = _UserLoginKeyCol(e.RowIndex) 
        If pMessageBox = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pUserLoginKey.ToCSV) 
        Else 
          Clipboard.SetText(pUserLoginKey.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The UserLoginKey is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvUserLoginKey_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvUserLoginKey.MouseDown 
    '--- Save anchor on normal click (no modifiers) ---
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And (Keys.Shift Or Keys.Control)) = 0 AndAlso dgvUserLoginKey.CurrentRow IsNot Nothing Then 
      _SelectionAnchor = dgvUserLoginKey.CurrentRow.Index 
    End If 
    'This removes on click from the update 
    If dgvUserLoginKey.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
 
    Dim pCell As DataGridView.HitTestInfo = dgvUserLoginKey.HitTest(e.X, e.Y) 
 
    If pCell.Type = DataGridViewHitTestType.Cell Then 
      'Enable edit force it to be current 
      Dim pCurrentCell As DataGridViewCell = Nothing 
      Try 
        pCurrentCell = dgvUserLoginKey(pCell.ColumnIndex, pCell.RowIndex) 
        If pCurrentCell.ReadOnly Then Exit Sub 
        dgvUserLoginKey.CurrentCell = pCurrentCell 
      Catch ex As Exception 
        Exit Sub  
      End Try 
      'make the combobox drop down if it's active 
      If pCurrentCell.GetType().Name.Equals("DataGridViewComboBoxCell", StringComparison.OrdinalIgnoreCase) Then 
        dgvUserLoginKey.BeginEdit(True) 
        DirectCast(dgvUserLoginKey.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvUserLoginKey_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvUserLoginKey.MouseUp 
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And Keys.Shift) = Keys.Shift AndAlso _SelectionAnchor >= 0 Then 
      Dim hit = dgvUserLoginKey.HitTest(e.X, e.Y) 
      If hit.RowIndex >= 0 Then 
        dgvUserLoginKey.MultiSelect = True 
        dgvUserLoginKey.ClearSelection() 
        Dim pFrom As Integer = Math.Min(_SelectionAnchor, hit.RowIndex) 
        Dim pTo As Integer = Math.Max(_SelectionAnchor, hit.RowIndex) 
        For i As Integer = pFrom To pTo 
          dgvUserLoginKey.Rows(i).Selected = True 
        Next 
      End If 
    ElseIf (pModifiers And Keys.Control) = Keys.Control Then 
      dgvUserLoginKey.MultiSelect = True 
    Else 
      dgvUserLoginKey.MultiSelect = False 
    End If 
  End Sub 
 
  Private Sub chkAutoRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoRefresh.CheckedChanged 
    If chkAutoRefresh.Checked Then 
      _PrevSortColumn = Nothing 
 
      Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(colID.Index, -1, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton2, 0, 0, 0, 0)) 
      dgvUserLoginKey_ColumnHeaderMouseClick(Me, pE) 
      Application.DoEvents() 
      dgvUserLoginKey_ColumnHeaderMouseClick(Me, pE) 
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
 
  Private Sub ctlc_UserLoginKeyCol_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
    'Set the font for the BN 
        If MyFont Is Nothing Then Return 
    BN.Font = New Font(MyFont.Name, MyFont.Size) 
    dgvUserLoginKey.RowTemplate.Height = ccHelper.ToInteger(23 * MyFont.Size / 9) 
  End Sub 
 
  Private Sub ctlc_UserLoginKeyCol_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    If Me.ParentForm Is Nothing Then Exit Sub 
    Dim pParent As String = Me.ParentForm.Name 
    Dim pResponse As Boolean = Me.Visible 
    Dim pSize As Integer = dgvUserLoginKey.Width 
 
    'now set sizes if needed 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_UserLoginKeyCol_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True AndAlso Not Me.Parent.Name.StartsWith("pnl", StringComparison.OrdinalIgnoreCase) Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_UserLoginKeyCol_Leave(sender As Object, e As EventArgs) Handles Me.Leave 
    If _Requester Is Nothing Then Return 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    DoCeaseEdit() 
  End Sub 
  'Track open detail windows to prevent duplicates 
  Private Shared _openDetailWindows As New Dictionary(Of String, Form)() 
 
  'Context menu - right-click: add to selection if not already selected, otherwise keep multi-selection 
  Private Sub dgvUserLoginKey_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvUserLoginKey.CellMouseDown 
    ReleaseStuckModifierKeys() 'Fix sticky SHIFT/CTRL before selection changes 
    If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then 
      If Not dgvUserLoginKey.Rows(e.RowIndex).Selected Then 
        dgvUserLoginKey.ClearSelection() 
        dgvUserLoginKey.Rows(e.RowIndex).Selected = True 
      End If 
    End If 
  End Sub 
 
  'Context menu - Opening: adjust items based on single/multi selection 
  Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening 
    Dim pCount As Integer = dgvUserLoginKey.SelectedRows.Count 
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
    If dgvUserLoginKey.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvUserLoginKey.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _UserLoginKeyCol.Count Then Exit Sub 
    Dim pUserLoginKey As csUserLoginKey = _UserLoginKeyCol(pRowIndex) 
    Dim pTitle As String = "UserLoginKey #" & pUserLoginKey.ID.ToString() 
    Dim pKey As String = "UserLoginKey_" & pUserLoginKey.ID.ToString() 
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
    Dim pCtlName As String = "ctlc_UserLoginKey" 
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
    Dim pFault As clsFault = CType(pLoad.Invoke(pControl, New Object() {pUserLoginKey, _Requester}), clsFault) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    'Return to Tab click handler - sends entity to a new tab in frmMain 
    Dim pEntityRef As Object = pUserLoginKey 
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
    If dgvUserLoginKey.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvUserLoginKey.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _UserLoginKeyCol.Count Then Exit Sub 
    Dim pUserLoginKey As csUserLoginKey = _UserLoginKeyCol(pRowIndex) 
    Dim pFrmMain As frmMain = Nothing 
    For Each pForm As Form In Application.OpenForms 
      If TypeOf pForm Is frmMain Then 
        pFrmMain = CType(pForm, frmMain) 
        Exit For 
      End If 
    Next 
    If pFrmMain Is Nothing Then Exit Sub 
    'Check if already open in a window - if so, bring to front instead 
    Dim pWinKey As String = "UserLoginKey_" & pUserLoginKey.ID.ToString() 
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
    Dim pTabTitle As String = "UserLoginKey #" & pUserLoginKey.ID.ToString() 
    Dim pFault As clsFault = pFrmMain.OpenEntityInNewTab("ctlc_UserLoginKey", pUserLoginKey, _Requester, pTabTitle) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Context menu - Copy ID (supports multi-select) 
  Private Sub tsmiCopyID_Click(sender As Object, e As EventArgs) Handles tsmiCopyID.Click 
    If dgvUserLoginKey.SelectedRows.Count = 0 Then Exit Sub 
    Dim pIDs As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvUserLoginKey.SelectedRows 
      If pSelectedRow.Index >= 0 AndAlso pSelectedRow.Index < _UserLoginKeyCol.Count Then 
        Dim pUserLoginKey As csUserLoginKey = _UserLoginKeyCol(pSelectedRow.Index) 
        If pIDs.Length > 0 Then pIDs.Append(", ") 
        pIDs.Append(pUserLoginKey.ID.ToString()) 
      End If 
    Next 
    If pIDs.Length > 0 Then 
      Clipboard.SetText(pIDs.ToString()) 
      Dim pCount As Integer = dgvUserLoginKey.SelectedRows.Count 
      ShowToast(If(pCount = 1, "ID copied: " & pIDs.ToString(), pCount.ToString() & " IDs copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows (supports multi-select, values only) 
  Private Sub tsmiCopyRow_Click(sender As Object, e As EventArgs) Handles tsmiCopyRow.Click 
    If dgvUserLoginKey.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvUserLoginKey.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvUserLoginKey.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row copied", pCount.ToString() & " rows copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows with Headers (supports multi-select) 
  Private Sub tsmiCopyRowHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopyRowHeaders.Click 
    If dgvUserLoginKey.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers from first row 
    Dim pFirstRow As DataGridViewRow = dgvUserLoginKey.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add all selected rows 
    For Each pSelectedRow As DataGridViewRow In dgvUserLoginKey.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvUserLoginKey.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row with headers copied", pCount.ToString() & " rows with headers copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy for Excel (with headers, VARCHAR fields wrapped in ="value" to preserve leading zeros) 
  Private Sub tsmiCopyExcel_Click(sender As Object, e As EventArgs) Handles tsmiCopyExcel.Click 
    If dgvUserLoginKey.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers 
    Dim pFirstRow As DataGridViewRow = dgvUserLoginKey.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add rows with Excel-safe formatting for text columns 
    For Each pSelectedRow As DataGridViewRow In dgvUserLoginKey.SelectedRows 
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
      Dim pCount As Integer = dgvUserLoginKey.SelectedRows.Count 
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
