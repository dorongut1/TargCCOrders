Public Class IntelliCombo

  Private Enum enmIntelliComboType
    UD
    Dumb
    Smart
  End Enum

  Public Event evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember)

  Private _ComboList As clsComboList
  Private _Prompt As String
  Private _PreviousText As String

  Private _IntelliComboType As enmIntelliComboType

  Private _IgnoreKey As Boolean

  Private _KeyType As clsEnums.enmComboListKeyType

  Private WithEvents _Timer As Timer

  Private _ShowOptionsOn1stLoad As Boolean = False

  Public ReadOnly Property IsLoaded() As Boolean
    Get
      If _ComboList IsNot Nothing Then
        Return True
      Else
        Return False
      End If
    End Get
  End Property

  Public ReadOnly Property isPageFromServer() As Boolean
    Get
      Return (_ComboListType <> clsEnums.enmComboListType.UD)
    End Get
  End Property

  Public ReadOnly Property SelectedItem() As clsComboListMember
    Get
      Return CType(cbo.SelectedItem, clsComboListMember)
    End Get
  End Property

  Public ReadOnly Property SelectedIndex() As Integer
    Get
      Return cbo.SelectedIndex
    End Get
  End Property

  Public ReadOnly Property SelectedValue() As Object
    Get
      Return cbo.SelectedValue
    End Get
  End Property

  Public ReadOnly Property IsDumb() As Boolean
    Get
      If _IntelliComboType = enmIntelliComboType.Dumb Then
        Return True
      ElseIf _IntelliComboType = enmIntelliComboType.Smart Then
        Return False
      Else
        'Throw New Exception("IntelliComboType is not defined!") 'dummy fix. 
        _IntelliComboType = enmIntelliComboType.Smart
        Return False
      End If
    End Get
  End Property

  Public Overrides Property Text() As String
    Get
      If _IntelliComboType = enmIntelliComboType.Dumb Then
        Return cbo.Text
      Else
        Return ""
      End If
    End Get
    Set(ByVal value As String)
      Throw New Exception("The text in an Intelligent ComboBox cannot be set.")
    End Set
  End Property

  Public Property DropDownStyle() As System.Windows.Forms.ComboBoxStyle
    Get
      Return cbo.DropDownStyle
    End Get
    Set(ByVal value As System.Windows.Forms.ComboBoxStyle)
      If _IntelliComboType = enmIntelliComboType.Smart Then
        If value = ComboBoxStyle.DropDownList Then Throw New Exception("Cannot use DropDownList when Combo is smart")
        If value = ComboBoxStyle.Simple Then Throw New Exception("IntelliCombo cannot use DropDownStyle of Simple")
      End If
      cbo.DropDownStyle = value
    End Set
  End Property

  Public Sub New()
    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    _IntelliComboType = enmIntelliComboType.UD
  End Sub

  Private Sub IntelliCombo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Me.DesignMode = True Then Exit Sub
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont
    Me.PerformAutoScale()

  End Sub

  ''' <summary>
  ''' Use only when not sending a ComboList
  ''' </summary>
  ''' <param name="vKeyType"></param>
  Public Sub SetKeyType(ByVal vKeyType As clsEnums.enmComboListKeyType)

    If _ComboList Is Nothing Then
      _KeyType = vKeyType
      If _KeyType = clsEnums.enmComboListKeyType.UD Then
        Throw New Exception("When sending a null ComboList (i.e., Combolist is Nothing), you must define the KeyType")
      End If
    Else
      Throw New Exception("Do not define a KeyType for non-null ComboList")
    End If


  End Sub

  Private Shared _LoggedRowHeader As Text.StringBuilder = Nothing

  Private _ComboListType As clsEnums.enmComboListType = clsEnums.enmComboListType.UD
  Private _ParentID As Long = 0
  Private _Requester As clsRequester

  ''' <summary>
  ''' Use this when you want to page the server to get additional options. 
  ''' On 1st load, by default, it will only show a row telling you to type. This makes it load much faster, when using multiple controls
  ''' </summary>
  ''' <param name="vPrompt"></param>
  ''' <param name="vComboListType"></param>
  ''' <param name="vParentID"></param>
  ''' <param name="vRequester"></param>
  ''' <param name="vShowOptionsOn1stLoad"></param>
  Public Sub LoadControlAndPageFromServer(ByVal vPrompt As String, ByVal vComboListType As clsEnums.enmComboListType, ByVal vParentID As Long, ByVal vRequester As clsRequester, Optional vShowOptionsOn1stLoad As Boolean = False)
    _ComboListType = vComboListType
    _ParentID = vParentID
    _Requester = vRequester
    _ShowOptionsOn1stLoad = vShowOptionsOn1stLoad
    Dim pComboList As New clsComboList()
    LoadControlInternal(pComboList, vPrompt)
  End Sub
  ''' <summary>
  ''' Use this when you want to load the entire combolist
  ''' On 1st load, by default, it will only show a row telling you to type. This makes it load much faster, when using multiple controls
  ''' </summary>
  ''' <param name="vComboList"></param>
  ''' <param name="vPrompt"></param>
  ''' <param name="vShowOptionsOn1stLoad"></param>
  Public Sub LoadControl(ByVal vComboList As clsComboList, ByVal vPrompt As String, Optional vShowOptionsOn1stLoad As Boolean = False)
    _ComboListType = clsEnums.enmComboListType.UD
    _ParentID = 0
    _ShowOptionsOn1stLoad = vShowOptionsOn1stLoad
    If Not cbo.SelectedItem Is Nothing Then cbo.SelectedItem = Nothing
    If Not cbo.SelectedValue Is Nothing AndAlso vComboList IsNot Nothing AndAlso vComboList.Count > 0 Then cbo.SelectedValue = Nothing
    LoadControlInternal(vComboList, vPrompt)
  End Sub

  Private Sub LoadControlInternal(ByVal vComboList As clsComboList, ByVal vPrompt As String)
    If vComboList Is Nothing Then
      Throw New Exception("ComboList is Nothing")
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
        _LoggedRowHeader.Append(", Owner, CountInList, ")
      End If
      Dim pParentFormName As String
      Dim pParent As Control = Me
      Do
        pParent = pParent.Parent
        If pParent Is Nothing Then
          pParentFormName = Me.Parent?.Name & " Default"
          Exit Do
        Else
          pParentFormName = pParent.Name
        End If
      Loop Until pParentFormName.IndexOf("_") > 0
      pLoggedRow.Append(String.Format(", {0}, {1}, ", pParentFormName, vComboList.Count))
      sw.Start()
    End If

    _PreviousText = ""
    _Prompt = vPrompt
    cbo.Text = ""

    If vComboList IsNot Nothing AndAlso vComboList.Count > 0 Then
      _KeyType = vComboList.KeyType
    End If

    If pLogDAL = True Then
      sw.Stop()
      If pCreateHeader = True Then _LoggedRowHeader.Append("Clone, ")
      pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds))
      sw.Restart()
    End If

    If _IntelliComboType = enmIntelliComboType.UD Then
      If Debugger.IsAttached Then
        MsgBox("Please define the IntelliComboType before calling LoadControl. Don't rely on the safety catch below.....")
        Throw New Exception("IntelliComboType must be defined before calling LoadControl! Fix this 1st!!")
      End If
      _IntelliComboType = enmIntelliComboType.Smart
    End If

    If _IntelliComboType = enmIntelliComboType.Dumb Then
      _ComboList = vComboList.Clone
      With cbo
        If _KeyType = clsEnums.enmComboListKeyType.String Then
          If _ComboList.Count = 0 OrElse _ComboList(0).KeyString <> "" Then
            _ComboList.AddToTop("", _Prompt)
          End If
          .ValueMember = "KeyString"
        ElseIf _KeyType = clsEnums.enmComboListKeyType.Long Then
          If _ComboList.Count = 0 OrElse _ComboList(0).KeyLong <> -1 Then
            _ComboList.AddToTop(ccHelper.ToLong(-1), _Prompt)
          End If
          .ValueMember = "KeyLong"
        ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer Then
          If _ComboList.Count = 0 OrElse _ComboList(0).KeyInteger <> -1 Then
            _ComboList.AddToTop(ccHelper.ToInteger(-1), _Prompt)
          End If
          .ValueMember = "KeyInteger"
        End If
        .DisplayMember = "Text"
        .DataSource = _ComboList
      End With

      cbo.Text = _Prompt
      cbo.ForeColor = Color.DimGray

      If cbo.Items.Count > 0 Then cbo.SelectedIndex = 0
      If pLogDAL = True Then
        sw.Stop()
        If pCreateHeader = True Then _LoggedRowHeader.Append("Execute, ")
        pLoggedRow.Append(String.Format("{0}, {1}", "Dumb", sw.Elapsed.TotalMilliseconds))
        sw.Restart()
      End If
    ElseIf _IntelliComboType = enmIntelliComboType.Smart Then

      _ComboList = vComboList
      RunIntelligentCbo()

      _Timer = New Timer
      _Timer.Interval = 100
      _Timer.Enabled = False

      cbo.Text = _Prompt
      cbo.ForeColor = Color.DimGray

      If pLogDAL = True Then
        sw.Stop()
        If pCreateHeader = True Then _LoggedRowHeader.Append("Execute, ")
        pLoggedRow.Append(String.Format("{0}, {1}", "Smart", sw.Elapsed.TotalMilliseconds))
        sw.Restart()
      End If
    End If

    If pLogDAL = True Then
      If pCreateHeader = True Then Tools.LogToTextFile.WriteMessage(_LoggedRowHeader.ToString() & " times in ms", "LoadIntelliComboTimes")
      Tools.LogToTextFile.WriteMessage(pLoggedRow.ToString(), "LoadIntelliComboTimes")
    End If
  End Sub

  Public Sub Clear()
    _ComboList = Nothing
  End Sub

  Public Sub MakeDumb()
    If _IntelliComboType <> enmIntelliComboType.UD Then
      If _IntelliComboType = enmIntelliComboType.Dumb Then Return
      Throw New Exception("You can only set the IntelliComboType once")
    End If

    _IntelliComboType = enmIntelliComboType.Dumb
    If cbo.DropDownStyle = ComboBoxStyle.DropDown Then
      cbo.DropDownStyle = ComboBoxStyle.DropDownList
    End If
  End Sub
  Public Sub MakeSmart()
    If _IntelliComboType <> enmIntelliComboType.UD Then
      If _IntelliComboType = enmIntelliComboType.Smart Then Return
      Throw New Exception("You can only set the IntelliComboType once")
    End If

    If cbo.DropDownStyle = ComboBoxStyle.DropDownList Then
      'Throw New Exception("Cannot use DropDownList when Combo is smart")
      cbo.DropDownStyle = ComboBoxStyle.DropDown
    End If
    _IntelliComboType = enmIntelliComboType.Smart
    RunIntelligentCbo()
    cbo.Text = _Prompt
    cbo.ForeColor = Color.DimGray
  End Sub

  ''' <summary>
  ''' Use vValue if looking for a specific entry when Combolist type is PageFromServer
  ''' </summary>
  ''' <param name="vValue"></param>
  Public Sub ValueSelect(ByVal vValue As Object)
    Dim pComboList As clsComboList = CType(cbo.DataSource, clsComboList)
    If pComboList Is Nothing Then Return
    If _ComboList Is Nothing OrElse _ComboList.Count = 0 Then Return

    If _IntelliComboType = enmIntelliComboType.Smart Then
      Dim pFirstRowIsChoose As Boolean = False
      If _KeyType = clsEnums.enmComboListKeyType.String AndAlso _ComboList(0).KeyString = "" Then
        pFirstRowIsChoose = True
      ElseIf _KeyType = clsEnums.enmComboListKeyType.Long AndAlso _ComboList(0).KeyLong = -1 Then
        pFirstRowIsChoose = True
      ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer AndAlso _ComboList(0).KeyInteger = -1 Then
        pFirstRowIsChoose = True
        'ElseIf _KeyType = clsEnums.enmComboListKeyType.Object AndAlso ccHelper.ToInteger(_ComboList(0).KeyObject) = -1 Then
        '  pFirstRowIsChoose = True
      End If
      If isPageFromServer Then
        If vValue.GetType.Name.Equals("int64", StringComparison.OrdinalIgnoreCase) OrElse vValue.GetType.Name.Equals("int32", StringComparison.OrdinalIgnoreCase) Then
          Dim pID As Long = ccHelper.ToLong(vValue)
          RunIntelligentCbo(vID:=pID)
          pComboList = CType(cbo.DataSource, clsComboList)
        Else
          Dim pStrg As String = vValue.ToString
          If Not String.IsNullOrEmpty(pStrg) Then
            RunIntelligentCbo(vCode:=pStrg)
            pComboList = CType(cbo.DataSource, clsComboList)
          End If
        End If
      End If
      If pComboList.Count <> _ComboList.Count OrElse (_ComboList.Count = 1 AndAlso pComboList.Count = 1 AndAlso pComboList(0).Text.StartsWith("** Start typing")) Then
        cbo.SelectedIndex = -1
        cbo.Text = ""
        If _ComboList.Count > 0 Then
          If Not (pFirstRowIsChoose) Then
            cbo.Text = _ComboList.FindByKey(vValue).Text
          End If
        Else
          cbo.Text = "Choose"
          'Throw New Exception("No Items in ComboList!")
        End If
        RunIntelligentCbo()
      End If
      If _ComboList.Count > 0 Then
        If Not (pFirstRowIsChoose) Then
          cbo.SelectedValue = vValue
          Dim pMember As clsComboListMember = Nothing
          If cbo.SelectedValue Is Nothing Then
            If _KeyType = clsEnums.enmComboListKeyType.String Then
              pMember = New clsComboListMember("", "")
            ElseIf _KeyType = clsEnums.enmComboListKeyType.Long Then
              pMember = New clsComboListMember(ccHelper.ToLong(-1), "")
            ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer Then
              pMember = New clsComboListMember(ccHelper.ToInteger(-1), "")
            End If
            'cbo.SelectedIndex = -1
            cbo.Text = _Prompt
            cbo.ForeColor = Color.DimGray
          Else
            pMember = CType(cbo.SelectedItem, clsComboListMember)
          End If
          RaiseEvent evtComboListMemberChosen(pMember)
        Else
          Throw New Exception("Invalid Object Type Received!")
        End If
      Else
        cbo.Text = "Choose"
        'Throw New Exception("No Items in ComboList!")
      End If
    ElseIf _IntelliComboType = enmIntelliComboType.Dumb Then
      If _ComboList.Count > 1 Then
        If _ComboList.FindByKey(vValue).Text = "" Then
          'This could happen due to security restrictions
          cbo.Text = ""
        Else
          cbo.SelectedValue = vValue
        End If
      Else
        If vValue IsNot Nothing Then
          Dim pMember As clsComboListMember
          If _KeyType = clsEnums.enmComboListKeyType.String Then
            pMember = New clsComboListMember(CStr(vValue), _Prompt)
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Long Then
            pMember = New clsComboListMember(ccHelper.ToLong(vValue), _Prompt)
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer Then
            pMember = New clsComboListMember(ccHelper.ToInteger(vValue), _Prompt)
          Else
            pMember = New clsComboListMember(0, _Prompt)
          End If
          RaiseEvent evtComboListMemberChosen(pMember)
        End If
        Return
      End If
    Else
      Throw New Exception("IntelliComboType must be defined before calling ValueSelect! ")
    End If

  End Sub

  Public Sub ValueClear()
    If _IntelliComboType = enmIntelliComboType.Smart Then
      cbo.SelectedIndex = -1
      cbo.Text = ""
      RunIntelligentCbo()
      cbo.Text = _Prompt
      cbo.ForeColor = Color.DimGray
    ElseIf _IntelliComboType = enmIntelliComboType.Dumb Then
      If cbo.Items.Count > 0 Then
        cbo.SelectedIndex = 0
      End If
    Else
      Throw New Exception("IntelliComboType must be defined before calling ValueClear! ")
    End If
  End Sub

  Private Sub cbo_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo.DropDown
    If cbo.Text = _Prompt Then cbo.Text = ""
    cbo.ForeColor = Color.Black
  End Sub

  Private Sub cbo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cbo.KeyUp
    _IgnoreKey = True
    If _IntelliComboType = enmIntelliComboType.Dumb Then
      If e.KeyCode = Keys.Enter Then
        Dim pStrg As String = cbo.Text
        Dim pMember As New clsComboListMember

        If _KeyType = clsEnums.enmComboListKeyType.String Then
          pMember.KeyString = "-3"
        ElseIf _KeyType = clsEnums.enmComboListKeyType.Long Then
          pMember.KeyLong = -3
        ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer Then
          pMember.KeyInteger = -3
          'ElseIf _KeyType = clsEnums.enmComboListKeyType.Object Then
          '  pMember.KeyObject = -3
        Else
          If _KeyType = clsEnums.enmComboListKeyType.UD Then
            Throw New Exception("When sending a null ComboList (i.e., Combolist is Nothing), you must define the KeyType with SetKeyType")
          End If
        End If
        pMember.Text = pStrg
        RaiseEvent evtComboListMemberChosen(pMember)
      End If
      Exit Sub
    End If

    If Not (e.KeyCode = Keys.Menu Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Enter) Then
      _Timer.Start()
    End If
    _IgnoreKey = False
  End Sub

  Private Sub cbo_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo.SelectionChangeCommitted
    If _IntelliComboType = enmIntelliComboType.Smart Then
      If cbo.SelectedIndex >= 0 Then
        cbo.ForeColor = Color.Black
        Dim pComboListMember As clsComboListMember
        pComboListMember = CType(cbo.SelectedItem, clsComboListMember)
        cbo.DroppedDown = False
        cbo.Text = pComboListMember.Text
        Application.DoEvents()
        RaiseEvent evtComboListMemberChosen(pComboListMember)
      End If
    ElseIf _IntelliComboType = enmIntelliComboType.Dumb Then
      If cbo.SelectedIndex >= 0 Then
        Dim pComboListMember As clsComboListMember
        pComboListMember = CType(cbo.SelectedItem, clsComboListMember)
        RaiseEvent evtComboListMemberChosen(pComboListMember)
      End If
    Else
      Throw New Exception("IntelliComboType must be defined before calling cbo_SelectionChangeCommitted! ")
    End If
  End Sub

  Dim _FirstRun As Boolean = True
  Private Sub RunIntelligentCbo(Optional vID As Long = -1, Optional vCode As String = "")
    If _ComboList Is Nothing Then Return

    Dim pTextIn As String = cbo.Text
    Dim pTextToTestFor As String = "" 'pTextIn.ToLowerInvariant() '.Replace(" ", "")
    'If pTextIn = " " Then Stop
    If pTextIn.StartsWith(" ", StringComparison.OrdinalIgnoreCase) AndAlso pTextIn.EndsWith(" ") Then
      pTextToTestFor = " " & pTextIn.ToLowerInvariant.Replace(" ", "") & " "
      If pTextToTestFor.Equals("  ", StringComparison.OrdinalIgnoreCase) Then pTextToTestFor = " "
    ElseIf pTextIn.StartsWith(" ") Then
      pTextToTestFor = " " & pTextIn.ToLowerInvariant.Replace(" ", "")
    ElseIf pTextIn.EndsWith(" ") Then
      pTextToTestFor = pTextIn.ToLowerInvariant.Replace(" ", "") & " "
    Else
      pTextToTestFor = pTextIn.ToLowerInvariant.Replace(" ", "")
    End If

    If pTextToTestFor = "" Then
      cbo.SelectedIndex = -1
    End If

    Dim pNewListForCombo As New clsComboList

    If pTextIn.Equals(_Prompt, StringComparison.OrdinalIgnoreCase) AndAlso vID = -1 AndAlso String.IsNullOrEmpty(vCode) Then
      'cbo.SelectedIndex = -1
      Exit Sub
    End If

    If _ComboList.KeyType = clsEnums.enmComboListKeyType.String Then
      Dim pSelectedValue As String
      If SelectedValue Is Nothing Then
        pSelectedValue = "-9999" 'Make sure it's trapped
      Else
        pSelectedValue = SelectedValue.ToString()
      End If
      If pTextIn = _PreviousText AndAlso pTextToTestFor.Length > 0 AndAlso vCode = pSelectedValue Then Exit Sub
    Else
      Dim pSelectedValue As Long
      If SelectedValue Is Nothing Then
        pSelectedValue = -9999 'Make sure it's trapped
      Else
        If _ComboList.KeyType = clsEnums.enmComboListKeyType.UD Then Return 'avoid error when no items in table
        If _ComboList.Count = 1 AndAlso Not (SelectedValue.GetType().Name.Equals("Int64", StringComparison.OrdinalIgnoreCase) OrElse SelectedValue.GetType().Name.Equals("Int32", StringComparison.OrdinalIgnoreCase)) Then
          'there's a problem when adding the 1st item to an entity
          pSelectedValue = ccHelper.ToLong(_ComboList(0).Key)
        Else
          pSelectedValue = ccHelper.ToLong(SelectedValue)
        End If
      End If
      If pTextIn = _PreviousText AndAlso pTextToTestFor.Length > 0 AndAlso vID = pSelectedValue Then Exit Sub
    End If

    Cursor = Cursors.WaitCursor

    cbo.SuspendLayout()
    Application.DoEvents()

    Dim pMaxshow As Integer = 28
    Dim pMaxRowsToReturn As Integer
    pMaxRowsToReturn = 30

    If String.IsNullOrEmpty(pTextToTestFor) AndAlso vID = -1 AndAlso String.IsNullOrEmpty(vCode) Then
      _FirstRun = True
    End If

    If _FirstRun And Not _ShowOptionsOn1stLoad Then
      pMaxshow = 0
      pMaxRowsToReturn = 1
      _FirstRun = False
    End If

    Dim sw As Stopwatch = Nothing
    Dim pLog As Text.StringBuilder = Nothing

    Dim pDoLog As Boolean = MyController.LogDetails
    If pDoLog Then
      sw = New Stopwatch()
      pLog = New Text.StringBuilder()
    End If

    'Now see if we have to get data for server
    If isPageFromServer Then
      If (pTextToTestFor = "" AndAlso _ComboList.Count < pMaxRowsToReturn) OrElse
          (pTextToTestFor <> "" AndAlso
            (_ComboList.Count = pMaxRowsToReturn OrElse _ComboList.Count = 0 OrElse _ComboList.Count = 1 OrElse pTextToTestFor.Length < _PreviousText.Length) OrElse vID > -1 OrElse Not String.IsNullOrEmpty(vCode)) Then
        _Requester.CallingFunctionWithinApplication = "RunIntelligentCbo"
        If pDoLog Then
          sw.Reset()
          sw.Start()
        End If
        If vID > -1 OrElse Not String.IsNullOrEmpty(vCode) Then
          pTextToTestFor = ""
        End If
        Dim pFault As clsFault = _ComboList.Fill(_ComboListType, _Requester, _ParentID, pTextToTestFor, pMaxRowsToReturn, vID, vCode)
        If Not pFault.isOK() Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Return
        _KeyType = _ComboList.KeyType
        If pDoLog Then
          sw.Stop()
          pLog.AppendLine(Space(23) & $"ComboListType: {_ComboListType}, ParentID: {_ParentID}, TextToTestFor: {pTextToTestFor}, IDToTestFor(Code): {vID} ({vCode}), Time (ms): {sw.Elapsed.TotalMilliseconds}")
        End If
      Else
        If pDoLog Then
          pLog.AppendLine(Space(23) & $"ComboListType: {_ComboListType}, ParentID: {_ParentID}, TextToTestFor: {pTextToTestFor}, IDToTestFor(Code): {vID} ({vCode}), Time: FillNotCalled")
        End If
      End If
    End If

    If pDoLog Then
      sw.Reset()
      sw.Start()
    End If

    Dim lcntr As Long = 0
    Dim pInstructionText As String = "↓↓↓↓↓ Use space before and/or after to narrow the search"
    If pMaxshow = 0 Then pInstructionText = "** Start typing or hit space to get the options **"
    If pTextToTestFor = "" Then
      For Each MyRow As clsComboListMember In _ComboList
        lcntr += 1
        If lcntr > pMaxshow Then
          Dim pNewRow As clsComboListMember = Nothing
          Select Case _KeyType
            '»»»»»»»»»»»»»»»»»»
            Case clsEnums.enmComboListKeyType.Integer : pNewRow = New clsComboListMember(ccHelper.ToInteger(0), pInstructionText)
            Case clsEnums.enmComboListKeyType.Long : pNewRow = New clsComboListMember(ccHelper.ToLong(0), pInstructionText)
            'Case clsEnums.enmComboListKeyType.Object : pNewRow = New clsComboListMember(MyRow.KeyObject, pInstructionText)
            'Case clsEnums.enmComboListKeyType.String : pNewRow = New clsComboListMember("", pInstructionText)
            Case clsEnums.enmComboListKeyType.String : pNewRow = New clsComboListMember("NeverChoose", pInstructionText)
            Case clsEnums.enmComboListKeyType.Enum : pNewRow = New clsComboListMember(ccHelper.ToInteger(0), pInstructionText)
          End Select
          pNewListForCombo.Add(pNewRow)
          Exit For
        End If
        pNewListForCombo.Add(MyRow)
      Next
    Else 'If pTextToTestFor <> pPreviousText Then
      If _ComboList Is Nothing Then Exit Sub
      '1st try to find it
      'Dim pFound As clsComboListMember = _ComboList.FindByText(pTextIn)
      'If Not String.IsNullOrEmpty(pFound.Text) Then
      '  pNewListForCombo.Add(pFound)
      '  lCntr += 1
      'End If
      For Each MyRow As clsComboListMember In _ComboList
        'If MyRow.Text = pFound.Text Then Continue For
        'If MyRow.Text.Replace(" ", "").ToLowerInvariant().IndexOf(pTextToTestFor, StringComparison.OrdinalIgnoreCase) >= 0 Then
        If (" " + MyRow.Text.Replace(" ", "").Trim + " ").ToLowerInvariant().IndexOf(pTextToTestFor, StringComparison.OrdinalIgnoreCase) >= 0 Then
          lcntr += 1
          If lcntr > pMaxshow Then
            Dim pNewRow As clsComboListMember = Nothing
            Select Case _KeyType
              Case clsEnums.enmComboListKeyType.Integer : pNewRow = New clsComboListMember(ccHelper.ToInteger(0), pInstructionText)
              Case clsEnums.enmComboListKeyType.Long : pNewRow = New clsComboListMember(ccHelper.ToLong(0), pInstructionText)
              'Case clsEnums.enmComboListKeyType.Object : pNewRow = New clsComboListMember(MyRow.KeyObject, pInstructionText)
              'Case clsEnums.enmComboListKeyType.String : pNewRow = New clsComboListMember("", pInstructionText)
              Case clsEnums.enmComboListKeyType.String : pNewRow = New clsComboListMember("NeverChoose", pInstructionText)
              Case clsEnums.enmComboListKeyType.Enum : pNewRow = New clsComboListMember(ccHelper.ToInteger(0), pInstructionText)
            End Select
            pNewListForCombo.Add(pNewRow)
            Exit For
          End If
          pNewListForCombo.Add(MyRow)
        End If
      Next

    End If

    If pNewListForCombo.Count = 0 Then
      Dim pRowToUse As New clsComboListMember
      pRowToUse.Text = ""
      pNewListForCombo.Add(pRowToUse)
    End If

    With cbo
      .DisplayMember = "Text"
      'Console.WriteLine(Now.ToString("ssffff") & ": pNewListForCombo.Count" & pNewListForCombo.Count)

      Select Case pNewListForCombo.KeyType
        Case clsEnums.enmComboListKeyType.Long : .ValueMember = "KeyLong"
        Case clsEnums.enmComboListKeyType.Integer : .ValueMember = "KeyInteger"
        Case clsEnums.enmComboListKeyType.String : .ValueMember = "KeyString"
        Case clsEnums.enmComboListKeyType.Enum : .ValueMember = "KeyEnum"
          'Case clsEnums.enmComboListKeyType.Object : .ValueMember = "KeyObject"
      End Select
      .DataSource = pNewListForCombo
    End With

    With cbo
      .ResumeLayout()
      If pNewListForCombo.Count > 0 Then
        .SelectedIndex = -1
      End If
      .Text = pTextIn
      If pTextIn.Length > 0 Then .SelectionStart = pTextIn.Length
      'Console.WriteLine(Now.ToString("ssffff") & ": SelStart " & pTextIn)
    End With

    If pTextToTestFor = "" Then
      cbo.SelectedIndex = -1
    End If

    _PreviousText = pTextIn

    If pDoLog Then
      sw.Stop()
      pLog.AppendLine(Space(23) & $"ComboListType: {_ComboListType}, _ComboList.Count: {_ComboList.Count}, TextToTestFor: {pTextToTestFor}, Load Time (ms){sw.Elapsed.TotalMilliseconds}")
      Tools.LogToTextFile.WriteMessage(Environment.NewLine & pLog.ToString(), "RunIntelliCombo")
    End If

    Cursor = Cursors.Default

  End Sub

  Private Sub _Timer_Tick(sender As Object, e As EventArgs) Handles _Timer.Tick
    _Timer.Stop()
    RunIntelligentCbo()
  End Sub

  'handle drop-down
  Private Sub cbo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbo.KeyPress
    If _IntelliComboType = enmIntelliComboType.Dumb Then
      If cbo.Text = _Prompt Then
        cbo.Text = ""
      End If
      Exit Sub
    End If
    If _IgnoreKey = True Then
      e.Handled = True
      cbo.Text = "Please type slower!"
      Console.Beep(500, 100)
      Console.Beep(2000, 300)
    End If
    If cbo.Text = _Prompt Then
      cbo.Text = ""
      cbo.ForeColor = Color.Black
    End If
    cbo.DroppedDown = True
  End Sub
  Private Sub cbo_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo.DropDownClosed
    If _IntelliComboType = enmIntelliComboType.Dumb Then Exit Sub

    If cbo.Items.Count = 0 Then Exit Sub
    If cbo.Text = "" OrElse cbo.Text = _Prompt Then
      cbo.SelectedIndex = -1
      cbo.Text = _Prompt
      cbo.ForeColor = Color.DimGray
    End If
  End Sub
  Private Sub cbo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo.LostFocus
    If _IntelliComboType = enmIntelliComboType.Dumb Then Exit Sub

    If cbo.Items.Count = 0 Then Exit Sub
    If cbo.Text = "" Then
      cbo.Text = _Prompt
      cbo.ForeColor = Color.DimGray
    End If
  End Sub
  Private Sub cbo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo.GotFocus
    If _IntelliComboType = enmIntelliComboType.Dumb Then Exit Sub

    If cbo.Items.Count = 0 Then Exit Sub
    If cbo.Text = _Prompt Then
      cbo.Select(0, 0)
    End If
  End Sub

  Private Sub cbo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo.SelectedIndexChanged
    If _IntelliComboType = enmIntelliComboType.Dumb Then Exit Sub
    If cbo.SelectedIndex > -1 Then
      cbo.ForeColor = Color.Black
    End If
  End Sub

  Private _HandlingKeyDown As Boolean
  Private Sub cbo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cbo.KeyDown
    If _IntelliComboType = enmIntelliComboType.Dumb Then
      If cbo.Text = _Prompt Then
        If e.KeyCode = Keys.Left OrElse
            e.KeyCode = Keys.Up OrElse
            e.KeyCode = Keys.Down OrElse
            e.KeyCode = Keys.PageDown OrElse
            e.KeyCode = Keys.PageUp OrElse
            e.KeyCode = Keys.Home OrElse
            e.KeyCode = Keys.End OrElse
            e.KeyCode = Keys.Right Then Exit Sub
        _HandlingKeyDown = True
        cbo.Text = ""
        cbo.ForeColor = Color.Black
        _HandlingKeyDown = False
      End If
    ElseIf _IntelliComboType = enmIntelliComboType.Smart Then
      If cbo.Items.Count = 0 Then Exit Sub
      If cbo.Text = _Prompt Then
        cbo.Text = ""
        cbo.ForeColor = Color.Black
      End If
    Else
      Throw New Exception("IntelliComboType must be defined before calling cbo_KeyDown! ")
    End If
  End Sub

  Private Sub cbo_TextChanged(sender As Object, e As EventArgs) Handles cbo.TextChanged
    If _HandlingKeyDown = True Then Exit Sub
    If _IntelliComboType = enmIntelliComboType.Dumb Then
      cbo.ForeColor = Color.Black
      If cbo.Text = "" Then
        cbo.Text = _Prompt
      ElseIf cbo.Text = _Prompt Then
        cbo.ForeColor = Color.DimGray
      End If
    Else
      'now remove commas if it's numerical
      If cbo.Text.IndexOf(",") > 0 Then
        Dim pCheckNumerical = cbo.Text.Replace(",", "")
        If ccHelper.IsNumeric(pCheckNumerical) Then
          cbo.Text = pCheckNumerical
        End If
      End If
    End If
  End Sub

End Class
