(function () {
    $("#logoutBtn").on("click", managerLogout);

    function managerLogout() {
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("userid");
        sessionStorage.removeItem("username");
        sessionStorage.removeItem("expires");
        loadLoginPage();
    }

    function loadLoginPage() {
        $("#mainWindow").load("/templates/Login.html");
        $.getScript("/Scripts/App/Login.js");
    }
})()