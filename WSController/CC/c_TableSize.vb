Public Class csTableSize
  Inherits cTargCCEntity 
 
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property CanHave0AsPrimaryKey As Boolean 
    Get 
      Return True 
    End Get 
  End Property 
 
  ''' <summary> 
  ''' To be used by the partial class to Override CreateEmpty 
  ''' </summary> 
  Private Event evtOverrideCreateEmpty() 
 
  ''' <summary> 
  ''' Raised after getting the row from the data store. This also occurs after an update 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterGet()
  Friend Event evtAfterGetWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [TableName] 
    [NumberOfRows] 
    [ReservedSizeKb] 
    [DataSizeKb] 
    [IndexSizeKb] 
    [UnusedSizeKb] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [NumberOfRows] 
    [ReservedSizeKb] 
    [DataSizeKb] 
    [IndexSizeKb] 
    [UnusedSizeKb] 
  End Enum 
  Private Event evtOverrideDefaultDesignation(ByRef rOverridenValue As String) 
  
  Private _ID As Long
  Private _TableName As String
  Private _NumberOfRows As Integer
  Private _ReservedSizeKb As Integer
  Private _DataSizeKb As Integer
  Private _IndexSizeKb As Integer
  Private _UnusedSizeKb As Integer
  Private _Tag As String
  
  Public Property [ID]() As Long
    Get
      Return Me._ID
    End Get
    Set(ByVal value As Long)
      If Me._ID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ID = value 
        bPrimaryKey = _ID 
      End If 
    End Set
  End Property
  Public Property [TableName]() As String
    Get
      Return Me._TableName
    End Get
    Set(ByVal value As String)
      If Me._TableName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TableName = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [NumberOfRows]() As Integer
    Get
      Return Me._NumberOfRows
    End Get
    Set(ByVal value As Integer)
      If Me._NumberOfRows <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._NumberOfRows = value 
      End If 
    End Set
  End Property
  Public Property [ReservedSizeKb]() As Integer
    Get
      Return Me._ReservedSizeKb
    End Get
    Set(ByVal value As Integer)
      If Me._ReservedSizeKb <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ReservedSizeKb = value 
      End If 
    End Set
  End Property
  Public Property [DataSizeKb]() As Integer
    Get
      Return Me._DataSizeKb
    End Get
    Set(ByVal value As Integer)
      If Me._DataSizeKb <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DataSizeKb = value 
      End If 
    End Set
  End Property
  Public Property [IndexSizeKb]() As Integer
    Get
      Return Me._IndexSizeKb
    End Get
    Set(ByVal value As Integer)
      If Me._IndexSizeKb <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IndexSizeKb = value 
      End If 
    End Set
  End Property
  Public Property [UnusedSizeKb]() As Integer
    Get
      Return Me._UnusedSizeKb
    End Get
    Set(ByVal value As Integer)
      If Me._UnusedSizeKb <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UnusedSizeKb = value 
      End If 
    End Set
  End Property
  ''' <summary> 
  ''' Extra property that is not stored in the database. Setting it does not trip the status to 'Dirty' 
  ''' </summary> 
  ''' <returns></returns> 
  <Newtonsoft.Json.JsonIgnore, Xml.Serialization.XmlIgnore> 
  Public Property [Tag]() As String
    Get
      Return Me._Tag
    End Get
    Set(ByVal value As String)
      If Me._Tag <> value Then 
        Me._Tag = value 
      End If 
    End Set
  End Property
  
  Private Sub CreateDefaultDesignation() 
    Dim pOverridenValue As String = Nothing 
    RaiseEvent evtOverrideDefaultDesignation(pOverridenValue) 
    If pOverridenValue = Nothing Then bDefaultDesignation = _TableName Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _TableName <> "" Then pValue.Append("TableName='" & _TableName & "' ‡ ") 
    If _NumberOfRows <> 0 Then pValue.Append("NumberOfRows='" & _NumberOfRows.ToString() & "' ‡ ") 
    If _ReservedSizeKb <> 0 Then pValue.Append("ReservedSizeKb='" & _ReservedSizeKb.ToString() & "' ‡ ") 
    If _DataSizeKb <> 0 Then pValue.Append("DataSizeKb='" & _DataSizeKb.ToString() & "' ‡ ") 
    If _IndexSizeKb <> 0 Then pValue.Append("IndexSizeKb='" & _IndexSizeKb.ToString() & "' ‡ ") 
    If _UnusedSizeKb <> 0 Then pValue.Append("UnusedSizeKb='" & _UnusedSizeKb.ToString() & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TableName)}""") 
    pCSV.Append("," & _NumberOfRows.ToString() & "") 
    pCSV.Append("," & _ReservedSizeKb.ToString() & "") 
    pCSV.Append("," & _DataSizeKb.ToString() & "") 
    pCSV.Append("," & _IndexSizeKb.ToString() & "") 
    pCSV.Append("," & _UnusedSizeKb.ToString() & "") 
    If Not vWithTexts Then 
        pCSV.Append($",""{ccHelper.StringForCSV(_Tag)}""") 
    End If 
    'pCSV.Append($",""{bDateAdded:yyyyMMddTHH:mm:ss.ffff}"" ") 
    
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty()
  End Sub
  
  Public Sub New(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional vMustExist As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    
    rFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
  End Sub
  
  Public Sub New(ByVal vcsTableSize As csTableSize)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsTableSize) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vTableName As String = "" _ 
    , Optional vNumberOfRows As Integer = 0 _ 
    , Optional vReservedSizeKb As Integer = 0 _ 
    , Optional vDataSizeKb As Integer = 0 _ 
    , Optional vIndexSizeKb As Integer = 0 _ 
    , Optional vUnusedSizeKb As Integer = 0 _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _TableName = vTableName 
    _NumberOfRows = vNumberOfRows 
    _ReservedSizeKb = vReservedSizeKb 
    _DataSizeKb = vDataSizeKb 
    _IndexSizeKb = vIndexSizeKb 
    _UnusedSizeKb = vUnusedSizeKb 
    _Tag = vTag 
    bDateAdded = vDateAdded 
    bccStatus = clsEnums.enmObjectStatus.Dirty 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  Friend Sub New(ByVal vRow As DataRow, ByVal vRequester As clsRequester) 
    MyBase.New()
    CreateEmpty()
    Dim pFault As New clsFault 
 
    pFault = LoadDataRow(vRow, vRequester) 
    If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox) 
 
 
  End Sub 
 
  Public Sub New(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New()
    CreateEmpty()
    LoadByteArray(vBytes, rFault, vRequester) 
  End Sub 
 
  Public Sub New(ByVal vBytesFromAPI As Object, ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    Dim pBytes As Byte() = DirectCast(vBytesFromAPI, Byte()) 
    LoadByteArray(pBytes, rFault, vRequester) 
  End Sub 
 
  Public Overrides Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) 
    Throw New Exception("Entity has no parents") 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    Throw New Exception("Entity is not localizable") 
  End Sub 
 
  Private _IsTruncated As Boolean = False 
  
  ''' <summary> 
  ''' Use this before loading a DataGridView. You don't need more than X c to see what you want. 
  ''' </summary> 
  ''' <param name="pTruncateLength"></param> 
  Friend Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
    'Truncates strings, and reduces pictures to W 100 x H 50 
 
    _IsTruncated = False 
 
    _TableName = _TableName.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the TableSize by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-TableSize-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [TableName] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the TableSize by the chosen parameters. This function may be a bit slower than accessing the TableSize's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.TableName 
          pFault = GetByTableName(CStr(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-TableSize-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-TableSize-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the TableSize by TableName. 
  ''' </summary>
  ''' <param name="vTableName"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByTableName(ByVal vTableName As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}", vTableName)
    Dim pFault As New clsFault 
    
    CreateEmpty() 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vTableName 
          If vTableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vTableName) 
          ' 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csTableSizeGetByTableName" 
      Dim pParametersToLog = $"TableName: {vTableName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the TableSize 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the TableSize by ID. 
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault 
    
    CreateEmpty() 
    
    If vID = -1 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vID 
          pBinaryWriter.Write(vID) 
          ' 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csTableSizeGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the TableSize 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  'Interface Edits
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csTableSize) Then Return False 
    Dim pTableSizeToTest As csTableSize = CType(vTargCCEntityToTest, csTableSize) 
    Return isEqual(pTableSizeToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vTableSizeToTest As csTableSize) As Boolean
    With vTableSizeToTest
      If _ID <> .ID Then Return False
      If _TableName <> .TableName Then Return False
      If _NumberOfRows <> .NumberOfRows Then Return False
      If _ReservedSizeKb <> .ReservedSizeKb Then Return False
      If _DataSizeKb <> .DataSizeKb Then Return False
      If _IndexSizeKb <> .IndexSizeKb Then Return False
      If _UnusedSizeKb <> .UnusedSizeKb Then Return False
      If _Tag <> .Tag Then Return False
      If bDateAdded <> .DateAdded Then Return False 
      If bccStatus <> .ccStatus Then Return False 
    End With
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are equal, IGNORING the dependants 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCEntity() As ITargCCEntity 
    Dim pClone As New csTableSize(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csTableSize
    Dim pClone As New csTableSize(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-TableSize-130316-0852", vRequester) : End Try 
    Try : vDataRow("TableName") = _TableName : Catch ex As Exception : Return pFault.LogException(ex, "TableName", "TRGT-TableSize-130316-0852", vRequester) : End Try 
    Try : vDataRow("NumberOfRows") = _NumberOfRows : Catch ex As Exception : Return pFault.LogException(ex, "NumberOfRows", "TRGT-TableSize-130316-0852", vRequester) : End Try 
    Try : vDataRow("ReservedSizeKb") = _ReservedSizeKb : Catch ex As Exception : Return pFault.LogException(ex, "ReservedSizeKb", "TRGT-TableSize-130316-0852", vRequester) : End Try 
    Try : vDataRow("DataSizeKb") = _DataSizeKb : Catch ex As Exception : Return pFault.LogException(ex, "DataSizeKb", "TRGT-TableSize-130316-0852", vRequester) : End Try 
    Try : vDataRow("IndexSizeKb") = _IndexSizeKb : Catch ex As Exception : Return pFault.LogException(ex, "IndexSizeKb", "TRGT-TableSize-130316-0852", vRequester) : End Try 
    Try : vDataRow("UnusedSizeKb") = _UnusedSizeKb : Catch ex As Exception : Return pFault.LogException(ex, "UnusedSizeKb", "TRGT-TableSize-130316-0852", vRequester) : End Try 
    Try : vDataRow("Tag") = _Tag : Catch ex As Exception : End Try 
    Try : vDataRow("DateAdded") = bDateAdded : Catch ex As Exception : Return pFault.LogException(ex, "DateAdded", "TRGT-TransactionLoad-130316-0852", vRequester) : End Try 
    bPrimaryKey = _ID
    CreateDefaultDesignation() 
 
    Return pFault.SetOK() 
  End Function 
 
  Public Overrides Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    rXML = "" 
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
 
      rXML = MyStringBuilder.ToString() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-TableSize-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pTableSize As csTableSize = CType(pXmlSerializer.Deserialize(pStreamReader), csTableSize) 
      AssignValues(pTableSize) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-TableSize-130515-1230", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  Public Overrides Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
    
    Dim pBytes As Byte() = Nothing 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pLength As Integer = 0 
          Dim pHasValue As Boolean = False 
          Dim pObjectBytes As Byte() = Nothing 
          pBinaryWriter.Write(bccStatus.FastToString()) 
          'ID 
          pBinaryWriter.Write(_ID) 
          'TableName 
          If _TableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_TableName) 
          'NumberOfRows 
          pBinaryWriter.Write(_NumberOfRows) 
          'ReservedSizeKb 
          pBinaryWriter.Write(_ReservedSizeKb) 
          'DataSizeKb 
          pBinaryWriter.Write(_DataSizeKb) 
          'IndexSizeKb 
          pBinaryWriter.Write(_IndexSizeKb) 
          'UnusedSizeKb 
          pBinaryWriter.Write(_UnusedSizeKb) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, pFunctionParameters, "TRGT-TableSize-150307-2338", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Public Overrides Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Try 
      If rFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pLength As Integer = 0 
          Dim pHasValue As Boolean = False 
          Dim pObjectBytes As Byte() = Nothing 
          bccStatus = clsEnums.TranslateEnmObjectStatus(pReader.ReadString) 
          'ID 
          _ID = pReader.ReadInt64 
          'TableName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _TableName = pReader.ReadString 
          'NumberOfRows 
          _NumberOfRows = pReader.ReadInt32 
          'ReservedSizeKb 
          _ReservedSizeKb = pReader.ReadInt32 
          'DataSizeKb 
          _DataSizeKb = pReader.ReadInt32 
          'IndexSizeKb 
          _IndexSizeKb = pReader.ReadInt32 
          'UnusedSizeKb 
          _UnusedSizeKb = pReader.ReadInt32 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-TableSize-150307-2339", vRequester) 
    End Try 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  ''' <summary> 
  ''' Returns JSON for public properties 
  ''' </summary> 
  ''' <param name="rJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-TableSize-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  ''' <summary> 
  ''' Creates object using JSON received, for public properties 
  ''' </summary> 
  ''' <param name="vJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      Dim pTableSize As csTableSize = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csTableSize)(vJSON, pSettings) 
      AssignValues(pTableSize) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-TableSize-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vTableSize As csTableSize)
    With vTableSize
      _ID = .ID 
      _TableName = .TableName 
      _NumberOfRows = .NumberOfRows 
      _ReservedSizeKb = .ReservedSizeKb 
      _DataSizeKb = .DataSizeKb 
      _IndexSizeKb = .IndexSizeKb 
      _UnusedSizeKb = .UnusedSizeKb 
      _Tag = .Tag 
      bDateAdded = .DateAdded 
      bccStatus = .ccStatus
    End With
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub
  
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If _ID = -1 Then 
      Return pFault.SetOK() 
    End If 
 
    'There are no enums or lookups. This function was added to this object for interface compatibility 
    Return pFault.SetOK() 
  End Function 
 
  Private Sub CreateEmpty()
    
    _ID = -1 
    _TableName = ""
    _NumberOfRows = 0
    _ReservedSizeKb = 0
    _DataSizeKb = 0
    _IndexSizeKb = 0
    _UnusedSizeKb = 0
    _Tag = ""
    bccStatus = clsEnums.enmObjectStatus.New 
    bPrimaryKey = _ID 
    bDateAdded = Nothing 
    bDefaultDesignation = "" 
     
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = True 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class csTableSizeCol
  Inherits cTargCCCollection(Of csTableSize)
  
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property CanHave0AsPrimaryKey As Boolean 
    Get 
      Return True 
    End Get 
  End Property 
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Long) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByTableName As Dictionary(Of String, csTableSize) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByTableName As Boolean 
  Private Function CreateKeyForFindByTableName(ByVal vTableSize As csTableSize) As String 
    With vTableSize 
      Return .TableName
    End With 
  End Function 
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csTableSize) 
  Private _RecreateDictionaryForFindByID As Boolean 
   
  Private _Tag As String = "" 
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
    Dim pString As New Text.StringBuilder 
 
    pString.AppendLine("Instance of " & Me.GetType().Name & ". Number of rows" & Me.Count.ToString()) 
    If _Tag <> "" Then pString.AppendLine("  Tag='" & _Tag & "'") 
 
    For Each pRow As csTableSize In Me 
      pString.AppendLine(pRow.ToString & Environment.NewLine) 
    Next 
 
    Return pString.ToString() 
  End Function 
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New Text.StringBuilder 
    Dim pCSVTitle As New Text.StringBuilder 
    'Get title 
    Dim pDbCode As String = "" 
    If vWithTexts Then pDbCode = " (Db Code)" 
    pCSVTitle.Append("""ID""") 
    pCSVTitle.Append(",""TableName""") 
    pCSVTitle.Append(",""NumberOfRows""") 
    pCSVTitle.Append(",""ReservedSizeKb""") 
    pCSVTitle.Append(",""DataSizeKb""") 
    pCSVTitle.Append(",""IndexSizeKb""") 
    pCSVTitle.Append(",""UnusedSizeKb""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csTableSize In Me 
      pCSV.AppendLine(pRow.ToCSV(vWithTexts)) 
    Next 
 
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty() 
  End Sub
  
  Public Sub New(ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) 
    MyBase.New()
    CreateEmpty() 
    
    rFault = Fill(vRequester, vHowMany, vDir) 
  End Sub
  
  Public Sub New(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    LoadByteArray(vBytes, rFault, vRequester) 
  End Sub 
 
  Public Sub New(ByVal vBytesFromAPI As Object, ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    Dim pBytes As Byte() = DirectCast(vBytesFromAPI, Byte()) 
    LoadByteArray(pBytes, rFault, vRequester) 
  End Sub 
 
  Public Overloads Sub Add(ByVal vTableSize As csTableSize) 
    SyncLock _CollectionLock 
      MyBase.Add(vTableSize) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vTableSize As csTableSize) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vTableSize) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vTableSizeCol As csTableSizeCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vTableSizeCol) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vTableSize As csTableSize) 
    SyncLock _CollectionLock 
      MyBase.Remove(vTableSize) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
 
  Private Sub LoadTableNames() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByTableName Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByTableName Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByTableName = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByTableName' yet!
      Dim pTempDictionary As New Dictionary(Of String, csTableSize)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lTableSize In Me 
        Try 
          Dim pTableName As String = CreateKeyForFindByTableName(lTableSize) 
          If String.IsNullOrEmpty(pTableName.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pTableName)) Then 
            pTempDictionary.Add(pTableName, lTableSize) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lTableSize.ToString, "TRGT-TableSize-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByTableName:" & ex.Message & ", TableSize:" & lTableSize.ToString() & ", TRGT-TableSize-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByTableName = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByTableName = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadIDs() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByID Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByID Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByID = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByID' yet!
      Dim pTempDictionary As New Dictionary(Of Long, csTableSize) 
      
      For Each lTableSize In Me 
        If lTableSize.IsEmpty OrElse pTempDictionary.ContainsKey(lTableSize.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lTableSize.ID, lTableSize) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lTableSize.ToString, "TRGT-TableSize-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", TableSize:" & lTableSize.ToString() & ", TRGT-TableSize-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Public Overrides Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) 
    Throw New Exception("Entity has no parents") 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    Throw New Exception("Entity is not localizable") 
  End Sub 
 
  ''' <summary>  
  ''' Use this before loading a DataGridView. You don't need more than pTruncateLength characters to see what you want.  
  ''' </summary>  
  ''' <param name="pTruncateLength"></param>  
  Public Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
 
    For Each lTableSize As csTableSize In Me 
      lTableSize.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the TableSizes by the chosen parameters. This function may be a bit slower than accessing the TableSize's FillBy... directly 
  ''' </summary> 
  ''' <param name="vWhichParameterCombination"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vHowMany"></param> 
  ''' <param name="vDir"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillByParameters(ByVal vWhichParameterCombination As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault 
    Dim pFunctionParameters As String = String.Format("WhichParameterCombination={0}", vWhichParameterCombination.ToString()) 
    Dim pFault As clsFault 
 
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-TableSize-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-TableSize-151223_1716", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a collection of all the items, or a sub-collection defined by HowMany and Direction
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
    
    Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csTableSizeColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the TableSize 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TableName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedTableName(ByVal vTableNameFrom As String, ByVal vTableNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableNameFrom={0}, TableNameTo={1}", vTableNameFrom, vTableNameTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vTableNameFrom 
          If vTableNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vTableNameFrom) 
          ' 
          'vTableNameTo 
          If vTableNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vTableNameTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csTableSizeColFillByBoundedTableName" 
      Dim pParametersToLog = $"TableName: {vTableNameFrom};{vTableNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the TableSize  
      If vAppend = True Then 
        Dim pTableSizes As New csTableSizeCol 
        pTableSizes.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pTableSizes) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDFrom 
          pBinaryWriter.Write(vIDFrom) 
          ' 
          'vIDTo 
          pBinaryWriter.Write(vIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csTableSizeColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the TableSize  
      If vAppend = True Then 
        Dim pTableSizes As New csTableSizeCol 
        pTableSizes.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pTableSizes) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TableName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardTableName(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}, TableNameWildcardType={1}", vTableName, vTableNameWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vTableName 
          If vTableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vTableName) 
          ' 
          pBinaryWriter.Write(vTableNameWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csTableSizeColFillByWildCardTableName" 
      Dim pParametersToLog = $"TableName: {vTableName};{vTableNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the TableSize  
      If vAppend = True Then 
        Dim pTableSizes As New csTableSizeCol 
        pTableSizes.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pTableSizes) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>  
  ''' Gets a collection of all the items for the specified list of ID's. To append to an existing collection, set vAppend to true (default is false).  
  ''' An ID can only exist once in the collection. If it's already in the collection, it will be removed from vIDs before sending to the server. 
  ''' </summary>  
  ''' <param name="vIDs"></param>  
  ''' <param name="vRequester"></param>  
  ''' <param name="vDir"></param>  
  ''' <param name="vAppend"></param>  
  ''' <returns></returns>  
  Public Function FillByListOfID(vIDs As List(Of Long), vRequester As clsRequester, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = $"Count of IDs: {vIDs?.Count}" 
    Dim pFault As New clsFault 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    'If it's append, we have to ensure no doubles, even though we're not sending the collection to the server 
    If vAppend = True Then 
      For Each l In Me 
        If vIDs.Contains(l.ID) Then 
          vIDs.Remove(l.ID) 
        End If 
      Next 
    End If 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDs 
          pBinaryWriter.Write(vIDs.Count) 
          For Each l In vIDs 
            pBinaryWriter.Write(l) 
          Next 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csTableSizeColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the TableSize   
      If vAppend = True Then 
        Dim pTableSizes As New csTableSizeCol 
        pTableSizes.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pTableSizes) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-231207-1750", vRequester) 
    End Try 
 
    pFault.SetOK() 
    RaiseEvent evtAfterFill() 
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault 
  End Function 
 
  Public Enum enmFillOnTheFlyParameters 
    UD 
    IDFrom
    IDTo
    [TableName]
    TableNameWildcardType
  End Enum 
  Public Enum enmListDefinition 
    UD 
    HowMany 
    Dir 
  End Enum 
 
  Public Overrides Function FillOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
    Return pFault.LogFreeTextFault(141, "", "", "TRGT-TableSize-160112", vRequester) 
  End Function 
  ''' <summary>
  ''' Gets a collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, TableName={2}, TableNameWildcardType={3}", vIDFrom, vIDTo, vTableName, vTableNameWildcardType.FastToString())
    
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Dim pParametersToLog = $"" 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) : pParametersToLog &= $"IDFrom={vIDFrom};"  
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) : pParametersToLog &= $"IDTo={vIDTo};"  
          'TableName 
          If vTableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vTableName) : pBinaryWriter.Write(vTableNameWildcardType.FastToString()) : pParametersToLog &= $"TableName={vTableName};" : pParametersToLog &= $"TableNameWildcardType={vTableNameWildcardType};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csTableSizeColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the TableSize  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overrides Function FillSumOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
    Return pFault.LogFreeTextFault(141, "", "", "TRGT-TableSize-160114", vRequester) 
  End Function 
  ''' <summary>
  ''' Gets a grouped collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillSumOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, TableName={2}, TableNameWildcardType={3}", vIDFrom, vIDTo, vTableName, vTableNameWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Dim pParametersToLog = $"" 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) : pParametersToLog &= $"IDFrom={vIDFrom};"  
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) : pParametersToLog &= $"IDTo={vIDTo};"  
          'TableName 
          If vTableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vTableName) : pBinaryWriter.Write(vTableNameWildcardType.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csTableSizeColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the TableSize  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-TableSize-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vTableSizeArray As csTableSize())
    Me.Clear()
    
    For Each pTableSize As csTableSize In vTableSizeArray
      Me.Add(pTableSize)
      _Clean.Add(pTableSize.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pTableSize As New csTableSize(pRow, vRequester) 
        Me.Add(pTableSize) 
        _Clean.Add(pTableSize.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-TableSizeCol-130315-2118", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Overrides Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    rXML = "" 
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
 
      rXML = MyStringBuilder.ToString() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-TableSize-130515-1300", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function FillFromXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pTableSizes As csTableSizeCol = CType(pXmlSerializer.Deserialize(pStreamReader), csTableSizeCol) 
      For Each pTableSize As csTableSize In pTableSizes 
        Me.Add(pTableSize) 
        _Clean.Add(pTableSize.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-TableSize-130515-1329", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' Returns JSON for public properties in collection 
  ''' </summary> 
  ''' <param name="rJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-TableSize-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  ''' <summary> 
  '''   ''' Creates collection using JSON received, for public properties 
  ''' </summary> 
  ''' <param name="vJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault 
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
 
      Dim pTableSizes As List(Of csTableSize) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csTableSize))(vJSON, pSettings) 
      For Each pTableSize As csTableSize In pTableSizes 
        Me.Add(pTableSize) 
        _Clean.Add(pTableSize.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-TableSize-190720-2059", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Overrides Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Dim pBytes As Byte() = Nothing 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'Tag  
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'Items 
          pBinaryWriter.Write(Me.Count) 
          For Each lTableSize As csTableSize In Me 
            Dim pByte As Byte() = lTableSize.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
            pBinaryWriter.Write(pByte.Length) 
            pBinaryWriter.Write(pByte, 0, pByte.Length) 
          Next 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, pFunctionParameters, "TRGT-TableSize-150307-2340", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Public Overrides Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
 
    Me.Clear() 
    
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'Tag  
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'Items 
          Dim pCount As Integer = pReader.ReadInt32 
          For iCntr As Integer = 0 To pCount - 1 
            Dim pLength As Integer = pReader.ReadInt32 
            Dim pTableSize As csTableSize = New csTableSize(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pTableSize) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pTableSize.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-TableSize-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pTableSize As csTableSize In Me 
      With pTableSize 
        pFault = pTableSize.LoadLookupAndEnumText(vRequester) 
        If Not pFault.isOK Then Exit For 
      End With 
    Next 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vEntitiesToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(vEntitiesToTest As ITargCCCollection) As Boolean 
    If Not (TypeOf (vEntitiesToTest) Is csTableSizeCol) Then Return False 
    Dim pTableSizeColToTest As csTableSizeCol = CType(vEntitiesToTest, csTableSizeCol) 
    Return isEqual(pTableSizeColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTableSizesToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vTableSizesToTest As csTableSizeCol) As Boolean
    If Me.Count <> vTableSizesToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vTableSizesToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pTableSizes As New csTableSizeCol() 
    If pFilledFromSumOnTheFly Then pTableSizes._FilledFromSumOnTheFly = True
    
    For Each pTableSize As csTableSize In Me 
      Dim pTableSizeClone As csTableSize = pTableSize.Clone() 
      pTableSizes.Add(pTableSizeClone) 
      If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
    Next 
    Return pTableSizes 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csTableSizeCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pTableSizes As New csTableSizeCol() 
    If pFilledFromSumOnTheFly Then pTableSizes._FilledFromSumOnTheFly = True
    
    For Each pTableSize As csTableSize In Me
      Dim pTableSizeClone As csTableSize = pTableSize.Clone()
      pTableSizes.Add(pTableSizeClone)
      If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
    Next
    Return pTableSizes
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TableName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTableName(ByVal vTableNameFrom As String, ByVal vTableNameTo As String) As csTableSizeCol 
    Dim pTableSizes As New csTableSizeCol()  
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pTableSize As csTableSize In _SortedDictionaryForFindByID.Values.ToList() 
      If (pTableSize.TableName > vTableNameFrom AndAlso pTableSize.TableName <= vTableNameTo) Then 
        Dim pTableSizeClone As csTableSize = pTableSize.Clone() 
        pTableSizes.Add(pTableSizeClone) 
        If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
      End If 
    Next 
    Return pTableSizes 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csTableSizeCol 
    Dim pTableSizes As New csTableSizeCol()  
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pTableSize As csTableSize In _SortedDictionaryForFindByID.Values.ToList() 
      If (pTableSize.ID > vIDFrom AndAlso pTableSize.ID <= vIDTo) Then 
        Dim pTableSizeClone As csTableSize = pTableSize.Clone() 
        pTableSizes.Add(pTableSizeClone) 
        If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
      End If 
    Next 
    Return pTableSizes 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardTableName(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType) As csTableSizeCol 
    Dim pTableSizes As New csTableSizeCol 
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pTableSize As csTableSize In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pTableSize.TableName.StartsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pTableSize.TableName.EndsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pTableSize.TableName.IndexOf(vTableName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vTableName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pTableSize.TableName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pTableSizeClone As csTableSize = pTableSize.Clone() 
      pTableSizes.Add(pTableSizeClone) 
    Next 
    Return pTableSizes 
  End Function 
  
  ''' <summary> 
  ''' Used for Interface compliance. This returns a unique object in the collection. It searches locally, within the collection. It does not access the database  
  ''' If it doesn't find anything, it creates a new, empty object 
  ''' </summary> 
  ''' <param name="vPrimaryKey"></param> 
  ''' <returns></returns> 
  Public Overrides Function FindByPrimaryKey(vPrimaryKey As Long) As ITargCCEntity 
    Return FindByID(vPrimaryKey) 
  End Function 
 
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByTableName(ByVal vTableName As String) As csTableSize
    If Me.Count = 0 Then Return New csTableSize 
    
    If _RecreateDictionaryForFindByTableName = True Then LoadTableNames() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csTableSize) = _SortedDictionaryForFindByTableName 
    
    Dim pTableSize As csTableSize = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vTableName
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pTableSize) 
    If pTableSize IsNot Nothing Then Return pTableSize Else Return New csTableSize() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByID(ByVal vID As Long) As csTableSize
    If Me.Count = 0 Then Return New csTableSize 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csTableSize) = _SortedDictionaryForFindByID 
    
    Dim pTableSize As csTableSize = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pTableSize) 
    If pTableSize IsNot Nothing Then Return pTableSize Else Return New csTableSize() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TableName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTableName(ByVal vTableName As String) As csTableSizeCol
    Dim pTableSizes As New csTableSizeCol() 
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTableSize) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTableName = vTableName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTableSize As csTableSize In pTempDist.Values
        If pTableSize.TableName.ToLowerInvariant() = vTableName Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTableName with vTableName of {vTableName}", "2ndPartOfClone") 
      Dim pList As csTableSizeCol = Me.Clone() 
      For Each pTableSize As csTableSize In pList 
        If pTableSize.TableName.ToLowerInvariant() = vTableName Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    End If 
    
    Return pTableSizes
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined NumberOfRows
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNumberOfRows(ByVal vNumberOfRows As Integer) As csTableSizeCol
    Dim pTableSizes As New csTableSizeCol() 
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTableSize) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTableSize As csTableSize In pTempDist.Values
        If pTableSize.NumberOfRows = vNumberOfRows Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNumberOfRows with vNumberOfRows of {vNumberOfRows}", "2ndPartOfClone") 
      Dim pList As csTableSizeCol = Me.Clone() 
      For Each pTableSize As csTableSize In pList 
        If pTableSize.NumberOfRows = vNumberOfRows Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    End If 
    
    Return pTableSizes
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ReservedSizeKb
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByReservedSizeKb(ByVal vReservedSizeKb As Integer) As csTableSizeCol
    Dim pTableSizes As New csTableSizeCol() 
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTableSize) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTableSize As csTableSize In pTempDist.Values
        If pTableSize.ReservedSizeKb = vReservedSizeKb Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByReservedSizeKb with vReservedSizeKb of {vReservedSizeKb}", "2ndPartOfClone") 
      Dim pList As csTableSizeCol = Me.Clone() 
      For Each pTableSize As csTableSize In pList 
        If pTableSize.ReservedSizeKb = vReservedSizeKb Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    End If 
    
    Return pTableSizes
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DataSizeKb
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDataSizeKb(ByVal vDataSizeKb As Integer) As csTableSizeCol
    Dim pTableSizes As New csTableSizeCol() 
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTableSize) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTableSize As csTableSize In pTempDist.Values
        If pTableSize.DataSizeKb = vDataSizeKb Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDataSizeKb with vDataSizeKb of {vDataSizeKb}", "2ndPartOfClone") 
      Dim pList As csTableSizeCol = Me.Clone() 
      For Each pTableSize As csTableSize In pList 
        If pTableSize.DataSizeKb = vDataSizeKb Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    End If 
    
    Return pTableSizes
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IndexSizeKb
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIndexSizeKb(ByVal vIndexSizeKb As Integer) As csTableSizeCol
    Dim pTableSizes As New csTableSizeCol() 
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTableSize) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTableSize As csTableSize In pTempDist.Values
        If pTableSize.IndexSizeKb = vIndexSizeKb Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIndexSizeKb with vIndexSizeKb of {vIndexSizeKb}", "2ndPartOfClone") 
      Dim pList As csTableSizeCol = Me.Clone() 
      For Each pTableSize As csTableSize In pList 
        If pTableSize.IndexSizeKb = vIndexSizeKb Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    End If 
    
    Return pTableSizes
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UnusedSizeKb
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUnusedSizeKb(ByVal vUnusedSizeKb As Integer) As csTableSizeCol
    Dim pTableSizes As New csTableSizeCol() 
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTableSize) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTableSize As csTableSize In pTempDist.Values
        If pTableSize.UnusedSizeKb = vUnusedSizeKb Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUnusedSizeKb with vUnusedSizeKb of {vUnusedSizeKb}", "2ndPartOfClone") 
      Dim pList As csTableSizeCol = Me.Clone() 
      For Each pTableSize As csTableSize In pList 
        If pTableSize.UnusedSizeKb = vUnusedSizeKb Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    End If 
    
    Return pTableSizes
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csTableSizeCol
    Dim pTableSizes As New csTableSizeCol() 
    pTableSizes._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTableSize) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTableSize As csTableSize In pTempDist.Values
        If pTableSize.Tag.ToLowerInvariant() = vTag Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csTableSizeCol = Me.Clone() 
      For Each pTableSize As csTableSize In pList 
        If pTableSize.Tag.ToLowerInvariant() = vTag Then
          Dim pTableSizeClone As csTableSize = pTableSize.Clone()
          pTableSizes.Add(pTableSizeClone)
          If Not _FilledFromSumOnTheFly Then pTableSizes._Clean.Add(pTableSize.ID) 
        End If
      Next
    End If 
    
    Return pTableSizes
  End Function
  
  ''' <summary> 
  ''' Loads Me into the datatable vDataTable provided. 
  ''' </summary> 
  ''' <param name="vDataTable"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadMeIntoDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    vDataTable.Rows.Clear() 
    For Each pTableSize As csTableSize In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pTableSize.LoadDataRow(pRow, vRequester) 
      If pFault.isOK = False Then Return pFault 
      vDataTable.Rows.Add(pRow) 
    Next 
 
    Return pFault.SetOK 
  End Function 
 
  ''' <summary> 
  ''' Used for Interface compliance. Sorts the Entity by the Primary Key (usually ID) 
  ''' </summary> 
  Public Overrides Sub SortByPrimaryKey() 
    SortByID() 
  End Sub 
 
  Public Sub SortByID()
    Me.Sort(New csTableSizeCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csTableSize)
    Private Function Compare(ByVal x As csTableSize, ByVal y As csTableSize) As Integer Implements System.Collections.Generic.IComparer(Of csTableSize).Compare
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
  
  Public Sub SortByTableName()
    Me.Sort(New csTableSizeCol.CompareByTableName)
  End Sub
  Private Class CompareByTableName
    Implements IComparer(Of csTableSize)
    Private Function Compare(ByVal x As csTableSize, ByVal y As csTableSize) As Integer Implements System.Collections.Generic.IComparer(Of csTableSize).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TableName, y.TableName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNumberOfRows()
    Me.Sort(New csTableSizeCol.CompareByNumberOfRows)
  End Sub
  Private Class CompareByNumberOfRows
    Implements IComparer(Of csTableSize)
    Private Function Compare(ByVal x As csTableSize, ByVal y As csTableSize) As Integer Implements System.Collections.Generic.IComparer(Of csTableSize).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.NumberOfRows < y.NumberOfRows Then
        Return -1
      ElseIf x.NumberOfRows = y.NumberOfRows Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByReservedSizeKb()
    Me.Sort(New csTableSizeCol.CompareByReservedSizeKb)
  End Sub
  Private Class CompareByReservedSizeKb
    Implements IComparer(Of csTableSize)
    Private Function Compare(ByVal x As csTableSize, ByVal y As csTableSize) As Integer Implements System.Collections.Generic.IComparer(Of csTableSize).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ReservedSizeKb < y.ReservedSizeKb Then
        Return -1
      ElseIf x.ReservedSizeKb = y.ReservedSizeKb Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDataSizeKb()
    Me.Sort(New csTableSizeCol.CompareByDataSizeKb)
  End Sub
  Private Class CompareByDataSizeKb
    Implements IComparer(Of csTableSize)
    Private Function Compare(ByVal x As csTableSize, ByVal y As csTableSize) As Integer Implements System.Collections.Generic.IComparer(Of csTableSize).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DataSizeKb < y.DataSizeKb Then
        Return -1
      ElseIf x.DataSizeKb = y.DataSizeKb Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByIndexSizeKb()
    Me.Sort(New csTableSizeCol.CompareByIndexSizeKb)
  End Sub
  Private Class CompareByIndexSizeKb
    Implements IComparer(Of csTableSize)
    Private Function Compare(ByVal x As csTableSize, ByVal y As csTableSize) As Integer Implements System.Collections.Generic.IComparer(Of csTableSize).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.IndexSizeKb < y.IndexSizeKb Then
        Return -1
      ElseIf x.IndexSizeKb = y.IndexSizeKb Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUnusedSizeKb()
    Me.Sort(New csTableSizeCol.CompareByUnusedSizeKb)
  End Sub
  Private Class CompareByUnusedSizeKb
    Implements IComparer(Of csTableSize)
    Private Function Compare(ByVal x As csTableSize, ByVal y As csTableSize) As Integer Implements System.Collections.Generic.IComparer(Of csTableSize).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.UnusedSizeKb < y.UnusedSizeKb Then
        Return -1
      ElseIf x.UnusedSizeKb = y.UnusedSizeKb Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csTableSizeCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csTableSize)
    Private Function Compare(ByVal x As csTableSize, ByVal y As csTableSize) As Integer Implements System.Collections.Generic.IComparer(Of csTableSize).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Tag, y.Tag, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByTableName = New Dictionary(Of String, csTableSize)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByTableName = False 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csTableSize) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByTableName = New Dictionary(Of String, csTableSize)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csTableSize) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = True 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
