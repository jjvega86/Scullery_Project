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

        public async Task<RecipeSearchResults> GetSearchResults(string searchInput)
        {
            string key = ApiKeys.Key;
            string url = $"https://api.spoonacular.com/recipes/complexSearch?query={searchInput}&apiKey={key}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<RecipeSearchResults>(json);
            }

            return null;
        }

    }
}
