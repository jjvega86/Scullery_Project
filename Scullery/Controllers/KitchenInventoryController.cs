using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scullery.Data;
using Scullery.Models;
using Scullery.Models.ViewModels;
using Scullery.Services;

namespace Scullery.Controllers
{
    public class KitchenInventoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SpoonacularService _spoonacular;


        public KitchenInventoryController(ApplicationDbContext context, SpoonacularService spoonacular)
        {
            _context = context;
            _spoonacular = spoonacular;

        }
        public async Task<IActionResult> Index()
        {
            var planner = GetLoggedInPlanner();
            var inventory =  await _context.KitchenInventories.Where(i => i.PodId == planner.PodId).SingleOrDefaultAsync();

            var pantryIngredients = await _context.Ingredients.Where(i => i.KitchenInventoryId == inventory.KitchenInventoryId).ToListAsync();

            return View(pantryIngredients);
        }

        public async Task<IActionResult> SearchIngredients(string searchString)
        {
            var rawSearchResults = await _spoonacular.AutoCompleteIngredientSearch(searchString);

            IngredientResults results = new IngredientResults();

            foreach (IngredientResult result in rawSearchResults)
            {
                results.Results.Add(result);
            }

            return View("SearchIngredients", results);
        }

        [HttpPost, ActionName("SaveIngredient")]
        [ValidateAntiForgeryToken]

        public IActionResult SaveIngredient(IngredientResult result)
        {
            var ingredientInfo = _spoonacular.GetIngredientInformation(result.id);
            var planner = GetLoggedInPlanner();
            var kitchenInventory = _context.KitchenInventories.Where(i => i.PodId == planner.PodId).SingleOrDefault();

            Ingredient ingredient = new Ingredient();

            ingredient.IngredientName = ingredientInfo.Result.name;
            ingredient.SpoonacularIngredientId = ingredientInfo.Result.id;
            ingredient.KitchenInventoryId = kitchenInventory.KitchenInventoryId;
            ingredient.UnitType = ingredientInfo.Result.unit;
            ingredient.UnitQuantity = ingredientInfo.Result.amount;

            _context.Add(ingredient);
            _context.SaveChanges();



            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _context.Ingredients.Where(r => r.IngredientId == id).SingleOrDefaultAsync();

            _context.Remove(ingredient);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


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

    }
}
