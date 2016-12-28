import React from "react";
import ReactDOM from "react-dom";
import {render} from "react-dom";

class HighlightDisplay extends React.Component{
    constructor(){
        super();
        this.state = {date: new Date()};
        this.statement = "Current date:"
        this.monthNames = [
            "January", "February", "March",
            "April", "May", "June", "July",
            "August", "September", "October",
            "November", "December"
            ];
    }
    render(){
        return (
            <div className= "card medium pink-grey darken-1">
                <div className="card-content blue-text">
                    <span className="card-title">{this.props.values.category} for May 2017</span>
                    <p><label>{this.statement} {this.state.date.getDate()} {this.monthNames[this.state.date.getMonth()]} {this.state.date.getYear()}</label></p>
                    <p style={{fontWeight:400, fontSize:"50px"}}>
                        $ {this.props.values.value}
                    </p>
                    {/*new line*/}
                </div>
                <div className="card-action">
                    <a href="#">Add New {this.props.values.category}</a>
                    <a href="#">See Your Saved {this.props.values.category}</a>
                </div>
            </div>
        );
    }
}


class ManagerPage extends React.Component{
    render(){
        return (
            <div>
                {/*NAVIGSTION BAR*/}
                <ul id="income-dropdown" className="dropdown-content">{/*income dropdown*/}
                    <li><a href="#!">Monthly Income</a></li>
                    <li><a href="#!">Yearly Income</a></li>
                    <li><a href="#!">All Incomes</a></li>
                </ul>

                <ul id="expense-dropdown" className="dropdown-content">{/*expenses dropdown*/}
                    <li><a href="#!">Monthly Expenses</a></li>
                    <li><a href="#!">Yearly Expenses</a></li>
                    <li><a href="#!">All Expenses</a></li>
                </ul>


                <nav>
                    <div className="nav-wrapper" style={{marginLeft:"2%"}}>

                        <a href="#" className="brand-logo">Manager Account</a>
                        {/*HERE IS FOR MOBILE MENU VIEWS*/}
                        <a href="#" data-activates="mobile-demo" className="button-collapse"><i className="material-icons">menu</i></a>
                        
                        <ul className="right hide-on-med-and-down">
                            <li><a href="#">All Incomes</a></li>
                            <li><a href="#">All Expenses</a></li>
                            {/*DROPDOWNS*/}
                            <li><a className="dropdown-button" href="#!" data-activates="income-dropdown">Income Options<i className="material-icons right">arrow_drop_down</i></a></li>
                            <li><a className="dropdown-button" href="#!" data-activates="expense-dropdown">Expenses Options<i className="material-icons right">arrow_drop_down</i></a></li>
                        </ul>

                    </div>
                </nav>

                <div className="container">
                    <div className="row">
                        <div className="col s6">
                            <HighlightDisplay values={{category:"Income", value:"40,000"}} />
                        </div>
                        <div className="col s6">
                            <HighlightDisplay values={{category:"Expense", value:"35,000"}}/>
                        </div>
                    </div>
                </div>

    

        </div>);{/*MANAGER-PAGE ENDING*/}
    }
}

module.exports = ManagerPage;