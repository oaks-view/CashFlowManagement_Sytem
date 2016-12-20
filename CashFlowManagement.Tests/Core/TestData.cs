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
            Id = "34482765-486c-45a3-805e-5857a1a04782",
            Name = "Clark Kent",
            StaffCategory = (int)StaffsCategory.Employee
        };

        public static Staff sampleManager = new Staff
        {
            Id = "776682765-226u-09a3-805o-5857a1a04782",
            Name = "Bruce Wayne",
            StaffCategory = (int)StaffsCategory.Manager
        };

        public static List<Income> _sampleIncomes = new List<Income>
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
    }
}
