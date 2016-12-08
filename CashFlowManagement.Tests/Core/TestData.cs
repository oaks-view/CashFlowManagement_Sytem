using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Tests.Core
{
    public class TestData
    {
        public static Staff sampleEmployee = new Staff
        {
            Id = 23,
            FirstName = "Clark",
            LastName = "Kent",
            Username = "kalel",
            Password = "superman",
            StaffCategory = (int)StaffsCategory.Employee

        };

        public static Staff sampleManager = new Staff
        {
            Id = 12,
            FirstName = "Bruce",
            LastName = "Wayne",
            Username = "batman",
            Password = "gothamcity",
            StaffCategory = (int)StaffsCategory.Manager
        };
    }
}
