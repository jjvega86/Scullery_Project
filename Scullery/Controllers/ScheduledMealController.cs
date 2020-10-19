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
    public class ScheduledMealController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduledMealController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: ScheduledMealController
        public ActionResult Calendar()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            var scheduledMeals = _context.ScheduledMeals.ToList();
            MealEvent event = new MealEvent();
           
            var events = dc.Events.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
        }


    }
}
