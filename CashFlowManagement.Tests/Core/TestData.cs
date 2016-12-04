using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Tests.Core
{
    public class TestData
    {
        public static IStaffEntity sampleEmployee = new Employee
        {
            FirstName = "Clark",
            LastName = "Kent",
            Username = "kalel",
            Password = "superman",

        };

        public static IStaffEntity sampleManager = new Manager
        {
            FirstName = "Bruce",
            LastName = "Wayne",
            Username = "batman",
            Password = "gothamcity",
        };
    }
}
