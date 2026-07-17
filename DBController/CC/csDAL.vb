Imports System.Data.SqlClient

Public Class ccDAL

  ''' <summary>
  ''' Use this when you want to get the csTargCCReader back. If don't sent vCommandTimeoutSec, it will use the default (30 sec)
  ''' </summary>
  ''' <param name="vQueryName"></param>
  ''' <param name="vParameters"></param>
  ''' <param name="vRequester"></param>
  ''' <param name="rTargCCReader"></param>
  ''' <param name="vCommandTimeoutSec"></param>
  ''' <returns></returns>
  Friend Shared Function ExecuteQuery(ByVal vQueryName As String, ByVal vParameters As csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rTargCCReader As csTargCCReader, Optional vCommandTimeoutSec As Integer = 0) As clsFault
    Try
      Return ExecuteQuery(vQueryName, vParameters, vRequester, Nothing, rTargCCReader, vCommandTimeoutSec)
    Catch ex As Exception
      Tools.LogToTextFile.WriteException("Untrapped Exception when running ExecuteQuery", ex, "CC")
      Throw ex
    End Try
  End Function
  ''' <summary>
  ''' Use this when you want to fill a cTargCCEntity or cTargCCCollection. It's faster, since it doesn't have to fill the csTargCCReader. If don't sent vCommand3TimeoutSec, it will use the default (30 sec)
  ''' </summary>
  ''' <param name="vQueryName"></param>
  ''' <param name="vParameters"></param>
  ''' <param name="vRequester"></param>
  ''' <param name="rEntity"></param>
  ''' <param name="vCommandTimeoutSec"></param>
  ''' <returns></returns>
  Friend Shared Function ExecuteQuery(ByVal vQueryName As String, ByVal vParameters As csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rEntity As ITargCCDataReaderUser, Optional vCommandTimeoutSec As Integer = 0) As clsFault
    Try
      Return ExecuteQuery(vQueryName, vParameters, vRequester, rEntity, Nothing, vCommandTimeoutSec)
    Catch ex As Exception
      Tools.LogToTextFile.WriteException("Untrapped Exception when running ExecuteQuery", ex, "CC")
      Throw ex
    End Try
  End Function


  Private Shared _LoggedRowHeader As Text.StringBuilder = Nothing

  Private Shared Function ExecuteQuery(ByVal vQueryName As String, ByVal vParameters As csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rEntity As ITargCCDataReaderUser, ByRef rTargCCReader As csTargCCReader, ByVal vCommandTimeoutSec As Integer) As clsFault
    Dim pFunctionParameters = vQueryName
    Dim pFault As New clsFault
    Dim pLastReadVariableName As String = ""

    If vCommandTimeoutSec = 0 Then
      If vQueryName.EndsWith("OnTheFly", StringComparison.OrdinalIgnoreCase) Then vCommandTimeoutSec = 90
    End If
    If vCommandTimeoutSec <> 0 Then
      pFunctionParameters &= "(TimeOutSec:" & vCommandTimeoutSec.ToString() & ")"
    End If

    Dim pCreateHeader As Boolean = (_LoggedRowHeader Is Nothing)
    Dim sw As Stopwatch = Nothing
    Dim pLoggedRow As Text.StringBuilder = Nothing

    Dim pDBConn As String = ""
    Try
      pDBConn = MyController.DBConn
    Catch ex As Exception
      pFault = New clsFault
      pFault.SetAlertMessage("Cannot create connection string", "Contact Support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.SMS)
      Return pFault.LogException(5, ex, pFunctionParameters, "TRGT-160515-1113", vRequester)
    End Try

    Dim pLogDAL As Boolean = False
    If Not (_LoggedRowHeader Is Nothing AndAlso vQueryName.Equals("c_SystemDefaultsFillByGroup", StringComparison.OrdinalIgnoreCase)) Then
      'to avoid circular reference...
      pLogDAL = MyController.LogDetails
    End If

    If pLogDAL = True Then
      sw = New Stopwatch
      pLoggedRow = New Text.StringBuilder
      If pCreateHeader = True Then
        _LoggedRowHeader = New Text.StringBuilder
        _LoggedRowHeader.Append(", CallingApplication, UserName, LoggedLoginID, vQueryName, ")
      End If
      pLoggedRow.Append(String.Format(", {0}, {1}, {2}, {3}, ", vRequester.CallingApplication, vRequester.UserName, vRequester.LoggedLoginID, vQueryName))
      If pCreateHeader = True Then
        _LoggedRowHeader.Append("ChangedBy, ")
      End If
      Try
        'If vParameters.Count > 2 AndAlso vParameters(vParameters.Count - 2).Name = "ChangedBy" Then 'vParameters(vParameters.Count - 2).Value.ToString().Length < 25
        If vParameters.Count > 2 AndAlso vParameters(vParameters.Count - 2).Value.ToString().Length < 25 Then 'vParameters(vParameters.Count - 2).Name = "ChangedBy" Then 
          pLoggedRow.Append(String.Format("{0}, ", vParameters(vParameters.Count - 2).Value.ToString() & "(" & vParameters(vParameters.Count - 2).Name & ")"))
        Else
          pLoggedRow.Append("None, ")
        End If
      Catch ex As Exception
        pLoggedRow.Append("None (not caught), ")
      End Try
      sw.Start()
    End If

    Using myConn As New SqlConnection(pDBConn)
      Using myCommand As New SqlCommand
        Dim myReader As SqlDataReader = Nothing
        Try
          With myCommand
            .CommandType = CommandType.StoredProcedure
            .CommandText = vQueryName
            .Connection = myConn
            If vCommandTimeoutSec > 0 Then
              .CommandTimeout = vCommandTimeoutSec
            End If
            If pLogDAL = True Then
              If pCreateHeader = True Then _LoggedRowHeader.Append("1st Parameter, ")
              If vParameters.Count > 0 Then
                pLoggedRow.Append(String.Format("{0}: {1}, ", vParameters(0).Name, vParameters(0).Value))
              Else
                pLoggedRow.Append("None, ")
              End If
            End If
            For Each l In vParameters
              pLastReadVariableName = l.Name
              Dim pSize As Integer = l.Size
              If l.DataType = enmSQLDataType.NVarCharMax OrElse l.DataType = enmSQLDataType.VarCharMax Then
                pSize = 0
              ElseIf l.DataType = enmSQLDataType.VarBinaryMax Then
                pSize = -1
              End If
              If pSize = 0 Then
                .Parameters.Add("@" & l.Name, TranslateSQLDataType(l.DataType)).Value = l.Value
              Else
                .Parameters.Add("@" & l.Name, TranslateSQLDataType(l.DataType), pSize).Value = l.Value
                If (l.DataType = enmSQLDataType.NVarChar OrElse l.DataType = enmSQLDataType.VarChar) AndAlso l.Value.ToString().Length > pSize Then
                  'only 'fail' in Test mode, to avoid catastrophes
                  pFunctionParameters = pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & Environment.NewLine & vParameters.ToString() & ccHelper.GetStack()
                  If MyController.InTestMode Then
                    Return pFault.LogFreeTextFault(48, $"The max data size for {pLastReadVariableName} is {pSize}. Received {l.Value.ToString().Length}", pFunctionParameters, "TRGT-240608-222641", vRequester, vAdditionalMessageToUser:=$"The max data size for {pLastReadVariableName} is {pSize}. Received {l.Value.ToString().Length}")
                  Else
                    pFault.LogFreeTextFault(3, $"The max data size for {pLastReadVariableName} is {pSize}. Received {l.Value.ToString().Length}{Environment.NewLine}The data was truncated and saved", pFunctionParameters, "TRGT-240611-143945", vRequester)
                  End If
                End If
              End If
            Next
            pLastReadVariableName = ""
          End With
        Catch ex As Exception
          pFunctionParameters = pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & Environment.NewLine & vParameters.ToString()
          If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
          Return pFault.LogException(ex, pFunctionParameters, "TRGT-160328-0845", vRequester)
        End Try
        Try
          Try
            If pLogDAL = True Then
              sw.Stop()
              If pCreateHeader = True Then _LoggedRowHeader.Append("PrepareforQuery, ")
              pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds))
              sw.Restart()
            End If
            myConn.Open()
            If pLogDAL = True Then
              sw.Stop()
              If pCreateHeader = True Then _LoggedRowHeader.Append("OpenConn, ")
              pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds))
            End If
          Catch ex As SqlException
            pFunctionParameters = pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & Environment.NewLine & vParameters.ToString()
            Return LogSQLFault(49, ex, "Tried to open SQL connection", pFunctionParameters, "TRGT-160331-0822", vRequester)
          End Try
          Try
            If pLogDAL = True Then
              sw.Restart()
            End If
            myReader = myCommand.ExecuteReader()
            pFault.SetOK()
            If pLogDAL = True Then
              sw.Stop()
              If pCreateHeader = True Then _LoggedRowHeader.Append("ExecuteReader, ")
              pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds))
              sw.Restart()
            End If
          Catch ex As SqlException
            If pLogDAL = True Then
              pLoggedRow.Append(String.Format("ExecuteReader Failed!! {0}, ", sw.Elapsed.TotalMilliseconds))
            End If
            pFunctionParameters = pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & Environment.NewLine & vParameters.ToString()
            If ex.State = 24 Then
              'this means it's mine
              pFault.LogFreeTextFault(50, ex.Errors(0).Message, pFunctionParameters, "TRGT-240131-180137", vRequester, vAdditionalMessageToUser:=ex.Errors(0).Message)
            ElseIf ex.Number = 2601 Then 'Unique key violation
              Dim pAppendix As String = ""
              Dim pMessage As String = ex.Errors(0).Message
              Try
                pMessage = ex.Errors(0).Message
                Dim pTable As String = pMessage.Split("'"c)(1)
                pTable = pTable.Replace("dbo.", "")
                Dim pIndex As String = pMessage.Split("'"c)(3)
                pIndex = pIndex.Replace("IX_", "IX").Replace("IX" + pTable, "")
                pIndex = pIndex.TrimStart("_"c)
                pAppendix = pIndex
                Dim pCriminal As String = pMessage.Split("'"c)(4)
                pCriminal = pCriminal.Replace(". The duplicate key value is (", "")
                pCriminal = pCriminal.Substring(0, pCriminal.Length - 2)
                pMessage = "There is already a " & pTable & " with the " & pIndex & " of " & pCriminal
              Catch exx As Exception
                pMessage = "Tried to add a double row. " & ex.Message.Replace("dbo.", "").Replace("IX_", "")
              End Try
              pFault.LogFreeTextFault(41, pMessage, pFunctionParameters, "TRGT-10032-1601", vRequester, vAdditionalMessageToUser:=pAppendix)
            ElseIf ex.Number = 229 Then 'Execute permission denied 
              Tools.LogToTextFile.WriteException("229 - Execute permission denied", ex, "CC")
              Dim pEx As String = Tools.LogToTextFile.GetExceptionString(ex)
              pFault = ccHelper.SendSMSorEmail("229 - Execute permission denied" & Environment.NewLine & pEx, MyController.ProblemMailTo, vRequester, vSubject:="DB Failure!")
              'Tools.Mailer.SendExceptionByMailToMultipleRecipients("DB Failure!", MyController.FailedMailTo, "229 - Execute permission denied", ex)
              pFault = LogSQLFault(50, ex, myCommand.CommandText, pFunctionParameters, "TRGT-210313-1154", vRequester)
              pFault.SetAlertMessage("Execute permission denied", "Call support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.Alert)
            ElseIf ex.Number = 547 Then 'The INSERT statement conflicted with the FOREIGN KEY constraint
              Dim pMessage As String = ex.Errors(0).Message
              If pMessage.IndexOf("The DELETE statement", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Try
                  Dim pTable As String = ""
                  Try
                    pTable = pMessage.Split(""""c)(5).Replace("dbo.", "")
                  Catch
                    pMessage = ex.Message & " (Error Parse failed)"
                  End Try
                  pMessage = "The delete cannot be done because there is at least 1 dependant row in " & pTable
                Catch exx As Exception
                  pMessage = ex.Message
                End Try
                pFault.LogFreeTextFault(43, pMessage, pFunctionParameters, "TRGT-200520-1626", vRequester)
              ElseIf pMessage.IndexOf("The UPDATE statement", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Try
                  Dim pTable As String = ""
                  Dim pColumn As String = ""
                  Try
                    pTable = pMessage.Split(""""c)(5).Replace("dbo.", "")
                    pColumn = pMessage.Split("'"c)(1)
                  Catch
                    pMessage = ex.Message & " (Error Parse failed)"
                  End Try
                  pMessage = $"The update cannot be done because there is no row in {pTable} with this {pColumn}"
                Catch exx As Exception
                  pMessage = ex.Message
                End Try
                pFault.LogFreeTextFault(42, pMessage, pFunctionParameters, "TRGT-200520-1625", vRequester)
              Else
                pMessage = ex.Message
                pFault.LogFreeTextFault(42, pMessage, pFunctionParameters, "TRGT-200520-1624", vRequester)
              End If
            Else
              If MyController.IsForgiving = True Then
                Threading.Thread.Sleep(2000)
                pFault.LogException(52, ex, $"{pFunctionParameters}{Environment.NewLine}|||{Environment.NewLine}{ccHelper.GetStack}", "TRGT-090202-1519", vRequester)
                pFault.SetOK(vRequester) ' we want to keep going....
                EnsureConnectionIsOpen(myConn)
                myReader = myCommand.ExecuteReader()
                pFault.SetOK()
              Else
                pFault = LogSQLFault(55, ex, myCommand.CommandText, pFunctionParameters, "TRGT-140522-1206", vRequester)
              End If
            End If
          Catch ex As Exception
            pFunctionParameters = pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & Environment.NewLine & vParameters.ToString()
            pFault.LogException(50, ex, pFunctionParameters, "TRGT-160331-0824", vRequester)
          End Try
        Catch ex As SqlException
          If pFunctionParameters.IndexOf("called by") < 0 Then pFunctionParameters = pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & Environment.NewLine & vParameters.ToString()
          If ex.Message.IndexOf("Procedure or function mn_ComboListFillManual has too many arguments specified") >= 0 Then
            pFault.LogFreeTextFault(50, "Stored Procedure 'mn_ComboListFillManual' has to be upgraded. Add the arguments SearchBy and Howmany. Use c__ComboListFillAuto as an example. Make sure the fields you 'order by' are indexed.", ex.Message, "TRGT-200719-1636", vRequester)
          Else
            pFault = LogSQLFault(50, ex, myCommand.CommandText, pFunctionParameters, "TRGT-090623-1647", vRequester)
          End If
        Catch exx As Exception
          If pFunctionParameters.IndexOf("called by") < 0 Then pFunctionParameters = pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & Environment.NewLine & vParameters.ToString()
          pFault.LogException(exx, pFunctionParameters, "TRGT-090623-1648", vRequester)
        End Try

        If pFault.isOK = True Then
          If myReader.HasRows = True Then
            Try
              If rEntity Is Nothing Then
                pFault = LoadTargCCReader(myReader, vRequester, rTargCCReader)
              Else
                If TypeOf (rEntity) Is ITargCCEntity Then myReader.Read()
                pFault = rEntity.LoadMeFromIDataReader(myReader, vRequester)
              End If
              If Not pFault.isOK Then
                Return pFault
              End If
            Catch ex As Exception
              If pFunctionParameters.IndexOf("called by") < 0 Then pFunctionParameters = pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & Environment.NewLine & vParameters.ToString()
              pFault.LogException(ex, pFunctionParameters, "TRGT-160331-1206", vRequester)
            End Try
          ElseIf myReader.FieldCount = 0 Then 'this indicates that the query failed
            Dim pPar As String = ""
            If vParameters.Count > 0 Then
              pPar = vParameters(0).Value.ToString()
            End If
            pFault.LogFreeTextFault(50, $"No fields were returned for SP {vQueryName} with Parameter(0) of '{pPar}'", pFunctionParameters, "TRGT-240508-201657", vRequester)
          Else
            If rEntity Is Nothing Then
              rTargCCReader = New csTargCCReader(New csTargCCReaderData(New csTargCCReaderHeader(0)))
            End If
          End If
        End If

        'check if we had an error - errors that occur after a 'select' can only be seen if we "try" to get the next result set (we only have one real result set). 
        If myReader IsNot Nothing Then
          Try
            If Not myReader.NextResult = True Then
              If pLogDAL = True Then
                sw.Stop()
                If pCreateHeader = True Then _LoggedRowHeader.Append("LoadedObject, ")
                pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds))
                sw.Restart()
              End If
              myReader.Close()
              If pLogDAL = True Then
                sw.Stop()
                If pCreateHeader = True Then _LoggedRowHeader.Append("CloseReader, ")
                pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds))
              End If
            Else
              'I had an error here. Send to mail for now.
              pFunctionParameters = "Stored Procedure " & pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & "." & Environment.NewLine & "Check the stored procedure!!" & Environment.NewLine & vParameters.ToString()
              pFault.LogFreeTextFault(4, "There was a NextResult for myReader which I did not read. It could mean there was an error.", pFunctionParameters, "TRGT-211101-0913", vRequester)
            End If
          Catch ex As Exception
            pFunctionParameters = "Stored Procedure " & pFunctionParameters & " called by " & (New StackFrame(2)).GetMethod().Name & "." & Environment.NewLine & "Check the stored procedure!!" & Environment.NewLine & vParameters.ToString()
            pFault.LogException(50, ex, pFunctionParameters, "TRGT-151113-1405", vRequester)
          End Try
        End If

        If pLogDAL = True Then
          sw.Restart()
        End If
        myConn.Close()
        If pLogDAL = True Then
          sw.Stop()
          If pCreateHeader = True Then _LoggedRowHeader.Append("CloseConn, ")
          pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds))
        End If
        myReader = Nothing
      End Using
    End Using

    If pLogDAL = True Then
      If pCreateHeader = True Then Tools.LogToTextFile.WriteMessage(_LoggedRowHeader.ToString() & " times in ms", "DALTimes")
      Tools.LogToTextFile.WriteMessage(pLoggedRow.ToString(), "DALTimes")
    End If

    Return pFault
  End Function

  Private Shared _InfoMessage As String

  Friend Shared Function ExecuteScript(ByVal vScript As String, ByRef rResponse As String, ByVal vRequester As clsRequester, Optional ByVal vCommandTimeoutSec As Integer = 0) As clsFault
    Dim pFunctionParameters = vScript
    Dim pFault As New clsFault
    Dim pLastReadVariableName As String = ""

    Dim pDBConn As String = ""
    Try
      pDBConn = MyController.DBConn
    Catch ex As Exception
      pFault = New clsFault
      pFault.SetAlertMessage("Cannot create connection string", "Contact Support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.SMS)
      Return pFault.LogException(5, ex, pFunctionParameters, "TRGT-190401-1623", vRequester)
    End Try

    Try
      Using myConn As New SqlConnection(pDBConn)
        Using myCommand As New SqlCommand
          Dim myReader As SqlDataReader = Nothing
          Try
            With myCommand
              .CommandType = CommandType.Text
              .CommandText = vScript
              .Connection = myConn
              If vCommandTimeoutSec > 0 Then
                .CommandTimeout = vCommandTimeoutSec
              End If
            End With
          Catch ex As Exception
            pFunctionParameters = pFunctionParameters & Environment.NewLine & " called by " & (New StackFrame(2)).GetMethod().Name
            Return pFault.LogException(ex, pFunctionParameters, "TRGT-160328-0845", vRequester)
          End Try
          Try
            Try
              myConn.Open()
              AddHandler myConn.InfoMessage, AddressOf OnInfoMessage
            Catch ex As SqlException
              pFunctionParameters = pFunctionParameters & Environment.NewLine & " called by " & (New StackFrame(2)).GetMethod().Name
              Return LogSQLFault(49, ex, "Tried to open SQL connection", pFunctionParameters, "TRGT-160331-0822", vRequester)
            End Try
            Try
              myReader = myCommand.ExecuteReader()
              pFault.SetOK()
            Catch ex As SqlException
              pFunctionParameters = pFunctionParameters & Environment.NewLine & " called by " & (New StackFrame(2)).GetMethod().Name
              If ex.State = 24 Then
                'this means it's mine
                pFault.LogFreeTextFault(50, ex.Errors(0).Message, pFunctionParameters, "TRGT-240201-092631", vRequester, vAdditionalMessageToUser:=ex.Errors(0).Message)
              ElseIf ex.Number = 2601 Then 'Unique key violation
                pFault.LogFreeTextFault(41, "Tried to add a double row. " & ex.Message.Replace("dbo.", "").Replace("IX_", ""), pFunctionParameters, "TRGT-10032-1601", vRequester)
              ElseIf ex.Number = 229 Then 'Execute permission denied 
                Tools.LogToTextFile.WriteException("229 - Execute permission denied", ex, "CC")
                Dim pEx As String = Tools.LogToTextFile.GetExceptionString(ex)
                pFault = ccHelper.SendSMSorEmail("229 - Execute permission denied" & Environment.NewLine & pEx, MyController.ProblemMailTo, vRequester, vSubject:="DB Failure!")
                'Tools.Mailer.SendExceptionByMailToMultipleRecipients("DB Failure!", MyController.FailedMailTo, "229 - Execute permission denied", ex)
                pFault = LogSQLFault(50, ex, myCommand.CommandText, pFunctionParameters, "TRGT-210313-1155", vRequester)
                pFault.SetAlertMessage("Execute permission denied", "Call support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.Alert)
              ElseIf ex.Number = 547 Then 'The INSERT statement conflicted with the FOREIGN KEY constraint
                pFault.LogFreeTextFault(41, "Tried to add row with an invalid key. " & ex.Message.Replace("dbo.", "").Replace("IX_", ""), pFunctionParameters, "TRGT-170207-1009", vRequester)
              Else
                If MyController.IsForgiving = True Then
                  Threading.Thread.Sleep(2000)
                  pFault.LogException(52, ex, $"{pFunctionParameters}{Environment.NewLine}|||{Environment.NewLine}{ccHelper.GetStack}", "TRGT-090202-1519", vRequester)
                  pFault.SetOK(vRequester) ' we want to keep going....
                  EnsureConnectionIsOpen(myConn)
                  myReader = myCommand.ExecuteReader()
                  pFault.SetOK()
                Else
                  pFault = LogSQLFault(55, ex, myCommand.CommandText, pFunctionParameters, "TRGT-140522-1206", vRequester)
                End If
              End If
            Catch ex As Exception
              pFunctionParameters = pFunctionParameters & Environment.NewLine & " called by " & (New StackFrame(2)).GetMethod().Name
              pFault.LogException(50, ex, pFunctionParameters, "TRGT-160331-0824", vRequester)
            End Try
          Catch ex As SqlException
            If pFunctionParameters.IndexOf("called by") < 0 Then pFunctionParameters = pFunctionParameters & Environment.NewLine & " called by " & (New StackFrame(2)).GetMethod().Name
            pFault = LogSQLFault(50, ex, myCommand.CommandText, pFunctionParameters, "TRGT-090623-1647", vRequester)
          Catch exx As Exception
            If pFunctionParameters.IndexOf("called by") < 0 Then pFunctionParameters = pFunctionParameters & Environment.NewLine & " called by " & (New StackFrame(2)).GetMethod().Name
            pFault.LogException(exx, pFunctionParameters, "TRGT-090623-1648", vRequester)
          End Try

          If pFault.isOK = True Then
            If myReader.HasRows = True Then
              Try
                myReader.Read()
                rResponse = myReader(0).ToString()
              Catch ex As Exception
                If pFunctionParameters.IndexOf("called by") < 0 Then pFunctionParameters = pFunctionParameters & Environment.NewLine & " called by " & (New StackFrame(2)).GetMethod().Name
                pFault.LogException(ex, pFunctionParameters, "TRGT-160331-1206", vRequester)
              End Try
            Else
              rResponse = ""
            End If
          End If
          If Not String.IsNullOrEmpty(_InfoMessage) Then
            If String.IsNullOrEmpty(rResponse) Then
              rResponse = _InfoMessage
            Else
              rResponse = rResponse & Environment.NewLine & _InfoMessage
            End If
            _InfoMessage = ""
          End If

          'check if we had an error - errors that occur after a 'select' can only be seen if we "try" to get the next result set (we only have one real result set). 
          If myReader IsNot Nothing Then
            Try
              If Not myReader.NextResult = True Then
                myReader.Close()
              Else
                'I had an error here 
              End If
            Catch ex As Exception
              pFunctionParameters = pFunctionParameters & Environment.NewLine & " called by " & (New StackFrame(2)).GetMethod().Name
              pFault.LogException(50, ex, pFunctionParameters, "TRGT-151113-1405", vRequester)
            End Try
          End If

          myConn.Close()
          myReader = Nothing
        End Using
      End Using
    Catch ex As Exception
      Tools.LogToTextFile.WriteException("Untrapped Exception when running ExecuteScript", ex, "CC")
      Throw ex
    End Try

    Return pFault
  End Function

  Private Shared Sub OnInfoMessage(ByVal sender As Object, ByVal e As System.Data.SqlClient.SqlInfoMessageEventArgs)
    'I'm only using this for the script (not the SP's), since I cant see how it would work in a multi-user multi-threaded environment
    If String.IsNullOrEmpty(_InfoMessage) Then
      _InfoMessage = e.Message
    Else
      _InfoMessage &= Environment.NewLine & e.Message
    End If
  End Sub

  Friend Shared Function LogSQLFault(ByVal vFaultNo As Integer, ByVal vSQLException As SqlException, ByVal vFaultingCommand As String, ByVal vFaultingFunctionParameters As String, ByVal vIdent As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault


    Dim pCalledBy As String = (New StackFrame(3)).GetMethod().Name

    Dim pFreeText As String
    pFreeText = "SQL Exception: Command: " & vFaultingCommand & " called by " & pCalledBy & " ‡ "
    For i As Integer = 0 To vSQLException.Errors.Count - 1
      pFreeText &= "  " & (i + 1).ToString() & ".: Num: " & vSQLException.Errors(i).Number & "; " & vSQLException.Errors(i).Message & " ‡ "
      pFreeText &= "      " & "SP: " & vSQLException.Errors(i).Procedure & "; LineNoInSP:" & vSQLException.Errors(i).LineNumber & " ‡ "
      pFreeText &= "      " & "State: " & vSQLException.Errors(i).State & "; Severity:" & vSQLException.Errors(i).Class & " ‡ "
    Next i

    pFreeText &= "  Checking Inner Exceptions:" & " ‡ "
    Dim pEx As Exception = vSQLException
    Dim iCntr As Integer = 1
    Do Until pEx.InnerException Is Nothing
      iCntr += 1
      pEx = pEx.InnerException
      pFreeText &= " " & iCntr & ". " & pEx.Message & " ‡ "
    Loop

    If (vFaultNo = 49 OrElse (vFaultNo = 50 AndAlso vSQLException.Number = 229)) Then
      pFault = New clsFault
      Dim pMessage As String
      If vSQLException.Number = 229 Then
        pMessage = "SQL User has insufficient rights."
      Else
        pMessage = "Could not open SQL Server connection."
      End If
      pFault.SetAlertMessage(vMessage:=pMessage, vAction:="Check your text logs in " & MyController.LogLocation, vType:=clsEnums.enmFaultType.System, vSeverity:=clsEnums.enmFaultSeverity.SMS)
      Dim pFaultingApplication = (New StackFrame()).GetMethod().DeclaringType.Namespace()
      Dim pFaultingClass = (New StackFrame()).GetMethod().DeclaringType.Name()
      Dim pFaultingFunction = (New StackFrame(1)).GetMethod().Name
      Return pFault.LogFreeTextFault(vFaultNo, pFreeText, vFaultingFunctionParameters, "TRGT-190401-1639", vRequester, vManualFaultingAssembly:=pFaultingApplication, vManualFaultingClass:=pFaultingClass, vManualFaultingFunction:=pFaultingFunction)
    Else
      pFault.LogFreeTextFault(vFaultNo, pFreeText, vFaultingFunctionParameters, vIdent, vRequester, vManualFaultingAssembly:=(New StackFrame(2)).GetMethod().DeclaringType.Namespace(), vManualFaultingClass:=(New StackFrame(2)).GetMethod().DeclaringType.Name(), vManualFaultingFunction:=(New StackFrame(2)).GetMethod().Name)
    End If

    Return pFault
  End Function

  Friend Shared Sub EnsureConnectionIsOpen(ByRef rConn As SqlConnection)
    'we only enter here if we want to retry a query. 
    'However, if we are in a transaction, we should not try again! 
    If System.Transactions.Transaction.Current IsNot Nothing Then
      Throw New Exception("We are in a transaction, therefore we should not do a retry")
    End If
    If rConn.State <> ConnectionState.Open Then
      If rConn.State <> ConnectionState.Closed Then
        rConn.Close()
      End If
      rConn.Open()
    End If
  End Sub


  Private Shared Function LoadTargCCReader(ByVal vSQLReader As SqlDataReader, ByVal vRequester As clsRequester, ByRef rTargCCReader As csTargCCReader) As clsFault
    Dim pFault As New clsFault

    Dim pLastReadVariableName As String = ""
    Dim iRow As Integer = 0

    Try

      'Get the number fields
      pLastReadVariableName = "Loading VisibleFieldCount"
      Dim pNumFields As Integer = vSQLReader.VisibleFieldCount

      Dim pTargCCReaderHeader As New csTargCCReaderHeader(pNumFields)
      Dim pTargCCReaderData As New csTargCCReaderData(pTargCCReaderHeader)
      For iCntr As Integer = 0 To pNumFields - 1
        pTargCCReaderHeader.FieldName(iCntr) = vSQLReader.GetName(iCntr)
        Dim pSQLDataType As String = vSQLReader.GetDataTypeName(iCntr).ToLowerInvariant
        Select Case pSQLDataType
          Case "bigint"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Long
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.BigInt
          Case "bit"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Boolean
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Bit
          Case "char"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.String
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Char
          Case "datetime"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Date
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.DateTime
          Case "datetimeoffset"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.DateTimeOffset
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.DateTimeOffset
          Case "decimal"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Decimal
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Decimal
          Case "float"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Double
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Float
          Case "int"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Integer
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Int
          Case "money"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Decimal
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Money
          Case "nchar"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.String
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.NChar
          Case "ntext"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.String
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.NText
          Case "nvarchar"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.String
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.NVarChar
          Case "sql_variant"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.String
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.SQLVariant
          Case "real"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Single
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Real
          Case "text"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.String
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Text
          Case "tinyint"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Integer
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.TinyInt
          Case "varchar"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.String
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.VarChar
          Case "varbinary"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.ByteArray
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.VarBinary
          Case "date"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Date
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Date
          Case "time"
            pTargCCReaderHeader.DNVariableType(iCntr) = enmDNVariableType.Date
            pTargCCReaderHeader.SQLDataType(iCntr) = enmSQLDataType.Time
          Case Else
            Throw New Exception("I could not load data definition of the type " & vSQLReader.GetDataTypeName(iCntr).ToLower)
        End Select
      Next

      While vSQLReader.Read()
        Dim pTargCCReaderRowNew As New csTargCCReaderRow(pTargCCReaderHeader)

        For iCntr As Integer = 0 To pNumFields - 1
          pLastReadVariableName = "Getting Name "

          pLastReadVariableName = pTargCCReaderHeader.FieldName(iCntr)

          Dim pStrg As String = ""
          Select Case pTargCCReaderHeader.SQLDataType(iCntr)
            Case enmSQLDataType.BigInt
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.Long(iCntr) = vSQLReader.GetInt64(iCntr)
              Else
                pTargCCReaderRowNew.Long(iCntr) = 0
              End If
            Case enmSQLDataType.Bit
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.Boolean(iCntr) = vSQLReader.GetBoolean(iCntr)
              Else
                pTargCCReaderRowNew.Boolean(iCntr) = False
              End If
            Case enmSQLDataType.NVarChar,
                 enmSQLDataType.VarChar,
                 enmSQLDataType.Char,
                 enmSQLDataType.NChar,
                 enmSQLDataType.NText,
                 enmSQLDataType.Text
              If Not vSQLReader.IsDBNull(iCntr) Then
                pStrg = vSQLReader.GetString(iCntr)
                pTargCCReaderRowNew.String(iCntr) = pStrg
              Else
                pTargCCReaderRowNew.String(iCntr) = ""
              End If
            Case enmSQLDataType.SQLVariant
              If Not vSQLReader.IsDBNull(iCntr) Then
                pStrg = CType(vSQLReader(iCntr), String)
                pTargCCReaderRowNew.String(iCntr) = pStrg
              Else
                pTargCCReaderRowNew.String(iCntr) = ""
              End If
            Case enmSQLDataType.DateTime,
                 enmSQLDataType.Date,
                 enmSQLDataType.Time
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.Date(iCntr) = vSQLReader.GetDateTime(iCntr)
              Else
                pTargCCReaderRowNew.Date(iCntr) = Nothing
              End If
            Case enmSQLDataType.DateTimeOffset
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.DateTimeOffset(iCntr) = vSQLReader.GetDateTimeOffset(iCntr)
              Else
                pTargCCReaderRowNew.DateTimeOffset(iCntr) = Nothing
              End If
            Case enmSQLDataType.Decimal,
                 enmSQLDataType.Money
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.Decimal(iCntr) = vSQLReader.GetDecimal(iCntr)
              Else
                pTargCCReaderRowNew.Decimal(iCntr) = 0
              End If
            Case enmSQLDataType.Float
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.Double(iCntr) = vSQLReader.GetFloat(iCntr)
              Else
                pTargCCReaderRowNew.Double(iCntr) = 0
              End If
            Case enmSQLDataType.Int,
                 enmSQLDataType.TinyInt
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.Integer(iCntr) = vSQLReader.GetInt32(iCntr)
              Else
                pTargCCReaderRowNew.Integer(iCntr) = 0
              End If
            Case enmSQLDataType.Real
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.Single(iCntr) = vSQLReader.GetFloat(iCntr)
              Else
              End If
            Case enmSQLDataType.VarBinary
              If Not vSQLReader.IsDBNull(iCntr) Then
                pTargCCReaderRowNew.ByteArray(iCntr) = CType(vSQLReader(iCntr), Byte())
              Else
                pTargCCReaderRowNew.ByteArray(iCntr) = Nothing
              End If
            Case Else
              Throw New Exception("I could not load data Of the type " & vSQLReader.GetDataTypeName(iCntr).ToLower)
          End Select

        Next
        pTargCCReaderData.Add(pTargCCReaderRowNew)
        iRow += 1
      End While

      rTargCCReader = New csTargCCReader(pTargCCReaderData)

      pFault.SetOK()
      pLastReadVariableName = ""
    Catch ex As Exception
      Dim pMessage As String = String.Format("Row {0}, Item {1}. Called by {2}", iRow, pLastReadVariableName, (New StackFrame(3)).GetMethod().Name)
      pFault.LogException(ex, pMessage, "TRGT-090624-1143", vRequester)
    End Try


    Return pFault
  End Function

  Private Shared Function TranslateSQLDataType(ByVal vSQLDataType As enmSQLDataType) As SqlDbType
    Dim pSqlDbType As SqlDbType

    Select Case vSQLDataType
      Case enmSQLDataType.BigInt
        pSqlDbType = SqlDbType.BigInt


      Case enmSQLDataType.BigInt
        pSqlDbType = SqlDbType.BigInt
      Case enmSQLDataType.Bit
        pSqlDbType = SqlDbType.Bit
      Case enmSQLDataType.Char
        pSqlDbType = SqlDbType.Char
      Case enmSQLDataType.DateTime
        pSqlDbType = SqlDbType.DateTime
      Case enmSQLDataType.Decimal
        pSqlDbType = SqlDbType.Decimal
      Case enmSQLDataType.Float
        pSqlDbType = SqlDbType.Float
      Case enmSQLDataType.Int
        pSqlDbType = SqlDbType.Int
      Case enmSQLDataType.Money
        pSqlDbType = SqlDbType.Money
      Case enmSQLDataType.NChar
        pSqlDbType = SqlDbType.NChar
      Case enmSQLDataType.NText
        pSqlDbType = SqlDbType.NText
      Case enmSQLDataType.NVarChar
        pSqlDbType = SqlDbType.NVarChar
      Case enmSQLDataType.NVarCharMax
        pSqlDbType = SqlDbType.NVarChar
      Case enmSQLDataType.Real
        pSqlDbType = SqlDbType.Real
      Case enmSQLDataType.Text
        pSqlDbType = SqlDbType.Text
      Case enmSQLDataType.TinyInt
        pSqlDbType = SqlDbType.TinyInt
      Case enmSQLDataType.VarChar
        pSqlDbType = SqlDbType.VarChar
      Case enmSQLDataType.VarCharMax
        pSqlDbType = SqlDbType.VarChar
      Case enmSQLDataType.VarBinary
        pSqlDbType = SqlDbType.VarBinary
      Case enmSQLDataType.VarBinaryMax
        pSqlDbType = SqlDbType.VarBinary
      Case enmSQLDataType.Date
        pSqlDbType = SqlDbType.Date
      Case enmSQLDataType.DateTimeOffset
        pSqlDbType = SqlDbType.DateTimeOffset
      Case enmSQLDataType.Time
        pSqlDbType = SqlDbType.Time
      Case Else
        Throw New Exception("I can't translate the enmSQLDataType " & vSQLDataType.ToString())
    End Select
    Return pSqlDbType
  End Function

  Public Enum enmSQLDataType
    UD
    BigInt
    Bit
    [Char]
    DateTime
    [Decimal]
    Float
    Int
    Money
    NChar
    NText
    NVarChar
    NVarCharMax
    Real
    SQLVariant
    Text
    TinyInt
    VarChar
    VarCharMax
    VarBinary
    VarBinaryMax
    [Date]
    [Time]
    [DateTimeOffset]
  End Enum

  Public Enum enmDNVariableType
    UD
    [String]
    [Long]
    [Integer]
    [Boolean]
    [Enum]
    [Decimal]
    [Date]
    [DateTimeOffset]
    [Double]
    [Single]
    [ByteArray]
    Undefined
  End Enum

  Friend Class csTargCCParameter

    Friend Property Name As String
    Friend Property DataType As enmSQLDataType
    Friend Property Size As Integer
    Friend Property Value As Object

    Friend Sub New()
      CreateEmpty()
    End Sub
    Friend Sub New(ByVal vName As String, ByVal vDataType As enmSQLDataType, Optional ByVal vSize As Integer = 0)
      CreateEmpty()
      _Name = vName
      _DataType = vDataType
      _Size = vSize
    End Sub

    Private Sub CreateEmpty()
      _Name = ""
      _DataType = enmSQLDataType.UD
      _Size = 0
      _Value = Nothing
    End Sub

  End Class

  Friend Class csTargCCParameterCol
    Inherits Generic.List(Of csTargCCParameter)

    Friend Overloads Function Add(ByVal vName As String, ByVal vDataType As enmSQLDataType, Optional ByVal vSize As Integer = 0) As csTargCCParameter
      Dim pParameter As New csTargCCParameter(vName, vDataType, vSize)
      Me.Add(pParameter)
      Return pParameter
    End Function

    Public Overrides Function ToString() As String
      Dim pOut As New Text.StringBuilder
      For Each l As csTargCCParameter In Me
        pOut.AppendLine(l.Name & "='" & l.Value.ToString & "'")
      Next
      Return pOut.ToString()
    End Function

  End Class

  Public Class csTargCCReader
    Implements IDataReader

    Private _TargCCReaderData As csTargCCReaderData
    Private _TargCCReaderRow As csTargCCReaderRow

    Friend Sub New(ByVal vTargCCReaderData As csTargCCReaderData)
      If vTargCCReaderData Is Nothing Then Throw New Exception("TargCCReaderData is null!")
      _TargCCReaderData = vTargCCReaderData
      _TargCCReaderData.Reset()
    End Sub

    Public ReadOnly Property Depth As Integer Implements IDataReader.Depth
      Get
        Return 0
      End Get
    End Property

    Public ReadOnly Property FieldCount As Integer Implements IDataRecord.FieldCount
      Get
        Return _TargCCReaderData.Header.NumColumns
      End Get
    End Property

    Public ReadOnly Property HasRows As Boolean
      Get
        If Not (_TargCCReaderData Is Nothing) AndAlso (_TargCCReaderData.Count > 0) Then
          Return True
        Else
          Return False
        End If
      End Get
    End Property

    Public ReadOnly Property NumberOfRows As Integer
      Get
        If Not (_TargCCReaderData Is Nothing) AndAlso (_TargCCReaderData.Count > 0) Then
          Return _TargCCReaderData.Count
        Else
          Return 0
        End If
      End Get
    End Property


    Public ReadOnly Property IsClosed As Boolean Implements IDataReader.IsClosed
      Get
        Return False
      End Get
    End Property

    Default Public ReadOnly Property Item(name As String) As Object Implements IDataRecord.Item
      Get
        Dim pOrdinal As Integer = _TargCCReaderRow.GetOrdinal(name)
        Select Case _TargCCReaderRow.DNVariableType(pOrdinal)
          Case enmDNVariableType.Boolean
            Return _TargCCReaderRow.GetBoolean(pOrdinal)
          Case enmDNVariableType.ByteArray
            Return _TargCCReaderRow.GetByteArray(pOrdinal)
          Case enmDNVariableType.Date
            Return _TargCCReaderRow.GetDate(pOrdinal)
          Case enmDNVariableType.Decimal
            Return _TargCCReaderRow.GetDecimal(pOrdinal)
          Case enmDNVariableType.Double
            Return _TargCCReaderRow.GetDouble(pOrdinal)
          Case enmDNVariableType.Integer
            Return _TargCCReaderRow.GetInteger(pOrdinal)
          Case enmDNVariableType.Long
            Return _TargCCReaderRow.GetLong(pOrdinal)
          Case enmDNVariableType.Single
            Return _TargCCReaderRow.GetSingle(pOrdinal)
          Case enmDNVariableType.String
            Return _TargCCReaderRow.GetString(pOrdinal)
          Case Else
            Throw New Exception("Invalid variable type received: " & _TargCCReaderRow.DNVariableType(pOrdinal).ToString())
        End Select
      End Get
    End Property

    Default Public ReadOnly Property Item(i As Integer) As Object Implements IDataRecord.Item
      Get
        Select Case _TargCCReaderRow.DNVariableType(i)
          Case enmDNVariableType.Boolean
            Return _TargCCReaderRow.GetBoolean(i)
          Case enmDNVariableType.ByteArray
            Return _TargCCReaderRow.GetByteArray(i)
          Case enmDNVariableType.Date
            Return _TargCCReaderRow.GetDate(i)
          Case enmDNVariableType.Decimal
            Return _TargCCReaderRow.GetDecimal(i)
          Case enmDNVariableType.Double
            Return _TargCCReaderRow.GetDouble(i)
          Case enmDNVariableType.Integer
            Return _TargCCReaderRow.GetInteger(i)
          Case enmDNVariableType.Long
            Return _TargCCReaderRow.GetLong(i)
          Case enmDNVariableType.Single
            Return _TargCCReaderRow.GetSingle(i)
          Case enmDNVariableType.String
            Return _TargCCReaderRow.GetString(i)
          Case Else
            Throw New Exception("Invalid variable type received: " & _TargCCReaderRow.DNVariableType(i).ToString())
        End Select
      End Get
    End Property

    Public ReadOnly Property RecordsAffected As Integer Implements IDataReader.RecordsAffected
      Get
        Return -1
      End Get
    End Property

    Public Sub Close() Implements IDataReader.Close
      _TargCCReaderData = Nothing
      _TargCCReaderRow = Nothing
    End Sub

    Public Function GetBoolean(i As Integer) As Boolean Implements IDataRecord.GetBoolean
      Return _TargCCReaderRow.GetBoolean(i)
    End Function

    Public Function GetByte(i As Integer) As Byte Implements IDataRecord.GetByte
      Throw New NotImplementedException()
    End Function

    Public Function GetBytes(i As Integer, fieldOffset As Long, buffer() As Byte, bufferoffset As Integer, length As Integer) As Long Implements IDataRecord.GetBytes
      buffer = _TargCCReaderRow.GetByteArray(i)
      Return _TargCCReaderRow.GetByteArray(i).LongLength
    End Function

    Public Function GetChar(i As Integer) As Char Implements IDataRecord.GetChar
      Throw New NotImplementedException()
    End Function

    Public Function GetChars(i As Integer, fieldoffset As Long, buffer() As Char, bufferoffset As Integer, length As Integer) As Long Implements IDataRecord.GetChars
      Throw New NotImplementedException()
    End Function

    Public Function GetData(i As Integer) As IDataReader Implements IDataRecord.GetData
      Throw New NotImplementedException()
    End Function

    Public Function GetDataTypeName(i As Integer) As String Implements IDataRecord.GetDataTypeName
      Return _TargCCReaderRow.DNVariableType(i).ToString()
    End Function

    Public Function GetDateTime(i As Integer) As Date Implements IDataRecord.GetDateTime
      Return _TargCCReaderRow.GetDate(i)
    End Function

    Public Function GetDecimal(i As Integer) As Decimal Implements IDataRecord.GetDecimal
      Return _TargCCReaderRow.GetDecimal(i)
    End Function

    Public Function GetDouble(i As Integer) As Double Implements IDataRecord.GetDouble
      Return _TargCCReaderRow.GetDouble(i)
    End Function

    Public Function GetFieldType(i As Integer) As Type Implements IDataRecord.GetFieldType
      Throw New NotImplementedException()
    End Function

    Public Function GetFloat(i As Integer) As Single Implements IDataRecord.GetFloat
      Return _TargCCReaderRow.GetSingle(i)
    End Function

    Public Function GetGuid(i As Integer) As Guid Implements IDataRecord.GetGuid
      Throw New NotImplementedException()
    End Function

    Public Function GetInt16(i As Integer) As Short Implements IDataRecord.GetInt16
      Throw New NotImplementedException()
    End Function

    Public Function GetInt32(i As Integer) As Integer Implements IDataRecord.GetInt32
      Return _TargCCReaderRow.GetInteger(i)
    End Function

    Public Function GetInt64(i As Integer) As Long Implements IDataRecord.GetInt64
      Return _TargCCReaderRow.GetLong(i)
    End Function

    Public Function GetName(i As Integer) As String Implements IDataRecord.GetName
      Return _TargCCReaderRow.FieldName(i)
    End Function

    Public Function GetOrdinal(name As String) As Integer Implements IDataRecord.GetOrdinal
      Return _TargCCReaderRow.GetOrdinal(name)
    End Function

    Public Function GetSchemaTable() As DataTable Implements IDataReader.GetSchemaTable
      Throw New NotImplementedException()
    End Function

    Public Function GetString(i As Integer) As String Implements IDataRecord.GetString
      Return _TargCCReaderRow.GetString(i)
    End Function

    Public Function GetValue(i As Integer) As Object Implements IDataRecord.GetValue
      Return Item(i)
    End Function

    Public Function GetValues(values() As Object) As Integer Implements IDataRecord.GetValues
      ReDim values(_TargCCReaderRow.NumColumns - 1)
      For i = 0 To _TargCCReaderRow.NumColumns - 1
        values(i) = Item(i)
      Next
      Return _TargCCReaderRow.NumColumns
    End Function

    Public Function IsDBNull(i As Integer) As Boolean Implements IDataRecord.IsDBNull
      'I don't handle nulls
      Return False
    End Function

    Public Function NextResult() As Boolean Implements IDataReader.NextResult
      'I only handle 1 result set
      Return False
    End Function

    Public Function Read() As Boolean Implements IDataReader.Read
      Dim pIsNext As Boolean = _TargCCReaderData.ReadNext()
      If pIsNext = True Then
        _TargCCReaderRow = _TargCCReaderData.ActiveRow
      Else
        _TargCCReaderRow = Nothing
      End If
      Return pIsNext
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
      If Not disposedValue Then
        If disposing Then
          _TargCCReaderData = Nothing
          _TargCCReaderRow = Nothing
        End If

        _TargCCReaderData = Nothing
        _TargCCReaderRow = Nothing
      End If
      disposedValue = True
    End Sub


    Public Sub Dispose() Implements IDisposable.Dispose
      ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
      Dispose(True)
    End Sub
#End Region


  End Class

  Friend Class csTargCCReaderData
    Inherits Generic.List(Of csTargCCReaderRow)

    Private _index As Integer 'is zero based

    Friend Property Header As csTargCCReaderHeader

    Friend Sub New(ByVal vHeader As csTargCCReaderHeader)
      MyBase.New
      _index = -1
      _Header = vHeader
    End Sub

    Friend ReadOnly Property ActiveRow As csTargCCReaderRow
      Get
        Return Me(_index)
      End Get
    End Property

    Friend Function ReadNext() As Boolean
      _index += 1
      If _index = Me.Count Then
        Return False
      Else
        Return True
      End If
    End Function

    Friend Sub Reset()
      _index = -1
    End Sub

  End Class

  Friend Class csTargCCReaderHeader

    Private _NumColumns As Integer
    Private _DNVariableType As enmDNVariableType()
    Private _SQLDataType As enmSQLDataType()
    Private _FieldName As String()
    Private _IndexInDNVariableType As Integer()

    Private _NumStrings As Integer
    Private _NumLongs As Integer
    Private _NumIntegers As Integer
    Private _NumBooleans As Integer
    Private _NumDecimals As Integer
    Private _NumDates As Integer
    Private _NumDoubles As Integer
    Private _NumSingles As Integer
    Private _NumByteArrays As Integer

    Friend Property DNVariableType(ByVal vIndex As Integer) As enmDNVariableType
      Get
        Return _DNVariableType(vIndex)
      End Get
      Set(value As enmDNVariableType)
        _DNVariableType(vIndex) = value
        Select Case value
          Case enmDNVariableType.Boolean
            _NumBooleans += 1
            _IndexInDNVariableType(vIndex) = _NumBooleans - 1
          Case enmDNVariableType.ByteArray
            _NumByteArrays += 1
            _IndexInDNVariableType(vIndex) = _NumByteArrays - 1
          Case enmDNVariableType.Date
            _NumDates += 1
            _IndexInDNVariableType(vIndex) = _NumDates - 1
          Case enmDNVariableType.Decimal
            _NumDecimals += 1
            _IndexInDNVariableType(vIndex) = _NumDecimals - 1
          Case enmDNVariableType.Double
            _NumDoubles += 1
            _IndexInDNVariableType(vIndex) = _NumDoubles - 1
          Case enmDNVariableType.Integer
            _NumIntegers += 1
            _IndexInDNVariableType(vIndex) = _NumIntegers - 1
          Case enmDNVariableType.Long
            _NumLongs += 1
            _IndexInDNVariableType(vIndex) = _NumLongs - 1
          Case enmDNVariableType.Single
            _NumSingles += 1
            _IndexInDNVariableType(vIndex) = _NumSingles - 1
          Case enmDNVariableType.String
            _NumStrings += 1
            _IndexInDNVariableType(vIndex) = _NumStrings - 1
          Case Else
            Throw New Exception("Invalid variable type received: " & value.ToString())
        End Select
      End Set
    End Property

    Friend Property SQLDataType(ByVal vIndex As Integer) As enmSQLDataType
      Get
        Return _SQLDataType(vIndex)
      End Get
      Set(value As enmSQLDataType)
        _SQLDataType(vIndex) = value
      End Set
    End Property

    Friend ReadOnly Property IndexInDNVariableType(ByVal vIndex As Integer) As Integer
      Get
        Return _IndexInDNVariableType(vIndex)
      End Get
    End Property

    Friend Property FieldName(ByVal vIndex As Integer) As String
      Get
        Return _FieldName(vIndex)
      End Get
      Set(value As String)
        _FieldName(vIndex) = value
      End Set
    End Property

    Friend ReadOnly Property NumStrings As Integer
      Get
        Return _NumStrings
      End Get
    End Property
    Friend ReadOnly Property NumLongs As Integer
      Get
        Return _NumLongs
      End Get
    End Property
    Friend ReadOnly Property NumIntegers As Integer
      Get
        Return _NumIntegers
      End Get
    End Property
    Friend ReadOnly Property NumBooleans As Integer
      Get
        Return _NumBooleans
      End Get
    End Property

    Friend ReadOnly Property NumDecimals As Integer
      Get
        Return _NumDecimals
      End Get
    End Property
    Friend ReadOnly Property NumDates As Integer
      Get
        Return _NumDates
      End Get
    End Property
    Friend ReadOnly Property NumDoubles As Integer
      Get
        Return _NumDoubles
      End Get
    End Property
    Friend ReadOnly Property NumSingles As Integer
      Get
        Return _NumSingles
      End Get
    End Property
    Friend ReadOnly Property NumByteArrays As Integer
      Get
        Return _NumByteArrays
      End Get
    End Property

    Friend ReadOnly Property NumColumns As Integer
      Get
        Return _NumColumns
      End Get
    End Property


    Friend Function GetOrdinal(ByVal vFieldName As String) As Integer
      For iCntr = 0 To _NumColumns - 1
        If String.Equals(_FieldName(iCntr), vFieldName, StringComparison.OrdinalIgnoreCase) Then
          Return iCntr
        End If
      Next
      Return -1
    End Function

    Friend Sub New(ByVal vNumColumns As Integer)
      _NumColumns = vNumColumns
      ReDim _DNVariableType(_NumColumns - 1)
      ReDim _SQLDataType(_NumColumns - 1)
      ReDim _FieldName(_NumColumns - 1)
      ReDim _IndexInDNVariableType(_NumColumns - 1)

      _NumStrings = 0
      _NumLongs = 0
      _NumIntegers = 0
      _NumBooleans = 0
      _NumDecimals = 0
      _NumDates = 0
      _NumDoubles = 0
      _NumSingles = 0
      _NumByteArrays = 0
    End Sub

  End Class

  Friend Class csTargCCReaderRow

    Private _NumColumns As Integer
    Private _Header As csTargCCReaderHeader

    Private _String As String()
    Private _Long As Long()
    Private _Integer As Integer()
    Private _Boolean As Boolean()
    Private _Decimal As Decimal()
    Private _Date As Date()
    Private _DateTimeOffset As DateTimeOffset()
    Private _Double As Double()
    Private _Single As Single()
    Private _ByteArray As Byte()()

    Friend ReadOnly Property NumColumns As Integer
      Get
        Return _NumColumns
      End Get
    End Property

    Friend ReadOnly Property FieldName(ByVal vIndex As Integer) As String
      Get
        Return _Header.FieldName(vIndex)
      End Get
    End Property

    Friend ReadOnly Property DNVariableType(ByVal vIndex As Integer) As enmDNVariableType
      Get
        Return _Header.DNVariableType(vIndex)
      End Get
    End Property

    Friend ReadOnly Property SQLDataType(ByVal vIndex As Integer) As enmSQLDataType
      Get
        Return _Header.SQLDataType(vIndex)
      End Get
    End Property

    Friend Function GetOrdinal(ByVal vFieldName As String) As Integer
      For iCntr = 0 To _NumColumns - 1
        If String.Equals(_Header.FieldName(iCntr), vFieldName, StringComparison.OrdinalIgnoreCase) Then
          Return iCntr
        End If
      Next
      Return -1
    End Function


    Friend WriteOnly Property [String](ByVal vIndex As Integer) As String
      Set(value As String)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.String Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _String(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [Long](ByVal vIndex As Integer) As Long
      Set(value As Long)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.Long Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _Long(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [Integer](ByVal vIndex As Integer) As Integer
      Set(value As Integer)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.Integer Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _Integer(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [Boolean](ByVal vIndex As Integer) As Boolean
      Set(value As Boolean)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.Boolean Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _Boolean(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [Decimal](ByVal vIndex As Integer) As Decimal
      Set(value As Decimal)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.Decimal Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _Decimal(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [Date](ByVal vIndex As Integer) As Date
      Set(value As Date)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.Date Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _Date(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [DateTimeOffset](ByVal vIndex As Integer) As DateTimeOffset
      Set(value As DateTimeOffset)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.DateTimeOffset Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _DateTimeOffset(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [Double](ByVal vIndex As Integer) As Double
      Set(value As Double)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.Double Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _Double(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [Single](ByVal vIndex As Integer) As Single
      Set(value As Single)
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.Single Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _Single(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property
    Friend WriteOnly Property [ByteArray](ByVal vIndex As Integer) As Byte()
      Set(value As Byte())
        If _Header.DNVariableType(vIndex) <> enmDNVariableType.ByteArray Then
          Throw New Exception("Cast Failed. I expected a " & _Header.DNVariableType(vIndex).ToString())
        End If
        _ByteArray(_Header.IndexInDNVariableType(vIndex)) = value
      End Set
    End Property

    Friend Sub New(ByVal vHeader As csTargCCReaderHeader)
      _NumColumns = vHeader.NumColumns
      _Header = vHeader

      If vHeader.NumStrings > 0 Then ReDim _String(vHeader.NumStrings - 1)
      If vHeader.NumLongs > 0 Then ReDim _Long(vHeader.NumLongs - 1)
      If vHeader.NumIntegers > 0 Then ReDim _Integer(vHeader.NumIntegers - 1)
      If vHeader.NumBooleans > 0 Then ReDim _Boolean(vHeader.NumBooleans - 1)
      If vHeader.NumDecimals > 0 Then ReDim _Decimal(vHeader.NumDecimals - 1)
      If vHeader.NumDates > 0 Then ReDim _Date(vHeader.NumDates - 1)
      If vHeader.NumDoubles > 0 Then ReDim _Double(vHeader.NumDoubles - 1)
      If vHeader.NumSingles > 0 Then ReDim _Single(vHeader.NumSingles - 1)
      If vHeader.NumByteArrays > 0 Then ReDim _ByteArray(vHeader.NumByteArrays - 1)

    End Sub

    Friend Function GetString(ByVal vIndex As Integer) As String
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.String Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _String(_Header.IndexInDNVariableType(vIndex))
    End Function
    Friend Function GetLong(ByVal vIndex As Integer) As Long
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.Long Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _Long(_Header.IndexInDNVariableType(vIndex))
    End Function
    Friend Function GetInteger(ByVal vIndex As Integer) As Integer
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.Integer Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _Integer(_Header.IndexInDNVariableType(vIndex))
    End Function
    Friend Function GetBoolean(ByVal vIndex As Integer) As Boolean
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.Boolean Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _Boolean(_Header.IndexInDNVariableType(vIndex))
    End Function
    Friend Function GetDecimal(ByVal vIndex As Integer) As Decimal
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.Decimal Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _Decimal(_Header.IndexInDNVariableType(vIndex))
    End Function
    Friend Function GetDate(ByVal vIndex As Integer) As Date
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.Date Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _Date(_Header.IndexInDNVariableType(vIndex))
    End Function
    Friend Function GetDouble(ByVal vIndex As Integer) As Double
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.Double Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _Double(_Header.IndexInDNVariableType(vIndex))
    End Function
    Friend Function GetSingle(ByVal vIndex As Integer) As Single
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.Single Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _Single(_Header.IndexInDNVariableType(vIndex))
    End Function
    Friend Function GetByteArray(ByVal vIndex As Integer) As Byte()
      If _Header.DNVariableType(vIndex) <> enmDNVariableType.ByteArray Then
        Throw New Exception(String.Format("Cast Failed for {0}. I'm holding a {1}", _Header.FieldName(vIndex), _Header.DNVariableType(vIndex).ToString()))
      End If
      Return _ByteArray(_Header.IndexInDNVariableType(vIndex))
    End Function

  End Class

End Class

