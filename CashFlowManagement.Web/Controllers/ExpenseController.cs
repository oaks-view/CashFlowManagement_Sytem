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

        public IHttpActionResult Get()
        {
            var expenses = _expenseService.GetAllExpense();
            if (expenses == null)
            {
                return NotFound();
            }
            return Ok(expenses);
        }

        public IHttpActionResult Get(int id)
        {
            var expense = _expenseService.GetExpense(id);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }


        public void Post([FromBody]Expense values)
        {
            Expense expense = new Expense(
                values.Description,
                values.Cost,
                values.StaffId);

            _expenseService.SaveExpense(expense);
        }

        public void Put([FromBody]Expense values)
        {
            _expenseService.SaveExpense(values);
        }

        public IHttpActionResult GetMonthlyExpenses()
        {
            var monthlyExpenses =_expenseService.GetMonthlyExpenses();
            if (monthlyExpenses == null)
            {
                return NotFound();
            }
            return Ok(monthlyExpenses);
        }

        public IHttpActionResult GetYearlyExpenses()
        {
            var yearlyExpenses = _expenseService.GetYearlyExpenses();
            if (yearlyExpenses == null)
            {
                return NotFound();
            }
            return Ok(yearlyExpenses);
        }

        public void Delete(int expenseId)
        {
            _expenseService.DeleteExpense(expenseId);
        }

        public IHttpActionResult GetStaffExpenses(string staffId)
        {
            var staffExpenses = _expenseService.GetStaffExpenses(staffId);
            if (staffExpenses.Count == 0)
            {
                return NotFound();
            }
            return Ok(staffExpenses);
        }
    }
}
