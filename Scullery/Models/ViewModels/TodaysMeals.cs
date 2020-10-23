using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class TodaysMeals
    {
        public string PlannerName { get; set; }

        public List<MealCard> MealCards { get; set; }

    }

    public class MealCard
    {
        public string CookName { get; set; }

        public string MealType { get; set; }

        public string MealSlot { get; set; }

        public string ImgUrl { get; set; }

        public string RecipeUrl { get; set; }

    }
}
