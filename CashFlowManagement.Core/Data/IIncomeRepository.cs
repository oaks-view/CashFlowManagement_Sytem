using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data.DB
{
    public interface IIncomeRepository
    {
        void Create(IncomeEntity income);
        Income GetIncome(int id);
        List<Income> GetAllIncome();
    }
}
