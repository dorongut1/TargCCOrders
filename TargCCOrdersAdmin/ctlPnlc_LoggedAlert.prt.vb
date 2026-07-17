Partial Public Class ctlPnlc_LoggedAlert 
 
  Private Sub ctlPnlc_LoggedAlert_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_LoggedAlert_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtLoggedJobChosen = True 
    '_CancelEvtLoggedLoginChosen = True 
    '_CancelEvtAffectedUserChosen = True 
  End Sub 
End Class 
