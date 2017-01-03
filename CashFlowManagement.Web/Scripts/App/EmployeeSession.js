import React from "react";
import ReactDOM from "react-dom";
import {Router, Route, Link, IndexRoute, hashHistory, browserHistory} from "react-router";
import {HighlightDisplay} from "./ManagerSession";
import {ExpensePage, formBackground,addCommas} from "./sharedPages";
import {getStaffExpenses,getStaffCurrentMonthTotalExpenses} from "./apiCalls";
import {SavedExpensesPage} from "./savedExpensePage";
const ReactNotify = require('react-notify');

class EmployeeNav extends React.Component {
    constructor(){
        super();
        this.state = {activeTab:"Summary"};
        this.handleLogout = this.handleLogout.bind(this);
        this.changeActiveStatus = this.changeActiveStatus.bind(this);
    }
    changeActiveStatus(e){
        console.log(e.target.id);
        this.setState({activeTab: e.target.id});
    }
    
    handleLogout(){
        this.props.onClick();
    }

    render(){
        return (
            <div>
                <nav>
                    <div className="nav-wrapper" style={{ marginLeft: "2%", marginRight:"2%"}}>
                        <a href="#" className="brand-logo">EmployeesLogin</a>
                        <ul id="nav-mobile" className="right hide-on-med-and-down">
                            <li className={(this.state.activeTab==="Summary")?"active":""}>
                                <a id="Summary"  href="#" onClick={this.changeActiveStatus}>Summary</a>
                            </li>
                            <li className={(this.state.activeTab==="AllExpenses")?"active":""}>
                                <a id="AllExpenses" onClick={this.changeActiveStatus} href="#saved-expenses">All Expenses</a>
                            </li>
                            <li ><a href="#" onClick={this.handleLogout}>logout</a></li>
                        </ul>
                    </div>
                </nav>
                <Router history={hashHistory}>
                    <Route path="/" component={EmployeeHomePage}/>
                    <Route path="/save-expense" component={EmployeeExpensePage}/>
                    <Route path="/saved-expenses" component={SavedExpensesPage}/>
                </Router>
            </div>
        )
    }
}

class EmployeeExpensePage extends React.Component {
    constructor(){
        super();
    }

    render(){
        return (
            <ExpensePage/>
        )
    }
}


class EmployeeHomePage extends React.Component {
    constructor(){
        super();
        this.state = {
            monthTotal:0, 
            yearTotal:0
        };
        this.showNotification = this.showNotification.bind(this);
    }
    componentDidMount(){
        getStaffCurrentMonthTotalExpenses((response)=>{
            let totalExpense = 
            this.setState({monthTotal: addCommas(response)});
        });
    }

    showNotification(e){
        e.preventDefault();
        this.notify.success("Hi Moses","Succesfully added expense","$ 40,000");
    }
    render(){
        return (
            <div className = "container">
                <div className = "row">
                    <div className="col s12">
                        <HighlightDisplay values={{category: "Expense", value: this.state.monthTotal}}/>
                    </div>
                </div>
            </div>
        ) 
    }
}


export default EmployeeNav;