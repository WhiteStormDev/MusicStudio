<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagedAbonements.aspx.cs" Inherits="MusicStudioApplication.PagedAbonements" %>
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Orders view via jQuery</title>
	<script src="Scripts/jquery-3.4.1.js" type="text/javascript"></script>
	<script src="Scripts/jquery.tmpl.min.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="Styles/PageAbonementsStyle.css"/>
</head>
<body>
    <h1>Orders</h1>

    <div id="progress">Loading...</div>
    <div id="results">
        <table>
		    <thead>
			    <tr>
				    <th id="Id" class="clickableTH sortedAsc">
					    Номер абонемента
				    </th>
				    <th id="ClientName" class="clickableTH">
					    Клиент
				    </th>
				    <th id="DateNext" class="clickableTH">
					    Дата следующего занятия
				    </th>
				    <th id="DateEnd" class="clickableTH">
					    Дата окончания
				    </th>
                    <th id="TeacherSurname" class="simpleTH">
					    Преподаватель
				    </th>
                    <th id="Subject" class="simpleTH">
					    Предмет
				    </th>
				   
			    </tr>
		    </thead>
		    <tbody>
		    </tbody>
	    </table>
        <div class="navPanel">
            <a href="#" id="firstPage" class="navBtn"><<</a>
            <a href="#" id="prevPage" class="navBtn"><</a>
            <div id="pageIndicator">Loading...</div>
            <a href="#" id="nextPage" class="navBtn">></a>
            <a href="#" id="lastPage" class="navBtn">>></a>
        </div>
    </div>

<script type="text/javascript">
    var serverURL = '<%=APIServerURL%>';
    var _page = 1;
    var _sortExpr = "ID";
    var _sort = "asc";
    var _pageLen = <%=PageLen%>;
    var _pageCount = 0;

    function loadTemplates() {
        $.get("tmpl/abonementitem.html", function f(data) {
            $.template("abonementItem", data);
        });
    }

    //function deleteOrderParts(id) {
    //    $.ajax({
    //        url: serverURL + "/api/orderparts?orderid=" + id,
    //        type: 'DELETE',
    //        success: function (result) {
    //            console.log("Parts for order " + id + " deleted");
    //        }
    //    });
    //}


    function deleteEntity(id) {
        $.ajax({
            url: serverURL + "/api/abonements?id=" + id,
            type: 'DELETE',
            success: function (result) {
                console.log("deleted " + id);
            },
            error: function (body, status, errorThrown) {
                switch (body.status) {
                    case 500: showError("Server error happened, call to support"); return;
                        break;
                    case 409: if (confirm("This order also contains several parts. Do you really want to delete order and all its parts?")) {
                        deleteOrderParts(id);
                        deleteEntity(id);
                        }
                        break;
                }
            }           
        });
    }

    function loadData(page, pageLen, sortExpr, sort) {
        var url = serverURL + "/api/abonements?";

        _page = page;
        _sortExpr = sortExpr;
        _sort = sort;


        if (page)
            url += "page=" + page + "&";
        if (pageLen)
            url += "pageLen=" + pageLen + "&";
        if (sortExpr)
            url += "sortBy=" + sortExpr + "&";
        if (sort)
            url += "sort=" + sort + "&";

        $("#results>table>tbody").html("");

        $.getJSON(url, function (obj) {
            _pageCount = obj.PageCount;
            $("#pageIndicator").html(_page + " of " + _pageCount);
            $.tmpl("abonementItem", obj.Page).appendTo("#results>table>tbody");

            $(".button").click(function (e) {
                console.log(e);
                var id = $(e.target).attr("entityID");
                if (confirm("Do you really want to delete order#" + id)) {
                    deleteEntity(id);
                }
            });
            $("#progress").hide();
        });
    }
    $(window).on('load', function () {
        loadTemplates();

        loadData(1, _pageLen, _sortExpr, _sort);

        $('#firstPage').click(function () {
            loadData(1, _pageLen, _sortExpr, _sort);
        });

        $('#prevPage').click(function () {
            if (_page == 1)
                alert("It's a first page!");
            else
                loadData(_page - 1, _pageLen, _sortExpr, _sort);
        });

        $('#nextPage').click(function () {
            if (_page == _pageCount + 1)
                alert("No more pages!");
            else
                loadData(_page + 1, _pageLen, _sortExpr, _sort);
        });

        $('#lastPage').click(function () {
            loadData(_pageCount, _pageLen, _sortExpr, _sort);
        });


        $(".clickableTH").click(function (e) {
            var oldSort = $(e.target).attr("sort");
            var newSort = oldSort == "asc" ? "desc" : "asc";
            $(".clickableTH").attr("class", "clickableTH");
            $(e.target).attr("class", "clickableTH " + "sorted" + newSort);
            var sortExpr = e.target.id;
            loadData(1, _pageLen, sortExpr, newSort);
            $(e.target).attr("sort", newSort);
        });
	});
</script>    
</body>
</html>