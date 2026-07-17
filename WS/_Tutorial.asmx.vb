Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace TutorialWS

  ' This tutorial shows how to use DBController in an XML Web Service


  ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
  ' <System.Web.Script.Services.ScriptService()> _
  <System.Web.Services.WebService(Namespace:="http://targcc.ca/")>
  <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
  <ToolboxItem(False)>
  Public Class Tutorial
    Inherits System.Web.Services.WebService

    Friend Shared _TextFileName As String = "TutorialWS"

    <WebMethod()>
    Public Function CreateTicket(ByVal userName As String, ByVal password As String, ByRef ticket As String) As Fault
      Dim pFault As clsFault

      Try
        If String.IsNullOrEmpty(userName) Then Throw New Exception("No UserName received")
        If String.IsNullOrEmpty(password) Then Throw New Exception("No Password received")
      Catch ex As Exception
        Helper.WriteException(ex, Nothing, "TRGT-200501-0908")
        Return Nothing
      End Try

      Dim pMessage As String = $"CreateTicket for {userName} at: {Now:dd/MM/yy HH:mm:ss.fff:}: Requester IP: {My.Request.UserHostAddress}" : Tools.LogToTextFile.WriteMessage(pMessage, _TextFileName)

      Dim pRequester As clsRequester = Nothing
      'You can force the language (by using vOverrideUILang:=), or use the language for the user
      pFault = ccSecurity.LogInByNamePwd(userName, password, pRequester, vAccessingEntity:=Helper.AccessingEntity("_Tutorial"))
      If Not pFault.isOK Then Return Helper.WriteFault(pFault, pRequester)

      pRequester.CallingFunctionWithinApplication = "Login"

      ticket = pRequester.CreateTicket()

      Return Helper.WriteFault(pFault, pRequester)
    End Function

    <WebMethod()>
    Public Function CloseTicket(ByVal ticket As String) As Fault
      Dim pFault As clsFault

      If String.IsNullOrEmpty(ticket) Then Return Nothing
      Dim pRequester As clsRequester
      Try
        pRequester = New clsRequester(ticket)
      Catch ex As Exception
        Helper.WriteException(ex, Nothing, "TRGT-200501-0851")
        Return Nothing
      End Try
      pRequester.CallingFunctionWithinApplication = "CloseTicket"
      Helper.WriteEntry(pRequester)

      If Helper.IsExpired(pRequester) Then Return New Fault With {.ErrorTypeNumber = 0, .ErrorDescription = "Ticket Expired"}

      pFault = ccSecurity.LogOut(pRequester)
      If Not pFault.isOK Then Return Helper.WriteFault(pFault, pRequester)

      Return Helper.WriteFault(pFault, pRequester)
    End Function

    'Sample to get a collection based on input
    <WebMethod()>
    Public Function csLoggedAlertCol_FillByBoundedFaultNumber(ByVal faultNumberFrom As Integer, ByVal faultNumberTo As Integer, ByVal ticket As String, ByRef loggedAlerts As csLoggedAlertCol, ByRef count As Integer) As Fault

      'erase or initialize referenced values
      loggedAlerts = Nothing
      count = 0

      'Check Ticket
      Dim pRequester As clsRequester = Nothing
      Helper.CheckTicket(ticket, pRequester)
      If pRequester Is Nothing Then Return Nothing
      pRequester.CallingFunctionWithinApplication = "csLoggedAlertCol_FillByBoundedFaultNumber"
      Helper.WriteEntry(pRequester)
      If Helper.IsExpired(pRequester) Then Return New Fault With {.ErrorTypeNumber = 0, .ErrorDescription = "Ticket Expired"}

      Dim pLoggedAlerts As New csLoggedAlertCol()

      'Execute the function
      Dim pFault = pLoggedAlerts.FillByBoundedFaultNumber(faultNumberFrom, faultNumberTo, pRequester, 0, clsEnums.enmFillDirection.DESC) : If Not pFault.isOK() Then Return Helper.WriteFault(pFault, pRequester)

      'Load the response
      loggedAlerts = pLoggedAlerts
      count = pLoggedAlerts.Count

      Return Helper.WriteFault(pFault, pRequester)
    End Function

    'Sample to get a list of Alerts with error 41 and 50 in the past month - returns 2 lists of LoggedAlerts, the month and the number in each error type
    <WebMethod()>
    Public Function Tutorial_GetAlertListsFromFunction(ByVal monthIn As Date, ByVal ticket As String, ByRef error41List As csLoggedAlertCol, ByRef error50List As csLoggedAlertCol, ByRef monthOut As Date, ByRef num41 As Integer, ByRef num50 As Integer) As Fault

      'erase or initialize referenced values
      error41List = Nothing
      error50List = Nothing
      monthOut = Nothing
      num41 = 0
      num50 = 0

      'Check Ticket
      Dim pRequester As clsRequester = Nothing
      Helper.CheckTicket(ticket, pRequester)
      If pRequester Is Nothing Then Return Nothing
      pRequester.CallingFunctionWithinApplication = "Tutorial_GetAlertListsFromFunction"
      Helper.WriteEntry(pRequester)
      If Helper.IsExpired(pRequester) Then Return New Fault With {.ErrorTypeNumber = 0, .ErrorDescription = "Ticket Expired"}

      'Do function
      Dim pFault As clsFault = TutorialController.GetAlertListsFromFunction(monthIn, pRequester, error41List, error50List, monthOut, num41, num50) : If Not pFault.isOK() Then Return Helper.WriteFault(pFault, pRequester)

      'Return
      Return Helper.WriteFault(pFault, pRequester)
    End Function


    'This function shows how to run a Stored Procedure, from scratch
    <WebMethod()>
    Public Function Tutorial_CreateReportPayment(ByVal minimumReimbursalAmount As Decimal, ByVal paymentMonth As Date, ByVal ticket As String) As Fault

      'erase or initialize referenced values

      'Check Ticket
      Dim pRequester As clsRequester = Nothing
      Helper.CheckTicket(ticket, pRequester)
      If pRequester Is Nothing Then Return Nothing
      pRequester.CallingFunctionWithinApplication = "Tutorial_CreateReportPayment"
      Helper.WriteEntry(pRequester)
      If Helper.IsExpired(pRequester) Then Return New Fault With {.ErrorTypeNumber = 0, .ErrorDescription = "Ticket Expired"}

      'Do function
      Dim pFault As clsFault = TutorialController.CreateReportPayment(minimumReimbursalAmount, paymentMonth, pRequester) : If Not pFault.isOK() Then Return Helper.WriteFault(pFault, pRequester)

      'Load the response

      'Return
      Return Helper.WriteFault(pFault, pRequester)
    End Function

    'This function shows how to run a Stored Procedure, using ccHelper
    <WebMethod()>
    Public Function Tutorial_CreateReportPaymentUsingCcHelper(ByVal minimumReimbursalAmount As Decimal, ByVal paymentMonth As Date, ByVal ticket As String, ByRef response As String) As Fault

      'erase or initialize referenced values
      response = ""

      'Check Ticket
      Dim pRequester As clsRequester = Nothing
      Helper.CheckTicket(ticket, pRequester)
      If pRequester Is Nothing Then Return Nothing
      pRequester.CallingFunctionWithinApplication = "Tutorial_CreateReportPaymentUsingCcHelper"
      Helper.WriteEntry(pRequester)
      If Helper.IsExpired(pRequester) Then Return New Fault With {.ErrorTypeNumber = 0, .ErrorDescription = "Ticket Expired"}

      'Do function
      Dim pFault As clsFault = TutorialController.CreateReportPaymentUsingCcHelper(minimumReimbursalAmount, paymentMonth, pRequester, response) : If Not pFault.isOK() Then Return Helper.WriteFault(pFault, pRequester)

      'Load the response

      'Return
      Return Helper.WriteFault(pFault, pRequester)

    End Function

    'Sample to get a list of Users that logged in yesterday. Returns yesterday's date and a combolist with UserID and UserNames
    <WebMethod()>
    Public Function Tutorial_GetYesterdaysLoggedinUsers(ByVal ticket As String, ByRef yesterdaysDate As Date, ByRef loggedInUsers As clsLongAndText()) As Fault

      'erase or initialize referenced values
      yesterdaysDate = Nothing
      loggedInUsers = Nothing

      'Check Ticket
      Dim pRequester As clsRequester = Nothing
      Helper.CheckTicket(ticket, pRequester)
      If pRequester Is Nothing Then Return Nothing
      pRequester.CallingFunctionWithinApplication = "Tutorial_GetYesterdaysLoggedinUsers"
      Helper.WriteEntry(pRequester)
      If Helper.IsExpired(pRequester) Then Return New Fault With {.ErrorTypeNumber = 0, .ErrorDescription = "Ticket Expired"}

      Dim pLoggedInUsers As clsComboList = Nothing

      'Do function
      Dim pFault As clsFault = TutorialController.GetYesterdaysLoggedinUsers(pRequester, yesterdaysDate, pLoggedInUsers) : If Not pFault.isOK() Then Return Helper.WriteFault(pFault, pRequester)

      'Load the response
      ReDim loggedInUsers(pLoggedInUsers.Count - 1)
      For i = 0 To pLoggedInUsers.Count - 1
        loggedInUsers(i) = New clsLongAndText(pLoggedInUsers(i).KeyLong, pLoggedInUsers(i).Text)
      Next

      'Return
      Return Helper.WriteFault(pFault, pRequester)
    End Function

    'Sample to create a "Mail"  entry (returns nothing)
    <WebMethod()>
    Public Function Tutorial_CreateDummyMail(ByVal ticket As String, ByRef mail As csMail) As Fault

      'erase or initialize referenced values
      mail = Nothing

      'Check Ticket
      Dim pRequester As clsRequester = Nothing
      Helper.CheckTicket(ticket, pRequester)
      If pRequester Is Nothing Then Return Nothing
      pRequester.CallingFunctionWithinApplication = "Tutorial_CreateDummyMail"
      Helper.WriteEntry(pRequester)
      If Helper.IsExpired(pRequester) Then Return New Fault With {.ErrorTypeNumber = 0, .ErrorDescription = "Ticket Expired"}

      'Do function
      Dim pFault As clsFault = TutorialController.CreateDummyMail(pRequester, mail) : If Not pFault.isOK() Then Return Helper.WriteFault(pFault, pRequester)

      'Load the response

      'Return
      Return Helper.WriteFault(pFault, pRequester)

    End Function

  End Class

  Public Class Fault
    Public Property ErrorTypeNumber As Integer
    Public Property ErrorDescription As String
    Public Property ErrorActionToTake As String
    Public Property LoggedErrorNumber As Long
  End Class

  Public Class Helper

    Friend Shared Function WriteException(ByVal ex As Exception, ByVal vRequester As clsRequester, ByVal vLocationCode As String) As String
      If vRequester IsNot Nothing Then
        Tools.LogToTextFile.WriteMessage($"        LoginID: {vRequester.LoggedLoginID}. Tried {vRequester.CallingFunctionWithinApplication}. {ex.Message} at {vLocationCode}", Tutorial._TextFileName)
      Else
        Tools.LogToTextFile.WriteMessage($"        LoginID: None. Tried Unknown. {ex.Message} at {vLocationCode}", Tutorial._TextFileName)
      End If
      Return "Failed"
    End Function

    Friend Shared Function WriteFault(ByVal vFault As clsFault, ByVal vRequester As clsRequester) As Fault
      Tools.LogToTextFile.WriteMessage($"        LoginID: {vRequester?.LoggedLoginID}. Tried {vRequester?.CallingFunctionWithinApplication}. {vFault.ShortStringForMessageBox(False).Replace(Environment.NewLine, "; ")}", Tutorial._TextFileName)
      Dim pLoginResponse As New Fault With {
        .ErrorTypeNumber = vFault.Number,
        .ErrorDescription = vFault.Message,
        .ErrorActionToTake = vFault.Action,
        .LoggedErrorNumber = vFault.LoggedAlertID}
      Return pLoginResponse
    End Function

    Friend Shared Sub WriteEntry(ByVal vRequester As clsRequester)
      Dim pMessage As String = $"    LoginID: {vRequester.LoggedLoginID}. Entered {vRequester.CallingFunctionWithinApplication} at: {Now:dd/MM/yy HH:mm:ss.fff:}: Requester: IP: {My.Request.UserHostAddress}" : Tools.LogToTextFile.WriteMessage(pMessage, Tutorial._TextFileName)
    End Sub

    Friend Shared Function IsExpired(ByVal vRequester As clsRequester) As Boolean
      Dim pFault As clsFault = Nothing
      Dim pLoggedLogin As New csLoggedLogin(vRequester.LoggedLoginID, vRequester, pFault, vMustExist:=True)
      If Not pFault.isOK Then WriteFault(pFault, vRequester) : Return True

      Dim pMaxMinutes As Integer = 5

      If pLoggedLogin.TimeLoggedOut > Date.MinValue Then
        Dim pMessage As String = $"        LoginID: {vRequester.LoggedLoginID} From {vRequester.CallingFunctionWithinApplication} at: {Now:dd/MM/yy HH:mm:ss.fff:}: Requester IP: {My.Request.UserHostAddress}. LOGGED OUT at {pLoggedLogin.TimeLoggedOut:dd-MMM-yyyy HH:mm.ss}) !!" : Tools.LogToTextFile.WriteMessage(pMessage, Tutorial._TextFileName)
        'pFault = ccSecurity.LogOut(vRequester)
        Return True
      ElseIf DateTime.Now.Subtract(pLoggedLogin.TimeLoggedIn).TotalMinutes > pMaxMinutes Then
        Dim pMessage As String = $"        LoginID: {vRequester.LoggedLoginID} From {vRequester.CallingFunctionWithinApplication} at: {Now:dd/MM/yy HH:mm:ss.fff:}: Requester IP: {My.Request.UserHostAddress}. EXPIRED (Greater than {pMaxMinutes}) !!" : Tools.LogToTextFile.WriteMessage(pMessage, Tutorial._TextFileName)
        pFault = ccSecurity.LogOut(vRequester)
        Return True
      Else
        Return False
      End If
    End Function

    Friend Shared Sub CheckTicket(ByVal vTicket As String, ByRef rRequester As clsRequester)

      rRequester = Nothing

      'Check ticket
      If String.IsNullOrEmpty(vTicket) Then Return
      Dim pRequester As clsRequester
      Try
        pRequester = New clsRequester(vTicket)
      Catch ex As Exception
        Helper.WriteException(ex, Nothing, "TRGT-200501-1055")
        Return
      End Try

      rRequester = pRequester

    End Sub

    Friend Shared Function AccessingEntity(ByVal vPageName As String) As csAccessingEntity
      Dim pFault As New clsFault()

      Dim pAccessingEntity = New csAccessingEntity(vLoadPCDetails:=False, vLoadIPAndCountry:=False, vRequester:=Nothing, rFault:=pFault)
      If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox)

      With pAccessingEntity
        .ApplicationName = System.Web.HttpContext.Current.Request.Url.LocalPath.Substring(1).Replace("/", ":").Replace(".asmx", "")
        .ApplicationVersion = System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString()
        .DnsGetHostName = System.Web.HttpContext.Current.Request.UserHostName
        .EnvironmentUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name
        Dim pOIP As String = System.Web.HttpContext.Current.Request("HTTP_CF_CONNECTING_IP")
        If pOIP Is Nothing Then
          .WSReportedIP = System.Web.HttpContext.Current.Request("REMOTE_ADDR")
          .WSReportedCountry = "UD"
        Else
          .WSReportedIP = pOIP
          .WSReportedCountry = System.Web.HttpContext.Current.Request("HTTP_CF_IPCOUNTRY")
        End If
        .AccessingComputerDetails = System.Web.HttpContext.Current.Request.UserAgent
        Dim pUserLanguages As String = ""
        If System.Web.HttpContext.Current.Request.UserLanguages IsNot Nothing Then
          Try
            For Each p In System.Web.HttpContext.Current.Request.UserLanguages
              pUserLanguages &= p & ";"
            Next
          Catch ex As Exception
            pUserLanguages &= "Exception" & ex.ToString() & ";"
          End Try
        Else
          pUserLanguages = "Unknown"
        End If
        .UICulture = pUserLanguages
        If System.Web.HttpContext.Current.Request.UrlReferrer IsNot Nothing AndAlso System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri IsNot Nothing Then '* Edited by Roni   
          .EnvironmentUserDomainName = System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri
        End If
      End With

      Return pAccessingEntity
    End Function

  End Class

  Public Class clsLongAndText
    Public Property [Long] As Long
    Public Property Text As String

    Public Sub New()
      _Long = 0
      _Text = ""
    End Sub
    Public Sub New(ByVal vLong As Long, ByVal vText As String)
      _Long = vLong
      _Text = vText
    End Sub
  End Class

End Namespace