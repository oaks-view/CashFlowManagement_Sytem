﻿

(function () {

    alert("ManagerSESSION LOADED")
    $("#logout-btn").on("click", managerLogout);
    $("#add-income-btn").on("click", function () { $("#addIncomeModal").modal("show") });
    $("#add-expense-btn").on("click", function () { alert("ADD EXPENSES YEEAHHH"); $("#addExpenseModal").modal("show"); return false; });
    $("#get-saved-expenses-btn").on("click", function () { });
    $("#get-saved-incomes-btn").on("click", getAllSavedIncome);
    $("#saveNewIncomeButton").on("click", saveNewIncome);
    $("#saveNewExpenseBtn").on("click", function () { alert("JUST STARTING TO CRAWL"); saveNewExpense(); });
    $("#cancel").on("click", clearModalPopupFields);

    function managerLogout() {
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("userid");
        sessionStorage.removeItem("username");
        sessionStorage.removeItem("expires");
        loadLoginPage();
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

    function loadLoginPage() {
        $("#mainWindow").load("/templates/Login.html");
        $.getScript("/Scripts/App/Login.js");
    }

    function getAllSavedIncome() {
        var defer = jQuery.Deferred();
        $.ajax({
            url: "api/Income",
            method: "GET",
            headers:{
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                var refined = JSON.parse(response);
                console.log(refined);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.response);
            }
        });
    }

    function saveNewIncome() {
        var income = {
            Description: $("#incomeDescription").val(),
            Amount: $("#incomeAmount").val(),
            StaffId: sessionStorage.getItem("userid")
        };
        alert(JSON.stringify(income));
        $.ajax({
            url: "api/Income",
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
            url: "api/Expense",
            contentType: "application/json",
            method: "POST",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            data: JSON.stringify(expense),
            success: function () {
                alert("added expenss successfully");
                console.log("Added expense successfully");
            },
            error: function (jqXHR) {
                console.log(jqXHR.response);
            }
        });
    }

})()