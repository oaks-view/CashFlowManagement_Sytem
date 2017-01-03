import React from "react";
import ReactDOM from "react-dom";
import LoginPage from "./Login";
import Registration from "./Registration";
import {Router, Route, Link, IndexRoute, hashHistory, browserHistory} from "react-router";

function LoginOrRegister(props){
    return (
        <div>
            <Router history={hashHistory}>
                <Route path="/" component={()=><LoginPage onClick={props.onClick}/>}/>
                <Route path="/registration" component={Registration}/>
            </Router>
        </div>
    )
}

export default LoginOrRegister;