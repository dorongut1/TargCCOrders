<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangePassword.aspx.vb" Inherits="TargCCOrders.WS.ChangePassword" %>
<!DOCTYPE html>
<html dir="<%=prtDir%>">
<head>
    <title>TargCCOrders Change Password</title>
    <!-- Add your CSS stylesheets here -->
    <!--
    <link rel="stylesheet" href="<%=csslocation%>">
    <link rel="stylesheet" href="ws.css">
        -->
    <link rel="stylesheet" href="ws.css">
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
                        <asp:Label ID="lblTitle" runat="server" Text="Change Password"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="">
                        <asp:Label ID="lblInstructions" runat="server" Text="Hi! Change your password below"></asp:Label>
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
                        <asp:Label ID="lblPresentPassword" runat="server" Text="Present Password:"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtPresentPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
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
                        <asp:Label ID="lblNewPassword" runat="server" Text="New Password:"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="<%=prtAlignRight %>">
                        <asp:Label ID="lblRetypePassword" runat="server" Text="Retype Password:"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="">&nbsp;
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
                        <asp:Button ID="btnSend" runat="server" align="center" Text="Change Password" OnClick="btnSend_Click" CssClass="btn-login" />
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