Partial Public Class ctlPnlccOrderHeader 
 
  Private Sub ctlPnlccOrderHeader_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlccOrderHeader_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtCustomerDebtChosen = True 
    '_CancelEvtDeliveryChosen = True 
    '_CancelEvtOrderLineChosen = True 
    '_CancelEvtSupplierOrderChosen = True 
    '_CancelEvtCustomerChosen = True 
  End Sub 
End Class 
