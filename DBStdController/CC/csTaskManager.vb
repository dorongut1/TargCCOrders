Public Class ccTaskManager

  'Friend Shared Event evtRunExternalJob(ByRef rJob As csJob, ByRef rFault As clsFault, ByRef rJobFound As Boolean, ByVal vRequester As clsRequester)

  ''' <summary>
  ''' This scans all jobs. If the job is in process and timed out, it sends a warning. If it's managed, it also marked as failed. If it missed its run, then a warning is sent.
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Friend Shared Function ScanJobs(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As clsFault

    Dim pStartTime As Date = DateTime.Now

    'get list  of active jobs
    Dim pJobCol As New csJobCol
    pFault = pJobCol.FillByActive(False, vRequester) : If Not pFault.isOK Then Return pFault
    'Set NextRunTime to Nothing
    For Each l In pJobCol
      l.NextRunTime = Nothing
      l.ActivatingUser = "System on " & Environment.MachineName
      pFault = l.UpdateFriend(vRequester, False) : If Not pFault.isOK Then Return pFault
    Next

    pJobCol = New csJobCol
    pFault = pJobCol.FillByActive(True, vRequester) : If Not pFault.isOK Then Return pFault

    '1st check to see if something is InProcess
    Dim pUpdateCntr As Integer = 0
    Dim pFailedCntr As Integer = 0
    Dim pFailedText As String = ""
    Dim pMailCntr As Integer = 0
    For Each l In pJobCol
      If l.ID = vJobID Then Continue For ' Ignore myself!
      If l.NextRunTime = Nothing Then
        l.NextRunTime = DateTime.Now.AddSeconds(-5)
        pFault = SetNextRunTime(l, vRequester)
        If Not pFault.isOK Then
          'if the SetNextRunFailed, then do not exit - Just write it to the log
          pFailedText &= pFault.LoggedAlertID.ToString() & ";"
          pFault.SetOK(vRequester)
          pFailedCntr += 1
          Continue For
        End If
        l.WarningMailSent = False
        pFault = l.UpdateFriend(vRequester, False) : If Not pFault.isOK Then Return pFault
        pUpdateCntr += 1
      End If

      'check to see if in process and timed out
      If l.JobStatus = clsEnums.enmJobStatus.InProcess Then
        If l.LastRunTime.AddSeconds(l.TimeOutSec) < DateTime.Now Then
          'It timed-out!
          'If l.IsManaged = True Then
          pFault = MarkJobAsComplete(l.ID, clsEnums.enmJobStatus.Failure, l.LastRunTime, DateTime.Now, "Job timed out TimeOut (sec) = " & l.TimeOutSec & ". Found by Scanner", 0, 0, Nothing, vRequester) : If Not pFault.isOK Then Return pFault
          pUpdateCntr += 1
          'Else
          'pFault = SendMailOnFailure(l, "Scanner found the job as timed-out", False, True, vRequester) : If Not pFault.isOK Then Return pFault
          'pMailCntr += 1
          'End If
          'Continue For
        End If
      Else 'If l.JobStatus <> clsEnums.enmJobStatus.UD Then
        'check to see if complete and did not start yet (check time out for runner)
        If l.WarningMailSent = False AndAlso l.NextRunTime < DateTime.Now Then
          Dim pMaxSecs As Integer = 0
          Dim pJobsForRunner As csJobCol = pJobCol.CloneByJobRunnerCodeAndActive(l.JobRunnerCode, True)
          'Get the maximum time he could be working on other projects"
          For Each ll In pJobsForRunner
            pMaxSecs += ll.TimeOutSec
            'If ll.TimeOutSec > pMaxSecs Then pMaxSecs = ll.TimeOutSec
          Next
          'If the job is managed, then it has to run at the next run time. If it is not, then it has to run within the time frame for the job
          If l.IsManaged = False Then
            If l.JobType = clsEnums.enmJobType.Annually Then
              l.NextRunTime = l.LastRunTime.AddYears(1)
            ElseIf l.JobType = clsEnums.enmJobType.Monthly Then
              l.NextRunTime = l.LastRunTime.AddMonths(1)
            ElseIf l.JobType = clsEnums.enmJobType.Weekly Then
              l.NextRunTime = l.LastRunTime.AddDays(7)
            ElseIf l.JobType = clsEnums.enmJobType.Daily Then
              l.NextRunTime = l.LastRunTime.AddDays(1)
            ElseIf l.JobType = clsEnums.enmJobType.CyclicDay Then
              l.NextRunTime = l.LastRunTime.AddDays(l.CyclicCount)
            ElseIf l.JobType = clsEnums.enmJobType.CyclicHour Then
              l.NextRunTime = l.LastRunTime.AddHours(l.CyclicCount)
            ElseIf l.JobType = clsEnums.enmJobType.CyclicMin Then
              l.NextRunTime = l.LastRunTime.AddMinutes(l.CyclicCount)
            ElseIf l.JobType = clsEnums.enmJobType.CyclicSec Then
              l.NextRunTime = l.LastRunTime.AddSeconds(l.CyclicCount)
            End If
          End If
          If l.NextRunTime.AddSeconds(pMaxSecs) < DateTime.Now Then
            'Send warning
            pFault = SendMailOnFailure(l, "Scanner found that the job did not start on time.", True, vRequester) : If Not pFault.isOK Then Return pFault
            pMailCntr += 1
          End If
        End If
      End If
    Next
    If Not pFault.isOK Then Return pFault

    If pFailedText <> "" Then
      pFailedText = "Warning: AlertID's " & pFailedText & " "
    End If

    Dim pResponse As String = String.Format(pFailedText & "I updated {0} tasks and send mails for {1} tasks", pUpdateCntr, pMailCntr)

    pFault = MarkJobAsComplete(vJobID, clsEnums.enmJobStatus.Success, pStartTime, DateTime.Now, pResponse, pUpdateCntr + pMailCntr, pFailedCntr, Nothing, vRequester)
    If Not pFault.isOK Then pFault.SetOK(vRequester) 'continue on

    Return pFault
  End Function

  ''' <summary>
  ''' This gets the next job for the runner. It is intended for managed jobs
  ''' </summary>
  ''' <param name="vRunnerCode"></param>
  ''' <param name="rJob"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Friend Shared Function GetNextManagedJobForRunner(ByVal vRunnerCode As String, ByRef rJob As csJob, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As clsFault

    Dim pJobCol As New csJobCol
    pFault = pJobCol.FillByJobRunnerCodeAndActive(vRunnerCode, True, vRequester) : If Not pFault.isOK Then Return pFault

    pJobCol.SortByNextRunTime()

    '1st check to see if something is InProcess
    rJob = New csJob
    For Each l In pJobCol
      If l.IsManaged = False Then Continue For 'only handle managed jobs
      If l.JobCode = "ScanJobs" AndAlso l.NextRunTime = Nothing Then
        'Run this 1st
        rJob = l
        Exit For
      End If
      If l.NextRunTime = Nothing Then Continue For
      If l.JobStatus = clsEnums.enmJobStatus.InProcess Then
        'mark job as failed (hung)
        pFault = MarkJobAsComplete(l.ID, clsEnums.enmJobStatus.Failure, l.LastRunTime, DateTime.Now, "The owner found the job as in-process", 0, 0, Nothing, vRequester) : If Not pFault.isOK Then Return pFault
        Continue For ' get it next time around
      End If
      If l.NextRunTime < DateTime.Now Then
        If rJob.NextRunTime = Nothing OrElse rJob.NextRunTime > l.NextRunTime Then
          rJob = l
          Continue For
        End If
      End If
    Next

    If rJob.ID > 0 Then ' We found one
      Dim pJobID As Long = rJob.ID
      Dim pLastRunBy As String = vRequester.UserName & " @ LoginID: " & vRequester.LoggedLoginID
      'we want to make sure another machine/thread didn't already run it
      pFault = csJob.UpdateSetToInProcess(pJobID, pLastRunBy, vRequester) : If Not pFault.isOK Then Return pFault
      If pJobID = 0 Then
        'someone else got it!
        rJob = New csJob
      Else
        rJob = New csJob
        'refill the properties
        pFault = rJob.GetByID(pJobID, vRequester, True) : If Not pFault.isOK Then Return pFault
      End If
    End If

    Return pFault
  End Function

  'Friend Shared Function RunExternalJob(ByRef rJob As csJob, ByRef rJobFound As Boolean, ByVal vRequester As clsRequester) As clsFault
  '  Dim pFault As clsFault = Nothing
  '  RaiseEvent evtRunExternalJob(rJob, pFault, rJobFound, vRequester)
  '  Return pFault
  'End Function

  Public Shared Function GetSpecificUnmanagedJobForRunner(ByVal vJobID As Long, ByRef rJob As csJob, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "JobID= " & vJobID
    Dim pFault As clsFault

    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_GetSpecificUnmanagedJobForRunner, "ccTaskManager_GetSpecificUnmanagedJobForRunner", vRequester)
    If pFault.isOK = False Then Return pFault

    rJob = New csJob
    pFault = rJob.GetByID(vJobID, vRequester, True) : If Not pFault.isOK Then Return pFault

    If rJob.IsManaged = True Then
      rJob = New csJob
      'GetSpecificUnmanagedJobForRunner is not used for Managed jobs
      Return pFault.LogFreeTextFault(126, "", pFunctionParameters, "TRGT-141105-2342", vRequester)
    ElseIf rJob.TimeOutSec = 0 Then
      rJob = New csJob
      Return pFault.LogFreeTextFault(124, "", pFunctionParameters, "TRGT-141211-1306", vRequester)
    End If

    pFault = GetSpecificUnmanagedJobForRunner(rJob, vRequester)

    Return pFault
  End Function

  Public Shared Function GetSpecificUnmanagedJobForRunner(ByVal vRunnerCode As String, ByVal vJobCode As String, ByRef rJob As csJob, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "RunnerCode= " & vRunnerCode & "; JobCode= " & vJobCode & ""
    Dim pFault As clsFault

    'Now get the permissions. This should be an enum. If it doesn't exist, create it 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_GetSpecificUnmanagedJobForRunner, "ccTaskManager_GetSpecificUnmanagedJobForRunner", vRequester)
    If pFault.isOK = False Then Return pFault

    rJob = New csJob
    pFault = rJob.GetByJobCodeAndJobRunnerCode(vJobCode, vRunnerCode, vRequester, True) : If Not pFault.isOK Then Return pFault

    If rJob.IsManaged = True Then
      rJob = New csJob
      Return pFault.LogFreeTextFault(126, "", pFunctionParameters, "TRGT-141105-2343", vRequester)
    ElseIf rJob.TimeOutSec = 0 Then
      rJob = New csJob
      Return pFault.LogFreeTextFault(124, "", pFunctionParameters, "TRGT-141211-1306", vRequester)
    End If

    pFault = GetSpecificUnmanagedJobForRunner(rJob, vRequester)

    Return pFault
  End Function

  Private Shared Function GetSpecificUnmanagedJobForRunner(ByRef rJob As csJob, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Job-'{0}', Runner-'{1}'", rJob.JobCode, rJob.JobRunnerCode)
    Dim pFault As New clsFault

    If rJob.ID > 0 AndAlso rJob.JobStatus <> clsEnums.enmJobStatus.InProcess Then
      If rJob.Active = True AndAlso
            rJob.NextRunTime <> Nothing AndAlso
            rJob.NextRunTime < DateTime.Now Then

        'Should not have called managed job
        If rJob.IsManaged = True Then
          Dim pJob As String = rJob.JobCode
          Dim pRunner As String = rJob.JobRunnerCode
          rJob = New csJob
          Return pFault.LogFreeTextFault(126, "", pFunctionParameters, "TRGT-141105-1007", vRequester)
        End If

        Dim pJobID As Long = rJob.ID
        Dim pLastRunBy As String = vRequester.UserName & " @ LoginID: " & vRequester.LoggedLoginID
        pFault = csJob.UpdateSetToInProcess(pJobID, pLastRunBy, vRequester) : If Not pFault.isOK Then Return pFault
        If pJobID = 0 Then
          'this means it was grabbed by someone else. I found it as in-process
          rJob = New csJob
        Else
          'get the job with the updated fields
          rJob = New csJob
          pFault = rJob.GetByID(pJobID, vRequester, True) : If Not pFault.isOK Then Return pFault
        End If
      Else
        rJob = New csJob
      End If
    Else
      rJob = New csJob
    End If

    Return pFault.SetOK()
  End Function

  Private Shared Function SetNextRunTime(ByVal vJob As csJob, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Job ID={0}, Job Code={1}, Runner={2}", vJob.ID, vJob.JobCode, vJob.JobRunnerCode)
    Dim pFault As New clsFault

    Dim pNextRunTime As Date = Nothing
    With vJob.WhenToRun
      Select Case vJob.JobType
        Case clsEnums.enmJobType.Annually
          pNextRunTime = New Date(DateTime.Now.Year, .Month, .Day, .Hour, .Minute, 0)
          If pNextRunTime < DateTime.Now Then pNextRunTime = pNextRunTime.AddYears(1)
        Case clsEnums.enmJobType.Monthly
          pNextRunTime = New Date(DateTime.Now.Year, DateTime.Now.Month, .Day, .Hour, .Minute, 0)
          If pNextRunTime < DateTime.Now Then pNextRunTime = pNextRunTime.AddMonths(1)
        Case clsEnums.enmJobType.Daily
          pNextRunTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, .Hour, .Minute, 0)
          If pNextRunTime < DateTime.Now Then pNextRunTime = pNextRunTime.AddDays(1)
        Case clsEnums.enmJobType.Weekly
          Dim pDay As System.DayOfWeek = .DayOfWeek
          pNextRunTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, .Hour, .Minute, 0)
          If pNextRunTime < DateTime.Now Then pNextRunTime = pNextRunTime.AddDays(1)
          For i = 0 To 6
            If pNextRunTime.AddDays(i).DayOfWeek = pDay Then
              pNextRunTime.AddDays(i)
              Exit For
            End If
          Next
        Case clsEnums.enmJobType.CyclicDay
          If vJob.CyclicCount <= 0 Then
            Return pFault.LogFreeTextFault(127, "", pFunctionParameters, "TRGT-140727-1226", vRequester)
          End If
          If vJob.LastRunTime = Nothing Then
            pNextRunTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, .Hour, .Minute, 0)
            If pNextRunTime < DateTime.Now Then pNextRunTime = pNextRunTime.AddDays(1)
          Else
            pNextRunTime = New Date(vJob.LastRunTime.Year, vJob.LastRunTime.Month, vJob.LastRunTime.Day, .Hour, .Minute, 0)
            Do Until pNextRunTime > DateTime.Now
              pNextRunTime = pNextRunTime.AddDays(vJob.CyclicCount)
            Loop
          End If
        Case clsEnums.enmJobType.CyclicHour
          If vJob.CyclicCount <= 0 Then
            Return pFault.LogFreeTextFault(127, "", pFunctionParameters, "TRGT-140822-1115", vRequester)
          End If
          If vJob.LastRunTime = Nothing Then
            pNextRunTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, .Minute, 0)
            If pNextRunTime < DateTime.Now Then pNextRunTime = pNextRunTime.AddHours(1)
          Else
            pNextRunTime = New Date(vJob.LastRunTime.Year, vJob.LastRunTime.Month, vJob.LastRunTime.Day, vJob.LastRunTime.Hour, .Minute, 0)
            Do Until pNextRunTime > DateTime.Now
              pNextRunTime = pNextRunTime.AddHours(vJob.CyclicCount)
            Loop
          End If
        Case clsEnums.enmJobType.CyclicMin
          If vJob.CyclicCount <= 0 Then
            Return pFault.LogFreeTextFault(127, "", pFunctionParameters, "TRGT-140727-1226", vRequester)
          End If
          If vJob.LastRunTime = Nothing Then
            pNextRunTime = DateTime.Now
          Else
            pNextRunTime = vJob.LastRunTime.AddMinutes(vJob.CyclicCount)
            If pNextRunTime < DateTime.Now Then pNextRunTime = DateTime.Now
          End If
        Case clsEnums.enmJobType.CyclicSec
          If vJob.CyclicCount <= 0 Then
            Return pFault.LogFreeTextFault(127, "", pFunctionParameters, "TRGT-140727-1226", vRequester)
          End If
          If vJob.LastRunTime = Nothing Then
            pNextRunTime = DateTime.Now
          Else
            pNextRunTime = vJob.LastRunTime.AddSeconds(vJob.CyclicCount)
            If pNextRunTime < DateTime.Now Then pNextRunTime = DateTime.Now
          End If
        Case clsEnums.enmJobType.OneOff
          pNextRunTime = New Date(.Year, .Month, DateTime.Now.Day, .Hour, .Minute, 0)
        Case Else
          Throw New Exception("Invalid JobType received:" & vJob.JobType.FastToString() & ": TRGT-LCL-140825-2220")
      End Select
    End With

    vJob.NextRunTime = pNextRunTime

    Return pFault.SetOK
  End Function

  Public Shared Function MarkJobAsComplete(ByVal vJobID As Long, ByVal vStatus As clsEnums.enmJobStatus, ByVal vWhenStarted As Date, ByVal vWhenCompleted As Date, ByVal vRemarks As String, ByVal vSuccessCount As Integer, ByVal vFailureCount As Integer, ByVal vFault As clsFault, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "JobID= " & vJobID & "; Status= " & vStatus.FastToString() & ""
    Dim pFault As clsFault

    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_MarkJobAsComplete, "ccTaskManager_MarkJobAsComplete", vRequester)
    If pFault.isOK = False Then Return pFault

    'If the job succeed, but it's contents didn't, then set the status to warning
    If vStatus = clsEnums.enmJobStatus.Success AndAlso vFailureCount > 0 Then
      vStatus = clsEnums.enmJobStatus.Warning
    End If

    'Get the job
    Dim pJob As New csJob
    pFault = pJob.GetByID(vJobID, vRequester, True) : If Not pFault.isOK Then Return pFault

    Dim pTranOptions As New System.Transactions.TransactionOptions()
    pTranOptions.Timeout = System.Transactions.TransactionManager.MaximumTimeout
    pTranOptions.IsolationLevel = Transactions.IsolationLevel.RepeatableRead ' don't need more than this

    Try
      Using pTran As New System.Transactions.TransactionScope(Transactions.TransactionScopeOption.Required, pTranOptions)

        'Assign a fault to the remarks
        If vFault Is Nothing Then
          'in order to enable the user to send 'nothing'
          vFault = New clsFault
          vFault.SetOK()
        End If
        If Not vFault.isOK Then vRemarks = If(String.IsNullOrEmpty(vRemarks), "", vRemarks & Environment.NewLine) & vFault.ShortStringForMessageBox(True)

        Dim pRemarks As String = vRemarks

        If (Not (pJob.JobCode.Equals("MoveAudits", StringComparison.OrdinalIgnoreCase) OrElse pJob.JobCode.Equals("ScanJobs", StringComparison.OrdinalIgnoreCase))) OrElse
              vStatus <> clsEnums.enmJobStatus.Success Then

          'Truncate text if needed
          If vRemarks.Length > 1000 Then
            Dim pFileName As String = $"{pJob.JobCode}_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            pRemarks = vRemarks.Substring(0, 960) & $"...{Environment.NewLine}See file '{pFileName}' on server."
            Try
              IO.File.WriteAllText(MyController.LogLocation & pFileName, vRemarks)
            Catch ex As Exception
              pRemarks = $"Writing file '{pFileName}' to server failed.{Environment.NewLine}{pRemarks}"
            End Try
          End If

          Dim pLoggedJob As New csLoggedJob
          With pLoggedJob
            .JobID = vJobID
            .WhenStarted = vWhenStarted
            .LastRunBy = vRequester.UserName & " @ LoginID: " & vRequester.LoggedLoginID
            .ActivatingUser = pJob.ActivatingUser
            .ExecutionTimeSec = ccHelper.ToInteger(vWhenCompleted.Subtract(.WhenStarted).TotalSeconds)
            .RunStatus = vStatus
            .Remarks = pRemarks
            .LoggedAlertID = vFault.LoggedAlertID
            .SuccessCount = vSuccessCount
            .FailureCount = vFailureCount
          End With
          pFault = pLoggedJob.Update(vRequester, False) : If Not pFault.isOK Then pTran.Dispose() : Return pFault
        End If

        With pJob
          If .JobType = clsEnums.enmJobType.OneOff Then
            .Active = False
            .NextRunTime = Nothing
          Else
            pFault = SetNextRunTime(pJob, vRequester) : If Not pFault.isOK Then pTran.Dispose() : Return pFault
          End If
          .ActivatingUser = "System on " & Environment.MachineName
          .LastRunTime = vWhenCompleted
          .JobStatus = vStatus
          .WarningMailSent = False
        End With
        pFault = pJob.UpdateFriend(vRequester, True) : If Not pFault.isOK Then pTran.Dispose() : Return pFault

        'Send alarms here, if any
        If pJob.JobStatus = clsEnums.enmJobStatus.Failure OrElse pJob.JobStatus = clsEnums.enmJobStatus.Warning Then
          pFault = SendMailOnFailure(pJob, vRemarks, False, vRequester) : If Not pFault.isOK Then pTran.Dispose() : Return pFault
        ElseIf pJob.JobStatus = clsEnums.enmJobStatus.Success AndAlso pJob.SendNotificationOnSuccess = True Then
          If Not (pJob.JobCode.Equals("MoveAudits", StringComparison.OrdinalIgnoreCase) OrElse pJob.JobCode.Equals("ScanJobs", StringComparison.OrdinalIgnoreCase)) Then
            'the user would drown!! We also didn't save a successful one to the DB to be read.
            pFault = SendRemarksAsMailOnSuccess(pJob.ID, vRequester) : If Not pFault.isOK Then pTran.Dispose() : Return pFault
          End If
        End If
        pTran.Complete()
      End Using
    Catch ex As Transactions.TransactionAbortedException
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-141120-1012", vRequester)
    Catch ex As Exception
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-141120-1013", vRequester)
    End Try

    Return pFault
  End Function

  Private Shared Function SendMailOnFailure(ByVal vJob As csJob, ByVal vText As String, ByVal vMissed As Boolean, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Job ID={0}, Job Code={1}, Runner={2}", vJob.ID, vJob.JobCode, vJob.JobRunnerCode)
    Dim pFault As clsFault

    If vJob.WarningMailSent = True Then
      pFault = New clsFault
      Return pFault.SetOK()
    End If

    Dim pLookups As New clsComboList()
    pFault = pLookups.FillLookup(clsEnums.enmLookup.Job, vRequester) : If Not pFault.isOK Then Return pFault
    vJob.JobText = pLookups.FindByKey(vJob.JobCode).Text
    pFault = pLookups.FillLookup(clsEnums.enmLookup.JobRunner, vRequester) : If Not pFault.isOK Then Return pFault
    vJob.JobRunnerText = pLookups.FindByKey(vJob.JobRunnerCode).Text

    Dim pHeader As String = ""

    If vMissed = True Then
      pHeader = "Job Missed" & ": " & vJob.JobText & " for " & vJob.JobRunnerText
      vText &= Environment.NewLine
      vText &= "NextRunTime = " & vJob.NextRunTime.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine
      vText &= "LastRunTime = " & vJob.LastRunTime.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine
    Else
      pHeader = vJob.JobStatus.FastToString() & ": " & vJob.JobText & " for " & vJob.JobRunnerText
    End If
    'Dim pMessage As String = vText & Environment.NewLine & Environment.NewLine & "Server Time is: " & DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US"))
    Dim pMessage As String = vText

    Dim pJobAlertRecipients As New csJobAlertRecipientCol()
    pFault = pJobAlertRecipients.FillByJobID(vJob.ID, vRequester) : If Not pFault.isOK Then Return pFault
    Dim pEmailList As String = ""
    For Each l In pJobAlertRecipients
      Dim pName As String = ""
      Dim pEmail As String = ""

      If l.JobAlertType <> clsEnums.enmJobAlertType.email Then Continue For

      If Not String.IsNullOrEmpty(l.OverrideName) Then
        pName = l.OverrideName
        pEmail = l.OverrideEmailOrPhone
      End If
      If String.IsNullOrEmpty(pEmail) AndAlso l.UserID > 0 Then
        Dim pUser As New csUser()
        pFault = pUser.GetByID(l.UserID, vRequester, True) : If Not pFault.isOK Then Return pFault
        pName = pUser.FullName
        pEmail = pUser.Email
      End If
      If pEmail = "" Then
        pFault.LogFreeTextFault(129, "JobAlertRecipient=" & l.ID & " - " & pName, pFunctionParameters, "TRGT-140720-1634", vRequester)
        If Not pFault.isOK() Then pFault.SetOK(vRequester) 'this won't stop us
        Continue For
      End If

      If Not String.IsNullOrEmpty(pName) Then pEmailList &= pName & ","
      pEmailList &= pEmail & ";"
    Next

    Dim pFrequency As String = ""
    If vJob.JobType = clsEnums.enmJobType.OneOff Then
      pFrequency = $"This job ran once only"
    ElseIf vJob.JobType = clsEnums.enmJobType.Annually Then
      pFrequency = $"This job runs once a year"
    ElseIf vJob.JobType = clsEnums.enmJobType.Daily Then
      pFrequency = $"This job runs once a day"
    ElseIf vJob.JobType = clsEnums.enmJobType.Monthly Then
      pFrequency = $"This job runs once a month"
    ElseIf vJob.JobType = clsEnums.enmJobType.Weekly Then
      pFrequency = $"This job runs once a week"
    ElseIf vJob.JobType = clsEnums.enmJobType.CyclicDay Then
      pFrequency = $"This job runs every {vJob.CyclicCount} day"
    ElseIf vJob.JobType = clsEnums.enmJobType.CyclicHour Then
      pFrequency = $"This job runs every {vJob.CyclicCount} hour"
    ElseIf vJob.JobType = clsEnums.enmJobType.CyclicMin Then
      pFrequency = $"This job runs every {vJob.CyclicCount} minute"
    ElseIf vJob.JobType = clsEnums.enmJobType.CyclicSec Then
      pFrequency = $"This job runs every {vJob.CyclicCount} second"
    Else
      pFrequency = $"This job runs every unknown"
    End If
    If vJob.CyclicCount > 0 Then
      pFrequency &= "s"
    End If
    pMessage = pMessage & Environment.NewLine & pFrequency

    Dim pMessageSuffix As String = ""
    If String.IsNullOrEmpty(pEmailList) Then
      'get the system admin email
      Dim pDefault As New csSystemDefault
      pFault = pDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Config_ProblemMailTo, vRequester, True) : If Not pFault.isOK Then Return pFault
      If String.IsNullOrEmpty(pDefault.SettingValue) Then
        pFault.LogFreeTextFault(132, "", pFunctionParameters, "TRGT-140720-1633", vRequester)
        If Not pFault.isOK() Then pFault.SetOK(vRequester) 'this won't stop us
      Else
        pMessageSuffix = "Sent to you as 'Default Admin Email'"
        pEmailList = pDefault.SettingValue
      End If
    End If
    If String.IsNullOrEmpty(pEmailList) Then
      'get the config failed mail
      pMessageSuffix = "Sent to you as 'ProblemMailTo'"
      pEmailList = MyController.ProblemMailTo
    End If

    If Not String.IsNullOrEmpty(pMessageSuffix) Then pMessage &= Environment.NewLine & Environment.NewLine & pMessageSuffix

    pFault = ccHelper.SendSMSorEmail(pMessage, pEmailList, vRequester, vSubject:=pHeader)
    If Not pFault.isOK Then pFault.SetOK(vRequester) 'this won't stop us

    vJob.WarningMailSent = True
    pFault = vJob.UpdateFriend(vRequester, False) : If Not pFault.isOK Then Return pFault

    Return pFault
  End Function

  ''' <summary>
  ''' If there are remarks, it will say Check Remarks in the title
  ''' </summary>
  ''' <param name="vJobID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  Private Shared Function SendRemarksAsMailOnSuccess(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Job ID={0}", vJobID)
    Dim pFault As New clsFault

    'Get a fresh copy of the job
    Dim pJob As New csJob
    pFault = pJob.GetByID(vJobID, vRequester, True) : If Not pFault.isOK Then Return pFault

    'Get Previous response before updating, for comparison
    Dim pLoggedJobs As New csLoggedJobCol()
    pFault = pLoggedJobs.FillByJobID(pJob.ID, vRequester, 2, clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then Return pFault
    Dim pLastLoggedJob As csLoggedJob
    Dim pThisLoggedJob As csLoggedJob
    If pLoggedJobs.Count >= 2 Then
      pThisLoggedJob = pLoggedJobs(0)
      pLastLoggedJob = pLoggedJobs(1) 'use the 2nd to last one, since the last one is the present one (we just marked it as complete)!
    ElseIf pLoggedJobs.Count = 1 Then
      pThisLoggedJob = pLoggedJobs(0)
      pLastLoggedJob = New csLoggedJob
    Else
      'No message to send
      Return pFault
    End If

    'send mail if there is a change in the problem, or if it's the 1st run of the day
    Dim pSendMail As Boolean = False
    If pThisLoggedJob.WhenStarted.Day <> pLastLoggedJob.WhenStarted.Day Then
      pSendMail = True
    ElseIf pThisLoggedJob.Remarks <> pLastLoggedJob.Remarks Then
      pSendMail = True
    End If

    If pSendMail = False Then Return pFault 'no mail to send

    Dim pLookups As New clsComboList()
    pFault = pLookups.FillLookup(clsEnums.enmLookup.Job, vRequester) : If Not pFault.isOK Then Return pFault
    pJob.JobText = pLookups.FindByKey(pJob.JobCode).Text

    Dim pHeader As String = ""

    pHeader = pJob.JobText & ": Job succeeded"

    'Dim pMessage As String = pThisLoggedJob.Remarks & Environment.NewLine & Environment.NewLine & "Server Time is: " & DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US"))
    Dim pMessage As String = pThisLoggedJob.Remarks

    If Not String.IsNullOrEmpty(pThisLoggedJob.Remarks) Then
      pHeader &= " - Check remarks"
    Else
      pMessage = "All OK!"
    End If

    Dim pJobAlertRecipients As New csJobAlertRecipientCol()
    pFault = pJobAlertRecipients.FillByJobID(vJobID, vRequester) : If Not pFault.isOK Then Return pFault
    Dim pEmailList As String = ""
    For Each l In pJobAlertRecipients
      Dim pName As String = ""
      Dim pEmail As String = ""

      If l.JobAlertType <> clsEnums.enmJobAlertType.email Then Continue For

      If Not String.IsNullOrEmpty(l.OverrideName) Then
        pName = l.OverrideName
        pEmail = l.OverrideEmailOrPhone
      End If
      If String.IsNullOrEmpty(pEmail) AndAlso l.UserID > 0 Then
        Dim pUser As New csUser()
        pFault = pUser.GetByID(l.UserID, vRequester, True) : If Not pFault.isOK Then Return pFault
        pName = pUser.FullName
        pEmail = pUser.Email
      End If
      If pEmail = "" Then
        pFault.LogFreeTextFault(129, "JobAlertRecipient=" & l.ID & " - " & pName, pFunctionParameters, "TRGT-140720-1634", vRequester)
        If Not pFault.isOK() Then pFault.SetOK(vRequester) 'this won't stop us
        Continue For
      End If

      If Not String.IsNullOrEmpty(pName) Then pEmailList &= pName & ","
      pEmailList &= pEmail & ";"
    Next

    Dim pFrequency As String = ""
    If pJob.JobType = clsEnums.enmJobType.OneOff Then
      pFrequency = $"This job ran once only"
    ElseIf pJob.JobType = clsEnums.enmJobType.Annually Then
      pFrequency = $"This job runs once a year"
    ElseIf pJob.JobType = clsEnums.enmJobType.Daily Then
      pFrequency = $"This job runs once a day"
    ElseIf pJob.JobType = clsEnums.enmJobType.Monthly Then
      pFrequency = $"This job runs once a month"
    ElseIf pJob.JobType = clsEnums.enmJobType.Weekly Then
      pFrequency = $"This job runs once a week"
    ElseIf pJob.JobType = clsEnums.enmJobType.CyclicDay Then
      pFrequency = $"This job runs every {pJob.CyclicCount} day"
    ElseIf pJob.JobType = clsEnums.enmJobType.CyclicHour Then
      pFrequency = $"This job runs every {pJob.CyclicCount} hour"
    ElseIf pJob.JobType = clsEnums.enmJobType.CyclicMin Then
      pFrequency = $"This job runs every {pJob.CyclicCount} minute"
    ElseIf pJob.JobType = clsEnums.enmJobType.CyclicSec Then
      pFrequency = $"This job runs every {pJob.CyclicCount} second"
    Else
      pFrequency = $"This job runs every unknown"
    End If
    If pJob.CyclicCount > 0 Then
      pFrequency &= "s"
    End If
    pMessage = pMessage & Environment.NewLine & pFrequency

    Dim pMessageSuffix As String = ""
    If String.IsNullOrEmpty(pEmailList) Then
      'get the system admin email
      Dim pDefault As New csSystemDefault
      pFault = pDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Config_ProblemMailTo, vRequester, True) : If Not pFault.isOK Then Return pFault
      If String.IsNullOrEmpty(pDefault.SettingValue) Then
        pFault.LogFreeTextFault(132, "", pFunctionParameters, "TRGT-140720-1633", vRequester)
        If Not pFault.isOK() Then pFault.SetOK(vRequester) 'this won't stop us
      Else
        pMessageSuffix = "Sent to you as 'Default Admin Email'"
        pEmailList = pDefault.SettingValue
      End If
    End If
    If String.IsNullOrEmpty(pEmailList) Then
      'get the config failed mail
      pMessageSuffix = "Sent to you as 'ProblemMailTo'"
      pEmailList = MyController.ProblemMailTo
    End If

    If Not String.IsNullOrEmpty(pMessageSuffix) Then pMessage &= Environment.NewLine & Environment.NewLine & pMessageSuffix

    pFault = ccHelper.SendSMSorEmail(pMessage, pEmailList, vRequester, vSubject:=pHeader)
    If Not pFault.isOK() Then pFault.SetOK(vRequester) 'this won't stop us

    pJob.WarningMailSent = True
    pFault = pJob.UpdateFriend(vRequester, False) : If Not pFault.isOK Then Return pFault

    Return pFault
  End Function

  Public Shared Function SetJobToNow(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "JobID=" & vJobID
    Dim pFault As clsFault

    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_SetJobToNow, "ccTaskManager_SetJobToNow", vRequester)
    If pFault.isOK = False Then Return pFault

    'This is only done for managed jobs!
    Dim pJob As New csJob
    pFault = pJob.GetByID(vJobID, vRequester, True) : If Not pFault.isOK Then Return pFault
    pFunctionParameters = String.Format("Job ID={0}, Job Code={1}, Runner={2}", pJob.ID, pJob.JobCode, pJob.JobRunnerCode)

    If pJob.JobStatus = clsEnums.enmJobStatus.InProcess AndAlso pJob.NextRunTime.AddSeconds(pJob.TimeOutSec) > DateTime.Now Then
      Return pFault.LogFreeTextFault(133, String.Format("It was run by {0} at {1}", pJob.ActivatingUser, pJob.LastRunTime.ToString("dd/MM/yyyy HH:mm")), pFunctionParameters, "TRGT-140826-1009", vRequester)
    End If

    pFault = csJob.UpdateSetToNow(pJob.ID, DateTime.Now, True, False, vRequester.UserName, vRequester) : If Not pFault.isOK Then Return pFault

    Return pFault
  End Function

  Friend Shared Function UnlockUsers(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionName As String = "UnlockUsers"
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault

    Dim pStartTime As Date = DateTime.Now

    Dim pUserContactStatus As New Text.StringBuilder()

    'LoadTransactions
    Dim pUsers As New csUserCol(clsEnums.enmLoadParent.DoNotLoad, vRequester, pFault)

    Dim pNumScanned As Integer = 0
    Dim pNumFailed As Integer = 0
    Dim pNumSucceeded As Integer = 0
    'Now go through each one and do it
    For Each l In pUsers
      If l.IsDisabled Then Continue For
      pNumScanned += 1

      If l.IsLockedOut Then
        l.IsLockedOut = False
        pFault = l.Update(vRequester, False)
        If Not pFault.isOK Then
          pNumFailed += 1
          pFault.SetOK(vRequester)
        Else
          pNumSucceeded += 1
        End If
      End If

      'Now check UserContactStatus
      If l.AuthenticationMethod = clsEnums.enmAuthenticationMethod.UD Then
        pUserContactStatus.AppendLine($"AuthenticationMethod: Undefined for User {l.UserName} ")
        Continue For
      End If
      If l.AuthenticationMethod <> clsEnums.enmAuthenticationMethod.NamePassword Then
        If l.MessagingMode = clsEnums.enmMessagingMode.UD Then
          pUserContactStatus.AppendLine($"MessagingMode: Undefined for User {l.UserName} ")
        ElseIf l.MessagingMode = clsEnums.enmMessagingMode.Email Then
          If String.IsNullOrEmpty(l.Email) OrElse l.Email.IndexOf("@") < 0 Then
            pUserContactStatus.AppendLine($"Email: Invalid email '{l.Email}' for User {l.UserName} ")
          End If
        ElseIf l.MessagingMode = clsEnums.enmMessagingMode.SMS Then
          If String.IsNullOrEmpty(l.PhoneNumber) OrElse Not ccHelper.IsNumeric(l.PhoneNumber.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "")) Then
            pUserContactStatus.AppendLine($"PhoneNumber: Invalid PhoneNumber '{l.PhoneNumber}' for User {l.UserName} ")
          End If
        End If
      Else
        If DateTime.Now.DayOfWeek = DayOfWeek.Saturday AndAlso DateTime.Now.Hour = 1 Then
          'check that we have email
          If String.IsNullOrEmpty(l.Email) OrElse l.Email.IndexOf("@") < 0 Then
            pUserContactStatus.AppendLine($"User {l.UserName}: Invalid email '{l.Email}' ")
          End If
          'check that we have Phone
          'If String.IsNullOrEmpty(l.PhoneNumber) OrElse Not ccHelper.IsNumeric(l.PhoneNumber.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "")) Then
          '  pUserContactStatus.AppendLine($"User {l.UserName}: Invalid PhoneNumber '{l.PhoneNumber}' ")
          'End If
        End If
      End If
    Next

    Dim pUserContactStatusToSend As String = pUserContactStatus.ToString()
    Dim pStatus As clsEnums.enmJobStatus = clsEnums.enmJobStatus.Success
    Dim pMessage As String = $"Scanned {pNumScanned} users"
    If Not String.IsNullOrEmpty(pUserContactStatusToSend) Then
      pStatus = clsEnums.enmJobStatus.Warning
      pMessage &= Environment.NewLine & Environment.NewLine & pUserContactStatusToSend
    End If

    pFault = ccTaskManager.MarkJobAsComplete(vJobID, pStatus, pStartTime, DateTime.Now, pMessage, pNumSucceeded, pNumFailed, Nothing, vRequester)
    If Not pFault.isOK() Then pFault.SetOK(vRequester)

    Return pFault
  End Function

  ''' <summary>
  ''' This function runs a job that is totally encompassed in a stored procedure
  ''' </summary>
  ''' <param name="vJobID"></param>
  ''' <param name="vSP"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  Public Shared Function RunSP(ByVal vJobID As Long, ByVal vSP As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionName As String = "RunSP"
    Dim pFunctionParameters As String = ""
    Dim pFault As clsFault

    Dim pStartTime As Date = DateTime.Now

    Dim pCommandText As String = vSP
    Dim pDALParameters As New ccDAL.csTargCCParameterCol

    Dim pResult As String = ""

    Try
      Dim pTargCCReader As ccDAL.csTargCCReader = Nothing
      pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader)

      If pFault.isOK = True Then
        If pTargCCReader.HasRows = True Then
          pTargCCReader.Read()
          pResult = pTargCCReader(0).ToString
        End If
      End If
    Catch ex As Exception
      pFault = New clsFault
      pFault.LogException(ex, pFunctionParameters, "TRGT-170402-1430", vRequester)
    End Try
    If Not pFault.isOK Then Return pFault

    pFault = ccTaskManager.MarkJobAsComplete(vJobID, clsEnums.enmJobStatus.Success, pStartTime, DateTime.Now, pResult, Nothing, Nothing, Nothing, vRequester)
    If Not pFault.isOK() Then pFault.SetOK(vRequester)

    Return pFault
  End Function

End Class

