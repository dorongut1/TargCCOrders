Partial Public Class ctlPnlc_Permission 
 
  Private Sub ctlPnlc_Permission_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_Permission_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtProcessChosen = True 
    '_CancelEvtRoleChosen = True 
  End Sub 
End Class 
