$(document).ready(function () {
    $("#linkClose").on("click",function () {
        $("#divError").hide('fade');
    });

    $("#topRegisterBtn").on("click",function () {
        $("#mainWindow").load("/templates/Registration.html");
        $.getScript("/Scripts/App/Registration.js");
        })
    });

    $("#btnLogin").on("click",function () {
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
                GetStaffDetails();
                loadManagerPage();                

            },
            error: function (jqXHR) {
                $("#divErrorText").text(jqXHR.responseText);
                $("#divError").show("fade");
            }
        });

        function loadManagerPage() {
            $("#mainWindow").load("/templates/managerHome.html");
            $.getScript("/Scripts/bootstrap.min.js");
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
            var userid = sessionStorage.getItem("userid");
            $.ajax({
                url: "api/Staff",
                method: "Get",
                data: { userId: userid },
                contentType: "application/json",
                headers: {
                    "Authorization": "Bearer" + sessionStorage.getItem("accessToken")
                },
                success: function(response){
                    sessionStorage.setItem("currentStaff", response);
                },
                error: function (jqXHR) {
                    console.log(jqXHR.reponse);
                }
            })
        }
    });