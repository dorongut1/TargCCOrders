Public Interface ITargCCEntity

  ReadOnly Property HasParents As Boolean
  ReadOnly Property HasLocalizedFields As Boolean
  ReadOnly Property CanHave0AsPrimaryKey As Boolean

  ReadOnly Property PrimaryKey As Long
  ReadOnly Property DateAdded As Date
  ReadOnly Property DefaultDesignation As String
  ReadOnly Property IsEmpty As Boolean

  Function ToString() As String
  Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String

  Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent)
  Sub SetLocalizable(ByVal vIsLocalized As Boolean)

  Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
  Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault

  Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean
  Function CloneTargCCEntity() As ITargCCEntity
  Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault
  Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault
  Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte()
  Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester)
  Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault
  Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault

  Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault

  'Sample to find if interface is implemented
  'Dim pC As New clsCustomer
  'If TypeOf (pC) Is ITargCCEntity Then
  '  Stop
  'End If

End Interface

Public Interface ITargCCEntityAddable

  Function AddUpdate(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
End Interface

Public Interface ITargCCEntityEditable

  Function EditUpdate(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
End Interface

Public Interface ITargCCEntityDeletable

  Function Delete(ByVal vRequester As clsRequester) As clsFault
End Interface


Public MustInherit Class cTargCCEntity
  Implements ITargCCEntity

  Protected bHasParents As Boolean
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property HasParents As Boolean Implements ITargCCEntity.HasParents
    Get
      Return bHasParents
    End Get
  End Property

  Protected bHasLocalizedFields As Boolean
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property HasLocalizedFields As Boolean Implements ITargCCEntity.HasLocalizedFields
    Get
      Return bHasLocalizedFields
    End Get
  End Property

  Protected bCanHave0AsPrimaryKey As Boolean
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property CanHave0AsPrimaryKey As Boolean Implements ITargCCEntity.CanHave0AsPrimaryKey
    Get
      Return bCanHave0AsPrimaryKey
    End Get
  End Property

  Protected bPrimaryKey As Long
  ''' <summary> 
  ''' The same as ID for most entities. Used for Interface compliance 
  ''' </summary> 
  ''' <returns></returns> 
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property PrimaryKey As Long Implements ITargCCEntity.PrimaryKey
    Get
      Return bPrimaryKey
    End Get
  End Property

  Protected bDateAdded As Date
  ''' <summary> 
  ''' The same as ID for most entities. Used for Interface compliance 
  ''' </summary> 
  ''' <returns></returns> 
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property DateAdded As Date Implements ITargCCEntity.DateAdded
    Get
      Return bDateAdded
    End Get
  End Property

  Protected bDefaultDesignation As String
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property DefaultDesignation As String Implements ITargCCEntity.DefaultDesignation
    Get
      Return bDefaultDesignation
    End Get
  End Property

  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property IsEmpty As Boolean Implements ITargCCEntity.IsEmpty
    Get
      If CanHave0AsPrimaryKey = True Then
        If bPrimaryKey = -1 Then Return True Else Return False
      Else
        If bPrimaryKey = 0 Then Return True Else Return False
      End If
    End Get
  End Property

  Protected bccStatus As clsEnums.enmObjectStatus
  <Newtonsoft.Json.JsonIgnore, Xml.Serialization.XmlIgnore>
  Public Property ccStatus As clsEnums.enmObjectStatus
    Get
      Return bccStatus
    End Get
    Set(ByVal value As clsEnums.enmObjectStatus)
      bccStatus = value
    End Set
  End Property

  Public MustOverride Overrides Function ToString() As String Implements ITargCCEntity.ToString
  Public MustOverride Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String Implements ITargCCEntity.ToCSV

  Public MustOverride Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) Implements ITargCCEntity.SetWithParents
  Public MustOverride Sub SetLocalizable(ByVal vIsLocalized As Boolean) Implements ITargCCEntity.SetLocalizable

  Public MustOverride Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault Implements ITargCCEntity.GetByPrimaryKey
  Public MustOverride Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault Implements ITargCCEntity.GetByParameters

  Public MustOverride Function isEqual(vTargCCEntityToTest As ITargCCEntity) As Boolean Implements ITargCCEntity.isEqual

  Public MustOverride Function CloneTargCCEntity() As ITargCCEntity Implements ITargCCEntity.CloneTargCCEntity

  Friend MustOverride Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault

  Public MustOverride Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntity.CreateXML
  Public MustOverride Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntity.LoadXML
  Public MustOverride Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte() Implements ITargCCEntity.CreateByteArray
  Public MustOverride Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) Implements ITargCCEntity.LoadByteArray
  Public MustOverride Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntity.CreateJSON
  Public MustOverride Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntity.LoadJSON

  Public MustOverride Function LoadLookupAndEnumText(vRequester As clsRequester) As clsFault Implements ITargCCEntity.LoadLookupAndEnumText

