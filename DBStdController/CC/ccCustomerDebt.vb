Public Class clsCustomerDebt
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
    [OrderHeader] 
    [DebtStatus] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [Customer] 
    [OrderHeader] 
    [DebtAmount] 
    [PaidAmount] 
    [RemainingAmount] 
    [DebtDate] 
    [DueDate] 
    [DebtStatus] 
    [Notes] 
    [NeedsAttention] 
    [ProductTypes] 
    [DeliveryDate] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [DebtAmount] 
    [PaidAmount] 
    [RemainingAmount] 
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
  Private _CustomerID As Long
  Private _Customer As clsCustomer
  Private _CustomerText As String
  Private _OrderHeaderID As Long
  Private _OrderHeader As clsOrderHeader
  Private _OrderHeaderText As String
  Private _DebtAmount As Decimal
  Private _PaidAmount As Decimal
  Private _RemainingAmount As Decimal
  Private _DebtDate As Date
  Private _DueDate As Date
  Private _DebtStatus As clsEnums.enmDebtStatus
  Private _DebtStatusText As String 
  Private _Notes As String
  Private _NeedsAttention As Boolean
  Private _ProductTypes As String
  Private _DeliveryDate As Date
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
  Public Property [CustomerID]() As Long
    Get
      Return Me._CustomerID
    End Get
    Set(ByVal value As Long)
      If Me._CustomerID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CustomerID = value 
        CreateDefaultDesignation() 
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
  Public Property [DebtAmount]() As Decimal
    Get
      Return Me._DebtAmount
    End Get
    Set(ByVal value As Decimal)
      If Me._DebtAmount <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DebtAmount = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [PaidAmount]() As Decimal
    Get
      Return Me._PaidAmount
    End Get
    Set(ByVal value As Decimal)
      If Me._PaidAmount <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PaidAmount = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [RemainingAmount]() As Decimal
    Get
      Return Me._RemainingAmount
    End Get
  End Property
  Public Property [DebtDate]() As Date
    Get
      Return Me._DebtDate
    End Get
    Set(ByVal value As Date)
      If Me._DebtDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DebtDate = value 
      End If 
    End Set
  End Property
  Public Property [DueDate]() As Date
    Get
      Return Me._DueDate
    End Get
    Set(ByVal value As Date)
      If Me._DueDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DueDate = value 
      End If 
    End Set
  End Property
  Public Property [DebtStatus]() As clsEnums.enmDebtStatus
    Get
      Return Me._DebtStatus
    End Get
    Set(ByVal value As clsEnums.enmDebtStatus)
      If Me._DebtStatus <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DebtStatus = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [DebtStatusText]() As String
    Get
      Return Me._DebtStatusText
    End Get
    Set(ByVal value As String)
      Me._DebtStatusText = value
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
  Public Property [NeedsAttention]() As Boolean
    Get
      Return Me._NeedsAttention
    End Get
    Set(ByVal value As Boolean)
      If Me._NeedsAttention <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._NeedsAttention = value 
      End If 
    End Set
  End Property
  Public Property [ProductTypes]() As String
    Get
      Return Me._ProductTypes
    End Get
    Set(ByVal value As String)
      If Me._ProductTypes <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProductTypes = value 
      End If 
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
    If pOverridenValue = Nothing Then bDefaultDesignation = _CustomerID.ToString() & "bt of " & _DebtAmount.ToString() Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _CustomerID <> 0 Then pValue.Append("CustomerID='" & _CustomerID.ToString() & "' ‡ ") 
    If _CustomerText <> "" Then pValue.Append("CustomerText='" & _CustomerText & "' ‡ ") 
    If _OrderHeaderID <> 0 Then pValue.Append("OrderHeaderID='" & _OrderHeaderID.ToString() & "' ‡ ") 
    If _OrderHeaderText <> "" Then pValue.Append("OrderHeaderText='" & _OrderHeaderText & "' ‡ ") 
    If _DebtAmount <> 0 Then pValue.Append("DebtAmount='" & _DebtAmount.ToString() & "' ‡ ") 
    If _PaidAmount <> 0 Then pValue.Append("PaidAmount='" & _PaidAmount.ToString() & "' ‡ ") 
    If _RemainingAmount <> 0 Then pValue.Append("RemainingAmount='" & _RemainingAmount.ToString() & "' ‡ ") 
    If Not (_DebtDate = Nothing) Then pValue.Append("DebtDate='" & _DebtDate.ToString("o") & "' ‡ ") 
    If Not (_DueDate = Nothing) Then pValue.Append("DueDate='" & _DueDate.ToString("o") & "' ‡ ") 
    If _DebtStatus <> clsEnums.enmDebtStatus.UD Then pValue.Append("DebtStatus='" & _DebtStatus.FastToString() & "' ‡ ") 
    If _DebtStatusText <> "" Then pValue.Append("DebtStatusText='" & _DebtStatusText & "' ‡ ") 
    If _Notes <> "" Then pValue.Append("Notes='" & _Notes & "' ‡ ") 
    pValue.Append("NeedsAttention='" & _NeedsAttention.ToString() & "' ‡ ") 
    If _ProductTypes <> "" Then pValue.Append("ProductTypes='" & _ProductTypes & "' ‡ ") 
    If Not (_DeliveryDate = Nothing) Then pValue.Append("DeliveryDate='" & _DeliveryDate.ToString("o") & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _CustomerID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_CustomerText)}""") 
    pCSV.Append("," & _OrderHeaderID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_OrderHeaderText)}""") 
    pCSV.Append("," & _DebtAmount.ToString() & "") 
    pCSV.Append("," & _PaidAmount.ToString() & "") 
    pCSV.Append("," & _RemainingAmount.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DebtDate.ToShortDateString & " " & _DebtDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DueDate.ToShortDateString & " " & _DueDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DebtStatus.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_DebtStatusText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Notes)}""") 
    pCSV.Append(",""" & _NeedsAttention.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProductTypes)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DeliveryDate.ToShortDateString & " " & _DeliveryDate.ToShortTimeString)}""") 
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
  
  Public Sub New(ByVal vclsCustomerDebt As clsCustomerDebt)
    MyBase.New()
    CreateEmpty()
    AssignValues(vclsCustomerDebt) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vCustomerID As Long = 0 _ 
    , Optional vCustomerText As String = "" _ 
    , Optional vOrderHeaderID As Long = 0 _ 
    , Optional vOrderHeaderText As String = "" _ 
    , Optional vDebtAmount As Decimal = 0 _ 
    , Optional vPaidAmount As Decimal = 0D _ 
    , Optional vRemainingAmount As Decimal = 0 _ 
    , Optional vDebtDate As Date = Nothing _ 
    , Optional vDueDate As Date = Nothing _ 
    , Optional vDebtStatus As clsEnums.enmDebtStatus = clsEnums.enmDebtStatus.Open _ 
    , Optional vDebtStatusText As String = "" _ 
    , Optional vNotes As String = "" _ 
    , Optional vNeedsAttention As Boolean = False _ 
    , Optional vProductTypes As String = "" _ 
    , Optional vDeliveryDate As Date = Nothing _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _CustomerID = vCustomerID 
    _CustomerText = vCustomerText 
    _OrderHeaderID = vOrderHeaderID 
    _OrderHeaderText = vOrderHeaderText 
    _DebtAmount = vDebtAmount 
    _PaidAmount = vPaidAmount 
    _RemainingAmount = vRemainingAmount 
    _DebtDate = vDebtDate 
    _DueDate = vDueDate 
    _DebtStatus = vDebtStatus 
    _DebtStatusText = vDebtStatusText 
    _Notes = vNotes 
    _NeedsAttention = vNeedsAttention 
    _ProductTypes = vProductTypes 
    _DeliveryDate = vDeliveryDate 
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
 
    _Notes = _Notes.Truncate(pTruncateLength, _IsTruncated) 
    _ProductTypes = _ProductTypes.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _Notes = ccHelper.RemoveChrW0(_Notes) 
    _ProductTypes = ccHelper.RemoveChrW0(_ProductTypes) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the CustomerDebt by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebt_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-CustomerDebt-151224_0844", vRequester) 
    End Try 
 
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsCustomerDebt_GetByPrimaryKey", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the CustomerDebt by the chosen parameters. This function may be a bit slower than accessing the CustomerDebt's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebt_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-CustomerDebt-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-CustomerDebt-151223_1716", vRequester)  
    End Try  
 
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsCustomerDebt_GetByParameters", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the CustomerDebt by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebt_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"CustomerDebt not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-CustomerDebt-210927-1527", vRequester, vAdditionalMessageToUser:=$"CustomerDebt not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.CustomerDebtCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerDebtGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"CustomerDebt not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-CustomerDebt-210625-0950", vRequester, vAdditionalMessageToUser:=$"CustomerDebt not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "clsCustomerDebt_GetByID", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtUpdate, "clsCustomerDebt_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-CustomerDebt-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtUpdate, "clsCustomerDebt_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-CustomerDebt-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the CustomerDebt. If there are parents or children in the CustomerDebt, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Friend Function UpdateFriend(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtUpdate, "clsCustomerDebt_UpdateFriend", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pCustomerDebt As New clsCustomerDebt(_WithParents) 
    If Me.isEqual(pCustomerDebt) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-CustomerDebt-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-CustomerDebt-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "ccCustomerDebtUpdateFriend"
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
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      Dim pCachedCustomerDebt As clsCustomerDebt 
      If _ID = 0 Then 
        pCachedCustomerDebt = New clsCustomerDebt(_WithParents) 
        'get last ID 
        Dim pCustomerDebtCol As clsCustomerDebtCol = MyController.DBCache.CustomerDebtCol.Clone() 
        If pCustomerDebtCol.Count = 0 Then 
          _ID = 1 
        Else 
          pCustomerDebtCol.SortByID() 
          Dim pLastID As Long = pCustomerDebtCol(pCustomerDebtCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.CustomerDebtCol.Add(pCachedCustomerDebt) 
      Else  
        pCachedCustomerDebt = MyController.DBCache.CustomerDebtCol.FindByID(_ID) 
      End If 
      pCachedCustomerDebt.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.CustomerDebtCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_CustomerID, False) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_OrderHeaderID, False) 
        pLastReadVariableName = "DebtAmount" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Decimal).Value = (_DebtAmount) 
        pLastReadVariableName = "PaidAmount" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Decimal).Value = (_PaidAmount) 
        pLastReadVariableName = "DebtDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_DebtDate) 
        pLastReadVariableName = "DueDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_DueDate) 
        pLastReadVariableName = "enmDebtStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_DebtStatus.FastToString()) 
        pLastReadVariableName = "Notes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes) 
        pLastReadVariableName = "blg_NeedsAttention" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (_NeedsAttention) 
        pLastReadVariableName = "ProductTypes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 500).Value = ccHelper.ObjectNullable(_ProductTypes) 
        pLastReadVariableName = "DeliveryDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_DeliveryDate) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-CustomerDebt-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-CustomerDebt-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Parents 
      Dim pCustomer As clsCustomer = _Customer 
      Dim pOrderHeader As clsOrderHeader = _OrderHeader 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Parents 
      If _WithParents <> clsEnums.enmLoadParent.EntireObject Then 
        If Not pCustomer Is Nothing Then _Customer = pCustomer 
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
  ''' This updates the CustomerDebt. If there are parents or children in the CustomerDebt, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtUpdate, "clsCustomerDebt_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pCustomerDebt As New clsCustomerDebt(_WithParents) 
    If Me.isEqual(pCustomerDebt) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-CustomerDebt-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-CustomerDebt-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "ccCustomerDebtUpdate"
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
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      Dim pCachedCustomerDebt As clsCustomerDebt 
      If _ID = 0 Then 
        pCachedCustomerDebt = New clsCustomerDebt(_WithParents) 
        'get last ID 
        Dim pCustomerDebtCol As clsCustomerDebtCol = MyController.DBCache.CustomerDebtCol.Clone() 
        If pCustomerDebtCol.Count = 0 Then 
          _ID = 1 
        Else 
          pCustomerDebtCol.SortByID() 
          Dim pLastID As Long = pCustomerDebtCol(pCustomerDebtCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.CustomerDebtCol.Add(pCachedCustomerDebt) 
      Else  
        pCachedCustomerDebt = MyController.DBCache.CustomerDebtCol.FindByID(_ID) 
      End If 
      pCachedCustomerDebt.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.CustomerDebtCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_CustomerID, False) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_OrderHeaderID, False) 
        pLastReadVariableName = "DebtAmount" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Decimal).Value = (_DebtAmount) 
        pLastReadVariableName = "PaidAmount" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Decimal).Value = (_PaidAmount) 
        pLastReadVariableName = "DebtDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_DebtDate) 
        pLastReadVariableName = "DueDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_DueDate) 
        pLastReadVariableName = "enmDebtStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (_DebtStatus.FastToString()) 
        pLastReadVariableName = "Notes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Notes) 
        pLastReadVariableName = "ProductTypes" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 500).Value = ccHelper.ObjectNullable(_ProductTypes) 
        pLastReadVariableName = "DeliveryDate" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(_DeliveryDate) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-CustomerDebt-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-CustomerDebt-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Parents 
      Dim pCustomer As clsCustomer = _Customer 
      Dim pOrderHeader As clsOrderHeader = _OrderHeader 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Parents 
      If _WithParents <> clsEnums.enmLoadParent.EntireObject Then 
        If Not pCustomer Is Nothing Then _Customer = pCustomer 
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
    Dim pFunctionParameters As String = String.Format("CustomerDebt.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtDelete, "clsCustomerDebt_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "ccCustomerDebtDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      MyController.DBCache.CustomerDebtCol.Remove(MyController.DBCache.CustomerDebtCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.CustomerDebtCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-CustomerDebt-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-CustomerDebt-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtDelete, "clsCustomerDebt_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "ccCustomerDebtDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      MyController.DBCache.CustomerDebtCol.Remove(MyController.DBCache.CustomerDebtCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.CustomerDebtCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-CustomerDebt-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-CustomerDebt-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-231207-0843", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is clsCustomerDebt) Then Return False 
    Dim pCustomerDebtToTest As clsCustomerDebt = CType(vTargCCEntityToTest, clsCustomerDebt) 
    Return isEqual(pCustomerDebtToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vCustomerDebtToTest As clsCustomerDebt) As Boolean
    With vCustomerDebtToTest
      If _ID <> .ID Then Return False
      If _CustomerID <> .CustomerID Then Return False
      If _OrderHeaderID <> .OrderHeaderID Then Return False
      If _DebtAmount <> .DebtAmount Then Return False
      If _PaidAmount <> .PaidAmount Then Return False
      If _RemainingAmount <> .RemainingAmount Then Return False
      If _DebtDate <> Nothing AndAlso .DebtDate <> Nothing Then 
        If ccHelper.ToLong(_DebtDate.Subtract(.DebtDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_DebtDate = Nothing AndAlso .DebtDate = Nothing) Then 
        Return False 
      End If 
      If _DueDate <> Nothing AndAlso .DueDate <> Nothing Then 
        If ccHelper.ToLong(_DueDate.Subtract(.DueDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_DueDate = Nothing AndAlso .DueDate = Nothing) Then 
        Return False 
      End If 
      If _DebtStatus <> .DebtStatus Then Return False
      If _Notes <> .Notes Then Return False
      If _NeedsAttention <> .NeedsAttention Then Return False
      If _ProductTypes <> .ProductTypes Then Return False
      If _DeliveryDate <> Nothing AndAlso .DeliveryDate <> Nothing Then 
        If ccHelper.ToLong(_DeliveryDate.Subtract(.DeliveryDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_DeliveryDate = Nothing AndAlso .DeliveryDate = Nothing) Then 
        Return False 
      End If 
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
    Dim pClone As New clsCustomerDebt(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsCustomerDebt
    Dim pClone As New clsCustomerDebt(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("CustomerID") = _CustomerID : Catch ex As Exception : Return pFault.LogException(ex, "CustomerID", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("OrderHeaderID") = _OrderHeaderID : Catch ex As Exception : Return pFault.LogException(ex, "OrderHeaderID", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("DebtAmount") = _DebtAmount : Catch ex As Exception : Return pFault.LogException(ex, "DebtAmount", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("PaidAmount") = _PaidAmount : Catch ex As Exception : Return pFault.LogException(ex, "PaidAmount", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("RemainingAmount") = _RemainingAmount : Catch ex As Exception : Return pFault.LogException(ex, "RemainingAmount", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("DebtDate") = _DebtDate : Catch ex As Exception : Return pFault.LogException(ex, "DebtDate", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("DueDate") = _DueDate : Catch ex As Exception : Return pFault.LogException(ex, "DueDate", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("DebtStatus") = _DebtStatus : Catch ex As Exception : Return pFault.LogException(ex, "DebtStatus", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("Notes") = _Notes : Catch ex As Exception : Return pFault.LogException(ex, "Notes", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("NeedsAttention") = _NeedsAttention : Catch ex As Exception : Return pFault.LogException(ex, "NeedsAttention", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProductTypes") = _ProductTypes : Catch ex As Exception : Return pFault.LogException(ex, "ProductTypes", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
    Try : vDataRow("DeliveryDate") = _DeliveryDate : Catch ex As Exception : Return pFault.LogException(ex, "DeliveryDate", "TRGT-CustomerDebt-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pCustomerDebt As clsCustomerDebt = CType(pXmlSerializer.Deserialize(pStreamReader), clsCustomerDebt) 
      AssignValues(pCustomerDebt) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-CustomerDebt-130515-1230", vRequester) 
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
          'DebtAmount 
          pBinaryWriter.Write(_DebtAmount) 
          'PaidAmount 
          pBinaryWriter.Write(_PaidAmount) 
          'RemainingAmount 
          pBinaryWriter.Write(_RemainingAmount) 
          'DebtDate 
          pBinaryWriter.Write(_DebtDate.Ticks) 
          'DueDate 
          pBinaryWriter.Write(_DueDate.Ticks) 
          'DebtStatus 
          pBinaryWriter.Write(_DebtStatus.FastToString()) 
          'Notes 
          If _Notes Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Notes) 
          'NeedsAttention 
          pBinaryWriter.Write(_NeedsAttention) 
          'ProductTypes 
          If _ProductTypes Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProductTypes) 
          'DeliveryDate 
          pBinaryWriter.Write(_DeliveryDate.Ticks) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-150307-2338", vRequester) 
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
          'DebtAmount 
          _DebtAmount = pReader.ReadDecimal 
          'PaidAmount 
          _PaidAmount = pReader.ReadDecimal 
          'RemainingAmount 
          _RemainingAmount = pReader.ReadDecimal 
          'DebtDate 
          _DebtDate = New Date(pReader.ReadInt64) 
          'DueDate 
          _DueDate = New Date(pReader.ReadInt64) 
          'DebtStatus 
          _DebtStatus = clsEnums.TranslateEnmDebtStatus(pReader.ReadString) 
          'Notes 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Notes = pReader.ReadString 
          'NeedsAttention 
          _NeedsAttention = pReader.ReadBoolean 
          'ProductTypes 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProductTypes = pReader.ReadString 
          'DeliveryDate 
          _DeliveryDate = New Date(pReader.ReadInt64) 
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
      rFault.LogException(ex, "", "TRGT-CustomerDebt-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-190720-1443", vRequester) 
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
 
      Dim pCustomerDebt As clsCustomerDebt = Newtonsoft.Json.JsonConvert.DeserializeObject(Of clsCustomerDebt)(vJSON, pSettings) 
      AssignValues(pCustomerDebt) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vCustomerDebt As clsCustomerDebt)
    With vCustomerDebt
      _ID = .ID 
      _CustomerID = .CustomerID 
      If .Customer IsNot Nothing Then 
        _Customer = .Customer.Clone() 
      End If 
      _CustomerText = .CustomerText 
      _OrderHeaderID = .OrderHeaderID 
      If .OrderHeader IsNot Nothing Then 
        _OrderHeader = .OrderHeader.Clone() 
      End If 
      _OrderHeaderText = .OrderHeaderText 
      _DebtAmount = .DebtAmount 
      _PaidAmount = .PaidAmount 
      _RemainingAmount = .RemainingAmount 
      _DebtDate = .DebtDate 
      _DueDate = .DueDate 
      _DebtStatus = .DebtStatus 
      _DebtStatusText = .DebtStatusText
      _Notes = .Notes 
      _NeedsAttention = .NeedsAttention 
      _ProductTypes = .ProductTypes 
      _DeliveryDate = .DeliveryDate 
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
      'DebtStatus 
      pTextToGet = "DebtStatusText (Enum)" 
      _DebtStatusText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.DebtStatus, _DebtStatus.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-CustomerDebt-151124-1900", vRequester) 
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
      pLastReadVariableName = "CustomerID" 
      If Not vReader.IsDBNull(1) Then _CustomerID = vReader.GetInt64(1)
      pLastReadVariableName = "OrderHeaderID" 
      If Not vReader.IsDBNull(2) Then _OrderHeaderID = vReader.GetInt64(2)
      pLastReadVariableName = "DebtAmount" 
      If Not vReader.IsDBNull(3) Then _DebtAmount = vReader.GetDecimal(3)
      pLastReadVariableName = "PaidAmount" 
      If Not vReader.IsDBNull(4) Then _PaidAmount = vReader.GetDecimal(4)
      pLastReadVariableName = "clc_RemainingAmount" 
      If Not vReader.IsDBNull(5) Then _RemainingAmount = vReader.GetDecimal(5)
      pLastReadVariableName = "DebtDate" 
      If Not vReader.IsDBNull(6) Then _DebtDate = vReader.GetDateTime(6)
      pLastReadVariableName = "DueDate" 
      If Not vReader.IsDBNull(7) Then _DueDate = vReader.GetDateTime(7)
      pLastReadVariableName = "enmDebtStatus" 
      If Not vReader.IsDBNull(8) Then _DebtStatus = clsEnums.TranslateEnmDebtStatus(vReader.GetString(8))
      pLastReadVariableName = "Notes" 
      If Not vReader.IsDBNull(9) Then _Notes = vReader.GetString(9) 
      pLastReadVariableName = "blg_NeedsAttention" 
      If Not vReader.IsDBNull(10) Then _NeedsAttention = vReader.GetBoolean(10)
      pLastReadVariableName = "ProductTypes" 
      If Not vReader.IsDBNull(11) Then _ProductTypes = vReader.GetString(11) 
      pLastReadVariableName = "DeliveryDate" 
      If Not vReader.IsDBNull(12) Then _DeliveryDate = vReader.GetDateTime(12)
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(13) Then bDateAdded = vReader.GetDateTime(13)   
      If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
        pLastReadVariableName = "CustomerText" 
        If Not vReader.IsDBNull(14) Then _CustomerText = vReader.GetString(14) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedCustomerDebt As clsCustomerDebt, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pWithParents As clsEnums.enmLoadParent = _WithParents 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedCustomerDebt) 
      If pWithParents = clsEnums.enmLoadParent.DoNotLoad Then 
        _CustomerText = "."
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _CustomerID = 0
    _Customer = Nothing
    _CustomerText = "."
    _OrderHeaderID = 0
    _OrderHeader = Nothing
    _OrderHeaderText = "."
    _DebtAmount = 0
    'Default Value set by SQL Server Database (below): 0D
    _PaidAmount = 0D
    _RemainingAmount = 0
    'Default Value set by SQL Server Database (below): etdate(
    _DebtDate = Nothing
    _DueDate = Nothing
    'Default Value set by SQL Server Database (below): Open
    _DebtStatus = clsEnums.enmDebtStatus.Open
    _DebtStatusText = ""
    _Notes = ""
    'Default Value set by SQL Server Database (below): 0
    _NeedsAttention = False
    _ProductTypes = ""
    _DeliveryDate = Nothing
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
  
Public Class clsCustomerDebtCol
  Inherits cTargCCCollection(Of clsCustomerDebt)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, clsCustomerDebt) 
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
 
    For Each pRow As clsCustomerDebt In Me 
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
    pCSVTitle.Append(",""CustomerID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Customer (Text)""") 
    pCSVTitle.Append(",""OrderHeaderID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""OrderHeader (Text)""") 
    pCSVTitle.Append(",""DebtAmount""") 
    pCSVTitle.Append(",""PaidAmount""") 
    pCSVTitle.Append(",""RemainingAmount""") 
    pCSVTitle.Append(",""DebtDate""") 
    pCSVTitle.Append(",""DueDate""") 
    pCSVTitle.Append(",""DebtStatus" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""DebtStatus (Text)""") 
    pCSVTitle.Append(",""Notes""") 
    pCSVTitle.Append(",""NeedsAttention""") 
    pCSVTitle.Append(",""ProductTypes""") 
    pCSVTitle.Append(",""DeliveryDate""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As clsCustomerDebt In Me 
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
 
  Public Overloads Sub Add(ByVal vCustomerDebt As clsCustomerDebt) 
    SyncLock _CollectionLock 
      MyBase.Add(vCustomerDebt) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vCustomerDebt As clsCustomerDebt) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vCustomerDebt) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vCustomerDebtCol As clsCustomerDebtCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vCustomerDebtCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vCustomerDebt As clsCustomerDebt) 
    SyncLock _CollectionLock 
      MyBase.Remove(vCustomerDebt) 
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
      Dim pTempDictionary As New Dictionary(Of Long, clsCustomerDebt) 
      
      For Each lCustomerDebt In Me 
        If lCustomerDebt.IsEmpty OrElse pTempDictionary.ContainsKey(lCustomerDebt.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lCustomerDebt.ID, lCustomerDebt) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lCustomerDebt.ToString, "TRGT-CustomerDebt-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", CustomerDebt:" & lCustomerDebt.ToString() & ", TRGT-CustomerDebt-260111-154657") 'Send it up the line 
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
 
    For Each lCustomerDebt As clsCustomerDebt In Me 
      lCustomerDebt.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lCustomerDebt As clsCustomerDebt In Me 
      lCustomerDebt.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [CustomerID] 
    [OrderHeaderID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the CustomerDebts by the chosen parameters. This function may be a bit slower than accessing the CustomerDebt's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.CustomerID 
          pFault = FillByCustomerID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OrderHeaderID 
          pFault = FillByOrderHeaderID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-CustomerDebt-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-CustomerDebt-151223_1716", vRequester) 
    End Try 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_FillByParameters", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      Dim pCustomerDebtsCached As clsCustomerDebtCol = MyController.DBCache.CustomerDebtCol.Clone() 
      pCustomerDebtsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pCustomerDebtsCached.Reverse() 
      If vHowMany > 0 AndAlso pCustomerDebtsCached.Count > vHowMany Then 
        Dim tmp As New clsCustomerDebtCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pCustomerDebtsCached(i)) 
        Next 
        pCustomerDebtsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pCustomerDebtsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerDebtsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillByCustomerID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      Dim pCustomerDebtsCached As clsCustomerDebtCol = MyController.DBCache.CustomerDebtCol.CloneByCustomerID(vCustomerID)
      pFault = LoadMeFromDBCache(pCustomerDebtsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerDebtsFillByCustomerID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_FillByCustomerID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillByOrderHeaderID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      Dim pCustomerDebtsCached As clsCustomerDebtCol = MyController.DBCache.CustomerDebtCol.CloneByOrderHeaderID(vOrderHeaderID)
      pFault = LoadMeFromDBCache(pCustomerDebtsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerDebtsFillByOrderHeaderID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(vOrderHeaderID, False) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_FillByOrderHeaderID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.CustomerDebtCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.CustomerDebtCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load clsCustomerDebtCol failed: " & pResponse) 
      Dim pCustomerDebtsCached As clsCustomerDebtCol = MyController.DBCache.CustomerDebtCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pCustomerDebtsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerDebtsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_FillByBoundedID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lCustomerDebt As New clsCustomerDebt() 
      pFault = lCustomerDebt.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lCustomerDebt.IsEmpty Then Me.Add(lCustomerDebt) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
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
    [CustomerID]
    [OrderHeaderID]
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pCustomerID As Nullable(Of Long) = Nothing
    Dim pOrderHeaderID As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerID) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerID) : If pObj IsNot Nothing Then pCustomerID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderID) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderID) : If pObj IsNot Nothing Then pOrderHeaderID = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pCustomerID _
        , pOrderHeaderID _
        , vRequester, pHowMany, pDir) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
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
        , ByVal vCustomerID As Nullable(Of Long) _
        , ByVal vOrderHeaderID As Nullable(Of Long) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, CustomerID={2}, OrderHeaderID={3}", vIDFrom, vIDTo, vCustomerID, vOrderHeaderID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-CustomerDebt-121122-2008", vRequester) 
      Dim pCustomerDebtsCached As clsCustomerDebtCol = MyController.DBCache.CustomerDebtCol.Clone() 
      Dim pCustomerDebtsToUse As New clsCustomerDebtCol() 
      For Each l In pCustomerDebtsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vCustomerID.HasValue Then 
          If l.CustomerID <> vCustomerID.Value Then Continue For 
        End If 
        If vOrderHeaderID.HasValue Then 
          If l.OrderHeaderID <> vOrderHeaderID.Value Then Continue For 
        End If 
        pCustomerDebtsToUse.Add(l) 
      Next 
      pCustomerDebtsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pCustomerDebtsToUse.Reverse() 
      If vHowMany > 0 AndAlso pCustomerDebtsToUse.Count > vHowMany Then 
        Dim tmp As New clsCustomerDebtCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pCustomerDebtsToUse(i)) 
        Next 
        pCustomerDebtsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pCustomerDebtsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerDebtsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vCustomerID) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOrderHeaderID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByCustomerID
    GroupByOrderHeaderID
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pCustomerID As Nullable(Of Long) = Nothing
    Dim pOrderHeaderID As Nullable(Of Long) = Nothing
    Dim pGroupByCustomerID As Boolean = False
    Dim pGroupByOrderHeaderID As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CustomerID) Then pObj = vParameters(enmFillOnTheFlyParameters.CustomerID) : If pObj IsNot Nothing Then pCustomerID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OrderHeaderID) Then pObj = vParameters(enmFillOnTheFlyParameters.OrderHeaderID) : If pObj IsNot Nothing Then pOrderHeaderID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByCustomerID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByCustomerID) : If pObj IsNot Nothing Then pGroupByCustomerID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOrderHeaderID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOrderHeaderID) : If pObj IsNot Nothing Then pGroupByOrderHeaderID = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pCustomerID _
        , pOrderHeaderID _
        , pGroupByCustomerID _
        , pGroupByOrderHeaderID _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
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
        , ByVal vCustomerID As Nullable(Of Long) _
        , ByVal vOrderHeaderID As Nullable(Of Long) _
        , ByVal vGroupByCustomerID As Boolean _
        , ByVal vGroupByOrderHeaderID As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, CustomerID={2}, OrderHeaderID={3}, GroupByCustomerID={4}, GroupByOrderHeaderID={5}", vIDFrom, vIDTo, vCustomerID, vOrderHeaderID, vGroupByCustomerID, vGroupByOrderHeaderID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-CustomerDebt-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "ccCustomerDebtsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vCustomerID) 
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOrderHeaderID) 
        pLastReadVariableName = "CustomerID" 
        pDALParameters.Add("GroupByCustomerID", ccDAL.enmSQLDataType.Bit).Value = vGroupByCustomerID
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add("GroupByOrderHeaderID", ccDAL.enmSQLDataType.Bit).Value = vGroupByOrderHeaderID
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pCustomerDebts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pCustomerDebts, "clsCustomerDebtCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pCustomerDebts IsNot Nothing AndAlso Me.Count <> pCustomerDebts.Count Then FillFromListOfITargCCEntity(pCustomerDebts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vCustomerDebtArray As clsCustomerDebt())
    Me.Clear()
    
    For Each pCustomerDebt As clsCustomerDebt In vCustomerDebtArray
      Me.Add(pCustomerDebt)
      _Clean.Add(pCustomerDebt.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pCustomerDebt As New clsCustomerDebt(pRow, vRequester, _WithParents) 
        Me.Add(pCustomerDebt) 
        _Clean.Add(pCustomerDebt.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-CustomerDebtCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-130515-1300", vRequester) 
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
      Dim pCustomerDebts As clsCustomerDebtCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsCustomerDebtCol) 
      For Each pCustomerDebt As clsCustomerDebt In pCustomerDebts 
        Me.Add(pCustomerDebt) 
        _Clean.Add(pCustomerDebt.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-CustomerDebt-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-190720-1443", vRequester) 
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
 
      Dim pCustomerDebts As List(Of clsCustomerDebt) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of clsCustomerDebt))(vJSON, pSettings) 
      For Each pCustomerDebt As clsCustomerDebt In pCustomerDebts 
        Me.Add(pCustomerDebt) 
        _Clean.Add(pCustomerDebt.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-190720-2059", vRequester) 
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
          For Each lCustomerDebt As clsCustomerDebt In Me 
            Dim pByte As Byte() = lCustomerDebt.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-150307-2340", vRequester) 
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
            Dim pCustomerDebt As clsCustomerDebt = New clsCustomerDebt(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pCustomerDebt) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pCustomerDebt.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-CustomerDebt-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pCustomerDebt As clsCustomerDebt In Me 
      With pCustomerDebt 
        pFault = pCustomerDebt.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is clsCustomerDebtCol) Then Return False 
    Dim pCustomerDebtColToTest As clsCustomerDebtCol = CType(vEntitiesToTest, clsCustomerDebtCol) 
    Return isEqual(pCustomerDebtColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vCustomerDebtsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vCustomerDebtsToTest As clsCustomerDebtCol) As Boolean
    If Me.Count <> vCustomerDebtsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vCustomerDebtsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pCustomerDebts._FilledFromSumOnTheFly = True
    
    For Each pCustomerDebt As clsCustomerDebt In Me 
      Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone() 
      pCustomerDebts.Add(pCustomerDebtClone) 
      If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
    Next 
    Return pCustomerDebts 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As clsCustomerDebtCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pCustomerDebts._FilledFromSumOnTheFly = True
    
    For Each pCustomerDebt As clsCustomerDebt In Me
      Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
      pCustomerDebts.Add(pCustomerDebtClone)
      If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
    Next
    Return pCustomerDebts
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As clsCustomerDebtCol 
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents)  
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pCustomerDebt As clsCustomerDebt In _SortedDictionaryForFindByID.Values.ToList() 
      If (pCustomerDebt.ID > vIDFrom AndAlso pCustomerDebt.ID <= vIDTo) Then 
        Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone() 
        pCustomerDebts.Add(pCustomerDebtClone) 
        If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
      End If 
    Next 
    Return pCustomerDebts 
  End Function 
  
  ''' <summary>
  ''' This loads the dependant parents for each of the rows 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault
    For Each pCustomerDebt As clsCustomerDebt In Me
      pFault = pCustomerDebt.LoadParents(vRequester)
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
  Public Function FindByID(ByVal vID As Long) As clsCustomerDebt
    If Me.Count = 0 Then Return New clsCustomerDebt 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
    
    Dim pCustomerDebt As clsCustomerDebt = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pCustomerDebt) 
    If pCustomerDebt IsNot Nothing Then Return pCustomerDebt Else Return New clsCustomerDebt() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CustomerID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCustomerID(ByVal vCustomerID As Long) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.CustomerID = vCustomerID Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCustomerID with vCustomerID of {vCustomerID}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.CustomerID = vCustomerID Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OrderHeaderID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOrderHeaderID(ByVal vOrderHeaderID As Long) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.OrderHeaderID = vOrderHeaderID Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOrderHeaderID with vOrderHeaderID of {vOrderHeaderID}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.OrderHeaderID = vOrderHeaderID Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DebtAmount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDebtAmount(ByVal vDebtAmount As Decimal) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.DebtAmount = vDebtAmount Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDebtAmount with vDebtAmount of {vDebtAmount}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.DebtAmount = vDebtAmount Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PaidAmount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPaidAmount(ByVal vPaidAmount As Decimal) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.PaidAmount = vPaidAmount Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPaidAmount with vPaidAmount of {vPaidAmount}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.PaidAmount = vPaidAmount Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RemainingAmount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRemainingAmount(ByVal vRemainingAmount As Decimal) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.RemainingAmount = vRemainingAmount Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRemainingAmount with vRemainingAmount of {vRemainingAmount}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.RemainingAmount = vRemainingAmount Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DebtDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDebtDate(ByVal vDebtDate As Date) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.DebtDate = vDebtDate Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDebtDate with vDebtDate of {vDebtDate}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.DebtDate = vDebtDate Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DueDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDueDate(ByVal vDueDate As Date) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.DueDate = vDueDate Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDueDate with vDueDate of {vDueDate}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.DueDate = vDueDate Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DebtStatus
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDebtStatus(ByVal vDebtStatus As clsEnums.enmDebtStatus) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.DebtStatus = vDebtStatus Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDebtStatus with vDebtStatus of {vDebtStatus}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.DebtStatus = vDebtStatus Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Notes
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNotes(ByVal vNotes As String) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNotes = vNotes.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.Notes.ToLowerInvariant() = vNotes Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNotes with vNotes of {vNotes}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.Notes.ToLowerInvariant() = vNotes Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined NeedsAttention
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNeedsAttention(ByVal vNeedsAttention As Boolean) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.NeedsAttention = vNeedsAttention Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNeedsAttention with vNeedsAttention of {vNeedsAttention}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.NeedsAttention = vNeedsAttention Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProductTypes
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProductTypes(ByVal vProductTypes As String) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vProductTypes = vProductTypes.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.ProductTypes.ToLowerInvariant() = vProductTypes Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByProductTypes with vProductTypes of {vProductTypes}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.ProductTypes.ToLowerInvariant() = vProductTypes Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DeliveryDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDeliveryDate(ByVal vDeliveryDate As Date) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.DeliveryDate = vDeliveryDate Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDeliveryDate with vDeliveryDate of {vDeliveryDate}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.DeliveryDate = vDeliveryDate Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As clsCustomerDebtCol
    Dim pCustomerDebts As New clsCustomerDebtCol(_WithParents) 
    pCustomerDebts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, clsCustomerDebt) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pCustomerDebt As clsCustomerDebt In pTempDist.Values
        If pCustomerDebt.Tag.ToLowerInvariant() = vTag Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As clsCustomerDebtCol = Me.Clone() 
      For Each pCustomerDebt As clsCustomerDebt In pList 
        If pCustomerDebt.Tag.ToLowerInvariant() = vTag Then
          Dim pCustomerDebtClone As clsCustomerDebt = pCustomerDebt.Clone()
          pCustomerDebts.Add(pCustomerDebtClone)
          If Not _FilledFromSumOnTheFly Then pCustomerDebts._Clean.Add(pCustomerDebt.ID) 
        End If
      Next
    End If 
    
    Return pCustomerDebts
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
    For Each pCustomerDebt As clsCustomerDebt In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pCustomerDebt.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtView, "clsCustomerDebtCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As clsCustomerDebt In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As clsCustomerDebt = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pCustomerDebtToKill As New clsCustomerDebt 
          pCustomerDebtToKill.ID = pCleanID 
          pCustomerDebtToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pCustomerDebtToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As clsCustomerDebt In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-CustomerDebt-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtUpdate, "clsCustomerDebtCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As clsCustomerDebt In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As clsCustomerDebt In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtDelete, "clsCustomerDebtCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccCustomerDebtsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New clsCustomerDebtCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-CustomerDebt-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-CustomerDebt-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt--090624-1625", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtDelete, "clsCustomerDebtCol_DeleteByCustomerID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccCustomerDebtsDeleteByCustomerID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllCustomerDebts As New clsCustomerDebtCol() : pAllCustomerDebts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredCustomerDebts As clsCustomerDebtCol = pAllCustomerDebts.CloneByCustomerID(vCustomerID) 
      For Each l In pFilteredCustomerDebts 
        pAllCustomerDebts.Remove(pAllCustomerDebts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllCustomerDebts, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-CustomerDebt-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-CustomerDebt-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtDelete, "clsCustomerDebtCol_DeleteByOrderHeaderID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccCustomerDebtsDeleteByOrderHeaderID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllCustomerDebts As New clsCustomerDebtCol() : pAllCustomerDebts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredCustomerDebts As clsCustomerDebtCol = pAllCustomerDebts.CloneByOrderHeaderID(vOrderHeaderID) 
      For Each l In pFilteredCustomerDebts 
        pAllCustomerDebts.Remove(pAllCustomerDebts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllCustomerDebts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OrderHeaderID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vOrderHeaderID) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-CustomerDebt-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-CustomerDebt-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_CustomerDebtDelete, "clsCustomerDebtCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "ccCustomerDebtsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-CustomerDebt-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-CustomerDebt-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-CustomerDebt-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-090210-1341", vRequester) 
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
    Me.Sort(New clsCustomerDebtCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
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
  
  Public Sub SortByCustomerID()
    Me.Sort(New clsCustomerDebtCol.CompareByCustomerID)
  End Sub
  Private Class CompareByCustomerID
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
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
    Me.Sort(New clsCustomerDebtCol.CompareByCustomerText)
  End Sub
  Private Class CompareByCustomerText
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CustomerText, y.CustomerText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOrderHeaderID()
    Me.Sort(New clsCustomerDebtCol.CompareByOrderHeaderID)
  End Sub
  Private Class CompareByOrderHeaderID
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
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
    Me.Sort(New clsCustomerDebtCol.CompareByOrderHeaderText)
  End Sub
  Private Class CompareByOrderHeaderText
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OrderHeaderText, y.OrderHeaderText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDebtAmount()
    Me.Sort(New clsCustomerDebtCol.CompareByDebtAmount)
  End Sub
  Private Class CompareByDebtAmount
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DebtAmount < y.DebtAmount Then
        Return -1
      ElseIf x.DebtAmount = y.DebtAmount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByPaidAmount()
    Me.Sort(New clsCustomerDebtCol.CompareByPaidAmount)
  End Sub
  Private Class CompareByPaidAmount
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.PaidAmount < y.PaidAmount Then
        Return -1
      ElseIf x.PaidAmount = y.PaidAmount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByRemainingAmount()
    Me.Sort(New clsCustomerDebtCol.CompareByRemainingAmount)
  End Sub
  Private Class CompareByRemainingAmount
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.RemainingAmount < y.RemainingAmount Then
        Return -1
      ElseIf x.RemainingAmount = y.RemainingAmount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDebtDate()
    Me.Sort(New clsCustomerDebtCol.CompareByDebtDate)
  End Sub
  Private Class CompareByDebtDate
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DebtDate < y.DebtDate Then
        Return -1
      ElseIf x.DebtDate = y.DebtDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDueDate()
    Me.Sort(New clsCustomerDebtCol.CompareByDueDate)
  End Sub
  Private Class CompareByDueDate
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DueDate < y.DueDate Then
        Return -1
      ElseIf x.DueDate = y.DueDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDebtStatus()
    Me.Sort(New clsCustomerDebtCol.CompareByDebtStatus)
  End Sub
  Private Class CompareByDebtStatus
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DebtStatus < y.DebtStatus Then
        Return -1
      ElseIf x.DebtStatus = y.DebtStatus Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDebtStatusText()
    Me.Sort(New clsCustomerDebtCol.CompareByDebtStatusText)
  End Sub
  Private Class CompareByDebtStatusText
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DebtStatusText, y.DebtStatusText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNotes()
    Me.Sort(New clsCustomerDebtCol.CompareByNotes)
  End Sub
  Private Class CompareByNotes
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Notes, y.Notes, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNeedsAttention()
    Me.Sort(New clsCustomerDebtCol.CompareByNeedsAttention)
  End Sub
  Private Class CompareByNeedsAttention
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.NeedsAttention.ToString, y.NeedsAttention.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProductTypes()
    Me.Sort(New clsCustomerDebtCol.CompareByProductTypes)
  End Sub
  Private Class CompareByProductTypes
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProductTypes, y.ProductTypes, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDeliveryDate()
    Me.Sort(New clsCustomerDebtCol.CompareByDeliveryDate)
  End Sub
  Private Class CompareByDeliveryDate
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
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
  
  Public Sub SortByTag()
    Me.Sort(New clsCustomerDebtCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of clsCustomerDebt)
    Private Function Compare(ByVal x As clsCustomerDebt, ByVal y As clsCustomerDebt) As Integer Implements System.Collections.Generic.IComparer(Of clsCustomerDebt).Compare
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
  
    Dim pCustomerDebt As clsCustomerDebt
  
    While vReader.Read()
      pCustomerDebt = New clsCustomerDebt(_WithParents) 
      pFault = pCustomerDebt.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pCustomerDebt)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pCustomerDebt.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedCustomerDebtCol As clsCustomerDebtCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pCustomerDebt As clsCustomerDebt 
 
      For Each pCachedCustomerDebt As clsCustomerDebt In vCachedCustomerDebtCol 
        pCachedCustomerDebt.SetWithParents(_WithParents) 
        pCustomerDebt = New clsCustomerDebt(pCachedCustomerDebt) 
        If _WithParents = clsEnums.enmLoadParent.DoNotLoad Then 
          pCustomerDebt.CustomerText = "." 
          pCustomerDebt.OrderHeaderText = "." 
        End If 
        pCustomerDebt.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pCustomerDebt) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pCustomerDebt.ID) 
      Next 
      If _WithParents = clsEnums.enmLoadParent.EntireObject Then 
        pFault = LoadParents(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-CustomerDebt-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsCustomerDebt) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, clsCustomerDebt) 
 
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
  
