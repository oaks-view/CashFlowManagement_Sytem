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

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Save_New_Expense_To_Database()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new ExpenseRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                context.Staffs.Add(sampleEmployee);
                context.SaveChanges();
                Staff employee = context.Staffs.Where(x => x.Username == sampleEmployee.Username).SingleOrDefault();
                Expense staffExpense = new Expense
                (
                    "Unitest_Expenses",
                    30000,
                    employee.Id
                );
                employee.Expenses.Add(staffExpense);
                context.SaveChanges();
                Expense queryObj = context.Expenses.SingleOrDefault();
                Assert.AreEqual("Unitest_Expenses", queryObj.Description);
                Assert.AreEqual(employee.Id, queryObj.StaffId);
                transaction.Rollback();
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_Saved_Expense_From_Database()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new ExpenseRepository(context))
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                context.Staffs.Add(sampleEmployee);
                context.SaveChanges();
                Staff employee = context.Staffs.Where(x => x.Username == sampleEmployee.Username).Single();
                Expense staffExpense = new Expense
                (
                    "Unitest_Expenses",
                    30000,
                    employee.Id
                );

                employee.Expenses.Add(staffExpense);
                context.SaveChanges();
                Expense queryObj = repo.GetExpense(context.Expenses.Single().Id);
                transaction.Rollback();
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Retrieve_All_Expenses_From_Database()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new ExpenseRepository(context))
            using (var transaction = context.Database.BeginTransaction())
            {
                context.Staffs.Add(sampleManager);
                context.SaveChanges();
                var staff = context.Staffs.Single();
                repo.Create(new Expense("E1", 2000, staff.Id));
                repo.Create(new Expense("E2", 32000, staff.Id));
                var allExpenses = repo.GetAllExpenses();
                Assert.AreEqual(2, allExpenses.Count);
                transaction.Rollback();
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Edit_Saved_Expenses()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new ExpenseRepository(context))
            using (var transaction = context.Database.BeginTransaction())
            {
                context.Staffs.Add(sampleManager);
                context.SaveChanges();
                var staff = context.Staffs.Single();
                repo.Create(new Expense("E1", 2000, staff.Id));
                Expense expense = context.Expenses.Single();
                repo.Update("Test_Expenditure", 45000, expense.Id);
                Assert.IsNotNull(context.Expenses.Where(x => x.Description == "Test_Expenditure").Single());
                transaction.Rollback();
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Can_Delete_Saved_Expense_From_DataBase()
        {
            using (CashFlowEntities context = new CashFlowEntities())
            using (var repo = new ExpenseRepository(context))
            using (var transaction = context.Database.BeginTransaction())
            {
                context.Staffs.Add(sampleManager);
                context.SaveChanges();
                var staff = context.Staffs.Single();
                repo.Create(new Expense("E1", 2000, staff.Id));
                repo.Create(new Expense("E2", 2000, staff.Id));
                Assert.IsTrue(context.Expenses.Count() == 2);

                Expense expense = context.Expenses.First();
                repo.Delete(expense.Id);
                Assert.AreEqual(1, context.Expenses.Count());
                transaction.Rollback();
            }
        }
    }
}
