Partial Public Class ctlPnlc_Job 
 
  Private Sub ctlPnlc_Job_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_Job_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtJobAlertRecipientChosen = True 
    '_CancelEvtLoggedJobChosen = True 
  End Sub 
End Class 
