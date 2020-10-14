using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scullery.Data;

namespace Scullery.Controllers
{
    public class SavedRecipeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SavedRecipeController(ApplicationDbContext context)
        {
            _context = context;

        }


        // GET: SavedRecipeController
        // This view will trigger from the navbar "My Recipes"
        // It will take the LoggedInUser and show a list of their SavedRecipes
        // It will also include a search utility to find a recipe using the Spoonacular API
        public ActionResult Index()
        {
            return View();
        }

        // GET: SavedRecipeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SavedRecipeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SavedRecipeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SavedRecipeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SavedRecipeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SavedRecipeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SavedRecipeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
