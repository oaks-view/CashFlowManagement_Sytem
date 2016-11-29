using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models
{
    public class Employee: IStaff
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; }
        public string Password { get; private set; }

        public StaffsCategory StaffCategory { get; private set; }

        public Employee()
        {
            StaffCategory = StaffsCategory.Employee;
        }
    }
}
