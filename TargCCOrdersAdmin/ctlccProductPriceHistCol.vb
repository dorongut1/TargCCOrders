Public Class ctlccProductPriceHistCol
 
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
 
  Public Event evtBeforeUpdate(ByVal vProductPriceHist As clsProductPriceHist, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vProductPriceHist As clsProductPriceHist) 
  Private Event evtBeforeDelete(ByVal vProductPriceHist As clsProductPriceHist, ByRef rCancel As Nullable(Of Boolean)) 
  
  Public Event evtRowClicked(ByVal vProductPriceHist As clsProductPriceHist) 
  Public Event evtRowDoubleClicked(ByVal vProductPriceHist As clsProductPriceHist, ByRef rHandled As Boolean) 
  Public Event evtUnChosen() 
 
  Public Event evtOverrideLoadCbo(ByVal vParentName As clsProductPriceHist.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  
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
    Public Property DoNotSummarizeProperties As List(Of clsProductPriceHist.enmSummarizeableProperty) 
    Public Property SpreadsheetShowAllFields As Nullable(Of Boolean) 
    Public Property GridTitle As String 
    Public Property ReportTitle As String 
    Public Property [ReadOnly] As Boolean 
    Public Property CbosDoNotLoad As List(Of clsProductPriceHist.enmParentProperty) 
    Public Property ColumnsReadOnly As List(Of clsProductPriceHist.enmProperty) 
    Public Property ColumnsHide As List(Of clsProductPriceHist.enmProperty) 
    Public Property ColumnsFormat As Dictionary(Of clsProductPriceHist.enmProperty, String) 
    Public Property ColumnsOrdinalPosition As Dictionary(Of clsProductPriceHist.enmProperty, Integer) 
    Public Property ColumnsAlignment As Dictionary(Of clsProductPriceHist.enmProperty, DataGridViewContentAlignment) 
    Public Property ColumnsHeaderText As Dictionary(Of clsProductPriceHist.enmProperty, String) 
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
      _DoNotSummarizeProperties = New List(Of clsProductPriceHist.enmSummarizeableProperty) 
      _SpreadsheetShowAllFields = Nothing 
      _GridTitle = "" 
      _ReportTitle = "" 
      _ReadOnly = False 
      _CbosDoNotLoad = New List(Of clsProductPriceHist.enmParentProperty) 
      _ColumnsReadOnly = New List(Of clsProductPriceHist.enmProperty) 
      _ColumnsHide = New List(Of clsProductPriceHist.enmProperty) 
      _ColumnsFormat = New Dictionary(Of clsProductPriceHist.enmProperty, String) 
      _ColumnsOrdinalPosition = New Dictionary(Of clsProductPriceHist.enmProperty, Integer) 
      _ColumnsAlignment = New Dictionary(Of clsProductPriceHist.enmProperty, DataGridViewContentAlignment) 
      _ColumnsHeaderText = New Dictionary(Of clsProductPriceHist.enmProperty, String) 
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
 
  Private WithEvents _ProductPriceHistCol As clsProductPriceHistCol
  Private WithEvents _ProductPriceHistColFullLength As clsProductPriceHistCol
 
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
  Public ReadOnly Property [SelectedProductPriceHist]() As clsProductPriceHist 
    Get 
      If dgvProductPriceHist.SelectedRows.Count = 0 OrElse _Loading = True Then Return Nothing 
      Dim RowIndex As Integer = dgvProductPriceHist.SelectedRows(0).Cells(0).RowIndex 
      If RowIndex < 0 Then Return Nothing 
      If _Summarized = True AndAlso RowIndex = dgvProductPriceHist.Rows.Count - 1 Then dgvProductPriceHist.ClearSelection() : RaiseEvent evtUnChosen() : Return Nothing 
      Return _ProductPriceHistCol(RowIndex) 
    End Get 
  End Property 
  
  Public ReadOnly Property [ProductPriceHistCol]() As clsProductPriceHistCol 
    Get 
      Return _ProductPriceHistCol 
    End Get 
  End Property 
 
  Public Function LoadControl(ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    Dim pProductPriceHistCol As New clsProductPriceHistCol() 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    pFault = pProductPriceHistCol.Fill(_Requester) 
    If pFault.isOK = False Then Return pFault 
 
    pFault = LoadControl(pProductPriceHistCol)
    Return pFault 
  End Function 
 
  Public Function LoadControl(vProductPriceHistCol As clsProductPriceHistCol, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    Return LoadControl(vProductPriceHistCol) 
  End Function
  
  Private Function LoadControl(vProductPriceHistCol As clsProductPriceHistCol) As clsFault
    Dim pFault As New clsFault
 
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
    'Use evtBeforeLoad to set or remove the list type, if you don't want the default 
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList  
    RaiseEvent evtBeforeLoad() 
 
    LoadLocalizedText() 
 
    'keep safe in case 
    Dim pColumnsHides As List(Of clsProductPriceHist.enmProperty) = Nothing 
    If _LoadParameters.IsSumFillOnTheFly Then 
      pColumnsHides = New List(Of clsProductPriceHist.enmProperty) 
      pColumnsHides.AddRange(_LoadParameters.ColumnsHide) 
    End If 
 
    'Force blg and clc fields to read-only 
    
    'Check for ReadOnly columns 
    For Each l In _LoadParameters.ColumnsReadOnly 
      If l = clsProductPriceHist.enmProperty.ID Then colID.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.ProductID Then colProductID.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.CustomerType Then colCustomerType.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.BaseCost Then colBaseCost.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.SellingPrice Then colSellingPrice.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.MinQuantity Then colMinQuantity.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.DiscountPercent Then colDiscountPercent.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.ValidFrom Then colValidFrom.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.ValidTo Then colValidTo.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.ArchivedDate Then colArchivedDate.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.ArchivedReason Then colArchivedReason.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.OriginalPriceID Then colOriginalPriceID.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.Notes Then colNotes.ReadOnly = True 
      If l = clsProductPriceHist.enmProperty.AddFieldsHere Then colAddFieldsHere.ReadOnly = True 
    Next 
 
    For Each l In _LoadParameters.ColumnsHide 
      'Parents only 
    Next 
 
    If _LoadParameters.IsSumFillOnTheFly Then 
      'Use what we just save instead 
      _LoadParameters.ColumnsHide = pColumnsHides 
    End If 
 
    dgvProductPriceHist.DoubleBuffered(True) 
 
    pFault = vProductPriceHistCol.LoadLookupAndEnumText(_Requester) : If Not pFault.isOK Then Return pFault 
    
    'Now transfer to local collection 
    _ProductPriceHistColFullLength = vProductPriceHistCol.Clone() 
 
    'Truncate the strings 
    _ProductPriceHistCol = vProductPriceHistCol 
    If _LoadParameters.TruncateStrings Then 
      _ProductPriceHistCol.TruncateStrings() 
    Else 
      dgvProductPriceHist.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
      dgvProductPriceHist.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders 
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
    For Each lCol As DataGridViewColumn In dgvProductPriceHist.Columns 
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
    For Each p As clsProductPriceHist.enmProperty In _LoadParameters.ColumnsHide 
      Dim pGridSetting As clsGridSetting = _GridSettings.FindByColumnName("col" & p.ToString()) 
      'HideColumn(p.ToString) 
      pGridSetting.ColumnRemoved = True 
    Next 
     
    'Set Header Text 
    For Each pD In _LoadParameters.ColumnsHeaderText 
      dgvProductPriceHist.Columns("col" & pD.Key.ToString).HeaderText = pD.Value 
    Next 
 
    'Format Columns 
    For Each pD In _LoadParameters.ColumnsFormat 
      dgvProductPriceHist.Columns("col" & pD.Key.ToString).DefaultCellStyle.Format = pD.Value 
    Next 
 
    'ordinal position 
    For Each pD In _LoadParameters.ColumnsOrdinalPosition 
      dgvProductPriceHist.Columns("col" & pD.Key.ToString).DisplayIndex = pD.Value 
    Next 
    _GridSettings.Update(Me, _Requester)
    
    'Align Columns 
    For Each pD In _LoadParameters.ColumnsAlignment 
      dgvProductPriceHist.Columns("col" & pD.Key.ToString).DefaultCellStyle.Alignment = pD.Value 
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
 
    dgvProductPriceHist.ClearSelection()
    bsCtlProductPriceHist.DataSource = Nothing 
    
    pFault = LoadSupportingCombos() : If pFault.isOK = False Then Return pFault 
 
    lblGrid.Text = _LoadParameters.GridTitle 
    If lblGrid.Text = "" Then 
      'Assume chkAutoRefresh is not used either. (may have to add it to LoadParameters) 
      pnlHeader.Visible = False 
    End If 
    Try
      LoadGrid()
    Catch ex As Exception
      Return pFault.LogException(ex, "LoadGrid", "TRGT-ProductPriceHist-090124-2345", _Requester) 
    End Try
    
    RaiseEvent evtLoaded() 
    
    'Show row count in status label 
    lblStatus.ForeColor = Color.DarkGreen 
    lblStatus.Text = dgvProductPriceHist.RowCount & " rows" 
    
    'now do the default sorts 
    If _SortList IsNot Nothing Then 
      _AutoSorting = True 
      _PrevSortColumn = Nothing 
      For Each i In _SortList 
        Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(i, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0)) 
        dgvProductPriceHist_ColumnHeaderMouseClick(Me, pE) 
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
    If dgvProductPriceHist.SelectedRows.Count > 0 Then 
      pRowIndex = dgvProductPriceHist.SelectedRows(0).Cells(0).RowIndex 
    Else 
      If dgvProductPriceHist.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
        pRowIndex = dgvProductPriceHist.CurrentCellAddress.Y 
      End If 
    End If 
 
    If _LoadParameters.SummarizeGrid = True Then Summarize() 
 
    _Loading = True 
 
    bsCtlProductPriceHist.DataSource = Nothing 
    bsCtlProductPriceHist.DataSource = _ProductPriceHistCol
    
    dgvProductPriceHist.ClearSelection() 
    
    RaiseEvent evtUnChosen()
    
    SetUpBNButtons(False)
    'set columns 
    LoadColumns() 
 
    'Load buttons 
    For Each p As ToolStripMenuItem In btnColumns.DropDownItems 
      If p Is mnuColsReset OrElse p Is mnuColsHideMost Then Continue For 
      Dim pMenuItemProprty As String = p.Name.Substring(13) 
      p.Checked = dgvProductPriceHist.Columns("col" & pMenuItemProprty).Visible 
    Next 
 
    If pRowIndex >= 0 Then 
      If _Summarized = True Then 
        If pRowIndex <= _ProductPriceHistCol.Count - 2 Then 
          dgvProductPriceHist.Rows(pRowIndex).Selected = True 
        End If 
      Else 
        If pRowIndex <= _ProductPriceHistCol.Count - 1 Then 
          dgvProductPriceHist.Rows(pRowIndex).Selected = True 
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
    
    Dim pPrompt As String 
    Dim pChoose As String = GetChoose(_Requester) 
    Dim pEnumCol As clsComboList = Nothing 
    'Load comboLists 
    'EnumCustomerType
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(clsProductPriceHist.enmParentProperty.CustomerType, Nothing, Nothing, pEnumCol, pPrompt) 
    If pEnumCol Is Nothing Then 
      pEnumCol = New clsComboList 
      pFault = pEnumCol.FillEnums(clsEnums.enmEnum.CustomerType, _Requester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      pFault.SetOK() 
    End If 
    pEnumCol.Remove(pEnumCol.FindByKey(clsEnums.enmCustomerType.UD)) 
    pEnumCol.SortByText() 
    If pPrompt = "" Then 
      pPrompt = pChoose 
    End If 
    pEnumCol.AddToTop(clsEnums.enmCustomerType.UD, pPrompt) 
    bsCustomerType.DataSource = pEnumCol 
    colCustomerType.Tag = pPrompt 

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
      btnDelete.Visible = False 
      btnCeaseEdit.Visible = False 
    Else 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, _Requester) = True AndAlso _LoadParameters.ImportButtonHide = False Then btnImport.Visible = vInEdit Else btnImport.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, _Requester) = True Then btnAdd.Visible = vInEdit Else btnAdd.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_ProductPriceHistDelete, _Requester) = True Then btnDelete.Visible = vInEdit Else btnDelete.Visible = False 
      btnCeaseEdit.Visible = vInEdit 
      If _LoadParameters.AddEditDeleteButtonsHide = True Then 
        btnAdd.Visible = False 
        btnDelete.Visible = False 
      End If 
    End If 
    If vInEdit = True AndAlso _LoadParameters.AddEditDeleteButtonsHide = False Then 
      colID.ReadOnly = True 
      dgvProductPriceHist.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 
      dgvProductPriceHist.SelectionMode = DataGridViewSelectionMode.CellSelect 
      _DVGDirty = False 
    Else 
      dgvProductPriceHist.EditMode = DataGridViewEditMode.EditProgrammatically 
      dgvProductPriceHist.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
      dgvProductPriceHist.AllowUserToDeleteRows = False 
      dgvProductPriceHist.AllowUserToAddRows = False 
      'Don't automatically set the 1st one If dgvProductPriceHist.Rows.Count > 0 Then 
      '  Dim pCurrentRow As Integer 
      '  pCurrentRow = dgvProductPriceHist.CurrentRow.Index 
      '  dgvProductPriceHist.CurrentCell = dgvProductPriceHist.Rows(pCurrentRow).Cells(0) 
      '  dgvProductPriceHist.Rows(pCurrentRow).Selected = True 
      'End If 
    End If 
    If vInEdit = True Then 
      lblEditMode.Text = "Edit Mode" 
      tssReports.Visible = True 
      btnSpreadsheet.Enabled = False 
      btnReport.Enabled = False 
    Else 
      If _ProductPriceHistCol.Count = 0 Then 
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
    dgvProductPriceHist.Refresh() 
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
    pFieldList.Append("ProductID, ") 
    pFieldList.Append("CustomerType (DB Code), ") 
    pFieldList.Append("BaseCost, ") 
    pFieldList.Append("SellingPrice, ") 
    pFieldList.Append("MinQuantity, ") 
    pFieldList.Append("DiscountPercent, ") 
    pFieldList.Append("ValidFrom, ") 
    pFieldList.Append("ValidTo, ") 
    pFieldList.Append("ArchivedDate, ") 
    pFieldList.Append("ArchivedReason, ") 
    pFieldList.Append("OriginalPriceID, ") 
    pFieldList.Append("Notes, ") 
    pFieldList.Append("AddFieldsHere, ") 
    
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
    pMessage &= "If there is no ID field (the 1st field is ProductID), then I will delete the table and recreate it with the data in this spreadsheet" 
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
    Dim pIncomingProductPriceHists As New clsProductPriceHistCol() 
 
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
          Dim pIncomingProductPriceHist As New clsProductPriceHist() 
          pIncomingProductPriceHist.Tag = "Row " & pRow.ToString 
          pCurrentRow = pReader.ReadFields() 
          If pRow = 0 Then 
            If pCurrentRow.Length = pNumberOfFields - 1 Then 
              pNoPrimaryKey = True 
              pFault = clsProductPriceHistCol.Delete(_Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
              pNumberOfFields = pNumberOfFields - 1 
            End If 
            Continue While 'Header line  
          End If 
 
          If pCurrentRow.Length <> pNumberOfFields Then 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ": There should be " & pNumberOfFields & " fields, but there are actually " & pCurrentRow.Length & " fields." 
            pIncomingProductPriceHists.Add(pIncomingProductPriceHist) 
            Continue While 
          End If 
 
          Dim pFieldNo As Integer = -1 
 
          If pNoPrimaryKey = False Then 
            Try 
              pFieldNo += 1 
              pFieldName = "ID" 
              pIncomingProductPriceHist.ID = CType(pCurrentRow(pFieldNo), Long) 
            Catch ex As Exception 
              pErrorFound = True 
              pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
            End Try 
          End If 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ProductID" 
            pIncomingProductPriceHist.ProductID = CType(pCurrentRow(pFieldNo), Long) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "CustomerType" 
            pIncomingProductPriceHist.CustomerType = clsEnums.TranslateEnmCustomerType(pCurrentRow(pFieldNo)) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "BaseCost" 
            pIncomingProductPriceHist.BaseCost = CType(pCurrentRow(pFieldNo), Decimal) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "SellingPrice" 
            pIncomingProductPriceHist.SellingPrice = CType(pCurrentRow(pFieldNo), Decimal) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "MinQuantity" 
            pIncomingProductPriceHist.MinQuantity = CType(pCurrentRow(pFieldNo), Integer) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "DiscountPercent" 
            pIncomingProductPriceHist.DiscountPercent = CType(pCurrentRow(pFieldNo), Decimal) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ValidFrom" 
            pIncomingProductPriceHist.ValidFrom = CType(pCurrentRow(pFieldNo), Date) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ValidTo" 
            pIncomingProductPriceHist.ValidTo = CType(pCurrentRow(pFieldNo), Date) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ArchivedDate" 
            pIncomingProductPriceHist.ArchivedDate = CType(pCurrentRow(pFieldNo), Date) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ArchivedReason" 
            pIncomingProductPriceHist.ArchivedReason = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "OriginalPriceID" 
            pIncomingProductPriceHist.OriginalPriceID = CType(pCurrentRow(pFieldNo), Long) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "Notes" 
            pIncomingProductPriceHist.Notes = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "AddFieldsHere" 
            pIncomingProductPriceHist.AddFieldsHere = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingProductPriceHist.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          If pIncomingProductPriceHist.Tag = "Row " & pRow.ToString Then 
            pIncomingProductPriceHist.Tag &= ": OK" 
          End If 
 
          pIncomingProductPriceHists.Add(pIncomingProductPriceHist) 
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
          My.Computer.FileSystem.WriteAllText(pFilenameOut, pIncomingProductPriceHists.ToCSV, False) 
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
    pFault = pIncomingProductPriceHists.UpdateFromCollection(_Requester) 
    If pFault.isOK = False Then 
      ShowFault(pFault, _Requester) 
      Exit Sub 
    End If 
    'Reset the ProductPriceHist collection 
    MyCache.ClearComboList(clsEnums.enmComboListType.ccProductPriceHistDefaultByID) 
 
    Cursor = Cursors.Default 
 
    'Check that there were no problems 
    pErrorFound = False 
    For Each p In pIncomingProductPriceHists 
      If p.Tag <> "OK" Then 
        pErrorFound = True 
      End If 
    Next 
    If pErrorFound = True Then 
      Try 
        My.Computer.FileSystem.WriteAllText(pFilenameOut, pIncomingProductPriceHists.ToCSV, False) 
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
      _ProductPriceHistCol = pIncomingProductPriceHists 
      LoadGrid() 
      frmMessageOrInputBox.ShowMsg("Update Successful! Please click on Refresh to see all the data", frmMessageOrInputBox.enmIconType.Information) 
    End If 
 
  End Sub 
 
  'ExternalButtons 
  Private Sub DoEdit() 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    Dim pCellRow As Integer = -1 
    Dim pCellCol As Integer = -1 
 
 
    If dgvProductPriceHist.Focused = True AndAlso dgvProductPriceHist.SelectedRows.Count > 0 Then 
      pCellRow = dgvProductPriceHist.CurrentCell.RowIndex 
      pCellCol = dgvProductPriceHist.CurrentCell.ColumnIndex 
    End If 
 
    Try 'in case it's empty 
      dgvProductPriceHist.CurrentCell = dgvProductPriceHist.Rows(0).Cells(0) 
      dgvProductPriceHist.CurrentCell.Selected = True 
    Catch ex As Exception 
    End Try 
 
 
    'remove summary row 
    If _LoadParameters.SummarizeGrid = True AndAlso _ProductPriceHistCol.Count > 0 AndAlso _ProductPriceHistCol(_ProductPriceHistCol.Count - 1).ID = 0 Then 
      _ProductPriceHistCol.RemoveAt(_ProductPriceHistCol.Count - 1) 
      bsCtlProductPriceHist.DataSource = Nothing 
      bsCtlProductPriceHist.DataSource = _ProductPriceHistCol 
      _Summarized = False 
    End If 
 
    SetUpBNButtons(True) 
    If pCellRow >= 0 AndAlso pCellCol >= 0 Then 
      dgvProductPriceHist.Focus() 
      dgvProductPriceHist.CurrentCell = dgvProductPriceHist.Rows(pCellRow).Cells(pCellCol) 
      dgvProductPriceHist.CurrentCell.Selected = True 
    ElseIf _ProductPriceHistCol.Count = 0 Then 
    Else 
      Try 'in case the cell is hidden.... 
        dgvProductPriceHist.CurrentCell = dgvProductPriceHist.Rows(0).Cells(0) 
        dgvProductPriceHist.CurrentCell.Selected = True 
      Catch ex As Exception 
      End Try 
    End If 
  End Sub 
  Private Sub DoAdd() 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True Then Exit Sub 
    bsCtlProductPriceHist.AddNew() 
 
    'Now choose any needed fields 
    Dim pEntity As clsProductPriceHist 
    pEntity = CType(bsCtlProductPriceHist.Current, clsProductPriceHist) 
 
    For Each l In _LoadParameters.SearchFilters 
      If l.Key.ToString().EndsWith("ID") Then 
        CallByName(pEntity, l.Key.ToString(), CallType.Set, l.Value) 
      ElseIf l.Key.ToString().EndsWith("Code") Then 
        CallByName(pEntity, l.Key.ToString(), CallType.Set, l.Value) 
      End If 
    Next 
 
  End Sub 
  Private Function DoDelete() As clsFault 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As New clsFault 
    pFault.SetOK() 
    
    If dgvProductPriceHist.CurrentCell Is Nothing Then Return pFault 
    
    If dgvProductPriceHist.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      Dim pProductPriceHist As clsProductPriceHist 
      pProductPriceHist = CType(bsCtlProductPriceHist.Current, clsProductPriceHist) 
      If pProductPriceHist Is Nothing Then 
        pFault.LogFreeTextFault("There is no ProductPriceHist to delete", "", "TRGT-110303-165408", _Requester) 
        Return pFault 
      End If 
      Dim pOriginalCol As Integer = dgvProductPriceHist.CurrentCell.ColumnIndex 
      Dim pOriginalRow As Integer = dgvProductPriceHist.CurrentCell.RowIndex 
      'show row as selected  
      dgvProductPriceHist.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
      dgvProductPriceHist.EditMode = DataGridViewEditMode.EditProgrammatically 
      dgvProductPriceHist.CurrentRow.Selected = True 
      If pProductPriceHist.ID > 0 Then 
        Dim pRequest As String = "Are you sure you want to delete '" & pProductPriceHist.ProductID.ToString() & " " & pProductPriceHist.CustomerType.FastToString() & "'?" 
        Dim pCancel As Nullable(Of Boolean) = Nothing 
        RaiseEvent evtBeforeDelete(pProductPriceHist, pCancel) 
        If pCancel = True Then 
          Return pFault 
        ElseIf pCancel Is Nothing Then 
          Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
          pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
          If pResponse = frmMessageOrInputBox.enmButtonReturned.No Then 
            dgvProductPriceHist.SelectionMode = DataGridViewSelectionMode.CellSelect 
            dgvProductPriceHist.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 
            dgvProductPriceHist.Rows(pOriginalRow).Cells(pOriginalCol).Selected = True 
            Return pFault 
          End If 
        End If 
        pFault = pProductPriceHist.Delete(_Requester) : If pFault.isOK = False Then Return pFault 
      End If 
      bsCtlProductPriceHist.Remove(bsCtlProductPriceHist.Current) 
      LoadGrid() 
    End If 
    Return pFault 
  End Function 
  Private Sub DoCeaseEdit() 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True And _DVGDirty = False Then 
      bsCtlProductPriceHist.DataSource = _ProductPriceHistCol 
    End If 
    If _DVGDirty = True Then 
      RaiseEvent evtTimerTripped() 
      Exit Sub 
    End If 
    Dim pProductPriceHist As clsProductPriceHist = CType(bsCtlProductPriceHist.Current, clsProductPriceHist) 
    If pProductPriceHist IsNot Nothing Then 
      If pProductPriceHist.ID = 0 Then 
        _IgnoreGridFault = True 
        bsCtlProductPriceHist.Remove(bsCtlProductPriceHist.Current) 
        _IgnoreGridFault = False 
      End If 
    End If 
    SetUpBNButtons(False) 
    If _ProductPriceHistCol.Count > 0 AndAlso dgvProductPriceHist.CurrentCell IsNot Nothing Then 
      For i As Integer = 0 To dgvProductPriceHist.Columns.Count - 1 
        If dgvProductPriceHist.Columns(i).Visible Then 
          dgvProductPriceHist.CurrentCell = dgvProductPriceHist.Rows(dgvProductPriceHist.CurrentCell.RowIndex).Cells(i) 
          Exit For 
        End If 
      Next 
      dgvProductPriceHist.Refresh() 
      dgvProductPriceHist.Rows(dgvProductPriceHist.CurrentCell.RowIndex).Selected = True 
    Else 
      dgvProductPriceHist.Refresh() 
    End If 
  End Sub 
  'Grid RowValidating 
  Private Sub dgvProductPriceHist_RowValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvProductPriceHist.RowValidating 
    If _Loading = True OrElse dgvProductPriceHist.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True Then 
      e.Cancel = True 
      dgvProductPriceHist.CurrentCell = dgvProductPriceHist.Rows(e.RowIndex).Cells(e.ColumnIndex) 
    End If 
  End Sub 
  'CellFormatting  
  Private Sub dgvProductPriceHist_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvProductPriceHist.CellFormatting 
    '_Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    If dgvProductPriceHist.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      If dgvProductPriceHist.Columns(e.ColumnIndex).ReadOnly = False Then 
        Exit Sub 
      End If 
    End If 
 
    RaiseEvent evtCellFormatting(sender, e) 
 
    ' Sample code evtCellFormatting - evtCellFormatting 
    ' You can use this to colour the fonts or your cell background or anything else that requires complete control of your cell 
    'Dim pProductPriceHist As clsProductPriceHist = Nothing 
    'If dgvProductPriceHist.Columns(e.ColumnIndex).Name = colRecommendedQuantityToOrder.Name Then 
    '  If pProductPriceHist Is Nothing Then pProductPriceHist = CType(dgvProductPriceHist.Rows(e.RowIndex).DataBoundItem, clsProductPriceHist) ' Only assign it if needed 
    '  If pProductPriceHist.CustomerOrders > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pProductPriceHist.CustomerOrders > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
    'If dgvProductPriceHist.Columns(e.ColumnIndex).Name = colRAV.Name Then 
    '  If pProductPriceHist Is Nothing Then pProductPriceHist = CType(dgvProductPriceHist.Rows(e.RowIndex).DataBoundItem, clsProductPriceHist) ' Only assign it if needed
    '  If pProductPriceHist.RAV > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pProductPriceHist.RAV - pProductPriceHist.MaximumStock > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
 
    'Debug.Print("loc x,y:" & e.RowIndex & ", " & e.ColumnIndex & ": GetType" & dgvProductPriceHist.Columns(e.ColumnIndex).GetType.ToString & ": zValue" & e.Value.ToString) 
    If dgvProductPriceHist.Columns(e.ColumnIndex).GetType.ToString = "System.Windows.Forms.DataGridViewComboBoxColumn" Then 
      Dim pCol As System.Windows.Forms.DataGridViewComboBoxColumn = CType(dgvProductPriceHist.Columns(e.ColumnIndex), System.Windows.Forms.DataGridViewComboBoxColumn) 
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
          e.Value = "* BadCode '" & dgvProductPriceHist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & "' *" 
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
 
    If e.ColumnIndex = colBaseCost.Index Then 
      If CType(e.Value, Decimal) = 0D Then e.Value = "" 
    End If 
    
    If e.ColumnIndex = colMinQuantity.Index Then 
      If CType(e.Value, Integer) = 1 Then e.Value = "" 
    End If 
    
    If e.ColumnIndex = colDiscountPercent.Index Then 
      If CType(e.Value, Decimal) = 0D Then e.Value = "" 
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
 
    If dgvProductPriceHist.Columns(e.ColumnIndex).GetType.Name.Equals("DataGridViewImageColumn", StringComparison.OrdinalIgnoreCase) Then 
      If e.Value Is Nothing Then 
        e.Value = New Bitmap(1, 1) 
      End If 
    End If 
 
    If _Summarized = True Then 
      If e.RowIndex = dgvProductPriceHist.Rows.Count - 1 Then 
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
            If _SummaryOverFlow.IndexOf(dgvProductPriceHist.Columns(e.ColumnIndex).Name.Substring(3)) >= 0 Then 
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
  Private Sub dgvProductPriceHist_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProductPriceHist.CellValueChanged 
    If e.RowIndex < 0 Then Exit Sub 
 
  End Sub 
 
  'Grid Sort
  Private Sub dgvProductPriceHist_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvProductPriceHist.ColumnHeaderMouseClick
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewColumn As DataGridViewColumn = dgvProductPriceHist.Columns(e.ColumnIndex)
    If bsCtlProductPriceHist.Current Is Nothing Then Exit Sub

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
    dgvProductPriceHist.SuspendLayout()

    Dim pProductPriceHist As clsProductPriceHist
    Dim pID As Long = 0 
    If dgvProductPriceHist.SelectedRows.Count > 0 Then 
    pProductPriceHist = CType(bsCtlProductPriceHist.Current, clsProductPriceHist)
      pID = pProductPriceHist.ID 
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
    Dim pProductPriceHistCol As clsProductPriceHistCol
    pProductPriceHistCol = CType(bsCtlProductPriceHist.DataSource, clsProductPriceHistCol)

    Dim pSummaryRow As clsProductPriceHist = Nothing 
    If _Summarized = True Then 
      pSummaryRow = pProductPriceHistCol(pProductPriceHistCol.Count - 1) 
      pProductPriceHistCol.RemoveAt(pProductPriceHistCol.Count - 1) 
    End If 
 
    If pNewSortOrder = SortOrder.Ascending Then
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
        'save the present sort 
        For iCntr As Integer = 0 To pProductPriceHistCol.Count - 1 
          pProductPriceHistCol(iCntr).Tag = iCntr.ToString("0000000000") 
        Next 
      End If 
      If pNewColumn Is colID Then
        pProductPriceHistCol.SortByID()
      ElseIf pNewColumn Is colProductID Then
        pProductPriceHistCol.SortByProductID()
      ElseIf pNewColumn Is colCustomerType Then
        pProductPriceHistCol.SortByCustomerType()
      ElseIf pNewColumn Is colBaseCost Then
        pProductPriceHistCol.SortByBaseCost()
      ElseIf pNewColumn Is colSellingPrice Then
        pProductPriceHistCol.SortBySellingPrice()
      ElseIf pNewColumn Is colMinQuantity Then
        pProductPriceHistCol.SortByMinQuantity()
      ElseIf pNewColumn Is colDiscountPercent Then
        pProductPriceHistCol.SortByDiscountPercent()
      ElseIf pNewColumn Is colValidFrom Then
        pProductPriceHistCol.SortByValidFrom()
      ElseIf pNewColumn Is colValidTo Then
        pProductPriceHistCol.SortByValidTo()
      ElseIf pNewColumn Is colArchivedDate Then
        pProductPriceHistCol.SortByArchivedDate()
      ElseIf pNewColumn Is colArchivedReason Then
        pProductPriceHistCol.SortByArchivedReason()
      ElseIf pNewColumn Is colOriginalPriceID Then
        pProductPriceHistCol.SortByOriginalPriceID()
      ElseIf pNewColumn Is colNotes Then
        pProductPriceHistCol.SortByNotes()
      ElseIf pNewColumn Is colAddFieldsHere Then
        pProductPriceHistCol.SortByAddFieldsHere()
      End If
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
      Dim iCntr As Integer = 0 
        If pNewColumn Is colID Then
          Dim pTest As Long = 0 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.ID <> pTest Then iCntr += 1 : pTest = p.ID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colProductID Then
          Dim pTest As Long = 0 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.ProductID <> pTest Then iCntr += 1 : pTest = p.ProductID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colCustomerType Then
          Dim pTest As clsEnums.enmCustomerType = clsEnums.enmCustomerType.UD 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.CustomerType <> pTest Then iCntr += 1 : pTest = p.CustomerType 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colBaseCost Then
          Dim pTest As Decimal = 0 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.BaseCost <> pTest Then iCntr += 1 : pTest = p.BaseCost 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colSellingPrice Then
          Dim pTest As Decimal = 0 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.SellingPrice <> pTest Then iCntr += 1 : pTest = p.SellingPrice 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colMinQuantity Then
          Dim pTest As Integer = 0 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.MinQuantity <> pTest Then iCntr += 1 : pTest = p.MinQuantity 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colDiscountPercent Then
          Dim pTest As Decimal = 0 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.DiscountPercent <> pTest Then iCntr += 1 : pTest = p.DiscountPercent 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colValidFrom Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.ValidFrom <> pTest Then iCntr += 1 : pTest = p.ValidFrom 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colValidTo Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.ValidTo <> pTest Then iCntr += 1 : pTest = p.ValidTo 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colArchivedDate Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.ArchivedDate <> pTest Then iCntr += 1 : pTest = p.ArchivedDate 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colArchivedReason Then
          Dim pTest As String = "" 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.ArchivedReason <> pTest Then iCntr += 1 : pTest = p.ArchivedReason 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colOriginalPriceID Then
          Dim pTest As Long = 0 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.OriginalPriceID <> pTest Then iCntr += 1 : pTest = p.OriginalPriceID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colNotes Then
          Dim pTest As String = "" 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.Notes <> pTest Then iCntr += 1 : pTest = p.Notes 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAddFieldsHere Then
          Dim pTest As String = "" 
          For Each p As clsProductPriceHist In pProductPriceHistCol 
            If p.AddFieldsHere <> pTest Then iCntr += 1 : pTest = p.AddFieldsHere 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        End If 
        pProductPriceHistCol.SortByTag() 
      End If 
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
      _PrevSortColumn = pNewColumn
      pPrevSortOrder = SortOrder.Ascending
    Else
      pProductPriceHistCol.Reverse()
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
      pPrevSortOrder = SortOrder.Descending
    End If

    If _Summarized = True Then 
      pProductPriceHistCol.Add(pSummaryRow) 
    End If 
 
    If pID > 0 Then
      bsCtlProductPriceHist.Position = bsCtlProductPriceHist.IndexOf(pProductPriceHistCol.FindByID(pID))
    End If

    'dgvProductPriceHist.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
    dgvProductPriceHist.ResumeLayout()

    Cursor = Cursors.Default
    dgvProductPriceHist.Refresh()

  End Sub
  'Select Row 
  Public Sub SelectRowByObjectID(ByVal pID As Long) 
    If pID > 0 Then 
      Dim pProductPriceHistCol As clsProductPriceHistCol 
      pProductPriceHistCol = CType(bsCtlProductPriceHist.DataSource, clsProductPriceHistCol) 
      Dim pProductPriceHist As clsProductPriceHist = pProductPriceHistCol.FindByID(pID) 
      If Not pProductPriceHist.IsEmpty Then 
        bsCtlProductPriceHist.Position = bsCtlProductPriceHist.IndexOf(pProductPriceHistCol.FindByID(pID)) 
        dgvProductPriceHist.Rows(bsCtlProductPriceHist.Position).Selected = True 
      Else 
        dgvProductPriceHist.ClearSelection() 
      End If 
    ElseIf pID = 0 Then 
      dgvProductPriceHist.ClearSelection() 
    End If 
  End Sub 
  
  'Grid Resize
  Private Sub dgvProductPriceHist_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvProductPriceHist.ColumnHeaderMouseDoubleClick
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    dgvProductPriceHist.AutoResizeColumn(e.ColumnIndex)
    Cursor = Cursors.Default
  End Sub
  'Other Grid Events
  Private Sub dgvProductPriceHist_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvProductPriceHist.CurrentCellDirtyStateChanged
   _DVGDirty = True 
  End Sub
  Private Sub dgvProductPriceHist_Scroll(sender As Object, e As ScrollEventArgs) Handles dgvProductPriceHist.Scroll
    dgvProductPriceHist.Invalidate() 
  End Sub
 
  Private Sub dgvProductPriceHist_DataFault(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvProductPriceHist.DataError
    'Dim pFault As New clsFault
    '
    'If e.RowIndex = dgvProductPriceHist.Rows.Count - 1 Then Exit Sub
 
    'If dgvProductPriceHist.Columns(e.ColumnIndex).Name.StartsWith("colIDin", StringComparison.OrdinalIgnoreCase) Then Exit Sub 
 
    'If _IgnoreGridFault = True Then Exit Sub
    '_DVGDirty = False 
    'Static pShown As Boolean 
    '
    'Dim pSubStrg As New System.Text.StringBuilder 
    ''Other Error 
    'Try 
    '  Try 
    '    pSubStrg.AppendLine("In table 'ProductPriceHist', the row with an ID of " & dgvProductPriceHist.Rows(e.RowIndex).Cells(0).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine("In grid 'ProductPriceHist', row index " & e.RowIndex) 
    '  End Try 
    '  Try 
    '    pSubStrg.AppendLine(" has an invalid value of " & dgvProductPriceHist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine(" has an invalid value of Nothing.") 
    '  End Try 
    '  pSubStrg.AppendLine(" in column " & dgvProductPriceHist.Columns(e.ColumnIndex).DataPropertyName) 
    'Catch ex As Exception 
    '  pSubStrg.AppendLine("; Failed trying to fill DataFault as well!") 
    'End Try 
    'pFault.LogException(209, e.Exception, pSubStrg.ToString, "TRGT-ProductPriceHist-100409-2248", _Requester) 
    'If pShown = False Then 
    '  Dim pCell As DataGridViewCell 
    '  Try 
    '    pCell = dgvProductPriceHist(e.ColumnIndex, e.RowIndex)
    '  Catch ex As Exception 
    '    pCell = dgvProductPriceHist(0, 0)
    '  End Try 
    '  ShowFault(pFault, _Requester) 
    '  pShown = True 
    'End If 
  End Sub
  Private Sub dgvProductPriceHist_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvProductPriceHist.KeyDown 
    If e.KeyCode = Keys.Escape Then 
      'DoCeaseEdit() 
      Dim pProductPriceHist As clsProductPriceHist = CType(bsCtlProductPriceHist.Current, clsProductPriceHist) 
      If pProductPriceHist IsNot Nothing Then 
        If pProductPriceHist.ID = 0 Then 
          _IgnoreGridFault = True 
          bsCtlProductPriceHist.Remove(bsCtlProductPriceHist.Current) 
          _IgnoreGridFault = False 
        End If 
      End If 
      SetUpBNButtons(False) 
      'dgvProductPriceHist.CurrentCell = dgvProductPriceHist.Rows(dgvProductPriceHist.CurrentCell.RowIndex).Cells(0) 
      dgvProductPriceHist.Refresh() 
    End If 
  End Sub 
  Private Sub dgvProductPriceHist_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvProductPriceHist.ColumnWidthChanged
    If Me.DesignMode = True Then Exit Sub 
    If _Loading = False Then SaveSizes()
  End Sub
  Private Sub dgvProductPriceHist_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvProductPriceHist.ColumnDisplayIndexChanged
    Cursor = Cursors.WaitCursor
    If _Loading = False Then SaveSizes()
    Cursor = Cursors.Default
  End Sub
  Private Sub dgvProductPriceHist_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductPriceHist.CellDoubleClick 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso e.RowIndex = dgvProductPriceHist.Rows.Count - 1 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
    Dim pCell As DataGridViewCell = dgvProductPriceHist(e.ColumnIndex, e.RowIndex) 
 
    Dim pHandled As Boolean = False 
    Dim pProductPriceHist As clsProductPriceHist = _ProductPriceHistCol(e.RowIndex)
    RaiseEvent evtRowDoubleClicked(pProductPriceHist, pHandled) 
    Cursor = Cursors.Default 
 
    If pHandled = False Then 
      If Me.ParentForm.Name.Equals("frmPopup", StringComparison.OrdinalIgnoreCase) Then Return 
      frmPopup.Text = "ProductPriceHist Detail" 
      Dim pFault As clsFault = frmPopup.LoadControl("ctlccProductPriceHist", pProductPriceHist, _Requester) 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
      frmPopup.ShowDialog() 
    End If 
 
  End Sub 
  Private Sub dgvProductPriceHist_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvProductPriceHist.SelectionChanged 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If _Loading = True Then Exit Sub 
    If DateTime.Now < _IgnoreSelectionUntil Then Exit Sub
    If _ProcessingSelection Then Exit Sub
    If dgvProductPriceHist.SelectedRows.Count = 0 Then 
      RaiseEvent evtUnChosen() 
      Exit Sub 
    End If 
    Dim RowIndex As Integer = dgvProductPriceHist.SelectedRows(0).Cells(0).RowIndex 
    If RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso RowIndex = dgvProductPriceHist.Rows.Count - 1 Then dgvProductPriceHist.ClearSelection() : RaiseEvent evtUnChosen() : Exit Sub 
    Dim pProductPriceHist As clsProductPriceHist = _ProductPriceHistCol(RowIndex)
    _ProcessingSelection = True
    Try
      RaiseEvent evtRowClicked(pProductPriceHist) 
    Finally
      _IgnoreSelectionUntil = DateTime.Now.AddMilliseconds(500)
      _ProcessingSelection = False
    End Try
  End Sub 
  Private Sub dgvProductPriceHist_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductPriceHist.RowLeave 
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
 
    Dim pOriginalCol As Integer = dgvProductPriceHist.CurrentCell.ColumnIndex 
     
    'If user clicked on CeaseEdit without changing cells, the data will not be received 
    ' therefore we have to fake exiting the cell 
    Dim pNewCol As Integer 
    'We can only go to a visible cell! 
    If pOriginalCol > 0 Then 
      pNewCol = pOriginalCol - 1 
      Do Until dgvProductPriceHist.Columns(pNewCol).Visible = True OrElse pNewCol = 0 
        pNewCol = pNewCol - 1 
      Loop 
    Else 
      pNewCol = 1 
    End If 
    If dgvProductPriceHist.Columns(pNewCol).Visible = False Then 
      dgvProductPriceHist.Columns(pNewCol).Visible = True 
      dgvProductPriceHist.CurrentCell = dgvProductPriceHist.CurrentRow.Cells(pNewCol) 
      dgvProductPriceHist.CurrentCell = dgvProductPriceHist.CurrentRow.Cells(pOriginalCol) 
      dgvProductPriceHist.Columns(pNewCol).Visible = False 
    Else 
      dgvProductPriceHist.CurrentCell = dgvProductPriceHist.CurrentRow.Cells(pNewCol) 
      dgvProductPriceHist.CurrentCell = dgvProductPriceHist.CurrentRow.Cells(pOriginalCol) 
    End If 
    dgvProductPriceHist.Rows(dgvProductPriceHist.CurrentCell.RowIndex).Selected = True 
    Dim pProductPriceHist As clsProductPriceHist 
    pProductPriceHist = CType(bsCtlProductPriceHist.Current, clsProductPriceHist) 
 
    'Add required data (primary keys) from parent objects  
    RaiseEvent evtBeforeUpdate(CType(pProductPriceHist, clsProductPriceHist), pCancel) 
    If pCancel = True Then 
      _DVGDirty = False 
      RaiseEvent evtTimerTripped() 
      Return True 
    End If 
    pFault = pProductPriceHist.Update(_Requester) 
    If pFault.isOK = False AndAlso pFault.Severity <> clsEnums.enmFaultSeverity.LogOnly Then 
      ShowFault(pFault, _Requester) 
      frmMessageOrInputBox.ShowMsg("Fix the problem, or click on 'Esc' to remove the row.", frmMessageOrInputBox.enmIconType.Information, frmMessageOrInputBox.enmButtons.Yes) 
      Return True 
    Else 
      If pFault.isOK = False Then 'AndAlso pFault.Severity = clsEnums.enmFaultSeverity.LogOnly  
        ShowFault(pFault, _Requester) 
      End If 
      dgvProductPriceHist.EndEdit() 
      _DVGDirty = False 
      'Reset the ProductPriceHist collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.ccProductPriceHistDefaultByID) 
      RaiseEvent evtUpdated(pProductPriceHist) 
      Return False 
    End If 
  End Function 
  Private Sub SaveSizes() 
    ' Save column state data  
    ' including order, column width and whether or not the column is visible  
    For Each pCol As DataGridViewColumn In dgvProductPriceHist.Columns 
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
    If _ProductPriceHistCol.Count <= 1 Then 
      _Summarized = False 
      Exit Sub 
    End If 
 
    Dim pProductID As Long 
    Dim pBaseCost As Decimal 
    Dim pSellingPrice As Decimal 
    Dim pMinQuantity As Integer 
    Dim pDiscountPercent As Decimal 
    Dim pOriginalPriceID As Long 
    For Each pExistingRow As clsProductPriceHist In _ProductPriceHistCol 
      If _SummaryOverFlow.IndexOf("#ProductID#") < 0 Then 
        Try 
          pProductID += pExistingRow.ProductID 
        Catch ex As System.OverflowException 
          pProductID = -99999999 
          _SummaryOverFlow &= "ProductID#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#BaseCost#") < 0 Then 
        Try 
          pBaseCost += pExistingRow.BaseCost 
        Catch ex As System.OverflowException 
          pBaseCost = -99999999 
          _SummaryOverFlow &= "BaseCost#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#SellingPrice#") < 0 Then 
        Try 
          pSellingPrice += pExistingRow.SellingPrice 
        Catch ex As System.OverflowException 
          pSellingPrice = -99999999 
          _SummaryOverFlow &= "SellingPrice#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#MinQuantity#") < 0 Then 
        Try 
          pMinQuantity += pExistingRow.MinQuantity 
        Catch ex As System.OverflowException 
          pMinQuantity = -99999999 
          _SummaryOverFlow &= "MinQuantity#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#DiscountPercent#") < 0 Then 
        Try 
          pDiscountPercent += pExistingRow.DiscountPercent 
        Catch ex As System.OverflowException 
          pDiscountPercent = -99999999 
          _SummaryOverFlow &= "DiscountPercent#" 
        End Try 
      End If 
      If _SummaryOverFlow.IndexOf("#OriginalPriceID#") < 0 Then 
        Try 
          pOriginalPriceID += pExistingRow.OriginalPriceID 
        Catch ex As System.OverflowException 
          pOriginalPriceID = -99999999 
          _SummaryOverFlow &= "OriginalPriceID#" 
        End Try 
      End If 
    Next 
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = clsProductPriceHist.enmSummarizeableProperty.ProductID) = clsProductPriceHist.enmSummarizeableProperty.ProductID Then pProductID = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = clsProductPriceHist.enmSummarizeableProperty.BaseCost) = clsProductPriceHist.enmSummarizeableProperty.BaseCost Then pBaseCost = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = clsProductPriceHist.enmSummarizeableProperty.SellingPrice) = clsProductPriceHist.enmSummarizeableProperty.SellingPrice Then pSellingPrice = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = clsProductPriceHist.enmSummarizeableProperty.MinQuantity) = clsProductPriceHist.enmSummarizeableProperty.MinQuantity Then pMinQuantity = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = clsProductPriceHist.enmSummarizeableProperty.DiscountPercent) = clsProductPriceHist.enmSummarizeableProperty.DiscountPercent Then pDiscountPercent = 0
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = clsProductPriceHist.enmSummarizeableProperty.OriginalPriceID) = clsProductPriceHist.enmSummarizeableProperty.OriginalPriceID Then pOriginalPriceID = 0
    Dim pSummaryRow As New clsProductPriceHist( _ 
        vID:=0 _ 
      , vProductID:=pProductID _ 
      , vCustomerType:=clsEnums.enmCustomerType.UD _ 
      , vCustomerTypeText:="" _ 
      , vBaseCost:=pBaseCost _ 
      , vSellingPrice:=pSellingPrice _ 
      , vMinQuantity:=pMinQuantity _ 
      , vDiscountPercent:=pDiscountPercent _ 
      , vValidFrom:=Nothing _ 
      , vValidTo:=Nothing _ 
      , vArchivedDate:=Nothing _ 
      , vArchivedReason:="" _ 
      , vOriginalPriceID:=pOriginalPriceID _ 
      , vNotes:="" _ 
      , vAddFieldsHere:="" _ 
      , vTag:="" _ 
      , vDateAdded:=Nothing _ 
      )
    _ProductPriceHistCol.Add(pSummaryRow) 
    _Summarized = True 
  End Sub 
  
  'Reports and Excel 
  Friend Function CreateSpreadSheet() As clsFault  
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name  
    Dim pFault As New clsFault  
    'Dim pExcel As New Tools.ExcelSheet  
    Dim pDateToShow As String = DateTime.Now.ToString("yyMMdd_HHmmss")  
    Dim pRoot As String = $"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles" 
 
    Dim pFileNameAllFields As String = $"{pRoot}\ProductPriceHistCol_{pDateToShow}AllFields.csv" 
    Dim pFileNameFieldsOnGrid As String = $"{pRoot}\ProductPriceHistCol_{pDateToShow}FieldsOnGrid.csv" 
    Dim pFileNameAllFieldsWithIDs As String = $"{pRoot}\ProductPriceHistCol_{pDateToShow}AllFieldsWithIDs.csv" 
    Dim pFileNameAllFieldsXML As String = $"{pRoot}\ProductPriceHistCol_{pDateToShow}AllFields.xml" 
    Dim pFileNameAllFieldsJson As String = $"{pRoot}\ProductPriceHistCol_{pDateToShow}AllFields.json" 
 
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
    For Each pCol As DataGridViewColumn In dgvProductPriceHist.Columns 
      If pCol.Visible = True Then 
        pTmpStrg.Append(",""" & pCol.HeaderText & """") 
      End If 
    Next 
    pCSV.AppendLine(pTmpStrg.ToString.Substring(1)) 
 
    'Now the data  
    Dim i As Integer 
    Dim pStart As Date = Now 
 
    Dim pTruncatedFieldNames As String = "" 
    For Each Row As DataGridViewRow In dgvProductPriceHist.Rows 
      i += 1 
      If _LoadParameters.SummarizeGrid = True Then 
        If Row.Index = dgvProductPriceHist.Rows.Count - 1 Then Exit For 
      End If 
      If i Mod 500 = 0 Then 
        lblStatus.Text = " Writing Row " & i & ". Time Elapsed: " & DateTime.Now.Subtract(pStart).TotalSeconds().ToString("###0") & " sec" : Application.DoEvents() 
      End If 
      pTmpStrg = New System.Text.StringBuilder 
       
      For Each pCol As DataGridViewColumn In dgvProductPriceHist.Columns 
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
        pFault = _ProductPriceHistColFullLength.CreateXML(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsXML, pStrg, False) 
        'json 
        pFault = _ProductPriceHistColFullLength.CreateJSON(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsJson, pStrg, False) 
        'default  
        My.Computer.FileSystem.WriteAllText(pFileNameAllFields, _ProductPriceHistColFullLength.ToCSV, False)  
        'WithIDs  
        'pFault = _ProductPriceHistColFullLength.LoadLookupAndEnumText(_Requester) : If pFault.isOK = False Then Return pFault (already done) 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsWithIDs, _ProductPriceHistColFullLength.ToCSV(True), False) 
      End If  
      'default  
      My.Computer.FileSystem.WriteAllText(pFileNameFieldsOnGrid, pCSV.ToString, False)  
      pFault.SetOK()  
    Catch ex As Exception  
      pFault.LogException(ex, "", "TRGT-ProductPriceHist-090210-1618", _Requester)  
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
      pFault.LogException(ex, "", "TRGT-ProductPriceHist-090210-1618", _Requester)  
    End Try  
  
    If pFault.isOK = False Then Return pFault  
    
    Return pFault  
  End Function  
 
  Private Sub ReportDesign() 
 
    _Report = New vbReport.ReportDocument 
    _Report.AutoDiscover = False 
    Try 
      For Each pCol As DataGridViewColumn In dgvProductPriceHist.Columns 
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
        .SubTitleLeft = "ProductPriceHists" 
        .SubTitleRight = "Rows: " & _ProductPriceHistCol.Count.ToString 
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
        .DataSource = _ProductPriceHistCol 
        .HasSummaryLine = _Summarized 
      End With 
    Catch ex As Exception 
      Dim pFault As New clsFault 
      pFault.LogException(ex, "", "TRGT-ProductPriceHist-090210-2119", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
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
      pFault.LogException(ex, "", "TRGT-ProductPriceHist-090211-0746", _Requester) 
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
      For Each pRow As DataGridViewRow In dgvProductPriceHist.Rows 
        Try : pRow.Visible = True : Catch : End Try 
      Next 
      lblStatus.ForeColor = Color.DarkGreen 
      lblStatus.Text = dgvProductPriceHist.RowCount & " rows" 
      Exit Sub 
    End If 
    ' Hide rows that don't match search text 
    For Each row As DataGridViewRow In dgvProductPriceHist.Rows 
      Dim pVisible As Boolean = False 
      For Each cell As DataGridViewCell In row.Cells 
        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(pSearchText) Then 
          pVisible = True : Exit For 
        End If 
      Next 
      Try 
        Dim pBS As CurrencyManager = CType(Me.BindingContext(bsCtlProductPriceHist), CurrencyManager) 
        row.Visible = pVisible 
      Catch : End Try 
    Next 
    Dim pVisibleCount As Integer = 0 
    For Each row As DataGridViewRow In dgvProductPriceHist.Rows 
      If row.Visible Then pVisibleCount += 1 
    Next 
    lblStatus.ForeColor = Color.DarkBlue 
    lblStatus.Text = pVisibleCount & " of " & dgvProductPriceHist.RowCount & " rows" 
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
      pFault.LogException(ex, "GetOrInitializeGridSettings", "TRGT-ProductPriceHist-120225-1310", _Requester) 
    End Try 
    If pFault.isOK = False Then Return pFault 
 
    Dim pSaveInitial As Boolean = False 
    
    '_GridSettings.Clear() Use for testing 
    If _GridSettings.Count = 0 Then 
      For Each pCol As DataGridViewColumn In dgvProductPriceHist.Columns 
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
      For Each pCol As DataGridViewColumn In dgvProductPriceHist.Columns 
        Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
        If pG.ColumnName = "" Then 
          pG.ColumnDisplayIndex = pCol.DisplayIndex 
          pG.ColumnWidth = ccHelper.ToInteger((dgvProductPriceHist.Width - 30) / dgvProductPriceHist.Columns.Count) 
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
    'colArchivedReason.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colNotes.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    
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
           
          With dgvProductPriceHist.Columns(lGridSetting.ColumnName) 
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
      pFault.LogException(204, ex, "", "TRGT-ProductPriceHist-090120-1502", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
    End Try 
  End Sub 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ID", _Requester) 
    If pStrg <> "" Then colID.HeaderText = pStrg : mnuColVisibleID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ProductID", _Requester) 
    If pStrg <> "" Then colProductID.HeaderText = pStrg : mnuColVisibleProductID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "CustomerType", _Requester) 
    If pStrg <> "" Then colCustomerType.HeaderText = pStrg : mnuColVisibleCustomerType.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "BaseCost", _Requester) 
    If pStrg <> "" Then colBaseCost.HeaderText = pStrg : mnuColVisibleBaseCost.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "SellingPrice", _Requester) 
    If pStrg <> "" Then colSellingPrice.HeaderText = pStrg : mnuColVisibleSellingPrice.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "MinQuantity", _Requester) 
    If pStrg <> "" Then colMinQuantity.HeaderText = pStrg : mnuColVisibleMinQuantity.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "DiscountPercent", _Requester) 
    If pStrg <> "" Then colDiscountPercent.HeaderText = pStrg : mnuColVisibleDiscountPercent.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ValidFrom", _Requester) 
    If pStrg <> "" Then colValidFrom.HeaderText = pStrg : mnuColVisibleValidFrom.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ValidTo", _Requester) 
    If pStrg <> "" Then colValidTo.HeaderText = pStrg : mnuColVisibleValidTo.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ArchivedDate", _Requester) 
    If pStrg <> "" Then colArchivedDate.HeaderText = pStrg : mnuColVisibleArchivedDate.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "ArchivedReason", _Requester) 
    If pStrg <> "" Then colArchivedReason.HeaderText = pStrg : mnuColVisibleArchivedReason.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "OriginalPriceID", _Requester) 
    If pStrg <> "" Then colOriginalPriceID.HeaderText = pStrg : mnuColVisibleOriginalPriceID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "Notes", _Requester) 
    If pStrg <> "" Then colNotes.HeaderText = pStrg : mnuColVisibleNotes.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("ProductPriceHist", "AddFieldsHere", _Requester) 
    If pStrg <> "" Then colAddFieldsHere.HeaderText = pStrg : mnuColVisibleAddFieldsHere.Text = pStrg
 
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
 
  Private Sub mnuColVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColVisibleID.Click, mnuColVisibleProductID.Click, mnuColVisibleCustomerType.Click, mnuColVisibleBaseCost.Click, mnuColVisibleSellingPrice.Click, mnuColVisibleMinQuantity.Click, mnuColVisibleDiscountPercent.Click, mnuColVisibleValidFrom.Click, mnuColVisibleValidTo.Click, mnuColVisibleArchivedDate.Click, mnuColVisibleArchivedReason.Click, mnuColVisibleOriginalPriceID.Click, mnuColVisibleNotes.Click, mnuColVisibleAddFieldsHere.Click
    Cursor = Cursors.WaitCursor 
    Dim pToolStripItem As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem) 
    dgvProductPriceHist.Columns("col" & pToolStripItem.Name.Substring(13)).Visible = pToolStripItem.Checked 
    If _Loading = False Then SaveSizes() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsReset.Click 
    Cursor = Cursors.WaitCursor 
    dgvProductPriceHist.SuspendLayout() 
 
    For Each pCol As DataGridViewColumn In dgvProductPriceHist.Columns 
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
    pNewWidth = ccHelper.ToInteger((dgvProductPriceHist.Width - 30) / pVisibleColumns) 
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
     
    dgvProductPriceHist.ResumeLayout() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsHideMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsHideMost.Click 
 
    _Loading = True 
    'Hide All 
    If mnuColVisibleID.Checked = True Then mnuColVisibleID.PerformClick() 
    If mnuColVisibleProductID.Checked = True Then mnuColVisibleProductID.PerformClick() 
    If mnuColVisibleCustomerType.Checked = True Then mnuColVisibleCustomerType.PerformClick() 
    If mnuColVisibleBaseCost.Checked = True Then mnuColVisibleBaseCost.PerformClick() 
    If mnuColVisibleSellingPrice.Checked = True Then mnuColVisibleSellingPrice.PerformClick() 
    If mnuColVisibleMinQuantity.Checked = True Then mnuColVisibleMinQuantity.PerformClick() 
    If mnuColVisibleDiscountPercent.Checked = True Then mnuColVisibleDiscountPercent.PerformClick() 
    If mnuColVisibleValidFrom.Checked = True Then mnuColVisibleValidFrom.PerformClick() 
    If mnuColVisibleValidTo.Checked = True Then mnuColVisibleValidTo.PerformClick() 
    If mnuColVisibleArchivedDate.Checked = True Then mnuColVisibleArchivedDate.PerformClick() 
    If mnuColVisibleArchivedReason.Checked = True Then mnuColVisibleArchivedReason.PerformClick() 
    If mnuColVisibleOriginalPriceID.Checked = True Then mnuColVisibleOriginalPriceID.PerformClick() 
    If mnuColVisibleNotes.Checked = True Then mnuColVisibleNotes.PerformClick() 
    If mnuColVisibleAddFieldsHere.Checked = True Then mnuColVisibleAddFieldsHere.PerformClick() 
    'Show Defaults 
    If mnuColVisibleProductID.Checked = False Then mnuColVisibleProductID.PerformClick() 
    If mnuColVisibleCustomerType.Checked = False Then mnuColVisibleCustomerType.PerformClick() 
    
    _Loading = False 
    'dgvProductPriceHist.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
  End Sub 
  
  Private Sub dgvProductPriceHist_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvProductPriceHist.CellMouseClick 
    If e.Button = MouseButtons.Right Then 
      Dim pMessageBox As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the ProductPriceHist to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pMessageBox <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pProductPriceHist As clsProductPriceHist = _ProductPriceHistCol(e.RowIndex) 
        If pMessageBox = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pProductPriceHist.ToCSV) 
        Else 
          Clipboard.SetText(pProductPriceHist.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The ProductPriceHist is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvProductPriceHist_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvProductPriceHist.MouseDown 
    '--- Save anchor on normal click (no modifiers) ---
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And (Keys.Shift Or Keys.Control)) = 0 AndAlso dgvProductPriceHist.CurrentRow IsNot Nothing Then 
      _SelectionAnchor = dgvProductPriceHist.CurrentRow.Index 
    End If 
    'This removes on click from the update 
    If dgvProductPriceHist.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
 
    Dim pCell As DataGridView.HitTestInfo = dgvProductPriceHist.HitTest(e.X, e.Y) 
 
    If pCell.Type = DataGridViewHitTestType.Cell Then 
      'Enable edit force it to be current 
      Dim pCurrentCell As DataGridViewCell = Nothing 
      Try 
        pCurrentCell = dgvProductPriceHist(pCell.ColumnIndex, pCell.RowIndex) 
        If pCurrentCell.ReadOnly Then Exit Sub 
        dgvProductPriceHist.CurrentCell = pCurrentCell 
      Catch ex As Exception 
        Exit Sub  
      End Try 
      'make the combobox drop down if it's active 
      If pCurrentCell.GetType().Name.Equals("DataGridViewComboBoxCell", StringComparison.OrdinalIgnoreCase) Then 
        dgvProductPriceHist.BeginEdit(True) 
        DirectCast(dgvProductPriceHist.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvProductPriceHist_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvProductPriceHist.MouseUp 
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And Keys.Shift) = Keys.Shift AndAlso _SelectionAnchor >= 0 Then 
      Dim hit = dgvProductPriceHist.HitTest(e.X, e.Y) 
      If hit.RowIndex >= 0 Then 
        dgvProductPriceHist.MultiSelect = True 
        dgvProductPriceHist.ClearSelection() 
        Dim pFrom As Integer = Math.Min(_SelectionAnchor, hit.RowIndex) 
        Dim pTo As Integer = Math.Max(_SelectionAnchor, hit.RowIndex) 
        For i As Integer = pFrom To pTo 
          dgvProductPriceHist.Rows(i).Selected = True 
        Next 
      End If 
    ElseIf (pModifiers And Keys.Control) = Keys.Control Then 
      dgvProductPriceHist.MultiSelect = True 
    Else 
      dgvProductPriceHist.MultiSelect = False 
    End If 
  End Sub 
 
  Private Sub chkAutoRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoRefresh.CheckedChanged 
    If chkAutoRefresh.Checked Then 
      _PrevSortColumn = Nothing 
 
      Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(colID.Index, -1, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton2, 0, 0, 0, 0)) 
      dgvProductPriceHist_ColumnHeaderMouseClick(Me, pE) 
      Application.DoEvents() 
      dgvProductPriceHist_ColumnHeaderMouseClick(Me, pE) 
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
 
  Private Sub ctlccProductPriceHistCol_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
    'Set the font for the BN 
        If MyFont Is Nothing Then Return 
    BN.Font = New Font(MyFont.Name, MyFont.Size) 
    dgvProductPriceHist.RowTemplate.Height = ccHelper.ToInteger(23 * MyFont.Size / 9) 
  End Sub 
 
  Private Sub ctlccProductPriceHistCol_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    If Me.ParentForm Is Nothing Then Exit Sub 
    Dim pParent As String = Me.ParentForm.Name 
    Dim pResponse As Boolean = Me.Visible 
    Dim pSize As Integer = dgvProductPriceHist.Width 
 
    'now set sizes if needed 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlccProductPriceHistCol_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True AndAlso Not Me.Parent.Name.StartsWith("pnl", StringComparison.OrdinalIgnoreCase) Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlccProductPriceHistCol_Leave(sender As Object, e As EventArgs) Handles Me.Leave 
    If _Requester Is Nothing Then Return 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    DoCeaseEdit() 
  End Sub 
  'Track open detail windows to prevent duplicates 
  Private Shared _openDetailWindows As New Dictionary(Of String, Form)() 
 
  'Context menu - right-click: add to selection if not already selected, otherwise keep multi-selection 
  Private Sub dgvProductPriceHist_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvProductPriceHist.CellMouseDown 
    ReleaseStuckModifierKeys() 'Fix sticky SHIFT/CTRL before selection changes 
    If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then 
      If Not dgvProductPriceHist.Rows(e.RowIndex).Selected Then 
        dgvProductPriceHist.ClearSelection() 
        dgvProductPriceHist.Rows(e.RowIndex).Selected = True 
      End If 
    End If 
  End Sub 
 
  'Context menu - Opening: adjust items based on single/multi selection 
  Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening 
    Dim pCount As Integer = dgvProductPriceHist.SelectedRows.Count 
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
    If dgvProductPriceHist.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvProductPriceHist.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _ProductPriceHistCol.Count Then Exit Sub 
    Dim pProductPriceHist As clsProductPriceHist = _ProductPriceHistCol(pRowIndex) 
    Dim pTitle As String = "ProductPriceHist #" & pProductPriceHist.ID.ToString() 
    Dim pKey As String = "ProductPriceHist_" & pProductPriceHist.ID.ToString() 
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
    Dim pCtlName As String = "ctlccProductPriceHist" 
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
    Dim pFault As clsFault = CType(pLoad.Invoke(pControl, New Object() {pProductPriceHist, _Requester}), clsFault) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    'Return to Tab click handler - sends entity to a new tab in frmMain 
    Dim pEntityRef As Object = pProductPriceHist 
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
    If dgvProductPriceHist.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvProductPriceHist.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _ProductPriceHistCol.Count Then Exit Sub 
    Dim pProductPriceHist As clsProductPriceHist = _ProductPriceHistCol(pRowIndex) 
    Dim pFrmMain As frmMain = Nothing 
    For Each pForm As Form In Application.OpenForms 
      If TypeOf pForm Is frmMain Then 
        pFrmMain = CType(pForm, frmMain) 
        Exit For 
      End If 
    Next 
    If pFrmMain Is Nothing Then Exit Sub 
    'Check if already open in a window - if so, bring to front instead 
    Dim pWinKey As String = "ProductPriceHist_" & pProductPriceHist.ID.ToString() 
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
    Dim pTabTitle As String = "ProductPriceHist #" & pProductPriceHist.ID.ToString() 
    Dim pFault As clsFault = pFrmMain.OpenEntityInNewTab("ctlccProductPriceHist", pProductPriceHist, _Requester, pTabTitle) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Context menu - Copy ID (supports multi-select) 
  Private Sub tsmiCopyID_Click(sender As Object, e As EventArgs) Handles tsmiCopyID.Click 
    If dgvProductPriceHist.SelectedRows.Count = 0 Then Exit Sub 
    Dim pIDs As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvProductPriceHist.SelectedRows 
      If pSelectedRow.Index >= 0 AndAlso pSelectedRow.Index < _ProductPriceHistCol.Count Then 
        Dim pProductPriceHist As clsProductPriceHist = _ProductPriceHistCol(pSelectedRow.Index) 
        If pIDs.Length > 0 Then pIDs.Append(", ") 
        pIDs.Append(pProductPriceHist.ID.ToString()) 
      End If 
    Next 
    If pIDs.Length > 0 Then 
      Clipboard.SetText(pIDs.ToString()) 
      Dim pCount As Integer = dgvProductPriceHist.SelectedRows.Count 
      ShowToast(If(pCount = 1, "ID copied: " & pIDs.ToString(), pCount.ToString() & " IDs copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows (supports multi-select, values only) 
  Private Sub tsmiCopyRow_Click(sender As Object, e As EventArgs) Handles tsmiCopyRow.Click 
    If dgvProductPriceHist.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvProductPriceHist.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvProductPriceHist.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row copied", pCount.ToString() & " rows copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows with Headers (supports multi-select) 
  Private Sub tsmiCopyRowHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopyRowHeaders.Click 
    If dgvProductPriceHist.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers from first row 
    Dim pFirstRow As DataGridViewRow = dgvProductPriceHist.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add all selected rows 
    For Each pSelectedRow As DataGridViewRow In dgvProductPriceHist.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvProductPriceHist.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row with headers copied", pCount.ToString() & " rows with headers copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy for Excel (with headers, VARCHAR fields wrapped in ="value" to preserve leading zeros) 
  Private Sub tsmiCopyExcel_Click(sender As Object, e As EventArgs) Handles tsmiCopyExcel.Click 
    If dgvProductPriceHist.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers 
    Dim pFirstRow As DataGridViewRow = dgvProductPriceHist.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add rows with Excel-safe formatting for text columns 
    For Each pSelectedRow As DataGridViewRow In dgvProductPriceHist.SelectedRows 
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
      Dim pCount As Integer = dgvProductPriceHist.SelectedRows.Count 
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
