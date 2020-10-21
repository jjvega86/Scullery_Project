using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [ForeignKey("KitchenInventoryId")]
        public int KitchenInventoryId { get; set; }
        public KitchenInventory KitchenInventory { get; set; }

        public int SpoonacularIngredientId { get; set; }

        [Display(Name = "Ingredient Name")]
        public string IngredientName { get; set; }

        [Display(Name = "Unit of Measure")]
        public string UnitType { get; set; }

        [Display(Name = "Unit of Measure Quantity")]
        public double UnitQuantity { get; set; }
        


    }
}
