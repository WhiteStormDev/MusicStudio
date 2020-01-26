<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditClient.aspx.cs" Inherits="MusicStudio.EditClient" %>
<%@ Register Src="~/Controls/ClientControl.ascx" TagName="ClientControl" TagPrefix="mus" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <mus:ClientControl ID="clientEditor" runat="server"/>
        <hr />
        <asp:Button runat="server" Text="Save" ID="btnSave"/>
        <asp:Button runat="server" Text="Cancel" ID="btnCancel"/>
    </form>
</body>
</html>
