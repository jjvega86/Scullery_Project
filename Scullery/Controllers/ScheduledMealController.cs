using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Scullery.Controllers
{
    public class ScheduledMealController : Controller
    {
        // GET: ScheduledMealController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ScheduledMealController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ScheduledMealController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScheduledMealController/Create
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

        // GET: ScheduledMealController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ScheduledMealController/Edit/5
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

        // GET: ScheduledMealController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ScheduledMealController/Delete/5
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
