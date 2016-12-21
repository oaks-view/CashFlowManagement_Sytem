﻿using CashFlowManagement.Core.Data;
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

        public void DeleteExpense(int expenseId)
        {
            _expenseRepository.Delete(expenseId);
        }
    }
}
