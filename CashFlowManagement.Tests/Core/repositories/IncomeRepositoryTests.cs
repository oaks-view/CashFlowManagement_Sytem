using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Tests;
using CashFlowManagement.Core.Data;
using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Models;
using System.Data.Entity;
using System.Linq;
using CashFlowManagement.Core.Exceptions;

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

        [TestInitialize]
        public void BeforeEachTest()
        {
            _context = new CashFlowEntities();
            _repo = new IncomeRepository(_context);
            _staffRepo = new StaffRepository(_context);
            _transaction = _context.Database.BeginTransaction();
            _staffRepo.Create(TestData.sampleManager);
            _managerDependency = _staffRepo.GetStaff(TestData.sampleManager.Username);
        }

        [TestCleanup]
        public void AfterEachTest()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _context.Dispose();
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Successfully_Create_New_Income_In_DataBase()
        {
            IncomeEntity incomeData = new IncomeEntity("Subscription Revenue", 3200, _managerDependency.Id);
            _repo.Create(incomeData);
            Income savedIncome = _context.Incomes
                .Where(x => x.Description == "Subscription Revenue").Single();
            Assert.IsNotNull(savedIncome);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Get_Staff_From_Saved_Income()
        {
            IncomeEntity incomeData = new IncomeEntity("Subscription Revenue", 3200, _managerDependency.Id);
            _repo.Create(incomeData);
            Income savedIncome = _context.Incomes
                .Where(x => x.Description == "Subscription Revenue").Single();
            var incomeCreator = savedIncome.Staff;
            Assert.AreEqual(_managerDependency.FirstName, incomeCreator.FirstName);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Saved_Income_From_Database()
        {
            IncomeEntity incomeData = new IncomeEntity("Subscription Revenue", 3200, _managerDependency.Id);
            _repo.Create(incomeData);
            int incomeId = _context.Incomes
                .Where(x => x.Description == "Subscription Revenue").Single().Id;
            Income savedIncome = _repo.GetIncome(incomeId);
            Assert.IsNotNull(savedIncome);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        [ExpectedException(typeof(NoMatchFound))]
        public void Throw_Exception_If_Income_Dies_Not_Exist()
        {
            var incomeInstance = _repo.GetIncome(000);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_All_Saved_Income_From_Database()
        {
            IncomeEntity incomeData = new IncomeEntity("Subscription Revenue", 3200, _managerDependency.Id);
            int prevIncomeCount = _context.Incomes.Count();
            _repo.Create(incomeData);
            _repo.Create(incomeData);
            var allIncomes = _repo.GetAllIncome();
            Assert.AreEqual(prevIncomeCount + 2, allIncomes.Count);
        }
    }
}
