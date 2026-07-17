Imports TargCCOrders.DataController.clsEnums
Imports System.Diagnostics
Imports System.Runtime.CompilerServices

Partial Public Class ctlccOrderHeaderCol
  Inherits System.Windows.Forms.UserControl

#Region "Private Variables"
  Private WithEvents dgvOrders As DataGridView
  Private WithEvents btnQuickPay As Button
  Private WithEvents btnFilterToday As Button
  Private WithEvents btnFilterUnpaid As Button
  Private WithEvents btnExportExcel As Button
  Private WithEvents btnPrintDeliveries As Button
  Private _NextInvoiceNumber As Integer = 0
  Private _businessLogic As OrderHeaderBusinessLogic
  Private _parentControl As ctlccOrderHeader
#End Region

#Region "Properties"
  ' _Requester כבר מוגדר בקובץ הרגיל ctlccOrderHeaderCol.vb
  ' כאן רק מוסיפים Property לגישה מבחוץ
  Public Property Requester() As clsRequester
    Get
      Return _Requester
    End Get
    Set(value As clsRequester)
      _Requester = value
      If _businessLogic IsNot Nothing Then
        _businessLogic.Requester = value
      End If
    End Set
  End Property
#End Region

#Region "Initialization and Load"
  Private Sub ctlccOrderHeaderCol_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
      ' Initialize business logic
      InitializeBusinessLogic()

      ' Find the grid
      FindDataGridView()

      ' Add action buttons
      CreateActionButtons()

      ' Add action columns to grid
      If dgvOrders IsNot Nothing Then
        AddActionColumns()
        ColorRowsByStatus()
      End If

      ' Find parent control for Requester access
      FindParentControl()

    Catch ex As Exception
      Dim pFault As New clsFault()
      pFault.LogException(ex, "ctlccOrderHeaderCol_Load", "TRGT-OCHC-241225-1001", _Requester)
    End Try
  End Sub

  Private Sub InitializeBusinessLogic()
    _businessLogic = New OrderHeaderBusinessLogic()
    If _Requester IsNot Nothing Then
      _businessLogic.Requester = _Requester
    End If
  End Sub

  Private Sub FindParentControl()
    Try
      ' חיפוש ה-control ההורה
      Dim parent As Control = Me.Parent
      While parent IsNot Nothing
        If TypeOf parent Is ctlccOrderHeader Then
          _parentControl = DirectCast(parent, ctlccOrderHeader)
          If _Requester Is Nothing Then
            _Requester = _parentControl.Requester
          End If
          Exit While
        End If
        parent = parent.Parent
      End While
    Catch ex As Exception
      ' לא קריטי
    End Try
  End Sub

  Private Sub FindDataGridView()
    Dim pFault As New clsFault()
    Try
      ' חיפוש הגריד הראשי
      Dim foundControls = Me.Controls.Find("dgvOrderHeader", True)
      If foundControls.Length > 0 Then
        dgvOrders = DirectCast(foundControls(0), DataGridView)
      Else
        foundControls = Me.Controls.Find("DataGridView1", True)
        If foundControls.Length > 0 Then
          dgvOrders = DirectCast(foundControls(0), DataGridView)
        End If
      End If

      If dgvOrders IsNot Nothing Then
        ' הוספת אירועים
        AddHandler dgvOrders.CellContentClick, AddressOf OnCellContentClick
        AddHandler dgvOrders.RowsAdded, AddressOf OnRowsAdded
        AddHandler dgvOrders.DataSourceChanged, AddressOf OnDataSourceChanged
      End If

    Catch ex As Exception
      pFault.LogException(ex, "FindDataGridView", "TRGT-OCHC-241225-1002", _Requester)
    End Try
  End Sub
#End Region

