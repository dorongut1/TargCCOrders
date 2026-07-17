Namespace vbReport
  ''' Defines a column into whch text can be rendered on a line
  ''' of a table when the <see cref="T:vbReport.ReportDocument" />
  ''' is bound to a datasource.
  Public Class ReportColumn
    ''' <summary>
    ''' Defines the human-readable name of the column. This value
    ''' can be useful for generating descriptive headers.
    ''' </summary>
    Private _MappingName As String
    Private _HeaderText As String
    Private _FormatCode As String = ""
    Private _Alignment As CellTextJustification = CellTextJustification.Near

    Public Property MappingName() As String
      Get
        Return _MappingName
      End Get
      Set(ByVal Value As String)
        _MappingName = Value
        If _HeaderText = "" Then
          _HeaderText = _MappingName
        End If
      End Set
    End Property
    Public Property HeaderText() As String
      Get
        Return _HeaderText
      End Get
      Set(ByVal Value As String)
        _HeaderText = Value
        If _MappingName = "" Then
          _MappingName = _HeaderText
        End If
      End Set
    End Property
    Public Property FormatCode() As String
      Get
        Return _FormatCode
      End Get
      Set(ByVal Value As String)
        _FormatCode = Value
      End Set
    End Property
    Public Property Alignment() As CellTextJustification
      Get
        Return _Alignment
      End Get
      Set(ByVal Value As CellTextJustification)
        _Alignment = Value
      End Set
    End Property
    ''' <summary>
    ''' Contains the name of the field within the data source that
    ''' contains the data. This value is used to retrieve the data
    ''' value from the data source. It corresponds to the column
    ''' name in a DataTable, or a property name of an object.
    ''' </summary>
    Public Field As String
    ''' <summary>
    ''' Defines the horizontal start location (X coordinate) of the
    ''' column. When text is written to the column by the 
    ''' <see cref="M:vbReport.ReportPageEventArgs.WriteColumn(System.String,vbReport.ReportColumn)" /> method
    ''' it is rendered starting at this horizontal location.
    ''' </summary>
    Friend Left As Integer
    ''' <summary>
    ''' Defines the width of the column. Before text is written to the 
    ''' column by the 
    ''' <see cref="M:vbReport.ReportPageEventArgs.WriteColumn(System.String,vbReport.ReportColumn)" /> method
    ''' the column is filled with a white rectangle defined by the width
    ''' of the column. This helps prevent text from overwriting other
    ''' text within our columns.
    ''' </summary>
    Friend Width As Integer
  End Class
End Namespace