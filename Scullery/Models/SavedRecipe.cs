using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models
{
    public class SavedRecipe
    {
        [Key]
        public int SavedRecipeId { get; set; }

        [ForeignKey("PlannerId")]
        public int PlannerId { get; set; }
        public Planner Planner { get; set; }
        public int SpoonacularRecipeId { get; set; }
        public string ImageURL { get; set; }
        public string RecipeName { get; set; }
        public string RecipeURL { get; set; }

    }
}
