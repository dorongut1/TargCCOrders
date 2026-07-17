Imports System.Globalization

Partial Public Class ctlccOrderHeader

  ' אירוע שקורה כשהקונטרול נטען
  Private Sub ctlccOrderHeader_Load(sender As Object, e As EventArgs) Handles Me.Load
    ' הוסף אירוע לשינוי תאריך משלוח
    AddHandler dtpDeliveryDate.ValueChanged, AddressOf DeliveryDate_Changed
    AddHandler cboDeliveryDay.Enter, AddressOf DeliveryDay_Enter
  End Sub
  Private Sub DeliveryDay_Enter(sender As Object, e As EventArgs)
    If dtpDeliveryDate.Checked Then
      ' אם יש תאריך, אל תיתן לשנות
      dtpDeliveryDate.Focus()  ' העבר את הפוקוס חזרה לתאריך
      MessageBox.Show("היום נקבע אוטומטית לפי התאריך", "מידע", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End If
  End Sub
  ' אירוע כשמשנים תאריך משלוח
  Private Sub DeliveryDate_Changed(sender As Object, e As EventArgs)
    If dtpDeliveryDate.Checked = False Then Return ' אם התאריך לא מסומן, צא

    Dim selectedDate As Date = dtpDeliveryDate.Value

    ' בדוק אם זה שישי או שבת
    If selectedDate.DayOfWeek = DayOfWeek.Friday OrElse
           selectedDate.DayOfWeek = DayOfWeek.Saturday Then

      MessageBox.Show("לא ניתן לבחור שישי או שבת למשלוח",
                          "תאריך לא תקין",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning)

      ' העבר ליום ראשון הבא
      While selectedDate.DayOfWeek = DayOfWeek.Friday OrElse
                  selectedDate.DayOfWeek = DayOfWeek.Saturday
        selectedDate = selectedDate.AddDays(1)
      End While

      dtpDeliveryDate.Value = selectedDate
    End If

    ' הכנס את היום בשבוע ל-ComboBox
    Dim dayValue As String = ""
    Select Case selectedDate.DayOfWeek
      Case DayOfWeek.Sunday
        dayValue = "Sunday"
      Case DayOfWeek.Monday
        dayValue = "Monday"
      Case DayOfWeek.Tuesday
        dayValue = "Tuesday"
      Case DayOfWeek.Wednesday
        dayValue = "Wednesday"
      Case DayOfWeek.Thursday
        dayValue = "Thursday"
    End Select

    ' עדכן את ה-ComboBox
    If Not String.IsNullOrEmpty(dayValue) Then
      If cboDeliveryDay.SelectedValue Is Nothing OrElse
               cboDeliveryDay.SelectedValue.ToString() <> dayValue Then
        ' חפש את הערך ב-Items
        For i As Integer = 0 To cboDeliveryDay.Items.Count - 1
          cboDeliveryDay.SelectedIndex = i
          If cboDeliveryDay.Text.Contains(dayValue) OrElse
                       (cboDeliveryDay.SelectedValue IsNot Nothing AndAlso
                        cboDeliveryDay.SelectedValue.ToString() = dayValue) Then
            Exit For
          End If
        Next
      End If
    End If
  End Sub

End Class