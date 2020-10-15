using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class SearchResults
    {
        public RecipeSearchResults Result { get; set; }

        public IEnumerable<Result> Results { get; set; }

    }
}
