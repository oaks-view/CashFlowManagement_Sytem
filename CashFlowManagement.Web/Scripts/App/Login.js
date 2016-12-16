$(document).ready(function () {
    $("#linkClose").click(function () {
        $("#divError").hide('fade');
    });
    $("#topRegisterBtn").on("click",function () {
        $("#mainWindow").load("/templates/Registration.html");
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
                $("#mainWindow").load("/templates/managerHome.html");
                
                registerStaff();
            },
            error: function (jqXHR) {
                $("#divErrorText").text(jqXHR.responseText);
                $("#divError").show("fade");
            }
        });

        function registerStaff() {
            //finds out if staff record exists for this user 
            //and create one if not.
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
    });