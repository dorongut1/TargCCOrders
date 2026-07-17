Partial Public Class ctlPnlc_UserLoginKey 
 
  Private Sub ctlPnlc_UserLoginKey_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_UserLoginKey_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtUserChosen = True 
  End Sub 
End Class 
