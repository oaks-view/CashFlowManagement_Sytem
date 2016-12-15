using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlowManagement.Core.Data;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Exceptions;

namespace CashFlowManagement.Core.Services
{
    public class StaffService:IStaffService
    {
        private IStaffRepository _staffRepository;
        public StaffService(IStaffRepository staffRepo)
        {
            _staffRepository = staffRepo;
        }

        public void CreateStaff(Staff staff)
        {
            var dbStaff = _staffRepository.GetStaff(staff.Id);
            if (dbStaff != null)
            {
                return;
            }
            try
            {
                _staffRepository.Create(staff);
            }

            catch (Exception e)
            {
                DBExceptionsHandler.HandleException(e);
            }
        }

        public Staff GetStaff(string id)
        {
            Staff staff = _staffRepository.GetStaff(id);
            return staff;//moses exception should be throwwn when staf is not found
        }

        public List<Staff> GetAllStaffs()
        {
            return _staffRepository.GetAllStaffs();
        }

        public List<Staff> GetAllManagers()
        {
            var allManagers = GetAllStaffs().Where(x => x.StaffCategory == (int)StaffsCategory.Manager);
            return allManagers.ToList();
        }

        public List<Staff> GetAllEmployees()
        {
            var allEmployees = GetAllStaffs().Where(x => x.StaffCategory == (int)StaffsCategory.Employee);
            return allEmployees.ToList();
        }

        public List<Expense> GetAllSavedExpenses(string staffId)
        {
            var staff = GetStaff(staffId);
            var staffExpenses = staff.Expenses;
            return staffExpenses.ToList();
        }

        public List<Income> GetAllSavedIncomes(string staffId)
        {
            var manager = GetStaff(staffId);
            var incomes = manager.Incomes;
            return incomes.ToList();
        }
    }
}