#Region "UI Controls Creation"
  Private Sub CreateActionButtons()
    Dim pFault As New clsFault()
    Try
      ' פאנל לכפתורים
      Dim pnlButtons As New Panel()
      With pnlButtons
        .Name = "pnlActionButtons"
        .Height = 40
        .Dock = DockStyle.Top
        .BackColor = Color.WhiteSmoke
        .BorderStyle = BorderStyle.FixedSingle
      End With

      ' כפתור תשלום מהיר
      btnQuickPay = New Button()
      With btnQuickPay
        .Name = "btnQuickPay"
        .Text = "💳 תשלום מהיר"
        .Size = New Size(120, 30)
        .Location = New Point(10, 5)
        .BackColor = Color.LightGreen
        .Font = New Font("Arial", 9, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Green
      End With

      ' כפתור סינון משלוחים להיום
      btnFilterToday = New Button()
      With btnFilterToday
        .Name = "btnFilterToday"
        .Text = "🚚 להיום"
        .Size = New Size(100, 30)
        .Location = New Point(140, 5)
        .BackColor = Color.LightBlue
        .Font = New Font("Arial", 9, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Blue
      End With

      ' כפתור סינון לא שולם
      btnFilterUnpaid = New Button()
      With btnFilterUnpaid
        .Name = "btnFilterUnpaid"
        .Text = "⚠ לא שולם"
        .Size = New Size(100, 30)
        .Location = New Point(250, 5)
        .BackColor = Color.LightPink
        .Font = New Font("Arial", 9, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Red
      End With

      ' כפתור ייצוא לאקסל
      btnExportExcel = New Button()
      With btnExportExcel
        .Name = "btnExportExcel"
        .Text = "📊 Excel"
        .Size = New Size(80, 30)
        .Location = New Point(360, 5)
        .BackColor = Color.LightYellow
        .Font = New Font("Arial", 9, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Orange
      End With

      ' כפתור הדפסת משלוחים
      btnPrintDeliveries = New Button()
      With btnPrintDeliveries
        .Name = "btnPrintDeliveries"
        .Text = "🖨 הדפס משלוחים"
        .Size = New Size(120, 30)
        .Location = New Point(450, 5)
        .BackColor = Color.Lavender
        .Font = New Font("Arial", 9, FontStyle.Bold)
        .Cursor = Cursors.Hand
        .FlatStyle = FlatStyle.Flat
        .FlatAppearance.BorderColor = Color.Purple
      End With

      ' הוספת הכפתורים לפאנל
      pnlButtons.Controls.Add(btnQuickPay)
      pnlButtons.Controls.Add(btnFilterToday)
      pnlButtons.Controls.Add(btnFilterUnpaid)
      pnlButtons.Controls.Add(btnExportExcel)
      pnlButtons.Controls.Add(btnPrintDeliveries)

      ' הוספת הפאנל לקונטרול
      Me.Controls.Add(pnlButtons)
      pnlButtons.BringToFront()

      ' Tooltips
      Dim toolTip As New ToolTip()
      toolTip.SetToolTip(btnQuickPay, "סמן הזמנות נבחרות כשולמו")
      toolTip.SetToolTip(btnFilterToday, "הצג רק משלוחים להיום")
      toolTip.SetToolTip(btnFilterUnpaid, "הצג רק הזמנות שלא שולמו")
      toolTip.SetToolTip(btnExportExcel, "ייצא לקובץ Excel")
      toolTip.SetToolTip(btnPrintDeliveries, "הדפס רשימת משלוחים")

    Catch ex As Exception
      pFault.LogException(ex, "CreateActionButtons", "TRGT-OCHC-241225-1003", _Requester)
    End Try
  End Sub

  Private Sub AddActionColumns()
    Dim pFault As New clsFault()
    Try
      If dgvOrders Is Nothing Then Exit Sub

      ' בדיקה אם העמודות כבר קיימות
      If dgvOrders.Columns.Contains("PaymentAction") Then Exit Sub

      ' עמודת כפתור תשלום
      Dim btnColumn As New DataGridViewButtonColumn()
      With btnColumn
        .Name = "PaymentAction"
        .HeaderText = "תשלום"
        .Text = "💰 שולם"
        .UseColumnTextForButtonValue = True
        .Width = 80
        .DefaultCellStyle.BackColor = Color.LightGreen
        .DefaultCellStyle.ForeColor = Color.DarkGreen
        .DefaultCellStyle.Font = New Font("Arial", 8, FontStyle.Bold)
      End With
      dgvOrders.Columns.Add(btnColumn)

      ' עמודת כפתור הדפסה
      Dim printColumn As New DataGridViewButtonColumn()
      With printColumn
        .Name = "PrintAction"
        .HeaderText = "הדפסה"
        .Text = "🖨"
        .UseColumnTextForButtonValue = True
        .Width = 60
        .DefaultCellStyle.BackColor = Color.WhiteSmoke
      End With
      dgvOrders.Columns.Add(printColumn)

      ' עמודת כפתור עריכה
      Dim editColumn As New DataGridViewButtonColumn()
      With editColumn
        .Name = "EditAction"
        .HeaderText = "עריכה"
        .Text = "✏"
        .UseColumnTextForButtonValue = True
        .Width = 60
        .DefaultCellStyle.BackColor = Color.LightBlue
      End With
      dgvOrders.Columns.Add(editColumn)

    Catch ex As Exception
      pFault.LogException(ex, "AddActionColumns", "TRGT-OCHC-241225-1004", _Requester)
    End Try
  End Sub
#End Region

#Region "Grid Events"
  Private Sub OnCellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    Dim pFault As New clsFault()
    Try
      If e.RowIndex < 0 Then Exit Sub

      Dim columnName = dgvOrders.Columns(e.ColumnIndex).Name
      Dim row = dgvOrders.Rows(e.RowIndex)

      Select Case columnName
        Case "PaymentAction"
          QuickPayForRow(row)

        Case "PrintAction"
          PrintOrderForRow(row)

        Case "EditAction"
          EditOrderForRow(row)
      End Select

    Catch ex As Exception
      pFault.LogException(ex, "OnCellContentClick", "TRGT-OCHC-241225-1005", _Requester)
    End Try
  End Sub

  Private Sub OnRowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
    ColorRowsByStatus()
  End Sub

  Private Sub OnDataSourceChanged(sender As Object, e As EventArgs)
    ColorRowsByStatus()
  End Sub
#End Region

#Region "Business Operations"
  Private Sub QuickPayForRow(row As DataGridViewRow)
    Dim pFault As clsFault
    Try
      If _businessLogic Is Nothing Then
        InitializeBusinessLogic()
      End If

      ' קבלת ההזמנה
      Dim orderID As Long = CLng(row.Cells("ID").Value)
      If orderID <= 0 Then Exit Sub

      ' ביצוע תשלום מהיר
      pFault = _businessLogic.ProcessQuickPayment(orderID)
      If Not pFault.isOK Then
        MsgBox("שגיאה בעדכון תשלום: " & pFault.Message, MsgBoxStyle.Critical)
        Exit Sub
      End If

      ' עדכון השורה בגריד
      row.Cells("enmPaymentStatus").Value = enmPaymentStatus.Paid
      row.Cells("PaymentDate").Value = Date.Today

      ' צביעה מחדש
      ColorRowByStatus(row)

      MsgBox("התשלום נקלט בהצלחה!", MsgBoxStyle.Information, "תשלום הושלם")

    Catch ex As Exception
      pFault = New clsFault()
      pFault.LogException(ex, "QuickPayForRow", "TRGT-OCHC-241225-1006", _Requester)
      MsgBox("שגיאה בעדכון תשלום: " & ex.Message, MsgBoxStyle.Critical)
    End Try
  End Sub

  Private Sub PrintOrderForRow(row As DataGridViewRow)
    Dim pFault As New clsFault()
    Try
      Dim orderID As Long = CLng(row.Cells("ID").Value)
      MsgBox(String.Format("הדפסת הזמנה {0} - פונקציונליות זו תפותח בהמשך",
          row.Cells("OrderNumber").Value), MsgBoxStyle.Information)
      ' TODO: יישום הדפסה
    Catch ex As Exception
      pFault.LogException(ex, "PrintOrderForRow", "TRGT-OCHC-241225-1007", _Requester)
    End Try
  End Sub

  Private Sub EditOrderForRow(row As DataGridViewRow)
    Dim pFault As clsFault
    Try
      Dim orderID As Long = CLng(row.Cells("ID").Value)
      If orderID <= 0 Then Exit Sub

      ' טעינת ההזמנה
      Dim order As New clsOrderHeader()
      pFault = order.GetByID(orderID, _Requester)
      If Not pFault.isOK Then
        MsgBox("שגיאה בטעינת הזמנה: " & pFault.Message, MsgBoxStyle.Critical)
        Exit Sub
      End If

      ' פתיחה בחלון Popup
      Dim frmPopup As New frmPopup()
      Dim orderControl As New ctlccOrderHeader()
      orderControl.Requester = _Requester
      orderControl.LoadControl(order, New ctlccOrderHeader.clsLoadParameters(), _Requester)

      With frmPopup
        .Text = "עריכת הזמנה " & order.OrderNumber
        .Size = New Size(1200, 700)
        .StartPosition = FormStartPosition.CenterScreen
        orderControl.Dock = DockStyle.Fill
        .Controls.Add(orderControl)
        .ShowDialog()
      End With

      ' רענון הגריד
      RefreshGrid()

    Catch ex As Exception
      pFault = New clsFault()
      pFault.LogException(ex, "EditOrderForRow", "TRGT-OCHC-241225-1008", _Requester)
      MsgBox("שגיאה בפתיחת הזמנה: " & ex.Message, MsgBoxStyle.Critical)
    End Try
  End Sub
#End Region

#Region "Grid Styling"
  Private Sub ColorRowsByStatus()
    Try
      If dgvOrders Is Nothing Then Exit Sub

      For Each row As DataGridViewRow In dgvOrders.Rows
        ColorRowByStatus(row)
      Next
    Catch ex As Exception
      ' לא קריטי
    End Try
  End Sub

  Private Sub ColorRowByStatus(row As DataGridViewRow)
    Try
      ' צביעה לפי סטטוס תשלום
      Dim paymentStatus = row.Cells("enmPaymentStatus").Value
      If paymentStatus IsNot Nothing Then
        Select Case CType(paymentStatus, enmPaymentStatus)
          Case enmPaymentStatus.Pending
            row.DefaultCellStyle.BackColor = Color.LightPink

          Case enmPaymentStatus.PartiallyPaid
            row.DefaultCellStyle.BackColor = Color.LightYellow

          Case enmPaymentStatus.Paid
            row.DefaultCellStyle.BackColor = Color.LightGreen
        End Select
      End If

      ' הדגשת משלוחים להיום
      Dim deliveryDate = row.Cells("DeliveryDate").Value
      If deliveryDate IsNot Nothing AndAlso CDate(deliveryDate).Date = Date.Today Then
        row.DefaultCellStyle.Font = New Font(dgvOrders.Font, FontStyle.Bold)
        row.DefaultCellStyle.ForeColor = Color.DarkBlue
      End If

    Catch ex As Exception
      ' לא קריטי
    End Try
  End Sub
#End Region

#Region "Button Click Handlers"
  Private Sub btnQuickPay_Click(sender As Object, e As EventArgs) Handles btnQuickPay.Click
    Dim pFault As clsFault
    Try
      If dgvOrders Is Nothing Then Exit Sub

      ' קבלת השורות הנבחרות
      Dim selectedRows = dgvOrders.SelectedRows
      If selectedRows.Count = 0 Then
        MsgBox("אנא בחר הזמנות לעדכון תשלום", MsgBoxStyle.Information)
        Exit Sub
      End If

      ' אישור
      Dim result = MsgBox(String.Format("לסמן {0} הזמנות כשולמו?",
          selectedRows.Count), CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle))

      If result <> MsgBoxResult.Yes Then Exit Sub

      ' עדכון כל השורות הנבחרות
      Dim successCount As Integer = 0
      For Each row As DataGridViewRow In selectedRows
        Try
          QuickPayForRow(row)
          successCount += 1
        Catch ex As Exception
          ' המשך לשורה הבאה
        End Try
      Next

      MsgBox(String.Format("{0} הזמנות עודכנו בהצלחה", successCount),
          MsgBoxStyle.Information)

    Catch ex As Exception
      pFault = New clsFault()
      pFault.LogException(ex, "btnQuickPay_Click", "TRGT-OCHC-241225-1009", _Requester)
      MsgBox("שגיאה בעדכון תשלומים: " & ex.Message, MsgBoxStyle.Critical)
    End Try
  End Sub

  Private Sub btnFilterToday_Click(sender As Object, e As EventArgs) Handles btnFilterToday.Click
    Dim pFault As New clsFault()
    Try
      If dgvOrders Is Nothing Then Exit Sub

      ' סינון למשלוחים של היום
      For Each row As DataGridViewRow In dgvOrders.Rows
        Dim deliveryDate = row.Cells("DeliveryDate").Value
        If deliveryDate IsNot Nothing Then
          row.Visible = (CDate(deliveryDate).Date = Date.Today)
        Else
          row.Visible = False
        End If
      Next

    Catch ex As Exception
      pFault.LogException(ex, "btnFilterToday_Click", "TRGT-OCHC-241225-1010", _Requester)
    End Try
  End Sub

  Private Sub btnFilterUnpaid_Click(sender As Object, e As EventArgs) Handles btnFilterUnpaid.Click
    Dim pFault As New clsFault()
    Try
      If dgvOrders Is Nothing Then Exit Sub

      ' סינון להזמנות שלא שולמו
      For Each row As DataGridViewRow In dgvOrders.Rows
        Dim paymentStatus = row.Cells("enmPaymentStatus").Value
        If paymentStatus IsNot Nothing Then
          row.Visible = (CType(paymentStatus, enmPaymentStatus) <> enmPaymentStatus.Paid)
        End If
      Next

    Catch ex As Exception
      pFault.LogException(ex, "btnFilterUnpaid_Click", "TRGT-OCHC-241225-1011", _Requester)
    End Try
  End Sub

  Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
    Dim pFault As clsFault
    Try
      If dgvOrders Is Nothing OrElse dgvOrders.Rows.Count = 0 Then
        MsgBox("אין נתונים לייצוא", MsgBoxStyle.Information)
        Exit Sub
      End If

      If _businessLogic Is Nothing Then
        InitializeBusinessLogic()
      End If

      ' קריאה ללוגיקה העסקית
      pFault = _businessLogic.ExportToExcel(dgvOrders)
      If Not pFault.isOK Then
        MsgBox("שגיאה בייצוא: " & pFault.Message, MsgBoxStyle.Critical)
      End If

    Catch ex As Exception
      pFault = New clsFault()
      pFault.LogException(ex, "btnExportExcel_Click", "TRGT-OCHC-241225-1012", _Requester)
      MsgBox("שגיאה בייצוא: " & ex.Message, MsgBoxStyle.Critical)
    End Try
  End Sub

  Private Sub btnPrintDeliveries_Click(sender As Object, e As EventArgs) Handles btnPrintDeliveries.Click
    Dim pFault As New clsFault()
    Try
      MsgBox("הדפסת רשימת משלוחים - פונקציונליות זו תפותח בהמשך", MsgBoxStyle.Information)
      ' TODO: יישום הדפסת משלוחים
    Catch ex As Exception
      pFault.LogException(ex, "btnPrintDeliveries_Click", "TRGT-OCHC-241225-1013", _Requester)
    End Try
  End Sub
#End Region

#Region "Helper Methods"
  Private Sub RefreshGrid()
    Try
      ' רענון הגריד
      RaiseEvent RefreshRequired(Me, EventArgs.Empty)
    Catch ex As Exception
      ' לא קריטי
    End Try
  End Sub

  Public Event RefreshRequired As EventHandler
#End Region

End Class

#Region "Business Logic Module"
''' <summary>
''' מחלקה נפרדת ללוגיקה העסקית של OrderHeader
''' </summary>
Public Class OrderHeaderBusinessLogic
  Private _Requester As clsRequester
  Private _NextInvoiceNumber As Integer = 0

  Public Property Requester() As clsRequester
    Get
      Return _Requester
    End Get
    Set(value As clsRequester)
      _Requester = value
    End Set
  End Property

  Public Sub New()
  End Sub

  Public Sub New(requester As clsRequester)
    _Requester = requester
  End Sub

  ''' <summary>
  ''' מבצע תשלום מהיר להזמנה
  ''' </summary>
  Public Function ProcessQuickPayment(orderID As Long) As clsFault
    Dim pFault As clsFault
    Try
      ' טעינת ההזמנה
      Dim order As New clsOrderHeader()
      pFault = order.GetByID(orderID, _Requester)
      If Not pFault.isOK Then Return pFault

      ' בדיקה אם כבר שולם
      If order.PaymentStatus = enmPaymentStatus.Paid Then
        Return pFault.LogFreeTextFault(100, "ההזמנה כבר מסומנת כשולמה", "", "TRGT-BL-241225-2001", _Requester)
      End If

      ' אישור מהמשתמש
      Dim customerName = GetCustomerName(order.CustomerID)
      Dim result = MsgBox(String.Format("לסמן הזמנה {0} כשולמה?" & vbCrLf &
          "לקוח: {1}" & vbCrLf &
          "סכום: ₪{2:N2}",
          order.OrderNumber,
          customerName,
          order.TotalWithVAT),
          CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "אישור תשלום")

      If result <> MsgBoxResult.Yes Then
        Return pFault.SetOK()
      End If

      ' עדכון פרטי התשלום
      With order
        .PaymentStatus = enmPaymentStatus.Paid
        .PaymentDate = Date.Today
        .PaymentMethod = enmPaymentMethod.Credit ' ברירת מחדל

        ' יצירת מספר חשבונית אוטומטי
        If String.IsNullOrEmpty(.InvoiceNumber) Then
          .InvoiceNumber = GenerateInvoiceNumber()
        End If
      End With

      ' שמירה
      pFault = order.Update(_Requester)
      If pFault.isOK Then
        MsgBox(String.Format("מספר חשבונית: {0}", order.InvoiceNumber),
            MsgBoxStyle.Information, "תשלום הושלם")
      End If

      Return pFault

    Catch ex As Exception
      pFault = New clsFault()
      pFault.LogException(ex, "ProcessQuickPayment", "TRGT-BL-241225-2002", _Requester)
      Return pFault
    End Try
  End Function

  ''' <summary>
  ''' מייצא נתונים לאקסל
  ''' </summary>
  Public Function ExportToExcel(dgvOrders As DataGridView) As clsFault
    Dim pFault As New clsFault()
    Try
      ' בקשת מיקום לשמירה
      Dim saveDialog As New SaveFileDialog()
      With saveDialog
        .Title = "שמור קובץ Excel"
        .Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
        .FilterIndex = 1
        .FileName = String.Format("Orders_{0}.xlsx", Date.Now.ToString("yyyyMMdd_HHmmss"))
        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
      End With

      If saveDialog.ShowDialog() <> DialogResult.OK Then
        Return pFault.SetOK()
      End If

      ' הצגת סרגל התקדמות
      Dim progressForm As New Form()
      With progressForm
        .Text = "מייצא לאקסל..."
        .Size = New Size(400, 100)
        .StartPosition = FormStartPosition.CenterScreen
        .FormBorderStyle = FormBorderStyle.FixedDialog
        .MaximizeBox = False
        .MinimizeBox = False
      End With

      Dim progressBar As New ProgressBar()
      With progressBar
        .Dock = DockStyle.Fill
        .Style = ProgressBarStyle.Marquee
      End With
      progressForm.Controls.Add(progressBar)
      progressForm.Show()
      Application.DoEvents()

      Try
        If saveDialog.FileName.ToLower().EndsWith(".csv") Then
          ExportToCSV(dgvOrders, saveDialog.FileName)
        Else
          ExportToExcelHTML(dgvOrders, saveDialog.FileName)
        End If

        progressForm.Close()

        ' שאלה האם לפתוח את הקובץ
        If MsgBox("הייצוא הושלם בהצלחה!" & vbCrLf & "האם לפתוח את הקובץ?",
            CType(MsgBoxStyle.YesNo + MsgBoxStyle.Information, MsgBoxStyle), "ייצוא הושלם") = MsgBoxResult.Yes Then
          Process.Start(saveDialog.FileName)
        End If

        Return pFault.SetOK()

      Finally
        progressForm.Close()
        progressForm.Dispose()
      End Try

    Catch ex As Exception
      pFault.LogException(ex, "ExportToExcel", "TRGT-BL-241225-2003", _Requester)
      Return pFault
    End Try
  End Function

  Private Sub ExportToExcelHTML(dgvOrders As DataGridView, fileName As String)
    ' יצירת StringBuilder ל-HTML שיפתח באקסל
    Dim sb As New System.Text.StringBuilder()

    ' כותרת HTML
    sb.AppendLine("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>")
    sb.AppendLine("<head>")
    sb.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>")
    sb.AppendLine("<style>")
    sb.AppendLine("table { border-collapse: collapse; width: 100%; }")
    sb.AppendLine("th { background-color: #4CAF50; color: white; font-weight: bold; padding: 8px; text-align: right; border: 1px solid #ddd; }")
    sb.AppendLine("td { padding: 8px; text-align: right; border: 1px solid #ddd; }")
    sb.AppendLine(".number { text-align: left; mso-number-format:'0.00'; }")
    sb.AppendLine(".date { mso-number-format:'dd\/mm\/yyyy'; }")
    sb.AppendLine(".pending { background-color: #ffcccc; }")
    sb.AppendLine(".partial { background-color: #ffffcc; }")
    sb.AppendLine(".paid { background-color: #ccffcc; }")
    sb.AppendLine("</style>")
    sb.AppendLine("</head>")
    sb.AppendLine("<body>")

    ' כותרת
    sb.AppendLine("<h2>דוח הזמנות - " & Date.Now.ToString("dd/MM/yyyy HH:mm") & "</h2>")

    ' טבלה
    sb.AppendLine("<table>")
    sb.AppendLine("<thead><tr>")

    ' כותרות עמודות - רק עמודות נראות ולא כפתורים
    For Each col As DataGridViewColumn In dgvOrders.Columns
      If col.Visible AndAlso Not TypeOf col Is DataGridViewButtonColumn Then
        Dim headerText As String = GetColumnHeaderInHebrew(col.Name, col.HeaderText)
        sb.AppendLine("<th>" & headerText & "</th>")
      End If
    Next
    sb.AppendLine("</tr></thead>")

    ' גוף הטבלה
    sb.AppendLine("<tbody>")
    Dim visibleRowCount As Integer = 0

    For Each row As DataGridViewRow In dgvOrders.Rows
      If row.Visible AndAlso Not row.IsNewRow Then
        visibleRowCount += 1

        ' קביעת class לפי סטטוס
        Dim rowClass As String = ""
        If row.Cells("enmPaymentStatus").Value IsNot Nothing Then
          Select Case CType(row.Cells("enmPaymentStatus").Value, enmPaymentStatus)
            Case enmPaymentStatus.Pending
              rowClass = "pending"
            Case enmPaymentStatus.PartiallyPaid
              rowClass = "partial"
            Case enmPaymentStatus.Paid
              rowClass = "paid"
          End Select
        End If

        sb.AppendLine("<tr class='" & rowClass & "'>")

        For Each col As DataGridViewColumn In dgvOrders.Columns
          If col.Visible AndAlso Not TypeOf col Is DataGridViewButtonColumn Then
            Dim cellValue As String = GetCellValueForExport(row.Cells(col.Index))
            Dim cellClass As String = GetCellClass(col.Name)
            sb.AppendLine("<td class='" & cellClass & "'>" & cellValue & "</td>")
          End If
        Next

        sb.AppendLine("</tr>")
      End If
    Next
    sb.AppendLine("</tbody>")

    ' סיכומים
    If visibleRowCount > 0 Then
      sb.AppendLine("<tfoot>")
      sb.AppendLine("<tr style='font-weight: bold; background-color: #f2f2f2;'>")
      sb.AppendLine("<td colspan='3'>סה""כ שורות: " & visibleRowCount & "</td>")

      ' חישוב סיכומים
      Dim totalAmount As Decimal = 0
      Dim totalPaid As Decimal = 0
      Dim totalPending As Decimal = 0

      For Each row As DataGridViewRow In dgvOrders.Rows
        If row.Visible AndAlso Not row.IsNewRow Then
          If row.Cells("TotalWithVAT").Value IsNot Nothing Then
            Dim amount As Decimal = CDec(row.Cells("TotalWithVAT").Value)
            totalAmount += amount

            If row.Cells("enmPaymentStatus").Value IsNot Nothing Then
              Select Case CType(row.Cells("enmPaymentStatus").Value, enmPaymentStatus)
                Case enmPaymentStatus.Paid
                  totalPaid += amount
                Case enmPaymentStatus.Pending
                  totalPending += amount
              End Select
            End If
          End If
        End If
      Next

      sb.AppendLine("<td colspan='3'>סה""כ כולל: ₪" & totalAmount.ToString("N2") & "</td>")
      sb.AppendLine("<td colspan='3'>שולם: ₪" & totalPaid.ToString("N2") & "</td>")
      sb.AppendLine("<td colspan='3'>ממתין: ₪" & totalPending.ToString("N2") & "</td>")
      sb.AppendLine("</tr>")
      sb.AppendLine("</tfoot>")
    End If

    sb.AppendLine("</table>")
    sb.AppendLine("</body>")
    sb.AppendLine("</html>")

    ' שמירה לקובץ
    System.IO.File.WriteAllText(fileName, sb.ToString(), System.Text.Encoding.UTF8)
  End Sub

  Private Sub ExportToCSV(dgvOrders As DataGridView, fileName As String)
    Dim sb As New System.Text.StringBuilder()

    ' כותרות
    Dim headers As New List(Of String)
    For Each col As DataGridViewColumn In dgvOrders.Columns
      If col.Visible AndAlso Not TypeOf col Is DataGridViewButtonColumn Then
        headers.Add(GetColumnHeaderInHebrew(col.Name, col.HeaderText))
      End If
    Next
    sb.AppendLine(String.Join(",", headers))

    ' נתונים
    For Each row As DataGridViewRow In dgvOrders.Rows
      If row.Visible AndAlso Not row.IsNewRow Then
        Dim values As New List(Of String)

        For Each col As DataGridViewColumn In dgvOrders.Columns
          If col.Visible AndAlso Not TypeOf col Is DataGridViewButtonColumn Then
            Dim cellValue As String = GetCellValueForExport(row.Cells(col.Index))
            ' הוספת מרכאות אם יש פסיקים
            If cellValue.Contains(",") Then
              cellValue = """" & cellValue.Replace("""", """""") & """"
            End If
            values.Add(cellValue)
          End If
        Next

        sb.AppendLine(String.Join(",", values))
      End If
    Next

    ' שמירה עם BOM לעברית
    Dim encoding = New System.Text.UTF8Encoding(True)
    System.IO.File.WriteAllText(fileName, sb.ToString(), encoding)
  End Sub

  Private Function GenerateInvoiceNumber() As String
    Try
      ' קבלת השנה הנוכחית
      Dim currentYear = Date.Today.Year

      ' קבלת המספר הבא
      If _NextInvoiceNumber = 0 Then
        ' TODO: לקבל מספר אמיתי מבסיס הנתונים
        _NextInvoiceNumber = 1
      End If

      _NextInvoiceNumber += 1
      Return String.Format("INV-{0}-{1:D4}", currentYear, _NextInvoiceNumber)

    Catch ex As Exception
      ' במקרה של שגיאה, החזר מספר אקראי
      Return "INV-" & Date.Now.ToString("yyyyMMddHHmmss")
    End Try
  End Function

  Private Function GetCustomerName(customerID As Long) As String
    Try
      Dim customer As New clsCustomer()
      Dim pFault = customer.GetByID(customerID, _Requester)
      If pFault.isOK Then
        Return customer.CustomerName
      End If
      Return ""
    Catch ex As Exception
      Return ""
    End Try
  End Function

  Private Function GetColumnHeaderInHebrew(columnName As String, defaultHeader As String) As String
    Select Case columnName.ToLower()
      Case "id"
        Return "קוד"
      Case "ordernumber"
        Return "מספר הזמנה"
      Case "customerid"
        Return "קוד לקוח"
      Case "customername"
        Return "שם לקוח"
      Case "orderdate"
        Return "תאריך הזמנה"
      Case "totalamount"
        Return "סכום לפני מע""מ"
      Case "vatamount"
        Return "מע""מ"
      Case "totalwithvat"
        Return "סה""כ כולל מע""מ"
      Case "enmpaymentstatus"
        Return "סטטוס תשלום"
      Case "enmpaymentmethod"
        Return "אמצעי תשלום"
      Case "paymentdate"
        Return "תאריך תשלום"
      Case "invoicenumber"
        Return "מספר חשבונית"
      Case "enmdeliverymethod"
        Return "שיטת משלוח"
      Case "deliverydate"
        Return "תאריך משלוח"
      Case "enmdeliveryday"
        Return "יום משלוח"
      Case "enmorderstatus"
        Return "סטטוס הזמנה"
      Case "notes"
        Return "הערות"
      Case "clc_quarter"
        Return "רבעון"
      Case "clc_monthname"
        Return "חודש"
      Case Else
        Return defaultHeader
    End Select
  End Function

  Private Function GetCellValueForExport(cell As DataGridViewCell) As String
    Try
      If cell.Value Is Nothing OrElse IsDBNull(cell.Value) Then
        Return ""
      End If

      ' טיפול בערכי enum
      If cell.OwningColumn.Name.StartsWith("enm") Then
        Return GetEnumDisplayValue(cell.Value)
      End If

      ' טיפול בתאריכים
      If TypeOf cell.Value Is Date Then
        Return CDate(cell.Value).ToString("dd/MM/yyyy")
      End If

      ' טיפול במספרים
      If TypeOf cell.Value Is Decimal OrElse TypeOf cell.Value Is Double Then
        Return CDec(cell.Value).ToString("N2")
      End If

      Return cell.Value.ToString()

    Catch ex As Exception
      Return ""
    End Try
  End Function

  Private Function GetEnumDisplayValue(value As Object) As String
    Try
      If value Is Nothing Then Return ""

      Select Case value.GetType().Name
        Case "enmPaymentStatus"
          Select Case CType(value, enmPaymentStatus)
            Case enmPaymentStatus.Pending
              Return "ממתין לתשלום"
            Case enmPaymentStatus.PartiallyPaid
              Return "שולם חלקית"
            Case enmPaymentStatus.Paid
              Return "שולם"
            Case Else
              Return value.ToString()
          End Select

        Case "enmPaymentMethod"
          Select Case CType(value, enmPaymentMethod)
            Case enmPaymentMethod.Cash
              Return "מזומן"
            Case enmPaymentMethod.Credit
              Return "אשראי"
            Case enmPaymentMethod.Transfer
              Return "העברה בנקאית"
            Case Else
              Return value.ToString()
          End Select

        Case "enmDeliveryMethod"
          Select Case CType(value, enmDeliveryMethod)
            Case enmDeliveryMethod.Biobee
              Return "ביובי"
            Case enmDeliveryMethod.Netzach
              Return "נצח"
            Case enmDeliveryMethod.Tzofar
              Return "צופר"
            Case Else
              Return value.ToString()
          End Select

        Case "enmOrderStatus"
          Select Case CType(value, enmOrderStatus)
            Case enmOrderStatus.New
              Return "חדש"
            Case enmOrderStatus.Processing
              Return "בתהליך"
            Case enmOrderStatus.Completed
              Return "הושלם"
            Case enmOrderStatus.Cancelled
              Return "בוטל"
            Case Else
              Return value.ToString()
          End Select

        Case Else
          Return value.ToString()
      End Select

    Catch ex As Exception
      Return value.ToString()
    End Try
  End Function

  Private Function GetCellClass(columnName As String) As String
    Select Case columnName.ToLower()
      Case "totalamount", "vatamount", "totalwithvat", "quantity", "unitprice", "linetotal"
        Return "number"
      Case "orderdate", "paymentdate", "deliverydate", "debtdate"
        Return "date"
      Case Else
        Return ""
    End Select
  End Function
End Class
#End Region