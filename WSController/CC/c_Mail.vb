Public Class csMail
  Inherits cTargCCEntity 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
 
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
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csMailGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the Mail 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  'Interface Edits
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
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
    Dim pFunctionParameters As String = String.Format("Mail.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    If _ID = 0 Then
      Return pFault.LogFreeTextFault(56, "'Mail' is not 'Addable'", pFunctionParameters, "TRGT-Mail-190217-1702", vRequester) 
    End If
    
    'Check if we got an empty object 
    Dim pMail As New csMail 
    If Me.isEqual(pMail) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-Mail-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Mail-240611-135714", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
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
      Dim pFunction As String = "csMailUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.Standard)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.Standard, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  'Interface Delete 
  Public Function Delete(ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntityDeletable.Delete 
    Dim pFunctionParameters As String = String.Format("Mail.ID={0}", _ID)
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
      Dim pFunction As String = "csMailDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150314-1803", vRequester) 
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
      Dim pFunction As String = "csMailDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-Mail-231207-1707", vRequester) 
    End Try 
 
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
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Long) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csMail) 
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
 
      Dim pFunction As String = "csMailColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MessagingMode and RecipientEmail, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmail As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmail={1}", vMessagingMode, vRecipientEmail)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) 
          ' 
          'vRecipientEmail 
          If vRecipientEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmail) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColFillByMessagingModeAndRecipientEmail" 
      Dim pParametersToLog = $"MessagingModeAndRecipientEmail: {vMessagingMode};{vRecipientEmail};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail  
      If vAppend = True Then 
        Dim pMails As New csMailCol 
        pMails.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMails) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MessagingMode and RecipientEmail and WasSeen, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmail As String, ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmail={1}, WasSeen={2}", vMessagingMode, vRecipientEmail, vWasSeen)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) 
          ' 
          'vRecipientEmail 
          If vRecipientEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmail) 
          ' 
          'vWasSeen 
          pBinaryWriter.Write(vWasSeen) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColFillByMessagingModeAndRecipientEmailAndWasSeen" 
      Dim pParametersToLog = $"MessagingModeAndRecipientEmailAndWasSeen: {vMessagingMode};{vRecipientEmail};{vWasSeen};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail  
      If vAppend = True Then 
        Dim pMails As New csMailCol 
        pMails.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMails) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific WasSeen, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWasSeen(ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("WasSeen={0}", vWasSeen)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vWasSeen 
          pBinaryWriter.Write(vWasSeen) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColFillByWasSeen" 
      Dim pParametersToLog = $"WasSeen: {vWasSeen};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail  
      If vAppend = True Then 
        Dim pMails As New csMailCol 
        pMails.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMails) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csMailColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail  
      If vAppend = True Then 
        Dim pMails As New csMailCol 
        pMails.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMails) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MessagingMode and RecipientEmail, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedMessagingModeAndRecipientEmail(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmailFrom As String, ByVal vRecipientEmailTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmailFrom={1}, RecipientEmailTo={2}", vMessagingMode, vRecipientEmailFrom, vRecipientEmailTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) 
          ' 
          'vRecipientEmailFrom 
          If vRecipientEmailFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmailFrom) 
          ' 
          'vRecipientEmailTo 
          If vRecipientEmailTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmailTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColFillByBoundedMessagingModeAndRecipientEmail" 
      Dim pParametersToLog = $"MessagingModeAndRecipientEmail: {vMessagingMode};{vRecipientEmailFrom};{vRecipientEmailTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail  
      If vAppend = True Then 
        Dim pMails As New csMailCol 
        pMails.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMails) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MessagingMode and RecipientEmail and WasSeen, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedMessagingModeAndRecipientEmailAndWasSeen(ByVal vMessagingMode As clsEnums.enmMessagingMode, ByVal vRecipientEmailFrom As String, ByVal vRecipientEmailTo As String, ByVal vWasSeen As Boolean, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MessagingMode={0}, RecipientEmailFrom={1}, RecipientEmailTo={2}, WasSeen={3}", vMessagingMode, vRecipientEmailFrom, vRecipientEmailTo, vWasSeen)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) 
          ' 
          'vRecipientEmailFrom 
          If vRecipientEmailFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmailFrom) 
          ' 
          'vRecipientEmailTo 
          If vRecipientEmailTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmailTo) 
          ' 
          'vWasSeen 
          pBinaryWriter.Write(vWasSeen) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColFillByBoundedMessagingModeAndRecipientEmailAndWasSeen" 
      Dim pParametersToLog = $"MessagingModeAndRecipientEmailAndWasSeen: {vMessagingMode};{vRecipientEmailFrom};{vRecipientEmailTo};{vWasSeen};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail  
      If vAppend = True Then 
        Dim pMails As New csMailCol 
        pMails.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMails) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csMailColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail   
      If vAppend = True Then 
        Dim pMails As New csMailCol 
        pMails.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMails) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-231207-1750", vRequester) 
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
          'MessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) : pParametersToLog &= $"MessagingMode={vMessagingMode};"  
          'RecipientEmail 
          If vRecipientEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmail) : pBinaryWriter.Write(vRecipientEmailWildcardType.FastToString()) : pParametersToLog &= $"RecipientEmail={vRecipientEmail};" : pParametersToLog &= $"RecipientEmailWildcardType={vRecipientEmailWildcardType};"  
          'WasSeen 
          pBinaryWriter.Write(vWasSeen.HasValue) 
          If vWasSeen.HasValue = True Then pBinaryWriter.Write(vWasSeen.Value) : pParametersToLog &= $"WasSeen={vWasSeen};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
          'MessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) : pParametersToLog &= $"MessagingMode={vMessagingMode};"  
          'RecipientEmail 
          If vRecipientEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmail) : pBinaryWriter.Write(vRecipientEmailWildcardType.FastToString()) 
          'WasSeen 
          pBinaryWriter.Write(vWasSeen.HasValue) 
          If vWasSeen.HasValue = True Then pBinaryWriter.Write(vWasSeen.Value) : pParametersToLog &= $"WasSeen={vWasSeen};"  
          pBinaryWriter.Write(vGroupByMessagingMode) : pParametersToLog &= $"GroupByMessagingMode={vGroupByMessagingMode};"  
          pBinaryWriter.Write(vGroupByRecipientEmail) : pParametersToLog &= $"GroupByRecipientEmail={vGroupByRecipientEmail};"  
          pBinaryWriter.Write(vGroupByWasSeen) : pParametersToLog &= $"GroupByWasSeen={vGroupByWasSeen};"  
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Mail  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
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
 
    'Check for new rows 
    For Each p As csMail In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
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
      Dim pFunction As String = "csMailColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MailCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150314-1803", vRequester) 
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
      Dim pFunction As String = "csMailColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MailCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-150314-1803", vRequester) 
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
 
      Dim pFunction As String = "csMailColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) 
          ' 
          'vRecipientEmail 
          If vRecipientEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmail) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColDeleteByMessagingModeAndRecipientEmail" 
      Dim pParametersToLog = $"MessagingModeAndRecipientEmail: {vMessagingMode};{vRecipientEmail};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) 
          ' 
          'vRecipientEmail 
          If vRecipientEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmail) 
          ' 
          'vWasSeen 
          pBinaryWriter.Write(vWasSeen) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColDeleteByMessagingModeAndRecipientEmailAndWasSeen" 
      Dim pParametersToLog = $"MessagingModeAndRecipientEmailAndWasSeen: {vMessagingMode};{vRecipientEmail};{vWasSeen};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vWasSeen 
          pBinaryWriter.Write(vWasSeen) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColDeleteByWasSeen" 
      Dim pParametersToLog = $"WasSeen: {vWasSeen};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
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
 
      Dim pFunction As String = "csMailColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) 
          ' 
          'vRecipientEmailFrom 
          If vRecipientEmailFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmailFrom) 
          ' 
          'vRecipientEmailTo 
          If vRecipientEmailTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmailTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColDeleteByBoundedMessagingModeAndRecipientEmail" 
      Dim pParametersToLog = $"MessagingModeAndRecipientEmail: {vMessagingMode};{vRecipientEmailFrom};{vRecipientEmailTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMessagingMode 
          pBinaryWriter.Write(vMessagingMode.ToString()) 
          ' 
          'vRecipientEmailFrom 
          If vRecipientEmailFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmailFrom) 
          ' 
          'vRecipientEmailTo 
          If vRecipientEmailTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vRecipientEmailTo) 
          ' 
          'vWasSeen 
          pBinaryWriter.Write(vWasSeen) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMailColDeleteByBoundedMessagingModeAndRecipientEmailAndWasSeen" 
      Dim pParametersToLog = $"MessagingModeAndRecipientEmailAndWasSeen: {vMessagingMode};{vRecipientEmailFrom};{vRecipientEmailTo};{vWasSeen};" 
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
  
