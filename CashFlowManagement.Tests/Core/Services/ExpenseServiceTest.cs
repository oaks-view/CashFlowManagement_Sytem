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

namespace CashFlowManagement.Tests.Core.Services
{
    [TestClass]
    public class ExpenseServiceTest
    {
        private Mock<IExpenseRepository> _mockExpenseRepo;
        private List<Expense> _sampleExpenses = new List<Expense>
        {
            new Expense
            {
                Description = "Expense1",
                Cost = 23000,
                Id = 23,
                StaffId = sampleEmployee.Id,
                Staff = sampleEmployee,
                DateCreated = DateTime.Now,
            },
            new Expense
            {
                Description = "Expense2",
                Cost = 8750,
                Id = 122,
                StaffId = sampleEmployee.Id,
                Staff = sampleEmployee,
                DateCreated = DateTime.Now,
            },
            new Expense
            {
                Description = "Expense3",
                Cost = 8000,
                Id = 663,
                StaffId = sampleEmployee.Id,
                Staff = sampleEmployee
            },
            new Expense
            {
                Description = "Expense4",
                Cost = 6450,
                Id = 822,
                StaffId = sampleEmployee.Id,
                Staff = sampleEmployee,
            }
        };

        [TestInitialize]
        public void BeforeTests()
        {
            _mockExpenseRepo = new Mock<IExpenseRepository>();
            _mockExpenseRepo.Setup(x => x.GetAllExpenses()).Returns(_sampleExpenses);
            _mockExpenseRepo.Setup(x => x.GetExpense(It.IsAny<int>()))
                .Returns((int arg) => {
                    return _sampleExpenses.FirstOrDefault(x => x.Id == arg);
                });
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
    }
}
