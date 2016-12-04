using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;
using EmployeeManagement.Tests;

namespace CashFlowManagement.Tests.Core
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void StaffCategory_Property_Is_Set__When_Employee_Instance_Is_Created()
        {
            IStaffEntity employee = new Employee
            {
                Id = 1,
                FirstName = "Moses",
                LastName = "Scott",
            };
            var employeeCategory = (int)StaffsCategory.Employee;
            Assert.AreEqual(employeeCategory, employee.StaffCategory);
        }
    }
}
