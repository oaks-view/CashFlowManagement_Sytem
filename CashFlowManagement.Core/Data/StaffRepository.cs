using CashFlowManagement.Core.Data.DB;
using CashFlowManagement.Core.Exceptions;
using CashFlowManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data
{
    public class StaffRepository:IStaffRepository
    {
        private CashFlowEntities _db;

        public StaffRepository()
        {
            _db = new CashFlowEntities();
        }

        public StaffRepository(CashFlowEntities context)
        {
            _db = context;
        }

        public void Create(IStaffEntity companyStaff)
        {
            Staff CompanyStaff = new Staff();
            CompanyStaff.FirstName = companyStaff.FirstName;
            CompanyStaff.LastName = companyStaff.LastName;
            CompanyStaff.Username = companyStaff.Username;
            CompanyStaff.Password = companyStaff.Password;
            CompanyStaff.StaffCategory = companyStaff.StaffCategory;

            _db.Staffs.Add(CompanyStaff);
            try
            {
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                DBExceptionsHandler.HandleException(e);
            }
        }

        public void Update(IStaffEntity companyStaff)
        {
            Staff dbStaff = GetStaff(companyStaff.Id);
            dbStaff.FirstName = companyStaff.FirstName;
            dbStaff.LastName = companyStaff.LastName;

        }
        public Staff GetStaff(int Id)//moses would probably remove if never used
        {
            return _db.Staffs.Find(Id);
        }
        public Staff GetStaff(string username)
        {
            Staff staff = _db.Staffs
                 .Where(x => x.Username == username)
                 .Select(x => x).SingleOrDefault();
            if (staff == null)
            {
                throw new NoMatchFound();
            }
            return staff;
        }

        public List<Staff> GetAllStaffs()
        {
            return _db.Staffs.ToList();
        }
    }
}
