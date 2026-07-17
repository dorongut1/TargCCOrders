Public Class ccDatabaseMaintenance 
  
  Public Shared Function ResetPermissionsForDefaultRoles(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceResetPermissionsForDefaultRoles" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1311", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Shared Function WriteDatabaseToBinary(ByVal vDatabaseInOneFile As Boolean, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vDatabaseInOneFile) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceWriteDatabaseToBinary" 
      Dim pParametersToLog = $"DatabaseInOneFile: {vDatabaseInOneFile};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1318", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function EjectAllUsers(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceEjectAllUsers" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1321", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function EjectNonMaster(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceEjectNonMaster" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1323", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function RequestIndexReorganization(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceRequestIndexReorganization" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1324", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  Public Shared Function EnableCLR(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Return pFault.LogFreeTextFault(96, "EnableCLR Can only be enabled via the DBController", "", "TRGT-160621-1520", vRequester) 
 
    Return pFault 
  End Function 
 
  Public Shared Function RunSQLScriptOnServer(ByVal vScript As String, ByRef rResponse As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Return pFault.LogFreeTextFault(96, "RunSQLScriptOnServer Can only be enabled via the DBController", "", "TRGT-160731-1243", vRequester) 
 
    Return pFault 
  End Function 
 
  ''' <summary>  
  ''' This runs a stored procedure. The stored procedure should end with 'SELECT -1 AS ID', as a flag of success. It is public, and only SysAdmin can run it 
  ''' </summary>  
  ''' <param name="vSprocName"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function RunSQLStoredProcedureOnServer(ByVal vSprocName As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Return pFault.LogFreeTextFault(96, "RunSQLStoredProcedureOnServer Can only be enabled via the DBController", "", "TRGT-170612-2058", vRequester) 
 
    Return pFault 
  End Function 
 
  Public Shared Function RequestDatabaseBackup(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceRequestDatabaseBackup" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1325", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  Public Shared Function GetDatabaseFileSizes(ByVal vRequester As clsRequester, ByRef rDBName As List(Of String), ByRef rFileName As List(Of String), ByRef rType As List(Of String), ByRef rCurrentSize As List(Of Integer), ByRef rFreeSpace As List(Of Integer)) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'erase or initialize referenced values 
    rDBName = New List(Of String) 
    rFileName = New List(Of String) 
    rType = New List(Of String) 
    rCurrentSize = New List(Of Integer) 
    rFreeSpace = New List(Of Integer) 
 
    Dim pClass As String = "ccDatabaseMaintenance" 
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
      pFault = WebAPI.ExecuteFunction(pClass, pFunction, pWSALParameters, vRequester, pResults) : If Not pFault.isOK Then Return pFault 
 
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
 
  Public Shared Function TranslationAddAllPossibilitiesToObjectToTranslate(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceTranslationAddAllPossibilitiesToObjectToTranslate" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1326", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function TranslationRemoveUnusedPossibilitiesFromObjectToTranslate(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceTranslationRemoveUnusedPossibilitiesFromObjectToTranslate" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1327", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function FillAllTranslationPossibilities(ByVal vObjectToTranslateID As Long, ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester, ByRef rObjectTranslations As csObjectTranslationCol) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vObjectToTranslateID) 
          pBinaryWriter.Write(vLanguage.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccDatabaseMaintenanceFillAllTranslationPossibilities" 
      Dim pParametersToLog = $"ObjectToTranslateID: {vObjectToTranslateID};Language: {vLanguage.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the rObjectTranslations  
      rObjectTranslations = New csObjectTranslationCol(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, "", "TRGT-150424-1328", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
End Class 
