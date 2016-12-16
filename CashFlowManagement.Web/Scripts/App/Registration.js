$(document).ready(function () {
    $("#linkClose").click(function () {
        $("#divError").hide('fade')
    });
    $("#registerPage").load("templates/registration.html");

    $("#topLoginBtn").click(function () {
        $("#registerPage").addClass("hidden");
        $("#loginForm").removeClass("hidden");
        //$("#registerPage").addClass("hidden");
    });

    $("#btnRegister").click(function () {
        alert($("#staff1").value);
        alert($("input[name=optradio]:checked").value)
        //$("#successModal").modal('show');
        $.ajax({
            url: "api/account/register",
            method: "POST",
            data: {
                FirstName: $("#txtFirstName").val(),
                LastName: $("#txtLastName").val(),
                Email: $("#txtEmail").val(),
                Password: $("#txtPassword").val(),
                ConfirmPassword: $("#txtConfirmPassword").val(),
            },
            success: function () {
                $("#successModal").modal('show');
            },
            error: function (jqXHR) {
                $("#divErrorText").text(jqXHR.responseText);
                $("#divError").show("fade");
            }
        });
    });
});