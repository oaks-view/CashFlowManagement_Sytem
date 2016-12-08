using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data
{
    public interface IExpenseRepository
    {
        void Create(Expense expenseData);
        Expense GetExpense(int Id);
        List<Expense> GetAllExpenses();
        void Update(string description, int cost, int expenseId);
    }
}
