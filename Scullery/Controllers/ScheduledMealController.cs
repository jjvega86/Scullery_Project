﻿using System;
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

        public async Task<IActionResult> GetMealSchedule()
        {
            var planner = GetLoggedInPlanner();
            var scheduledMeals = await _context.ScheduledMeals.Where(m => m.AssignedPlannerId == planner.PlannerId).ToListAsync();

            List<MealEvent> meals = new List<MealEvent>();

            foreach (ScheduledMeal meal in scheduledMeals)
            {
                var recipe = _context.SavedRecipes.Find(meal.SavedRecipeId);
                MealEvent mealEvent = new MealEvent();
                mealEvent.MealDate = meal.DateOfMeal;
                mealEvent.MealType = meal.MealType;
                mealEvent.RecipeName = recipe.RecipeName;
                var mealPlanner = _context.Planners.Find(recipe.PlannerId);
                mealEvent.PlannerName = mealPlanner.FirstName;
                mealEvent.Slot = meal.Slot;

                meals.Add(mealEvent);
            }


            
            return View(meals);
        }

        //// GET: To be used with potential Full Calendar view (after MVP)
        //public ActionResult Calendar()
        //{
        //    return View();
        //}

       


    }
}