End Class


Public Interface ITargCCCollection
  Inherits IEnumerable, IList

  ReadOnly Property HasParents As Boolean
  ReadOnly Property HasLocalizedFields As Boolean
  ReadOnly Property CanHave0AsPrimaryKey As Boolean

  Function ToString() As String
  Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String

  Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent)
  Sub SetLocalizable(ByVal vIsLocalized As Boolean)

  Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
  Function FillByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault

  Function FillOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault

  Function FillSumOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault

  Sub FillFromArray(ByVal vTargCCEntityArray As ITargCCEntity())
  Sub FillFromListOfITargCCEntity(ByVal vList As Generic.List(Of ITargCCEntity))
  Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault
  Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault
  Function FillFromXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault
  Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte()
  Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester)
  Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault
  Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault

  Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault

  Function isEqual(ByVal vEntitiesToTest As ITargCCCollection) As Boolean

  Function CloneTargCCCollection() As ITargCCCollection

  Function FindByPrimaryKey(ByVal vPrimaryKey As Long) As ITargCCEntity

  Function LoadMeIntoDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault

  Sub SortByPrimaryKey()

End Interface

Public Interface ITargCCCollectionUpdateable

  Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
  Function UpdateFromCollection(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
End Interface

Public MustInherit Class cTargCCCollection(Of ITargCCEntity)
  Inherits Generic.List(Of ITargCCEntity)
  Implements ITargCCCollection

  Protected bHasParents As Boolean
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property HasParents As Boolean Implements ITargCCCollection.HasParents
    Get
      Return bHasParents
    End Get
  End Property

  Protected bHasLocalizedFields As Boolean
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property HasLocalizedFields As Boolean Implements ITargCCCollection.HasLocalizedFields
    Get
      Return bHasLocalizedFields
    End Get
  End Property

  Protected bCanHave0AsPrimaryKey As Boolean
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property CanHave0AsPrimaryKey As Boolean Implements ITargCCCollection.CanHave0AsPrimaryKey
    Get
      Return bCanHave0AsPrimaryKey
    End Get
  End Property

  Public MustOverride Overrides Function ToString() As String Implements ITargCCCollection.ToString
  Public MustOverride Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String Implements ITargCCCollection.ToCSV

  Public MustOverride Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) Implements ITargCCCollection.SetWithParents
  Public MustOverride Sub SetLocalizable(ByVal vIsLocalized As Boolean) Implements ITargCCCollection.SetLocalizable

  Public MustOverride Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault Implements ITargCCCollection.Fill
  Public MustOverride Function FillByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault Implements ITargCCCollection.FillByParameters

  Public MustOverride Function FillOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.FillOnTheFly

  Public MustOverride Function FillSumOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.FillSumOnTheFly

  Public Sub FillFromArray(vTargCCEntityArray() As DataController.ITargCCEntity) Implements ITargCCCollection.FillFromArray
    Me.Clear()
    For Each pTargCCEntity As ITargCCEntity In vTargCCEntityArray
      Me.Add(pTargCCEntity)
    Next
  End Sub
  Public Sub FillFromListOfITargCCEntity(vList As List(Of DataController.ITargCCEntity)) Implements ITargCCCollection.FillFromListOfITargCCEntity
    Me.Clear()
    For Each pLookup As ITargCCEntity In vList
      Me.Add(pLookup)
    Next
  End Sub
  Public MustOverride Function FillFromDataTable(vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.FillFromDataTable

  Public MustOverride Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.CreateXML
  Public MustOverride Function FillFromXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.FillFromXML
  Public MustOverride Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte() Implements ITargCCCollection.CreateByteArray
  Public MustOverride Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) Implements ITargCCCollection.LoadByteArray
  Public MustOverride Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.CreateJSON
  Public MustOverride Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.LoadJSON

  Public MustOverride Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.LoadLookupAndEnumText

  Public MustOverride Function isEqual(vEntitiesToTest As ITargCCCollection) As Boolean Implements ITargCCCollection.isEqual

  Public MustOverride Function CloneTargCCCollection() As ITargCCCollection Implements ITargCCCollection.CloneTargCCCollection

  Public MustOverride Function FindByPrimaryKey(vPrimaryKey As Long) As DataController.ITargCCEntity Implements ITargCCCollection.FindByPrimaryKey

  Public MustOverride Function LoadMeIntoDataTable(vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault Implements ITargCCCollection.LoadMeIntoDataTable

  Public MustOverride Sub SortByPrimaryKey() Implements ITargCCCollection.SortByPrimaryKey

End Class
