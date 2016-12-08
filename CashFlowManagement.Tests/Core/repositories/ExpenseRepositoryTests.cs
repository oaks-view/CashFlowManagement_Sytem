using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Tests;
using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Data;
using System.Data.Entity;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core;
using System.Linq;
using CashFlowManagement.Core.Models.DB;
using static CashFlowManagement.Tests.Core.TestData;

namespace CashFlowManagement.Tests.Core.repositories
{
    [TestClass]
    public class ExpenseRepositoryTests
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

            _transaction.Rollback();
            _transaction.Dispose();
            _context.Dispose();
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Save_New_Expense_To_Database()
        {
            using (var repo = new ExpenseRepository(_context))
            {
                _context.Staffs.Add(sampleEmployee);
                _context.SaveChanges();
                Staff employee = _context.Staffs.Where(x => x.Username == sampleEmployee.Username).SingleOrDefault();
                Expense staffExpense = new Expense
                (
                    "Unitest_Expenses",
                    30000,
                    employee.Id
                );
                employee.Expenses.Add(staffExpense);
                _context.SaveChanges();
                Expense queryObj = _context.Expenses.SingleOrDefault();
                Assert.AreEqual("Unitest_Expenses", queryObj.Description);
                Assert.AreEqual(employee.Id, queryObj.StaffId);
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Saved_Expense_From_Database()
        {
            using (var repo = new ExpenseRepository(_context))
            {
                _context.Staffs.Add(sampleEmployee);
                _context.SaveChanges();
                Staff employee = _context.Staffs.Where(x => x.Username == sampleEmployee.Username).Single();
                Expense staffExpense = new Expense
                (
                    "Unitest_Expenses",
                    30000,
                    employee.Id
                );

                employee.Expenses.Add(staffExpense);
                _context.SaveChanges();
                Expense queryObj = repo.GetExpense(_context.Expenses.Single().Id);
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_All_Expenses_From_Database()
        {
            using (var repo = new ExpenseRepository(_context))
            {
                _context.Staffs.Add(sampleManager);
                _context.SaveChanges();
                var staff = _context.Staffs.Single();
                repo.Create(new Expense("E1", 2000, staff.Id));
                repo.Create(new Expense("E2", 32000, staff.Id));
                var allExpenses = repo.GetAllExpenses();
                Assert.AreEqual(2, allExpenses.Count);
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Edit_Saved_Expenses()
        {
            using (var repo = new ExpenseRepository(_context))
            {
                _context.Staffs.Add(sampleManager);
                _context.SaveChanges();
                var staff = _context.Staffs.Single();
                repo.Create(new Expense("E1", 2000, staff.Id));
                Expense expense = _context.Expenses.Single();
                repo.Update("Test_Expenditure", 45000, expense.Id);
                Assert.IsNotNull(_context.Expenses.Where(x => x.Description == "Test_Expenditure").Single());
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Delete_Saved_Expense_From_DataBase()
        {
            using (var repo = new ExpenseRepository(_context))
            {
                _context.Staffs.Add(sampleManager);
                _context.SaveChanges();
                var staff = _context.Staffs.Single();
                repo.Create(new Expense("E1", 2000, staff.Id));
                repo.Create(new Expense("E2", 2000, staff.Id));
                Assert.IsTrue(_context.Expenses.Count() == 2);

                Expense expense = _context.Expenses.First();
                repo.Delete(expense.Id);
                Assert.AreEqual(1, _context.Expenses.Count());
            }
        }
    }
}
