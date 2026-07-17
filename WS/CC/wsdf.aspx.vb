'Created by TargCC Version 4.0.6.3
Partial Public Class wsdf 
    Inherits System.Web.UI.Page 
 
  Protected WithEvents Head1 As Global.System.Web.UI.HtmlControls.HtmlHead 
  Protected WithEvents form1 As Global.System.Web.UI.HtmlControls.HtmlForm 
  Protected WithEvents lblText As Global.System.Web.UI.WebControls.Label 
 
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 
    Dim pIn As String = Request("WSPwd") 
    If pIn = MyConfig.WSDFPwd Then 'If it doesn't exist, create it in a partial class as per the tutorial 
      Dim pSb As New System.Text.StringBuilder 
 
      pSb.AppendLine("AppName: " & Me.ToString) 
      pSb.AppendLine("CallerIP: " & System.Web.HttpContext.Current.Request.UserHostAddress) 
      pSb.AppendLine("User: " & System.Web.HttpContext.Current.Request.LogonUserIdentity.Name) 
 
      Dim pWSLogonUserGroups As String = "" 
      pWSLogonUserGroups = "#" 
      For Each pGroup As System.Security.Principal.IdentityReference In System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups 
        Try 
          Dim pStrg As String = pGroup.Translate(GetType(System.Security.Principal.NTAccount)).ToString() 
          If pStrg.IndexOf("\") >= 0 Then 
            pStrg = pStrg.Split("\"c)(1) 
          End If 
          pWSLogonUserGroups &= pStrg & "#" 
        Catch ex As Exception 
          pWSLogonUserGroups &= ":" & ex.Message 
        End Try 
      Next 
      pSb.AppendLine("UserGroups: " & pWSLogonUserGroups) 
      pSb.AppendLine("PhysicalApplicationPath: " & My.Request.PhysicalApplicationPath) 
      pSb.AppendLine("PhysicalPath: " & My.Request.PhysicalPath) 
      pSb.AppendLine("UserHostAddress: " & My.Request.UserHostAddress) 
      pSb.AppendLine("AuthenticationType: " & System.Web.HttpContext.Current.Request.LogonUserIdentity.AuthenticationType) 
      pSb.AppendLine("Server Time: " & DateTime.Now.ToString) 
      lblText.Text = pSb.ToString.Replace(Environment.NewLine, "<br/><br/>") 
    Else 
      lblText.Text = DateTime.Now.ToString 
    End If 
  End Sub 
 
End Class 
