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
        void CreateExpense(Expense expense);
        Expense GetExpense(int expense);
        List<Expense> GetAllExpense();
        Dictionary<String, int> GetMonthlyExpenses();
        Dictionary<string, int> GetYearlyExpenses();
        void Update(Expense expense);
    }
}
