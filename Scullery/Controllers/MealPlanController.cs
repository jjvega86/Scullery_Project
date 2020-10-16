﻿using System;
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

namespace Scullery.Controllers
{
    public class MealPlanController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MealPlanController(ApplicationDbContext context)
        {
            _context = context;

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

        public async Task<IActionResult> ViewPendingMeals()
        {
            var planner = GetLoggedInPlanner();

            var pendingMeals = await _context.ScheduledMeals.Where(m => m.AssignedPlannerId == planner.PlannerId).OrderBy(m => m.DateOfMeal).ToListAsync();

            return View(pendingMeals);

        }

        // GET: MealPlanController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MealPlanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
