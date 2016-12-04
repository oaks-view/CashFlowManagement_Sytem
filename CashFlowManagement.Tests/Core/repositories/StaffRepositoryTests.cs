using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Data;
using CashFlowManagement.Core.Exceptions;
using System.Data.Entity;
using EmployeeManagement.Tests;
using System.Linq;

namespace CashFlowManagement.Tests.Core.repositories
{
    [TestClass]
    public class StaffRepositoryTests
    {
        private CashFlowEntities _context;
        private DbContextTransaction _transaction;
        private StaffRepository _repo;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _context = new CashFlowEntities();
            _repo = new StaffRepository(_context);
            _transaction = _context.Database.BeginTransaction();
        }

        [TestCleanup]
        public void AfterEachTest()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _context.Dispose();
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Successfully_Create_Staff_In_Database()
        {
            _repo.Create(TestData.sampleEmployee);
            var dbEmployee = _context.Staffs
                .Where(x => x.Username == TestData.sampleEmployee.Username).Single();
            Assert.IsNotNull(dbEmployee);
        }
        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void StaffCategory_Property_Is_Stored_Correctly_When_Staff_Is_Saved_To_DataBase()
        {
            _repo.Create(TestData.sampleEmployee);
            _repo.Create(TestData.sampleManager);
            var allStaffs = _context.Staffs;
            var dbEmployee = allStaffs.Where(x => x.Username == TestData.sampleEmployee.Username).Single();
            Staff dbManager = allStaffs.Where(x => x.Username == TestData.sampleManager.Username).Single();
            Assert.AreEqual((int)StaffsCategory.Employee, dbEmployee.StaffCategory);
            Assert.AreEqual((int)StaffsCategory.Manager, dbManager.StaffCategory);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        [ExpectedException(typeof(UsernameExistsException))]
        public void Throws_Exception_When_Staff_With_Username_Already_Exists_In_Database()
        {
            IStaffRepository repo = new StaffRepository(_context);
            repo.Create(TestData.sampleEmployee);
            repo.Create(TestData.sampleEmployee);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Staff_From_DataBase_Using_Id()
        {
            _repo.Create(TestData.sampleManager);
            Staff dbStaff = _context.Staffs.First();
            Staff repoStaff = _repo.GetStaff(dbStaff.Id);
            Assert.IsNotNull(repoStaff);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Staff_From_DataBase_Using_Username()
        {
            _repo.Create(TestData.sampleManager);
            Staff dbStaff = _context.Staffs.First();
            Staff repoStaff = _repo.GetStaff(dbStaff.Username);
            Assert.IsNotNull(repoStaff);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Should_Be_Able_To_Retrieve_All_Staffs_In_Database()
        {
            var staffsCount = _context.Staffs.Count();
            _repo.Create(TestData.sampleManager);
            _repo.Create(TestData.sampleEmployee);
            var allStaffs = _repo.GetAllStaffs();
            Assert.AreEqual(staffsCount + 2, allStaffs.Count);
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        [ExpectedException(typeof(NoMatchFound))]
        public void Should_Throw_Exception_When_Username_Is_Not_Found()
        {
            var fakeUsername = "mightyJane";
            var dbStaff = _repo.GetStaff(fakeUsername);
        }
    }
}
