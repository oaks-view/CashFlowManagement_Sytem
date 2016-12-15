using CashFlowManagement.Core.Data;
using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Services;
using CashFlowManagement.Web.Models;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace CashFlowManagement.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            
            container.RegisterType<IStaffRepository, StaffRepository>();
            container.RegisterType<IStaffService, StaffService>();
            container.RegisterType<IExpenseRepository, ExpenseRepository>();
            container.RegisterType<IExpenseService, ExpenseService>();
            container.RegisterType<IIncomeRepository, IncomeRepository>();
            container.RegisterType<IIncomeService, IncomeService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}