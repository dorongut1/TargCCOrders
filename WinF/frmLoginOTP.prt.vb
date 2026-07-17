Partial Public Class frmLoginOTP 
  Private Sub frmLoginOTP_evtFormLoaded() Handles Me.evtFormLoaded 
    Me.Icon = New Icon(My.Computer.FileSystem.CurrentDirectory & "\" & "TargetIcon32.ico") 
    pctLogo.Image = New Bitmap(My.Computer.FileSystem.CurrentDirectory & "\" & "TargetSmall.JPG") 
  End Sub 
End Class 
