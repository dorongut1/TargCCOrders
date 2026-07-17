Public Class clsFault 
 
  Private _AdditionalMessageToUser As String 
 
  Private _Number As Integer 
  Private _Description As String 
  Private _Message As String 
  Private _Action As String 
  Private _FreeText As String 
  Private _FaultingApplication As String 
  Private _FaultingClass As String 
  Private _FaultingFunction As String 
  Private _FaultingFunctionParameters As String 
  Private _Ident As String 
  Private _LoggedAlertID As Long 
  Private _Type As clsEnums.enmFaultType 
  Private _Severity As clsEnums.enmFaultSeverity 
  Private _UILang As clsEnums.enmLanguage 
 
  Public ReadOnly Property Number() As Integer 
    Get 
      Return _Number 
    End Get 
  End Property 
  Public ReadOnly Property Description() As String 
    Get 
      Return _Description 
    End Get 
  End Property 
  Public ReadOnly Property Message() As String 
    Get 
      Return _Message 
    End Get 
  End Property 
  Public ReadOnly Property Action() As String 
    Get 
      Return _Action 
    End Get 
  End Property 
  Public ReadOnly Property FreeText() As String 
    Get 
      Return _FreeText 
    End Get 
  End Property 
  Public ReadOnly Property FaultingApplication() As String 
    Get 
      Return _FaultingApplication 
    End Get 
  End Property 
  Public ReadOnly Property FaultingClass() As String 
    Get 
      Return _FaultingClass 
    End Get 
  End Property 
  Public ReadOnly Property FaultingFunction() As String 
    Get 
      Return _FaultingFunction 
    End Get 
  End Property 
  Public ReadOnly Property FaultingFunctionParameters() As String 
    Get 
      Return _FaultingFunctionParameters 
    End Get 
  End Property 
  Public ReadOnly Property Ident() As String 
    Get 
      Return _Ident 
    End Get 
  End Property 
 
  Public ReadOnly Property LoggedAlertID() As Long 
    Get 
      Return _LoggedAlertID 
    End Get 
  End Property 
 
  Public ReadOnly Property Type() As clsEnums.enmFaultType 
    Get 
      Return _Type 
    End Get 
  End Property 
  Public ReadOnly Property Severity() As clsEnums.enmFaultSeverity 
    Get 
      Return _Severity 
    End Get 
  End Property 
  Public ReadOnly Property UILang() As clsEnums.enmLanguage 
    Get 
      Return _UILang 
    End Get 
  End Property 
 
  Public ReadOnly Property isOK() As Boolean 
    Get 
      If _Number = -1 Then Return True Else Return False 
    End Get 
  End Property 
  Public ReadOnly Property StringForMessageBox() As String 
    Get 
      Dim pStrg As String = "" 
 
      pStrg = "Fault Description:" & Environment.NewLine 
      pStrg &= "Number: " & _Number & Environment.NewLine 
      pStrg &= "Description: " & _Description & Environment.NewLine 
      pStrg &= "Message: " & _Message & Environment.NewLine 
      pStrg &= "Action: " & _Action & Environment.NewLine 
      pStrg &= "FreeText: " & _FreeText.Replace(" ‡ ", Environment.NewLine & "   ") & Environment.NewLine 
      pStrg &= "FaultingApplication: " & _FaultingApplication.Replace(" ‡ ", Environment.NewLine & "   ") & Environment.NewLine 
      pStrg &= "FaultingFunctionParameters: " & _FaultingFunctionParameters.Replace(" ‡ ", Environment.NewLine & "   ") & Environment.NewLine 
      pStrg &= "FaultingClass: " & _FaultingClass & Environment.NewLine 
      pStrg &= "FaultingFunction: " & _FaultingFunction & Environment.NewLine 
      pStrg &= "Type: " & _Type.FastToString() & Environment.NewLine 
      pStrg &= "Severity: " & _Severity.FastToString() & Environment.NewLine 
      pStrg &= "Ident: " & _Ident & Environment.NewLine 
      pStrg &= "LoggedAlertID: " & _LoggedAlertID 
 
      Return pStrg 
    End Get 
  End Property 
  Public ReadOnly Property ShortStringForMessageBox(ByVal vWithIdentation As Boolean) As String 
    Get 
      Dim pStrg As New Text.StringBuilder() 
      Dim pSpaces As String = "" 
      If vWithIdentation Then pSpaces = "    " 
 
      pStrg.AppendLine("Fault Number: " & _Number) 
      pStrg.AppendLine(pSpaces & "Description: " & _Description) 
      If Not (_Message.Equals("No Message") OrElse String.IsNullOrEmpty(_Message)) Then pStrg.AppendLine(pSpaces & "Message: " & _Message) 
      If Not (_Action.Equals("No Action") OrElse String.IsNullOrEmpty(_Action)) Then pStrg.AppendLine(pSpaces & "Action: " & _Action) 
      If Not String.IsNullOrEmpty(_FreeText) Then pStrg.AppendLine(pSpaces & "FreeText: " & _FreeText.Replace(" ‡ ", Environment.NewLine & "   ")) 
      pStrg.AppendLine(pSpaces & "Ident: " & _Ident) 
      If _LoggedAlertID > 0 Then pStrg.AppendLine(pSpaces & "LoggedAlert ID: " & _LoggedAlertID) 
 
      Return pStrg.ToString() 
    End Get 
  End Property 
 
  Public ReadOnly Property ShortStringForUser() As String 
    Get 
      Dim pStrg As New Text.StringBuilder() 
 
      If Not (_Message.Equals("No Message") OrElse String.IsNullOrEmpty(_Message)) Then pStrg.AppendLine(_Message) 
      If Not (_Action.Equals("No Action") OrElse String.IsNullOrEmpty(_Action)) Then pStrg.AppendLine(_Action) 
      pStrg.AppendLine() 
      pStrg.AppendLine("Fault: " & _Number) 
      If _LoggedAlertID > 0 Then pStrg.AppendLine($"(LoggedAlert ID: {_LoggedAlertID})") 
 
      Return pStrg.ToString() 
    End Get 
  End Property 
 
  Public ReadOnly Property ShortStringForConcatenation() As String 
    Get 
      Dim pStrg As String = "" 
 
      pStrg &= "Fault Number: " & _Number & Environment.NewLine 
      pStrg &= "Description: " & _Description & Environment.NewLine 
      pStrg &= "Ident: " & _Ident & Environment.NewLine 
      If _LoggedAlertID > 0 Then pStrg &= "LoggedAlert ID: " & _LoggedAlertID 
 
      Return pStrg 
    End Get 
  End Property 
 
  ''' <summary> 
  ''' Create an empty Fault 
  ''' </summary> 
  Public Sub New()  
    CreateEmpty()  
  End Sub 
 
  ''' <summary> 
  ''' Create a clone of a Fault from a Byte Array 
  ''' </summary> 
  ''' <param name="vBytes"></param> 
  ''' <param name="vFault"></param> 
  ''' <param name="vRequester"></param> 
  Public Sub New(ByVal vBytes As Byte(), ByVal vFault As clsFault, ByVal vRequester As clsRequester)  
    LoadByteArray(vBytes, vFault, vRequester)  
  End Sub 
 
  ''' <summary> 
  ''' Create a Fault from a Logged Alert 
  ''' </summary> 
  ''' <param name="vLoggedAlert"></param> 
  Public Sub New(ByVal vLoggedAlert As csLoggedAlert)  
    CreateEmpty()  
    _Action = vLoggedAlert.ActionSentToUser  
    _Description = vLoggedAlert.FaultDescription  
    _FaultingApplication = vLoggedAlert.CallingApplication  
    _FaultingClass = vLoggedAlert.FaultingClass  
    _FaultingFunction = vLoggedAlert.FaultingFunction  
    _FaultingFunctionParameters = vLoggedAlert.FaultingFunctionParameters  
    _FreeText = vLoggedAlert.FreeText  
    _Ident = vLoggedAlert.FaultIdent  
    _LoggedAlertID = vLoggedAlert.ID  
    _Message = vLoggedAlert.MessageSentToUser  
    _Number = vLoggedAlert.FaultNumber  
    _Severity = vLoggedAlert.FaultSeverity  
    _Type = vLoggedAlert.FaultType  
    _UILang = clsEnums.enmLanguage.en  
  End Sub 
 
  ''' <summary> 
  ''' Create a clone of a Fault 
  ''' </summary> 
  ''' <param name="vFault"></param> 
  Public Sub New(ByVal vFault As clsFault)  
    CreateEmpty()  
    _Number = vFault.Number  
    _Description = vFault.Description  
    _Message = vFault.Message  
    _Action = vFault.Action  
    _FreeText = vFault.FreeText  
    _FaultingApplication = vFault.FaultingApplication  
    _FaultingClass = vFault.FaultingClass  
    _FaultingFunction = vFault.FaultingFunction  
    _FaultingFunctionParameters = vFault.FaultingFunctionParameters  
    _Ident = vFault.Ident  
    _LoggedAlertID = vFault.LoggedAlertID  
    _Type = vFault.Type  
    _Severity = vFault.Severity  
    _UILang = vFault.UILang  
  End Sub 
 
  ''' <summary> 
  ''' Create a Fault from an Exception, using the default FaultNumber 60 
  ''' </summary> 
  ''' <param name="vException"></param> 
  ''' <param name="vFaultingFunctionParameters"></param> 
  ''' <param name="vIdent"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vAdditionalMessageToUser"></param> 
  Public Sub New(ByVal vException As Exception, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, Optional ByVal vAdditionalMessageToUser As String = "") 
    CreateEmpty() 
    LoadFunctionProperties() 
    LogExceptionInternal(60, vException, vFaultingFunctionParameters, vIdent, vRequester, vAdditionalMessageToUser) 
  End Sub 
 
  ''' <summary> 
  ''' Create a Fault from an Exception, using a specific Fault Number 
  ''' </summary> 
  ''' <param name="vFaultNo"></param> 
  ''' <param name="vException"></param> 
  ''' <param name="vFaultingFunctionParameters"></param> 
  ''' <param name="vIdent"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vAdditionalMessageToUser"></param> 
  Public Sub New(ByVal vFaultNo As Integer, ByVal vException As Exception, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, Optional ByVal vAdditionalMessageToUser As String = "") 
    CreateEmpty() 
    LoadFunctionProperties() 
    LogExceptionInternal(vFaultNo, vException, vFaultingFunctionParameters, vIdent, vRequester, vAdditionalMessageToUser) 
  End Sub 
 
  ''' <summary> 
  ''' Create a user-defined Fault with FreeText, using the default Fault Number 1 
  ''' </summary> 
  ''' <param name="vFaultNo"></param> 
  ''' <param name="vFreeText"></param> 
  ''' <param name="vFaultingFunctionParameters"></param> 
  ''' <param name="vIdent"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vAdditionalMessageToUser"></param> 
  ''' <param name="vManualFaultingAssembly"></param> 
  ''' <param name="vManualFaultingClass"></param> 
  ''' <param name="vManualFaultingFunction"></param> 
  Public Sub New(ByVal vFaultNo As Integer, ByVal vFreeText As String, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, Optional ByVal vAdditionalMessageToUser As String = "", Optional ByVal vManualFaultingAssembly As String = "", Optional ByVal vManualFaultingClass As String = "", Optional ByVal vManualFaultingFunction As String = "") 
    CreateEmpty() 
    LoadFunctionProperties() 
    If Not String.IsNullOrEmpty(vManualFaultingAssembly) Then _FaultingApplication = vManualFaultingAssembly 
    If Not String.IsNullOrEmpty(vManualFaultingClass) Then _FaultingClass = vManualFaultingClass 
    If Not String.IsNullOrEmpty(vManualFaultingFunction) Then _FaultingFunction = vManualFaultingFunction 
    LogFreeTextFaultInternal(vFaultNo, vFreeText, vFaultingFunctionParameters, vIdent, vRequester, vAdditionalMessageToUser) 
  End Sub 
 
  ''' <summary> 
  ''' Create a user-defined Fault with FreeText, using a specific Fault Number  
  ''' </summary> 
  ''' <param name="vFreeText"></param> 
  ''' <param name="vFaultingFunctionParameters"></param> 
  ''' <param name="vIdent"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vAdditionalMessageToUser"></param> 
  ''' <param name="vManualFaultingAssembly"></param> 
  ''' <param name="vManualFaultingClass"></param> 
  ''' <param name="vManualFaultingFunction"></param> 
  Public Sub New(ByVal vFreeText As String, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, Optional ByVal vAdditionalMessageToUser As String = "", Optional ByVal vManualFaultingAssembly As String = "", Optional ByVal vManualFaultingClass As String = "", Optional ByVal vManualFaultingFunction As String = "") 
    CreateEmpty() 
    LoadFunctionProperties() 
    If Not String.IsNullOrEmpty(vManualFaultingAssembly) Then _FaultingApplication = vManualFaultingAssembly 
    If Not String.IsNullOrEmpty(vManualFaultingClass) Then _FaultingClass = vManualFaultingClass 
    If Not String.IsNullOrEmpty(vManualFaultingFunction) Then _FaultingFunction = vManualFaultingFunction 
    LogFreeTextFaultInternal(1, vFreeText, vFaultingFunctionParameters, vIdent, vRequester, vAdditionalMessageToUser) 
  End Sub 
 
  ''' <summary> 
  ''' This clones the Fault, returning an exact replica (but with a different address) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function Clone() As clsFault 
    Dim pClone As New clsFault(Me) 
    Return pClone 
  End Function 
 
  ''' <summary> 
  ''' This is used to create the Fault object, when there is no access to the database 
  ''' </summary> 
  ''' <param name="vMessage"></param> 
  ''' <param name="vAction"></param> 
  ''' <param name="vType"></param> 
  ''' <param name="vSeverity"></param> 
  Public Sub SetAlertMessage( 
                            ByVal vMessage As String, 
                            ByVal vAction As String, 
                            ByVal vType As clsEnums.enmFaultType, 
                            ByVal vSeverity As clsEnums.enmFaultSeverity 
                            ) 
    _Message = vMessage 
    _Action = vAction 
    _Type = vType 
    _Severity = vSeverity 
  End Sub 
 
  ''' <summary> 
  ''' This is used to delete the free text, if you don't want it shown 
  ''' </summary> 
  Public Sub HideFreeText() 
    _FreeText = "" 
  End Sub 
 
  ''' <summary> 
  ''' This is used to add to the free text, in case you want to add manual information 
  ''' </summary> 
  ''' <param name="vFreeText"></param> 
  Public Sub AddToFreeText(ByVal vFreeText As String) 
    _FreeText = vFreeText & Environment.NewLine() & _FreeText 
  End Sub 
 
  ''' <summary>  
  ''' This is used to add extra text in the message to the user, for an existing fault object  
  ''' </summary>  
  ''' <param name="vMessage"></param>  
  Public Sub AddToUserMessage(ByVal vMessage As String) 
    _Action = _Action & Environment.NewLine() & $"({vMessage})" 
  End Sub 
 
  Public Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-150221-1008", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pFaultToLoad As clsFault = CType(pXmlSerializer.Deserialize(pStreamReader), clsFault) 
      AssignValues(pFaultToLoad) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-150221-1012", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Function CreateByteArray(ByVal vFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    vFault.ClearOK() 
    Dim pBytes As Byte() = Nothing 
    Try 
      If vFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pLength As Integer = 0 
          pBinaryWriter.Write(_Number) 
          pBinaryWriter.Write(_Description) 
          pBinaryWriter.Write(_Message) 
          pBinaryWriter.Write(_Action) 
          pBinaryWriter.Write(_FreeText) 
          pBinaryWriter.Write(_FaultingApplication) 
          pBinaryWriter.Write(_FaultingClass) 
          pBinaryWriter.Write(_FaultingFunction) 
          pBinaryWriter.Write(_FaultingFunctionParameters) 
          pBinaryWriter.Write(_Ident) 
          pBinaryWriter.Write(_LoggedAlertID) 
          pBinaryWriter.Write(_Type.FastToString()) 
          pBinaryWriter.Write(_Severity.FastToString()) 
          pBinaryWriter.Write(_UILang.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      vFault.SetOK() 
    Catch ex As Exception 
      vFault.LogException(ex, pFunctionParameters, "TRGT-150307-2338", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Public Sub LoadByteArray(ByVal vBytes As Byte(), ByVal vFault As clsFault, ByVal vRequester As clsRequester) 
    Dim pFault As New clsFault 
 
    vFault.ClearOK() 
    Try 
      If vFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          _Number = pReader.ReadInt32 
          _Description = pReader.ReadString 
          _Message = pReader.ReadString 
          _Action = pReader.ReadString 
          _FreeText = pReader.ReadString 
          _FaultingApplication = pReader.ReadString 
          _FaultingClass = pReader.ReadString 
          _FaultingFunction = pReader.ReadString 
          _FaultingFunctionParameters = pReader.ReadString 
          _Ident = pReader.ReadString 
          _LoggedAlertID = pReader.ReadInt64 
          _Type = clsEnums.TranslateEnmFaultType(pReader.ReadString) 
          _Severity = clsEnums.TranslateEnmFaultSeverity(pReader.ReadString) 
          _UILang = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      vFault.SetOK() 
    Catch ex As Exception 
      vFault.LogException(ex, "", "TRGT-150307-2339", vRequester) 
    End Try 
 
  End Sub 
 
  ''' <summary> 
  ''' If you send a requester, it will revive the LoginID. If you don't have a requester, send 'Nothing' or 'null'. 
  ''' If you're setting it to OK just to be OK (there was no error), the send nothing '()' 
  ''' </summary> 
  ''' <returns></returns> 
  Public Function SetOK() As clsFault 
    Me._Number = -1 
    Me._Description = "OK" 
    Return Me 
  End Function 
  Public Function ClearOK() As clsFault 
    Me._Number = 0 
    Me._Description = "" 
    Return Me 
  End Function 
 
  ''' <summary> 
  ''' This fills the fault object. It also returns the fault object in case you want to return it further. <br/> 
  ''' If there is no requester, then send 'Nothing' or 'null' 
  ''' </summary> 
  ''' <param name="vException"></param> 
  ''' <param name="vFaultingFunctionParameters"></param> 
  ''' <param name="vIdent"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vAdditionalMessageToUser"></param> 
  ''' <returns></returns> 
  Public Function LogException(ByVal vException As Exception, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, Optional ByVal vAdditionalMessageToUser As String = "") As clsFault 
    LoadFunctionProperties() 
    Return LogExceptionInternal(60, vException, vFaultingFunctionParameters, vIdent, vRequester, vAdditionalMessageToUser) 
  End Function 
 
  ''' <summary> 
  ''' This fills the fault object. It also returns the fault object in case you want to return it further. <br/> 
  ''' If there is no requester, then send 'Nothing' or 'null'  <br/> 
  ''' The generic fault numbers are: <br/> 
  ''' 1 - Undefined Fault <br/> 
  ''' 2 - Generic Info <br/> 
  ''' 3 - Generic LogOnly <br/> 
  ''' 4 - Generic Email <br/> 
  ''' 5 - Generic SMS <br/> 
  ''' </summary> 
  ''' <param name="vFaultNo"></param> 
  ''' <param name="vException"></param> 
  ''' <param name="vFaultingFunctionParameters"></param> 
  ''' <param name="vIdent"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vAdditionalMessageToUser"></param> 
  ''' <returns></returns> 
  Public Function LogException(ByVal vFaultNo As Integer, ByVal vException As Exception, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, Optional ByVal vAdditionalMessageToUser As String = "") As clsFault 
    LoadFunctionProperties() 
    Return LogExceptionInternal(vFaultNo, vException, vFaultingFunctionParameters, vIdent, vRequester, vAdditionalMessageToUser) 
  End Function 
  Private Function LogExceptionInternal(ByVal vFaultNo As Integer, ByVal vException As Exception, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, ByVal vAdditionalMessageToUser As String) As clsFault  
  
    _Number = vFaultNo  
  
    _FreeText = vException.Message 
    Dim pEx As Exception = vException 
    Do Until pEx.InnerException Is Nothing 
      pEx = pEx.InnerException 
      _FreeText &= " ‡ " & pEx.Message 
    Loop 
 
    _FaultingFunctionParameters = vFaultingFunctionParameters & Environment.NewLine 
 
    _FaultingFunctionParameters &= "The 'Exception':" & " ‡ " 
 
    pEx = vException 
    Dim iCntr As Integer = 1 
    _FaultingFunctionParameters &= " " & iCntr & ". " & vException.Message & " ‡ " & "||| ‡     Stack Trace:" & vException.StackTrace & " ‡ " 
    _FaultingFunctionParameters &= "  Checking Inner Exceptions:" & " ‡ " 
    Do Until pEx.InnerException Is Nothing 
      iCntr += 1 
      pEx = pEx.InnerException 
      _FaultingFunctionParameters &= " " & iCntr & ". " & pEx.Message & " ‡ " & "    Stack Trace:" & pEx.StackTrace & " ‡ " 
    Loop 
 
    _Ident = vIdent  
    _AdditionalMessageToUser = vAdditionalMessageToUser  
  
    CreateLoggedAlert(vRequester)  
    Return Me  
  End Function 
 
  ''' <summary> 
  ''' This fills the fault object. It also returns the fault object in case you want to return it further. <br/> 
  ''' If there is no requester, then send 'Nothing' or 'null'  <br/> 
  ''' The generic fault numbers are: <br/> 
  ''' 1 - Undefined Fault <br/> 
  ''' 2 - Generic Info <br/> 
  ''' 3 - Generic LogOnly <br/> 
  ''' 4 - Generic Email <br/> 
  ''' 5 - Generic SMS <br/> 
  ''' </summary> 
  ''' <param name="vFaultNo"></param> 
  ''' <param name="vFreeText"></param> 
  ''' <param name="vFaultingFunctionParameters"></param> 
  ''' <param name="vIdent"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vAdditionalMessageToUser"></param> 
  ''' <param name="vManualFaultingAssembly"></param> 
  ''' <param name="vManualFaultingClass"></param> 
  ''' <param name="vManualFaultingFunction"></param> 
  ''' <returns></returns> 
  Public Function LogFreeTextFault(ByVal vFaultNo As Integer, ByVal vFreeText As String, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, Optional ByVal vAdditionalMessageToUser As String = "", Optional ByVal vManualFaultingAssembly As String = "", Optional ByVal vManualFaultingClass As String = "", Optional ByVal vManualFaultingFunction As String = "") As clsFault 
    LoadFunctionProperties() 
    If Not String.IsNullOrEmpty(vManualFaultingAssembly) Then _FaultingApplication = vManualFaultingAssembly 
    If Not String.IsNullOrEmpty(vManualFaultingClass) Then _FaultingClass = vManualFaultingClass 
    If Not String.IsNullOrEmpty(vManualFaultingFunction) Then _FaultingFunction = vManualFaultingFunction 
    Return LogFreeTextFaultInternal(vFaultNo, vFreeText, vFaultingFunctionParameters, vIdent, vRequester, vAdditionalMessageToUser) 
  End Function 
 
  ''' <summary> 
  ''' This fills the fault object. It also returns the fault object in case you want to return it further. <br/> 
  ''' If there is no requester, then send 'Nothing' or 'null' 
  ''' </summary> 
  ''' <param name="vFreeText"></param> 
  ''' <param name="vFaultingFunctionParameters"></param> 
  ''' <param name="vIdent"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vAdditionalMessageToUser"></param> 
  ''' <param name="vManualFaultingAssembly"></param> 
  ''' <param name="vManualFaultingClass"></param> 
  ''' <param name="vManualFaultingFunction"></param> 
  ''' <returns></returns> 
  Public Function LogFreeTextFault(ByVal vFreeText As String, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, Optional ByVal vAdditionalMessageToUser As String = "", Optional ByVal vManualFaultingAssembly As String = "", Optional ByVal vManualFaultingClass As String = "", Optional ByVal vManualFaultingFunction As String = "") As clsFault 
    LoadFunctionProperties() 
    If Not String.IsNullOrEmpty(vManualFaultingAssembly) Then _FaultingApplication = vManualFaultingAssembly 
    If Not String.IsNullOrEmpty(vManualFaultingClass) Then _FaultingClass = vManualFaultingClass 
    If Not String.IsNullOrEmpty(vManualFaultingFunction) Then _FaultingFunction = vManualFaultingFunction 
    Return LogFreeTextFaultInternal(1, vFreeText, vFaultingFunctionParameters, vIdent, vRequester, vAdditionalMessageToUser) 
  End Function 
 
  Private Function LogFreeTextFaultInternal(ByVal vFaultNo As Integer, ByVal vFreeText As String, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester, ByVal vAdditionalMessageToUser As String) As clsFault  
  
    _Number = vFaultNo  
    _FreeText = vFreeText  
  
    _FaultingFunctionParameters = vFaultingFunctionParameters  
    _Ident = vIdent  
    _AdditionalMessageToUser = vAdditionalMessageToUser  
  
    CreateLoggedAlert(vRequester)  
    Return Me  
  End Function  
  
  Private Sub LoadFunctionProperties()  
    _FaultingApplication = (New StackFrame(2)).GetMethod().DeclaringType.Namespace()  
    _FaultingClass = (New StackFrame(2)).GetMethod().DeclaringType.Name()  
    _FaultingFunction = (New StackFrame(2)).GetMethod().Name  
  End Sub  
  
  Private Sub CreateLoggedAlert(ByVal vRequester As clsRequester) 
 
    'Get Stack Trace 
    If Not (_Severity = clsEnums.enmFaultSeverity.Info OrElse _Severity = clsEnums.enmFaultSeverity.LogOnly OrElse 
          _Number = 144 OrElse 
          _Number = 106 OrElse 
          _Number = 104 OrElse 
          _Number = 91 OrElse 
          _Number = 92 OrElse 
          _Number = 78 OrElse 
          _Number = 82 OrElse 
          _Number = 99 OrElse 
          _Number = 88 OrElse 
          _Number = 6 OrElse 
          _Number = 5 OrElse 
          _Number = 4 OrElse 
          _Number = 3 OrElse 
          _Number = 2 OrElse 
          vRequester Is Nothing OrElse 
          _FaultingFunctionParameters.IndexOf("Stack Trace", StringComparison.OrdinalIgnoreCase) >= 0) Then 'don't want to get the stack trace if we already got it from the outside 
        Dim pStackTrace As New Text.StringBuilder 
      Try 
        pStackTrace.AppendLine("WSController: ") 
        pStackTrace.AppendLine("  Call List: ") 
        Dim iCntr As Integer = 0 
        Do 
          Dim pMethodBase As Reflection.MethodBase = (New StackFrame(iCntr)).GetMethod() 
         iCntr += 1 
          If Not (pMethodBase Is Nothing) Then 
            If pMethodBase.DeclaringType?.Namespace.StartsWith("System.", StringComparison.OrdinalIgnoreCase) Then Continue Do 
            If pMethodBase.DeclaringType?.Namespace.StartsWith("Microsoft.", StringComparison.OrdinalIgnoreCase) Then Continue Do 
            pStackTrace.AppendLine("    " & iCntr.ToString().PadLeft(2, " "c) & ": " & pMethodBase.DeclaringType?.FullName() & "." & pMethodBase.Name) 
          Else 
            Exit Do 
          End If 
        Loop 
        pStackTrace.AppendLine(Environment.NewLine & Environment.NewLine & "--> Stack Trace: ") 
        Dim pStacks As String() = Environment.StackTrace.Replace(Environment.NewLine, "|").Split("|"c) 
        For Each l In pStacks 
          If l.IndexOf(":line ", StringComparison.OrdinalIgnoreCase) >= 0 Then 
            pStackTrace.AppendLine(l) 
          End If 
        Next 
      Catch ex As Exception 
        pStackTrace.AppendLine("--> WSController Stack Trace Failed: " & "--> Stack Trace: " & Environment.NewLine & Environment.StackTrace & Environment.NewLine & ex.Message) 
      End Try 
      If String.IsNullOrEmpty(_FaultingFunctionParameters) Then 
        _FaultingFunctionParameters = $"|||{Environment.NewLine}" & ccHelper.RemoveIllegalXMLChars(pStackTrace.ToString()) 
      Else 
        _FaultingFunctionParameters &= Environment.NewLine & $"|||{Environment.NewLine}" & Environment.NewLine & ccHelper.RemoveIllegalXMLChars(pStackTrace.ToString()) 
      End If 
    End If 
 
    If _Number = 68 Then 
      'prepare to show the user. It's already been logged 
      _Description = "RunAPI Unavailable" 
      _Type = clsEnums.enmFaultType.System 
      _Severity = clsEnums.enmFaultSeverity.Alert 
      _Message = "The system cannot be accessed at this time." 
      _Action = "Please try again later." 
      Return 
    End If 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(_Number) 
          pBinaryWriter.Write(_FreeText) 
          pBinaryWriter.Write(_FaultingApplication) 
          pBinaryWriter.Write(_FaultingClass) 
          pBinaryWriter.Write(_FaultingFunction) 
          pBinaryWriter.Write(_FaultingFunctionParameters) 
          pBinaryWriter.Write(_Ident) 
          pBinaryWriter.Write(_AdditionalMessageToUser) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFault As New clsFault 
      Dim pFunction As String = "clsFaultCreateLoggedAlert" 
      Dim pParametersToLog = $"Number: {_Number};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) 
      If Not pFault.isOK Then 
        _FreeText = "Logging Failed: " & pFault.StringForMessageBox & "; FreeText" & _FreeText : _Message = "Logging Failed (Bad Fault Returned)" & _Message 
        Tools.LogToTextFile.WriteMessage("Logging Failed Fault in RunAPI:" & pFault.StringForMessageBox & Environment.NewLine & "Original Problem: " & _FreeText, "CreateLoggedAlert") 
        Exit Sub 
      End If 
 
      'Use the response to build the Fault   
      LoadByteArray(pResponse, pFault, vRequester) 
      If Not pFault.isOK Then 
        _FreeText = "Logging Failed: " & pFault.StringForMessageBox 
        Tools.LogToTextFile.WriteMessage("Logging Failed Fault in LoadByteArray:" & pFault.StringForMessageBox & Environment.NewLine & "Original Problem: " & _FreeText, "CreateLoggedAlert") 
        Exit Sub 
      End If 
    Catch ex As Exception 
      _FreeText = "Logging Failed: " & ex.Message & "; FreeText" & _FreeText : _Message = "Logging Failed (exception)" & _Message  
      Tools.LogToTextFile.WriteMessage("Logging Failed Exception:" & Tools.LogToTextFile.GetExceptionString(ex) & Environment.NewLine & "Original Problem: " & _FreeText, "CreateLoggedAlert") 
      Exit Sub 
    End Try 
 
  End Sub 
 
  Private Sub CreateEmpty() 
    _Number = 0 
    _Description = "" 
    _Message = "" 
    _Action = "" 
    _FreeText = "" 
    _FaultingApplication = "" 
    _FaultingClass = "" 
    _FaultingFunction = "" 
    _FaultingFunctionParameters = "" 
    _Ident = "" 
 
    _LoggedAlertID = 0 
 
    _Type = clsEnums.enmFaultType.UD 
    _Severity = clsEnums.enmFaultSeverity.UD 
    _UILang = clsEnums.enmLanguage.en 
  End Sub 
 
  ''' <summary> 
  ''' This assigns the intrinsic values of the item to the object 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Sub AssignValues(ByVal vFaultToLoad As clsFault) 
    With vFaultToLoad 
      _Number = .Number 
      _Description = .Description 
      _Message = .Message 
      _Action = .Action 
      _FreeText = .FreeText 
      _FaultingApplication = .FaultingApplication 
      _FaultingClass = .FaultingClass 
      _FaultingFunction = .FaultingFunction 
      _FaultingFunctionParameters = .FaultingFunctionParameters 
      _Ident = .Ident 
      _LoggedAlertID = .LoggedAlertID 
      _Type = .Type 
      _Severity = .Severity 
      _UILang = .UILang 
    End With 
  End Sub 
 
End Class 
