Public Class ctlc_MFACol
 
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
 
  Public Event evtBeforeUpdate(ByVal vMFA As csMFA, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vMFA As csMFA) 
  Private Event evtBeforeDelete(ByVal vMFA As csMFA, ByRef rCancel As Nullable(Of Boolean)) 
  
  Public Event evtRowClicked(ByVal vMFA As csMFA) 
  Public Event evtRowDoubleClicked(ByVal vMFA As csMFA, ByRef rHandled As Boolean) 
  Public Event evtUnChosen() 
 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csMFA.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  
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
    Public Property DoNotSummarizeProperties As List(Of csMFA.enmSummarizeableProperty) 
    Public Property SpreadsheetShowAllFields As Nullable(Of Boolean) 
    Public Property GridTitle As String 
    Public Property ReportTitle As String 
    Public Property [ReadOnly] As Boolean 
    Public Property CbosDoNotLoad As List(Of csMFA.enmParentProperty) 
    Public Property ColumnsReadOnly As List(Of csMFA.enmProperty) 
    Public Property ColumnsHide As List(Of csMFA.enmProperty) 
    Public Property ColumnsFormat As Dictionary(Of csMFA.enmProperty, String) 
    Public Property ColumnsOrdinalPosition As Dictionary(Of csMFA.enmProperty, Integer) 
    Public Property ColumnsAlignment As Dictionary(Of csMFA.enmProperty, DataGridViewContentAlignment) 
    Public Property ColumnsHeaderText As Dictionary(Of csMFA.enmProperty, String) 
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
      _DoNotSummarizeProperties = New List(Of csMFA.enmSummarizeableProperty) 
      _SpreadsheetShowAllFields = Nothing 
      _GridTitle = "" 
      _ReportTitle = "" 
      _ReadOnly = False 
      _CbosDoNotLoad = New List(Of csMFA.enmParentProperty) 
      _ColumnsReadOnly = New List(Of csMFA.enmProperty) 
      _ColumnsHide = New List(Of csMFA.enmProperty) 
      _ColumnsFormat = New Dictionary(Of csMFA.enmProperty, String) 
      _ColumnsOrdinalPosition = New Dictionary(Of csMFA.enmProperty, Integer) 
      _ColumnsAlignment = New Dictionary(Of csMFA.enmProperty, DataGridViewContentAlignment) 
      _ColumnsHeaderText = New Dictionary(Of csMFA.enmProperty, String) 
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
 
  Private WithEvents _MFACol As csMFACol
  Private WithEvents _MFAColFullLength As csMFACol
 
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
  Public ReadOnly Property [SelectedMFA]() As csMFA 
    Get 
      If dgvMFA.SelectedRows.Count = 0 OrElse _Loading = True Then Return Nothing 
      Dim RowIndex As Integer = dgvMFA.SelectedRows(0).Cells(0).RowIndex 
      If RowIndex < 0 Then Return Nothing 
      If _Summarized = True AndAlso RowIndex = dgvMFA.Rows.Count - 1 Then dgvMFA.ClearSelection() : RaiseEvent evtUnChosen() : Return Nothing 
      Return _MFACol(RowIndex) 
    End Get 
  End Property 
  
  Public ReadOnly Property [MFACol]() As csMFACol 
    Get 
      Return _MFACol 
    End Get 
  End Property 
 
  Public Function LoadControl(ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    Dim pMFACol As New csMFACol(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    pFault = pMFACol.Fill(_Requester) 
    If pFault.isOK = False Then Return pFault 
 
    pFault = LoadControl(pMFACol)
    Return pFault 
  End Function 
 
  Public Function LoadControl(vMFACol As csMFACol, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    Return LoadControl(vMFACol) 
  End Function
  
  Private Function LoadControl(vMFACol As csMFACol) As clsFault
    Dim pFault As New clsFault
 
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
    'Use evtBeforeLoad to set or remove the list type, if you don't want the default 
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList  
    RaiseEvent evtBeforeLoad() 
 
    LoadLocalizedText() 
 
    'keep safe in case 
    Dim pColumnsHides As List(Of csMFA.enmProperty) = Nothing 
    If _LoadParameters.IsSumFillOnTheFly Then 
      pColumnsHides = New List(Of csMFA.enmProperty) 
      pColumnsHides.AddRange(_LoadParameters.ColumnsHide) 
    End If 
 
    'Force blg and clc fields to read-only 
    
    'Check for ReadOnly columns 
    For Each l In _LoadParameters.ColumnsReadOnly 
      If l = csMFA.enmProperty.ID Then colID.ReadOnly = True 
      If l = csMFA.enmProperty.CellOrEmail Then colCellOrEmail.ReadOnly = True 
      If l = csMFA.enmProperty.ProtectedFunction Then colProtectedFunction.ReadOnly = True 
      If l = csMFA.enmProperty.CodeHashed Then colCodeHashed.ReadOnly = True 
      If l = csMFA.enmProperty.AttemptNo Then colAttemptNo.ReadOnly = True 
      If l = csMFA.enmProperty.IsSuccessful Then colIsSuccessful.ReadOnly = True 
      If l = csMFA.enmProperty.LastAccessingIP Then colLastAccessingIP.ReadOnly = True 
      If l = csMFA.enmProperty.LastAccessingCountry Then colLastAccessingCountry.ReadOnly = True 
      If l = csMFA.enmProperty.UILang Then colUILang.ReadOnly = True 
      If l = csMFA.enmProperty.WhenCreated Then colWhenCreated.ReadOnly = True 
      If l = csMFA.enmProperty.WhenAccessed Then colWhenAccessed.ReadOnly = True 
      If l = csMFA.enmProperty.WhenExpires Then colWhenExpires.ReadOnly = True 
      If l = csMFA.enmProperty.Details Then colDetails.ReadOnly = True 
      If l = csMFA.enmProperty.User Then colUser.ReadOnly = True 
    Next 
 
    For Each l In _LoadParameters.ColumnsHide 
      'Parents only 
      Dim pParentProperty As csMFA.enmParentProperty = csMFA.enmParentProperty.UD 
      Dim pSuccess As Boolean = [Enum].TryParse(Of csMFA.enmParentProperty)(l.ToString(), ignoreCase:=False, pParentProperty) 
      If pSuccess = False Then Continue For 
      If Not _LoadParameters.CbosDoNotLoad.Contains(pParentProperty) Then 
        _LoadParameters.CbosDoNotLoad.Add(pParentProperty) 
      End If 
    Next 
 
    If _LoadParameters.IsSumFillOnTheFly Then 
      'Use what we just save instead 
      _LoadParameters.ColumnsHide = pColumnsHides 
    End If 
 
    dgvMFA.DoubleBuffered(True) 
 
    pFault = vMFACol.LoadLookupAndEnumText(_Requester) : If Not pFault.isOK Then Return pFault 
    
    'Now transfer to local collection 
    _MFAColFullLength = vMFACol.Clone() 
 
    'Truncate the strings 
    _MFACol = vMFACol 
    If _LoadParameters.TruncateStrings Then 
      _MFACol.TruncateStrings() 
    Else 
      dgvMFA.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
      dgvMFA.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders 
    End If 
 
    ' If you switch between ReadOnly and not Readonly, it causes problems
    Static sReadOnlyHandled As Boolean = False 
    If sReadOnlyHandled = False Then 
      If _LoadParameters.ReadOnly = True Then 
        colUser.Name = colUser.Name & "zzzz" 
        colUserText.Name = colUser.Name.Replace("zzzz", "") 
        If colUser.DataGridView IsNot Nothing Then dgvMFA.Columns.Remove(colUser) 
      Else 
        If colUser.ReadOnly = False Then 
          If colUserText.DataGridView IsNot Nothing Then dgvMFA.Columns.Remove(colUserText) 
        Else 
          colUser.Name = colUser.Name & "zzzz" 
          colUserText.Name = colUser.Name.Replace("zzzz", "") 
          If colUser.DataGridView IsNot Nothing Then dgvMFA.Columns.Remove(colUser) 
          If Not _LoadParameters.CbosDoNotLoad.Contains(csMFA.enmParentProperty.User) Then 
            _LoadParameters.CbosDoNotLoad.Add(csMFA.enmParentProperty.User) 
          End If 
        End If 
      End If 
      sReadOnlyHandled = True 
    End If 
    If _LoadParameters.ReadOnly = False Then 
      'Load ComboListCache 
      If Not _LoadParameters.CbosDoNotLoad.Contains(csMFA.enmParentProperty.User) Then 
        MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.Previous) 
      End If 
    End If 
 
    _SummaryOverFlow = "#" 
 
    Dim pHiddenColumnNames As New List(Of String) 
    For Each l In _LoadParameters.ColumnsHide 
      pHiddenColumnNames.Add("col" & l.ToString()) 
    Next 
    For Each lCol As DataGridViewColumn In dgvMFA.Columns 
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
    For Each p As csMFA.enmProperty In _LoadParameters.ColumnsHide 
      Dim pGridSetting As clsGridSetting = _GridSettings.FindByColumnName("col" & p.ToString()) 
      'HideColumn(p.ToString) 
      pGridSetting.ColumnRemoved = True 
    Next 
     
    'Set Header Text 
    For Each pD In _LoadParameters.ColumnsHeaderText 
      dgvMFA.Columns("col" & pD.Key.ToString).HeaderText = pD.Value 
    Next 
 
    'Format Columns 
    For Each pD In _LoadParameters.ColumnsFormat 
      dgvMFA.Columns("col" & pD.Key.ToString).DefaultCellStyle.Format = pD.Value 
    Next 
 
    'ordinal position 
    For Each pD In _LoadParameters.ColumnsOrdinalPosition 
      dgvMFA.Columns("col" & pD.Key.ToString).DisplayIndex = pD.Value 
    Next 
    _GridSettings.Update(Me, _Requester)
    
    'Align Columns 
    For Each pD In _LoadParameters.ColumnsAlignment 
      dgvMFA.Columns("col" & pD.Key.ToString).DefaultCellStyle.Alignment = pD.Value 
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
 
    dgvMFA.ClearSelection()
    bsCtlMFA.DataSource = Nothing 
    
    pFault = LoadSupportingCombos() : If pFault.isOK = False Then Return pFault 
 
    lblGrid.Text = _LoadParameters.GridTitle 
    If lblGrid.Text = "" Then 
      'Assume chkAutoRefresh is not used either. (may have to add it to LoadParameters) 
      pnlHeader.Visible = False 
    End If 
    Try
      LoadGrid()
    Catch ex As Exception
      Return pFault.LogException(ex, "LoadGrid", "TRGT-MFA-090124-2345", _Requester) 
    End Try
    
    RaiseEvent evtLoaded() 
    
    'Show row count in status label 
    lblStatus.ForeColor = Color.DarkGreen 
    lblStatus.Text = dgvMFA.RowCount & " rows" 
    
    'now do the default sorts 
    If _SortList IsNot Nothing Then 
      _AutoSorting = True 
      _PrevSortColumn = Nothing 
      For Each i In _SortList 
        Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(i, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0)) 
        dgvMFA_ColumnHeaderMouseClick(Me, pE) 
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
    If dgvMFA.SelectedRows.Count > 0 Then 
      pRowIndex = dgvMFA.SelectedRows(0).Cells(0).RowIndex 
    Else 
      If dgvMFA.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
        pRowIndex = dgvMFA.CurrentCellAddress.Y 
      End If 
    End If 
 
    If _LoadParameters.SummarizeGrid = True Then Summarize() 
 
    _Loading = True 
 
    bsCtlMFA.DataSource = Nothing 
    bsCtlMFA.DataSource = _MFACol
    
    dgvMFA.ClearSelection() 
    
    RaiseEvent evtUnChosen()
    
    SetUpBNButtons(False)
    'set columns 
    LoadColumns() 
 
    'Load buttons 
    For Each p As ToolStripMenuItem In btnColumns.DropDownItems 
      If p Is mnuColsReset OrElse p Is mnuColsHideMost Then Continue For 
      Dim pMenuItemProprty As String = p.Name.Substring(13) 
      p.Checked = dgvMFA.Columns("col" & pMenuItemProprty).Visible 
    Next 
 
    If pRowIndex >= 0 Then 
      If _Summarized = True Then 
        If pRowIndex <= _MFACol.Count - 2 Then 
          dgvMFA.Rows(pRowIndex).Selected = True 
        End If 
      Else 
        If pRowIndex <= _MFACol.Count - 1 Then 
          dgvMFA.Rows(pRowIndex).Selected = True 
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
    'EnumUILang
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csMFA.enmParentProperty.UILang, Nothing, Nothing, pEnumCol, pPrompt) 
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
    bsUILang.DataSource = pEnumCol 
    colUILang.Tag = pPrompt 

    'User
    If _LoadParameters.ReadOnly = False AndAlso _LoadParameters.CbosDoNotLoad.Find(Function(p) p = csMFA.enmParentProperty.User) = csMFA.enmParentProperty.UD Then 
      'enable using an external list if needed 
      pComboList = Nothing 
      pPrompt = "" 
      Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_UserDefaultByID 
      Dim pParentID As Long = 0 
      RaiseEvent evtOverrideLoadCbo(csMFA.enmParentProperty.User, pComboListTypeToLoad, pParentID, pComboList, pPrompt) 
      If pComboList Is Nothing Then 
        pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList) : If Not pFault.isOK() Then Return pFault 
        If MyCache.GetLevel(pComboListTypeToLoad) = Cache.enmLevel.AlwaysPageFromServer Then 
          Return pFault.LogFreeTextFault($"In {Me.Name}, {pComboListTypeToLoad.FastToString()} is defined as AlwaysPageFromServer. Either change it to AlwaysCache in evtBeforeLoad, make the column read-only, or make this grid read-only", "", "TRGT-MFACol-200806-1015", _Requester) 
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
      btnAdd.Visible = False 
      btnCeaseEdit.Visible = False 
    Else 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_MFAUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_MFAUpdate, _Requester) = True AndAlso _LoadParameters.ImportButtonHide = False Then btnImport.Visible = vInEdit Else btnImport.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_MFAUpdate, _Requester) = True Then btnAdd.Visible = vInEdit Else btnAdd.Visible = False 
      btnCeaseEdit.Visible = vInEdit 
      If _LoadParameters.AddEditDeleteButtonsHide = True Then 
        btnAdd.Visible = False 
      End If 
    End If 
    If vInEdit = True AndAlso _LoadParameters.AddEditDeleteButtonsHide = False Then 
      colID.ReadOnly = True 
      dgvMFA.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 
      dgvMFA.SelectionMode = DataGridViewSelectionMode.CellSelect 
      _DVGDirty = False 
    Else 
      dgvMFA.EditMode = DataGridViewEditMode.EditProgrammatically 
      dgvMFA.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
      dgvMFA.AllowUserToDeleteRows = False 
      dgvMFA.AllowUserToAddRows = False 
      'Don't automatically set the 1st one If dgvMFA.Rows.Count > 0 Then 
      '  Dim pCurrentRow As Integer 
      '  pCurrentRow = dgvMFA.CurrentRow.Index 
      '  dgvMFA.CurrentCell = dgvMFA.Rows(pCurrentRow).Cells(0) 
      '  dgvMFA.Rows(pCurrentRow).Selected = True 
      'End If 
    End If 
    If vInEdit = True Then 
      lblEditMode.Text = "Edit Mode" 
      tssReports.Visible = True 
      btnSpreadsheet.Enabled = False 
      btnReport.Enabled = False 
    Else 
      If _MFACol.Count = 0 Then 
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
    dgvMFA.Refresh() 
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
  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    DoAdd() 
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
    pFieldList.Append("CellOrEmail, ") 
    pFieldList.Append("ProtectedFunction, ") 
    pFieldList.Append("CodeHashed, ") 
    pFieldList.Append("AttemptNo, ") 
    pFieldList.Append("IsSuccessful, ") 
    pFieldList.Append("LastAccessingIP, ") 
    pFieldList.Append("LastAccessingCountry, ") 
    pFieldList.Append("UILang (DB Code), ") 
    pFieldList.Append("WhenCreated, ") 
    pFieldList.Append("WhenAccessed, ") 
    pFieldList.Append("WhenExpires, ") 
    pFieldList.Append("Details, ") 
    pFieldList.Append("UserID (DB Code), ") 
    
    Dim pNumberOfFields As Integer = 14 
    
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
    pMessage &= "If there is no ID field (the 1st field is CellOrEmail), then I will delete the table and recreate it with the data in this spreadsheet" 
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
    Dim pIncomingMFAs As New csMFACol(vWithParents:=clsEnums.enmLoadParent.DoNotLoad) 
 
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
          Dim pIncomingMFA As New csMFA(vWithParents:=clsEnums.enmLoadParent.DoNotLoad) 
          pIncomingMFA.Tag = "Row " & pRow.ToString 
          pCurrentRow = pReader.ReadFields() 
          If pRow = 0 Then 
            Continue While 'Header line  
          End If 
 
          If pCurrentRow.Length <> pNumberOfFields Then 
            pErrorFound = True 
            pIncomingMFA.Tag &= ": There should be " & pNumberOfFields & " fields, but there are actually " & pCurrentRow.Length & " fields." 
            pIncomingMFAs.Add(pIncomingMFA) 
            Continue While 
          End If 
 
          Dim pFieldNo As Integer = -1 
 
          If pNoPrimaryKey = False Then 
            Try 
              pFieldNo += 1 
              pFieldName = "ID" 
              pIncomingMFA.ID = CType(pCurrentRow(pFieldNo), Long) 
            Catch ex As Exception 
              pErrorFound = True 
              pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
            End Try 
          End If 
 
          Try 
            pFieldNo += 1 
            pFieldName = "CellOrEmail" 
            pIncomingMFA.CellOrEmail = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ProtectedFunction" 
            pIncomingMFA.ProtectedFunction = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "CodeHashed" 
            pIncomingMFA.CodeHashed = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "AttemptNo" 
            pIncomingMFA.AttemptNo = CType(pCurrentRow(pFieldNo), Integer) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "IsSuccessful" 
            pIncomingMFA.IsSuccessful = CType(pCurrentRow(pFieldNo), Boolean) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "LastAccessingIP" 
            pIncomingMFA.LastAccessingIP = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "LastAccessingCountry" 
            pIncomingMFA.LastAccessingCountry = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "UILang" 
            pIncomingMFA.UILang = clsEnums.TranslateEnmLanguage(pCurrentRow(pFieldNo)) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "WhenCreated" 
            pIncomingMFA.WhenCreated = CType(pCurrentRow(pFieldNo), DateTimeOffset) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "WhenAccessed" 
            pIncomingMFA.WhenAccessed = CType(pCurrentRow(pFieldNo), DateTimeOffset) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "WhenExpires" 
            pIncomingMFA.WhenExpires = CType(pCurrentRow(pFieldNo), DateTimeOffset) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "Details" 
            pIncomingMFA.Details = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "UserID" 
            pIncomingMFA.UserID = CType(pCurrentRow(pFieldNo), Long) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingMFA.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          If pIncomingMFA.Tag = "Row " & pRow.ToString Then 
            pIncomingMFA.Tag &= ": OK" 
          End If 
 
          pIncomingMFAs.Add(pIncomingMFA) 
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
          My.Computer.FileSystem.WriteAllText(pFilenameOut, pIncomingMFAs.ToCSV, False) 
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
    pFault = pIncomingMFAs.UpdateFromCollection(_Requester) 
    If pFault.isOK = False Then 
      ShowFault(pFault, _Requester) 
      Exit Sub 
    End If 
    'Reset the MFA collection 
    MyCache.ClearComboList(clsEnums.enmComboListType.c_MFADefaultByID) 
 
    Cursor = Cursors.Default 
 
    'Check that there were no problems 
    pErrorFound = False 
    For Each p In pIncomingMFAs 
      If p.Tag <> "OK" Then 
        pErrorFound = True 
      End If 
    Next 
    If pErrorFound = True Then 
      Try 
        My.Computer.FileSystem.WriteAllText(pFilenameOut, pIncomingMFAs.ToCSV, False) 
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
      _MFACol = pIncomingMFAs 
      LoadGrid() 
      frmMessageOrInputBox.ShowMsg("Update Successful! Please click on Refresh to see all the data", frmMessageOrInputBox.enmIconType.Information) 
    End If 
 
  End Sub 
 
  'ExternalButtons 
  Private Sub DoEdit() 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    Dim pCellRow As Integer = -1 
    Dim pCellCol As Integer = -1 
 
 
    If dgvMFA.Focused = True AndAlso dgvMFA.SelectedRows.Count > 0 Then 
      pCellRow = dgvMFA.CurrentCell.RowIndex 
      pCellCol = dgvMFA.CurrentCell.ColumnIndex 
    End If 
 
    Try 'in case it's empty 
      dgvMFA.CurrentCell = dgvMFA.Rows(0).Cells(0) 
      dgvMFA.CurrentCell.Selected = True 
    Catch ex As Exception 
    End Try 
 
 
    'remove summary row 
    If _LoadParameters.SummarizeGrid = True AndAlso _MFACol.Count > 0 AndAlso _MFACol(_MFACol.Count - 1).ID = 0 Then 
      _MFACol.RemoveAt(_MFACol.Count - 1) 
      bsCtlMFA.DataSource = Nothing 
      bsCtlMFA.DataSource = _MFACol 
      _Summarized = False 
    End If 
 
    SetUpBNButtons(True) 
    If pCellRow >= 0 AndAlso pCellCol >= 0 Then 
      dgvMFA.Focus() 
      dgvMFA.CurrentCell = dgvMFA.Rows(pCellRow).Cells(pCellCol) 
      dgvMFA.CurrentCell.Selected = True 
    ElseIf _MFACol.Count = 0 Then 
    Else 
      Try 'in case the cell is hidden.... 
        dgvMFA.CurrentCell = dgvMFA.Rows(0).Cells(0) 
        dgvMFA.CurrentCell.Selected = True 
      Catch ex As Exception 
      End Try 
    End If 
  End Sub 
  Private Sub DoAdd() 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True Then Exit Sub 
    bsCtlMFA.AddNew() 
 
    'Now choose any needed fields 
    Dim pEntity As csMFA 
    pEntity = CType(bsCtlMFA.Current, csMFA) 
 
    For Each l In _LoadParameters.SearchFilters 
      If l.Key.ToString().EndsWith("ID") Then 
        CallByName(pEntity, l.Key.ToString(), CallType.Set, l.Value) 
      ElseIf l.Key.ToString().EndsWith("Code") Then 
        CallByName(pEntity, l.Key.ToString(), CallType.Set, l.Value) 
      End If 
    Next 
 
  End Sub 
  Private Sub DoCeaseEdit() 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True And _DVGDirty = False Then 
      bsCtlMFA.DataSource = _MFACol 
    End If 
    If _DVGDirty = True Then 
      RaiseEvent evtTimerTripped() 
      Exit Sub 
    End If 
    Dim pMFA As csMFA = CType(bsCtlMFA.Current, csMFA) 
    If pMFA IsNot Nothing Then 
      If pMFA.ID = 0 Then 
        _IgnoreGridFault = True 
        bsCtlMFA.Remove(bsCtlMFA.Current) 
        _IgnoreGridFault = False 
      End If 
    End If 
    SetUpBNButtons(False) 
    If _MFACol.Count > 0 AndAlso dgvMFA.CurrentCell IsNot Nothing Then 
      For i As Integer = 0 To dgvMFA.Columns.Count - 1 
        If dgvMFA.Columns(i).Visible Then 
          dgvMFA.CurrentCell = dgvMFA.Rows(dgvMFA.CurrentCell.RowIndex).Cells(i) 
          Exit For 
        End If 
      Next 
      dgvMFA.Refresh() 
      dgvMFA.Rows(dgvMFA.CurrentCell.RowIndex).Selected = True 
    Else 
      dgvMFA.Refresh() 
    End If 
  End Sub 
  'Grid RowValidating 
  Private Sub dgvMFA_RowValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvMFA.RowValidating 
    If _Loading = True OrElse dgvMFA.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True Then 
      e.Cancel = True 
      dgvMFA.CurrentCell = dgvMFA.Rows(e.RowIndex).Cells(e.ColumnIndex) 
    End If 
  End Sub 
  'CellFormatting  
  Private Sub dgvMFA_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvMFA.CellFormatting 
    '_Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    If dgvMFA.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      If dgvMFA.Columns(e.ColumnIndex).ReadOnly = False Then 
        Exit Sub 
      End If 
    End If 
 
    RaiseEvent evtCellFormatting(sender, e) 
 
    ' Sample code evtCellFormatting - evtCellFormatting 
    ' You can use this to colour the fonts or your cell background or anything else that requires complete control of your cell 
    'Dim pMFA As csMFA = Nothing 
    'If dgvMFA.Columns(e.ColumnIndex).Name = colRecommendedQuantityToOrder.Name Then 
    '  If pMFA Is Nothing Then pMFA = CType(dgvMFA.Rows(e.RowIndex).DataBoundItem, csMFA) ' Only assign it if needed 
    '  If pMFA.CustomerOrders > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pMFA.CustomerOrders > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
    'If dgvMFA.Columns(e.ColumnIndex).Name = colRAV.Name Then 
    '  If pMFA Is Nothing Then pMFA = CType(dgvMFA.Rows(e.RowIndex).DataBoundItem, csMFA) ' Only assign it if needed
    '  If pMFA.RAV > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pMFA.RAV - pMFA.MaximumStock > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
 
    'Debug.Print("loc x,y:" & e.RowIndex & ", " & e.ColumnIndex & ": GetType" & dgvMFA.Columns(e.ColumnIndex).GetType.ToString & ": zValue" & e.Value.ToString) 
    If dgvMFA.Columns(e.ColumnIndex).GetType.ToString = "System.Windows.Forms.DataGridViewComboBoxColumn" Then 
      Dim pCol As System.Windows.Forms.DataGridViewComboBoxColumn = CType(dgvMFA.Columns(e.ColumnIndex), System.Windows.Forms.DataGridViewComboBoxColumn) 
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
          e.Value = "* BadCode '" & dgvMFA.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & "' *" 
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
 
    If dgvMFA.Columns(e.ColumnIndex).GetType.Name.Equals("DataGridViewImageColumn", StringComparison.OrdinalIgnoreCase) Then 
      If e.Value Is Nothing Then 
        e.Value = New Bitmap(1, 1) 
      End If 
    End If 
 
    If _Summarized = True Then 
      If e.RowIndex = dgvMFA.Rows.Count - 1 Then 
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
            If _SummaryOverFlow.IndexOf(dgvMFA.Columns(e.ColumnIndex).Name.Substring(3)) >= 0 Then 
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
  Private Sub dgvMFA_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMFA.CellValueChanged 
    If e.RowIndex < 0 Then Exit Sub 
 
  End Sub 
 
  'Grid Sort
  Private Sub dgvMFA_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMFA.ColumnHeaderMouseClick
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewColumn As DataGridViewColumn = dgvMFA.Columns(e.ColumnIndex)
    If bsCtlMFA.Current Is Nothing Then Exit Sub

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
    dgvMFA.SuspendLayout()

    Dim pMFA As csMFA
    Dim pID As Long = 0 
    If dgvMFA.SelectedRows.Count > 0 Then 
    pMFA = CType(bsCtlMFA.Current, csMFA)
      pID = pMFA.ID 
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
    Dim pMFACol As csMFACol
    pMFACol = CType(bsCtlMFA.DataSource, csMFACol)

    Dim pSummaryRow As csMFA = Nothing 
    If _Summarized = True Then 
      pSummaryRow = pMFACol(pMFACol.Count - 1) 
      pMFACol.RemoveAt(pMFACol.Count - 1) 
    End If 
 
    If pNewSortOrder = SortOrder.Ascending Then
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
        'save the present sort 
        For iCntr As Integer = 0 To pMFACol.Count - 1 
          pMFACol(iCntr).Tag = iCntr.ToString("0000000000") 
        Next 
      End If 
      If pNewColumn Is colID Then
        pMFACol.SortByID()
      ElseIf pNewColumn Is colCellOrEmail Then
        pMFACol.SortByCellOrEmail()
      ElseIf pNewColumn Is colProtectedFunction Then
        pMFACol.SortByProtectedFunction()
      ElseIf pNewColumn Is colAttemptNo Then
        pMFACol.SortByAttemptNo()
      ElseIf pNewColumn Is colIsSuccessful Then
        pMFACol.SortByIsSuccessful()
      ElseIf pNewColumn Is colLastAccessingIP Then
        pMFACol.SortByLastAccessingIP()
      ElseIf pNewColumn Is colLastAccessingCountry Then
        pMFACol.SortByLastAccessingCountry()
      ElseIf pNewColumn Is colUILang Then
        pMFACol.SortByUILang()
      ElseIf pNewColumn Is colWhenCreated Then
        pMFACol.SortByWhenCreated()
      ElseIf pNewColumn Is colWhenAccessed Then
        pMFACol.SortByWhenAccessed()
      ElseIf pNewColumn Is colWhenExpires Then
        pMFACol.SortByWhenExpires()
      ElseIf pNewColumn Is colDetails Then
        pMFACol.SortByDetails()
      ElseIf pNewColumn Is colUser OrElse pNewColumn Is colUserText Then
        pMFACol.SortByUserText()
      End If
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
      Dim iCntr As Integer = 0 
        If pNewColumn Is colID Then
          Dim pTest As Long = 0 
          For Each p As csMFA In pMFACol 
            If p.ID <> pTest Then iCntr += 1 : pTest = p.ID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colCellOrEmail Then
          Dim pTest As String = "" 
          For Each p As csMFA In pMFACol 
            If p.CellOrEmail <> pTest Then iCntr += 1 : pTest = p.CellOrEmail 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colProtectedFunction Then
          Dim pTest As String = "" 
          For Each p As csMFA In pMFACol 
            If p.ProtectedFunction <> pTest Then iCntr += 1 : pTest = p.ProtectedFunction 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAttemptNo Then
          Dim pTest As Integer = 0 
          For Each p As csMFA In pMFACol 
            If p.AttemptNo <> pTest Then iCntr += 1 : pTest = p.AttemptNo 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colIsSuccessful Then
          Dim pTest As Boolean = False 
          For Each p As csMFA In pMFACol 
            If p.IsSuccessful <> pTest Then iCntr += 1 : pTest = p.IsSuccessful 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLastAccessingIP Then
          Dim pTest As String = "" 
          For Each p As csMFA In pMFACol 
            If p.LastAccessingIP <> pTest Then iCntr += 1 : pTest = p.LastAccessingIP 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLastAccessingCountry Then
          Dim pTest As String = "" 
          For Each p As csMFA In pMFACol 
            If p.LastAccessingCountry <> pTest Then iCntr += 1 : pTest = p.LastAccessingCountry 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUILang Then
          Dim pTest As clsEnums.enmLanguage = clsEnums.enmLanguage.UD 
          For Each p As csMFA In pMFACol 
            If p.UILang <> pTest Then iCntr += 1 : pTest = p.UILang 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colWhenCreated Then
          Dim pTest As DateTimeOffset = DateTimeOffset.MinValue 
          For Each p As csMFA In pMFACol 
            If p.WhenCreated <> pTest Then iCntr += 1 : pTest = p.WhenCreated 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colWhenAccessed Then
          Dim pTest As DateTimeOffset = DateTimeOffset.MinValue 
          For Each p As csMFA In pMFACol 
            If p.WhenAccessed <> pTest Then iCntr += 1 : pTest = p.WhenAccessed 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colWhenExpires Then
          Dim pTest As DateTimeOffset = DateTimeOffset.MinValue 
          For Each p As csMFA In pMFACol 
            If p.WhenExpires <> pTest Then iCntr += 1 : pTest = p.WhenExpires 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colDetails Then
          Dim pTest As String = "" 
          For Each p As csMFA In pMFACol 
            If p.Details <> pTest Then iCntr += 1 : pTest = p.Details 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUser OrElse pNewColumn Is colUserText Then
          Dim pTest As String = "" 
          For Each p As csMFA In pMFACol 
            If p.UserText <> pTest Then iCntr += 1 : pTest = p.UserText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        End If 
        pMFACol.SortByTag() 
      End If 
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
      _PrevSortColumn = pNewColumn
      pPrevSortOrder = SortOrder.Ascending
    Else
      pMFACol.Reverse()
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
      pPrevSortOrder = SortOrder.Descending
    End If

    If _Summarized = True Then 
      pMFACol.Add(pSummaryRow) 
    End If 
 
    If pID > 0 Then
      bsCtlMFA.Position = bsCtlMFA.IndexOf(pMFACol.FindByID(pID))
    End If

    'dgvMFA.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
    dgvMFA.ResumeLayout()

    Cursor = Cursors.Default
    dgvMFA.Refresh()

  End Sub
  'Select Row 
  Public Sub SelectRowByObjectID(ByVal pID As Long) 
    If pID > 0 Then 
      Dim pMFACol As csMFACol 
      pMFACol = CType(bsCtlMFA.DataSource, csMFACol) 
      Dim pMFA As csMFA = pMFACol.FindByID(pID) 
      If Not pMFA.IsEmpty Then 
        bsCtlMFA.Position = bsCtlMFA.IndexOf(pMFACol.FindByID(pID)) 
        dgvMFA.Rows(bsCtlMFA.Position).Selected = True 
      Else 
        dgvMFA.ClearSelection() 
      End If 
    ElseIf pID = 0 Then 
      dgvMFA.ClearSelection() 
    End If 
  End Sub 
  
  'Grid Resize
  Private Sub dgvMFA_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMFA.ColumnHeaderMouseDoubleClick
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    dgvMFA.AutoResizeColumn(e.ColumnIndex)
    Cursor = Cursors.Default
  End Sub
  'Other Grid Events
  Private Sub dgvMFA_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMFA.CurrentCellDirtyStateChanged
   _DVGDirty = True 
  End Sub
  Private Sub dgvMFA_Scroll(sender As Object, e As ScrollEventArgs) Handles dgvMFA.Scroll
    dgvMFA.Invalidate() 
  End Sub
 
  Private Sub dgvMFA_DataFault(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvMFA.DataError
    'Dim pFault As New clsFault
    '
    'If e.RowIndex = dgvMFA.Rows.Count - 1 Then Exit Sub
 
    'If dgvMFA.Columns(e.ColumnIndex).Name.StartsWith("colIDin", StringComparison.OrdinalIgnoreCase) Then Exit Sub 
 
    'If _IgnoreGridFault = True Then Exit Sub
    '_DVGDirty = False 
    'Static pShown As Boolean 
    '
    'Dim pSubStrg As New System.Text.StringBuilder 
    ''Other Error 
    'Try 
    '  Try 
    '    pSubStrg.AppendLine("In table 'MFA', the row with an ID of " & dgvMFA.Rows(e.RowIndex).Cells(0).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine("In grid 'MFA', row index " & e.RowIndex) 
    '  End Try 
    '  Try 
    '    pSubStrg.AppendLine(" has an invalid value of " & dgvMFA.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine(" has an invalid value of Nothing.") 
    '  End Try 
    '  pSubStrg.AppendLine(" in column " & dgvMFA.Columns(e.ColumnIndex).DataPropertyName) 
    'Catch ex As Exception 
    '  pSubStrg.AppendLine("; Failed trying to fill DataFault as well!") 
    'End Try 
    'pFault.LogException(209, e.Exception, pSubStrg.ToString, "TRGT-MFA-100409-2248", _Requester) 
    'If pShown = False Then 
    '  Dim pCell As DataGridViewCell 
    '  Try 
    '    pCell = dgvMFA(e.ColumnIndex, e.RowIndex)
    '  Catch ex As Exception 
    '    pCell = dgvMFA(0, 0)
    '  End Try 
    '  ShowFault(pFault, _Requester) 
    '  pShown = True 
    'End If 
  End Sub
  Private Sub dgvMFA_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvMFA.KeyDown 
    If e.KeyCode = Keys.Escape Then 
      'DoCeaseEdit() 
      Dim pMFA As csMFA = CType(bsCtlMFA.Current, csMFA) 
      If pMFA IsNot Nothing Then 
        If pMFA.ID = 0 Then 
          _IgnoreGridFault = True 
          bsCtlMFA.Remove(bsCtlMFA.Current) 
          _IgnoreGridFault = False 
        End If 
      End If 
      SetUpBNButtons(False) 
      'dgvMFA.CurrentCell = dgvMFA.Rows(dgvMFA.CurrentCell.RowIndex).Cells(0) 
      dgvMFA.Refresh() 
    End If 
  End Sub 
  Private Sub dgvMFA_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvMFA.ColumnWidthChanged
    If Me.DesignMode = True Then Exit Sub 
    If _Loading = False Then SaveSizes()
  End Sub
  Private Sub dgvMFA_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvMFA.ColumnDisplayIndexChanged
    Cursor = Cursors.WaitCursor
    If _Loading = False Then SaveSizes()
    Cursor = Cursors.Default
  End Sub
  Private Sub dgvMFA_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMFA.CellDoubleClick 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso e.RowIndex = dgvMFA.Rows.Count - 1 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
    Dim pCell As DataGridViewCell = dgvMFA(e.ColumnIndex, e.RowIndex) 
 
    Dim pHandled As Boolean = False 
    Dim pMFA As csMFA = _MFACol(e.RowIndex)
    RaiseEvent evtRowDoubleClicked(pMFA, pHandled) 
    Cursor = Cursors.Default 
 
    If pHandled = False Then 
      If Me.ParentForm.Name.Equals("frmPopup", StringComparison.OrdinalIgnoreCase) Then Return 
      frmPopup.Text = "MFA Detail" 
      Dim pFault As clsFault = frmPopup.LoadControl("ctlc_MFA", pMFA, _Requester) 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
      frmPopup.ShowDialog() 
    End If 
 
  End Sub 
  Private Sub dgvMFA_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMFA.SelectionChanged 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If _Loading = True Then Exit Sub 
    If DateTime.Now < _IgnoreSelectionUntil Then Exit Sub
    If _ProcessingSelection Then Exit Sub
    If dgvMFA.SelectedRows.Count = 0 Then 
      RaiseEvent evtUnChosen() 
      Exit Sub 
    End If 
    Dim RowIndex As Integer = dgvMFA.SelectedRows(0).Cells(0).RowIndex 
    If RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso RowIndex = dgvMFA.Rows.Count - 1 Then dgvMFA.ClearSelection() : RaiseEvent evtUnChosen() : Exit Sub 
    Dim pMFA As csMFA = _MFACol(RowIndex)
    _ProcessingSelection = True
    Try
      RaiseEvent evtRowClicked(pMFA) 
    Finally
      _IgnoreSelectionUntil = DateTime.Now.AddMilliseconds(500)
      _ProcessingSelection = False
    End Try
  End Sub 
  Private Sub dgvMFA_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMFA.RowLeave 
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
 
    Dim pOriginalCol As Integer = dgvMFA.CurrentCell.ColumnIndex 
     
    'If user clicked on CeaseEdit without changing cells, the data will not be received 
    ' therefore we have to fake exiting the cell 
    Dim pNewCol As Integer 
    'We can only go to a visible cell! 
    If pOriginalCol > 0 Then 
      pNewCol = pOriginalCol - 1 
      Do Until dgvMFA.Columns(pNewCol).Visible = True OrElse pNewCol = 0 
        pNewCol = pNewCol - 1 
      Loop 
    Else 
      pNewCol = 1 
    End If 
    If dgvMFA.Columns(pNewCol).Visible = False Then 
      dgvMFA.Columns(pNewCol).Visible = True 
      dgvMFA.CurrentCell = dgvMFA.CurrentRow.Cells(pNewCol) 
      dgvMFA.CurrentCell = dgvMFA.CurrentRow.Cells(pOriginalCol) 
      dgvMFA.Columns(pNewCol).Visible = False 
    Else 
      dgvMFA.CurrentCell = dgvMFA.CurrentRow.Cells(pNewCol) 
      dgvMFA.CurrentCell = dgvMFA.CurrentRow.Cells(pOriginalCol) 
    End If 
    dgvMFA.Rows(dgvMFA.CurrentCell.RowIndex).Selected = True 
    Dim pMFA As csMFA 
    pMFA = CType(bsCtlMFA.Current, csMFA) 
 
    'Add required data (primary keys) from parent objects  
    RaiseEvent evtBeforeUpdate(CType(pMFA, csMFA), pCancel) 
    If pCancel = True Then 
      _DVGDirty = False 
      RaiseEvent evtTimerTripped() 
      Return True 
    End If 
    pFault = pMFA.Update(_Requester) 
    If pFault.isOK = False AndAlso pFault.Severity <> clsEnums.enmFaultSeverity.LogOnly Then 
      ShowFault(pFault, _Requester) 
      frmMessageOrInputBox.ShowMsg("Fix the problem, or click on 'Esc' to remove the row.", frmMessageOrInputBox.enmIconType.Information, frmMessageOrInputBox.enmButtons.Yes) 
      Return True 
    Else 
      If pFault.isOK = False Then 'AndAlso pFault.Severity = clsEnums.enmFaultSeverity.LogOnly  
        ShowFault(pFault, _Requester) 
      End If 
      dgvMFA.EndEdit() 
      _DVGDirty = False 
      'Reset the MFA collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.c_MFADefaultByID) 
      RaiseEvent evtUpdated(pMFA) 
      Return False 
    End If 
  End Function 
  Private Sub SaveSizes() 
    ' Save column state data  
    ' including order, column width and whether or not the column is visible  
    For Each pCol As DataGridViewColumn In dgvMFA.Columns 
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
    If _MFACol.Count <= 1 Then 
      _Summarized = False 
      Exit Sub 
    End If 
 
    Dim pAttemptNo As Integer 
    For Each pExistingRow As csMFA In _MFACol 
      If _SummaryOverFlow.IndexOf("#AttemptNo#") < 0 Then 
        Try 
          pAttemptNo += pExistingRow.AttemptNo 
        Catch ex As System.OverflowException 
          pAttemptNo = -99999999 
          _SummaryOverFlow &= "AttemptNo#" 
        End Try 
      End If 
    Next 
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csMFA.enmSummarizeableProperty.AttemptNo) = csMFA.enmSummarizeableProperty.AttemptNo Then pAttemptNo = 0
    Dim pSummaryRow As New csMFA( _ 
        vID:=0 _ 
      , vCellOrEmail:="" _ 
      , vProtectedFunction:="" _ 
      , vCodeHashed:="" _ 
      , vAttemptNo:=pAttemptNo _ 
      , vIsSuccessful:=False _ 
      , vLastAccessingIP:="" _ 
      , vLastAccessingCountry:="" _ 
      , vUILang:=clsEnums.enmLanguage.UD _ 
      , vUILangText:="" _ 
      , vWhenCreated:=Nothing _ 
      , vWhenAccessed:=Nothing _ 
      , vWhenExpires:=Nothing _ 
      , vDetails:="" _ 
      , vUserID:=0 _ 
      , vUserText:="" _ 
      , vTag:="" _ 
      , vDateAdded:=Nothing _ 
      , vWithParents:=clsEnums.enmLoadParent.TextOnly _ 
      )
    _MFACol.Add(pSummaryRow) 
    _Summarized = True 
  End Sub 
  
  'Reports and Excel 
  Friend Function CreateSpreadSheet() As clsFault  
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name  
    Dim pFault As New clsFault  
    'Dim pExcel As New Tools.ExcelSheet  
    Dim pDateToShow As String = DateTime.Now.ToString("yyMMdd_HHmmss")  
    Dim pRoot As String = $"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles" 
 
    Dim pFileNameAllFields As String = $"{pRoot}\MFACol_{pDateToShow}AllFields.csv" 
    Dim pFileNameFieldsOnGrid As String = $"{pRoot}\MFACol_{pDateToShow}FieldsOnGrid.csv" 
    Dim pFileNameAllFieldsWithIDs As String = $"{pRoot}\MFACol_{pDateToShow}AllFieldsWithIDs.csv" 
    Dim pFileNameAllFieldsXML As String = $"{pRoot}\MFACol_{pDateToShow}AllFields.xml" 
    Dim pFileNameAllFieldsJson As String = $"{pRoot}\MFACol_{pDateToShow}AllFields.json" 
 
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
    For Each pCol As DataGridViewColumn In dgvMFA.Columns 
      If pCol.Visible = True Then 
        pTmpStrg.Append(",""" & pCol.HeaderText & """") 
      End If 
    Next 
    pCSV.AppendLine(pTmpStrg.ToString.Substring(1)) 
 
    'Now the data  
    Dim i As Integer 
    Dim pStart As Date = Now 
 
    Dim pTruncatedFieldNames As String = "" 
    For Each Row As DataGridViewRow In dgvMFA.Rows 
      i += 1 
      If _LoadParameters.SummarizeGrid = True Then 
        If Row.Index = dgvMFA.Rows.Count - 1 Then Exit For 
      End If 
      If i Mod 500 = 0 Then 
        lblStatus.Text = " Writing Row " & i & ". Time Elapsed: " & DateTime.Now.Subtract(pStart).TotalSeconds().ToString("###0") & " sec" : Application.DoEvents() 
      End If 
      pTmpStrg = New System.Text.StringBuilder 
       
      For Each pCol As DataGridViewColumn In dgvMFA.Columns 
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
        pFault = _MFAColFullLength.CreateXML(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsXML, pStrg, False) 
        'json 
        pFault = _MFAColFullLength.CreateJSON(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsJson, pStrg, False) 
        'default  
        My.Computer.FileSystem.WriteAllText(pFileNameAllFields, _MFAColFullLength.ToCSV, False)  
        'WithIDs  
        'pFault = _MFAColFullLength.LoadLookupAndEnumText(_Requester) : If pFault.isOK = False Then Return pFault (already done) 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsWithIDs, _MFAColFullLength.ToCSV(True), False) 
      End If  
      'default  
      My.Computer.FileSystem.WriteAllText(pFileNameFieldsOnGrid, pCSV.ToString, False)  
      pFault.SetOK()  
    Catch ex As Exception  
      pFault.LogException(ex, "", "TRGT-MFA-090210-1618", _Requester)  
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
      pFault.LogException(ex, "", "TRGT-MFA-090210-1618", _Requester)  
    End Try  
  
    If pFault.isOK = False Then Return pFault  
    
    Return pFault  
  End Function  
 
  Private Sub ReportDesign() 
 
    _Report = New vbReport.ReportDocument 
    _Report.AutoDiscover = False 
    Try 
      For Each pCol As DataGridViewColumn In dgvMFA.Columns 
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
        .SubTitleLeft = "MFAs" 
        .SubTitleRight = "Rows: " & _MFACol.Count.ToString 
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
        .DataSource = _MFACol 
        .HasSummaryLine = _Summarized 
      End With 
    Catch ex As Exception 
      Dim pFault As New clsFault 
      pFault.LogException(ex, "", "TRGT-MFA-090210-2119", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
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
      pFault.LogException(ex, "", "TRGT-MFA-090211-0746", _Requester) 
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
      For Each pRow As DataGridViewRow In dgvMFA.Rows 
        Try : pRow.Visible = True : Catch : End Try 
      Next 
      lblStatus.ForeColor = Color.DarkGreen 
      lblStatus.Text = dgvMFA.RowCount & " rows" 
      Exit Sub 
    End If 
    ' Hide rows that don't match search text 
    For Each row As DataGridViewRow In dgvMFA.Rows 
      Dim pVisible As Boolean = False 
      For Each cell As DataGridViewCell In row.Cells 
        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(pSearchText) Then 
          pVisible = True : Exit For 
        End If 
      Next 
      Try 
        Dim pBS As CurrencyManager = CType(Me.BindingContext(bsCtlMFA), CurrencyManager) 
        row.Visible = pVisible 
      Catch : End Try 
    Next 
    Dim pVisibleCount As Integer = 0 
    For Each row As DataGridViewRow In dgvMFA.Rows 
      If row.Visible Then pVisibleCount += 1 
    Next 
    lblStatus.ForeColor = Color.DarkBlue 
    lblStatus.Text = pVisibleCount & " of " & dgvMFA.RowCount & " rows" 
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
      pFault.LogException(ex, "GetOrInitializeGridSettings", "TRGT-MFA-120225-1310", _Requester) 
    End Try 
    If pFault.isOK = False Then Return pFault 
 
    Dim pSaveInitial As Boolean = False 
    
    '_GridSettings.Clear() Use for testing 
    If _GridSettings.Count = 0 Then 
      For Each pCol As DataGridViewColumn In dgvMFA.Columns 
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
      For Each pCol As DataGridViewColumn In dgvMFA.Columns 
        Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
        If pG.ColumnName = "" Then 
          pG.ColumnDisplayIndex = pCol.DisplayIndex 
          pG.ColumnWidth = ccHelper.ToInteger((dgvMFA.Width - 30) / dgvMFA.Columns.Count) 
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
    'colDetails.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    
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
           
          With dgvMFA.Columns(lGridSetting.ColumnName) 
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
      pFault.LogException(204, ex, "", "TRGT-MFA-090120-1502", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
    End Try 
  End Sub 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "ID", _Requester) 
    If pStrg <> "" Then colID.HeaderText = pStrg : mnuColVisibleID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "CellOrEmail", _Requester) 
    If pStrg <> "" Then colCellOrEmail.HeaderText = pStrg : mnuColVisibleCellOrEmail.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "ProtectedFunction", _Requester) 
    If pStrg <> "" Then colProtectedFunction.HeaderText = pStrg : mnuColVisibleProtectedFunction.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "CodeHashed", _Requester) 
    If pStrg <> "" Then colCodeHashed.HeaderText = pStrg : mnuColVisibleCodeHashed.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "AttemptNo", _Requester) 
    If pStrg <> "" Then colAttemptNo.HeaderText = pStrg : mnuColVisibleAttemptNo.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "IsSuccessful", _Requester) 
    If pStrg <> "" Then colIsSuccessful.HeaderText = pStrg : mnuColVisibleIsSuccessful.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "LastAccessingIP", _Requester) 
    If pStrg <> "" Then colLastAccessingIP.HeaderText = pStrg : mnuColVisibleLastAccessingIP.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "LastAccessingCountry", _Requester) 
    If pStrg <> "" Then colLastAccessingCountry.HeaderText = pStrg : mnuColVisibleLastAccessingCountry.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "UILang", _Requester) 
    If pStrg <> "" Then colUILang.HeaderText = pStrg : mnuColVisibleUILang.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "WhenCreated", _Requester) 
    If pStrg <> "" Then colWhenCreated.HeaderText = pStrg : mnuColVisibleWhenCreated.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "WhenAccessed", _Requester) 
    If pStrg <> "" Then colWhenAccessed.HeaderText = pStrg : mnuColVisibleWhenAccessed.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "WhenExpires", _Requester) 
    If pStrg <> "" Then colWhenExpires.HeaderText = pStrg : mnuColVisibleWhenExpires.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "Details", _Requester) 
    If pStrg <> "" Then colDetails.HeaderText = pStrg : mnuColVisibleDetails.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_MFA", "User", _Requester) 
    If pStrg <> "" Then colUser.HeaderText = pStrg : mnuColVisibleUser.Text = pStrg
 
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
 
  Private Sub mnuColVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColVisibleID.Click, mnuColVisibleCellOrEmail.Click, mnuColVisibleProtectedFunction.Click, mnuColVisibleCodeHashed.Click, mnuColVisibleAttemptNo.Click, mnuColVisibleIsSuccessful.Click, mnuColVisibleLastAccessingIP.Click, mnuColVisibleLastAccessingCountry.Click, mnuColVisibleUILang.Click, mnuColVisibleWhenCreated.Click, mnuColVisibleWhenAccessed.Click, mnuColVisibleWhenExpires.Click, mnuColVisibleDetails.Click, mnuColVisibleUser.Click
    Cursor = Cursors.WaitCursor 
    Dim pToolStripItem As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem) 
    dgvMFA.Columns("col" & pToolStripItem.Name.Substring(13)).Visible = pToolStripItem.Checked 
    If _Loading = False Then SaveSizes() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsReset.Click 
    Cursor = Cursors.WaitCursor 
    dgvMFA.SuspendLayout() 
 
    For Each pCol As DataGridViewColumn In dgvMFA.Columns 
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
    pNewWidth = ccHelper.ToInteger((dgvMFA.Width - 30) / pVisibleColumns) 
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
     
    dgvMFA.ResumeLayout() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsHideMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsHideMost.Click 
 
    _Loading = True 
    'Hide All 
    If mnuColVisibleID.Checked = True Then mnuColVisibleID.PerformClick() 
    If mnuColVisibleCellOrEmail.Checked = True Then mnuColVisibleCellOrEmail.PerformClick() 
    If mnuColVisibleProtectedFunction.Checked = True Then mnuColVisibleProtectedFunction.PerformClick() 
    If mnuColVisibleCodeHashed.Checked = True Then mnuColVisibleCodeHashed.PerformClick() 
    If mnuColVisibleAttemptNo.Checked = True Then mnuColVisibleAttemptNo.PerformClick() 
    If mnuColVisibleIsSuccessful.Checked = True Then mnuColVisibleIsSuccessful.PerformClick() 
    If mnuColVisibleLastAccessingIP.Checked = True Then mnuColVisibleLastAccessingIP.PerformClick() 
    If mnuColVisibleLastAccessingCountry.Checked = True Then mnuColVisibleLastAccessingCountry.PerformClick() 
    If mnuColVisibleUILang.Checked = True Then mnuColVisibleUILang.PerformClick() 
    If mnuColVisibleWhenCreated.Checked = True Then mnuColVisibleWhenCreated.PerformClick() 
    If mnuColVisibleWhenAccessed.Checked = True Then mnuColVisibleWhenAccessed.PerformClick() 
    If mnuColVisibleWhenExpires.Checked = True Then mnuColVisibleWhenExpires.PerformClick() 
    If mnuColVisibleDetails.Checked = True Then mnuColVisibleDetails.PerformClick() 
    If mnuColVisibleUser.Checked = True Then mnuColVisibleUser.PerformClick() 
    'Show Defaults 
    If mnuColVisibleCellOrEmail.Checked = False Then mnuColVisibleCellOrEmail.PerformClick() 
    
    _Loading = False 
    'dgvMFA.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
  End Sub 
  
  Private Sub dgvMFA_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvMFA.CellMouseClick 
    If e.Button = MouseButtons.Right Then 
      Dim pMessageBox As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the MFA to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pMessageBox <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pMFA As csMFA = _MFACol(e.RowIndex) 
        If pMessageBox = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pMFA.ToCSV) 
        Else 
          Clipboard.SetText(pMFA.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The MFA is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvMFA_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvMFA.MouseDown 
    '--- Save anchor on normal click (no modifiers) ---
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And (Keys.Shift Or Keys.Control)) = 0 AndAlso dgvMFA.CurrentRow IsNot Nothing Then 
      _SelectionAnchor = dgvMFA.CurrentRow.Index 
    End If 
    'This removes on click from the update 
    If dgvMFA.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
 
    Dim pCell As DataGridView.HitTestInfo = dgvMFA.HitTest(e.X, e.Y) 
 
    If pCell.Type = DataGridViewHitTestType.Cell Then 
      'Enable edit force it to be current 
      Dim pCurrentCell As DataGridViewCell = Nothing 
      Try 
        pCurrentCell = dgvMFA(pCell.ColumnIndex, pCell.RowIndex) 
        If pCurrentCell.ReadOnly Then Exit Sub 
        dgvMFA.CurrentCell = pCurrentCell 
      Catch ex As Exception 
        Exit Sub  
      End Try 
      'make the combobox drop down if it's active 
      If pCurrentCell.GetType().Name.Equals("DataGridViewComboBoxCell", StringComparison.OrdinalIgnoreCase) Then 
        dgvMFA.BeginEdit(True) 
        DirectCast(dgvMFA.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvMFA_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvMFA.MouseUp 
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And Keys.Shift) = Keys.Shift AndAlso _SelectionAnchor >= 0 Then 
      Dim hit = dgvMFA.HitTest(e.X, e.Y) 
      If hit.RowIndex >= 0 Then 
        dgvMFA.MultiSelect = True 
        dgvMFA.ClearSelection() 
        Dim pFrom As Integer = Math.Min(_SelectionAnchor, hit.RowIndex) 
        Dim pTo As Integer = Math.Max(_SelectionAnchor, hit.RowIndex) 
        For i As Integer = pFrom To pTo 
          dgvMFA.Rows(i).Selected = True 
        Next 
      End If 
    ElseIf (pModifiers And Keys.Control) = Keys.Control Then 
      dgvMFA.MultiSelect = True 
    Else 
      dgvMFA.MultiSelect = False 
    End If 
  End Sub 
 
  Private Sub chkAutoRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoRefresh.CheckedChanged 
    If chkAutoRefresh.Checked Then 
      _PrevSortColumn = Nothing 
 
      Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(colID.Index, -1, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton2, 0, 0, 0, 0)) 
      dgvMFA_ColumnHeaderMouseClick(Me, pE) 
      Application.DoEvents() 
      dgvMFA_ColumnHeaderMouseClick(Me, pE) 
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
 
  Private Sub ctlc_MFACol_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
    'Set the font for the BN 
        If MyFont Is Nothing Then Return 
    BN.Font = New Font(MyFont.Name, MyFont.Size) 
    dgvMFA.RowTemplate.Height = ccHelper.ToInteger(23 * MyFont.Size / 9) 
  End Sub 
 
  Private Sub ctlc_MFACol_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    If Me.ParentForm Is Nothing Then Exit Sub 
    Dim pParent As String = Me.ParentForm.Name 
    Dim pResponse As Boolean = Me.Visible 
    Dim pSize As Integer = dgvMFA.Width 
 
    'now set sizes if needed 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_MFACol_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True AndAlso Not Me.Parent.Name.StartsWith("pnl", StringComparison.OrdinalIgnoreCase) Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_MFACol_Leave(sender As Object, e As EventArgs) Handles Me.Leave 
    If _Requester Is Nothing Then Return 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    DoCeaseEdit() 
  End Sub 
  Private Sub cc_ctlc_MFACol_evtBeforeLoad() Handles Me.evtBeforeLoad 
    _LoadParameters.ReadOnly = True 
    _LoadParameters.ColumnsHide.Add(csMFA.enmProperty.ID) 
    _LoadParameters.ColumnsHide.Add(csMFA.enmProperty.CodeHashed) 
  End Sub 
 

  'Track open detail windows to prevent duplicates 
  Private Shared _openDetailWindows As New Dictionary(Of String, Form)() 
 
  'Context menu - right-click: add to selection if not already selected, otherwise keep multi-selection 
  Private Sub dgvMFA_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvMFA.CellMouseDown 
    ReleaseStuckModifierKeys() 'Fix sticky SHIFT/CTRL before selection changes 
    If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then 
      If Not dgvMFA.Rows(e.RowIndex).Selected Then 
        dgvMFA.ClearSelection() 
        dgvMFA.Rows(e.RowIndex).Selected = True 
      End If 
    End If 
  End Sub 
 
  'Context menu - Opening: adjust items based on single/multi selection 
  Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening 
    Dim pCount As Integer = dgvMFA.SelectedRows.Count 
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
    If dgvMFA.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvMFA.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _MFACol.Count Then Exit Sub 
    Dim pMFA As csMFA = _MFACol(pRowIndex) 
    Dim pTitle As String = "MFA #" & pMFA.ID.ToString() 
    Dim pKey As String = "MFA_" & pMFA.ID.ToString() 
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
    Dim pCtlName As String = "ctlc_MFA" 
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
    Dim pFault As clsFault = CType(pLoad.Invoke(pControl, New Object() {pMFA, _Requester}), clsFault) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    'Return to Tab click handler - sends entity to a new tab in frmMain 
    Dim pEntityRef As Object = pMFA 
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
    If dgvMFA.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvMFA.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _MFACol.Count Then Exit Sub 
    Dim pMFA As csMFA = _MFACol(pRowIndex) 
    Dim pFrmMain As frmMain = Nothing 
    For Each pForm As Form In Application.OpenForms 
      If TypeOf pForm Is frmMain Then 
        pFrmMain = CType(pForm, frmMain) 
        Exit For 
      End If 
    Next 
    If pFrmMain Is Nothing Then Exit Sub 
    'Check if already open in a window - if so, bring to front instead 
    Dim pWinKey As String = "MFA_" & pMFA.ID.ToString() 
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
    Dim pTabTitle As String = "MFA #" & pMFA.ID.ToString() 
    Dim pFault As clsFault = pFrmMain.OpenEntityInNewTab("ctlc_MFA", pMFA, _Requester, pTabTitle) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Context menu - Copy ID (supports multi-select) 
  Private Sub tsmiCopyID_Click(sender As Object, e As EventArgs) Handles tsmiCopyID.Click 
    If dgvMFA.SelectedRows.Count = 0 Then Exit Sub 
    Dim pIDs As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvMFA.SelectedRows 
      If pSelectedRow.Index >= 0 AndAlso pSelectedRow.Index < _MFACol.Count Then 
        Dim pMFA As csMFA = _MFACol(pSelectedRow.Index) 
        If pIDs.Length > 0 Then pIDs.Append(", ") 
        pIDs.Append(pMFA.ID.ToString()) 
      End If 
    Next 
    If pIDs.Length > 0 Then 
      Clipboard.SetText(pIDs.ToString()) 
      Dim pCount As Integer = dgvMFA.SelectedRows.Count 
      ShowToast(If(pCount = 1, "ID copied: " & pIDs.ToString(), pCount.ToString() & " IDs copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows (supports multi-select, values only) 
  Private Sub tsmiCopyRow_Click(sender As Object, e As EventArgs) Handles tsmiCopyRow.Click 
    If dgvMFA.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvMFA.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvMFA.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row copied", pCount.ToString() & " rows copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows with Headers (supports multi-select) 
  Private Sub tsmiCopyRowHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopyRowHeaders.Click 
    If dgvMFA.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers from first row 
    Dim pFirstRow As DataGridViewRow = dgvMFA.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add all selected rows 
    For Each pSelectedRow As DataGridViewRow In dgvMFA.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvMFA.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row with headers copied", pCount.ToString() & " rows with headers copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy for Excel (with headers, VARCHAR fields wrapped in ="value" to preserve leading zeros) 
  Private Sub tsmiCopyExcel_Click(sender As Object, e As EventArgs) Handles tsmiCopyExcel.Click 
    If dgvMFA.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers 
    Dim pFirstRow As DataGridViewRow = dgvMFA.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add rows with Excel-safe formatting for text columns 
    For Each pSelectedRow As DataGridViewRow In dgvMFA.SelectedRows 
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
      Dim pCount As Integer = dgvMFA.SelectedRows.Count 
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
