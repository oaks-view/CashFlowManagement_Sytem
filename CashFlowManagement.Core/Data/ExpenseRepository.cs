using CashFlowManagement.Core.Exceptions;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data
{
    public class ExpenseRepository:IExpenseRepository,IDisposable
    {
        private CashFlowEntities _db;
        private readonly bool _externalContext;

        public ExpenseRepository()
        {
            _db = new CashFlowEntities();
        }
        public ExpenseRepository(CashFlowEntities context)
        {
            _db = context;
            _externalContext = true;
        }

        public void Create(Expense expense)
        {
            _db.Expenses.Add(expense);
            _db.SaveChanges();
        }

        public Expense GetExpense(int expenseId)
        {
            Expense expense = _db.Expenses.Find(expenseId);
            if (expense == null)
            {
                throw new NoMatchFound("No record for Expense with this Id was found.");
            }
            return expense;
        }

        public List<Expense> GetAllExpenses()
        {
            return _db.Expenses.ToList();
        }

        public void Update(string description, int cost, int expenseId)
        {
            Expense expense = GetExpense(expenseId);
            expense.Description = description;
            expense.Cost = cost;
            _db.SaveChanges();
        }

        public void Delete(int expenseId)
        {
            var expense = GetExpense(expenseId);
            _db.Expenses.Remove(expense);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db == null || _externalContext)
            {
                return;
            }
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
