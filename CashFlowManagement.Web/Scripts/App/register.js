$(document).ready(function () {
    $("#linkClose").click(function () {
        $("#divError").hide('fade')
    });
    $("#btnRegister").click(function () {
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