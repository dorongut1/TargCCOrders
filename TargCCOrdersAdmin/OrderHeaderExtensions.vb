Imports System.Runtime.CompilerServices
Imports TargCCOrders.DataController.clsEnums

''' <summary>
''' Extension Methods for OrderHeader operations
''' </summary>
Public Module OrderHeaderExtensions

  ''' <summary>
  ''' מבצע תשלום מהיר להזמנה
  ''' </summary>
  <Extension()>
  Public Function QuickPay(order As clsOrderHeader, 
                           paymentMethod As enmPaymentMethod, 
                           requester As clsRequester) As clsFault
    Dim pFault As New clsFault()
    Try
      With order
        .PaymentStatus = enmPaymentStatus.Paid
        .PaymentDate = Date.Today
        .PaymentMethod = paymentMethod
        
        ' יצירת מספר חשבונית אוטומטי אם אין
        If String.IsNullOrEmpty(.InvoiceNumber) Then
          .InvoiceNumber = GenerateInvoiceNumber()
        End If
      End With
      
      ' שמירה
      pFault = order.Update(requester)
      Return pFault
      
    Catch ex As Exception
      pFault.LogException(ex, "QuickPay", "TRGT-EXT-241225-3001", requester)
      Return pFault
    End Try
  End Function
  
  ''' <summary>
  ''' בדיקה אם ההזמנה דורשת תשלום
  ''' </summary>
  <Extension()>
  Public Function RequiresPayment(order As clsOrderHeader) As Boolean
    Return order.PaymentStatus <> enmPaymentStatus.Paid
  End Function
  
  ''' <summary>
  ''' בדיקה אם ההזמנה למשלוח היום
  ''' </summary>
  <Extension()>
  Public Function IsDeliveryToday(order As clsOrderHeader) As Boolean
    Return order.DeliveryDate.Date = Date.Today
  End Function
  
  ''' <summary>
  ''' מחזיר צבע לפי סטטוס התשלום
  ''' </summary>
  <Extension()>
  Public Function GetPaymentStatusColor(order As clsOrderHeader) As Color
    Select Case order.PaymentStatus
      Case enmPaymentStatus.Pending
        Return Color.LightPink
      Case enmPaymentStatus.PartiallyPaid
        Return Color.LightYellow
      Case enmPaymentStatus.Paid
        Return Color.LightGreen
      Case Else
        Return Color.White
    End Select
  End Function
  
  ''' <summary>
  ''' יוצר מספר חשבונית אוטומטי
  ''' </summary>
  Private Function GenerateInvoiceNumber() As String
    Try
      Dim currentYear = Date.Today.Year
      Dim timestamp = Date.Now.ToString("HHmmss")
      Return String.Format("INV-{0}-{1}", currentYear, timestamp)
    Catch ex As Exception
      Return "INV-" & Guid.NewGuid().ToString().Substring(0, 8)
    End Try
  End Function
  
End Module

''' <summary>
''' Extension Methods for OrderHeaderCol operations
''' </summary>
Public Module OrderHeaderColExtensions
  
  ''' <summary>
  ''' מסנן את האוסף להזמנות שלא שולמו
  ''' </summary>
  <Extension()>
  Public Function FilterUnpaid(collection As clsOrderHeaderCol) As clsOrderHeaderCol
    Dim filtered As New clsOrderHeaderCol()
    For Each order As clsOrderHeader In collection
      If order.PaymentStatus <> enmPaymentStatus.Paid Then
        filtered.Add(order)
      End If
    Next
    Return filtered
  End Function
  
  ''' <summary>
  ''' מסנן את האוסף למשלוחים של היום
  ''' </summary>
  <Extension()>
  Public Function FilterTodayDeliveries(collection As clsOrderHeaderCol) As clsOrderHeaderCol
    Dim filtered As New clsOrderHeaderCol()
    For Each order As clsOrderHeader In collection
      If order.DeliveryDate.Date = Date.Today Then
        filtered.Add(order)
      End If
    Next
    Return filtered
  End Function
  
  ''' <summary>
  ''' מחשב סכום כולל של הזמנות
  ''' </summary>
  <Extension()>
  Public Function CalculateTotalAmount(collection As clsOrderHeaderCol) As Decimal
    Dim total As Decimal = 0
    For Each order As clsOrderHeader In collection
      total += order.TotalWithVAT
    Next
    Return total
  End Function
  
  ''' <summary>
  ''' מחזיר סטטיסטיקות תשלום
  ''' </summary>
  <Extension()>
  Public Function GetPaymentStatistics(collection As clsOrderHeaderCol) As PaymentStatistics
    Dim stats As New PaymentStatistics()
    
    For Each order As clsOrderHeader In collection
      Select Case order.PaymentStatus
        Case enmPaymentStatus.Paid
          stats.PaidCount += 1
          stats.PaidAmount += order.TotalWithVAT
        Case enmPaymentStatus.Pending
          stats.PendingCount += 1
          stats.PendingAmount += order.TotalWithVAT
        Case enmPaymentStatus.PartiallyPaid
          stats.PartialCount += 1
          stats.PartialAmount += order.TotalWithVAT
      End Select
      stats.TotalCount += 1
      stats.TotalAmount += order.TotalWithVAT
    Next
    
    Return stats
  End Function
  
End Module

''' <summary>
''' מחלקה לאחסון סטטיסטיקות תשלום
''' </summary>
Public Class PaymentStatistics
  Public Property TotalCount As Integer
  Public Property TotalAmount As Decimal
  Public Property PaidCount As Integer
  Public Property PaidAmount As Decimal
  Public Property PendingCount As Integer
  Public Property PendingAmount As Decimal
  Public Property PartialCount As Integer
  Public Property PartialAmount As Decimal
  
  Public ReadOnly Property PaidPercentage As Decimal
    Get
      If TotalCount = 0 Then Return 0
      Return CDec(PaidCount) / CDec(TotalCount) * 100
    End Get
  End Property
  
  Public ReadOnly Property CollectionRate As Decimal
    Get
      If TotalAmount = 0 Then Return 0
      Return PaidAmount / TotalAmount * 100
    End Get
  End Property
End Class