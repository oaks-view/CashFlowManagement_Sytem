using CashFlowManagement.Core.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data
{
    public interface IStaffRepository
    {
        void Create(Staff staff);
        Staff GetStaff(string username);
        List<Staff> GetAllStaffs();
    }
}
