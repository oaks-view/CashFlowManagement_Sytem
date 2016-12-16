$(document).ready(function () {
    $("#topLoginBtn").on("click",function () {
        $("#mainWindow").load("/templates/Login.html");
    });
    $("#linkClose").on("click",function () {
        $("#divError").hide('fade')
    });

    $("#btnRegister").on("click",function () {
        alert($("REGISTERING OUR USER"));
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