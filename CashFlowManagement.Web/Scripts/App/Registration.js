import React from "react";
import ReactDOM from "react-dom";
import {render} from "react-dom";


function RegistrationPage(){
    return (
        <div className="container">
            <h2 className="header" style={{color:"rgb(238, 110, 115)"}}>New Staff registration</h2>
            <div className = "row">
                <form className="col s12">
                    <div className= "row">{/*row 1*/}
                        <div className = "input-field col s6">
                            <input id="first-name" type="text" className="validate"/>
                            <label htmlFor="first-name">First Name</label>
                        </div>
                        <div className="input-field col s6">
                            <input id="last-name" type="text" className="validate"/>
                            <label htmlFor="last-name">Last Name</label>
                        </div>
                    </div>{/*row 1 ends here*/}

                    <div className = "row">{/*EMAIL FIELD*/}
                        <div className="input-field col s12">
                            <input id="reg-email" type="email" className="validate"/>
                            <label htmlFor="reg-email">Email</label>
                        </div>
                    </div>{/*row3 ends here*/}

                    <div className="row">{/*password */}
                        <div className="input-field col s12">
                            <input id="reg-password" type="password" className="validate"/>
                            <label htmlFor="reg-password">Password</label>
                        </div>
                    </div>{/*row2 ends here*/}

                    <div className="row">{/*row2*/}
                        <div className="input-field col s12">
                            <input id="reg-confrim-password" type="password" className="validate"/>
                            <label htmlFor="reg-confirm-password">Confirm Password</label>
                        </div>
                    </div>{/*row2 ends here*/}              

                    <div className="row">{/*row4 radio buttons*/}
                        <div className="col s6">
                            <p>
                                <input name="group1" type="radio" id="employee" />
                                <label htmlFor="employee">Employee</label>
                            </p>
                        </div>
                        <div className="col s6">
                            <p>
                                <input name="group1" type="radio" id="manager" />
                                <label htmlFor="manager">Manager</label>
                            </p>
                        </div>
                    </div>{/*row4 ends here*/}

                    <div className="row">{/*row 5 submit*/}
                        <div className="col s6">
                            <button className="btn waves-effect waves-light" type="submit" name="action">Submit
                                <i className="material-icons right">send</i>
                            </button>
                        </div>
                        <div className="col s6">
                            <a href="#"><span style={{fontWeight:400, fontSize:20}}>Already have an account?</span></a>
                        </div>
                    </div>{/*row 5 ends here*/}

                </form>
            </div>
        </div>
    );
}


module.exports = RegistrationPage;