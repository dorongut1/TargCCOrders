Partial Public Class ctlPnlc_LoggedJob 
 
  Private Sub ctlPnlc_LoggedJob_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_LoggedJob_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtJobChosen = True 
    '_CancelEvtLoggedAlertChosen = True 
  End Sub 
End Class 
