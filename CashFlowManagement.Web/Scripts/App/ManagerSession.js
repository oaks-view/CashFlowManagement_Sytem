

(function () {
    alert("ManagerSESSION LOADED")
    $("#logout-btn").on("click", managerLogout);
    $("#add-income-btn").on("click", function () { $("#addIncomeModal").modal("show") });
    $("#add-expense-btn").on("click", function () { alert("ADD EXPENSES YEEAHHH"); $("#addExpenseModal").modal("show"); return false; });
    $("#get-saved-expenses-btn").on("click", function () { getAllSavedExpenses();});
    $("#get-saved-incomes-btn").on("click", getAllSavedIncome);
    $("#saveNewIncomeButton").on("click", saveNewIncome);
    $("#saveNewExpenseBtn").on("click", function () { saveNewExpense(); });
    $("#cancel").on("click", clearModalPopupFields);
    $("#manager-monthly-income").on("click", function (e) { e.preventDefault(); getSavedIncomesByMonth(); })
    $("#manager-yearly-income").on("click", function (e) { e.preventDefault(); getSavedIncomeByYear(); })
    $("#manager-saved-incomes").on("click", function (e) { e.preventDefault(); getManagerSavedIncomes(); })
    $("#manager-monthly-expenses").on("click", function (e) { e.preventDefault(); getMonthlyExpenses(); });
    $("#manager-yearly-expenses").on("click", function (e) { e.preventDefault(); getYearlyExpenses();})

    function managerLogout() {
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("userid");
        sessionStorage.removeItem("username");
        sessionStorage.removeItem("expires");
        displayLogin(true);
        return false;
    }

    function clearModalPopupFields() {
        $("#incomeDescription").val("");
        $("#incomeAmount").val("");
    }

    function clearExpenseModalPopupFields() {
        $("#expenseDescription").val("");
        $("#expenseAmount").val("");
    }

    function getAllSavedExpenses() {
        defer = jQuery.Deferred();
        $.ajax({
            url: "api/Expense/Get",
            method: "GET",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
        return defer.promise();
    }

    function getYearlyExpenses() {
        defer = jQuery.Deferred();
        $.ajax({
            url: "api/Expense/GetYearlyExpenses",
            method: "GET",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
        return defer.promise();
    }

    function getMonthlyExpenses() {
        defer = jQuery.Deferred();
        $.ajax({
            url: "api/Expense/GetMonthlyExpenses",
            method: "GET",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.respnseText);
            }
        });
        return defer.promise();
    }

    function loadLoginPage() {
        function displayLogin(status) {
            if (status) {
                $('#login-layout').addClass("hidden").removeClass("hidden")
                $('#manager-layout').removeClass("hidden").addClass("hidden")
            } else {
                $('#login-layout').removeClass("hidden").addClass("hidden")
                $('#manager-layout').addClass("hidden").removeClass("hidden")
            }
        }
    }

    function getSavedIncomeByYear() {
        var defer = jQuery.Deferred();
        $.ajax({
            url: "api/Income/GetYearlyIncome",
            method: "GET",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                defer.resolve(true);
            },
            fail: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
        return defer.promise();
    }


    function getSavedIncomesByMonth() {
        var defer = jQuery.Deferred();
        $.ajax({
            url: "api/Income/GetMonthlyIncome",
            method: "GET",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                defer.resolve(true);
            },
            fail: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
        return defer.promise();
    }

    function getManagerSavedIncomes() {
        var defer = jQuery.Deferred();
        $.ajax({
            url: "api/Staff/GetSavedIncome" + "/userId=" + sessionStorage.getItem("userid"),
            method: "GET",
            //data: JSON.stringify(sessionStorage.getItem("userid")),
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
        return defer.promise();
    }

    function getAllSavedIncome() {
        var defer = jQuery.Deferred();
        $.ajax({
            url: "api/Income/Get",
            //url: "api/Income/GetMonthlyIncome",
            method: "GET",
            headers:{
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                var refined = response;
                console.log(refined);
                displayAllIncome(response);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.response);
                defer.resolve(true);
            }
        });
        return defer.promise();
    }

    function saveNewIncome() {
        var income = {
            Description: $("#incomeDescription").val(),
            Amount: $("#incomeAmount").val(),
            StaffId: sessionStorage.getItem("userid")
        };
        alert(JSON.stringify(income));
        $.ajax({
            url: "api/Income/Post",
            method: "POST",
            contentType: "application/json",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            data: JSON.stringify(income),
            success: function () {
                $("#addIncomeModal").modal("hide");
                clearModalPopupFields();
            },
            error: function (jqXHR) {
                console.log(jqXHR);
            }
        }
        );
    };

    function saveNewExpense() {
        var expense = {
            Description: $("#expenseDescription").val(),
            Cost: $("#expenseAmount").val(),
            StaffId: sessionStorage.getItem("userid")
        };
        alert(JSON.stringify(expense));
        $.ajax({
            url: "api/Expense/Post",
            contentType: "application/json",
            method: "POST",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            data: JSON.stringify(expense),
            success: function () {
                $("#addExpenseModal").modal("hide");
                clearExpenseModalPopupFields();
            },
            error: function (jqXHR) {
                console.log(jqXHR.response);
            }
        });
    }



   /* <div class="container" id="table-container">
        
        <div class="list-group"><!--expenses element-->>
            <a href="#" class="list-group-item active">
                <h4 class="list-group-item-heading">Monthly Subscription</h4>
                <p class="list-group-item-text">
                    <label>$30000</label>
                </p>
            </a>
        </div>
    </div>*/

    function displayAllIncome(dto) {
        var tableContainer = document.getElementById("table-container");
        $.each(dto, function (index, values) {
            var node = document.createElement("div");
            node.id = values.id;
            node.date = values.date;
            node.onclick = function (e) { alert("PLEASE WORKKKKKKKK "+ node.id) };
            node.innerHTML = createElement(values.description, values.amount);
            alert(node);
            tableContainer.appendChild(node);
        })
    }

    function createElement(description, amount) {
        var div = "<div class='list-group'> "
            + "<a href='javascript:void(0)' class='list-group-item active'}>"
            + "<h4 class='list-group-item-heading'>" + description + "</h4>"
            + "<p class='list-group-item-text'>"
            + "<label>" + "$" + amount + "</label>" + "</p>" + "</a>"
            + "</div>";
        return div;
    }

    $('#menu1').children().first().on('click', function (e) {
        e.preventDefault();
        console.log("Holla");
    })
    $('#menu1 ul li:nth-child(2)')
})()