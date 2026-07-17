Public Class clsOrderLineFull
  Inherits cTargCCEntity 
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
      Return True 
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
  
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [OrderHeaderID] 
    [ProductID] 
    [Quantity] 
    [UnitPrice] 
    [UnitCost] 
    [DiscountPercent] 
    [LineNumber] 
    [ProductName] 
    [ProductCode] 
    [LineTotal] 
    [TotalCost] 
    [GrossProfit] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [OrderHeaderID] 
    [ProductID] 
    [Quantity] 
    [UnitPrice] 
    [UnitCost] 
    [DiscountPercent] 
    [LineNumber] 
    [LineTotal] 
    [TotalCost] 
    [GrossProfit] 
  End Enum 
  
  Private _IsCleanForXML As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
  
  
  Private _ID As Long
  Private _OrderHeaderID As Long
  Private _ProductID As Long
  Private _Quantity As Integer
  Private _UnitPrice As Decimal
  Private _UnitCost As Decimal
  Private _DiscountPercent As Decimal
  Private _LineNumber As Integer
  Private _ProductName As String
  Private _ProductCode As String
  Private _LineTotal As Decimal
  Private _TotalCost As Decimal
  Private _GrossProfit As Decimal
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
  Public Property [UnitCost]() As Decimal
    Get
      Return Me._UnitCost
    End Get
    Set(ByVal value As Decimal)
      If Me._UnitCost <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UnitCost = value 
      End If 
    End Set
  End Property
  Public Property [DiscountPercent]() As Decimal
    Get
      Return Me._DiscountPercent
    End Get
    Set(ByVal value As Decimal)
      If Me._DiscountPercent <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DiscountPercent = value 
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
    If _Quantity <> 0 Then pValue.Append("Quantity='" & _Quantity.ToString() & "' ‡ ") 
    If _UnitPrice <> 0 Then pValue.Append("UnitPrice='" & _UnitPrice.ToString() & "' ‡ ") 
    If _UnitCost <> 0 Then pValue.Append("UnitCost='" & _UnitCost.ToString() & "' ‡ ") 
    If _DiscountPercent <> 0 Then pValue.Append("DiscountPercent='" & _DiscountPercent.ToString() & "' ‡ ") 
    If _LineNumber <> 0 Then pValue.Append("LineNumber='" & _LineNumber.ToString() & "' ‡ ") 
    If _ProductName <> "" Then pValue.Append("ProductName='" & _ProductName & "' ‡ ") 
    If _ProductCode <> "" Then pValue.Append("ProductCode='" & _ProductCode & "' ‡ ") 
    If _LineTotal <> 0 Then pValue.Append("LineTotal='" & _LineTotal.ToString() & "' ‡ ") 
    If _TotalCost <> 0 Then pValue.Append("TotalCost='" & _TotalCost.ToString() & "' ‡ ") 
    If _GrossProfit <> 0 Then pValue.Append("GrossProfit='" & _GrossProfit.ToString() & "' ‡ ") 
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
    pCSV.Append("," & _Quantity.ToString() & "") 
    pCSV.Append("," & _UnitPrice.ToString() & "") 
    pCSV.Append("," & _UnitCost.ToString() & "") 
    pCSV.Append("," & _DiscountPercent.ToString() & "") 
    pCSV.Append("," & _LineNumber.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProductName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProductCode)}""") 
    pCSV.Append("," & _LineTotal.ToString() & "") 
    pCSV.Append("," & _TotalCost.ToString() & "") 
    pCSV.Append("," & _GrossProfit.ToString() & "") 
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
  
  Public Sub New(ByVal vclsOrderLineFull As clsOrderLineFull)
    MyBase.New()
    CreateEmpty()
    AssignValues(vclsOrderLineFull) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vOrderHeaderID As Long = 0 _ 
    , Optional vProductID As Long = 0 _ 
    , Optional vQuantity As Integer = 0 _ 
    , Optional vUnitPrice As Decimal = 0 _ 
    , Optional vUnitCost As Decimal = 0 _ 
    , Optional vDiscountPercent As Decimal = 0 _ 
    , Optional vLineNumber As Integer = 0 _ 
    , Optional vProductName As String = "" _ 
    , Optional vProductCode As String = "" _ 
    , Optional vLineTotal As Decimal = 0 _ 
    , Optional vTotalCost As Decimal = 0 _ 
    , Optional vGrossProfit As Decimal = 0 _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _OrderHeaderID = vOrderHeaderID 
    _ProductID = vProductID 
    _Quantity = vQuantity 
    _UnitPrice = vUnitPrice 
    _UnitCost = vUnitCost 
    _DiscountPercent = vDiscountPercent 
    _LineNumber = vLineNumber 
    _ProductName = vProductName 
    _ProductCode = vProductCode 
    _LineTotal = vLineTotal 
    _TotalCost = vTotalCost 
    _GrossProfit = vGrossProfit 
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
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _ProductName = ccHelper.RemoveChrW0(_ProductName) 
    _ProductCode = ccHelper.RemoveChrW0(_ProductCode) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the OrderLineFull by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFull_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderLineFull-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the OrderLineFull by the chosen parameters. This function may be a bit slower than accessing the OrderLineFull's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFull_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-OrderLineFull-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderLineFull-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the OrderLineFull by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFull_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = -1 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"OrderLineFull not found for GetByID, since its value is -1", pFunctionParameters, "TRGT-OrderLineFull-210927-1527", vRequester, vAdditionalMessageToUser:=$"OrderLineFull not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderLineFullCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderLineFullCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderLineFullCol failed: " & pResponse) 
      ' Not Implemented Yet!!  pFault = LoadMeFromDBCache(MyController.DBCache.OrderLineFullCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"OrderLineFull not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-OrderLineFull-210625-0950", vRequester, vAdditionalMessageToUser:=$"OrderLineFull not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
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
    If Not (TypeOf (vTargCCEntityToTest) Is clsOrderLineFull) Then Return False 
    Dim pOrderLineFullToTest As clsOrderLineFull = CType(vTargCCEntityToTest, clsOrderLineFull) 
    Return isEqual(pOrderLineFullToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vOrderLineFullToTest As clsOrderLineFull) As Boolean
    With vOrderLineFullToTest
      If _ID <> .ID Then Return False
      If _OrderHeaderID <> .OrderHeaderID Then Return False
      If _ProductID <> .ProductID Then Return False
      If _Quantity <> .Quantity Then Return False
      If _UnitPrice <> .UnitPrice Then Return False
      If _UnitCost <> .UnitCost Then Return False
      If _DiscountPercent <> .DiscountPercent Then Return False
      If _LineNumber <> .LineNumber Then Return False
      If _ProductName <> .ProductName Then Return False
      If _ProductCode <> .ProductCode Then Return False
      If _LineTotal <> .LineTotal Then Return False
      If _TotalCost <> .TotalCost Then Return False
      If _GrossProfit <> .GrossProfit Then Return False
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
    Dim pClone As New clsOrderLineFull(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsOrderLineFull
    Dim pClone As New clsOrderLineFull(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderHeaderID") = _OrderHeaderID : Catch ex As Exception : Return pFault.LogException(ex, "OrderHeaderID", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductID") = _ProductID : Catch ex As Exception : Return pFault.LogException(ex, "ProductID", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("Quantity") = _Quantity : Catch ex As Exception : Return pFault.LogException(ex, "Quantity", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("UnitPrice") = _UnitPrice : Catch ex As Exception : Return pFault.LogException(ex, "UnitPrice", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("UnitCost") = _UnitCost : Catch ex As Exception : Return pFault.LogException(ex, "UnitCost", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("DiscountPercent") = _DiscountPercent : Catch ex As Exception : Return pFault.LogException(ex, "DiscountPercent", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("LineNumber") = _LineNumber : Catch ex As Exception : Return pFault.LogException(ex, "LineNumber", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductName") = _ProductName : Catch ex As Exception : Return pFault.LogException(ex, "ProductName", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductCode") = _ProductCode : Catch ex As Exception : Return pFault.LogException(ex, "ProductCode", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("LineTotal") = _LineTotal : Catch ex As Exception : Return pFault.LogException(ex, "LineTotal", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("TotalCost") = _TotalCost : Catch ex As Exception : Return pFault.LogException(ex, "TotalCost", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
    Try : vDataRow("GrossProfit") = _GrossProfit : Catch ex As Exception : Return pFault.LogException(ex, "GrossProfit", "TRGT-OrderLineFull-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pOrderLineFull As clsOrderLineFull = CType(pXmlSerializer.Deserialize(pStreamReader), clsOrderLineFull) 
      AssignValues(pOrderLineFull) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-OrderLineFull-130515-1230", vRequester) 
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
          'Quantity 
          pBinaryWriter.Write(_Quantity) 
          'UnitPrice 
          pBinaryWriter.Write(_UnitPrice) 
          'UnitCost 
          pBinaryWriter.Write(_UnitCost) 
          'DiscountPercent 
          pBinaryWriter.Write(_DiscountPercent) 
          'LineNumber 
          pBinaryWriter.Write(_LineNumber) 
          'ProductName 
          If _ProductName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProductName) 
          'ProductCode 
          If _ProductCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProductCode) 
          'LineTotal 
          pBinaryWriter.Write(_LineTotal) 
          'TotalCost 
          pBinaryWriter.Write(_TotalCost) 
          'GrossProfit 
          pBinaryWriter.Write(_GrossProfit) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-150307-2338", vRequester) 
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
          'Quantity 
          _Quantity = pReader.ReadInt32 
          'UnitPrice 
          _UnitPrice = pReader.ReadDecimal 
          'UnitCost 
          _UnitCost = pReader.ReadDecimal 
          'DiscountPercent 
          _DiscountPercent = pReader.ReadDecimal 
          'LineNumber 
          _LineNumber = pReader.ReadInt32 
          'ProductName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProductName = pReader.ReadString 
          'ProductCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProductCode = pReader.ReadString 
          'LineTotal 
          _LineTotal = pReader.ReadDecimal 
          'TotalCost 
          _TotalCost = pReader.ReadDecimal 
          'GrossProfit 
          _GrossProfit = pReader.ReadDecimal 
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
      rFault.LogException(ex, "", "TRGT-OrderLineFull-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-190720-1443", vRequester) 
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
 
      Dim pOrderLineFull As clsOrderLineFull = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsOrderLineFull)(vJSON, pSettings) 
      AssignValues(pOrderLineFull) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vOrderLineFull As clsOrderLineFull)
    With vOrderLineFull
      _ID = .ID 
      _OrderHeaderID = .OrderHeaderID 
      _ProductID = .ProductID 
      _Quantity = .Quantity 
      _UnitPrice = .UnitPrice 
      _UnitCost = .UnitCost 
      _DiscountPercent = .DiscountPercent 
      _LineNumber = .LineNumber 
      _ProductName = .ProductName 
      _ProductCode = .ProductCode 
      _LineTotal = .LineTotal 
      _TotalCost = .TotalCost 
      _GrossProfit = .GrossProfit 
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
      pLastReadVariableName = "ProductID" 
      If Not vReader.IsDBNull(2) Then _ProductID = vReader.GetInt64(2)
      pLastReadVariableName = "Quantity" 
      If Not vReader.IsDBNull(3) Then _Quantity = vReader.GetInt32(3)
      pLastReadVariableName = "UnitPrice" 
      If Not vReader.IsDBNull(4) Then _UnitPrice = vReader.GetDecimal(4)
      pLastReadVariableName = "blg_UnitCost" 
      If Not vReader.IsDBNull(5) Then _UnitCost = vReader.GetDecimal(5)
      pLastReadVariableName = "DiscountPercent" 
      If Not vReader.IsDBNull(6) Then _DiscountPercent = vReader.GetDecimal(6)
      pLastReadVariableName = "LineNumber" 
      If Not vReader.IsDBNull(7) Then _LineNumber = vReader.GetInt32(7)
      pLastReadVariableName = "ProductName" 
      If Not vReader.IsDBNull(8) Then _ProductName = vReader.GetString(8) 
      pLastReadVariableName = "ProductCode" 
      If Not vReader.IsDBNull(9) Then _ProductCode = vReader.GetString(9) 
      pLastReadVariableName = "LineTotal" 
      If Not vReader.IsDBNull(10) Then _LineTotal = vReader.GetDecimal(10)
      pLastReadVariableName = "TotalCost" 
      If Not vReader.IsDBNull(11) Then _TotalCost = vReader.GetDecimal(11)
      pLastReadVariableName = "GrossProfit" 
      If Not vReader.IsDBNull(12) Then _GrossProfit = vReader.GetDecimal(12)
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedOrderLineFull As clsOrderLineFull, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedOrderLineFull) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = -1 
    _OrderHeaderID = 0
    _ProductID = 0
    _Quantity = 0
    _UnitPrice = 0
    _UnitCost = 0
    _DiscountPercent = 0
    _LineNumber = 0
    _ProductName = ""
    _ProductCode = ""
    _LineTotal = 0
    _TotalCost = 0
    _GrossProfit = 0
    _Tag = ""
    _IsCleanForXML = False 
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
  
Public Class clsOrderLineFullCol
  Inherits cTargCCCollection(Of clsOrderLineFull)
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
      Return True 
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
  
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsOrderLineFull) 
  Private _LockForFindByID As New Object 
  Private _RecreateDictionaryForFindByID As Boolean 
   
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
 
    For Each pRow As clsOrderLineFull In Me 
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
    pCSVTitle.Append(",""Quantity""") 
    pCSVTitle.Append(",""UnitPrice""") 
    pCSVTitle.Append(",""UnitCost""") 
    pCSVTitle.Append(",""DiscountPercent""") 
    pCSVTitle.Append(",""LineNumber""") 
    pCSVTitle.Append(",""ProductName""") 
    pCSVTitle.Append(",""ProductCode""") 
    pCSVTitle.Append(",""LineTotal""") 
    pCSVTitle.Append(",""TotalCost""") 
    pCSVTitle.Append(",""GrossProfit""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As clsOrderLineFull In Me 
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
 
  Public Overloads Sub Add(ByVal vOrderLineFull As clsOrderLineFull) 
    MyBase.Add(vOrderLineFull) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vOrderLineFull As clsOrderLineFull) 
    MyBase.Insert(vIndex, vOrderLineFull) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub AddRange(ByVal vOrderLineFullCol As clsOrderLineFullCol) 
    MyBase.AddRange(vOrderLineFullCol) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    MyBase.RemoveAt(vIndex) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub Remove(ByVal vOrderLineFull As clsOrderLineFull) 
    MyBase.Remove(vOrderLineFull) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
 
  Private Sub LoadIDs() 
    If _RecreateDictionaryForFindByID = True Then 
      SyncLock _LockForFindByID 
        If _RecreateDictionaryForFindByID = True Then 
          _SortedDictionaryForFindByID = New Dictionary(Of Long, clsOrderLineFull) 
          If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByID = False : Exit Sub 'Not logical 
          For Each lOrderLineFull In Me 
            If lOrderLineFull.IsEmpty OrElse _SortedDictionaryForFindByID.ContainsKey(lOrderLineFull.ID) Then 
              'Not Unique or no ID 
              Continue For 
            End If 
            Try 
              _SortedDictionaryForFindByID.Add(lOrderLineFull.ID, lOrderLineFull) 
            Catch ex As Exception 
              Dim pFault As New clsFault 
              pFault.LogException(ex, lOrderLineFull.ToString, "TRGT-OrderLineFull-190412-1939", Nothing) 'Log it 
              Throw New Exception("Failed _SortedDictionaryForFindByID:" & ex.Message & ", OrderLineFull:" & lOrderLineFull.ToString() & ", TRGT-OrderLineFull-190412-1939") 'Send it up the line 
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
 
    For Each lOrderLineFull As clsOrderLineFull In Me 
      lOrderLineFull.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lOrderLineFull As clsOrderLineFull In Me 
      lOrderLineFull.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [OrderHeaderID] 
    [ProductID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the OrderLineFulls by the chosen parameters. This function may be a bit slower than accessing the OrderLineFull's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
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
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-OrderLineFull-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-OrderLineFull-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderLineFullCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderLineFullCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderLineFullCol failed: " & pResponse) 
      Dim pOrderLineFullsCached As clsOrderLineFullCol = MyController.DBCache.OrderLineFullCol.Clone() 
      pOrderLineFullsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pOrderLineFullsCached.Reverse() 
      If vHowMany > 0 AndAlso pOrderLineFullsCached.Count > vHowMany Then 
        Dim tmp As New clsOrderLineFullCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pOrderLineFullsCached(i)) 
        Next 
        pOrderLineFullsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pOrderLineFullsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-090624-1625", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillByOrderHeaderID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderLineFullCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderLineFullCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderLineFullCol failed: " & pResponse) 
      Dim pOrderLineFullsCached As clsOrderLineFullCol = MyController.DBCache.OrderLineFullCol.CloneByOrderHeaderID(vOrderHeaderID)
      pFault = LoadMeFromDBCache(pOrderLineFullsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullsFillByOrderHeaderID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vOrderHeaderID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ProductID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByProductID(ByVal vProductID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ProductID={0}", vProductID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillByProductID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderLineFullCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderLineFullCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderLineFullCol failed: " & pResponse) 
      Dim pOrderLineFullsCached As clsOrderLineFullCol = MyController.DBCache.OrderLineFullCol.CloneByProductID(vProductID)
      pFault = LoadMeFromDBCache(pOrderLineFullsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullsFillByProductID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ProductID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vProductID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-090624-1702", vRequester) 
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
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedOrderHeaderID(ByVal vOrderHeaderIDFrom As Long, ByVal vOrderHeaderIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OrderHeaderIDFrom={0}, OrderHeaderIDTo={1}", vOrderHeaderIDFrom, vOrderHeaderIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillByBoundedOrderHeaderID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderLineFullCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderLineFullCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderLineFullCol failed: " & pResponse) 
      Dim pOrderLineFullsCached As clsOrderLineFullCol = MyController.DBCache.OrderLineFullCol.CloneByBoundedOrderHeaderID(vOrderHeaderIDFrom, vOrderHeaderIDTo)
      pFault = LoadMeFromDBCache(pOrderLineFullsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullsFillByBoundedOrderHeaderID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OrderHeaderIDFrom" 
        pDALParameters.Add("bndOrderHeaderIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vOrderHeaderIDFrom) 
        pLastReadVariableName = "OrderHeaderIDTo" 
        pDALParameters.Add("bndOrderHeaderIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vOrderHeaderIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillByBoundedProductID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderLineFullCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderLineFullCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderLineFullCol failed: " & pResponse) 
      Dim pOrderLineFullsCached As clsOrderLineFullCol = MyController.DBCache.OrderLineFullCol.CloneByBoundedProductID(vProductIDFrom, vProductIDTo)
      pFault = LoadMeFromDBCache(pOrderLineFullsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullsFillByBoundedProductID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ProductIDFrom" 
        pDALParameters.Add("bndProductIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vProductIDFrom) 
        pLastReadVariableName = "ProductIDTo" 
        pDALParameters.Add("bndProductIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vProductIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.OrderLineFullCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.OrderLineFullCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsOrderLineFullCol failed: " & pResponse) 
      Dim pOrderLineFullsCached As clsOrderLineFullCol = MyController.DBCache.OrderLineFullCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pOrderLineFullsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lOrderLineFull As New clsOrderLineFull() 
      pFault = lOrderLineFull.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lOrderLineFull.IsEmpty Then Me.Add(lOrderLineFull) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pOrderLineFulls As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pOrderLineFulls, "clsOrderLineFullCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pOrderLineFulls IsNot Nothing AndAlso Me.Count <> pOrderLineFulls.Count Then FillFromListOfITargCCEntity(pOrderLineFulls) 
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-OrderLineFull-121122-2008", vRequester) 
      Dim pOrderLineFullsCached As clsOrderLineFullCol = MyController.DBCache.OrderLineFullCol.Clone() 
      Dim pOrderLineFullsToUse As New clsOrderLineFullCol() 
      For Each l In pOrderLineFullsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vOrderHeaderIDFrom.HasValue Then 
          If vOrderHeaderIDTo.HasValue Then 
            If l.OrderHeaderID < vOrderHeaderIDFrom OrElse l.OrderHeaderID > vOrderHeaderIDTo.Value Then Continue For 
          Else 
            If l.OrderHeaderID <> vOrderHeaderIDFrom.Value Then Continue For 
          End If 
        End If 
        If vProductIDFrom.HasValue Then 
          If vProductIDTo.HasValue Then 
            If l.ProductID < vProductIDFrom OrElse l.ProductID > vProductIDTo.Value Then Continue For 
          Else 
            If l.ProductID <> vProductIDFrom.Value Then Continue For 
          End If 
        End If 
        pOrderLineFullsToUse.Add(l) 
      Next 
      pOrderLineFullsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pOrderLineFullsToUse.Reverse() 
      If vHowMany > 0 AndAlso pOrderLineFullsToUse.Count > vHowMany Then 
        Dim tmp As New clsOrderLineFullCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pOrderLineFullsToUse(i)) 
        Next 
        pOrderLineFullsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pOrderLineFullsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "OrderHeaderIDFrom" 
        pDALParameters.Add("bndOrderHeaderIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOrderHeaderIDFrom) 
        pLastReadVariableName = "OrderHeaderIDTo" 
        pDALParameters.Add("bndOrderHeaderIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOrderHeaderIDTo) 
        pLastReadVariableName = "ProductIDFrom" 
        pDALParameters.Add("bndProductIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vProductIDFrom) 
        pLastReadVariableName = "ProductIDTo" 
        pDALParameters.Add("bndProductIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vProductIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-090303-1658", vRequester) 
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vwOrderLineFullView, "clsOrderLineFullCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-OrderLineFull-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccOrderLineFullsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "OrderHeaderIDFrom" 
        pDALParameters.Add("bndOrderHeaderIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOrderHeaderIDFrom) 
        pLastReadVariableName = "OrderHeaderIDTo" 
        pDALParameters.Add("bndOrderHeaderIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOrderHeaderIDTo) 
        pLastReadVariableName = "ProductIDFrom" 
        pDALParameters.Add("bndProductIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vProductIDFrom) 
        pLastReadVariableName = "ProductIDTo" 
        pDALParameters.Add("bndProductIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vProductIDTo) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add("GroupByOrderHeaderID", ccDAL.enmSQLDataType.Bit).Value = vGroupByOrderHeaderID
        pLastReadVariableName = "ProductID" 
        pDALParameters.Add("GroupByProductID", ccDAL.enmSQLDataType.Bit).Value = vGroupByProductID
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vOrderLineFullArray As clsOrderLineFull())
    Me.Clear()
    
    For Each pOrderLineFull As clsOrderLineFull In vOrderLineFullArray
      Me.Add(pOrderLineFull)
      _Clean.Add(pOrderLineFull.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pOrderLineFull As New clsOrderLineFull(pRow, vRequester) 
        Me.Add(pOrderLineFull) 
        _Clean.Add(pOrderLineFull.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-OrderLineFullCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-130515-1300", vRequester) 
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
      Dim pOrderLineFulls As clsOrderLineFullCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsOrderLineFullCol) 
      For Each pOrderLineFull As clsOrderLineFull In pOrderLineFulls 
        Me.Add(pOrderLineFull) 
        _Clean.Add(pOrderLineFull.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-OrderLineFull-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-190720-1443", vRequester) 
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
 
      Dim pOrderLineFulls As List(Of clsOrderLineFull) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of clsOrderLineFull))(vJSON, pSettings) 
      For Each pOrderLineFull As clsOrderLineFull In pOrderLineFulls 
        Me.Add(pOrderLineFull) 
        _Clean.Add(pOrderLineFull.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-190720-2059", vRequester) 
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
          For Each lOrderLineFull As clsOrderLineFull In Me 
            Dim pByte As Byte() = lOrderLineFull.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-150307-2340", vRequester) 
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
            Dim pOrderLineFull As clsOrderLineFull = New clsOrderLineFull(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pOrderLineFull) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pOrderLineFull.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-OrderLineFull-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pOrderLineFull As clsOrderLineFull In Me 
      With pOrderLineFull 
        pFault = pOrderLineFull.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is clsOrderLineFullCol) Then Return False 
    Dim pOrderLineFullColToTest As clsOrderLineFullCol = CType(vEntitiesToTest, clsOrderLineFullCol) 
    Return isEqual(pOrderLineFullColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vOrderLineFullsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vOrderLineFullsToTest As clsOrderLineFullCol) As Boolean
    If Me.Count <> vOrderLineFullsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vOrderLineFullsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    If pFilledFromSumOnTheFly Then pOrderLineFulls._FilledFromSumOnTheFly = True
    
    For Each pOrderLineFull As clsOrderLineFull In Me 
      Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone() 
      pOrderLineFulls.Add(pOrderLineFullClone) 
      If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
    Next 
    Return pOrderLineFulls 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsOrderLineFullCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    If pFilledFromSumOnTheFly Then pOrderLineFulls._FilledFromSumOnTheFly = True
    
    For Each pOrderLineFull As clsOrderLineFull In Me
      Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
      pOrderLineFulls.Add(pOrderLineFullClone)
      If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
    Next
    Return pOrderLineFulls
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OrderHeaderID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOrderHeaderID(ByVal vOrderHeaderIDFrom As Long, ByVal vOrderHeaderIDTo As Long) As clsOrderLineFullCol 
    Dim pOrderLineFulls As New clsOrderLineFullCol()  
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderLineFull.OrderHeaderID > vOrderHeaderIDFrom AndAlso pOrderLineFull.OrderHeaderID <= vOrderHeaderIDTo) Then 
        Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone() 
        pOrderLineFulls.Add(pOrderLineFullClone) 
        If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
      End If 
    Next 
    Return pOrderLineFulls 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ProductID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedProductID(ByVal vProductIDFrom As Long, ByVal vProductIDTo As Long) As clsOrderLineFullCol 
    Dim pOrderLineFulls As New clsOrderLineFullCol()  
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderLineFull.ProductID > vProductIDFrom AndAlso pOrderLineFull.ProductID <= vProductIDTo) Then 
        Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone() 
        pOrderLineFulls.Add(pOrderLineFullClone) 
        If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
      End If 
    Next 
    Return pOrderLineFulls 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As clsOrderLineFullCol 
    Dim pOrderLineFulls As New clsOrderLineFullCol()  
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList() 
      If (pOrderLineFull.ID > vIDFrom AndAlso pOrderLineFull.ID <= vIDTo) Then 
        Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone() 
        pOrderLineFulls.Add(pOrderLineFullClone) 
        If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
      End If 
    Next 
    Return pOrderLineFulls 
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
  Public Function FindByID(ByVal vID As Long) As clsOrderLineFull
    If Me.Count = 0 Then Return New clsOrderLineFull 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    Dim pOrderLineFull As clsOrderLineFull = Nothing 
    Dim pFound As Boolean = _SortedDictionaryForFindByID.TryGetValue(vID, pOrderLineFull) 
    If pFound = True Then Return pOrderLineFull Else Return New clsOrderLineFull() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderHeaderID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderHeaderID(ByVal vOrderHeaderID As Long) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.OrderHeaderID = vOrderHeaderID Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.OrderHeaderID = vOrderHeaderID Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductID(ByVal vProductID As Long) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.ProductID = vProductID Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.ProductID = vProductID Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Quantity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByQuantity(ByVal vQuantity As Integer) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.Quantity = vQuantity Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.Quantity = vQuantity Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UnitPrice
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUnitPrice(ByVal vUnitPrice As Decimal) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.UnitPrice = vUnitPrice Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.UnitPrice = vUnitPrice Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UnitCost
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUnitCost(ByVal vUnitCost As Decimal) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.UnitCost = vUnitCost Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.UnitCost = vUnitCost Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DiscountPercent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDiscountPercent(ByVal vDiscountPercent As Decimal) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.DiscountPercent = vDiscountPercent Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.DiscountPercent = vDiscountPercent Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LineNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLineNumber(ByVal vLineNumber As Integer) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.LineNumber = vLineNumber Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.LineNumber = vLineNumber Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductName(ByVal vProductName As String) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vProductName = vProductName.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.ProductName.ToLowerInvariant() = vProductName Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.ProductName.ToLowerInvariant() = vProductName Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductCode(ByVal vProductCode As String) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vProductCode = vProductCode.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.ProductCode.ToLowerInvariant() = vProductCode Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.ProductCode.ToLowerInvariant() = vProductCode Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LineTotal
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLineTotal(ByVal vLineTotal As Decimal) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.LineTotal = vLineTotal Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.LineTotal = vLineTotal Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TotalCost
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTotalCost(ByVal vTotalCost As Decimal) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.TotalCost = vTotalCost Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.TotalCost = vTotalCost Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined GrossProfit
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByGrossProfit(ByVal vGrossProfit As Decimal) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.GrossProfit = vGrossProfit Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.GrossProfit = vGrossProfit Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As clsOrderLineFullCol
    Dim pOrderLineFulls As New clsOrderLineFullCol() 
    pOrderLineFulls._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vTag = vTag.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pOrderLineFull As clsOrderLineFull In _SortedDictionaryForFindByID.Values.ToList()
        If pOrderLineFull.Tag.ToLowerInvariant() = vTag Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As clsOrderLineFullCol = Me.Clone() 
      For Each pOrderLineFull As clsOrderLineFull In pList 
        If pOrderLineFull.Tag.ToLowerInvariant() = vTag Then
          Dim pOrderLineFullClone As clsOrderLineFull = pOrderLineFull.Clone()
          pOrderLineFulls.Add(pOrderLineFullClone)
          If Not _FilledFromSumOnTheFly Then pOrderLineFulls._Clean.Add(pOrderLineFull.ID) 
        End If
      Next
    End If 
    
    Return pOrderLineFulls
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
    For Each pOrderLineFull As clsOrderLineFull In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pOrderLineFull.LoadDataRow(pRow, vRequester) 
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
    Me.Sort(New clsOrderLineFullCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
    Me.Sort(New clsOrderLineFullCol.CompareByOrderHeaderID)
  End Sub
  Private Class CompareByOrderHeaderID
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
    Me.Sort(New clsOrderLineFullCol.CompareByProductID)
  End Sub
  Private Class CompareByProductID
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
  
  Public Sub SortByQuantity()
    Me.Sort(New clsOrderLineFullCol.CompareByQuantity)
  End Sub
  Private Class CompareByQuantity
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
    Me.Sort(New clsOrderLineFullCol.CompareByUnitPrice)
  End Sub
  Private Class CompareByUnitPrice
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
    Me.Sort(New clsOrderLineFullCol.CompareByUnitCost)
  End Sub
  Private Class CompareByUnitCost
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
  
  Public Sub SortByDiscountPercent()
    Me.Sort(New clsOrderLineFullCol.CompareByDiscountPercent)
  End Sub
  Private Class CompareByDiscountPercent
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DiscountPercent < y.DiscountPercent Then
        Return -1
      ElseIf x.DiscountPercent = y.DiscountPercent Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLineNumber()
    Me.Sort(New clsOrderLineFullCol.CompareByLineNumber)
  End Sub
  Private Class CompareByLineNumber
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
  
  Public Sub SortByProductName()
    Me.Sort(New clsOrderLineFullCol.CompareByProductName)
  End Sub
  Private Class CompareByProductName
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProductName, y.ProductName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProductCode()
    Me.Sort(New clsOrderLineFullCol.CompareByProductCode)
  End Sub
  Private Class CompareByProductCode
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProductCode, y.ProductCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLineTotal()
    Me.Sort(New clsOrderLineFullCol.CompareByLineTotal)
  End Sub
  Private Class CompareByLineTotal
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
    Me.Sort(New clsOrderLineFullCol.CompareByTotalCost)
  End Sub
  Private Class CompareByTotalCost
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
    Me.Sort(New clsOrderLineFullCol.CompareByGrossProfit)
  End Sub
  Private Class CompareByGrossProfit
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
  
  Public Sub SortByTag()
    Me.Sort(New clsOrderLineFullCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsOrderLineFull)
    Private Function Compare(ByVal x As clsOrderLineFull, ByVal y As clsOrderLineFull) As Integer Implements System.Collections.Generic.IComparer(Of clsOrderLineFull).Compare
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
  
    Dim pOrderLineFull As clsOrderLineFull
  
    While vReader.Read()
      pOrderLineFull = New clsOrderLineFull() 
      pFault = pOrderLineFull.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pOrderLineFull)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pOrderLineFull.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedOrderLineFullCol As clsOrderLineFullCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pOrderLineFull As clsOrderLineFull 
 
      For Each pCachedOrderLineFull As clsOrderLineFull In vCachedOrderLineFullCol 
        pOrderLineFull = New clsOrderLineFull(pCachedOrderLineFull) 
        pOrderLineFull.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pOrderLineFull) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pOrderLineFull.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-OrderLineFull-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsOrderLineFull) 
    _RecreateDictionaryForFindByID = False 
    _LockForFindByID = New Object 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsOrderLineFull) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = True 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
