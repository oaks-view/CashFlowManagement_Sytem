using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;
using EmployeeManagement.Tests;

namespace CashFlowManagement.Tests.Core
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void StaffCategory_Property_Is_Set__When_Manager_Instance_Is_Created()
        {
            IStaffEntity manager = new Manager
            {
                Id = 1,
                FirstName = "Moses",
                LastName = "Scott",
            };
            var staffCategory = (int)StaffsCategory.Manager;
            Assert.AreEqual(staffCategory, manager.StaffCategory);
        }
    }
}
