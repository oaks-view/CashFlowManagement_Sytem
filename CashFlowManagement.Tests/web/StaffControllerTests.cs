using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Services;
using Moq;
using CashFlowManagement.Web.Controllers;
using System.Collections.Generic;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Tests.Core;
using System.Linq;
using EmployeeManagement.Tests;
using System.Web.Http;
using System.Web.Http.Results;

namespace CashFlowManagement.Tests.web
{
    [TestClass]
    public class StaffControllerTests
    {
        private Mock<IStaffService> _staffServiceMock;
        private List<Staff> _sampleStaffs = new List<Staff>
        {
            TestData.sampleEmployee,
            TestData.sampleManager
        };

        [TestInitialize]
        public void Before_Each_Test()
        {
            _staffServiceMock = new Mock<IStaffService>();

            _staffServiceMock
                .Setup(x => x.GetAllEmployees())
                .Returns(_sampleStaffs.Where(s => 
                (int)s.StaffCategory == (int)StaffsCategory.Employee).ToList<Staff>);
            _staffServiceMock
                .Setup(x => x.GetAllManagers())
                .Returns(_sampleStaffs.Where(s =>
                (int)s.StaffCategory == (int)StaffsCategory.Manager).ToList<Staff>);

            _staffServiceMock
                .Setup(x => x.GetStaff(It.IsAny<string>()))
                .Returns((string input) =>
                {
                    return _sampleStaffs.Find(x => x.Id == input);
                });
            _staffServiceMock
                 .Setup(x => x.GetAllStaffs())
                 .Returns(_sampleStaffs);

            _staffServiceMock
                .Setup(x => x.GetAllSavedIncomes(It.IsAny<string>()))
                .Returns((string input) =>
                {
                    return TestData._sampleIncomes.Where(x => x.StaffId == input).ToList<Income>();
                });

            _staffServiceMock
                .Setup(x => x.GetAllSavedExpenses(It.IsAny<string>()))
                .Returns((string input) =>
                {
                    return TestData._sampleExpenses.Where(x => x.StaffId == input).ToList<Expense>();
                });
        }
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Get_Retrieves_All_Saved_Staffs()
        {
            var controller = new StaffController(_staffServiceMock.Object);
            var apiCallResult = controller.Get();
            Assert.IsInstanceOfType(apiCallResult, typeof(OkNegotiatedContentResult<List<Staff>>));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Get_With_Parameter_Retrieves_A_Saved_Staff()
        {
            var controller = new StaffController(_staffServiceMock.Object);
            var apiCallResult = controller.Get(_sampleStaffs[0].Id);
            Assert.IsNotNull(apiCallResult);
            Assert.IsInstanceOfType(apiCallResult, typeof(OkNegotiatedContentResult<Staff>));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void GetSavedIncome_Can_Retrieve_Staff_Saved_Income_Successfully()
        {
            var controller = new StaffController(_staffServiceMock.Object);
            var apiCallResult = controller.GetSavedIncome(_sampleStaffs[0].Id);
            Assert.IsInstanceOfType(apiCallResult, typeof(OkNegotiatedContentResult<List<Income>>));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void GetSavedExpenses_Retrieves_All_Expenses_By_Staff()
        {
            var controller = new StaffController(_staffServiceMock.Object);
            var apiCallResult = controller.GetAllSavedExpenses(_sampleStaffs[1].Id);
            Assert.IsInstanceOfType(apiCallResult, typeof(OkNegotiatedContentResult<List<Expense>>));
        }
    }
}
