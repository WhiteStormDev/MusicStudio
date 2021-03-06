﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AbonementControl.ascx.cs" Inherits="MusicStudioApplication.Controls.AbonementControl" %>
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