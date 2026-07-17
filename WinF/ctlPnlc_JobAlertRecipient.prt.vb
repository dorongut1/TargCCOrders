Partial Public Class ctlPnlc_JobAlertRecipient 
 
  Private Sub ctlPnlc_JobAlertRecipient_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_JobAlertRecipient_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtJobChosen = True 
    '_CancelEvtUserChosen = True 
  End Sub 
End Class 
