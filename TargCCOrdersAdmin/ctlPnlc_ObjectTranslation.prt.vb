Partial Public Class ctlPnlc_ObjectTranslation 
 
  Private Sub ctlPnlc_ObjectTranslation_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_ObjectTranslation_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtObjectToTranslateChosen = True 
  End Sub 
End Class 
