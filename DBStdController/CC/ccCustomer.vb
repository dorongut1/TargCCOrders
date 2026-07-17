Public Class clsCustomer
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
  Implements ITargCCDataReaderUser 
 
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
    [CustomerType] 
    [AccountantMethod] 
  End Enum 
  'Child Properties 
  Public Enum enmChildProperty 
    UD 
    [BeehiveBuyerTracking] 
    [CustomerDebt] 
    [OrderHeader] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [CustomerCode] 
    [CustomerName] 
    [Phone] 
    [Email] 
    [Address] 
    [City] 
    [TaxID] 
    [CustomerType] 
    [PaymentTermsDays] 
    [Notes] 
    [IsActive] 
    [Location] 
    [AccountantEmail] 
    [AccountantMethod] 
    [InvoiceName] 
    [ProfitabilityCode] 
    [CustomerIdentifier] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [PaymentTermsDays] 
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
  
  
  Private _ID As Long
  Private _CustomerCode As String
  Private _CustomerName As String
  Private _Phone As String
  Private _Email As String
  Private _Address As String
  Private _City As String
  Private _TaxID As String
  Private _CustomerType As clsEnums.enmCustomerType
  Private _CustomerTypeText As String 
  Private _PaymentTermsDays As Integer
  Private _Notes As String
  Private _IsActive As Boolean
  Private _Location As String
  Private _AccountantEmail As String
  Private _AccountantMethod As clsEnums.enmAccountantMethod
  Private _AccountantMethodText As String 
  Private _InvoiceName As String
  Private _ProfitabilityCode As String
  Private _CustomerIdentifier As String
  Private _Tag As String
  Private _BeehiveBuyerTrackings As clsBeehiveBuyerTrackingCol
  Private _CustomerDebts As clsCustomerDebtCol
  Private _OrderHeaders As clsOrderHeaderCol
  
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
  Public Property [CustomerCode]() As String
    Get
      Return Me._CustomerCode
    End Get
    Set(ByVal value As String)
      If Me._CustomerCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CustomerCode = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [CustomerName]() As String
    Get
      Return Me._CustomerName
    End Get
    Set(ByVal value As String)
      If Me._CustomerName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CustomerName = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [Phone]() As String
    Get
      Return Me._Phone
    End Get
    Set(ByVal value As String)
      If Me._Phone <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Phone = value 
      End If 
    End Set
  End Property
  Public Property [Email]() As String
    Get
      Return Me._Email
    End Get
    Set(ByVal value As String)
      If Me._Email <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Email = value 
      End If 
    End Set
  End Property
  Public Property [Address]() As String
    Get
      Return Me._Address
    End Get
    Set(ByVal value As String)
      If Me._Address <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Address = value 
      End If 
    End Set
  End Property
  Public Property [City]() As String
    Get
      Return Me._City
    End Get
    Set(ByVal value As String)
      If Me._City <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._City = value 
      End If 
    End Set
  End Property
  Public Property [TaxID]() As String
    Get
      Return Me._TaxID
    End Get
    Set(ByVal value As String)
      If Me._TaxID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TaxID = value 
      End If 
    End Set
  End Property
  Public Property [CustomerType]() As clsEnums.enmCustomerType
    Get
      Return Me._CustomerType
    End Get
    Set(ByVal value As clsEnums.enmCustomerType)
      If Me._CustomerType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CustomerType = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [CustomerTypeText]() As String
    Get
      Return Me._CustomerTypeText
    End Get
    Set(ByVal value As String)
      Me._CustomerTypeText = value
    End Set
  End Property
  Public Property [PaymentTermsDays]() As Integer
    Get
      Return Me._PaymentTermsDays
    End Get
    Set(ByVal value As Integer)
      If Me._PaymentTermsDays <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PaymentTermsDays = value 
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
  Public Property [IsActive]() As Boolean
    Get
      Return Me._IsActive
    End Get
    Set(ByVal value As Boolean)
      If Me._IsActive <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IsActive = value 
      End If 
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
  Public Property [AccountantEmail]() As String
    Get
      Return Me._AccountantEmail
    End Get
    Set(ByVal value As String)
      If Me._AccountantEmail <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AccountantEmail = value 
      End If 
    End Set
  End Property
  Public Property [AccountantMethod]() As clsEnums.enmAccountantMethod
    Get
      Return Me._AccountantMethod
    End Get
    Set(ByVal value As clsEnums.enmAccountantMethod)
      If Me._AccountantMethod <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AccountantMethod = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [AccountantMethodText]() As String
    Get
      Return Me._AccountantMethodText
    End Get
    Set(ByVal value As String)
      Me._AccountantMethodText = value
    End Set
  End Property
  Public Property [InvoiceName]() As String
    Get
      Return Me._InvoiceName
    End Get
    Set(ByVal value As String)
      If Me._InvoiceName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._InvoiceName = value 
      End If 
    End Set
  End Property
  Public Property [ProfitabilityCode]() As String
    Get
      Return Me._ProfitabilityCode
    End Get
    Set(ByVal value As String)
      If Me._ProfitabilityCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProfitabilityCode = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [CustomerIdentifier]() As String
    Get
      Return Me._CustomerIdentifier
    End Get
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
  Public Property [BeehiveBuyerTrackings]() As clsBeehiveBuyerTrackingCol
    Get
      Return Me._BeehiveBuyerTrackings
    End Get
    Set(ByVal value As clsBeehiveBuyerTrackingCol)
      Me._BeehiveBuyerTrackings = value
    End Set
  End Property
  Public Property [CustomerDebts]() As clsCustomerDebtCol
    Get
      Return Me._CustomerDebts
    End Get
    Set(ByVal value As clsCustomerDebtCol)
      Me._CustomerDebts = value
    End Set
  End Property
  Public Property [OrderHeaders]() As clsOrderHeaderCol
    Get
      Return Me._OrderHeaders
    End Get
    Set(ByVal value As clsOrderHeaderCol)
      Me._OrderHeaders = value
    End Set
  End Property
  
  Private Sub CreateDefaultDesignation() 
    Dim pOverridenValue As String = Nothing 
    RaiseEvent evtOverrideDefaultDesignation(pOverridenValue) 
    If pOverridenValue = Nothing Then bDefaultDesignation = _CustomerName & " " & _CustomerCode Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _CustomerCode <> "" Then pValue.Append("CustomerCode='" & _CustomerCode & "' ‡ ") 
    If _CustomerName <> "" Then pValue.Append("CustomerName='" & _CustomerName & "' ‡ ") 
    If _Phone <> "" Then pValue.Append("Phone='" & _Phone & "' ‡ ") 
    If _Email <> "" Then pValue.Append("Email='" & _Email & "' ‡ ") 
    If _Address <> "" Then pValue.Append("Address='" & _Address & "' ‡ ") 
    If _City <> "" Then pValue.Append("City='" & _City & "' ‡ ") 
    If _TaxID <> "" Then pValue.Append("TaxID='" & _TaxID & "' ‡ ") 
    If _CustomerType <> clsEnums.enmCustomerType.UD Then pValue.Append("CustomerType='" & _CustomerType.FastToString() & "' ‡ ") 
    If _CustomerTypeText <> "" Then pValue.Append("CustomerTypeText='" & _CustomerTypeText & "' ‡ ") 
    If _PaymentTermsDays <> 0 Then pValue.Append("PaymentTermsDays='" & _PaymentTermsDays.ToString() & "' ‡ ") 
    If _Notes <> "" Then pValue.Append("Notes='" & _Notes & "' ‡ ") 
    pValue.Append("IsActive='" & _IsActive.ToString() & "' ‡ ") 
    If _Location <> "" Then pValue.Append("Location='" & _Location & "' ‡ ") 
    If _AccountantEmail <> "" Then pValue.Append("AccountantEmail='" & _AccountantEmail & "' ‡ ") 
    If _AccountantMethod <> clsEnums.enmAccountantMethod.UD Then pValue.Append("AccountantMethod='" & _AccountantMethod.FastToString() & "' ‡ ") 
    If _AccountantMethodText <> "" Then pValue.Append("AccountantMethodText='" & _AccountantMethodText & "' ‡ ") 
    If _InvoiceName <> "" Then pValue.Append("InvoiceName='" & _InvoiceName & "' ‡ ") 
    If _ProfitabilityCode <> "" Then pValue.Append("ProfitabilityCode='" & _ProfitabilityCode & "' ‡ ") 
    If _CustomerIdentifier <> "" Then pValue.Append("CustomerIdentifier='" & _CustomerIdentifier & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CustomerCode)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CustomerName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Phone)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Email)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Address)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_City)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TaxID)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CustomerType.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_CustomerTypeText)}""") 
    pCSV.Append("," & _PaymentTermsDays.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Notes)}""") 
    pCSV.Append(",""" & _IsActive.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Location)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_AccountantEmail)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_AccountantMethod.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_AccountantMethodText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_InvoiceName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProfitabilityCode)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CustomerIdentifier)}""") 
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
  
  Public Sub New(ByVal vclsCustomer As clsCustomer)
    MyBase.New()
    CreateEmpty()
    AssignValues(vclsCustomer) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vCustomerCode As String = "" _ 
    , Optional vCustomerName As String = "" _ 
    , Optional vPhone As String = "" _ 
    , Optional vEmail As String = "" _ 
    , Optional vAddress As String = "" _ 
    , Optional vCity As String = "" _ 
    , Optional vTaxID As String = "" _ 
    , Optional vCustomerType As clsEnums.enmCustomerType = clsEnums.enmCustomerType.Private _ 
    , Optional vCustomerTypeText As String = "" _ 
    , Optional vPaymentTermsDays As Integer = 0 _ 
    , Optional vNotes As String = "" _ 
    , Optional vIsActive As Boolean = True _ 
    , Optional vLocation As String = "" _ 
    , Optional vAccountantEmail As String = "" _ 
    , Optional vAccountantMethod As clsEnums.enmAccountantMethod = clsEnums.enmAccountantMethod.UD _ 
    , Optional vAccountantMethodText As String = "" _ 
    , Optional vInvoiceName As String = "" _ 
    , Optional vProfitabilityCode As String = "" _ 
    , Optional vCustomerIdentifier As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _CustomerCode = vCustomerCode 
    _CustomerName = vCustomerName 
    _Phone = vPhone 
    _Email = vEmail 
    _Address = vAddress 
    _City = vCity 
    _TaxID = vTaxID 
    _CustomerType = vCustomerType 
    _CustomerTypeText = vCustomerTypeText 
    _PaymentTermsDays = vPaymentTermsDays 
    _Notes = vNotes 
    _IsActive = vIsActive 
    _Location = vLocation 
    _AccountantEmail = vAccountantEmail 
    _AccountantMethod = vAccountantMethod 
    _AccountantMethodText = vAccountantMethodText 
    _InvoiceName = vInvoiceName 
    _ProfitabilityCode = vProfitabilityCode 
    _CustomerIdentifier = vCustomerIdentifier 
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
 
    _CustomerCode = _CustomerCode.Truncate(pTruncateLength, _IsTruncated) 
    _CustomerName = _CustomerName.Truncate(pTruncateLength, _IsTruncated) 
    _Phone = _Phone.Truncate(pTruncateLength, _IsTruncated) 
    _Email = _Email.Truncate(pTruncateLength, _IsTruncated) 
    _Address = _Address.Truncate(pTruncateLength, _IsTruncated) 
    _City = _City.Truncate(pTruncateLength, _IsTruncated) 
    _TaxID = _TaxID.Truncate(pTruncateLength, _IsTruncated) 
    _Notes = _Notes.Truncate(pTruncateLength, _IsTruncated) 
    _Location = _Location.Truncate(pTruncateLength, _IsTruncated) 
    _AccountantEmail = _AccountantEmail.Truncate(pTruncateLength, _IsTruncated) 
    _InvoiceName = _InvoiceName.Truncate(pTruncateLength, _IsTruncated) 
    _ProfitabilityCode = _ProfitabilityCode.Truncate(pTruncateLength, _IsTruncated) 
    _CustomerIdentifier = _CustomerIdentifier.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _CustomerCode = ccHelper.RemoveChrW0(_CustomerCode) 
    _CustomerName = ccHelper.RemoveChrW0(_CustomerName) 
    _Phone = ccHelper.RemoveChrW0(_Phone) 
    _Email = ccHelper.RemoveChrW0(_Email) 
    _Address = ccHelper.RemoveChrW0(_Address) 
    _City = ccHelper.RemoveChrW0(_City) 
    _TaxID = ccHelper.RemoveChrW0(_TaxID) 
    _Notes = ccHelper.RemoveChrW0(_Notes) 
    _Location = ccHelper.RemoveChrW0(_Location) 
    _AccountantEmail = ccHelper.RemoveChrW0(_AccountantEmail) 
    _InvoiceName = ccHelper.RemoveChrW0(_InvoiceName) 
    _ProfitabilityCode = ccHelper.RemoveChrW0(_ProfitabilityCode) 
    _CustomerIdentifier = ccHelper.RemoveChrW0(_CustomerIdentifier) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Customer by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomer_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Customer-151224_0844", vRequester) 
    End Try 
 
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsCustomer_GetByPrimaryKey", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [CustomerCode] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Customer by the chosen parameters. This function may be a bit slower than accessing the Customer's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomer_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.CustomerCode 
          pFault = GetByCustomerCode(CStr(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Customer-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Customer-151223_1716", vRequester)  
    End Try  
 
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsCustomer_GetByParameters", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the Customer by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomer_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"Customer not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-Customer-210927-1527", vRequester, vAdditionalMessageToUser:=$"Customer not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.CustomerCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerGetByID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vID) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"Customer not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-Customer-210625-0950", vRequester, vAdditionalMessageToUser:=$"Customer not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsCustomer_GetByID", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the Customer by CustomerCode.
  ''' </summary>
  ''' <param name="vCustomerCode"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByCustomerCode(ByVal vCustomerCode As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerCode={0}", vCustomerCode)
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomer_GetByCustomerCode", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.CustomerCol.FindByCustomerCode(vCustomerCode), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerGetByCustomerCode" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "CustomerCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vCustomerCode) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"Customer not found for GetByCustomerCode. See FunctionParameters for values", pFunctionParameters, "TRGT-Customer-210625-0950", vRequester, vAdditionalMessageToUser:=$"Customer not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsCustomer_GetByCustomerCode", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerUpdate, "clsCustomer_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Customer-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerUpdate, "clsCustomer_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Customer-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the Customer. If there are parents or children in the Customer, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Friend Function UpdateFriend(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerUpdate, "clsCustomer_UpdateFriend", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pCustomer As New clsCustomer() 
    If Me.isEqual(pCustomer) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-Customer-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Customer-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "ccCustomerUpdateFriend"
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
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      Dim pCachedCustomer As clsCustomer 
      If _ID = 0 Then 
        pCachedCustomer = New clsCustomer() 
        'get last ID 
        Dim pCustomerCol As clsCustomerCol = MyController.DBCache.CustomerCol.Clone() 
        If pCustomerCol.Count = 0 Then 
          _ID = 1 
        Else 
          pCustomerCol.SortByID() 
          Dim pLastID As Long = pCustomerCol(pCustomerCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.CustomerCol.Add(pCachedCustomer) 
      Else  
        pCachedCustomer = MyController.DBCache.CustomerCol.FindByID(_ID) 
      End If 
      pCachedCustomer.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.CustomerCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "CustomerCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_CustomerCode) 
        pLastReadVariableName = "CustomerName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_CustomerName) 
        pLastReadVariableName = "Phone" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 20).Value = ccHelper.ObjectNullable(_Phone) 
        pLastReadVariableName = "Email" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_Email) 
        pLastReadVariableName = "Address" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Address) 
        pLastReadVariableName = "City" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_City) 
        pLastReadVariableName = "TaxID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 20).Value = ccHelper.ObjectNullable(_TaxID) 
        pLastReadVariableName = "enmCustomerType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_CustomerType.FastToString()) 
        pLastReadVariableName = "PaymentTermsDays" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_PaymentTermsDays) 
        pLastReadVariableName = "Notes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes) 
        pLastReadVariableName = "blg_IsActive" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (_IsActive) 
        pLastReadVariableName = "Location" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_Location) 
        pLastReadVariableName = "AccountantEmail" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_AccountantEmail) 
        pLastReadVariableName = "enmAccountantMethod" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_AccountantMethod.FastToString()) 
        pLastReadVariableName = "InvoiceName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_InvoiceName) 
        pLastReadVariableName = "ProfitabilityCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_ProfitabilityCode) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-Customer-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Children 
      Dim pBeehiveBuyerTrackings As clsBeehiveBuyerTrackingCol = _BeehiveBuyerTrackings 
      Dim pCustomerDebts As clsCustomerDebtCol = _CustomerDebts 
      Dim pOrderHeaders As clsOrderHeaderCol = _OrderHeaders 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Children 
      If Not pBeehiveBuyerTrackings Is Nothing Then _BeehiveBuyerTrackings = pBeehiveBuyerTrackings 
      If Not pCustomerDebts Is Nothing Then _CustomerDebts = pCustomerDebts 
      If Not pOrderHeaders Is Nothing Then _OrderHeaders = pOrderHeaders 
      
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
  ''' This updates the Customer. If there are parents or children in the Customer, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerUpdate, "clsCustomer_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pCustomer As New clsCustomer() 
    If Me.isEqual(pCustomer) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-Customer-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Customer-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "ccCustomerUpdate"
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
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      Dim pCachedCustomer As clsCustomer 
      If _ID = 0 Then 
        pCachedCustomer = New clsCustomer() 
        'get last ID 
        Dim pCustomerCol As clsCustomerCol = MyController.DBCache.CustomerCol.Clone() 
        If pCustomerCol.Count = 0 Then 
          _ID = 1 
        Else 
          pCustomerCol.SortByID() 
          Dim pLastID As Long = pCustomerCol(pCustomerCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.CustomerCol.Add(pCachedCustomer) 
      Else  
        pCachedCustomer = MyController.DBCache.CustomerCol.FindByID(_ID) 
      End If 
      pCachedCustomer.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.CustomerCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "CustomerCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_CustomerCode) 
        pLastReadVariableName = "CustomerName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_CustomerName) 
        pLastReadVariableName = "Phone" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 20).Value = ccHelper.ObjectNullable(_Phone) 
        pLastReadVariableName = "Email" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_Email) 
        pLastReadVariableName = "Address" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Address) 
        pLastReadVariableName = "City" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_City) 
        pLastReadVariableName = "TaxID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 20).Value = ccHelper.ObjectNullable(_TaxID) 
        pLastReadVariableName = "enmCustomerType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_CustomerType.FastToString()) 
        pLastReadVariableName = "PaymentTermsDays" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_PaymentTermsDays) 
        pLastReadVariableName = "Notes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes) 
        pLastReadVariableName = "Location" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_Location) 
        pLastReadVariableName = "AccountantEmail" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_AccountantEmail) 
        pLastReadVariableName = "enmAccountantMethod" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_AccountantMethod.FastToString()) 
        pLastReadVariableName = "InvoiceName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_InvoiceName) 
        pLastReadVariableName = "ProfitabilityCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_ProfitabilityCode) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-Customer-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Children 
      Dim pBeehiveBuyerTrackings As clsBeehiveBuyerTrackingCol = _BeehiveBuyerTrackings 
      Dim pCustomerDebts As clsCustomerDebtCol = _CustomerDebts 
      Dim pOrderHeaders As clsOrderHeaderCol = _OrderHeaders 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Children 
      If Not pBeehiveBuyerTrackings Is Nothing Then _BeehiveBuyerTrackings = pBeehiveBuyerTrackings 
      If Not pCustomerDebts Is Nothing Then _CustomerDebts = pCustomerDebts 
      If Not pOrderHeaders Is Nothing Then _OrderHeaders = pOrderHeaders 
      
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
    Dim pFunctionParameters As String = String.Format("Customer.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDelete, "clsCustomer_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "ccCustomerDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      MyController.DBCache.CustomerCol.Remove(MyController.DBCache.CustomerCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.CustomerCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = _ID
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Customer-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDelete, "clsCustomer_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "ccCustomerDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      MyController.DBCache.CustomerCol.Remove(MyController.DBCache.CustomerCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.CustomerCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = vID 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
 
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
 
        'I expected to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Customer-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Fills the Customer's BeehiveBuyerTracking collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillBeehiveBuyerTrackings(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomer_FillBeehiveBuyerTrackings", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _BeehiveBuyerTrackings = New clsBeehiveBuyerTrackingCol(clsEnums.enmLoadParent.DoNotLoad)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _BeehiveBuyerTrackings.FillByCustomerID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the Customer's CustomerDebt collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillCustomerDebts(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomer_FillCustomerDebts", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _CustomerDebts = New clsCustomerDebtCol(clsEnums.enmLoadParent.DoNotLoad)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _CustomerDebts.FillByCustomerID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the Customer's OrderHeader collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillOrderHeaders(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomer_FillOrderHeaders", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _OrderHeaders = New clsOrderHeaderCol(clsEnums.enmLoadParent.DoNotLoad)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _OrderHeaders.FillByCustomerID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is clsCustomer) Then Return False 
    Dim pCustomerToTest As clsCustomer = CType(vTargCCEntityToTest, clsCustomer) 
    Return isEqual(pCustomerToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vCustomerToTest As clsCustomer) As Boolean
    With vCustomerToTest
      If _ID <> .ID Then Return False
      If _CustomerCode <> .CustomerCode Then Return False
      If _CustomerName <> .CustomerName Then Return False
      If _Phone <> .Phone Then Return False
      If _Email <> .Email Then Return False
      If _Address <> .Address Then Return False
      If _City <> .City Then Return False
      If _TaxID <> .TaxID Then Return False
      If _CustomerType <> .CustomerType Then Return False
      If _PaymentTermsDays <> .PaymentTermsDays Then Return False
      If _Notes <> .Notes Then Return False
      If _IsActive <> .IsActive Then Return False
      If _Location <> .Location Then Return False
      If _AccountantEmail <> .AccountantEmail Then Return False
      If _AccountantMethod <> .AccountantMethod Then Return False
      If _InvoiceName <> .InvoiceName Then Return False
      If _ProfitabilityCode <> .ProfitabilityCode Then Return False
      If _CustomerIdentifier <> .CustomerIdentifier Then Return False
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
    Dim pClone As New clsCustomer(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsCustomer
    Dim pClone As New clsCustomer(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("CustomerCode") = _CustomerCode : Catch ex As Exception : Return pFault.LogException(ex, "CustomerCode", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("CustomerName") = _CustomerName : Catch ex As Exception : Return pFault.LogException(ex, "CustomerName", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("Phone") = _Phone : Catch ex As Exception : Return pFault.LogException(ex, "Phone", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("Email") = _Email : Catch ex As Exception : Return pFault.LogException(ex, "Email", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("Address") = _Address : Catch ex As Exception : Return pFault.LogException(ex, "Address", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("City") = _City : Catch ex As Exception : Return pFault.LogException(ex, "City", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("TaxID") = _TaxID : Catch ex As Exception : Return pFault.LogException(ex, "TaxID", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("CustomerType") = _CustomerType : Catch ex As Exception : Return pFault.LogException(ex, "CustomerType", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("PaymentTermsDays") = _PaymentTermsDays : Catch ex As Exception : Return pFault.LogException(ex, "PaymentTermsDays", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("Notes") = _Notes : Catch ex As Exception : Return pFault.LogException(ex, "Notes", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("IsActive") = _IsActive : Catch ex As Exception : Return pFault.LogException(ex, "IsActive", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("Location") = _Location : Catch ex As Exception : Return pFault.LogException(ex, "Location", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("AccountantEmail") = _AccountantEmail : Catch ex As Exception : Return pFault.LogException(ex, "AccountantEmail", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("AccountantMethod") = _AccountantMethod : Catch ex As Exception : Return pFault.LogException(ex, "AccountantMethod", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("InvoiceName") = _InvoiceName : Catch ex As Exception : Return pFault.LogException(ex, "InvoiceName", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProfitabilityCode") = _ProfitabilityCode : Catch ex As Exception : Return pFault.LogException(ex, "ProfitabilityCode", "TRGT-Customer-130316-0852", vRequester) : End Try 
    Try : vDataRow("CustomerIdentifier") = _CustomerIdentifier : Catch ex As Exception : Return pFault.LogException(ex, "CustomerIdentifier", "TRGT-Customer-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pCustomer As clsCustomer = CType(pXmlSerializer.Deserialize(pStreamReader), clsCustomer) 
      AssignValues(pCustomer) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-Customer-130515-1230", vRequester) 
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
          'CustomerCode 
          If _CustomerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CustomerCode) 
          'CustomerName 
          If _CustomerName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CustomerName) 
          'Phone 
          If _Phone Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Phone) 
          'Email 
          If _Email Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Email) 
          'Address 
          If _Address Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Address) 
          'City 
          If _City Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_City) 
          'TaxID 
          If _TaxID Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_TaxID) 
          'CustomerType 
          pBinaryWriter.Write(_CustomerType.FastToString()) 
          'PaymentTermsDays 
          pBinaryWriter.Write(_PaymentTermsDays) 
          'Notes 
          If _Notes Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Notes) 
          'IsActive 
          pBinaryWriter.Write(_IsActive) 
          'Location 
          If _Location Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Location) 
          'AccountantEmail 
          If _AccountantEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_AccountantEmail) 
          'AccountantMethod 
          pBinaryWriter.Write(_AccountantMethod.FastToString()) 
          'InvoiceName 
          If _InvoiceName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_InvoiceName) 
          'ProfitabilityCode 
          If _ProfitabilityCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProfitabilityCode) 
          'CustomerIdentifier 
          If _CustomerIdentifier Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CustomerIdentifier) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          'BeehiveBuyerTrackings  
          If _BeehiveBuyerTrackings IsNot Nothing Then 
            pObjectBytes = _BeehiveBuyerTrackings.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'CustomerDebts  
          If _CustomerDebts IsNot Nothing Then 
            pObjectBytes = _CustomerDebts.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'OrderHeaders  
          If _OrderHeaders IsNot Nothing Then 
            pObjectBytes = _OrderHeaders.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, pFunctionParameters, "TRGT-Customer-150307-2338", vRequester) 
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
          'CustomerCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CustomerCode = pReader.ReadString 
          'CustomerName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CustomerName = pReader.ReadString 
          'Phone 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Phone = pReader.ReadString 
          'Email 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Email = pReader.ReadString 
          'Address 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Address = pReader.ReadString 
          'City 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _City = pReader.ReadString 
          'TaxID 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _TaxID = pReader.ReadString 
          'CustomerType 
          _CustomerType = clsEnums.TranslateEnmCustomerType(pReader.ReadString) 
          'PaymentTermsDays 
          _PaymentTermsDays = pReader.ReadInt32 
          'Notes 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Notes = pReader.ReadString 
          'IsActive 
          _IsActive = pReader.ReadBoolean 
          'Location 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Location = pReader.ReadString 
          'AccountantEmail 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _AccountantEmail = pReader.ReadString 
          'AccountantMethod 
          _AccountantMethod = clsEnums.TranslateEnmAccountantMethod(pReader.ReadString) 
          'InvoiceName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _InvoiceName = pReader.ReadString 
          'ProfitabilityCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProfitabilityCode = pReader.ReadString 
          'CustomerIdentifier 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CustomerIdentifier = pReader.ReadString 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          'BeehiveBuyerTrackings 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _BeehiveBuyerTrackings = New clsBeehiveBuyerTrackingCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'CustomerDebts 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _CustomerDebts = New clsCustomerDebtCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'OrderHeaders 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _OrderHeaders = New clsOrderHeaderCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-Customer-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-190720-1443", vRequester) 
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
 
      Dim pCustomer As clsCustomer = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsCustomer)(vJSON, pSettings) 
      AssignValues(pCustomer) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vCustomer As clsCustomer)
    With vCustomer
      _ID = .ID 
      _CustomerCode = .CustomerCode 
      _CustomerName = .CustomerName 
      _Phone = .Phone 
      _Email = .Email 
      _Address = .Address 
      _City = .City 
      _TaxID = .TaxID 
      _CustomerType = .CustomerType 
      _CustomerTypeText = .CustomerTypeText
      _PaymentTermsDays = .PaymentTermsDays 
      _Notes = .Notes 
      _IsActive = .IsActive 
      _Location = .Location 
      _AccountantEmail = .AccountantEmail 
      _AccountantMethod = .AccountantMethod 
      _AccountantMethodText = .AccountantMethodText
      _InvoiceName = .InvoiceName 
      _ProfitabilityCode = .ProfitabilityCode 
      _CustomerIdentifier = .CustomerIdentifier 
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
 
    Dim pTextToGet As String = "" 
    Try 
      'CustomerType 
      pTextToGet = "CustomerTypeText (Enum)" 
      _CustomerTypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.CustomerType, _CustomerType.FastToString(), vRequester) 
      'AccountantMethod 
      pTextToGet = "AccountantMethodText (Enum)" 
      _AccountantMethodText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.AccountantMethod, _AccountantMethod.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-Customer-151124-1900", vRequester) 
    End Try 
 
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
      pLastReadVariableName = "CustomerCode" 
      If Not vReader.IsDBNull(1) Then _CustomerCode = vReader.GetString(1) 
      pLastReadVariableName = "CustomerName" 
      If Not vReader.IsDBNull(2) Then _CustomerName = vReader.GetString(2) 
      pLastReadVariableName = "Phone" 
      If Not vReader.IsDBNull(3) Then _Phone = vReader.GetString(3) 
      pLastReadVariableName = "Email" 
      If Not vReader.IsDBNull(4) Then _Email = vReader.GetString(4) 
      pLastReadVariableName = "Address" 
      If Not vReader.IsDBNull(5) Then _Address = vReader.GetString(5) 
      pLastReadVariableName = "City" 
      If Not vReader.IsDBNull(6) Then _City = vReader.GetString(6) 
      pLastReadVariableName = "TaxID" 
      If Not vReader.IsDBNull(7) Then _TaxID = vReader.GetString(7) 
      pLastReadVariableName = "enmCustomerType" 
      If Not vReader.IsDBNull(8) Then _CustomerType = clsEnums.TranslateEnmCustomerType(vReader.GetString(8))
      pLastReadVariableName = "PaymentTermsDays" 
      If Not vReader.IsDBNull(9) Then _PaymentTermsDays = vReader.GetInt32(9)
      pLastReadVariableName = "Notes" 
      If Not vReader.IsDBNull(10) Then _Notes = vReader.GetString(10) 
      pLastReadVariableName = "blg_IsActive" 
      If Not vReader.IsDBNull(11) Then _IsActive = vReader.GetBoolean(11)
      pLastReadVariableName = "Location" 
      If Not vReader.IsDBNull(12) Then _Location = vReader.GetString(12) 
      pLastReadVariableName = "AccountantEmail" 
      If Not vReader.IsDBNull(13) Then _AccountantEmail = vReader.GetString(13) 
      pLastReadVariableName = "enmAccountantMethod" 
      If Not vReader.IsDBNull(14) Then _AccountantMethod = clsEnums.TranslateEnmAccountantMethod(vReader.GetString(14))
      pLastReadVariableName = "InvoiceName" 
      If Not vReader.IsDBNull(15) Then _InvoiceName = vReader.GetString(15) 
      pLastReadVariableName = "ProfitabilityCode" 
      If Not vReader.IsDBNull(16) Then _ProfitabilityCode = vReader.GetString(16) 
      pLastReadVariableName = "clc_CustomerIdentifier" 
      If Not vReader.IsDBNull(17) Then _CustomerIdentifier = vReader.GetString(17) 
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(18) Then bDateAdded = vReader.GetDateTime(18)   
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedCustomer As clsCustomer, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedCustomer) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _CustomerCode = ""
    _CustomerName = ""
    _Phone = ""
    _Email = ""
    _Address = ""
    _City = ""
    _TaxID = ""
    'Default Value set by SQL Server Database (below): Private
    _CustomerType = clsEnums.enmCustomerType.Private
    _CustomerTypeText = ""
    'Default Value set by SQL Server Database (below): 0
    _PaymentTermsDays = 0
    _Notes = ""
    'Default Value set by SQL Server Database (below): 1
    _IsActive = True
    _Location = ""
    _AccountantEmail = ""
    _AccountantMethod = clsEnums.enmAccountantMethod.UD
    _AccountantMethodText = ""
    _InvoiceName = ""
    _ProfitabilityCode = ""
    _CustomerIdentifier = ""
    _Tag = ""
    _BeehiveBuyerTrackings = Nothing
    _CustomerDebts = Nothing
    _OrderHeaders = Nothing
    _IsCleanForXML = False 
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
  
Public Class clsCustomerCol
  Inherits cTargCCCollection(Of clsCustomer)
  Implements ITargCCCollectionUpdateable 
  Implements ITargCCDataReaderUser 
  
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsCustomer) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByCustomerCode As Dictionary(Of String, clsCustomer) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByCustomerCode As Boolean 
  Private Function CreateKeyForFindByCustomerCode(ByVal vCustomer As clsCustomer) As String 
    With vCustomer 
      Return .CustomerCode
    End With 
  End Function 
   
  Private _IsCleanForXML As Boolean 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
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
 
    For Each pRow As clsCustomer In Me 
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
    pCSVTitle.Append(",""CustomerCode""") 
    pCSVTitle.Append(",""CustomerName""") 
    pCSVTitle.Append(",""Phone""") 
    pCSVTitle.Append(",""Email""") 
    pCSVTitle.Append(",""Address""") 
    pCSVTitle.Append(",""City""") 
    pCSVTitle.Append(",""TaxID""") 
    pCSVTitle.Append(",""CustomerType" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""CustomerType (Text)""") 
    pCSVTitle.Append(",""PaymentTermsDays""") 
    pCSVTitle.Append(",""Notes""") 
    pCSVTitle.Append(",""IsActive""") 
    pCSVTitle.Append(",""Location""") 
    pCSVTitle.Append(",""AccountantEmail""") 
    pCSVTitle.Append(",""AccountantMethod" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""AccountantMethod (Text)""") 
    pCSVTitle.Append(",""InvoiceName""") 
    pCSVTitle.Append(",""ProfitabilityCode""") 
    pCSVTitle.Append(",""CustomerIdentifier""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As clsCustomer In Me 
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
 
  Public Overloads Sub Add(ByVal vCustomer As clsCustomer) 
    SyncLock _CollectionLock 
      MyBase.Add(vCustomer) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByCustomerCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vCustomer As clsCustomer) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vCustomer) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByCustomerCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vCustomerCol As clsCustomerCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vCustomerCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByCustomerCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByCustomerCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vCustomer As clsCustomer) 
    SyncLock _CollectionLock 
      MyBase.Remove(vCustomer) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByCustomerCode = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, clsCustomer) 
      
      For Each lCustomer In Me 
        If lCustomer.IsEmpty OrElse pTempDictionary.ContainsKey(lCustomer.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lCustomer.ID, lCustomer) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lCustomer.ToString, "TRGT-Customer-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", Customer:" & lCustomer.ToString() & ", TRGT-Customer-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadCustomerCodes() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByCustomerCode Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByCustomerCode Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByCustomerCode = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByCustomerCode' yet!
      Dim pTempDictionary As New Dictionary(Of String, clsCustomer)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lCustomer In Me 
        Try 
          Dim pCustomerCode As String = CreateKeyForFindByCustomerCode(lCustomer) 
          If String.IsNullOrEmpty(pCustomerCode.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pCustomerCode)) Then 
            pTempDictionary.Add(pCustomerCode, lCustomer) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lCustomer.ToString, "TRGT-Customer-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByCustomerCode:" & ex.Message & ", Customer:" & lCustomer.ToString() & ", TRGT-Customer-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByCustomerCode = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByCustomerCode = False
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
 
    For Each lCustomer As clsCustomer In Me 
      lCustomer.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lCustomer As clsCustomer In Me 
      lCustomer.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [CustomerType] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the Customers by the chosen parameters. This function may be a bit slower than accessing the Customer's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.CustomerType 
          pFault = FillByCustomerType(clsEnums.TranslateEnmCustomerType(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Customer-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Customer-151223_1716", vRequester) 
    End Try 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillByParameters", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
    End If 
 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      Dim pCustomersCached As clsCustomerCol = MyController.DBCache.CustomerCol.Clone() 
      pCustomersCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pCustomersCached.Reverse() 
      If vHowMany > 0 AndAlso pCustomersCached.Count > vHowMany Then 
        Dim tmp As New clsCustomerCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pCustomersCached(i)) 
        Next 
        pCustomersCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pCustomersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomersFill"
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific CustomerType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByCustomerType(ByVal vCustomerType As clsEnums.enmCustomerType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerType={0}", vCustomerType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillByCustomerType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      Dim pCustomersCached As clsCustomerCol = MyController.DBCache.CustomerCol.CloneByCustomerType(vCustomerType)
      pFault = LoadMeFromDBCache(pCustomersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomersFillByCustomerType" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmCustomerType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vCustomerType.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillByCustomerType", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      Dim pCustomersCached As clsCustomerCol = MyController.DBCache.CustomerCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pCustomersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomersFillByBoundedID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillByBoundedID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific CustomerCode, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedCustomerCode(ByVal vCustomerCodeFrom As String, ByVal vCustomerCodeTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerCodeFrom={0}, CustomerCodeTo={1}", vCustomerCodeFrom, vCustomerCodeTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillByBoundedCustomerCode", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerCol failed: " & pResponse) 
      Dim pCustomersCached As clsCustomerCol = MyController.DBCache.CustomerCol.CloneByBoundedCustomerCode(vCustomerCodeFrom, vCustomerCodeTo)
      pFault = LoadMeFromDBCache(pCustomersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomersFillByBoundedCustomerCode" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "CustomerCodeFrom" 
        pDALParameters.Add("bndCustomerCodeFrom", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vCustomerCodeFrom) 
        pLastReadVariableName = "CustomerCodeTo" 
        pDALParameters.Add("bndCustomerCodeTo", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vCustomerCodeTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillByBoundedCustomerCode", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded CustomerCode, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardCustomerCode(ByVal vCustomerCode As String, ByVal vCustomerCodeWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerCode={0}, CustomerCodeWildcardType={1}", vCustomerCode, vCustomerCodeWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillByWildCardCustomerCode", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'CustomerCode 
    Dim pWCCustomerCode As String = "" 
    If vCustomerCodeWildcardType = clsEnums.enmWildCardType.After Then 
      pWCCustomerCode = vCustomerCode & "%" 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCCustomerCode = "%" & vCustomerCode 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCCustomerCode = "%" & vCustomerCode & "%" 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vCustomerCode.ToCharArray 
        pWCCustomerCode &= p & "%" 
      Next 
      pWCCustomerCode = "%" & pWCCustomerCode 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-Customer-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomersFillByWildCardCustomerCode" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldCustomerCode" 
        pDALParameters.Add("wldCustomerCode", ccDAL.enmSQLDataType.NVarChar, 50).Value = (pWCCustomerCode) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillByWildCardCustomerCode", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lCustomer As New clsCustomer() 
      pFault = lCustomer.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lCustomer.IsEmpty Then Me.Add(lCustomer) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
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
    [CustomerCode]
    CustomerCodeWildcardType
    [CustomerType]
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pCustomerCode As String = Nothing
    Dim pCustomerCodeWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pCustomerType As clsEnums.enmCustomerType = clsEnums.enmCustomerType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerCode) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerCode) : If pObj IsNot Nothing Then pCustomerCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerCodeWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerCodeWildcardType) : If pObj IsNot Nothing Then pCustomerCodeWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerType) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerType) : If pObj IsNot Nothing Then pCustomerType = CType(pObj, clsEnums.enmCustomerType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pCustomerCode, pCustomerCodeWildcardType _
        , pCustomerType _
        , vRequester, pHowMany, pDir) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
    End If 
 
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
        , ByVal vCustomerCode As String, ByVal vCustomerCodeWildcardType As clsEnums.enmWildCardType _
        , ByVal vCustomerType As clsEnums.enmCustomerType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, CustomerCode={2}, CustomerCodeWildcardType={3}, CustomerType={4}", vIDFrom, vIDTo, vCustomerCode, vCustomerCodeWildcardType.FastToString(), vCustomerType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'CustomerCode 
    Dim pWCCustomerCode As String = "" 
    If vCustomerCode = Nothing Then 
      pWCCustomerCode = vCustomerCode
    Else 
      If vCustomerCodeWildcardType = clsEnums.enmWildCardType.None OrElse vCustomerCodeWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCCustomerCode = vCustomerCode
      ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.After Then 
        pWCCustomerCode = vCustomerCode & "%" 
      ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCCustomerCode = "%" & vCustomerCode 
      ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCCustomerCode = "%" & vCustomerCode & "%" 
      ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vCustomerCode.ToCharArray 
          pWCCustomerCode &= p & "%" 
        Next 
        pWCCustomerCode = "%" & pWCCustomerCode 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Customer-121122-2008", vRequester) 
      Dim pCustomersCached As clsCustomerCol = MyController.DBCache.CustomerCol.Clone() 
      Dim pCustomersToUse As New clsCustomerCol() 
      For Each l In pCustomersCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vCustomerCode) Then 
          If vCustomerCodeWildcardType = clsEnums.enmWildCardType.UD OrElse vCustomerCodeWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.CustomerCode.Equals(vCustomerCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.CustomerCode.StartsWith(vCustomerCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.CustomerCode.EndsWith(vCustomerCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.CustomerCode.IndexOf(vCustomerCode, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vCustomerType <> clsEnums.enmCustomerType.UD Then 
          If l.CustomerType <> vCustomerType Then Continue For 
        End If 
        pCustomersToUse.Add(l) 
      Next 
      pCustomersToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pCustomersToUse.Reverse() 
      If vHowMany > 0 AndAlso pCustomersToUse.Count > vHowMany Then 
        Dim tmp As New clsCustomerCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pCustomersToUse(i)) 
        Next 
        pCustomersToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pCustomersToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomersFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "CustomerCode" 
        pDALParameters.Add("wldCustomerCode", ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(pWCCustomerCode) 
        pLastReadVariableName = "enmCustomerType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(vCustomerType.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByCustomerType
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pCustomerCode As String = Nothing
    Dim pCustomerCodeWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pCustomerType As clsEnums.enmCustomerType = clsEnums.enmCustomerType.UD
    Dim pGroupByCustomerType As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerCode) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerCode) : If pObj IsNot Nothing Then pCustomerCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerCodeWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerCodeWildcardType) : If pObj IsNot Nothing Then pCustomerCodeWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerType) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerType) : If pObj IsNot Nothing Then pCustomerType = CType(pObj, clsEnums.enmCustomerType) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByCustomerType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByCustomerType) : If pObj IsNot Nothing Then pGroupByCustomerType = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pCustomerCode, pCustomerCodeWildcardType _
        , pCustomerType _
        , pGroupByCustomerType _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a grouped collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillSumOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vCustomerCode As String, ByVal vCustomerCodeWildcardType As clsEnums.enmWildCardType _
        , ByVal vCustomerType As clsEnums.enmCustomerType _
        , ByVal vGroupByCustomerType As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, CustomerCode={2}, CustomerCodeWildcardType={3}, CustomerType={4}, GroupByCustomerType={5}", vIDFrom, vIDTo, vCustomerCode, vCustomerCodeWildcardType.FastToString(), vCustomerType, vGroupByCustomerType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'CustomerCode 
    Dim pWCCustomerCode As String = "" 
    If vCustomerCode = Nothing Then 
      pWCCustomerCode = vCustomerCode
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.None OrElse vCustomerCodeWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCCustomerCode = vCustomerCode
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.After Then 
      pWCCustomerCode = vCustomerCode & "%" 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCCustomerCode = "%" & vCustomerCode 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCCustomerCode = "%" & vCustomerCode & "%" 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vCustomerCode.ToCharArray 
        pWCCustomerCode &= p & "%" 
      Next 
      pWCCustomerCode = "%" & pWCCustomerCode 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Customer-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomersFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "CustomerCode" 
        pDALParameters.Add("wldCustomerCode", ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(pWCCustomerCode) 
        pLastReadVariableName = "enmCustomerType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(vCustomerType) 
        pLastReadVariableName = "enmCustomerType" 
        pDALParameters.Add("GroupByenmCustomerType", ccDAL.enmSQLDataType.Bit).Value = vGroupByCustomerType
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomers As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomers, "clsCustomerCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomers IsNot Nothing AndAlso Me.Count <> pCustomers.Count Then FillFromListOfITargCCEntity(pCustomers) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vCustomerArray As clsCustomer())
    Me.Clear()
    
    For Each pCustomer As clsCustomer In vCustomerArray
      Me.Add(pCustomer)
      _Clean.Add(pCustomer.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pCustomer As New clsCustomer(pRow, vRequester) 
        Me.Add(pCustomer) 
        _Clean.Add(pCustomer.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-CustomerCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-130515-1300", vRequester) 
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
      Dim pCustomers As clsCustomerCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsCustomerCol) 
      For Each pCustomer As clsCustomer In pCustomers 
        Me.Add(pCustomer) 
        _Clean.Add(pCustomer.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-Customer-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-190720-1443", vRequester) 
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
 
      Dim pCustomers As List(Of clsCustomer) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of clsCustomer))(vJSON, pSettings) 
      For Each pCustomer As clsCustomer In pCustomers 
        Me.Add(pCustomer) 
        _Clean.Add(pCustomer.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-190720-2059", vRequester) 
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
          For Each lCustomer As clsCustomer In Me 
            Dim pByte As Byte() = lCustomer.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Customer-150307-2340", vRequester) 
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
            Dim pCustomer As clsCustomer = New clsCustomer(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pCustomer) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pCustomer.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-Customer-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pCustomer As clsCustomer In Me 
      With pCustomer 
        pFault = pCustomer.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is clsCustomerCol) Then Return False 
    Dim pCustomerColToTest As clsCustomerCol = CType(vEntitiesToTest, clsCustomerCol) 
    Return isEqual(pCustomerColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vCustomersToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vCustomersToTest As clsCustomerCol) As Boolean
    If Me.Count <> vCustomersToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vCustomersToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pCustomers As New clsCustomerCol() 
    If pFilledFromSumOnTheFly Then pCustomers._FilledFromSumOnTheFly = True
    
    For Each pCustomer As clsCustomer In Me 
      Dim pCustomerClone As clsCustomer = pCustomer.Clone() 
      pCustomers.Add(pCustomerClone) 
      If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
    Next 
    Return pCustomers 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsCustomerCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pCustomers As New clsCustomerCol() 
    If pFilledFromSumOnTheFly Then pCustomers._FilledFromSumOnTheFly = True
    
    For Each pCustomer As clsCustomer In Me
      Dim pCustomerClone As clsCustomer = pCustomer.Clone()
      pCustomers.Add(pCustomerClone)
      If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
    Next
    Return pCustomers
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As clsCustomerCol 
    Dim pCustomers As New clsCustomerCol()  
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pCustomer As clsCustomer In _SortedDictionaryForFindByID.Values.ToList() 
      If (pCustomer.ID > vIDFrom AndAlso pCustomer.ID <= vIDTo) Then 
        Dim pCustomerClone As clsCustomer = pCustomer.Clone() 
        pCustomers.Add(pCustomerClone) 
        If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
      End If 
    Next 
    Return pCustomers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by CustomerCode (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedCustomerCode(ByVal vCustomerCodeFrom As String, ByVal vCustomerCodeTo As String) As clsCustomerCol 
    Dim pCustomers As New clsCustomerCol()  
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pCustomer As clsCustomer In _SortedDictionaryForFindByID.Values.ToList() 
      If (pCustomer.CustomerCode > vCustomerCodeFrom AndAlso pCustomer.CustomerCode <= vCustomerCodeTo) Then 
        Dim pCustomerClone As clsCustomer = pCustomer.Clone() 
        pCustomers.Add(pCustomerClone) 
        If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
      End If 
    Next 
    Return pCustomers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardCustomerCode(ByVal vCustomerCode As String, ByVal vCustomerCodeWildcardType As clsEnums.enmWildCardType) As clsCustomerCol 
    Dim pCustomers As New clsCustomerCol 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pCustomer As clsCustomer In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vCustomerCodeWildcardType = clsEnums.enmWildCardType.After Then 
        If pCustomer.CustomerCode.StartsWith(vCustomerCode, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.Before Then 
        If pCustomer.CustomerCode.EndsWith(vCustomerCode, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pCustomer.CustomerCode.IndexOf(vCustomerCode, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vCustomerCode.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pCustomer.CustomerCode.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pCustomerClone As clsCustomer = pCustomer.Clone() 
      pCustomers.Add(pCustomerClone) 
    Next 
    Return pCustomers 
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
  Public Function FindByID(ByVal vID As Long) As clsCustomer
    If Me.Count = 0 Then Return New clsCustomer 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
    
    Dim pCustomer As clsCustomer = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pCustomer) 
    If pCustomer IsNot Nothing Then Return pCustomer Else Return New clsCustomer() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByCustomerCode(ByVal vCustomerCode As String) As clsCustomer
    If Me.Count = 0 Then Return New clsCustomer 
    
    If _RecreateDictionaryForFindByCustomerCode = True Then LoadCustomerCodes() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, clsCustomer) = _SortedDictionaryForFindByCustomerCode 
    
    Dim pCustomer As clsCustomer = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vCustomerCode
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pCustomer) 
    If pCustomer IsNot Nothing Then Return pCustomer Else Return New clsCustomer() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CustomerCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCustomerCode(ByVal vCustomerCode As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCustomerCode = vCustomerCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.CustomerCode.ToLowerInvariant() = vCustomerCode Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCustomerCode with vCustomerCode of {vCustomerCode}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.CustomerCode.ToLowerInvariant() = vCustomerCode Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CustomerName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCustomerName(ByVal vCustomerName As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCustomerName = vCustomerName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.CustomerName.ToLowerInvariant() = vCustomerName Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCustomerName with vCustomerName of {vCustomerName}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.CustomerName.ToLowerInvariant() = vCustomerName Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Phone
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPhone(ByVal vPhone As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vPhone = vPhone.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.Phone.ToLowerInvariant() = vPhone Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPhone with vPhone of {vPhone}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.Phone.ToLowerInvariant() = vPhone Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Email
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEmail(ByVal vEmail As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEmail = vEmail.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.Email.ToLowerInvariant() = vEmail Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEmail with vEmail of {vEmail}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.Email.ToLowerInvariant() = vEmail Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Address
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAddress(ByVal vAddress As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vAddress = vAddress.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.Address.ToLowerInvariant() = vAddress Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAddress with vAddress of {vAddress}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.Address.ToLowerInvariant() = vAddress Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined City
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCity(ByVal vCity As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCity = vCity.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.City.ToLowerInvariant() = vCity Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCity with vCity of {vCity}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.City.ToLowerInvariant() = vCity Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TaxID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTaxID(ByVal vTaxID As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTaxID = vTaxID.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.TaxID.ToLowerInvariant() = vTaxID Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTaxID with vTaxID of {vTaxID}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.TaxID.ToLowerInvariant() = vTaxID Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CustomerType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCustomerType(ByVal vCustomerType As clsEnums.enmCustomerType) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.CustomerType = vCustomerType Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCustomerType with vCustomerType of {vCustomerType}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.CustomerType = vCustomerType Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PaymentTermsDays
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPaymentTermsDays(ByVal vPaymentTermsDays As Integer) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.PaymentTermsDays = vPaymentTermsDays Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPaymentTermsDays with vPaymentTermsDays of {vPaymentTermsDays}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.PaymentTermsDays = vPaymentTermsDays Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Notes
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNotes(ByVal vNotes As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNotes = vNotes.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.Notes.ToLowerInvariant() = vNotes Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNotes with vNotes of {vNotes}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.Notes.ToLowerInvariant() = vNotes Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IsActive
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIsActive(ByVal vIsActive As Boolean) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.IsActive = vIsActive Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIsActive with vIsActive of {vIsActive}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.IsActive = vIsActive Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Location
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLocation(ByVal vLocation As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLocation = vLocation.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.Location.ToLowerInvariant() = vLocation Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLocation with vLocation of {vLocation}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.Location.ToLowerInvariant() = vLocation Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AccountantEmail
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAccountantEmail(ByVal vAccountantEmail As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vAccountantEmail = vAccountantEmail.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.AccountantEmail.ToLowerInvariant() = vAccountantEmail Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAccountantEmail with vAccountantEmail of {vAccountantEmail}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.AccountantEmail.ToLowerInvariant() = vAccountantEmail Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AccountantMethod
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAccountantMethod(ByVal vAccountantMethod As clsEnums.enmAccountantMethod) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.AccountantMethod = vAccountantMethod Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAccountantMethod with vAccountantMethod of {vAccountantMethod}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.AccountantMethod = vAccountantMethod Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined InvoiceName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByInvoiceName(ByVal vInvoiceName As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vInvoiceName = vInvoiceName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.InvoiceName.ToLowerInvariant() = vInvoiceName Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByInvoiceName with vInvoiceName of {vInvoiceName}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.InvoiceName.ToLowerInvariant() = vInvoiceName Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProfitabilityCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProfitabilityCode(ByVal vProfitabilityCode As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vProfitabilityCode = vProfitabilityCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.ProfitabilityCode.ToLowerInvariant() = vProfitabilityCode Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByProfitabilityCode with vProfitabilityCode of {vProfitabilityCode}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.ProfitabilityCode.ToLowerInvariant() = vProfitabilityCode Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CustomerIdentifier
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCustomerIdentifier(ByVal vCustomerIdentifier As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCustomerIdentifier = vCustomerIdentifier.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.CustomerIdentifier.ToLowerInvariant() = vCustomerIdentifier Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCustomerIdentifier with vCustomerIdentifier of {vCustomerIdentifier}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.CustomerIdentifier.ToLowerInvariant() = vCustomerIdentifier Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As clsCustomerCol
    Dim pCustomers As New clsCustomerCol() 
    pCustomers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomer) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomer As clsCustomer In pTempDist.Values
        If pCustomer.Tag.ToLowerInvariant() = vTag Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As clsCustomerCol = Me.Clone() 
      For Each pCustomer As clsCustomer In pList 
        If pCustomer.Tag.ToLowerInvariant() = vTag Then
          Dim pCustomerClone As clsCustomer = pCustomer.Clone()
          pCustomers.Add(pCustomerClone)
          If Not _FilledFromSumOnTheFly Then pCustomers._Clean.Add(pCustomer.ID) 
        End If
      Next
    End If 
    
    Return pCustomers
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
    For Each pCustomer As clsCustomer In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pCustomer.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerView, "clsCustomerCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As clsCustomer In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As clsCustomer = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pCustomerToKill As New clsCustomer 
          pCustomerToKill.ID = pCleanID 
          pCustomerToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pCustomerToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As clsCustomer In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-Customer-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerUpdate, "clsCustomerCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As clsCustomer In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As clsCustomer In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDelete, "clsCustomerCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccCustomersDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New clsCustomerCol(), vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Customer-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific CustomerType 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByCustomerType(ByVal vCustomerType As clsEnums.enmCustomerType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerType={0}", vCustomerType)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDelete, "clsCustomerCol_DeleteByCustomerType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccCustomersDeleteByCustomerType"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllCustomers As New clsCustomerCol() : pAllCustomers.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredCustomers As clsCustomerCol = pAllCustomers.CloneByCustomerType(vCustomerType) 
      For Each l In pFilteredCustomers 
        pAllCustomers.Remove(pAllCustomers.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllCustomers, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmCustomerType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vCustomerType) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Customer-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDelete, "clsCustomerCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccCustomersDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Customer-150216-2148", vRequester) 
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
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Customer-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific CustomerCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedCustomerCode(ByVal vCustomerCodeFrom As String, ByVal vCustomerCodeTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerCodeFrom={0}, CustomerCodeTo={1}", vCustomerCodeFrom, vCustomerCodeTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDelete, "clsCustomerCol_DeleteByBoundedCustomerCode", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccCustomersDeleteByBoundedCustomerCode"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Customer-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "CustomerCodeFrom" 
        pDALParameters.Add("bndCustomerCodeFrom", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vCustomerCodeFrom) 
        pLastReadVariableName = "CustomerCodeTo" 
        pDALParameters.Add("bndCustomerCodeTo", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vCustomerCodeTo) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Customer-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded CustomerCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardCustomerCode(ByVal vCustomerCode As String, ByVal vCustomerCodeWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerCode={0}, CustomerCodeWildcardType={1}", vCustomerCode, vCustomerCodeWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDelete, "clsCustomerCol_DeleteByWildCardCustomerCode", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'CustomerCode 
    Dim pWCCustomerCode As String = "" 
    If vCustomerCodeWildcardType = clsEnums.enmWildCardType.After Then 
      pWCCustomerCode = vCustomerCode & "%" 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCCustomerCode = "%" & vCustomerCode 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCCustomerCode = "%" & vCustomerCode & "%" 
    ElseIf vCustomerCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vCustomerCode.ToCharArray 
        pWCCustomerCode &= p & "%" 
      Next 
      pWCCustomerCode = "%" & pWCCustomerCode 
    End If 
    
    Dim pCommandText As String = "ccCustomersDeleteByWildCardCustomerCode"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Customer-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldCustomerCode" 
        pDALParameters.Add("wldCustomerCode", ccDAL.enmSQLDataType.NVarChar, 50).Value = (pWCCustomerCode) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Customer-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Customer-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-090219-1632", vRequester) 
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
    Me.Sort(New clsCustomerCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
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
  
  Public Sub SortByCustomerCode()
    Me.Sort(New clsCustomerCol.CompareByCustomerCode)
  End Sub
  Private Class CompareByCustomerCode
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CustomerCode, y.CustomerCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCustomerName()
    Me.Sort(New clsCustomerCol.CompareByCustomerName)
  End Sub
  Private Class CompareByCustomerName
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CustomerName, y.CustomerName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByPhone()
    Me.Sort(New clsCustomerCol.CompareByPhone)
  End Sub
  Private Class CompareByPhone
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Phone, y.Phone, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEmail()
    Me.Sort(New clsCustomerCol.CompareByEmail)
  End Sub
  Private Class CompareByEmail
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Email, y.Email, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAddress()
    Me.Sort(New clsCustomerCol.CompareByAddress)
  End Sub
  Private Class CompareByAddress
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Address, y.Address, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCity()
    Me.Sort(New clsCustomerCol.CompareByCity)
  End Sub
  Private Class CompareByCity
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.City, y.City, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTaxID()
    Me.Sort(New clsCustomerCol.CompareByTaxID)
  End Sub
  Private Class CompareByTaxID
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TaxID, y.TaxID, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCustomerType()
    Me.Sort(New clsCustomerCol.CompareByCustomerType)
  End Sub
  Private Class CompareByCustomerType
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.CustomerType < y.CustomerType Then
        Return -1
      ElseIf x.CustomerType = y.CustomerType Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByCustomerTypeText()
    Me.Sort(New clsCustomerCol.CompareByCustomerTypeText)
  End Sub
  Private Class CompareByCustomerTypeText
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CustomerTypeText, y.CustomerTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByPaymentTermsDays()
    Me.Sort(New clsCustomerCol.CompareByPaymentTermsDays)
  End Sub
  Private Class CompareByPaymentTermsDays
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.PaymentTermsDays < y.PaymentTermsDays Then
        Return -1
      ElseIf x.PaymentTermsDays = y.PaymentTermsDays Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByNotes()
    Me.Sort(New clsCustomerCol.CompareByNotes)
  End Sub
  Private Class CompareByNotes
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Notes, y.Notes, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByIsActive()
    Me.Sort(New clsCustomerCol.CompareByIsActive)
  End Sub
  Private Class CompareByIsActive
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IsActive.ToString, y.IsActive.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLocation()
    Me.Sort(New clsCustomerCol.CompareByLocation)
  End Sub
  Private Class CompareByLocation
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Location, y.Location, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAccountantEmail()
    Me.Sort(New clsCustomerCol.CompareByAccountantEmail)
  End Sub
  Private Class CompareByAccountantEmail
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AccountantEmail, y.AccountantEmail, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAccountantMethod()
    Me.Sort(New clsCustomerCol.CompareByAccountantMethod)
  End Sub
  Private Class CompareByAccountantMethod
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.AccountantMethod < y.AccountantMethod Then
        Return -1
      ElseIf x.AccountantMethod = y.AccountantMethod Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByAccountantMethodText()
    Me.Sort(New clsCustomerCol.CompareByAccountantMethodText)
  End Sub
  Private Class CompareByAccountantMethodText
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AccountantMethodText, y.AccountantMethodText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByInvoiceName()
    Me.Sort(New clsCustomerCol.CompareByInvoiceName)
  End Sub
  Private Class CompareByInvoiceName
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.InvoiceName, y.InvoiceName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProfitabilityCode()
    Me.Sort(New clsCustomerCol.CompareByProfitabilityCode)
  End Sub
  Private Class CompareByProfitabilityCode
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProfitabilityCode, y.ProfitabilityCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCustomerIdentifier()
    Me.Sort(New clsCustomerCol.CompareByCustomerIdentifier)
  End Sub
  Private Class CompareByCustomerIdentifier
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CustomerIdentifier, y.CustomerIdentifier, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New clsCustomerCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsCustomer)
    Private Function Compare(ByVal x As clsCustomer, ByVal y As clsCustomer) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomer).Compare
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
  
    Dim pCustomer As clsCustomer
  
    While vReader.Read()
      pCustomer = New clsCustomer() 
      pFault = pCustomer.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pCustomer)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pCustomer.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedCustomerCol As clsCustomerCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pCustomer As clsCustomer 
 
      For Each pCachedCustomer As clsCustomer In vCachedCustomerCol 
        pCustomer = New clsCustomer(pCachedCustomer) 
        pCustomer.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pCustomer) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pCustomer.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Customer-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsCustomer) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByCustomerCode = New Dictionary(Of String, clsCustomer)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByCustomerCode = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsCustomer) 
    _SortedDictionaryForFindByCustomerCode = New Dictionary(Of String, clsCustomer)(StringComparer.OrdinalIgnoreCase) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
