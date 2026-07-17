Public Class csJob
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
 
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property CanHave0AsPrimaryKey As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  ''' <summary> 
  ''' To be used by the partial class to Override CreateEmpty 
  ''' </summary> 
  Private Event evtOverrideCreateEmpty() 
 
  ''' <summary> 
  ''' Raised after getting the row from the data store. This also occurs after an update 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterGet()
  Friend Event evtAfterGetWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  'Parent Properties 
  Public Enum enmParentProperty 
    UD 
    [Job] 
    [JobRunner] 
    [JobType] 
    [JobStatus] 
  End Enum 
  'Child Properties 
  Public Enum enmChildProperty 
    UD 
    [JobAlertRecipient] 
    [LoggedJob] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [Job] 
    [JobRunner] 
    [Description] 
    [Instructions] 
    [JobType] 
    [WhenToRun] 
    [CyclicCount] 
    [SendNotificationOnSuccess] 
    [SendAlarmOnMissed] 
    [TimeOutSec] 
    [Active] 
    [ActivatingUser] 
    [NextRunTime] 
    [LastRunTime] 
    [JobStatus] 
    [WarningMailSent] 
    [IsManaged] 
    [LastRunBy] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [CyclicCount] 
    [TimeOutSec] 
  End Enum 
  ''' <summary> 
  ''' Raised before add, just before evtBeforeUpdate 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeAdd(ByRef rCancel As Boolean) 
  Friend Event evtBeforeAddWithRequester(ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  ''' <summary> 
  ''' Raised after add, just before evtAfterUpdate 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterAdd()
  Friend Event evtAfterAddWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  'UpdatedColumns
  Public Enum enmUpdateType 
    UD 
    [Standard] 
    [ccUpdateSetToNowShared] 
  End Enum 
  ''' <summary> 
  ''' Raised before updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeUpdate(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean) 
  Friend Event evtBeforeUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  Friend Shared Event evtBeforeSharedUpdateWithRequester(ByVal vUpdateType As enmUpdateType, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  ''' <summary> 
  ''' Raised after updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterUpdate(ByVal vWhichColumn As enmUpdateType)
  Friend Event evtAfterUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  Friend Shared Event evtAfterSharedUpdateWithRequester(ByVal vUpdateType As enmUpdateType, ByVal vID As Long, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  ''' <summary> 
  ''' Raised before deleting the row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeDelete(ByRef rCancel As Boolean) 
  Friend Event evtBeforeDeleteWithRequester(ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  ''' <summary> 
  ''' Raised after deleting the row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterDelete() 
  Friend Event evtAfterDeleteWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private Event evtOverrideDefaultDesignation(ByRef rOverridenValue As String) 
  
  Private _ID As Long
  Private _JobCode As String
  Private _JobText As String 
  Private _JobRunnerCode As String
  Private _JobRunnerText As String 
  Private _Description As String
  Private _Instructions As String
  Private _JobType As clsEnums.enmJobType
  Private _JobTypeText As String 
  Private _WhenToRun As Date
  Private _CyclicCount As Integer
  Private _SendNotificationOnSuccess As Boolean
  Private _SendAlarmOnMissed As Boolean
  Private _TimeOutSec As Integer
  Private _Active As Boolean
  Private _ActivatingUser As String
  Private _NextRunTime As Date
  Private _LastRunTime As Date
  Private _JobStatus As clsEnums.enmJobStatus
  Private _JobStatusText As String 
  Private _WarningMailSent As Boolean
  Private _IsManaged As Boolean
  Private _LastRunBy As String
  Private _Tag As String
  Private _JobAlertRecipients As csJobAlertRecipientCol
  Private _LoggedJobs As csLoggedJobCol
  
  Public Property [ID]() As Long
    Get
      Return Me._ID
    End Get
    Set(ByVal value As Long)
      If Me._ID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ID = value 
        bPrimaryKey = _ID 
      End If 
    End Set
  End Property
  Public Property [JobCode]() As String
    Get
      Return Me._JobCode
    End Get
    Set(ByVal value As String)
      If Me._JobCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._JobCode = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property JobText() As String
    Get
      Return Me._JobText
    End Get
    Set(ByVal value As String)
      Me._JobText = value
    End Set
  End Property
  Public Property [JobRunnerCode]() As String
    Get
      Return Me._JobRunnerCode
    End Get
    Set(ByVal value As String)
      If Me._JobRunnerCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._JobRunnerCode = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property JobRunnerText() As String
    Get
      Return Me._JobRunnerText
    End Get
    Set(ByVal value As String)
      Me._JobRunnerText = value
    End Set
  End Property
  Public Property [Description]() As String
    Get
      Return Me._Description
    End Get
    Set(ByVal value As String)
      If Me._Description <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Description = value 
      End If 
    End Set
  End Property
  Public Property [Instructions]() As String
    Get
      Return Me._Instructions
    End Get
    Set(ByVal value As String)
      If Me._Instructions <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Instructions = value 
      End If 
    End Set
  End Property
  Public Property [JobType]() As clsEnums.enmJobType
    Get
      Return Me._JobType
    End Get
    Set(ByVal value As clsEnums.enmJobType)
      If Me._JobType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._JobType = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [JobTypeText]() As String
    Get
      Return Me._JobTypeText
    End Get
    Set(ByVal value As String)
      Me._JobTypeText = value
    End Set
  End Property
  Public Property [WhenToRun]() As Date
    Get
      Return Me._WhenToRun
    End Get
    Set(ByVal value As Date)
      If Me._WhenToRun <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._WhenToRun = value 
      End If 
    End Set
  End Property
  Public Property [CyclicCount]() As Integer
    Get
      Return Me._CyclicCount
    End Get
    Set(ByVal value As Integer)
      If Me._CyclicCount <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CyclicCount = value 
      End If 
    End Set
  End Property
  Public Property [SendNotificationOnSuccess]() As Boolean
    Get
      Return Me._SendNotificationOnSuccess
    End Get
    Set(ByVal value As Boolean)
      If Me._SendNotificationOnSuccess <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SendNotificationOnSuccess = value 
      End If 
    End Set
  End Property
  Public Property [SendAlarmOnMissed]() As Boolean
    Get
      Return Me._SendAlarmOnMissed
    End Get
    Set(ByVal value As Boolean)
      If Me._SendAlarmOnMissed <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SendAlarmOnMissed = value 
      End If 
    End Set
  End Property
  Public Property [TimeOutSec]() As Integer
    Get
      Return Me._TimeOutSec
    End Get
    Set(ByVal value As Integer)
      If Me._TimeOutSec <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TimeOutSec = value 
      End If 
    End Set
  End Property
  Public Property [Active]() As Boolean
    Get
      Return Me._Active
    End Get
    Set(ByVal value As Boolean)
      If Me._Active <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Active = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [ActivatingUser]() As String
    Get
      Return Me._ActivatingUser
    End Get
  End Property
  Public ReadOnly Property [NextRunTime]() As Date
    Get
      Return Me._NextRunTime
    End Get
  End Property
  Public ReadOnly Property [LastRunTime]() As Date
    Get
      Return Me._LastRunTime
    End Get
  End Property
  Public ReadOnly Property [JobStatus]() As clsEnums.enmJobStatus
    Get
      Return Me._JobStatus
    End Get
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [JobStatusText]() As String
    Get
      Return Me._JobStatusText
    End Get
    Set(ByVal value As String)
      Me._JobStatusText = value
    End Set
  End Property
  Public ReadOnly Property [WarningMailSent]() As Boolean
    Get
      Return Me._WarningMailSent
    End Get
  End Property
  Public Property [IsManaged]() As Boolean
    Get
      Return Me._IsManaged
    End Get
    Set(ByVal value As Boolean)
      If Me._IsManaged <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IsManaged = value 
      End If 
    End Set
  End Property
  Public Property [LastRunBy]() As String
    Get
      Return Me._LastRunBy
    End Get
    Set(ByVal value As String)
      If Me._LastRunBy <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LastRunBy = value 
      End If 
    End Set
  End Property
  ''' <summary> 
  ''' Extra property that is not stored in the database. Setting it does not trip the status to 'Dirty' 
  ''' </summary> 
  ''' <returns></returns> 
  <Newtonsoft.Json.JsonIgnore, Xml.Serialization.XmlIgnore> 
  Public Property [Tag]() As String
    Get
      Return Me._Tag
    End Get
    Set(ByVal value As String)
      If Me._Tag <> value Then 
        Me._Tag = value 
      End If 
    End Set
  End Property
  Public Property [JobAlertRecipients]() As csJobAlertRecipientCol
    Get
      Return Me._JobAlertRecipients
    End Get
    Set(ByVal value As csJobAlertRecipientCol)
      Me._JobAlertRecipients = value
    End Set
  End Property
  Public Property [LoggedJobs]() As csLoggedJobCol
    Get
      Return Me._LoggedJobs
    End Get
    Set(ByVal value As csLoggedJobCol)
      Me._LoggedJobs = value
    End Set
  End Property
  
  Private Sub CreateDefaultDesignation() 
    Dim pOverridenValue As String = Nothing 
    RaiseEvent evtOverrideDefaultDesignation(pOverridenValue) 
    If pOverridenValue = Nothing Then bDefaultDesignation = ccHelper.CreateFriendlyTextFromHungarianNotation(_JobCode & " on " & _JobRunnerCode) Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _JobCode <> "" Then pValue.Append("JobCode='" & _JobCode & "' ‡ ") 
    If _JobText <> "" Then pValue.Append("JobText='" & _JobText & "' ‡ ") 
    If _JobRunnerCode <> "" Then pValue.Append("JobRunnerCode='" & _JobRunnerCode & "' ‡ ") 
    If _JobRunnerText <> "" Then pValue.Append("JobRunnerText='" & _JobRunnerText & "' ‡ ") 
    If _Description <> "" Then pValue.Append("Description='" & _Description & "' ‡ ") 
    If _Instructions <> "" Then pValue.Append("Instructions='" & _Instructions & "' ‡ ") 
    If _JobType <> clsEnums.enmJobType.UD Then pValue.Append("JobType='" & _JobType.FastToString() & "' ‡ ") 
    If _JobTypeText <> "" Then pValue.Append("JobTypeText='" & _JobTypeText & "' ‡ ") 
    If Not (_WhenToRun = Nothing) Then pValue.Append("WhenToRun='" & _WhenToRun.ToString("o") & "' ‡ ") 
    If _CyclicCount <> 0 Then pValue.Append("CyclicCount='" & _CyclicCount.ToString() & "' ‡ ") 
    pValue.Append("SendNotificationOnSuccess='" & _SendNotificationOnSuccess.ToString() & "' ‡ ") 
    pValue.Append("SendAlarmOnMissed='" & _SendAlarmOnMissed.ToString() & "' ‡ ") 
    If _TimeOutSec <> 0 Then pValue.Append("TimeOutSec='" & _TimeOutSec.ToString() & "' ‡ ") 
    pValue.Append("Active='" & _Active.ToString() & "' ‡ ") 
    If _ActivatingUser <> "" Then pValue.Append("ActivatingUser='" & _ActivatingUser & "' ‡ ") 
    If Not (_NextRunTime = Nothing) Then pValue.Append("NextRunTime='" & _NextRunTime.ToString("o") & "' ‡ ") 
    If Not (_LastRunTime = Nothing) Then pValue.Append("LastRunTime='" & _LastRunTime.ToString("o") & "' ‡ ") 
    If _JobStatus <> clsEnums.enmJobStatus.UD Then pValue.Append("JobStatus='" & _JobStatus.FastToString() & "' ‡ ") 
    If _JobStatusText <> "" Then pValue.Append("JobStatusText='" & _JobStatusText & "' ‡ ") 
    pValue.Append("WarningMailSent='" & _WarningMailSent.ToString() & "' ‡ ") 
    pValue.Append("IsManaged='" & _IsManaged.ToString() & "' ‡ ") 
    If _LastRunBy <> "" Then pValue.Append("LastRunBy='" & _LastRunBy & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_JobCode)}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_JobText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_JobRunnerCode)}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_JobRunnerText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Description)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Instructions)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_JobType.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_JobTypeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_WhenToRun.ToShortDateString & " " & _WhenToRun.ToShortTimeString)}""") 
    pCSV.Append("," & _CyclicCount.ToString() & "") 
    pCSV.Append(",""" & _SendNotificationOnSuccess.ToString() & """") 
    pCSV.Append(",""" & _SendAlarmOnMissed.ToString() & """") 
    pCSV.Append("," & _TimeOutSec.ToString() & "") 
    pCSV.Append(",""" & _Active.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ActivatingUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_NextRunTime.ToShortDateString & " " & _NextRunTime.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastRunTime.ToShortDateString & " " & _LastRunTime.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_JobStatus.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_JobStatusText)}""") 
    pCSV.Append(",""" & _WarningMailSent.ToString() & """") 
    pCSV.Append(",""" & _IsManaged.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastRunBy)}""") 
    If Not vWithTexts Then 
        pCSV.Append($",""{ccHelper.StringForCSV(_Tag)}""") 
    End If 
    'pCSV.Append($",""{bDateAdded:yyyyMMddTHH:mm:ss.ffff}"" ") 
    
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty()
  End Sub
  
  Public Sub New(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional vMustExist As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    
    rFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
  End Sub
  
  Public Sub New(ByVal vcsJob As csJob)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsJob) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vJobCode As String = "" _ 
    , Optional vJobText As String = "" _ 
    , Optional vJobRunnerCode As String = "" _ 
    , Optional vJobRunnerText As String = "" _ 
    , Optional vDescription As String = "" _ 
    , Optional vInstructions As String = "" _ 
    , Optional vJobType As clsEnums.enmJobType = clsEnums.enmJobType.UD _ 
    , Optional vJobTypeText As String = "" _ 
    , Optional vWhenToRun As Date = Nothing _ 
    , Optional vCyclicCount As Integer = 0 _ 
    , Optional vSendNotificationOnSuccess As Boolean = False _ 
    , Optional vSendAlarmOnMissed As Boolean = False _ 
    , Optional vTimeOutSec As Integer = 0 _ 
    , Optional vActive As Boolean = False _ 
    , Optional vActivatingUser As String = "" _ 
    , Optional vNextRunTime As Date = Nothing _ 
    , Optional vLastRunTime As Date = Nothing _ 
    , Optional vJobStatus As clsEnums.enmJobStatus = clsEnums.enmJobStatus.UD _ 
    , Optional vJobStatusText As String = "" _ 
    , Optional vWarningMailSent As Boolean = False _ 
    , Optional vIsManaged As Boolean = False _ 
    , Optional vLastRunBy As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _JobCode = vJobCode 
    _JobText = vJobText 
    _JobRunnerCode = vJobRunnerCode 
    _JobRunnerText = vJobRunnerText 
    _Description = vDescription 
    _Instructions = vInstructions 
    _JobType = vJobType 
    _JobTypeText = vJobTypeText 
    _WhenToRun = vWhenToRun 
    _CyclicCount = vCyclicCount 
    _SendNotificationOnSuccess = vSendNotificationOnSuccess 
    _SendAlarmOnMissed = vSendAlarmOnMissed 
    _TimeOutSec = vTimeOutSec 
    _Active = vActive 
    _ActivatingUser = vActivatingUser 
    _NextRunTime = vNextRunTime 
    _LastRunTime = vLastRunTime 
    _JobStatus = vJobStatus 
    _JobStatusText = vJobStatusText 
    _WarningMailSent = vWarningMailSent 
    _IsManaged = vIsManaged 
    _LastRunBy = vLastRunBy 
    _Tag = vTag 
    bDateAdded = vDateAdded 
    bccStatus = clsEnums.enmObjectStatus.Dirty 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  Friend Sub New(ByVal vRow As DataRow, ByVal vRequester As clsRequester) 
    MyBase.New()
    CreateEmpty()
    Dim pFault As New clsFault 
 
    pFault = LoadDataRow(vRow, vRequester) 
    If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox) 
 
 
  End Sub 
 
  Public Sub New(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New()
    CreateEmpty()
    LoadByteArray(vBytes, rFault, vRequester) 
  End Sub 
 
  Public Sub New(ByVal vBytesFromAPI As Object, ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    Dim pBytes As Byte() = DirectCast(vBytesFromAPI, Byte()) 
    LoadByteArray(pBytes, rFault, vRequester) 
  End Sub 
 
  Public Overrides Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) 
    Throw New Exception("Entity has no parents") 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    Throw New Exception("Entity is not localizable") 
  End Sub 
 
  Private _IsTruncated As Boolean = False 
  
  ''' <summary> 
  ''' Use this before loading a DataGridView. You don't need more than X c to see what you want. 
  ''' </summary> 
  ''' <param name="pTruncateLength"></param> 
  Friend Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
    'Truncates strings, and reduces pictures to W 100 x H 50 
 
    _IsTruncated = False 
 
    _JobCode = _JobCode.Truncate(pTruncateLength, _IsTruncated) 
    _JobRunnerCode = _JobRunnerCode.Truncate(pTruncateLength, _IsTruncated) 
    _Description = _Description.Truncate(pTruncateLength, _IsTruncated) 
    _Instructions = _Instructions.Truncate(pTruncateLength, _IsTruncated) 
    _ActivatingUser = _ActivatingUser.Truncate(pTruncateLength, _IsTruncated) 
    _LastRunBy = _LastRunBy.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Job by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Job-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [JobCodeAndJobRunnerCode] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Job by the chosen parameters. This function may be a bit slower than accessing the Job's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.JobCodeAndJobRunnerCode 
          pFault = GetByJobCodeAndJobRunnerCode(CStr(vParameters(0)), CStr(vParameters(1)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Job-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Job-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the Job by ID. 
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vID 
          pBinaryWriter.Write(vID) 
          ' 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csJobGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the Job 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the Job by JobCodeAndJobRunnerCode. 
  ''' </summary>
  ''' <param name="vJobCode"></param>
  ''' <param name="vJobRunnerCode"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByJobCodeAndJobRunnerCode(ByVal vJobCode As String, ByVal vJobRunnerCode As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("JobCode={0}, JobRunnerCode={1}", vJobCode, vJobRunnerCode)
    Dim pFault As New clsFault 
    
    CreateEmpty() 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vJobCode 
          If vJobCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobCode) 
          ' 
          'vJobRunnerCode 
          If vJobRunnerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobRunnerCode) 
          ' 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csJobGetByJobCodeAndJobRunnerCode" 
      Dim pParametersToLog = $"JobCodeAndJobRunnerCode: {vJobCode};{vJobRunnerCode};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the Job 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Job-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Job-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the Job. If there are parents or children in the Job, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = String.Format("Job.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pJob As New csJob 
    If Me.isEqual(pJob) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-Job-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Job-240611-135714", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Dim pObjectAdded As Boolean = False 
    
    If _ID = 0 Then 
      pObjectAdded = True 
      RaiseEvent evtBeforeAdd(pCancel) 
      If pCancel = True Then Return pFault 
      RaiseEvent evtBeforeAddWithRequester(pCancel, vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
      If pCancel = True Then Return pFault 
    End If 
    RaiseEvent evtBeforeUpdate(enmUpdateType.Standard, pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeUpdateWithRequester(enmUpdateType.Standard, pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csJobUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    If pObjectAdded = True Then 
      RaiseEvent evtAfterAdd() 
      RaiseEvent evtAfterAddWithRequester(vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
    End If 
      RaiseEvent evtAfterUpdate(enmUpdateType.Standard)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.Standard, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  ''' <summary> 
  ''' This updates the minimum fields needed to update the SetToNow. Use when performance is important 
  ''' </summary> 
  ''' <param name="vID"></param> 
  ''' <param name="vNextRunTime"></param> 
  ''' <param name="vActive"></param> 
  ''' <param name="vWarningMailSent"></param> 
  ''' <param name="vActivatingUser"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Shared Function UpdateSetToNow(ByVal vID As Long, ByVal vNextRunTime As Date, ByVal vActive As Boolean, ByVal vWarningMailSent As Boolean, ByVal vActivatingUser As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("Job.ID={0}", vID) 
    Dim pFault As New clsFault 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    RaiseEvent evtBeforeSharedUpdateWithRequester(enmUpdateType.ccUpdateSetToNowShared, pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
    
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vID 
          pBinaryWriter.Write(vID) 
          'vNextRunTime 
          pBinaryWriter.Write(vNextRunTime.Ticks) 
          'vActive 
          pBinaryWriter.Write(vActive) 
          'vWarningMailSent 
          pBinaryWriter.Write(vWarningMailSent) 
          'vActivatingUser 
          If vActivatingUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vActivatingUser) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "csJobUpdateSetToNow" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction,  pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-CardActivation-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterSharedUpdateWithRequester(enmUpdateType.ccUpdateSetToNowShared, vID, vRequester, pFault) 
 
    Return pFault 
  End Function 
 
  
  'Interface Delete 
  Public Function Delete(ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntityDeletable.Delete 
    Dim pFunctionParameters As String = String.Format("Job.ID={0}", _ID)
    Dim pFault As New clsFault
    
    Dim pCancel As Boolean = False
    
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(_ID) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csJobDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterDelete()
    RaiseEvent evtAfterDeleteWithRequester(vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  ''' <summary>  
  ''' This function enables you to delete an entity from the database without first loading it.  
  ''' </summary>  
  ''' <param name="vID"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function DeleteByID(vID As Long, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"ID: {vID}" 
    Dim pFault As clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vID) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "csJobDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-Job-231207-1707", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Fills the Job's JobAlertRecipient collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillJobAlertRecipients(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    _JobAlertRecipients = New csJobAlertRecipientCol(clsEnums.enmLoadParent.DoNotLoad)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _JobAlertRecipients.FillByJobID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the Job's LoggedJob collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillLoggedJobs(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    _LoggedJobs = New csLoggedJobCol(clsEnums.enmLoadParent.DoNotLoad)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _LoggedJobs.FillByJobID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csJob) Then Return False 
    Dim pJobToTest As csJob = CType(vTargCCEntityToTest, csJob) 
    Return isEqual(pJobToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vJobToTest As csJob) As Boolean
    With vJobToTest
      If _ID <> .ID Then Return False
      If _JobCode <> .JobCode Then Return False
      If _JobRunnerCode <> .JobRunnerCode Then Return False
      If _Description <> .Description Then Return False
      If _Instructions <> .Instructions Then Return False
      If _JobType <> .JobType Then Return False
      If _WhenToRun <> Nothing AndAlso .WhenToRun <> Nothing Then 
        If ccHelper.ToLong(_WhenToRun.Subtract(.WhenToRun).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_WhenToRun = Nothing AndAlso .WhenToRun = Nothing) Then 
        Return False 
      End If 
      If _CyclicCount <> .CyclicCount Then Return False
      If _SendNotificationOnSuccess <> .SendNotificationOnSuccess Then Return False
      If _SendAlarmOnMissed <> .SendAlarmOnMissed Then Return False
      If _TimeOutSec <> .TimeOutSec Then Return False
      If _Active <> .Active Then Return False
      If _ActivatingUser <> .ActivatingUser Then Return False
      If _NextRunTime <> Nothing AndAlso .NextRunTime <> Nothing Then 
        If ccHelper.ToLong(_NextRunTime.Subtract(.NextRunTime).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_NextRunTime = Nothing AndAlso .NextRunTime = Nothing) Then 
        Return False 
      End If 
      If _LastRunTime <> Nothing AndAlso .LastRunTime <> Nothing Then 
        If ccHelper.ToLong(_LastRunTime.Subtract(.LastRunTime).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_LastRunTime = Nothing AndAlso .LastRunTime = Nothing) Then 
        Return False 
      End If 
      If _JobStatus <> .JobStatus Then Return False
      If _WarningMailSent <> .WarningMailSent Then Return False
      If _IsManaged <> .IsManaged Then Return False
      If _LastRunBy <> .LastRunBy Then Return False
      If _Tag <> .Tag Then Return False
      If bDateAdded <> .DateAdded Then Return False 
      If bccStatus <> .ccStatus Then Return False 
    End With
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are equal, IGNORING the dependants 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCEntity() As ITargCCEntity 
    Dim pClone As New csJob(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csJob
    Dim pClone As New csJob(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("JobCode") = _JobCode : Catch ex As Exception : Return pFault.LogException(ex, "JobCode", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("JobRunnerCode") = _JobRunnerCode : Catch ex As Exception : Return pFault.LogException(ex, "JobRunnerCode", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("Description") = _Description : Catch ex As Exception : Return pFault.LogException(ex, "Description", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("Instructions") = _Instructions : Catch ex As Exception : Return pFault.LogException(ex, "Instructions", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("JobType") = _JobType : Catch ex As Exception : Return pFault.LogException(ex, "JobType", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("WhenToRun") = _WhenToRun : Catch ex As Exception : Return pFault.LogException(ex, "WhenToRun", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("CyclicCount") = _CyclicCount : Catch ex As Exception : Return pFault.LogException(ex, "CyclicCount", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("SendNotificationOnSuccess") = _SendNotificationOnSuccess : Catch ex As Exception : Return pFault.LogException(ex, "SendNotificationOnSuccess", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("SendAlarmOnMissed") = _SendAlarmOnMissed : Catch ex As Exception : Return pFault.LogException(ex, "SendAlarmOnMissed", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("TimeOutSec") = _TimeOutSec : Catch ex As Exception : Return pFault.LogException(ex, "TimeOutSec", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("Active") = _Active : Catch ex As Exception : Return pFault.LogException(ex, "Active", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("ActivatingUser") = _ActivatingUser : Catch ex As Exception : Return pFault.LogException(ex, "ActivatingUser", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("NextRunTime") = _NextRunTime : Catch ex As Exception : Return pFault.LogException(ex, "NextRunTime", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastRunTime") = _LastRunTime : Catch ex As Exception : Return pFault.LogException(ex, "LastRunTime", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("JobStatus") = _JobStatus : Catch ex As Exception : Return pFault.LogException(ex, "JobStatus", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("WarningMailSent") = _WarningMailSent : Catch ex As Exception : Return pFault.LogException(ex, "WarningMailSent", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("IsManaged") = _IsManaged : Catch ex As Exception : Return pFault.LogException(ex, "IsManaged", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastRunBy") = _LastRunBy : Catch ex As Exception : Return pFault.LogException(ex, "LastRunBy", "TRGT-Job-130316-0852", vRequester) : End Try 
    Try : vDataRow("Tag") = _Tag : Catch ex As Exception : End Try 
    Try : vDataRow("DateAdded") = bDateAdded : Catch ex As Exception : Return pFault.LogException(ex, "DateAdded", "TRGT-TransactionLoad-130316-0852", vRequester) : End Try 
    bPrimaryKey = _ID
    CreateDefaultDesignation() 
 
    Return pFault.SetOK() 
  End Function 
 
  Public Overrides Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    rXML = "" 
    Try 
      Dim pType As Type = Me.GetType 
      pFunctionParameters = pType.Name 
      Dim pSerializer As Xml.Serialization.XmlSerializer 
      pSerializer = New Xml.Serialization.XmlSerializer(pType) 
      Dim MyStringBuilder As New Text.StringBuilder 
      Dim pWriter As New IO.StringWriter(MyStringBuilder) 
      pSerializer.Serialize(pWriter, Me) 
      pWriter.Close() 
      pFault.SetOK() 
 
      rXML = MyStringBuilder.ToString() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Job-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pJob As csJob = CType(pXmlSerializer.Deserialize(pStreamReader), csJob) 
      AssignValues(pJob) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-Job-130515-1230", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  Public Overrides Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
    
    Dim pBytes As Byte() = Nothing 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pLength As Integer = 0 
          Dim pHasValue As Boolean = False 
          Dim pObjectBytes As Byte() = Nothing 
          pBinaryWriter.Write(bccStatus.FastToString()) 
          'ID 
          pBinaryWriter.Write(_ID) 
          'JobCode 
          If _JobCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_JobCode) 
          pBinaryWriter.Write(_JobText) 
          'JobRunnerCode 
          If _JobRunnerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_JobRunnerCode) 
          pBinaryWriter.Write(_JobRunnerText) 
          'Description 
          If _Description Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Description) 
          'Instructions 
          If _Instructions Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Instructions) 
          'JobType 
          pBinaryWriter.Write(_JobType.FastToString()) 
          'WhenToRun 
          pBinaryWriter.Write(_WhenToRun.Ticks) 
          'CyclicCount 
          pBinaryWriter.Write(_CyclicCount) 
          'SendNotificationOnSuccess 
          pBinaryWriter.Write(_SendNotificationOnSuccess) 
          'SendAlarmOnMissed 
          pBinaryWriter.Write(_SendAlarmOnMissed) 
          'TimeOutSec 
          pBinaryWriter.Write(_TimeOutSec) 
          'Active 
          pBinaryWriter.Write(_Active) 
          'ActivatingUser 
          If _ActivatingUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ActivatingUser) 
          'NextRunTime 
          pBinaryWriter.Write(_NextRunTime.Ticks) 
          'LastRunTime 
          pBinaryWriter.Write(_LastRunTime.Ticks) 
          'JobStatus 
          pBinaryWriter.Write(_JobStatus.FastToString()) 
          'WarningMailSent 
          pBinaryWriter.Write(_WarningMailSent) 
          'IsManaged 
          pBinaryWriter.Write(_IsManaged) 
          'LastRunBy 
          If _LastRunBy Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LastRunBy) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          'JobAlertRecipients  
          If _JobAlertRecipients IsNot Nothing Then 
            pObjectBytes = _JobAlertRecipients.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'LoggedJobs  
          If _LoggedJobs IsNot Nothing Then 
            pObjectBytes = _LoggedJobs.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, pFunctionParameters, "TRGT-Job-150307-2338", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Public Overrides Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Try 
      If rFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pLength As Integer = 0 
          Dim pHasValue As Boolean = False 
          Dim pObjectBytes As Byte() = Nothing 
          bccStatus = clsEnums.TranslateEnmObjectStatus(pReader.ReadString) 
          'ID 
          _ID = pReader.ReadInt64 
          'JobCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _JobCode = pReader.ReadString 
          _JobText = pReader.ReadString 
          'JobRunnerCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _JobRunnerCode = pReader.ReadString 
          _JobRunnerText = pReader.ReadString 
          'Description 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Description = pReader.ReadString 
          'Instructions 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Instructions = pReader.ReadString 
          'JobType 
          _JobType = clsEnums.TranslateEnmJobType(pReader.ReadString) 
          'WhenToRun 
          _WhenToRun = New Date(pReader.ReadInt64) 
          'CyclicCount 
          _CyclicCount = pReader.ReadInt32 
          'SendNotificationOnSuccess 
          _SendNotificationOnSuccess = pReader.ReadBoolean 
          'SendAlarmOnMissed 
          _SendAlarmOnMissed = pReader.ReadBoolean 
          'TimeOutSec 
          _TimeOutSec = pReader.ReadInt32 
          'Active 
          _Active = pReader.ReadBoolean 
          'ActivatingUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ActivatingUser = pReader.ReadString 
          'NextRunTime 
          _NextRunTime = New Date(pReader.ReadInt64) 
          'LastRunTime 
          _LastRunTime = New Date(pReader.ReadInt64) 
          'JobStatus 
          _JobStatus = clsEnums.TranslateEnmJobStatus(pReader.ReadString) 
          'WarningMailSent 
          _WarningMailSent = pReader.ReadBoolean 
          'IsManaged 
          _IsManaged = pReader.ReadBoolean 
          'LastRunBy 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LastRunBy = pReader.ReadString 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          'JobAlertRecipients 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _JobAlertRecipients = New csJobAlertRecipientCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'LoggedJobs 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedJobs = New csLoggedJobCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-Job-150307-2339", vRequester) 
    End Try 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  ''' <summary> 
  ''' Returns JSON for public properties 
  ''' </summary> 
  ''' <param name="rJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    rJSON = "" 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      rJSON = Newtonsoft.Json.JsonConvert.SerializeObject(Me, pSettings) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Job-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  ''' <summary> 
  ''' Creates object using JSON received, for public properties 
  ''' </summary> 
  ''' <param name="vJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      Dim pJob As csJob = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csJob)(vJSON, pSettings) 
      AssignValues(pJob) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Job-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vJob As csJob)
    With vJob
      _ID = .ID 
      _JobCode = .JobCode 
      _JobText = .JobText 
      _JobRunnerCode = .JobRunnerCode 
      _JobRunnerText = .JobRunnerText 
      _Description = .Description 
      _Instructions = .Instructions 
      _JobType = .JobType 
      _JobTypeText = .JobTypeText
      _WhenToRun = .WhenToRun 
      _CyclicCount = .CyclicCount 
      _SendNotificationOnSuccess = .SendNotificationOnSuccess 
      _SendAlarmOnMissed = .SendAlarmOnMissed 
      _TimeOutSec = .TimeOutSec 
      _Active = .Active 
      _ActivatingUser = .ActivatingUser 
      _NextRunTime = .NextRunTime 
      _LastRunTime = .LastRunTime 
      _JobStatus = .JobStatus 
      _JobStatusText = .JobStatusText
      _WarningMailSent = .WarningMailSent 
      _IsManaged = .IsManaged 
      _LastRunBy = .LastRunBy 
      _Tag = .Tag 
      bDateAdded = .DateAdded 
      bccStatus = .ccStatus
    End With
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub
  
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If _ID = 0 Then 
      Return pFault.SetOK() 
    End If 
 
    Dim pTextToGet As String = "" 
    Try 
      'Job 
      pTextToGet = "JobText (Lookup)" 
      _JobText = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.Job, _JobCode, vRequester) 
      'JobRunner 
      pTextToGet = "JobRunnerText (Lookup)" 
      _JobRunnerText = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.JobRunner, _JobRunnerCode, vRequester) 
      'JobType 
      pTextToGet = "JobTypeText (Enum)" 
      _JobTypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.JobType, _JobType.FastToString(), vRequester) 
      'JobStatus 
      pTextToGet = "JobStatusText (Enum)" 
      _JobStatusText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.JobStatus, _JobStatus.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-Job-151124-1900", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _JobCode = ""
    _JobText = ""
    _JobRunnerCode = ""
    _JobRunnerText = ""
    _Description = ""
    _Instructions = ""
    _JobType = clsEnums.enmJobType.UD
    _JobTypeText = ""
    _WhenToRun = Nothing
    _CyclicCount = 0
    _SendNotificationOnSuccess = False
    _SendAlarmOnMissed = False
    _TimeOutSec = 0
    _Active = False
    _ActivatingUser = ""
    _NextRunTime = Nothing
    _LastRunTime = Nothing
    _JobStatus = clsEnums.enmJobStatus.UD
    _JobStatusText = ""
    _WarningMailSent = False
    _IsManaged = False
    _LastRunBy = ""
    _Tag = ""
    _JobAlertRecipients = Nothing
    _LoggedJobs = Nothing
    bccStatus = clsEnums.enmObjectStatus.New 
    bPrimaryKey = _ID 
    bDateAdded = Nothing 
    bDefaultDesignation = "" 
     
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class csJobCol
  Inherits cTargCCCollection(Of csJob)
  Implements ITargCCCollectionUpdateable 
  
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property CanHave0AsPrimaryKey As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Long) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csJob) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByJobCodeAndJobRunnerCode As Dictionary(Of String, csJob) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByJobCodeAndJobRunnerCode As Boolean 
  Private Function CreateKeyForFindByJobCodeAndJobRunnerCode(ByVal vJob As csJob) As String 
    With vJob 
      Return .JobCode & "|" & .JobRunnerCode
    End With 
  End Function 
   
  Private _Tag As String = "" 
  Public Property [Tag]() As String 
    Get 
      Return Me._Tag 
    End Get 
    Set(ByVal value As String) 
      Me._Tag = value 
    End Set 
  End Property 
 
  'ToString 
  Public Overrides Function ToString() As String 
    Dim pString As New Text.StringBuilder 
 
    pString.AppendLine("Instance of " & Me.GetType().Name & ". Number of rows" & Me.Count.ToString()) 
    If _Tag <> "" Then pString.AppendLine("  Tag='" & _Tag & "'") 
 
    For Each pRow As csJob In Me 
      pString.AppendLine(pRow.ToString & Environment.NewLine) 
    Next 
 
    Return pString.ToString() 
  End Function 
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New Text.StringBuilder 
    Dim pCSVTitle As New Text.StringBuilder 
    'Get title 
    Dim pDbCode As String = "" 
    If vWithTexts Then pDbCode = " (Db Code)" 
    pCSVTitle.Append("""ID""") 
    pCSVTitle.Append(",""JobCode" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Job (Text)""") 
    pCSVTitle.Append(",""JobRunnerCode" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""JobRunner (Text)""") 
    pCSVTitle.Append(",""Description""") 
    pCSVTitle.Append(",""Instructions""") 
    pCSVTitle.Append(",""JobType" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""JobType (Text)""") 
    pCSVTitle.Append(",""WhenToRun""") 
    pCSVTitle.Append(",""CyclicCount""") 
    pCSVTitle.Append(",""SendNotificationOnSuccess""") 
    pCSVTitle.Append(",""SendAlarmOnMissed""") 
    pCSVTitle.Append(",""TimeOutSec""") 
    pCSVTitle.Append(",""Active""") 
    pCSVTitle.Append(",""ActivatingUser""") 
    pCSVTitle.Append(",""NextRunTime""") 
    pCSVTitle.Append(",""LastRunTime""") 
    pCSVTitle.Append(",""JobStatus" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""JobStatus (Text)""") 
    pCSVTitle.Append(",""WarningMailSent""") 
    pCSVTitle.Append(",""IsManaged""") 
    pCSVTitle.Append(",""LastRunBy""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csJob In Me 
      pCSV.AppendLine(pRow.ToCSV(vWithTexts)) 
    Next 
 
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty() 
  End Sub
  
  Public Sub New(ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) 
    MyBase.New()
    CreateEmpty() 
    
    rFault = Fill(vRequester, vHowMany, vDir) 
  End Sub
  
  Public Sub New(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    LoadByteArray(vBytes, rFault, vRequester) 
  End Sub 
 
  Public Sub New(ByVal vBytesFromAPI As Object, ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    Dim pBytes As Byte() = DirectCast(vBytesFromAPI, Byte()) 
    LoadByteArray(pBytes, rFault, vRequester) 
  End Sub 
 
  Public Overloads Sub Add(ByVal vJob As csJob) 
    SyncLock _CollectionLock 
      MyBase.Add(vJob) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vJob As csJob) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vJob) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vJobCol As csJobCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vJobCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vJob As csJob) 
    SyncLock _CollectionLock 
      MyBase.Remove(vJob) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = True 
    End SyncLock 
  End Sub 
 
  Private Sub LoadIDs() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByID Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByID Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByID = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByID' yet!
      Dim pTempDictionary As New Dictionary(Of Long, csJob) 
      
      For Each lJob In Me 
        If lJob.IsEmpty OrElse pTempDictionary.ContainsKey(lJob.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lJob.ID, lJob) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lJob.ToString, "TRGT-Job-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", Job:" & lJob.ToString() & ", TRGT-Job-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadJobCodeAndJobRunnerCodes() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByJobCodeAndJobRunnerCode Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByJobCodeAndJobRunnerCode Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByJobCodeAndJobRunnerCode' yet!
      Dim pTempDictionary As New Dictionary(Of String, csJob)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lJob In Me 
        Try 
          Dim pJobCodeAndJobRunnerCode As String = CreateKeyForFindByJobCodeAndJobRunnerCode(lJob) 
          If String.IsNullOrEmpty(pJobCodeAndJobRunnerCode.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pJobCodeAndJobRunnerCode)) Then 
            pTempDictionary.Add(pJobCodeAndJobRunnerCode, lJob) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lJob.ToString, "TRGT-Job-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByJobCodeAndJobRunnerCode:" & ex.Message & ", Job:" & lJob.ToString() & ", TRGT-Job-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByJobCodeAndJobRunnerCode = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = False
    End SyncLock 
  End Sub 
 
  Public Overrides Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) 
    Throw New Exception("Entity has no parents") 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    Throw New Exception("Entity is not localizable") 
  End Sub 
 
  ''' <summary>  
  ''' Use this before loading a DataGridView. You don't need more than pTruncateLength characters to see what you want.  
  ''' </summary>  
  ''' <param name="pTruncateLength"></param>  
  Public Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
 
    For Each lJob As csJob In Me 
      lJob.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [Active] 
    [JobCode] 
    [JobRunnerCode] 
    [JobRunnerCodeAndActive] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the Jobs by the chosen parameters. This function may be a bit slower than accessing the Job's FillBy... directly 
  ''' </summary> 
  ''' <param name="vWhichParameterCombination"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vHowMany"></param> 
  ''' <param name="vDir"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillByParameters(ByVal vWhichParameterCombination As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault 
    Dim pFunctionParameters As String = String.Format("WhichParameterCombination={0}", vWhichParameterCombination.ToString()) 
    Dim pFault As clsFault 
 
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.Active 
          pFault = FillByActive(CBool(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.JobCode 
          pFault = FillByJobCode(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.JobRunnerCode 
          pFault = FillByJobRunnerCode(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.JobRunnerCodeAndActive 
          pFault = FillByJobRunnerCodeAndActive(CStr(vParameters(0)), CBool(vParameters(1)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Job-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Job-151223_1716", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a collection of all the items, or a sub-collection defined by HowMany and Direction
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
    
    Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Active, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByActive(ByVal vActive As Boolean, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Active={0}", vActive)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vActive 
          pBinaryWriter.Write(vActive) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFillByActive" 
      Dim pParametersToLog = $"Active: {vActive};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job  
      If vAppend = True Then 
        Dim pJobs As New csJobCol 
        pJobs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pJobs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific JobCode, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByJobCode(ByVal vJobCode As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("JobCode={0}", vJobCode)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vJobCode 
          If vJobCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobCode) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFillByJobCode" 
      Dim pParametersToLog = $"JobCode: {vJobCode};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job  
      If vAppend = True Then 
        Dim pJobs As New csJobCol 
        pJobs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pJobs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific JobRunnerCode, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByJobRunnerCode(ByVal vJobRunnerCode As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("JobRunnerCode={0}", vJobRunnerCode)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vJobRunnerCode 
          If vJobRunnerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobRunnerCode) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFillByJobRunnerCode" 
      Dim pParametersToLog = $"JobRunnerCode: {vJobRunnerCode};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job  
      If vAppend = True Then 
        Dim pJobs As New csJobCol 
        pJobs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pJobs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific JobRunnerCode and Active, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByJobRunnerCodeAndActive(ByVal vJobRunnerCode As String, ByVal vActive As Boolean, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("JobRunnerCode={0}, Active={1}", vJobRunnerCode, vActive)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vJobRunnerCode 
          If vJobRunnerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobRunnerCode) 
          ' 
          'vActive 
          pBinaryWriter.Write(vActive) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFillByJobRunnerCodeAndActive" 
      Dim pParametersToLog = $"JobRunnerCodeAndActive: {vJobRunnerCode};{vActive};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job  
      If vAppend = True Then 
        Dim pJobs As New csJobCol 
        pJobs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pJobs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDFrom 
          pBinaryWriter.Write(vIDFrom) 
          ' 
          'vIDTo 
          pBinaryWriter.Write(vIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job  
      If vAppend = True Then 
        Dim pJobs As New csJobCol 
        pJobs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pJobs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>  
  ''' Gets a collection of all the items for the specified list of ID's. To append to an existing collection, set vAppend to true (default is false).  
  ''' An ID can only exist once in the collection. If it's already in the collection, it will be removed from vIDs before sending to the server. 
  ''' </summary>  
  ''' <param name="vIDs"></param>  
  ''' <param name="vRequester"></param>  
  ''' <param name="vDir"></param>  
  ''' <param name="vAppend"></param>  
  ''' <returns></returns>  
  Public Function FillByListOfID(vIDs As List(Of Long), vRequester As clsRequester, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = $"Count of IDs: {vIDs?.Count}" 
    Dim pFault As New clsFault 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    'If it's append, we have to ensure no doubles, even though we're not sending the collection to the server 
    If vAppend = True Then 
      For Each l In Me 
        If vIDs.Contains(l.ID) Then 
          vIDs.Remove(l.ID) 
        End If 
      Next 
    End If 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDs 
          pBinaryWriter.Write(vIDs.Count) 
          For Each l In vIDs 
            pBinaryWriter.Write(l) 
          Next 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job   
      If vAppend = True Then 
        Dim pJobs As New csJobCol 
        pJobs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pJobs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-231207-1750", vRequester) 
    End Try 
 
    pFault.SetOK() 
    RaiseEvent evtAfterFill() 
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault 
  End Function 
 
  Public Enum enmFillOnTheFlyParameters 
    UD 
    IDFrom
    IDTo
    [JobCode]
    [JobRunnerCode]
    [Active]
  End Enum 
  Public Enum enmListDefinition 
    UD 
    HowMany 
    Dir 
  End Enum 
 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. Only send the fields you need 
  ''' </summary> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillOnTheFly(ByVal vParameters As Dictionary(Of System.Enum, Object), ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pJobCode As String = Nothing
    Dim pJobRunnerCode As String = Nothing
    Dim pActive As Nullable(Of Boolean) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.JobCode) Then pObj = vParameters(enmFillOnTheFlyParameters.JobCode) : If pObj IsNot Nothing Then pJobCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.JobRunnerCode) Then pObj = vParameters(enmFillOnTheFlyParameters.JobRunnerCode) : If pObj IsNot Nothing Then pJobRunnerCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Active) Then pObj = vParameters(enmFillOnTheFlyParameters.Active) : If pObj IsNot Nothing Then pActive = CBool(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pJobCode _
        , pJobRunnerCode _
        , pActive _
        , vRequester, pHowMany, pDir) : If pFault.isOK = False Then Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vJobCode As String _
        , ByVal vJobRunnerCode As String _
        , ByVal vActive As Nullable(Of Boolean) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, JobCode={2}, JobRunnerCode={3}, Active={4}", vIDFrom, vIDTo, vJobCode, vJobRunnerCode, vActive)
    
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Dim pParametersToLog = $"" 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) : pParametersToLog &= $"IDFrom={vIDFrom};"  
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) : pParametersToLog &= $"IDTo={vIDTo};"  
          'JobCode 
          If vJobCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobCode) : pParametersToLog &= $"JobCode={vJobCode};"  
          'JobRunnerCode 
          If vJobRunnerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobRunnerCode) : pParametersToLog &= $"JobRunnerCode={vJobRunnerCode};"  
          'Active 
          pBinaryWriter.Write(vActive.HasValue) 
          If vActive.HasValue = True Then pBinaryWriter.Write(vActive.Value) : pParametersToLog &= $"Active={vActive};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByJobCode
    GroupByJobRunnerCode
    GroupByActive
  End Enum 
 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. Only send the fields you need. Default for GrouBy is False 
  ''' </summary> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillSumOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pJobCode As String = Nothing
    Dim pJobRunnerCode As String = Nothing
    Dim pActive As Nullable(Of Boolean) = Nothing
    Dim pGroupByJobCode As Boolean = False
    Dim pGroupByJobRunnerCode As Boolean = False
    Dim pGroupByActive As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.JobCode) Then pObj = vParameters(enmFillOnTheFlyParameters.JobCode) : If pObj IsNot Nothing Then pJobCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.JobRunnerCode) Then pObj = vParameters(enmFillOnTheFlyParameters.JobRunnerCode) : If pObj IsNot Nothing Then pJobRunnerCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Active) Then pObj = vParameters(enmFillOnTheFlyParameters.Active) : If pObj IsNot Nothing Then pActive = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByJobCode) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByJobCode) : If pObj IsNot Nothing Then pGroupByJobCode = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByJobRunnerCode) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByJobRunnerCode) : If pObj IsNot Nothing Then pGroupByJobRunnerCode = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByActive) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByActive) : If pObj IsNot Nothing Then pGroupByActive = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pJobCode _
        , pJobRunnerCode _
        , pActive _
        , pGroupByJobCode _
        , pGroupByJobRunnerCode _
        , pGroupByActive _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a grouped collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillSumOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vJobCode As String _
        , ByVal vJobRunnerCode As String _
        , ByVal vActive As Nullable(Of Boolean) _
        , ByVal vGroupByJobCode As Boolean _
        , ByVal vGroupByJobRunnerCode As Boolean _
        , ByVal vGroupByActive As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, JobCode={2}, JobRunnerCode={3}, Active={4}, GroupByJobCode={5}, GroupByJobRunnerCode={6}, GroupByActive={7}", vIDFrom, vIDTo, vJobCode, vJobRunnerCode, vActive, vGroupByJobCode, vGroupByJobRunnerCode, vGroupByActive)
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Dim pParametersToLog = $"" 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) : pParametersToLog &= $"IDFrom={vIDFrom};"  
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) : pParametersToLog &= $"IDTo={vIDTo};"  
          'JobCode 
          If vJobCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobCode) : pParametersToLog &= $"JobCode={vJobCode};"  
          'JobRunnerCode 
          If vJobRunnerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobRunnerCode) : pParametersToLog &= $"JobRunnerCode={vJobRunnerCode};"  
          'Active 
          pBinaryWriter.Write(vActive.HasValue) 
          If vActive.HasValue = True Then pBinaryWriter.Write(vActive.Value) : pParametersToLog &= $"Active={vActive};"  
          pBinaryWriter.Write(vGroupByJobCode) : pParametersToLog &= $"GroupByJobCode={vGroupByJobCode};"  
          pBinaryWriter.Write(vGroupByJobRunnerCode) : pParametersToLog &= $"GroupByJobRunnerCode={vGroupByJobRunnerCode};"  
          pBinaryWriter.Write(vGroupByActive) : pParametersToLog &= $"GroupByActive={vGroupByActive};"  
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the Job  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vJobArray As csJob())
    Me.Clear()
    
    For Each pJob As csJob In vJobArray
      Me.Add(pJob)
      _Clean.Add(pJob.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pJob As New csJob(pRow, vRequester) 
        Me.Add(pJob) 
        _Clean.Add(pJob.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-JobCol-130315-2118", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Overrides Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    rXML = "" 
    Try 
      Dim pType As Type = Me.GetType 
      pFunctionParameters = pType.Name 
      Dim pSerializer As Xml.Serialization.XmlSerializer 
      pSerializer = New Xml.Serialization.XmlSerializer(pType) 
      Dim MyStringBuilder As New Text.StringBuilder 
      Dim pWriter As New IO.StringWriter(MyStringBuilder) 
      pSerializer.Serialize(pWriter, Me) 
      pWriter.Close() 
      pFault.SetOK() 
 
      rXML = MyStringBuilder.ToString() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Job-130515-1300", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function FillFromXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pJobs As csJobCol = CType(pXmlSerializer.Deserialize(pStreamReader), csJobCol) 
      For Each pJob As csJob In pJobs 
        Me.Add(pJob) 
        _Clean.Add(pJob.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-Job-130515-1329", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' Returns JSON for public properties in collection 
  ''' </summary> 
  ''' <param name="rJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    rJSON = "" 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      rJSON = Newtonsoft.Json.JsonConvert.SerializeObject(Me, pSettings) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Job-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  ''' <summary> 
  '''   ''' Creates collection using JSON received, for public properties 
  ''' </summary> 
  ''' <param name="vJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      Dim pJobs As List(Of csJob) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csJob))(vJSON, pSettings) 
      For Each pJob As csJob In pJobs 
        Me.Add(pJob) 
        _Clean.Add(pJob.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Job-190720-2059", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Overrides Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Dim pBytes As Byte() = Nothing 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'Tag  
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'Items 
          pBinaryWriter.Write(Me.Count) 
          For Each lJob As csJob In Me 
            Dim pByte As Byte() = lJob.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
            pBinaryWriter.Write(pByte.Length) 
            pBinaryWriter.Write(pByte, 0, pByte.Length) 
          Next 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, pFunctionParameters, "TRGT-Job-150307-2340", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Public Overrides Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
 
    Me.Clear() 
    
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'Tag  
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'Items 
          Dim pCount As Integer = pReader.ReadInt32 
          For iCntr As Integer = 0 To pCount - 1 
            Dim pLength As Integer = pReader.ReadInt32 
            Dim pJob As csJob = New csJob(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pJob) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pJob.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-Job-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pJob As csJob In Me 
      With pJob 
        pFault = pJob.LoadLookupAndEnumText(vRequester) 
        If Not pFault.isOK Then Exit For 
      End With 
    Next 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vEntitiesToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(vEntitiesToTest As ITargCCCollection) As Boolean 
    If Not (TypeOf (vEntitiesToTest) Is csJobCol) Then Return False 
    Dim pJobColToTest As csJobCol = CType(vEntitiesToTest, csJobCol) 
    Return isEqual(pJobColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vJobsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vJobsToTest As csJobCol) As Boolean
    If Me.Count <> vJobsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vJobsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pJobs As New csJobCol() 
    If pFilledFromSumOnTheFly Then pJobs._FilledFromSumOnTheFly = True
    
    For Each pJob As csJob In Me 
      Dim pJobClone As csJob = pJob.Clone() 
      pJobs.Add(pJobClone) 
      If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
    Next 
    Return pJobs 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csJobCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pJobs As New csJobCol() 
    If pFilledFromSumOnTheFly Then pJobs._FilledFromSumOnTheFly = True
    
    For Each pJob As csJob In Me
      Dim pJobClone As csJob = pJob.Clone()
      pJobs.Add(pJobClone)
      If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
    Next
    Return pJobs
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csJobCol 
    Dim pJobs As New csJobCol()  
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pJob As csJob In _SortedDictionaryForFindByID.Values.ToList() 
      If (pJob.ID > vIDFrom AndAlso pJob.ID <= vIDTo) Then 
        Dim pJobClone As csJob = pJob.Clone() 
        pJobs.Add(pJobClone) 
        If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
      End If 
    Next 
    Return pJobs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by JobCode (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedJobCode(ByVal vJobCodeFrom As String, ByVal vJobCodeTo As String) As csJobCol 
    Dim pJobs As New csJobCol()  
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pJob As csJob In _SortedDictionaryForFindByID.Values.ToList() 
      If (pJob.JobCode > vJobCodeFrom AndAlso pJob.JobCode <= vJobCodeTo) Then 
        Dim pJobClone As csJob = pJob.Clone() 
        pJobs.Add(pJobClone) 
        If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
      End If 
    Next 
    Return pJobs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by JobCode and JobRunnerCode (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedJobCodeAndJobRunnerCode(ByVal vJobCodeFrom As String, ByVal vJobCodeTo As String, ByVal vJobRunnerCodeFrom As String, ByVal vJobRunnerCodeTo As String) As csJobCol 
    Dim pJobs As New csJobCol()  
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pJob As csJob In _SortedDictionaryForFindByID.Values.ToList() 
      If (pJob.JobCode > vJobCodeFrom AndAlso pJob.JobCode <= vJobCodeTo) AndAlso (pJob.JobRunnerCode > vJobRunnerCodeFrom AndAlso pJob.JobRunnerCode <= vJobRunnerCodeTo) Then 
        Dim pJobClone As csJob = pJob.Clone() 
        pJobs.Add(pJobClone) 
        If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
      End If 
    Next 
    Return pJobs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by JobRunnerCode (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedJobRunnerCode(ByVal vJobRunnerCodeFrom As String, ByVal vJobRunnerCodeTo As String) As csJobCol 
    Dim pJobs As New csJobCol()  
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pJob As csJob In _SortedDictionaryForFindByID.Values.ToList() 
      If (pJob.JobRunnerCode > vJobRunnerCodeFrom AndAlso pJob.JobRunnerCode <= vJobRunnerCodeTo) Then 
        Dim pJobClone As csJob = pJob.Clone() 
        pJobs.Add(pJobClone) 
        If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
      End If 
    Next 
    Return pJobs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by JobRunnerCode and Active (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedJobRunnerCodeAndActive(ByVal vJobRunnerCodeFrom As String, ByVal vJobRunnerCodeTo As String, ByVal vActive As Boolean) As csJobCol 
    Dim pJobs As New csJobCol()  
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pJob As csJob In _SortedDictionaryForFindByID.Values.ToList() 
      If (pJob.JobRunnerCode > vJobRunnerCodeFrom AndAlso pJob.JobRunnerCode <= vJobRunnerCodeTo) AndAlso (pJob.Active = vActive) Then 
        Dim pJobClone As csJob = pJob.Clone() 
        pJobs.Add(pJobClone) 
        If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
      End If 
    Next 
    Return pJobs 
  End Function 
  
  ''' <summary> 
  ''' Used for Interface compliance. This returns a unique object in the collection. It searches locally, within the collection. It does not access the database  
  ''' If it doesn't find anything, it creates a new, empty object 
  ''' </summary> 
  ''' <param name="vPrimaryKey"></param> 
  ''' <returns></returns> 
  Public Overrides Function FindByPrimaryKey(vPrimaryKey As Long) As ITargCCEntity 
    Return FindByID(vPrimaryKey) 
  End Function 
 
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByID(ByVal vID As Long) As csJob
    If Me.Count = 0 Then Return New csJob 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
    
    Dim pJob As csJob = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pJob) 
    If pJob IsNot Nothing Then Return pJob Else Return New csJob() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByJobCodeAndJobRunnerCode(ByVal vJobCode As String, ByVal vJobRunnerCode As String) As csJob
    If Me.Count = 0 Then Return New csJob 
    
    If _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = True Then LoadJobCodeAndJobRunnerCodes() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csJob) = _SortedDictionaryForFindByJobCodeAndJobRunnerCode 
    
    Dim pJob As csJob = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vJobCode & "|" & vJobRunnerCode
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pJob) 
    If pJob IsNot Nothing Then Return pJob Else Return New csJob() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined JobCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByJobCode(ByVal vJobCode As String) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vJobCode = vJobCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.JobCode.ToLowerInvariant() = vJobCode Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByJobCode with vJobCode of {vJobCode}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.JobCode.ToLowerInvariant() = vJobCode Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined JobRunnerCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByJobRunnerCode(ByVal vJobRunnerCode As String) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vJobRunnerCode = vJobRunnerCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.JobRunnerCode.ToLowerInvariant() = vJobRunnerCode Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByJobRunnerCode with vJobRunnerCode of {vJobRunnerCode}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.JobRunnerCode.ToLowerInvariant() = vJobRunnerCode Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Description
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDescription(ByVal vDescription As String) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDescription = vDescription.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.Description.ToLowerInvariant() = vDescription Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDescription with vDescription of {vDescription}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.Description.ToLowerInvariant() = vDescription Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Instructions
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByInstructions(ByVal vInstructions As String) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vInstructions = vInstructions.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.Instructions.ToLowerInvariant() = vInstructions Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByInstructions with vInstructions of {vInstructions}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.Instructions.ToLowerInvariant() = vInstructions Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined JobType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByJobType(ByVal vJobType As clsEnums.enmJobType) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.JobType = vJobType Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByJobType with vJobType of {vJobType}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.JobType = vJobType Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WhenToRun
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWhenToRun(ByVal vWhenToRun As Date) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.WhenToRun = vWhenToRun Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWhenToRun with vWhenToRun of {vWhenToRun}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.WhenToRun = vWhenToRun Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CyclicCount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCyclicCount(ByVal vCyclicCount As Integer) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.CyclicCount = vCyclicCount Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCyclicCount with vCyclicCount of {vCyclicCount}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.CyclicCount = vCyclicCount Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SendNotificationOnSuccess
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySendNotificationOnSuccess(ByVal vSendNotificationOnSuccess As Boolean) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.SendNotificationOnSuccess = vSendNotificationOnSuccess Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySendNotificationOnSuccess with vSendNotificationOnSuccess of {vSendNotificationOnSuccess}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.SendNotificationOnSuccess = vSendNotificationOnSuccess Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SendAlarmOnMissed
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySendAlarmOnMissed(ByVal vSendAlarmOnMissed As Boolean) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.SendAlarmOnMissed = vSendAlarmOnMissed Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySendAlarmOnMissed with vSendAlarmOnMissed of {vSendAlarmOnMissed}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.SendAlarmOnMissed = vSendAlarmOnMissed Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TimeOutSec
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTimeOutSec(ByVal vTimeOutSec As Integer) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.TimeOutSec = vTimeOutSec Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTimeOutSec with vTimeOutSec of {vTimeOutSec}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.TimeOutSec = vTimeOutSec Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Active
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByActive(ByVal vActive As Boolean) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.Active = vActive Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByActive with vActive of {vActive}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.Active = vActive Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ActivatingUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByActivatingUser(ByVal vActivatingUser As String) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vActivatingUser = vActivatingUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.ActivatingUser.ToLowerInvariant() = vActivatingUser Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByActivatingUser with vActivatingUser of {vActivatingUser}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.ActivatingUser.ToLowerInvariant() = vActivatingUser Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined NextRunTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNextRunTime(ByVal vNextRunTime As Date) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.NextRunTime = vNextRunTime Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNextRunTime with vNextRunTime of {vNextRunTime}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.NextRunTime = vNextRunTime Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastRunTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastRunTime(ByVal vLastRunTime As Date) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.LastRunTime = vLastRunTime Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastRunTime with vLastRunTime of {vLastRunTime}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.LastRunTime = vLastRunTime Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined JobStatus
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByJobStatus(ByVal vJobStatus As clsEnums.enmJobStatus) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.JobStatus = vJobStatus Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByJobStatus with vJobStatus of {vJobStatus}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.JobStatus = vJobStatus Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WarningMailSent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWarningMailSent(ByVal vWarningMailSent As Boolean) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.WarningMailSent = vWarningMailSent Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWarningMailSent with vWarningMailSent of {vWarningMailSent}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.WarningMailSent = vWarningMailSent Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IsManaged
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIsManaged(ByVal vIsManaged As Boolean) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.IsManaged = vIsManaged Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIsManaged with vIsManaged of {vIsManaged}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.IsManaged = vIsManaged Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastRunBy
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastRunBy(ByVal vLastRunBy As String) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLastRunBy = vLastRunBy.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.LastRunBy.ToLowerInvariant() = vLastRunBy Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastRunBy with vLastRunBy of {vLastRunBy}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.LastRunBy.ToLowerInvariant() = vLastRunBy Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pJob As csJob In pTempDist.Values
        If pJob.Tag.ToLowerInvariant() = vTag Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.Tag.ToLowerInvariant() = vTag Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    
    Return pJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined JobRunnerCodeAndActive
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByJobRunnerCodeAndActive(ByVal vJobRunnerCode As String, ByVal vActive As Boolean) As csJobCol
    Dim pJobs As New csJobCol() 
    pJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pJob As csJob In _SortedDictionaryForFindByID.Values.ToList()
        If pJob.JobRunnerCode = vJobRunnerCode AndAlso pJob.Active = vActive Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csJobCol = Me.Clone() 
      For Each pJob As csJob In pList 
        If pJob.JobRunnerCode = vJobRunnerCode AndAlso pJob.Active = vActive Then
          Dim pJobClone As csJob = pJob.Clone()
          pJobs.Add(pJobClone)
          If Not _FilledFromSumOnTheFly Then pJobs._Clean.Add(pJob.ID) 
        End If
      Next
    End If 
    Return pJobs
  End Function
  
  ''' <summary> 
  ''' Loads Me into the datatable vDataTable provided. 
  ''' </summary> 
  ''' <param name="vDataTable"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadMeIntoDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    vDataTable.Rows.Clear() 
    For Each pJob As csJob In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pJob.LoadDataRow(pRow, vRequester) 
      If pFault.isOK = False Then Return pFault 
      vDataTable.Rows.Add(pRow) 
    Next 
 
    Return pFault.SetOK 
  End Function 
 
  ''' <summary> 
  ''' This updates a collection that originates from the database. It will delete any rows not in the collection that were originally there (checks the 'Clean' variable) 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault Implements ITargCCCollectionUpdateable.Update 
    Dim pFunctionParameters As String = ""
 
    Dim pFault As New clsFault
 
    'Check for new rows 
    For Each p As csJob In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
    For Each pCleanID As Long In _Clean 
      If pCleanID = 0 Then Continue For 
      Dim pFound As csJob = Me.FindByID(pCleanID) 
      If pFound.ID = 0 Then 
        Dim pJobToKill As New csJob 
        pJobToKill.ID = pCleanID 
        pJobToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
        Me.Add(pJobToKill) 
      End If 
    Next 
 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csJobColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the JobCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150314-1803", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
   
  ''' <summary> 
  ''' This takes an external collection and updates the found rows in the database. If a row is not found (has an ID of 0), it adds it. It will not delete any rows. Check the 'tag' of the returned collection to see if it was updated. 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function UpdateFromCollection(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault Implements ITargCCCollectionUpdateable.UpdateFromCollection 
    Dim pFunctionParameters As String = ""
 
    Dim pFault As New clsFault
 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csJobColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the JobCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-150314-1803", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
   
  ''' <summary>
  ''' Deletes a collection of all items 
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function Delete(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific Active 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByActive(ByVal vActive As Boolean, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Active={0}", vActive)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vActive 
          pBinaryWriter.Write(vActive) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColDeleteByActive" 
      Dim pParametersToLog = $"Active: {vActive};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific JobCode 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByJobCode(ByVal vJobCode As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("JobCode={0}", vJobCode)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vJobCode 
          If vJobCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobCode) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColDeleteByJobCode" 
      Dim pParametersToLog = $"JobCode: {vJobCode};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific JobRunnerCode 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByJobRunnerCode(ByVal vJobRunnerCode As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("JobRunnerCode={0}", vJobRunnerCode)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vJobRunnerCode 
          If vJobRunnerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobRunnerCode) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColDeleteByJobRunnerCode" 
      Dim pParametersToLog = $"JobRunnerCode: {vJobRunnerCode};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific JobRunnerCodeAndActive 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByJobRunnerCodeAndActive(ByVal vJobRunnerCode As String, ByVal vActive As Boolean, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("JobRunnerCode={0}, Active={1}", vJobRunnerCode, vActive)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vJobRunnerCode 
          If vJobRunnerCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vJobRunnerCode) 
          ' 
          'vActive 
          pBinaryWriter.Write(vActive) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColDeleteByJobRunnerCodeAndActive" 
      Dim pParametersToLog = $"JobRunnerCodeAndActive: {vJobRunnerCode};{vActive};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Job-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDFrom 
          pBinaryWriter.Write(vIDFrom) 
          ' 
          'vIDTo 
          pBinaryWriter.Write(vIDTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csJobColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary> 
  ''' Used for Interface compliance. Sorts the Entity by the Primary Key (usually ID) 
  ''' </summary> 
  Public Overrides Sub SortByPrimaryKey() 
    SortByID() 
  End Sub 
 
  Public Sub SortByID()
    Me.Sort(New csJobCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ID < y.ID Then
        Return -1
      ElseIf x.ID = y.ID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByJobCode()
    Me.Sort(New csJobCol.CompareByJobCode)
  End Sub
  Private Class CompareByJobCode
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.JobCode, y.JobCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByJobText()
    Me.Sort(New csJobCol.CompareByJobText)
  End Sub
  Private Class CompareByJobText
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.JobText, y.JobText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByJobRunnerCode()
    Me.Sort(New csJobCol.CompareByJobRunnerCode)
  End Sub
  Private Class CompareByJobRunnerCode
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.JobRunnerCode, y.JobRunnerCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByJobRunnerText()
    Me.Sort(New csJobCol.CompareByJobRunnerText)
  End Sub
  Private Class CompareByJobRunnerText
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.JobRunnerText, y.JobRunnerText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDescription()
    Me.Sort(New csJobCol.CompareByDescription)
  End Sub
  Private Class CompareByDescription
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Description, y.Description, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByInstructions()
    Me.Sort(New csJobCol.CompareByInstructions)
  End Sub
  Private Class CompareByInstructions
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Instructions, y.Instructions, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByJobType()
    Me.Sort(New csJobCol.CompareByJobType)
  End Sub
  Private Class CompareByJobType
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.JobType < y.JobType Then
        Return -1
      ElseIf x.JobType = y.JobType Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByJobTypeText()
    Me.Sort(New csJobCol.CompareByJobTypeText)
  End Sub
  Private Class CompareByJobTypeText
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.JobTypeText, y.JobTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByWhenToRun()
    Me.Sort(New csJobCol.CompareByWhenToRun)
  End Sub
  Private Class CompareByWhenToRun
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.WhenToRun < y.WhenToRun Then
        Return -1
      ElseIf x.WhenToRun = y.WhenToRun Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByCyclicCount()
    Me.Sort(New csJobCol.CompareByCyclicCount)
  End Sub
  Private Class CompareByCyclicCount
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.CyclicCount < y.CyclicCount Then
        Return -1
      ElseIf x.CyclicCount = y.CyclicCount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySendNotificationOnSuccess()
    Me.Sort(New csJobCol.CompareBySendNotificationOnSuccess)
  End Sub
  Private Class CompareBySendNotificationOnSuccess
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SendNotificationOnSuccess.ToString, y.SendNotificationOnSuccess.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySendAlarmOnMissed()
    Me.Sort(New csJobCol.CompareBySendAlarmOnMissed)
  End Sub
  Private Class CompareBySendAlarmOnMissed
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SendAlarmOnMissed.ToString, y.SendAlarmOnMissed.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTimeOutSec()
    Me.Sort(New csJobCol.CompareByTimeOutSec)
  End Sub
  Private Class CompareByTimeOutSec
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TimeOutSec < y.TimeOutSec Then
        Return -1
      ElseIf x.TimeOutSec = y.TimeOutSec Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByActive()
    Me.Sort(New csJobCol.CompareByActive)
  End Sub
  Private Class CompareByActive
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Active.ToString, y.Active.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByActivatingUser()
    Me.Sort(New csJobCol.CompareByActivatingUser)
  End Sub
  Private Class CompareByActivatingUser
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ActivatingUser, y.ActivatingUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNextRunTime()
    Me.Sort(New csJobCol.CompareByNextRunTime)
  End Sub
  Private Class CompareByNextRunTime
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.NextRunTime < y.NextRunTime Then
        Return -1
      ElseIf x.NextRunTime = y.NextRunTime Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLastRunTime()
    Me.Sort(New csJobCol.CompareByLastRunTime)
  End Sub
  Private Class CompareByLastRunTime
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LastRunTime < y.LastRunTime Then
        Return -1
      ElseIf x.LastRunTime = y.LastRunTime Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByJobStatus()
    Me.Sort(New csJobCol.CompareByJobStatus)
  End Sub
  Private Class CompareByJobStatus
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.JobStatus < y.JobStatus Then
        Return -1
      ElseIf x.JobStatus = y.JobStatus Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByJobStatusText()
    Me.Sort(New csJobCol.CompareByJobStatusText)
  End Sub
  Private Class CompareByJobStatusText
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.JobStatusText, y.JobStatusText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByWarningMailSent()
    Me.Sort(New csJobCol.CompareByWarningMailSent)
  End Sub
  Private Class CompareByWarningMailSent
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.WarningMailSent.ToString, y.WarningMailSent.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByIsManaged()
    Me.Sort(New csJobCol.CompareByIsManaged)
  End Sub
  Private Class CompareByIsManaged
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IsManaged.ToString, y.IsManaged.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLastRunBy()
    Me.Sort(New csJobCol.CompareByLastRunBy)
  End Sub
  Private Class CompareByLastRunBy
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LastRunBy, y.LastRunBy, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csJobCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csJob)
    Private Function Compare(ByVal x As csJob, ByVal y As csJob) As Integer Implements System.Collections.Generic.IComparer(Of csJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Tag, y.Tag, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csJob) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByJobCodeAndJobRunnerCode = New Dictionary(Of String, csJob)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByJobCodeAndJobRunnerCode = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csJob) 
    _SortedDictionaryForFindByJobCodeAndJobRunnerCode = New Dictionary(Of String, csJob)(StringComparer.OrdinalIgnoreCase) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
