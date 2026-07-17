Partial Public Class ctlPnlccSupplierOrder 
 
  Private Sub ctlPnlccSupplierOrder_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlccSupplierOrder_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtOrderHeaderChosen = True 
  End Sub 
End Class 
