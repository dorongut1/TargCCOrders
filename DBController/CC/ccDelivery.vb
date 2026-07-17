Public Class clsDelivery
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
  Implements ITargCCDataReaderUser 
 
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
  ''' Raised before GetByXXX. Used to override the SP. Check rCommand to see what the SP was supposed to be 
  ''' </summary> 
  ''' <param name="rCommandText"></param> 
  ''' <param name="rDALParameters"></param> 
  ''' <param name="rCancel"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="rFault"></param> 
  Friend Event evtBeforeGetWithRequester(ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
 
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
  Friend Event evtBeforeAddWithRequester(ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
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
    [Friend] 
  End Enum 
  ''' <summary> 
  ''' Raised before updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeUpdate(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean) 
  Friend Event evtBeforeUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
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
  
  Private _IsCleanForXML As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
  
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
  Public Property [ProductsSummary]() As String
    Get
      Return Me._ProductsSummary
    End Get
    Set(ByVal value As String)
      If Me._ProductsSummary <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProductsSummary = value 
      End If 
    End Set
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
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _DeliveryAddress = ccHelper.RemoveChrW0(_DeliveryAddress) 
    _ContactPhone = ccHelper.RemoveChrW0(_ContactPhone) 
    _ContactName = ccHelper.RemoveChrW0(_ContactName) 
    _Location = ccHelper.RemoveChrW0(_Location) 
    _ProductsSummary = ccHelper.RemoveChrW0(_ProductsSummary) 
    _Notes = ccHelper.RemoveChrW0(_Notes) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDelivery_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDelivery_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDelivery_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"Delivery not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-Delivery-210927-1527", vRequester, vAdditionalMessageToUser:=$"Delivery not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.DeliveryCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliveryGetByID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vID) 
        pLastReadVariableName = "WithParents" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"Delivery not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-Delivery-210625-0950", vRequester, vAdditionalMessageToUser:=$"Delivery not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryUpdate, "clsDelivery_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryUpdate, "clsDelivery_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
  Friend Function UpdateFriend(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryUpdate, "clsDelivery_UpdateFriend", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pDelivery As New clsDelivery(_WithParents) 
    If Me.isEqual(pDelivery) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-Delivery-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Delivery-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "ccDeliveryUpdateFriend"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
    
    Dim pObjectAdded As Boolean = False 
    
    If _ID = 0 Then 
      pObjectAdded = True 
      RaiseEvent evtBeforeAdd(pCancel) 
      If pCancel = True Then Return pFault 
      RaiseEvent evtBeforeAddWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
      If pCancel = True Then Return pFault 
    End If 
    RaiseEvent evtBeforeUpdate(enmUpdateType.Friend, pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeUpdateWithRequester(enmUpdateType.Friend, pCommandText, pDALParameters, pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      Dim pCachedDelivery As clsDelivery 
      If _ID = 0 Then 
        pCachedDelivery = New clsDelivery(_WithParents) 
        'get last ID 
        Dim pDeliveryCol As clsDeliveryCol = MyController.DBCache.DeliveryCol.Clone() 
        If pDeliveryCol.Count = 0 Then 
          _ID = 1 
        Else 
          pDeliveryCol.SortByID() 
          Dim pLastID As Long = pDeliveryCol(pDeliveryCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.DeliveryCol.Add(pCachedDelivery) 
      Else  
        pCachedDelivery = MyController.DBCache.DeliveryCol.FindByID(_ID) 
      End If 
      pCachedDelivery.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.DeliveryCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_OrderHeaderID, False) 
        pLastReadVariableName = "DeliveryAddress" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 500).Value = ccHelper.ObjectNullable(_DeliveryAddress) 
        pLastReadVariableName = "ContactPhone" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_ContactPhone) 
        pLastReadVariableName = "ContactName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_ContactName) 
        pLastReadVariableName = "enmDeliveryMethod" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_DeliveryMethod.FastToString()) 
        pLastReadVariableName = "OrderedDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_OrderedDate) 
        pLastReadVariableName = "ReceivedDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_ReceivedDate) 
        pLastReadVariableName = "ArrivalToHubDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_ArrivalToHubDate) 
        pLastReadVariableName = "ArrivalToCustomerDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_ArrivalToCustomerDate) 
        pLastReadVariableName = "enmDeliveryStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_DeliveryStatus.FastToString()) 
        pLastReadVariableName = "Location" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 500).Value = ccHelper.ObjectNullable(_Location) 
        pLastReadVariableName = "blg_ProductsSummary" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_ProductsSummary) 
        pLastReadVariableName = "Notes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
        
        'Execute query 
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'Now get the ID 
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            pID = pTargCCReader.GetInt64(0) 
            _ID = pID 
            bPrimaryKey = pID 
            If pID = 0 Then 
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Parents 
      Dim pOrderHeader As clsOrderHeader = _OrderHeader 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Parents 
      If _WithParents <> clsEnums.enmLoadParent.EntireObject Then 
        If Not pOrderHeader Is Nothing Then _OrderHeader = pOrderHeader 
      End If 
      
    End If 
  
    If pObjectAdded = True Then 
      RaiseEvent evtAfterAdd() 
      RaiseEvent evtAfterAddWithRequester(vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
    End If 
    RaiseEvent evtAfterUpdate(enmUpdateType.Friend)
    RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.Friend, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
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
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryUpdate, "clsDelivery_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pDelivery As New clsDelivery(_WithParents) 
    If Me.isEqual(pDelivery) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-Delivery-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Delivery-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "ccDeliveryUpdate"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
    
    Dim pObjectAdded As Boolean = False 
    
    If _ID = 0 Then 
      pObjectAdded = True 
      RaiseEvent evtBeforeAdd(pCancel) 
      If pCancel = True Then Return pFault 
      RaiseEvent evtBeforeAddWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
      If pCancel = True Then Return pFault 
    End If 
    RaiseEvent evtBeforeUpdate(enmUpdateType.Standard, pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeUpdateWithRequester(enmUpdateType.Standard, pCommandText, pDALParameters, pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      Dim pCachedDelivery As clsDelivery 
      If _ID = 0 Then 
        pCachedDelivery = New clsDelivery(_WithParents) 
        'get last ID 
        Dim pDeliveryCol As clsDeliveryCol = MyController.DBCache.DeliveryCol.Clone() 
        If pDeliveryCol.Count = 0 Then 
          _ID = 1 
        Else 
          pDeliveryCol.SortByID() 
          Dim pLastID As Long = pDeliveryCol(pDeliveryCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.DeliveryCol.Add(pCachedDelivery) 
      Else  
        pCachedDelivery = MyController.DBCache.DeliveryCol.FindByID(_ID) 
      End If 
      pCachedDelivery.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.DeliveryCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_OrderHeaderID, False) 
        pLastReadVariableName = "DeliveryAddress" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 500).Value = ccHelper.ObjectNullable(_DeliveryAddress) 
        pLastReadVariableName = "ContactPhone" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_ContactPhone) 
        pLastReadVariableName = "ContactName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_ContactName) 
        pLastReadVariableName = "enmDeliveryMethod" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_DeliveryMethod.FastToString()) 
        pLastReadVariableName = "OrderedDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_OrderedDate) 
        pLastReadVariableName = "ReceivedDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_ReceivedDate) 
        pLastReadVariableName = "ArrivalToHubDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_ArrivalToHubDate) 
        pLastReadVariableName = "ArrivalToCustomerDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_ArrivalToCustomerDate) 
        pLastReadVariableName = "enmDeliveryStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_DeliveryStatus.FastToString()) 
        pLastReadVariableName = "Location" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 500).Value = ccHelper.ObjectNullable(_Location) 
        pLastReadVariableName = "Notes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
        
        'Execute query 
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'Now get the ID 
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            pID = pTargCCReader.GetInt64(0) 
            _ID = pID 
            bPrimaryKey = pID 
            If pID = 0 Then 
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Parents 
      Dim pOrderHeader As clsOrderHeader = _OrderHeader 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Parents 
      If _WithParents <> clsEnums.enmLoadParent.EntireObject Then 
        If Not pOrderHeader Is Nothing Then _OrderHeader = pOrderHeader 
      End If 
      
    End If 
  
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
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryDelete, "clsDelivery_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "ccDeliveryDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      MyController.DBCache.DeliveryCol.Remove(MyController.DBCache.DeliveryCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.DeliveryCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = _ID
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
 
        'Execute query 
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expected to get -1 back 
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090623-1813", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
          
    RaiseEvent evtAfterDelete()
    RaiseEvent evtAfterDeleteWithRequester(vRequester, pFault) : If pFault.isOK = False Then Return pFault 
          
    CreateEmpty()
          
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryDelete, "clsDelivery_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "ccDeliveryDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      MyController.DBCache.DeliveryCol.Remove(MyController.DBCache.DeliveryCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.DeliveryCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = vID 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
 
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
 
        'I expected to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Delivery-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
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
 
    If _IsCleanForXML = False Then 
      CleanEntityForXML() 
    End If 
 
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
 
    If _IsCleanForXML = False Then 
      CleanEntityForXML() 
    End If 
 
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
  ''' This loads the dependant Parents
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault
    
    'Foreign Parent
    If _OrderHeaderID > 0 Then
      _OrderHeader = New clsOrderHeader()
      pFault = _OrderHeader.GetByID(_OrderHeaderID, vRequester, True)
      If pFault.isOK = False Then Return pFault
      _OrderHeaderText = _OrderHeader.DefaultDesignation 
    End If
    _WithParents = clsEnums.enmLoadParent.EntireObject 
    
    pFault.SetOK()
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
#Region "Load Entity" 
  Friend Function LoadMeFromIDataReader(vReader As IDataReader, vRequester As clsRequester) As clsFault Implements ITargCCDataReaderUser.LoadMeFromIDataReader 
    Dim pFunctionParameters As String = ""  
    Dim pFault As New clsFault
  
    Dim pLastReadVariableName As String = "" 
    Try
      pLastReadVariableName = "ID" 
      If Not vReader.IsDBNull(0) Then _ID = vReader.GetInt64(0)
      pLastReadVariableName = "OrderHeaderID" 
      If Not vReader.IsDBNull(1) Then _OrderHeaderID = vReader.GetInt64(1)
      pLastReadVariableName = "DeliveryAddress" 
      If Not vReader.IsDBNull(2) Then _DeliveryAddress = vReader.GetString(2) 
      pLastReadVariableName = "ContactPhone" 
      If Not vReader.IsDBNull(3) Then _ContactPhone = vReader.GetString(3) 
      pLastReadVariableName = "ContactName" 
      If Not vReader.IsDBNull(4) Then _ContactName = vReader.GetString(4) 
      pLastReadVariableName = "enmDeliveryMethod" 
      If Not vReader.IsDBNull(5) Then _DeliveryMethod = clsEnums.TranslateEnmDeliveryMethod(vReader.GetString(5))
      pLastReadVariableName = "OrderedDate" 
      If Not vReader.IsDBNull(6) Then _OrderedDate = vReader.GetDateTime(6)
      pLastReadVariableName = "ReceivedDate" 
      If Not vReader.IsDBNull(7) Then _ReceivedDate = vReader.GetDateTime(7)
      pLastReadVariableName = "ArrivalToHubDate" 
      If Not vReader.IsDBNull(8) Then _ArrivalToHubDate = vReader.GetDateTime(8)
      pLastReadVariableName = "ArrivalToCustomerDate" 
      If Not vReader.IsDBNull(9) Then _ArrivalToCustomerDate = vReader.GetDateTime(9)
      pLastReadVariableName = "enmDeliveryStatus" 
      If Not vReader.IsDBNull(10) Then _DeliveryStatus = clsEnums.TranslateEnmDeliveryStatus(vReader.GetString(10))
      pLastReadVariableName = "Location" 
      If Not vReader.IsDBNull(11) Then _Location = vReader.GetString(11) 
      pLastReadVariableName = "blg_ProductsSummary" 
      If Not vReader.IsDBNull(12) Then _ProductsSummary = vReader.GetString(12) 
      pLastReadVariableName = "Notes" 
      If Not vReader.IsDBNull(13) Then _Notes = vReader.GetString(13) 
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(14) Then bDateAdded = vReader.GetDateTime(14)   
      If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
        pLastReadVariableName = "OrderHeaderText" 
        If Not vReader.IsDBNull(15) Then _OrderHeaderText = vReader.GetString(15) 
      ElseIf _WithParents = clsEnums.enmLoadParent.EntireObject Then 
        'vReader.Close() ' in case we are in a transaction - can't open 2 readers 
        pFault = LoadParents(vRequester) : If pFault.isOK = False Then Return pFault 
      End If
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedDelivery As clsDelivery, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pWithParents As clsEnums.enmLoadParent = _WithParents 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedDelivery) 
      If pWithParents = clsEnums.enmLoadParent.DoNotLoad Then 
        _OrderHeaderText = "."
        _WithParents = clsEnums.enmLoadParent.DoNotLoad 
      ElseIf pWithParents = clsEnums.enmLoadParent.TextOnly Then 
        'cache is loaded with TextOnly 
        _WithParents = clsEnums.enmLoadParent.TextOnly 
      ElseIf pWithParents = clsEnums.enmLoadParent.EntireObject Then 
        pFault = LoadParents(vRequester) : If pFault.isOK = False Then Return pFault 
      End If 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
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
    _IsCleanForXML = False 
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
  Implements ITargCCDataReaderUser 
  
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
  ''' Raised before FillByXXX. Used to override the SP. Check rCommand to see what the SP was supposed to be 
  ''' </summary> 
  ''' <param name="rCommandText"></param> 
  ''' <param name="rDALParameters"></param> 
  ''' <param name="rCancel"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="rFault"></param> 
  Friend Event evtBeforeFillWithRequester(ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  ''' <summary> 
  ''' Use the tag of the collection to define what you want to do 
  ''' </summary> 
  ''' <param name="rCancel"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="rFault"></param> 
  Friend Event evtBeforeUpdateWithRequester(ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Long) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsDelivery) 
  Private _RecreateDictionaryForFindByID As Boolean 
   
  Private _IsCleanForXML As Boolean 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
 
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
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lDelivery As clsDelivery In Me 
      lDelivery.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
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
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      Dim pDeliverysCached As clsDeliveryCol = MyController.DBCache.DeliveryCol.Clone() 
      pDeliverysCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pDeliverysCached.Reverse() 
      If vHowMany > 0 AndAlso pDeliverysCached.Count > vHowMany Then 
        Dim tmp As New clsDeliveryCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pDeliverysCached(i)) 
        Next 
        pDeliverysCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pDeliverysCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliverysFill"
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "Top" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString()
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ArrivalToCustomerDate, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByArrivalToCustomerDate(ByVal vArrivalToCustomerDate As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ArrivalToCustomerDate={0}", vArrivalToCustomerDate)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillByArrivalToCustomerDate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      Dim pDeliverysCached As clsDeliveryCol = MyController.DBCache.DeliveryCol.CloneByArrivalToCustomerDate(vArrivalToCustomerDate)
      pFault = LoadMeFromDBCache(pDeliverysCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliverysFillByArrivalToCustomerDate" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ArrivalToCustomerDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(vArrivalToCustomerDate) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OrderHeaderID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOrderHeaderID(ByVal vOrderHeaderID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderHeaderID={0}", vOrderHeaderID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillByOrderHeaderID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      Dim pDeliverysCached As clsDeliveryCol = MyController.DBCache.DeliveryCol.CloneByOrderHeaderID(vOrderHeaderID)
      pFault = LoadMeFromDBCache(pDeliverysCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliverysFillByOrderHeaderID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vOrderHeaderID) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific DeliveryStatus, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByDeliveryStatus(ByVal vDeliveryStatus As clsEnums.enmDeliveryStatus, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("DeliveryStatus={0}", vDeliveryStatus)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillByDeliveryStatus", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      Dim pDeliverysCached As clsDeliveryCol = MyController.DBCache.DeliveryCol.CloneByDeliveryStatus(vDeliveryStatus)
      pFault = LoadMeFromDBCache(pDeliverysCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliverysFillByDeliveryStatus" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmDeliveryStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vDeliveryStatus.FastToString()) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      Dim pDeliverysCached As clsDeliveryCol = MyController.DBCache.DeliveryCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pDeliverysCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliverysFillByBoundedID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vIDTo) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillByBoundedArrivalToCustomerDate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.DeliveryCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.DeliveryCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsDeliveryCol failed: " & pResponse) 
      Dim pDeliverysCached As clsDeliveryCol = MyController.DBCache.DeliveryCol.CloneByBoundedArrivalToCustomerDate(vArrivalToCustomerDateStart, vArrivalToCustomerDateEnd)
      pFault = LoadMeFromDBCache(pDeliverysCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliverysFillByBoundedArrivalToCustomerDate" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ArrivalToCustomerDateFrom" 
        pDALParameters.Add("bndArrivalToCustomerDateFrom", ccDAL.enmSQLDataType.Date).Value = (vArrivalToCustomerDateStart) 
        pLastReadVariableName = "ArrivalToCustomerDateTo" 
        pDALParameters.Add("bndArrivalToCustomerDateTo", ccDAL.enmSQLDataType.Date).Value = (vArrivalToCustomerDateEnd) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary> 
  ''' Gets a collection of all the items for the specified list of ID's. To append to an existing collection, set vAppend to true (default is false). An ID can only exist once in the collection 
  ''' </summary> 
  ''' <param name="vIDs"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vDir"></param> 
  ''' <param name="vAppend"></param> 
  ''' <returns></returns> 
  Public Function FillByListOfID(vIDs As List(Of Long), vRequester As clsRequester, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = $"Count of IDs: {vIDs?.Count}" 
    Dim pFault As New clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lDelivery As New clsDelivery() 
      pFault = lDelivery.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lDelivery.IsEmpty Then Me.Add(lDelivery) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pDeliverys As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pDeliverys, "clsDeliveryCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pDeliverys IsNot Nothing AndAlso Me.Count <> pDeliverys.Count Then FillFromListOfITargCCEntity(pDeliverys) 
    End If 
 
    Me.SortByID() 
    If vDir = clsEnums.enmFillDirection.DESC Then Me.Reverse() 
 
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Delivery-121122-2008", vRequester) 
      Dim pDeliverysCached As clsDeliveryCol = MyController.DBCache.DeliveryCol.Clone() 
      Dim pDeliverysToUse As New clsDeliveryCol() 
      For Each l In pDeliverysCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vOrderHeaderID.HasValue Then 
          If l.OrderHeaderID <> vOrderHeaderID.Value Then Continue For 
        End If 
        If vArrivalToCustomerDateStart.HasValue Then 
          If vArrivalToCustomerDateEnd.HasValue Then 
            If l.ArrivalToCustomerDate < vArrivalToCustomerDateStart OrElse l.ArrivalToCustomerDate > vArrivalToCustomerDateEnd.Value Then Continue For 
          Else 
            If l.ArrivalToCustomerDate <> vArrivalToCustomerDateStart.Value Then Continue For 
          End If 
        End If 
        If vDeliveryStatus <> clsEnums.enmDeliveryStatus.UD Then 
          If l.DeliveryStatus <> vDeliveryStatus Then Continue For 
        End If 
        pDeliverysToUse.Add(l) 
      Next 
      pDeliverysToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pDeliverysToUse.Reverse() 
      If vHowMany > 0 AndAlso pDeliverysToUse.Count > vHowMany Then 
        Dim tmp As New clsDeliveryCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pDeliverysToUse(i)) 
        Next 
        pDeliverysToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pDeliverysToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliverysFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOrderHeaderID) 
        pLastReadVariableName = "ArrivalToCustomerDateFrom" 
        pDALParameters.Add("bndArrivalToCustomerDateFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vArrivalToCustomerDateStart) 
        pLastReadVariableName = "ArrivalToCustomerDateTo" 
        pDALParameters.Add("bndArrivalToCustomerDateTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vArrivalToCustomerDateEnd) 
        pLastReadVariableName = "enmDeliveryStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(vDeliveryStatus.FastToString()) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Delivery-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccDeliverysFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOrderHeaderID) 
        pLastReadVariableName = "ArrivalToCustomerDateFrom" 
        pDALParameters.Add("bndArrivalToCustomerDateFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vArrivalToCustomerDateStart) 
        pLastReadVariableName = "ArrivalToCustomerDateTo" 
        pDALParameters.Add("bndArrivalToCustomerDateTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vArrivalToCustomerDateEnd) 
        pLastReadVariableName = "enmDeliveryStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(vDeliveryStatus) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add("GroupByOrderHeaderID", ccDAL.enmSQLDataType.Bit).Value = vGroupByOrderHeaderID
        pLastReadVariableName = "ArrivalToCustomerDate" 
        pDALParameters.Add("GroupByArrivalToCustomerDate", ccDAL.enmSQLDataType.Bit).Value = vGroupByArrivalToCustomerDate
        pLastReadVariableName = "enmDeliveryStatus" 
        pDALParameters.Add("GroupByenmDeliveryStatus", ccDAL.enmSQLDataType.Bit).Value = vGroupByDeliveryStatus
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
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
 
    If _IsCleanForXML = False Then 
      CleanCollectionForXML() 
    End If 
 
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
  ''' This loads the dependant parents for each of the rows 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault
    For Each pDelivery As clsDelivery In Me
      pFault = pDelivery.LoadParents(vRequester)
      If pFault.isOK = False Then Return pFault
    Next
    _WithParents = clsEnums.enmLoadParent.EntireObject 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryView, "clsDeliveryCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As clsDelivery In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
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
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As clsDelivery In Me 
      If pExists.ccStatus = clsEnums.enmObjectStatus.Dirty OrElse pExists.ccStatus = clsEnums.enmObjectStatus.New Then 
        pFault = pExists.Update(vRequester, vReload) : If pFault.isOK = False Then Exit For 
        _Clean.Add(pExists.ID) 
      ElseIf pExists.ccStatus = clsEnums.enmObjectStatus.Deleted Then 
        Dim pPrevID As Long = pExists.ID 
        pFault = pExists.Delete(vRequester) : If pFault.isOK = False Then Exit For 
        pExists.ID = pPrevID 
        pToRemove.Add(pExists.ID) 
      ElseIf pExists.ccStatus = clsEnums.enmObjectStatus.Clean Then 
        _Clean.Add(pExists.ID) 
      ElseIf pExists.ccStatus = clsEnums.enmObjectStatus.UD Then 
        'Status should not be UD  
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-Delivery-130415-0942", vRequester) 
      End If 
    Next 
    
    'Now remove the deleted ones from the collection 
    For Each pIDToDelete As Long In pToRemove 
      Me.Remove(Me.FindByID(pIDToDelete)) 
    Next 
 
    Return pFault 
  End Function 
  
  ''' <summary> 
  ''' This takes an external collection and updates the found rows in the database. If a row is not found (has an ID of 0), it adds it.  
  ''' It will not delete any rows. Check the 'tag' of each item in the collection to see if it was updated.  
  ''' Use the tag of the collection itself if you want to override the function with evtBeforeUpdateWithRequester 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function UpdateFromCollection(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault Implements ITargCCCollectionUpdateable.UpdateFromCollection 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryUpdate, "clsDeliveryCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As clsDelivery In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As clsDelivery In Me 
      p.Tag = "" 
      pFault = p.Update(vRequester, vReload) 
      If pFault.isOK = False Then 
        p.Tag = "Number: " & pFault.Number & ccHelper.NewLine & 
            "Message: " & pFault.Message & ccHelper.NewLine & 
            "Action: " & pFault.Action & ccHelper.NewLine & 
            "Description: " & pFault.Description & ccHelper.NewLine & 
            "FreeText: " & pFault.FreeText.Replace(Environment.NewLine, ccHelper.NewLine) & ccHelper.NewLine & 
            "LoggedAlertID: " & pFault.LoggedAlertID & ccHelper.NewLine 
        pFault.SetOK(vRequester) 
      Else 
        p.Tag = "OK" 
      End If 
    Next 
 
    pFault.SetOK() 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryDelete, "clsDeliveryCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccDeliverysDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New clsDeliveryCol(), vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryDelete, "clsDeliveryCol_DeleteByArrivalToCustomerDate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccDeliverysDeleteByArrivalToCustomerDate"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllDeliverys As New clsDeliveryCol() : pAllDeliverys.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredDeliverys As clsDeliveryCol = pAllDeliverys.CloneByArrivalToCustomerDate(vArrivalToCustomerDate) 
      For Each l In pFilteredDeliverys 
        pAllDeliverys.Remove(pAllDeliverys.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllDeliverys, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ArrivalToCustomerDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = (vArrivalToCustomerDate) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryDelete, "clsDeliveryCol_DeleteByOrderHeaderID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccDeliverysDeleteByOrderHeaderID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllDeliverys As New clsDeliveryCol() : pAllDeliverys.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredDeliverys As clsDeliveryCol = pAllDeliverys.CloneByOrderHeaderID(vOrderHeaderID) 
      For Each l In pFilteredDeliverys 
        pAllDeliverys.Remove(pAllDeliverys.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllDeliverys, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vOrderHeaderID) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryDelete, "clsDeliveryCol_DeleteByDeliveryStatus", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccDeliverysDeleteByDeliveryStatus"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllDeliverys As New clsDeliveryCol() : pAllDeliverys.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredDeliverys As clsDeliveryCol = pAllDeliverys.CloneByDeliveryStatus(vDeliveryStatus) 
      For Each l In pFilteredDeliverys 
        pAllDeliverys.Remove(pAllDeliverys.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllDeliverys, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmDeliveryStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vDeliveryStatus) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryDelete, "clsDeliveryCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccDeliverysDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Delivery-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vIDTo) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_DeliveryDelete, "clsDeliveryCol_DeleteByBoundedArrivalToCustomerDate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccDeliverysDeleteByBoundedArrivalToCustomerDate"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Delivery-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ArrivalToCustomerDateFrom" 
        pDALParameters.Add("bndArrivalToCustomerDateFrom", ccDAL.enmSQLDataType.Date).Value = (vArrivalToCustomerDateStart) 
        pLastReadVariableName = "ArrivalToCustomerDateTo" 
        pDALParameters.Add("bndArrivalToCustomerDateTo", ccDAL.enmSQLDataType.Date).Value = (vArrivalToCustomerDateEnd) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Delivery-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Delivery-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
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
  
#Region "Load Collection"  
  Friend Function LoadMeFromIDataReader(vReader As IDataReader, vRequester As clsRequester) As clsFault Implements ITargCCDataReaderUser.LoadMeFromIDataReader 
    Dim pFunctionParameters As String = ""  
    Dim pFault As New clsFault
  
    Dim pDelivery As clsDelivery
  
    While vReader.Read()
      pDelivery = New clsDelivery(_WithParents) 
      pFault = pDelivery.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pDelivery)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pDelivery.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedDeliveryCol As clsDeliveryCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pDelivery As clsDelivery 
 
      For Each pCachedDelivery As clsDelivery In vCachedDeliveryCol 
        pCachedDelivery.SetWithParents(_WithParents) 
        pDelivery = New clsDelivery(pCachedDelivery) 
        If _WithParents = clsEnums.enmLoadParent.DoNotLoad Then 
          pDelivery.OrderHeaderText = "." 
        End If 
        pDelivery.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pDelivery) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pDelivery.ID) 
      Next 
      If _WithParents = clsEnums.enmLoadParent.EntireObject Then 
        pFault = LoadParents(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Delivery-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
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
    _IsCleanForXML = False 
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
  
