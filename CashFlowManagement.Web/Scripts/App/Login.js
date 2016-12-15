$(document).ready(function () {
    $("#linkClose").click(function () {
        $("#divError").hide('fade')
    });
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
                $("#loginForm").addClass("hidden");
                $("#staffData").removeClass("hidden");
                registerStaff();
            },
            error: function (jqXHR) {
                $("#divErrorText").text(jqXHR.responseText);
                $("#divError").show("fade");
            }
        });
    });
});