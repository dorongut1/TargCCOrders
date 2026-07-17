Public Class csEnumeration
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
 
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return True 
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
    [IsSystem] 
    [EnumType] 
    [EnumValue] 
    [Text] 
    [TextLocalized] 
    [Tag] 
  End Enum 
  ''' <summary> 
  ''' Raised before add, just before evtBeforeUpdate 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeAdd(ByRef rCancel As Boolean) 
  Friend Event evtBeforeAddWithRequester(ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  ''' <summary> 
  ''' Raised after add, just before evtAfterUpdate 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterAdd()
  Friend Event evtAfterAddWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  'UpdatedColumns
  Public Enum enmUpdateType 
    UD 
    [Standard] 
  End Enum 
  ''' <summary> 
  ''' Raised before updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeUpdate(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean) 
  Friend Event evtBeforeUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  ''' <summary> 
  ''' Raised after updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterUpdate(ByVal vWhichColumn As enmUpdateType)
  Friend Event evtAfterUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  ''' <summary> 
  ''' Raised before deleting the row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeDelete(ByRef rCancel As Boolean) 
  Friend Event evtBeforeDeleteWithRequester(ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  ''' <summary> 
  ''' Raised after deleting the row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterDelete() 
  Friend Event evtAfterDeleteWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private Event evtOverrideDefaultDesignation(ByRef rOverridenValue As String) 
  Private _IsLocalized As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsLocalized() As Boolean 
    Get
      Return _IsLocalized
    End Get
  End Property
  Private _LocalizedLanguage As clsEnums.enmLanguage 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property LocalizedLanguage() As clsEnums.enmLanguage 
    Get 
      Return _LocalizedLanguage 
    End Get 
  End Property 
  
  Private _ID As Integer
  Private _IsSystem As Boolean
  Private _EnumType As String
  Private _EnumValue As String
  Private _Text As String
  Private _TextLocalized As String 
  Private _Tag As String
  
  Public Property [ID]() As Integer
    Get
      Return Me._ID
    End Get
    Set(ByVal value As Integer)
      If Me._ID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ID = value 
        bPrimaryKey = _ID 
      End If 
    End Set
  End Property
  Public Property [IsSystem]() As Boolean
    Get
      Return Me._IsSystem
    End Get
    Set(ByVal value As Boolean)
      If Me._IsSystem <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IsSystem = value 
      End If 
    End Set
  End Property
  Public Property [EnumType]() As String
    Get
      Return Me._EnumType
    End Get
    Set(ByVal value As String)
      If Me._EnumType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EnumType = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [EnumValue]() As String
    Get
      Return Me._EnumValue
    End Get
    Set(ByVal value As String)
      If Me._EnumValue <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EnumValue = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [Text]() As String
    Get
      Return Me._Text
    End Get
    Set(ByVal value As String)
      If Me._Text <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Text = value 
      End If 
    End Set
  End Property
  Public Property [TextLocalized]() As String
    Get
      Return Me._TextLocalized
    End Get
    Set(ByVal value As String)
      If Me._TextLocalized <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TextLocalized = value 
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
    If pOverridenValue = Nothing Then bDefaultDesignation = _EnumType & " --> " & _EnumValue Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    pValue.Append("IsSystem='" & _IsSystem.ToString() & "' ‡ ") 
    If _EnumType <> "" Then pValue.Append("EnumType='" & _EnumType & "' ‡ ") 
    If _EnumValue <> "" Then pValue.Append("EnumValue='" & _EnumValue & "' ‡ ") 
    If _Text <> "" Then pValue.Append("Text='" & _Text & "' ‡ ") 
    If _TextLocalized <> "" Then pValue.Append("TextLocalized='" & _TextLocalized & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append(",""" & _IsSystem.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EnumType)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EnumValue)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Text)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TextLocalized)}""") 
    If Not vWithTexts Then 
        pCSV.Append($",""{ccHelper.StringForCSV(_Tag)}""") 
    End If 
    'pCSV.Append($",""{bDateAdded:yyyyMMddTHH:mm:ss.ffff}"" ") 
    
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty()
    _IsLocalized = False 
  End Sub
  
  Public Sub New(ByVal vIsLocalized As Boolean) 
    MyBase.New()
    CreateEmpty()
    _IsLocalized = vIsLocalized 
  End Sub
  
  Public Sub New(ByVal vPrimaryKeyValue As Integer, ByVal vIsLocalized As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional vMustExist As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    _IsLocalized = vIsLocalized 
    
    rFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
  End Sub
  
  Public Sub New(ByVal vcsEnumeration As csEnumeration)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsEnumeration) 
  End Sub
  
  Public Sub New( 
      vID As Integer _ 
    , Optional vIsSystem As Boolean = False _ 
    , Optional vEnumType As String = "" _ 
    , Optional vEnumValue As String = "" _ 
    , Optional vText As String = "" _ 
    , Optional vTextLocalized As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vIsLocalized As Boolean = False _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _IsSystem = vIsSystem 
    _EnumType = vEnumType 
    _EnumValue = vEnumValue 
    _Text = vText 
    _TextLocalized = vTextLocalized 
    _Tag = vTag 
    bDateAdded = vDateAdded 
    _IsLocalized = vIsLocalized 
    bccStatus = clsEnums.enmObjectStatus.Dirty 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  Friend Sub New(ByVal vRow As DataRow, ByVal vRequester As clsRequester, Optional ByVal vIsLocalized As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    Dim pFault As New clsFault 
 
    pFault = LoadDataRow(vRow, vRequester) 
    If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox) 
 
    _IsLocalized = vIsLocalized 
 
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
    _IsLocalized = vIsLocalized 
  End Sub 
  ''' <summary> 
  ''' The default language is that if the Requester object. You can override this here 
  ''' </summary> 
  ''' <param name="vLanguage"></param> 
  Public Sub OverrideDefaultLanguage(ByVal vLanguage As clsEnums.enmLanguage) 
    If _IsLocalized = True Then 
      _LocalizedLanguage = vLanguage 
    End If 
    If vLanguage <> clsEnums.enmLanguage.UD AndAlso _IsLocalized = False Then 
      Throw New Exception("You can't set a language unless you 1st localize the instance") 
    End If 
  End Sub 
 
  Private _IsTruncated As Boolean = False 
  
  ''' <summary> 
  ''' Use this before loading a DataGridView. You don't need more than X c to see what you want. 
  ''' </summary> 
  ''' <param name="pTruncateLength"></param> 
  Friend Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
    'Truncates strings, and reduces pictures to W 100 x H 50 
 
    _IsTruncated = False 
 
    _EnumType = _EnumType.Truncate(pTruncateLength, _IsTruncated) 
    _EnumValue = _EnumValue.Truncate(pTruncateLength, _IsTruncated) 
    _Text = _Text.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Enumeration by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    Try 
      pFault = GetByID(ccHelper.ToInteger(vPrimaryKeyValue), vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Enumeration-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [EnumTypeAndEnumValue] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Enumeration by the chosen parameters. This function may be a bit slower than accessing the Enumeration's GetBy... directly 
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
          pFault = GetByID(ccHelper.ToInteger(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.EnumTypeAndEnumValue 
          pFault = GetByEnumTypeAndEnumValue(CStr(vParameters(0)), CStr(vParameters(1)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Enumeration-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Enumeration-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the Enumeration by ID. 
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Integer, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
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
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csEnumerationGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the Enumeration 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the Enumeration by EnumTypeAndEnumValue. 
  ''' </summary>
  ''' <param name="vEnumType"></param>
  ''' <param name="vEnumValue"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByEnumTypeAndEnumValue(ByVal vEnumType As String, ByVal vEnumValue As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumType={0}, EnumValue={1}", vEnumType, vEnumValue)
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
          'vEnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) 
          ' 
          'vEnumValue 
          If vEnumValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValue) 
          ' 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csEnumerationGetByEnumTypeAndEnumValue" 
      Dim pParametersToLog = $"EnumTypeAndEnumValue: {vEnumType};{vEnumValue};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the Enumeration 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Enumeration-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Enumeration-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the Enumeration. If there are parents or children in the Enumeration, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = String.Format("Enumeration.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pEnumeration As New csEnumeration 
    If Me.isEqual(pEnumeration) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-Enumeration-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Enumeration-240611-135714", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Dim pObjectAdded As Boolean = False 
    
    If _ID = 0 Then 
      pObjectAdded = True 
      RaiseEvent evtBeforeAdd(pCancel) 
      If pCancel = True Then Return pFault 
      RaiseEvent evtBeforeAddWithRequester(pCancel, vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
      If pCancel = True Then Return pFault 
    End If 
    RaiseEvent evtBeforeUpdate(enmUpdateType.Standard, pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeUpdateWithRequester(enmUpdateType.Standard, pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csEnumerationUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Integer = BitConverter.ToInt32(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    If pObjectAdded = True Then 
      RaiseEvent evtAfterAdd() 
      RaiseEvent evtAfterAddWithRequester(vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
    End If 
      RaiseEvent evtAfterUpdate(enmUpdateType.Standard)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.Standard, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  'Interface Delete 
  Public Function Delete(ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntityDeletable.Delete 
    Dim pFunctionParameters As String = String.Format("Enumeration.ID={0}", _ID)
    Dim pFault As New clsFault
    
    Dim pCancel As Boolean = False
    
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(_ID) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csEnumerationDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterDelete()
    RaiseEvent evtAfterDeleteWithRequester(vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  ''' <summary>  
  ''' This function enables you to delete an entity from the database without first loading it.  
  ''' </summary>  
  ''' <param name="vID"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function DeleteByID(vID As Integer, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"ID: {vID}" 
    Dim pFault As clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vID) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "csEnumerationDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-231207-1707", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csEnumeration) Then Return False 
    Dim pEnumerationToTest As csEnumeration = CType(vTargCCEntityToTest, csEnumeration) 
    Return isEqual(pEnumerationToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vEnumerationToTest As csEnumeration) As Boolean
    With vEnumerationToTest
      If _ID <> .ID Then Return False
      If _IsSystem <> .IsSystem Then Return False
      If _EnumType <> .EnumType Then Return False
      If _EnumValue <> .EnumValue Then Return False
      If _Text <> .Text Then Return False
      If _TextLocalized <> .TextLocalized Then Return False
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
    Dim pClone As New csEnumeration(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csEnumeration
    Dim pClone As New csEnumeration(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-Enumeration-130316-0852", vRequester) : End Try 
    Try : vDataRow("IsSystem") = _IsSystem : Catch ex As Exception : Return pFault.LogException(ex, "IsSystem", "TRGT-Enumeration-130316-0852", vRequester) : End Try 
    Try : vDataRow("EnumType") = _EnumType : Catch ex As Exception : Return pFault.LogException(ex, "EnumType", "TRGT-Enumeration-130316-0852", vRequester) : End Try 
    Try : vDataRow("EnumValue") = _EnumValue : Catch ex As Exception : Return pFault.LogException(ex, "EnumValue", "TRGT-Enumeration-130316-0852", vRequester) : End Try 
    Try : vDataRow("Text") = _Text : Catch ex As Exception : Return pFault.LogException(ex, "Text", "TRGT-Enumeration-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Enumeration-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pEnumeration As csEnumeration = CType(pXmlSerializer.Deserialize(pStreamReader), csEnumeration) 
      AssignValues(pEnumeration) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-Enumeration-130515-1230", vRequester) 
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
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Write(bccStatus.FastToString()) 
          'ID 
          pBinaryWriter.Write(_ID) 
          'IsSystem 
          pBinaryWriter.Write(_IsSystem) 
          'EnumType 
          If _EnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_EnumType) 
          'EnumValue 
          If _EnumValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_EnumValue) 
          'Text 
          If _Text Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Text) 
          pBinaryWriter.Write(_TextLocalized) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Enumeration-150307-2338", vRequester) 
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
          _IsLocalized = pReader.ReadBoolean 
          _LocalizedLanguage = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          bccStatus = clsEnums.TranslateEnmObjectStatus(pReader.ReadString) 
          'ID 
          _ID = pReader.ReadInt32 
          'IsSystem 
          _IsSystem = pReader.ReadBoolean 
          'EnumType 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _EnumType = pReader.ReadString 
          'EnumValue 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _EnumValue = pReader.ReadString 
          'Text 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Text = pReader.ReadString 
          'Localizable 
          _TextLocalized = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-Enumeration-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Enumeration-190720-1443", vRequester) 
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
 
      Dim pEnumeration As csEnumeration = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csEnumeration)(vJSON, pSettings) 
      AssignValues(pEnumeration) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Enumeration-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vEnumeration As csEnumeration)
    With vEnumeration
      _ID = .ID 
      _IsSystem = .IsSystem 
      _EnumType = .EnumType 
      _EnumValue = .EnumValue 
      _Text = .Text 
      _TextLocalized = .TextLocalized
      _Tag = .Tag 
      _IsLocalized = .IsLocalized 
      _LocalizedLanguage = .LocalizedLanguage 
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
    _IsSystem = False
    _EnumType = ""
    _EnumValue = ""
    _Text = ""
    _TextLocalized = ""
    _Tag = ""
    bccStatus = clsEnums.enmObjectStatus.New 
    bPrimaryKey = _ID 
    bDateAdded = Nothing 
    bDefaultDesignation = "" 
     
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      _IsLocalized = False 
      _LocalizedLanguage = clsEnums.enmLanguage.UD 
      bHasParents = False 
      bHasLocalizedFields = True 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class csEnumerationCol
  Inherits cTargCCCollection(Of csEnumeration)
  Implements ITargCCCollectionUpdateable 
  
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return True 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property CanHave0AsPrimaryKey As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Integer) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Integer, csEnumeration) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByEnumTypeAndEnumValue As Dictionary(Of String, csEnumeration) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByEnumTypeAndEnumValue As Boolean 
  Private Function CreateKeyForFindByEnumTypeAndEnumValue(ByVal vEnumeration As csEnumeration) As String 
    With vEnumeration 
      Return .EnumType & "|" & .EnumValue
    End With 
  End Function 
   
  Private _IsLocalized As Boolean 
  Public ReadOnly Property IsLocalized() As Boolean
    Get
      Return _IsLocalized
    End Get
  End Property
  Private _LocalizedLanguage As clsEnums.enmLanguage 
  Public ReadOnly Property LocalizedLanguage() As clsEnums.enmLanguage 
    Get 
      Return _LocalizedLanguage 
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
 
    For Each pRow As csEnumeration In Me 
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
    pCSVTitle.Append(",""IsSystem""") 
    pCSVTitle.Append(",""EnumType""") 
    pCSVTitle.Append(",""EnumValue""") 
    pCSVTitle.Append(",""Text""") 
    pCSVTitle.Append(",""TextLocalized""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csEnumeration In Me 
      pCSV.AppendLine(pRow.ToCSV(vWithTexts)) 
    Next 
 
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty() 
  End Sub
  
  Public Sub New(ByVal vIsLocalized As Boolean) 
    MyBase.New()
    CreateEmpty() 
    _IsLocalized = vIsLocalized 
  End Sub
  
  Public Sub New(ByVal vIsLocalized As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) 
    MyBase.New()
    CreateEmpty() 
    _IsLocalized = vIsLocalized 
    
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
 
  Public Overloads Sub Add(ByVal vEnumeration As csEnumeration) 
    SyncLock _CollectionLock 
      MyBase.Add(vEnumeration) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByEnumTypeAndEnumValue = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vEnumeration As csEnumeration) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vEnumeration) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByEnumTypeAndEnumValue = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vEnumerationCol As csEnumerationCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vEnumerationCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByEnumTypeAndEnumValue = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByEnumTypeAndEnumValue = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vEnumeration As csEnumeration) 
    SyncLock _CollectionLock 
      MyBase.Remove(vEnumeration) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByEnumTypeAndEnumValue = True 
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
      Dim pTempDictionary As New Dictionary(Of Integer, csEnumeration) 
      
      For Each lEnumeration In Me 
        If lEnumeration.IsEmpty OrElse pTempDictionary.ContainsKey(lEnumeration.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lEnumeration.ID, lEnumeration) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lEnumeration.ToString, "TRGT-Enumeration-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", Enumeration:" & lEnumeration.ToString() & ", TRGT-Enumeration-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadEnumTypeAndEnumValues() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByEnumTypeAndEnumValue Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByEnumTypeAndEnumValue Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByEnumTypeAndEnumValue = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByEnumTypeAndEnumValue' yet!
      Dim pTempDictionary As New Dictionary(Of String, csEnumeration)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lEnumeration In Me 
        Try 
          Dim pEnumTypeAndEnumValue As String = CreateKeyForFindByEnumTypeAndEnumValue(lEnumeration) 
          If String.IsNullOrEmpty(pEnumTypeAndEnumValue.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pEnumTypeAndEnumValue)) Then 
            pTempDictionary.Add(pEnumTypeAndEnumValue, lEnumeration) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lEnumeration.ToString, "TRGT-Enumeration-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByEnumTypeAndEnumValue:" & ex.Message & ", Enumeration:" & lEnumeration.ToString() & ", TRGT-Enumeration-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByEnumTypeAndEnumValue = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByEnumTypeAndEnumValue = False
    End SyncLock 
  End Sub 
 
  Public Overrides Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) 
    Throw New Exception("Entity has no parents") 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    _IsLocalized = vIsLocalized 
  End Sub 
  ''' <summary> 
  ''' The default language is that if the Requester object. You can override this here 
  ''' </summary> 
  ''' <param name="vLanguage"></param> 
  Public Sub OverrideDefaultLanguage(ByVal vLanguage As clsEnums.enmLanguage) 
    If _IsLocalized = True Then 
      _LocalizedLanguage = vLanguage 
    End If 
    If vLanguage <> clsEnums.enmLanguage.UD AndAlso _IsLocalized = False Then 
      Throw New Exception("You can't set a language unless you 1st localize the instance") 
    End If 
  End Sub 
 
  ''' <summary>  
  ''' Use this before loading a DataGridView. You don't need more than pTruncateLength characters to see what you want.  
  ''' </summary>  
  ''' <param name="pTruncateLength"></param>  
  Public Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
 
    For Each lEnumeration As csEnumeration In Me 
      lEnumeration.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [EnumType] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the Enumerations by the chosen parameters. This function may be a bit slower than accessing the Enumeration's FillBy... directly 
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
        Case enmFillByParameterCombination.EnumType 
          pFault = FillByEnumType(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Enumeration-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Enumeration-151223_1716", vRequester) 
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
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific EnumType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByEnumType(ByVal vEnumType As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumType={0}", vEnumType)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillByEnumType" 
      Dim pParametersToLog = $"EnumType: {vEnumType};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration  
      If vAppend = True Then 
        Dim pEnumerations As New csEnumerationCol 
        pEnumerations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pEnumerations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150407-2142", vRequester) 
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
  Public Function FillByBoundedID(ByVal vIDFrom As Integer, ByVal vIDTo As Integer, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
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
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration  
      If vAppend = True Then 
        Dim pEnumerations As New csEnumerationCol 
        pEnumerations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pEnumerations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific EnumType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedEnumType(ByVal vEnumTypeFrom As String, ByVal vEnumTypeTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumTypeFrom={0}, EnumTypeTo={1}", vEnumTypeFrom, vEnumTypeTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumTypeFrom 
          If vEnumTypeFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumTypeFrom) 
          ' 
          'vEnumTypeTo 
          If vEnumTypeTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumTypeTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillByBoundedEnumType" 
      Dim pParametersToLog = $"EnumType: {vEnumTypeFrom};{vEnumTypeTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration  
      If vAppend = True Then 
        Dim pEnumerations As New csEnumerationCol 
        pEnumerations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pEnumerations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific EnumType and EnumValue, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedEnumTypeAndEnumValue(ByVal vEnumTypeFrom As String, ByVal vEnumTypeTo As String, ByVal vEnumValueFrom As String, ByVal vEnumValueTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumTypeFrom={0}, EnumTypeTo={1}, EnumValueFrom={2}, EnumValueTo={3}", vEnumTypeFrom, vEnumTypeTo, vEnumValueFrom, vEnumValueTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumTypeFrom 
          If vEnumTypeFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumTypeFrom) 
          ' 
          'vEnumTypeTo 
          If vEnumTypeTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumTypeTo) 
          ' 
          'vEnumValueFrom 
          If vEnumValueFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValueFrom) 
          ' 
          'vEnumValueTo 
          If vEnumValueTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValueTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillByBoundedEnumTypeAndEnumValue" 
      Dim pParametersToLog = $"EnumTypeAndEnumValue: {vEnumTypeFrom};{vEnumTypeTo};{vEnumValueFrom};{vEnumValueTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration  
      If vAppend = True Then 
        Dim pEnumerations As New csEnumerationCol 
        pEnumerations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pEnumerations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific EnumType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardEnumType(ByVal vEnumType As String, ByVal vEnumTypeWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumType={0}, EnumTypeWildcardType={1}", vEnumType, vEnumTypeWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) 
          ' 
          pBinaryWriter.Write(vEnumTypeWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillByWildCardEnumType" 
      Dim pParametersToLog = $"EnumType: {vEnumType};{vEnumTypeWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration  
      If vAppend = True Then 
        Dim pEnumerations As New csEnumerationCol 
        pEnumerations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pEnumerations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific EnumType and EnumValue, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardEnumTypeAndEnumValue(ByVal vEnumType As String, ByVal vEnumTypeWildcardType As clsEnums.enmWildCardType, ByVal vEnumValue As String, ByVal vEnumValueWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumType={0}, EnumTypeWildcardType={1}, EnumValue={2}, EnumValueWildcardType={3}", vEnumType, vEnumTypeWildcardType.FastToString(), vEnumValue, vEnumValueWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) 
          ' 
          pBinaryWriter.Write(vEnumTypeWildcardType.FastToString())
          'vEnumValue 
          If vEnumValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValue) 
          ' 
          pBinaryWriter.Write(vEnumValueWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillByWildCardEnumTypeAndEnumValue" 
      Dim pParametersToLog = $"EnumTypeAndEnumValue: {vEnumType};{vEnumTypeWildcardType.FastToString()};{vEnumValue};{vEnumValueWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration  
      If vAppend = True Then 
        Dim pEnumerations As New csEnumerationCol 
        pEnumerations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pEnumerations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150407-2142", vRequester) 
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
  Public Function FillByListOfID(vIDs As List(Of Integer), vRequester As clsRequester, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault 
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
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration   
      If vAppend = True Then 
        Dim pEnumerations As New csEnumerationCol 
        pEnumerations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pEnumerations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-231207-1750", vRequester) 
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
    [EnumType]
    EnumTypeWildcardType
    [EnumValue]
    EnumValueWildcardType
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
 
    Dim pIDFrom As Nullable(Of Integer) = Nothing
    Dim pIDTo As Nullable(Of Integer) = Nothing
    Dim pEnumType As String = Nothing
    Dim pEnumTypeWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pEnumValue As String = Nothing
    Dim pEnumValueWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.EnumType) Then pObj = vParameters(enmFillOnTheFlyParameters.EnumType) : If pObj IsNot Nothing Then pEnumType = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.EnumTypeWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.EnumTypeWildcardType) : If pObj IsNot Nothing Then pEnumTypeWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.EnumValue) Then pObj = vParameters(enmFillOnTheFlyParameters.EnumValue) : If pObj IsNot Nothing Then pEnumValue = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.EnumValueWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.EnumValueWildcardType) : If pObj IsNot Nothing Then pEnumValueWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pEnumType, pEnumTypeWildcardType _
        , pEnumValue, pEnumValueWildcardType _
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
          ByVal vIDFrom As Nullable(Of Integer), ByVal vIDTo As Nullable(Of Integer) _
        , ByVal vEnumType As String, ByVal vEnumTypeWildcardType As clsEnums.enmWildCardType _
        , ByVal vEnumValue As String, ByVal vEnumValueWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, EnumType={2}, EnumTypeWildcardType={3}, EnumValue={4}, EnumValueWildcardType={5}", vIDFrom, vIDTo, vEnumType, vEnumTypeWildcardType.FastToString(), vEnumValue, vEnumValueWildcardType.FastToString())
    
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
          'EnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) : pBinaryWriter.Write(vEnumTypeWildcardType.FastToString()) : pParametersToLog &= $"EnumType={vEnumType};" : pParametersToLog &= $"EnumTypeWildcardType={vEnumTypeWildcardType};"  
          'EnumValue 
          If vEnumValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValue) : pBinaryWriter.Write(vEnumValueWildcardType.FastToString()) : pParametersToLog &= $"EnumValue={vEnumValue};" : pParametersToLog &= $"EnumValueWildcardType={vEnumValueWildcardType};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByEnumType
    GroupByEnumValue
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
 
    Dim pIDFrom As Nullable(Of Integer) = Nothing
    Dim pIDTo As Nullable(Of Integer) = Nothing
    Dim pEnumType As String = Nothing
    Dim pEnumTypeWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pEnumValue As String = Nothing
    Dim pEnumValueWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pGroupByEnumType As Boolean = False
    Dim pGroupByEnumValue As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.EnumType) Then pObj = vParameters(enmFillOnTheFlyParameters.EnumType) : If pObj IsNot Nothing Then pEnumType = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.EnumTypeWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.EnumTypeWildcardType) : If pObj IsNot Nothing Then pEnumTypeWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.EnumValue) Then pObj = vParameters(enmFillOnTheFlyParameters.EnumValue) : If pObj IsNot Nothing Then pEnumValue = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.EnumValueWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.EnumValueWildcardType) : If pObj IsNot Nothing Then pEnumValueWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByEnumType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByEnumType) : If pObj IsNot Nothing Then pGroupByEnumType = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByEnumValue) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByEnumValue) : If pObj IsNot Nothing Then pGroupByEnumValue = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pEnumType, pEnumTypeWildcardType _
        , pEnumValue, pEnumValueWildcardType _
        , pGroupByEnumType _
        , pGroupByEnumValue _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a grouped collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillSumOnTheFly( _
          ByVal vIDFrom As Nullable(Of Integer), ByVal vIDTo As Nullable(Of Integer) _
        , ByVal vEnumType As String, ByVal vEnumTypeWildcardType As clsEnums.enmWildCardType _
        , ByVal vEnumValue As String, ByVal vEnumValueWildcardType As clsEnums.enmWildCardType _
        , ByVal vGroupByEnumType As Boolean _
        , ByVal vGroupByEnumValue As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, EnumType={2}, EnumTypeWildcardType={3}, EnumValue={4}, EnumValueWildcardType={5}, GroupByEnumType={6}, GroupByEnumValue={7}", vIDFrom, vIDTo, vEnumType, vEnumTypeWildcardType.FastToString(), vEnumValue, vEnumValueWildcardType.FastToString(), vGroupByEnumType, vGroupByEnumValue)
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
          'EnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) : pBinaryWriter.Write(vEnumTypeWildcardType.FastToString()) 
          'EnumValue 
          If vEnumValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValue) : pBinaryWriter.Write(vEnumValueWildcardType.FastToString()) 
          pBinaryWriter.Write(vGroupByEnumType) : pParametersToLog &= $"GroupByEnumType={vGroupByEnumType};"  
          pBinaryWriter.Write(vGroupByEnumValue) : pParametersToLog &= $"GroupByEnumValue={vGroupByEnumValue};"  
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Enumeration  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vEnumerationArray As csEnumeration())
    Me.Clear()
    
    For Each pEnumeration As csEnumeration In vEnumerationArray
      Me.Add(pEnumeration)
      _Clean.Add(pEnumeration.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pEnumeration As New csEnumeration(pRow, vRequester, _IsLocalized) 
        Me.Add(pEnumeration) 
        _Clean.Add(pEnumeration.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-EnumerationCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Enumeration-130515-1300", vRequester) 
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
      Dim pEnumerations As csEnumerationCol = CType(pXmlSerializer.Deserialize(pStreamReader), csEnumerationCol) 
      For Each pEnumeration As csEnumeration In pEnumerations 
        Me.Add(pEnumeration) 
        _Clean.Add(pEnumeration.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-Enumeration-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Enumeration-190720-1443", vRequester) 
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
 
      Dim pEnumerations As List(Of csEnumeration) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csEnumeration))(vJSON, pSettings) 
      For Each pEnumeration As csEnumeration In pEnumerations 
        Me.Add(pEnumeration) 
        _Clean.Add(pEnumeration.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Enumeration-190720-2059", vRequester) 
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
          'IsLocalized 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          'Items 
          pBinaryWriter.Write(Me.Count) 
          For Each lEnumeration As csEnumeration In Me 
            Dim pByte As Byte() = lEnumeration.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Enumeration-150307-2340", vRequester) 
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
          'IsLocalized 
          _IsLocalized = pReader.ReadBoolean 
          _LocalizedLanguage = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          'Items 
          Dim pCount As Integer = pReader.ReadInt32 
          For iCntr As Integer = 0 To pCount - 1 
            Dim pLength As Integer = pReader.ReadInt32 
            Dim pEnumeration As csEnumeration = New csEnumeration(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pEnumeration) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pEnumeration.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-Enumeration-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pEnumeration As csEnumeration In Me 
      With pEnumeration 
        pFault = pEnumeration.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csEnumerationCol) Then Return False 
    Dim pEnumerationColToTest As csEnumerationCol = CType(vEntitiesToTest, csEnumerationCol) 
    Return isEqual(pEnumerationColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vEnumerationsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vEnumerationsToTest As csEnumerationCol) As Boolean
    If Me.Count <> vEnumerationsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vEnumerationsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pEnumerations As New csEnumerationCol(_IsLocalized) 
    If pFilledFromSumOnTheFly Then pEnumerations._FilledFromSumOnTheFly = True
    
    For Each pEnumeration As csEnumeration In Me 
      Dim pEnumerationClone As csEnumeration = pEnumeration.Clone() 
      pEnumerations.Add(pEnumerationClone) 
      If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pEnumerations 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csEnumerationCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pEnumerations As New csEnumerationCol(_IsLocalized) 
    If pFilledFromSumOnTheFly Then pEnumerations._FilledFromSumOnTheFly = True
    
    For Each pEnumeration As csEnumeration In Me
      Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
      pEnumerations.Add(pEnumerationClone)
      If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
    Next
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pEnumerations
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Integer, ByVal vIDTo As Integer) As csEnumerationCol 
    Dim pEnumerations As New csEnumerationCol(_IsLocalized)  
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pEnumeration As csEnumeration In _SortedDictionaryForFindByID.Values.ToList() 
      If (pEnumeration.ID > vIDFrom AndAlso pEnumeration.ID <= vIDTo) Then 
        Dim pEnumerationClone As csEnumeration = pEnumeration.Clone() 
        pEnumerations.Add(pEnumerationClone) 
        If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pEnumerations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by EnumType (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedEnumType(ByVal vEnumTypeFrom As String, ByVal vEnumTypeTo As String) As csEnumerationCol 
    Dim pEnumerations As New csEnumerationCol(_IsLocalized)  
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pEnumeration As csEnumeration In _SortedDictionaryForFindByID.Values.ToList() 
      If (pEnumeration.EnumType > vEnumTypeFrom AndAlso pEnumeration.EnumType <= vEnumTypeTo) Then 
        Dim pEnumerationClone As csEnumeration = pEnumeration.Clone() 
        pEnumerations.Add(pEnumerationClone) 
        If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pEnumerations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by EnumType and EnumValue (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedEnumTypeAndEnumValue(ByVal vEnumTypeFrom As String, ByVal vEnumTypeTo As String, ByVal vEnumValueFrom As String, ByVal vEnumValueTo As String) As csEnumerationCol 
    Dim pEnumerations As New csEnumerationCol(_IsLocalized)  
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pEnumeration As csEnumeration In _SortedDictionaryForFindByID.Values.ToList() 
      If (pEnumeration.EnumType > vEnumTypeFrom AndAlso pEnumeration.EnumType <= vEnumTypeTo) AndAlso (pEnumeration.EnumValue > vEnumValueFrom AndAlso pEnumeration.EnumValue <= vEnumValueTo) Then 
        Dim pEnumerationClone As csEnumeration = pEnumeration.Clone() 
        pEnumerations.Add(pEnumerationClone) 
        If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pEnumerations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardEnumType(ByVal vEnumType As String, ByVal vEnumTypeWildcardType As clsEnums.enmWildCardType) As csEnumerationCol 
    Dim pEnumerations As New csEnumerationCol 
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pEnumeration As csEnumeration In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vEnumTypeWildcardType = clsEnums.enmWildCardType.After Then 
        If pEnumeration.EnumType.StartsWith(vEnumType, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vEnumTypeWildcardType = clsEnums.enmWildCardType.Before Then 
        If pEnumeration.EnumType.EndsWith(vEnumType, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vEnumTypeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pEnumeration.EnumType.IndexOf(vEnumType, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vEnumTypeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vEnumType.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pEnumeration.EnumType.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pEnumerationClone As csEnumeration = pEnumeration.Clone() 
      pEnumerations.Add(pEnumerationClone) 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pEnumerations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardEnumTypeAndEnumValue(ByVal vEnumType As String, ByVal vEnumTypeWildcardType As clsEnums.enmWildCardType, ByVal vEnumValue As String, ByVal vEnumValueWildcardType As clsEnums.enmWildCardType) As csEnumerationCol 
    Dim pEnumerations As New csEnumerationCol 
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pEnumeration As csEnumeration In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vEnumTypeWildcardType = clsEnums.enmWildCardType.After Then 
        If pEnumeration.EnumType.StartsWith(vEnumType, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vEnumTypeWildcardType = clsEnums.enmWildCardType.Before Then 
        If pEnumeration.EnumType.EndsWith(vEnumType, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vEnumTypeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pEnumeration.EnumType.IndexOf(vEnumType, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vEnumTypeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vEnumType.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pEnumeration.EnumType.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      If vEnumValueWildcardType = clsEnums.enmWildCardType.After Then 
        If pEnumeration.EnumValue.StartsWith(vEnumValue, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vEnumValueWildcardType = clsEnums.enmWildCardType.Before Then 
        If pEnumeration.EnumValue.EndsWith(vEnumValue, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vEnumValueWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pEnumeration.EnumValue.IndexOf(vEnumValue, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vEnumValueWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vEnumValue.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pEnumeration.EnumValue.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pEnumerationClone As csEnumeration = pEnumeration.Clone() 
      pEnumerations.Add(pEnumerationClone) 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pEnumerations 
  End Function 
  
  ''' <summary> 
  ''' Used for Interface compliance. This returns a unique object in the collection. It searches locally, within the collection. It does not access the database  
  ''' If it doesn't find anything, it creates a new, empty object 
  ''' </summary> 
  ''' <param name="vPrimaryKey"></param> 
  ''' <returns></returns> 
  Public Overrides Function FindByPrimaryKey(vPrimaryKey As Long) As ITargCCEntity 
    Return FindByID(ccHelper.ToInteger(vPrimaryKey)) 
  End Function 
 
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByID(ByVal vID As Integer) As csEnumeration
    If Me.Count = 0 Then Return New csEnumeration 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Integer, csEnumeration) = _SortedDictionaryForFindByID 
    
    Dim pEnumeration As csEnumeration = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pEnumeration) 
    If pEnumeration IsNot Nothing Then Return pEnumeration Else Return New csEnumeration() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByEnumTypeAndEnumValue(ByVal vEnumType As String, ByVal vEnumValue As String) As csEnumeration
    If Me.Count = 0 Then Return New csEnumeration 
    
    If _RecreateDictionaryForFindByEnumTypeAndEnumValue = True Then LoadEnumTypeAndEnumValues() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csEnumeration) = _SortedDictionaryForFindByEnumTypeAndEnumValue 
    
    Dim pEnumeration As csEnumeration = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vEnumType & "|" & vEnumValue
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pEnumeration) 
    If pEnumeration IsNot Nothing Then Return pEnumeration Else Return New csEnumeration() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IsSystem
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIsSystem(ByVal vIsSystem As Boolean) As csEnumerationCol
    Dim pEnumerations As New csEnumerationCol(_IsLocalized) 
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Integer, csEnumeration) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pEnumeration As csEnumeration In pTempDist.Values
        If pEnumeration.IsSystem = vIsSystem Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIsSystem with vIsSystem of {vIsSystem}", "2ndPartOfClone") 
      Dim pList As csEnumerationCol = Me.Clone() 
      For Each pEnumeration As csEnumeration In pList 
        If pEnumeration.IsSystem = vIsSystem Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pEnumerations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EnumType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEnumType(ByVal vEnumType As String) As csEnumerationCol
    Dim pEnumerations As New csEnumerationCol(_IsLocalized) 
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Integer, csEnumeration) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEnumType = vEnumType.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pEnumeration As csEnumeration In pTempDist.Values
        If pEnumeration.EnumType.ToLowerInvariant() = vEnumType Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEnumType with vEnumType of {vEnumType}", "2ndPartOfClone") 
      Dim pList As csEnumerationCol = Me.Clone() 
      For Each pEnumeration As csEnumeration In pList 
        If pEnumeration.EnumType.ToLowerInvariant() = vEnumType Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pEnumerations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EnumValue
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEnumValue(ByVal vEnumValue As String) As csEnumerationCol
    Dim pEnumerations As New csEnumerationCol(_IsLocalized) 
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Integer, csEnumeration) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEnumValue = vEnumValue.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pEnumeration As csEnumeration In pTempDist.Values
        If pEnumeration.EnumValue.ToLowerInvariant() = vEnumValue Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEnumValue with vEnumValue of {vEnumValue}", "2ndPartOfClone") 
      Dim pList As csEnumerationCol = Me.Clone() 
      For Each pEnumeration As csEnumeration In pList 
        If pEnumeration.EnumValue.ToLowerInvariant() = vEnumValue Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pEnumerations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Text
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByText(ByVal vText As String) As csEnumerationCol
    Dim pEnumerations As New csEnumerationCol(_IsLocalized) 
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Integer, csEnumeration) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vText = vText.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pEnumeration As csEnumeration In pTempDist.Values
        If pEnumeration.Text.ToLowerInvariant() = vText Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByText with vText of {vText}", "2ndPartOfClone") 
      Dim pList As csEnumerationCol = Me.Clone() 
      For Each pEnumeration As csEnumeration In pList 
        If pEnumeration.Text.ToLowerInvariant() = vText Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pEnumerations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csEnumerationCol
    Dim pEnumerations As New csEnumerationCol(_IsLocalized) 
    pEnumerations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Integer, csEnumeration) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pEnumeration As csEnumeration In pTempDist.Values
        If pEnumeration.Tag.ToLowerInvariant() = vTag Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csEnumerationCol = Me.Clone() 
      For Each pEnumeration As csEnumeration In pList 
        If pEnumeration.Tag.ToLowerInvariant() = vTag Then
          Dim pEnumerationClone As csEnumeration = pEnumeration.Clone()
          pEnumerations.Add(pEnumerationClone)
          If Not _FilledFromSumOnTheFly Then pEnumerations._Clean.Add(pEnumeration.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pEnumerations.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pEnumerations
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
    For Each pEnumeration As csEnumeration In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pEnumeration.LoadDataRow(pRow, vRequester) 
      If pFault.isOK = False Then Return pFault 
      vDataTable.Rows.Add(pRow) 
    Next 
 
    Return pFault.SetOK 
  End Function 
 
  ''' <summary> 
  ''' This updates a collection that originates from the database. It will delete any rows not in the collection that were originally there (checks the 'Clean' variable) 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault Implements ITargCCCollectionUpdateable.Update 
    Dim pFunctionParameters As String = ""
 
    Dim pFault As New clsFault
 
    'Check for new rows 
    For Each p As csEnumeration In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
    For Each pCleanID As Integer In _Clean 
      If pCleanID = 0 Then Continue For 
      Dim pFound As csEnumeration = Me.FindByID(pCleanID) 
      If pFound.ID = 0 Then 
        Dim pEnumerationToKill As New csEnumeration 
        pEnumerationToKill.ID = pCleanID 
        pEnumerationToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
        Me.Add(pEnumerationToKill) 
      End If 
    Next 
 
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
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csEnumerationColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the EnumerationCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150314-1803", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
   
  ''' <summary> 
  ''' This takes an external collection and updates the found rows in the database. If a row is not found (has an ID of 0), it adds it. It will not delete any rows. Check the 'tag' of the returned collection to see if it was updated. 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function UpdateFromCollection(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault Implements ITargCCCollectionUpdateable.UpdateFromCollection 
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
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csEnumerationColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the EnumerationCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-150314-1803", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
   
  ''' <summary>
  ''' Deletes a collection of all items 
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function Delete(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific EnumType 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByEnumType(ByVal vEnumType As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumType={0}", vEnumType)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColDeleteByEnumType" 
      Dim pParametersToLog = $"EnumType: {vEnumType};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Enumeration-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedID(ByVal vIDFrom As Integer, ByVal vIDTo As Integer, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault
 
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
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific EnumType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedEnumType(ByVal vEnumTypeFrom As String, ByVal vEnumTypeTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumTypeFrom={0}, EnumTypeTo={1}", vEnumTypeFrom, vEnumTypeTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumTypeFrom 
          If vEnumTypeFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumTypeFrom) 
          ' 
          'vEnumTypeTo 
          If vEnumTypeTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumTypeTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColDeleteByBoundedEnumType" 
      Dim pParametersToLog = $"EnumType: {vEnumTypeFrom};{vEnumTypeTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific EnumTypeAndEnumValue
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedEnumTypeAndEnumValue(ByVal vEnumTypeFrom As String, ByVal vEnumTypeTo As String, ByVal vEnumValueFrom As String, ByVal vEnumValueTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumTypeFrom={0}, EnumTypeTo={1}, EnumValueFrom={2}, EnumValueTo={3}", vEnumTypeFrom, vEnumTypeTo, vEnumValueFrom, vEnumValueTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumTypeFrom 
          If vEnumTypeFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumTypeFrom) 
          ' 
          'vEnumTypeTo 
          If vEnumTypeTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumTypeTo) 
          ' 
          'vEnumValueFrom 
          If vEnumValueFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValueFrom) 
          ' 
          'vEnumValueTo 
          If vEnumValueTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValueTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColDeleteByBoundedEnumTypeAndEnumValue" 
      Dim pParametersToLog = $"EnumTypeAndEnumValue: {vEnumTypeFrom};{vEnumTypeTo};{vEnumValueFrom};{vEnumValueTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded EnumType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardEnumType(ByVal vEnumType As String, ByVal vEnumTypeWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumType={0}, EnumTypeWildcardType={1}", vEnumType, vEnumTypeWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) 
          ' 
          pBinaryWriter.Write(vEnumTypeWildcardType.FastToString())
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColDeleteByWildCardEnumType" 
      Dim pParametersToLog = $"EnumType: {vEnumType};{vEnumTypeWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded EnumTypeAndEnumValue
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardEnumTypeAndEnumValue(ByVal vEnumType As String, ByVal vEnumTypeWildcardType As clsEnums.enmWildCardType, ByVal vEnumValue As String, ByVal vEnumValueWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("EnumType={0}, EnumTypeWildcardType={1}, EnumValue={2}, EnumValueWildcardType={3}", vEnumType, vEnumTypeWildcardType.FastToString(), vEnumValue, vEnumValueWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vEnumType 
          If vEnumType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumType) 
          ' 
          pBinaryWriter.Write(vEnumTypeWildcardType.FastToString())
          'vEnumValue 
          If vEnumValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vEnumValue) 
          ' 
          pBinaryWriter.Write(vEnumValueWildcardType.FastToString())
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csEnumerationColDeleteByWildCardEnumTypeAndEnumValue" 
      Dim pParametersToLog = $"EnumTypeAndEnumValue: {vEnumType};{vEnumTypeWildcardType.FastToString()};{vEnumValue};{vEnumValueWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary> 
  ''' Used for Interface compliance. Sorts the Entity by the Primary Key (usually ID) 
  ''' </summary> 
  Public Overrides Sub SortByPrimaryKey() 
    SortByID() 
  End Sub 
 
  Public Sub SortByID()
    Me.Sort(New csEnumerationCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csEnumeration)
    Private Function Compare(ByVal x As csEnumeration, ByVal y As csEnumeration) As Integer Implements System.Collections.Generic.IComparer(Of csEnumeration).Compare
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
  
  Public Sub SortByIsSystem()
    Me.Sort(New csEnumerationCol.CompareByIsSystem)
  End Sub
  Private Class CompareByIsSystem
    Implements IComparer(Of csEnumeration)
    Private Function Compare(ByVal x As csEnumeration, ByVal y As csEnumeration) As Integer Implements System.Collections.Generic.IComparer(Of csEnumeration).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IsSystem.ToString, y.IsSystem.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEnumType()
    Me.Sort(New csEnumerationCol.CompareByEnumType)
  End Sub
  Private Class CompareByEnumType
    Implements IComparer(Of csEnumeration)
    Private Function Compare(ByVal x As csEnumeration, ByVal y As csEnumeration) As Integer Implements System.Collections.Generic.IComparer(Of csEnumeration).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EnumType, y.EnumType, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEnumValue()
    Me.Sort(New csEnumerationCol.CompareByEnumValue)
  End Sub
  Private Class CompareByEnumValue
    Implements IComparer(Of csEnumeration)
    Private Function Compare(ByVal x As csEnumeration, ByVal y As csEnumeration) As Integer Implements System.Collections.Generic.IComparer(Of csEnumeration).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EnumValue, y.EnumValue, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByText()
    Me.Sort(New csEnumerationCol.CompareByText)
  End Sub
  Private Class CompareByText
    Implements IComparer(Of csEnumeration)
    Private Function Compare(ByVal x As csEnumeration, ByVal y As csEnumeration) As Integer Implements System.Collections.Generic.IComparer(Of csEnumeration).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Text, y.Text, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTextLocalized()
    Me.Sort(New csEnumerationCol.CompareByTextLocalized)
  End Sub
  Private Class CompareByTextLocalized
    Implements IComparer(Of csEnumeration)
    Private Function Compare(ByVal x As csEnumeration, ByVal y As csEnumeration) As Integer Implements System.Collections.Generic.IComparer(Of csEnumeration).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TextLocalized, y.TextLocalized, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csEnumerationCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csEnumeration)
    Private Function Compare(ByVal x As csEnumeration, ByVal y As csEnumeration) As Integer Implements System.Collections.Generic.IComparer(Of csEnumeration).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Tag, y.Tag, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Integer) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Integer, csEnumeration) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByEnumTypeAndEnumValue = New Dictionary(Of String, csEnumeration)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByEnumTypeAndEnumValue = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Integer) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Integer, csEnumeration) 
    _SortedDictionaryForFindByEnumTypeAndEnumValue = New Dictionary(Of String, csEnumeration)(StringComparer.OrdinalIgnoreCase) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      _IsLocalized = False 
      _LocalizedLanguage = clsEnums.enmLanguage.UD 
      bHasParents = False 
      bHasLocalizedFields = True 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
