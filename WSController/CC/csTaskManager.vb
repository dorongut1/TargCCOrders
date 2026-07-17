Imports System.Security.Cryptography

Public Class ccTaskManager

  Public Shared Function SetJobToNow(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "JobID=" & vJobID
    Dim pFault As New clsFault

    Try
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it 

      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream()
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream)
          pBinaryWriter.Write(vJobID)
          pBinaryWriter.Close()
        End Using
        pRequest = pMemoryStream.ToArray()
        pMemoryStream.Close()
      End Using

      'Run the request 
      Dim pFunction As String = "ccTaskManagerSetJobToNow"
      Dim pParametersToLog = $"JobID: {vJobID};"
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault
    Catch ex As Exception
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150424-1915", vRequester)
    End Try

    Return pFault
  End Function

  Public Shared Function GetSpecificUnmanagedJobForRunner(ByVal vRunnerCode As String, ByVal vJobCode As String, ByRef rJob As csJob, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "RunnerCode= " & vRunnerCode & "; JobCode= " & vJobCode & ""
    Dim pFault As New clsFault

    Try
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing
      Dim pResponse As Byte() = Nothing

      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream()
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream)
          pBinaryWriter.Write(vRunnerCode)
          pBinaryWriter.Write(vJobCode)
          pBinaryWriter.Close()
        End Using
        pRequest = pMemoryStream.ToArray()
        pMemoryStream.Close()
      End Using

      'Run the request 
      Dim pFunction As String = "ccTaskManagerGetSpecificUnmanagedJobForRunner"
      Dim pParametersToLog = $"RunnerCode: {vRunnerCode};JobCode: {vJobCode};"
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault

      'Use the response to build the Job 
      rJob = New csJob(pResponse, pFault, vRequester)
    Catch ex As Exception
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150424-1919", vRequester)
    End Try

    Return pFault
  End Function

  Public Shared Function MarkJobAsComplete(ByVal vJobID As Long, ByVal vStatus As clsEnums.enmJobStatus, ByVal vWhenStarted As Date, ByVal vWhenCompleted As Date, ByVal vRemarks As String, ByVal vSuccessCount As Integer, ByVal vFailureCount As Integer, ByVal vFault As clsFault, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "JobID= " & vJobID & "; Status= " & vStatus.ToString & ""
    Dim pFault As New clsFault

    Try
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it 

      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream()
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream)
          pBinaryWriter.Write(vJobID)
          pBinaryWriter.Write(vStatus.ToString())
          pBinaryWriter.Write(vWhenStarted.Ticks)
          pBinaryWriter.Write(vWhenCompleted.Ticks)
          pBinaryWriter.Write(vRemarks)
          pBinaryWriter.Write(vSuccessCount)
          pBinaryWriter.Write(vFailureCount)
          Dim pByte As Byte() = vFault.CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault
          pBinaryWriter.Write(pByte.Length)
          pBinaryWriter.Write(pByte)
          pBinaryWriter.Close()
        End Using
        pRequest = pMemoryStream.ToArray()
        pMemoryStream.Close()
      End Using

      'Run the request 
      Dim pFunction As String = "ccTaskManagerMarkJobAsComplete"
      Dim pParametersToLog = $"JobID: {vJobID};Status: {vStatus};"
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault

    Catch ex As Exception
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150424-1919", vRequester)
    End Try

    Return pFault
  End Function

End Class
