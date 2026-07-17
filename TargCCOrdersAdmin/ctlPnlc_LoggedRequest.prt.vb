Partial Public Class ctlPnlc_LoggedRequest 
 
  Private Sub ctlPnlc_LoggedRequest_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_LoggedRequest_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtLoggedLoginChosen = True 
    '_CancelEvtUserChosen = True 
  End Sub 
End Class 
