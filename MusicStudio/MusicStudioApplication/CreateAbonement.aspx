<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAbonement.aspx.cs" Inherits="MusicStudioApplication.CreateAbonement" %>
<%@ Register Src="~/Controls/AbonementControl.ascx" TagName="AbonementControl" TagPrefix="mab" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <mab:AbonementControl ID="abonementEditor" runat="server" />
        <asp:Button runat="server" Text="Save" ID="btnSave"/>
        <asp:Button runat="server" Text="Cancel" ID="btnCancel"/>
    </form>
</body>
</html>