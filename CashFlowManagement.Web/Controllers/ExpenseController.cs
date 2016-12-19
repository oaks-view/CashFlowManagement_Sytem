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

        // GET: api/Expense
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Expense/5
        public string Get(int id)
        {
            return "value";
        }


        // POST: api/Expense
        public void Post([FromBody]Expense values)
        {
            Expense expense = new Expense(
                values.Description,
                values.Cost,
                values.StaffId);

            _expenseService.CreateExpense(expense);
        }

        // PUT: api/Expense/5
        public void Put([FromBody]string value)
        {
        }

        // DELETE: api/Expense/5
        public void Delete(int id)
        {
        }
    }
}
