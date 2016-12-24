using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Services
{
    public class EntitySort
    {
        public static List<Income> SortedList(List<Income> incomeObjects)
        {
            List<Income> incomes = incomeObjects.OrderByDescending(i => i.DateCreated).ToList();
            return incomes;
        }

        public static List<Expense> SortedList(List<Expense> incomeObjects)
        {
            List<Expense> expenses = incomeObjects.OrderByDescending(i => i.DateCreated).ToList();
            return expenses;
        }
    }
}
