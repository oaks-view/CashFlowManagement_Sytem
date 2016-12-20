using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Services;
using CashFlowManagement.Core.Data.DB;
using Moq;
using CashFlowManagement.Tests.Core;
using CashFlowManagement.Web.Controllers;
using CashFlowManagement.Core.Models;
using System.Web.Http;

namespace CashFlowManagement.Tests.web
{
    [TestClass]
    public class IncomeControllerTests
    {
        private Mock<IIncomeService> _incomeServiceMock;
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
                    return TestData._sampleIncomes.Find( x => x.Id == incomeId);
                });

            _incomeServiceMock
                .Setup(x => x.CreateIncome(It.IsAny<Income>())).Verifiable();
        }
        [TestMethod]
        public void Returns_Saved_Income()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            var incomeId = TestData._sampleIncomes[0].Id;
            var apiCallResult = incomeController.Get(incomeId);
            Assert.IsInstanceOfType(apiCallResult, typeof(Income));
        }

        [TestMethod]
        public void Returns_All_Saved_Income()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            var apiCallResult = incomeController.Get();
            Assert.AreEqual(TestData._sampleIncomes.Count, apiCallResult.Count);
        }

        [TestMethod]
        public void Income_Is_Saved_Successfully()
        {
            var incomeController = new IncomeController(_incomeServiceMock.Object);
            Income newIncome = TestData._sampleIncomes[0];
            incomeController.Post(newIncome);
            _incomeServiceMock.Verify(x => x.CreateIncome(newIncome));
        }
    }
}
