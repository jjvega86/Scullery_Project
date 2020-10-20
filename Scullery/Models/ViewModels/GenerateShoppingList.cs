using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class GenerateShoppingList
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string User { get; set; }

        public string Hash { get; set; }

        public string StartString { get; set; }

        public string EndString { get; set; }
    }
}
