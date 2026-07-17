Public Class ctlPnlcsDBMaintenance

  Private _Requester As clsRequester

  Public Event evtBeforeLoad()
  Public Event evtLoaded()

  Private Sub ctl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Me.DesignMode = True Then Exit Sub


    Me.Visible = False
  End Sub

  Public Function LoadControl(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault

    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont
    Me.PerformAutoScale()

    _Requester = vRequester

    RaiseEvent evtBeforeLoad()

    If (_Requester.IsInRole("Master") OrElse _Requester.IsInRole("ApplicationMaster") OrElse _Requester.IsInRole("SysAdmin")) Then
      gpbCreateBinaryFilesOnServer.Visible = True
      gbpDBMaintenance.Visible = True
    End If

    'Hide the grids
    grdIndexFragmentation.Dock = DockStyle.Fill
    grdTableSizes.Dock = DockStyle.Fill
    txtDatabaseFileSizes.Dock = DockStyle.Fill

    grdTableSizes.Visible = False
    grdIndexFragmentation.Visible = False
    txtDatabaseFileSizes.Visible = False

    If MyController.IsSQLUserSysAdmin OrElse MyController.IsSQLUserDBOwner Then
      gpbSysAdmin.Visible = True
      If MyController.IsSQLUserSysAdmin = False Then btnEnableCLR.Visible = False
    Else
      gpbSysAdmin.Visible = False
    End If

    Me.Visible = True
    Application.DoEvents()

    RaiseEvent evtLoaded()

    pFault.SetOK()
    Return pFault
  End Function

  'UI
  Private Sub tbc_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tbc.DrawItem
    'http://www.daniweb.com/software-development/vbnet/threads/353766/tabcontrol#
    'Firstly we'll define some parameters.
    Dim CurrentTab As TabPage = tbc.TabPages(e.Index)
    Dim ItemRect As Rectangle = tbc.GetTabRect(e.Index)
    'Dim FillBrush As New SolidBrush(Color.Red)
    'Dim TextBrush As New SolidBrush(Color.White)
    Dim FillBrush As New SolidBrush(Me.BackColor)
    Dim TextBrush As New SolidBrush(Color.Black)
    Dim sf As New StringFormat
    sf.Alignment = StringAlignment.Center
    sf.LineAlignment = StringAlignment.Center

    'If we are currently painting the Selected TabItem we'll 
    'change the brush colours and inflate the rectangle.
    If CBool(e.State And DrawItemState.Selected) Then
      'FillBrush.Color = Color.White
      'TextBrush.Color = Color.Red
      FillBrush.Color = CurrentTab.BackColor
      TextBrush.Color = Color.Black
      If e.Index = 0 Then
        ItemRect.Offset(2, 0)
        ItemRect.Inflate(0, 2)
      Else
        ItemRect.Inflate(2, 2)
      End If
    Else

    End If

    'Set up rotation for left and right aligned tabs
    If tbc.Alignment = TabAlignment.Left Or tbc.Alignment = TabAlignment.Right Then
      Dim RotateAngle As Single = 90
      If tbc.Alignment = TabAlignment.Left Then RotateAngle = 270
      Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
      e.Graphics.TranslateTransform(cp.X, cp.Y)
      e.Graphics.RotateTransform(RotateAngle)
      ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
    End If

    'Next we'll paint the TabItem with our Fill Brush
    e.Graphics.FillRectangle(FillBrush, ItemRect)

    'Now draw the text.
    e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

    'Reset any Graphics rotation
    e.Graphics.ResetTransform()

    'Finally, we should Dispose of our brushes.
    FillBrush.Dispose()
    TextBrush.Dispose()

    Dim rt As Rectangle = tbc.GetTabRect(tbc.TabPages.Count - 1)
    Dim rf As RectangleF = New RectangleF(rt.X + rt.Width + 4, rt.Y - 5, tbc.Width - (rt.X + rt.Width) - 4, rt.Height + 5)
    Dim b As Brush = New SolidBrush(Me.BackColor)
    e.Graphics.FillRectangle(b, rf)
    b.Dispose()

  End Sub

  'Permissions
  Private Sub ResetPermissionsForDefaultRoles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetPermissionsForDefaultRoles.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As New clsFault

    If AreYouSure("reset permissions for the default roles (SysAdmin, Administrator, UserManger, User)") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.ResetPermissionsForDefaultRoles(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub

  'Database
  Private Sub btnEjectAllUsers_Click(sender As Object, e As EventArgs) Handles btnEjectAllUsers.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("Eject All Users") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.EjectAllUsers(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub
  Private Sub btnEjectNonMasterUsersOnly_Click(sender As Object, e As EventArgs) Handles btnEjectNonMasterUsersOnly.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("Eject Non Master Users Only") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.EjectNonMaster(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub
  Private Sub btnRequestIndexReorganization_Click(sender As Object, e As EventArgs) Handles btnRequestIndexReorganization.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("Request Index Reorganization") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.RequestIndexReorganization(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub
  Private Sub btnBackupDatabase_Click(sender As Object, e As EventArgs) Handles btnBackupDatabase.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("Backup Database") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.RequestDatabaseBackup(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub
  Private Sub btnTableSizes_Click(sender As Object, e As EventArgs) Handles btnTableSizes.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    Cursor = Cursors.WaitCursor

    Dim pTableSizes As New csTableSizeCol
    pFault = pTableSizes.Fill(_Requester)
    If pFault.isOK = False Then
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
      Exit Sub
    End If
    pTableSizes.SortByTableName()

    grdTableSizes.Visible = False

    Dim pParameters As New ctlc_TableSizeCol.clsLoadParameters
    With pParameters
      .NavigationBarHide = False
      '.ColumnsListHide = True
      .ReadOnly = True
      .ReportTitle = "Table Size Report"
      .GridTitle = "" '"Showing " & pTableSizes.Count & " Tables"
      .SummarizeGrid = True

      .ColumnsHide.Add(csTableSize.enmProperty.ID)

      .ColumnsFormat.Add(csTableSize.enmProperty.NumberOfRows, "#,##0")

      .ColumnsFormat.Add(csTableSize.enmProperty.ReservedSizeKb, "#,##0")
      .ColumnsFormat.Add(csTableSize.enmProperty.DataSizeKb, "#,##0")
      .ColumnsFormat.Add(csTableSize.enmProperty.IndexSizeKb, "#,##0")
      .ColumnsFormat.Add(csTableSize.enmProperty.UnusedSizeKb, "#,##0")

      .ColumnsHeaderText.Add(csTableSize.enmProperty.ReservedSizeKb, "Reserved Size KB")
      .ColumnsHeaderText.Add(csTableSize.enmProperty.DataSizeKb, "Data Size KB")
      .ColumnsHeaderText.Add(csTableSize.enmProperty.IndexSizeKb, "Index Size KB")
      .ColumnsHeaderText.Add(csTableSize.enmProperty.UnusedSizeKb, "Unused Size KB")

    End With

    pFault = grdTableSizes.LoadControl(pTableSizes, pParameters, _Requester)
    If pFault.isOK = False Then
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
      Exit Sub
    End If

    txtDatabaseFileSizes.Visible = False
    grdTableSizes.Visible = True
    grdIndexFragmentation.Visible = False

    Cursor = Cursors.Default

  End Sub
  Private Sub btnIndexFragmentation_Click(sender As Object, e As EventArgs) Handles btnIndexFragmentation.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    Cursor = Cursors.WaitCursor

    Dim pIndexFragmentations As New csIndexFragmentationCol
    pFault = pIndexFragmentations.Fill(_Requester)
    If pFault.isOK = False Then
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
      Exit Sub
    End If


    pIndexFragmentations.SortByFragmentationPct()
    pIndexFragmentations.Reverse()

    grdIndexFragmentation.Visible = False

    Dim pParameters As New ctlc_IndexFragmentationCol.clsLoadParameters
    With pParameters
      .NavigationBarHide = False
      '.ColumnsListHide = True
      .ReadOnly = True
      .ReportTitle = "Index Fragmentation Report"
      .GridTitle = "" '"Showing " & pIndexFragmentations.Count & " Indexes"
      .SummarizeGrid = False

      .ColumnsHide.Add(csIndexFragmentation.enmProperty.ID)

      .ColumnsFormat.Add(csIndexFragmentation.enmProperty.FragmentationPct, "#0.00")
      .ColumnsFormat.Add(csIndexFragmentation.enmProperty.PageCount, "#,##0 ")

      .ColumnsHeaderText.Add(csIndexFragmentation.enmProperty.FragmentationPct, "Fragmentation %")
    End With

    pFault = grdIndexFragmentation.LoadControl(pIndexFragmentations, pParameters, _Requester)
    If pFault.isOK = False Then
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
      Exit Sub
    End If

    txtDatabaseFileSizes.Visible = False
    grdIndexFragmentation.Visible = True
    grdTableSizes.Visible = False

    Cursor = Cursors.Default

  End Sub

  Private Sub btnDatabaseFileSizes_Click(sender As Object, e As EventArgs) Handles btnDatabaseFileSizes.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    Cursor = Cursors.WaitCursor

    Dim pDBName = New List(Of String)
    Dim pFileName = New List(Of String)
    Dim pType = New List(Of String)
    Dim pCurrentSize = New List(Of Integer)
    Dim pFreeSpace = New List(Of Integer)

    pFault = ccDatabaseMaintenance.GetDatabaseFileSizes(_Requester, pDBName, pFileName, pType, pCurrentSize, pFreeSpace)
    If pFault.isOK = False Then
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
      Exit Sub
    End If

    Dim pText As New Text.StringBuilder

    pText.AppendLine($"Database Name: {pDBName(0)}{Environment.NewLine}")

    pText.Append($"{"FileName".PadRight(20, " "c)}{Chr(9)}{Chr(9)}")
    pText.Append($"Type{Chr(9)}{Chr(9)}")
    pText.Append($"Size MB{Chr(9)}{Chr(9)}")

    pText.AppendLine($"Free Space MB")
    pText.Append($"{"--------".PadRight(20, " "c)}{Chr(9)}{Chr(9)}")
    pText.Append($"----{Chr(9)}{Chr(9)}")
    pText.Append($"-------{Chr(9)}{Chr(9)}")
    pText.AppendLine($"-------------")

    For i As Integer = 0 To pDBName.Count - 1
      pText.Append($"{pFileName(i).PadRight(20, " "c)}{Chr(9)}{Chr(9)}")
      pText.Append($"{pType(i)}{Chr(9)}{Chr(9)}")
      pText.Append($"{pCurrentSize(i):#,##0}{Chr(9)}{Chr(9)}")
      pText.AppendLine($"{pFreeSpace(i):#,###0}")
    Next

    txtDatabaseFileSizes.Text = pText.ToString()

    txtDatabaseFileSizes.Visible = True
    grdIndexFragmentation.Visible = False
    grdTableSizes.Visible = False

    Cursor = Cursors.Default

  End Sub


  Private Sub btnCreateBinaryFilesOnServer_Click(sender As Object, e As EventArgs) Handles btnCreateBinaryFilesOnServer.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault
    If rbtnOneFileForDatabase.Checked = True Then
      Cursor = Cursors.WaitCursor
      pFault = ccDatabaseMaintenance.WriteDatabaseToBinary(True, _Requester)
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
    ElseIf rbtnOneFilePerTable.Checked = True Then
      Cursor = Cursors.WaitCursor
      pFault = ccDatabaseMaintenance.WriteDatabaseToBinary(False, _Requester)
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
    Else
      frmMessageOrInputBox.ShowMsg("Choose a format!", frmMessageOrInputBox.enmIconType.Exclamation)
    End If
  End Sub
  Private Sub btnTranslationAddAllPossibilitiesToObjectToTranslate_Click(sender As Object, e As EventArgs) Handles btnTranslationAddAllPossibilitiesToObjectToTranslate.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault
    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.TranslationAddAllPossibilitiesToObjectToTranslate(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub
  Private Sub btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate_Click(sender As Object, e As EventArgs) Handles btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault
    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.TranslationRemoveUnusedPossibilitiesFromObjectToTranslate(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub

  Private Sub btnEnableCLR_Click(sender As Object, e As EventArgs) Handles btnEnableCLR.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("enable CLR on the database server. Only a SysAdmin can do this!") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.EnableCLR(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub

  Private Sub btnRunScriptOnServer_Click(sender As Object, e As EventArgs) Handles btnRunScriptOnServer.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pScript As String = ""

    Dim pSucceeded As Boolean = False

    Dim pInitialValue As String = "--Sample" & Environment.NewLine & "Update MyTable " & Environment.NewLine & "Set Col = 'Value' " & Environment.NewLine & "Where Field = 'OtherValue' " & Environment.NewLine & "GO" & Environment.NewLine
    pInitialValue &= "Delete From MyTable " & Environment.NewLine & "Where OtherField = 'NewValue' " & Environment.NewLine & "GO"

    Dim pPrompt As String = "Copy the script into the textbox. Separate the batches with GO."
    frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.MultiLineTextBox
    frmUpdateField.DialogueInitialValue = pInitialValue
    frmUpdateField.DialoguePrompt = pPrompt
    Do
      frmUpdateField.ShowDialog()
      If frmUpdateField.DialogResult = DialogResult.OK Then
        Try
          pScript = frmUpdateField.DialogueReturnValue.ToString
          pSucceeded = True
        Catch ex As Exception
          pSucceeded = False
        End Try
      Else
        Exit Sub
      End If
    Loop Until pSucceeded = True

    If pScript = pInitialValue Then
      Exit Sub
    End If

    If String.IsNullOrEmpty(pScript) Then
      Exit Sub
    End If

    Dim pLines As String() = pScript.Replace(Environment.NewLine, "~").Split("~"c)
    Dim pCommands As New List(Of String)
    Dim pCommand As String = ""
    For Each lLine As String In pLines
      If String.IsNullOrEmpty(lLine) Then Continue For
      If lLine.Trim.Equals("go", StringComparison.OrdinalIgnoreCase) Then
        If Not String.IsNullOrEmpty(pCommand) Then
          pCommands.Add(pCommand)
          pCommand = ""
        End If
      Else
        pCommand &= lLine & Environment.NewLine
      End If
    Next
    If Not String.IsNullOrEmpty(pCommand) Then
      pCommands.Add(pCommand)
      pCommand = ""
    End If

    'Now run it
    Dim pShowMessages As Boolean = True
    For Each lCommand As String In pCommands
      Dim pFault As clsFault
      Dim pResponse As String = ""
      pFault = ccDatabaseMaintenance.RunSQLScriptOnServer(lCommand, pResponse, _Requester)
      If Not pFault.isOK Then
        ShowFault(pFault, _Requester)
        If pFault.Description.IndexOf("Num: 229", StringComparison.OrdinalIgnoreCase) >= 0 Then
          frmMessageOrInputBox.ShowMsg("Ensure that the SQL user is a SysAdmin, or try using Integrated Security (change the Controller in the config file)", frmMessageOrInputBox.enmIconType.Information)
        End If
        Exit Sub
      Else
        If pShowMessages = True Then
          If String.IsNullOrEmpty(pResponse) Then pResponse = "OK"
          pResponse = pResponse & Environment.NewLine & Environment.NewLine & "Continue showing individual messages?"
          Dim pResult As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg(pResponse, frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes")
          If pResult = frmMessageOrInputBox.enmButtonReturned.No Then pShowMessages = False
        End If
      End If
    Next

    frmMessageOrInputBox.ShowMsg("Completed", frmMessageOrInputBox.enmIconType.Information)

  End Sub

  Private Sub grdTableSizes_evtRowDoubleClicked(vTableSize As csTableSize, ByRef rHandled As Boolean) Handles grdTableSizes.evtRowDoubleClicked
    rHandled = True
  End Sub

  Private Sub grdIndexFragmentation_evtRowDoubleClicked(vIndexFragmentation As csIndexFragmentation, ByRef rHandled As Boolean) Handles grdIndexFragmentation.evtRowDoubleClicked
    rHandled = True
  End Sub

  Private Sub ctlPnlcsDBMaintenance_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged

    Dim pSize As Single = CSng(14 * MyFont.Size / 9)
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 

    txtDatabaseFileSizes.Font = New Font("Courier New", MyFont.Size)
  End Sub

End Class
