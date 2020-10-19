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
        public ActionResult Calendar()
        {
            return View();
        }

      
    }
}
