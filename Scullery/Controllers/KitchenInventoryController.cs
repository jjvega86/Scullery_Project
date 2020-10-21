using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scullery.Data;
using Scullery.Models;
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddIngredient()
        {
            Ingredient ingredient = new Ingredient();

            return View(ingredient);
        }

        [HttpPost, ActionName("AddIngredient")]
        [ValidateAntiForgeryToken]

        public IActionResult AddIngredient(Ingredient ingredient)
        {
            var planner = GetLoggedInPlanner();
            var kitchenInventory = _context.KitchenInventories.Where(i => i.PodId == planner.PodId).SingleOrDefault();


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
