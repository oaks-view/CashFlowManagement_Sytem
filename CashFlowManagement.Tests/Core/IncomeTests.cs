using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;
using EmployeeManagement.Tests;
using CashFlowManagement.Core.Models.DB;

namespace CashFlowManagement.Tests.Core
{
    [TestClass]
    public class IncomeTests
    {

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Create_Instance_Of_Income()
        {
            Income income = new Income
            {
                Description = "Subscription",
                Amount = 230000,
                StaffId = TestData.sampleManager.Id,
                DateCreated = DateTime.Now
            };
            Assert.IsNotNull(income);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void DateCreated_Property_Is_Set_When_Income_Is_Instantiated_With_The_Correct_Arguments()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            Income income = new Income("Subscriptions Revenue", 232000, TestData.sampleManager.Id);
            Assert.AreEqual(today, income.DateCreated.ToString("yyyy-MM-dd HH:mm"));
        }
    }
}
