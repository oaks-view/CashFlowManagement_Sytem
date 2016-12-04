using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;
using EmployeeManagement.Tests;

namespace CashFlowManagement.Tests.Core
{
    [TestClass]
    public class IncomeEntityTests
    {
        private IStaffEntity _staff;
        private IncomeEntity _income;
        [TestInitialize]
        public void TestInit()
        {
            _staff = new Manager
            {
                FirstName = "Dayo",
                LastName = "Yankee",
                Id = 2,
                Username = "d1yankee"
            };
            _income = new IncomeEntity("Bank Interest", 30000, _staff.Id);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void DateCreated_Property_Is_Set_When_IncomeEntity_Is_Innstantiated_With_The_Correct_Arguments()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd HH");
            Assert.AreEqual(today, _income.DateCreated.ToString("yyyy-MM-dd HH"));
        }
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Only_Description_Property_Is_Changed_When_A_String_Argument_Is_Passed_To_EditIncome()
        {
            int previousIncomeAmount = _income.Amount;
            _income.EditIncome("Goods Payment");
            Assert.AreEqual(previousIncomeAmount, _income.Amount);
            Assert.AreEqual("Goods Payment", _income.Description);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Only_Amount_Property_Is_Changed_When_An_Int_Argument_Is_Passed_To_EditIncome()
        {
            string previousIncomeDescription = _income.Description;
            _income.EditIncome(98000);
            Assert.AreEqual(previousIncomeDescription, _income.Description);
            Assert.AreEqual(98000, _income.Amount);
        }
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Description_And_Amount_Is_Changed_When_Both_Integer_And_String_Arguments_Are_Passed_To_EditIncome()
        {
            _income.EditIncome("New Sales", 450000);
            Assert.AreEqual("New Sales", _income.Description);
            Assert.AreEqual(450000, _income.Amount);
        }
    }
}
