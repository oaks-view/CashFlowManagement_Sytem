import ReactDOM from "react-dom";
import {render} from "react-dom";
import React from "react";
import LoginPage from "./Login";
import RegistrationPage from "./Registration";
import ManagerPage from "./ManagerSession";

function renderPage(component){
    render(component, document.getElementById("page-container"));
}

 class PageManager extends React.Component{//THIS IS D MAIN COMPONENT
    constructor(){
        super();
        this.state = {loginStatus:"NotLoggedIn"};
        this.userId;
        this.staffCategory;
    }

    handleLogin(){
        this.setState({loginStatus:"LoggedIn"});
        this.state.loginStatus = "LoggedIn";
        //this.forceUpdate();
    }

    render(){
        if (this.state.loginStatus == "NotLoggedIn"){
            return <LoginPage onClick = {()=> this.handleLogin()}/>
        }
        else if(this.state.loginStatus == "LoggedIn"){
            return <ManagerPage/>
        }
    }
}

render(<PageManager/>, document.getElementById("page-container"));