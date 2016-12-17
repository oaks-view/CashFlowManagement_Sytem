(function () {
    $("#logoutBtn").on("click", managerLogout);
    $("#addIncomeBtn").on("click", function () { $("#addIncomeModal").modal("show"); return false; });
    $("#saveNewIncomeButton").on("click", saveNewIncome);
    $("#cancel").on("click", function(){
        $("#incomeDescription").val("");
        $("#incomeAmount").val("");
    });

    function managerLogout() {
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("userid");
        sessionStorage.removeItem("username");
        sessionStorage.removeItem("expires");
        loadLoginPage();
        return false;
    }

    function loadLoginPage() {
        $("#mainWindow").load("/templates/Login.html");
        $.getScript("/Scripts/App/Login.js");
    }

    function saveNewIncome() {
        var defer = jQuery.Deferred();
        $.ajax({
            url: "api/Income",
            method: "POST",
            contentType: "application/json",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            data: {
                Income: {
                    Description: $("#incomeDescription").val(),
                    Amount: $("#incomeAmount").val(),
                    StaffId: sessionStorage.getItem("userid")
                },
                success: function () {
                    $("#addIncomeModal").modal("hide");
                    defer.resolve(true)
                },
                error: function (jqXHR) {
                    console.log(jqXHR);
                }
            }
        });
        return defer.promise();
    };
})()