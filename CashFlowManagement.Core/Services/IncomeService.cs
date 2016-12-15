using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Services
{
    public class IncomeService: IIncomeService
    {
        private IIncomeRepository _repository; 
        public IncomeService(IIncomeRepository repo)
        {
            _repository = repo;
        }
        public void CreateIncome(Income income)
        {
            _repository.Create(income);
        }

        public Income GetIncome(int id)
        {
            var income = _repository.GetIncome(id);
            return income;
        }

        public List<Income> GetAllIncome()
        {
            var allIncome = _repository.GetAllIncome();
            return allIncome;
        }

        public Dictionary<string, int > GetMonthlyIncome()
        {
            List<Income> allIncome = GetAllIncome();
            Dictionary<string, int> monthlyIncome = allIncome.GroupBy(i => i.DateCreated.Month)
                .ToDictionary(i => i.Key.ToString(), i=> i.Sum( x => x.Amount));
            return monthlyIncome;
        }

        public  Dictionary<string, int> GetYearlyIncome()
        {
            List<Income> allIncome = GetAllIncome();
            Dictionary<string, int> yearlyIncome = allIncome.GroupBy(i => i.DateCreated.Year)
                .ToDictionary(i => i.Key.ToString(), i => i.Sum( x => x.Amount));
            return yearlyIncome;
        }
    }
}
