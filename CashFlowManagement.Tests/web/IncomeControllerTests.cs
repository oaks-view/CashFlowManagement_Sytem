using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Services;
using CashFlowManagement.Core.Data.DB;
using Moq;
using CashFlowManagement.Tests.Core;
using CashFlowManagement.Web.Controllers;
using CashFlowManagement.Core.Models;
using System.Web.Http;
using EmployeeManagement.Tests;
using System.Collections.Generic;

namespace CashFlowManagement.Tests.web
{
    [TestClass]
    public class IncomeControllerTests
    {
        private Mock<IIncomeService> _incomeServiceMock;

        private Dictionary<string, int> _monthlyIncomeVal
            = new Dictionary<string, int> { { "4", 450000 }, { "5", 450000 } };

        private Dictionary<string, int> _yearlyIncomeVal
            = new Dictionary<string, int> { { "2015", 9000000 }, { "2016", 12000000 } };

        [TestInitialize]
        public void BeforeEachTest()
        {
            _incomeServiceMock = new Mock<IIncomeService>();

            _incomeServiceMock
                .Setup(x => x.GetAllIncome())
                .Returns(TestData._sampleIncomes);

            _incomeServiceMock
                .Setup(x => x.GetIncome(It.IsAny<int>()))
                .Returns((int incomeId) =>
                {
                    return TestData._sampleIncomes.Find(x => x.Id == incomeId);
                });

            _incomeServiceMock
                .Setup(x => x.CreateIncome(It.IsAny<Income>())).Verifiable();

            _incomeServiceMock
                .Setup(x => x.GetMonthlyIncome())
                .Returns(_monthlyIncomeVal);

            _incomeServiceMock
                .Setup(x => x.GetYearlyIncome())
                .Returns(_yearlyIncomeVal);
        }
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Get_With_Parameter_Returns_Saved_Income()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            var incomeId = TestData._sampleIncomes[0].Id;
            var apiCallResult = incomeController.Get(incomeId);
            Assert.IsInstanceOfType(apiCallResult, typeof(Income));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Get_Returns_All_Saved_Income()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            var apiCallResult = incomeController.Get();
            Assert.AreEqual(TestData._sampleIncomes.Count, apiCallResult.Count);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Post_Saves_Income_Successfully()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            Income newIncome = TestData._sampleIncomes[0];
            incomeController.Post(newIncome);
            _incomeServiceMock.Verify(x => x.CreateIncome(It.Is<Income>(inc =>
                inc.Description == newIncome.Description
                && inc.Amount == newIncome.Amount
                && inc.StaffId == newIncome.StaffId)));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Put_Updates_Income_Successfully()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            Income prevIncome = TestData._sampleIncomes[0];
            prevIncome.Description = "UpdatedDescription";
            prevIncome.Amount = 900;
            incomeController.Put(prevIncome);
            _incomeServiceMock.Verify(x => x.CreateIncome(It.Is<Income>(inc =>
                inc.Description == "UpdatedDescription"
                && inc.Amount == 900
                && inc.StaffId == prevIncome.StaffId)
                ));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void GetMonthlyIncome_API_Call_Works_Correctly()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            var apiCallResult = incomeController.GetMonthlyIncome();
            Assert.AreEqual(_monthlyIncomeVal.Keys, apiCallResult.Keys);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void GetYearlyIncome_API_Call_Works_Correctly()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            var apiCallResult = incomeController.GetYearlyIncome();
            Assert.AreEqual(_yearlyIncomeVal.Keys, apiCallResult.Keys);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void DELETE_Successfully_Removes_Saved_Income()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            var savedIncome = TestData._sampleIncomes[0];
            incomeController.Delete(savedIncome.Id);
            _incomeServiceMock.Verify(x => x.DeleteIncome(It.Is<int>(input =>
            input == savedIncome.Id)
            ));
        }
    }
}
