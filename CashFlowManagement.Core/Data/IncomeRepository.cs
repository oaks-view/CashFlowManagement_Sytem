using CashFlowManagement.Core.Exceptions;
using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data.DB
{
    public class IncomeRepository:IIncomeRepository
    {
        CashFlowEntities _db;
        public IncomeRepository()
        {
            _db = new CashFlowEntities();
        }

        public IncomeRepository(CashFlowEntities context)
        {
            _db = context;
        }
        public void Create(IncomeEntity incomeEntity)
        {
            Income newIncome = new Income
            {
                Description = incomeEntity.Description,
                Amount = incomeEntity.Amount,
                StaffId = incomeEntity.StaffId,
                DateCreated = incomeEntity.DateCreated
            };
            _db.Incomes.Add(newIncome);
            _db.SaveChanges();
        }

        public Income GetIncome(int id)
        {
            var income = _db.Incomes.Find(id);
            if (income == null)
            {
                throw new NoMatchFound("No Record for Income with this Id was found.");
            }
            return income;
        }

        public List<Income> GetAllIncome()
        {
            return _db.Incomes.ToList<Income>();
        }
    }
}
