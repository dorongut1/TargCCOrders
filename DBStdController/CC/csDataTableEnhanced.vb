Public Class DataTableEnhanced
  Inherits DataTable

  Private _Title As String
  Private _SubTitle As String
  Private _PictureFileName As String
  Private _PictureBytes As Byte()
  Private _ShowTableHeaders As Boolean
  Private _FilterColumns As List(Of DataColumEnhanced)
  Private _DataColumnsEnhanced As List(Of DataColumEnhanced)

  Public ReadOnly Property Title As String
    Get
      Return _Title
    End Get
  End Property
  Public ReadOnly Property SubTitle As String
    Get
      Return _SubTitle
    End Get
  End Property
  Public ReadOnly Property PictureFileName As String
    Get
      Return _PictureFileName
    End Get
  End Property
  Public ReadOnly Property PictureBytes As Byte()
    Get
      Return _PictureBytes
    End Get
  End Property
  Public ReadOnly Property ShowTableHeaders As Boolean
    Get
      Return _ShowTableHeaders
    End Get
  End Property
  ''' <summary>
  ''' A list of columns acting as filters, in order of ordinal preference
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property FilterColumns As List(Of DataColumEnhanced)
    Get
      Return _FilterColumns
    End Get
  End Property
  Public ReadOnly Property DataColumnsEnhanced As List(Of DataColumEnhanced)
    Get
      Return _DataColumnsEnhanced
    End Get
  End Property


  Public Sub New()
    MyBase.New()

    CreateEmpty()
  End Sub

  Friend Function SetTitle(vTitle As String, vSubTitle As String, vPictureFleName As String, vSubFolder As String) As String
    Dim pFault As String

    Try
      _Title = vTitle.Trim
      _SubTitle = vSubTitle.Trim
      _PictureFileName = vPictureFleName.Trim
      pFault = "OK"
    Catch ex As Exception
      pFault = ex.ToString
    End Try

    If _PictureFileName.Length > 0 Then
      _PictureFileName = _PictureFileName.ToLowerInvariant()
      If Not (_PictureFileName.EndsWith(".jpg") OrElse _PictureFileName.EndsWith(".gif") OrElse _PictureFileName.EndsWith(".png")) Then
        Return "PictureFleName must end with 'jpg', 'gif' or 'png'"
      End If
      Try
        Dim pSubFolder As String = ""
        If String.IsNullOrEmpty(vSubFolder) Then pSubFolder = "" Else pSubFolder = vSubFolder & "\"

        'Handles a situation where in development there are many options, but in production only one is used, and is put in the root
        If Not String.IsNullOrEmpty(vSubFolder) AndAlso Not IO.Directory.Exists(MyController.UploadedFilesRootFolder & pSubFolder) Then
          pSubFolder = ""
        End If

        Dim pBytes As Byte() = IO.File.ReadAllBytes(MyController.UploadedFilesRootFolder & pSubFolder & _PictureFileName)
        _PictureBytes = ccHelper.ResizeBitmap(pBytes, New Drawing.Size(1000, 71), False)
      Catch ex As Exception
        Return ex.ToString
      End Try
    Else
      _PictureBytes = Nothing
    End If

    Return pFault
  End Function


  ''' <summary>   
  ''' Sets 1st Column as key and sets padding for numerical fields   
  ''' </summary>   
  ''' <remarks></remarks>   
  Public Function SetUpTable() As String
    Dim pResponse As String = ""

    pResponse = Set1stColumnAsKey()
    If pResponse <> "OK" Then Return pResponse

    pResponse = SetPadding()
    If pResponse <> "OK" Then Return pResponse

    pResponse = SetUpFilterColumns()
    If pResponse <> "OK" Then Return pResponse

    pResponse = SetUpDataColumnsEnhanced()
    If pResponse <> "OK" Then Return pResponse

    Return pResponse
  End Function
  Private Function Set1stColumnAsKey() As String
    Dim pResponse As String = ""

    'Check that the 1st Column is OK   
    Dim pCol As DataColumEnhanced = CType(Me.Columns(0), DataColumEnhanced)
    If Not (pCol.ColumnName = "Column1" OrElse pCol.ColumnName = "Key") Then
      pResponse = "The 1st column is the key. It should be blank or called 'Key'"
    Else
      pResponse = "OK"
    End If

    pCol.ColumnName = "Key"
    pCol.Caption = "Key"

    If pResponse = "OK" Then
      Try
        pCol.Unique = True
        pCol.SetFieldType(DataColumEnhanced.enmFieldType.String)
        pCol.SetIsVisible(False)
        Dim pKeys(1) As DataColumEnhanced
        pKeys(0) = pCol
        Me.PrimaryKey = pKeys
        pCol.SetIsKey(True)
        pResponse = "OK"
      Catch ex As Exception
        pResponse = ex.ToString
      End Try
    End If

    Return pResponse
  End Function
  Private Function SetPadding() As String
    Dim pResponse As String = ""


    'check that all the fields are string   
    For Each pCol As DataColumEnhanced In Me.Columns
      If pCol.FieldType = DataColumEnhanced.enmFieldType.UD Then
        pResponse = "This is field type is not defined. It must be defined!. Check '" & pCol.ColumnName & "'"
        Return pResponse
      End If
      If pCol.FieldType = DataColumEnhanced.enmFieldType.String Then 'This makes Numeric & String both Numeric 
        pCol.SetFieldType(DataColumEnhanced.enmFieldType.Numeric)
      End If
      If pCol.DataType <> System.Type.GetType("System.String") Then
        If pCol.FieldType <> DataColumEnhanced.enmFieldType.PictureBytes Then
          pResponse = "This is a generic grid. All columns must be string (even numbers). Do not override. Check '" & pCol.ColumnName & "'"
          Return pResponse
        End If
      End If
    Next

    Try
      'Initialize   
      For Each pCol As DataColumEnhanced In Me.Columns
        pCol.SetPadding(0)
        If pCol.FieldType = DataColumEnhanced.enmFieldType.Numeric Then
          For Each pRow As DataRow In Me.Rows
            Dim pValue As String = pRow(pCol).ToString.Trim
            If pValue <> "" AndAlso ccHelper.IsNumeric(pValue) = False Then
              'Make it s string and exit 
              pCol.SetFieldType(DataColumEnhanced.enmFieldType.String)
              pCol.SetPadding(0)
              Exit For
            End If
            'Calculate padding 
            If pCol.IsKey = True Then Continue For 'only wanted to see if numeric or string 
            Dim pWidth As Integer = pValue.Length
            If pWidth > pCol.Padding Then
              pCol.SetPadding(pWidth)
            End If
          Next
        End If
      Next

      'Set padding   
      For Each pCol As DataColumEnhanced In Me.Columns
        If pCol.IsKey = True Then Continue For 'do not set padding on key field 
        If pCol.FieldType = DataColumEnhanced.enmFieldType.Numeric Then
          pCol.ReadOnly = False
          For Each pRow As DataRow In Me.Rows
            Dim pValue As String = pRow(pCol).ToString.Trim
            pValue = pValue.PadLeft(pCol.Padding)
            pRow(pCol) = pValue
          Next
          pCol.ReadOnly = True
        End If
      Next
      pResponse = "OK"
    Catch ex As Exception
      pResponse = ex.ToString
    End Try

    Return pResponse
  End Function
  Private Function SetUpFilterColumns() As String
    Dim pFilters As New List(Of DataColumEnhanced)
    Dim pMaxFilter As Integer = 0
    _FilterColumns = New List(Of DataColumEnhanced)

    'scan the columns and see if we have any filters and get the max
    For Each pCol As DataColumEnhanced In Me.Columns
      If pCol.FilterOrdinal IsNot Nothing Then
        pFilters.Add(pCol)
        If pMaxFilter < pCol.FilterOrdinal Then
          pMaxFilter = pCol.FilterOrdinal.Value
        End If
      End If
    Next

    If pFilters.Count = 0 Then
      _FilterColumns = New List(Of DataColumEnhanced)
      Return "OK"
    End If

    Dim Filters(pMaxFilter - 1) As String

    For Each pCol As DataColumEnhanced In pFilters
      Dim pIndex As Integer = pCol.FilterOrdinal.Value - 1
      If Filters(pIndex) <> "" Then
        Return "There is more than one filter with an ordinal of " & pIndex + 1
      End If
      Filters(pIndex) = pCol.ColumnName
    Next

    'Now check that there are no holes
    'also load the list
    For pindex = 0 To pMaxFilter - 1
      If Filters(pindex) = "" Then
        _FilterColumns = New List(Of DataColumEnhanced)
        Return "There is no filter defined for Ordinal " & pindex + 1 & ". There must be no holes"
      End If
      _FilterColumns.Add(CType(Me.Columns(Filters(pindex)), DataColumEnhanced))
    Next

    Return "OK"
  End Function
  Private Function SetUpDataColumnsEnhanced() As String
    _DataColumnsEnhanced = New List(Of DataColumEnhanced)

    'scan the columns and see if we have any filters and get the max
    For Each pCol As DataColumEnhanced In Me.Columns
      _DataColumnsEnhanced.Add(pCol)
    Next

    Return "OK"
  End Function

  Friend Sub SetShowTableHeaders(vShowTableHeaders As Boolean)
    _ShowTableHeaders = vShowTableHeaders
  End Sub

  Private Sub CreateEmpty()

    _Title = ""
    _SubTitle = ""
    _PictureFileName = ""
    _PictureBytes = Nothing
    _ShowTableHeaders = False
  End Sub
