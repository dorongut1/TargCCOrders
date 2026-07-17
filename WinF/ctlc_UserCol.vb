Public Class ctlc_UserCol
 
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
 
  Public Event evtBeforeUpdate(ByVal vUser As csUser, ByRef rCancel As Boolean) 
  Public Event evtUpdated(ByVal vUser As csUser) 
  Private Event evtBeforeDelete(ByVal vUser As csUser, ByRef rCancel As Nullable(Of Boolean)) 
  
  Public Event evtRowClicked(ByVal vUser As csUser) 
  Public Event evtRowDoubleClicked(ByVal vUser As csUser, ByRef rHandled As Boolean) 
  Public Event evtUnChosen() 
 
  Public Event evtOverrideLoadCbo(ByVal vParentName As csUser.enmParentProperty, ByRef rComboListTypeToLoad As clsEnums.enmComboListType, ByRef rParentID As Long, ByRef rComboList As clsComboList, ByRef rPrompt As String) 
  
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
    Public Property DoNotSummarizeProperties As List(Of csUser.enmSummarizeableProperty) 
    Public Property SpreadsheetShowAllFields As Nullable(Of Boolean) 
    Public Property GridTitle As String 
    Public Property ReportTitle As String 
    Public Property [ReadOnly] As Boolean 
    Public Property CbosDoNotLoad As List(Of csUser.enmParentProperty) 
    Public Property ColumnsReadOnly As List(Of csUser.enmProperty) 
    Public Property ColumnsHide As List(Of csUser.enmProperty) 
    Public Property ColumnsFormat As Dictionary(Of csUser.enmProperty, String) 
    Public Property ColumnsOrdinalPosition As Dictionary(Of csUser.enmProperty, Integer) 
    Public Property ColumnsAlignment As Dictionary(Of csUser.enmProperty, DataGridViewContentAlignment) 
    Public Property ColumnsHeaderText As Dictionary(Of csUser.enmProperty, String) 
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
      _DoNotSummarizeProperties = New List(Of csUser.enmSummarizeableProperty) 
      _SpreadsheetShowAllFields = Nothing 
      _GridTitle = "" 
      _ReportTitle = "" 
      _ReadOnly = False 
      _CbosDoNotLoad = New List(Of csUser.enmParentProperty) 
      _ColumnsReadOnly = New List(Of csUser.enmProperty) 
      _ColumnsHide = New List(Of csUser.enmProperty) 
      _ColumnsFormat = New Dictionary(Of csUser.enmProperty, String) 
      _ColumnsOrdinalPosition = New Dictionary(Of csUser.enmProperty, Integer) 
      _ColumnsAlignment = New Dictionary(Of csUser.enmProperty, DataGridViewContentAlignment) 
      _ColumnsHeaderText = New Dictionary(Of csUser.enmProperty, String) 
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
 
  Private WithEvents _UserCol As csUserCol
  Private WithEvents _UserColFullLength As csUserCol
 
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
  Public ReadOnly Property [SelectedUser]() As csUser 
    Get 
      If dgvUser.SelectedRows.Count = 0 OrElse _Loading = True Then Return Nothing 
      Dim RowIndex As Integer = dgvUser.SelectedRows(0).Cells(0).RowIndex 
      If RowIndex < 0 Then Return Nothing 
      If _Summarized = True AndAlso RowIndex = dgvUser.Rows.Count - 1 Then dgvUser.ClearSelection() : RaiseEvent evtUnChosen() : Return Nothing 
      Return _UserCol(RowIndex) 
    End Get 
  End Property 
  
  Public ReadOnly Property [UserCol]() As csUserCol 
    Get 
      Return _UserCol 
    End Get 
  End Property 
 
  Public Function LoadControl(ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    Dim pUserCol As New csUserCol(clsEnums.enmLoadParent.TextOnly) 
    Dim pFault As clsFault 
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    pFault = pUserCol.Fill(_Requester) 
    If pFault.isOK = False Then Return pFault 
 
    pFault = LoadControl(pUserCol)
    Return pFault 
  End Function 
 
  Public Function LoadControl(ByVal vUniqueCode As Object, ByVal vParentObjectType As String, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
    vRequester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pUserCol As New csUserCol(clsEnums.enmLoadParent.EntireObject) 
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
        Case "Role" 
          pFault = pUserCol.FillByRoleID(ccHelper.ToLong(vUniqueCode), _Requester) 
        Case Else 
          Throw New Exception("Invalid vParentObjectType '" & vParentObjectType & "' received ") 
      End Select 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    pFault = LoadControl(pUserCol) 
    Return pFault 
  End Function 
 
  Public Function LoadControl(vUserCol As csUserCol, ByVal vLoadParameters As clsLoadParameters, ByVal vRequester As clsRequester) As clsFault
 
    _Requester = vRequester 
    _LoadParameters = vLoadParameters 
 
    Return LoadControl(vUserCol) 
  End Function
  
  Private Function LoadControl(vUserCol As csUserCol) As clsFault
    Dim pFault As New clsFault
 
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
    'Use evtBeforeLoad to set or remove the list type, if you don't want the default 
    'If you want to force recalculation, then set remove the combolist from the cache using ClearComboList  
    RaiseEvent evtBeforeLoad() 
 
    LoadLocalizedText() 
 
    'keep safe in case 
    Dim pColumnsHides As List(Of csUser.enmProperty) = Nothing 
    If _LoadParameters.IsSumFillOnTheFly Then 
      pColumnsHides = New List(Of csUser.enmProperty) 
      pColumnsHides.AddRange(_LoadParameters.ColumnsHide) 
    End If 
 
    'Force blg and clc fields to read-only 
    
    'Check for ReadOnly columns 
    For Each l In _LoadParameters.ColumnsReadOnly 
      If l = csUser.enmProperty.ID Then colID.ReadOnly = True 
      If l = csUser.enmProperty.UserName Then colUserName.ReadOnly = True 
      If l = csUser.enmProperty.LastName Then colLastName.ReadOnly = True 
      If l = csUser.enmProperty.FirstName Then colFirstName.ReadOnly = True 
      If l = csUser.enmProperty.FullName Then colFullName.ReadOnly = True 
      If l = csUser.enmProperty.NationalIDNo Then colNationalIDNo.ReadOnly = True 
      If l = csUser.enmProperty.Address Then colAddress.ReadOnly = True 
      If l = csUser.enmProperty.City Then colCity.ReadOnly = True 
      If l = csUser.enmProperty.ProvinceState Then colProvinceState.ReadOnly = True 
      If l = csUser.enmProperty.PostalCode Then colPostalCode.ReadOnly = True 
      If l = csUser.enmProperty.Country Then colCountry.ReadOnly = True 
      If l = csUser.enmProperty.PhoneNumber Then colPhoneNumber.ReadOnly = True 
      If l = csUser.enmProperty.Email Then colEmail.ReadOnly = True 
      If l = csUser.enmProperty.PasswordHashed Then colPasswordHashed.ReadOnly = True 
      If l = csUser.enmProperty.DatePasswordChanged Then colDatePasswordChanged.ReadOnly = True 
      If l = csUser.enmProperty.Type Then colType.ReadOnly = True 
      If l = csUser.enmProperty.IDinType Then colIDinType.ReadOnly = True 
      If l = csUser.enmProperty.RequiresComputerIdentification Then colRequiresComputerIdentification.ReadOnly = True 
      If l = csUser.enmProperty.EnableSimultaneousLogins Then colEnableSimultaneousLogins.ReadOnly = True 
      If l = csUser.enmProperty.DateActivated Then colDateActivated.ReadOnly = True 
      If l = csUser.enmProperty.IsDisabled Then colIsDisabled.ReadOnly = True 
      If l = csUser.enmProperty.ExpiryDate Then colExpiryDate.ReadOnly = True 
      If l = csUser.enmProperty.Comments Then colComments.ReadOnly = True 
      If l = csUser.enmProperty.LastPasswords Then colLastPasswords.ReadOnly = True 
      If l = csUser.enmProperty.Applications Then colApplications.ReadOnly = True 
      If l = csUser.enmProperty.Language Then colLanguage.ReadOnly = True 
      If l = csUser.enmProperty.IsLockedOut Then colIsLockedOut.ReadOnly = True 
      If l = csUser.enmProperty.Role Then colRole.ReadOnly = True 
      If l = csUser.enmProperty.AuthenticationMethod Then colAuthenticationMethod.ReadOnly = True 
      If l = csUser.enmProperty.RequiresFixedIP Then colRequiresFixedIP.ReadOnly = True 
      If l = csUser.enmProperty.MessagingMode Then colMessagingMode.ReadOnly = True 
      If l = csUser.enmProperty.LoggedInIP Then colLoggedInIP.ReadOnly = True 
      If l = csUser.enmProperty.ApprovalCodeHashed Then colApprovalCodeHashed.ReadOnly = True 
      If l = csUser.enmProperty.ApprovalFunctionName Then colApprovalFunctionName.ReadOnly = True 
      If l = csUser.enmProperty.ApprovalTime Then colApprovalTime.ReadOnly = True 
      If l = csUser.enmProperty.LastSuccessfulLogin Then colLastSuccessfulLogin.ReadOnly = True 
      If l = csUser.enmProperty.PasswordNeverExpires Then colPasswordNeverExpires.ReadOnly = True 
      If l = csUser.enmProperty.SecurityQuestion1 Then colSecurityQuestion1.ReadOnly = True 
      If l = csUser.enmProperty.SecurityQuestion1Response Then colSecurityQuestion1Response.ReadOnly = True 
      If l = csUser.enmProperty.SecurityQuestion2 Then colSecurityQuestion2.ReadOnly = True 
      If l = csUser.enmProperty.SecurityQuestion2Response Then colSecurityQuestion2Response.ReadOnly = True 
      If l = csUser.enmProperty.SecurityQuestion3 Then colSecurityQuestion3.ReadOnly = True 
      If l = csUser.enmProperty.SecurityQuestion3Response Then colSecurityQuestion3Response.ReadOnly = True 
      If l = csUser.enmProperty.PIN Then colPIN.ReadOnly = True 
    Next 
 
    For Each l In _LoadParameters.ColumnsHide 
      'Parents only 
      Dim pParentProperty As csUser.enmParentProperty = csUser.enmParentProperty.UD 
      Dim pSuccess As Boolean = [Enum].TryParse(Of csUser.enmParentProperty)(l.ToString(), ignoreCase:=False, pParentProperty) 
      If pSuccess = False Then Continue For 
      If Not _LoadParameters.CbosDoNotLoad.Contains(pParentProperty) Then 
        _LoadParameters.CbosDoNotLoad.Add(pParentProperty) 
      End If 
    Next 
 
    If _LoadParameters.IsSumFillOnTheFly Then 
      'Use what we just save instead 
      _LoadParameters.ColumnsHide = pColumnsHides 
    End If 
 
    dgvUser.DoubleBuffered(True) 
 
    pFault = vUserCol.LoadLookupAndEnumText(_Requester) : If Not pFault.isOK Then Return pFault 
    
    'Now transfer to local collection 
    _UserColFullLength = vUserCol.Clone() 
 
    'Truncate the strings 
    _UserCol = vUserCol 
    If _LoadParameters.TruncateStrings Then 
      _UserCol.TruncateStrings() 
    Else 
      dgvUser.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
      dgvUser.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders 
    End If 
 
    ' If you switch between ReadOnly and not Readonly, it causes problems
    Static sReadOnlyHandled As Boolean = False 
    If sReadOnlyHandled = False Then 
      If _LoadParameters.ReadOnly = True Then 
        colRole.Name = colRole.Name & "zzzz" 
        colRoleText.Name = colRole.Name.Replace("zzzz", "") 
        If colRole.DataGridView IsNot Nothing Then dgvUser.Columns.Remove(colRole) 
      Else 
        If colRole.ReadOnly = False Then 
          If colRoleText.DataGridView IsNot Nothing Then dgvUser.Columns.Remove(colRoleText) 
        Else 
          colRole.Name = colRole.Name & "zzzz" 
          colRoleText.Name = colRole.Name.Replace("zzzz", "") 
          If colRole.DataGridView IsNot Nothing Then dgvUser.Columns.Remove(colRole) 
          If Not _LoadParameters.CbosDoNotLoad.Contains(csUser.enmParentProperty.Role) Then 
            _LoadParameters.CbosDoNotLoad.Add(csUser.enmParentProperty.Role) 
          End If 
        End If 
      End If 
      sReadOnlyHandled = True 
    End If 
    If _LoadParameters.ReadOnly = False Then 
      'Load ComboListCache 
      If Not _LoadParameters.CbosDoNotLoad.Contains(csUser.enmParentProperty.Role) Then 
        MyCache.SetLevel(clsEnums.enmComboListType.c_RoleDefaultByID, Cache.enmLevel.Previous) 
      End If 
    End If 
 
    _SummaryOverFlow = "#" 
 
    Dim pHiddenColumnNames As New List(Of String) 
    For Each l In _LoadParameters.ColumnsHide 
      pHiddenColumnNames.Add("col" & l.ToString()) 
    Next 
    For Each lCol As DataGridViewColumn In dgvUser.Columns 
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
    For Each p As csUser.enmProperty In _LoadParameters.ColumnsHide 
      Dim pGridSetting As clsGridSetting = _GridSettings.FindByColumnName("col" & p.ToString()) 
      'HideColumn(p.ToString) 
      pGridSetting.ColumnRemoved = True 
    Next 
     
    'Set Header Text 
    For Each pD In _LoadParameters.ColumnsHeaderText 
      dgvUser.Columns("col" & pD.Key.ToString).HeaderText = pD.Value 
    Next 
 
    'Format Columns 
    For Each pD In _LoadParameters.ColumnsFormat 
      dgvUser.Columns("col" & pD.Key.ToString).DefaultCellStyle.Format = pD.Value 
    Next 
 
    'ordinal position 
    For Each pD In _LoadParameters.ColumnsOrdinalPosition 
      dgvUser.Columns("col" & pD.Key.ToString).DisplayIndex = pD.Value 
    Next 
    _GridSettings.Update(Me, _Requester)
    
    'Align Columns 
    For Each pD In _LoadParameters.ColumnsAlignment 
      dgvUser.Columns("col" & pD.Key.ToString).DefaultCellStyle.Alignment = pD.Value 
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
 
    dgvUser.ClearSelection()
    bsCtlUser.DataSource = Nothing 
    
    pFault = LoadSupportingCombos() : If pFault.isOK = False Then Return pFault 
 
    lblGrid.Text = _LoadParameters.GridTitle 
    If lblGrid.Text = "" Then 
      'Assume chkAutoRefresh is not used either. (may have to add it to LoadParameters) 
      pnlHeader.Visible = False 
    End If 
    Try
      LoadGrid()
    Catch ex As Exception
      Return pFault.LogException(ex, "LoadGrid", "TRGT-User-090124-2345", _Requester) 
    End Try
    
    RaiseEvent evtLoaded() 
    
    'Show row count in status label 
    lblStatus.ForeColor = Color.DarkGreen 
    lblStatus.Text = dgvUser.RowCount & " rows" 
    
    'now do the default sorts 
    If _SortList IsNot Nothing Then 
      _AutoSorting = True 
      _PrevSortColumn = Nothing 
      For Each i In _SortList 
        Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(i, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0)) 
        dgvUser_ColumnHeaderMouseClick(Me, pE) 
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
    If dgvUser.SelectedRows.Count > 0 Then 
      pRowIndex = dgvUser.SelectedRows(0).Cells(0).RowIndex 
    Else 
      If dgvUser.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
        pRowIndex = dgvUser.CurrentCellAddress.Y 
      End If 
    End If 
 
    If _LoadParameters.SummarizeGrid = True Then Summarize() 
 
    _Loading = True 
 
    bsCtlUser.DataSource = Nothing 
    bsCtlUser.DataSource = _UserCol
    
    dgvUser.ClearSelection() 
    
    RaiseEvent evtUnChosen()
    
    SetUpBNButtons(False)
    'set columns 
    LoadColumns() 
 
    'Load buttons 
    For Each p As ToolStripMenuItem In btnColumns.DropDownItems 
      If p Is mnuColsReset OrElse p Is mnuColsHideMost Then Continue For 
      Dim pMenuItemProprty As String = p.Name.Substring(13) 
      p.Checked = dgvUser.Columns("col" & pMenuItemProprty).Visible 
    Next 
 
    If pRowIndex >= 0 Then 
      If _Summarized = True Then 
        If pRowIndex <= _UserCol.Count - 2 Then 
          dgvUser.Rows(pRowIndex).Selected = True 
        End If 
      Else 
        If pRowIndex <= _UserCol.Count - 1 Then 
          dgvUser.Rows(pRowIndex).Selected = True 
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
    'EnumType
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.Type, Nothing, Nothing, pEnumCol, pPrompt) 
    If pEnumCol Is Nothing Then 
      pEnumCol = New clsComboList 
      pFault = pEnumCol.FillEnums(clsEnums.enmEnum.UserIdentityType, _Requester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      pFault.SetOK() 
    End If 
    pEnumCol.Remove(pEnumCol.FindByKey(clsEnums.enmUserIdentityType.UD)) 
    pEnumCol.SortByText() 
    If pPrompt = "" Then 
      pPrompt = pChoose 
    End If 
    pEnumCol.AddToTop(clsEnums.enmUserIdentityType.UD, pPrompt) 
    bsType.DataSource = pEnumCol 
    colType.Tag = pPrompt 

    'EnumLanguage
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.Language, Nothing, Nothing, pEnumCol, pPrompt) 
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

    'Role
    If _LoadParameters.ReadOnly = False AndAlso _LoadParameters.CbosDoNotLoad.Find(Function(p) p = csUser.enmParentProperty.Role) = csUser.enmParentProperty.UD Then 
      'enable using an external list if needed 
      pComboList = Nothing 
      pPrompt = "" 
      Dim pComboListTypeToLoad As clsEnums.enmComboListType = clsEnums.enmComboListType.c_RoleDefaultByID 
      Dim pParentID As Long = 0 
      RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.Role, pComboListTypeToLoad, pParentID, pComboList, pPrompt) 
      If pComboList Is Nothing Then 
        pFault = MyCache.GetComboList(pComboListTypeToLoad, pComboList) : If Not pFault.isOK() Then Return pFault 
        If MyCache.GetLevel(pComboListTypeToLoad) = Cache.enmLevel.AlwaysPageFromServer Then 
          Return pFault.LogFreeTextFault($"In {Me.Name}, {pComboListTypeToLoad.FastToString()} is defined as AlwaysPageFromServer. Either change it to AlwaysCache in evtBeforeLoad, make the column read-only, or make this grid read-only", "", "TRGT-UserCol-200806-1015", _Requester) 
        End If 
      End If 
      pComboList = pComboList.Clone() 
      If pPrompt = "" Then pPrompt = pChoose 
        pComboList.AddToTop(ccHelper.ToLong(0), pPrompt) 
      bsRole.DataSource = pComboList 
      colRole.Tag = pPrompt 
    End If 

    'EnumAuthenticationMethod
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.AuthenticationMethod, Nothing, Nothing, pEnumCol, pPrompt) 
    If pEnumCol Is Nothing Then 
      pEnumCol = New clsComboList 
      pFault = pEnumCol.FillEnums(clsEnums.enmEnum.AuthenticationMethod, _Requester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      pFault.SetOK() 
    End If 
    pEnumCol.Remove(pEnumCol.FindByKey(clsEnums.enmAuthenticationMethod.UD)) 
    pEnumCol.SortByText() 
    If pPrompt = "" Then 
      pPrompt = pChoose 
    End If 
    pEnumCol.AddToTop(clsEnums.enmAuthenticationMethod.UD, pPrompt) 
    bsAuthenticationMethod.DataSource = pEnumCol 
    colAuthenticationMethod.Tag = pPrompt 

    'EnumMessagingMode
    pPrompt = "" 
    pEnumCol = Nothing 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.MessagingMode, Nothing, Nothing, pEnumCol, pPrompt) 
    If pEnumCol Is Nothing Then 
      pEnumCol = New clsComboList 
      pFault = pEnumCol.FillEnums(clsEnums.enmEnum.MessagingMode, _Requester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      pFault.SetOK() 
    End If 
    pEnumCol.Remove(pEnumCol.FindByKey(clsEnums.enmMessagingMode.UD)) 
    pEnumCol.SortByText() 
    If pPrompt = "" Then 
      pPrompt = pChoose 
    End If 
    pEnumCol.AddToTop(clsEnums.enmMessagingMode.UD, pPrompt) 
    bsMessagingMode.DataSource = pEnumCol 
    colMessagingMode.Tag = pPrompt 

    'SecurityQuestion1
    'enable using an external list if needed 
    pTestLookupCol = Nothing 
    pPrompt = pChoose 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.SecurityQuestion1, Nothing, Nothing, pTestLookupCol, pPrompt) 
    If pTestLookupCol Is Nothing Then 
      pComboList = New clsComboList() 
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester) : If pFault.isOK = False Then Return pFault 
      pPrompt = pChoose 
      pComboList.AddToTop("", pPrompt) 
    Else 
      pComboList = pTestLookupCol 
    End If 
    bsSecurityQuestion1.DataSource = pComboList 
    colSecurityQuestion1.Tag = pPrompt 

    'SecurityQuestion2
    'enable using an external list if needed 
    pTestLookupCol = Nothing 
    pPrompt = pChoose 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.SecurityQuestion2, Nothing, Nothing, pTestLookupCol, pPrompt) 
    If pTestLookupCol Is Nothing Then 
      pComboList = New clsComboList() 
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester) : If pFault.isOK = False Then Return pFault 
      pPrompt = pChoose 
      pComboList.AddToTop("", pPrompt) 
    Else 
      pComboList = pTestLookupCol 
    End If 
    bsSecurityQuestion2.DataSource = pComboList 
    colSecurityQuestion2.Tag = pPrompt 

    'SecurityQuestion3
    'enable using an external list if needed 
    pTestLookupCol = Nothing 
    pPrompt = pChoose 
    RaiseEvent evtOverrideLoadCbo(csUser.enmParentProperty.SecurityQuestion3, Nothing, Nothing, pTestLookupCol, pPrompt) 
    If pTestLookupCol Is Nothing Then 
      pComboList = New clsComboList() 
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester) : If pFault.isOK = False Then Return pFault 
      pPrompt = pChoose 
      pComboList.AddToTop("", pPrompt) 
    Else 
      pComboList = pTestLookupCol 
    End If 
    bsSecurityQuestion3.DataSource = pComboList 
    colSecurityQuestion3.Tag = pPrompt 

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
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) = True Then btnEdit.Visible = Not (vInEdit) Else btnEdit.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) = True AndAlso _LoadParameters.ImportButtonHide = False Then btnImport.Visible = vInEdit Else btnImport.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserUpdate, _Requester) = True Then btnAdd.Visible = vInEdit Else btnAdd.Visible = False 
      If ccSecurity.GetPermissionForUI(clsEnums.enmProcess.tbl_c_UserDelete, _Requester) = True Then btnDelete.Visible = vInEdit Else btnDelete.Visible = False 
      btnCeaseEdit.Visible = vInEdit 
      If _LoadParameters.AddEditDeleteButtonsHide = True Then 
        btnAdd.Visible = False 
        btnDelete.Visible = False 
      End If 
    End If 
    If vInEdit = True AndAlso _LoadParameters.AddEditDeleteButtonsHide = False Then 
      colID.ReadOnly = True 
      colFullName.ReadOnly = True 
      colPasswordHashed.ReadOnly = True 
      colDatePasswordChanged.ReadOnly = True 
      colDateActivated.ReadOnly = True 
      colComments.ReadOnly = True 
      colLastPasswords.ReadOnly = True 
      colApplications.ReadOnly = True 
      colLoggedInIP.ReadOnly = True 
      colLastSuccessfulLogin.ReadOnly = True 
      colSecurityQuestion1Response.ReadOnly = True 
      colSecurityQuestion2Response.ReadOnly = True 
      colSecurityQuestion3Response.ReadOnly = True 
      colPIN.ReadOnly = True 
      dgvUser.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 
      dgvUser.SelectionMode = DataGridViewSelectionMode.CellSelect 
      _DVGDirty = False 
    Else 
      dgvUser.EditMode = DataGridViewEditMode.EditProgrammatically 
      dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
      dgvUser.AllowUserToDeleteRows = False 
      dgvUser.AllowUserToAddRows = False 
      'Don't automatically set the 1st one If dgvUser.Rows.Count > 0 Then 
      '  Dim pCurrentRow As Integer 
      '  pCurrentRow = dgvUser.CurrentRow.Index 
      '  dgvUser.CurrentCell = dgvUser.Rows(pCurrentRow).Cells(0) 
      '  dgvUser.Rows(pCurrentRow).Selected = True 
      'End If 
    End If 
    If vInEdit = True Then 
      lblEditMode.Text = "Edit Mode" 
      tssReports.Visible = True 
      btnSpreadsheet.Enabled = False 
      btnReport.Enabled = False 
    Else 
      If _UserCol.Count = 0 Then 
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
    dgvUser.Refresh() 
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
    pFieldList.Append("UserName, ") 
    pFieldList.Append("LastName, ") 
    pFieldList.Append("FirstName, ") 
    pFieldList.Append("NationalIDNo, ") 
    pFieldList.Append("Address, ") 
    pFieldList.Append("City, ") 
    pFieldList.Append("ProvinceState, ") 
    pFieldList.Append("PostalCode, ") 
    pFieldList.Append("Country, ") 
    pFieldList.Append("PhoneNumber, ") 
    pFieldList.Append("Email, ") 
    pFieldList.Append("Type (DB Code), ") 
    pFieldList.Append("IDinType, ") 
    pFieldList.Append("RequiresComputerIdentification, ") 
    pFieldList.Append("EnableSimultaneousLogins, ") 
    pFieldList.Append("IsDisabled, ") 
    pFieldList.Append("ExpiryDate, ") 
    pFieldList.Append("Language (DB Code), ") 
    pFieldList.Append("IsLockedOut, ") 
    pFieldList.Append("RoleID (DB Code), ") 
    pFieldList.Append("AuthenticationMethod (DB Code), ") 
    pFieldList.Append("RequiresFixedIP, ") 
    pFieldList.Append("MessagingMode (DB Code), ") 
    pFieldList.Append("ApprovalCodeHashed, ") 
    pFieldList.Append("ApprovalFunctionName, ") 
    pFieldList.Append("ApprovalTime, ") 
    pFieldList.Append("PasswordNeverExpires, ") 
    pFieldList.Append("SecurityQuestion1Code, ") 
    pFieldList.Append("SecurityQuestion2Code, ") 
    pFieldList.Append("SecurityQuestion3Code, ") 
    
    Dim pNumberOfFields As Integer = 31 
    
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
    Dim pIncomingUsers As New csUserCol(vWithParents:=clsEnums.enmLoadParent.DoNotLoad) 
 
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
          Dim pIncomingUser As New csUser(vWithParents:=clsEnums.enmLoadParent.DoNotLoad) 
          pIncomingUser.Tag = "Row " & pRow.ToString 
          pCurrentRow = pReader.ReadFields() 
          If pRow = 0 Then 
            Continue While 'Header line  
          End If 
 
          If pCurrentRow.Length <> pNumberOfFields Then 
            pErrorFound = True 
            pIncomingUser.Tag &= ": There should be " & pNumberOfFields & " fields, but there are actually " & pCurrentRow.Length & " fields." 
            pIncomingUsers.Add(pIncomingUser) 
            Continue While 
          End If 
 
          Dim pFieldNo As Integer = -1 
 
          If pNoPrimaryKey = False Then 
            Try 
              pFieldNo += 1 
              pFieldName = "ID" 
              pIncomingUser.ID = CType(pCurrentRow(pFieldNo), Long) 
            Catch ex As Exception 
              pErrorFound = True 
              pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
            End Try 
          End If 
 
          Try 
            pFieldNo += 1 
            pFieldName = "UserName" 
            pIncomingUser.UserName = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "LastName" 
            pIncomingUser.LastName = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "FirstName" 
            pIncomingUser.FirstName = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "NationalIDNo" 
            pIncomingUser.NationalIDNo = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "Address" 
            pIncomingUser.Address = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "City" 
            pIncomingUser.City = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ProvinceState" 
            pIncomingUser.ProvinceState = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "PostalCode" 
            pIncomingUser.PostalCode = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "Country" 
            pIncomingUser.Country = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "PhoneNumber" 
            pIncomingUser.PhoneNumber = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "Email" 
            pIncomingUser.Email = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "Type" 
            pIncomingUser.Type = clsEnums.TranslateEnmUserIdentityType(pCurrentRow(pFieldNo)) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "IDinType" 
            pIncomingUser.IDinType = CType(pCurrentRow(pFieldNo), Long) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "RequiresComputerIdentification" 
            pIncomingUser.RequiresComputerIdentification = CType(pCurrentRow(pFieldNo), Boolean) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "EnableSimultaneousLogins" 
            pIncomingUser.EnableSimultaneousLogins = CType(pCurrentRow(pFieldNo), Boolean) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "IsDisabled" 
            pIncomingUser.IsDisabled = CType(pCurrentRow(pFieldNo), Boolean) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ExpiryDate" 
            pIncomingUser.ExpiryDate = CType(pCurrentRow(pFieldNo), Date) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "Language" 
            pIncomingUser.Language = clsEnums.TranslateEnmLanguage(pCurrentRow(pFieldNo)) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "IsLockedOut" 
            pIncomingUser.IsLockedOut = CType(pCurrentRow(pFieldNo), Boolean) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "RoleID" 
            pIncomingUser.RoleID = CType(pCurrentRow(pFieldNo), Long) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "AuthenticationMethod" 
            pIncomingUser.AuthenticationMethod = clsEnums.TranslateEnmAuthenticationMethod(pCurrentRow(pFieldNo)) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "RequiresFixedIP" 
            pIncomingUser.RequiresFixedIP = CType(pCurrentRow(pFieldNo), Boolean) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "MessagingMode" 
            pIncomingUser.MessagingMode = clsEnums.TranslateEnmMessagingMode(pCurrentRow(pFieldNo)) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ApprovalCodeHashed" 
            pIncomingUser.ApprovalCodeHashed = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ApprovalFunctionName" 
            pIncomingUser.ApprovalFunctionName = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "ApprovalTime" 
            pIncomingUser.ApprovalTime = CType(pCurrentRow(pFieldNo), DateTimeOffset) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "PasswordNeverExpires" 
            pIncomingUser.PasswordNeverExpires = CType(pCurrentRow(pFieldNo), Boolean) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "SecurityQuestion1Code" 
            pIncomingUser.SecurityQuestion1Code = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "SecurityQuestion2Code" 
            pIncomingUser.SecurityQuestion2Code = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          Try 
            pFieldNo += 1 
            pFieldName = "SecurityQuestion3Code" 
            pIncomingUser.SecurityQuestion3Code = CType(pCurrentRow(pFieldNo), String) 
          Catch ex As Exception 
            pErrorFound = True 
            pIncomingUser.Tag &= ccHelper.NewLine & "Field: " & pFieldName & "; Value: " & pCurrentRow(pFieldNo) & "; Problem: " & ex.Message 
          End Try 
 
          If pIncomingUser.Tag = "Row " & pRow.ToString Then 
            pIncomingUser.Tag &= ": OK" 
          End If 
 
          pIncomingUsers.Add(pIncomingUser) 
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
          My.Computer.FileSystem.WriteAllText(pFilenameOut, pIncomingUsers.ToCSV, False) 
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
    pFault = pIncomingUsers.UpdateFromCollection(_Requester) 
    If pFault.isOK = False Then 
      ShowFault(pFault, _Requester) 
      Exit Sub 
    End If 
    'Reset the User collection 
    MyCache.ClearComboList(clsEnums.enmComboListType.c_UserDefaultByID) 
 
    Cursor = Cursors.Default 
 
    'Check that there were no problems 
    pErrorFound = False 
    For Each p In pIncomingUsers 
      If p.Tag <> "OK" Then 
        pErrorFound = True 
      End If 
    Next 
    If pErrorFound = True Then 
      Try 
        My.Computer.FileSystem.WriteAllText(pFilenameOut, pIncomingUsers.ToCSV, False) 
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
      _UserCol = pIncomingUsers 
      LoadGrid() 
      frmMessageOrInputBox.ShowMsg("Update Successful! Please click on Refresh to see all the data", frmMessageOrInputBox.enmIconType.Information) 
    End If 
 
  End Sub 
 
  'ExternalButtons 
  Private Sub DoEdit() 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    Dim pCellRow As Integer = -1 
    Dim pCellCol As Integer = -1 
 
 
    If dgvUser.Focused = True AndAlso dgvUser.SelectedRows.Count > 0 Then 
      pCellRow = dgvUser.CurrentCell.RowIndex 
      pCellCol = dgvUser.CurrentCell.ColumnIndex 
    End If 
 
    Try 'in case it's empty 
      dgvUser.CurrentCell = dgvUser.Rows(0).Cells(0) 
      dgvUser.CurrentCell.Selected = True 
    Catch ex As Exception 
    End Try 
 
 
    'remove summary row 
    If _LoadParameters.SummarizeGrid = True AndAlso _UserCol.Count > 0 AndAlso _UserCol(_UserCol.Count - 1).ID = 0 Then 
      _UserCol.RemoveAt(_UserCol.Count - 1) 
      bsCtlUser.DataSource = Nothing 
      bsCtlUser.DataSource = _UserCol 
      _Summarized = False 
    End If 
 
    SetUpBNButtons(True) 
    If pCellRow >= 0 AndAlso pCellCol >= 0 Then 
      dgvUser.Focus() 
      dgvUser.CurrentCell = dgvUser.Rows(pCellRow).Cells(pCellCol) 
      dgvUser.CurrentCell.Selected = True 
    ElseIf _UserCol.Count = 0 Then 
    Else 
      Try 'in case the cell is hidden.... 
        dgvUser.CurrentCell = dgvUser.Rows(0).Cells(0) 
        dgvUser.CurrentCell.Selected = True 
      Catch ex As Exception 
      End Try 
    End If 
  End Sub 
  Private Sub DoAdd() 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True Then Exit Sub 
    bsCtlUser.AddNew() 
 
    'Now choose any needed fields 
    Dim pEntity As csUser 
    pEntity = CType(bsCtlUser.Current, csUser) 
 
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
    
    If dgvUser.CurrentCell Is Nothing Then Return pFault 
    
    If dgvUser.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      Dim pUser As csUser 
      pUser = CType(bsCtlUser.Current, csUser) 
      If pUser Is Nothing Then 
        pFault.LogFreeTextFault("There is no User to delete", "", "TRGT-110303-165408", _Requester) 
        Return pFault 
      End If 
      Dim pOriginalCol As Integer = dgvUser.CurrentCell.ColumnIndex 
      Dim pOriginalRow As Integer = dgvUser.CurrentCell.RowIndex 
      'show row as selected  
      dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect 
      dgvUser.EditMode = DataGridViewEditMode.EditProgrammatically 
      dgvUser.CurrentRow.Selected = True 
      If pUser.ID > 0 Then 
        Dim pRequest As String = "Are you sure you want to delete '" & pUser.FirstName & " " & pUser.LastName & " (" & pUser.UserName & ")" & "'?" 
        Dim pCancel As Nullable(Of Boolean) = Nothing 
        RaiseEvent evtBeforeDelete(pUser, pCancel) 
        If pCancel = True Then 
          Return pFault 
        ElseIf pCancel Is Nothing Then 
          Dim pResponse As frmMessageOrInputBox.enmButtonReturned 
          pResponse = frmMessageOrInputBox.ShowMsg(pRequest, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes") 
          If pResponse = frmMessageOrInputBox.enmButtonReturned.No Then 
            dgvUser.SelectionMode = DataGridViewSelectionMode.CellSelect 
            dgvUser.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 
            dgvUser.Rows(pOriginalRow).Cells(pOriginalCol).Selected = True 
            Return pFault 
          End If 
        End If 
        pFault = pUser.Delete(_Requester) : If pFault.isOK = False Then Return pFault 
      End If 
      bsCtlUser.Remove(bsCtlUser.Current) 
      LoadGrid() 
    End If 
    Return pFault 
  End Function 
  Private Sub DoCeaseEdit() 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True And _DVGDirty = False Then 
      bsCtlUser.DataSource = _UserCol 
    End If 
    If _DVGDirty = True Then 
      RaiseEvent evtTimerTripped() 
      Exit Sub 
    End If 
    Dim pUser As csUser = CType(bsCtlUser.Current, csUser) 
    If pUser IsNot Nothing Then 
      If pUser.ID = 0 Then 
        _IgnoreGridFault = True 
        bsCtlUser.Remove(bsCtlUser.Current) 
        _IgnoreGridFault = False 
      End If 
    End If 
    SetUpBNButtons(False) 
    If _UserCol.Count > 0 AndAlso dgvUser.CurrentCell IsNot Nothing Then 
      For i As Integer = 0 To dgvUser.Columns.Count - 1 
        If dgvUser.Columns(i).Visible Then 
          dgvUser.CurrentCell = dgvUser.Rows(dgvUser.CurrentCell.RowIndex).Cells(i) 
          Exit For 
        End If 
      Next 
      dgvUser.Refresh() 
      dgvUser.Rows(dgvUser.CurrentCell.RowIndex).Selected = True 
    Else 
      dgvUser.Refresh() 
    End If 
  End Sub 
  'Grid RowValidating 
  Private Sub dgvUser_RowValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvUser.RowValidating 
    If _Loading = True OrElse dgvUser.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
    Dim pCancel As Boolean 
    pCancel = UpdateRow() 
    If pCancel = True Then 
      e.Cancel = True 
      dgvUser.CurrentCell = dgvUser.Rows(e.RowIndex).Cells(e.ColumnIndex) 
    End If 
  End Sub 
  'CellFormatting  
  Private Sub dgvUser_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvUser.CellFormatting 
    '_Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    If dgvUser.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2 Then 
      If dgvUser.Columns(e.ColumnIndex).ReadOnly = False Then 
        Exit Sub 
      End If 
    End If 
 
    RaiseEvent evtCellFormatting(sender, e) 
 
    'For ID in XXX 
    If e.ColumnIndex = colType.Index Then 
      _IgnoreGridFault = True 
 
      Dim pType As clsEnums.enmUserIdentityType = clsEnums.TranslateEnmUserIdentityType(e.Value.ToString()) 
 
      Dim pCell As DataGridViewComboBoxCell = CType(dgvUser(colIDinType.Index, e.RowIndex), DataGridViewComboBoxCell) 
      pCell.DisplayMember = "Text" 
      pCell.ValueMember = "KeyLong" 
      Select Case pType 
        Case clsEnums.enmUserIdentityType.Global 
          pCell.DataSource = _Global 
        Case clsEnums.enmUserIdentityType.c_User 
          pCell.DataSource = _Users 
        Case clsEnums.enmUserIdentityType.Customer 
          pCell.DataSource = _Customers 
        Case Else 
          pCell.DataSource = _UD 
      End Select 
      Application.DoEvents() 
      _IgnoreGridFault = False 
    End If 
 
    'For ID in XXX 
 
    ' Sample code evtCellFormatting - evtCellFormatting 
    ' You can use this to colour the fonts or your cell background or anything else that requires complete control of your cell 
    'Dim pUser As csUser = Nothing 
    'If dgvUser.Columns(e.ColumnIndex).Name = colRecommendedQuantityToOrder.Name Then 
    '  If pUser Is Nothing Then pUser = CType(dgvUser.Rows(e.RowIndex).DataBoundItem, csUser) ' Only assign it if needed 
    '  If pUser.CustomerOrders > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pUser.CustomerOrders > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
    'If dgvUser.Columns(e.ColumnIndex).Name = colRAV.Name Then 
    '  If pUser Is Nothing Then pUser = CType(dgvUser.Rows(e.RowIndex).DataBoundItem, csUser) ' Only assign it if needed
    '  If pUser.RAV > 10 Then 
    '    e.CellStyle.ForeColor = Color.Red 
    '    If pUser.RAV - pUser.MaximumStock > 100 Then 
    '      e.CellStyle.BackColor = Color.LightYellow 
    '    End If 
    '  End If 
    'End If 
 
    'Debug.Print("loc x,y:" & e.RowIndex & ", " & e.ColumnIndex & ": GetType" & dgvUser.Columns(e.ColumnIndex).GetType.ToString & ": zValue" & e.Value.ToString) 
    If dgvUser.Columns(e.ColumnIndex).GetType.ToString = "System.Windows.Forms.DataGridViewComboBoxColumn" Then 
      Dim pCol As System.Windows.Forms.DataGridViewComboBoxColumn = CType(dgvUser.Columns(e.ColumnIndex), System.Windows.Forms.DataGridViewComboBoxColumn) 
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
          e.Value = "* BadCode '" & dgvUser.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & "' *" 
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
 
    If dgvUser.Columns(e.ColumnIndex).GetType.Name.Equals("DataGridViewImageColumn", StringComparison.OrdinalIgnoreCase) Then 
      If e.Value Is Nothing Then 
        e.Value = New Bitmap(1, 1) 
      End If 
    End If 
 
    If _Summarized = True Then 
      If e.RowIndex = dgvUser.Rows.Count - 1 Then 
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
            If _SummaryOverFlow.IndexOf(dgvUser.Columns(e.ColumnIndex).Name.Substring(3)) >= 0 Then 
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
  Private Sub dgvUser_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUser.CellValueChanged 
    If e.RowIndex < 0 Then Exit Sub 
 
  End Sub 
 
  Private Sub dgvUser_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvUser.DataBindingComplete 
    'For ID in XXX 
    For Each pCol As DataGridViewColumn In dgvUser.Columns 
      If pCol.Name.StartsWith("colIDin", StringComparison.OrdinalIgnoreCase) Then 
        Dim pColIndex As Integer = pCol.Index 
        For Each pRow As DataGridViewRow In dgvUser.Rows 
          If Not pRow.Visible Then Continue For 
          Dim pCell As DataGridViewCell = pRow.Cells(pColIndex) 
          Dim pDummy As String = pCell.FormattedValue.ToString() 
        Next 
      End If 
    Next 
    'For ID in XXX 
  End Sub 
  'Grid Sort
  Private Sub dgvUser_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvUser.ColumnHeaderMouseClick
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pNewColumn As DataGridViewColumn = dgvUser.Columns(e.ColumnIndex)
    If bsCtlUser.Current Is Nothing Then Exit Sub

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
    dgvUser.SuspendLayout()

    Dim pUser As csUser
    Dim pID As Long = 0 
    If dgvUser.SelectedRows.Count > 0 Then 
    pUser = CType(bsCtlUser.Current, csUser)
      pID = pUser.ID 
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
    Dim pUserCol As csUserCol
    pUserCol = CType(bsCtlUser.DataSource, csUserCol)

    Dim pSummaryRow As csUser = Nothing 
    If _Summarized = True Then 
      pSummaryRow = pUserCol(pUserCol.Count - 1) 
      pUserCol.RemoveAt(pUserCol.Count - 1) 
    End If 
 
    If pNewSortOrder = SortOrder.Ascending Then
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
        'save the present sort 
        For iCntr As Integer = 0 To pUserCol.Count - 1 
          pUserCol(iCntr).Tag = iCntr.ToString("0000000000") 
        Next 
      End If 
      If pNewColumn Is colID Then
        pUserCol.SortByID()
      ElseIf pNewColumn Is colUserName Then
        pUserCol.SortByUserName()
      ElseIf pNewColumn Is colLastName Then
        pUserCol.SortByLastName()
      ElseIf pNewColumn Is colFirstName Then
        pUserCol.SortByFirstName()
      ElseIf pNewColumn Is colFullName Then
        pUserCol.SortByFullName()
      ElseIf pNewColumn Is colNationalIDNo Then
        pUserCol.SortByNationalIDNo()
      ElseIf pNewColumn Is colAddress Then
        pUserCol.SortByAddress()
      ElseIf pNewColumn Is colCity Then
        pUserCol.SortByCity()
      ElseIf pNewColumn Is colProvinceState Then
        pUserCol.SortByProvinceState()
      ElseIf pNewColumn Is colPostalCode Then
        pUserCol.SortByPostalCode()
      ElseIf pNewColumn Is colCountry Then
        pUserCol.SortByCountry()
      ElseIf pNewColumn Is colPhoneNumber Then
        pUserCol.SortByPhoneNumber()
      ElseIf pNewColumn Is colEmail Then
        pUserCol.SortByEmail()
      ElseIf pNewColumn Is colDatePasswordChanged Then
        pUserCol.SortByDatePasswordChanged()
      ElseIf pNewColumn Is colType Then
        pUserCol.SortByType()
      ElseIf pNewColumn Is colIDinType Then
        pUserCol.SortByIDinType()
      ElseIf pNewColumn Is colRequiresComputerIdentification Then
        pUserCol.SortByRequiresComputerIdentification()
      ElseIf pNewColumn Is colEnableSimultaneousLogins Then
        pUserCol.SortByEnableSimultaneousLogins()
      ElseIf pNewColumn Is colDateActivated Then
        pUserCol.SortByDateActivated()
      ElseIf pNewColumn Is colIsDisabled Then
        pUserCol.SortByIsDisabled()
      ElseIf pNewColumn Is colExpiryDate Then
        pUserCol.SortByExpiryDate()
      ElseIf pNewColumn Is colComments Then
        pUserCol.SortByComments()
      ElseIf pNewColumn Is colLastPasswords Then
        pUserCol.SortByLastPasswords()
      ElseIf pNewColumn Is colApplications Then
        pUserCol.SortByApplications()
      ElseIf pNewColumn Is colLanguage Then
        pUserCol.SortByLanguage()
      ElseIf pNewColumn Is colIsLockedOut Then
        pUserCol.SortByIsLockedOut()
      ElseIf pNewColumn Is colRole OrElse pNewColumn Is colRoleText Then
        pUserCol.SortByRoleText()
      ElseIf pNewColumn Is colAuthenticationMethod Then
        pUserCol.SortByAuthenticationMethod()
      ElseIf pNewColumn Is colRequiresFixedIP Then
        pUserCol.SortByRequiresFixedIP()
      ElseIf pNewColumn Is colMessagingMode Then
        pUserCol.SortByMessagingMode()
      ElseIf pNewColumn Is colLoggedInIP Then
        pUserCol.SortByLoggedInIP()
      ElseIf pNewColumn Is colApprovalFunctionName Then
        pUserCol.SortByApprovalFunctionName()
      ElseIf pNewColumn Is colApprovalTime Then
        pUserCol.SortByApprovalTime()
      ElseIf pNewColumn Is colLastSuccessfulLogin Then
        pUserCol.SortByLastSuccessfulLogin()
      ElseIf pNewColumn Is colPasswordNeverExpires Then
        pUserCol.SortByPasswordNeverExpires()
      ElseIf pNewColumn Is colSecurityQuestion1 Then
        pUserCol.SortBySecurityQuestion1Text()
      ElseIf pNewColumn Is colSecurityQuestion2 Then
        pUserCol.SortBySecurityQuestion2Text()
      ElseIf pNewColumn Is colSecurityQuestion3 Then
        pUserCol.SortBySecurityQuestion3Text()
      End If
      If _PrevSortColumn IsNot Nothing AndAlso _PrevSortColumn IsNot pNewColumn Then 
      Dim iCntr As Integer = 0 
        If pNewColumn Is colID Then
          Dim pTest As Long = 0 
          For Each p As csUser In pUserCol 
            If p.ID <> pTest Then iCntr += 1 : pTest = p.ID 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colUserName Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.UserName <> pTest Then iCntr += 1 : pTest = p.UserName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLastName Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.LastName <> pTest Then iCntr += 1 : pTest = p.LastName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFirstName Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.FirstName <> pTest Then iCntr += 1 : pTest = p.FirstName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colFullName Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.FullName <> pTest Then iCntr += 1 : pTest = p.FullName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colNationalIDNo Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.NationalIDNo <> pTest Then iCntr += 1 : pTest = p.NationalIDNo 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAddress Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.Address <> pTest Then iCntr += 1 : pTest = p.Address 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colCity Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.City <> pTest Then iCntr += 1 : pTest = p.City 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colProvinceState Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.ProvinceState <> pTest Then iCntr += 1 : pTest = p.ProvinceState 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colPostalCode Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.PostalCode <> pTest Then iCntr += 1 : pTest = p.PostalCode 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colCountry Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.Country <> pTest Then iCntr += 1 : pTest = p.Country 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colPhoneNumber Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.PhoneNumber <> pTest Then iCntr += 1 : pTest = p.PhoneNumber 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colEmail Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.Email <> pTest Then iCntr += 1 : pTest = p.Email 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colDatePasswordChanged Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csUser In pUserCol 
            If p.DatePasswordChanged <> pTest Then iCntr += 1 : pTest = p.DatePasswordChanged 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colType Then
          Dim pTest As clsEnums.enmUserIdentityType = clsEnums.enmUserIdentityType.UD 
          For Each p As csUser In pUserCol 
            If p.Type <> pTest Then iCntr += 1 : pTest = p.Type 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colIDinType Then
          Dim pTest As Long = 0 
          For Each p As csUser In pUserCol 
            If p.IDinType <> pTest Then iCntr += 1 : pTest = p.IDinType 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colRequiresComputerIdentification Then
          Dim pTest As Boolean = False 
          For Each p As csUser In pUserCol 
            If p.RequiresComputerIdentification <> pTest Then iCntr += 1 : pTest = p.RequiresComputerIdentification 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colEnableSimultaneousLogins Then
          Dim pTest As Boolean = False 
          For Each p As csUser In pUserCol 
            If p.EnableSimultaneousLogins <> pTest Then iCntr += 1 : pTest = p.EnableSimultaneousLogins 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colDateActivated Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csUser In pUserCol 
            If p.DateActivated <> pTest Then iCntr += 1 : pTest = p.DateActivated 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colIsDisabled Then
          Dim pTest As Boolean = False 
          For Each p As csUser In pUserCol 
            If p.IsDisabled <> pTest Then iCntr += 1 : pTest = p.IsDisabled 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colExpiryDate Then
          Dim pTest As Date = #12:00:00 AM# 
          For Each p As csUser In pUserCol 
            If p.ExpiryDate <> pTest Then iCntr += 1 : pTest = p.ExpiryDate 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colComments Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.Comments <> pTest Then iCntr += 1 : pTest = p.Comments 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLastPasswords Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.LastPasswords <> pTest Then iCntr += 1 : pTest = p.LastPasswords 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colApplications Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.Applications <> pTest Then iCntr += 1 : pTest = p.Applications 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLanguage Then
          Dim pTest As clsEnums.enmLanguage = clsEnums.enmLanguage.UD 
          For Each p As csUser In pUserCol 
            If p.Language <> pTest Then iCntr += 1 : pTest = p.Language 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colIsLockedOut Then
          Dim pTest As Boolean = False 
          For Each p As csUser In pUserCol 
            If p.IsLockedOut <> pTest Then iCntr += 1 : pTest = p.IsLockedOut 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colRole OrElse pNewColumn Is colRoleText Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.RoleText <> pTest Then iCntr += 1 : pTest = p.RoleText 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colAuthenticationMethod Then
          Dim pTest As clsEnums.enmAuthenticationMethod = clsEnums.enmAuthenticationMethod.UD 
          For Each p As csUser In pUserCol 
            If p.AuthenticationMethod <> pTest Then iCntr += 1 : pTest = p.AuthenticationMethod 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colRequiresFixedIP Then
          Dim pTest As Boolean = False 
          For Each p As csUser In pUserCol 
            If p.RequiresFixedIP <> pTest Then iCntr += 1 : pTest = p.RequiresFixedIP 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colMessagingMode Then
          Dim pTest As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD 
          For Each p As csUser In pUserCol 
            If p.MessagingMode <> pTest Then iCntr += 1 : pTest = p.MessagingMode 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLoggedInIP Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.LoggedInIP <> pTest Then iCntr += 1 : pTest = p.LoggedInIP 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colApprovalFunctionName Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.ApprovalFunctionName <> pTest Then iCntr += 1 : pTest = p.ApprovalFunctionName 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colApprovalTime Then
          Dim pTest As DateTimeOffset = DateTimeOffset.MinValue 
          For Each p As csUser In pUserCol 
            If p.ApprovalTime <> pTest Then iCntr += 1 : pTest = p.ApprovalTime 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colLastSuccessfulLogin Then
          Dim pTest As DateTimeOffset = DateTimeOffset.MinValue 
          For Each p As csUser In pUserCol 
            If p.LastSuccessfulLogin <> pTest Then iCntr += 1 : pTest = p.LastSuccessfulLogin 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colPasswordNeverExpires Then
          Dim pTest As Boolean = False 
          For Each p As csUser In pUserCol 
            If p.PasswordNeverExpires <> pTest Then iCntr += 1 : pTest = p.PasswordNeverExpires 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colSecurityQuestion1 Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.SecurityQuestion1Text <> pTest Then iCntr += 1 : pTest = p.SecurityQuestion1Text 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colSecurityQuestion2 Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.SecurityQuestion2Text <> pTest Then iCntr += 1 : pTest = p.SecurityQuestion2Text 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        ElseIf pNewColumn Is colSecurityQuestion3 Then
          Dim pTest As String = "" 
          For Each p As csUser In pUserCol 
            If p.SecurityQuestion3Text <> pTest Then iCntr += 1 : pTest = p.SecurityQuestion3Text 
            p.Tag = iCntr.ToString("0000000000") & p.Tag 
          Next 
        End If 
        pUserCol.SortByTag() 
      End If 
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
      _PrevSortColumn = pNewColumn
      pPrevSortOrder = SortOrder.Ascending
    Else
      pUserCol.Reverse()
      pNewColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
      pPrevSortOrder = SortOrder.Descending
    End If

    If _Summarized = True Then 
      pUserCol.Add(pSummaryRow) 
    End If 
 
    If pID > 0 Then
      bsCtlUser.Position = bsCtlUser.IndexOf(pUserCol.FindByID(pID))
    End If

    'dgvUser.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
    dgvUser.ResumeLayout()

    Cursor = Cursors.Default
    dgvUser.Refresh()

  End Sub
  'Select Row 
  Public Sub SelectRowByObjectID(ByVal pID As Long) 
    If pID > 0 Then 
      Dim pUserCol As csUserCol 
      pUserCol = CType(bsCtlUser.DataSource, csUserCol) 
      Dim pUser As csUser = pUserCol.FindByID(pID) 
      If Not pUser.IsEmpty Then 
        bsCtlUser.Position = bsCtlUser.IndexOf(pUserCol.FindByID(pID)) 
        dgvUser.Rows(bsCtlUser.Position).Selected = True 
      Else 
        dgvUser.ClearSelection() 
      End If 
    ElseIf pID = 0 Then 
      dgvUser.ClearSelection() 
    End If 
  End Sub 
  
  'Grid Resize
  Private Sub dgvUser_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvUser.ColumnHeaderMouseDoubleClick
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Cursor = Cursors.WaitCursor
    dgvUser.AutoResizeColumn(e.ColumnIndex)
    Cursor = Cursors.Default
  End Sub
  'Other Grid Events
  Private Sub dgvUser_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvUser.CurrentCellDirtyStateChanged
   _DVGDirty = True 
 
    'For ID in XXX  
 
    Dim pRowIndex As Integer = dgvUser.CurrentCell.RowIndex 
    Dim pColIndex As Integer = dgvUser.CurrentCell.ColumnIndex 
 
    If pRowIndex < 0 Then Exit Sub 
    If pColIndex = colType.Index Then 
      _IgnoreGridFault = True 
      Me.Cursor = Cursors.WaitCursor 
      dgvUser.Cursor = Cursors.WaitCursor 
      Application.DoEvents() 
      Dim pType As clsEnums.enmUserIdentityType = clsEnums.TranslateEnmUserIdentityType(dgvUser(pColIndex, pRowIndex).Value.ToString()) 
 
      Dim pCell As DataGridViewComboBoxCell = CType(dgvUser(colIDinType.Index, pRowIndex), DataGridViewComboBoxCell) 
      pCell.DisplayMember = "Text" 
      pCell.ValueMember = "KeyLong" 
      Select Case pType 
        Case clsEnums.enmUserIdentityType.Global 
          pCell.DataSource = _Global 
        Case clsEnums.enmUserIdentityType.c_User 
          pCell.DataSource = _Users 
        Case clsEnums.enmUserIdentityType.Customer 
          pCell.DataSource = _Customers 
        Case Else 
          pCell.DataSource = _UD 
      End Select 
      dgvUser.Cursor = Cursors.Default 
      Me.Cursor = Cursors.Default 
      Application.DoEvents() 
      dgvUser.CurrentCell = pCell 
      _IgnoreGridFault = False 
    End If 
 
    'For ID in XXX  
 
  End Sub
  Private Sub dgvUser_Scroll(sender As Object, e As ScrollEventArgs) Handles dgvUser.Scroll
    dgvUser.Invalidate() 
  End Sub
 
  Private Sub dgvUser_DataFault(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvUser.DataError
    'Dim pFault As New clsFault
    '
    'If e.RowIndex = dgvUser.Rows.Count - 1 Then Exit Sub
 
    'If dgvUser.Columns(e.ColumnIndex).Name.StartsWith("colIDin", StringComparison.OrdinalIgnoreCase) Then Exit Sub 
 
    'If _IgnoreGridFault = True Then Exit Sub
    '_DVGDirty = False 
    'Static pShown As Boolean 
    '
    'Dim pSubStrg As New System.Text.StringBuilder 
    ''Other Error 
    'Try 
    '  Try 
    '    pSubStrg.AppendLine("In table 'User', the row with an ID of " & dgvUser.Rows(e.RowIndex).Cells(0).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine("In grid 'User', row index " & e.RowIndex) 
    '  End Try 
    '  Try 
    '    pSubStrg.AppendLine(" has an invalid value of " & dgvUser.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
    '  Catch ex As Exception 
    '    pSubStrg.AppendLine(" has an invalid value of Nothing.") 
    '  End Try 
    '  pSubStrg.AppendLine(" in column " & dgvUser.Columns(e.ColumnIndex).DataPropertyName) 
    'Catch ex As Exception 
    '  pSubStrg.AppendLine("; Failed trying to fill DataFault as well!") 
    'End Try 
    'pFault.LogException(209, e.Exception, pSubStrg.ToString, "TRGT-User-100409-2248", _Requester) 
    'If pShown = False Then 
    '  Dim pCell As DataGridViewCell 
    '  Try 
    '    pCell = dgvUser(e.ColumnIndex, e.RowIndex)
    '  Catch ex As Exception 
    '    pCell = dgvUser(0, 0)
    '  End Try 
    '  ShowFault(pFault, _Requester) 
    '  pShown = True 
    'End If 
  End Sub
  Private Sub dgvUser_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvUser.KeyDown 
    If e.KeyCode = Keys.Escape Then 
      'DoCeaseEdit() 
      Dim pUser As csUser = CType(bsCtlUser.Current, csUser) 
      If pUser IsNot Nothing Then 
        If pUser.ID = 0 Then 
          _IgnoreGridFault = True 
          bsCtlUser.Remove(bsCtlUser.Current) 
          _IgnoreGridFault = False 
        End If 
      End If 
      SetUpBNButtons(False) 
      'dgvUser.CurrentCell = dgvUser.Rows(dgvUser.CurrentCell.RowIndex).Cells(0) 
      dgvUser.Refresh() 
    End If 
  End Sub 
  Private Sub dgvUser_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvUser.ColumnWidthChanged
    If Me.DesignMode = True Then Exit Sub 
    If _Loading = False Then SaveSizes()
  End Sub
  Private Sub dgvUser_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvUser.ColumnDisplayIndexChanged
    Cursor = Cursors.WaitCursor
    If _Loading = False Then SaveSizes()
    Cursor = Cursors.Default
  End Sub
  Private Sub dgvUser_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUser.CellDoubleClick 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If e.RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso e.RowIndex = dgvUser.Rows.Count - 1 Then Exit Sub 
    Cursor = Cursors.WaitCursor 
    Dim pCell As DataGridViewCell = dgvUser(e.ColumnIndex, e.RowIndex) 
 
    Dim pHandled As Boolean = False 
    Dim pUser As csUser = _UserCol(e.RowIndex)
    RaiseEvent evtRowDoubleClicked(pUser, pHandled) 
    Cursor = Cursors.Default 
 
    If pHandled = False Then 
      If Me.ParentForm.Name.Equals("frmPopup", StringComparison.OrdinalIgnoreCase) Then Return 
      frmPopup.Text = "User Detail" 
      Dim pFault As clsFault = frmPopup.LoadControl("ctlc_User", pUser, _Requester) 
      If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
      frmPopup.ShowDialog() 
    End If 
 
  End Sub 
  Private Sub dgvUser_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvUser.SelectionChanged 
    If btnCeaseEdit.Visible = True Then Exit Sub 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If _Loading = True Then Exit Sub 
    If DateTime.Now < _IgnoreSelectionUntil Then Exit Sub
    If _ProcessingSelection Then Exit Sub
    If dgvUser.SelectedRows.Count = 0 Then 
      RaiseEvent evtUnChosen() 
      Exit Sub 
    End If 
    Dim RowIndex As Integer = dgvUser.SelectedRows(0).Cells(0).RowIndex 
    If RowIndex < 0 Then Exit Sub 
    If _Summarized = True AndAlso RowIndex = dgvUser.Rows.Count - 1 Then dgvUser.ClearSelection() : RaiseEvent evtUnChosen() : Exit Sub 
    Dim pUser As csUser = _UserCol(RowIndex)
    _ProcessingSelection = True
    Try
      RaiseEvent evtRowClicked(pUser) 
    Finally
      _IgnoreSelectionUntil = DateTime.Now.AddMilliseconds(500)
      _ProcessingSelection = False
    End Try
  End Sub 
  Private Sub dgvUser_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUser.RowLeave 
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
 
    Dim pOriginalCol As Integer = dgvUser.CurrentCell.ColumnIndex 
     
    'If user clicked on CeaseEdit without changing cells, the data will not be received 
    ' therefore we have to fake exiting the cell 
    Dim pNewCol As Integer 
    'We can only go to a visible cell! 
    If pOriginalCol > 0 Then 
      pNewCol = pOriginalCol - 1 
      Do Until dgvUser.Columns(pNewCol).Visible = True OrElse pNewCol = 0 
        pNewCol = pNewCol - 1 
      Loop 
    Else 
      pNewCol = 1 
    End If 
    If dgvUser.Columns(pNewCol).Visible = False Then 
      dgvUser.Columns(pNewCol).Visible = True 
      dgvUser.CurrentCell = dgvUser.CurrentRow.Cells(pNewCol) 
      dgvUser.CurrentCell = dgvUser.CurrentRow.Cells(pOriginalCol) 
      dgvUser.Columns(pNewCol).Visible = False 
    Else 
      dgvUser.CurrentCell = dgvUser.CurrentRow.Cells(pNewCol) 
      dgvUser.CurrentCell = dgvUser.CurrentRow.Cells(pOriginalCol) 
    End If 
    dgvUser.Rows(dgvUser.CurrentCell.RowIndex).Selected = True 
    Dim pUser As csUser 
    pUser = CType(bsCtlUser.Current, csUser) 
 
    'Add required data (primary keys) from parent objects  
    RaiseEvent evtBeforeUpdate(CType(pUser, csUser), pCancel) 
    If pCancel = True Then 
      _DVGDirty = False 
      RaiseEvent evtTimerTripped() 
      Return True 
    End If 
    pFault = pUser.Update(_Requester) 
    If pFault.isOK = False AndAlso pFault.Severity <> clsEnums.enmFaultSeverity.LogOnly Then 
      ShowFault(pFault, _Requester) 
      frmMessageOrInputBox.ShowMsg("Fix the problem, or click on 'Esc' to remove the row.", frmMessageOrInputBox.enmIconType.Information, frmMessageOrInputBox.enmButtons.Yes) 
      Return True 
    Else 
      If pFault.isOK = False Then 'AndAlso pFault.Severity = clsEnums.enmFaultSeverity.LogOnly  
        ShowFault(pFault, _Requester) 
      End If 
      dgvUser.EndEdit() 
      _DVGDirty = False 
      'Reset the User collection 
      MyCache.ClearComboList(clsEnums.enmComboListType.c_UserDefaultByID) 
      RaiseEvent evtUpdated(pUser) 
      Return False 
    End If 
  End Function 
  Private Sub SaveSizes() 
    ' Save column state data  
    ' including order, column width and whether or not the column is visible  
    For Each pCol As DataGridViewColumn In dgvUser.Columns 
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
    If _UserCol.Count <= 1 Then 
      _Summarized = False 
      Exit Sub 
    End If 
 
    Dim pIDinType As Long 
    For Each pExistingRow As csUser In _UserCol 
      If _SummaryOverFlow.IndexOf("#IDinType#") < 0 Then 
        Try 
          pIDinType += pExistingRow.IDinType 
        Catch ex As System.OverflowException 
          pIDinType = -99999999 
          _SummaryOverFlow &= "IDinType#" 
        End Try 
      End If 
    Next 
    If _LoadParameters.DoNotSummarizeProperties.Find(Function(p) p = csUser.enmSummarizeableProperty.IDinType) = csUser.enmSummarizeableProperty.IDinType Then pIDinType = 0
    Dim pSummaryRow As New csUser( _ 
        vID:=0 _ 
      , vUserName:="" _ 
      , vLastName:="" _ 
      , vFirstName:="" _ 
      , vFullName:="" _ 
      , vNationalIDNo:="" _ 
      , vAddress:="" _ 
      , vCity:="" _ 
      , vProvinceState:="" _ 
      , vPostalCode:="" _ 
      , vCountry:="" _ 
      , vPhoneNumber:="" _ 
      , vEmail:="" _ 
      , vPasswordHashed:="" _ 
      , vDatePasswordChanged:=Nothing _ 
      , vType:=clsEnums.enmUserIdentityType.UD _ 
      , vTypeText:="" _ 
      , vIDinType:=pIDinType _ 
      , vRequiresComputerIdentification:=False _ 
      , vEnableSimultaneousLogins:=False _ 
      , vDateActivated:=Nothing _ 
      , vIsDisabled:=False _ 
      , vExpiryDate:=Nothing _ 
      , vComments:="" _ 
      , vLastPasswords:="" _ 
      , vApplications:="" _ 
      , vLanguage:=clsEnums.enmLanguage.UD _ 
      , vLanguageText:="" _ 
      , vIsLockedOut:=False _ 
      , vRoleID:=0 _ 
      , vRoleText:="" _ 
      , vAuthenticationMethod:=clsEnums.enmAuthenticationMethod.UD _ 
      , vAuthenticationMethodText:="" _ 
      , vRequiresFixedIP:=False _ 
      , vMessagingMode:=clsEnums.enmMessagingMode.UD _ 
      , vMessagingModeText:="" _ 
      , vLoggedInIP:="" _ 
      , vApprovalCodeHashed:="" _ 
      , vApprovalFunctionName:="" _ 
      , vApprovalTime:=Nothing _ 
      , vLastSuccessfulLogin:=Nothing _ 
      , vPasswordNeverExpires:=False _ 
      , vSecurityQuestion1Code:="" _ 
      , vSecurityQuestion1Text:="" _ 
      , vSecurityQuestion1Response:="" _ 
      , vSecurityQuestion2Code:="" _ 
      , vSecurityQuestion2Text:="" _ 
      , vSecurityQuestion2Response:="" _ 
      , vSecurityQuestion3Code:="" _ 
      , vSecurityQuestion3Text:="" _ 
      , vSecurityQuestion3Response:="" _ 
      , vPIN:="" _ 
      , vTag:="" _ 
      , vDateAdded:=Nothing _ 
      , vWithParents:=clsEnums.enmLoadParent.TextOnly _ 
      )
    _UserCol.Add(pSummaryRow) 
    _Summarized = True 
  End Sub 
  
  'Reports and Excel 
  Friend Function CreateSpreadSheet() As clsFault  
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name  
    Dim pFault As New clsFault  
    'Dim pExcel As New Tools.ExcelSheet  
    Dim pDateToShow As String = DateTime.Now.ToString("yyMMdd_HHmmss")  
    Dim pRoot As String = $"{My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData}\MyFiles" 
 
    Dim pFileNameAllFields As String = $"{pRoot}\UserCol_{pDateToShow}AllFields.csv" 
    Dim pFileNameFieldsOnGrid As String = $"{pRoot}\UserCol_{pDateToShow}FieldsOnGrid.csv" 
    Dim pFileNameAllFieldsWithIDs As String = $"{pRoot}\UserCol_{pDateToShow}AllFieldsWithIDs.csv" 
    Dim pFileNameAllFieldsXML As String = $"{pRoot}\UserCol_{pDateToShow}AllFields.xml" 
    Dim pFileNameAllFieldsJson As String = $"{pRoot}\UserCol_{pDateToShow}AllFields.json" 
 
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
    For Each pCol As DataGridViewColumn In dgvUser.Columns 
      If pCol.Visible = True Then 
        pTmpStrg.Append(",""" & pCol.HeaderText & """") 
      End If 
    Next 
    pCSV.AppendLine(pTmpStrg.ToString.Substring(1)) 
 
    'Now the data  
    Dim i As Integer 
    Dim pStart As Date = Now 
 
    Dim pTruncatedFieldNames As String = "" 
    For Each Row As DataGridViewRow In dgvUser.Rows 
      i += 1 
      If _LoadParameters.SummarizeGrid = True Then 
        If Row.Index = dgvUser.Rows.Count - 1 Then Exit For 
      End If 
      If i Mod 500 = 0 Then 
        lblStatus.Text = " Writing Row " & i & ". Time Elapsed: " & DateTime.Now.Subtract(pStart).TotalSeconds().ToString("###0") & " sec" : Application.DoEvents() 
      End If 
      pTmpStrg = New System.Text.StringBuilder 
       
      For Each pCol As DataGridViewColumn In dgvUser.Columns 
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
        pFault = _UserColFullLength.CreateXML(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsXML, pStrg, False) 
        'json 
        pFault = _UserColFullLength.CreateJSON(pStrg, _Requester) : If pFault.isOK = False Then Return pFault 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsJson, pStrg, False) 
        'default  
        My.Computer.FileSystem.WriteAllText(pFileNameAllFields, _UserColFullLength.ToCSV, False)  
        'WithIDs  
        'pFault = _UserColFullLength.LoadLookupAndEnumText(_Requester) : If pFault.isOK = False Then Return pFault (already done) 
        My.Computer.FileSystem.WriteAllText(pFileNameAllFieldsWithIDs, _UserColFullLength.ToCSV(True), False) 
      End If  
      'default  
      My.Computer.FileSystem.WriteAllText(pFileNameFieldsOnGrid, pCSV.ToString, False)  
      pFault.SetOK()  
    Catch ex As Exception  
      pFault.LogException(ex, "", "TRGT-User-090210-1618", _Requester)  
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
      pFault.LogException(ex, "", "TRGT-User-090210-1618", _Requester)  
    End Try  
  
    If pFault.isOK = False Then Return pFault  
    
    Return pFault  
  End Function  
 
  Private Sub ReportDesign() 
 
    _Report = New vbReport.ReportDocument 
    _Report.AutoDiscover = False 
    Try 
      For Each pCol As DataGridViewColumn In dgvUser.Columns 
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
        .SubTitleLeft = "Users" 
        .SubTitleRight = "Rows: " & _UserCol.Count.ToString 
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
        .DataSource = _UserCol 
        .HasSummaryLine = _Summarized 
      End With 
    Catch ex As Exception 
      Dim pFault As New clsFault 
      pFault.LogException(ex, "", "TRGT-User-090210-2119", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
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
      pFault.LogException(ex, "", "TRGT-User-090211-0746", _Requester) 
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
      For Each pRow As DataGridViewRow In dgvUser.Rows 
        Try : pRow.Visible = True : Catch : End Try 
      Next 
      lblStatus.ForeColor = Color.DarkGreen 
      lblStatus.Text = dgvUser.RowCount & " rows" 
      Exit Sub 
    End If 
    ' Hide rows that don't match search text 
    For Each row As DataGridViewRow In dgvUser.Rows 
      Dim pVisible As Boolean = False 
      For Each cell As DataGridViewCell In row.Cells 
        If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(pSearchText) Then 
          pVisible = True : Exit For 
        End If 
      Next 
      Try 
        Dim pBS As CurrencyManager = CType(Me.BindingContext(bsCtlUser), CurrencyManager) 
        row.Visible = pVisible 
      Catch : End Try 
    Next 
    Dim pVisibleCount As Integer = 0 
    For Each row As DataGridViewRow In dgvUser.Rows 
      If row.Visible Then pVisibleCount += 1 
    Next 
    lblStatus.ForeColor = Color.DarkBlue 
    lblStatus.Text = pVisibleCount & " of " & dgvUser.RowCount & " rows" 
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
      pFault.LogException(ex, "GetOrInitializeGridSettings", "TRGT-User-120225-1310", _Requester) 
    End Try 
    If pFault.isOK = False Then Return pFault 
 
    Dim pSaveInitial As Boolean = False 
    
    '_GridSettings.Clear() Use for testing 
    If _GridSettings.Count = 0 Then 
      For Each pCol As DataGridViewColumn In dgvUser.Columns 
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
      For Each pCol As DataGridViewColumn In dgvUser.Columns 
        Dim pG As clsGridSetting = _GridSettings.FindByColumnName(pCol.Name) 
        If pG.ColumnName = "" Then 
          pG.ColumnDisplayIndex = pCol.DisplayIndex 
          pG.ColumnWidth = ccHelper.ToInteger((dgvUser.Width - 30) / dgvUser.Columns.Count) 
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
    'colAddress.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colComments.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colLastPasswords.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colLoggedInIP.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    'colApprovalFunctionName.DefaultCellStyle.WrapMode = DataGridViewTriState.True 
    
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
           
          With dgvUser.Columns(lGridSetting.ColumnName) 
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
      pFault.LogException(204, ex, "", "TRGT-User-090120-1502", _Requester) : ShowFault(pFault, _Requester) : Exit Sub 
    End Try 
  End Sub 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ID", _Requester) 
    If pStrg <> "" Then colID.HeaderText = pStrg : mnuColVisibleID.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "UserName", _Requester) 
    If pStrg <> "" Then colUserName.HeaderText = pStrg : mnuColVisibleUserName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "LastName", _Requester) 
    If pStrg <> "" Then colLastName.HeaderText = pStrg : mnuColVisibleLastName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "FirstName", _Requester) 
    If pStrg <> "" Then colFirstName.HeaderText = pStrg : mnuColVisibleFirstName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "FullName", _Requester) 
    If pStrg <> "" Then colFullName.HeaderText = pStrg : mnuColVisibleFullName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "NationalIDNo", _Requester) 
    If pStrg <> "" Then colNationalIDNo.HeaderText = pStrg : mnuColVisibleNationalIDNo.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Address", _Requester) 
    If pStrg <> "" Then colAddress.HeaderText = pStrg : mnuColVisibleAddress.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "City", _Requester) 
    If pStrg <> "" Then colCity.HeaderText = pStrg : mnuColVisibleCity.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ProvinceState", _Requester) 
    If pStrg <> "" Then colProvinceState.HeaderText = pStrg : mnuColVisibleProvinceState.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PostalCode", _Requester) 
    If pStrg <> "" Then colPostalCode.HeaderText = pStrg : mnuColVisiblePostalCode.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Country", _Requester) 
    If pStrg <> "" Then colCountry.HeaderText = pStrg : mnuColVisibleCountry.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PhoneNumber", _Requester) 
    If pStrg <> "" Then colPhoneNumber.HeaderText = pStrg : mnuColVisiblePhoneNumber.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Email", _Requester) 
    If pStrg <> "" Then colEmail.HeaderText = pStrg : mnuColVisibleEmail.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PasswordHashed", _Requester) 
    If pStrg <> "" Then colPasswordHashed.HeaderText = pStrg : mnuColVisiblePasswordHashed.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "DatePasswordChanged", _Requester) 
    If pStrg <> "" Then colDatePasswordChanged.HeaderText = pStrg : mnuColVisibleDatePasswordChanged.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Type", _Requester) 
    If pStrg <> "" Then colType.HeaderText = pStrg : mnuColVisibleType.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "IDinType", _Requester) 
    If pStrg <> "" Then colIDinType.HeaderText = pStrg : mnuColVisibleIDinType.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "RequiresComputerIdentification", _Requester) 
    If pStrg <> "" Then colRequiresComputerIdentification.HeaderText = pStrg : mnuColVisibleRequiresComputerIdentification.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "EnableSimultaneousLogins", _Requester) 
    If pStrg <> "" Then colEnableSimultaneousLogins.HeaderText = pStrg : mnuColVisibleEnableSimultaneousLogins.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "DateActivated", _Requester) 
    If pStrg <> "" Then colDateActivated.HeaderText = pStrg : mnuColVisibleDateActivated.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "IsDisabled", _Requester) 
    If pStrg <> "" Then colIsDisabled.HeaderText = pStrg : mnuColVisibleIsDisabled.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ExpiryDate", _Requester) 
    If pStrg <> "" Then colExpiryDate.HeaderText = pStrg : mnuColVisibleExpiryDate.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Comments", _Requester) 
    If pStrg <> "" Then colComments.HeaderText = pStrg : mnuColVisibleComments.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "LastPasswords", _Requester) 
    If pStrg <> "" Then colLastPasswords.HeaderText = pStrg : mnuColVisibleLastPasswords.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Applications", _Requester) 
    If pStrg <> "" Then colApplications.HeaderText = pStrg : mnuColVisibleApplications.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Language", _Requester) 
    If pStrg <> "" Then colLanguage.HeaderText = pStrg : mnuColVisibleLanguage.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "IsLockedOut", _Requester) 
    If pStrg <> "" Then colIsLockedOut.HeaderText = pStrg : mnuColVisibleIsLockedOut.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "Role", _Requester) 
    If pStrg <> "" Then colRole.HeaderText = pStrg : mnuColVisibleRole.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "AuthenticationMethod", _Requester) 
    If pStrg <> "" Then colAuthenticationMethod.HeaderText = pStrg : mnuColVisibleAuthenticationMethod.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "RequiresFixedIP", _Requester) 
    If pStrg <> "" Then colRequiresFixedIP.HeaderText = pStrg : mnuColVisibleRequiresFixedIP.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "MessagingMode", _Requester) 
    If pStrg <> "" Then colMessagingMode.HeaderText = pStrg : mnuColVisibleMessagingMode.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "LoggedInIP", _Requester) 
    If pStrg <> "" Then colLoggedInIP.HeaderText = pStrg : mnuColVisibleLoggedInIP.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ApprovalCodeHashed", _Requester) 
    If pStrg <> "" Then colApprovalCodeHashed.HeaderText = pStrg : mnuColVisibleApprovalCodeHashed.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ApprovalFunctionName", _Requester) 
    If pStrg <> "" Then colApprovalFunctionName.HeaderText = pStrg : mnuColVisibleApprovalFunctionName.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "ApprovalTime", _Requester) 
    If pStrg <> "" Then colApprovalTime.HeaderText = pStrg : mnuColVisibleApprovalTime.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "LastSuccessfulLogin", _Requester) 
    If pStrg <> "" Then colLastSuccessfulLogin.HeaderText = pStrg : mnuColVisibleLastSuccessfulLogin.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PasswordNeverExpires", _Requester) 
    If pStrg <> "" Then colPasswordNeverExpires.HeaderText = pStrg : mnuColVisiblePasswordNeverExpires.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion1", _Requester) 
    If pStrg <> "" Then colSecurityQuestion1.HeaderText = pStrg : mnuColVisibleSecurityQuestion1.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion1Response", _Requester) 
    If pStrg <> "" Then colSecurityQuestion1Response.HeaderText = pStrg : mnuColVisibleSecurityQuestion1Response.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion2", _Requester) 
    If pStrg <> "" Then colSecurityQuestion2.HeaderText = pStrg : mnuColVisibleSecurityQuestion2.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion2Response", _Requester) 
    If pStrg <> "" Then colSecurityQuestion2Response.HeaderText = pStrg : mnuColVisibleSecurityQuestion2Response.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion3", _Requester) 
    If pStrg <> "" Then colSecurityQuestion3.HeaderText = pStrg : mnuColVisibleSecurityQuestion3.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "SecurityQuestion3Response", _Requester) 
    If pStrg <> "" Then colSecurityQuestion3Response.HeaderText = pStrg : mnuColVisibleSecurityQuestion3Response.Text = pStrg
 
    pStrg = ccHelper.GetLocalizedFieldName("c_User", "PIN", _Requester) 
    If pStrg <> "" Then colPIN.HeaderText = pStrg : mnuColVisiblePIN.Text = pStrg
 
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
 
  Private Sub mnuColVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColVisibleID.Click, mnuColVisibleUserName.Click, mnuColVisibleLastName.Click, mnuColVisibleFirstName.Click, mnuColVisibleFullName.Click, mnuColVisibleNationalIDNo.Click, mnuColVisibleAddress.Click, mnuColVisibleCity.Click, mnuColVisibleProvinceState.Click, mnuColVisiblePostalCode.Click, mnuColVisibleCountry.Click, mnuColVisiblePhoneNumber.Click, mnuColVisibleEmail.Click, mnuColVisiblePasswordHashed.Click, mnuColVisibleDatePasswordChanged.Click, mnuColVisibleType.Click, mnuColVisibleIDinType.Click, mnuColVisibleRequiresComputerIdentification.Click, mnuColVisibleEnableSimultaneousLogins.Click, mnuColVisibleDateActivated.Click, mnuColVisibleIsDisabled.Click, mnuColVisibleExpiryDate.Click, mnuColVisibleComments.Click, mnuColVisibleLastPasswords.Click, mnuColVisibleApplications.Click, mnuColVisibleLanguage.Click, mnuColVisibleIsLockedOut.Click, mnuColVisibleRole.Click, mnuColVisibleAuthenticationMethod.Click, mnuColVisibleRequiresFixedIP.Click, mnuColVisibleMessagingMode.Click, mnuColVisibleLoggedInIP.Click, mnuColVisibleApprovalCodeHashed.Click, mnuColVisibleApprovalFunctionName.Click, mnuColVisibleApprovalTime.Click, mnuColVisibleLastSuccessfulLogin.Click, mnuColVisiblePasswordNeverExpires.Click, mnuColVisibleSecurityQuestion1.Click, mnuColVisibleSecurityQuestion1Response.Click, mnuColVisibleSecurityQuestion2.Click, mnuColVisibleSecurityQuestion2Response.Click, mnuColVisibleSecurityQuestion3.Click, mnuColVisibleSecurityQuestion3Response.Click, mnuColVisiblePIN.Click
    Cursor = Cursors.WaitCursor 
    Dim pToolStripItem As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem) 
    dgvUser.Columns("col" & pToolStripItem.Name.Substring(13)).Visible = pToolStripItem.Checked 
    If _Loading = False Then SaveSizes() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsReset.Click 
    Cursor = Cursors.WaitCursor 
    dgvUser.SuspendLayout() 
 
    For Each pCol As DataGridViewColumn In dgvUser.Columns 
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
    pNewWidth = ccHelper.ToInteger((dgvUser.Width - 30) / pVisibleColumns) 
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
     
    dgvUser.ResumeLayout() 
    Cursor = Cursors.Default 
  End Sub 
  Private Sub mnuColsHideMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColsHideMost.Click 
 
    _Loading = True 
    'Hide All 
    If mnuColVisibleID.Checked = True Then mnuColVisibleID.PerformClick() 
    If mnuColVisibleUserName.Checked = True Then mnuColVisibleUserName.PerformClick() 
    If mnuColVisibleLastName.Checked = True Then mnuColVisibleLastName.PerformClick() 
    If mnuColVisibleFirstName.Checked = True Then mnuColVisibleFirstName.PerformClick() 
    If mnuColVisibleFullName.Checked = True Then mnuColVisibleFullName.PerformClick() 
    If mnuColVisibleNationalIDNo.Checked = True Then mnuColVisibleNationalIDNo.PerformClick() 
    If mnuColVisibleAddress.Checked = True Then mnuColVisibleAddress.PerformClick() 
    If mnuColVisibleCity.Checked = True Then mnuColVisibleCity.PerformClick() 
    If mnuColVisibleProvinceState.Checked = True Then mnuColVisibleProvinceState.PerformClick() 
    If mnuColVisiblePostalCode.Checked = True Then mnuColVisiblePostalCode.PerformClick() 
    If mnuColVisibleCountry.Checked = True Then mnuColVisibleCountry.PerformClick() 
    If mnuColVisiblePhoneNumber.Checked = True Then mnuColVisiblePhoneNumber.PerformClick() 
    If mnuColVisibleEmail.Checked = True Then mnuColVisibleEmail.PerformClick() 
    If mnuColVisiblePasswordHashed.Checked = True Then mnuColVisiblePasswordHashed.PerformClick() 
    If mnuColVisibleDatePasswordChanged.Checked = True Then mnuColVisibleDatePasswordChanged.PerformClick() 
    If mnuColVisibleType.Checked = True Then mnuColVisibleType.PerformClick() 
    If mnuColVisibleIDinType.Checked = True Then mnuColVisibleIDinType.PerformClick() 
    If mnuColVisibleRequiresComputerIdentification.Checked = True Then mnuColVisibleRequiresComputerIdentification.PerformClick() 
    If mnuColVisibleEnableSimultaneousLogins.Checked = True Then mnuColVisibleEnableSimultaneousLogins.PerformClick() 
    If mnuColVisibleDateActivated.Checked = True Then mnuColVisibleDateActivated.PerformClick() 
    If mnuColVisibleIsDisabled.Checked = True Then mnuColVisibleIsDisabled.PerformClick() 
    If mnuColVisibleExpiryDate.Checked = True Then mnuColVisibleExpiryDate.PerformClick() 
    If mnuColVisibleComments.Checked = True Then mnuColVisibleComments.PerformClick() 
    If mnuColVisibleLastPasswords.Checked = True Then mnuColVisibleLastPasswords.PerformClick() 
    If mnuColVisibleApplications.Checked = True Then mnuColVisibleApplications.PerformClick() 
    If mnuColVisibleLanguage.Checked = True Then mnuColVisibleLanguage.PerformClick() 
    If mnuColVisibleIsLockedOut.Checked = True Then mnuColVisibleIsLockedOut.PerformClick() 
    If mnuColVisibleRole.Checked = True Then mnuColVisibleRole.PerformClick() 
    If mnuColVisibleAuthenticationMethod.Checked = True Then mnuColVisibleAuthenticationMethod.PerformClick() 
    If mnuColVisibleRequiresFixedIP.Checked = True Then mnuColVisibleRequiresFixedIP.PerformClick() 
    If mnuColVisibleMessagingMode.Checked = True Then mnuColVisibleMessagingMode.PerformClick() 
    If mnuColVisibleLoggedInIP.Checked = True Then mnuColVisibleLoggedInIP.PerformClick() 
    If mnuColVisibleApprovalCodeHashed.Checked = True Then mnuColVisibleApprovalCodeHashed.PerformClick() 
    If mnuColVisibleApprovalFunctionName.Checked = True Then mnuColVisibleApprovalFunctionName.PerformClick() 
    If mnuColVisibleApprovalTime.Checked = True Then mnuColVisibleApprovalTime.PerformClick() 
    If mnuColVisibleLastSuccessfulLogin.Checked = True Then mnuColVisibleLastSuccessfulLogin.PerformClick() 
    If mnuColVisiblePasswordNeverExpires.Checked = True Then mnuColVisiblePasswordNeverExpires.PerformClick() 
    If mnuColVisibleSecurityQuestion1.Checked = True Then mnuColVisibleSecurityQuestion1.PerformClick() 
    If mnuColVisibleSecurityQuestion1Response.Checked = True Then mnuColVisibleSecurityQuestion1Response.PerformClick() 
    If mnuColVisibleSecurityQuestion2.Checked = True Then mnuColVisibleSecurityQuestion2.PerformClick() 
    If mnuColVisibleSecurityQuestion2Response.Checked = True Then mnuColVisibleSecurityQuestion2Response.PerformClick() 
    If mnuColVisibleSecurityQuestion3.Checked = True Then mnuColVisibleSecurityQuestion3.PerformClick() 
    If mnuColVisibleSecurityQuestion3Response.Checked = True Then mnuColVisibleSecurityQuestion3Response.PerformClick() 
    If mnuColVisiblePIN.Checked = True Then mnuColVisiblePIN.PerformClick() 
    'Show Defaults 
    If mnuColVisibleFirstName.Checked = False Then mnuColVisibleFirstName.PerformClick() 
    If mnuColVisibleLastName.Checked = False Then mnuColVisibleLastName.PerformClick() 
    If mnuColVisibleUserName.Checked = False Then mnuColVisibleUserName.PerformClick() 
    
    _Loading = False 
    'dgvUser.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells) 
  End Sub 
  
  Private Sub dgvUser_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvUser.CellMouseClick 
    If e.Button = MouseButtons.Right Then 
      Dim pMessageBox As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("How do you want to copy the User to the clipboard?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNoCancel, vYesText:="Text", vNoText:="CSV") 
      If pMessageBox <> frmMessageOrInputBox.enmButtonReturned.Cancel Then 
        Dim pUser As csUser = _UserCol(e.RowIndex) 
        If pMessageBox = frmMessageOrInputBox.enmButtonReturned.No Then 
          Clipboard.SetText(pUser.ToCSV) 
        Else 
          Clipboard.SetText(pUser.ToString.Replace(" ‡ ", Environment.NewLine)) 
        End If 
        frmMessageOrInputBox.ShowMsg("The User is in your clipboard", frmMessageOrInputBox.enmIconType.Information) 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvUser_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvUser.MouseDown 
    '--- Save anchor on normal click (no modifiers) ---
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And (Keys.Shift Or Keys.Control)) = 0 AndAlso dgvUser.CurrentRow IsNot Nothing Then 
      _SelectionAnchor = dgvUser.CurrentRow.Index 
    End If 
    'This removes on click from the update 
    If dgvUser.EditMode = DataGridViewEditMode.EditProgrammatically Then Exit Sub 
 
    Dim pCell As DataGridView.HitTestInfo = dgvUser.HitTest(e.X, e.Y) 
 
    If pCell.Type = DataGridViewHitTestType.Cell Then 
      'Enable edit force it to be current 
      Dim pCurrentCell As DataGridViewCell = Nothing 
      Try 
        pCurrentCell = dgvUser(pCell.ColumnIndex, pCell.RowIndex) 
        If pCurrentCell.ReadOnly Then Exit Sub 
        dgvUser.CurrentCell = pCurrentCell 
      Catch ex As Exception 
        Exit Sub  
      End Try 
      'make the combobox drop down if it's active 
      If pCurrentCell.GetType().Name.Equals("DataGridViewComboBoxCell", StringComparison.OrdinalIgnoreCase) Then 
        dgvUser.BeginEdit(True) 
        DirectCast(dgvUser.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True 
      End If 
    End If 
  End Sub 
 
  Private Sub dgvUser_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvUser.MouseUp 
    Dim pModifiers = Control.ModifierKeys 
    If (pModifiers And Keys.Shift) = Keys.Shift AndAlso _SelectionAnchor >= 0 Then 
      Dim hit = dgvUser.HitTest(e.X, e.Y) 
      If hit.RowIndex >= 0 Then 
        dgvUser.MultiSelect = True 
        dgvUser.ClearSelection() 
        Dim pFrom As Integer = Math.Min(_SelectionAnchor, hit.RowIndex) 
        Dim pTo As Integer = Math.Max(_SelectionAnchor, hit.RowIndex) 
        For i As Integer = pFrom To pTo 
          dgvUser.Rows(i).Selected = True 
        Next 
      End If 
    ElseIf (pModifiers And Keys.Control) = Keys.Control Then 
      dgvUser.MultiSelect = True 
    Else 
      dgvUser.MultiSelect = False 
    End If 
  End Sub 
 
  Private Sub chkAutoRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoRefresh.CheckedChanged 
    If chkAutoRefresh.Checked Then 
      _PrevSortColumn = Nothing 
 
      Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(colID.Index, -1, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton2, 0, 0, 0, 0)) 
      dgvUser_ColumnHeaderMouseClick(Me, pE) 
      Application.DoEvents() 
      dgvUser_ColumnHeaderMouseClick(Me, pE) 
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
 
  Private Sub ctlc_UserCol_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged 
    'Set the font for the BN 
        If MyFont Is Nothing Then Return 
    BN.Font = New Font(MyFont.Name, MyFont.Size) 
    dgvUser.RowTemplate.Height = ccHelper.ToInteger(23 * MyFont.Size / 9) 
  End Sub 
 
  Private Sub ctlc_UserCol_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged 
    If Me.ParentForm Is Nothing Then Exit Sub 
    Dim pParent As String = Me.ParentForm.Name 
    Dim pResponse As Boolean = Me.Visible 
    Dim pSize As Integer = dgvUser.Width 
 
    'now set sizes if needed 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_UserCol_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged 
    If _GridSettings IsNot Nothing AndAlso Me.Visible = True AndAlso Not Me.Parent.Name.StartsWith("pnl", StringComparison.OrdinalIgnoreCase) Then 
      If _GridSettings(0).ColumnWidth = 5 Then 
        mnuColsReset_Click(New System.Object, New System.EventArgs) 
      End If 
    End If 
  End Sub 
 
  Private Sub ctlc_UserCol_Leave(sender As Object, e As EventArgs) Handles Me.Leave 
    If _Requester Is Nothing Then Return 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    DoCeaseEdit() 
  End Sub 
  'For ID in XXX 
  Private _Global As clsComboList 
  Private _Users As clsComboList 
  Private _Customers As clsComboList 
  Private _UD As clsComboList 
  'For ID in XXX 
 
  Private Sub cc_evtBeforeLoad() Handles Me.evtBeforeLoad 
    _LoadParameters.SummarizeGrid = False 
    _LoadParameters.ReadOnly = True 
    BN.Items.Remove(btnAdd) 
 
    'For ID in XXX 
    Dim pFault As clsFault 
    Dim pPrompt As String 
    pPrompt = GetChoose(_Requester) 
 
    _Users = New clsComboList 
    pFault = _Users.Fill(clsEnums.enmComboListType.c_UserDefaultByID, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    _Users.SortByText() 
    _Users.AddToTop(ccHelper.ToLong(0), pPrompt) 
 
    _Customers = New clsComboList 
    pFault = _Customers.Fill(clsEnums.enmComboListType.ccCustomerDefaultByID, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    _Customers.SortByText() 
    _Customers.AddToTop(ccHelper.ToLong(0), pPrompt) 
 
    _Global = New clsComboList 
    _Global.AddToTop(ccHelper.ToLong(0), pPrompt) 
 
    _UD = New clsComboList 
    _UD.AddToTop(ccHelper.ToLong(0), pPrompt) 
 
    colIDinType.Tag = pPrompt 
    'For ID in XXX 
 
    If _LoadParameters.IsSumFillOnTheFly Then Exit Sub 
 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.FirstName) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.LastName) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.NationalIDNo) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.Address) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.City) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.Applications) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.ProvinceState) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.PostalCode) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.PasswordHashed) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.DatePasswordChanged) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.DateActivated) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.Comments) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.LastPasswords) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.Email) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.ExpiryDate) 
 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.LoggedInIP) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.ApprovalCodeHashed) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.ApprovalFunctionName) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.ApprovalTime) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.SecurityQuestion1) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.SecurityQuestion1Response) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.SecurityQuestion2) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.SecurityQuestion2Response) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.SecurityQuestion3) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.SecurityQuestion3Response) 
    _LoadParameters.ColumnsHide.Add(csUser.enmProperty.PIN) 
 
  End Sub 
 
  'Track open detail windows to prevent duplicates 
  Private Shared _openDetailWindows As New Dictionary(Of String, Form)() 
 
  'Context menu - right-click: add to selection if not already selected, otherwise keep multi-selection 
  Private Sub dgvUser_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvUser.CellMouseDown 
    ReleaseStuckModifierKeys() 'Fix sticky SHIFT/CTRL before selection changes 
    If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then 
      If Not dgvUser.Rows(e.RowIndex).Selected Then 
        dgvUser.ClearSelection() 
        dgvUser.Rows(e.RowIndex).Selected = True 
      End If 
    End If 
  End Sub 
 
  'Context menu - Opening: adjust items based on single/multi selection 
  Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening 
    Dim pCount As Integer = dgvUser.SelectedRows.Count 
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
    If dgvUser.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvUser.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _UserCol.Count Then Exit Sub 
    Dim pUser As csUser = _UserCol(pRowIndex) 
    Dim pTitle As String = "User #" & pUser.ID.ToString() 
    Dim pKey As String = "User_" & pUser.ID.ToString() 
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
    Dim pCtlName As String = "ctlc_User" 
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
    Dim pFault As clsFault = CType(pLoad.Invoke(pControl, New Object() {pUser, _Requester}), clsFault) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) : Exit Sub 
    'Return to Tab click handler - sends entity to a new tab in frmMain 
    Dim pEntityRef As Object = pUser 
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
    If dgvUser.SelectedRows.Count <> 1 Then Exit Sub 
    Dim pRowIndex As Integer = dgvUser.SelectedRows(0).Index 
    If pRowIndex < 0 OrElse pRowIndex >= _UserCol.Count Then Exit Sub 
    Dim pUser As csUser = _UserCol(pRowIndex) 
    Dim pFrmMain As frmMain = Nothing 
    For Each pForm As Form In Application.OpenForms 
      If TypeOf pForm Is frmMain Then 
        pFrmMain = CType(pForm, frmMain) 
        Exit For 
      End If 
    Next 
    If pFrmMain Is Nothing Then Exit Sub 
    'Check if already open in a window - if so, bring to front instead 
    Dim pWinKey As String = "User_" & pUser.ID.ToString() 
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
    Dim pTabTitle As String = "User #" & pUser.ID.ToString() 
    Dim pFault As clsFault = pFrmMain.OpenEntityInNewTab("ctlc_User", pUser, _Requester, pTabTitle) 
    If Not pFault.isOK Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Context menu - Copy ID (supports multi-select) 
  Private Sub tsmiCopyID_Click(sender As Object, e As EventArgs) Handles tsmiCopyID.Click 
    If dgvUser.SelectedRows.Count = 0 Then Exit Sub 
    Dim pIDs As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvUser.SelectedRows 
      If pSelectedRow.Index >= 0 AndAlso pSelectedRow.Index < _UserCol.Count Then 
        Dim pUser As csUser = _UserCol(pSelectedRow.Index) 
        If pIDs.Length > 0 Then pIDs.Append(", ") 
        pIDs.Append(pUser.ID.ToString()) 
      End If 
    Next 
    If pIDs.Length > 0 Then 
      Clipboard.SetText(pIDs.ToString()) 
      Dim pCount As Integer = dgvUser.SelectedRows.Count 
      ShowToast(If(pCount = 1, "ID copied: " & pIDs.ToString(), pCount.ToString() & " IDs copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows (supports multi-select, values only) 
  Private Sub tsmiCopyRow_Click(sender As Object, e As EventArgs) Handles tsmiCopyRow.Click 
    If dgvUser.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    For Each pSelectedRow As DataGridViewRow In dgvUser.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvUser.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row copied", pCount.ToString() & " rows copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy Rows with Headers (supports multi-select) 
  Private Sub tsmiCopyRowHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopyRowHeaders.Click 
    If dgvUser.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers from first row 
    Dim pFirstRow As DataGridViewRow = dgvUser.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add all selected rows 
    For Each pSelectedRow As DataGridViewRow In dgvUser.SelectedRows 
      For Each pCell As DataGridViewCell In pSelectedRow.Cells 
        If pCell.OwningColumn.Visible Then pSB.Append(If(pCell.Value IsNot Nothing, pCell.Value.ToString(), "")).Append(vbTab) 
      Next 
      If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
      pSB.AppendLine() 
    Next 
    If pSB.Length > 0 Then 
      Clipboard.SetText(pSB.ToString().TrimEnd()) 
      Dim pCount As Integer = dgvUser.SelectedRows.Count 
      ShowToast(If(pCount = 1, "Row with headers copied", pCount.ToString() & " rows with headers copied")) 
    End If 
  End Sub 
 
  'Context menu - Copy for Excel (with headers, VARCHAR fields wrapped in ="value" to preserve leading zeros) 
  Private Sub tsmiCopyExcel_Click(sender As Object, e As EventArgs) Handles tsmiCopyExcel.Click 
    If dgvUser.SelectedRows.Count = 0 Then Exit Sub 
    Dim pSB As New System.Text.StringBuilder() 
    'Add headers 
    Dim pFirstRow As DataGridViewRow = dgvUser.SelectedRows(0) 
    For Each pCell As DataGridViewCell In pFirstRow.Cells 
      If pCell.OwningColumn.Visible Then pSB.Append(pCell.OwningColumn.HeaderText).Append(vbTab) 
    Next 
    If pSB.Length > 0 AndAlso pSB.Chars(pSB.Length - 1) = vbTab Then pSB.Remove(pSB.Length - 1, 1) 
    pSB.AppendLine() 
    'Add rows with Excel-safe formatting for text columns 
    For Each pSelectedRow As DataGridViewRow In dgvUser.SelectedRows 
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
      Dim pCount As Integer = dgvUser.SelectedRows.Count 
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
