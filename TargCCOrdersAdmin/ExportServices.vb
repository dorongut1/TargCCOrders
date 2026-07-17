Imports System.Text
Imports System.IO
Imports TargCCOrders.DataController.clsEnums

''' <summary>
''' ממשק לייצוא נתונים לפורמטים שונים
''' </summary>
Public Interface IExportService
  Function Export(dgvGrid As DataGridView, fileName As String) As clsFault
End Interface

''' <summary>
''' מודול עזר משותף לפונקציות ייצוא
''' </summary>
Public Module ExportHelper

  Public Function TranslateColumnHeader(columnName As String) As String
    Select Case columnName.ToLower()
      Case "id" : Return "קוד"
      Case "ordernumber" : Return "מספר הזמנה"
      Case "customerid" : Return "קוד לקוח"
      Case "customername" : Return "שם לקוח"
      Case "orderdate" : Return "תאריך הזמנה"
      Case "totalamount" : Return "סכום לפני מע""מ"
      Case "vatamount" : Return "מע""מ"
      Case "totalwithvat" : Return "סה""כ כולל מע""מ"
      Case "enmpaymentstatus" : Return "סטטוס תשלום"
      Case "enmpaymentmethod" : Return "אמצעי תשלום"
      Case "paymentdate" : Return "תאריך תשלום"
      Case "invoicenumber" : Return "מספר חשבונית"
      Case "enmdeliverymethod" : Return "שיטת משלוח"
      Case "deliverydate" : Return "תאריך משלוח"
      Case "enmdeliveryday" : Return "יום משלוח"
      Case "enmorderstatus" : Return "סטטוס הזמנה"
      Case "notes" : Return "הערות"
      Case Else : Return columnName
    End Select
  End Function

  Public Function FormatCellValue(cell As DataGridViewCell) As String
    If cell.Value Is Nothing OrElse IsDBNull(cell.Value) Then
      Return ""
    End If

    ' Format dates
    If TypeOf cell.Value Is Date Then
      Return CDate(cell.Value).ToString("dd/MM/yyyy")
    End If

    ' Format numbers
    If TypeOf cell.Value Is Decimal OrElse TypeOf cell.Value Is Double Then
      Return CDec(cell.Value).ToString("N2")
    End If

    ' Format enums
    If cell.OwningColumn.Name.StartsWith("enm") Then
      Return TranslateEnumValue(cell.Value)
    End If

    Return cell.Value.ToString()
  End Function

  Public Function TranslateEnumValue(value As Object) As String
    If value Is Nothing Then Return ""

    Select Case value.GetType().Name
      Case "enmPaymentStatus"
        Select Case CType(value, enmPaymentStatus)
          Case enmPaymentStatus.Pending : Return "ממתין לתשלום"
          Case enmPaymentStatus.PartiallyPaid : Return "שולם חלקית"
          Case enmPaymentStatus.Paid : Return "שולם"
        End Select

      Case "enmPaymentMethod"
        Select Case CType(value, enmPaymentMethod)
          Case enmPaymentMethod.Cash : Return "מזומן"
          Case enmPaymentMethod.Credit : Return "אשראי"
          Case enmPaymentMethod.Transfer : Return "העברה בנקאית"
        End Select

      Case "enmDeliveryMethod"
        Select Case CType(value, enmDeliveryMethod)
          Case enmDeliveryMethod.Biobee : Return "ביובי"
          Case enmDeliveryMethod.Netzach : Return "נצח"
          Case enmDeliveryMethod.Tzofar : Return "צופר"
        End Select

      Case "enmOrderStatus"
        Select Case CType(value, enmOrderStatus)
          Case enmOrderStatus.New : Return "חדש"
          Case enmOrderStatus.Processing : Return "בתהליך"
          Case enmOrderStatus.Completed : Return "הושלם"
          Case enmOrderStatus.Cancelled : Return "בוטל"
        End Select
    End Select

    Return value.ToString()
  End Function

End Module

