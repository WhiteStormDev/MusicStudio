<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="MusicStudio.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
</head>
<body>
    <p id="myweather">Weather placeholder</p>
    <script type="text/javascript">
        const Http = new XMLHttpRequest();
        const url = 'https://api.weather.yandex.ru/v1/forecast/';
        Http.open("GET", url);
        Http.setRequestHeader("X-Yandex-API-Key", "5c108abf-2a50-4b38-835d-d6a3120f34ee")
        Http.setRequestHeader("Access-Control-Allow-Origin", "https://api.weather.yandex.ru")
        
        Http.send();

        Http.onreadystatechange = function() {
            var obj = JSON.parse(Http.responseText);
            var s = "Moscow = " + obj['fact']['temp'];
            console.log(s);
            $("#myweather").text(s);
        }
    </script>
    <form id="form1" runat="server">
        <div>
            <h1>Clients</h1>
            <button id="buttonAddClient" runat="server">Add New Client</button>
            <asp:Panel ID="errPanel" runat="server" ForeColor="Red" Visible="false">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <asp:Label ID="dbexLblMsg" runat="server"></asp:Label>
                <asp:Button ID ="buttonDel" visible="false" runat="server" Text="Удалить вместе с абонементами" />
            </asp:Panel>
            <asp:DataGrid runat="server" ID="dgClients" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="6" GridLines="Horizontal" AutoGenerateColumns="false">
                <Columns>
                    <asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
                    <asp:BoundColumn DataField="FirstName" HeaderText="Имя"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Surname" HeaderText="Фамилия"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SecondName" HeaderText="Отчество"></asp:BoundColumn>
                    <asp:BoundColumn DataField="PhoneNumber" HeaderText="Телефон"></asp:BoundColumn>
                    <asp:BoundColumn DataField="RemainingAbonementsCount" HeaderText="Количество абонементов"></asp:BoundColumn>
                    <asp:ButtonColumn Text="Edit" CommandName="Edit"></asp:ButtonColumn>
                    <asp:ButtonColumn Text="Delete" CommandName="Delete"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
            
            <asp:Repeater runat="server" ID="rptClientAbonements">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>Дата начала</th>
                            <th>Дата конца</th>
                            <th>Фамилия преподавателя</th>
                            <th>Кол-во оставашихся уроков</th>
                            <th>Дата следующего урока</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server">
                                <%# Eval("DateStartStr")%>
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server">
                                <%# Eval("DateEndStr")%>
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server">
                                <%# Eval("TeacherSurname")%>
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server">
                                <%# Eval("LessonsCount")%>
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server">
                                <%# Eval("DateNextStr")%>
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="buttonSetNextDate" runat="server"  CommandName="Click" Text="Записать дату" CommandArgument='<%# Eval("Id") %>' />
                        </td>
                        <!-- <td>
                            <asp:Button ID="buttonEditAbonement" runat="server"  CommandName="Click" Text="Редактировать абонемент" runat="server" CommandArgument='<%# Eval("Id") %>' />
                        </td>-->
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Calendar ID="calendarNextDate" runat="server"  visible="false" OnSelectionChanged="CalendarNextDate_SelectionChanged"></asp:Calendar>
        </div>
    </form>
</body>
</html>
