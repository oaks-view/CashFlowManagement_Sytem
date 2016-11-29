using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core;

namespace CashFlowManagement.Tests
{
    [TestClass]
    public class ExpenditureTests
    {
        private Expenditure _expenditure;
        private string _expenditureDescription = "Milk";
        private int _expenditureCost = 4000;
        [TestInitialize]
        public void InitTest()
        {
            _expenditure = new Expenditure(_expenditureDescription, _expenditureCost);
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
        public void Confirm__dateCreated_Field_Is_Set_Once_Id_Property_Is_Set()
        {
            DateTime today = DateTime.Today;
            _expenditure.Id = 2;
            Assert.AreEqual(today.Month, _expenditure.GetDate().Month);
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
        public void Both_Cost_And_Description_Is_Changed_When_EditExpenditure_IsCalled()
        {
            var newCost = 2250;
            var newDescription = "Transportation";
            _expenditure.EditExpenditure(newDescription, newCost);
            Assert.AreEqual(newCost, _expenditure.Cost);
            Assert.AreEqual(_expenditure.Description, newDescription);
        }
    }
}
