'See also "Partial Class MyController" at the end of this file

Partial Friend Class csFunctions

  'This tutorial will show you how to create a function of your own to implement your business logic

  Private Shared Sub csFunctions_evtDoExternalUserFunctionTutorial(ByVal vClass As String, ByVal vFunction As String, vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault) Handles Me.evtDoExternalUserFunction

    Try
      If vClass = "csLoggedAlertCol" AndAlso vFunction = "FillByBoundedFaultNumber" Then
        csLoggedAlertCol_FillByBoundedFaultNumber(vRequest, rResponse, vRequester, rFault)

      ElseIf vClass = "TutorialController" AndAlso vFunction = "GetAlertListsFromFunction" Then
        TutorialController_GetAlertListsFromFunction(vRequest, rResponse, vRequester, rFault)

      ElseIf vClass = "TutorialController" AndAlso vFunction = "CreateReportPayment" Then
        TutorialController_CreateReportPayment(vRequest, rResponse, vRequester, rFault)

      ElseIf vClass = "TutorialController" AndAlso vFunction = "GetDatabaseFileSizes" Then
        TutorialController_GetDatabaseFileSizes(vRequest, rResponse, vRequester, rFault)

      ElseIf vClass = "TutorialController" AndAlso vFunction = "CreateReportPaymentUsingCcHelper" Then
        TutorialController_CreateReportPaymentUsingCcHelper(vRequest, rResponse, vRequester, rFault)

      ElseIf vClass = "TutorialController" AndAlso vFunction = "GetYesterdaysLoggedinUsers" Then
        TutorialController_GetYesterdaysLoggedinUsers(vRequest, rResponse, vRequester, rFault)

      ElseIf vClass = "TutorialController" AndAlso vFunction = "GetTop10UsersAfterApproval" Then
        TutorialController_GetTop10UsersAfterApproval(vRequest, rResponse, vRequester, rFault)

      ElseIf vClass = "TutorialController" AndAlso vFunction = "CreateDummyMail" Then
        TutorialController_CreateDummyMail(vRequest, rResponse, vRequester, rFault)

      End If
    Catch ex As Exception
      Throw New Exception($"In 'csFunctions_evtDoExternalUserFunction' Class:{vClass}, Function {vFunction}, Problem: {ex.Message}", ex)
    End Try

  End Sub

  'Sample to get a collection based on input
  Private Shared Sub csLoggedAlertCol_FillByBoundedFaultNumber(ByVal vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault)

    Dim pVariable As String = ""
    Try
      'get the parameters
      pVariable = "FaultNumberFrom"
      Dim pFaultNumberFrom As Integer = DirectCast(vRequest(pVariable).Value, Integer)
      pVariable = "FaultNumberTo"
      Dim pFaultNumberTo As Integer = DirectCast(vRequest(pVariable).Value, Integer)
      pVariable = "HowMany"
      Dim pHowMany As Integer = DirectCast(vRequest(pVariable).Value, Integer)
      pVariable = "Dir"
      Dim pDir As clsEnums.enmFillDirection = DirectCast(vRequest(pVariable).Value, clsEnums.enmFillDirection)
      pVariable = "WithParent"
      Dim pWithParents As clsEnums.enmLoadParent = DirectCast(vRequest(pVariable).Value, clsEnums.enmLoadParent)
      pVariable = ""

      'Create the instance
      Dim pLoggedAlerts As New csLoggedAlertCol(pWithParents)

      'Execute the function
      rFault = pLoggedAlerts.FillByBoundedFaultNumber(pFaultNumberFrom, pFaultNumberTo, vRequester, pHowMany, pDir)
      If Not rFault.isOK AndAlso (pLoggedAlerts Is Nothing) Then Return 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'Load the response
      rResponse = New ccWSAL.csTargCCParameterCol
      'the collection
      pVariable = "LoggedAlerts"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pLoggedAlerts.CreateByteArray(rFault, vRequester) : If Not rFault.isOK() Then Return
      'now add anything else you like
      pVariable = "Count"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.Integer).Value = pLoggedAlerts.Count
      pVariable = ""

    Catch ex As Exception
      Throw New Exception($"Variable: {pVariable}, Problem: {ex.Message}", ex)
    End Try

  End Sub

  'Sample to get a list of Alerts with error 41 and 50 in the past month - returns 2 lists of LoggedAlerts, the month and the number in each error type
  Private Shared Sub TutorialController_GetAlertListsFromFunction(ByVal vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault)

    Dim pVariable As String = ""
    Try
      'get the parameters
      pVariable = "Month"
      Dim pMonth As Date = DirectCast(vRequest(pVariable).Value, Date)
      pVariable = ""

      Dim pError41List As csLoggedAlertCol = Nothing
      Dim pError50List As csLoggedAlertCol = Nothing
      Dim pMonthOut As Date
      Dim pNum41 As Integer
      Dim pNum50 As Integer

      'Execute the function
      rFault = TutorialController.GetAlertListsFromFunction(pMonth, vRequester, pError41List, pError50List, pMonthOut, pNum41, pNum50)
      If Not rFault.isOK() AndAlso (pError41List Is Nothing OrElse pError50List Is Nothing) Then Return 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'Load the response
      rResponse = New ccWSAL.csTargCCParameterCol
      'the collection
      pVariable = "Error41List"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pError41List.CreateByteArray(rFault, vRequester) : If Not rFault.isOK() Then Return
      pVariable = "Error50List"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pError50List.CreateByteArray(rFault, vRequester) : If Not rFault.isOK() Then Return
      pVariable = "MonthOut"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.Date).Value = pMonthOut
      pVariable = "Num41"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.Integer).Value = pNum41
      pVariable = "Num50"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.Integer).Value = pNum50
      pVariable = ""

    Catch ex As Exception
      Throw New Exception($"Variable: {pVariable}, Problem: {ex.Message}", ex)
    End Try

  End Sub

  'This function shows how to run a Stored Procedure, from scratch
  Public Shared Sub TutorialController_CreateReportPayment(ByVal vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault)


    Dim pVariable As String = ""
    Try
      'get the parameters
      pVariable = "MinimumReimbursalAmount"
      Dim pMinimumReimbursalAmount As Decimal = DirectCast(vRequest(pVariable).Value, Decimal)
      pVariable = "PaymentMonth"
      Dim pPaymentMonth As Date = DirectCast(vRequest(pVariable).Value, Date)
      pVariable = ""

      'Execute the function
      rFault = TutorialController.CreateReportPayment(pMinimumReimbursalAmount, pPaymentMonth, vRequester) : If Not rFault.isOK() Then Return

      'nothing returned, so no need to create parameters
    Catch ex As Exception
      Throw New Exception($"Variable: {pVariable}, Problem: {ex.Message}", ex)
    End Try

  End Sub

  Private Shared Sub TutorialController_GetDatabaseFileSizes(ByVal vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault)

    Dim pVariable As String = ""
    Try
      'get the parameters
      'pVariable = "FaultNumberFrom" 'None
      'Dim pFaultNumberFrom As Integer = DirectCast(vRequest(pVariable).Value, Integer)
      pVariable = ""

      'Create the instance
      Dim pDBName = New List(Of String)
      Dim pFileName = New List(Of String)
      Dim pType = New List(Of String)
      Dim pCurrentSize = New List(Of Integer)
      Dim pFreeSpace = New List(Of Integer)

      'Execute the function
      rFault = ccDatabaseMaintenance.GetDatabaseFileSizes(vRequester, pDBName, pFileName, pType, pCurrentSize, pFreeSpace)
      If Not rFault.isOK AndAlso (pDBName Is Nothing OrElse pFileName Is Nothing OrElse pType Is Nothing OrElse pCurrentSize Is Nothing OrElse pFreeSpace Is Nothing) Then Return 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'Load the response
      rResponse = New ccWSAL.csTargCCParameterCol

      'the collection
      pVariable = "DBName"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pDBName.ToByteArray()
      pVariable = "FileName"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pFileName.ToByteArray()
      pVariable = "Type"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pType.ToByteArray()
      pVariable = "CurrentSize"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pCurrentSize.ToByteArray()
      pVariable = "FreeSpace"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pFreeSpace.ToByteArray()
      pVariable = ""

    Catch ex As Exception
      Throw New Exception($"Variable: {pVariable}, Problem: {ex.Message}", ex)
    End Try

  End Sub

  'This function shows how to run a Stored Procedure, using ccHelper
  Public Shared Sub TutorialController_CreateReportPaymentUsingCcHelper(ByVal vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault)

    Dim pVariable As String = ""
    Try
      'get the parameters
      pVariable = "MinimumReimbursalAmount"
      Dim pMinimumReimbursalAmount As Decimal = DirectCast(vRequest(pVariable).Value, Decimal)
      pVariable = "PaymentMonth"
      Dim pPaymentMonth As Date = DirectCast(vRequest(pVariable).Value, Date)
      pVariable = ""

      Dim pResponse As String = ""
      'Execute the function
      rFault = TutorialController.CreateReportPaymentUsingCcHelper(pMinimumReimbursalAmount, pPaymentMonth, vRequester, pResponse)
      If Not rFault.isOK AndAlso (pResponse Is Nothing) Then Return 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'Load the response
      rResponse = New ccWSAL.csTargCCParameterCol()
      pVariable = "Response"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.String).Value = pResponse
      pVariable = ""

    Catch ex As Exception
      Throw New Exception($"Variable: {pVariable}, Problem: {ex.Message}", ex)
    End Try

  End Sub

  'Sample to get a list of Users that logged in yesterday. Returns yesterday's date and a combolist with UserID and UserNames
  Public Shared Sub TutorialController_GetYesterdaysLoggedinUsers(ByVal vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault)

    Dim pVariable As String = ""
    Try
      'get the parameters
      pVariable = ""

      Dim pYesterdaysDate As Date = Nothing
      Dim pLoggedInUsers As clsComboList = Nothing

      'Execute the function
      rFault = TutorialController.GetYesterdaysLoggedinUsers(vRequester, pYesterdaysDate, pLoggedInUsers)
      If Not rFault.isOK AndAlso (pYesterdaysDate = Nothing OrElse pLoggedInUsers Is Nothing) Then Return 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'Load the response
      rResponse = New ccWSAL.csTargCCParameterCol()
      pVariable = "YesterdaysDate"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.Date).Value = pYesterdaysDate
      pVariable = "LoggedInUsers"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pLoggedInUsers.CreateByteArray(rFault, vRequester) : If Not rFault.isOK() Then Return
      pVariable = ""
    Catch ex As Exception
      Throw New Exception($"Variable: {pVariable}, Problem: {ex.Message}", ex)
    End Try

  End Sub

  'Sample to create a "Mail"  entry (returns nothing)
  Public Shared Sub TutorialController_CreateDummyMail(ByVal vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault)

    Dim pVariable As String = ""
    Try
      'get the parameters
      pVariable = ""

      Dim pMail As csMail = Nothing

      'Execute the function
      rFault = TutorialController.CreateDummyMail(vRequester, pMail) : If Not rFault.isOK() Then Return
      If Not rFault.isOK AndAlso (pMail Is Nothing) Then Return 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'Load the response
      rResponse = New ccWSAL.csTargCCParameterCol()
      pVariable = "Mail"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pMail.CreateByteArray(rFault, vRequester) : If Not rFault.isOK() Then Return
      pVariable = ""
    Catch ex As Exception
      Throw New Exception($"Variable: {pVariable}, Problem: {ex.Message}", ex)
    End Try

  End Sub

  'Sample to show how the ApproveViaWebPage is used
  Public Shared Sub TutorialController_GetTop10UsersAfterApproval(ByVal vRequest As ccWSAL.csTargCCParameterCol, ByRef rResponse As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rFault As clsFault)

    Dim pVariable As String = ""
    Try
      'get the parameters
      pVariable = ""

      Dim pTop10Users As clsComboList = Nothing

      'Execute the function
      rFault = TutorialController.GetTop10UsersAfterApproval(vRequester, pTop10Users)
      If Not rFault.isOK AndAlso (pTop10Users Is Nothing) Then Return 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'

      'Load the response
      rResponse = New ccWSAL.csTargCCParameterCol()
      pVariable = "Top10Users"
      rResponse.Add(pVariable, ccWSAL.enmDNVariableType.ByteArray).Value = pTop10Users.CreateByteArray(rFault, vRequester) : If Not rFault.isOK() Then Return
      pVariable = ""
    Catch ex As Exception
      Throw New Exception($"Variable: {pVariable}, Problem: {ex.Message}", ex)
    End Try

  End Sub


End Class

Partial Friend Class MyConfig

  'Use this to enter the config data. 
  'This is great if you obfuscate the code 

  'Add anything else you'd want, here 
  'Public Shared SignatureLocation As String = "D:\Logs\ExchangeNetPhotos" 

  Private Shared _SampleKeyFromConfig As String
  Friend Shared ReadOnly Property SampleKeyFromConfig() As String
    Get
      Return _SampleKeyFromConfig
    End Get
  End Property

End Class
