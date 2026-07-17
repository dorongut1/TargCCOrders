'See also "Partial Class MyController" at the end of this file

Public Class TutorialController

  'This tutorial will show you how to create a function of your own to implement your business logic

  'Sample to get a list of Alerts with error 41 and 50 in the past month - returns 2 lists of LoggedAlerts, the month and the number in each error type
  Public Shared Function GetAlertListsFromFunction(vMonth As Date, vRequester As clsRequester, ByRef rError41List As csLoggedAlertCol, ByRef rError50List As csLoggedAlertCol, ByRef rMonth As Date, ByRef rNum41 As Integer, ByRef rNum50 As Integer) As clsFault
    Dim pFunctionParameters As String = $"Month: {vMonth:dd-MMM-yy}"
    Dim pFault As New clsFault

    'erase or initialize referenced values
    rError41List = Nothing
    rError50List = Nothing
    rMonth = Nothing
    rNum41 = 0
    rNum50 = 0

    'choose an existing process, or create a new by adding it to the Process table
    'tbl_ prefix is used intrinsically, prc_ prefix is for your use. 
    'The entry point is the ClassName_FunctionName. This is to ensure the function is not "faking it"
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "TutorialController_GetAlertListsFromFunction", vRequester)
    If pFault.isOK = False Then Return pFault

    Dim pMonthStart As New Date(vMonth.Year, vMonth.Month, 1)
    Dim pMonthEnd As New Date(vMonth.AddMonths(1).Year, vMonth.AddMonths(1).Month, 1)
    pMonthEnd = pMonthEnd.AddDays(-1)

    'pErrorList = New csLoggedAlertCol(clsEnums.enmLoadParent.DoNotLoad) 'gets only the LoggedAlert
    'pErrorList = New csLoggedAlertCol(clsEnums.enmLoadParent.TextOnly) ' As above, but also gets the text for the default designation of the parents
    'pErrorList = New csLoggedAlertCol(clsEnums.enmLoadParent.EntireObject) ' As above, but includes the parent object for each of the objects
    Dim pErrorList As csLoggedAlertCol = New csLoggedAlertCol(clsEnums.enmLoadParent.DoNotLoad)
    pFault = pErrorList.FillByBoundedTimeOccurred(pMonthStart, pMonthEnd, vRequester) : If Not pFault.isOK() Then Return pFault

    'Now we can clone the data to get what we want, 
    rError41List = pErrorList.CloneByFaultNumber(41)
    rError50List = pErrorList.CloneByFaultNumber(50)

    rMonth = pMonthStart
    rNum41 = rError41List.Count
    rNum50 = rError50List.Count

    Return pFault
  End Function

  'This function shows how to run a Stored Procedure, from scratch. To see an example with returned data, see Class ccDatabaseMaintenance, Function GetActiveSQLUserRights
  Public Shared Function CreateReportPayment(vMinimumReimbursalAmount As Decimal, vPaymentMonth As Date, vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MinimumReimbursalAmount={0}, PaymentMonth={1}", vMinimumReimbursalAmount, vPaymentMonth.ToString("dd-MMM-yyyy HH:mm:ss"))
    Dim pFault As New clsFault

    'erase or initialize referenced values

    'Check permission
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_DoSample, "TutorialController_CreateReportPayment", vRequester)
    If pFault.isOK = False Then Return pFault

    Dim pCommandText As String = "BGRptCreatePayments"
    Dim pParameters As New ccDAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters  
      pLastReadVariableName = "PaymentMonth"
      pParameters.Add("PaymentMonth", ccDAL.enmSQLDataType.DateTime).Value = vPaymentMonth
      pLastReadVariableName = "MinimumReimbursalAmount"
      pParameters.Add("MinimumReimbursalAmount", ccDAL.enmSQLDataType.Decimal).Value = vMinimumReimbursalAmount
      pLastReadVariableName = "ChangedBy"
      pParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
      pLastReadVariableName = "UpdatingLoginID"
      pParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
      pLastReadVariableName = ""

      'Execute query   
      Dim pTargCCReader As ccDAL.csTargCCReader = Nothing
      pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader)

      'I expect to get -1 back   
      If pFault.isOK = True Then
        If pTargCCReader.HasRows = True Then
          pTargCCReader.Read()
          Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0))
          If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-160326-1641", vRequester)
        Else
          pFault.LogFreeTextFault(51, "No response returned for SQL query!", pFunctionParameters, "TRGT-160326-1642", vRequester)
        End If
      End If
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester)
    End Try
    If Not pFault.isOK Then Return pFault

    Return pFault
  End Function

  'This function shows how to run a Stored Procedure, from scratch, returning a collection as a response, using the pTargCCReader
  Public Shared Function GetDatabaseFileSizes(vRequester As clsRequester, ByRef rDBName As List(Of String), ByRef rFileName As List(Of String), ByRef rType As List(Of String), ByRef rCurrentSize As List(Of Decimal), ByRef rFreeSpace As List(Of Decimal)) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault

    'erase or initialize referenced values
    rDBName = New List(Of String)
    rFileName = New List(Of String)
    rType = New List(Of String)
    rCurrentSize = New List(Of Decimal)
    rFreeSpace = New List(Of Decimal)

    'Check permission
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_DoSample, "TutorialController_GetDatabaseFileSizes", vRequester)
    If pFault.isOK = False Then Return pFault

    Dim pCommandText As String = "c__DatabaseFileSizesFill"
    Dim pParameters As New ccDAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters  
      'pLastReadVariableName = "PaymentMonth" 'Sample - not needed for this query
      'pParameters.Add("PaymentMonth", ccDAL.enmSQLDataType.DateTime).Value = vPaymentMonth
      pLastReadVariableName = ""

      'Execute query   
      Dim pTargCCReader As ccDAL.csTargCCReader = Nothing
      pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader)

      'I expect to get -1 back   
      If pFault.isOK = True Then
        If pTargCCReader.HasRows = True Then
          While pTargCCReader.Read()
            Try
              rDBName.Add(pTargCCReader.GetString(0))
              rFileName.Add(pTargCCReader.GetString(1))
              rType.Add(pTargCCReader.GetString(2))
              rCurrentSize.Add(pTargCCReader.GetDecimal(3))
              rFreeSpace.Add(pTargCCReader.GetDecimal(4))
            Catch ex As Exception
              Return pFault.LogException(ex, pFunctionParameters, "TRGT-210227-1041", vRequester)
            End Try
          End While
        End If
      End If
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester)
    End Try
    If Not pFault.isOK Then Return pFault

    Return pFault
  End Function

  'This function shows how to run a Stored Procedure, using ccHelper
  Public Shared Function CreateReportPaymentUsingCcHelper(vMinimumReimbursalAmount As Decimal, vPaymentMonth As Date, vRequester As clsRequester, ByRef rResponse As String) As clsFault
    Dim pFunctionParameters As String = String.Format("MinimumReimbursalAmount={0}, PaymentMonth={1}", vMinimumReimbursalAmount, vPaymentMonth.ToString("dd-MMM-yyyy HH:mm:ss"))
    Dim pFault As New clsFault

    'erase or initialize referenced values
    rResponse = ""

    'Check permission
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_DoSample, "TutorialController_CreateReportPaymentUsingCcHelper", vRequester)
    If pFault.isOK = False Then Return pFault

    Dim pCommandText As String = "BGRptCreatePayments"
    Dim pParameters As New ccDAL.csTargCCParameterCol

    Dim pLastReadVariableName As String = ""
    Try
      'set parameters  
      pLastReadVariableName = "PaymentMonth"
      pParameters.Add("PaymentMonth", ccDAL.enmSQLDataType.DateTime).Value = vPaymentMonth
      pLastReadVariableName = "MinimumReimbursalAmount"
      pParameters.Add("MinimumReimbursalAmount", ccDAL.enmSQLDataType.Decimal).Value = vMinimumReimbursalAmount
      pLastReadVariableName = "ChangedBy"
      pParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
      pLastReadVariableName = "UpdatingLoginID"
      pParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
      pLastReadVariableName = ""

      'Execute query   
      pFault = ccHelper.RunStoredProcedure(pCommandText, pParameters, rResponse, vRequester)

    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters
      pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester)
    End Try
    If Not pFault.isOK Then Return pFault

    Return pFault
  End Function

  'Sample to get a list of Users that logged in yesterday. Returns yesterday's date and a combolist with UserID and UserNames
  Public Shared Function GetYesterdaysLoggedinUsers(vRequester As clsRequester, ByRef rYesterdaysDate As Date, ByRef rLoggedInUsers As clsComboList) As clsFault
    Dim pFunctionParameters As String = $""
    Dim pFault As New clsFault

    'erase or initialize referenced values
    rYesterdaysDate = Nothing
    rLoggedInUsers = Nothing

    'Check permission
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "TutorialController_GetYesterdaysLoggedinUsers", vRequester)
    If pFault.isOK = False Then Return pFault

    rYesterdaysDate = DateTime.Now.Date.AddDays(-1)
    rLoggedInUsers = New clsComboList()
    Dim pStart As DateTime = rYesterdaysDate
    Dim pEnd As DateTime = rYesterdaysDate.AddDays(1).AddSeconds(-1)

    'Note that system classes start with cs, while user classes start with cls
    Dim pLoggedLogins As New csLoggedLoginCol()
    pFault = pLoggedLogins.FillByBoundedTimeLoggedIn(pStart, pEnd, vRequester) : If Not pFault.isOK() Then Return pFault

    If pLoggedLogins.Count = 0 Then Return pFault 'No need to continue

    'Sort by UserID and fill the combolist (user could have logged in more than once)
    pLoggedLogins.SortByUserName()

    Dim pLastUserName As String = ""
    Dim pLoggedInUsers As New clsComboList() 'use this in the interim, in case we are thrown out in the middle
    For Each l In pLoggedLogins
      If Not pLastUserName.Equals(l.UserName, StringComparison.OrdinalIgnoreCase) Then 'StringComparison.OrdinalIgnoreCase is faster
        'Get the user
        Dim pUser As New csUser()
        pFault = pUser.GetByUserName(l.UserName, vRequester, False) : If Not pFault.isOK() Then Return pFault
        If pUser.IsEmpty Then 'no user  found - 
          Continue For
        End If
        pLoggedInUsers.Add(New clsComboListMember(pUser.ID, pUser.UserName))
        pLastUserName = l.UserName
      End If
    Next

    rLoggedInUsers = pLoggedInUsers

    Return pFault
  End Function

  'Sample to create a "Mail"  entry (returns nothing)
  Public Shared Function CreateDummyMail(vRequester As clsRequester, ByRef rMail As csMail) As clsFault
    Dim pFunctionParameters As String = $""
    Dim pFault As New clsFault

    'erase or initialize referenced values
    rMail = Nothing

    'Check permission

    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_MailUpdate, "TutorialController_CreateDummyMail", vRequester)
    If pFault.isOK = False Then Return pFault

    rMail = New csMail()

    Dim pMail As New csMail() With {.Body = "This is a test", .MessagingMode = clsEnums.enmMessagingMode.Email, .RecipientEmail = "test@plop.ca", .Subject = "Test", .WhenSeen = DateTimeOffset.Now}

    pFault = pMail.Update(vRequester) : If Not pFault.isOK() Then Return pFault ' since the ID is 0, it will add the row.

    rMail = pMail

    Return pFault
  End Function

  'Fill-in for WSController
  Public Shared Function csLoggedAlertCol_FillByBoundedFaultNumber(vFaultNumberFrom As Integer, vFaultNumberTo As Integer, vRequester As clsRequester, ByRef rLoggedAlerts As csLoggedAlertCol, ByRef rCount As Integer) As clsFault
    Dim pFunctionParameters As String = String.Format($"vFaultNumberFrom: {vFaultNumberFrom}, FaultNumberTo: {vFaultNumberTo}")
    Dim pFault As New clsFault

    'Used only to show intrinsic functions via WS Controller

    Return pFault.LogFreeTextFault(11, "This function was only written to work opposite WSController, to show how to call an intrinsic function", pFunctionParameters, "TRGT-200927-1500", vRequester)
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

    'choose an existing process, or create a new by adding it to the Process table
    'tbl_ prefix is used intrinsically, prc_ prefix is for your use. 
    'The entry point is the ClassName_FunctionName. This is to ensure the function is not "faking it"
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "TutorialController_GetTop10UsersAfterApproval", vRequester)
    If pFault.isOK = False Then Return pFault

    'Send the SMS Message
    Dim pFunctionName As String = $"Top10Users-2210041148#Get Top 10 Users"
    pFault = ccSecurity.RequireApproval(pFunctionName, ccSecurity.enmApprovalMethod.ApproveViaWebPage, vRequester) : If Not pFault.isOK Then Return pFault

    pFault = ccSecurity.CheckApproval(pFunctionName, vRequester) : If Not pFault.isOK Then Return pFault

    Dim pUsers As New csUserCol
    pFault = pUsers.Fill(vRequester, 10, clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then Return pFault

    rTop10Users = New clsComboList()
    For Each l In pUsers
      rTop10Users.Add(New clsComboListMember(l.ID, l.FullName))
    Next

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
