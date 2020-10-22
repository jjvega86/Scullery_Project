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
using Scullery.Utilities;

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
            return View(CreateBudget());
        }

        private Budget CreateBudget()
        {
            // check for a budget during the current week (Sunday - Saturday)
            // if there isn't a budget for the current week, add one
            // carry over the CurrentWeekBudget from previous budget OR add a property to Pod that carries that value with it
            bool currentWeekBudgetExists = false;
            DateTime firstDayOfWeek = TimeTools.FirstDayOfWeek(DateTime.Today);
            DateTime lastDayOfWeek = TimeTools.LastDayOfWeek(DateTime.Today);

            var planner = GetLoggedInPlanner();
            var allPlannerBudgets = _context.Budgets.Where(b => b.PodId == planner.PodId).ToList();
            Budget currentBudget = new Budget();

            foreach(Budget selectedBudget in allPlannerBudgets)
            {
                if(selectedBudget.CurrentWeekStart == firstDayOfWeek)
                {
                    currentWeekBudgetExists = true;
                    currentBudget = selectedBudget;
                    break;
                }
                
            }

            if(currentWeekBudgetExists == false)
            {
                Budget budget = new Budget();
                budget.PodId = planner.PodId;
                budget.CurrentWeekBudget = 0; // Make this carry over from last budget 
                budget.CurrentWeekStart = firstDayOfWeek;
                budget.CurrentWeekEnd = lastDayOfWeek;
                _context.Add(budget);
                _context.SaveChanges();
                currentBudget = budget;

            }

            return currentBudget;

        }

        

        //set up budgets to be automatically created for each week
        //user sets weekly budget amount (that can change via edit action)
        //user enters grocery expenses and budget progress is automatically updated for the week and lifecycle of app usage
    }
}
