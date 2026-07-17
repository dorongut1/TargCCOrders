Partial Public Class ctlPnlccCustomer 
 
  Private Sub ctlPnlccCustomer_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlccCustomer_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtCustomerDebtChosen = True 
    '_CancelEvtOrderHeaderChosen = True 
  End Sub 
End Class 
