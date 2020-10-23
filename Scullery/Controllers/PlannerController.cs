using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scullery.Data;
using Scullery.Models;
using Scullery.Models.ViewModels;
using Scullery.Services;
using Scullery.Utilities;

namespace Scullery.Controllers
{
    public class PlannerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SpoonacularService _spoonacular;


        public PlannerController(ApplicationDbContext context, SpoonacularService spoonacular)
        {
            _context = context;
            _spoonacular = spoonacular;

        }


        // Default view that shows Today's Assigned Meals to prepare
        public ActionResult Index()
        {
            var planner = GetLoggedInPlanner();

            if (planner == null)
            {
                return RedirectToAction("Create");
            }

            if (planner.SpoonacularUserName == null)
            {
                LinkSpoonacularAccount(planner);
            }

            ValidatePlannerKitchenInventory(planner);
            ValidateCurrentBudget(planner);
            var meals = GetTodaysMeals(planner);

            return View(meals);
        }
        public TodaysMeals GetTodaysMeals(Planner planner)
        {
            TodaysMeals meals = new TodaysMeals();
            meals.MealCards = new List<MealCard>();
            meals.PlannerName = planner.FirstName;

            var podMembers = _context.Planners.Where(p => p.PodId == planner.PodId).ToList();

            foreach(Planner podMember in podMembers)
            {
                var allScheduledMeals = _context.ScheduledMeals.Where(m => m.AssignedPlannerId == podMember.PlannerId && m.DateOfMeal == DateTime.Today).ToList();

                foreach(ScheduledMeal meal in allScheduledMeals)
                {                  
                    if(meal.MealType == "Planned")
                    {
                        var recipe = _context.SavedRecipes.Where(r => r.SavedRecipeId == meal.SavedRecipeId).SingleOrDefault();
                        MealCard card = new MealCard();
                        card.CookName = podMember.FirstName;
                        card.ImgUrl = recipe.ImageURL;
                        card.MealType = meal.MealType;
                        card.MealSlot = meal.Slot;
                        card.RecipeUrl = recipe.RecipeURL;
                        card.RecipeName = recipe.RecipeName;

                        meals.MealCards.Add(card);
                    }

                    else if (meal.MealType == "Out")
                    {
                        MealCard card = new MealCard();
                        card.CookName = podMember.FirstName;
                        card.ImgUrl = "https://www.supermarketnews.com/sites/supermarketnews.com/files/styles/article_featured_standard/public/millennials-entertaining-sharing-meal_1.jpg?itok=z2udjYxq";
                        card.MealType = meal.MealType;
                        card.MealSlot = meal.Slot;
                        card.RecipeUrl = "";
                        card.RecipeName = "";

                        meals.MealCards.Add(card);

                    }
                    else
                    {
                        MealCard card = new MealCard();
                        card.CookName = podMember.FirstName;
                        card.ImgUrl = "https://www.washingtonpost.com/resizer/rduyJyxnQahs6-CwLkvPaM4oCx0=/1484x0/arc-anglerfish-washpost-prod-washpost.s3.amazonaws.com/public/6FVR77O5VE5THDHUVRMFLPIZ5Q.jpg";
                        card.MealType = meal.MealType;
                        card.MealSlot = meal.Slot;
                        card.RecipeUrl = "";
                        card.RecipeName = "";

                        meals.MealCards.Add(card);


                    }

                }
            }
            

            return meals;

        }

        // GET: PlannerController/Create
        public ActionResult Create()
        {
            Planner planner = new Planner();
            return View(planner);
        }

