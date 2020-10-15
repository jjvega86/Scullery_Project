using Newtonsoft.Json;
using Scullery.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scullery.Services
{
    public class SpoonacularService
    {
        private readonly HttpClient client;
        public SpoonacularService()
        {
            client = new HttpClient();

        }

        public async Task<RecipeSearch> GetSearchResults(string searchInput)
        {
            HttpResponseMessage response = await client.GetAsync($"https://api.spoonacular.com/recipes/complexSearch?query={searchInput}&apiKey={ApiKeys.Key}");
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<RecipeSearch>(json);
            }

            return null;
        }

    }
}
