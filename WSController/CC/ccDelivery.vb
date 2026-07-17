Public Class clsDelivery
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
 
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
    [OrderHeader] 
    [DeliveryMethod] 
    [DeliveryStatus] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [OrderHeader] 
    [DeliveryAddress] 
    [ContactPhone] 
    [ContactName] 
    [DeliveryMethod] 
    [OrderedDate] 
    [ReceivedDate] 
    [ArrivalToHubDate] 
    [ArrivalToCustomerDate] 
    [DeliveryStatus] 
    [Location] 
    [ProductsSummary] 
    [Notes] 
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
  Private _WithParents As clsEnums.enmLoadParent
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
    End Get
  End Property
  
  Private _ID As Long
  Private _OrderHeaderID As Long
  Private _OrderHeader As clsOrderHeader
  Private _OrderHeaderText As String
  Private _DeliveryAddress As String
  Private _ContactPhone As String
  Private _ContactName As String
  Private _DeliveryMethod As clsEnums.enmDeliveryMethod
  Private _DeliveryMethodText As String 
  Private _OrderedDate As Date
  Private _ReceivedDate As Date
  Private _ArrivalToHubDate As Date
  Private _ArrivalToCustomerDate As Date
  Private _DeliveryStatus As clsEnums.enmDeliveryStatus
  Private _DeliveryStatusText As String 
  Private _Location As String
  Private _ProductsSummary As String
  Private _Notes As String
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
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [OrderHeaderID]() As Long
    Get
      Return Me._OrderHeaderID
    End Get
    Set(ByVal value As Long)
      If Me._OrderHeaderID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OrderHeaderID = value 
      End If 
    End Set
  End Property
  Public Property [OrderHeader]() As clsOrderHeader
    Get
      Return Me._OrderHeader
    End Get
    Set(ByVal value As clsOrderHeader)
      Me._OrderHeader = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the OrderHeader object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property OrderHeaderText() As String
    Get
      Return Me._OrderHeaderText
    End Get
    Set(ByVal value As String)
      Me._OrderHeaderText = value
    End Set
  End Property
  Public Property [DeliveryAddress]() As String
    Get
      Return Me._DeliveryAddress
    End Get
    Set(ByVal value As String)
      If Me._DeliveryAddress <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DeliveryAddress = value 
      End If 
    End Set
  End Property
  Public Property [ContactPhone]() As String
    Get
      Return Me._ContactPhone
    End Get
    Set(ByVal value As String)
      If Me._ContactPhone <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ContactPhone = value 
      End If 
    End Set
  End Property
  Public Property [ContactName]() As String
    Get
      Return Me._ContactName
    End Get
    Set(ByVal value As String)
      If Me._ContactName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ContactName = value 
      End If 
    End Set
  End Property
  Public Property [DeliveryMethod]() As clsEnums.enmDeliveryMethod
    Get
      Return Me._DeliveryMethod
    End Get
    Set(ByVal value As clsEnums.enmDeliveryMethod)
      If Me._DeliveryMethod <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DeliveryMethod = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [DeliveryMethodText]() As String
    Get
      Return Me._DeliveryMethodText
    End Get
    Set(ByVal value As String)
      Me._DeliveryMethodText = value
    End Set
  End Property
  Public Property [OrderedDate]() As Date
    Get
      Return Me._OrderedDate
    End Get
    Set(ByVal value As Date)
      If Me._OrderedDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OrderedDate = value 
      End If 
    End Set
  End Property
  Public Property [ReceivedDate]() As Date
    Get
      Return Me._ReceivedDate
    End Get
    Set(ByVal value As Date)
      If Me._ReceivedDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ReceivedDate = value 
      End If 
    End Set
  End Property
  Public Property [ArrivalToHubDate]() As Date
    Get
      Return Me._ArrivalToHubDate
    End Get
    Set(ByVal value As Date)
      If Me._ArrivalToHubDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ArrivalToHubDate = value 
      End If 
    End Set
  End Property
  Public Property [ArrivalToCustomerDate]() As Date
    Get
      Return Me._ArrivalToCustomerDate
    End Get
    Set(ByVal value As Date)
      If Me._ArrivalToCustomerDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ArrivalToCustomerDate = value 
      End If 
    End Set
  End Property
  Public Property [DeliveryStatus]() As clsEnums.enmDeliveryStatus
    Get
      Return Me._DeliveryStatus
    End Get
    Set(ByVal value As clsEnums.enmDeliveryStatus)
      If Me._DeliveryStatus <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DeliveryStatus = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [DeliveryStatusText]() As String
    Get
      Return Me._DeliveryStatusText
    End Get
    Set(ByVal value As String)
      Me._DeliveryStatusText = value
    End Set
  End Property
  Public Property [Location]() As String
    Get
      Return Me._Location
    End Get
    Set(ByVal value As String)
      If Me._Location <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Location = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [ProductsSummary]() As String
    Get
      Return Me._ProductsSummary
    End Get
  End Property
  Public Property [Notes]() As String
    Get
      Return Me._Notes
    End Get
    Set(ByVal value As String)
      If Me._Notes <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Notes = value 
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
    If pOverridenValue = Nothing Then bDefaultDesignation = _ID.ToString() Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _OrderHeaderID <> 0 Then pValue.Append("OrderHeaderID='" & _OrderHeaderID.ToString() & "' ‡ ") 
    If _OrderHeaderText <> "" Then pValue.Append("OrderHeaderText='" & _OrderHeaderText & "' ‡ ") 
    If _DeliveryAddress <> "" Then pValue.Append("DeliveryAddress='" & _DeliveryAddress & "' ‡ ") 
    If _ContactPhone <> "" Then pValue.Append("ContactPhone='" & _ContactPhone & "' ‡ ") 
    If _ContactName <> "" Then pValue.Append("ContactName='" & _ContactName & "' ‡ ") 
    If _DeliveryMethod <> clsEnums.enmDeliveryMethod.UD Then pValue.Append("DeliveryMethod='" & _DeliveryMethod.FastToString() & "' ‡ ") 
    If _DeliveryMethodText <> "" Then pValue.Append("DeliveryMethodText='" & _DeliveryMethodText & "' ‡ ") 
    If Not (_OrderedDate = Nothing) Then pValue.Append("OrderedDate='" & _OrderedDate.ToString("o") & "' ‡ ") 
    If Not (_ReceivedDate = Nothing) Then pValue.Append("ReceivedDate='" & _ReceivedDate.ToString("o") & "' ‡ ") 
    If Not (_ArrivalToHubDate = Nothing) Then pValue.Append("ArrivalToHubDate='" & _ArrivalToHubDate.ToString("o") & "' ‡ ") 
    If Not (_ArrivalToCustomerDate = Nothing) Then pValue.Append("ArrivalToCustomerDate='" & _ArrivalToCustomerDate.ToString("o") & "' ‡ ") 
    If _DeliveryStatus <> clsEnums.enmDeliveryStatus.UD Then pValue.Append("DeliveryStatus='" & _DeliveryStatus.FastToString() & "' ‡ ") 
    If _DeliveryStatusText <> "" Then pValue.Append("DeliveryStatusText='" & _DeliveryStatusText & "' ‡ ") 
    If _Location <> "" Then pValue.Append("Location='" & _Location & "' ‡ ") 
    If _ProductsSummary <> "" Then pValue.Append("ProductsSummary='" & _ProductsSummary & "' ‡ ") 
    If _Notes <> "" Then pValue.Append("Notes='" & _Notes & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _OrderHeaderID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_OrderHeaderText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryAddress)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ContactPhone)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ContactName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryMethod.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryMethodText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OrderedDate.ToShortDateString & " " & _OrderedDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ReceivedDate.ToShortDateString & " " & _ReceivedDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ArrivalToHubDate.ToShortDateString & " " & _ArrivalToHubDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ArrivalToCustomerDate.ToShortDateString & " " & _ArrivalToCustomerDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryStatus.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryStatusText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Location)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProductsSummary)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Notes)}""") 
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
  
  Public Sub New(ByVal vclsDelivery As clsDelivery)
    MyBase.New()
    CreateEmpty()
    AssignValues(vclsDelivery) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vOrderHeaderID As Long = 0 _ 
    , Optional vOrderHeaderText As String = "" _ 
    , Optional vDeliveryAddress As String = "" _ 
    , Optional vContactPhone As String = "" _ 
    , Optional vContactName As String = "" _ 
    , Optional vDeliveryMethod As clsEnums.enmDeliveryMethod = clsEnums.enmDeliveryMethod.UD _ 
    , Optional vDeliveryMethodText As String = "" _ 
    , Optional vOrderedDate As Date = Nothing _ 
    , Optional vReceivedDate As Date = Nothing _ 
    , Optional vArrivalToHubDate As Date = Nothing _ 
    , Optional vArrivalToCustomerDate As Date = Nothing _ 
    , Optional vDeliveryStatus As clsEnums.enmDeliveryStatus = clsEnums.enmDeliveryStatus.Pending _ 
    , Optional vDeliveryStatusText As String = "" _ 
    , Optional vLocation As String = "" _ 
    , Optional vProductsSummary As String = "" _ 
    , Optional vNotes As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _OrderHeaderID = vOrderHeaderID 
    _OrderHeaderText = vOrderHeaderText 
    _DeliveryAddress = vDeliveryAddress 
    _ContactPhone = vContactPhone 
    _ContactName = vContactName 
    _DeliveryMethod = vDeliveryMethod 
    _DeliveryMethodText = vDeliveryMethodText 
    _OrderedDate = vOrderedDate 
    _ReceivedDate = vReceivedDate 
    _ArrivalToHubDate = vArrivalToHubDate 
    _ArrivalToCustomerDate = vArrivalToCustomerDate 
    _DeliveryStatus = vDeliveryStatus 
    _DeliveryStatusText = vDeliveryStatusText 
    _Location = vLocation 
    _ProductsSummary = vProductsSummary 
    _Notes = vNotes 
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
 
    _DeliveryAddress = _DeliveryAddress.Truncate(pTruncateLength, _IsTruncated) 
    _ContactPhone = _ContactPhone.Truncate(pTruncateLength, _IsTruncated) 
    _ContactName = _ContactName.Truncate(pTruncateLength, _IsTruncated) 
    _Location = _Location.Truncate(pTruncateLength, _IsTruncated) 
    _ProductsSummary = _ProductsSummary.Truncate(pTruncateLength, _IsTruncated) 
    _Notes = _Notes.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Delivery by PrimaryKey (ID) 
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
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Delivery-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Delivery by the chosen parameters. This function may be a bit slower than accessing the Delivery's GetBy... directly 
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
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Delivery-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Delivery-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the Delivery by ID. 
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
      Dim pFunction As String = "clsDeliveryGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the Delivery 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150308-1015", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Delivery-151227-1738", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Delivery-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the Delivery. If there are parents or children in the Delivery, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = String.Format("Delivery.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pDelivery As New clsDelivery 
    If Me.isEqual(pDelivery) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-Delivery-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Delivery-240611-135714", vRequester) 
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
      Dim pFunction As String = "clsDeliveryUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150314-1803", vRequester) 
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
    Dim pFunctionParameters As String = String.Format("Delivery.ID={0}", _ID)
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
      Dim pFunction As String = "clsDeliveryDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150314-1803", vRequester) 
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
  Public Shared Function DeleteByID(vID As Long, vRequester As clsRequester) As clsFault 
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
      Dim pFunction As String = "clsDeliveryDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-Delivery-231207-1707", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is clsDelivery) Then Return False 
    Dim pDeliveryToTest As clsDelivery = CType(vTargCCEntityToTest, clsDelivery) 
    Return isEqual(pDeliveryToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vDeliveryToTest As clsDelivery) As Boolean
    With vDeliveryToTest
      If _ID <> .ID Then Return False
      If _OrderHeaderID <> .OrderHeaderID Then Return False
      If _DeliveryAddress <> .DeliveryAddress Then Return False
      If _ContactPhone <> .ContactPhone Then Return False
      If _ContactName <> .ContactName Then Return False
      If _DeliveryMethod <> .DeliveryMethod Then Return False
      If _OrderedDate <> Nothing AndAlso .OrderedDate <> Nothing Then 
        If ccHelper.ToLong(_OrderedDate.Subtract(.OrderedDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_OrderedDate = Nothing AndAlso .OrderedDate = Nothing) Then 
        Return False 
      End If 
      If _ReceivedDate <> Nothing AndAlso .ReceivedDate <> Nothing Then 
        If ccHelper.ToLong(_ReceivedDate.Subtract(.ReceivedDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_ReceivedDate = Nothing AndAlso .ReceivedDate = Nothing) Then 
        Return False 
      End If 
      If _ArrivalToHubDate <> Nothing AndAlso .ArrivalToHubDate <> Nothing Then 
        If ccHelper.ToLong(_ArrivalToHubDate.Subtract(.ArrivalToHubDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_ArrivalToHubDate = Nothing AndAlso .ArrivalToHubDate = Nothing) Then 
        Return False 
      End If 
      If _ArrivalToCustomerDate <> Nothing AndAlso .ArrivalToCustomerDate <> Nothing Then 
        If ccHelper.ToLong(_ArrivalToCustomerDate.Subtract(.ArrivalToCustomerDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_ArrivalToCustomerDate = Nothing AndAlso .ArrivalToCustomerDate = Nothing) Then 
        Return False 
      End If 
      If _DeliveryStatus <> .DeliveryStatus Then Return False
      If _Location <> .Location Then Return False
      If _ProductsSummary <> .ProductsSummary Then Return False
      If _Notes <> .Notes Then Return False
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
    Dim pClone As New clsDelivery(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsDelivery
    Dim pClone As New clsDelivery(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderHeaderID") = _OrderHeaderID : Catch ex As Exception : Return pFault.LogException(ex, "OrderHeaderID", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("DeliveryAddress") = _DeliveryAddress : Catch ex As Exception : Return pFault.LogException(ex, "DeliveryAddress", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("ContactPhone") = _ContactPhone : Catch ex As Exception : Return pFault.LogException(ex, "ContactPhone", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("ContactName") = _ContactName : Catch ex As Exception : Return pFault.LogException(ex, "ContactName", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("DeliveryMethod") = _DeliveryMethod : Catch ex As Exception : Return pFault.LogException(ex, "DeliveryMethod", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderedDate") = _OrderedDate : Catch ex As Exception : Return pFault.LogException(ex, "OrderedDate", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("ReceivedDate") = _ReceivedDate : Catch ex As Exception : Return pFault.LogException(ex, "ReceivedDate", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("ArrivalToHubDate") = _ArrivalToHubDate : Catch ex As Exception : Return pFault.LogException(ex, "ArrivalToHubDate", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("ArrivalToCustomerDate") = _ArrivalToCustomerDate : Catch ex As Exception : Return pFault.LogException(ex, "ArrivalToCustomerDate", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("DeliveryStatus") = _DeliveryStatus : Catch ex As Exception : Return pFault.LogException(ex, "DeliveryStatus", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("Location") = _Location : Catch ex As Exception : Return pFault.LogException(ex, "Location", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductsSummary") = _ProductsSummary : Catch ex As Exception : Return pFault.LogException(ex, "ProductsSummary", "TRGT-Delivery-130316-0852", vRequester) : End Try 
    Try : vDataRow("Notes") = _Notes : Catch ex As Exception : Return pFault.LogException(ex, "Notes", "TRGT-Delivery-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pDelivery As clsDelivery = CType(pXmlSerializer.Deserialize(pStreamReader), clsDelivery) 
      AssignValues(pDelivery) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-Delivery-130515-1230", vRequester) 
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
          'OrderHeaderID 
          pBinaryWriter.Write(_OrderHeaderID) 
          'OrderHeader 
          If _OrderHeader IsNot Nothing Then 
            pObjectBytes = _OrderHeader.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _OrderHeaderText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_OrderHeaderText) 
          'DeliveryAddress 
          If _DeliveryAddress Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_DeliveryAddress) 
          'ContactPhone 
          If _ContactPhone Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ContactPhone) 
          'ContactName 
          If _ContactName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ContactName) 
          'DeliveryMethod 
          pBinaryWriter.Write(_DeliveryMethod.FastToString()) 
          'OrderedDate 
          pBinaryWriter.Write(_OrderedDate.Ticks) 
          'ReceivedDate 
          pBinaryWriter.Write(_ReceivedDate.Ticks) 
          'ArrivalToHubDate 
          pBinaryWriter.Write(_ArrivalToHubDate.Ticks) 
          'ArrivalToCustomerDate 
          pBinaryWriter.Write(_ArrivalToCustomerDate.Ticks) 
          'DeliveryStatus 
          pBinaryWriter.Write(_DeliveryStatus.FastToString()) 
          'Location 
          If _Location Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Location) 
          'ProductsSummary 
          If _ProductsSummary Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProductsSummary) 
          'Notes 
          If _Notes Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Notes) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-150307-2338", vRequester) 
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
          'OrderHeaderID 
          _OrderHeaderID = pReader.ReadInt64 
          'OrderHeader 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _OrderHeader = New clsOrderHeader(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _OrderHeaderText = pReader.ReadString 
          'DeliveryAddress 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _DeliveryAddress = pReader.ReadString 
          'ContactPhone 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ContactPhone = pReader.ReadString 
          'ContactName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ContactName = pReader.ReadString 
          'DeliveryMethod 
          _DeliveryMethod = clsEnums.TranslateEnmDeliveryMethod(pReader.ReadString) 
          'OrderedDate 
          _OrderedDate = New Date(pReader.ReadInt64) 
          'ReceivedDate 
          _ReceivedDate = New Date(pReader.ReadInt64) 
          'ArrivalToHubDate 
          _ArrivalToHubDate = New Date(pReader.ReadInt64) 
          'ArrivalToCustomerDate 
          _ArrivalToCustomerDate = New Date(pReader.ReadInt64) 
          'DeliveryStatus 
          _DeliveryStatus = clsEnums.TranslateEnmDeliveryStatus(pReader.ReadString) 
          'Location 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Location = pReader.ReadString 
          'ProductsSummary 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProductsSummary = pReader.ReadString 
          'Notes 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Notes = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-Delivery-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-190720-1443", vRequester) 
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
 
      Dim pDelivery As clsDelivery = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsDelivery)(vJSON, pSettings) 
      AssignValues(pDelivery) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vDelivery As clsDelivery)
    With vDelivery
      _ID = .ID 
      _OrderHeaderID = .OrderHeaderID 
      If .OrderHeader IsNot Nothing Then 
        _OrderHeader = .OrderHeader.Clone() 
      End If 
      _OrderHeaderText = .OrderHeaderText 
      _DeliveryAddress = .DeliveryAddress 
      _ContactPhone = .ContactPhone 
      _ContactName = .ContactName 
      _DeliveryMethod = .DeliveryMethod 
      _DeliveryMethodText = .DeliveryMethodText
      _OrderedDate = .OrderedDate 
      _ReceivedDate = .ReceivedDate 
      _ArrivalToHubDate = .ArrivalToHubDate 
      _ArrivalToCustomerDate = .ArrivalToCustomerDate 
      _DeliveryStatus = .DeliveryStatus 
      _DeliveryStatusText = .DeliveryStatusText
      _Location = .Location 
      _ProductsSummary = .ProductsSummary 
      _Notes = .Notes 
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
 
    Dim pTextToGet As String = "" 
    Try 
      'DeliveryMethod 
      pTextToGet = "DeliveryMethodText (Enum)" 
      _DeliveryMethodText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.DeliveryMethod, _DeliveryMethod.FastToString(), vRequester) 
      'DeliveryStatus 
      pTextToGet = "DeliveryStatusText (Enum)" 
      _DeliveryStatusText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.DeliveryStatus, _DeliveryStatus.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-Delivery-151124-1900", vRequester) 
    End Try 
 
    Return pFault 
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
      Dim pFunction As String = "clsDeliveryLoadParents" 
      Dim pParametersToLog = $"ID: {_ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150411-1107", vRequester) 
    End Try 
    
    pFault.SetOK() 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  Private Sub CreateEmpty()
    
    _ID = 0 
    _OrderHeaderID = 0
    _OrderHeader = Nothing
    _OrderHeaderText = "."
    _DeliveryAddress = ""
    _ContactPhone = ""
    _ContactName = ""
    _DeliveryMethod = clsEnums.enmDeliveryMethod.UD
    _DeliveryMethodText = ""
    _OrderedDate = Nothing
    _ReceivedDate = Nothing
    _ArrivalToHubDate = Nothing
    _ArrivalToCustomerDate = Nothing
    'Default Value set by SQL Server Database (below): Pending
    _DeliveryStatus = clsEnums.enmDeliveryStatus.Pending
    _DeliveryStatusText = ""
    _Location = ""
    _ProductsSummary = ""
    _Notes = ""
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
  
Public Class clsDeliveryCol
  Inherits cTargCCCollection(Of clsDelivery)
  Implements ITargCCCollectionUpdateable 
  
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsDelivery) 
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
 
    For Each pRow As clsDelivery In Me 
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
    pCSVTitle.Append(",""OrderHeaderID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""OrderHeader (Text)""") 
    pCSVTitle.Append(",""DeliveryAddress""") 
    pCSVTitle.Append(",""ContactPhone""") 
    pCSVTitle.Append(",""ContactName""") 
    pCSVTitle.Append(",""DeliveryMethod" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""DeliveryMethod (Text)""") 
    pCSVTitle.Append(",""OrderedDate""") 
    pCSVTitle.Append(",""ReceivedDate""") 
    pCSVTitle.Append(",""ArrivalToHubDate""") 
    pCSVTitle.Append(",""ArrivalToCustomerDate""") 
    pCSVTitle.Append(",""DeliveryStatus" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""DeliveryStatus (Text)""") 
    pCSVTitle.Append(",""Location""") 
    pCSVTitle.Append(",""ProductsSummary""") 
    pCSVTitle.Append(",""Notes""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As clsDelivery In Me 
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
 
  Public Overloads Sub Add(ByVal vDelivery As clsDelivery) 
    SyncLock _CollectionLock 
      MyBase.Add(vDelivery) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vDelivery As clsDelivery) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vDelivery) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vDeliveryCol As clsDeliveryCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vDeliveryCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vDelivery As clsDelivery) 
    SyncLock _CollectionLock 
      MyBase.Remove(vDelivery) 
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
      Dim pTempDictionary As New Dictionary(Of Long, clsDelivery) 
      
      For Each lDelivery In Me 
        If lDelivery.IsEmpty OrElse pTempDictionary.ContainsKey(lDelivery.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lDelivery.ID, lDelivery) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lDelivery.ToString, "TRGT-Delivery-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", Delivery:" & lDelivery.ToString() & ", TRGT-Delivery-260111-154657") 'Send it up the line 
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
 
    For Each lDelivery As clsDelivery In Me 
      lDelivery.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [ArrivalToCustomerDate] 
    [OrderHeaderID] 
    [DeliveryStatus] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the Deliverys by the chosen parameters. This function may be a bit slower than accessing the Delivery's FillBy... directly 
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
        Case enmFillByParameterCombination.ArrivalToCustomerDate 
          pFault = FillByArrivalToCustomerDate(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OrderHeaderID 
          pFault = FillByOrderHeaderID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.DeliveryStatus 
          pFault = FillByDeliveryStatus(clsEnums.TranslateEnmDeliveryStatus(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Delivery-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Delivery-151223_1716", vRequester) 
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
 
      Dim pFunction As String = "clsDeliveryColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ArrivalToCustomerDate, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByArrivalToCustomerDate(ByVal vArrivalToCustomerDate As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ArrivalToCustomerDate={0}", vArrivalToCustomerDate)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vArrivalToCustomerDate 
          pBinaryWriter.Write(vArrivalToCustomerDate.Ticks) 
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
 
      Dim pFunction As String = "clsDeliveryColFillByArrivalToCustomerDate" 
      Dim pParametersToLog = $"ArrivalToCustomerDate: {vArrivalToCustomerDate};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery  
      If vAppend = True Then 
        Dim pDeliverys As New clsDeliveryCol 
        pDeliverys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pDeliverys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OrderHeaderID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOrderHeaderID(ByVal vOrderHeaderID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderHeaderID={0}", vOrderHeaderID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderHeaderID 
          pBinaryWriter.Write(vOrderHeaderID) 
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
 
      Dim pFunction As String = "clsDeliveryColFillByOrderHeaderID" 
      Dim pParametersToLog = $"OrderHeaderID: {vOrderHeaderID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery  
      If vAppend = True Then 
        Dim pDeliverys As New clsDeliveryCol 
        pDeliverys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pDeliverys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific DeliveryStatus, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByDeliveryStatus(ByVal vDeliveryStatus As clsEnums.enmDeliveryStatus, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("DeliveryStatus={0}", vDeliveryStatus)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vDeliveryStatus 
          pBinaryWriter.Write(vDeliveryStatus.ToString()) 
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
 
      Dim pFunction As String = "clsDeliveryColFillByDeliveryStatus" 
      Dim pParametersToLog = $"DeliveryStatus: {vDeliveryStatus};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery  
      If vAppend = True Then 
        Dim pDeliverys As New clsDeliveryCol 
        pDeliverys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pDeliverys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "clsDeliveryColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery  
      If vAppend = True Then 
        Dim pDeliverys As New clsDeliveryCol 
        pDeliverys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pDeliverys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ArrivalToCustomerDate, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedArrivalToCustomerDate(ByVal vArrivalToCustomerDateStart As Date, ByVal vArrivalToCustomerDateEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ArrivalToCustomerDateStart={0}, ArrivalToCustomerDateEnd={1}", vArrivalToCustomerDateStart, vArrivalToCustomerDateEnd)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vArrivalToCustomerDateStart 
          pBinaryWriter.Write(vArrivalToCustomerDateStart.Ticks) 
          ' 
          'vArrivalToCustomerDateEnd 
          pBinaryWriter.Write(vArrivalToCustomerDateEnd.Ticks) 
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
 
      Dim pFunction As String = "clsDeliveryColFillByBoundedArrivalToCustomerDate" 
      Dim pParametersToLog = $"ArrivalToCustomerDate: {vArrivalToCustomerDateStart};{vArrivalToCustomerDateEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery  
      If vAppend = True Then 
        Dim pDeliverys As New clsDeliveryCol 
        pDeliverys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pDeliverys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "clsDeliveryColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery   
      If vAppend = True Then 
        Dim pDeliverys As New clsDeliveryCol 
        pDeliverys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pDeliverys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-231207-1750", vRequester) 
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
    [OrderHeaderID]
    ArrivalToCustomerDateStart
    ArrivalToCustomerDateEnd
    [DeliveryStatus]
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
    Dim pOrderHeaderID As Nullable(Of Long) = Nothing
    Dim pArrivalToCustomerDateStart As Nullable(Of Date) = Nothing
    Dim pArrivalToCustomerDateEnd As Nullable(Of Date) = Nothing
    Dim pDeliveryStatus As clsEnums.enmDeliveryStatus = clsEnums.enmDeliveryStatus.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderID) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderID) : If pObj IsNot Nothing Then pOrderHeaderID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ArrivalToCustomerDateStart) Then pObj = vParameters(enmFillOnTheFlyParameters.ArrivalToCustomerDateStart) : If pObj IsNot Nothing Then pArrivalToCustomerDateStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ArrivalToCustomerDateEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.ArrivalToCustomerDateEnd) : If pObj IsNot Nothing Then pArrivalToCustomerDateEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DeliveryStatus) Then pObj = vParameters(enmFillOnTheFlyParameters.DeliveryStatus) : If pObj IsNot Nothing Then pDeliveryStatus = CType(pObj, clsEnums.enmDeliveryStatus) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pOrderHeaderID _
        , pArrivalToCustomerDateStart, pArrivalToCustomerDateEnd _
        , pDeliveryStatus _
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
        , ByVal vOrderHeaderID As Nullable(Of Long) _
        , ByVal vArrivalToCustomerDateStart As Nullable(Of Date), ByVal vArrivalToCustomerDateEnd As Nullable(Of Date) _
        , ByVal vDeliveryStatus As clsEnums.enmDeliveryStatus _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OrderHeaderID={2}, ArrivalToCustomerDateStart={3}, ArrivalToCustomerDateEnd={4}, DeliveryStatus={5}", vIDFrom, vIDTo, vOrderHeaderID, vArrivalToCustomerDateStart, vArrivalToCustomerDateEnd, vDeliveryStatus)
    
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
          'OrderHeaderID 
          pBinaryWriter.Write(vOrderHeaderID.HasValue) 
          If vOrderHeaderID.HasValue = True Then pBinaryWriter.Write(vOrderHeaderID.Value) : pParametersToLog &= $"OrderHeaderID={vOrderHeaderID};"  
          'ArrivalToCustomerDate 
          pBinaryWriter.Write(vArrivalToCustomerDateStart.HasValue) 
          If vArrivalToCustomerDateStart.HasValue Then pBinaryWriter.Write(vArrivalToCustomerDateStart.Value.Ticks) : pParametersToLog &= $"ArrivalToCustomerDateStart={vArrivalToCustomerDateStart.Value};"  
          pBinaryWriter.Write(vArrivalToCustomerDateEnd.HasValue) 
          If vArrivalToCustomerDateEnd.HasValue Then pBinaryWriter.Write(vArrivalToCustomerDateEnd.Value.Ticks) : pParametersToLog &= $"ArrivalToCustomerDateEnd={vArrivalToCustomerDateEnd.Value};"  
          'DeliveryStatus 
          pBinaryWriter.Write(vDeliveryStatus.ToString()) : pParametersToLog &= $"DeliveryStatus={vDeliveryStatus};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsDeliveryColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByOrderHeaderID
    GroupByArrivalToCustomerDate
    GroupByDeliveryStatus
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
    Dim pOrderHeaderID As Nullable(Of Long) = Nothing
    Dim pArrivalToCustomerDateStart As Nullable(Of Date) = Nothing
    Dim pArrivalToCustomerDateEnd As Nullable(Of Date) = Nothing
    Dim pDeliveryStatus As clsEnums.enmDeliveryStatus = clsEnums.enmDeliveryStatus.UD
    Dim pGroupByOrderHeaderID As Boolean = False
    Dim pGroupByArrivalToCustomerDate As Boolean = False
    Dim pGroupByDeliveryStatus As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderID) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderID) : If pObj IsNot Nothing Then pOrderHeaderID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ArrivalToCustomerDateStart) Then pObj = vParameters(enmFillOnTheFlyParameters.ArrivalToCustomerDateStart) : If pObj IsNot Nothing Then pArrivalToCustomerDateStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ArrivalToCustomerDateEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.ArrivalToCustomerDateEnd) : If pObj IsNot Nothing Then pArrivalToCustomerDateEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DeliveryStatus) Then pObj = vParameters(enmFillOnTheFlyParameters.DeliveryStatus) : If pObj IsNot Nothing Then pDeliveryStatus = CType(pObj, clsEnums.enmDeliveryStatus) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOrderHeaderID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOrderHeaderID) : If pObj IsNot Nothing Then pGroupByOrderHeaderID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByArrivalToCustomerDate) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByArrivalToCustomerDate) : If pObj IsNot Nothing Then pGroupByArrivalToCustomerDate = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByDeliveryStatus) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByDeliveryStatus) : If pObj IsNot Nothing Then pGroupByDeliveryStatus = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pOrderHeaderID _
        , pArrivalToCustomerDateStart, pArrivalToCustomerDateEnd _
        , pDeliveryStatus _
        , pGroupByOrderHeaderID _
        , pGroupByArrivalToCustomerDate _
        , pGroupByDeliveryStatus _
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
        , ByVal vOrderHeaderID As Nullable(Of Long) _
        , ByVal vArrivalToCustomerDateStart As Nullable(Of Date), ByVal vArrivalToCustomerDateEnd As Nullable(Of Date) _
        , ByVal vDeliveryStatus As clsEnums.enmDeliveryStatus _
        , ByVal vGroupByOrderHeaderID As Boolean _
        , ByVal vGroupByArrivalToCustomerDate As Boolean _
        , ByVal vGroupByDeliveryStatus As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OrderHeaderID={2}, ArrivalToCustomerDateStart={3}, ArrivalToCustomerDateEnd={4}, DeliveryStatus={5}, GroupByOrderHeaderID={6}, GroupByArrivalToCustomerDate={7}, GroupByDeliveryStatus={8}", vIDFrom, vIDTo, vOrderHeaderID, vArrivalToCustomerDateStart, vArrivalToCustomerDateEnd, vDeliveryStatus, vGroupByOrderHeaderID, vGroupByArrivalToCustomerDate, vGroupByDeliveryStatus)
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
          'OrderHeaderID 
          pBinaryWriter.Write(vOrderHeaderID.HasValue) 
          If vOrderHeaderID.HasValue = True Then pBinaryWriter.Write(vOrderHeaderID.Value) : pParametersToLog &= $"OrderHeaderID={vOrderHeaderID};"  
          'ArrivalToCustomerDate 
          pBinaryWriter.Write(vArrivalToCustomerDateStart.HasValue) 
          If vArrivalToCustomerDateStart.HasValue Then pBinaryWriter.Write(vArrivalToCustomerDateStart.Value.Ticks) : pParametersToLog &= $"ArrivalToCustomerDateStart={vArrivalToCustomerDateStart};"  
          pBinaryWriter.Write(vArrivalToCustomerDateEnd.HasValue) 
          If vArrivalToCustomerDateEnd.HasValue Then pBinaryWriter.Write(vArrivalToCustomerDateEnd.Value.Ticks) : pParametersToLog &= $"ArrivalToCustomerDateEnd={vArrivalToCustomerDateEnd};"  
          'DeliveryStatus 
          pBinaryWriter.Write(vDeliveryStatus.ToString()) : pParametersToLog &= $"DeliveryStatus={vDeliveryStatus};"  
          pBinaryWriter.Write(vGroupByOrderHeaderID) : pParametersToLog &= $"GroupByOrderHeaderID={vGroupByOrderHeaderID};"  
          pBinaryWriter.Write(vGroupByArrivalToCustomerDate) : pParametersToLog &= $"GroupByArrivalToCustomerDate={vGroupByArrivalToCustomerDate};"  
          pBinaryWriter.Write(vGroupByDeliveryStatus) : pParametersToLog &= $"GroupByDeliveryStatus={vGroupByDeliveryStatus};"  
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsDeliveryColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Delivery  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vDeliveryArray As clsDelivery())
    Me.Clear()
    
    For Each pDelivery As clsDelivery In vDeliveryArray
      Me.Add(pDelivery)
      _Clean.Add(pDelivery.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pDelivery As New clsDelivery(pRow, vRequester, _WithParents) 
        Me.Add(pDelivery) 
        _Clean.Add(pDelivery.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-DeliveryCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-130515-1300", vRequester) 
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
      Dim pDeliverys As clsDeliveryCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsDeliveryCol) 
      For Each pDelivery As clsDelivery In pDeliverys 
        Me.Add(pDelivery) 
        _Clean.Add(pDelivery.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-Delivery-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-190720-1443", vRequester) 
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
 
      Dim pDeliverys As List(Of clsDelivery) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of clsDelivery))(vJSON, pSettings) 
      For Each pDelivery As clsDelivery In pDeliverys 
        Me.Add(pDelivery) 
        _Clean.Add(pDelivery.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-190720-2059", vRequester) 
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
          For Each lDelivery As clsDelivery In Me 
            Dim pByte As Byte() = lDelivery.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-150307-2340", vRequester) 
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
            Dim pDelivery As clsDelivery = New clsDelivery(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pDelivery) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pDelivery.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-Delivery-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pDelivery As clsDelivery In Me 
      With pDelivery 
        pFault = pDelivery.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is clsDeliveryCol) Then Return False 
    Dim pDeliveryColToTest As clsDeliveryCol = CType(vEntitiesToTest, clsDeliveryCol) 
    Return isEqual(pDeliveryColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vDeliverysToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vDeliverysToTest As clsDeliveryCol) As Boolean
    If Me.Count <> vDeliverysToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vDeliverysToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pDeliverys._FilledFromSumOnTheFly = True
    
    For Each pDelivery As clsDelivery In Me 
      Dim pDeliveryClone As clsDelivery = pDelivery.Clone() 
      pDeliverys.Add(pDeliveryClone) 
      If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
    Next 
    Return pDeliverys 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsDeliveryCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pDeliverys._FilledFromSumOnTheFly = True
    
    For Each pDelivery As clsDelivery In Me
      Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
      pDeliverys.Add(pDeliveryClone)
      If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
    Next
    Return pDeliverys
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As clsDeliveryCol 
    Dim pDeliverys As New clsDeliveryCol(_WithParents)  
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pDelivery As clsDelivery In _SortedDictionaryForFindByID.Values.ToList() 
      If (pDelivery.ID > vIDFrom AndAlso pDelivery.ID <= vIDTo) Then 
        Dim pDeliveryClone As clsDelivery = pDelivery.Clone() 
        pDeliverys.Add(pDeliveryClone) 
        If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
      End If 
    Next 
    Return pDeliverys 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ArrivalToCustomerDate (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedArrivalToCustomerDate(ByVal vArrivalToCustomerDateStart As Date, ByVal vArrivalToCustomerDateEnd As Date) As clsDeliveryCol 
    Dim pDeliverys As New clsDeliveryCol(_WithParents)  
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pDelivery As clsDelivery In _SortedDictionaryForFindByID.Values.ToList() 
      If (pDelivery.ArrivalToCustomerDate > vArrivalToCustomerDateStart AndAlso pDelivery.ArrivalToCustomerDate <= vArrivalToCustomerDateEnd) Then 
        Dim pDeliveryClone As clsDelivery = pDelivery.Clone() 
        pDeliverys.Add(pDeliveryClone) 
        If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
      End If 
    Next 
    Return pDeliverys 
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
      Dim pFunction As String = "clsDeliveryColLoadParents" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the DeliveryCol 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150314-1803", vRequester) 
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
  Public Function FindByID(ByVal vID As Long) As clsDelivery
    If Me.Count = 0 Then Return New clsDelivery 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
    
    Dim pDelivery As clsDelivery = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pDelivery) 
    If pDelivery IsNot Nothing Then Return pDelivery Else Return New clsDelivery() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderHeaderID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderHeaderID(ByVal vOrderHeaderID As Long) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.OrderHeaderID = vOrderHeaderID Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOrderHeaderID with vOrderHeaderID of {vOrderHeaderID}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.OrderHeaderID = vOrderHeaderID Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DeliveryAddress
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDeliveryAddress(ByVal vDeliveryAddress As String) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDeliveryAddress = vDeliveryAddress.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.DeliveryAddress.ToLowerInvariant() = vDeliveryAddress Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDeliveryAddress with vDeliveryAddress of {vDeliveryAddress}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.DeliveryAddress.ToLowerInvariant() = vDeliveryAddress Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ContactPhone
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByContactPhone(ByVal vContactPhone As String) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vContactPhone = vContactPhone.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.ContactPhone.ToLowerInvariant() = vContactPhone Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByContactPhone with vContactPhone of {vContactPhone}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.ContactPhone.ToLowerInvariant() = vContactPhone Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ContactName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByContactName(ByVal vContactName As String) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vContactName = vContactName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.ContactName.ToLowerInvariant() = vContactName Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByContactName with vContactName of {vContactName}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.ContactName.ToLowerInvariant() = vContactName Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DeliveryMethod
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDeliveryMethod(ByVal vDeliveryMethod As clsEnums.enmDeliveryMethod) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.DeliveryMethod = vDeliveryMethod Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDeliveryMethod with vDeliveryMethod of {vDeliveryMethod}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.DeliveryMethod = vDeliveryMethod Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderedDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderedDate(ByVal vOrderedDate As Date) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.OrderedDate = vOrderedDate Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOrderedDate with vOrderedDate of {vOrderedDate}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.OrderedDate = vOrderedDate Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ReceivedDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByReceivedDate(ByVal vReceivedDate As Date) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.ReceivedDate = vReceivedDate Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByReceivedDate with vReceivedDate of {vReceivedDate}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.ReceivedDate = vReceivedDate Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ArrivalToHubDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByArrivalToHubDate(ByVal vArrivalToHubDate As Date) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.ArrivalToHubDate = vArrivalToHubDate Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByArrivalToHubDate with vArrivalToHubDate of {vArrivalToHubDate}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.ArrivalToHubDate = vArrivalToHubDate Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ArrivalToCustomerDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByArrivalToCustomerDate(ByVal vArrivalToCustomerDate As Date) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.ArrivalToCustomerDate = vArrivalToCustomerDate Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByArrivalToCustomerDate with vArrivalToCustomerDate of {vArrivalToCustomerDate}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.ArrivalToCustomerDate = vArrivalToCustomerDate Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DeliveryStatus
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDeliveryStatus(ByVal vDeliveryStatus As clsEnums.enmDeliveryStatus) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.DeliveryStatus = vDeliveryStatus Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDeliveryStatus with vDeliveryStatus of {vDeliveryStatus}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.DeliveryStatus = vDeliveryStatus Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Location
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLocation(ByVal vLocation As String) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLocation = vLocation.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.Location.ToLowerInvariant() = vLocation Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLocation with vLocation of {vLocation}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.Location.ToLowerInvariant() = vLocation Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductsSummary
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductsSummary(ByVal vProductsSummary As String) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vProductsSummary = vProductsSummary.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.ProductsSummary.ToLowerInvariant() = vProductsSummary Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByProductsSummary with vProductsSummary of {vProductsSummary}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.ProductsSummary.ToLowerInvariant() = vProductsSummary Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Notes
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNotes(ByVal vNotes As String) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNotes = vNotes.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.Notes.ToLowerInvariant() = vNotes Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNotes with vNotes of {vNotes}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.Notes.ToLowerInvariant() = vNotes Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As clsDeliveryCol
    Dim pDeliverys As New clsDeliveryCol(_WithParents) 
    pDeliverys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsDelivery) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pDelivery As clsDelivery In pTempDist.Values
        If pDelivery.Tag.ToLowerInvariant() = vTag Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As clsDeliveryCol = Me.Clone() 
      For Each pDelivery As clsDelivery In pList 
        If pDelivery.Tag.ToLowerInvariant() = vTag Then
          Dim pDeliveryClone As clsDelivery = pDelivery.Clone()
          pDeliverys.Add(pDeliveryClone)
          If Not _FilledFromSumOnTheFly Then pDeliverys._Clean.Add(pDelivery.ID) 
        End If
      Next
    End If 
    
    Return pDeliverys
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
    For Each pDelivery As clsDelivery In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pDelivery.LoadDataRow(pRow, vRequester) 
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
    For Each p As clsDelivery In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
    For Each pCleanID As Long In _Clean 
      If pCleanID = 0 Then Continue For 
      Dim pFound As clsDelivery = Me.FindByID(pCleanID) 
      If pFound.ID = 0 Then 
        Dim pDeliveryToKill As New clsDelivery 
        pDeliveryToKill.ID = pCleanID 
        pDeliveryToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
        Me.Add(pDeliveryToKill) 
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
      Dim pFunction As String = "clsDeliveryColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the DeliveryCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150314-1803", vRequester) 
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
      Dim pFunction As String = "clsDeliveryColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the DeliveryCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-150314-1803", vRequester) 
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
 
      Dim pFunction As String = "clsDeliveryColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ArrivalToCustomerDate 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByArrivalToCustomerDate(ByVal vArrivalToCustomerDate As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ArrivalToCustomerDate={0}", vArrivalToCustomerDate)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vArrivalToCustomerDate 
          pBinaryWriter.Write(vArrivalToCustomerDate.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsDeliveryColDeleteByArrivalToCustomerDate" 
      Dim pParametersToLog = $"ArrivalToCustomerDate: {vArrivalToCustomerDate};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OrderHeaderID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByOrderHeaderID(ByVal vOrderHeaderID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderHeaderID={0}", vOrderHeaderID)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderHeaderID 
          pBinaryWriter.Write(vOrderHeaderID) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsDeliveryColDeleteByOrderHeaderID" 
      Dim pParametersToLog = $"OrderHeaderID: {vOrderHeaderID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific DeliveryStatus 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByDeliveryStatus(ByVal vDeliveryStatus As clsEnums.enmDeliveryStatus, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("DeliveryStatus={0}", vDeliveryStatus)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vDeliveryStatus 
          pBinaryWriter.Write(vDeliveryStatus.ToString()) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsDeliveryColDeleteByDeliveryStatus" 
      Dim pParametersToLog = $"DeliveryStatus: {vDeliveryStatus};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Delivery-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long, ByVal vRequester As clsRequester) As clsFault
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
 
      Dim pFunction As String = "clsDeliveryColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ArrivalToCustomerDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedArrivalToCustomerDate(ByVal vArrivalToCustomerDateStart As Date, ByVal vArrivalToCustomerDateEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ArrivalToCustomerDateStart={0}, ArrivalToCustomerDateEnd={1}", vArrivalToCustomerDateStart, vArrivalToCustomerDateEnd)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vArrivalToCustomerDateStart 
          pBinaryWriter.Write(vArrivalToCustomerDateStart.Ticks) 
          ' 
          'vArrivalToCustomerDateEnd 
          pBinaryWriter.Write(vArrivalToCustomerDateEnd.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsDeliveryColDeleteByBoundedArrivalToCustomerDate" 
      Dim pParametersToLog = $"ArrivalToCustomerDate: {vArrivalToCustomerDateStart};{vArrivalToCustomerDateEnd};" 
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
    Me.Sort(New clsDeliveryCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
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
  
  Public Sub SortByOrderHeaderID()
    Me.Sort(New clsDeliveryCol.CompareByOrderHeaderID)
  End Sub
  Private Class CompareByOrderHeaderID
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OrderHeaderID < y.OrderHeaderID Then
        Return -1
      ElseIf x.OrderHeaderID = y.OrderHeaderID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByOrderHeaderText()
    Me.Sort(New clsDeliveryCol.CompareByOrderHeaderText)
  End Sub
  Private Class CompareByOrderHeaderText
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OrderHeaderText, y.OrderHeaderText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDeliveryAddress()
    Me.Sort(New clsDeliveryCol.CompareByDeliveryAddress)
  End Sub
  Private Class CompareByDeliveryAddress
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DeliveryAddress, y.DeliveryAddress, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByContactPhone()
    Me.Sort(New clsDeliveryCol.CompareByContactPhone)
  End Sub
  Private Class CompareByContactPhone
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ContactPhone, y.ContactPhone, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByContactName()
    Me.Sort(New clsDeliveryCol.CompareByContactName)
  End Sub
  Private Class CompareByContactName
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ContactName, y.ContactName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDeliveryMethod()
    Me.Sort(New clsDeliveryCol.CompareByDeliveryMethod)
  End Sub
  Private Class CompareByDeliveryMethod
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DeliveryMethod < y.DeliveryMethod Then
        Return -1
      ElseIf x.DeliveryMethod = y.DeliveryMethod Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDeliveryMethodText()
    Me.Sort(New clsDeliveryCol.CompareByDeliveryMethodText)
  End Sub
  Private Class CompareByDeliveryMethodText
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DeliveryMethodText, y.DeliveryMethodText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOrderedDate()
    Me.Sort(New clsDeliveryCol.CompareByOrderedDate)
  End Sub
  Private Class CompareByOrderedDate
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OrderedDate < y.OrderedDate Then
        Return -1
      ElseIf x.OrderedDate = y.OrderedDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByReceivedDate()
    Me.Sort(New clsDeliveryCol.CompareByReceivedDate)
  End Sub
  Private Class CompareByReceivedDate
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ReceivedDate < y.ReceivedDate Then
        Return -1
      ElseIf x.ReceivedDate = y.ReceivedDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByArrivalToHubDate()
    Me.Sort(New clsDeliveryCol.CompareByArrivalToHubDate)
  End Sub
  Private Class CompareByArrivalToHubDate
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ArrivalToHubDate < y.ArrivalToHubDate Then
        Return -1
      ElseIf x.ArrivalToHubDate = y.ArrivalToHubDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByArrivalToCustomerDate()
    Me.Sort(New clsDeliveryCol.CompareByArrivalToCustomerDate)
  End Sub
  Private Class CompareByArrivalToCustomerDate
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ArrivalToCustomerDate < y.ArrivalToCustomerDate Then
        Return -1
      ElseIf x.ArrivalToCustomerDate = y.ArrivalToCustomerDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDeliveryStatus()
    Me.Sort(New clsDeliveryCol.CompareByDeliveryStatus)
  End Sub
  Private Class CompareByDeliveryStatus
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DeliveryStatus < y.DeliveryStatus Then
        Return -1
      ElseIf x.DeliveryStatus = y.DeliveryStatus Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDeliveryStatusText()
    Me.Sort(New clsDeliveryCol.CompareByDeliveryStatusText)
  End Sub
  Private Class CompareByDeliveryStatusText
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DeliveryStatusText, y.DeliveryStatusText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLocation()
    Me.Sort(New clsDeliveryCol.CompareByLocation)
  End Sub
  Private Class CompareByLocation
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Location, y.Location, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProductsSummary()
    Me.Sort(New clsDeliveryCol.CompareByProductsSummary)
  End Sub
  Private Class CompareByProductsSummary
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProductsSummary, y.ProductsSummary, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNotes()
    Me.Sort(New clsDeliveryCol.CompareByNotes)
  End Sub
  Private Class CompareByNotes
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Notes, y.Notes, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New clsDeliveryCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsDelivery)
    Private Function Compare(ByVal x As clsDelivery, ByVal y As clsDelivery) As Integer Implements System.Collections.Generic.IComparer(Of clsDelivery).Compare
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
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsDelivery) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsDelivery) 
 
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
  
