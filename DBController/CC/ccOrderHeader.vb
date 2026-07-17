Public Class clsOrderHeader
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
    [Customer] 
    [PaymentMethod] 
    [PaymentStatus] 
    [DeliveryMethod] 
    [DeliveryDay] 
    [OrderStatus] 
  End Enum 
  'Child Properties 
  Public Enum enmChildProperty 
    UD 
    [CustomerDebt] 
    [Delivery] 
    [OrderLine] 
    [SupplierOrder] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [OrderNumber] 
    [Customer] 
    [OrderDate] 
    [TotalAmount] 
    [VATAmount] 
    [TotalWithVAT] 
    [PaymentMethod] 
    [PaymentStatus] 
    [PaymentDate] 
    [InvoiceNumber] 
    [DeliveryMethod] 
    [DeliveryDate] 
    [DeliveryDay] 
    [OrderStatus] 
    [Notes] 
    [Notes2] 
    [OrderMonth] 
    [Quarter] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [OrderNumber] 
    [TotalAmount] 
    [VATAmount] 
    [TotalWithVAT] 
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
  Private _OrderNumber As Integer
  Private _CustomerID As Long
  Private _Customer As clsCustomer
  Private _CustomerText As String
  Private _OrderDate As Date
  Private _TotalAmount As Decimal
  Private _VATAmount As Decimal
  Private _TotalWithVAT As Decimal
  Private _PaymentMethod As clsEnums.enmPaymentMethod
  Private _PaymentMethodText As String 
  Private _PaymentStatus As clsEnums.enmPaymentStatus
  Private _PaymentStatusText As String 
  Private _PaymentDate As Date
  Private _InvoiceNumber As String
  Private _DeliveryMethod As clsEnums.enmDeliveryMethod
  Private _DeliveryMethodText As String 
  Private _DeliveryDate As Date
  Private _DeliveryDay As clsEnums.enmDeliveryDay
  Private _DeliveryDayText As String 
  Private _OrderStatus As clsEnums.enmOrderStatus
  Private _OrderStatusText As String 
  Private _Notes As String
  Private _Notes2 As String
  Private _OrderMonth As String
  Private _Quarter As String
  Private _Tag As String
  Private _CustomerDebts As clsCustomerDebtCol
  Private _Deliverys As clsDeliveryCol
  Private _OrderLines As clsOrderLineCol
  Private _SupplierOrders As clsSupplierOrderCol
  
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
  Public Property [OrderNumber]() As Integer
    Get
      Return Me._OrderNumber
    End Get
    Set(ByVal value As Integer)
      If Me._OrderNumber <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OrderNumber = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [CustomerID]() As Long
    Get
      Return Me._CustomerID
    End Get
    Set(ByVal value As Long)
      If Me._CustomerID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CustomerID = value 
      End If 
    End Set
  End Property
  Public Property [Customer]() As clsCustomer
    Get
      Return Me._Customer
    End Get
    Set(ByVal value As clsCustomer)
      Me._Customer = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the Customer object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property CustomerText() As String
    Get
      Return Me._CustomerText
    End Get
    Set(ByVal value As String)
      Me._CustomerText = value
    End Set
  End Property
  Public Property [OrderDate]() As Date
    Get
      Return Me._OrderDate
    End Get
    Set(ByVal value As Date)
      If Me._OrderDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OrderDate = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [TotalAmount]() As Decimal
    Get
      Return Me._TotalAmount
    End Get
  End Property
  Public ReadOnly Property [VATAmount]() As Decimal
    Get
      Return Me._VATAmount
    End Get
  End Property
  Public ReadOnly Property [TotalWithVAT]() As Decimal
    Get
      Return Me._TotalWithVAT
    End Get
  End Property
  Public Property [PaymentMethod]() As clsEnums.enmPaymentMethod
    Get
      Return Me._PaymentMethod
    End Get
    Set(ByVal value As clsEnums.enmPaymentMethod)
      If Me._PaymentMethod <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PaymentMethod = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [PaymentMethodText]() As String
    Get
      Return Me._PaymentMethodText
    End Get
    Set(ByVal value As String)
      Me._PaymentMethodText = value
    End Set
  End Property
  Public Property [PaymentStatus]() As clsEnums.enmPaymentStatus
    Get
      Return Me._PaymentStatus
    End Get
    Set(ByVal value As clsEnums.enmPaymentStatus)
      If Me._PaymentStatus <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PaymentStatus = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [PaymentStatusText]() As String
    Get
      Return Me._PaymentStatusText
    End Get
    Set(ByVal value As String)
      Me._PaymentStatusText = value
    End Set
  End Property
  Public Property [PaymentDate]() As Date
    Get
      Return Me._PaymentDate
    End Get
    Set(ByVal value As Date)
      If Me._PaymentDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PaymentDate = value 
      End If 
    End Set
  End Property
  Public Property [InvoiceNumber]() As String
    Get
      Return Me._InvoiceNumber
    End Get
    Set(ByVal value As String)
      If Me._InvoiceNumber <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._InvoiceNumber = value 
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
  Public Property [DeliveryDate]() As Date
    Get
      Return Me._DeliveryDate
    End Get
    Set(ByVal value As Date)
      If Me._DeliveryDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DeliveryDate = value 
      End If 
    End Set
  End Property
  Public Property [DeliveryDay]() As clsEnums.enmDeliveryDay
    Get
      Return Me._DeliveryDay
    End Get
    Set(ByVal value As clsEnums.enmDeliveryDay)
      If Me._DeliveryDay <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DeliveryDay = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [DeliveryDayText]() As String
    Get
      Return Me._DeliveryDayText
    End Get
    Set(ByVal value As String)
      Me._DeliveryDayText = value
    End Set
  End Property
  Public Property [OrderStatus]() As clsEnums.enmOrderStatus
    Get
      Return Me._OrderStatus
    End Get
    Set(ByVal value As clsEnums.enmOrderStatus)
      If Me._OrderStatus <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OrderStatus = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [OrderStatusText]() As String
    Get
      Return Me._OrderStatusText
    End Get
    Set(ByVal value As String)
      Me._OrderStatusText = value
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
  Public Property [Notes2]() As String
    Get
      Return Me._Notes2
    End Get
    Set(ByVal value As String)
      If Me._Notes2 <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Notes2 = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [OrderMonth]() As String
    Get
      Return Me._OrderMonth
    End Get
  End Property
  Public ReadOnly Property [Quarter]() As String
    Get
      Return Me._Quarter
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
  Public Property [CustomerDebts]() As clsCustomerDebtCol
    Get
      Return Me._CustomerDebts
    End Get
    Set(ByVal value As clsCustomerDebtCol)
      Me._CustomerDebts = value
    End Set
  End Property
  Public Property [Deliverys]() As clsDeliveryCol
    Get
      Return Me._Deliverys
    End Get
    Set(ByVal value As clsDeliveryCol)
      Me._Deliverys = value
    End Set
  End Property
  Public Property [OrderLines]() As clsOrderLineCol
    Get
      Return Me._OrderLines
    End Get
    Set(ByVal value As clsOrderLineCol)
      Me._OrderLines = value
    End Set
  End Property
  Public Property [SupplierOrders]() As clsSupplierOrderCol
    Get
      Return Me._SupplierOrders
    End Get
    Set(ByVal value As clsSupplierOrderCol)
      Me._SupplierOrders = value
    End Set
  End Property
  
  Private Sub CreateDefaultDesignation() 
    Dim pOverridenValue As String = Nothing 
    RaiseEvent evtOverrideDefaultDesignation(pOverridenValue) 
    If pOverridenValue = Nothing Then bDefaultDesignation = _OrderNumber.ToString() Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _OrderNumber <> 0 Then pValue.Append("OrderNumber='" & _OrderNumber.ToString() & "' ‡ ") 
    If _CustomerID <> 0 Then pValue.Append("CustomerID='" & _CustomerID.ToString() & "' ‡ ") 
    If _CustomerText <> "" Then pValue.Append("CustomerText='" & _CustomerText & "' ‡ ") 
    If Not (_OrderDate = Nothing) Then pValue.Append("OrderDate='" & _OrderDate.ToString("o") & "' ‡ ") 
    If _TotalAmount <> 0 Then pValue.Append("TotalAmount='" & _TotalAmount.ToString() & "' ‡ ") 
    If _VATAmount <> 0 Then pValue.Append("VATAmount='" & _VATAmount.ToString() & "' ‡ ") 
    If _TotalWithVAT <> 0 Then pValue.Append("TotalWithVAT='" & _TotalWithVAT.ToString() & "' ‡ ") 
    If _PaymentMethod <> clsEnums.enmPaymentMethod.UD Then pValue.Append("PaymentMethod='" & _PaymentMethod.FastToString() & "' ‡ ") 
    If _PaymentMethodText <> "" Then pValue.Append("PaymentMethodText='" & _PaymentMethodText & "' ‡ ") 
    If _PaymentStatus <> clsEnums.enmPaymentStatus.UD Then pValue.Append("PaymentStatus='" & _PaymentStatus.FastToString() & "' ‡ ") 
    If _PaymentStatusText <> "" Then pValue.Append("PaymentStatusText='" & _PaymentStatusText & "' ‡ ") 
    If Not (_PaymentDate = Nothing) Then pValue.Append("PaymentDate='" & _PaymentDate.ToString("o") & "' ‡ ") 
    If _InvoiceNumber <> "" Then pValue.Append("InvoiceNumber='" & _InvoiceNumber & "' ‡ ") 
    If _DeliveryMethod <> clsEnums.enmDeliveryMethod.UD Then pValue.Append("DeliveryMethod='" & _DeliveryMethod.FastToString() & "' ‡ ") 
    If _DeliveryMethodText <> "" Then pValue.Append("DeliveryMethodText='" & _DeliveryMethodText & "' ‡ ") 
    If Not (_DeliveryDate = Nothing) Then pValue.Append("DeliveryDate='" & _DeliveryDate.ToString("o") & "' ‡ ") 
    If _DeliveryDay <> clsEnums.enmDeliveryDay.UD Then pValue.Append("DeliveryDay='" & _DeliveryDay.FastToString() & "' ‡ ") 
    If _DeliveryDayText <> "" Then pValue.Append("DeliveryDayText='" & _DeliveryDayText & "' ‡ ") 
    If _OrderStatus <> clsEnums.enmOrderStatus.UD Then pValue.Append("OrderStatus='" & _OrderStatus.FastToString() & "' ‡ ") 
    If _OrderStatusText <> "" Then pValue.Append("OrderStatusText='" & _OrderStatusText & "' ‡ ") 
    If _Notes <> "" Then pValue.Append("Notes='" & _Notes & "' ‡ ") 
    If _Notes2 <> "" Then pValue.Append("Notes2='" & _Notes2 & "' ‡ ") 
    If _OrderMonth <> "" Then pValue.Append("OrderMonth='" & _OrderMonth & "' ‡ ") 
    If _Quarter <> "" Then pValue.Append("Quarter='" & _Quarter & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _OrderNumber.ToString() & "") 
    pCSV.Append("," & _CustomerID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_CustomerText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OrderDate.ToShortDateString & " " & _OrderDate.ToShortTimeString)}""") 
    pCSV.Append("," & _TotalAmount.ToString() & "") 
    pCSV.Append("," & _VATAmount.ToString() & "") 
    pCSV.Append("," & _TotalWithVAT.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_PaymentMethod.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_PaymentMethodText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_PaymentStatus.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_PaymentStatusText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_PaymentDate.ToShortDateString & " " & _PaymentDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_InvoiceNumber)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryMethod.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryMethodText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryDate.ToShortDateString & " " & _DeliveryDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryDay.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryDayText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OrderStatus.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_OrderStatusText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Notes)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Notes2)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OrderMonth)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Quarter)}""") 
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
  
  Public Sub New(ByVal vclsOrderHeader As clsOrderHeader)
    MyBase.New()
    CreateEmpty()
    AssignValues(vclsOrderHeader) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vOrderNumber As Integer = 0 _ 
    , Optional vCustomerID As Long = 0 _ 
    , Optional vCustomerText As String = "" _ 
    , Optional vOrderDate As Date = Nothing _ 
    , Optional vTotalAmount As Decimal = 0D _ 
    , Optional vVATAmount As Decimal = 0D _ 
    , Optional vTotalWithVAT As Decimal = 0D _ 
    , Optional vPaymentMethod As clsEnums.enmPaymentMethod = clsEnums.enmPaymentMethod.UD _ 
    , Optional vPaymentMethodText As String = "" _ 
    , Optional vPaymentStatus As clsEnums.enmPaymentStatus = clsEnums.enmPaymentStatus.Pending _ 
    , Optional vPaymentStatusText As String = "" _ 
    , Optional vPaymentDate As Date = Nothing _ 
    , Optional vInvoiceNumber As String = "" _ 
    , Optional vDeliveryMethod As clsEnums.enmDeliveryMethod = clsEnums.enmDeliveryMethod.UD _ 
    , Optional vDeliveryMethodText As String = "" _ 
    , Optional vDeliveryDate As Date = Nothing _ 
    , Optional vDeliveryDay As clsEnums.enmDeliveryDay = clsEnums.enmDeliveryDay.UD _ 
    , Optional vDeliveryDayText As String = "" _ 
    , Optional vOrderStatus As clsEnums.enmOrderStatus = clsEnums.enmOrderStatus.New _ 
    , Optional vOrderStatusText As String = "" _ 
    , Optional vNotes As String = "" _ 
    , Optional vNotes2 As String = "" _ 
    , Optional vOrderMonth As String = "" _ 
    , Optional vQuarter As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _OrderNumber = vOrderNumber 
    _CustomerID = vCustomerID 
    _CustomerText = vCustomerText 
    _OrderDate = vOrderDate 
    _TotalAmount = vTotalAmount 
    _VATAmount = vVATAmount 
    _TotalWithVAT = vTotalWithVAT 
    _PaymentMethod = vPaymentMethod 
    _PaymentMethodText = vPaymentMethodText 
    _PaymentStatus = vPaymentStatus 
    _PaymentStatusText = vPaymentStatusText 
    _PaymentDate = vPaymentDate 
    _InvoiceNumber = vInvoiceNumber 
    _DeliveryMethod = vDeliveryMethod 
    _DeliveryMethodText = vDeliveryMethodText 
    _DeliveryDate = vDeliveryDate 
    _DeliveryDay = vDeliveryDay 
    _DeliveryDayText = vDeliveryDayText 
    _OrderStatus = vOrderStatus 
    _OrderStatusText = vOrderStatusText 
    _Notes = vNotes 
    _Notes2 = vNotes2 
    _OrderMonth = vOrderMonth 
    _Quarter = vQuarter 
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
 
    _InvoiceNumber = _InvoiceNumber.Truncate(pTruncateLength, _IsTruncated) 
    _Notes = _Notes.Truncate(pTruncateLength, _IsTruncated) 
    _Notes2 = _Notes2.Truncate(pTruncateLength, _IsTruncated) 
    _OrderMonth = _OrderMonth.Truncate(pTruncateLength, _IsTruncated) 
    _Quarter = _Quarter.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _InvoiceNumber = ccHelper.RemoveChrW0(_InvoiceNumber) 
    _Notes = ccHelper.RemoveChrW0(_Notes) 
    _Notes2 = ccHelper.RemoveChrW0(_Notes2) 
    _OrderMonth = ccHelper.RemoveChrW0(_OrderMonth) 
    _Quarter = ccHelper.RemoveChrW0(_Quarter) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the OrderHeader by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeader_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderHeader-151224_0844", vRequester) 
    End Try 
 
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsOrderHeader_GetByPrimaryKey", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [OrderNumber] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the OrderHeader by the chosen parameters. This function may be a bit slower than accessing the OrderHeader's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeader_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.OrderNumber 
          pFault = GetByOrderNumber(ccHelper.ToInteger(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-OrderHeader-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderHeader-151223_1716", vRequester)  
    End Try  
 
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsOrderHeader_GetByParameters", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the OrderHeader by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeader_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"OrderHeader not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-OrderHeader-210927-1527", vRequester, vAdditionalMessageToUser:=$"OrderHeader not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.OrderHeaderCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeaderGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"OrderHeader not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-OrderHeader-210625-0950", vRequester, vAdditionalMessageToUser:=$"OrderHeader not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsOrderHeader_GetByID", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the OrderHeader by OrderNumber.
  ''' </summary>
  ''' <param name="vOrderNumber"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByOrderNumber(ByVal vOrderNumber As Integer, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderNumber={0}", vOrderNumber)
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeader_GetByOrderNumber", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.OrderHeaderCol.FindByOrderNumber(vOrderNumber), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeaderGetByOrderNumber" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OrderNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (vOrderNumber) 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"OrderHeader not found for GetByOrderNumber. See FunctionParameters for values", pFunctionParameters, "TRGT-OrderHeader-210625-0950", vRequester, vAdditionalMessageToUser:=$"OrderHeader not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsOrderHeader_GetByOrderNumber", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderUpdate, "clsOrderHeader_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-OrderHeader-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderUpdate, "clsOrderHeader_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-OrderHeader-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the OrderHeader. If there are parents or children in the OrderHeader, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderUpdate, "clsOrderHeader_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pOrderHeader As New clsOrderHeader(_WithParents) 
    If Me.isEqual(pOrderHeader) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-OrderHeader-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-OrderHeader-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "ccOrderHeaderUpdate"
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
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pCachedOrderHeader As clsOrderHeader 
      If _ID = 0 Then 
        pCachedOrderHeader = New clsOrderHeader(_WithParents) 
        'get last ID 
        Dim pOrderHeaderCol As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.Clone() 
        If pOrderHeaderCol.Count = 0 Then 
          _ID = 1 
        Else 
          pOrderHeaderCol.SortByID() 
          Dim pLastID As Long = pOrderHeaderCol(pOrderHeaderCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.OrderHeaderCol.Add(pCachedOrderHeader) 
      Else  
        pCachedOrderHeader = MyController.DBCache.OrderHeaderCol.FindByID(_ID) 
      End If 
      pCachedOrderHeader.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.OrderHeaderCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "OrderNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_OrderNumber) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_CustomerID, False) 
        pLastReadVariableName = "OrderDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_OrderDate) 
        pLastReadVariableName = "enmPaymentMethod" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_PaymentMethod.FastToString()) 
        pLastReadVariableName = "enmPaymentStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_PaymentStatus.FastToString()) 
        pLastReadVariableName = "PaymentDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_PaymentDate) 
        pLastReadVariableName = "InvoiceNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_InvoiceNumber) 
        pLastReadVariableName = "enmDeliveryMethod" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_DeliveryMethod.FastToString()) 
        pLastReadVariableName = "DeliveryDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_DeliveryDate) 
        pLastReadVariableName = "enmDeliveryDay" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 10).Value = (_DeliveryDay.FastToString()) 
        pLastReadVariableName = "enmOrderStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_OrderStatus.FastToString()) 
        pLastReadVariableName = "Notes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes) 
        pLastReadVariableName = "Notes2" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes2) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Parents 
      Dim pCustomer As clsCustomer = _Customer 
      
      'Children 
      Dim pCustomerDebts As clsCustomerDebtCol = _CustomerDebts 
      Dim pDeliverys As clsDeliveryCol = _Deliverys 
      Dim pOrderLines As clsOrderLineCol = _OrderLines 
      Dim pSupplierOrders As clsSupplierOrderCol = _SupplierOrders 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Parents 
      If _WithParents <> clsEnums.enmLoadParent.EntireObject Then 
        If Not pCustomer Is Nothing Then _Customer = pCustomer 
      End If 
      
      'Children 
      If Not pCustomerDebts Is Nothing Then _CustomerDebts = pCustomerDebts 
      If Not pDeliverys Is Nothing Then _Deliverys = pDeliverys 
      If Not pOrderLines Is Nothing Then _OrderLines = pOrderLines 
      If Not pSupplierOrders Is Nothing Then _SupplierOrders = pSupplierOrders 
      
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
    Dim pFunctionParameters As String = String.Format("OrderHeader.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeader_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "ccOrderHeaderDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      MyController.DBCache.OrderHeaderCol.Remove(MyController.DBCache.OrderHeaderCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.OrderHeaderCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeader_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeaderDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      MyController.DBCache.OrderHeaderCol.Remove(MyController.DBCache.OrderHeaderCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.OrderHeaderCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Fills the OrderHeader's CustomerDebt collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillCustomerDebts(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeader_FillCustomerDebts", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _CustomerDebts = New clsCustomerDebtCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _CustomerDebts.FillByOrderHeaderID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the OrderHeader's Delivery collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillDeliverys(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeader_FillDeliverys", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _Deliverys = New clsDeliveryCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _Deliverys.FillByOrderHeaderID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the OrderHeader's OrderLine collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillOrderLines(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeader_FillOrderLines", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _OrderLines = New clsOrderLineCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _OrderLines.FillByOrderHeaderID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the OrderHeader's SupplierOrder collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillSupplierOrders(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeader_FillSupplierOrders", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _SupplierOrders = New clsSupplierOrderCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _SupplierOrders.FillByOrderHeaderID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
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
    If Not (TypeOf (vTargCCEntityToTest) Is clsOrderHeader) Then Return False 
    Dim pOrderHeaderToTest As clsOrderHeader = CType(vTargCCEntityToTest, clsOrderHeader) 
    Return isEqual(pOrderHeaderToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vOrderHeaderToTest As clsOrderHeader) As Boolean
    With vOrderHeaderToTest
      If _ID <> .ID Then Return False
      If _OrderNumber <> .OrderNumber Then Return False
      If _CustomerID <> .CustomerID Then Return False
      If _OrderDate <> Nothing AndAlso .OrderDate <> Nothing Then 
        If ccHelper.ToLong(_OrderDate.Subtract(.OrderDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_OrderDate = Nothing AndAlso .OrderDate = Nothing) Then 
        Return False 
      End If 
      If _TotalAmount <> .TotalAmount Then Return False
      If _VATAmount <> .VATAmount Then Return False
      If _TotalWithVAT <> .TotalWithVAT Then Return False
      If _PaymentMethod <> .PaymentMethod Then Return False
      If _PaymentStatus <> .PaymentStatus Then Return False
      If _PaymentDate <> Nothing AndAlso .PaymentDate <> Nothing Then 
        If ccHelper.ToLong(_PaymentDate.Subtract(.PaymentDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_PaymentDate = Nothing AndAlso .PaymentDate = Nothing) Then 
        Return False 
      End If 
      If _InvoiceNumber <> .InvoiceNumber Then Return False
      If _DeliveryMethod <> .DeliveryMethod Then Return False
      If _DeliveryDate <> Nothing AndAlso .DeliveryDate <> Nothing Then 
        If ccHelper.ToLong(_DeliveryDate.Subtract(.DeliveryDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_DeliveryDate = Nothing AndAlso .DeliveryDate = Nothing) Then 
        Return False 
      End If 
      If _DeliveryDay <> .DeliveryDay Then Return False
      If _OrderStatus <> .OrderStatus Then Return False
      If _Notes <> .Notes Then Return False
      If _Notes2 <> .Notes2 Then Return False
      If _OrderMonth <> .OrderMonth Then Return False
      If _Quarter <> .Quarter Then Return False
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
    Dim pClone As New clsOrderHeader(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsOrderHeader
    Dim pClone As New clsOrderHeader(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderNumber") = _OrderNumber : Catch ex As Exception : Return pFault.LogException(ex, "OrderNumber", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("CustomerID") = _CustomerID : Catch ex As Exception : Return pFault.LogException(ex, "CustomerID", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderDate") = _OrderDate : Catch ex As Exception : Return pFault.LogException(ex, "OrderDate", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("TotalAmount") = _TotalAmount : Catch ex As Exception : Return pFault.LogException(ex, "TotalAmount", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("VATAmount") = _VATAmount : Catch ex As Exception : Return pFault.LogException(ex, "VATAmount", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("TotalWithVAT") = _TotalWithVAT : Catch ex As Exception : Return pFault.LogException(ex, "TotalWithVAT", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("PaymentMethod") = _PaymentMethod : Catch ex As Exception : Return pFault.LogException(ex, "PaymentMethod", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("PaymentStatus") = _PaymentStatus : Catch ex As Exception : Return pFault.LogException(ex, "PaymentStatus", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("PaymentDate") = _PaymentDate : Catch ex As Exception : Return pFault.LogException(ex, "PaymentDate", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("InvoiceNumber") = _InvoiceNumber : Catch ex As Exception : Return pFault.LogException(ex, "InvoiceNumber", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("DeliveryMethod") = _DeliveryMethod : Catch ex As Exception : Return pFault.LogException(ex, "DeliveryMethod", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("DeliveryDate") = _DeliveryDate : Catch ex As Exception : Return pFault.LogException(ex, "DeliveryDate", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("DeliveryDay") = _DeliveryDay : Catch ex As Exception : Return pFault.LogException(ex, "DeliveryDay", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderStatus") = _OrderStatus : Catch ex As Exception : Return pFault.LogException(ex, "OrderStatus", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("Notes") = _Notes : Catch ex As Exception : Return pFault.LogException(ex, "Notes", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("Notes2") = _Notes2 : Catch ex As Exception : Return pFault.LogException(ex, "Notes2", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderMonth") = _OrderMonth : Catch ex As Exception : Return pFault.LogException(ex, "OrderMonth", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
    Try : vDataRow("Quarter") = _Quarter : Catch ex As Exception : Return pFault.LogException(ex, "Quarter", "TRGT-OrderHeader-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pOrderHeader As clsOrderHeader = CType(pXmlSerializer.Deserialize(pStreamReader), clsOrderHeader) 
      AssignValues(pOrderHeader) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-OrderHeader-130515-1230", vRequester) 
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
          'OrderNumber 
          pBinaryWriter.Write(_OrderNumber) 
          'CustomerID 
          pBinaryWriter.Write(_CustomerID) 
          'Customer 
          If _Customer IsNot Nothing Then 
            pObjectBytes = _Customer.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _CustomerText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CustomerText) 
          'OrderDate 
          pBinaryWriter.Write(_OrderDate.Ticks) 
          'TotalAmount 
          pBinaryWriter.Write(_TotalAmount) 
          'VATAmount 
          pBinaryWriter.Write(_VATAmount) 
          'TotalWithVAT 
          pBinaryWriter.Write(_TotalWithVAT) 
          'PaymentMethod 
          pBinaryWriter.Write(_PaymentMethod.FastToString()) 
          'PaymentStatus 
          pBinaryWriter.Write(_PaymentStatus.FastToString()) 
          'PaymentDate 
          pBinaryWriter.Write(_PaymentDate.Ticks) 
          'InvoiceNumber 
          If _InvoiceNumber Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_InvoiceNumber) 
          'DeliveryMethod 
          pBinaryWriter.Write(_DeliveryMethod.FastToString()) 
          'DeliveryDate 
          pBinaryWriter.Write(_DeliveryDate.Ticks) 
          'DeliveryDay 
          pBinaryWriter.Write(_DeliveryDay.FastToString()) 
          'OrderStatus 
          pBinaryWriter.Write(_OrderStatus.FastToString()) 
          'Notes 
          If _Notes Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Notes) 
          'Notes2 
          If _Notes2 Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Notes2) 
          'OrderMonth 
          If _OrderMonth Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_OrderMonth) 
          'Quarter 
          If _Quarter Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Quarter) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          'CustomerDebts  
          If _CustomerDebts IsNot Nothing Then 
            pObjectBytes = _CustomerDebts.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'Deliverys  
          If _Deliverys IsNot Nothing Then 
            pObjectBytes = _Deliverys.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'OrderLines  
          If _OrderLines IsNot Nothing Then 
            pObjectBytes = _OrderLines.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'SupplierOrders  
          If _SupplierOrders IsNot Nothing Then 
            pObjectBytes = _SupplierOrders.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-150307-2338", vRequester) 
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
          'OrderNumber 
          _OrderNumber = pReader.ReadInt32 
          'CustomerID 
          _CustomerID = pReader.ReadInt64 
          'Customer 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _Customer = New clsCustomer(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CustomerText = pReader.ReadString 
          'OrderDate 
          _OrderDate = New Date(pReader.ReadInt64) 
          'TotalAmount 
          _TotalAmount = pReader.ReadDecimal 
          'VATAmount 
          _VATAmount = pReader.ReadDecimal 
          'TotalWithVAT 
          _TotalWithVAT = pReader.ReadDecimal 
          'PaymentMethod 
          _PaymentMethod = clsEnums.TranslateEnmPaymentMethod(pReader.ReadString) 
          'PaymentStatus 
          _PaymentStatus = clsEnums.TranslateEnmPaymentStatus(pReader.ReadString) 
          'PaymentDate 
          _PaymentDate = New Date(pReader.ReadInt64) 
          'InvoiceNumber 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _InvoiceNumber = pReader.ReadString 
          'DeliveryMethod 
          _DeliveryMethod = clsEnums.TranslateEnmDeliveryMethod(pReader.ReadString) 
          'DeliveryDate 
          _DeliveryDate = New Date(pReader.ReadInt64) 
          'DeliveryDay 
          _DeliveryDay = clsEnums.TranslateEnmDeliveryDay(pReader.ReadString) 
          'OrderStatus 
          _OrderStatus = clsEnums.TranslateEnmOrderStatus(pReader.ReadString) 
          'Notes 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Notes = pReader.ReadString 
          'Notes2 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Notes2 = pReader.ReadString 
          'OrderMonth 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _OrderMonth = pReader.ReadString 
          'Quarter 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Quarter = pReader.ReadString 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          'CustomerDebts 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _CustomerDebts = New clsCustomerDebtCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'Deliverys 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _Deliverys = New clsDeliveryCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'OrderLines 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _OrderLines = New clsOrderLineCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'SupplierOrders 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _SupplierOrders = New clsSupplierOrderCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-OrderHeader-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-190720-1443", vRequester) 
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
 
      Dim pOrderHeader As clsOrderHeader = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsOrderHeader)(vJSON, pSettings) 
      AssignValues(pOrderHeader) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vOrderHeader As clsOrderHeader)
    With vOrderHeader
      _ID = .ID 
      _OrderNumber = .OrderNumber 
      _CustomerID = .CustomerID 
      If .Customer IsNot Nothing Then 
        _Customer = .Customer.Clone() 
      End If 
      _CustomerText = .CustomerText 
      _OrderDate = .OrderDate 
      _TotalAmount = .TotalAmount 
      _VATAmount = .VATAmount 
      _TotalWithVAT = .TotalWithVAT 
      _PaymentMethod = .PaymentMethod 
      _PaymentMethodText = .PaymentMethodText
      _PaymentStatus = .PaymentStatus 
      _PaymentStatusText = .PaymentStatusText
      _PaymentDate = .PaymentDate 
      _InvoiceNumber = .InvoiceNumber 
      _DeliveryMethod = .DeliveryMethod 
      _DeliveryMethodText = .DeliveryMethodText
      _DeliveryDate = .DeliveryDate 
      _DeliveryDay = .DeliveryDay 
      _DeliveryDayText = .DeliveryDayText
      _OrderStatus = .OrderStatus 
      _OrderStatusText = .OrderStatusText
      _Notes = .Notes 
      _Notes2 = .Notes2 
      _OrderMonth = .OrderMonth 
      _Quarter = .Quarter 
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
      'PaymentMethod 
      pTextToGet = "PaymentMethodText (Enum)" 
      _PaymentMethodText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.PaymentMethod, _PaymentMethod.FastToString(), vRequester) 
      'PaymentStatus 
      pTextToGet = "PaymentStatusText (Enum)" 
      _PaymentStatusText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.PaymentStatus, _PaymentStatus.FastToString(), vRequester) 
      'DeliveryMethod 
      pTextToGet = "DeliveryMethodText (Enum)" 
      _DeliveryMethodText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.DeliveryMethod, _DeliveryMethod.FastToString(), vRequester) 
      'DeliveryDay 
      pTextToGet = "DeliveryDayText (Enum)" 
      _DeliveryDayText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.DeliveryDay, _DeliveryDay.FastToString(), vRequester) 
      'OrderStatus 
      pTextToGet = "OrderStatusText (Enum)" 
      _OrderStatusText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.OrderStatus, _OrderStatus.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-OrderHeader-151124-1900", vRequester) 
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
    If _CustomerID > 0 Then
      _Customer = New clsCustomer()
      pFault = _Customer.GetByID(_CustomerID, vRequester, True)
      If pFault.isOK = False Then Return pFault
      _CustomerText = _Customer.DefaultDesignation 
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
      pLastReadVariableName = "OrderNumber" 
      If Not vReader.IsDBNull(1) Then _OrderNumber = vReader.GetInt32(1)
      pLastReadVariableName = "CustomerID" 
      If Not vReader.IsDBNull(2) Then _CustomerID = vReader.GetInt64(2)
      pLastReadVariableName = "OrderDate" 
      If Not vReader.IsDBNull(3) Then _OrderDate = vReader.GetDateTime(3)
      pLastReadVariableName = "clc_TotalAmount" 
      If Not vReader.IsDBNull(4) Then _TotalAmount = vReader.GetDecimal(4)
      pLastReadVariableName = "clc_VATAmount" 
      If Not vReader.IsDBNull(5) Then _VATAmount = vReader.GetDecimal(5)
      pLastReadVariableName = "clc_TotalWithVAT" 
      If Not vReader.IsDBNull(6) Then _TotalWithVAT = vReader.GetDecimal(6)
      pLastReadVariableName = "enmPaymentMethod" 
      If Not vReader.IsDBNull(7) Then _PaymentMethod = clsEnums.TranslateEnmPaymentMethod(vReader.GetString(7))
      pLastReadVariableName = "enmPaymentStatus" 
      If Not vReader.IsDBNull(8) Then _PaymentStatus = clsEnums.TranslateEnmPaymentStatus(vReader.GetString(8))
      pLastReadVariableName = "PaymentDate" 
      If Not vReader.IsDBNull(9) Then _PaymentDate = vReader.GetDateTime(9)
      pLastReadVariableName = "InvoiceNumber" 
      If Not vReader.IsDBNull(10) Then _InvoiceNumber = vReader.GetString(10) 
      pLastReadVariableName = "enmDeliveryMethod" 
      If Not vReader.IsDBNull(11) Then _DeliveryMethod = clsEnums.TranslateEnmDeliveryMethod(vReader.GetString(11))
      pLastReadVariableName = "DeliveryDate" 
      If Not vReader.IsDBNull(12) Then _DeliveryDate = vReader.GetDateTime(12)
      pLastReadVariableName = "enmDeliveryDay" 
      If Not vReader.IsDBNull(13) Then _DeliveryDay = clsEnums.TranslateEnmDeliveryDay(vReader.GetString(13))
      pLastReadVariableName = "enmOrderStatus" 
      If Not vReader.IsDBNull(14) Then _OrderStatus = clsEnums.TranslateEnmOrderStatus(vReader.GetString(14))
      pLastReadVariableName = "Notes" 
      If Not vReader.IsDBNull(15) Then _Notes = vReader.GetString(15) 
      pLastReadVariableName = "Notes2" 
      If Not vReader.IsDBNull(16) Then _Notes2 = vReader.GetString(16) 
      pLastReadVariableName = "clc_OrderMonth" 
      If Not vReader.IsDBNull(17) Then _OrderMonth = vReader.GetString(17) 
      pLastReadVariableName = "clc_Quarter" 
      If Not vReader.IsDBNull(18) Then _Quarter = vReader.GetString(18) 
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(19) Then bDateAdded = vReader.GetDateTime(19)   
      If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
        pLastReadVariableName = "CustomerText" 
        If Not vReader.IsDBNull(20) Then _CustomerText = vReader.GetString(20) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedOrderHeader As clsOrderHeader, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pWithParents As clsEnums.enmLoadParent = _WithParents 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedOrderHeader) 
      If pWithParents = clsEnums.enmLoadParent.DoNotLoad Then 
        _CustomerText = "."
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _OrderNumber = 0
    _CustomerID = 0
    _Customer = Nothing
    _CustomerText = "."
    'Default Value set by SQL Server Database (below): etdate(
    _OrderDate = Nothing
    'Default Value set by SQL Server Database (below): 0D
    _TotalAmount = 0D
    'Default Value set by SQL Server Database (below): 0D
    _VATAmount = 0D
    'Default Value set by SQL Server Database (below): 0D
    _TotalWithVAT = 0D
    _PaymentMethod = clsEnums.enmPaymentMethod.UD
    _PaymentMethodText = ""
    'Default Value set by SQL Server Database (below): Pending
    _PaymentStatus = clsEnums.enmPaymentStatus.Pending
    _PaymentStatusText = ""
    _PaymentDate = Nothing
    _InvoiceNumber = ""
    _DeliveryMethod = clsEnums.enmDeliveryMethod.UD
    _DeliveryMethodText = ""
    _DeliveryDate = Nothing
    _DeliveryDay = clsEnums.enmDeliveryDay.UD
    _DeliveryDayText = ""
    'Default Value set by SQL Server Database (below): New
    _OrderStatus = clsEnums.enmOrderStatus.New
    _OrderStatusText = ""
    _Notes = ""
    _Notes2 = ""
    _OrderMonth = ""
    _Quarter = ""
    _Tag = ""
    _CustomerDebts = Nothing
    _Deliverys = Nothing
    _OrderLines = Nothing
    _SupplierOrders = Nothing
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
  
Public Class clsOrderHeaderCol
  Inherits cTargCCCollection(Of clsOrderHeader)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsOrderHeader) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByOrderNumber As Dictionary(Of String, clsOrderHeader) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByOrderNumber As Boolean 
  Private Function CreateKeyForFindByOrderNumber(ByVal vOrderHeader As clsOrderHeader) As String 
    With vOrderHeader 
      Return .OrderNumber.ToString()
    End With 
  End Function 
   
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
 
    For Each pRow As clsOrderHeader In Me 
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
    pCSVTitle.Append(",""OrderNumber""") 
    pCSVTitle.Append(",""CustomerID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Customer (Text)""") 
    pCSVTitle.Append(",""OrderDate""") 
    pCSVTitle.Append(",""TotalAmount""") 
    pCSVTitle.Append(",""VATAmount""") 
    pCSVTitle.Append(",""TotalWithVAT""") 
    pCSVTitle.Append(",""PaymentMethod" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""PaymentMethod (Text)""") 
    pCSVTitle.Append(",""PaymentStatus" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""PaymentStatus (Text)""") 
    pCSVTitle.Append(",""PaymentDate""") 
    pCSVTitle.Append(",""InvoiceNumber""") 
    pCSVTitle.Append(",""DeliveryMethod" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""DeliveryMethod (Text)""") 
    pCSVTitle.Append(",""DeliveryDate""") 
    pCSVTitle.Append(",""DeliveryDay" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""DeliveryDay (Text)""") 
    pCSVTitle.Append(",""OrderStatus" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""OrderStatus (Text)""") 
    pCSVTitle.Append(",""Notes""") 
    pCSVTitle.Append(",""Notes2""") 
    pCSVTitle.Append(",""OrderMonth""") 
    pCSVTitle.Append(",""Quarter""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As clsOrderHeader In Me 
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
 
  Public Overloads Sub Add(ByVal vOrderHeader As clsOrderHeader) 
    SyncLock _CollectionLock 
      MyBase.Add(vOrderHeader) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByOrderNumber = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vOrderHeader As clsOrderHeader) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vOrderHeader) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByOrderNumber = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vOrderHeaderCol As clsOrderHeaderCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vOrderHeaderCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByOrderNumber = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByOrderNumber = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vOrderHeader As clsOrderHeader) 
    SyncLock _CollectionLock 
      MyBase.Remove(vOrderHeader) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByOrderNumber = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, clsOrderHeader) 
      
      For Each lOrderHeader In Me 
        If lOrderHeader.IsEmpty OrElse pTempDictionary.ContainsKey(lOrderHeader.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lOrderHeader.ID, lOrderHeader) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lOrderHeader.ToString, "TRGT-OrderHeader-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", OrderHeader:" & lOrderHeader.ToString() & ", TRGT-OrderHeader-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadOrderNumbers() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByOrderNumber Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByOrderNumber Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByOrderNumber = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByOrderNumber' yet!
      Dim pTempDictionary As New Dictionary(Of String, clsOrderHeader)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lOrderHeader In Me 
        Try 
          Dim pOrderNumber As String = CreateKeyForFindByOrderNumber(lOrderHeader) 
          If String.IsNullOrEmpty(pOrderNumber.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pOrderNumber)) Then 
            pTempDictionary.Add(pOrderNumber, lOrderHeader) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lOrderHeader.ToString, "TRGT-OrderHeader-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByOrderNumber:" & ex.Message & ", OrderHeader:" & lOrderHeader.ToString() & ", TRGT-OrderHeader-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByOrderNumber = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByOrderNumber = False
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
 
    For Each lOrderHeader As clsOrderHeader In Me 
      lOrderHeader.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lOrderHeader As clsOrderHeader In Me 
      lOrderHeader.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [CustomerID] 
    [OrderStatus] 
    [PaymentStatus] 
    [OrderDate] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the OrderHeaders by the chosen parameters. This function may be a bit slower than accessing the OrderHeader's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.CustomerID 
          pFault = FillByCustomerID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OrderStatus 
          pFault = FillByOrderStatus(clsEnums.TranslateEnmOrderStatus(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.PaymentStatus 
          pFault = FillByPaymentStatus(clsEnums.TranslateEnmPaymentStatus(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OrderDate 
          pFault = FillByOrderDate(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-OrderHeader-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderHeader-151223_1716", vRequester) 
    End Try 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillByParameters", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.Clone() 
      pOrderHeadersCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pOrderHeadersCached.Reverse() 
      If vHowMany > 0 AndAlso pOrderHeadersCached.Count > vHowMany Then 
        Dim tmp As New clsOrderHeaderCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pOrderHeadersCached(i)) 
        Next 
        pOrderHeadersCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pOrderHeadersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific CustomerID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByCustomerID(ByVal vCustomerID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerID={0}", vCustomerID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByCustomerID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.CloneByCustomerID(vCustomerID)
      pFault = LoadMeFromDBCache(pOrderHeadersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillByCustomerID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vCustomerID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillByCustomerID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OrderStatus, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOrderStatus(ByVal vOrderStatus As clsEnums.enmOrderStatus, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderStatus={0}", vOrderStatus)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByOrderStatus", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.CloneByOrderStatus(vOrderStatus)
      pFault = LoadMeFromDBCache(pOrderHeadersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillByOrderStatus" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmOrderStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vOrderStatus.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillByOrderStatus", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific PaymentStatus, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByPaymentStatus(ByVal vPaymentStatus As clsEnums.enmPaymentStatus, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("PaymentStatus={0}", vPaymentStatus)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByPaymentStatus", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.CloneByPaymentStatus(vPaymentStatus)
      pFault = LoadMeFromDBCache(pOrderHeadersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillByPaymentStatus" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmPaymentStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vPaymentStatus.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillByPaymentStatus", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OrderDate, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOrderDate(ByVal vOrderDate As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderDate={0}", vOrderDate)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByOrderDate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.CloneByOrderDate(vOrderDate)
      pFault = LoadMeFromDBCache(pOrderHeadersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillByOrderDate" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OrderDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(vOrderDate) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillByOrderDate", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pOrderHeadersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillByBoundedID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OrderDate, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedOrderDate(ByVal vOrderDateStart As Date, ByVal vOrderDateEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderDateStart={0}, OrderDateEnd={1}", vOrderDateStart, vOrderDateEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByBoundedOrderDate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.CloneByBoundedOrderDate(vOrderDateStart, vOrderDateEnd)
      pFault = LoadMeFromDBCache(pOrderHeadersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillByBoundedOrderDate" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OrderDateFrom" 
        pDALParameters.Add("bndOrderDateFrom", ccDAL.enmSQLDataType.DateTime).Value = (vOrderDateStart) 
        pLastReadVariableName = "OrderDateTo" 
        pDALParameters.Add("bndOrderDateTo", ccDAL.enmSQLDataType.DateTime).Value = (vOrderDateEnd) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillByBoundedOrderDate", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OrderNumber, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedOrderNumber(ByVal vOrderNumberFrom As Integer, ByVal vOrderNumberTo As Integer, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderNumberFrom={0}, OrderNumberTo={1}", vOrderNumberFrom, vOrderNumberTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByBoundedOrderNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderHeaderCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderHeaderCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderHeaderCol failed: " & pResponse) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.CloneByBoundedOrderNumber(vOrderNumberFrom, vOrderNumberTo)
      pFault = LoadMeFromDBCache(pOrderHeadersCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillByBoundedOrderNumber" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OrderNumberFrom" 
        pDALParameters.Add("bndOrderNumberFrom", ccDAL.enmSQLDataType.Int).Value = (vOrderNumberFrom) 
        pLastReadVariableName = "OrderNumberTo" 
        pDALParameters.Add("bndOrderNumberTo", ccDAL.enmSQLDataType.Int).Value = (vOrderNumberTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillByBoundedOrderNumber", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lOrderHeader As New clsOrderHeader() 
      pFault = lOrderHeader.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lOrderHeader.IsEmpty Then Me.Add(lOrderHeader) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
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
    OrderNumberFrom
    OrderNumberTo
    [CustomerID]
    OrderDateStart
    OrderDateEnd
    [PaymentStatus]
    [OrderStatus]
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pOrderNumberFrom As Nullable(Of Integer) = Nothing
    Dim pOrderNumberTo As Nullable(Of Integer) = Nothing
    Dim pCustomerID As Nullable(Of Long) = Nothing
    Dim pOrderDateStart As Nullable(Of Date) = Nothing
    Dim pOrderDateEnd As Nullable(Of Date) = Nothing
    Dim pPaymentStatus As clsEnums.enmPaymentStatus = clsEnums.enmPaymentStatus.UD
    Dim pOrderStatus As clsEnums.enmOrderStatus = clsEnums.enmOrderStatus.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderNumberFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderNumberFrom) : If pObj IsNot Nothing Then pOrderNumberFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderNumberTo) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderNumberTo) : If pObj IsNot Nothing Then pOrderNumberTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerID) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerID) : If pObj IsNot Nothing Then pCustomerID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderDateStart) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderDateStart) : If pObj IsNot Nothing Then pOrderDateStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderDateEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderDateEnd) : If pObj IsNot Nothing Then pOrderDateEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.PaymentStatus) Then pObj = vParameters(enmFillOnTheFlyParameters.PaymentStatus) : If pObj IsNot Nothing Then pPaymentStatus = CType(pObj, clsEnums.enmPaymentStatus) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderStatus) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderStatus) : If pObj IsNot Nothing Then pOrderStatus = CType(pObj, clsEnums.enmOrderStatus) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pOrderNumberFrom, pOrderNumberTo _
        , pCustomerID _
        , pOrderDateStart, pOrderDateEnd _
        , pPaymentStatus _
        , pOrderStatus _
        , vRequester, pHowMany, pDir) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
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
        , ByVal vOrderNumberFrom As Nullable(Of Integer), ByVal vOrderNumberTo As Nullable(Of Integer) _
        , ByVal vCustomerID As Nullable(Of Long) _
        , ByVal vOrderDateStart As Nullable(Of Date), ByVal vOrderDateEnd As Nullable(Of Date) _
        , ByVal vPaymentStatus As clsEnums.enmPaymentStatus _
        , ByVal vOrderStatus As clsEnums.enmOrderStatus _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OrderNumberFrom={2}, OrderNumberTo={3}, CustomerID={4}, OrderDateStart={5}, OrderDateEnd={6}, PaymentStatus={7}, OrderStatus={8}", vIDFrom, vIDTo, vOrderNumberFrom, vOrderNumberTo, vCustomerID, vOrderDateStart, vOrderDateEnd, vPaymentStatus, vOrderStatus)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-OrderHeader-121122-2008", vRequester) 
      Dim pOrderHeadersCached As clsOrderHeaderCol = MyController.DBCache.OrderHeaderCol.Clone() 
      Dim pOrderHeadersToUse As New clsOrderHeaderCol() 
      For Each l In pOrderHeadersCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vOrderNumberFrom.HasValue Then 
          If vOrderNumberTo.HasValue Then 
            If l.OrderNumber < vOrderNumberFrom OrElse l.OrderNumber > vOrderNumberTo.Value Then Continue For 
          Else 
            If l.OrderNumber <> vOrderNumberFrom.Value Then Continue For 
          End If 
        End If 
        If vCustomerID.HasValue Then 
          If l.CustomerID <> vCustomerID.Value Then Continue For 
        End If 
        If vOrderDateStart.HasValue Then 
          If vOrderDateEnd.HasValue Then 
            If l.OrderDate < vOrderDateStart OrElse l.OrderDate > vOrderDateEnd.Value Then Continue For 
          Else 
            If l.OrderDate <> vOrderDateStart.Value Then Continue For 
          End If 
        End If 
        If vPaymentStatus <> clsEnums.enmPaymentStatus.UD Then 
          If l.PaymentStatus <> vPaymentStatus Then Continue For 
        End If 
        If vOrderStatus <> clsEnums.enmOrderStatus.UD Then 
          If l.OrderStatus <> vOrderStatus Then Continue For 
        End If 
        pOrderHeadersToUse.Add(l) 
      Next 
      pOrderHeadersToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pOrderHeadersToUse.Reverse() 
      If vHowMany > 0 AndAlso pOrderHeadersToUse.Count > vHowMany Then 
        Dim tmp As New clsOrderHeaderCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pOrderHeadersToUse(i)) 
        Next 
        pOrderHeadersToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pOrderHeadersToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "OrderNumberFrom" 
        pDALParameters.Add("bndOrderNumberFrom", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vOrderNumberFrom) 
        pLastReadVariableName = "OrderNumberTo" 
        pDALParameters.Add("bndOrderNumberTo", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vOrderNumberTo) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vCustomerID) 
        pLastReadVariableName = "OrderDateFrom" 
        pDALParameters.Add("bndOrderDateFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vOrderDateStart) 
        pLastReadVariableName = "OrderDateTo" 
        pDALParameters.Add("bndOrderDateTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vOrderDateEnd) 
        pLastReadVariableName = "enmPaymentStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(vPaymentStatus.FastToString()) 
        pLastReadVariableName = "enmOrderStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(vOrderStatus.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByCustomerID
    GroupByOrderDate
    GroupByPaymentStatus
    GroupByOrderStatus
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pOrderNumberFrom As Nullable(Of Integer) = Nothing
    Dim pOrderNumberTo As Nullable(Of Integer) = Nothing
    Dim pCustomerID As Nullable(Of Long) = Nothing
    Dim pOrderDateStart As Nullable(Of Date) = Nothing
    Dim pOrderDateEnd As Nullable(Of Date) = Nothing
    Dim pPaymentStatus As clsEnums.enmPaymentStatus = clsEnums.enmPaymentStatus.UD
    Dim pOrderStatus As clsEnums.enmOrderStatus = clsEnums.enmOrderStatus.UD
    Dim pGroupByCustomerID As Boolean = False
    Dim pGroupByOrderDate As Boolean = False
    Dim pGroupByPaymentStatus As Boolean = False
    Dim pGroupByOrderStatus As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderNumberFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderNumberFrom) : If pObj IsNot Nothing Then pOrderNumberFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderNumberTo) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderNumberTo) : If pObj IsNot Nothing Then pOrderNumberTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerID) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerID) : If pObj IsNot Nothing Then pCustomerID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderDateStart) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderDateStart) : If pObj IsNot Nothing Then pOrderDateStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderDateEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderDateEnd) : If pObj IsNot Nothing Then pOrderDateEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.PaymentStatus) Then pObj = vParameters(enmFillOnTheFlyParameters.PaymentStatus) : If pObj IsNot Nothing Then pPaymentStatus = CType(pObj, clsEnums.enmPaymentStatus) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderStatus) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderStatus) : If pObj IsNot Nothing Then pOrderStatus = CType(pObj, clsEnums.enmOrderStatus) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByCustomerID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByCustomerID) : If pObj IsNot Nothing Then pGroupByCustomerID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOrderDate) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOrderDate) : If pObj IsNot Nothing Then pGroupByOrderDate = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByPaymentStatus) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByPaymentStatus) : If pObj IsNot Nothing Then pGroupByPaymentStatus = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOrderStatus) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOrderStatus) : If pObj IsNot Nothing Then pGroupByOrderStatus = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pOrderNumberFrom, pOrderNumberTo _
        , pCustomerID _
        , pOrderDateStart, pOrderDateEnd _
        , pPaymentStatus _
        , pOrderStatus _
        , pGroupByCustomerID _
        , pGroupByOrderDate _
        , pGroupByPaymentStatus _
        , pGroupByOrderStatus _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
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
        , ByVal vOrderNumberFrom As Nullable(Of Integer), ByVal vOrderNumberTo As Nullable(Of Integer) _
        , ByVal vCustomerID As Nullable(Of Long) _
        , ByVal vOrderDateStart As Nullable(Of Date), ByVal vOrderDateEnd As Nullable(Of Date) _
        , ByVal vPaymentStatus As clsEnums.enmPaymentStatus _
        , ByVal vOrderStatus As clsEnums.enmOrderStatus _
        , ByVal vGroupByCustomerID As Boolean _
        , ByVal vGroupByOrderDate As Boolean _
        , ByVal vGroupByPaymentStatus As Boolean _
        , ByVal vGroupByOrderStatus As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OrderNumberFrom={2}, OrderNumberTo={3}, CustomerID={4}, OrderDateStart={5}, OrderDateEnd={6}, PaymentStatus={7}, OrderStatus={8}, GroupByCustomerID={9}, GroupByOrderDate={10}, GroupByPaymentStatus={11}, GroupByOrderStatus={12}", vIDFrom, vIDTo, vOrderNumberFrom, vOrderNumberTo, vCustomerID, vOrderDateStart, vOrderDateEnd, vPaymentStatus, vOrderStatus, vGroupByCustomerID, vGroupByOrderDate, vGroupByPaymentStatus, vGroupByOrderStatus)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-OrderHeader-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderHeadersFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "OrderNumberFrom" 
        pDALParameters.Add("bndOrderNumberFrom", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vOrderNumberFrom) 
        pLastReadVariableName = "OrderNumberTo" 
        pDALParameters.Add("bndOrderNumberTo", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vOrderNumberTo) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vCustomerID) 
        pLastReadVariableName = "OrderDateFrom" 
        pDALParameters.Add("bndOrderDateFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vOrderDateStart) 
        pLastReadVariableName = "OrderDateTo" 
        pDALParameters.Add("bndOrderDateTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vOrderDateEnd) 
        pLastReadVariableName = "enmPaymentStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(vPaymentStatus) 
        pLastReadVariableName = "enmOrderStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(vOrderStatus) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add("GroupByCustomerID", ccDAL.enmSQLDataType.Bit).Value = vGroupByCustomerID
        pLastReadVariableName = "OrderDate" 
        pDALParameters.Add("GroupByOrderDate", ccDAL.enmSQLDataType.Bit).Value = vGroupByOrderDate
        pLastReadVariableName = "enmPaymentStatus" 
        pDALParameters.Add("GroupByenmPaymentStatus", ccDAL.enmSQLDataType.Bit).Value = vGroupByPaymentStatus
        pLastReadVariableName = "enmOrderStatus" 
        pDALParameters.Add("GroupByenmOrderStatus", ccDAL.enmSQLDataType.Bit).Value = vGroupByOrderStatus
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pOrderHeaders As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderHeaders, "clsOrderHeaderCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderHeaders IsNot Nothing AndAlso Me.Count <> pOrderHeaders.Count Then FillFromListOfITargCCEntity(pOrderHeaders) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vOrderHeaderArray As clsOrderHeader())
    Me.Clear()
    
    For Each pOrderHeader As clsOrderHeader In vOrderHeaderArray
      Me.Add(pOrderHeader)
      _Clean.Add(pOrderHeader.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pOrderHeader As New clsOrderHeader(pRow, vRequester, _WithParents) 
        Me.Add(pOrderHeader) 
        _Clean.Add(pOrderHeader.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-OrderHeaderCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-130515-1300", vRequester) 
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
      Dim pOrderHeaders As clsOrderHeaderCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsOrderHeaderCol) 
      For Each pOrderHeader As clsOrderHeader In pOrderHeaders 
        Me.Add(pOrderHeader) 
        _Clean.Add(pOrderHeader.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-OrderHeader-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-190720-1443", vRequester) 
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
 
      Dim pOrderHeaders As List(Of clsOrderHeader) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of clsOrderHeader))(vJSON, pSettings) 
      For Each pOrderHeader As clsOrderHeader In pOrderHeaders 
        Me.Add(pOrderHeader) 
        _Clean.Add(pOrderHeader.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-190720-2059", vRequester) 
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
          For Each lOrderHeader As clsOrderHeader In Me 
            Dim pByte As Byte() = lOrderHeader.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-150307-2340", vRequester) 
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
            Dim pOrderHeader As clsOrderHeader = New clsOrderHeader(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pOrderHeader) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pOrderHeader.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-OrderHeader-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pOrderHeader As clsOrderHeader In Me 
      With pOrderHeader 
        pFault = pOrderHeader.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is clsOrderHeaderCol) Then Return False 
    Dim pOrderHeaderColToTest As clsOrderHeaderCol = CType(vEntitiesToTest, clsOrderHeaderCol) 
    Return isEqual(pOrderHeaderColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vOrderHeadersToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vOrderHeadersToTest As clsOrderHeaderCol) As Boolean
    If Me.Count <> vOrderHeadersToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vOrderHeadersToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pOrderHeaders._FilledFromSumOnTheFly = True
    
    For Each pOrderHeader As clsOrderHeader In Me 
      Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone() 
      pOrderHeaders.Add(pOrderHeaderClone) 
      If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
    Next 
    Return pOrderHeaders 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsOrderHeaderCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pOrderHeaders._FilledFromSumOnTheFly = True
    
    For Each pOrderHeader As clsOrderHeader In Me
      Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
      pOrderHeaders.Add(pOrderHeaderClone)
      If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
    Next
    Return pOrderHeaders
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As clsOrderHeaderCol 
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents)  
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderHeader As clsOrderHeader In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderHeader.ID > vIDFrom AndAlso pOrderHeader.ID <= vIDTo) Then 
        Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone() 
        pOrderHeaders.Add(pOrderHeaderClone) 
        If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
      End If 
    Next 
    Return pOrderHeaders 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OrderDate (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOrderDate(ByVal vOrderDateStart As Date, ByVal vOrderDateEnd As Date) As clsOrderHeaderCol 
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents)  
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderHeader As clsOrderHeader In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderHeader.OrderDate > vOrderDateStart AndAlso pOrderHeader.OrderDate <= vOrderDateEnd) Then 
        Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone() 
        pOrderHeaders.Add(pOrderHeaderClone) 
        If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
      End If 
    Next 
    Return pOrderHeaders 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OrderNumber (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOrderNumber(ByVal vOrderNumberFrom As Integer, ByVal vOrderNumberTo As Integer) As clsOrderHeaderCol 
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents)  
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderHeader As clsOrderHeader In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderHeader.OrderNumber > vOrderNumberFrom AndAlso pOrderHeader.OrderNumber <= vOrderNumberTo) Then 
        Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone() 
        pOrderHeaders.Add(pOrderHeaderClone) 
        If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
      End If 
    Next 
    Return pOrderHeaders 
  End Function 
  
  ''' <summary>
  ''' This loads the dependant parents for each of the rows 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault
    For Each pOrderHeader As clsOrderHeader In Me
      pFault = pOrderHeader.LoadParents(vRequester)
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
  Public Function FindByID(ByVal vID As Long) As clsOrderHeader
    If Me.Count = 0 Then Return New clsOrderHeader 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
    
    Dim pOrderHeader As clsOrderHeader = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pOrderHeader) 
    If pOrderHeader IsNot Nothing Then Return pOrderHeader Else Return New clsOrderHeader() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByOrderNumber(ByVal vOrderNumber As Integer) As clsOrderHeader
    If Me.Count = 0 Then Return New clsOrderHeader 
    
    If _RecreateDictionaryForFindByOrderNumber = True Then LoadOrderNumbers() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, clsOrderHeader) = _SortedDictionaryForFindByOrderNumber 
    
    Dim pOrderHeader As clsOrderHeader = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vOrderNumber.ToString()
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pOrderHeader) 
    If pOrderHeader IsNot Nothing Then Return pOrderHeader Else Return New clsOrderHeader() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderNumber(ByVal vOrderNumber As Integer) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.OrderNumber = vOrderNumber Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOrderNumber with vOrderNumber of {vOrderNumber}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.OrderNumber = vOrderNumber Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CustomerID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCustomerID(ByVal vCustomerID As Long) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.CustomerID = vCustomerID Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCustomerID with vCustomerID of {vCustomerID}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.CustomerID = vCustomerID Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderDate(ByVal vOrderDate As Date) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.OrderDate = vOrderDate Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOrderDate with vOrderDate of {vOrderDate}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.OrderDate = vOrderDate Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TotalAmount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTotalAmount(ByVal vTotalAmount As Decimal) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.TotalAmount = vTotalAmount Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTotalAmount with vTotalAmount of {vTotalAmount}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.TotalAmount = vTotalAmount Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined VATAmount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByVATAmount(ByVal vVATAmount As Decimal) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.VATAmount = vVATAmount Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByVATAmount with vVATAmount of {vVATAmount}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.VATAmount = vVATAmount Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TotalWithVAT
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTotalWithVAT(ByVal vTotalWithVAT As Decimal) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.TotalWithVAT = vTotalWithVAT Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTotalWithVAT with vTotalWithVAT of {vTotalWithVAT}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.TotalWithVAT = vTotalWithVAT Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PaymentMethod
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPaymentMethod(ByVal vPaymentMethod As clsEnums.enmPaymentMethod) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.PaymentMethod = vPaymentMethod Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPaymentMethod with vPaymentMethod of {vPaymentMethod}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.PaymentMethod = vPaymentMethod Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PaymentStatus
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPaymentStatus(ByVal vPaymentStatus As clsEnums.enmPaymentStatus) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.PaymentStatus = vPaymentStatus Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPaymentStatus with vPaymentStatus of {vPaymentStatus}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.PaymentStatus = vPaymentStatus Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PaymentDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPaymentDate(ByVal vPaymentDate As Date) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.PaymentDate = vPaymentDate Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPaymentDate with vPaymentDate of {vPaymentDate}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.PaymentDate = vPaymentDate Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined InvoiceNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByInvoiceNumber(ByVal vInvoiceNumber As String) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vInvoiceNumber = vInvoiceNumber.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.InvoiceNumber.ToLowerInvariant() = vInvoiceNumber Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByInvoiceNumber with vInvoiceNumber of {vInvoiceNumber}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.InvoiceNumber.ToLowerInvariant() = vInvoiceNumber Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DeliveryMethod
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDeliveryMethod(ByVal vDeliveryMethod As clsEnums.enmDeliveryMethod) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.DeliveryMethod = vDeliveryMethod Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDeliveryMethod with vDeliveryMethod of {vDeliveryMethod}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.DeliveryMethod = vDeliveryMethod Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DeliveryDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDeliveryDate(ByVal vDeliveryDate As Date) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.DeliveryDate = vDeliveryDate Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDeliveryDate with vDeliveryDate of {vDeliveryDate}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.DeliveryDate = vDeliveryDate Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DeliveryDay
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDeliveryDay(ByVal vDeliveryDay As clsEnums.enmDeliveryDay) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.DeliveryDay = vDeliveryDay Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDeliveryDay with vDeliveryDay of {vDeliveryDay}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.DeliveryDay = vDeliveryDay Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderStatus
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderStatus(ByVal vOrderStatus As clsEnums.enmOrderStatus) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.OrderStatus = vOrderStatus Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOrderStatus with vOrderStatus of {vOrderStatus}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.OrderStatus = vOrderStatus Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Notes
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNotes(ByVal vNotes As String) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNotes = vNotes.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.Notes.ToLowerInvariant() = vNotes Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNotes with vNotes of {vNotes}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.Notes.ToLowerInvariant() = vNotes Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Notes2
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNotes2(ByVal vNotes2 As String) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNotes2 = vNotes2.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.Notes2.ToLowerInvariant() = vNotes2 Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNotes2 with vNotes2 of {vNotes2}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.Notes2.ToLowerInvariant() = vNotes2 Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderMonth
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderMonth(ByVal vOrderMonth As String) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vOrderMonth = vOrderMonth.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.OrderMonth.ToLowerInvariant() = vOrderMonth Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOrderMonth with vOrderMonth of {vOrderMonth}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.OrderMonth.ToLowerInvariant() = vOrderMonth Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Quarter
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByQuarter(ByVal vQuarter As String) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vQuarter = vQuarter.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.Quarter.ToLowerInvariant() = vQuarter Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByQuarter with vQuarter of {vQuarter}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.Quarter.ToLowerInvariant() = vQuarter Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As clsOrderHeaderCol
    Dim pOrderHeaders As New clsOrderHeaderCol(_WithParents) 
    pOrderHeaders._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsOrderHeader) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pOrderHeader As clsOrderHeader In pTempDist.Values
        If pOrderHeader.Tag.ToLowerInvariant() = vTag Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As clsOrderHeaderCol = Me.Clone() 
      For Each pOrderHeader As clsOrderHeader In pList 
        If pOrderHeader.Tag.ToLowerInvariant() = vTag Then
          Dim pOrderHeaderClone As clsOrderHeader = pOrderHeader.Clone()
          pOrderHeaders.Add(pOrderHeaderClone)
          If Not _FilledFromSumOnTheFly Then pOrderHeaders._Clean.Add(pOrderHeader.ID) 
        End If
      Next
    End If 
    
    Return pOrderHeaders
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
    For Each pOrderHeader As clsOrderHeader In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pOrderHeader.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderView, "clsOrderHeaderCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As clsOrderHeader In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As clsOrderHeader = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pOrderHeaderToKill As New clsOrderHeader 
          pOrderHeaderToKill.ID = pCleanID 
          pOrderHeaderToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pOrderHeaderToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As clsOrderHeader In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-OrderHeader-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderUpdate, "clsOrderHeaderCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As clsOrderHeader In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As clsOrderHeader In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeaderCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeadersDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New clsOrderHeaderCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific CustomerID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByCustomerID(ByVal vCustomerID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerID={0}", vCustomerID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeaderCol_DeleteByCustomerID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeadersDeleteByCustomerID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllOrderHeaders As New clsOrderHeaderCol() : pAllOrderHeaders.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredOrderHeaders As clsOrderHeaderCol = pAllOrderHeaders.CloneByCustomerID(vCustomerID) 
      For Each l In pFilteredOrderHeaders 
        pAllOrderHeaders.Remove(pAllOrderHeaders.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllOrderHeaders, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vCustomerID) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OrderStatus 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByOrderStatus(ByVal vOrderStatus As clsEnums.enmOrderStatus, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderStatus={0}", vOrderStatus)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeaderCol_DeleteByOrderStatus", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeadersDeleteByOrderStatus"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllOrderHeaders As New clsOrderHeaderCol() : pAllOrderHeaders.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredOrderHeaders As clsOrderHeaderCol = pAllOrderHeaders.CloneByOrderStatus(vOrderStatus) 
      For Each l In pFilteredOrderHeaders 
        pAllOrderHeaders.Remove(pAllOrderHeaders.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllOrderHeaders, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmOrderStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vOrderStatus) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific PaymentStatus 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByPaymentStatus(ByVal vPaymentStatus As clsEnums.enmPaymentStatus, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("PaymentStatus={0}", vPaymentStatus)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeaderCol_DeleteByPaymentStatus", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeadersDeleteByPaymentStatus"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllOrderHeaders As New clsOrderHeaderCol() : pAllOrderHeaders.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredOrderHeaders As clsOrderHeaderCol = pAllOrderHeaders.CloneByPaymentStatus(vPaymentStatus) 
      For Each l In pFilteredOrderHeaders 
        pAllOrderHeaders.Remove(pAllOrderHeaders.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllOrderHeaders, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmPaymentStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vPaymentStatus) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OrderDate 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByOrderDate(ByVal vOrderDate As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderDate={0}", vOrderDate)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeaderCol_DeleteByOrderDate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeadersDeleteByOrderDate"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllOrderHeaders As New clsOrderHeaderCol() : pAllOrderHeaders.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredOrderHeaders As clsOrderHeaderCol = pAllOrderHeaders.CloneByOrderDate(vOrderDate) 
      For Each l In pFilteredOrderHeaders 
        pAllOrderHeaders.Remove(pAllOrderHeaders.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllOrderHeaders, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OrderDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = (vOrderDate) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeaderCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeadersDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-OrderHeader-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OrderDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedOrderDate(ByVal vOrderDateStart As Date, ByVal vOrderDateEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderDateStart={0}, OrderDateEnd={1}", vOrderDateStart, vOrderDateEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeaderCol_DeleteByBoundedOrderDate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeadersDeleteByBoundedOrderDate"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-OrderHeader-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OrderDateFrom" 
        pDALParameters.Add("bndOrderDateFrom", ccDAL.enmSQLDataType.DateTime).Value = (vOrderDateStart) 
        pLastReadVariableName = "OrderDateTo" 
        pDALParameters.Add("bndOrderDateTo", ccDAL.enmSQLDataType.DateTime).Value = (vOrderDateEnd) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OrderNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedOrderNumber(ByVal vOrderNumberFrom As Integer, ByVal vOrderNumberTo As Integer, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderNumberFrom={0}, OrderNumberTo={1}", vOrderNumberFrom, vOrderNumberTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_OrderHeaderDelete, "clsOrderHeaderCol_DeleteByBoundedOrderNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccOrderHeadersDeleteByBoundedOrderNumber"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-OrderHeader-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OrderNumberFrom" 
        pDALParameters.Add("bndOrderNumberFrom", ccDAL.enmSQLDataType.Int).Value = (vOrderNumberFrom) 
        pLastReadVariableName = "OrderNumberTo" 
        pDALParameters.Add("bndOrderNumberTo", ccDAL.enmSQLDataType.Int).Value = (vOrderNumberTo) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-OrderHeader-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-OrderHeader-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-090210-1341", vRequester) 
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
    Me.Sort(New clsOrderHeaderCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
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
  
  Public Sub SortByOrderNumber()
    Me.Sort(New clsOrderHeaderCol.CompareByOrderNumber)
  End Sub
  Private Class CompareByOrderNumber
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OrderNumber < y.OrderNumber Then
        Return -1
      ElseIf x.OrderNumber = y.OrderNumber Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByCustomerID()
    Me.Sort(New clsOrderHeaderCol.CompareByCustomerID)
  End Sub
  Private Class CompareByCustomerID
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.CustomerID < y.CustomerID Then
        Return -1
      ElseIf x.CustomerID = y.CustomerID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByCustomerText()
    Me.Sort(New clsOrderHeaderCol.CompareByCustomerText)
  End Sub
  Private Class CompareByCustomerText
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CustomerText, y.CustomerText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOrderDate()
    Me.Sort(New clsOrderHeaderCol.CompareByOrderDate)
  End Sub
  Private Class CompareByOrderDate
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OrderDate < y.OrderDate Then
        Return -1
      ElseIf x.OrderDate = y.OrderDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTotalAmount()
    Me.Sort(New clsOrderHeaderCol.CompareByTotalAmount)
  End Sub
  Private Class CompareByTotalAmount
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TotalAmount < y.TotalAmount Then
        Return -1
      ElseIf x.TotalAmount = y.TotalAmount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByVATAmount()
    Me.Sort(New clsOrderHeaderCol.CompareByVATAmount)
  End Sub
  Private Class CompareByVATAmount
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.VATAmount < y.VATAmount Then
        Return -1
      ElseIf x.VATAmount = y.VATAmount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTotalWithVAT()
    Me.Sort(New clsOrderHeaderCol.CompareByTotalWithVAT)
  End Sub
  Private Class CompareByTotalWithVAT
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TotalWithVAT < y.TotalWithVAT Then
        Return -1
      ElseIf x.TotalWithVAT = y.TotalWithVAT Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByPaymentMethod()
    Me.Sort(New clsOrderHeaderCol.CompareByPaymentMethod)
  End Sub
  Private Class CompareByPaymentMethod
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.PaymentMethod < y.PaymentMethod Then
        Return -1
      ElseIf x.PaymentMethod = y.PaymentMethod Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByPaymentMethodText()
    Me.Sort(New clsOrderHeaderCol.CompareByPaymentMethodText)
  End Sub
  Private Class CompareByPaymentMethodText
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.PaymentMethodText, y.PaymentMethodText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByPaymentStatus()
    Me.Sort(New clsOrderHeaderCol.CompareByPaymentStatus)
  End Sub
  Private Class CompareByPaymentStatus
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.PaymentStatus < y.PaymentStatus Then
        Return -1
      ElseIf x.PaymentStatus = y.PaymentStatus Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByPaymentStatusText()
    Me.Sort(New clsOrderHeaderCol.CompareByPaymentStatusText)
  End Sub
  Private Class CompareByPaymentStatusText
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.PaymentStatusText, y.PaymentStatusText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByPaymentDate()
    Me.Sort(New clsOrderHeaderCol.CompareByPaymentDate)
  End Sub
  Private Class CompareByPaymentDate
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.PaymentDate < y.PaymentDate Then
        Return -1
      ElseIf x.PaymentDate = y.PaymentDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByInvoiceNumber()
    Me.Sort(New clsOrderHeaderCol.CompareByInvoiceNumber)
  End Sub
  Private Class CompareByInvoiceNumber
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.InvoiceNumber, y.InvoiceNumber, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDeliveryMethod()
    Me.Sort(New clsOrderHeaderCol.CompareByDeliveryMethod)
  End Sub
  Private Class CompareByDeliveryMethod
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
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
    Me.Sort(New clsOrderHeaderCol.CompareByDeliveryMethodText)
  End Sub
  Private Class CompareByDeliveryMethodText
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DeliveryMethodText, y.DeliveryMethodText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDeliveryDate()
    Me.Sort(New clsOrderHeaderCol.CompareByDeliveryDate)
  End Sub
  Private Class CompareByDeliveryDate
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DeliveryDate < y.DeliveryDate Then
        Return -1
      ElseIf x.DeliveryDate = y.DeliveryDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDeliveryDay()
    Me.Sort(New clsOrderHeaderCol.CompareByDeliveryDay)
  End Sub
  Private Class CompareByDeliveryDay
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DeliveryDay < y.DeliveryDay Then
        Return -1
      ElseIf x.DeliveryDay = y.DeliveryDay Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDeliveryDayText()
    Me.Sort(New clsOrderHeaderCol.CompareByDeliveryDayText)
  End Sub
  Private Class CompareByDeliveryDayText
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DeliveryDayText, y.DeliveryDayText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOrderStatus()
    Me.Sort(New clsOrderHeaderCol.CompareByOrderStatus)
  End Sub
  Private Class CompareByOrderStatus
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OrderStatus < y.OrderStatus Then
        Return -1
      ElseIf x.OrderStatus = y.OrderStatus Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByOrderStatusText()
    Me.Sort(New clsOrderHeaderCol.CompareByOrderStatusText)
  End Sub
  Private Class CompareByOrderStatusText
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OrderStatusText, y.OrderStatusText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNotes()
    Me.Sort(New clsOrderHeaderCol.CompareByNotes)
  End Sub
  Private Class CompareByNotes
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Notes, y.Notes, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNotes2()
    Me.Sort(New clsOrderHeaderCol.CompareByNotes2)
  End Sub
  Private Class CompareByNotes2
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Notes2, y.Notes2, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOrderMonth()
    Me.Sort(New clsOrderHeaderCol.CompareByOrderMonth)
  End Sub
  Private Class CompareByOrderMonth
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OrderMonth, y.OrderMonth, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByQuarter()
    Me.Sort(New clsOrderHeaderCol.CompareByQuarter)
  End Sub
  Private Class CompareByQuarter
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Quarter, y.Quarter, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New clsOrderHeaderCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsOrderHeader)
    Private Function Compare(ByVal x As clsOrderHeader, ByVal y As clsOrderHeader) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderHeader).Compare
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
  
    Dim pOrderHeader As clsOrderHeader
  
    While vReader.Read()
      pOrderHeader = New clsOrderHeader(_WithParents) 
      pFault = pOrderHeader.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pOrderHeader)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pOrderHeader.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedOrderHeaderCol As clsOrderHeaderCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pOrderHeader As clsOrderHeader 
 
      For Each pCachedOrderHeader As clsOrderHeader In vCachedOrderHeaderCol 
        pCachedOrderHeader.SetWithParents(_WithParents) 
        pOrderHeader = New clsOrderHeader(pCachedOrderHeader) 
        If _WithParents = clsEnums.enmLoadParent.DoNotLoad Then 
          pOrderHeader.CustomerText = "." 
        End If 
        pOrderHeader.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pOrderHeader) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pOrderHeader.ID) 
      Next 
      If _WithParents = clsEnums.enmLoadParent.EntireObject Then 
        pFault = LoadParents(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderHeader-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsOrderHeader) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByOrderNumber = New Dictionary(Of String, clsOrderHeader)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByOrderNumber = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsOrderHeader) 
    _SortedDictionaryForFindByOrderNumber = New Dictionary(Of String, clsOrderHeader)(StringComparer.OrdinalIgnoreCase) 
 
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
  