''' <summary>
''' שירות ייצוא ל-Excel (HTML format)
''' </summary>
Public Class ExcelExportService
  Implements IExportService

  Private _requester As clsRequester

  Public Sub New(requester As clsRequester)
    _requester = requester
  End Sub

  Public Function Export(dgvGrid As DataGridView, fileName As String) As clsFault Implements IExportService.Export
    Dim pFault As New clsFault()
    Try
      Dim html = GenerateHTML(dgvGrid)
      File.WriteAllText(fileName, html, Encoding.UTF8)
      Return pFault.SetOK()
    Catch ex As Exception
      pFault.LogException(ex, "Export", "TRGT-EXP-241225-4001", _requester)
      Return pFault
    End Try
  End Function

  Private Function GenerateHTML(dgvGrid As DataGridView) As String
    Dim sb As New StringBuilder()

    ' HTML Header
    sb.AppendLine("<!DOCTYPE html>")
    sb.AppendLine("<html>")
    sb.AppendLine("<head>")
    sb.AppendLine("<meta charset='UTF-8'>")
    sb.AppendLine("<title>Export Data</title>")
    sb.AppendLine(GetStyles())
    sb.AppendLine("</head>")
    sb.AppendLine("<body>")

    ' Title
    sb.AppendLine("<h1>דוח הזמנות - " & Date.Now.ToString("dd/MM/yyyy HH:mm") & "</h1>")

    ' Table
    sb.AppendLine("<table>")

    ' Headers
    sb.AppendLine("<thead>")
    sb.AppendLine("<tr>")
    For Each col As DataGridViewColumn In dgvGrid.Columns
      If col.Visible AndAlso Not TypeOf col Is DataGridViewButtonColumn Then
        sb.AppendLine("<th>" & ExportHelper.TranslateColumnHeader(col.Name) & "</th>")
      End If
    Next
    sb.AppendLine("</tr>")
    sb.AppendLine("</thead>")

    ' Body
    sb.AppendLine("<tbody>")
    For Each row As DataGridViewRow In dgvGrid.Rows
      If row.Visible AndAlso Not row.IsNewRow Then
        Dim rowClass = GetRowClass(row)
        sb.AppendLine("<tr class='" & rowClass & "'>")

        For Each col As DataGridViewColumn In dgvGrid.Columns
          If col.Visible AndAlso Not TypeOf col Is DataGridViewButtonColumn Then
            Dim cellValue = ExportHelper.FormatCellValue(row.Cells(col.Index))
            sb.AppendLine("<td>" & cellValue & "</td>")
          End If
        Next

        sb.AppendLine("</tr>")
      End If
    Next
    sb.AppendLine("</tbody>")

    ' Footer with statistics
    sb.AppendLine(GenerateFooter(dgvGrid))

    sb.AppendLine("</table>")
    sb.AppendLine("</body>")
    sb.AppendLine("</html>")

    Return sb.ToString()
  End Function

  Private Function GetStyles() As String
    Return "<style>
      body { font-family: Arial, sans-serif; direction: rtl; }
      h1 { text-align: center; color: #333; }
      table { width: 100%; border-collapse: collapse; margin: 20px 0; }
      th { background-color: #4CAF50; color: white; padding: 10px; text-align: right; border: 1px solid #ddd; }
      td { padding: 8px; text-align: right; border: 1px solid #ddd; }
      tr:nth-child(even) { background-color: #f2f2f2; }
      .paid { background-color: #d4edda !important; }
      .pending { background-color: #f8d7da !important; }
      .partial { background-color: #fff3cd !important; }
      .summary { background-color: #e9ecef; font-weight: bold; }
    </style>"
  End Function

  Private Function GetRowClass(row As DataGridViewRow) As String
    If row.Cells("enmPaymentStatus") IsNot Nothing AndAlso
       row.Cells("enmPaymentStatus").Value IsNot Nothing Then
      Select Case CType(row.Cells("enmPaymentStatus").Value, enmPaymentStatus)
        Case enmPaymentStatus.Paid : Return "paid"
        Case enmPaymentStatus.Pending : Return "pending"
        Case enmPaymentStatus.PartiallyPaid : Return "partial"
      End Select
    End If
    Return ""
  End Function

  Private Function GenerateFooter(dgvGrid As DataGridView) As String
    Dim sb As New StringBuilder()
    Dim stats = CalculateStatistics(dgvGrid)

    sb.AppendLine("<tfoot>")
    sb.AppendLine("<tr class='summary'>")
    sb.AppendLine("<td colspan='3'>סה""כ שורות: " & stats.RowCount & "</td>")
    sb.AppendLine("<td colspan='3'>סה""כ כולל: ₪" & stats.TotalAmount.ToString("N2") & "</td>")
    sb.AppendLine("<td colspan='3'>שולם: ₪" & stats.PaidAmount.ToString("N2") & "</td>")
    sb.AppendLine("<td colspan='3'>ממתין: ₪" & stats.PendingAmount.ToString("N2") & "</td>")
    sb.AppendLine("</tr>")
    sb.AppendLine("</tfoot>")

    Return sb.ToString()
  End Function

  Private Function CalculateStatistics(dgvGrid As DataGridView) As ExportStatistics
    Dim stats As New ExportStatistics()

    For Each row As DataGridViewRow In dgvGrid.Rows
      If row.Visible AndAlso Not row.IsNewRow Then
        stats.RowCount += 1

        If row.Cells("TotalWithVAT") IsNot Nothing AndAlso
           row.Cells("TotalWithVAT").Value IsNot Nothing Then
          Dim amount = CDec(row.Cells("TotalWithVAT").Value)
          stats.TotalAmount += amount

          If row.Cells("enmPaymentStatus") IsNot Nothing AndAlso
             row.Cells("enmPaymentStatus").Value IsNot Nothing Then
            Select Case CType(row.Cells("enmPaymentStatus").Value, enmPaymentStatus)
              Case enmPaymentStatus.Paid
                stats.PaidAmount += amount
              Case enmPaymentStatus.Pending
                stats.PendingAmount += amount
            End Select
          End If
        End If
      End If
    Next

    Return stats
  End Function

  ' הפונקציות TranslateColumnHeader ו-TranslateEnumValue הועברו ל-ExportHelper Module

  Private Class ExportStatistics
    Public Property RowCount As Integer
    Public Property TotalAmount As Decimal
    Public Property PaidAmount As Decimal
    Public Property PendingAmount As Decimal
  End Class
End Class

''' <summary>
''' שירות ייצוא ל-CSV
''' </summary>
Public Class CSVExportService
  Implements IExportService

  Private _requester As clsRequester

  Public Sub New(requester As clsRequester)
    _requester = requester
  End Sub

  Public Function Export(dgvGrid As DataGridView, fileName As String) As clsFault Implements IExportService.Export
    Dim pFault As New clsFault()
    Try
      Dim csv = GenerateCSV(dgvGrid)
      ' Write with BOM for Hebrew support
      Dim encoding = New UTF8Encoding(True)
      File.WriteAllText(fileName, csv, encoding)
      Return pFault.SetOK()
    Catch ex As Exception
      pFault.LogException(ex, "Export", "TRGT-CSV-241225-4002", _requester)
      Return pFault
    End Try
  End Function

  Private Function GenerateCSV(dgvGrid As DataGridView) As String
    Dim sb As New StringBuilder()

    ' Headers
    Dim headers As New List(Of String)
    For Each col As DataGridViewColumn In dgvGrid.Columns
      If col.Visible AndAlso Not TypeOf col Is DataGridViewButtonColumn Then
        headers.Add(ExportHelper.TranslateColumnHeader(col.Name))
      End If
    Next
    sb.AppendLine(String.Join(",", headers))

    ' Data
    For Each row As DataGridViewRow In dgvGrid.Rows
      If row.Visible AndAlso Not row.IsNewRow Then
        Dim values As New List(Of String)

        For Each col As DataGridViewColumn In dgvGrid.Columns
          If col.Visible AndAlso Not TypeOf col Is DataGridViewButtonColumn Then
            Dim cellValue = FormatCellValue(row.Cells(col.Index))
            ' Add quotes if contains comma
            If cellValue.Contains(",") Then
              cellValue = """" & cellValue.Replace("""", """""") & """"
            End If
            values.Add(cellValue)
          End If
        Next

        sb.AppendLine(String.Join(",", values))
      End If
    Next

    Return sb.ToString()
  End Function

  Private Function TranslateColumnHeader(columnName As String) As String
    Return ExportHelper.TranslateColumnHeader(columnName)
  End Function

  Private Function FormatCellValue(cell As DataGridViewCell) As String
    Return ExportHelper.FormatCellValue(cell)
  End Function
End Class

''' <summary>
''' Factory לייצור שירותי ייצוא
''' </summary>
Public Class ExportServiceFactory

  Public Shared Function CreateExportService(fileName As String, requester As clsRequester) As IExportService
    If fileName.ToLower().EndsWith(".csv") Then
      Return New CSVExportService(requester)
    Else
      Return New ExcelExportService(requester)
    End If
  End Function

End Class