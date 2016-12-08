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
        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Successfully_Create_Staff_In_Database()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new StaffRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                repo.Create(sampleEmployee);
                Staff employee = context.Staffs.Where(s => s.Username == sampleEmployee.Username).Single();
                Assert.IsNotNull(employee);
                transaction.Rollback();
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        [ExpectedException(typeof(UsernameExistsException))]
        public void Throws_Exception_When_Staff_With_Username_Already_Exists_In_Database()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new StaffRepository(context))
            using(DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                repo.Create(sampleEmployee);
                repo.Create(sampleEmployee);
                transaction.Rollback();
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
                Staff manager = context.Staffs.Where(x => x.Username == TestData.sampleManager.Username).Single();
                Staff managerId = repo.GetStaff(manager.Id);
                Assert.IsNotNull(manager);
                Assert.AreEqual("Bruce", manager.FirstName);
                transaction.Rollback();
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Staff_From_DataBase_Using_Username()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new StaffRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                repo.Create(TestData.sampleManager);
                Staff manager = repo.GetStaff(TestData.sampleManager.Username);
                Assert.AreEqual(sampleManager.Username, manager.Username);
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

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        [ExpectedException(typeof(NoMatchFound))]
        public void Should_Throw_Exception_When_Username_Is_Not_Found()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new StaffRepository(context))
            {
                var fakeUsername = "mightyFakeJane";
                var staff = repo.GetStaff(fakeUsername);
            }
        }
    }
}
