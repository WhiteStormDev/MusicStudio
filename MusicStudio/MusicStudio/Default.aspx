<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MusicStudio.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    < <form id="form1" runat="server">
        <asp:Panel ID="pnlLogin" runat="server">
			<asp:Label runat="server">Login</asp:Label>
			<asp:TextBox runat="server" ID="txtLogin"/>
			<asp:Label runat="server">Password</asp:Label>
			<asp:TextBox runat="server" ID="txtPassword" TextMode="Password"/>
			<asp:Button runat="server" ID="btnSubmit" Text="Log In"/>
        </asp:Panel>
		<div>
			<asp:Label ID="lblOutput" runat="server" />
		</div>
		
    </form>
</body>
</html>
