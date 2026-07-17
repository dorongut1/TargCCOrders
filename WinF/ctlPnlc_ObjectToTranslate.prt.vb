Partial Public Class ctlPnlc_ObjectToTranslate 
 
  Private Sub ctlPnlc_ObjectToTranslate_evtLoaded() Handles Me.evtLoaded 
    'chkGrid.Checked = True 
  End Sub 
  
  Private Sub ctlPnlc_ObjectToTranslate_evtOverrideLoadControl(ByRef pIntelliComboMakeDumb As Boolean, ByRef pIntelliComboDropDownStyle As ComboBoxStyle, ByRef pExitSubAfterEvent As Boolean) Handles Me.evtOverrideLoadControl 
    '_CancelEvtObjectTranslationChosen = True 
  End Sub 
End Class 
