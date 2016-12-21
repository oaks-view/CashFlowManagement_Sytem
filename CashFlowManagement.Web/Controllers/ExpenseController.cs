using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CashFlowManagement.Web.Controllers
{
    [Authorize]
    public class ExpenseController : ApiController
    {
        private IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public List<Expense> Get()
        {
            return _expenseService.GetAllExpense();
        }

        public Expense Get(int id)
        {
            return _expenseService.GetExpense(id);
        }


        public void Post([FromBody]Expense values)
        {
            Expense expense = new Expense(
                values.Description,
                values.Cost,
                values.StaffId);

            _expenseService.SaveExpense(expense);
        }

        // PUT: api/Expense/5
        public void Put([FromBody]Expense values)
        {
            _expenseService.SaveExpense(values);
        }

        [HttpGet]
        [Route("MonthlyExpense")]
        public Dictionary<string,int> GetMonthlyExpenses()
        {
            return _expenseService.GetMonthlyExpenses();
        }

        [HttpGet]
        [Route("YearlyExpense")]
        public Dictionary<string,int> GetYearlyExpenses()
        {
            return _expenseService.GetYearlyExpenses();
        }

        // DELETE: api/Expense/5
        public void Delete(int expenseId)
        {
            _expenseService.DeleteExpense(expenseId);
        }
    }
}
