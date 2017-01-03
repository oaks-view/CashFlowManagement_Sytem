import React from "react";
import ReactDOM from "react-dom";
import {render} from "react-dom";
import {Router, Route, Link, IndexRoute, hashHistory, browserHistory} from "react-router";
import {newUserRegistration} from "./apiCalls";


class RegistrationPage extends React.Component{
    constructor(){
        super();
        this.state = {radioButton:"Employee"}
        this.handleRadioClick = this.handleRadioClick.bind(this);
        this.register = this.register.bind(this);
    }
    handleRadioClick(changeEvent){
        this.setState({radioButton:changeEvent.target.value});
    }
    register(e){
        e.preventDefault();
        var registrationDetails = {
            FirstName: this.firstName.value,
            LastName: this.lastName.value,
            Email: this.email.value,
            Password: this.password.value,
            ConfirmPassword: this.confirmPassword.value,
            StaffCategory: this.state.radioButton
        }
        newUserRegistration(registrationDetails, ()=>hashHistory.push(""));
    }

    render(){
        return (
            <div className="container">
                <div className="row">
                    <h4 className="header col s8 offset-s2" style={{color:"rgb(238, 110, 115)"}}>New Staff registration</h4>
                </div>
                <div className = "row">
                    <form className="col s8 offset-s2" method="POST">
                        <div className= "row">{/*row 1*/}
                            <div className = "input-field col s6">
                                <input id="first-name" ref={(firstName)=>this.firstName=firstName} type="text" className="validate"/>
                                <label htmlFor="first-name">First Name</label>
                            </div>
                            <div className="input-field col s6">
                                <input ref={(lastName) => this.lastName=lastName} id="last-name" type="text" className="validate"/>
                                <label htmlFor="last-name">Last Name</label>
                            </div>
                        </div>{/*row 1 ends here*/}

                        <div className = "row">{/*EMAIL FIELD*/}
                            <div className="input-field col s12">
                                <input ref={(email)=>this.email=email} id="reg-email" type="email" className="validate"/>
                                <label htmlFor="reg-email">Email</label>
                            </div>
                        </div>{/*row3 ends here*/}

                        <div className="row">{/*password */}
                            <div className="input-field col s12">
                                <input ref={(password)=> this.password = password} id="reg-password" type="password" className="validate"/>
                                <label htmlFor="reg-password">Password</label>
                            </div>
                        </div>{/*password ends here*/}

                        <div className="row">{/*confimPassword*/}
                            <div className="input-field col s12">
                                <input ref={(confirmPassword) => this.confirmPassword = confirmPassword} id="reg-confrim-password" type="password" className="validate"/>
                                <label htmlFor="reg-confirm-password">Confirm Password</label>
                            </div>
                        </div>{/*confirmPassword ends here*/}              

                        <div className="row">{/*row4 radio buttons*/}
                            <div className="col s6">
                                <p>
                                    <input onChange={this.handleRadioClick} value="Employee" name="group1" checked={this.state.radioButton === "Employee"} type="radio" id="employee" />
                                    <label htmlFor="employee">Employee</label>
                                </p>
                            </div>
                            <div className="col s6">
                                <p>
                                    <input onChange={this.handleRadioClick} checked={this.state.radioButton === "Manager"} value="Manager" name="group1" type="radio" id="manager" />
                                    <label htmlFor="manager">Manager</label>
                                </p>
                            </div>
                        </div>{/*row4 ends here*/}

                        <div className="row">{/*row 5 submit*/}
                            <div className="col s6">
                                <button onClick={this.register} className="btn waves-effect waves-light" type="submit" name="action">Submit
                                    <i className="material-icons right">send</i>
                                </button>
                            </div>
                            <div className="col s6">
                                <a onClick={()=>hashHistory.push("")} href="#"><span style={{fontWeight:400, fontSize:20}}>Already have an account?</span></a>
                            </div>
                        </div>{/*row 5 ends here*/}

                    </form>
                </div>
            </div>
        );
    }
}


module.exports = RegistrationPage;