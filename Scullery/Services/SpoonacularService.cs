using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Scullery.Models;
using Scullery.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
           
            string url = $"https://api.spoonacular.com/recipes/complexSearch?query={searchInput}&apiKey={ApiKeys.Key}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<RecipeSearchResults>(json);
            }

            return null;
        }

        public async Task<RecipeInformation> GetRecipeInformation(int id)
        {
            string url = $"https://api.spoonacular.com/recipes/{id}/information?includeNutrition=false&apiKey={ApiKeys.Key}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<RecipeInformation>(json);
            }

            return null;

        }

        public async Task<string> ConnectUser(Planner planner)
        {
            var values = new Dictionary<string, string>
            {
                { "username", planner.UserName },
                
            };

            var content = new FormUrlEncodedContent(values);

            string url = $"https://api.spoonacular.com/users/connect?apiKey={ApiKeys.Key}";
            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }



    }
}
