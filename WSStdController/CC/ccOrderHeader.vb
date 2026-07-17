Public Class clsOrderHeader
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
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderHeader-151224_0844", vRequester) 
    End Try 
 
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
      Dim pFunction As String = "clsOrderHeaderGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the OrderHeader 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150308-1015", vRequester) 
    End Try 
 
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
    
    CreateEmpty() 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vOrderNumber 
          pBinaryWriter.Write(vOrderNumber) 
          ' 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "clsOrderHeaderGetByOrderNumber" 
      Dim pParametersToLog = $"OrderNumber: {vOrderNumber};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the OrderHeader 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150308-1015", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-OrderHeader-151227-1738", vRequester) 
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
    Dim pFunctionParameters As String = String.Format("OrderHeader.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pOrderHeader As New clsOrderHeader 
    If Me.isEqual(pOrderHeader) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-OrderHeader-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-OrderHeader-240611-135714", vRequester) 
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
      Dim pFunction As String = "clsOrderHeaderUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150314-1803", vRequester) 
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
    Dim pFunctionParameters As String = String.Format("OrderHeader.ID={0}", _ID)
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
      Dim pFunction As String = "clsOrderHeaderDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150314-1803", vRequester) 
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
      Dim pFunction As String = "clsOrderHeaderDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-231207-1707", vRequester) 
    End Try 
 
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
      Dim pFunction As String = "clsOrderHeaderLoadParents" 
      Dim pParametersToLog = $"ID: {_ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150411-1107", vRequester) 
    End Try 
    
    pFault.SetOK() 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsOrderHeader) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByOrderNumber As Dictionary(Of String, clsOrderHeader) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByOrderNumber As Boolean 
  Private Function CreateKeyForFindByOrderNumber(ByVal vOrderHeader As clsOrderHeader) As String 
    With vOrderHeader 
      Return .OrderNumber.ToString()
    End With 
  End Function 
   
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
 
      Dim pFunction As String = "clsOrderHeaderColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific CustomerID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByCustomerID(ByVal vCustomerID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CustomerID={0}", vCustomerID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vCustomerID 
          pBinaryWriter.Write(vCustomerID) 
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
 
      Dim pFunction As String = "clsOrderHeaderColFillByCustomerID" 
      Dim pParametersToLog = $"CustomerID: {vCustomerID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      If vAppend = True Then 
        Dim pOrderHeaders As New clsOrderHeaderCol 
        pOrderHeaders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderHeaders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OrderStatus, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOrderStatus(ByVal vOrderStatus As clsEnums.enmOrderStatus, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderStatus={0}", vOrderStatus)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderStatus 
          pBinaryWriter.Write(vOrderStatus.ToString()) 
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
 
      Dim pFunction As String = "clsOrderHeaderColFillByOrderStatus" 
      Dim pParametersToLog = $"OrderStatus: {vOrderStatus};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      If vAppend = True Then 
        Dim pOrderHeaders As New clsOrderHeaderCol 
        pOrderHeaders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderHeaders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific PaymentStatus, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByPaymentStatus(ByVal vPaymentStatus As clsEnums.enmPaymentStatus, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("PaymentStatus={0}", vPaymentStatus)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vPaymentStatus 
          pBinaryWriter.Write(vPaymentStatus.ToString()) 
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
 
      Dim pFunction As String = "clsOrderHeaderColFillByPaymentStatus" 
      Dim pParametersToLog = $"PaymentStatus: {vPaymentStatus};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      If vAppend = True Then 
        Dim pOrderHeaders As New clsOrderHeaderCol 
        pOrderHeaders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderHeaders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByOrderDate(ByVal vOrderDate As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderDate={0}", vOrderDate)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderDate 
          pBinaryWriter.Write(vOrderDate.Ticks) 
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
 
      Dim pFunction As String = "clsOrderHeaderColFillByOrderDate" 
      Dim pParametersToLog = $"OrderDate: {vOrderDate};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      If vAppend = True Then 
        Dim pOrderHeaders As New clsOrderHeaderCol 
        pOrderHeaders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderHeaders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "clsOrderHeaderColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      If vAppend = True Then 
        Dim pOrderHeaders As New clsOrderHeaderCol 
        pOrderHeaders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderHeaders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderDateStart 
          pBinaryWriter.Write(vOrderDateStart.Ticks) 
          ' 
          'vOrderDateEnd 
          pBinaryWriter.Write(vOrderDateEnd.Ticks) 
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
 
      Dim pFunction As String = "clsOrderHeaderColFillByBoundedOrderDate" 
      Dim pParametersToLog = $"OrderDate: {vOrderDateStart};{vOrderDateEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      If vAppend = True Then 
        Dim pOrderHeaders As New clsOrderHeaderCol 
        pOrderHeaders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderHeaders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderNumberFrom 
          pBinaryWriter.Write(vOrderNumberFrom) 
          ' 
          'vOrderNumberTo 
          pBinaryWriter.Write(vOrderNumberTo) 
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
 
      Dim pFunction As String = "clsOrderHeaderColFillByBoundedOrderNumber" 
      Dim pParametersToLog = $"OrderNumber: {vOrderNumberFrom};{vOrderNumberTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      If vAppend = True Then 
        Dim pOrderHeaders As New clsOrderHeaderCol 
        pOrderHeaders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderHeaders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "clsOrderHeaderColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader   
      If vAppend = True Then 
        Dim pOrderHeaders As New clsOrderHeaderCol 
        pOrderHeaders.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderHeaders) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-231207-1750", vRequester) 
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
          'OrderNumber 
          pBinaryWriter.Write(vOrderNumberFrom.HasValue) 
          If vOrderNumberFrom.HasValue Then pBinaryWriter.Write(vOrderNumberFrom.Value) : pParametersToLog &= $"OrderNumberFrom={vOrderNumberFrom};"  
          pBinaryWriter.Write(vOrderNumberTo.HasValue) 
          If vOrderNumberTo.HasValue Then pBinaryWriter.Write(vOrderNumberTo.Value) : pParametersToLog &= $"OrderNumberTo={vOrderNumberTo};"  
          'CustomerID 
          pBinaryWriter.Write(vCustomerID.HasValue) 
          If vCustomerID.HasValue = True Then pBinaryWriter.Write(vCustomerID.Value) : pParametersToLog &= $"CustomerID={vCustomerID};"  
          'OrderDate 
          pBinaryWriter.Write(vOrderDateStart.HasValue) 
          If vOrderDateStart.HasValue Then pBinaryWriter.Write(vOrderDateStart.Value.Ticks) : pParametersToLog &= $"OrderDateStart={vOrderDateStart.Value};"  
          pBinaryWriter.Write(vOrderDateEnd.HasValue) 
          If vOrderDateEnd.HasValue Then pBinaryWriter.Write(vOrderDateEnd.Value.Ticks) : pParametersToLog &= $"OrderDateEnd={vOrderDateEnd.Value};"  
          'PaymentStatus 
          pBinaryWriter.Write(vPaymentStatus.ToString()) : pParametersToLog &= $"PaymentStatus={vPaymentStatus};"  
          'OrderStatus 
          pBinaryWriter.Write(vOrderStatus.ToString()) : pParametersToLog &= $"OrderStatus={vOrderStatus};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderHeaderColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
          'OrderNumber 
          pBinaryWriter.Write(vOrderNumberFrom.HasValue) 
          If vOrderNumberFrom.HasValue Then pBinaryWriter.Write(vOrderNumberFrom.Value) : pParametersToLog &= $"OrderNumberFrom={vOrderNumberFrom};"  
          pBinaryWriter.Write(vOrderNumberTo.HasValue) 
          If vOrderNumberTo.HasValue Then pBinaryWriter.Write(vOrderNumberTo.Value) : pParametersToLog &= $"OrderNumberTo={vOrderNumberTo};"  
          'CustomerID 
          pBinaryWriter.Write(vCustomerID.HasValue) 
          If vCustomerID.HasValue = True Then pBinaryWriter.Write(vCustomerID.Value) : pParametersToLog &= $"CustomerID={vCustomerID};"  
          'OrderDate 
          pBinaryWriter.Write(vOrderDateStart.HasValue) 
          If vOrderDateStart.HasValue Then pBinaryWriter.Write(vOrderDateStart.Value.Ticks) : pParametersToLog &= $"OrderDateStart={vOrderDateStart};"  
          pBinaryWriter.Write(vOrderDateEnd.HasValue) 
          If vOrderDateEnd.HasValue Then pBinaryWriter.Write(vOrderDateEnd.Value.Ticks) : pParametersToLog &= $"OrderDateEnd={vOrderDateEnd};"  
          'PaymentStatus 
          pBinaryWriter.Write(vPaymentStatus.ToString()) : pParametersToLog &= $"PaymentStatus={vPaymentStatus};"  
          'OrderStatus 
          pBinaryWriter.Write(vOrderStatus.ToString()) : pParametersToLog &= $"OrderStatus={vOrderStatus};"  
          pBinaryWriter.Write(vGroupByCustomerID) : pParametersToLog &= $"GroupByCustomerID={vGroupByCustomerID};"  
          pBinaryWriter.Write(vGroupByOrderDate) : pParametersToLog &= $"GroupByOrderDate={vGroupByOrderDate};"  
          pBinaryWriter.Write(vGroupByPaymentStatus) : pParametersToLog &= $"GroupByPaymentStatus={vGroupByPaymentStatus};"  
          pBinaryWriter.Write(vGroupByOrderStatus) : pParametersToLog &= $"GroupByOrderStatus={vGroupByOrderStatus};"  
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderHeaderColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeader  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
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
      Dim pFunction As String = "clsOrderHeaderColLoadParents" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeaderCol 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150314-1803", vRequester) 
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
 
    'Check for new rows 
    For Each p As clsOrderHeader In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
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
      Dim pFunction As String = "clsOrderHeaderColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeaderCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150314-1803", vRequester) 
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
      Dim pFunction As String = "clsOrderHeaderColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderHeaderCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-150314-1803", vRequester) 
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
 
      Dim pFunction As String = "clsOrderHeaderColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vCustomerID 
          pBinaryWriter.Write(vCustomerID) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderHeaderColDeleteByCustomerID" 
      Dim pParametersToLog = $"CustomerID: {vCustomerID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderStatus 
          pBinaryWriter.Write(vOrderStatus.ToString()) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderHeaderColDeleteByOrderStatus" 
      Dim pParametersToLog = $"OrderStatus: {vOrderStatus};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vPaymentStatus 
          pBinaryWriter.Write(vPaymentStatus.ToString()) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderHeaderColDeleteByPaymentStatus" 
      Dim pParametersToLog = $"PaymentStatus: {vPaymentStatus};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderDate 
          pBinaryWriter.Write(vOrderDate.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderHeaderColDeleteByOrderDate" 
      Dim pParametersToLog = $"OrderDate: {vOrderDate};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderHeader-200709-0852-", vRequester) 
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
 
      Dim pFunction As String = "clsOrderHeaderColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderDateStart 
          pBinaryWriter.Write(vOrderDateStart.Ticks) 
          ' 
          'vOrderDateEnd 
          pBinaryWriter.Write(vOrderDateEnd.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderHeaderColDeleteByBoundedOrderDate" 
      Dim pParametersToLog = $"OrderDate: {vOrderDateStart};{vOrderDateEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderNumberFrom 
          pBinaryWriter.Write(vOrderNumberFrom) 
          ' 
          'vOrderNumberTo 
          pBinaryWriter.Write(vOrderNumberTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderHeaderColDeleteByBoundedOrderNumber" 
      Dim pParametersToLog = $"OrderNumber: {vOrderNumberFrom};{vOrderNumberTo};" 
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
  
