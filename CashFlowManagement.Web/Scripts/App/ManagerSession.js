import React from "react";
import ReactDOM from "react-dom";
import { render } from "react-dom";
import { Router, Route, Link, IndexRoute, hashHistory, browserHistory } from 'react-router'


class HighlightDisplay extends React.Component {
    constructor() {
        super();
        this.state = { 
            date: new Date() 
        };
        this.statement = "Current date:"
        this.monthNames = [
            "January", "February", "March",
            "April", "May", "June", "July",
            "August", "September", "October",
            "November", "December"
        ];
    }
    getUrl(){
        const url = this.props.values.category == "Expense"? "#save-expense" : "#new-income";
        return url;
    }
    render() {
        var month = this.monthNames[this.state.date.getMonth()];
        return (
            <div className="card medium pink-grey darken-1">
                <div className="card-content blue-text">
                    <span className="card-title">{this.props.values.category}s for {month}  2017</span>
                    <p><label>{this.statement} {this.state.date.getDate()} {month} {this.state.date.getFullYear()}</label></p>
                    <p style={{ fontWeight: 400, fontSize: "50px" }}>
                        $ {this.props.values.value}
                    </p>
                    {/*new line*/}
                </div>
                <div className="card-action">
                    <a href={this.getUrl()}>Add New {this.props.values.category}</a>
                    <a href="#">This Months Saved {this.props.values.category}</a>
                </div>
            </div>
        );
    }
}


class ManagerPage extends React.Component {
    constructor(){
        super()
        this.state = {
            monthly: 0,
            yearly: 0,
        }
    }
    componentDidMount(){
        this.setState({
            monthly: 5000,
            yearly: 20000
        })
    }
    render() {
        return (
            <div>

                <div className="container">
                    <div className="row">
                        <div className="col s6">
                            <HighlightDisplay values={{ category: "Income", value: this.state.monthly }} />
                        </div>
                        <div className="col s6">
                            <HighlightDisplay values={{ category: "Expense", value: this.state.yearly }} />
                        </div>
                    </div>
                </div>



            </div>); {/*MANAGER-PAGE ENDING*/ }
    }
}
const Address = () => {
    return (<h1>Hello address</h1>)
}
const Hello = () => <h2>Hola</h2>
const ManagerNav = () => {
    return (
        <div>
            {/*NAVIGSTION BAR*/}
                <ul id="income-dropdown" className="dropdown-content">{/*income dropdown*/}
                    <li><a href="#address">Monthly Income</a></li>
                    <li><a href="#income/year">Yearly Income</a></li>
                    <li><a href="#!">All Incomes</a></li>
                </ul>

                <ul id="expense-dropdown" className="dropdown-content">{/*expenses dropdown*/}
                    <li><a href="#!">Monthly Expenses</a></li>
                    <li><a href="#!">Yearly Expenses</a></li>
                    <li><a href="#!">All Expenses</a></li>
                </ul>


                <nav>
                    <div className="nav-wrapper" style={{ marginLeft: "2%", marginRight:"2%" }}>

                        <a href="#" className="brand-logo">Manager Account</a>
                        {/*HERE IS FOR MOBILE MENU VIEWS*/}
                        <a href="#" data-activates="mobile-demo" className="button-collapse"><i className="material-icons">menu</i></a>

                        <ul className="right hide-on-med-and-down">
                            <li><a href="#">All Incomes</a></li>
                            <li><a href="#">All Expenses</a></li>
                            {/*DROPDOWNS*/}
                            <li><a className="dropdown-button" href="#" data-activates="income-dropdown">Income Options<i className="material-icons right">arrow_drop_down</i></a></li>
                            <li><a className="dropdown-button" href="#" data-activates="expense-dropdown">Expenses Options<i className="material-icons right">arrow_drop_down</i></a></li>
                        </ul>

                    </div>
                </nav>
            
            <Router history={hashHistory}>
                <Route path='/' component={ManagerPage} />
                <Route path='/address' component={Address} />
                <Route path='income'>
                    <Route path="year" component={Hello}/>
                </Route>
            </Router>
        </div>

    )
}


export default ManagerNav;
export {HighlightDisplay};