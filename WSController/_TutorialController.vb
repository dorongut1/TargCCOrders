'See also "Partial Class MyController" at the end of this file

Public Class TutorialController

  'This tutorial will show you how to create a function of your own to implement your business logic

  'Sample to get a collection based on input
  Public Shared Function csLoggedAlertCol_FillByBoundedFaultNumber(ByVal vFaultNumberFrom As Integer, ByVal vFaultNumberTo As Integer, ByVal vRequester As clsRequester, ByRef rLoggedAlerts As csLoggedAlertCol, ByRef rCount As Integer) As clsFault
    Dim pFunctionParameters As String = String.Format($"vFaultNumberFrom: {vFaultNumberFrom}, FaultNumberTo: {vFaultNumberTo}")
    Dim pFault As clsFault

    'erase or initialize referenced values
    rLoggedAlerts = Nothing
    rCount = 0

    Dim pClass As String = "csLoggedAlertCol"
    Dim pFunction As String = "FillByBoundedFaultNumber"
    Dim pWSALParameters As New ccWSAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters 
      pLastReadVariableName = "FaultNumberFrom" 'Name sure the text matches !!
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Integer).Value = vFaultNumberFrom
      pLastReadVariableName = "FaultNumberTo"
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Integer).Value = vFaultNumberTo
      pLastReadVariableName = "HowMany"
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Integer).Value = 0
      pLastReadVariableName = "Dir"
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Enum).Value = clsEnums.enmFillDirection.DESC
      pLastReadVariableName = "WithParent"
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Enum).Value = clsEnums.enmLoadParent.DoNotLoad
      pLastReadVariableName = ""

      'Execute query 
      Dim pResults As ccWSAL.csTargCCParameterCol = Nothing
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK AndAlso pResults Is Nothing Then Return pFault 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'get the response
      pLastReadVariableName = "LoggedAlerts"
      rLoggedAlerts = New csLoggedAlertCol(DirectCast(pResults(pLastReadVariableName).Value, Byte()), pFault, vRequester) : If Not pFault.isOK Then Return pFault
      pLastReadVariableName = "Count"
      rCount = DirectCast(pResults(pLastReadVariableName).Value, Integer)
      pLastReadVariableName = ""

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      Return New clsFault(ex, pFunctionParameters, "TRGT-200909-1031", vRequester)
    End Try

    Return pFault
  End Function

  'Sample to get a list of Alerts with error 41 and 50 in the past month - returns 2 lists of LoggedAlerts, the month and the number in each error type
  Public Shared Function GetAlertListsFromFunction(ByVal vMonth As Date, ByVal vRequester As clsRequester, ByRef rError41List As csLoggedAlertCol, ByRef rError50List As csLoggedAlertCol, ByRef rMonth As Date, ByRef rNum41 As Integer, ByRef rNum50 As Integer) As clsFault
    Dim pFunctionParameters As String = $"Month: {vMonth:dd-MMM-yy}"
    Dim pFault As clsFault

    'erase or initialize referenced values
    rError41List = Nothing
    rError50List = Nothing
    rMonth = Nothing
    rNum41 = 0
    rNum50 = 0

    Dim pClass As String = "TutorialController"
    Dim pFunction As String = "GetAlertListsFromFunction"
    Dim pWSALParameters As New ccWSAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters 
      pLastReadVariableName = "Month" 'Name sure the text matches !!
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Date).Value = vMonth
      pLastReadVariableName = ""

      'Execute query 
      Dim pResults As ccWSAL.csTargCCParameterCol = Nothing
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK AndAlso pResults Is Nothing Then Return pFault 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'get the response
      pLastReadVariableName = "Error41List"
      rError41List = New csLoggedAlertCol(DirectCast(pResults(pLastReadVariableName).Value, Byte()), pFault, vRequester) : If Not pFault.isOK Then Return pFault
      pLastReadVariableName = "Error50List"
      rError50List = New csLoggedAlertCol(DirectCast(pResults(pLastReadVariableName).Value, Byte()), pFault, vRequester) : If Not pFault.isOK Then Return pFault
      pLastReadVariableName = "MonthOut"
      rMonth = DirectCast(pResults(pLastReadVariableName).Value, Date)
      pLastReadVariableName = "Num41"
      rNum41 = DirectCast(pResults(pLastReadVariableName).Value, Integer)
      pLastReadVariableName = "Num50"
      rNum50 = DirectCast(pResults(pLastReadVariableName).Value, Integer)
      pLastReadVariableName = ""

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      Return New clsFault(ex, pFunctionParameters, "TRGT-200909-1031", vRequester)
    End Try

    Return pFault
  End Function

  'This function shows how to run a Stored Procedure, from scratch
  Public Shared Function CreateReportPayment(ByVal vMinimumReimbursalAmount As Decimal, ByVal vPaymentMonth As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MinimumReimbursalAmount={0}, PaymentMonth={1}", vMinimumReimbursalAmount, vPaymentMonth.ToString("dd-MMM-yyyy HH:mm:ss"))
    Dim pFault As clsFault

    'erase or initialize referenced values

    Dim pClass As String = "TutorialController"
    Dim pFunction As String = "CreateReportPayment"
    Dim pWSALParameters As New ccWSAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters 
      pLastReadVariableName = "MinimumReimbursalAmount" 'Name sure the text matches !!
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Decimal).Value = vMinimumReimbursalAmount
      pLastReadVariableName = "PaymentMonth"
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Date).Value = vPaymentMonth
      pLastReadVariableName = ""

      'Execute query 
      Dim pResults As ccWSAL.csTargCCParameterCol = Nothing
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK Then Return pFault

      'get the response

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      Return New clsFault(ex, pFunctionParameters, "TRGT-200909-1031", vRequester)
    End Try

    Return pFault
  End Function

  'This function shows how to run a Stored Procedure, from scratch, returning a collection as a response, using the pTargCCReader
  Public Shared Function GetDatabaseFileSizes(ByVal vRequester As clsRequester, ByRef rDBName As List(Of String), ByRef rFileName As List(Of String), ByRef rType As List(Of String), ByRef rCurrentSize As List(Of Integer), ByRef rFreeSpace As List(Of Integer)) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault

    'erase or initialize referenced values
    rDBName = New List(Of String)
    rFileName = New List(Of String)
    rType = New List(Of String)
    rCurrentSize = New List(Of Integer)
    rFreeSpace = New List(Of Integer)

    Dim pClass As String = "TutorialController"
    Dim pFunction As String = "GetDatabaseFileSizes"
    Dim pWSALParameters As New ccWSAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters 
      'pLastReadVariableName = "Month" 'Name sure the text matches !!
      'pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Date).Value = vMonth
      pLastReadVariableName = ""

      'Execute query 
      Dim pResults As ccWSAL.csTargCCParameterCol = Nothing
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK AndAlso pResults Is Nothing Then Return pFault 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'get the response
      pLastReadVariableName = "DBName"
      rDBName = rDBName.FromByteArray(DirectCast(pResults(pLastReadVariableName).Value, Byte()))
      pLastReadVariableName = "FileName"
      rFileName = rFileName.FromByteArray(DirectCast(pResults(pLastReadVariableName).Value, Byte()))
      pLastReadVariableName = "Type"
      rType = rType.FromByteArray(DirectCast(pResults(pLastReadVariableName).Value, Byte()))
      pLastReadVariableName = "CurrentSize"
      rCurrentSize = rCurrentSize.FromByteArray(DirectCast(pResults(pLastReadVariableName).Value, Byte()))
      pLastReadVariableName = "FreeSpace"
      rFreeSpace = rFreeSpace.FromByteArray(DirectCast(pResults(pLastReadVariableName).Value, Byte()))
      pLastReadVariableName = ""

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      Return New clsFault(ex, pFunctionParameters, "TRGT-200909-1031", vRequester)
    End Try

    Return pFault
  End Function

  'This function shows how to run a Stored Procedure, using ccHelper
  Public Shared Function CreateReportPaymentUsingCcHelper(ByVal vMinimumReimbursalAmount As Decimal, ByVal vPaymentMonth As Date, ByVal vRequester As clsRequester, ByRef rResponse As String) As clsFault
    Dim pFunctionParameters As String = String.Format("MinimumReimbursalAmount={0}, PaymentMonth={1}", vMinimumReimbursalAmount, vPaymentMonth.ToString("dd-MMM-yyyy HH:mm:ss"))
    Dim pFault As clsFault

    'erase or initialize referenced values
    rResponse = ""

    Dim pClass As String = "TutorialController"
    Dim pFunction As String = "CreateReportPaymentUsingCcHelper"
    Dim pWSALParameters As New ccWSAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters 
      pLastReadVariableName = "MinimumReimbursalAmount" 'Name sure the text matches !!
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Decimal).Value = vMinimumReimbursalAmount
      pLastReadVariableName = "PaymentMonth"
      pWSALParameters.Add(pLastReadVariableName, ccWSAL.enmDNVariableType.Date).Value = vPaymentMonth
      pLastReadVariableName = ""

      'Execute query 
      Dim pResults As ccWSAL.csTargCCParameterCol = Nothing
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK AndAlso pResults Is Nothing Then Return pFault 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'get the response
      pLastReadVariableName = "Response"
      rResponse = DirectCast(pResults(pLastReadVariableName).Value, String)
      pLastReadVariableName = ""

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      Return New clsFault(ex, pFunctionParameters, "TRGT-200909-1031", vRequester)
    End Try

    Return pFault
  End Function

  'Sample to get a list of Users that logged in yesterday. Returns yesterday's date and a combolist with UserID and UserNames
  Public Shared Function GetYesterdaysLoggedinUsers(ByVal vRequester As clsRequester, ByRef rYesterdaysDate As Date, ByRef rLoggedInUsers As clsComboList) As clsFault
    Dim pFunctionParameters As String = $""
    Dim pFault As clsFault

    'erase or initialize referenced values
    rYesterdaysDate = Nothing
    rLoggedInUsers = Nothing

    Dim pClass As String = "TutorialController"
    Dim pFunction As String = "GetYesterdaysLoggedinUsers"
    Dim pWSALParameters As New ccWSAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters 

      'Execute query 
      Dim pResults As ccWSAL.csTargCCParameterCol = Nothing
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK AndAlso pResults Is Nothing Then Return pFault 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'get the response
      pLastReadVariableName = "YesterdaysDate"
      rYesterdaysDate = DirectCast(pResults(pLastReadVariableName).Value, Date)
      pLastReadVariableName = "LoggedInUsers"
      rLoggedInUsers = New clsComboList(DirectCast(pResults(pLastReadVariableName).Value, Byte()), pFault, vRequester) : If Not pFault.isOK Then Return pFault
      pLastReadVariableName = ""

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      Return New clsFault(ex, pFunctionParameters, "TRGT-200909-1031", vRequester)
    End Try

    Return pFault
  End Function

  'Sample to create a "Mail"  entry (returns nothing)
  Public Shared Function CreateDummyMail(ByVal vRequester As clsRequester, ByRef rMail As csMail) As clsFault
    Dim pFunctionParameters As String = $""
    Dim pFault As clsFault

    'erase or initialize referenced values
    rMail = Nothing

    Dim pClass As String = "TutorialController"
    Dim pFunction As String = "CreateDummyMail"
    Dim pWSALParameters As New ccWSAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters 

      'Execute query 
      Dim pResults As ccWSAL.csTargCCParameterCol = Nothing
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK AndAlso pResults Is Nothing Then Return pFault 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'get the response
      pLastReadVariableName = "Mail"
      rMail = New csMail(DirectCast(pResults(pLastReadVariableName).Value, Byte()), pFault, vRequester) : If Not pFault.isOK Then Return pFault
      pLastReadVariableName = ""

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      Return New clsFault(ex, pFunctionParameters, "TRGT-200909-1031", vRequester)
    End Try

    Return pFault
  End Function

  ''' <summary>
  ''' This shows how the ApproveViaWebPage is used
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="rTop10Users"></param>
  ''' <returns></returns>
  Public Shared Function GetTop10UsersAfterApproval(vRequester As clsRequester, ByRef rTop10Users As clsComboList) As clsFault
    Dim pFunctionParameters As String = $""
    Dim pFault As New clsFault

    'erase or initialize referenced values
    rTop10Users = Nothing

    Dim pClass As String = "TutorialController"
    Dim pFunction As String = "GetTop10UsersAfterApproval"
    Dim pWSALParameters As New ccWSAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters 

      'Execute query 
      Dim pResults As ccWSAL.csTargCCParameterCol = Nothing
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK AndAlso pResults Is Nothing Then Return pFault 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'get the response
      pLastReadVariableName = "Top10Users"
      rTop10Users = New clsComboList(DirectCast(pResults(pLastReadVariableName).Value, Byte()), pFault, vRequester) : If Not pFault.isOK Then Return pFault
      pLastReadVariableName = ""

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      Return New clsFault(ex, pFunctionParameters, "TRGT-200909-1031", vRequester)
    End Try

    Return pFault
  End Function


End Class

Partial Class MyController

  'If you are saving the config data in code, then 
  '  follow the example below 

  Private Shared _SampleValue As String

  'Use "friend" if you want it to be visible only in the Controller object 
  Friend Shared ReadOnly Property SampleValue() As String
    Get
      If String.IsNullOrEmpty(_SampleValue) = True Then
        'LoadSampleValue() 
      End If
      Return _SampleValue
    End Get
  End Property

End Class
