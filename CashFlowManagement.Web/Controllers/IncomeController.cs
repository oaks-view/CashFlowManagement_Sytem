﻿using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace CashFlowManagement.Web.Controllers
{
    [Authorize]
    public class IncomeController : ApiController
    {
        private IIncomeService _incomeService;
        public IncomeController(IIncomeService service)
        {
            _incomeService = service;
        }

        public List<Income> Get()
        {
            return _incomeService.GetAllIncome();
        }

        public Income Get(int incomeId)
        {
            return _incomeService.GetIncome(incomeId);
        }

        public void Post([FromBody]dynamic values)
        {
            Income income = new Income(
                values.Description.Value,
                values.Amount.Value,
                values.StaffId.Value
                );

            _incomeService.CreateIncome(income);
        }
    }
}