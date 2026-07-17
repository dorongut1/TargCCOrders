Partial Public Class frmLogin 
  Private Sub frmLogin_evtFormLoaded() Handles Me.evtFormLoaded 
    Me.Icon = New Icon(My.Computer.FileSystem.CurrentDirectory & "\" & "TargetIcon32.ico") 
    pctLogo.Image = New Bitmap(My.Computer.FileSystem.CurrentDirectory & "\" & "TargetSmall.JPG") 
  End Sub 
End Class 
