Public Class clsGridSetting
 
  Private _ID As Long
  Private _GridName As String
  Private _ColumnName As String
  Private _ColumnDisplayIndex As Integer
  Private _ColumnWidth As Integer
  Private _ColumnVisible As Boolean
  Private _ColumnRemoved As Boolean
  Private _LastSaved As Date
  Private _Tag As String

  Public Property [ID]() As Long
    Get
      Return Me._ID
    End Get
    Set(ByVal value As Long)
      Me._ID = value
    End Set
  End Property
  Public Property [GridName]() As String
    Get
      Return Me._GridName
    End Get
    Set(ByVal value As String)
      Me._GridName = value
    End Set
  End Property
  Public Property [ColumnName]() As String
    Get
      Return Me._ColumnName
    End Get
    Set(ByVal value As String)
      Me._ColumnName = value
    End Set
  End Property
  Public Property [ColumnDisplayIndex]() As Integer
    Get
      Return Me._ColumnDisplayIndex
    End Get
    Set(ByVal value As Integer)
      Me._ColumnDisplayIndex = value
    End Set
  End Property
  Public Property [ColumnWidth]() As Integer
    Get
      Return Me._ColumnWidth
    End Get
    Set(ByVal value As Integer)
      Me._ColumnWidth = value
    End Set
  End Property
  Public Property [ColumnVisible]() As Boolean
    Get
      Return Me._ColumnVisible
    End Get
    Set(ByVal value As Boolean)
      Me._ColumnVisible = value
    End Set
  End Property
  Public Property [ColumnRemoved]() As Boolean
    Get
      Return Me._ColumnRemoved
    End Get
    Set(ByVal value As Boolean)
      Me._ColumnRemoved = value
    End Set
  End Property
  Public Property [LastSaved]() As Date
    Get
      Return Me._LastSaved
    End Get
    Set(ByVal value As Date)
      Me._LastSaved = value
    End Set
  End Property
  Public Property [Tag]() As String
    Get
      Return Me._Tag
    End Get
    Set(ByVal value As String)
      Me._Tag = value
    End Set
  End Property

  'ToString
  Public Overrides Function ToString() As String
    Dim pValue As New System.Text.StringBuilder
    If _ID <> 0 Then pValue.Append("ID='" & _ID.ToString & "' ‡ ")
    If _GridName <> "" Then pValue.Append("GridName='" & _GridName & "' ‡ ")
    If _ColumnName <> "" Then pValue.Append("ColumnName='" & _ColumnName & "' ‡ ")
    If _ColumnDisplayIndex <> 0 Then pValue.Append("ColumnDisplayIndex='" & _ColumnDisplayIndex.ToString & "' ‡ ")
    If _ColumnWidth <> 0 Then pValue.Append("ColumnWidth='" & _ColumnWidth.ToString & "' ‡ ")
    pValue.Append("ColumnVisible='" & _ColumnVisible.ToString & "' ‡ ")
    pValue.Append("ColumnRemoved='" & _ColumnRemoved.ToString & "' ‡ ")
    If Not (_LastSaved = Nothing) Then pValue.Append("LastSaved='" & _LastSaved.ToString("yyyyMMddTHHmmssff") & "' ‡ ")
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ")
    Return pValue.ToString
  End Function

  'ToCSV 
  Public Function ToCSV() As String
    Dim pCSV As New System.Text.StringBuilder

    pCSV.Append("" & _ID.ToString & "")
    pCSV.Append(",""" & _GridName.Replace(ChrW(34), "''").Replace(",", " ") & """")
    pCSV.Append(",""" & _ColumnName.Replace(ChrW(34), "''").Replace(",", " ") & """")
    pCSV.Append("," & _ColumnDisplayIndex.ToString & "")
    pCSV.Append("," & _ColumnWidth.ToString & "")
    pCSV.Append(",""" & _ColumnVisible.ToString & """")
    pCSV.Append(",""" & _ColumnRemoved.ToString & """")
    pCSV.Append("," & _LastSaved.ToShortDateString & " " & _LastSaved.ToShortTimeString & "")
    pCSV.Append(",""" & _Tag.Replace(ChrW(34), "''").Replace(",", " ") & """")
    Return pCSV.ToString
  End Function

  Public Sub New()
    CreateEmpty()
  End Sub

  Public Sub New(ByVal vGridSetting As clsGridSetting)
    CreateEmpty()
    With vGridSetting
      _ID = .ID
      _GridName = .GridName
      _ColumnName = .ColumnName
      _ColumnDisplayIndex = .ColumnDisplayIndex
      _ColumnWidth = .ColumnWidth
      _ColumnVisible = .ColumnVisible
      _ColumnRemoved = .ColumnRemoved
      _LastSaved = .LastSaved
      _Tag = .Tag
    End With
  End Sub

  Public Sub New(
      ByVal vID As Long _
    , ByVal vGridName As String _
    , ByVal vColumnName As String _
    , ByVal vColumnDisplayIndex As Integer _
    , ByVal vColumnWidth As Integer _
    , ByVal vColumnVisible As Boolean _
    , ByVal vColumnRemoved As Boolean _
    , ByVal vLastSaved As Date _
    , ByVal vTag As String
)

    _ID = vID
    _GridName = vGridName
    _ColumnName = vColumnName
    _ColumnDisplayIndex = vColumnDisplayIndex
    _ColumnWidth = vColumnWidth
    _ColumnVisible = vColumnVisible
    _ColumnRemoved = vColumnRemoved
    _LastSaved = vLastSaved
    _Tag = vTag
  End Sub

  ''' <summary>
  ''' This checks if the objects are equal, IGNORING the dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function isEqual(ByVal vGridSettingToTest As clsGridSetting) As Boolean
    With vGridSettingToTest
      If _ID <> .ID Then Return False
      If _GridName <> .GridName Then Return False
      If _ColumnName <> .ColumnName Then Return False
      If _ColumnDisplayIndex <> .ColumnDisplayIndex Then Return False
      If _ColumnWidth <> .ColumnWidth Then Return False
      If _ColumnVisible <> .ColumnVisible Then Return False
      If _ColumnRemoved <> .ColumnRemoved Then Return False
      If _LastSaved <> .LastSaved Then Return False
      If _Tag <> .Tag Then Return False
    End With
    Return True
  End Function

  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsGridSetting
    Dim pClone As New clsGridSetting(Me)
    Return pClone
  End Function

  ''' <summary>
  ''' This assigns the intrinsic values of the item to the object
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub AssignValues(ByVal vGridSetting As clsGridSetting)
    With vGridSetting
      _ID = .ID
      _GridName = .GridName
      _ColumnName = .ColumnName
      _ColumnDisplayIndex = .ColumnDisplayIndex
      _ColumnWidth = .ColumnWidth
      _ColumnVisible = .ColumnVisible
      _ColumnRemoved = .ColumnRemoved
      _LastSaved = .LastSaved
      _Tag = .Tag
    End With
  End Sub

  Private Sub CreateEmpty()
    _ID = 0
    _GridName = ""
    _ColumnName = ""
    _ColumnDisplayIndex = 0
    _ColumnWidth = 0
    _ColumnVisible = False
    _ColumnRemoved = False
    _LastSaved = Nothing
    _Tag = ""
  End Sub
End Class

Public Class clsGridSettingCol
  Inherits Generic.List(Of clsGridSetting)
 
  Private Shared _ThisInstance As clsGridSettingCol

  'ToCSV 
  Public Function ToCSV() As String
    Dim pCSV As New Text.StringBuilder
    Dim pCSVTitle As New Text.StringBuilder
    'Get title 
    pCSVTitle.Append("""ID""")
    pCSVTitle.Append(",""GridName""")
    pCSVTitle.Append(",""ColumnName""")
    pCSVTitle.Append(",""ColumnDisplayIndex""")
    pCSVTitle.Append(",""ColumnWidth""")
    pCSVTitle.Append(",""ColumnVisible""")
    pCSVTitle.Append(",""ColumnRemoved""")
    pCSVTitle.Append(",""LastSaved""")
    pCSVTitle.Append(",""Tag""")

    pCSV.AppendLine(pCSVTitle.ToString)

    For Each pRow As clsGridSetting In Me
      pCSV.AppendLine(pRow.ToCSV)
    Next

    Return pCSV.ToString
  End Function

  Private Sub New()
    MyBase.New()
  End Sub

  Public Shared Function GetGridSettings(ByVal vGrid As Control, ByVal vRequester As clsRequester, ByRef rfault As clsFault) As clsGridSettingCol
    rfault = New clsFault
    rfault.SetOK()

    If _ThisInstance Is Nothing Then
      _ThisInstance = New clsGridSettingCol
      rfault = _ThisInstance.Fill(vRequester)
    End If

    Dim pGridName As String = CreateGridName(vGrid)

    Return _ThisInstance.CloneByGridName(pGridName)
  End Function

  Private Shared Function CreateGridName(ByVal vGrid As Control) As String
    Dim pName As String = ""

    Dim pControl As Control = vGrid

    Do
      If pName <> "" Then pName = "/" & pName
      pName = pControl.Name & pName
      If TypeOf (pControl) Is Form Then Exit Do
      pControl = pControl.Parent
      If pControl Is Nothing Then frmMessageOrInputBox.ShowMsg("The final owner must be a form. If this Fault occurs, then add the control to the form's controls collection before running", frmMessageOrInputBox.enmIconType.Exclamation)
    Loop

    Return pName
  End Function

  ''' <summary>
  ''' Gets a collection of all the items, or a sub-collection defined by HowMany and Direction
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("vWithParents={0}", "NoParents")
    Dim pFault As New clsFault

    Me.Clear()

    Dim pText As String = ""

    pText = My.Settings.GridInfo

    If pText Is Nothing OrElse pText.Trim.Length = 0 Then
      pFault.SetOK()
      Return pFault
    End If

    Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(GetType(clsGridSettingCol))
    Dim pStreamReader As New IO.StringReader(pText)
    Dim pGridSettingCol As clsGridSettingCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsGridSettingCol)

    'Target.Tools.LogToTextFile.WriteMessage(Environment.NewLine & pGridSettingCol.ToCSV, "GridSetting")

    For Each p In pGridSettingCol
      _ThisInstance.Add(p)
    Next

    pFault.SetOK()
    Return pFault
  End Function

  Private Sub FillFromArray(ByVal vGridSettingArray As clsGridSetting())
    Me.Clear()
    For Each pGridSetting As clsGridSetting In vGridSettingArray
      Me.Add(pGridSetting)
    Next
  End Sub

  Public Function isEqual(ByVal pGridSettingsToTest As clsGridSettingCol) As Boolean
    If Me.Count <> pGridSettingsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1
      If Me(i).isEqual(pGridSettingsToTest(i)) = False Then Return False
    Next
    Return True
  End Function

  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsGridSettingCol
    Dim pGridSettings As New clsGridSettingCol
    For Each pGridSetting As clsGridSetting In Me
      Dim pGridSettingClone As clsGridSetting = pGridSetting.Clone
      pGridSettings.Add(pGridSettingClone)
    Next
    Return pGridSettings
  End Function

  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FindByID(ByVal vID As Long) As clsGridSetting
    If Me.Count = 0 Then Return New clsGridSetting
    Static pSortedCol As clsGridSettingCol
    Static inProcess As Boolean = False 'check if in-process by another thread 
    If inProcess = True Then
      Do
        Threading.Thread.Sleep(1000)
      Loop Until inProcess = False
    End If
    If pSortedCol Is Nothing OrElse pSortedCol.Count <> Me.Count Then
      inProcess = True
      pSortedCol = New clsGridSettingCol
      For Each pGridSetting As clsGridSetting In Me
        pSortedCol.Add(pGridSetting)
      Next
      pSortedCol.SortByID()
      inProcess = False
    End If
    Dim pLower As Integer = 0
    Dim pUpper As Integer = Me.Count - 1
    Dim pMiddle As Integer = 0
    If vID = pSortedCol(pLower).ID Then
      Return pSortedCol(pLower)
    ElseIf vID = pSortedCol(pUpper).ID Then
      Return pSortedCol(pUpper)
    End If
    If Me.Count <= 2 Then
      Return New clsGridSetting
    End If
    Do
      pMiddle = ccHelper.ToInteger((pUpper - pLower) / 2) + pLower
      If vID > pSortedCol(pMiddle).ID Then
        pLower = pMiddle
      ElseIf vID < pSortedCol(pMiddle).ID Then
        pUpper = pMiddle
      ElseIf vID = pSortedCol(pMiddle).ID Then
        Return pSortedCol(pMiddle)
      End If
    Loop Until pUpper - pLower = 1
    'if we got here, we didn't find one
    Return New clsGridSetting
  End Function

  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function FindByGridNameAndColumnName(ByVal vGridName As String, ByVal vColumnName As String) As clsGridSetting
    If Me.Count = 0 Then Return New clsGridSetting
    Static pSortedCol As clsComboList
    Static inProcess As Boolean = False 'check if in-process by another thread 
    If inProcess = True Then
      Do
        Threading.Thread.Sleep(1000)
      Loop Until inProcess = False
    End If
    If pSortedCol Is Nothing OrElse pSortedCol.Count <> Me.Count Then
      inProcess = True
      pSortedCol = New clsComboList
      For Each pGridSetting As clsGridSetting In Me
        With pGridSetting
          pSortedCol.AddToEnd(.ID, .GridName & "|" & .ColumnName)
        End With
      Next
      pSortedCol.SortByText()
      inProcess = False
    End If
    Dim pLower As Integer = 0
    Dim pUpper As Integer = Me.Count - 1
    Dim pMiddle As Integer = 0
    Dim pValueIn As String = vGridName & "|" & vColumnName
    Dim pText As String
    Dim pGridSettingIdToReturn As Long = 0
    If String.Compare(pValueIn, pSortedCol(pLower).Text, StringComparison.OrdinalIgnoreCase) = 0 Then
      pGridSettingIdToReturn = pSortedCol(pLower).KeyLong
    ElseIf String.Compare(pValueIn, pSortedCol(pUpper).Text, StringComparison.OrdinalIgnoreCase) = 0 Then
      pGridSettingIdToReturn = pSortedCol(pUpper).KeyLong
    End If
    If pGridSettingIdToReturn = 0 Then
      If Me.Count <= 2 Then
        Return New clsGridSetting
      End If
      Do
        pMiddle = ccHelper.ToInteger((pUpper - pLower) / 2) + pLower
        pText = pSortedCol(pMiddle).Text
        If String.Compare(pValueIn, pText, StringComparison.OrdinalIgnoreCase) > 0 Then
          pLower = pMiddle
        ElseIf String.Compare(pValueIn, pText, StringComparison.OrdinalIgnoreCase) < 0 Then
          pUpper = pMiddle
        ElseIf String.Compare(pValueIn, pText, StringComparison.OrdinalIgnoreCase) = 0 Then
          pGridSettingIdToReturn = pSortedCol(pMiddle).KeyLong
          Exit Do
        End If
      Loop Until pUpper - pLower = 1
    End If
    If pGridSettingIdToReturn > 0 Then
      Return FindByID(pGridSettingIdToReturn)
    End If
    'if we got here, we didn't find one
    Return New clsGridSetting
  End Function

  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByColumnName(ByVal vColumnName As String) As clsGridSetting
    If Me.Count = 0 Then Return New clsGridSetting
    Static pSortedCol As clsGridSettingCol
    Static inProcess As Boolean = False 'check if in-process by another thread 
    If inProcess = True Then
      Do
        Threading.Thread.Sleep(1000)
      Loop Until inProcess = False
    End If
    If pSortedCol Is Nothing OrElse pSortedCol.Count <> Me.Count Then
      inProcess = True
      pSortedCol = New clsGridSettingCol
      For Each pPerson As clsGridSetting In Me
        pSortedCol.Add(pPerson)
      Next
      pSortedCol.SortByColumnName()
      inProcess = False
    End If
    Dim pLower As Integer = 0
    Dim pUpper As Integer = Me.Count - 1
    Dim pMiddle As Integer = 0
    Dim pColumnName As String
    If String.Compare(vColumnName, pSortedCol(pLower).ColumnName, StringComparison.OrdinalIgnoreCase) = 0 Then
      Return pSortedCol(pLower)
    ElseIf String.Compare(vColumnName, pSortedCol(pUpper).ColumnName, StringComparison.OrdinalIgnoreCase) = 0 Then
      Return pSortedCol(pUpper)
    End If
    If Me.Count <= 2 Then
      Return New clsGridSetting
    End If
    Do
      pMiddle = ccHelper.ToInteger((pUpper - pLower) / 2) + pLower
      pColumnName = pSortedCol(pMiddle).ColumnName
      If String.Compare(vColumnName, pColumnName, StringComparison.OrdinalIgnoreCase) > 0 Then
        pLower = pMiddle
      ElseIf String.Compare(vColumnName, pColumnName, StringComparison.OrdinalIgnoreCase) < 0 Then
        pUpper = pMiddle
      ElseIf String.Compare(vColumnName, pColumnName, StringComparison.OrdinalIgnoreCase) = 0 Then
        Return pSortedCol(pMiddle)
      End If
    Loop Until pUpper - pLower = 1
    'if we got here, we didn't find one
    Return New clsGridSetting
  End Function

  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined GridName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function CloneByGridName(ByVal vGridName As String) As clsGridSettingCol
    Dim pGridSettings As New clsGridSettingCol
    For Each pGridSetting As clsGridSetting In Me
      If pGridSetting.GridName = vGridName Then
        Dim pGridSettingClone As clsGridSetting = pGridSetting.Clone
        pGridSettings.Add(pGridSettingClone)
      End If
    Next
    Return pGridSettings
  End Function

  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ColumnName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function CloneByColumnName(ByVal vColumnName As String) As clsGridSettingCol
    Dim pGridSettings As New clsGridSettingCol
    For Each pGridSetting As clsGridSetting In Me
      If pGridSetting.ColumnName = vColumnName Then
        Dim pGridSettingClone As clsGridSetting = pGridSetting.Clone
        pGridSettings.Add(pGridSettingClone)
      End If
    Next
    Return pGridSettings
  End Function

  Private Function Update(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault

    Try
      Dim pSerializer As Xml.Serialization.XmlSerializer
      pSerializer = New Xml.Serialization.XmlSerializer(GetType(clsGridSettingCol))
      Dim pString As New IO.StringWriter()
      pSerializer.Serialize(pString, _ThisInstance)
      My.Settings.GridInfo = pString.ToString
      My.Settings.Save()
      pString.Close()
      pFault.SetOK()
    Catch ex As Exception
      pFault.LogException(ex, "", "TRGT-120225-1044", vRequester)
    End Try

    Return pFault
  End Function

  Public Function Update(ByVal vGrid As Control, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault

    Dim pGridName As String = CreateGridName(vGrid)

    'Define TagValue to kill missing items
    Dim pTagTest As String = DateTime.Now.ToString("yyMMddHHmmssfff")

    'Get the last ID
    Dim pNewID As Long = 0
    If _ThisInstance.Count = 0 Then
      pNewID = 1
    Else
      _ThisInstance.SortByID()
      _ThisInstance.Reverse()
      pNewID = _ThisInstance(0).ID + 1
    End If

    'Find what to update
    For Each p In Me
      Dim pGridSetting As clsGridSetting = _ThisInstance.FindByGridNameAndColumnName(pGridName, p.ColumnName)

      pGridSetting.ColumnDisplayIndex = p.ColumnDisplayIndex
      pGridSetting.ColumnVisible = p.ColumnVisible
      pGridSetting.ColumnRemoved = p.ColumnRemoved
      pGridSetting.ColumnWidth = p.ColumnWidth
      pGridSetting.LastSaved = Now
      pGridSetting.Tag = pTagTest
      If pGridSetting.GridName = "" Then
        pGridSetting.GridName = pGridName
        pGridSetting.ColumnName = p.ColumnName
        pGridSetting.ID = pNewID
        pNewID += 1
        _ThisInstance.Add(pGridSetting)
      End If
    Next

    'Now remove what has to be removed....
    Dim pGridSettings As clsGridSettingCol = _ThisInstance.CloneByGridName(pGridName)
    For Each p In pGridSettings
      If p.Tag <> pTagTest Then
        Dim pToKill As clsGridSetting = _ThisInstance.FindByGridNameAndColumnName(pGridName, p.ColumnName)
        _ThisInstance.Remove(pToKill)
      End If
    Next

    pFault = _ThisInstance.Update(vRequester)

    Return pFault
  End Function

  ''' <summary>
  ''' Deletes a collection of all the items for a specific GridName 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function DeleteByGridName(ByVal vGridName As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("GridName={0}", vGridName)
    Dim pFault As New clsFault

    Dim pToDelete As clsGridSettingCol = Me.CloneByGridName(vGridName)

    For Each p In pToDelete
      Dim pGS As clsGridSetting = _ThisInstance.FindByGridNameAndColumnName(p.GridName, p.ColumnName)
      If pGS.GridName = "" Then Continue For
      _ThisInstance.Remove(pGS)
    Next

    _ThisInstance.Update(vRequester)

    Return pFault
  End Function

  Public Sub SortByID()
    Me.Sort(New clsGridSettingCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsGridSetting)
    Private Function Compare(ByVal x As clsGridSetting, ByVal y As clsGridSetting) As Integer Implements System.Collections.Generic.IComparer(Of clsGridSetting).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ID < y.ID Then
        Return -1
      ElseIf x.ID = y.ID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class

  Public Sub SortByGridName()
    Me.Sort(New clsGridSettingCol.CompareByGridName)
  End Sub
  Private Class CompareByGridName
    Implements IComparer(Of clsGridSetting)
    Private Function Compare(ByVal x As clsGridSetting, ByVal y As clsGridSetting) As Integer Implements System.Collections.Generic.IComparer(Of clsGridSetting).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.GridName, y.GridName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class

  Public Sub SortByColumnName()
    Me.Sort(New clsGridSettingCol.CompareByColumnName)
  End Sub
  Private Class CompareByColumnName
    Implements IComparer(Of clsGridSetting)
    Private Function Compare(ByVal x As clsGridSetting, ByVal y As clsGridSetting) As Integer Implements System.Collections.Generic.IComparer(Of clsGridSetting).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ColumnName, y.ColumnName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class

  Public Sub SortByColumnDisplayIndex()
    Me.Sort(New clsGridSettingCol.CompareByColumnDisplayIndex)
  End Sub
  Private Class CompareByColumnDisplayIndex
    Implements IComparer(Of clsGridSetting)
    Private Function Compare(ByVal x As clsGridSetting, ByVal y As clsGridSetting) As Integer Implements System.Collections.Generic.IComparer(Of clsGridSetting).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ColumnDisplayIndex < y.ColumnDisplayIndex Then
        Return -1
      ElseIf x.ColumnDisplayIndex = y.ColumnDisplayIndex Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class

  Public Sub SortByColumnWidth()
    Me.Sort(New clsGridSettingCol.CompareByColumnWidth)
  End Sub
  Private Class CompareByColumnWidth
    Implements IComparer(Of clsGridSetting)
    Private Function Compare(ByVal x As clsGridSetting, ByVal y As clsGridSetting) As Integer Implements System.Collections.Generic.IComparer(Of clsGridSetting).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ColumnWidth < y.ColumnWidth Then
        Return -1
      ElseIf x.ColumnWidth = y.ColumnWidth Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class

  Public Sub SortByLastSaved()
    Me.Sort(New clsGridSettingCol.CompareByLastSaved)
  End Sub
  Private Class CompareByLastSaved
    Implements IComparer(Of clsGridSetting)
    Private Function Compare(ByVal x As clsGridSetting, ByVal y As clsGridSetting) As Integer Implements System.Collections.Generic.IComparer(Of clsGridSetting).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LastSaved < y.LastSaved Then
        Return -1
      ElseIf x.LastSaved = y.LastSaved Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class

  Public Sub SortByTag()
    Me.Sort(New clsGridSettingCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsGridSetting)
    Private Function Compare(ByVal x As clsGridSetting, ByVal y As clsGridSetting) As Integer Implements System.Collections.Generic.IComparer(Of clsGridSetting).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Tag, y.Tag, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class

End Class

