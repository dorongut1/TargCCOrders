Public Class cls_OrderLineCalc
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
  
  Public Sub New(ByVal vcls_OrderLineCalc As cls_OrderLineCalc)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcls_OrderLineCalc) 
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
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _ProductName = ccHelper.RemoveChrW0(_ProductName) 
    _ProductCode = ccHelper.RemoveChrW0(_ProductCode) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the _OrderLineCalc by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalc_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-_OrderLineCalc-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the _OrderLineCalc by the chosen parameters. This function may be a bit slower than accessing the _OrderLineCalc's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalc_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-_OrderLineCalc-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-_OrderLineCalc-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the _OrderLineCalc by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalc_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = -1 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"_OrderLineCalc not found for GetByID, since its value is -1", pFunctionParameters, "TRGT-_OrderLineCalc-210927-1527", vRequester, vAdditionalMessageToUser:=$"_OrderLineCalc not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache._OrderLineCalcCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache._OrderLineCalcCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load cls_OrderLineCalcCol failed: " & pResponse) 
      ' Not Implemented Yet!!  pFault = LoadMeFromDBCache(MyController.DBCache._OrderLineCalcCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"_OrderLineCalc not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-_OrderLineCalc-210625-0950", vRequester, vAdditionalMessageToUser:=$"_OrderLineCalc not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-090623-1648", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is cls_OrderLineCalc) Then Return False 
    Dim p_OrderLineCalcToTest As cls_OrderLineCalc = CType(vTargCCEntityToTest, cls_OrderLineCalc) 
    Return isEqual(p_OrderLineCalcToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal v_OrderLineCalcToTest As cls_OrderLineCalc) As Boolean
    With v_OrderLineCalcToTest
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
    Dim pClone As New cls_OrderLineCalc(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As cls_OrderLineCalc
    Dim pClone As New cls_OrderLineCalc(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderHeaderID") = _OrderHeaderID : Catch ex As Exception : Return pFault.LogException(ex, "OrderHeaderID", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductID") = _ProductID : Catch ex As Exception : Return pFault.LogException(ex, "ProductID", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductName") = _ProductName : Catch ex As Exception : Return pFault.LogException(ex, "ProductName", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductCode") = _ProductCode : Catch ex As Exception : Return pFault.LogException(ex, "ProductCode", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("Quantity") = _Quantity : Catch ex As Exception : Return pFault.LogException(ex, "Quantity", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("UnitPrice") = _UnitPrice : Catch ex As Exception : Return pFault.LogException(ex, "UnitPrice", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("UnitCost") = _UnitCost : Catch ex As Exception : Return pFault.LogException(ex, "UnitCost", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("LineTotal") = _LineTotal : Catch ex As Exception : Return pFault.LogException(ex, "LineTotal", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("TotalCost") = _TotalCost : Catch ex As Exception : Return pFault.LogException(ex, "TotalCost", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("GrossProfit") = _GrossProfit : Catch ex As Exception : Return pFault.LogException(ex, "GrossProfit", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProfitMarginPercent") = _ProfitMarginPercent : Catch ex As Exception : Return pFault.LogException(ex, "ProfitMarginPercent", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
    Try : vDataRow("LineNumber") = _LineNumber : Catch ex As Exception : Return pFault.LogException(ex, "LineNumber", "TRGT-_OrderLineCalc-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim p_OrderLineCalc As cls_OrderLineCalc = CType(pXmlSerializer.Deserialize(pStreamReader), cls_OrderLineCalc) 
      AssignValues(p_OrderLineCalc) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-_OrderLineCalc-130515-1230", vRequester) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-150307-2338", vRequester) 
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
      rFault.LogException(ex, "", "TRGT-_OrderLineCalc-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-190720-1443", vRequester) 
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
 
      Dim p_OrderLineCalc As cls_OrderLineCalc = Newtonsoft.Json.JsonConvert.DeserializeObject(Of cls_OrderLineCalc)(vJSON, pSettings) 
      AssignValues(p_OrderLineCalc) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal v_OrderLineCalc As cls_OrderLineCalc)
    With v_OrderLineCalc
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
      pLastReadVariableName = "ProductName" 
      If Not vReader.IsDBNull(3) Then _ProductName = vReader.GetString(3) 
      pLastReadVariableName = "ProductCode" 
      If Not vReader.IsDBNull(4) Then _ProductCode = vReader.GetString(4) 
      pLastReadVariableName = "Quantity" 
      If Not vReader.IsDBNull(5) Then _Quantity = vReader.GetInt32(5)
      pLastReadVariableName = "UnitPrice" 
      If Not vReader.IsDBNull(6) Then _UnitPrice = vReader.GetDecimal(6)
      pLastReadVariableName = "blg_UnitCost" 
      If Not vReader.IsDBNull(7) Then _UnitCost = vReader.GetDecimal(7)
      pLastReadVariableName = "LineTotal" 
      If Not vReader.IsDBNull(8) Then _LineTotal = vReader.GetDecimal(8)
      pLastReadVariableName = "TotalCost" 
      If Not vReader.IsDBNull(9) Then _TotalCost = vReader.GetDecimal(9)
      pLastReadVariableName = "GrossProfit" 
      If Not vReader.IsDBNull(10) Then _GrossProfit = vReader.GetDecimal(10)
      pLastReadVariableName = "ProfitMarginPercent" 
      If Not vReader.IsDBNull(11) Then _ProfitMarginPercent = vReader.GetDecimal(11)
      pLastReadVariableName = "LineNumber" 
      If Not vReader.IsDBNull(12) Then _LineNumber = vReader.GetInt32(12)
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCached_OrderLineCalc As cls_OrderLineCalc, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCached_OrderLineCalc) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
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
  
Public Class cls_OrderLineCalcCol
  Inherits cTargCCCollection(Of cls_OrderLineCalc)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, cls_OrderLineCalc) 
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
 
    For Each pRow As cls_OrderLineCalc In Me 
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
 
    For Each pRow As cls_OrderLineCalc In Me 
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
 
  Public Overloads Sub Add(ByVal v_OrderLineCalc As cls_OrderLineCalc) 
    MyBase.Add(v_OrderLineCalc) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal v_OrderLineCalc As cls_OrderLineCalc) 
    MyBase.Insert(vIndex, v_OrderLineCalc) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub AddRange(ByVal v_OrderLineCalcCol As cls_OrderLineCalcCol) 
    MyBase.AddRange(v_OrderLineCalcCol) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    MyBase.RemoveAt(vIndex) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
  Public Overloads Sub Remove(ByVal v_OrderLineCalc As cls_OrderLineCalc) 
    MyBase.Remove(v_OrderLineCalc) 
    _RecreateDictionaryForFindByID = True 
  End Sub 
 
  Private Sub LoadIDs() 
    If _RecreateDictionaryForFindByID = True Then 
      SyncLock _LockForFindByID 
        If _RecreateDictionaryForFindByID = True Then 
          _SortedDictionaryForFindByID = New Dictionary(Of Long, cls_OrderLineCalc) 
          If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByID = False : Exit Sub 'Not logical 
          For Each l_OrderLineCalc In Me 
            If l_OrderLineCalc.IsEmpty OrElse _SortedDictionaryForFindByID.ContainsKey(l_OrderLineCalc.ID) Then 
              'Not Unique or no ID 
              Continue For 
            End If 
            Try 
              _SortedDictionaryForFindByID.Add(l_OrderLineCalc.ID, l_OrderLineCalc) 
            Catch ex As Exception 
              Dim pFault As New clsFault 
              pFault.LogException(ex, l_OrderLineCalc.ToString, "TRGT-_OrderLineCalc-190412-1939", Nothing) 'Log it 
              Throw New Exception("Failed _SortedDictionaryForFindByID:" & ex.Message & ", _OrderLineCalc:" & l_OrderLineCalc.ToString() & ", TRGT-_OrderLineCalc-190412-1939") 'Send it up the line 
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
 
    For Each l_OrderLineCalc As cls_OrderLineCalc In Me 
      l_OrderLineCalc.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each l_OrderLineCalc As cls_OrderLineCalc In Me 
      l_OrderLineCalc.CleanEntityForXML() 
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
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the _OrderLineCalcs by the chosen parameters. This function may be a bit slower than accessing the _OrderLineCalc's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillByParameters", vRequester) 
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
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-_OrderLineCalc-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-_OrderLineCalc-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache._OrderLineCalcCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache._OrderLineCalcCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load cls_OrderLineCalcCol failed: " & pResponse) 
      Dim p_OrderLineCalcsCached As cls_OrderLineCalcCol = MyController.DBCache._OrderLineCalcCol.Clone() 
      p_OrderLineCalcsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then p_OrderLineCalcsCached.Reverse() 
      If vHowMany > 0 AndAlso p_OrderLineCalcsCached.Count > vHowMany Then 
        Dim tmp As New cls_OrderLineCalcCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(p_OrderLineCalcsCached(i)) 
        Next 
        p_OrderLineCalcsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(p_OrderLineCalcsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-090624-1625", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillByOrderHeaderID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache._OrderLineCalcCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache._OrderLineCalcCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load cls_OrderLineCalcCol failed: " & pResponse) 
      Dim p_OrderLineCalcsCached As cls_OrderLineCalcCol = MyController.DBCache._OrderLineCalcCol.CloneByOrderHeaderID(vOrderHeaderID)
      pFault = LoadMeFromDBCache(p_OrderLineCalcsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcsFillByOrderHeaderID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillByProductID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache._OrderLineCalcCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache._OrderLineCalcCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load cls_OrderLineCalcCol failed: " & pResponse) 
      Dim p_OrderLineCalcsCached As cls_OrderLineCalcCol = MyController.DBCache._OrderLineCalcCol.CloneByProductID(vProductID)
      pFault = LoadMeFromDBCache(p_OrderLineCalcsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcsFillByProductID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillByBoundedOrderHeaderID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache._OrderLineCalcCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache._OrderLineCalcCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load cls_OrderLineCalcCol failed: " & pResponse) 
      Dim p_OrderLineCalcsCached As cls_OrderLineCalcCol = MyController.DBCache._OrderLineCalcCol.CloneByBoundedOrderHeaderID(vOrderHeaderIDFrom, vOrderHeaderIDTo)
      pFault = LoadMeFromDBCache(p_OrderLineCalcsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcsFillByBoundedOrderHeaderID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillByBoundedProductID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache._OrderLineCalcCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache._OrderLineCalcCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load cls_OrderLineCalcCol failed: " & pResponse) 
      Dim p_OrderLineCalcsCached As cls_OrderLineCalcCol = MyController.DBCache._OrderLineCalcCol.CloneByBoundedProductID(vProductIDFrom, vProductIDTo)
      pFault = LoadMeFromDBCache(p_OrderLineCalcsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcsFillByBoundedProductID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache._OrderLineCalcCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache._OrderLineCalcCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load cls_OrderLineCalcCol failed: " & pResponse) 
      Dim p_OrderLineCalcsCached As cls_OrderLineCalcCol = MyController.DBCache._OrderLineCalcCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(p_OrderLineCalcsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim l_OrderLineCalc As New cls_OrderLineCalc() 
      pFault = l_OrderLineCalc.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not l_OrderLineCalc.IsEmpty Then Me.Add(l_OrderLineCalc) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim p_OrderLineCalcs As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, p_OrderLineCalcs, "cls_OrderLineCalcCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If p_OrderLineCalcs IsNot Nothing AndAlso Me.Count <> p_OrderLineCalcs.Count Then FillFromListOfITargCCEntity(p_OrderLineCalcs) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillOnTheFly", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-_OrderLineCalc-121122-2008", vRequester) 
      Dim p_OrderLineCalcsCached As cls_OrderLineCalcCol = MyController.DBCache._OrderLineCalcCol.Clone() 
      Dim p_OrderLineCalcsToUse As New cls_OrderLineCalcCol() 
      For Each l In p_OrderLineCalcsCached 
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
        p_OrderLineCalcsToUse.Add(l) 
      Next 
      p_OrderLineCalcsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then p_OrderLineCalcsToUse.Reverse() 
      If vHowMany > 0 AndAlso p_OrderLineCalcsToUse.Count > vHowMany Then 
        Dim tmp As New cls_OrderLineCalcCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(p_OrderLineCalcsToUse(i)) 
        Next 
        p_OrderLineCalcsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(p_OrderLineCalcsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcsFillOnTheFly" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-090303-1658", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillSumOnTheFly", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_vw_OrderLineCalcView, "cls_OrderLineCalcCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-_OrderLineCalc-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "cc_OrderLineCalcsFillSumOnTheFly" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal v_OrderLineCalcArray As cls_OrderLineCalc())
    Me.Clear()
    
    For Each p_OrderLineCalc As cls_OrderLineCalc In v_OrderLineCalcArray
      Me.Add(p_OrderLineCalc)
      _Clean.Add(p_OrderLineCalc.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim p_OrderLineCalc As New cls_OrderLineCalc(pRow, vRequester) 
        Me.Add(p_OrderLineCalc) 
        _Clean.Add(p_OrderLineCalc.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-_OrderLineCalcCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-130515-1300", vRequester) 
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
      Dim p_OrderLineCalcs As cls_OrderLineCalcCol = CType(pXmlSerializer.Deserialize(pStreamReader), cls_OrderLineCalcCol) 
      For Each p_OrderLineCalc As cls_OrderLineCalc In p_OrderLineCalcs 
        Me.Add(p_OrderLineCalc) 
        _Clean.Add(p_OrderLineCalc.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-_OrderLineCalc-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-190720-1443", vRequester) 
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
 
      Dim p_OrderLineCalcs As List(Of cls_OrderLineCalc) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of cls_OrderLineCalc))(vJSON, pSettings) 
      For Each p_OrderLineCalc As cls_OrderLineCalc In p_OrderLineCalcs 
        Me.Add(p_OrderLineCalc) 
        _Clean.Add(p_OrderLineCalc.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-190720-2059", vRequester) 
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
          For Each l_OrderLineCalc As cls_OrderLineCalc In Me 
            Dim pByte As Byte() = l_OrderLineCalc.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-150307-2340", vRequester) 
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
            Dim p_OrderLineCalc As cls_OrderLineCalc = New cls_OrderLineCalc(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(p_OrderLineCalc) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(p_OrderLineCalc.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-_OrderLineCalc-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each p_OrderLineCalc As cls_OrderLineCalc In Me 
      With p_OrderLineCalc 
        pFault = p_OrderLineCalc.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is cls_OrderLineCalcCol) Then Return False 
    Dim p_OrderLineCalcColToTest As cls_OrderLineCalcCol = CType(vEntitiesToTest, cls_OrderLineCalcCol) 
    Return isEqual(p_OrderLineCalcColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="v_OrderLineCalcsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal v_OrderLineCalcsToTest As cls_OrderLineCalcCol) As Boolean
    If Me.Count <> v_OrderLineCalcsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(v_OrderLineCalcsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    If pFilledFromSumOnTheFly Then p_OrderLineCalcs._FilledFromSumOnTheFly = True
    
    For Each p_OrderLineCalc As cls_OrderLineCalc In Me 
      Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone() 
      p_OrderLineCalcs.Add(p_OrderLineCalcClone) 
      If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
    Next 
    Return p_OrderLineCalcs 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As cls_OrderLineCalcCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    If pFilledFromSumOnTheFly Then p_OrderLineCalcs._FilledFromSumOnTheFly = True
    
    For Each p_OrderLineCalc As cls_OrderLineCalc In Me
      Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
      p_OrderLineCalcs.Add(p_OrderLineCalcClone)
      If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
    Next
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OrderHeaderID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOrderHeaderID(ByVal vOrderHeaderIDFrom As Long, ByVal vOrderHeaderIDTo As Long) As cls_OrderLineCalcCol 
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol()  
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList() 
      If (p_OrderLineCalc.OrderHeaderID > vOrderHeaderIDFrom AndAlso p_OrderLineCalc.OrderHeaderID <= vOrderHeaderIDTo) Then 
        Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone() 
        p_OrderLineCalcs.Add(p_OrderLineCalcClone) 
        If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
      End If 
    Next 
    Return p_OrderLineCalcs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ProductID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedProductID(ByVal vProductIDFrom As Long, ByVal vProductIDTo As Long) As cls_OrderLineCalcCol 
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol()  
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList() 
      If (p_OrderLineCalc.ProductID > vProductIDFrom AndAlso p_OrderLineCalc.ProductID <= vProductIDTo) Then 
        Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone() 
        p_OrderLineCalcs.Add(p_OrderLineCalcClone) 
        If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
      End If 
    Next 
    Return p_OrderLineCalcs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As cls_OrderLineCalcCol 
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol()  
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList() 
      If (p_OrderLineCalc.ID > vIDFrom AndAlso p_OrderLineCalc.ID <= vIDTo) Then 
        Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone() 
        p_OrderLineCalcs.Add(p_OrderLineCalcClone) 
        If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
      End If 
    Next 
    Return p_OrderLineCalcs 
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
  Public Function FindByID(ByVal vID As Long) As cls_OrderLineCalc
    If Me.Count = 0 Then Return New cls_OrderLineCalc 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    Dim p_OrderLineCalc As cls_OrderLineCalc = Nothing 
    Dim pFound As Boolean = _SortedDictionaryForFindByID.TryGetValue(vID, p_OrderLineCalc) 
    If pFound = True Then Return p_OrderLineCalc Else Return New cls_OrderLineCalc() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderHeaderID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderHeaderID(ByVal vOrderHeaderID As Long) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.OrderHeaderID = vOrderHeaderID Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.OrderHeaderID = vOrderHeaderID Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductID(ByVal vProductID As Long) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.ProductID = vProductID Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.ProductID = vProductID Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductName(ByVal vProductName As String) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vProductName = vProductName.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.ProductName.ToLowerInvariant() = vProductName Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.ProductName.ToLowerInvariant() = vProductName Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductCode(ByVal vProductCode As String) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vProductCode = vProductCode.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.ProductCode.ToLowerInvariant() = vProductCode Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.ProductCode.ToLowerInvariant() = vProductCode Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Quantity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByQuantity(ByVal vQuantity As Integer) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.Quantity = vQuantity Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.Quantity = vQuantity Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UnitPrice
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUnitPrice(ByVal vUnitPrice As Decimal) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.UnitPrice = vUnitPrice Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.UnitPrice = vUnitPrice Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UnitCost
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUnitCost(ByVal vUnitCost As Decimal) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.UnitCost = vUnitCost Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.UnitCost = vUnitCost Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LineTotal
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLineTotal(ByVal vLineTotal As Decimal) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.LineTotal = vLineTotal Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.LineTotal = vLineTotal Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TotalCost
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTotalCost(ByVal vTotalCost As Decimal) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.TotalCost = vTotalCost Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.TotalCost = vTotalCost Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined GrossProfit
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByGrossProfit(ByVal vGrossProfit As Decimal) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.GrossProfit = vGrossProfit Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.GrossProfit = vGrossProfit Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProfitMarginPercent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProfitMarginPercent(ByVal vProfitMarginPercent As Decimal) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.ProfitMarginPercent = vProfitMarginPercent Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.ProfitMarginPercent = vProfitMarginPercent Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LineNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLineNumber(ByVal vLineNumber As Integer) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.LineNumber = vLineNumber Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.LineNumber = vLineNumber Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As cls_OrderLineCalcCol
    Dim p_OrderLineCalcs As New cls_OrderLineCalcCol() 
    p_OrderLineCalcs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    vTag = vTag.ToLowerInvariant() 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each p_OrderLineCalc As cls_OrderLineCalc In _SortedDictionaryForFindByID.Values.ToList()
        If p_OrderLineCalc.Tag.ToLowerInvariant() = vTag Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As cls_OrderLineCalcCol = Me.Clone() 
      For Each p_OrderLineCalc As cls_OrderLineCalc In pList 
        If p_OrderLineCalc.Tag.ToLowerInvariant() = vTag Then
          Dim p_OrderLineCalcClone As cls_OrderLineCalc = p_OrderLineCalc.Clone()
          p_OrderLineCalcs.Add(p_OrderLineCalcClone)
          If Not _FilledFromSumOnTheFly Then p_OrderLineCalcs._Clean.Add(p_OrderLineCalc.ID) 
        End If
      Next
    End If 
    
    Return p_OrderLineCalcs
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
    For Each p_OrderLineCalc As cls_OrderLineCalc In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = p_OrderLineCalc.LoadDataRow(pRow, vRequester) 
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByOrderHeaderID)
  End Sub
  Private Class CompareByOrderHeaderID
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByProductID)
  End Sub
  Private Class CompareByProductID
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByProductName)
  End Sub
  Private Class CompareByProductName
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProductName, y.ProductName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProductCode()
    Me.Sort(New cls_OrderLineCalcCol.CompareByProductCode)
  End Sub
  Private Class CompareByProductCode
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProductCode, y.ProductCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByQuantity()
    Me.Sort(New cls_OrderLineCalcCol.CompareByQuantity)
  End Sub
  Private Class CompareByQuantity
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByUnitPrice)
  End Sub
  Private Class CompareByUnitPrice
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByUnitCost)
  End Sub
  Private Class CompareByUnitCost
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByLineTotal)
  End Sub
  Private Class CompareByLineTotal
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByTotalCost)
  End Sub
  Private Class CompareByTotalCost
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByGrossProfit)
  End Sub
  Private Class CompareByGrossProfit
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByProfitMarginPercent)
  End Sub
  Private Class CompareByProfitMarginPercent
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByLineNumber)
  End Sub
  Private Class CompareByLineNumber
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
    Me.Sort(New cls_OrderLineCalcCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of cls_OrderLineCalc)
    Private Function Compare(ByVal x As cls_OrderLineCalc, ByVal y As cls_OrderLineCalc) As Integer Implements System.Collections.Generic.IComparer(Of cls_OrderLineCalc).Compare
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
  
    Dim p_OrderLineCalc As cls_OrderLineCalc
  
    While vReader.Read()
      p_OrderLineCalc = New cls_OrderLineCalc() 
      pFault = p_OrderLineCalc.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(p_OrderLineCalc)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(p_OrderLineCalc.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCached_OrderLineCalcCol As cls_OrderLineCalcCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim p_OrderLineCalc As cls_OrderLineCalc 
 
      For Each pCached_OrderLineCalc As cls_OrderLineCalc In vCached_OrderLineCalcCol 
        p_OrderLineCalc = New cls_OrderLineCalc(pCached_OrderLineCalc) 
        p_OrderLineCalc.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(p_OrderLineCalc) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(p_OrderLineCalc.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-_OrderLineCalc-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, cls_OrderLineCalc) 
    _RecreateDictionaryForFindByID = False 
    _LockForFindByID = New Object 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, cls_OrderLineCalc) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = True 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
