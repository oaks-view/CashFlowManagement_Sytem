

(function () { 
    $("#logout-btn").on("click", managerLogout);
    $("#add-income-btn").on("click", function () { $("#addIncomeModal").modal("show"); $("#toggler").bootstrapToggle("on"); });
    $("#add-expense-btn").on("click", function () { $("#addExpenseModal").modal("show"); $("#toggler").bootstrapToggle("off");return false; });
    $("#get-saved-expenses-btn").on("click", function () { getAllSavedExpenses(); $("#toggler").bootstrapToggle("off"); });
    $("#get-saved-incomes-btn").on("click", function () { getAllSavedIncome(); $("#toggler").bootstrapToggle("on"); })// getAllSavedIncome);
    $("#saveNewIncomeButton").on("click", saveNewIncome);
    $("#saveNewExpenseBtn").on("click", function () { saveNewExpense(); });
    $("#cancel").on("click", clearModalPopupFields);
    $("#manager-monthly-income").on("click", function (e) { e.preventDefault(); getSavedIncomesByMonth(); $("#toggler").bootstrapToggle("on") })
    $("#manager-yearly-income").on("click", function (e) { e.preventDefault(); getSavedIncomeByYear(); $("#toggler").bootstrapToggle("on") })
    $("#manager-saved-incomes").on("click", function (e) { e.preventDefault(); getManagerSavedIncomes(); $("#toggler").bootstrapToggle("on"); })
    $("#manager-saved-expenses").on("click", function (e) { e.preventDefault(); getManagerSavedExpenses(); $("#toggler").bootstrapToggle("off"); })
    $("#manager-monthly-expenses").on("click", function (e) { e.preventDefault(); getMonthlyExpenses(); $("#toggler").bootstrapToggle("off"); });
    $("#manager-yearly-expenses").on("click", function (e) { e.preventDefault(); getYearlyExpenses(); $("#toggler").bootstrapToggle("off"); })

    function managerLogout() {
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("userid");
        sessionStorage.removeItem("username");
        sessionStorage.removeItem("expires");
        sessionStorage.removeItem("staffCategory");
        displayLogin(true);
        return false;
    }

    function clearAll() {
        var container = document.getElementById("table-container");
        while (container.hasChildNodes()) {
            container.removeChild(container.lastChild);
        }
    }

    function clearModalPopupFields() {
        $("#incomeDescription").val("");
        $("#incomeAmount").val("");
    }

    function clearExpenseModalPopupFields() {
        $("#expenseDescription").val("");
        $("#expenseAmount").val("");
    }

    
    function getManagerSavedExpenses() {
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
                clearAll();
                displayAllIncome(response, false);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
        return defer.promise();
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
                clearAll();
                displayAllIncome(response,false);
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
                clearAll();
                displayPeriodically(response);
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
                clearAll();
                displayPeriodically(response);
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

    function displayPeriodically(dto) {
        $.each(dto, function (index, value) {
            var div = document.createElement("div");
            div.innerHTML = createPeriodicElement(index, value);
            div.onclick = function () { alert('PERIODIC INCOMES HERE')};
            var container = document.getElementById("table-container");
            container.appendChild(div);
        });
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
                clearAll();
                displayPeriodically(response);
                $("#toggler").bootstrapToggle("on");
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
                clearAll();
                displayPeriodically(response);
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
            url: "api/Staff/GetSavedIncome?" + "StaffId=" + sessionStorage.getItem("userid"),
            method: "GET",
            //data: JSON.stringify(sessionStorage.getItem("userid")),
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                displayAllIncome(response);
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
                clearAll();
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
                getAllSavedIncome();
                
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
                getAllSavedExpenses();
            },
            error: function (jqXHR) {
                console.log(jqXHR.response);
            }
        });
    }

    function parseDate(cdate) {
        var date = new Date(Date.parse(cdate));
        return date.toISOString().split("T")[0];
    }

    function displayAllIncome(dto,status) {
        var tableContainer = document.getElementById("table-container");
        $.each(dto, function (index, values) {
            var node = document.createElement("div");
            node.id = values.id;
            node.date = values.date;
            node.onclick = function (e) { alert("PLEASE WORKKKKKKKK " + node.id) };
            if (status == false) {
                node.innerHTML = createElement(values.description, values.cost, parseDate(values.dateCreated));
            }
            else
                node.innerHTML = createElement(values.description, values.amount, parseDate(values.dateCreated));
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

    function createPeriodicElement(period, amount) {
        var div = "<div class='list-group'> "
            + "<a href='javascript:void(0)' class='list-group-item active'}>"
            + "<h4 class='list-group-item-heading'>" + period + "</h4>"
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