End Class

Public Class DataColumEnhanced
  Inherits DataColumn

  Public Enum enmAlignment
    UD
    Left
    Center
    Right
  End Enum

  Public Enum enmFieldType
    UD
    Numeric
    [String]
    PictureFileName
    PictureBytes
  End Enum

  Private _Alignment As enmAlignment
  Public ReadOnly Property Alignment As enmAlignment
    Get
      Return _Alignment
    End Get
  End Property

  Private _IsKey As Boolean
  Public ReadOnly Property IsKey As Boolean
    Get
      Return _IsKey
    End Get
  End Property

  Private _IsVisible As Boolean
  ''' <summary>
  ''' The default is 'True'
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property IsVisible As Boolean
    Get
      Return _IsVisible
    End Get
  End Property

  Private _FilterOrdinal As Nullable(Of Integer)
  ''' <summary>
  ''' If the column is used as a filter, set the order (1 based). If not, set it to Nothing
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property FilterOrdinal As Nullable(Of Integer)
    Get
      Return _FilterOrdinal
    End Get
  End Property

  Private _FieldType As enmFieldType
  Public ReadOnly Property FieldType As enmFieldType
    Get
      Return _FieldType
    End Get
  End Property

  Private _Padding As Integer
  Friend ReadOnly Property Padding As Integer
    Get
      Return _Padding
    End Get
  End Property

  Public Sub New()
    MyBase.New()
    CreateEmpty()

    Me.DataType = System.Type.GetType("System.String")
    _FieldType = enmFieldType.String
    Me.ReadOnly = True
  End Sub

  Public Sub SetAlignment(ByVal vAlignment As enmAlignment)
    _Alignment = vAlignment
  End Sub

  Friend Sub SetPadding(ByVal vPadding As Integer)
    _Padding = vPadding
  End Sub

  Friend Sub SetFieldType(ByVal vFieldType As enmFieldType)
    _FieldType = vFieldType
    _Padding = 0
    If _FieldType = enmFieldType.PictureBytes Then
      Me.DataType = System.Type.GetType("System.Byte[]")
    End If
  End Sub

  Friend Sub SetIsKey(ByVal vIsKey As Boolean)
    _IsKey = vIsKey
  End Sub

  Friend Sub SetIsVisible(ByVal vIsVisible As Boolean)
    _IsVisible = vIsVisible
  End Sub

  ''' <summary>
  ''' If the column is used as a filter, set the order (1 based). If not, set it to Nothing
  ''' </summary>
  ''' <param name="vFilterOrdinal"></param>
  ''' <remarks></remarks>
  Friend Sub SetFilterOrdinal(ByVal vFilterOrdinal As Nullable(Of Integer))
    _FilterOrdinal = vFilterOrdinal
  End Sub

  Private Sub CreateEmpty()
    _Alignment = enmAlignment.UD
    _IsKey = False
    _IsVisible = True
    _FilterOrdinal = Nothing
    _FieldType = enmFieldType.UD
    _Padding = 0
  End Sub
End Class