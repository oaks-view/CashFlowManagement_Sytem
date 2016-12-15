using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Data;
using static CashFlowManagement.Tests.Core.TestData;
using System.Collections.Generic;
using CashFlowManagement.Core.Models;
using EmployeeManagement.Tests;
using CashFlowManagement.Core.Services;
using System.Linq;

namespace CashFlowManagement.Tests.Core.Services
{
    [TestClass]
    public class StaffServicesTests
    {
        private Mock<IStaffRepository> _staffRepoMock;
        private List<Staff> _sampleStaffs = new List<Staff>
        {
            sampleEmployee,
            sampleManager
        };

        private List<Expense> sampleExpenses = new List<Expense>
            {
                new Expense("Expense1", 20000, sampleEmployee.Id),
                new Expense("Expense2", 12000, sampleEmployee.Id)
            };
        private List<Income> sampleIncomes = new List<Income>
            {
                new Income("Income1", 34000, sampleManager.Id),
                new Income("Income2", 45000, sampleManager.Id)
            };


        [TestInitialize]
        public void BeforeEachTest()
        {
            _staffRepoMock = new Mock<IStaffRepository>();
            _staffRepoMock.Setup(x => x.GetStaff(It.IsAny<string>()))
                .Returns((string arg) => {
                    return _sampleStaffs.FirstOrDefault(x => x.Id == arg);
                });
            _staffRepoMock.Setup(x => x.GetAllStaffs())
                .Returns(new List<Staff> { sampleEmployee, sampleManager });

            _sampleStaffs[0].Expenses = sampleExpenses;
            _sampleStaffs[1].Expenses = sampleExpenses;
            _sampleStaffs[1].Incomes = sampleIncomes;
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Succesfully_Create_Instance_Of_StaffService()
        {
            var service = new StaffService(_staffRepoMock.Object);
            Assert.IsNotNull(service);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Should_Be_Able_To_Retrieve_A_Staff()
        {
            var service = new StaffService(_staffRepoMock.Object);
            Staff employee = service.GetStaff(_sampleStaffs[0].Id);
            Assert.AreEqual(_sampleStaffs[0].Name, employee.Name);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_All_Staffs()
        {
            var service = new StaffService(_staffRepoMock.Object);
            var allStaffs = service.GetAllStaffs();
            Assert.AreEqual(_sampleStaffs.Count, allStaffs.Count);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_All_Employees_Only()
        {
            var service = new StaffService(_staffRepoMock.Object);
            var allEmployees = service.GetAllEmployees();
            Assert.AreEqual(1, allEmployees.Count);
            Assert.AreEqual(_sampleStaffs[0].Name, allEmployees.Single().Name);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_All_Managers_Only()
        {
            var service = new StaffService(_staffRepoMock.Object);
            var allManagers = service.GetAllManagers();
            Assert.IsTrue(allManagers.Count == 1);
            Assert.IsTrue(allManagers.Single().Name == _sampleStaffs[1].Name);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_Saved_Income_From_Staff()
        {
            var service = new StaffService(_staffRepoMock.Object);
            var savedIncomes = service.GetAllSavedIncomes(_sampleStaffs[1].Id);
            Assert.AreEqual(_sampleStaffs[1].Incomes.Count, savedIncomes.Count);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Retrieve_Saved_Expenses_From_Staff()
        {
            var service = new StaffService(_staffRepoMock.Object);
            var savedExpenses = service.GetAllSavedIncomes(sampleManager.Id);
            Assert.AreEqual(2, savedExpenses.Count);
        }
    }
}
