using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Services
{
    public interface IExpenseService
    {
        void SaveExpense(Expense expense);
        Expense GetExpense(int expense);
        List<Expense> GetAllExpense();
        Dictionary<String, int> GetMonthlyExpenses();
        Dictionary<string, int> GetYearlyExpenses();
        void DeleteExpense(int expenseId);
        List<Expense> GetStaffExpenses(string staffId);
    }
}
