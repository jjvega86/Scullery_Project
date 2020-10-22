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
            var budget = _context.Budgets.Where(b => b.PodId == planner.PodId).FirstOrDefault();

            
            return View(budget);
        }

        public IActionResult CreateBudget()
        {
            // check for a budget during the current week (Sunday - Saturday)
            // if there isn't a budget for the current week, add one
            // carry over the CurrentWeekBudget from previous budget OR add a property to Pod that carries that value with it
           
            var planner = GetLoggedInPlanner();
            Budget budget = new Budget();
            budget.PodId = planner.PodId;
            _context.Add(budget);
            _context.SaveChanges();

            return null;
        }

        

        //set up budgets to be automatically created for each week
        //user sets weekly budget amount (that can change via edit action)
        //user enters grocery expenses and budget progress is automatically updated for the week and lifecycle of app usage
    }
}
