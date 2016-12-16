$(document).ready(function () {
    $("#linkClose").click(function () {
        $("#divError").hide('fade');
    });
    $("#topRegisterBtn").click(function () {
        $("#registerPage").removeClass("hidden");
        $("#loginForm").addClass("hidden");
    });
    $("#managerHomePage").load("/templates/managerHome.html");
    $("#registerPage").load("templates/registration.html");

    $("#btnLogin").click(function () {
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
                $("#managerHomePage").removeClass("hidden")
                
                $("#loginForm").addClass("hidden");
                registerStaff();
            },
            error: function (jqXHR) {
                $("#divErrorText").text(jqXHR.responseText);
                $("#divError").show("fade");
            }
        });
    });
});