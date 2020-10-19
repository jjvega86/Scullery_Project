using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class MealEvent
    {
        public Int64 Id { get; set; }

        public string Title { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public bool AllDay { get; set; }
    }
}
