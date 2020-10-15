using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scullery.Models.ViewModels
{
    public class SearchResults
    {
        public IEnumerable<RecipeSearchResults> Results { get; set; }

    }
}
