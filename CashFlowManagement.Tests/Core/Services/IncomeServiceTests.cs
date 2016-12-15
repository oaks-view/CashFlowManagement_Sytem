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
        private List<Income> _sampleIncomes = new List<Income>
        {
            new Income
            {
                Description = "Income1",
                Amount = 22300,
                Id = 221,
                StaffId = sampleManager.Id,
                Staff = sampleManager,
                DateCreated = DateTime.Now
            },
            new Income
            {
                Description = "Income2",
                Amount = 45000,
                Id = 912,
                StaffId = sampleManager.Id,
                Staff = sampleManager,
                DateCreated = DateTime.Now
            },
            new Income
            {
                Description = "Income3",
                Amount = 13000,
                Id = 81,
                StaffId = sampleManager.Id,
                Staff = sampleManager
            },
            new Income
            {
                Description = "Income4",
                Amount = 5000,
                Id = 372,
                StaffId = sampleManager.Id,
                Staff = sampleManager
            }
        };

        [TestInitialize]
        public void BeforeTests()
        {
            _mockIncomeRepository = new Mock<IIncomeRepository>();
            _mockIncomeRepository.Setup(x => x.GetAllIncome()).Returns(_sampleIncomes);
            _mockIncomeRepository.Setup(x => x.GetIncome(It.IsAny<int>()))
                .Returns((int arg) => {
                    return _sampleIncomes.FirstOrDefault(x => x.Id == arg);
                });

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
    }
}
