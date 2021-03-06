﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Tests;
using CashFlowManagement.Core.Models.DB;
using CashFlowManagement.Core.Models;

namespace CashFlowManagement.Tests.Core
{
    [TestClass]
    public class StaffTests
    {
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Can_Create_Staff_Instance()
        {
            Staff staff = new Staff
            {
                Id = "anyidfortest",
                Name = "Moses Fejiro",
                StaffCategory = (int)StaffsCategory.Employee
            };
            Assert.IsNotNull(staff);
        }
    }
}
