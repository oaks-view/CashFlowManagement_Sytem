﻿using CashFlowManagement.Core.Exceptions;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Core.Data
{
    public class StaffRepository:IStaffRepository,IDisposable
    {
        private CashFlowEntities _db;
        private readonly bool _externalContext;

        public StaffRepository()
        {
            _db = new CashFlowEntities();
        }

        public StaffRepository(CashFlowEntities context) 
        {
            _db = context;
            _externalContext = true;
        }

        public void Create(Staff staff)
        {
            _db.Staffs.Add(staff);
            _db.SaveChanges();
        }

        public Staff GetStaff(string Id)
        {
            return _db.Staffs.Find(Id);
        }

        public List<Staff> GetAllStaffs()
        {
            return _db.Staffs.ToList();
        }

        public void Dispose()
        {
            if (_db == null || _externalContext)
            {
                return;
            }
            _db.Dispose();
        }
    }
}
