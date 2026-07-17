Public Class ForgotPasswordRoot
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If String.IsNullOrEmpty(My.Settings.AuthenticationServer) Then
      Response.Redirect("CC/ForgotPassword.aspx")
    Else
      Dim pURL As String = My.Settings.AuthenticationServer
      Response.Redirect(pURL)
    End If
  End Sub
End Class