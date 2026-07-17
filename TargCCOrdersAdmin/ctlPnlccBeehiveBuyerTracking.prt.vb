Partial Public Class ctlPnlccBeehiveBuyerTracking 
 
  Private Sub ctlPnlccBeehiveBuyerTracking_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlccBeehiveBuyerTracking_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtCustomerChosen = True 
  End Sub 
End Class 
