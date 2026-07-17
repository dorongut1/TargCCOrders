<Serializable> 
<DebuggerDisplay("Text={Text}, Key={Key}, Type={KeyTypeName} (DebuggerDisplay Sample)")> 
Public Class clsComboListMember 
 
  Public Event evtChanged() 
 
  Private _KeyType As clsEnums.enmComboListKeyType 
  Private _KeyTypeName As String 
 
  Private _KeyLong As Long 
  Private _KeyInteger As Integer 
  Private _KeyString As String 
  Private _KeyEnum As [Enum] 
  Private _KeyObject As Object 
 
  Private _Text As String 
  <NonSerialized> 
  Private _Tag As String 
 
  Public ReadOnly Property KeyType As clsEnums.enmComboListKeyType 
    Get 
      Return _KeyType 
    End Get 
  End Property 
  Public ReadOnly Property KeyTypeName As String 
    Get 
      Return _KeyTypeName 
    End Get 
  End Property 
 
  Public Property KeyLong As Long 
    Get 
      If _KeyType <> clsEnums.enmComboListKeyType.Long Then 
        If _KeyType = clsEnums.enmComboListKeyType.UD Then Return ccHelper.ToLong(-1) 
        Dim pMessage As String = "Invalid data type requested. I am a " & _KeyType.FastToString() & ", but you requested as Long." 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2005", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2005") 
      End If 
      Return _KeyLong 
    End Get 
    Set(value As Long)  
      If _KeyType = clsEnums.enmComboListKeyType.UD Then  
        _KeyType = clsEnums.enmComboListKeyType.Long  
        _KeyTypeName = "Long"  
      ElseIf _KeyType <> clsEnums.enmComboListKeyType.Long Then  
        Dim pMessage As String = "Invalid data type received. Received Long when I expected " & _KeyType.FastToString()  
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2006", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2006") 
      End If  
      _KeyLong = value  
    End Set  
  End Property  
  Public Property KeyInteger As Integer  
    Get 
      If _KeyType <> clsEnums.enmComboListKeyType.Integer Then 
        If _KeyType = clsEnums.enmComboListKeyType.UD Then Return ccHelper.ToInteger(-1) 
        Dim pMessage As String = "Invalid data type requested. I am a " & _KeyType.FastToString() & ", but you requested as Integer." 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2007", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2007") 
      End If 
      Return _KeyInteger 
    End Get  
    Set(value As Integer)  
      If _KeyType = clsEnums.enmComboListKeyType.UD Then  
        _KeyType = clsEnums.enmComboListKeyType.Integer  
        _KeyTypeName = "Integer"  
      ElseIf _KeyType <> clsEnums.enmComboListKeyType.Integer Then  
        Dim pMessage As String = "Invalid data type received. Received Integer when I expected " & _KeyType.FastToString()  
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2008", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2008") 
      End If  
      _KeyInteger = value  
    End Set  
  End Property  
  Public Property KeyString As String  
    Get 
      If _KeyType <> clsEnums.enmComboListKeyType.String Then 
        If _KeyType = clsEnums.enmComboListKeyType.UD Then Return "" 
        Dim pMessage As String = "Invalid data type requested. I am a " & _KeyType.FastToString() & ", but you requested as String." 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2009", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2009") 
      End If 
      Return _KeyString 
    End Get  
    Set(value As String)  
      If _KeyType = clsEnums.enmComboListKeyType.UD Then  
        _KeyType = clsEnums.enmComboListKeyType.String  
        _KeyTypeName = "String"  
      ElseIf _KeyType <> clsEnums.enmComboListKeyType.String Then  
        Dim pMessage As String = "Invalid data type received. Received String when I expected " & _KeyType.FastToString()  
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2010", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2010") 
      End If  
      _KeyString = value  
    End Set  
  End Property  
  Public Property KeyEnum As [Enum]  
    Get 
      If _KeyType <> clsEnums.enmComboListKeyType.Enum Then 
        Dim pMessage As String = "Invalid data type requested. I am a " & _KeyType.FastToString() & ", but you requested as Enum." 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2011", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2011") 
      End If 
      Return _KeyEnum 
    End Get  
    Set(value As [Enum])  
      If _KeyType = clsEnums.enmComboListKeyType.UD Then  
        _KeyType = clsEnums.enmComboListKeyType.Enum  
        _KeyTypeName = "Enum"  
      ElseIf _KeyType <> clsEnums.enmComboListKeyType.Enum Then  
        Dim pMessage As String = "Invalid data type received. Received Enum when I expected " & _KeyType.FastToString()  
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2012", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2012") 
      End If  
      _KeyEnum = value  
    End Set  
  End Property 
  Public Property KeyObject As Object 
    Get 
      If _KeyType <> clsEnums.enmComboListKeyType.Object Then 
        Dim pMessage As String = "Invalid data type requested. I am a " & _KeyType.FastToString() & ", but you requested as Object." 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2013", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2013") 
      End If 
      Return _KeyObject 
    End Get 
    Set(value As Object) 
      If _KeyType = clsEnums.enmComboListKeyType.UD Then 
        _KeyType = clsEnums.enmComboListKeyType.Object 
        _KeyTypeName = "Object" 
      ElseIf _KeyType <> clsEnums.enmComboListKeyType.Object Then 
        Dim pMessage As String = "Invalid data type received. Received Object when I expected " & _KeyType.FastToString() 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2014", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2014") 
      End If 
      _KeyObject = value 
    End Set 
  End Property 
 
  ''' <summary> 
  ''' This property exists for convenience only. To avoid boxing and unboxing, use one of the KeyXXXX properties 
  ''' </summary> 
  ''' <returns></returns> 
  Public ReadOnly Property Key As Object 
    Get 
      Select Case _KeyType 
        Case clsEnums.enmComboListKeyType.Long : Return _KeyLong 
        Case clsEnums.enmComboListKeyType.Integer : Return _KeyInteger 
        Case clsEnums.enmComboListKeyType.String : Return _KeyString 
        Case clsEnums.enmComboListKeyType.Enum : Return _KeyEnum 
        Case clsEnums.enmComboListKeyType.Object : Return _KeyObject 
        Case Else : Return "" 
      End Select 
    End Get 
  End Property 
 
  Public Property Text() As String 
    Get 
      Return Me._Text 
    End Get 
    Set(ByVal value As String) 
      Me._Text = value 
    End Set 
  End Property 
  <Xml.Serialization.XmlIgnore> 
  Public Property Tag() As String 
    Get 
      Return Me._Tag 
    End Get 
    Set(ByVal value As String) 
      Me._Tag = value 
    End Set 
  End Property 
 
  Public Sub New() 
    CreateEmpty() 
  End Sub 
  Public Sub New(vKey As Long, vText As String) 
    CreateEmpty() 
 
    _KeyType = clsEnums.enmComboListKeyType.Long 
    _KeyTypeName = "Long" 
 
    _KeyLong = vKey 
    _Text = vText 
  End Sub 
  Public Sub New(vKey As Integer, vText As String) 
    CreateEmpty() 
 
    _KeyType = clsEnums.enmComboListKeyType.Integer 
    _KeyTypeName = "Integer" 
 
    _KeyInteger = vKey 
    _Text = vText 
  End Sub 
  Public Sub New(vKey As String, vText As String) 
    CreateEmpty() 
 
    _KeyType = clsEnums.enmComboListKeyType.String 
    _KeyTypeName = "String" 
 
    _KeyString = vKey 
    _Text = vText 
  End Sub 
  Public Sub New(vKey As System.Enum, ByVal vEnumType As clsEnums.enmEnum, vText As String) 
    CreateEmpty() 
 
    _KeyType = clsEnums.enmComboListKeyType.Enum 
    _KeyTypeName = vEnumType.GetType.FullName.Replace("+enmEnum", "+enm" + vEnumType.FastToString()) 
 
    _KeyEnum = vKey 
    _Text = vText 
  End Sub 
  Public Sub New(vKey As Object, vText As String) 
    CreateEmpty() 
 
    _KeyType = clsEnums.enmComboListKeyType.Object 
    _KeyTypeName = vKey.GetType.Name 
 
    _KeyObject = vKey 
    _Text = vText 
  End Sub 
  Public Sub New(ByVal vKeyType As clsEnums.enmComboListKeyType, ByVal vKeyTypeName As String, ByVal vBytes As Byte(), ByVal vFault As clsFault, ByVal vRequester As clsRequester) 
    CreateEmpty() 
    LoadByteArray(vKeyType, vKeyTypeName, vBytes, vFault, vRequester) 
  End Sub 
 
  ''' <summary> 
  ''' This clones the object, returning an exact replica excluding dependants 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function Clone() As clsComboListMember 
    Dim pComboListMemberClone As New clsComboListMember 
    With pComboListMemberClone 
      ._KeyType = _KeyType 
      ._KeyTypeName = _KeyTypeName 
      Select Case _KeyType 
        Case clsEnums.enmComboListKeyType.Long : .KeyLong = _KeyLong 
        Case clsEnums.enmComboListKeyType.Integer : .KeyInteger = _KeyInteger 
        Case clsEnums.enmComboListKeyType.String : .KeyString = _KeyString 
        Case clsEnums.enmComboListKeyType.Enum : .KeyEnum = _KeyEnum 
        Case clsEnums.enmComboListKeyType.Object : .KeyObject = _KeyObject 
      End Select 
      .Text = _Text 
      .Tag = _Tag 
    End With 
    Return pComboListMemberClone 
  End Function 
  
  Public Function CreateByteArray(ByVal vFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    vFault.ClearOK() 
    Dim pBytes As Byte() = Nothing 
    Try 
      If vFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          If _KeyType = clsEnums.enmComboListKeyType.String Then 
            pBinaryWriter.Write(_KeyString) 
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Enum Then 
            pBinaryWriter.Write(_KeyEnum.ToString()) 
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer Then 
            pBinaryWriter.Write(_KeyInteger) 
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Long Then 
            pBinaryWriter.Write(_KeyLong) 
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Object Then 
            If TypeOf _KeyObject Is String Then 
              pBinaryWriter.Write("S") 
              pBinaryWriter.Write(_KeyObject.ToString()) 
            ElseIf _KeyObject.GetType.IsEnum = True Then 
              pBinaryWriter.Write("E") 
              pBinaryWriter.Write(_KeyObject.ToString()) 
            ElseIf TypeOf _KeyObject Is Integer Then 
              pBinaryWriter.Write("I") 
              pBinaryWriter.Write(ccHelper.ToInteger(_KeyObject)) 
            ElseIf TypeOf _KeyObject Is Long Then 
              pBinaryWriter.Write("L") 
              pBinaryWriter.Write(ccHelper.ToLong(_KeyObject)) 
            End If 
          End If 
          pBinaryWriter.Write(_Text) 
          pBinaryWriter.Write(_Tag) 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      vFault.SetOK() 
    Catch ex As Exception 
      vFault.LogException(ex, pFunctionParameters, "TRGT-150424-1214", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Private Sub LoadByteArray(ByVal vKeyType As clsEnums.enmComboListKeyType, ByVal vKeyTypeName As String, ByVal vBytes As Byte(), ByVal vFault As clsFault, ByVal vRequester As clsRequester) 
 
    vFault.ClearOK() 
    Try 
      If vFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          _KeyType = vKeyType 
          _KeyTypeName = vKeyTypeName 
 
          If _KeyType = clsEnums.enmComboListKeyType.String Then 
            _KeyString = pReader.ReadString 
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Enum Then 
            _KeyEnum = clsEnums.TranslateToEnum(_KeyTypeName, pReader.ReadString()) 
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer Then 
            _KeyInteger = pReader.ReadInt32 
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Long Then 
            _KeyLong = pReader.ReadInt64 
          ElseIf _KeyType = clsEnums.enmComboListKeyType.Object Then 
            Dim pObjectType As String = pReader.ReadString 
            If pObjectType = "S" Then 
              _KeyObject = pReader.ReadString 
            ElseIf pObjectType = "E" Then 
              _KeyObject = pReader.ReadString 
            ElseIf pObjectType = "I" Then 
              _KeyObject = pReader.ReadInt32 
            ElseIf pObjectType = "L" Then 
              _KeyObject = pReader.ReadInt64 
            End If 
          End If 
          _Text = pReader.ReadString 
          _Tag = pReader.ReadString  
          pReader.Close()  
        End Using 
        pMemoryStream.Close() 
      End Using 
      vFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      vFault.LogException(ex, "", "TRGT-150424-1219", vRequester) 
    End Try 
  End Sub 
 
  'ToString   
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
 
    pValue.Append("KeyType='" & _KeyType.FastToString() & "' ‡ ") 
    If String.IsNullOrEmpty(_KeyTypeName) Then pValue.Append("KeyTypeName='" & _KeyTypeName & "' ‡ ") 
    If _KeyType = clsEnums.enmComboListKeyType.Enum Then 
      pValue.Append("KeyEnum='" & _KeyEnum.ToString() & "' ‡ ") 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer Then 
      pValue.Append("KeyInteger='" & _KeyInteger.ToString() & "' ‡ ") 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Long Then 
      pValue.Append("KeyLong='" & _KeyLong.ToString() & "' ‡ ") 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Object Then 
      pValue.Append("KeyObject='" & _KeyObject.ToString() & "' ‡ ") 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.String Then 
      pValue.Append("KeyString='" & _KeyString & "' ‡ ") 
    End If 
 
    If _Text <> "" Then pValue.Append("Text='" & _Text & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    Return pValue.ToString() 
  End Function 
 
  Private Sub CreateEmpty() 
    _KeyType = clsEnums.enmComboListKeyType.UD 
    _KeyTypeName = "" 
 
    _KeyLong = 0 
    _KeyInteger = 0 
    _KeyString = "" 
    _KeyEnum = Nothing 
    _KeyObject = Nothing 
 
    _Text = "" 
    _Tag = "" 
  End Sub 
 
End Class 
 
<Serializable> 
Public Class clsComboList 
  Inherits Generic.List(Of clsComboListMember) 
 
  Private _SortedDictionaryForFindByKeyLong As Dictionary(Of Long, clsComboListMember) 
  Private _SortedDictionaryForFindByKeyInteger As Dictionary(Of Integer, clsComboListMember) 
  Private _SortedDictionaryForFindByKeyString As Dictionary(Of String, clsComboListMember) 
  Private _SortedDictionaryForFindByKeyEnum As Dictionary(Of [Enum], clsComboListMember) 
  Private _SortedDictionaryForFindByKeyObject As Dictionary(Of Object, clsComboListMember) 
 
  Private _SortedDictionaryForFindByTextReturnsLong As Dictionary(Of String, Long) 
  Private _SortedDictionaryForFindByTextReturnsInteger As Dictionary(Of String, Integer) 
  Private _SortedDictionaryForFindByTextReturnsString As Dictionary(Of String, String) 
  Private _SortedDictionaryForFindByTextReturnsEnum As Dictionary(Of String, [Enum]) 
  Private _SortedDictionaryForFindByTextReturnsObject As Dictionary(Of String, Object) 
 
  Private _SortedDictionaryForFindByTagReturnsLong As Dictionary(Of String, Long) 
  Private _SortedDictionaryForFindByTagReturnsInteger As Dictionary(Of String, Integer) 
  Private _SortedDictionaryForFindByTagReturnsString As Dictionary(Of String, String) 
  Private _SortedDictionaryForFindByTagReturnsEnum As Dictionary(Of String, [Enum]) 
  Private _SortedDictionaryForFindByTagReturnsObject As Dictionary(Of String, Object) 
 
  Private _CollectionLock As New Object() 
  Private _RecreateDictionaryForFindByKey As Boolean 
  Private _RecreateDictionaryForFindByText As Boolean 
  Private _RecreateDictionaryForFindByTag As Boolean 
 
  Private _KeyType As clsEnums.enmComboListKeyType 
  Private _KeyTypeName As String 
 
  Public ReadOnly Property KeyType As clsEnums.enmComboListKeyType 
    Get 
      Return _KeyType 
    End Get 
  End Property 
  Public ReadOnly Property KeyTypeName As String 
    Get 
      Return _KeyTypeName 
    End Get 
  End Property 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pString As New Text.StringBuilder 
 
    pString.AppendLine("Instance of " & Me.GetType().Name & ". Number of rows" & Me.Count.ToString()) 
 
    For Each pRow As clsComboListMember In Me 
      pString.AppendLine(pRow.ToString() & Environment.NewLine) 
    Next 
 
    Return pString.ToString() 
  End Function 
 
  'ToXML  
  Public Function ToXML(ByVal vRequester As clsRequester) As String 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Dim pXML As String = "" 
    Try 
      Dim pType As Type = Me.GetType 
      pFunctionParameters = pType.Name 
      Dim pSerializer As Xml.Serialization.XmlSerializer 
      pSerializer = New Xml.Serialization.XmlSerializer(pType) 
      Dim MyStringBuilder As New Text.StringBuilder 
      Dim pWriter As New IO.StringWriter(MyStringBuilder) 
      pSerializer.Serialize(pWriter, Me) 
      pWriter.Close() 
      pFault.SetOK() 
 
      pXML = MyStringBuilder.ToString() 
    Catch ex As Exception 
      Throw New Exception(pFault.LogException(ex, pFunctionParameters, "TRGT-130515-1300", vRequester).StringForMessageBox) 
    End Try 
 
    Return pXML 
  End Function 
 
  'ToBinary  
  Public Function ToBinary(ByVal vRequester As clsRequester) As String 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Dim pBinary As String = "" 
    Try 
      Dim pMemoryStream As New System.IO.MemoryStream() 
      Dim pBinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter() 
      pBinaryFormatter.Serialize(pMemoryStream, Me) 
      pBinary = System.Convert.ToBase64String(pMemoryStream.ToArray()) 
      pMemoryStream.Close() 
      pFault.SetOK() 
    Catch ex As Exception 
      Throw New Exception(pFault.LogException(ex, pFunctionParameters, "TRGT-130515-1300", vRequester).StringForMessageBox) 
    End Try 
 
    Return pBinary 
  End Function 
 
  Public Sub New() 
    MyBase.New() 
    CreateEmpty() 
  End Sub 
 
  Public Sub New(ByVal vBytes As Byte(), ByVal vFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    FillFromByteArray(vBytes, vFault, vRequester) 
  End Sub 
 
  Public Sub New(ByVal vBytesFromAPI As Object, ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    Dim pBytes As Byte() = DirectCast(vBytesFromAPI, Byte()) 
    FillFromByteArray(pBytes, rFault, vRequester) 
  End Sub 
 
  Public Sub AddToTop(ByVal vKey As Long, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vText) 
    Me.Insert(0, pComboListMember) 
  End Sub 
  Public Sub AddToTop(ByVal vKey As Integer, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vText) 
    Me.Insert(0, pComboListMember) 
  End Sub 
  Public Sub AddToTop(ByVal vKey As String, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vText) 
    Me.Insert(0, pComboListMember) 
  End Sub 
  Public Sub AddToTop(ByVal vKey As System.Enum, ByVal vEnumType As clsEnums.enmEnum, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vEnumType, vText) 
    Me.Insert(0, pComboListMember) 
  End Sub 
  Public Sub AddToTop(ByVal vKey As Object, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vText) 
    Me.Insert(0, pComboListMember) 
  End Sub 
 
  Public Sub AddToTop(ByVal vComboListMember As clsComboListMember) 
    Me.Insert(0, vComboListMember) 
  End Sub 
 
  Public Sub AddToEnd(ByVal vKey As Long, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vText) 
    Me.Add(pComboListMember) 
  End Sub 
  Public Sub AddToEnd(ByVal vKey As Integer, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vText) 
    Me.Add(pComboListMember) 
  End Sub 
  Public Sub AddToEnd(ByVal vKey As String, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vText) 
    Me.Add(pComboListMember) 
  End Sub 
  Public Sub AddToEnd(ByVal vKey As System.Enum, ByVal vEnumType As clsEnums.enmEnum, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vEnumType, vText) 
    Me.Add(pComboListMember) 
  End Sub 
  Public Sub AddToEnd(ByVal vKey As Object, ByVal vText As String) 
    Dim pComboListMember As New clsComboListMember(vKey, vText) 
    Me.Add(pComboListMember) 
  End Sub 
 
  Public Sub AddToEnd(ByVal vComboListMember As clsComboListMember) 
    Me.Add(vComboListMember) 
  End Sub 
 
  Public Overloads Sub Add(ByVal vCombolistMember As clsComboListMember) 
    SyncLock _CollectionLock 
      If _KeyType = clsEnums.enmComboListKeyType.UD Then 
        _KeyType = vCombolistMember.KeyType 
        _KeyTypeName = vCombolistMember.KeyTypeName 
      ElseIf _KeyType = clsEnums.enmComboListKeyType.Long AndAlso vCombolistMember.KeyType = clsEnums.enmComboListKeyType.Integer Then 
        Dim pOriginalInteger As Integer = vCombolistMember.KeyInteger 
        Dim pOriginalText As String = vCombolistMember.Text 
        vCombolistMember = New clsComboListMember(CType(pOriginalInteger, Long), pOriginalText) 
      ElseIf _KeyType = clsEnums.enmComboListKeyType.Enum AndAlso vCombolistMember.KeyType = clsEnums.enmComboListKeyType.Integer Then 
        Dim pOriginalInteger As Integer = vCombolistMember.KeyInteger 
        Dim pOriginalText As String = vCombolistMember.Text 
 
        Dim pEnum As System.Enum = CType(System.Enum.ToObject(Type.GetType(_KeyTypeName), pOriginalInteger), System.Enum) 
        Dim pEnumType As clsEnums.enmEnum = clsEnums.TranslateEnmEnum(_KeyTypeName.Substring(_KeyTypeName.IndexOf("+") + 4, _KeyTypeName.Length - (_KeyTypeName.IndexOf("+") + 4))) 
        vCombolistMember = New clsComboListMember(pEnum, pEnumType, pOriginalText) 
      ElseIf _KeyType <> vCombolistMember.KeyType Then 
        Dim pMessage As String = "Invalid data type received. Received " & vCombolistMember.KeyType.FastToString() & " when I expected " & _KeyType.FastToString() 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2000", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2000") 
      End If 
      MyBase.Add(vCombolistMember) 
 
      _RecreateDictionaryForFindByKey = True 
      _RecreateDictionaryForFindByText = True 
      _RecreateDictionaryForFindByTag = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vCombolist As clsComboList) 
    SyncLock _CollectionLock 
      If _KeyType = clsEnums.enmComboListKeyType.UD Then 
        _KeyType = vCombolist.KeyType 
        _KeyTypeName = vCombolist.KeyTypeName 
      ElseIf _KeyType <> vCombolist.KeyType Then 
        Dim pMessage As String = $"Invalid key type received. Received {vCombolist.KeyType.FastToString()} when I expected {_KeyType.FastToString()}" 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-210811-1205", Nothing) 
        Throw New Exception($"{pMessage}, TRGT-210811-1205") 
      ElseIf _KeyTypeName <> vCombolist.KeyTypeName Then 
        Dim pMessage As String = $"Invalid data type name received. Received {vCombolist.KeyTypeName} when I expected {_KeyTypeName}" 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-210811-1206", Nothing) 
        Throw New Exception($"{pMessage}, TRGT-210811-1206") 
      End If 
      MyBase.AddRange(vCombolist) 
 
      _RecreateDictionaryForFindByKey = True 
      _RecreateDictionaryForFindByText = True 
      _RecreateDictionaryForFindByTag = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vCombolistMember As clsComboListMember) 
    SyncLock _CollectionLock 
      If _KeyType = clsEnums.enmComboListKeyType.UD Then 
        _KeyType = vCombolistMember.KeyType 
        _KeyTypeName = vCombolistMember.KeyTypeName 
      ElseIf _KeyType = clsEnums.enmComboListKeyType.Long AndAlso vCombolistMember.KeyType = clsEnums.enmComboListKeyType.Integer Then 
        Dim pOriginalInteger As Integer = vCombolistMember.KeyInteger 
        Dim pOriginalText As String = vCombolistMember.Text 
        vCombolistMember = New clsComboListMember(CType(pOriginalInteger, Long), pOriginalText) 
      ElseIf _KeyType = clsEnums.enmComboListKeyType.Enum AndAlso vCombolistMember.KeyType = clsEnums.enmComboListKeyType.Integer Then 
        Dim pOriginalInteger As Integer = vCombolistMember.KeyInteger 
        Dim pOriginalText As String = vCombolistMember.Text 
   
        Dim pEnum As System.Enum = CType(System.Enum.ToObject(Type.GetType(_KeyTypeName), pOriginalInteger), System.Enum) 
        Dim pEnumType As clsEnums.enmEnum = clsEnums.TranslateEnmEnum(_KeyTypeName.Substring(_KeyTypeName.IndexOf("+") + 4, _KeyTypeName.Length - (_KeyTypeName.IndexOf("+") + 4))) 
        vCombolistMember = New clsComboListMember(pEnum, pEnumType, pOriginalText) 
      ElseIf _KeyType <> vCombolistMember.KeyType Then 
        Dim pMessage As String = "Invalid data type received. Received " & vCombolistMember.KeyType.FastToString() & " when I expected " & _KeyType.FastToString() 
        Dim pFault As New clsFault 
        pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2001", Nothing) 
        Throw New Exception(pMessage & ", TRGT-190810-2001") 
      End If 
      MyBase.Insert(vIndex, vCombolistMember) 
   
      _RecreateDictionaryForFindByKey = True 
      _RecreateDictionaryForFindByText = True 
      _RecreateDictionaryForFindByTag = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vCombolistMember As clsComboListMember) 
    SyncLock _CollectionLock 
      MyBase.Remove(vCombolistMember) 
   
      _RecreateDictionaryForFindByKey = True 
      _RecreateDictionaryForFindByText = True 
      _RecreateDictionaryForFindByTag = True 
    End SyncLock 
  End Sub 
 
  ''' <summary>  
  ''' vSearchString search ignores spaces. HowMany assumes text is sorted ascending. Send ParentID as needed. SearchID and SearchCode return 1 entry only, depending if the UniqueKey is numeric or a string 
  ''' </summary>  
  ''' <param name="vQuery"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vParentID"></param> 
  ''' <param name="vSearchString"></param> 
  ''' <param name="vHowMany"></param> 
  ''' <param name="vSearchID"></param> 
  ''' <param name="vSearchCode"></param> 
  ''' <returns></returns>  
  Public Function Fill(ByVal vQuery As clsEnums.enmComboListType, ByVal vRequester As clsRequester, Optional ByVal vParentID As Long = 0, Optional ByVal vSearchString As String = "", Optional ByVal vHowMany As Long = 0, Optional ByVal vSearchID As Long = -1, Optional ByVal vSearchCode As String = "") As clsFault 
    Dim pFault As clsFault 
 
    'Debug.Write("Controller Fill vListType =" & vQuery.FastToString() & Environment.NewLine()) 
    pFault = Fill(vQuery.FastToString(), vRequester, vParentID, vSearchString, vHowMany, vSearchID, vSearchCode) : If Not pFault.isOK Then Return pFault 
    Return pFault 
  End Function 
 
  ''' <summary>  
  ''' vSearchString search ignores spaces. HowMany assumes text is sorted ascending. Send ParentID as needed. SearchID and SearchCode return 1 entry only, depending if the UniqueKey is numeric or a string 
  ''' </summary>  
  ''' <param name="vQuery"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vParentID"></param> 
  ''' <param name="vSearchString"></param> 
  ''' <param name="vHowMany"></param> 
  ''' <param name="vSearchID"></param> 
  ''' <param name="vSearchCode"></param> 
  ''' <returns></returns>  
  Public Function Fill(ByVal vQuery As String, ByVal vRequester As clsRequester, Optional ByVal vParentID As Long = 0, Optional ByVal vSearchString As String = "", Optional ByVal vHowMany As Long = 0, Optional ByVal vSearchID As Long = -1, Optional ByVal vSearchCode As String = "") As clsFault 
    Dim pFunctionParameters As String = String.Format("vQuery={0}", vQuery) 
    Dim pFault As New clsFault 
 
    If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"Query: {vQuery}, ParentID: {vParentID}, SearchString: {vSearchString}, SearchID: {vSearchID}, SearchCode: {vSearchCode}", "ComboListCheck") 
 
    Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write(vQuery) 
          pBinaryWriter.Write(vParentID) 
          pBinaryWriter.Write(vSearchID) 
          pBinaryWriter.Write(vSearchCode) 
          pBinaryWriter.Write(vSearchString) 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsComboListFill" 
      Dim pParametersToLog = $"Query: {vQuery};ParentID: {vParentID};SearchID: {vSearchID};SearchCode: {vSearchCode};SearchString: {vSearchString};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"Fault.Description: {pFault.Description}", "ComboListCheck") 
      If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Combolist 
      FillFromByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150308-1015", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Function FillEnums(ByVal vEnum As clsEnums.enmEnum, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("vEnum={0}", vEnum.FastToString()) 
    Dim pFault As New clsFault 
 
    Dim pEnumArr As Array 
 
    If vEnum = clsEnums.enmEnum.AccountantMethod Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmAccountantMethod)) 
    ElseIf vEnum = clsEnums.enmEnum.ApplicationAuthenticationToWS Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmApplicationAuthenticationToWS)) 
    ElseIf vEnum = clsEnums.enmEnum.AuthenticationMethod Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmAuthenticationMethod)) 
    ElseIf vEnum = clsEnums.enmEnum.Category Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmCategory)) 
    ElseIf vEnum = clsEnums.enmEnum.ccAPICompressionMode Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmccAPICompressionMode)) 
    ElseIf vEnum = clsEnums.enmEnum.ComboListKeyType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmComboListKeyType)) 
    ElseIf vEnum = clsEnums.enmEnum.CustomerType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmCustomerType)) 
    ElseIf vEnum = clsEnums.enmEnum.DebtStatus Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmDebtStatus)) 
    ElseIf vEnum = clsEnums.enmEnum.DeliveryDay Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmDeliveryDay)) 
    ElseIf vEnum = clsEnums.enmEnum.DeliveryMethod Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmDeliveryMethod)) 
    ElseIf vEnum = clsEnums.enmEnum.DeliveryStatus Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmDeliveryStatus)) 
    ElseIf vEnum = clsEnums.enmEnum.EmailStatus Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmEmailStatus)) 
    ElseIf vEnum = clsEnums.enmEnum.FaultSeverity Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmFaultSeverity)) 
    ElseIf vEnum = clsEnums.enmEnum.FaultType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmFaultType)) 
    ElseIf vEnum = clsEnums.enmEnum.FillDirection Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmFillDirection)) 
    ElseIf vEnum = clsEnums.enmEnum.Importance Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmImportance)) 
    ElseIf vEnum = clsEnums.enmEnum.JobAlertType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmJobAlertType)) 
    ElseIf vEnum = clsEnums.enmEnum.JobStatus Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmJobStatus)) 
    ElseIf vEnum = clsEnums.enmEnum.JobType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmJobType)) 
    ElseIf vEnum = clsEnums.enmEnum.Language Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmLanguage)) 
    ElseIf vEnum = clsEnums.enmEnum.LoadParent Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmLoadParent)) 
    ElseIf vEnum = clsEnums.enmEnum.Lookup Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmLookup)) 
    ElseIf vEnum = clsEnums.enmEnum.MessagingMode Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmMessagingMode)) 
    ElseIf vEnum = clsEnums.enmEnum.ObjectStatus Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmObjectStatus)) 
    ElseIf vEnum = clsEnums.enmEnum.ObjectType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmObjectType)) 
    ElseIf vEnum = clsEnums.enmEnum.OrderStatus Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmOrderStatus)) 
    ElseIf vEnum = clsEnums.enmEnum.PaymentMethod Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmPaymentMethod)) 
    ElseIf vEnum = clsEnums.enmEnum.PaymentStatus Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmPaymentStatus)) 
    ElseIf vEnum = clsEnums.enmEnum.Process Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmProcess)) 
    ElseIf vEnum = clsEnums.enmEnum.SystemDefaultType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmSystemDefaultType)) 
    ElseIf vEnum = clsEnums.enmEnum.UserIdentificationModel Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmUserIdentificationModel)) 
    ElseIf vEnum = clsEnums.enmEnum.UserIdentityType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmUserIdentityType)) 
    ElseIf vEnum = clsEnums.enmEnum.WildCardType Then 
      pEnumArr = System.Enum.GetValues(GetType(clsEnums.enmWildCardType)) 
    Else 
      Dim pMessage As String = "Invalid enum received" 
      pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2002", vRequester) 
      Throw New Exception(pMessage & ", TRGT-190810-2002") 
    End If 
 
    If vEnum <> clsEnums.enmEnum.Language Then 
      For Each p In pEnumArr 
        Dim pKey As System.Enum = CType(p, System.Enum) 
        Dim pText As String = "" 
        'Find the Enum in the collection   
        If p.ToString() <> "UD" Then 
          Dim pStrg As String = "" 
          pFault = clsEnums.LoadLocalizedText(vEnum, p.ToString(), pStrg, vRequester) : If pFault.isOK = False Then Return pFault 
          If pStrg = "" Then pStrg = p.ToString() 
          pText = pStrg 
        Else 
          pText = ccHelper.GetUndefined(vRequester) 
        End If 
        Me.Add(New clsComboListMember(pKey, vEnum, pText)) 
      Next 
    Else 
      Dim pLanguages As New csLanguageCol   
      pFault = pLanguages.Fill(vRequester)   
      If pFault.isOK = False Then Return pFault   
   
      For Each p In pEnumArr 
        Dim pKey As System.Enum = CType(p, clsEnums.enmLanguage) 
        Dim pText As String = "" 
        Dim pLanguage As csLanguage = pLanguages.FindByCode(p.ToString()) 
        If pLanguage.Name = "" Then 
          If p.ToString() <> "UD" Then Continue For 
          pText = p.ToString() 
        Else   
          If pLanguage.Name = pLanguage.NameLoc Then 
            pText = pLanguage.Name 
          Else 
            pText = pLanguage.Name & " (" & pLanguage.NameLoc & ")" 
          End If   
        End If 
        Me.Add(New clsComboListMember(pKey, vEnum, pText)) 
      Next 
    End If   
  
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary>  
  ''' Use when no hierarchy  
  ''' </summary>  
  ''' <param name="vLookupType"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Function FillLookup(ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = False) As clsFault 
    Return FillLookup(clsEnums.enmLookup.UD, "", vLookupType, vRequester) 
  End Function 
 
  Private Shared _LookupCol As csLookupCol 
  Private Shared _LastLookupFilledTime As DateTimeOffset = DateTimeOffset.MinValue 
  Private Shared _FillLookupSync As New Object 
 
  Private Function FillLookupCheck(ByVal vReload As Boolean, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    'this will update every 30 minutes  
    Dim pDoIt As Boolean = False 
 
    SyncLock _FillLookupSync 
      If _LastLookupFilledTime = DateTimeOffset.MinValue Then 
        _LastLookupFilledTime = DateTimeOffset.Now 
        _LookupCol = New csLookupCol(vIsLocalized:=True) 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("clsComboList LookupCol.Fill Initial Fill", "Caches") 
        pFault = _LookupCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      ElseIf DateTimeOffset.Now.Subtract(_LastLookupFilledTime).TotalMinutes > 20 Then 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("clsComboList LookupCol.Fill (20m) About to DoIt", "Caches") 
        _LastLookupFilledTime = DateTimeOffset.Now 
        pDoIt = True 
      ElseIf vReload = True Then 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("clsComboList LookupCol.Fill (forced) About to DoIt", "Caches") 
        _LastLookupFilledTime = DateTimeOffset.Now 
        pDoIt = True 
      Else 
        'If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("    clsComboList LookupCol.Fill No update required", "Caches") 
      End If 
    End SyncLock  
  
    If pDoIt = True Then 
      If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("clsComboList LookupCol.Fill Doing It", "Caches") 
      Dim pLookupCol = New csLookupCol(vIsLocalized:=True) 
      pFault = pLookupCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _LookupCol = pLookupCol 
    Else 
      pFault = New clsFault 
      pFault.SetOK() 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>   
  ''' Use when hierarchy , used for filling ComboBoxes  
  ''' </summary>   
  ''' <param name="vLookupType"></param>   
  ''' <param name="vRequester"></param>   
  ''' <returns></returns>   
  Public Function FillLookup(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = FillLookupCheck(vReload, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Dim pLookupCol As csLookupCol = _LookupCol.CloneByParentLookupTypeAndLookupType(vParentLookupType, vLookupType) 
    Dim pIsString As Boolean = False 'Use to assign the ComboList Type. If even one is a string, then it's a string  
    For Each l In pLookupCol 
      Dim pCode As String = l.Code 
      If pIsString = False Then 
        'If Not (String.IsNullOrEmpty(pCode)) Then 
        If Not ccHelper.IsNumeric(pCode) Then 
          pIsString = True 
          Exit For 
        End If 
        'End If 
      End If 
    Next 
 
    'Now get a collection from the lookups    
    Dim pComboListMember As clsComboListMember 
    For Each p In pLookupCol 
      pComboListMember = New clsComboListMember 
      If pIsString = True Then 
        pComboListMember.KeyString = p.Code 
      Else 
        pComboListMember.KeyInteger = ccHelper.ToInteger(p.Code) 
      End If 
      If String.IsNullOrEmpty(p.TextLocalized) Then 
        'if no test, use the code as default 
        pComboListMember.Text = p.Code 
      Else 
        pComboListMember.Text = p.TextLocalized 
      End If 
      Me.Add(pComboListMember) 
    Next 
 
    Return pFault 
  End Function 
 
  ''' <summary>  
  ''' Use with hierarchical lookups  
  ''' </summary>  
  ''' <param name="vParentLookupType"></param>  
  ''' <param name="vParentCode"></param>  
  ''' <param name="vLookupType"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Function FillLookup(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCode As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = FillLookupCheck(vReload, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Dim pLookupCol As csLookupCol = _LookupCol.CloneByParentLookupTypeAndParentCodeAndLookupType(vParentLookupType, vParentCode, vLookupType) 
    Dim pIsString As Boolean = False 'Use to assign the ComboList Type. If even one is a string, then it's a string  
    For Each l In pLookupCol 
      Dim pCode As String = l.Code 
      If pIsString = False Then 
        'If Not (String.IsNullOrEmpty(pCode)) Then 
        If Not ccHelper.IsNumeric(pCode) Then 
          pIsString = True 
          Exit For 
        End If 
        'End If 
      End If 
    Next 
 
    'Now get a collection from the lookups    
    Dim pComboListMember As clsComboListMember 
    For Each p In pLookupCol 
      pComboListMember = New clsComboListMember 
      If pIsString = True Then 
        pComboListMember.KeyString = p.Code 
      Else 
        pComboListMember.KeyInteger = ccHelper.ToInteger(p.Code) 
      End If 
      If String.IsNullOrEmpty(p.TextLocalized) Then 
        'if no test, use the code as default 
        pComboListMember.Text = p.Code 
      Else 
        pComboListMember.Text = p.TextLocalized 
      End If 
      Me.Add(pComboListMember) 
    Next 
 
    Return pFault 
  End Function 
 
  Public Function FillFromXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pComboList As clsComboList = CType(pXmlSerializer.Deserialize(pStreamReader), clsComboList) 
 
      For Each pComboListMember In pComboList 
        Me.Add(pComboListMember) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-130515-1329", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Function FillFromBinary(ByVal vBinary As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pMemoryStream As New System.IO.MemoryStream(Convert.FromBase64String(vBinary)) 
      Dim pBinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter() 
      Dim pComboList As clsComboList = CType(pBinaryFormatter.Deserialize(pMemoryStream), clsComboList) 
      For Each pComboListMember In pComboList 
        Me.Add(pComboListMember) 
      Next 
      pMemoryStream.Close() 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "", "TRGT-140702-0020", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Private Sub LoadKeys() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByKey Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByKey Then Return 
 
      ' 3. Create a TEMPORARY dictionary first.  
      ' Do not touch the shared variable '_SortedDictionaryForFindByKeyXXX' yet! 
      Dim pTempDictionaryLong As Dictionary(Of Long, clsComboListMember) = Nothing 
      Dim pTempDictionaryInteger As Dictionary(Of Integer, clsComboListMember) = Nothing 
      Dim pTempDictionaryString As Dictionary(Of String, clsComboListMember) = Nothing 
      Dim pTempDictionaryEnum As Dictionary(Of [Enum], clsComboListMember) = Nothing 
      Dim pTempDictionaryObject As Dictionary(Of Object, clsComboListMember) = Nothing 
 
      Select Case _KeyType 
        Case clsEnums.enmComboListKeyType.Long 
          pTempDictionaryLong = New Dictionary(Of Long, clsComboListMember) 
        Case clsEnums.enmComboListKeyType.Integer 
          pTempDictionaryInteger = New Dictionary(Of Integer, clsComboListMember) 
        Case clsEnums.enmComboListKeyType.String 
          pTempDictionaryString = New Dictionary(Of String, clsComboListMember)(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.Enum 
          pTempDictionaryEnum = New Dictionary(Of [Enum], clsComboListMember) 
        Case clsEnums.enmComboListKeyType.Object 
          pTempDictionaryObject = New Dictionary(Of Object, clsComboListMember) 
      End Select 
      For Each lCombolistMember In Me 
        Try 
          Select Case lCombolistMember.KeyType 
            Case clsEnums.enmComboListKeyType.Long 
              pTempDictionaryLong.Add(lCombolistMember.KeyLong, lCombolistMember) 
            Case clsEnums.enmComboListKeyType.Integer 
              pTempDictionaryInteger.Add(lCombolistMember.KeyInteger, lCombolistMember) 
            Case clsEnums.enmComboListKeyType.String 
              pTempDictionaryString.Add(lCombolistMember.KeyString, lCombolistMember) 
            Case clsEnums.enmComboListKeyType.Enum 
              pTempDictionaryEnum.Add(lCombolistMember.KeyEnum, lCombolistMember) 
            Case clsEnums.enmComboListKeyType.Object 
              pTempDictionaryObject.Add(lCombolistMember.KeyObject, lCombolistMember) 
          End Select 
        Catch ex As Exception 
          Dim clsFault As New clsFault 
          'clsFault.LogException(ex, lCombolistMember.ToString() & " from " & (New StackFrame(3)).GetMethod().DeclaringType.Name() & ":" & (New StackFrame(3)).GetMethod().Name, "TRGT-190412-1741", Nothing)  
          clsFault.LogException(ex, $"{{lCombolistMember}} from {{ccHelper.GetStack()}}", "TRGT-190412-1741", Nothing) 
        End Try 
      Next 
 
      ' 5. ATOMIC SWAP: Now that the dictionary is full and ready,  
      ' we swap it into the public variable instantly. 
      Select Case _KeyType 
        Case clsEnums.enmComboListKeyType.Long 
          _SortedDictionaryForFindByKeyLong = pTempDictionaryLong 
        Case clsEnums.enmComboListKeyType.Integer 
          _SortedDictionaryForFindByKeyInteger = pTempDictionaryInteger 
        Case clsEnums.enmComboListKeyType.String 
          _SortedDictionaryForFindByKeyString = pTempDictionaryString 
        Case clsEnums.enmComboListKeyType.Enum 
          _SortedDictionaryForFindByKeyEnum = pTempDictionaryEnum 
        Case clsEnums.enmComboListKeyType.Object 
          _SortedDictionaryForFindByKeyObject = pTempDictionaryObject 
      End Select 
 
      _RecreateDictionaryForFindByKey = False 
    End SyncLock 
 
  End Sub 
 
  Private Sub LoadTexts() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByText = True Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByText = True Then Return 
 
      ' 3. Create a TEMPORARY dictionary first.  
      ' Do not touch the shared variable '_SortedDictionaryForFindByKeyXXX' yet! 
      Dim pTempDictionaryLong As Dictionary(Of String, Long) = Nothing 
      Dim pTempDictionaryInteger As Dictionary(Of String, Integer) = Nothing 
      Dim pTempDictionaryString As Dictionary(Of String, String) = Nothing 
      Dim pTempDictionaryEnum As Dictionary(Of String, [Enum]) = Nothing 
      Dim pTempDictionaryObject As Dictionary(Of String, Object) = Nothing 
 
 
      Select Case _KeyType 
        Case clsEnums.enmComboListKeyType.Long 
          pTempDictionaryLong = New Dictionary(Of String, Long)(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.Integer 
          pTempDictionaryInteger = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.String 
          pTempDictionaryString = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.Enum 
          pTempDictionaryEnum = New Dictionary(Of String, [Enum])(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.Object 
          pTempDictionaryObject = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase) 
      End Select 
      For Each lCombolistMember In Me 
        Try 
          Select Case lCombolistMember.KeyType 
            Case clsEnums.enmComboListKeyType.Long 
              If Not pTempDictionaryLong.ContainsKey(lCombolistMember.Text) Then pTempDictionaryLong.Add(lCombolistMember.Text, lCombolistMember.KeyLong) 
            Case clsEnums.enmComboListKeyType.Integer 
              If Not pTempDictionaryInteger.ContainsKey(lCombolistMember.Text) Then pTempDictionaryInteger.Add(lCombolistMember.Text, lCombolistMember.KeyInteger) 
            Case clsEnums.enmComboListKeyType.String 
              If Not pTempDictionaryString.ContainsKey(lCombolistMember.Text) Then pTempDictionaryString.Add(lCombolistMember.Text, lCombolistMember.KeyString) 
            Case clsEnums.enmComboListKeyType.Enum 
              If Not pTempDictionaryEnum.ContainsKey(lCombolistMember.Text) Then pTempDictionaryEnum.Add(lCombolistMember.Text, lCombolistMember.KeyEnum) 
            Case clsEnums.enmComboListKeyType.Object 
              If Not pTempDictionaryObject.ContainsKey(lCombolistMember.Text) Then pTempDictionaryObject.Add(lCombolistMember.Text, lCombolistMember.KeyObject) 
          End Select 
        Catch ex As Exception 
          Dim clsFault As New clsFault 
          clsFault.LogException(ex, lCombolistMember.ToString() & " from " & (New StackFrame(3)).GetMethod().DeclaringType.Name() & ":" & (New StackFrame(3)).GetMethod().Name, "TRGT-190412-1751", Nothing) 
        End Try 
      Next 
 
      ' 5. ATOMIC SWAP: Now that the dictionary is full and ready,  
      ' we swap it into the public variable instantly. 
      Select Case _KeyType 
        Case clsEnums.enmComboListKeyType.Long 
          _SortedDictionaryForFindByTextReturnsLong = pTempDictionaryLong 
        Case clsEnums.enmComboListKeyType.Integer 
          _SortedDictionaryForFindByTextReturnsInteger = pTempDictionaryInteger 
        Case clsEnums.enmComboListKeyType.String 
          _SortedDictionaryForFindByTextReturnsString = pTempDictionaryString 
        Case clsEnums.enmComboListKeyType.Enum 
          _SortedDictionaryForFindByTextReturnsEnum = pTempDictionaryEnum 
        Case clsEnums.enmComboListKeyType.Object 
          _SortedDictionaryForFindByTextReturnsObject = pTempDictionaryObject 
      End Select 
 
      _RecreateDictionaryForFindByText = False 
    End SyncLock 
 
  End Sub 
 
 
  Private Sub LoadTags() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByTag = True Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByTag = True Then Return 
 
      ' 3. Create a TEMPORARY dictionary first.  
      ' Do not touch the shared variable '_SortedDictionaryForFindByKeyXXX' yet! 
      Dim pTempDictionaryLong As Dictionary(Of String, Long) = Nothing 
      Dim pTempDictionaryInteger As Dictionary(Of String, Integer) = Nothing 
      Dim pTempDictionaryString As Dictionary(Of String, String) = Nothing 
      Dim pTempDictionaryEnum As Dictionary(Of String, [Enum]) = Nothing 
      Dim pTempDictionaryObject As Dictionary(Of String, Object) = Nothing 
 
      Select Case _KeyType 
        Case clsEnums.enmComboListKeyType.Long 
          pTempDictionaryLong = New Dictionary(Of String, Long)(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.Integer 
          pTempDictionaryInteger = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.String 
          pTempDictionaryString = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.Enum 
          pTempDictionaryEnum = New Dictionary(Of String, [Enum])(StringComparer.OrdinalIgnoreCase) 
        Case clsEnums.enmComboListKeyType.Object 
          pTempDictionaryObject = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase) 
      End Select 
      For Each lCombolistMember In Me 
        If String.IsNullOrEmpty(lCombolistMember.Tag) Then Continue For 
        Try 
          Select Case lCombolistMember.KeyType 
            Case clsEnums.enmComboListKeyType.Long 
              If Not pTempDictionaryLong.ContainsKey(lCombolistMember.Tag) Then pTempDictionaryLong.Add(lCombolistMember.Tag, lCombolistMember.KeyLong) 
            Case clsEnums.enmComboListKeyType.Integer 
              If Not pTempDictionaryInteger.ContainsKey(lCombolistMember.Tag) Then pTempDictionaryInteger.Add(lCombolistMember.Tag, lCombolistMember.KeyInteger) 
            Case clsEnums.enmComboListKeyType.String 
              If Not pTempDictionaryString.ContainsKey(lCombolistMember.Tag) Then pTempDictionaryString.Add(lCombolistMember.Tag, lCombolistMember.KeyString) 
            Case clsEnums.enmComboListKeyType.Enum 
              If Not pTempDictionaryEnum.ContainsKey(lCombolistMember.Tag) Then pTempDictionaryEnum.Add(lCombolistMember.Tag, lCombolistMember.KeyEnum) 
            Case clsEnums.enmComboListKeyType.Object 
              If Not pTempDictionaryObject.ContainsKey(lCombolistMember.Tag) Then pTempDictionaryObject.Add(lCombolistMember.Tag, lCombolistMember.KeyObject) 
          End Select 
        Catch ex As Exception 
          Dim clsFault As New clsFault 
          clsFault.LogException(ex, lCombolistMember.ToString() & " from " & (New StackFrame(3)).GetMethod().DeclaringType.Name() & ":" & (New StackFrame(3)).GetMethod().Name, "TRGT-190412-1814", Nothing) 
        End Try 
 
        ' 5. ATOMIC SWAP: Now that the dictionary is full and ready,  
        ' we swap it into the public variable instantly. 
        Select Case _KeyType 
          Case clsEnums.enmComboListKeyType.Long 
            _SortedDictionaryForFindByTagReturnsLong = pTempDictionaryLong 
          Case clsEnums.enmComboListKeyType.Integer 
            _SortedDictionaryForFindByTagReturnsInteger = pTempDictionaryInteger 
          Case clsEnums.enmComboListKeyType.String 
            _SortedDictionaryForFindByTagReturnsString = pTempDictionaryString 
          Case clsEnums.enmComboListKeyType.Enum 
            _SortedDictionaryForFindByTagReturnsEnum = pTempDictionaryEnum 
          Case clsEnums.enmComboListKeyType.Object 
            _SortedDictionaryForFindByTagReturnsObject = pTempDictionaryObject 
        End Select 
      Next 
      _RecreateDictionaryForFindByTag = False 
    End SyncLock 
  End Sub 
 
  ''' <summary>   
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database    
  ''' </summary>   
  ''' <returns></returns>   
  ''' <remarks></remarks>   
  Public Function FindByKey(ByVal vKey As Long) As clsComboListMember 
    If _KeyType = clsEnums.enmComboListKeyType.Long Then 
      Return FindByLong(vKey) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.String Then 
      Return FindByString(vKey.ToString()) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Object Then 
      Return FindByObject(vKey) 
    Else 
      Dim pMessage As String = "KeyType doesn't match Key Received: KeyType is '" & _KeyTypeName & "', Received Long" 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2003", Nothing) 
      Throw New Exception(pMessage & ", TRGT-190810-2003") 
    End If 
  End Function 
  ''' <summary>   
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database    
  ''' </summary>   
  ''' <returns></returns>   
  ''' <remarks></remarks>   
  Public Function FindByKey(ByVal vKey As Integer) As clsComboListMember 
    If _KeyType = clsEnums.enmComboListKeyType.Integer Then 
      Return FindByInteger(vKey) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.String Then 
      Return FindByString(vKey.ToString()) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Enum Then 'it defaults here, since it is seen as an integer 
      Dim pEnum As System.Enum = CType(System.Enum.ToObject(Type.GetType(_KeyTypeName), vKey), System.Enum) 
      Return FindByEnum(pEnum) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Object Then 
      Return FindByObject(vKey) 
    Else 
      Dim pMessage As String = "KeyType doesn't match Key Received: KeyType is '" & _KeyTypeName & "', Received Integer" 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2004", Nothing) 
      Throw New Exception(pMessage & ", TRGT-190810-2004") 
    End If 
  End Function 
  ''' <summary>   
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database    
  ''' </summary>   
  ''' <returns></returns>   
  ''' <remarks></remarks>   
  Public Function FindByKey(ByVal vKey As String) As clsComboListMember 
    If _KeyType = clsEnums.enmComboListKeyType.String Then 
      Return FindByString(vKey) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Object Then 
      Return FindByObject(vKey) 
    Else 
      Dim pMessage As String = "KeyType doesn't match Key Received: KeyType is '" & _KeyTypeName & "', Received String" 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2015", Nothing) 
      Throw New Exception(pMessage & ", TRGT-190810-2015") 
    End If 
  End Function 
  ''' <summary>   
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database    
  ''' </summary>   
  ''' <returns></returns>   
  ''' <remarks></remarks>   
  Public Function FindByKey(ByVal vKey As Object) As clsComboListMember 
    If _KeyType = clsEnums.enmComboListKeyType.Object Then 
      Return FindByObject(vKey) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Long Then 
      Return FindByLong(ccHelper.ToLong(vKey)) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Integer Then 
      Return FindByInteger(ccHelper.ToInteger(vKey)) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.Enum Then 
      Return FindByEnum(CType(vKey, [Enum])) 
    ElseIf _KeyType = clsEnums.enmComboListKeyType.String Then 
      Return FindByString(CStr(vKey)) 
    Else 
      Dim pMessage As String = "KeyType doesn't match Key Received: KeyType is '" & _KeyTypeName & "', Received Object" 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(pMessage, "", "TRGT-190810-2016", Nothing) 
      Throw New Exception(pMessage & ", TRGT-190810-2016") 
    End If 
  End Function 
 
  Private Function CreateNewComboListMember() As clsComboListMember 
    Select Case _KeyType 
      Case clsEnums.enmComboListKeyType.Long : Return New clsComboListMember(ccHelper.ToLong(0), "") 
      Case clsEnums.enmComboListKeyType.Integer : Return New clsComboListMember(ccHelper.ToInteger(0), "") 
      Case clsEnums.enmComboListKeyType.String : Return New clsComboListMember("", "") 
      Case clsEnums.enmComboListKeyType.Enum : Return New clsComboListMember(CType(System.Enum.ToObject(Type.GetType(_KeyTypeName), 0), System.Enum), "") 
      Case clsEnums.enmComboListKeyType.Object : Return New clsComboListMember("", "") 
      Case Else : Return New clsComboListMember() 
    End Select 
  End Function 
 
  Private Function FindByLong(ByVal vKey As Long) As clsComboListMember 
    If Me.Count = 0 Then Return CreateNewComboListMember() 
    
    If _RecreateDictionaryForFindByKey = True Then LoadKeys() 
 
    Dim pComboListMember As clsComboListMember = Nothing 
    _SortedDictionaryForFindByKeyLong.TryGetValue(vKey, pComboListMember) 
    If pComboListMember Is Nothing Then pComboListMember = CreateNewComboListMember() 
    
    Return pComboListMember 
  End Function 
  Private Function FindByInteger(ByVal vKey As Integer) As clsComboListMember 
    If Me.Count = 0 Then Return CreateNewComboListMember() 
 
    If _RecreateDictionaryForFindByKey = True Then LoadKeys() 
 
    ' Capture the current reference to a local variable. 
    ' This ensures that even if LoadIDs replaces the dictionary halfway through  
    ' this function, we are still looking at the valid (older) snapshot. 
    Dim pLocalDict As Dictionary(Of Integer, clsComboListMember) = _SortedDictionaryForFindByKeyInteger 
 
 
    Dim pComboListMember As clsComboListMember = Nothing 
    pLocalDict.TryGetValue(vKey, pComboListMember) 
    If pComboListMember Is Nothing Then pComboListMember = CreateNewComboListMember() 
 
    Return pComboListMember 
  End Function 
  Private Function FindByString(ByVal vKey As String) As clsComboListMember 
    If Me.Count = 0 Then Return CreateNewComboListMember() 
 
    If _RecreateDictionaryForFindByKey = True Then LoadKeys() 
 
    ' Capture the current reference to a local variable. 
    ' This ensures that even if LoadIDs replaces the dictionary halfway through  
    ' this function, we are still looking at the valid (older) snapshot. 
    Dim pLocalDict As Dictionary(Of String, clsComboListMember) = _SortedDictionaryForFindByKeyString 
 
    Dim pComboListMember As clsComboListMember = Nothing 
    pLocalDict.TryGetValue(vKey, pComboListMember) 
    If pComboListMember Is Nothing Then pComboListMember = CreateNewComboListMember() 
 
    Return pComboListMember 
  End Function 
  Private Function FindByEnum(ByVal vKey As [Enum]) As clsComboListMember 
    If Me.Count = 0 Then Return CreateNewComboListMember() 
 
    If _RecreateDictionaryForFindByKey = True Then LoadKeys() 
 
    ' Capture the current reference to a local variable. 
    ' This ensures that even if LoadIDs replaces the dictionary halfway through  
    ' this function, we are still looking at the valid (older) snapshot. 
    Dim pLocalDict As Dictionary(Of [Enum], clsComboListMember) = _SortedDictionaryForFindByKeyEnum 
 
    Dim pComboListMember As clsComboListMember = Nothing 
    pLocalDict.TryGetValue(vKey, pComboListMember) 
    If pComboListMember Is Nothing Then pComboListMember = CreateNewComboListMember() 
 
    Return pComboListMember 
  End Function 
  Private Function FindByObject(ByVal vKey As Object) As clsComboListMember 
    If Me.Count = 0 Then Return CreateNewComboListMember() 
 
    If _RecreateDictionaryForFindByKey = True Then LoadKeys() 
 
 
    ' Capture the current reference to a local variable. 
    ' This ensures that even if LoadIDs replaces the dictionary halfway through  
    ' this function, we are still looking at the valid (older) snapshot. 
    Dim pLocalDict As Dictionary(Of Object, clsComboListMember) = _SortedDictionaryForFindByKeyObject 
 
    Dim pComboListMember As clsComboListMember = Nothing 
    pLocalDict.TryGetValue(vKey, pComboListMember) 
    If pComboListMember Is Nothing Then pComboListMember = CreateNewComboListMember() 
 
    Return pComboListMember 
  End Function 
 
  ''' <summary>   
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database    
  ''' </summary>   
  ''' <returns></returns>   
  ''' <remarks></remarks>   
  Public Function FindByText(ByVal vText As String) As clsComboListMember 
    If Me.Count = 0 Then Return CreateNewComboListMember() 
 
    If _RecreateDictionaryForFindByText = True Then LoadTexts() 
 
    Dim pValueToSearchFor As String = vText 
    Select Case _KeyType 
      Case clsEnums.enmComboListKeyType.Long 
        Dim pLocalDict As Dictionary(Of String, Long) = _SortedDictionaryForFindByTextReturnsLong 
        Dim pID As Long = 0 
        Dim pFound As Boolean = pLocalDict.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByLong(pID) 
      Case clsEnums.enmComboListKeyType.Integer 
        Dim pLocalDict As Dictionary(Of String, Integer) = _SortedDictionaryForFindByTextReturnsInteger 
        Dim pID As Integer = 0 
        Dim pFound As Boolean = pLocalDict.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByInteger(pID) 
      Case clsEnums.enmComboListKeyType.String 
        Dim pLocalDict As Dictionary(Of String, String) = _SortedDictionaryForFindByTextReturnsString 
        Dim pID As String = "" 
        Dim pFound As Boolean = pLocalDict.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByString(pID) 
      Case clsEnums.enmComboListKeyType.Enum 
        Dim pLocalDict As Dictionary(Of String, [Enum]) = _SortedDictionaryForFindByTextReturnsEnum 
        Dim pID As [Enum] = clsEnums.enmEnum.UD 
        Dim pFound As Boolean = pLocalDict.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByEnum(pID) 
      Case clsEnums.enmComboListKeyType.Object 
        Dim pLocalDict As Dictionary(Of String, Object) = _SortedDictionaryForFindByTextReturnsObject 
        Dim pID As Object = 0 
        Dim pFound As Boolean = pLocalDict.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByObject(pID) 
    End Select 
 
    Return CreateNewComboListMember() 
  End Function 
  
  ''' <summary>   
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database    
  ''' </summary>   
  ''' <returns></returns>   
  ''' <remarks></remarks>   
  Public Function FindByTag(ByVal vTag As String) As clsComboListMember 
    If Me.Count = 0 Then Return CreateNewComboListMember() 
 
    If _RecreateDictionaryForFindByTag = True Then LoadTags() 
 
    Dim pValueToSearchFor As String = vTag 
    Select Case _KeyType 
      Case clsEnums.enmComboListKeyType.Long 
        Dim pID As Long = 0 
        Dim pFound As Boolean = _SortedDictionaryForFindByTagReturnsLong.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByLong(pID) 
      Case clsEnums.enmComboListKeyType.Integer 
        Dim pID As Integer = 0 
        Dim pFound As Boolean = _SortedDictionaryForFindByTagReturnsInteger.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByInteger(pID) 
      Case clsEnums.enmComboListKeyType.String 
        Dim pID As String = "" 
        Dim pFound As Boolean = _SortedDictionaryForFindByTagReturnsString.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByString(pID) 
      Case clsEnums.enmComboListKeyType.Enum 
        Dim pID As [Enum] = clsEnums.enmEnum.UD 
        Dim pFound As Boolean = _SortedDictionaryForFindByTagReturnsEnum.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByEnum(pID) 
      Case clsEnums.enmComboListKeyType.Object 
        Dim pID As Object = 0 
        Dim pFound As Boolean = _SortedDictionaryForFindByTagReturnsObject.TryGetValue(pValueToSearchFor, pID) 
        If pFound = True Then Return FindByObject(pID) 
    End Select 
 
    'if we got here, we didn't find one   
    Return CreateNewComboListMember()  
  End Function  
  
  Public Function CreateByteArray(ByVal vFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    vFault.ClearOK() 
    Dim pBytes As Byte() = Nothing 
    Try 
      If vFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(Me.Count) 
          pBinaryWriter.Write(_KeyType.FastToString()) 
          pBinaryWriter.Write(_KeyTypeName) 
          For Each lComboListMember As clsComboListMember In Me 
            Dim pByte As Byte() = lComboListMember.CreateByteArray(vFault, vRequester) : If Not vFault.isOK Then Return Nothing 
            pBinaryWriter.Write(pByte.Length) 
            pBinaryWriter.Write(pByte, 0, pByte.Length) 
          Next 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      vFault.SetOK() 
    Catch ex As Exception 
      vFault.LogException(ex, pFunctionParameters, "TRGT-150424-1235", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Private Sub FillFromByteArray(ByVal vBytes As Byte(), ByVal vFault As clsFault, ByVal vRequester As clsRequester) 
 
    Me.Clear() 
 
    vFault.ClearOK() 
 
    Try 
      If vFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pCount As Integer = pReader.ReadInt32 
          Dim psKeyType As String = pReader.ReadString() 
          Dim pKeyType As clsEnums.enmComboListKeyType = clsEnums.TranslateEnmComboListKeyType(psKeyType) 
          Dim pKeyTypeName As String = pReader.ReadString() 
          For iCntr As Integer = 0 To pCount - 1 
            Dim pLength As Integer = pReader.ReadInt32 
            Me.Add(New clsComboListMember(pKeyType, pKeyTypeName, pReader.ReadBytes(pLength), vFault, vRequester)) : If Not vFault.isOK Then Exit Sub 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      vFault.SetOK() 
    Catch ex As Exception 
      vFault.LogException(ex, "", "TRGT-150424-1236", vRequester) 
    End Try 
 
  End Sub 
 
  ''' <summary>  
  ''' Returns JSON for public properties  
  ''' </summary>  
  ''' <param name="rJSON"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    rJSON = "" 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.   
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic  
      pSettings.ContractResolver = pDefaultContractResolver 
 
      rJSON = Newtonsoft.Json.JsonConvert.SerializeObject(Me, pSettings) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  ''' <summary>  
  ''' Creates object using JSON received, for public properties  
  ''' </summary>  
  ''' <param name="vJSON"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.   
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic  
      pSettings.ContractResolver = pDefaultContractResolver 
 
      Dim pComboList As clsComboList = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsComboList)(vJSON, pSettings) 
      For Each l In pComboList 
        Me.Add(l) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function Clone() As clsComboList 
    Dim pComboListClone As New clsComboList 
    For Each pComboListMember As clsComboListMember In Me 
      Dim pComboListMemberClone As clsComboListMember = pComboListMember.Clone() 
      pComboListClone.Add(pComboListMemberClone) 
    Next 
    pComboListClone._KeyType = _KeyType 
    pComboListClone._KeyTypeName = _KeyTypeName 
    Return pComboListClone 
  End Function 
 
  Public Sub SortByKey() 
    Select Case _KeyType 
      Case clsEnums.enmComboListKeyType.Long : SortByKeyLong() 
      Case clsEnums.enmComboListKeyType.Integer : SortByKeyInteger() 
      Case clsEnums.enmComboListKeyType.String : SortByKeyString() 
      Case clsEnums.enmComboListKeyType.Enum : SortByKeyEnum() 
      Case clsEnums.enmComboListKeyType.Object : SortByKeyObject() 
    End Select 
  End Sub 
 
  Private Sub SortByKeyLong() 
    Me.Sort(New clsComboList.CompareByKeyLong) 
  End Sub 
  Private Class CompareByKeyLong 
    Implements IComparer(Of clsComboListMember) 
    Private Function Compare(ByVal x As clsComboListMember, ByVal y As clsComboListMember) As Integer Implements System.Collections.Generic.IComparer(Of clsComboListMember).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
 
      Dim pX As Long = x.KeyLong 
      Dim pY As Long = y.KeyLong 
      If pX < pY Then 
        Return -1 
      ElseIf pX = pY Then 
        Return 0 
      Else 
        Return 1 
      End If 
 
    End Function 
  End Class 
 
  Private Sub SortByKeyInteger() 
    Me.Sort(New clsComboList.CompareByKeyInteger) 
  End Sub 
  Private Class CompareByKeyInteger 
    Implements IComparer(Of clsComboListMember) 
    Private Function Compare(ByVal x As clsComboListMember, ByVal y As clsComboListMember) As Integer Implements System.Collections.Generic.IComparer(Of clsComboListMember).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
 
      Dim pX As Integer = x.KeyInteger 
      Dim pY As Integer = y.KeyInteger 
      If pX < pY Then 
        Return -1 
      ElseIf pX = pY Then 
        Return 0 
      Else 
        Return 1 
      End If 
 
    End Function 
  End Class 
 
  Private Sub SortByKeyString() 
    Me.Sort(New clsComboList.CompareByKeyString) 
  End Sub 
  Private Class CompareByKeyString 
    Implements IComparer(Of clsComboListMember) 
    Private Function Compare(ByVal x As clsComboListMember, ByVal y As clsComboListMember) As Integer Implements System.Collections.Generic.IComparer(Of clsComboListMember).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
 
      Return String.Compare(x.KeyString, y.KeyString, StringComparison.OrdinalIgnoreCase) 
 
    End Function 
  End Class 
 
  Private Sub SortByKeyEnum() 
    Me.Sort(New clsComboList.CompareByKeyEnum) 
  End Sub 
  Private Class CompareByKeyEnum 
    Implements IComparer(Of clsComboListMember) 
    Private Function Compare(ByVal x As clsComboListMember, ByVal y As clsComboListMember) As Integer Implements System.Collections.Generic.IComparer(Of clsComboListMember).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
 
      Return String.Compare(x.KeyEnum.ToString(), y.KeyEnum.ToString(), StringComparison.OrdinalIgnoreCase) 
    End Function 
  End Class 
 
  Private Sub SortByKeyObject() 
    Me.Sort(New clsComboList.CompareByKeyObject) 
  End Sub 
  Private Class CompareByKeyObject 
    Implements IComparer(Of clsComboListMember) 
    Private Function Compare(ByVal x As clsComboListMember, ByVal y As clsComboListMember) As Integer Implements System.Collections.Generic.IComparer(Of clsComboListMember).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
      If TypeOf x.KeyObject Is String OrElse x.KeyObject.GetType.IsEnum = True Then 
        Return String.Compare(x.KeyObject.ToString(), y.KeyObject.ToString(), StringComparison.OrdinalIgnoreCase) 
      ElseIf TypeOf x.KeyObject Is Integer OrElse TypeOf x.KeyObject Is Long Then 
        Dim pX As Long = CType(x.KeyObject, Long) 
        Dim pY As Long = CType(y.KeyObject, Long) 
        If pX < pY Then 
          Return -1 
        ElseIf pX = pY Then 
          Return 0 
        Else 
          Return 1 
        End If 
      Else 
        Throw New Exception("KeyObject is an invalid type") 
      End If 
    End Function 
  End Class 
 
  Public Sub SortByText() 
    Me.Sort(New clsComboList.CompareByText) 
  End Sub 
  Private Class CompareByText 
    Implements IComparer(Of clsComboListMember) 
    Private Function Compare(ByVal x As clsComboListMember, ByVal y As clsComboListMember) As Integer Implements System.Collections.Generic.IComparer(Of clsComboListMember).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
      Return String.Compare(x.Text, y.Text, StringComparison.OrdinalIgnoreCase) 
    End Function 
  End Class 
 
  Public Sub SortByTag() 
    Me.Sort(New clsComboList.CompareByTag) 
  End Sub 
  Private Class CompareByTag 
    Implements IComparer(Of clsComboListMember) 
    Private Function Compare(ByVal x As clsComboListMember, ByVal y As clsComboListMember) As Integer Implements System.Collections.Generic.IComparer(Of clsComboListMember).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
      Return String.Compare(x.Tag, y.Tag, StringComparison.OrdinalIgnoreCase) 
    End Function 
  End Class 
 
 
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _SortedDictionaryForFindByKeyLong = New Dictionary(Of Long, clsComboListMember) 
    _SortedDictionaryForFindByKeyInteger = New Dictionary(Of Integer, clsComboListMember) 
    _SortedDictionaryForFindByKeyString = New Dictionary(Of String, clsComboListMember)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByKeyEnum = New Dictionary(Of [Enum], clsComboListMember) 
    _SortedDictionaryForFindByKeyObject = New Dictionary(Of Object, clsComboListMember) 
 
    _SortedDictionaryForFindByTextReturnsLong = New Dictionary(Of String, Long)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByTextReturnsInteger = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByTextReturnsString = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByTextReturnsEnum = New Dictionary(Of String, [Enum])(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByTextReturnsObject = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase) 
 
    _SortedDictionaryForFindByTagReturnsLong = New Dictionary(Of String, Long)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByTagReturnsInteger = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByTagReturnsString = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByTagReturnsEnum = New Dictionary(Of String, [Enum])(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByTagReturnsObject = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase) 
 
    _RecreateDictionaryForFindByKey = False 
    _RecreateDictionaryForFindByText = False 
    _RecreateDictionaryForFindByTag = False 
  End Sub 
 
  Private Sub CreateEmpty() 
 
    _KeyType = clsEnums.enmComboListKeyType.UD 
    _KeyTypeName = "" 
 
    Clear() 
 
  End Sub 
 
End Class 
