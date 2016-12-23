using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Services
{
    public interface IStaffService
    {
        void CreateStaff(Staff staff);
        Staff GetStaff(string Id);
        List<Staff> GetAllStaffs();
        List<Staff> GetAllManagers();
        List<Staff> GetAllEmployees();
        List<Income> GetAllSavedIncomes(string staffId);
        List<Expense> GetAllSavedExpenses(string staffId);
    }
}
