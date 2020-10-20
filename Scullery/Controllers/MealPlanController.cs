using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scullery.Data;
using Scullery.Models;
using Scullery.Services;
using Scullery.Utilities;

namespace Scullery.Controllers
{
    public class MealPlanController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly SpoonacularService _spoonacular;

        public MealPlanController(ApplicationDbContext context, SpoonacularService spoonacular)
        {
            _context = context;
            _spoonacular = spoonacular;

        }

        private string GetLoggedInUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }

        private Planner GetLoggedInPlanner()
        {
            var planner =  _context.Planners.Where(c => c.IdentityUserId == GetLoggedInUser()).SingleOrDefault();
            return planner;

        }


        // GET: MealPlanController
        public ActionResult Index()
        {

            var planner = GetLoggedInPlanner();
            var allMealPlans =  _context.MealPlans.Where(p => p.PodId == planner.PodId).ToList();


            return View(allMealPlans);
        }

        // GET: MealPlanController/Create
        public ActionResult Create()
        {
            MealPlan mealPlan = new MealPlan();
            return View(mealPlan);
        }

        // POST: MealPlanController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealPlan mealPlan)
        {
            if (ModelState.IsValid)
            {
                var planner = GetLoggedInPlanner();
                mealPlan.PodId = planner.PodId;

                await _context.AddAsync(mealPlan);
                await _context.SaveChangesAsync();

                await CreateMealsToBePlanned(mealPlan);

                // this will redirect to an "assign" action that allows the meal plan creator to assign pod members to meal plan
                return RedirectToAction("ViewMealsToAssign", mealPlan);

            }
            else
            {
                return null;
            }
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        private async Task CreateMealsToBePlanned(MealPlan mealPlan)
        {
            foreach(DateTime day in EachDay(mealPlan.StartDate.Value, mealPlan.EndDate.Value))
            {
                ScheduledMeal meal1 = new ScheduledMeal();
                ScheduledMeal meal2 = new ScheduledMeal();
                ScheduledMeal meal3 = new ScheduledMeal();

                meal1.DateOfMeal = day;
                meal1.Slot = 1;
                meal1.MealPlanId = mealPlan.MealPlanId;
                await _context.AddAsync(meal1);

                meal2.DateOfMeal = day;
                meal2.Slot = 2;
                meal2.MealPlanId = mealPlan.MealPlanId;
                await _context.AddAsync(meal2);


                meal3.DateOfMeal = day;
                meal3.Slot = 3;
                meal3.MealPlanId = mealPlan.MealPlanId;
                await _context.AddAsync(meal3);

            }

            await _context.SaveChangesAsync();

        }

        //GET meal plan and direct to view that contains all unplanned meals in a list
        //with an option to select a pod member to plan the meal
        public ActionResult ViewMealsToAssign(MealPlan mealPlan)
        {
            var mealsToAssign =  _context.ScheduledMeals.Where(m => m.MealPlanId == mealPlan.MealPlanId).ToList();
            var finalMealsToAssign = mealsToAssign.Where(m => m.AssignedPlannerId == 0).OrderBy(m => m.DateOfMeal).ToList();

            return View(finalMealsToAssign);
            
        }

   

        // GET: MealPlanController/Edit/5
        public ActionResult Edit(int id)
        {
            var meal = _context.ScheduledMeals.Find(id);
            var mealPlan = _context.MealPlans.Find(meal.MealPlanId);
            var planners = _context.Planners.Where(p => p.PodId == mealPlan.PodId).ToList();

            var plannerNames = new List<string>();

            foreach(Planner planner in planners)
            {
                plannerNames.Add(planner.FirstName);
            }
            
            meal.Planners = new SelectList(plannerNames);
            return View(meal);
        }

        // POST: MealPlanController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitEdit(ScheduledMeal meal)
        {
            var assignedPlanner = _context.Planners.Where(n => n.FirstName == meal.PlannerName).FirstOrDefault();

            meal.AssignedPlannerId = assignedPlanner.PlannerId;
            _context.Update(meal);
            _context.SaveChanges();
            var mealPlan = _context.MealPlans.Find(meal.MealPlanId);

            return RedirectToAction("ViewMealsToAssign", mealPlan);

        }

        public ActionResult ViewPendingMeals()
        {
            var planner = GetLoggedInPlanner();

            var pendingMeals = _context.ScheduledMeals.Where(m => m.AssignedPlannerId == planner.PlannerId).ToList();
            var sortedPendingMeals = pendingMeals.Where(m => m.Planned == false).OrderBy(m => m.DateOfMeal).ToList();

            return View(sortedPendingMeals);

        }

        //GET: Plan Meal
        public ActionResult Plan(int id)
        {
            var mealToPlan = _context.ScheduledMeals.Find(id);

            var recipes = _context.SavedRecipes.Where(r => r.PlannerId == mealToPlan.AssignedPlannerId).ToList();
            SavedRecipe blankRecipe = new SavedRecipe();
            blankRecipe.RecipeName = "None";
            recipes.Add(blankRecipe);

            
            mealToPlan.Recipes = new SelectList(recipes, nameof(SavedRecipe.SavedRecipeId), nameof(SavedRecipe.RecipeName));
            mealToPlan.Types = new SelectList(AddMealTypes());

            return View(mealToPlan);
        }

        [HttpPost, ActionName("Plan")]
        [ValidateAntiForgeryToken]
        public ActionResult SavePlan (ScheduledMeal meal)
        {
            meal.Planned = true;
            _context.Update(meal);
            _context.SaveChanges();

            if (meal.MealType == "Planned")
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var planner = _context.Planners.Where(c => c.IdentityUserId == GetLoggedInUser()).SingleOrDefault();
                SpoonacularUserInfo userInfo = new SpoonacularUserInfo { hash = planner.UserHash, username = planner.SpoonacularUserName };
                AddToSpoonacularMealPlan(meal, userInfo); // add meal to Spoonacular API meal plan if it's a recipe

            }

            var mealPlan = _context.MealPlans.Find(meal.MealPlanId);

            return RedirectToAction("ViewPendingMeals", mealPlan);
        }

        private async void AddToSpoonacularMealPlan(ScheduledMeal meal, SpoonacularUserInfo userInfo)
        {
            RecipeAddToMealPlan recipe = new RecipeAddToMealPlan();
            var savedRecipe = _context.SavedRecipes.Find(meal.SavedRecipeId);
            recipe.date = (int)Int64.Parse(UserTools.GetTimeStamp(meal.DateOfMeal.Value));
            recipe.slot = meal.Slot;
            recipe.type = "RECIPE";
            recipe.value = new Value();
            recipe.value.id = savedRecipe.SpoonacularRecipeId;
            recipe.value.imageType = "jpg";
            recipe.value.title = savedRecipe.RecipeName;
            recipe.value.servings = GetPodCount(); // sets servings to the number of pod members (assuming enough food is needed for all planners)
            // look at giving the user the option to set serving amount (if they have kids, they'll need more)

            await _spoonacular.AddRecipeToMealPlan(recipe, userInfo);
                
        }

        private int GetPodCount()
        {
            int podMembersCount = _context.Planners.Where(p => p.PodId == GetLoggedInPlanner().PodId).Count();
            return podMembersCount;
        }



        private List<string> AddMealTypes()
        {
            List<string> mealTypes = new List<string>() { "Leftovers", "Out", "Free", "Planned"};

            return mealTypes;
        }

        
    }
}
