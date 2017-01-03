using CashFlowManagement.Core.Data;
using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CashFlowManagement.Core.Services.EntitySort;

namespace CashFlowManagement.Core.Services
{
    public class ExpenseService: IExpenseService
    {
        private IExpenseRepository _expenseRepository;
        public ExpenseService(IExpenseRepository repo)
        {
            _expenseRepository = repo;
        }
        public void SaveExpense(Expense expense)
        {
            Expense dbExpense = GetExpense(expense.Id);
            if (dbExpense == null)
            {
                _expenseRepository.Create(expense);
            }
            else
                _expenseRepository.Update(expense);
        }

        public Expense GetExpense(int id)
        {
            return _expenseRepository.GetExpense(id);
        }

        public List<Expense> GetAllExpense()
        {
            var allExpenses = _expenseRepository.GetAllExpenses();
            return SortedList(allExpenses);
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

        public void DeleteExpense(int expenseId)
        {
            _expenseRepository.Delete(expenseId);
        }

        public List<Expense> GetStaffExpenses(string staffId)
        {
            List<Expense> allExpenses = GetAllExpense();
            List<Expense> staffExpenses = allExpenses
                .Where(e => e.StaffId == staffId)
                .ToList<Expense>();
            return SortedList(staffExpenses);
        }

        public int StaffTotalExpensesForThisMonth(string staffId)
        {
            var staffExpenses = GetStaffExpenses(staffId);
            var currentMonth = DateTime.Now.Month;
            return staffExpenses
                .Where(x => x.DateCreated.Month == currentMonth)
                .Sum(x => x.Cost);
        }
    }
}
