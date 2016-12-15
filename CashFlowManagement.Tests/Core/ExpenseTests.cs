using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;
using EmployeeManagement.Tests;

namespace CashFlowManagement.Tests.Core
{
    [TestClass]
    public class ExpenseTests
    {
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Instatiate_Expenses_Successfully()
        {
            Expense expense = new Expense
            {
                Description = "Subscription",
                Cost = 230000,
                StaffId = TestData.sampleManager.Id,
                DateCreated = DateTime.Now
            };
            Assert.IsNotNull(expense);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void DateCreated_Property_Is_Set_When_Expense_Is_Instantiated_With_The_Correct_Arguments()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            Expense expense = new Expense("Subscriptions Revenue", 232000, TestData.sampleManager.Id);
            Assert.AreEqual(today, expense.DateCreated.ToString("yyyy-MM-dd HH:mm"));
        }
    }
}
