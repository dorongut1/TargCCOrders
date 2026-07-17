Imports System.ComponentModel

Public Class IntelliMultiCombo

  Public Event evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember)

  Private _MultiComboList As clsMultiComboList

  Private _IsDumb As Boolean

  Public Enum enmLayoutDirection
    Horizontal
    Vertical
    Undefined
  End Enum

  Private _NumCombos As Integer = 3

  Private _ComboSize01 As Size = New Size(100, 25)
  Private _ComboSize02 As Size = New Size(100, 25)
  Private _ComboSize03 As Size = New Size(100, 25)
  Private _ComboPrompt01 As String
  Private _ComboPrompt02 As String
  Private _ComboPrompt03 As String

  Private _LayoutDirection As enmLayoutDirection = enmLayoutDirection.Horizontal

  <Category("Layout"), _
      Description("Sets the size of the leftmost combobox"), _
      DefaultValue(GetType(Size), "158, 23")> _
  Public Property ComboSize01() As Size
    Get
      Return _ComboSize01
    End Get
    Set(ByVal value As Size)
      _ComboSize01 = value
      icbo01.Size = _ComboSize01
      Me.Invalidate()
    End Set
  End Property
  <Category("Layout"), _
      Description("Sets the size of the middle combobox"), _
      DefaultValue(GetType(Size), "158, 23")> _
  Public Property ComboSize02() As Size
    Get
      Return _ComboSize02
    End Get
    Set(ByVal value As Size)
      _ComboSize02 = value
      icbo02.Size = _ComboSize02
      Me.Invalidate()
    End Set
  End Property
  <Category("Layout"), _
      Description("Sets the size of the rightmost combobox"), _
      DefaultValue(GetType(Size), "158, 23")> _
  Public Property ComboSize03() As Size
    Get
      Return _ComboSize03
    End Get
    Set(ByVal value As Size)
      _ComboSize03 = value
      icbo03.Size = _ComboSize03
      Me.Invalidate()
    End Set
  End Property
  Public Property DropDownStyle() As System.Windows.Forms.ComboBoxStyle
    Get
      Return icbo03.DropDownStyle
    End Get
    Set(ByVal value As System.Windows.Forms.ComboBoxStyle)
      If _IsDumb = False Then
        If value = ComboBoxStyle.DropDownList Then Throw New Exception("Cannot use DropDownList when Combos are smart")
      End If
      icbo01.DropDownStyle = value
      icbo02.DropDownStyle = value
      icbo03.DropDownStyle = value
    End Set
  End Property
  <Category("Layout"), _
      Description("Defines how many comboboxes"), _
      DefaultValue(GetType(Integer), "3")> _
  Public Property NumCombos() As Integer
    Get
      Return _NumCombos
    End Get
    Set(ByVal value As Integer)
      _NumCombos = value
      If _NumCombos = 2 Then
        icbo01.Visible = False
      Else
        icbo01.Visible = True
      End If
      Me.Invalidate()
    End Set
  End Property
  <Category("Layout"), _
      Description("Defines Direction"), _
      DefaultValue(GetType(enmLayoutDirection), "Horizontal")> _
  Public Property LayoutDirection() As enmLayoutDirection
    Get
      Return _LayoutDirection
    End Get
    Set(ByVal value As enmLayoutDirection)
      _LayoutDirection = value
      If _LayoutDirection = enmLayoutDirection.Vertical Then
        Me.flp.SetFlowBreak(Me.icbo01, True)
        Me.flp.SetFlowBreak(Me.icbo02, True)
        Me.flp.SetFlowBreak(Me.icbo03, True)
      Else
        Me.flp.SetFlowBreak(Me.icbo01, False)
        Me.flp.SetFlowBreak(Me.icbo02, False)
        Me.flp.SetFlowBreak(Me.icbo03, False)
      End If
      Me.Invalidate()
    End Set
  End Property
  Public ReadOnly Property IsDumb() As Boolean
    Get
      Return _IsDumb
    End Get
  End Property
  Public ReadOnly Property IsLoaded() As Boolean
    Get
      If _MultiComboList IsNot Nothing Then
        Return True
      Else
        Return False
      End If
    End Get
  End Property

  Public ReadOnly Property SelectedItem() As clsComboListMember
    Get
      Return CType(icbo03.SelectedItem, clsComboListMember)
    End Get
  End Property

  Public ReadOnly Property SelectedIndex() As Integer
    Get
      Return icbo03.SelectedIndex
    End Get
  End Property
  Public ReadOnly Property SelectedValue() As Object
    Get
      Return icbo03.SelectedValue
    End Get
  End Property


  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    _IsDumb = False
  End Sub

  ''' <summary>
  ''' The text expected is of the format ID01|Text01|ID02|Text02|ID03|Text03|. The ID will be a copy of ID03. If there are only two combos, then 01 is not sent.
  ''' The prompt expected is of the format Prompt01|Prompt02|Prompt03|. If there are only two combos, then 01 is not sent.
  ''' If there are no delimiters, then we can assume 1 combobox only (cbo03)
  ''' </summary>
  ''' <param name="vComboList"></param>
  ''' <param name="vComboPrompt"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadControl(ByVal vComboList As clsComboList, ByVal vComboPrompt As String) As String
    Dim pResponse As String = ""
    If vComboList Is Nothing Then
      Throw New Exception("ComboList is Nothing")
    End If

    'Create The Combolist
    _MultiComboList = New clsMultiComboList
    pResponse = _MultiComboList.Load(vComboList)
    If pResponse <> "OK" Then Return pResponse

    If _MultiComboList.NumberOfCombos = 1 Then
      icbo01.Visible = False
      icbo02.Visible = False
      _ComboPrompt01 = ""
      _ComboPrompt02 = ""
      _ComboPrompt03 = vComboPrompt
      LoadCbo03(0)
    ElseIf _MultiComboList.NumberOfCombos = 2 Then
      icbo01.Visible = False
      icbo02.Visible = True
      _ComboPrompt01 = ""
      _ComboPrompt02 = vComboPrompt.Split("|"c)(0)
      _ComboPrompt03 = vComboPrompt.Split("|"c)(1)
      LoadCbo02(0)
    ElseIf _MultiComboList.NumberOfCombos = 3 Then
      icbo01.Visible = True
      icbo02.Visible = True
      _ComboPrompt01 = vComboPrompt.Split("|"c)(0)
      _ComboPrompt02 = vComboPrompt.Split("|"c)(1)
      _ComboPrompt03 = vComboPrompt.Split("|"c)(2)
      LoadCbo01()
    End If
    Return pResponse
  End Function

  Public Sub MakeDumb()
    If _IsDumb = False Then
      _IsDumb = True
      icbo01.MakeDumb()
      icbo02.MakeDumb()
      icbo03.MakeDumb()
    End If
  End Sub
  Public Sub MakeSmart()
    If _IsDumb = True Then
      If icbo03.DropDownStyle = ComboBoxStyle.DropDownList Then
        Throw New Exception("Cannot use DropDownList when Combo is smart")
      End If
      _IsDumb = False
      icbo01.MakeSmart()
      icbo02.MakeSmart()
      icbo03.MakeSmart()
    End If
  End Sub
  Public Function ValueSelect(ByVal vValue As Long) As String

    'Find the row in the list
    Dim pFound As Boolean = False
    For Each pL As clsMultiComboListLine In _MultiComboList
      If pL.ID03 = vValue Then
        pFound = True
        If _MultiComboList.NumberOfCombos = 2 Then
          icbo02.ValueSelect(pL.ID02)
          LoadCbo03(pL.ID02)
          icbo03.ValueSelect(pL.ID03)
        Else
          icbo01.ValueSelect(pL.ID01)
          LoadCbo02(pL.ID01)
          icbo02.ValueSelect(pL.ID02)
          LoadCbo03(pL.ID02)
          icbo03.ValueSelect(pL.ID03)
        End If
        Exit For
      End If
    Next

    If pFound = False Then
      Return "Value not found"
    End If

    Return "OK"
  End Function
  Public Sub ValueClear()
    If _MultiComboList.NumberOfCombos = 3 Then
      icbo01.ValueClear()
    End If
    LoadCbo02(-1)
    LoadCbo03(-1)
    icbo02.ValueClear()
    icbo03.ValueClear()
  End Sub

  Private Sub LoadCbo01()
    Dim pComboList01 As clsComboList
    pComboList01 = New clsComboList
    Dim pLastID As Long = -1
    For Each pMCL As clsMultiComboListLine In _MultiComboList
      If pMCL.ID01 <> pLastID Then
        pComboList01.AddToEnd(pMCL.ID01, pMCL.Text01)
        pLastID = pMCL.ID01
      End If
    Next
    icbo01.LoadControl(pComboList01, _ComboPrompt01)
    LoadCbo02(-1)
  End Sub
  Private Sub LoadCbo02(ByVal vChosenID01 As Long)
    Dim pComboList02 As clsComboList
    pComboList02 = New clsComboList
    If vChosenID01 <> -1 Then
      Dim pLastID As Long = -1
      For Each pMCL As clsMultiComboListLine In _MultiComboList
        If vChosenID01 <> -1 AndAlso vChosenID01 <> pMCL.ID01 Then Continue For
        If pMCL.ID02 <> pLastID Then
          pComboList02.AddToEnd(pMCL.ID02, pMCL.Text02)
          pLastID = pMCL.ID02
        End If
      Next
    End If
    icbo02.LoadControl(pComboList02, _ComboPrompt02)
    LoadCbo03(-1)
  End Sub
  Private Sub LoadCbo03(ByVal vChosenID02 As Long)
    Dim pComboList03 As clsComboList
    pComboList03 = New clsComboList
    If vChosenID02 <> -1 Then
      For Each pMCL As clsMultiComboListLine In _MultiComboList
        If vChosenID02 <> -1 AndAlso vChosenID02 <> pMCL.ID02 Then Continue For
        pComboList03.AddToEnd(pMCL.ID03, pMCL.Text03)
      Next
    End If
    icbo03.LoadControl(pComboList03, _ComboPrompt03)
  End Sub


  Private Sub icbo01_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles icbo01.evtComboListMemberChosen
    Dim pID01 As Long
    If vComboListMember Is Nothing Then
      pID01 = -1
    Else
      pID01 = ccHelper.ToLong(vComboListMember.KeyLong)
    End If
    LoadCbo02(pID01)
  End Sub
  Private Sub icbo02_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles icbo02.evtComboListMemberChosen
    Dim pID02 As Long
    If vComboListMember Is Nothing Then
      pID02 = -1
    Else
      pID02 = ccHelper.ToLong(vComboListMember.KeyLong)
    End If
    LoadCbo03(pID02)
  End Sub
  Private Sub icbo03_evtComboListMemberChosen(ByVal vComboListMember As clsComboListMember) Handles icbo03.evtComboListMemberChosen
    RaiseEvent evtComboListMemberChosen(vComboListMember)
  End Sub

  Private Sub IntelliMultiCombo_Load(sender As Object, e As EventArgs) Handles Me.Load
    If Me.DesignMode = True Then Exit Sub
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont
    Me.PerformAutoScale()

  End Sub

End Class


Friend Class clsMultiComboListLine

  Private _ID01 As Long
  Private _Text01 As String
  Private _ID02 As Long
  Private _Text02 As String
  Private _ID03 As Long
  Private _Text03 As String

  Friend Property ID01() As Long
    Get
      Return _ID01
    End Get
    Set(ByVal value As Long)
      _ID01 = value
    End Set
  End Property
  Friend Property Text01() As String
    Get
      Return _Text01
    End Get
    Set(ByVal value As String)
      _Text01 = value
    End Set
  End Property
  Friend Property ID02() As Long
    Get
      Return _ID02
    End Get
    Set(ByVal value As Long)
      _ID02 = value
    End Set
  End Property
  Friend Property Text02() As String
    Get
      Return _Text02
    End Get
    Set(ByVal value As String)
      _Text02 = value
    End Set
  End Property
  Friend Property ID03() As Long
    Get
      Return _ID03
    End Get
    Set(ByVal value As Long)
      _ID03 = value
    End Set
  End Property
  Friend Property Text03() As String
    Get
      Return _Text03
    End Get
    Set(ByVal value As String)
      _Text03 = value
    End Set
  End Property

  Friend Sub New()
    CreateEmpty()
  End Sub

  Private Sub CreateEmpty()
    _ID01 = -1
    _Text01 = ""
    _ID02 = -1
    _Text02 = ""
    _ID03 = -1
    _Text03 = ""
  End Sub

End Class

Friend Class clsMultiComboList
  Inherits Generic.List(Of clsMultiComboListLine)

  Private _NumberOfCombos As Integer

  Friend ReadOnly Property NumberOfCombos() As Integer
    Get
      Return _NumberOfCombos
    End Get
  End Property

  Friend Function Load(ByVal vComboList As clsComboList) As String
    Dim pStrg As String = ""
    _NumberOfCombos = 0

    If vComboList Is Nothing OrElse vComboList.Count = 0 Then
      Return "Empty ComboList"
    End If

    pStrg = vComboList(0).Text
    If pStrg.Split("|"c).Length = 1 Then
      _NumberOfCombos = 1
    ElseIf pStrg.Split("|"c).Length = 5 Then
      _NumberOfCombos = 2
    ElseIf pStrg.Split("|"c).Length = 7 Then
      _NumberOfCombos = 3
    Else
      Return "Invalid input combolist: " & pStrg
    End If

    For Each fMember As clsComboListMember In vComboList
      Dim pLine As New clsMultiComboListLine
      Try
        With pLine
          If _NumberOfCombos = 1 Then
            .ID03 = fMember.KeyLong
            .Text03 = fMember.Text
          ElseIf _NumberOfCombos = 2 Then
            .ID02 = ccHelper.ToLong(fMember.Text.Split("|"c)(0))
            .Text02 = CStr(fMember.Text.Split("|"c)(1))
            .ID03 = ccHelper.ToLong(fMember.Text.Split("|"c)(2))
            .Text03 = CStr(fMember.Text.Split("|"c)(3))
          ElseIf _NumberOfCombos = 3 Then
            .ID01 = ccHelper.ToLong(fMember.Text.Split("|"c)(0))
            .Text01 = CStr(fMember.Text.Split("|"c)(1))
            .ID02 = ccHelper.ToLong(fMember.Text.Split("|"c)(2))
            .Text02 = CStr(fMember.Text.Split("|"c)(3))
            .ID03 = ccHelper.ToLong(fMember.Text.Split("|"c)(4))
            .Text03 = CStr(fMember.Text.Split("|"c)(5))
          End If
          If .ID03 <> fMember.KeyLong Then
            Return "Line Translation Failed: ID's don't match. Text=" & fMember.Text & "; ID=" & fMember.KeyLong
          End If
        End With
        Me.Add(pLine)
      Catch ex As Exception
        Return "Line Translation Failed: " & fMember.Text
      End Try
    Next

    Return "OK"
  End Function

End Class
