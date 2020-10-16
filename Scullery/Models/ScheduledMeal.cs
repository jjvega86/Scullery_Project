﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models
{
    public class ScheduledMeal
    {
        
        [Key]
        public int ScheduledMealId { get; set; }

        public int AssignedPlannerId { get; set; }

        [Display(Name = "Recipe")]
        [BindProperty]
        public int SavedRecipeId { get; set; }

        public int MealPlanId { get; set; }

        public DateTime? DateOfMeal { get; set; }
        public int Slot { get; set; } //breakfast, lunch, or dinner according to Spoonacular API
        public bool MealCompleted { get; set; }
        public bool Planned { get; set; } //use to mark this complete and filter once the assigned planner has planned the meal

      
        [NotMapped]
        public SelectList Recipes { get; set; }

        [Display(Name = "Type of Meal")]

        [BindProperty]
        public string MealType { get; set; } // OUT, HOME, UNPLANNED - setting this property will trigger different display logic to handle various use cases
        [NotMapped]
        public SelectList Types { get; set; }

        [NotMapped]
        [BindProperty]
        public string PlannerName { get; set; }
        [NotMapped]
        public SelectList Planners { get; set; }

        
    }
}
