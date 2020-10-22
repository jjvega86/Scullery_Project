﻿using System;
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
            return View(GetCurrentBudget());
        }

        private Budget GetCurrentBudget()
        {
            
            DateTime firstDayOfWeek = TimeTools.FirstDayOfWeek(DateTime.Today);
            DateTime lastDayOfWeek = TimeTools.LastDayOfWeek(DateTime.Today);

            var planner = GetLoggedInPlanner();
            var allPlannerBudgets = _context.Budgets.Where(b => b.PodId == planner.PodId).ToList();
            Budget currentBudget = new Budget();

            foreach(Budget selectedBudget in allPlannerBudgets)
            {
                if(selectedBudget.CurrentWeekStart == firstDayOfWeek)
                {
                    currentBudget = selectedBudget;
                    break;
                }
                
            }

            return currentBudget;
        }

        public IActionResult SetBudgetAmount()
        {
            var planner = GetLoggedInPlanner();
            var currentBudget = GetCurrentBudget();

            return View(currentBudget);
        }

        [HttpPost, ActionName("SetBudgetAmount")]
        public IActionResult SetBudgetAmount(double amount)
        {
            return null;
        }

        

        //set up budgets to be automatically created for each week **
        //user sets weekly budget amount (that can change via edit action)
        //user enters grocery expenses and budget progress is automatically updated for the week and lifecycle of app usage
    }
}
