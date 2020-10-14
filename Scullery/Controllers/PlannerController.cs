using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scullery.Data;
using Scullery.Models;

namespace Scullery.Controllers
{
    public class PlannerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlannerController(ApplicationDbContext context)
        {
            _context = context;

        }

        private Planner GetLoggedInPlanner()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var planner = _context.Planners.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            return planner;
        }


        // GET: PlannerController
        public async Task<IActionResult>  Index()
        {
            var planner = GetLoggedInPlanner();
            return View(planner);
        }

        // GET: PlannerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlannerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlannerController/Create
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

        // GET: PlannerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlannerController/Edit/5
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

        
    }
}
