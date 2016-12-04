using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data
{
    public interface IStaffRepository
    {
        void Create(IStaffEntity staffObject);
        Staff GetStaff(string username);
        List<Staff> GetAllStaffs();
    }
}
