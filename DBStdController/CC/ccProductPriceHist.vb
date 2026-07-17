Public Class clsProductPriceHist
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
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [ProductID] 
    [CustomerType] 
    [BaseCost] 
    [SellingPrice] 
    [MinQuantity] 
    [DiscountPercent] 
    [ValidFrom] 
    [ValidTo] 
    [ArchivedDate] 
    [ArchivedReason] 
    [OriginalPriceID] 
    [Notes] 
    [AddFieldsHere] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [ProductID] 
    [BaseCost] 
    [SellingPrice] 
    [MinQuantity] 
    [DiscountPercent] 
    [OriginalPriceID] 
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
  
  
  Private _ID As Long
  Private _ProductID As Long
  Private _CustomerType As clsEnums.enmCustomerType
  Private _CustomerTypeText As String 
  Private _BaseCost As Decimal
  Private _SellingPrice As Decimal
  Private _MinQuantity As Integer
  Private _DiscountPercent As Decimal
  Private _ValidFrom As Date
  Private _ValidTo As Date
  Private _ArchivedDate As Date
  Private _ArchivedReason As String
  Private _OriginalPriceID As Long
  Private _Notes As String
  Private _AddFieldsHere As String
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
  Public Property [ProductID]() As Long
    Get
      Return Me._ProductID
    End Get
    Set(ByVal value As Long)
      If Me._ProductID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProductID = value 
        CreateDefaultDesignation() 
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
        CreateDefaultDesignation() 
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
  Public Property [BaseCost]() As Decimal
    Get
      Return Me._BaseCost
    End Get
    Set(ByVal value As Decimal)
      If Me._BaseCost <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._BaseCost = value 
      End If 
    End Set
  End Property
  Public Property [SellingPrice]() As Decimal
    Get
      Return Me._SellingPrice
    End Get
    Set(ByVal value As Decimal)
      If Me._SellingPrice <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SellingPrice = value 
      End If 
    End Set
  End Property
  Public Property [MinQuantity]() As Integer
    Get
      Return Me._MinQuantity
    End Get
    Set(ByVal value As Integer)
      If Me._MinQuantity <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._MinQuantity = value 
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
  Public Property [ValidFrom]() As Date
    Get
      Return Me._ValidFrom
    End Get
    Set(ByVal value As Date)
      If Me._ValidFrom <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ValidFrom = value 
      End If 
    End Set
  End Property
  Public Property [ValidTo]() As Date
    Get
      Return Me._ValidTo
    End Get
    Set(ByVal value As Date)
      If Me._ValidTo <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ValidTo = value 
      End If 
    End Set
  End Property
  Public Property [ArchivedDate]() As Date
    Get
      Return Me._ArchivedDate
    End Get
    Set(ByVal value As Date)
      If Me._ArchivedDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ArchivedDate = value 
      End If 
    End Set
  End Property
  Public Property [ArchivedReason]() As String
    Get
      Return Me._ArchivedReason
    End Get
    Set(ByVal value As String)
      If Me._ArchivedReason <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ArchivedReason = value 
      End If 
    End Set
  End Property
  Public Property [OriginalPriceID]() As Long
    Get
      Return Me._OriginalPriceID
    End Get
    Set(ByVal value As Long)
      If Me._OriginalPriceID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OriginalPriceID = value 
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
  Public Property [AddFieldsHere]() As String
    Get
      Return Me._AddFieldsHere
    End Get
    Set(ByVal value As String)
      If Me._AddFieldsHere <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AddFieldsHere = value 
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
    If pOverridenValue = Nothing Then bDefaultDesignation = ccHelper.CreateFriendlyTextFromHungarianNotation(_ProductID.ToString() & " " & _CustomerType.FastToString()) Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _ProductID <> 0 Then pValue.Append("ProductID='" & _ProductID.ToString() & "' ‡ ") 
    If _CustomerType <> clsEnums.enmCustomerType.UD Then pValue.Append("CustomerType='" & _CustomerType.FastToString() & "' ‡ ") 
    If _CustomerTypeText <> "" Then pValue.Append("CustomerTypeText='" & _CustomerTypeText & "' ‡ ") 
    If _BaseCost <> 0 Then pValue.Append("BaseCost='" & _BaseCost.ToString() & "' ‡ ") 
    If _SellingPrice <> 0 Then pValue.Append("SellingPrice='" & _SellingPrice.ToString() & "' ‡ ") 
    If _MinQuantity <> 0 Then pValue.Append("MinQuantity='" & _MinQuantity.ToString() & "' ‡ ") 
    If _DiscountPercent <> 0 Then pValue.Append("DiscountPercent='" & _DiscountPercent.ToString() & "' ‡ ") 
    If Not (_ValidFrom = Nothing) Then pValue.Append("ValidFrom='" & _ValidFrom.ToString("o") & "' ‡ ") 
    If Not (_ValidTo = Nothing) Then pValue.Append("ValidTo='" & _ValidTo.ToString("o") & "' ‡ ") 
    If Not (_ArchivedDate = Nothing) Then pValue.Append("ArchivedDate='" & _ArchivedDate.ToString("o") & "' ‡ ") 
    If _ArchivedReason <> "" Then pValue.Append("ArchivedReason='" & _ArchivedReason & "' ‡ ") 
    If _OriginalPriceID <> 0 Then pValue.Append("OriginalPriceID='" & _OriginalPriceID.ToString() & "' ‡ ") 
    If _Notes <> "" Then pValue.Append("Notes='" & _Notes & "' ‡ ") 
    If _AddFieldsHere <> "" Then pValue.Append("AddFieldsHere='" & _AddFieldsHere & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _ProductID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CustomerType.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_CustomerTypeText)}""") 
    pCSV.Append("," & _BaseCost.ToString() & "") 
    pCSV.Append("," & _SellingPrice.ToString() & "") 
    pCSV.Append("," & _MinQuantity.ToString() & "") 
    pCSV.Append("," & _DiscountPercent.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ValidFrom.ToShortDateString & " " & _ValidFrom.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ValidTo.ToShortDateString & " " & _ValidTo.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ArchivedDate.ToShortDateString & " " & _ArchivedDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ArchivedReason)}""") 
    pCSV.Append("," & _OriginalPriceID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Notes)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_AddFieldsHere)}""") 
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
  
  Public Sub New(ByVal vclsProductPriceHist As clsProductPriceHist)
    MyBase.New()
    CreateEmpty()
    AssignValues(vclsProductPriceHist) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vProductID As Long = 0 _ 
    , Optional vCustomerType As clsEnums.enmCustomerType = clsEnums.enmCustomerType.UD _ 
    , Optional vCustomerTypeText As String = "" _ 
    , Optional vBaseCost As Decimal = 0D _ 
    , Optional vSellingPrice As Decimal = 0 _ 
    , Optional vMinQuantity As Integer = 1 _ 
    , Optional vDiscountPercent As Decimal = 0D _ 
    , Optional vValidFrom As Date = Nothing _ 
    , Optional vValidTo As Date = Nothing _ 
    , Optional vArchivedDate As Date = Nothing _ 
    , Optional vArchivedReason As String = "" _ 
    , Optional vOriginalPriceID As Long = 0 _ 
    , Optional vNotes As String = "" _ 
    , Optional vAddFieldsHere As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _ProductID = vProductID 
    _CustomerType = vCustomerType 
    _CustomerTypeText = vCustomerTypeText 
    _BaseCost = vBaseCost 
    _SellingPrice = vSellingPrice 
    _MinQuantity = vMinQuantity 
    _DiscountPercent = vDiscountPercent 
    _ValidFrom = vValidFrom 
    _ValidTo = vValidTo 
    _ArchivedDate = vArchivedDate 
    _ArchivedReason = vArchivedReason 
    _OriginalPriceID = vOriginalPriceID 
    _Notes = vNotes 
    _AddFieldsHere = vAddFieldsHere 
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
 
    _ArchivedReason = _ArchivedReason.Truncate(pTruncateLength, _IsTruncated) 
    _Notes = _Notes.Truncate(pTruncateLength, _IsTruncated) 
    _AddFieldsHere = _AddFieldsHere.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _ArchivedReason = ccHelper.RemoveChrW0(_ArchivedReason) 
    _Notes = ccHelper.RemoveChrW0(_Notes) 
    _AddFieldsHere = ccHelper.RemoveChrW0(_AddFieldsHere) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the ProductPriceHist by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHist_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ProductPriceHist-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the ProductPriceHist by the chosen parameters. This function may be a bit slower than accessing the ProductPriceHist's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHist_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-ProductPriceHist-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ProductPriceHist-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the ProductPriceHist by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHist_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"ProductPriceHist not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-ProductPriceHist-210927-1527", vRequester, vAdditionalMessageToUser:=$"ProductPriceHist not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ProductPriceHistCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ProductPriceHistCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsProductPriceHistCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ProductPriceHistCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccProductPriceHistGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"ProductPriceHist not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-ProductPriceHist-210625-0950", vRequester, vAdditionalMessageToUser:=$"ProductPriceHist not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, "clsProductPriceHist_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-ProductPriceHist-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, "clsProductPriceHist_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-ProductPriceHist-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the ProductPriceHist. If there are parents or children in the ProductPriceHist, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, "clsProductPriceHist_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pProductPriceHist As New clsProductPriceHist() 
    If Me.isEqual(pProductPriceHist) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-ProductPriceHist-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-ProductPriceHist-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "ccProductPriceHistUpdate"
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
      If MyController.DBCache.ProductPriceHistCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ProductPriceHistCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsProductPriceHistCol failed: " & pResponse) 
      Dim pCachedProductPriceHist As clsProductPriceHist 
      If _ID = 0 Then 
        pCachedProductPriceHist = New clsProductPriceHist() 
        'get last ID 
        Dim pProductPriceHistCol As clsProductPriceHistCol = MyController.DBCache.ProductPriceHistCol.Clone() 
        If pProductPriceHistCol.Count = 0 Then 
          _ID = 1 
        Else 
          pProductPriceHistCol.SortByID() 
          Dim pLastID As Long = pProductPriceHistCol(pProductPriceHistCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ProductPriceHistCol.Add(pCachedProductPriceHist) 
      Else  
        pCachedProductPriceHist = MyController.DBCache.ProductPriceHistCol.FindByID(_ID) 
      End If 
      pCachedProductPriceHist.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ProductPriceHistCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "ProductID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ProductID) 
        pLastReadVariableName = "enmCustomerType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_CustomerType.FastToString()) 
        pLastReadVariableName = "BaseCost" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Decimal).Value = (_BaseCost) 
        pLastReadVariableName = "SellingPrice" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Decimal).Value = (_SellingPrice) 
        pLastReadVariableName = "MinQuantity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_MinQuantity) 
        pLastReadVariableName = "DiscountPercent" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Decimal).Value = (_DiscountPercent) 
        pLastReadVariableName = "ValidFrom" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_ValidFrom) 
        pLastReadVariableName = "ValidTo" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_ValidTo) 
        pLastReadVariableName = "ArchivedDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_ArchivedDate) 
        pLastReadVariableName = "ArchivedReason" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_ArchivedReason) 
        pLastReadVariableName = "OriginalPriceID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_OriginalPriceID) 
        pLastReadVariableName = "Notes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes) 
        pLastReadVariableName = "AddFieldsHere" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_AddFieldsHere) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-ProductPriceHist-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ProductPriceHist-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
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
    Dim pFunctionParameters As String = String.Format("ProductPriceHist.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistDelete, "clsProductPriceHist_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "ccProductPriceHistDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ProductPriceHistCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ProductPriceHistCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsProductPriceHistCol failed: " & pResponse) 
      MyController.DBCache.ProductPriceHistCol.Remove(MyController.DBCache.ProductPriceHistCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ProductPriceHistCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ProductPriceHist-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ProductPriceHist-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistDelete, "clsProductPriceHist_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "ccProductPriceHistDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ProductPriceHistCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ProductPriceHistCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsProductPriceHistCol failed: " & pResponse) 
      MyController.DBCache.ProductPriceHistCol.Remove(MyController.DBCache.ProductPriceHistCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ProductPriceHistCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ProductPriceHist-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ProductPriceHist-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-231207-0843", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is clsProductPriceHist) Then Return False 
    Dim pProductPriceHistToTest As clsProductPriceHist = CType(vTargCCEntityToTest, clsProductPriceHist) 
    Return isEqual(pProductPriceHistToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vProductPriceHistToTest As clsProductPriceHist) As Boolean
    With vProductPriceHistToTest
      If _ID <> .ID Then Return False
      If _ProductID <> .ProductID Then Return False
      If _CustomerType <> .CustomerType Then Return False
      If _BaseCost <> .BaseCost Then Return False
      If _SellingPrice <> .SellingPrice Then Return False
      If _MinQuantity <> .MinQuantity Then Return False
      If _DiscountPercent <> .DiscountPercent Then Return False
      If _ValidFrom <> Nothing AndAlso .ValidFrom <> Nothing Then 
        If ccHelper.ToLong(_ValidFrom.Subtract(.ValidFrom).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_ValidFrom = Nothing AndAlso .ValidFrom = Nothing) Then 
        Return False 
      End If 
      If _ValidTo <> Nothing AndAlso .ValidTo <> Nothing Then 
        If ccHelper.ToLong(_ValidTo.Subtract(.ValidTo).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_ValidTo = Nothing AndAlso .ValidTo = Nothing) Then 
        Return False 
      End If 
      If _ArchivedDate <> Nothing AndAlso .ArchivedDate <> Nothing Then 
        If ccHelper.ToLong(_ArchivedDate.Subtract(.ArchivedDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_ArchivedDate = Nothing AndAlso .ArchivedDate = Nothing) Then 
        Return False 
      End If 
      If _ArchivedReason <> .ArchivedReason Then Return False
      If _OriginalPriceID <> .OriginalPriceID Then Return False
      If _Notes <> .Notes Then Return False
      If _AddFieldsHere <> .AddFieldsHere Then Return False
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
    Dim pClone As New clsProductPriceHist(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsProductPriceHist
    Dim pClone As New clsProductPriceHist(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductID") = _ProductID : Catch ex As Exception : Return pFault.LogException(ex, "ProductID", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("CustomerType") = _CustomerType : Catch ex As Exception : Return pFault.LogException(ex, "CustomerType", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("BaseCost") = _BaseCost : Catch ex As Exception : Return pFault.LogException(ex, "BaseCost", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("SellingPrice") = _SellingPrice : Catch ex As Exception : Return pFault.LogException(ex, "SellingPrice", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("MinQuantity") = _MinQuantity : Catch ex As Exception : Return pFault.LogException(ex, "MinQuantity", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("DiscountPercent") = _DiscountPercent : Catch ex As Exception : Return pFault.LogException(ex, "DiscountPercent", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("ValidFrom") = _ValidFrom : Catch ex As Exception : Return pFault.LogException(ex, "ValidFrom", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("ValidTo") = _ValidTo : Catch ex As Exception : Return pFault.LogException(ex, "ValidTo", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("ArchivedDate") = _ArchivedDate : Catch ex As Exception : Return pFault.LogException(ex, "ArchivedDate", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("ArchivedReason") = _ArchivedReason : Catch ex As Exception : Return pFault.LogException(ex, "ArchivedReason", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("OriginalPriceID") = _OriginalPriceID : Catch ex As Exception : Return pFault.LogException(ex, "OriginalPriceID", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("Notes") = _Notes : Catch ex As Exception : Return pFault.LogException(ex, "Notes", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
    Try : vDataRow("AddFieldsHere") = _AddFieldsHere : Catch ex As Exception : Return pFault.LogException(ex, "AddFieldsHere", "TRGT-ProductPriceHist-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pProductPriceHist As clsProductPriceHist = CType(pXmlSerializer.Deserialize(pStreamReader), clsProductPriceHist) 
      AssignValues(pProductPriceHist) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-ProductPriceHist-130515-1230", vRequester) 
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
          'ProductID 
          pBinaryWriter.Write(_ProductID) 
          'CustomerType 
          pBinaryWriter.Write(_CustomerType.FastToString()) 
          'BaseCost 
          pBinaryWriter.Write(_BaseCost) 
          'SellingPrice 
          pBinaryWriter.Write(_SellingPrice) 
          'MinQuantity 
          pBinaryWriter.Write(_MinQuantity) 
          'DiscountPercent 
          pBinaryWriter.Write(_DiscountPercent) 
          'ValidFrom 
          pBinaryWriter.Write(_ValidFrom.Ticks) 
          'ValidTo 
          pBinaryWriter.Write(_ValidTo.Ticks) 
          'ArchivedDate 
          pBinaryWriter.Write(_ArchivedDate.Ticks) 
          'ArchivedReason 
          If _ArchivedReason Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ArchivedReason) 
          'OriginalPriceID 
          pBinaryWriter.Write(_OriginalPriceID) 
          'Notes 
          If _Notes Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Notes) 
          'AddFieldsHere 
          If _AddFieldsHere Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_AddFieldsHere) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-150307-2338", vRequester) 
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
          'ProductID 
          _ProductID = pReader.ReadInt64 
          'CustomerType 
          _CustomerType = clsEnums.TranslateEnmCustomerType(pReader.ReadString) 
          'BaseCost 
          _BaseCost = pReader.ReadDecimal 
          'SellingPrice 
          _SellingPrice = pReader.ReadDecimal 
          'MinQuantity 
          _MinQuantity = pReader.ReadInt32 
          'DiscountPercent 
          _DiscountPercent = pReader.ReadDecimal 
          'ValidFrom 
          _ValidFrom = New Date(pReader.ReadInt64) 
          'ValidTo 
          _ValidTo = New Date(pReader.ReadInt64) 
          'ArchivedDate 
          _ArchivedDate = New Date(pReader.ReadInt64) 
          'ArchivedReason 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ArchivedReason = pReader.ReadString 
          'OriginalPriceID 
          _OriginalPriceID = pReader.ReadInt64 
          'Notes 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Notes = pReader.ReadString 
          'AddFieldsHere 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _AddFieldsHere = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-ProductPriceHist-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-190720-1443", vRequester) 
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
 
      Dim pProductPriceHist As clsProductPriceHist = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsProductPriceHist)(vJSON, pSettings) 
      AssignValues(pProductPriceHist) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vProductPriceHist As clsProductPriceHist)
    With vProductPriceHist
      _ID = .ID 
      _ProductID = .ProductID 
      _CustomerType = .CustomerType 
      _CustomerTypeText = .CustomerTypeText
      _BaseCost = .BaseCost 
      _SellingPrice = .SellingPrice 
      _MinQuantity = .MinQuantity 
      _DiscountPercent = .DiscountPercent 
      _ValidFrom = .ValidFrom 
      _ValidTo = .ValidTo 
      _ArchivedDate = .ArchivedDate 
      _ArchivedReason = .ArchivedReason 
      _OriginalPriceID = .OriginalPriceID 
      _Notes = .Notes 
      _AddFieldsHere = .AddFieldsHere 
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
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-ProductPriceHist-151124-1900", vRequester) 
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
      pLastReadVariableName = "ProductID" 
      If Not vReader.IsDBNull(1) Then _ProductID = vReader.GetInt64(1)
      pLastReadVariableName = "enmCustomerType" 
      If Not vReader.IsDBNull(2) Then _CustomerType = clsEnums.TranslateEnmCustomerType(vReader.GetString(2))
      pLastReadVariableName = "BaseCost" 
      If Not vReader.IsDBNull(3) Then _BaseCost = vReader.GetDecimal(3)
      pLastReadVariableName = "SellingPrice" 
      If Not vReader.IsDBNull(4) Then _SellingPrice = vReader.GetDecimal(4)
      pLastReadVariableName = "MinQuantity" 
      If Not vReader.IsDBNull(5) Then _MinQuantity = vReader.GetInt32(5)
      pLastReadVariableName = "DiscountPercent" 
      If Not vReader.IsDBNull(6) Then _DiscountPercent = vReader.GetDecimal(6)
      pLastReadVariableName = "ValidFrom" 
      If Not vReader.IsDBNull(7) Then _ValidFrom = vReader.GetDateTime(7)
      pLastReadVariableName = "ValidTo" 
      If Not vReader.IsDBNull(8) Then _ValidTo = vReader.GetDateTime(8)
      pLastReadVariableName = "ArchivedDate" 
      If Not vReader.IsDBNull(9) Then _ArchivedDate = vReader.GetDateTime(9)
      pLastReadVariableName = "ArchivedReason" 
      If Not vReader.IsDBNull(10) Then _ArchivedReason = vReader.GetString(10) 
      pLastReadVariableName = "OriginalPriceID" 
      If Not vReader.IsDBNull(11) Then _OriginalPriceID = vReader.GetInt64(11)
      pLastReadVariableName = "Notes" 
      If Not vReader.IsDBNull(12) Then _Notes = vReader.GetString(12) 
      pLastReadVariableName = "AddFieldsHere" 
      If Not vReader.IsDBNull(13) Then _AddFieldsHere = vReader.GetString(13) 
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(14) Then bDateAdded = vReader.GetDateTime(14)   
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedProductPriceHist As clsProductPriceHist, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedProductPriceHist) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _ProductID = 0
    _CustomerType = clsEnums.enmCustomerType.UD
    _CustomerTypeText = ""
    'Default Value set by SQL Server Database (below): 0D
    _BaseCost = 0D
    _SellingPrice = 0
    'Default Value set by SQL Server Database (below): 1
    _MinQuantity = 1
    'Default Value set by SQL Server Database (below): 0D
    _DiscountPercent = 0D
    _ValidFrom = Nothing
    _ValidTo = Nothing
    'Default Value set by SQL Server Database (below): etdate(
    _ArchivedDate = Nothing
    _ArchivedReason = ""
    _OriginalPriceID = 0
    _Notes = ""
    _AddFieldsHere = ""
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
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class clsProductPriceHistCol
  Inherits cTargCCCollection(Of clsProductPriceHist)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsProductPriceHist) 
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
 
    For Each pRow As clsProductPriceHist In Me 
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
    pCSVTitle.Append(",""ProductID""") 
    pCSVTitle.Append(",""CustomerType" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""CustomerType (Text)""") 
    pCSVTitle.Append(",""BaseCost""") 
    pCSVTitle.Append(",""SellingPrice""") 
    pCSVTitle.Append(",""MinQuantity""") 
    pCSVTitle.Append(",""DiscountPercent""") 
    pCSVTitle.Append(",""ValidFrom""") 
    pCSVTitle.Append(",""ValidTo""") 
    pCSVTitle.Append(",""ArchivedDate""") 
    pCSVTitle.Append(",""ArchivedReason""") 
    pCSVTitle.Append(",""OriginalPriceID""") 
    pCSVTitle.Append(",""Notes""") 
    pCSVTitle.Append(",""AddFieldsHere""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As clsProductPriceHist In Me 
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
 
  Public Overloads Sub Add(ByVal vProductPriceHist As clsProductPriceHist) 
    SyncLock _CollectionLock 
      MyBase.Add(vProductPriceHist) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vProductPriceHist As clsProductPriceHist) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vProductPriceHist) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vProductPriceHistCol As clsProductPriceHistCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vProductPriceHistCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vProductPriceHist As clsProductPriceHist) 
    SyncLock _CollectionLock 
      MyBase.Remove(vProductPriceHist) 
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
      Dim pTempDictionary As New Dictionary(Of Long, clsProductPriceHist) 
      
      For Each lProductPriceHist In Me 
        If lProductPriceHist.IsEmpty OrElse pTempDictionary.ContainsKey(lProductPriceHist.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lProductPriceHist.ID, lProductPriceHist) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lProductPriceHist.ToString, "TRGT-ProductPriceHist-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", ProductPriceHist:" & lProductPriceHist.ToString() & ", TRGT-ProductPriceHist-260111-154657") 'Send it up the line 
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
 
    For Each lProductPriceHist As clsProductPriceHist In Me 
      lProductPriceHist.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lProductPriceHist As clsProductPriceHist In Me 
      lProductPriceHist.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the ProductPriceHists by the chosen parameters. This function may be a bit slower than accessing the ProductPriceHist's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-ProductPriceHist-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ProductPriceHist-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ProductPriceHistCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ProductPriceHistCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsProductPriceHistCol failed: " & pResponse) 
      Dim pProductPriceHistsCached As clsProductPriceHistCol = MyController.DBCache.ProductPriceHistCol.Clone() 
      pProductPriceHistsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pProductPriceHistsCached.Reverse() 
      If vHowMany > 0 AndAlso pProductPriceHistsCached.Count > vHowMany Then 
        Dim tmp As New clsProductPriceHistCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pProductPriceHistsCached(i)) 
        Next 
        pProductPriceHistsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pProductPriceHistsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccProductPriceHistsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-090624-1625", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ProductPriceHistCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ProductPriceHistCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsProductPriceHistCol failed: " & pResponse) 
      Dim pProductPriceHistsCached As clsProductPriceHistCol = MyController.DBCache.ProductPriceHistCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pProductPriceHistsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccProductPriceHistsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lProductPriceHist As New clsProductPriceHist() 
      pFault = lProductPriceHist.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lProductPriceHist.IsEmpty Then Me.Add(lProductPriceHist) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pProductPriceHists As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pProductPriceHists, "clsProductPriceHistCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pProductPriceHists IsNot Nothing AndAlso Me.Count <> pProductPriceHists.Count Then FillFromListOfITargCCEntity(pProductPriceHists) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
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
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-ProductPriceHist-121122-2008", vRequester) 
      Dim pProductPriceHistsCached As clsProductPriceHistCol = MyController.DBCache.ProductPriceHistCol.Clone() 
      Dim pProductPriceHistsToUse As New clsProductPriceHistCol() 
      For Each l In pProductPriceHistsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        pProductPriceHistsToUse.Add(l) 
      Next 
      pProductPriceHistsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pProductPriceHistsToUse.Reverse() 
      If vHowMany > 0 AndAlso pProductPriceHistsToUse.Count > vHowMany Then 
        Dim tmp As New clsProductPriceHistCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pProductPriceHistsToUse(i)) 
        Next 
        pProductPriceHistsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pProductPriceHistsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccProductPriceHistsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
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
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-ProductPriceHist-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccProductPriceHistsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vProductPriceHistArray As clsProductPriceHist())
    Me.Clear()
    
    For Each pProductPriceHist As clsProductPriceHist In vProductPriceHistArray
      Me.Add(pProductPriceHist)
      _Clean.Add(pProductPriceHist.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pProductPriceHist As New clsProductPriceHist(pRow, vRequester) 
        Me.Add(pProductPriceHist) 
        _Clean.Add(pProductPriceHist.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-ProductPriceHistCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-130515-1300", vRequester) 
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
      Dim pProductPriceHists As clsProductPriceHistCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsProductPriceHistCol) 
      For Each pProductPriceHist As clsProductPriceHist In pProductPriceHists 
        Me.Add(pProductPriceHist) 
        _Clean.Add(pProductPriceHist.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-ProductPriceHist-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-190720-1443", vRequester) 
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
 
      Dim pProductPriceHists As List(Of clsProductPriceHist) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of clsProductPriceHist))(vJSON, pSettings) 
      For Each pProductPriceHist As clsProductPriceHist In pProductPriceHists 
        Me.Add(pProductPriceHist) 
        _Clean.Add(pProductPriceHist.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-190720-2059", vRequester) 
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
          For Each lProductPriceHist As clsProductPriceHist In Me 
            Dim pByte As Byte() = lProductPriceHist.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-150307-2340", vRequester) 
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
            Dim pProductPriceHist As clsProductPriceHist = New clsProductPriceHist(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pProductPriceHist) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pProductPriceHist.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-ProductPriceHist-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pProductPriceHist As clsProductPriceHist In Me 
      With pProductPriceHist 
        pFault = pProductPriceHist.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is clsProductPriceHistCol) Then Return False 
    Dim pProductPriceHistColToTest As clsProductPriceHistCol = CType(vEntitiesToTest, clsProductPriceHistCol) 
    Return isEqual(pProductPriceHistColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vProductPriceHistsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vProductPriceHistsToTest As clsProductPriceHistCol) As Boolean
    If Me.Count <> vProductPriceHistsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vProductPriceHistsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    If pFilledFromSumOnTheFly Then pProductPriceHists._FilledFromSumOnTheFly = True
    
    For Each pProductPriceHist As clsProductPriceHist In Me 
      Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone() 
      pProductPriceHists.Add(pProductPriceHistClone) 
      If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
    Next 
    Return pProductPriceHists 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsProductPriceHistCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    If pFilledFromSumOnTheFly Then pProductPriceHists._FilledFromSumOnTheFly = True
    
    For Each pProductPriceHist As clsProductPriceHist In Me
      Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
      pProductPriceHists.Add(pProductPriceHistClone)
      If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
    Next
    Return pProductPriceHists
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As clsProductPriceHistCol 
    Dim pProductPriceHists As New clsProductPriceHistCol()  
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pProductPriceHist As clsProductPriceHist In _SortedDictionaryForFindByID.Values.ToList() 
      If (pProductPriceHist.ID > vIDFrom AndAlso pProductPriceHist.ID <= vIDTo) Then 
        Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone() 
        pProductPriceHists.Add(pProductPriceHistClone) 
        If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
      End If 
    Next 
    Return pProductPriceHists 
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
  Public Function FindByID(ByVal vID As Long) As clsProductPriceHist
    If Me.Count = 0 Then Return New clsProductPriceHist 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
    
    Dim pProductPriceHist As clsProductPriceHist = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pProductPriceHist) 
    If pProductPriceHist IsNot Nothing Then Return pProductPriceHist Else Return New clsProductPriceHist() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductID(ByVal vProductID As Long) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.ProductID = vProductID Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByProductID with vProductID of {vProductID}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.ProductID = vProductID Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CustomerType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCustomerType(ByVal vCustomerType As clsEnums.enmCustomerType) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.CustomerType = vCustomerType Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCustomerType with vCustomerType of {vCustomerType}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.CustomerType = vCustomerType Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined BaseCost
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByBaseCost(ByVal vBaseCost As Decimal) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.BaseCost = vBaseCost Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByBaseCost with vBaseCost of {vBaseCost}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.BaseCost = vBaseCost Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SellingPrice
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySellingPrice(ByVal vSellingPrice As Decimal) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.SellingPrice = vSellingPrice Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySellingPrice with vSellingPrice of {vSellingPrice}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.SellingPrice = vSellingPrice Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined MinQuantity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMinQuantity(ByVal vMinQuantity As Integer) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.MinQuantity = vMinQuantity Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByMinQuantity with vMinQuantity of {vMinQuantity}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.MinQuantity = vMinQuantity Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DiscountPercent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDiscountPercent(ByVal vDiscountPercent As Decimal) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.DiscountPercent = vDiscountPercent Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDiscountPercent with vDiscountPercent of {vDiscountPercent}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.DiscountPercent = vDiscountPercent Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ValidFrom
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByValidFrom(ByVal vValidFrom As Date) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.ValidFrom = vValidFrom Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByValidFrom with vValidFrom of {vValidFrom}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.ValidFrom = vValidFrom Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ValidTo
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByValidTo(ByVal vValidTo As Date) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.ValidTo = vValidTo Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByValidTo with vValidTo of {vValidTo}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.ValidTo = vValidTo Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ArchivedDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByArchivedDate(ByVal vArchivedDate As Date) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.ArchivedDate = vArchivedDate Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByArchivedDate with vArchivedDate of {vArchivedDate}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.ArchivedDate = vArchivedDate Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ArchivedReason
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByArchivedReason(ByVal vArchivedReason As String) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vArchivedReason = vArchivedReason.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.ArchivedReason.ToLowerInvariant() = vArchivedReason Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByArchivedReason with vArchivedReason of {vArchivedReason}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.ArchivedReason.ToLowerInvariant() = vArchivedReason Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OriginalPriceID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOriginalPriceID(ByVal vOriginalPriceID As Long) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.OriginalPriceID = vOriginalPriceID Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOriginalPriceID with vOriginalPriceID of {vOriginalPriceID}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.OriginalPriceID = vOriginalPriceID Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Notes
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNotes(ByVal vNotes As String) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNotes = vNotes.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.Notes.ToLowerInvariant() = vNotes Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNotes with vNotes of {vNotes}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.Notes.ToLowerInvariant() = vNotes Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AddFieldsHere
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAddFieldsHere(ByVal vAddFieldsHere As String) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vAddFieldsHere = vAddFieldsHere.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.AddFieldsHere.ToLowerInvariant() = vAddFieldsHere Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAddFieldsHere with vAddFieldsHere of {vAddFieldsHere}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.AddFieldsHere.ToLowerInvariant() = vAddFieldsHere Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As clsProductPriceHistCol
    Dim pProductPriceHists As New clsProductPriceHistCol() 
    pProductPriceHists._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsProductPriceHist) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pProductPriceHist As clsProductPriceHist In pTempDist.Values
        If pProductPriceHist.Tag.ToLowerInvariant() = vTag Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As clsProductPriceHistCol = Me.Clone() 
      For Each pProductPriceHist As clsProductPriceHist In pList 
        If pProductPriceHist.Tag.ToLowerInvariant() = vTag Then
          Dim pProductPriceHistClone As clsProductPriceHist = pProductPriceHist.Clone()
          pProductPriceHists.Add(pProductPriceHistClone)
          If Not _FilledFromSumOnTheFly Then pProductPriceHists._Clean.Add(pProductPriceHist.ID) 
        End If
      Next
    End If 
    
    Return pProductPriceHists
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
    For Each pProductPriceHist As clsProductPriceHist In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pProductPriceHist.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistView, "clsProductPriceHistCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As clsProductPriceHist In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As clsProductPriceHist = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pProductPriceHistToKill As New clsProductPriceHist 
          pProductPriceHistToKill.ID = pCleanID 
          pProductPriceHistToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pProductPriceHistToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As clsProductPriceHist In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-ProductPriceHist-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistUpdate, "clsProductPriceHistCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As clsProductPriceHist In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As clsProductPriceHist In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistDelete, "clsProductPriceHistCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccProductPriceHistsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New clsProductPriceHistCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ProductPriceHist-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ProductPriceHist-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist--090624-1625", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_ProductPriceHistDelete, "clsProductPriceHistCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccProductPriceHistsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-ProductPriceHist-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ProductPriceHist-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ProductPriceHist-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-090210-1341", vRequester) 
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
    Me.Sort(New clsProductPriceHistCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
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
  
  Public Sub SortByProductID()
    Me.Sort(New clsProductPriceHistCol.CompareByProductID)
  End Sub
  Private Class CompareByProductID
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
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
  
  Public Sub SortByCustomerType()
    Me.Sort(New clsProductPriceHistCol.CompareByCustomerType)
  End Sub
  Private Class CompareByCustomerType
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
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
    Me.Sort(New clsProductPriceHistCol.CompareByCustomerTypeText)
  End Sub
  Private Class CompareByCustomerTypeText
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CustomerTypeText, y.CustomerTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByBaseCost()
    Me.Sort(New clsProductPriceHistCol.CompareByBaseCost)
  End Sub
  Private Class CompareByBaseCost
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.BaseCost < y.BaseCost Then
        Return -1
      ElseIf x.BaseCost = y.BaseCost Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySellingPrice()
    Me.Sort(New clsProductPriceHistCol.CompareBySellingPrice)
  End Sub
  Private Class CompareBySellingPrice
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.SellingPrice < y.SellingPrice Then
        Return -1
      ElseIf x.SellingPrice = y.SellingPrice Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByMinQuantity()
    Me.Sort(New clsProductPriceHistCol.CompareByMinQuantity)
  End Sub
  Private Class CompareByMinQuantity
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.MinQuantity < y.MinQuantity Then
        Return -1
      ElseIf x.MinQuantity = y.MinQuantity Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDiscountPercent()
    Me.Sort(New clsProductPriceHistCol.CompareByDiscountPercent)
  End Sub
  Private Class CompareByDiscountPercent
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
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
  
  Public Sub SortByValidFrom()
    Me.Sort(New clsProductPriceHistCol.CompareByValidFrom)
  End Sub
  Private Class CompareByValidFrom
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ValidFrom < y.ValidFrom Then
        Return -1
      ElseIf x.ValidFrom = y.ValidFrom Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByValidTo()
    Me.Sort(New clsProductPriceHistCol.CompareByValidTo)
  End Sub
  Private Class CompareByValidTo
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ValidTo < y.ValidTo Then
        Return -1
      ElseIf x.ValidTo = y.ValidTo Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByArchivedDate()
    Me.Sort(New clsProductPriceHistCol.CompareByArchivedDate)
  End Sub
  Private Class CompareByArchivedDate
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ArchivedDate < y.ArchivedDate Then
        Return -1
      ElseIf x.ArchivedDate = y.ArchivedDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByArchivedReason()
    Me.Sort(New clsProductPriceHistCol.CompareByArchivedReason)
  End Sub
  Private Class CompareByArchivedReason
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ArchivedReason, y.ArchivedReason, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOriginalPriceID()
    Me.Sort(New clsProductPriceHistCol.CompareByOriginalPriceID)
  End Sub
  Private Class CompareByOriginalPriceID
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OriginalPriceID < y.OriginalPriceID Then
        Return -1
      ElseIf x.OriginalPriceID = y.OriginalPriceID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByNotes()
    Me.Sort(New clsProductPriceHistCol.CompareByNotes)
  End Sub
  Private Class CompareByNotes
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Notes, y.Notes, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAddFieldsHere()
    Me.Sort(New clsProductPriceHistCol.CompareByAddFieldsHere)
  End Sub
  Private Class CompareByAddFieldsHere
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AddFieldsHere, y.AddFieldsHere, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New clsProductPriceHistCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsProductPriceHist)
    Private Function Compare(ByVal x As clsProductPriceHist, ByVal y As clsProductPriceHist) As Integer Implements System.Collections.Generic.IComparer(Of clsProductPriceHist).Compare
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
  
    Dim pProductPriceHist As clsProductPriceHist
  
    While vReader.Read()
      pProductPriceHist = New clsProductPriceHist() 
      pFault = pProductPriceHist.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pProductPriceHist)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pProductPriceHist.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedProductPriceHistCol As clsProductPriceHistCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pProductPriceHist As clsProductPriceHist 
 
      For Each pCachedProductPriceHist As clsProductPriceHist In vCachedProductPriceHistCol 
        pProductPriceHist = New clsProductPriceHist(pCachedProductPriceHist) 
        pProductPriceHist.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pProductPriceHist) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pProductPriceHist.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ProductPriceHist-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsProductPriceHist) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsProductPriceHist) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
