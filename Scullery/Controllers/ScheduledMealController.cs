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
using System.Collections.Specialized;

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

        private List<ScheduledMeal> GetAllPodScheduledMeals()
        {
            var planner = GetLoggedInPlanner();
            var plannerPod = _context.Pods.Find(planner.PodId);
            var allPlanners =  _context.Planners.Where(p => p.PodId == plannerPod.PodId).ToList();
            List<ScheduledMeal> scheduledMeals = new List<ScheduledMeal>();

            foreach (Planner podMember in allPlanners)
            {
                var theseMeals =  _context.ScheduledMeals.Where(m => m.AssignedPlannerId == podMember.PlannerId).ToList();
                scheduledMeals.AddRange(theseMeals);

            }

            

            return SortMealsByDate(scheduledMeals);

        }

        private List<ScheduledMeal> SortMealsByDate (List<ScheduledMeal> scheduledMeals)
        {
            List<ScheduledMeal> sortedList = scheduledMeals.OrderBy(m => m.DateOfMeal).ToList();

            return sortedList;
        }

        public ActionResult GetMealSchedule()
        {
          
            var scheduledMeals = GetAllPodScheduledMeals();

            List<MealEvent> meals = new List<MealEvent>();

            foreach (ScheduledMeal meal in scheduledMeals)
            {

                if(meal.MealType == "Planned")
                {
                    meals.Add(CreatePlannedMealEvent(meal));

                }
                else
                {
                    meals.Add(CreateLeftoversOrOutMealEvent(meal));

                }

            }

  
            return View(meals);
        }



        private MealEvent CreatePlannedMealEvent(ScheduledMeal meal)
        {
            var recipe = _context.SavedRecipes.Find(meal.SavedRecipeId);
            var mealPlanner = _context.Planners.Find(recipe.PlannerId);

            MealEvent mealEvent = new MealEvent();
            mealEvent.MealDate = meal.DateOfMeal;
            mealEvent.MealType = meal.MealType;
            mealEvent.RecipeName = recipe.RecipeName;
            mealEvent.PlannerName = mealPlanner.FirstName;
            mealEvent.Slot = meal.Slot;
            mealEvent.ScheduledMealId = meal.ScheduledMealId;

            return mealEvent;

        }

        private MealEvent CreateLeftoversOrOutMealEvent(ScheduledMeal meal)
        {
            var mealPlanner = _context.Planners.Find(meal.AssignedPlannerId);

            MealEvent mealEvent = new MealEvent();
            mealEvent.MealDate = meal.DateOfMeal;
            mealEvent.MealType = meal.MealType;
            mealEvent.RecipeName = "None";
            mealEvent.PlannerName = mealPlanner.FirstName;
            mealEvent.Slot = meal.Slot;
            mealEvent.ScheduledMealId = meal.ScheduledMealId;

            return mealEvent;

        }

        public ActionResult CreateShoppingList()
        {
            GenerateShoppingList dates = new GenerateShoppingList();

            return View(dates);
        }

        public ActionResult GenerateShoppingList(GenerateShoppingList dates)
        {
            var planner = GetLoggedInPlanner();
            dates.User = planner.SpoonacularUserName;
            dates.Hash = planner.UserHash;

            return View(); // will return a list of ingredients to the View
        }

       








    }
}
