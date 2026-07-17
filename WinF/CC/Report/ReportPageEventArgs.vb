Imports System.Drawing
Imports System.Drawing.Printing

Namespace vbReport
  ''' <summary>
  ''' This is a list of the possible text justification values
  ''' used by the 
  ''' <see cref="M:vbReport.ReportPageEventArgs.Write(System.String,vbReport.ReportLineJustification)" />
  ''' and
  ''' <see cref="M:vbReport.ReportPageEventArgs.WriteLine(System.String,vbReport.ReportLineJustification)" />
  ''' methods.
  ''' </summary>
  Public Enum ReportLineJustification
    Left = 0
    Centered = 1
    Right = 2
  End Enum

  Public Enum CellTextJustification
    Near = 0
    Centered = 1
    Far = 2
    AlwaysRight = 3
  End Enum


  ''' <summary>
  ''' The ReportPageEventArgs the type of the parameter provided by
  ''' the events raised from the <see cref="T:vbReport.ReportDocument" /> 
  ''' object. This class includes methods to simplify the process of
  ''' rendering text output into each page of the report.
  ''' </summary>
  Public Class ReportPageEventArgs
    Inherits PrintPageEventArgs

    Private mFont As Font
    Private mRTL As Boolean
    Private mBrush As Brush
    Private mPageNumber As Integer
    Private mX As Integer
    Private mY As Integer
    Private mFooterLines As Integer
    Private mLineHeight As Integer
    Private mPageBottom As Integer

    Friend Sub New(ByVal e As PrintPageEventArgs,
      ByVal pageNumber As Integer, ByVal font As Font,
      ByVal brush As Brush, ByVal footerLines As Integer, ByVal RTL As Boolean)

      MyBase.New(e.Graphics, e.MarginBounds, e.PageBounds, e.PageSettings)
      mPageNumber = pageNumber
      mFont = font
      mBrush = brush
      mRTL = RTL
      PositionToStart()
      mFooterLines = footerLines

      mLineHeight = ccHelper.ToInteger(mFont.GetHeight(Graphics))
      mPageBottom = MarginBounds.Bottom - mFooterLines * mLineHeight - mLineHeight

    End Sub

    ''' <summary>
    ''' Writes some text to the report starting at the current cursor location.
    ''' The cursor is moved to the right, but not down to the next line.
    ''' </summary>
    ''' <param name="Text">The text to render.</param>
    Public Sub Write(ByVal Text As String)

      If mRTL = False Then
        Graphics.DrawString(Text, mFont, mBrush, mX, mY)
        mX += ccHelper.ToInteger(Graphics.MeasureString(Text, mFont).Width)
      Else
        mX -= ccHelper.ToInteger(Graphics.MeasureString(Text, mFont).Width)
        Graphics.DrawString(Text, mFont, mBrush, mX, mY)
      End If

    End Sub

    Public Sub Write(ByVal Text As String, ByVal Bold As Boolean, ByVal Bigger As Boolean)
      Dim NewSize As Single

      NewSize = mFont.SizeInPoints
      If Bigger = True Then
        NewSize += 5
      End If

      Dim nfont As Font
      If Bold = True Then
        nfont = New Font(mFont.FontFamily, NewSize, FontStyle.Bold)
      Else
        nfont = New Font(mFont.FontFamily, NewSize, FontStyle.Regular)
      End If

      If mRTL = False Then
        Graphics.DrawString(Text, nfont, mBrush, mX, mY)
        mX += ccHelper.ToInteger(Graphics.MeasureString(Text, nfont).Width)
      Else
        mX -= ccHelper.ToInteger(Graphics.MeasureString(Text, nfont).Width)
        Graphics.DrawString(Text, nfont, mBrush, mX, mY)
      End If

    End Sub

    ''' <summary>
    ''' Writes text to the report on the current line, but justified based on
    ''' the justification parameter value. 
    ''' The cursor is moved to the right, but not down to the next line.
    ''' </summary>
    ''' <param name="Text">The text to render.</param>
    ''' <param name="Justification">Indicates the justification for the text.</param>
    Public Sub Write(ByVal Text As String, ByVal Justification As ReportLineJustification)

      If mRTL = False Then
        Select Case Justification
          Case ReportLineJustification.Left
            mX = MarginBounds.Left

          Case ReportLineJustification.Centered
            mX = MarginBounds.Left + ccHelper.ToInteger(MarginBounds.Width / 2 - Graphics.MeasureString(Text, mFont).Width / 2)

          Case ReportLineJustification.Right
            mX = ccHelper.ToInteger(MarginBounds.Right - Graphics.MeasureString(Text, mFont).Width)

        End Select
      Else
        Select Case Justification
          Case ReportLineJustification.Left
            mX = MarginBounds.Right

          Case ReportLineJustification.Centered
            mX = MarginBounds.Right - ccHelper.ToInteger(MarginBounds.Width / 2 - Graphics.MeasureString(Text, mFont).Width / 2)

          Case ReportLineJustification.Right
            mX = ccHelper.ToInteger(MarginBounds.Left + Graphics.MeasureString(Text, mFont).Width)

        End Select
      End If
      Write(Text)

    End Sub

    Public Sub Write(ByVal Text As String, ByVal Justification As ReportLineJustification, ByVal Bold As Boolean, ByVal Bigger As Boolean)
      Dim NewSize As Single

      NewSize = mFont.SizeInPoints
      If Bigger = True Then
        NewSize += 5
      End If

      Dim nfont As Font
      If Bold = True Then
        nfont = New Font(mFont.FontFamily, NewSize, FontStyle.Bold)
      Else
        nfont = New Font(mFont.FontFamily, NewSize, FontStyle.Regular)
      End If

      If mRTL = False Then
        Select Case Justification
          Case ReportLineJustification.Left
            mX = MarginBounds.Left

          Case ReportLineJustification.Centered
            mX = MarginBounds.Left + ccHelper.ToInteger(MarginBounds.Width / 2 - Graphics.MeasureString(Text, nfont).Width / 2)

          Case ReportLineJustification.Right
            mX = ccHelper.ToInteger(MarginBounds.Right - Graphics.MeasureString(Text, nfont).Width)

        End Select
      Else
        Select Case Justification
          Case ReportLineJustification.Right
            mX = MarginBounds.Left

          Case ReportLineJustification.Centered
            mX = MarginBounds.Right - ccHelper.ToInteger(MarginBounds.Width / 2 - Graphics.MeasureString(Text, nfont).Width / 2)

          Case ReportLineJustification.Left
            mX = ccHelper.ToInteger(MarginBounds.Right - Graphics.MeasureString(Text, nfont).Width)

        End Select
      End If
      Write(Text, Bold, Bigger)

    End Sub



    ''' <summary>
    ''' This method writes text into a specific column within the report on
    ''' the current line. It uses a <see cref="T:vbReport.ReportColumn" />
    ''' object to define the X position and width of the column. The cursor
    ''' is not moved by calling this method.
    ''' </summary>
    ''' <param name="Text">The text to render into the column.</param>
    ''' <param name="column">The <see cref="T:vbReport.ReportColumn" /> object defining this column.</param>
    Public Sub WriteColumn(ByVal Text As String, ByVal column As ReportColumn, ByVal Bold As Boolean, ByVal Bordered As Boolean)
      Dim x As Integer

      If mRTL = False Then
        x = MarginBounds.Left + column.Left
      Else
        x = MarginBounds.Right - column.Left - column.Width
      End If

      Dim rectP As New RectangleF(x, mY, column.Width - 5, mLineHeight)
      Dim rectF As New RectangleF(x - 5, mY, column.Width + 5, mLineHeight)
      Graphics.FillRectangle(Brushes.White, rectF)

      Dim sf As StringFormat = StringFormat.GenericDefault

      Dim nFont As Font
      If Bold = True Then
        nFont = New Font(mFont.FontFamily, mFont.SizeInPoints, FontStyle.Bold)
      Else
        nFont = mFont
      End If

      'Text = "The rain in Spain falls mainly in the plain. It is also very warm there..."

      'now handle justification
      If mRTL = False Then
        If column.Alignment = CellTextJustification.Near Then
          sf.Alignment = StringAlignment.Near
        ElseIf column.Alignment = CellTextJustification.Centered Then
          'x = ccHelper.ToInteger(x + (column.Width - Graphics.MeasureString(Text, nFont).Width) / 2) - 5
          sf.Alignment = StringAlignment.Center
        ElseIf column.Alignment = CellTextJustification.Far Then
          'x = ccHelper.ToInteger(x + column.Width - Graphics.MeasureString(Text, nFont).Width) - 5
          sf.Alignment = StringAlignment.Far
        End If
      Else
        If column.Alignment = CellTextJustification.Far Then
          sf.Alignment = StringAlignment.Near
        ElseIf column.Alignment = CellTextJustification.Centered Then
          'x = ccHelper.ToInteger(x + (column.Width - Graphics.MeasureString(Text, nFont).Width) / 2) - 5
          sf.Alignment = StringAlignment.Center
        ElseIf column.Alignment = CellTextJustification.Near Then
          'x = ccHelper.ToInteger(x + column.Width - Graphics.MeasureString(Text, nFont).Width) - 5
          sf.Alignment = StringAlignment.Far
        End If
      End If
      If column.Alignment = CellTextJustification.AlwaysRight Then
        sf.Alignment = StringAlignment.Far
      End If

      Graphics.DrawString(Text, nFont, mBrush, rectP, sf)
      If Bordered = True Then
        Graphics.DrawRectangle(Pens.Black, Rectangle.Truncate(rectF))
      End If

    End Sub

    Public Sub WriteColumn(ByVal Text As String, ByVal column As ReportColumn, ByVal Bordered As Boolean)
      WriteColumn(Text, column, False, Bordered)
    End Sub

    ''' <summary>
    ''' Moves the cursor down one line and to the left side of the page.
    ''' </summary>
    Public Sub WriteLine()

      If mRTL = False Then
        mX = MarginBounds.Left
        mY += mLineHeight
      Else
        mX = MarginBounds.Right
        mY += mLineHeight
      End If

    End Sub

    ''' <summary>
    ''' Writes text to the report starting at the current cursor location and 
    ''' then moves the cursor down one line and to the left side of the page.
    ''' </summary>
    ''' <param name="Text">The text to render.</param>
    Public Sub WriteLine(ByVal Text As String)

      Write(Text)
      'Graphics.DrawString(Text, mFont, mBrush, mX, mY)
      WriteLine()

    End Sub

    ''' <summary>
    ''' Writes text to the report on the current line, but justified based on
    ''' the justification parameter value. 
    ''' The cursor is moved to the right, but not down to the next line.
    ''' </summary>
    ''' <param name="Text">The text to render.</param>
    ''' <param name="Justification">Indicates the justification for the text.</param>
    Public Sub WriteLine(ByVal Text As String, ByVal Justification As ReportLineJustification)
      Write(Text, Justification)
      WriteLine()
    End Sub

    Public Sub WriteLine(ByVal Text As String, ByVal Justification As ReportLineJustification, ByVal Bold As Boolean, ByVal Bigger As Boolean)
      Write(Text, Justification, Bold, Bigger)
      WriteLine()
      If Bold = True Then
        mY += ccHelper.ToInteger(mLineHeight / 2)
      End If
    End Sub

    ''' <summary>
    ''' Draws a horizontal line across the width of the page on the current
    ''' line. After the line is drawn the cursor is moved down one line and
    ''' to the left side of the page.
    ''' </summary>
    Public Sub HorizontalRule()

      Dim y As Integer = mY + ccHelper.ToInteger(mLineHeight / 2)

      Graphics.DrawLine(Pens.Black, MarginBounds.Left, y, MarginBounds.Right, y)
      WriteLine()

    End Sub

    ''' <summary>
    ''' Sets or returns the current X position (left to right) of the
    ''' cursor on the page.
    ''' </summary>
    ''' <value>The horizontal position of the cursor.</value>
    Public Property CurrentX() As Integer
      Get
        Return mX
      End Get
      Set(ByVal Value As Integer)
        mY = Value
      End Set
    End Property

    ''' <summary>
    ''' Sets or returns the current Y position (top to bottom) of the
    ''' cursor on the page.
    ''' </summary>
    ''' <value>The vertical position of the cursor.</value>
    Public Property CurrentY() As Integer
      Get
        Return mY
      End Get
      Set(ByVal Value As Integer)
        mY = Value
      End Set
    End Property

    ''' <summary>
    ''' Moves the cursor to the top left corner of the page.
    ''' </summary>
    Public Sub PositionToStart()

      If mRTL = False Then
        mX = MarginBounds.Left
      Else
        mX = MarginBounds.Right
      End If
      mY = MarginBounds.Top

    End Sub

    ''' <summary>
    ''' Returns the Y value corresponding to the bottom of the page
    ''' body. This is the position immediately above the start of the 
    ''' page footer.
    ''' </summary>
    ''' <value>The Y value of the bottom of the page.</value>
    Public ReadOnly Property PageBottom() As Integer
      Get
        Return mPageBottom + mLineHeight
      End Get
    End Property

    ''' <summary>
    ''' Returns True if the cursor's current location is beyond the bottom of
    ''' the page body. This doesn't mean we're into the bottom margin, but may
    ''' indicate that the cursor in the page's footer region.
    ''' </summary>
    ''' <value>A Boolean indicating whether the cursor is past the end of the page.</value>
    Public ReadOnly Property EndOfPage() As Boolean
      Get
        Return mY >= mPageBottom
      End Get
    End Property

    ''' <summary>
    ''' Returns the page number of the current page. This value is automatically
    ''' incremented as each new page is rendered.
    ''' </summary>
    ''' <value>The current page number.</value>
    Public ReadOnly Property PageNumber() As Integer
      Get
        Return mPageNumber
      End Get
    End Property

  End Class
End Namespace