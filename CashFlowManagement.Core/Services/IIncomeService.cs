using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Services
{
    public interface IIncomeService
    {
        void CreateIncome(Income income);
        Income GetIncome(int id);
        List<Income> GetAllIncome();
        Dictionary<string, int> GetMonthlyIncome();
        Dictionary<string, int> GetYearlyIncome();
        void DeleteIncome(int incomeId);
    }
}
