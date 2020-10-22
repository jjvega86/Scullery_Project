using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scullery.Data;
using System.Net.Http;
using Newtonsoft.Json;
using Scullery.Services;
using Scullery.Models.ViewModels;
using Scullery.Models;
using Microsoft.EntityFrameworkCore;

namespace Scullery.Controllers
{
    public class SavedRecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SpoonacularService _spoonacular;

        public SavedRecipeController(ApplicationDbContext context, SpoonacularService spoonacular)
        {
            _context = context;
            _spoonacular = spoonacular;

        }

        private string GetLoggedInUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }

        private Planner GetLoggedInPlanner()
        {
            var planner = _context.Planners.Where(c => c.IdentityUserId == GetLoggedInUser()).SingleOrDefault();
            return planner;

        }

        // GET: SavedRecipeController
        // This view will trigger from the navbar "My Recipes"
        // It will take the LoggedInUser and show a list of their SavedRecipes
        // It will also include a search utility to find a recipe using the Spoonacular API
        public async Task <IActionResult> Index()
        {
            var planner = GetLoggedInPlanner();

            var recipeCollection = await _context.SavedRecipes.Where(c => c.PlannerId == planner.PlannerId).ToListAsync();

            return View(recipeCollection);
        }

        public async Task<IActionResult> SearchResults(string searchString)
        {
            //install Spoonacular API as a 
            // take searchString input and GET results from Spoonacular API
            // take those results and post them to a new page that shows all results in a list
            // planner can then add recipes to their collection of recipes
            var rawSearchResults = await _spoonacular.GetSearchResults(searchString);

            SearchResults searchResults = new SearchResults();

            searchResults.Result = rawSearchResults;
            searchResults.Results = rawSearchResults.results.ToList();

            return View("SearchResults", searchResults);

        }

        public async Task<IActionResult> ShowRecipeInformation(int id)
        {
            var planner = GetLoggedInPlanner();

            var rawRecipeInformation = await _spoonacular.GetRecipeInformation(id);
            SavedRecipe recipeToSave = new SavedRecipe();
            recipeToSave.PlannerId = planner.PlannerId;

            recipeToSave.ImageURL = rawRecipeInformation.image;
            recipeToSave.RecipeName = rawRecipeInformation.title;
            recipeToSave.SpoonacularRecipeId = rawRecipeInformation.id;
            recipeToSave.RecipeURL = rawRecipeInformation.spoonacularSourceUrl;




            return View("RecipeInformation", recipeToSave);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Save(SavedRecipe saved)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saved);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return RedirectToAction("Index");

            }
           

        }

        public ActionResult Details(int id)
        {
            var recipe = _context.SavedRecipes.Where(r => r.SavedRecipeId == id).SingleOrDefault();
            return View(recipe);
        }

        // GET: SavedRecipeController/Delete/5
        public ActionResult Delete(int id)
        {
            var recipe = _context.SavedRecipes.Where(r => r.SavedRecipeId == id).SingleOrDefault();

            return View(recipe);
        }

        // POST: SavedRecipeController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.SavedRecipes.Where(r => r.SavedRecipeId == id).SingleOrDefaultAsync();

            _context.Remove(recipe);
           await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }
    }
}
