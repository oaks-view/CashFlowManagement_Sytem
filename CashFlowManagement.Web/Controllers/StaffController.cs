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
    [Authorize(Roles = "Manager")]

    public class StaffController : ApiController
    {
        private IStaffService _service;

        public StaffController()
        {
            _service = new StaffService(new StaffRepository());
        }
        public StaffController(IStaffService staffService)
        {
            _service = staffService;
        }

        public Staff Get(string userId)
        {
            return _service.GetStaff(userId);
        }
        public void Post()
        {
            string userId = User.Identity.GetUserId();
            Staff staff = new Staff
            {
                Id = userId,
                Name = User.Identity.Name,
                StaffCategory = (int)StaffsCategory.Manager
            };
            _service.CreateStaff(staff);
        }
    }
}
