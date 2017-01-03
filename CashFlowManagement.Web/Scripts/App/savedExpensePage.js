import React from "react";
import ReactDOM from "react-dom";
import {getStaffExpenses} from "./apiCalls";
import {addCommas} from "./sharedPages";
import {hashHistory} from "react-router";

class SavedExpensesPage extends React.Component{
    constructor(){
        super();
        this.state = {
            expenses: []
        };
        
        this.displayExpenses = this.displayExpenses.bind(this);        
    }

    displayExpenses(response){
        const allExpenses = response.map(x =>
            <Expense key={x.id} id={x.id}  description = {x.description} cost = {addCommas(x.cost)} dateCreated = {x.dateCreated}/>
        );
        this.setState({expenses: allExpenses});
    }

    componentDidMount(){
        getStaffExpenses(this.displayExpenses);
    }   

    render(){
        return(
            <div className="container row">
                {this.state.expenses}
            </div>
        )
    }
}

function Expense(props){
    const handleClick = (e)=>{
        e.preventDefault();
        var expense = {
            description: props.description,
            cost: props.cost,
            id: props.id
        }
        sessionStorage.setItem("editExpense", JSON.stringify(expense))
        hashHistory.push("save-expense");
    }
    return (
        <div className="col s6 m4">
            <div className="card gray lighten-5">
                <div className="card-content blue-text">
                    <span className="card-title">{props.description}</span>
                    <p className="text-red text-lighten-4">
                        $ {props.cost}
                    </p>
                    {/*new line*/}
                </div>
                <div className="card-action">
                    <a href="#" onClick={handleClick}>Edit</a>
                    <a href="#">Delete</a>
                </div>
            </div>
        </div>
    );
}


export {SavedExpensesPage};