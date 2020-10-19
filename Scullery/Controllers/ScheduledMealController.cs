using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scullery.Data;
using System.Net.Http;
using Newtonsoft.Json;
using Scullery.Services;
using Scullery.Models.ViewModels;
using Scullery.Models;
using Microsoft.EntityFrameworkCore;

namespace Scullery.Controllers
{
    public class ScheduledMealController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduledMealController(ApplicationDbContext context)
        {
            _context = context;

        }

        private string GetLoggedInUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }

        private Planner GetLoggedInPlanner()
        {
            var planner = _context.Planners.Where(c => c.IdentityUserId == GetLoggedInUser()).SingleOrDefault();
            return planner;

        }

        // GET: ScheduledMealController
        public ActionResult Calendar()
        {
            return View();
        }

       


    }
}
