Partial Public Class clsOrderLine

    'Private Sub clsOrderLine_evtBeforeUpdateWithRequester(vWhichColumn As enmUpdateType, ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, vRequester As clsRequester, ByRef rFault As clsFault) Handles Me.evtBeforeUpdateWithRequester

    '    'get orderheader
    '    Dim pOrderHeader As New clsOrderHeader(_OrderHeaderID, clsEnums.enmLoadParent.DoNotLoad, vRequester, rFault, vMustExist:=True) : If Not rFault.isOK Then Return
    '    'now get the customer
    '    Dim pCustomer As New clsCustomer(pOrderHeader.CustomerID, vRequester, rFault, vMustExist:=True) : If Not rFault.isOK Then Return

    '    Dim pProductPrice As New clsProductPrice()
    '    rFault = pProductPrice.GetByProductIDAndCustomerType(_ProductID, pCustomer.CustomerType, vRequester, vMustExist:=True) : If Not rFault.isOK Then Return

    '    '_UnitPrice = pProductPrice.SellingPrice

    'End Sub
End Class