        // POST: PlannerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Planner planner)
        {
            if(planner.PodExists == 1)
            {
                var pod = _context.Pods.Where(p => p.PodName == planner.PodName && p.PodPassword == planner.PodPassword).SingleOrDefault();
                var foundingPlanner = _context.Planners.Where(p => p.UserName == pod.FoundingUserName).SingleOrDefault();
                planner.IdentityUserId = GetLoggedInUser();
                planner.PodId = pod.PodId;
                planner.SpoonacularUserName = foundingPlanner.SpoonacularUserName;
                planner.UserHash = foundingPlanner.UserHash;
                
                _context.Add(planner);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else if (planner.PodExists == 2)
            {
                Pod pod = new Pod();
                pod.PodName = planner.PodName;
                pod.PodPassword = planner.PodPassword;
                pod.FoundingUserName = planner.UserName;

                _context.Add(pod);
                _context.SaveChanges();

                var selectedPod = _context.Pods.Where(p => p.PodName == planner.PodName && p.PodPassword == planner.PodPassword).FirstOrDefault();
                planner.PodId = pod.PodId;
                planner.IdentityUserId = GetLoggedInUser();
                _context.Add(planner);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Create");
           
        }

        private string GetLoggedInUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }

        private Planner GetLoggedInPlanner()
        {
            var planner = _context.Planners.Where(c => c.IdentityUserId == GetLoggedInUser()).SingleOrDefault();

            return planner;

        }

        private void LinkSpoonacularAccount(Planner planner)
        {
            var spoonacularInfo = _spoonacular.ConnectUser(planner);
            planner.SpoonacularUserName = spoonacularInfo.Result.username;
            planner.UserHash = spoonacularInfo.Result.hash;
            _context.Update(planner);
            _context.SaveChanges();

        }

        private IActionResult ValidatePlannerKitchenInventory(Planner planner)
        {
            var plannerInventory = _context.KitchenInventories.Where(i => i.PodId == planner.PodId).SingleOrDefault();

            if (plannerInventory == null)
            {
                KitchenInventory inventory = new KitchenInventory() { PodId = planner.PodId };
                _context.Add(inventory);
                _context.SaveChanges();

                return View(nameof(Index));
            }

            return View(nameof(Index));

        }

        private void ValidateCurrentBudget(Planner planner)
        {
            // check for a budget during the current week (Sunday - Saturday)
            // if there isn't a budget for the current week, add one
            // carry over the CurrentWeekBudget from previous budget OR add a property to Pod that carries that value with it
            bool currentWeekBudgetExists = false;
            DateTime firstDayOfWeek = TimeTools.FirstDayOfWeek(DateTime.Today);
            DateTime lastDayOfWeek = TimeTools.LastDayOfWeek(DateTime.Today);

            var allPlannerBudgets = _context.Budgets.Where(b => b.PodId == planner.PodId).ToList();

            foreach (Budget selectedBudget in allPlannerBudgets)
            {
                if (selectedBudget.CurrentWeekStart == firstDayOfWeek)
                {
                    currentWeekBudgetExists = true;
                    break;
                }

            }
            if (currentWeekBudgetExists == false)
            {
                CreateNewBudget(planner.PodId, firstDayOfWeek, lastDayOfWeek);

            }

        }

        private void CreateNewBudget(int podId, DateTime firstDayOfWeek, DateTime lastDayOfWeek)
        {
            Budget budget = new Budget();
            budget.PodId = podId;
            budget.CurrentWeekBudget = InheritPreviousBudgetAmount(podId); // Make this carry over from last budget 
            budget.CurrentWeekStart = firstDayOfWeek;
            budget.CurrentWeekEnd = lastDayOfWeek;
            UpdateCumulativeTotals(budget);

            _context.Add(budget);
            _context.SaveChanges();

        }

        private double InheritPreviousBudgetAmount(int podId)
        {
            var lastBudget = _context.Budgets.Where(b => b.PodId == podId).OrderByDescending(b => b.BudgetId).FirstOrDefault();
            double currentAmount;
            if (lastBudget != null)
            {
                currentAmount = lastBudget.CurrentWeekBudget;


            }
            else
            {
                currentAmount = 0;
            }

            return currentAmount;

        }

        private void UpdateCumulativeTotals(Budget budget)
        {     
            var allBudgets = _context.Budgets.Where(b => b.PodId == budget.PodId).ToList();

            if(allBudgets.Count <= 1)
            {
                budget.CumulativeBudget = 0;
                budget.CumulativeSpent = 0;

            }
            else
            {
                var lastBudget = allBudgets.OrderByDescending(b => b.BudgetId).FirstOrDefault();
                budget.CumulativeBudget = lastBudget.CumulativeBudget + lastBudget.CurrentWeekBudget;
                budget.CumulativeSpent = lastBudget.CumulativeSpent + lastBudget.CurrentWeekSpent;

            }

        }



    }
}
