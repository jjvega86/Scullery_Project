﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scullery.Models;

namespace Scullery.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Planner> Planners { get; set; }
        public DbSet<Pod> Pods { get; set; }
        public DbSet<SavedRecipe> SavedRecipes { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<KitchenInventory> KitchenInventories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<ScheduledMeal> ScheduledMeals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<IdentityRole>()
            //.HasData(
            //new IdentityRole
            //{
            //    Name = "Planner",
            //    NormalizedName = "PLANNER"
            //}
            //);

            //builder.Entity<Budget>()
            //    .HasData(
            //    new Budget
            //    {
            //        BudgetId = 1,
            //        PodId = 1,
            //        CurrentWeekBudget = 200,
            //        CurrentWeekSpent = 100,
            //        CurrentWeekStart = new DateTime(2020, 9, 27),
            //        CurrentWeekEnd = new DateTime(2020, 10, 3)

            //    },
            //    new Budget
            //    {
            //        BudgetId = 2,
            //        PodId = 1,
            //        CurrentWeekBudget = 200,
            //        CurrentWeekSpent = 75,
            //        CurrentWeekStart = new DateTime(2020, 10, 4),
            //        CurrentWeekEnd = new DateTime(2020, 10, 10)

            //    },
            //    new Budget
            //    {
            //        BudgetId = 3,
            //        PodId = 1,
            //        CurrentWeekBudget = 200,
            //        CurrentWeekSpent = 185,
            //        CurrentWeekStart = new DateTime(2020, 10, 11),
            //        CurrentWeekEnd = new DateTime(2020, 10, 17)

            //    }
            //    );

            
                
           
        }

    }
}
