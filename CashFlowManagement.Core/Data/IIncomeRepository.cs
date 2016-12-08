using CashFlowManagement.Core.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data.DB
{
    public interface IIncomeRepository
    {
        void Create(Income incomeData);
        Income GetIncome(int id);
        List<Income> GetAllIncome();
        void Update(Income incomeData);
    }
}
