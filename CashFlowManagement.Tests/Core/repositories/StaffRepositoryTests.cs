using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Data;
using CashFlowManagement.Core.Exceptions;
using System.Data.Entity;
using EmployeeManagement.Tests;
using System.Linq;
using CashFlowManagement.Core.Models.DB;
using static CashFlowManagement.Tests.Core.TestData;

namespace CashFlowManagement.Tests.Core.repositories
{
    [TestClass]
    public class StaffRepositoryTests
    {
        private CashFlowEntities _context;
        private DbContextTransaction _transaction;

        [TestInitialize]
        public void BeforeEach()
        {
            _context = new CashFlowEntities();
            _transaction = _context.Database.BeginTransaction();
        }

        [TestCleanup]
        public void AfterEach()
        {
            var expenses = _context.Expenses.ToList();

            _context.Expenses.RemoveRange(expenses);
            _context.SaveChanges();
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Successfully_Create_Staff_In_Database()
        {
            using (var repo = new StaffRepository(_context))
            {
                repo.Create(sampleEmployee);
                Staff employee = _context.Staffs.Single();
                Assert.IsNotNull(employee);
                _transaction.Rollback();
            }
        }
        


        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Staff_From_DataBase_Using_Id()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new StaffRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                repo.Create(TestData.sampleManager);
                Staff manager = context.Staffs.Single();
                Staff managerId = repo.GetStaff(manager.Id);
                Assert.IsNotNull(manager);
                Assert.AreEqual("Bruce", manager.Name);
                transaction.Rollback();
            }
        }


        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Should_Be_Able_To_Retrieve_All_Staffs_In_Database()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new StaffRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                var prevCount = context.Staffs.Count();
                repo.Create(sampleEmployee);
                repo.Create(sampleManager);
                Assert.AreEqual(prevCount + 2, context.Staffs.Count());
                transaction.Rollback();
            }
        }

    }
}
