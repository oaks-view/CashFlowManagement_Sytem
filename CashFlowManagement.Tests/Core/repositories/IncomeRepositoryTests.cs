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
using static CashFlowManagement.Tests.Core.TestData;

namespace CashFlowManagement.Tests.Core.repositories
{
    [TestClass]
    public class IncomeRepositoryTests
    {
        private CashFlowEntities _context;
        private DbContextTransaction _transaction;

        [ClassInitialize]
        public static void BeforeAllTests(TestContext testContext)
        {
            var ctx = new CashFlowEntities();
            ctx.Database
                .ExecuteSqlCommand(@"insert into AspNetUsers(Id,FirstName, LastName,Email, Username) 
                Values({0},'Clark','Kent','clark@superman.com','clark@superman.com')", sampleEmployee.Id);
            ctx.Database
                .ExecuteSqlCommand(@"insert into AspNetUsers(Id,FirstName, LastName,Email, Username) 
                Values({0},'Bruce','Wayne','bruce@batman.com','bruce@batman.com')", sampleManager.Id);
        }

        [ClassCleanup]
        public static void AfterAllTests()
        {
            var ctx = new CashFlowEntities();
            ctx.Database.ExecuteSqlCommand(@"delete from AspNetUsers
            where Id = {0}", sampleEmployee.Id);

            ctx.Database.ExecuteSqlCommand(@"delete from AspNetUsers
            where Id = {0}", sampleManager.Id);
        }

        [TestInitialize]
        public void BeforeEach()
        {
            _context = new CashFlowEntities();
            _transaction = _context.Database.BeginTransaction();
        }

        [TestCleanup]
        public void AfterEach()
        {
            var incomes = _context.Incomes.ToList();

            _context.Incomes.RemoveRange(incomes);
            _context.SaveChanges();

            _transaction.Rollback();
            _transaction.Dispose();
            _context.Dispose();
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Successfully_Create_New_Income_In_DataBase()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new IncomeRepository(context))
            using (var staffRepo = new StaffRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                context.Staffs.Add(sampleManager);
                context.SaveChanges();
                Income income = new Income("Web_Advertisment", 3000, sampleManager.Id);
                repo.Create(income);
                var dbIncome = sampleManager.Incomes.SingleOrDefault();
                Assert.AreEqual(3000, dbIncome.Amount);
                transaction.Rollback();
            }
        }


        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Saved_Income_From_Database()
        {
            using (var repo = new IncomeRepository(_context))
            {
                _context.Staffs.Add(sampleManager);
                _context.SaveChanges();
                Staff manager = _context.Staffs.Single();
                Income income = new Income("Web_Adverts", 2300, manager.Id);
                repo.Create(income);
                Income retrievedIncome = repo.GetIncome(manager.Incomes.Single().Id);
                Assert.AreEqual(2300, retrievedIncome.Amount);
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_DateCreated_From_Saved_Income()
        {
            using (var repo = new IncomeRepository(_context))
            {
                _context.Staffs.Add(sampleManager);
                _context.SaveChanges();
                Staff manager = _context.Staffs.Single();
                Income income = new Income("IMF_Grant", 45000, manager.Id);
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd-HH");
                repo.Create(income);
                Income retrievedIncome = manager.Incomes.Single();
                Assert.AreEqual(currentDate, retrievedIncome.DateCreated.ToString("yyyy-MM-dd-HH"));
            }
        }


        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_All_Saved_Income_From_Database()
        {
            using (var repo = new IncomeRepository(_context))
            {
                _context.Staffs.Add(sampleManager);
                _context.SaveChanges();
                var manager = _context.Staffs.Single();
                repo.Create(new Income("income1", 2300, manager.Id));
                //repo.Create(new Income("income2", 4300, manager.Id));
                Assert.AreEqual(2, repo.GetAllIncome().Count());
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Update_Saved_Income()
        {
            using (var repo = new IncomeRepository(_context))
            {
                _context.Staffs.Add(sampleManager);
                _context.SaveChanges();
                var manager = _context.Staffs.Single();
                repo.Create(new Income("income1", 2300, manager.Id));
                Income savedIncome = manager.Incomes.Single();
                savedIncome.Description = ".Net_Conference_Revenues";
                savedIncome.Amount = 3400;
                repo.Update(savedIncome);
                Assert.AreEqual(".Net_Conference_Revenues", manager.Incomes.Single().Description);
            }
        }
    }
}
