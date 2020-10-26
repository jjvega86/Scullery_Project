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
                    currentBudget.LifeTimeSavings = currentBudget.CumulativeBudget - currentBudget.CumulativeSpent;
                    break;
                }
                
            }

            return currentBudget;
        }

        public IActionResult SetBudgetAmount()
        {
            var currentBudget = GetCurrentBudget();

            return View(currentBudget);
        }

        [HttpPost, ActionName("SetBudgetAmount")]
        public IActionResult SetBudgetAmount(Budget budget)
        {
            var currentBudget = GetCurrentBudget();

            currentBudget.CurrentWeekBudget = budget.CurrentWeekBudget;
            _context.Update(currentBudget);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult AddExpense()
        {
            BudgetExpenseAmount amount = new BudgetExpenseAmount();

            return View(amount);
        }

        [HttpPost, ActionName("AddExpense")]
        public IActionResult AddExpense(BudgetExpenseAmount amount)
        {
            var currentBudget = GetCurrentBudget();

            currentBudget.CurrentWeekSpent += amount.ExpenseAmount;
            _context.Update(currentBudget);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    
    }
}
