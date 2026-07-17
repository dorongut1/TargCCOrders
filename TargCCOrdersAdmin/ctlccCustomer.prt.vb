Imports clsEnums = TargCCOrders.DataController.clsEnums
Partial Public Class ctlccCustomer
  Inherits System.Windows.Forms.UserControl
  ' כפתור ליצירת הזמנה חדשה
  Private WithEvents btnCreateNewOrder As Button
  ' כפתור להצגת היסטוריית הזמנות
  Private WithEvents btnShowOrderHistory As Button

  ' פונקציה ציבורית לגישה ללקוח
  Public Function GetCustomer() As clsCustomer
    Return _Customer
  End Function

  Private Sub ctlccCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' הוספת כפתורים
    CreateOrderButton()
    CreateOrderHistoryButton()
  End Sub

  Private Sub CreateOrderButton()
    Try
      ' יצירת הכפתור
      btnCreateNewOrder = New Button()
      With btnCreateNewOrder
        .Name = "btnCreateNewOrder"
        .Text = "צור הזמנה חדשה"
        .Size = New Size(150, 30)
        .Location = New Point(10, 10) ' נשנה מיקום בהתאם לעיצוב
        .BackColor = Color.LightGreen
        .Font = New Font("Arial", 10, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Green
        .FlatAppearance.BorderSize = 2
      End With

      ' הוספת הכפתור לפאנל העליון או למיקום מתאים
      ' נחפש פאנל מתאים או נוסיף ישירות לקונטרול
      Dim targetPanel As Control = Me.Controls.Find("pnlTop", True).FirstOrDefault()
      If targetPanel Is Nothing Then
        targetPanel = Me.Controls.Find("pnlButtons", True).FirstOrDefault()
      End If

      If targetPanel IsNot Nothing Then
        targetPanel.Controls.Add(btnCreateNewOrder)
      Else
        ' אם אין פאנל, נוסיף ישירות לקונטרול
        Me.Controls.Add(btnCreateNewOrder)
        btnCreateNewOrder.BringToFront()
      End If

      ' הוספת Tooltip
      Dim toolTip As New ToolTip()
      toolTip.SetToolTip(btnCreateNewOrder, "יצירת הזמנה חדשה עבור הלקוח הנוכחי")

    Catch ex As Exception
      ' אם יש בעיה ביצירת הכפתור, לא נעצור את כל המסך
      Console.WriteLine("Error creating order button: " & ex.Message)
    End Try
  End Sub

  Private Sub CreateOrderHistoryButton()
    Try
      ' יצירת כפתור היסטוריית הזמנות
      btnShowOrderHistory = New Button()
      With btnShowOrderHistory
        .Name = "btnShowOrderHistory"
        .Text = "הצג היסטוריית הזמנות"
        .Size = New Size(150, 30)
        .Location = New Point(170, 10) ' ליד הכפתור הראשון
        .BackColor = Color.LightBlue
        .Font = New Font("Arial", 10, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Blue
        .FlatAppearance.BorderSize = 2
      End With

      ' הוספה לאותו מקום כמו הכפתור הראשון
      Dim targetPanel As Control = Me.Controls.Find("pnlTop", True).FirstOrDefault()
      If targetPanel Is Nothing Then
        targetPanel = Me.Controls.Find("pnlButtons", True).FirstOrDefault()
      End If

      If targetPanel IsNot Nothing Then
        targetPanel.Controls.Add(btnShowOrderHistory)
      Else
        Me.Controls.Add(btnShowOrderHistory)
        btnShowOrderHistory.BringToFront()
      End If

      ' הוספת Tooltip
      Dim toolTip As New ToolTip()
      toolTip.SetToolTip(btnShowOrderHistory, "הצגת כל ההזמנות של הלקוח")

    Catch ex As Exception
      Console.WriteLine("Error creating order history button: " & ex.Message)
    End Try
  End Sub

  Private Sub btnCreateNewOrder_Click(sender As Object, e As EventArgs) Handles btnCreateNewOrder.Click
    Try
      ' בדיקה שיש לקוח פעיל
      If _Customer Is Nothing OrElse _Customer.ID <= 0 Then
        MsgBox("אנא שמור את פרטי הלקוח לפני יצירת הזמנה", MsgBoxStyle.Information, "לא ניתן ליצור הזמנה")
        Exit Sub
      End If

      ' בדיקה שהלקוח פעיל - נבדוק אם השדה קיים
      ' אם אין שדה IsActive, נדלג על הבדיקה
      Dim isActiveProperty = _Customer.GetType().GetProperty("IsActive")
      If isActiveProperty IsNot Nothing Then
        Dim isActive = CBool(isActiveProperty.GetValue(_Customer))
        If isActive = False Then
          If MsgBox("הלקוח לא פעיל. האם להמשיך בכל זאת?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "לקוח לא פעיל") = MsgBoxResult.No Then
            Exit Sub
          End If
        End If
      End If

      ' יצירת הזמנה חדשה
      Dim newOrder As New clsOrderHeader()
      With newOrder
        .CustomerID = _Customer.ID
        .OrderDate = Date.Today
        .OrderStatus = clsEnums.enmOrderStatus.New
        .PaymentStatus = clsEnums.enmPaymentStatus.Pending

        ' קביעת שיטת משלוח ברירת מחדל לפי סוג לקוח
        Select Case _Customer.CustomerType
          Case clsEnums.enmCustomerType.Private
            .DeliveryMethod = clsEnums.enmDeliveryMethod.Biobee
          Case clsEnums.enmCustomerType.Farmer
            .DeliveryMethod = clsEnums.enmDeliveryMethod.Netzach
          Case clsEnums.enmCustomerType.Hydro
            .DeliveryMethod = clsEnums.enmDeliveryMethod.Tzofar
          Case Else
            .DeliveryMethod = clsEnums.enmDeliveryMethod.Biobee
        End Select

        ' תאריך משלוח - 3 ימים מהיום (ברירת מחדל)
        .DeliveryDate = Date.Today.AddDays(3)

        ' קביעת יום משלוח לפי היום בשבוע
        Dim dayOfWeek As Integer = CInt(Date.Today.AddDays(3).DayOfWeek)
        Select Case dayOfWeek
          Case 1
            .DeliveryDay = clsEnums.enmDeliveryDay.Monday
          Case 2
            .DeliveryDay = clsEnums.enmDeliveryDay.Tuesday
          Case 3
            .DeliveryDay = clsEnums.enmDeliveryDay.Wednesday
          Case 4
            .DeliveryDay = clsEnums.enmDeliveryDay.Thursday
          Case 5
            ' אין משלוח בשישי - נדחה לראשון
            .DeliveryDay = clsEnums.enmDeliveryDay.Sunday
            .DeliveryDate = .DeliveryDate.AddDays(2)
          Case 6 ' שבת
            .DeliveryDay = clsEnums.enmDeliveryDay.Sunday
            .DeliveryDate = .DeliveryDate.AddDays(1) ' דחיה ליום ראשון
          Case Else ' ראשון
            .DeliveryDay = clsEnums.enmDeliveryDay.Sunday
        End Select

        ' חישוב רבעון וחודש אוטומטי (אם השדות computed columns)
        ' הערך יחושב אוטומטית ב-SQL

        ' הערות
        .Notes = "הזמנה חדשה - " & _Customer.CustomerName
      End With

      ' שמירת ההזמנה
      Dim fault As clsFault = newOrder.Update(_Requester)
      If Not fault.isOK Then
        MsgBox("שגיאה ביצירת הזמנה: " & fault.Message, MsgBoxStyle.Critical, "שגיאה")
        Exit Sub
      End If

      ' הודעה על הצלחה ופתיחת ההזמנה
      MsgBox("הזמנה מספר " & newOrder.OrderNumber & " נוצרה בהצלחה!", MsgBoxStyle.Information, "הזמנה נוצרה")

      ' פתיחת ההזמנה בחלון popup לעריכה
      OpenOrderInPopup(newOrder)

    Catch ex As Exception
      MsgBox("שגיאה ביצירת הזמנה: " & ex.Message, MsgBoxStyle.Critical, "שגיאה")
      Console.WriteLine("Error in btnCreateNewOrder_Click: " & ex.ToString())
    End Try
  End Sub

  Private Sub OpenOrderInPopup(order As clsOrderHeader)
    Try
      Dim pFault As New clsFault
      Dim pLoadParameters As New ctlccOrderHeader.clsLoadParameters()

      ' יצירת חלון Popup
      Dim frmPopup As New frmPopup()
      ' טעינת קונטרול ההזמנה
      Dim orderControl As New ctlccOrderHeader()

      ' העברת הרפרנס ל-Requester דרך property ציבורי או מתודה
      pFault = orderControl.LoadControl(order, pLoadParameters, _Requester)

      ' הגדרת הפופאפ
      With frmPopup
        .Text = "הזמנה מספר " & order.OrderNumber & " - " & _Customer.CustomerName
        .Size = New Size(1200, 700)
        .StartPosition = FormStartPosition.CenterScreen

        ' הוספת הקונטרול לפופאפ
        orderControl.Dock = DockStyle.Fill
        .Controls.Add(orderControl)

        ' הצגת הפופאפ
        .ShowDialog()
      End With

      ' רענון רשימת ההזמנות של הלקוח (אם יש)
      RefreshCustomerOrders()

    Catch ex As Exception
      MsgBox("שגיאה בפתיחת חלון ההזמנה: " & ex.Message, MsgBoxStyle.Critical, "שגיאה")
      Console.WriteLine("Error in OpenOrderInPopup: " & ex.ToString())
    End Try
  End Sub

  Private Sub RefreshCustomerOrders()
    Try
      ' חיפוש גריד ההזמנות של הלקוח אם קיים
      Dim ordersGrid = Me.Controls.Find("dgvOrders", True).FirstOrDefault()
      If ordersGrid IsNot Nothing Then
        ' טעינה מחדש של ההזמנות
        RaiseEvent RefreshRequired(Me, EventArgs.Empty)
      End If
    Catch ex As Exception
      ' לא חובה שיהיה גריד הזמנות
    End Try
  End Sub

  ' אירוע לרענון נתונים
  Public Event RefreshRequired As EventHandler

  Private Sub btnShowOrderHistory_Click(sender As Object, e As EventArgs) Handles btnShowOrderHistory.Click
    Try
      ' בדיקה שיש לקוח פעיל
      If _Customer Is Nothing OrElse _Customer.ID <= 0 Then
        MsgBox("אנא בחר לקוח להצגת היסטוריית הזמנות", MsgBoxStyle.Information, "לא נבחר לקוח")
        Exit Sub
      End If

      ' טעינת ההזמנות של הלקוח
      Dim orders As New clsOrderHeaderCol()
      Dim fault As clsFault = orders.FillByCustomerID(_Customer.ID, _Requester)

      If Not fault.isOK Then
        MsgBox("שגיאה בטעינת הזמנות: " & fault.Message, MsgBoxStyle.Critical, "שגיאה")
        Exit Sub
      End If

      If orders.Count = 0 Then
        MsgBox("לא נמצאו הזמנות עבור הלקוח", MsgBoxStyle.Information, "אין הזמנות")
        Exit Sub
      End If

      ' יצירת חלון Popup עם גריד הזמנות
      Dim frmPopup As New frmPopup()
      Dim pLoadParams As New ctlccOrderHeaderCol.clsLoadParameters()

      ' יצירת קונטרול גריד הזמנות
      Dim ordersGrid As New ctlccOrderHeaderCol()
      ordersGrid.LoadControl(orders, pLoadParams, _Requester)

      ' הגדרת הפופאפ
      With frmPopup
        .Text = "היסטוריית הזמנות - " & _Customer.CustomerName & " (" & orders.Count & " הזמנות)"
        .Size = New Size(1400, 600)
        .StartPosition = FormStartPosition.CenterScreen

        ' הוספת הקונטרול לפופאפ
        ordersGrid.Dock = DockStyle.Fill
        .Controls.Add(ordersGrid)

        ' הצגת הפופאפ
        .ShowDialog()
      End With

    Catch ex As Exception
      MsgBox("שגיאה בהצגת היסטוריית הזמנות: " & ex.Message, MsgBoxStyle.Critical, "שגיאה")
      Console.WriteLine("Error in btnShowOrderHistory_Click: " & ex.ToString())
    End Try
  End Sub

End Class