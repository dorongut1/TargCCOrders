Partial Public Class ctlPnlc_User 
 
  Private Sub ctlPnlc_User_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_User_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtJobAlertRecipientChosen = True 
    '_CancelEvtLoggedAlertsForAffectedUserChosen = True 
    '_CancelEvtLoggedRequestChosen = True 
    '_CancelEvtUserPermissionChosen = True 
    '_CancelEvtUserStatusChosen = True 
    '_CancelEvtRoleChosen = True 
  End Sub 
End Class 
