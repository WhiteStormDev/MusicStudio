<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateClient.aspx.cs" Inherits="MusicStudio.CreateClient" %>
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
        <div>
            <asp:Label runat="server" Text="Предмет"></asp:Label>
            <asp:DropDownList runat="server" ID ="ddlSubject" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
        </div>
        <div>
            <asp:Label runat="server" Text="Преподаватель"></asp:Label>
            <asp:DropDownList runat="server" ID ="ddlTeacher" DataTextField="Surname" DataValueField="Id"></asp:DropDownList>
        </div>
        <div>
            <asp:Label runat="server" Text="Количество занятий"></asp:Label>
            <asp:TextBox runat="server" ID ="txtLessonCount"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" Text="Дата начала действия абонемента"></asp:Label>
            <asp:Calendar runat="server" ID="calendarStart"></asp:Calendar>
        </div>
        <div>
            <asp:Label runat="server" Text="Дата конца действия абонемента"></asp:Label>
            <asp:Calendar runat="server" ID="calendarEnd"></asp:Calendar>
        </div>
        <asp:Button runat="server" Text="Save" ID="btnSave"/>
        <asp:Button runat="server" Text="Cancel" ID="btnCancel"/>
    </form>
</body>
</html>
