$(document).ready(function () {
    var staffCategory = "Employee";
    $("#staff1").on("click", function () { staffCategory = "Employee";});
    $("#staff2").on("click", function () { staffCategory = "Manager"; });

    function loadLogInPage() {
        $("#mainWindow").load("/templates/Login.html");
        $.getScript("/Scripts/App/Login.js");
    };

    function clearRegistrationFields() {
        $("#txtFirstName").val("");
        $("#txtLastName").val("");
        $("#txtEmail").val("");
        $("#txtPassword").val("");
        $("#txtConfirmPassword").val("");
    };

    $("#topLoginBtn").on("click", function () {
        loadLogInPage();
    });

    $("#linkClose").on("click",function () {
        $("#divError").hide('fade')
    });

    $("#btnRegister").on("click", function () {
        $.ajax({
            url: "api/account/register",
            method: "POST",
            data: {
                FirstName: $("#txtFirstName").val(),
                LastName: $("#txtLastName").val(),
                Email: $("#txtEmail").val(),
                Password: $("#txtPassword").val(),
                ConfirmPassword: $("#txtConfirmPassword").val(),
                StaffCategory: staffCategory
            },
            success: function () {
                clearRegistrationFields();
                $("#successModal").modal('show');
            },
            error: function (jqXHR) {
                $("#divErrorText").text(jqXHR.responseText);
                $("#divError").show("fade");
            }
        });
    });
});