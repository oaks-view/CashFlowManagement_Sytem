using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data
{
    public class StaffRepository
    {
        private CashFlowEntities _db;

        public StaffRepository()
        {
            _db = new CashFlowEntities();
        }

        public void SaveEmployeeToDatabase(IStaff employee)
        {
            Staff Employee = new Staff();
            Employee.FirstName = employee.FirstName;
            Employee.LastName = employee.LastName;
            Employee.Username = employee.Username;
            Employee.Password = employee.Password;

            _db.Staffs.Add(Employee);
            _db.SaveChanges();
        }
    }
}
