Partial Public Class ctlPnlccCustomerDebt 
 
  Private Sub ctlPnlccCustomerDebt_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlccCustomerDebt_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtCustomerChosen = True 
    '_CancelEvtOrderHeaderChosen = True 
  End Sub 
End Class 
