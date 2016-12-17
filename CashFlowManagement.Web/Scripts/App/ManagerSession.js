(function () {
    alert("Manager seesion Activated " + sessionStorage.getItem("userid"));
    $("#logoutBtn").on("click", managerLogout);

    function managerLogout() {
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("userid");
        sessionStorage.removeItem("username");
        sessionStorage.removeItem("expires");
        alert("yeah done with cleanup");
    }

    function loadLoginPage() {
        $("#mainWindow").load("/templates/Login.html");
        $.getScript("/Scripts/App/Login.js");
    }
})()