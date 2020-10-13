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
        public string IngredientName { get; set; }
        public string UnitType { get; set; }
        public double UnitQuantity { get; set; }
        


    }
}
