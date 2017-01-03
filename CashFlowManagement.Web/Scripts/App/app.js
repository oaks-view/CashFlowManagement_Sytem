import ReactDOM from "react-dom";
import {render} from "react-dom";
import React from "react";
//import LoginPage from "./Login";
//import RegistrationPage from "./Registration";
import LoginOrRegister from "./LoginOrRegister";
import ManagerPage from "./ManagerSession";
import EmployeePage from "./EmployeeSession";


 class PageManager extends React.Component{
    constructor(){
        super();
        this.state = {
            userId:window.sessionStorage.getItem('accessToken'),
            staffCategory: sessionStorage.getItem('staffCategory')
        };
        this.handleLogin =  this.handleLogin.bind(this);
        this.handleLogout =  this.handleLogout.bind(this);
    }

    handleLogin(credentials){
        this.setState({userId: credentials.userId});
        this.setState({staffCategory: sessionStorage.getItem("staffCategory")});
    }
    handleLogout(){
        this.setState({userId: null});
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("expires");
        sessionStorage.removeItem("staffCategory");
        sessionStorage.removeItem("userid");
        sessionStorage.removeItem("username");
    }

    staffPage(staffCategory){
        let page = this.state.staffCategory == "3366" ? <ManagerPage logout = {this.handleLogout} /> : 
        <EmployeePage onClick={this.handleLogout} />;
        return page;
    }

    render(){
        return (
            <div>
            {this.state.userId ? 
               this.staffPage(this.staffCategory)
               : <LoginOrRegister onClick={this.handleLogin}/>
            }
            </div>
        )        
    }
}

render(<PageManager/>, document.getElementById("page-container"));