Public Class csMail
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
    [MessagingMode] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [MessagingMode] 
    [RecipientEmail] 
    [WhenSent] 
    [Subject] 
    [Body] 
    [WhenSeen] 
    [WasSeen] 
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
  
  
  Private _IsCleanForXML As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
  
  
  Private _ID As Long
  Private _MessagingMode As clsEnums.enmMessagingMode
  Private _MessagingModeText As String 
  Private _RecipientEmail As String
  Private _WhenSent As DateTimeOffset
  Private _Subject As String
  Private _Body As String
  Private _WhenSeen As DateTimeOffset
  Private _WasSeen As Boolean
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
  Public Property [MessagingMode]() As clsEnums.enmMessagingMode
    Get
      Return Me._MessagingMode
    End Get
    Set(ByVal value As clsEnums.enmMessagingMode)
      If Me._MessagingMode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._MessagingMode = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [MessagingModeText]() As String
    Get
      Return Me._MessagingModeText
    End Get
    Set(ByVal value As String)
      Me._MessagingModeText = value
    End Set
  End Property
  Public Property [RecipientEmail]() As String
    Get
      Return Me._RecipientEmail
    End Get
    Set(ByVal value As String)
      If Me._RecipientEmail <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RecipientEmail = value 
      End If 
    End Set
  End Property
  Public Property [WhenSent]() As DateTimeOffset
    Get
      Return Me._WhenSent
    End Get
    Set(ByVal value As DateTimeOffset)
      If Me._WhenSent <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._WhenSent = value 
      End If 
    End Set
  End Property
  Public Property [Subject]() As String
    Get
      Return Me._Subject
    End Get
    Set(ByVal value As String)
      If Me._Subject <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Subject = value 
      End If 
    End Set
  End Property
  Public Property [Body]() As String
    Get
      Return Me._Body
    End Get
    Set(ByVal value As String)
      If Me._Body <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Body = value 
      End If 
    End Set
  End Property
  Public Property [WhenSeen]() As DateTimeOffset
    Get
      Return Me._WhenSeen
    End Get
    Set(ByVal value As DateTimeOffset)
      If Me._WhenSeen <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._WhenSeen = value 
      End If 
    End Set
  End Property
  Public Property [WasSeen]() As Boolean
    Get
      Return Me._WasSeen
    End Get
    Set(ByVal value As Boolean)
      If Me._WasSeen <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._WasSeen = value 
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
    If _MessagingMode <> clsEnums.enmMessagingMode.UD Then pValue.Append("MessagingMode='" & _MessagingMode.FastToString() & "' ‡ ") 
    If _MessagingModeText <> "" Then pValue.Append("MessagingModeText='" & _MessagingModeText & "' ‡ ") 
    If _RecipientEmail <> "" Then pValue.Append("RecipientEmail='" & _RecipientEmail & "' ‡ ") 
    If Not (_WhenSent = Nothing) Then pValue.Append("WhenSent='" & _WhenSent.ToString("o") & "' ‡ ") 
    If _Subject <> "" Then pValue.Append("Subject='" & _Subject & "' ‡ ") 
    If _Body <> "" Then pValue.Append("Body='" & _Body & "' ‡ ") 
    If Not (_WhenSeen = Nothing) Then pValue.Append("WhenSeen='" & _WhenSeen.ToString("o") & "' ‡ ") 
    pValue.Append("WasSeen='" & _WasSeen.ToString() & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_MessagingMode.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_MessagingModeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_RecipientEmail)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_WhenSent.DateTime.ToShortDateString & " " & _WhenSent.DateTime.ToShortTimeString & " " & _WhenSent.Offset.TotalMinutes)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Subject)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Body)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_WhenSeen.DateTime.ToShortDateString & " " & _WhenSeen.DateTime.ToShortTimeString & " " & _WhenSeen.Offset.TotalMinutes)}""") 
    pCSV.Append(",""" & _WasSeen.ToString() & """") 
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
  
  Public Sub New(ByVal vcsMail As csMail)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsMail) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vMessagingMode As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD _ 
    , Optional vMessagingModeText As String = "" _ 
    , Optional vRecipientEmail As String = "" _ 
    , Optional vWhenSent As DateTimeOffset = Nothing _ 
    , Optional vSubject As String = "" _ 
    , Optional vBody As String = "" _ 
    , Optional vWhenSeen As DateTimeOffset = Nothing _ 
    , Optional vWasSeen As Boolean = False _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _MessagingMode = vMessagingMode 
    _MessagingModeText = vMessagingModeText 
    _RecipientEmail = vRecipientEmail 
    _WhenSent = vWhenSent 
    _Subject = vSubject 
    _Body = vBody 
    _WhenSeen = vWhenSeen 
    _WasSeen = vWasSeen 
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
 
    _RecipientEmail = _RecipientEmail.Truncate(pTruncateLength, _IsTruncated) 
    _Subject = _Subject.Truncate(pTruncateLength, _IsTruncated) 
    _Body = _Body.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _RecipientEmail = ccHelper.RemoveChrW0(_RecipientEmail) 
    _Subject = ccHelper.RemoveChrW0(_Subject) 
    _Body = ccHelper.RemoveChrW0(_Body) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Mail by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMail_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Mail-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Mail by the chosen parameters. This function may be a bit slower than accessing the Mail's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMail_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Mail-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Mail-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the Mail by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMail_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"Mail not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-Mail-210927-1527", vRequester, vAdditionalMessageToUser:=$"Mail not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccMailCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"Mail not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-Mail-210625-0950", vRequester, vAdditionalMessageToUser:=$"Mail not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailUpdate, "csMail_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Mail-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailUpdate, "csMail_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Mail-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the Mail. If there are parents or children in the Mail, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailUpdate, "csMail_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pMail As New csMail() 
    If Me.isEqual(pMail) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-Mail-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Mail-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_MailUpdate"
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
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      Dim pCachedMail As csMail 
      If _ID = 0 Then 
        pCachedMail = New csMail() 
        'get last ID 
        Dim pMailCol As csMailCol = MyController.DBCache.ccMailCol.Clone() 
        If pMailCol.Count = 0 Then 
          _ID = 1 
        Else 
          pMailCol.SortByID() 
          Dim pLastID As Long = pMailCol(pMailCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccMailCol.Add(pCachedMail) 
      Else  
        pCachedMail = MyController.DBCache.ccMailCol.FindByID(_ID) 
      End If 
      pCachedMail.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccMailCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_MessagingMode.FastToString()) 
        pLastReadVariableName = "RecipientEmail" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_RecipientEmail) 
        pLastReadVariableName = "WhenSent" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTimeOffset, 7).Value = ccHelper.DateTimeOffsetNullable(_WhenSent) 
        pLastReadVariableName = "Subject" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_Subject) 
        pLastReadVariableName = "Body" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Body) 
        pLastReadVariableName = "WhenSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTimeOffset, 7).Value = ccHelper.DateTimeOffsetNullable(_WhenSeen) 
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (_WasSeen) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090623-1809", vRequester) 
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
    Dim pFunctionParameters As String = String.Format("Mail.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMail_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_MailDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      MyController.DBCache.ccMailCol.Remove(MyController.DBCache.ccMailCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccMailCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = _ID
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMail_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_MailDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      MyController.DBCache.ccMailCol.Remove(MyController.DBCache.ccMailCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccMailCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = vID 
        pLastReadVariableName = "" 
 
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
 
        'I expected to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-231207-0843", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is csMail) Then Return False 
    Dim pMailToTest As csMail = CType(vTargCCEntityToTest, csMail) 
    Return isEqual(pMailToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vMailToTest As csMail) As Boolean
    With vMailToTest
      If _ID <> .ID Then Return False
      If _MessagingMode <> .MessagingMode Then Return False
      If _RecipientEmail <> .RecipientEmail Then Return False
      If _WhenSent <> Nothing AndAlso .WhenSent <> Nothing Then 
        If ccHelper.ToLong(_WhenSent.Subtract(.WhenSent).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_WhenSent = Nothing AndAlso .WhenSent = Nothing) Then 
        Return False 
      End If 
      If _Subject <> .Subject Then Return False
      If _Body <> .Body Then Return False
      If _WhenSeen <> Nothing AndAlso .WhenSeen <> Nothing Then 
        If ccHelper.ToLong(_WhenSeen.Subtract(.WhenSeen).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_WhenSeen = Nothing AndAlso .WhenSeen = Nothing) Then 
        Return False 
      End If 
      If _WasSeen <> .WasSeen Then Return False
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
    Dim pClone As New csMail(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csMail
    Dim pClone As New csMail(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-Mail-130316-0852", vRequester) : End Try 
    Try : vDataRow("MessagingMode") = _MessagingMode : Catch ex As Exception : Return pFault.LogException(ex, "MessagingMode", "TRGT-Mail-130316-0852", vRequester) : End Try 
    Try : vDataRow("RecipientEmail") = _RecipientEmail : Catch ex As Exception : Return pFault.LogException(ex, "RecipientEmail", "TRGT-Mail-130316-0852", vRequester) : End Try 
    Try : vDataRow("WhenSent") = _WhenSent : Catch ex As Exception : Return pFault.LogException(ex, "WhenSent", "TRGT-Mail-130316-0852", vRequester) : End Try 
    Try : vDataRow("Subject") = _Subject : Catch ex As Exception : Return pFault.LogException(ex, "Subject", "TRGT-Mail-130316-0852", vRequester) : End Try 
    Try : vDataRow("Body") = _Body : Catch ex As Exception : Return pFault.LogException(ex, "Body", "TRGT-Mail-130316-0852", vRequester) : End Try 
    Try : vDataRow("WhenSeen") = _WhenSeen : Catch ex As Exception : Return pFault.LogException(ex, "WhenSeen", "TRGT-Mail-130316-0852", vRequester) : End Try 
    Try : vDataRow("WasSeen") = _WasSeen : Catch ex As Exception : Return pFault.LogException(ex, "WasSeen", "TRGT-Mail-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pMail As csMail = CType(pXmlSerializer.Deserialize(pStreamReader), csMail) 
      AssignValues(pMail) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-Mail-130515-1230", vRequester) 
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
          'MessagingMode 
          pBinaryWriter.Write(_MessagingMode.FastToString()) 
          'RecipientEmail 
          If _RecipientEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_RecipientEmail) 
          'WhenSent 
          pBinaryWriter.Write(_WhenSent.DateTime.Ticks) 
          pBinaryWriter.Write(_WhenSent.Offset.Ticks) 
          'Subject 
          If _Subject Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Subject) 
          'Body 
          If _Body Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Body) 
          'WhenSeen 
          pBinaryWriter.Write(_WhenSeen.DateTime.Ticks) 
          pBinaryWriter.Write(_WhenSeen.Offset.Ticks) 
          'WasSeen 
          pBinaryWriter.Write(_WasSeen) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Mail-150307-2338", vRequester) 
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
          'MessagingMode 
          _MessagingMode = clsEnums.TranslateEnmMessagingMode(pReader.ReadString) 
          'RecipientEmail 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _RecipientEmail = pReader.ReadString 
          'WhenSent 
          _WhenSent = New DateTimeOffset(pReader.ReadInt64, New TimeSpan(pReader.ReadInt64)) 
          'Subject 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Subject = pReader.ReadString 
          'Body 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Body = pReader.ReadString 
          'WhenSeen 
          _WhenSeen = New DateTimeOffset(pReader.ReadInt64, New TimeSpan(pReader.ReadInt64)) 
          'WasSeen 
          _WasSeen = pReader.ReadBoolean 
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
      rFault.LogException(ex, "", "TRGT-Mail-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-190720-1443", vRequester) 
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
 
      Dim pMail As csMail = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csMail)(vJSON, pSettings) 
      AssignValues(pMail) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vMail As csMail)
    With vMail
      _ID = .ID 
      _MessagingMode = .MessagingMode 
      _MessagingModeText = .MessagingModeText
      _RecipientEmail = .RecipientEmail 
      _WhenSent = .WhenSent 
      _Subject = .Subject 
      _Body = .Body 
      _WhenSeen = .WhenSeen 
      _WasSeen = .WasSeen 
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
      'MessagingMode 
      pTextToGet = "MessagingModeText (Enum)" 
      _MessagingModeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.MessagingMode, _MessagingMode.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-Mail-151124-1900", vRequester) 
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
      pLastReadVariableName = "enmMessagingMode" 
      If Not vReader.IsDBNull(1) Then _MessagingMode = clsEnums.TranslateEnmMessagingMode(vReader.GetString(1))
      pLastReadVariableName = "RecipientEmail" 
      If Not vReader.IsDBNull(2) Then _RecipientEmail = vReader.GetString(2) 
      pLastReadVariableName = "WhenSent" 
      If Not vReader.IsDBNull(3) Then _WhenSent = CType(vReader(3), DateTimeOffset)
      pLastReadVariableName = "Subject" 
      If Not vReader.IsDBNull(4) Then _Subject = vReader.GetString(4) 
      pLastReadVariableName = "Body" 
      If Not vReader.IsDBNull(5) Then _Body = vReader.GetString(5) 
      pLastReadVariableName = "WhenSeen" 
      If Not vReader.IsDBNull(6) Then _WhenSeen = CType(vReader(6), DateTimeOffset)
      pLastReadVariableName = "WasSeen" 
      If Not vReader.IsDBNull(7) Then _WasSeen = vReader.GetBoolean(7)
      bDateAdded = Nothing 
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedMail As csMail, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedMail) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _MessagingMode = clsEnums.enmMessagingMode.UD
    _MessagingModeText = ""
    _RecipientEmail = ""
    _WhenSent = Nothing
    _Subject = ""
    _Body = ""
    _WhenSeen = Nothing
    'Default Value set by SQL Server Database (below): 0
    _WasSeen = False
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
  
Public Class csMailCol
  Inherits cTargCCCollection(Of csMail)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csMail) 
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
 
    For Each pRow As csMail In Me 
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
    pCSVTitle.Append(",""MessagingMode" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""MessagingMode (Text)""") 
    pCSVTitle.Append(",""RecipientEmail""") 
    pCSVTitle.Append(",""WhenSent""") 
    pCSVTitle.Append(",""Subject""") 
    pCSVTitle.Append(",""Body""") 
    pCSVTitle.Append(",""WhenSeen""") 
    pCSVTitle.Append(",""WasSeen""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csMail In Me 
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
 
  Public Overloads Sub Add(ByVal vMail As csMail) 
    SyncLock _CollectionLock 
      MyBase.Add(vMail) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vMail As csMail) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vMail) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vMailCol As csMailCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vMailCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vMail As csMail) 
    SyncLock _CollectionLock 
      MyBase.Remove(vMail) 
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
      Dim pTempDictionary As New Dictionary(Of Long, csMail) 
      
      For Each lMail In Me 
        If lMail.IsEmpty OrElse pTempDictionary.ContainsKey(lMail.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lMail.ID, lMail) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lMail.ToString, "TRGT-Mail-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", Mail:" & lMail.ToString() & ", TRGT-Mail-260111-154657") 'Send it up the line 
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
 
    For Each lMail As csMail In Me 
      lMail.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lMail As csMail In Me 
      lMail.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [MessagingModeAndRecipientEmail] 
    [MessagingModeAndRecipientEmailAndWasSeen] 
    [WasSeen] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the Mails by the chosen parameters. This function may be a bit slower than accessing the Mail's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.MessagingModeAndRecipientEmail 
          pFault = FillByMessagingModeAndRecipientEmail(clsEnums.TranslateEnmMessagingMode(CStr(vParameters(0))), CStr(vParameters(1)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.MessagingModeAndRecipientEmailAndWasSeen 
          pFault = FillByMessagingModeAndRecipientEmailAndWasSeen(clsEnums.TranslateEnmMessagingMode(CStr(vParameters(0))), CStr(vParameters(1)), CBool(vParameters(2)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.WasSeen 
          pFault = FillByWasSeen(CBool(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Mail-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Mail-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      Dim pMailsCached As csMailCol = MyController.DBCache.ccMailCol.Clone() 
      pMailsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pMailsCached.Reverse() 
      If vHowMany > 0 AndAlso pMailsCached.Count > vHowMany Then 
        Dim tmp As New csMailCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pMailsCached(i)) 
        Next 
        pMailsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pMailsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MessagingModeAndRecipientEmail, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmail As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmail={1}", vMessagingMode, vRecipientEmail)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillByMessagingModeAndRecipientEmail", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      Dim pMailsCached As csMailCol = MyController.DBCache.ccMailCol.CloneByMessagingModeAndRecipientEmail(vMessagingMode, vRecipientEmail)
      pFault = LoadMeFromDBCache(pMailsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFillByMessagingMode&RecipientEmail" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vMessagingMode.FastToString()) 
        pLastReadVariableName = "RecipientEmail" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmail) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MessagingModeAndRecipientEmailAndWasSeen, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmail As String, ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmail={1}, WasSeen={2}", vMessagingMode, vRecipientEmail, vWasSeen)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillByMessagingModeAndRecipientEmailAndWasSeen", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      Dim pMailsCached As csMailCol = MyController.DBCache.ccMailCol.CloneByMessagingModeAndRecipientEmailAndWasSeen(vMessagingMode, vRecipientEmail, vWasSeen)
      pFault = LoadMeFromDBCache(pMailsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFillByMessagingMode&RecipientEmail&WasSeen" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vMessagingMode.FastToString()) 
        pLastReadVariableName = "RecipientEmail" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmail) 
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (vWasSeen) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific WasSeen, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWasSeen(ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("WasSeen={0}", vWasSeen)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillByWasSeen", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      Dim pMailsCached As csMailCol = MyController.DBCache.ccMailCol.CloneByWasSeen(vWasSeen)
      pFault = LoadMeFromDBCache(pMailsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFillByWasSeen" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (vWasSeen) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      Dim pMailsCached As csMailCol = MyController.DBCache.ccMailCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pMailsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MessagingModeAndRecipientEmail, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmailFrom As String, ByVal vRecipientEmailTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmailFrom={1}, RecipientEmailTo={2}", vMessagingMode, vRecipientEmailFrom, vRecipientEmailTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillByBoundedMessagingModeAndRecipientEmail", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      Dim pMailsCached As csMailCol = MyController.DBCache.ccMailCol.CloneByBoundedMessagingModeAndRecipientEmail(vMessagingMode, vRecipientEmailFrom, vRecipientEmailTo)
      pFault = LoadMeFromDBCache(pMailsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFillByBoundedMessagingMode&RecipientEmail" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vMessagingMode.FastToString()) 
        pLastReadVariableName = "RecipientEmailFrom" 
        pDALParameters.Add("bndRecipientEmailFrom", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmailFrom) 
        pLastReadVariableName = "RecipientEmailTo" 
        pDALParameters.Add("bndRecipientEmailTo", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmailTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MessagingModeAndRecipientEmailAndWasSeen, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmailFrom As String, ByVal vRecipientEmailTo As String, ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmailFrom={1}, RecipientEmailTo={2}, WasSeen={3}", vMessagingMode, vRecipientEmailFrom, vRecipientEmailTo, vWasSeen)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillByBoundedMessagingModeAndRecipientEmailAndWasSeen", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccMailCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccMailCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csMailCol failed: " & pResponse) 
      Dim pMailsCached As csMailCol = MyController.DBCache.ccMailCol.CloneByBoundedMessagingModeAndRecipientEmailAndWasSeen(vMessagingMode, vRecipientEmailFrom, vRecipientEmailTo, vWasSeen)
      pFault = LoadMeFromDBCache(pMailsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFillByBoundedMessagingMode&RecipientEmail&WasSeen" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vMessagingMode.FastToString()) 
        pLastReadVariableName = "RecipientEmailFrom" 
        pDALParameters.Add("bndRecipientEmailFrom", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmailFrom) 
        pLastReadVariableName = "RecipientEmailTo" 
        pDALParameters.Add("bndRecipientEmailTo", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmailTo) 
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (vWasSeen) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lMail As New csMail() 
      pFault = lMail.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lMail.IsEmpty Then Me.Add(lMail) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pMails As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pMails, "csMailCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pMails IsNot Nothing AndAlso Me.Count <> pMails.Count Then FillFromListOfITargCCEntity(pMails) 
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
    [MessagingMode]
    [RecipientEmail]
    RecipientEmailWildcardType
    [WasSeen]
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pMessagingMode As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD
    Dim pRecipientEmail As String = Nothing
    Dim pRecipientEmailWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pWasSeen As Nullable(Of Boolean) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MessagingMode) Then pObj = vParameters(enmFillOnTheFlyParameters.MessagingMode) : If pObj IsNot Nothing Then pMessagingMode = CType(pObj, clsEnums.enmMessagingMode) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RecipientEmail) Then pObj = vParameters(enmFillOnTheFlyParameters.RecipientEmail) : If pObj IsNot Nothing Then pRecipientEmail = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RecipientEmailWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.RecipientEmailWildcardType) : If pObj IsNot Nothing Then pRecipientEmailWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.WasSeen) Then pObj = vParameters(enmFillOnTheFlyParameters.WasSeen) : If pObj IsNot Nothing Then pWasSeen = CBool(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pMessagingMode _
        , pRecipientEmail, pRecipientEmailWildcardType _
        , pWasSeen _
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
        , ByVal vMessagingMode As clsEnums.enmMessagingMode _
        , ByVal vRecipientEmail As String, ByVal vRecipientEmailWildcardType As clsEnums.enmWildCardType _
        , ByVal vWasSeen As Nullable(Of Boolean) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, MessagingMode={2}, RecipientEmail={3}, RecipientEmailWildcardType={4}, WasSeen={5}", vIDFrom, vIDTo, vMessagingMode, vRecipientEmail, vRecipientEmailWildcardType.FastToString(), vWasSeen)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'RecipientEmail 
    Dim pWCRecipientEmail As String = "" 
    If vRecipientEmail = Nothing Then 
      pWCRecipientEmail = vRecipientEmail
    Else 
      If vRecipientEmailWildcardType = clsEnums.enmWildCardType.None OrElse vRecipientEmailWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCRecipientEmail = vRecipientEmail
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.After Then 
        pWCRecipientEmail = vRecipientEmail & "%" 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCRecipientEmail = "%" & vRecipientEmail 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCRecipientEmail = "%" & vRecipientEmail & "%" 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vRecipientEmail.ToCharArray 
          pWCRecipientEmail &= p & "%" 
        Next 
        pWCRecipientEmail = "%" & pWCRecipientEmail 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Mail-121122-2008", vRequester) 
      Dim pMailsCached As csMailCol = MyController.DBCache.ccMailCol.Clone() 
      Dim pMailsToUse As New csMailCol() 
      For Each l In pMailsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vMessagingMode <> clsEnums.enmMessagingMode.UD Then 
          If l.MessagingMode <> vMessagingMode Then Continue For 
        End If 
        If Not String.IsNullOrEmpty(vRecipientEmail) Then 
          If vRecipientEmailWildcardType = clsEnums.enmWildCardType.UD OrElse vRecipientEmailWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.RecipientEmail.Equals(vRecipientEmail, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.RecipientEmail.StartsWith(vRecipientEmail, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.RecipientEmail.EndsWith(vRecipientEmail, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.RecipientEmail.IndexOf(vRecipientEmail, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vWasSeen.HasValue Then 
          If l.WasSeen <> vWasSeen.Value Then Continue For 
        End If 
        pMailsToUse.Add(l) 
      Next 
      pMailsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pMailsToUse.Reverse() 
      If vHowMany > 0 AndAlso pMailsToUse.Count > vHowMany Then 
        Dim tmp As New csMailCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pMailsToUse(i)) 
        Next 
        pMailsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pMailsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vMessagingMode.FastToString()) 
        pLastReadVariableName = "RecipientEmail" 
        pDALParameters.Add("wldRecipientEmail", ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(pWCRecipientEmail) 
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = ccHelper.ObjectNullable(vWasSeen) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByMessagingMode
    GroupByRecipientEmail
    GroupByWasSeen
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pMessagingMode As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD
    Dim pRecipientEmail As String = Nothing
    Dim pRecipientEmailWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pWasSeen As Nullable(Of Boolean) = Nothing
    Dim pGroupByMessagingMode As Boolean = False
    Dim pGroupByRecipientEmail As Boolean = False
    Dim pGroupByWasSeen As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MessagingMode) Then pObj = vParameters(enmFillOnTheFlyParameters.MessagingMode) : If pObj IsNot Nothing Then pMessagingMode = CType(pObj, clsEnums.enmMessagingMode) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RecipientEmail) Then pObj = vParameters(enmFillOnTheFlyParameters.RecipientEmail) : If pObj IsNot Nothing Then pRecipientEmail = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RecipientEmailWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.RecipientEmailWildcardType) : If pObj IsNot Nothing Then pRecipientEmailWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.WasSeen) Then pObj = vParameters(enmFillOnTheFlyParameters.WasSeen) : If pObj IsNot Nothing Then pWasSeen = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByMessagingMode) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByMessagingMode) : If pObj IsNot Nothing Then pGroupByMessagingMode = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByRecipientEmail) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByRecipientEmail) : If pObj IsNot Nothing Then pGroupByRecipientEmail = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByWasSeen) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByWasSeen) : If pObj IsNot Nothing Then pGroupByWasSeen = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pMessagingMode _
        , pRecipientEmail, pRecipientEmailWildcardType _
        , pWasSeen _
        , pGroupByMessagingMode _
        , pGroupByRecipientEmail _
        , pGroupByWasSeen _
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
        , ByVal vMessagingMode As clsEnums.enmMessagingMode _
        , ByVal vRecipientEmail As String, ByVal vRecipientEmailWildcardType As clsEnums.enmWildCardType _
        , ByVal vWasSeen As Nullable(Of Boolean) _
        , ByVal vGroupByMessagingMode As Boolean _
        , ByVal vGroupByRecipientEmail As Boolean _
        , ByVal vGroupByWasSeen As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, MessagingMode={2}, RecipientEmail={3}, RecipientEmailWildcardType={4}, WasSeen={5}, GroupByMessagingMode={6}, GroupByRecipientEmail={7}, GroupByWasSeen={8}", vIDFrom, vIDTo, vMessagingMode, vRecipientEmail, vRecipientEmailWildcardType.FastToString(), vWasSeen, vGroupByMessagingMode, vGroupByRecipientEmail, vGroupByWasSeen)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'RecipientEmail 
    Dim pWCRecipientEmail As String = "" 
    If vRecipientEmail = Nothing Then 
      pWCRecipientEmail = vRecipientEmail
    ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.None OrElse vRecipientEmailWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCRecipientEmail = vRecipientEmail
    ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.After Then 
      pWCRecipientEmail = vRecipientEmail & "%" 
    ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCRecipientEmail = "%" & vRecipientEmail 
    ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCRecipientEmail = "%" & vRecipientEmail & "%" 
    ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vRecipientEmail.ToCharArray 
        pWCRecipientEmail &= p & "%" 
      Next 
      pWCRecipientEmail = "%" & pWCRecipientEmail 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Mail-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_MailsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vMessagingMode) 
        pLastReadVariableName = "RecipientEmail" 
        pDALParameters.Add("wldRecipientEmail", ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(pWCRecipientEmail) 
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = ccHelper.ObjectNullable(vWasSeen) 
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add("GroupByenmMessagingMode", ccDAL.enmSQLDataType.Bit).Value = vGroupByMessagingMode
        pLastReadVariableName = "RecipientEmail" 
        pDALParameters.Add("GroupByRecipientEmail", ccDAL.enmSQLDataType.Bit).Value = vGroupByRecipientEmail
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add("GroupByWasSeen", ccDAL.enmSQLDataType.Bit).Value = vGroupByWasSeen
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vMailArray As csMail())
    Me.Clear()
    
    For Each pMail As csMail In vMailArray
      Me.Add(pMail)
      _Clean.Add(pMail.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pMail As New csMail(pRow, vRequester) 
        Me.Add(pMail) 
        _Clean.Add(pMail.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-MailCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-130515-1300", vRequester) 
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
      Dim pMails As csMailCol = CType(pXmlSerializer.Deserialize(pStreamReader), csMailCol) 
      For Each pMail As csMail In pMails 
        Me.Add(pMail) 
        _Clean.Add(pMail.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-Mail-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-190720-1443", vRequester) 
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
 
      Dim pMails As List(Of csMail) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csMail))(vJSON, pSettings) 
      For Each pMail As csMail In pMails 
        Me.Add(pMail) 
        _Clean.Add(pMail.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-190720-2059", vRequester) 
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
          For Each lMail As csMail In Me 
            Dim pByte As Byte() = lMail.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Mail-150307-2340", vRequester) 
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
            Dim pMail As csMail = New csMail(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pMail) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pMail.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-Mail-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pMail As csMail In Me 
      With pMail 
        pFault = pMail.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csMailCol) Then Return False 
    Dim pMailColToTest As csMailCol = CType(vEntitiesToTest, csMailCol) 
    Return isEqual(pMailColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vMailsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vMailsToTest As csMailCol) As Boolean
    If Me.Count <> vMailsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vMailsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pMails As New csMailCol() 
    If pFilledFromSumOnTheFly Then pMails._FilledFromSumOnTheFly = True
    
    For Each pMail As csMail In Me 
      Dim pMailClone As csMail = pMail.Clone() 
      pMails.Add(pMailClone) 
      If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
    Next 
    Return pMails 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csMailCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pMails As New csMailCol() 
    If pFilledFromSumOnTheFly Then pMails._FilledFromSumOnTheFly = True
    
    For Each pMail As csMail In Me
      Dim pMailClone As csMail = pMail.Clone()
      pMails.Add(pMailClone)
      If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
    Next
    Return pMails
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csMailCol 
    Dim pMails As New csMailCol()  
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pMail As csMail In _SortedDictionaryForFindByID.Values.ToList() 
      If (pMail.ID > vIDFrom AndAlso pMail.ID <= vIDTo) Then 
        Dim pMailClone As csMail = pMail.Clone() 
        pMails.Add(pMailClone) 
        If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
      End If 
    Next 
    Return pMails 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by MessagingMode and RecipientEmail (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmailFrom As String, ByVal vRecipientEmailTo As String) As csMailCol 
    Dim pMails As New csMailCol()  
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pMail As csMail In _SortedDictionaryForFindByID.Values.ToList() 
      If (pMail.MessagingMode = vMessagingMode) AndAlso (pMail.RecipientEmail > vRecipientEmailFrom AndAlso pMail.RecipientEmail <= vRecipientEmailTo) Then 
        Dim pMailClone As csMail = pMail.Clone() 
        pMails.Add(pMailClone) 
        If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
      End If 
    Next 
    Return pMails 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by MessagingMode and RecipientEmail and WasSeen (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmailFrom As String, ByVal vRecipientEmailTo As String, ByVal vWasSeen As Boolean) As csMailCol 
    Dim pMails As New csMailCol()  
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pMail As csMail In _SortedDictionaryForFindByID.Values.ToList() 
      If (pMail.MessagingMode = vMessagingMode) AndAlso (pMail.RecipientEmail > vRecipientEmailFrom AndAlso pMail.RecipientEmail <= vRecipientEmailTo) AndAlso (pMail.WasSeen = vWasSeen) Then 
        Dim pMailClone As csMail = pMail.Clone() 
        pMails.Add(pMailClone) 
        If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
      End If 
    Next 
    Return pMails 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vMessagingModeWildcardType As clsEnums.enmWildCardType, ByVal vRecipientEmail As String, ByVal vRecipientEmailWildcardType As clsEnums.enmWildCardType) As csMailCol 
    Dim pMails As New csMailCol 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pMail As csMail In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vRecipientEmailWildcardType = clsEnums.enmWildCardType.After Then 
        If pMail.RecipientEmail.StartsWith(vRecipientEmail, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.Before Then 
        If pMail.RecipientEmail.EndsWith(vRecipientEmail, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pMail.RecipientEmail.IndexOf(vRecipientEmail, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vRecipientEmail.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pMail.RecipientEmail.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pMailClone As csMail = pMail.Clone() 
      pMails.Add(pMailClone) 
    Next 
    Return pMails 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vMessagingModeWildcardType As clsEnums.enmWildCardType, ByVal vRecipientEmail As String, ByVal vRecipientEmailWildcardType As clsEnums.enmWildCardType, ByVal vWasSeen As Boolean, ByVal vWasSeenWildcardType As clsEnums.enmWildCardType) As csMailCol 
    Dim pMails As New csMailCol 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pMail As csMail In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vRecipientEmailWildcardType = clsEnums.enmWildCardType.After Then 
        If pMail.RecipientEmail.StartsWith(vRecipientEmail, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.Before Then 
        If pMail.RecipientEmail.EndsWith(vRecipientEmail, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pMail.RecipientEmail.IndexOf(vRecipientEmail, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vRecipientEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vRecipientEmail.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pMail.RecipientEmail.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pMailClone As csMail = pMail.Clone() 
      pMails.Add(pMailClone) 
    Next 
    Return pMails 
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
  Public Function FindByID(ByVal vID As Long) As csMail
    If Me.Count = 0 Then Return New csMail 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
    
    Dim pMail As csMail = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pMail) 
    If pMail IsNot Nothing Then Return pMail Else Return New csMail() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined MessagingMode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMessagingMode(ByVal vMessagingMode As clsEnums.enmMessagingMode) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMail As csMail In pTempDist.Values
        If pMail.MessagingMode = vMessagingMode Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByMessagingMode with vMessagingMode of {vMessagingMode}", "2ndPartOfClone") 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.MessagingMode = vMessagingMode Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RecipientEmail
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRecipientEmail(ByVal vRecipientEmail As String) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vRecipientEmail = vRecipientEmail.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMail As csMail In pTempDist.Values
        If pMail.RecipientEmail.ToLowerInvariant() = vRecipientEmail Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRecipientEmail with vRecipientEmail of {vRecipientEmail}", "2ndPartOfClone") 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.RecipientEmail.ToLowerInvariant() = vRecipientEmail Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WhenSent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWhenSent(ByVal vWhenSent As DateTimeOffset) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMail As csMail In pTempDist.Values
        If pMail.WhenSent = vWhenSent Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWhenSent with vWhenSent of {vWhenSent}", "2ndPartOfClone") 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.WhenSent = vWhenSent Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Subject
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySubject(ByVal vSubject As String) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSubject = vSubject.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMail As csMail In pTempDist.Values
        If pMail.Subject.ToLowerInvariant() = vSubject Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySubject with vSubject of {vSubject}", "2ndPartOfClone") 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.Subject.ToLowerInvariant() = vSubject Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Body
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByBody(ByVal vBody As String) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vBody = vBody.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMail As csMail In pTempDist.Values
        If pMail.Body.ToLowerInvariant() = vBody Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByBody with vBody of {vBody}", "2ndPartOfClone") 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.Body.ToLowerInvariant() = vBody Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WhenSeen
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWhenSeen(ByVal vWhenSeen As DateTimeOffset) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMail As csMail In pTempDist.Values
        If pMail.WhenSeen = vWhenSeen Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWhenSeen with vWhenSeen of {vWhenSeen}", "2ndPartOfClone") 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.WhenSeen = vWhenSeen Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WasSeen
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWasSeen(ByVal vWasSeen As Boolean) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMail As csMail In pTempDist.Values
        If pMail.WasSeen = vWasSeen Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWasSeen with vWasSeen of {vWasSeen}", "2ndPartOfClone") 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.WasSeen = vWasSeen Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMail) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMail As csMail In pTempDist.Values
        If pMail.Tag.ToLowerInvariant() = vTag Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.Tag.ToLowerInvariant() = vTag Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined MessagingModeAndRecipientEmail
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmail As String) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pMail As csMail In _SortedDictionaryForFindByID.Values.ToList()
        If pMail.MessagingMode = vMessagingMode AndAlso pMail.RecipientEmail = vRecipientEmail Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.MessagingMode = vMessagingMode AndAlso pMail.RecipientEmail = vRecipientEmail Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    Return pMails
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined MessagingModeAndRecipientEmailAndWasSeen
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmail As String, ByVal vWasSeen As Boolean) As csMailCol
    Dim pMails As New csMailCol() 
    pMails._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pMail As csMail In _SortedDictionaryForFindByID.Values.ToList()
        If pMail.MessagingMode = vMessagingMode AndAlso pMail.RecipientEmail = vRecipientEmail AndAlso pMail.WasSeen = vWasSeen Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          pMails._Clean.Add(pMail.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csMailCol = Me.Clone() 
      For Each pMail As csMail In pList 
        If pMail.MessagingMode = vMessagingMode AndAlso pMail.RecipientEmail = vRecipientEmail AndAlso pMail.WasSeen = vWasSeen Then
          Dim pMailClone As csMail = pMail.Clone()
          pMails.Add(pMailClone)
          If Not _FilledFromSumOnTheFly Then pMails._Clean.Add(pMail.ID) 
        End If
      Next
    End If 
    Return pMails
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
    For Each pMail As csMail In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pMail.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailView, "csMailCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csMail In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csMail = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pMailToKill As New csMail 
          pMailToKill.ID = pCleanID 
          pMailToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pMailToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csMail In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-Mail-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailUpdate, "csMailCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csMail In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csMail In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMailCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_MailsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csMailCol(), vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific MessagingModeAndRecipientEmail 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmail As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmail={1}", vMessagingMode, vRecipientEmail)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMailCol_DeleteByMessagingModeAndRecipientEmail", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_MailsDeleteByMessagingMode&RecipientEmail"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllMails As New csMailCol() : pAllMails.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredMails As csMailCol = pAllMails.CloneByMessagingModeAndRecipientEmail(vMessagingMode, vRecipientEmail) 
      For Each l In pFilteredMails 
        pAllMails.Remove(pAllMails.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllMails, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vMessagingMode) 
        pLastReadVariableName = "RecipientEmail" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmail) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific MessagingModeAndRecipientEmailAndWasSeen 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmail As String, ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmail={1}, WasSeen={2}", vMessagingMode, vRecipientEmail, vWasSeen)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMailCol_DeleteByMessagingModeAndRecipientEmailAndWasSeen", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_MailsDeleteByMessagingMode&RecipientEmail&WasSeen"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllMails As New csMailCol() : pAllMails.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredMails As csMailCol = pAllMails.CloneByMessagingModeAndRecipientEmailAndWasSeen(vMessagingMode, vRecipientEmail, vWasSeen) 
      For Each l In pFilteredMails 
        pAllMails.Remove(pAllMails.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllMails, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vMessagingMode) 
        pLastReadVariableName = "RecipientEmail" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmail) 
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (vWasSeen) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific WasSeen 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWasSeen(ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("WasSeen={0}", vWasSeen)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMailCol_DeleteByWasSeen", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_MailsDeleteByWasSeen"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllMails As New csMailCol() : pAllMails.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredMails As csMailCol = pAllMails.CloneByWasSeen(vWasSeen) 
      For Each l In pFilteredMails 
        pAllMails.Remove(pAllMails.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllMails, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (vWasSeen) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMailCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_MailsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Mail-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vIDTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific MessagingModeAndRecipientEmail
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmailFrom As String, ByVal vRecipientEmailTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmailFrom={1}, RecipientEmailTo={2}", vMessagingMode, vRecipientEmailFrom, vRecipientEmailTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMailCol_DeleteByBoundedMessagingModeAndRecipientEmail", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_MailsDeleteByBoundedMessagingMode&RecipientEmail"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Mail-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vMessagingMode) 
        pLastReadVariableName = "RecipientEmailFrom" 
        pDALParameters.Add("bndRecipientEmailFrom", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmailFrom) 
        pLastReadVariableName = "RecipientEmailTo" 
        pDALParameters.Add("bndRecipientEmailTo", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmailTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific MessagingModeAndRecipientEmailAndWasSeen
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmailFrom As String, ByVal vRecipientEmailTo As String, ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmailFrom={1}, RecipientEmailTo={2}, WasSeen={3}", vMessagingMode, vRecipientEmailFrom, vRecipientEmailTo, vWasSeen)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailDelete, "csMailCol_DeleteByBoundedMessagingModeAndRecipientEmailAndWasSeen", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_MailsDeleteByBoundedMessagingMode&RecipientEmail&WasSeen"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Mail-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmMessagingMode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vMessagingMode) 
        pLastReadVariableName = "RecipientEmailFrom" 
        pDALParameters.Add("bndRecipientEmailFrom", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmailFrom) 
        pLastReadVariableName = "RecipientEmailTo" 
        pDALParameters.Add("bndRecipientEmailTo", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vRecipientEmailTo) 
        pLastReadVariableName = "WasSeen" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Bit).Value = (vWasSeen) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Mail-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Mail-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-090210-1341", vRequester) 
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
    Me.Sort(New csMailCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
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
  
  Public Sub SortByMessagingMode()
    Me.Sort(New csMailCol.CompareByMessagingMode)
  End Sub
  Private Class CompareByMessagingMode
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.MessagingMode < y.MessagingMode Then
        Return -1
      ElseIf x.MessagingMode = y.MessagingMode Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByMessagingModeText()
    Me.Sort(New csMailCol.CompareByMessagingModeText)
  End Sub
  Private Class CompareByMessagingModeText
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.MessagingModeText, y.MessagingModeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRecipientEmail()
    Me.Sort(New csMailCol.CompareByRecipientEmail)
  End Sub
  Private Class CompareByRecipientEmail
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.RecipientEmail, y.RecipientEmail, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByWhenSent()
    Me.Sort(New csMailCol.CompareByWhenSent)
  End Sub
  Private Class CompareByWhenSent
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.WhenSent < y.WhenSent Then
        Return -1
      ElseIf x.WhenSent = y.WhenSent Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySubject()
    Me.Sort(New csMailCol.CompareBySubject)
  End Sub
  Private Class CompareBySubject
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Subject, y.Subject, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByBody()
    Me.Sort(New csMailCol.CompareByBody)
  End Sub
  Private Class CompareByBody
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Body, y.Body, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByWhenSeen()
    Me.Sort(New csMailCol.CompareByWhenSeen)
  End Sub
  Private Class CompareByWhenSeen
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.WhenSeen < y.WhenSeen Then
        Return -1
      ElseIf x.WhenSeen = y.WhenSeen Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByWasSeen()
    Me.Sort(New csMailCol.CompareByWasSeen)
  End Sub
  Private Class CompareByWasSeen
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.WasSeen.ToString, y.WasSeen.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csMailCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csMail)
    Private Function Compare(ByVal x As csMail, ByVal y As csMail) As Integer Implements System.Collections.Generic.IComparer(Of csMail).Compare
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
  
    Dim pMail As csMail
  
    While vReader.Read()
      pMail = New csMail() 
      pFault = pMail.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pMail)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pMail.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedMailCol As csMailCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pMail As csMail 
 
      For Each pCachedMail As csMail In vCachedMailCol 
        pMail = New csMail(pCachedMail) 
        pMail.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pMail) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pMail.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Mail-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csMail) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csMail) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
