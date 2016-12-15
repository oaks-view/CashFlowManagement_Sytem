using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;//import need to grab userId
using System.Security.Claims;//import was said to be needed
using CashFlowManagement.Core.Models;
using CashFlowManagement.Core.Services;
using CashFlowManagement.Core.Data;//moses unity injector takew cARE OF THIS

namespace CashFlowManagement.Web.Controllers
{
    [Authorize]

    public class StaffController : ApiController
    {
        private StaffService _service;
        public StaffController()
        {
            _service = new StaffService(new StaffRepository());
        }
        public string Get()
        {
            string userId = User.Identity.GetUserId();
            return userId;
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
