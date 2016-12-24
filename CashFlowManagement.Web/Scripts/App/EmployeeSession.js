(function () {
    $("#emp-logout-btn").on("click", employeeLogout);
    $("#emp-cancel").on("click", clearExpenseModalFields);
    $("#emp-saveNewExpenseBtn").on("click", function () { saveNewExpense(); });
    $("#emp-get-saved-expenses-btn").on("click", function () { getExployeeExpenses(); });


    function getExployeeExpenses() {
        var defer = jQuery.Deferred();
        $.ajax({
            url: "api/Expense/GetStaffExpenses?staffId=" + sessionStorage.getItem("userid"),
            method: "GET",
            contentType: "application/json",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                clearExpenseData();
                displayAllExpenses(response);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
        return defer.promise();
    }

    function saveNewExpense() {
        var expense = {
            Description: $("#emp-expenseDescription").val(),
            Cost: $("#emp-expenseAmount").val(),
            StaffId: sessionStorage.getItem("userid")
        }

        $.ajax({
            url: "api/Expense/Post",
            contentType: "application/json",
            method: "POST",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            data: JSON.stringify(expense),
            success: function () {
                $("#emp-addExpenseModal").modal("hide");
                clearExpenseModalFields();
                getExployeeExpenses();
            },
            error: function (jqXHR) {
                console.log(jqXHR.response);
                $("#emp-expenseAmount").val("");
            }
        });
    }

    function clearExpenseModalFields() {
        $("#emp-expenseDescription").val("");
        $("#emp-expenseAmount").val("");
    }

    function clearExpenseData() {
        var container = document.getElementById("emp-table-container");
        while (container.hasChildNodes()) {
            container.removeChild(container.lastChild);
        }
    }



    function parseDate(cdate) {
        var date = new Date(Date.parse(cdate));
        return date.toISOString().split("T")[0];
    }

    function displayAllExpenses(dto) {
        var tableContainer = document.getElementById("emp-table-container");
        $.each(dto, function (index, values) {
            var node = document.createElement("div");
            node.id = values.id;
            node.date = values.date;
            node.onclick = function (e) { alert("PLEASE WORKKKKKKKK " + node.id) };
            node.innerHTML = createElement(values.description, values.cost, parseDate(values.dateCreated));
            tableContainer.appendChild(node);
        })
    }

    function createElement(description, amount, date) {
        var div = "<div class='list-group'> "
            + "<a href='javascript:void(0)' class='list-group-item active'}>"
            + "<h4 class='list-group-item-heading'>" + date + "</h4>"
            + "<h4 class='list-group-item-heading'>" + description + "</h4>"
            + "<p class='list-group-item-text'>"
            + "<label>" + "$" + amount + "</label>" + "</p>" + "</a>"
            + "</div>";
        return div;
    }

    function employeeLogout() {
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("userid");
        sessionStorage.removeItem("username");
        sessionStorage.removeItem("expires");
        sessionStorage.removeItem("staffCategory");
        clearExpenseData();
        displayLogin(true);
    }
})()