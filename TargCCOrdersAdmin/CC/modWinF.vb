Imports System.Runtime.CompilerServices  
 
Public Module modWinF  
 
  Public Structure strPermissions   
    Public CanView As Boolean   
    Public CanAdd As Boolean   
    Public CanUpdate As Boolean   
    Public CanDelete As Boolean   
  End Structure   
   
  Public Property MyCache As Cache   
  Public Property MyFont As Font   
   
  Public Property LocalizedTextLanguage As clsEnums.enmLanguage   
   
  Public Sub ShowFault(ByVal vFault As clsFault, ByVal vRequester As clsRequester)   
  
    If vFault.isOK = True Then 
      frmMessageOrInputBox.ShowMsg("Completed Successfully", frmMessageOrInputBox.enmIconType.Information) 
    Else 
      If vFault.Severity <> clsEnums.enmFaultSeverity.LogOnly Then 
        Dim pIconType As frmMessageOrInputBox.enmIconType = frmMessageOrInputBox.enmIconType.Exclamation 
        If vFault.Severity = clsEnums.enmFaultSeverity.Info Then 
          pIconType = frmMessageOrInputBox.enmIconType.Information 
        End If 
        Dim pMessage As String = "" 
        If (vRequester IsNot Nothing AndAlso (vRequester.IsInRole("Master") = True OrElse vRequester.IsInRole("ApplicationMaster") = True)) OrElse (vFault.LoggedAlertID = 0 AndAlso vFault.Severity <> clsEnums.enmFaultSeverity.Info) Then 
          If vFault.StringForMessageBox.IndexOf("18456") > -1 Then 'Login failed for user ....    
            pMessage = "Critical error on server. Check server logs." 
            frmMessageOrInputBox.ShowMsg(pMessage, frmMessageOrInputBox.enmIconType.CriticalError) 
          Else 
            pMessage = vFault.Message & Environment.NewLine & vFault.Action & Environment.NewLine & Environment.NewLine & vFault.FreeText.Replace(ccHelper.NewLine, Environment.NewLine) & Environment.NewLine & Environment.NewLine & "Alert Type: " & vFault.Number & Environment.NewLine & "Record No: " & vFault.LoggedAlertID & Environment.NewLine & vFault.Ident 
            Dim pFrm As New frmMessageOrInputBox() 
            pFrm.ShowMsg(pMessage, pIconType) 
          End If 
        Else 
          If vFault.Severity = clsEnums.enmFaultSeverity.Info Then 
            If vFault.Number = 2 Then 
              pMessage = vFault.FreeText & Environment.NewLine 
            Else 
              pMessage = vFault.Message & Environment.NewLine & vFault.Action & Environment.NewLine & Environment.NewLine & "Alert Type: " & vFault.Number & Environment.NewLine & vFault.Ident 
            End If 
          Else 
            pMessage = vFault.Message & Environment.NewLine & vFault.Action & Environment.NewLine & Environment.NewLine & "Alert Type: " & vFault.Number & Environment.NewLine & "Record No: " & vFault.LoggedAlertID & Environment.NewLine & vFault.Ident 
          End If 
          Dim pFrm As New frmMessageOrInputBox() 
          pFrm.ShowMsg(pMessage, pIconType) 
        End If  
        If Debugger.IsAttached Then 
          pMessage = vFault.StringForMessageBox.Replace(ccHelper.NewLine, Environment.NewLine) 
          frmMessageOrInputBox.ShowMsg("!! Also showing full error (for your convenience) because we are in debug mode !!" & vbNewLine & vFault.StringForMessageBox, pIconType) 
        End If 
      End If 
    End If 
  
    If vFault.Number = 103 OrElse 
        vFault.Number = 104 OrElse 
        vFault.Number = 105 OrElse 
        vFault.Number = 109 Then 
      Environment.Exit(0) 
    End If 
 
  End Sub   
   
  Public Function GetChoose(ByVal vRequester As clsRequester) As String 
    Static pChoose As String 
 
    If pChoose = "" Then 
      pChoose = ccHelper.GetChoose(vRequester) 
    End If 
 
    Return pChoose 
  End Function 
 
  Private _LoggedRowHeader As Text.StringBuilder = Nothing 
   
  Public Function LoadCbo(ByRef rCbo As ComboBox, ByRef rList As clsComboList, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    If rList.Count = 0 Then 
      rCbo.DataSource = Nothing 
      Return pFault.SetOK() 
    End If 
 
    Dim pCreateHeader As Boolean = (_LoggedRowHeader Is Nothing) 
    Dim sw As Stopwatch = Nothing 
    Dim pLoggedRow As Text.StringBuilder = Nothing 
 
    Dim pLogDAL As Boolean = MyController.LogDetails 
 
    If pLogDAL = True Then 
      sw = New Stopwatch 
      pLoggedRow = New Text.StringBuilder 
      If pCreateHeader = True Then 
        _LoggedRowHeader = New Text.StringBuilder 
        _LoggedRowHeader.Append(", ComboBox, CountInList, ") 
      End If 
      Dim pParentFormName As String 
      Dim pParent As Control = rCbo 
      Do 
        pParent = pParent.Parent 
        If pParent Is Nothing Then 
          pParentFormName = rCbo.Parent.Name & " Default" 
          Exit Do 
        Else 
          pParentFormName = pParent.Name 
        End If 
      Loop Until pParentFormName.IndexOf("_") > 0 
      pLoggedRow.Append(String.Format(", {0}, {1}, ", pParentFormName & ":" & rCbo.Name, rList.Count)) 
      sw.Start() 
    End If 
 
 
    Dim pComboCol As clsComboList = rList.Clone() 
 
    If pLogDAL = True Then 
      sw.Stop() 
      If pCreateHeader = True Then _LoggedRowHeader.Append("Clone, ") 
      pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds)) 
      sw.Restart() 
    End If 
 
    Dim pAddChoose As Boolean = True 
 
    Dim pComboMember As New clsComboListMember 
    pComboMember.Text = GetChoose(vRequester) 
    If pComboMember.Text.Length > 100 Then 
      Return pFault.LogFreeTextFault("Problem Getting Choose Translation", pComboMember.Text, "TRGT-160329-1011", vRequester) 
    End If 
 
    If pLogDAL = True Then 
      sw.Stop() 
      If pCreateHeader = True Then _LoggedRowHeader.Append("GetChoose, ") 
      pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds)) 
      sw.Restart() 
    End If 
 
    If rList.Count > 0 Then 
      If rList.KeyType = clsEnums.enmComboListKeyType.String Then 
        If pComboCol(0).KeyString = "" OrElse pComboCol(0).KeyString = "-1" Then 
          pAddChoose = False 
        End If 
        If CStr(rCbo.Tag) = "Numeric" Then 
          pComboMember.KeyString = "-1" 
        Else 
          pComboMember.KeyString = "" 
        End If 
        rCbo.ValueMember = "KeyString" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Long Then 
        If pComboCol(0).KeyLong = -1 Then 
          pAddChoose = False 
        End If 
        pComboMember.KeyLong = -1 
        rCbo.ValueMember = "KeyLong" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Integer Then 
        If pComboCol(0).KeyInteger = -1 Then 
          pAddChoose = False 
        End If 
        pComboMember.KeyInteger = -1 
        rCbo.ValueMember = "KeyInteger" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Enum Then 
        If CStr(pComboCol(0).KeyEnum.ToString()).Equals("UD", StringComparison.OrdinalIgnoreCase) OrElse CStr(pComboCol(0).KeyEnum.ToString()).Equals("undefined", StringComparison.OrdinalIgnoreCase) Then 
          pAddChoose = False 
        End If 
        pComboMember.KeyEnum = clsEnums.enmEnum.UD 
        rCbo.ValueMember = "KeyEnum" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Object Then 
        If ccHelper.ToInteger(pComboCol(0).KeyObject) = -1 Then 
          pAddChoose = False 
        End If 
        rCbo.ValueMember = "KeyObject" 
      End If 
    Else 
      pComboMember.KeyLong = -1 
      rCbo.ValueMember = "KeyLong" 
    End If 
 
    If pAddChoose = True Then 
      'pComboCol.SortByText()  
      pComboCol.Insert(0, pComboMember) 
    End If 
 
    rCbo.DisplayMember = "Text" 
    rCbo.DataSource = pComboCol 
 
    If pLogDAL = True Then 
      sw.Stop() 
      If pCreateHeader = True Then _LoggedRowHeader.Append("Loaded, ") 
      pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds)) 
      sw.Restart() 
    End If 
 
    If pLogDAL = True Then 
      If pCreateHeader = True Then Tools.LogToTextFile.WriteMessage(_LoggedRowHeader.ToString() & " times in ms", "LoadCboTimes") 
      Tools.LogToTextFile.WriteMessage(pLoggedRow.ToString(), "LoadCboTimes") 
    End If 
 
    pFault.SetOK() : Return pFault 
  End Function 
 
  Public Function LoadLst(ByRef rLst As ListBox, ByRef rList As clsComboList, Optional ByVal vSortText As Boolean = True) As clsFault   
    Dim pFault As New clsFault   
   
    If rList.Count = 0 Then Return pFault.SetOK() 
 
    Dim pComboCol As clsComboList = rList.Clone()   
  
    If vSortText = True Then pComboCol.SortByText()  
  
    rLst.DataSource = pComboCol  
  
    If rList.Count > 0 Then 
      If rList.KeyType = clsEnums.enmComboListKeyType.String Then 
        rLst.ValueMember = "KeyString" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Long Then 
        rLst.ValueMember = "KeyLong" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Integer Then 
        rLst.ValueMember = "KeyInteger" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Object Then 
        rLst.ValueMember = "KeyObject" 
      End If 
    Else 
      rLst.ValueMember = "KeyLong" 
    End If 
 
    rLst.DisplayMember = "Text" 
 
    rLst.SetSelected(0, False) 
 
    pFault.SetOK() : Return pFault 
  End Function  
   
  'CheckList Functions 
  Public Function LoadChl(ByRef rChl As CheckedListBox, ByRef rList As clsComboList, Optional ByVal vSortText As Boolean = True) As clsFault 
    Dim pFault As New clsFault 
 
    If rList.Count = 0 Then Return pFault.SetOK() 
 
    Dim pComboCol As clsComboList = rList.Clone() 
 
    If vSortText = True Then pComboCol.SortByText() 
 
    If rList.Count > 0 Then 
      If rList.KeyType = clsEnums.enmComboListKeyType.String Then 
        rChl.ValueMember = "KeyString" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Long Then 
        rChl.ValueMember = "KeyLong" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Integer Then 
        rChl.ValueMember = "KeyInteger" 
      ElseIf rList.KeyType = clsEnums.enmComboListKeyType.Object Then 
        rChl.ValueMember = "KeyObject" 
      End If 
    Else 
      rChl.ValueMember = "KeyLong" 
    End If 
 
    rChl.DisplayMember = "Text" 
    rChl.DataSource = pComboCol 
 
    rChl.DisplayMember = "Text" 'to make the text appear  
 
    'make sure it's cleared 
    For i As Integer = 0 To rChl.Items.Count - 1 
      rChl.SetItemChecked(i, False) 
    Next 
 
    pFault.SetOK() : Return pFault 
  End Function 
  Friend Function GetCheckedItemsInChl(vLst As CheckedListBox) As clsComboList 
    Dim pComboList As New clsComboList 
    For i As Integer = 0 To vLst.Items.Count - 1 
      Dim pState As CheckState = vLst.GetItemCheckState(i) 
      If pState <> CheckState.Unchecked Then 
        Dim pMember As clsComboListMember = DirectCast(vLst.Items(i), clsComboListMember) 
        pMember.Tag = pState.ToString() 
        pComboList.Add(pMember) 
      End If 
    Next 
    Return pComboList 
  End Function 
  Friend Function CreateTextFromComboListInChl(vComboList As clsComboList, ByRef rIsMixed As Boolean) As String 
    rIsMixed = False 
    Dim pText As String 
 
    Dim pIDs As New Text.StringBuilder 
    Dim pType As String = "" 
    For Each l In vComboList 
      Dim pSign As String = "+" 
      If l.Tag <> "Checked" Then pSign = "-" 
      If pType = "" Then 
        pType = pSign 
      Else 
        If pType <> pSign Then rIsMixed = True 
      End If 
      pIDs.Append($"{pSign}{l.KeyLong}, ") 
    Next 
    pText = pIDs.ToString().Substring(0, pIDs.ToString().Length - 2) 
 
    Return pText 
  End Function 
  Friend Function GetCheckStatusByKeyInChl(vLst As CheckedListBox, vKey As Long) As CheckState 
    For i As Integer = 0 To vLst.Items.Count - 1 
      If DirectCast(vLst.Items(i), clsComboListMember).KeyLong = vKey Then 
        Return vLst.GetItemCheckState(i) 
      End If 
    Next 
    Return CheckState.Unchecked 
  End Function 
  Friend Sub SetCheckStatusByKeyInChl(vLst As CheckedListBox, vKey As Long) 
    Dim pID As Long = Math.Abs(vKey) 
    Dim pCheckState As CheckState = CheckState.Checked : If vKey < 0 Then pCheckState = CheckState.Indeterminate 
    For i As Integer = 0 To vLst.Items.Count - 1 
      If DirectCast(vLst.Items(i), clsComboListMember).KeyLong = pID Then 
        vLst.SetItemCheckState(i, pCheckState) 
      End If 
    Next 
  End Sub 
  Friend Sub ToggleCheckInChl(e As ItemCheckEventArgs) 
    Select Case e.CurrentValue 
      Case CheckState.Checked 
        e.NewValue = CheckState.Indeterminate 
      Case CheckState.Unchecked 
        e.NewValue = CheckState.Checked 
      Case CheckState.Indeterminate 
        e.NewValue = CheckState.Unchecked 
    End Select 
  End Sub 
 
  Public Sub TextBoxHandleNumericalKeyPress(ByVal vTextBox As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)  
    Dim pTextBox As TextBox = CType(vTextBox, TextBox)  
    If Char.IsControl(e.KeyChar) = True Then  
      Exit Sub  
    End If  
    If e.KeyChar = "-"c Then Exit Sub  
    If Not ccHelper.IsNumeric(pTextBox.Text & e.KeyChar) Then  
      e.Handled = True  
    End If  
  End Sub  
  
  Public Function FormatFromTag(ByVal vTextBox As TextBox, ByVal vDefaultFormat As String) As String 
    Dim pFormat As String = "" 
 
    If vTextBox.Tag IsNot Nothing AndAlso vTextBox.Tag.ToString.Length > 0 Then 
      pFormat = vTextBox.Tag.ToString().Split(";"c)(0) 
    Else 
      pFormat = vDefaultFormat 
    End If 
 
    Return pFormat 
  End Function 
  Public Function FormattedDateTimeOffsetFromTag(ByVal vTextBox As TextBox, ByVal vDateTimeOffset As DateTimeOffset) As String 
    Dim pTagObj As Object = vTextBox.Tag 
 
    Dim pTag As String = "" 
 
    If vTextBox.Tag IsNot Nothing AndAlso vTextBox.Tag.ToString.Length > 0 Then 
      pTag = pTagObj.ToString() 
    End If 
 
    Return FormattedDateTimeOffsetFromGridCellStyle(pTag, vDateTimeOffset) 
 
  End Function 
 
  Public Function FormattedDateTimeOffsetFromGridCellStyle(ByVal vFormatting As String, ByVal vDateTimeOffset As DateTimeOffset) As String 
    Dim pFormat As String = "" 
    Dim pDateType As String = "" 
 
 
    If Math.Abs(vDateTimeOffset.Subtract(DateTimeOffset.MinValue).TotalDays) < 5 OrElse Math.Abs(vDateTimeOffset.Subtract(DateTimeOffset.MaxValue).TotalDays) < 5 Then 
      Return "" 
    End If 
 
    Dim pTags As String() = vFormatting.Split(";"c) 
 
    pFormat = "dd-MM-yyyy HH:mm:ss zzz" 
    pDateType = "" 
 
    If Not String.IsNullOrEmpty(vFormatting) AndAlso pTags.Length = 1 Then 
      pFormat = pTags(0) 
    ElseIf pTags.Length > 1 Then 
      pFormat = pTags(0) 
      pDateType = pTags(1) 
    End If 
 
    If pDateType.Equals("", StringComparison.OrdinalIgnoreCase) Then 
      Return vDateTimeOffset.ToString(pFormat) 
    ElseIf pDateType.Equals("B", StringComparison.OrdinalIgnoreCase) Then 
      If vDateTimeOffset.Offset = DateTimeOffset.Now.Offset Then 
        Return vDateTimeOffset.ToLocalTime.ToString(pFormat) 
      Else 
        Return vDateTimeOffset.ToLocalTime.ToString(pFormat) & " (☼: " & vDateTimeOffset.DateTime.ToString(pFormat) & ")" 
      End If 
    ElseIf pDateType.Equals("L", StringComparison.OrdinalIgnoreCase) Then 
      Return vDateTimeOffset.ToLocalTime.ToString(pFormat) 
    ElseIf pDateType.Equals("R", StringComparison.OrdinalIgnoreCase) Then 
      If vDateTimeOffset.Offset = DateTimeOffset.Now.Offset Then 
        Return vDateTimeOffset.DateTime.ToString(pFormat) 
      Else 
        Return vDateTimeOffset.DateTime.ToString(pFormat) & "☼" 
      End If 
    ElseIf pDateType.Equals("U", StringComparison.OrdinalIgnoreCase) Then 
      Return vDateTimeOffset.ToUniversalTime.ToString(pFormat) & "Z" 
    Else 
      Return "Invalid Date Type! Use B, L, R, U or nothing. Delimit from date format with ';'" 
    End If 
 
  End Function 
 
  Public Sub RestartMe() 
 
    Application.DoEvents() 
    My.MyApplication.ExecuteRestart() 
 
  End Sub 
    
  Public Sub UITranslate(ByVal pControl As Control, ByVal vRequester As clsRequester) 
    Dim pStrg As String 
    pStrg = ccHelper.GetLocalizedUIText("UserText", pControl.Text, vRequester) 
    If pStrg <> "" Then pControl.Text = pStrg 
 
    If vRequester.UILang <> clsEnums.enmLanguage.en Then 
      If pStrg = "" Then pControl.Text = "$" & pControl.Text & "$" 
    Else 
      If pStrg = "" Then pControl.Text = pControl.Text 
    End If 
 
  End Sub 
 
  Public Function UITranslate(ByVal pText As String, ByVal vRequester As clsRequester) As String 
    Dim pStrg As String 
    Dim pStringToReturn As String = "" 
    pStrg = ccHelper.GetLocalizedUIText("UserText", pText, vRequester) 
    If pStrg <> "" Then pStringToReturn = pStrg 
 
    If vRequester.UILang <> clsEnums.enmLanguage.en Then 
      If pStrg = "" Then pStringToReturn = "$" & pText & "$" 
    Else 
      If pStrg = "" Then pStringToReturn = pText 
    End If 
    Return pStringToReturn 
  End Function 
 
  Public Function CCTextTranslate(ByVal pText As String, ByVal vRequester As clsRequester) As String 
    Dim pStrg As String 
 
    If My.Settings.IsLocalized = True Then 
      pStrg = ccHelper.GetLocalizedUIText("CCText", pText, vRequester) 
      If pStrg = "" Then pStrg = ccHelper.CreateFriendlyTextFromHungarianNotation(pText) 
    Else 
      pStrg = ccHelper.CreateFriendlyTextFromHungarianNotation(pText) 
    End If 
 
    Return pStrg 
  End Function 
 
  Public Function TableNameTranslate(ByVal pTableName As String, ByVal vRequester As clsRequester, Optional vMakePlural As Boolean = False) As String 
    Dim pStrg As String 
 
 
    If My.Settings.IsLocalized = True Then 
      pStrg = ccHelper.GetLocalizedFieldName(pTableName, "_TableTitle", vRequester) 
      If pStrg = "" Then pStrg = ccHelper.CreateFriendlyTextFromHungarianNotation(pTableName) 
    Else 
      pStrg = ccHelper.CreateFriendlyTextFromHungarianNotation(pTableName) 
    End If 
 
    If pStrg.StartsWith("Logged") Then pStrg = pStrg.Substring(6) 
    pStrg = pStrg.Trim() 
 
    If vMakePlural = True Then 
      If vRequester.UILang = clsEnums.enmLanguage.he Then 
        Dim pStrgs As String() = pStrg.Split(" "c) 
        If pStrgs.Length = 1 Then 
          pStrg &= "ים" 
        Else 
          pStrg = "" 
          For Each l In pStrgs 
            If String.IsNullOrEmpty(pStrg) Then 
              If l.EndsWith("ת") Then 
                pStrg &= l.Substring(0, l.Length - 1) & "ות" & " " 
              Else 
                pStrg &= l & "י" & " " 
              End If 
            Else 
              pStrg &= l & " " 
            End If 
          Next 
          pStrg = pStrg.Trim 
        End If 
      Else 
        pStrg &= "s" 
      End If 
    End If 
 
    Return pStrg 
  End Function 
 
  ''' <summary> 
  ''' The prompt already includes part of the text. "Are you sure you want to " + vPrompt +"?". 
  ''' </summary> 
  ''' <param name="vPrompt"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function AreYouSure(ByVal vPrompt As String) As Boolean  
  
    Dim pRequest As String = "Are you sure you want to " & vPrompt & "?"  
  
    Dim pTest As String = DateTime.Now.ToString("ssff")   
    If frmMessageOrInputBox.GetInput(pRequest & Environment.NewLine & Environment.NewLine & "Please type in '" & pTest & "' to confirm or cancel to exit.") <> pTest Then 
      vPrompt = UCase(vPrompt.Substring(0, 1)) & vPrompt.Substring(1) 
      frmMessageOrInputBox.ShowMsg("'" & vPrompt & "' cancelled.", frmMessageOrInputBox.enmIconType.Exclamation) 
      Return False 
    End If 
  
    Return True  
  End Function  
 
  Public Function IsOnScreen(ByVal form As Form) As Boolean 
    Dim screens() As Screen = Screen.AllScreens 
 
    For Each scrn As Screen In screens 
      Dim formRectangle As Rectangle = New Rectangle(form.Left, form.Top, form.Width, form.Height) 
 
      If scrn.WorkingArea.IntersectsWith(formRectangle) Then 
        Return True 
      End If 
    Next 
 
    Return False 
  End Function 
 
  Friend Function LoadInitialCache(ByVal vRequester As clsRequester) As clsFault 
 
    'Initiate the cache  
    MyCache = New Cache(vRequester) 
 
    Dim pFault As clsFault 
    Dim pStart As Double = DateAndTime.Timer 
    vRequester.CallingFunctionWithinApplication = "modWinF:LoadInitialCache" 
 
    'Load comboLists that you'd like preloaded.  
 
    MyCache.SetLevel(clsEnums.enmComboListType.c_RoleDefaultByID, Cache.enmLevel.AlwaysCache) 
    MyCache.SetLevel(clsEnums.enmComboListType.c_ProcessDefaultByID, Cache.enmLevel.AlwaysCache) 
 
    'Due to Always Encrypt (can't sort in the database
 
    'Create a partial module of modWinF and add the function below    
    ' - If you want to 'PageFromServer", i.e. you don't want to load all the items to the combolist, and want to query the   
    '   database to get the handful of items at a time, then set the level in MyCache to PageFromServer.    
    ' - If you want it to load all the items to the combolist, set it to AlwaysCache   
    ' - If you don't set it, then it will default to Auto (PageFromServer if over 100 items it, otherwise AlwaysCache)  
 
    'Private Sub LoadInitialCacheManualAdditions()  
    '  MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.AlwaysPageFromServer)  
    '  MyCache.SetLevel(clsEnums.enmComboListType.c_AlertMessageDefaultByID, Cache.enmLevel.AlwaysPageFromServer)  
    '  MyCache.SetComboListParentID(clsEnums.enmComboListType.ccItemAtPartnerForPartnerDefaultByID, 0)  
    'End Sub  
 
    LoadInitialCacheManualAdditions() 
 
    If MyCache.Levels IsNot Nothing AndAlso MyCache.Levels.Count > 0 Then 
      pFault = MyCache.LoadLists() : If Not pFault.isOK() Then Return pFault 
    Else 
      pFault = New clsFault() 
      pFault.SetOK() 
    End If 
 
    Dim pStop As Double = DateAndTime.Timer 
    If MyController.LogDetails Then 
      Dim pElapsed As Double = pStop - pStart 
      Tools.LogToTextFile.WriteMessage("Done! It took " & pElapsed.ToString("#,##0.0000") & " sec. ", "InitialCache") 
    End If 
 
    Return pFault 
 
 
  End Function 
 
  <Extension> 
  Public Sub DoubleBuffered(ByVal dgv As DataGridView, ByVal setting As Boolean) 
    'Fixing a slow scrolling DataGridView 
    '  http://bitmatic.com/c/fixing-a-slow-scrolling-datagridview 
    'Horrible redraw performance of the DataGridView on one of my two screens (explains Remote Desktop problem) 
    '  https://stackoverflow.com/questions/118528/horrible-redraw-performance-of-the-datagridview-on-one-of-my-two-screens 
    If System.Windows.Forms.SystemInformation.TerminalServerSession = True Then Exit Sub 
    Dim dgvType As Type = dgv.[GetType]() 
    Dim pi As System.Reflection.PropertyInfo = dgvType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic) 
    pi.SetValue(dgv, setting, Nothing) 
  End Sub 
 
  Friend Sub MakeControlRTL(ctl As Control) 
    If Not (My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he") Then Return 
 
    For Each item As Control In ctl.Controls 
      Try 
        item.RightToLeft = RightToLeft.Yes 
        item.Location = New System.Drawing.Point(ctl.Size.Width - item.Size.Width - item.Location.X, item.Location.Y) 
        If item.Dock = DockStyle.None Then 
          If item.Anchor = (AnchorStyles.Top Or AnchorStyles.Right) Then 
            item.Anchor = (AnchorStyles.Top Or AnchorStyles.Left) 
          ElseIf item.Anchor = (AnchorStyles.Top Or AnchorStyles.Left) Then 
            item.Anchor = (AnchorStyles.Top Or AnchorStyles.Right) 
          ElseIf item.Anchor = (AnchorStyles.Bottom Or AnchorStyles.Right) Then 
            item.Anchor = (AnchorStyles.Bottom Or AnchorStyles.Left) 
          ElseIf item.Anchor = (AnchorStyles.Bottom Or AnchorStyles.Left) Then 
            item.Anchor = (AnchorStyles.Bottom Or AnchorStyles.Right) 
          End If 
        ElseIf item.Dock = DockStyle.Left Then 
          item.Dock = DockStyle.Right 
        ElseIf item.Dock = DockStyle.Right Then 
          item.Dock = DockStyle.Left 
        End If 
        If item.Controls.Count > 0 Then 
          MakeControlRTL(item) 
        End If 
      Catch 
      End Try 
    Next 
  End Sub 
 
  'Toast notification - non-modal floating message 
  Public Sub ShowToast(vMessage As String, Optional vDuration As Integer = 2500) 
    Dim pToast As New Form() 
    pToast.FormBorderStyle = FormBorderStyle.None 
    pToast.StartPosition = FormStartPosition.Manual 
    pToast.ShowInTaskbar = False 
    pToast.TopMost = True 
    pToast.BackColor = Color.FromArgb(50, 50, 50) 
    pToast.Size = New Size(300, 50) 
    pToast.Opacity = 0.92 
    Dim pLbl As New Label() 
    pLbl.Text = vMessage 
    pLbl.ForeColor = Color.White 
    pLbl.Font = New Font("Segoe UI", 10, FontStyle.Regular) 
    pLbl.AutoSize = False 
    pLbl.Dock = DockStyle.Fill 
    pLbl.TextAlign = ContentAlignment.MiddleCenter 
    pToast.Controls.Add(pLbl) 
    Dim pArea = Screen.PrimaryScreen.WorkingArea 
    pToast.Location = New Point(pArea.Right - pToast.Width - 20, pArea.Bottom - pToast.Height - 20) 
    Dim pTmr As New Timer() 
    pTmr.Interval = vDuration 
    AddHandler pTmr.Tick, Sub(s, ev) 
                              pToast.Close() 
                              DirectCast(s, Timer).Dispose() 
                          End Sub 
    pToast.Show() 
    pTmr.Start() 
  End Sub 
 
End Module  
 
Public Class Cache 
 
  Private _ComboLists As Dictionary(Of clsEnums.enmComboListType, clsComboList) 
 
  Private _ComboListParentIDs As Dictionary(Of clsEnums.enmComboListType, Long) 
 
  Private _Levels As Dictionary(Of clsEnums.enmComboListType, enmLevel) 
 
  Private _Requester As clsRequester 
 
  Private _TimeCleared As DateTimeOffset 
 
  Public Enum enmLevel 
    UD 
    AlwaysPageFromServer 
    AlwaysCache 
    Auto 
    Previous 
  End Enum 
 
  Public Sub SetLevel(ByVal vListType As clsEnums.enmComboListType, ByVal vLevel As enmLevel) 
    If _Levels.ContainsKey(vListType) Then 
      If vLevel <> enmLevel.Previous Then 
        If _Levels(vListType) <> vLevel Then 
          _Levels(vListType) = vLevel 
          If _ComboLists.ContainsKey(vListType) Then _ComboLists.Remove(vListType) 'reset it   
        End If 
      Else 
        'leave it alone  
      End If 
    Else 
      If vListType = clsEnums.enmComboListType.UD Then frmMessageOrInputBox.ShowMsg($"Level received a ComboListType of UD in SetLevel", frmMessageOrInputBox.enmIconType.CriticalError) 
      If vLevel = enmLevel.Previous Then 
        _Levels.Add(vListType, enmLevel.Auto) 
      Else 
        _Levels.Add(vListType, vLevel) 
      End If 
    End If 
  End Sub 
 
  Public Sub SetComboListParentID(ByVal vListType As clsEnums.enmComboListType, ByVal vComboListParentID As Long) 
    If _ComboListParentIDs.ContainsKey(vListType) Then 
      'if it's not changed, then don't reset it 
      If _ComboListParentIDs(vListType) <> vComboListParentID Then 
        If _ComboLists.ContainsKey(vListType) Then _ComboLists.Remove(vListType) 'reset it  
        _ComboListParentIDs(vListType) = vComboListParentID 
      End If 
    Else 
      _ComboListParentIDs.Add(vListType, vComboListParentID) 
      If _ComboLists.ContainsKey(vListType) Then _ComboLists.Remove(vListType) 'reset it  
    End If 
  End Sub 
 
  Public Function GetLevel(ByVal vListType As clsEnums.enmComboListType) As enmLevel 
    If _Levels.ContainsKey(vListType) Then 
      Return _Levels(vListType) 
    Else 
      SetLevel(vListType, enmLevel.Auto) 
      Return enmLevel.Auto 
    End If 
  End Function 
 
  Public Function GetComboListParentID(ByVal vListType As clsEnums.enmComboListType) As Long 
    If _ComboListParentIDs.ContainsKey(vListType) Then 
      Return _ComboListParentIDs(vListType) 
    Else 
      SetComboListParentID(vListType, 0) 
      Return GetComboListParentID(vListType) 
    End If 
  End Function 
 
  Public ReadOnly Property Levels As Dictionary(Of clsEnums.enmComboListType, enmLevel) 
    Get 
      Return _Levels 
    End Get 
  End Property 
 
  Public ReadOnly Property ComboListParentIDs As Dictionary(Of clsEnums.enmComboListType, Long) 
    Get 
      Return _ComboListParentIDs 
    End Get 
  End Property 
 
  Public Function IsCombolistCached(ByVal vListType As clsEnums.enmComboListType) As Boolean 
    If Not _ComboLists.ContainsKey(vListType) Then 
      Return False 
    Else 
      Return True 
    End If 
  End Function 
 
  Public Function GetComboList(ByVal vListType As clsEnums.enmComboListType, ByRef rComboList As clsComboList, Optional ByVal vParentID As Long = 0) As clsFault 
    Dim pFault As clsFault 
 
    CheckTime() 
 
    'check if we have to kill the combolist 
    SetComboListParentID(vListType, vParentID) 
 
    If Not _ComboLists.ContainsKey(vListType) Then 
      pFault = LoadComboList(vListType) : If Not pFault.isOK() Then Return pFault 
    Else 
      pFault = New clsFault 
      pFault.SetOK() 
    End If 
    If vListType = clsEnums.enmComboListType.UD Then frmMessageOrInputBox.ShowMsg($"ComboLists received a ComboListType of UD in GetComboList", frmMessageOrInputBox.enmIconType.CriticalError) 
    rComboList = _ComboLists(vListType) 
    Return pFault 
  End Function 
 
  Public Sub ClearComboList(ByVal vListType As clsEnums.enmComboListType) 
    If _ComboLists.ContainsKey(vListType) Then 
      _ComboLists.Remove(vListType) 
    End If 
  End Sub 
 
  Public Sub ClearComboLists() 
    _ComboLists.Clear() 
    _TimeCleared = DateTimeOffset.Now() 
    Tools.LogToTextFile.WriteMessage($"Cleared ComboLists ", "LoadComboListCache") 
  End Sub 
 
  Public Sub New(ByVal vRequester As clsRequester) 
    'put this after login  
    If _Requester IsNot Nothing Then 
      Throw New Exception("Only one instance of the Cache can exist") 
    End If 
    _TimeCleared = DateTimeOffset.Now() 
    _Requester = vRequester 
    _ComboLists = New Dictionary(Of clsEnums.enmComboListType, clsComboList) 
    _Levels = New Dictionary(Of clsEnums.enmComboListType, enmLevel) 
    _ComboListParentIDs = New Dictionary(Of clsEnums.enmComboListType, Long) 
  End Sub 
 
  Private ReadOnly PadLock As New Object 
 
  Private _Which As Integer = 0 
 
  Public Function LoadListsPrev() As clsFault 
 
    SyncLock PadLock 
      CheckTime() 
 
      'do this do avoid numeration error 
      Dim pTaskLevels As New List(Of clsEnums.enmComboListType) 
      For Each l In _Levels 
        pTaskLevels.Add(l.Key) 
      Next 
 
      Dim pTasks(pTaskLevels.Count - 1) As Task(Of clsFault) 
      For i As Integer = 0 To pTaskLevels.Count - 1 
        Dim pComboListType As clsEnums.enmComboListType = pTaskLevels(i) 
        pTasks(i) = Task.Run(Function() 
                               Dim pFault As New clsFault() 
                               If Not _ComboLists.ContainsKey(pComboListType) Then 
                                 pFault = LoadComboList(pComboListType) 
                               Else 
                                 Return pFault.SetOK 
                               End If 
                               Return pFault 
                             End Function) 
      Next 
 
      Task.WaitAll(pTasks) 
      For Each p As Task(Of clsFault) In pTasks 
        If Not p.Result.isOK Then Return p.Result 
      Next 
      Dim pFaultToReturn As New clsFault() 
      Return pFaultToReturn.SetOK() 
    End SyncLock 
 
  End Function 
 
  Public Function LoadLists() As clsFault 
    Dim pFault As New clsFault() 
 
    SyncLock PadLock 
      CheckTime() 
 
      Dim pLevels As New List(Of clsEnums.enmComboListType) 
      For Each l In _Levels 
        pLevels.Add(l.Key) 
      Next 
 
      For Each l In pLevels 
        Dim pComboListType As clsEnums.enmComboListType = l 
        If Not _ComboLists.ContainsKey(l) Then 
          pFault = LoadComboList(pComboListType) 
        Else 
          Return pFault.SetOK() 
        End If 
        If Not pFault.isOK() Then Return pFault 
      Next 
 
      Return pFault.SetOK() 
    End SyncLock 
 
  End Function 
 
  Private ReadOnly WhichLock As New Object 
 
  Private Function LoadComboList(ByVal vComboListType As clsEnums.enmComboListType) As clsFault 
    Dim pFault As clsFault 
 
    Dim pDoLog As Boolean = MyController.LogDetails 
 
    Dim sw As Stopwatch = Nothing 
    If pDoLog Then 
      sw = New Stopwatch 
      sw.Start() 
    End If 
 
    Dim pLevel As enmLevel 
    If _Levels.ContainsKey(vComboListType) Then 
      pLevel = _Levels(vComboListType) 
    Else 
      If vComboListType = clsEnums.enmComboListType.UD Then frmMessageOrInputBox.ShowMsg($"Level received a ComboListType of UD in LoadComboList", frmMessageOrInputBox.enmIconType.CriticalError) 
      _Levels.Add(vComboListType, enmLevel.Auto) 
      pLevel = enmLevel.Auto 
    End If 
    Dim pComboListParentID As Long = 0 
    If _ComboListParentIDs.ContainsKey(vComboListType) Then 
      pComboListParentID = _ComboListParentIDs(vComboListType) 
    Else 
      _ComboListParentIDs.Add(vComboListType, 0) 
    End If 
 
    Dim pCombolist As clsComboList = Nothing 
    'check if we load form cache  
    If pLevel = enmLevel.Auto Then 
      pCombolist = New clsComboList() 
      pFault = pCombolist.Fill(vComboListType, _Requester, pComboListParentID, "", 101) : If Not pFault.isOK Then Return pFault 
      If pCombolist.Count = 101 Then 
        pLevel = enmLevel.AlwaysPageFromServer 
        pCombolist = Nothing 
      Else 
        pLevel = enmLevel.AlwaysCache 
      End If 
    ElseIf pLevel = enmLevel.AlwaysPageFromServer Then 
      pFault = New clsFault 
      pFault.SetOK() 
    Else 'If pLevel = enmLevel.AlwaysCache Then 
      pCombolist = New clsComboList() 
      pFault = pCombolist.Fill(vComboListType, _Requester, pComboListParentID) : If Not pFault.isOK Then Return pFault 
    End If 
 
    If _ComboLists.ContainsKey(vComboListType) Then 
      _ComboLists(vComboListType) = pCombolist 
    Else 
      Try 
        If vComboListType = clsEnums.enmComboListType.UD Then frmMessageOrInputBox.ShowMsg($"Combolist received a ComboListType of UD in LoadComboList", frmMessageOrInputBox.enmIconType.CriticalError) 
        If _ComboLists Is Nothing Then 
          Tools.LogToTextFile.WriteMessage($"LoadComboList failed while loading {vComboListType.FastToString()}, since _ComboLists is Nothing", "LoadComboListCache") 
          If Debugger.IsLogging Then frmMessageOrInputBox.ShowMsg($"LoadComboList failed while loading {vComboListType.FastToString()}, since _ComboLists is Nothing", frmMessageOrInputBox.enmIconType.Exclamation) 
          Return pFault 
        End If 
        _ComboLists.Add(vComboListType, pCombolist) 
      Catch ex As Exception 
        Dim pNow As DateTimeOffset = DateTimeOffset.Now 
        Tools.LogToTextFile.WriteMessage($"LoadComboList failed while loading {vComboListType.FastToString()}. Now - TimeCleared (ms): {pNow.Subtract(_TimeCleared).TotalMilliseconds} ", "LoadComboListCache") 
        pFault.LogException(ex, $"LoadComboList failed while loading {vComboListType.FastToString()}. Now - TimeCleared (ms): {pNow.Subtract(_TimeCleared).TotalMilliseconds} ", "TRGT-200801-1940", _Requester) 
        Throw (ex) 
      End Try 
    End If 
    _Levels(vComboListType) = pLevel 
 
    If pDoLog Then 
      sw.Stop() 
      Tools.LogToTextFile.WriteMessage($"{vComboListType.FastToString()}, Level {pLevel}. Elapsed.TotalMilliseconds: {sw.Elapsed.TotalMilliseconds}", "LoadComboListCache") 
    End If 
 
    Return pFault 
  End Function 
 
  Private ReadOnly TimeLock As New Object 
 
  Private Sub CheckTime() 
 
    If DateTimeOffset.Now.Subtract(_TimeCleared).TotalMinutes > MyController.CacheKeepAliveMin Then 
      SyncLock TimeLock 
        If DateTimeOffset.Now.Subtract(_TimeCleared).TotalMinutes > MyController.CacheKeepAliveMin Then 
          ClearComboLists() 
        End If 
      End SyncLock 
    End If 
 
  End Sub 
 
End Class  
 
