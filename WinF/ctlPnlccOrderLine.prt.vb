Partial Public Class ctlPnlccOrderLine 
 
  Private Sub ctlPnlccOrderLine_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlccOrderLine_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtOrderHeaderChosen = True 
    '_CancelEvtProductChosen = True 
  End Sub 
End Class 
