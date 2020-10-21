using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult CreateInventory(Planner planner)
        {
            KitchenInventory inventory = new KitchenInventory() { PodId = planner.PodId };
             _context.Add(inventory);
            _context.SaveChanges();

            return RedirectToAction("Index", "Planner");
            
        }
    }
}
