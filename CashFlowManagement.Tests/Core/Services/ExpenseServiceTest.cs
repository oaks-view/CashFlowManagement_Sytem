using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Data;
using Moq;
using CashFlowManagement.Core.Models;
using System.Collections.Generic;
using static CashFlowManagement.Tests.Core.TestData;
using System.Linq;
using CashFlowManagement.Core.Services;
using EmployeeManagement.Tests;
using static CashFlowManagement.Tests.Core.TestData;

namespace CashFlowManagement.Tests.Core.Services
{
    [TestClass]
    public class ExpenseServiceTest
    {
        private Mock<IExpenseRepository> _mockExpenseRepo;

        [TestInitialize]
        public void BeforeTests()
        {
            _mockExpenseRepo = new Mock<IExpenseRepository>();
            _mockExpenseRepo.Setup(x => x.GetAllExpenses()).Returns(_sampleExpenses);
            _mockExpenseRepo.Setup(x => x.GetExpense(It.IsAny<int>()))
                .Returns((int arg) => {
                    return _sampleExpenses.FirstOrDefault(x => x.Id == arg);
                });

            _mockExpenseRepo
                .Setup(x => x.Create(It.IsAny<Expense>())).Verifiable();

            _mockExpenseRepo
                .Setup(x => x.Update(It.IsAny<Expense>())).Verifiable();

            _mockExpenseRepo
                .Setup(x => x.Delete(It.IsAny<int>())).Verifiable();
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Successfully_Instantiate_ExpenseService()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            Assert.IsNotNull(service);
        }
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_Saved_Expense()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            var expense = service.GetExpense(_sampleExpenses[0].Id);
            Assert.AreEqual(_sampleExpenses[0].Description, expense.Description);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_All_Expenses()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            var allExpense = service.GetAllExpense();
            Assert.IsTrue(allExpense.Count == 4);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_All_Staff_From_Expenses()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            var expense = service.GetExpense(_sampleExpenses[0].Id);
            Assert.AreEqual(_sampleExpenses[0].Staff.Name, expense.Staff.Name);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Get_Monthly_Expenses_From_Saved_Expenses()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            var monthlyIncome = service.GetMonthlyExpenses();
            var allKeys = monthlyIncome.Keys;
            var currentMonth = DateTime.Now.Month.ToString();
            var sampleSum = _sampleExpenses[0].Cost + _sampleExpenses[1].Cost;
            Assert.IsTrue(allKeys.Contains(currentMonth));
            Assert.AreEqual(sampleSum, monthlyIncome[currentMonth]);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Get_Annual_Expense_From_Saved_Expenses()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            var yearlyExpenses = service.GetYearlyExpenses();
            var allKeys = yearlyExpenses.Keys;
            var currentYear = DateTime.Now.Year.ToString();
            Assert.IsTrue(allKeys.Contains(currentYear));
            var sampleSum = _sampleExpenses[0].Cost + _sampleExpenses[1].Cost;
            Assert.AreEqual(sampleSum, yearlyExpenses[currentYear]);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Update_Saved_Expense()
        {
            Expense savedExpense = _sampleExpenses[0];
            var service = new ExpenseService(_mockExpenseRepo.Object);
            service.SaveExpense(savedExpense);
            _mockExpenseRepo.Verify(x => 
            x.Update(It.Is<Expense>(
                inc => inc.Description == savedExpense.Description
                && inc.Cost == savedExpense.Cost
                && inc.StaffId == savedExpense.StaffId
                && inc.Staff == savedExpense.Staff
                && inc.Id == savedExpense.Id)
                ));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Save_New_Expense()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            Expense expense = new Expense("AnyExpense", 12000, sampleManager.Id);
            service.SaveExpense(expense);
            _mockExpenseRepo.Verify(x => x.Create(It.Is<Expense>(exp =>
                exp.Description == "AnyExpense"
                && exp.Cost == 12000)
                ));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Delete_Saved_Expense()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            var savedExpense = _sampleExpenses[0];
            service.DeleteExpense(savedExpense.Id);
            _mockExpenseRepo.Verify(x => x.Delete(It.Is<int>(input =>
            input == savedExpense.Id
            )));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_Expenses_By_Staffs_Id()
        {
            var service = new ExpenseService(_mockExpenseRepo.Object);
            var staffExpenses = _sampleExpenses.Where(x => x.StaffId == sampleEmployee.Id).ToList<Expense>();
            var dbExpenses = service.GetStaffExpenses(sampleEmployee.Id);
            Assert.AreEqual(staffExpenses.Count, dbExpenses.Count);       
        }
    }
}
