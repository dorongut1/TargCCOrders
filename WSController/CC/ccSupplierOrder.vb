Public Class clsSupplierOrder
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
    [EmailStatus] 
    [DeliveryMethod] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [OrderHeader] 
    [SupplierEmail] 
    [EmailSubject] 
    [EmailBody] 
    [EmailStatus] 
    [SentDate] 
    [TotalCost] 
    [DeliveryMethod] 
    [RequestedDeliveryDate] 
    [RequestedDeliveryDay] 
    [Notes] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [TotalCost] 
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
  Private _SupplierEmail As String
  Private _EmailSubject As String
  Private _EmailBody As String
  Private _EmailStatus As clsEnums.enmEmailStatus
  Private _EmailStatusText As String 
  Private _SentDate As Date
  Private _TotalCost As Decimal
  Private _DeliveryMethod As clsEnums.enmDeliveryMethod
  Private _DeliveryMethodText As String 
  Private _RequestedDeliveryDate As Date
  Private _RequestedDeliveryDay As String
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
  Public Property [SupplierEmail]() As String
    Get
      Return Me._SupplierEmail
    End Get
    Set(ByVal value As String)
      If Me._SupplierEmail <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SupplierEmail = value 
      End If 
    End Set
  End Property
  Public Property [EmailSubject]() As String
    Get
      Return Me._EmailSubject
    End Get
    Set(ByVal value As String)
      If Me._EmailSubject <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EmailSubject = value 
      End If 
    End Set
  End Property
  Public Property [EmailBody]() As String
    Get
      Return Me._EmailBody
    End Get
    Set(ByVal value As String)
      If Me._EmailBody <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EmailBody = value 
      End If 
    End Set
  End Property
  Public Property [EmailStatus]() As clsEnums.enmEmailStatus
    Get
      Return Me._EmailStatus
    End Get
    Set(ByVal value As clsEnums.enmEmailStatus)
      If Me._EmailStatus <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EmailStatus = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [EmailStatusText]() As String
    Get
      Return Me._EmailStatusText
    End Get
    Set(ByVal value As String)
      Me._EmailStatusText = value
    End Set
  End Property
  Public Property [SentDate]() As Date
    Get
      Return Me._SentDate
    End Get
    Set(ByVal value As Date)
      If Me._SentDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SentDate = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [TotalCost]() As Decimal
    Get
      Return Me._TotalCost
    End Get
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
  Public Property [RequestedDeliveryDate]() As Date
    Get
      Return Me._RequestedDeliveryDate
    End Get
    Set(ByVal value As Date)
      If Me._RequestedDeliveryDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RequestedDeliveryDate = value 
      End If 
    End Set
  End Property
  Public Property [RequestedDeliveryDay]() As String
    Get
      Return Me._RequestedDeliveryDay
    End Get
    Set(ByVal value As String)
      If Me._RequestedDeliveryDay <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RequestedDeliveryDay = value 
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
    If _SupplierEmail <> "" Then pValue.Append("SupplierEmail='" & _SupplierEmail & "' ‡ ") 
    If _EmailSubject <> "" Then pValue.Append("EmailSubject='" & _EmailSubject & "' ‡ ") 
    If _EmailBody <> "" Then pValue.Append("EmailBody='" & _EmailBody & "' ‡ ") 
    If _EmailStatus <> clsEnums.enmEmailStatus.UD Then pValue.Append("EmailStatus='" & _EmailStatus.FastToString() & "' ‡ ") 
    If _EmailStatusText <> "" Then pValue.Append("EmailStatusText='" & _EmailStatusText & "' ‡ ") 
    If Not (_SentDate = Nothing) Then pValue.Append("SentDate='" & _SentDate.ToString("o") & "' ‡ ") 
    If _TotalCost <> 0 Then pValue.Append("TotalCost='" & _TotalCost.ToString() & "' ‡ ") 
    If _DeliveryMethod <> clsEnums.enmDeliveryMethod.UD Then pValue.Append("DeliveryMethod='" & _DeliveryMethod.FastToString() & "' ‡ ") 
    If _DeliveryMethodText <> "" Then pValue.Append("DeliveryMethodText='" & _DeliveryMethodText & "' ‡ ") 
    If Not (_RequestedDeliveryDate = Nothing) Then pValue.Append("RequestedDeliveryDate='" & _RequestedDeliveryDate.ToString("o") & "' ‡ ") 
    If _RequestedDeliveryDay <> "" Then pValue.Append("RequestedDeliveryDay='" & _RequestedDeliveryDay & "' ‡ ") 
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
    pCSV.Append($",""{ccHelper.StringForCSV(_SupplierEmail)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EmailSubject)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EmailBody)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EmailStatus.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_EmailStatusText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SentDate.ToShortDateString & " " & _SentDate.ToShortTimeString)}""") 
    pCSV.Append("," & _TotalCost.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryMethod.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryMethodText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_RequestedDeliveryDate.ToShortDateString & " " & _RequestedDeliveryDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_RequestedDeliveryDay)}""") 
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
  
  Public Sub New(ByVal vclsSupplierOrder As clsSupplierOrder)
    MyBase.New()
    CreateEmpty()
    AssignValues(vclsSupplierOrder) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vOrderHeaderID As Long = 0 _ 
    , Optional vOrderHeaderText As String = "" _ 
    , Optional vSupplierEmail As String = "" _ 
    , Optional vEmailSubject As String = "" _ 
    , Optional vEmailBody As String = "" _ 
    , Optional vEmailStatus As clsEnums.enmEmailStatus = clsEnums.enmEmailStatus.Draft _ 
    , Optional vEmailStatusText As String = "" _ 
    , Optional vSentDate As Date = Nothing _ 
    , Optional vTotalCost As Decimal = 0 _ 
    , Optional vDeliveryMethod As clsEnums.enmDeliveryMethod = clsEnums.enmDeliveryMethod.UD _ 
    , Optional vDeliveryMethodText As String = "" _ 
    , Optional vRequestedDeliveryDate As Date = Nothing _ 
    , Optional vRequestedDeliveryDay As String = "" _ 
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
    _SupplierEmail = vSupplierEmail 
    _EmailSubject = vEmailSubject 
    _EmailBody = vEmailBody 
    _EmailStatus = vEmailStatus 
    _EmailStatusText = vEmailStatusText 
    _SentDate = vSentDate 
    _TotalCost = vTotalCost 
    _DeliveryMethod = vDeliveryMethod 
    _DeliveryMethodText = vDeliveryMethodText 
    _RequestedDeliveryDate = vRequestedDeliveryDate 
    _RequestedDeliveryDay = vRequestedDeliveryDay 
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
 
    _SupplierEmail = _SupplierEmail.Truncate(pTruncateLength, _IsTruncated) 
    _EmailSubject = _EmailSubject.Truncate(pTruncateLength, _IsTruncated) 
    _EmailBody = _EmailBody.Truncate(pTruncateLength, _IsTruncated) 
    _RequestedDeliveryDay = _RequestedDeliveryDay.Truncate(pTruncateLength, _IsTruncated) 
    _Notes = _Notes.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the SupplierOrder by PrimaryKey (ID) 
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
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SupplierOrder-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the SupplierOrder by the chosen parameters. This function may be a bit slower than accessing the SupplierOrder's GetBy... directly 
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
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-SupplierOrder-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SupplierOrder-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the SupplierOrder by ID. 
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
      Dim pFunction As String = "clsSupplierOrderGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the SupplierOrder 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150308-1015", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-SupplierOrder-151227-1738", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-SupplierOrder-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the SupplierOrder. If there are parents or children in the SupplierOrder, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = String.Format("SupplierOrder.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pSupplierOrder As New clsSupplierOrder 
    If Me.isEqual(pSupplierOrder) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-SupplierOrder-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-SupplierOrder-240611-135714", vRequester) 
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
      Dim pFunction As String = "clsSupplierOrderUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150314-1803", vRequester) 
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
    Dim pFunctionParameters As String = String.Format("SupplierOrder.ID={0}", _ID)
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
      Dim pFunction As String = "clsSupplierOrderDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150314-1803", vRequester) 
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
      Dim pFunction As String = "clsSupplierOrderDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-231207-1707", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is clsSupplierOrder) Then Return False 
    Dim pSupplierOrderToTest As clsSupplierOrder = CType(vTargCCEntityToTest, clsSupplierOrder) 
    Return isEqual(pSupplierOrderToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vSupplierOrderToTest As clsSupplierOrder) As Boolean
    With vSupplierOrderToTest
      If _ID <> .ID Then Return False
      If _OrderHeaderID <> .OrderHeaderID Then Return False
      If _SupplierEmail <> .SupplierEmail Then Return False
      If _EmailSubject <> .EmailSubject Then Return False
      If _EmailBody <> .EmailBody Then Return False
      If _EmailStatus <> .EmailStatus Then Return False
      If _SentDate <> Nothing AndAlso .SentDate <> Nothing Then 
        If ccHelper.ToLong(_SentDate.Subtract(.SentDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_SentDate = Nothing AndAlso .SentDate = Nothing) Then 
        Return False 
      End If 
      If _TotalCost <> .TotalCost Then Return False
      If _DeliveryMethod <> .DeliveryMethod Then Return False
      If _RequestedDeliveryDate <> Nothing AndAlso .RequestedDeliveryDate <> Nothing Then 
        If ccHelper.ToLong(_RequestedDeliveryDate.Subtract(.RequestedDeliveryDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_RequestedDeliveryDate = Nothing AndAlso .RequestedDeliveryDate = Nothing) Then 
        Return False 
      End If 
      If _RequestedDeliveryDay <> .RequestedDeliveryDay Then Return False
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
    Dim pClone As New clsSupplierOrder(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsSupplierOrder
    Dim pClone As New clsSupplierOrder(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderHeaderID") = _OrderHeaderID : Catch ex As Exception : Return pFault.LogException(ex, "OrderHeaderID", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("SupplierEmail") = _SupplierEmail : Catch ex As Exception : Return pFault.LogException(ex, "SupplierEmail", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("EmailSubject") = _EmailSubject : Catch ex As Exception : Return pFault.LogException(ex, "EmailSubject", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("EmailBody") = _EmailBody : Catch ex As Exception : Return pFault.LogException(ex, "EmailBody", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("EmailStatus") = _EmailStatus : Catch ex As Exception : Return pFault.LogException(ex, "EmailStatus", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("SentDate") = _SentDate : Catch ex As Exception : Return pFault.LogException(ex, "SentDate", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("TotalCost") = _TotalCost : Catch ex As Exception : Return pFault.LogException(ex, "TotalCost", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("DeliveryMethod") = _DeliveryMethod : Catch ex As Exception : Return pFault.LogException(ex, "DeliveryMethod", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("RequestedDeliveryDate") = _RequestedDeliveryDate : Catch ex As Exception : Return pFault.LogException(ex, "RequestedDeliveryDate", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("RequestedDeliveryDay") = _RequestedDeliveryDay : Catch ex As Exception : Return pFault.LogException(ex, "RequestedDeliveryDay", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
    Try : vDataRow("Notes") = _Notes : Catch ex As Exception : Return pFault.LogException(ex, "Notes", "TRGT-SupplierOrder-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SupplierOrder-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pSupplierOrder As clsSupplierOrder = CType(pXmlSerializer.Deserialize(pStreamReader), clsSupplierOrder) 
      AssignValues(pSupplierOrder) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-SupplierOrder-130515-1230", vRequester) 
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
          'SupplierEmail 
          If _SupplierEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SupplierEmail) 
          'EmailSubject 
          If _EmailSubject Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_EmailSubject) 
          'EmailBody 
          If _EmailBody Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_EmailBody) 
          'EmailStatus 
          pBinaryWriter.Write(_EmailStatus.FastToString()) 
          'SentDate 
          pBinaryWriter.Write(_SentDate.Ticks) 
          'TotalCost 
          pBinaryWriter.Write(_TotalCost) 
          'DeliveryMethod 
          pBinaryWriter.Write(_DeliveryMethod.FastToString()) 
          'RequestedDeliveryDate 
          pBinaryWriter.Write(_RequestedDeliveryDate.Ticks) 
          'RequestedDeliveryDay 
          If _RequestedDeliveryDay Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_RequestedDeliveryDay) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-SupplierOrder-150307-2338", vRequester) 
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
          'SupplierEmail 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SupplierEmail = pReader.ReadString 
          'EmailSubject 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _EmailSubject = pReader.ReadString 
          'EmailBody 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _EmailBody = pReader.ReadString 
          'EmailStatus 
          _EmailStatus = clsEnums.TranslateEnmEmailStatus(pReader.ReadString) 
          'SentDate 
          _SentDate = New Date(pReader.ReadInt64) 
          'TotalCost 
          _TotalCost = pReader.ReadDecimal 
          'DeliveryMethod 
          _DeliveryMethod = clsEnums.TranslateEnmDeliveryMethod(pReader.ReadString) 
          'RequestedDeliveryDate 
          _RequestedDeliveryDate = New Date(pReader.ReadInt64) 
          'RequestedDeliveryDay 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _RequestedDeliveryDay = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-SupplierOrder-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SupplierOrder-190720-1443", vRequester) 
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
 
      Dim pSupplierOrder As clsSupplierOrder = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsSupplierOrder)(vJSON, pSettings) 
      AssignValues(pSupplierOrder) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SupplierOrder-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vSupplierOrder As clsSupplierOrder)
    With vSupplierOrder
      _ID = .ID 
      _OrderHeaderID = .OrderHeaderID 
      If .OrderHeader IsNot Nothing Then 
        _OrderHeader = .OrderHeader.Clone() 
      End If 
      _OrderHeaderText = .OrderHeaderText 
      _SupplierEmail = .SupplierEmail 
      _EmailSubject = .EmailSubject 
      _EmailBody = .EmailBody 
      _EmailStatus = .EmailStatus 
      _EmailStatusText = .EmailStatusText
      _SentDate = .SentDate 
      _TotalCost = .TotalCost 
      _DeliveryMethod = .DeliveryMethod 
      _DeliveryMethodText = .DeliveryMethodText
      _RequestedDeliveryDate = .RequestedDeliveryDate 
      _RequestedDeliveryDay = .RequestedDeliveryDay 
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
      'EmailStatus 
      pTextToGet = "EmailStatusText (Enum)" 
      _EmailStatusText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.EmailStatus, _EmailStatus.FastToString(), vRequester) 
      'DeliveryMethod 
      pTextToGet = "DeliveryMethodText (Enum)" 
      _DeliveryMethodText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.DeliveryMethod, _DeliveryMethod.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-SupplierOrder-151124-1900", vRequester) 
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
      Dim pFunction As String = "clsSupplierOrderLoadParents" 
      Dim pParametersToLog = $"ID: {_ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150411-1107", vRequester) 
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
    _SupplierEmail = ""
    _EmailSubject = ""
    _EmailBody = ""
    'Default Value set by SQL Server Database (below): Draft
    _EmailStatus = clsEnums.enmEmailStatus.Draft
    _EmailStatusText = ""
    _SentDate = Nothing
    _TotalCost = 0
    _DeliveryMethod = clsEnums.enmDeliveryMethod.UD
    _DeliveryMethodText = ""
    _RequestedDeliveryDate = Nothing
    _RequestedDeliveryDay = ""
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
  
Public Class clsSupplierOrderCol
  Inherits cTargCCCollection(Of clsSupplierOrder)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsSupplierOrder) 
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
 
    For Each pRow As clsSupplierOrder In Me 
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
    pCSVTitle.Append(",""SupplierEmail""") 
    pCSVTitle.Append(",""EmailSubject""") 
    pCSVTitle.Append(",""EmailBody""") 
    pCSVTitle.Append(",""EmailStatus" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""EmailStatus (Text)""") 
    pCSVTitle.Append(",""SentDate""") 
    pCSVTitle.Append(",""TotalCost""") 
    pCSVTitle.Append(",""DeliveryMethod" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""DeliveryMethod (Text)""") 
    pCSVTitle.Append(",""RequestedDeliveryDate""") 
    pCSVTitle.Append(",""RequestedDeliveryDay""") 
    pCSVTitle.Append(",""Notes""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As clsSupplierOrder In Me 
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
 
  Public Overloads Sub Add(ByVal vSupplierOrder As clsSupplierOrder) 
    SyncLock _CollectionLock 
      MyBase.Add(vSupplierOrder) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vSupplierOrder As clsSupplierOrder) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vSupplierOrder) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vSupplierOrderCol As clsSupplierOrderCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vSupplierOrderCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vSupplierOrder As clsSupplierOrder) 
    SyncLock _CollectionLock 
      MyBase.Remove(vSupplierOrder) 
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
      Dim pTempDictionary As New Dictionary(Of Long, clsSupplierOrder) 
      
      For Each lSupplierOrder In Me 
        If lSupplierOrder.IsEmpty OrElse pTempDictionary.ContainsKey(lSupplierOrder.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lSupplierOrder.ID, lSupplierOrder) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lSupplierOrder.ToString, "TRGT-SupplierOrder-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", SupplierOrder:" & lSupplierOrder.ToString() & ", TRGT-SupplierOrder-260111-154657") 'Send it up the line 
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
 
    For Each lSupplierOrder As clsSupplierOrder In Me 
      lSupplierOrder.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [OrderHeaderID] 
    [SentDate] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the SupplierOrders by the chosen parameters. This function may be a bit slower than accessing the SupplierOrder's FillBy... directly 
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
        Case enmFillByParameterCombination.OrderHeaderID 
          pFault = FillByOrderHeaderID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.SentDate 
          pFault = FillBySentDate(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-SupplierOrder-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SupplierOrder-151223_1716", vRequester) 
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
 
      Dim pFunction As String = "clsSupplierOrderColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150308-1015", vRequester) 
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
 
      Dim pFunction As String = "clsSupplierOrderColFillByOrderHeaderID" 
      Dim pParametersToLog = $"OrderHeaderID: {vOrderHeaderID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder  
      If vAppend = True Then 
        Dim pSupplierOrders As New clsSupplierOrderCol 
        pSupplierOrders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pSupplierOrders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific SentDate, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillBySentDate(ByVal vSentDate As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("SentDate={0}", vSentDate)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vSentDate 
          pBinaryWriter.Write(vSentDate.Ticks) 
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
 
      Dim pFunction As String = "clsSupplierOrderColFillBySentDate" 
      Dim pParametersToLog = $"SentDate: {vSentDate};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder  
      If vAppend = True Then 
        Dim pSupplierOrders As New clsSupplierOrderCol 
        pSupplierOrders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pSupplierOrders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "clsSupplierOrderColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder  
      If vAppend = True Then 
        Dim pSupplierOrders As New clsSupplierOrderCol 
        pSupplierOrders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pSupplierOrders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific SentDate, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedSentDate(ByVal vSentDateStart As Date, ByVal vSentDateEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("SentDateStart={0}, SentDateEnd={1}", vSentDateStart, vSentDateEnd)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vSentDateStart 
          pBinaryWriter.Write(vSentDateStart.Ticks) 
          ' 
          'vSentDateEnd 
          pBinaryWriter.Write(vSentDateEnd.Ticks) 
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
 
      Dim pFunction As String = "clsSupplierOrderColFillByBoundedSentDate" 
      Dim pParametersToLog = $"SentDate: {vSentDateStart};{vSentDateEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder  
      If vAppend = True Then 
        Dim pSupplierOrders As New clsSupplierOrderCol 
        pSupplierOrders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pSupplierOrders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "clsSupplierOrderColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder   
      If vAppend = True Then 
        Dim pSupplierOrders As New clsSupplierOrderCol 
        pSupplierOrders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pSupplierOrders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-231207-1750", vRequester) 
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
    SentDateStart
    SentDateEnd
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
    Dim pSentDateStart As Nullable(Of Date) = Nothing
    Dim pSentDateEnd As Nullable(Of Date) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderID) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderID) : If pObj IsNot Nothing Then pOrderHeaderID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SentDateStart) Then pObj = vParameters(enmFillOnTheFlyParameters.SentDateStart) : If pObj IsNot Nothing Then pSentDateStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SentDateEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.SentDateEnd) : If pObj IsNot Nothing Then pSentDateEnd = CDate(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pOrderHeaderID _
        , pSentDateStart, pSentDateEnd _
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
        , ByVal vSentDateStart As Nullable(Of Date), ByVal vSentDateEnd As Nullable(Of Date) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OrderHeaderID={2}, SentDateStart={3}, SentDateEnd={4}", vIDFrom, vIDTo, vOrderHeaderID, vSentDateStart, vSentDateEnd)
    
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
          'SentDate 
          pBinaryWriter.Write(vSentDateStart.HasValue) 
          If vSentDateStart.HasValue Then pBinaryWriter.Write(vSentDateStart.Value.Ticks) : pParametersToLog &= $"SentDateStart={vSentDateStart.Value};"  
          pBinaryWriter.Write(vSentDateEnd.HasValue) 
          If vSentDateEnd.HasValue Then pBinaryWriter.Write(vSentDateEnd.Value.Ticks) : pParametersToLog &= $"SentDateEnd={vSentDateEnd.Value};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsSupplierOrderColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByOrderHeaderID
    GroupBySentDate
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
    Dim pSentDateStart As Nullable(Of Date) = Nothing
    Dim pSentDateEnd As Nullable(Of Date) = Nothing
    Dim pGroupByOrderHeaderID As Boolean = False
    Dim pGroupBySentDate As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderID) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderID) : If pObj IsNot Nothing Then pOrderHeaderID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SentDateStart) Then pObj = vParameters(enmFillOnTheFlyParameters.SentDateStart) : If pObj IsNot Nothing Then pSentDateStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SentDateEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.SentDateEnd) : If pObj IsNot Nothing Then pSentDateEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOrderHeaderID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOrderHeaderID) : If pObj IsNot Nothing Then pGroupByOrderHeaderID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupBySentDate) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupBySentDate) : If pObj IsNot Nothing Then pGroupBySentDate = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pOrderHeaderID _
        , pSentDateStart, pSentDateEnd _
        , pGroupByOrderHeaderID _
        , pGroupBySentDate _
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
        , ByVal vSentDateStart As Nullable(Of Date), ByVal vSentDateEnd As Nullable(Of Date) _
        , ByVal vGroupByOrderHeaderID As Boolean _
        , ByVal vGroupBySentDate As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OrderHeaderID={2}, SentDateStart={3}, SentDateEnd={4}, GroupByOrderHeaderID={5}, GroupBySentDate={6}", vIDFrom, vIDTo, vOrderHeaderID, vSentDateStart, vSentDateEnd, vGroupByOrderHeaderID, vGroupBySentDate)
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
          'SentDate 
          pBinaryWriter.Write(vSentDateStart.HasValue) 
          If vSentDateStart.HasValue Then pBinaryWriter.Write(vSentDateStart.Value.Ticks) : pParametersToLog &= $"SentDateStart={vSentDateStart};"  
          pBinaryWriter.Write(vSentDateEnd.HasValue) 
          If vSentDateEnd.HasValue Then pBinaryWriter.Write(vSentDateEnd.Value.Ticks) : pParametersToLog &= $"SentDateEnd={vSentDateEnd};"  
          pBinaryWriter.Write(vGroupByOrderHeaderID) : pParametersToLog &= $"GroupByOrderHeaderID={vGroupByOrderHeaderID};"  
          pBinaryWriter.Write(vGroupBySentDate) : pParametersToLog &= $"GroupBySentDate={vGroupBySentDate};"  
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsSupplierOrderColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrder  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vSupplierOrderArray As clsSupplierOrder())
    Me.Clear()
    
    For Each pSupplierOrder As clsSupplierOrder In vSupplierOrderArray
      Me.Add(pSupplierOrder)
      _Clean.Add(pSupplierOrder.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pSupplierOrder As New clsSupplierOrder(pRow, vRequester, _WithParents) 
        Me.Add(pSupplierOrder) 
        _Clean.Add(pSupplierOrder.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-SupplierOrderCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SupplierOrder-130515-1300", vRequester) 
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
      Dim pSupplierOrders As clsSupplierOrderCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsSupplierOrderCol) 
      For Each pSupplierOrder As clsSupplierOrder In pSupplierOrders 
        Me.Add(pSupplierOrder) 
        _Clean.Add(pSupplierOrder.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-SupplierOrder-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SupplierOrder-190720-1443", vRequester) 
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
 
      Dim pSupplierOrders As List(Of clsSupplierOrder) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of clsSupplierOrder))(vJSON, pSettings) 
      For Each pSupplierOrder As clsSupplierOrder In pSupplierOrders 
        Me.Add(pSupplierOrder) 
        _Clean.Add(pSupplierOrder.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SupplierOrder-190720-2059", vRequester) 
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
          For Each lSupplierOrder As clsSupplierOrder In Me 
            Dim pByte As Byte() = lSupplierOrder.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-SupplierOrder-150307-2340", vRequester) 
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
            Dim pSupplierOrder As clsSupplierOrder = New clsSupplierOrder(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pSupplierOrder) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pSupplierOrder.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-SupplierOrder-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pSupplierOrder As clsSupplierOrder In Me 
      With pSupplierOrder 
        pFault = pSupplierOrder.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is clsSupplierOrderCol) Then Return False 
    Dim pSupplierOrderColToTest As clsSupplierOrderCol = CType(vEntitiesToTest, clsSupplierOrderCol) 
    Return isEqual(pSupplierOrderColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vSupplierOrdersToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vSupplierOrdersToTest As clsSupplierOrderCol) As Boolean
    If Me.Count <> vSupplierOrdersToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vSupplierOrdersToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pSupplierOrders._FilledFromSumOnTheFly = True
    
    For Each pSupplierOrder As clsSupplierOrder In Me 
      Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone() 
      pSupplierOrders.Add(pSupplierOrderClone) 
      If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
    Next 
    Return pSupplierOrders 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsSupplierOrderCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pSupplierOrders._FilledFromSumOnTheFly = True
    
    For Each pSupplierOrder As clsSupplierOrder In Me
      Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
      pSupplierOrders.Add(pSupplierOrderClone)
      If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
    Next
    Return pSupplierOrders
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As clsSupplierOrderCol 
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents)  
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pSupplierOrder As clsSupplierOrder In _SortedDictionaryForFindByID.Values.ToList() 
      If (pSupplierOrder.ID > vIDFrom AndAlso pSupplierOrder.ID <= vIDTo) Then 
        Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone() 
        pSupplierOrders.Add(pSupplierOrderClone) 
        If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
      End If 
    Next 
    Return pSupplierOrders 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by SentDate (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedSentDate(ByVal vSentDateStart As Date, ByVal vSentDateEnd As Date) As clsSupplierOrderCol 
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents)  
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pSupplierOrder As clsSupplierOrder In _SortedDictionaryForFindByID.Values.ToList() 
      If (pSupplierOrder.SentDate > vSentDateStart AndAlso pSupplierOrder.SentDate <= vSentDateEnd) Then 
        Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone() 
        pSupplierOrders.Add(pSupplierOrderClone) 
        If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
      End If 
    Next 
    Return pSupplierOrders 
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
      Dim pFunction As String = "clsSupplierOrderColLoadParents" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrderCol 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150314-1803", vRequester) 
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
  Public Function FindByID(ByVal vID As Long) As clsSupplierOrder
    If Me.Count = 0 Then Return New clsSupplierOrder 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
    
    Dim pSupplierOrder As clsSupplierOrder = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pSupplierOrder) 
    If pSupplierOrder IsNot Nothing Then Return pSupplierOrder Else Return New clsSupplierOrder() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderHeaderID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderHeaderID(ByVal vOrderHeaderID As Long) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.OrderHeaderID = vOrderHeaderID Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOrderHeaderID with vOrderHeaderID of {vOrderHeaderID}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.OrderHeaderID = vOrderHeaderID Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SupplierEmail
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySupplierEmail(ByVal vSupplierEmail As String) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSupplierEmail = vSupplierEmail.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.SupplierEmail.ToLowerInvariant() = vSupplierEmail Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySupplierEmail with vSupplierEmail of {vSupplierEmail}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.SupplierEmail.ToLowerInvariant() = vSupplierEmail Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EmailSubject
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEmailSubject(ByVal vEmailSubject As String) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEmailSubject = vEmailSubject.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.EmailSubject.ToLowerInvariant() = vEmailSubject Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEmailSubject with vEmailSubject of {vEmailSubject}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.EmailSubject.ToLowerInvariant() = vEmailSubject Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EmailBody
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEmailBody(ByVal vEmailBody As String) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEmailBody = vEmailBody.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.EmailBody.ToLowerInvariant() = vEmailBody Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEmailBody with vEmailBody of {vEmailBody}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.EmailBody.ToLowerInvariant() = vEmailBody Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EmailStatus
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEmailStatus(ByVal vEmailStatus As clsEnums.enmEmailStatus) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.EmailStatus = vEmailStatus Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEmailStatus with vEmailStatus of {vEmailStatus}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.EmailStatus = vEmailStatus Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SentDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySentDate(ByVal vSentDate As Date) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.SentDate = vSentDate Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySentDate with vSentDate of {vSentDate}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.SentDate = vSentDate Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TotalCost
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTotalCost(ByVal vTotalCost As Decimal) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.TotalCost = vTotalCost Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTotalCost with vTotalCost of {vTotalCost}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.TotalCost = vTotalCost Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DeliveryMethod
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDeliveryMethod(ByVal vDeliveryMethod As clsEnums.enmDeliveryMethod) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.DeliveryMethod = vDeliveryMethod Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDeliveryMethod with vDeliveryMethod of {vDeliveryMethod}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.DeliveryMethod = vDeliveryMethod Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RequestedDeliveryDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRequestedDeliveryDate(ByVal vRequestedDeliveryDate As Date) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.RequestedDeliveryDate = vRequestedDeliveryDate Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRequestedDeliveryDate with vRequestedDeliveryDate of {vRequestedDeliveryDate}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.RequestedDeliveryDate = vRequestedDeliveryDate Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RequestedDeliveryDay
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRequestedDeliveryDay(ByVal vRequestedDeliveryDay As String) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vRequestedDeliveryDay = vRequestedDeliveryDay.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.RequestedDeliveryDay.ToLowerInvariant() = vRequestedDeliveryDay Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRequestedDeliveryDay with vRequestedDeliveryDay of {vRequestedDeliveryDay}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.RequestedDeliveryDay.ToLowerInvariant() = vRequestedDeliveryDay Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Notes
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNotes(ByVal vNotes As String) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNotes = vNotes.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.Notes.ToLowerInvariant() = vNotes Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNotes with vNotes of {vNotes}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.Notes.ToLowerInvariant() = vNotes Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As clsSupplierOrderCol
    Dim pSupplierOrders As New clsSupplierOrderCol(_WithParents) 
    pSupplierOrders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsSupplierOrder) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSupplierOrder As clsSupplierOrder In pTempDist.Values
        If pSupplierOrder.Tag.ToLowerInvariant() = vTag Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As clsSupplierOrderCol = Me.Clone() 
      For Each pSupplierOrder As clsSupplierOrder In pList 
        If pSupplierOrder.Tag.ToLowerInvariant() = vTag Then
          Dim pSupplierOrderClone As clsSupplierOrder = pSupplierOrder.Clone()
          pSupplierOrders.Add(pSupplierOrderClone)
          If Not _FilledFromSumOnTheFly Then pSupplierOrders._Clean.Add(pSupplierOrder.ID) 
        End If
      Next
    End If 
    
    Return pSupplierOrders
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
    For Each pSupplierOrder As clsSupplierOrder In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pSupplierOrder.LoadDataRow(pRow, vRequester) 
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
    For Each p As clsSupplierOrder In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
    For Each pCleanID As Long In _Clean 
      If pCleanID = 0 Then Continue For 
      Dim pFound As clsSupplierOrder = Me.FindByID(pCleanID) 
      If pFound.ID = 0 Then 
        Dim pSupplierOrderToKill As New clsSupplierOrder 
        pSupplierOrderToKill.ID = pCleanID 
        pSupplierOrderToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
        Me.Add(pSupplierOrderToKill) 
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
      Dim pFunction As String = "clsSupplierOrderColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrderCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150314-1803", vRequester) 
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
      Dim pFunction As String = "clsSupplierOrderColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the SupplierOrderCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-150314-1803", vRequester) 
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
 
      Dim pFunction As String = "clsSupplierOrderColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-200709-0852-", vRequester) 
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
 
      Dim pFunction As String = "clsSupplierOrderColDeleteByOrderHeaderID" 
      Dim pParametersToLog = $"OrderHeaderID: {vOrderHeaderID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific SentDate 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteBySentDate(ByVal vSentDate As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("SentDate={0}", vSentDate)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vSentDate 
          pBinaryWriter.Write(vSentDate.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsSupplierOrderColDeleteBySentDate" 
      Dim pParametersToLog = $"SentDate: {vSentDate};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-SupplierOrder-200709-0852-", vRequester) 
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
 
      Dim pFunction As String = "clsSupplierOrderColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific SentDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedSentDate(ByVal vSentDateStart As Date, ByVal vSentDateEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("SentDateStart={0}, SentDateEnd={1}", vSentDateStart, vSentDateEnd)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vSentDateStart 
          pBinaryWriter.Write(vSentDateStart.Ticks) 
          ' 
          'vSentDateEnd 
          pBinaryWriter.Write(vSentDateEnd.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsSupplierOrderColDeleteByBoundedSentDate" 
      Dim pParametersToLog = $"SentDate: {vSentDateStart};{vSentDateEnd};" 
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
    Me.Sort(New clsSupplierOrderCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
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
    Me.Sort(New clsSupplierOrderCol.CompareByOrderHeaderID)
  End Sub
  Private Class CompareByOrderHeaderID
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
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
    Me.Sort(New clsSupplierOrderCol.CompareByOrderHeaderText)
  End Sub
  Private Class CompareByOrderHeaderText
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OrderHeaderText, y.OrderHeaderText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySupplierEmail()
    Me.Sort(New clsSupplierOrderCol.CompareBySupplierEmail)
  End Sub
  Private Class CompareBySupplierEmail
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SupplierEmail, y.SupplierEmail, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEmailSubject()
    Me.Sort(New clsSupplierOrderCol.CompareByEmailSubject)
  End Sub
  Private Class CompareByEmailSubject
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EmailSubject, y.EmailSubject, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEmailBody()
    Me.Sort(New clsSupplierOrderCol.CompareByEmailBody)
  End Sub
  Private Class CompareByEmailBody
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EmailBody, y.EmailBody, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEmailStatus()
    Me.Sort(New clsSupplierOrderCol.CompareByEmailStatus)
  End Sub
  Private Class CompareByEmailStatus
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.EmailStatus < y.EmailStatus Then
        Return -1
      ElseIf x.EmailStatus = y.EmailStatus Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByEmailStatusText()
    Me.Sort(New clsSupplierOrderCol.CompareByEmailStatusText)
  End Sub
  Private Class CompareByEmailStatusText
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EmailStatusText, y.EmailStatusText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySentDate()
    Me.Sort(New clsSupplierOrderCol.CompareBySentDate)
  End Sub
  Private Class CompareBySentDate
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.SentDate < y.SentDate Then
        Return -1
      ElseIf x.SentDate = y.SentDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTotalCost()
    Me.Sort(New clsSupplierOrderCol.CompareByTotalCost)
  End Sub
  Private Class CompareByTotalCost
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TotalCost < y.TotalCost Then
        Return -1
      ElseIf x.TotalCost = y.TotalCost Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDeliveryMethod()
    Me.Sort(New clsSupplierOrderCol.CompareByDeliveryMethod)
  End Sub
  Private Class CompareByDeliveryMethod
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
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
    Me.Sort(New clsSupplierOrderCol.CompareByDeliveryMethodText)
  End Sub
  Private Class CompareByDeliveryMethodText
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DeliveryMethodText, y.DeliveryMethodText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRequestedDeliveryDate()
    Me.Sort(New clsSupplierOrderCol.CompareByRequestedDeliveryDate)
  End Sub
  Private Class CompareByRequestedDeliveryDate
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.RequestedDeliveryDate < y.RequestedDeliveryDate Then
        Return -1
      ElseIf x.RequestedDeliveryDate = y.RequestedDeliveryDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByRequestedDeliveryDay()
    Me.Sort(New clsSupplierOrderCol.CompareByRequestedDeliveryDay)
  End Sub
  Private Class CompareByRequestedDeliveryDay
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.RequestedDeliveryDay, y.RequestedDeliveryDay, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNotes()
    Me.Sort(New clsSupplierOrderCol.CompareByNotes)
  End Sub
  Private Class CompareByNotes
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Notes, y.Notes, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New clsSupplierOrderCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsSupplierOrder)
    Private Function Compare(ByVal x As clsSupplierOrder, ByVal y As clsSupplierOrder) As Integer Implements System.Collections.Generic.IComparer(Of clsSupplierOrder).Compare
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
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsSupplierOrder) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsSupplierOrder) 
 
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
  
