$(function () {
    function loadManagerPage() {
        $("#mainWindow").load("/templates/ManagerPage.html");
        //$.getScript("/Scripts/bootstrap.min.js");
        $.getScript("/Scripts/App/ManagerSession.js");
    };


    function registerStaff() {
        $.ajax({
            url: "api/Staff",
            method: "Post",
            contentType: "application/json",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
            },
            error: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
    }

    function GetStaffDetails() {
        var defer = jQuery.Deferred()
        var userid = sessionStorage.getItem("userid");
        $.ajax({
            url: "api/Staff",
            method: "GET",
            data: { userId: userid },
            headers: {
                "Authorization": "Bearer" + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                sessionStorage.setItem("currentStaff", response);
                defer.resolve(true);
            },
            error: function (jqXHR) {
                console.log(jqXHR.reponse);
                defer.resolve(true);
            }
        })
        return defer.promise();
    }

    $("#linkClose").on("click",function () {
        $("#divError").hide('fade');
    });
    alert("WE ARE INTHE PAGE");

    $("#register-btn-top").on("click", function () {
        alert("A NEW WAY TO CODE");
        $("#mainWindow").load("/templates/Registration.html");
        $.getScript("/Scripts/App/Registration.js");
    });


    $("#login-btn").on("click",function () {
        $.ajax({
            url: "/token",
            method: "POST",
            contentType:"application/json",
            data: {
                Username: $("#txtEmailLogin").val(),
                Password: $("#txtPasswordLogin").val(),
                grant_type: "password"
            },
            success: function (response) {
                sessionStorage.setItem("accessToken", response.access_token);
                sessionStorage.setItem('expires', response['.expires']);
                sessionStorage.setItem('username', response.userName);
                sessionStorage.setItem('userid', response.userId);
                console.log(response);
                registerStaff();
                //GetStaffDetails();
                loadManagerPage();                

            },
            error: function (jqXHR) {
                $("#divErrorText").text(jqXHR.responseText);
                $("#divError").show("fade");
            }
        });
    })
})();