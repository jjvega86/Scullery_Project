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


        // Default view that shows progress bar for Budget and Today's Assigned Meals to prepare
        public ActionResult Index()
        {
            var planner = GetLoggedInPlanner();


            if (planner == null)
            {
                return RedirectToAction("Create");
            }

            if (planner.SpoonacularUserName == null)
            {
                LinkSpoonacularAccount(planner);
            }

            ValidatePlannerKitchenInventory(planner);

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
                var foundingPlanner = _context.Planners.Where(p => p.UserName == pod.FoundingUserName).SingleOrDefault();
                planner.IdentityUserId = GetLoggedInUser();
                planner.PodId = pod.PodId;
                planner.SpoonacularUserName = foundingPlanner.SpoonacularUserName;
                planner.UserHash = foundingPlanner.UserHash;
                
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
                planner.PodId = pod.PodId;
                planner.IdentityUserId = GetLoggedInUser();
                _context.Add(planner);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Create");
           
        }

        private string GetLoggedInUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }

        private Planner GetLoggedInPlanner()
        {
            var planner = _context.Planners.Where(c => c.IdentityUserId == GetLoggedInUser()).SingleOrDefault();

            return planner;

        }

        private void LinkSpoonacularAccount(Planner planner)
        {
            var spoonacularInfo = _spoonacular.ConnectUser(planner);
            planner.SpoonacularUserName = spoonacularInfo.Result.username;
            planner.UserHash = spoonacularInfo.Result.hash;
            _context.Update(planner);
            _context.SaveChanges();

        }

        private IActionResult ValidatePlannerKitchenInventory(Planner planner)
        {
            var plannerInventory = _context.KitchenInventories.Where(i => i.PodId == planner.PodId).SingleOrDefault();

            if (plannerInventory == null)
            {
                KitchenInventory inventory = new KitchenInventory() { PodId = planner.PodId };
                _context.Add(inventory);
                _context.SaveChanges();

                return View(nameof(Index));
            }

            return View(nameof(Index));

        }

        


    }
}
