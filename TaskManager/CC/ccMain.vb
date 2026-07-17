'Created by TargCC Version 4.0.6.3
Public Class ccMain

  Private Shared _Requester As clsRequester
  Private Shared _DelayOnStartUp As Integer

  Private Shared _Log As EventLog

  Private Shared _Mutex As Threading.Mutex

  Friend Shared Event evtRunExternalJob(ByRef rJob As csJob, ByRef rFault As clsFault, ByRef rJobFound As Boolean, ByVal vRequester As clsRequester)

  'For Shutdown
  Public Declare Auto Function SetConsoleCtrlHandler Lib "kernel32.dll" (ByVal Handler As HandlerRoutine, ByVal Add As Boolean) As Boolean
  Public Delegate Function HandlerRoutine(ByVal CtrlType As CtrlTypes) As Boolean
  Public Enum CtrlTypes
    CTRL_C_EVENT = 0
    CTRL_BREAK_EVENT = 1
    CTRL_CLOSE_EVENT = 2
    CTRL_LOGOFF_EVENT = 5
    CTRL_SHUTDOWN_EVENT = 6
  End Enum

  Shared Sub Main()

    'AlwaysOn means that the TaskManager is run once and stays up
    'Otherwise, it is activated at regular intervals by the Task Scheduler

    If My.Settings.AlwaysOn = True Then
      'wait 5 seconds so that I can kill myself 
      Threading.Thread.Sleep(5000)

      _Mutex = New Threading.Mutex(False, My.Application.Info.AssemblyName)

      Tools.LogToTextFile.WriteMessage("TaskManager was started. " & My.Application.Info.AssemblyName, "Main")

      If _Mutex.WaitOne(0, False) = False Then
        _Mutex.Close()
        _Mutex = Nothing
        Tools.LogToTextFile.WriteMessage("I found that TaskManager was open. Killing myself", "Main")
        Environment.Exit(0)
      Else
        Tools.LogToTextFile.WriteMessage("I found that TaskManager was closed. Continuing", "Main")
      End If

      ' Attach the event handler method
      AddHandler AppDomain.CurrentDomain.ProcessExit, AddressOf MyApp_ProcessExit
      SetConsoleCtrlHandler(New HandlerRoutine(AddressOf ControlHandler), True)

      If My.Settings.WriteToEventLog Then
        Try

          Try
            Dim p As Boolean = EventLog.SourceExists(My.Application.Info.ProductName)
          Catch ex As Exception
            Throw New Exception("Could not check Log Source. When running for the 1st time, run as administrator", ex)
          End Try

          If Not EventLog.SourceExists(My.Application.Info.ProductName) Then
            ' Create the source, if it does not already exist.
            ' An event log source should not be created and immediately used. There is a latency time to enable the source, it should be created
            ' prior to executing the application that uses the source.
            ' Execute this sample a second time to use the new source.
            EventLog.CreateEventSource(My.Application.Info.ProductName, "Application")
            'The source is created.  Exit the application to allow it to be registered.
            SendMessage("Created Log Source", "Created log source and exited. Now you can run me for real.", "TRGT-140916-2342", True, EventLogEntryType.Information)
            Return
          End If


          ' Create an EventLog instance and assign its source.
          _Log = New EventLog()
          _Log.Source = My.Application.Info.ProductName

          ' Write an informational entry to the event log.    
          _Log.WriteEntry(My.Application.Info.ProductName & " Ver " & My.Application.Info.Version.ToString & " started up", EventLogEntryType.Information)

        Catch ex As Exception
          SendException(System.Reflection.MethodInfo.GetCurrentMethod().Name & ": Fatal Exception", "Trapped unhandled exception.", "TRGT-170816-1614", ex, True)
          Return
        End Try
      End If 'My.Settings.AlwaysOn = True Then

      KillIfAlive() ' check if another copy of me is open
    End If 'If My.Settings.AlwaysOn = True Then

    Try
      AnnounceAppAlive()

      StartUp()

      Dim pFunctionParameters As String = ""
      Dim pFault As New clsFault

      If _DelayOnStartUp > 0 AndAlso My.Settings.AlwaysOn = True Then
        Threading.Thread.Sleep(_DelayOnStartUp)
      End If

      'put out a thread to check AppAlive
      'CheckAppAlive()
      Dim t As New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf CheckAppAlive))
      t.Start()

      Do
        Try
          Threading.Thread.Sleep(30000)
          _Requester.CallingFunctionWithinApplication = "Main"
          pFault = DoTasks(My.Application.Info.ProductName, My.Settings.IsOneOfACouple, My.Settings.AlwaysOn, My.Settings.WriteToEventLog, _Requester)
          _Requester.CallingFunctionWithinApplication = "Main" 'set it back
          'We should never get here if we're AlwaysOn :-), since we're in an endless loop
          If pFault.isOK = False Then
            If My.Settings.AlwaysOn = True Then
              SendMessage("Fatal Fault: Retrying .....", pFault.StringForMessageBox, "TRGT-131009-0931", True, EventLogEntryType.Warning)
            Else
              SendMessage("Fatal Fault: ", pFault.StringForMessageBox, "TRGT-170816-1656", True, EventLogEntryType.Warning)
            End If
          End If
          If My.Settings.AlwaysOn = True Then
            Threading.Thread.Sleep(120000) 'Wait 2 minutes
          Else
            Exit Do
          End If
        Catch ex As Exception
          Try
            pFault = New clsFault
            pFault.LogException(ex, ex.ToString, "TRGT-140919-1249", _Requester)
          Catch exx As Exception
          End Try
          Try
            SendException(System.Reflection.MethodInfo.GetCurrentMethod().Name & ": Fatal Exception", "Trapped unhandled exception. Waiting 5 minutes and trying again!", "TRGT-140909-1250", ex, True)
          Catch exx As Exception
          End Try
          Threading.Thread.Sleep(300000) 'Wait 5 minutes
        End Try
      Loop
    Catch ex As Exception
      Try
        Dim pFault As New clsFault
        If _Requester IsNot Nothing Then
          pFault.LogException(ex, ex.ToString, "TRGT-140516-1239", _Requester)
        End If
      Catch exx As Exception
      End Try
      Try
        SendException(System.Reflection.MethodInfo.GetCurrentMethod().Name & ": Fatal Exception", "Trapped unhandled exception. Application exiting!", "TRGT-111201-175801", ex, True)
      Catch exx As Exception
        Throw exx
      End Try
    End Try
  End Sub


  Private Shared Function DoTasks(ByVal vTaskManagerProductName As String, ByVal vIsPartOfACouple As Boolean, ByVal vAlwaysOn As Boolean, ByVal vWriteToEventLog As Boolean, ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault

    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_DoTasks, "ccTaskManager_DoTasks", vRequester)
    If pFault.isOK = False Then Return pFault

    Dim pJobRunner As String = vRequester.UserName

    'Check that it's valid
    Dim pJobRunnerText As New csLookup()
    pFault = pJobRunnerText.GetByParentLookupTypeAndParentCodeAndLookupTypeAndCode(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.JobRunner, pJobRunner, vRequester, True) : If Not pFault.isOK Then Return pFault

    Static sLastHour As Integer = -1
    Static sStartTime As Date = DateTime.Now
    'Static sLastDay As Integer = -1

    Dim pLog As New EventLog()
    pLog.Source = vTaskManagerProductName

    'Create thread that checks if app is active
    Dim pActiveChecker As ccTaskManagerStatusChecker = Nothing
    Dim pAlive As New csJob
    pFault = pAlive.GetByJobCodeAndJobRunnerCode("ImAlive", pJobRunner, vRequester, True) : If Not pFault.isOK Then Return pFault

    If vIsPartOfACouple = True Then 'Only check if we're part of a couple
      pAlive.Active = True
      pActiveChecker = New ccTaskManagerStatusChecker(pJobRunner, vRequester)
      'pActiveChecker.Run() 'check
      Dim t As New Threading.Thread(New Threading.ThreadStart(AddressOf pActiveChecker.Run))
      t.Start()
    Else
      'set the times to 0
      pAlive.NextRunTime = Nothing
      pAlive.LastRunTime = Nothing
      pAlive.Active = False
    End If
    pFault = pAlive.UpdateFriend(vRequester) : If Not pFault.isOK Then Return pFault

    Static sLastMinute As Integer = -1

    Do
      'Check if we are alive
      If vIsPartOfACouple = True AndAlso pActiveChecker.IsActive = False Then
        Threading.Thread.Sleep(5000)
        AnnounceAppAlive()
        Continue Do
      End If

      If sLastHour <> DateTime.Now.Hour Then
        If vAlwaysOn = True Then
          Dim pLogEntry As String = ccHelper.GetEntryAssemblyDetails.ProductName & " is alive. I was started " & sStartTime.ToString("dd/MMM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US"))
          If vWriteToEventLog Then
            pLog.WriteEntry(pLogEntry, EventLogEntryType.Information)
          Else
            Tools.LogToTextFile.WriteMessage(pLogEntry, pLog.Source)
          End If
        End If
        sLastHour = DateTime.Now.Hour
      End If

      'get next job for job-runner
      Dim pJob As csJob = Nothing
      pFault = ccTaskManager.GetNextManagedJobForRunner(pJobRunner, pJob, vRequester) : If Not pFault.isOK Then Return pFault

      If pJob.ID > 0 Then
        Dim pStartTime As Date = DateTime.Now
        'run the function or the exe
        Dim pJobFound As Boolean = False

        vRequester.CallingFunctionWithinApplication = pJob.JobCode
        Select Case pJob.JobCode
          Case "ScanJobs"
            pFault = ccTaskManager.ScanJobs(pJob.ID, vRequester)
            pJobFound = True
          Case "BackupDB"
            pFault = ccDatabaseMaintenance.BackupDatabase(pJob.ID, vRequester)
            pJobFound = True
          Case "DeleteOldLogs"
            pFault = ccDatabaseMaintenance.DeleteOldLogs(pJob.ID, vRequester)
            pJobFound = True
          Case "MoveAudits"
            pFault = ccDatabaseMaintenance.MoveAudits(pJob.ID, vRequester)
            pJobFound = True
          Case "ReorganizeIndexes"
            pFault = ccDatabaseMaintenance.ReorganizeIndexes(pJob.ID, vRequester)
            pJobFound = True
          Case "UnlockUsers"
            pFault = ccTaskManager.UnlockUsers(pJob.ID, vRequester)
            pJobFound = True
          Case Else
            If pJob.IsManaged = True Then 'Just to reaffirm......
              If pJob.JobCode.StartsWith("mnTM_", StringComparison.OrdinalIgnoreCase) Then
                pFault = ccTaskManager.RunSP(pJob.ID, pJob.JobCode, vRequester)
                pJobFound = True
              Else
                RaiseEvent evtRunExternalJob(pJob, pFault, pJobFound, vRequester)

                'Put this sample in a prt file, using the event evtRunExternalJob
                'Partial Public Class ccMain
                '  Private Shared Sub ccMain_evtRunExternalJob(ByRef rJob As csJob, ByRef rFault As clsFault, ByRef rJobFound As Boolean, vRequester As clsRequester) Handles Me.evtRunExternalJob
                '    If rJobFound = False Then ccTaskManager.RunExternalJob(rJob, rFault, rJobFound, vRequester)
                '    If rJobFound = False Then ccTaskManagerBL.RunExternalJob(rJob, rFault, rJobFound, vRequester)
                '  End Sub
                'End Class

                'Create RunExternalJob, which would be in a ccTaskManager.prt file in DBController
                'Partial Public Class ccTaskManager
                '  Friend Shared Sub RunExternalJob(ByRef rJob As csJob, ByRef rFault As clsFault, ByRef rJobFound As Boolean, ByVal vRequester As clsRequester)
                '    Select Case rJob.JobCode
                '      Case "MoveEventsToHistory"
                '        rFault = mnMaintenance.MoveEventsToHistory(rJob.ID, vRequester)
                '        rJobFound = True
                '      Case Else
                '        ''see http://www.thescarms.com/dotnet/Process.aspx
                '        '' Start a new process 
                '        'Try
                '        '  Dim myProcess As Process = System.Diagnostics.Process.Start(pJob.JobCode)
                '        '  myProcess.WaitForExit()
                '        '  myProcess.Close()
                '        '  pFault.SetOK()
                '        'Catch ex As Exception
                '        '  pFault.LogException(ex, "JobRunner=" & pJobRunner, "TRGT-140804-1629", vRequester)
                '        'End Try
                '    End Select
                '  End Sub
                'End Class 

                'You can also put external jobs in the ccTaskManagerBL, which is in c#
                'public class ccTaskManagerBL
                '  {
                '      public static void RunExternalJob(ref csJob job, ref clsFault fault, ref bool jobFound, clsRequester requester)
                '      {
                '          switch (job.JobCode)
                '          {
                '              case "UpdateExchangeRates":
                '                  fault = DeletePartiallyRegisteredCustomers(job.ID, requester);
                '                  jobFound = true;
                '                  break;
                '              case "ImportCardOrdersFromLeumiCard":
                '                  fault = BLIssuerImport.ImportCards(job.ID, requester);
                '                  jobFound = true;
                '                  break;
                '              default:
                '                  // Handle other cases if needed
                '                  break;
                '          }
                '      }
              End If
            End If
        End Select
        If pJobFound = False Then
          pFault.LogFreeTextFault(128, "Job not found in code: '" & pJob.JobCode & "'", "", "TRGT-191119-1935", vRequester)
        End If
        If Not pFault.isOK Then
          Dim pFaultReceived As clsFault = pFault.Clone()
          pFault.SetOK(vRequester) 'Revives the requester
          'Close the Job
          pFault = ccTaskManager.MarkJobAsComplete(pJob.ID, clsEnums.enmJobStatus.Failure, pStartTime, DateTime.Now, "Unexpected failure", 0, 0, pFaultReceived, vRequester) : If Not pFault.isOK Then Return pFault
        End If
      Else
        If vAlwaysOn = False Then
          Return pFault
        End If
      End If

      'sleep 1 sec
      If vAlwaysOn = True Then
        Threading.Thread.Sleep(1000)
      End If
      AnnounceAppAlive()
    Loop

    Return pFault
  End Function

  Private Shared Function StartUp() As String
    Dim pFunction As String = "StartUp"
    Dim pSubject As String = ""
    Dim pMessage As String = ""

    Dim pFault As New clsFault

    Dim pJobRunner As String = My.Settings.JobRunner

    Dim pResponse As String = ""
    Dim pPwd As String = MyController.GetConfigValueFromApplicationSetting(My.Settings.Pwd, pResponse)
    If pPwd = "" Then
      Do
        SendMessage(pFunction & ": Login Failed", "Invalid password: " & pResponse, "TRGT-170121-1113", True, EventLogEntryType.Error)
        Threading.Thread.Sleep(300000) ' try again in 5 minutes, in order to keep getting messages and mails
      Loop
    End If

    If pJobRunner Is Nothing OrElse pPwd Is Nothing Then
      Do
        SendMessage(pFunction & ": Login Failed", "JobRunner or Password not supplied", "TRGT-111201-181201", True, EventLogEntryType.Error)
        Threading.Thread.Sleep(300000) ' try again in 5 minutes, in order to keep getting messages and mails
      Loop
    End If

    pFault = ccSecurity.LogInByNamePwd(pJobRunner, pPwd, _Requester)
    If pFault.isOK = False Then
      Do
        SendMessage(pFunction & ": Login Failed", pFault.StringForMessageBox, "TRGT-111201-171601", False, EventLogEntryType.Error)
        Threading.Thread.Sleep(300000) ' try again in 5 minutes, in order to keep getting messages and mails
        pFault = ccSecurity.LogInByNamePwd(pJobRunner, pPwd, _Requester)
      Loop Until pFault.isOK() = True
    End If

    'Now get the ProblemMailTo form the DB
    Dim pMailTo As String = ""
    Try
      pMailTo = MyController.ProblemMailTo
    Catch ex As Exception
      SendException(pFunction & ": Config problem", "'Link2013.ProblemMailTo' not defined", "TRGT-131009-09640", ex, False)
      Environment.Exit(0)
    End Try

    _DelayOnStartUp = My.Settings.DelayOnStartUpSec
    _DelayOnStartUp = _DelayOnStartUp * 1000

    If My.Settings.AlwaysOn = True Then
      pMessage = My.Application.Info.ProductName & " version " & My.Application.Info.Version.ToString & " "
      If _DelayOnStartUp = 0 Then
        pMessage &= "Started: No Delay assigned in app config. I recommend you assign value in key 'DelayOnStartUpSec'!!."
      Else
        pMessage &= "Started: Delay in sec: " & My.Settings.DelayOnStartUpSec & "."
      End If
      SendMessage(pFunction & ": Started", pMessage, "", True, EventLogEntryType.Information)
    End If

    Return "OK"
  End Function

  Private Shared Sub KillIfAlive()

    Dim pCurrentProcess As Process = Process.GetCurrentProcess

    Dim pProcesses As System.Diagnostics.Process() = System.Diagnostics.Process.GetProcessesByName(pCurrentProcess.ProcessName)

    'If pProcesses.Length > 1 Then
    For iCntr As Integer = 0 To pProcesses.Length - 1
      Dim pProcess As System.Diagnostics.Process = pProcesses(iCntr)
      If pProcess.Id <> pCurrentProcess.Id Then
        SendMessage("KillIfAlive: Copy Existed", "I killed the previous copy", "", True, EventLogEntryType.Information)
        pProcess.Kill()
        Threading.Thread.Sleep(2000)
      End If
    Next
    'End If

  End Sub

  Friend Shared Function SendMessage(ByVal vSubject As String, ByVal vMessage As String, ByVal vUniqueCode As String, ByVal vSendMail As Boolean, ByVal vMessageType As EventLogEntryType) As String
    Dim pResponse As String = ""

    vMessage = vMessage.Replace("~", Environment.NewLine)

    Tools.LogToTextFile.WriteMessage(vSubject & ": " & vMessage & Environment.NewLine & vUniqueCode, "Main")

    If My.Settings.WriteToEventLog Then
      Try
        _Log.WriteEntry(My.Application.Info.ProductName & Environment.NewLine & vMessage & Environment.NewLine & vUniqueCode, vMessageType)
      Catch ex As Exception
        Console.WriteLine("Failed writing to event log: " & ex.Message)
        Console.WriteLine("Could not check Log Source. When running for the 1st time, run as administrator")
        Environment.Exit(0)
      End Try
    End If

    If vSendMail = True Then
      Dim pMailTo As String = ""
      Try
        pMailTo = MyController.ProblemMailTo
      Catch ex As Exception
        Tools.LogToTextFile.WriteMessage("SendMail: " & Environment.NewLine & ex.Message, "Main")
      End Try
      pResponse = Tools.Mailer.SendMailToMultipleRecipients("", My.Application.Info.ProductName & ":" & vSubject, pMailTo, vSubject & Environment.NewLine & Environment.NewLine & vMessage & Environment.NewLine & vUniqueCode & Environment.NewLine & Environment.NewLine & "ServerTime: " & DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss", New System.Globalization.CultureInfo("en-US")))
      If pResponse <> "OK" Then
        Tools.LogToTextFile.WriteMessage("SendMail: " & Environment.NewLine & pResponse, "Main")
      End If
    End If

    Return pResponse
  End Function
  Friend Shared Function SendException(ByVal vSubject As String, ByVal vMessage As String, ByVal vUniqueCode As String, ByVal vException As Exception, ByVal vSendMail As Boolean) As String
    Dim pResponse As String = ""

    Tools.LogToTextFile.WriteException(vSubject & ": " & vMessage & Environment.NewLine & vUniqueCode & Environment.NewLine & vException.ToString & Environment.NewLine, vException, "Main")

    If My.Settings.WriteToEventLog Then
      Try
        _Log.WriteEntry(My.Application.Info.ProductName & Environment.NewLine & vMessage & Environment.NewLine & Environment.NewLine & vException.ToString, EventLogEntryType.Error)
      Catch ex As Exception
        Console.WriteLine("Failed writing to event log: " & ex.Message)
        Console.WriteLine("Could not check Log Source. When running for the 1st time, run as administrator")
        Environment.Exit(0)
      End Try
    End If

    If vSendMail = True Then
      Dim pMailTo As String = ""
      Try
        pMailTo = MyController.ProblemMailTo
      Catch ex As Exception
        Tools.LogToTextFile.WriteMessage("SendMail: " & Environment.NewLine & ex.Message, "Main")
      End Try
      pResponse = Tools.Mailer.SendExceptionByMailToMultipleRecipients(My.Application.Info.ProductName & ":" & vSubject, pMailTo, vSubject & Environment.NewLine & vMessage & Environment.NewLine & vUniqueCode & Environment.NewLine & Environment.NewLine & "ServerTime: " & DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss", New System.Globalization.CultureInfo("en-US")), vException)
      If pResponse <> "OK" Then
        Tools.LogToTextFile.WriteMessage("SendException: " & Environment.NewLine & pResponse & Environment.NewLine & vException.ToString, "Main")
      End If
    End If

    Return pResponse
  End Function

  Private Shared Sub MyApp_ProcessExit(sender As Object, e As EventArgs)
    SendMessage("Application Exiting ", "Application Exiting ... Due to Process Exit", "", True, EventLogEntryType.Warning)

    If _Requester IsNot Nothing AndAlso _Requester.LoggedLoginID > 0 Then
      Dim pFault As clsFault = ccSecurity.LogOut(_Requester)
      If pFault.isOK = False Then
        SendMessage("Logout Failed", pFault.StringForMessageBox, "", True, EventLogEntryType.Warning)
      End If
    End If

  End Sub

  Public Shared Function ControlHandler(ByVal ctrlType As CtrlTypes) As Boolean

    Dim pCancel As Boolean = False

    SendMessage("Application closed down", "Exiting ... Due to " & ctrlType.ToString, "", True, EventLogEntryType.Warning)

    If _Requester IsNot Nothing AndAlso _Requester.LoggedLoginID > 0 Then
      Dim pFault As clsFault = ccSecurity.LogOut(_Requester)
      If pFault.isOK = False Then
        SendMessage("Logout Failed", pFault.StringForMessageBox, "", True, EventLogEntryType.Warning)
      End If
    End If

    If My.Settings.AlwaysOn = True Then
      If _Mutex.WaitOne(1000) = True Then
        _Mutex.ReleaseMutex()
      End If
      _Mutex.Close()
      If _Mutex IsNot Nothing Then _Mutex.Dispose()
      _Mutex = Nothing
    End If

    Return pCancel
  End Function


  Private Shared Sub CheckAppAlive()
    'This checks AppAlive. If it's more than 3 hours old, it shuts down the task manager
    Static pLogLocation As String

    If pLogLocation = "" Then
      pLogLocation = MyController.LogLocation
    End If
    Dim pAppAliveFileName As String = pLogLocation & "Alive.txt"

    Do
      System.Threading.Thread.Sleep(10000)
      Dim pFunction As String = ""
      Try
        pFunction = "Dim pFileInfo As IO.FileInfo = New IO.FileInfo(pAppAliveFileName)"
        Dim pFileInfo As IO.FileInfo = New IO.FileInfo(pAppAliveFileName)

        pFunction = "If Now.ToUniversalTime.Subtract(pFileInfo.LastWriteTimeUtc).TotalMinutes > 360 Then"
        If DateTime.Now.ToUniversalTime.Subtract(pFileInfo.LastWriteTimeUtc).TotalMinutes > 360 Then
          Dim pCurrentProcess As Process = Process.GetCurrentProcess
          Dim pMessage As String = $"Killing myself, since I found that AppAlive.txt was last written to at {pFileInfo.LastWriteTime:dd-MMM-yy HH:mm:ss}.{Environment.NewLine}
                                    I think I'm 'hung' (I give 6 hours grace).{Environment.NewLine}I've been 'up' since {pCurrentProcess.StartTime:dd-MM-yy HH:mm:ss}.{Environment.NewLine}
                                    If you are getting many of these mails, ensure that AlwaysOn is set to True in app.config, or that there is enough time between runs if it set to False."
          Tools.LogToTextFile.WriteMessage(pMessage.Replace(Environment.NewLine, " "), "Hung")
          SendMessage("TaskMangager Hung !!!!!!!!", pMessage, "", True, EventLogEntryType.Error)
          pCurrentProcess.Kill()
        End If
      Catch ex As Exception
        Tools.LogToTextFile.WriteException($"FailedCheckAppAlive at Function {pFunction}", ex, "AppAliveException")
      End Try
    Loop

  End Sub

  Friend Class ccTaskManagerStatusChecker

    Private _JobRunner As String
    Private _Requester As clsRequester
    Dim _LastRunTimes(2) As Date

    Private _IsActive As Boolean


    Friend ReadOnly Property IsActive As Boolean
      Get
        Return _IsActive
      End Get
    End Property

    Friend Sub New(ByVal vJobRunner As String, ByVal vRequester As clsRequester)
      _JobRunner = vJobRunner
      _Requester = vRequester
      If vJobRunner = "" Then Throw New Exception("No JobRunner received")
      If vRequester Is Nothing Then Throw New Exception("No Requester received")
      _LastRunTimes(0) = Nothing
      _LastRunTimes(1) = Nothing
      _LastRunTimes(2) = Nothing
    End Sub

    Friend Sub Run()
      Do
        StateAlive()
        Threading.Thread.Sleep(5000)
      Loop
    End Sub

    Private Sub StateAlive()
      Dim pFault As clsFault

      'Check to see if I'm Alive
      Dim pAlive As New csJob
      pFault = pAlive.GetByJobCodeAndJobRunnerCode("ImAlive", _JobRunner, _Requester, False) : If Not pFault.isOK Then Exit Sub 'no use continuing - I didn't get an ID
      'if there is none, then create it
      If pAlive.ID = 0 Then
        _IsActive = True
      Else
        If pAlive.LastRunBy = Environment.MachineName Then
          _IsActive = True
        Else
          'check if other computer died.
          'In worst case, there will be a 15 second wait
          _LastRunTimes(0) = _LastRunTimes(1)
          _LastRunTimes(1) = _LastRunTimes(2)
          _LastRunTimes(2) = pAlive.LastRunTime
          If _LastRunTimes(0) = _LastRunTimes(1) AndAlso _LastRunTimes(1) = _LastRunTimes(2) Then
            _IsActive = True
          Else
            _IsActive = False
          End If
        End If
      End If

      Dim pMessage As String = ""
      pMessage &= "ccTaskManagerStatusChecker _IsActive=" & _IsActive.ToString() & Environment.NewLine
      If _IsActive = False Then Tools.LogToTextFile.WriteMessage(pMessage, "Thread")

      If _IsActive = True Then
        _LastRunTimes(0) = Nothing
        _LastRunTimes(1) = Nothing
        _LastRunTimes(2) = Nothing

        pAlive.JobCode = "ImAlive"
        pAlive.JobRunnerCode = _JobRunner
        pAlive.Description = "States which version of Task Manager for " & _JobRunner & " is active"
        pAlive.Instructions = "n/a"
        pAlive.JobType = clsEnums.enmJobType.CyclicSec
        pAlive.CyclicCount = 1
        pAlive.SendNotificationOnSuccess = False
        pAlive.SendAlarmOnMissed = False
        pAlive.TimeOutSec = 5
        pAlive.Active = True
        pAlive.LastRunTime = DateTime.Now
        pAlive.NextRunTime = DateTime.Now.AddSeconds(5)
        pAlive.JobStatus = clsEnums.enmJobStatus.Success
        pAlive.IsManaged = False
        pAlive.LastRunBy = Environment.MachineName
        pFault = pAlive.UpdateFriend(_Requester, False)
      End If
    End Sub

  End Class

  Public Shared Sub AnnounceAppAlive()
    Static pLogLocation As String

    If pLogLocation = "" Then
      pLogLocation = MyController.LogLocation
    End If
    Dim pAppAliveFileName As String = pLogLocation & "Alive.txt"

    Try
      IO.File.WriteAllText(pAppAliveFileName, DateTime.Now.ToString("dd#MM#yyyy#HH#mm#ss"))
    Catch ex As Exception
      Tools.LogToTextFile.WriteException("FailedWritingAppAliveText: Trying Again", ex, "AppAliveException")
      Threading.Thread.Sleep(2000)
      Try
        IO.File.WriteAllText(pAppAliveFileName, DateTime.Now.ToString("dd#MM#yyyy#HH#mm#ss"))
        Tools.LogToTextFile.WriteMessage("Succeeded in 2nd attempt", "AppAliveException")
      Catch exx As Exception
        Tools.LogToTextFile.WriteException("FailedWritingAppAliveText: Gave Up!", exx, "AppAliveException")
      End Try
    End Try
  End Sub

End Class
