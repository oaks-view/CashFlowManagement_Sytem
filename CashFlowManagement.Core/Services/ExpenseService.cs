using CashFlowManagement.Core.Data;
using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Services
{
    public class ExpenseService: IExpenseService
    {
        private IExpenseRepository _repository;
        public ExpenseService(IExpenseRepository repo)
        {
            _repository = repo;
        }
        public void CreateExpense(Expense expense)
        {
            _repository.Create(expense);
        }

        public Expense GetExpense(int id)
        {
            return _repository.GetExpense(id);
        }

        public List<Expense> GetAllExpense()
        {
            var allExpenses = _repository.GetAllExpenses();
            return allExpenses.ToList();
        }

        public Dictionary<String, int> GetMonthlyExpenses()
        {
            List<Expense> allExpenses = GetAllExpense();
            Dictionary<string, int> monthlyExpenses = allExpenses.GroupBy(e => e.DateCreated.Month)
                .ToDictionary(e => e.Key.ToString(), e => e.Sum(x => x.Cost));
            return monthlyExpenses;
        }

        public Dictionary<string, int> GetYearlyExpenses()
        {
            List<Expense> allExpenses = GetAllExpense();
            Dictionary<string, int> yearlyExpenses = allExpenses.GroupBy(e => e.DateCreated.Year)
                .ToDictionary(e => e.Key.ToString(), e => e.Sum(x => x.Cost));
            return yearlyExpenses;
        }
    }
}
