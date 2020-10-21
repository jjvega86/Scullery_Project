using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Scullery.Controllers
{
    public class KitchenInventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
