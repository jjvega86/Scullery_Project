using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class ShoppingList
    {
        public float TotalCost { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public List<Item> Items { get; set; }
    }
}
