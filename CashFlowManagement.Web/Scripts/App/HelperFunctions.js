function registerStaff() {
    //finds out if staff record exists for this user 
    //and create one if not.
    $.ajax({
        url: "api/Staff",
        method: "Post",
        contentType:"application/json",
        headers: {
            "Authorization" : "Bearer " + sessionStorage.getItem("accessToken")
        },
        success: function (response) {
            console.log(response);
        },
        error: function(jqXHR){
            console.log(jqXHR.responseText);
    }
    })
}