using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;

namespace CashFlowManagement.Tests.Core
{
    [TestClass]
    public class EmployeeTests
    {
        [TestInitialize]
        public void TestInit()
        {
            //some logic here
        }
        [TestMethod]
        public void StaffCategory_Property_Is_Set__When_Employee_Instance_Is_Created()//moses modify testmethod name
        {
            IStaff employee = new Employee
            {
                Id = 1,
                FirstName = "Moses",
                LastName = "Scott",
            };
            var employeeCategory = (int)StaffsCategory.Employee;
            Assert.AreEqual(employeeCategory, (int)employee.StaffCategory);
        }
    }
}
