Partial Public Class ctlccOrderLine
  Inherits System.Windows.Forms.UserControl

  ' משתנים פרטיים לפונקציונליות נוספת
  Private _IsCalculating As Boolean = False
  Private lblPriceInfo As Label

  Private Sub ctlccOrderLine_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' הוספת תווית למידע על מחיר
    AddPriceInfoLabel()

    ' הוספת אירועים לחישוב אוטומטי
    AddCustomEventHandlers()

    ' חישוב ראשוני אם יש נתונים
    If _OrderLine IsNot Nothing AndAlso _OrderLine.ProductID > 0 Then
      DisplayCalculatedValues()
    End If
  End Sub

  Private Sub AddCustomEventHandlers()
    Try
      ' חיבור לאירועים של שדות קיימים
      Dim txtQuantity = TryCast(Me.Controls.Find("txtQuantity", True).FirstOrDefault(), TextBox)
      Dim txtUnitPrice = TryCast(Me.Controls.Find("txtUnitPrice", True).FirstOrDefault(), TextBox)
      Dim cboProduct = TryCast(Me.Controls.Find("cboProduct", True).FirstOrDefault(), IntelliCombo)

      If txtQuantity IsNot Nothing Then
        AddHandler txtQuantity.Leave, AddressOf OnQuantityLeave
      End If

      If txtUnitPrice IsNot Nothing Then
        AddHandler txtUnitPrice.Leave, AddressOf OnPriceLeave
      End If

      If cboProduct IsNot Nothing Then
        AddHandler cboProduct.evtComboListMemberChosen, AddressOf OnProductChosen
      End If

    Catch ex As Exception
      Dim pFault As New clsFault()
      pFault.LogException(ex, "AddCustomEventHandlers", "TRGT-OrderLine-" & DateTime.Now.ToString("yyMMdd-HHmm"), _Requester)
    End Try
  End Sub

  Private Sub AddPriceInfoLabel()
    Try
      ' יצירת תווית למידע על מחיר
      lblPriceInfo = New Label()
      With lblPriceInfo
        .Name = "lblPriceInfo"
        .Text = ""
        .Size = New Size(300, 20)
        .Location = New Point(10, 100) ' מיקום יתעדכן לפי הצורך
        .Font = New Font("Arial", 9, FontStyle.Italic)
        .ForeColor = Color.Blue
        .Visible = False
      End With

      Me.Controls.Add(lblPriceInfo)

    Catch ex As Exception
      ' לא קריטי
    End Try
  End Sub

  Private Sub OnProductChosen(ByVal vComboListMember As clsComboListMember)
    If _IsCalculating Then Exit Sub

    Try
      _IsCalculating = True

      Dim productID As Long = vComboListMember.KeyLong

      If productID > 0 Then
        ' עדכון המוצר בשורה
        If _OrderLine IsNot Nothing Then
          _OrderLine.ProductID = productID
        End If

        ' קבלת מחיר אוטומטי
        UpdatePriceForProduct(productID)

        ' הצגת ערכים מחושבים
        DisplayCalculatedValues()
      End If

    Catch ex As Exception
      Dim pFault As New clsFault()
      pFault.LogException(ex, "OnProductChosen", "TRGT-OrderLine-" & DateTime.Now.ToString("yyMMdd-HHmm"), _Requester)
    Finally
      _IsCalculating = False
    End Try
  End Sub

  Private Sub UpdatePriceForProduct(productID As Long)
    Try
      ' קבלת ההזמנה הראשית
      Dim orderHeader = GetOrderHeader()
      If orderHeader Is Nothing Then Exit Sub

      ' קבלת סוג הלקוח
      Dim customerType = GetCustomerType(orderHeader.CustomerID)

      ' חיפוש מחיר מתאים
      Dim price = GetProductPrice(productID, customerType)

      ' עדכון המחיר (רק UnitPrice כי הוא לא ReadOnly)
      Dim txtUnitPrice = TryCast(Me.Controls.Find("txtUnitPrice", True).FirstOrDefault(), TextBox)
      If txtUnitPrice IsNot Nothing AndAlso price > 0 Then
        txtUnitPrice.Text = price.ToString("N2")

        If _OrderLine IsNot Nothing Then
          _OrderLine.UnitPrice = price
        End If
      End If

      ' הצגת מידע על המחיר
      ShowPriceInfo(productID, customerType, price)

    Catch ex As Exception
      Dim pFault As New clsFault()
      pFault.LogException(ex, "UpdatePriceForProduct", "TRGT-OrderLine-" & DateTime.Now.ToString("yyMMdd-HHmm"), _Requester)
    End Try
  End Sub

  Private Function GetOrderHeader() As clsOrderHeader
    Try
      ' חיפוש ההזמנה דרך ה-Parent controls
      Dim parentControl = Me.Parent
      While parentControl IsNot Nothing
        If TypeOf parentControl Is ctlccOrderHeader Then
          ' שימוש ב-Property הקיים עם סוגריים מרובעים
          Return DirectCast(parentControl, ctlccOrderHeader).[OrderHeader]
        End If
        parentControl = parentControl.Parent
      End While

      ' אם לא נמצא, נסה לטעון לפי OrderHeaderID
      If _OrderLine IsNot Nothing AndAlso _OrderLine.OrderHeaderID > 0 Then
        Dim order As New clsOrderHeader()
        Dim fault = order.GetByID(_OrderLine.OrderHeaderID, _Requester)
        If fault.isOK Then Return order
      End If

      Return Nothing

    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  Private Function GetCustomerType(customerID As Long) As clsEnums.enmCustomerType
    Try
      Dim customer As New clsCustomer()
      Dim fault = customer.GetByID(customerID, _Requester)
      If fault.isOK Then
        Return customer.CustomerType
      End If
      Return clsEnums.enmCustomerType.UD

    Catch ex As Exception
      Return clsEnums.enmCustomerType.UD
    End Try
  End Function

  Private Function GetProductPrice(productID As Long, customerType As clsEnums.enmCustomerType) As Decimal
    Try
      ' חיפוש מחיר בטבלת ProductPrice
      Dim prices As New clsProductPriceCol()
      Dim fault = prices.FillByProductID(productID, _Requester)
      If Not fault.isOK Then Return 0

      ' חיפוש מחיר לפי סוג לקוח
      For Each price As clsProductPrice In prices
        If price.CustomerType = customerType Then
          Return price.SellingPrice
        End If
      Next

      ' אם לא נמצא, חפש מחיר כללי
      For Each price As clsProductPrice In prices
        If price.CustomerType = clsEnums.enmCustomerType.UD Then
          Return price.SellingPrice
        End If
      Next

      Return 0

    Catch ex As Exception
      Return 0
    End Try
  End Function

  Private Sub ShowPriceInfo(productID As Long, customerType As clsEnums.enmCustomerType, price As Decimal)
    Try
      If lblPriceInfo IsNot Nothing Then
        ' קבלת שם המוצר
        Dim product As New clsProduct()
        Dim productName As String = ""
        Dim fault = product.GetByID(productID, _Requester)
        If fault.isOK Then
          productName = product.ProductName
        End If

        ' הצגת מידע
        lblPriceInfo.Text = String.Format("מחיר ל{0}: ₪{1:N2}",
                    GetCustomerTypeHebrew(customerType), price)
        lblPriceInfo.Visible = True

        ' צבע לפי מחיר
        If price > 100 Then
          lblPriceInfo.ForeColor = Color.Red
        ElseIf price > 50 Then
          lblPriceInfo.ForeColor = Color.Orange
        Else
          lblPriceInfo.ForeColor = Color.Green
        End If
      End If

    Catch ex As Exception
      ' לא קריטי
    End Try
  End Sub

  Private Function GetCustomerTypeHebrew(customerType As clsEnums.enmCustomerType) As String
    Select Case customerType
      Case clsEnums.enmCustomerType.Private
        Return "פרטי"
      Case clsEnums.enmCustomerType.Farmer
        Return "חקלאי"
      Case clsEnums.enmCustomerType.Hydro
        Return "הידרו"
      Case clsEnums.enmCustomerType.Farm
        Return "חווה"
      Case Else
        Return "כללי"
    End Select
  End Function

  Private Sub OnQuantityLeave(sender As Object, e As EventArgs)
    ' ולידציה של כמות
    Dim txtQuantity = TryCast(sender, TextBox)
    If txtQuantity IsNot Nothing Then
      Dim quantity As Integer
      If Not Integer.TryParse(txtQuantity.Text, quantity) OrElse quantity < 0 Then
        txtQuantity.Text = "1"
        If _OrderLine IsNot Nothing Then _OrderLine.Quantity = 1
      End If
    End If
    DisplayCalculatedValues()
  End Sub

  Private Sub OnPriceLeave(sender As Object, e As EventArgs)
    ' ולידציה של מחיר
    Dim txtUnitPrice = TryCast(sender, TextBox)
    If txtUnitPrice IsNot Nothing Then
      Dim price As Decimal
      If Not Decimal.TryParse(txtUnitPrice.Text, price) OrElse price < 0 Then
        txtUnitPrice.Text = "0"
        If _OrderLine IsNot Nothing Then _OrderLine.UnitPrice = 0
      End If
    End If
    DisplayCalculatedValues()
  End Sub

  Private Sub DisplayCalculatedValues()
    Try
      _IsCalculating = True

      ' קבלת הערכים הנוכחיים
      Dim quantity As Integer = 0
      Dim unitPrice As Decimal = 0

      If _OrderLine IsNot Nothing Then
        quantity = _OrderLine.Quantity
        unitPrice = _OrderLine.UnitPrice
      End If

      ' חישוב לתצוגה בלבד (לא עדכון של שדות ReadOnly!)
      Dim lineTotal = quantity * unitPrice

      ' הצגת הסכום המחושב בתווית או בטקסט בוקס אם רוצים
      Dim txtLineTotal = TryCast(Me.Controls.Find("txtLineTotal", True).FirstOrDefault(), TextBox)
      If txtLineTotal IsNot Nothing Then
        ' אם השדה המחושב קיים ב-DB, השתמש בו
        If _OrderLine IsNot Nothing AndAlso _OrderLine.LineTotal > 0 Then
          txtLineTotal.Text = _OrderLine.LineTotal.ToString("N2")
        Else
          ' אחרת הצג את החישוב הידני
          txtLineTotal.Text = lineTotal.ToString("N2")
        End If

        ' צביעת השדה לפי גודל ההזמנה
        If lineTotal > 1000 Then
          txtLineTotal.BackColor = Color.LightGreen
        ElseIf lineTotal > 500 Then
          txtLineTotal.BackColor = Color.LightYellow
        Else
          txtLineTotal.BackColor = Color.White
        End If
      End If

      ' הפעלת אירוע שהשורה השתנתה
      RaiseEvent LineTotalChanged(Me, EventArgs.Empty)

    Catch ex As Exception
      Dim pFault As New clsFault()
      pFault.LogException(ex, "DisplayCalculatedValues", "TRGT-OrderLine-" & DateTime.Now.ToString("yyMMdd-HHmm"), _Requester)
    Finally
      _IsCalculating = False
    End Try
  End Sub

  ' אירוע שנורה כשסכום השורה השתנה
  Public Event LineTotalChanged As EventHandler

  ' פונקציה ציבורית לחישוב מחדש
  Public Sub RecalculateLine()
    DisplayCalculatedValues()
  End Sub

  ' פונקציה לקבלת מידע על השורה
  Public Function GetLineInfo() As String
    Try
      If _OrderLine Is Nothing Then Return ""

      Dim productName As String = ""
      If _OrderLine.ProductID > 0 Then
        Dim product As New clsProduct()
        Dim fault = product.GetByID(_OrderLine.ProductID, _Requester)
        If fault.isOK Then productName = product.ProductName
      End If

      ' שימוש בשדות רגילים או מחושבים אם קיימים
      Dim lineTotal As Decimal = _OrderLine.Quantity * _OrderLine.UnitPrice
      If _OrderLine.LineTotal > 0 Then
        lineTotal = _OrderLine.LineTotal
      End If

      Return String.Format("{0} - כמות: {1} | מחיר: ₪{2:N2} | סה""כ: ₪{3:N2}",
                productName, _OrderLine.Quantity, _OrderLine.UnitPrice, lineTotal)

    Catch ex As Exception
      Return ""
    End Try
  End Function

End Class