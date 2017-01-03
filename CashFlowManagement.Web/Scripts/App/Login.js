import ReactDOM from "react-dom";
import React from "react";
import { render } from "react-dom";
import $ from "jquery";
import {validateUser,registerStaff,getStaffCategory} from "./apiCalls";


const formBackground = {
    display: 'inline-block',
    padding: '32px 48px 0px 48px',
    border: '1px solid #EEE'
};

class LoginPage extends React.Component {
    constructor(){
        super();
        this.onValidate = this.onValidate.bind(this);
    }
    onValidate(e){
        e.preventDefault();
        //const onClick = this.props.onclick
        const {onClick} =  this.props;
        validateUser(this.email.value, this.password.value, (response, error)=>{
            if(response){
                sessionStorage.setItem("accessToken", response.access_token);
                sessionStorage.setItem('expires', response['.expires']);
                sessionStorage.setItem('username', response.userName);
                sessionStorage.setItem('userid', response.userId);
                registerStaff(()=>getStaffCategory(onClick,response));
            }else{
                console.log(error);
            }
        })
    }
    render() {
        const props = this.props;
        return (
            <div>
                <main>
                    <center>
                        <div className="section"></div>

                        <h5 className="indigo-text">Please, login into your account</h5>
                        <div className="section"></div>

                        <div className="container">
                            <div className="z-depth-1 grey lighten-4 row" style={formBackground}>

                                <form className="col s12" method="post">
                                    <div className='row'>
                                        <div className='col s12'>
                                        </div>
                                    </div>

                                    <div className='row'>
                                        <div className='input-field col s12'>
                                            <input className='validate' ref={(email)=>{this.email = email}} type='email' name='email' id='email' />
                                            <label htmlFor='email'>Enter your email</label>
                                        </div>
                                    </div>

                                    <div className='row'>
                                        <div className='input-field col s12'>
                                            <input className='validate' ref={(password)=>{this.password=password}} type='password' name='password' id='password' />
                                            <label htmlFor='password'>Enter your password</label>
                                        </div>
                                        <label style={{ float: 'right' }}>
                                            <a className='pink-text' href='#!'><b>Forgot Password?</b></a>
                                        </label>
                                    </div>

                                    <br />
                                    <center>
                                        <div className='row'>
                                            <button onClick={this.onValidate} type='submit' name='btn_login' className='col s12 btn btn-large waves-effect indigo'>Login</button>
                                        </div>
                                    </center>
                                </form>
                            </div>
                        </div>
                        <a href="#registration" style={{fontWeight:400, fontSize:20}}>Create account!</a>
                    </center>

                    <div className="section"></div>
                    <div className="section"></div>
                </main>
            </div>
        )
    }
}

export default LoginPage;
export {formBackground};