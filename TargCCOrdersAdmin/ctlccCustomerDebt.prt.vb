Imports clsEnums = TargCCOrders.DataController.clsEnums

Partial Public Class ctlccCustomerDebt
  Inherits System.Windows.Forms.UserControl

  ' כפתורים לניהול חובות
  Private WithEvents btnMarkPaid As Button
  Private WithEvents btnPartialPayment As Button
  Private WithEvents btnCreateFromOrders As Button
  Private lblDebtSummary As Label

  Private Sub ctlccCustomerDebt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' הוספת כפתורים וסיכום
    CreateDebtManagementControls()

    ' חישוב סיכום החוב
    If _CustomerDebt IsNot Nothing Then
      UpdateDebtSummary()
    End If
  End Sub

  Private Sub CreateDebtManagementControls()
    Try
      ' תווית סיכום
      lblDebtSummary = New Label()
      With lblDebtSummary
        .Name = "lblDebtSummary"
        .Size = New Size(400, 30)
        .Location = New Point(10, 10)
        .Font = New Font("Arial", 12, FontStyle.Bold)
        .ForeColor = Color.DarkRed
        .Text = "סיכום חוב: ₪0.00"
      End With

      ' כפתור סמן כשולם
      btnMarkPaid = New Button()
      With btnMarkPaid
        .Name = "btnMarkPaid"
        .Text = "✅ סמן כשולם במלואו"
        .Size = New Size(140, 30)
        .Location = New Point(10, 50)
        .BackColor = Color.LightGreen
        .Font = New Font("Arial", 9, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Green
      End With

      ' כפתור תשלום חלקי
      btnPartialPayment = New Button()
      With btnPartialPayment
        .Name = "btnPartialPayment"
        .Text = "💰 תשלום חלקי"
        .Size = New Size(120, 30)
        .Location = New Point(160, 50)
        .BackColor = Color.LightYellow
        .Font = New Font("Arial", 9, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Orange
      End With

      ' כפתור יצירה מהזמנות
      btnCreateFromOrders = New Button()
      With btnCreateFromOrders
        .Name = "btnCreateFromOrders"
        .Text = "📋 צור מהזמנות לא משולמות"
        .Size = New Size(180, 30)
        .Location = New Point(290, 50)
        .BackColor = Color.LightBlue
        .Font = New Font("Arial", 9, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Blue
      End With

      ' הוספת הקונטרולים
      Me.Controls.Add(lblDebtSummary)
      Me.Controls.Add(btnMarkPaid)
      Me.Controls.Add(btnPartialPayment)
      Me.Controls.Add(btnCreateFromOrders)

      ' הבאה לקדמה
      lblDebtSummary.BringToFront()
      btnMarkPaid.BringToFront()
      btnPartialPayment.BringToFront()
      btnCreateFromOrders.BringToFront()

      ' Tooltips
      Dim toolTip As New ToolTip()
      toolTip.SetToolTip(btnMarkPaid, "סמן את כל החוב כשולם")
      toolTip.SetToolTip(btnPartialPayment, "הזן סכום תשלום חלקי")
      toolTip.SetToolTip(btnCreateFromOrders, "צור רשומת חוב מכל ההזמנות שלא שולמו")

    Catch ex As Exception
      ' WriteToLog מוחלף בלוג רגיל
      Console.WriteLine("Error creating debt management controls: " & ex.Message)
    End Try
  End Sub

  Private Sub UpdateDebtSummary()
    Try
      If _CustomerDebt Is Nothing Then Exit Sub

      ' חישוב היתרה
      Dim remainingDebt = _CustomerDebt.DebtAmount - _CustomerDebt.PaidAmount

      ' עדכון התווית
      If lblDebtSummary IsNot Nothing Then
        lblDebtSummary.Text = String.Format("סה""כ חוב: ₪{0:N2} | שולם: ₪{1:N2} | יתרה: ₪{2:N2}",
                    _CustomerDebt.DebtAmount,
                    _CustomerDebt.PaidAmount,
                    remainingDebt)

        ' צבע לפי סטטוס
        If remainingDebt <= 0 Then
          lblDebtSummary.ForeColor = Color.Green
        ElseIf remainingDebt < _CustomerDebt.DebtAmount Then
          lblDebtSummary.ForeColor = Color.Orange
        Else
          lblDebtSummary.ForeColor = Color.Red
        End If
      End If

      ' עדכון כפתורים
      If btnMarkPaid IsNot Nothing Then
        btnMarkPaid.Enabled = (remainingDebt > 0)
      End If

      If btnPartialPayment IsNot Nothing Then
        btnPartialPayment.Enabled = (remainingDebt > 0)
      End If

    Catch ex As Exception
      Console.WriteLine("Error updating debt summary: " & ex.Message)
    End Try
  End Sub

  Private Sub btnMarkPaid_Click(sender As Object, e As EventArgs) Handles btnMarkPaid.Click
    Try
      If _CustomerDebt Is Nothing OrElse _CustomerDebt.ID <= 0 Then
        MsgBox("אנא בחר חוב לעדכון", MsgBoxStyle.Information)
        Exit Sub
      End If

      ' חישוב היתרה
      Dim remainingDebt = _CustomerDebt.DebtAmount - _CustomerDebt.PaidAmount

      If remainingDebt <= 0 Then
        MsgBox("החוב כבר שולם במלואו", MsgBoxStyle.Information)
        Exit Sub
      End If

      ' אישור
      Dim result = MsgBox(String.Format("לסמן חוב של ₪{0:N2} כשולם במלואו?",
                remainingDebt), CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "אישור תשלום")

      If result <> MsgBoxResult.Yes Then Exit Sub

      ' עדכון החוב
      _CustomerDebt.PaidAmount = _CustomerDebt.DebtAmount
      _CustomerDebt.DebtStatus = clsEnums.enmDebtStatus.Paid
      _CustomerDebt.Notes = _CustomerDebt.Notes & vbCrLf &
                String.Format("[{0}] שולם במלואו", Date.Now.ToString("dd/MM/yyyy HH:mm"))

      ' שמירה
      Dim fault = _CustomerDebt.Update(_Requester)
      If Not fault.isOK Then
        MsgBox("שגיאה בעדכון חוב: " & fault.Message, MsgBoxStyle.Critical)
        Exit Sub
      End If

      ' עדכון התצוגה
      UpdateDebtSummary()
      UpdateDisplay()

      MsgBox("החוב סומן כשולם במלואו", MsgBoxStyle.Information, "עדכון הושלם")

    Catch ex As Exception
      MsgBox("שגיאה בעדכון חוב: " & ex.Message, MsgBoxStyle.Critical)
      Console.WriteLine("Error in btnMarkPaid_Click: " & ex.ToString())
    End Try
  End Sub

  Private Sub btnPartialPayment_Click(sender As Object, e As EventArgs) Handles btnPartialPayment.Click
    Try
      If _CustomerDebt Is Nothing OrElse _CustomerDebt.ID <= 0 Then
        MsgBox("אנא בחר חוב לעדכון", MsgBoxStyle.Information)
        Exit Sub
      End If

      ' חישוב היתרה
      Dim remainingDebt = _CustomerDebt.DebtAmount - _CustomerDebt.PaidAmount

      If remainingDebt <= 0 Then
        MsgBox("החוב כבר שולם במלואו", MsgBoxStyle.Information)
        Exit Sub
      End If

      ' בקשת סכום תשלום
      Dim paymentStr = InputBox(String.Format("הזן סכום תשלום חלקי:" & vbCrLf &
                "יתרת חוב נוכחית: ₪{0:N2}", remainingDebt),
                "תשלום חלקי", remainingDebt.ToString("N2"))

      If String.IsNullOrEmpty(paymentStr) Then Exit Sub

      Dim paymentAmount As Decimal
      If Not Decimal.TryParse(paymentStr, paymentAmount) OrElse paymentAmount <= 0 Then
        MsgBox("סכום לא תקין", MsgBoxStyle.Exclamation)
        Exit Sub
      End If

      If paymentAmount > remainingDebt Then
        If MsgBox(String.Format("הסכום גדול מהיתרה (₪{0:N2}). להמשיך?",
                    remainingDebt), CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle)) = MsgBoxResult.No Then
          Exit Sub
        End If
      End If

      ' עדכון החוב
      _CustomerDebt.PaidAmount += paymentAmount

      ' עדכון סטטוס
      If _CustomerDebt.PaidAmount >= _CustomerDebt.DebtAmount Then
        _CustomerDebt.DebtStatus = clsEnums.enmDebtStatus.Paid
      Else
        _CustomerDebt.DebtStatus = clsEnums.enmDebtStatus.PartiallyPaid
      End If

      ' הוספת הערה
      _CustomerDebt.Notes = _CustomerDebt.Notes & vbCrLf &
                String.Format("[{0}] תשלום חלקי: ₪{1:N2}",
                Date.Now.ToString("dd/MM/yyyy HH:mm"), paymentAmount)

      ' שמירה
      Dim fault = _CustomerDebt.Update(_Requester)
      If Not fault.isOK Then
        MsgBox("שגיאה בעדכון חוב: " & fault.Message, MsgBoxStyle.Critical)
        Exit Sub
      End If

      ' עדכון התצוגה
      UpdateDebtSummary()
      UpdateDisplay()

      MsgBox(String.Format("התשלום נקלט בהצלחה!" & vbCrLf &
                "שולם: ₪{0:N2}" & vbCrLf &
                "יתרה: ₪{1:N2}",
                paymentAmount,
                _CustomerDebt.DebtAmount - _CustomerDebt.PaidAmount),
                MsgBoxStyle.Information, "תשלום חלקי")

    Catch ex As Exception
      MsgBox("שגיאה בעדכון תשלום: " & ex.Message, MsgBoxStyle.Critical)
      Console.WriteLine("Error in btnPartialPayment_Click: " & ex.ToString())
    End Try
  End Sub

  Private Sub btnCreateFromOrders_Click(sender As Object, e As EventArgs) Handles btnCreateFromOrders.Click
    Try
      ' בדיקה שיש לקוח
      Dim customerID As Long = 0
      If _CustomerDebt IsNot Nothing Then
        customerID = _CustomerDebt.CustomerID
      Else
        ' נסה לקבל מהקונטקסט
        Dim parentControl = Me.Parent
        While parentControl IsNot Nothing
          If TypeOf parentControl Is ctlccCustomer Then
            Dim customerControl = DirectCast(parentControl, ctlccCustomer)
            ' שימוש ב-property או method ציבורי במקום גישה ישירה ל-_Customer
            If customerControl.GetCustomer() IsNot Nothing Then
              customerID = customerControl.GetCustomer().ID
              Exit While
            End If
          End If
          parentControl = parentControl.Parent
        End While
      End If

      If customerID <= 0 Then
        MsgBox("לא נמצא לקוח לחישוב חובות", MsgBoxStyle.Information)
        Exit Sub
      End If

      ' טעינת הזמנות לא משולמות
      Dim orders As New clsOrderHeaderCol()
      Dim fault = orders.FillByCustomerID(customerID, _Requester)
      If Not fault.isOK Then
        MsgBox("שגיאה בטעינת הזמנות: " & fault.Message, MsgBoxStyle.Critical)
        Exit Sub
      End If

      ' סינון להזמנות לא משולמות
      Dim unpaidTotal As Decimal = 0
      Dim unpaidCount As Integer = 0
      Dim orderNumbers As New List(Of String)

      For Each order As clsOrderHeader In orders
        If order.PaymentStatus <> clsEnums.enmPaymentStatus.Paid Then
          unpaidTotal += order.TotalWithVAT
          unpaidCount += 1
          orderNumbers.Add(order.OrderNumber.ToString)
        End If
      Next

      If unpaidCount = 0 Then
        MsgBox("לא נמצאו הזמנות שלא שולמו", MsgBoxStyle.Information)
        Exit Sub
      End If

      ' אישור יצירת חוב
      Dim result = MsgBox(String.Format("נמצאו {0} הזמנות שלא שולמו" & vbCrLf &
                "סה""כ חוב: ₪{1:N2}" & vbCrLf & vbCrLf &
                "ליצור רשומת חוב מרוכזת?",
                unpaidCount, unpaidTotal),
                CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "יצירת חוב")

      If result <> MsgBoxResult.Yes Then Exit Sub

      ' יצירת רשומת חוב חדשה
      Dim newDebt As New clsCustomerDebt()
      With newDebt
        .CustomerID = customerID
        .DebtAmount = unpaidTotal
        .PaidAmount = 0
        .DebtDate = Date.Today
        .DebtStatus = clsEnums.enmDebtStatus.Open
        .Notes = String.Format("חוב מ-{0} הזמנות: {1}",
                    unpaidCount, String.Join(", ", orderNumbers))
      End With

      ' שמירה
      fault = newDebt.Update(_Requester)
      If Not fault.isOK Then
        MsgBox("שגיאה ביצירת חוב: " & fault.Message, MsgBoxStyle.Critical)
        Exit Sub
      End If

      MsgBox(String.Format("רשומת חוב נוצרה בהצלחה!" & vbCrLf &
                "סכום: ₪{0:N2}" & vbCrLf &
                "מספר הזמנות: {1}",
                unpaidTotal, unpaidCount),
                MsgBoxStyle.Information, "חוב נוצר")

      ' טעינת החוב החדש
      Dim pLoadParams As New ctlccCustomerDebt.clsLoadParameters()
      Me.LoadControl(newDebt, pLoadParams, _Requester)

    Catch ex As Exception
      MsgBox("שגיאה ביצירת חוב: " & ex.Message, MsgBoxStyle.Critical)
      Console.WriteLine("Error in btnCreateFromOrders_Click: " & ex.ToString())
    End Try
  End Sub

  Private Sub UpdateDisplay()
    Try
      ' עדכון השדות בתצוגה
      Dim txtDebtAmount = Me.Controls.Find("txtDebtAmount", True).FirstOrDefault()
      If txtDebtAmount IsNot Nothing Then
        txtDebtAmount.Text = _CustomerDebt.DebtAmount.ToString("N2")
      End If

      Dim txtPaidAmount = Me.Controls.Find("txtPaidAmount", True).FirstOrDefault()
      If txtPaidAmount IsNot Nothing Then
        txtPaidAmount.Text = _CustomerDebt.PaidAmount.ToString("N2")
      End If

      Dim cboStatus = Me.Controls.Find("cboenmDebtStatus", True).FirstOrDefault()
      If cboStatus IsNot Nothing Then
        CType(cboStatus, IntelliCombo).ValueSelect(_CustomerDebt.DebtStatus)
      End If

    Catch ex As Exception
      ' לא קריטי
    End Try
  End Sub

End Class