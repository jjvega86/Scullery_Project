using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scullery.Data;
using Scullery.Models;
using Scullery.Services;

namespace Scullery.Controllers
{
    public class PlannerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SpoonacularService _spoonacular;


        public PlannerController(ApplicationDbContext context, SpoonacularService spoonacular)
        {
            _context = context;
            _spoonacular = spoonacular;

        }

        private string GetLoggedInUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }

        private Planner GetLoggedInPlanner()
        {
            var planner =  _context.Planners.Where(c => c.IdentityUserId == GetLoggedInUser()).SingleOrDefault();

            if(planner.SpoonacularUserName == null)
            {
                var spoonacularInfo = _spoonacular.ConnectUser(planner);
                planner.SpoonacularUserName = spoonacularInfo.Result.username;
                planner.UserHash = spoonacularInfo.Result.hash;
                _context.Update(planner);
                _context.SaveChanges();
            }

            return planner;

        }
       



        // GET: PlannerController
        // Default view that shows progress bar for Budget and Today's Assigned Meals to prepare
        public ActionResult Index()
        {
            var planner = GetLoggedInPlanner();

            if (planner == null)
            {
                return RedirectToAction("Create");
            }

            return View(planner);
        }

        // GET: PlannerController/Create
        public ActionResult Create()
        {
            Planner planner = new Planner();
            return View(planner);
        }

        // POST: PlannerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Planner planner)
        {
            if(planner.PodExists == 1)
            {
                var pod = _context.Pods.Where(p => p.PodName == planner.PodName && p.PodPassword == planner.PodPassword).SingleOrDefault();
                planner.IdentityUserId = GetLoggedInUser();
                planner.PodId = pod.PodId;
                _context.Add(planner);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else if (planner.PodExists == 2)
            {
                Pod pod = new Pod();
                pod.PodName = planner.PodName;
                pod.PodPassword = planner.PodPassword;
                pod.FoundingUserName = planner.UserName;

                _context.Add(pod);
                _context.SaveChanges();

                var selectedPod = _context.Pods.Where(p => p.PodName == planner.PodName && p.PodPassword == planner.PodPassword).FirstOrDefault();
                planner.IdentityUserId = GetLoggedInUser();
                planner.PodId = pod.PodId;
                _context.Add(planner);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Create");
           
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
