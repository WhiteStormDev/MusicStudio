<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientControl.ascx.cs" Inherits="MusicStudioApplication.Controls.ClientControl" %>
<%@ Register Assembly="ServerControls" Namespace="ServerControls" TagPrefix="sctr" %>
        <div>
            <asp:Label runat="server" Text="Surname"></asp:Label>
            <asp:TextBox runat="server" ID ="txtSurname"></asp:TextBox>
        </div>        
        <div>
            <asp:Label runat="server" Text="Name"></asp:Label>
            <asp:TextBox runat="server" ID ="txtName"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" Text="SecondName"></asp:Label>
            <asp:TextBox runat="server" ID ="txtSecondName"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" Text="Phone"></asp:Label>
            <sctr:PhoneBox runat="server" ID ="txtPhone"></sctr:PhoneBox>
        </div>