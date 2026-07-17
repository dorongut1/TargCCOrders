Public Class csAuditIndexed
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
      Return False 
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
    [OriginalID] 
    [TableName] 
    [RowID] 
    [Operation] 
    [OccurredAt] 
    [SqlCurrentUser] 
    [FieldName] 
    [OldValue] 
    [NewValue] 
    [ChangedByUser] 
    [ActiveLoginID] 
    [SqlSystemUser] 
    [SqlAppName] 
    [SqlHostName] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [OriginalID] 
    [RowID] 
    [ActiveLoginID] 
  End Enum 
  
  Private _ID As Long
  Private _OriginalID As Long
  Private _TableName As String
  Private _RowID As Long
  Private _Operation As String
  Private _OccurredAt As Date
  Private _SqlCurrentUser As String
  Private _FieldName As String
  Private _OldValue As String
  Private _NewValue As String
  Private _ChangedByUser As String
  Private _ActiveLoginID As Long
  Private _SqlSystemUser As String
  Private _SqlAppName As String
  Private _SqlHostName As String
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
  Public Property [OriginalID]() As Long
    Get
      Return Me._OriginalID
    End Get
    Set(ByVal value As Long)
      If Me._OriginalID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OriginalID = value 
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
      End If 
    End Set
  End Property
  Public Property [RowID]() As Long
    Get
      Return Me._RowID
    End Get
    Set(ByVal value As Long)
      If Me._RowID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RowID = value 
      End If 
    End Set
  End Property
  Public Property [Operation]() As String
    Get
      Return Me._Operation
    End Get
    Set(ByVal value As String)
      If Me._Operation <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Operation = value 
      End If 
    End Set
  End Property
  Public Property [OccurredAt]() As Date
    Get
      Return Me._OccurredAt
    End Get
    Set(ByVal value As Date)
      If Me._OccurredAt <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OccurredAt = value 
      End If 
    End Set
  End Property
  Public Property [SqlCurrentUser]() As String
    Get
      Return Me._SqlCurrentUser
    End Get
    Set(ByVal value As String)
      If Me._SqlCurrentUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SqlCurrentUser = value 
      End If 
    End Set
  End Property
  Public Property [FieldName]() As String
    Get
      Return Me._FieldName
    End Get
    Set(ByVal value As String)
      If Me._FieldName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FieldName = value 
      End If 
    End Set
  End Property
  Public Property [OldValue]() As String
    Get
      Return Me._OldValue
    End Get
    Set(ByVal value As String)
      If Me._OldValue <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OldValue = value 
      End If 
    End Set
  End Property
  Public Property [NewValue]() As String
    Get
      Return Me._NewValue
    End Get
    Set(ByVal value As String)
      If Me._NewValue <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._NewValue = value 
      End If 
    End Set
  End Property
  Public Property [ChangedByUser]() As String
    Get
      Return Me._ChangedByUser
    End Get
    Set(ByVal value As String)
      If Me._ChangedByUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ChangedByUser = value 
      End If 
    End Set
  End Property
  Public Property [ActiveLoginID]() As Long
    Get
      Return Me._ActiveLoginID
    End Get
    Set(ByVal value As Long)
      If Me._ActiveLoginID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ActiveLoginID = value 
      End If 
    End Set
  End Property
  Public Property [SqlSystemUser]() As String
    Get
      Return Me._SqlSystemUser
    End Get
    Set(ByVal value As String)
      If Me._SqlSystemUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SqlSystemUser = value 
      End If 
    End Set
  End Property
  Public Property [SqlAppName]() As String
    Get
      Return Me._SqlAppName
    End Get
    Set(ByVal value As String)
      If Me._SqlAppName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SqlAppName = value 
      End If 
    End Set
  End Property
  Public Property [SqlHostName]() As String
    Get
      Return Me._SqlHostName
    End Get
    Set(ByVal value As String)
      If Me._SqlHostName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SqlHostName = value 
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
    bDefaultDesignation = "" 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _OriginalID <> 0 Then pValue.Append("OriginalID='" & _OriginalID.ToString() & "' ‡ ") 
    If _TableName <> "" Then pValue.Append("TableName='" & _TableName & "' ‡ ") 
    If _RowID <> 0 Then pValue.Append("RowID='" & _RowID.ToString() & "' ‡ ") 
    If _Operation <> "" Then pValue.Append("Operation='" & _Operation & "' ‡ ") 
    If Not (_OccurredAt = Nothing) Then pValue.Append("OccurredAt='" & _OccurredAt.ToString("o") & "' ‡ ") 
    If _SqlCurrentUser <> "" Then pValue.Append("SqlCurrentUser='" & _SqlCurrentUser & "' ‡ ") 
    If _FieldName <> "" Then pValue.Append("FieldName='" & _FieldName & "' ‡ ") 
    If _OldValue <> "" Then pValue.Append("OldValue='" & _OldValue & "' ‡ ") 
    If _NewValue <> "" Then pValue.Append("NewValue='" & _NewValue & "' ‡ ") 
    If _ChangedByUser <> "" Then pValue.Append("ChangedByUser='" & _ChangedByUser & "' ‡ ") 
    If _ActiveLoginID <> 0 Then pValue.Append("ActiveLoginID='" & _ActiveLoginID.ToString() & "' ‡ ") 
    If _SqlSystemUser <> "" Then pValue.Append("SqlSystemUser='" & _SqlSystemUser & "' ‡ ") 
    If _SqlAppName <> "" Then pValue.Append("SqlAppName='" & _SqlAppName & "' ‡ ") 
    If _SqlHostName <> "" Then pValue.Append("SqlHostName='" & _SqlHostName & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _OriginalID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TableName)}""") 
    pCSV.Append("," & _RowID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Operation)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OccurredAt.ToShortDateString & " " & _OccurredAt.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlCurrentUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FieldName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OldValue)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_NewValue)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ChangedByUser)}""") 
    pCSV.Append("," & _ActiveLoginID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlSystemUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlAppName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlHostName)}""") 
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
  
  Public Sub New(ByVal vcsAuditIndexed As csAuditIndexed)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsAuditIndexed) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vOriginalID As Long = 0 _ 
    , Optional vTableName As String = "" _ 
    , Optional vRowID As Long = 0 _ 
    , Optional vOperation As String = "" _ 
    , Optional vOccurredAt As Date = Nothing _ 
    , Optional vSqlCurrentUser As String = "" _ 
    , Optional vFieldName As String = "" _ 
    , Optional vOldValue As String = "" _ 
    , Optional vNewValue As String = "" _ 
    , Optional vChangedByUser As String = "" _ 
    , Optional vActiveLoginID As Long = 0 _ 
    , Optional vSqlSystemUser As String = "" _ 
    , Optional vSqlAppName As String = "" _ 
    , Optional vSqlHostName As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _OriginalID = vOriginalID 
    _TableName = vTableName 
    _RowID = vRowID 
    _Operation = vOperation 
    _OccurredAt = vOccurredAt 
    _SqlCurrentUser = vSqlCurrentUser 
    _FieldName = vFieldName 
    _OldValue = vOldValue 
    _NewValue = vNewValue 
    _ChangedByUser = vChangedByUser 
    _ActiveLoginID = vActiveLoginID 
    _SqlSystemUser = vSqlSystemUser 
    _SqlAppName = vSqlAppName 
    _SqlHostName = vSqlHostName 
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
    _Operation = _Operation.Truncate(pTruncateLength, _IsTruncated) 
    _SqlCurrentUser = _SqlCurrentUser.Truncate(pTruncateLength, _IsTruncated) 
    _FieldName = _FieldName.Truncate(pTruncateLength, _IsTruncated) 
    _OldValue = _OldValue.Truncate(pTruncateLength, _IsTruncated) 
    _NewValue = _NewValue.Truncate(pTruncateLength, _IsTruncated) 
    _ChangedByUser = _ChangedByUser.Truncate(pTruncateLength, _IsTruncated) 
    _SqlSystemUser = _SqlSystemUser.Truncate(pTruncateLength, _IsTruncated) 
    _SqlAppName = _SqlAppName.Truncate(pTruncateLength, _IsTruncated) 
    _SqlHostName = _SqlHostName.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the AuditIndexed by PrimaryKey (ID) 
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
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AuditIndexed-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the AuditIndexed by the chosen parameters. This function may be a bit slower than accessing the AuditIndexed's GetBy... directly 
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
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-AuditIndexed-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AuditIndexed-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the AuditIndexed by ID. 
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
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
      Dim pFunction As String = "csAuditIndexedGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the AuditIndexed 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150308-1015", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is csAuditIndexed) Then Return False 
    Dim pAuditIndexedToTest As csAuditIndexed = CType(vTargCCEntityToTest, csAuditIndexed) 
    Return isEqual(pAuditIndexedToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vAuditIndexedToTest As csAuditIndexed) As Boolean
    With vAuditIndexedToTest
      If _ID <> .ID Then Return False
      If _OriginalID <> .OriginalID Then Return False
      If _TableName <> .TableName Then Return False
      If _RowID <> .RowID Then Return False
      If _Operation <> .Operation Then Return False
      If _OccurredAt <> Nothing AndAlso .OccurredAt <> Nothing Then 
        If ccHelper.ToLong(_OccurredAt.Subtract(.OccurredAt).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_OccurredAt = Nothing AndAlso .OccurredAt = Nothing) Then 
        Return False 
      End If 
      If _SqlCurrentUser <> .SqlCurrentUser Then Return False
      If _FieldName <> .FieldName Then Return False
      If _OldValue <> .OldValue Then Return False
      If _NewValue <> .NewValue Then Return False
      If _ChangedByUser <> .ChangedByUser Then Return False
      If _ActiveLoginID <> .ActiveLoginID Then Return False
      If _SqlSystemUser <> .SqlSystemUser Then Return False
      If _SqlAppName <> .SqlAppName Then Return False
      If _SqlHostName <> .SqlHostName Then Return False
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
    Dim pClone As New csAuditIndexed(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csAuditIndexed
    Dim pClone As New csAuditIndexed(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("OriginalID") = _OriginalID : Catch ex As Exception : Return pFault.LogException(ex, "OriginalID", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("TableName") = _TableName : Catch ex As Exception : Return pFault.LogException(ex, "TableName", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("RowID") = _RowID : Catch ex As Exception : Return pFault.LogException(ex, "RowID", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("Operation") = _Operation : Catch ex As Exception : Return pFault.LogException(ex, "Operation", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("OccurredAt") = _OccurredAt : Catch ex As Exception : Return pFault.LogException(ex, "OccurredAt", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlCurrentUser") = _SqlCurrentUser : Catch ex As Exception : Return pFault.LogException(ex, "SqlCurrentUser", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("FieldName") = _FieldName : Catch ex As Exception : Return pFault.LogException(ex, "FieldName", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("OldValue") = _OldValue : Catch ex As Exception : Return pFault.LogException(ex, "OldValue", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("NewValue") = _NewValue : Catch ex As Exception : Return pFault.LogException(ex, "NewValue", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("ChangedByUser") = _ChangedByUser : Catch ex As Exception : Return pFault.LogException(ex, "ChangedByUser", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("ActiveLoginID") = _ActiveLoginID : Catch ex As Exception : Return pFault.LogException(ex, "ActiveLoginID", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlSystemUser") = _SqlSystemUser : Catch ex As Exception : Return pFault.LogException(ex, "SqlSystemUser", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlAppName") = _SqlAppName : Catch ex As Exception : Return pFault.LogException(ex, "SqlAppName", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlHostName") = _SqlHostName : Catch ex As Exception : Return pFault.LogException(ex, "SqlHostName", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pAuditIndexed As csAuditIndexed = CType(pXmlSerializer.Deserialize(pStreamReader), csAuditIndexed) 
      AssignValues(pAuditIndexed) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-AuditIndexed-130515-1230", vRequester) 
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
          'OriginalID 
          pBinaryWriter.Write(_OriginalID) 
          'TableName 
          If _TableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_TableName) 
          'RowID 
          pBinaryWriter.Write(_RowID) 
          'Operation 
          If _Operation Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Operation) 
          'OccurredAt 
          pBinaryWriter.Write(_OccurredAt.Ticks) 
          'SqlCurrentUser 
          If _SqlCurrentUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SqlCurrentUser) 
          'FieldName 
          If _FieldName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FieldName) 
          'OldValue 
          If _OldValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_OldValue) 
          'NewValue 
          If _NewValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_NewValue) 
          'ChangedByUser 
          If _ChangedByUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ChangedByUser) 
          'ActiveLoginID 
          pBinaryWriter.Write(_ActiveLoginID) 
          'SqlSystemUser 
          If _SqlSystemUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SqlSystemUser) 
          'SqlAppName 
          If _SqlAppName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SqlAppName) 
          'SqlHostName 
          If _SqlHostName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SqlHostName) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-150307-2338", vRequester) 
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
          'OriginalID 
          _OriginalID = pReader.ReadInt64 
          'TableName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _TableName = pReader.ReadString 
          'RowID 
          _RowID = pReader.ReadInt64 
          'Operation 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Operation = pReader.ReadString 
          'OccurredAt 
          _OccurredAt = New Date(pReader.ReadInt64) 
          'SqlCurrentUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlCurrentUser = pReader.ReadString 
          'FieldName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FieldName = pReader.ReadString 
          'OldValue 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _OldValue = pReader.ReadString 
          'NewValue 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _NewValue = pReader.ReadString 
          'ChangedByUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ChangedByUser = pReader.ReadString 
          'ActiveLoginID 
          _ActiveLoginID = pReader.ReadInt64 
          'SqlSystemUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlSystemUser = pReader.ReadString 
          'SqlAppName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlAppName = pReader.ReadString 
          'SqlHostName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlHostName = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-AuditIndexed-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-190720-1443", vRequester) 
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
 
      Dim pAuditIndexed As csAuditIndexed = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csAuditIndexed)(vJSON, pSettings) 
      AssignValues(pAuditIndexed) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vAuditIndexed As csAuditIndexed)
    With vAuditIndexed
      _ID = .ID 
      _OriginalID = .OriginalID 
      _TableName = .TableName 
      _RowID = .RowID 
      _Operation = .Operation 
      _OccurredAt = .OccurredAt 
      _SqlCurrentUser = .SqlCurrentUser 
      _FieldName = .FieldName 
      _OldValue = .OldValue 
      _NewValue = .NewValue 
      _ChangedByUser = .ChangedByUser 
      _ActiveLoginID = .ActiveLoginID 
      _SqlSystemUser = .SqlSystemUser 
      _SqlAppName = .SqlAppName 
      _SqlHostName = .SqlHostName 
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
 
    If _ID = 0 Then 
      Return pFault.SetOK() 
    End If 
 
    'There are no enums or lookups. This function was added to this object for interface compatibility 
    Return pFault.SetOK() 
  End Function 
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _OriginalID = 0
    _TableName = ""
    _RowID = 0
    _Operation = ""
    _OccurredAt = Nothing
    _SqlCurrentUser = ""
    _FieldName = ""
    _OldValue = ""
    _NewValue = ""
    _ChangedByUser = ""
    _ActiveLoginID = 0
    _SqlSystemUser = ""
    _SqlAppName = ""
    _SqlHostName = ""
    _Tag = ""
    bccStatus = clsEnums.enmObjectStatus.New 
    bPrimaryKey = _ID 
    bDateAdded = Nothing 
    bDefaultDesignation = "" 
     
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class csAuditIndexedCol
  Inherits cTargCCCollection(Of csAuditIndexed)
  
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
      Return False 
    End Get 
  End Property 
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Long) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csAuditIndexed) 
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
 
    For Each pRow As csAuditIndexed In Me 
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
    pCSVTitle.Append(",""OriginalID""") 
    pCSVTitle.Append(",""TableName""") 
    pCSVTitle.Append(",""RowID""") 
    pCSVTitle.Append(",""Operation""") 
    pCSVTitle.Append(",""OccurredAt""") 
    pCSVTitle.Append(",""SqlCurrentUser""") 
    pCSVTitle.Append(",""FieldName""") 
    pCSVTitle.Append(",""OldValue""") 
    pCSVTitle.Append(",""NewValue""") 
    pCSVTitle.Append(",""ChangedByUser""") 
    pCSVTitle.Append(",""ActiveLoginID""") 
    pCSVTitle.Append(",""SqlSystemUser""") 
    pCSVTitle.Append(",""SqlAppName""") 
    pCSVTitle.Append(",""SqlHostName""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csAuditIndexed In Me 
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
 
  Public Overloads Sub Add(ByVal vAuditIndexed As csAuditIndexed) 
    SyncLock _CollectionLock 
      MyBase.Add(vAuditIndexed) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vAuditIndexed As csAuditIndexed) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vAuditIndexed) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vAuditIndexedCol As csAuditIndexedCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vAuditIndexedCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vAuditIndexed As csAuditIndexed) 
    SyncLock _CollectionLock 
      MyBase.Remove(vAuditIndexed) 
      _RecreateDictionaryForFindByID = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csAuditIndexed) 
      
      For Each lAuditIndexed In Me 
        If lAuditIndexed.IsEmpty OrElse pTempDictionary.ContainsKey(lAuditIndexed.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lAuditIndexed.ID, lAuditIndexed) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lAuditIndexed.ToString, "TRGT-AuditIndexed-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", AuditIndexed:" & lAuditIndexed.ToString() & ", TRGT-AuditIndexed-260111-154657") 'Send it up the line 
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
 
    For Each lAuditIndexed As csAuditIndexed In Me 
      lAuditIndexed.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [ActiveLoginID] 
    [ChangedByUser] 
    [FieldName] 
    [OccurredAt] 
    [OriginalID] 
    [RowID] 
    [TableName] 
    [TableNameAndRowID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the AuditIndexeds by the chosen parameters. This function may be a bit slower than accessing the AuditIndexed's FillBy... directly 
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
        Case enmFillByParameterCombination.ActiveLoginID 
          pFault = FillByActiveLoginID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.ChangedByUser 
          pFault = FillByChangedByUser(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.FieldName 
          pFault = FillByFieldName(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OccurredAt 
          pFault = FillByOccurredAt(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OriginalID 
          pFault = FillByOriginalID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.RowID 
          pFault = FillByRowID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TableName 
          pFault = FillByTableName(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TableNameAndRowID 
          pFault = FillByTableNameAndRowID(CStr(vParameters(0)), ccHelper.ToLong(vParameters(1)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-AuditIndexed-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AuditIndexed-151223_1716", vRequester) 
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
 
      Dim pFunction As String = "csAuditIndexedColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ActiveLoginID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByActiveLoginID(ByVal vActiveLoginID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ActiveLoginID={0}", vActiveLoginID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vActiveLoginID 
          pBinaryWriter.Write(vActiveLoginID) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByActiveLoginID" 
      Dim pParametersToLog = $"ActiveLoginID: {vActiveLoginID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ChangedByUser, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByChangedByUser(ByVal vChangedByUser As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUser={0}", vChangedByUser)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vChangedByUser 
          If vChangedByUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vChangedByUser) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByChangedByUser" 
      Dim pParametersToLog = $"ChangedByUser: {vChangedByUser};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FieldName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByFieldName(ByVal vFieldName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldName={0}", vFieldName)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vFieldName 
          If vFieldName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFieldName) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByFieldName" 
      Dim pParametersToLog = $"FieldName: {vFieldName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OccurredAt, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOccurredAt(ByVal vOccurredAt As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OccurredAt={0}", vOccurredAt)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOccurredAt 
          pBinaryWriter.Write(vOccurredAt.Ticks) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByOccurredAt" 
      Dim pParametersToLog = $"OccurredAt: {vOccurredAt};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OriginalID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOriginalID(ByVal vOriginalID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginalID={0}", vOriginalID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOriginalID 
          pBinaryWriter.Write(vOriginalID) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByOriginalID" 
      Dim pParametersToLog = $"OriginalID: {vOriginalID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific RowID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByRowID(ByVal vRowID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("RowID={0}", vRowID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vRowID 
          pBinaryWriter.Write(vRowID) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByRowID" 
      Dim pParametersToLog = $"RowID: {vRowID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
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
  Public Function FillByTableName(ByVal vTableName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}", vTableName)
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
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByTableName" 
      Dim pParametersToLog = $"TableName: {vTableName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TableName and RowID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByTableNameAndRowID(ByVal vTableName As String, ByVal vRowID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}, RowID={1}", vTableName, vRowID)
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
          'vRowID 
          pBinaryWriter.Write(vRowID) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByTableNameAndRowID" 
      Dim pParametersToLog = $"TableNameAndRowID: {vTableName};{vRowID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ActiveLoginID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedActiveLoginID(ByVal vActiveLoginIDFrom As Long, ByVal vActiveLoginIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ActiveLoginIDFrom={0}, ActiveLoginIDTo={1}", vActiveLoginIDFrom, vActiveLoginIDTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vActiveLoginIDFrom 
          pBinaryWriter.Write(vActiveLoginIDFrom) 
          ' 
          'vActiveLoginIDTo 
          pBinaryWriter.Write(vActiveLoginIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedActiveLoginID" 
      Dim pParametersToLog = $"ActiveLoginID: {vActiveLoginIDFrom};{vActiveLoginIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ChangedByUser, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedChangedByUser(ByVal vChangedByUserFrom As String, ByVal vChangedByUserTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUserFrom={0}, ChangedByUserTo={1}", vChangedByUserFrom, vChangedByUserTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vChangedByUserFrom 
          If vChangedByUserFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vChangedByUserFrom) 
          ' 
          'vChangedByUserTo 
          If vChangedByUserTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vChangedByUserTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedChangedByUser" 
      Dim pParametersToLog = $"ChangedByUser: {vChangedByUserFrom};{vChangedByUserTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FieldName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedFieldName(ByVal vFieldNameFrom As String, ByVal vFieldNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldNameFrom={0}, FieldNameTo={1}", vFieldNameFrom, vFieldNameTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vFieldNameFrom 
          If vFieldNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFieldNameFrom) 
          ' 
          'vFieldNameTo 
          If vFieldNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFieldNameTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedFieldName" 
      Dim pParametersToLog = $"FieldName: {vFieldNameFrom};{vFieldNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OccurredAt, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedOccurredAt(ByVal vOccurredAtStart As Date, ByVal vOccurredAtEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OccurredAtStart={0}, OccurredAtEnd={1}", vOccurredAtStart, vOccurredAtEnd)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOccurredAtStart 
          pBinaryWriter.Write(vOccurredAtStart.Ticks) 
          ' 
          'vOccurredAtEnd 
          pBinaryWriter.Write(vOccurredAtEnd.Ticks) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedOccurredAt" 
      Dim pParametersToLog = $"OccurredAt: {vOccurredAtStart};{vOccurredAtEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OriginalID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedOriginalID(ByVal vOriginalIDFrom As Long, ByVal vOriginalIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginalIDFrom={0}, OriginalIDTo={1}", vOriginalIDFrom, vOriginalIDTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOriginalIDFrom 
          pBinaryWriter.Write(vOriginalIDFrom) 
          ' 
          'vOriginalIDTo 
          pBinaryWriter.Write(vOriginalIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedOriginalID" 
      Dim pParametersToLog = $"OriginalID: {vOriginalIDFrom};{vOriginalIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific RowID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedRowID(ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("RowIDFrom={0}, RowIDTo={1}", vRowIDFrom, vRowIDTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vRowIDFrom 
          pBinaryWriter.Write(vRowIDFrom) 
          ' 
          'vRowIDTo 
          pBinaryWriter.Write(vRowIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedRowID" 
      Dim pParametersToLog = $"RowID: {vRowIDFrom};{vRowIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedTableName" 
      Dim pParametersToLog = $"TableName: {vTableNameFrom};{vTableNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TableName and RowID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedTableNameAndRowID(ByVal vTableNameFrom As String, ByVal vTableNameTo As String, ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableNameFrom={0}, TableNameTo={1}, RowIDFrom={2}, RowIDTo={3}", vTableNameFrom, vTableNameTo, vRowIDFrom, vRowIDTo)
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
          'vRowIDFrom 
          pBinaryWriter.Write(vRowIDFrom) 
          ' 
          'vRowIDTo 
          pBinaryWriter.Write(vRowIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByBoundedTableNameAndRowID" 
      Dim pParametersToLog = $"TableNameAndRowID: {vTableNameFrom};{vTableNameTo};{vRowIDFrom};{vRowIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ChangedByUser, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardChangedByUser(ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUser={0}, ChangedByUserWildcardType={1}", vChangedByUser, vChangedByUserWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vChangedByUser 
          If vChangedByUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vChangedByUser) 
          ' 
          pBinaryWriter.Write(vChangedByUserWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByWildCardChangedByUser" 
      Dim pParametersToLog = $"ChangedByUser: {vChangedByUser};{vChangedByUserWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FieldName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardFieldName(ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldName={0}, FieldNameWildcardType={1}", vFieldName, vFieldNameWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vFieldName 
          If vFieldName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFieldName) 
          ' 
          pBinaryWriter.Write(vFieldNameWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillByWildCardFieldName" 
      Dim pParametersToLog = $"FieldName: {vFieldName};{vFieldNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csAuditIndexedColFillByWildCardTableName" 
      Dim pParametersToLog = $"TableName: {vTableName};{vTableNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csAuditIndexedColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed   
      If vAppend = True Then 
        Dim pAuditIndexeds As New csAuditIndexedCol 
        pAuditIndexeds.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pAuditIndexeds) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-231207-1750", vRequester) 
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
    OriginalIDFrom
    OriginalIDTo
    [TableName]
    TableNameWildcardType
    RowIDFrom
    RowIDTo
    OccurredAtStart
    OccurredAtEnd
    [FieldName]
    FieldNameWildcardType
    [ChangedByUser]
    ChangedByUserWildcardType
    ActiveLoginIDFrom
    ActiveLoginIDTo
  End Enum 
  Public Enum enmListDefinition 
    UD 
    HowMany 
    Dir 
  End Enum 
 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. Only send the fields you need 
  ''' </summary> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillOnTheFly(ByVal vParameters As Dictionary(Of System.Enum, Object), ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pOriginalIDFrom As Nullable(Of Long) = Nothing
    Dim pOriginalIDTo As Nullable(Of Long) = Nothing
    Dim pTableName As String = Nothing
    Dim pTableNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pRowIDFrom As Nullable(Of Long) = Nothing
    Dim pRowIDTo As Nullable(Of Long) = Nothing
    Dim pOccurredAtStart As Nullable(Of Date) = Nothing
    Dim pOccurredAtEnd As Nullable(Of Date) = Nothing
    Dim pFieldName As String = Nothing
    Dim pFieldNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pChangedByUser As String = Nothing
    Dim pChangedByUserWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pActiveLoginIDFrom As Nullable(Of Long) = Nothing
    Dim pActiveLoginIDTo As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginalIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginalIDFrom) : If pObj IsNot Nothing Then pOriginalIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginalIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginalIDTo) : If pObj IsNot Nothing Then pOriginalIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableName) Then pObj = vParameters(enmFillOnTheFlyParameters.TableName) : If pObj IsNot Nothing Then pTableName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.TableNameWildcardType) : If pObj IsNot Nothing Then pTableNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RowIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.RowIDFrom) : If pObj IsNot Nothing Then pRowIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RowIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.RowIDTo) : If pObj IsNot Nothing Then pRowIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OccurredAtStart) Then pObj = vParameters(enmFillOnTheFlyParameters.OccurredAtStart) : If pObj IsNot Nothing Then pOccurredAtStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OccurredAtEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.OccurredAtEnd) : If pObj IsNot Nothing Then pOccurredAtEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FieldName) Then pObj = vParameters(enmFillOnTheFlyParameters.FieldName) : If pObj IsNot Nothing Then pFieldName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FieldNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.FieldNameWildcardType) : If pObj IsNot Nothing Then pFieldNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ChangedByUser) Then pObj = vParameters(enmFillOnTheFlyParameters.ChangedByUser) : If pObj IsNot Nothing Then pChangedByUser = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ChangedByUserWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ChangedByUserWildcardType) : If pObj IsNot Nothing Then pChangedByUserWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ActiveLoginIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.ActiveLoginIDFrom) : If pObj IsNot Nothing Then pActiveLoginIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ActiveLoginIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.ActiveLoginIDTo) : If pObj IsNot Nothing Then pActiveLoginIDTo = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pOriginalIDFrom, pOriginalIDTo _
        , pTableName, pTableNameWildcardType _
        , pRowIDFrom, pRowIDTo _
        , pOccurredAtStart, pOccurredAtEnd _
        , pFieldName, pFieldNameWildcardType _
        , pChangedByUser, pChangedByUserWildcardType _
        , pActiveLoginIDFrom, pActiveLoginIDTo _
        , vRequester, pHowMany, pDir) : If pFault.isOK = False Then Return pFault 
    
    Return pFault 
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
        , ByVal vOriginalIDFrom As Nullable(Of Long), ByVal vOriginalIDTo As Nullable(Of Long) _
        , ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRowIDFrom As Nullable(Of Long), ByVal vRowIDTo As Nullable(Of Long) _
        , ByVal vOccurredAtStart As Nullable(Of Date), ByVal vOccurredAtEnd As Nullable(Of Date) _
        , ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType _
        , ByVal vActiveLoginIDFrom As Nullable(Of Long), ByVal vActiveLoginIDTo As Nullable(Of Long) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OriginalIDFrom={2}, OriginalIDTo={3}, TableName={4}, TableNameWildcardType={5}, RowIDFrom={6}, RowIDTo={7}, OccurredAtStart={8}, OccurredAtEnd={9}, FieldName={10}, FieldNameWildcardType={11}, ChangedByUser={12}, ChangedByUserWildcardType={13}, ActiveLoginIDFrom={14}, ActiveLoginIDTo={15}", vIDFrom, vIDTo, vOriginalIDFrom, vOriginalIDTo, vTableName, vTableNameWildcardType.FastToString(), vRowIDFrom, vRowIDTo, vOccurredAtStart, vOccurredAtEnd, vFieldName, vFieldNameWildcardType.FastToString(), vChangedByUser, vChangedByUserWildcardType.FastToString(), vActiveLoginIDFrom, vActiveLoginIDTo)
    
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
          'OriginalID 
          pBinaryWriter.Write(vOriginalIDFrom.HasValue) 
          If vOriginalIDFrom.HasValue Then pBinaryWriter.Write(vOriginalIDFrom.Value) : pParametersToLog &= $"OriginalIDFrom={vOriginalIDFrom};"  
          pBinaryWriter.Write(vOriginalIDTo.HasValue) 
          If vOriginalIDTo.HasValue Then pBinaryWriter.Write(vOriginalIDTo.Value) : pParametersToLog &= $"OriginalIDTo={vOriginalIDTo};"  
          'TableName 
          If vTableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vTableName) : pBinaryWriter.Write(vTableNameWildcardType.FastToString()) : pParametersToLog &= $"TableName={vTableName};" : pParametersToLog &= $"TableNameWildcardType={vTableNameWildcardType};"  
          'RowID 
          pBinaryWriter.Write(vRowIDFrom.HasValue) 
          If vRowIDFrom.HasValue Then pBinaryWriter.Write(vRowIDFrom.Value) : pParametersToLog &= $"RowIDFrom={vRowIDFrom};"  
          pBinaryWriter.Write(vRowIDTo.HasValue) 
          If vRowIDTo.HasValue Then pBinaryWriter.Write(vRowIDTo.Value) : pParametersToLog &= $"RowIDTo={vRowIDTo};"  
          'OccurredAt 
          pBinaryWriter.Write(vOccurredAtStart.HasValue) 
          If vOccurredAtStart.HasValue Then pBinaryWriter.Write(vOccurredAtStart.Value.Ticks) : pParametersToLog &= $"OccurredAtStart={vOccurredAtStart.Value};"  
          pBinaryWriter.Write(vOccurredAtEnd.HasValue) 
          If vOccurredAtEnd.HasValue Then pBinaryWriter.Write(vOccurredAtEnd.Value.Ticks) : pParametersToLog &= $"OccurredAtEnd={vOccurredAtEnd.Value};"  
          'FieldName 
          If vFieldName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFieldName) : pBinaryWriter.Write(vFieldNameWildcardType.FastToString()) : pParametersToLog &= $"FieldName={vFieldName};" : pParametersToLog &= $"FieldNameWildcardType={vFieldNameWildcardType};"  
          'ChangedByUser 
          If vChangedByUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vChangedByUser) : pBinaryWriter.Write(vChangedByUserWildcardType.FastToString()) : pParametersToLog &= $"ChangedByUser={vChangedByUser};" : pParametersToLog &= $"ChangedByUserWildcardType={vChangedByUserWildcardType};"  
          'ActiveLoginID 
          pBinaryWriter.Write(vActiveLoginIDFrom.HasValue) 
          If vActiveLoginIDFrom.HasValue Then pBinaryWriter.Write(vActiveLoginIDFrom.Value) : pParametersToLog &= $"ActiveLoginIDFrom={vActiveLoginIDFrom};"  
          pBinaryWriter.Write(vActiveLoginIDTo.HasValue) 
          If vActiveLoginIDTo.HasValue Then pBinaryWriter.Write(vActiveLoginIDTo.Value) : pParametersToLog &= $"ActiveLoginIDTo={vActiveLoginIDTo};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByOriginalID
    GroupByTableName
    GroupByRowID
    GroupByOccurredAt
    GroupByFieldName
    GroupByChangedByUser
    GroupByActiveLoginID
  End Enum 
 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. Only send the fields you need. Default for GrouBy is False 
  ''' </summary> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillSumOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pOriginalIDFrom As Nullable(Of Long) = Nothing
    Dim pOriginalIDTo As Nullable(Of Long) = Nothing
    Dim pTableName As String = Nothing
    Dim pTableNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pRowIDFrom As Nullable(Of Long) = Nothing
    Dim pRowIDTo As Nullable(Of Long) = Nothing
    Dim pOccurredAtStart As Nullable(Of Date) = Nothing
    Dim pOccurredAtEnd As Nullable(Of Date) = Nothing
    Dim pFieldName As String = Nothing
    Dim pFieldNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pChangedByUser As String = Nothing
    Dim pChangedByUserWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pActiveLoginIDFrom As Nullable(Of Long) = Nothing
    Dim pActiveLoginIDTo As Nullable(Of Long) = Nothing
    Dim pGroupByOriginalID As Boolean = False
    Dim pGroupByTableName As Boolean = False
    Dim pGroupByRowID As Boolean = False
    Dim pGroupByOccurredAt As Boolean = False
    Dim pGroupByFieldName As Boolean = False
    Dim pGroupByChangedByUser As Boolean = False
    Dim pGroupByActiveLoginID As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginalIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginalIDFrom) : If pObj IsNot Nothing Then pOriginalIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginalIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginalIDTo) : If pObj IsNot Nothing Then pOriginalIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableName) Then pObj = vParameters(enmFillOnTheFlyParameters.TableName) : If pObj IsNot Nothing Then pTableName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.TableNameWildcardType) : If pObj IsNot Nothing Then pTableNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RowIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.RowIDFrom) : If pObj IsNot Nothing Then pRowIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RowIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.RowIDTo) : If pObj IsNot Nothing Then pRowIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OccurredAtStart) Then pObj = vParameters(enmFillOnTheFlyParameters.OccurredAtStart) : If pObj IsNot Nothing Then pOccurredAtStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OccurredAtEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.OccurredAtEnd) : If pObj IsNot Nothing Then pOccurredAtEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FieldName) Then pObj = vParameters(enmFillOnTheFlyParameters.FieldName) : If pObj IsNot Nothing Then pFieldName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FieldNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.FieldNameWildcardType) : If pObj IsNot Nothing Then pFieldNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ChangedByUser) Then pObj = vParameters(enmFillOnTheFlyParameters.ChangedByUser) : If pObj IsNot Nothing Then pChangedByUser = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ChangedByUserWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ChangedByUserWildcardType) : If pObj IsNot Nothing Then pChangedByUserWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ActiveLoginIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.ActiveLoginIDFrom) : If pObj IsNot Nothing Then pActiveLoginIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ActiveLoginIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.ActiveLoginIDTo) : If pObj IsNot Nothing Then pActiveLoginIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOriginalID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOriginalID) : If pObj IsNot Nothing Then pGroupByOriginalID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByTableName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByTableName) : If pObj IsNot Nothing Then pGroupByTableName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByRowID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByRowID) : If pObj IsNot Nothing Then pGroupByRowID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOccurredAt) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOccurredAt) : If pObj IsNot Nothing Then pGroupByOccurredAt = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByFieldName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByFieldName) : If pObj IsNot Nothing Then pGroupByFieldName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByChangedByUser) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByChangedByUser) : If pObj IsNot Nothing Then pGroupByChangedByUser = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByActiveLoginID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByActiveLoginID) : If pObj IsNot Nothing Then pGroupByActiveLoginID = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pOriginalIDFrom, pOriginalIDTo _
        , pTableName, pTableNameWildcardType _
        , pRowIDFrom, pRowIDTo _
        , pOccurredAtStart, pOccurredAtEnd _
        , pFieldName, pFieldNameWildcardType _
        , pChangedByUser, pChangedByUserWildcardType _
        , pActiveLoginIDFrom, pActiveLoginIDTo _
        , pGroupByOriginalID _
        , pGroupByTableName _
        , pGroupByRowID _
        , pGroupByOccurredAt _
        , pGroupByFieldName _
        , pGroupByChangedByUser _
        , pGroupByActiveLoginID _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a grouped collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillSumOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vOriginalIDFrom As Nullable(Of Long), ByVal vOriginalIDTo As Nullable(Of Long) _
        , ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRowIDFrom As Nullable(Of Long), ByVal vRowIDTo As Nullable(Of Long) _
        , ByVal vOccurredAtStart As Nullable(Of Date), ByVal vOccurredAtEnd As Nullable(Of Date) _
        , ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType _
        , ByVal vActiveLoginIDFrom As Nullable(Of Long), ByVal vActiveLoginIDTo As Nullable(Of Long) _
        , ByVal vGroupByOriginalID As Boolean _
        , ByVal vGroupByTableName As Boolean _
        , ByVal vGroupByRowID As Boolean _
        , ByVal vGroupByOccurredAt As Boolean _
        , ByVal vGroupByFieldName As Boolean _
        , ByVal vGroupByChangedByUser As Boolean _
        , ByVal vGroupByActiveLoginID As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OriginalIDFrom={2}, OriginalIDTo={3}, TableName={4}, TableNameWildcardType={5}, RowIDFrom={6}, RowIDTo={7}, OccurredAtStart={8}, OccurredAtEnd={9}, FieldName={10}, FieldNameWildcardType={11}, ChangedByUser={12}, ChangedByUserWildcardType={13}, ActiveLoginIDFrom={14}, ActiveLoginIDTo={15}, GroupByOriginalID={16}, GroupByTableName={17}, GroupByRowID={18}, GroupByOccurredAt={19}, GroupByFieldName={20}, GroupByChangedByUser={21}, GroupByActiveLoginID={22}", vIDFrom, vIDTo, vOriginalIDFrom, vOriginalIDTo, vTableName, vTableNameWildcardType.FastToString(), vRowIDFrom, vRowIDTo, vOccurredAtStart, vOccurredAtEnd, vFieldName, vFieldNameWildcardType.FastToString(), vChangedByUser, vChangedByUserWildcardType.FastToString(), vActiveLoginIDFrom, vActiveLoginIDTo, vGroupByOriginalID, vGroupByTableName, vGroupByRowID, vGroupByOccurredAt, vGroupByFieldName, vGroupByChangedByUser, vGroupByActiveLoginID)
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
          'OriginalID 
          pBinaryWriter.Write(vOriginalIDFrom.HasValue) 
          If vOriginalIDFrom.HasValue Then pBinaryWriter.Write(vOriginalIDFrom.Value) : pParametersToLog &= $"OriginalIDFrom={vOriginalIDFrom};"  
          pBinaryWriter.Write(vOriginalIDTo.HasValue) 
          If vOriginalIDTo.HasValue Then pBinaryWriter.Write(vOriginalIDTo.Value) : pParametersToLog &= $"OriginalIDTo={vOriginalIDTo};"  
          'TableName 
          If vTableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vTableName) : pBinaryWriter.Write(vTableNameWildcardType.FastToString()) 
          'RowID 
          pBinaryWriter.Write(vRowIDFrom.HasValue) 
          If vRowIDFrom.HasValue Then pBinaryWriter.Write(vRowIDFrom.Value) : pParametersToLog &= $"RowIDFrom={vRowIDFrom};"  
          pBinaryWriter.Write(vRowIDTo.HasValue) 
          If vRowIDTo.HasValue Then pBinaryWriter.Write(vRowIDTo.Value) : pParametersToLog &= $"RowIDTo={vRowIDTo};"  
          'OccurredAt 
          pBinaryWriter.Write(vOccurredAtStart.HasValue) 
          If vOccurredAtStart.HasValue Then pBinaryWriter.Write(vOccurredAtStart.Value.Ticks) : pParametersToLog &= $"OccurredAtStart={vOccurredAtStart};"  
          pBinaryWriter.Write(vOccurredAtEnd.HasValue) 
          If vOccurredAtEnd.HasValue Then pBinaryWriter.Write(vOccurredAtEnd.Value.Ticks) : pParametersToLog &= $"OccurredAtEnd={vOccurredAtEnd};"  
          'FieldName 
          If vFieldName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFieldName) : pBinaryWriter.Write(vFieldNameWildcardType.FastToString()) 
          'ChangedByUser 
          If vChangedByUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vChangedByUser) : pBinaryWriter.Write(vChangedByUserWildcardType.FastToString()) 
          'ActiveLoginID 
          pBinaryWriter.Write(vActiveLoginIDFrom.HasValue) 
          If vActiveLoginIDFrom.HasValue Then pBinaryWriter.Write(vActiveLoginIDFrom.Value) : pParametersToLog &= $"ActiveLoginIDFrom={vActiveLoginIDFrom};"  
          pBinaryWriter.Write(vActiveLoginIDTo.HasValue) 
          If vActiveLoginIDTo.HasValue Then pBinaryWriter.Write(vActiveLoginIDTo.Value) : pParametersToLog &= $"ActiveLoginIDTo={vActiveLoginIDTo};"  
          pBinaryWriter.Write(vGroupByOriginalID) : pParametersToLog &= $"GroupByOriginalID={vGroupByOriginalID};"  
          pBinaryWriter.Write(vGroupByTableName) : pParametersToLog &= $"GroupByTableName={vGroupByTableName};"  
          pBinaryWriter.Write(vGroupByRowID) : pParametersToLog &= $"GroupByRowID={vGroupByRowID};"  
          pBinaryWriter.Write(vGroupByOccurredAt) : pParametersToLog &= $"GroupByOccurredAt={vGroupByOccurredAt};"  
          pBinaryWriter.Write(vGroupByFieldName) : pParametersToLog &= $"GroupByFieldName={vGroupByFieldName};"  
          pBinaryWriter.Write(vGroupByChangedByUser) : pParametersToLog &= $"GroupByChangedByUser={vGroupByChangedByUser};"  
          pBinaryWriter.Write(vGroupByActiveLoginID) : pParametersToLog &= $"GroupByActiveLoginID={vGroupByActiveLoginID};"  
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csAuditIndexedColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the AuditIndexed  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-AuditIndexed-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vAuditIndexedArray As csAuditIndexed())
    Me.Clear()
    
    For Each pAuditIndexed As csAuditIndexed In vAuditIndexedArray
      Me.Add(pAuditIndexed)
      _Clean.Add(pAuditIndexed.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pAuditIndexed As New csAuditIndexed(pRow, vRequester) 
        Me.Add(pAuditIndexed) 
        _Clean.Add(pAuditIndexed.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-AuditIndexedCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-130515-1300", vRequester) 
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
      Dim pAuditIndexeds As csAuditIndexedCol = CType(pXmlSerializer.Deserialize(pStreamReader), csAuditIndexedCol) 
      For Each pAuditIndexed As csAuditIndexed In pAuditIndexeds 
        Me.Add(pAuditIndexed) 
        _Clean.Add(pAuditIndexed.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-AuditIndexed-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-190720-1443", vRequester) 
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
 
      Dim pAuditIndexeds As List(Of csAuditIndexed) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csAuditIndexed))(vJSON, pSettings) 
      For Each pAuditIndexed As csAuditIndexed In pAuditIndexeds 
        Me.Add(pAuditIndexed) 
        _Clean.Add(pAuditIndexed.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-190720-2059", vRequester) 
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
          For Each lAuditIndexed As csAuditIndexed In Me 
            Dim pByte As Byte() = lAuditIndexed.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-150307-2340", vRequester) 
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
            Dim pAuditIndexed As csAuditIndexed = New csAuditIndexed(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pAuditIndexed) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pAuditIndexed.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-AuditIndexed-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pAuditIndexed As csAuditIndexed In Me 
      With pAuditIndexed 
        pFault = pAuditIndexed.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csAuditIndexedCol) Then Return False 
    Dim pAuditIndexedColToTest As csAuditIndexedCol = CType(vEntitiesToTest, csAuditIndexedCol) 
    Return isEqual(pAuditIndexedColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vAuditIndexedsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vAuditIndexedsToTest As csAuditIndexedCol) As Boolean
    If Me.Count <> vAuditIndexedsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vAuditIndexedsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    If pFilledFromSumOnTheFly Then pAuditIndexeds._FilledFromSumOnTheFly = True
    
    For Each pAuditIndexed As csAuditIndexed In Me 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
      If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
    Next 
    Return pAuditIndexeds 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csAuditIndexedCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    If pFilledFromSumOnTheFly Then pAuditIndexeds._FilledFromSumOnTheFly = True
    
    For Each pAuditIndexed As csAuditIndexed In Me
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
      pAuditIndexeds.Add(pAuditIndexedClone)
      If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
    Next
    Return pAuditIndexeds
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.ID > vIDFrom AndAlso pAuditIndexed.ID <= vIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ActiveLoginID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedActiveLoginID(ByVal vActiveLoginIDFrom As Long, ByVal vActiveLoginIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.ActiveLoginID > vActiveLoginIDFrom AndAlso pAuditIndexed.ActiveLoginID <= vActiveLoginIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ChangedByUser (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedChangedByUser(ByVal vChangedByUserFrom As String, ByVal vChangedByUserTo As String) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.ChangedByUser > vChangedByUserFrom AndAlso pAuditIndexed.ChangedByUser <= vChangedByUserTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by FieldName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedFieldName(ByVal vFieldNameFrom As String, ByVal vFieldNameTo As String) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.FieldName > vFieldNameFrom AndAlso pAuditIndexed.FieldName <= vFieldNameTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OccurredAt (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOccurredAt(ByVal vOccurredAtStart As Date, ByVal vOccurredAtEnd As Date) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.OccurredAt > vOccurredAtStart AndAlso pAuditIndexed.OccurredAt <= vOccurredAtEnd) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OriginalID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOriginalID(ByVal vOriginalIDFrom As Long, ByVal vOriginalIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.OriginalID > vOriginalIDFrom AndAlso pAuditIndexed.OriginalID <= vOriginalIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by RowID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedRowID(ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.RowID > vRowIDFrom AndAlso pAuditIndexed.RowID <= vRowIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TableName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTableName(ByVal vTableNameFrom As String, ByVal vTableNameTo As String) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.TableName > vTableNameFrom AndAlso pAuditIndexed.TableName <= vTableNameTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TableName and RowID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTableNameAndRowID(ByVal vTableNameFrom As String, ByVal vTableNameTo As String, ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.TableName > vTableNameFrom AndAlso pAuditIndexed.TableName <= vTableNameTo) AndAlso (pAuditIndexed.RowID > vRowIDFrom AndAlso pAuditIndexed.RowID <= vRowIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardChangedByUser(ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vChangedByUserWildcardType = clsEnums.enmWildCardType.After Then 
        If pAuditIndexed.ChangedByUser.StartsWith(vChangedByUser, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAuditIndexed.ChangedByUser.EndsWith(vChangedByUser, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAuditIndexed.ChangedByUser.IndexOf(vChangedByUser, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vChangedByUser.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAuditIndexed.ChangedByUser.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardFieldName(ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vFieldNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pAuditIndexed.FieldName.StartsWith(vFieldName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAuditIndexed.FieldName.EndsWith(vFieldName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAuditIndexed.FieldName.IndexOf(vFieldName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vFieldName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAuditIndexed.FieldName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardTableName(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pAuditIndexed.TableName.StartsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAuditIndexed.TableName.EndsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAuditIndexed.TableName.IndexOf(vTableName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vTableName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAuditIndexed.TableName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardTableNameAndRowID(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType, ByVal vRowID As Long, ByVal vRowIDWildcardType As clsEnums.enmWildCardType) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pAuditIndexed.TableName.StartsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAuditIndexed.TableName.EndsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAuditIndexed.TableName.IndexOf(vTableName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vTableName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAuditIndexed.TableName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
    Next 
    Return pAuditIndexeds 
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
  Public Function FindByID(ByVal vID As Long) As csAuditIndexed
    If Me.Count = 0 Then Return New csAuditIndexed 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
    
    Dim pAuditIndexed As csAuditIndexed = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pAuditIndexed) 
    If pAuditIndexed IsNot Nothing Then Return pAuditIndexed Else Return New csAuditIndexed() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OriginalID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOriginalID(ByVal vOriginalID As Long) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.OriginalID = vOriginalID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOriginalID with vOriginalID of {vOriginalID}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.OriginalID = vOriginalID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TableName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTableName(ByVal vTableName As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTableName = vTableName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.TableName.ToLowerInvariant() = vTableName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTableName with vTableName of {vTableName}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.TableName.ToLowerInvariant() = vTableName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RowID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRowID(ByVal vRowID As Long) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.RowID = vRowID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRowID with vRowID of {vRowID}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.RowID = vRowID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Operation
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOperation(ByVal vOperation As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vOperation = vOperation.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.Operation.ToLowerInvariant() = vOperation Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOperation with vOperation of {vOperation}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.Operation.ToLowerInvariant() = vOperation Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OccurredAt
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOccurredAt(ByVal vOccurredAt As Date) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.OccurredAt = vOccurredAt Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOccurredAt with vOccurredAt of {vOccurredAt}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.OccurredAt = vOccurredAt Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlCurrentUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlCurrentUser(ByVal vSqlCurrentUser As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlCurrentUser = vSqlCurrentUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.SqlCurrentUser.ToLowerInvariant() = vSqlCurrentUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlCurrentUser with vSqlCurrentUser of {vSqlCurrentUser}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.SqlCurrentUser.ToLowerInvariant() = vSqlCurrentUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FieldName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFieldName(ByVal vFieldName As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFieldName = vFieldName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.FieldName.ToLowerInvariant() = vFieldName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFieldName with vFieldName of {vFieldName}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.FieldName.ToLowerInvariant() = vFieldName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OldValue
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOldValue(ByVal vOldValue As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vOldValue = vOldValue.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.OldValue.ToLowerInvariant() = vOldValue Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOldValue with vOldValue of {vOldValue}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.OldValue.ToLowerInvariant() = vOldValue Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined NewValue
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNewValue(ByVal vNewValue As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNewValue = vNewValue.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.NewValue.ToLowerInvariant() = vNewValue Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNewValue with vNewValue of {vNewValue}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.NewValue.ToLowerInvariant() = vNewValue Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ChangedByUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByChangedByUser(ByVal vChangedByUser As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vChangedByUser = vChangedByUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.ChangedByUser.ToLowerInvariant() = vChangedByUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByChangedByUser with vChangedByUser of {vChangedByUser}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.ChangedByUser.ToLowerInvariant() = vChangedByUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ActiveLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByActiveLoginID(ByVal vActiveLoginID As Long) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.ActiveLoginID = vActiveLoginID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByActiveLoginID with vActiveLoginID of {vActiveLoginID}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.ActiveLoginID = vActiveLoginID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlSystemUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlSystemUser(ByVal vSqlSystemUser As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlSystemUser = vSqlSystemUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.SqlSystemUser.ToLowerInvariant() = vSqlSystemUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlSystemUser with vSqlSystemUser of {vSqlSystemUser}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.SqlSystemUser.ToLowerInvariant() = vSqlSystemUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlAppName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlAppName(ByVal vSqlAppName As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlAppName = vSqlAppName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.SqlAppName.ToLowerInvariant() = vSqlAppName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlAppName with vSqlAppName of {vSqlAppName}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.SqlAppName.ToLowerInvariant() = vSqlAppName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlHostName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlHostName(ByVal vSqlHostName As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlHostName = vSqlHostName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.SqlHostName.ToLowerInvariant() = vSqlHostName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlHostName with vSqlHostName of {vSqlHostName}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.SqlHostName.ToLowerInvariant() = vSqlHostName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.Tag.ToLowerInvariant() = vTag Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.Tag.ToLowerInvariant() = vTag Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TableNameAndRowID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTableNameAndRowID(ByVal vTableName As String, ByVal vRowID As Long) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList()
        If pAuditIndexed.TableName = vTableName AndAlso pAuditIndexed.RowID = vRowID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.TableName = vTableName AndAlso pAuditIndexed.RowID = vRowID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    Return pAuditIndexeds
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
    For Each pAuditIndexed As csAuditIndexed In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pAuditIndexed.LoadDataRow(pRow, vRequester) 
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
    Me.Sort(New csAuditIndexedCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
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
  
  Public Sub SortByOriginalID()
    Me.Sort(New csAuditIndexedCol.CompareByOriginalID)
  End Sub
  Private Class CompareByOriginalID
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OriginalID < y.OriginalID Then
        Return -1
      ElseIf x.OriginalID = y.OriginalID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTableName()
    Me.Sort(New csAuditIndexedCol.CompareByTableName)
  End Sub
  Private Class CompareByTableName
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TableName, y.TableName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRowID()
    Me.Sort(New csAuditIndexedCol.CompareByRowID)
  End Sub
  Private Class CompareByRowID
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.RowID < y.RowID Then
        Return -1
      ElseIf x.RowID = y.RowID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByOperation()
    Me.Sort(New csAuditIndexedCol.CompareByOperation)
  End Sub
  Private Class CompareByOperation
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Operation, y.Operation, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOccurredAt()
    Me.Sort(New csAuditIndexedCol.CompareByOccurredAt)
  End Sub
  Private Class CompareByOccurredAt
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OccurredAt < y.OccurredAt Then
        Return -1
      ElseIf x.OccurredAt = y.OccurredAt Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySqlCurrentUser()
    Me.Sort(New csAuditIndexedCol.CompareBySqlCurrentUser)
  End Sub
  Private Class CompareBySqlCurrentUser
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlCurrentUser, y.SqlCurrentUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFieldName()
    Me.Sort(New csAuditIndexedCol.CompareByFieldName)
  End Sub
  Private Class CompareByFieldName
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FieldName, y.FieldName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOldValue()
    Me.Sort(New csAuditIndexedCol.CompareByOldValue)
  End Sub
  Private Class CompareByOldValue
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OldValue, y.OldValue, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNewValue()
    Me.Sort(New csAuditIndexedCol.CompareByNewValue)
  End Sub
  Private Class CompareByNewValue
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.NewValue, y.NewValue, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByChangedByUser()
    Me.Sort(New csAuditIndexedCol.CompareByChangedByUser)
  End Sub
  Private Class CompareByChangedByUser
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ChangedByUser, y.ChangedByUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByActiveLoginID()
    Me.Sort(New csAuditIndexedCol.CompareByActiveLoginID)
  End Sub
  Private Class CompareByActiveLoginID
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ActiveLoginID < y.ActiveLoginID Then
        Return -1
      ElseIf x.ActiveLoginID = y.ActiveLoginID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySqlSystemUser()
    Me.Sort(New csAuditIndexedCol.CompareBySqlSystemUser)
  End Sub
  Private Class CompareBySqlSystemUser
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlSystemUser, y.SqlSystemUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySqlAppName()
    Me.Sort(New csAuditIndexedCol.CompareBySqlAppName)
  End Sub
  Private Class CompareBySqlAppName
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlAppName, y.SqlAppName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySqlHostName()
    Me.Sort(New csAuditIndexedCol.CompareBySqlHostName)
  End Sub
  Private Class CompareBySqlHostName
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlHostName, y.SqlHostName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csAuditIndexedCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
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
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csAuditIndexed) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csAuditIndexed) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
