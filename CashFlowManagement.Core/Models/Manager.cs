using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Models
{
    public class Manager:IStaffEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int StaffCategory { get; private set; }

        public Manager()
        {
            StaffCategory = (int)StaffsCategory.Manager;
        }

    }
}
