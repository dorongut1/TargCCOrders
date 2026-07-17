Partial Public Class ctlPnlc_LoggedLogin 
 
  Private Sub ctlPnlc_LoggedLogin_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_LoggedLogin_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtLoggedAlertChosen = True 
    '_CancelEvtLoggedRequestChosen = True 
  End Sub 
End Class 
