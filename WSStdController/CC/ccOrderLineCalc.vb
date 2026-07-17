Public Class clsOrderLineCalc
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
    [OrderHeaderID] 
    [ProductID] 
    [ProductName] 
    [ProductCode] 
    [Quantity] 
    [UnitPrice] 
    [UnitCost] 
    [LineTotal] 
    [TotalCost] 
    [GrossProfit] 
    [ProfitMarginPercent] 
    [LineNumber] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [OrderHeaderID] 
    [ProductID] 
    [Quantity] 
    [UnitPrice] 
    [UnitCost] 
    [LineTotal] 
    [TotalCost] 
    [GrossProfit] 
    [ProfitMarginPercent] 
    [LineNumber] 
  End Enum 
  
  Private _ID As Long
  Private _OrderHeaderID As Long
  Private _ProductID As Long
  Private _ProductName As String
  Private _ProductCode As String
  Private _Quantity As Integer
  Private _UnitPrice As Decimal
  Private _UnitCost As Decimal
  Private _LineTotal As Decimal
  Private _TotalCost As Decimal
  Private _GrossProfit As Decimal
  Private _ProfitMarginPercent As Decimal
  Private _LineNumber As Integer
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
  Public Property [ProductID]() As Long
    Get
      Return Me._ProductID
    End Get
    Set(ByVal value As Long)
      If Me._ProductID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProductID = value 
      End If 
    End Set
  End Property
  Public Property [ProductName]() As String
    Get
      Return Me._ProductName
    End Get
    Set(ByVal value As String)
      If Me._ProductName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProductName = value 
      End If 
    End Set
  End Property
  Public Property [ProductCode]() As String
    Get
      Return Me._ProductCode
    End Get
    Set(ByVal value As String)
      If Me._ProductCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProductCode = value 
      End If 
    End Set
  End Property
  Public Property [Quantity]() As Integer
    Get
      Return Me._Quantity
    End Get
    Set(ByVal value As Integer)
      If Me._Quantity <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Quantity = value 
      End If 
    End Set
  End Property
  Public Property [UnitPrice]() As Decimal
    Get
      Return Me._UnitPrice
    End Get
    Set(ByVal value As Decimal)
      If Me._UnitPrice <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UnitPrice = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [UnitCost]() As Decimal
    Get
      Return Me._UnitCost
    End Get
  End Property
  Public Property [LineTotal]() As Decimal
    Get
      Return Me._LineTotal
    End Get
    Set(ByVal value As Decimal)
      If Me._LineTotal <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LineTotal = value 
      End If 
    End Set
  End Property
  Public Property [TotalCost]() As Decimal
    Get
      Return Me._TotalCost
    End Get
    Set(ByVal value As Decimal)
      If Me._TotalCost <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TotalCost = value 
      End If 
    End Set
  End Property
  Public Property [GrossProfit]() As Decimal
    Get
      Return Me._GrossProfit
    End Get
    Set(ByVal value As Decimal)
      If Me._GrossProfit <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._GrossProfit = value 
      End If 
    End Set
  End Property
  Public Property [ProfitMarginPercent]() As Decimal
    Get
      Return Me._ProfitMarginPercent
    End Get
    Set(ByVal value As Decimal)
      If Me._ProfitMarginPercent <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProfitMarginPercent = value 
      End If 
    End Set
  End Property
  Public Property [LineNumber]() As Integer
    Get
      Return Me._LineNumber
    End Get
    Set(ByVal value As Integer)
      If Me._LineNumber <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LineNumber = value 
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
    If _OrderHeaderID <> 0 Then pValue.Append("OrderHeaderID='" & _OrderHeaderID.ToString() & "' ‡ ") 
    If _ProductID <> 0 Then pValue.Append("ProductID='" & _ProductID.ToString() & "' ‡ ") 
    If _ProductName <> "" Then pValue.Append("ProductName='" & _ProductName & "' ‡ ") 
    If _ProductCode <> "" Then pValue.Append("ProductCode='" & _ProductCode & "' ‡ ") 
    If _Quantity <> 0 Then pValue.Append("Quantity='" & _Quantity.ToString() & "' ‡ ") 
    If _UnitPrice <> 0 Then pValue.Append("UnitPrice='" & _UnitPrice.ToString() & "' ‡ ") 
    If _UnitCost <> 0 Then pValue.Append("UnitCost='" & _UnitCost.ToString() & "' ‡ ") 
    If _LineTotal <> 0 Then pValue.Append("LineTotal='" & _LineTotal.ToString() & "' ‡ ") 
    If _TotalCost <> 0 Then pValue.Append("TotalCost='" & _TotalCost.ToString() & "' ‡ ") 
    If _GrossProfit <> 0 Then pValue.Append("GrossProfit='" & _GrossProfit.ToString() & "' ‡ ") 
    If _ProfitMarginPercent <> 0 Then pValue.Append("ProfitMarginPercent='" & _ProfitMarginPercent.ToString() & "' ‡ ") 
    If _LineNumber <> 0 Then pValue.Append("LineNumber='" & _LineNumber.ToString() & "' ‡ ") 
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
    pCSV.Append("," & _ProductID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProductName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProductCode)}""") 
    pCSV.Append("," & _Quantity.ToString() & "") 
    pCSV.Append("," & _UnitPrice.ToString() & "") 
    pCSV.Append("," & _UnitCost.ToString() & "") 
    pCSV.Append("," & _LineTotal.ToString() & "") 
    pCSV.Append("," & _TotalCost.ToString() & "") 
    pCSV.Append("," & _GrossProfit.ToString() & "") 
    pCSV.Append("," & _ProfitMarginPercent.ToString() & "") 
    pCSV.Append("," & _LineNumber.ToString() & "") 
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
  
  Public Sub New(ByVal vclsOrderLineCalc As clsOrderLineCalc)
    MyBase.New()
    CreateEmpty()
    AssignValues(vclsOrderLineCalc) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vOrderHeaderID As Long = 0 _ 
    , Optional vProductID As Long = 0 _ 
    , Optional vProductName As String = "" _ 
    , Optional vProductCode As String = "" _ 
    , Optional vQuantity As Integer = 0 _ 
    , Optional vUnitPrice As Decimal = 0 _ 
    , Optional vUnitCost As Decimal = 0 _ 
    , Optional vLineTotal As Decimal = 0 _ 
    , Optional vTotalCost As Decimal = 0 _ 
    , Optional vGrossProfit As Decimal = 0 _ 
    , Optional vProfitMarginPercent As Decimal = 0 _ 
    , Optional vLineNumber As Integer = 0 _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _OrderHeaderID = vOrderHeaderID 
    _ProductID = vProductID 
    _ProductName = vProductName 
    _ProductCode = vProductCode 
    _Quantity = vQuantity 
    _UnitPrice = vUnitPrice 
    _UnitCost = vUnitCost 
    _LineTotal = vLineTotal 
    _TotalCost = vTotalCost 
    _GrossProfit = vGrossProfit 
    _ProfitMarginPercent = vProfitMarginPercent 
    _LineNumber = vLineNumber 
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
 
    _ProductName = _ProductName.Truncate(pTruncateLength, _IsTruncated) 
    _ProductCode = _ProductCode.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the OrderLineCalc by PrimaryKey (ID) 
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
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderLineCalc-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the OrderLineCalc by the chosen parameters. This function may be a bit slower than accessing the OrderLineCalc's GetBy... directly 
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
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-OrderLineCalc-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderLineCalc-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the OrderLineCalc by ID. 
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
      Dim pFunction As String = "clsOrderLineCalcGetByID" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150308-1015", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is clsOrderLineCalc) Then Return False 
    Dim pOrderLineCalcToTest As clsOrderLineCalc = CType(vTargCCEntityToTest, clsOrderLineCalc) 
    Return isEqual(pOrderLineCalcToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vOrderLineCalcToTest As clsOrderLineCalc) As Boolean
    With vOrderLineCalcToTest
      If _ID <> .ID Then Return False
      If _OrderHeaderID <> .OrderHeaderID Then Return False
      If _ProductID <> .ProductID Then Return False
      If _ProductName <> .ProductName Then Return False
      If _ProductCode <> .ProductCode Then Return False
      If _Quantity <> .Quantity Then Return False
      If _UnitPrice <> .UnitPrice Then Return False
      If _UnitCost <> .UnitCost Then Return False
      If _LineTotal <> .LineTotal Then Return False
      If _TotalCost <> .TotalCost Then Return False
      If _GrossProfit <> .GrossProfit Then Return False
      If _ProfitMarginPercent <> .ProfitMarginPercent Then Return False
      If _LineNumber <> .LineNumber Then Return False
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
    Dim pClone As New clsOrderLineCalc(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsOrderLineCalc
    Dim pClone As New clsOrderLineCalc(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderHeaderID") = _OrderHeaderID : Catch ex As Exception : Return pFault.LogException(ex, "OrderHeaderID", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductID") = _ProductID : Catch ex As Exception : Return pFault.LogException(ex, "ProductID", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductName") = _ProductName : Catch ex As Exception : Return pFault.LogException(ex, "ProductName", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductCode") = _ProductCode : Catch ex As Exception : Return pFault.LogException(ex, "ProductCode", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("Quantity") = _Quantity : Catch ex As Exception : Return pFault.LogException(ex, "Quantity", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("UnitPrice") = _UnitPrice : Catch ex As Exception : Return pFault.LogException(ex, "UnitPrice", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("UnitCost") = _UnitCost : Catch ex As Exception : Return pFault.LogException(ex, "UnitCost", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("LineTotal") = _LineTotal : Catch ex As Exception : Return pFault.LogException(ex, "LineTotal", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("TotalCost") = _TotalCost : Catch ex As Exception : Return pFault.LogException(ex, "TotalCost", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("GrossProfit") = _GrossProfit : Catch ex As Exception : Return pFault.LogException(ex, "GrossProfit", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProfitMarginPercent") = _ProfitMarginPercent : Catch ex As Exception : Return pFault.LogException(ex, "ProfitMarginPercent", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("LineNumber") = _LineNumber : Catch ex As Exception : Return pFault.LogException(ex, "LineNumber", "TRGT-OrderLineCalc-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineCalc-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pOrderLineCalc As clsOrderLineCalc = CType(pXmlSerializer.Deserialize(pStreamReader), clsOrderLineCalc) 
      AssignValues(pOrderLineCalc) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-OrderLineCalc-130515-1230", vRequester) 
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
          'OrderHeaderID 
          pBinaryWriter.Write(_OrderHeaderID) 
          'ProductID 
          pBinaryWriter.Write(_ProductID) 
          'ProductName 
          If _ProductName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProductName) 
          'ProductCode 
          If _ProductCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProductCode) 
          'Quantity 
          pBinaryWriter.Write(_Quantity) 
          'UnitPrice 
          pBinaryWriter.Write(_UnitPrice) 
          'UnitCost 
          pBinaryWriter.Write(_UnitCost) 
          'LineTotal 
          pBinaryWriter.Write(_LineTotal) 
          'TotalCost 
          pBinaryWriter.Write(_TotalCost) 
          'GrossProfit 
          pBinaryWriter.Write(_GrossProfit) 
          'ProfitMarginPercent 
          pBinaryWriter.Write(_ProfitMarginPercent) 
          'LineNumber 
          pBinaryWriter.Write(_LineNumber) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineCalc-150307-2338", vRequester) 
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
          'OrderHeaderID 
          _OrderHeaderID = pReader.ReadInt64 
          'ProductID 
          _ProductID = pReader.ReadInt64 
          'ProductName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProductName = pReader.ReadString 
          'ProductCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProductCode = pReader.ReadString 
          'Quantity 
          _Quantity = pReader.ReadInt32 
          'UnitPrice 
          _UnitPrice = pReader.ReadDecimal 
          'UnitCost 
          _UnitCost = pReader.ReadDecimal 
          'LineTotal 
          _LineTotal = pReader.ReadDecimal 
          'TotalCost 
          _TotalCost = pReader.ReadDecimal 
          'GrossProfit 
          _GrossProfit = pReader.ReadDecimal 
          'ProfitMarginPercent 
          _ProfitMarginPercent = pReader.ReadDecimal 
          'LineNumber 
          _LineNumber = pReader.ReadInt32 
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
      rFault.LogException(ex, "", "TRGT-OrderLineCalc-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineCalc-190720-1443", vRequester) 
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
 
      Dim pOrderLineCalc As clsOrderLineCalc = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsOrderLineCalc)(vJSON, pSettings) 
      AssignValues(pOrderLineCalc) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineCalc-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vOrderLineCalc As clsOrderLineCalc)
    With vOrderLineCalc
      _ID = .ID 
      _OrderHeaderID = .OrderHeaderID 
      _ProductID = .ProductID 
      _ProductName = .ProductName 
      _ProductCode = .ProductCode 
      _Quantity = .Quantity 
      _UnitPrice = .UnitPrice 
      _UnitCost = .UnitCost 
      _LineTotal = .LineTotal 
      _TotalCost = .TotalCost 
      _GrossProfit = .GrossProfit 
      _ProfitMarginPercent = .ProfitMarginPercent 
      _LineNumber = .LineNumber 
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
    _OrderHeaderID = 0
    _ProductID = 0
    _ProductName = ""
    _ProductCode = ""
    _Quantity = 0
    _UnitPrice = 0
    _UnitCost = 0
    _LineTotal = 0
    _TotalCost = 0
    _GrossProfit = 0
    _ProfitMarginPercent = 0
    _LineNumber = 0
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
  
Public Class clsOrderLineCalcCol
  Inherits cTargCCCollection(Of clsOrderLineCalc)
  
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
  
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsOrderLineCalc) 
  Private _LockForFindByID As New Object 
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
 
    For Each pRow As clsOrderLineCalc In Me 
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
    pCSVTitle.Append(",""OrderHeaderID""") 
    pCSVTitle.Append(",""ProductID""") 
    pCSVTitle.Append(",""ProductName""") 
    pCSVTitle.Append(",""ProductCode""") 
    pCSVTitle.Append(",""Quantity""") 
    pCSVTitle.Append(",""UnitPrice""") 
    pCSVTitle.Append(",""UnitCost""") 
    pCSVTitle.Append(",""LineTotal""") 
    pCSVTitle.Append(",""TotalCost""") 
    pCSVTitle.Append(",""GrossProfit""") 
    pCSVTitle.Append(",""ProfitMarginPercent""") 
    pCSVTitle.Append(",""LineNumber""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As clsOrderLineCalc In Me 
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
 
  Public Overloads Sub Add(ByVal vOrderLineCalc As clsOrderLineCalc) 
    MyBase.Add(vOrderLineCalc) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vOrderLineCalc As clsOrderLineCalc) 
    MyBase.Insert(vIndex, vOrderLineCalc) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub AddRange(ByVal vOrderLineCalcCol As clsOrderLineCalcCol) 
    MyBase.AddRange(vOrderLineCalcCol) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    MyBase.RemoveAt(vIndex) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub Remove(ByVal vOrderLineCalc As clsOrderLineCalc) 
    MyBase.Remove(vOrderLineCalc) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
 
  Private Sub LoadIDs() 
    If _RecreateDictionaryForFindByID = True Then 
      SyncLock _LockForFindByID 
        If _RecreateDictionaryForFindByID = True Then 
          _SortedDictionaryForFindByID = New Dictionary(Of Long, clsOrderLineCalc) 
          If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByID = False : Exit Sub 'Not logical 
          For Each lOrderLineCalc In Me 
            If lOrderLineCalc.IsEmpty OrElse _SortedDictionaryForFindByID.ContainsKey(lOrderLineCalc.ID) Then 
              'Not Unique or no ID 
              Continue For 
            End If 
            Try 
              _SortedDictionaryForFindByID.Add(lOrderLineCalc.ID, lOrderLineCalc) 
            Catch ex As Exception 
              Dim pFault As New clsFault 
              pFault.LogException(ex, lOrderLineCalc.ToString, "TRGT-OrderLineCalc-190412-1939", Nothing) 'Log it 
              Throw New Exception("Failed _SortedDictionaryForFindByID:" & ex.Message & ", OrderLineCalc:" & lOrderLineCalc.ToString() & ", TRGT-OrderLineCalc-190412-1939") 'Send it up the line 
            End Try 
          Next 
          _RecreateDictionaryForFindByID = False 
        End If 
      End SyncLock 
    End If 
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
 
    For Each lOrderLineCalc As clsOrderLineCalc In Me 
      lOrderLineCalc.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [OrderHeaderID] 
    [ProductID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the OrderLineCalcs by the chosen parameters. This function may be a bit slower than accessing the OrderLineCalc's FillBy... directly 
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
        Case enmFillByParameterCombination.ProductID 
          pFault = FillByProductID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-OrderLineCalc-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderLineCalc-151223_1716", vRequester) 
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
 
      Dim pFunction As String = "clsOrderLineCalcColFill" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150308-1015", vRequester) 
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
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderLineCalcColFillByOrderHeaderID" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc  
      If vAppend = True Then 
        Dim pOrderLineCalcs As New clsOrderLineCalcCol 
        pOrderLineCalcs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderLineCalcs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ProductID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByProductID(ByVal vProductID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ProductID={0}", vProductID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vProductID 
          pBinaryWriter.Write(vProductID) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderLineCalcColFillByProductID" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc  
      If vAppend = True Then 
        Dim pOrderLineCalcs As New clsOrderLineCalcCol 
        pOrderLineCalcs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderLineCalcs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150407-2142", vRequester) 
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
  Public Function FillByBoundedOrderHeaderID(ByVal vOrderHeaderIDFrom As Long, ByVal vOrderHeaderIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderHeaderIDFrom={0}, OrderHeaderIDTo={1}", vOrderHeaderIDFrom, vOrderHeaderIDTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOrderHeaderIDFrom 
          pBinaryWriter.Write(vOrderHeaderIDFrom) 
          ' 
          'vOrderHeaderIDTo 
          pBinaryWriter.Write(vOrderHeaderIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderLineCalcColFillByBoundedOrderHeaderID" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc  
      If vAppend = True Then 
        Dim pOrderLineCalcs As New clsOrderLineCalcCol 
        pOrderLineCalcs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderLineCalcs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ProductID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedProductID(ByVal vProductIDFrom As Long, ByVal vProductIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ProductIDFrom={0}, ProductIDTo={1}", vProductIDFrom, vProductIDTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vProductIDFrom 
          pBinaryWriter.Write(vProductIDFrom) 
          ' 
          'vProductIDTo 
          pBinaryWriter.Write(vProductIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderLineCalcColFillByBoundedProductID" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc  
      If vAppend = True Then 
        Dim pOrderLineCalcs As New clsOrderLineCalcCol 
        pOrderLineCalcs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderLineCalcs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "clsOrderLineCalcColFillByBoundedID" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc  
      If vAppend = True Then 
        Dim pOrderLineCalcs As New clsOrderLineCalcCol 
        pOrderLineCalcs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderLineCalcs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "clsOrderLineCalcColFillByListOfID" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc   
      If vAppend = True Then 
        Dim pOrderLineCalcs As New clsOrderLineCalcCol 
        pOrderLineCalcs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pOrderLineCalcs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-231207-1750", vRequester) 
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
    OrderHeaderIDFrom
    OrderHeaderIDTo
    ProductIDFrom
    ProductIDTo
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
    Dim pOrderHeaderIDFrom As Nullable(Of Long) = Nothing
    Dim pOrderHeaderIDTo As Nullable(Of Long) = Nothing
    Dim pProductIDFrom As Nullable(Of Long) = Nothing
    Dim pProductIDTo As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderIDFrom) : If pObj IsNot Nothing Then pOrderHeaderIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderIDTo) : If pObj IsNot Nothing Then pOrderHeaderIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ProductIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.ProductIDFrom) : If pObj IsNot Nothing Then pProductIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ProductIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.ProductIDTo) : If pObj IsNot Nothing Then pProductIDTo = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pOrderHeaderIDFrom, pOrderHeaderIDTo _
        , pProductIDFrom, pProductIDTo _
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
        , ByVal vOrderHeaderIDFrom As Nullable(Of Long), ByVal vOrderHeaderIDTo As Nullable(Of Long) _
        , ByVal vProductIDFrom As Nullable(Of Long), ByVal vProductIDTo As Nullable(Of Long) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OrderHeaderIDFrom={2}, OrderHeaderIDTo={3}, ProductIDFrom={4}, ProductIDTo={5}", vIDFrom, vIDTo, vOrderHeaderIDFrom, vOrderHeaderIDTo, vProductIDFrom, vProductIDTo)
    
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) 
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) 
          'OrderHeaderID 
          pBinaryWriter.Write(vOrderHeaderIDFrom.HasValue) 
          If vOrderHeaderIDFrom.HasValue Then pBinaryWriter.Write(vOrderHeaderIDFrom.Value) 
          pBinaryWriter.Write(vOrderHeaderIDTo.HasValue) 
          If vOrderHeaderIDTo.HasValue Then pBinaryWriter.Write(vOrderHeaderIDTo.Value) 
          'ProductID 
          pBinaryWriter.Write(vProductIDFrom.HasValue) 
          If vProductIDFrom.HasValue Then pBinaryWriter.Write(vProductIDFrom.Value) 
          pBinaryWriter.Write(vProductIDTo.HasValue) 
          If vProductIDTo.HasValue Then pBinaryWriter.Write(vProductIDTo.Value) 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderLineCalcColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByOrderHeaderID
    GroupByProductID
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
    Dim pOrderHeaderIDFrom As Nullable(Of Long) = Nothing
    Dim pOrderHeaderIDTo As Nullable(Of Long) = Nothing
    Dim pProductIDFrom As Nullable(Of Long) = Nothing
    Dim pProductIDTo As Nullable(Of Long) = Nothing
    Dim pGroupByOrderHeaderID As Boolean = False
    Dim pGroupByProductID As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderIDFrom) : If pObj IsNot Nothing Then pOrderHeaderIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderIDTo) : If pObj IsNot Nothing Then pOrderHeaderIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ProductIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.ProductIDFrom) : If pObj IsNot Nothing Then pProductIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ProductIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.ProductIDTo) : If pObj IsNot Nothing Then pProductIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOrderHeaderID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOrderHeaderID) : If pObj IsNot Nothing Then pGroupByOrderHeaderID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByProductID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByProductID) : If pObj IsNot Nothing Then pGroupByProductID = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pOrderHeaderIDFrom, pOrderHeaderIDTo _
        , pProductIDFrom, pProductIDTo _
        , pGroupByOrderHeaderID _
        , pGroupByProductID _
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
        , ByVal vOrderHeaderIDFrom As Nullable(Of Long), ByVal vOrderHeaderIDTo As Nullable(Of Long) _
        , ByVal vProductIDFrom As Nullable(Of Long), ByVal vProductIDTo As Nullable(Of Long) _
        , ByVal vGroupByOrderHeaderID As Boolean _
        , ByVal vGroupByProductID As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OrderHeaderIDFrom={2}, OrderHeaderIDTo={3}, ProductIDFrom={4}, ProductIDTo={5}, GroupByOrderHeaderID={6}, GroupByProductID={7}", vIDFrom, vIDTo, vOrderHeaderIDFrom, vOrderHeaderIDTo, vProductIDFrom, vProductIDTo, vGroupByOrderHeaderID, vGroupByProductID)
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) 
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) 
          'OrderHeaderID 
          pBinaryWriter.Write(vOrderHeaderIDFrom.HasValue) 
          If vOrderHeaderIDFrom.HasValue Then pBinaryWriter.Write(vOrderHeaderIDFrom.Value) 
          pBinaryWriter.Write(vOrderHeaderIDTo.HasValue) 
          If vOrderHeaderIDTo.HasValue Then pBinaryWriter.Write(vOrderHeaderIDTo.Value) 
          'ProductID 
          pBinaryWriter.Write(vProductIDFrom.HasValue) 
          If vProductIDFrom.HasValue Then pBinaryWriter.Write(vProductIDFrom.Value) 
          pBinaryWriter.Write(vProductIDTo.HasValue) 
          If vProductIDTo.HasValue Then pBinaryWriter.Write(vProductIDTo.Value) 
          pBinaryWriter.Write(vGroupByOrderHeaderID) 
          pBinaryWriter.Write(vGroupByProductID) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "clsOrderLineCalcColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the OrderLineCalc  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-OrderLineCalc-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vOrderLineCalcArray As clsOrderLineCalc())
    Me.Clear()
    
    For Each pOrderLineCalc As clsOrderLineCalc In vOrderLineCalcArray
      Me.Add(pOrderLineCalc)
      _Clean.Add(pOrderLineCalc.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pOrderLineCalc As New clsOrderLineCalc(pRow, vRequester) 
        Me.Add(pOrderLineCalc) 
        _Clean.Add(pOrderLineCalc.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-OrderLineCalcCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineCalc-130515-1300", vRequester) 
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
      Dim pOrderLineCalcs As clsOrderLineCalcCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsOrderLineCalcCol) 
      For Each pOrderLineCalc As clsOrderLineCalc In pOrderLineCalcs 
        Me.Add(pOrderLineCalc) 
        _Clean.Add(pOrderLineCalc.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-OrderLineCalc-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineCalc-190720-1443", vRequester) 
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
 
      Dim pOrderLineCalcs As List(Of clsOrderLineCalc) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of clsOrderLineCalc))(vJSON, pSettings) 
      For Each pOrderLineCalc As clsOrderLineCalc In pOrderLineCalcs 
        Me.Add(pOrderLineCalc) 
        _Clean.Add(pOrderLineCalc.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineCalc-190720-2059", vRequester) 
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
          For Each lOrderLineCalc As clsOrderLineCalc In Me 
            Dim pByte As Byte() = lOrderLineCalc.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineCalc-150307-2340", vRequester) 
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
            Dim pOrderLineCalc As clsOrderLineCalc = New clsOrderLineCalc(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pOrderLineCalc) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pOrderLineCalc.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-OrderLineCalc-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pOrderLineCalc As clsOrderLineCalc In Me 
      With pOrderLineCalc 
        pFault = pOrderLineCalc.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is clsOrderLineCalcCol) Then Return False 
    Dim pOrderLineCalcColToTest As clsOrderLineCalcCol = CType(vEntitiesToTest, clsOrderLineCalcCol) 
    Return isEqual(pOrderLineCalcColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vOrderLineCalcsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vOrderLineCalcsToTest As clsOrderLineCalcCol) As Boolean
    If Me.Count <> vOrderLineCalcsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vOrderLineCalcsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    If pFilledFromSumOnTheFly Then pOrderLineCalcs._FilledFromSumOnTheFly = True
    
    For Each pOrderLineCalc As clsOrderLineCalc In Me 
      Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone() 
      pOrderLineCalcs.Add(pOrderLineCalcClone) 
      If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
    Next 
    Return pOrderLineCalcs 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsOrderLineCalcCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    If pFilledFromSumOnTheFly Then pOrderLineCalcs._FilledFromSumOnTheFly = True
    
    For Each pOrderLineCalc As clsOrderLineCalc In Me
      Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
      pOrderLineCalcs.Add(pOrderLineCalcClone)
      If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
    Next
    Return pOrderLineCalcs
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OrderHeaderID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOrderHeaderID(ByVal vOrderHeaderIDFrom As Long, ByVal vOrderHeaderIDTo As Long) As clsOrderLineCalcCol 
    Dim pOrderLineCalcs As New clsOrderLineCalcCol()  
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderLineCalc.OrderHeaderID > vOrderHeaderIDFrom AndAlso pOrderLineCalc.OrderHeaderID <= vOrderHeaderIDTo) Then 
        Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone() 
        pOrderLineCalcs.Add(pOrderLineCalcClone) 
        If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
      End If 
    Next 
    Return pOrderLineCalcs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ProductID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedProductID(ByVal vProductIDFrom As Long, ByVal vProductIDTo As Long) As clsOrderLineCalcCol 
    Dim pOrderLineCalcs As New clsOrderLineCalcCol()  
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderLineCalc.ProductID > vProductIDFrom AndAlso pOrderLineCalc.ProductID <= vProductIDTo) Then 
        Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone() 
        pOrderLineCalcs.Add(pOrderLineCalcClone) 
        If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
      End If 
    Next 
    Return pOrderLineCalcs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As clsOrderLineCalcCol 
    Dim pOrderLineCalcs As New clsOrderLineCalcCol()  
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderLineCalc.ID > vIDFrom AndAlso pOrderLineCalc.ID <= vIDTo) Then 
        Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone() 
        pOrderLineCalcs.Add(pOrderLineCalcClone) 
        If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
      End If 
    Next 
    Return pOrderLineCalcs 
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
  Public Function FindByID(ByVal vID As Long) As clsOrderLineCalc
    If Me.Count = 0 Then Return New clsOrderLineCalc 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    Dim pOrderLineCalc As clsOrderLineCalc = Nothing 
    Dim pFound As Boolean = _SortedDictionaryForFindByID.TryGetValue(vID, pOrderLineCalc) 
    If pFound = True Then Return pOrderLineCalc Else Return New clsOrderLineCalc() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderHeaderID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderHeaderID(ByVal vOrderHeaderID As Long) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.OrderHeaderID = vOrderHeaderID Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.OrderHeaderID = vOrderHeaderID Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductID(ByVal vProductID As Long) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.ProductID = vProductID Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.ProductID = vProductID Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductName(ByVal vProductName As String) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vProductName = vProductName.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.ProductName.ToLowerInvariant() = vProductName Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.ProductName.ToLowerInvariant() = vProductName Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductCode(ByVal vProductCode As String) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vProductCode = vProductCode.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.ProductCode.ToLowerInvariant() = vProductCode Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.ProductCode.ToLowerInvariant() = vProductCode Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Quantity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByQuantity(ByVal vQuantity As Integer) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.Quantity = vQuantity Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.Quantity = vQuantity Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UnitPrice
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUnitPrice(ByVal vUnitPrice As Decimal) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.UnitPrice = vUnitPrice Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.UnitPrice = vUnitPrice Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UnitCost
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUnitCost(ByVal vUnitCost As Decimal) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.UnitCost = vUnitCost Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.UnitCost = vUnitCost Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LineTotal
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLineTotal(ByVal vLineTotal As Decimal) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.LineTotal = vLineTotal Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.LineTotal = vLineTotal Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TotalCost
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTotalCost(ByVal vTotalCost As Decimal) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.TotalCost = vTotalCost Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.TotalCost = vTotalCost Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined GrossProfit
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByGrossProfit(ByVal vGrossProfit As Decimal) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.GrossProfit = vGrossProfit Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.GrossProfit = vGrossProfit Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProfitMarginPercent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProfitMarginPercent(ByVal vProfitMarginPercent As Decimal) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.ProfitMarginPercent = vProfitMarginPercent Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.ProfitMarginPercent = vProfitMarginPercent Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LineNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLineNumber(ByVal vLineNumber As Integer) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.LineNumber = vLineNumber Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.LineNumber = vLineNumber Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As clsOrderLineCalcCol
    Dim pOrderLineCalcs As New clsOrderLineCalcCol() 
    pOrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vTag = vTag.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineCalc As clsOrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineCalc.Tag.ToLowerInvariant() = vTag Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineCalcCol = Me.Clone() 
      For Each pOrderLineCalc As clsOrderLineCalc In pList 
        If pOrderLineCalc.Tag.ToLowerInvariant() = vTag Then
          Dim pOrderLineCalcClone As clsOrderLineCalc = pOrderLineCalc.Clone()
          pOrderLineCalcs.Add(pOrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineCalcs._Clean.Add(pOrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineCalcs
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
    For Each pOrderLineCalc As clsOrderLineCalc In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pOrderLineCalc.LoadDataRow(pRow, vRequester) 
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
    Me.Sort(New clsOrderLineCalcCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
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
    Me.Sort(New clsOrderLineCalcCol.CompareByOrderHeaderID)
  End Sub
  Private Class CompareByOrderHeaderID
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
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
  
  Public Sub SortByProductID()
    Me.Sort(New clsOrderLineCalcCol.CompareByProductID)
  End Sub
  Private Class CompareByProductID
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ProductID < y.ProductID Then
        Return -1
      ElseIf x.ProductID = y.ProductID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByProductName()
    Me.Sort(New clsOrderLineCalcCol.CompareByProductName)
  End Sub
  Private Class CompareByProductName
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProductName, y.ProductName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProductCode()
    Me.Sort(New clsOrderLineCalcCol.CompareByProductCode)
  End Sub
  Private Class CompareByProductCode
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProductCode, y.ProductCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByQuantity()
    Me.Sort(New clsOrderLineCalcCol.CompareByQuantity)
  End Sub
  Private Class CompareByQuantity
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.Quantity < y.Quantity Then
        Return -1
      ElseIf x.Quantity = y.Quantity Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUnitPrice()
    Me.Sort(New clsOrderLineCalcCol.CompareByUnitPrice)
  End Sub
  Private Class CompareByUnitPrice
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.UnitPrice < y.UnitPrice Then
        Return -1
      ElseIf x.UnitPrice = y.UnitPrice Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUnitCost()
    Me.Sort(New clsOrderLineCalcCol.CompareByUnitCost)
  End Sub
  Private Class CompareByUnitCost
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.UnitCost < y.UnitCost Then
        Return -1
      ElseIf x.UnitCost = y.UnitCost Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLineTotal()
    Me.Sort(New clsOrderLineCalcCol.CompareByLineTotal)
  End Sub
  Private Class CompareByLineTotal
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LineTotal < y.LineTotal Then
        Return -1
      ElseIf x.LineTotal = y.LineTotal Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTotalCost()
    Me.Sort(New clsOrderLineCalcCol.CompareByTotalCost)
  End Sub
  Private Class CompareByTotalCost
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
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
  
  Public Sub SortByGrossProfit()
    Me.Sort(New clsOrderLineCalcCol.CompareByGrossProfit)
  End Sub
  Private Class CompareByGrossProfit
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.GrossProfit < y.GrossProfit Then
        Return -1
      ElseIf x.GrossProfit = y.GrossProfit Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByProfitMarginPercent()
    Me.Sort(New clsOrderLineCalcCol.CompareByProfitMarginPercent)
  End Sub
  Private Class CompareByProfitMarginPercent
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ProfitMarginPercent < y.ProfitMarginPercent Then
        Return -1
      ElseIf x.ProfitMarginPercent = y.ProfitMarginPercent Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLineNumber()
    Me.Sort(New clsOrderLineCalcCol.CompareByLineNumber)
  End Sub
  Private Class CompareByLineNumber
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LineNumber < y.LineNumber Then
        Return -1
      ElseIf x.LineNumber = y.LineNumber Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New clsOrderLineCalcCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsOrderLineCalc)
    Private Function Compare(ByVal x As clsOrderLineCalc, ByVal y As clsOrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineCalc).Compare
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
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsOrderLineCalc) 
    _RecreateDictionaryForFindByID = False 
    _LockForFindByID = New Object 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsOrderLineCalc) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = True 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
