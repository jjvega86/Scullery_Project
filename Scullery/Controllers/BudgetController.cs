using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scullery.Data;
using Scullery.Models;
using Scullery.Models.ViewModels;
using Scullery.Services;

namespace Scullery.Controllers
{
    public class BudgetController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SpoonacularService _spoonacular;


        public BudgetController(ApplicationDbContext context, SpoonacularService spoonacular)
        {
            _context = context;
            _spoonacular = spoonacular;

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
        public IActionResult Index()
        {
            var planner = GetLoggedInPlanner();
            
            return View();
        }

        public IActionResult CreateBudget()
        {
            return null;
        }
    }
}
