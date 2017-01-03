import React from "react";
import ReactDOM from "react-dom";
import {Router, Route, Link, IndexRoute, hashHistory, browserHistory} from "react-router";
import {saveNewExpense} from "./apiCalls";


const formBackground = {
    display: 'inline-block',
    padding: '32px 48px 0px 48px',
    border: '1px solid #EEE'
};

class ExpensePage extends React.Component {
    constructor(){
        super();
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    componentDidMount(){
        //
    }

    handleCancel(e){
        e.preventDefault();
        hashHistory.push("");
    }

    handleSave(e){
        e.preventDefault();
        const expenseDTO = {
            Description: this.description.value,
            Cost : this.expenseValue.value,
            StaffId: sessionStorage.getItem("userid")
        }
        saveNewExpense(expenseDTO, ()=>{
            //here is our toast;
            hashHistory.push("")
        });
    }

    render(){
        return (
            <div>
                <div>
                    <main>
                        <center>
                            <div className="section"></div>
                            <h5 className="indigo-text">Please, Enter New Expense</h5>
                            {/*<div className="section"></div>*/}
                            <div className="container">
                                <div className="z-depth-1 grey lighten-4 row" style={formBackground}>
                                    <form className="s12" method="POST">

                                        <div className="row">
                                            <div className='input-field col s12'>
                                                <div className="col s12">
                                                    <input ref={(description)=>{this.description = description}}  className='Placeholder' type='text' name='description' id='expense-decription' />
                                                    <label htmlFor='expense-decription'>Expense description</label>
                                                </div>
                                            </div>
                                        </div>


                                        <div className="row">
                                            <div className='input-field col s12'>
                                                <div className="col s12">
                                                    <input ref={(expenseValue)=>{this.expenseValue = expenseValue}} className='Placeholder' type='number' name='description' id='expense-amount' />
                                                    <label htmlFor='expense-amount'>Amount in value</label>
                                                </div>
                                            </div>
                                        </div>

                                        <div className="row">
                                            <div className="col s6">
                                                <button onClick={this.handleSave} className="btn waves-effect waves-light green left"  name="action">Save</button>
                                            </div>
                                            <div className="col s6">
                                                <button onClick={this.handleCancel} className="btn waves-effect waves-light red right" name="action">Cancel</button>
                                            </div>
                                        </div>

                                    </form>
                                </div>
                            </div>
                        </center>
                    </main>
                </div>
            </div>
        )
    }
}

function addCommas(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

export {ExpensePage,addCommas};