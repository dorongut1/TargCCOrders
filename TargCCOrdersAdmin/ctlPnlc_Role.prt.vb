Partial Public Class ctlPnlc_Role 
 
  Private Sub ctlPnlc_Role_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_Role_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtPermissionChosen = True 
    '_CancelEvtUserChosen = True 
    '_CancelEvtBaseRoleChosen = True 
  End Sub 
End Class 
