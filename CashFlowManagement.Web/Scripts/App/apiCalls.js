import  jQuery from "jquery";

function newUserRegistration(dto,callback){
    const defer = jQuery.Deferred();
    $.ajax({
        url: "api/Account/Register",
        method:"POST",
        data:dto,
        success:(response)=>{
            console.log("REGISTRATIO SUCCESSFUL "+ response);
            callback();
        },
        error:(response)=>{
            console.log("ERRRRRROOOOORRRRRRRR");
            console.log(response);
        }
    })
}

function editSavedExpense(dto, callback){
    let defer = jQuery.Deferred();
    $.ajax({
        url: "api/Expense/Put",
        method:"PUT",
        data: JSON.stringify(dto),
        contentType:"application/json",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
        },
        sucess:(response)=>{
            console.log(response);
            callback();
            defer.resolve(true);
        }
    })
}

function saveNewExpense(dto,callback){
    var defer = jQuery.Deferred();
    $.ajax({
        url: "api/Expense/Post",
        method: "POST",
        data: JSON.stringify(dto),
        contentType:"application/json",
        headers:{
            "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
        },
        success:(response)=>{
            console.log(response);
            callback();
            defer.resolve(true);
        },
        error:(jqXHR)=>{
            console.log(jqXHR.responseText);
        }
    });
    defer.promise();
}

function getStaffCurrentMonthTotalExpenses(callback){
    var defer = jQuery.Deferred();
    $.ajax({
        url: "api/Expense/GetCurrentMonthTotalExpenses?staffId=" + sessionStorage.getItem("userid"),
        method: "GET",
        headers:{
            "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
        },
        success:(response)=>{
            console.log("This months total: "+ response);
            callback(response);
            defer.resolve(true);
        },
        error:(jqXHR)=>{
            console.log("ERRRRORRR "+ jqXHR.responseText);
        }
    });
    return defer.promise();
}

function getStaffExpenses(callback){
    let defer = jQuery.Deferred();
    $.ajax({
        url:"api/Expense/GetStaffExpenses?staffId="+ sessionStorage.getItem("userid"),
        method:"GET",
        contentType:"application/json",
        headers:{
            "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
        },
        success:(response)=>{
            callback(response);
            defer.resolve(true);
        },
        error:(jqXHR)=>{
            console.log("EERRRRORRR "+jqXHR.responseText);
        }
    });
    return defer.promise();
}

function validateUser(email, password, callback){
    $.ajax({
            url: "/token",
            method: "POST",
            contentType: "application/json",
            data: {
                Username: email,
                Password: password,
                grant_type: "password"
            },
            success: function (response) {
                callback(response);
                // registerStaff();

            },
            error: function (jqXHR) {
                callback(null, jqXHR.responseText);
            }
        });
}

function getStaffCategory(callback,ajaxResponse) {
            let defer = jQuery.Deferred();
            var userid = sessionStorage.getItem("userid");
            $.ajax({
                url: "api/Staff/GetStaffCategory?staffId=" + userid,
                method: "GET",
                headers: {
                    "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
                },
                success: function (response) {
                    sessionStorage.setItem("staffCategory", response);
                    callback(ajaxResponse);
                    defer.resolve(true);
                },
                error: function (jqXHR) {
                    console.log(jqXHR.responseText);
                }
            });
            return defer.promise();
        }


function registerStaff(callback) {
        $.ajax({
            url: "api/Staff/Post",
            method: "Post",
            contentType: "application/json",
            headers: {
                "Authorization": "Bearer " + sessionStorage.getItem("accessToken")
            },
            success: function (response) {
                console.log(response);
                callback();
            },
            error: function (jqXHR) {
                console.log(jqXHR.responseText);
            }
        });
    }

export {validateUser,registerStaff,getStaffCategory,getStaffExpenses,getStaffCurrentMonthTotalExpenses
    ,saveNewExpense, newUserRegistration
    }