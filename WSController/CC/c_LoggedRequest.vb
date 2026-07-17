Public Class csLoggedRequest
  Inherits cTargCCEntity 
 
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return True 
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
  
  'Parent Properties 
  Public Enum enmParentProperty 
    UD 
    [LoggedLogin] 
    [User] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [LoggedLogin] 
    [TimeAccessed] 
    [User] 
    [CallingFunctionWithinApplication] 
    [EntryPoint] 
    [Process] 
    [Thread] 
    [Tag] 
  End Enum 
  Private _WithParents As clsEnums.enmLoadParent
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
    End Get
  End Property
  
  Private _ID As Long
  Private _LoggedLoginID As Long
  Private _LoggedLogin As csLoggedLogin
  Private _LoggedLoginText As String
  Private _TimeAccessed As Date
  Private _UserID As Long
  Private _User As csUser
  Private _UserText As String
  Private _CallingFunctionWithinApplication As String
  Private _EntryPoint As String
  Private _Process As String
  Private _Thread As String
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
  Public Property [LoggedLoginID]() As Long
    Get
      Return Me._LoggedLoginID
    End Get
    Set(ByVal value As Long)
      If Me._LoggedLoginID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LoggedLoginID = value 
      End If 
    End Set
  End Property
  Public Property [LoggedLogin]() As csLoggedLogin
    Get
      Return Me._LoggedLogin
    End Get
    Set(ByVal value As csLoggedLogin)
      Me._LoggedLogin = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the LoggedLogin object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property LoggedLoginText() As String
    Get
      Return Me._LoggedLoginText
    End Get
    Set(ByVal value As String)
      Me._LoggedLoginText = value
    End Set
  End Property
  Public Property [TimeAccessed]() As Date
    Get
      Return Me._TimeAccessed
    End Get
    Set(ByVal value As Date)
      If Me._TimeAccessed <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TimeAccessed = value 
      End If 
    End Set
  End Property
  Public Property [UserID]() As Long
    Get
      Return Me._UserID
    End Get
    Set(ByVal value As Long)
      If Me._UserID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UserID = value 
      End If 
    End Set
  End Property
  Public Property [User]() As csUser
    Get
      Return Me._User
    End Get
    Set(ByVal value As csUser)
      Me._User = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the User object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property UserText() As String
    Get
      Return Me._UserText
    End Get
    Set(ByVal value As String)
      Me._UserText = value
    End Set
  End Property
  Public Property [CallingFunctionWithinApplication]() As String
    Get
      Return Me._CallingFunctionWithinApplication
    End Get
    Set(ByVal value As String)
      If Me._CallingFunctionWithinApplication <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CallingFunctionWithinApplication = value 
      End If 
    End Set
  End Property
  Public Property [EntryPoint]() As String
    Get
      Return Me._EntryPoint
    End Get
    Set(ByVal value As String)
      If Me._EntryPoint <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EntryPoint = value 
      End If 
    End Set
  End Property
  Public Property [Process]() As String
    Get
      Return Me._Process
    End Get
    Set(ByVal value As String)
      If Me._Process <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Process = value 
      End If 
    End Set
  End Property
  Public Property [Thread]() As String
    Get
      Return Me._Thread
    End Get
    Set(ByVal value As String)
      If Me._Thread <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Thread = value 
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
    If _LoggedLoginID <> 0 Then pValue.Append("LoggedLoginID='" & _LoggedLoginID.ToString() & "' ‡ ") 
    If _LoggedLoginText <> "" Then pValue.Append("LoggedLoginText='" & _LoggedLoginText & "' ‡ ") 
    If Not (_TimeAccessed = Nothing) Then pValue.Append("TimeAccessed='" & _TimeAccessed.ToString("o") & "' ‡ ") 
    If _UserID <> 0 Then pValue.Append("UserID='" & _UserID.ToString() & "' ‡ ") 
    If _UserText <> "" Then pValue.Append("UserText='" & _UserText & "' ‡ ") 
    If _CallingFunctionWithinApplication <> "" Then pValue.Append("CallingFunctionWithinApplication='" & _CallingFunctionWithinApplication & "' ‡ ") 
    If _EntryPoint <> "" Then pValue.Append("EntryPoint='" & _EntryPoint & "' ‡ ") 
    If _Process <> "" Then pValue.Append("Process='" & _Process & "' ‡ ") 
    If _Thread <> "" Then pValue.Append("Thread='" & _Thread & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _LoggedLoginID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_LoggedLoginText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TimeAccessed.ToShortDateString & " " & _TimeAccessed.ToShortTimeString)}""") 
    pCSV.Append("," & _UserID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_UserText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CallingFunctionWithinApplication)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EntryPoint)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Process)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Thread)}""") 
    If Not vWithTexts Then 
        pCSV.Append($",""{ccHelper.StringForCSV(_Tag)}""") 
    End If 
    'pCSV.Append($",""{bDateAdded:yyyyMMddTHH:mm:ss.ffff}"" ") 
    
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty()
    _WithParents = clsEnums.enmLoadParent.DoNotLoad 
  End Sub
  
  Public Sub New(ByVal vWithParents As clsEnums.enmLoadParent) 
    MyBase.New()
    CreateEmpty()
    _WithParents = vWithParents 
  End Sub
  
  Public Sub New(ByVal vPrimaryKeyValue As Long, ByVal vWithParents As clsEnums.enmLoadParent, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional vMustExist As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    _WithParents = vWithParents 
    
    rFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
  End Sub
  
  Public Sub New(ByVal vcsLoggedRequest As csLoggedRequest)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsLoggedRequest) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vLoggedLoginID As Long = 0 _ 
    , Optional vLoggedLoginText As String = "" _ 
    , Optional vTimeAccessed As Date = Nothing _ 
    , Optional vUserID As Long = 0 _ 
    , Optional vUserText As String = "" _ 
    , Optional vCallingFunctionWithinApplication As String = "" _ 
    , Optional vEntryPoint As String = "" _ 
    , Optional vProcess As String = "" _ 
    , Optional vThread As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _LoggedLoginID = vLoggedLoginID 
    _LoggedLoginText = vLoggedLoginText 
    _TimeAccessed = vTimeAccessed 
    _UserID = vUserID 
    _UserText = vUserText 
    _CallingFunctionWithinApplication = vCallingFunctionWithinApplication 
    _EntryPoint = vEntryPoint 
    _Process = vProcess 
    _Thread = vThread 
    _Tag = vTag 
    bDateAdded = vDateAdded 
    bccStatus = clsEnums.enmObjectStatus.Dirty 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  Friend Sub New(ByVal vRow As DataRow, ByVal vRequester As clsRequester, Optional ByVal vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad) 
    MyBase.New()
    CreateEmpty()
    Dim pFault As New clsFault 
 
    pFault = LoadDataRow(vRow, vRequester) 
    If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox) 
 
    _WithParents = vWithParents 
 
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
    _WithParents = vWithParents 
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
 
    _CallingFunctionWithinApplication = _CallingFunctionWithinApplication.Truncate(pTruncateLength, _IsTruncated) 
    _EntryPoint = _EntryPoint.Truncate(pTruncateLength, _IsTruncated) 
    _Process = _Process.Truncate(pTruncateLength, _IsTruncated) 
    _Thread = _Thread.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the LoggedRequest by PrimaryKey (ID) 
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
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedRequest-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the LoggedRequest by the chosen parameters. This function may be a bit slower than accessing the LoggedRequest's GetBy... directly 
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
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-LoggedRequest-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedRequest-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the LoggedRequest by ID. 
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
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csLoggedRequestGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the LoggedRequest 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150308-1015", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is csLoggedRequest) Then Return False 
    Dim pLoggedRequestToTest As csLoggedRequest = CType(vTargCCEntityToTest, csLoggedRequest) 
    Return isEqual(pLoggedRequestToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vLoggedRequestToTest As csLoggedRequest) As Boolean
    With vLoggedRequestToTest
      If _ID <> .ID Then Return False
      If _LoggedLoginID <> .LoggedLoginID Then Return False
      If _TimeAccessed <> Nothing AndAlso .TimeAccessed <> Nothing Then 
        If ccHelper.ToLong(_TimeAccessed.Subtract(.TimeAccessed).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_TimeAccessed = Nothing AndAlso .TimeAccessed = Nothing) Then 
        Return False 
      End If 
      If _UserID <> .UserID Then Return False
      If _CallingFunctionWithinApplication <> .CallingFunctionWithinApplication Then Return False
      If _EntryPoint <> .EntryPoint Then Return False
      If _Process <> .Process Then Return False
      If _Thread <> .Thread Then Return False
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
    Dim pClone As New csLoggedRequest(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLoggedRequest
    Dim pClone As New csLoggedRequest(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-LoggedRequest-130316-0852", vRequester) : End Try 
    Try : vDataRow("LoggedLoginID") = _LoggedLoginID : Catch ex As Exception : Return pFault.LogException(ex, "LoggedLoginID", "TRGT-LoggedRequest-130316-0852", vRequester) : End Try 
    Try : vDataRow("TimeAccessed") = _TimeAccessed : Catch ex As Exception : Return pFault.LogException(ex, "TimeAccessed", "TRGT-LoggedRequest-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserID") = _UserID : Catch ex As Exception : Return pFault.LogException(ex, "UserID", "TRGT-LoggedRequest-130316-0852", vRequester) : End Try 
    Try : vDataRow("CallingFunctionWithinApplication") = _CallingFunctionWithinApplication : Catch ex As Exception : Return pFault.LogException(ex, "CallingFunctionWithinApplication", "TRGT-LoggedRequest-130316-0852", vRequester) : End Try 
    Try : vDataRow("EntryPoint") = _EntryPoint : Catch ex As Exception : Return pFault.LogException(ex, "EntryPoint", "TRGT-LoggedRequest-130316-0852", vRequester) : End Try 
    Try : vDataRow("Process") = _Process : Catch ex As Exception : Return pFault.LogException(ex, "Process", "TRGT-LoggedRequest-130316-0852", vRequester) : End Try 
    Try : vDataRow("Thread") = _Thread : Catch ex As Exception : Return pFault.LogException(ex, "Thread", "TRGT-LoggedRequest-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedRequest-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pLoggedRequest As csLoggedRequest = CType(pXmlSerializer.Deserialize(pStreamReader), csLoggedRequest) 
      AssignValues(pLoggedRequest) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-LoggedRequest-130515-1230", vRequester) 
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
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(bccStatus.FastToString()) 
          'ID 
          pBinaryWriter.Write(_ID) 
          'LoggedLoginID 
          pBinaryWriter.Write(_LoggedLoginID) 
          'LoggedLogin 
          If _LoggedLogin IsNot Nothing Then 
            pObjectBytes = _LoggedLogin.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _LoggedLoginText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LoggedLoginText) 
          'TimeAccessed 
          pBinaryWriter.Write(_TimeAccessed.Ticks) 
          'UserID 
          pBinaryWriter.Write(_UserID) 
          'User 
          If _User IsNot Nothing Then 
            pObjectBytes = _User.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _UserText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_UserText) 
          'CallingFunctionWithinApplication 
          If _CallingFunctionWithinApplication Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CallingFunctionWithinApplication) 
          'EntryPoint 
          If _EntryPoint Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_EntryPoint) 
          'Process 
          If _Process Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Process) 
          'Thread 
          If _Thread Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Thread) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-LoggedRequest-150307-2338", vRequester) 
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
          _WithParents = clsEnums.TranslateEnmLoadParent(pReader.ReadString) 
          bccStatus = clsEnums.TranslateEnmObjectStatus(pReader.ReadString) 
          'ID 
          _ID = pReader.ReadInt64 
          'LoggedLoginID 
          _LoggedLoginID = pReader.ReadInt64 
          'LoggedLogin 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedLogin = New csLoggedLogin(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LoggedLoginText = pReader.ReadString 
          'TimeAccessed 
          _TimeAccessed = New Date(pReader.ReadInt64) 
          'UserID 
          _UserID = pReader.ReadInt64 
          'User 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _User = New csUser(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _UserText = pReader.ReadString 
          'CallingFunctionWithinApplication 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CallingFunctionWithinApplication = pReader.ReadString 
          'EntryPoint 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _EntryPoint = pReader.ReadString 
          'Process 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Process = pReader.ReadString 
          'Thread 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Thread = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-LoggedRequest-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedRequest-190720-1443", vRequester) 
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
 
      Dim pLoggedRequest As csLoggedRequest = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csLoggedRequest)(vJSON, pSettings) 
      AssignValues(pLoggedRequest) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedRequest-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vLoggedRequest As csLoggedRequest)
    With vLoggedRequest
      _ID = .ID 
      _LoggedLoginID = .LoggedLoginID 
      If .LoggedLogin IsNot Nothing Then 
        _LoggedLogin = .LoggedLogin.Clone() 
      End If 
      _LoggedLoginText = .LoggedLoginText 
      _TimeAccessed = .TimeAccessed 
      _UserID = .UserID 
      If .User IsNot Nothing Then 
        _User = .User.Clone() 
      End If 
      _UserText = .UserText 
      _CallingFunctionWithinApplication = .CallingFunctionWithinApplication 
      _EntryPoint = .EntryPoint 
      _Process = .Process 
      _Thread = .Thread 
      _Tag = .Tag 
      _WithParents = .WithParents 
      _WithParents = .WithParents 
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
 
  ''' <summary>
  ''' This loads the dependant Parent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault 
    
    If _ID = 0 Then 
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
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csLoggedRequestLoadParents" 
      Dim pParametersToLog = $"ID: {_ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequest 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150411-1107", vRequester) 
    End Try 
    
    pFault.SetOK() 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  Private Sub CreateEmpty()
    
    _ID = 0 
    _LoggedLoginID = 0
    _LoggedLogin = Nothing
    _LoggedLoginText = "."
    _TimeAccessed = Nothing
    _UserID = 0
    _User = Nothing
    _UserText = "."
    _CallingFunctionWithinApplication = ""
    _EntryPoint = ""
    _Process = ""
    _Thread = ""
    _Tag = ""
    bccStatus = clsEnums.enmObjectStatus.New 
    bPrimaryKey = _ID 
    bDateAdded = Nothing 
    bDefaultDesignation = "" 
     
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      _WithParents = clsEnums.enmLoadParent.UD 
      bHasParents = True 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class csLoggedRequestCol
  Inherits cTargCCCollection(Of csLoggedRequest)
  
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return True 
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csLoggedRequest) 
  Private _RecreateDictionaryForFindByID As Boolean 
   
  Private _WithParents As clsEnums.enmLoadParent
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
    End Get
  End Property
  
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
 
    For Each pRow As csLoggedRequest In Me 
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
    pCSVTitle.Append(",""LoggedLoginID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""LoggedLogin (Text)""") 
    pCSVTitle.Append(",""TimeAccessed""") 
    pCSVTitle.Append(",""UserID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""User (Text)""") 
    pCSVTitle.Append(",""CallingFunctionWithinApplication""") 
    pCSVTitle.Append(",""EntryPoint""") 
    pCSVTitle.Append(",""Process""") 
    pCSVTitle.Append(",""Thread""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csLoggedRequest In Me 
      pCSV.AppendLine(pRow.ToCSV(vWithTexts)) 
    Next 
 
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty() 
  End Sub
  
  Public Sub New(ByVal vWithParents As clsEnums.enmLoadParent) 
    MyBase.New()
    CreateEmpty() 
    _WithParents = vWithParents 
  End Sub
  
  Public Sub New(ByVal vWithParents As clsEnums.enmLoadParent, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) 
    MyBase.New()
    CreateEmpty() 
    _WithParents = vWithParents 
    
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
 
  Public Overloads Sub Add(ByVal vLoggedRequest As csLoggedRequest) 
    SyncLock _CollectionLock 
      MyBase.Add(vLoggedRequest) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vLoggedRequest As csLoggedRequest) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vLoggedRequest) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vLoggedRequestCol As csLoggedRequestCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vLoggedRequestCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vLoggedRequest As csLoggedRequest) 
    SyncLock _CollectionLock 
      MyBase.Remove(vLoggedRequest) 
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
      Dim pTempDictionary As New Dictionary(Of Long, csLoggedRequest) 
      
      For Each lLoggedRequest In Me 
        If lLoggedRequest.IsEmpty OrElse pTempDictionary.ContainsKey(lLoggedRequest.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lLoggedRequest.ID, lLoggedRequest) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lLoggedRequest.ToString, "TRGT-LoggedRequest-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", LoggedRequest:" & lLoggedRequest.ToString() & ", TRGT-LoggedRequest-260111-154657") 'Send it up the line 
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
    _WithParents = vWithParents 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    Throw New Exception("Entity is not localizable") 
  End Sub 
 
  ''' <summary>  
  ''' Use this before loading a DataGridView. You don't need more than pTruncateLength characters to see what you want.  
  ''' </summary>  
  ''' <param name="pTruncateLength"></param>  
  Public Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
 
    For Each lLoggedRequest As csLoggedRequest In Me 
      lLoggedRequest.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [LoggedLoginID] 
    [UserID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the LoggedRequests by the chosen parameters. This function may be a bit slower than accessing the LoggedRequest's FillBy... directly 
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
        Case enmFillByParameterCombination.LoggedLoginID 
          pFault = FillByLoggedLoginID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.UserID 
          pFault = FillByUserID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-LoggedRequest-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedRequest-151223_1716", vRequester) 
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
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedRequestColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequest 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LoggedLoginID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLoggedLoginID(ByVal vLoggedLoginID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LoggedLoginID={0}", vLoggedLoginID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLoggedLoginID 
          pBinaryWriter.Write(vLoggedLoginID) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedRequestColFillByLoggedLoginID" 
      Dim pParametersToLog = $"LoggedLoginID: {vLoggedLoginID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequest  
      If vAppend = True Then 
        Dim pLoggedRequests As New csLoggedRequestCol 
        pLoggedRequests.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedRequests) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByUserID(ByVal vUserID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}", vUserID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserID 
          pBinaryWriter.Write(vUserID) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedRequestColFillByUserID" 
      Dim pParametersToLog = $"UserID: {vUserID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequest  
      If vAppend = True Then 
        Dim pLoggedRequests As New csLoggedRequestCol 
        pLoggedRequests.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedRequests) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150407-2142", vRequester) 
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
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedRequestColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequest  
      If vAppend = True Then 
        Dim pLoggedRequests As New csLoggedRequestCol 
        pLoggedRequests.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedRequests) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150407-2142", vRequester) 
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
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedRequestColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequest   
      If vAppend = True Then 
        Dim pLoggedRequests As New csLoggedRequestCol 
        pLoggedRequests.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedRequests) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-231207-1750", vRequester) 
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
    [LoggedLoginID]
    [UserID]
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
    Dim pLoggedLoginID As Nullable(Of Long) = Nothing
    Dim pUserID As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoggedLoginID) Then pObj = vParameters(enmFillOnTheFlyParameters.LoggedLoginID) : If pObj IsNot Nothing Then pLoggedLoginID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserID) Then pObj = vParameters(enmFillOnTheFlyParameters.UserID) : If pObj IsNot Nothing Then pUserID = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pLoggedLoginID _
        , pUserID _
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
        , ByVal vLoggedLoginID As Nullable(Of Long) _
        , ByVal vUserID As Nullable(Of Long) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, LoggedLoginID={2}, UserID={3}", vIDFrom, vIDTo, vLoggedLoginID, vUserID)
    
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
          'LoggedLoginID 
          pBinaryWriter.Write(vLoggedLoginID.HasValue) 
          If vLoggedLoginID.HasValue = True Then pBinaryWriter.Write(vLoggedLoginID.Value) : pParametersToLog &= $"LoggedLoginID={vLoggedLoginID};"  
          'UserID 
          pBinaryWriter.Write(vUserID.HasValue) 
          If vUserID.HasValue = True Then pBinaryWriter.Write(vUserID.Value) : pParametersToLog &= $"UserID={vUserID};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedRequestColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequest  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByLoggedLoginID
    GroupByUserID
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
    Dim pLoggedLoginID As Nullable(Of Long) = Nothing
    Dim pUserID As Nullable(Of Long) = Nothing
    Dim pGroupByLoggedLoginID As Boolean = False
    Dim pGroupByUserID As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoggedLoginID) Then pObj = vParameters(enmFillOnTheFlyParameters.LoggedLoginID) : If pObj IsNot Nothing Then pLoggedLoginID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserID) Then pObj = vParameters(enmFillOnTheFlyParameters.UserID) : If pObj IsNot Nothing Then pUserID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLoggedLoginID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLoggedLoginID) : If pObj IsNot Nothing Then pGroupByLoggedLoginID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByUserID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByUserID) : If pObj IsNot Nothing Then pGroupByUserID = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pLoggedLoginID _
        , pUserID _
        , pGroupByLoggedLoginID _
        , pGroupByUserID _
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
        , ByVal vLoggedLoginID As Nullable(Of Long) _
        , ByVal vUserID As Nullable(Of Long) _
        , ByVal vGroupByLoggedLoginID As Boolean _
        , ByVal vGroupByUserID As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, LoggedLoginID={2}, UserID={3}, GroupByLoggedLoginID={4}, GroupByUserID={5}", vIDFrom, vIDTo, vLoggedLoginID, vUserID, vGroupByLoggedLoginID, vGroupByUserID)
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
          'LoggedLoginID 
          pBinaryWriter.Write(vLoggedLoginID.HasValue) 
          If vLoggedLoginID.HasValue = True Then pBinaryWriter.Write(vLoggedLoginID.Value) : pParametersToLog &= $"LoggedLoginID={vLoggedLoginID};"  
          'UserID 
          pBinaryWriter.Write(vUserID.HasValue) 
          If vUserID.HasValue = True Then pBinaryWriter.Write(vUserID.Value) : pParametersToLog &= $"UserID={vUserID};"  
          pBinaryWriter.Write(vGroupByLoggedLoginID) : pParametersToLog &= $"GroupByLoggedLoginID={vGroupByLoggedLoginID};"  
          pBinaryWriter.Write(vGroupByUserID) : pParametersToLog &= $"GroupByUserID={vGroupByUserID};"  
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedRequestColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequest  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vLoggedRequestArray As csLoggedRequest())
    Me.Clear()
    
    For Each pLoggedRequest As csLoggedRequest In vLoggedRequestArray
      Me.Add(pLoggedRequest)
      _Clean.Add(pLoggedRequest.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pLoggedRequest As New csLoggedRequest(pRow, vRequester, _WithParents) 
        Me.Add(pLoggedRequest) 
        _Clean.Add(pLoggedRequest.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-LoggedRequestCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedRequest-130515-1300", vRequester) 
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
      Dim pLoggedRequests As csLoggedRequestCol = CType(pXmlSerializer.Deserialize(pStreamReader), csLoggedRequestCol) 
      For Each pLoggedRequest As csLoggedRequest In pLoggedRequests 
        Me.Add(pLoggedRequest) 
        _Clean.Add(pLoggedRequest.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-LoggedRequest-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedRequest-190720-1443", vRequester) 
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
 
      Dim pLoggedRequests As List(Of csLoggedRequest) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csLoggedRequest))(vJSON, pSettings) 
      For Each pLoggedRequest As csLoggedRequest In pLoggedRequests 
        Me.Add(pLoggedRequest) 
        _Clean.Add(pLoggedRequest.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedRequest-190720-2059", vRequester) 
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
          'WithParents 
          pBinaryWriter.Write(_WithParents.ToString()) 
          'Items 
          pBinaryWriter.Write(Me.Count) 
          For Each lLoggedRequest As csLoggedRequest In Me 
            Dim pByte As Byte() = lLoggedRequest.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-LoggedRequest-150307-2340", vRequester) 
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
          'WithParents 
          _WithParents = clsEnums.TranslateEnmLoadParent(pReader.ReadString) 
          'Items 
          Dim pCount As Integer = pReader.ReadInt32 
          For iCntr As Integer = 0 To pCount - 1 
            Dim pLength As Integer = pReader.ReadInt32 
            Dim pLoggedRequest As csLoggedRequest = New csLoggedRequest(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pLoggedRequest) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pLoggedRequest.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-LoggedRequest-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pLoggedRequest As csLoggedRequest In Me 
      With pLoggedRequest 
        pFault = pLoggedRequest.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csLoggedRequestCol) Then Return False 
    Dim pLoggedRequestColToTest As csLoggedRequestCol = CType(vEntitiesToTest, csLoggedRequestCol) 
    Return isEqual(pLoggedRequestColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vLoggedRequestsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vLoggedRequestsToTest As csLoggedRequestCol) As Boolean
    If Me.Count <> vLoggedRequestsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vLoggedRequestsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pLoggedRequests._FilledFromSumOnTheFly = True
    
    For Each pLoggedRequest As csLoggedRequest In Me 
      Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone() 
      pLoggedRequests.Add(pLoggedRequestClone) 
      If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
    Next 
    Return pLoggedRequests 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLoggedRequestCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pLoggedRequests._FilledFromSumOnTheFly = True
    
    For Each pLoggedRequest As csLoggedRequest In Me
      Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
      pLoggedRequests.Add(pLoggedRequestClone)
      If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
    Next
    Return pLoggedRequests
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csLoggedRequestCol 
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents)  
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedRequest As csLoggedRequest In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedRequest.ID > vIDFrom AndAlso pLoggedRequest.ID <= vIDTo) Then 
        Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone() 
        pLoggedRequests.Add(pLoggedRequestClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
      End If 
    Next 
    Return pLoggedRequests 
  End Function 
  
  ''' <summary>
  ''' This loads the dependant parents for each of the rows and the 1 to 1 children
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    _WithParents = clsEnums.enmLoadParent.EntireObject 
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csLoggedRequestColLoadParents" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedRequestCol 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedRequest-150314-1803", vRequester) 
    End Try 
 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
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
  Public Function FindByID(ByVal vID As Long) As csLoggedRequest
    If Me.Count = 0 Then Return New csLoggedRequest 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
    
    Dim pLoggedRequest As csLoggedRequest = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pLoggedRequest) 
    If pLoggedRequest IsNot Nothing Then Return pLoggedRequest Else Return New csLoggedRequest() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LoggedLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLoggedLoginID(ByVal vLoggedLoginID As Long) As csLoggedRequestCol
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedRequest As csLoggedRequest In pTempDist.Values
        If pLoggedRequest.LoggedLoginID = vLoggedLoginID Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLoggedLoginID with vLoggedLoginID of {vLoggedLoginID}", "2ndPartOfClone") 
      Dim pList As csLoggedRequestCol = Me.Clone() 
      For Each pLoggedRequest As csLoggedRequest In pList 
        If pLoggedRequest.LoggedLoginID = vLoggedLoginID Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    End If 
    
    Return pLoggedRequests
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TimeAccessed
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTimeAccessed(ByVal vTimeAccessed As Date) As csLoggedRequestCol
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedRequest As csLoggedRequest In pTempDist.Values
        If pLoggedRequest.TimeAccessed = vTimeAccessed Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTimeAccessed with vTimeAccessed of {vTimeAccessed}", "2ndPartOfClone") 
      Dim pList As csLoggedRequestCol = Me.Clone() 
      For Each pLoggedRequest As csLoggedRequest In pList 
        If pLoggedRequest.TimeAccessed = vTimeAccessed Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    End If 
    
    Return pLoggedRequests
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserID(ByVal vUserID As Long) As csLoggedRequestCol
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedRequest As csLoggedRequest In pTempDist.Values
        If pLoggedRequest.UserID = vUserID Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserID with vUserID of {vUserID}", "2ndPartOfClone") 
      Dim pList As csLoggedRequestCol = Me.Clone() 
      For Each pLoggedRequest As csLoggedRequest In pList 
        If pLoggedRequest.UserID = vUserID Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    End If 
    
    Return pLoggedRequests
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CallingFunctionWithinApplication
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCallingFunctionWithinApplication(ByVal vCallingFunctionWithinApplication As String) As csLoggedRequestCol
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCallingFunctionWithinApplication = vCallingFunctionWithinApplication.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedRequest As csLoggedRequest In pTempDist.Values
        If pLoggedRequest.CallingFunctionWithinApplication.ToLowerInvariant() = vCallingFunctionWithinApplication Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCallingFunctionWithinApplication with vCallingFunctionWithinApplication of {vCallingFunctionWithinApplication}", "2ndPartOfClone") 
      Dim pList As csLoggedRequestCol = Me.Clone() 
      For Each pLoggedRequest As csLoggedRequest In pList 
        If pLoggedRequest.CallingFunctionWithinApplication.ToLowerInvariant() = vCallingFunctionWithinApplication Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    End If 
    
    Return pLoggedRequests
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EntryPoint
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEntryPoint(ByVal vEntryPoint As String) As csLoggedRequestCol
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEntryPoint = vEntryPoint.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedRequest As csLoggedRequest In pTempDist.Values
        If pLoggedRequest.EntryPoint.ToLowerInvariant() = vEntryPoint Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEntryPoint with vEntryPoint of {vEntryPoint}", "2ndPartOfClone") 
      Dim pList As csLoggedRequestCol = Me.Clone() 
      For Each pLoggedRequest As csLoggedRequest In pList 
        If pLoggedRequest.EntryPoint.ToLowerInvariant() = vEntryPoint Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    End If 
    
    Return pLoggedRequests
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Process
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProcess(ByVal vProcess As String) As csLoggedRequestCol
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vProcess = vProcess.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedRequest As csLoggedRequest In pTempDist.Values
        If pLoggedRequest.Process.ToLowerInvariant() = vProcess Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByProcess with vProcess of {vProcess}", "2ndPartOfClone") 
      Dim pList As csLoggedRequestCol = Me.Clone() 
      For Each pLoggedRequest As csLoggedRequest In pList 
        If pLoggedRequest.Process.ToLowerInvariant() = vProcess Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    End If 
    
    Return pLoggedRequests
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Thread
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByThread(ByVal vThread As String) As csLoggedRequestCol
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vThread = vThread.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedRequest As csLoggedRequest In pTempDist.Values
        If pLoggedRequest.Thread.ToLowerInvariant() = vThread Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByThread with vThread of {vThread}", "2ndPartOfClone") 
      Dim pList As csLoggedRequestCol = Me.Clone() 
      For Each pLoggedRequest As csLoggedRequest In pList 
        If pLoggedRequest.Thread.ToLowerInvariant() = vThread Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    End If 
    
    Return pLoggedRequests
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csLoggedRequestCol
    Dim pLoggedRequests As New csLoggedRequestCol(_WithParents) 
    pLoggedRequests._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedRequest) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedRequest As csLoggedRequest In pTempDist.Values
        If pLoggedRequest.Tag.ToLowerInvariant() = vTag Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csLoggedRequestCol = Me.Clone() 
      For Each pLoggedRequest As csLoggedRequest In pList 
        If pLoggedRequest.Tag.ToLowerInvariant() = vTag Then
          Dim pLoggedRequestClone As csLoggedRequest = pLoggedRequest.Clone()
          pLoggedRequests.Add(pLoggedRequestClone)
          If Not _FilledFromSumOnTheFly Then pLoggedRequests._Clean.Add(pLoggedRequest.ID) 
        End If
      Next
    End If 
    
    Return pLoggedRequests
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
    For Each pLoggedRequest As csLoggedRequest In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pLoggedRequest.LoadDataRow(pRow, vRequester) 
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
    Me.Sort(New csLoggedRequestCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
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
  
  Public Sub SortByLoggedLoginID()
    Me.Sort(New csLoggedRequestCol.CompareByLoggedLoginID)
  End Sub
  Private Class CompareByLoggedLoginID
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LoggedLoginID < y.LoggedLoginID Then
        Return -1
      ElseIf x.LoggedLoginID = y.LoggedLoginID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLoggedLoginText()
    Me.Sort(New csLoggedRequestCol.CompareByLoggedLoginText)
  End Sub
  Private Class CompareByLoggedLoginText
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LoggedLoginText, y.LoggedLoginText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTimeAccessed()
    Me.Sort(New csLoggedRequestCol.CompareByTimeAccessed)
  End Sub
  Private Class CompareByTimeAccessed
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TimeAccessed < y.TimeAccessed Then
        Return -1
      ElseIf x.TimeAccessed = y.TimeAccessed Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUserID()
    Me.Sort(New csLoggedRequestCol.CompareByUserID)
  End Sub
  Private Class CompareByUserID
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.UserID < y.UserID Then
        Return -1
      ElseIf x.UserID = y.UserID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUserText()
    Me.Sort(New csLoggedRequestCol.CompareByUserText)
  End Sub
  Private Class CompareByUserText
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserText, y.UserText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCallingFunctionWithinApplication()
    Me.Sort(New csLoggedRequestCol.CompareByCallingFunctionWithinApplication)
  End Sub
  Private Class CompareByCallingFunctionWithinApplication
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CallingFunctionWithinApplication, y.CallingFunctionWithinApplication, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEntryPoint()
    Me.Sort(New csLoggedRequestCol.CompareByEntryPoint)
  End Sub
  Private Class CompareByEntryPoint
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EntryPoint, y.EntryPoint, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProcess()
    Me.Sort(New csLoggedRequestCol.CompareByProcess)
  End Sub
  Private Class CompareByProcess
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Process, y.Process, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByThread()
    Me.Sort(New csLoggedRequestCol.CompareByThread)
  End Sub
  Private Class CompareByThread
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Thread, y.Thread, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csLoggedRequestCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csLoggedRequest)
    Private Function Compare(ByVal x As csLoggedRequest, ByVal y As csLoggedRequest) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedRequest).Compare
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
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLoggedRequest) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLoggedRequest) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      _WithParents = clsEnums.enmLoadParent.UD 
      bHasParents = True 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
