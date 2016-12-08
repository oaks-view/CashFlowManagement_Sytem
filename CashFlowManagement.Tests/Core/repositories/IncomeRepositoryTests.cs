using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Tests;
using CashFlowManagement.Core.Data;
using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Models;
using System.Data.Entity;
using System.Linq;
using CashFlowManagement.Core.Exceptions;
using CashFlowManagement.Core.Models.DB;

namespace CashFlowManagement.Tests.Core.repositories
{
    [TestClass]
    public class IncomeRepositoryTests
    {
        private IIncomeRepository _repo;
        private CashFlowEntities _context;
        private DbContextTransaction _transaction;
        private IStaffRepository _staffRepo;
        Staff _managerDependency;


        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Successfully_Create_New_Income_In_DataBase()
        {
            Income incomeData = new Income();
            _repo.Create(incomeData);
            Income savedIncome = _context.Incomes
                .Where(x => x.Description == "Subscription Revenue").Single();
            Assert.IsNotNull(savedIncome);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Get_Staff_From_Saved_Income()
        {
            Income incomeData = new Income();
            _repo.Create(incomeData);
            Income savedIncome = _context.Incomes
                .Where(x => x.Description == "Subscription Revenue").Single();
            var incomeCreator = savedIncome.Staff;
            Assert.AreEqual(_managerDependency.FirstName, incomeCreator.FirstName);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Saved_Income_From_Database()
        {
            Income incomeData = new Income();
            _repo.Create(incomeData);
            int incomeId = _context.Incomes
                .Where(x => x.Description == "Subscription Revenue").Single().Id;
            Income savedIncome = _repo.GetIncome(incomeId);
            Assert.IsNotNull(savedIncome);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_DateCreated_From_Saved_Income()
        {
            Income incomeData = new Income();
            _repo.Create(incomeData);
            Income savedIncome = _context.Incomes
                .Where(x => x.Description == "Subscription Revenue").Single();
            Assert.AreEqual(DateTime.Now.Month, savedIncome.DateCreated.Month);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        [ExpectedException(typeof(NoMatchFound))]
        public void Throw_Exception_If_Income_Does_Not_Exist()
        {
            var incomeInstance = _repo.GetIncome(000);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_All_Saved_Income_From_Database()
        {
            Income incomeData = new Income();
            int prevIncomeCount = _context.Incomes.Count();
            _repo.Create(incomeData);
            _repo.Create(incomeData);
            var allIncomes = _repo.GetAllIncome();
            Assert.AreEqual(prevIncomeCount + 2, allIncomes.Count);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Update_Saved_Income()
        {
            Income incomeData = new Income();
            _repo.Create(incomeData);
            
            incomeData.Id = _context.Incomes
                .Where(x => x.Description == "Subscription Revenue").Single().Id;
            _repo.Update(incomeData);
            Income updatedIncome = _context.Incomes
                .Where(x => x.Description == "Website Advertisement").Single();
            Assert.AreEqual(24000, updatedIncome.Amount);
        }
    }
}
