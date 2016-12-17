using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Services;
using CashFlowManagement.Core.Data;

namespace CashFlowManagement.Web.Controllers
{
    //[Authorize(Roles = "Manager,Employee")]
    //[Authorize]
    public class StaffController : ApiController
    {
        private IStaffService _service;

        public StaffController(IStaffService staffService)
        {
            _service = staffService;
        }

        public Staff Get(string userId)
        {
            return _service.GetStaff(userId);
        }

        public List<Staff> Get()
        {
            return _service.GetAllStaffs();
        }

        public void Post()
        {
            string userId = User.Identity.GetUserId();
            Staff staff = new Staff
            {
                Id = userId,
                Name = User.Identity.Name
            };
            bool userRole = User.IsInRole("Manager");
            staff.StaffCategory = userRole ? (int)StaffsCategory.Manager : (int)StaffsCategory.Employee;
            _service.CreateStaff(staff);
        }
    }
}
