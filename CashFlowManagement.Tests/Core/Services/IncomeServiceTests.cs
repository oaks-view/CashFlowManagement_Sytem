using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Tests;
using CashFlowManagement.Core.Data.DB;
using Moq;
using CashFlowManagement.Core.Services;
using CashFlowManagement.Core.Models;
using System.Collections.Generic;
using static CashFlowManagement.Tests.Core.TestData;
using System.Linq;

namespace CashFlowManagement.Tests.Core.Services
{
    [TestClass]
    public class IncomeServiceTests
    {
        private Mock<IIncomeRepository> _mockIncomeRepository;

        [TestInitialize]
        public void BeforeTests()
        {
            _mockIncomeRepository = new Mock<IIncomeRepository>();
            _mockIncomeRepository.Setup(x => x.GetAllIncome()).Returns(_sampleIncomes);
            _mockIncomeRepository.Setup(x => x.GetIncome(It.IsAny<int>()))
                .Returns((int arg) => {
                    return _sampleIncomes.FirstOrDefault(x => x.Id == arg);
                });

            _mockIncomeRepository.Setup(x => x.Create(It.IsAny<Income>())).Verifiable();
            _mockIncomeRepository.Setup(x => x.Update(It.IsAny<Income>())).Verifiable();
            _mockIncomeRepository.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Create_Instance_Of_Income_Service()
        {
            var service = new IncomeService(_mockIncomeRepository.Object);
            Assert.IsNotNull(service);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_Saved_Income()
        {
            var service = new IncomeService(_mockIncomeRepository.Object);
            var income = service.GetIncome(_sampleIncomes[0].Id);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_All_Saved_Income()
        {
            var service = new IncomeService(_mockIncomeRepository.Object);
            var allIncome = service.GetAllIncome();
            Assert.AreEqual(4, allIncome.Count);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Get_Staff_From_Income()
        {
            var service = new IncomeService(_mockIncomeRepository.Object);
            var income = service.GetIncome(_sampleIncomes[0].Id);
            Assert.AreEqual(sampleManager.Name, income.Staff.Name);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Get_Monthly_Income_From_Saved_Incomes()
        {
            var service = new IncomeService(_mockIncomeRepository.Object);
            Dictionary<string, int> monthlyIncomes = service.GetMonthlyIncome();
            var allKeys = monthlyIncomes.Keys;
            var currentMonth = DateTime.Now.Month;

            var testKey = _sampleIncomes[0].DateCreated.Month.ToString();
            Assert.IsTrue(allKeys.Contains(testKey));

            var testSum = _sampleIncomes[0].Amount + _sampleIncomes[1].Amount;
            Assert.AreEqual(testSum, monthlyIncomes[testKey]);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Get_Annual_Income_From_Saved_Incomes()
        {
            var service = new IncomeService(_mockIncomeRepository.Object);
            var yearlyIncome = service.GetYearlyIncome();
            var currentYear = DateTime.Now.Year.ToString();
            var allKeys = yearlyIncome.Keys;
            var sampleSum = _sampleIncomes[0].Amount + _sampleIncomes[1].Amount;
            Assert.IsTrue(allKeys.Contains(currentYear));
            Assert.AreEqual(sampleSum, yearlyIncome[currentYear]);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Update_Saved_Income()
        {
            Income newIncome = _sampleIncomes[0];
            var service = new IncomeService(_mockIncomeRepository.Object);
            service.CreateIncome(newIncome);
            _mockIncomeRepository.Verify(x => x.Update(It.Is<Income>(
                inc => inc.Description == newIncome.Description
                && inc.Amount == newIncome.Amount
                &&inc.StaffId == newIncome.StaffId
                &&inc.Staff == newIncome.Staff
                && inc.Id == newIncome.Id))
                );
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Save_New_Income()
        {
            Income newIncome = new Income(
                "AnyIncome",
                23000,
                "any-stffs-id-12ert"
                );
            var service = new IncomeService(_mockIncomeRepository.Object);
            service.CreateIncome(newIncome);
            _mockIncomeRepository.Verify(x => x.Create(It.Is<Income>(inc=>
            inc.Description == "AnyIncome"
            )));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Delete_Saved_Income()
        {
            var service = new IncomeService(_mockIncomeRepository.Object);
            var savedIncome = _sampleIncomes[0];
            service.DeleteIncome(savedIncome.Id);
            _mockIncomeRepository.Verify(x => x.Delete(It.Is<int>(input =>
            input == savedIncome.Id)
            ));
        }
    }
}
