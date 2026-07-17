<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ForgotPassword.aspx.vb" Inherits="TargCCOrders.WS.ForgotPassword" %>
<!DOCTYPE html>
<html dir="<%=prtDir%>">
<head>
    <title>TargCCOrders Forgot Password</title>
    <!-- Add your CSS stylesheets here -->
    <link rel="stylesheet" href="<%=csslocation%>">
</head>
<body>
    <form id="form1" runat="server">

        <div class="header-container">
            <table border="0" width="100%">
                <tr height="10%">
                    <td>
                        <div class="logo">
                            <!-- Replace with your logo image -->
                            <asp:Image ID="imgLogo" runat="server" />
                        </div>
                    </td>
                    <td>
                        <div class="title">
                            <asp:Label ID="lblSystemName" runat="server" Text="SystemName"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;   
                    </td>
                    <td>&nbsp;   
                    </td>
                </tr>
            </table>
        </div>
        <div class="login-container">
            <table border="0" width="100%">
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="form-header">
                        <asp:Label ID="lblTitle" runat="server" Text="Forgot My Password"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="<%=prtAlignRight %>">
                        <asp:Label ID="lblUserName" runat="server" Text="User Name:"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="<%=prtAlignRight %>">
                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="<%=prtAlignRight %>">
                        <asp:Label ID="lblCellphone" runat="server" Text="Cellphone:"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtCellphone" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="">
                        <asp:Label ID="lblInstructions" runat="server" Text="Please enter your details below.<br>We will send you a link where you can create a new password"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td align="center">
                        <asp:Button ID="btnRequestLink" runat="server" align="center" Text="Login" OnClick="btnRequestLink_Click" CssClass="btn-login" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td colspan="1" align="center">
                        <asp:Label ID="lblResponseFailed" runat="server" Text="TheError" CssClass="form-error"></asp:Label>
                        <asp:Label ID="lblResponseSucceeded" runat="server" Text="TheSuccess" CssClass="form-success"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>