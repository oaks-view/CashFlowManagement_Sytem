using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Services;
using log4net;
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

        public IHttpActionResult Get()
        {
            var incomes = _incomeService.GetAllIncome();
            if (incomes == null)
            {
                return NotFound();
            }
            return Ok(incomes);
        }

        public IHttpActionResult Get(int incomeId)
        {
            var income = _incomeService.GetIncome(incomeId);
            if (income == null)
            {
                return NotFound();
            }
            return Ok(income);
        }

        public void Post([FromBody]Income values)
        {
            Income income = new Income(
                values.Description,
                values.Amount,
                values.StaffId
                );

            _incomeService.CreateIncome(income);
        }

        public void Put([FromBody]Income values)
        {
            _incomeService.CreateIncome(values);
        }

        public IHttpActionResult GetMonthlyIncome()
        {
            var monthlyIncome = _incomeService.GetMonthlyIncome();
            if (monthlyIncome == null)
            {
                return NotFound();
            }
            return Ok(monthlyIncome);
        }

        public IHttpActionResult GetYearlyIncome()
        {
            var yearlyIncomes =  _incomeService.GetYearlyIncome();
            if (yearlyIncomes == null)
            {
                return NotFound();
            }
            return Ok(yearlyIncomes);
        }

        public void Delete(int incomeId)
        {
            _incomeService.DeleteIncome(incomeId);
        }
    }
}
