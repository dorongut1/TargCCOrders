Public Class ccDatabaseMaintenance 
  
  Public Shared Function ResetPermissionsForDefaultRoles(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_CreateDefaultPermissionsForNewRolesAndTables, "ccDatabaseMaintenance_CreateDefaultPermissionsForNewRolesAndTables", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
        Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-150216-2159", vRequester) 
    Else 
      Dim pCommandText As String = "c__PermissionCreateDefault" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
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
    End If
 
    Return pFault 
  End Function 
  Public Shared Function EjectAllUsers(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_EjectAllUsers, "ccDatabaseMaintenance_EjectAllUsers", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-150216-2203", vRequester) 
    Else 
        Dim pCommandText As String = "c__EjectAll" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ChangedBy" 
        pParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName 
        pLastReadVariableName = "UpdatingLoginID" 
        pParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID 
        pLastReadVariableName = "" 
   
        'Execute query   
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader, 120) 
   
        'I expect to get -1 back   
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-160326-1641", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-160326-1642", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
 
    Return pFault 
  End Function 
 
  Public Shared Function EjectNonMaster(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_EjectNonMaster, "ccDatabaseMaintenance_EjectNonMaster", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-150216-2204", vRequester) 
    Else 
      Dim pCommandText As String = "c__EjectNonMaster" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ChangedBy" 
        pParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName 
        pLastReadVariableName = "UpdatingLoginID" 
        pParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID 
        pLastReadVariableName = "" 
   
        'Execute query   
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader, 120) 
   
        'I expect to get -1 back   
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-160326-1705", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-160326-1706", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
 
    Return pFault 
  End Function 
  
  Public Shared Function RequestIndexReorganization(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_RequestIndexReorganization, "ccDatabaseMaintenance_RequestIndexReorganization", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pJob As New csJob 
    pFault = pJob.GetByJobCodeAndJobRunnerCode("ReorganizeIndexes", "LocalTaskManager", vRequester, True) : If Not pFault.isOK Then Return pFault 
    If pJob.Active = True Then 
      Return pFault.LogFreeTextFault("There is already an planned Index Reorganization that is active.", pFunctionParameters, "TRGT-140804-1331", vRequester) 
    End If 
 
    pFault = ccTaskManager.SetJobToNow(pJob.ID, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Return pFault 
  End Function 
 
  Friend Shared Function ReorganizeIndexes(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pStartTime As Date = DateTime.Now 
 
    'At 1st, I though it best to eject non-masters and block everyone while reindexing. However, that caused the database to remain 'dirty' if there was a failure. 
    ' Since I'm not sure it's needed, I'm commenting it out for DateTime.Now. 
    'Dim pSystemDefault As New csSystemDefault 
    'pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.RealTime_BlockNonmasterLogin, vRequester, True) : If pFault.isOK = False Then Return pFault 
    'pSystemDefault.UpdateSettingValue("1", vRequester) : If pFault.isOK = False Then Return pFault 
 
    'pFault = EjectNonMaster(vRequester) 
    'If pFault.isOK = False Then Return pFault 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-150216-2208", vRequester) 
    Else 
        Dim pCommandText As String = "c__ReorganizeIndexes" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "" 
   
        'Execute query   
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader, 120) 
   
        'I expect to get -1 back   
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-160326-1708", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-160326-1707", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
 
    'open lockout 
    'pSystemDefault = New csSystemDefault 
    'pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.RealTime_BlockNonmasterLogin, vRequester) : If pFault.isOK = False Then Return pFault 
    'pSystemDefault.UpdateSettingValue("0", vRequester) : If pFault.isOK = False Then Return pFault 
 
    'Report Response 
    pFault = ccTaskManager.MarkJobAsComplete(vJobID, clsEnums.enmJobStatus.Success, pStartTime, DateTime.Now, "", Nothing, Nothing, Nothing, vRequester) 
    If Not pFault.isOK Then pFault.SetOK(vRequester) 'continue on 
 
    Return pFault 
  End Function 
 
  Public Shared Function RequestDatabaseBackup(ByVal vRequester As clsRequester) As clsFault  
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_RequestDatabaseBackup, "ccDatabaseMaintenance_RequestDatabaseBackup", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pJob As New csJob 
    pFault = pJob.GetByJobCodeAndJobRunnerCode("BackupDB", "LocalTaskManager", vRequester, True) : If Not pFault.isOK Then Return pFault 
    If pJob.Active Then 
      Return pFault.LogFreeTextFault("There is already an planned Database Backup that is active.", pFunctionParameters, "TRGT-140804-1331", vRequester) 
    End If 
 
    pFault = ccTaskManager.SetJobToNow(pJob.ID, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Return pFault 
  End Function 
 
  Public Shared Function EnableCLR(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_SysAdmin, "ccDatabaseMaintenance_EnableCLR", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Can't enable CLR on a local database", pFunctionParameters, "TRGT-160609-1324", vRequester)  
    Else 
        Dim pCommandText As String = "c__EnableCLR" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters   
        pLastReadVariableName = "" 
   
        'Execute query    
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader, 120) 
   
        'I expect to get -1 back    
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-160609-1326", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-160609-1325", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-160609-1324", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If 
 
 
    Return pFault 
  End Function 
 
  Friend Shared Function CreateSQLUser(ByVal vUserName As String, ByVal vPassword As String, ByVal vSID As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_SysAdmin, "ccDatabaseMaintenance_EnableCLR", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Can't enable a user on a local database", pFunctionParameters, "TRGT-170131-1437", vRequester) 
    Else 
      Dim pCommandText As String = "c__CreateUser" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters    
        pLastReadVariableName = "UserName" 
        pParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = vUserName 
        pLastReadVariableName = "Password" 
        pParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = vPassword 
        pLastReadVariableName = "SID" 
        pParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = vSID 
        pLastReadVariableName = "" 
 
        'Execute query     
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader, 120) 
 
        'I expect to get -1 back     
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-160609-1326", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-160609-1325", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-160609-1324", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' SQL User must be a SysAdmin or at least a DBOwner to run this. 
  ''' This should only be run by an authorized user. 
  ''' The SQL User defined in the config file must be a SysAdmin or the dbOwner. 
  ''' </summary> 
  ''' <param name="vScript"></param> 
  ''' <param name="rResponse"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function RunSQLScriptOnServer(ByVal vScript As String, ByRef rResponse As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_SysAdmin, "ccDatabaseMaintenance_RunSQLScriptOnServer", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Can't Run SQL Script on a local database", pFunctionParameters, "TRGT-160728-1831", vRequester)   
    Else 
   
      'Only a DBOwner or SysAdmin SQL User can run this 
      Dim pIsSysAdmin As Boolean 
      Dim pIsDBOwner As Boolean 
      Dim pIsClrEnabled As Boolean 
 
      pFault = GetActiveSQLUserRights(pIsSysAdmin, pIsDBOwner, pIsClrEnabled, vRequester) 
      If Not pFault.isOK Then Return pFault 
      If Not (pIsSysAdmin OrElse pIsDBOwner) Then 
        Return pFault.LogFreeTextFault(62, "SQL User must be a SysAdmin or at least a dbOwner to run this", pFunctionParameters, "TRGT-160728-1831", vRequester) 
      End If 
 
      Try 
        'Execute query     
        rResponse = Nothing 
        pFault = ccDAL.ExecuteScript(vScript, rResponse, vRequester) 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-160609-1324", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If 
 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_SysAdmin, "ccDatabaseMaintenance_RunSQLStoredProcedureOnServer", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-170612-2136", vRequester) 
    Else 
      Dim pCommandText As String = vSprocName 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
 
      'Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters   
        'pLastReadVariableName = "ChangedBy" 
        'pParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName 
        'pLastReadVariableName = "UpdatingLoginID" 
        'pParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID 
        'pLastReadVariableName = "" 
 
        'Execute query    
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader) 
 
        'I expect to get -1 back    
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-170612-2135", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-170612-2134", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        'If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-170612-2133", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  Friend Shared Function BackupDatabase(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pStartTime As Date = DateTime.Now  
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Can't back up a local database", pFunctionParameters, "TRGT-131021-2241", vRequester) 
    Else 
      Dim pCommandText As String = "c__BackupDatabase" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "DBName" 
        pParameters.Add("DBName", ccDAL.enmSQLDataType.VarChar, 50).Value = MyController.DBName 
        pLastReadVariableName = "BackupDBName" 
        pParameters.Add("BackupDBName", ccDAL.enmSQLDataType.NVarChar, 50).Value = MyController.DBName & "_" & DateTime.Now.ToString("yyMMdd_HHmmss") & ".bak" 
        pLastReadVariableName = "" 
   
        'Execute query   
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader, 120) 
   
        'I expect to get -1 back   
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-160326-1709", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-160326-1708", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
 
    pFault = ccTaskManager.MarkJobAsComplete(vJobID, clsEnums.enmJobStatus.Success, pStartTime, DateTime.Now, "", Nothing, Nothing, Nothing, vRequester) 
    If Not pFault.isOK Then pFault.SetOK(vRequester) 'continue on 
 
    Return pFault 
  End Function 
 
  Friend Shared Function GetActiveSQLUserRights(ByRef rIsSysAdmin As Boolean, ByRef rIsDBOwner As Boolean, ByRef rIsClrEnabled As Boolean, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As clsFault 
 
    Dim pStartTime As Date = DateTime.Now 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Can't get SQL User rights from a non-SQL database - back up a local database 
      rIsSysAdmin = False 
      rIsDBOwner = False 
      rIsClrEnabled = False 
      pFault = New clsFault 
      pFault.SetOK() 
    Else 
      Dim pCommandText As String = "c__GetActiveSQLUserRights" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters   
   
        'Execute query    
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader, 120) 
   
        'I expect to get -1 back    
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            pLastReadVariableName = "rIsSysAdmin" 
            rIsSysAdmin = CBool(pTargCCReader(0)) 
            pLastReadVariableName = "rIsDBOwner" 
            rIsDBOwner = CBool(pTargCCReader(1)) 
            pLastReadVariableName = "rIsClrEnabled" 
            rIsClrEnabled = CBool(pTargCCReader(2)) 
            pLastReadVariableName = "" 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-160326-1708", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault = New clsFault 
        pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If 
 
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
 
    'Check permission 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_TableSizeView, "ccDatabaseMaintenance_GetDatabaseFileSizes", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c__DatabaseFileSizesFill" 
    Dim pParameters As New ccDAL.csTargCCParameterCol 
 
    Dim pLastReadVariableName As String = "" 
    Try 
      'set parameters   
      'pLastReadVariableName = "PaymentMonth" 'not needed for this query 
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
              rCurrentSize.Add(ccHelper.ToInteger(Decimal.Round(pTargCCReader.GetDecimal(3), 0))) 
              rFreeSpace.Add(ccHelper.ToInteger(Decimal.Round(pTargCCReader.GetDecimal(4), 0))) 
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
 
  Public Shared Function TranslationAddAllPossibilitiesToObjectToTranslate(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pObjectName As String 
    Dim pClassType As Type 
    Dim pClass As Object 
 
    Dim pObjectToTranslate As csObjectToTranslate 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_HandleObjectToTranslate, "ccDatabaseMaintenance_TranslationAddAllPossibilitiesToObjectToTranslate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Get the list of tables in the system 
    Dim pTableCol As New csTableCol 
    pFault = pTableCol.Fill(vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Get the ObjectToTranslateCol 
    Dim pObjectToTranslateCol As New csObjectToTranslateCol 
    pFault = pObjectToTranslateCol.FillByObjectType(clsEnums.enmObjectType.TableFieldName, vRequester) 
    If pFault.isOK = False Then Return pFault 
    pFault = pObjectToTranslateCol.FillByObjectType(clsEnums.enmObjectType.TableData, vRequester, vAppend:=True) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly() 
 
    'Get the Root NameSpace 
    Dim pRootNameSpace As String = "" 
    'Try 
    '  'this does not seem to work in .NET Standard - returns nothing 
    '  pRootNameSpace = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames(0) 
    '  If pRootNameSpace.EndsWith(".Resources.resources", StringComparison.OrdinalIgnoreCase) Then 
    '    pRootNameSpace = pRootNameSpace.Substring(0, pRootNameSpace.Length - 20) 
    '  End If 
    'Catch ex As Exception 
    '  pFault = New clsFault 
    '  Return pFault.LogException(ex, "'pRootNameSpace = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames(0)' didn't work", "TRGT-170301-1055", vRequester) 
    'End Try 
    pRootNameSpace = "TargCCOrders.DataController" 
 
    'now set the tag, so we can delete them if not used 
    For Each l In pObjectToTranslateCol 
      l.Tag = "NotUsed" 
    Next 
 
    'Now scan the tables in each column 
    For Each pTable As csTable In pTableCol 
      Dim pTableName As String = pTable.Name 
 
      If pTableName = "c_Language" Then Continue For 
      If pTableName = "c_SystemAudit" Then Continue For 
      If pTableName = "c_Table" Then Continue For 
 
      'Save the Table Title 
      pObjectToTranslate = pObjectToTranslateCol.FindByObjectTypeAndObjectAndItem(clsEnums.enmObjectType.TableFieldName, pTableName, "_TableTitle") 
      If pObjectToTranslate.ID = 0 Then 
        'not found, so add it  
        pObjectToTranslate = New csObjectToTranslate 
        With pObjectToTranslate 
          .ObjectType = clsEnums.enmObjectType.TableFieldName 
          .Object = pTableName 
          .Item = "_TableTitle" 
          pFault = .Update(vRequester, True) 
        End With 
        If pFault.isOK = False Then Return pFault 
      Else 
        pObjectToTranslate.Tag = "" 
      End If 
 
      pObjectName = pTable.Name 
      Dim pDropped As String = "" 
      If pObjectName.EndsWith("es", StringComparison.OrdinalIgnoreCase) Then 
        pObjectName = pObjectName.Substring(0, pObjectName.Length - 2) 
        pDropped = "es" 
      ElseIf pObjectName.EndsWith("s", StringComparison.OrdinalIgnoreCase) Then 
        pObjectName = pObjectName.Substring(0, pObjectName.Length - 1) 
        pDropped = "s" 
      End If 
      If pObjectName.StartsWith("c_", StringComparison.OrdinalIgnoreCase) = True Then 
        pObjectName = "cs" & pObjectName.Substring(2) 
      ElseIf pObjectName.StartsWith("vw", StringComparison.OrdinalIgnoreCase) = True Then 
        pObjectName = "cls" & pObjectName.Substring(2) 
      Else 
        pObjectName = "cls" & pObjectName 
      End If 
      'pObjectName = pAssembly.GetName.Name & "." & pObjectName 
      pObjectName = pRootNameSpace & "." & pObjectName 
      pClassType = pAssembly.GetType(pObjectName) 
      If pClassType Is Nothing AndAlso Not String.IsNullOrEmpty(pDropped) Then 
        pObjectName &= pDropped 
        pClassType = pAssembly.GetType(pObjectName) 
      End If 
      Try 
        pClass = CType(Activator.CreateInstance(pClassType), Object) 
      Catch ex As Exception 
        Return pFault.LogException(ex, "Fell doing " & pTable.Name, "TRGT-160526-1737", vRequester) 
      End Try 
 
      Dim pNumProperties As Integer = pClass.GetType().GetProperties().Count 
      Dim pCntr As Integer = -1 
      While pCntr < pNumProperties - 1 
        pCntr += 1 
        Dim pProperty As System.Reflection.PropertyInfo = pClass.GetType().GetProperties()(pCntr) 
        If pProperty.CanRead Then 
          Dim pPropertyName As String = pProperty.Name 
          'remove extraneous field-names that will never be translated 
          If pPropertyName = "ccStatus" Then Continue While 
          If pPropertyName = "Tag" Then Continue While 
          If pPropertyName = "WithParents" Then Continue While 
          If pPropertyName = "HasParents" Then Continue While 
          If pPropertyName = "HasLocalizedFields" Then Continue While 
          If pPropertyName = "CanHave0AsPrimaryKey" Then Continue While 
          If pPropertyName = "AddedBy" Then Continue While 
          If pPropertyName = "AddedOn" Then Continue While 
          If pPropertyName = "ChangedBy" Then Continue While 
          If pPropertyName = "ChangedOn" Then Continue While 
          If pPropertyName = "IsCleanForXML" Then Continue While 
          If pPropertyName = "IsEmpty" Then Continue While 
          If pPropertyName = "PrimaryKey" Then Continue While 
          If pPropertyName = "UpdatingLoginID" Then Continue While 
          If pPropertyName = "IsLocalized" Then Continue While 
          If pPropertyName = "DateAdded" Then Continue While 
          If pPropertyName = "DefaultDesignation" Then Continue While 
 
          'Check for Lookup fields 
          If pPropertyName.EndsWith("Code", StringComparison.OrdinalIgnoreCase) Then 
            Try 
              Dim pPropertyNext As System.Reflection.PropertyInfo = pClass.GetType().GetProperties()(pCntr + 1) 
              If pPropertyNext.Name.EndsWith("Text", StringComparison.OrdinalIgnoreCase) Then 
                'Assume lookup 
                pPropertyName = pPropertyName.Substring(0, pPropertyName.Length - 4) 
                'now check 
                If pPropertyName & "Text" = pPropertyNext.Name Then 
                  pCntr += 1 '(for next time) 
                Else 
                  'Restore the Code 
                  pPropertyName = pPropertyName & "Code" 
                End If 
              End If 
            Catch ex As Exception 
            End Try 
          End If 
 
          'Check for Enum fields 
          If pPropertyName.EndsWith("Text", StringComparison.OrdinalIgnoreCase) Then 
            Try 
              Dim pPropertyPrev As System.Reflection.PropertyInfo = pClass.GetType().GetProperties()(pCntr - 1) 
              If pPropertyPrev.Name & "Text" = pPropertyName Then 
                'Assume was enum 
                Continue While 'Was already done 
              End If 
            Catch ex As Exception 
            End Try 
          End If 
 
          'Check for Foreign key fields 
          Dim pPlus2 As String = pClass.GetType().GetProperties()(pCntr + 2).Name 
          Dim pPlus1 As String = pClass.GetType().GetProperties()(pCntr + 1).Name 
          If pPlus2.EndsWith("Text", StringComparison.OrdinalIgnoreCase) Then 
            If pPlus1 = pPlus2.Substring(0, pPlus2.Length - 4) Then 
              If pPropertyName.StartsWith(pPlus1) Then 
                Continue While 'save the next 1 
              End If 
            End If 
          End If 
 
          If pPropertyName.EndsWith("Localized", StringComparison.OrdinalIgnoreCase) Then 
            pPropertyName = pPropertyName.Substring(0, pPropertyName.Length - 9) 
            'Check to see if it exists in the ObjectToTranslateCol   
            pObjectToTranslate = pObjectToTranslateCol.FindByObjectTypeAndObjectAndItem(clsEnums.enmObjectType.TableData, pTableName, pPropertyName) 
            If pObjectToTranslate.ID = 0 Then 
              'not found, so add it  
              pObjectToTranslate = New csObjectToTranslate 
              With pObjectToTranslate 
                .ObjectType = clsEnums.enmObjectType.TableData 
                .Object = pTableName 
                .Item = pPropertyName 
                pFault = .Update(vRequester, True) 
              End With 
              If pFault.isOK = False Then Return pFault 
            End If 
          Else 
            'Check to see if it exists in the ObjectToTranslateCol   
            pObjectToTranslate = pObjectToTranslateCol.FindByObjectTypeAndObjectAndItem(clsEnums.enmObjectType.TableFieldName, pTableName, pPropertyName) 
            If pObjectToTranslate.ID = 0 Then 
              'not found, so add it  
              pObjectToTranslate = New csObjectToTranslate 
              With pObjectToTranslate 
                .ObjectType = clsEnums.enmObjectType.TableFieldName 
                .Object = pTableName 
                .Item = pPropertyName 
                pFault = .Update(vRequester, True) 
              End With 
              If pFault.isOK = False Then Return pFault 
            End If 
          End If 
          pObjectToTranslate.Tag = "" 
        End If 
      End While 
    Next 
 
    'now delete those that are not used 
    For Each l In pObjectToTranslateCol 
      If l.Tag.Equals("NotUsed", StringComparison.OrdinalIgnoreCase) Then 
        'delete any translations 
        pFault = csObjectTranslationCol.DeleteByObjectToTranslateID(l.ID, vRequester) : If pFault.isOK = False Then Return pFault 
        Dim pToDelete = l.Clone() 
        pFault = l.Delete(vRequester) : If pFault.isOK = False Then Return pFault 
      End If 
    Next 
 
    'Now add the UI Text 
    pObjectToTranslateCol = New csObjectToTranslateCol 
    pFault = pObjectToTranslateCol.FillByObjectTypeAndObject(clsEnums.enmObjectType.UI, "CCText", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
 
    Dim pUIText As New List(Of String) 
 
    pUIText.Add("Delete") 
    pUIText.Add("Cancel") 
    pUIText.Add("Update") 
    pUIText.Add("Add") 
    pUIText.Add("Edit") 
    pUIText.Add("Cease Edit") 
    pUIText.Add("Edit Mode") 
    pUIText.Add("Spreadsheet") 
    pUIText.Add("Report") 
    pUIText.Add("Columns") 
    pUIText.Add("Showing {0} rows") 
    pUIText.Add("Showing 1st 99 rows") 
    pUIText.Add("Login") 
    pUIText.Add("OK") 
    pUIText.Add("User Name") 
    pUIText.Add("Password") 
    pUIText.Add("Copyright") 
    pUIText.Add("Version") 
    pUIText.Add("Tasks") 
    pUIText.Add("Entities") 
    pUIText.Add("User Management") 
    pUIText.Add("Control") 
    pUIText.Add("System") 
    pUIText.Add("Audit") 
    pUIText.Add("Refresh") 
    pUIText.Add("Show List") 
    pUIText.Add("List") 
    pUIText.Add("Details") 
    pUIText.Add("Fault No:") 
    pUIText.Add("Record No:") 
 
    For Each pString In pUIText 
      pObjectToTranslate = pObjectToTranslateCol.FindByObjectTypeAndObjectAndItem(clsEnums.enmObjectType.UI, "CCTExt", pString) 
      If pObjectToTranslate.ID = 0 Then 
        'not found, so add it  
        pObjectToTranslate = New csObjectToTranslate 
        With pObjectToTranslate 
          .ObjectType = clsEnums.enmObjectType.UI 
          .Object = "CCText" 
          .Item = pString 
          pFault = .Update(vRequester, False) 
        End With 
        If pFault.isOK = False Then Return pFault 
      End If 
    Next 
    If Not pFault.isOK Then Return pFault 
 
    Return pFault 
  End Function 
 
  Public Shared Function TranslationRemoveUnusedPossibilitiesFromObjectToTranslate(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_HandleObjectToTranslate, "ccDatabaseMaintenance_TranslationRemoveUnusedPossibilitiesFromObjectToTranslate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'load all the automatically added items 
    Dim pObjectToTranslateCol As New csObjectToTranslateCol  
    pFault = pObjectToTranslateCol.FillByObjectTypeAndObject(clsEnums.enmObjectType.System, "Text", vRequester, vAppend:=True) : If pFault.isOK = False Then Return pFault 
    pFault = pObjectToTranslateCol.FillByObjectType(clsEnums.enmObjectType.TableData, vRequester, vAppend:=True) : If pFault.isOK = False Then Return pFault 
    pFault = pObjectToTranslateCol.FillByObjectType(clsEnums.enmObjectType.TableFieldName, vRequester, vAppend:=True) : If pFault.isOK = False Then Return pFault 
    pFault = pObjectToTranslateCol.FillByObjectTypeAndObject(clsEnums.enmObjectType.UI, "CCText", vRequester, vAppend:=True) : If pFault.isOK = False Then Return pFault 
 
    'Now scan the tables in each column 
    For Each pObjectToTranslate As csObjectToTranslate In pObjectToTranslateCol 
      'Check to see if there are any in the Translation Fields 
      Dim pObjectTranslationCol As New csObjectTranslationCol() 
      pFault = pObjectTranslationCol.FillByObjectToTranslateID(pObjectToTranslate.ID, vRequester) 
      If pFault.isOK = False Then Return pFault 
 
      If pObjectTranslationCol.Count = 0 Then 
        'Remove it 
        pFault = pObjectToTranslate.Delete(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
    Next 
    If Not pFault.isOK Then Return pFault 
 
    Return pFault 
  End Function 
 
  Public Shared Function FillAllTranslationPossibilities(ByVal vObjectToTranslateID As Long, ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester, ByRef rObjectTranslations As csObjectTranslationCol) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_HandleObjectToTranslate, "ccDatabaseMaintenance_FillAllTranslationPossibilities", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    rObjectTranslations = Nothing 
 
    'Load all the 'c_ObjectToTranslates' 
    Dim pObjectToTranslates As New csObjectToTranslateCol 
    If vObjectToTranslateID > 0 Then 
      Dim pObjectToTranslate As New csObjectToTranslate 
      pFault = pObjectToTranslate.GetByID(vObjectToTranslateID, vRequester, False) 
      If pFault.isOK = False Then Return pFault 
      pObjectToTranslates.Add(pObjectToTranslate) 
    Else 
      pFault = pObjectToTranslates.Fill(vRequester) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    'Load the combo list 
    Dim pObjectToTranslateTexts As New clsComboList 
    pFault = pObjectToTranslateTexts.Fill(clsEnums.enmComboListType.c_ObjectToTranslateDefaultByID, vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Load all the ObjectTranslations 
    Dim pObjectTranslations As New csObjectTranslationCol() 
    pFault = pObjectTranslations.Fill(vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Get the Language Lists 
    Dim pLanguages As New csLanguageCol 
    pFault = pLanguages.Fill(vRequester) 
    If pFault.isOK = False Then Return pFault 
    Dim pLanguagesToTranslate As New List(Of clsEnums.enmLanguage) 
    For Each p In pLanguages 
      If vLanguage = clsEnums.enmLanguage.UD Then 
        pLanguagesToTranslate.Add(clsEnums.TranslateEnmLanguage(p.Code)) 
      Else 
        If vLanguage.FastToString() = p.Code Then 
          pLanguagesToTranslate.Add(clsEnums.TranslateEnmLanguage(p.Code)) 
        End If 
      End If 
    Next 
    pLanguages = Nothing 
 
 
    'There are 5 types 
    Dim ptmpObjectToTranslates As csObjectToTranslateCol 
    Dim pObjectTranslationToAdd As csObjectTranslation 
 
    'now start 
    rObjectTranslations = New csObjectTranslationCol 
 
    'Get the System & UI 
    ptmpObjectToTranslates = pObjectToTranslates.CloneByObjectType(clsEnums.enmObjectType.System) 
    ptmpObjectToTranslates.AddRange(pObjectToTranslates.CloneByObjectType(clsEnums.enmObjectType.UI)) 
    For Each pObjectToTranslate In ptmpObjectToTranslates 
      'check for array 
      Dim pObjectInstances = pObjectTranslations.CloneByObjectToTranslateID(pObjectToTranslate.ID) 
      If pObjectInstances.Count = 0 Then 
        Dim pFakeInstance As New csObjectTranslation 
        With pFakeInstance 
          .ObjectToTranslateID = pObjectToTranslate.ID 
          .DefaultText = pObjectToTranslate.Item 
          .Instance = 0 
          .Language = clsEnums.enmLanguage.en 
        End With 
        pObjectInstances.Add(pFakeInstance) 
      End If 
      pObjectInstances.SortByInstance() 
      Dim pInstanceNoID As Long = -1 
      For Each pInstanceUsed In pObjectInstances 
        If pInstanceUsed.Instance <> pInstanceNoID Then 
          pInstanceNoID = pInstanceUsed.Instance 
 
          Dim pObjectToTranslateText = pObjectToTranslateTexts.FindByKey(pObjectToTranslate.ID).Text 
 
          'Find the Default language  
          Dim pDefault As String = pObjectToTranslate.Item 
          pDefault = pObjectTranslations.FindByObjectToTranslateIDAndInstanceAndLanguage(pObjectToTranslate.ID, pInstanceNoID, clsEnums.enmLanguage.en).Text 
          If pDefault = "" Then 
            pDefault = pObjectToTranslate.Item 
          End If 
          For Each pL In pLanguagesToTranslate 
            'See if it already exists  
            pObjectTranslationToAdd = New csObjectTranslation 
            pObjectTranslationToAdd = pObjectTranslations.FindByObjectToTranslateIDAndInstanceAndLanguage(pObjectToTranslate.ID, pInstanceNoID, pL) 
            If pObjectTranslationToAdd.ID = 0 Then 
              With pObjectTranslationToAdd 
                .ObjectToTranslateID = pObjectToTranslate.ID 
                .Instance = pInstanceNoID 
                .Language = pL 
                .Text = "" 
              End With 
            End If 
            pObjectTranslationToAdd.ObjectToTranslateText = pObjectToTranslateText 
            pObjectTranslationToAdd.DefaultText = pDefault 
            pObjectTranslationToAdd.InstanceUniqueText = pObjectToTranslateText & ":" 
            rObjectTranslations.Add(pObjectTranslationToAdd) 
          Next 
        End If 
      Next 
    Next 
 
    'Get the TableData 
    Dim pExecutingAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly() 
    'Get the assembly    
    Dim pAssembly As System.Reflection.Assembly = Nothing 
    pAssembly = pExecutingAssembly 
 
    'Get the Root NameSpace 
    Dim pRootNameSpace As String = "" 
    'Try 
    '  'this does not seem to work in .NET Standard - returns nothing 
    '  pRootNameSpace = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames(0) 
    '  If pRootNameSpace.EndsWith(".Resources.resources", StringComparison.OrdinalIgnoreCase) Then 
    '    pRootNameSpace = pRootNameSpace.Substring(0, pRootNameSpace.Length - 20) 
    '  End If 
    'Catch ex As Exception 
    '  pFault = New clsFault 
    '  Return pFault.LogException(ex, "'pRootNameSpace = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames(0)' didn't work", "TRGT-170301-1055", vRequester) 
    'End Try 
    pRootNameSpace = "TargCCOrders.DataController" 
 
    Dim pInstance As Object = Nothing 
 
    ptmpObjectToTranslates = pObjectToTranslates.CloneByObjectType(clsEnums.enmObjectType.TableData) 
    ptmpObjectToTranslates.SortByObject() 
    Dim pLastTableName As String = "" 
    For Each pObjectToTranslate In ptmpObjectToTranslates 
      Dim pObjectToTranslateID As Long = pObjectToTranslate.ID 
      Dim pObjectToTranslateText = pObjectToTranslateTexts.FindByKey(pObjectToTranslate.ID).Text 
 
      'Find the field name 
      Dim pTableName As String = pObjectToTranslate.Object 
      Dim pFieldName As String = pObjectToTranslate.Item 
 
      If pTableName <> pLastTableName Then 
        Dim pClassType As Type 
        Dim pObjectName As String = pTableName 
        If pObjectName.StartsWith("c_", StringComparison.OrdinalIgnoreCase) = True Then 
          pObjectName = "cs" & pObjectName.Substring(2) 
        ElseIf pObjectName.StartsWith("vw", StringComparison.OrdinalIgnoreCase) = True Then 
          pObjectName = "cls" & pObjectName.Substring(2) 
        Else 
          pObjectName = "cls" & pObjectName 
        End If 
        'pObjectName = pAssembly.GetName.Name & "." & pObjectName & "Col" 
        pObjectName = pRootNameSpace & "." & pObjectName & "Col" 
        pClassType = pAssembly.GetType(pObjectName) 
        pInstance = Activator.CreateInstance(pClassType) 
 
        'Dim pLoad As Reflection.MethodInfo = pClassType.GetMethod("Fill", Reflction.BindingFlags.NonPublic Or Reflction.BindingFlags.Instance Or Reflction.BindingFlags.DeclaredOnly) 
        Dim pLoad As Reflection.MethodInfo = pClassType.GetMethod("Fill") 
        Dim pParam() As Object 
        ReDim pParam(2) 
        pParam(0) = vRequester 
        pParam(1) = 0 
        pParam(2) = clsEnums.enmFillDirection.ASC 
        'Load the control  
        pFault = CType(pLoad.Invoke(pInstance, pParam), clsFault) 
        If pFault.isOK = False Then Return pFault 
 
        pLastTableName = pTableName 
      End If 
 
      Dim pCount As Integer = ccHelper.ToInteger(pInstance.GetType.GetProperty("Count").GetValue(pInstance, Nothing)) 
 
      For i As Integer = 0 To pCount - 1 
        Dim pRow As Object = pInstance.GetType.GetProperty("Item").GetValue(pInstance, {i}) 
        Dim pPropertyName As String 
        Dim pID As Long 
        Dim pDefaultText As String 
 
        pPropertyName = pObjectToTranslate.Item 
        pID = ccHelper.ToLong(pRow.GetType.GetProperty("ID").GetValue(pRow, Nothing)) 
        pDefaultText = CStr(pRow.GetType.GetProperty(pPropertyName).GetValue(pRow, Nothing)) 
 
        'If there is no default text, the no need to translate, unless it's c_Enumeration or c_Lookup 
        If pDefaultText.Trim = "" Then 
          If pTableName = "c_Lookup" Then 
            pDefaultText = CStr(pRow.GetType.GetProperty("Code").GetValue(pRow, Nothing)) 
          ElseIf pTableName = "c_Enumeration" Then 
            pDefaultText = CStr(pRow.GetType.GetProperty("EnumValue").GetValue(pRow, Nothing)) 
          Else 
            Continue For 
          End If 
        End If 
 
        'Check the texts  
        For Each pL In pLanguagesToTranslate 
          'See if it already exists 
          pObjectTranslationToAdd = New csObjectTranslation 
          pObjectTranslationToAdd = pObjectTranslations.FindByObjectToTranslateIDAndInstanceAndLanguage(pObjectToTranslate.ID, pID, pL) 
          If pObjectTranslationToAdd.ID = 0 Then 
            With pObjectTranslationToAdd 
              .ObjectToTranslateID = pObjectToTranslateID 
              .Instance = pID 
              .Language = pL 
              .Text = "" 
            End With 
          End If 
          pObjectTranslationToAdd.InstanceUniqueText = pObjectToTranslateText & ":" & DirectCast(pRow, ITargCCEntity).DefaultDesignation 
          pObjectTranslationToAdd.ObjectToTranslateText = pObjectToTranslateText 
          pObjectTranslationToAdd.DefaultText = pDefaultText 
          rObjectTranslations.Add(pObjectTranslationToAdd) 
        Next 
      Next 
    Next 
 
    'Get the TableFieldName 
    ptmpObjectToTranslates = pObjectToTranslates.CloneByObjectType(clsEnums.enmObjectType.TableFieldName) 
    ptmpObjectToTranslates.SortByObject() 
    For Each pObjectToTranslate In ptmpObjectToTranslates 
      Dim pObjectToTranslateID As Long = pObjectToTranslate.ID 
      Dim pObjectToTranslateText = pObjectToTranslateTexts.FindByKey(pObjectToTranslate.ID).Text 
 
      'Find the field name 
      Dim pTableName As String = pObjectToTranslate.Object 
      Dim pFieldName As String = pObjectToTranslate.Item 
 
      'Check the Enums 
      For Each pL In pLanguagesToTranslate 
        'See if it already exists 
        pObjectTranslationToAdd = New csObjectTranslation 
        pObjectTranslationToAdd = pObjectTranslations.FindByObjectToTranslateIDAndInstanceAndLanguage(pObjectToTranslate.ID, 0, pL) 
        If pObjectTranslationToAdd.ID = 0 Then 
          With pObjectTranslationToAdd 
            .ObjectToTranslateID = pObjectToTranslateID 
            .Instance = 0 
            .Language = pL 
            .Text = "" 
          End With 
        End If 
        pObjectTranslationToAdd.InstanceUniqueText = pObjectToTranslateText & ":" 
        pObjectTranslationToAdd.ObjectToTranslateText = pObjectToTranslateText 
        pObjectTranslationToAdd.DefaultText = pFieldName 'pDefaultText 
        rObjectTranslations.Add(pObjectTranslationToAdd) 
      Next 
    Next 
    If Not pFault.isOK Then Return pFault 
 
    Return pFault 
  End Function 
 
  Public Shared Function WriteDatabaseToBinary(ByVal vDatabaseInOneFile As Boolean, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_WriteDatabaseToXML, "ccDatabaseMaintenance_WriteDatabaseToXML", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      Dim pDatabase As New clsDatabase 
      pFault = pDatabase.SaveSQLDataToBinary(vDatabaseInOneFile, vRequester) 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-130205-1755", vRequester) 
    End Try 
    If Not pFault.isOK Then Return pFault 
 
    Return pFault 
  End Function 
 
  Friend Shared Function MoveAudits(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As clsFault 
 
    Dim pStartTime As Date = DateTime.Now 
 
    'If we don't want a field to be audited, the set the field's extended property key ccDNA to 1 
 
    Dim pTimeStarted As Date = DateTime.Now 
 
    'Get a static list of audits to ignore 
    Static pAuditsToIgnore As clsComboList = Nothing 
    If pAuditsToIgnore Is Nothing Then 
      pFault = FillAuditsToIgnore(pAuditsToIgnore, vRequester) 
      If Not pFault.isOK Then 
        pAuditsToIgnore = Nothing 
        Return pFault 
      End If 
    End If 
 
    'get last Audit moved  
    Dim pAuditIndexedCol As New csAuditIndexedCol 
    pFault = pAuditIndexedCol.Fill(vRequester, 1, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then Return pFault 
    Dim pMaxIDMoved As Long 
    If pAuditIndexedCol.Count = 0 Then 
      pMaxIDMoved = 0 
    Else 
      pMaxIDMoved = pAuditIndexedCol(0).OriginalID 
    End If 
 
    'get last Audit in SystemAudit  
    Dim pAuditNotMovedCol As New csSystemAuditCol 
    pFault = pAuditNotMovedCol.Fill(vRequester, 1, clsEnums.enmFillDirection.DESC) 
    If pFault.isOK = False Then Return pFault 
    Dim pMaxIDNotMoved As Long 
    If pAuditNotMovedCol.Count = 0 Then 
      'nothing to move   
      pFault = ccTaskManager.MarkJobAsComplete(vJobID, clsEnums.enmJobStatus.Success, pStartTime, DateTime.Now, "Nothing to move A", 0, Nothing, Nothing, vRequester) 
      If Not pFault.isOK Then pFault.SetOK(vRequester) 'continue on 
      Return pFault 
    Else 
      pMaxIDNotMoved = pAuditNotMovedCol(0).ID 
    End If 
 
    If pMaxIDNotMoved = pMaxIDMoved Then 
      pFault = ccTaskManager.MarkJobAsComplete(vJobID, clsEnums.enmJobStatus.Success, pStartTime, DateTime.Now, "Nothing to move 8", 0, Nothing, Nothing, vRequester) 
      If Not pFault.isOK Then pFault.SetOK(vRequester) 'continue on 
      Return pFault 
    End If 
 
    'Now get then  
    Dim pAuditToMoveCol As New csSystemAuditCol 
    pFault = pAuditToMoveCol.FillByBoundedID(pMaxIDMoved + 1, pMaxIDNotMoved, vRequester, 1000, clsEnums.enmFillDirection.ASC) 
    If pFault.isOK = False Then Return pFault 
    If pAuditToMoveCol.Count = 0 Then 
      Return pFault.LogFreeTextFault(301, String.Format("MoveAudits found no audits to move, but should have; {0:#,##0.00} seconds elapsed", DateTime.Now.Subtract(pTimeStarted).Ticks / 10000000), pFunctionParameters, "TRGT-090202-1637", vRequester) 
    End If 
 
    Dim pRowCntr As Integer = 0 
    Dim pChangeCntr As Integer = 0 
    For Each pSystemAudit As csSystemAudit In pAuditToMoveCol 
      Dim pTableName As String = pSystemAudit.TableName 
      Dim pChanges As String = pSystemAudit.Changes 
      If pChanges.StartsWith("<clsChangeCol ", StringComparison.OrdinalIgnoreCase) = True Then 
        pChanges = pChanges.Replace("<clsChangeCol xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">", "").Replace("</clsChangeCol>", "") ' for Auditor that was in .NET 2 
      End If 
      pChanges = "<?xml version=""1.0"" encoding=""utf-16""?>" & Environment.NewLine & "<ArrayOfClsChange xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & pChanges & Environment.NewLine & "</ArrayOfClsChange>" 
 
      Dim pChangeCol As New clsChangeCol 
      Try 
        pChangeCol.FillFromXML(pChanges) 
      Catch ex As Exception 
        Return pFault.LogException(ex, $"{pTableName}{Environment.NewLine}{pChanges}", "TRGT-140505-1449", vRequester) 
      End Try  
  
      If pChangeCol.Count = 0 Then 
        Return pFault.LogFreeTextFault("I could not extract the changes", $"{pTableName}{Environment.NewLine}{pChanges}", "TRGT-140516-1228", vRequester) 
      End If 
 
      'Now move to history, using a transaction 
      Dim pTranOptions As New System.Transactions.TransactionOptions() 
      pTranOptions.Timeout = System.Transactions.TransactionManager.MaximumTimeout 
      pTranOptions.IsolationLevel = Transactions.IsolationLevel.ReadCommitted 
 
      Using pTran As New System.Transactions.TransactionScope(Transactions.TransactionScopeOption.Required, pTranOptions) 
        Dim pChangeMade As Boolean = False 
        For Each pChange In pChangeCol 
          Dim pColumnName As String = pChange.FN 
          Dim pSearchFor As String = pTableName & "#" & pColumnName 
          If pAuditsToIgnore.FindByKey(pSearchFor).Text <> "" Then Continue For 
          Dim pSystemAuditIndexed As New csAuditIndexed 
          'now get the transaction  
          With pSystemAuditIndexed 
            .FieldName = pChange.FN 
            .OriginalID = pSystemAudit.ID 
            .NewValue = pChange.NV.Replace(" +++", " ---") 
            .OccurredAt = pSystemAudit.OccurredAt 
            .OldValue = pChange.OV.Replace(" +++", " ---") 
            .Operation = pSystemAudit.Operation 
            .SqlAppName = pSystemAudit.SqlAppName 
            .SqlCurrentUser = pSystemAudit.SqlCurrentUser 
            .SqlHostName = pSystemAudit.SqlHostName 
            .SqlSystemUser = pSystemAudit.SqlSystemUser 
            .RowID = pSystemAudit.RowId 
            .TableName = pSystemAudit.TableName 
            .ChangedByUser = pSystemAudit.ChangedByUser 
            .ActiveLoginID = pSystemAudit.ActiveLoginID 
          End With 
          pFault = pSystemAuditIndexed.Update(vRequester, False) : If pFault.isOK = False Then Return pFault 
          pChangeCntr += 1 
          pChangeMade = True 
        Next 
        If pChangeMade = True Then pRowCntr += 1 
        
        'Delete it here 
        pFault = pSystemAudit.Delete(vRequester) : If Not pFault.isOK Then Return pFault 
        pTran.Complete() 
      End Using 
    Next 
 
    Dim pResult As String = String.Format("Moved {1} fields from {0} updated rows", pRowCntr, pChangeCntr) 
    If pRowCntr + pChangeCntr = 0 Then 
      pResult = "" 
    End If 
 
    pFault = ccTaskManager.MarkJobAsComplete(vJobID, clsEnums.enmJobStatus.Success, pStartTime, DateTime.Now, pResult, pChangeCntr, Nothing, Nothing, vRequester) 
    If Not pFault.isOK Then pFault.SetOK(vRequester) 'continue on 
 
    Return pFault 
  End Function 
  'For MoveAudits 
  Public Class clsChange 
 
    Private _FN As String 
    Private _OV As String 
    Private _NV As String 
 
    ''' <summary> 
    ''' FieldName 
    ''' </summary> 
    ''' <value></value> 
    ''' <returns></returns> 
    ''' <remarks></remarks> 
    Public Property [FN]() As String 
      Get 
        Return Me._FN 
      End Get 
      Set(ByVal value As String) 
        Me._FN = value 
      End Set 
    End Property 
    ''' <summary> 
    ''' Old Value 
    ''' </summary> 
    ''' <value></value> 
    ''' <returns></returns> 
    ''' <remarks></remarks> 
    Public Property [OV]() As String 
      Get 
        Return Me._OV 
      End Get 
      Set(ByVal value As String) 
        Me._OV = value 
      End Set 
    End Property 
    ''' <summary> 
    ''' New Value 
    ''' </summary> 
    ''' <value></value> 
    ''' <returns></returns> 
    ''' <remarks></remarks> 
    Public Property [NV]() As String 
      Get 
        Return Me._NV 
      End Get 
      Set(ByVal value As String) 
        Me._NV = value 
      End Set 
    End Property 
 
    Public Function ToXML() As String 
      Dim pFunctionParameters As String = "" 
 
      Dim pXML As String = "" 
      Try 
        Dim pType As Type = Me.GetType 
        pFunctionParameters = pType.Name 
        Dim pSerializer As Xml.Serialization.XmlSerializer 
        pSerializer = New Xml.Serialization.XmlSerializer(pType) 
        Dim MyStringBuilder As New Text.StringBuilder 
        Dim pWriter As New IO.StringWriter(MyStringBuilder) 
        pSerializer.Serialize(pWriter, Me) 
        pWriter.Close() 
 
        pXML = MyStringBuilder.ToString() 
      Catch ex As Exception 
        Throw New Exception("At " & (New StackFrame).GetMethod().Name & ":" & Environment.NewLine & ex.Message & Environment.NewLine & ex.InnerException?.ToString() & Environment.NewLine & ex.StackTrace) 
      End Try 
 
      Return pXML 
    End Function 
 
    Public Sub New() 
      CreateEmpty() 
    End Sub 
 
    Private Sub CreateEmpty() 
      _FN = "" 
      _OV = "" 
      _NV = "" 
    End Sub 
  End Class 
  Public Class clsChangeCol 
    Inherits Generic.List(Of clsChange) 
 
    'ToXML  
    Public Function ToXML() As String 
      Dim pFunctionParameters As String = "" 
 
      Dim pXML As String = "" 
      Try 
        Dim pType As Type = Me.GetType 
        pFunctionParameters = pType.Name 
        Dim pSerializer As Xml.Serialization.XmlSerializer 
        pSerializer = New Xml.Serialization.XmlSerializer(pType) 
        Dim MyStringBuilder As New Text.StringBuilder 
        Dim pWriter As New IO.StringWriter(MyStringBuilder) 
        pSerializer.Serialize(pWriter, Me) 
        pWriter.Close() 
 
        pXML = MyStringBuilder.ToString() 
      Catch ex As Exception 
        Throw New Exception("At " & (New StackFrame).GetMethod().Name & ":" & Environment.NewLine & ex.Message & Environment.NewLine & ex.InnerException?.ToString() & Environment.NewLine & ex.StackTrace) 
      End Try 
 
      Return pXML 
    End Function 
 
    'From XML 
    Public Sub FillFromXML(ByVal vXML As String) 
 
      Me.Clear() 
 
      vXML = ccHelper.RemoveIllegalXMLChars(vXML) 
 
      Dim pType As Type = Me.GetType 
      Try 
        Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
        Dim pStreamReader As New IO.StringReader(vXML) 
        Dim pChanges As clsChangeCol = CType(pXmlSerializer.Deserialize(pStreamReader), clsChangeCol) 
 
        For Each pChange In pChanges 
          Me.Add(pChange) 
        Next 
      Catch ex As Exception 
        Tools.LogToTextFile.WriteException($"vXML: {vXML}", ex, "ReadFailure") 
        Throw New Exception("At " & (New StackFrame).GetMethod().Name & " (Check ReadFailure in Logs):" & Environment.NewLine & ex.Message & Environment.NewLine & ex.InnerException?.ToString() & Environment.NewLine & ex.StackTrace) 
      End Try 
 
    End Sub 
 
    Public Sub New() 
      MyBase.New() 
    End Sub 
 
  End Class 
  Private Shared Function FillAuditsToIgnore(ByRef rColumns As clsComboList, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    rColumns = New clsComboList() 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-150216-2210", vRequester) 
    Else 
        Dim pCommandText As String = "c__FillExtendedProperties" 
      Dim pParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "Table" 
        pParameters.Add("Table", ccDAL.enmSQLDataType.VarChar, 50).Value = "%" 
        pLastReadVariableName = "Key" 
        pParameters.Add("Key", ccDAL.enmSQLDataType.VarChar, 50).Value = "ccDNA" 
        pLastReadVariableName = "" 
   
        'Execute query   
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back   
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            Dim pTable As String = "" 
            Dim pColumn As String = "" 
            Dim pValue As String = "" 
   
            Dim pComboListMember As clsComboListMember 
            While pTargCCReader.Read() 
              pComboListMember = New clsComboListMember 
              Try 
                pTable = pTargCCReader.GetString(0) 
                pColumn = pTargCCReader.GetString(1) 
                'pObj = myReader(2) : If Not IsDBNull(pObj) Then pKey = CType(pObj, String) 'doing this only as a sample for future use  
                pValue = pTargCCReader.GetString(3) 
                pComboListMember.KeyString = pTable & "#" & pColumn 
                pComboListMember.Text = pValue 
              Catch ex As Exception 
                Return pFault.LogException(ex, pFunctionParameters, "TRGT-140823-1941", vRequester) 
              End Try 
   
              If pComboListMember.Text = "1" Then 
                rColumns.Add(pComboListMember) 
              End If 
            End While 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
 
    Return pFault 
  End Function 
 
  Friend Shared Function DeleteOldLogs(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault  
    Dim pFunctionParameters As String = ""  
    Dim pFault As clsFault 
 
    Dim pStartTime As Date = DateTime.Now 
 
    Dim pResult As String = "" 'this is the text result sent on Job completion  
 
    'Get the Root NameSpace 
    Dim pRootNameSpace As String = "" 
    'Try 
    '  'this does not seem to work in .NET Standard - returns nothing 
    '  pRootNameSpace = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames(0) 
    '  If pRootNameSpace.EndsWith(".Resources.resources", StringComparison.OrdinalIgnoreCase) Then 
    '    pRootNameSpace = pRootNameSpace.Substring(0, pRootNameSpace.Length - 20) 
    '  End If 
    'Catch ex As Exception 
    '  pFault = New clsFault 
    '  Return pFault.LogException(ex, "'pRootNameSpace = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames(0)' didn't work", "TRGT-170301-1055", vRequester) 
    'End Try 
    pRootNameSpace = "TargCCOrders.DataController" 
 
    'Get the amount of days to keep for SystemDefault  
    Dim pSystemDefault As New csSystemDefault 
    pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Maintenance_DaysToKeep, vRequester, True) : If pFault.isOK = False Then Return pFault 
 
    Dim pMaintenanceDaysToKeep As String = pSystemDefault.SettingValue 
 
    Dim pTableList As New Dictionary(Of Integer, String)  
    Dim pMaintenanceDaysToKeepSplit As String() = pMaintenanceDaysToKeep.Trim.Replace(ChrW(10), "").Split(ChrW(13))  
    For Each lInfo As String In pMaintenanceDaysToKeepSplit  
      If String.IsNullOrEmpty(lInfo.Trim) Then Continue For  
      Dim pInfo As String() = lInfo.Split("#"c)  
      If pInfo.Length <> 3 Then Continue For  
      pTableList.Add(ccHelper.ToInteger(pInfo(0)), pInfo(1) & "#" & pInfo(2))  
    Next 
 
    Dim pNumTablesForDaysCompleted As Integer = 0 
    'Scan the tables. Assume the user may have skipped a number..  
    Dim pTableFailed As Boolean = False  
    For i As Integer = 0 To 100  
      If pTableList.ContainsKey(i) = False Then Continue For  
  
      Dim pTableInfo As String = pTableList(i)  
      Dim pTableName As String = pTableInfo.Split("#"c)(0)  
      Dim pNumDays As Integer = ccHelper.ToInteger(pTableInfo.Split("#"c)(1))  
  
      pTableFailed = False  
      Dim pOldestDate As Date = DateTime.Now.AddDays(-pNumDays)  
  
      'Find the oldest date to keep   
      Dim pCol As ITargCCCollection  
  
      'Now get the Collection type  
      Dim pType As Type = Type.GetType(pRootNameSpace & ".cs" & pTableName & "Col")  
      If pType Is Nothing Then  
        pType = Type.GetType(pRootNameSpace & ".cls" & pTableName & "Col")  
      End If  
      If pType Is Nothing Then  
        Return pFault.LogFreeTextFault("Could not create the object for " & pTableName, "", "TRGT-160722-1710", vRequester)  
      End If  
  
      'Create the collection  
      Try  
        pCol = DirectCast(Activator.CreateInstance(pType), ITargCCCollection)  
      Catch ex As Exception  
        Return pFault.LogException(ex, "Failed creating the object for " & pTableName, "TRGT-160722-1709", vRequester)  
      End Try  
  
      Dim pCntr As Integer = 0  
      Do  
        Dim pCounterToKeep As Integer = 0  
        pFault = pCol.Fill(vRequester, 100, clsEnums.enmFillDirection.ASC) : If Not pFault.isOK Then Return pFault  
  
        For Each lEntity As ITargCCEntity In pCol  
          If lEntity.DateAdded.CompareTo(pOldestDate) < 0 Then  
            pFault = CType(lEntity, ITargCCEntityDeletable).Delete(vRequester) 
            If Not pFault.isOK Then 
              pResult &= pTableName & ": " & pCntr & " rows deleted and then Fault. LoggedAlertID=" & pFault.LoggedAlertID & Environment.NewLine 
              pFault.SetOK(vRequester) 'continue on 
              pTableFailed = True 
              Exit Do 
            End If 
            pCntr += 1  
          Else  
            pCounterToKeep += 1  
          End If  
        Next  
  
        If pCounterToKeep = pCol.Count Then Exit Do  
      Loop  
      If pTableFailed = False Then 
        pResult &= pTableName & ": " & pCntr & " rows deleted" & Environment.NewLine 
      End If 
 
      pNumTablesForDaysCompleted += 1 
      If pNumTablesForDaysCompleted >= pTableList.Count Then Exit For 
    Next 
 
    'do it for the number of rows 
    pSystemDefault = New csSystemDefault 
    pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Maintenance_RowsToKeep, vRequester, True) : If pFault.isOK = False Then Return pFault 
 
    Dim pMaintenanceRowsToKeep As String = pSystemDefault.SettingValue 
    Dim pNumTablesForRowsCompleted As Integer = 0 
 
    If String.IsNullOrEmpty(pMaintenanceRowsToKeep.Trim) = False Then 
      pTableList = New Dictionary(Of Integer, String) 
      Dim pMaintenanceRowsToKeepSplit As String() = pMaintenanceRowsToKeep.Trim.Replace(ChrW(10), "").Split(ChrW(13)) 
      For Each lInfo As String In pMaintenanceRowsToKeepSplit 
      If String.IsNullOrEmpty(lInfo.Trim) Then Continue For  
        Dim pInfo As String() = lInfo.Split("#"c)  
        If pInfo.Length <> 3 Then Continue For  
        pTableList.Add(ccHelper.ToInteger(pInfo(0)), pInfo(1) & "#" & pInfo(2))  
      Next 
 
      'Scan the tables. Assume the user may have skipped a number..  
      For i As Integer = 0 To 100 
        If pTableList.ContainsKey(i) = False Then Continue For 
 
        Dim pTableInfo As String = pTableList(i) 
        Dim pTableName As String = pTableInfo.Split("#"c)(0) 
        Dim pNumRows As Integer = ccHelper.ToInteger(pTableInfo.Split("#"c)(1)) 
 
        pTableFailed = False   
        Dim pCol As ITargCCCollection 
 
        'Now get the Collection type  
        Dim pFillBy As String = "" 
        Dim pType As Type = Type.GetType(pRootNameSpace & ".cs" & pTableName & "Col") 
        pFillBy = "c_" & pTableName 
        If pType Is Nothing Then 
          pType = Type.GetType(pRootNameSpace & ".cls" & pTableName & "Col") 
          pFillBy = "cc" & pTableName 
        End If 
        If pType Is Nothing Then 
          Return pFault.LogFreeTextFault("Could not create the object for " & pTableName, "", "TRGT-160722-1710", vRequester) 
        End If 
 
 
        'get combolist of all - see how many there are. then get collection of the all, ascending and delete! 
        Dim pList As New clsComboList 
        pFault = pList.Fill(pFillBy & "DefaultByID", vRequester) : If Not pFault.isOK Then Return pFault 
        Dim pTotalInTable As Integer = pList.Count 
 
        Dim pNumRowsToKill As Integer = pTotalInTable - pNumRows 
 
        If pNumRowsToKill > 0 Then 
          'Create the collection  
          Try 
            pCol = DirectCast(Activator.CreateInstance(pType), ITargCCCollection) 
          Catch ex As Exception 
            Return pFault.LogException(ex, "Failed creating the object for " & pTableName, "TRGT-160722-1709", vRequester) 
          End Try 
 
          Dim pCntr As Integer = 0 
          Do 
            pFault = pCol.Fill(vRequester, 100, clsEnums.enmFillDirection.ASC) : If Not pFault.isOK Then Return pFault 
 
            For Each lEntity As ITargCCEntity In pCol 
              pFault = CType(lEntity, ITargCCEntityDeletable).Delete(vRequester) 
              If Not pFault.isOK Then 
                pResult &= pTableName & ": " & pCntr & " rows deleted and then Fault. LoggedAlertID=" & pFault.LoggedAlertID & Environment.NewLine 
                pFault.SetOK(vRequester) 'continue on 
                pTableFailed = True 
                Exit Do 
              End If 
              pCntr += 1 
              If pCntr = pNumRowsToKill Then Exit Do 
            Next 
          Loop 
          If pTableFailed = False Then 
            pResult &= pTableName & ": " & pCntr & " rows deleted" & Environment.NewLine 
          End If 
        End If 
 
        pNumTablesForRowsCompleted += 1 
        If pNumTablesForRowsCompleted >= pTableList.Count Then Exit For 
      Next 
    End If 
 
    'now do c__CleanLogs 
    pFault = ccHelper.RunStoredProcedure("c__CleanLogs", Nothing, Nothing, vRequester, vCommandTimeoutSec:=120) : If Not pFault.isOK Then Return pFault 
 
    pFault = ccTaskManager.MarkJobAsComplete(vJobID, clsEnums.enmJobStatus.Success, pStartTime, DateTime.Now, pResult, pNumTablesForDaysCompleted + pNumTablesForRowsCompleted, Nothing, Nothing, vRequester) 
    If Not pFault.isOK Then pFault.SetOK(vRequester) 'continue on 
  
    Return pFault  
  End Function  
  
  Public Class clsDatabase 
 
    Public Property BeehiveBuyerTrackingCol As clsBeehiveBuyerTrackingCol 
    Public Property CustomerCol As clsCustomerCol 
    Public Property CustomerDebtCol As clsCustomerDebtCol 
    Public Property DeliveryCol As clsDeliveryCol 
    Public Property OrderHeaderCol As clsOrderHeaderCol 
    Public Property OrderLineCol As clsOrderLineCol 
    Public Property ProductCol As clsProductCol 
    Public Property ProductPriceCol As clsProductPriceCol 
    Public Property ProductPriceHistCol As clsProductPriceHistCol 
    Public Property SupplierOrderCol As clsSupplierOrderCol 
    Public Property ccAlertMessageCol As csAlertMessageCol 
    Public Property ccAuditIndexedCol As csAuditIndexedCol 
    Public Property ccEnumerationCol As csEnumerationCol 
    Public Property ccIndexFragmentationCol As csIndexFragmentationCol 
    Public Property ccJobCol As csJobCol 
    Public Property ccJobAlertRecipientCol As csJobAlertRecipientCol 
    Public Property ccLanguageCol As csLanguageCol 
    Public Property ccLoggedAlertCol As csLoggedAlertCol 
    Public Property ccLoggedJobCol As csLoggedJobCol 
    Public Property ccLoggedLoginCol As csLoggedLoginCol 
    Public Property ccLoggedRequestCol As csLoggedRequestCol 
    Public Property ccLookupCol As csLookupCol 
    Public Property ccMailCol As csMailCol 
    Public Property ccMFACol As csMFACol 
    Public Property ccObjectToTranslateCol As csObjectToTranslateCol 
    Public Property ccObjectTranslationCol As csObjectTranslationCol 
    Public Property ccPermissionCol As csPermissionCol 
    Public Property ccProcessCol As csProcessCol 
    Public Property ccRoleCol As csRoleCol 
    Public Property ccSystemAuditCol As csSystemAuditCol 
    Public Property ccSystemDefaultCol As csSystemDefaultCol 
    Public Property ccTableCol As csTableCol 
    Public Property ccTableSizeCol As csTableSizeCol 
    Public Property ccUserCol As csUserCol 
    Public Property ccUserLoginKeyCol As csUserLoginKeyCol 
    Public Property ccUserPermissionCol As csUserPermissionCol 
    Public Property ccUserStatusCol As csUserStatusCol 
 
    Public Property [ReadOnly] As Boolean 
 
    Private Shared _SQLTableNames As List(Of String) 
    Public Shared ReadOnly Property SQLTableNames As List(Of String) 
      Get 
        If _SQLTableNames Is Nothing Then 
          _SQLTableNames = New List(Of String) 
          _SQLTableNames.Add("BeehiveBuyerTracking") 
          _SQLTableNames.Add("Customer") 
          _SQLTableNames.Add("CustomerDebt") 
          _SQLTableNames.Add("Delivery") 
          _SQLTableNames.Add("OrderHeader") 
          _SQLTableNames.Add("OrderLine") 
          _SQLTableNames.Add("Product") 
          _SQLTableNames.Add("ProductPrice") 
          _SQLTableNames.Add("ProductPriceHist") 
          _SQLTableNames.Add("SupplierOrder") 
          _SQLTableNames.Add("c_AlertMessage") 
          _SQLTableNames.Add("c_AuditIndexed") 
          _SQLTableNames.Add("c_Enumeration") 
          _SQLTableNames.Add("c_Job") 
          _SQLTableNames.Add("c_JobAlertRecipient") 
          _SQLTableNames.Add("c_Language") 
          _SQLTableNames.Add("c_LoggedAlert") 
          _SQLTableNames.Add("c_LoggedJob") 
          _SQLTableNames.Add("c_LoggedLogin") 
          _SQLTableNames.Add("c_LoggedRequest") 
          _SQLTableNames.Add("c_Lookup") 
          _SQLTableNames.Add("c_Mail") 
          _SQLTableNames.Add("c_MFA") 
          _SQLTableNames.Add("c_ObjectToTranslate") 
          _SQLTableNames.Add("c_ObjectTranslation") 
          _SQLTableNames.Add("c_Permission") 
          _SQLTableNames.Add("c_Process") 
          _SQLTableNames.Add("c_Role") 
          _SQLTableNames.Add("c_SystemAudit") 
          _SQLTableNames.Add("c_SystemDefault") 
          _SQLTableNames.Add("c_Table") 
          _SQLTableNames.Add("c_User") 
          _SQLTableNames.Add("c_UserLoginKey") 
          _SQLTableNames.Add("c_UserPermission") 
          _SQLTableNames.Add("c_UserStatus") 
        End If 
        Return _SQLTableNames 
      End Get 
    End Property 
 
    Friend Sub New() 
      _ReadOnly = False 
 
      _BeehiveBuyerTrackingCol = New clsBeehiveBuyerTrackingCol() 
      _CustomerCol = New clsCustomerCol() 
      _CustomerDebtCol = New clsCustomerDebtCol() 
      _DeliveryCol = New clsDeliveryCol() 
      _OrderHeaderCol = New clsOrderHeaderCol() 
      _OrderLineCol = New clsOrderLineCol() 
      _ProductCol = New clsProductCol() 
      _ProductPriceCol = New clsProductPriceCol() 
      _ProductPriceHistCol = New clsProductPriceHistCol() 
      _SupplierOrderCol = New clsSupplierOrderCol() 
      _ccAlertMessageCol = New csAlertMessageCol() 
      _ccAuditIndexedCol = New csAuditIndexedCol() 
      _ccEnumerationCol = New csEnumerationCol() 
      _ccIndexFragmentationCol = New csIndexFragmentationCol() 
      _ccJobCol = New csJobCol() 
      _ccJobAlertRecipientCol = New csJobAlertRecipientCol() 
      _ccLanguageCol = New csLanguageCol() 
      _ccLoggedAlertCol = New csLoggedAlertCol() 
      _ccLoggedJobCol = New csLoggedJobCol() 
      _ccLoggedLoginCol = New csLoggedLoginCol() 
      _ccLoggedRequestCol = New csLoggedRequestCol() 
      _ccLookupCol = New csLookupCol() 
      _ccMailCol = New csMailCol() 
      _ccMFACol = New csMFACol() 
      _ccObjectToTranslateCol = New csObjectToTranslateCol() 
      _ccObjectTranslationCol = New csObjectTranslationCol() 
      _ccPermissionCol = New csPermissionCol() 
      _ccProcessCol = New csProcessCol() 
      _ccRoleCol = New csRoleCol() 
      _ccSystemAuditCol = New csSystemAuditCol() 
      _ccSystemDefaultCol = New csSystemDefaultCol() 
      _ccTableCol = New csTableCol() 
      _ccTableSizeCol = New csTableSizeCol() 
      _ccUserCol = New csUserCol() 
      _ccUserLoginKeyCol = New csUserLoginKeyCol() 
      _ccUserPermissionCol = New csUserPermissionCol() 
      _ccUserStatusCol = New csUserStatusCol() 
 
    End Sub 
 
    Friend Enum enmTables 
      UD 
      [BeehiveBuyerTracking] 
      [Customer] 
      [CustomerDebt] 
      [Delivery] 
      [OrderHeader] 
      [OrderLine] 
      [Product] 
      [ProductPrice] 
      [ProductPriceHist] 
      [SupplierOrder] 
      [ccAlertMessage] 
      [ccAuditIndexed] 
      [ccEnumeration] 
      [ccIndexFragmentation] 
      [ccJob] 
      [ccJobAlertRecipient] 
      [ccLanguage] 
      [ccLoggedAlert] 
      [ccLoggedJob] 
      [ccLoggedLogin] 
      [ccLoggedRequest] 
      [ccLookup] 
      [ccMail] 
      [ccMFA] 
      [ccObjectToTranslate] 
      [ccObjectTranslation] 
      [ccPermission] 
      [ccProcess] 
      [ccRole] 
      [ccSystemAudit] 
      [ccSystemDefault] 
      [ccTable] 
      [ccTableSize] 
      [ccUser] 
      [ccUserLoginKey] 
      [ccUserPermission] 
      [ccUserStatus] 
    End Enum 
 
    Friend Event evtTablesToNotFill(ByVal vList As List(Of enmTables)) 
 
    Private Function LoadMeFromSQLCreatedCollections(ByVal vRequester As clsRequester, ByVal vIncludeSystemLogs As Boolean) As clsFault 
      Dim pFunctionParameters As String = "" 
 
      Dim pFault As New clsFault 
 
      'enable Override of table list 
      Dim pListToNotFill As New List(Of enmTables) 
      RaiseEvent evtTablesToNotFill(pListToNotFill) 
 
      _BeehiveBuyerTrackingCol = New clsBeehiveBuyerTrackingCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.BeehiveBuyerTracking) < 0 Then pFault = _BeehiveBuyerTrackingCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _CustomerCol = New clsCustomerCol() : If pListToNotFill.IndexOf(enmTables.Customer) < 0 Then pFault = _CustomerCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _CustomerDebtCol = New clsCustomerDebtCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.CustomerDebt) < 0 Then pFault = _CustomerDebtCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _DeliveryCol = New clsDeliveryCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.Delivery) < 0 Then pFault = _DeliveryCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _OrderHeaderCol = New clsOrderHeaderCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.OrderHeader) < 0 Then pFault = _OrderHeaderCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _OrderLineCol = New clsOrderLineCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.OrderLine) < 0 Then pFault = _OrderLineCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ProductCol = New clsProductCol() : If pListToNotFill.IndexOf(enmTables.Product) < 0 Then pFault = _ProductCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ProductPriceCol = New clsProductPriceCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.ProductPrice) < 0 Then pFault = _ProductPriceCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ProductPriceHistCol = New clsProductPriceHistCol() : If pListToNotFill.IndexOf(enmTables.ProductPriceHist) < 0 Then pFault = _ProductPriceHistCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _SupplierOrderCol = New clsSupplierOrderCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.SupplierOrder) < 0 Then pFault = _SupplierOrderCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccAlertMessageCol = New csAlertMessageCol() : If pListToNotFill.IndexOf(enmTables.ccAlertMessage) < 0 Then pFault = _ccAlertMessageCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccAuditIndexedCol = New csAuditIndexedCol() 
      _ccEnumerationCol = New csEnumerationCol() : If pListToNotFill.IndexOf(enmTables.ccEnumeration) < 0 Then pFault = _ccEnumerationCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccIndexFragmentationCol = New csIndexFragmentationCol() 
      _ccJobCol = New csJobCol() 
      _ccJobAlertRecipientCol = New csJobAlertRecipientCol(clsEnums.enmLoadParent.TextOnly) 
      _ccLanguageCol = New csLanguageCol() : If pListToNotFill.IndexOf(enmTables.ccLanguage) < 0 Then pFault = _ccLanguageCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccLoggedAlertCol = New csLoggedAlertCol(clsEnums.enmLoadParent.TextOnly) 
      _ccLoggedJobCol = New csLoggedJobCol(clsEnums.enmLoadParent.TextOnly) 
      _ccLoggedLoginCol = New csLoggedLoginCol() 
      _ccLoggedRequestCol = New csLoggedRequestCol(clsEnums.enmLoadParent.TextOnly) 
      _ccLookupCol = New csLookupCol() : If pListToNotFill.IndexOf(enmTables.ccLookup) < 0 Then pFault = _ccLookupCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccMailCol = New csMailCol() : If pListToNotFill.IndexOf(enmTables.ccMail) < 0 Then pFault = _ccMailCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccMFACol = New csMFACol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.ccMFA) < 0 Then pFault = _ccMFACol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccObjectToTranslateCol = New csObjectToTranslateCol() : If pListToNotFill.IndexOf(enmTables.ccObjectToTranslate) < 0 Then pFault = _ccObjectToTranslateCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccObjectTranslationCol = New csObjectTranslationCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.ccObjectTranslation) < 0 Then pFault = _ccObjectTranslationCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccPermissionCol = New csPermissionCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.ccPermission) < 0 Then pFault = _ccPermissionCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccProcessCol = New csProcessCol() : If pListToNotFill.IndexOf(enmTables.ccProcess) < 0 Then pFault = _ccProcessCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccRoleCol = New csRoleCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.ccRole) < 0 Then pFault = _ccRoleCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccSystemAuditCol = New csSystemAuditCol() 
      _ccSystemDefaultCol = New csSystemDefaultCol() : If pListToNotFill.IndexOf(enmTables.ccSystemDefault) < 0 Then pFault = _ccSystemDefaultCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccTableCol = New csTableCol() 
      _ccTableSizeCol = New csTableSizeCol() 
      _ccUserCol = New csUserCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.ccUser) < 0 Then pFault = _ccUserCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccUserLoginKeyCol = New csUserLoginKeyCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.ccUserLoginKey) < 0 Then pFault = _ccUserLoginKeyCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccUserPermissionCol = New csUserPermissionCol(clsEnums.enmLoadParent.TextOnly) : If pListToNotFill.IndexOf(enmTables.ccUserPermission) < 0 Then pFault = _ccUserPermissionCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
      _ccUserStatusCol = New csUserStatusCol(clsEnums.enmLoadParent.TextOnly) 
      Return pFault 
    End Function 
 
    Friend Function LoadTableFromFileSystem(ByVal vCollection As ITargCCCollection) As String 
      Dim pResponse As String 
 
      pResponse = ReadBinary(vCollection) 
 
      Return pResponse 
    End Function 
     
    Friend Function LoadDatabaseFromBinary() As String 
      Dim pFault As New clsFault 
 
      Dim pDatabase As New clsDatabase 
      Dim pResponse As String 
 
      pResponse = ReadDatabaseFromBinary(pDatabase) 
      If pResponse <> "OK" Then Return pResponse 
 
      Return pResponse 
    End Function 
 
    Friend Function SaveData(ByVal vCollection As ITargCCCollection, ByVal vRequester As clsRequester) As clsFault 
      If MyController.DBName = "tables" Then 
        Return WriteBinary(vCollection, vRequester) 
      Else 'If MyController.DBName = "database" Then  
        Return WriteDatabaseToBinary(vRequester) 
      End If 
    End Function 
 
    Friend Function SaveSQLDataToBinary(ByVal vDatabaseInOneFile As Boolean, ByVal vRequester As clsRequester) As clsFault 
      If vDatabaseInOneFile = True Then 
        Return WriteDatabaseToBinary(vRequester) 
      Else 
        Return WriteAllTablesToBinary(vRequester) 
      End If 
    End Function 
 
    Private Function WriteAllTablesToBinary(ByVal vRequester As clsRequester) As clsFault 
      Dim pFunctionParameters As String = "" 
 
      Dim pFault As New clsFault 
 
      pFault = LoadMeFromSQLCreatedCollections(vRequester, False) 
      If pFault.isOK = False Then Return pFault 
 
      For Each p As System.Reflection.PropertyInfo In Me.GetType().GetProperties() 
        If p.GetValue(Me, Nothing).GetType.GetInterface("ITargCCCollection") Is Nothing Then Continue For 
        pFault = WriteBinary(CType(p.GetValue(Me, Nothing), ITargCCCollection), vRequester) 
      Next 
 
      Return pFault 
    End Function 
 
    Private Function WriteDatabaseToBinary(ByVal vRequester As clsRequester) As clsFault 
      Dim pFunctionParameters As String = "" 
 
      Dim pFault As New clsFault 
 
      If _ReadOnly = True Then 
        Return pFault.SetOK() 
      End If 
 
      Dim pFileLocation As String = "" 
      Try 
        pFileLocation = MyController.XMLDataLocation 
      Catch ex As Exception 
        Return pFault.LogFreeTextFault(74, ex.Message, pFunctionParameters, "TRGT-130210-1842", vRequester) 
      End Try 
 
      If MyController.DBType = MyController.enmDBType.SQL Then 
        pFault = LoadMeFromSQLCreatedCollections(vRequester, False) 'if using the cached DB, it erases it  
        If pFault.isOK = False Then Return pFault 
      End If 
 
      Try 
        Dim MyStringBuilder As New Text.StringBuilder 
 
        For Each p As System.Reflection.PropertyInfo In Me.GetType().GetProperties() 
          If p.GetValue(Me, Nothing).GetType.GetInterface("ITargCCCollection") Is Nothing Then Continue For 
          Dim pName As String = p.Name 
          Dim pCollection As ITargCCCollection = CType(p.GetValue(Me, Nothing), ITargCCCollection) 
          Dim pBytes As Byte() = pCollection.CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          MyStringBuilder.Append(p.Name & "^" & ccHelper.ToBase64String(pBytes) & "~") 
        Next 
 
        Dim MyString As String = MyStringBuilder.ToString() 
        'Now compress it  
        Dim MyStringCompressed As String = ccHelper.Zip(MyString) 
 
        'Now encrypt it  
        Dim MyStringCompressedAndEncrypted As String = ccHelper.Encrypt(ccHelper.enmEncryptionMethod.AES, MyStringCompressed) 
 
        'Now save it  
        Try 
          IO.File.WriteAllText(pFileLocation & "\" & NETEncryption.clsHash.Hash("clsDatabase", NETEncryption.clsHash.HashName.MD5) & ".gz", MyStringCompressedAndEncrypted) 
        Catch ex As Exception 
          pFault.LogException(ex, pFunctionParameters, "TRGT-130206-1008", vRequester) 
        End Try 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-121120-1142", vRequester) 
      End Try 
      Return pFault 
    End Function 
 
    Private Function ReadDatabaseFromBinary(ByRef rDatabase As clsDatabase) As String 
      Dim pFunctionParameters As String = "" 
 
      'Dim pType As Type = rDatabase.GetType 
 
      Dim pFileLocation As String = "" 
      Try 
        pFileLocation = MyController.XMLDataLocation 
      Catch ex As Exception 
        Return ex.Message 
      End Try 
 
      'Get the compressed and encrypted file  
      Dim MyStringCompressedAndEncrypted = IO.File.ReadAllText(pFileLocation & "\" & NETEncryption.clsHash.Hash("clsDatabase", NETEncryption.clsHash.HashName.MD5) & ".gz") 'ccHelper.BFEncrypt(MyStringCompressed)  
      'Now decrypt  
      Dim MyStringCompressed As String = ccHelper.Decrypt(ccHelper.enmEncryptionMethod.AES, MyStringCompressedAndEncrypted) 
      'now decompress  
      Dim MyString As String = ccHelper.UnZip(MyStringCompressed) 
 
      Dim pDictionary As New Dictionary(Of String, Byte()) 
 
      Dim pTables As String() = MyString.Split("~"c) 
      For Each l In pTables 
        If String.IsNullOrEmpty(l) Then Continue For 
        Dim pBytes As Byte() = ccHelper.ToByteArrayFromBase64String(l.Split("^"c)(1)) 
        pDictionary.Add(l.Split("^"c)(0), pBytes) 
      Next 
 
 
      Dim pResponse As String = "" 
      Try 
        rDatabase = New clsDatabase() 
 
        Dim pFault As clsFault = Nothing 
 
        For Each p As System.Reflection.PropertyInfo In Me.GetType().GetProperties() 
          If p.GetValue(Me, Nothing).GetType.GetInterface("ITargCCCollection") Is Nothing Then Continue For 
          Dim pName As String = p.Name 
          Dim pCollection As ITargCCCollection = CType(p.GetValue(Me, Nothing), ITargCCCollection) 
          Dim pBytes As Byte() = pDictionary(pName) 
          pCollection.LoadByteArray(pBytes, pFault, Nothing) : If Not pFault.isOK Then Return pFault.ShortStringForConcatenation 
        Next 
 
        pResponse = "OK" 
      Catch ex As Exception 
        pResponse = ex.ToString() & ccHelper.NewLine & (New StackFrame()).GetMethod().DeclaringType.Namespace() & ccHelper.NewLine & Me.GetType.Name & ccHelper.NewLine & (New StackFrame).GetMethod().Name & ccHelper.NewLine & pFunctionParameters & ccHelper.NewLine & "TRGT-121120-1304" 
        Tools.LogToTextFile.WriteMessage("Failed reading Binary database" & Environment.NewLine() & pResponse & Environment.NewLine(), "BinaryDB") 
      End Try 
      Return pResponse 
    End Function 
 
 
    Private Shared _LockDatabase As New Object 
 
    Private Function ReadBinary(ByVal rCollection As ITargCCCollection) As String 
      SyncLock _LockDatabase 
        Dim pFunctionParameters As String = "" 
 
 
        Dim pCollectionName As String = rCollection.GetType.Name 
 
        Dim pFileLocation As String = "" 
        Try 
          pFileLocation = MyController.XMLDataLocation 
        Catch ex As Exception 
          Return ex.Message 
        End Try 
 
 
        Dim pResponse As String = "" 
 
        'Check to see if Update.zip exists  
        Dim pUpdateFullFileName As String = pFileLocation & "Update.zip" 
        If IO.File.Exists(pUpdateFullFileName) Then 
          'unzip them  
          pResponse = ccHelper.UnZipUpdateFolder(pFileLocation) : If pResponse <> "OK" Then Return pResponse 
        End If 
 
        'Check to see if the master database exists. If so, delete all the tables  
        Dim pDatabaseFileName As String = NETEncryption.clsHash.Hash("clsDatabase", NETEncryption.clsHash.HashName.MD5) & ".gz" 
        Dim pDatabaseFullFileName As String = pFileLocation & pDatabaseFileName 
        If IO.File.Exists(pDatabaseFullFileName) Then 
          'an upgrade was done and a new database file was sent   
          For Each l In IO.Directory.GetFiles(pFileLocation) 
            If Not l.Equals(pDatabaseFullFileName, StringComparison.OrdinalIgnoreCase) Then 
              IO.File.Delete(l) 
            End If 
          Next 
        End If 
 
        Dim pStringEncrypted As String = "" 
        Try 
          Dim pFileToRead As String = pFileLocation & "\" & NETEncryption.clsHash.Hash(pCollectionName, NETEncryption.clsHash.HashName.MD5) & ".gz" 
          If IO.File.Exists(pFileToRead) Then 
            pStringEncrypted = IO.File.ReadAllText(pFileToRead) 
            pResponse = "OK" 
          Else 
            'Check if we have to expand the database file    
            pResponse = LoadDatabaseFromBinary() 
            If pResponse <> "OK" Then Return pResponse 
            Dim pFault As New clsFault 
            For Each p As System.Reflection.PropertyInfo In Me.GetType().GetProperties() 
              If p.GetValue(Me, Nothing).GetType.GetInterface("ITargCCCollection") Is Nothing Then Continue For 
              pFault = WriteBinary(CType(p.GetValue(Me, Nothing), ITargCCCollection), Nothing, True) 
            Next 
            If pFault.isOK = False Then Return pFault.StringForMessageBox 
            'now delete the master database  
            IO.File.Delete(pDatabaseFullFileName) 
            'now try again   
            pStringEncrypted = IO.File.ReadAllText(pFileToRead) 
            pResponse = "OK" 
          End If 
        Catch ex As Exception 
          pResponse = ex.ToString() & ccHelper.NewLine & (New StackFrame()).GetMethod().DeclaringType.Namespace() & ccHelper.NewLine & Me.GetType.Name & ccHelper.NewLine & (New StackFrame).GetMethod().Name & ccHelper.NewLine & pFunctionParameters & ccHelper.NewLine & "TRGT-121120-1303" 
        End Try 
 
        If pResponse <> "OK" Then Return pResponse 
 
        Dim pStringCompressed As String = ccHelper.Decrypt(ccHelper.enmEncryptionMethod.AES, pStringEncrypted) 
 
        Dim pString As String = ccHelper.UnZip(pStringCompressed) 
 
        Dim pData As String() = pString.Split("^"c) 
 
        Dim pName As String = pData(0) 
        Dim pByteArrayString As String = pData(1) 
 
        If Not pName.Equals(pCollectionName, StringComparison.OrdinalIgnoreCase) Then 
          Return "Expected file:" & pCollectionName & ". Got File:" & pName & "" 
        End If 
 
        Try 
          Dim pFault As clsFault = Nothing 
          rCollection.LoadByteArray(ccHelper.ToByteArrayFromBase64String(pByteArrayString), pFault, Nothing) 
          If Not pFault.isOK Then pResponse = pFault.ShortStringForConcatenation Else pResponse = "OK" 
        Catch ex As Exception 
          pResponse = ex.ToString() & ccHelper.NewLine & (New StackFrame()).GetMethod().DeclaringType.Namespace() & ccHelper.NewLine & Me.GetType.Name & ccHelper.NewLine & (New StackFrame).GetMethod().Name & ccHelper.NewLine & pFunctionParameters & ccHelper.NewLine & "TRGT-121120-1303" 
        End Try 
 
        Return pResponse 
      End SyncLock 
 
    End Function 
 
    Private Function WriteBinary(ByVal vCollection As ITargCCCollection, ByVal vRequester As clsRequester, Optional ByVal vOverwriteReadonly As Boolean = False) As clsFault 
      SyncLock _LockDatabase 
 
        Dim pFunctionParameters As String = "" 
        Dim pFault As New clsFault 
 
        If Not (_ReadOnly = False OrElse vOverwriteReadonly = True) Then 
          Return pFault.SetOK() 
        End If 
 
        Dim pFileLocation As String = "" 
        Try 
          pFileLocation = MyController.XMLDataLocation 
        Catch ex As Exception 
          Return pFault.LogFreeTextFault(74, ex.Message, pFunctionParameters, "TRGT-130210-1840", vRequester) 
        End Try 
 
        Try 
          Dim pName As String = vCollection.GetType.Name 
          Dim pBytes As Byte() 
          pBytes = vCollection.CreateByteArray(pFault, vRequester) 
          Dim pByteString As String = pName & "^" & ccHelper.ToBase64String(pBytes) 
 
          'Now Compress and encrypt it  
          Dim MyStringCompressed As String = ccHelper.Zip(pByteString) 
          Dim MyStringCompressedEncypted As String = ccHelper.Encrypt(ccHelper.enmEncryptionMethod.AES, MyStringCompressed) 
 
          'Now save it  
          Try 
            'My.Computer.FileSystem.WriteAllText(pFileLocation & "\" & pType.Name & ".xml", MyString, False)  
            IO.File.WriteAllText(pFileLocation & "\" & NETEncryption.clsHash.Hash(pName, NETEncryption.clsHash.HashName.MD5) & ".gz", MyStringCompressedEncypted) 
          Catch ex As Exception 
            pFault.LogException(ex, pFunctionParameters, "TRGT-130206-2115", vRequester) 
          End Try 
        Catch ex As Exception 
          pFault.LogException(ex, pFunctionParameters, "TRGT-130206-2114", vRequester) 
        End Try 
        Return pFault 
 
      End SyncLock 
 
    End Function 
 
  End Class 
End Class 
