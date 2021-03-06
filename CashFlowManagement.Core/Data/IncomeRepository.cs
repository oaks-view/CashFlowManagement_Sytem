﻿using CashFlowManagement.Core.Exceptions;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data.DB
{
    public class IncomeRepository : IIncomeRepository, IDisposable
    {
        CashFlowEntities _db;
        private readonly bool _externalContext;
        public IncomeRepository()
        {
            _db = new CashFlowEntities();
        }

        public IncomeRepository(CashFlowEntities context)
        {
            _db = context;
            _externalContext = true;
        }
        public void Create(Income income)
        {
            _db.Incomes.Add(income);
            _db.SaveChanges();
        }

        public Income GetIncome(int id)
        {
            return _db.Incomes.Find(id);
        }

        public List<Income> GetAllIncome()
        {
            return _db.Incomes.ToList<Income>();
        }

        public void Update(Income incomeData)
        {
            Income income = GetIncome(incomeData.Id);
            income.Description = incomeData.Description;
            income.Amount = incomeData.Amount;
            _db.SaveChanges();
        }

        public void Delete(int incomeId)
        {
            Income income = GetIncome(incomeId);
            _db.Incomes.Remove(income);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db == null || _externalContext)
            {
                return;
            }
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
