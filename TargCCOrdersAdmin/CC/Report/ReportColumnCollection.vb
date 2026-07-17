Namespace vbReport
  ''' <summary>
  ''' Defines a strongly-typed collection that contains
  ''' <see cref="T:vbReport.ReportColumn" /> objects.
  ''' </summary>
  Public Class ReportColumnCollection
    Inherits CollectionBase

    ''' <summary>
    ''' Returns a specific <see cref="T:vbReport.ReportColumn" /> object
    ''' from the collection.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <value>A specific column object.</value>
    Default Public ReadOnly Property Item(ByVal index As Integer) As ReportColumn
      Get
        Return CType(list(index), ReportColumn)
      End Get
    End Property

    ''' <summary>
    ''' Adds a <see cref="T:vbReport.ReportColumn" /> object
    ''' to the collection.
    ''' </summary>
    ''' <param name="column">A column object.</param>
    Public Sub Add(ByVal column As ReportColumn)
      list.Add(column)
    End Sub

    ''' <summary>
    ''' Adds a <see cref="T:vbReport.ReportColumn" /> object
    ''' to the collection based on a field name. The Name and Field
    ''' of the column are set to the provided field name. The 
    ''' Left and Width values are 0 and must be set separately.
    ''' </summary>
    Public Sub Add(ByVal MappingName As String)
      Add(MappingName, MappingName, "", CellTextJustification.Near)
    End Sub

    Public Sub Add(ByVal MappingName As String, ByVal HeaderText As String, ByVal FormatCode As String, ByVal Alignment As CellTextJustification)
      Dim col As New ReportColumn
      col.MappingName = MappingName
      col.Field = MappingName
      col.HeaderText = HeaderText
      col.FormatCode = FormatCode
      col.Alignment = Alignment
      Add(col)
    End Sub

    Public Sub Add(ByVal MappingName As String, ByVal HeaderText As String, ByVal FormatCode As String, ByVal Width As Integer, ByVal Alignment As CellTextJustification)
      Dim col As New ReportColumn
      col.MappingName = MappingName
      col.Field = MappingName
      col.HeaderText = HeaderText
      col.FormatCode = FormatCode
      col.Alignment = Alignment
      col.Width = Width
      Add(col)
      SetLefts()
    End Sub

    Public Sub Add(ByVal MappingName As String, ByVal HeaderText As String, ByVal Alignment As CellTextJustification)
      Add(MappingName, HeaderText, "", Alignment)
    End Sub

    ''' <summary>
    ''' Removes the specified column object from the collection.
    ''' </summary>
    ''' <param name="column">A column object.</param>
    Public Sub Remove(ByVal column As ReportColumn)
      list.Remove(column)
    End Sub

    ''' <summary>
    ''' Called by the data binding mechanism to automatically run
    ''' through all the columns defined by this collection and to
    ''' set their widths to evenly consume all the horizontal space
    ''' on a line.
    ''' </summary>
    ''' <param name="Width">The total width of a printed line.</param>
    Public Sub SetEvenSpacing(ByVal Width As Integer)

      Dim space As Integer = ccHelper.ToInteger(Width / list.Count)
      Dim index As Integer

      For index = 0 To list.Count - 1
        With CType(list(index), ReportColumn)
          .Left = space * index
          .Width = space
        End With
      Next

    End Sub

    Private Sub SetLefts()

      Dim Left As Integer = 0
      Dim index As Integer

      For index = 0 To list.Count - 1
        With CType(list(index), ReportColumn)
          .Left = Left
          Left += .Width
        End With
      Next

    End Sub

  End Class

  Public Class clsSummary
    Private _SummaryText As String
    Private _SummaryCol As Integer
    Private _CountText As String
    Private _CountAmount As String
    Private _CountCol As Integer
    Private _TotalText As String
    Private _TotalValCol As Integer
    Private _TotalTitleCol As Integer
    Private _TotalItemsTotalText As String
    Private _TotalItemsTotalAmount As String
    Private _TotalVATRoundingText As String
    Private _TotalVATRoundingAmount As String
    Private _TotalSubTotalText As String
    Private _TotalSubTotalAmount As String
    Private _TotalVATText As String
    Private _TotalVATAmount As String
    Private _TotalTotalText As String
    Private _TotalTotalAmount As String

    Public Property SummaryText() As String
      Get
        Return _SummaryText
      End Get
      Set(ByVal Value As String)
        _SummaryText = Value
      End Set
    End Property
    Public Property SummaryCol() As Integer
      Get
        Return _SummaryCol
      End Get
      Set(ByVal Value As Integer)
        _SummaryCol = Value
      End Set
    End Property
    Public Property CountText() As String
      Get
        Return _CountText
      End Get
      Set(ByVal Value As String)
        _CountText = Value
      End Set
    End Property
    Public Property CountAmount() As String
      Get
        Return _CountAmount
      End Get
      Set(ByVal Value As String)
        _CountAmount = Value
      End Set
    End Property
    Public Property CountCol() As Integer
      Get
        Return _CountCol
      End Get
      Set(ByVal Value As Integer)
        _CountCol = Value
      End Set
    End Property
    Public Property TotalText() As String
      Get
        Return _TotalText
      End Get
      Set(ByVal Value As String)
        _TotalText = Value
      End Set
    End Property
    Public Property TotalValCol() As Integer
      Get
        Return _TotalValCol
      End Get
      Set(ByVal Value As Integer)
        _TotalValCol = Value
      End Set
    End Property
    Public Property TotalTitleCol() As Integer
      Get
        Return _TotalTitleCol
      End Get
      Set(ByVal Value As Integer)
        _TotalTitleCol = Value
      End Set
    End Property
    Public Property TotalItemsTotalText() As String
      Get
        Return _TotalItemsTotalText
      End Get
      Set(ByVal Value As String)
        _TotalItemsTotalText = Value
      End Set
    End Property
    Public Property TotalItemsTotalAmount() As String
      Get
        Return _TotalItemsTotalAmount
      End Get
      Set(ByVal Value As String)
        _TotalItemsTotalAmount = Value
      End Set
    End Property
    Public Property TotalVATRoundingText() As String
      Get
        Return _TotalVATRoundingText
      End Get
      Set(ByVal Value As String)
        _TotalVATRoundingText = Value
      End Set
    End Property
    Public Property TotalVATRoundingAmount() As String
      Get
        Return _TotalVATRoundingAmount
      End Get
      Set(ByVal Value As String)
        _TotalVATRoundingAmount = Value
      End Set
    End Property
    Public Property TotalSubTotalText() As String
      Get
        Return _TotalSubTotalText
      End Get
      Set(ByVal Value As String)
        _TotalSubTotalText = Value
      End Set
    End Property
    Public Property TotalSubTotalAmount() As String
      Get
        Return _TotalSubTotalAmount
      End Get
      Set(ByVal Value As String)
        _TotalSubTotalAmount = Value
      End Set
    End Property
    Public Property TotalVATText() As String
      Get
        Return _TotalVATText
      End Get
      Set(ByVal Value As String)
        _TotalVATText = Value
      End Set
    End Property
    Public Property TotalVATAmount() As String
      Get
        Return _TotalVATAmount
      End Get
      Set(ByVal Value As String)
        _TotalVATAmount = Value
      End Set
    End Property
    Public Property TotalTotalText() As String
      Get
        Return _TotalTotalText
      End Get
      Set(ByVal Value As String)
        _TotalTotalText = Value
      End Set
    End Property
    Public Property TotalTotalAmount() As String
      Get
        Return _TotalTotalAmount
      End Get
      Set(ByVal Value As String)
        _TotalTotalAmount = Value
      End Set
    End Property

    Public Sub New()
      CreateEmpty()
    End Sub

    Private Sub CreateEmpty()
      _SummaryText = ""
      _SummaryCol = 0
      _CountText = ""
      _CountAmount = ""
      _CountCol = 0
      _TotalText = ""
      _TotalValCol = 0
      _TotalTitleCol = 0
      _TotalItemsTotalText = ""
      _TotalItemsTotalAmount = ""
      _TotalVATRoundingText = ""
      _TotalVATRoundingAmount = ""
      _TotalSubTotalText = ""
      _TotalSubTotalAmount = ""
      _TotalVATText = ""
      _TotalVATAmount = ""
      _TotalTotalText = ""
      _TotalTotalAmount = ""
    End Sub
  End Class
End Namespace