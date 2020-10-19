using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class MealEvent
    {
        public DateTime? MealDate { get; set; }
        public string MealType { get; set; }

        public string RecipeName { get; set; }

        public string PlannerName { get; set; }

        public int Slot { get; set; }

        public int ScheduledMealId { get; set; }
    }
}
