using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core;
using CashFlowManagement.Core.Models;

namespace CashFlowManagement.Tests
{
    [TestClass]
    public class ExpenditureTests
    {
        private Expenditure _expenditure;
        private string _expenditureDescription = "Milk";
        private int _expenditureCost = 4000;
        private IStaff _staff;
        [TestInitialize]
        public void InitTest()
        {
            _staff = new Employee
            {
                Id = 1,
                FirstName = "Moses",
                LastName = "Scott",
            };
            _expenditure = new Expenditure(_expenditureDescription, _expenditureCost, _staff.Id);
            Assert.AreEqual(_expenditureDescription, _expenditure.Description);
            Assert.AreEqual(_expenditureCost, _expenditure.Cost);
        }

        [TestMethod]
        public void Confirm_Creation_Of_Expenditure_Instance_Works_Correctly()
        {
            Assert.AreEqual(_expenditureDescription, _expenditure.Description);
            Assert.AreEqual(_expenditureCost, _expenditure.Cost);
        }

        [TestMethod]
        public void DateCreated_Property_Is_Set_When_Expenditure_Instantiated_With_The_Correct_Arguments()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd HH");
            Assert.AreEqual(today, _expenditure.DateCreated.ToString("yyyy-MM-dd HH"));
        }

        [TestMethod]
        public void Only_Description_Is_Changed_When_EditExpenditure_Is_Called_With_String_Input()
        {
            int PreviousCost = _expenditure.Cost;
            string newDescription = "Hotel Expenses";
            _expenditure.EditExpenditure(newDescription);
            Assert.AreEqual(_expenditure.Description, newDescription);
            Assert.AreEqual(_expenditure.Cost, PreviousCost);
        }

        [TestMethod]
        public void Only_Cost_Is_Changed_When_EditExpenditure_Is_Called_With_Integer_Input()
        {
            int newCost = 3500;
            string PreviousDescription = _expenditure.Description;
            _expenditure.EditExpenditure(newCost);
            Assert.AreEqual(newCost, _expenditure.Cost);
            Assert.AreEqual(_expenditure.Description, PreviousDescription);
        }
        [TestMethod]
        public void Cost_And_Description_Is_Changed_When_EditExpenditure_Is_Called_With_String_And_Integer_Arguments()
        {
            var newCost = 2250;
            var newDescription = "Transportation";
            _expenditure.EditExpenditure(newDescription, newCost);
            Assert.AreEqual(newCost, _expenditure.Cost);
            Assert.AreEqual(_expenditure.Description, newDescription);
        }
    }
}
