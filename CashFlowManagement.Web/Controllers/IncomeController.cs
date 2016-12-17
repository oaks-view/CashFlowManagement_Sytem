using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CashFlowManagement.Web.Controllers
{
    public class IncomeController : ApiController
    {
        private IIncomeService _staffService;
        public IncomeController(IIncomeService service)
        {
            _staffService = service;
        }

        public List<Income> Get()
        {
            return _staffService.GetAllIncome();
        }

        public Income Get(int incomeId)
        {
            return _staffService.GetIncome(incomeId);
        }

        public void Post([FromBody]Income income)
        {
            if (!ModelState.IsValid)
                return;

            _staffService.CreateIncome(income);
        }

        /*
        public async Task Post()
        {
            dynamic obj = await Request.Content.ReadAsAsync<JObject>();
            var incomeDescription = obj.Description;
        }*/
    }
}
