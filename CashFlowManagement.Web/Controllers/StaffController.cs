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
    [Authorize]
    public class StaffController : ApiController
    {
        private IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public IHttpActionResult Get(string userId)
        {
            var staff = _staffService.GetStaff(userId);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        public IHttpActionResult GetSavedIncome(string staffId)
        {
            var savedIncomes = _staffService.GetAllSavedIncomes(staffId);
            if (savedIncomes == null)
            {
                return NotFound();
            }
            return Ok(savedIncomes);
        }

        public IHttpActionResult Get()
        {
            var staffs = _staffService.GetAllStaffs();
            if (staffs == null)
            {
                return NotFound();
            }
            return Ok(staffs);
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
            _staffService.CreateStaff(staff);
        }

        public IHttpActionResult GetAllSavedExpenses(string staffId)
        {
            var allSavedExpenses = _staffService.GetAllSavedExpenses(staffId);
            if (allSavedExpenses == null)
            {
                return NotFound();
            }
            return Ok(allSavedExpenses);
        }

        public IHttpActionResult GetStaffCategory(string staffId)
        {
            var staffCategory = _staffService.GetStaffCategory(staffId);
            if (staffCategory == default(int))
            {
                return NotFound();
            }
            return Ok(staffCategory);
        }
    }
}
