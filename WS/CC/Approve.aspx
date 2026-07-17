<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Approve.aspx.vb" Inherits="TargCCOrders.WS.Approve" %>
<!DOCTYPE html>
<html dir="<%=prtDir%>">
<head runat="server">
    <title>TargCCOrders Approve Request</title>
</head>
<body style="font-family: Arial; font-size: 12px">
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellspacing="5" cellpadding="0" align="center">
                <tr>
                    <td width="50%">&nbsp;</td>
                    <td width="50%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Image ID="imgLogo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="font-size: 24px; font-weight: bold">
                        <asp:Label ID="lblApproveRequest" runat="server" Text="Approve Request"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td class="auto-style1"></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblHeader" runat="server" Text="Hi! Please enter your code"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="<%=prtAlignRight %>">
                        <asp:Label ID="lblCodeReceived" runat="server" Text="Code Received"></asp:Label>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtCode" Width="50%" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSend" runat="server" Font-Size="12px" TextMode="Password" Text="Send Code" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="font-size: 14px; text-align: center">
                        <span style="color: red">
                            <asp:Label ID="lblResponseFailed" runat="server" Text=""></asp:Label>
                        </span>
                        <span style="color: green">
                            <asp:Label ID="lblResponseSucceeded" runat="server" Text=""></asp:Label>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>