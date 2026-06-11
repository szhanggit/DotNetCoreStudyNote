using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class CubedController : Controller
    {
        public IActionResult Index()
        {
            return View("Cubed");
        }

        public IActionResult Cube(double num)
        {
            //TempData["value"] = num.ToString();
            //TempData["result"] = Math.Pow(num, 3).ToString();
            //return RedirectToAction(nameof(Index));

            Value = num.ToString();
            Result = Math.Pow(num, 3).ToString();
            return RedirectToAction(nameof(Index));
        }

        [TempData]
        public string Value { get; set; }
        [TempData]
        public string Result { get; set; }
    }
}
