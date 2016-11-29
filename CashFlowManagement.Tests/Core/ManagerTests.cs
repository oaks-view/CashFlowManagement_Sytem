using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlowManagement.Core.Models;

namespace CashFlowManagement.Tests.Core
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void StaffCategory_Property_Is_Set__When_Manager_Instance_Is_Created()//moses modify testmethod name
        {
            IStaff manager = new Manager
            {
                Id = 1,
                FirstName = "Moses",
                LastName = "Scott",
            };
            var staffCategory = (int)StaffsCategory.Manager;
            Assert.AreEqual(staffCategory, (int)manager.StaffCategory);
        }
    }
}
