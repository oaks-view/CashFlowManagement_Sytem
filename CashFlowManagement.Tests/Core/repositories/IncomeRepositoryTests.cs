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
        private IIncomeRepository _repo;
        private CashFlowEntities _context;
        private DbContextTransaction _transaction;
        private IStaffRepository _staffRepo;
        Staff _managerDependency;


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
                Staff manager = staffRepo.GetStaff(sampleManager.Username);
                Income income = new Income("Web_Advertisment", 3000, manager.Id);
                repo.Create(income);
                var dbIncome = manager.Incomes.SingleOrDefault();
                Assert.AreEqual(3000, dbIncome.Amount);
                transaction.Rollback();
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Get_Staff_From_Saved_Income()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new IncomeRepository(context))
            using (var staffRepo = new StaffRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                staffRepo.Create(sampleManager);
                Staff manager = context.Staffs.FirstOrDefault();
                Income income = new Income("Wesite_Traffic", 3000, manager.Id);
                repo.Create(income);
                Income retrievedIncome = context.Incomes.SingleOrDefault();
                Assert.AreEqual(sampleManager.Username, retrievedIncome.Staff.Username);
                transaction.Rollback();
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Saved_Income_From_Database()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new IncomeRepository(context))
            using (var staffRepo = new StaffRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                context.Staffs.Add(sampleManager);
                context.SaveChanges();
                Staff manager = staffRepo.GetStaff(sampleManager.Username);
                Income income = new Income("Web_Adverts", 2300, manager.Id);
                context.Incomes.Add(income);
                context.SaveChanges();
                Income retrievedIncome = repo.GetIncome(manager.Incomes.SingleOrDefault().Id);
                Assert.AreEqual(2300, retrievedIncome.Amount);
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_DateCreated_From_Saved_Income()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new IncomeRepository(context))
            using (var staffRepo = new StaffRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                staffRepo.Create(sampleManager);
                Staff manager = context.Staffs.SingleOrDefault();
                Income income = new Income("IMF_Grant", 45000, manager.Id);
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd-HH");
                repo.Create(income);
                Income retrievedIncome = manager.Incomes.SingleOrDefault();
                Assert.AreEqual(currentDate, retrievedIncome.DateCreated.ToString("yyyy-MM-dd-HH"));
                transaction.Rollback();
            }
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
