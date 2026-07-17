Partial Public Class ctlPnlccProduct 
 
  Private Sub ctlPnlccProduct_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlccProduct_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtOrderLineChosen = True 
    '_CancelEvtProductPriceChosen = True 
  End Sub 
End Class 
